using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Order_Details.DAL
{
    public class clsMain
    {
        static ErrorLog oErrorLog = new ErrorLog();


        public static void Save(string Code, string Name, string Address1, string Address2, string ContactEmail, string ContactNo, string ValidTillDate, bool IsActive)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@Code", Code);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Address1", Address1);
                cmd.Parameters.AddWithValue("@Address2", Address2);
                cmd.Parameters.AddWithValue("@ContactEmail", ContactEmail);
                cmd.Parameters.AddWithValue("@ContactNo", ContactNo);
                cmd.Parameters.AddWithValue("@ValidTillDate", Convert.ToDateTime(ValidTillDate));
                cmd.Parameters.AddWithValue("@IsActive", IsActive);
                clsDataAccess.ExecuteNonQuery("insert_Vendor_Details", cmd);
            }
            catch (Exception ex)
            {

                oErrorLog.WriteErrorLog("Error in insert_Vendor_Details :" + ex.ToString());
            }


        }

        public static DataTable LoadData()
        {
            DataTable ds = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();
                ds = clsDataAccess.GetDataTable("Load_Vendor_Data", cmd);


            }
            catch (Exception ex)
            {
                oErrorLog.WriteErrorLog("Error in Load_Vendor_Data :" + ex.ToString());
            }
            return ds;
        }

        public static void Save_Material(string Code, string ShortText, string LongText, string ReorderLevel, string MinOrderQuantity, string UOM, bool IsActive)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@Code", Code);
                cmd.Parameters.AddWithValue("@ShortText", ShortText);
                cmd.Parameters.AddWithValue("@LongText", LongText);
                cmd.Parameters.AddWithValue("@ReorderLevel", Convert.ToInt32(ReorderLevel));
                cmd.Parameters.AddWithValue("@MinOrderQuantity", Convert.ToInt32(MinOrderQuantity));
                cmd.Parameters.AddWithValue("@UOM", UOM);
                cmd.Parameters.AddWithValue("@IsActive", IsActive);

                clsDataAccess.ExecuteNonQuery("Insert_Material_Data", cmd);
            }
            catch (Exception ex)
            {

                oErrorLog.WriteErrorLog("Error in Insert_Material_Data :" + ex.ToString());
            }

        }

        public static DataTable LoadMaterialData()
        {
            DataTable ds = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();
                ds = clsDataAccess.GetDataTable("Load_Material_Data", cmd);


            }
            catch (Exception ex)
            {
                oErrorLog.WriteErrorLog("Error in Load_Material_Data :" + ex.ToString());
            }
            return ds;
        }

        public static DataTable VendorName()
        {
            DataTable ds = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();
                ds = clsDataAccess.GetDataTable("Load_Vendorlist_Data", cmd);
            }
            catch (Exception ex)
            {
                oErrorLog.WriteErrorLog("Error in Load_Vendorlist_Data :" + ex.ToString());
            }
            return ds;
        }

        public static DataTable Materialsdata()
        {
            DataTable ds = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();
                ds = clsDataAccess.GetDataTable("Load_Materiallist_Data", cmd);
            }
            catch (Exception ex)
            {
                oErrorLog.WriteErrorLog("Error in Load_Materiallist_Data :" + ex.ToString());
            }
            return ds;
        }

        public static DataTable SavePurchaseData(string txtOrderNo, string ddlVendor, string txtOrderDate, string txtNotes, string txtOrderValue)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@OrderNo", txtOrderNo);
                cmd.Parameters.AddWithValue("@VendorID", ddlVendor);
                cmd.Parameters.AddWithValue("@OrderDate", Convert.ToDateTime(txtOrderDate));
                cmd.Parameters.AddWithValue("@Notes", txtNotes);
                cmd.Parameters.AddWithValue("@OrderValue", txtOrderValue);
                dt=clsDataAccess.GetDataTable("insert_Purchase_Details", cmd);
            }
            catch (Exception ex)
            {
                oErrorLog.WriteErrorLog("Error in insert_Purchase_Details :" + ex.ToString());
            }
            return dt;

        }

        public static DataTable Save_Purchase(int orderID, string ddlMaterialCode, int  Quantity, int Rate, string ExpectedDate)
            {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@OrderID", orderID);
                cmd.Parameters.AddWithValue("@MaterialID", ddlMaterialCode);
                cmd.Parameters.AddWithValue("@Quantity", Quantity);
                cmd.Parameters.AddWithValue("@Rate", Rate);
                cmd.Parameters.AddWithValue("@ExpectedDate", Convert.ToDateTime(ExpectedDate));
                clsDataAccess.GetDataTable("insert_Purchaseorder_Details", cmd);
            }
            catch (Exception ex)
            {
                oErrorLog.WriteErrorLog("Error in insert_Purchaseorder_Details :" + ex.ToString());
            }
            return dt;

        }

    }
}