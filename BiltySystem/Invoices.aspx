<%@ Page Title="" Language="C#" MasterPageFile="~/BiltySystem.Master" AutoEventWireup="true" CodeBehind="Invoices.aspx.cs" Inherits="BiltySystem.Invoices" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>               
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
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

        function Print() {
            debugger;
            //readTextFile("assets/css/style.css");
            
            var InvoiceHTML = "";
            InvoiceHTML += "<div style=\"width: 100%; \">";
                InvoiceHTML += "<div id = \"InvoiceHeader\" style = \"width: 100%; background-color: #f5f5f5; padding: 0px 0px;\">";
                    InvoiceHTML += "<div style=\"width: 18%; background: #3F51B5; padding: 3px 8px; color: #ffffff; float: left;\">";
                        InvoiceHTML += "<h2 style=\"text-align: center;\">Invoice</h2>";
                    InvoiceHTML += "</div>";
                    InvoiceHTML += "<div style=\"width: 25%; padding-top: 10px; padding-bottom: 10px; margin-left: 5px; white-space: nowrap; font-size: 14px; float: left;\">" + document.getElementById('ContentPlaceHolder1_lblBilltoCustomer').innerText + "</div>";
                    InvoiceHTML += "<div style=\"width: 25%; padding-top: 10px; padding-bottom: 10px; white-space: nowrap; font-size: 14px; float: left;\">";
                        InvoiceHTML += "<span style='color: #999999;'>";
                            InvoiceHTML += "Invoice # " + document.getElementById('ContentPlaceHolder1_lblOrderNo').innerText;
                            InvoiceHTML += "<br />";
                            InvoiceHTML += document.getElementById('ContentPlaceHolder1_lblOrderDate').innerText;
                        InvoiceHTML += "</span>";
                    InvoiceHTML += "</div>";
                    InvoiceHTML += "<div style=\"width: 25%; padding-right: 0px; float: left;\">";
                        InvoiceHTML += "<img alt=\"\" src=\"../data/invoice/invoice-logo.png\" style=\"text-align: center\" />";
                    InvoiceHTML += "</div>";
                InvoiceHTML += "</div>";
                InvoiceHTML += "<div style=\"width: 100%\">";
                    InvoiceHTML += "<h6 style=\"text-align: center; font-size: 12; margin: 0;\">Karachi Office: Plot H/2, between Gate # 2 & 3, Quaid-e-Azam Truck Stand, Karachi. 0300 823 29 94</h6>";
                    InvoiceHTML += "<h6 style=\"text-align: center; margin: 0;\">Hyderabad Office: Plot # 54, Halanaka Road, Hyderabad. 022 203 26 86</h6>";
                InvoiceHTML += "</div>";
                    
                InvoiceHTML += "<div id=\"InvoiceBody\" style=\"width: 100%;\">";
                    InvoiceHTML += "<div style=\"width: 50%; float: left;\">";
            InvoiceHTML += "<h4>Sender</h4>";
            InvoiceHTML += "<h3><span>" + document.getElementById('ContentPlaceHolder1_lblSenderCompanyName').innerText + "</span></h3>";
                        InvoiceHTML += "<address>";
                        InvoiceHTML += "<span>" + document.getElementById('ContentPlaceHolder1_lblSenderAddress').innerText + "</span>";
                        InvoiceHTML += "</address>";
                    InvoiceHTML += "</div>";

                    InvoiceHTML += "<div style=\"width: 50%; float: left; text-align: right;\">";
                        InvoiceHTML += "<h4>Receiver</h4>";
                        InvoiceHTML += "<h3><span>" + document.getElementById('ContentPlaceHolder1_lblReceiverCompanyName').innerText + "</span></h3>";
                        InvoiceHTML += "<address>";
                        InvoiceHTML += "<span>" + document.getElementById('ContentPlaceHolder1_lblSenderAddress').innerText + "-</span>";
                        InvoiceHTML += "</address>";
                    InvoiceHTML += "</div>";
                InvoiceHTML += "</div>";
                InvoiceHTML += "<div style=\"width: 100%;\">";
                    InvoiceHTML += "<table style=\"width: 100%;\">";
                        InvoiceHTML += "<thead>";
                            InvoiceHTML += "<tr style=\"background-color: #3F51B5; color: white;\">";
                                InvoiceHTML += "<th style=\"padding: 10px;\">No. Of Packages</th>";
                                InvoiceHTML += "<th style=\"padding: 10px;\">Description</th>";
                                InvoiceHTML += "<th style=\"padding: 10px;\">Rate</th>";
                                InvoiceHTML += "<th style=\"padding: 10px;\">Amount</th>";
                            InvoiceHTML += "</tr>";
                        InvoiceHTML += "</thead>";
                        InvoiceHTML += "<tbody>";
                            InvoiceHTML += "<tr>";
                                InvoiceHTML += "<td>" + document.getElementById('ContentPlaceHolder1_lblTotalInvoiceContainers').innerText + "</td>";
                                InvoiceHTML += "<td>" + document.getElementById('ContentPlaceHolder1_lblInvoiceDescription').innerText + "</td>";
                                InvoiceHTML += "<td>" + document.getElementById('ContentPlaceHolder1_lblInvoiceContainerRate').innerText + "/-</td>";
                                InvoiceHTML += "<td>" + document.getElementById('ContentPlaceHolder1_lblInvoicecontainerTotal').innerText + "/-</td>";
                            InvoiceHTML += "</tr>";
                            var tableExpenses = document.getElementById("ContentPlaceHolder1_tblContainerExpense");
                            var rowLength = tableExpenses.rows.length;
                            for (i = 0; i < rowLength; i++) {
                                
                                var expenseCells = tableExpenses.rows.item(i).cells;
                                var cellLength = expenseCells.length;
                                var ExpenseAmount = expenseCells.item(3).innerHTML;
                                InvoiceHTML += "<tr>";
                                    InvoiceHTML += "<td style=\"text-align: center;\">" + expenseCells.item(0).innerHTML + "</td>";
                                    InvoiceHTML += "<td style=\"text-align: center;\">" + expenseCells.item(1).innerHTML + "</td>";
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
                                    InvoiceHTML += "<td style=\"text-align: center;\">" + weighmentCells.item(1).innerHTML + "</td>";
                                    InvoiceHTML += "<td style=\"text-align: center;\">" + weighmentCells.item(2).innerHTML + "</td>";
                                    InvoiceHTML += "<td style=\"text-align: center;\">" + weighmentCells.item(3).innerHTML + "</td>";
                                InvoiceHTML += "</tr>";
                                Total = (+Total + +WeighmentAmount);                                
                            }
                            InvoiceHTML += "<tr>";
                                InvoiceHTML += "<td></td>";
                                InvoiceHTML += "<td colspan=\"3\">";
                                    InvoiceHTML += "<div style=\"width: 100%;\">";
                                        InvoiceHTML += "<h3>Containers Summary</h3>";
                                        InvoiceHTML += "<table style=\"width: 100%\">";
                                            InvoiceHTML += "<thead>";
                                                InvoiceHTML += "<tr style=\"background-color: #3F51B5; color: white; padding: 10px;\">";
                                                    InvoiceHTML += "<th style=\"padding: 10px;\">Date</th>";
                                                    InvoiceHTML += "<th style=\"padding: 10px;\">Vehicle #</th>";
                                                    InvoiceHTML += "<th style=\"padding: 10px;\">Container #</th>";
                                                    InvoiceHTML += "<th style=\"padding: 10px;\">Rate</th>";
                                                InvoiceHTML += "</tr>";
                                            InvoiceHTML += "</thead>";
                                            InvoiceHTML += "<tbody>";
                                            var tableContainers = document.getElementById("ContentPlaceHolder1_gvSelectedContainers");
                                            var rowLength = tableContainers.rows.length;
                                            var Total = 0;

                                            //loops through rows    
                                            for (i = 0; i < rowLength; i++) {
                                                if (i > 0) {
                                                    var oCells = tableContainers.rows.item(i).cells;
                                                    var cellLength = oCells.length;
                                                    var rate = oCells.item(4).innerHTML;
                                                    InvoiceHTML += "<tr>";
                                                        InvoiceHTML += "<td style=\"text-align: center;\">" + oCells.item(1).innerHTML + "</td>";
                                                        InvoiceHTML += "<td style=\"text-align: center;\">" + oCells.item(3).innerHTML + "</td>";
                                                        InvoiceHTML += "<td style=\"text-align: center;\">" + oCells.item(0).innerHTML + "</td>";
                                                        InvoiceHTML += "<td style=\"text-align: center;\">" + oCells.item(4).innerHTML + "</td>";
                                                    InvoiceHTML += "</tr>";
                                                    Total = (+Total + +rate);
                                                }
                                            }
                                            InvoiceHTML += "</tbody>";
                                        InvoiceHTML += "</table>";
                                    InvoiceHTML += "</div>";
                                InvoiceHTML += "</td>";
                            InvoiceHTML += "</tr>";
                            InvoiceHTML += "<tr>";
                                InvoiceHTML += "<td colspan=\"3\"><h4>Total</h4></td>";
                                //InvoiceHTML += "<td>" + Total + "</td>";
                                InvoiceHTML += "<td>" + document.getElementById('ContentPlaceHolder1_lblInvoiceGrandTotal').innerText + "</td>";
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <!-- START CONTENT -->
    <section id="main-content" class=" ">
        <section class="wrapper main-wrapper row" style=''>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <!-- MAIN CONTENT AREA STARTS -->
                    <div class="col-xs-12" id="SearchInvoices" runat="server">
                        <section class="box ">
                            <header class="panel_header">
                                <h2 class="title pull-left">Search Invoices</h2>
                                <asp:LinkButton ID="lnkCreateInvoice" runat="server" OnClick="lnkCreateInvoice_Click" CssClass="pull-right m-t-30 m-r-20" ToolTip="Click to add new invoice"><i class="fas fa-plus"></i> | Create Invoice</asp:LinkButton>
                            </header>
                            <div class="content-body">
                                <div class="row">
                                    <div class="col-md-8 col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="form-label" for="email-1">Customer</label>
                                            <asp:DropDownList ID="ddlInvoiceCustomers" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="form-label" for="email-1">Container #</label>
                                            <asp:TextBox ID="txtInvoiceContainers" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="form-label" for="email-1">Date</label>
                                            <asp:TextBox ID="txtDateContainers" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                        </div>
                                    </div>
                                    <%--<div class="col-md-4 col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="form-label" for="email-1">Order #</label>
                                            <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>--%>
                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <asp:LinkButton ID="lnkSearchContainers" runat="server" CssClass="btn btn-primary" OnClick="lnkSearch_Click"><i class="fas fa-search"></i> | Search</asp:LinkButton>
                                            <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btn-primary" OnClick="LinkButton2_Click"><i class="fas fa-search"></i> | Search</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div id="div3" style="margin-top: 10px;" runat="server"></div>
                            </div>
                        </section>
                    </div>
                    <div class="col-xs-12" id="ResultInvoices" runat="server">
                        <section class="box ">
                            <header class="panel_header">
                                <h2 class="title pull-left">Invoices</h2>
                                <%--<asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btn-xs btn-primary pull-right m-t-30 m-r-10" OnClick="lnkAddContainers_Click"><i class="fas fa-plus-square"></i> | Add Containers</asp:LinkButton>
                                <asp:LinkButton ID="LinkButton3" runat="server" CssClass="btn btn-xs btn-info pull-right m-r-10 m-t-30" OnClick="lnkGenerateInvoice_Click"><i class="fas fa-file-invoice"></i> | Generate Invoice</asp:LinkButton>--%>
                            </header>
                            <div class="content-body">
                                <div class="row">                                    
                                    <asp:GridView ID="gvInvoices" runat="server" CssClass="table table-hover" AutoGenerateColumns="false" Font-Size="10px" OnRowCommand="gvInvoices_RowCommand">
                                        <Columns>
                                            <asp:BoundField DataField="InvoiceNo" HeaderText="Container #" />
                                            <asp:BoundField DataField="CustomerCompany" HeaderText="Order Date" />
                                            <asp:BoundField DataField="Total" HeaderText="Order #" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkInvoice" runat="server" CommandName="PrintInvoice" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'><i class="fas fa-print"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkCheck" runat="server" CommandName="open" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'><i class="fas fa-print"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </section>
                    </div>
                    <div class="col-xs-12" id="SearchOrders" runat="server" visible="false">
                        <section class="box ">
                            <header class="panel_header">
                                <h2 class="title pull-left">Search Containers</h2>
                                <asp:LinkButton ID="lnkCloseInvoiceSearch" runat="server" CssClass="fas fa-times-circle pull-right m-t-30 m-r-20" ToolTip="Clock to close New Invoice panel" ForeColor="Maroon" OnClick="lnkCloseInvoiceSearch_Click"></asp:LinkButton>
                            </header>
                            <div class="content-body">
                                <div class="row">
                                    <div class="col-md-8 col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="form-label" for="email-1">Customer</label>
                                            <asp:DropDownList ID="ddlBilToCustomer" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="form-label" for="email-1">Container #</label>
                                            <asp:TextBox ID="txtContainerNo" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="form-label" for="email-1">Date</label>
                                            <asp:TextBox ID="txtOrderDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="form-label" for="email-1">Order #</label>
                                            <asp:TextBox ID="txtOrderNo" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <asp:LinkButton ID="lnkSearch" runat="server" CssClass="btn btn-primary" OnClick="lnkSearch_Click"><i class="fas fa-search"></i> | Search</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div id="divNotification" style="margin-top: 10px;" runat="server"></div>
                            </div>
                        </section>
                    </div>
                    <div class="col-xs-12" id="ResultOrders" runat="server" visible="false">
                        <section class="box ">
                            <header class="panel_header">
                                <h2 class="title pull-left">Containers</h2>
                                <asp:LinkButton ID="lnkAddContainers" runat="server" CssClass="btn btn-xs btn-primary pull-right m-t-30 m-r-10" OnClick="lnkAddContainers_Click"><i class="fas fa-plus-square"></i> | Add Containers</asp:LinkButton>
                                <asp:LinkButton ID="lnkGenerateInvoice" runat="server" CssClass="btn btn-xs btn-info pull-right m-r-10 m-t-30" OnClick="lnkGenerateInvoice_Click"><i class="fas fa-file-invoice"></i> | Generate Invoice</asp:LinkButton>
                            </header>
                            <div class="content-body">
                                <div class="row">
                                    <div class="col-md-6 col-sm-12 col-xs-12 pull-left">
                                        <asp:GridView ID="gvAllContainers" runat="server" CssClass="table table-hover" AutoGenerateColumns="false" Font-Size="10px" 
                                            DataKeyNames="OrderConsignmentID, Rate, CustomerCompanyID, WeighmentAmount" OnRowDataBound="gvAllContainers_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="cbContainer" runat="server" AutoPostBack="true" OnCheckedChanged="cbContainer_CheckedChanged" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ContainerNo" HeaderText="Container #" />
                                                <asp:BoundField DataField="RecordedDate" HeaderText="Order Date" />
                                                <asp:BoundField DataField="OrderNo" HeaderText="Order #" />
                                                <asp:BoundField DataField="AssignedVehicle" HeaderText="Vehicle" />
                                                <asp:BoundField DataField="EmptyContainerPickLocation" HeaderText="Pickup" />
                                                <asp:BoundField DataField="EmptyContainerDropLocation" HeaderText="Dropoff" />
                                            </Columns>
                                        </asp:GridView>

                                        
                                    </div>
                                    <div class="col-md-6 col-sm-12 col-xs-12 pull-left">
                                        <asp:GridView ID="gvSelectedContainers" runat="server" CssClass="table table-hover" AutoGenerateColumns="false" Font-Size="10px" 
                                            DataKeyNames="OrderConsignmentID, CustomerCompanyID, WeighmentCharges">
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
                                    </div>


                                </div>
                                <ajaxToolkit:ModalPopupExtender ID="modalInvoice" runat="server" PopupControlID="pnlInvoice" DropShadow="True" TargetControlID="btnOpenInvoice" 
                                    CancelControlID="lnkCloseInvoice" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlInvoice" runat="server" CssClass="row" style="background-color: white; padding: 20px; border: 1px solid black; height: 600px; overflow-y: scroll" Width="1100px">
                            
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
                                                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-info m-t-40" OnClick="lnkAddContainers_Click"><i class="fas fa-plus-square"></i></asp:LinkButton>
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
                                                    <div class="col-xs-12 col-md-3 invoice-head-info">
                                                        <span class='text-muted'>
                                                            Invoice # <asp:Label ID="lblOrderNo" runat="server"></asp:Label>
                                                            <br>
                                                            <asp:Label ID="lblOrderDate" runat="server"></asp:Label>
                                                        </span>
                                                        
                                                    </div>
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
                                                <%--<asp:LinkButton ID="lnkPrint" runat="server" CssClass="btn btn-purple btn-md" OnClick="lnkPrint_Click"><i class="fa fa-print"></i> &nbsp; Print </asp:LinkButton>--%>
                                                <a href="#" onclick="Print(); return false;" class="btn btn-purple btn-md"><i class="fa fa-print"></i> &nbsp; Print </a>
                                                <a href="#" target="_blank" class="btn btn-accent btn-md"><i class="fa fa-send"></i> &nbsp; Send </a>        
                                            </div>
                                        </div>
                                        <!-- end -->
                                    </asp:Panel>
                                </asp:Panel>

                            </div>
                        </section>
                    </div>
                    <!-- MAIN CONTENT AREA ENDS -->

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
        </section>
    </section>
    <!-- END CONTENT -->
</asp:Content>
