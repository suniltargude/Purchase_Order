using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Order_Details.DAL;

namespace Order_Details.Pages
{
    public partial class Purchase_Order_Entry : System.Web.UI.Page
    {
        bool IsPageRefresh;
        string connectionString = ConfigurationManager.ConnectionStrings["Order_Details"].ConnectionString;

        DataTable dtOrderLines = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["postGuids"] = System.Guid.NewGuid().ToString();
                Session["postGuid"] = ViewState["postGuids"].ToString();
                LoadVendors();
                LoadMaterials();
                InitializeGrid();
            }
            else
            {
                if (ViewState["postGuids"].ToString() != Session["postGuid"].ToString())
                {
                    IsPageRefresh = true;
                }
               
            }

        }

        private void LoadVendors()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = clsMain.VendorName();
                ddlVendor.DataSource = dt;
                ddlVendor.DataTextField = "Name";
                ddlVendor.DataValueField = "VendorID";
                ddlVendor.DataBind();
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void LoadMaterials()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = clsMain.Materialsdata();
                // Bind Material Code Dropdown
                ddlMaterialCode.DataSource = dt;
                ddlMaterialCode.DataTextField = "Code";
                ddlMaterialCode.DataValueField = "MaterialID";
                ddlMaterialCode.DataBind();

                // Bind Short Text Dropdown
                ddlShortText.DataSource = dt;
                ddlShortText.DataTextField = "ShortText";
                ddlShortText.DataValueField = "MaterialID";
                ddlShortText.DataBind();
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void InitializeGrid()
        {
            dtOrderLines.Columns.Add("LineID", typeof(int));  // Ensure LineID is included
            dtOrderLines.Columns.Add("Code");
            dtOrderLines.Columns.Add("Quantity");
            dtOrderLines.Columns.Add("Rate");
            dtOrderLines.Columns.Add("Amount");
            dtOrderLines.Columns.Add("ExpectedDate");
            ViewState["OrderLines"] = dtOrderLines;
        }

        protected void ddlMaterialCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT ShortText, UOM FROM Materials WHERE MaterialID = @MaterialID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaterialID", ddlMaterialCode.SelectedValue);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            ddlShortText.SelectedValue = ddlMaterialCode.SelectedValue;
                            txtUOM.Text = reader["UOM"].ToString();
                        }
                    }
                }
            }
        }

        protected void ddlShortText_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlMaterialCode.SelectedValue = ddlShortText.SelectedValue;
            ddlMaterialCode_SelectedIndexChanged(sender, e); // Update UOM when Short Text is changed
        }

        protected void btnAddLine_Click(object sender, EventArgs e)
        {
            dtOrderLines = (DataTable)ViewState["OrderLines"];
            DataRow dr = dtOrderLines.NewRow();
            dr["LineID"] = dtOrderLines.Rows.Count + 1;  // Assigning a unique LineID
            dr["Code"] = ddlMaterialCode.SelectedItem.Text;
            dr["Quantity"] = txtQuantity.Text;
            dr["Rate"] = txtRate.Text;
            dr["Amount"] = Convert.ToInt32(txtQuantity.Text) * Convert.ToDecimal(txtRate.Text);
            dr["ExpectedDate"] = txtExpectedDate.Text;
            dtOrderLines.Rows.Add(dr);
            ViewState["OrderLines"] = dtOrderLines;
            gvOrderLines.DataSource = dtOrderLines;
            gvOrderLines.DataBind();
            UpdateOrderValue();
            
        }

        private void UpdateOrderValue()
        {
            decimal total = 0;
            foreach (DataRow row in dtOrderLines.Rows)
            {
                total += Convert.ToDecimal(row["Amount"]);
            }
            txtOrderValue.Text = total.ToString("0.00");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) // Ensure all required fields are filled
            {
                lblMessage.Text = "Please fill in all required fields!";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
           
            DataTable dt = new DataTable();
            if (!IsPageRefresh)
                try
                {
                    dt = clsMain.SavePurchaseData(txtOrderNo.Text, ddlVendor.SelectedValue, txtOrderDate.Text, txtNotes.Text, txtOrderValue.Text);
                    int orderID = Convert.ToInt32(dt.Rows[0]["OrderID"]);


                    // Insert Purchase Order Lines
                    DataTable dtOrderLines = (DataTable)ViewState["OrderLines"];
                    foreach (DataRow row in dtOrderLines.Rows)
                    {

                        dtOrderLines = clsMain.Save_Purchase(orderID, ddlMaterialCode.SelectedValue, Convert.ToInt32(row["Quantity"]), Convert.ToInt32(row["Rate"]), Convert.ToDateTime(row["ExpectedDate"]).ToString());

                    }
                   // ClearFields();
                    lblMessage.Text = "Purchase Order saved successfully!";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    

                }
                catch (Exception)
                {

                    throw;
                }
        }

        protected void btnUpdateLine_Click(object sender, EventArgs e)
        {

            DataTable dtOrderLines = (DataTable)ViewState["OrderLines"];
            foreach (GridViewRow row in gvOrderLines.Rows)
            {
                CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                if (chkSelect.Checked)
                {
                    int rowIndex = row.RowIndex;
                    dtOrderLines.Rows[rowIndex]["Quantity"] = txtQuantity.Text;
                    dtOrderLines.Rows[rowIndex]["Rate"] = txtRate.Text;
                    dtOrderLines.Rows[rowIndex]["Amount"] = txtAmount.Text;
                    dtOrderLines.Rows[rowIndex]["ExpectedDate"] = txtExpectedDate.Text;

                }
            }
            ViewState["OrderLines"] = dtOrderLines;
            gvOrderLines.DataSource = dtOrderLines;
            gvOrderLines.DataBind();
            UpdateOrderValue();

        }

        protected void btnRemoveLine_Click(object sender, EventArgs e)
        {
            DataTable dtOrderLines = (DataTable)ViewState["OrderLines"];
            for (int i = gvOrderLines.Rows.Count - 1; i >= 0; i--)
            {
                GridViewRow row = gvOrderLines.Rows[i];
                CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                if (chkSelect.Checked)
                {
                    dtOrderLines.Rows.RemoveAt(i);
                }
            }
            ViewState["OrderLines"] = dtOrderLines;
            gvOrderLines.DataSource = dtOrderLines;
            gvOrderLines.DataBind();
            UpdateOrderValue();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearFields();  // Reset form fields
            Response.Redirect(Request.RawUrl);  // Reload the page to discard unsaved data
        }

        private void ClearFields()
        {
            txtOrderNo.Text = "";
            txtOrderDate.Text = "";
            ddlVendor.SelectedIndex = 0;
            txtNotes.Text = "";
            txtOrderValue.Text = "";
            txtQuantity.Text = "";
            txtRate.Text = "";
            txtAmount.Text = "";
            txtExpectedDate.Text = "";
        }

        protected void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            CalculateAmount();
        }

        protected void txtRate_TextChanged(object sender, EventArgs e)
        {
            CalculateAmount();
        }

        private void CalculateAmount()
        {
            if (!string.IsNullOrEmpty(txtQuantity.Text) && !string.IsNullOrEmpty(txtRate.Text))
            {
                decimal quantity = Convert.ToDecimal(txtQuantity.Text);
                decimal rate = Convert.ToDecimal(txtRate.Text);
                txtAmount.Text = (quantity * rate).ToString("0.00");
                // txtOrderValue.Text = (quantity * rate).ToString("0.00");
                UpdateOrderValue();
            }

        }

        protected void btnVendor_Click(object sender, EventArgs e)
        {
            Response.Redirect("Vendor_Details.aspx");
        }

        protected void btnMaterials_Click(object sender, EventArgs e)
        {
            Response.Redirect("Material_Entry.aspx");
        }
    }
}
