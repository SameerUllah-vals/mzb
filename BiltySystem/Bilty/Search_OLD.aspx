<%@ Page Title="" Language="C#" MasterPageFile="~/Bilty/BiltySystem.Master" AutoEventWireup="true" CodeBehind="Search_OLD.aspx.cs" Inherits="BiltySystem.Bilty.Search_OLD" %>
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
    </style>
    
    <script src="../Scripts/jquery-1.7.min.js"></script>
    <script src="../Scripts/select2.js"></script>

    <link href="../Content/css/select2.css" rel="stylesheet" />

    <script type="text/javascript">

        $(document).ready(function () {            

            $("#<%=ddlProductItem.ClientID%>").select2({

                placeholder: "Select Item",

                allowClear: true

            });


        });

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
                        <div class="actions panel_actions pull-right">
                            <a class="box_toggle fa fa-chevron-up"></a>
                        </div>
                    </header>
                    <div class="content-body" style="display: none;">
                        <div id="general_validate" action="javascript:;" novalidate="novalidate">
                            <div class="row">
                                <div class="col-xs-8">
                                    <div class="col-xs-12 col-sm-6">
                                        <div class="form-group">
                                            <label class="form-label">Keyword</label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtKeyword" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xs-4">      
                                    <asp:LinkButton ID="lnkSearch" runat="server" CssClass="btn btn-success pull-right m-r-10 m-t-25" ToolTip="Click to Search Bilty"><i class="fas fa-search"></i></asp:LinkButton>
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
                                <div id="general_validate" action="javascript:;" novalidate="novalidate">
                                    <div class="row">
                                        <asp:HiddenField ID="hfSelectedOrder" runat="server" />
                                        <asp:GridView ID="gvBilty" runat="server" CssClass="table table-hover" AutoGenerateColumns="false" Font-Size="10px"
                                            DataKeyNames="OrderID, Vehicles, Containers, Products, Recievings, RecievingDocs, Damages" OnRowCommand="gvBilty_RowCommand">
                                            <Columns>
                                                <asp:BoundField DataField="OrderNo" HeaderText="Bilty #" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="RecordedDate" HeaderText="Date" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="SenderCompany" HeaderText="Sender" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="ReceiverCompany" HeaderText="Reciever" ItemStyle-HorizontalAlign="Center" />
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
                                                <asp:TemplateField HeaderText="Recievings" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkRecievings" runat="server" CommandName="BiltyRecievings" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'><i class="fas fa-receipt"></i></asp:LinkButton> | <asp:Label ID="lblTotalReceivings" runat="server" Text='<%# Eval("Recievings") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Recieving Docs" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkRecievingDocs" runat="server" CommandName="BiltyRecievingDocs" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'><i class="fas fa-dolly-flatbed"></i></asp:LinkButton> | <asp:Label ID="lblTotalReceivingDocs" runat="server" Text='<%# Eval("RecievingDocs") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Damages" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDamages" runat="server" CommandName="BiltDamages" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'><i class="fas fa-chain-broken"></i></asp:LinkButton> | <asp:Label ID="lblTotalRecievingDocs" runat="server" Text='<%# Eval("Damages") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="BiltyFreight" HeaderText="Bilty Freight" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="PartyCommission" HeaderText="Party Commission" ItemStyle-HorizontalAlign="Center" />
                                                <asp:TemplateField HeaderText="Update">
                                                    <ItemTemplate>
                                                        <div class="dropdown">
                                                            <button class="btn btn-xs btn-default dropdown-toggle" type="button" id="dropdownMenu1" data-toggle="dropdown" aria-expanded="true">
                                                                Edit
                                                                <span class="caret"></span>
                                                            </button>
                                                            <ul class="dropdown-menu" role="menu" aria-labelledby="dropdownMenu1">
                                                                <li role="presentation"><asp:LinkButton ID="lnkEdit" runat="server" CommandName="Change" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Text="Only Bilty"></asp:LinkButton></li>
                                                                <li role="presentation"><asp:LinkButton ID="lnkEditWhole" runat="server" CommandName="ChangeAll" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' Text="Whole Bilty"></asp:LinkButton></li>
                                                            </ul>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                                <ajaxToolkit:ModalPopupExtender ID="modalBilty" runat="server" PopupControlID="pnlBilty" DropShadow="True" TargetControlID="btnOpenBilty" 
                            CancelControlID="lnkCloseBilty" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlBilty" runat="server" CssClass="row" style="background-color: white; padding: 20px; border: 1px solid black; height: 600px; overflow-y: scroll" Width="1100px">
                            
                                    <asp:Button ID="btnOpenBilty" runat="server" style="display: none" />
                                    <asp:LinkButton ID="lnkCloseBilty" runat="server" ForeColor="Maroon" CssClass="pull-right" style="display: none;"><i class="fa fa-times-circle-o"></i></asp:LinkButton>
                                    <h4 class="pull-left"><asp:Label ID="Label6" runat="server"></asp:Label></h4> 
                                    <%--<asp:LinkButton ID="LinkButton2" runat="server" ForeColor="Maroon" CssClass="pull-right" OnClick="lnkCloseBiltyVehicle_Click"><i class="fas fa-times-circle"></i></asp:LinkButton>--%>
                                    
                                    <div class="row">                                
                                        <asp:Panel ID="Panel2" runat="server" class="col-md-12">
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
                                                                                <asp:TextBox ID="txtBiltyNo" runat="server" class="form-control"></asp:TextBox>
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
                                                                <div class="col-xs-4">                                
                                                                    <asp:LinkButton ID="lnkCancelledBilty" runat="server" CssClass="btn btn-danger pull-right m-t-25" ToolTip="Cancelled Bilty"><i class="fas fa-ban"></i></asp:LinkButton>
                                                                    <asp:LinkButton ID="lnkCombinedBilty" runat="server" CssClass="btn btn-info pull-right m-r-10 m-t-25" ToolTip="Combined Bilty"><i class="fas fa-compress-arrows-alt"></i></asp:LinkButton>
                                                                    <asp:LinkButton ID="lnkMergedBilty" runat="server" CssClass="btn btn-success pull-right m-r-10 m-t-25" ToolTip="Merged Bilty"><i class="fas fa-link"></i></asp:LinkButton>
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
                                                                                    <asp:DropDownList ID="ddlSearchSender" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
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
                                                                                    <asp:DropDownList ID="ddlSearchReceiver" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
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

                                                                                    <asp:DropDownList ID="ddlSearchCustomer" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
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
                                                                                <%--<label class="form-label">Payment Type</label>--%>
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
                                                                        <%--<div class="col-xs-12">
                                                                            <div class="pull-right ">
                                                                                <button type="submit" class="btn btn-success">Save</button>
                                                                                <button type="button" class="btn">Cancel</button>
                                                                            </div>
                                                                        </div>--%>
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
                                                        <%--<div class="actions panel_actions pull-right">
                                                            <a class="box_toggle fa fa-chevron-down"></a>
                                                
                                                
                                                        </div>--%>
                                                    </header>
                                                    <div class="content-body">
                                                        <div id="general_validate" action="javascript:;" novalidate="novalidate">
                                                            <div class="row">

                                                                <div id="divShippingInfoNotification" runat="server"></div>
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
                                                            </div>
                                                        </div>
                                                    </div>
                                                </section>
                                            </div>

                                            <div class="col-lg-12">
                                                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
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
                                                                <%--<div class="actions panel_actions pull-right">
                                                                    <a class="box_toggle fa fa-chevron-down"></a>
                                                        
                                                        
                                                                </div>--%>
                                                            </header>
                                                            <div class="content-body">
                                                                <div id="general_validate" action="javascript:;" novalidate="novalidate">
                                                                    <div class="row">
                                                                        <div class="col-xs-12">
                                                                            <div id="divBiltyFreightNotification" runat="server"></div>
                                                                            <div class="col-xs-3">
                                                                                <div class="form-group">
                                                                                    <label class="form-label">Bilty Freight</label>
                                                                                    <div class="controls">
                                                                                        <asp:TextBox ID="txtBiltyFreight" runat="server" CssClass="form-control" TextMode="Number" AutoPostBack="true"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-xs-3">
                                                                                <div class="form-group">
                                                                                    <label class="form-label">Freight</label>
                                                                                    <div class="controls">
                                                                                        <asp:TextBox ID="txtFreight" runat="server" CssClass="form-control" TextMode="Number" AutoPostBack="true"></asp:TextBox>
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
                                                            </div>
                                                        </section>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>

                                            <div class="col-lg-12">
                                                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
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
                                                                                        <asp:TextBox ID="txtAdvanceFreight" runat="server" CssClass="form-control" TextMode="Number" AutoPostBack="true"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-xs-2">
                                                                                <div class="form-group">
                                                                                    <label class="form-label">Factory Advance</label>
                                                                                    <div class="controls">
                                                                                        <asp:TextBox ID="txtFactoryAdvance" runat="server" CssClass="form-control" TextMode="Number" AutoPostBack="true"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-xs-2">
                                                                                <div class="form-group">
                                                                                    <label class="form-label">Diesel</label>
                                                                                    <div class="controls">
                                                                                        <asp:TextBox ID="txtDieselAdvance" runat="server" CssClass="form-control" TextMode="Number" AutoPostBack="true"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-xs-2" id="divAdvanceVehicle" runat="server" visible="false">
                                                                                <div class="form-group">
                                                                                    <label class="form-label">Adv. Amount</label>
                                                                                    <div class="controls">
                                                                                        <asp:TextBox ID="txtVehicleAdvanceAmount" runat="server" CssClass="form-control" TextMode="Number" AutoPostBack="true"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-xs-2">
                                                                                <div class="form-group">
                                                                                    <label class="form-label">Adv. Vehicle?</label>
                                                                                    <div class="controls">
                                                                                        <asp:CheckBox ID="cbAdvVehicle" runat="server" AutoPostBack="true" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                            
                                                                            <div class="col-xs-2 pull-right">
                                                                                <div class="form-group">
                                                                                    <label class="form-label">Total Advance</label>
                                                                                    <div class="controls">
                                                                                        <asp:TextBox ID="txtTotalAdvance" runat="server" Text="0" CssClass="form-control" Enabled="false" AutoPostBack="true"></asp:TextBox>
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
                                <asp:Panel ID="pnlBiltyVehicles" runat="server" CssClass="row" style="background-color: white; padding: 20px; border: 1px solid black;" Width="1100px">
                            
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
                                            <asp:LinkButton ID="lnkAddNewBiltyVehicle" runat="server" CssClass="btn btn-xs btn-info pull-right m-b-10 m-r-10" OnClick="lnkAddNewBiltyVehicle_Click"><i class="fas fa-plus"></i> | Add New</asp:LinkButton>
                                            <asp:GridView ID="gvBiltyVehicles" runat="server" Font-Size="12px" CssClass="table table-hover" AutoGenerateColumns="false" 
                                                EmptyDataText="No vehicle assigned to selected bilty" OnRowCommand="gvBiltyVehicles_RowCommand" DataKeyNames="OrderVehicleID">
                                                <Columns>
                                                    <asp:BoundField DataField="VehicleType" HeaderText="Type" />
                                                    <asp:BoundField DataField="VehicleRegNo" HeaderText="Reg. No." />
                                                    <asp:BoundField DataField="VehicleContactNo" HeaderText="Vehicle Contact #" />
                                                    <asp:BoundField DataField="BrokerID" HeaderText="Broker" />
                                                    <asp:BoundField DataField="DriverName" HeaderText="Driver" />
                                                    <asp:BoundField DataField="DriverName" HeaderText="Driver" />
                                                    <asp:BoundField DataField="DriverCellNo" HeaderText="Contact" />
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEdit" runat="server" CssClass="btn btn-xs btn-info" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="Change"><i class="fas fa-edit"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField>
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
                                <asp:Panel ID="pnlBiltyContainers" runat="server" CssClass="row" style="background-color: white; padding: 20px; border: 1px solid black;" Width="1100px">
                            
                                    <asp:Button ID="btnOpenBiltyContainers" runat="server" style="display: none" />
                                    <asp:LinkButton ID="lnkCloseBiltyContainers" runat="server" ForeColor="Maroon" CssClass="pull-right" style="display: none;"><i class="fa fa-times-circle-o"></i></asp:LinkButton>
                                    <h4 class="pull-left"><asp:Label ID="Label1" runat="server"></asp:Label></h4> 
                                    <asp:LinkButton ID="lnkCloseBiltyContainer" runat="server" ForeColor="Maroon" CssClass="pull-right" OnClick="lnkCloseBiltyContainer_Click"><i class="fas fa-times-circle"></i></asp:LinkButton>
                                    
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
                                                    <label class="form-label">Vessel Name</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtVesselName" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-12 col-sm-2">
                                                <div class="form-group">
                                                    <label class="form-label">Remarks</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control"></asp:TextBox>
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
                                            <div class="col-xs-12">
                                                <div class="form-group">
                                                    <div class="controls">
                                                        <asp:LinkButton ID="lnkCancelSaveBiltyContainer" runat="server" CssClass="btn btn-danger pull-right m-b-10" OnClick="lnkCancelSaveBiltyContainer_Click"><i class="fas fa-times"></i> | Cancel</asp:LinkButton>
                                                        <asp:LinkButton ID="lnkSaveBiltyContainer" runat="server" CssClass="btn btn-primary pull-right m-b-10 m-r-10" OnClick="lnkSaveBiltyContainer_Click"><i class="fas fa-save"></i> | Save</asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <div class="col-md-12">
                                            <div id="divContainerNotifications" runat="server"></div>
                                            
                                            <asp:HiddenField ID="hfSelectedOrderContainer" runat="server" />
                                            <asp:LinkButton ID="lnkAddNewContainer" runat="server" CssClass="btn btn-xs btn-info pull-right m-b-10 m-r-10" OnClick="lnkAddNewContainer_Click"><i class="fas fa-plus"></i> | Add New</asp:LinkButton>
                                            <asp:GridView ID="gvContainer" runat="server" Font-Size="10px" CssClass="table table-hover" AutoGenerateColumns="false"
                                                EmptyDataText="No container assigned to selected bilty" OnRowCommand="gvContainer_RowCommand" DataKeyNames="OrderConsignmentID">
                                                <Columns>
                                                    <asp:BoundField DataField="ContainerTypeName" HeaderText="Container" />
                                                    <asp:BoundField DataField="ContainerNo" HeaderText="Container #" />
                                                    <asp:BoundField DataField="ContainerWeight" HeaderText="Weight" />
                                                    <asp:BoundField DataField="EmptyContainerDropLocation" HeaderText="Drop Location" />
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEditContainer" runat="server" CssClass="btn btn-xs btn-info" ToolTip="Click to Edit" CommandName="Change" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'><i class="fas fa-edit"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkDeleteContainert" runat="server" CssClass="btn btn-xs btn-danger" ToolTip="Click to Delete" CommandName="Wipe" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'><i class="fas fa-trash"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                
                            
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <ajaxToolkit:ModalPopupExtender ID="modalProducts" runat="server" PopupControlID="pnlBiltyProducts" DropShadow="True" TargetControlID="btnOpenBiltyProducts" 
                            CancelControlID="lnkCloseBiltyProducts" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlBiltyProducts" runat="server" CssClass="row" style="background-color: white; padding: 20px; border: 1px solid black;" Width="1100px">
                            
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
                                            <asp:LinkButton ID="lnkAddNewProduct" runat="server" CssClass="btn btn-xs btn-info pull-right m-b-10 m-r-10" OnClick="lnkAddNewProduct_Click"><i class="fas fa-plus"></i> | Add New</asp:LinkButton>
                                            <asp:GridView ID="gvProduct" runat="server" Font-Size="10px" CssClass="table table-hover" AutoGenerateColumns="false"
                                                EmptyDataText="No Product assigned to selected bilty" OnRowCommand="gvProduct_RowCommand" DataKeyNames="OrderProductID">
                                                <Columns>
                                                    <asp:BoundField DataField="Item" HeaderText="Product" />
                                                    <asp:BoundField DataField="PackageType" HeaderText="Packaging" />
                                                    <asp:BoundField DataField="Qty" HeaderText="Quantity" />
                                                    <asp:BoundField DataField="TotalWeight" HeaderText="Weight" />
                                                    <asp:TemplateField HeaderText="Update">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEdit" runat="server" CssClass="btn btn-xs btn-info" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="Change"><i class="fas fa-edit"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete">
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
                                <asp:Panel ID="pnlBiltyRecievings" runat="server" CssClass="row" style="background-color: white; padding: 20px; border: 1px solid black;" Width="1100px">
                            
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
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEdit" runat="server" CssClass="btn btn-xs btn-info" CommandName="Change" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'><i class="fas fa-edit"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
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

                        <%--<asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>--%>
                                <ajaxToolkit:ModalPopupExtender ID="modalRecievingDocs" runat="server" PopupControlID="pnlBiltyRecievingDocs" DropShadow="True" TargetControlID="btnOpenBiltyRecievingDocs" 
                            CancelControlID="lnkCloseBiltyRecievingDocs" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlBiltyRecievingDocs" runat="server" CssClass="row" style="background-color: white; padding: 20px; border: 1px solid black;" Width="1100px">
                            
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
                                                    <asp:TemplateField HeaderText="Delete">
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
                                <asp:Panel ID="pnlBiltyDamages" runat="server" CssClass="row" style="background-color: white; padding: 20px; border: 1px solid black;" Width="1100px">
                            
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
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEdit" runat="server" CssClass="btn btn-xs btn-info" CommandName="Change" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'><i class="fas fa-edit"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
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
                    </div>
                </section>
            </div>

            <!-- MAIN CONTENT AREA ENDS -->
        </section>
    </section>
    <!-- END CONTENT -->
    
    <script src="../Scripts/jquery-1.7.min.js"></script>
    
    


</asp:Content>
