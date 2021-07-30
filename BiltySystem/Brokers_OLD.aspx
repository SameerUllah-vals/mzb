<%@ Page Title="" Language="C#" MasterPageFile="~/BiltySystem.Master" AutoEventWireup="true" CodeBehind="Brokers_OLD.aspx.cs" Inherits="BiltySystem.Brokers_OLD" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Brokers</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <section id="main-content" class=" ">
        <section class="wrapper main-wrapper row" style=''>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>        
                    <div class="page-head" style="padding 10px; height: 66px;">
                        <h2 class="pull-left" style="margin: 0;">Brokers</h2>
                        <div class="col-md-7 pull-right">
                            <div class="col-md-3 pull-right">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-xs btn-info" OnClick="btnSearch_Click" />
                                <asp:Button ID="btnEnableInput" runat="server" Text="Add New" CssClass="btn btn-xs btn-primary m-l-5" OnClick="btnEnableInput_Click" />
                            </div>
                            <div class="col-md-4 pull-right">
                                <asp:LinkButton ID="lnkCancelSearch" runat="server" OnClick="lnkCancelSearch_Click"><i class="fa fa-times-circle m-t-5 m-r-5" style="color: maroon;"></i></asp:LinkButton>
                                <asp:TextBox ID="txtSearch" runat="server" CssClass="col-md-10 pull-right"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="cl-mcont">
                        <div id="divNotification" runat="server"></div>

                        <asp:Panel ID="pnlView" runat="server" CssClass="row" Visible="false">
                            <div class="col-sm-12 col-md-12">
                                <div class="block-flat">
                                    <asp:LinkButton ID="lnkCloseView" runat="server" OnClick="lnkCloseView_Click" ForeColor="Maroon" Font-Size="35px" ToolTip="Click to close view panel" CssClass="pull-right"><i class="fa fa-times-circle"></i></asp:LinkButton>
                                    <div class="content">                                
                                        <div role="form">
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="form-group row">
                                                        <label class="control-label text-right col-md-3">Code:</label>
                                                        <div class="col-md-9">
                                                            <asp:Label ID="lblCodemodal" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <!--/span-->
                                                <div class="col-md-6">
                                                    <div class="form-group row">
                                                        <label class="control-label text-right col-md-3">Name:</label>
                                                        <div class="col-md-9">
                                                            <asp:Label ID="lblNameModal" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <!--/span-->
                                            </div>
                                            <!--/row-->
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="form-group row">
                                                        <label class="control-label text-right col-md-3">Phone:</label>
                                                        <div class="col-md-9">
                                                            <asp:Label ID="lblPhone" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group row">
                                                        <label class="control-label text-right col-md-3">Other Phone:</label>
                                                        <div class="col-md-9">
                                                            <asp:Label ID="lblOther" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!--row-->
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="form-group row">
                                                        <label class="control-label text-right col-md-3">Home No:</label>
                                                        <div class="col-md-9">
                                                            <asp:Label ID="lblHome" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group row">
                                                        <label class="control-label text-right col-md-3">NIC#:</label>
                                                        <div class="col-md-9">
                                                            <asp:Label ID="lblNIC" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!--/row-->
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="form-group row">
                                                        <label class="control-label text-right col-md-3">Address:</label>
                                                        <div class="col-md-9">
                                                            <asp:Label ID="lblAddress" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group row">
                                                        <label class="control-label text-right col-md-3">Description:</label>
                                                        <div class="col-md-9">
                                                            <asp:Label ID="lblDesModal" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            <!--/row-->
                                        </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="pnlInput" runat="server" CssClass="row">
                            <div class="col-sm-12 col-md-12">
                                <div class="block-flat">
                                    <div class="content">

                                        <div role="form">
                                            <div class="row">
                                                <div class="form-group col-md-3 pull-left">
                                                    <label>Code</label>
                                                    <asp:TextBox ID="txtCode" runat="server" CssClass="form-control"></asp:TextBox><asp:HiddenField ID="hfEditID" runat="server" />
                                                </div>
                                                <div class="form-group col-md-3 pull-left">
                                                    <label>Name</label>
                                                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-md-3 pull-left">
                                                    <label>Phone</label>
                                                    <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-md-3 pull-left">
                                                    <label>Secondary Phone</label>
                                                    <asp:TextBox ID="txtPhone2" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-md-3 pull-left">
                                                    <label>Home No</label>
                                                    <asp:TextBox ID="txtHomeNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-md-3 pull-left">
                                                    <label>NIC</label>
                                                    <asp:TextBox ID="txtNIC" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-md- pull-left demo-checkbox">
                                                    <asp:CheckBox ID="cbActive" runat="server" />
                                                    <label for="cbActive">Active</label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group col-md-6 pull-left">
                                                    <label>Address</label>
                                                    <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Width="100%" Rows="3" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-md-6 pull-left">
                                                    <label>Description</label>
                                                    <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Width="100%" Rows="3" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-lg-12 pull-left">
                                                    <asp:Button ID="btnCloseInput" runat="server" Text="Close" CssClass="btn btn-dark pull-right m-l-5" OnClick="btnCloseInput_Click" />
                                                    <%--<asp:Button ID="btnCloseInput" runat="server" Text="Close" CssClass="btn btn-dark pull-right m-l-5" OnClick="btnCloseInput_Click" />--%>
                                                    <asp:Button ID="btnDeleteBroker" runat="server" Text="Delete" CssClass="btn btn-danger pull-right m-l-5" Visible="false" OnClick="btnDeleteBroker_Click" />
                                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-success pull-right" OnClick="btnSubmit_Click" />
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="Panel1" runat="server" CssClass="row">
                            <div class="col-sm-12 col-md-12">
                                <div class="block-flat">
                                    <div class="content">
                                        <asp:GridView ID="gvResult" runat="server" Width="100%" EmptyDataText="No brokers found" AutoGenerateColumns="false"
                                            OnRowCommand="gvResult_RowCommand" CssClass="table table-hover" Font-Size="10px" DataKeyNames="ID">
                                            <Columns>
                                                <asp:ButtonField Text="View" CommandName="View"></asp:ButtonField>
                                                <asp:BoundField DataField="Code" HeaderText="Code"></asp:BoundField>
                                                <asp:BoundField DataField="Name" HeaderText="Name"></asp:BoundField>
                                                <asp:BoundField DataField="Phone" HeaderText="Phone"></asp:BoundField>
                                                <asp:BoundField DataField="Address" HeaderText="Address"></asp:BoundField>
                                                <asp:BoundField DataField="NIC" HeaderText="NIC"></asp:BoundField>
                                                <asp:BoundField DataField="Description" HeaderText="Description"></asp:BoundField>
                                                <asp:BoundField DataField="isActive" HeaderText="Active"></asp:BoundField>
                                                <asp:ButtonField Text="Edit" CommandName="Change"></asp:ButtonField>
                                            </Columns>
                                            <HeaderStyle BackColor="Navy" ForeColor="White" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </section>
    </section>
</asp:Content>
