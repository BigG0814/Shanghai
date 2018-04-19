<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BillPayment.ascx.cs" Inherits="Shanghai.WebApp.UserControls.BillPayment" %>

<script type="text/javascript">
</script>
<div class="row">
    <div class="col-sm-5 clearfix">
        <asp:Panel ID="BillItemPanel" runat="server">
            <div class="panel panel-default panel-collapse">
                <div class="panel-heading clearfix" style="text-align:center;">
                    <asp:Button Text="Previous bill" OnClick="PrevBillBtn_Click" ID="PrevBillBtn" CssClass="btn btn-sm btn-primary pull-left" runat="server" />
                    <asp:Label Text="Bill #1" CssClass="center" Font-Size="X-Large" Font-Bold="true" ID="BillLabel" runat="server" />
                    <asp:Button Text="Next Bill" OnClick="NextBillBtn_Click" ID="NextBillBtn" CssClass="btn btn-sm btn-primary pull-right" runat="server" />
                </div>
                <div class="panel-body" style="">
                    <div class="row">
                        <div class="col-md-12">
                            <div style="height: 280px; padding-bottom: 60px; overflow-y: scroll;">
                                <asp:Repeater ID="BillItemRepeater" ItemType="Shanghai.Data.Entities.BillItem" runat="server">
                                    <HeaderTemplate>
                                        <table class="table table-striped" style="width: 100%; margin-bottom: 0px;">
                                            <thead>
                                                <tr>
                                                    <th style="width: 175px">Item</th>
                                                    <th style="width: 100px;">Price</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td style="width: 175px">
                                                <asp:Label Font-Size="Smaller" Text='<%# Item.ItemName %>' runat="server" />
                                                <asp:Label Font-Italic="true" Style="display: block;" ForeColor="#CCCCCC" Font-Size="X-Small" Text='<%# Item.Notes %>' runat="server" />
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
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel-footer">
                    <table class="table" style="width: 100%; border:1px solid #EEEEEE">
                                    <tbody style="background-color:white;">
                                        <tr>
                            <td style="text-align:right; width: 175px">
                                <asp:Label Style="display: block" Text="SubTotal:" runat="server" /></td>
                            <td style="width: 100px">
                                <asp:Label Style="display: block" Text="22.50" Font-Bold="false" ID="SubtotalLabel" runat="server" /></td>
                        </tr>
                        <tr runat="server" id="discountRow">
                            <td style="text-align:right; width: 175px">
                                <asp:Label ID="DiscountPromptlbl" Visible="false" Style="display: block" Text="Discount:" runat="server" /></td>
                            <td style="width: 100px">
                                <asp:Label Visible="false" Style="display: block" Text="-22.50" Font-Bold="false" ID="DiscountLabel" runat="server" /></td>
                        </tr>
                        <tr runat="server" id="deliveryRow">
                            <td style="text-align:right; width: 175px">
                                <asp:Label ID="DeliveryFeePromptLbl" Visible="false" Style="display: block" Text="Delivery Fee" runat="server" /></td>
                            <td style="width: 100px">
                                <asp:Label Visible="false" Style="display: block" ID="DeliveryFeeLabel" runat="server" /></td>
                        </tr>
                        <tr>
                            <td style="text-align:right; width: 175px">
                                <asp:Label Style="display: block" Text="GST:" runat="server" /></td>
                            <td style="width: 100px">
                                <asp:Label Style="display: block" Text="GST" Font-Bold="false" ID="GSTLabel" runat="server" /></td>
                        </tr>
                        <tr>
                            <td style="text-align:right; width: 175px">
                                <asp:Label Style="display: block" Text="Total:" runat="server" /></td>
                            <td style="width: 100px">
                                <asp:Label Style="display: block" Text="Total" Font-Bold="false" ID="TotalLabel" runat="server" /></td>
                        </tr>
                                    </tbody>
                                </table>
                </div>
            </div>
        </asp:Panel>
    </div>
    <div class="col-sm-7">
        <div class="panel panel-default">
            <div class="panel-heading">
                <asp:Label Text="Add Payment" Font-Bold="true" Font-Size="Large" runat="server" />
            </div>
            <div class="panel-body">
                <table class="table table-striped" style="width: 100%; text-align: right;">
                    <tbody>
                        
                        
                        <tr>
                            <td style="text-align:right;">
                                <asp:Label Style="display: block" Text="Payment Type: " runat="server" />
                            </td>
                            <td>
                                <asp:DropDownList runat="server" CssClass="form-control" ID="PaymentTypeDropDown" Width="150px" DataSourceID="ObjectDataSource2" DataTextField="PaymentDescription" DataValueField="PaymentTypeID">
                                </asp:DropDownList>
                                <asp:ObjectDataSource runat="server" ID="ObjectDataSource2" OldValuesParameterFormatString="original_{0}" SelectMethod="Payment_List" TypeName="Shanghai.System.BLL.PaymentTypeController"></asp:ObjectDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:right;">
                                <asp:Label Style="display: block" Text="Payment Amount: " runat="server" />
                            </td>
                            <td>
                                <asp:TextBox ID="PaymentAmountTB" Width="250px" TextMode="Number" step="0.01" min="0" ClientIDMode="Static" CssClass="form-control" runat="server" />
                            </td>
                        </tr>
                        
                        <tr runat="server" id="paidRow">
                            <td style="vertical-align:middle; text-align:right;" >
                                <asp:Label Visible="false" style="display:block;" ID="PaidPrompt" Text="Payments:" runat="server" />
                            </td>
                            <td>
                                <div style="max-height:200px; overflow-y:scroll;">
                                <asp:Repeater ID="PaymentRepeater" ItemType="Shanghai.Data.Entities.BillPayment" runat="server">
                                    <ItemTemplate>
                                        <asp:Label Font-Strikeout="<%# Item.isVoid %>" ForeColor='<%# Item.isVoid? System.Drawing.Color.Firebrick : System.Drawing.Color.ForestGreen %>' Text='<%# Item.PaymentType.PaymentDescription + ":   " %>' runat="server" /> 
                                        <asp:Label Font-Strikeout="<%# Item.isVoid %>" Text='<%# Item.PaymentAmount.ToString("C2") %>' ForeColor='<%# Item.isVoid? System.Drawing.Color.Firebrick : System.Drawing.Color.ForestGreen  %>' runat="server" /> 
                                        <asp:Button Text='<%# Item.isVoid == true ? "Re-Open" : "Void" %>' CssClass='<%# Item.isVoid == true? "btn btn-success btn-sm" : "btn btn-danger btn-sm"  %>' OnClick="VoidPayment_Click" CommandArgument="<%# Item.BillPaymentID %>" runat="server" /> <br /> <br />
                                    </ItemTemplate>
                                </asp:Repeater>
                                </div>
                            </td>
                        </tr>
                        <tr runat="server"  id="totalPaid">
                            <td style="text-align:right;">
                                <asp:Label Text="Total Paid: " style="vertical-align:bottom" Font-Bold="true" runat="server" />
                            </td>
                            <td>
                                <asp:Label Text="$0.00" ID="TotalPaidLbl" Font-Bold="true" Style="vertical-align:bottom" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:right;">
                                <asp:Label Style="display: block" Text="Outstanding Amount" runat="server" /></td>
                            <td>
                                <asp:Label Style="display: block" Text="Outstanding" Font-Bold="false" ID="OutstandingLbl" runat="server" /></td>
                        </tr>
                        <tr runat="server" id="tipRow">
                            <td style="text-align:right;">
                                <asp:Label Visible="false" ID="TipPrompt" Style="display: block" Text="Tip:" runat="server" /></td>
                            <td>
                                <asp:Label Visible="false" Style="display: block" Text="Tip" Font-Bold="false" ID="TipAmountLbl" runat="server" /></td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="panel-footer">
                 <asp:Button Text="Enter Payment" ID="SavePaymentBtn" OnClick="SavePaymentBtn_Click" CssClass="btn btn-success btn-lg" runat="server" />
                &nbsp; &nbsp; <asp:Button Text="Close Bill" ID="CloseBillBtn" OnClick="CloseBillBtn_Click" CssClass="btn btn-warning" runat="server" />
            </div>
        </div>

    </div>
</div>
