<%@ Page Title="" Language="C#" MasterPageFile="~/BackEnd.Master" AutoEventWireup="true" CodeBehind="Add_Employee.aspx.cs" Inherits="Shanghai.WebApp.BackEnd_Page.Add_Employee" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="body-content" style="background-color: white;">
        <style>
            .eContainer {
                background-color: white;
                margin: 0 auto;
            }

            h1 {
                text-align: center;
                font-family: 'Amatic SC', cursive;
            }

            .tempcss {
                margin-left: 0.5em;
                margin-right: 0.5em;
                left: 15em;
            }

            .jumbotron {
                background-color: #eadc5f;
            }

            .a {
                width: 22.5em;
                margin: 0 auto;
                text-align: center;
                text-decoration: none;
            }

            .l {
                padding-right: 1em;
            }

            .space {
                margin-left: 4em;
            }

            .form-control {
                display: inline;
            }

            .pageNav {
                font-family: 'Amatic SC', cursive;
                font-weight: bolder;
                font-size: 2em;
            }

            .emp {
                text-align: left;
                padding: 0 0.5em;
                clear: both;
                margin-top: 1.5em;
                max-width: 64em;
            }

            .pushRight {
                margin-left: 15px;
            }
        </style>

        <div class="container eContainer">

            <div class="jumbotron">
                <h1>Add & Edit Employee Information</h1>
            </div>

            <div class="tab">
                <%-- <div class="col-md-12">--%>
                <ul class="nav nav-tabs pageNav">
                    <li class="active"><a href="#insertemployee" data-toggle="tab">Add Employee</a></li>
                    <li><a href="#editemployee" data-toggle="tab">Edit Employee</a></li>
                </ul>

                <div class="tab-content display">
                    <!-- user tab -->
                    <%--  <div class="a">--%>
                    <div class="tab-pane fade in active" id="insertemployee">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <uc1:MessageUserControl runat="server" ID="MessageUserControl" />

                                <div class="emp col-md-offset-3">
                                    <div class="form-group col-md-5 ">
                                        <label for="formGroupExampleInput">*First Name</label>
                                        <asp:TextBox TabStop="true" TabIndex="1" ID="FNameTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-5">
                                        <label for="formGroupExampleInput2">Last Name</label>
                                        <asp:TextBox TabStop="true" TabIndex="2" ID="LNameTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-5">
                                        <label for="formGroupExampleInput2">*User Name</label>
                                        <asp:TextBox TabStop="true" TabIndex="3" runat="server" ID="UserNameTextBox" CssClass="form-control" />
                                    </div>
                                    <div class="form-group col-md-5">
                                        <label for="formGroupExampleInput2">*Address</label>
                                        <asp:TextBox TabStop="true" TabIndex="4" ID="AddressTextBox" runat="server" CssClass="form-control"></asp:TextBox><br />
                                    </div>
                                    <div class="form-group col-md-5">
                                        <label for="formGroupExampleInput2">City</label>
                                        <asp:TextBox TabStop="true" TabIndex="5" ID="CityTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-5">
                                        <label for="formGroupExampleInput2">Province</label>
                                        <asp:TextBox TabStop="true" TabIndex="6" ID="ProvinceTextBox" runat="server" CssClass="form-control" placeholder="AB"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-md-5">
                                        <label for="formGroupExampleInput2">Country</label>
                                        <asp:TextBox TabStop="true" TabIndex="7" ID="CountryTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-5">
                                        <label for="formGroupExampleInput2">*Postal Code</label>
                                        <asp:TextBox TabStop="true" TabIndex="8" ID="POTextBox" runat="server" CssClass="form-control" placeholder="ex:X0X 0X0"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-md-5">
                                        <label for="formGroupExampleInput2">*Role</label>
                                        <asp:DropDownList TabStop="true" TabIndex="9" runat="server" ID="RoleDDL" CssClass="form-control" Style="width: 280px" DataSourceID="RolesODS"
                                            DataTextField="RoleName" DataValueField="RoleId" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">[Choose a role]</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <asp:ObjectDataSource runat="server" ID="RolesODS" DataObjectTypeName="Shanghai.Security.Entities.RoleProfile" DeleteMethod="RemoveRole"
                                        InsertMethod="AddRole" OldValuesParameterFormatString="original_{0}" SelectMethod="ListAllRoles" TypeName="Shanghai.Security.BLL.RoleManager"></asp:ObjectDataSource>

                                    <div class="form-group col-md-5">
                                        <label for="formGroupExampleInput2">Email</label>
                                        <asp:TextBox TabStop="true" TabIndex="10" ID="EmailTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-md-5">
                                        <label for="formGroupExampleInput2">*Phone Number</label><br />
                                        <asp:TextBox TabStop="true" TabIndex="11" MaxLength="3" ID="CellPhoneTextBox1" runat="server" CssClass="form-control" Width="70" />
                                        <asp:TextBox TabStop="true" TabIndex="12" MaxLength="3" ID="CellPhoneTextBox2" runat="server" CssClass="form-control" Width="70" />
                                        <asp:TextBox TabStop="true" TabIndex="13" MaxLength="4" ID="CellPhoneTextBox3" runat="server" CssClass="form-control" Width="140" />
                                    </div>

                                    <div class="form-group col-md-5">
                                        <label for="formGroupExampleInput2">Cellular Provider (required for managers)</label><br />
                                        <asp:DropDownList runat="server" ID="CellularProviderDDL" CssClass="form-control" DataSourceID="CellularProvidersODS" DataTextField="Name" DataValueField="CellularProviderID" Style="width: 280px"
                                            AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">[Choose a Provider]</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <asp:ObjectDataSource ID="CellularProvidersODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ListCellularProviders" TypeName="Shanghai.System.BLL.EmployeeController"></asp:ObjectDataSource>

                                    <div class="form-group col-md-5">
                                        <label for="formGroupExampleInput2">*Hire Date</label>
                                        <asp:ImageButton TabStop="true" TabIndex="14" ID="CalenderImg" runat="server" ImageUrl="~/images/calender.jpg" Height="20px" OnClick="CalenderImg_Click" Width="20px" />
                                        <asp:Calendar Style="float: left;" ID="Calendar1" runat="server" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1"
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
                                        <asp:TextBox ID="HireDTextBox" TabStop="true" TabIndex="14" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-md-5">
                                    </div>

                                    <%-- Contact Info --%>
                                    <div class="form-group col-md-10">
                                        <fieldset class="form-horizontal" style="font-family: 'Amatic SC', cursive; font-weight: bold;">
                                            <legend style="font-size: 27px;">Emergency Contact</legend>
                                        </fieldset>
                                    </div>
                                    <div class="form-group col-md-5">
                                        <label for="formGroupExampleInput2">*Contact Name</label>
                                        <asp:TextBox TabStop="true" TabIndex="15" ID="ContactNameTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-5">
                                        <label for="formGroupExampleInput2">*Relationship</label>
                                        <asp:TextBox TabStop="true" TabIndex="16" ID="CRelationTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-5">
                                        <label for="formGroupExampleInput2">*Contact Number</label><br />
                                        <asp:TextBox TabStop="true" TabIndex="17" MaxLength="3" ID="CNumber1" runat="server" CssClass="form-control" Width="70" />
                                        <asp:TextBox TabStop="true" TabIndex="18" MaxLength="3" ID="CNumber2" runat="server" CssClass="form-control" Width="70" />
                                        <asp:TextBox TabStop="true" TabIndex="19" MaxLength="4" ID="CNumber3" runat="server" CssClass="form-control" Width="140" />
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <div class="form-group col-md-12 btn">
                            <br />
                            <br />
                            <asp:Button ID="Button1" runat="server" OnClick="submit_Click" Text="Submit" CssClass="btn  btn-success" Style="width: 150px; font-weight: bolder;" />
                            <asp:Button ID="Button2" runat="server" OnClick="clear_Click" Text="Clear" CssClass="btn" Style="width: 150px; font-weight: bolder;" />
                        </div>
                    </div>



                    <div class="tab-pane fade" id="editemployee">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>

                                <div class="emp col-md-offset-3">
                                    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control pushRight" DataSourceID="ObjectDataSource1" DataTextField="FullName" DataValueField="EmployeeID"
                                        AutoPostBack="True" Width="266px" AppendDataBoundItems="true">
                                        <asp:ListItem Value="0">[Choose an Employee]</asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;

                               <asp:Button ID="Button3" runat="server" Text="Search" OnClick="search" CausesValidation="false" CssClass="btn btn-info" Style="width: 130px;" />&nbsp;

                                    <asp:ObjectDataSource runat="server" ID="ObjectDataSource1" OldValuesParameterFormatString="original_{0}" SelectMethod="List_Employee"
                                        TypeName="Shanghai.System.BLL.EmployeeController"></asp:ObjectDataSource>

                                    <uc1:MessageUserControl runat="server" ID="MessageUserControl1" />

                                    <asp:Label ID="Label1" runat="server" Visible="false"></asp:Label>
                                    <br />
                                    <br />
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:Label Visible="false" Text="POS Login Code: " ID="codePrompt" Font-Size="Large" Font-Bold="true" runat="server" />
                                            <asp:Label Visible="false" Text="" Font-Bold="true" Font-Size="Large" ID="EmployeeLoginCode" runat="server" />
                                        </div>
                                    </div>
                                    <div class="form-group col-md-5 ">
                                        <label for="formGroupExampleInput">*First Name</label>
                                        <asp:TextBox ID="editFname" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-md-5">
                                        <label for="formGroupExampleInput2">Last Name</label>
                                        <asp:TextBox ID="editLname" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-5">
                                        <label for="formGroupExampleInput2">*User Name</label>
                                        <asp:TextBox runat="server" ID="editUname" CssClass="form-control" />
                                    </div>
                                    <div class="form-group col-md-5">
                                        <label for="formGroupExampleInput2">*Address</label>
                                        <asp:TextBox ID="editAddress" runat="server" CssClass="form-control"></asp:TextBox><br />
                                    </div>
                                    <div class="form-group col-md-5">
                                        <label for="formGroupExampleInput2">City</label>
                                        <asp:TextBox ID="editCity" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-5">
                                        <label for="formGroupExampleInput2">Province</label>
                                        <asp:TextBox ID="editProvince" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-md-5">
                                        <label for="formGroupExampleInput2">Country</label>
                                        <asp:TextBox ID="editCountry" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-5">
                                        <label for="formGroupExampleInput2">*Postal Code</label>
                                        <asp:TextBox ID="editPC" runat="server" CssClass="form-control" placeholder="ex:X0X 0X0"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-md-5">
                                        <label for="formGroupExampleInput2">*Role</label>
                                        <asp:DropDownList runat="server" ID="DropDownList2" CssClass="form-control" Style="width: 280px" DataSourceID="RolesODS"
                                            DataTextField="RoleName" DataValueField="RoleId" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">[Choose a role]</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-md-5">
                                        <label for="formGroupExampleInput2">Email</label>
                                        <asp:TextBox ID="editEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-5">
                                        <label for="formGroupExampleInput2">*Phone Number</label>
                                        <asp:TextBox ID="editPhone" runat="server" CssClass="form-control" placeholder="ex:780-888-8888"></asp:TextBox>
                                    </div>

                                    <asp:ObjectDataSource runat="server" ID="ObjectDataSource2" DataObjectTypeName="Shanghai.Security.Entities.RoleProfile" DeleteMethod="RemoveRole"
                                        InsertMethod="AddRole" OldValuesParameterFormatString="original_{0}" SelectMethod="ListAllRoles" TypeName="Shanghai.Security.BLL.RoleManager"></asp:ObjectDataSource>

                                    <div class="form-group col-md-5">
                                        <label for="formGroupExampleInput2">*Hire Date</label>
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/calender.jpg" Height="20px" OnClick="CalenderImg1_click" Width="20px" />
                                        <asp:Calendar Style="float: left;" ID="Calendar2" runat="server" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1"
                                            DayNameFormat="Shortest"
                                            Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="200px" OnSelectionChanged="Calendar2_SelectionChanged" Width="220px">
                                            <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                                            <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                                            <OtherMonthDayStyle ForeColor="#999999" />
                                            <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                            <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                                            <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
                                            <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                                            <WeekendDayStyle BackColor="#CCCCFF" />
                                        </asp:Calendar>
                                        <asp:TextBox ID="editHiredate" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-5" style="height: 64px;">
                                        <label for="formGroupExampleInput2">Mark employee as inactive:&nbsp;&nbsp;</label>
                                        <asp:CheckBox runat="server" ID="editActiveYN" />
                                    </div>

                                    <%-- Emergency Contact Info --%>
                                    <div class="form-group col-md-10">
                                        <fieldset class="form-horizontal" style="font-family: 'Amatic SC', cursive; font-weight: bold;">
                                            <legend style="font-size: 27px;">Emergency Contact</legend>
                                        </fieldset>
                                    </div>
                                    <div class="form-group col-md-5">
                                        <label for="formGroupExampleInput2">*Contact Name</label>
                                        <asp:TextBox ID="editContactname" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-5">
                                        <label for="formGroupExampleInput2">*Relationship</label>
                                        <asp:TextBox ID="editRelationship" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-5">
                                        <label for="formGroupExampleInput2">*Contact Number</label>
                                        <asp:TextBox ID="editContactnumber" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-md-8 col-md-offset-2" style="margin-top: 20px; margin-bottom: 20px;">
                                        <asp:Button ID="Button4" runat="server" Text="Update" OnClick="UpdateEmployee_Click" CssClass="btn btn-success" Style="width: 130px; margin-right: 43px; margin-left: 20px;" />&nbsp;
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>


                    </div>

                </div>
            </div>
        </div>


    </div>

</asp:Content>
