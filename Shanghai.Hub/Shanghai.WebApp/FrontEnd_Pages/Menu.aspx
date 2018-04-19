<%@ Page Title="Shanghai Leduc - Menu" Language="C#" MasterPageFile="FrontFacing.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="Shanghai.WebApp.Menu" %>

<%@ Register Src="~/UserControls/ComboItemSelector.ascx" TagPrefix="uc1" TagName="ComboItemSelector" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        @media(min-width: 768px) {
            .modal-dialog{
            right: auto;
            left: 0 !important;
            width: 600px;
            padding-top: 30px;
            padding-bottom: 30px;
            }

        }
    </style>
<script src="https://cdnjs.cloudflare.com/ajax/libs/clipboard.js/1.5.10/clipboard.min.js"></script>
<script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
<script src="https://netdna.bootstrapcdn.com/bootstrap/3.0.0/js/bootstrap.min.js"></script>
<link href="https://netdna.bootstrapcdn.com/bootstrap/3.0.0/css/bootstrap.min.css" rel="stylesheet"/>
<script>
    $('.confirm').tooltip({
        trigger: 'click',
        placement: 'bottom'
    });

    function setTooltip(btn, message) {
        btn.tooltip('hide')
          .attr('data-original-title', message)
          .tooltip('show');
    }

    function hideTooltip(btn) {
        setTimeout(function () {
            btn.tooltip('hide');
        }, 5000);
    }

    // Clipboard

    var clipboard = new Clipboard('.confirm');

    clipboard.on('success', function (e) {
        var btn = $(e.trigger);
        setTooltip(btn, 'Item added');
        hideTooltip(btn);
    });

</script>

    <asp:Panel ID="MessagePanel" runat="server">
        <div class="alert alert-danger" style="margin-bottom: 0 !important;">
            <strong>Note:</strong> Shanghai Leduc is currently <b>closed</b> and will be open for ordering between 10:00am-10:00pm on weekdays, and 4:00pm-10:00pm on weekends.
        </div>
    </asp:Panel>
    

    <div class="container mainContainer clearFix">
        <div class="menuTitle">
            <h2>Menu</h2>
            <p>To Order Call <a href="tel:780-986-1862">780-986-1862</a> OR <a href="tel:780-986-1883">780-986-1883</a><br />Or Order Online Now!</p>
        </div>
        
        <div class="grid">
                <asp:Repeater ID="CategoryRepeater" runat="server" ItemType="Shanghai.Data.Entities.DTOs.Category" DataSourceID="MenuItemsODS">
                <ItemTemplate>
                    <div class="menuCategory grid-item">
                        <h2><%# Item.CatName %></h2>

                        <asp:Repeater ID="ItemsRepeater" runat="server" DataSource="<%# Item.Items %>" ItemType="Shanghai.Data.POCOs.MenuItemInfo">
                           <HeaderTemplate>
                               <table class="table" style="margin-bottom: 0;">
                           </HeaderTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                            <ItemTemplate>
                                <tr>
                                    <th><%# Item.ItemName %>
                                        <asp:Label Font-Size="Smaller" Font-Italic="true" ForeColor="Gray" Font-Bold="false" Text='<%# String.IsNullOrEmpty(Item.Description)? "" : Item.Description %>' style="display:block;" Visible="<%# !String.IsNullOrEmpty(Item.Description) %>" runat="server" />
                                    </th>
                                    <td class="price"><%# $"{Item.Price:C}" %></td>
                                    <td>
                                        <asp:LinkButton CssClass="linkButton" ID="addbtn" CommandArgument='<%# Item.ItemID +";" + Item.isCombo + ";" + Item.AmountOfSelections%>' ClientIDMode="AutoID" CommandName="Select" runat="server" OnCommand="AddToCart_Command">
                                            <div class="confirm" data-clipboard-text="1"><i class="fa fa-plus-circle" style="font-size:30px;"></i></div>
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                           
                        </asp:Repeater>

                   </div>
                </ItemTemplate>
            
            </asp:Repeater>
            </div>
            <asp:ObjectDataSource ID="MenuItemsODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="MenuItemsByCategory" TypeName="Shanghai.System.BLL.MenuItemController"></asp:ObjectDataSource>
        <uc1:ComboItemSelector runat="server" OnSelectionSubmitted="ComboItemSelector_SelectionSubmitted" ID="ComboItemSelector" />
           </div>
</asp:Content>
