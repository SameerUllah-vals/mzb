<%@ Page Title="" Language="C#" MasterPageFile="~/BiltySystem.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="BiltySystem.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Dashboard - BiltySytem</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- START CONTENT -->
    <section id="main-content" class=" ">
        <section class="wrapper main-wrapper row" style=''>

            <div class='col-xs-12'>
                <div class="page-title">

                    <div class="pull-left">
                        <!-- PAGE HEADING TAG - START -->
                        <h1 class="title">Dashboard</h1>
                        <!-- PAGE HEADING TAG - END -->
                    </div>


                </div>
            </div>
            <div class="clearfix"></div>
            <!-- MAIN CONTENT AREA STARTS -->

            <div class="col-xs-12 col-md-6 col-lg-4">
                <section class="box ">
                    <header class="panel_header">
                        <h2 class="title pull-left">Platforms Used</h2>
                        <div class="actions panel_actions pull-right">
                            <a class="box_toggle fa fa-chevron-down"></a>
                            <a class="box_setting fa fa-cog" data-toggle="modal" href="#section-settings"></a>
                            <a class="box_close fa fa-times"></a>
                        </div>
                    </header>
                    <div class="content-body">
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="chart-container">
                                    <div class="" style="height: 200px" id="platform_type_dates"></div>
                                </div>
                            </div>
                        </div>
                        <!-- End .row -->
                    </div>
                </section>
            </div>



            <div class="col-xs-12 col-md-6 col-lg-4">
                <section class="box ">
                    <header class="panel_header">
                        <h2 class="title pull-left">Guest / Registered</h2>
                        <div class="actions panel_actions pull-right">
                            <a class="box_toggle fa fa-chevron-down"></a>
                            <a class="box_setting fa fa-cog" data-toggle="modal" href="#section-settings"></a>
                            <a class="box_close fa fa-times"></a>
                        </div>
                    </header>
                    <div class="content-body">
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="chart-container">
                                    <div class="" style="height: 200px" id="user_type"></div>
                                </div>
                            </div>
                        </div>
                        <!-- End .row -->
                    </div>
                </section>
            </div>



            <div class="col-xs-12 col-md-6 col-lg-4">
                <section class="box ">
                    <header class="panel_header">
                        <h2 class="title pull-left">Browsers Used</h2>
                        <div class="actions panel_actions pull-right">
                            <a class="box_toggle fa fa-chevron-down"></a>
                            <a class="box_setting fa fa-cog" data-toggle="modal" href="#section-settings"></a>
                            <a class="box_close fa fa-times"></a>
                        </div>
                    </header>
                    <div class="content-body">
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="chart-container">
                                    <div class="" style="height: 200px" id="browser_type"></div>
                                </div>


                            </div>
                        </div>
                        <!-- End .row -->
                    </div>
                </section>
            </div>

            <div class="col-xs-12 col-md-6 col-lg-4">
                <section class="box ">
                    <header class="panel_header">
                        <h2 class="title pull-left">Site Referrals</h2>
                        <div class="actions panel_actions pull-right">
                            <a class="box_toggle fa fa-chevron-down"></a>
                            <a class="box_setting fa fa-cog" data-toggle="modal" href="#section-settings"></a>
                            <a class="box_close fa fa-times"></a>
                        </div>
                    </header>
                    <div class="content-body">
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="chart-container">
                                    <div class="chart has-fixed-height" style="height: 200px" id="scatter_chart"></div>
                                </div>
                            </div>
                        </div>
                        <!-- End .row -->
                    </div>
                </section>
            </div>

            <div class="col-xs-12 col-md-6 col-lg-4">
                <section class="box ">
                    <header class="panel_header">
                        <h2 class="title pull-left">Today Visitors</h2>
                        <div class="actions panel_actions pull-right">
                            <a class="box_toggle fa fa-chevron-down"></a>
                            <a class="box_setting fa fa-cog" data-toggle="modal" href="#section-settings"></a>
                            <a class="box_close fa fa-times"></a>
                        </div>
                    </header>
                    <div class="content-body">
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="chart-container">
                                    <div class="chart has-fixed-height" style="height: 200px" id="page_views_today"></div>
                                </div>
                            </div>
                        </div>
                        <!-- End .row -->
                    </div>
                </section>
            </div>


            <div class="col-xs-12 col-md-6 col-lg-4">
                <section class="box ">
                    <header class="panel_header">
                        <h2 class="title pull-left">Server Load</h2>
                        <div class="actions panel_actions pull-right">
                            <a class="box_toggle fa fa-chevron-down"></a>
                            <a class="box_setting fa fa-cog" data-toggle="modal" href="#section-settings"></a>
                            <a class="box_close fa fa-times"></a>
                        </div>
                    </header>
                    <div class="content-body">
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="chart-container">
                                    <div class="chart has-fixed-height" style="height: 200px" id="gauge_chart"></div>
                                </div>
                            </div>
                        </div>
                        <!-- End .row -->
                    </div>
                </section>
            </div>


            <div class="clearfix"></div>
            <div class="col-lg-12">
                <section class="box nobox marginBottom0">
                    <div class="content-body">
                        <div class="row">
                            <div class="col-lg-3 col-sm-6 col-xs-12">
                                <div class="r4_counter db_box">
                                    <i class='pull-left fa fa-thumbs-up icon-md icon-rounded icon-primary'></i>
                                    <div class="stats">
                                        <h4><strong>45%</strong></h4>
                                        <span>New Orders</span>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3 col-sm-6 col-xs-12">
                                <div class="r4_counter db_box">
                                    <i class='pull-left fa fa-shopping-cart icon-md icon-rounded icon-accent'></i>
                                    <div class="stats">
                                        <h4><strong>243</strong></h4>
                                        <span>New Products</span>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3 col-sm-6 col-xs-12">
                                <div class="r4_counter db_box">
                                    <i class='pull-left fa fa-dollar icon-md icon-rounded icon-purple'></i>
                                    <div class="stats">
                                        <h4><strong>$3424</strong></h4>
                                        <span>Profit Today</span>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3 col-sm-6 col-xs-12">
                                <div class="r4_counter db_box">
                                    <i class='pull-left fa fa-users icon-md icon-rounded icon-warning'></i>
                                    <div class="stats">
                                        <h4><strong>1433</strong></h4>
                                        <span>New Users</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- End .row -->
                    </div>
                </section>
            </div>


            <div class="col-xs-12">
                <section class="box ">
                    <header class="panel_header">
                        <h2 class="title pull-left">Visitor's Statistics</h2>
                        <div class="actions panel_actions pull-right">
                            <a class="box_toggle fa fa-chevron-down"></a>
                            <a class="box_setting fa fa-cog" data-toggle="modal" href="#section-settings"></a>
                            <a class="box_close fa fa-times"></a>
                        </div>
                    </header>
                    <div class="content-body">
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="wid-vectormap">
                                    <div class="row">
                                        <div class="col-xs-12 col-sm-9">
                                            <figure>
                                                <div id="db-world-map-markers" style="width: 100%; height: 300px"></div>
                                            </figure>
                                        </div>
                                        <div class="map_progress col-xs-12 col-sm-3">
                                            <h4>Unique Visitors</h4>
                                            <span class='text-muted'><small>Last Week Rise by 62%</small></span>
                                            <div class="progress">
                                                <div class="progress-bar progress-bar-primary" role="progressbar" aria-valuenow="62" aria-valuemin="0" aria-valuemax="100" style="width: 62%"></div>
                                            </div>
                                            <br>
                                            <h4>Registrations</h4>
                                            <span class='text-muted'><small>Up by 57% last 7 days</small></span>
                                            <div class="progress">
                                                <div class="progress-bar progress-bar-primary" role="progressbar" aria-valuenow="57" aria-valuemin="0" aria-valuemax="100" style="width: 57%"></div>
                                            </div>
                                            <br>
                                            <h4>Direct Sales</h4>
                                            <span class='text-muted'><small>Last Month Rise by 22%</small></span>
                                            <div class="progress">
                                                <div class="progress-bar progress-bar-primary" role="progressbar" aria-valuenow="22" aria-valuemin="0" aria-valuemax="100" style="width: 22%"></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- End .row -->

                    </div>
                </section>
            </div>


            <!-- MAIN CONTENT AREA ENDS -->
        </section>
    </section>
    <!-- END CONTENT -->
</asp:Content>
