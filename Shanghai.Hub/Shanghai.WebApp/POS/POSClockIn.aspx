<%@ Page Title="" Language="C#" MasterPageFile="~/POSMaster.Master" AutoEventWireup="true" CodeBehind="POSClockIn.aspx.cs" Inherits="Shanghai.WebApp.POS.POSClockIn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <nav class="navbar navbar-default" style="position:unset !important">
        <div class="container-fluid">
            <div class="navbar-header">
                <a class="navbar-brand" style="font-weight:600" runat="server" id="SeatingLink" href="SeatingChart.aspx">Seating Chart</a>
            </div>
            <p class="navbar-text">Hello, <asp:Label Text="" ID="CurrentUser" runat="server" /></p>
            <ul class="nav navbar-nav navbar-right">
                <li><a href="POSLogIn.aspx">Log Out</a></li>
            </ul>
        </div>
    </nav>
    <asp:Panel runat="server" style="margin:0 auto; width:50%; ">
        <div style="max-height:500px; min-height:300px; overflow-y:scroll;">
                <asp:Repeater ID="HoursRepeater" ItemType="Shanghai.Data.Entities.WorkHour" runat="server">
        <HeaderTemplate>
            <table class="table table-responsive" style="border:1px solid #EEE; padding:25px;">
                <thead>
                    <tr>
                        <th>Shift Start</th>
                        <th>Shift End</th>
                        <th>Hours Worked</th>
                    </tr>
                </thead>
                <tbody style="min-height:300px;">
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <asp:Label Text='<%# Item.StartTime.Date.ToLongDateString() %>' runat="server" />
                    <br />
                    <asp:Label Text='<%# Item.StartTime.ToLongTimeString() %>' runat="server" />
                </td>
                <td>
                    <asp:Label Text='<%# Item.EndTime.HasValue? Item.EndTime.Value.Date.ToLongDateString() : "" %>' runat="server" />
                    <br />
                    <asp:Label Text='<%# Item.EndTime.HasValue? Item.EndTime.Value.ToLongTimeString() : "" %>' runat="server" />
                </td>
                <td>
                    <asp:Label Text='<%# Item.EndTime.HasValue ? (Item.EndTime.Value - Item.StartTime).TotalHours.ToString("N2") : "" %>' runat="server" />
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </tbody>
            </table>
        </FooterTemplate>
    </asp:Repeater>
        </div>

        <asp:Button Text="Start Shift" CssClass="btn btn-primary" OnInit="StartShiftBtn_Init" OnClick="StartShiftBtn_Click" ID="StartShiftBtn" runat="server" /> <asp:Button OnClick="EndShiftBtn_Click" CssClass="btn btn-primary" Text="End Shift" OnInit="EndShiftBtn_Init" ID="EndShiftBtn" runat="server" />
    </asp:Panel>
</asp:Content>
