using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000882 RID: 2178
	internal class OpaqueContent : HttpContent
	{
		// Token: 0x06005291 RID: 21137 RVA: 0x00130AA7 File Offset: 0x0012ECA7
		public OpaqueContent(MessageEncoder encoder, Message message, string mtomBoundary)
		{
			this.messageEncoder = encoder;
			this.message = message;
			this.mtomBoundary = mtomBoundary;
		}

		// Token: 0x17001463 RID: 5219
		// (get) Token: 0x06005292 RID: 21138 RVA: 0x00130AC4 File Offset: 0x0012ECC4
		public bool IsEmpty
		{
			get
			{
				return this.message.IsEmpty;
			}
		}

		// Token: 0x06005293 RID: 21139 RVA: 0x00130AD4 File Offset: 0x0012ECD4
		public void WriteToStream(Stream stream)
		{
			MtomMessageEncoder mtomMessageEncoder = this.messageEncoder as MtomMessageEncoder;
			if (mtomMessageEncoder == null)
			{
				this.messageEncoder.WriteMessage(this.message, stream);
				return;
			}
			mtomMessageEncoder.WriteMessage(this.message, stream, this.mtomBoundary);
		}

		// Token: 0x06005294 RID: 21140 RVA: 0x00130B18 File Offset: 0x0012ED18
		public IAsyncResult BeginWriteToStream(Stream stream, AsyncCallback callback, object state)
		{
			MtomMessageEncoder mtomMessageEncoder = this.messageEncoder as MtomMessageEncoder;
			if (mtomMessageEncoder == null)
			{
				return this.messageEncoder.BeginWriteMessage(this.message, stream, callback, state);
			}
			return mtomMessageEncoder.BeginWriteMessage(this.message, stream, this.mtomBoundary, callback, state);
		}

		// Token: 0x06005295 RID: 21141 RVA: 0x00130B5E File Offset: 0x0012ED5E
		public void EndWriteToStream(IAsyncResult result)
		{
			this.messageEncoder.EndWriteMessage(result);
		}

		// Token: 0x06005296 RID: 21142 RVA: 0x00130B6C File Offset: 0x0012ED6C
		protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("WebSocketOpaqueStreamContentNotSupportError")));
		}

		// Token: 0x06005297 RID: 21143 RVA: 0x00130B87 File Offset: 0x0012ED87
		protected override bool TryComputeLength(out long length)
		{
			length = 0L;
			return false;
		}

		// Token: 0x0400327C RID: 12924
		private MessageEncoder messageEncoder;

		// Token: 0x0400327D RID: 12925
		private Message message;

		// Token: 0x0400327E RID: 12926
		private string mtomBoundary;
	}
}
