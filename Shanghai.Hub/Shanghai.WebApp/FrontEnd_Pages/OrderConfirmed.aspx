<%@ Page Title="" Language="C#" MasterPageFile="FrontFacing.Master" AutoEventWireup="true" CodeBehind="OrderConfirmed.aspx.cs" Inherits="Shanghai.WebApp.OrderConfirmed" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="https://fonts.googleapis.com/css?family=Amatic+SC:400,700" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css?family=Lato:400,700" rel="stylesheet" />
    <style>
        .confirmContainer{
            text-align: center;
            background-color: white;
            padding: 0 20px 0 20px;
        }
        .confirmContainer h3{
            font-family: 'Amatic SC', cursive;
            font-size: 50px;
            font-weight: bold;
        }
        #MainContent_orderConfirmPanel{
            text-align: left;
            margin: 0 auto;
            max-width: 800px;
        }
        .confirmContainer h4, .tag{
            font-family: 'Amatic SC', cursive;
            font-size: 40px;
            font-weight: bold;
        }
        .confirmContainer h5{
            font-family: 'Amatic SC', cursive;
            font-size: 30px;
            font-weight: bold;
            color: #b92d27;
        }
        .orderListview{
            margin: 0 auto;
            width: 500px;
        }
        .totals{
            text-align: right;
            margin-right: 160px;
        }
        .totalLabel{
            position: relative;
            right: 10px;
        }
        .soup{
            width: 20%;
        }

    </style>
    <div class="container confirmContainer">
        <div class="orderConfirm">
            <h3>Order Placed!</h3>
            <asp:Panel ID="orderConfirmPanel" runat="server">
                

                <h5>Customer Information</h5>
                <asp:Label style="display: block;" runat="server" Text="" ID="nameLabel"></asp:Label>
                <asp:Label style="display: block;" runat="server" Text="" ID="phoneLabel"></asp:Label>
                <asp:Label style="display: block;" runat="server" Text="" ID="emailLabel"></asp:Label>
                <hr />
                <h5>Order Details</h5>
                <asp:Label runat="server" Text="" ID="datePlacedLabel"></asp:Label><br />
                <asp:Label style="display: block;" runat="server" Text="" ID="deliveryLabel" Visible="false"></asp:Label>
                <asp:Label style="display: block;" runat="server" Text="" ID="deliveryEstimateLabel" Visible="false"></asp:Label>
                <asp:Label style="display: block;" runat="server" Text="" ID="deliveryAddressLabel" Visible="false"></asp:Label>
                <asp:Label style="display: block;" runat="server" Text="" ID="pickupLabel" Visible="false"></asp:Label>
                <asp:Label style="display: block;" runat="server" Text="" ID="pickupTimeLabel" Visible="false"></asp:Label>
                
            </asp:Panel>
            <asp:HiddenField runat="server" ID="CustomerIDHF" Value='1' /> <%--TODO get actual userID--%>

            <h4>Here is what you ordered:</h4>
            <div class="orderListview">
                <asp:ListView runat="server" ID="OrderItemListView" DataSourceID="OrderDetailODS"
                    ItemType="Shanghai.Data.Entities.OrderDetail"
                    DataKeyNames="OrderID"
                    OnItemDataBound="OrderItemListView_ItemDataBound" >
                    <AlternatingItemTemplate>
                        <tr style="">
                            <td class="changeDisplay">
                                <asp:Label Text='<%# Eval("MenuItemID") %>' runat="server" ID="MenuItemIDLabel" /></td>
                            <td>
                                <asp:HiddenField ID="isComboHF" Value="<%# Item.MenuItem.isCombo %>" runat="server" />
                                 <asp:HiddenField ID="MenuItemID" runat="server" Value="<%# Item.MenuItemID %>" />
                                <asp:HiddenField ID="OrderID" runat="server" Value="<%# Item.OrderID %>" />
                                <asp:Label Text='<%# Item.MenuItem.MenuItemName %>' runat="server" ID="MenuItemLabel" />
                                <asp:Repeater runat="server" ID="ComboRepeater" ItemType="Shanghai.Data.POCOs.OrderComboSelection">
                                    <ItemTemplate>
                                        <p style="text-align: left; margin: 0; font-style: italic;">-<%# Item.MenuItemName %></p>
                                    </ItemTemplate>
                                </asp:Repeater>
                                </td>
                            <td>
                                <asp:Label Text='<%# Eval("Quantity") %>' runat="server" ID="QuantityLabel" /></td>
                            <td style="text-align: right;">
                                <asp:Label Text='<%# Eval("SellingPrice", "{0:c}") %>' runat="server" ID="SellingPriceLabel" /></td> 
                            <td style="text-align: right;">
                            <asp:Label Text='<%# (Item.MenuItem.CurrentPrice * Item.Quantity).ToString("C") %>' runat="server" ID="Total" /></td> 
                        </tr>
                    </AlternatingItemTemplate>
                    <EmptyDataTemplate>
                        <table runat="server" style="">
                            <tr>
                                <td>No data was returned.</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <tr style="">
                            <td class="changeDisplay">
                                <asp:Label Text='<%# Eval("MenuItemID") %>' runat="server" ID="MenuItemIDLabel" /></td>
                            <td>
                                <asp:HiddenField ID="isComboHF" Value="<%# Item.MenuItem.isCombo %>" runat="server" />
                                 <asp:HiddenField ID="MenuItemID" runat="server" Value="<%# Item.MenuItemID %>" />
                                <asp:HiddenField ID="OrderID" runat="server" Value="<%# Item.OrderID %>" />
                                <asp:Label Text='<%# Item.MenuItem.MenuItemName %>' runat="server" ID="MenuItemLabel" />
                            <asp:Repeater runat="server" ID="ComboRepeater" ItemType="Shanghai.Data.POCOs.OrderComboSelection">
                                <ItemTemplate>
                                    <p style="text-align: left; margin: 0; font-style: italic;">-<%# Item.MenuItemName %></p>
                                </ItemTemplate>
                            </asp:Repeater>
                            </td>
                            <td>
                                <asp:Label Text='<%# Eval("Quantity") %>' runat="server" ID="QuantityLabel" /></td>
                            <td style="text-align: right;">
                                <asp:Label Text='<%# Eval("SellingPrice", "{0:c}") %>' runat="server" ID="SellingPriceLabel" /></td>
                            <td style="text-align: right;">
                            <asp:Label Text='<%# (Item.MenuItem.CurrentPrice * Item.Quantity).ToString("C") %>' runat="server" ID="Total" /></td>
                        </tr>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table runat="server">
                            <tr runat="server">
                                <td runat="server">
                                    <table runat="server" id="itemPlaceholderContainer" border="0" class="table table-condensed table-bordered table-responsive" style="width: 500px">
                                        <tr runat="server">
                                            <th style="text-align: center;" class="col-md-2 col-sm-1 changeDisplay" runat="server">Item Number</th>
                                            <th style="text-align: center;" class="col-md-4 col-sm-2" runat="server">Name</th>
                                            <th style="text-align: center;" class="col-md-2 col-sm-1" runat="server">Quantity</th>
                                            <th style="text-align: center;" class="col-md-2 col-sm-1" runat="server">Price</th>
                                            <th style="text-align: center;" class="col-md-2 col-sm-1" runat="server">Item Total</th>
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
            <asp:Label class="totalLabel"  Text='GST: ' runat="server"/><asp:Label class="totals" style="text-align: right;" Text="" ID="displayGST" runat="server" /> <br />
        <asp:Label class="totals" style="text-align: right; display: block;" Text="" ID="displayDelivery" runat="server" Visible="false" />
            <hr style="margin-bottom: 2px; margin-top: 2px; border: 0.5px solid #c1c1c1" width="10%" class="totals">
            <asp:Label class="totalLabel"  Text='Total:' runat="server"/><asp:Label class="totals" style="text-align: right;" Text="" ID="displayTotal" runat="server" /> <br />
        </div>
            <asp:ObjectDataSource runat="server" ID="OrderDetailODS" OldValuesParameterFormatString="original_{0}" SelectMethod="ListOrderDetails" TypeName="Shanghai.System.BLL.OrderController">
                <SelectParameters>
                    <asp:SessionParameter SessionField="ConfirmedOrder" DefaultValue="" Name="orderID" Type="Int32"></asp:SessionParameter>
                </SelectParameters>
            </asp:ObjectDataSource>
            <p style="font-weight: bold; margin-top: 20px; font-size: 16px; color: #b92d27;">To correct any information above or make changes to this order, please call 780-986-1862.</p>
            <asp:Label class="tag" style="display: block;" runat="server" Text="" ID="confirmationTag"></asp:Label>
            <img class="soup" src="../images/soup.png" />
        </div>

    </div> 
</asp:Content>
