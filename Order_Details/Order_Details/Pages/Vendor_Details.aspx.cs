using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Order_Details.DAL;

namespace Order_Details.Pages
{


    public partial class Vendor_Details : System.Web.UI.Page
    {
        // Flag to check if the page was refreshed
        bool IsPageRefresh;

        // Retrieve database connection string from Web.config
        string connectionString = ConfigurationManager.ConnectionStrings["Order_Details"].ConnectionString;

        /// <summary>
        /// Page Load Event - Handles first-time load and postbacks
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) // Executes only on the first page load
            {
                // Generate a unique identifier to track form submissions
                string newGuid = Guid.NewGuid().ToString();
                ViewState["postGuid"] = newGuid;
                Session["postGuid"] = newGuid;

                // Load vendors into the GridView
                LoadVendors();
            }
            else // Executes on postbacks (form submissions, button clicks, etc.)
            {
                // Check if the ViewState GUID matches the session GUID to detect a refresh
                IsPageRefresh = ViewState["postGuid"]?.ToString() != Session["postGuid"]?.ToString();

                // Update GUIDs to track the latest valid form submission
                string newGuid = Guid.NewGuid().ToString();
                Session["postGuid"] = newGuid;
                ViewState["postGuid"] = newGuid;
            }
        }

        /// <summary>
        /// Loads vendor data into the GridView from the database
        /// </summary>
        private void LoadVendors()
        {
            DataTable dt = new DataTable();
            try
            {
                // Fetch vendor data from the database
                dt = clsMain.LoadData();

                // Bind the data to GridView
                gvVendors.DataSource = dt;
                gvVendors.DataBind();
            }
            catch (Exception ex)
            {
                // Log the exception (if logging is implemented)
                throw new Exception("Error loading vendors: " + ex.Message);
            }
        }

        /// <summary>
        /// Save Vendor Details to the Database
        /// </summary>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            // Validate required fields before saving
            if (!Page.IsValid)
            {
                lblMessage.Text = "Please fill in all required fields!";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            // Prevent duplicate form submissions due to page refresh
            if (!IsPageRefresh)
            {
                try
                {
                    // Save vendor details into the database
                    clsMain.Save(txtVendorCode.Text, txtVendorName.Text, txtAddress1.Text, txtAddress2.Text,
                                 txtContactEmail.Text, txtContactNo.Text, txtValidTillDate.Text, chkIsActive.Checked);

                    // Show success message
                    lblMessage.Text = "Vendor saved successfully!";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                }
                catch (Exception ex)
                {
                    // Log the exception and display error message
                    lblMessage.Text = "Error: " + ex.Message;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }

            // Clear the form fields after saving
            ClearFields();

            // Reload the vendor list to show the new data
            LoadVendors();
        }

        /// <summary>
        /// Clear all input fields and reset the form
        /// </summary>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        /// <summary>
        /// Helper method to clear all form fields
        /// </summary>
        private void ClearFields()
        {
            txtVendorCode.Text = "";
            txtVendorName.Text = "";
            txtAddress1.Text = "";
            txtAddress2.Text = "";
            txtContactEmail.Text = "";
            txtContactNo.Text = "";
            txtValidTillDate.Text = "";
            chkIsActive.Checked = false;
        }

        /// <summary>
        /// Navigate to Material Entry Page
        /// </summary>
        protected void btnMaterials_Click(object sender, EventArgs e)
        {
            Response.Redirect("Material_Entry.aspx");
        }

        /// <summary>
        /// Navigate to Purchase Order Entry Page
        /// </summary>
        protected void btnPurchase_Click(object sender, EventArgs e)
        {
            Response.Redirect("Purchase_Order_Entry.aspx");
        }
    }

}