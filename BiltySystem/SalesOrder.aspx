<%@ Page Title="" Language="C#" MasterPageFile="~/BiltySystem.Master" AutoEventWireup="true" CodeBehind="SalesOrder.aspx.cs" Inherits="BiltySystem.SalesOrder" %>
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
                                                <%--        <asp:Button ID="btnModal" runat="server" CssClass="btn btn-info" OnClick="btnModal_Click" />
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


                <asp:ScriptManager runat="server"></asp:ScriptManager>



                    <asp:Panel runat="server" CssClass="col-xs-12" ID="pnlInput" Visible="false">
                        <section class="box ">
                            <asp:LinkButton ID="lnkCloseInput" runat="server" CssClass=" fa fa-times-circle pull-right" Style="margin-top: 10px; margin-right: 10px;" onclick="lnkCloseInput_Click"></asp:LinkButton>
                            <div class="content-body">
                                <div class="row">
                                    <div class="col-md-12 col-sm-12 col-xs-12">
                        
                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                        
                                        <div class="col-md-3">
                                            <label>Customer Name</label>
                                            <asp:DropDownList ID="ddlCustomerName" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>

                                          
                                        <div class="col-md-3">
                                            <label>Project</label>
                                            <asp:TextBox ID="txtProject"  runat="server" CssClass="form-control"></asp:TextBox>

                                        </div>
                                        <div class="col-md-3">
                                            <label>Sales Date</label>
                                            <asp:TextBox ID="txtSalesDate" TextMode="Date" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                         <div class="col-md-3">
                                            <label>Project Cost</label>
                                            <asp:TextBox ID="txtProjectCost" TextMode="Number" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3">
                                            <label>Sales Price</label>
                                            <asp:TextBox ID="txtSalesPrice" TextMode="Number" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3">
                                            <label>Project Discount</label>
                                            <asp:TextBox ID="txtProjectDiscount" TextMode="Number" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3">
                                            <label>Sales By</label>
                                            <asp:DropDownList ID="ddlSalesBy" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                        <div class="col-md-3">
                                            <label>Modules</label>
                                            <asp:LinkButton ID="lnkAddModules" runat="server" CssClass="btn btn-info" OnClick="lnkAddModules_Click">Click to Add Modules</asp:LinkButton>
                                        </div>
                                        
                                        <%--<div class="col-md-6 m-t-10" style="height: 150px; overflow-y: scroll;">
                                            <label>Modules</label>
                                            <asp:CheckBoxList ID="cbModules" runat="server"></asp:CheckBoxList>
                                        </div>--%>

                                      
                                         
                                    </div>

                                        <div class="col-lg-12">
                                           <asp:LinkButton ID="lnkSubmit" runat="server" CssClass="btn btn-success pull-right m-l-10" OnClick="lnkSubmit_Click"><i class="fa fa-save"></i> | Save</asp:LinkButton>
                                                

                                         </div>
                                </div>
                            </div>

                        </section>
                    </asp:Panel>
                     
                    <div class="col-xs-12">

                        <section class="box ">
                            

                             <header class="panel_header">
                                <h2 class="title pull-left">Sales Order</h2> 
                                <div class="actions panel_actions pull-right">
                           
                                    <asp:LinkButton ID="lnkAddNew" runat="server" CssClass="box_setting fas fa-plus" OnClick="lnkAddNew_Click" ToolTip="Click to Add New"></asp:LinkButton>
                                    <%--<a class="box_setting fa fa-cog" data-toggle="modal" href="#section-settings"></a>
                                    <a class="box_close fa fa-times"></a>--%>
                                </div>
                            </header>
                            <div class="content-body">
                                <asp:HiddenField ID="hfEditID" runat="server" />
                                <div class="row">
                                    <div class="col-xs-12">
                                        <asp:GridView ID="gvResult" runat="server" Width="100%" EmptyDataText="No Record found" AutoGenerateColumns="false"
                                            CssClass="table table-hover" Font-Size="12px" Font-Names="Open Sans" OnRowCommand="gvResult_RowCommand" OnRowDataBound="gvResult_RowDataBound"  DataKeyNames="SalesOrderID,IsActive" BackColor="White">
                                            <Columns>

                                              <asp:TemplateField HeaderText="S.no">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                              <asp:BoundField DataField="CompanyName" HeaderText="Customer Name"></asp:BoundField>
                                                <asp:BoundField DataField="Project" HeaderText="Project Name"></asp:BoundField>
                                                <asp:BoundField DataField="SalesDate" HeaderText="Sales Date"></asp:BoundField>
                                                <asp:BoundField DataField="ProjectCost" HeaderText="Project Cost"></asp:BoundField>
                                                <asp:BoundField DataField="SalesPrice" HeaderText="Sales Price"></asp:BoundField>
                                                <asp:BoundField DataField="ProjectDiscount" HeaderText="Project Discount"></asp:BoundField>

                                                
                                                

                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkActive" runat="server" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="Active"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                
                                                <asp:TemplateField>

                                                    <ItemTemplate>

                                                        <asp:LinkButton ID="lnkEdit" runat="server" ForeColor="Blue" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="Change"><i class="fas fa-edit"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="lnkDelete" runat="server" ForeColor="Maroon" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="Erase"><i class="fas fa-trash"></i></asp:LinkButton>
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
                    
                    <ajaxToolkit:ModalPopupExtender ID="modalAddModule" runat="server" PopupControlID="pnlAddModule" DropShadow="True" TargetControlID="btnAddModule" 
                        CancelControlID="btnCloseModule" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
                    <asp:Panel ID="pnlAddModule" runat="server" CssClass="row" style="background-color: white; padding: 20px; border: 1px solid black;" Width="850px">
                        <asp:Button ID="btnAddModule" runat="server" style="display: none" />
                        <asp:LinkButton ID="btnCloseModule" runat="server" ForeColor="Maroon" CssClass="pull-right" ><i class="fas fa-times-circle"></i></asp:LinkButton>
                        <%-- <asp:LinkButton ID="btnCloseModule" runat="server" CssClass=" fa fa-times-circle pull-right" Style="margin-top: 10px; margin-right: 10px;" ></asp:LinkButton>--%>
                        <h4><asp:Label ID="Label1" runat="server"></asp:Label></h4> 
                         
                        <div class="col-md-12">
                            <div class="col-md-3">
                                <label>Module</label>
                                <asp:DropDownList ID="ddlModule" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <label>Price</label>
                                <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label>Discount %</label>
                                <asp:TextBox ID="txtDiscount" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            
                            <div class="col-md-3">
                                <asp:LinkButton ID="lnkSaveModule" runat="server" CssClass="btn btn-primary" style="margin-top: 27px;" OnClick="lnkSaveModule_Click"><i class="fas fa-plus"></i></asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <asp:GridView ID="gvModules" runat="server" CssClass="table table-hover" DataKeyNames="ModuleID" AutoGenerateColumns="false">

                                               <Columns>
                                                   <asp:BoundField DataField="ModuleName" HeaderText="Module" />
                                                   <asp:BoundField DataField="ModulePrice" HeaderText="Price" />
                                                   <asp:BoundField DataField="ModuleDiscount" HeaderText="Discount" />

                                               </Columns>


                            </asp:GridView>
                        </div>
                    </asp:Panel>
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
</asp:Content>
