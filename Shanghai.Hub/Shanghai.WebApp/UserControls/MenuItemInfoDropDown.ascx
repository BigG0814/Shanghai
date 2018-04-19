<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuItemInfoDropDown.ascx.cs" Inherits="Shanghai.WebApp.UserControls.MenuItemInfoDropDown" %>
<style>
    div.InfoUserControl a:focus, div.InfoUserControl a:hover {
        color:red !important;
    }
</style>
<div class="InfoUserControl">
    <div class="dropdown">
        <a href="#" class="dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
            <i class="fa fa-info-circle" style="font-size: 22px;"></i>
        </a>
        <ul class="dropdown-menu itemInfo" style="left: 25px; top:-6px; z-index:10000;" aria-labelledby="dropdownMenuButton">
            <li><div class="arrow-left" style="border-right: 8px solid rgba(0, 0, 0, .15); position: absolute; top:3px; left: -7px;"></div><div class="arrow-left" style="top:3px; border-right: 6px solid white; position: absolute; left: -5px;"></div><a href="#" class="popuplink" data-toggle="modal" data-target="#addMemo">Add Memo</a></li>
            <li><a href="#" class="popuplink" runat="server" id="DiscountLink" data-toggle="modal" data-target="#addDiscount">Add Discount</a></li>
            <li><a href="#" class="popuplink" data-toggle="modal" data-target="#confirmRemove">Remove</a></li>
            <li runat="server" id="editcomboLI"><asp:LinkButton ID="editItems" Text="Edit Combo Selections" runat="server" OnClick="editItems_Click" /></li>
        </ul>
    </div>

    <div id="addMemo"  class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Add Memo</h4>
                </div>
                <div class="modal-body">
                    <asp:TextBox runat="server" Width="100%" Style="max-width: none;" ID="MemoTextBox" CssClass="form-control" />
                </div>
                <div class="modal-footer">
                    <asp:Button Text="Add Memo" cssClass="btn btn-success" ID="addMemoBtn" OnClick="addMemoBtn_Click" runat="server" />
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>

        </div>
    </div>

    <div id="confirmRemove"  class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Remove Item?</h4>
                </div>
                <div class="modal-footer">
                    <asp:Button Text="Remove" class="btn btn-danger" ID="removeBtn" OnClick="RemoveItemClick" runat="server" />
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>

        </div>
    </div>

    <div id="addDiscount" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Add Discount</h4>
                </div>
                <div class="modal-body">
                    <asp:TextBox TextMode="Number" ID="DiscountBox" Width="100%" min="0.01" Style="max-width: none; display: inline;" CssClass="form-control" step="any" runat="server" />
                </div>
                <div class="modal-footer">
                    <asp:Button Text="Add Discount As %" ID="addDiscountBtn" class="btn btn-success" OnClick="addDiscountBtn_Click" runat="server" />
                    <asp:Button Text="Add Discount As $" ID="addDiscountBtnDollar" CssClass="btn btn-success" OnClick="addDiscountBtnDollar_Click" runat="server" />
                    <asp:Button Text="Clear Existing Discount" ID="ClearDiscountBtn" CssClass="btn btn-danger" OnClick="ClearDiscountBtn_Click" runat="server" />
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>

        </div>
    </div>
</div>





