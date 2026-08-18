using System;
using System.Threading;

namespace Renci.SshNet.Abstractions
{
	// Token: 0x0200011A RID: 282
	internal static class ThreadAbstraction
	{
		// Token: 0x06000C20 RID: 3104 RVA: 0x000275B7 File Offset: 0x000257B7
		public static void Sleep(int millisecondsTimeout)
		{
			Thread.Sleep(millisecondsTimeout);
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x000275BF File Offset: 0x000257BF
		public static void ExecuteThread(Action action)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			ThreadPool.QueueUserWorkItem(delegate(object o)
			{
				action();
			});
		}
	}
}
