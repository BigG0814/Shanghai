<%@ Page Title="" Language="C#" MasterPageFile="FrontFacing.Master" AutoEventWireup="true" CodeBehind="ViewCart.aspx.cs" Inherits="Shanghai.WebApp.ViewCart" EnableEventValidation="false" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        
    </style>
    <asp:Panel ID="MessagePanel" runat="server">
        <div class="alert alert-danger" style="margin-bottom: 0 !important;">
            <strong>Note:</strong> Shanghai Leduc is currently <b>closed</b> and will be open for ordering between 10:00am-10:00pm on weekdays, and 4:00pm-10:00pm on weekends.
        </div>
    </asp:Panel>

    <div class="container mainContainer viewCartContainer">
        <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
        <h3>Your Order</h3>
        <p>Please choose pickup or delivery:</p>
        <asp:Button runat="server" CssClass="btn btn-success btn-Pickup btn-Cart" OnClick="PickUpButton_OnClick" Text="Pick-Up" ID="OrderBtn1" /><asp:Button runat="server" Text="Delivery" CssClass="btn btn-info btn-Cart" OnClick="DeliveryButton_OnClick" ID="OrderBtn2" />
        <asp:Label runat="server" CssClass="notification" ID="NotificationLabel" BorderColor="Red" Text="Store is currently closed. Please place order during open hours." Visible="false"></asp:Label>
        <asp:HiddenField ID="CartIDHF" runat="server"/>

       
        <div class="cartGridview">
            <asp:ListView ID="CartListView" runat="server" 
                OnDataBound="CartListView_DataBound"
                OnItemUpdated="CartListView_ItemUpdated"
                DataSourceID="CartDataSource"
                DataKeyNames="ShoppingCartItemID"
                ItemType="Shanghai.Data.Entities.ShoppingCartItem"
                 OnItemDataBound="CartListView_ItemDataBound" >
                <AlternatingItemTemplate>
                    <tr style="">
                        <td class="changeDisplay">
                            <asp:Label Text='<%# Eval("MenuItemID") %>' runat="server" ID="MenuItemIDLabel" /></td>
                        <td class="col-xs-1">
                            <asp:HiddenField ID="CartItemID" runat="server" Value="<%# Item.ShoppingCartItemID %>" />
                            <asp:Label Text='<%# Item.MenuItem.MenuItemName %>' runat="server" ID="MenuItemLabel" />
                            <asp:HiddenField ID="isComboHF" Value="<%# Item.MenuItem.isCombo %>" runat="server" />
                            <asp:Repeater runat="server" ID="ComboRepeater" ItemType="Shanghai.Data.POCOs.OrderComboSelection">
                                <ItemTemplate>
                                    <p style="text-align: left; margin: 0; font-style: italic;">-<%# Item.MenuItemName %></p>
                                </ItemTemplate>
                            </asp:Repeater>
                        </td>
                        <td style="text-align: right;">
                            <asp:Label Text='<%# Item.MenuItem.CurrentPrice.ToString("C") %>' runat="server" ID="Price" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("Quantity") %>' runat="server" ID="QuantityLabel" /></td>
                        
                        <td>
                            <asp:Button CssClass="btn btn-info" runat="server" CommandName="Edit" Text="Update" ID="EditButton" />
                            <asp:Button CssClass="btn btn-danger" runat="server" CommandName="Delete" Text="Delete" ID="DeleteButton" />
                        </td>
                        <td style="text-align: right;">
                            <asp:Label Text='<%# (Item.MenuItem.CurrentPrice * Item.Quantity).ToString("C") %>' runat="server" ID="Total" /></td>
                    </tr>
                </AlternatingItemTemplate>
                <EditItemTemplate>
                    <tr style="">
                            <asp:HiddenField Value='<%# Bind("ShoppingCartItemID") %>' runat="server" ID="ShoppingCartItemIDHF" Visible="false" />

                            <asp:HiddenField Value='<%# Bind("ShoppingCartID") %>' runat="server" ID="ShoppingCartIDHF" Visible="false" />
                        <td class="changeDisplay">
                            <asp:Label Text='<%# Bind("MenuItemID") %>' runat="server" ID="MenuItemIDTextBox" /></td>
                        <td class="col-xs-1">
                            <asp:Label Text='<%# Item.MenuItem.MenuItemName %>' runat="server" ID="MenuItemTextBox"  /></td>
                        <td style="text-align: right;">
                            <asp:Label Text='<%# Item.MenuItem.CurrentPrice.ToString("C") %>' runat="server" ID="Price" /></td>
                        <td>
                            <asp:TextBox Text='<%# Bind("Quantity") %>' TextMode="Number" runat="server" ID="QuantityTextBox" /></td>
                        
                        <td>
                            <asp:Button CssClass="btn btn-success" runat="server" CommandName="Update" Text="Confirm" ID="UpdateButton" />
                            <asp:Button CssClass="btn btn-danger" runat="server" CommandName="Cancel" Text="Cancel" ID="CancelButton" />
                            
                        </td>
                        <td style="text-align: right;">
                            <asp:Label Text='<%# (Item.MenuItem.CurrentPrice * Item.Quantity).ToString("C") %>' runat="server" ID="Total" /></td>
                        </tr>
                </EditItemTemplate>
                <EmptyDataTemplate>
                    <table runat="server" style="">
                        <tr>
                            <td>No items in your cart. <a href="Menu.aspx">Go To Menu -></a></td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <ItemTemplate>
                    <tr style="">
                        <td class="changeDisplay">
                            <asp:Label Text='<%# Eval("MenuItemID") %>' runat="server" ID="MenuItemIDLabel" /></td>
                        <td>
                            <asp:HiddenField ID="CartItemID" runat="server" Value="<%# Item.ShoppingCartItemID %>" />
                            <asp:Label Text='<%# Item.MenuItem.MenuItemName %>' runat="server" ID="MenuItemLabel" />
                            <asp:HiddenField ID="isComboHF" Value="<%# Item.MenuItem.isCombo %>" runat="server" />
                            <asp:Repeater runat="server" ID="ComboRepeater" ItemType="Shanghai.Data.POCOs.OrderComboSelection">
                                <ItemTemplate>
                                    <p style="text-align: left; margin: 0; font-style: italic;">-<%# Item.MenuItemName %></p>
                                </ItemTemplate>
                            </asp:Repeater>

                            <%--put here--%>
                        </td>  
                        <td style="text-align: right;">
                            <asp:Label Text='<%# Item.MenuItem.CurrentPrice.ToString("C") %>' runat="server" ID="Price" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("Quantity") %>' runat="server" ID="QuantityLabel" /></td>                        
                        <td>
                            <asp:Button CausesValidation="false" CssClass="btn btn-info" runat="server" CommandName="Edit" Text="Update" ID="EditButton" />
                            <asp:Button CausesValidation="false" CssClass="btn btn-danger" runat="server" CommandName="Delete" Text="Delete" ID="DeleteButton" />
                        </td>
                        <td style="text-align: right;">
                            <asp:Label Text='<%# (Item.MenuItem.CurrentPrice * Item.Quantity).ToString("C") %>' runat="server" ID="Total" /></td>
                    </tr>
                </ItemTemplate>
                <LayoutTemplate>
                    <table runat="server">
                        <tr runat="server">
                            <td runat="server">
                                <table runat="server" id="itemPlaceholderContainer" class="table table-hover table-condensed table-bordered table-responsive" style="width: 1000px; margin: 0 auto;" >
                                    <tr runat="server" style="background-color: lightgray; text-align: center;">
                                        <th style="text-align: center;" class="col-lg-2 col-sm-1 changeDisplay" runat="server">Item Number</th>
                                        <th style="text-align: center;" class="col-lg-3 col-sm-2 col-xs-1" runat="server">Item Name</th>
                                        <th style="text-align: center;" class="col-lg-1 col-sm-1" runat="server">Price</th>
                                        <th style="text-align: center;" class="col-lg-2 col-sm-1" runat="server">Quantity</th>
                                        <th style="text-align: center;" class="col-lg-2 col-sm-1" runat="server">Change Item</th>
                                        <th style="text-align: center;" class="col-lg-2 col-sm-1" runat="server">Total</th>
                                    </tr>
                                    <tr runat="server" id="itemPlaceholder"></tr>
                                </table>
                            </td>
                        </tr>
                        <tr runat="server">
                            <td runat="server" style=""></td>
                        </tr>
                    </table>
                </LayoutTemplate>
            </asp:ListView>
        </div>
        <div class="totals">
        <asp:Label class="totalLabel" Text='Subtotal:' runat="server"/><asp:Label class="totals" style="text-align: right;" Text="" ID="displaySubtotal" runat="server" /><br/>
            <asp:Label class="totalLabel"  Text='GST:' runat="server"/><asp:Label class="totals" style="text-align: right;" Text="" ID="displayGST" runat="server" /> <br />
        <asp:Label class="totalLabel" ID="deliveryDisplayLabel"  Text='Delivery:' runat="server" Visible="false" /><asp:Label class="totals" Visible="false" style="text-align: right;" Text="" ID="deliveryLabel" runat="server" /> <br />
            <hr style="margin-bottom: 2px; margin-top: 2px; border: 0.5px solid #c1c1c1" width="10%" class="totals">
            <asp:Label class="totalLabel"  Text='Total:' runat="server"/><asp:Label class="totals" style="text-align: right;" Text="" ID="displayTotal" runat="server" /> <br />
        </div>
        <p style="height: 40px;"><a class="menuLink" href="Menu.aspx"><i class="fa fa-arrow-left"></i> Back to Menu</a></p>
        <asp:ObjectDataSource runat="server" OnSelecting="CartDataSource_Selecting" ID="CartDataSource" OldValuesParameterFormatString="original_{0}" SelectMethod="ListCartItems" TypeName="Shanghai.System.BLL.CartController" DataObjectTypeName="Shanghai.Data.Entities.ShoppingCartItem" DeleteMethod="DeleteCartItem" UpdateMethod="UpdateCartItem">
            <SelectParameters>
                <asp:Parameter Name="cartid" />
            </SelectParameters>
        </asp:ObjectDataSource>

        <asp:Panel ID="AlertPanel" runat="server" Visible="false">
            <div class="alert alert-danger" style="margin-bottom: 0 !important;">
                <strong>Please make menu selections before placing order</strong>
            </div>
        </asp:Panel>

        <asp:Panel ID="PickupDetailsPanel" runat="server" Visible="false" CssClass="row">
            <div class="orderDetails">
            <h3>Pickup Details</h3>
                    <div class="form-group col-md-6 field">
                    <label for="formGroupExampleInput">First Name</label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="FirstNameTextBoxPU" />
                        <asp:RequiredFieldValidator ValidationGroup="PickupInfoGroup" Display="Dynamic" CssClass="validator" runat="server" ID="reqPUFName" ControlToValidate="FirstNameTextBoxPU" ErrorMessage="Please enter your first name" />
                  </div>
                  <div class="form-group col-md-6 field">
                    <label for="formGroupExampleInput2">Last Name</label>
                    <asp:TextBox runat="server"  CssClass="form-control" ID="LastNameTextBoxPU" />
                     <asp:RequiredFieldValidator ValidationGroup="PickupInfoGroup" Display="Dynamic" CssClass="validator" runat="server" ID="reqPULName" ControlToValidate="LastNameTextBoxPU" ErrorMessage="Please enter your last name" />
                  </div>
                    <div class="form-group col-md-6 field">
                        <label for="PhoneTextBoxPU">Phone Number</label>
                        <asp:TextBox runat="server" TextMode="Phone" CssClass="form-control" ID="PhoneTextBoxPU" placeholder="###-###-####" />
                        <asp:RequiredFieldValidator ValidationGroup="PickupInfoGroup" Display="Dynamic" CssClass="validator" runat="server" ID="reqPUPhone" ControlToValidate="PhoneTextBoxPU" ErrorMessage="Please enter your phone number" />
                        <asp:RegularExpressionValidator ValidationGroup="PickupInfoGroup" Display="Dynamic" CssClass="validator" ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please use format: ###-###-####" ControlToValidate="PhoneTextBoxPU" ValidationExpression="^\d{3}-\d{3}-\d{4}$"></asp:RegularExpressionValidator>
                    </div>
                    <div class="form-group col-md-6 field">
                        <label for="timeSelect">Select pickup time:</label>
                        <asp:DropDownList runat="server" CssClass="form-control" ID="timeSelect">
                            <asp:ListItem value="0" Selected="True">Choose a time...</asp:ListItem>
                            <asp:ListItem value="12">12 pm</asp:ListItem>
                            <asp:ListItem value="1">1 pm</asp:ListItem>
                            <asp:ListItem value="2">2 pm</asp:ListItem>
                            <asp:ListItem value="3">3 pm</asp:ListItem>
                            <asp:ListItem value="4">4 pm</asp:ListItem>
                            <asp:ListItem value="5">5 pm</asp:ListItem>
                            <asp:ListItem value="6">6 pm</asp:ListItem>
                            <asp:ListItem value="7">7 pm</asp:ListItem>
                            <asp:ListItem value="8">8 pm</asp:ListItem>
                        </asp:DropDownList>
                       <asp:RequiredFieldValidator ValidationGroup="PickupInfoGroup" Display="Dynamic" CssClass="validator" ID="reqPUTime" runat="server" ControlToValidate="timeSelect" InitialValue="0" ErrorMessage="Please select a pickup time" />
                    </div>
                    <div class="form-group col-md-12 field">
                    <label for="formGroupExampleInput2">Email</label>
                    <asp:TextBox runat="server" TextMode="Email" CssClass="form-control" ID="EmailTextBoxPU" placeholder="email@domain.com" />
                       <asp:RequiredFieldValidator ValidationGroup="PickupInfoGroup" Display="Dynamic" CssClass="validator" runat="server" ID="reqPUEmail" ControlToValidate="EmailTextBoxPU" ErrorMessage="Please enter your email" />
                  </div>
                <div class="form-group col-md-12">
                    <label for="formGroupExampleInput2">Special Instructions</label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="CommentsPU" TextMode="MultiLine" Rows="3"/>
                  </div>

                    <div class="form-group">
                        <div class="col-md-offset-5 col-xs-offset-4">
                            <asp:Button runat="server" CausesValidation="true" ValidationGroup="PickupInfoGroup" ID="PickupBtn" Text="Place Order" CssClass="btn btn-default btn-lg" OnClick="SubmitOrderDetails_Click" />
                        </div>
                    </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="DeliveryDetailsPanel" runat="server" Visible="false" CssClass="row">
            <div class="orderDetails">
            <h3>Delivery Details</h3>
                  <div class="form-group col-md-6 field">
                    <label for="formGroupExampleInput">First Name</label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="FirstNameTextBoxDL" />
                     <asp:RequiredFieldValidator ValidationGroup="DeliveryInfoGroup" Display="Dynamic" CssClass="validator" runat="server" ID="DeliveryValidator1" ControlToValidate="FirstNameTextBoxDL" ErrorMessage="Please enter your first name" />
                  </div>
                  <div class="form-group col-md-6 field">
                    <label for="formGroupExampleInput2">Last Name</label>
                    <asp:TextBox runat="server"  CssClass="form-control" ID="LastNameTextBoxDL" />
                     <asp:RequiredFieldValidator ValidationGroup="DeliveryInfoGroup" Display="Dynamic" CssClass="validator" runat="server" ID="DeliveryValidator2" ControlToValidate="LastNameTextBoxDL" ErrorMessage="Please enter your last name" />
                  </div>
                    <div class="form-group col-md-6 field">
                    <label for="formGroupExampleInput">Street Address</label>
                    <asp:TextBox runat="server"  CssClass="form-control" ID="AddressTextBoxDL" />
                       <asp:RequiredFieldValidator ValidationGroup="DeliveryInfoGroup" Display="Dynamic" CssClass="validator" runat="server" ID="DeliveryValidator3" ControlToValidate="AddressTextBoxDL" ErrorMessage="Please enter a delivery address" />
                  </div>
                  <div class="form-group col-md-6 field">
                    <label for="formGroupExampleInput2">Apt/Suite/Unit No.</label>
                    <asp:TextBox runat="server"  CssClass="form-control" ID="AptTextBoxDL" />
                  </div>
                <div class="form-group col-md-6 field">
                    <label for="formGroupExampleInput2">City</label>
                    <asp:TextBox runat="server" CssClass="form-control" Enabled="false" Text="Leduc (We deliver to Leduc area only)" />
                  </div>
                    <div class="form-group col-md-6 field">
                    <label for="formGroupExampleInput">Phone Number</label>
                    <asp:TextBox runat="server" TextMode="Phone" CssClass="form-control" ID="PhoneTextBoxDL" placeholder="###-###-####" />
                       <asp:RequiredFieldValidator ValidationGroup="DeliveryInfoGroup" Display="Dynamic" CssClass="validator" runat="server" ID="DeliveryValidator4" ControlToValidate="PhoneTextBoxDL" ErrorMessage="Please enter your phone number" />
                        <asp:RegularExpressionValidator ValidationGroup="DeliveryInfoGroup" Display="Dynamic" CssClass="validator" ID="DeliveryRegExValidator4" runat="server" ErrorMessage="Please use format: ###-###-####" ControlToValidate="PhoneTextBoxDL" ValidationExpression="^\d{3}-\d{3}-\d{4}$"></asp:RegularExpressionValidator>
                  </div>
                  <div class="form-group col-md-12 field">
                    <label for="formGroupExampleInput2">Email</label>
                    <asp:TextBox runat="server" TextMode="Email" CssClass="form-control" ID="EmailTextBoxDL" placeholder="email@domain.com" />
                      <asp:RequiredFieldValidator ValidationGroup="DeliveryInfoGroup" Display="Dynamic" CssClass="validator" runat="server" ID="DeliveryValidator5" ControlToValidate="EmailTextBoxDL" ErrorMessage="Please enter your email" />
                  </div>
                <div class="form-group col-md-12">
                    <label for="formGroupExampleInput2">Special Instructions</label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="CommentsDL" TextMode="MultiLine" Rows="3"/>
                  </div>
                   <div class="form-group"> 
                        <div class="col-md-offset-5 col-xs-offset-4">
                            <asp:Button runat="server" CausesValidation="true" ValidationGroup="DeliveryInfoGroup" ID="DeliveryBtn" Text="Place Order" CssClass="btn btn-default btn-lg" OnClick="SubmitOrderDetails_Click" />
                        </div>
                   </div>
            </div>
        </asp:Panel>
    </div> 
</asp:Content>
