<%@ Page Title="" Language="C#" MasterPageFile="~/BackEnd.Master" AutoEventWireup="true" CodeBehind="Edit_Job_Types.aspx.cs" Inherits="Shanghai.WebApp.BackEnd_Page.Edit_Job_Types" %>


<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="body-content" style="background-color: white;">
        <style>
            .eContainer {
                background-color: white;
                margin: 0 auto;
            }

            h1 {
                text-align: center;
                font-family: 'Amatic SC', cursive;
            }

            .tempcss {
                margin-left: 0.5em;
                margin-right: 0.5em;
                left: 15em;
            }

            .jumbotron {
                background-color: #eadc5f;
            }

            .a {
                width: 22.5em;
                margin: 0 auto;
                text-align: center;
                text-decoration: none;
            }

            .l {
                padding-right: 1em;
            }

            .space {
                margin-left: 4em;
            }

            .form-control {
                display: inline;
            }

            .pageNav{
                font-family: 'Amatic SC', cursive;
                font-weight: bolder;
                font-size: 2em;
            }

            .emp {
                text-align: left;
                padding: 0 0.5em;
            }

            .emp {
                clear: both;
                margin: 0 auto;
                margin-top: 1.5em;
                max-width: 40em;
            }
            /*.btn{
                margin-left:10px;
            }*/
        </style>

        <div class="container eContainer">

            <div class="jumbotron">
                <h1>Add & Edit Job Types</h1>
            </div>

            <div class="row tab">
                <%-- <div class="col-md-12">--%>
                <ul class="nav nav-tabs pageNav">
                    <li class="active"><a href="#insertjobtype" data-toggle="tab">Add Job Type</a></li>
                    <li><a href="#editjobtype" data-toggle="tab">Edit Job Type</a></li>
                </ul>
                
                <div class="tab-content display">
                    <div class="tab-pane fade" id="editjobtype">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
                                <div class="emp">
                                    <asp:DropDownList ID="JobTypeDDL" runat="server" CssClass="form-control" Style="margin-left: 15px; margin-right: 10px; margin-bottom: 20px;"
                                        AutoPostBack="True" Width="180px" DataSourceID="JobTypeODS" DataTextField="Description" DataValueField="JobTypeID">
                                    </asp:DropDownList>
                                    <asp:ObjectDataSource runat="server" ID="JobTypeODS" OldValuesParameterFormatString="original_{0}" SelectMethod="List_JobTypes" TypeName="Shanghai.System.BLL.ShiftController"></asp:ObjectDataSource>

                                    <asp:Button ID="SearchBtn" runat="server" Text="Search" OnClick="SearchBtn_Click" CausesValidation="false" CssClass="btn btn-info" Style="width: 130px;" />&nbsp;

                                    <div class="form-group col-md-9 ">
                                        <label for="JobNameTextBox">Job Name:</label>
                                        <asp:TextBox ID="JobNameTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <br />
                                    <div class="form-group col-md-12 ">
                                        <label for="ActiveYNCB">Is job type active?:&nbsp;</label>
                                        <asp:CheckBox runat="server" ID="ActiveYNCB" />
                                    </div>
                                     <div class="form-group col-md-12 btn">
                                        <br />
                                        <br />
                                        <asp:Button ID="SubmitChanges" runat="server" OnClick="SubmitChanges_Click" Text="Submit" CssClass="btn btn-success" Style="width: 130px;" />
                                    </div>
                                    
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>


                    </div>
                    <!-- user tab -->
                    <%--  <div class="a">--%>
                    <div class="tab-pane fade in active" id="insertjobtype">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <uc1:MessageUserControl runat="server" ID="MessageUserControl1" />
                                <div class="emp">
                                    <div class="form-group col-md-9 ">
                                        <label for="JobNameTextBox">Job Name:</label>
                                        <asp:TextBox ID="NewJobNameTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <div class="form-group col-md-12 btn">
                            <br />
                            <br />
                            <asp:Button ID="SubmitBtn" runat="server" OnClick="SubmitBtn_Click" Text="Submit" CssClass="btn btn-success" Style="width: 150px;" />
                            <asp:Button ID="ClearBtn" runat="server" OnClick="ClearBtn_Click" Text="Clear" CssClass="btn" Style="width: 150px;" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

