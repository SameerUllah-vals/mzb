<%@ Page Title="" Language="C#" MasterPageFile="~/BiltySystem.Master" AutoEventWireup="true" CodeBehind="Invoice.aspx.cs" Inherits="BiltySystem.Invoice" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">\
    <title>Invoice</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <!-- START CONTENT -->
    <!-- START CONTENT -->
<section id="main-content" class=" ">
    <section class="wrapper main-wrapper row" style=''>

    <div class='col-xs-12'>
        <div class="page-title">

            <div class="pull-left">
                <!-- PAGE HEADING TAG - START --><h1 class="title">Form - Premade Forms</h1><!-- PAGE HEADING TAG - END -->                            </div>

                            <div class="pull-right hidden-xs">
                    <ol class="breadcrumb">
                        <li>
                            <a href="index.html"><i class="fa fa-home"></i>Home</a>
                        </li>
                        <li>
                            <a href="form-elements.html">Form Elements</a>
                        </li>
                        <li class="active">
                            <strong>Pre Made Forms</strong>
                        </li>
                    </ol>
                </div>
                                
        </div>
    </div>
    <div class="clearfix"></div>
    <!-- MAIN CONTENT AREA STARTS -->
    


<div class="col-xs-12">
    <section class="box ">
            <header class="panel_header">
                <h2 class="title pull-left">Vertical Form</h2>
                <div class="actions panel_actions pull-right">
                	<a class="box_toggle fa fa-chevron-down"></a>
                    <a class="box_setting fa fa-cog" data-toggle="modal" href="#section-settings"></a>
                    <a class="box_close fa fa-times"></a>
                </div>
            </header>
            <div class="content-body">
    <div class="row">
        <div class="col-md-8 col-sm-9 col-xs-10">

            <form role="form">
                <div class="form-group">
                    <label class="form-label" for="email-1">Email address:</label>
                    <input type="email" class="form-control" id="email-1" placeholder="Enter your email…">
                </div>

                <div class="form-group">
                    <label class="form-label" for="password-1">Password:</label>
                    <input type="password" class="form-control" id="password-1" placeholder="Enter your password">
                </div>

                <div class="form-group">
                    <label class="form-label">
                        <input type="checkbox" class="iCheck" checked> <span>Remember me</span>
                    </label>
                </div>

                <div class="form-group">
                    <button type="button" class="btn btn-primary ">Sign in</button>
                    <button type="button" class="btn btn-purple  pull-right">Register now</button>
                </div>

            </form>

        </div>
    </div>

    </div>
        </section></div>


<div class="col-xs-12">
    <section class="box ">
            <header class="panel_header">
                <h2 class="title pull-left">Modal Forms</h2>
                <div class="actions panel_actions pull-right">
                	<a class="box_toggle fa fa-chevron-down"></a>
                    <a class="box_setting fa fa-cog" data-toggle="modal" href="#section-settings"></a>
                    <a class="box_close fa fa-times"></a>
                </div>
            </header>
            <div class="content-body">
    <div class="row">
        <div class="col-md-8 col-sm-9 col-xs-10">

            <!-- START -->
            <div class="position-center ">
                <div class="">
                    <a href="#myModal" data-toggle="modal" class="btn btn-lg btn-primary">
                        MODAL FORM 1
                    </a>
                    <a href="#myModal-1" data-toggle="modal" class="btn btn-lg btn-primary">
                        MODAL FORM 2
                    </a>
                    <a href="#myModal-2" data-toggle="modal" class="btn btn-lg btn-primary">
                        MODAL FORM 3
                    </a>
                    <br>
                </div>

                <div aria-hidden="true"  role="dialog" tabindex="-1" id="myModal" class="modal fade" style="display: none;">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button aria-hidden="true" data-dismiss="modal" class="close" type="button">×</button>
                                <h4 class="modal-title">Register as User</h4>
                            </div>
                            <div class="modal-body">
                                <form role="form">

                                    <div class="form-group">
                                        <label for="modalname1" class="form-label">Full Name</label>
                                        <input type="text" class="form-control" id="modalname1" placeholder="Enter name">
                                    </div>
                                    <div class="form-group">
                                        <label for="modalemail1" class="form-label">Email address</label>
                                        <input type="email" class="form-control" id="modalemail1" placeholder="Enter email">
                                    </div>
                                    <div class="form-group">
                                        <label for="modalpw1" class="form-label">Password</label>
                                        <input type="password" class="form-control" id="modalpw1" placeholder="Password">
                                    </div>
                                    <div class="form-group">
                                        <label for="modalfile3" class="form-label">File input</label>
                                        <input type="file" id="modalfile3">
                                        <span class="help-block">Example block-level help text here.</span>
                                    </div>
                                    <div class="">
                                        <label>
                                            <input type="checkbox" class="iCheck"> Check me out
                                        </label>
                                    </div>
                                    <button type="submit" class="btn btn-primary">Submit</button>
                                </form>
                            </div>
                        </div>
                    </div>
                </div>
                <div aria-hidden="true"  role="dialog" tabindex="-1" id="myModal-1" class="modal fade" style="display: none;">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button aria-hidden="true" data-dismiss="modal" class="close" type="button">×</button>
                                <h4 class="modal-title">Sign In</h4>
                            </div>
                            <div class="modal-body">

                                <form class="form-horizontal" role="form">



                                    <div class="input-group">
                                        <span class="input-group-addon">
                                            <span class="arrow"></span>
                                            <i class="fa fa-envelope"></i>     
                                        </span>
                                        <input type="text" class="form-control" placeholder="Your Email" id='inputEmail2' value=''>
                                    </div>
                                    <br>
                                    <div class="input-group">
                                        <span class="input-group-addon">
                                            <span class="arrow"></span>
                                            <i class="fa fa-lock"></i>     
                                        </span>
                                        <input type="password" class="form-control" placeholder="Your Password" id='inputpw2' value=''>
                                    </div>

                                    <br>
                                    <div class="form-group">
                                        <label class="form-label">
                                            <input type="checkbox" checked="" class="iCheck"> <span>Remember me</span>
                                        </label>
                                    </div>                                            
                                    <br>
                                    <div class="form-group">
                                        <div class="">
                                            <button type="submit" class="btn btn-primary">Sign in</button>
                                        </div>
                                    </div>
                                </form>

                            </div>

                        </div>
                    </div>
                </div>
                <div aria-hidden="true"  role="dialog" tabindex="-1" id="myModal-2" class="modal fade" style="display: none;">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button aria-hidden="true" data-dismiss="modal" class="close" type="button">×</button>
                                <h4 class="modal-title">Inline Form In modal</h4>
                            </div>
                            <div class="modal-body">


                                <form role="form" class="form-inline">

                                    <div class="form-group">
                                        <input type="text" class="form-control" placeholder="Username">
                                    </div>

                                    <div class="form-group">
                                        <input type="password" class="form-control" placeholder="Password">
                                    </div>


                                    <div class="form-group pull-right">
                                        <button type="button" class="btn btn-primary ">Sign in</button>
                                    </div>


                                    <div class="form-group">
                                        <label class="cbr-inline form-label">
                                            <input type="checkbox"  class="iCheck" checked> <span>Remember</span>
                                        </label>
                                    </div>

                                </form>

                            </div>

                        </div>
                    </div>
                </div>

            </div>
            <!-- END -->




        </div>
    </div>

    </div>
        </section></div>


<div class="col-xs-12">
    <section class="box ">
            <header class="panel_header">
                <h2 class="title pull-left">Inline Form</h2>
                <div class="actions panel_actions pull-right">
                	<a class="box_toggle fa fa-chevron-down"></a>
                    <a class="box_setting fa fa-cog" data-toggle="modal" href="#section-settings"></a>
                    <a class="box_close fa fa-times"></a>
                </div>
            </header>
            <div class="content-body">
    <div class="row">
        <div class="col-xs-12">


            <form class="form-inline">
                <div class="form-group">
                    <label class="sr-only" for="exampleInputEmail3">Email address</label>
                    <input type="email" class="form-control" id="exampleInputEmail3" placeholder="Enter email">
                </div>
                <div class="form-group">
                    <label class="sr-only" for="exampleInputPassword3">Password</label>
                    <input type="password" class="form-control" id="exampleInputPassword3" placeholder="Password">
                </div>
                <div class="" style='margin:0px 15px;'>
                    <label>
                        <input type="checkbox" class="iCheck"> Remember me
                    </label>
                </div>
                <button type="submit" class="btn btn-primary">Sign in</button>

                <button type="button" class="btn btn-purple pull-right">Register</button>
            </form>

        </div>
    </div>

    </div>
        </section></div>




<!-- MAIN CONTENT AREA ENDS -->
    </section>
    </section>
    <!-- END CONTENT -->
</asp:Content>
