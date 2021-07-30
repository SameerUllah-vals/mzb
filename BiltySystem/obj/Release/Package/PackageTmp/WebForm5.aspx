<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm5.aspx.cs" Inherits="BiltySystem.WebForm5" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    
</head>
<body>
    <form id="form1" runat="server">
        <i class="fa fa-plus"></i>
        <%--<div style="width: 100%;">
            <div id="InvoiceHeader" style="width: 100%; background-color: #f5f5f5; padding: 0px 0px;">
                <div style="width: 16.66666667%; background: #3F51B5; padding: 3px 8px; color: #ffffff; float: left;">
                    <h2 style="text-align: center;">Invoice</h2>    
                </div>
                <div style="width: 25%; padding-top: 10px; padding-bottom: 10px; white-space: nowrap; font-size: 14px; float: left;"></div>
                <div style="width: 25%; padding-top: 10px; padding-bottom: 10px; white-space: nowrap; font-size: 14px; float: left;">
                    <span style='color: #999999;'>
                        Invoice # BL3278947
                        <br />
                        25-July-2019
                    </span>
                </div>
                <div style="width: 25%; padding-right: 0px; float: left;">
                    <img alt="" src="../data/invoice/invoice-logo.png" style="text-align: center" />
                </div>
            </div>
            <div style="width: 100%">
                <h2>Karachi Office: Plot H/2, between Gate # 2 & 3, Quaid-e-Azam Truck Stand, Karachi. 0300 823 29 94</h2>
                <h2>Hyderabad Office: Plot # 54, Halanaka Road, Hyderabad. 022 203 26 86</h2>
            </div>
            <div id="InvoiceBody" style="width: 100%;">
                <div style="width: 50%; float: left;">
                    <h4>Sender</h4>
                    <h3><span>Sender Name</span></h3>
                    <address>
                        <span style="color: #999999;">Sender address will be here, Sender address will be here.</span>
                    </address>
                </div>
                
                <div style="width: 50%; float: left; text-align: right;">
                    <h4>Receiver</h4>
                    <h3><span>Receiver Name</span></h3>
                    <address>
                        <span style="color: #999999;">Receiver address will be here, Receiver address will be here.</span>
                    </address>
                </div>
            </div>
            <div style="width: 100%;">
                <table style="width: 100%;">
                    <thead>
                        <tr style="background-color: #3F51B5; color: white;">
                            <th>No. Of Packages</th>
                            <th>Description</th>
                            <th>Rate</th>
                            <th>Amount</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>10X20</td>
                            <td>10x20 for Export from Karachi to Sindh to Faislabad to Lahore</td>
                            <td>43000/-</td>
                            <td>43000/-</td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>Empty Lift on chargers</td>
                            <td>800/-</td>
                            <td>800/-</td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>Weighment Charges</td>
                            <td>190/-</td>
                            <td>190/-</td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <div style="width: 100%;">
                                    <h3>Containers Summary</h3>
                                    <table style="width: 100%">
                                        <thead style="padding: 10px;">
                                            <tr style="background-color: #3F51B5; color: white;">
                                                <th>Date</th>
                                                <th>Vehicle #</th>
                                                <th>Container #</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>25-July-2019</td>
                                                <td>KGK6686</td>
                                                <td>MSI78979879879</td>
                                            </tr>
                                            <tr>
                                                <td>22-July-2019</td>
                                                <td>KMB5522</td>
                                                <td>MSU8979098008</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </td>
                            <td></td>
                            <td></td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>--%>
        <div style="width: 100%; "><div id = "InvoiceHeader" style = "width: 100%; background-color: #f5f5f5; padding: 0px 0px;"><div style="width: 25%; background: #3F51B5; padding: 3px 8px; color: #ffffff; float: left;"><h2 style="text-align: center;">Invoice</h2></div><div style="width: 25%; padding-top: 10px; padding-bottom: 10px; white-space: nowrap; font-size: 14px; float: left;"></div><div style="width: 25%; padding-top: 10px; padding-bottom: 10px; white-space: nowrap; font-size: 14px; float: left;"><span style='color: #999999;'>Invoice # <br /></span></div><div style="width: 25%; padding-right: 0px; float: left;"><img alt="" src="../data/invoice/invoice-logo.png" style="text-align: center" /></div></div><div style="width: 100%"><h6 style="text-align: center; font-size: 12; margin: 0;">Karachi Office: Plot H/2, between Gate # 2 & 3, Quaid-e-Azam Truck Stand, Karachi. 0300 823 29 94</h6><h6 style="text-align: center; margin: 0;">Hyderabad Office: Plot # 54, Halanaka Road, Hyderabad. 022 203 26 86</h6></div><div id="InvoiceBody" style="width: 100%;"><div style="width: 50%; float: left;"><h4>Sender</h4><h3><span></span></h3><address><span></span></address></div><div style="width: 50%; float: left; text-align: right;"><h4>Receiver</h4><h3><span></span></h3><address><span>-</span></address></div></div><div style="width: 100%;"><table style="width: 100%;"><thead><tr style="background-color: #3F51B5; color: white;"><th style="padding: 10px;">No. Of Packages</th><th style="padding: 10px;">Description</th><th style="padding: 10px;">Rate</th><th style="padding: 10px;">Amount</th></tr></thead><tbody><tr><td></td><td>3X20 for Export from Karachi to Lahore to Lahore to Lahore</td><td>73000/-</td><td>73000/-</td></tr><tr><td></td><td colspan="3"><div style="width: 100%;"><h3>Containers Summary</h3><table style="width: 100%"><thead><tr style="background-color: #3F51B5; color: white; padding: 10px;"><th style="padding: 10px;">Date</th><th style="padding: 10px;">Vehicle #</th><th style="padding: 10px;">Container #</th><th style="padding: 10px;">Rate</th></tr></thead><tbody><tr><td style="text-align: center;">7/25/2019 12:09:52 AM</td><td style="text-align: center;">JU9093</td><td style="text-align: center;">MSI868767868</td><td style="text-align: center;">20000</td></tr><tr><td style="text-align: center;">7/25/2019 12:13:58 AM</td><td style="text-align: center;">JU8890</td><td style="text-align: center;">MSI9099999</td><td style="text-align: center;">30000</td></tr><tr><td style="text-align: center;">7/26/2019 6:02:35 AM</td><td style="text-align: center;">KMB5522</td><td style="text-align: center;">MSI8999080</td><td style="text-align: center;">23000</td></tr></tbody></table></div></td></tr><tr><td></td><td>Empty Lift on chargers</td><td>800/-</td><td>800/-</td></tr><tr><td></td><td>Weighment Charges</td><td>190/-</td><td>190/-</td></tr><tr><td></td><td>Total</td><td>73190/-</td><td>73190/-</td></tr></tbody></table></div></div>
        <table style="width: 100%" border="1">
            <tr>
                <td>
                    Description
                </td>
                <td>
                    Quantity
                </td>
                <td>
                    Amount
                </td>
            </tr>
            <tr>
                <td style="padding: 10px;">
                    This is long description
                </td>
                <td>
                    1
                </td>
                <td>
                    200
                </td>
            </tr>
            <tr>
                <td style="padding: 10px;">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
