using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace System.Net.Http
{
	// Token: 0x02000016 RID: 22
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class OwinHttpRequestMessageExtensions
	{
		// Token: 0x060000AD RID: 173 RVA: 0x00004E0C File Offset: 0x0000300C
		public static IOwinContext GetOwinContext(this HttpRequestMessage request)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			IOwinContext owinContext;
			IDictionary<string, object> environment;
			if (!request.Properties.TryGetValue("MS_OwinContext", out owinContext) && request.Properties.TryGetValue("MS_OwinEnvironment", out environment))
			{
				owinContext = new OwinContext(environment);
				request.SetOwinContext(owinContext);
				request.Properties.Remove("MS_OwinEnvironment");
			}
			return owinContext;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00004E6F File Offset: 0x0000306F
		public static void SetOwinContext(this HttpRequestMessage request, IOwinContext context)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			request.Properties["MS_OwinContext"] = context;
			request.Properties.Remove("MS_OwinEnvironment");
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00004EB0 File Offset: 0x000030B0
		public static IDictionary<string, object> GetOwinEnvironment(this HttpRequestMessage request)
		{
			IOwinContext owinContext = request.GetOwinContext();
			if (owinContext == null)
			{
				return null;
			}
			return owinContext.Environment;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004ECF File Offset: 0x000030CF
		public static void SetOwinEnvironment(this HttpRequestMessage request, IDictionary<string, object> environment)
		{
			request.SetOwinContext(new OwinContext(environment));
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004EE0 File Offset: 0x000030E0
		internal static IAuthenticationManager GetAuthenticationManager(this HttpRequestMessage request)
		{
			IOwinContext owinContext = request.GetOwinContext();
			if (owinContext == null)
			{
				return null;
			}
			return owinContext.Authentication;
		}

		// Token: 0x0400002A RID: 42
		private const string OwinEnvironmentKey = "MS_OwinEnvironment";

		// Token: 0x0400002B RID: 43
		private const string OwinContextKey = "MS_OwinContext";
	}
}
