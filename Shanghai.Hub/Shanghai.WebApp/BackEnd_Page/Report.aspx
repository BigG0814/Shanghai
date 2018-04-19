<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/BackEnd.Master" CodeBehind="Report.aspx.cs" Inherits="Shanghai.WebApp.BackEnd_Page.Report" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>



<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .form-control {
            display: inline;
        }

        .eContainer {
            background-color: white;
            width: 95em;
            margin: 0 auto;
        }

        .report {
            margin-left: 1.5em;
        }

      
    </style>
    <div class="body-content eContainer" style="background-color: white;">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="report">
                    <br />
                    <br />
                    <span style="font-weight: bolder; font-family: 'Amatic SC', cursive; font-size: 1.8em;">Employee Name:</span>

                    <asp:DropDownList ID="DropDownList1" CssClass="form-control" Width="9em" runat="server" DataSourceID="ObjectDataSource1"
                        DataTextField="FullName" DataValueField="EmployeeID" AutoPostBack="True" AppendDataBoundItems="true">
                        <asp:ListItem Value="0" Text="All" />
                    </asp:DropDownList>
                    <asp:ObjectDataSource runat="server" ID="ObjectDataSource1" OldValuesParameterFormatString="original_{0}" SelectMethod="List_Employee"
                        TypeName="Shanghai.System.BLL.EmployeeController"></asp:ObjectDataSource>

                    <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Button1_Click" CausesValidation="false" CssClass="btn "
                        Style="width: 130px; background-color: #f6f0b9; font-weight: bolder; font-size: 20px; font-family: 'Amatic SC', cursive;" />


                    <span style="font-weight: bolder; font-family: 'Amatic SC', cursive; font-size: 1.8em;">&nbsp;&nbsp;&nbsp;&nbsp; Select Start Date:</span>
                    <asp:ImageButton TabStop="true" TabIndex="14" ID="CalenderImg" runat="server" ImageUrl="~/images/calender.jpg" Height="20px" OnClick="CalenderImg_Click" Width="20px" />
                    <asp:Calendar Style="margin-left: 435px; z-index: 1; position: absolute;" ID="Calendar1" runat="server" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1"
                        DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="200px" OnSelectionChanged="Calendar1_SelectionChanged"
                        Width="220px">
                        <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                        <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                        <OtherMonthDayStyle ForeColor="#999999" />
                        <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                        <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                        <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
                        <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                        <WeekendDayStyle BackColor="#CCCCFF" />
                    </asp:Calendar>
                    <asp:TextBox ID="HireDTextBox" TabStop="true" TabIndex="14" runat="server" CssClass="form-control" Width="7em"></asp:TextBox>
                    <span style="font-weight: bolder; font-family: 'Amatic SC', cursive; font-size: 1.8em;">&nbsp;&nbsp;&nbsp;&nbsp; Select End Date:</span>
                    <asp:ImageButton TabStop="true" TabIndex="14" ID="ImageButton1" runat="server" ImageUrl="~/images/calender.jpg" Height="20px" OnClick="ImageButton1_Click" Width="20px" />
                    <asp:Calendar Style="margin-left: 693px; z-index: 1; position: absolute;" ID="Calendar2" runat="server" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1"
                        DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="200px" OnSelectionChanged="Calendar2_SelectionChanged"
                        Width="220px">
                        <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                        <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                        <OtherMonthDayStyle ForeColor="#999999" />
                        <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                        <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                        <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
                        <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                        <WeekendDayStyle BackColor="#CCCCFF" />
                    </asp:Calendar>
                    <asp:TextBox ID="TextBox1" TabStop="true" TabIndex="14" runat="server" CssClass="form-control" Width="7em"></asp:TextBox>
                    <asp:Button ID="Button2" runat="server" Text="Submit" OnClick="Button2_Click" CausesValidation="false" CssClass="btn "
                        Style="width: 130px; background-color: #f6f0b9; font-weight: bolder; font-size: 20px; font-family: 'Amatic SC', cursive;" />
                    <br />
                    <br />

                    <%--Sale By Employee --%>


                    <rsweb:ReportViewer ID="EmployeeReport" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="88em">
                        <LocalReport ReportPath="Reports\SaleReportByEmployee.rdlc">
                            <DataSources>
                                <rsweb:ReportDataSource Name="DataSet1" DataSourceId="Salebyemployee"></rsweb:ReportDataSource>
                            </DataSources>
                        </LocalReport>
                    </rsweb:ReportViewer>
                    <asp:ObjectDataSource runat="server" OnDataBinding="Salebyemployee_DataBinding" ID="Salebyemployee" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="getCashOut_byemployee" TypeName="Shanghai.System.BLL.EmployeeReportController">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="DropDownList1" PropertyName="SelectedValue" Name="employeeid" Type="Int32"></asp:ControlParameter>
                        </SelectParameters>
                    </asp:ObjectDataSource>

                  


                    <rsweb:ReportViewer ID="EmployeeReportByDate" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt"
                        Width="90em" Style="margin-top: -400px; position: absolute;">
                        <LocalReport ReportPath="Reports\EmployeeSaleByDate.rdlc">

                            <DataSources>
                                <rsweb:ReportDataSource Name="DataSet1" DataSourceId="SaleByDate"></rsweb:ReportDataSource>
                            </DataSources>
                        </LocalReport>
                    </rsweb:ReportViewer>
                    <asp:ObjectDataSource runat="server" ID="SaleByDate" OldValuesParameterFormatString="original_{0}" SelectMethod="getCashOut_bydate" TypeName="Shanghai.System.BLL.EmployeeReportController">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="HireDTextBox" PropertyName="Text" Name="start" Type="DateTime"></asp:ControlParameter>
                            <asp:ControlParameter ControlID="TextBox1" PropertyName="Text" Name="end" Type="DateTime"></asp:ControlParameter>
                        </SelectParameters>
                    </asp:ObjectDataSource>

               <rsweb:ReportViewer ID="ReportViewer3" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="88em">
                        <LocalReport ReportPath="Reports\Chart Report.rdlc">
                            <DataSources>
                                <rsweb:ReportDataSource Name="DataSet1" DataSourceId="salebyemployeechart"></rsweb:ReportDataSource>
                            </DataSources>
                        </LocalReport>
                    </rsweb:ReportViewer>
                    <asp:ObjectDataSource runat="server" ID="salebyemployeechart" OldValuesParameterFormatString="original_{0}" SelectMethod="getCashOut_byemployee" TypeName="Shanghai.System.BLL.EmployeeReportController">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="DropDownList1" PropertyName="SelectedValue" Name="employeeid" Type="Int32"></asp:ControlParameter>
                        </SelectParameters>
                    </asp:ObjectDataSource>


                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="88em">
                        <LocalReport ReportPath="Reports\TotalSaleReportBy_Date.rdlc">
                            <DataSources>
                                <rsweb:ReportDataSource Name="DataSet1" DataSourceId="incomebydate"></rsweb:ReportDataSource>
                            </DataSources>
                        </LocalReport>
                    </rsweb:ReportViewer>

                    <asp:ObjectDataSource runat="server" ID="incomebydate" OldValuesParameterFormatString="original_{0}" SelectMethod="getincomeby_date" 
                        TypeName="Shanghai.System.BLL.EmployeeReportController"></asp:ObjectDataSource>

                    <rsweb:ReportViewer ID="ReportViewer2" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="88em">
                        <LocalReport ReportPath="Reports\customercountreport.rdlc">
                            <DataSources>
                                <rsweb:ReportDataSource Name="DataSet1" DataSourceId="customercount"></rsweb:ReportDataSource>
                            </DataSources>
                        </LocalReport>
                    </rsweb:ReportViewer>


                    <asp:ObjectDataSource runat="server" ID="customercount" OldValuesParameterFormatString="original_{0}" SelectMethod="customerby_date"
                         TypeName="Shanghai.System.BLL.EmployeeReportController"></asp:ObjectDataSource>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

</asp:Content>