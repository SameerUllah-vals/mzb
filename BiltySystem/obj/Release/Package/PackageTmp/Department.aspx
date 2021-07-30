<%@ Page Title="" Language="C#" MasterPageFile="~/BiltySystem.Master" AutoEventWireup="true" CodeBehind="Department.aspx.cs" Inherits="BiltySystem.Department" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Department</title>
    <style>        
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ClientIDMode="Static">
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
                    <div id="myModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                        <div class="modal-dialog modal-lg" style="max-width: 600px; width: 50%;">
                            <div class="modal-content">
                                <div class="modal-body" style="height: 200px; overflow-y: scroll;">
                                    <div class="card card-outline-info">
                                        <div class="card-header">
                                        </div>
                                        <div class="card-body">

                                            <asp:Label Font-Size="X-Large" ID="lblMessage" runat="server"></asp:Label>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-6">
                                               <%-- <asp:Button ID="btnModal" runat="server" CssClass="btn btn-info" OnClick="btnModal_Click" />
                                                <asp:Button ID="btnCancelModel" Text="Cancel" runat="server" CssClass="btn btn-danger" />--%>
                                            </div>

                                        </div>

                                    </div>
                                </div>
                            </div>
                            <!-- /.modal-content -->
                        </div>
                        <!-- /.modal-dialog -->
                    </div>


                    <asp:Panel ID="pnlView" runat="server" CssClass="col-xs-12" Visible="false">
                        <section class="box ">
                            <header class="panel_header">
                                <%--<h2 class="title pull-left">Vertical Form</h2>--%>
                                <asp:LinkButton ID="lnkCloseView" runat="server" OnClick="lnkCloseView_Click" ToolTip="Click to close view panel" CssClass="box_toggle fa fa-times-circle pull-right" style="margin-top: 10px; margin-right: 10px;"></asp:LinkButton>
                        
                            </header>
                            <div class="content-body">
                                <div class="row">
                                    <div class="col-md-8 col-sm-9 col-xs-10">

                                    <div role="form">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group row">
                                                <label class="control-label text-right col-md-3">Code:</label>
                                                <div class="col-md-9">
                                                    <asp:Label ID="txtCodeModal" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <!--/span-->
                                        <div class="col-md-6">
                                            <div class="form-group row">
                                                <label class="control-label text-right col-md-3">Name:</label>
                                                <div class="col-md-9">
                                                    <asp:Label ID="txtNameModal" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <!--/span-->
                                    </div>
                                    <!--/row-->
                                    <div class="row">


                                        <div class="col-md-6">
                                            <div class="form-group row">
                                                <label class="control-label text-right col-md-3">Email:</label>
                                                <div class="col-md-9">
                                                    <asp:Label ID="txtEmailModal" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>




                                        <div class="col-md-6">
                                            <div class="form-group row">
                                                <label class="control-label text-right col-md-3">group:</label>
                                                <div class="col-md-9">
                                                    <asp:Label ID="txtGroupModal" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>




                                    </div>
                                    <!--row-->


                                    <div class="row">


                                        <div class="col-md-6">
                                            <div class="form-group row">
                                                <label class="control-label text-right col-md-3">Company:</label>
                                                <div class="col-md-9">
                                                    <asp:Label ID="txtCompanyModal" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>




                                        <div class="col-md-6">
                                            <div class="form-group row">
                                                <label class="control-label text-right col-md-3">Website:</label>
                                                <div class="col-md-9">
                                                    <asp:Label ID="txtWebsiteModal" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>




                                    </div>
                                    <!--/row-->


                                    <div class="row">


                                        <div class="col-md-6">
                                            <div class="form-group row">
                                                <label class="control-label text-right col-md-3">Contact:</label>
                                                <div class="col-md-9">
                                                    <asp:Label ID="txtContactModal" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>




                                        <div class="col-md-6">
                                            <div class="form-group row">
                                                <label class="control-label text-right col-md-3">Other Contact:</label>
                                                <div class="col-md-9">
                                                    <asp:Label ID="txtOtherContactModal" runat="server"></asp:Label>
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
                                                    <asp:Label ID="txtAddressModal" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>




                                        <div class="col-md-6">
                                            <div class="form-group row">
                                                <label class="control-label text-right col-md-3">Description:</label>
                                                <div class="col-md-9">
                                                    <asp:Label ID="txtDescriptionModal" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>




                                    </div>
                                    <!--/row-->
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

                                        <div class="form-group col-md-3 pull-left">
                                            <label>Code</label>
                                            <asp:TextBox ID="txtCode" runat="server" CssClass="form-control"></asp:TextBox><asp:HiddenField ID="hfEditID" runat="server" />
                                        </div>
                                        <div class="form-group col-md-3 pull-left">
                                            <label>Name</label>
                                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-3 pull-left">
                                            <label>Email</label>
                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-3 pull-left">
                                            <label>Group</label>
                                            <asp:DropDownList ID="ddlgroup" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>


                                        <div class="form-group col-md-3 pull-left">
                                            <label>Company</label>
                                            <asp:DropDownList ID="ddlcompany" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                        <div class="form-group col-md-3 pull-left">
                                            <label>Website</label>
                                            <asp:TextBox ID="txtWebsite" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-md-3 pull-left">
                                            <label>Contact</label>
                                            <asp:TextBox ID="txtContact" runat="server" onkeydown="return (!(event.keyCode>=65) && event.keyCode!=32);" CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-md-3 pull-left">
                                            <label>Other Contact</label>
                                            <asp:TextBox ID="txtOtherContact" runat="server" onkeydown="return (!(event.keyCode>=65) && event.keyCode!=32);" CssClass="form-control"></asp:TextBox>
                                        </div>

                               
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-md-6 pull-left">
                                            <label>Address</label>
                                            <asp:TextBox ID="Address" runat="server" TextMode="MultiLine" Width="100%" Rows="3" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-6 pull-left">
                                            <label>Description</label>
                                            <asp:TextBox ID="Description" runat="server" TextMode="MultiLine" Width="100%" Rows="3" CssClass="form-control"></asp:TextBox>
                                        </div>


                                            <div class="form-group col-lg-12 pull-left">
                                                <%--<div class="col-md- pull-left demo-checkbox">
                                                    <asp:CheckBox ID="cbActive" runat="server" />
                                                    <label for="cbActive">Active</label>
                                                </div>--%>
                                        
                                                <asp:LinkButton ID="lnkSubmit" runat="server" CssClass="btn btn-success pull-right m-l-10" OnClick="lnkSubmit_Click"><i class="fa fa-save"></i> | Save</asp:LinkButton>
                                        
                                                <asp:LinkButton ID="lnkDelete" runat="server" CssClass="btn btn-danger pull-right" OnClick="lnkDelete_Click"><i class="fa fa-trash"></i> | Delete</asp:LinkButton>
                                        
                                            </div>

                                            <%--<div class="form-group">
                                                <button type="button" class="btn btn-primary ">Sign in</button>
                                                <button type="button" class="btn btn-purple  pull-right">Register now</button>
                                            </div>--%>

                                        <%--</div>--%>

                                    </div>
                                </div>
                            </div>
                        </section>
                    </asp:Panel>


                    <div class="col-xs-12">
                        <section class="box ">
                            <header class="panel_header">
                                <h2 class="title pull-left">Department</h2> 
                                <div class="col-md-6 m-t-25 pull-right">
                                    <asp:LinkButton ID="lnkCancelSearch" runat="server" OnClick="lnkCancelSearch_Click"><i class="fa fa-times-circle m-t-10 m-r-5 pull-left" style="color: maroon;"></i></asp:LinkButton>
                            
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="pull-left m-r-10 pull-left"></asp:TextBox>
                                    <asp:LinkButton ID="lnkSearch" runat="server" OnClick="lnkSearch_Click" CssClass="pull-left"><i class="fa fa-search"></i></asp:LinkButton>
                                </div>
                                <div class="actions panel_actions pull-right">
                           
                                    <asp:LinkButton ID="lnkAddNew" runat="server" CssClass="box_setting fas fa-plus" OnClick="lnkAddNew_Click" ToolTip="Click to Add New"></asp:LinkButton>
                                    <%--<a class="box_setting fa fa-cog" data-toggle="modal" href="#section-settings"></a>
                                    <a class="box_close fa fa-times"></a>--%>
                                </div>
                            </header>
                            <div class="content-body">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <asp:GridView ID="gvResult" runat="server" Width="100%" EmptyDataText="No Record found" AutoGenerateColumns="false"
                                            CssClass="table table-hover" Font-Size="12px" Font-Names="Open Sans" DataKeyNames="DepartID, isActive" BackColor="White" OnRowCommand="gvResult_RowCommand" OnRowDataBound="gvResult_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.no">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="DepartCode" HeaderText="Code"></asp:BoundField>
                                        <asp:BoundField DataField="DepartName" HeaderText="Name"></asp:BoundField>
                                        <asp:BoundField DataField="Groups" HeaderText="Group"></asp:BoundField>
                                        <asp:BoundField DataField="CompanyName" HeaderText="Company"></asp:BoundField>
                                        <asp:BoundField DataField="WebAdd" HeaderText="Website"></asp:BoundField>
                                        <asp:BoundField DataField="Contact" HeaderText="ContactNo"></asp:BoundField>
                                                <%--<asp:BoundField DataField="Status" HeaderText="Active"></asp:BoundField>--%>
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
