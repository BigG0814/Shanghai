<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BillSplitter.ascx.cs" Inherits="Shanghai.WebApp.UserControls.BillSplitter" %>
<style>
    .tempBill {
        height: 250px;
        overflow-y: scroll;
        width: 100%;
        border: 1px solid #DDDDDD;
        padding-bottom: 15px;
        border-radius: 5px;
    }

    .tempBill:hover {
        cursor:pointer;
    }

    .billSplitterItem {
        font-size: 13px;
        color: black;
        padding: 8px;
        border-top: 1px solid #EEEEEE;
        border-bottom: 1px solid #EEEEEE;
    }
</style>
<script>
    function BillClicked(s, e) {
        console.log('Hello');
        console.log(s);
        console.log(e);
        console.log(s.querySelector('a'));
        s.querySelector('a').click();
    }
</script>
<div class="row">
    <div class="col-sm-2">
        <ul class="nav nav-pills nav-stacked">
            <li>
                <asp:Button Text="Add Bill" Width="100%" ID="AddTableBtn" CssClass="btn btn-primary" OnClick="AddTableBtn_Click" runat="server" />
            </li>
            <li>
                <a href="#" runat="server" class="btn btn-primary" id="SplitMenuItemBtn" data-toggle="modal" data-target="#SplitItemModal">Split Item</a>
            </li>
            <li>
                <asp:Button Text="Save Changes" Width="100%" ID="SaveChangesBtn" CssClass="btn btn-success" OnClick="SaveChangesBtn_Click" runat="server" />
            </li>
            <li>
                <asp:Button Text="Cancel" Width="100%" ID="CancelBtn" CssClass="btn btn-danger" OnClick="CancelBtn_Click" runat="server" />
            </li>
        </ul>
    </div>
    <div class="col-sm-10">
        <asp:Repeater ID="TempBillRepeater" ItemType="Shanghai.Data.POCOs.TempBill" runat="server">
            <HeaderTemplate>
                <div class="row">
                    <div class="col-sm-12">
            </HeaderTemplate>
            <ItemTemplate>
                <%# (Container.ItemIndex + 3) % 3 == 0 ? "<div class='row'>" : string.Empty %>
                <div class="col-sm-3">
                    <div class="tempBill" onclick="BillClicked(this, <%# Container.ItemIndex %>)">
                        <asp:LinkButton Text="" OnClick="BillDiv_Click" CommandArgument='<%# Container.ItemIndex %>' runat="server" />
                        <asp:HiddenField ID="BillIndex" Value="<%# Container.ItemIndex %>" runat="server" />
                        <asp:Repeater runat="server" DataSource="<%# Item.items %>" ItemType="Shanghai.Data.POCOs.BillItemDetail">
                            <ItemTemplate>
                                <asp:Button Width="100%" BackColor="White" ID="ItemBtn" Text='<%# Item.MenuItemName %>' CssClass="billSplitterItem" CommandArgument="<%# Container.ItemIndex %>" OnClick="ItemBtn_Click" runat="server" />
                                
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
                <%# (Container.ItemIndex + 3) % 3 == 2 ? "</div>" : Container.ItemIndex == ((IList)((Repeater)Container.Parent).DataSource).Count - 1 ?"</div>" : string.Empty%>
            </ItemTemplate>
            <FooterTemplate>
                </div>
                </div>
            </FooterTemplate>
        </asp:Repeater>
        
        <div id="SplitItemModal" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">How Many Ways?</h4>
                    </div>
                    <div class="modal-body">
                        <asp:TextBox TextMode="Number" ID="SplitNumber_tb" Width="100%" min="1" Text="1" Style="max-width: none; display: inline;" CssClass="form-control" step="any" runat="server" />
                    </div>
                    <div class="modal-footer">
                        <asp:Button Text="Split" CssClass="btn btn-success" ID="SplitItemBtn" OnClick="SplitItemBtn_Click" runat="server" />
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>

            </div>
        </div>
    </div>
</div>
