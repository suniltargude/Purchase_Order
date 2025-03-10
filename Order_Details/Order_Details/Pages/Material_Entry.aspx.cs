using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Order_Details.DAL;

namespace Order_Details.Pages
{
	
    public partial class Material_Entry : System.Web.UI.Page
    {
        // Retrieve connection string from Web.config
        bool IsPageRefresh;
        string connectionString = ConfigurationManager.ConnectionStrings["Order_Details"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["postGuids"] = System.Guid.NewGuid().ToString();
                Session["postGuid"] = ViewState["postGuids"].ToString();
                LoadMaterials();
            }
            else
            {
                if (ViewState["postGuids"].ToString() != Session["postGuid"].ToString())
                {
                    IsPageRefresh = true;
                }
                Session["postGuid"] = System.Guid.NewGuid().ToString();
                ViewState["postGuids"] = Session["postGuid"].ToString();
            }
           
        }

        private void LoadMaterials()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = clsMain.LoadMaterialData();
                gvMaterials.DataSource = dt;
                gvMaterials.DataBind();
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) // Ensure all required fields are filled
            {
                lblMessage.Text = "Please fill in all required fields correctly!";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            clsMain.Save_Material(txtMaterialCode.Text, txtShortText.Text, txtLongText.Text, txtReorderLevel.Text, txtMinOrderQty.Text, txtUOM.Text, chkIsActive.Checked);
           
            ClearFields();
            LoadMaterials();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtMaterialCode.Text = "";
            txtShortText.Text = "";
            txtLongText.Text = "";
            txtReorderLevel.Text = "";
            txtMinOrderQty.Text = "";
            txtUOM.Text = "";
            chkIsActive.Checked = false;
        }

        private void ClearFields()
        {
            txtMaterialCode.Text = "";
            txtShortText.Text = "";
            txtLongText.Text = "";
            txtReorderLevel.Text = "";
            txtMinOrderQty.Text = "";
            txtUOM.Text = "";
            chkIsActive.Checked = false;
        }

        protected void gvMaterials_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvMaterials.EditIndex = e.NewEditIndex;
            LoadMaterials();
        }

        // Cancel Editing Mode
        protected void gvMaterials_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvMaterials.EditIndex = -1;
            LoadMaterials();
        }

        // Updating Material Details
        protected void gvMaterials_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvMaterials.Rows[e.RowIndex];
            int materialId = Convert.ToInt32(gvMaterials.DataKeys[e.RowIndex].Value);
            string code = (row.Cells[1].Controls[0] as TextBox).Text.Trim();
            string shortText = (row.Cells[2].Controls[0] as TextBox).Text.Trim();
            string longText = (row.Cells[3].Controls[0] as TextBox).Text.Trim();
            int reorderLevel = Convert.ToInt32((row.Cells[4].Controls[0] as TextBox).Text.Trim());
            int minOrderQuantity = Convert.ToInt32((row.Cells[5].Controls[0] as TextBox).Text.Trim());
            string uom = (row.Cells[6].Controls[0] as TextBox).Text.Trim();
            bool isActive = (row.Cells[7].Controls[0] as CheckBox).Checked;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"UPDATE Materials 
                             SET Code=@Code, ShortText=@ShortText, LongText=@LongText, ReorderLevel=@ReorderLevel, 
                                 MinOrderQuantity=@MinOrderQuantity, UOM=@UOM, IsActive=@IsActive 
                             WHERE MaterialID=@MaterialID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaterialID", materialId);
                    cmd.Parameters.AddWithValue("@Code", code);
                    cmd.Parameters.AddWithValue("@ShortText", shortText);
                    cmd.Parameters.AddWithValue("@LongText", longText);
                    cmd.Parameters.AddWithValue("@ReorderLevel", reorderLevel);
                    cmd.Parameters.AddWithValue("@MinOrderQuantity", minOrderQuantity);
                    cmd.Parameters.AddWithValue("@UOM", uom);
                    cmd.Parameters.AddWithValue("@IsActive", isActive);
                    cmd.ExecuteNonQuery();
                }
            }

            gvMaterials.EditIndex = -1;
            LoadMaterials();
        }

        // Deleting a Material
        protected void gvMaterials_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int materialId = Convert.ToInt32(gvMaterials.DataKeys[e.RowIndex].Value);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Materials WHERE MaterialID=@MaterialID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaterialID", materialId);
                    cmd.ExecuteNonQuery();
                }
            }

            LoadMaterials();
        }

        protected void btnVendor_Click(object sender, EventArgs e)
        {
            Response.Redirect("Vendor_Details.aspx");
        }

        protected void btnPurchase_Click(object sender, EventArgs e)
        {
            Response.Redirect("Purchase_Order_Entry.aspx");
        }


    }
}