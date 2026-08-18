using System;
using System.Security.Permissions;

namespace System.Diagnostics
{
	// Token: 0x02000493 RID: 1171
	[SwitchLevel(typeof(bool))]
	public class BooleanSwitch : Switch
	{
		// Token: 0x06002B63 RID: 11107 RVA: 0x000C5297 File Offset: 0x000C3497
		public BooleanSwitch(string displayName, string description) : base(displayName, description)
		{
		}

		// Token: 0x06002B64 RID: 11108 RVA: 0x000C52A1 File Offset: 0x000C34A1
		public BooleanSwitch(string displayName, string description, string defaultSwitchValue) : base(displayName, description, defaultSwitchValue)
		{
		}

		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x06002B65 RID: 11109 RVA: 0x000C52AC File Offset: 0x000C34AC
		// (set) Token: 0x06002B66 RID: 11110 RVA: 0x000C52B9 File Offset: 0x000C34B9
		public bool Enabled
		{
			get
			{
				return base.SwitchSetting != 0;
			}
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			set
			{
				base.SwitchSetting = (value ? 1 : 0);
			}
		}

		// Token: 0x06002B67 RID: 11111 RVA: 0x000C52C8 File Offset: 0x000C34C8
		protected override void OnValueChanged()
		{
			bool flag;
			if (bool.TryParse(base.Value, out flag))
			{
				base.SwitchSetting = (flag ? 1 : 0);
				return;
			}
			base.OnValueChanged();
		}
	}
}
