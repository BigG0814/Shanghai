<%@ Page Title="" Language="C#" MasterPageFile="~/BackEnd.Master" AutoEventWireup="true" CodeBehind="ViewSchedule.aspx.cs" Inherits="Shanghai.WebApp.Scheduling.ViewSchedule" %>

<%@ Register Src="~/UserControls/DatePicker.ascx" TagPrefix="uc1" TagName="DatePicker" %>
<%@ Register Src="~/UserControls/AddShifts.ascx" TagPrefix="uc1" TagName="AddShifts" %>
<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>
<%@ Register Src="~/UserControls/ShiftTrade.ascx" TagPrefix="uc1" TagName="ShiftTrade" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .thisContainer {
            background-color: #ffffff;
            z-index: -1;
        }

        .scheduleTableColumn {
            width: 14.29%;
            border: 2px dotted;
            padding: 0.25em;
            text-align: center;
            vertical-align: top;
        }

        th {
            text-align: center;
        }

        .shiftTable {
            /*background-color: #f2e6a3;*/
            width: 100%;
            margin-left: 0.75em;
            padding: 0;
        }

            .shiftTable th {
                font-family: 'Amatic SC', cursive;
                font-weight: 900;
                font-size: 3rem;
            }

        .weekSelecting {
            background: #eadc5f;
            padding: 2em 3em;
            margin: 1em;
        }

            .weekSelecting p {
                display: inline-block;
                width: 2.6em;
            }

        .bindedWeek {
            width: 21.25em;
        }

        .nextWeek {
            right: 0;
            position: relative;
        }

        .previousWeek {
            position: relative;
            left: 0;
        }

        /*.userPlus {
            height: 30px;
            box-shadow: 2px 2px 4px #888888;
            background-color: rgba(247, 202, 60, 0.17);
            font-size: 20px;
            padding: 2px;
        }*/

        .jobShiftItem {
            margin-bottom: 0.5em;
            box-shadow: 2px 2px 4px #888888;
            border:1px solid #EEE;
        }
    </style>
    <div class="container thisContainer" style="min-width:774px !important;">
        <div class="body-content">
            <div class="row weekSelecting jumbotron clearfix">
                <div class="col-sm-2 pull-left ">
                    <asp:Button Text="Previous Week" CssClass="btn btn-info btn-sm" runat="server" OnClick="PreviousWeek_Click" />
                </div>

                
                <div class="col-sm-8" style="text-align:center; float:left;">
                    <div >
                    <span style="font: italic bold 1em Georgia, serif;">Week of: </span> <br />
                            <uc1:DatePicker runat="server" OnDateChanged="DatePicker_DateChanged" ID="DatePicker" />
                    </div>
                </div>

                <div class="col-sm-2 pull-right">
                    <asp:Button Text="Next Week" CssClass="btn btn-info btn-sm" runat="server" OnClick="NextWeek_Click" Width="120" />
                </div>
                
            </div>
            <div style="text-align:center;">
                    <asp:Button Text="Copy Schedule to Next Week" CssClass="btn btn-default btn-sm" OnClick="SetNextWeekSchedule_Click" runat="server" Visible='<%# isManager %>' ID="setnextweeksched" />
                
                
                    <asp:Button Text="Set all these shifts as finalized" CssClass="btn btn-default btn-sm" OnClick="FinalizeWeekShifts_Click" runat="server" Visible='<%# isManager %>' ID="setfinalized" />
                
               
                    <asp:Button Text="Set all shifts to unfinalized" CssClass="btn btn-default btn-sm" OnClick="UnFinalizeWeekShifts_Click" runat="server" Visible='<%# isManager %>' ID="setUnfinalized"  />
                </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:CheckBox ID="SelfScheduleCB" AutoPostBack="true" runat="server" OnCheckedChanged="ViewSelfSchedule_Checked" Text=" View My Schedule" />
                </div>
            </div>
            <div class="row">
                <div>
                    <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 shiftTable">
                    <table style="width: 100%">
                        <thead>
                            <tr style="border-bottom: 2px solid;">
                                <th>Monday</th>
                                <th>Tuesday</th>
                                <th>Wednesday</th>
                                <th>Thursday</th>
                                <th>Friday</th>
                                <th>Saturday</th>
                                <th>Sunday</th>
                            </tr>
                        </thead>
                        <tr>
                            <td class="scheduleTableColumn">
                                <asp:Repeater ID="MondayRepeater" ItemType="Shanghai.Data.POCOs.ShiftGroup" runat="server">
                                    <ItemTemplate>
                                        <asp:HiddenField Value='<%# Item.Date %>' ID="ShiftDate" runat="server" />
                                        <asp:Label ForeColor="#3399ff" Font-Underline="true" Font-Bold="true" Font-Size="Medium" Text='<%# Item.JobDescription %>' runat="server" />
                                        <asp:Label ID="JobTypeCountLabel" ForeColor="#3399ff" Font-Underline="true" Font-Bold="true" Font-Size="Small" runat="server" Text="(0)" />
                                        <br />

                                        <%--Inner Shift Repeater--%>
                                        <asp:Repeater ID="InnerRepeater" OnItemDataBound="InnerRepeaterItemDataBound" ItemType="Shanghai.Data.POCOs.ShiftSummary" DataSource='<%# Item.Shifts %>' runat="server">
                                            <ItemTemplate>
                                                <div class="jobShiftItem" style="padding:5px;" >
                                                    
                                                    <!--Checkmark if the shift is finalized -->
                                                     
                                                    <%--trash icon--%>
                                                         

                                                    <%--Employee Shift--%>
                                                    <p><span Visible='<%# isManager && Item.isFinal %>' class="pull-left" runat="server" >
                                            <i class="fa fa-check" style="font-size:16px;""></i>
                                                    </span><asp:Label Text='<%# Item.EmployeeName %>' ID="EmpName" runat="server" /> <span>
                                                    <asp:LinkButton Visible='<%# isManager %>' OnClientClick="if (!confirm('Are you sure you want delete?')) return false;" CssClass="linkButton pull-right" runat="server" CommandArgument='<%# Item.ShiftID %>' OnCommand="DeleteShift">
                                            <i class="fa fa-trash-o" style="font-size:20px;"></i>
                                                    </asp:LinkButton> </span></p>

                                                    <asp:Label Text='<%# Item.StartTime.ToShortTimeString().ToString() + " - " + Item.EndTime.ToShortTimeString().ToString() %>'  runat="server" /><br />
                                                    <span><asp:LinkButton CssClass="linkButton" runat="server" ID="linkBtn_Trade" CommandArgument='<%# Item.ShiftID %>' OnCommand="PostShiftTrade">
                                            <i class="fa fa-exchange" runat="server" id="TradeBtn" style="font-size:20px;"></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>

                                        <%--Insert Day Shift Button--%>
                                        <div class="userPlus">
                                            <asp:LinkButton Visible='<%# isManager %>' CssClass="linkButton" CommandArgument='<%# Item.JobType + ":" + Item.JobDescription %>' runat="server" OnCommand="AddShift_icon_Click" Font-Underline="True">
                                            <div class="confirm" data-clipboard-text="1"><i class="fa fa-user-plus"></i></div>
                                            </asp:LinkButton>
                                        </div>

                                        <br />
                                        <br />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </td>
                            <td class="scheduleTableColumn">
                                <asp:Repeater ID="TuesdayRepeater" ItemType="Shanghai.Data.POCOs.ShiftGroup" runat="server">
                                    <ItemTemplate>
                                        <asp:HiddenField Value='<%# Item.Date %>' ID="ShiftDate" runat="server" />
                                        <asp:Label ForeColor="#3399ff" Font-Underline="true" Font-Bold="true" Font-Size="Medium" Text='<%# Item.JobDescription %>' runat="server" />
                                        <asp:Label ID="JobTypeCountLabel" ForeColor="#3399ff" Font-Underline="true" Font-Bold="true" Font-Size="Small" runat="server" Text="(0)" />
                                        <br />
                                        <asp:Repeater ID="InnerRepeater" OnItemDataBound="InnerRepeaterItemDataBound" ItemType="Shanghai.Data.POCOs.ShiftSummary" DataSource='<%# Item.Shifts %>' runat="server">
                                            <ItemTemplate>
                                                <div class="jobShiftItem" style="padding:5px;" >
                                                    
                                                    <!--Checkmark if the shift is finalized -->
                                                     
                                                    <%--trash icon--%>
                                                         

                                                    <%--Employee Shift--%>
                                                    <p><span Visible='<%# isManager && Item.isFinal %>' class="pull-left" runat="server" >
                                            <i class="fa fa-check" style="font-size:16px;""></i>
                                                    </span><asp:Label Text='<%# Item.EmployeeName %>' ID="EmpName" runat="server" /> <span>
                                                    <asp:LinkButton Visible='<%# isManager %>' OnClientClick="if (!confirm('Are you sure you want delete?')) return false;" CssClass="linkButton pull-right" runat="server" CommandArgument='<%# Item.ShiftID %>' OnCommand="DeleteShift">
                                            <i class="fa fa-trash-o" style="font-size:20px;"></i>
                                                    </asp:LinkButton> </span></p>

                                                    <asp:Label Text='<%# Item.StartTime.ToShortTimeString().ToString() + " - " + Item.EndTime.ToShortTimeString().ToString() %>'  runat="server" /><br />
                                                    <span><asp:LinkButton CssClass="linkButton" runat="server" ID="linkBtn_Trade" CommandArgument='<%# Item.ShiftID %>' OnCommand="PostShiftTrade">
                                            <i class="fa fa-exchange" runat="server" id="TradeBtn" style="font-size:20px;"></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>

                                        <div class="userPlus">
                                            <asp:LinkButton Visible='<%# isManager %>' CssClass="linkButton" CommandArgument='<%# Item.JobType + ":" + Item.JobDescription %>' runat="server" OnCommand="AddShift_icon_Click" Font-Underline="True">
                                            <div class="confirm" data-clipboard-text="1"><i class="fa fa-user-plus"></i></div>
                                            </asp:LinkButton>
                                        </div>
                                        <br />
                                        <br />
                                    </ItemTemplate>
                                </asp:Repeater>

                            </td>
                            <td class="scheduleTableColumn">
                                <asp:Repeater ID="WednesdayRepeater" ItemType="Shanghai.Data.POCOs.ShiftGroup" runat="server">
                                    <ItemTemplate>
                                        <asp:HiddenField Value='<%# Item.Date %>' ID="ShiftDate" runat="server" />
                                        <asp:Label ForeColor="#3399ff" Font-Underline="true" Font-Bold="true" Font-Size="Medium" Text='<%# Item.JobDescription %>' runat="server" />
                                        <asp:Label ID="JobTypeCountLabel" ForeColor="#3399ff" Font-Underline="true" Font-Bold="true" Font-Size="Small" runat="server" Text="(0)" />
                                        <br />
                                        <asp:Repeater ID="InnerRepeater" OnItemDataBound="InnerRepeaterItemDataBound" ItemType="Shanghai.Data.POCOs.ShiftSummary" DataSource='<%# Item.Shifts %>' runat="server">
                                            <ItemTemplate>
                                                <div class="jobShiftItem" style="padding:5px;" >
                                                    
                                                    <!--Checkmark if the shift is finalized -->
                                                     
                                                    <%--trash icon--%>
                                                         

                                                    <%--Employee Shift--%>
                                                    <p><span Visible='<%# isManager && Item.isFinal %>' class="pull-left" runat="server" >
                                            <i class="fa fa-check" style="font-size:16px;""></i>
                                                    </span><asp:Label Text='<%# Item.EmployeeName %>' ID="EmpName" runat="server" /> <span>
                                                    <asp:LinkButton Visible='<%# isManager %>' OnClientClick="if (!confirm('Are you sure you want delete?')) return false;" CssClass="linkButton pull-right" runat="server" CommandArgument='<%# Item.ShiftID %>' OnCommand="DeleteShift">
                                            <i class="fa fa-trash-o" style="font-size:20px;"></i>
                                                    </asp:LinkButton> </span></p>

                                                    <asp:Label Text='<%# Item.StartTime.ToShortTimeString().ToString() + " - " + Item.EndTime.ToShortTimeString().ToString() %>'  runat="server" /><br />
                                                    <span><asp:LinkButton CssClass="linkButton" runat="server" ID="linkBtn_Trade" CommandArgument='<%# Item.ShiftID %>' OnCommand="PostShiftTrade">
                                            <i class="fa fa-exchange" runat="server" id="TradeBtn" style="font-size:20px;"></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>

                                        <div class="userPlus">
                                            <asp:LinkButton Visible='<%# isManager %>' CssClass="linkButton" CommandArgument='<%# Item.JobType + ":" + Item.JobDescription %>' runat="server" OnCommand="AddShift_icon_Click" Font-Underline="True">
                                            <div class="confirm" data-clipboard-text="1"><i class="fa fa-user-plus"></i></div>
                                            </asp:LinkButton>
                                        </div>
                                        <br />
                                        <br />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </td>
                            <td class="scheduleTableColumn">
                                <asp:Repeater ID="ThursdayRepeater" ItemType="Shanghai.Data.POCOs.ShiftGroup" runat="server">
                                    <ItemTemplate>
                                        <asp:HiddenField Value='<%# Item.Date %>' ID="ShiftDate" runat="server" />
                                        <asp:Label ForeColor="#3399ff" Font-Underline="true" Font-Bold="true" Font-Size="Medium" Text='<%# Item.JobDescription %>' runat="server" />
                                        <asp:Label ID="JobTypeCountLabel" ForeColor="#3399ff" Font-Underline="true" Font-Bold="true" Font-Size="Small" runat="server" Text="(0)" />
                                        <br />
                                        <asp:Repeater ID="InnerRepeater" OnItemDataBound="InnerRepeaterItemDataBound" ItemType="Shanghai.Data.POCOs.ShiftSummary" DataSource='<%# Item.Shifts %>' runat="server">
                                            <ItemTemplate>
                                                <div class="jobShiftItem" style="padding:5px;" >
                                                    
                                                    <!--Checkmark if the shift is finalized -->
                                                     
                                                    <%--trash icon--%>
                                                         

                                                    <%--Employee Shift--%>
                                                    <p><span Visible='<%# isManager && Item.isFinal %>' class="pull-left" runat="server" >
                                            <i class="fa fa-check" style="font-size:16px;""></i>
                                                    </span><asp:Label Text='<%# Item.EmployeeName %>' ID="EmpName" runat="server" /> <span>
                                                    <asp:LinkButton Visible='<%# isManager %>' OnClientClick="if (!confirm('Are you sure you want delete?')) return false;" CssClass="linkButton pull-right" runat="server" CommandArgument='<%# Item.ShiftID %>' OnCommand="DeleteShift">
                                            <i class="fa fa-trash-o" style="font-size:20px;"></i>
                                                    </asp:LinkButton> </span></p>

                                                    <asp:Label Text='<%# Item.StartTime.ToShortTimeString().ToString() + " - " + Item.EndTime.ToShortTimeString().ToString() %>'  runat="server" /><br />
                                                    <span><asp:LinkButton CssClass="linkButton" runat="server" ID="linkBtn_Trade" CommandArgument='<%# Item.ShiftID %>' OnCommand="PostShiftTrade">
                                            <i class="fa fa-exchange" runat="server" id="TradeBtn" style="font-size:20px;"></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>

                                        <div class="userPlus">
                                            <asp:LinkButton Visible='<%# isManager %>' CssClass="linkButton" CommandArgument='<%# Item.JobType + ":" + Item.JobDescription %>' runat="server" OnCommand="AddShift_icon_Click" Font-Underline="True">
                                            <div class="confirm" data-clipboard-text="1"><i class="fa fa-user-plus"></i></div>
                                            </asp:LinkButton>
                                        </div>
                                        <br />
                                        <br />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </td>
                            <td class="scheduleTableColumn">
                                <asp:Repeater ID="FridayRepeater" ItemType="Shanghai.Data.POCOs.ShiftGroup" runat="server">
                                    <ItemTemplate>
                                        <asp:HiddenField Value='<%# Item.Date %>' ID="ShiftDate" runat="server" />
                                        <asp:Label ForeColor="#3399ff" Font-Underline="true" Font-Bold="true" Font-Size="Medium" Text='<%# Item.JobDescription %>' runat="server" />
                                        <asp:Label ID="JobTypeCountLabel" ForeColor="#3399ff" Font-Underline="true" Font-Bold="true" Font-Size="Small" runat="server" Text="(0)" />
                                        <br />
                                        <asp:Repeater ID="InnerRepeater" OnItemDataBound="InnerRepeaterItemDataBound" ItemType="Shanghai.Data.POCOs.ShiftSummary" DataSource='<%# Item.Shifts %>' runat="server">
                                            <ItemTemplate>
                                                <div class="jobShiftItem" style="padding:5px;" >
                                                    
                                                    <!--Checkmark if the shift is finalized -->
                                                     
                                                    <%--trash icon--%>
                                                         

                                                    <%--Employee Shift--%>
                                                    <p><span Visible='<%# isManager && Item.isFinal %>' class="pull-left" runat="server" >
                                            <i class="fa fa-check" style="font-size:16px;""></i>
                                                    </span><asp:Label Text='<%# Item.EmployeeName %>' ID="EmpName" runat="server" /> <span>
                                                    <asp:LinkButton Visible='<%# isManager %>' OnClientClick="if (!confirm('Are you sure you want delete?')) return false;" CssClass="linkButton pull-right" runat="server" CommandArgument='<%# Item.ShiftID %>' OnCommand="DeleteShift">
                                            <i class="fa fa-trash-o" style="font-size:20px;"></i>
                                                    </asp:LinkButton> </span></p>

                                                    <asp:Label Text='<%# Item.StartTime.ToShortTimeString().ToString() + " - " + Item.EndTime.ToShortTimeString().ToString() %>'  runat="server" /><br />
                                                    <span><asp:LinkButton CssClass="linkButton" runat="server" ID="linkBtn_Trade" CommandArgument='<%# Item.ShiftID %>' OnCommand="PostShiftTrade">
                                            <i class="fa fa-exchange" runat="server" id="TradeBtn" style="font-size:20px;"></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>

                                        <div class="userPlus">
                                            <asp:LinkButton Visible='<%# isManager %>' CssClass="linkButton" CommandArgument='<%# Item.JobType + ":" + Item.JobDescription %>' runat="server" OnCommand="AddShift_icon_Click" Font-Underline="True">
                                            <div class="confirm" data-clipboard-text="1"><i class="fa fa-user-plus"></i></div>
                                            </asp:LinkButton>
                                        </div>
                                        <br />
                                        <br />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </td>
                            <td class="scheduleTableColumn">
                                <asp:Repeater ID="SaturdayRepeater" ItemType="Shanghai.Data.POCOs.ShiftGroup" runat="server">
                                    <ItemTemplate>
                                        <asp:HiddenField Value='<%# Item.Date %>' ID="ShiftDate" runat="server" />
                                        <asp:Label ForeColor="#3399ff" Font-Underline="true" Font-Bold="true" Font-Size="Medium" Text='<%# Item.JobDescription %>' runat="server" />
                                        <asp:Label ID="JobTypeCountLabel" ForeColor="#3399ff" Font-Underline="true" Font-Bold="true" Font-Size="Small" runat="server" Text="(0)" />
                                        <br />
                                        <asp:Repeater ID="InnerRepeater" OnItemDataBound="InnerRepeaterItemDataBound" ItemType="Shanghai.Data.POCOs.ShiftSummary" DataSource='<%# Item.Shifts %>' runat="server">
                                            <ItemTemplate>
                                                <div class="jobShiftItem" style="padding:5px;" >
                                                    
                                                    <!--Checkmark if the shift is finalized -->
                                                     
                                                    <%--trash icon--%>
                                                         

                                                    <%--Employee Shift--%>
                                                    <p><span Visible='<%# isManager && Item.isFinal %>' class="pull-left" runat="server" >
                                            <i class="fa fa-check" style="font-size:16px;""></i>
                                                    </span><asp:Label Text='<%# Item.EmployeeName %>' ID="EmpName" runat="server" /> <span>
                                                    <asp:LinkButton Visible='<%# isManager %>' OnClientClick="if (!confirm('Are you sure you want delete?')) return false;" CssClass="linkButton pull-right" runat="server" CommandArgument='<%# Item.ShiftID %>' OnCommand="DeleteShift">
                                            <i class="fa fa-trash-o" style="font-size:20px;"></i>
                                                    </asp:LinkButton> </span></p>

                                                    <asp:Label Text='<%# Item.StartTime.ToShortTimeString().ToString() + " - " + Item.EndTime.ToShortTimeString().ToString() %>'  runat="server" /><br />
                                                    <span><asp:LinkButton CssClass="linkButton" runat="server" ID="linkBtn_Trade" CommandArgument='<%# Item.ShiftID %>' OnCommand="PostShiftTrade">
                                            <i class="fa fa-exchange" runat="server" id="TradeBtn" style="font-size:20px;"></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>

                                        <div class="userPlus">
                                            <asp:LinkButton Visible='<%# isManager %>' CssClass="linkButton" CommandArgument='<%# Item.JobType + ":" + Item.JobDescription %>' runat="server" OnCommand="AddShift_icon_Click" Font-Underline="True">
                                            <div class="confirm" data-clipboard-text="1"><i class="fa fa-user-plus"></i></div>
                                            </asp:LinkButton>
                                        </div>
                                        <br />
                                        <br />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </td>
                            <td class="scheduleTableColumn">
                                <asp:Repeater ID="SundayRepeater" ItemType="Shanghai.Data.POCOs.ShiftGroup" runat="server">
                                    <ItemTemplate>
                                        <asp:HiddenField Value='<%# Item.Date %>' ID="ShiftDate" runat="server" />
                                        <asp:Label ForeColor="#3399ff" Font-Underline="true" Font-Bold="true" Font-Size="Medium" Text='<%# Item.JobDescription %>' runat="server" />
                                        <asp:Label ID="JobTypeCountLabel" ForeColor="#3399ff" Font-Underline="true" Font-Bold="true" Font-Size="Small" runat="server" Text="(0)" />
                                        <br />
                                        <asp:Repeater ID="InnerRepeater" OnItemDataBound="InnerRepeaterItemDataBound" ItemType="Shanghai.Data.POCOs.ShiftSummary" DataSource='<%# Item.Shifts %>' runat="server">
                                            <ItemTemplate>
                                                <div class="jobShiftItem" style="padding:5px;" >
                                                    
                                                    <!--Checkmark if the shift is finalized -->
                                                     
                                                    <%--trash icon--%>
                                                         

                                                    <%--Employee Shift--%>
                                                    <p><span Visible='<%# isManager && Item.isFinal %>' class="pull-left" runat="server" >
                                            <i class="fa fa-check" style="font-size:16px;""></i>
                                                    </span><asp:Label Text='<%# Item.EmployeeName %>' ID="EmpName" runat="server" /> <span>
                                                    <asp:LinkButton Visible='<%# isManager %>' OnClientClick="if (!confirm('Are you sure you want delete?')) return false;" CssClass="linkButton pull-right" runat="server" CommandArgument='<%# Item.ShiftID %>' OnCommand="DeleteShift">
                                            <i class="fa fa-trash-o" style="font-size:20px;"></i>
                                                    </asp:LinkButton> </span></p>

                                                    <asp:Label Text='<%# Item.StartTime.ToShortTimeString().ToString() + " - " + Item.EndTime.ToShortTimeString().ToString() %>'  runat="server" /><br />
                                                    <span><asp:LinkButton CssClass="linkButton" runat="server" ID="linkBtn_Trade" CommandArgument='<%# Item.ShiftID %>' OnCommand="PostShiftTrade">
                                            <i class="fa fa-exchange" runat="server" id="TradeBtn" style="font-size:20px;"></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>

                                        <div class="userPlus">
                                            <asp:LinkButton Visible='<%# isManager %>' CssClass="linkButton" CommandArgument='<%# Item.JobType + ":" + Item.JobDescription %>' runat="server" OnCommand="AddShift_icon_Click" Font-Underline="True">
                                            <div class="confirm" data-clipboard-text="1"><i class="fa fa-user-plus"></i></div>
                                            </asp:LinkButton>
                                        </div>
                                        <br />
                                        <br />
                                    </ItemTemplate>

                                </asp:Repeater>

                            </td>
                        </tr>
                    </table>
                    <asp:Panel Visible="false" ID="AddShiftPanel" runat="server">
                        <div class="row">
                            <div class="col-md-6">
                            </div>
                        </div>
                    </asp:Panel>
                    <div class="modal fade" id="myModal">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span></button>
                                    <h4 class="modal-title">Add Shift</h4>
                                </div>
                                <div class="modal-body">
                                    <uc1:AddShifts runat="server" ID="UserControl_AddShifts" />
                                </div>
                                <div class="modal-footer">
                                    <asp:Button Text="Add Shift" ID="AddShift" OnClick="AddShift_Click" runat="server" />
                                    <asp:Button Text="Cancel" ID="CancelAddShift" OnClick="CancelAddShift_Click" runat="server" />
                                </div>
                            </div>
                            <!-- /.modal-content -->
                        </div>
                        <!-- /.modal-dialog -->
                    </div>

                    <button type="button" style="display: none;" id="btnShowPopup" class="btn btn-primary btn-lg"
                        data-toggle="modal" data-target="#myModal">
                        Launch demo modal</button>

                    <div class="modal fade" id="myTradeModal">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span></button>
                                    <h4 class="modal-title">Shift Swaping</h4>
                                </div>
                                <div class="modal-body">
                                    <uc1:ShiftTrade runat="server" OnActionTaken="UserControl_ShiftTrade_ActionTaken" ID="UserControl_ShiftTrade" />
                                </div>
                                <div class="modal-footer">
                                </div>
                            </div>
                            <!-- /.modal-content -->
                        </div>
                        <!-- /.modal-dialog -->
                    </div>
                    
                    <script>
                        function ShowPopup() {
                            $("#btnShowPopup").click();
                        }
                        function ShiftTradePopup() {
                            $("#myTradeModal").modal("show");
                        }
                    </script>
                    <asp:Label ID="lblJavaScript" runat="server"></asp:Label>
                    <!-- /.modal -->

                </div>
            </div>
        </div>
    </div>
</asp:Content>
