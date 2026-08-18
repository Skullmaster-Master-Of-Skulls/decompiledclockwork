using System;
using System.Diagnostics;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkWebAPI;
using ClockWorkWebAPIWeb;
using TechnoPro.Common.UI.ClientManager.Web.Auth;

namespace TechnoPro.ClockWorkWeb.ctrls
{
	// Token: 0x0200011F RID: 287
	public class ctrls_AppointmentNotes : UserControl
	{
		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06000835 RID: 2101 RVA: 0x0003B640 File Offset: 0x00039840
		// (remove) Token: 0x06000836 RID: 2102 RVA: 0x0003B678 File Offset: 0x00039878
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event ctrls_AppointmentNotes.SaveFormHandler SaveClicked;

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000837 RID: 2103 RVA: 0x0003B6B0 File Offset: 0x000398B0
		// (set) Token: 0x06000838 RID: 2104 RVA: 0x0003B6C8 File Offset: 0x000398C8
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

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000839 RID: 2105 RVA: 0x0003B6D4 File Offset: 0x000398D4
		// (set) Token: 0x0600083A RID: 2106 RVA: 0x0003B6EB File Offset: 0x000398EB
		public int ScreenNum
		{
			get
			{
				return ctrls_AppointmentNotes.screenNum;
			}
			set
			{
				ctrls_AppointmentNotes.screenNum = value;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x0600083B RID: 2107 RVA: 0x0003B6F4 File Offset: 0x000398F4
		// (set) Token: 0x0600083C RID: 2108 RVA: 0x0003B70C File Offset: 0x0003990C
		public string Title
		{
			get
			{
				return this.title;
			}
			set
			{
				this.title = value;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x0600083D RID: 2109 RVA: 0x0003B718 File Offset: 0x00039918
		// (set) Token: 0x0600083E RID: 2110 RVA: 0x0003B730 File Offset: 0x00039930
		public string Intro
		{
			get
			{
				return this.intro;
			}
			set
			{
				this.intro = value;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x0600083F RID: 2111 RVA: 0x0003B73C File Offset: 0x0003993C
		// (set) Token: 0x06000840 RID: 2112 RVA: 0x0003B753 File Offset: 0x00039953
		public int AppId
		{
			get
			{
				return ctrls_AppointmentNotes.appId;
			}
			set
			{
				ctrls_AppointmentNotes.appId = value;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000841 RID: 2113 RVA: 0x0003B75C File Offset: 0x0003995C
		// (set) Token: 0x06000842 RID: 2114 RVA: 0x0003B774 File Offset: 0x00039974
		public bool LoadExistingData
		{
			get
			{
				return this.loadExistingData;
			}
			set
			{
				this.loadExistingData = value;
			}
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x0003B780 File Offset: 0x00039980
		private int GetPid()
		{
			return WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x0003B7A2 File Offset: 0x000399A2
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x0003B7B0 File Offset: 0x000399B0
		protected void btn_submit_Click(object sender, EventArgs e)
		{
			bool flag = this.pid <= 0;
			if (flag)
			{
				this.pid = this.GetPid();
			}
			bool flag2 = this.pid > 0 && ctrls_AppointmentNotes.screenNum > 0 && ctrls_AppointmentNotes.appId > 0;
			if (flag2)
			{
				DynamicScreenLayout.SaveDynamicData(ScreenType.ScreenType_PerAppointment, this.pid, ctrls_AppointmentNotes.appId, ctrls_AppointmentNotes.screenNum, base.Cache, this.p_data, "");
				base.Response.Redirect("myupcomingappts.aspx");
			}
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x0003B835 File Offset: 0x00039A35
		protected void btn_cancel_Click(object sender, EventArgs e)
		{
			base.Response.Redirect("myupcomingappts.aspx", true);
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x0003B84C File Offset: 0x00039A4C
		private void FireSaveClicked()
		{
			bool flag = this.SaveClicked != null;
			if (flag)
			{
				this.SaveClicked(this, new EventArgs(), this.pid, ctrls_AppointmentNotes.appId, ctrls_AppointmentNotes.screenNum, this.p_data);
			}
		}

		// Token: 0x04000651 RID: 1617
		protected Panel p_title;

		// Token: 0x04000652 RID: 1618
		protected Label lbl_title;

		// Token: 0x04000653 RID: 1619
		protected Panel p_intro;

		// Token: 0x04000654 RID: 1620
		protected Label lbl_intro;

		// Token: 0x04000655 RID: 1621
		protected Panel p_data;

		// Token: 0x04000656 RID: 1622
		protected Panel p_action;

		// Token: 0x04000657 RID: 1623
		protected Button btn_cancel;

		// Token: 0x04000658 RID: 1624
		protected Button btn_submit;

		// Token: 0x0400065A RID: 1626
		private int pid = 0;

		// Token: 0x0400065B RID: 1627
		private static int appId = 0;

		// Token: 0x0400065C RID: 1628
		private static int screenNum = 0;

		// Token: 0x0400065D RID: 1629
		private string title = "";

		// Token: 0x0400065E RID: 1630
		private string intro = "";

		// Token: 0x0400065F RID: 1631
		private bool loadExistingData = false;

		// Token: 0x0200023A RID: 570
		// (Invoke) Token: 0x06000EC6 RID: 3782
		public delegate void SaveFormHandler(object sender, EventArgs e, int pid, int appId, int screenNum, Panel p_data);
	}
}
