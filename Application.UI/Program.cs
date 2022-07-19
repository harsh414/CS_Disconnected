using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Application.Entities;
using Application.DataAccess;


namespace Application.UI
{
    internal class Program
    {
        static IDataAccess<Employee, int> dsEmp = new EmployeeDataAccess();
        static IDataAccess<Department, int> dsDept = new DepartmentDataAccess();
        static void Main(string[] args)
        {
            string chooseBetweenCases = "y";
            do
            {
                Console.WriteLine("Choose Department or Employees for carrying operations");
                Console.WriteLine("1. Operations on Departments");
                Console.WriteLine("2. Operations on Employees");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    // *************************FOR DEPARTMENT ONLY*********************
                    case 1:
                        string canContinue = "y";
                        do
                        {
                            Console.WriteLine("ADO.NET Connected Architecture");
                            Console.WriteLine("Enter your Choice");
                            Console.WriteLine("1. Read All Records");
                            Console.WriteLine("2. Read Record by Primary Key");
                            Console.WriteLine("3. Create New Record");
                            Console.WriteLine("4. Update Exisiting Record");
                            Console.WriteLine("5. Delete Record");
                            Console.WriteLine("Enter the Option");
                            int input = Convert.ToInt32(Console.ReadLine());
                            switch (input)
                            {
                                case 1:
                                    try
                                    {
                                        var Departments = dsDept.Get();
                                        PrintResults(Departments);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine($"Error Occurred {ex.Message}");
                                    }
                                    break;
                                case 2:
                                    try
                                    {
                                        Console.WriteLine("Enter Id OF department");
                                        int d_id = Convert.ToInt32(Console.ReadLine());
                                        var Department = dsDept.Get(d_id);
                                        Console.WriteLine("DeptNo   DeptName    Location    Capacity");
                                        Console.WriteLine($"{Department.DeptNo}  {Department.DeptName} {Department.Location} {Department.Capacity}");
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine($"Error Occurred {ex.Message}");
                                    }
                                    break;
                                case 3:
                                    try
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine("Enter Department Details To add");
                                        Department department = new Department();
                                        Console.WriteLine("DeptNo");
                                        department.DeptNo = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine("DeptName");
                                        department.DeptName = Console.ReadLine();
                                        Console.WriteLine("Location");
                                        department.Location = Console.ReadLine();
                                        Console.WriteLine("Capacity");
                                        department.Capacity = Convert.ToInt32(Console.ReadLine());
                                        CreateDepartment(department);
                                        var Departments = dsDept.Get();
                                        PrintResults(Departments);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine($"Error Occurred {ex.Message}");
                                    }
                                    break;
                                case 4:
                                    Console.WriteLine("Enter the Dept Id you want to update");
                                    int deptId = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("Enter \n 1. To Update Department Name \n 2. To Update Location \n 3. To Update Capacity");
                                    var Depart = dsDept.Get(deptId);
                                    int updateChoice = Convert.ToInt32(Console.ReadLine());
                                    switch (updateChoice)
                                    {
                                        case 1:
                                            Console.WriteLine("Enter the new dept name");
                                            Depart.DeptName = Console.ReadLine();
                                            break;
                                        case 2:
                                            Console.WriteLine("Enter the new dept location");
                                            Depart.Location = Console.ReadLine();
                                            break;
                                        case 3:
                                            Console.WriteLine("Enter the new dept capacity");
                                            Depart.Capacity = Convert.ToInt32(Console.ReadLine());
                                            break;
                                        default:
                                            break;
                                    }
                                    UpdateDepartment(deptId, Depart);
                                    var all_Departments_after_update = dsDept.Get();
                                    PrintResults(all_Departments_after_update);
                                    break;
                                case 5:
                                    Console.WriteLine("Enter Id OF departmentyou want to delete");
                                    int d_id_to_delete = Convert.ToInt32(Console.ReadLine());
                                    DeleteDepartment(d_id_to_delete);
                                    var all_Departments = dsDept.Get();
                                    PrintResults(all_Departments);
                                    break;
                                default:
                                    break;

                            }
                            Console.WriteLine("Enter y or Y to continue");
                            canContinue = Console.ReadLine();
                            Console.Clear(); // Clearing the Screen
                        } while (canContinue == "y" || canContinue == "Y");
                        Console.ReadLine();
                        break;
                    //********************** DEPT ENDS********************





                    case 2:
                        // EMPLOYEE LOGIC
                        string canContinueEmployeeLogic = "y";
                        do
                        {
                            Console.WriteLine("Performing operation on employees now");
                            Console.WriteLine("Enter your Choice");
                            Console.WriteLine("1. Read All Employee");
                            Console.WriteLine("2. Read Employee by Primary Key");
                            Console.WriteLine("3. Create New Employee");
                            Console.WriteLine("4. Update Exisiting Employee");
                            Console.WriteLine("5. Delete Employee");
                            Console.WriteLine("Enter the Option");
                            int input2 = Convert.ToInt32(Console.ReadLine());
                            switch (input2)
                            {
                                case 1:
                                    try
                                    {
                                        var Employees = dsEmp.Get();
                                        PrintResultsEmployee(Employees);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine($"Error Occurred {ex.Message}");
                                    }
                                    break;
                                case 2:
                                    try
                                    {
                                        Console.WriteLine("Enter Id OF employee");
                                        int e_id = Convert.ToInt32(Console.ReadLine());
                                        var Employee = dsEmp.Get(e_id);
                                        Console.WriteLine("EmpNo   EmpName    Designation    Salary     DepartmentNo");
                                        Console.WriteLine($"{Employee.EmpNo}  {Employee.EmpName} {Employee.Designation} {Employee.Salary} {Employee.DeptNo}");
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine($"Error Occurred {ex.Message}");
                                    }
                                    break;
                                case 3:
                                    try
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine("Enter Employee Details To add");
                                        Employee employee = new Employee();
                                        Console.WriteLine("EmpNo");
                                        employee.EmpNo = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine("EmpName");
                                        employee.EmpName = Console.ReadLine();
                                        Console.WriteLine("Designation");
                                        employee.Designation = Console.ReadLine();
                                        Console.WriteLine("Salary");
                                        employee.Salary = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine("DeptNo");
                                        employee.DeptNo = Convert.ToInt32(Console.ReadLine());
                                        dsEmp.Create(employee);

                                        var Employees = dsEmp.Get();
                                        PrintResultsEmployee(Employees);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine($"Error Occurred {ex.Message}");
                                    }
                                    break;
                                case 4:
                                    Console.WriteLine("Enter the Emp Id you want to update");
                                    int EmpId = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("Enter \n 1. To Update Emp Name \n 2. To Update Designation \n 3. To Update Salary \n 4. To update DeptNo");
                                    var Emplo = dsEmp.Get(EmpId);
                                    int updateCh = Convert.ToInt32(Console.ReadLine());
                                    switch (updateCh)
                                    {
                                        case 1:
                                            Console.WriteLine("Enter the new emp name");
                                            Emplo.EmpName = Console.ReadLine();
                                            break;
                                        case 2:
                                            Console.WriteLine("Enter the new designation");
                                            Emplo.Designation = Console.ReadLine();
                                            break;
                                        case 3:
                                            Console.WriteLine("Enter the new salary");
                                            Emplo.Salary = Convert.ToInt32(Console.ReadLine());
                                            break;
                                        case 4:
                                            Console.WriteLine("Enter the new DeptNo");
                                            Emplo.DeptNo = Convert.ToInt32(Console.ReadLine());
                                            break;
                                        default:
                                            break;
                                    }
                                    UpdateEmployee(EmpId, Emplo);
                                    var all_Employees_after_update = dsEmp.Get();
                                    PrintResultsEmployee(all_Employees_after_update);
                                    break;
                                case 5:
                                    Console.WriteLine("Enter Id OF employee you want to delete");
                                    int e_id_to_delete = Convert.ToInt32(Console.ReadLine());
                                    DeleteEmployee(e_id_to_delete);
                                    var all_Employees = dsEmp.Get();
                                    PrintResultsEmployee(all_Employees);
                                    break;
                                default:
                                    break;

                            }
                            Console.WriteLine("Enter y or Y to continue");
                            canContinueEmployeeLogic = Console.ReadLine();
                            Console.Clear(); // Clearing the Screen
                        } while (canContinueEmployeeLogic == "y" || canContinueEmployeeLogic == "Y");
                        Console.ReadLine();
                        break;
                    // EMPLOEE LOGIC ENDS
                    default:
                        break;
                }


                Console.WriteLine("Enter y or Y to continue");
                chooseBetweenCases = Console.ReadLine();
                Console.Clear(); // Clearing the Screen
            } while (chooseBetweenCases == "y" || chooseBetweenCases == "Y");
            Console.ReadLine();
        }

        static void PrintResults(IEnumerable<Department> depts)
        {
            Console.WriteLine("DeptNo   DeptName    Location    Capacity");
            foreach (var dept in depts)
            {
                Console.WriteLine($"{dept.DeptNo}   {dept.DeptName} {dept.Location} {dept.Capacity}");
            }
        }

        static void CreateDepartment(Department department)
        {
            dsDept.Create(department);
        }

        static void UpdateDepartment(int id, Department department)
        {
            dsDept.Update(id, department);
        }

        static void DeleteDepartment(int id)
        {
            dsDept.Delete(id);
        }



        //*************************************
        static void PrintResultsEmployee(IEnumerable<Employee> emps)
        {
            Console.WriteLine("EmpNo   EmpName    Designation    Salary     DeptNo");
            foreach (var emp in emps)
            {
                Console.WriteLine($"{emp.EmpNo}   {emp.EmpName} {emp.Designation} {emp.Salary} {emp.DeptNo}");
            }
        }

        static void DeleteEmployee(int id)
        {
            dsEmp.Delete(id);
        }

        static void UpdateEmployee(int id, Employee employee)
        {
            dsEmp.Update(id, employee);
        }
    }
}
