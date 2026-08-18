using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Security.Principal;

namespace TechnoPro.Common.Win32
{
	// Token: 0x02000015 RID: 21
	public static class UAC
	{
		// Token: 0x0600007B RID: 123 RVA: 0x000047D4 File Offset: 0x000029D4
		public static void RunElevated(string filename, params string[] args)
		{
			ProcessStartInfo processStartInfo = new ProcessStartInfo
			{
				Verb = "runas",
				FileName = filename
			};
			if (args != null && args.Length != 0)
			{
				processStartInfo.Arguments = string.Join(" ", args);
			}
			try
			{
				Process.Start(processStartInfo);
			}
			catch (Win32Exception)
			{
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00004830 File Offset: 0x00002A30
		public static bool HasAdministrativeRight
		{
			get
			{
				return new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
			}
		}
	}
}
