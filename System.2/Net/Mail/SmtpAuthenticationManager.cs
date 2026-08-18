using System;
using System.Collections;

namespace System.Net.Mail
{
	// Token: 0x02000277 RID: 631
	internal static class SmtpAuthenticationManager
	{
		// Token: 0x060017B9 RID: 6073 RVA: 0x00078EF8 File Offset: 0x000770F8
		static SmtpAuthenticationManager()
		{
			SmtpAuthenticationManager.Register(new SmtpNegotiateAuthenticationModule());
			SmtpAuthenticationManager.Register(new SmtpNtlmAuthenticationModule());
			SmtpAuthenticationManager.Register(new SmtpDigestAuthenticationModule());
			SmtpAuthenticationManager.Register(new SmtpLoginAuthenticationModule());
		}

		// Token: 0x060017BA RID: 6074 RVA: 0x00078F2C File Offset: 0x0007712C
		internal static void Register(ISmtpAuthenticationModule module)
		{
			if (module == null)
			{
				throw new ArgumentNullException("module");
			}
			ArrayList obj = SmtpAuthenticationManager.modules;
			lock (obj)
			{
				SmtpAuthenticationManager.modules.Add(module);
			}
		}

		// Token: 0x060017BB RID: 6075 RVA: 0x00078F80 File Offset: 0x00077180
		internal static ISmtpAuthenticationModule[] GetModules()
		{
			ArrayList obj = SmtpAuthenticationManager.modules;
			ISmtpAuthenticationModule[] result;
			lock (obj)
			{
				ISmtpAuthenticationModule[] array = new ISmtpAuthenticationModule[SmtpAuthenticationManager.modules.Count];
				SmtpAuthenticationManager.modules.CopyTo(0, array, 0, SmtpAuthenticationManager.modules.Count);
				result = array;
			}
			return result;
		}

		// Token: 0x04001802 RID: 6146
		private static ArrayList modules = new ArrayList();
	}
}
