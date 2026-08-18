using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x02000006 RID: 6
	internal static class AsyncHelper
	{
		// Token: 0x06000010 RID: 16 RVA: 0x000023C8 File Offset: 0x000005C8
		public static TResult RunSync<TResult>(Func<Task<TResult>> func)
		{
			CultureInfo cultureUi = CultureInfo.CurrentUICulture;
			CultureInfo culture = CultureInfo.CurrentCulture;
			return AsyncHelper._myTaskFactory.StartNew<Task<TResult>>(delegate()
			{
				Thread.CurrentThread.CurrentCulture = culture;
				Thread.CurrentThread.CurrentUICulture = cultureUi;
				return func();
			}).Unwrap<TResult>().GetAwaiter().GetResult();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002458 File Offset: 0x00000658
		public static void RunSync(Func<Task> func)
		{
			CultureInfo cultureUi = CultureInfo.CurrentUICulture;
			CultureInfo culture = CultureInfo.CurrentCulture;
			AsyncHelper._myTaskFactory.StartNew<Task>(delegate()
			{
				Thread.CurrentThread.CurrentCulture = culture;
				Thread.CurrentThread.CurrentUICulture = cultureUi;
				return func();
			}).Unwrap().GetAwaiter().GetResult();
		}

		// Token: 0x04000003 RID: 3
		private static readonly TaskFactory _myTaskFactory = new TaskFactory(CancellationToken.None, TaskCreationOptions.None, TaskContinuationOptions.None, TaskScheduler.Default);
	}
}
