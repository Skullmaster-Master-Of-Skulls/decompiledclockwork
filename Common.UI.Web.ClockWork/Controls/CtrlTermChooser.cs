using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.LookupCourses;
using TechnoPro.Common.UI.ClientManager.Web.Core.LookupCourses;
using TechnoPro.Common.UI.Web.Entity.LookupCourses;

namespace TechnoPro.Common.UI.Web.ClockWork.Controls
{
	// Token: 0x0200000B RID: 11
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:CtrlTermChooser runat=server></{0}:CtrlTermChooser>")]
	public class CtrlTermChooser : WebControl, INamingContainer
	{
		// Token: 0x06000094 RID: 148 RVA: 0x000033A4 File Offset: 0x000015A4
		public CtrlTermChooser()
		{
			SessionView selectedSessionFromWebSession = this.GetSelectedSessionFromWebSession();
			bool flag = selectedSessionFromWebSession == null;
			if (flag)
			{
				ISessionClientManager sessionClientManager = new SessionClientManager();
				this.SetSelectedSessionInWebSession(sessionClientManager.GetCurrentSession());
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000034AC File Offset: 0x000016AC
		private void FireSelectedIndexChanged()
		{
			bool flag = this.SelectedIndexChanged != null;
			if (flag)
			{
				this.SelectedIndexChanged(this, new EventArgs());
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000096 RID: 150 RVA: 0x000034DC File Offset: 0x000016DC
		// (set) Token: 0x06000097 RID: 151 RVA: 0x000034F4 File Offset: 0x000016F4
		public TermChooserAvailableSessionMode AvailableSessionMode
		{
			get
			{
				return this.availableSessionMode;
			}
			set
			{
				this.availableSessionMode = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00003500 File Offset: 0x00001700
		// (set) Token: 0x06000099 RID: 153 RVA: 0x00003518 File Offset: 0x00001718
		public string StoreCurrentSelectedSessionInWebSessionKey
		{
			get
			{
				return this._storeCurrentSelectedSessionInWebSessionKey;
			}
			set
			{
				this._storeCurrentSelectedSessionInWebSessionKey = value;
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003524 File Offset: 0x00001724
		private SessionView GetSelectedSessionFromWebSession()
		{
			object obj = HttpContext.Current.Session[this._storeCurrentSelectedSessionInWebSessionKey];
			bool flag = obj == null || !(obj is SessionView);
			SessionView result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = (SessionView)obj;
			}
			return result;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000356C File Offset: 0x0000176C
		private void SetSelectedSessionInWebSession(SessionView session)
		{
			HttpContext.Current.Session.Add(this._storeCurrentSelectedSessionInWebSessionKey, session);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003588 File Offset: 0x00001788
		private SessionView GetSelectedSession()
		{
			bool flag = this.cmb_term != null && this.cmb_term.SelectedItem != null;
			if (flag)
			{
				string id = this.cmb_term.SelectedItem.Value;
				List<SessionView> list = this.Sessions;
				SessionView sessionView = list.Find((SessionView s) => s.Id.Equals(id));
				bool flag2 = sessionView != null;
				if (flag2)
				{
					return sessionView;
				}
			}
			return this.Sessions[1];
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600009D RID: 157 RVA: 0x0000360C File Offset: 0x0000180C
		// (set) Token: 0x0600009E RID: 158 RVA: 0x000036A8 File Offset: 0x000018A8
		public SessionView SelectedSession
		{
			get
			{
				SessionView selectedSessionFromWebSession = this.GetSelectedSessionFromWebSession();
				bool flag = selectedSessionFromWebSession != null;
				SessionView result;
				if (flag)
				{
					result = selectedSessionFromWebSession;
				}
				else
				{
					bool flag2 = this.cmb_term != null && this.cmb_term.SelectedItem != null;
					if (flag2)
					{
						string id = this.cmb_term.SelectedItem.Value;
						List<SessionView> list = this.Sessions;
						SessionView sessionView = list.Find((SessionView s) => s.Id.Equals(id));
						bool flag3 = sessionView != null;
						if (flag3)
						{
							return sessionView;
						}
					}
					result = this.Sessions[1];
				}
				return result;
			}
			set
			{
				bool flag = this.cmb_term != null;
				if (flag)
				{
					bool flag2 = value == null;
					if (flag2)
					{
						this.cmb_term.SelectedIndex = -1;
					}
					else
					{
						for (int i = 0; i < this.cmb_term.Items.Count; i++)
						{
							ListItem listItem = this.cmb_term.Items[i];
							bool flag3 = listItem.Value.Equals(value.Id);
							if (flag3)
							{
								this.ignoreSelectedIndexChanged = true;
								try
								{
									this.cmb_term.SelectedIndex = i;
									break;
								}
								finally
								{
									this.ignoreSelectedIndexChanged = false;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x0600009F RID: 159 RVA: 0x00003760 File Offset: 0x00001960
		// (remove) Token: 0x060000A0 RID: 160 RVA: 0x00003798 File Offset: 0x00001998
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler SelectedIndexChanged;

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x000037D0 File Offset: 0x000019D0
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x000037E8 File Offset: 0x000019E8
		[Category("Appearance")]
		[DefaultValue("Title")]
		[Localizable(true)]
		public string Title
		{
			get
			{
				return this.title;
			}
			set
			{
				this.title = value;
				bool flag = this.lblIntro != null;
				if (flag)
				{
					this.lblIntro.Text = this.title;
				}
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000381C File Offset: 0x00001A1C
		public override void Dispose()
		{
			bool flag = this.cmb_term != null;
			if (flag)
			{
				this.cmb_term.Dispose();
			}
			bool flag2 = this.lblIntro != null;
			if (flag2)
			{
				this.lblIntro.Dispose();
			}
			bool flag3 = this.btnRefresh != null;
			if (flag3)
			{
				this.btnRefresh.Dispose();
			}
			bool flag4 = this.table != null;
			if (flag4)
			{
				this.table.Dispose();
			}
			base.Dispose();
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00002619 File Offset: 0x00000819
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			writer.RenderBeginTag("div");
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003895 File Offset: 0x00001A95
		protected override void CreateChildControls()
		{
			this.BuildControlHeiarchy();
			base.CreateChildControls();
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x000038A8 File Offset: 0x00001AA8
		private List<SessionView> Sessions
		{
			get
			{
				bool flag = this.sessions == null;
				if (flag)
				{
					this.sessions = this.GetSessions();
				}
				return this.sessions;
			}
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x060000A7 RID: 167 RVA: 0x000038DC File Offset: 0x00001ADC
		// (remove) Token: 0x060000A8 RID: 168 RVA: 0x00003914 File Offset: 0x00001B14
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event UserInfoForCourseRequestedHandler OnUserInfoRequested;

		// Token: 0x060000A9 RID: 169 RVA: 0x0000394C File Offset: 0x00001B4C
		private UserInfoForCourses FireOnuserInfoRequested()
		{
			bool flag = this.OnUserInfoRequested != null;
			UserInfoForCourses result;
			if (flag)
			{
				UserInfoForCourseArgs userInfoForCourseArgs = new UserInfoForCourseArgs
				{
					Info = new UserInfoForCourses()
				};
				this.OnUserInfoRequested(this, userInfoForCourseArgs);
				result = userInfoForCourseArgs.Info;
			}
			else
			{
				result = new UserInfoForCourses();
			}
			return result;
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000AA RID: 170 RVA: 0x0000399C File Offset: 0x00001B9C
		private UserInfoForCourses userInfo
		{
			get
			{
				bool flag = this._userInfo == null;
				if (flag)
				{
					this._userInfo = this.FireOnuserInfoRequested();
				}
				return this._userInfo;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000AB RID: 171 RVA: 0x000039D0 File Offset: 0x00001BD0
		// (set) Token: 0x060000AC RID: 172 RVA: 0x000039E8 File Offset: 0x00001BE8
		public int? MaxSessionsInThePast
		{
			get
			{
				return this._maxSessionsInThePast;
			}
			set
			{
				this._maxSessionsInThePast = value;
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000039F4 File Offset: 0x00001BF4
		private List<SessionView> GetSessions()
		{
			ISessionClientManager sessionClientManager = new SessionClientManager();
			return this.sessions = sessionClientManager.GetSessions(this._maxSessionsInThePast, this.availableSessionMode, this.userInfo);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003A30 File Offset: 0x00001C30
		private void FillTermDropList()
		{
			bool flag = this.cmb_term == null || this.cmb_term.Items.Count > 1;
			if (!flag)
			{
				List<SessionView> list = this.Sessions;
				this.cmb_term.DataSource = list;
				this.cmb_term.DataTextField = "Title";
				this.cmb_term.DataValueField = "Id";
				this.cmb_term.DataBind();
				SessionView selectedSessionFromWebSession = this.GetSelectedSessionFromWebSession();
				bool flag2 = selectedSessionFromWebSession == null;
				SessionView selectedSession;
				if (flag2)
				{
					bool flag3 = list.Count > 0;
					if (flag3)
					{
						DateTime date = DateTime.Now.Date;
						int index = 0;
						for (int i = 0; i < list.Count; i++)
						{
							SessionView sessionView = list[i];
							bool flag4 = date >= sessionView.StartDate && date < sessionView.EndDate.Date;
							if (flag4)
							{
								index = i;
								break;
							}
						}
						selectedSession = list[index];
					}
					else
					{
						selectedSession = null;
					}
				}
				else
				{
					selectedSession = selectedSessionFromWebSession;
				}
				this.SelectedSession = selectedSession;
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003B58 File Offset: 0x00001D58
		protected override void OnInit(EventArgs e)
		{
			this.InitializeControls();
			base.OnInit(e);
			this.FillTermDropList();
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00003B74 File Offset: 0x00001D74
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x00003B8C File Offset: 0x00001D8C
		public string Caption
		{
			get
			{
				return this._caption;
			}
			set
			{
				this._caption = value;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00003B98 File Offset: 0x00001D98
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x00003BB0 File Offset: 0x00001DB0
		public HorizontalAlign DropListHorizontalAlign
		{
			get
			{
				return this._dropListHorizontalAlign;
			}
			set
			{
				this._dropListHorizontalAlign = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00003BBC File Offset: 0x00001DBC
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x00003BD4 File Offset: 0x00001DD4
		public string RefreshButtonText
		{
			get
			{
				return this._refreshButtonText;
			}
			set
			{
				this._refreshButtonText = value;
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003BE0 File Offset: 0x00001DE0
		private void InitializeControls()
		{
			this.table.Width = new Unit(100.0, UnitType.Percentage);
			this.row.VerticalAlign = VerticalAlign.Bottom;
			this.cell1.HorizontalAlign = HorizontalAlign.Left;
			bool flag = this.title.Length < 1;
			if (flag)
			{
				this.cell1.Visible = false;
			}
			this.lblIntro.ID = "lblIntro";
			this.lblIntro.Text = this.title;
			this.cell2.HorizontalAlign = this._dropListHorizontalAlign;
			this.cellRefreshButton.HorizontalAlign = HorizontalAlign.Center;
			this.cellShowTermLabel.HorizontalAlign = HorizontalAlign.Right;
			this.cellShowTermLabel.Style.Add("padding-right", "4px");
			this.cellRefreshButton.Style.Add("width", "1px");
			this.cellRefreshButton.Style.Add("white-space", "nowrap");
			this.cellRefreshButton.Style.Add("padding-left", "2px");
			this.lbl2.Text = this._caption;
			this.cmb_term.ID = "cmb_term";
			this.cmb_term.CausesValidation = false;
			this.cmb_term.AutoPostBack = true;
			this.cmb_term.EnableViewState = true;
			this.lbl2.AssociatedControlID = this.cmb_term.ID;
			this.btnRefresh.Text = this._refreshButtonText;
			this.btnRefresh.CssClass = "btn btn-sm btn-outline-secondary";
			this.btnRefresh.Style.Add("padding-top", "8px");
			this.btnRefresh.Style.Add("margin-top", "5px");
			this.lblIntro.Style.Add("padding-top", "8px");
			this.lbl2.Style.Add("padding-top", "8px");
			this.cmb_term.SelectedIndexChanged += this.cmb_term_SelectedIndexChanged;
			this.btnRefresh.Click += this.btnRefresh_Click;
			bool flag2 = this._dropListAutoPostBack != null;
			if (flag2)
			{
				this.cmb_term.AutoPostBack = this._dropListAutoPostBack.Value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00003E44 File Offset: 0x00002044
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x00003E5C File Offset: 0x0000205C
		public bool? DropListAutoPostBack
		{
			get
			{
				return this._dropListAutoPostBack;
			}
			set
			{
				this._dropListAutoPostBack = value;
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003E68 File Offset: 0x00002068
		private void BuildControlHeiarchy()
		{
			this.Controls.Add(this.table);
			this.cell1.Controls.Add(this.lblIntro);
			this.cellShowTermLabel.Controls.Add(this.lbl2);
			this.cmb_term.Style.Add("width", "100%");
			this.cell2.Controls.Add(this.cmb_term);
			this.cellRefreshButton.Controls.Add(this.btnRefresh);
			this.row.Cells.Add(this.cell1);
			this.row.Cells.Add(this.cellShowTermLabel);
			this.row.Cells.Add(this.cell2);
			this.row.Cells.Add(this.cellRefreshButton);
			this.table.Rows.Add(this.row);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003F72 File Offset: 0x00002172
		protected void btnRefresh_Click(object sender, EventArgs e)
		{
			this.SelectedSessionChanged();
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003F72 File Offset: 0x00002172
		protected void cmb_term_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.SelectedSessionChanged();
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003F7C File Offset: 0x0000217C
		private void SelectedSessionChanged()
		{
			bool flag = this.ignoreSelectedIndexChanged;
			if (!flag)
			{
				SessionView selectedSession = this.GetSelectedSession();
				bool flag2 = selectedSession != null;
				if (flag2)
				{
					this.SetSelectedSessionInWebSession(selectedSession);
				}
				this.FireSelectedIndexChanged();
			}
		}

		// Token: 0x0400002A RID: 42
		private Table table = new Table();

		// Token: 0x0400002B RID: 43
		private Label lblIntro = new Label();

		// Token: 0x0400002C RID: 44
		private DropDownList cmb_term = new DropDownList();

		// Token: 0x0400002D RID: 45
		private Button btnRefresh = new Button();

		// Token: 0x0400002E RID: 46
		private TableRow row = new TableRow();

		// Token: 0x0400002F RID: 47
		private TableCell cell1 = new TableCell();

		// Token: 0x04000030 RID: 48
		private TableCell cell2 = new TableCell();

		// Token: 0x04000031 RID: 49
		private TableCell cellRefreshButton = new TableCell();

		// Token: 0x04000032 RID: 50
		private TableCell cellShowTermLabel = new TableCell();

		// Token: 0x04000033 RID: 51
		private Label lbl2 = new Label();

		// Token: 0x04000034 RID: 52
		private string title = "Title";

		// Token: 0x04000035 RID: 53
		private bool ignoreSelectedIndexChanged = false;

		// Token: 0x04000036 RID: 54
		private TermChooserAvailableSessionMode availableSessionMode = TermChooserAvailableSessionMode.TermsWithLoggedInStudentsRegisteredCourses;

		// Token: 0x04000037 RID: 55
		private string _storeCurrentSelectedSessionInWebSessionKey = "tc_currentterm";

		// Token: 0x04000039 RID: 57
		private List<SessionView> sessions;

		// Token: 0x0400003B RID: 59
		private UserInfoForCourses _userInfo = null;

		// Token: 0x0400003C RID: 60
		private int? _maxSessionsInThePast = null;

		// Token: 0x0400003D RID: 61
		private string _caption = "Show term: ";

		// Token: 0x0400003E RID: 62
		private HorizontalAlign _dropListHorizontalAlign = HorizontalAlign.Right;

		// Token: 0x0400003F RID: 63
		private string _refreshButtonText = "Refresh";

		// Token: 0x04000040 RID: 64
		private bool? _dropListAutoPostBack = null;
	}
}
