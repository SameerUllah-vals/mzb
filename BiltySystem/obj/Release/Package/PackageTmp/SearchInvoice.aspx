<%@ Page Title="" Language="C#" MasterPageFile="~/BiltySystem.Master" AutoEventWireup="true" CodeBehind="SearchInvoice.aspx.cs" Inherits="BiltySystem.SearchInvoice" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://smtpjs.com/v3/smtp.js"></script>
    <style>        
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }
        .fa-filter-slash:before { content: "\fef7"; }
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
        var styles = "";

        function readTextFile(file) {
            var rawFile = new XMLHttpRequest();
            rawFile.open("GET", file, false);
            rawFile.onreadystatechange = function () {
                if (rawFile.readyState === 4) {
                    if (rawFile.status === 200 || rawFile.status == 0) {
                        var allText = rawFile.responseText;
                        alert(allText);
                        styles = allText;
                    }
                }
            }
            rawFile.send(null);
        }

        function numberWithCommas(x) {
            return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
            alert(1);
        }

        function PrintOLD() {
            debugger;
            //readTextFile("assets/css/style.css");
            
            var InvoiceHTML = "";
            InvoiceHTML += "<div style=\"width: 100%; \">";
                InvoiceHTML += "<div style=\"width: 100%\">";
                    InvoiceHTML += "<div style=\"width: 45%; float: left;\">";
                      //  InvoiceHTML += "<img alt=\"\" src=\"assets/images/MZBLogo.png\" style=\"width: 45%;\" class=\"img-reponsive\">";
                    InvoiceHTML += "</div>";
                    InvoiceHTML += "<div style=\"width: 45%; float: right;\">";
                        //InvoiceHTML += "<img alt=\"\" src=\"assets/images/MZBLogo2.png\" style=\"padding-top: 5%; float: right; width: 85%;\" class=\"img-reponsive\">";
                    InvoiceHTML += "</div>";
                InvoiceHTML += "</div>";
                InvoiceHTML += "<br><br><br>";
                InvoiceHTML += "<div style=\"width: 100%;\">";
                    InvoiceHTML += "<br><br><br>";
                    InvoiceHTML += "<table style=\"width: 100%;\">";
                        InvoiceHTML += "<tr>";
                            InvoiceHTML += "<td style=\"padding: 10px;\"><span>Invoice#</span> <strong>" + document.getElementById('ContentPlaceHolder1_lblOrderNo').innerText +"</strong></div></td>";
                            InvoiceHTML += "<td style=\"padding: 10px;\"><span>Bill To</span> <strong>" + document.getElementById('ContentPlaceHolder1_lblBilltoCustomer').innerText + "</strong></td>";
                        InvoiceHTML += "<tr>";
                        InvoiceHTML += "</tr>";
                            InvoiceHTML += "<td style=\"padding: 10px;\">Sender</span> <strong>" + document.getElementById('ContentPlaceHolder1_lblSenderCompanyName').innerText +"</strong></td>";
                            InvoiceHTML += "<td style=\"padding: 10px;\">Receiver</span> <strong>" + document.getElementById('ContentPlaceHolder1_lblReceiverCompanyName').innerText +"</strong></td>";
                        InvoiceHTML += "</tr>";
                    InvoiceHTML += "</table>";
                InvoiceHTML += "</div>";
                InvoiceHTML += "<br><br><br>";
                InvoiceHTML += "<div style=\"width: 100%;\">";
                    InvoiceHTML += "<table style=\"width: 100%;\" border=\"1\">";
                        InvoiceHTML += "<thead>";
                            InvoiceHTML += "<tr style=\"background-color: #CCC; color: Black; font-size: 20px;\">";
                                InvoiceHTML += "<th style=\"padding: 05px;\">Nos.</th>";
                                InvoiceHTML += "<th style=\"padding: 05px;\">Description</th>";
                                InvoiceHTML += "<th style=\"padding: 05px;\">Rate</th>";
                                InvoiceHTML += "<th style=\"padding: 05px;\">Amount</th>";
                            InvoiceHTML += "</tr>";
                        InvoiceHTML += "</thead>";
                        InvoiceHTML += "<tbody>";
                            InvoiceHTML += "<tr>";
                                InvoiceHTML += "<td style=\"text-align: center; padding: 10px;\">" + document.getElementById('ContentPlaceHolder1_lblTotalInvoiceContainers').innerText + "</td>";
                                InvoiceHTML += "<td style=\"text-align: left; padding: 10px;\">" + document.getElementById('ContentPlaceHolder1_lblInvoiceDescription').innerText + "</td>";
                                InvoiceHTML += "<td style=\"text-align: center; padding: 10px;\">" + numberWithCommas(document.getElementById('ContentPlaceHolder1_lblInvoiceContainerRate').innerText) + "/-</td>";
                                InvoiceHTML += "<td style=\"text-align: center; padding: 10px;\">" + document.getElementById('ContentPlaceHolder1_lblInvoicecontainerTotal').innerText + "/-</td>";
                            InvoiceHTML += "</tr>";
                            var tableExpenses = document.getElementById("ContentPlaceHolder1_tblContainerExpense");
                            var rowLength = tableExpenses.rows.length;
                            for (i = 0; i < rowLength; i++) {
                                
                                var expenseCells = tableExpenses.rows.item(i).cells;
                                var cellLength = expenseCells.length;
                                var ExpenseAmount = expenseCells.item(3).innerHTML;
                                InvoiceHTML += "<tr>";
                                    InvoiceHTML += "<td style=\"text-align: center;\">" + expenseCells.item(0).innerHTML + "</td>";
                                    InvoiceHTML += "<td style=\"text-align: left;\">" + expenseCells.item(1).innerHTML + "</td>";
                                    InvoiceHTML += "<td style=\"text-align: center;\">" + expenseCells.item(2).innerHTML + "</td>";
                                    InvoiceHTML += "<td style=\"text-align: center;\">" + expenseCells.item(3).innerHTML + "</td>";
                                InvoiceHTML += "</tr>";
                                Total = (+Total + +ExpenseAmount);                                
                            }
                            var tableWeighment = document.getElementById("ContentPlaceHolder1_tblCotainerWeighment");
                            var WeighmentLength = tableWeighment.rows.length;
                            for (i = 0; i < WeighmentLength; i++) {
                                
                                var weighmentCells = tableWeighment.rows.item(i).cells;
                                var cellLength = weighmentCells.length;
                                var WeighmentAmount = weighmentCells.item(3).innerHTML;
                                InvoiceHTML += "<tr>";
                                    InvoiceHTML += "<td>" + weighmentCells.item(0).innerHTML + "</td>";
                                    InvoiceHTML += "<td style=\"text-align: left;\">" + weighmentCells.item(1).innerHTML + "</td>";
                                    InvoiceHTML += "<td style=\"text-align: center;\">" + weighmentCells.item(2).innerHTML + "</td>";
                                    InvoiceHTML += "<td style=\"text-align: center;\">" + weighmentCells.item(3).innerHTML + "</td>";
                                InvoiceHTML += "</tr>";
                                Total = (+Total + +WeighmentAmount);                                
                            }
                            InvoiceHTML += "<tr>";
                                InvoiceHTML += "<td></td>";
                                InvoiceHTML += "<td colspan=\"3\" style=\"padding: 10px;\">";
                                    InvoiceHTML += "<div style=\"width: 100%;\">";
                                        InvoiceHTML += "<h3>Containers Summary</h3>";
                                        InvoiceHTML += "<table style=\"width: 100%\">";
                                            InvoiceHTML += "<thead>";
                                                InvoiceHTML += "<tr style=\"background-color: #ccc; color: black; padding: 10px;\">";
                                                    InvoiceHTML += "<th style=\"padding: 10px;\">Date</th>";
                                                    InvoiceHTML += "<th style=\"padding: 10px;\">Vehicle #</th>";
                                                    InvoiceHTML += "<th style=\"padding: 10px;\">Container #</th>";
                                                InvoiceHTML += "</tr>";
                                            InvoiceHTML += "</thead>";
                                            InvoiceHTML += "<tbody>";
                                            var tableContainers = document.getElementById("tblContainersHTML");
                                            var rowLength = tableContainers.rows.length;
                                            var Total = 0;

                                            //loops through rows    
                                            for (i = 0; i < rowLength; i++) {
                                                if (i > 0) {
                                                    var oCells = tableContainers.rows.item(i).cells;
                                                    var cellLength = oCells.length;
                                                    //var rate = oCells.item(4).innerHTML;
                                                    InvoiceHTML += "<tr>";
                                                        InvoiceHTML += "<td style=\"text-align: center;\">" + oCells.item(0).innerHTML + "</td>";
                                                        InvoiceHTML += "<td style=\"text-align: center;\">" + oCells.item(1).innerHTML + "</td>";
                                                        InvoiceHTML += "<td style=\"text-align: center;\">" + oCells.item(2).innerHTML + "</td>";
                                                    InvoiceHTML += "</tr>";
                                                    //Total = (+Total + +rate);
                                                }
                                            }
                                            InvoiceHTML += "</tbody>";
                                        InvoiceHTML += "</table>";
                                    InvoiceHTML += "</div>";
                                InvoiceHTML += "</td>";
                            InvoiceHTML += "</tr>";
            InvoiceHTML += "<tr>";
            InvoiceHTML += "<td>&nbsp;</td>";
                                InvoiceHTML += "<td colspan=\"2\" style=\"\"><h4>Total</h4></td>";
                                //InvoiceHTML += "<td>" + Total + "</td>";
                                InvoiceHTML += "<td><h4>" + document.getElementById('ContentPlaceHolder1_lblInvoiceGrandTotal').innerText + "</h4></td>";
                            InvoiceHTML += "</tr>";
                        InvoiceHTML += "</tbody>";
                    InvoiceHTML += "</table>";
                InvoiceHTML += "</div>";
            InvoiceHTML += "</div>";
            InvoiceHTML += "<br />";

            var prntData = document.getElementById('ContentPlaceHolder1_pnlInvoices');
            var prntWindow = window.open("", "Print", "width=400,height=400,left=0,top=0,toolbar=0,scrollbar=1,status=0");
            var style = "";
            prntWindow.document.write(InvoiceHTML);
            prntWindow.document.close();
            prntWindow.focus();
            prntWindow.print();
            prntWindow.close();
        }

        function Print() {
            debugger;
            var InvoiceHTML = "";
            InvoiceHTML += "<div style=\"width: 100%; margin-bottom: 0;\">";
            InvoiceHTML += "<div style=\"width: 100%\">";
            InvoiceHTML += "<div style=\"width: 45%; float: left;\">";
            InvoiceHTML += "<img alt=\"\" src=\"../assets/images/MZBLogo.png\" style=\"width: 45%;\" class=\"img-reponsive\">";
            InvoiceHTML += "</div>";
            InvoiceHTML += "<div style=\"width: 45%; float: right;\">";
            //InvoiceHTML += "<img alt=\"\" src=\"assets/images/MZBLogo2.png\" style=\"padding-top: 5%; float: right; width: 85%;\" class=\"img-reponsive\">";
            InvoiceHTML += "</div>";
            InvoiceHTML += "</div>";
            InvoiceHTML += "<br><br><br>";
            InvoiceHTML += "<div style=\"width: 100%;\">";
            InvoiceHTML += "<br><br><br>";
            InvoiceHTML += "<table style=\"width: 100%;\">";
            InvoiceHTML += "<tr>";
            InvoiceHTML += "<td style=\"padding: 5px;\"><span>Bill #</span> <strong>" + document.getElementById('ContentPlaceHolder1_lblBillNo').innerText + "</strong></td>";
            InvoiceHTML += "<td style=\"padding: 5px;text-align:right\"><span>Customer Invoice #</span> <strong>" + document.getElementById('ContentPlaceHolder1_lblCustomerBillNo').innerText + "</strong></td>";
            InvoiceHTML += "</tr>";
            InvoiceHTML += "<tr>";
            InvoiceHTML += "<td style=\"padding: 5px;\"><span>Bill Date</span> <strong>" + document.getElementById('ContentPlaceHolder1_lblBillDate').innerText + "</strong></td>";
            InvoiceHTML += "<td style=\"padding: 5px;text-align:right\"><span>Customer</span> <strong>" + document.getElementById('ContentPlaceHolder1_lblPartyName').innerText + "</strong></td>";
            InvoiceHTML += "</tr>";
            InvoiceHTML += "<tr>";
            InvoiceHTML += "<td colspan=\"2\">";
            InvoiceHTML += "<table style=\"width: 100%;\">";
            InvoiceHTML += "<tr>";
            InvoiceHTML += "<td style=\"padding: 5px;\"><span>Shipping</span> <strong>" + document.getElementById('ContentPlaceHolder1_lblShippingLine').innerText + "</strong></td>";
            InvoiceHTML += "<td style=\"padding: 5px;text-align:right\"><span>Clearing Agent</span> <strong>" + document.getElementById('ContentPlaceHolder1_lblClearingAgent').innerText + "</strong></td>";
            InvoiceHTML += "</tr>";
            InvoiceHTML += "</table>";
            InvoiceHTML += "</td>";
            InvoiceHTML += "</tr>";
            //    InvoiceHTML += "<td style=\"padding: 5px\"><span>Shipping Line</span> <strong>" + document.getElementById('ContentPlaceHolder1_lblShippingLine').innerText + "</strong></td>";
            //    InvoiceHTML += "<td style=\"padding: 5px\"><span>Clearing Agent</span> <strong>" + document.getElementById('ContentPlaceHolder1_lblClearingAgent').innerText +"</strong></td>";
            //InvoiceHTML += "</tr>";
            debugger;
            InvoiceHTML += "<tr>";
            InvoiceHTML += "<td style=\"padding: 5px\"><span>Remarks</span> <strong>" + document.getElementById('ContentPlaceHolder1_lblRemarks').innerText + "</strong></td>";
            InvoiceHTML += "<td style=\"padding: 5px;text-align:right\"><span>Containers Qty</span> <strong>" + document.getElementById('ContentPlaceHolder1_lblContainersQty').innerText + "</strong></td>";
            InvoiceHTML += "</tr>";
            InvoiceHTML += "</table>";
            InvoiceHTML += "</div>";
            InvoiceHTML += "<br><br><br>";
            InvoiceHTML += "<div style=\"width: 100%;\">";
            InvoiceHTML += "<table style=\"width: 100%;\" border=\"1\">";
            InvoiceHTML += "<thead>";
            InvoiceHTML += "<tr style=\"background-color: #CCC; color: Black; font-size: 15px;\">";
            InvoiceHTML += "<th style=\"padding: 05px;\">Bilty#</th>";
            InvoiceHTML += "<th style=\"padding: 05px;\">Date</th>";
            InvoiceHTML += "<th style=\"padding: 05px;\">Container</th>";
            InvoiceHTML += "<th style=\"padding: 05px;\">Truck</th>";
            InvoiceHTML += "<th style=\"padding: 05px;\">From</th>";
            InvoiceHTML += "<th style=\"padding: 05px;\">To</th>";
            //InvoiceHTML += "<th style=\"padding: 05px;\">Expenses</th>";
            InvoiceHTML += "<th style=\"padding: 05px;\">LoLo</th>";
            InvoiceHTML += "<th style=\"padding: 05px;\">Weighment</th>";
            InvoiceHTML += "<th style=\"padding: 05px;\">Rate</th>";
            InvoiceHTML += "<th style=\"padding: 05px;\">Amount</th>";
            InvoiceHTML += "</tr>";
            InvoiceHTML += "</thead>";
            InvoiceHTML += "<tbody>";
            var table = document.getElementById("ContentPlaceHolder1_Tbody1");
            var rowLength = table.rows.length;
            for (i = 0; i < rowLength; i++) {

                var cell = table.rows.item(i).cells;
                var cellLength = cell.length;
                InvoiceHTML += "<tr>";
                InvoiceHTML += "<td style=\"text-align: center; font-size: 12px; font-size: 12px;\">" + cell.item(0).innerHTML + "</td>";
                InvoiceHTML += "<td style=\"text-align: center; font-size: 12px;\">" + cell.item(1).innerHTML + "</td>";
                InvoiceHTML += "<td style=\"text-align: center; font-size: 12px;\">" + cell.item(2).innerHTML + "</td>";
                InvoiceHTML += "<td style=\"text-align: center; font-size: 12px;\">" + cell.item(3).innerHTML + "</td>";
                InvoiceHTML += "<td style=\"text-align: center; font-size: 12px;\">" + cell.item(4).innerHTML + "</td>";
                InvoiceHTML += "<td style=\"text-align: center; font-size: 12px;\">" + cell.item(5).innerHTML + "</td>";
                InvoiceHTML += "<td style=\"text-align: center; font-size: 12px;\">" + numberWithCommas(cell.item(6).innerHTML) + "</td>";
                InvoiceHTML += "<td style=\"text-align: center; font-size: 12px;\">" + cell.item(7).innerHTML + "</td>";
                InvoiceHTML += "<td style=\"text-align: center; font-size: 12px;\">" + numberWithCommas(cell.item(8).innerHTML) + "</td>";
                //InvoiceHTML += "<td style=\"text-align: center; font-size: 12px;\">" + numberWithCommas(cell.item(9).innerHTML) + "</td>";
                InvoiceHTML += "<td style=\"text-align: center; font-size: 12px;\">" + numberWithCommas(cell.item(9).innerHTML) + "</td>";
                InvoiceHTML += "</tr>";
            }
            InvoiceHTML += "<tr>";
            InvoiceHTML += "<td colspan=\"9\" style=\"text-align: right; vertical-align: bottom;\">Total</td>";
            InvoiceHTML += "<td style=\"text-align: center; font-size: 12px;\">" + document.getElementById('ContentPlaceHolder1_lblInvoiceGrandTotal').innerText + "</td>";
            InvoiceHTML += "</tr>";
            InvoiceHTML += "</tbody>";
            InvoiceHTML += "</table>";
            InvoiceHTML += "<h4 style=\"text-decoration: underline;\">" + document.getElementById('ContentPlaceHolder1_lblAmountinWords').innerText + "</h4>";
            InvoiceHTML += "</div>";
            InvoiceHTML += "</div>";
            InvoiceHTML += "<br />";







            InvoiceHTML += '  <div class="col-md-4" style="display:inline-block ; float:right; width:330px">';

            InvoiceHTML += '<label>For _________________________</label></div >';



            InvoiceHTML += "<div style=\"padding: 5px;  left: 0; bottom: 0;\"> <p style=\"text-decoration: underline; text-align: center;\">Developed by: Vals Technologies | PABX: 0304 111 66 88 | www.valstechnologies.com</p></div>";

            var prntData = document.getElementById('ContentPlaceHolder1_pnlInvoices');
            var prntWindow = window.open("", "Print", "width=400,height=400,left=0,top=0,toolbar=0,scrollbar=1,status=0");
            var style = "";
            prntWindow.document.write(InvoiceHTML);
            prntWindow.document.close();
            prntWindow.focus();
            prntWindow.print();
            prntWindow.close();
        }

        function EmailInvoice() {
            debugger;
            var InvoiceHTML = "";
            InvoiceHTML += "<div style=\"width: 100%; margin-bottom: 0;\">";
            InvoiceHTML += "<div style=\"width: 100%\">";
            InvoiceHTML += "<div style=\"width: 45%; float: left;\">";
            InvoiceHTML += "<img alt=\"\" src=\"http://valstechnologies.com/images/mzb.jpeg \" style=\"width: 45%;\" class=\"img-reponsive\">";
            InvoiceHTML += "</div>";
            InvoiceHTML += "<div style=\"width: 45%; float: right;\">";
            InvoiceHTML += "<img alt=\"\" src=\"http://valstechnologies.com/images/mzb1.jpeg \" style=\"padding-top: 5%; float: right; width: 85%;\" class=\"img-reponsive\">";
            InvoiceHTML += "</div>";
            InvoiceHTML += "</div>";
            InvoiceHTML += "<br><br><br>";
            InvoiceHTML += "<div style=\"width: 100%;\">";
            InvoiceHTML += "<br><br><br>";
            InvoiceHTML += "<table style=\"width: 100%;\" class=\"headings\">";
            InvoiceHTML += "<tr>";
            InvoiceHTML += "<td style=\"padding: 5px;\"><span>Bill #</span> <strong>" + document.getElementById('ContentPlaceHolder1_lblBillNo').innerText + "</strong></td>";
            InvoiceHTML += "<td style=\"padding: 5px;text-align:right\"><span>Customer Invoice #</span> <strong>" + document.getElementById('ContentPlaceHolder1_lblCustomerBillNo').innerText + "</strong></td>";
            InvoiceHTML += "</tr>";
            InvoiceHTML += "<tr>";
            InvoiceHTML += "<td style=\"padding: 5px;\"><span>Bill Date</span> <strong>" + document.getElementById('ContentPlaceHolder1_lblBillDate').innerText + "</strong></td>";
            InvoiceHTML += "<td style=\"padding: 5px;text-align:right\"><span>Customer</span> <strong>" + document.getElementById('ContentPlaceHolder1_lblPartyName').innerText + "</strong></td>";
            InvoiceHTML += "</tr>";
            InvoiceHTML += "<tr>";
            InvoiceHTML += "<td colspan=\"2\">";
            InvoiceHTML += "<table style=\"width: 100%;\">";
            InvoiceHTML += "<tr>";
            InvoiceHTML += "<td style=\"padding: 5px;\"><span>Shipping</span> <strong>" + document.getElementById('ContentPlaceHolder1_lblShippingLine').innerText + "</strong></td>";
            InvoiceHTML += "<td style=\"padding: 5px;text-align:right\"><span>Clearing Agent</span> <strong>" + document.getElementById('ContentPlaceHolder1_lblClearingAgent').innerText + "</strong></td>";
            InvoiceHTML += "</tr>";
            InvoiceHTML += "</table>";
            InvoiceHTML += "</td>";
            InvoiceHTML += "</tr>";
            //    InvoiceHTML += "<td style=\"padding: 5px\"><span>Shipping Line</span> <strong>" + document.getElementById('ContentPlaceHolder1_lblShippingLine').innerText + "</strong></td>";
            //    InvoiceHTML += "<td style=\"padding: 5px\"><span>Clearing Agent</span> <strong>" + document.getElementById('ContentPlaceHolder1_lblClearingAgent').innerText +"</strong></td>";
            //InvoiceHTML += "</tr>";
            debugger;
            InvoiceHTML += "<tr>";
            InvoiceHTML += "<td style=\"padding: 5px\"><span>Remarks</span> <strong>" + document.getElementById('ContentPlaceHolder1_lblRemarks').innerText + "</strong></td>";
            InvoiceHTML += "<td style=\"padding: 5px;text-align:right\"><span>Containers Qty</span> <strong>" + document.getElementById('ContentPlaceHolder1_lblContainersQty').innerText + "</strong></td>";
            InvoiceHTML += "</tr>";
            InvoiceHTML += "</table>";
            InvoiceHTML += "</div>";
            InvoiceHTML += "<br><br><br>";
            InvoiceHTML += "<div style=\"width: 100%;\">";
            InvoiceHTML += "<table style=\"width: 100%;\" border=\"1\" class=\"grids\">";
            InvoiceHTML += "<thead >";
            InvoiceHTML += "<tr style=\"background-color: #CCC; color: Black; font-size: 8px;font-weight:bold\">";
            InvoiceHTML += "<th style=\"padding: 05px;\">Bilty#</th>";
            InvoiceHTML += "<th style=\"padding: 05px;\">Date</th>";
            InvoiceHTML += "<th style=\"padding: 05px;\">Container</th>";
            InvoiceHTML += "<th style=\"padding: 05px;\">Truck</th>";
            InvoiceHTML += "<th style=\"padding: 05px;\">From</th>";
            InvoiceHTML += "<th style=\"padding: 05px;\">To</th>";
            //InvoiceHTML += "<th style=\"padding: 05px;\">Expenses</th>";
            InvoiceHTML += "<th style=\"padding: 05px;\">LoLo</th>";
            InvoiceHTML += "<th style=\"padding: 05px;\">Weighment</th>";
            InvoiceHTML += "<th style=\"padding: 05px;\">Rate</th>";
            InvoiceHTML += "<th style=\"padding: 05px;\">Amount</th>";
            InvoiceHTML += "</tr>";
            InvoiceHTML += "</thead>";
            InvoiceHTML += "<tbody style=\"color: Black; font-size: 9px;\">";
            var table = document.getElementById("ContentPlaceHolder1_Tbody1");
            var rowLength = table.rows.length;
            for (i = 0; i < rowLength; i++) {

                var cell = table.rows.item(i).cells;
                var cellLength = cell.length;
                debugger;
                InvoiceHTML += "<tr>";
                InvoiceHTML += "<td style=\"text-align: center; font-size: 8px; font-size: 8px;\">" + cell.item(0).innerHTML + "</td>";
                InvoiceHTML += "<td style=\"text-align: center; font-size: 8px;\">" + cell.item(1).innerHTML + "</td>";
                InvoiceHTML += "<td style=\"text-align: center; font-size: 8px;\">" + cell.item(2).innerHTML + "</td>";
                InvoiceHTML += "<td style=\"text-align: center; font-size: 8px;\">" + cell.item(3).innerHTML + "</td>";
                InvoiceHTML += "<td style=\"text-align: center; font-size: 8px;\">" + cell.item(4).innerHTML + "</td>";
                InvoiceHTML += "<td style=\"text-align: center; font-size: 8px;\">" + cell.item(5).innerHTML + "</td>";
                InvoiceHTML += "<td style=\"text-align: center; font-size: 8px;\">" + numberWithCommas(cell.item(6).innerHTML) + "</td>";
                InvoiceHTML += "<td style=\"text-align: center; font-size: 8px;\">" + cell.item(7).innerHTML + "</td>";
                InvoiceHTML += "<td style=\"text-align: center; font-size: 8px;\">" + numberWithCommas(cell.item(8).innerHTML) + "</td>";
               // InvoiceHTML += "<td style=\"text-align: center; font-size: 12px;\">" + numberWithCommas(cell.item(9).innerHTML) + "</td>";
                InvoiceHTML += "<td style=\"text-align: center; font-size: 8px;\">" + numberWithCommas(cell.item(9).innerHTML) + "</td>";
                InvoiceHTML += "</tr>";
            }
            InvoiceHTML += "<tr>";
            InvoiceHTML += "<td colspan=\"9\" style=\"text-align: right; vertical-align: bottom;\">Total</td>";
            InvoiceHTML += "<td style=\"text-align: center; font-size: 8px;\">" + document.getElementById('ContentPlaceHolder1_lblInvoiceGrandTotal').innerText + "</td>";
            InvoiceHTML += "</tr>";
            InvoiceHTML += "</tbody>";
            InvoiceHTML += "</table>";
            InvoiceHTML += "<h4 style=\"text-decoration: underline;\">" + document.getElementById('ContentPlaceHolder1_lblAmountinWords').innerText + "</h4>";
            InvoiceHTML += "</div>";
            InvoiceHTML += "</div>";
            InvoiceHTML += "<br />";







            InvoiceHTML += '  <div class="col-md-4" style="display:inline-block ; float:right; width:330px">';

            InvoiceHTML += '<label>For _________________________</label></div >';



            InvoiceHTML += "<div style=\"padding: 5px;  left: 0; bottom: 0;\"> <p style=\"text-decoration: underline; text-align: center;\">Developed by: Vals Technologies | PABX: 0304 111 66 88 | www.valstechnologies.com</p></div>";

            var to = $("#ContentPlaceHolder1_txtSend").val();
            if (to.replace(" ", "") == "") {
                alert("Please enter email address first");
                $("#ContentPlaceHolder1_txtSend").focus();
            } else {
                var email = document.getElementById('<%=txtSend.ClientID %>').value;
                PageMethods.SendEmail("MZ Cargo", InvoiceHTML, email, onSucess, onError);
            }

            
            //window.open('mailto:vals.naveed@gmail.com?subject=Testing&body=' + InvoiceHTML);
            
        }


        //function sendEmail(to, subject, body) {
        //    Email.send({
        //        Host: "smtp.gmail.com",
        //        Username: "vals.mzb@gmail.com",
        //        Password: "Valspakistan",
        //        To: to,
        //        From: "MZ Cargo",
        //        Subject: subject,
        //        Body: body,
        //    }).then(
        //        message => alert("mail sent successfully")
        //    );
        //}
        function onSucess(result) {
            alert(result);
        }

        function onError(result) {
            alert('Cannot process your request at the moment, please try later.');
        }
    </script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
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

                    <!-- MAIN CONTENT AREA STARTS -->
                    
                    <div id="divNotification" style="margin-top: 10px;" runat="server"></div>
                    <div class="col-xs-12" id="pnlSearchInvoicces" runat="server" visible="false">
                        <section class="box ">
                            <header class="panel_header">
                                <h2 class="title pull-left">Search</h2>
                                <asp:LinkButton ID="lnkCloseSearch" runat="server" ForeColor="Maroon" CssClass="m-t-30 m-r-10 pull-right" OnClick="lnkCloseSearch_Click"><i class="fas fa-times-circle"></i></asp:LinkButton>
                            </header>
                            <div class="content-body">
                                <div class="row">
                                    <div class="col-md-4 col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="form-label" for="email-1">Invoice No.</label>
                                            <asp:TextBox ID="txtInvoiceNo" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-8 col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="form-label" for="email-1">Customer</label>
                                            <asp:DropDownList ID="ddlBilToCustomer" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="form-label" for="email-1">From</label>
                                            <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="form-label" for="email-1">To</label>
                                            <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <asp:LinkButton ID="lnkSearch" runat="server" CssClass="btn btn-primary" OnClick="lnkSearch_Click"><i class="fas fa-search"></i> | Search</asp:LinkButton>
                                            <asp:LinkButton ID="lnkClearFilter" runat="server" CssClass="btn btn-danger pull-right" OnClick="lnkClearFilter_Click"><i class="fas fa-ban"></i> | Clear Filters</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </section>
                    </div>
                    <div class="col-xs-12">
                        <section class="box ">
                            <header class="panel_header">
                                <h2 class="title pull-left">Bills</h2>
                                <asp:LinkButton ID="lnkSearchInvoices" runat="server" CssClass="btn btn-xs btn-primary pull-right m-t-30 m-r-10" OnClick="lnkSearchInvoices_Click"><i class="fas fa-search"></i> | Search Bills</asp:LinkButton>
                                <asp:LinkButton ID="lnkGenerateInvoice" runat="server" CssClass="btn btn-xs btn-info pull-right m-r-10 m-t-30" OnClick="lnkGenerateInvoice_Click"><i class="fas fa-file-invoice"></i> | Generate Bills</asp:LinkButton>
                                <asp:LinkButton ID="lnkPrint" runat="server" CssClass="btn btn-xs btn-info pull-right m-r-10 m-t-30" OnClick="lnkPrint_Click"><i class="fas fa-file-invoice"></i> | Print Bills</asp:LinkButton>
                            </header>
                            <div class="content-body">
                                <div class="row">
                                    <asp:Panel ID="pnlBills" runat="server" CssClass="col-md-12 col-sm-12 col-xs-12 pull-left">
                                        <asp:HiddenField ID="hfSelectedBill" runat="server" />
                                        <asp:HiddenField ID="hfSelectedBillCustomer" runat="server" />
                                        <asp:GridView ID="gvInvoice" runat="server" CssClass="table table-hover" AutoGenerateColumns="false" Font-Size="10px" DataKeyNames="BillNo, ActualBillNo, CreditLimit, isPaid, TotalBalance, CustCodeID, CustomerCompany"
                                            OnRowCommand="gvInvoice_RowCommand" OnRowDataBound="gvInvoice_RowDataBound" AllowPaging="true" AllowSorting="true" OnSorting="gvInvoice_Sorting" 
                                            OnPageIndexChanging="gvInvoice_PageIndexChanging" PageSize="30" PagerSettings-Position="TopAndBottom" PagerStyle-HorizontalAlign="Center" PagerSettings-FirstPageText="<<" PagerSettings-LastPageText=">>">
                                            <Columns>
                                                <asp:BoundField DataField="InvoiceDate" SortExpression="CreatedDate" HeaderText="Date" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                                
                                                <asp:BoundField DataField="BillNo" SortExpression="BillNo" HeaderText="Bill #" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="CustomerCompany" SortExpression="CustomerCompany" HeaderText="Customer" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="BiltyDate" SortExpression="BiltyDate" HeaderText="Bilty Date" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="TotalContainers" SortExpression="TotalContainers" HeaderText="Containers" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="ShippingLine" SortExpression="ShippingLine" HeaderText="Shipping" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                                <asp:TemplateField HeaderText="Total">
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
                                                        <asp:LinkButton ID="lnkReceivePayment"  runat="server" Font-Size="12px" ForeColor="SeaGreen" ToolTip="Click to receive payment" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="Payment"><i class="fas fa-money-bill-alt"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkPrintInvoice" runat="server" Font-Size="12px" ToolTip="Click to Print this invoice" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="PrintBill"><i class="fas fa-print"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEditBill" runat="server" Font-Size="12px" ToolTip="Click to Edit Bill" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="EditBill"><i class="fas fa-plus"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                     <asp:LinkButton ID="lnkDeleteBill" runat="server" Font-Size="12px" ForeColor="Maroon" ToolTip="Click to Delete Bill" OnClientClick="return DeleteBill();" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="DeleteBill"><i class="fas fa-trash"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                            <PagerStyle CssClass="pagination-ys" />
                                        </asp:GridView>                                        
                                    </asp:Panel>
                                    
                                    <asp:Panel ID="pnlSSRSReport" style="display:none" runat="server" CssClass="col-md-12 col-sm-12 col-xs-12 pull-left">
                                        <asp:LinkButton CssClass="fas fa-times pull-right" OnClick="Unnamed_Click" runat="server" />
                                        <%-- SSRS REPORT --%>
                                        <rsweb:reportviewer ID="rvInvoices" runat="server" Width="100%" Height="800px"></rsweb:reportviewer>
                                        <%-- SSRS REPORT --%>                                        
                                    </asp:Panel>

                              <%--      <asp:Panel ID="pnlSSRSreportWithDetails" Visible="false" runat="server" CssClass="col-md-12 col-sm-12 col-xs-12 pull-left">
                                        <asp:LinkButton CssClass="fas fa-times pull-right" ID="lnkPnlReportWithDetails" OnClick="lnkPnlReportWithDetails_Click" runat="server" />
                                      
                                        <rsweb:reportviewer ID="rvReportwithDetails" runat="server" Width="100%" Height="800px" ShowPageNavigationControls="False"></rsweb:reportviewer>
                                      
                                    </asp:Panel>--%>
                                </div>
                                <ajaxToolkit:ModalPopupExtender ID="modalInvoice" runat="server" PopupControlID="pnlInvoice" DropShadow="True" TargetControlID="btnOpenInvoice" 
                                    CancelControlID="lnkCloseInvoice" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlInvoice" runat="server" CssClass="row" style="background-color: white; padding: 20px; border: 1px solid black; height: 600px; overflow-y: scroll" Width="1100px">
                            
                                    <asp:Button ID="btnOpenInvoice" runat="server" style="display: none" />
                                    <asp:LinkButton ID="lnkCloseInvoice" runat="server" ForeColor="Maroon" CssClass="pull-right" style="display: none;"><i class="fa fa-times-circle-o"></i></asp:LinkButton>
                                    <h4 class="pull-left"><asp:Label ID="Label7" runat="server"></asp:Label></h4> 
                                    <asp:LinkButton ID="lnkCloseInvoices" runat="server" ForeColor="Maroon" CssClass="pull-right"><i class="fas fa-times-circle"></i></asp:LinkButton>
                                    <div id="divInvoiceNotification" runat="server"></div>
                                    <h2>Invoice</h2>
                                    
                                    <asp:Panel ID="pnlContainerSelection" runat="server" CssClass="col-xs-12" Visible="false">
                                        <div class="col-xs-12 col-sm-6">
                                            <div class="form-group">
                                                <label class="form-label">Keyword</label>
                                                <div class="controls">
                                                    <asp:CheckBoxList  ID="cbOrderContainers" runat="server"></asp:CheckBoxList>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xs-12 col-sm-6">
                                            <div class="form-group">
                                                <div class="controls">
                                                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-info m-t-40"><i class="fas fa-plus-square"></i></asp:LinkButton>
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
                                                        <img alt="" src="assets/images/MZBLogo.png" style="width: 100%" class="img-reponsive">
                                                    </div>
                                                
                                                    <div class="col-xs-12 col-md-3 invoice-head-info">
                                                        <span class='text-muted'>
                                                            Customer Bill# <asp:TextBox ID="txtCustInvoice" runat="server"></asp:TextBox>
                                                            <br>
                                                            <asp:Label ID="lblInvoiceDate" runat="server"></asp:Label>
                                                        </span>
                                                        
                                                    </div>
                                                    <div class="col-xs-12 col-md-3 invoice-logo col-md-offset-1">
                                                        <%--<img alt="" src="../data/invoice/invoice-logo.png" class="img-reponsive">--%>
                                                        <img alt="" src="assets/images/MZBLogo2.png" style="padding-top: 5%;" class="img-reponsive">
                                                    </div>
                                                </div>
                                                <div class="clearfix"></div><br>
                                            </div>
                                            <div class="col-xs-4 invoice-infoblock pull-left">
                                                <h4>Customer:</h4>
                                                <h3><asp:Label ID="lblPartyName" runat="server"></asp:Label></h3>
                                            </div>
                                            <div class="col-xs-4 invoice-infoblock pull-left">
                                                <h4>Bill No:</h4>
                                                <h3><asp:Label ID="lblBillNo" runat="server"></asp:Label></h3>
                                            </div>
                                            <div class="col-xs-4 invoice-infoblock pull-left">
                                                <h4>Customer Bill #</h4>
                                                <h3><asp:Label ID="lblCustomerBillNo" runat="server"></asp:Label></h3>
                                            </div>
                                            <div class="col-xs-4 invoice-infoblock pull-left">
                                                <h4>Clearing Agent</h4>
                                                <h3><asp:Label ID="lblClearingAgent" runat="server"></asp:Label></h3>
                                            </div>
                                            <div class="col-xs-4 invoice-infoblock pull-left">
                                                <h4>Shipping Line</h4>
                                                <h3><asp:Label ID="lblShippingLine" runat="server"></asp:Label></h3>
                                            </div>
                                            <div class="col-xs-4 invoice-infoblock pull-left">
                                                <h4>Bill Date</h4>
                                                <h3><asp:Label ID="lblBillDate" runat="server"></asp:Label></h3>
                                            </div>
                                            <div class="col-xs-6 invoice-infoblock pull-left">
                                                <h4>Containers Qty</h4>
                                                <h3><asp:Label ID="lblContainersQty" runat="server"></asp:Label></h3>
                                            </div>
                                            <div class="col-xs-6 invoice-infoblock pull-left">
                                                <h4>Remarks</h4>
                                                <h3><asp:Label ID="lblRemarks" runat="server"></asp:Label></h3>
                                            </div>
                                        </div>

                                        <div class="row" id="Div1" runat="server">
                                            <div class="col-xs-12">
                                                <%--<h3>Order summary</h3><br>--%>
                                                <div class="table-responsive">

                                                    <table class="table table-hover invoice-table">
                                                        <thead>
                                                            <tr>
                                                                <td class="text-center">Bilty #</td>
                                                                <td class="text-center">Date</td>
                                                                <td class="text-center">Container</td>
                                                                <td class="text-center">Truck</td>
                                                                <td class="text-center">From</td>
                                                                <td class="text-center">To</td>
                                                                <td class="text-center">Rate</td>
                                                                <td class="text-center">LoLo</td>
                                                                <td class="text-center">Weighment</td>
                                                                <%--<td class="text-center">Expenses</td>--%>
                                                                <td class="text-center">Total</td>
                                                            </tr>
                                                        </thead>
                                                        <tbody id="Tbody1" runat="server">
                                                            <!-- foreach ($order->lineItems as $line) or some such thing here -->
                                                            <%--<tr>
                                                                <td><asp:Label ID="lblTotalInvoiceContainers" runat="server"></asp:Label></td>
                                                                <td class=""><asp:Label ID="lblInvoiceDescription" runat="server"></asp:Label></td>
                                                                <td class="text-center"><asp:Label ID="lblInvoiceContainerRate" runat="server"></asp:Label></td>
                                                                <td class="text-center"><asp:Label ID="lblInvoicecontainerTotal" runat="server"></asp:Label></td>
                                                            </tr>--%>
                                                            <tr>
                                                                <td colspan="4">
                                                                    <table style="width: 100%;">
                                                                        <tbody id="tblContainerExpense" runat="server" >
                                                                            <tr>
                                                                                <td>adsfas</td>
                                                                            </tr>
                                                                        </tbody>                                                                        
                                                                    </table>
                                                                </td>
                                                                <%--<td></td>
                                                                <td class="">Empty Lift on Charges</td>
                                                                <td class="text-center">1850</td>
                                                                <td class="text-center">1850</td>--%>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4">
                                                                    <table style="width: 100%;">
                                                                        <tbody id="tblCotainerWeighment" runat="server">
                                                                            <tr>
                                                                                <td>asdfadfs</td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                                <%--<td></td>
                                                                <td class="">Weightment Charges</td>
                                                                <td class="text-center"><asp:Label ID="lblWeighmentCharges" runat="server" Text="190"></asp:Label></td>
                                                                <td class="text-center"><asp:Label ID="lblTotalWeighmentCharges" runat="server" Text="190"></asp:Label></td>--%>
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
                                                                                <table class="table table-hover invoice-table" id="tblContainersHTML">
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
                                                    <h4><asp:Label ID="lblAmountinWords" runat="server"></asp:Label></h4>
                                                </div>
                                            <div class="clearfix"></div><br>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <%--<div class="col-md-4">
                                                <asp:Label ID="lblReceive" runat="server" Text="Received By _________________________" ></asp:Label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:Label ID="lblCheck" runat="server" Text="Checked By _________________________" ></asp:Label>

                                            </div>
                                            <div class="col-md-4">
                                                <asp:Label ID="lblPrePaid" runat="server" Text="PrePaired By _________________________" ></asp:Label>

                                            </div>--%>

                                            <div class="col-md-12 text-center">
                                                <label style="font-size:8px;">Developed by: Vals Technologies | PABX: 0304 111 66 88 | www.valstechnologies.com</label>
                                            </div>
                                        </div>
                                        
                                        <div class="row" style="display:none" id="ShowHide">
                                                <div class="col-md-12" >
                                                        <asp:TextBox ID="txtSend" runat="server"></asp:TextBox>
                                                       <%-- <a href="#" target="_blank" onclick="EmailInvoice(); return false;"   class="btn btn-accent btn-md"><i class="fa fa-send"></i> &nbsp; Send </a>        --%>
                                                    <asp:Button runat="server" OnClientClick="EmailInvoice();return false" Text="Send" class="btn btn-accent btn-md" ></asp:Button>

                                                    </div>
                                            
                                        </div>
                                        <div class="row">
                                            <div class="col-xs-12 text-center">
                                                <%--<asp:LinkButton ID="lnkPrint" runat="server" CssClass="btn btn-purple btn-md" OnClick="lnkPrint_Click"><i class="fa fa-print"></i> &nbsp; Print </asp:LinkButton>--%>
                                                <a href="#" onclick="Print(); return false;" class="btn btn-purple btn-md"><i class="fa fa-print"></i> &nbsp; Print </a>
                                               <%-- <a href="#" target="_blank" id="btnSend" class="btn btn-accent btn-md"><i class="fa fa-send"></i> &nbsp; Send </a>        --%>
                                                <button type="button" class="btn btn-info" id="btnSend" onclick="show()"> Send</button>
                                            </div>
                                        </div>
                                        <!-- end -->
                                    </asp:Panel>
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
                                            <%--<asp:RadioButtonList ID="rbPaymentMode" runat="server" RepeatDirection="Horizontal">--%>
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
                                            <div class="col-md-6 form-group">
                                                <asp:Label id="Label3" runat="server" Font-Size="Smaller"><strong>Transaction Date</strong></asp:Label>
                                                <asp:TextBox ID="txtTransactionDate" TextMode="Date" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>                                            
                                        </div>
                                        <div class="col-md-12">
                                            <label>Description</label>
                                            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Columns="12" Rows="2" CssClass="form-control"></asp:TextBox>
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

                                  <ajaxToolkit:ModalPopupExtender ID="modalEditBill" runat="server" PopupControlID="pnlEditBill" DropShadow="True" TargetControlID="btnOpenEditBill" 
                                    CancelControlID="lnkCloseEditBill" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
                                 <asp:Panel ID="pnlEditBill" runat="server" CssClass="row" style="background-color: white; padding: 20px; border: 1px solid black;" Width="600px">
                                    <asp:Button ID="btnOpenEditBill" runat="server" style="display: none" />
                                    <asp:LinkButton ID="lnkCloseEditBill" runat="server" ForeColor="Maroon"  OnClick="lnkCloseEditBill_Click"><i class="fas fa-times-circle pull-right"></i></asp:LinkButton>
                                    <h4><asp:Label ID="Label2" runat="server" Text="Edit Bill"></asp:Label></h4>                       
                                    <div class="col-md-12">
                                        <div class="col-md-3 form-group">
                                             <asp:HiddenField ID="hfEditBill" runat="server" />
                                             <asp:HiddenField ID="hfCustomerCompanyName" runat="server" />
                                             <asp:HiddenField ID="hfOldAmount" runat="server" />
                                       <asp:LinkButton ID="lnkEditBill" runat="server" OnClick="lnkEditBill_Click" CssClass="btn btn-xs btn-info pull-right "><i class="fas fa-file-invoice"></i> | Edit Bill</asp:LinkButton>
                                        </div>
                                         <div class="col-md-12 col-sm-12 col-xs-12 pull-left table-responsive-lg pre-scrollable">
                                             <asp:HiddenField ID="hfBillNo" runat="server" />
                                        <asp:GridView ID="gvAllContainers" runat="server" CssClass="table table-hover " AutoGenerateColumns="false" Font-Size="10px"
                                             DataKeyNames="OrderConsignmentID, Rate, CustomerCompanyID">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="cbSelect" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ContainerNo" HeaderText="Container #" />
                                                <asp:BoundField DataField="RecordedDate" HeaderText="Order Date" />
                                                <asp:BoundField DataField="ShippingLine" HeaderText="Shipping Line" />
                                                <asp:BoundField DataField="AssignedVehicle" HeaderText="Vehicle" />
                                                <asp:BoundField DataField="EmptyContainerPickLocation" HeaderText="Pickup" />
                                                <asp:BoundField DataField="EmptyContainerDropLocation" HeaderText="Dropoff" />
                                            </Columns>
                                        </asp:GridView>

                                        
                                    </div>
                                    <%--<div class="col-md-6 col-sm-12 col-xs-12 pull-left">
                                        <asp:GridView ID="gvSelectedContainers" runat="server" CssClass="table table-hover" AutoGenerateColumns="false" Font-Size="10px">
                                            <Columns>
                                                <asp:BoundField DataField="ContainerNo" HeaderText="Container #" />
                                                <asp:BoundField DataField="RecordedDate" HeaderText="Order Date" />
                                                <asp:BoundField DataField="OrderNo" HeaderText="Order #" />
                                                <asp:BoundField DataField="AssignedVehicle" HeaderText="Vehicle" />
                                                <asp:BoundField DataField="Rate" HeaderText="Rate" />
                                                <asp:BoundField DataField="EmptyContainerPickLocation" HeaderText="Pickup" />
                                                <asp:BoundField DataField="EmptyContainerDropLocation" HeaderText="Dropoff" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>--%>
                            
                            
                                    </div>
                                </asp:Panel>
                                <%-- <ajaxToolkit:ModalPopupExtender ID="modalBill" runat="server" PopupControlID="pnlBill" DropShadow="True" TargetControlID="btnOpenBill" 
                                    CancelControlID="lnkCloseBill" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlBill" runat="server" CssClass="row" style="background-color: white; padding: 20px; border: 1px solid black; height: 600px; overflow-y: scroll" Width="1100px">
                            
                                    <asp:Button ID="btnOpenBill" runat="server" style="display: none" />
                                    <asp:LinkButton ID="lnkCloseBill" runat="server" ForeColor="Maroon" CssClass="pull-right" style="display: none;"><i class="fa fa-times-circle-o"></i></asp:LinkButton>
                                    <h4 class="pull-left"><asp:Label ID="Label2" runat="server"></asp:Label></h4> 
                                    <asp:LinkButton ID="lnkCloseBills" runat="server" ForeColor="Maroon" CssClass="pull-right"><i class="fas fa-times-circle"></i></asp:LinkButton>
                                    <div id="div2" runat="server"></div>
                                    <h2>Invoice</h2>
                                    
                                    <asp:Panel ID="pnlContainerSelections" runat="server" CssClass="col-xs-12" Visible="false">
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
                                                    <asp:LinkButton ID="LinkButton4" runat="server" CssClass="btn btn-info m-t-40"><i class="fas fa-plus-square"></i></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <asp:Panel ID="panelBill" runat="server" CssClass="col-xs-12">
                                        <!-- start -->
                                        <div class="row">
                                            <div class="col-xs-12">
                                                <div class="invoice-head row">
                                                    <div class="col-xs-12 col-md-2 invoice-title">
                                                        <img alt="" src="assets/images/MZBLogo.png" style="width: 100%" class="img-reponsive">
                                                    </div>
                                                    
                                                    <div class="col-xs-12 col-md-3 invoice-head-info">
                                                        <span class='text-muted'>
                                                            Customer Bill# <asp:TextBox ID="txtCusBill" runat="server"></asp:TextBox>
                                                            <br>
                                                            <asp:Label ID="lblBillDates" runat="server"></asp:Label>
                                                        </span>
                                                        
                                                    </div>
                                                    <div class="col-xs-12 col-md-3 invoice-logo col-md-offset-1">
                                                        <%--<img alt="" src="../data/invoice/invoice-logo.png" class="img-reponsive">--%>
                                                      <%--  <img alt="" src="assets/images/MZBLogo2.png" style="padding-top: 5%;" class="img-reponsive">
                                                    </div>
                                                </div>
                                                <div class="clearfix"></div><br>
                                            </div>
                                            <div class="col-xs-4 invoice-infoblock pull-left">
                                                <h4>Customer:</h4>
                                                <h3><asp:Label ID="lblPartyCustName" runat="server"></asp:Label></h3>
                                            </div>
                                            <div class="col-xs-4 invoice-infoblock pull-left">
                                                <h4>Bill No:</h4>
                                                <h3><asp:Label ID="lblBill" runat="server"></asp:Label></h3>
                                            </div>
                                            <div class="col-xs-4 invoice-infoblock pull-left">
                                                <h4>Customer Bill #</h4>
                                                <h3><asp:Label ID="lblCustomerBill" runat="server"></asp:Label></h3>
                                            </div>
                                            <div class="col-xs-4 invoice-infoblock pull-left">
                                                <h4>Clearing Agent</h4>
                                                <h3><asp:Label ID="lblClearing" runat="server"></asp:Label></h3>
                                            </div>
                                            <div class="col-xs-4 invoice-infoblock pull-left">
                                                <h4>Shipping Line</h4>
                                                <h3><asp:Label ID="lblShipping" runat="server"></asp:Label></h3>
                                            </div>
                                            <div class="col-xs-4 invoice-infoblock pull-left">
                                                <h4>Bill Date</h4>
                                                <h3><asp:Label ID="lblDate" runat="server"></asp:Label></h3>
                                            </div>
                                            <div class="col-xs-6 invoice-infoblock pull-left">
                                                <h4>Containers Qty</h4>
                                                <h3><asp:Label ID="lblContainer" runat="server"></asp:Label></h3>
                                            </div>
                                            <div class="col-xs-6 invoice-infoblock pull-left">
                                                <h4>Remarks</h4>
                                                <h3><asp:Label ID="lblRemark" runat="server"></asp:Label></h3>
                                            </div>
                                        </div>

                                        <div class="row" id="Div3" runat="server">
                                            <div class="col-xs-12">
                                                <%--<h3>Order summary</h3><br>--%>
                                               <%-- <div class="table-responsive">

                                                    <table class="table table-hover invoice-table">
                                                        <thead>
                                                            <tr>
                                                                <td class="text-center">Bilty #</td>
                                                                <td class="text-center">Date</td>
                                                                <td class="text-center">Container</td>
                                                                <td class="text-center">Truck</td>
                                                                <td class="text-center">From</td>
                                                                <td class="text-center">To</td>
                                                                <td class="text-center">Expenses</td>
                                                                <td class="text-center">LoLo</td>
                                                                <td class="text-center">Weighment</td>
                                                                <td class="text-center">Rate</td>
                                                                <td class="text-center">Total</td>
                                                            </tr>
                                                        </thead>
                                                        <tbody id="Tbody2" runat="server">
                                                            <!-- foreach ($order->lineItems as $line) or some such thing here -->
                                                            <%--<tr>
                                                                <td><asp:Label ID="lblTotalInvoiceContainers" runat="server"></asp:Label></td>
                                                                <td class=""><asp:Label ID="lblInvoiceDescription" runat="server"></asp:Label></td>
                                                                <td class="text-center"><asp:Label ID="lblInvoiceContainerRate" runat="server"></asp:Label></td>
                                                                <td class="text-center"><asp:Label ID="lblInvoicecontainerTotal" runat="server"></asp:Label></td>
                                                            </tr>--%>
                                                            <%--<tr>
                                                                <td colspan="4">
                                                                    <table style="width: 100%;">
                                                                        <tbody id="Tbody3" runat="server" >
                                                                            <tr>
                                                                                <td>adsfas</td>
                                                                            </tr>
                                                                        </tbody>                                                                        
                                                                    </table>
                                                                </td>
                                                                <%--<td></td>
                                                                <td class="">Empty Lift on Charges</td>
                                                                <td class="text-center">1850</td>
                                                                <td class="text-center">1850</td>--%>
                                                           <%-- </tr>
                                                            <tr>
                                                                <td colspan="4">
                                                                    <table style="width: 100%;">
                                                                        <tbody id="Tbody4" runat="server">
                                                                            <tr>
                                                                                <td>asdfadfs</td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                                <%--<td></td>
                                                                <td class="">Weightment Charges</td>
                                                                <td class="text-center"><asp:Label ID="lblWeighmentCharges" runat="server" Text="190"></asp:Label></td>
                                                                <td class="text-center"><asp:Label ID="lblTotalWeighmentCharges" runat="server" Text="190"></asp:Label></td>--%>
                                                           <%-- </tr>
                                                            <tr>
                                                                <td>MSC LINE</td>
                                                                <td class="text-center"></td>
                                                                <td class="text-center"></td>
                                                                <td class="text-right"></td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td></td>
                                                                <td class="">
                                                                    <div class="row" id="Div4" runat="server">
                                                                        <div class="col-xs-12">
                                                                            <h3>Container's summary</h3><br>
                                                                            <div class="table-responsive">
                                                                                <table class="table table-hover invoice-table" id="tblContainersHTML">
                                                                                    <thead>
                                                                                        <tr>
                                                                                            <td><h4>Date #</h4></td>
                                                                                            <td class="text-center"><h4>Vehicle #</h4></td>
                                                                                            <td class="text-center"><h4>Container #</h4></td>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tbody id="Tbody5" runat="server"></tbody>
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
                                                    </table>--%>
                                                  <%--  <table class="table table-hover invoice-table">
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
                                                       <%-- <tr>
                                                            <td class="no-line"></td>
                                                            <td class="no-line"></td>
                                                            <td class="no-line text-center"><h4>Total</h4></td>
                                                            <td class="no-line text-right"><h3 style='margin:0px;' class="text-primary"><asp:Label ID="Label13" runat="server"></asp:Label></h3></td>
                                                        </tr>
                                                    </table>
                                                    <h4><asp:Label ID="Label14" runat="server"></asp:Label></h4>
                                                </div>--%>
                                           <%-- <div class="clearfix"></div><br>
                                            </div>
                                        </div>--%>
                                       <%-- <div class="row">
                                            <%--<div class="col-md-4">
                                                <asp:Label ID="lblReceive" runat="server" Text="Received By _________________________" ></asp:Label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:Label ID="lblCheck" runat="server" Text="Checked By _________________________" ></asp:Label>

                                            </div>
                                            <div class="col-md-4">
                                                <asp:Label ID="lblPrePaid" runat="server" Text="PrePaired By _________________________" ></asp:Label>

                                            </div>--%>

                                           <%-- <div class="col-md-12 text-center">
                                                <label style="font-size:8px;">Developed by: Vals Technologies | PABX: 0304 111 66 88 | www.valstechnologies.com</label>
                                            </div>
                                        </div>--%>
                                        

                                       <%-- <div class="row">
                                            <div class="col-xs-12 text-center">
                                                <%--<asp:LinkButton ID="lnkPrint" runat="server" CssClass="btn btn-purple btn-md" OnClick="lnkPrint_Click"><i class="fa fa-print"></i> &nbsp; Print </asp:LinkButton>--%>
<%--                                                <a href="#" onclick="Print(); return false;" class="btn btn-purple btn-md"><i class="fa fa-print"></i> &nbsp; Print </a>
                                                <a href="#" target="_blank" class="btn btn-accent btn-md"><i class="fa fa-send"></i> &nbsp; Send </a>        
                                            </div>
                                        </div>
                                        <!-- end -->
                                    </asp:Panel>
                                </asp:Panel>--%>

                            </div>
                        </section>
                    </div>
                    <!-- MAIN CONTENT AREA ENDS -->
                 </ContentTemplate>
            </asp:UpdatePanel>
        </section>
    </section>
    <!-- END CONTENT -->
    <script src="assets/js/jquery-1.11.2.min.js"></script>
     <script>
        //$(document).ready(function () {
        //    $('#btnSend').click(function () {
        //        alert();
        //        //$('#ShowHide').show();
        //    });
        //});
        function show()
        {            
            document.getElementById("ShowHide").style.display = "block";
        }

         function DeleteBill() {
             let isConfirm = confirm('Are you sure want to delete?');
             if (isConfirm) {
                 return true;
             }
             else {
                 return false;
             }
         }
    </script>
</asp:Content>
