using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Dynamic;
using MCC71.Context;
using MCC71.Models;
using MCC71.Repositories.Data;

namespace MCC71 
{
    public class Program
    {
        public static void Main(string[] args)
        {           
            DivisionRepository divisionRepository = new DivisionRepository();
            DepartmentRepository departmentRepository = new DepartmentRepository();

            #region Division

            #region GET ALL
            //var data = divisionRepository.Get();
            //foreach (var item in data)
            //{
            //    Console.WriteLine(item.Id);
            //    Console.WriteLine(item.Name);
            //}
            #endregion

            #region GET BY ID
            //var data = divisionRepository.Get(1);
            //if (data != null)
            //{
            //    Console.WriteLine(data.Id);
            //    Console.WriteLine(data.Name);
            //}
            //else
            //{
            //    Console.WriteLine("Data not found");
            //}
            #endregion

            #region INSERT
            //Division division = new Division("General Affair");
            //var result = divisionRepository.Insert(division);
            //if (result > 0)
            //{
            //    Console.WriteLine("Data has been successfully inserted");
            //}
            //else
            //{
            //    Console.WriteLine("Failed to insert data");
            //}            
            #endregion

            #region UPDATE
            //Division division = new Division(7, "GA");
            //var result = divisionRepository.Update(division);
            //if (result > 0)
            //{
            //    Console.WriteLine("Data has been successfully updated");

            //    var data = divisionRepository.Get();
            //    foreach (var item in data)
            //    {
            //        Console.WriteLine(item.Id);
            //        Console.WriteLine(item.Name);
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("Failed to update data");
            //}
            #endregion

            #region DELETE
            //var result = divisionRepository.Delete(7);
            //if (result > 0)
            //{
            //    Console.WriteLine("Data has been successfully deleted");
            //    var data = divisionRepository.Get();
            //    foreach (var item in data)
            //    {
            //        Console.WriteLine(item.Id);
            //        Console.WriteLine(item.Name);
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("Failed to delete data");
            //}
            #endregion

            #endregion

            #region Department

            #region GET ALL
            var data = departmentRepository.Get();
            foreach (var item in data)
            {
                Console.WriteLine("ID           : " + item.Id);
                Console.WriteLine("Name         : " + item.Nama);
                Console.WriteLine("Division ID  : " + item.DivisionId);
            }
            #endregion

            #region GET BY ID
            //var data = departmentRepository.Get(1);
            //if (data != null)
            //{
            //    Console.WriteLine("ID           : " + data.Id);
            //    Console.WriteLine("Name         : " + data.Nama);
            //    Console.WriteLine("Division ID  : " + data.DivisionId);

            //}
            //else
            //{
            //    Console.WriteLine("Data not found");
            //}
            #endregion

            #region INSERT
            //Department department = new Department("Quality Control", 1);
            //var result = departmentRepository.Insert(department);
            //if (result > 0)
            //{
            //    Console.WriteLine("Data has been successfully inserted");
            //    var data = departmentRepository.Get();
            //    foreach (var item in data)
            //    {
            //        Console.WriteLine("ID           : " + item.Id);
            //        Console.WriteLine("Name         : " + item.Nama);
            //        Console.WriteLine("Division ID  : " + item.DivisionId);
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("Failed to insert data");
            //}
            #endregion

            #region UPDATE
            //Department department = new Department(4, "QC");
            //var result = departmentRepository.Update(department);
            //if (result > 0)
            //{
            //    Console.WriteLine("Data has been successfully updated");

            //    var data = departmentRepository.Get();
            //    foreach (var item in data)
            //    {
            //        Console.WriteLine("ID           : " + item.Id);
            //        Console.WriteLine("Name         : " + item.Nama);
            //        Console.WriteLine("Division ID  : " + item.DivisionId);
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("Failed to update data");
            //}
            #endregion

            #region DELETE
            //var result = departmentRepository.Delete(4);
            //if (result > 0)
            //{
            //    Console.WriteLine("Data has been successfully deleted");
            //    var data = departmentRepository.Get();
            //    foreach (var item in data)
            //    {
            //        Console.WriteLine("ID           : " + item.Id);
            //        Console.WriteLine("Name         : " + item.Nama);
            //        Console.WriteLine("Division ID  : " + item.DivisionId);
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("Failed to delete data");
            //}
            #endregion

            #endregion

        }
    }
}