<%@ Page Title="" Language="C#" MasterPageFile="~/BiltySystem.Master" AutoEventWireup="true" CodeBehind="CompanyProfile.aspx.cs" Inherits="BiltySystem.CompanyProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
       <style>        
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
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
                                                <%--        <asp:Button ID="btnModal" runat="server" CssClass="btn btn-info" OnClick="btnModal_Click" />
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




                    <asp:Panel runat="server" CssClass="col-xs-12" ID="pnlInput" Visible="false">
                        <section class="box ">
                            <asp:LinkButton ID="lnkCloseInput" runat="server" CssClass=" fa fa-times-circle pull-right" Style="margin-top: 10px; margin-right: 10px;" OnClick="lnkCloseInput_Click"></asp:LinkButton>
                            <div class="content-body">
                                <div class="row">
                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                        <div class="form-group col-md-3 pull-left">
                                            <label>Type</label>
                                            <asp:RadioButtonList ID="rbCompanyType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbCompanyType_SelectedIndexChanged">
                                                <asp:ListItem Selected="True">Customer</asp:ListItem>
                                                <asp:ListItem>Broker</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        
                                        <div class="form-group col-md-3 pull-left" id="CustomerPlaceholder" runat="server">
                                            <label>Customer</label>
                                            <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control"></asp:DropDownList><asp:HiddenField runat="server" ID="hfEditID" />
                                        </div>

                                          <div class="form-group col-md-3 pull-left" id="BrokerPlaceholder" runat="server" visible="false">
                                            <label>Broker</label>
                                            <asp:DropDownList ID="ddlBroker" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>

                                        <div class="form-group col-md-3 pull-left">
                                            <label>Payment Type</label>

                                            <asp:DropDownList ID="ddlpayment" runat="server" CssClass="form-control">
                                                <asp:ListItem>-Select-</asp:ListItem>
                                                <asp:ListItem Value="Paid">Paid</asp:ListItem>
                                                <asp:ListItem Value="ToPay">ToPay</asp:ListItem>
                                            </asp:DropDownList>

                                        </div>
                                        <div class="form-group col-md-3 pull-left">
                                            <label>Credit Term</label>

                                            <asp:DropDownList ID="ddlCredit" runat="server" CssClass="form-control">
                                                <asp:ListItem>-Select-</asp:ListItem>
                                                <asp:ListItem Value="15">15 Days</asp:ListItem>
                                                <asp:ListItem Value="30">30 Days</asp:ListItem>
                                            </asp:DropDownList>

                                        </div>
                                        <%--<div class="form-group col-md-3 pull-left">
                                            <label>Invoice Format</label>

                                            <asp:DropDownList ID="ddlInvoice" runat="server" CssClass="form-control">
                                                <asp:ListItem>-Select-</asp:ListItem>
                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                <asp:ListItem Value="2">2</asp:ListItem>
                                            </asp:DropDownList>

                                        </div>--%>


                                        <div class="form-group col-lg-12 pull-left">
                                           <asp:LinkButton ID="lnkSubmit" runat="server" CssClass="btn btn-success pull-right m-l-10" OnClick="lnkSubmit_Click"><i class="fa fa-save"></i> | Save</asp:LinkButton>
                                           <asp:LinkButton ID="lnkDelete" runat="server" CssClass="btn btn-danger pull-right" Visible="false" OnClick="lnkDelete_Click"><i class="fa fa-trash"></i> | Delete</asp:LinkButton>                                        
                                         </div>
                                         
                                    </div>
                                </div>
                            </div>

                        </section>
                    </asp:Panel>


                     
                    <div class="col-xs-12">

                        <section class="box ">
                            

                             <header class="panel_header">
                                <h2 class="title pull-left">Customer Profile</h2> 
                                <div class="actions panel_actions pull-right">
                           
                                    <asp:LinkButton ID="lnkAddNew" runat="server" CssClass="box_setting fas fa-plus" OnClick="AddNew_Click" ToolTip="Click to Add New"></asp:LinkButton>
                                    <%--<a class="box_setting fa fa-cog" data-toggle="modal" href="#section-settings"></a>
                                    <a class="box_close fa fa-times"></a>--%>
                                </div>
                            </header>
                            <div class="content-body">
                               
                                <div class="row">
                                    <div class="col-xs-12">
                                        <asp:GridView ID="gvResult" runat="server" Width="100%" EmptyDataText="No Record found" AutoGenerateColumns="false"
                                            CssClass="table table-hover" Font-Size="12px" Font-Names="Open Sans" OnRowCommand="gvResult_RowCommand" DataKeyNames="ProfileID" BackColor="White">
                                            <Columns>

                                                <asp:BoundField DataField="Companies" HeaderText="Customer"></asp:BoundField>
                                                <asp:BoundField DataField="BrokerName" HeaderText="Broker"></asp:BoundField>
                                                <asp:BoundField DataField="PaymentTerm" HeaderText="Payment term"></asp:BoundField>
                                                <asp:BoundField DataField="CreditTerm" HeaderText="Credit term"></asp:BoundField>
                                                <asp:BoundField DataField="InvoiceFormat" HeaderText="Invoice Format"></asp:BoundField>
                                                <asp:BoundField DataField="CreatedDate" HeaderText="Created Date"></asp:BoundField>
                                                <asp:BoundField DataField="ModifiedDate" HeaderText="Modified Date"></asp:BoundField>

                                                
                                                <asp:TemplateField HeaderText="Update">

                                                    <ItemTemplate>

                                                        <asp:LinkButton ID="lnkEdit" runat="server" ForeColor="Blue" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="Change"><i class="fas fa-edit"></i></asp:LinkButton>
                                                    </ItemTemplate>
                      
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDelete" runat="server" ForeColor="Maroon" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="DeleteProfile"><i class="fas fa-trash"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Details">
                                              <ItemTemplate>
                                              <asp:LinkButton ID="lnkDetail" runat="server" ForeColor="Blue" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="Details">Add Detail</asp:LinkButton>
                                             </ItemTemplate>
                                              </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle BackColor="#4b23dd" ForeColor="White" />
                                        </asp:GridView>

                                        <ajaxToolkit:ModalPopupExtender ID="modalProfileDetail" runat="server" PopupControlID="pnlProfileDetail" DropShadow="True" TargetControlID="btnOpenProfileDetail"
                                            CancelControlID="lnkCloseProfileDetail" BackgroundCssClass="modalBackground">
                                        </ajaxToolkit:ModalPopupExtender>
                                        <asp:Panel ID="pnlProfileDetail" runat="server" CssClass="row" Style="background-color: white; padding: 20px; border: 1px solid black;" Width="900px" Height="550px">
                                            <asp:Button ID="btnOpenProfileDetail" runat="server" Style="display: none" />
                                            <asp:LinkButton ID="lnkCloseProfileDetail" runat="server" ForeColor="Maroon" CssClass="pull-right" Style="display: none;"><i class="fa fa-times-circle-o"></i></asp:LinkButton>
                                            <div runat="server" cssclass="col-xs-12">
                                                <section class="box ">
                                                    <asp:LinkButton ID="LinkButton1" OnClick="LinkButton1_Click" runat="server" CssClass=" fa fa-times-circle pull-right" Style="margin-top: 10px; margin-right: 10px; color: maroon;"></asp:LinkButton>
                                                    <div class="content-body">
                                                        <div class="row">
                                                            <div class="col-md-12 col-sm-12 col-xs-12">

                                                                <%--<div class="form-group col-md-3 pull-left">
                                                                    <label>Product</label>
                                                                    <asp:DropDownList ID="ddlProduct" runat="server" CssClass="form-control"></asp:DropDownList><asp:HiddenField ID="HiddenField1" runat="server" />

                                                                </div>--%>

                                                                <div class="form-group col-md-3 pull-left" id="LocationFromPlaceholder" runat="server">
                                                                    <label>Location From</label>
                                                                    <asp:DropDownList ID="ddlLocationFrom" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                </div>

                                                                <div class="form-group col-md-3 pull-left" id="LocationToPlaceholder" runat="server">
                                                                    <label>Location To</label>
                                                                    <asp:DropDownList ID="ddlLocationTo" runat="server" CssClass="form-control">
                                                                        <asp:ListItem>-Select-</asp:ListItem>
                                                                        <asp:ListItem Value="Paid">Paid</asp:ListItem>
                                                                        <asp:ListItem Value="ToPay">ToPay</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>

                                                                <div class="form-group col-md-3 pull-left" id="ContainerTypePlaceholder" runat="server">
                                                                    <label>Container Type</label>
                                                                    <asp:DropDownList ID="ddlContainerType" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                </div>  
                                                                <div class="form-group col-md-3 pull-left" id="VehicleTypePlaceHolder" runat="server">
                                                                    <label>Vehicle Type</label>
                                                                    <asp:DropDownList ID="ddlVehicleType" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                </div>  
                                                                
                                                                <%--<div class="form-group col-md-3 pull-left">
                                                                    <label>Freight Type</label>
                                                                    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control">
                                                                        <asp:ListItem>-Select-</asp:ListItem>
                                                                        <asp:ListItem>Container</asp:ListItem>
                                                                        <asp:ListItem>Vehicle</asp:ListItem>
                                                                        <asp:ListItem>Product</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>--%>

                                                                <%--<div class="form-group col-md-3 pull-left">
                                                                    <label>Rate Type</label>

                                                                    <asp:DropDownList ID="ddlRateType" runat="server" CssClass="form-control">
                                                                        <asp:ListItem>-Select-</asp:ListItem>
                                                                        <asp:ListItem>Kg</asp:ListItem>
                                                                        <asp:ListItem>Unit</asp:ListItem>
                                                                        <asp:ListItem>Lumsum</asp:ListItem>
                                                                    </asp:DropDownList>

                                                                </div>--%>
                                                                <div class="form-group col-md-3 pull-left" id="ContainerRatePlaceholder" runat="server">
                                                                    <label>Container Rate</label>
                                                                    <asp:TextBox runat="server" ID="txtContainerRate" CssClass="form-control" TextMode="Number"></asp:TextBox>

                                                                </div>
                                                                <div class="form-group col-md-3 pull-left" id="VehicleRatePlaceholder" runat="server">
                                                                    <label>Vehicle Rate</label>
                                                                    <asp:TextBox runat="server" ID="txtVehicleRate" CssClass="form-control" TextMode="Number"></asp:TextBox>

                                                                </div>
                                                                 <%--<div class="form-group col-md-3 pull-left">
                                                                    <label>Broker Rate</label>
                                                                    <asp:TextBox runat="server" ID="txtBrokerRate"></asp:TextBox>

                                                                </div>--%>
                                                                <%--<div class="form-group col-md-3 pull-left">
                                                                    <label>Door Step Rs.</label>
                                                                    <asp:TextBox runat="server" ID="txtDoorStep"></asp:TextBox>

                                                                </div>--%>


                                                                <div class="form-group col-lg-12 pull-left">
                                                                    <asp:LinkButton ID="btnAddDetail" runat="server" OnClick="btnAddDetail_Click" CssClass="btn btn-success pull-right m-l-10"><i class="fa fa-save"></i> | Save</asp:LinkButton>
                                                                    <asp:LinkButton ID="btnCancel" runat="server" OnClick="btnCancel_Click" CssClass="btn btn-danger pull-right m-l-10"><i class="fa fa-ban"></i> | Cancel</asp:LinkButton>

                                                                </div>
                                                            </div>
                                                        </div>
                                                        
                                                        <div id="divModalNotification" style="margin-top: 10px;" runat="server"></div>

                                                        <div class="row">
                                                            <asp:HiddenField runat="server" ID="hfEditDetail" />
                                                            <div style="width: 100%; max-height: 271px; overflow-y: scroll">
                                                                <asp:GridView ID="gvCustomerDetail" runat="server" Width="100%" EmptyDataText="No Record found" AutoGenerateColumns="false"
                                                                    CssClass="table table-hover" Font-Size="12px" Font-Names="Open Sans" OnRowCommand="gvCustomerDetail_RowCommand" DataKeyNames="ProfileDetail" BackColor="White">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="LocationFrom" HeaderText="From"></asp:BoundField>
                                                                        <asp:BoundField DataField="LocationTo" HeaderText="To"></asp:BoundField>
                                                                        <asp:BoundField DataField="ContainerType" HeaderText="Conatiner"></asp:BoundField>
                                                                        <asp:BoundField DataField="ContainerRate" HeaderText="Rate"></asp:BoundField>
                                                                        <asp:TemplateField HeaderText="Details">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkEdit" runat="server" ForeColor="Blue" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="ChangeDetail"><i class="fas fa-edit"></i></asp:LinkButton>
                                                                            </ItemTemplate>                                                                           
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Delete">
                                                                             <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkDelete" runat="server" ForeColor="Maroon" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="DeleteDetail"><i class="fas fa-trash"></i></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>                                                                      
                                                                    </Columns>
                                                                    <HeaderStyle BackColor="#4b23dd" ForeColor="White" />
                                                                </asp:GridView>
                                                                <asp:GridView ID="gvBrokerDetail" runat="server" Width="100%" EmptyDataText="No Record found" AutoGenerateColumns="false"
                                                                    CssClass="table table-hover" Font-Size="12px" Font-Names="Open Sans" OnRowCommand="gvBrokerDetail_RowCommand" DataKeyNames="ProfileDetail" BackColor="White">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="VehicleTypeName" HeaderText="VehicleTypeName"></asp:BoundField>
                                                                        <asp:BoundField DataField="BrokerRate" HeaderText="Rate"></asp:BoundField>
                                                                        
                                                                        <asp:TemplateField HeaderText="Details">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkEdit" runat="server" ForeColor="Blue" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="ChangeDetail"><i class="fas fa-edit"></i></asp:LinkButton>
                                                                            </ItemTemplate>                                                                           
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Delete">
                                                                             <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkDelete" runat="server" ForeColor="Maroon" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="DeleteDetail"><i class="fas fa-trash"></i></asp:LinkButton>
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
                                        </asp:Panel>
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
                            <asp:LinkButton ID="lnkConfirm" runat="server" ForeColor="Green" Font-Size="70px" OnClick="lnkConfirm_Click"><i class="fas fa-check pull-left"></i></asp:LinkButton>
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

