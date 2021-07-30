<%@ Page Title="" Language="C#" MasterPageFile="~/BiltySystem.Master" AutoEventWireup="true" CodeBehind="CustomerLedger.aspx.cs" Inherits="BiltySystem.CustomerLedger" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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


                   <%--  <asp:Panel ID="pnlView" runat="server" CssClass="row" Visible="false">
                    <div class="col-sm-12 col-md-12">
                        <div class="block-flat">
                            <asp:LinkButton ID="lnkCloseView" runat="server" OnClick="lnkCloseView_Click" ForeColor="Maroon" Font-Size="35px" ToolTip="Click to close view panel" CssClass="pull-right"><i class="fa fa-times-circle"></i></asp:LinkButton>
                            <div class="content">                                
                                <div role="form">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group row">
                                                <label class="control-label text-right col-md-3">Code:</label>
                                                <div class="col-md-9">
                                                    <asp:Label ID="txtCodeModal" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <!--/span-->
                                        <div class="col-md-6">
                                            <div class="form-group row">
                                                <label class="control-label text-right col-md-3">Name:</label>
                                                <div class="col-md-9">
                                                    <asp:Label ID="txtNameModal" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <!--/span-->
                                    </div>
                                    <!--/row-->
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group row">
                                                <label class="control-label text-right col-md-3">Description:</label>
                                                <div class="col-md-9">
                                                    <asp:Label ID="txtDescriptionModal" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <!--/span-->

                                        <!--/span-->
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>--%>

                    <%--<asp:Panel runat="server" CssClass="col-xs-12" ID="pnlInput" Visible="false">
                        <section class="box ">
                            <asp:LinkButton ID="lnkCloseInput" runat="server" OnClick="lnkCloseInput_Click" CssClass=" fa fa-times-circle pull-right" style="margin-top: 10px; margin-right: 10px;"></asp:LinkButton>
                            <div class="content-body">
                                <div class="row">
                                    <div class="col-md-12 col-sm-12 col-xs-12">

                                        <div role="form">
                                          <div class="row">
                                        <div class="form-group col-md-3 pull-left">
                                            <label>Code</label>
                                            <asp:TextBox ID="txtCode" runat="server" CssClass="form-control"></asp:TextBox><asp:HiddenField ID="hfEditID" runat="server" />

                                        </div>
                                        <div class="form-group col-md-3 pull-left">
                                            <label>Name</label>
                                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-md-6 pull-left">
                                            <label>Description</label>

                                            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Width="100%" Rows="3" CssClass="form-control"></asp:TextBox>
                                        </div>





                                            <div class="form-group col-lg-12 pull-left">
                                                <div class="col-md- pull-left demo-checkbox">
                                                    <asp:CheckBox ID="cbActive" runat="server" />
                                                    <label for="cbActive">Active</label>
                                                </div>
                                        
                                                <asp:LinkButton ID="lnkSubmit" runat="server" CssClass="btn btn-success pull-right m-l-10" OnClick="lnkSubmit_Click"><i class="fa fa-save"></i> | Save</asp:LinkButton>
                                        
                                                <asp:LinkButton ID="lnkDelete" runat="server" CssClass="btn btn-danger pull-right" OnClick="lnkDelete_Click"><i class="fa fa-trash"></i> | Delete</asp:LinkButton>
                                        
                                            </div>

                                            <%--<div class="form-group">
                                                <button type="button" class="btn btn-primary ">Sign in</button>
                                                <button type="button" class="btn btn-purple  pull-right">Register now</button>
                                            </div>

                                        <%--</div>

                                    </div>
                                </div>
                            </div>
                        </section>
                    </asp:Panel>--%>


                    <div class="col-xs-12" id="MainLedger" runat="server" visible="false">
                        <section class="box ">
                            <header class="panel_header">
                                <h2 class="title pull-left"><asp:LinkButton ID="lnkAllLedgers" runat="server" OnClick="lnkAllLedgers_Click" style="text-decoration: none;"><i class="fas fa-arrow-circle-left"></i> All Ledgers</asp:LinkButton></h2> 
                                <asp:Panel ID="pnlSearch" runat="server" DefaultButton="lnkSearch" CssClass="col-md-12 m-t-25 pull-right">
                                    <%--<asp:LinkButton ID="LinkButton1" runat="server"><i class="fa fa-times-circle m-t-10 m-r-5 pull-left" style="color: maroon;"></i></asp:LinkButton>--%>
                            
                                    <div class="form-group col-md-4">
                                        <label>From</label>
                                        <asp:TextBox ID="txtFrom" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label>To</label>
                                        <asp:TextBox ID="txtTo" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <asp:LinkButton ID="lnkSearchSingleAccount" runat="server"  CssClass="pull-left btn btn-info m-t-25" OnClick="lnkSearchSingleAccount_Click"><i class="fa fa-search"></i></asp:LinkButton>
                                        <asp:LinkButton ID="lnkCancelSearchSingleAccount" runat="server" CssClass="btn btn-danger m-t-25 pull-right" OnClick="lnkCancelSearchSingleAccount_Click" ToolTip="Click to clear search result"><i class="fa fa-times"></i></asp:LinkButton>
                                         <asp:LinkButton ID="lnkMainLedgerPring" runat="server" CssClass="btn btn-danger m-t-25 pull-right" OnClick="lnkMainLedgerPring_Click" ToolTip="Click to print"><i class="fa fa-print"></i></asp:LinkButton>
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
                                                <asp:BoundField DataField="DateCreated" HeaderText="Date" ItemStyle-Width="20%" ItemStyle-Wrap="false"></asp:BoundField>
                                                <asp:BoundField DataField="Item" HeaderText="Item" ItemStyle-Width="50%" ItemStyle-Wrap="false"></asp:BoundField>
                                                <asp:BoundField DataField="Debit" HeaderText="Debit" ItemStyle-Width="10%" ItemStyle-Wrap="false"></asp:BoundField>
                                                <asp:BoundField DataField="Credit" HeaderText="Credit" ItemStyle-Width="10%" ItemStyle-Wrap="false"></asp:BoundField>
                                                <asp:BoundField DataField="Balance" HeaderText="Balance" ItemStyle-Width="10%" ItemStyle-Wrap="false"></asp:BoundField>
                                            </Columns>
                                            <HeaderStyle BackColor="#4b23dd" ForeColor="White" />
                                        </asp:GridView>
                                    </div>
                                     <div class="col-xs-12">

                                         <%-- SSRS Report --%>
                                          <rsweb:reportviewer ID="rvMainCustomerLedger" runat="server" Width="100%" Height="800px" ></rsweb:reportviewer>
                                         <%-- SSRS Report --%>

                                     </div>
                                </div>
                            </div>
                        </section>
                    </div>

                    <div class="col-xs-12" id="AllAcounts" runat="server">
                        <section class="box ">
                            <header class="panel_header">
                                <h2 class="title pull-left">Customer Ledger</h2> 
                                <asp:Panel ID="Panel1" runat="server" DefaultButton="lnkSearch" CssClass="col-md-6 m-t-25 pull-right">
                                    
                            <asp:LinkButton ID="lnkPrint" runat="server" CssClass="btn btn-danger m-t-25 pull-right" OnClick="lnkPrint_Click" ToolTip="Click to print"><i class="fa fa-print"></i></asp:LinkButton>
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
                                        <asp:GridView ID="gvAllAcounts" runat="server" Width="100%" EmptyDataText="No Account found" AutoGenerateColumns="false"
                                            CssClass="table table-hover" Font-Size="12px" Font-Names="Open Sans" DataKeyNames="CompanyID" BackColor="White" 
                                            OnRowDataBound="gvAllAcounts_RowDataBound" HeaderStyle-HorizontalAlign="Center" OnRowCommand="gvAllAcounts_RowCommand">
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
                                        <asp:HiddenField ID="hfAccountName" runat="server"/>
                                    </div>
                                     <div class="col-xs-12">

                                         <%-- SSRS Report --%>
                                          <rsweb:reportviewer ID="rvCustomerLedger" runat="server" Width="100%" Height="800px" ></rsweb:reportviewer>
                                         <%-- SSRS Report --%>

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
