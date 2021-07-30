<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BiltyDesign.aspx.cs" Inherits="BiltySystem.BiltyDesign" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <style>
                label {
                    text-decoration: underline;
                }

                #tblMain{
                    padding: 15px;
                }
            </style>
            <table style="width:100%;" id="tblMain">
                <tr>
                    <td>
                        BiltyNo#&nbsp;
                        <label>5751</label>
                    </td>            
                    <td>
                        Date&nbsp;
                        <label>05/09/2019</label>
                    </td>
                    <td>
                        Truck#&nbsp;
                        <label>TLK680</label>
                    </td>
                    <td>
                        From&nbsp;
                        <label>Khi</label>
                    </td>
                    <td>
                        To&nbsp;
                        <label>Hyd</label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        Sender#&nbsp;
                        <label>Baba Enterprises</label>
                    </td>
                    <td colspan="2">
                        Broker&nbsp;
                        <label>Ghulam Rasool</label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        Receiver#&nbsp;
                        <label>Amar Enterprises</label>
                    </td>
                    <td colspan="3">
                        Vehicle Contact&nbsp;
                        <label>0321 123 456 7</label>
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <table style="width: 100%" border="1">
                            <thead>
                                <tr>
                                    <th style="text-align: center;">
                                        Nos.
                                    </th>
                                    <th style="text-align: center;">
                                        Description
                                    </th>
                                    <th style="text-align: center;">
                                        Weight (Kg)
                                    </th>
                                    <th style="text-align: center;">
                                        Total Rent
                                    </th>
                                    <th style="text-align: center;">
                                        Paishgi
                                    </th>
                                    <th style="text-align: center;">
                                        Total
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td style="text-align: center;">
                                        1 X 40
                                    </td>
                                    <td>
                                        Container Import
                                    </td>
                                    <td style="text-align: center;">
                                        120
                                    </td>
                                    <td style="text-align: center;">
                                        25,000
                                    </td>
                                    <td style="text-align: center;">

                                    </td>
                                    <td style="text-align: center;">
                                        25,000
                                    </td>
                                </tr>
                            </tbody>
                    
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">Shipping Line: <lablel></lablel></td>
                    <td colspan="3">Address: <lablel>B2, Horth Hyderabad, Hyderabad</lablel></td>
                </tr>
                <tr>
                    <td colspan="5">
                        <label>MSU890890890</label>
                    </td>
                </tr>
            </table>
        </div>

        <a href="#" onclick="Print(); return false;">LINK</a>
    </form>
    <script type="text/javascript">
        function Test() {
            alert("Checked");
        }

        function Print() {
            //alert(123);
            debugger;
            //readTextFile("assets/css/style.css");
            
            var InvoiceHTML = "";
            InvoiceHTML += "<style>";
                InvoiceHTML += "label {";
                    InvoiceHTML += "text-decoration: underline;";
                InvoiceHTML += "}";
                InvoiceHTML += "#tblMain tr {";
                    InvoiceHTML += "padding: 15px;";
                InvoiceHTML += "}";
            InvoiceHTML += "</style>";
            InvoiceHTML += "<table style=\"width:100%;\ ID=\"tblMain\">";
                InvoiceHTML += "<tr>";
                    InvoiceHTML += "<td>";
                        InvoiceHTML += "BiltyNo#&nbsp;";
            InvoiceHTML += "<label>5751</label>";
                    InvoiceHTML += "</td>";            
                    InvoiceHTML += "<td>";
                        InvoiceHTML += "Date&nbsp;";
                        InvoiceHTML += "<label>05/09/2019</label>";
                    InvoiceHTML += "</td>";
                    InvoiceHTML += "<td>";
                        InvoiceHTML += "Truck#&nbsp;";
                        InvoiceHTML += "<label>TLK680</label>";
                    InvoiceHTML += "</td>";
                    InvoiceHTML += "<td>";
                        InvoiceHTML += "From&nbsp;";
                        InvoiceHTML += "<label>Khi</label>";
                    InvoiceHTML += "</td>";
                    InvoiceHTML += "<td>";
                        InvoiceHTML += "To&nbsp;";
                        InvoiceHTML += "<label>Hyd</label>";
                    InvoiceHTML += "</td>";
                InvoiceHTML += "</tr>";
                InvoiceHTML += "<tr>";
                    InvoiceHTML += "<td colspan=\"3\">";
                        InvoiceHTML += "Sender#&nbsp;";
                        InvoiceHTML += "<label>Baba Enterprises</label>";
                    InvoiceHTML += "</td>";
                    InvoiceHTML += "<td colspan=\"2\">";
                        InvoiceHTML += "Broker&nbsp;";
                        InvoiceHTML += "<label>Ghulam Rasool</label>";
                    InvoiceHTML += "</td>";
                InvoiceHTML += "</tr>";
                InvoiceHTML += "<tr>";
                    InvoiceHTML += "<td colspan=\"2\">";
                        InvoiceHTML += "Receiver#&nbsp;";
                        InvoiceHTML += "<label>Amar Enterprises</label>";
                    InvoiceHTML += "</td>";
                    InvoiceHTML += "<td colspan=\"3\">";
                        InvoiceHTML += "Vehicle Contact&nbsp;";
                        InvoiceHTML += "<label>0321 123 456 7</label>";
                    InvoiceHTML += "</td>";
                InvoiceHTML += "</tr>";
                InvoiceHTML += "<tr>";
                    InvoiceHTML += "<td colspan=\"5\">";
                        InvoiceHTML += "<table style=\"width: 100%\" border=\"1\">";
                            InvoiceHTML += "<thead>";
                                InvoiceHTML += "<tr>";
                                    InvoiceHTML += "<th style=\"text-align: center;\">";
                                        InvoiceHTML += "Nos.";
                                    InvoiceHTML += "</th>";
                                    InvoiceHTML += "<th style=\"text-align: center;\">";
                                        InvoiceHTML += "Description";
                                    InvoiceHTML += "</th>";
                                    InvoiceHTML += "<th style=\"text-align: center;\">";
                                        InvoiceHTML += "Weight (Kg)";
                                    InvoiceHTML += "</th>";
                                    InvoiceHTML += "<th style=\"text-align: center;\">";
                                        InvoiceHTML += "Total Rent";
                                    InvoiceHTML += "</th>";
                                    InvoiceHTML += "<th style=\"text-align: center;\">";
                                        InvoiceHTML += "Paishgi";
                                    InvoiceHTML += "</th>";
                                    InvoiceHTML += "<th style=\"text-align: center;\">";
                                        InvoiceHTML += "Total";
                                    InvoiceHTML += "</th>";
                                InvoiceHTML += "</tr>";
                            InvoiceHTML += "</thead>";
                            InvoiceHTML += "<tbody>";
                                InvoiceHTML += "<tr>";
                                    InvoiceHTML += "<td style=\"text-align: center;\">";
                                        InvoiceHTML += "1 X 40";
                                    InvoiceHTML += "</td>";
                                    InvoiceHTML += "<td>";
                                        InvoiceHTML += "Container Import";
                                    InvoiceHTML += "</td>";
                                    InvoiceHTML += "<td style=\"text-align: center;\">";
                                        InvoiceHTML += "120";
                                    InvoiceHTML += "</td>";
                                    InvoiceHTML += "<td style=\"text-align: center;\">";
                                        InvoiceHTML += "25,000";
                                    InvoiceHTML += "</td>";
                                    InvoiceHTML += "<td style=\"text-align: center;\">";
                                    InvoiceHTML += "</td>";
                                    InvoiceHTML += "<td style=\"text-align: center;\">";
                                        InvoiceHTML += "25,000";
                                    InvoiceHTML += "</td>";
                                InvoiceHTML += "</tr>";
                            InvoiceHTML += "</tbody>";
                        InvoiceHTML += "</table>";
                    InvoiceHTML += "</td>";
                InvoiceHTML += "</tr>";
                InvoiceHTML += "<tr>";
                    InvoiceHTML += "<td colspan=\"2\">Shipping Line: <lablel></lablel></td>";
                    InvoiceHTML += "<td colspan=\"3\">Address: <lablel>B2, Horth Hyderabad, Hyderabad</lablel></td>";
                InvoiceHTML += "</tr>";
                InvoiceHTML += "<tr>";
                    InvoiceHTML += "<td colspan=\"5\">";
                        InvoiceHTML += "<label>MSU890890890</label>";
                    InvoiceHTML += "</td>";
                InvoiceHTML += "</tr>";
            InvoiceHTML += "</table>";

            var prntData = document.getElementById('ContentPlaceHolder1_pnlInvoices');
            var prntWindow = window.open("", "Print", "width=400,height=400,left=0,top=0,toolbar=0,scrollbar=1,status=0");
            var style = "";
            prntWindow.document.write(InvoiceHTML);
            prntWindow.document.close();
            prntWindow.focus();
            prntWindow.print();
            prntWindow.close();
        }
    </script>
</body>
</html>
