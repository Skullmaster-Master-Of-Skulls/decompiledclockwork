using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Razor;
using System.Web.SessionState;
using System.Web.WebPages.Resources;

namespace System.Web.WebPages
{
	// Token: 0x02000055 RID: 85
	internal static class SessionStateUtil
	{
		// Token: 0x0600020A RID: 522 RVA: 0x0000846E File Offset: 0x0000666E
		internal static void SetUpSessionState(HttpContextBase context, IHttpHandler handler)
		{
			SessionStateUtil.SetUpSessionState(context, handler, SessionStateUtil._sessionStateBehaviorCache);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000847C File Offset: 0x0000667C
		internal static void SetUpSessionState(HttpContextBase context, IHttpHandler handler, ConcurrentDictionary<Type, SessionStateBehavior?> cache)
		{
			WebPageHttpHandler webPageHttpHandler = handler as WebPageHttpHandler;
			SessionStateBehavior? sessionStateBehavior = SessionStateUtil.GetSessionStateBehavior(webPageHttpHandler.RequestedPage, cache);
			if (sessionStateBehavior != null)
			{
				context.SetSessionStateBehavior(sessionStateBehavior.Value);
				return;
			}
			WebPageRenderingBase webPageRenderingBase = webPageHttpHandler.StartPage;
			StartPage startPage;
			do
			{
				startPage = (webPageRenderingBase as StartPage);
				if (startPage != null)
				{
					sessionStateBehavior = SessionStateUtil.GetSessionStateBehavior(webPageRenderingBase, cache);
					webPageRenderingBase = startPage.ChildPage;
				}
			}
			while (startPage != null);
			if (sessionStateBehavior != null)
			{
				context.SetSessionStateBehavior(sessionStateBehavior.Value);
			}
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00008634 File Offset: 0x00006834
		private static SessionStateBehavior? GetSessionStateBehavior(WebPageExecutingBase page, ConcurrentDictionary<Type, SessionStateBehavior?> cache)
		{
			return cache.GetOrAdd(page.GetType(), delegate(Type type)
			{
				SessionStateBehavior value = SessionStateBehavior.Default;
				RazorDirectiveAttribute[] source = (RazorDirectiveAttribute[])type.GetCustomAttributes(typeof(RazorDirectiveAttribute), false);
				List<RazorDirectiveAttribute> list = (from attr in source
				where StringComparer.OrdinalIgnoreCase.Equals("sessionstate", attr.Name)
				select attr).ToList<RazorDirectiveAttribute>();
				if (!list.Any<RazorDirectiveAttribute>())
				{
					return null;
				}
				if (list.Count > 1)
				{
					throw new InvalidOperationException(WebPageResources.SessionState_TooManyValues);
				}
				RazorDirectiveAttribute razorDirectiveAttribute = list[0];
				if (!Enum.TryParse<SessionStateBehavior>(razorDirectiveAttribute.Value, true, out value))
				{
					IEnumerable<string> values = from SessionStateBehavior s in Enum.GetValues(typeof(SessionStateBehavior))
					select s.ToString();
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, WebPageResources.SessionState_InvalidValue, new object[]
					{
						razorDirectiveAttribute.Value,
						page.VirtualPath,
						string.Join(", ", values)
					}));
				}
				return new SessionStateBehavior?(value);
			});
		}

		// Token: 0x040000AA RID: 170
		private static readonly ConcurrentDictionary<Type, SessionStateBehavior?> _sessionStateBehaviorCache = new ConcurrentDictionary<Type, SessionStateBehavior?>();
	}
}
