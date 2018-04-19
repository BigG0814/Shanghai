<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ComboItemSelector.ascx.cs" Inherits="Shanghai.WebApp.UserControls.ComboItemSelector" %>

<button type="button" id="ShowComboSelector" class="btn btn-info btn-lg" style="display:none;" data-toggle="modal" data-target="#myModal">Open Modal</button>
<div id="myModal" class="modal fade" data-backdrop="static" role="dialog">
    <div class="modal-dialog">
        <!-- Modal content-->
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal">&times;</button>
                <h4 class="modal-title">Please Select <%= AllowedSelections %> Items...</h4>
            </div>
            <div class="modal-body">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <h3>Options</h3>
                        <asp:Label Text="" Font-Bold="true" ForeColor="Firebrick" ID="ErrorTextBox" runat="server" /> <br /> <br />
                        <div style="min-height:200px; max-height:400px; overflow-y:scroll">
                <asp:Repeater runat="server" ID="OptionsRepeater" ItemType="Shanghai.Data.Entities.MenuItem" DataSourceID="OptionDataSource">
                    <ItemTemplate>
                        <div>
                            <asp:Button Text='Add' CssClass="btn btn-success" OnClick="SelectItem_Click" CommandArgument='<%# Item.MenuItemID.ToString() + ";" + Item.MenuItemName.ToString()%>' runat="server" /> &nbsp; &nbsp; <asp:Label Text="<%# Item.MenuItemName %>" runat="server" />
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                            </div>
                        <h3>Selected Items</h3>
                        <div style="min-height:200px; max-height:400px; overflow-y:scroll">
                <asp:Repeater runat="server" ItemType="Shanghai.Data.Entities.MenuItem" DataSource="<%# SelectedItems %>" ID="SelectedItemsRepeater">
                    <ItemTemplate>
                        <div>
                            <asp:Button Text="Remove" CssClass="btn btn-danger" OnClick="RemoveItem_Click" CommandArgument='<%# Item.MenuItemID.ToString() + ";" + Item.MenuItemName.ToString()%>' runat="server" /> &nbsp; &nbsp; <asp:Label Text="<%# Item.MenuItemName %>" runat="server" />
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                            </div>
                <asp:ObjectDataSource runat="server" ID="OptionDataSource" OldValuesParameterFormatString="original_{0}" SelectMethod="getCombinationOptions" TypeName="Shanghai.System.BLL.MenuItemController"></asp:ObjectDataSource>
            
                    </ContentTemplate>
                </asp:UpdatePanel>
                </div>
            <div class="modal-footer">
                <asp:Button Text="Update" OnClick="Update_Click" ID="Update" Visible="false" CssClass="btn btn-success" runat="server" />
                <asp:Button Text="Done" ID="Select" Visible="false" OnClick="Select_Click" CssClass="btn btn-success" runat="server" />
                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
            </div>
        </div>
    </div>
</div>

<script>
    var btn = document.querySelector('#ShowComboSelector');
    function showComboItemPopUp() {
        btn.click();
    }
</script>