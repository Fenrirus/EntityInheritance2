using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace EntityInheritance2
{
    public partial class WebForm1 : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private DataTable Dane(List<Employees7> employees)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("FirstName");
            dt.Columns.Add("LastName");
            dt.Columns.Add("Gender");
            dt.Columns.Add("AnuualSalary");
            dt.Columns.Add("HourlyPay");
            dt.Columns.Add("HoursWorked");
            dt.Columns.Add("Type");
            foreach(Employees7 employee in employees)
            {
                DataRow dr = dt.NewRow();
                dr["ID"] = employee.EmployeeID;
                dr["FirstName"] = employee.FirstName;
                dr["LastName"] = employee.LastName;
                dr["Gender"] = employee.Gender;

                if (employee is PermanentEmployees7)
                {
                    dr["AnuualSalary"] = ((PermanentEmployees7)employee).AnnualSalary;
                    dr["Type"] = "Permanent";
                }
                else
                {
                    dr["HourlyPay"] = ((ContractEmployees7)employee).HourlyPay;
                    dr["HoursWorked"] = ((ContractEmployees7)employee).HoursWorked;
                    dr["Type"] = "Contract";
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            EmployeeDbcontext db = new EmployeeDbcontext();
            switch (RadioButtonList1.SelectedValue)
            {
                case "Permament":
                    {
                        GridView1.DataSource = db.Employees7.OfType<PermanentEmployees7>().ToList();
                        GridView1.DataBind();
                        break;
                    }
                case "Contract":
                    {
                        GridView1.DataSource = db.Employees7.OfType<ContractEmployees7>().ToList();
                        GridView1.DataBind();
                        break;
                    }
                default:
                    {
                        GridView1.DataSource = Dane(db.Employees7.ToList());
                        GridView1.DataBind();
                        break;
                    }
            }
        }
    }
}