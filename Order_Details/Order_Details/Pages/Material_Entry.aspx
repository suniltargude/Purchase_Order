<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Material_Entry.aspx.cs" Inherits="Order_Details.Pages.Material_Entry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="main_outer">
        <div class="container">
            <div class="row">
                <div class="col-lg-8 offset-2">
                    <div class="purchase_wrap">
                        <div class="row">
                            <div class="col-lg-6">
                                <h5 class="form_tit">Material Entry</h5>
                            </div>
                            <div class="col-lg-6">
                                <div class="btn_wrap">
                                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click" />
                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-secondary" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="false" />

                                </div>
                            </div>
                            <div class="col-lg-12">
                                <div class="btn_wrap">
                                    <asp:Button ID="btnVendor" runat="server"  Text="Go to Vendors" OnClick="btnVendor_Click" CausesValidation="false" />
                                    <asp:Button ID="btnPurchase" runat="server"  Text="Go to Purchase Orders" OnClick="btnPurchase_Click" CausesValidation="false" />

                                </div>
                            </div>

                            <div class="col-lg-6 mrbot10">
                                <div class="row">
                                    <label class="col-lg-6">Code</label>
                                    <div class="col-lg-6">
                                        <asp:TextBox ID="txtMaterialCode" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvMaterialCode" runat="server" ControlToValidate="txtMaterialCode"
                                            ErrorMessage="Material Code is required!" ForeColor="Red" Display="Dynamic" />
                                    </div>
                                </div>
                            </div>


                            <div class="col-lg-12 mrbot10">
                                <div class="row">
                                    <label class="col-lg-3">Short Text</label>
                                    <div class="col-lg-9">
                                        <asp:TextBox ID="txtShortText" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvShortText" runat="server" ControlToValidate="txtShortText"
                                            ErrorMessage="Short Text is required!" ForeColor="Red" Display="Dynamic" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-12 mrbot10">
                                <div class="row">
                                    <label class="col-lg-3">Long Text</label>
                                    <div class="col-lg-9">
                                        <asp:TextBox ID="txtLongText" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvLongText" runat="server" ControlToValidate="txtLongText"
                                            ErrorMessage="Long Text is required!" ForeColor="Red" Display="Dynamic" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-12 mrbot10">
                                <div class="row">
                                    <label class="col-lg-3">Reorder Level</label>
                                    <div class="col-lg-9">
                                        <asp:TextBox ID="txtReorderLevel" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvReorderLevel" runat="server" ControlToValidate="txtReorderLevel"
                                            ErrorMessage="Reorder Level is required!" ForeColor="Red" Display="Dynamic" />
                                        <asp:RangeValidator ID="rvReorderLevel" runat="server" ControlToValidate="txtReorderLevel"
                                            MinimumValue="1" MaximumValue="9999" Type="Integer"
                                            ErrorMessage="Reorder Level must be between 1 and 9999!" ForeColor="Red" Display="Dynamic" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-12 mrbot10">
                                <div class="row">
                                    <label class="col-lg-3">Min Order Quantity</label>
                                    <div class="col-lg-9">
                                        <asp:TextBox ID="txtMinOrderQty" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvMinOrderQty" runat="server" ControlToValidate="txtMinOrderQty"
                                            ErrorMessage="Minimum Order Quantity is required!" ForeColor="Red" Display="Dynamic" />
                                        <asp:RangeValidator ID="rvMinOrderQty" runat="server" ControlToValidate="txtMinOrderQty"
                                            MinimumValue="1" MaximumValue="9999" Type="Integer"
                                            ErrorMessage="Min Order Quantity must be between 1 and 9999!" ForeColor="Red" Display="Dynamic" />

                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-12 mrbot10">
                                <div class="row">
                                    <label class="col-lg-3">UOM (Unit of Measure)</label>
                                    <div class="col-lg-9">
                                        <asp:TextBox ID="txtUOM" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvUOM" runat="server" ControlToValidate="txtUOM"
                                            ErrorMessage="Unit of Measure (UOM) is required!" ForeColor="Red" Display="Dynamic" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-lg-6">
                                <div class="row">
                                    <label class="col-lg-5">Is Active</label>
                                    <div class="col-lg-7">
                                        <asp:CheckBox ID="chkIsActive" runat="server" />
                                    </div>
                                </div>
                            </div>


                            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>

                            <h3>Material List</h3>
                            <asp:GridView ID="gvMaterials" runat="server" AutoGenerateColumns="False" CssClass="table"
                                DataKeyNames="MaterialID"
                                OnRowEditing="gvMaterials_RowEditing"
                                OnRowCancelingEdit="gvMaterials_RowCancelingEdit"
                                OnRowUpdating="gvMaterials_RowUpdating"
                                OnRowDeleting="gvMaterials_RowDeleting">

                                <Columns>
                                    <asp:BoundField DataField="MaterialID" HeaderText="ID" ReadOnly="True" />
                                    <asp:BoundField DataField="Code" HeaderText="Code" />
                                    <asp:BoundField DataField="ShortText" HeaderText="Short Text" />
                                    <asp:BoundField DataField="LongText" HeaderText="Long Text" />
                                    <asp:BoundField DataField="ReorderLevel" HeaderText="Reorder Level" />
                                    <asp:BoundField DataField="MinOrderQuantity" HeaderText="Min Order Qty" />
                                    <asp:BoundField DataField="UOM" HeaderText="UOM" />
                                    <asp:CheckBoxField DataField="IsActive" HeaderText="Active" />
                                    <%-- <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />--%>
                                </Columns>
                            </asp:GridView>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
