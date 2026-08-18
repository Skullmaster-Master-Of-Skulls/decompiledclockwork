using System;
using System.Diagnostics;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkLogger;
using ClockWorkWebAPI;
using ClockWorkWebAPIWeb;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.ClientManager.Core.DynamicForms;
using TechnoPro.Common.ClientManager.ICore.DynamicForms;
using TechnoPro.Common.Configuration;
using Telerik.Web.UI;

namespace TechnoPro.ClockWorkWeb.ctrls.DynamicForms
{
	// Token: 0x0200014B RID: 331
	public class ctrls_DynamicForms_CtrlPerDateData : UserControl
	{
		// Token: 0x06000A2B RID: 2603 RVA: 0x00003E0A File Offset: 0x0000200A
		protected void Page_Load(object sender, EventArgs e)
		{
		}

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06000A2C RID: 2604 RVA: 0x000471E8 File Offset: 0x000453E8
		// (remove) Token: 0x06000A2D RID: 2605 RVA: 0x00047220 File Offset: 0x00045420
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<ScreenNumberNeededArgs> OnScreenNumberNeeded;

		// Token: 0x06000A2E RID: 2606 RVA: 0x00047258 File Offset: 0x00045458
		private int FireOnScreenNumberNeeded()
		{
			EventHandler<ScreenNumberNeededArgs> onScreenNumberNeeded = this.OnScreenNumberNeeded;
			bool flag = onScreenNumberNeeded != null;
			int result;
			if (flag)
			{
				ScreenNumberNeededArgs screenNumberNeededArgs = new ScreenNumberNeededArgs
				{
					ScreenNum = 0
				};
				onScreenNumberNeeded(this, screenNumberNeededArgs);
				result = screenNumberNeededArgs.ScreenNum;
			}
			else
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x06000A2F RID: 2607 RVA: 0x0004729C File Offset: 0x0004549C
		// (remove) Token: 0x06000A30 RID: 2608 RVA: 0x000472D4 File Offset: 0x000454D4
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<PidNeededArgs> OnPidNeeded;

		// Token: 0x06000A31 RID: 2609 RVA: 0x0004730C File Offset: 0x0004550C
		private int FireOnPidNeeded()
		{
			EventHandler<PidNeededArgs> onPidNeeded = this.OnPidNeeded;
			bool flag = onPidNeeded != null;
			int result;
			if (flag)
			{
				PidNeededArgs pidNeededArgs = new PidNeededArgs
				{
					Pid = 0
				};
				onPidNeeded(this, pidNeededArgs);
				result = pidNeededArgs.Pid;
			}
			else
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x00047350 File Offset: 0x00045550
		private void Page_Init(object sender, EventArgs e)
		{
			DynamicControlLayoutHelper dynamicControlLayoutHelper = null;
			db conn = new db(ClockWorkConfigurationManager.GetConnectionStringByNameUsingProtection("clockwork"));
			string exemptCids = "";
			int screenNum = this.FireOnScreenNumberNeeded();
			DynamicScreenLayout.ControlsToScreen(ref dynamicControlLayoutHelper, base.Cache, conn, screenNum, this.p_data, null, false, false, exemptCids);
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000A33 RID: 2611 RVA: 0x00047398 File Offset: 0x00045598
		private IDynamicDataClientManager dynamicDataClientManager
		{
			get
			{
				bool flag = this._dynamicDataClientManager == null;
				if (flag)
				{
					this._dynamicDataClientManager = new DynamicDataClientManager();
				}
				return this._dynamicDataClientManager;
			}
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x000473C8 File Offset: 0x000455C8
		public bool Save()
		{
			int num = this.FireOnScreenNumberNeeded();
			bool flag = num < 1;
			bool result;
			if (flag)
			{
				CWLogger.Logger.Error("CtrlPerDateData:TryingToSaveButScreenNum={0}", num.ToString());
				result = false;
			}
			else
			{
				int num2 = this.FireOnPidNeeded();
				bool flag2 = num2 < 1;
				if (flag2)
				{
					CWLogger.Logger.Error("CtrlPerDateData:TryingToSaveButPid={0}", num2.ToString());
					result = false;
				}
				else
				{
					int num3 = this.dynamicDataClientManager.CreatePerDateEntry(new PerDateEntryDTO
					{
						ScreenNum = num,
						DateEntered = DateTime.Now,
						Student = new PersonBaseDTO
						{
							PersonId = num2
						},
						WhoEntered = new PersonBaseDTO
						{
							PersonId = num2
						}
					});
					int appId = num3;
					Exception ex = DynamicScreenLayout.SaveDynamicData(ScreenType.ScreenType_PerDate, num2, appId, num, base.Cache, this.p_data, "");
					bool flag3 = ex != null;
					if (flag3)
					{
						CWLogger.Logger.Error("CtrlPerDateData save form Error: {0}", ex.ToString());
						result = false;
					}
					else
					{
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x040007E0 RID: 2016
		protected RadCodeBlock RadCodeBlock1;

		// Token: 0x040007E1 RID: 2017
		protected ValidationSummary vsumAll;

		// Token: 0x040007E2 RID: 2018
		protected Panel p_data;

		// Token: 0x040007E5 RID: 2021
		private IDynamicDataClientManager _dynamicDataClientManager;
	}
}
