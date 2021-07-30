<%@ Page Title="" Language="C#" MasterPageFile="~/BiltySystem.Master" AutoEventWireup="true" CodeBehind="Banks.aspx.cs" Inherits="BiltySystem.Banks" %>
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

                    <div id="divNotification" runat="server"></div>
                     <asp:Panel ID="pnlView" runat="server" CssClass="row" Visible="false">
                        <div class="col-sm-12 col-md-12">
                            <div class="block-flat">
                                <asp:LinkButton ID="lnkCloseView" runat="server" ForeColor="Maroon" Font-Size="35px" ToolTip="Click to close view panel" CssClass="pull-right"><i class="fa fa-times-circle"></i></asp:LinkButton>
                                <div class="content">                                
                                       <div role="form">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group row">
                                                    <label class="control-label text-right col-md-3">Code:</label>
                                                    <div class="col-md-9">
                                                        <asp:Label ID="lblCodemodal" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <!--/span-->
                                            <div class="col-md-6">
                                                <div class="form-group row">
                                                    <label class="control-label text-right col-md-3">Name:</label>
                                                    <div class="col-md-9">
                                                        <asp:Label ID="lblNameModal" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <!--/span-->
                                        </div>
                                        <!--/row-->
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group row">
                                                    <label class="control-label text-right col-md-3">Phone:</label>
                                                    <div class="col-md-9">
                                                        <asp:Label ID="lblPhone" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group row">
                                                    <label class="control-label text-right col-md-3">Other Phone:</label>
                                                    <div class="col-md-9">
                                                        <asp:Label ID="lblOther" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!--row-->
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group row">
                                                    <label class="control-label text-right col-md-3">Home No:</label>
                                                    <div class="col-md-9">
                                                        <asp:Label ID="lblHome" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group row">
                                                    <label class="control-label text-right col-md-3">NIC#:</label>
                                                    <div class="col-md-9">
                                                        <asp:Label ID="lblNIC" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!--/row-->
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group row">
                                                    <label class="control-label text-right col-md-3">Address:</label>
                                                    <div class="col-md-9">
                                                        <asp:Label ID="lblAddress" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group row">
                                                    <label class="control-label text-right col-md-3">Description:</label>
                                                    <div class="col-md-9">
                                                        <asp:Label ID="lblDesModal" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        <!--/row-->
                                    </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <asp:Panel runat="server" CssClass="col-xs-12" ID="pnlInput" Visible="false">
                        <section class="box ">
                            <asp:LinkButton ID="lnkCloseInput" runat="server" OnClick="lnkCloseInput_Click" CssClass=" fa fa-times-circle pull-right" style="margin-top: 10px; margin-right: 10px;"></asp:LinkButton>
                            <div class="content-body">
                                <div class="row">
                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                        <div class="row">
                                            <div class="form-group col-md-3 pull-left">
                                                <label>Code</label>
                                                <asp:TextBox ID="txtCode" runat="server" CssClass="form-control"></asp:TextBox><asp:HiddenField ID="hfEditID" runat="server" />
                                            </div>
                                            <div class="form-group col-md-6 pull-left">
                                                <label>Name</label>
                                                <asp:DropDownList ID="ddlBank" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                    <asp:ListItem>Al Baraka Bank (Pakistan) Limitted</asp:ListItem>
                                                    <asp:ListItem>Allied Bank Limited</asp:ListItem>
                                                    <asp:ListItem>Askari Bank Limited</asp:ListItem>
                                                    <asp:ListItem>Bank Alfalah Limited</asp:ListItem>
                                                    <asp:ListItem>Bank Al-Habib Limited</asp:ListItem>
                                                    <asp:ListItem>BankIslami Pakistan Limited</asp:ListItem>
                                                    <asp:ListItem>Citi Bank N.A</asp:ListItem>
                                                    <asp:ListItem>Deutsche Bank A.G</asp:ListItem>
                                                    <asp:ListItem>The Bank of Tokyo-Mitsubishi UFJ</asp:ListItem>
                                                    <asp:ListItem>Dubai Islamic Bank Pakistan Limited</asp:ListItem>
                                                    <asp:ListItem>Faysal Bank Limited</asp:ListItem>
                                                    <asp:ListItem>First Women Bank Limited</asp:ListItem>
                                                    <asp:ListItem>Finca Microfinace Bank Limited</asp:ListItem>
                                                    <asp:ListItem>Habib Bank Limited</asp:ListItem>
                                                    <asp:ListItem>Standard Chartered Bank (Pakistan) Limited</asp:ListItem>
                                                    <asp:ListItem>Habib Metropolitan Bank Limited</asp:ListItem>
                                                    <asp:ListItem>Industrial and Commercial Bank of China</asp:ListItem>
                                                    <asp:ListItem>Industrial Development Bank of Pakistan</asp:ListItem>
                                                    <asp:ListItem>JS Bank Limited</asp:ListItem>
                                                    <asp:ListItem>MCB Bank Limited</asp:ListItem>
                                                    <asp:ListItem>21. MCB Islamic Bank Limited</asp:ListItem>
                                                    <asp:ListItem>Meezan Bank Limited</asp:ListItem>
                                                    <asp:ListItem>National Bank of Pakistan</asp:ListItem>
                                                    <asp:ListItem>S.M.E. Bank Limited</asp:ListItem>
                                                    <asp:ListItem>25. Samba Bank Limited</asp:ListItem>
                                                    <asp:ListItem>26. Silk Bank Limited</asp:ListItem>
                                                    <asp:ListItem>Sindh Bank Limited</asp:ListItem>
                                                    <asp:ListItem>Soneri Bank Limited</asp:ListItem>
                                                    <asp:ListItem>Summit Bank Limited</asp:ListItem>
                                                    <asp:ListItem>Tameer Bank</asp:ListItem>
                                                    <asp:ListItem>The Bank of Khyber</asp:ListItem>
                                                    <asp:ListItem>The Bank of Punjab</asp:ListItem>
                                                    <asp:ListItem>The Punjab Provincial Cooperative Bank Limited</asp:ListItem>
                                                    <asp:ListItem>United Bank Limited</asp:ListItem>
                                                    <asp:ListItem>Zarai Taraqiati Bank Limited</asp:ListItem>

                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-md-6 pull-left">
                                                <label>Account Title</label>
                                                <asp:TextBox ID="txtAccountTitle" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-6 pull-left">
                                                <label>Account #</label>
                                                <asp:TextBox ID="txtAccountNo" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-lg-12 pull-left">
                                        
                                                <asp:LinkButton ID="lnkSubmit" runat="server" CssClass="btn btn-success pull-right m-l-10" OnClick="lnkSubmit_Click"><i class="fa fa-save"></i> | Save</asp:LinkButton>
                                        
                                                <asp:LinkButton ID="lnkDelete" runat="server" CssClass="btn btn-danger pull-right" OnClick="lnkDelete_Click"><i class="fa fa-trash"></i> | Delete</asp:LinkButton>
                                        
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </section>
                    </asp:Panel>


                    <div class="col-xs-12">
                        <section class="box ">
                            <header class="panel_header">
                                <h2 class="title pull-left">Banks</h2> 
                                <div class="col-md-6 m-t-25 pull-right">
                                    <asp:LinkButton ID="lnkCancelSearch" runat="server" OnClick="lnkCancelSearch_Click"><i class="fa fa-times-circle m-t-10 m-r-5 pull-left" style="color: maroon;"></i></asp:LinkButton>
                            
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="pull-left m-r-10 pull-left"></asp:TextBox>
                                    <asp:LinkButton ID="lnkSearch" runat="server" OnClick="lnkSearch_Click" CssClass="pull-left"><i class="fa fa-search"></i></asp:LinkButton>
                                </div>
                                <div class="actions panel_actions pull-right">
                           
                                    <asp:LinkButton ID="lnkAddNew" runat="server" CssClass="box_setting fas fa-plus" OnClick="lnkAddNew_Click" ToolTip="Click to Add New"></asp:LinkButton>
                                </div>
                            </header>
                            <div class="content-body">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <asp:GridView ID="gvResult" runat="server" Width="100%" EmptyDataText="No Record found" AutoGenerateColumns="false"
                                            CssClass="table table-hover" Font-Size="12px" Font-Names="Open Sans" DataKeyNames="BankID, isActive" BackColor="White" 
                                            OnRowCommand="gvResult_RowCommand" OnRowDataBound="gvResult_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.no">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Code" HeaderText="Code"></asp:BoundField>
                                                <asp:BoundField DataField="Name" HeaderText="Name"></asp:BoundField>
                                                <asp:BoundField DataField="AccountNo" HeaderText="Account#"></asp:BoundField>
                                                <asp:BoundField DataField="AccountTitle" HeaderText="Account Title"></asp:BoundField>
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
