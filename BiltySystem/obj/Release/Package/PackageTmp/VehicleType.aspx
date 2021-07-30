<%@ Page Title="" Language="C#" MasterPageFile="~/BiltySystem.Master" AutoEventWireup="true" CodeBehind="VehicleType.aspx.cs" Inherits="BiltySystem.VehicleType" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Vehicle Type</title>
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
                                                <label class="control-label text-right col-md-3">Unit Type:</label>
                                                <div class="col-md-9">
                                                    <asp:Label ID="lblUnit" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>



                                    </div>
                                    <!--row-->                                 

                                    <h2>Lower Deck</h2>
                                      <div class="row">


                                        <div class="col-md-6">
                                            <div class="form-group row">
                                                <label class="control-label text-right col-md-3">Inner Length:</label>
                                                <div class="col-md-9">
                                                    <asp:Label ID="lblLowerDeckInnerLength" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>




                                        <div class="col-md-6">
                                            <div class="form-group row">
                                                <label class="control-label text-right col-md-3">Inner Height:</label>
                                                <div class="col-md-9">
                                                    <asp:Label ID="lblLowerDeckInnerHeight" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>




                                    </div>
                                    <!--/row-->


                                     <div class="row">


                                        <div class="col-md-6">
                                            <div class="form-group row">
                                                <label class="control-label text-right col-md-3"> Inner Width:</label>
                                                <div class="col-md-9">
                                                    <asp:Label ID="lblLowerDeckInnerWidth" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>




                                        <div class="col-md-6">
                                            <div class="form-group row">
                                                <label class="control-label text-right col-md-3">Outer Length:</label>
                                                <div class="col-md-9">
                                                    <asp:Label ID="lblLowerDeckOuterLength" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>




                                    </div>
                                    <!--/row-->


                                     <div class="row">


                                        <div class="col-md-6">
                                            <div class="form-group row">
                                                <label class="control-label text-right col-md-3">Outer Width:</label>
                                                <div class="col-md-9">
                                                    <asp:Label ID="lblLowerDeckOuterWidth" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>




                                        <div class="col-md-6">
                                            <div class="form-group row">
                                                <label class="control-label text-right col-md-3">Outer Height:</label>
                                                <div class="col-md-9">
                                                    <asp:Label ID="lblLowerDeckOuterHeight" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>




                                    </div>
                                    <!--/row-->

                                    <h2>Upper Deck</h2>
                                     <div class="row">


                                        <div class="col-md-6">
                                            <div class="form-group row">
                                                <label class="control-label text-right col-md-3">Inner Length:</label>
                                                <div class="col-md-9">
                                                    <asp:Label ID="lblUpperDeckInnerLength" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>




                                        <div class="col-md-6">
                                            <div class="form-group row">
                                                <label class="control-label text-right col-md-3">Inner Width:</label>
                                                <div class="col-md-9">
                                                    <asp:Label ID="lblUpperDeckInnerWidth" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>




                                    </div>
                                    <!--/row-->

                                     <div class="row">


                                        <div class="col-md-6">
                                            <div class="form-group row">
                                                <label class="control-label text-right col-md-3">Inner Height:</label>
                                                <div class="col-md-9">
                                                    <asp:Label ID="lblUpperDeckInnerHeight" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>




                                        <div class="col-md-6">
                                            <div class="form-group row">
                                                <label class="control-label text-right col-md-3">Outer Length:</label>
                                                <div class="col-md-9">
                                                    <asp:Label ID="lblUpperDeckOuterLength" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>




                                    </div>
                                    <!--/row-->

                                     <div class="row">


                                        <div class="col-md-6">
                                            <div class="form-group row">
                                                <label class="control-label text-right col-md-3">Outer Width:</label>
                                                <div class="col-md-9">
                                                    <asp:Label ID="lblUpperDeckOuterWidth" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>




                                        <div class="col-md-6">
                                            <div class="form-group row">
                                                <label class="control-label text-right col-md-3">Outer Height:</label>
                                                <div class="col-md-9">
                                                    <asp:Label ID="lblUpperDeckOuterHeight" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>




                                    </div>
                                    <!--/row-->
                                    <h2>Upper Portion</h2>
                                     <div class="row">


                                        <div class="col-md-6">
                                            <div class="form-group row">
                                                <label class="control-label text-right col-md-3">Inner Length:</label>
                                                <div class="col-md-9">
                                                    <asp:Label ID="lblUpperPortionInnerLength" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>




                                        <div class="col-md-6">
                                            <div class="form-group row">
                                                <label class="control-label text-right col-md-3">Inner width:</label>
                                                <div class="col-md-9">
                                                    <asp:Label ID="lblUpperPortionInnerwidth" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>




                                    </div>
                                    <!--/row-->

                                     <div class="row">


                                        <div class="col-md-6">
                                            <div class="form-group row">
                                                <label class="control-label text-right col-md-3">Inner Height:</label>
                                                <div class="col-md-9">
                                                    <asp:Label ID="lblUpperPortionInnerHeight" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>




                                     




                                    </div>
                                    <!--/row-->

                                    <h2>Lower Portion</h2>
                                     <div class="row">


                                       




                                        <div class="col-md-6">
                                            <div class="form-group row">
                                                <label class="control-label text-right col-md-3">Inner Width:</label>
                                                <div class="col-md-9">
                                                    <asp:Label ID="lblLowerPortionInnerWidth" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>




                                    </div>
                                    <!--/row-->

                                     <div class="row">


                                        <div class="col-md-6">
                                            <div class="form-group row">
                                                <label class="control-label text-right col-md-3">Inner Length:</label>
                                                <div class="col-md-9">
                                                    <asp:Label ID="lblLowerPortionInnerLength" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>




                                        <div class="col-md-6">
                                            <div class="form-group row">
                                                <label class="control-label text-right col-md-3">Inner Height:</label>
                                                <div class="col-md-9">
                                                    <asp:Label ID="lblLowerPortionInnerHeight" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>




                                    </div>
                                    <!--/row-->
                                    <hr />
                                     <div class="row">


                                        <div class="col-md-6">
                                            <div class="form-group row">
                                                <label class="control-label text-right col-md-3">Permisible Height:</label>
                                                <div class="col-md-9">
                                                    <asp:Label ID="lblPermisibleHeight" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>




                                        <div class="col-md-6">
                                            <div class="form-group row">
                                                <label class="control-label text-right col-md-3">Permisible Length:</label>
                                                <div class="col-md-9">
                                                    <asp:Label ID="lblPermisibleLength" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>




                                    </div>
                                    <!--/row-->


                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group row">
                                                <label class="control-label text-right col-md-3">Discription:</label>
                                                <div class="col-md-9">
                                                    <asp:Label ID="lblDescription" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!--/row-->
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
                                            <label>Unit Type</label>
                                          <asp:DropDownList ID="ddlUnitType" runat="server" CssClass="form-control">
                                              <asp:ListItem Value="Meter">Meter</asp:ListItem>
                                              <asp:ListItem Value="Feet">Feet</asp:ListItem>
                                              <asp:ListItem Value="Inch">Inch</asp:ListItem>
                                                                                      </asp:DropDownList>
                                        </div>         
                                        
                                        <div class="form-group col-md-3 pull-left">
                                            <label>LowerDeck Inner Length</label>
                                            <asp:TextBox ID="txtLowerDeckInnerLength" runat="server" onkeydown="return (!(event.keyCode>=65) && event.keyCode!=32);" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-3 pull-left">
                                            <label>LowerDeck Inner Width</label>
                                            <asp:TextBox ID="txtLowerDeckInnerWidth" runat="server" onkeydown="return (!(event.keyCode>=65) && event.keyCode!=32);" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-3 pull-left">
                                            <label>LowerDeck Inner Height</label>
                                            <asp:TextBox ID="txtLowerDeckInnerHeight" runat="server" onkeydown="return (!(event.keyCode>=65) && event.keyCode!=32);" CssClass="form-control"></asp:TextBox>
                                        </div>
                                         <div class="form-group col-md-3 pull-left">
                                            <label>LowerDeck Outer Length</label>
                                            <asp:TextBox ID="txtLowerDeckOuterLength" runat="server" onkeydown="return (!(event.keyCode>=65) && event.keyCode!=32);" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-3 pull-left">
                                            <label>LowerDeck Outer Width</label>
                                            <asp:TextBox ID="txtLowerDeckOuterWidth" runat="server" onkeydown="return (!(event.keyCode>=65) && event.keyCode!=32);" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-3 pull-left">
                                            <label>LowerDeck Outer Height</label>
                                            <asp:TextBox ID="txtLowerDeckOuterHeight" runat="server" onkeydown="return (!(event.keyCode>=65) && event.keyCode!=32);" CssClass="form-control"></asp:TextBox>
                                        </div>











                                              <div class="form-group col-md-3 pull-left">
                                            <label>UpperDeck Inner Length</label>
                                            <asp:TextBox ID="txtUpperDeckInnerLength" runat="server" onkeydown="return (!(event.keyCode>=65) && event.keyCode!=32);" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-3 pull-left">
                                            <label>UpperDeck Inner Width</label>
                                            <asp:TextBox ID="txtUpperDeckInnerWidth" runat="server" onkeydown="return (!(event.keyCode>=65) && event.keyCode!=32);" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-3 pull-left">
                                            <label>UpperDeck Inner Height</label>
                                            <asp:TextBox ID="txtUpperDeckInnerHeight" runat="server" onkeydown="return (!(event.keyCode>=65) && event.keyCode!=32);" CssClass="form-control"></asp:TextBox>
                                        </div>
                                         <div class="form-group col-md-3 pull-left">
                                            <label>UpperDeck Outer Length</label>
                                            <asp:TextBox ID="txtUpperDeckOuterLength" runat="server" onkeydown="return (!(event.keyCode>=65) && event.keyCode!=32);" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-3 pull-left">
                                            <label>UpperDeck Outer Width</label>
                                            <asp:TextBox ID="txtUpperDeckOuterWidth" runat="server" onkeydown="return (!(event.keyCode>=65) && event.keyCode!=32);" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-3 pull-left">
                                            <label>UpperDeck Outer Height</label>
                                            <asp:TextBox ID="txtUpperDeckOuterHeight" runat="server" onkeydown="return (!(event.keyCode>=65) && event.keyCode!=32);" CssClass="form-control"></asp:TextBox>
                                        </div>





                                           <div class="form-group col-md-3 pull-left">
                                            <label>UpperPortion Inner Length</label>
                                            <asp:TextBox ID="txtUpperPortionInnerLength" runat="server" onkeydown="return (!(event.keyCode>=65) && event.keyCode!=32);" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-3 pull-left">
                                            <label>UpperPortion Inner width</label>
                                            <asp:TextBox ID="txtUpperPortionInnerwidth" runat="server" onkeydown="return (!(event.keyCode>=65) && event.keyCode!=32);" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-3 pull-left">
                                            <label>UpperPortion Inner Height</label>
                                            <asp:TextBox ID="txtUpperPortionInnerHeight" runat="server" onkeydown="return (!(event.keyCode>=65) && event.keyCode!=32);" CssClass="form-control"></asp:TextBox>
                                        </div>
                                         <div class="form-group col-md-3 pull-left">
                                            <label>LowerPortion Inner Width</label>
                                            <asp:TextBox ID="txtLowerPortionInnerWidth" runat="server" onkeydown="return (!(event.keyCode>=65) && event.keyCode!=32);" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-3 pull-left">
                                            <label>LowerPortion Inner Length</label>
                                            <asp:TextBox ID="txtLowerPortionInnerLength" runat="server" onkeydown="return (!(event.keyCode>=65) && event.keyCode!=32);" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-3 pull-left">
                                            <label>LowerPortion Inner Height</label>
                                            <asp:TextBox ID="txtLowerPortionInnerHeight" runat="server" onkeydown="return (!(event.keyCode>=65) && event.keyCode!=32);" CssClass="form-control"></asp:TextBox>
                                        </div>


                                             <div class="form-group col-md-3 pull-left">
                                            <label>Permisible Height</label>
                                            <asp:TextBox ID="txtPermisibleHeight" runat="server" onkeydown="return (!(event.keyCode>=65) && event.keyCode!=32);" CssClass="form-control"></asp:TextBox>
                                        </div>
                                         <div class="form-group col-md-3 pull-left">
                                            <label>Permisible Length</label>
                                            <asp:TextBox ID="txtPermisibleLength" runat="server" onkeydown="return (!(event.keyCode>=65) && event.keyCode!=32);" CssClass="form-control"></asp:TextBox>
                                        </div>
                   


                                       
                                    </div>
                                    <div class="row">

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
                                <h2 class="title pull-left">Vehicle Type</h2> 
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
                                            CssClass="table table-hover" Font-Size="12px" Font-Names="Open Sans" DataKeyNames="VehicleTypeID, isActive" BackColor="White" OnRowCommand="gvResult_RowCommand" OnRowDataBound="gvResult_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.no">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               <asp:BoundField DataField="VehicleTypeCode" HeaderText="Code"></asp:BoundField>
                                        <asp:BoundField DataField="VehicleTypeName" HeaderText="Name"></asp:BoundField>
                                        <asp:BoundField DataField="DimensionUnitType" HeaderText="Unit Type"></asp:BoundField>
                                        <asp:BoundField DataField="LowerDeckInnerLength" HeaderText="Length"></asp:BoundField>
                                        <asp:BoundField DataField="LowerDeckInnerWidth" HeaderText="Width"></asp:BoundField>
                                        <asp:BoundField DataField="LowerDeckInnerHeight" HeaderText="Height"></asp:BoundField> 
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

