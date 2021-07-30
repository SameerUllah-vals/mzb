<%@ Page Title="" Language="C#" MasterPageFile="~/BiltySystem.Master" AutoEventWireup="true" CodeBehind="Vehicle.aspx.cs" Inherits="BiltySystem.Vehicle" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Vehicle</title>
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
                                                                  <asp:Label ID="lblCodemodal" runat="server"  ></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!--/span-->
                                                        <div class="col-md-6">
                                                            <div class="form-group row">
                                                                <label class="control-label text-right col-md-3">Registration No:</label>
                                                                <div class="col-md-9">
                                                                       <asp:Label ID="lblregno" runat="server" ></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!--/span-->
                                                    </div>
                                                    <!--/row-->
                                                    <div class="row">


                                                            <div class="col-md-6">
                                                            <div class="form-group row">
                                                                <label class="control-label text-right col-md-3">Eng No:</label>
                                                                <div class="col-md-9">
                                                                      <asp:Label ID="lblEnginNo" runat="server" ></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="form-group row">
                                                                <label class="control-label text-right col-md-3">Chasis No:</label>
                                                                <div class="col-md-9">
                                                                      <asp:Label ID="lblChasisNo" runat="server" ></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>                                                
                                                    </div>
                                                    <!--row-->


                                                      <div class="row">


                                                            <div class="col-md-6">
                                                            <div class="form-group row">
                                                                <label class="control-label text-right col-md-3">Make:</label>
                                                                <div class="col-md-9">
                                                                      <asp:Label ID="lblMake" runat="server" ></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>




                                                        <div class="col-md-6">
                                                            <div class="form-group row">
                                                                <label class="control-label text-right col-md-3">Model:</label>
                                                                <div class="col-md-9">
                                                                      <asp:Label ID="lblModel" runat="server" ></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>


                                                      
                                                     
                                                    </div>
                                                    <!--/row-->



                                                           <div class="row">


                                                            <div class="col-md-6">
                                                            <div class="form-group row">
                                                                <label class="control-label text-right col-md-3">Manufacturer:</label>
                                                                <div class="col-md-9">
                                                                      <asp:Label ID="lblManu"  runat="server" ></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>



                                                                  <div class="col-md-6">
                                                            <div class="form-group row">
                                                                <label class="control-label text-right col-md-3">Color:</label>
                                                                <div class="col-md-9">
                                                                      <asp:Label ID="lblcolor" runat="server" ></asp:Label>
                                                                </div>
                                                            </div>
                                                                      </div>
                                                                                 
                                                     
                                                    </div>
                                                    <!--/row-->


                                                           <div class="row">


                                                            <div class="col-md-6">
                                                            <div class="form-group row">
                                                                <label class="control-label text-right col-md-3">Vehicle TYpe:</label>
                                                                <div class="col-md-9">
                                                                      <asp:Label ID="lblVehicle" runat="server" ></asp:Label>
                                                                </div>
                                                            </div>

                                                        </div>


                                                                   <div class="col-md-6">
                                                            <div class="form-group row">
                                                                <label class="control-label text-right col-md-3">Maximum Loading Capacity:</label>
                                                                <div class="col-md-9">
                                                                      <asp:Label ID="lblMLC" runat="server" ></asp:Label>
                                                                </div>
                                                            </div>

                                                        </div>                                                   
                                                     
                                                    </div>
                                                    <!--/row-->

                                                        <div class="row">


                                                            <div class="col-md-6">
                                                            <div class="form-group row">
                                                                <label class="control-label text-right col-md-3">Loading Limit NHA:</label>
                                                                <div class="col-md-9">
                                                                      <asp:Label ID="lblLoadLimit" runat="server" ></asp:Label>
                                                                </div>
                                                            </div>

                                                        </div>


                                                                   <div class="col-md-6">
                                                            <div class="form-group row">
                                                                <label class="control-label text-right col-md-3">Purchase Date:</label>
                                                                <div class="col-md-9">
                                                                      <asp:Label ID="lblPurchaseDate" runat="server" ></asp:Label>
                                                                </div>
                                                            </div>

                                                        </div>                                                     
                                                     
                                                    </div>
                                                    <!--/row-->

                                                        <div class="row">


                                                            <div class="col-md-6">
                                                            <div class="form-group row">
                                                                <label class="control-label text-right col-md-3">Purchase Amount:</label>
                                                                <div class="col-md-9">
                                                                      <asp:Label ID="lblPamount" runat="server" ></asp:Label>
                                                                </div>
                                                            </div>
                                                                </div>                                                                                                                                                                       
                                                                   <div class="col-md-6">
                                                            <div class="form-group row">
                                                                <label class="control-label text-right col-md-3">Purchase From:</label>
                                                                <div class="col-md-9">
                                                                      <asp:Label ID="lblPfrom" runat="server" ></asp:Label>
                                                                </div>
                                                            </div>

                                                        </div>
                                                                                                          
                                                    </div>
                                                    <!--/row-->

                                                        <div class="row">


                                                            <div class="col-md-6">
                                                            <div class="form-group row">
                                                                <label class="control-label text-right col-md-3">Purchase Detail:</label>
                                                                <div class="col-md-9">
                                                                      <asp:Label ID="lblPdetail" runat="server" ></asp:Label>
                                                                </div>
                                                            </div>

                                                        </div>


                                                                   <div class="col-md-6">
                                                            <div class="form-group row">
                                                                <label class="control-label text-right col-md-3">Owner Name:</label>
                                                                <div class="col-md-9">
                                                                      <asp:Label ID="lblOname" runat="server" ></asp:Label>
                                                                </div>
                                                            </div>

                                                        </div>              
                                                    </div>
                                                    <!--/row-->

                                                        <div class="row">


                                                            <div class="col-md-6">
                                                            <div class="form-group row">
                                                                <label class="control-label text-right col-md-3">Owner Contact:</label>
                                                                <div class="col-md-9">
                                                                      <asp:Label ID="lblOcontact" runat="server" ></asp:Label>
                                                                </div>
                                                            </div>

                                                        </div>
                                                            

                                                            
                                                                   <div class="col-md-6">
                                                            <div class="form-group row">
                                                                <label class="control-label text-right col-md-3">Owner NIC:</label>
                                                                <div class="col-md-9">
                                                                      <asp:Label ID="lblONIC" runat="server" ></asp:Label>
                                                                </div>
                                                            </div>

                                                        </div>
                                                        </div>                                                                                                         
                                                    </div>
                                                    <!--/row-->

                                                        <div class="row">


                                                                    <div class="col-md-6">
                                                            <div class="form-group row">
                                                                <label class="control-label text-right col-md-3">OwnerShip Status:</label>
                                                                <div class="col-md-9">
                                                                      <asp:Label ID="lblOstatus" runat="server" ></asp:Label>
                                                                </div>
                                                            </div>

                                                        </div>
                                                                   <div class="col-md-6">
                                                            <div class="form-group row">
                                                                <label class="control-label text-right col-md-3">Description:</label>
                                                                <div class="col-md-9">
                                                                      <asp:Label ID="lblDescription" runat="server" ></asp:Label>
                                                                </div>
                                                            </div>

                                                        </div>                                                 
                                                    </div>
                                                    <!--/row-->

                                                        <div class="row">


                                                                 <div class="col-md-6">
                                                            <div class="form-group row">
                                                                <label class="control-label text-right col-md-3">Body Type:</label>
                                                                <div class="col-md-9">
                                                                      <asp:Label ID="lblBody" runat="server" ></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>

                                                                       <div class="col-md-6">
                                                            <div class="form-group row">
                                                                <label class="control-label text-right col-md-3">Length:</label>
                                                                <div class="col-md-9">
                                                                      <asp:Label ID="lblLength" runat="server" ></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>                                                     
                                                    </div>
                                                    <!--/row-->

                                                         <div class="row">
                                                                 <div class="col-md-6">
                                                            <div class="form-group row">
                                                                <label class="control-label text-right col-md-3">Width:</label>
                                                                <div class="col-md-9">
                                                                      <asp:Label ID="lblWidth" runat="server" ></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>

                                                                       <div class="col-md-6">
                                                            <div class="form-group row">
                                                                <label class="control-label text-right col-md-3">Height:</label>
                                                                <div class="col-md-9">
                                                                      <asp:Label ID="lblHeight" runat="server" ></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                   </div>
                                                    <!--/row-->
                                                        <div class="row">
                                                                 <div class="col-md-6">
                                                            <div class="form-group row">
                                                                <label class="control-label text-right col-md-3">Dimension Unit:</label>
                                                                <div class="col-md-9">
                                                                      <asp:Label ID="lblDimenison" runat="server" ></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>

                                                                       <div class="col-md-6">
                                                            <div class="form-group row">
                                                                <label class="control-label text-right col-md-3">Type:</label>
                                                                <div class="col-md-9">
                                                                      <asp:Label ID="lblType" runat="server" ></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>   
                                                    </div>
                                                    <!--/row-->
                                                         <div class="row">
                                                                 <div class="col-md-6">
                                                            <div class="form-group row">
                                                                <label class="control-label text-right col-md-3">Manufacture:</label>
                                                                <div class="col-md-9">
                                                                      <asp:Label ID="lblManufacture" runat="server" ></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>                                                                                                        
                                                    </div>
                                                    <!--/row-->

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
                                                    <label>Registration No</label>
                                                    <asp:TextBox ID="txtReg" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-md-3 pull-left">
                                                    <label>Engine No</label>
                                                    <asp:TextBox ID="txtEngin" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-md-3 pull-left">
                                                    <label>Chassis No</label>
                                                    <asp:TextBox ID="txtChasses" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-md-3 pull-left">
                                                    <label>Make</label>
                                                    <asp:TextBox ID="txtmake" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-md-3 pull-left">
                                                    <label>Model</label>
                                                    <asp:TextBox ID="txtmodel" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                 <div class="form-group col-md-3 pull-left">
                                                    <label>Manufacturer</label>
                                                    <asp:TextBox ID="txtmanu" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                 <div class="form-group col-md-3 pull-left">
                                                    <label>Color</label>
                                                    <asp:TextBox ID="txtcolor" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                 <div class="form-group col-md-3 pull-left">
                                                    <label>Body Type</label>
                                                    <asp:TextBox ID="txtbodytype" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                 <div class="form-group col-md-3 pull-left">
                                                    <label>Vehicle Type</label>
                                                     <asp:DropDownList ID="ddlVehicleType" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                                 <div class="form-group col-md-3 pull-left">
                                                    <label>Maximum Loading Capacity</label>
                                                    <asp:TextBox ID="txtMLC" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                 <div class="form-group col-md-3 pull-left">
                                                    <label>Loading Limit NHA</label>
                                                    <asp:TextBox ID="txtloadingNHA" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                 <div class="form-group col-md-3 pull-left">
                                                    <label>Purchase Date</label>
                                                    <asp:TextBox ID="txtPdate" TextMode="Date" runat="server" CssClass="form-control"></asp:TextBox>
                                                     <asp:HiddenField ID="hfPurchaseDate" runat="server" />
                                                </div>
                                                 <div class="form-group col-md-3 pull-left">
                                                    <label>Purchase Amount</label>
                                                    <asp:TextBox ID="txtPamount" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                   <div class="form-group col-md-3 pull-left">
                                                    <label>Purchase From</label>
                                                    <asp:TextBox ID="txtPfrom" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                     <div class="form-group col-md-3 pull-left">
                                                    <label>Purchase Detail</label>
                                                    <asp:TextBox ID="txtPdetails" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                   <div class="form-group col-md-3 pull-left">
                                                    <label>Owner Name</label>
                                                    <asp:TextBox ID="txtOname" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                   <div class="form-group col-md-3 pull-left">
                                                    <label>Owner Contact</label>
                                                    <asp:TextBox ID="txtOcontact" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                   <div class="form-group col-md-3 pull-left">
                                                    <label>Owner Nic</label>
                                                    <asp:TextBox ID="txtOnic" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                   <div class="form-group col-md-3 pull-left">
                                                    <label>Length</label>
                                                    <asp:TextBox ID="txtLength" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                   <div class="form-group col-md-3 pull-left">
                                                    <label>Width</label>
                                                    <asp:TextBox ID="txtWidth" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                   <div class="form-group col-md-3 pull-left">
                                                    <label>Height</label>
                                                    <asp:TextBox ID="txtHeight" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                   <div class="form-group col-md-3 pull-left">
                                                    <label>DimensionUnitType</label>
                                                    <asp:TextBox ID="txtDimension" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                   <div class="form-group col-md-3 pull-left">
                                                    <label>Type</label>
                                                    <asp:TextBox ID="txtType" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                   <div class="form-group col-md-3 pull-left">
                                                    <label>Manufacturer Year</label>
                                                    <asp:TextBox ID="txtManufacYear" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                   <%--<div class="form-group col-md-3 pull-left">
                                                    <label>isOwnVehicle</label>
                                                    <asp:CheckBox ID="cbIsOwnVehicle" runat="server" />
                                                </div>--%>
                                                 

                                                   <div class="form-group col-md-3 pull-left">
                                                    <label>Ownership Status</label>
                                                       <asp:DropDownList ID="ddlOwnership" runat="server" OnSelectedIndexChanged="ddlOwnership_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control">
                                                          <asp:ListItem Value="Own">Own</asp:ListItem>
                                                            <asp:ListItem Value="Hired">Hired</asp:ListItem>
                                                             </asp:DropDownList>
                                                                        </div>
                                              <div class="form-group col-md-3 pull-left" >
                                                    <label>Broker</label>
                                                       <asp:DropDownList ID="ddlBroker" runat="server"  CssClass="form-control" Enabled="false">
                                                          </asp:DropDownList>
                                                                        </div>

                                                              <div class="form-group col-md-3 pull-left">
                                                    <label>Upload Document</label>
                                                <asp:FileUpload ID="FileUpload1" runat="server" />
                                                                  <asp:HiddenField ID="hfFileUpload" runat="server" />
                                                </div>
                                                    <div class="form-group col-md-6 pull-left">
                                                    <label>Description</label>
                                                    <asp:TextBox ID="txtDes" runat="server" TextMode="MultiLine" Width="100%" Rows="3" CssClass="form-control"></asp:TextBox>
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
                                <h2 class="title pull-left">Vehicle</h2> 
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
                                            CssClass="table table-hover" Font-Size="12px" Font-Names="Open Sans" DataKeyNames="VehicleID, isActive" BackColor="White" OnRowCommand="gvResult_RowCommand" OnRowDataBound="gvResult_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.no">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:BoundField DataField="VehicleCode" HeaderText="Code"></asp:BoundField>
                                                <asp:BoundField DataField="RegNo" HeaderText="RegistrationNo"></asp:BoundField>
                                                <asp:BoundField DataField="EngineNo" HeaderText="EngineNo"></asp:BoundField>
                                                <asp:BoundField DataField="ChasisNo" HeaderText="ChasisNo"></asp:BoundField>
                                                <asp:BoundField DataField="Make" HeaderText="Make"></asp:BoundField>
                                                <asp:BoundField DataField="VehicleModel" HeaderText="Model"></asp:BoundField>
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
                </ContentTemplate>
            </asp:UpdatePanel>
            <!-- MAIN CONTENT AREA ENDS -->
        </section>
    </section>
    <!-- END CONTENT -->
</asp:Content>
