using System;
using System.ComponentModel;
using System.Web.Http.Owin;

namespace System.Web.Http
{
	// Token: 0x02000010 RID: 16
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class OwinHttpConfigurationExtensions
	{
		// Token: 0x06000078 RID: 120 RVA: 0x00003171 File Offset: 0x00001371
		public static void SuppressDefaultHostAuthentication(this HttpConfiguration configuration)
		{
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			configuration.MessageHandlers.Insert(0, new PassiveAuthenticationMessageHandler());
		}
	}
}
