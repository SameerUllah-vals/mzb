<%@ Page Title="" Language="C#" MasterPageFile="~/BiltySystem.Master" AutoEventWireup="true" CodeBehind="WebForm3.aspx.cs" Inherits="BiltySystem.WebForm3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <section id="main-content" class=" ">
        <section class="wrapper main-wrapper row" style=''>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>                
                    <div class='col-xs-12'>
                        <div class="page-title">

                            <div class="pull-left">
                                <!-- PAGE HEADING TAG - START -->
                                <h1 class="title">Brokers</h1>
                                <!-- PAGE HEADING TAG - END -->
                            </div>

                            <div class="pull-right hidden-xs">
                                <asp:LinkButton ID="lnkAddNew" runat="server" CssClass="btn btn-xs btn-info" OnClick="lnkAddNew_Click"><i class="fa fa-plus"></i> | Add New</asp:LinkButton>
                            </div>

                        </div>
                    </div>

                    <div class="clearfix"></div>

                    <asp:Panel ID="pnlInput" runat="server" CssClass="col-xs-12" Visible="false">
                        <section class="box ">
                            <header class="panel_header">
                                <%--<h2 class="title pull-left">Basic Elements</h2>--%>
                                <div class="actions panel_actions pull-right">
                                    <a class="box_toggle fa fa-chevron-down"></a>
                                    <a class="box_setting fa fa-cog" data-toggle="modal" href="#section-settings"></a>
                                    <a class="box_close fa fa-times"></a>
                                </div>
                            </header>
                            <div class="content-body">
                                <div class="row">
                                    <div class="col-md-3 col-sm-4 col-xs-5">
                                        <div class="form-group">
                                            <label class="form-label" for="field-1">Code</label>                                            
                                            <div class="controls">
                                                <asp:TextBox ID="txtCode" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3 col-sm-4 col-xs-5">
                                        <div class="form-group">
                                            <label class="form-label" for="field-1">Name</label>                                            
                                            <div class="controls">
                                                <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3 col-sm-4 col-xs-5">
                                        <div class="form-group">
                                            <label class="form-label" for="field-1">Phone</label>                                            
                                            <div class="controls">
                                                <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3 col-sm-4 col-xs-5">
                                        <div class="form-group">
                                            <label class="form-label" for="field-1">Phone 2</label>                                            
                                            <div class="controls">
                                                <asp:TextBox ID="txtPhone2" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3 col-sm-4 col-xs-5">
                                        <div class="form-group">
                                            <label class="form-label" for="field-1">NIC</label>                                            
                                            <div class="controls">
                                                <asp:TextBox ID="txtNIC" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3 col-sm-4 col-xs-5">
                                        <div class="form-group">
                                            <label class="form-label" for="field-1">Home No.</label>                                            
                                            <div class="controls">
                                                <asp:TextBox ID="txtHomeNo" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-sm-7 col-xs-8">
                                        <div class="form-group">
                                            <label class="form-label" for="field-1">Address</label>                                            
                                            <div class="controls">
                                                <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-sm-7 col-xs-8">
                                        <div class="form-group">
                                            <label class="form-label" for="field-1">Description</label>                                            
                                            <div class="controls">
                                                <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                            </div>
                        </section>
                    </asp:Panel>
                    <!-- MAIN CONTENT AREA ENDS -->
                </ContentTemplate>
            </asp:UpdatePanel>
        </section>
    </section>
</asp:Content>
