<%@ Page Title="" Language="C#" MasterPageFile="~/BiltySystem.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="BiltySystem.Users" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Users</title>
    <style>        
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <!-- START CONTENT -->
    <section id="main-content" class=" ">
        <section class="wrapper main-wrapper row" style=''>
            
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    
                    <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                        <ProgressTemplate>
                            <div class="modalBackground" style="position: fixed; left: 0; width: 100%; height: 100%; z-index: 1;">
                                <img src="assets/images/loader.gif" style="position: fixed; top: 40%; left: 45%; margin-top: -50px; margin-left: -100px;">
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>

                    <div class="clearfix"></div>
                    <!-- MAIN CONTENT AREA STARTS -->
                    <div id="divNotification" style="margin-top: 10px;" runat="server"></div>

                    <asp:Panel ID="pnlView" runat="server" CssClass="col-xs-12" Visible="false">
                        <section class="box ">
                            <header class="panel_header">
                                <%--<h2 class="title pull-left">Vertical Form</h2>--%>
                                <asp:LinkButton ID="lnkCloseView" runat="server" OnClick="lnkCloseView_Click" ToolTip="Click to close view panel" CssClass="box_toggle fa fa-times-circle pull-right" style="margin-top: 10px; margin-right: 10px;"></asp:LinkButton>
                        
                            </header>
                            <div class="content-body">
                                <div class="row">
                                    <div class="col-md-12 col-sm-12 col-xs-12">

                                        <div class="form-group col-md-3 pull-left">
                                            <label>UserName</label>
                                            <asp:Label ID="lblUserName" runat="server"></asp:Label>
                                        </div>
                                        <div class="form-group col-md-3 pull-left">
                                            <label>Password</label>
                                            <asp:Label ID="lblPassword" runat="server"></asp:Label>
                                        </div>
                                        <div class="form-group col-md-3 pull-left">
                                            <label>Group</label>
                                            <asp:Label ID="lblGroup" runat="server"></asp:Label>
                                        </div>
                                        <div class="form-group col-md-3 pull-left">
                                            <label>Company</label>
                                            <asp:Label ID="lblCompany" runat="server"></asp:Label>
                                        </div>
                                        <div class="form-group col-md-3 pull-left">
                                            <label>Department</label>
                                            <asp:Label ID="lblDepartment" runat="server"></asp:Label>
                                        </div>
                                        <div class="form-group col-md-3 pull-left">
                                            <label>Designation</label>
                                            <asp:Label ID="lblDesignation" runat="server"></asp:Label>
                                        </div>
                                        <div class="form-group col-md-3 pull-left">
                                            <label>Role</label>
                                            <asp:Label ID="lblRole" runat="server"></asp:Label>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </section>
                    </asp:Panel>

                    <asp:Panel runat="server" CssClass="col-xs-12" ID="pnlInput" Visible="false">
                        <section class="box ">
                            <asp:LinkButton ID="lnkCloseInput" runat="server" OnClick="lnkCloseInput_Click" CssClass=" fa fa-times-circle pull-right" style="margin-top: 10px; margin-right: 10px;"></asp:LinkButton>
                            <div class="content-body">
                                <div class="row">
                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                        <asp:HiddenField ID="hfEditID" runat="server" />
                                        <asp:HiddenField ID="hfTempPassword" runat="server" />
                                        <div class="form-group col-md-3 pull-left">
                                            <label>UserName</label>
                                            <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-3 pull-left">
                                            <label>Password</label>
                                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-3 pull-left">
                                            <label>Own Group</label>
                                            <asp:DropDownList ID="ddlGroup" runat="server" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged" CssClass="form-control" AutoPostBack="true" ></asp:DropDownList>
                                        </div>
                                        <div class="form-group col-md-3 pull-left">
                                            <label>Own Company</label>
                                            <asp:DropDownList ID="ddlCompany" runat="server" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" CssClass="form-control" AutoPostBack="true" Enabled="false" ></asp:DropDownList>
                                        </div>
                                        <div class="form-group col-md-3 pull-left">
                                            <label>Own Department</label>
                                            <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>
                                        </div>
                                        <div class="form-group col-md-3 pull-left">
                                            <label>Designation</label>
                                            <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                        <%--<div class="form-group col-md-6 pull-left">
                                            <label>Role</label>
                                            <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>--%>
                                        
                                        <div class="form-group col-md-6 pull-left" >
                                            <label>Role</label>
                                            <asp:CheckBoxList ID="cbRoles" runat="server">
                                                <asp:ListItem>Create</asp:ListItem>
                                                <asp:ListItem>Read</asp:ListItem>
                                                <asp:ListItem>Update</asp:ListItem>
                                                <asp:ListItem>Delete</asp:ListItem>
                                                <asp:ListItem>Super Admin</asp:ListItem>
                                            </asp:CheckBoxList>
                                        </div>

                                           <div class="form-group col-md-3 pull-left" style="height:100px;overflow-y:scroll">
                                          

                                            <asp:CheckBoxList ID="cbNavMenu" runat="server"> </asp:CheckBoxList>
                                        </div>

                                        <div class="form-group col-lg-12 pull-left">
                                            <asp:LinkButton ID="lnkSubmit" runat="server" CssClass="btn btn-success pull-right m-l-10" OnClick="lnkSubmit_Click"><i class="fa fa-save"></i> | Save</asp:LinkButton>
                                            <asp:LinkButton ID="lnkDelete" runat="server" CssClass="btn btn-danger pull-right" OnClick="lnkDelete_Click"><i class="fa fa-trash"></i> | Delete</asp:LinkButton>                                        
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </section>
                    </asp:Panel>


                    <div class="col-xs-12">
                        <section class="box ">
                            <header class="panel_header">
                                <h2 class="title pull-left">Users</h2> 
                                <div class="col-md-6 m-t-25 pull-right">
                                    <asp:LinkButton ID="lnkCancelSearch" runat="server" OnClick="lnkCancelSearch_Click"><i class="fa fa-times-circle m-t-10 m-r-5 pull-left" style="color: maroon;"></i></asp:LinkButton>                            
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="pull-left m-r-10 pull-left"></asp:TextBox>
                                    <asp:LinkButton ID="lnkSearch" runat="server" OnClick="lnkSearch_Click" CssClass="pull-left"><i class="fa fa-search"></i></asp:LinkButton>
                                </div>
                                <div class="actions panel_actions pull-right">                           
                                    <asp:LinkButton ID="lnkAddNew" runat="server" CssClass="box_setting fas fa-plus" OnClick="lnkAddNew_Click" ToolTip="Click to Add New"></asp:LinkButton>
                                </div>
                            </header>
                            <div class="content-body">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <asp:GridView ID="gvResult" runat="server" Width="100%" EmptyDataText="No Record found" AutoGenerateColumns="false"
                                            CssClass="table table-hover" Font-Size="12px" Font-Names="Open Sans" DataKeyNames="UserID, Active" BackColor="White" 
                                            OnRowCommand="gvResult_RowCommand" OnRowDataBound="gvResult_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.no">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="UserName" HeaderText="Username"></asp:BoundField>
                                                <asp:BoundField DataField="GroupName" HeaderText="Group"></asp:BoundField>
                                                <asp:BoundField DataField="CompanyName" HeaderText="Company"></asp:BoundField>
                                                <asp:BoundField DataField="DepartName" HeaderText="Department"></asp:BoundField>
                                                <asp:TemplateField HeaderText="Active">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkActive" runat="server" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="Active"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkView" runat="server" CssClass="fa fa-eye" ForeColor="DodgerBlue" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="View"></asp:LinkButton>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" CssClass="fa fa-edit m-l-15" ForeColor="LightSeaGreen" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="Change"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle BackColor="#4b23dd" ForeColor="White" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </section>
                    </div>


                    <ajaxToolkit:ModalPopupExtender ID="modalConfirm" runat="server" PopupControlID="pnlConfirm" DropShadow="True" TargetControlID="btnOpenConfirmModal" 
                        CancelControlID="lnkCloseConfirmModal" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
                    <asp:Panel ID="pnlConfirm" runat="server" CssClass="row" style="background-color: white; padding: 20px; border: 1px solid black;" Width="400px">
                        <asp:Button ID="btnOpenConfirmModal" runat="server" style="display: none" />
                        <asp:LinkButton ID="lnkCloseConfirmModal" runat="server" ForeColor="Maroon" CssClass="pull-right" style="display: none;"><i class="fa fa-times-circle-o"></i></asp:LinkButton>
                        <h4><asp:Label ID="lblModalTitle" runat="server"></asp:Label></h4>                       
                        <div class="col-md-12">
                            <asp:HiddenField ID="hfConfirmAction" runat="server" />
                            <asp:LinkButton ID="lnkConfirm" runat="server" ForeColor="Green" Font-Size="70px" OnClick="lnkConfirm_Click"><i class="fas fa-check pull-left"></i></asp:LinkButton>
                            <asp:LinkButton ID="lnkCancelConfirm" runat="server" ForeColor="Red" Font-Size="70px"><i class="fas fa-times-circle pull-right"></i></asp:LinkButton>
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <!-- MAIN CONTENT AREA ENDS -->
        </section>
    </section>
    <!-- END CONTENT -->
</asp:Content>
