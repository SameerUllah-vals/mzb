<%@ Page Title="" Language="C#" MasterPageFile="~/BiltySystem.Master" AutoEventWireup="true" CodeBehind="TripManagement.aspx.cs" Inherits="BiltySystem.TripManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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


                    <asp:Panel ID="pnlView" runat="server" CssClass="row" Visible="false">
                    <div class="col-sm-12 col-md-12">
                        <div class="block-flat">
                            <asp:LinkButton ID="lnkCloseView" runat="server" OnClick="lnkCloseView_Click" ForeColor="Maroon" Font-Size="35px" ToolTip="Click to close view panel" CssClass="pull-right"><i class="fa fa-times-circle"></i></asp:LinkButton>
                            <div class="content">                                
                                <div role="form">
                                    <div class="row">
                                        <div class="col-md-5">
                                            <div class="form-group row">
                                                <label class="control-label text-right col-md-3">Customer:</label>
                                                <div class="col-md-9">
                                                    <asp:Label ID="lblCustomer" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <!--/span-->
                                        <div class="col-md-4">
                                            <div class="form-group row">
                                                <label class="control-label text-right col-md-3">Trip Start:</label>
                                                <div class="col-md-9">
                                                    <asp:Label ID="lblTripStart" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <!--/span-->
                                    </div>
                                    <!--/row-->
                                    <div class="row">
                                        <div class="col-md-5">
                                            <div class="form-group row">
                                                <label class="control-label text-right col-md-3">TripEnd:</label>
                                                <div class="col-md-9">
                                                    <asp:Label ID="lblTripEnd" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group row">
                                                <label class="control-label text-right col-md-3">Pick:</label>
                                                <div class="col-md-9">
                                                    <asp:Label ID="lblPick" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!--row-->

                                    <div class="row">
                                        <div class="col-md-5">
                                            <div class="form-group row">
                                                <label class="control-label text-right col-md-3">Drop:</label>
                                                <div class="col-md-9">
                                                    <asp:Label ID="lblDrop" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group row">
                                                <label class="control-label text-right col-md-3">Vehicle Reg#:</label>
                                                <div class="col-md-9">
                                                    <asp:Label ID="lblVehicle" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!--/row-->

                                    <div class="row">
                                        <div class="col-md-5">
                                            <div class="form-group row">
                                                <label class="control-label text-right col-md-3">Freight:</label>
                                                <div class="col-md-9">
                                                    <asp:Label ID="lblFreight" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                       
                                    </div>
                                    <!--/row-->

                                    
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>

                    <asp:Panel runat="server" CssClass="col-xs-12" ID="pnlInput" Visible="false">
                        <section class="box ">
                            <asp:LinkButton ID="lnkCloseInput" runat="server" OnClick="lnkCloseInput_Click" CssClass=" fa fa-times-circle pull-right" style="margin-top: 10px; margin-right: 10px;"></asp:LinkButton>
                            <div class="content-body">
                                <div class="row">
                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                        <asp:HiddenField ID="hfEditID" runat="server" />
                                        <%--<div role="form">--%>
                                           <div class="form-group col-md-3 pull-left">
                                            <label>Customer</label>
                                           <asp:DropDownList ID="ddlCustomer" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                        <div class="form-group col-md-3 pull-left">
                                            <label>Vehicle Reg#</label>
                                            <%--<asp:TextBox ID="txtVehicle" runat="server" CssClass="form-control" ></asp:TextBox>--%>
                                            <asp:DropDownList ID="ddlVehicle" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                        <div class="form-group col-md-3 pull-left">
                                            <label>Trip Start</label>
                                            <asp:TextBox ID="txtTripStart" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-3 pull-left">
                                            <label>Trip End</label>
                                            <asp:TextBox ID="txtTripEnd" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        
                                        <div class="form-group col-md-3 pull-left">
                                            <label>Pick</label>
                                            <asp:TextBox ID="txtPick" runat="server" Rows="1" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-3 pull-left">
                                            <label>Drop</label>
                                            <asp:TextBox ID="txtDrop" runat="server" Rows="1" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                         <div class="form-group col-md-3 pull-left">
                                            <label>Freight</label>
                                            <asp:TextBox ID="txtFreight" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                        </div>
                                        
                                       
                                       
                                    </div>
                                    <div class="row">
                                       <%-- <div class="form-group col-md-6 pull-left">
                                            <label>Address</label>
                                            <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Width="100%" Rows="3" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-6 pull-left">
                                            <label>Description</label>
                                            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Width="100%" Rows="3" CssClass="form-control"></asp:TextBox>
                                        </div>--%>




                                            <div class="form-group col-lg-12 pull-left">
                                                <%--<div class="col-md- pull-left demo-checkbox">
                                                    <asp:CheckBox ID="cbActive" runat="server" />
                                                    <label for="cbActive">Active</label>
                                                </div>--%>
                                        
                                                <asp:LinkButton ID="lnkSubmit" runat="server" CssClass="btn btn-success pull-right m-l-10" OnClick="lnkSubmit_Click"><i class="fa fa-save"></i> | Save</asp:LinkButton>
                                        
                                                <asp:LinkButton ID="lnkDelete" Visible="false" runat="server" CssClass="btn btn-danger pull-right" OnClick="lnkDelete_Click"><i class="fa fa-trash"></i> | Delete</asp:LinkButton>
                                        
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

                    <asp:Panel runat="server" CssClass="col-xs-12" ID="pnlSearch" Visible="false">
                        <section class="box ">
                            <asp:LinkButton ID="lnkCloseSearch" runat="server" OnClick="lnkCloseSearch_Click" CssClass=" fa fa-times-circle pull-right" style="margin-top: 10px; margin-right: 10px;"></asp:LinkButton>
                            <div class="content-body">
                                <div class="row">
                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                        <asp:HiddenField ID="HiddenField1" runat="server" />
                                        <%--<div role="form">--%>
                                         <div class="form-group col-md-3 pull-left">
                                            <label>Trip Start</label>
                                            <asp:TextBox ID="txtStartSearch" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-3 pull-left">
                                            <label>Trip End</label>
                                            <asp:TextBox ID="txtEndSearch" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                                        </div>
                                           <div class="form-group col-md-3 pull-left">
                                            <label>Customer</label>
                                           <asp:DropDownList ID="ddlCustomerSearch" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                        <div class="form-group col-md-3 pull-left">
                                            <label>Vehicle Reg#</label>
                                            <%--<asp:TextBox ID="txtVehicle" runat="server" CssClass="form-control" ></asp:TextBox>--%>
                                            <asp:DropDownList ID="ddlVehicleSearch" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                       
                                        
                                        <div class="form-group col-md-3 pull-left">
                                            <label>Pick</label>
                                            <asp:TextBox ID="txtPickSearch" runat="server" Rows="1" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-3 pull-left">
                                            <label>Drop</label>
                                            <asp:TextBox ID="txtDropSearch" runat="server" Rows="1" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                        
                                        
                                       
                                       
                                    </div>
                                    <div class="row">
                                       




                                            <div class="form-group col-lg-12 pull-left">
                                               
                                        
                                                <asp:LinkButton ID="lnkTripSearch" runat="server" CssClass="btn btn-success pull-right m-l-10" OnClick="lnkTripSearch_Click"><i class="fa fa-search"></i> | Search</asp:LinkButton>
                                        
                                               
                                        
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
                                <h2 class="title pull-left">Trip Management</h2> 
                                <div class="col-md-6 m-t-25 pull-right">
                                   <%-- <asp:LinkButton ID="lnkCancelSearch" runat="server" OnClick="lnkCancelSearch_Click"><i class="fa fa-times-circle m-t-10 m-r-5 pull-left" style="color: maroon;"></i></asp:LinkButton>
                            
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="pull-left m-r-10 pull-left"></asp:TextBox>
                                    <asp:LinkButton ID="lnkSearch" runat="server" OnClick="lnkSearch_Click" CssClass="pull-left"><i class="fa fa-search"></i></asp:LinkButton>
                                </div>--%>
                                <div class="actions panel_actions pull-right">
                                    <asp:LinkButton ID="lnkSearchNew" runat="server" CssClass="box_setting fas fa-search" OnClick="lnkSearchNew_Click" ToolTip="Click to Search"></asp:LinkButton>
                           
                                    <asp:LinkButton ID="lnkAddNew" runat="server" CssClass="box_setting fas fa-plus" OnClick="lnkAddNew_Click" ToolTip="Click to Add New"></asp:LinkButton>
                                    <%--<a class="box_setting fa fa-cog" data-toggle="modal" href="#section-settings"></a>
                                    <a class="box_close fa fa-times"></a>--%>
                                </div>
                            </header>
                            <div class="content-body">
                                <div class="row">
                                    <div class="col-xs-12">
                                            

                                        <asp:GridView ID="gvResult" runat="server" Width="100%" EmptyDataText="No Record found" AutoGenerateColumns="false"
                                            CssClass="table table-hover" Font-Size="12px" Font-Names="Open Sans" DataKeyNames="ID,IsActive" BackColor="White" 
                                            OnRowCommand="gvResult_RowCommand" OnRowDataBound="gvResult_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.no">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                             <asp:BoundField DataField="ID" HeaderText="Trip ID"></asp:BoundField>
                                        <asp:BoundField DataField="TripStart" HeaderText="Trip Start Date"></asp:BoundField>
                                        <asp:BoundField DataField="TripEnd" HeaderText="Trip End Date"></asp:BoundField>
                                        <asp:BoundField DataField="CompanyName" HeaderText="Customer"></asp:BoundField>
                                        <asp:BoundField DataField="RegNo" HeaderText="Vehicle Reg#"></asp:BoundField>
                                        <asp:BoundField DataField="Pickup" HeaderText="Pick City"></asp:BoundField>
                                        <asp:BoundField DataField="Dropoff" HeaderText="Drop City"></asp:BoundField>
                                        <asp:BoundField DataField="Freight" HeaderText="Total Freight"></asp:BoundField>
                                                <asp:TemplateField HeaderText="Total Trip Expenses" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkExpenses" runat="server" CommandName="TripExpenses" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'><i class="fas fa-search-plus"></i></asp:LinkButton> | <asp:Label ID="lblExpenses" runat="server" Text='<%# Eval("Expenses") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                        <%--<asp:BoundField DataField="Expenses" HeaderText="Total Trip Expenses"></asp:BoundField>--%>
                                        <asp:BoundField DataField="PnL" HeaderText="Trip Profit/Loss"></asp:BoundField>
                                                <%--<asp:BoundField DataField="Status" HeaderText="Active"></asp:BoundField>--%>
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkActive" runat="server" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="IsActive"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkView" runat="server" CssClass="fa fa-eye" ForeColor="DodgerBlue" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="View"></asp:LinkButton>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" CssClass="fa fa-edit m-l-15" ForeColor="LightSeaGreen" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="Change"></asp:LinkButton>
                                                        <%--<asp:LinkButton ID="lnkEdit" runat="server" CssClass="fa fa-edit m-l-15" ForeColor="LightSeaGreen" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="Change"></asp:LinkButton>--%>
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

            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <ajaxToolkit:ModalPopupExtender ID="modalExpenses" runat="server" PopupControlID="pnlExpenses" DropShadow="True" TargetControlID="btnExpenses" 
                            CancelControlID="lnkCloseExpenses" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlExpenses" runat="server" CssClass="row" style="background-color: white; padding: 20px; border: 1px solid black;" Width="800px">
                            
                                    <asp:Button ID="btnExpenses" runat="server" style="display: none" />
                                    <%--<asp:LinkButton ID="lnkCloseExpenses" runat="server" ForeColor="Maroon" CssClass="pull-right" style="display: none;"><i class="fa fa-times-circle-o"></i></asp:LinkButton>--%>
                                    <h4 class="pull-left"><asp:Label ID="Label15" runat="server"></asp:Label></h4> 
                                    <asp:LinkButton ID="lnkCloseExpenses" runat="server" ForeColor="Maroon" CssClass="pull-right" OnClick="lnkCloseExpenses_Click"><i class="fas fa-times-circle"></i></asp:LinkButton>
                                    <div class="col-md-12" style="overflow-y: scroll; overflow-x: scroll; max-height: 400px;">
                                    <div class="row">
                                        <asp:Panel ID="pnlExpensesInput" runat="server" class="col-md-12" Visible="true">
                                           
                                            <div class="col-md-6 pull-left" style="border-left: 1px solid black;">
                                                <div class="col-md-6">
                                                    <label>Expenses Type</label>
                                                    <asp:DropDownList ID="ddlExpensesType" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                                <div class="col-md-6" id="ExpensesAmountPlaceholder" runat="server">
                                                    <label>Amount</label>
                                                    <asp:TextBox ID="txtAmount" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-md-6"  runat="server">
                                                    <label>Remarks</label>
                                                    <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                
                                            </div>
                                            
                                            <div class="col-xs-12">
                                                <div class="form-group">
                                                    <div class="controls">
                                                        <asp:LinkButton ID="lnkCancel" runat="server" CssClass="btn btn-danger pull-right m-b-10" OnClick="lnkCancel_Click"><i class="fas fa-times"></i> | Cancel</asp:LinkButton>
                                                        <asp:LinkButton ID="lnkSave" runat="server" CssClass="btn btn-primary pull-right m-b-10 m-r-10" OnClick="lnkSave_Click"><i class="fas fa-save"></i> | Save</asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                        
                                    <div class="row">
                                        <div class="col-md-12">
                                            <%--<asp:LinkButton ID="lnkAddExpenses" runat="server" CssClass="btn btn-info pull-right" OnClick="lnkAddExpenses_Click"><i class="fas fa-plus-square"></i></asp:LinkButton>--%>
                                            <h4 class="pull-left">Total: <asp:Label ID="lblTotalExpenses" runat="server"></asp:Label></h4>
                                        </div>
                                        <div id="divExpensesNotification" runat="server"></div>
                                        
                                        <asp:Panel ID="Panel7" runat="server" class="col-md-12">
                                            <asp:HiddenField ID="hfTripID" runat="server" />
                                            
                                            <asp:GridView ID="gvExpenses" runat="server" CssClass="table table-hover" AutoGenerateColumns="false" 
                                                 DataKeyNames="ID" >
                                                <Columns>
                                                    <asp:BoundField DataField="ExpensesTypeName" HeaderText="Expense" />
                                                    <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                                    <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                                                    
                                                    
                                                   <%-- <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDelete" runat="server" CssClass="btn btn-xs btn-danger" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="Wipe"><i class="fas fa-trash"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                </Columns>
                                            </asp:GridView>
                                                
                                        </asp:Panel>
                                        <div class="col-md-12">
                                            
                                            
                                            <asp:HiddenField ID="HiddenField3" runat="server" />
                                            
                                           
                                        </div>
                                    </div>
                                </div>
                            
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
            <!-- MAIN CONTENT AREA ENDS -->
        </section>
    </section>
    <!-- END CONTENT -->
</asp:Content>
