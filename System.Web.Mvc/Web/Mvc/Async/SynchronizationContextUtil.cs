using System;
using System.Threading;

namespace System.Web.Mvc.Async
{
	// Token: 0x02000126 RID: 294
	internal static class SynchronizationContextUtil
	{
		// Token: 0x060007BB RID: 1979 RVA: 0x00014DB8 File Offset: 0x00012FB8
		public static SynchronizationContext GetSynchronizationContext()
		{
			return SynchronizationContext.Current ?? new SynchronizationContext();
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x00014E0C File Offset: 0x0001300C
		public static T Sync<T>(this SynchronizationContext syncContext, Func<T> func)
		{
			T theValue = default(T);
			Exception thrownException = null;
			syncContext.Send(delegate(object o)
			{
				try
				{
					theValue = func();
				}
				catch (Exception thrownException)
				{
					thrownException = thrownException;
				}
			}, null);
			if (thrownException != null)
			{
				throw Error.SynchronizationContextUtil_ExceptionThrown(thrownException);
			}
			return theValue;
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x00014E94 File Offset: 0x00013094
		public static void Sync(this SynchronizationContext syncContext, Action action)
		{
			syncContext.Sync(delegate()
			{
				action();
				return default(AsyncVoid);
			});
		}
	}
}
