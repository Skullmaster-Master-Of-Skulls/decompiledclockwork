using System;
using System.Collections.Generic;
using System.Web.Http.Controllers;

namespace System.Web.Http.Routing
{
	// Token: 0x02000085 RID: 133
	internal static class HttpRouteExtensions
	{
		// Token: 0x06000373 RID: 883 RVA: 0x0000AD18 File Offset: 0x00008F18
		public static CandidateAction[] GetDirectRouteCandidates(this IHttpRoute route)
		{
			IDictionary<string, object> dataTokens = route.DataTokens;
			if (dataTokens == null)
			{
				return null;
			}
			List<CandidateAction> list = new List<CandidateAction>();
			HttpActionDescriptor[] array = null;
			HttpActionDescriptor[] array2;
			if (dataTokens.TryGetValue("actions", out array2) && array2 != null && array2.Length > 0)
			{
				array = array2;
			}
			if (array == null)
			{
				return null;
			}
			int order = 0;
			int num;
			if (dataTokens.TryGetValue("order", out num))
			{
				order = num;
			}
			decimal precedence = 0m;
			decimal num2;
			if (dataTokens.TryGetValue("precedence", out num2))
			{
				precedence = num2;
			}
			foreach (HttpActionDescriptor actionDescriptor in array)
			{
				list.Add(new CandidateAction
				{
					ActionDescriptor = actionDescriptor,
					Order = order,
					Precedence = precedence
				});
			}
			return list.ToArray();
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000ADD8 File Offset: 0x00008FD8
		public static HttpActionDescriptor[] GetTargetActionDescriptors(this IHttpRoute route)
		{
			IDictionary<string, object> dataTokens = route.DataTokens;
			if (dataTokens == null)
			{
				return null;
			}
			HttpActionDescriptor[] result;
			if (!dataTokens.TryGetValue("actions", out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000AE04 File Offset: 0x00009004
		public static HttpControllerDescriptor GetTargetControllerDescriptor(this IHttpRoute route)
		{
			IDictionary<string, object> dataTokens = route.DataTokens;
			if (dataTokens == null)
			{
				return null;
			}
			HttpControllerDescriptor result;
			if (!dataTokens.TryGetValue("controller", out result))
			{
				return null;
			}
			return result;
		}
	}
}
