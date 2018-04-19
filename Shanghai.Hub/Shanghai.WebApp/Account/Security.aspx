<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Security.aspx.cs" Inherits="Security_Security" %>

<%@ Register Src="~/UserControl/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="row jumbotron">
        <h1>Site Administration</h1>
    </div>
    <div class="row">
        <div class="col-md-12">
            <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
            <!-- Nav tabs via Bootstrap -->
            <ul class="nav nav-tabs">
                <li class="active"><a href="#users" data-toggle="tab">Users</a></li>
                <li><a href="#roles" data-toggle="tab">Security Roles</a></li>
                <li><a href="#unregistered" data-toggle="tab">Unassigned Users</a></li>
            </ul>
            <div class="tab-content">
                <div id="users" class="tab-pane fade in active">
                    <h3>Registered Users in the Site</h3>
                    <asp:ListView ID="UsersListView" runat="server"
                         DataSourceID="UsersDataSource"
                         DataKeyNames="UserId"
                         ItemType="ebike.Security.Entities.UserProfile"
                         OnItemInserting="UserListView_ItemInserting"
                         InsertItemPosition="FirstItem">
                        <LayoutTemplate>
                            <div class="row bg-info">
                                <div class="col-sm-2 h4">Action</div>
                                <div class="col-sm-2 h4">User Name</div>
                                <div class="col-sm-5 h4">Profile</div>
                                <div class="col-sm-3 h4">Roles</div>
                            </div>
                            <div runat="server" id="itemPlaceholder"></div>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <div class="row">
                                <div class="col-sm-2">
                                    <asp:LinkButton runat="server" CommandName="Delete" Text="Delete" ID="DeleteButton" />
                                </div>
                                <div class="col-sm-2">
                                    <%# Item.UserName %>
                                </div>
                                <div class="col-sm-5">
                                    <%# Item.FullName %>
                                    <br />
                                    Email: <%# string.IsNullOrEmpty(Item.Email) ? "-no email-" : Item.Email %>
                                    <asp:CheckBox ID="IsConfirmed" runat="server"
                                         Enabled="false" Checked="<%# Item.EmailConfirmed %>"
                                         Text="Confirmed" />
                                    <br />
                                    <%# Item.EmployeeId.HasValue ? "Employee"
                                        : !Item.CustomerId.HasValue ? "Customer"
                                        : "-unassigned-" %>
                                    <br />
                                </div>
                                <div class="col-sm-3">
                                    <asp:Repeater ID="RoleUserRepeater" runat="server"
                                            DataSource="<%# Item.RoleMemberships %>" ItemType="System.String">
                                        <ItemTemplate><%# Item %></ItemTemplate>
                                        <SeparatorTemplate>, </SeparatorTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </ItemTemplate>
                        <InsertItemTemplate>
                            <div class="row">
                                <div class="col-sm-2">
                                    <asp:LinkButton runat="server" CommandName="Insert" Text="Insert" ID="InsertButton" />
                                    <asp:LinkButton runat="server" CommandName="Cancel" Text="Clear" ID="CancelButton" />
                                </div>
                                <div class="col-sm-2">
                                    <asp:TextBox Text='<%# BindItem.UserName %>' runat="server" ID="UserNameTextBox" placeholder="User Name" />
                                </div>
                                <div class="col-sm-5">
                                    <asp:TextBox Text='<%# BindItem.Email %>' runat="server" ID="EmailTextBox" placeholder="Email" TextMode="Email" />
                                </div>
                                <div class="col-sm-3">
                                    <asp:CheckBoxList ID="RoleMemberships" runat="server" DataSource="<%# GetRoleNames() %>"></asp:CheckBoxList>
                                </div>
                            </div>
                            <hr />
                        </InsertItemTemplate>
                    </asp:ListView>
                    <asp:ObjectDataSource runat="server" ID="UsersDataSource"
                        OldValuesParameterFormatString="original_{0}"
                        TypeName="ebike.Security.BLL.UserManager"
                        DataObjectTypeName="ebike.Security.Entities.UserProfile"
                        SelectMethod="ListAllUsers"
                        InsertMethod="AddUser"
                        DeleteMethod="RemoveUser"
                        OnInserted="CheckForException"
                        OnDeleted="CheckForException"></asp:ObjectDataSource>
                </div>

                <div id="roles" class="tab-pane fade in">
                    <blockquote>Security Roles for the Site</blockquote>
                <asp:ListView ID="RoleListView" runat="server" DataSourceID="RoleDataSource" 
                         InsertItemPosition="LastItem" DataKeyNames="RoleId" 
                         ItemType="ebike.Security.Entities.RoleProfile"
                         OnItemDeleted="RefreshAll" OnItemInserted="RefreshAll">
                        <EmptyDataTemplate>
                            <span>No security roles have been set up.</span>
                        </EmptyDataTemplate>
                        <LayoutTemplate>
                            <div class="row bg-info">
                                <div class="col-sm-3 h4">Action</div>
                                <div class="col-sm-3 h4">Role</div>
                                <div class="col-sm-6 h4">Members</div>
                            </div>
                            <div runat="server" id="itemPlaceholder"></div>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <div class="row">
                                <div class="col-sm-3"><asp:LinkButton runat="server" CommandName="Delete" Text="Delete" ID="DeleteButton" /></div>
                                <div class="col-sm-3">
                                    <%# Item.RoleName %>
                                </div>
                                <div class="col-sm-6">
                                    <asp:Repeater ID="RoleUserRepeater" runat="server"
                                            DataSource="<%# Item.UserNames %>" ItemType="System.String">
                                        <ItemTemplate><%# Item %></ItemTemplate>
                                        <SeparatorTemplate>, </SeparatorTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </ItemTemplate>
                        <InsertItemTemplate>
                            <hr />
                            <div class="row">
                                <div class="col-sm-3">
                                    <asp:LinkButton runat="server" CommandName="Insert" Text="Insert" ID="InsertButton" />
                                    <asp:LinkButton runat="server" CommandName="Cancel" Text="Clear" ID="CancelButton" />
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox Text='<%# BindItem.RoleName %>' runat="server" ID="RoleNameTextBox" placeholder="Role Name" />
                                </div>
                            </div>
                        </InsertItemTemplate>
                    </asp:ListView>
                    <asp:ObjectDataSource runat="server" ID="RoleDataSource" DataObjectTypeName="ebike.Security.Entities.RoleProfile" DeleteMethod="RemoveRole" InsertMethod="AddRole" OldValuesParameterFormatString="original_{0}" SelectMethod="ListAllRoles" TypeName="ebike.Security.BLL.RoleManager"></asp:ObjectDataSource>
                </div>

                <div id="unregistered" class="tab-pane fade in">
                    <blockquote>Users who are not assigned to security roles</blockquote>
                    <asp:GridView ID="UnregisteredUsersGridView" runat="server"
                        CssClass="table table-hover" AutoGenerateColumns="False"
                        DataSourceID="UnregisteredUserDataSource"
                        ItemType="ebike.Security.Entities.UnregisteredUser"
                        AllowPaging="True"
                        OnSelectedIndexChanging="UnregisteredUsersGridView_SelectedIndexChanging">
                        <Columns>
                            <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id"></asp:BoundField>
                            <asp:BoundField DataField="UserType" HeaderText="UserType" SortExpression="UserType"></asp:BoundField>
                            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name"></asp:BoundField>
                            <asp:BoundField DataField="OtherName" HeaderText="OtherName" SortExpression="OtherName"></asp:BoundField>

                            <asp:BoundField DataField="AssignedUserName" HeaderText="AssignedUserName" SortExpression="AssignedUserName"></asp:BoundField>
                            <asp:BoundField DataField="AssignedEmail" HeaderText="AssignedEmail" SortExpression="AssignedEmail"></asp:BoundField>
                            <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone"></asp:BoundField>
                        </Columns>
                    </asp:GridView>
                    <asp:ObjectDataSource runat="server" ID="UnregisteredUserDataSource" OldValuesParameterFormatString="original_{0}" SelectMethod="ListAllUnregsiteredUsers" TypeName="ebike.Security.BLL.UserManager" DataObjectTypeName="ebike.Security.Entities.UserProfile" DeleteMethod="RemoveUser"></asp:ObjectDataSource>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

