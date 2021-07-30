<%@ Page Title="" Language="C#" MasterPageFile="~/BiltySystem.Master" AutoEventWireup="true" CodeBehind="BankLedgers.aspx.cs" Inherits="BiltySystem.BankLedgers" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Bank Ledgers</title>
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
    <script type="text/javascript">
        function Print() {
            debugger;
            
            var InvoiceHTML = "";
            InvoiceHTML += "<style>";
                InvoiceHTML += ".clearfix: after {";
                  InvoiceHTML += "content: \"\";";
                  InvoiceHTML += "display: table;";
                  InvoiceHTML += "clear: both;";
                InvoiceHTML += "}";

                InvoiceHTML += "a {";
                  InvoiceHTML += "color: #5D6975;";
                  InvoiceHTML += "text-decoration: underline;";
                InvoiceHTML += "}";

                InvoiceHTML += "body {";
                  InvoiceHTML += "position: relative;";
                  InvoiceHTML += "width: 21cm;  ";
                  InvoiceHTML += "height: 29.7cm; ";
                  InvoiceHTML += "margin: 0 auto; ";
                  InvoiceHTML += "color: #001028;";
                  InvoiceHTML += "background: #FFFFFF; ";
                  InvoiceHTML += "font-family: Calibri; ";
                  InvoiceHTML += "font-size: 12px; ";
                  InvoiceHTML += "font-family: Arial;";
                InvoiceHTML += "}";

                InvoiceHTML += "header {";
                  InvoiceHTML += "padding: 10px 0;";
                  InvoiceHTML += "margin-bottom: 30px;";
                InvoiceHTML += "}";

                InvoiceHTML += "#logo {";
                    InvoiceHTML += "text-align: center;";
                    InvoiceHTML += "margin-bottom: 10px;";
                    InvoiceHTML += "border-bottom: 1px solid #C1CED9;";
                InvoiceHTML += "}";

                InvoiceHTML += "#logo img {";
                  InvoiceHTML += "width: 90px;";
                InvoiceHTML += "}";

                InvoiceHTML += "h1 {";
                  InvoiceHTML += "border-top: 1px solid  #5D6975;";
                  InvoiceHTML += "border-bottom: 1px solid  #5D6975;";
                  InvoiceHTML += "color: #5D6975;";
                  InvoiceHTML += "font-size: 2.4em;";
                  InvoiceHTML += "line-height: 1.4em;";
                  InvoiceHTML += "font-weight: normal;";
                  InvoiceHTML += "text-align: center;";
                  InvoiceHTML += "margin: 0 0 20px 0;";
                  InvoiceHTML += "background: url(dimension.png);";
                InvoiceHTML += "}";

                InvoiceHTML += "#project {";
                  InvoiceHTML += "float: left;";
                InvoiceHTML += "}";

                InvoiceHTML += "#project span {";
                  InvoiceHTML += "color: #5D6975;";
                  InvoiceHTML += "text-align: right;";
                  InvoiceHTML += "width: 52px;";
                  InvoiceHTML += "margin-right: 10px;";
                  InvoiceHTML += "display: inline-block;";
                  InvoiceHTML += "font-size: 0.8em;";
                InvoiceHTML += "}";

                InvoiceHTML += "#company {";
                  InvoiceHTML += "float: right;";
                  InvoiceHTML += "text-align: right;";
                InvoiceHTML += "}";

                InvoiceHTML += "#project div,";
                InvoiceHTML += "#company div {";
                  InvoiceHTML += "white-space: nowrap;";    
                InvoiceHTML += "}";

                InvoiceHTML += ".tblDesc {";
                  InvoiceHTML += "width: 100%;";
                  InvoiceHTML += "border-collapse: collapse;";
                  InvoiceHTML += "border-spacing: 0;";
                  InvoiceHTML += "margin-bottom: 20px;";
                InvoiceHTML += "}";

                InvoiceHTML += ".tblDesc tr:nth-child(2n-1) td {";
                  InvoiceHTML += "background: #F5F5F5;";
                InvoiceHTML += "}";

                InvoiceHTML += ".tblDesc th,";
                InvoiceHTML += ".tblDesc td {";
                    InvoiceHTML += "text-align: center;";
                    InvoiceHTML += "border: 1px solid black;";
                    //InvoiceHTML += "pading: 10px";
                    //InvoiceHTML += "font-size: 13px";
                InvoiceHTML += "}";

                InvoiceHTML += ".tblDesc th {";
                    InvoiceHTML += "padding: 15px";
                    InvoiceHTML += "color: #5D6975;";
                    InvoiceHTML += "border-bottom: 1px solid #C1CED9;";
                    InvoiceHTML += "white-space: nowrap;";
                    InvoiceHTML += "font-weight: Bold;";
                    InvoiceHTML += "font-size: 18px;";
                InvoiceHTML += "}";

                InvoiceHTML += ".tblDesc .service,";
                InvoiceHTML += ".tblDesc .desc {";
                  InvoiceHTML += "text-align: left;";
                InvoiceHTML += "}";

                InvoiceHTML += "tblDesc td {";
                  InvoiceHTML += "padding: 20px;";
                  InvoiceHTML += "text-align: right;";
                InvoiceHTML += "}";

                InvoiceHTML += ".tblDesc td.service,";
                InvoiceHTML += ".tblDesc td.desc {";
                  InvoiceHTML += "vertical-align: top;";
                InvoiceHTML += "}";

                InvoiceHTML += ".tblDesc td.unit,";
                InvoiceHTML += ".tblDesc td.qty,";
                InvoiceHTML += ".tblDesc td.total {";
                  InvoiceHTML += "font-size: 1.2em;";
                InvoiceHTML += "}";

                InvoiceHTML += ".tblDesc td.grand {";
                  InvoiceHTML += "border-top: 1px solid #5D6975;;";
                InvoiceHTML += "}";

                InvoiceHTML += "#notices .notice {";
                  InvoiceHTML += "color: #5D6975;";
                  InvoiceHTML += "font-size: 1.2em;";
                InvoiceHTML += "}";

                InvoiceHTML += "footer {";
                  InvoiceHTML += "color: #5D6975;";
                  InvoiceHTML += "width: 100%;";
                  InvoiceHTML += "height: 30px;";
                  InvoiceHTML += "position: absolute;";
                  InvoiceHTML += "bottom: 5;";
                  InvoiceHTML += "border-top: 1px solid #C1CED9;";
                  InvoiceHTML += "padding: 8px 0;";
                  InvoiceHTML += "text-align: center;";
                InvoiceHTML += "}";
                InvoiceHTML += "</style > ";
            InvoiceHTML += "<header class=\"clearfix\">";
                InvoiceHTML += "<div id=\"logo\ style=\"width: 100%; border-top: 1px solid #C1CED9;\">";
                    InvoiceHTML += "<img src=\"assets/images/MZBLogo.png\" style=\"width: 25%;\">";
                    InvoiceHTML += "<img src=\"assets/images/MZBLogo2.png\" style=\"width: 43%; float: right;\">";
                InvoiceHTML += "</div>";
                InvoiceHTML += "<br>";
                InvoiceHTML += "<div class=\"clearfix\" style=\"width: 100%;\">";
                    InvoiceHTML += "<br><br><br>";
            InvoiceHTML += "<table style=\"width: 100%;\">";
                    let BankNameString = document.getElementById('ContentPlaceHolder1_lblSingleLedgerAccountName').innerText.split('|');
                    let BankName = BankNameString[0];

                        InvoiceHTML += "<tr>";
                            InvoiceHTML += "<td colspan=\"2\" style=\"width: 14px;\">Bank: </span> <strong>" + BankName +"</strong></div></td>";
                            InvoiceHTML += "<td colspan=\"2\" style=\"width: 14px;\">Account Name: </span> <strong>" + document.getElementById('ContentPlaceHolder1_lblSingleLedgerAccountName').innerText.split('|') + "</strong></div></td>";
                        InvoiceHTML += "</tr>";
            
                        InvoiceHTML += "<tr>";
                            InvoiceHTML += "<td colspan=\"2\" style=\"padding: 10px; style=\"width: 14px;\"\"><span>Account#</span> <strong>" + document.getElementById('ContentPlaceHolder1_lblAccountNo').innerText.split('|') + "</strong></td>";
                            InvoiceHTML += "<td colspan=\"2\" style=\"padding: 10px; style=\"width: 14px;\"\"><span>Account Title</span> <strong>" + document.getElementById('ContentPlaceHolder1_lblAccountTitle').innerText.split('|') + "</strong></td>";
                            //InvoiceHTML += "<td style=\"padding: 10px;\"><span>Statement Period</span> <strong>02-Sep-2019 to 02-Sep-2019</strong></div></td>";
                        InvoiceHTML += "</tr>";
            
                        //InvoiceHTML += "<tr>";
                        //    InvoiceHTML += "<td colspan=\"3\" style=\"padding: 10px;\"><span>Receiver</span> <strong>" + document.getElementById('ContentPlaceHolder1_lblReceiverCompany').innerText +"</strong></div></td>";
                        //    InvoiceHTML += "<td colspan=\"2\" style=\"padding: 10px;\"><span>Vehicle Contact</span> <strong>" + document.getElementById('ContentPlaceHolder1_lblContact').innerText +"</strong></td>";
                        //InvoiceHTML += "</tr>";
                    InvoiceHTML += "</table>";
                InvoiceHTML += "</div>";
            InvoiceHTML += "</header>";
            InvoiceHTML += "<main>";
                InvoiceHTML += "<table border\"1\" class=\"tblDesc\" style=\"font-size: 12px;\">";
                    InvoiceHTML += "<thead>";
                        InvoiceHTML += "<tr>";
                            InvoiceHTML += "<th style=\"padding: 10px; width: 20%; font-size: 14px;\">Date</th>";
                            InvoiceHTML += "<th style=\"padding: 10px; width: 50%; font-size: 14px;\">Description</th>";
                            InvoiceHTML += "<th style=\"padding: 10px; width: 10%; font-size: 14px;\">Debit</th>";
                            InvoiceHTML += "<th style=\"padding: 10px; width: 10%; font-size: 14px;\">Credit</th>";
                            InvoiceHTML += "<th style=\"padding: 10px; width: 10%; font-size: 14px;\">Balance</th>";
                        InvoiceHTML += "</tr>"
                    InvoiceHTML += "</thead>";
                    InvoiceHTML += "<tbody>";
            
                    var tableExpenses = document.getElementById("ContentPlaceHolder1_gvResult");
                    for (var i = (tableExpenses.rows.length - 1), row; row = tableExpenses.rows[i]; i--) {
                        var items = row.cells;
                        if (i != 0) {
                            InvoiceHTML += "<tr>";
                                InvoiceHTML += "<td>" + items.item(0).innerHTML + "</td>";
                                InvoiceHTML += "<td style=\"text-align: left;\">" + items.item(1).innerHTML + "</td>";
                                InvoiceHTML += "<td>" + items.item(2).innerHTML + "</td>";
                                InvoiceHTML += "<td>" + items.item(3).innerHTML + "</td>";
                                InvoiceHTML += "<td>" + items.item(4).innerHTML + "</td>";
                            InvoiceHTML += "</tr>";
                        }
                    }
                    InvoiceHTML += "</tbody>";
                InvoiceHTML += "</table>";
            //InvoiceHTML += "<br>";
                //InvoiceHTML += "<div id=\"notices\">";
                //    InvoiceHTML += "<div>NOTICE:</div>";
                //    InvoiceHTML += "<div class=\"notice\">Contact transportation company or broker in case of any debris.</div>";
                //InvoiceHTML += "</div>";
            //InvoiceHTML += "<br><br>";
                //InvoiceHTML += "<div id=\"notices\" style=\"text-align: right;\">";
                //    InvoiceHTML += "<div>Receiver Signature:</div>";
                //    InvoiceHTML += "<br><br><br> _________________________________";
                //InvoiceHTML += "</div>";
        InvoiceHTML += "</main>";
    //InvoiceHTML += "<footer>";
    //  InvoiceHTML += "Invoice was created on a computer and is valid without the signature and seal.";
    //InvoiceHTML += "</footer>";
            
            var prntWindow = window.open("", "Print", "width=900,height=900,left=0,top=0,toolbar=0,scrollbar=1,status=0");
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
                    <asp:Panel ID="pnlAllSearch" runat="server" DefaultButton="lnkSearch" CssClass="col-xs-12" Visible="false">
                           <section class="box ">  

                            <asp:LinkButton ID="lnkCloseSearch" runat="server" OnClick="lnkCloseSearch_Click" CssClass=" fa fa-times-circle pull-right" style="margin-top: 10px; margin-right: 10px;"></asp:LinkButton>
                            <div class="content-body">
                                <div class="row">
                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                        <div class="row">
                                    <div class="form-group col-md-3">
                                        <label>From</label>
                                        <asp:TextBox ID="txtFrom" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-3">
                                        <label>To</label>
                                        <asp:TextBox ID="txtTo" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    </div>
                                             <div class="form-group col-md-3">
                                        <label>Cheque Number</label>
                                        <asp:TextBox ID="txtChequeNumber" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-3">
                                        <asp:LinkButton ID="lnkAllSearch" runat="server"  CssClass="pull-left btn btn-info m-t-30" OnClick="lnkAllSearch_Click"><i class="fa fa-search"></i></asp:LinkButton>
                                       
                                    </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                               </section>
                                </asp:Panel>

                    <asp:Panel CssClass="col-xs-12" ID="PnlMainLedger" runat="server" Visible="false">
                        <section class="box ">
                            <header class="panel_header">
                                <h2 class="title pull-left"><asp:LinkButton ID="lnkAllLedgers" runat="server" OnClick="lnkAllLedgers_Click" style="text-decoration: none;"><i class="fas fa-arrow-circle-left"></i> All Ledgers</asp:LinkButton></h2> 
                                <asp:Panel ID="pnlSearch" runat="server" DefaultButton="lnkSearch" CssClass="col-md-9 m-t-25 pull-right">
                                    <%--<asp:LinkButton ID="lnkCancelSearchSingleAccount" runat="server" CssClass="pull-left m-t-30" ForeColor="Maroon" OnClick="lnkCancelSearchSingleAccount_Click" ToolTip="Click to clear search result"><i class="fa fa-times"></i></asp:LinkButton>--%>
                                    <%--<div class="form-group col-md-4">
                                        <label>From</label>
                                        <asp:TextBox ID="txtFrom" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label>To</label>
                                        <asp:TextBox ID="txtTo" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    </div>--%>
                                    <div class="form-group col-md-3 pull-right">
                                        <asp:LinkButton ID="lnkSearchSingleAccount" runat="server"  CssClass="pull-left btn btn-info m-t-30" OnClick="lnkSearchSingleAccount_Click"><i class="fa fa-search"></i></asp:LinkButton>
                                        <%--<asp:LinkButton ID="lnkPrint" runat="server" CssClass="pull-right m-t-40 m-l-10" OnClientClick="Print();" ToolTip="Click to Print Ledger"><i class="fa fa-print"></i></asp:LinkButton>--%>
                                        <asp:LinkButton ID="lnkPrintSingleLedger" runat="server" CssClass="pull-right m-t-40 m-l-10" OnClick="lnkPrintSingleLedger_Click" ToolTip="Click to Print Ledger"><i class="fa fa-print"></i></asp:LinkButton>
                                        <asp:LinkButton ID="lnkAddTransaction" runat="server" CssClass="pull-right m-t-30 btn btn-primary" ToolTip="Click to transact in current ladger" OnClick="lnkAddTransaction_Click"><i class="fa fa-plus-square"></i></asp:LinkButton>
                                    </div>
                                </asp:Panel>
                                <h1 class="title pull-left"><asp:Label ID="lblSingleLedgerAccountName" runat="server"></asp:Label></h1> 
                                <asp:Label ID="lblAccountNo" runat="server" style="display: none;"></asp:Label>
                                <asp:Label ID="lblAccountTitle" runat="server" style="display: none;"></asp:Label>
                            </header>
                            <div class="content-body">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <asp:GridView ID="gvResult" runat="server" Width="100%" EmptyDataText="No Record found" AutoGenerateColumns="false"
                                            CssClass="table table-hover" Font-Size="12px" Font-Names="Open Sans" DataKeyNames="AccountID" BackColor="White" OnRowDataBound="gvResult_RowDataBound">
                                            <Columns>
                                                <asp:BoundField DataField="AccountID" HeaderText="Ref. #" ItemStyle-Width="6%" ItemStyle-Wrap="false"></asp:BoundField>
                                                <asp:BoundField DataField="Date" HeaderText="Date" ItemStyle-Width="20%" ItemStyle-Wrap="false"></asp:BoundField>
                                                <asp:BoundField DataField="RecordedDate" HeaderText="Transaction Date" ItemStyle-Width="20%" ItemStyle-Wrap="false"></asp:BoundField>
                                                <asp:BoundField DataField="Item" HeaderText="Item" ItemStyle-Width="50%"></asp:BoundField>
                                                <asp:BoundField DataField="Debit" HeaderText="Debit" ItemStyle-Width="8%" ItemStyle-Wrap="false"></asp:BoundField>
                                                <asp:BoundField DataField="Credit" HeaderText="Credit" ItemStyle-Width="8%" ItemStyle-Wrap="false"></asp:BoundField>
                                                <asp:BoundField DataField="Balance" HeaderText="Balance" ItemStyle-Width="8%" ItemStyle-Wrap="false"></asp:BoundField>
                                            </Columns>
                                            <HeaderStyle BackColor="#4b23dd" ForeColor="White" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </section>
                    </asp:Panel>

                    <asp:Panel CssClass="col-xs-12" ID="PnlAllAcounts" runat="server">
                        <section class="box ">
                            <header class="panel_header">
                                <asp:LinkButton ID="lnkAddNewAccount" runat="server"  CssClass="pull-left btn btn-primary m-t-25" OnClick="lnkAddNewAccount_Click"><i class="fa fa-plus-square"></i></asp:LinkButton>
                                <h2 class="title pull-left">Bank Ledger</h2> 
                                <asp:Panel ID="Panel1" runat="server" DefaultButton="lnkSearch" CssClass="col-md-6 m-t-25 pull-right">
                                    <asp:LinkButton ID="lnkCancelSearch" runat="server"><i class="fa fa-times-circle m-t-10 m-r-5 pull-left" style="color: maroon;"></i></asp:LinkButton>
                                    <asp:TextBox ID="txtSearch" runat="server" class="form-control pull-left" placeholder="Search Consigner/Sender" Width="80%"></asp:TextBox>
                                    <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchCompanies"
                                        MinimumPrefixLength="2"
                                        CompletionListCssClass="list" 
	                                    CompletionListItemCssClass="listitem" 
	                                    CompletionListHighlightedItemCssClass="hoverlistitem"
                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                        TargetControlID="txtSearch"
                                        ID="AutoCompleteExtender2" runat="server" FirstRowSelected="false">
                                    </ajaxToolkit:AutoCompleteExtender>
                                    <asp:LinkButton ID="lnkSearch" runat="server"  CssClass="pull-left btn btn-info" OnClick="lnkSearch_Click"><i class="fa fa-search"></i></asp:LinkButton>
                                    <asp:LinkButton ID="lnkPrintAllAccounts" runat="server" CssClass="btn btn-primary m-l-10" OnClick="lnkPrintAllAccounts_Click"><i class="fas fa-print"></i></asp:LinkButton>

                                </asp:Panel>
                                <div class="actions panel_actions pull-right">

                                </div>
                            </header>
                            <div class="content-body">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <asp:GridView ID="gvAllBanksAcounts" runat="server" Width="100%" EmptyDataText="No Account found" AutoGenerateColumns="false"
                                            CssClass="table table-hover" Font-Size="12px" Font-Names="Open Sans" DataKeyNames="BankID" BackColor="White" 
                                            HeaderStyle-HorizontalAlign="Center" OnRowDataBound="gvAllBanksAcounts_RowDataBound" OnRowCommand="gvAllBankAcounts_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Account" ItemStyle-Width="70%">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkAccount" runat="server" style="text-decoration:none;" Text='<%# Eval("TABLE_NAME") %>' CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="Account"></asp:LinkButton>
                                                    </ItemTemplate>                                                    
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="30%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderText="Balance">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBalance" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle BackColor="#4b23dd" ForeColor="White" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </section>
                    </asp:Panel>

                    


                    <asp:Panel class="col-xs-12" ID="pnlPrintAllAccounts" runat="server" style="display:none;">
                        <section class="box ">
                            <header class="panel_header">
                                <h2 class="title pull-left">Print All Ledgers</h2>                                 
                                <asp:LinkButton ID="lnkCloseAllAccountPrint" runat="server" OnClick="lnkCloseAllAccountPrint_Click" CssClass="btn btn-danger pull-right m-t-10 m-r-10"><i class="fa fa-times"></i></asp:LinkButton>                               
                            </header>
                            <div class="content-body">
                                <div class="row">
                                    <div class="col-xs-12">
                                        
                                         <%-- SSRS Report --%>
                                          <rsweb:reportviewer ID="rvBankLedger" runat="server" Width="100%" Height="800px" ></rsweb:reportviewer>
                                         <%-- SSRS Report --%>

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
                                          <rsweb:reportviewer ID="rvAllBankLedger" runat="server" Width="100%" Height="800px" ></rsweb:reportviewer>
                                         <%-- SSRS Report --%>
                                    </div>
                                </div>
                            </div>
                        </section>
                    </asp:Panel>

                    <ajaxToolkit:ModalPopupExtender ID="modalAddLedger" runat="server" PopupControlID="pnlAddLedger" DropShadow="True" TargetControlID="lnkOpenAddLedger" 
                        CancelControlID="lnkCloseAddLedger" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
                    <asp:Panel ID="pnlAddLedger" runat="server" CssClass="box " Width="600px" DefaultButton="lnkSaveBankLedger">
                        <header class="panel_header">
                            <h2 class="title pull-left">Add Bank Account</h2>
                            <div class="actions panel_actions pull-right">
                                <asp:LinkButton ID="lnkOpenAddLedger" runat="server" CssClass="box_close fa fa-times" style="display:none;"></asp:LinkButton>
                                <asp:LinkButton ID="lnkCloseAddLedger" runat="server" CssClass="box_close fa fa-times" style="display:none;"></asp:LinkButton>
                                <asp:LinkButton ID="lnkCloseAddLedgers" runat="server" CssClass="btn btn-danger btn-xs m-t-15 m-r-5" style="margin-right: 5px;" OnClick="lnkCloseAddLedgers_Click"><i class="fa fa-times-circle"></i></asp:LinkButton>
                            </div>
                        </header>
                        <div class="content-body">
                            <div class="row">
                                
                                <div class="col-md-12 col-sm-12 col-xs-12">
                                    <div class="form-group">
                                        <label class="form-label" for="email-1">Bank</label>
                                        <asp:DropDownList ID="ddlBanks" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                
                                <div class="col-md-6 col-sm-12 col-xs-12">
                                    <div class="form-group">
                                        <label class="form-label" for="email-1">Opening Balance</label>
                                        <asp:TextBox ID="txtOpeningBalance" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                    </div>
                                </div>
                                
                                <div class="col-md-6 col-sm-12 col-xs-12">
                                    <div class="form-group">
                                        <label class="form-label" for="email-1">Date</label>
                                        <asp:TextBox ID="txtRecordedDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-12 col-sm-12 col-xs-12">
                                    <div class="form-group">
                                        <asp:LinkButton ID="lnkSaveBankLedger" runat="server" CssClass="btn btn-info m-t-30" ToolTip="Click to Save bank ledger" OnClick="lnkSaveBankLedger_Click"><i class="fas fa-save"></i> | Save</asp:LinkButton>
                                    </div>
                                </div>
                            </div>                            
                            <div id="divAddLedgerNotification" runat="server"></div>
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
                                
                                
                                <div class="col-md-5 col-sm-12 col-xs-12">
                                    <div class="form-group">
                                        <label class="form-label" for="email-1">Date</label>
                                        <asp:TextBox ID="txtTransactionRecordedDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-7 col-sm-12 col-xs-12">
                                    <div class="form-group">
                                        <label class="form-label" for="email-1">For</label>
                                        <asp:TextBox ID="txtTransactedFor" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
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

                </ContentTemplate>
            </asp:UpdatePanel>
            <!-- MAIN CONTENT AREA ENDS -->
        </section>
    </section>
    <!-- END CONTENT -->

</asp:Content>
