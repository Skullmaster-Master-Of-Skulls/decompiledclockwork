using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkLogger;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests;
using TechnoPro.Common.ClientManager.Core.DynamicForms;
using TechnoPro.Common.ClientManager.Core.LookupCourses;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.Core.StudentAccommodationRequests;
using TechnoPro.Common.ClientManager.ICore.DynamicForms;
using TechnoPro.Common.ClientManager.ICore.LookupCourses;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.ClientManager.ICore.StudentAccommodationRequests;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.LookupCourses;
using TechnoPro.Common.UI.ClientManager.Web.Core.LookupCourses;
using TechnoPro.Common.UI.Web.AccommodationsRequest.Entity;
using TechnoPro.Common.UI.Web.Entity.LookupCourses;
using Telerik.Web.UI;

namespace TechnoPro.Common.UI.Web.AccommodationsRequest.Controls
{
	// Token: 0x02000003 RID: 3
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:CtrlAccommodationRequestCoursesList runat=server></{0}:CtrlAccommodationRequestCoursesList>")]
	public class CtrlAccommodationRequestCoursesList : WebControl, INamingContainer
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000024B4 File Offset: 0x000006B4
		// (set) Token: 0x0600000F RID: 15 RVA: 0x000024CC File Offset: 0x000006CC
		public SessionDTO SessionDTO
		{
			get
			{
				return this.session;
			}
			set
			{
				this.session = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000024D8 File Offset: 0x000006D8
		// (set) Token: 0x06000011 RID: 17 RVA: 0x000024F0 File Offset: 0x000006F0
		public int Pid
		{
			get
			{
				return this.pid;
			}
			set
			{
				this.pid = value;
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000024FA File Offset: 0x000006FA
		public void RefreshList()
		{
			this.gv_courses.Rebind();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000250C File Offset: 0x0000070C
		private void InitializeControls()
		{
			this.gv_courses.MasterTableView.DataKeyNames = new string[]
			{
				"LuCourseId",
				"eStatus"
			};
			this.gv_courses.AutoGenerateColumns = false;
			this.gv_courses.GridLines = GridLines.None;
			this.gv_courses.Skin = "Office2007";
			this.gv_courses.AlternatingItemStyle.BackColor = Color.Azure;
			this.gv_courses.ItemStyle.Height = 50;
			this.gv_courses.HeaderContextMenu.EnableTheming = true;
			this.gv_courses.HeaderContextMenu.CollapseAnimation.Duration = 200;
			this.gv_courses.AlternatingItemStyle.BackColor = Color.FromArgb(244, 244, 244);
			this.gv_courses.MasterTableView.Font.Size = FontUnit.Medium;
			this.gv_courses.MasterTableView.NoMasterRecordsText = "You are not currently registered in any courses.";
			Unit width = new Unit(20.0, UnitType.Pixel);
			this.gv_courses.MasterTableView.RowIndicatorColumn.HeaderStyle.Width = width;
			this.gv_courses.MasterTableView.ExpandCollapseColumn.HeaderStyle.Width = width;
			this.gv_courses.ClientSettings.EnableRowHoverStyle = true;
			this.gv_courses.FilterMenu.EnableTheming = true;
			this.gv_courses.FilterMenu.CollapseAnimation.Duration = 200;
			this.col_courseDescription.UniqueName = "col_courses";
			this.col_courseDescription.HeaderText = "Course";
			this.col_courseDescription.SortExpression = "CourseDescription";
			this.col_courseDescription.DataField = "CourseDescription";
			this.col_courseDescription.ItemStyle.Width = new Unit(100.0, UnitType.Pixel);
			this.col_status.UniqueName = "col_status";
			this.col_status.HeaderText = "Status";
			this.col_status.SortExpression = "Status";
			this.col_status.DataField = "Status";
			this.col_button_letter.CommandName = "letter";
			this.col_button_letter.ButtonType = GridButtonColumnType.PushButton;
			this.col_button_letter.ButtonCssClass = "btn btn-sm btn-primary";
			this.col_button_letter.HeaderText = "Letter";
			this.col_button_letter.Text = "Get letter";
			this.col_button_letter.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
			this.col_button_letter.ItemStyle.VerticalAlign = VerticalAlign.Middle;
			this.col_button_letter.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
			this.col_button_letter.HeaderStyle.VerticalAlign = VerticalAlign.Middle;
			this.col_button_letter.Visible = this.allowStudentsToDownloadTheirLetter;
			this.col_button.CommandName = "request";
			this.col_button.ButtonType = GridButtonColumnType.PushButton;
			this.col_button.ButtonCssClass = "btn btn-sm btn-primary";
			this.col_button.HeaderText = "Request";
			this.col_button.Text = "Request";
			this.col_button.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
			this.col_button.ItemStyle.VerticalAlign = VerticalAlign.Middle;
			this.col_button.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
			this.col_button.HeaderStyle.VerticalAlign = VerticalAlign.Middle;
			this.gv_courses.NeedDataSource += this.gv_courses_NeedDataSource;
			this.gv_courses.ItemCommand += this.gv_courses_ItemCommand;
			this.gv_courses.ItemDataBound += this.gv_courses_ItemDataBound;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000028DC File Offset: 0x00000ADC
		private SessionView CurrentSession
		{
			get
			{
				bool flag = this._currentSession == null;
				if (flag)
				{
					TechnoPro.Common.UI.ClientManager.Web.Core.LookupCourses.ISessionClientManager sessionClientManager = new TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.LookupCourses.SessionClientManager();
					this._currentSession = sessionClientManager.GetCurrentSession();
				}
				return this._currentSession;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002918 File Offset: 0x00000B18
		private bool allowStudentsToDownloadTheirLetter
		{
			get
			{
				bool flag = this._allowStudentsToDownloadTheirLetter != null;
				bool value;
				if (flag)
				{
					value = this._allowStudentsToDownloadTheirLetter.Value;
				}
				else
				{
					IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
					this._allowStudentsToDownloadTheirLetter = new bool?(webSettingsClientManager.GetSettingValue<bool>(Setting.SELFREGC_AllowStudentsToDownloadTheirLetter));
					value = this._allowStudentsToDownloadTheirLetter.Value;
				}
				return value;
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002970 File Offset: 0x00000B70
		private void gv_courses_ItemDataBound(object sender, GridItemEventArgs e)
		{
			bool flag = e.Item is GridDataItem;
			if (flag)
			{
				GridDataItem gridDataItem = (GridDataItem)e.Item;
				object dataItem = gridDataItem.DataItem;
				bool flag2 = dataItem != null;
				if (flag2)
				{
					CourseWithStudentAccommodationRequestView courseWithStudentAccommodationRequestView = (CourseWithStudentAccommodationRequestView)dataItem;
					eStudentCourseAccommodationRequestStatusDTO eStudentCourseAccommodationRequestStatusDTO = (courseWithStudentAccommodationRequestView.AccommodationRequest != null) ? courseWithStudentAccommodationRequestView.AccommodationRequest.Status : eStudentCourseAccommodationRequestStatusDTO.Unknown;
					SessionView currentSession = this.CurrentSession;
					ILookupCourseClientManager lookupCourseClientManager = new LookupCourseClientManager();
					bool flag3 = lookupCourseClientManager.IsCourseCurrentlyInScopeForActionByStudentOrProf(eCourseUsageType.StudentAccommodationLetterRequests, courseWithStudentAccommodationRequestView.CourseRegistrationWithAccommodations.CourseReg.Course.StartDate, courseWithStudentAccommodationRequestView.CourseRegistrationWithAccommodations.CourseReg.Course.EndDate);
					bool disableRequestingBecauseCourseStartDateIsAfterAccExpiry = courseWithStudentAccommodationRequestView.DisableRequestingBecauseCourseStartDateIsAfterAccExpiry;
					if (disableRequestingBecauseCourseStartDateIsAfterAccExpiry)
					{
						flag3 = false;
					}
					CWLogger.Logger.Trace("ctrlaccommodationrequestcourseslist:allowedtoRequest={0}", flag3.ToString());
					bool flag4 = !flag3;
					if (flag4)
					{
						gridDataItem[this.col_button_letter].Controls[0].Visible = false;
						gridDataItem[this.col_button].Controls[0].Visible = false;
					}
					else
					{
						eStudentCourseAccommodationRequestStatusDTO eStudentCourseAccommodationRequestStatusDTO2 = eStudentCourseAccommodationRequestStatusDTO;
						eStudentCourseAccommodationRequestStatusDTO eStudentCourseAccommodationRequestStatusDTO3 = eStudentCourseAccommodationRequestStatusDTO2;
						if (eStudentCourseAccommodationRequestStatusDTO3 != eStudentCourseAccommodationRequestStatusDTO.Unknown && eStudentCourseAccommodationRequestStatusDTO3 != eStudentCourseAccommodationRequestStatusDTO.PendingWaitingForStudent)
						{
							if (eStudentCourseAccommodationRequestStatusDTO3 != eStudentCourseAccommodationRequestStatusDTO.Approved)
							{
								gridDataItem[this.col_button_letter].Controls[0].Visible = false;
								gridDataItem[this.col_button].Controls[0].Visible = false;
							}
							else
							{
								gridDataItem[this.col_button_letter].Controls[0].Visible = this.allowStudentsToDownloadTheirLetter;
								gridDataItem[this.col_button].Controls[0].Visible = false;
							}
						}
						else
						{
							gridDataItem[this.col_button_letter].Controls[0].Visible = false;
							gridDataItem[this.col_button].Controls[0].Visible = true;
						}
					}
				}
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002B74 File Offset: 0x00000D74
		private void AddColumns()
		{
			this.gv_courses.MasterTableView.Columns.Add(this.col_courseDescription);
			this.gv_courses.MasterTableView.Columns.Add(this.col_status);
			this.gv_courses.MasterTableView.Columns.Add(this.col_button);
			this.gv_courses.MasterTableView.Columns.Add(this.col_button_letter);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002BF4 File Offset: 0x00000DF4
		private void gv_courses_ItemCommand(object sender, GridCommandEventArgs e)
		{
			object obj = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["LuCourseId"];
			object obj2 = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["eStatus"];
			bool flag = obj == null;
			if (!flag)
			{
				int lucid = (int)obj;
				bool flag2 = obj2 != null;
				eStudentCourseAccommodationRequestStatusDTO eStudentCourseAccommodationRequestStatusDTO;
				if (flag2)
				{
					eStudentCourseAccommodationRequestStatusDTO = (eStudentCourseAccommodationRequestStatusDTO)obj2;
				}
				else
				{
					eStudentCourseAccommodationRequestStatusDTO = eStudentCourseAccommodationRequestStatusDTO.Unknown;
				}
				bool flag3 = e.CommandName.Equals("letter") && eStudentCourseAccommodationRequestStatusDTO == eStudentCourseAccommodationRequestStatusDTO.Approved;
				if (flag3)
				{
					this.FireLetterRequestSubmitted(lucid);
				}
				else
				{
					bool flag4 = e.CommandName.Equals("request");
					if (flag4)
					{
						this.FireCourseRequestSubmitted(lucid);
					}
				}
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000019 RID: 25 RVA: 0x00002CC8 File Offset: 0x00000EC8
		// (remove) Token: 0x0600001A RID: 26 RVA: 0x00002D00 File Offset: 0x00000F00
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event CtrlAccommodationRequestCoursesList.CourseRequestEventHandler CourseRequestSubmitted;

		// Token: 0x0600001B RID: 27 RVA: 0x00002D35 File Offset: 0x00000F35
		private void FireCourseRequestSubmitted(int lucid)
		{
			CtrlAccommodationRequestCoursesList.CourseRequestEventHandler courseRequestSubmitted = this.CourseRequestSubmitted;
			if (courseRequestSubmitted != null)
			{
				courseRequestSubmitted(this, lucid);
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600001C RID: 28 RVA: 0x00002D4C File Offset: 0x00000F4C
		// (remove) Token: 0x0600001D RID: 29 RVA: 0x00002D84 File Offset: 0x00000F84
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event CtrlAccommodationRequestCoursesList.CourseRequestEventHandler LetterRequestSubmitted;

		// Token: 0x0600001E RID: 30 RVA: 0x00002DB9 File Offset: 0x00000FB9
		private void FireLetterRequestSubmitted(int lucid)
		{
			CtrlAccommodationRequestCoursesList.CourseRequestEventHandler letterRequestSubmitted = this.LetterRequestSubmitted;
			if (letterRequestSubmitted != null)
			{
				letterRequestSubmitted(this, lucid);
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002DD0 File Offset: 0x00000FD0
		protected void gv_courses_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
		{
			bool flag = this.session == null;
			if (!flag)
			{
				IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
				bool settingValue = webSettingsClientManager.GetSettingValue<bool>(Setting.SELFREGC_DontAllowStudentsToCompleteSelfRegForCoursesStartingAfterAccommodationsExpiryDate);
				bool flag2 = settingValue;
				if (flag2)
				{
					IAccommodationsClientManager accommodationsClientManager = new AccommodationsClientManager();
					DateTime? studentAccommodationsExpiryDate = accommodationsClientManager.GetStudentAccommodationsExpiryDate(this.pid);
					bool flag3 = studentAccommodationsExpiryDate == null;
					if (flag3)
					{
						CWLogger.Logger.Warn("ctrlaccommodationrequestcourseslist:EnableRestrictRequestsForCoursesStartingPastAccExpiryDate is set to true, but acc expiry date control id is not configured");
					}
					else
					{
						this._dateCourseMustStartBefore = new DateTime?(studentAccommodationsExpiryDate.Value.Date);
					}
				}
				IStudentAccommodationReqClientManager studentAccommodationReqClientManager = new StudentAccommodationReqClientManager();
				IList<CourseRegistrationWithAccommodationRequestDTO> source = studentAccommodationReqClientManager.LoadCourseRegistrationsWithRequestByStudentAndDate(this.pid, this.session.StartDate, this.session.EndDate, false);
				IEnumerable<CourseWithStudentAccommodationRequestView> dataSource = from f in source
				select new CourseWithStudentAccommodationRequestView(f, this._dateCourseMustStartBefore);
				this.gv_courses.DataSource = dataSource;
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002EB0 File Offset: 0x000010B0
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			writer.RenderBeginTag("div");
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002EBF File Offset: 0x000010BF
		protected override void CreateChildControls()
		{
			this.BuildControlHeiarchy();
			base.CreateChildControls();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002ED0 File Offset: 0x000010D0
		private void BuildControlHeiarchy()
		{
			this.Controls.Add(this.gv_courses);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002EE5 File Offset: 0x000010E5
		protected override void OnInit(EventArgs e)
		{
			this.InitializeControls();
			this.AddColumns();
			base.OnInit(e);
		}

		// Token: 0x04000002 RID: 2
		private RadGrid gv_courses = new RadGrid();

		// Token: 0x04000003 RID: 3
		private GridBoundColumn col_courseDescription = new GridBoundColumn();

		// Token: 0x04000004 RID: 4
		private GridBoundColumn col_status = new GridBoundColumn();

		// Token: 0x04000005 RID: 5
		private GridButtonColumn col_button = new GridButtonColumn();

		// Token: 0x04000006 RID: 6
		private GridButtonColumn col_button_letter = new GridButtonColumn();

		// Token: 0x04000007 RID: 7
		private SessionDTO session;

		// Token: 0x04000008 RID: 8
		private int pid;

		// Token: 0x04000009 RID: 9
		private SessionView _currentSession;

		// Token: 0x0400000A RID: 10
		private bool? _allowStudentsToDownloadTheirLetter;

		// Token: 0x0400000D RID: 13
		private DateTime? _dateCourseMustStartBefore;

		// Token: 0x0200000B RID: 11
		// (Invoke) Token: 0x06000063 RID: 99
		public delegate void CourseRequestEventHandler(object sender, int lucid);
	}
}
