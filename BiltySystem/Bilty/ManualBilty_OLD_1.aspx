<%@ Page Title="" Language="C#" MasterPageFile="~/BiltySystem.Master" AutoEventWireup="true" CodeBehind="ManualBilty_OLD.aspx.cs" Inherits="BiltySystem.Bilty.ManualBilty_OLD" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Manually Bilty</title>
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


    <script>
        function onListPopulated() {
            debugger;
            var completionList = $find("AutoCompleteExtender1").get_completionList();
            completionList.style.width = 'auto';
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <!-- START CONTENT -->
    <section id="main-content" class=" ">
        <section class="wrapper main-wrapper row" style=''>

            <div class='col-xs-12'>
                <div class="page-title">

                    <%--<div class="pull-left">
                        <!-- PAGE HEADING TAG - START -->
                        <h1 class="title">Manual Bilty</h1>
                        <!-- PAGE HEADING TAG - END -->
                    </div>--%>


                </div>
            </div>
            <div class="clearfix"></div>
            <!-- MAIN CONTENT AREA STARTS -->
            <div id="divNotification" style="margin-top: 10px;" runat="server"></div>
            <div class="col-lg-12">
                <section class="box ">
                    <header class="panel_header">
                        <h2 class="title pull-left">Manual Bilty</h2>
                        <div class="actions panel_actions pull-right">
                            <a class="box_toggle fa fa-chevron-down"></a>
                            <a class="box_setting fa fa-cog" data-toggle="modal" href="#section-settings"></a>
                            <a class="box_close fa fa-times"></a>
                        </div>
                    </header>
                    <div class="content-body">
                        <div id="general_validate" action="javascript:;" novalidate="novalidate">
                            <div class="row">

                                <div class="col-xs-8">
                                    <div class="col-xs-6">
                                        <div class="form-group">
                                            <label class="form-label">Bilty No.</label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtBiltyNo" runat="server" class="form-control"></asp:TextBox>
                                                <%--<input type="text" class="form-control" name="formfield1">--%>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-xs-6">
                                        <div class="form-group">
                                            <label class="form-label">Bilty Date</label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtBiltyDate" runat="server" class="form-control" TextMode="Date"></asp:TextBox>
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
                <section class="box ">
                    <header class="panel_header">
                        <h2 class="title pull-left">Customer Information</h2>
                        <div class="actions panel_actions pull-right">
                            <a class="box_toggle fa fa-chevron-down"></a>
                            <a class="box_setting fa fa-cog" data-toggle="modal" href="#section-settings"></a>
                            <a class="box_close fa fa-times"></a>
                        </div>
                    </header>
                    <div class="content-body">
                        <div id="general_validate" action="javascript:;" novalidate="novalidate">
                            <div class="row">
                                <div class="col-xs-4">
                                    <div class="form-group">
                                        <label class="form-label">Search Consigner/Sender</label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtSearchSender" runat="server" class="form-control"></asp:TextBox>
                                            <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchCustomers"
                                                MinimumPrefixLength="2"
                                                CompletionListCssClass="list" 
	                                            CompletionListItemCssClass="listitem" 
	                                            CompletionListHighlightedItemCssClass="hoverlistitem"
                                                CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                                TargetControlID="txtSearchSender"
                                                ID="AutoCompleteExtender2" runat="server" FirstRowSelected="false">
                                            </ajaxToolkit:AutoCompleteExtender>
                                            <%--<input type="text" class="form-control" name="formfield1">--%>
                                        </div>
                                    </div>
                                </div>

                                
                                <div class="col-xs-2">
                                    <div class="form-group">
                                        <label class="form-label">Code</label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtSenderCompanyCode" runat="server" class="form-control"></asp:TextBox>
                                            <%--<input type="text" class="form-control" name="formfield1">--%>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-xs-2">
                                    <div class="form-group">
                                        <label class="form-label">Group</label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtSenderGroup" runat="server" class="form-control"></asp:TextBox>
                                            <%--<input type="text" class="form-control" name="formfield1">--%>
                                        </div>
                                    </div>
                                </div>
                                
                                <div class="col-xs-2">
                                    <div class="form-group">
                                        <label class="form-label">Company</label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtSenderCompany" runat="server" class="form-control"></asp:TextBox>
                                            <%--<input type="text" class="form-control" name="formfield1">--%>
                                        </div>
                                    </div>
                                </div>
                                
                                <div class="col-xs-2">
                                    <div class="form-group">
                                        <label class="form-label">Department</label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtSenderDepartment" runat="server" class="form-control"></asp:TextBox>
                                            <%--<input type="text" class="form-control" name="formfield1">--%>
                                        </div>
                                    </div>
                                </div>
                                
                                <div class="col-xs-4">
                                    <div class="form-group">
                                        <label class="form-label">Search Consignee/Receiver</label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtSearchReceiver" runat="server" class="form-control"></asp:TextBox>
                                            <%--<input type="text" class="form-control" name="formfield1">--%>
                                        </div>
                                    </div>
                                </div>
                                
                                
                                <div class="col-xs-2">
                                    <div class="form-group">
                                        <label class="form-label">Code</label>
                                        <div class="controls">
                                            <asp:TextBox ID="TextBox1" runat="server" class="form-control"></asp:TextBox>
                                            <%--<input type="text" class="form-control" name="formfield1">--%>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-xs-2">
                                    <div class="form-group">
                                        <label class="form-label">Group</label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtReceiverGroup" runat="server" class="form-control"></asp:TextBox>
                                            <%--<input type="text" class="form-control" name="formfield1">--%>
                                        </div>
                                    </div>
                                </div>
                                
                                <div class="col-xs-2">
                                    <div class="form-group">
                                        <label class="form-label">Company</label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtReceiverCompany" runat="server" class="form-control"></asp:TextBox>
                                            <%--<input type="text" class="form-control" name="formfield1">--%>
                                        </div>
                                    </div>
                                </div>
                                
                                <div class="col-xs-2">
                                    <div class="form-group">
                                        <label class="form-label">Department</label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtReceiverDepartment" runat="server" class="form-control"></asp:TextBox>
                                            <%--<input type="text" class="form-control" name="formfield1">--%>
                                        </div>
                                    </div>
                                </div>
                                
                                <div class="col-xs-4">
                                    <div class="form-group">
                                        <label class="form-label">Bill To/Customer</label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtSearchCustomer" runat="server" class="form-control"></asp:TextBox>
                                            <%--<input type="text" class="form-control" name="formfield1">--%>
                                        </div>
                                    </div>
                                </div>
                                
                                
                                <div class="col-xs-2">
                                    <div class="form-group">
                                        <label class="form-label">Code</label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtCustomerCode" runat="server" class="form-control"></asp:TextBox>
                                            <%--<input type="text" class="form-control" name="formfield1">--%>
                                        </div>
                                    </div>
                                </div>
                                
                                <div class="col-xs-2">
                                    <div class="form-group">
                                        <label class="form-label">Group</label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtCustomerGroup" runat="server" class="form-control"></asp:TextBox>
                                            <%--<input type="text" class="form-control" name="formfield1">--%>
                                        </div>
                                    </div>
                                </div>
                                
                                <div class="col-xs-2">
                                    <div class="form-group">
                                        <label class="form-label">Compnay</label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtCustomerCompany" runat="server" class="form-control"></asp:TextBox>
                                            <%--<input type="text" class="form-control" name="formfield1">--%>
                                        </div>
                                    </div>
                                </div>
                                
                                <div class="col-xs-2">
                                    <div class="form-group">
                                        <label class="form-label">Department</label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtCustomerDepartment" runat="server" class="form-control"></asp:TextBox>
                                            <%--<input type="text" class="form-control" name="formfield1">--%>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-xs-3">
                                    <div class="form-group">
                                        <label class="form-label">Payment Type</label>
                                        <div class="controls">
                                            <asp:DropDownList ID="ddlBillingType" runat="server" CssClass="form-control">
                                                <asp:ListItem>- Select -</asp:ListItem>
                                                <asp:ListItem>Vehicle Wise</asp:ListItem>
                                                <asp:ListItem>Weight Wise</asp:ListItem>
                                                <asp:ListItem>Container Wise</asp:ListItem>
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
            </div>

            <div class="col-lg-12">
                <section class="box ">
                    <header class="panel_header">
                        <h2 class="title pull-left">Shipping Information</h2>
                        <div class="actions panel_actions pull-right">
                            <a class="box_toggle fa fa-chevron-down"></a>
                            <a class="box_setting fa fa-cog" data-toggle="modal" href="#section-settings"></a>
                            <a class="box_close fa fa-times"></a>
                        </div>
                    </header>
                    <div class="content-body">
                        <div id="general_validate" action="javascript:;" novalidate="novalidate">
                            <div class="row">
                                <div class="col-xs-3">
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
                                <div class="col-xs-3">
                                    <div class="form-group">
                                        <label class="form-label">Loading Date</label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtLoadingDate" runat="server" class="form-control" TextMode="Date"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
            </div>

            <div class="col-lg-12">
                <section class="box ">
                    <header class="panel_header">
                        <h2 class="title pull-left">Location Information</h2>
                        <div class="actions panel_actions pull-right">
                            <a class="box_toggle fa fa-chevron-down"></a>
                            <a class="box_setting fa fa-cog" data-toggle="modal" href="#section-settings"></a>
                            <a class="box_close fa fa-times"></a>
                        </div>
                    </header>
                    <div class="content-body">
                        <div id="general_validate" action="javascript:;" novalidate="novalidate">
                            <div class="row">
                                <div class="col-xs-2">
                                    <div class="form-group">
                                        <label class="form-label">Pick Location</label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtSearchPickLocation" runat="server" class="form-control"></asp:TextBox>
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
                                            <asp:TextBox ID="txtSearchDropLocation" runat="server" class="form-control"></asp:TextBox>
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
            </div>

            <div class="col-lg-12">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <section class="box ">
                            <header class="panel_header">
                                <h2 class="title pull-left">Consigment Information</h2>
                                <div class="actions panel_actions pull-right">
                                    <a class="box_toggle fa fa-chevron-down"></a>
                                    <a class="box_setting fa fa-cog" data-toggle="modal" href="#section-settings"></a>
                                    <a class="box_close fa fa-times"></a>
                                </div>
                            </header>
                            <div class="content-body">
                                <div id="general_validate" action="javascript:;" novalidate="novalidate">
                                    <div class="row">
                                        <div class="col-xs-4">
                                            <div class="form-group">
                                                <label class="form-label">Clearing Agent</label>
                                                <div class="controls">
                                                    <asp:DropDownList ID="ddlClearingAgent" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div> 
                                        <div class="col-xs-3">
                                            <div class="form-group">
                                                <div class="controls">
                                                    <asp:LinkButton ID="lnkContainerInfo" runat="server" CssClass="btn btn-info m-t-25" OnClick="lnkContainerInfo_Click" ToolTip="Click to Add Container Info"><i class="fas fa-info"></i></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xs-12">
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <label class="form-label">Container Type</label>
                                                    <div class="controls">
                                                        <asp:DropDownList ID="ddlContainerType" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <label class="form-label">Container Qty</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtContainerQty" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <label class="form-label">Total Gross Weight</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtTotalGrossWeight" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <%--<label class="form-label">Total Gross Weight</label>--%>
                                                    <div class="controls">
                                                        <asp:LinkButton ID="lnkAddContainerType" runat="server" CssClass="btn btn-success pull-right m-t-25" OnClick="lnkAddContainerType_Click" ToolTip="Click to Add Container Type"><i class="fas fa-plus"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xs-12">
                                            <asp:GridView ID="gvConsignmentInfo" runat="server" Width="100%"></asp:GridView>
                                        </div>
                                
                                    </div>
                                </div>

                                <ajaxToolkit:ModalPopupExtender ID="modalContainerInfo" runat="server" PopupControlID="pnlContainerInfo" DropShadow="True" TargetControlID="btnOpenContainerInfo" 
                                    CancelControlID="lnkCloseContainerInfo" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlContainerInfo" runat="server" CssClass="row" style="background-color: white; padding: 20px; border: 1px solid black;" Width="1100px">
                                    <asp:Button ID="btnOpenContainerInfo" runat="server" style="display: none" />
                                    <asp:LinkButton ID="lnkCloseContainerInfo" runat="server" ForeColor="Maroon" CssClass="pull-right" style="display: none;"><i class="fa fa-times-circle-o"></i></asp:LinkButton>
                                    <h4>Container Info</h4>                       
                                    <div class="col-md-12">
                                        <asp:GridView ID="gvContainerInfo" runat="server" OnRowDataBound="gvContainerInfo_RowDataBound" Font-Size="10px" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:TemplateField HeaderText = "Row Number" ItemStyle-Width="100">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ContainerType">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblContainerType" runat="server" Text='<%# Eval("ContainerType") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ContainerNo">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtContainerNo" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Weight">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtWeight" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="EmptyContainerPickupExport">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlPickUpLocation" runat="server"></asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="EmptyContainerDropOff">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlDropoffLocation" runat="server"></asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="VesselName">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtVesselName" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remarks">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtRemarks" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:LinkButton ID="lnkSaveContainerInfo" runat="server" CssClass="btn btn-info" OnClick="lnkSaveContainerInfo_Click"><i class="fas fa-save"></i> | Save</asp:LinkButton>
                                        <asp:LinkButton ID="lnkCancelContainerInfo" runat="server" CssClass="btn btn-danger" OnClick="lnkCancelContainerInfo_Click"><i class="fas fa-ban"></i> | Cancel</asp:LinkButton>
                                    </div>
                                </asp:Panel>
                            </div>
                        </section>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <div class="col-lg-12">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <section class="box ">
                            <header class="panel_header">
                                <h2 class="title pull-left">Vehicle Information</h2>
                                <div class="actions panel_actions pull-right">
                                    <a class="box_toggle fa fa-chevron-down"></a>
                                    <a class="box_setting fa fa-cog" data-toggle="modal" href="#section-settings"></a>
                                    <a class="box_close fa fa-times"></a>
                                </div>
                            </header>
                            <div class="content-body">
                                <div id="general_validate" action="javascript:;" novalidate="novalidate">
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <label class="form-label">Vehicle Type</label>
                                                    <div class="controls">
                                                        <asp:DropDownList ID="ddlVehicleType" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <label class="form-label">Vehicle Quantity</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtVehicleQuantity" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <div class="controls">
                                                        <asp:LinkButton ID="lnkAddVehicleType" runat="server" CssClass="btn btn-success m-t-25" OnClick="lnkAddVehicleType_Click" ToolTip="Click to Add Vehicle Type"><i class="fas fa-plus"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <div class="controls">
                                                        <asp:LinkButton ID="lnkAddVehicleInfo" runat="server" CssClass="btn btn-info m-t-25" OnClick="lnkAddVehicleInfo_Click" ToolTip="Add Container Info"><i class="fas fa-info-circle"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="lnkViewVehicleInfo" runat="server" CssClass="btn btn-info pull-right m-t-25" OnClick="lnkViewVehicleInfo_Click" ToolTip="View/Edit Container Info"><i class="fas fa-eye"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xs-12">
                                            <asp:GridView ID="gvVehicleType" runat="server" Width="100%"></asp:GridView>
                                        </div>
                                        <ajaxToolkit:ModalPopupExtender ID="modalVehicleInfo" runat="server" PopupControlID="pnlVehicleInfo" DropShadow="True" TargetControlID="btnOpenVehicleInfo" 
                                            CancelControlID="lnkCloseVehicleInfo" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
                                        <asp:Panel ID="pnlVehicleInfo" runat="server" CssClass="row" style="background-color: white; padding: 20px; border: 1px solid black;" Width="1100px">
                                            <asp:Button ID="btnOpenVehicleInfo" runat="server" style="display: none" />
                                            <asp:LinkButton ID="lnkCloseVehicleInfo" runat="server" ForeColor="Maroon" CssClass="pull-right" style="display: none;"><i class="fa fa-times-circle-o"></i></asp:LinkButton>
                                            <h4>Container Info</h4>                       
                                            <div class="col-md-12">
                                                <asp:GridView ID="gvVehicleInfo" runat="server" OnRowDataBound="gvContainerInfo_RowDataBound" Font-Size="10px" AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText = "Row Number" ItemStyle-Width="100">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="VehicleType">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblVehicleType" runat="server" Text='<%# Eval("VehicleType") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="VehicleRegNo">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtVehicleNo" runat="server"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="VehicleContactNo">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtVehicleContactNo" runat="server"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Broker">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlBroker" runat="server"></asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Driver">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtDriverName" runat="server"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="FatherName">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtFatherName" runat="server"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DriverNIC">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtDriverNIC" runat="server"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DriverLicence">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtDriverLicence" runat="server"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DriverCellNo">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtDriveCellNo" runat="server"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:LinkButton ID="lnkSaveVehicleInfo" runat="server" CssClass="btn btn-info" OnClick="lnkSaveVehicleInfo_Click"><i class="fas fa-save"></i> | Save</asp:LinkButton>
                                                <asp:LinkButton ID="lnkCancelSaveVehicleInfo" runat="server" CssClass="btn btn-danger" OnClick="lnkCancelSaveVehicleInfo_Click"><i class="fas fa-ban"></i> | Cancel</asp:LinkButton>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                        </section>
                    </ContentTemplate>
                </asp:UpdatePanel>
                
            </div>

            <div class="col-lg-12">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>                    
                        <section class="box ">
                            <header class="panel_header">
                                <h2 class="title pull-left">Product</h2>
                                <div class="actions panel_actions pull-right">
                                    <a class="box_toggle fa fa-chevron-down"></a>
                                    <a class="box_setting fa fa-cog" data-toggle="modal" href="#section-settings"></a>
                                    <a class="box_close fa fa-times"></a>
                                </div>
                            </header>
                            <div class="content-body">
                                <div id="general_validate" action="javascript:;" novalidate="novalidate">
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div class="col-xs-3">
                                                <div class="form-group">
                                                    <label class="form-label">Search</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtSearchProduct" runat="server" CssClass="form-control" OnTextChanged="txtSearchProduct_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchCustomers"
                                                            MinimumPrefixLength="2"
                                                            CompletionListCssClass="list" 
	                                                        CompletionListItemCssClass="listitem" 
	                                                        CompletionListHighlightedItemCssClass="hoverlistitem"
                                                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                                            TargetControlID="txtSearchProduct"
                                                            ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false">
                                                        </ajaxToolkit:AutoCompleteExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-2">
                                                <div class="form-group">
                                                    <label class="form-label">Package Type</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtPackageType" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-2">
                                                <div class="form-group">
                                                    <label class="form-label">Item</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtItem" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-2">
                                                <div class="form-group">
                                                    <label class="form-label">Qty</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtProductQty" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtProductQty_TextChanged"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-2">
                                                <div class="form-group">
                                                    <label class="form-label">Total Weight</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtTotalProductWeight" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-1">
                                                <div class="form-group">
                                                    <div class="controls">
                                                        <asp:LinkButton ID="lnkAddProduct" runat="server" CssClass="btn btn-success m-t-25" OnClick="lnkAddProduct_Click" ToolTip="Click to Add Product"><i class="fas fa-plus"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xs-12">
                                            <asp:GridView ID="gvProducts" runat="server" Width="100%"></asp:GridView>
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
                        <h2 class="title pull-left">Dispatch Document Detail</h2>
                        <div class="actions panel_actions pull-right">
                            <a class="box_toggle fa fa-chevron-down"></a>
                            <a class="box_setting fa fa-cog" data-toggle="modal" href="#section-settings"></a>
                            <a class="box_close fa fa-times"></a>
                        </div>
                    </header>
                    <div class="content-body">
                        <div id="general_validate" action="javascript:;" novalidate="novalidate">
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="col-xs-3">
                                        <div class="form-group">
                                            <label class="form-label">Document Type</label>
                                            <div class="controls">
                                                <asp:DropDownList ID="ddlDocumentType" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-xs-3">
                                        <div class="form-group">
                                            <label class="form-label">Document No.</label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtDocumentNo" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>                                    
                                    <div class="col-xs-3">
                                        <div class="form-group">
                                            <label class="form-label">Upload Document</label>
                                            <div class="controls">
                                                <asp:FileUpload ID="fuDispatchDoc" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-xs-3">
                                        <div class="form-group">
                                            <%--<label class="form-label">Total Gross Weight</label>--%>
                                            <div class="controls">
                                                <asp:LinkButton ID="lnkAddDispatchDocument" runat="server" CssClass="btn btn-success pull-right m-t-25" OnClick="lnkAddDispatchDocument_Click" ToolTip="Click to Add Document"><i class="fas fa-plus"></i></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xs-12">
                                    <asp:GridView ID="gvDispatchDocument" runat="server" Width="100%"></asp:GridView>
                                </div>
                                
                            </div>
                        </div>
                    </div>
                </section>
            </div>

            <div class="col-lg-12">
                <section class="box ">
                    <header class="panel_header">
                        <h2 class="title pull-left">Receiving Information</h2>
                        <div class="actions panel_actions pull-right">
                            <a class="box_toggle fa fa-chevron-down"></a>
                            <a class="box_setting fa fa-cog" data-toggle="modal" href="#section-settings"></a>
                            <a class="box_close fa fa-times"></a>
                        </div>
                    </header>
                    <div class="content-body">
                        <div id="general_validate" action="javascript:;" novalidate="novalidate">
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="col-xs-3">
                                        <div class="form-group">
                                            <label class="form-label">ReceivedBy</label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtReceivedBy" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-xs-3">
                                        <div class="form-group">
                                            <label class="form-label">Receiving Date</label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtReceivingDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-xs-3">
                                        <div class="form-group">
                                            <label class="form-label">Receiving Time</label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtReceivingTime" runat="server" CssClass="form-control" TextMode="Time"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-xs-3">
                                        <div class="form-group">
                                            <div class="controls">
                                                <asp:LinkButton ID="lnkAddReceiving" runat="server" CssClass="btn btn-success m-t-25 pull-right" OnClick="lnkAddReceiving_Click" ToolTip="Add Receiving"><i class="fas fa-plus"></i></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xs-12">
                                    <asp:GridView ID="gvReceiving" runat="server" Width="100%"></asp:GridView>
                                </div>
                                
                            </div>
                        </div>
                    </div>
                </section>
            </div>

            <div class="col-lg-12">
                <section class="box ">
                    <header class="panel_header">
                        <h2 class="title pull-left">Receiving Document Detail</h2>
                        <div class="actions panel_actions pull-right">
                            <a class="box_toggle fa fa-chevron-down"></a>
                            <a class="box_setting fa fa-cog" data-toggle="modal" href="#section-settings"></a>
                            <a class="box_close fa fa-times"></a>
                        </div>
                    </header>
                    <div class="content-body">
                        <div id="general_validate" action="javascript:;" novalidate="novalidate">
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="col-xs-3">
                                        <div class="form-group">
                                            <label class="form-label">Document Type</label>
                                            <div class="controls">
                                                <asp:DropDownList ID="ddlReceivingDocumentType" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-xs-3">
                                        <div class="form-group">
                                            <label class="form-label">Document No.</label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtReceivingDocumentNo" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>                                    
                                    <div class="col-xs-3">
                                        <div class="form-group">
                                            <label class="form-label">Upload Document</label>
                                            <div class="controls">
                                                <asp:FileUpload ID="fuReceivingDoc" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-xs-3">
                                        <div class="form-group">
                                            <%--<label class="form-label">Total Gross Weight</label>--%>
                                            <div class="controls">
                                                <asp:LinkButton ID="lnkAddReceivingDocument" runat="server" CssClass="btn btn-success m-t-25 pull-right" OnClick="lnkAddReceivingDocument_Click" ToolTip="Click to Add Document"><i class="fas fa-plus"></i></asp:LinkButton>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xs-12">
                                    <asp:GridView ID="gvReceivingDocument" runat="server" Width="100%"></asp:GridView>
                                </div>
                                
                            </div>
                        </div>
                    </div>
                </section>
            </div>

            <div class="col-lg-12">
                <section class="box ">
                    <header class="panel_header">
                        <h2 class="title pull-left">Damage Detail</h2>
                        <div class="actions panel_actions pull-right">
                            <a class="box_toggle fa fa-chevron-down"></a>
                            <a class="box_setting fa fa-cog" data-toggle="modal" href="#section-settings"></a>
                            <a class="box_close fa fa-times"></a>
                        </div>
                    </header>
                    <div class="content-body">
                        <div id="general_validate" action="javascript:;" novalidate="novalidate">
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="col-xs-2">
                                        <div class="form-group">
                                            <label class="form-label">Item</label>
                                            <div class="controls">
                                                <asp:DropDownList ID="ddlDamageItem" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-xs-2">
                                        <div class="form-group">
                                            <label class="form-label">Damage Type</label>
                                            <div class="controls">
                                                <asp:DropDownList ID="ddlDamageType" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>                                    
                                    <div class="col-xs-2">
                                        <div class="form-group">
                                            <label class="form-label">Claim/Damage Cost</label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtDamageCost" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-xs-2">
                                        <div class="form-group">
                                            <label class="form-label">Damage Cause</label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtDamageCause" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>                               
                                    <div class="col-xs-2">
                                        <div class="form-group">
                                            <label class="form-label">Upload Document</label>
                                            <div class="controls">
                                                <asp:FileUpload ID="fuDamageDoc" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-xs-2">
                                        <div class="form-group">
                                            <div class="controls">
                                                <asp:LinkButton ID="lnkDamageCostSharing" runat="server" CssClass="btn btn-success btn-xs col-xs-6" OnClick="lnkDamageCostSharing_Click" ToolTip="Click to add Damage cost sharing"><i class="fas fa-money"></i></asp:LinkButton>
                                                <asp:LinkButton ID="lnkAddDamage" runat="server" CssClass="btn btn-info btn-xs col-xs-6" OnClick="lnkAddDamage_Click" ToolTip="Click to add Damage"><i class="fas fa-plus"></i></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xs-12">
                                    <asp:GridView ID="gvDamage" runat="server" Width="100%"></asp:GridView>
                                </div>
                                
                            </div>
                        </div>
                    </div>
                </section>
            </div>

            <!-- MAIN CONTENT AREA ENDS -->
        </section>
    </section>
    <!-- END CONTENT -->
</asp:Content>
