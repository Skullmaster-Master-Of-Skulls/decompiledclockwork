using System;
using System.Web.Http.ExceptionHandling;

namespace System.Web.Http.Owin
{
	// Token: 0x0200000D RID: 13
	public static class OwinExceptionCatchBlocks
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002FF2 File Offset: 0x000011F2
		public static ExceptionContextCatchBlock HttpMessageHandlerAdapterBufferContent
		{
			get
			{
				return OwinExceptionCatchBlocks._httpMessageHandlerAdapterBufferContent;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002FF9 File Offset: 0x000011F9
		public static ExceptionContextCatchBlock HttpMessageHandlerAdapterBufferError
		{
			get
			{
				return OwinExceptionCatchBlocks._httpMessageHandlerAdapterBufferError;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00003000 File Offset: 0x00001200
		public static ExceptionContextCatchBlock HttpMessageHandlerAdapterComputeContentLength
		{
			get
			{
				return OwinExceptionCatchBlocks._httpMessageHandlerAdapterComputeContentLength;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00003007 File Offset: 0x00001207
		public static ExceptionContextCatchBlock HttpMessageHandlerAdapterStreamContent
		{
			get
			{
				return OwinExceptionCatchBlocks._httpMessageHandlerAdapterStreamContent;
			}
		}

		// Token: 0x04000009 RID: 9
		private static readonly ExceptionContextCatchBlock _httpMessageHandlerAdapterBufferContent = new ExceptionContextCatchBlock(typeof(HttpMessageHandlerAdapter).Name + ".BufferContent", true, true);

		// Token: 0x0400000A RID: 10
		private static readonly ExceptionContextCatchBlock _httpMessageHandlerAdapterBufferError = new ExceptionContextCatchBlock(typeof(HttpMessageHandlerAdapter).Name + ".BufferError", true, false);

		// Token: 0x0400000B RID: 11
		private static readonly ExceptionContextCatchBlock _httpMessageHandlerAdapterComputeContentLength = new ExceptionContextCatchBlock(typeof(HttpMessageHandlerAdapter).Name + ".ComputeContentLength", true, false);

		// Token: 0x0400000C RID: 12
		private static readonly ExceptionContextCatchBlock _httpMessageHandlerAdapterStreamContent = new ExceptionContextCatchBlock(typeof(HttpMessageHandlerAdapter).Name + ".StreamContent", true, false);
	}
}
