<%@ Page Title="" Language="C#" MasterPageFile="~/BiltySystem.Master" AutoEventWireup="true" CodeBehind="WebForm4.aspx.cs" Inherits="BiltySystem.WebForm4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width: 100%;">
        <div id="InvoiceHeader" style="width: 100%; background-color: #f5f5f5; padding: 0px 0px;">
            <div style="width: 16.66666667%; background: #3F51B5; padding: 3px 8px; color: #ffffff;">
                <h2 class="text-center bg-primary ">Invoice</h2>    
            </div>
            <div style="width: 25%; padding-top: 10px; padding-bottom: 10px; white-space: nowrap; font-size: 14px;"></div>
            <div style="width: 25%; padding-top: 10px; padding-bottom: 10px; white-space: nowrap; font-size: 14px;">
                <span style='color: #999999;'>
                    Invoice # BL3278947
                    <br>
                    25-July-2019
                </span>
            </div>
            <div style="width: 25%; padding-right: 0px; margin-left: 8.33333333%;">
                <img alt="" src="../data/invoice/invoice-logo.png" class="img-reponsive">
            </div>
        </div>
    </div>
</asp:Content>
