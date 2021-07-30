<%@ Page Title="" Language="C#" MasterPageFile="~/BiltySystem.Master" AutoEventWireup="true" CodeBehind="Driver.aspx.cs" Inherits="BiltySystem.Driver" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Driver</title>
    <style>        
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ClientIDMode="Static">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <!-- START CONTENT -->
    <section id="main-content" class=" ">
        <section class="wrapper main-wrapper row" style=''>
            
            <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    
                    <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                        <ProgressTemplate>
                            <div class="modalBackground" style="position: fixed; left: 0; width: 100%; height: 100%; z-index: 1;">
                                <img src="assets/images/loader.gif" style="position: fixed; top: 40%; left: 45%; margin-top: -50px; margin-left: -100px;">
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>--%>

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


                     <asp:Panel ID="pnlView" runat="server" CssClass="row" Visible="false">
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
                                            
                                            <div class="col-md-6">
                                                <div class="form-group row">
                                                    <label class="control-label text-right col-md-3">Father:</label>
                                                    <div class="col-md-9">
                                                        <asp:Label ID="lblFather" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>




                                            <div class="col-md-6">
                                                <div class="form-group row">
                                                    <label class="control-label text-right col-md-3">Type:</label>
                                                    <div class="col-md-9">
                                                        <asp:Label ID="lblTypeModal" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            
                                            <div class="col-md-6">
                                                <div class="form-group row">
                                                    <label class="control-label text-right col-md-3">Date Of Birth:</label>
                                                    <div class="col-md-9">
                                                        <asp:Label ID="lblDOB" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-6">
                                                <div class="form-group row">
                                                    <label class="control-label text-right col-md-3">Gender:</label>
                                                    <div class="col-md-9">
                                                        <asp:Label ID="lblGender" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            
                                            <div class="col-md-6">
                                                <div class="form-group row">
                                                    <label class="control-label text-right col-md-3">Blood Group:</label>
                                                    <div class="col-md-9">
                                                        <asp:Label ID="lblBlood" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>




                                            <div class="col-md-6">
                                                <div class="form-group row">
                                                    <label class="control-label text-right col-md-3">Cell No:</label>
                                                    <div class="col-md-9">
                                                        <asp:Label ID="lblCell" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            
                                            <div class="col-md-6">
                                                <div class="form-group row">
                                                    <label class="control-label text-right col-md-3">Other Contact:</label>
                                                    <div class="col-md-9">
                                                        <asp:Label ID="lblOther" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>


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
                                                    <label class="control-label text-right col-md-3">NIC #:</label>
                                                    <div class="col-md-9">
                                                        <asp:Label ID="lblNIC" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="col-md-6">
                                                <div class="form-group row">
                                                    <label class="control-label text-right col-md-3">Identity Mark:</label>
                                                    <div class="col-md-9">
                                                        <asp:Label ID="lblIdentity" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            
                                            <div class="col-md-6">
                                                <div class="form-group row">
                                                    <label class="control-label text-right col-md-3">NIC issue date:</label>
                                                    <div class="col-md-9">
                                                        <asp:Label ID="lblNICissue" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="col-md-6">
                                                <div class="form-group row">
                                                    <label class="control-label text-right col-md-3">NIC Expiry Date:</label>
                                                    <div class="col-md-9">
                                                        <asp:Label ID="lblNICExpiry" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            
                                            <div class="col-md-6">
                                                <div class="form-group row">
                                                    <label class="control-label text-right col-md-3">License No:</label>
                                                    <div class="col-md-9">
                                                        <asp:Label ID="lblLicenseNo" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="col-md-6">
                                                <div class="form-group row">
                                                    <label class="control-label text-right col-md-3">License Category:</label>
                                                    <div class="col-md-9">
                                                        <asp:Label ID="lblLicenseCat" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            
                                            <div class="col-md-6">
                                                <div class="form-group row">
                                                    <label class="control-label text-right col-md-3">License Issue Date:</label>
                                                    <div class="col-md-9">
                                                        <asp:Label ID="lblLicenseIssue" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="col-md-6">
                                                <div class="form-group row">
                                                    <label class="control-label text-right col-md-3">License Epiry Date:</label>
                                                    <div class="col-md-9">
                                                        <asp:Label ID="lblLicensexpiryDate" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            
                                            <div class="col-md-6">
                                                <div class="form-group row">
                                                    <label class="control-label text-right col-md-3">Issuing Authority:</label>
                                                    <div class="col-md-9">
                                                        <asp:Label ID="lblIssueAutho" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            
                                            <div class="col-md-6">
                                                <div class="form-group row">
                                                    <label class="control-label text-right col-md-3">License Type:</label>
                                                    <div class="col-md-9">
                                                        <asp:Label ID="lbllicenceStatus" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="col-md-6">
                                                <div class="form-group row">
                                                    <label class="control-label text-right col-md-3">Emergency Contact Name:</label>
                                                    <div class="col-md-9">
                                                        <asp:Label ID="lblEmerConatctName" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            
                                            <div class="col-md-6">
                                                <div class="form-group row">
                                                    <label class="control-label text-right col-md-3">Emergency Contact No</label>
                                                    <div class="col-md-9">
                                                        <asp:Label ID="lblEmerContactNo" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="col-md-6">
                                                <div class="form-group row">
                                                    <label class="control-label text-right col-md-3">Emergency Contact Relation:</label>
                                                    <div class="col-md-9">
                                                        <asp:Label ID="lblEmerContactRelation" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            
                                            <div class="col-md-6">
                                                <div class="form-group row">
                                                    <label class="control-label text-right col-md-3">Address</label>
                                                    <div class="col-md-9">
                                                        <asp:Label ID="lblAddress" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="col-md-6">
                                                <div class="form-group row">
                                                    <label class="control-label text-right col-md-3">Description:</label>
                                                    <div class="col-md-9">
                                                        <asp:Label ID="lblDescription" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>  
                                  <div class="col-md-4">
                                        <asp:GridView ID="gvImages" runat="server" EmptyDataText="No image found" OnRowDataBound="gvImages_RowDataBound" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:BoundField DataField="Name" HeaderText="Name" />
                                                <asp:TemplateField HeaderText="Image">
                                                    <ItemTemplate>
                                                        <asp:Image ID="Image1" runat="server" Width="60%" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>                                            
                                        </asp:GridView>
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

                                        <%--<div role="form">--%>
                                          <div class="row">
                                       <div class="form-group col-md-3 pull-left">
                                            <label>Code</label>
                                            <asp:TextBox ID="txtCode" runat="server" CssClass="form-control"></asp:TextBox><asp:HiddenField ID="hfEditID" runat="server" />
                                        </div>
                                        <div class="form-group col-md-3 pull-left">
                                            <label>Name</label>
                                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-3 pull-left">
                                            <label>Father</label>
                                            <asp:TextBox ID="txtFatherName" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-md-3 pull-left">
                                            <label>Type</label>
                                            <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="Driver">Driver</asp:ListItem>
                                                <asp:ListItem Value="Helper">Helper</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-md-3 pull-left">
                                            <label>Date of Birth</label>
                                            <asp:TextBox ID="txtDateOfBirth" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                                            <asp:HiddenField ID="hfdob" runat="server" />

                                        </div>
                                        <div class="form-group col-md-3 pull-left">
                                            <label>Gender</label>
                                            <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="Male">Male</asp:ListItem>
                                                <asp:ListItem Value="Female">Female</asp:ListItem>
                                                <asp:ListItem Value="Other">Other</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-md-3 pull-left">
                                            <label>Blood Group</label>
                                            <asp:TextBox ID="txtBloodGroup" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>


                                        <div class="form-group col-md-3 pull-left">
                                            <label>Cell No</label>
                                            <asp:TextBox ID="txtCellNo" runat="server" onkeydown="return (!(event.keyCode>=65) && event.keyCode!=32);" CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-md-3 pull-left">
                                            <label>Other Contact</label>
                                            <asp:TextBox ID="txtOtherContact" onkeydown="return (!(event.keyCode>=65) && event.keyCode!=32);" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-md-3 pull-left">
                                            <label>Home no</label>
                                            <asp:TextBox ID="txtHomeNo" runat="server" onkeydown="return (!(event.keyCode>=65) && event.keyCode!=32);" CssClass="form-control"></asp:TextBox>
                                        </div>




                                        <div class="form-group col-md-3 pull-left">
                                            <label>NIC #</label>
                                            <asp:TextBox ID="txtNICNo" runat="server" onkeydown="return (!(event.keyCode>=65) && event.keyCode!=32);" CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-md-3 pull-left">
                                            <label>Identity Mark</label>
                                            <asp:TextBox ID="txtIDMark" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>


                                        <div class="form-group col-md-3 pull-left">
                                            <label>NIC Issue Date</label>
                                            <asp:TextBox ID="txtNICIssueDate" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                                            <asp:HiddenField ID="hfNICissue" runat="server" />
                                        </div>


                                        <div class="form-group col-md-3 pull-left">
                                            <label>NIC Expiry Date</label>
                                            <asp:TextBox ID="txtNICExpiryDate" TextMode="Date" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:HiddenField ID="hfNICexpiry" runat="server" />
                                        </div>


                                        <div class="form-group col-md-3 pull-left">
                                            <label>License no</label>
                                            <asp:TextBox ID="txtLicenseNo" onkeydown="return (!(event.keyCode>=65) && event.keyCode!=32);" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>


                                        <div class="form-group col-md-3 pull-left">
                                            <label>License Category</label>
                                            <asp:DropDownList ID="ddlLicenseCategory" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="TV">LTV</asp:ListItem>
                                                <asp:ListItem Value="HTV">HTV</asp:ListItem>
                                                <asp:ListItem Value="Other">Other</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>


                                        <div class="form-group col-md-3 pull-left">
                                            <label>Lisense Issue Date</label>
                                            <asp:TextBox ID="txtLicenseIssueDate" TextMode="Date" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:HiddenField ID="hfLicenseIssue" runat="server" />
                                        </div>


                                        <div class="form-group col-md-3 pull-left">
                                            <label>Lisense Expiry Date</label>
                                            <asp:TextBox ID="txtLicenseExpiryDate" TextMode="Date" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:HiddenField ID="hfLicenseExpiry" runat="server" />
                                        </div>
                                        <div class="form-group col-md-3 pull-left">
                                            <label>Issuing Authority</label>
                                            <asp:TextBox ID="txtIssuingAuthority" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-md-3 pull-left">
                                            <label>License Type</label>
                                            <asp:DropDownList ID="ddlLicenseType" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="Gold">Gold</asp:ListItem>
                                                <asp:ListItem Value="Silver">Silver</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-md-3 pull-left">
                                            <label>Emergency Contact Name</label>
                                            <asp:TextBox ID="txtEmerContactName" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-3 pull-left">
                                            <label>Emergency Contact No</label>
                                            <asp:TextBox ID="txtEmerContactNo" runat="server" onkeydown="return (!(event.keyCode>=65) && event.keyCode!=32);" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-3 pull-left">
                                            <label>Emergency Contact Relation</label>
                                            <asp:TextBox ID="txtEmerContactRelation" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-md-3 pull-left">
                                            <label>Select Image</label>
                                            
                                            <asp:FileUpload ID="fuDriverImage" runat="server" CssClass="form-control"  />

                                           <%-- <asp:HiddenField ID="hfDriverImageName" runat="server" />
                                            <asp:HiddenField ID="hfDriverImageContentType" runat="server" />
                                            <asp:HiddenField ID="hfDriverImageBytes" runat="server" />--%>

                                        </div>
                                        <div class="form-group col-md-3 pull-left">
                                            <label>Select Document</label>
                                            <asp:FileUpload ID="fuDocument" runat="server" CssClass="form-control"  />
                                        </div>


                                        
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-md-6 pull-left">
                                            <label>Address</label>
                                            <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Width="100%" Rows="3" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-6 pull-left">
                                            <label>Description</label>
                                            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Width="100%" Rows="3" CssClass="form-control"></asp:TextBox>
                                        </div>




                                            <div class="form-group col-lg-12 pull-left">
                                                <%--<div class="col-md- pull-left demo-checkbox">
                                                    <asp:CheckBox ID="cbActive" runat="server" />
                                                    <label for="cbActive">Active</label>
                                                </div>--%>
                                        
                                                <asp:LinkButton ID="lnkSubmit" runat="server" CssClass="btn btn-success pull-right m-l-10" OnClick="lnkSubmit_Click"><i class="fa fa-save"></i> | Save</asp:LinkButton>
                                        
                                                <asp:LinkButton ID="lnkDelete" runat="server" CssClass="btn btn-danger pull-right" OnClick="lnkDelete_Click"><i class="fa fa-trash"></i> | Delete</asp:LinkButton>
                                        
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
                                <h2 class="title pull-left">Driver</h2> 
                                <div class="col-md-6 m-t-25 pull-right">
                                    <asp:LinkButton ID="lnkCancelSearch" runat="server" OnClick="lnkCancelSearch_Click"><i class="fa fa-times-circle m-t-10 m-r-5 pull-left" style="color: maroon;"></i></asp:LinkButton>
                            
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="pull-left m-r-10 pull-left"></asp:TextBox>
                                    <asp:LinkButton ID="lnkSearch" runat="server" OnClick="lnkSearch_Click" CssClass="pull-left"><i class="fa fa-search"></i></asp:LinkButton>
                                </div>
                                <div class="actions panel_actions pull-right">
                           
                                    <asp:LinkButton ID="lnkAddNew" runat="server" CssClass="box_setting fas fa-plus" OnClick="lnkAddNew_Click" ToolTip="Click to Add New"></asp:LinkButton>
                                    <%--<a class="box_setting fa fa-cog" data-toggle="modal" href="#section-settings"></a>
                                    <a class="box_close fa fa-times"></a>--%>
                                </div>
                            </header>
                            <div class="content-body">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <asp:GridView ID="gvResult" runat="server" Width="100%" EmptyDataText="No Record found" AutoGenerateColumns="false"
                                            CssClass="table table-hover" Font-Size="12px" Font-Names="Open Sans" DataKeyNames="ID, Active" BackColor="White"
                                            OnRowCommand="gvResult_RowCommand" OnRowDataBound="gvResult_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.no">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Code" HeaderText="Code"></asp:BoundField>
                                                <asp:BoundField DataField="Name" HeaderText="Name"></asp:BoundField>
                                                <asp:BoundField DataField="FatherName" HeaderText="FaterName"></asp:BoundField>
                                                <asp:BoundField DataField="Type" HeaderText="Type"></asp:BoundField>
                                                <asp:BoundField DataField="DateOfBirth" HeaderText="DateOfBirth"></asp:BoundField>
                                                <asp:TemplateField HeaderText="Image">
                                                    <ItemTemplate>
                                                        <asp:Image ID="Image1" runat="server" style="width: 6%;" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:ButtonField ButtonType="Link" Text="Download" CommandName="DocDownload" DataTextField="Document" HeaderText="Files" />
                                                
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
                <%--</ContentTemplate>
            </asp:UpdatePanel>--%>
            <!-- MAIN CONTENT AREA ENDS -->
        </section>
    </section>
    <!-- END CONTENT -->
</asp:Content>
