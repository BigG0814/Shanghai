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
                              <asp:GridView ID="EmployeeSkillList" runat="server" AutoGenerateColumns="False" DataSourceID="EmployeeSkillODS" CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True">
                                  <AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
                                  <Columns>
                                      <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name"></asp:BoundField>
                                      <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone"></asp:BoundField>
                                      <asp:CheckBoxField DataField="Active" HeaderText="Active" SortExpression="Active"></asp:CheckBoxField>
                                      <asp:BoundField DataField="SkillLevel" HeaderText="SkillLevel" SortExpression="SkillLevel"></asp:BoundField>
                                      <asp:BoundField DataField="YOE" HeaderText="YOE" SortExpression="YOE"></asp:BoundField>

                                  </Columns>
                                  <EditRowStyle BackColor="#2461BF"></EditRowStyle>

                                  <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"></FooterStyle>

                                  <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"></HeaderStyle>

                                  <PagerStyle HorizontalAlign="Center" BackColor="#2461BF" ForeColor="White"></PagerStyle>

                                  <RowStyle BackColor="#EFF3FB"></RowStyle>

                                  <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>

                                  <SortedAscendingCellStyle BackColor="#F5F7FB"></SortedAscendingCellStyle>

                                  <SortedAscendingHeaderStyle BackColor="#6D95E1"></SortedAscendingHeaderStyle>

                                  <SortedDescendingCellStyle BackColor="#E9EBEF"></SortedDescendingCellStyle>

                                  <SortedDescendingHeaderStyle BackColor="#4870BE"></SortedDescendingHeaderStyle>
                              </asp:GridView>
                              <asp:ObjectDataSource runat="server" ID="EmployeeSkillODS" OldValuesParameterFormatString="original_{0}" SelectMethod="Skills_ListEmployees" TypeName="WorkScheduleSystem.BLL.EmployeeSkillController">
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
                              <asp:ListView ID="ListView1" runat="server" DataSourceID="EmployeeSkillsODS" 
                                  InsertItemPosition="LastItem" DataKeyNames="EmployeeSkillID">

                                  <AlternatingItemTemplate>
                                      <tr style="background-color: #FFF8DC;">
                                          <td>
                                              <asp:Button runat="server" CommandName="Delete" Text="Delete" ID="DeleteButton" />
                                              <asp:Button runat="server" CommandName="Edit" Text="Edit" ID="EditButton" />
                                          </td>
                                          <td>
                                              <asp:Label Text='<%# Eval("EmployeeSkillID") %>' runat="server" 
                                                  ID="EmployeeSkillIDLabel" Width="50px"/></td>
                                  
                                          <td>
                                              <asp:DropDownList ID="DropDownList1" runat="server" 
                                                  DataSourceID="EmployeeODS" 
                                                  DataTextField="FirstName" 
                                                  DataValueField="EmployeeID" 
                                                  SelectedValue='<%# Eval("EmployeeID") %>'
                                                  Enabled="False">
                                              </asp:DropDownList>
                                              
                                          </td>
                                          <td>
                                              <asp:DropDownList ID="DropDownList2" runat="server" 
                                                  DataSourceID="SkillODS" 
                                                  DataTextField="Description" 
                                                  DataValueField="SkillID"
                                                  SelectedValue='<%# Eval("SkillID") %>'
                                                  Enabled="False">

                                              </asp:DropDownList>

                                              
                                          </td>
                                          <td>
                                              <asp:Label Text='<%# Eval("Level") %>' runat="server" ID="LevelLabel" />
                                   
                                          </td>
                                          <td align="center">
                                              <asp:Label Text='<%# Eval("YearsOfExperience") %>' runat="server" ID="YearsOfExperienceLabel" /></td>
                                          <td align="right">
                                              <asp:Label Text='<%# Eval("HourlyWage") %>' runat="server" ID="HourlyWageLabel" /></td>

                                      </tr>
                                  </AlternatingItemTemplate>
                                  <EditItemTemplate>
                                      <tr style="background-color: #008A8C; color: #000000;">
                                          <td>
                                              <asp:Button runat="server" CommandName="Update" Text="Update" ID="UpdateButton" />
                                              <asp:Button runat="server" CommandName="Cancel" Text="Cancel" ID="CancelButton" />
                                          </td>
                                          <td>
                                              <asp:TextBox Text='<%# Bind("EmployeeSkillID") %>' runat="server" 
                                                  ID="EmployeeSkillIDTextBox" Width="50px"/></td>
                                     
                                          <td>
                                              <asp:DropDownList ID="DropDownList1" runat="server" 
                                                  DataSourceID="EmployeeODS" 
                                                  DataTextField="FirstName" 
                                                  DataValueField="EmployeeID" 
                                                  SelectedValue='<%# Bind("EmployeeID") %>'></asp:DropDownList>
                                              
                                          </td>
                                          <td>
                                              <asp:DropDownList ID="DropDownList2" runat="server" 
                                                  DataSourceID="SkillODS" 
                                                  DataTextField="Description" 
                                                  DataValueField="SkillID"
                                                  SelectedValue='<%# Bind("SkillID") %>'></asp:DropDownList>

                                              
                                          </td>
                                          <td>
                                              <asp:TextBox Text='<%# Bind("Level") %>' runat="server" ID="LevelTextBox" /></td>
                                          <td align="center">
                                              <asp:TextBox Text='<%# Bind("YearsOfExperience") %>' runat="server" ID="YearsOfExperienceTextBox" /></td>
                                          <td align="right">
                                              <asp:TextBox Text='<%# Bind("HourlyWage") %>' runat="server" ID="HourlyWageTextBox" /></td>

                                          
                                      </tr>
                                  </EditItemTemplate>
                                  <EmptyDataTemplate>
                                      <table runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;">
                                          <tr>
                                              <td>No data was returned.</td>
                                          </tr>
                                      </table>
                                  </EmptyDataTemplate>
                                  <InsertItemTemplate>
                                      <tr style="">
                                          <td>
                                              <asp:Button runat="server" CommandName="Insert" Text="Insert" ID="InsertButton" />
                                              <asp:Button runat="server" CommandName="Cancel" Text="Clear" ID="CancelButton" />
                                          </td>
                                          <td>
                                              <asp:TextBox Text='<%# Bind("EmployeeSkillID") %>' runat="server" 
                                                  ID="EmployeeSkillIDTextBox" Width="50px" Enabled="false"/></td>
                                          <td>
                                              <asp:DropDownList ID="DropDownList1" runat="server" 
                                                  DataSourceID="EmployeeODS" 
                                                  DataTextField="FirstName" 
                                                  DataValueField="EmployeeID" 
                                                  SelectedValue='<%# Bind("EmployeeID") %>'></asp:DropDownList>
                                              
                                          </td>
                                          <td>
                                              <asp:DropDownList ID="DropDownList2" runat="server" 
                                                  DataSourceID="SkillODS" 
                                                  DataTextField="Description" 
                                                  DataValueField="SkillID"
                                                  SelectedValue='<%# Bind("SkillID") %>'></asp:DropDownList>

                                              
                                          </td>
                                          
                                          <td>
                                              <asp:TextBox Text='<%# Bind("Level") %>' runat="server" ID="LevelTextBox" /></td>
                                          <td align="center">
                                              <asp:TextBox Text='<%# Bind("YearsOfExperience") %>' runat="server" ID="YearsOfExperienceTextBox" /></td>
                                          <td align="right">
                                              <asp:TextBox Text='<%# Bind("HourlyWage") %>' runat="server" ID="HourlyWageTextBox" /></td>

                                          
                                      </tr>
                                  </InsertItemTemplate>
                                  <ItemTemplate>
                                      <tr style="background-color: #DCDCDC; color: #000000;">
                                          <td>
                                              <asp:Button runat="server" CommandName="Delete" Text="Delete" ID="DeleteButton" />
                                              <asp:Button runat="server" CommandName="Edit" Text="Edit" ID="EditButton" />
                                          </td>
                                          <td>
                                              <asp:Label Text='<%# Eval("EmployeeSkillID") %>' runat="server" 
                                                  ID="EmployeeSkillIDLabel" Width="50px"/></td>
                                          <td>
                                              <asp:DropDownList ID="DropDownList1" runat="server" 
                                                  DataSourceID="EmployeeODS" 
                                                  DataTextField="FirstName" 
                                                  DataValueField="EmployeeID" 
                                                  SelectedValue='<%# Eval("EmployeeID") %>'
                                                  Enabled="False">

                                              </asp:DropDownList>
                                              
                                          </td>
                                          <td>
                                              <asp:DropDownList ID="DropDownList2" runat="server" 
                                                  DataSourceID="SkillODS" 
                                                  DataTextField="Description" 
                                                  DataValueField="SkillID"
                                                  SelectedValue='<%# Eval("SkillID") %>'
                                                  Enabled="False">

                                              </asp:DropDownList>

                                              
                                          </td>
                                          <td>
                                              <asp:Label Text='<%# Eval("Level") %>' runat="server" ID="LevelLabel" /></td>
                                          <td align="center">
                                              <asp:Label Text='<%# Eval("YearsOfExperience") %>' runat="server" ID="YearsOfExperienceLabel" /></td>
                                          <td align="right">
                                              <asp:Label Text='<%# Eval("HourlyWage") %>' runat="server" ID="HourlyWageLabel" /></td>

                                          
                                      </tr>
                                  </ItemTemplate>
                                  <LayoutTemplate>
                                      <table runat="server">
                                          <tr runat="server">
                                              <td runat="server">
                                                  <table runat="server" id="itemPlaceholderContainer" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px; font-family: Verdana, Arial, Helvetica, sans-serif;" border="1">
                                                      <tr runat="server" style="background-color: #DCDCDC; color: #000000;">
                                                          <th runat="server"></th>
                                                          <th runat="server">ID</th>
                                                          <th runat="server">Employee</th>
                                                          <th runat="server">Skill</th>
                                                          <th runat="server">Level</th>
                                                          <th runat="server">YearsOfExperience</th>
                                                          <th runat="server">HourlyWage</th>

                                                      </tr>
                                                      <tr runat="server" id="itemPlaceholder"></tr>
                                                  </table>
                                              </td>
                                          </tr>
                                          <tr runat="server">
                                              <td runat="server" style="text-align: center; background-color: #CCCCCC; font-family: Verdana, Arial, Helvetica, sans-serif; color: #000000;">
                                                  <asp:DataPager runat="server" ID="DataPager2">
                                                      <Fields>
                                                          <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                                                          <asp:NumericPagerField></asp:NumericPagerField>
                                                          <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                                                      </Fields>
                                                  </asp:DataPager>
                                              </td>
                                          </tr>
                                      </table>
                                  </LayoutTemplate>
                                  <SelectedItemTemplate>
                                      <tr style="background-color: #008A8C; font-weight: bold; color: #FFFFFF;">
                                          <td>
                                              <asp:Button runat="server" CommandName="Delete" Text="Delete" ID="DeleteButton" />
                                              <asp:Button runat="server" CommandName="Edit" Text="Edit" ID="EditButton" />
                                          </td>
                                          <td>
                                              <asp:Label Text='<%# Eval("EmployeeSkillID") %>' runat="server" 
                                                  ID="EmployeeSkillIDLabel" Width="50px"/></td>
                                          <td>
                                              <asp:DropDownList ID="DropDownList1" runat="server" 
                                                  DataSourceID="EmployeeODS" 
                                                  DataTextField="FirstName" 
                                                  DataValueField="EmployeeID" 
                                                  SelectedValue='<%# Eval("EmployeeID") %>'
                                                  Enabled="False">

                                              </asp:DropDownList>
                                              
                                          </td>
                                          <td>
                                              <asp:DropDownList ID="DropDownList2" runat="server" 
                                                  DataSourceID="SkillODS" 
                                                  DataTextField="Description" 
                                                  DataValueField="SkillID"
                                                  SelectedValue='<%# Eval("SkillID") %>'
                                                  Enabled="False">

                                              </asp:DropDownList>

                                              
                                          </td>
                                          <td>
                                              <asp:Label Text='<%# Eval("Level") %>' runat="server" ID="LevelLabel" /></td>
                                          <td align="center">
                                              <asp:Label Text='<%# Eval("YearsOfExperience") %>' runat="server" ID="YearsOfExperienceLabel" /></td>
                                          <td align="right">
                                              <asp:Label Text='<%# Eval("HourlyWage") %>' runat="server" ID="HourlyWageLabel" /></td>

                                          
                                      </tr>
                                  </SelectedItemTemplate>
                              </asp:ListView>
                              
                              <asp:ObjectDataSource runat="server" ID="SkillODS" 
                                  OldValuesParameterFormatString="original_{0}" 
                                  SelectMethod="Skills_List" 
                                  TypeName="WorkScheduleSystem.BLL.SkillController"></asp:ObjectDataSource>
                              <asp:ObjectDataSource runat="server" ID="EmployeeODS" 
                                  OldValuesParameterFormatString="original_{0}" 
                                  SelectMethod="Employees_List" 
                                  TypeName="WorkScheduleSystem.BLL.EmployeeController"></asp:ObjectDataSource>
                              <asp:ObjectDataSource runat="server" ID="EmployeeSkillsODS"
                                  OldValuesParameterFormatString="original_{0}" SelectMethod="Employees_List"
                                  TypeName="WorkScheduleSystem.BLL.EmployeeSkillController" 
                                  DataObjectTypeName="WorkSchedule.Data.Entities.EmployeeSkills" 
                                  DeleteMethod="EmployeeSkills_Delete" InsertMethod="EmployeeSkills_Add" 
                                  UpdateMethod="EmployeeSkills_Update"></asp:ObjectDataSource>
                          </ContentTemplate>
                      </asp:UpdatePanel>
                  </div>
                  <%--<div class="tab-pane fade in active" id="listviewcrud">
                      <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                          <ContentTemplate>
                              <asp:ListView ID="ListView1" runat="server" DataSourceID="EmployeeSkillsODS" InsertItemPosition="LastItem">
                                  <AlternatingItemTemplate>
                                      <tr style="background-color: #FFFFFF; color: #284775;">
                                          <td>
                                              <asp:Button runat="server" CommandName="Delete" Text="Delete" ID="DeleteButton" />
                                              <asp:Button runat="server" CommandName="Edit" Text="Edit" ID="EditButton" />
                                          </td>
                                          <td>
                                              <asp:Label Text='<%# Eval("EmployeeSkillID") %>' runat="server" ID="EmployeeSkillIDLabel" /></td>
                                          <td>
                                            
                                              <asp:DropDownList ID="DropDownList2" runat="server"
                                                  DataSourceID="EmployeeListODS"
                                                  DataTextField="FirstName"
                                                  DataValueField="EmployeeID"
                                                  SelectedValue='<%# Eval("Employee") %>'
                                                  Enabled="false">
                                              </asp:DropDownList>
                                              <asp:ObjectDataSource runat="server" ID="EmployeeListODS" OldValuesParameterFormatString="original_{0}" SelectMethod="Employees_List" TypeName="WorkScheduleSystem.BLL.EmployeeController"></asp:ObjectDataSource>
                                              
                                          </td>
                                          <td>
                                            
                                              <asp:DropDownList ID="DropDownList1" runat="server"
                                                  DataSourceID="SkillListODS"
                                                  DataTextField="Description"
                                                  DataValueField="SkillID"
                                                  SelectedValue='<%# Eval("Skill") %>'
                                                  Enabled="false">
                                              </asp:DropDownList>
                                              
                                          </td>
                                          <td>
                                              <asp:Label Text='<%# Eval("Level") %>' runat="server" ID="LevelLabel" /></td>
                                          <td>
                                              <asp:Label Text='<%# Eval("YearsOfExperience") %>' runat="server" ID="YearsOfExperienceLabel" /></td>
                                          <td>
                                              <asp:Label Text='<%# Eval("HourlyWage") %>' runat="server" ID="HourlyWageLabel" /></td>
                                      
                                      </tr>
                                  </AlternatingItemTemplate>
                                  <EditItemTemplate>
                                      <tr style="background-color: #999999;">
                                          <td>
                                              <asp:Button runat="server" CommandName="Update" Text="Update" ID="UpdateButton" />
                                              <asp:Button runat="server" CommandName="Cancel" Text="Cancel" ID="CancelButton" />
                                          </td>
                                          <td>
                                              <asp:TextBox Text='<%# Bind("EmployeeSkillID") %>' runat="server" ID="EmployeeSkillIDTextBox" /></td>
                                          <td>
                                            
                                              <asp:DropDownList ID="DropDownList2" runat="server"
                                                  DataSourceID="EmployeeListODS"
                                                  DataTextField="FirstName"
                                                  DataValueField="EmployeeID"
                                                  SelectedValue='<%# Bind("Employee") %>'
                                                  >
                                              </asp:DropDownList>
                                          </td>
                                          <td>
                                            
                                              <asp:DropDownList ID="DropDownList1" runat="server"
                                                  DataSourceID="SkillListODS"
                                                  DataTextField="Description"
                                                  DataValueField="SkillID"
                                                  SelectedValue='<%# Bind("Skill") %>'
                                                  >
                                              </asp:DropDownList>
                                          </td>
                                          <td>
                                              <asp:TextBox Text='<%# Bind("Level") %>' runat="server" ID="LevelTextBox" /></td>
                                          <td>
                                              <asp:TextBox Text='<%# Bind("YearsOfExperience") %>' runat="server" ID="YearsOfExperienceTextBox" /></td>
                                          <td>
                                              <asp:TextBox Text='<%# Bind("HourlyWage") %>' runat="server" ID="HourlyWageTextBox" /></td>
                             
                                      </tr>
                                  </EditItemTemplate>
                                  <EmptyDataTemplate>
                                      <table runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;">
                                          <tr>
                                              <td>No data was returned.</td>
                                          </tr>
                                      </table>
                                  </EmptyDataTemplate>
                                  <InsertItemTemplate>
                                      <tr style="">
                                          <td>
                                              <asp:Button runat="server" CommandName="Insert" Text="Insert" ID="InsertButton" />
                                              <asp:Button runat="server" CommandName="Cancel" Text="Clear" ID="CancelButton" />
                                          </td>
                                          <td>
                                              <asp:TextBox Text='<%# Bind("EmployeeSkillID") %>' runat="server" ID="EmployeeSkillIDTextBox" /></td>
                                          <td>
                                            
                                              <asp:DropDownList ID="DropDownList2" runat="server"
                                                  DataSourceID="EmployeeListODS"
                                                  DataTextField="FirstName"
                                                  DataValueField="EmployeeID"
                                                  SelectedValue='<%# Bind("Employee") %>'
                                                  >
                                              </asp:DropDownList>
                                          </td>
                                          <td>
                                            
                                              <asp:DropDownList ID="DropDownList1" runat="server"
                                                  DataSourceID="SkillListODS"
                                                  DataTextField="Description"
                                                  DataValueField="SkillID"
                                                  SelectedValue='<%# Bind("Skill") %>'
                                                  >
                                              </asp:DropDownList>
                                          </td>
                                          <td>
                                              <asp:TextBox Text='<%# Bind("Level") %>' runat="server" ID="LevelTextBox" /></td>
                                          <td>
                                              <asp:TextBox Text='<%# Bind("YearsOfExperience") %>' runat="server" ID="YearsOfExperienceTextBox" /></td>
                                          <td>
                                              <asp:TextBox Text='<%# Bind("HourlyWage") %>' runat="server" ID="HourlyWageTextBox" /></td>
                
                                      </tr>
                                  </InsertItemTemplate>
                                  <ItemTemplate>
                                      <tr style="background-color: #E0FFFF; color: #333333;">
                                          <td>
                                              <asp:Button runat="server" CommandName="Delete" Text="Delete" ID="DeleteButton" />
                                              <asp:Button runat="server" CommandName="Edit" Text="Edit" ID="EditButton" />
                                          </td>
                                          <td>
                                              <asp:Label Text='<%# Eval("EmployeeSkillID") %>' runat="server" ID="EmployeeSkillIDLabel" /></td>
                                          <td>
                                            
                                              <asp:DropDownList ID="DropDownList2" runat="server"
                                                  DataSourceID="EmployeeListODS"
                                                  DataTextField="FirstName"
                                                  DataValueField="EmployeeID"
                                                  SelectedValue='<%# Eval("Employee") %>'
                                                  Enabled="false">
                                              </asp:DropDownList>
                                          </td>
                                          <td>
                                            
                                              <asp:DropDownList ID="DropDownList1" runat="server"
                                                  DataSourceID="SkillListODS"
                                                  DataTextField="Description"
                                                  DataValueField="SkillID"
                                                  SelectedValue='<%# Eval("Skill") %>'
                                                  Enabled="false">
                                              </asp:DropDownList>
                                          </td>
                                          <td>
                                              <asp:Label Text='<%# Eval("Level") %>' runat="server" ID="LevelLabel" /></td>
                                          <td>
                                              <asp:Label Text='<%# Eval("YearsOfExperience") %>' runat="server" ID="YearsOfExperienceLabel" /></td>
                                          <td>
                                              <asp:Label Text='<%# Eval("HourlyWage") %>' runat="server" ID="HourlyWageLabel" /></td>
                                          
                                      </tr>
                                  </ItemTemplate>
                                  <LayoutTemplate>
                                      <table runat="server">
                                          <tr runat="server">
                                              <td runat="server">
                                                  <table runat="server" id="itemPlaceholderContainer" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px; font-family: Verdana, Arial, Helvetica, sans-serif;" border="1">
                                                      <tr runat="server" style="background-color: #E0FFFF; color: #333333;">
                                                          <th runat="server"></th>
                                                          <th runat="server">EmployeeSkillID</th>
                                                          <th runat="server">Employee</th>
                                                          <th runat="server">Skill</th>
                                                          <th runat="server">Level</th>
                                                          <th runat="server">YearsOfExperience</th>
                                                          <th runat="server">HourlyWage</th>
                                   
                                                      </tr>
                                                      <tr runat="server" id="itemPlaceholder"></tr>
                                                  </table>
                                              </td>
                                          </tr>
                                          <tr runat="server">
                                              <td runat="server" style="text-align: center; background-color: #5D7B9D; font-family: Verdana, Arial, Helvetica, sans-serif; color: #FFFFFF">
                                                  <asp:DataPager runat="server" ID="DataPager1">
                                                      <Fields>
                                                          <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                                                          <asp:NumericPagerField></asp:NumericPagerField>
                                                          <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                                                      </Fields>
                                                  </asp:DataPager>
                                              </td>
                                          </tr>
                                      </table>
                                  </LayoutTemplate>
                                  <SelectedItemTemplate>
                                      <tr style="background-color: #E2DED6; font-weight: bold; color: #333333;">
                                          <td>
                                              <asp:Button runat="server" CommandName="Delete" Text="Delete" ID="DeleteButton" />
                                              <asp:Button runat="server" CommandName="Edit" Text="Edit" ID="EditButton" />
                                          </td>
                                          <td>
                                              <asp:Label Text='<%# Eval("EmployeeSkillID") %>' runat="server" ID="EmployeeSkillIDLabel" /></td>
                                          <td>
                                            
                                              <asp:DropDownList ID="DropDownList2" runat="server"
                                                  DataSourceID="EmployeeListODS"
                                                  DataTextField="FirstName"
                                                  DataValueField="EmployeeID"
                                                  SelectedValue='<%# Eval("Employee") %>'
                                                  Enabled="false">
                                              </asp:DropDownList>
                                          </td>
                                          <td>
                                            
                                              <asp:DropDownList ID="DropDownList1" runat="server"
                                                  DataSourceID="SkillListODS"
                                                  DataTextField="Description"
                                                  DataValueField="SkillID"
                                                  SelectedValue='<%# Eval("Skill") %>'
                                                  Enabled="false">
                                              </asp:DropDownList>
                                              
                                          </td>
                                          <td>
                                              <asp:Label Text='<%# Eval("Level") %>' runat="server" ID="LevelLabel" /></td>
                                          <td>
                                              <asp:Label Text='<%# Eval("YearsOfExperience") %>' runat="server" ID="YearsOfExperienceLabel" /></td>
                                          <td>
                                              <asp:Label Text='<%# Eval("HourlyWage") %>' runat="server" ID="HourlyWageLabel" /></td>
                                
                                      </tr>
                                  </SelectedItemTemplate>
                              </asp:ListView>
                              <asp:ObjectDataSource runat="server" ID="EmployeeSkillsODS" DataObjectTypeName="WorkSchedule.Data.Entities.EmployeeSkills" DeleteMethod="EmployeeSkills_Delete" InsertMethod="EmployeeSkills_Add" OldValuesParameterFormatString="original_{0}" SelectMethod="Employees_List" TypeName="WorkScheduleSystem.BLL.EmployeeSkillController" UpdateMethod="EmployeeSkills_Update"></asp:ObjectDataSource>
                          </ContentTemplate>
                      </asp:UpdatePanel>
                  </div>--%>
              </div>
          </div>
      </div>

</asp:Content>

