using System;
using Owin;

namespace Microsoft.Owin.Security
{
	// Token: 0x02000002 RID: 2
	public static class AppBuilderSecurityExtensions
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public static string GetDefaultSignInAsAuthenticationType(this IAppBuilder app)
		{
			if (app == null)
			{
				throw new ArgumentNullException("app");
			}
			object obj;
			if (app.Properties.TryGetValue("Microsoft.Owin.Security.Constants.DefaultSignInAsAuthenticationType", out obj))
			{
				string text = obj as string;
				if (!string.IsNullOrEmpty(text))
				{
					return text;
				}
			}
			throw new InvalidOperationException(Resources.Exception_MissingDefaultSignInAsAuthenticationType);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000211A File Offset: 0x0000031A
		public static void SetDefaultSignInAsAuthenticationType(this IAppBuilder app, string authenticationType)
		{
			if (app == null)
			{
				throw new ArgumentNullException("app");
			}
			if (authenticationType == null)
			{
				throw new ArgumentNullException("authenticationType");
			}
			app.Properties["Microsoft.Owin.Security.Constants.DefaultSignInAsAuthenticationType"] = authenticationType;
		}
	}
}
