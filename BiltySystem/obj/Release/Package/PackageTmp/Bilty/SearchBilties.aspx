<%@ Page Title="" Language="C#" MasterPageFile="~/Bilty/BiltySystem.Master" AutoEventWireup="true" CodeBehind="SearchBilties.aspx.cs" Inherits="BiltySystem.Bilty.SearchBilties" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Search Bilties</title>
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
                  InvoiceHTML += "position: fixed;";
                  InvoiceHTML += "bottom: 0;";
                  InvoiceHTML += "border-top: 1px solid #C1CED9;";
                  //InvoiceHTML += "padding: 8px;";
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
      InvoiceHTML += "<div id=\"notices\">";
        InvoiceHTML += "<div>NOTICE:</div>";
        InvoiceHTML += "<div class=\"notice\">Contact transportation company or broker in case of any debris.</div>";
            InvoiceHTML += "</div>";
      InvoiceHTML += "<div id=\"notices\" style=\"text-align: right;\">";
        InvoiceHTML += "<div>Receiver Signature:</div>";
        InvoiceHTML += "<br><br><br> _________________________________";
      InvoiceHTML += "</div>";
    InvoiceHTML += "</main>";
    InvoiceHTML += "<footer>";
            InvoiceHTML += "Invoice was created on a computer and is valid without the signature and seal.";
            InvoiceHTML += "<div style=\"padding: 5px;\"><p style=\"text-decoration: underline; text-align: center;\">Developed by: Vals Technologies | PABX: 0304 111 66 88 | www.valstechnologies.com</p></div>";
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
            InvoiceHTML += "<div style=\"padding: 5px; position: fixed; left: 0; bottom: 0;\"><p style=\"text-decoration: underline; text-align: center;\">Developed by: Vals Technologies | PABX: 0304 111 66 88 | www.valstechnologies.com</p></div>";
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
            InvoiceHTML += "<div style=\"padding: 5px; position: fixed; left: 0; bottom: 0;\"><p style=\"text-decoration: underline; text-align: center;\">Developed by: Vals Technologies | PABX: 0304 111 66 88 | www.valstechnologies.com</p></div>";
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
                    <header class="panel_header">
                        <h2 class="title pull-left">Search Bilties</h2>
                        <%--div class="pull-right">
                            
                        </div>--%>
                        <div class="actions panel_actions pull-right">
                            <asp:LinkButton ID="lnkclear" OnClick="lnkclear_Click" runat="server" ForeColor="Maroon"><i class="fas fa-ban"></i></asp:LinkButton>
                            <a class="box_toggle fa fa-chevron-up"></a>
                        </div>
                    </header>
                    <%--<div class="content-body" style="display: none;">--%>
                    <div class="content-body">
                        <div id="general_validate" action="javascript:;" novalidate="novalidate">
                            <div class="row">
                                <div class="col-xs-10">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="form-label">Keyword</label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtKeyword" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
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
                                </div>
                                <div class="col-xs-2">      
                                    <asp:LinkButton ID="lnkSearch" runat="server" CssClass="btn btn-success pull-right m-r-10 m-t-25" ToolTip="Click to Search Bilty" OnClick="lnkSearch_Click"><i class="fas fa-search"></i></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
            </div>

            <div class="col-lg-12">
                <section class="box ">
                    <div class="content-body">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                                    <ProgressTemplate>
                                        <div class="modalBackground" style="position: fixed; left: 0; top: 0; width: 100%; height: 100%; z-index: 20000;">
                                            <img src="../assets/images/loader.gif" style="position: fixed; top: 40%; left: 45%; margin-top: -50px; margin-left: -100px;">
                                        </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                                <div id="general_validate" action="javascript:;" novalidate="novalidate">
                                    <div class="row">
                                        <asp:HiddenField ID="hfSelectedOrder" runat="server" />
                                        <asp:GridView ID="gvBilty" runat="server" CssClass="table table-hover" AutoGenerateColumns="false" Font-Size="10px"
                                            DataKeyNames="OrderID, Vehicles, Containers, Products, Recievings, RecievingDocs, Damages, isInvoiced, PaidToPay, BillToCustomerCompany" 
                                            OnRowCommand="gvBilty_RowCommand" OnRowDataBound="gvBilty_RowDataBound" AllowPaging="true" AllowSorting="true" OnSorting="gvBilty_Sorting" 
                                            OnPageIndexChanging="gvBilty_PageIndexChanging" PageSize="50" PagerSettings-Position="TopAndBottom" PagerStyle-HorizontalAlign="Center" PagerSettings-FirstPageText="<<" PagerSettings-LastPageText=">>">
                                            <Columns>
                                                <asp:BoundField DataField="OrderNo" HeaderText="Bilty #" ItemStyle-HorizontalAlign="Center" SortExpression="OrderNo" />
                                                <asp:BoundField DataField="Date" HeaderText="Date" ItemStyle-HorizontalAlign="Center" SortExpression="Date" />
                                                <asp:BoundField DataField="SenderCompany" HeaderText="Sender" ItemStyle-HorizontalAlign="Center" SortExpression="SenderCompany" />
                                                <asp:BoundField DataField="ReceiverCompany" HeaderText="Reciever" ItemStyle-HorizontalAlign="Center" SortExpression="ReceiverCompany" />
                                                <asp:BoundField DataField="BillToCustomerCompany" HeaderText="Bill To" ItemStyle-HorizontalAlign="Center" SortExpression="BillToCustomerCompany" />
                                                <asp:BoundField DataField="ShippingName" HeaderText="Shipping Line" ItemStyle-HorizontalAlign="Center" SortExpression="ShippingName" />
                                                <asp:TemplateField HeaderText="Vehicles" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkVehicles" runat="server" CommandName="BiltyVehicles" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'><i class="fas fa-truck"></i></asp:LinkButton> | <asp:Label ID="lblTotalVehicles" runat="server" Text='<%# Eval("Vehicles") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Containers" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkContainers" runat="server" CommandName="BiltyContainers" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'><i class="fas fa-shuttle-van"></i></asp:LinkButton> | <asp:Label ID="lblTotalContainers" runat="server" Text='<%# Eval("Containers") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Products" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkProducts" runat="server" CommandName="BiltyProducts" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'><i class="fas fa-boxes"></i></asp:LinkButton> | <asp:Label ID="lblProducts" runat="server" Text='<%# Eval("Products") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Advances" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkAdvances" runat="server" CommandName="BiltyAdvances" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'><i class="fas fa-search-plus"></i></asp:LinkButton> | <asp:Label ID="lblAdvances" runat="server" Text='<%# Eval("Advances") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Damages" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDamages" runat="server" CommandName="Invoices" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'><i class="fas fa-unlink"></i></asp:LinkButton> | <asp:Label ID="lblTotalRecievingDocs" runat="server" Text='<%# Eval("Damages") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="BiltyFreight" HeaderText="Bilty Freight" ItemStyle-HorizontalAlign="Center" SortExpression="BiltyFreight" />
                                                <asp:TemplateField HeaderText="---" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <div class="btn-group bottom15 right15">
                                                            <div class="dropdown">
                                                                <asp:LinkButton ID="lnkUpdateDropdown" runat="server" CssClass="btn btn-primary btn-border dropdown-toggle btn-xs" type="button" data-toggle="dropdown" aria-expanded="true">
                                                                    Documents
                                                                    <span class="caret"></span>
                                                                </asp:LinkButton>
                                                                <ul class="dropdown-menu" role="menu" aria-labelledby="dropdownMenu1">
                                                                    <li role="presentation"><asp:LinkButton ID="lnkBiltyPrint" runat="server" Font-Size="12px" CommandName="BiltyPrint" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'><i class="fas fa-print"></i> | Print Bilty</asp:LinkButton></li>
                                                                    <li role="presentation"><asp:LinkButton ID="lnkInvoice" runat="server" Font-Size="12px" CommandName="Invoice" Enabled ="False" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'><i class="fas fa-file-invoice"></i> | Invoice</asp:LinkButton></li>
                                                                    <li role="presentation"><asp:LinkButton ID="SalesTaxInvoice" runat="server" Font-Size="12px" ToolTip="Click to generate Sales Tax Invoice" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="STInvoice"><i class="fas fa-percent"></i> | Sales Tax Invoice</asp:LinkButton></li>
                                                                </ul>
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Update">
                                                    <ItemTemplate>
                                                        <div class="dropdown">
                                                            <asp:LinkButton CssClass="btn btn-xs btn-default dropdown-toggle" type="button" id="UpdateDropdown" runat="server" data-toggle="dropdown" aria-expanded="true">
                                                                Edit
                                                                <span class="caret"></span>
                                                            </asp:LinkButton>
                                                            <ul class="dropdown-menu" role="menu" aria-labelledby="dropdownMenu1">
                                                                <li role="presentation"><asp:LinkButton ID="lnkEdit" runat="server" CommandName="Change" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Text="Only Bilty"></asp:LinkButton></li>
                                                                <li role="presentation"><asp:LinkButton ID="lnkEditWhole" runat="server" CommandName="ChangeAll" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Text="Whole Bilty"></asp:LinkButton></li>
                                                            </ul>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlStatus" runat="server" runat="server" CssClass="form-control">
                                                            <asp:ListItem>Pending</asp:ListItem>
                                                            <asp:ListItem>Advance Received</asp:ListItem>
                                                            <asp:ListItem>Partial Payment</asp:ListItem>
                                                            <asp:ListItem>Complete</asp:ListItem>
                                                            <asp:ListItem>Cancel</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle CssClass="pagination-ys" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                                <ajaxToolkit:ModalPopupExtender ID="modalBilty" runat="server" PopupControlID="pnlBilty" DropShadow="True" TargetControlID="btnOpenBilty" 
                            CancelControlID="lnkCloseBilty" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlBilty" runat="server" CssClass="row" style="background-color: white; padding: 20px; border: 1px solid black; height: 600px; overflow-y: scroll" Width="1300px">
                            
                                    <asp:Button ID="btnOpenBilty" runat="server" style="display: none" />
                                    <asp:LinkButton ID="lnkCloseBilty" runat="server" style="display: none;"></asp:LinkButton>
                                    <h4 class="pull-left"><asp:Label ID="Label6" runat="server"></asp:Label></h4> 
                                    <asp:LinkButton ID="lnkCloseBiltys" runat="server" ForeColor="Maroon" CssClass="pull-right" OnClick="lnkCloseBiltys_Click"><i class="fas fa-times-circle"></i></asp:LinkButton>
                                    
                                    <div id="divShippingInfoNotification" runat="server"></div>

                                    <div class="row">                                
                                        <asp:Panel ID="Panel2" runat="server" class="col-md-12">
                                            <div id="divBiltyNotification" runat="server"></div>
                                            <div class="col-lg-12">
                                                <section class="box ">
                                                    <header class="panel_header">
                                                        <h2 class="title pull-left">Manual Bilty</h2>
                                                        <div class="actions panel_actions pull-right">
                                                            <%--<a class="box_toggle fa fa-chevron-down"></a>--%>
                            
                            
                                                        </div>
                                                    </header>
                                                    <div class="content-body">
                                                        <div id="general_validate" action="javascript:;" novalidate="novalidate">
                                                            <div class="row">

                                                                <div class="col-xs-8">
                                                                    <div class="col-xs-12 col-sm-6">
                                                                        <div class="form-group">
                                                                            <label class="form-label">Bilty No.</label>
                                                                            <div class="controls">
                                                                                <asp:TextBox ID="txtBiltyNo" runat="server" class="form-control" Enabled="false"></asp:TextBox>
                                                                                <%--<input type="text" class="form-control" name="formfield1">--%>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-xs-12 col-sm-6">
                                                                        <div class="form-group">
                                                                            <label class="form-label">Bilty Date</label>
                                                                            <div class="controls">
                                                                                <asp:TextBox ID="txtBiltyDate" runat="server" class="form-control" TextMode="Date"></asp:TextBox>
                                                                                <asp:HiddenField ID="hfBiltyDate" runat="server" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>



                                                    </div>
                                                </section>
                                            </div>

                                            <div class="col-lg-12">
                                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                    <ContentTemplate>                    
                                                        <section class="box ">
                                                            <header class="panel_header">
                                                                <h2 class="title pull-left">Customer Information</h2>
                                                                <div class="actions panel_actions pull-right">
                                                                    <%--<a class="box_toggle fa fa-chevron-down"></a>--%>
                                    
                                    
                                                                </div>
                                                            </header>
                                                            <div class="content-body">
                                                                <div id="general_validate" action="javascript:;" novalidate="novalidate">
                                                                    <div class="row">
                                                                        <div id="divCustomerInfoNotification" style="margin-top: 10px;" runat="server"></div>
                                                                        <div class="col-xs-12 col-sm-4">
                                                                            <div class="form-group">
                                                                                <div class="controls">
                                                                                    <asp:DropDownList ID="ddlSearchSender" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSearchSender_SelectedIndexChanged"></asp:DropDownList>
                                                                                    <%--<asp:TextBox ID="txtSearchSender" runat="server" class="form-control" placeholder="Search Consigner/Sender" AutoPostBack="true" OnTextChanged="txtSearchSender_TextChanged"></asp:TextBox>
                                                                                    <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchCompanies"
                                                                                        MinimumPrefixLength="2"
                                                                                        CompletionListCssClass="list" 
	                                                                                    CompletionListItemCssClass="listitem" 
	                                                                                    CompletionListHighlightedItemCssClass="hoverlistitem"
                                                                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                                                                        TargetControlID="txtSearchSender"
                                                                                        ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false">
                                                                                    </ajaxToolkit:AutoCompleteExtender>--%>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                
                                                                        <div class="col-xs-12 col-sm-2">
                                                                            <div class="form-group">
                                                                                <%--<label class="form-label">Code</label>--%>
                                                                                <div class="controls">
                                                                                    <asp:TextBox ID="txtSenderCompanyCode" runat="server" placeholder="Code" class="form-control"></asp:TextBox>
                                                    
                                                                                    <%--<input type="text" class="form-control" name="formfield1">--%>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-xs-12 col-sm-2">
                                                                            <div class="form-group">
                                                                                <%--<label class="form-label">Group</label>--%>
                                                                                <div class="controls">
                                                                                    <asp:TextBox ID="txtSenderGroup" runat="server" class="form-control" placeholder="Group"></asp:TextBox>
                                                                                    <%--<input type="text" class="form-control" name="formfield1">--%>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                
                                                                        <div class="col-xs-12 col-sm-2">
                                                                            <div class="form-group">
                                                                                <%--<label class="form-label">Company</label>--%>
                                                                                <div class="controls">
                                                                                    <asp:TextBox ID="txtSenderCompany" runat="server" class="form-control" placeholder="Company"></asp:TextBox>
                                                                                    <%--<input type="text" class="form-control" name="formfield1">--%>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                
                                                                        <div class="col-xs-12 col-sm-2">
                                                                            <div class="form-group">
                                                                                <%--<label class="form-label">Department</label>--%>
                                                                                <div class="controls">
                                                                                    <asp:TextBox ID="txtSenderDepartment" runat="server" class="form-control" placeholder="Department"></asp:TextBox>
                                                                                    <%--<input type="text" class="form-control" name="formfield1">--%>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                
                                                                        <div class="col-xs-12 col-sm-4">
                                                                            <div class="form-group">
                                                 
                                                                                <div class="controls">
                                                                                    <asp:DropDownList ID="ddlSearchReceiver" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSearchReceiver_SelectedIndexChanged"></asp:DropDownList>
                                                                                    <%--<asp:TextBox ID="txtSearchReceiver" runat="server" class="form-control" placeholder="Search Consignee/Sender" AutoPostBack="true" OnTextChanged="txtSearchReceiver_TextChanged"></asp:TextBox>
                                                                                    <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchCompanies"
                                                                                        MinimumPrefixLength="2"
                                                                                        CompletionListCssClass="list" 
	                                                                                    CompletionListItemCssClass="listitem" 
	                                                                                    CompletionListHighlightedItemCssClass="hoverlistitem"
                                                                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                                                                        TargetControlID="txtSearchReceiver"
                                                                                        ID="AutoCompleteExtender3" runat="server" FirstRowSelected="false">
                                                                                    </ajaxToolkit:AutoCompleteExtender>--%>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                
                                
                                                                        <div class="col-xs-12 col-sm-2">
                                                                            <div class="form-group">
                                                                                <%--<label class="form-label">Code</label>--%>
                                                                                <div class="controls">
                                                                                    <asp:TextBox ID="txtReceiverCompanyCode" runat="server" class="form-control" placeholder="Code"></asp:TextBox>
                                                    
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-xs-12 col-sm-2">
                                                                            <div class="form-group">
                                                                                <%--<label class="form-label">Group</label>--%>
                                                                                <div class="controls">
                                                                                    <asp:TextBox ID="txtReceiverGroup" runat="server" class="form-control" placeholder="Group"></asp:TextBox>
                                                                                    <%--<input type="text" class="form-control" name="formfield1">--%>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                
                                                                        <div class="col-xs-12 col-sm-2">
                                                                            <div class="form-group">
                                                                                <%--<label class="form-label">Company</label>--%>
                                                                                <div class="controls">
                                                                                    <asp:TextBox ID="txtReceiverCompany" runat="server" class="form-control" placeholder="Company"></asp:TextBox>
                                                                                    <%--<input type="text" class="form-control" name="formfield1">--%>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                
                                                                        <div class="col-xs-12 col-sm-2">
                                                                            <div class="form-group">
                                                                                <%--<label class="form-label">Department</label>--%>
                                                                                <div class="controls">
                                                                                    <asp:TextBox ID="txtReceiverDepartment" runat="server" class="form-control" placeholder="Department"></asp:TextBox>
                                                                                    <%--<input type="text" class="form-control" name="formfield1">--%>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                
                                                                        <div class="col-xs-12 col-sm-4">
                                                                            <div class="form-group">
                                                                                <%--<label class="form-label">Bill To/Customer</label>--%>
                                                                                <div class="controls">

                                                                                    <asp:DropDownList ID="ddlSearchCustomer" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlSearchCustomer_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                                    <%--<asp:TextBox ID="txtSearchCustomer" runat="server" class="form-control" placeholder="Bill To/Customer" AutoPostBack="true" OnTextChanged="txtSearchCustomer_TextChanged"></asp:TextBox>
                                                                                    <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchCompanies"
                                                                                        MinimumPrefixLength="2"
                                                                                        CompletionListCssClass="list" 
	                                                                                    CompletionListItemCssClass="listitem" 
	                                                                                    CompletionListHighlightedItemCssClass="hoverlistitem"
                                                                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                                                                        TargetControlID="txtSearchCustomer"
                                                                                        ID="AutoCompleteExtender4" runat="server" FirstRowSelected="false">
                                                                                    </ajaxToolkit:AutoCompleteExtender>--%>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                
                                
                                                                        <div class="col-xs-12 col-sm-2">
                                                                            <div class="form-group">
                                                                                <%--<label class="form-label">Code</label>--%>
                                                                                <div class="controls">
                                                                                    <asp:TextBox ID="txtCustomerCode" runat="server" class="form-control" placeholder="Code"></asp:TextBox>
                                                                                    <%--<input type="text" class="form-control" name="formfield1">--%>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                
                                                                        <div class="col-xs-12 col-sm-2">
                                                                            <div class="form-group">
                                                                                <%--<label class="form-label">Group</label>--%>
                                                                                <div class="controls">
                                                                                    <asp:TextBox ID="txtCustomerGroup" runat="server" class="form-control" placeholder="Group"></asp:TextBox>
                                                                                    <%--<input type="text" class="form-control" name="formfield1">--%>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                
                                                                        <div class="col-xs-12 col-sm-2">
                                                                            <div class="form-group">
                                                                                <%--<label class="form-label">Compnay</label>--%>
                                                                                <div class="controls">
                                                                                    <asp:TextBox ID="txtCustomerCompany" runat="server" class="form-control" placeholder="Company"></asp:TextBox>
                                                                                    <%--<input type="text" class="form-control" name="formfield1">--%>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                
                                                                        <div class="col-xs-12 col-sm-2">
                                                                            <div class="form-group">
                                                                                <%--<label class="form-label">Department</label>--%>
                                                                                <div class="controls">
                                                                                    <asp:TextBox ID="txtCustomerDepartment" runat="server" class="form-control" placeholder="Department"></asp:TextBox>
                                                                                    <%--<input type="text" class="form-control" name="formfield1">--%>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-xs-12 col-sm-3">
                                                                            <div class="form-group">
                                                                                <div class="controls">
                                                                                    <asp:DropDownList ID="ddlBillingType" runat="server" CssClass="form-control">
                                                                                        <asp:ListItem>- Select Payment Type -</asp:ListItem>
                                                                                        <asp:ListItem>Vehicle Wise</asp:ListItem>
                                                                                        <asp:ListItem>Weight Wise</asp:ListItem>
                                                                                        <asp:ListItem Selected="True">Container Wise</asp:ListItem>
                                                                                        <asp:ListItem>Vehicle + Weight Wise</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>



                                                            </div>
                                                        </section>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>

                                            <div class="col-lg-12">
                                                <section class="box ">
                                                    <header class="panel_header">
                                                        <h2 class="title pull-left">Shipping Information</h2>
                                                    </header>
                                                    <div class="content-body">
                                                        <div class="row">

                                                            <div class="col-xs-12 col-sm-3">
                                                                <div class="form-group">
                                                                    <label class="form-label">Shipping Type</label>
                                                                    <div class="controls">
                                                                        <asp:DropDownList ID="ddlShippingType" runat="server" CssClass="form-control">
                                                                            <asp:ListItem>Containerized (Import)</asp:ListItem>
                                                                            <asp:ListItem>Containerized (Export)</asp:ListItem>
                                                                            <asp:ListItem>Containerized (Partial)</asp:ListItem>
                                                                            <asp:ListItem>Loose Cargo (Full)</asp:ListItem>
                                                                            <asp:ListItem>Loose Cargo (Partial)</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <%--<input type="text" class="form-control" name="formfield1">--%>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xs-12 col-sm-3">
                                                                <div class="form-group">
                                                                    <label class="form-label">Loading Date</label>
                                                                    <div class="controls">
                                                                        <asp:TextBox ID="txtLoadingDate" runat="server" class="form-control" TextMode="Date"></asp:TextBox>
                                                                        <asp:HiddenField ID="hfLoadingDate" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xs-12 col-sm-3">
                                                                <div class="form-group">
                                                                    <%--<label class="form-label">Clearing Agent</label>
                                                                    <div class="controls">
                                                                        <asp:DropDownList ID="ddlClearingAgent" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                    </div>--%>
                                                                        <label class="form-label">Shipping Line</label>
                                                        <div class="controls">
                                                            <asp:DropDownList ID="ddlShippingLine" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlShippingLine_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </section>
                                            </div>

                                            <div class="col-lg-12">
                                                <asp:UpdatePanel ID="UpdatePanel8" runat="server" Visible="false">
                                                    <ContentTemplate>
                                                        <section class="box ">
                                                            <header class="panel_header">
                                                                <h2 class="title pull-left">Location Information</h2>
                                                                <%--<div class="actions panel_actions pull-right">
                                                                    <a class="box_toggle fa fa-chevron-down"></a>
                                                        
                                                        
                                                                </div>--%>
                                                            </header>
                                                            <div class="content-body">
                                                                <div id="general_validate" action="javascript:;" novalidate="novalidate">
                                                                    <div class="row">
                                                                        <div class="col-xs-2">
                                                                            <div class="form-group">
                                                                                <label class="form-label">Pick Location</label>
                                                                                <div class="controls">
                                                                                    <asp:DropDownList ID="ddlSearchPickLocation" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSearchPickLocation_SelectedIndexChanged"></asp:DropDownList>
                                                                                    <%--<asp:TextBox ID="txtSearchPickLocation" runat="server" class="form-control" AutoPostBack="true" OnTextChanged="txtSearchPickLocation_TextChanged"></asp:TextBox>
                                                                                    <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchLocations"
                                                                                        MinimumPrefixLength="2"
                                                                                        CompletionListCssClass="list" 
	                                                                                    CompletionListItemCssClass="listitem" 
	                                                                                    CompletionListHighlightedItemCssClass="hoverlistitem"
                                                                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                                                                        TargetControlID="txtSearchPickLocation"
                                                                                        ID="AutoCompleteExtender5" runat="server" FirstRowSelected="false">
                                                                                    </ajaxToolkit:AutoCompleteExtender>--%>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-xs-2">
                                                                            <div class="form-group">
                                                                                <label class="form-label">City</label>
                                                                                <div class="controls">
                                                                                    <asp:TextBox ID="txtPickCity" runat="server" class="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                
                                                                        <div class="col-xs-2">
                                                                            <div class="form-group">
                                                                                <label class="form-label">Region</label>
                                                                                <div class="controls">
                                                                                    <asp:TextBox ID="txtPickRegion" runat="server" class="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-xs-2">
                                                                            <div class="form-group">
                                                                                <label class="form-label">Area</label>
                                                                                <div class="controls">
                                                                                    <asp:TextBox ID="txtPickArea" runat="server" class="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-xs-4">
                                                                            <div class="form-group">
                                                                                <label class="form-label">Address</label>
                                                                                <div class="controls">
                                                                                    <asp:TextBox ID="txtPickAddress" runat="server" class="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-xs-2">
                                                                            <div class="form-group">
                                                                                <label class="form-label">Drop Location</label>
                                                                                <div class="controls">
                                                                                    <asp:DropDownList ID="ddlSearchDropLocation" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSearchDropLocation_SelectedIndexChanged"></asp:DropDownList>
                                                                                    <%--<asp:TextBox ID="txtSearchDropLocation" runat="server" class="form-control" AutoPostBack="true" OnTextChanged="txtSearchDropLocation_TextChanged"></asp:TextBox>
                                                                                    <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchLocations"
                                                                                    <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchLocations"
                                                                                        MinimumPrefixLength="2"
                                                                                        CompletionListCssClass="list" 
	                                                                                    CompletionListItemCssClass="listitem" 
	                                                                                    CompletionListHighlightedItemCssClass="hoverlistitem"
                                                                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                                                                        TargetControlID="txtSearchDropLocation"
                                                                                        ID="AutoCompleteExtender6" runat="server" FirstRowSelected="false">
                                                                                    </ajaxToolkit:AutoCompleteExtender>--%>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-xs-2">
                                                                            <div class="form-group">
                                                                                <label class="form-label">City</label>
                                                                                <div class="controls">
                                                                                    <asp:TextBox ID="txtDropCity" runat="server" class="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                
                                                                        <div class="col-xs-2">
                                                                            <div class="form-group">
                                                                                <label class="form-label">Region</label>
                                                                                <div class="controls">
                                                                                    <asp:TextBox ID="txtDropRegion" runat="server" class="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-xs-2">
                                                                            <div class="form-group">
                                                                                <label class="form-label">Area</label>
                                                                                <div class="controls">
                                                                                    <asp:TextBox ID="txtDropArea" runat="server" class="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-xs-4">
                                                                            <div class="form-group">
                                                                                <label class="form-label">Address</label>
                                                                                <div class="controls">
                                                                                    <asp:TextBox ID="txtDropAddress" runat="server" class="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </section>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>

                                            <div class="col-lg-12">
                                                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                    <ContentTemplate>
                                                        <section class="box ">
                                                            <header class="panel_header">
                                                                <h2 class="title pull-left">Bilty Freight</h2>
                                                            </header>
                                                            <div class="content-body">
                                                                <div class="row">
                                                                    <div class="col-xs-12">
                                                                        <div id="divBiltyFreightNotification" runat="server"></div>
                                                                        <div class="col-xs-3">
                                                                            <div class="form-group">
                                                                                <label class="form-label">Bilty Freight</label>
                                                                                <div class="controls">
                                                                                    <asp:TextBox ID="txtBiltyFreight" runat="server" CssClass="form-control" TextMode="Number" AutoPostBack="true" OnTextChanged="txtBiltyFreight_TextChanged" Enabled="false"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-xs-3">
                                                                            <div class="form-group">
                                                                                <label class="form-label">Freight</label>
                                                                                <div class="controls">
                                                                                    <asp:TextBox ID="txtFreight" runat="server" CssClass="form-control" TextMode="Number" AutoPostBack="true" OnTextChanged="txtFreight_TextChanged" Enabled="false"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-xs-3">
                                                                            <div class="form-group">
                                                                                <label class="form-label">PartyCommision</label>
                                                                                <div class="controls">
                                                                                    <asp:TextBox ID="txtPartyCommission" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </section>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>

                                            <div class="col-lg-12">
                                                <asp:UpdatePanel ID="UpdatePanel9" runat="server" Visible="false">
                                                    <ContentTemplate>
                                                        <section class="box ">
                                                            <header class="panel_header">
                                                                <h2 class="title pull-left">Advance Information</h2>
                                                                <%--<div class="actions panel_actions pull-right">
                                                                    <a class="box_toggle fa fa-chevron-down"></a>
                                                        
                                                        
                                                                </div>--%>
                                                            </header>
                                                            <div class="content-body">
                                                                <div id="general_validate" action="javascript:;" novalidate="novalidate">
                                                                    <div class="row">
                                                                        <div class="col-xs-12">
                                                                            <div id="divAdvanceInfoNotification" runat="server"></div>
                                                                            <div class="col-xs-2">
                                                                                <div class="form-group">
                                                                                    <label class="form-label">Advance Freight</label>
                                                                                    <div class="controls">
                                                                                        <asp:TextBox ID="txtAdvanceFreight" runat="server" CssClass="form-control" TextMode="Number" AutoPostBack="true" OnTextChanged="txtAdvanceFreight_TextChanged"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-xs-2">
                                                                                <div class="form-group">
                                                                                    <label class="form-label">Factory Advance</label>
                                                                                    <div class="controls">
                                                                                        <asp:TextBox ID="txtFactoryAdvance" runat="server" CssClass="form-control" TextMode="Number" AutoPostBack="true" OnTextChanged="txtAdvanceFreight_TextChanged"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-xs-2">
                                                                                <div class="form-group">
                                                                                    <label class="form-label">Diesel</label>
                                                                                    <div class="controls">
                                                                                        <asp:TextBox ID="txtDieselAdvance" runat="server" CssClass="form-control" TextMode="Number" AutoPostBack="true" OnTextChanged="txtAdvanceFreight_TextChanged"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-xs-2" id="divAdvanceVehicle" runat="server" visible="false">
                                                                                <div class="form-group">
                                                                                    <label class="form-label">Adv. Amount</label>
                                                                                    <div class="controls">
                                                                                        <asp:TextBox ID="txtVehicleAdvanceAmount" runat="server" CssClass="form-control" TextMode="Number" AutoPostBack="true" OnTextChanged="txtAdvanceFreight_TextChanged"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-xs-2">
                                                                                <div class="form-group">
                                                                                    <label class="form-label">Adv. Vehicle?</label>
                                                                                    <div class="controls">
                                                                                        <asp:CheckBox ID="cbAdvVehicle" runat="server" AutoPostBack="true" OnCheckedChanged="cbAdvVehicle_CheckedChanged" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                            
                                                                            <div class="col-xs-2 pull-right">
                                                                                <div class="form-group">
                                                                                    <label class="form-label">Total Advance</label>
                                                                                    <div class="controls">
                                                                                        <asp:TextBox ID="txtTotalAdvance" runat="server" Text="0" CssClass="form-control" Enabled="false" AutoPostBack="true" OnTextChanged="txtTotalAdvance_TextChanged"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-xs-10">
                                                                                <div class="col-xs-2 pull-right">
                                                                                    <div class="form-group">
                                                                                        <label class="form-label">Additional Weight</label>
                                                                                        <div class="controls">
                                                                                            <asp:TextBox ID="txtAdditionalWeight" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-xs-2 pull-right">
                                                                                    <div class="form-group">
                                                                                        <label class="form-label">Actual Weight</label>
                                                                                        <div class="controls">
                                                                                            <asp:TextBox ID="txtActualWeight" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-xs-2 pull-right">
                                                                                <div class="form-group">
                                                                                    <label class="form-label">Balance Freight</label>
                                                                                    <div class="controls">
                                                                                        <asp:TextBox ID="txtBalanceFreight" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </section>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-xs-12">
                                                <div class="form-group">
                                                    <div class="controls">
                                                        <asp:LinkButton ID="lnkCancelSaveBilty" runat="server" CssClass="btn btn-danger pull-right m-b-10" OnClick="lnkCancelSaveBilty_Click"><i class="fas fa-times"></i> | Cancel</asp:LinkButton>
                                                        <asp:LinkButton ID="lnkSaveBilty" runat="server" CssClass="btn btn-primary pull-right m-b-10 m-r-10" OnClick="lnkSaveBilty_Click"><i class="fas fa-save"></i> | Save</asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                            
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <ajaxToolkit:ModalPopupExtender ID="modalBiltyVehicles" runat="server" PopupControlID="pnlBiltyVehicles" DropShadow="True" TargetControlID="btnOpenBiltyVehicles" 
                            CancelControlID="lnkCloseBiltyVehicles" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlBiltyVehicles" runat="server" CssClass="row" style="background-color: white; padding: 20px; border: 1px solid black;" Width="1300px">
                            
                                    <asp:Button ID="btnOpenBiltyVehicles" runat="server" style="display: none" />
                                    <asp:LinkButton ID="lnkCloseBiltyVehicles" runat="server" ForeColor="Maroon" CssClass="pull-right" style="display: none;"><i class="fa fa-times-circle-o"></i></asp:LinkButton>
                                    <h4 class="pull-left"><asp:Label ID="lblBiltyVehicleOrderID" runat="server"></asp:Label></h4> 
                                    <asp:LinkButton ID="lnkCloseBiltyVehicle" runat="server" ForeColor="Maroon" CssClass="pull-right" OnClick="lnkCloseBiltyVehicle_Click"><i class="fas fa-times-circle"></i></asp:LinkButton>
                                    
                                    <div class="row">                                
                                        <asp:Panel ID="pnlBiltyVehicleInputs" runat="server" class="col-md-12" Visible="false">
                                            <div class="col-xs-12 col-sm-2">
                                                <div class="form-group">
                                                    <label class="form-label">Vehicle Type</label>
                                                    <div class="controls">
                                                        <asp:DropDownList ID="ddlVehicleType" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-12 col-sm-2">
                                                <div class="form-group">
                                                    <label class="form-label">Vehicle Reg. No. </label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtVehicleRegNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                       <%-- <asp:DropDownList ID="ddlVehicleNo" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlVehicleNo_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>--%>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-12 col-sm-2">
                                                <div class="form-group">
                                                    <label class="form-label">Vehicle Contact</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtVehicleContactNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-12 col-sm-2">
                                                <div class="form-group">
                                                    <label class="form-label">Broker</label>
                                                    <div class="controls">
                                                        <asp:DropDownList ID="ddlBroker" runat="server" CssClass="form-control select-chosen"></asp:DropDownList>
                                                        
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-12 col-sm-2">
                                                <div class="form-group">
                                                    <label class="form-label">Driver</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtDriverName" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-12 col-sm-2">
                                                <div class="form-group">
                                                    <label class="form-label">Driver Father</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtDriverfather" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-12 col-sm-2">
                                                <div class="form-group">
                                                    <label class="form-label">Driver NIC</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtDriverNIC" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-12 col-sm-2">
                                                <div class="form-group">
                                                    <label class="form-label">License</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtDriverLicense" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-12 col-sm-2">
                                                <div class="form-group">
                                                    <label class="form-label">Contact No.</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtDriverContactNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-12 col-sm-2">
                                                <div class="form-group">
                                                    <label class="form-label">Rate</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtVehicleRate" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-12">
                                                <div class="form-group">
                                                    <div class="controls">
                                                        <asp:LinkButton ID="lnkCancelAddingNewBilty" runat="server" CssClass="btn btn-danger pull-right m-b-10" OnClick="lnkCancelAddingNewBilty_Click"><i class="fas fa-times"></i> | Cancel</asp:LinkButton>
                                                        <asp:LinkButton ID="lnkSaveBiltyVehicles" runat="server" CssClass="btn btn-primary pull-right m-b-10 m-r-10" OnClick="lnkSaveBiltyVehicles_Click"><i class="fas fa-save"></i> | Save</asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <div class="col-md-12">
                                            <div id="divVehicleInfoModalNotification" runat="server"></div>
                                            
                                            <asp:HiddenField ID="hfSelectedOrderVehicle" runat="server" />
                                            <%--<asp:LinkButton ID="lnkAddNewBiltyVehicle" runat="server" CssClass="btn btn-xs btn-info pull-right m-b-10 m-r-10" OnClick="lnkAddNewBiltyVehicle_Click"><i class="fas fa-plus"></i> | Add New</asp:LinkButton>--%>
                                            <asp:GridView ID="gvBiltyVehicles" runat="server" Font-Size="12px" CssClass="table table-hover" AutoGenerateColumns="false" DataKeyNames="OrderVehicleID, Status, PaidToPay" 
                                                EmptyDataText="No vehicle assigned to selected bilty" OnRowCommand="gvBiltyVehicles_RowCommand" OnRowDataBound="gvBiltyVehicles_RowDataBound">
                                                <Columns>
                                                    <asp:BoundField DataField="VehicleType" HeaderText="Type" />
                                                    <asp:BoundField DataField="VehicleRegNo" HeaderText="Reg. No." />
                                                    <asp:BoundField DataField="VehicleContactNo" HeaderText="Vehicle Contact #" />
                                                    <asp:BoundField DataField="Broker" HeaderText="Broker" />
                                                    <asp:BoundField DataField="DriverName" HeaderText="Driver" />
                                                    <asp:BoundField DataField="Rate" HeaderText="Rate" />
                                                    <asp:BoundField DataField="DriverCellNo" HeaderText="Contact" />
                                                    <asp:TemplateField HeaderText="Update">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEdit" runat="server" CssClass="btn btn-xs btn-info" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="Change"><i class="fas fa-edit"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDelete" runat="server" CssClass="btn btn-xs btn-danger" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="Wipe"><i class="fas fa-trash"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                
                            
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <ajaxToolkit:ModalPopupExtender ID="modalContainers" runat="server" PopupControlID="pnlBiltyContainers" DropShadow="True" TargetControlID="btnOpenBiltyContainers" 
                            CancelControlID="lnkCloseBiltyContainers" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlBiltyContainers" runat="server" CssClass="row" style="background-color: white; padding: 20px; border: 1px solid black;" Width="1300px">
                            
                                    <asp:Button ID="btnOpenBiltyContainers" runat="server" style="display: none" />
                                    <asp:LinkButton ID="lnkCloseBiltyContainers" runat="server" ForeColor="Maroon" CssClass="pull-right" style="display: none;"><i class="fa fa-times-circle-o"></i></asp:LinkButton>
                                    <h4 class="pull-left"><asp:Label ID="Label1" runat="server"></asp:Label></h4> 
                                    <asp:LinkButton ID="lnkCloseBiltyContainer" runat="server" ForeColor="Maroon" CssClass="pull-right" OnClick="lnkCloseBiltyContainer_Click"><i class="fas fa-times-circle"></i></asp:LinkButton>
                                    <asp:LinkButton ID="lnkCloseContainerExpense" runat="server" ForeColor="Maroon" CssClass="pull-right" OnClick="lnkCloseContainerExpense_Click" Visible="false"><i class="fas fa-times-circle"></i></asp:LinkButton>
                                    <asp:LinkButton ID="lnkCloseContainerReceiving" runat="server" ForeColor="Maroon" CssClass="pull-right" OnClick="lnkCloseContainerReceiving_Click" Visible="false"><i class="fas fa-times-circle"></i></asp:LinkButton>
                                    
                                    <div class="row">                                
                                        <asp:Panel ID="pnlBiltyContainerInputs" runat="server" class="col-md-12" Visible="false">
                                            <div class="col-xs-12 col-sm-2">
                                                <div class="form-group">
                                                    <label class="form-label">Container Type</label>
                                                    <div class="controls">
                                                        <asp:DropDownList ID="ddlContainerType" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-12 col-sm-2">
                                                <div class="form-group">
                                                    <label class="form-label">Container No.</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtContainerNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-12 col-sm-2">
                                                <div class="form-group">
                                                    <label class="form-label">Weight</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtWeight" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-12 col-sm-2">
                                                <div class="form-group">
                                                    <label class="form-label">Container Pickup</label>
                                                    <div class="controls">
                                                        <asp:DropDownList ID="ddlContainerPickup" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-12 col-sm-2">
                                                <div class="form-group">
                                                    <label class="form-label">Container Dropoff</label>
                                                    <div class="controls">
                                                        <asp:DropDownList ID="ddlContainerDropoff" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-12 col-sm-2">
                                                <div class="form-group">
                                                    <label class="form-label">Rate</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtContainerRate" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-12 col-sm-2">
                                                <div class="form-group">
                                                    <label class="form-label">Assigned to</label>
                                                    <div class="controls">
                                                        <asp:DropDownList ID="ddlAssignedVehicle" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-12 col-sm-12">
                                                <div class="form-group">
                                                    <label class="form-label">Remarks</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtContainerRemarks" runat="server" CssClass="form-control" TextMode="MultiLine" Columns="12" Rows="2"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-12">
                                                <div class="form-group">
                                                    <div class="controls">
                                                        <asp:LinkButton ID="lnkCancelSaveBiltyContainer" runat="server" CssClass="btn btn-danger pull-right m-b-10" OnClick="lnkCancelSaveBiltyContainer_Click"><i class="fas fa-times"></i> | Cancel</asp:LinkButton>
                                                        <asp:LinkButton ID="lnkSaveBiltyContainer" runat="server" CssClass="btn btn-primary pull-right m-b-10 m-r-10" OnClick="lnkSaveBiltyContainer_Click"><i class="fas fa-save"></i> | Save</asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <div id="divContainerNotifications" runat="server"></div>
                                        <div id="divContainerDetails" runat="server" class="col-md-12">                                            
                                            
                                            <asp:HiddenField ID="hfSelectedOrderContainer" runat="server" />
                                            <asp:HiddenField ID="hfSelectedCotnainerReceiving" runat="server" />
                                            <%--<asp:LinkButton ID="lnkAddNewContainer" runat="server" CssClass="btn btn-xs btn-info pull-right m-b-10 m-r-10" OnClick="lnkAddNewContainer_Click"><i class="fa fa-plus"></i> | Add New</asp:LinkButton>--%>
                                            <asp:GridView ID="gvContainer" runat="server" Font-Size="10px" CssClass="table table-hover" AutoGenerateColumns="false"
                                                EmptyDataText="No container assigned to selected bilty" OnRowCommand="gvContainer_RowCommand" DataKeyNames="OrderConsignmentID, Status" OnRowDataBound="gvContainer_RowDataBound">
                                                <Columns>
                                                    <asp:BoundField DataField="ContainerTypeName" HeaderText="Container" />
                                                    <asp:BoundField DataField="ContainerNo" HeaderText="Container #" />
                                                    <asp:BoundField DataField="ContainerWeight" HeaderText="Weight" />
                                                    <asp:BoundField DataField="EmptyContainerDropLocation" HeaderText="Drop Location" />
                                                    <asp:BoundField DataField="Rate" HeaderText="Rate" />
                                                    <asp:TemplateField HeaderText="Expenses">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkExpenses" runat="server" ToolTip="Click to Add/View Expenses" CommandName="Expenses" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'><i class="fa fa-money"></i></asp:LinkButton> | <asp:Label ID="lblTotalExpense" runat="server" Text='<%# Eval("Expenses") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Receiving">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlReceiving" runat="server" CssClass="form-control" Font-Size="12px" AutoPostBack="true" OnSelectedIndexChanged="ddlReceiving_SelectedIndexChanged">
                                                                <asp:ListItem>Pending</asp:ListItem>
                                                                <asp:ListItem>Received</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Update">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEditContainer" runat="server" CssClass="btn btn-xs btn-info" ToolTip="Click to Edit" CommandName="Change" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'><i class="fa fa-edit"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkDeleteContainert" runat="server" CssClass="btn btn-xs btn-danger" ToolTip="Click to Delete" Visible="false" CommandName="Wipe" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'><i class="fas fa-trash"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>

                                        <asp:Panel ID="pnlContainerExpensesInput" runat="server" class="col-md-12" Visible="false">
                                            <div class="col-xs-12 col-sm-3">
                                                <div class="form-group">
                                                    <label class="form-label">Container</label>
                                                    <div class="controls">
                                                        <asp:DropDownList ID="ddlExpenseContainer" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-12 col-sm-3">
                                                <div class="form-group">
                                                    <label class="form-label">Expense Type</label>
                                                    <div class="controls">
                                                        <asp:DropDownList ID="ddlExpenseType" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-12 col-sm-3">
                                                <div class="form-group">
                                                    <label class="form-label">Amount</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtExpenseAmount" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-12 col-sm-3">
                                                <div class="form-group">
                                                    <label class="form-label">Paid By Driver</label>
                                                    <div class="controls">
                                                        <asp:CheckBox ID="ChkPaid" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-12">
                                                <div class="form-group">
                                                    <div class="controls">
                                                        <asp:LinkButton ID="lnkCancelSaveContainerExpense" runat="server" CssClass="btn btn-danger pull-right m-b-10" OnClick="lnkCancelSaveContainerExpense_Click"><i class="fas fa-times"></i> | Cancel</asp:LinkButton>
                                                        <asp:LinkButton ID="lnkSaveContainerExpense" runat="server" CssClass="btn btn-primary pull-right m-b-10 m-r-10" OnClick="lnkSaveContainerExpense_Click"><i class="fas fa-save"></i> | Save</asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                         <div id="divContExpensesNotification" runat="server"></div>
                                        <div id="divContainerExpense" runat="server" class="col-md-12" visible="false">
                                            <asp:LinkButton ID="lnkAddExpense" runat="server" CssClass="btn btn-primary btn-xs pull-right m-b-10" OnClick="lnkAddExpense_Click"><i class="fa fa-plus-square"></i> | Add Expense</asp:LinkButton>
                                            <asp:HiddenField ID="hfContainerExpense" runat="server" />
                                            <asp:GridView ID="gvContainerExpense" runat="server" Font-Size="10px" CssClass="table table-hover" AutoGenerateColumns="false"
                                                EmptyDataText="No container assigned to selected bilty" DataKeyNames="ContainerExpenseID,PaidByDriver" OnRowCommand="gvContainerExpense_RowCommand" OnRowDataBound="gvContainerExpense_RowDataBound">
                                                <Columns>
                                                    <asp:BoundField DataField="ContainerNo" HeaderText="Container #" />
                                                    <asp:BoundField DataField="ExpensesTypeName" HeaderText="Expense" />
                                                    <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                                    <asp:TemplateField HeaderText="Paid by Driver?">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkPaid" runat="server" CssClass="btn btn-danger pull-right m-b-10" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="Paid"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Update">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEditContainerExpense" runat="server" CssClass="btn btn-xs btn-info" ToolTip="Click to Edit this Container Expense" CommandName="Change" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'><i class="fa fa-edit"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkDeleteContainertExpense" runat="server" CssClass="btn btn-xs btn-danger" ToolTip="Click to Delete"  CommandName="Remove" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'><i class="fas fa-trash"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>

                                        <asp:Panel ID="pnlContainerReceiving" runat="server" class="col-md-12" Visible="false">
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <label class="form-label">ReceivedBy</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtContainerReceivedBy" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <label class="form-label">Receiving Date</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtContainerReceivingDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <label class="form-label">Receiving Time</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtContainerReceivingTime" runat="server" CssClass="form-control" TextMode="Time"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <%--<div class="col-xs-3">
                                                <div class="form-group">
                                                    <label class="form-label">Weighment Charges</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtWeighmentCharges" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>--%>
                                            <div class="col-xs-12">
                                                <div class="form-group">
                                                    <div class="controls">
                                                        <asp:LinkButton ID="lnkCancelSaveContainerReceiving" runat="server" CssClass="btn btn-danger pull-right m-b-10" OnClick="lnkCancelSaveContainerReceiving_Click"><i class="fas fa-times"></i> | Cancel</asp:LinkButton>
                                                        <asp:LinkButton ID="lnkSaveContainerReceiving" runat="server" CssClass="btn btn-primary pull-right m-b-10 m-r-10" OnClick="lnkSaveContainerReceiving_Click"><i class="fas fa-save"></i> | Save</asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                            
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <ajaxToolkit:ModalPopupExtender ID="modalProducts" runat="server" PopupControlID="pnlBiltyProducts" DropShadow="True" TargetControlID="btnOpenBiltyProducts" 
                            CancelControlID="lnkCloseBiltyProducts" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlBiltyProducts" runat="server" CssClass="row" style="background-color: white; padding: 20px; border: 1px solid black;" Width="1300px">
                            
                                    <asp:Button ID="btnOpenBiltyProducts" runat="server" style="display: none" />
                                    <asp:LinkButton ID="lnkCloseBiltyProducts" runat="server" ForeColor="Maroon" CssClass="pull-right" style="display: none;"><i class="fa fa-times-circle-o"></i></asp:LinkButton>
                                    <h4 class="pull-left"><asp:Label ID="Label2" runat="server"></asp:Label></h4> 
                                    <asp:LinkButton ID="lnkCloseBiltyProduct" runat="server" ForeColor="Maroon" CssClass="pull-right" OnClick="lnkCloseBiltyContainer_Click"><i class="fas fa-times-circle"></i></asp:LinkButton>
                                    
                                    <div class="row">                                
                                        <asp:Panel ID="pnlBiltyProductInputs" runat="server" class="col-md-12" Visible="false">
                                            <div class="col-xs-12 col-sm-3">
                                                <div class="form-group">
                                                    <label class="form-label">Search</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtSearchProducts" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <%--<ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"></ajaxToolkit:AutoCompleteExtender>--%>
                                                        <%--<ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchProducts"
                                                            MinimumPrefixLength="2"
                                                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                                            TargetControlID="txtSearchProducts"
                                                            ID="AutoCompleteExtender1" runat="server" FirstRowSelected = "false">
                                                        </ajaxToolkit:AutoCompleteExtender>--%>

                                                        <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchProducts"
                                                            MinimumPrefixLength="2"
                                                            CompletionListCssClass="list" 
	                                                        CompletionListItemCssClass="listitem" 
	                                                        CompletionListHighlightedItemCssClass="hoverlistitem"
                                                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                                            TargetControlID="txtSearchProducts"
                                                            ID="AutoCompleteExtender2" runat="server" FirstRowSelected="false">
                                                        </ajaxToolkit:AutoCompleteExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-12 col-sm-3">
                                                <div class="form-group">
                                                    <label class="form-label">Item</label>
                                                    <div class="controls">                                                        
                                                        <asp:DropDownList ID="ddlProductItem"  runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlProductItem_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-12 col-sm-2">
                                                <div class="form-group">
                                                    <label class="form-label">Package Type</label>
                                                    <div class="controls">
                                                        <asp:DropDownList ID="ddlPackageType" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-12 col-sm-2">
                                                <div class="form-group">
                                                    <label class="form-label">Qty</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtProductQantity" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-12 col-sm-2">
                                                <div class="form-group">
                                                    <label class="form-label">Weight</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtProductWeight" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-12">
                                                <div class="form-group">
                                                    <div class="controls">
                                                        <asp:LinkButton ID="lnkCancelAddingProduct" runat="server" CssClass="btn btn-danger pull-right m-b-10" OnClick="lnkCancelAddingProduct_Click"><i class="fas fa-times"></i> | Cancel</asp:LinkButton>
                                                        <asp:LinkButton ID="lnkAddProduct" runat="server" CssClass="btn btn-primary pull-right m-b-10 m-r-10" OnClick="lnkAddProduct_Click"><i class="fas fa-save"></i> | Save</asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <div class="col-md-12">
                                            <div id="divProductNotification" runat="server"></div>
                                            
                                            <asp:HiddenField ID="hfSelectedProductID" runat="server" />
                                            <%--<asp:LinkButton ID="lnkAddNewProduct" runat="server" CssClass="btn btn-xs btn-info pull-right m-b-10 m-r-10" OnClick="lnkAddNewProduct_Click"><i class="fas fa-plus"></i> | Add New</asp:LinkButton>--%>
                                            <asp:GridView ID="gvProduct" runat="server" Font-Size="10px" CssClass="table table-hover" AutoGenerateColumns="false"
                                                EmptyDataText="No Product assigned to selected bilty" OnRowCommand="gvProduct_RowCommand" DataKeyNames="OrderProductID">
                                                <Columns>
                                                    <asp:BoundField DataField="Item" HeaderText="Product" />
                                                    <asp:BoundField DataField="PackageType" HeaderText="Packaging" />
                                                    <asp:BoundField DataField="Qty" HeaderText="Quantity" />
                                                    <asp:BoundField DataField="TotalWeight" HeaderText="Weight" />
                                                    <asp:TemplateField HeaderText="Update" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEdit" runat="server" CssClass="btn btn-xs btn-info" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="Change"><i class="fas fa-edit"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDelete" runat="server" CssClass="btn btn-xs btn-danger" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="Wipe"><i class="fas fa-trash"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                 </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                
                            
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <ajaxToolkit:ModalPopupExtender ID="modalRecievings" runat="server" PopupControlID="pnlBiltyRecievings" DropShadow="True" TargetControlID="btnOpenBiltyRecievings" 
                            CancelControlID="lnkCloseBiltyRecievings" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlBiltyRecievings" runat="server" CssClass="row" style="background-color: white; padding: 20px; border: 1px solid black;" Width="1300px">
                            
                                    <asp:Button ID="btnOpenBiltyRecievings" runat="server" style="display: none" />
                                    <asp:LinkButton ID="lnkCloseBiltyRecievings" runat="server" ForeColor="Maroon" CssClass="pull-right" style="display: none;"><i class="fa fa-times-circle-o"></i></asp:LinkButton>
                                    <h4 class="pull-left"><asp:Label ID="Label3" runat="server"></asp:Label></h4> 
                                    <asp:LinkButton ID="lnkCloseBiltyRecieving" runat="server" ForeColor="Maroon" CssClass="pull-right" OnClick="lnkCloseBiltyRecieving_Click"><i class="fas fa-times-circle"></i></asp:LinkButton>
                                    
                                    <div class="row">                                
                                        <asp:Panel ID="pnlRecievingInputs" runat="server" class="col-md-12" Visible="false">
                                            <div class="col-xs-12 col-sm-4">
                                                <div class="form-group">
                                                    <label class="form-label">Received By</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtOrderReceivedBy" runat="server" CssClass="form-control" ></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-12 col-sm-4">
                                                <div class="form-group">
                                                    <label class="form-label">Receiving Date</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtOrderReceivingDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-12 col-sm-4">
                                                <div class="form-group">
                                                    <label class="form-label">Receiving Time</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtOrderReceivingTime" runat="server" CssClass="form-control" TextMode="Time"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-12">
                                                <div class="form-group">
                                                    <div class="controls">
                                                        <asp:LinkButton ID="lnkCacnelAddingReceiving" runat="server" CssClass="btn btn-danger pull-right m-b-10" OnClick="lnkCacnelAddingReceiving_Click"><i class="fas fa-times"></i> | Cancel</asp:LinkButton>
                                                        <asp:LinkButton ID="lnkAddReceiving" runat="server" CssClass="btn btn-primary pull-right m-b-10 m-r-10" OnClick="lnkAddReceiving_Click"><i class="fas fa-save"></i> | Save</asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <div class="col-md-12">
                                            <div id="divRecievingNotification" runat="server"></div>
                                            
                                            <asp:HiddenField ID="hfSelectedReceiving" runat="server" />
                                            <asp:LinkButton ID="lnkAddNewRecieving" runat="server" CssClass="btn btn-xs btn-info pull-right m-b-10 m-r-10" OnClick="lnkAddNewRecieving_Click"><i class="fas fa-plus"></i> | Add New</asp:LinkButton>
                                            <asp:GridView ID="gvRecievings" runat="server" Font-Size="10px" CssClass="table table-hover" AutoGenerateColumns="false"
                                                EmptyDataText="No reciepts of selected bilty" OnRowCommand="gvRecievings_RowCommand" DataKeyNames="ConsignmentReceiverID">
                                                <Columns>
                                                    <asp:BoundField DataField="ReceivedBy" HeaderText="Receiver" />
                                                    <asp:BoundField DataField="ReceivedDateTime" HeaderText="Receivied On" />
                                                    <asp:TemplateField HeaderText="Update">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEdit" runat="server" CssClass="btn btn-xs btn-info" CommandName="Change" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'><i class="fas fa-edit"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDelete" runat="server" CssClass="btn btn-xs btn-danger" CommandName="Wipe" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'><i class="fas fa-trash"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                
                            
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        
                        <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                            <ContentTemplate>
                                <ajaxToolkit:ModalPopupExtender ID="modalAdvances" runat="server" PopupControlID="pnlAdvances" DropShadow="True" TargetControlID="btnOpenAdvances" 
                                    CancelControlID="lnkCloseAdvances" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlAdvances" runat="server" CssClass="row" style="background-color: white; padding: 20px; border: 1px solid black;" Width="1300px">
                            
                                    <asp:Button ID="btnOpenAdvances" runat="server" style="display: none" />
                                    <asp:LinkButton ID="lnkCloseAdvances" runat="server" ForeColor="Maroon" CssClass="pull-right" style="display: none;"><i class="fa fa-times-circle-o"></i></asp:LinkButton>
                                    <h4 class="pull-left"><asp:Label ID="Label14" runat="server"></asp:Label></h4> 
                                    <asp:LinkButton ID="lnkCloseAdvancess" runat="server" ForeColor="Maroon" CssClass="pull-right" OnClick="lnkCloseAdvancess_Click"><i class="fas fa-times-circle"></i></asp:LinkButton>
                                    
                                    <div class="row">                                
                                        <asp:Panel ID="pnl" runat="server" class="col-md-12">
                                            <div class="col-xs-12 col-sm-2">
                                                <div class="form-group">
                                                    <label class="form-label">Advance Friehgt</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtAdvancefrei" runat="server" CssClass="form-control" OnTextChanged="SumAllAdvances" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-12 col-sm-2">
                                                <div class="form-group">
                                                    <label class="form-label">Factory Advance</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtFactAdvance" runat="server" CssClass="form-control" OnTextChanged="SumAllAdvances" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-12 col-sm-2">
                                                <div class="form-group">
                                                    <label class="form-label">Diesel</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtDiesAdvance" runat="server" CssClass="form-control" OnTextChanged="SumAllAdvances" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-12 col-sm-2">
                                                <div class="form-group">
                                                    <label class="form-label">Advance from Vehicle</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtVehicAdvance" runat="server" CssClass="form-control" OnTextChanged="SumAllAdvances" AutoPostBack="true"></asp:TextBox>                                                        
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-12 col-sm-4">
                                                <div class="form-group">
                                                    <label>Total</label>
                                                    <h3><asp:Label ID="lblTotAdvance" runat="server"></asp:Label></h3>
                                                </div>
                                            </div>
                                            <div id="divAdvancesNotification" runat="server"></div>
                                            <div class="col-xs-12">
                                                <div class="form-group">
                                                    <div class="controls">
                                                        <asp:LinkButton ID="lnkCancelSaveAdvances" runat="server" CssClass="btn btn-danger pull-right m-b-10" OnClick="lnkCancelSaveAdvances_Click"><i class="fas fa-times"></i> | Cancel</asp:LinkButton>
                                                        <asp:LinkButton ID="lnkSaveAdvances" runat="server" CssClass="btn btn-primary pull-right m-b-10 m-r-10" OnClick="lnkSaveAdvances_Click"><i class="fas fa-save"></i> | Save</asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>

                                        </asp:Panel>
                                        <div class="col-md-12">
                                            
                                            
                                            <asp:HiddenField ID="HiddenField2" runat="server" />
                                            
                                            <%--<asp:GridView ID="GridView1" runat="server" Font-Size="12px" CssClass="table table-hover" AutoGenerateColumns="false" 
                                                EmptyDataText="No vehicle assigned to selected bilty" OnRowCommand="gvBiltyVehicles_RowCommand" OnRowDataBound="gvBiltyVehicles_RowDataBound" DataKeyNames="OrderVehicleID, Status">
                                                <Columns>
                                                    <asp:BoundField DataField="VehicleType" HeaderText="Type" />
                                                    <asp:BoundField DataField="VehicleRegNo" HeaderText="Reg. No." />
                                                    <asp:BoundField DataField="VehicleContactNo" HeaderText="Vehicle Contact #" />
                                                    <asp:BoundField DataField="Broker" HeaderText="Broker" />
                                                    <asp:BoundField DataField="DriverName" HeaderText="Driver" />
                                                    <asp:BoundField DataField="Rate" HeaderText="Rate" />
                                                    <asp:BoundField DataField="DriverCellNo" HeaderText="Contact" />
                                                    <asp:TemplateField HeaderText="Update">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEdit" runat="server" CssClass="btn btn-xs btn-info" Enabled="false" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="Change"><i class="fas fa-edit"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDelete" runat="server" CssClass="btn btn-xs btn-danger" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="Wipe"><i class="fas fa-trash"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>--%>
                                        </div>
                                    </div>
                                
                            
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                            <ContentTemplate>
                                <ajaxToolkit:ModalPopupExtender ID="modalAdvances2" runat="server" PopupControlID="pnlAdvances2" DropShadow="True" TargetControlID="btnOpenAdvances2" 
                            CancelControlID="lnkCloseAdvance2" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlAdvances2" runat="server" CssClass="row" style="background-color: white; padding: 20px; border: 1px solid black;" Width="1300px">
                            
                                    <asp:Button ID="btnOpenAdvances2" runat="server" style="display: none" />
                                    <asp:LinkButton ID="lnkCloseAdvance2" runat="server" ForeColor="Maroon" CssClass="pull-right" style="display: none;"><i class="fa fa-times-circle-o"></i></asp:LinkButton>
                                    <h4 class="pull-left"><asp:Label ID="Label15" runat="server"></asp:Label></h4> 
                                    <asp:LinkButton ID="lnkCloseAdvances2" runat="server" ForeColor="Maroon" CssClass="pull-right" OnClick="lnkCloseAdvances2_Click"><i class="fas fa-times-circle"></i></asp:LinkButton>
                                    
                                    <div class="row">
                                        <asp:Panel ID="pnlAdvanceInput" runat="server" class="col-md-12" Visible="false" DefaultButton="lnkSaveAdvances2">
                                            <div class="col-md-4 pull-left">
                                                <label>Advance Type</label>
                                                <asp:RadioButtonList ID="rbAdvanceTypes" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbAdvanceTypes_SelectedIndexChanged">
                                                    <asp:ListItem>Advance Freight</asp:ListItem>
                                                    <asp:ListItem>Factory Advance</asp:ListItem>
                                                    <asp:ListItem>Diesel Advance</asp:ListItem>
                                                    <asp:ListItem>Vehicle Advance</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="col-md-6 pull-left" style="border-left: 1px solid black;">
                                                <div class="form-group" id="divAdvancePlaces" runat="server">
                                                    <label>Advance From:</label>
                                                    <asp:TextBox ID="txtAdvancePlaces" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-md-6" id="AdvanceAmountPlaceholder" runat="server">
                                                    <label>Amount</label>
                                                    <asp:TextBox ID="txtAdvanceAmount" runat="server" TextMode="Number" CssClass="form-control" OnTextChanged="txtAdvanceAmount_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-md-6" id="divAdvanceDate" runat="server">
                                                    <label>Date</label>
                                                    <asp:TextBox ID="txtAdvanceDate" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-md-6" id="PatrolPumpAdvancePlaceholder" runat="server" visible="false">
                                                    <label>Patrol Pump</label>
                                                    <asp:LinkButton ID="lnkRefreshPatrolPumpDDL" runat="server" CssClass="pull-right" OnClick="lnkRefreshPatrolPumpDDL_Click"><i class="fas fa-refresh"></i></asp:LinkButton>
                                                    <asp:DropDownList ID="ddlPatrolPumps" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPatrolPumps_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                </div>
                                                <div class="form-group col-md-6" id="PatrolRatePlaceholder" runat="server" visible="false">
                                                    <label>Rate per Litre</label>
                                                    <asp:TextBox ID="txtPatrolRate" runat="server" CssClass="form-control" OnTextChanged="txtPatrolRate_TextChanged" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-md-6" id="PatrolLitrePlaceholder" runat="server" visible="false">
                                                    <label>Litres</label>
                                                    <asp:LinkButton ID="lnkAmountCalculation" runat="server" CssClass="pull-right" OnClick="lnkAmountCalculation_Click"><i class="fas fa-refresh"></i></asp:LinkButton>
                                                    <asp:TextBox ID="txtPatrolLitre" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="form-group" id="VehicleAdvancePlaceholder" runat="server" visible="false">
                                                    <label>Vehicle</label>
                                                    <asp:TextBox ID="txtAdvanceVehicleFrom" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            
                                            <div class="col-xs-12">
                                                <div class="form-group">
                                                    <div class="controls">
                                                        <asp:LinkButton ID="lnkCancelSaveAdvances2" runat="server" CssClass="btn btn-danger pull-right m-b-10" OnClick="lnkCancelSaveAdvances2_Click"><i class="fas fa-times"></i> | Cancel</asp:LinkButton>
                                                        <asp:LinkButton ID="lnkSaveAdvances2" runat="server" CssClass="btn btn-primary pull-right m-b-10 m-r-10" OnClick="lnkSaveAdvances2_Click"><i class="fas fa-save"></i> | Save</asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:LinkButton ID="lnkAddAdvance2" runat="server" CssClass="btn btn-info pull-right" OnClick="lnkAddAdvance2_Click"><i class="fas fa-plus-square"></i></asp:LinkButton>
                                            <h4 class="pull-left">Total: <asp:Label ID="lblTotalAdvances" runat="server"></asp:Label></h4>
                                        </div>
                                        <div id="divAdvances2Notification" runat="server"></div>
                                        
                                        <asp:Panel ID="Panel7" runat="server" class="col-md-12">
                                            
                                            <asp:GridView ID="gvAdvances2" runat="server" CssClass="table table-hover" AutoGenerateColumns="false" 
                                                OnRowDataBound="gvAdvances2_RowDataBound" OnRowCommand="gvAdvances2_RowCommand" DataKeyNames="AdvanceID">
                                                <Columns>
                                                    <asp:BoundField DataField="AdvanceID" HeaderText="ID" />
                                                    <asp:BoundField DataField="AdvancePlace" HeaderText="From" />
                                                    <asp:BoundField DataField="AdvanceAgainst" HeaderText="Type" />
                                                    <asp:BoundField DataField="AdvanceAmount" HeaderText="Amount" />
                                                    <asp:BoundField DataField="PatrolPump" HeaderText="Fuel Station" />
                                                    <asp:BoundField DataField="PatrolRate" HeaderText="Rate/Ltr" />
                                                    <asp:BoundField DataField="PatrolLitres" HeaderText="Litres" />
                                                    <asp:BoundField DataField="CreatedDate" HeaderText="Date" />
                                                    <%--<asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkVoucher" runat="server" CssClass="btn btn-xs btn-secondary" Enabled="false" ToolTip="Cannnot make its voucher" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="Voucher"><i class="fas fa-receipt"></i> | Voucher</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDelete" runat="server" CssClass="btn btn-xs btn-danger" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="Wipe"><i class="fas fa-trash"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>

                                        </asp:Panel>
                                        <div class="col-md-12">
                                            
                                            
                                            <asp:HiddenField ID="HiddenField3" runat="server" />
                                            
                                            <%--<asp:GridView ID="GridView1" runat="server" Font-Size="12px" CssClass="table table-hover" AutoGenerateColumns="false" 
                                                EmptyDataText="No vehicle assigned to selected bilty" OnRowCommand="gvBiltyVehicles_RowCommand" OnRowDataBound="gvBiltyVehicles_RowDataBound" DataKeyNames="OrderVehicleID, Status">
                                                <Columns>
                                                    <asp:BoundField DataField="VehicleType" HeaderText="Type" />
                                                    <asp:BoundField DataField="VehicleRegNo" HeaderText="Reg. No." />
                                                    <asp:BoundField DataField="VehicleContactNo" HeaderText="Vehicle Contact #" />
                                                    <asp:BoundField DataField="Broker" HeaderText="Broker" />
                                                    <asp:BoundField DataField="DriverName" HeaderText="Driver" />
                                                    <asp:BoundField DataField="Rate" HeaderText="Rate" />
                                                    <asp:BoundField DataField="DriverCellNo" HeaderText="Contact" />
                                                    <asp:TemplateField HeaderText="Update">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEdit" runat="server" CssClass="btn btn-xs btn-info" Enabled="false" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="Change"><i class="fas fa-edit"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDelete" runat="server" CssClass="btn btn-xs btn-danger" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="Wipe"><i class="fas fa-trash"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>--%>
                                        </div>
                                    </div>
                                
                            
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>



                        <%--<asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>--%>
                                <ajaxToolkit:ModalPopupExtender ID="modalRecievingDocs" runat="server" PopupControlID="pnlBiltyRecievingDocs" DropShadow="True" TargetControlID="btnOpenBiltyRecievingDocs" 
                            CancelControlID="lnkCloseBiltyRecievingDocs" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlBiltyRecievingDocs" runat="server" CssClass="row" style="background-color: white; padding: 20px; border: 1px solid black;" Width="1300px">
                            
                                    <asp:Button ID="btnOpenBiltyRecievingDocs" runat="server" style="display: none" />
                                    <asp:LinkButton ID="lnkCloseBiltyRecievingDocs" runat="server" ForeColor="Maroon" CssClass="pull-right" style="display: none;"><i class="fa fa-times-circle-o"></i></asp:LinkButton>
                                    <h4 class="pull-left"><asp:Label ID="Label4" runat="server"></asp:Label></h4> 
                                    <asp:LinkButton ID="lnkCloseBiltyRecievingDoc" runat="server" ForeColor="Maroon" CssClass="pull-right" OnClick="lnkCloseBiltyRecievingDoc_Click"><i class="fas fa-times-circle"></i></asp:LinkButton>
                                    
                                    <div class="row">                                
                                        <asp:Panel ID="pnlRecievingDocInputs" runat="server" class="col-md-12" Visible="false">
                                            <div class="col-xs-12 col-sm-4">
                                                <div class="form-group">
                                                    <label class="form-label">Document Type</label>
                                                    <div class="controls">
                                                        <asp:DropDownList ID="ddlDocumentType" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-12 col-sm-4">
                                                <div class="form-group">
                                                    <label class="form-label">Document No. </label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtDocumentNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-12 col-sm-4">
                                                <div class="form-group">
                                                    <label class="form-label">Document</label>
                                                    <div class="controls">
                                                        <asp:FileUpload ID="fuReceivingDocument" runat="server" />
                                                    </div>
                                                </div>
                                            <div id="hfReceivingDocNotification" runat="server"></div>
                                            
                                            </div>
                                            <div class="col-xs-12">
                                                <div class="form-group">
                                                    <div class="controls">
                                                        <asp:LinkButton ID="lnkCancelAddingReceivingDoc" runat="server" CssClass="btn btn-danger pull-right m-b-10" OnClick="lnkCancelAddingReceivingDoc_Click"><i class="fas fa-times"></i> | Cancel</asp:LinkButton>
                                                        <asp:LinkButton ID="lnkAddReceivingDoc" runat="server" CssClass="btn btn-primary pull-right m-b-10 m-r-10" OnClick="lnkAddReceivingDoc_Click"><i class="fas fa-save"></i> | Save</asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <div class="col-md-12">
                                            <asp:HiddenField ID="hfSelectedRecievingDocID" runat="server" />
                                            <asp:HiddenField ID="hfReceivingDocumentName" runat="server" />
                                            <asp:LinkButton ID="lnkAddNewRecievingDoc" runat="server" CssClass="btn btn-xs btn-info pull-right m-b-10 m-r-10" OnClick="lnkAddNewRecievingDoc_Click"><i class="fas fa-plus"></i> | Add New</asp:LinkButton>
                                            <asp:GridView ID="gvRecievingDoc" runat="server" Font-Size="10px" CssClass="table table-hover" AutoGenerateColumns="false"
                                                EmptyDataText="No reciept document of selected bilty" OnRowCommand="gvRecievingDoc_RowCommand" DataKeyNames="OrderReceivedDocumentID">
                                                <Columns>
                                                    <asp:BoundField DataField="DocumentType" HeaderText="Type" />
                                                    <asp:BoundField DataField="DocumentNo" HeaderText="Documnet #" />
                                                    <asp:BoundField DataField="DocumentName" HeaderText="Name" />
                                                    <asp:BoundField DataField="DocumentPath" HeaderText="Path" />
                                                    <asp:TemplateField HeaderText="Update">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEdit" runat="server" CssClass="btn btn-xs btn-info" CommandName="Change" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'><i class="fas fa-edit"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDelete" runat="server" CssClass="btn btn-xs btn-danger" CommandName="Wipe" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'><i class="fas fa-trash"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                
                            
                                </asp:Panel>
                            <%--</ContentTemplate>
                        </asp:UpdatePanel>--%>

                        <%--<asp:UpdatePanel ID="UpdatePanel7" runat="server">
                            <ContentTemplate>--%>
                                <ajaxToolkit:ModalPopupExtender ID="modalDamages" runat="server" PopupControlID="pnlBiltyDamages" DropShadow="True" TargetControlID="btnOpenBiltyDamages" 
                                    CancelControlID="lnkCloseBiltyDamages" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlBiltyDamages" runat="server" CssClass="row" style="background-color: white; padding: 20px; border: 1px solid black;" Width="1300px">
                            
                                    <asp:Button ID="btnOpenBiltyDamages" runat="server" style="display: none" />
                                    <asp:LinkButton ID="lnkCloseBiltyDamages" runat="server" ForeColor="Maroon" CssClass="pull-right" style="display: none;"><i class="fa fa-times-circle-o"></i></asp:LinkButton>
                                    <h4 class="pull-left"><asp:Label ID="Label5" runat="server"></asp:Label></h4> 
                                    <asp:LinkButton ID="lnkCloseBiltyDamage" runat="server" ForeColor="Maroon" CssClass="pull-right" OnClick="lnkCloseBiltyDamage_Click"><i class="fas fa-times-circle"></i></asp:LinkButton>
                                    
                                    <div class="row">                                
                                        <asp:Panel ID="pnlDamageInputs" runat="server" class="col-md-12" Visible="false">
                                            <div class="col-xs-12 col-sm-3">
                                                <div class="form-group">
                                                    <label class="form-label">Item</label>
                                                    <div class="controls">
                                                        <asp:DropDownList ID="ddlDamageItem" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-12 col-sm-3">
                                                <div class="form-group">
                                                    <label class="form-label">Damage Type</label>
                                                    <div class="controls">
                                                        <asp:DropDownList ID="ddlDamageType" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-12 col-sm-2">
                                                <div class="form-group">
                                                    <label class="form-label">Damage Cost</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtDamageCost" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-12 col-sm-2">
                                                <div class="form-group">
                                                    <label class="form-label">Damage Cause</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtDamageCause" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-12 col-sm-2">
                                                <div class="form-group">
                                                    <label class="form-label">Damage Document</label>
                                                    <div class="controls">
                                                        <asp:FileUpload ID="fuDamageDocument" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-12">
                                                <div class="form-group">
                                                    <div class="controls">
                                                        <asp:LinkButton ID="lnkCancelSaveBiltyDamages" runat="server" CssClass="btn btn-danger pull-right m-b-10" OnClick="lnkCancelSaveBiltyDamages_Click"><i class="fas fa-times"></i> | Cancel</asp:LinkButton>
                                                        <asp:LinkButton ID="lnkSaveBiltyDamages" runat="server" CssClass="btn btn-primary pull-right m-b-10 m-r-10" OnClick="lnkSaveBiltyDamages_Click"><i class="fas fa-save"></i> | Save</asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <div class="col-md-12">
                                            <div id="divDamageNotification" runat="server"></div>
                                            
                                            <asp:HiddenField ID="hfSelectedDamageID" runat="server" />
                                            <asp:HiddenField ID="hfDamageDocument" runat="server" />
                                            <asp:LinkButton ID="lnkAddNewDamage" runat="server" CssClass="btn btn-xs btn-info pull-right m-b-10 m-r-10" OnClick="lnkAddNewDamage_Click"><i class="fas fa-plus"></i> | Add New</asp:LinkButton>
                                            <asp:GridView ID="gvDamage" runat="server" Font-Size="10px" CssClass="table table-hover" 
                                                EmptyDataText="No reciept document of selected bilty" OnRowCommand="gvDamage_RowCommand" DataKeyNames="OrderDamageID" AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:BoundField DataField="ItemName" HeaderText="Item" />
                                                    <asp:BoundField DataField="DamageType" HeaderText="Damage Type" />
                                                    <asp:BoundField DataField="DamageCost" HeaderText="Cost" />
                                                    <asp:BoundField DataField="DamageCause" HeaderText="Damage Cause" />
                                                    <asp:BoundField DataField="DamageDocumentName" HeaderText="Image" />
                                                    <asp:TemplateField HeaderText="Update">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEdit" runat="server" CssClass="btn btn-xs btn-info" CommandName="Change" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'><i class="fas fa-edit"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDelete" runat="server" CssClass="btn btn-xs btn-danger" CommandName="Wipe" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'><i class="fas fa-trash"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                            
                                </asp:Panel>
                            <%--</ContentTemplate>
                        </asp:UpdatePanel>--%>

                        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                            <ContentTemplate>
                                <ajaxToolkit:ModalPopupExtender ID="modalBiltyPrint" runat="server" PopupControlID="pnlBiltyPrint" DropShadow="True" TargetControlID="btnOpenBiltyPrint" 
                                    CancelControlID="lnkCloseBiltyPrint" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlBiltyPrint" runat="server" CssClass="row" style="background-color: white; padding: 20px; border: 1px solid black; height: 600px; overflow-y: scroll" Width="1300px">
                            
                                    <asp:Button ID="btnOpenBiltyPrint" runat="server" style="display: none" />
                                    <asp:LinkButton ID="lnkCloseBiltyPrint" runat="server" ForeColor="Maroon" CssClass="pull-right" style="display: none;"><i class="fa fa-times-circle-o"></i></asp:LinkButton>
                                    <h4 class="pull-left"><asp:Label ID="Label8" runat="server"></asp:Label></h4> 
                                    <asp:LinkButton ID="lnkCloseBiltyPrints" runat="server" ForeColor="Maroon" CssClass="pull-right" OnClick="lnkCloseBills_Click"><i class="fas fa-times-circle"></i></asp:LinkButton>
                                    <div id="div2" runat="server"></div>
                                    <h2>Invoice</h2>
                                    
                                    <asp:Panel ID="Panel3" runat="server" CssClass="col-xs-12" Visible="false">
                                        <div class="col-xs-12 col-sm-6">
                                            <div class="form-group">
                                                <label class="form-label">Keyword</label>
                                                <div class="controls">
                                                    <asp:CheckBoxList ID="CheckBoxList1" runat="server"></asp:CheckBoxList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xs-12 col-sm-6">
                                            <div class="form-group">
                                                <div class="controls">
                                                    <asp:LinkButton ID="LinkButton3" runat="server" CssClass="btn btn-info m-t-40" OnClick="lnkAddContainers_Click"><i class="fas fa-plus-square"></i></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <asp:Panel ID="Panel4" runat="server" CssClass="col-xs-12">
                                        <!-- start -->
                                        <header class="clearfix">
                                            <div id="logo">
                                                <img src="../assets/images/MZBLogo.png" style="width:15%;">
                                                <img src="../assets/images/MZBLogo2.png" style="width:33%; float: right;">
                                            </div>
                                            <h1>Customer Bill</h1>
                                            <div id="company" class="col-lg-6 col-md-6 col-xs-6 pull-right">
                                                <div style="text-align: right;"> <asp:Label ID="lblBillToCompany" runat="server"></asp:Label></div>
                                                <div style="text-align: right;"> <asp:Label ID="lblBilltoAddress" runat="server"></asp:Label></div>
                                                <div style="text-align: right;"> <asp:Label ID="lblContact" runat="server"></asp:Label></div>
                                                <div style="text-align: right;"> <asp:Label ID="lblBilltoEmail" runat="server"></asp:Label></div>
                                            </div>
                                            <div id="project" class="col-lg-6 col-md-6 col-xs-6 pull-left">
                                                <div><span class="heading">BILTY#</span> <asp:Label ID="lblBiltyNo" runat="server"></asp:Label></div>
                                                <div><span class="heading">TRUCK</span>  <asp:Label ID="lblVehicleRegNo" runat="server"></asp:Label></div>
                                                <div><span class="heading">FROM</span>  <asp:Label ID="lblFrom" runat="server"></asp:Label> <span>TO</span>  <asp:Label ID="lblTo" runat="server"></asp:Label></div>
                                                <div><span class="heading">SENDER</span>  <asp:Label ID="lblSenderCompany" runat="server"></asp:Label></div>
                                                <div><span class="heading">RECEIVER</span>  <asp:Label ID="lblReceiverCompany" runat="server"></asp:Label></div>
                                                <div><span class="heading">DATE</span>  <asp:Label ID="lblBiltyDate" runat="server"></asp:Label></div>
                                                <div><span class="heading">BROKER</span>  <asp:Label ID="lblBroker" runat="server"></asp:Label></div>
                                                
                                            </div>
                                        </header>
                                        <main>
                                            <table class="table table-hover">
                                                <thead id="tblDescriptionHead" runat="server">
                                                    <tr>
                                                        <th class="service">Nos.</th>
                                                        <th class="desc">DESCRIPTION</th>
                                                        <th>Weight</th>
                                                        <%--<th>Freight</th>--%>
                                                    </tr>
                                                </thead>
                                                <tbody id="tblDescriptionBody" runat="server">
                                                    <tr>
                                                        <td class="service">1 X 46</td>
                                                        <td class="desc">Container Export</td>
                                                        <td class="unit"></td>
                                                        <td class="qty"></td>
                                                        <td class="total"></td>
                                                        <td class="total"></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" style="text-align:right">Shipping Line</td>
                                                        <td colspan="2" class="total"></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" style="text-align:right">Address</td>
                                                        <td colspan="2" class="total">Gat#1, Old Truck adda, Mauripur, Karachi</td>
                                                    </tr>
                                                </tbody>

                                                <asp:HiddenField ID="hfBalance" runat="server" />
                                            </table>
                                            <table style="width: 100%">
                                                <%--<tr id="trTotalAdvance" runat="server" visible="false">
                                                    <td colspan="3"></td>
                                                    
                                                    <td style="text-align: right;"><h4>Total Advance: <asp:Label ID="lblTotalAdvance" runat="server"></asp:Label></h4></td>
                                                </tr>--%>
                                                <tr id="trBalance" runat="server" visible="false">
                                                    <td colspan="3"></td>
                                                    <td style="text-align: right;"><h4>Balance: <asp:Label ID="lblBalance" runat="server"></asp:Label></h4></td>
                                                </tr>
                                                <tr id="trPaid" runat="server">
                                                    <td colspan="3"></td>
                                                    <td style="text-align: right;"><h4>Paid</h4></td>
                                                </tr>
                                            </table>
                                            <asp:Label ID="lblPaidtoPay" runat="server" style="display: none;"></asp:Label>
                                            <div id="notices">
                                                <div>NOTICE:</div>
                                                <div class="notice">Contact transportation company or broker in case of debris.</div>
                                            </div>
                                        </main>
                                        <footer>
                                            Invoice was created on a computer and is valid without the signature and seal.
                                       
                                        </footer>
                                        <!-- end -->
                                        <a href="#" onclick="Print(); return false;" class="btn btn-purple btn-md"><i class="fa fa-print"></i> &nbsp; Print </a>
                                    </asp:Panel>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        
                        <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                            <ContentTemplate>
                                <ajaxToolkit:ModalPopupExtender ID="modalInvoicePrint" runat="server" PopupControlID="pnlInvoicePrint" DropShadow="True" TargetControlID="btnOpenInvoicePrint" 
                                    CancelControlID="lnkCloseInvoicePrint" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlInvoicePrint" runat="server" CssClass="row" style="background-color: white; padding: 20px; border: 1px solid black; height: 600px; overflow-y: scroll" Width="1300px">
                            
                                    <asp:Button ID="btnOpenInvoicePrint" runat="server" style="display: none" />
                                    <asp:LinkButton ID="lnkCloseInvoicePrint" runat="server" ForeColor="Maroon" CssClass="pull-right" style="display: none;"><i class="fa fa-times-circle-o"></i></asp:LinkButton>
                                    <h4 class="pull-left"><asp:Label ID="Label9" runat="server"></asp:Label></h4> 
                                    <asp:LinkButton ID="lnkCloseInvoicePrints" runat="server" ForeColor="Maroon" CssClass="pull-right" OnClick="lnkCloseInvoicePrints_Click"><i class="fas fa-times-circle"></i></asp:LinkButton>
                                    <div id="div3" runat="server"></div>
                                    <h2>Invoice</h2>
                                    
                                    <asp:Panel ID="Panel5" runat="server" CssClass="col-xs-12" Visible="false">
                                        <div class="col-xs-12 col-sm-6">
                                            <div class="form-group">
                                                <label class="form-label">Keyword</label>
                                                <div class="controls">
                                                    <asp:CheckBoxList ID="CheckBoxList2" runat="server"></asp:CheckBoxList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xs-12 col-sm-6">
                                            <div class="form-group">
                                                <div class="controls">
                                                    <asp:LinkButton ID="LinkButton4" runat="server" CssClass="btn btn-info m-t-40" OnClick="lnkAddContainers_Click"><i class="fas fa-plus-square"></i></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <asp:Panel ID="Panel6" runat="server" CssClass="col-xs-12">
                                        <!-- start -->
                                        <header class="clearfix">
                                            <div id="logo">
                                                <img src="../assets/images/MZBLogo.png" style="width:15%;">
                                                <img src="../assets/images/MZBLogo2.png" style="width:33%; float: right;">
                                            </div>
                                            <h1>Customer Invoice</h1>
                                            <div id="company" class="col-lg-6 col-md-6 col-xs-6 pull-right">
                                                <div style="text-align: right;"> <asp:Label ID="Label10" runat="server"></asp:Label></div>
                                                <div style="text-align: right;"> <asp:Label ID="Label11" runat="server"></asp:Label></div>
                                                <div style="text-align: right;"> <asp:Label ID="Label12" runat="server"></asp:Label></div>
                                                <div style="text-align: right;"> <asp:Label ID="Label13" runat="server"></asp:Label></div>
                                            </div>
                                            <div id="project" class="col-lg-6 col-md-6 col-xs-6 pull-left">
                                                <div><span class="heading">Invoice#</span> <asp:Label ID="lblPrintInvoieno" runat="server"></asp:Label></div>
                                                <div><span class="heading">InvoiceDate</span>  <asp:Label ID="lblPrintInvoiceDate" runat="server"></asp:Label></div>
                                                <div><span class="heading">Customer</span>  <asp:Label ID="lblPrintInvoiceCsutomer" runat="server"></asp:Label> <span>TO</span>  <asp:Label ID="Label17" runat="server"></asp:Label></div>
                                                <div><span class="heading">Remarks</span>  <asp:Label ID="lblPrintInvoiceRemarks" runat="server"></asp:Label></div>
                                                
                                            </div>
                                        </header>
                                        <main>
                                            <table class="table table-hover">
                                                <thead>
                                                    <tr>
                                                        <th class="service">Nos.</th>
                                                        <th class="desc">DESCRIPTION</th>
                                                        <th>Qty</th>
                                                        <th>Rate</th>
                                                        <th>Amount</th>
                                                    </tr>
                                                </thead>
                                                <tbody id="tblPrintInvoice" runat="server">
                                                    <tr>
                                                        <td class="service">1 X 46</td>
                                                        <td class="desc">Container Export</td>
                                                        <td class="unit"></td>
                                                        <td class="qty"></td>
                                                        <td class="total"></td>
                                                        <td class="total"></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" style="text-align:right">Shipping Line</td>
                                                        <td colspan="2" class="total"></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" style="text-align:right">Address</td>
                                                        <td colspan="2" class="total">Gat#1, Old Truck adda, Mauripur, Karachi</td>
                                                    </tr>
                                                </tbody>

                                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                            </table>
                                            <table style="width: 100%">
                                                <%--<tr>
                                                    <td colspan="3"></td>
                                                    
                                                    <td style="text-align: right;"><h4>Total Advance: <asp:Label ID="Label22" runat="server"></asp:Label></h4></td>
                                                </tr>--%>
                                                <tr>
                                                    <td colspan="3"></td>
                                                    <td style="text-align: right;"><h4>Total: <asp:Label ID="lblPrintInvoiceToal" runat="server"></asp:Label></h4></td>
                                                </tr>
                                            </table>
                                            <div id="notices">
                                                <div>NOTICE:</div>
                                                <div class="notice">Contact transportation company or broker in case of debris.</div>
                                            </div>
                                        </main>
                                        <footer>
                                            Invoice was created on a computer and is valid without the signature and seal.
                                       
                                        </footer>
                                        <!-- end -->
                                        <a href="#" onclick="PrintInvoice(); return false;" class="btn btn-purple btn-md"><i class="fa fa-print"></i> &nbsp; Print </a>
                                    </asp:Panel>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <ajaxToolkit:ModalPopupExtender ID="modalConfirm" runat="server" PopupControlID="pnlConfirm" DropShadow="True" TargetControlID="btnOpenConfirmModal" 
                            CancelControlID="lnkCloseConfirmModal" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
                        <asp:Panel ID="pnlConfirm" runat="server" CssClass="row" style="background-color: white; padding: 20px; border: 1px solid black;" Width="400px">
                            <asp:Button ID="btnOpenConfirmModal" runat="server" style="display: none" />
                            <asp:LinkButton ID="lnkCloseConfirmModal" runat="server" ForeColor="Maroon" CssClass="pull-right" style="display: none;"><i class="fa fa-times-circle-o"></i></asp:LinkButton>
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>Customer Invoice No</label>
                                    <asp:TextBox ID="txtCustomerInvoiceNo" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <h4><asp:Label ID="lblModalTitle" runat="server"></asp:Label></h4>                       
                            <div class="col-md-12">
                                <asp:HiddenField ID="hfConfirmAction" runat="server" />
                                <asp:Label ID="lblConfirmAction" runat="server" Visible="false"></asp:Label>
                                <asp:LinkButton ID="lnkConfirm" runat="server" ForeColor="Green" Font-Size="70px" OnClick="lnkConfirm_Click"><i class="fas fa-check pull-left"></i></asp:LinkButton>
                                <asp:LinkButton ID="lnkCancelConfirm" runat="server" ForeColor="Red" Font-Size="70px"><i class="fas fa-times-circle pull-right"></i></asp:LinkButton>
                            </div>
                            <div class="col-md-12">
                                <label>Note: Provide Customer Invoice # if wish so</label>
                            </div>
                        </asp:Panel>

                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                            <ContentTemplate>
                                <ajaxToolkit:ModalPopupExtender ID="modalInvoice" runat="server" PopupControlID="pnlInvoice" DropShadow="True" TargetControlID="btnOpenInvoice" 
                                    CancelControlID="lnkCloseInvoice" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlInvoice" runat="server" CssClass="row" style="background-color: white; padding: 20px; border: 1px solid black; height: 600px; overflow-y: scroll" Width="1300px">
                            
                                    <asp:Button ID="btnOpenInvoice" runat="server" style="display: none" />
                                    <asp:LinkButton ID="lnkCloseInvoice" runat="server" ForeColor="Maroon" CssClass="pull-right" style="display: none;"><i class="fa fa-times-circle-o"></i></asp:LinkButton>
                                    <h4 class="pull-left"><asp:Label ID="Label7" runat="server"></asp:Label></h4> 
                                    <asp:LinkButton ID="lnkCloseInvoices" runat="server" ForeColor="Maroon" CssClass="pull-right" OnClick="lnkCloseInvoices_Click"><i class="fas fa-times-circle"></i></asp:LinkButton>
                                    <div id="divInvoiceNotification" runat="server"></div>
                                    <h2>Invoice</h2>
                                    
                                    <asp:Panel ID="pnlContainerSelection" runat="server" CssClass="col-xs-12" Visible="false">
                                        <div class="col-xs-12 col-sm-6">
                                            <div class="form-group">
                                                <label class="form-label">Keyword</label>
                                                <div class="controls">
                                                    <asp:CheckBoxList ID="cbOrderContainers" runat="server"></asp:CheckBoxList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xs-12 col-sm-6">
                                            <div class="form-group">
                                                <div class="controls">
                                                    <asp:LinkButton ID="lnkAddContainers" runat="server" CssClass="btn btn-info m-t-40" OnClick="lnkAddContainers_Click"><i class="fas fa-plus-square"></i></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlInvoices" runat="server" CssClass="col-xs-12">
                                        <!-- start -->
                                        <div class="row">
                                            <div class="col-xs-12">
                                                <div class="invoice-head row">
                                                    <div class="col-xs-12 col-md-2 invoice-title">
                                                        <h2 class="text-center bg-primary ">Invoice</h2>    
                                                    </div>
                                                    <div class="col-xs-12 col-md-3 invoice-head-info">
                                                        <span class='text-muted'>
                                                            <span class='text-muted'><asp:Label ID="lblBilltoCustomer" runat="server"></asp:Label></span><br>
                                                        </span>
                                                    </div>
                                                    <div class="col-xs-12 col-md-3 invoice-head-info"><span class='text-muted'>Invoice # <asp:Label ID="lblOrderNo" runat="server"></asp:Label><br><asp:Label ID="lblOrderDate" runat="server"></asp:Label></span></div>
                                                    <div class="col-xs-12 col-md-3 invoice-logo col-md-offset-1">
                                                        <img alt="" src="../data/invoice/invoice-logo.png" class="img-reponsive">
                                                    </div>
                                                </div>
                                                <div class="clearfix"></div><br>
                                            </div>
                                            <div class="col-xs-6 invoice-infoblock pull-left">
                                                <h4>Sender</h4>
                                                <h3><asp:Label ID="lblSenderCompanyName" runat="server"></asp:Label></h3>
                                                <address><span class='text-muted'><asp:Label ID="lblSenderAddress" runat="server"></asp:Label></span>
                                                </address>
                                            </div>
                                            <div class="col-xs-6 invoice-infoblock text-right">
                                               <h4>Receiver</h4>
                                                    <h3><asp:Label ID="lblReceiverCompanyName" runat="server"></asp:Label></h3>
                                                <address>
                                                    <span class='text-muted'><asp:Label ID="lblReceiverAddress" runat="server"></asp:Label></span>
                                                </address>
                                            </div>
                                        </div>

                                        <div class="row" id="Div1" runat="server">
                                            <div class="col-xs-12">
                                                <h3>Order summary</h3><br>
                                                <div class="table-responsive">
                                                    <table class="table table-hover invoice-table">
                                                        <thead>
                                                            <tr>
                                                                <td><h4>No. Of Packages</h4></td>
                                                                <td class="text-center"><h4>Details</h4></td>
                                                                <td class="text-center"><h4>Rate</h4></td>
                                                                <td>Amount</td>
                                                            </tr>
                                                        </thead>
                                                        <tbody id="Tbody1" runat="server">
                                                            <!-- foreach ($order->lineItems as $line) or some such thing here -->
                                                            <tr>
                                                                <td><asp:Label ID="lblTotalInvoiceContainers" runat="server"></asp:Label></td>
                                                                <td class=""><asp:Label ID="lblInvoiceDescription" runat="server"></asp:Label></td>
                                                                <td class="text-center"><asp:Label ID="lblInvoiceContainerRate" runat="server"></asp:Label></td>
                                                                <td class="text-center"><asp:Label ID="lblInvoicecontainerTotal" runat="server"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                                <td class="">Empty Lift on Charges</td>
                                                                <td class="text-center">1850</td>
                                                                <td class="text-center">1850</td>
                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                                <td class="">Weightment Charges</td>
                                                                <td class="text-center">190</td>
                                                                <td class="text-center">190</td>
                                                            </tr>
                                                            <tr>
                                                                <td>MSC LINE</td>
                                                                <td class="text-center"></td>
                                                                <td class="text-center"></td>
                                                                <td class="text-right"></td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td></td>
                                                                <td class="">
                                                                    <div class="row" id="tblContainers" runat="server">
                                                                        <div class="col-xs-12">
                                                                            <h3>Container's summary</h3><br>
                                                                            <div class="table-responsive">
                                                                                <table class="table table-hover invoice-table">
                                                                                    <thead>
                                                                                        <tr>
                                                                                            <td><h4>Date #</h4></td>
                                                                                            <td class="text-center"><h4>Vehicle #</h4></td>
                                                                                            <td class="text-center"><h4>Container #</h4></td>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tbody id="containersList" runat="server"></tbody>
                                                                                </table>
                                                                            </div>
                                                                        <div class="clearfix"></div><br>
                                                                        </div>
                                                                    </div>
                                                                </td>
                                                                <td class="text-center"></td>
                                                                <td class="text-right"></td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <table class="table table-hover invoice-table">
                                                        <%--<tr>
                                                            <td class="thick-line"></td>
                                                            <td class="thick-line"></td>
                                                            <td class="thick-line text-center"><h4>Subtotal</h4></td>
                                                            <td class="thick-line text-right"><h4>$1670.99</h4></td>
                                                        </tr>
                                                        <tr>
                                                            <td class="no-line"></td>
                                                            <td class="no-line"></td>
                                                            <td class="no-line text-center"><h4>Shipping</h4></td>
                                                            <td class="no-line text-right"><h4>$15</h4></td>
                                                        </tr>
                                                        <tr>
                                                            <td class="no-line"></td>
                                                            <td class="no-line"></td>
                                                            <td class="no-line text-center"><h4>VAT</h4></td>
                                                            <td class="no-line text-right"><h4>$150.23</h4></td>
                                                        </tr>--%>
                                                        <tr>
                                                            <td class="no-line"></td>
                                                            <td class="no-line"></td>
                                                            <td class="no-line text-center"><h4>Total</h4></td>
                                                            <td class="no-line text-right"><h3 style='margin:0px;' class="text-primary"><asp:Label ID="lblInvoiceGrandTotal" runat="server"></asp:Label></h3></td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            <div class="clearfix"></div><br>
                                            </div>
                                        </div>

                                        

                                        <div class="row">
                                            <div class="col-xs-12 text-center">
                                                <a href="#" onclick="Print();" class="btn btn-purple btn-md"><i class="fa fa-print"></i> &nbsp; Print </a>        
                                                <%--<a href="#" target="_blank" class="btn btn-accent btn-md"><i class="fa fa-send"></i> &nbsp; Send </a>        --%>
                                            </div>
                                        </div>
                                        <!-- end -->
                                    </asp:Panel>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                            <ContentTemplate>
                                <ajaxToolkit:ModalPopupExtender ID="modalSalesTaxConfirm" runat="server" PopupControlID="pnlSalesTaxConfirm" DropShadow="True" TargetControlID="lnkOpenSalesTaxConfirm" 
                                    CancelControlID="lnkCloseSalesTaxConfirm" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlSalesTaxConfirm" runat="server" CssClass="box " Width="600px" DefaultButton="lnkSaveSalesTax">
                                    <header class="panel_header">
                                        <h2 class="title pull-left">Transaction</h2>
                                        <div class="actions panel_actions pull-right">
                                            <asp:LinkButton ID="lnkOpenSalesTaxConfirm" runat="server" CssClass="box_close fa fa-times" style="display:none;"></asp:LinkButton>
                                            <asp:LinkButton ID="lnkCloseSalesTaxConfirm" runat="server" CssClass="box_close fa fa-times" style="display:none;"></asp:LinkButton>
                                            <asp:LinkButton ID="lnkCloseSalesTaxConfirms" runat="server" CssClass="btn btn-danger btn-xs m-t-15 m-r-5" style="margin-right: 5px;" OnClick="lnkCloseSalesTaxConfirms_Click"><i class="fa fa-times-circle"></i></asp:LinkButton>
                                        </div>
                                    </header>
                                    <div class="content-body">
                                        <div class="row">
                                
                                            <div class="col-md-4 col-sm-12 col-xs-12">
                                                <div class="form-group">
                                                    <label class="form-label" for="email-1">Supplier</label>
                                                    <asp:DropDownList ID="ddlSuppliers" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                
                                            <div class="col-md-4 col-sm-12 col-xs-12">
                                                <div class="form-group">
                                                    <label class="form-label" for="email-1">Buyer</label>
                                                    <asp:DropDownList ID="ddlBuyer" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                
                                            <div class="col-md-4 col-sm-12 col-xs-12">
                                                <div class="form-group">
                                                    <label class="form-label" for="email-1">Invoice Date</label>
                                                    <asp:TextBox ID="txtSTInvoiceDate" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                
                                            <div class="col-md-3 col-sm-12 col-xs-12">
                                                <div class="form-group">
                                                    <label class="form-label" for="email-1">Value Exclusive Tax</label>
                                                    <asp:TextBox ID="txtActualAmount" runat="server" TextMode="Number" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                
                                            <div class="col-md-3 col-sm-12 col-xs-12">
                                                <div class="form-group">
                                                    <label class="form-label" for="email-1">Sales Tax Percentage</label>
                                                    <asp:TextBox ID="txtSalesTaxPercentage" runat="server" TextMode="Number" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                
                                            <div class="col-md-3 col-sm-12 col-xs-12">
                                                <div class="form-group">
                                                    <label class="form-label" for="email-1">Sales Tax Amount</label>
                                                    <asp:TextBox ID="txtSalesTaxAmount" runat="server" TextMode="Number" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                
                                            <div class="col-md-3 col-sm-12 col-xs-12">
                                                <div class="form-group">
                                                    <label class="form-label" for="email-1">Value Inclusive Tax</label>
                                                    <asp:TextBox ID="txtValueInclusiveTax" runat="server" TextMode="Number" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                
                                
                                            <div class="col-md-12 col-sm-12 col-xs-12">
                                                <div class="form-group">
                                                    <label class="form-label" for="email-1"><h4>Are you sure you want to Confirm and Generate Sales Tax Invoice?</h4></label>
                                                    <asp:LinkButton ID="lnkSaveSalesTax" runat="server" CssClass="btn btn-info m-t-30" ToolTip="Click to save Save Sales Tax" OnClick="lnkSaveSalesTax_Click"><i class="fas fa-save"></i> | Confirm</asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                            
                                        <div id="divSalesTaxInvoiceConfirmNotification" runat="server"></div>
                                    </div>
                                </asp:Panel>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        

                        <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                            <ContentTemplate>
                                <ajaxToolkit:ModalPopupExtender ID="modalSTInvoice" runat="server" PopupControlID="pnlSTInvoice" DropShadow="True" TargetControlID="btnOpenSTInvoice" 
                                    CancelControlID="lnkCloseSTInvoice" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlSTInvoice" runat="server" CssClass="row" style="background-color: white; padding: 20px; border: 1px solid black; height: 600px; overflow-y: scroll" Width="1300px">
                            
                                    <asp:Button ID="btnOpenSTInvoice" runat="server" style="display: none" />
                                    <asp:Button ID="btnCloseSTInvoice" runat="server" style="display: none" />
                                    <h4 class="pull-left"><asp:Label ID="Label16" runat="server"></asp:Label></h4> 
                                    <asp:LinkButton ID="lnkCloseSTInvoice" runat="server" ForeColor="Maroon" CssClass="pull-right" OnClick="lnkCloseSTInvoice_Click"><i class="fas fa-times-circle"></i></asp:LinkButton>
                                    <div id="divSTInvoiceNotification" runat="server"></div>
                                    <h2>Sales Tax Invoice</h2>
                                    
                                    <asp:Panel ID="Panel8" runat="server" CssClass="col-xs-12" Visible="false">
                                        <div class="col-xs-12 col-sm-6">
                                            <div class="form-group">
                                                <label class="form-label">Keyword</label>
                                                <div class="controls">
                                                    <asp:CheckBoxList ID="CheckBoxList3" runat="server"></asp:CheckBoxList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xs-12 col-sm-6">
                                            <div class="form-group">
                                                <div class="controls">
                                                    <asp:LinkButton ID="LinkButton5" runat="server" CssClass="btn btn-info m-t-40" OnClick="lnkAddContainers_Click"><i class="fas fa-plus-square"></i></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <asp:Panel ID="Panel9" runat="server" CssClass="col-xs-12">
                                        <!-- start -->
                                        <header class="clearfix">
                                            <div id="logo">
                                                <img src="../assets/images/MZBLogo.png" style="width:15%;">
                                                <img src="../assets/images/MZBLogo2.png" style="width:33%; float: right;">
                                            </div>
                                            
                                        </header>
                                        <main>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td>
                                                        <table style="width: 100%;" border="1">
                                                            <tr>
                                                                <td style="width: 15%; vertical-align: top;">
                                                                    Supplier's Name & Address
                                                                </td>
                                                                <td style="width: 35%">
                                                                    <asp:Label ID="lblSTSuppliersNameAddress" runat="server" Text="Malik Zafar & Brothers Plot# 2, Office# 7, Gate# 3, near MCB bank, Cafe Jilani Building, Quaid e Azam Truck Stand, Hawksbay Road, Karachi"></asp:Label>
                                                                </td>
                                                                <td style="width: 15%; vertical-align: top;">
                                                                    Buyer's Name & Address
                                                                </td>
                                                                <td style="width: 35%">
                                                                    <asp:Label ID="lblSTBuyersNameAddress" runat="server" Text="Al Razaq Fibres (PVT) Ltd, Plot# 28/B, Block 6, PECHS, Shahrah e Faisal, Karachi."></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 15%; vertical-align: top;">
                                                                    Telephone#
                                                                </td>
                                                                <td style="width: 35%">
                                                                    <asp:Label ID="lblSTSupllierPhone" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="width: 15%; vertical-align: top;">
                                                                    Telephone#
                                                                </td>
                                                                <td style="width: 35%">
                                                                    <asp:Label ID="lblSTBuyerPhone" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 15%; vertical-align: top;">
                                                                    N.T.N
                                                                </td>
                                                                <td style="width: 35%">
                                                                    <asp:Label ID="lblSTSupplierNTN" runat="server" Text="45665406-3"></asp:Label>
                                                                </td>
                                                                <td style="width: 15%; vertical-align: top;">
                                                                    N.T.N
                                                                </td>
                                                                <td style="width: 35%">
                                                                    <asp:Label ID="lblSTBuyerNTN" runat="server" Text="45665406-3"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Terms of sale: 
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table style="width: 100%;" border="1">
                                                            <thead>
                                                                <tr>
                                                                    <th>
                                                                        Description of Goods
                                                                    </th>
                                                                    <th>
                                                                        Value of Goods
                                                                    </th>
                                                                    <th>
                                                                        Rate of Sales Tax
                                                                    </th>
                                                                    <th>
                                                                        Amount on Sales Tax
                                                                    </th>
                                                                    <th>
                                                                        Total
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        Goods Transport Services
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblSTContainerRate" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblSTPercentage" runat="server"></asp:Label>%
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblSTAmount" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblAmountInclusiveST" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblSTFrom" runat="server"></asp:Label> to <asp:Label ID="lblSTTo" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td>

                                                                    </td>
                                                                    <td>

                                                                    </td>
                                                                    <td>

                                                                    </td>
                                                                    <td>

                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblSTInvoiceNo" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td>

                                                                    </td>
                                                                    <td>

                                                                    </td>
                                                                    <td>

                                                                    </td>
                                                                    <td>

                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblSTInvoiceDate" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td>

                                                                    </td>
                                                                    <td>

                                                                    </td>
                                                                    <td>

                                                                    </td>
                                                                    <td>

                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblSTVehicleRegNo" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td>

                                                                    </td>
                                                                    <td>

                                                                    </td>
                                                                    <td>

                                                                    </td>
                                                                    <td>

                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                            <tfoot>
                                                                <tr>
                                                                    <th>
                                                                        
                                                                    </th>
                                                                    <th colspan="3">
                                                                        Total
                                                                    </th>
                                                                    <th>
                                                                        <asp:Label ID="lblSTTotal" runat="server"></asp:Label>
                                                                    </th>
                                                                </tr>
                                                            </tfoot>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:Label ID="Label31" runat="server" style="display: none;"></asp:Label>
                                            <div id="notices">
                                                <div>NOTICE:</div>
                                                <div class="notice">Contact transportation company or broker in case of debris.</div>
                                            </div>
                                        </main>
                                        <footer>
                                            Invoice was created on a computer and is valid without the signature and seal.
                                       
                                        </footer>
                                        <!-- end -->
                                        <a href="#" onclick="PrintSTInvoice(); return false;" class="btn btn-purple btn-md"><i class="fa fa-print"></i> &nbsp; Print </a>
                                    </asp:Panel>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </section>
            </div>

            <!-- MAIN CONTENT AREA ENDS -->
        </section>
    </section>
    <!-- END CONTENT -->
    
    <script src="../Scripts/jquery-1.7.min.js"></script>
</asp:Content>
