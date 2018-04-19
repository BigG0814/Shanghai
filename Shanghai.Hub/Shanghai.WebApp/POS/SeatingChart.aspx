<%@ Page Title="Seating Chart" Language="C#" MasterPageFile="~/POSMaster.Master" AutoEventWireup="true" CodeBehind="SeatingChart.aspx.cs" Inherits="Shanghai.WebApp.POS.SeatingChart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        body {
            background: white;
        }

        .squareTable {
            width: 7vw;
            height: 7vw;
            border: 1px solid hidden;
        }

        .circleTable {
            width: 11vw;
            height: 11vw;
            border: 1px solid hidden;
            border-radius: 5vw;
        }

        .rectangleTable {
            width: 11vw;
            height: 6vw;
            border: 1px solid hidden;
        }

        .tableButton {
            background-color: transparent;
            position: absolute;
            font-weight: bold;
            font-size: 2vw;
        }

        .tableButtonLabel {
            background-color: transparent;
            position: absolute;
            font-weight: bold;
            font-size: 1.3vw;
            margin-top: -1.7vw;
            width: 10vw;
            margin-left: -1vw;
            background: #eaeaea;
        }


        .tableRowOne {
            background-color: deepskyblue;
        }

        .tableRowtwo {
            background-color: #8bf18b;
        }

        .tableRowThree {
            background-color: #ff9292;
        }

        .buffet {
            width: 37vw;
            height: 4vw;
            top: 3vw;
            left: 31vw;
        }

        .barCash {
            width: 30vw;
            height: 4vw;
            top: 53vw;
            left: 46vw;
        }

        .door {
            width: 16vw;
            height: 15vw;
            top: 53vw;
            left: 84vw;
            border-bottom: hidden;
            border-right: hidden;
        }

        .ladies {
            border-top: hidden;
            border-left: hidden;
            border-right: hidden;
            width: 10vw;
            height: 4vw;
            top: 0vw;
            left: 1vw;
        }

        .mens {
            border-top: hidden;
            border-left: hidden;
            border-right: hidden;
            width: 10vw;
            height: 4vw;
            top: 0vw;
            left: 89vw;
        }

        .LayoutLine1 {
            height: 33vw;
            top: 9vw;
            left: 16.9vw;
            border-top: hidden;
            border-left: hidden;
            border-bottom: hidden;
            border-right: double;
        }

        .LayoutLine2 {
            height: 35vw;
            top: 9vw;
            left: 72.2vw;
            border-top: hidden;
            border-left: hidden;
            border-bottom: hidden;
            border-right: double;
        }

        .LayoutLine3 {
            height: 11vw;
            top: 57vw;
            left: 74.6vw;
            border-top: hidden;
            border-left: hidden;
            border-bottom: hidden;
        }

        .LayoutLine4 {
            height: 21vw;
            top: 48vw;
            left: 17.3vw;
            width: 25vw;
            border-left: hidden;
            border-bottom: hidden;
            border-right: double;
            border-top: double;
        }

        .LayoutLine5 {
            top: 47.2vw;
            left: 86vw;
            width: 10vw;
            border-left: hidden;
            border-bottom: hidden;
            border-right: hidden;
            border-top: double;
        }

        .LayoutLine6 {
            top: 9.4vw;
            left: 86vw;
            width: 10vw;
            border-left: hidden;
            border-bottom: hidden;
            border-right: hidden;
            border-top: double;
        }


        .table1 {
            left: .73vw;
            top: 10.4vw;
        }

        .table2 {
            left: .73vw;
            top: 21.6vw;
        }

        .table3 {
            left: .73vw;
            top: 32.3vw;
        }

        .table4 {
            left: .73vw;
            top: 42.3vw;
        }

        .table5 {
            left: 3.5vw;
            top: 53.9vw;
        }

        .table6 {
            left: 18vw;
            top: 59.8vw;
        }

        .table7 {
            left: 35vw;
            top: 59.3vw;
        }

        .table8 {
            left: 35vw;
            top: 49.3vw;
        }

        .table9 {
            left: 24.1vw;
            top: 48.4vw;
        }

        .table11 {
            left: 11vw;
            top: 10.5vw;
        }

        .table10 {
            left: 11vw;
            top: 21.2vw;
        }

        .tableb1 {
            left: 55vw;
            top: 10.5vw;
        }

        .tableb2 {
            left: 55vw;
            top: 20.1vw;
        }

        .tableb3 {
            left: 44.5vw;
            top: 10.5vw;
        }

        .tableb4 {
            left: 44.5vw;
            top: 20.1vw;
        }

        .tableb5 {
            left: 35vw;
            top: 13.9vw;
        }

        .tableb6 {
            left: 27.6vw;
            top: 23.3vw;
        }

        .tableb7 {
            left: 41vw;
            top: 32.3vw;
        }

        .tableb8 {
            left: 30.1vw;
            top: 32.3vw;
        }

        .tableb9 {
            left: 21.3vw;
            top: 8.7vw;
        }

        .tableb11 {
            left: 18.2vw;
            top: 30.9vw;
        }

        .tableb10 {
            left: 18.2vw;
            top: 20.1vw;
        }

        .tablea1 {
            left: 87.5vw;
            top: 40.2vw;
        }

        .tablea2 {
            left: 87.5vw;
            top: 30vw;
        }

        .tablea3 {
            left: 87.5vw;
            top: 19.7vw;
        }

        .tablea4 {
            left: 87.5vw;
            top: 9.6vw;
        }

        .tablea5 {
            left: 73.6vw;
            top: 28.2vw;
        }

        .tablea6 {
            left: 73.6vw;
            top: 14.8vw;
        }

        .tablea7 {
            left: 66.3vw;
            top: 33.4vw;
        }

        .tablea8 {
            left: 66.3vw;
            top: 19.9vw;
        }

        .tablea9 {
            left: 66.3vw;
            top: 10.2vw;
        }

        .tablea10 {
            left: 52.6vw;
            top: 31vw;
        }

        .btncontainer {
            display: flex;
            width: 100%;
        }

        .ItemBtnContainer {
            flex: 1;
            padding: 16px;
        }
    
    </style>

    <nav class="navbar navbar-default" style="margin-bottom:0px;">
        <div class="container-fluid">
            <p class="navbar-text">Hello, <asp:Label Text="" ID="CurrentUser" runat="server" /></p>
            <ul class="nav navbar-nav">
            </ul>
            <div class="navbar-form navbar-left">
                <div class="input-group">
                    <%--<asp:Label Text="Delivery: " runat="server" class="dropdownlabel"/>--%>
                    <asp:DropDownList ID="DeliveryDDL" CssClass="form-control" runat="server" DataSourceID="BillODS" DataTextField="FullName" DataValueField="TableNumber">
                        <asp:ListItem Selected="True">Select A Delivery Order</asp:ListItem>
                    </asp:DropDownList>
                    
                <div class="input-group-btn">
                    <asp:Button Text="Go D.O" runat="server" ID="DeliveryDropdownButton" CssClass="btn btn-default" OnClick="SelectDeliveryDropDown" />
                </div>
                    </div>
                <div class="input-group">
                    <%--<asp:Label Text="Takeout: " runat="server" class="dropdownlabel" />--%>
                    <asp:DropDownList ID="TakeoutDDL" CssClass="form-control" runat="server" DataSourceID="BillODS2" DataTextField="FullName" DataValueField="TableNumber"></asp:DropDownList>
                
                <div class="input-group-btn">
                    <asp:Button Text="Go T.O." CssClass="btn btn-default" runat="server" ID="TakeoutDropdownButton" OnClick="SelectTakeoutDropDown" />
                </div>
                </div>
            </div>
            <ul class="nav navbar-nav navbar-right">
                <li><a href="POSLogIn.aspx">Log Out</a></li>
                <li><a href="POSClockIn.aspx">Clock Out</a></li>
                <li>
                    <asp:LinkButton Text="Quit POS" ID="QuitPOSBtn" OnClick="QuitPOSBtn_Click" runat="server" /></li>
            </ul>
        </div>
    </nav>
    <asp:ObjectDataSource runat="server" ID="BillODS" OldValuesParameterFormatString="original_{0}" SelectMethod="DeliveryTables" TypeName="Shanghai.System.BLL.BillController"></asp:ObjectDataSource>
    <asp:ObjectDataSource runat="server" ID="BillODS2" OldValuesParameterFormatString="original_{0}" SelectMethod="TakeoutTables" TypeName="Shanghai.System.BLL.BillController"></asp:ObjectDataSource>

    <div style="padding-left: 5px; padding-top: 10px;">
    </div>
    <div>

        <div style="margin-top: 5px; position: absolute;" class="col-md-9">
            <asp:Button ID="buffet" Text="Buffet" CssClass="buffet tableButton" runat="server" Enabled="false" BorderColor="Black" />
            <asp:Button ID="barCash" Text="Bar & Cashier" CssClass="barCash tableButton" runat="server" Enabled="false" BorderColor="Black" />
            <asp:Button ID="door" Text="Door" CssClass="door tableButton" runat="server" Enabled="false" BorderColor="Black" />
            <asp:Button ID="mens" Text="Men's" CssClass="mens tableButton" runat="server" Enabled="false" BorderColor="Black" />
            <asp:Button ID="ladies" Text="Ladies" CssClass="ladies tableButton" runat="server" Enabled="false" BorderColor="Black" />
            <asp:Button ID="LayoutLine1" CssClass="LayoutLine1 tableButton" runat="server" Enabled="false" BorderColor="Black" />
            <asp:Button ID="LayoutLine2" CssClass="LayoutLine2 tableButton" runat="server" Enabled="false" BorderColor="Black" />
            <asp:Button ID="LayoutLine3" CssClass="LayoutLine3 tableButton" runat="server" Enabled="false" BorderColor="Black" />
            <asp:Button ID="LayoutLine4" CssClass="LayoutLine4 tableButton" runat="server" Enabled="false" BorderColor="Black" />
            <asp:Button ID="LayoutLine5" CssClass="LayoutLine5 tableButton" runat="server" Enabled="false" BorderColor="Black" />
            <asp:Button ID="LayoutLine6" CssClass="LayoutLine6 tableButton" runat="server" Enabled="false" BorderColor="Black" />
            <asp:Button ID="C1" Text="1" CssClass="tableButton squareTable tableRowOne table1" BorderColor="Black" OnClick="SelectTable" runat="server" />
            <div runat="server">
                <asp:Label ID="ServerOfTableC1" CssClass="table1 tableButtonLabel" runat="server" /></div>
            <asp:Button ID="C2" Text="2" CssClass="tableButton squareTable tableRowOne table2" BorderColor="Black" OnClick="SelectTable" runat="server" />
            <div runat="server">
                <asp:Label ID="ServerOfTableC2" CssClass="table2 tableButtonLabel" runat="server" /></div>
            <asp:Button ID="C3" Text="3" CssClass="tableButton squareTable tableRowOne table3" BorderColor="Black" OnClick="SelectTable" runat="server" />
            <div runat="server">
                <asp:Label ID="ServerOfTableC3" CssClass="table3 tableButtonLabel" runat="server" /></div>
            <asp:Button ID="C4" Text="4" CssClass="tableButton squareTable tableRowOne table4" BorderColor="Black" OnClick="SelectTable" runat="server" />
            <div runat="server">
                <asp:Label ID="ServerOfTableC4" CssClass="table4 tableButtonLabel" runat="server" /></div>
            <asp:Button ID="C5" Text="5" CssClass="tableButton circleTable tableRowOne table5" BorderColor="Black" OnClick="SelectTable" runat="server" />
            <div runat="server">
                <asp:Label ID="ServerOfTableC5" CssClass="table5 tableButtonLabel" runat="server" /></div>
            <asp:Button ID="C6" Text="6" CssClass="tableButton rectangleTable tableRowOne table6" BorderColor="Black" OnClick="SelectTable" runat="server" />
            <div runat="server">
                <asp:Label ID="ServerOfTableC6" CssClass="table6 tableButtonLabel" runat="server" /></div>
            <asp:Button ID="C7" Text="7" CssClass="tableButton squareTable tableRowOne table7" BorderColor="Black" OnClick="SelectTable" runat="server" />
            <div runat="server">
                <asp:Label ID="ServerOfTableC7" CssClass="table7 tableButtonLabel" runat="server" /></div>
            <asp:Button ID="C8" Text="8" CssClass="tableButton squareTable tableRowOne table8" BorderColor="Black" OnClick="SelectTable" runat="server" />
            <div runat="server">
                <asp:Label ID="ServerOfTableC8" CssClass="table8 tableButtonLabel" runat="server" /></div>
            <asp:Button ID="C9" Text="9" CssClass="tableButton squareTable tableRowOne table9" BorderColor="Black" OnClick="SelectTable" runat="server" />
            <div runat="server">
                <asp:Label ID="ServerOfTableC9" CssClass="table9 tableButtonLabel" runat="server" /></div>
            <asp:Button ID="C10" Text="10" CssClass="tableButton squareTable tableRowOne table10" BorderColor="Black" OnClick="SelectTable" runat="server" />
            <div runat="server">
                <asp:Label ID="ServerOfTableC10" CssClass="table10 tableButtonLabel" runat="server" /></div>
            <asp:Button ID="C11" Text="11" CssClass="tableButton squareTable tableRowOne table11" BorderColor="Black" OnClick="SelectTable" runat="server" />
            <div runat="server">
                <asp:Label ID="ServerOfTableC11" CssClass="table11 tableButtonLabel" runat="server" /></div>
            <asp:Button ID="B1" Text="1" CssClass="tableButton squareTable tableRowtwo tableb1" BorderColor="Black" OnClick="SelectTable" runat="server" />
            <div runat="server">
                <asp:Label ID="ServerOfTableB1" CssClass="tableb1 tableButtonLabel" runat="server" /></div>
            <asp:Button ID="B2" Text="2" CssClass="tableButton squareTable tableRowtwo tableb2" BorderColor="Black" OnClick="SelectTable" runat="server" />
            <div runat="server">
                <asp:Label ID="ServerOfTableB2" CssClass="tableb2 tableButtonLabel" runat="server" /></div>
            <asp:Button ID="B3" Text="3" CssClass="tableButton squareTable tableRowtwo tableb3" BorderColor="Black" OnClick="SelectTable" runat="server" />
            <div runat="server">
                <asp:Label ID="ServerOfTableB3" CssClass="tableb3 tableButtonLabel" runat="server" /></div>
            <asp:Button ID="B4" Text="4" CssClass="tableButton squareTable tableRowtwo tableb4" BorderColor="Black" OnClick="SelectTable" runat="server" />
            <div runat="server">
                <asp:Label ID="ServerOfTableB4" CssClass="tableb4 tableButtonLabel" runat="server" /></div>
            <asp:Button ID="B5" Text="5" CssClass="tableButton squareTable tableRowtwo tableb5" BorderColor="Black" OnClick="SelectTable" runat="server" />
            <div runat="server">
                <asp:Label ID="ServerOfTableB5" CssClass="tableb5 tableButtonLabel" runat="server" /></div>
            <asp:Button ID="B6" Text="6" CssClass="tableButton rectangleTable tableRowtwo tableb6" BorderColor="Black" OnClick="SelectTable" runat="server" />
            <div runat="server">
                <asp:Label ID="ServerOfTableB6" CssClass="tableb6 tableButtonLabel" runat="server" /></div>
            <asp:Button ID="B7" Text="7" CssClass="tableButton squareTable tableRowtwo tableb7" BorderColor="Black" OnClick="SelectTable" runat="server" />
            <div runat="server">
                <asp:Label ID="ServerOfTableB7" CssClass="tableb7 tableButtonLabel" runat="server" /></div>
            <asp:Button ID="B8" Text="8" CssClass="tableButton squareTable tableRowtwo tableb8" BorderColor="Black" OnClick="SelectTable" runat="server" />
            <div runat="server">
                <asp:Label ID="ServerOfTableB8" CssClass="tableb8 tableButtonLabel" runat="server" /></div>
            <asp:Button ID="B9" Text="9" CssClass="tableButton circleTable tableRowtwo tableb9" BorderColor="Black" OnClick="SelectTable" runat="server" />
            <div runat="server">
                <asp:Label ID="ServerOfTableB9" CssClass="tableb9 tableButtonLabel" runat="server" /></div>
            <asp:Button ID="B10" Text="10" CssClass="tableButton squareTable tableRowtwo tableb10" BorderColor="Black" OnClick="SelectTable" runat="server" />
            <div runat="server">
                <asp:Label ID="ServerOfTableB10" CssClass="tableb10 tableButtonLabel" runat="server" /></div>
            <asp:Button ID="B11" Text="11" CssClass="tableButton squareTable tableRowtwo tableb11" BorderColor="Black" OnClick="SelectTable" runat="server" />
            <div runat="server">
                <asp:Label ID="ServerOfTableB11" CssClass="tableb11 tableButtonLabel" runat="server" /></div>
            <asp:Button ID="A1" Text="1" CssClass="tableButton squareTable tableRowThree tablea1" BorderColor="Black" OnClick="SelectTable" runat="server" />
            <div runat="server">
                <asp:Label ID="ServerOfTableA1" CssClass="tablea1 tableButtonLabel" runat="server" /></div>
            <asp:Button ID="A2" Text="2" CssClass="tableButton squareTable tableRowThree tablea2" BorderColor="Black" OnClick="SelectTable" runat="server" />
            <div runat="server">
                <asp:Label ID="ServerOfTableA2" CssClass="tablea2 tableButtonLabel" runat="server" /></div>
            <asp:Button ID="A3" Text="3" CssClass="tableButton squareTable tableRowThree tablea3" BorderColor="Black" OnClick="SelectTable" runat="server" />
            <div runat="server">
                <asp:Label ID="ServerOfTableA3" CssClass="tablea3 tableButtonLabel" runat="server" /></div>
            <asp:Button ID="A4" Text="4" CssClass="tableButton squareTable tableRowThree tablea4" BorderColor="Black" OnClick="SelectTable" runat="server" />
            <div runat="server">
                <asp:Label ID="ServerOfTableA4" CssClass="tablea4 tableButtonLabel" runat="server" /></div>
            <asp:Button ID="A5" Text="5" CssClass="tableButton squareTable tableRowThree tablea5" BorderColor="Black" OnClick="SelectTable" runat="server" />
            <div runat="server">
                <asp:Label ID="ServerOfTableA5" CssClass="tablea5 tableButtonLabel" runat="server" /></div>
            <asp:Button ID="A6" Text="6" CssClass="tableButton squareTable tableRowThree tablea6" BorderColor="Black" OnClick="SelectTable" runat="server" />
            <div runat="server">
                <asp:Label ID="ServerOfTableA6" CssClass="tablea6 tableButtonLabel" runat="server" /></div>
            <asp:Button ID="A7" Text="7" CssClass="tableButton squareTable tableRowThree tablea7" BorderColor="Black" OnClick="SelectTable" runat="server" />
            <div runat="server">
                <asp:Label ID="ServerOfTableA7" CssClass="tablea7 tableButtonLabel" runat="server" /></div>
            <asp:Button ID="A8" Text="8" CssClass="tableButton squareTable tableRowThree tablea8" BorderColor="Black" OnClick="SelectTable" runat="server" />
            <div runat="server">
                <asp:Label ID="ServerOfTableA8" CssClass="tablea8 tableButtonLabel" runat="server" /></div>
            <asp:Button ID="A9" Text="9" CssClass="tableButton squareTable tableRowThree tablea9" BorderColor="Black" OnClick="SelectTable" runat="server" />
            <div runat="server">
                <asp:Label ID="ServerOfTableA9" CssClass="tablea9 tableButtonLabel" runat="server" /></div>
            <asp:Button ID="A10" Text="10" CssClass="tableButton circleTable tableRowThree tablea10" BorderColor="Black" OnClick="SelectTable" runat="server" />
            <div runat="server">
                <asp:Label ID="ServerOfTableA10" CssClass="tablea10 tableButtonLabel" runat="server" /></div>
        </div>

        <%--<script src="../Scripts/raphael-min.js"></script>
                 <script src="../Scripts/tablemap.js"></script>--%>


        <%-- Make this only visable when a person clicks on it, if they have not clicked on it or click away, then it disapears--%>
        <div class="col-md-3" style="border-left: solid 1px black; background-color: lightgrey; color: white;">
            <asp:Panel runat="server" ID="UnoccupiedTable" Visible="false">

                <div class="row">
                    <h3>Selected Table Info</h3>
                </div>
                <div>Table Selected:
                    <asp:Label ID="Tablepicked" runat="server"></asp:Label></div>


                <asp:Label runat="server">Select A Server:</asp:Label>
                <%--if a server is already assigned to an occupied table, have it load to the employee it is.--%>
                <asp:DropDownList ID="EmployeeDDL" runat="server" DataSourceID="EmployeeDS" DataTextField="FName" DataValueField="EmployeeID" AutoPostBack="True" ForeColor="Brown">
                </asp:DropDownList>

                <asp:ObjectDataSource runat="server" ID="EmployeeDS" OldValuesParameterFormatString="original_{0}" SelectMethod="List_Employee" TypeName="Shanghai.System.BLL.EmployeeController"></asp:ObjectDataSource>
                <div>
                    <asp:Button ID="TableBillLink" Text="Go to table Bill" runat="server" OnClick="goToBill" /></div>
                <asp:Label Text="Tables occupied" ID="tableOccupied" runat="server" Visible="false" />
            </asp:Panel>




        </div>
    </div>

    <%--list all orders with table number 800/900 that are set to unavaliable to get the orders of takeout and delivery--%>
</asp:Content>
