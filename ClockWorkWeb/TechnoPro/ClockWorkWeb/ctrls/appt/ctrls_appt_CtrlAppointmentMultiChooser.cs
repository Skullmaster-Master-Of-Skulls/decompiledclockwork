using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.Common.ClientManager.Core.AppointmentsCalendar;
using TechnoPro.Common.ClientManager.ICore.AppointmentsCalendar;
using TechnoPro.Common.UI.ClientManager.Web.Auth;

namespace TechnoPro.ClockWorkWeb.ctrls.appt
{
	// Token: 0x02000153 RID: 339
	public class ctrls_appt_CtrlAppointmentMultiChooser : UserControl
	{
		// Token: 0x06000A74 RID: 2676 RVA: 0x00048138 File Offset: 0x00046338
		protected void Page_Load(object sender, EventArgs e)
		{
			bool flag = !this.Page.IsPostBack;
			if (flag)
			{
				bool flag2 = !this.loadedApps;
				if (flag2)
				{
					this.ReloadApps(null);
					this.loadedApps = true;
				}
			}
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x00048178 File Offset: 0x00046378
		private void ReloadApps(params int[] appIdsToCheck)
		{
			int personId = this.personId;
			bool flag = personId > 0;
			if (flag)
			{
				DateTime startDate = DateTime.Now.Date.AddMonths(-1);
				DateTime endDate = DateTime.Now.Date.AddDays(7.0);
				IAppointmentClientManager appointmentClientManager = new AppointmentClientManager();
				List<BaseBasicAppointmentDTO> list = appointmentClientManager.LoadBasicAppointmentInformationByUserAndDateRange(personId, startDate, endDate, true).ToList<BaseBasicAppointmentDTO>();
				list = (from g in list
				where !g.IsPointOfContact
				select g).ToList<BaseBasicAppointmentDTO>();
				bool flag2 = list.Count > 0;
				if (flag2)
				{
					list.Sort((BaseBasicAppointmentDTO a1, BaseBasicAppointmentDTO a2) => a1.StartDateTime.CompareTo(a2.StartDateTime));
					this.chks.Items.Clear();
					foreach (BaseBasicAppointmentDTO baseBasicAppointmentDTO in list)
					{
						ListItem listItem = new ListItem(baseBasicAppointmentDTO.GetStudentAppointmentDescriptionShort(personId), baseBasicAppointmentDTO.AppointmentId.ToString());
						bool flag3 = appIdsToCheck != null && appIdsToCheck.Contains(baseBasicAppointmentDTO.AppointmentId);
						if (flag3)
						{
							listItem.Selected = true;
						}
						this.chks.Items.Add(listItem);
					}
					this.lbl_noapps.Visible = false;
				}
				else
				{
					this.lbl_noapps.Visible = true;
				}
			}
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x00048318 File Offset: 0x00046518
		public void SetSelectedAppIds(params int[] appIds)
		{
			bool flag = !this.loadedApps;
			if (flag)
			{
				this.ReloadApps(appIds);
				this.loadedApps = true;
			}
			else
			{
				bool flag2 = appIds == null;
				if (!flag2)
				{
					foreach (object obj in this.chks.Items)
					{
						ListItem listItem = (ListItem)obj;
						bool flag3 = !listItem.Selected;
						if (flag3)
						{
							int value;
							bool flag4 = int.TryParse(listItem.Value, out value) && appIds.Contains(value);
							if (flag4)
							{
								listItem.Selected = true;
							}
						}
					}
				}
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000A77 RID: 2679 RVA: 0x000483DC File Offset: 0x000465DC
		public IList<int> SelectedAppIds
		{
			get
			{
				List<int> list = new List<int>();
				foreach (object obj in this.chks.Items)
				{
					ListItem listItem = (ListItem)obj;
					bool selected = listItem.Selected;
					if (selected)
					{
						int num;
						bool flag = int.TryParse(listItem.Value, out num) && num > 0 && !list.Contains(num);
						if (flag)
						{
							list.Add(num);
						}
					}
				}
				return list;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000A78 RID: 2680 RVA: 0x00048488 File Offset: 0x00046688
		public IList<string> SelectedAppointmentDescriptions
		{
			get
			{
				List<string> list = new List<string>();
				foreach (object obj in this.chks.Items)
				{
					ListItem listItem = (ListItem)obj;
					bool selected = listItem.Selected;
					if (selected)
					{
						string item = listItem.Text ?? "";
						bool flag = !list.Contains(item);
						if (flag)
						{
							list.Add(item);
						}
					}
				}
				return list;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000A79 RID: 2681 RVA: 0x0004852C File Offset: 0x0004672C
		private int personId
		{
			get
			{
				bool flag = this._personId < 1;
				if (flag)
				{
					this._personId = this.LookupStudentPid();
				}
				return this._personId;
			}
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x00048560 File Offset: 0x00046760
		private int LookupStudentPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
		}

		// Token: 0x04000803 RID: 2051
		protected Label lbl_noapps;

		// Token: 0x04000804 RID: 2052
		protected CheckBoxList chks;

		// Token: 0x04000805 RID: 2053
		private bool loadedApps;

		// Token: 0x04000806 RID: 2054
		private int _personId;
	}
}
