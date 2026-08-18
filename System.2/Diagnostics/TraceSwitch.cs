using System;
using System.Security.Permissions;

namespace System.Diagnostics
{
	// Token: 0x020004B8 RID: 1208
	[SwitchLevel(typeof(TraceLevel))]
	public class TraceSwitch : Switch
	{
		// Token: 0x06002D29 RID: 11561 RVA: 0x000CB5E9 File Offset: 0x000C97E9
		public TraceSwitch(string displayName, string description) : base(displayName, description)
		{
		}

		// Token: 0x06002D2A RID: 11562 RVA: 0x000CB5F3 File Offset: 0x000C97F3
		public TraceSwitch(string displayName, string description, string defaultSwitchValue) : base(displayName, description, defaultSwitchValue)
		{
		}

		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x06002D2B RID: 11563 RVA: 0x000CB5FE File Offset: 0x000C97FE
		// (set) Token: 0x06002D2C RID: 11564 RVA: 0x000CB606 File Offset: 0x000C9806
		public TraceLevel Level
		{
			get
			{
				return (TraceLevel)base.SwitchSetting;
			}
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			set
			{
				if (value < TraceLevel.Off || value > TraceLevel.Verbose)
				{
					throw new ArgumentException(SR.GetString("TraceSwitchInvalidLevel"));
				}
				base.SwitchSetting = (int)value;
			}
		}

		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x06002D2D RID: 11565 RVA: 0x000CB627 File Offset: 0x000C9827
		public bool TraceError
		{
			get
			{
				return this.Level >= TraceLevel.Error;
			}
		}

		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x06002D2E RID: 11566 RVA: 0x000CB635 File Offset: 0x000C9835
		public bool TraceWarning
		{
			get
			{
				return this.Level >= TraceLevel.Warning;
			}
		}

		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x06002D2F RID: 11567 RVA: 0x000CB643 File Offset: 0x000C9843
		public bool TraceInfo
		{
			get
			{
				return this.Level >= TraceLevel.Info;
			}
		}

		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x06002D30 RID: 11568 RVA: 0x000CB651 File Offset: 0x000C9851
		public bool TraceVerbose
		{
			get
			{
				return this.Level == TraceLevel.Verbose;
			}
		}

		// Token: 0x06002D31 RID: 11569 RVA: 0x000CB65C File Offset: 0x000C985C
		protected override void OnSwitchSettingChanged()
		{
			int switchSetting = base.SwitchSetting;
			if (switchSetting < 0)
			{
				Trace.WriteLine(SR.GetString("TraceSwitchLevelTooLow", new object[]
				{
					base.DisplayName
				}));
				base.SwitchSetting = 0;
				return;
			}
			if (switchSetting > 4)
			{
				Trace.WriteLine(SR.GetString("TraceSwitchLevelTooHigh", new object[]
				{
					base.DisplayName
				}));
				base.SwitchSetting = 4;
			}
		}

		// Token: 0x06002D32 RID: 11570 RVA: 0x000CB6C3 File Offset: 0x000C98C3
		protected override void OnValueChanged()
		{
			base.SwitchSetting = (int)Enum.Parse(typeof(TraceLevel), base.Value, true);
		}
	}
}
