<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/Reports.Master" AutoEventWireup="true" CodeBehind="Drivers.aspx.cs" Inherits="BiltySystem.Reports.Drivers" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Drivers Report</title>
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
            z-index: 11111111111111;
        }

        .hoverlistitem {
		    background-color: #d3d3d3;
	    }

        .heading {
            font-weight: bold;
        }
    </style>
    <script src="../assets/js/jquery-1.11.2.min.js"></script>

    <script type="text/javascript">
        

        function Print() {
            debugger;
            
            var InvoiceHTML = "";

            var PaidToPay = document.getElementById('ContentPlaceHolder1_lblPaidtoPay').innerText;
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
                  InvoiceHTML += "bottom: 0;";
                  InvoiceHTML += "border-top: 1px solid #C1CED9;";
                  InvoiceHTML += "padding: 8px 0;";
                  InvoiceHTML += "text-align: center;";
                InvoiceHTML += "}";
                InvoiceHTML += "</style > ";
            InvoiceHTML += "<header class=\"clearfix\">";
      InvoiceHTML += "<div id=\"logo\ style=\"width: 100%; border-top: 1px solid #C1CED9;\">";
            InvoiceHTML += "<img src=\"../assets/images/MZBLogo.png\" style=\"width: 25%;\">";
            InvoiceHTML += "<img src=\"../assets/images/MZBLogo2.png\" style=\"width: 43%; float: right;\">";
        InvoiceHTML += "</div>";
        InvoiceHTML += "<br>";
            InvoiceHTML += "<div class=\"clearfix\" style=\"width: 100%;\">";
            InvoiceHTML += "<br><br><br>";
            InvoiceHTML += "<table style=\"width: 100%;\">";
            InvoiceHTML += "<tr>";
            InvoiceHTML += "<td style=\"padding: 10px;\"><span>Bilty#</span> <strong>" + document.getElementById('ContentPlaceHolder1_lblBiltyNo').innerText +"</strong></div></td>";
            InvoiceHTML += "<td style=\"padding: 10px;\"><span>Date#</span> <strong>" + document.getElementById('ContentPlaceHolder1_lblBiltyDate').innerText +"</strong></td>";
            InvoiceHTML += "<td style=\"padding: 10px;\">Truck</span> <strong>" + document.getElementById('ContentPlaceHolder1_lblVehicleRegNo').innerText +"</strong></td>";
            InvoiceHTML += "<td style=\"padding: 10px;\">From</span> <strong>" + document.getElementById('ContentPlaceHolder1_lblFrom').innerText +"</strong></td>";
            InvoiceHTML += "<td style=\"padding: 10px;\"><span>To</span> <strong>" + document.getElementById('ContentPlaceHolder1_lblTo').innerText +"</strong></td>";
            InvoiceHTML += "</tr>";
            
            InvoiceHTML += "<tr>";
            InvoiceHTML += "<td colspan=\"3\" style=\"padding: 10px;\"><span>Sender</span> <strong>" + document.getElementById('ContentPlaceHolder1_lblSenderCompany').innerText +"</strong></div></td>";
            InvoiceHTML += "<td colspan=\"2\" style=\"padding: 10px;\"><span>Broker</span> <strong>" + document.getElementById('ContentPlaceHolder1_lblBroker').innerText +"</strong></td>";
            InvoiceHTML += "</tr>";
            
            InvoiceHTML += "<tr>";
            InvoiceHTML += "<td colspan=\"3\" style=\"padding: 10px;\"><span>Receiver</span> <strong>" + document.getElementById('ContentPlaceHolder1_lblReceiverCompany').innerText +"</strong></div></td>";
            InvoiceHTML += "<td colspan=\"2\" style=\"padding: 10px;\"><span>Vehicle Contact</span> <strong>" + document.getElementById('ContentPlaceHolder1_lblContact').innerText +"</strong></td>";
            InvoiceHTML += "</tr>";
            InvoiceHTML += "</table>";
            InvoiceHTML += "</div>";
    InvoiceHTML += "</header>";
    InvoiceHTML += "<main>";
      InvoiceHTML += "<table border\"1\" class=\"tblDesc\">";
        InvoiceHTML += "<thead>";
          InvoiceHTML += "<tr>";
            InvoiceHTML += "<th style=\"padding: 20px;\">NOs.</th>";
            InvoiceHTML += "<th style=\"padding: 20px;\">DESCRIPTION</th>";
            InvoiceHTML += "<th style=\"padding: 20px;\">WEIGHT</th>";
            InvoiceHTML += PaidToPay == "To-Pay" ? "<th style=\"padding: 20px;\">FREIGHT</th>" : "";
          InvoiceHTML += "</tr>";
        InvoiceHTML += "</thead>";
        InvoiceHTML += "<tbody>";
            
            var tableExpenses = document.getElementById("ContentPlaceHolder1_tblDescriptionBody");
            for (var i = 0, row; row = tableExpenses.rows[i]; i++) {
                var items = row.cells;
                InvoiceHTML += "<tr>";
                    InvoiceHTML += "<td style=\"" + (i > 1 ? " padding: 10px;" : " padding: 20px;") + "\">" + items.item(0).innerHTML + "</td>";
                    InvoiceHTML += "<td style=\"text-align: left;" + (i > 1 ? " padding: 10px;" : " padding: 20px;") + "\">" + items.item(1).innerHTML + "</td>";
                    InvoiceHTML += "<td style=\"" + (i > 1 ? " padding: 10px;" : " padding: 20px;") + "\">" + items.item(2).innerHTML + "</td>";
                    InvoiceHTML += PaidToPay == "To-Pay" ? "<td style=\"" + (i > 1 ? " padding: 10px;" : "padding: 20px;") + "\">" + items.item(3).innerHTML + "</td>" : "";
                    //InvoiceHTML += "<td style=\"padding: 20px;\">" + items.item(4).innerHTML + "</td>";
                    //InvoiceHTML += "<td style=\"padding: 20px;\">" + items.item(5).innerHTML + "</td>";
                InvoiceHTML += "</tr>";
            }
            InvoiceHTML += "<tr style=\"height: 175px;\">";
                InvoiceHTML += "<td style=\"padding: 20px;\">&nbsp;</td>";
                InvoiceHTML += "<td style=\"padding: 20px;\">&nbsp;</td>";
                InvoiceHTML += "<td style=\"padding: 20px;\">&nbsp;</td>";
                InvoiceHTML += PaidToPay == "To-Pay" ? "<td style=\"padding: 20px;\">&nbsp;</td>" : "";
                //InvoiceHTML += "<td style=\"padding: 20px;\">&nbsp;</td>";
                //InvoiceHTML += "<td style=\"padding: 20px;\">&nbsp;</td>";
            InvoiceHTML += "</tr>";
            //var items = tableExpenses.rows.item(0).cells;
            
        InvoiceHTML += "</tbody>";
            InvoiceHTML += "</table>";
            InvoiceHTML += "<table style=\"width: 100%\">";
            if (PaidToPay == "To-Pay") {
                //InvoiceHTML += "<tr>";
                //    InvoiceHTML += "<td colspan=\"2\">&nbsp;</td>";
                //    InvoiceHTML += document.getElementById('ContentPlaceHolder1_lblTotalAdvance').innerText == "0" ? "" : "<td style=\"text-align: right;\"><h2>Total Advance: " + document.getElementById('ContentPlaceHolder1_lblTotalAdvance').innerText + "</h4></td>";
                //InvoiceHTML += "</tr>";
                InvoiceHTML += "<tr>";
                    InvoiceHTML += "<td colspan=\"2\">&nbsp;</td>";
                    InvoiceHTML += "<td style=\"text-align: right;\"><h2>Total: " + document.getElementById('ContentPlaceHolder1_lblBalance').innerText + "</h4></td>";
                InvoiceHTML += "</tr>";
            } else {
                InvoiceHTML += "<tr>";
                    InvoiceHTML += "<td colspan=\"2\">&nbsp;</td>";
                    InvoiceHTML += "<td style=\"text-align: right;\"><h2>Paid</h4></td>";
                InvoiceHTML += "</tr>";
            }
            
            InvoiceHTML += "</table > ";
            InvoiceHTML += "<br>";
      InvoiceHTML += "<div id=\"notices\">";
        InvoiceHTML += "<div>NOTICE:</div>";
        InvoiceHTML += "<div class=\"notice\">Contact transportation company or broker in case of any debris.</div>";
            InvoiceHTML += "</div>";
            InvoiceHTML += "<br><br>";
      InvoiceHTML += "<div id=\"notices\" style=\"text-align: right;\">";
        InvoiceHTML += "<div>Receiver Signature:</div>";
        InvoiceHTML += "<br><br><br> _________________________________";
      InvoiceHTML += "</div>";
    InvoiceHTML += "</main>";
    InvoiceHTML += "<footer>";
      InvoiceHTML += "Invoice was created on a computer and is valid without the signature and seal.";
    InvoiceHTML += "</footer>";
            var prntData = document.getElementById('ContentPlaceHolder1_pnlInvoices');
            var prntWindow = window.open("", "Print", "width=400,height=400,left=0,top=0,toolbar=0,scrollbar=1,status=0");
            var style = "";
            prntWindow.document.write(InvoiceHTML);
            prntWindow.document.close();
            prntWindow.focus();
            prntWindow.print();
            prntWindow.close();
        }

        function PrintInvoice() {
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
                  InvoiceHTML += "bottom: 0;";
                  InvoiceHTML += "border-top: 1px solid #C1CED9;";
                  InvoiceHTML += "padding: 8px 0;";
                  InvoiceHTML += "text-align: center;";
                InvoiceHTML += "}";
                InvoiceHTML += "</style > ";
            InvoiceHTML += "<header class=\"clearfix\">";
      InvoiceHTML += "<div id=\"logo\ style=\"width: 100%; border-top: 1px solid #C1CED9;\">";
            InvoiceHTML += "<img src=\"../assets/images/MZBLogo.png\" style=\"width: 25%;\">";
            InvoiceHTML += "<img src=\"../assets/images/MZBLogo2.png\" style=\"width: 43%; float: right;\">";
        InvoiceHTML += "</div>";
        InvoiceHTML += "<br>";
            //InvoiceHTML += "<h1>BILTY# " + document.getElementById('ContentPlaceHolder1_lblBiltyNo').innerText + "</h1>";
            InvoiceHTML += "<div class=\"clearfix\" style=\"width: 100%;\">";
            InvoiceHTML += "<br><br><br>";
            InvoiceHTML += "<table style=\"width: 100%;\">";
            InvoiceHTML += "<tr>";
            InvoiceHTML += "<td><span>Invoice #</span> <strong>" + document.getElementById('ContentPlaceHolder1_lblPrintInvoieno').innerText +"</strong></div></td>";
            InvoiceHTML += "</tr>";
            InvoiceHTML += "<tr>";
            InvoiceHTML += "<td><span>Invoice Date</span> <strong>" + document.getElementById('ContentPlaceHolder1_lblPrintInvoiceDate').innerText +"</strong></td>";
            InvoiceHTML += "</tr>";
            InvoiceHTML += "<tr>";
            InvoiceHTML += "<td>Customer</span> <strong>" + document.getElementById('ContentPlaceHolder1_lblPrintInvoiceCsutomer').innerText +"</strong></td>";
            InvoiceHTML += "</tr>";
            InvoiceHTML += "<tr>";
            InvoiceHTML += "<td>Remarks</span> <strong>" + document.getElementById('ContentPlaceHolder1_lblPrintInvoiceRemarks').innerText +"</strong></td>";
            InvoiceHTML += "</tr>";
            InvoiceHTML += "</table>";
            InvoiceHTML += "</div>";
    InvoiceHTML += "</header>";
    InvoiceHTML += "<main>";
      InvoiceHTML += "<table border\"1\" class=\"tblDesc\">";
        InvoiceHTML += "<thead>";
          InvoiceHTML += "<tr>";
            InvoiceHTML += "<th style=\"padding: 20px;\">CODE</th>";
            InvoiceHTML += "<th style=\"padding: 20px;\">DESCRIPTION</th>";
            InvoiceHTML += "<th style=\"padding: 20px;\">QUANTITY</th>";
            InvoiceHTML += "<th style=\"padding: 20px;\">RATE</th>";
            InvoiceHTML += "<th style=\"padding: 20px;\">AMOUNT</th>";
          InvoiceHTML += "</tr>";
        InvoiceHTML += "</thead>";
        InvoiceHTML += "<tbody>";
            
            var tableExpenses = document.getElementById("ContentPlaceHolder1_tblPrintInvoice");
            for (var i = 0, row; row = tableExpenses.rows[i]; i++) {
                var items = row.cells;
                InvoiceHTML += "<tr>";
                    InvoiceHTML += "<td" + (i > 0 ? "" : " style=\"padding: 20px;\"") + ">" + items.item(0).innerHTML + "</td>";
                    InvoiceHTML += "<td" + (i > 0 ? "" : " style=\"padding: 20px;\"") + ">" + items.item(1).innerHTML + "</td>";
                    InvoiceHTML += "<td" + (i > 0 ? "" : " style=\"padding: 20px;\"") + ">" + items.item(2).innerHTML + "</td>";
                    InvoiceHTML += "<td" + (i > 0 ? "" : " style=\"padding: 20px;\"") + ">" + items.item(3).innerHTML + "</td>";
                    InvoiceHTML += "<td" + (i > 0 ? "" : " style=\"padding: 20px;\"") + ">" + items.item(4).innerHTML + "</td>";
                InvoiceHTML += "</tr>";
            }
            //InvoiceHTML += "<tr style=\"height: 175px;\">";
            //    InvoiceHTML += "<td style=\"padding: 20px;\">&nbsp;</td>";
            //    InvoiceHTML += "<td style=\"padding: 20px;\">&nbsp;</td>";
            //    InvoiceHTML += "<td style=\"padding: 20px;\">&nbsp;</td>";
            //    InvoiceHTML += "<td style=\"padding: 20px;\">&nbsp;</td>";
                //InvoiceHTML += "<td style=\"padding: 20px;\">&nbsp;</td>";
                //InvoiceHTML += "<td style=\"padding: 20px;\">&nbsp;</td>";
            InvoiceHTML += "</tr>";
            //var items = tableExpenses.rows.item(0).cells;
            
        InvoiceHTML += "</tbody>";
            InvoiceHTML += "</table>";
            InvoiceHTML += "<table style=\"width: 100%\">";
            InvoiceHTML += "<tr>";
            InvoiceHTML += "<td colspan=\"3\">&nbsp;</td>";
            InvoiceHTML += "<td style=\"text-align: right;\"><h2>Total: " + document.getElementById('ContentPlaceHolder1_lblPrintInvoiceToal').innerText + "</h4></td>";
            InvoiceHTML += "</tr>";
            InvoiceHTML += "</table > ";
            InvoiceHTML += "<br>";
      InvoiceHTML += "<div id=\"notices\">";
        InvoiceHTML += "<div>NOTICE:</div>";
        InvoiceHTML += "<div class=\"notice\">Contact transportation company or broker in case of debris.</div>";
            InvoiceHTML += "</div>";
            InvoiceHTML += "<br><br>";
      InvoiceHTML += "<div id=\"notices\" style=\"text-align: right;\">";
        InvoiceHTML += "<div>Receiver Signature:</div>";
        InvoiceHTML += "<br><br><br> _________________________________";
      InvoiceHTML += "</div>";
    InvoiceHTML += "</main>";
    InvoiceHTML += "<footer>";
      InvoiceHTML += "Invoice was created on a computer and is valid without the signature and seal.";
    InvoiceHTML += "</footer>";
            var prntData = document.getElementById('ContentPlaceHolder1_pnlInvoices');
            var prntWindow = window.open("", "Print", "width=400,height=400,left=0,top=0,toolbar=0,scrollbar=1,status=0");
            var style = "";
            prntWindow.document.write(InvoiceHTML);
            prntWindow.document.close();
            prntWindow.focus();
            prntWindow.print();
            prntWindow.close();
        }

        function PrintSTInvoice() {
            debugger;
            //alert(123);
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
            InvoiceHTML += "bottom: 0;";
            InvoiceHTML += "border-top: 1px solid #C1CED9;";
            InvoiceHTML += "padding: 8px 0;";
            InvoiceHTML += "text-align: center;";
            InvoiceHTML += "}";
            InvoiceHTML += "</style > ";
            InvoiceHTML += "<header class=\"clearfix\">";
            InvoiceHTML += "<div id=\"logo\ style=\"width: 100%; border-top: 1px solid #C1CED9;\">";
            InvoiceHTML += "<img src=\"../assets/images/MZBLogo.png\" style=\"width: 25%;\">";
            InvoiceHTML += "<img src=\"../assets/images/MZBLogo2.png\" style=\"width: 43%; float: right;\">";
            InvoiceHTML += "</div>";
            InvoiceHTML += "</header>";
            InvoiceHTML += "<main>";
            InvoiceHTML += "<table style=\"width: 100%\">";
            InvoiceHTML += "<tr>";
            InvoiceHTML += "<td>";
            InvoiceHTML += "<table style=\"width: 100%;\" border=\"1\">";
            InvoiceHTML += "<tr>";
            InvoiceHTML += "<td style=\"width: 15%; vertical-align: top;\">Supplier's Name & Address</td>";
            InvoiceHTML += "<td style=\"width: 35%\">" + document.getElementById('ContentPlaceHolder1_lblSTSuppliersNameAddress').innerText + "</td>";
            InvoiceHTML += "<td style=\"width: 15%; vertical-align: top;\">Buyer's Name & Address</td>";
            InvoiceHTML += "<td style=\"width: 35%\">" + document.getElementById('ContentPlaceHolder1_lblSTBuyersNameAddress').innerText + "</td>";
            InvoiceHTML += "</tr>";
            InvoiceHTML += "<tr>";
            InvoiceHTML += "<td style=\"width: 15%; vertical-align: top;\">Telephone#</td>";
            InvoiceHTML += "<td style=\"width: 35%\">" + document.getElementById('ContentPlaceHolder1_lblSTSupllierPhone').innerText + "</td>";
            InvoiceHTML += "<td style=\"width: 15%; vertical-align: top;\">Telephone#</td>";
            InvoiceHTML += "<td style=\"width: 35%\">" + document.getElementById('ContentPlaceHolder1_lblSTBuyerPhone').innerText + "</td>";
            InvoiceHTML += "</tr>";
            InvoiceHTML += "<tr>";
            InvoiceHTML += "<td style=\"width: 15%; vertical-align: top;\">N.T.N</td>";
            InvoiceHTML += "<td style=\"width: 35%\">" + document.getElementById('ContentPlaceHolder1_lblSTSupplierNTN').innerText + "</td>";
            InvoiceHTML += "<td style=\"width: 15%; vertical-align: top;\">N.T.N</td>";
            InvoiceHTML += "<td style=\"width: 35%\">" + document.getElementById('ContentPlaceHolder1_lblSTBuyerNTN').innerText + "</td>";
            InvoiceHTML += "</tr>";
            InvoiceHTML += "</table>";
            InvoiceHTML += "</td>";
            InvoiceHTML += "</tr>";
            InvoiceHTML += "<tr>";
            InvoiceHTML += "<td>Terms of Sale:</td>";
            InvoiceHTML += "</tr>";
            InvoiceHTML += "<tr>";
            InvoiceHTML += "<td>";
            InvoiceHTML += "<table style=\"width: 100%;\" border=\"1\">";
            InvoiceHTML += "<thead>";
            InvoiceHTML += "<tr>";
            InvoiceHTML += "<th>Description of Goods</th>";
            InvoiceHTML += "<th>Value of Goods</th>";
            InvoiceHTML += "<th>Rate of Sales Tax</th>";
            InvoiceHTML += "<th>Amount on Sales Tax</th>";
            InvoiceHTML += "<th>Total</th>";
            InvoiceHTML += "</tr>";
            InvoiceHTML += "</thead>";
            InvoiceHTML += "<tbody>";
            InvoiceHTML += "<tr>";
            InvoiceHTML += "<td>Goods Transport Services</td>";
            InvoiceHTML += "<td>" + document.getElementById('ContentPlaceHolder1_lblSTContainerRate').innerText + "</td>";
            InvoiceHTML += "<td>" + document.getElementById('ContentPlaceHolder1_lblSTPercentage').innerText + "%</td>";
            InvoiceHTML += "<td>" + document.getElementById('ContentPlaceHolder1_lblSTAmount').innerText + "</td>";
            InvoiceHTML += "<td>" + document.getElementById('ContentPlaceHolder1_lblAmountInclusiveST').innerText + "</td>";
            InvoiceHTML += "</tr>";
            InvoiceHTML += "<tr>";
            InvoiceHTML += "<td>" + document.getElementById('ContentPlaceHolder1_lblSTFrom').innerText + "</td>";
            InvoiceHTML += "<td>";

            InvoiceHTML += "</td>";
            InvoiceHTML += "<td>";

            InvoiceHTML += "</td>";
            InvoiceHTML += "<td>";

            InvoiceHTML += "</td>";
            InvoiceHTML += "<td>";

            InvoiceHTML += "</td>";
            InvoiceHTML += "</tr>";
            InvoiceHTML += "<tr>";
            InvoiceHTML += "<td>" + document.getElementById('ContentPlaceHolder1_lblSTInvoiceNo').innerText + "</td>";
            InvoiceHTML += "<td>";

            InvoiceHTML += "</td>";
            InvoiceHTML += "<td>";

            InvoiceHTML += "</td>";
            InvoiceHTML += "<td>";

            InvoiceHTML += "</td>";
            InvoiceHTML += "<td>";

            InvoiceHTML += "</td>";
            InvoiceHTML += "</tr>";
            InvoiceHTML += "<tr>";
            InvoiceHTML += "<td>" + document.getElementById('ContentPlaceHolder1_lblSTInvoiceDate').innerText + "</td>";
            InvoiceHTML += "<td>";

            InvoiceHTML += "</td>";
            InvoiceHTML += "<td>";

            InvoiceHTML += "</td>";
            InvoiceHTML += "<td>";

            InvoiceHTML += "</td>";
            InvoiceHTML += "<td>";

            InvoiceHTML += "</td>";
            InvoiceHTML += "</tr>";
            InvoiceHTML += "<tr>";
            InvoiceHTML += "<td>" + document.getElementById('ContentPlaceHolder1_lblSTVehicleRegNo').innerText + "</td>";
            InvoiceHTML += "<td>";

            InvoiceHTML += "</td>";
            InvoiceHTML += "<td>";

            InvoiceHTML += "</td>";
            InvoiceHTML += "<td>";

            InvoiceHTML += "</td>";
            InvoiceHTML += "<td>";

            InvoiceHTML += "</td>";
            InvoiceHTML += "</tr>";
            InvoiceHTML += "</tbody>";
            InvoiceHTML += "<tfoot>";
            InvoiceHTML += "<tr>";
            InvoiceHTML += "<th>";

            InvoiceHTML += "</th>";
            InvoiceHTML += "<th colspan=\"3\">Total</th>";
            InvoiceHTML += "<th>" + document.getElementById('ContentPlaceHolder1_lblSTTotal').innerText + "</th>";
            InvoiceHTML += "</tr>";
            InvoiceHTML += "</tfoot>";
            InvoiceHTML += "</table>";
            InvoiceHTML += "</td>";
            InvoiceHTML += "</tr>";
            InvoiceHTML += "</table>";
            InvoiceHTML += "<table style=\"width: 100%\">";
            InvoiceHTML += "<tr>";
            InvoiceHTML += "<td colspan=\"3\">&nbsp;</td>";
            InvoiceHTML += "<td style=\"text-align: right;\"><h2>Total: " + document.getElementById('ContentPlaceHolder1_lblPrintInvoiceToal').innerText + "</h4></td>";
            InvoiceHTML += "</tr>";
            InvoiceHTML += "</table > ";
            InvoiceHTML += "<br>";
            InvoiceHTML += "<div id=\"notices\">";
            InvoiceHTML += "<div>NOTICE:</div>";
            InvoiceHTML += "<div class=\"notice\">Contact transportation company or broker in case of debris.</div>";
            InvoiceHTML += "</div>";
            InvoiceHTML += "<br><br>";
            InvoiceHTML += "<div id=\"notices\" style=\"text-align: right;\">";
            InvoiceHTML += "<div>Receiver Signature:</div>";
            InvoiceHTML += "<br><br><br> _________________________________";
            InvoiceHTML += "</div>";
            InvoiceHTML += "</main>";
            InvoiceHTML += "<footer>";
            InvoiceHTML += "Invoice was created on a computer and is valid without the signature and seal.";
            InvoiceHTML += "</footer>";
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

    <!-- START CONTENT -->
    <section id="main-content" class=" ">
        <section class="wrapper main-wrapper row" style=''>

            <!-- MAIN CONTENT AREA STARTS -->
            <div id="divNotification" style="margin-top: 10px;" runat="server"></div>

            <div class="col-lg-12">
                <section class="box ">
                    <%--<header class="panel_header">
                        <h2 class="title pull-left">Drivers Reports</h2>
                        <div class="actions panel_actions pull-right">
                            <a class="box_toggle fa fa-chevron-down"></a>
                        </div>
                    </header>--%>
                    <div class="content-body">
                        <div id="general_validate" action="javascript:;" novalidate="novalidate">
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="col-md-2">
                                        <label>From</label>
                                        <asp:LinkButton ID="lnkClearFrom" runat="server" CssClass="fas fa-ban pull-right" ForeColor="Maroon" style="margin-top: 3%;" OnClick="lnkClearFrom_Click"></asp:LinkButton>
                                        <asp:TextBox ID="txtSearchDateFrom" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <label>To</label>
                                        <asp:LinkButton ID="lnkClearTo" runat="server" CssClass="fas fa-ban pull-right" ForeColor="Maroon" style="margin-top: 3%;" OnClick="lnkClearTo_Click"></asp:LinkButton>
                                        <asp:TextBox ID="txtSearchDateTo" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <label>Broker Name</label>
                                        <asp:LinkButton ID="lnkClearBroker" runat="server" CssClass="fas fa-ban pull-right" ForeColor="Maroon" style="margin-top: 3%;" OnClick="lnkClearBroker_Click"></asp:LinkButton>
                                        <asp:DropDownList ID="ddlSearchBroker" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <label>Vehicle Reg#</label>
                                        <asp:LinkButton ID="lnkClearVehicleRegNo" runat="server" CssClass="fas fa-ban pull-right" ForeColor="Maroon" style="margin-top: 3%;" OnClick="lnkClearVehicleRegNo_Click"></asp:LinkButton>
                                        <asp:TextBox ID="txtSearchVehicleRegNo" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <label>Bilty#</label>
                                        <asp:LinkButton ID="lnkClearBiltyNo" runat="server" CssClass="fas fa-ban pull-right" ForeColor="Maroon" style="margin-top: 3%;" OnClick="lnkClearBiltyNo_Click"></asp:LinkButton>
                                        <asp:TextBox ID="txtSearchBiltyNo" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">   
                                        

                                        <div class="btn-group bottom15 right15 m-t-25">
                                            <asp:LinkButton ID="lnkSearch" runat="server"  type="button" CssClass="btn btn-primary" OnClick="lnkSearch_Click"><i class="fas fa-search"></i></asp:LinkButton>
                                            <%--<CssClass="btn btn-success pull-left m-r-10 m-t-25" ToolTip="Click to Search Bilty" OnClick="lnkSearch_Click"><i class="fas fa-search"></i> | Search</asp:LinkButton>--%>
                                            <button type="button" class="btn btn-primary dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
                                                <span class="caret"></span>
                                                <span class="sr-only">Toggle Dropdown</span>
                                            </button>
                                            <ul class="dropdown-menu" role="menu">
                                                
                                                <li><a href="#">Select Actions</a></li>
                                                <%--< Text="Generate" runat="server" ID="lnkGenerateReport" OnClick="lnkGenerateReport_Click" CssClass="pull-right btn-xs btn-info"><i class="fas fa-chart-line"></i> | Generate Report</>--%>
                                                <li></li>
                                                <%--<li><a href="#">Something else here</a></li>
                                                <li class="divider"></li>
                                                <li><a href="#">Separated link</a></li>--%>
                                            </ul>
                                        </div>
                                        
                                        <asp:LinkButton ID="lnkClearFilterAll" runat="server" CssClass="btn btn-danger m-t-25 pull-right" style="margin-top: 3%;" OnClick="lnkClearFilterAll_Click"><i class="fas fa-ban"></i></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
            </div>

            <asp:Panel runat="server" ID="pnlGrid" Visible="false" CssClass="col-lg-12">
                <section class="box ">
                    <div class="content-body">
                        
                        
                            <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                                    <ProgressTemplate>
                                        <div class="modalBackground" style="position: fixed; left: 0; top: 0; width: 100%; height: 100%; z-index: 20000;">
                                            <img src="../assets/images/loader.gif" style="position: fixed; top: 40%; left: 45%; margin-top: -50px; margin-left: -100px;">
                                        </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>--%>
                                <div id="general_validate" action="javascript:;" novalidate="novalidate">
                                    <div class="row">
                                        <asp:LinkButton runat="server" ID="lnkGenerateReport" OnClick="lnkGenerateReport_Click" CssClass="pull-right btn-xs btn-danger"><i class="fas fa-chart-line"></i> | Generate Report</asp:LinkButton>
                                        <div class="col-md-12" style="overflow: scroll; height: 400px;">
                                            <asp:HiddenField ID="hfSelectedOrder" runat="server" />
                                            <asp:GridView ID="gvResult" runat="server" CssClass="table table-hover" AutoGenerateColumns="true" Font-Size="10px"
                                                OnRowCommand="gvResult_RowCommand" OnRowDataBound="gvResult_RowDataBound">
                                                <%--<Columns>
                                                    <asp:TemplateField HeaderText="S.no">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Broker" HeaderText="Broker" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="BookingDate" HeaderText="Date" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="VehicleRegNo" HeaderText="Vehicle" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="EmptyContainerDropLocation" HeaderText="Station" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="Rate" HeaderText="Freight" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="TotalAdvances" HeaderText="Advance" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="TotalExpenses" HeaderText="Expenses" ItemStyle-HorizontalAlign="Center" />
                                                </Columns>--%>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                           <%-- </ContentTemplate>
                        </asp:UpdatePanel>--%>
                    </div>
                </section>
            </asp:Panel>

            <asp:Panel runat="server" ID="pnlReport" Visible="false" CssClass="col-lg-12">
                <section class="box ">
                    <div class="content-body">
                        <%--<asp:LinkButton runat="server" ID="" OnClick="" CssClass="btn btn-info pull-right"><i class="fas fa-chart-line"></i> | Generate Report</asp:LinkButton>--%>
                        <asp:LinkButton runat="server" ID="lnkCloseReport" OnClick="lnkCloseReport_Click" CssClass="pull-right btn-xs btn-danger"><i class="fas fa-times"></i> | Close Report</asp:LinkButton>
                        
                        <%-- SSRS REPORT --%>
                        <rsweb:reportviewer ID="rvDrivers" runat="server" Width="100%" Height="800px"></rsweb:reportviewer>
                        <%-- SSRS REPORT --%>
                    </div>
                </section>
            </asp:Panel>

            <!-- MAIN CONTENT AREA ENDS -->
        </section>
    </section>
    <!-- END CONTENT -->
    
    <script src="../Scripts/jquery-1.7.min.js"></script>
</asp:Content>
