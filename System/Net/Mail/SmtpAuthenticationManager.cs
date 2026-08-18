using System;
using System.Collections;

namespace System.Net.Mail
{
	// Token: 0x020006B8 RID: 1720
	internal static class SmtpAuthenticationManager
	{
		// Token: 0x06003523 RID: 13603 RVA: 0x000E1EA2 File Offset: 0x000E0EA2
		static SmtpAuthenticationManager()
		{
			if (ComNetOS.IsWin2K)
			{
				SmtpAuthenticationManager.Register(new SmtpNegotiateAuthenticationModule());
			}
			SmtpAuthenticationManager.Register(new SmtpNtlmAuthenticationModule());
			SmtpAuthenticationManager.Register(new SmtpDigestAuthenticationModule());
			SmtpAuthenticationManager.Register(new SmtpLoginAuthenticationModule());
		}

		// Token: 0x06003524 RID: 13604 RVA: 0x000E1EE0 File Offset: 0x000E0EE0
		internal static void Register(ISmtpAuthenticationModule module)
		{
			if (module == null)
			{
				throw new ArgumentNullException("module");
			}
			lock (SmtpAuthenticationManager.modules)
			{
				SmtpAuthenticationManager.modules.Add(module);
			}
		}

		// Token: 0x06003525 RID: 13605 RVA: 0x000E1F2C File Offset: 0x000E0F2C
		internal static ISmtpAuthenticationModule[] GetModules()
		{
			ISmtpAuthenticationModule[] result;
			lock (SmtpAuthenticationManager.modules)
			{
				ISmtpAuthenticationModule[] array = new ISmtpAuthenticationModule[SmtpAuthenticationManager.modules.Count];
				SmtpAuthenticationManager.modules.CopyTo(0, array, 0, SmtpAuthenticationManager.modules.Count);
				result = array;
			}
			return result;
		}

		// Token: 0x040030BD RID: 12477
		private static ArrayList modules = new ArrayList();
	}
}
