using System;
using System.Collections.Generic;
using Microsoft.Owin;

namespace System.Web.Http.Owin
{
	// Token: 0x02000018 RID: 24
	internal static class OwinResponseExtensions
	{
		// Token: 0x060000B4 RID: 180 RVA: 0x00004FC8 File Offset: 0x000031C8
		public static void DisableBuffering(this IOwinResponse response)
		{
			if (response == null)
			{
				throw new ArgumentNullException("response");
			}
			IDictionary<string, object> environment = response.Environment;
			if (environment == null)
			{
				return;
			}
			Action action;
			if (!environment.TryGetValue("server.DisableResponseBuffering", out action))
			{
				return;
			}
			action();
		}

		// Token: 0x0400002E RID: 46
		private const string DisableResponseBufferingKey = "server.DisableResponseBuffering";
	}
}
