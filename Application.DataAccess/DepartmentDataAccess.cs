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
    public class DepartmentDataAccess : IDataAccess<Department, int>
    {
        SqlConnection Conn;
        SqlDataAdapter AdDpt;
        DataSet Ds;

        public DepartmentDataAccess()
        {
            Conn = new SqlConnection("Data Source=IN-9RVTJM3;Initial Catalog=Ucompany;Integrated Security=SSPI");
            Ds = new DataSet();
            AdDpt = new SqlDataAdapter("Select * from Department", Conn);
            // Ask the Adapter to use the Primary Key
            AdDpt.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            FillData();
        }

        private void FillData()
        {
            // Fill Data into the DataSet
            AdDpt.Fill(Ds, "Department");
        }
        Department IDataAccess<Department, int>.Create(Department entity)
        {
            Department d = new Department();
            try
            {
                // 1. Create a New Row Object based in Table in the DataSet
                DataRow DrNew = Ds.Tables["Department"].NewRow();
                // 2. Specify the Column Values
                DrNew["DeptNo"] = entity.DeptNo;
                DrNew["DeptName"] = entity.DeptName;
                DrNew["Location"] = entity.Location;
                DrNew["Capacity"] = entity.Capacity;
                // 3. Add this Record in Department Table of DataSet
                Ds.Tables["Department"].Rows.Add(DrNew);
                // 4. Create a Command Builder
                SqlCommandBuilder builder = new SqlCommandBuilder(AdDpt);
                // 5. Update DataBase
                AdDpt.Update(Ds, "Department");
               
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error While Insert: {ex.Message}");
            }

            return d;
        }

        Department IDataAccess<Department, int>.Delete(int id)
        {
            Department d = new Department();
            try
            {
                // 1. Find Record based on the Primary Key
                DataRow DrFind = Ds.Tables["Department"].Rows.Find(id);
                // 2. Delete means Row will be marked for Deletion
                DrFind.Delete();
                SqlCommandBuilder builder = new SqlCommandBuilder(AdDpt);
                // 4. Update DataBase
                AdDpt.Update(Ds, "Department");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error While Update: {ex.Message}");
            }
            return d;
        }

        IEnumerable<Department> IDataAccess<Department, int>.Get()
        {
            List<Department> departments = new List<Department>();
            try
            {
                
                for (int i = 0; i < Ds.Tables["Department"].Rows.Count; i++)
                {
                    Department department = new Department();
                    department.DeptNo = Convert.ToInt32(Ds.Tables["Department"].Rows[i]["DeptNo"]);
                    department.DeptName = Ds.Tables["Department"].Rows[i]["DeptName"].ToString();
                    department.Location = Ds.Tables["Department"].Rows[i]["Location"].ToString();
                    department.Capacity = Convert.ToInt32(Ds.Tables["Department"].Rows[i]["DeptNo"]);
                    departments.Add(department);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error While Insert: {ex.Message}");
            }

            return departments;
        }

        Department IDataAccess<Department, int>.Get(int id)
        {
            Department d = new Department();
            try
            {
                // 1. Find Record based on the Primary Key
                DataRow DrFind = Ds.Tables["Department"].Rows.Find(id);
                d.DeptNo = Convert.ToInt32(DrFind["DeptNo"]);
                d.DeptName = DrFind["DeptName"].ToString();
                d.Location = DrFind["Location"].ToString();
                d.Capacity = Convert.ToInt32(DrFind["Capacity"]);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error While Update: {ex.Message}");
            }
            return d;
        }

        Department IDataAccess<Department, int>.Update(int id, Department entity)
        {
            Department d = new Department();
            try
            {
                // 1. Find Record based on the Primary Key
                DataRow DrFind = Ds.Tables["Department"].Rows.Find(id);
                // 2. Update Its Values
                DrFind["DeptName"] = entity.DeptName;
                DrFind["Location"] = entity.Location;
                DrFind["Capacity"] = entity.Capacity;
                
                SqlCommandBuilder builder = new SqlCommandBuilder(AdDpt);
           
                AdDpt.Update(Ds, "Department");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error While Update: {ex.Message}");
            }

            return d;
        }
    }
}
