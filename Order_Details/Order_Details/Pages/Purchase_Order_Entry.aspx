<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Purchase_Order_Entry.aspx.cs" Inherits="Order_Details.Pages.Purchase_Order_Entry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="main_outer">
        <div class="container">
            <div class="row">
                <div class="col-lg-6 offset-3">
                    <div class="purchase_wrap">
                        <div class="row">
                            <div class="col-lg-6">
                                <h5 class="form_tit">Purchase Order Entry</h5>
                            </div>
                            <div class="col-lg-6">
                                <div class="btn_wrap">
                                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_Click" />
                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-secondary" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="false" />

                                </div>
                            </div>
                            <div class="col-lg-12">
                                <div class="btn_wrap">
                                    <asp:Button ID="btnVendor" runat="server" Text="Go to Vendors" OnClick="btnVendor_Click" CausesValidation="false" />
                                    <asp:Button ID="btnMaterials" runat="server" Text="Go to Materials" OnClick="btnMaterials_Click" CausesValidation="false" />
                                </div>
                            </div>
                            <div class="col-lg-6">
                                <div class="row">
                                    <label class="col-lg-5">Order No</label>
                                    <div class="col-lg-7">
                                        <asp:TextBox ID="txtOrderNo" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvOrderNo" runat="server" ControlToValidate="txtOrderNo"
                                            ErrorMessage="Order No is required!" ForeColor="Red" Display="Dynamic" />

                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-6">
                                <div class="row">
                                    <label class="col-lg-5">Order Date</label>
                                    <div class="col-lg-7">
                                        <asp:TextBox ID="txtOrderDate" runat="server" CssClass="form-control" TextMode="DateTimeLocal"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvOrderDate" runat="server" ControlToValidate="txtOrderDate"
                                            ErrorMessage="Order Date is required!" ForeColor="Red" Display="Dynamic" />

                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-12 cust_drop">
                                <div class="row">
                                    <label class="col-lg-2">Vendor</label>
                                    <div class="col-lg-10">
                                        <asp:DropDownList ID="ddlVendor" runat="server" CssClass="form-select">
                                            <asp:ListItem Text="-- Select Vendor --" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvVendor" runat="server" ControlToValidate="ddlVendor"
                                            InitialValue="" ErrorMessage="Vendor selection is required!" ForeColor="Red" Display="Dynamic" />

                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-12 cust_textarea">
                                <div class="row">
                                    <label class="col-lg-2">Notes</label>
                                    <div class="col-lg-10">
                                        <asp:TextBox ID="txtNotes" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvNotes" runat="server" ControlToValidate="txtNotes"
                                            ErrorMessage="Notes are required!" ForeColor="Red" Display="Dynamic" />

                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-6 mrbot10">
                                <div class="row">
                                    <label class="col-lg-5">Order Value</label>
                                    <div class="col-lg-7">
                                        <asp:TextBox ID="txtOrderValue" runat="server" CssClass="form-control" ReadOnly="true" Style="background-color: lightgray; color: black;"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group cust_material mrbot10">
                                <div class="row gx-0">
                                    <label class="col-lg-3 mrtop20">Material:</label>
                                    <div class="col-lg-3 mrleft20">
                                        <label>Code</label>
                                        <asp:DropDownList ID="ddlMaterialCode" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMaterialCode_SelectedIndexChanged" CssClass="form-select">
                                            <asp:ListItem Text="-- Select Material --" Value="" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvMaterialCode" runat="server" ControlToValidate="ddlMaterialCode"
                                            InitialValue="" ErrorMessage="Material selection is required!" ForeColor="Red" Display="Dynamic" />
                                    </div>
                                    <div class="col-lg-3">
                                        <label>Short Text</label>
                                        <asp:DropDownList ID="ddlShortText" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlShortText_SelectedIndexChanged" CssClass="form-select"></asp:DropDownList>
                                    </div>
                                    <div class="col-lg-3">
                                        <label>UOM</label>
                                        <asp:TextBox ID="txtUOM" runat="server" ReadOnly="true" CssClass="form-control" Style="background-color: lightgray; color: black;"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="col-lg-6 mrbot10">
                                <div class="row">
                                    <label class="col-lg-5">Quantity</label>
                                    <div class="col-lg-7">
                                        <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control" AutoPostBack="true" TextMode="Number" OnTextChanged="txtQuantity_TextChanged"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvQuantity" runat="server" ControlToValidate="txtQuantity"
                                            ErrorMessage="Quantity is required!" ForeColor="Red" Display="Dynamic" />

                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-6 mrbot10">
                                <div class="row">
                                    <label class="col-lg-5">Rate</label>
                                    <div class="col-lg-7">
                                        <asp:TextBox ID="txtRate" runat="server" CssClass="form-control" AutoPostBack="true" TextMode="Number" OnTextChanged="txtRate_TextChanged"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvRate" runat="server" ControlToValidate="txtRate"
                                            ErrorMessage="Rate is required!" ForeColor="Red" Display="Dynamic" />

                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-6 mrbot10">
                                <div class="row">
                                    <label class="col-lg-5">Amount</label>
                                    <div class="col-lg-7">
                                        <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" ReadOnly="true" Style="background-color: lightgray; color: black;"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-6 mrbot10">
                                <div class="row">
                                    <label class="col-lg-5">Expected Date</label>
                                    <div class="col-lg-7">
                                        <asp:TextBox ID="txtExpectedDate" runat="server" CssClass="form-control" TextMode="DateTimeLocal"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvExpectedDate" runat="server" ControlToValidate="txtExpectedDate"
                                            ErrorMessage="Expected Date is required!" ForeColor="Red" Display="Dynamic" />

                                    </div>
                                </div>
                            </div>

                            <div class="col-lg-12 mrbot10">
                                <div class="button-group">
                                    <div class="row">
                                        <div class="col-lg-4">
                                            <asp:Button ID="btnAddLine" runat="server" Text="Add Line" OnClick="btnAddLine_Click" CausesValidation="false" />
                                        </div>
                                        <div class="col-lg-4">
                                            <asp:Button ID="btnUpdateLine" runat="server" Text="Update Line" OnClick="btnUpdateLine_Click" />
                                        </div>
                                        <div class="col-lg-4">
                                            <asp:Button ID="btnRemoveLine" runat="server" Text="Remove Line" OnClick="btnRemoveLine_Click" CausesValidation="false" />
                                        </div>

                                    </div>
                                </div>
                            </div>

                            <div class="scrollable-grid">
                            <asp:GridView ID="gvOrderLines" runat="server" AutoGenerateColumns="False" CssClass="table"
                                DataKeyNames="LineID" >

                                <Columns>
                                    <asp:TemplateField HeaderText="Select">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="LineID" HeaderText="Line ID" ReadOnly="True" />
                                    <asp:BoundField DataField="Code" HeaderText="Code" />
                                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                                    <asp:BoundField DataField="Rate" HeaderText="Rate" />
                                    <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                    <asp:BoundField DataField="ExpectedDate" HeaderText="Exp Date" DataFormatString="{0:dd.MM.yyyy}" />
                                </Columns>
                            </asp:GridView>
                                </div>

                            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
