<%@ Page Title="" Language="C#" MasterPageFile="~/BiltySystem.Master" AutoEventWireup="true" CodeBehind="BiltyInvoices.aspx.cs" Inherits="BiltySystem.BiltyInvoices" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Invoices</title>
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

                    <div class="col-xs-12">
                        <section class="box ">
                            <header class="panel_header">
                                <h2 class="title pull-left">Invoices</h2> 
                                <div class="col-md-6 m-t-25 pull-right">
                                    <asp:LinkButton ID="lnkCancelSearch" runat="server" OnClick="lnkCancelSearch_Click"><i class="fa fa-times-circle m-t-10 m-r-5 pull-left" style="color: maroon;"></i></asp:LinkButton>
                            
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="pull-left m-r-10 pull-left"></asp:TextBox>
                                    <asp:LinkButton ID="lnkSearch" runat="server" OnClick="lnkSearch_Click" CssClass="pull-left"><i class="fa fa-search"></i></asp:LinkButton>
                                </div>
                                <div class="actions panel_actions pull-right">
                                    <%--<asp:LinkButton ID="lnkAddNew" runat="server" CssClass="box_setting fas fa-plus" OnClick="lnkAddNew_Click" ToolTip="Click to Add New"></asp:LinkButton>--%>
                                </div>
                            </header>
                            <div class="content-body">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <asp:HiddenField ID="hfSelectedInvoice" runat="server" />
                                        <asp:HiddenField ID="hfSelectedInvoiceCustomer" runat="server" />
                                        <asp:GridView ID="gvResult" runat="server" Width="100%" EmptyDataText="No Record found" AutoGenerateColumns="false"
                                            CssClass="table table-hover" Font-Size="12px" Font-Names="Open Sans" DataKeyNames="InvoiceNo, TotalBalance, isPaid, CreditLimit" BackColor="White" 
                                            OnRowCommand="gvResult_RowCommand" OnRowDataBound="gvResult_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.no">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="InvoiceNo" HeaderText="Invoice#"></asp:BoundField>
                                                <asp:BoundField DataField="CustomerInvoice" HeaderText="Customer Invoice#"></asp:BoundField>
                                                <asp:BoundField DataField="CustomerCompany" HeaderText="Customer"></asp:BoundField>
                                                <asp:BoundField DataField="OrderID" HeaderText="Order#"></asp:BoundField>
                                                <asp:BoundField DataField="CreatedDate" HeaderText="Invoice Date"></asp:BoundField>
                                                <asp:TemplateField HeaderText="Total" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTotal" runat="server" Text='<%# "Rs. " + String.Format("{0:n}", Convert.ToDouble(Eval("Total"))) + "/-" %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Balance" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTotalBalance" runat="server" Text='<%# "Rs. " + String.Format("{0:n}", Eval("TotalBalance") == DBNull.Value ? 0 : (Convert.ToDouble(Eval("TotalBalance")) <=0 ? 0 : Convert.ToDouble(Eval("TotalBalance")))) + "/-" %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkReceivePayment" runat="server" Font-Size="12px" ForeColor="SeaGreen" ToolTip="Click to receive payment" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="Payment"><i class="fas fa-money-bill-alt"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkPrintInvoice" runat="server" Font-Size="12px" ToolTip="Click to Print this invoice" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="PrintBill"><i class="fas fa-print"></i></asp:LinkButton>
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

                    

                    <ajaxToolkit:ModalPopupExtender ID="modalPayment" runat="server" PopupControlID="pnlPayment" DropShadow="True" TargetControlID="btnOpenPayment" 
                        CancelControlID="lnkClosePayment" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
                    <asp:Panel ID="pnlPayment" runat="server" CssClass="row" style="background-color: white; padding: 20px; border: 1px solid black;" Width="600px">
                        <asp:Button ID="btnOpenPayment" runat="server" style="display: none" />
                        <asp:LinkButton ID="lnkClosePayment" runat="server" ForeColor="Maroon" CssClass="pull-right" style="display: none;"><i class="fa fa-times-circle-o"></i></asp:LinkButton>
                        <h4><asp:Label ID="Label1" runat="server" Text="Payment"></asp:Label></h4>                       
                        <div class="col-md-12">
                            <div class="col-md-3 form-group">
                                <asp:Label id="lblPaymentMode" runat="server" Font-Size="Smaller"><strong>Payment Mode</strong></asp:Label>
                                <asp:RadioButtonList ID="rbPaymentMode" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rbPaymentMode_SelectedIndexChanged" Font-Size="Smaller">
                                    <asp:ListItem>Cash</asp:ListItem>
                                    <asp:ListItem>Cheque</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                            <div class="col-md-3 form-group">
                                <asp:Label id="lblPettyCash" runat="server" Font-Size="Smaller"><strong>Petty Cash</strong></asp:Label>
                                <asp:CheckBox ID="cbPettyCash" runat="server" AutoPostBack="true" OnCheckedChanged="cbPettyCash_CheckedChanged" />
                            </div>
                            <div class="col-md-6" id="bankAccountsPlaceholder" runat="server">
                                <asp:Label id="lblBankAccounts" runat="server" Font-Size="Smaller"><strong>Bank Accounts</strong></asp:Label>
                                <asp:DropDownList ID="ddlBankAccounts" runat="server" CssClass="form-control">
                                    <asp:ListItem>Cash</asp:ListItem>
                                    <asp:ListItem>Cheque</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-6 form-group">
                                    <asp:Label id="lblAmount" runat="server" Font-Size="Smaller"><strong>Amount</strong></asp:Label>
                                    <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="col-md-6 form-group" id="DocumentNoPlaceholder" runat="server" visible="false">
                                    <asp:Label id="lblDocumentNo" runat="server" Font-Size="Smaller"><strong>Document No.</strong></asp:Label>
                                    <asp:TextBox ID="txtDocumentNo" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                                        
                            <div class="col-md-12">
                                <div id="divPaymentNotification" runat="server"></div>
                            </div>
                            <div class="col-md-12">
                                <asp:LinkButton ID="lnkSavePayment" runat="server" ForeColor="Green" Font-Size="70px" OnClick="lnkSavePayment_Click"><i class="fas fa-check pull-left"></i></asp:LinkButton>
                                <asp:LinkButton ID="lnkCancelSavePayment" runat="server" ForeColor="Red" Font-Size="70px"><i class="fas fa-times-circle pull-right"></i></asp:LinkButton>
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
