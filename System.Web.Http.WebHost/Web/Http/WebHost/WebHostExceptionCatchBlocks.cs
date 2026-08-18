using System;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.WebHost.Routing;

namespace System.Web.Http.WebHost
{
	// Token: 0x0200000E RID: 14
	public static class WebHostExceptionCatchBlocks
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000065 RID: 101 RVA: 0x000034CC File Offset: 0x000016CC
		public static ExceptionContextCatchBlock HttpControllerHandlerBufferContent
		{
			get
			{
				return WebHostExceptionCatchBlocks._httpControllerHandlerBufferContent;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000066 RID: 102 RVA: 0x000034D3 File Offset: 0x000016D3
		public static ExceptionContextCatchBlock HttpControllerHandlerBufferError
		{
			get
			{
				return WebHostExceptionCatchBlocks._httpControllerHandlerBufferError;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000067 RID: 103 RVA: 0x000034DA File Offset: 0x000016DA
		public static ExceptionContextCatchBlock HttpControllerHandlerComputeContentLength
		{
			get
			{
				return WebHostExceptionCatchBlocks._httpControllerHandlerComputeContentLength;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000068 RID: 104 RVA: 0x000034E1 File Offset: 0x000016E1
		public static ExceptionContextCatchBlock HttpControllerHandlerStreamContent
		{
			get
			{
				return WebHostExceptionCatchBlocks._httpControllerHandlerStreamContent;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000069 RID: 105 RVA: 0x000034E8 File Offset: 0x000016E8
		public static ExceptionContextCatchBlock HttpWebRoute
		{
			get
			{
				return WebHostExceptionCatchBlocks._httpWebRoute;
			}
		}

		// Token: 0x04000011 RID: 17
		private static readonly ExceptionContextCatchBlock _httpControllerHandlerBufferContent = new ExceptionContextCatchBlock(typeof(HttpControllerHandler).Name + ".BufferContent", true, true);

		// Token: 0x04000012 RID: 18
		private static readonly ExceptionContextCatchBlock _httpControllerHandlerBufferError = new ExceptionContextCatchBlock(typeof(HttpControllerHandler).Name + ".BufferError", true, false);

		// Token: 0x04000013 RID: 19
		private static readonly ExceptionContextCatchBlock _httpControllerHandlerComputeContentLength = new ExceptionContextCatchBlock(typeof(HttpControllerHandler).Name + ".ComputeContentLength", true, false);

		// Token: 0x04000014 RID: 20
		private static readonly ExceptionContextCatchBlock _httpControllerHandlerStreamContent = new ExceptionContextCatchBlock(typeof(HttpControllerHandler).Name + ".StreamContent", true, false);

		// Token: 0x04000015 RID: 21
		private static readonly ExceptionContextCatchBlock _httpWebRoute = new ExceptionContextCatchBlock(typeof(HttpWebRoute).Name, true, true);
	}
}
