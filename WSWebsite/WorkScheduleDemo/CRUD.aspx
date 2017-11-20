<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CRUD.aspx.cs" Inherits="WorkScheduleDemo_WorkSchdule" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="row jumbotron">
        <h1>Tabbed Work Schedule</h1>
    </div>
      <div class="row">
          <div class="col-md-12">
              <ul class="nav nav-tabs">
                  <li class="active"><a href="#search" data-toggle="tab">Lookup</a></li>
                  <li><a href="#listviewcrud" data-toggle="tab">ListView Crud</a></li>
              </ul>

              <div class="tab-content">
                  <div class="tab-pane fade" id="search">
                      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                          <ContentTemplate>
                              <h1> Employees for Skills</h1>
                              <asp:Label ID="Label1" runat="server" Text="Select an Skill:"></asp:Label>
                              <asp:DropDownList ID="SkillList" runat="server" DataSourceID="SkillListODS" DataTextField="Description" DataValueField="SkillID"></asp:DropDownList>
                              <asp:ObjectDataSource runat="server" ID="SkillListODS" OldValuesParameterFormatString="original_{0}" SelectMethod="Skills_List" TypeName="WorkScheduleSystem.BLL.SkillController"></asp:ObjectDataSource>
                              <asp:Button ID="Search" runat="server" Text="Button" />
                              <br />
                              <asp:GridView ID="EmployeeSkillList" runat="server" AutoGenerateColumns="False" DataSourceID="EmployeeSkillListODS" AllowPaging="True" AllowCustomPaging="False">
                                  <%--<Columns>
                                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name"></asp:BoundField>
                                    <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone"></asp:BoundField>
                                    <asp:CheckBoxField DataField="Active" HeaderText="Active" ReadOnly="true" />
                                    <asp:BoundField DataField="SkillLevel" HeaderText="SkillLevel" SortExpression="SkillLevel"></asp:BoundField>
                                    <asp:BoundField DataField="YOE" HeaderText="YOE" SortExpression="YOE"></asp:BoundField>
                                </Columns>--%>
                              </asp:GridView>
                              <asp:ObjectDataSource runat="server" ID="EmployeeSkillListODS" OldValuesParameterFormatString="original_{0}" SelectMethod="Skills_ListEmployees" TypeName="WorkScheduleSystem.BLL.EmployeeSkillController">
                                  <SelectParameters>
                                      <asp:ControlParameter ControlID="SkillList" PropertyName="SelectedValue" DefaultValue="0" Name="skillid" Type="Int32"></asp:ControlParameter>
                                  </SelectParameters>
                              </asp:ObjectDataSource>
                          </ContentTemplate>
                      </asp:UpdatePanel>
                  </div>
                  <div class="tab-pane fade in active" id="listviewcrud">
                      <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                          <ContentTemplate>
                              <asp:ListView ID="ListView1" runat="server"
                                  DataSourceID="EmployeeSkillsODS" InsertItemPosition="LastItem">
                                  
                              </asp:ListView>
                              <asp:ObjectDataSource runat="server" ID="EmployeeSkillsODS" OldValuesParameterFormatString="original_{0}" SelectMethod="Employees_List" TypeName="WorkScheduleSystem.BLL.EmployeeSkillController" DataObjectTypeName="WorkSchedule.Data.Entities.EmployeeSkills" DeleteMethod="EmployeeSkills_Delete" InsertMethod="EmployeeSkills_Add" UpdateMethod="EmployeeSkills_Update"></asp:ObjectDataSource>
                          </ContentTemplate>
                      </asp:UpdatePanel>
                  </div>
              </div>
          </div>
      </div>

</asp:Content>

