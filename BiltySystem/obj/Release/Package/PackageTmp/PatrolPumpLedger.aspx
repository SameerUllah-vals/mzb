<%@ Page Title="" Language="C#" MasterPageFile="~/BiltySystem.Master" AutoEventWireup="true" CodeBehind="PatrolPumpLedger.aspx.cs" Inherits="BiltySystem.PatrolPumpLedger" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Patrol Pump Ledgers</title>
    <style>        
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }

        .list {
	        list-style-type: none;
	        background-color: #FFF;
            font-size: 10px;
            padding: 2px 5px;
            width: 100%;
        }

        .hoverlistitem {
		    background-color: #d3d3d3;
	    }
    </style>
    
    <script type="text/javascript">
        function Print() {            
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
                    let VendorNameString = document.getElementById('ContentPlaceHolder1_lblSingleLedgerAccountName').innerText.split('|');
                    let VendorName = VendorNameString[0];

                        InvoiceHTML += "<tr>";
                            InvoiceHTML += "<td colspan=\"2\" style=\"width: 14px;\">Vendor: </span> <strong>" + VendorName +"</strong></div></td>";
                            InvoiceHTML += "<td colspan=\"2\" style=\"width: 14px;\">Account Name: </span> <strong>" + document.getElementById('ContentPlaceHolder1_lblSingleLedgerAccountName').innerText.split('|') + "</strong></div></td>";
                        InvoiceHTML += "</tr>";
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
                    //for Opposite state of sort
                    //for (var i = (tableExpenses.rows.length - 1), row; row = tableExpenses.rows[i]; i--) {
                    for (var i = 0, row; row = tableExpenses.rows[i]; i++) {
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

                    <div class="col-xs-12" id="MainLedger" runat="server" visible="false">
                        <section class="box ">
                            <header class="panel_header">
                                <h2 class="title pull-left"><asp:LinkButton ID="lnkAllLedgers" runat="server" OnClick="lnkAllLedgers_Click" style="text-decoration: none;"><i class="fas fa-arrow-circle-left"></i> All Ledgers</asp:LinkButton></h2> 
                                <asp:Panel ID="pnlSearch" runat="server" DefaultButton="lnkSearchSingleAccount" CssClass="col-md-9 m-t-25 pull-right">
                                    <asp:LinkButton ID="lnkCancelSearchSingleAccount" runat="server" CssClass="pull-left m-t-30" ForeColor="Maroon" OnClick="lnkCancelSearchSingleAccount_Click" ToolTip="Click to clear search result"><i class="fa fa-times"></i></asp:LinkButton>
                                    <div class="form-group col-md-4">
                                        <label>From</label>
                                        <asp:TextBox ID="txtFrom" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label>To</label>
                                        <asp:TextBox ID="txtTo" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-3">
                                        <asp:LinkButton ID="lnkSearchSingleAccount" runat="server"  CssClass="pull-left btn btn-info m-t-25" OnClick="lnkSearchSingleAccount_Click"><i class="fa fa-search"></i></asp:LinkButton>
                                        <%--<asp:LinkButton ID="lnkPrint" runat="server" CssClass="pull-right m-t-40 m-l-10" OnClientClick="Print();" ToolTip="Click to Print Ledger"><i class="fa fa-print"></i></asp:LinkButton>--%>
                                        <asp:LinkButton ID="lnkPrintSingleLedger" runat="server" CssClass="pull-right m-t-40 m-l-10" OnClick="lnkPrintSingleLedger_Click" ToolTip="Click to Print Ledger"><i class="fa fa-print"></i></asp:LinkButton>
                                        
                                        <button class="btn btn-default dropdown-toggle" type="button" id="dropdownMenu1" data-toggle="dropdown" aria-expanded="true">
                                            Transaction
                                            <span class="caret"></span>
                                        </button>
                                        <ul class="dropdown-menu" role="menu" aria-labelledby="dropdownMenu1">
                                            <li role="presentation"><asp:LinkButton ID="lnkAddTransaction" runat="server" ToolTip="Click to transact in current ladger" OnClick="lnkAddTransaction_Click">Basic Transaction | <i class="fa fa-money"></i></asp:LinkButton></li>
                                            <li role="presentation"><asp:LinkButton ID="lnkAddVehicleFuel" runat="server" ToolTip="Click to Record vehicle refuelling" OnClick="lnkAddVehicleFuel_Click">Record Vehicle Refuelling | <i class="fas fa-gas-pump"></i></asp:LinkButton></li>
                                        </ul>
                                        
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
                                            CssClass="table table-hover" Font-Size="12px" Font-Names="Open Sans" DataKeyNames="AccountID, RecordedDate, DateCreated" BackColor="White" OnRowDataBound="gvResult_RowDataBound">
                                            <Columns>
                                                <asp:BoundField DataField="AccountID" HeaderText="Ref. #" ItemStyle-Width="10%" ItemStyle-Wrap="false"></asp:BoundField>
                                                <asp:TemplateField HeaderText="Date" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <%--<asp:Label ID="lblDate" runat="server" Text='<%# Eval("RecordedDate").ToString() == string.Empty ? Eval("DateCreated") : Eval("RecordedDate") %>' ForeColor="Black"></asp:Label>--%>
                                                        <asp:Label ID="lblDate" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField DataField="RecordedDate" HeaderText="Date" ItemStyle-Width="20%" ItemStyle-Wrap="false"></asp:BoundField>--%>
                                                <asp:BoundField DataField="Item" HeaderText="Item" ItemStyle-Width="50%" ItemStyle-Wrap="false"></asp:BoundField>
                                                <asp:BoundField DataField="Debit" HeaderText="Debit" ItemStyle-Width="10%" ItemStyle-Wrap="false"></asp:BoundField>
                                                <asp:BoundField DataField="Credit" HeaderText="Credit" ItemStyle-Width="10%" ItemStyle-Wrap="false"></asp:BoundField>
                                                <asp:BoundField DataField="Balance" HeaderText="Balance" ItemStyle-Width="10%" ItemStyle-Wrap="false"></asp:BoundField>
                                            </Columns>
                                            <HeaderStyle BackColor="#4b23dd" ForeColor="White" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </section>
                    </div>

                    <div class="col-xs-12" id="AllAcounts" runat="server">
                        <section class="box ">
                            <header class="panel_header">
                                <asp:LinkButton ID="lnkAddNewAccount" runat="server"  CssClass="pull-left btn btn-primary m-t-25" OnClick="lnkAddNewAccount_Click"><i class="fa fa-plus-square"></i></asp:LinkButton>
                                <h2 class="title pull-left">Patrol Pumps Ledger</h2> 
                                <asp:Panel ID="Panel1" runat="server" DefaultButton="lnkSearch" CssClass="col-md-6 m-t-25 pull-right">
                                    
                                    <asp:LinkButton ID="lnkCancelSearch" runat="server" OnClick="lnkCancelSearch_Click"><i class="fa fa-times-circle m-t-10 m-r-5 pull-left" style="color: maroon;"></i></asp:LinkButton>

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
                                        <asp:HiddenField ID="hfSelectedAccount" runat="server" />
                                        <asp:GridView ID="gvAllPatrolPumpsAcounts" runat="server" Width="100%" EmptyDataText="No Account found" AutoGenerateColumns="false"
                                            CssClass="table table-hover" Font-Size="12px" Font-Names="Open Sans" DataKeyNames="PatrolPumpID" BackColor="White" 
                                            OnRowDataBound="gvAllPatrolPumpsAcounts_RowDataBound" HeaderStyle-HorizontalAlign="Center" OnRowCommand="gvAllPatrolPumpsAcounts_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Account" ItemStyle-Width="70%">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkAccount" runat="server" style="text-decoration:none;" Text='<%# Eval("TABLE_NAME") %>' CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="Account"></asp:LinkButton>
                                                    </ItemTemplate>                                                    
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="30%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderText="Balance">
                                                    <ItemTemplate>
                                                        <%--String.Format("{0:n}", Debit)--%>
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
                    </div>


                    

                    


                    <asp:Panel class="col-xs-12" ID="pnlPrintAllAccounts" runat="server" style="display:none">
                        <section class="box ">
                            <header class="panel_header">
                                <h2 class="title pull-left">Print All Ledgers</h2>                                 
                                <asp:LinkButton ID="lnkCloseAllAccountPrint" runat="server" OnClick="lnkCloseAllAccountPrint_Click" CssClass="btn btn-danger pull-right m-t-10 m-r-10"><i class="fa fa-times"></i></asp:LinkButton>                               
                            </header>
                            <div class="content-body">
                                <div class="row">
                                    <div class="col-xs-12">
                                        
                                        <%-- SSRS Report --%>
                                          <rsweb:reportviewer ID="rvPetrolPump" runat="server" Width="100%" Height="800px"></rsweb:reportviewer>
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
                                          <rsweb:reportviewer ID="rvAllPetrolPumpLedger" runat="server" Width="100%" Height="800px" ></rsweb:reportviewer>
                                         <%-- SSRS Report --%>

                                    </div>
                                </div>
                            </div>
                        </section>
                    </asp:Panel>

                    <ajaxToolkit:ModalPopupExtender ID="modalAddLedger" runat="server" PopupControlID="pnlAddLedger" DropShadow="True" TargetControlID="lnkOpenAddLedger" 
                        CancelControlID="lnkCloseAddLedger" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
                    <asp:Panel ID="pnlAddLedger" runat="server" CssClass="box " Width="600px" DefaultButton="lnkSavePatrolPumpLedger">
                        <header class="panel_header">
                            <h2 class="title pull-left">Add Patrol Pump Account</h2>
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
                                        <label class="form-label" for="email-1">Patrol Pump</label>
                                        <asp:DropDownList ID="ddlPatrolPump" runat="server" CssClass="form-control"></asp:DropDownList>
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
                                        <label class="form-label" for="email-1">Opening Date</label>
                                        <asp:TextBox ID="txtOpeningDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-12 col-sm-12 col-xs-12">
                                    <div class="form-group">
                                        <asp:LinkButton ID="lnkSavePatrolPumpLedger" runat="server" CssClass="btn btn-info m-t-30" ToolTip="Click to Save PatrolPump ledger" OnClick="lnkSavePatrolPumpLedger_Click"><i class="fas fa-save"></i> | Save</asp:LinkButton>
                                    </div>
                                </div>
                            </div>                            
                            <div id="divAddLedgerNotification" runat="server"></div>
                        </div>
                    </asp:Panel>

                    <ajaxToolkit:ModalPopupExtender ID="modalVehicleFuel" runat="server" PopupControlID="pnlVehicleFuel" DropShadow="True" TargetControlID="lnkOpenAddVehicleFuel" 
                        CancelControlID="lnkCloseVehicleFuel" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
                    <asp:Panel ID="pnlVehicleFuel" runat="server" CssClass="box " Width="600px" DefaultButton="lnkSaveTransaction">
                        <header class="panel_header">
                            <h2 class="title pull-left">Transaction</h2>
                            <div class="actions panel_actions pull-right">
                                <asp:LinkButton ID="lnkOpenAddVehicleFuel" runat="server" CssClass="box_close fa fa-times" style="display:none;"></asp:LinkButton>
                                <asp:LinkButton ID="lnkCloseVehicleFuel" runat="server" CssClass="box_close fa fa-times" style="display:none;"></asp:LinkButton>
                                <asp:LinkButton ID="lnkCloseVehicleFuels" runat="server" CssClass="btn btn-danger btn-xs m-t-15 m-r-5" style="margin-right: 5px;" OnClick="lnkCloseVehicleFuels_Click"><i class="fa fa-times-circle"></i></asp:LinkButton>
                            </div>
                        </header>
                        <div class="content-body">
                            <div class="row">
                                
                                <div class="col-md-6 col-sm-12 col-xs-12">
                                    <div class="form-group">
                                        <label class="form-label" for="email-1">Refuelling Date</label>
                                        <asp:TextBox ID="txtRefuellingDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    </div>
                                </div>
                                
                                <div class="col-md-6 col-sm-12 col-xs-12">
                                    <div class="form-group">
                                        <label class="form-label" for="email-1">Vehicle Reg. No.</label>
                                        <asp:TextBox ID="txtVehicleRegNo" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                
                                <div class="col-md-6 col-sm-12 col-xs-12">
                                    <div class="form-group">
                                        <label class="form-label" for="email-1">Rate / Ltr</label>
                                        <asp:TextBox ID="txtRatePerLitre" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6 col-sm-12 col-xs-12">
                                    <div class="form-group">
                                        <label class="form-label" for="email-1">Amount</label>
                                        <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6 col-sm-12 col-xs-12">
                                    <div class="form-group">
                                        <label class="form-label" for="email-1">Litres</label>
                                        <asp:TextBox ID="txtLitres" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-6 col-sm-12 col-xs-12">
                                    <div class="form-group">
                                        <label class="form-label" for="email-1">By</label>
                                        <asp:TextBox ID="txtVehicleFuelBy" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                
                                <div class="col-md-12 col-sm-12 col-xs-12">
                                    <div class="form-group">
                                        <label class="form-label" for="email-1">For</label>
                                        <asp:TextBox ID="txtVehicleFuelFor" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                                
                                
                                <div class="col-md-12 col-sm-12 col-xs-12">
                                    <div class="form-group">
                                        <asp:LinkButton ID="lnkSaveVehicleFuel" runat="server" CssClass="btn btn-info m-t-30" ToolTip="Click to save Vehicle Fuel" OnClick="lnkSaveVehicleFuel_Click"><i class="fas fa-save"></i> | Save</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            
                            <div id="divVehicleFuelNotification" runat="server"></div>
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
