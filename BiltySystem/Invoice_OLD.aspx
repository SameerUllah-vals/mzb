<%@ Page Title="" Language="C#" MasterPageFile="~/BiltySystem.Master" AutoEventWireup="true" CodeBehind="Invoice.aspx.cs" Inherits="BiltySystem.Invoice_OLD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .modalBackground {
            position: fixed;
            left: 0;
            width: 100%;
            height: 100%;
            z-index: 11111;
            TOP: 0;
        }
    </style>
    <script src="assets/js/jquery-1.11.2.min.js"></script>
    <script type="text/javascript">


        function GetContent() {
            debugger;
            var content = $("#tblInvoice *").html();
            $('#<%=hfInvoiceContent.ClientID%>').val(content);

        //PageMethods.GenerateInvoicePDF(content, onSuccess, onError);

        //function onSuccess() { alert('result'); }

        //function onError() { alert('Error'); }
        //console.log("hf" + $('#<%=hfInvoiceContent.ClientID%>').val());
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>--%>
    <asp:ScriptManager ID="ScriptManager2" runat="server" EnablePageMethods="true"></asp:ScriptManager>

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



                    <asp:Panel runat="server" CssClass="col-xs-12" ID="pnlInput">
                        <section class="box ">
                            <asp:LinkButton ID="lnkCloseInput" runat="server" CssClass=" fa fa-times-circle pull-right" Style="margin-top: 10px; margin-right: 10px;"></asp:LinkButton>
                            <div class="content-body">
                                <div class="row">
                                    <div class="col-md-12 col-sm-12 col-xs-12">

                                        <%--<div role="form">--%>
                                        <div class="row">
                                            <div class="form-group col-md-6 pull-left">
                                                <label>Sender</label>
                                                <asp:DropDownList runat="server" ID="ddlSender" CssClass="form-control"></asp:DropDownList><asp:HiddenField ID="hfEditID" runat="server" />

                                            </div>
                                            <div class="form-group col-md-6 pull-left">
                                                <label>Receiver</label>
                                                <asp:DropDownList runat="server" ID="ddlReciver" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                            <div class="form-group col-md-3 pull-left">
                                                <label>Date From</label>
                                                <asp:TextBox TextMode="Date" ID="txtDateFrom" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-md-3 pull-left">
                                                <label>Date From</label>
                                                <asp:TextBox TextMode="Date" ID="txtDateTo" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-3 pull-left">
                                                <label>Invoice Format</label>
                                                <asp:DropDownList runat="server" ID="ddlInvoice" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                            <div class="form-group col-md-3 pull-left">
                                                <label>Bilty No</label>
                                                <asp:TextBox ID="txtBiltyNo" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>







                                            <div class="form-group col-lg-12 pull-left">



                                                <asp:LinkButton ID="lnkSearch" runat="server" OnClick="lnkSearch_Click" CssClass="btn btn-success pull-right m-l-10"><i class="fa fa-search"></i> | Search</asp:LinkButton>



                                            </div>

                                            <%--<div class="form-group">
                                                <button type="button" class="btn btn-primary ">Sign in</button>
                                                <button type="button" class="btn btn-purple  pull-right">Register now</button>
                                            </div>--%>

                                            <%--</div>--%>
                                        </div>
                                    </div>
                                </div>
                        </section>
                    </asp:Panel>


                    <div class="col-xs-12">
                        <section class="box ">
                            <header class="panel_header">
                                <h2 class="title pull-left">Invoice</h2>
                                <%-- <div class="col-md-6 m-t-25 pull-right">
                                    <asp:LinkButton ID="lnkCancelSearch" runat="server" ><i class="fa fa-times-circle m-t-10 m-r-5 pull-left" style="color: maroon;"></i></asp:LinkButton>
                            
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="pull-left m-r-10 pull-left"></asp:TextBox>
                                    <asp:LinkButton ID="lnkSearch" runat="server" CssClass="pull-left"><i class="fa fa-search"></i></asp:LinkButton>
                                </div>--%>
                                <div class="actions panel_actions pull-right">

                                    <asp:LinkButton ID="lnkAddNew" runat="server" CssClass="box_setting fas fa-plus" ToolTip="Click to Add New"></asp:LinkButton>
                                    <%--<a class="box_setting fa fa-cog" data-toggle="modal" href="#section-settings"></a>
                                    <a class="box_close fa fa-times"></a>--%>
                                </div>
                            </header>
                            <div class="content-body">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <asp:GridView ID="gvResult" runat="server" Width="100%" EmptyDataText="No Record found" AutoGenerateColumns="false"
                                            CssClass="table table-hover" Font-Size="12px" Font-Names="Open Sans" DataKeyNames="OrderID" BackColor="White" OnRowCommand="gvResult_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.no">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="OrderNo" HeaderText="Order #"></asp:BoundField>
                                                <asp:BoundField DataField="RecordedDate" HeaderText="Date"></asp:BoundField>
                                                <asp:BoundField DataField="Sender" HeaderText="Sender"></asp:BoundField>
                                                <%--<asp:BoundField DataField="Status" HeaderText="Active"></asp:BoundField>--%>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkActive" runat="server" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="Active"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkView" runat="server" CssClass="fa fa-eye" ForeColor="DodgerBlue" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="View"></asp:LinkButton>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" CssClass="fa fa-edit m-l-15" ForeColor="LightSeaGreen" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="Change"></asp:LinkButton>
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
                    </ContentTemplate>
            </asp:UpdatePanel>

                    <ajaxToolkit:ModalPopupExtender ID="modalInvoice" runat="server" PopupControlID="pnlInvoice" DropShadow="True" TargetControlID="btnOpenInvoice"
                        CancelControlID="lnkCloseInvoice" BackgroundCssClass="modalBackground">
                    </ajaxToolkit:ModalPopupExtender>
                    <asp:Panel ID="pnlInvoice" runat="server" CssClass="row" Style="background-color: white; padding: 20px; border: 1px solid black;" Width="1200px" Height="500px">
                        <asp:Button ID="btnOpenInvoice" runat="server" Style="display: none" />
                        <asp:LinkButton ID="lnkCloseInvoice" runat="server" ForeColor="Maroon" CssClass="pull-right" Style="display: none;"><i class="fa fa-times-circle-o"></i></asp:LinkButton>
                        <h4>
                            <asp:Label ID="lblModalTitle" runat="server"></asp:Label></h4>
                        <div class="col-md-12">
                            <div style="height: 400px; overflow-y: scroll">
                                <div class="content-body">
                                    <asp:HiddenField ID="hfInvoiceContent" runat="server" />
                                    <asp:Label ID="lblInvoiceContent" runat="server" Style="display: none;"></asp:Label>
                                    <div class="close pull-right">
                                        <asp:Button runat="server" OnClick="btnClose_Click" ID="btnClose" CssClass="close" Text="X" /></div>
                                    <div class="row">
                                        <div class="col-xs-12" id="tblInvoice">


                                            <!-- start -->

                                            <div class="row">
                                                <div class="col-xs-12">
                                                    <div class="invoice-head">
                                                        <div class="col-xs-12 col-md-2 invoice-title">
                                                            <h2 class="text-center bg-primary ">Invoice</h2>
                                                        </div>
                                                        <div class="col-xs-12 col-md-3 invoice-head-info">
                                                            <span class='text-muted'>
                                                                <asp:Label runat="server" ID="lblSenderCompany"></asp:Label>
                                                                <br />
                                                                <asp:Label runat="server" ID="lblSenderGroup"></asp:Label>
                                                                <br />
                                                                <asp:Label runat="server" ID="lblSenderDepartment"></asp:Label>
                                                            </span>

                                                        </div>
                                                        <div class="col-xs-12 col-md-3 invoice-head-info">
                                                            <span class='text-muted pull-right'>
                                                                <asp:Label runat="server" ID="lblReciverCompany"></asp:Label><br />
                                                                <asp:Label runat="server" ID="lblReciverGroup"></asp:Label><br />
                                                                <asp:Label runat="server" ID="lblReciverDepartment"></asp:Label>
                                                            </span>
                                                        </div>
                                                        <div class="col-xs-12 col-md-3 invoice-logo col-md-offset-1">
                                                            <%--  <img src="../data/invoice/invoice-logo.png" class="img-reponsive">--%>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="clearfix"></div>
                                                <br>

                                                <div class="col-xs-4 invoice-infoblock pull-left">
                                                    <h4>Billed To:</h4>
                                                    <address>
                                                        <h3>
                                                            <asp:Label runat="server" ID="lblCustomerCompany"></asp:Label></h3>
                                                        <span class='text-muted'>
                                                            <asp:Label runat="server" ID="lblCustomerGroup"></asp:Label></span>


                                                        <address>
                                                            <span class='text-muted'>
                                                                <asp:Label runat="server" ID="lblCustomerDepartment"></asp:Label></span>
                                                        </address>

                                                        <address>
                                                        </address>

                                                        <address>
                                                        </address>

                                                        <address>
                                                        </address>

                                                        <address>
                                                        </address>

                                                        <address>
                                                        </address>

                                                        <address>
                                                        </address>

                                                    </address>
                                                </div>
                                                <div class="col-xs-2">&nbsp;</div>
                                                <div class="col-xs-6 invoice-infoblock text-right">
                                                    <address>
                                                        <span><strong>Bilty No:</strong></span> <span class='text-muted'>
                                                            <asp:Label runat="server" ID="lblOrderNo"></asp:Label></span>

                                                    </address>
                                                    <address>

                                                        <span><strong>Date:</strong></span> <span class='text-muted'>
                                                            <asp:Label ID="lblRecordedDate" runat="server"></asp:Label></span>

                                                    </address>
                                                    <div class="invoice-due">
                                                        <h3 class="text-muted">Total Due:</h3>
                                                        &nbsp;
                                <h2 class="text-primary">$ 2140.00</h2>
                                                    </div>
                                                    <address>
                                                    </address>
                                                    <address>
                                                    </address>
                                                    <address>
                                                    </address>
                                                    <address>
                                                    </address>
                                                    <address>
                                                    </address>
                                                    </address>
                       
              

                                                </div>


                                                <div class="clearfix"></div>
                                                <br>
                                            </div>

                                            <div class="row">
                                                <div class="col-xs-12">
                                                    <h3>Order summary</h3>
                                                    <br>
                                                    <div class="table-responsive">
                                                        <table class="table table-hover invoice-table">
                                                            <thead>
                                                                <tr>
                                                                    <td>
                                                                        <h4>No Of Package</h4>
                                                                    </td>
                                                                    <td class="text-center">
                                                                        <h4>Detail</h4>
                                                                    </td>
                                                                    <td class="text-center">
                                                                        <h4>Quantity</h4>
                                                                    </td>
                                                                    <td class="text-center">
                                                                        <h4>Rate</h4>
                                                                    </td>
                                                                    <td class="text-right">
                                                                        <h4>Totals</h4>
                                                                    </td>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <!-- foreach ($order->lineItems as $line) or some such thing here -->
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label runat="server" ID="lblNoOfPackage"></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label runat="server" ID="lblDetail"></asp:Label></td>
                                                                    <td class="text-center">
                                                                        <asp:Label runat="server" ID="lblQuantity"></asp:Label></td>
                                                                    <td class="text-right">
                                                                        <asp:Label runat="server" ID="lblRate">400</asp:Label></td>
                                                                    <td class="text-right">
                                                                        <asp:Label runat="server" ID="Total">400</asp:Label></td>
                                                                </tr>

                                                                <tr>
                                                                    <td class="thick-line"></td>
                                                                    <td class="thick-line"></td>
                                                                    <td class="thick-line text-center">
                                                                        <h4>Subtotal</h4>
                                                                    </td>
                                                                    <td class="thick-line text-right">
                                                                        <h4>$1670.99</h4>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="no-line"></td>
                                                                    <td class="no-line"></td>
                                                                    <td class="no-line text-center">
                                                                        <h4>Shipping</h4>
                                                                    </td>
                                                                    <td class="no-line text-right">
                                                                        <h4>$15</h4>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="no-line"></td>
                                                                    <td class="no-line"></td>
                                                                    <td class="no-line text-center">
                                                                        <h4>VAT</h4>
                                                                    </td>
                                                                    <td class="no-line text-right">
                                                                        <h4>$150.23</h4>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="no-line"></td>
                                                                    <td class="no-line"></td>
                                                                    <td class="no-line text-center">
                                                                        <h4>Total</h4>
                                                                    </td>
                                                                    <td class="no-line text-right">
                                                                        <h3 style="margin: 0px;" class="text-primary">$1985.99</h3>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>



                                            <div class="clearfix"></div>
                                            <br>
                                            
                                            <div class="row">
                                                <div class="col-xs-12 text-right">
                                                    <%--<a href="#" target="_blank" class="btn btn-purple btn-md"><i class="fa fa-print"></i> &nbsp; Print </a> --%>
                                                  <%--  <asp:Button runat="server" ID="btnPrint" CssClass="btn btn-purple btn-md" OnClick="btnPrint_Click" />--%>
                                                    <asp:Button ID="btnPrints" runat="server" Text="Print" OnClick="btnPrints_Click" />

                                                    <%--<asp:Button runat="server" Text="Button" OnClick="GenerateInvoicePDFss" />--%>
                                                    <a href="#" target="_blank" class="btn btn-accent btn-md"><i class="fa fa-send"></i>&nbsp; Proceed to payment </a>
                                                </div>
                                            </div>


                                            <!-- end -->


                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                
            <!-- MAIN CONTENT AREA ENDS -->
        </section>
    </section>
    <!-- END CONTENT -->
</asp:Content>
