<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GroupsJDT.aspx.cs" Inherits="ValsProjectManagment.GroupsJDT" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

            
        <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField HeaderText="S.no">
                    <ItemTemplate>
                        <%#Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="GroupCode" HeaderText="Code"></asp:BoundField>
                <asp:BoundField DataField="GroupName" HeaderText="Group"></asp:BoundField>
                <asp:BoundField DataField="EmailAdd" HeaderText="Email"></asp:BoundField>
                <asp:BoundField DataField="WebAdd" HeaderText="Website"></asp:BoundField>
                <asp:BoundField DataField="Contact" HeaderText="Contact"></asp:BoundField>
                <%--<asp:BoundField DataField="Status" HeaderText="Active"></asp:BoundField>--%>
                <asp:TemplateField HeaderText="Active">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkActive" runat="server" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="IsActive"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="View/Edit">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkView" runat="server" CssClass="fa fa-eye" ForeColor="DodgerBlue" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="View"></asp:LinkButton>
                        <asp:LinkButton ID="lnkEdit" runat="server" CssClass="fa fa-edit m-l-15" ForeColor="LightSeaGreen" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="Change"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkDelete" runat="server" ForeColor="Maroon" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="DeleteGroup"><i class="fas fa-trash"></i></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
                </ContentTemplate>
        </asp:UpdatePanel>
        <style type="text/css">
            body {
                font-family: Arial;
                font-size: 10pt;
            }

            table {
                border: 1px solid #ccc;
            }

                table th {
                    background-color: #F7F7F7;
                    color: #333;
                    font-weight: bold;
                }

                table th, table td {
                    padding: 5px;
                    border-color: #ccc;
                }

            .highlight {
                background-color: Lime;
            }
        </style>
        <link type="text/css" rel="stylesheet" href="https://cdn.datatables.net/1.10.12/css/jquery.dataTables.min.css" />
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <script type="text/javascript" src="https://cdn.datatables.net/1.10.12/js/jquery.dataTables.min.js"></script>
        <script type="text/javascript">
            $(document).ready(function () {
                $("#gvCustomers").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable();
                $('#gvCustomers').DataTable();
                $('input[type=search]').on("keyup", function () {
                    var searchTerm = $(this).val();
                    $(".forHighlight").each(function () {
                        var searchPattern = new RegExp('(' + searchTerm + ')', 'ig');
                        $(this).html($(this).text().replace(searchPattern, "<span class = 'highlight'>" + searchTerm + "</span>"));
                    });
                });
            });
        </script>
    </form>
</body>
</html>
