<%@ Page Title="" Language="C#" MasterPageFile="~/BackEnd.Master" AutoEventWireup="true" CodeBehind="Lookup_Order.aspx.cs" Inherits="Shanghai.WebApp.BackEnd_Page.Lookup_Order" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .orderContainer{
            background-color: white;
            min-height: calc(100vh - 106px);
            max-width: 50em;
        }
        .ddl{
            width: 50% !important;
        }
        .completedLabel{
            color: firebrick;
            font-size: 1.5em;
            display: block;
        }
        #MainContent_OrderPanel{
            margin-bottom: 1em;
        }
        .orderTotals{
            text-align: right;
        }
        .orderContent{
            max-width: 40em;
            margin: 0 auto;
        }
        .radio-btn{
            font-size: 0.9em !important;
            font-weight: normal !important;
            margin-left: 0.5em;
        }
        .radio-btn label{
            margin-left: 0.5em;
        }
        .btn{
            margin-top: 1em;
        }
    </style>
    <div class="container orderContainer">
        <div class="orderContent">
            <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
            <h2>Order Lookup</h2>
           
            <asp:Panel runat="server" ID="OrderPanel">
                <h3>Enter an order number:</h3>
           
                <asp:TextBox ID="OrderNumberTextbox" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:Label CssClass="alert-danger" Style="display: block" ID="ErrorMsg" Visible="false" runat="server" Text =""></asp:Label>
                <asp:Button CssClass="btn btn-success" runat="server" Text="Display Details" OnClick="LookupDetails_Click" />
            </asp:Panel>


            <asp:Label runat="server" ID="StatusLabel" Text="" CssClass="completedLabel" Visible="false" />
            <asp:Label runat="server" ID="TypeLabel" Text="" CssClass="completedLabel" Visible="false" />
            <asp:Panel runat="server" ID="DeliveryPanel" Visible="false">
                <div class="row">
                    <div class="col-sm-2"><asp:Label ID="DLNameLabel" runat="server" Text="Customer Name" AssociatedControlID="DLCustomerName" /></div>
                    <div class="col-sm-9"><asp:TextBox ID="DLCustomerName" runat="server" ReadOnly="true" CssClass="form-control" /></div>
                </div>
                <div class="row">
                    <div class="col-sm-2"><asp:Label ID="DLAddressLabel" runat="server" Text="Delivery Address" AssociatedControlID="DLAddress" /></div>
                    <div class="col-sm-9"><asp:TextBox ID="DLAddress" runat="server" ReadOnly="true" CssClass="form-control" /></div>
                </div>
                <div class="row">
                    <div class="col-sm-2"><asp:Label ID="DLPhoneLabel" runat="server" Text="Phone" AssociatedControlID="DLPhone" /></div>
                    <div class="col-sm-9"><asp:TextBox ID="DLPhone" runat="server" ReadOnly="true" CssClass="form-control" /></div>
                </div>
            </asp:Panel>
            <asp:Panel runat="server" ID="PickupPanel" Visible="false">
                <div class="row">
                    <div class="col-sm-2"><asp:Label ID="PUNameLabel" runat="server" Text="Customer Name" AssociatedControlID="PUCustomerName" /></div>
                    <div class="col-sm-9"><asp:TextBox ID="PUCustomerName" runat="server" ReadOnly="true" CssClass="form-control" /></div>
                </div>
                <div class="row">
                    <div class="col-sm-2"><asp:Label ID="PUPhoneLabel" runat="server" Text="Phone" AssociatedControlID="PUPhone" /></div>
                    <div class="col-sm-9"><asp:TextBox ID="PUPhone" runat="server" ReadOnly="true" CssClass="form-control" /></div>
                </div>
                <div class="row">
                    <div class="col-sm-2"><asp:Label ID="PUTimeLabel" runat="server" Text="Pickup Time" AssociatedControlID="PUTime" /></div>
                    <div class="col-sm-9"><asp:TextBox ID="PUTime" runat="server" ReadOnly="true" CssClass="form-control" /></div>
                </div>
            </asp:Panel>
            <asp:Panel runat="server" ID="OrderDetailsPanel">
                 <div class="orderListview">
                    <asp:ListView runat="server" ID="OrderItemListView" DataSourceID="OrderDetailODS"
                        ItemType="Shanghai.Data.Entities.OrderDetail"
                        DataKeyName="OrderID"
                       OnItemDataBound="OrderItemListView_ItemDataBound" >
                        <AlternatingItemTemplate>
                            <tr style="">
                                <td>
                                    <asp:Label Text='<%# Eval("MenuItemID") %>' runat="server" ID="MenuItemIDLabel" /></td>
                                <td>
                                    <asp:Label Text='<%# Item.MenuItem.MenuItemName %>' runat="server" ID="MenuItemLabel" />
                                    <asp:HiddenField ID="isComboHF" Value="<%# Item.MenuItem.isCombo %>" runat="server" />
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
                                <td>
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
                                                <th style="text-align: center;" class="col-md-2" runat="server">Item Number</th>
                                                <th style="text-align: center;" class="col-md-4" runat="server">Name</th>
                                                <th style="text-align: center;" class="col-md-2" runat="server">Quantity</th>
                                                <th style="text-align: center;" class="col-md-2" runat="server">Price</th>
                                                <th style="text-align: center;" class="col-md-2" runat="server">Item Total</th>
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
                <div class="orderTotals">
            <asp:Label class="totalLabel" Text='Subtotal:' runat="server"/><asp:Label class="orderTotals" style="text-align: right;" Text="" ID="displaySubtotal" runat="server" /><br/>
                <asp:Label class="totalLabel"  Text='GST: ' runat="server"/><asp:Label class="orderTotals" style="text-align: right;" Text="" ID="displayGST" runat="server" /> <br />
            <asp:Label class="orderTotals" style="text-align: right;" Text="" ID="displayDelivery" runat="server" Visible="false" />
                <hr style="margin-bottom: 2px; margin-top: 2px; border: 0.5px solid #c1c1c1; width: 90px; margin: 0; margin-left: 410px;" class="orderTotals">
                <asp:Label class="totalLabel"  Text='Total:' runat="server"/><asp:Label class="orderTotals" style="text-align: right;" Text="" ID="displayTotal" runat="server" /> <br />
            </div>
                <asp:Label runat="server" ID="commentsLabel" AssociatedControlID="CommentsTextbox">Special Instructions:</asp:Label><br />
                <asp:TextBox id="CommentsTextbox" runat="server" TextMode="MultiLine" Text="" ReadOnly="true" CssClass="form-control ddl" />
                <asp:ObjectDataSource runat="server" ID="OrderDetailODS" OldValuesParameterFormatString="original_{0}" SelectMethod="ListOrderDetails" TypeName="Shanghai.System.BLL.OrderController">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="OrderNumberTextbox" PropertyName="Text" DefaultValue="" Name="orderID" Type="Int32"></asp:ControlParameter>

                    </SelectParameters>
                </asp:ObjectDataSource>
            </asp:Panel>
           
        </div>
        </div>
</asp:Content>
