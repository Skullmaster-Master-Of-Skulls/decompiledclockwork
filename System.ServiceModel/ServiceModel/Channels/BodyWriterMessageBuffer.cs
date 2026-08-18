using System;
using System.Collections.Generic;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009C8 RID: 2504
	internal class BodyWriterMessageBuffer : MessageBuffer
	{
		// Token: 0x06006256 RID: 25174 RVA: 0x0016E184 File Offset: 0x0016C384
		public BodyWriterMessageBuffer(MessageHeaders headers, KeyValuePair<string, object>[] properties, BodyWriter bodyWriter)
		{
			this.bodyWriter = bodyWriter;
			this.headers = new MessageHeaders(headers);
			this.properties = properties;
		}

		// Token: 0x170017B2 RID: 6066
		// (get) Token: 0x06006257 RID: 25175 RVA: 0x0016E1B1 File Offset: 0x0016C3B1
		protected object ThisLock
		{
			get
			{
				return this.thisLock;
			}
		}

		// Token: 0x170017B3 RID: 6067
		// (get) Token: 0x06006258 RID: 25176 RVA: 0x0016E1B9 File Offset: 0x0016C3B9
		public override int BufferSize
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06006259 RID: 25177 RVA: 0x0016E1BC File Offset: 0x0016C3BC
		public override void Close()
		{
			object obj = this.ThisLock;
			lock (obj)
			{
				if (!this.closed)
				{
					this.closed = true;
					this.bodyWriter = null;
					this.headers = null;
					this.properties = null;
				}
			}
		}

		// Token: 0x0600625A RID: 25178 RVA: 0x0016E21C File Offset: 0x0016C41C
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
				result = new BodyWriterMessage(this.headers, this.properties, this.bodyWriter);
			}
			return result;
		}

		// Token: 0x170017B4 RID: 6068
		// (get) Token: 0x0600625B RID: 25179 RVA: 0x0016E288 File Offset: 0x0016C488
		protected BodyWriter BodyWriter
		{
			get
			{
				return this.bodyWriter;
			}
		}

		// Token: 0x170017B5 RID: 6069
		// (get) Token: 0x0600625C RID: 25180 RVA: 0x0016E290 File Offset: 0x0016C490
		protected MessageHeaders Headers
		{
			get
			{
				return this.headers;
			}
		}

		// Token: 0x170017B6 RID: 6070
		// (get) Token: 0x0600625D RID: 25181 RVA: 0x0016E298 File Offset: 0x0016C498
		protected KeyValuePair<string, object>[] Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x170017B7 RID: 6071
		// (get) Token: 0x0600625E RID: 25182 RVA: 0x0016E2A0 File Offset: 0x0016C4A0
		protected bool Closed
		{
			get
			{
				return this.closed;
			}
		}

		// Token: 0x04003914 RID: 14612
		private BodyWriter bodyWriter;

		// Token: 0x04003915 RID: 14613
		private KeyValuePair<string, object>[] properties;

		// Token: 0x04003916 RID: 14614
		private MessageHeaders headers;

		// Token: 0x04003917 RID: 14615
		private bool closed;

		// Token: 0x04003918 RID: 14616
		private object thisLock = new object();
	}
}
