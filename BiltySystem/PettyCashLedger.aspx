<%@ Page Title="" Language="C#" MasterPageFile="~/BiltySystem.Master" AutoEventWireup="true" CodeBehind="PettyCashLedger.aspx.cs" Inherits="BiltySystem.PettyCashLedger" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>        
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }

        .spaced input[type="radio"]
        {
            margin-Left: 50px; /* Or any other value */
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <!-- START CONTENT -->
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
                                     <%--           <asp:Button ID="btnModal" runat="server" CssClass="btn btn-info" OnClick="btnModal_Click" />
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


                    <asp:Panel class="col-xs-12" ID="MainLedger" runat="server">
                        <section class="box ">
                            <header class="panel_header">
                                <h2 class="title pull-left">Petty Cash</h2> 
                                <asp:Panel ID="pnlSearch" runat="server" DefaultButton="lnkSearchPettyCashAccount" CssClass="col-md-12 m-t-25 pull-right">
                                    <div class="form-group col-md-4">
                                        <label>From</label>
                                        <asp:TextBox ID="txtFrom" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label>To</label>
                                        <asp:TextBox ID="txtTo" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <asp:LinkButton ID="lnkSearchPettyCashAccount" runat="server"  CssClass="pull-left btn btn-info m-t-25" OnClick="lnkSearchPettyCashAccount_Click"><i class="fa fa-search"></i></asp:LinkButton>
                                        <asp:LinkButton ID="lnkCancelSearchPettyCashAccount" runat="server" CssClass="btn btn-danger m-t-25 pull-left" OnClick="lnkCancelSearchPettyCashAccount_Click" ToolTip="Click to clear search result"><i class="fa fa-times"></i></asp:LinkButton>
                                        <asp:LinkButton ID="lnkPrintSingleLedger" runat="server" CssClass="btn btn-success m-t-25 pull-left" OnClick="lnkPrintSingleLedger_Click" ToolTip="Click to Print"><i class="fas fa-print"></i></asp:LinkButton>
                                        <asp:LinkButton ID="lnkAddTransaction" runat="server" CssClass="pull-right m-t-25 btn btn-primary" ToolTip="Click to transact in current ladger" OnClick="lnkAddTransaction_Click"><i class="fa fa-plus-square"></i></asp:LinkButton>
                                    </div>
                                </asp:Panel>
                                <div class="actions panel_actions pull-right">
                           
                                    <%--<asp:LinkButton ID="lnkAddNew" runat="server" CssClass="box_setting fas fa-plus"  ToolTip="Click to Add New"></asp:LinkButton>--%>
                                    <%--<a class="box_setting fa fa-cog" data-toggle="modal" href="#section-settings"></a>
                                    <a class="box_close fa fa-times"></a>--%>
                                </div>
                            </header>
                            <div class="content-body">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <asp:GridView ID="gvResult" runat="server" Width="100%" EmptyDataText="No Record found" AutoGenerateColumns="false"
                                            CssClass="table table-hover" Font-Size="12px" Font-Names="Open Sans" DataKeyNames="AccountID" BackColor="White">
                                            <Columns>
                                                <asp:BoundField DataField="AccountID" HeaderText="Ref #" ItemStyle-Width="20%" ItemStyle-Wrap="false"></asp:BoundField>
                                                <asp:BoundField DataField="DateCreated" HeaderText="Date" ItemStyle-Width="20%" ItemStyle-Wrap="false"></asp:BoundField>
                                                <asp:BoundField DataField="TransactionDate" HeaderText="Transaction Date" ItemStyle-Width="20%" ItemStyle-Wrap="false"></asp:BoundField>
                                                <asp:BoundField DataField="Item" HeaderText="Item" ItemStyle-Width="50%" ItemStyle-Wrap="false"></asp:BoundField>
                                                
                                                <asp:TemplateField HeaderText="Debit"><ItemTemplate><asp:Label ID="lblDebit" runat="server" Text='<%# String.Format("{0:n}", Eval("Debit")) %>'></asp:Label></ItemTemplate></asp:TemplateField>
                                                <asp:TemplateField HeaderText="Credit"><ItemTemplate><asp:Label ID="lblCredit" runat="server" Text='<%# String.Format("{0:n}", Eval("Credit")) %>'></asp:Label></ItemTemplate></asp:TemplateField>
                                                <asp:TemplateField HeaderText="Balance"><ItemTemplate><asp:Label ID="lblBalance" runat="server" Text='<%# String.Format("{0:n}", Eval("Balance")) %>'></asp:Label></ItemTemplate></asp:TemplateField>
                                                <%--<asp:BoundField DataField="Debit" HeaderText="Debit" ItemStyle-Width="10%" ItemStyle-Wrap="false"></asp:BoundField>
                                                <asp:BoundField DataField="Credit" HeaderText="Credit" ItemStyle-Width="10%" ItemStyle-Wrap="false"></asp:BoundField>
                                                <asp:BoundField DataField="Balance" HeaderText="Balance" ItemStyle-Width="10%" ItemStyle-Wrap="false"></asp:BoundField>--%>
                                            </Columns>
                                            <HeaderStyle BackColor="#4b23dd" ForeColor="White" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </section>
                    </asp:Panel>

                    


                    <asp:Panel class="col-xs-12" ID="pnlPrintSingleLedger" runat="server" style="display:none">
                        <section class="box ">
                            <header class="panel_header">
                                <h2 class="title pull-left">Print Single Ledger</h2>                                 
                                <asp:LinkButton ID="lnkCloseSingleLedger" runat="server" OnClick="lnkCloseSingleLedger_Click" CssClass="btn btn-danger pull-right m-t-10 m-r-10"><i class="fa fa-times"></i></asp:LinkButton>                               
                            </header>
                            <div class="content-body">
                                <div class="row">
                                    <div class="col-xs-12">
                                        
                                         <%-- SSRS Report --%>
                                          <rsweb:reportviewer ID="rvPettyCash" runat="server" Width="100%" Height="800px"></rsweb:reportviewer>
                                         <%-- SSRS Report --%>

                                    </div>
                                </div>
                            </div>
                        </section>
                    </asp:Panel>
                    


                    <ajaxToolkit:ModalPopupExtender ID="modalConfirm" runat="server" PopupControlID="pnlConfirm" DropShadow="True" TargetControlID="btnOpenConfirmModal" 
                        CancelControlID="lnkCloseConfirmModal" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
                    <asp:Panel ID="pnlConfirm" runat="server" CssClass="row" style="background-color: white; padding: 20px; border: 1px solid black;" Width="400px">
                        <asp:Button ID="btnOpenConfirmModal" runat="server" style="display: none" />
                        <asp:LinkButton ID="lnkCloseConfirmModal" runat="server" ForeColor="Maroon" CssClass="pull-right" style="display: none;"><i class="fa fa-times-circle-o"></i></asp:LinkButton>
                        <h4><asp:Label ID="lblModalTitle" runat="server"></asp:Label></h4>                       
                        <div class="col-md-12">
                            <asp:HiddenField ID="hfConfirmAction" runat="server" />
                            <asp:LinkButton ID="lnkConfirm" runat="server" ForeColor="Green" Font-Size="70px" ><i class="fas fa-check pull-left"></i></asp:LinkButton>
                            <asp:LinkButton ID="lnkCancelConfirm" runat="server" ForeColor="Red" Font-Size="70px"><i class="fas fa-times-circle pull-right"></i></asp:LinkButton>
                        </div>
                    </asp:Panel>

                    <ajaxToolkit:ModalPopupExtender ID="modalTransaction" runat="server" PopupControlID="pnlTransaction" DropShadow="True" TargetControlID="lnkOpenAddTransaction" 
                        CancelControlID="lnkCloseTransactions" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
                    <asp:Panel ID="pnlTransaction" runat="server" CssClass="box " Width="600px" DefaultButton="lnkSaveTransaction">
                        <header class="panel_header">
                            <h2 class="title pull-left">Transaction</h2>
                            <div class="actions panel_actions pull-right">
                                <asp:LinkButton ID="lnkOpenAddTransaction" runat="server" CssClass="box_close fa fa-times" style="display:none;"></asp:LinkButton>
                                <asp:LinkButton ID="lnkCloseTransactions" runat="server" CssClass="box_close fa fa-times" style="display:none;"></asp:LinkButton>
                                <asp:LinkButton ID="lnkCloseTransaction" runat="server" CssClass="btn btn-danger btn-xs m-t-15 m-r-5" style="margin-right: 5px;" OnClick="lnkCloseTransaction_Click"><i class="fa fa-times-circle"></i></asp:LinkButton>
                            </div>
                        </header>
                        <div class="content-body">
                            <div class="row">
                                
                                <div class="col-md-6 col-sm-12 col-xs-12">
                                    <div class="form-group">
                                        <label class="form-label" for="email-1">Tansaction Type</label>
                                        <asp:RadioButtonList ID="rbTransactionType" runat="server" RepeatDirection="Horizontal" CssClass="spaced">
                                            <asp:ListItem>Deposit</asp:ListItem>
                                            <asp:ListItem>Withdraw</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                
                                <div class="col-md-6 col-sm-12 col-xs-12">
                                    <div class="form-group">
                                        <label class="form-label" for="email-1">Payment Via</label>
                                        <asp:RadioButtonList ID="rbTransactionMode" runat="server" RepeatDirection="Horizontal" CssClass="spaced">
                                            <asp:ListItem>Cash</asp:ListItem>
                                            <asp:ListItem>Cheque</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                
                                <div class="col-md-4 col-sm-12 col-xs-12">
                                    <div class="form-group">
                                        <label class="form-label" for="email-1">Amount</label>
                                        <asp:TextBox ID="txtTransactionAmount" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                    </div>
                                </div>
                                
                                <div class="col-md-4 col-sm-12 col-xs-12">
                                    <div class="form-group">
                                        <label class="form-label" for="email-1">Document No.</label>
                                        <asp:TextBox ID="txtTransactionDocumentNo" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-4 col-sm-12 col-xs-12">
                                    <div class="form-group">
                                        <label class="form-label" for="email-1">By</label>
                                        <asp:TextBox ID="txtTransactedBy" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                
                                <div class="col-md-12 col-sm-12 col-xs-12">
                                    <div class="form-group">
                                        <label class="form-label" for="email-1">For</label>
                                        <asp:TextBox ID="txtTransactedFor" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>

                                  <div class="col-md-12 col-sm-12 col-xs-12">
                                    <div class="form-group">
                                        <label class="form-label" for="email-1">Transaction Date</label>
                                        <asp:TextBox ID="txtTransactionDate" runat="server" CssClass="form-control" TextMode="Date" ></asp:TextBox>
                                    </div>
                                </div>
                                
                                
                                <div class="col-md-12 col-sm-12 col-xs-12">
                                    <div class="form-group">
                                        <asp:LinkButton ID="lnkSaveTransaction" runat="server" CssClass="btn btn-info m-t-30" ToolTip="Click to Transact" OnClick="lnkSaveTransaction_Click"><i class="fas fa-save"></i> | Save</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            
                            <div id="divTransactionNotification" runat="server"></div>
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <!-- MAIN CONTENT AREA ENDS -->
        </section>
    </section>
    <!-- END CONTENT -->
</asp:Content>
