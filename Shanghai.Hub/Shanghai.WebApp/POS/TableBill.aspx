<%@ Page Title="TableBill" Language="C#" MasterPageFile="~/POSMaster.Master" AutoEventWireup="true" CodeBehind="TableBill.aspx.cs" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" Inherits="Shanghai.WebApp.POS.TableBill" %>

<%@ Register Src="~/UserControls/MenuItemInfoDropDown.ascx" TagPrefix="uc1" TagName="MenuItemInfoDropDown" %>
<%@ Register Src="~/UserControls/BillSplitter.ascx" TagPrefix="uc1" TagName="BillSplitter" %>
<%@ Register Src="~/UserControls/BillPayment.ascx" TagPrefix="uc1" TagName="BillPayment" %>
<%@ Register Src="~/UserControls/ComboItemSelector.ascx" TagPrefix="uc1" TagName="ComboItemSelector" %>




<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:ComboItemSelector runat="server" OnItemsUpdated="ComboItemSelector_ItemsUpdated" OnSelectionSubmitted="ItemsSelected" ID="ComboItemSelector" />
    <style>
        #billFoot * {
            border: none !important;
            padding: 2px !important;
        }

        #billFoot {
            border-top: 1px solid #BBB;
            padding: 6px 0;
        }

        .btncontainer {
            display: flex;
            width: 100%;
        }

        .ItemBtnContainer {
            flex: 1;
            padding: 16px;
        }

        @media only screen and (max-width: 600px) {
            .col {
                display: block;
                width: 100%;
            }
        }

        .navbar a {
            margin-top:0px !important;
        }
    </style>
    <script>
        function CheckInsert(s) {
            console.log(s);
            console.log("this happened");
            var regex=/^[0-9-]*$/;
            if (!s.value.match(regex)) {
                s.value = s.value.slice(s.value.length);
            }
            if (s.value.length == 3) {
                s.value = s.value.slice(0, 3) + '-' + s.value.slice(3);
            }
            if (s.value.length == 7) {
                s.value = s.value.slice(0, 7) + '-' + s.value.slice(7);
            }
            if (s.value.length > 11) {
                s.value = s.value.slice(0,11);
            }
        }
    </script>
    <asp:Panel ID="NavPanel" runat="server">
            <nav class="navbar navbar-default" style="position:unset !important">
        <div class="container-fluid">
            <div class="navbar-header">
                <a class="navbar-brand" style="font-weight:600" href="SeatingChart.aspx">Done</a>
            </div>
            <ul class="nav navbar-nav">
                <li>
                    <asp:LinkButton Text="Select Menu Items" OnClick="SelectMenuItems_Click" runat="server" />
                </li>
                <li>
                    <asp:LinkButton OnClick="SplitBillBtn_Click" ID="SplitBillBtn" Text="Split Bills" runat="server" /></li>
                <li>
                    <asp:LinkButton Text="Pay Bills" ID="PayBillBtn" OnClick="tempPayBtn_Click" runat="server" />
                </li>
                
                <li>
                    <asp:LinkButton Text="Adjust Cust. Count/Order Info" OnClick="AdjustCustomerCount_Click" runat="server" />
                </li>
            </ul>
            <ul class="nav navbar-nav navbar-right">
                <li><a href="POSLogIn.aspx">Log Out</a></li>
            </ul>
        </div>
    </nav>
    </asp:Panel>


    <div style="width: 95%; margin: 0 auto; background-color: white;">
        <asp:Panel Width="50%" Visible="false" Style="margin: 20px auto;" ID="CustomerCountPanel" runat="server">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h4 class="modal-title">Customer Count</h4>
                </div>
                <div class="panel-body">
                    <asp:TextBox TextMode="Number" ID="CustomerCountTB" Width="100%" Style="max-width: none; display: inline;" CssClass="form-control" runat="server" />
                </div>
                <div class="panel-footer">
                    <asp:Button Text="Submit" ID="addCustomerCount" CssClass="btn btn-success" OnClick="addCustomerCount_Click" runat="server" />
                    <asp:Button Text="Cancel" ID="cancelCustomerCount" CssClass="btn btn-danger" OnClick="cancelCustomerCount_Click" runat="server" />
                </div>
            </div>
            <asp:Panel Width="100%" Visible="false" Style="margin: 20px auto;" ID="TakeOutPanel" runat="server">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:Label Text="Delivery" ID="HeadingLbl" Font-Bold="true" Font-Size="X-Large" runat="server" />
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-6" runat="server" id="customerInfoPanel">
                            <asp:Label Text="Customer Info" ID="CustomerInfoLabel" runat="server" />
                            <hr />
                            <asp:HiddenField runat="server" ID="isTakeOut" />
                            First Name: <br /> <asp:TextBox CssClass="form-control" runat="server" ID="FNameBox"></asp:TextBox> <br />
                            <asp:RequiredFieldValidator runat="server" ID="FnameValidator" ControlToValidate="FNameBox" Display="Dynamic"></asp:RequiredFieldValidator>
                            Last Name: <br /> <asp:TextBox CssClass="form-control" runat="server" ID="LNameBox"></asp:TextBox><br />
                            <asp:RequiredFieldValidator runat="server" ID="LNameValidator" ControlToValidate="LNameBox" Display="Dynamic"></asp:RequiredFieldValidator>
                            Phone:<br /> <asp:TextBox CssClass="form-control" onkeypress="CheckInsert(this);" runat="server" ID="PhoneBox"></asp:TextBox> <br />
                            <asp:RequiredFieldValidator runat="server" ID="PhoneValidator" ControlToValidate="PhoneBox" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-sm-6" visible="false" runat="server" id="customerAddressPanel">
                            <asp:Label Text="Address Info" ID="CustomerAddressLabel" runat="server"></asp:Label>
                            <hr />
                            Street:<br /> <asp:TextBox CssClass="form-control" runat="server" ID="StreetBox"></asp:TextBox> <br />
                            Apartment #:<br /> <asp:TextBox CssClass="form-control" runat="server" ID="APTBox"></asp:TextBox> <br />
                            City:<br /> <asp:TextBox CssClass="form-control" runat="server" ID="City" Text="Leduc"></asp:TextBox> <br />
                        </div>
                    </div>
                </div>
                <div class="panel-footer">
                    <asp:Button Text="Submit" ID="SubmitOrder" CausesValidation="true" CssClass="btn btn-success" OnClick="SubmitOrder_Click" runat="server" />
                    <asp:Button Text="Cancel" ID="cancelDetails" CssClass="btn btn-danger" OnClick="cancelCustomerCount_Click" runat="server" />
                </div>
            </div>
        </asp:Panel>
        </asp:Panel>
        
        <asp:Panel ID="BillInfoPanel" Visible="false" runat="server">
            <div class="row">
                <div class="col-sm-5" style="padding: 20px 35px;">
                    <asp:Panel ID="BillItemPanel" runat="server">
                        <div class="panel panel-default panel-collapse">
                            <div class="panel-heading">
                                <asp:Label Text="" Font-Size="X-Large" Font-Bold="true" ID="BillLabel" runat="server" />
                            </div>
                            <div class="panel-body" style="">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div style="height: 300px; padding-bottom: 60px; overflow-y: scroll;">
                                            <asp:Repeater ID="BillRepeater" OnItemDataBound="BillRepeater_ItemDataBound" DataSource='<%# allBills %>' runat="server" ItemType="Shanghai.Data.Entities.Bill">
                                                <SeparatorTemplate>
                                                    <br />
                                                </SeparatorTemplate>
                                                <ItemTemplate>
                                                    <asp:Label Text="Check #" Font-Bold="true" Font-Size="Larger" runat="server" /><asp:Label Font-Bold="true" Font-Size="Larger" Text='<%# Item.BillID %>' runat="server" />
                                                    <asp:Repeater ID="BillItemRepeater" OnItemDataBound="BillItemRepeater_ItemDataBound" DataSource='<%# Item.BillItems %>' ItemType="Shanghai.Data.Entities.BillItem" runat="server">
                                                        <HeaderTemplate>
                                                            <table class="table table-striped" style="width: 100%; margin-bottom: 0px;">
                                                                <thead>
                                                                    <tr>
                                                                        <th style="width: 40px"></th>
                                                                        <th style="width: 175px">Item</th>
                                                                        <th style="width: 100px;">Price</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td style="width: 40px">
                                                                    <uc1:MenuItemInfoDropDown isCombo="<%# Item.MenuItem.isCombo %>" OnUpdateItems="MenuItemInfoDropDown_UpdateItems" OnMemoAdded="MenuItemInfoDropDown_MemoAdded" memo='<%# Item.Notes %>' OnDiscountAdded="MenuItemInfoDropDown_DiscountAdded" OnRemove="MenuItemInfoDropDown_Remove" runat="server" ID="MenuItemInfoDropDown" />
                                                                </td>
                                                                <td style="width: 175px">
                                                                    <asp:Label Font-Size="Smaller" Text='<%# Item.ItemName %>' runat="server" />
                                                                    <asp:Label Font-Italic="true" Style="display: block;" ForeColor="#CCCCCC" Font-Size="X-Small" Text='<%# Item.Notes %>' runat="server" />
                                                                    <asp:Repeater runat="server" ID="ItemSelectionsRepeater" ItemType="Shanghai.Data.POCOs.ComboItemDetail">
                                                                        <ItemTemplate>
                                                                            <asp:Label Font-Size="XX-Small" Font-Italic="true" Text='<%# Item.MenuItemName %>' runat="server" /><br />
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                </td>
                                                                <td style="width: 100px">
                                                                    <asp:Label Font-Size="Smaller" Text='<%# Item.SellingPrice.ToString("C") %>' runat="server" />
                                                                    <asp:Label Style="display: block;" Font-Italic="true" ForeColor="#CCCCCC" Font-Size="X-Small" ID="ItemDiscount" runat="server" />
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            </tbody>
                                                </table>
                                                        </FooterTemplate>
                                                    </asp:Repeater>
                                                    <asp:Panel Visible='<%# ((IList)((Repeater)Container.Parent).DataSource).Count > 1 %>' runat="server">
                                                        <table style="width: 100%">
                                                            <tfoot id="billFoot" class="BillFooter">
                                                                <tr style="border-top: 1px solid #999 !important; padding-top: 2px;">
                                                                    <th style="width: 40px"></th>
                                                                    <th style="text-align: right; padding-right: 8px; width: 175px;">
                                                                        <asp:Label Font-Size="Small" Style="display: block" Text="SubTotal:" runat="server" />
                                                                        <asp:Label Font-Size="Small" ID="DiscountPromptlbl" Visible="false" Style="display: block" Text="Discount:" runat="server" />
                                                                        <asp:Label Font-Size="Small" Style="display: block" Text="GST:" runat="server" />
                                                                        <asp:Label Font-Size="Small" Style="display: block" Text="Total:" runat="server" />
                                                                        <asp:Label Font-Size="Small" ID="TipPromptLbl" Visible='<%# Item.Tip > 0 ? true : false %>' ForeColor="#AAAAAA" Style="display: block" Text="Tip:" runat="server" />
                                                                        <asp:Label Font-Size="Small" ID="PaidPromptLbl" Visible='<%# Item.Payments != null ? Item.Payments.Count > 0 ? true : false : false %>' ForeColor="Red" Style="display: block" Text="Paid:" runat="server" />

                                                                    </th>
                                                                    <th style="width: 100px;">
                                                                        <asp:Label Font-Size="Small" Style="display: block" Text="$0.00" Font-Bold="false" ID="SubtotalLabel" runat="server" />
                                                                        <asp:Label Font-Size="Small" Visible="false" Style="display: block" Text="$0.00" Font-Bold="false" ID="DiscountLabel" runat="server" />
                                                                        <asp:Label Font-Size="Small" Style="display: block" Text="$0.00" Font-Bold="false" ID="GSTLabel" runat="server" />
                                                                        <asp:Label Font-Size="Small" Style="display: block" Text="$0.000" Font-Bold="false" ID="TotalLabel" runat="server" />
                                                                        <asp:Label Font-Size="Small" ForeColor="#AAAAAA" Visible='<%# Item.Tip > 0 ? true : false %>' Style="display: block" Text="$0.00" Font-Bold="false" ID="TipAmountLbl" runat="server" />
                                                                        <asp:Label Font-Size="Small" ForeColor="Red" Visible='<%# Item.Payments != null ? Item.Payments.Count > 0 ? true : false : false %>' Style="display: block" Text="$0.00" Font-Bold="false" ID="PaidAmountLbl" runat="server" />
                                                                    </th>
                                                                </tr>
                                                            </tfoot>
                                                        </table>
                                                    </asp:Panel>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                        <div style="position: relative; bottom: 0; height: 100px;">
                                            <table style="width: 100%">
                                                <tfoot id="billFoot" class="BillFooter">
                                                    <tr style="border-top: 1px solid #999 !important; padding-top: 2px;">
                                                        <th style="width: 40px"></th>
                                                        <th style="text-align: right; padding-right: 8px; width: 175px;">
                                                            <asp:Label Font-Size="Smaller" Style="display: block" Text="SubTotal:" runat="server" />
                                                            <asp:Label Font-Size="Smaller" ID="DiscountPromptlbl" Visible="false" Style="display: block" Text="Discount:" runat="server" />
                                                            <asp:Label Font-Size="Smaller" Style="display: block" Text="GST:" runat="server" />
                                                            <asp:Label Font-Size="Smaller" ID="DeliveryFeePrompt" Style="display: block" Visible="false" Text="Delivery Fee:" runat="server" />
                                                            <asp:Label Font-Size="Smaller" Style="display: block" Text="Total:" runat="server" />
                                                            <asp:Label Font-Size="Smaller" Visible="false" ID="tipPromptLbl" ForeColor="#AAAAAA" Style="display: block" Text="Tip:" runat="server" />
                                                            <asp:Label Font-Size="Smaller" Visible="false" ID="paidPromptLbl" ForeColor="Red" Style="display: block" Text="Paid:" runat="server" />
                                                        </th>
                                                        <th style="width: 100px;">
                                                            <asp:Label Font-Size="Smaller" Style="display: block" Text="22.50" Font-Bold="false" ID="SubtotalLabel" runat="server" />
                                                            <asp:Label Font-Size="Smaller" Visible="false" Style="display: block" Text="-22.50" Font-Bold="false" ID="DiscountLabel" runat="server" />
                                                            <asp:Label Font-Size="Smaller" Style="display: block" Text="22.50" Font-Bold="false" ID="GSTLabel" runat="server" />
                                                            <asp:Label Font-Size="Smaller" Style="display: block" Text="22.50" Font-Bold="false" Visible="false" ID="DeliveryFeeLabel" runat="server" />
                                                            <asp:Label Font-Size="Smaller" Style="display: block" Text="143.74" Font-Bold="false" ID="TotalLabel" runat="server" />
                                                            <asp:Label Font-Size="Smaller" Visible="false" ForeColor="#AAAAAA" Style="display: block" Text="22.50" Font-Bold="false" ID="TipAmountLbl" runat="server" />
                                                            <asp:Label Font-Size="Smaller" Visible="false" ForeColor="Red" Style="display: block" Text="143.74" Font-Bold="false" ID="PaidAmountLbl" runat="server" />
                                                        </th>
                                                    </tr>
                                                </tfoot>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="panel-footer">
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:Button Text="Print Bill" CssClass="btn btn-primary" ID="BtnPrintBill" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>

                    </asp:Panel>
                </div>

                <div class="col-sm-7">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div class="row" style="padding-top: 20px;">
                                <div class="col-sm-9">
                                    <asp:Panel runat="server" ID="MenuCategoryItemPanel">
                                        <asp:Repeater ID="MenuCategoryRepeater" ItemType="Shanghai.Data.Entities.MenuCategory" runat="server">
                                            <HeaderTemplate>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Panel runat="server" CssClass="panel panel-default" Visible="false" ID="Panel1">
                                                    <div class="panel-heading">
                                                        <asp:HiddenField ID="HdnCategoryName" Value='<%# Item.CategoryName %>' runat="server" />
                                                        <asp:Label runat="server" Text='<%# Item.CategoryName %>' Font-Bold="true" Font-Size="X-Large"></asp:Label>
                                                    </div>
                                                    <div class="panel-body" style="height: 500px; overflow-y: scroll; overflow-x: hidden;">
                                                        <asp:Repeater ID="MenuItemDataSource" ItemType="Shanghai.Data.Entities.MenuItem" DataSource='<%# Item.MenuItems.ToList() %>' runat="server">
                                                            <HeaderTemplate>
                                                                <div class="row">
                                                                    <div class="col-sm-12">
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <%# (Container.ItemIndex + 4) % 4 == 0 ? "<div class='row'>" : string.Empty %>
                                                                <div class="col-sm-3" style="padding: 5px 15px;">
                                                                    <asp:LinkButton Style="white-space: normal; display: flex; align-items: center; justify-content: center; margin: 5px;" Height="100px" Width="100%" OnClick="MenuItem_Click" CommandArgument='<%# Item.MenuItemID %>' CssClass="btn btn-primary" PostBackUrl="#" runat="server"><p style="width:100%; font-size:10px; font-weight:bold;"><%# Item.MenuItemName %></p></asp:LinkButton>
                                                                </div>
                                                                <%# (Container.ItemIndex + 4) % 4 == 3 ? "</div>" : Container.ItemIndex == ((IList)((Repeater)Container.Parent).DataSource).Count - 1 ?"</div>" : string.Empty%>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                </div>
                                                                    </div>
                                                            </FooterTemplate>
                                                        </asp:Repeater>
                                                    </div>
                                                </asp:Panel>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </asp:Panel>
                                </div>
                                <div class="col-sm-3">
                                    <div style="height: 550px; overflow-y: scroll; overflow-x: hidden;">
                                        <asp:Repeater runat="server" ItemType="Shanghai.Data.Entities.MenuCategory" ID="CategoryRepeater">
                                            <ItemTemplate>
                                                <asp:Button ID="CatBtn" Width="100%" CssClass="btn btn-primary wrap" Text='<%# Item.CategoryName %>' CommandArgument='<%# Item.CategoryName %>' OnCommand="CategorySelected" runat="server" />
                                                <br />
                                                <br />
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="SplitBillsPanel" Visible="false" runat="server">
            <uc1:BillSplitter runat="server" OnTableSplit="BillSplitterControl_TableSplit" ID="BillSplitterControl" />
        </asp:Panel>
        <asp:Panel ID="BillPaymentPanel" Visible="false" runat="server">
            <uc1:BillPayment runat="server" ID="BillPayment" />
        </asp:Panel>
    </div>
    <script>
        var controls = document.querySelectorAll('.InfoUserControl');
        var dropdownlists = document.querySelectorAll('.itemInfo');
        console.log(dropdownlists);
        for (var i = 0; i < dropdownlists.length; i++) {
            var links = dropdownlists[i].querySelectorAll("a.popuplink");
            var modals = controls[i].querySelectorAll('.modal');
            console.log(modals);
            console.log(links);
            for (var x = 0; x < links.length; x++) {
                console.log(links[x]);
                console.log()
                links[x].setAttribute("data-target", links[x].getAttribute("data-target") + i);
                modals[x].setAttribute("id", modals[x].getAttribute("id") + i);
            }
        }
    </script>
</asp:Content>
