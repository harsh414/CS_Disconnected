using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Entities;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Application.DataAccess
{
    public class EmployeeDataAccess : IDataAccess<Employee, int>
    {
        SqlConnection Conn;
        SqlDataAdapter AdDpt;
        DataSet Ds;

        public EmployeeDataAccess()
        {
            Conn = new SqlConnection("Data Source=IN-9RVTJM3;Initial Catalog=Ucompany;Integrated Security=SSPI");
            Ds = new DataSet();
            AdDpt = new SqlDataAdapter("Select * from Employee", Conn);
            // Ask the Adapter to use the Primary Key
            AdDpt.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            FillData();
        }

        private void FillData()
        {
            // Fill Data into the DataSet
            AdDpt.Fill(Ds, "Employee");
        }
        Employee IDataAccess<Employee, int>.Create(Employee entity)
        {
            Employee d = new Employee();
            try
            {
                // 1. Create a New Row Object based in Table in the DataSet
                DataRow DrNew = Ds.Tables["Employee"].NewRow();
                // 2. Specify the Column Values
                DrNew["EmpNo"] = entity.EmpNo;
                DrNew["EmpName"] = entity.EmpName;
                DrNew["Designation"] = entity.Designation;
                DrNew["Salary"] = entity.Salary;
                DrNew["DeptNo"] = entity.DeptNo;
                // 3. Add this Record in Employee Table of DataSet
                Ds.Tables["Employee"].Rows.Add(DrNew);
                // 4. Create a Command Builder
                SqlCommandBuilder builder = new SqlCommandBuilder(AdDpt);
                // 5. Update DataBase
                AdDpt.Update(Ds, "Employee");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error While Insert: {ex.Message}");
            }

            return d;
        }

        Employee IDataAccess<Employee, int>.Delete(int id)
        {
            Employee d = new Employee();
            try
            {
                // 1. Find Record based on the Primary Key
                DataRow DrFind = Ds.Tables["Employee"].Rows.Find(id);
                // 2. Delete means Row will be marked for Deletion
                DrFind.Delete();
                SqlCommandBuilder builder = new SqlCommandBuilder(AdDpt);
                // 4. Update DataBase
                AdDpt.Update(Ds, "Employee");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error While Update: {ex.Message}");
            }
            return d;
        }

        IEnumerable<Employee> IDataAccess<Employee, int>.Get()
        {
            List<Employee> employees = new List<Employee>();
            try
            {

                for (int i = 0; i < Ds.Tables["Employee"].Rows.Count; i++)
                {
                    Employee employee = new Employee();
                    employee.EmpNo = Convert.ToInt32(Ds.Tables["Employee"].Rows[i]["EmpNo"]);
                    employee.EmpName = Ds.Tables["Employee"].Rows[i]["EmpName"].ToString();
                    employee.Designation = Ds.Tables["Employee"].Rows[i]["Designation"].ToString();
                    employee.Salary = Convert.ToInt32(Ds.Tables["Employee"].Rows[i]["Salary"]);
                    employee.DeptNo = Convert.ToInt32(Ds.Tables["Employee"].Rows[i]["DeptNo"]);
                    employees.Add(employee);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error While Insert: {ex.Message}");
            }

            return employees;
        }

        Employee IDataAccess<Employee, int>.Get(int id)
        {
            Employee d = new Employee();
            try
            {
                // 1. Find Record based on the Primary Key
                DataRow DrFind = Ds.Tables["Employee"].Rows.Find(id);
                d.EmpNo = Convert.ToInt32(DrFind["EmpNo"]);
                d.EmpName = DrFind["EmpName"].ToString();
                d.Designation = DrFind["Designation"].ToString();
                d.Salary = Convert.ToInt32(DrFind["Salary"]);
                d.DeptNo = Convert.ToInt32(DrFind["DeptNo"]);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error While Update: {ex.Message}");
            }
            return d;
        }

        Employee IDataAccess<Employee, int>.Update(int id, Employee entity)
        {
            Employee d = new Employee();
            try
            {
                // 1. Find Record based on the Primary Key
                DataRow DrFind = Ds.Tables["Employee"].Rows.Find(id);
                // 2. Update Its Values
                DrFind["EmpName"] = entity.EmpName;
                DrFind["Designation"] = entity.Designation;
                DrFind["Salary"] = entity.Salary;
                DrFind["DeptNo"] = entity.DeptNo;

                SqlCommandBuilder builder = new SqlCommandBuilder(AdDpt);

                AdDpt.Update(Ds, "Employee");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error While Update: {ex.Message}");
            }

            return d;
        }
    }
}
