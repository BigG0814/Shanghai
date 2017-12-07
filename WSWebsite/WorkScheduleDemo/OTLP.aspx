<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="OTLP.aspx.cs" Inherits="WorkScheduleDemo_OTLP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div>
        <h1>Employee Skill Registration</h1>
    </div>
    <div class="row">
        <div class="col-sm-2">
            <asp:Label ID="Label1" runat="server" Text="First Name "></asp:Label>
            <asp:TextBox ID="FirstNameBox" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="Label2" runat="server" Text="Last Name "></asp:Label>
            <asp:TextBox ID="LastNameBox" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="Label3" runat="server" Text="Home Phone "></asp:Label>
            <asp:TextBox ID="HomePhoneBox" runat="server"></asp:TextBox>
            <br />
        </div>
        <div class="col-sm-10">
            <asp:LinkButton ID="register" runat="server" OnClick="register_Click">Register</asp:LinkButton>
            <br />
            <asp:LinkButton ID="cancel" runat="server" OnClick="cancel_Click">Clear</asp:LinkButton>
        </div>
        <div>
            <asp:ListView ID="EmployeeSkillRegistrationListView" runat="server" DataSourceID="SkillSetODS">
                <AlternatingItemTemplate>
                    <tr style="background-color: #FFF8DC;">
                        <td>
                            <asp:CheckBox ID="SkillCheckBox1" runat="server"  />
                            <asp:Label Text='<%# Eval("SkillId") %>' runat="server" ID="SkillIdLabel" Visible="False" />
                            <asp:Label Text='<%# Eval("SkillName") %>' runat="server" ID="SkillNameLabel" />
                        </td>
                            
                        <td>
                            <asp:RadioButtonList ID="LevelRadioButtonList1" runat="server"
                                RepeatDirection="Horizontal" RepeatLayout="Flow">
                                <asp:ListItem Value="1">Novice</asp:ListItem>
                                <asp:ListItem Value="2">Proficient</asp:ListItem>
                                <asp:ListItem Value="3">Expert</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>

                        <td>
                            <asp:TextBox ID="YearTextBox1" runat="server"></asp:TextBox>
                        </td>
                         <td>
                            <asp:TextBox ID="WageTextBox1" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </AlternatingItemTemplate>

                <EmptyDataTemplate>
                    <table runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;">
                        <tr>
                            <td>No data was returned.</td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                
                <ItemTemplate>
                    <tr style="background-color: #DCDCDC; color: #000000;">
                         <td>
                            <asp:CheckBox ID="SkillCheckBox1" runat="server"  />
                            <asp:Label Text='<%# Eval("SkillId") %>' runat="server" ID="SkillIdLabel" Visible="False" />
                            <asp:Label Text='<%# Eval("SkillName") %>' runat="server" ID="SkillNameLabel" />
                        </td>
                        <td>
                            <asp:RadioButtonList ID="LevelRadioButtonList1" runat="server"
                                RepeatDirection="Horizontal" RepeatLayout="Flow">
                                <asp:ListItem Value="1">Novice</asp:ListItem>
                                <asp:ListItem Value="2">Proficient</asp:ListItem>
                                <asp:ListItem Value="3">Expert</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>

                        <td>
                            <asp:TextBox ID="YearTextBox1" runat="server"></asp:TextBox>
                        </td>
                         <td>
                            <asp:TextBox ID="WageTextBox1" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </ItemTemplate>
                <LayoutTemplate>
                    <table runat="server">
                        <tr runat="server">
                            <td runat="server">
                                <table runat="server" id="itemPlaceholderContainer" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px; font-family: Verdana, Arial, Helvetica, sans-serif;" border="1">
                                    <tr runat="server" style="background-color: #DCDCDC; color: #000000;">
                                        <th runat="server">SkillName</th>
                                        <th runat="server">Level</th>
                                        <th runat="server">YOE</th>
                                        <th runat="server">Hourly Wage</th>
                                    </tr>
                                    <tr runat="server" id="itemPlaceholder"></tr>
                                </table>
                            </td>
                        </tr>
                        <tr runat="server">
                            <td runat="server" style="text-align: center; background-color: #CCCCCC; font-family: Verdana, Arial, Helvetica, sans-serif; color: #000000;">
                                <asp:DataPager runat="server" ID="DataPager1">
                                    <Fields>
                                        <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowLastPageButton="True"></asp:NextPreviousPagerField>
                                    </Fields>
                                </asp:DataPager>
                            </td>
                        </tr>
                    </table>
                </LayoutTemplate>
                
            </asp:ListView>
            <asp:ObjectDataSource ID="SkillSetODS" runat="server" 
                OldValuesParameterFormatString="original_{0}" 
                SelectMethod="Skill_List" 
                TypeName="WorkScheduleSystem.BLL.SkillController"></asp:ObjectDataSource>
        </div>
    </div>
</asp:Content>

