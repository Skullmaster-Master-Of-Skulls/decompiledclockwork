using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Tutoring;
using TechnoPro.Common.UI.ClientManager.Web.Core.Tutoring;
using Telerik.Web.UI;

namespace TechnoPro.ClockWorkWeb.ctrls.Tutoring.Tutor
{
	// Token: 0x0200012C RID: 300
	public class ctrls_Tutoring_Tutor_CtrlSelectedAvailabilityTimeView : UserControl
	{
		// Token: 0x060008DF RID: 2271 RVA: 0x0003FD64 File Offset: 0x0003DF64
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
			}
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x0003FD8C File Offset: 0x0003DF8C
		private int LookupStudentPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x00003E0A File Offset: 0x0000200A
		private void Page_Init(object sender, EventArgs e)
		{
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x0003FDB0 File Offset: 0x0003DFB0
		public void RefreshList(IList<DateTime> Dates, DateTime? FocusedDate)
		{
			List<DateTime> list = Dates.ToList<DateTime>();
			list.Sort((DateTime g1, DateTime g2) => g1.CompareTo(g2));
			this.ViewState.Add("dates", list);
			this.ViewState.Add("sel", FocusedDate);
			base.Session.Add("tutordates", list);
			this.RadGrid1.Rebind();
			list = (List<DateTime>)this.ViewState["dates"];
			bool flag = list == null || list.Count < 1;
			if (flag)
			{
				this.btn_edit.Enabled = false;
				this.btn_remove.Enabled = false;
			}
			else
			{
				this.btn_edit.Enabled = true;
				this.btn_remove.Enabled = true;
			}
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x0003FE98 File Offset: 0x0003E098
		private void UpdateItem(RadPanelItem item)
		{
			DateTime dateTime;
			bool flag = DateTime.TryParse(item.Text, out dateTime);
			if (flag)
			{
				int num = this.LookupStudentPid();
				ITutoringClientWebClientManager tutoringClientWebClientManager = new TutoringClientWebClientManager();
				int tutorAvailabilityScheduleGroupId = tutoringClientWebClientManager.TutorAvailabilityScheduleGroupId;
				item.Controls.Add(new LiteralControl("</div>"));
			}
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x0003FEE4 File Offset: 0x0003E0E4
		protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
		{
			DateTime? dateTime = null;
			object commandArgument = e.CommandArgument;
			bool flag = commandArgument != null;
			if (flag)
			{
				bool flag2 = commandArgument is string;
				if (flag2)
				{
					DateTime value;
					bool flag3 = DateTime.TryParse((string)commandArgument, out value);
					if (flag3)
					{
						dateTime = new DateTime?(value);
					}
				}
				else
				{
					bool flag4 = commandArgument is DateTime;
					if (flag4)
					{
						dateTime = new DateTime?((DateTime)commandArgument);
					}
				}
			}
			bool flag5 = e.CommandName == "expand";
			if (flag5)
			{
				GridDataItem gridDataItem = e.Item as GridDataItem;
				Panel panel = gridDataItem.FindControl("p_data") as Panel;
				bool flag6 = panel != null;
				if (flag6)
				{
					bool flag7 = !panel.Visible;
					panel.Visible = flag7;
					LinkButton linkButton = (LinkButton)gridDataItem.FindControl("btn_expand");
					bool flag8 = linkButton != null;
					if (flag8)
					{
						linkButton.Text = (flag7 ? "Hide availability" : "Show availability");
					}
					IList<DateTime> showingAvailableDates = this.ShowingAvailableDates;
					bool flag9 = dateTime != null;
					if (flag9)
					{
						bool flag10 = flag7;
						if (flag10)
						{
							Dictionary<DateTime, string> dictionary = (Dictionary<DateTime, string>)this.ViewState["availabilities"];
							bool flag11 = dictionary == null;
							if (flag11)
							{
								dictionary = new Dictionary<DateTime, string>();
							}
							bool flag12 = !dictionary.ContainsKey(dateTime.Value);
							if (flag12)
							{
								string text = this.LoadAvailabilities(dateTime.Value);
								dictionary.Add(dateTime.Value, text);
								this.ViewState.Add("availabilities", dictionary);
								Label label = (Label)e.Item.FindControl("lbl_availabilities");
								bool flag13 = label != null;
								if (flag13)
								{
									label.Text = text;
								}
							}
							bool flag14 = !showingAvailableDates.Contains(dateTime.Value);
							if (flag14)
							{
								showingAvailableDates.Add(dateTime.Value);
								this.ViewState.Add("showingAvailableDates", showingAvailableDates);
							}
						}
						else
						{
							bool flag15 = showingAvailableDates.Contains(dateTime.Value);
							if (flag15)
							{
								showingAvailableDates.Remove(dateTime.Value);
								this.ViewState.Add("showingAvailableDates", showingAvailableDates);
							}
						}
					}
				}
			}
			else
			{
				bool flag16 = e.CommandName == "remove";
				if (flag16)
				{
					List<DateTime> list = (List<DateTime>)this.ViewState["dates"];
					bool flag17 = list != null && dateTime != null;
					if (flag17)
					{
						list.Remove(dateTime.Value);
						this.RadGrid1.Rebind();
						bool flag18 = list.Count < 1;
						if (flag18)
						{
							this.btn_edit.Enabled = false;
							this.btn_remove.Enabled = false;
						}
						this.FireOnDateRemovedFromList(dateTime.Value);
						Dictionary<DateTime, string> dictionary2 = (Dictionary<DateTime, string>)this.ViewState["availabilities"];
						bool flag19 = dictionary2 != null;
						if (flag19)
						{
							bool flag20 = dictionary2.ContainsKey(dateTime.Value);
							if (flag20)
							{
								dictionary2.Remove(dateTime.Value);
							}
						}
						IList<DateTime> showingAvailableDates2 = this.ShowingAvailableDates;
						bool flag21 = showingAvailableDates2.Contains(dateTime.Value);
						if (flag21)
						{
							showingAvailableDates2.Remove(dateTime.Value);
							this.ViewState.Add("showingAvailableDates", showingAvailableDates2);
						}
					}
				}
			}
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x00040258 File Offset: 0x0003E458
		private string LoadAvailabilities(DateTime dt)
		{
			int num = this.LookupStudentPid();
			ITutoringClientWebClientManager tutoringClientWebClientManager = new TutoringClientWebClientManager();
			int tutorAvailabilityScheduleGroupId = tutoringClientWebClientManager.TutorAvailabilityScheduleGroupId;
			throw new NotImplementedException();
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x00040280 File Offset: 0x0003E480
		protected void btn_clearSelection_Click(object sender, EventArgs e)
		{
			this.ViewState.Remove("dates");
			this.FireOnClearSelectionRequested();
			this.ViewState.Remove("availabilities");
			this.RadGrid1.Rebind();
			this.btn_edit.Enabled = false;
			this.btn_remove.Enabled = false;
		}

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x060008E7 RID: 2279 RVA: 0x000402E0 File Offset: 0x0003E4E0
		// (remove) Token: 0x060008E8 RID: 2280 RVA: 0x00040318 File Offset: 0x0003E518
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler OnClearSelectionRequested;

		// Token: 0x060008E9 RID: 2281 RVA: 0x00040350 File Offset: 0x0003E550
		private void FireOnClearSelectionRequested()
		{
			EventHandler onClearSelectionRequested = this.OnClearSelectionRequested;
			bool flag = onClearSelectionRequested != null;
			if (flag)
			{
				onClearSelectionRequested(this, new EventArgs());
			}
		}

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x060008EA RID: 2282 RVA: 0x0004037C File Offset: 0x0003E57C
		// (remove) Token: 0x060008EB RID: 2283 RVA: 0x000403B4 File Offset: 0x0003E5B4
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<DateEventArgs> OnDateRemovedFromList;

		// Token: 0x060008EC RID: 2284 RVA: 0x000403EC File Offset: 0x0003E5EC
		private void FireOnDateRemovedFromList(DateTime date)
		{
			EventHandler<DateEventArgs> onDateRemovedFromList = this.OnDateRemovedFromList;
			bool flag = onDateRemovedFromList != null;
			if (flag)
			{
				onDateRemovedFromList(this, new DateEventArgs
				{
					Date = date
				});
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060008ED RID: 2285 RVA: 0x00040420 File Offset: 0x0003E620
		private IList<DateTime> ShowingAvailableDates
		{
			get
			{
				bool flag = this._showingAvailableDates != null;
				IList<DateTime> showingAvailableDates;
				if (flag)
				{
					showingAvailableDates = this._showingAvailableDates;
				}
				else
				{
					IList<DateTime> list = (IList<DateTime>)this.ViewState["showingAvailableDates"];
					this._showingAvailableDates = (list ?? new List<DateTime>());
					showingAvailableDates = this._showingAvailableDates;
				}
				return showingAvailableDates;
			}
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x00040474 File Offset: 0x0003E674
		protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
		{
			List<DateTime> list = (List<DateTime>)this.ViewState["dates"];
			bool flag = list == null;
			if (flag)
			{
				list = new List<DateTime>();
			}
			Dictionary<DateTime, string> dictionary = (Dictionary<DateTime, string>)this.ViewState["availabilities"];
			bool flag2 = dictionary == null;
			if (flag2)
			{
				dictionary = new Dictionary<DateTime, string>();
			}
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("date", typeof(DateTime));
			dataTable.Columns.Add("availabilities");
			dataTable.Columns.Add("showing", typeof(bool));
			IList<DateTime> showingAvailableDates = this.ShowingAvailableDates;
			foreach (DateTime dateTime in list)
			{
				DateTime date = dateTime.Date;
				bool flag3 = showingAvailableDates.Contains(date);
				bool flag4 = dictionary.ContainsKey(date);
				string text;
				if (flag4)
				{
					text = (dictionary[date] ?? "");
				}
				else
				{
					bool flag5 = flag3;
					if (flag5)
					{
						text = this.LoadAvailabilities(date);
						dictionary.Add(date, text);
					}
					else
					{
						text = "";
					}
				}
				dataTable.Rows.Add(new object[]
				{
					date,
					text,
					flag3
				});
			}
			this.RadGrid1.DataSource = dataTable;
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x00040600 File Offset: 0x0003E800
		protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
		{
			bool flag = e.Item is GridDataItem;
			if (flag)
			{
				GridDataItem gridDataItem = e.Item as GridDataItem;
				TableCell tableCell = gridDataItem["col_date"];
				bool flag2 = tableCell != null;
				if (flag2)
				{
					tableCell.Attributes["scope"] = "row";
				}
			}
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x00003E0A File Offset: 0x0000200A
		protected void RadGrid1_ItemInserted(object sender, GridInsertedEventArgs e)
		{
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x00040658 File Offset: 0x0003E858
		protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
		{
			bool flag = e.Item != null && e.Item is GridDataItem;
			if (flag)
			{
				GridDataItem gridDataItem = (GridDataItem)e.Item;
				Panel panel = (Panel)gridDataItem.FindControl("p_data");
				HiddenField hiddenField = (HiddenField)gridDataItem.FindControl("field_date");
				bool flag2 = panel != null && hiddenField != null && hiddenField.Value != null;
				if (flag2)
				{
					DateTime item;
					bool flag3 = DateTime.TryParse(hiddenField.Value, out item);
					if (flag3)
					{
						IList<DateTime> showingAvailableDates = this.ShowingAvailableDates;
						bool flag4 = showingAvailableDates.Contains(item);
						panel.Visible = flag4;
						bool flag5 = flag4;
						if (flag5)
						{
							LinkButton linkButton = (LinkButton)gridDataItem.FindControl("btn_expand");
							bool flag6 = linkButton != null;
							if (flag6)
							{
								linkButton.Text = "Hide availability";
							}
						}
					}
				}
			}
		}

		// Token: 0x040006D9 RID: 1753
		protected RadCodeBlock RadCodeBlock1;

		// Token: 0x040006DA RID: 1754
		protected Panel pAvailabilities;

		// Token: 0x040006DB RID: 1755
		protected Panel p_options;

		// Token: 0x040006DC RID: 1756
		protected Button btn_edit;

		// Token: 0x040006DD RID: 1757
		protected Button btn_remove;

		// Token: 0x040006DE RID: 1758
		protected Panel p_pleaseSelectDates;

		// Token: 0x040006DF RID: 1759
		protected LinkButton btn_clearSelection;

		// Token: 0x040006E0 RID: 1760
		protected RadGrid RadGrid1;

		// Token: 0x040006E1 RID: 1761
		protected RadWindowManager RadWindowManager1;

		// Token: 0x040006E2 RID: 1762
		protected RadWindow RadWindow1;

		// Token: 0x040006E3 RID: 1763
		protected RadWindow RadWindow2;

		// Token: 0x040006E6 RID: 1766
		private IList<DateTime> _showingAvailableDates;
	}
}
