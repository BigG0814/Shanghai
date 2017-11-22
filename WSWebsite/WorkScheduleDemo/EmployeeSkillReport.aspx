<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="EmployeeSkillReport.aspx.cs" Inherits="WorkScheduleDemo_EmployeeSkillReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <rsweb:ReportViewer ID="EmployeeSkillReportViewer" runat="server" 
        Font-Names="Verdana" 
        Font-Size="8pt" 
        WaitMessageFont-Names="Verdana" 
        WaitMessageFont-Size="14pt" 
        Width="100%">
        <LocalReport ReportPath="Report\EmployeeSkillReport.rdlc">
            <DataSources>
                <rsweb:ReportDataSource Name="EmployeeSkillReport" 
                    DataSourceId="EmployeeSkillReportODS"></rsweb:ReportDataSource>
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>

    <asp:ObjectDataSource runat="server" ID="EmployeeSkillReportODS" 
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="EmployeeSkillReport_Get" 
        TypeName="WorkScheduleSystem.BLL.EmployeeSkillController">
    </asp:ObjectDataSource>
</asp:Content>

