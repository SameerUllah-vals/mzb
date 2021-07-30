<%@ Page Title="" Language="C#" MasterPageFile="~/BiltySystem.Master" AutoEventWireup="true" CodeBehind="Vouchers.aspx.cs" Inherits="BiltySystem.Vouchers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>        
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }
    </style>
    <style>
        #tblVoucher tr td {
            padding: 5px;
        }
    </style>

    
    <style>
        .pagination-ys {
            /*display: inline-block;*/
            padding-left: 0;
            margin: 20px 0;
            border-radius: 4px;
        }

        .pagination-ys table > tbody > tr > td {
            display: inline;
        }

        .pagination-ys table > tbody > tr > td > a,
        .pagination-ys table > tbody > tr > td > span {
            position: relative;
            float: left;
            padding: 8px 12px;
            line-height: 1.42857143;
            text-decoration: none;
            color: #dd4814;
            background-color: #ffffff;
            border: 1px solid #dddddd;
            margin-left: -1px;
        }

        .pagination-ys table > tbody > tr > td > span {
            position: relative;
            float: left;
            padding: 8px 12px;
            line-height: 1.42857143;
            text-decoration: none;    
            margin-left: -1px;
            z-index: 2;
            color: #aea79f;
            background-color: #f5f5f5;
            border-color: #dddddd;
            cursor: default;
        }

        .pagination-ys table > tbody > tr > td:first-child > a,
        .pagination-ys table > tbody > tr > td:first-child > span {
            margin-left: 0;
            border-bottom-left-radius: 4px;
            border-top-left-radius: 4px;
        }

        .pagination-ys table > tbody > tr > td:last-child > a,
        .pagination-ys table > tbody > tr > td:last-child > span {
            border-bottom-right-radius: 4px;
            border-top-right-radius: 4px;
        }

        .pagination-ys table > tbody > tr > td > a:hover,
        .pagination-ys table > tbody > tr > td > span:hover,
        .pagination-ys table > tbody > tr > td > a:focus,
        .pagination-ys table > tbody > tr > td > span:focus {
            color: #97310e;
            background-color: #eeeeee;
            border-color: #dddddd;
        }
    </style>

    <script type="text/javascript">
        function Print() {
            debugger;
            
            var InvoiceHTML = "";


            
            
            InvoiceHTML += "<header class=\"clearfix\">";
            for (var i = 0; i < 2; i++) {
                InvoiceHTML += "<h2>Payment Voucher# " + document.getElementById("ContentPlaceHolder1_lblVoucherPrintNo").innerHTML + "</h2>";
                InvoiceHTML += "<table style=\"width: 100%\" border=\"1\" cellpadding=\"5\">";
                    InvoiceHTML += "<tbody>";
                        InvoiceHTML += document.getElementById("ContentPlaceHolder1_tblVoucher").innerHTML;            
                    InvoiceHTML += "</tbody>";
                InvoiceHTML += "</table>";

                InvoiceHTML += "<br /><br />";
                InvoiceHTML += "<div style=\"padding: 5px;\"><p style=\"text-decoration: underline; text-align: center;\">Developed by: Vals Technologies | PABX: 0304 111 66 88 | www.valstechnologies.com</p></div>";
                
                if (i == 0) {
                    
                    InvoiceHTML += "<hr>";
                    InvoiceHTML += "<br />";
                }
            }
            
            InvoiceHTML += "</header>";
            
            var prntData = document.getElementById('ContentPlaceHolder1_pnlInvoices');
            var prntWindow = window.open("", "Print", "width=400,height=400,left=0,top=0,toolbar=0,scrollbar=1,status=0");
            var style = "";
            prntWindow.document.write(InvoiceHTML);
            prntWindow.document.close();
            prntWindow.focus();
            prntWindow.print();
            prntWindow.close();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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


                    <div class="col-xs-12">
                        <section class="box ">
                            <header class="panel_header">
                                <h2 class="title pull-left">Vouchers</h2> 
                                <%--<div class="col-md-6 m-t-25 pull-right">
                                    <asp:LinkButton ID="lnkCancelSearch" runat="server" OnClick="lnkCancelSearch_Click"><i class="fa fa-times-circle m-t-10 m-r-5 pull-left" style="color: maroon;"></i></asp:LinkButton>
                            
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="pull-left m-r-10 pull-left"></asp:TextBox>
                                    <asp:LinkButton ID="lnkSearch" runat="server" OnClick="lnkSearch_Click" CssClass="pull-left"><i class="fa fa-search"></i></asp:LinkButton>
                                </div>--%>
                                <div class="actions panel_actions pull-right">
                                    <asp:LinkButton ID="lnkCancelSearch" runat="server" OnClick="lnkCancelSearch_Click1" ForeColor="Maroon"><i class="fas fa-ban"></i></asp:LinkButton>
                                    <a class="box_toggle fa fa-chevron-up"></a>

                                </div>
                                <div class="actions panel_actions pull-right">
                           
                                    <asp:LinkButton ID="lnkAddNew" runat="server" CssClass="box_setting fas fa-plus" OnClick="lnkAddNew_Click" ToolTip="Click to Add New"></asp:LinkButton>
                                    <%--<a class="box_setting fa fa-cog" data-toggle="modal" href="#section-settings"></a>
                                    <a class="box_close fa fa-times"></a>--%>
                                </div>
                                 <div class="col-xs-12">
                                 <div class="col-md-3">
                                        <label>From</label>
                                        <asp:LinkButton ID="lnkClearFrom" runat="server" CssClass="fas fa-ban pull-right" ForeColor="Maroon" style="margin-top: 3%;" onclick="lnkClearFrom_Click"></asp:LinkButton>
                                        <asp:TextBox ID="txtSearchDateFrom" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label>To</label>
                                        <asp:LinkButton ID="lnkClearTo" runat="server" CssClass="fas fa-ban pull-right" ForeColor="Maroon" style="margin-top: 3%;" onclick="lnkClearTo_Click"></asp:LinkButton>
                                        <asp:TextBox ID="txtSearchDateTo" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    </div>
                                 <div class="col-md-2">
                                            <label class="form-label">Voucher No</label>
                                        <asp:LinkButton ID="lnkClearVoucherNo" runat="server" CssClass="fas fa-ban pull-right" ForeColor="Maroon" style="margin-top: 3%;" onclick="lnkClearVoucherNo_Click"></asp:LinkButton>
                                            
                                                <asp:TextBox ID="txtSearchVoucherNo" runat="server" class="form-control"></asp:TextBox>
                                          
                                    </div>
                                 <div class="col-md-3">
                                        <asp:LinkButton ID="lnkClearVendor" runat="server" CssClass="fas fa-ban pull-right" ForeColor="Maroon" style="margin-top: 3%;" OnClick="lnkClearVendor_Click"></asp:LinkButton>                                       
                                            <label class="form-label">Vendor</label>
                                            
                                                <asp:TextBox ID="txtSearchVendor" runat="server" class="form-control"></asp:TextBox>
                                           
                                    </div>
                                     <div class="col-xs-1">      
                                    <asp:LinkButton ID="lnkSearchFilter" runat="server" CssClass="btn btn-success pull-right m-r-10 m-t-25" ToolTip="Click to Search Bilty" onclick="lnkSearchFilter_Click"><i class="fas fa-search"></i></asp:LinkButton>
                                </div>
                                     </div>
                            </header>
                            <div class="content-body">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <asp:GridView ID="gvResult" runat="server" Width="100%" EmptyDataText="No Record found" AutoGenerateColumns="false"
                                            CssClass="table table-hover" Font-Size="12px" Font-Names="Open Sans" DataKeyNames="VoucherID, isPayed" BackColor="White" 
                                            OnRowCommand="gvResult_RowCommand" OnRowDataBound="gvResult_RowDataBound" AllowPaging="true" AllowSorting="true" OnSorting="gvResult_Sorting" 
                                            OnPageIndexChanging="gvResult_PageIndexChanging" PageSize="50" PagerSettings-Position="TopAndBottom" PagerStyle-HorizontalAlign="Center" PagerSettings-FirstPageText="<<" PagerSettings-LastPageText=">>">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.no">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="VoucherNo" HeaderText="Voucher #"></asp:BoundField>
                                                <asp:BoundField DataField="Vendor" HeaderText="Vendor"></asp:BoundField>
                                                <asp:BoundField DataField="Bank" HeaderText="Bank"></asp:BoundField>
                                                <asp:BoundField DataField="ChequeNo" HeaderText="Cheque #"></asp:BoundField>
                                                <asp:BoundField DataField="Amount" HeaderText="Amount"></asp:BoundField>
                                                <asp:BoundField DataField="VehicleRegNo" HeaderText="Vehicle"></asp:BoundField>
                                                <asp:BoundField DataField="ReceivedBy" HeaderText="Received By"></asp:BoundField>
                                                <asp:BoundField DataField="CreatedDate" HeaderText="Created ON"></asp:BoundField>
                                                <asp:BoundField DataField="ModifiedDate" HeaderText="Updated ON"></asp:BoundField>
                                                <asp:TemplateField ItemStyle-Width="5%" HeaderText="Paid?">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgVoucherPayed" runat="server" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="Payed" ImageUrl="~/assets/images/Off.png" CssClass="m-t-5" Width="45%" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkPrintInvoice" runat="server" Font-Size="12px" ToolTip="Click to Print this Voucher" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="PrintVoucher"><i class="fas fa-print"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle BackColor="#4b23dd" ForeColor="White" />
                                            <PagerStyle CssClass="pagination-ys" />
                                        </asp:GridView>
                                        <asp:HiddenField ID="hfVoucherID" runat="server" />
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
                        <div class="col-md-12">
                            <label>Received By</label>
                            <asp:TextBox ID="txtReceivedBy" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <h4><asp:Label ID="lblModalTitle" runat="server"></asp:Label></h4>                       
                        <div class="col-md-12">
                            <asp:HiddenField ID="hfConfirmAction" runat="server" />
                            <asp:LinkButton ID="lnkConfirm" runat="server" ForeColor="Green" Font-Size="70px" OnClick="lnkConfirm_Click"><i class="fas fa-check pull-left"></i></asp:LinkButton>
                            <asp:LinkButton ID="lnkCancelConfirm" runat="server" ForeColor="Red" Font-Size="70px" OnClick="lnkCancelConfirm_Click"><i class="fas fa-times-circle pull-right"></i></asp:LinkButton>
                        </div>
                        <p style="color: maroon;"><string>Notice: </string>Once action taken, cannot revert back</p>
                        <div id="divConfirmNotification" runat="server"></div>
                    </asp:Panel>

                    <ajaxToolkit:ModalPopupExtender ID="modalPayment" runat="server" PopupControlID="pnlPayment" DropShadow="True" TargetControlID="btnOpenPayment" 
                        CancelControlID="lnkClosePayment" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
                    <asp:Panel ID="pnlPayment" runat="server" CssClass="row" style="background-color: white; padding: 20px; border: 1px solid black;" Width="600px">
                        <asp:Button ID="btnOpenPayment" runat="server" style="display: none" />
                        <asp:LinkButton ID="lnkClosePayment" runat="server" ForeColor="Maroon" CssClass="pull-right" style="display: none;"><i class="fa fa-times-circle-o"></i></asp:LinkButton>
                        <h4><asp:Label ID="Label1" runat="server" Text="Vouchers"></asp:Label></h4>                       
                        <div class="col-md-12">
                            <div class="col-md-6 form-group">
                                <label>Vendor Type</label>
                                <asp:RadioButtonList ID="rbVendor" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rbVendor_SelectedIndexChanged">
                                    <asp:ListItem>Broker</asp:ListItem>
                                    <asp:ListItem>Patrol Pumps</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                            <div class="col-md-6 form-group">
                                <label>Payment Mode</label>
                                <asp:RadioButtonList ID="rbPaymentMode" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rbPaymentMode_SelectedIndexChanged">
                                    <asp:ListItem>Cash</asp:ListItem>
                                    <asp:ListItem>Bank</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                            <div class="col-md-6 form-group">
                                <label>Vendors</label>
                                <asp:DropDownList ID="ddlVendors" runat="server" CssClass="form-control" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="ddlVendors_SelectedIndexChanged">
                                    <asp:ListItem>-Select-</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-6 form-group">
                                <label>Amount</label>
                                <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>   
                            
                            <div class="col-md-2" id="BrokerAdvancePlaceholder" runat="server" visible="false">
                                <label>Broker Advance?</label>
                                <asp:CheckBox ID="cbAdvance" runat="server" AutoPostBack="true" OnCheckedChanged="cbAdvance_CheckedChanged" />
                            </div>
                            
                            <div class="col-md-5" id="ddlOrderplaceholder" runat="server" visible="false">
                                <label>Order</label>
                                <asp:DropDownList ID="ddlOrder" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlOrder_SelectedIndexChanged" Enabled="false"></asp:DropDownList>
                            </div>
                            
                            <div class="col-md-5" id="VehicleRegNoPlaceholder" runat="server" visible="false">
                                <label>Vehicle</label>
                                <asp:TextBox ID="txtVehicleRegNo" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>

                            <div class="col-md-12">
                                <div class="col-md-6 form-group" id="DocumentNoPlaceholder" runat="server" visible="false">
                                    <label>Document No.</label>
                                    <asp:TextBox ID="txtDocumentNo" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-6" id="bankAccountsPlaceholder" runat="server" visible="false">
                                    <label>Bank Accounts</label>
                                    <asp:DropDownList ID="ddlBankAccounts" runat="server" CssClass="form-control">
                                        <asp:ListItem>Cash</asp:ListItem>
                                        <asp:ListItem>Cheque</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div id="divPaymentNotification" runat="server"></div>
                            </div>
                            <div class="col-md-12">
                                <asp:LinkButton ID="lnkSavePayment" runat="server" ForeColor="Green" Font-Size="70px" OnClick="lnkSavePayment_Click"><i class="fas fa-check pull-left"></i></asp:LinkButton>
                                <asp:LinkButton ID="LinkButton3" runat="server" ForeColor="Red" Font-Size="70px"><i class="fas fa-times-circle pull-right"></i></asp:LinkButton>
                            </div>
                            
                            
                        </div>
                    </asp:Panel>

                    <ajaxToolkit:ModalPopupExtender ID="modalVoucherPrint" runat="server" PopupControlID="pnlVoucherPrint" DropShadow="True" TargetControlID="btnOpenVoucherPrint" 
                        CancelControlID="lnkCloseVoucherPrint" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
                    <asp:Panel ID="pnlVoucherPrint" runat="server" CssClass="row" style="background-color: white; padding: 20px; border: 1px solid black; height: 600px; overflow-y: scroll" Width="1300px">
                            
                        <asp:Button ID="btnOpenVoucherPrint" runat="server" style="display: none" />
                        <asp:LinkButton ID="lnkCloseVoucherPrint" runat="server" ForeColor="Maroon" CssClass="pull-right" style="display: none;"><i class="fa fa-times-circle-o"></i></asp:LinkButton>
                        <h4 class="pull-left"><asp:Label ID="Label8" runat="server"></asp:Label></h4> 
                        <asp:LinkButton ID="lnkCloseVoucherPrints" runat="server" ForeColor="Maroon" CssClass="pull-right" OnClick="lnkCloseVoucherPrints_Click"><i class="fas fa-times-circle"></i></asp:LinkButton>
                        <div id="div2" runat="server"></div>
                        <h2>Payment Voucher# <asp:Label ID="lblVoucherPrintNo" runat="server"></asp:Label></h2>
                                    
                        <table id="tblVoucher" runat="server" style="width: 50%" border="1" cellpadding="5">                            
                            <tr>
                                <td colspan="2" style="padding: 5px;">To: <asp:Label ID="lblVoucherPrintTo" runat="server"></asp:Label></td>
                                <td style="padding: 5px;">For : <asp:Label ID="lblVoucherPrintVehicleRegNo" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td colspan="2" style="padding: 5px;">Amount: <asp:Label ID="lblVoucherPrintAmount" runat="server"></asp:Label></td>
                                <td style="padding: 5px;">Date: <asp:Label ID="lblVoucherPrintDate" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td colspan="3" style="text-align: center;">
                                    <h4>Payment Method</h4>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 5px;">Cash: <asp:Label ID="lblVoucherPrintCashNo" runat="server"></asp:Label></td>
                                <td style="padding: 5px;">Bank: <asp:Label ID="lblVoucherPrintBank" runat="server"></asp:Label></td>
                                <td style="padding: 5px;">Cheque #: <asp:Label ID="lblVoucherPrintChequeNo" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td colspan="3" style="padding: 5px;">The sum of Rs: <asp:Label ID="lblVoucherPrintAmountInWorlds" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="padding: 5px; text-align: center;">Approved By</td>
                                <td style="padding: 5px; text-align: center;">Paid By</td>
                                <td style="padding: 5px; text-align: center;">Received By</td>
                            </tr>
                            <tr>
                                <td style="padding: 5px; text-align: center;"><asp:Label ID="lblVoucherPrintApprovedBy" runat="server"></asp:Label></td>
                                <td style="padding: 5px; text-align: center;"><asp:Label ID="lblVoucherPrintPaidBy" runat="server"></asp:Label></td>
                                <td style="padding: 5px; text-align: center;"><asp:Label ID="lblVoucherPrintReceivedBy" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="padding: 5px;"><br /><br /></td>
                                <td style="padding: 5px;"><br /><br /></td>
                                <td style="padding: 5px;"><br /><br /></td>
                            </tr>
                        </table>

                        <a href="#" onclick="Print(); return false;" class="btn btn-purple btn-md"><i class="fa fa-print"></i> &nbsp; Print </a>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <!-- MAIN CONTENT AREA ENDS -->
        </section>
    </section>
</asp:Content>
