<%@ Page Title="" Language="C#" MasterPageFile="~/BackEnd.Master" AutoEventWireup="true" CodeBehind="EditMenuItem.aspx.cs"
    Inherits="Shanghai.WebApp.BackEnd_Page.EditMenuItem" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <div class="body-content" style="background-color: white;">
        <style>
            .bgcolor {
                background-color: white;
                margin: 0 auto;
            }

            h1 {
                text-align: center;
                font-family: 'Amatic SC', cursive;
            }

            body {
                background-color: white;
                color: none;
            }

            .display {
                width: 1156px;
                margin: 0 auto;
                /*margin-left:40px;*/
                text-align: center;
            }

            .tital {
                text-align: center;
                font-family: 'Lato', sans-serif;
            }

            .pageNav {
                font-family: 'Amatic SC', cursive;
                font-weight: bolder;
                font-size: 2em;
            }

            .jumbotron {
                background-color: #eadc5f;
            }

            .labelEllipsisStyle {
                text-overflow: ellipsis;
                white-space: nowrap;
                display: block;
                overflow: hidden;
            }

            .descField {
                display: inline;
            }

            .marR {
                margin-right: 25px;
            }
        </style>


        <div class="bgcolor container">
            <div class="jumbotron">
                <h1>Add & Edit Menu Items and Categories</h1>
            </div>
            <div class="row tab">
                <div class="col-md-9">
                    <ul class="nav nav-tabs pageNav">
                        <li class="active"><a href="#editmenuitem" data-toggle="tab">Edit Menu Item</a></li>
                        <li><a href="#editcategory" data-toggle="tab">Edit Categories</a></li>
                    </ul>


                    <div class="tab-content display">
                        <!-- user tab -->
                        <div class="tab-pane fade in active" id="editmenuitem">


                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>

                                    <br />
                                    <br />
                                    <asp:DropDownList ID="EditMenu_CategoryDDL" CssClass="form-control" runat="server" DataSourceID="ObjectDataSource1"
                                        DataTextField="CategoryName" AppendDataBoundItems="true" DataValueField="CategoryID" AutoPostBack="True" Width="250px">
                                        <asp:ListItem Value="0" Text="All" />
                                    </asp:DropDownList><br />
                                    <br />
                                    <asp:ObjectDataSource runat="server" ID="ObjectDataSource1" SelectMethod="Category_List"
                                        TypeName="Shanghai.System.BLL.categorycontroller" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>


                                    <asp:ListView runat="server" ID="EditMenuListView" DataSourceID="MIDataSource" DataKeyNames="MenuItemID"
                                        ItemType="Shanghai.Data.Entities.MenuItem" InsertItemPosition="LastItem">

                                        <EditItemTemplate>

                                            <td>
                                                <asp:TextBox Text='<%# Bind("MenuItemName") %>' CssClass="form-control" runat="server" ID="MenuItemNameTextBox" Width="250px" />
                                                <asp:Label runat="server" CssClass="descField">Description</asp:Label><asp:TextBox Width="181px" Font-Size="Small" ForeColor="Gray" CssClass="form-control descField marR" Text='<%# Bind("Description") %>' ID="DescriptionTextBox" runat="server" />
                                            </td>
                                            <td>
                                                <asp:CheckBox Checked='<%# Bind("ActiveYN") %>' runat="server" ID="ActiveYNCheckBox" Enabled="true" />
                                            </td>
                                            <td>
                                                <asp:TextBox Text='<%# Bind("CurrentPrice") %>' CssClass="form-control" runat="server" ID="CurrentPriceTextBox" /></td>

                                            <td>
                                                <asp:DropDownList ID="DropDownList2" SelectedValue='<%# Bind("CategoryID") %>' runat="server" CssClass="form-control" DataSourceID="categorylist" DataTextField="CategoryName" DataValueField="CategoryID"></asp:DropDownList>

                                                <asp:ObjectDataSource runat="server" ID="categorylist" OldValuesParameterFormatString="original_{0}" SelectMethod="Category_List" TypeName="Shanghai.System.BLL.categorycontroller"></asp:ObjectDataSource>
                                            </td>
                                            <td>
                                                <asp:CheckBox Checked='<%# Bind("includeInWebMenu") %>' runat="server" ID="CheckBox1" Enabled="true" />
                                            </td>
                                            <td>
                                                <asp:CheckBox Checked='<%# Bind("isComboOption") %>' runat="server" ID="CheckBox2" Enabled="true" />
                                            </td>
                                            <td>
                                                <asp:CheckBox Checked='<%# Bind("isCombo") %>' runat="server" ID="CheckBox3" Enabled="true" />
                                            </td>
                                            <td>
                                                <asp:TextBox Text='<%# BindItem.AmountOfSelections %>' CssClass="form-control" runat="server" ID="SelectionsTB" />
                                            </td>
                                            <td>
                                                <asp:Button runat="server" CssClass="btn btn-success" CommandName="Update" Text="Update" ID="UpdateButton" />
                                                <asp:Button runat="server" CssClass="btn btn-danger" CommandName="Cancel" Text="Cancel" ID="CancelButton" />
                                            </td>

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
                                                    <asp:TextBox Text='<%# Bind("MenuItemName") %>' CssClass="form-control" runat="server" ID="MenuItemNameTextBox" />
                                                    <asp:Label runat="server" CssClass="descField">Description</asp:Label><asp:TextBox Width="214px" Font-Size="Small" ForeColor="Gray" CssClass="form-control descField" Text='<%# Bind("Description") %>' ID="DescriptionTextBox" runat="server" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox Checked='<%# Bind("ActiveYN") %>' runat="server" ID="ActiveYNCheckBox" Enabled="false" />
                                                </td>
                                                <td>
                                                    <asp:TextBox Text='<%# Bind("CurrentPrice") %>' CssClass="form-control" runat="server" ID="CurrentPriceTextBox" /></td>

                                                <td>
                                                    <asp:DropDownList ID="DropDownList2" runat="server" SelectedValue='<%# Bind("CategoryID") %>' CssClass="form-control" DataSourceID="categorylist" DataTextField="CategoryName" DataValueField="CategoryID"></asp:DropDownList>

                                                    <asp:ObjectDataSource runat="server" ID="categorylist" OldValuesParameterFormatString="original_{0}" SelectMethod="Category_List" TypeName="Shanghai.System.BLL.categorycontroller"></asp:ObjectDataSource>
                                                </td>
                                                <td>
                                                    <asp:CheckBox Checked='<%# Bind("includeInWebMenu") %>' runat="server" ID="CheckBox1" Enabled="true" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox Checked='<%# Bind("isComboOption") %>' runat="server" ID="CheckBox2" Enabled="true" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox Checked='<%# Bind("isCombo") %>' runat="server" ID="CheckBox3" Enabled="true" />
                                                </td>
                                                <td>
                                                    <asp:TextBox Text='<%# BindItem.AmountOfSelections %>' CssClass="form-control" runat="server" ID="SelectionsTB" />
                                                </td>
                                                <td>
                                                    <asp:Button runat="server" CssClass="btn btn-success" CommandName="Insert" Text="Add" ID="InsertButton" />
                                                    <asp:Button runat="server" CssClass="btn btn-danger" CommandName="Cancel" Text="Clear" ID="CancelButton" />
                                                </td>

                                            </tr>
                                        </InsertItemTemplate>
                                        <ItemTemplate>
                                            <tr style="background-color: #f6f0b9; color: #333333; text-align: left;">

                                                <td>
                                                    <asp:Label Text='<%# Eval("MenuItemName") %>' CssClass="form-control labelEllipsisStyle" runat="server" ID="MenuItemNameLabel" Width="280px" />
                                                    <asp:Label Font-Size="Smaller" Font-Italic="true" ForeColor="Gray" Font-Bold="false" Text='<%# String.IsNullOrEmpty(Item.Description)? "" : Item.Description %>' Style="display: block;" Visible="<%# !String.IsNullOrEmpty(Item.Description) %>" runat="server" />
                                                </td>
                                                <td style="text-align: center;">
                                                    <asp:CheckBox Checked='<%# Eval("ActiveYN") %>' runat="server" ID="ActiveYNCheckBox" Enabled="false" Width="100px" />
                                                </td>
                                                <td style="text-align: right !important;">
                                                    <asp:Label Text='<%# Eval("CurrentPrice", "{0:c}") %>' CssClass="form-control" runat="server" ID="CurrentPriceLabel" /></td>

                                                <td>
                                                    <asp:Label Text='<%# Eval("MenuCategory.CategoryName") %>' CssClass="form-control labelEllipsisStyle" runat="server" ID="CategoryLabel" />
                                                </td>
                                                <td style="text-align: center;">
                                                    <asp:CheckBox Checked='<%# Eval("includeInWebMenu") %>' runat="server" ID="CheckBox1" Enabled="false" />
                                                </td>
                                                <td style="text-align: center;">
                                                    <asp:CheckBox Checked='<%# Eval("isComboOption") %>' runat="server" ID="CheckBox2" Enabled="false" />
                                                </td>
                                                <td style="text-align: center;">
                                                    <asp:CheckBox Checked='<%# Eval("isCombo") %>' runat="server" ID="CheckBox3" Enabled="false" />
                                                </td>
                                                <td>
                                                    <asp:TextBox Text='<%# Item.AmountOfSelections %>' CssClass="form-control" runat="server" Enabled="false" ID="SelectionsTB" />
                                                </td>
                                                <td>
                                                    <asp:Button runat="server" CssClass="btn btn-danger" CommandName="Delete" Text="Delete" ID="DeleteButton" />
                                                    <asp:Button runat="server" CssClass="btn btn-info" CommandName="Edit" Text="Edit" ID="EditButton" />
                                                </td>

                                            </tr>
                                            <%-- id="itemPlaceholder"--%>
                                        </ItemTemplate>
                                        <LayoutTemplate>
                                            <table runat="server" style="margin-bottom: 40px;">
                                                <tr runat="server">
                                                    <td runat="server">
                                                        <table runat="server" id="itemPlaceholderContainer" class="table table-hover">
                                                            <tr runat="server" style="">
                                                                <th runat="server" class="tital col-md-2">Menu Item Name</th>
                                                                <th runat="server" class="tital col-md-1">Active(Yes/No)</th>
                                                                <th runat="server" class="tital col-md-1 " style="width: 100px;">Current Price</th>
                                                                <th runat="server" class="tital col-md-2">Menu Category</th>
                                                                <th runat="server" class="tital col-md-1">Include in Menu?</th>
                                                                <th runat="server" class="tital col-md-1">Is Available as Combo Selection?</th>
                                                                <th runat="server" class="tital col-md-1">Is Combo?</th>
                                                                <th runat="server" class="tital col-md-1"># Of Combo Selections</th>
                                                                <th runat="server" class="tital col-md-2"></th>
                                                            </tr>
                                                            <tr runat="server" id="itemPlaceholder"></tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr runat="server">
                                                    <td runat="server" class="fix" style="background-color: #ede175">
                                                        <asp:DataPager runat="server" ID="DataPager2">
                                                            <Fields>
                                                                <asp:NextPreviousPagerField ButtonCssClass="btn-default" ButtonType="Button" ShowFirstPageButton="True" ShowLastPageButton="True"></asp:NextPreviousPagerField>

                                                            </Fields>
                                                        </asp:DataPager>
                                                    </td>
                                                </tr>
                                            </table>
                                        </LayoutTemplate>

                                    </asp:ListView>

                                    <asp:ObjectDataSource runat="server" ID="MIDataSource" DataObjectTypeName="Shanghai.Data.Entities.MenuItem"
                                        DeleteMethod="DeleteMenuItem"
                                        InsertMethod="AddMenuItem"
                                        OldValuesParameterFormatString="original_{0}"
                                        SelectMethod="MenuByCategory"
                                        TypeName="Shanghai.System.BLL.MenuController"
                                        UpdateMethod="UpdateMenuItem">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="EditMenu_CategoryDDL" PropertyName="SelectedValue" DefaultValue="0" Name="categoryid" Type="String"></asp:ControlParameter>
                                        </SelectParameters>
                                    </asp:ObjectDataSource>

                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>


                        <!-- user tab -->
                        <div class="tab-pane fade" id="editcategory" style="margin-bottom: 20px;">
                            <br />
                            <br />
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:ListView ID="ListView1" runat="server" DataSourceID="listviewcategory" DataKeyNames="CategoryID" InsertItemPosition="LastItem">

                                        <EditItemTemplate>

                                            <td>
                                                <asp:TextBox Text='<%# Bind("CategoryName") %>' runat="server" ID="CategoryNameTextBox" CssClass="form-control" /></td>
                                            <td>
                                                <asp:Button runat="server" CssClass="btn btn-success" CommandName="Update" Text="Update" ID="UpdateButton" />
                                                <asp:Button runat="server" CssClass="btn btn-danger" CommandName="Cancel" Text="Cancel" ID="CancelButton" />
                                            </td>

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
                                                    <asp:TextBox Text='<%# Bind("CategoryName") %>' runat="server" ID="CategoryNameTextBox" CssClass="form-control" /></td>
                                                <td>
                                                    <asp:Button runat="server" CssClass="btn btn-success" CommandName="Insert" Text="Add" ID="InsertButton" />
                                                    <asp:Button runat="server" CssClass="btn btn-danger" CommandName="Cancel" Text="Clear" ID="CancelButton" />
                                                </td>

                                            </tr>
                                        </InsertItemTemplate>

                                        <ItemTemplate>
                                            <tr style="background-color: #f6f0b9; color: #333333; text-align: left;">

                                                <td>
                                                    <asp:Label Text='<%# Eval("CategoryName") %>' runat="server" ID="CategoryNameLabel" CssClass="form-control" /></td>
                                                <td>
                                                    <asp:Button runat="server" CssClass="btn btn-danger" CommandName="Delete" Text="Delete" ID="DeleteButton" />
                                                    <asp:Button runat="server" CssClass="btn btn-info" CommandName="Edit" Text="Edit" ID="EditButton" />
                                                </td>



                                            </tr>
                                        </ItemTemplate>
                                        <LayoutTemplate>
                                            <table runat="server" style="margin: 0 auto;">
                                                <tr runat="server">
                                                    <td runat="server">
                                                        <table runat="server" id="itemPlaceholderContainer" class="table table-hover" style="width: 550px;">

                                                            <tr runat="server">

                                                                <th runat="server" class="tital">CategoryName</th>
                                                                <th runat="server"></th>

                                                            </tr>
                                                            <tr runat="server" id="itemPlaceholder"></tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr runat="server">
                                                    <td runat="server" class="fix" style="background-color: #ede175">
                                                        <asp:DataPager runat="server" ID="DataPager1">
                                                            <Fields>
                                                                <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowLastPageButton="True"></asp:NextPreviousPagerField>
                                                            </Fields>
                                                        </asp:DataPager>
                                                    </td>
                                                </tr>
                                                <br />
                                                <br />
                                            </table>
                                        </LayoutTemplate>

                                    </asp:ListView>
                                    <asp:ObjectDataSource runat="server" OnDataBinding="BindEditMenu" OnDeleted="BindEditMenu" OnInserted="BindEditMenu"
                                        OnUpdated="BindEditMenu" ID="listviewcategory" DataObjectTypeName="Shanghai.Data.Entities.MenuCategory"
                                        DeleteMethod="DeleteMenuCategory" InsertMethod="AddMenuCategory" OldValuesParameterFormatString="original_{0}"
                                        SelectMethod="Category_List" TypeName="Shanghai.System.BLL.categorycontroller" UpdateMethod="UpdateMenuCategory"></asp:ObjectDataSource>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>

                </div>
            </div>
        </div>

    </div>



</asp:Content>
