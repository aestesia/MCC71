//GET DATA GENDER
const dataGenders = [];

$.ajax({
    url: 'https://localhost:44393/api/Employee',
    type: "GET",
    dataType: 'json',
    async: false
}).done((res)=>{
    //dataGenders = res.data;
    $.each(res.data, function (key, val) {
        dataGenders.push(val.gender)
    })
});

const genderM = dataGenders.filter(getMale).length;
const genderF = dataGenders.filter(getFemale).length;

function getMale(dataGenders) {
    return dataGenders == "M";
}
function getFemale(dataGenders) {
    return dataGenders == "F";
}

//BAR CHART GENDER
var options = {
    series: [genderM, genderF],
    chart: {
        width: 380,
        type: 'pie',
    },
    labels: ['Male', 'Female'],
    responsive: [{
        breakpoint: 480,
        options: {
            chart: {
                width: 200
            },
            legend: {
                position: 'bottom'
            }
        }
    }]
};

var chart = new ApexCharts(document.querySelector("#chartGender"), options);

chart.render();


//GET DATA DEPARTMENT
const dataDiv = [];
$.ajax({
    url: 'https://localhost:44393/api/Department',
    type: "GET",
    dataType: 'json',
    async: false
}).done((res) => {
    //dataGenders = res.data;
    $.each(res.data, function (key, val) {
        dataDiv.push(val.divisionId)
    })
});

const divIT = dataDiv.filter(getIT).length;
const divSale = dataDiv.filter(getSale).length;
const divRM = dataDiv.filter(getRM).length;

function getIT(dataDiv) {
    return dataDiv == 1;
}
function getSale(dataDiv) {
    return dataDiv == 2;
}
function getRM(dataDiv) {
    return dataDiv == 3;
}

//PIE CHART DEPARTMENT
var options = {
    chart: {
        type: 'bar'
    },
    series: [{
        name: 'IT',
        data: [divIT]
    },
    {
        name: 'Sale',
        data: [divSale]
    },
    {
        name: 'RM',
        data: [divRM]
    }],
    xaxis: {
        categories: ['Divisions']
    }
}

var chart = new ApexCharts(document.querySelector("#chartDept"), options);
chart.render();