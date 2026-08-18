using System;
using System.Net.Http;
using System.Threading;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Hosting;

namespace System.Web.Http.Owin
{
	// Token: 0x0200000F RID: 15
	public class HttpMessageHandlerOptions
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00003114 File Offset: 0x00001314
		// (set) Token: 0x0600006E RID: 110 RVA: 0x0000311C File Offset: 0x0000131C
		public HttpMessageHandler MessageHandler { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00003125 File Offset: 0x00001325
		// (set) Token: 0x06000070 RID: 112 RVA: 0x0000312D File Offset: 0x0000132D
		public IHostBufferPolicySelector BufferPolicySelector { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00003136 File Offset: 0x00001336
		// (set) Token: 0x06000072 RID: 114 RVA: 0x0000313E File Offset: 0x0000133E
		public IExceptionLogger ExceptionLogger { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00003147 File Offset: 0x00001347
		// (set) Token: 0x06000074 RID: 116 RVA: 0x0000314F File Offset: 0x0000134F
		public IExceptionHandler ExceptionHandler { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00003158 File Offset: 0x00001358
		// (set) Token: 0x06000076 RID: 118 RVA: 0x00003160 File Offset: 0x00001360
		public CancellationToken AppDisposing { get; set; }
	}
}
