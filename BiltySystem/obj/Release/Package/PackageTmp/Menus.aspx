<%@ Page Title="" Language="C#" MasterPageFile="~/BiltySystem.Master" AutoEventWireup="true" CodeBehind="Menus.aspx.cs" Inherits="BiltySystem.Menus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Menus</title>
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
                                <asp:LinkButton ID="lnkCloseView" runat="server" OnClick="lnkCloseView_Click" ToolTip="Click to close view panel" CssClass="box_toggle fa fa-times-circle pull-right" style="margin-top: 10px; margin-right: 10px;"></asp:LinkButton>                        
                            </header>
                            <div class="content-body">
                                <div class="row">
                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                        <div class="form-group col-md-6 pull-left">
                                            <label>Name</label>
                                            <asp:Label ID="lblMenuName" runat="server" CssClass="form-control"></asp:Label>
                                        </div>
                                        <div class="form-group col-md-6 pull-left">
                                            <label>Icon</label>
                                            <asp:Label ID="lblIcon" runat="server" CssClass="form-control"></asp:Label>
                                        </div>
                                        <div class="form-group col-md-12 pull-left">
                                            <label>Description</label>
                                            <asp:Label ID="lblMenuDescription" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:Label>
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
                                        <div class="form-group col-md-6 pull-left">
                                            <label>Name</label>
                                            <asp:TextBox ID="txtMenuName" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-6 pull-left">
                                            <label>Icon</label>
                                            <asp:TextBox ID="txtIcon" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-12 pull-left">
                                            <label>Description</label>
                                            <asp:TextBox ID="txtMenuDescription" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
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
                                <h2 class="title pull-left">Menus</h2> 
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
                                            CssClass="table table-hover" Font-Size="12px" Font-Names="Open Sans" DataKeyNames="MenuID, Active" BackColor="White" OnRowCommand="gvResult_RowCommand" OnRowDataBound="gvResult_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.no">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="MenuName" HeaderText="Menu"></asp:BoundField>
                                                <asp:BoundField DataField="Icon" HeaderText="Icon / Logo"></asp:BoundField>
                                                <asp:BoundField DataField="Description" HeaderText="Description"></asp:BoundField>
                                                <asp:BoundField DataField="CreatedBy" HeaderText="Created By"></asp:BoundField>
                                                <asp:BoundField DataField="DateCreated" HeaderText="Created On"></asp:BoundField>
                                                <asp:BoundField DataField="ModifiedBy" HeaderText="Modified By"></asp:BoundField>
                                                <asp:BoundField DataField="DateModified" HeaderText="Modified On"></asp:BoundField>
                                                <asp:TemplateField HeaderText="Sub Menues">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkSubMenus" runat="server" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="SubMenu" ForeColor="DodgerBlue" CssClass="fas fa-plus-square"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
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
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <tr>  
                                                            <td colspan="11" style="background-color: lightgray">  
                                                                <div class="col-lg-12">
                                                                    <asp:LinkButton ID="lnkAddNewSubMenu" runat="server" CssClass="btn btn-xs btn-info m-b-10" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="AddNewSubMenu" Visible="false"><i class="fa fa-plus-square"></i> | Add New </asp:LinkButton>
                                                                </div>
                                                                <asp:GridView ID="gvSubMenus" CssClass="table table-bordered" runat="server" AutoGenerateColumns="false" Width="100%" OnRowCommand="gvSubMenus_RowCommand">  
                                                                    <Columns>  
                                                                         <asp:TemplateField HeaderText="S.no">
                                                                            <ItemTemplate>
                                                                                <%#Container.DataItemIndex + 1 %>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="FormName" HeaderText="Menu" />
                                                                        <asp:BoundField DataField="Url" HeaderText="Path / Link" />
                                                                        <asp:BoundField DataField="formTarget" HeaderText="Target" />
                                                                        <asp:TemplateField>                                                                            
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkEdit" runat="server" CssClass="fa fa-edit m-l-15" ForeColor="LightSeaGreen" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") + "," + Eval("FormID") %>' CommandName="ChangeSub"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>  
                                                                </asp:GridView>
                                                            </td>  
                                                        </tr>  
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

                    <ajaxToolkit:ModalPopupExtender ID="modalSubMenu" runat="server" PopupControlID="pnlSubMenu" DropShadow="True" TargetControlID="btnOpenSubMenu" 
                        CancelControlID="lnkCloseSubMenu" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
                    <asp:Panel ID="pnlSubMenu" runat="server" CssClass="row" style="background-color: white; padding: 20px; border: 1px solid black;" Width="800px">
                        <asp:Button ID="btnOpenSubMenu" runat="server" style="display: none" />
                        <asp:LinkButton ID="lnkCloseSubMenu" runat="server" ForeColor="Maroon" CssClass="pull-right" style="display: none;"><i class="fa fa-times-circle-o"></i></asp:LinkButton>
                        <h4><asp:Label ID="Label1" runat="server"></asp:Label></h4>                       
                        <div class="col-md-12">
                            <section class="box ">
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="lnkCloseInput_Click" CssClass=" fa fa-times-circle pull-right" style="margin-top: 10px; margin-right: 10px;"></asp:LinkButton>
                            <div class="content-body">
                                <div class="row">
                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                        <asp:HiddenField ID="hfSubMenuEditID" runat="server" />
                                        <div class="form-group col-md-4 pull-left">
                                            <label>Menu</label>
                                            <asp:TextBox ID="txtSubMenuName" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-4 pull-left">
                                            <label>Link/Path</label>
                                            <asp:TextBox ID="txtSubMenuLink" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-4 pull-left">
                                            <label>Target</label>
                                            <asp:DropDownList ID="ddlTarget" runat="server" CssClass="form-control"><asp:ListItem>_Blank</asp:ListItem><asp:ListItem>_self</asp:ListItem></asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-12 pull-left">
                                            <asp:LinkButton ID="lnkSaveSubMenu" runat="server" CssClass="btn btn-success pull-right m-l-10" OnClick="lnkSaveSubMenu_Click"><i class="fa fa-save"></i> | Save</asp:LinkButton>                                        
                                            <asp:LinkButton ID="lnkDeleteSubMenu" runat="server" CssClass="btn btn-danger pull-right" Visible="false" OnClick="lnkDeleteSubMenu_Click"><i class="fa fa-trash"></i> | Delete</asp:LinkButton>                                        
                                        </div>
                                        <div id="divSubMenuNotification" style="margin-top: 10px;" runat="server"></div>
                                    </div>
                                </div>
                            </div>
                        </section>
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <!-- MAIN CONTENT AREA ENDS -->
        </section>
    </section>
    <!-- END CONTENT -->
</asp:Content>
