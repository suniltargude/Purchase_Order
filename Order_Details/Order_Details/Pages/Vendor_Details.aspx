<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Vendor_Details.aspx.cs" Inherits="Order_Details.Pages.Vendor_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="main_outer">
        <div class="container">
            <div class="row">
                <div class="col-lg-8 offset-2">
                    <div class="purchase_wrap">
                        <div class="row">
                            <div class="col-lg-6">
                                <h5 class="form_tit">Vendor Entry</h5>
                            </div>
                            <div class="col-lg-6">
                                <div class="btn_wrap">
                                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click" />
                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-secondary" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="false"  />

                                </div>
                            </div>
                            <div class="col-lg-12">
                                <div class="btn_wrap">
                                <asp:Button ID="btnMaterials" runat="server" Text="Go to Materials" OnClick="btnMaterials_Click" CausesValidation="false" />
                                <asp:Button ID="Button1" runat="server" Text="Go to Purchase Orders" OnClick="btnPurchase_Click" CausesValidation="false"  />
                            </div>
                            </div>
                            <div class="col-lg-6 mrbot10">
                                <div class="row">
                                    <label class="col-lg-6">Code</label>
                                    <div class="col-lg-6">
                                        <asp:TextBox ID="txtVendorCode" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvVendorCode" runat="server" ControlToValidate="txtVendorCode"
                                            ErrorMessage="Vendor Code is required!" ForeColor="Red" Display="Dynamic" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-lg-12 mrbot10">
                                <div class="row">
                                    <label class="col-lg-3">Vendor</label>
                                    <div class="col-lg-9">
                                        <asp:TextBox ID="txtVendorName" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvVendorName" runat="server" ControlToValidate="txtVendorName"
                                            ErrorMessage="Vendor Name is required!" ForeColor="Red" Display="Dynamic" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-12 mrbot10">
                                <div class="row">
                                    <label class="col-lg-3">Address1</label>
                                    <div class="col-lg-9">
                                        <asp:TextBox ID="txtAddress1" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvAddress1" runat="server" ControlToValidate="txtAddress1"
                                            ErrorMessage="Address1 is required!" ForeColor="Red" Display="Dynamic" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-12 mrbot10">
                                <div class="row">
                                    <label class="col-lg-3">Address2</label>
                                    <div class="col-lg-9">
                                        <asp:TextBox ID="txtAddress2" runat="server" CssClass="form-control"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-12 mrbot10">
                                <div class="row">
                                    <label class="col-lg-3">Contact Email</label>
                                    <div class="col-lg-9">
                                        <asp:TextBox ID="txtContactEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtContactEmail"
                                            ErrorMessage="Email is required!" ForeColor="Red" Display="Dynamic" />
                                        <asp:RegularExpressionValidator
                                            ID="revEmail"
                                            runat="server"
                                            ControlToValidate="txtContactEmail"
                                            ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"
                                            ErrorMessage="Invalid email format!"
                                            ForeColor="Red"
                                            Display="Dynamic" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-12 mrbot10">
                                <div class="row">
                                    <label class="col-lg-3">Contact No</label>
                                    <div class="col-lg-9">
                                        <asp:TextBox ID="txtContactNo" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RegularExpressionValidator
                                            ID="revPhoneNumber"
                                            runat="server"
                                            ControlToValidate="txtContactNo"
                                            ValidationExpression="^\d{10}$"
                                            ErrorMessage="Phone number must be 10 digits!"
                                            ForeColor="Red"
                                            Display="Dynamic" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-lg-6">
                                <div class="row">
                                    <label class="col-lg-6">Valid Till Date</label>
                                    <div class="col-lg-6">
                                        <asp:TextBox ID="txtValidTillDate" runat="server" CssClass="form-control" TextMode="DateTimeLocal"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-lg-6">
                                <div class="row">
                                    <label class="col-lg-5">Is Active:</label>
                                    <div class="col-lg-7">
                                        <asp:CheckBox ID="chkIsActive" runat="server" />
                                    </div>
                                </div>
                            </div>

                            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                            <h3>Vendor List</h3>
                            <div class="col-lg-12">
                                <asp:GridView ID="gvVendors" runat="server" AutoGenerateColumns="False" CssClass="table"
                                    DataKeyNames="VendorID" >

                                    <Columns>
                                        <%--<asp:BoundField DataField="VendorID" HeaderText="ID" HeaderStyle-Width="5%" ReadOnly="True" />--%>
                                        <asp:BoundField DataField="Code" HeaderText="Code" HeaderStyle-Width="10%" />
                                        <asp:BoundField DataField="Name" HeaderText="Name" HeaderStyle-Width="10%" />
                                        <asp:BoundField DataField="Address1" HeaderText="Address1" HeaderStyle-Width="10%" />
                                        <asp:BoundField DataField="Address2" HeaderText="Address2" HeaderStyle-Width="10%" />
                                        <asp:BoundField DataField="ContactEmail" HeaderText="Email" HeaderStyle-Width="10%" />
                                        <asp:BoundField DataField="ContactNo" HeaderText="Contact No" HeaderStyle-Width="10%" />
                                        <asp:BoundField DataField="ValidTillDate" HeaderText="Valid Till" HeaderStyle-Width="10%" DataFormatString="{0:dd.MM.yyyy}" />
                                        <asp:CheckBoxField DataField="IsActive" HeaderText="Active" HeaderStyle-Width="10%" />
                                      
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
