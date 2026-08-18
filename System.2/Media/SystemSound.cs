using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Media
{
	// Token: 0x020003A6 RID: 934
	[HostProtection(SecurityAction.LinkDemand, UI = true)]
	public class SystemSound
	{
		// Token: 0x060022E8 RID: 8936 RVA: 0x000A62A8 File Offset: 0x000A44A8
		internal SystemSound(int soundType)
		{
			this.soundType = soundType;
		}

		// Token: 0x060022E9 RID: 8937 RVA: 0x000A62B8 File Offset: 0x000A44B8
		public void Play()
		{
			IntSecurity.UnmanagedCode.Assert();
			try
			{
				SystemSound.SafeNativeMethods.MessageBeep(this.soundType);
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
		}

		// Token: 0x04001FBA RID: 8122
		private int soundType;

		// Token: 0x020007E6 RID: 2022
		private class SafeNativeMethods
		{
			// Token: 0x060043E2 RID: 17378 RVA: 0x0011DD05 File Offset: 0x0011BF05
			private SafeNativeMethods()
			{
			}

			// Token: 0x060043E3 RID: 17379
			[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
			internal static extern bool MessageBeep(int type);
		}
	}
}
