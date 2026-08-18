using System;
using System.Web.Http.Batch;
using System.Web.Http.Dispatcher;
using System.Web.Http.Filters;

namespace System.Web.Http.ExceptionHandling
{
	// Token: 0x02000044 RID: 68
	public static class ExceptionCatchBlocks
	{
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00007933 File Offset: 0x00005B33
		public static ExceptionContextCatchBlock HttpBatchHandler
		{
			get
			{
				return ExceptionCatchBlocks._httpBatchHandler;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600018A RID: 394 RVA: 0x0000793A File Offset: 0x00005B3A
		public static ExceptionContextCatchBlock HttpControllerDispatcher
		{
			get
			{
				return ExceptionCatchBlocks._httpControllerDispatcher;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00007941 File Offset: 0x00005B41
		public static ExceptionContextCatchBlock HttpServer
		{
			get
			{
				return ExceptionCatchBlocks._httpServer;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00007948 File Offset: 0x00005B48
		public static ExceptionContextCatchBlock IExceptionFilter
		{
			get
			{
				return ExceptionCatchBlocks._exceptionFilter;
			}
		}

		// Token: 0x0400008E RID: 142
		private static readonly ExceptionContextCatchBlock _httpBatchHandler = new ExceptionContextCatchBlock(typeof(HttpBatchHandler).Name, false, true);

		// Token: 0x0400008F RID: 143
		private static readonly ExceptionContextCatchBlock _httpControllerDispatcher = new ExceptionContextCatchBlock(typeof(HttpControllerDispatcher).Name, false, true);

		// Token: 0x04000090 RID: 144
		private static readonly ExceptionContextCatchBlock _httpServer = new ExceptionContextCatchBlock(typeof(HttpServer).Name, true, true);

		// Token: 0x04000091 RID: 145
		private static readonly ExceptionContextCatchBlock _exceptionFilter = new ExceptionContextCatchBlock(typeof(IExceptionFilter).Name, false, true);
	}
}
