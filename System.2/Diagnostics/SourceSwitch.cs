using System;
using System.Security.Permissions;

namespace System.Diagnostics
{
	// Token: 0x020004A5 RID: 1189
	public class SourceSwitch : Switch
	{
		// Token: 0x06002C0D RID: 11277 RVA: 0x000C70AB File Offset: 0x000C52AB
		public SourceSwitch(string name) : base(name, string.Empty)
		{
		}

		// Token: 0x06002C0E RID: 11278 RVA: 0x000C70B9 File Offset: 0x000C52B9
		public SourceSwitch(string displayName, string defaultSwitchValue) : base(displayName, string.Empty, defaultSwitchValue)
		{
		}

		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x06002C0F RID: 11279 RVA: 0x000C70C8 File Offset: 0x000C52C8
		// (set) Token: 0x06002C10 RID: 11280 RVA: 0x000C70D0 File Offset: 0x000C52D0
		public SourceLevels Level
		{
			get
			{
				return (SourceLevels)base.SwitchSetting;
			}
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			set
			{
				base.SwitchSetting = (int)value;
			}
		}

		// Token: 0x06002C11 RID: 11281 RVA: 0x000C70D9 File Offset: 0x000C52D9
		public bool ShouldTrace(TraceEventType eventType)
		{
			return (base.SwitchSetting & (int)eventType) != 0;
		}

		// Token: 0x06002C12 RID: 11282 RVA: 0x000C70E6 File Offset: 0x000C52E6
		protected override void OnValueChanged()
		{
			base.SwitchSetting = (int)Enum.Parse(typeof(SourceLevels), base.Value, true);
		}
	}
}
