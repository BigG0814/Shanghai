<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ShiftTrade.ascx.cs" Inherits="Shanghai.WebApp.UserControls.ShiftTrade" %>

<%--<asp:Button Text="Show Trade" ID="ShowTrade" OnClick="ShowTrade_Click" runat="server" />--%>
<%--<asp:DropDownList ID="ShiftTradeDDL" runat="server" DataSourceID="ShiftTradeODS" DataTextField="TimePosted" DataValueField="TradeID" OnSelectedIndexChanged="PostTrade_Click">
    <asp:ListItem Selected="True" Value="0">View Pending Trades</asp:ListItem>
</asp:DropDownList>

<asp:ObjectDataSource runat="server" ID="ShiftTradeODS" OldValuesParameterFormatString="original_{0}" SelectMethod="ShiftTrade_List" TypeName="Shanghai.System.BLL.ShiftTradingProcessController">
</asp:ObjectDataSource>--%>
<asp:Panel Visible="true" ID="TradePanel" runat="server">
    <div class="row">
        <div class="col-md-12">
            <asp:HiddenField ID="ShiftIDHF" runat="server" />
            <table class="table">
                <thead>
                    <tr>
                        <th style="width: 25%;"></th>
                        <th style="width: 75%;"></th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>
                            <asp:Label Text="ShiftDate: " runat="server" /></td>
                        <td>
                            <asp:Label ID="ShiftDateL" Text="text" runat="server" /></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label Text="StartTime: " runat="server" /></td>
                        <td>
                            <asp:Label ID="StartTimeL" Text="text" runat="server" /></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label Text="EndTime: " runat="server" /></td>
                        <td>
                            <asp:Label ID="EndTimeL" Text="text" runat="server" /></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label Text="InitialEmployee: " runat="server" /></td>
                        <td>
                            <asp:Label ID="InitialEmployeeL" Text="text" runat="server" />
                            <asp:HiddenField ID="InitialEmployeeid" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label Text="NewEmployee: " runat="server" /></td>
                        <td>
                            <asp:Label ID="NewEmployeeL" Text="Unknown" runat="server" /></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label Text="Reasons: " runat="server" /></td>
                        <td>
                            <asp:TextBox ID="ReasonsTB" TextMode="MultiLine" Width="100%" Height="100px" runat="server" /></td>
                    </tr>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="2">
                            <asp:Button Text="Post Trade" CssClass="btn btn-success pull-left" ID="PostTrade" OnClick="PostTrade_Click" runat="server" /> &nbsp;
                            <asp:Button Text="Accept Trade" CssClass="btn btn-success pull-left" ID="AcceptTrade" OnClick="AcceptTrade_Click" runat="server" /> &nbsp; 
                            <asp:Button Text="Approve Trade" CssClass="btn btn-success pull-left" ID="ApproveTrade" OnClick="ApproveTrade_Click" runat="server" /> &nbsp;
                            <asp:Button Text="Delete Trade Offer" CssClass="btn btn-danger pull-left" ID="DeleteOffer" OnClick="DeleteOffer_Click" runat="server" /> &nbsp;
                            <asp:Button Text="Close" ID="Cancel" CssClass="btn btn-default pull-right" OnClick="Cancel_Click" runat="server" />
                        </td>
                    </tr>
                </tfoot>
            </table>
        </div>
    </div>
</asp:Panel>
