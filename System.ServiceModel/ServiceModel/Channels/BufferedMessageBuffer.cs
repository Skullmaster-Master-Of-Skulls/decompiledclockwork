using System;
using System.Collections.Generic;
using System.IO;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009C7 RID: 2503
	internal class BufferedMessageBuffer : MessageBuffer
	{
		// Token: 0x0600624F RID: 25167 RVA: 0x0016DEE0 File Offset: 0x0016C0E0
		public BufferedMessageBuffer(IBufferedMessageData messageData, KeyValuePair<string, object>[] properties, bool[] understoodHeaders, bool understoodHeadersModified)
		{
			this.messageData = messageData;
			this.properties = properties;
			this.understoodHeaders = understoodHeaders;
			this.understoodHeadersModified = understoodHeadersModified;
			messageData.Open();
		}

		// Token: 0x170017AF RID: 6063
		// (get) Token: 0x06006250 RID: 25168 RVA: 0x0016DF18 File Offset: 0x0016C118
		public override int BufferSize
		{
			get
			{
				object obj = this.ThisLock;
				int count;
				lock (obj)
				{
					if (this.closed)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(base.CreateBufferDisposedException());
					}
					count = this.messageData.Buffer.Count;
				}
				return count;
			}
		}

		// Token: 0x06006251 RID: 25169 RVA: 0x0016DF80 File Offset: 0x0016C180
		public override void WriteMessage(Stream stream)
		{
			if (stream == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("stream"));
			}
			object obj = this.ThisLock;
			lock (obj)
			{
				if (this.closed)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(base.CreateBufferDisposedException());
				}
				ArraySegment<byte> buffer = this.messageData.Buffer;
				stream.Write(buffer.Array, buffer.Offset, buffer.Count);
			}
		}

		// Token: 0x170017B0 RID: 6064
		// (get) Token: 0x06006252 RID: 25170 RVA: 0x0016E014 File Offset: 0x0016C214
		public override string MessageContentType
		{
			get
			{
				object obj = this.ThisLock;
				string contentType;
				lock (obj)
				{
					if (this.closed)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(base.CreateBufferDisposedException());
					}
					contentType = this.messageData.MessageEncoder.ContentType;
				}
				return contentType;
			}
		}

		// Token: 0x170017B1 RID: 6065
		// (get) Token: 0x06006253 RID: 25171 RVA: 0x0016E07C File Offset: 0x0016C27C
		private object ThisLock
		{
			get
			{
				return this.thisLock;
			}
		}

		// Token: 0x06006254 RID: 25172 RVA: 0x0016E084 File Offset: 0x0016C284
		public override void Close()
		{
			object obj = this.ThisLock;
			lock (obj)
			{
				if (!this.closed)
				{
					this.closed = true;
					this.messageData.Close();
					this.messageData = null;
				}
			}
		}

		// Token: 0x06006255 RID: 25173 RVA: 0x0016E0E0 File Offset: 0x0016C2E0
		public override Message CreateMessage()
		{
			object obj = this.ThisLock;
			Message result;
			lock (obj)
			{
				if (this.closed)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(base.CreateBufferDisposedException());
				}
				RecycledMessageState recycledMessageState = this.messageData.TakeMessageState();
				if (recycledMessageState == null)
				{
					recycledMessageState = new RecycledMessageState();
				}
				BufferedMessage bufferedMessage = new BufferedMessage(this.messageData, recycledMessageState, this.understoodHeaders, this.understoodHeadersModified);
				bufferedMessage.Properties.CopyProperties(this.properties);
				this.messageData.Open();
				result = bufferedMessage;
			}
			return result;
		}

		// Token: 0x0400390E RID: 14606
		private IBufferedMessageData messageData;

		// Token: 0x0400390F RID: 14607
		private KeyValuePair<string, object>[] properties;

		// Token: 0x04003910 RID: 14608
		private bool closed;

		// Token: 0x04003911 RID: 14609
		private object thisLock = new object();

		// Token: 0x04003912 RID: 14610
		private bool[] understoodHeaders;

		// Token: 0x04003913 RID: 14611
		private bool understoodHeadersModified;
	}
}
