using System;
using System.Reflection;

namespace System.Web.Mvc
{
	// Token: 0x02000179 RID: 377
	internal sealed class ActionMethodDispatcherCache : ReaderWriterCache<MethodInfo, ActionMethodDispatcher>
	{
		// Token: 0x06000A18 RID: 2584 RVA: 0x0001BE78 File Offset: 0x0001A078
		public ActionMethodDispatcher GetDispatcher(MethodInfo methodInfo)
		{
			return base.FetchOrCreateItem<MethodInfo>(methodInfo, (MethodInfo methodInfoInner) => new ActionMethodDispatcher(methodInfoInner), methodInfo);
		}
	}
}
