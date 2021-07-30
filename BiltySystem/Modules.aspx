<%@ Page Title="" Language="C#" MasterPageFile="~/BiltySystem.Master" AutoEventWireup="true" CodeBehind="Modules.aspx.cs" Inherits="BiltySystem.Modules" %>
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
                            <asp:LinkButton ID="lnkCloseInput" runat="server" CssClass=" fa fa-times-circle pull-right" Style="margin-top: 10px; margin-right: 10px;" OnClick="lnkCloseInput_Click"></asp:LinkButton>
                            <div class="content-body">
                                <div class="row">
                                    <div class="col-md-12 col-sm-12 col-xs-12">
                        
                                        
                                        <div class="col-md-3" id="CustomerPlaceholder" runat="server">
                                            <label>Module Name</label>
                                            <asp:TextBox ID="txtModuleName" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>

                                          
                                        <div class="col-md-3">
                                            <label>General Price</label>
                                            <asp:TextBox ID="txtGeneralPrice" CssClass="form-control" TextMode="Number" runat="server"></asp:TextBox>

                                        </div>
                                        <div class="col-md-3">
                                            <label>Market Price</label>
                                            <asp:TextBox ID="txtMarketPrice" CssClass="form-control" TextMode="Number" runat="server"></asp:TextBox>
                                        </div>
                                         <div class="col-md-3">
                                            <label>Module Discount</label>
                                            <asp:TextBox ID="txtModuleDiscount" CssClass="form-control" TextMode="Number" runat="server"></asp:TextBox>
                                        </div>
                                      

                                        
                                         
                                    </div>
                                    <div class="col-lg-12 m-t-20">
                                           <asp:LinkButton ID="lnkSubmit" runat="server" CssClass="btn btn-success pull-right m-l-10" OnClick="lnkSubmit_Click"><i class="fa fa-save"></i> | Save</asp:LinkButton>
                                                

                                         </div>
                                </div>
                            </div>

                        </section>
                    </asp:Panel>


                     
                    <div class="col-xs-12">

                        <section class="box ">
                            

                             <header class="panel_header">
                                <h2 class="title pull-left">Modules Profile</h2> 
                                <div class="actions panel_actions pull-right">
                           
                                    <asp:LinkButton ID="lnkAddNew" runat="server" CssClass="box_setting fas fa-plus" onclick="lnkAddNew_Click" ToolTip="Click to Add New"></asp:LinkButton>
                                    <%--<a class="box_setting fa fa-cog" data-toggle="modal" href="#section-settings"></a>
                                    <a class="box_close fa fa-times"></a>--%>
                                </div>
                            </header>
                            <div class="content-body">
                                <asp:HiddenField ID="hfEditID" runat="server" />
                                <div class="row">
                                    <div class="col-xs-12">
                                        <asp:GridView ID="gvResult" runat="server" Width="100%" EmptyDataText="No Record found" AutoGenerateColumns="false"
                                            CssClass="table table-hover" Font-Size="12px" Font-Names="Open Sans" OnRowCommand="gvResult_RowCommand" OnRowDataBound="gvResult_RowDataBound"  DataKeyNames="ModuleID,IsActive" BackColor="White">
                                            <Columns>

                                              <asp:TemplateField HeaderText="S.no">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:BoundField DataField="ModuleName" HeaderText="Module Name"></asp:BoundField>
                                                <asp:BoundField DataField="GeneralPrice" HeaderText="General Price"></asp:BoundField>
                                                <asp:BoundField DataField="MarketPrice" HeaderText="Market Price"></asp:BoundField>
                                                <asp:BoundField DataField="ModuleDiscount" HeaderText="Module Discount"></asp:BoundField>
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
                                                 <%-- <asp:TemplateField HeaderText="Details">
                                              <ItemTemplate>
                                              <asp:LinkButton ID="lnkDetail" runat="server" ForeColor="Blue" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="Details">Add Detail</asp:LinkButton>
                                             </ItemTemplate>
                                              </asp:TemplateField>--%>
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
                            <asp:LinkButton ID="lnkConfirm" runat="server" ForeColor="Green" Font-Size="70px" onclick="lnkConfirm_Click"><i class="fas fa-check pull-left"></i></asp:LinkButton>
                            <asp:LinkButton ID="lnkCancelConfirm" runat="server" ForeColor="Red" Font-Size="70px"><i class="fas fa-times-circle pull-right"></i></asp:LinkButton>
                        </div>
                    </asp:Panel>
                      

                </ContentTemplate>
            </asp:UpdatePanel>
            <!-- MAIN CONTENT AREA ENDS -->
        </section>
    </section>
</asp:Content>
