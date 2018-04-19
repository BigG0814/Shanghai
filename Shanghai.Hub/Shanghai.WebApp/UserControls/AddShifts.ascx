<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddShifts.ascx.cs" Inherits="Shanghai.WebApp.UserControls.AddShifts" %>

<script>
    function showPopUp(){
        $("#myModal").modal("show");
    }
</script>

<%--<asp:DropDownList runat="server" ID="JobTypeDDL" DataSourceID="CategoryDDLOS" DataTextField="Description" DataValueField="JobTypeID"></asp:DropDownList>--%>

<div class="row">
    <div class="col-md-6">
        <asp:Label Text="" ID="DateLabel" runat="server" />
    </div>
    <div class="col-md-6">
        <asp:Label Text="Job Type: " runat="server" /></div>
</div>
<div class="row">
    <div class="col-md-6">
        <asp:DropDownList runat="server" ID="EmployeeDDL" CssClass="form-control" DataTextField="FullName" DataValueField="EmployeeID" DataSourceID="EmpODs"></asp:DropDownList>
        <asp:ObjectDataSource runat="server" ID="EmpODs" OldValuesParameterFormatString="original_{0}" SelectMethod="List_Employee" TypeName="Shanghai.System.BLL.EmployeeController"></asp:ObjectDataSource>
        <br />
    </div>
    <div class="col-md-6">
        <asp:Label Text="" ID="JobTypeLabel" runat="server" CssClass="form-control" /></div>
</div>
<div class="row">
    <div class="col-md-6">
        <asp:Label Text="Start Time: " runat="server" /><asp:TextBox ID="StartTimeTB" CssClass="form-control" runat="server" TextMode="Time" />
    </div>
    <div class="col-md-6">
        <asp:Label Text="End Time: " runat="server" /><asp:TextBox ID="EndTimeTB" CssClass="form-control" runat="server" TextMode="Time" />
    </div>

</div>
<div class="row">
    <div class="col-md-6">
        <asp:Label ID="MessageLabel" runat="server" ForeColor="Red" />
    </div>
</div>
<asp:HiddenField runat="server" ID="JobIDHdnField" Value="" />



<%--<asp:ObjectDataSource runat="server" ID="CategoryDDLOS" OldValuesParameterFormatString="original_{0}" SelectMethod="Job_List" TypeName="Shanghai.System.BLL.ShiftController"></asp:ObjectDataSource>--%>
