using System;
using System.Collections.Generic;
using System.Runtime;
using System.ServiceModel.Diagnostics;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009BC RID: 2492
	internal class BodyWriterMessage : Message
	{
		// Token: 0x060061E1 RID: 25057 RVA: 0x0016C7EC File Offset: 0x0016A9EC
		private BodyWriterMessage(BodyWriter bodyWriter)
		{
			this.bodyWriter = bodyWriter;
		}

		// Token: 0x060061E2 RID: 25058 RVA: 0x0016C7FB File Offset: 0x0016A9FB
		public BodyWriterMessage(MessageVersion version, string action, BodyWriter bodyWriter) : this(bodyWriter)
		{
			this.headers = new MessageHeaders(version);
			this.headers.Action = action;
		}

		// Token: 0x060061E3 RID: 25059 RVA: 0x0016C81C File Offset: 0x0016AA1C
		public BodyWriterMessage(MessageVersion version, ActionHeader actionHeader, BodyWriter bodyWriter) : this(bodyWriter)
		{
			this.headers = new MessageHeaders(version);
			this.headers.SetActionHeader(actionHeader);
		}

		// Token: 0x060061E4 RID: 25060 RVA: 0x0016C83D File Offset: 0x0016AA3D
		public BodyWriterMessage(MessageHeaders headers, KeyValuePair<string, object>[] properties, BodyWriter bodyWriter) : this(bodyWriter)
		{
			this.headers = new MessageHeaders(headers);
			this.properties = new MessageProperties(properties);
		}

		// Token: 0x17001792 RID: 6034
		// (get) Token: 0x060061E5 RID: 25061 RVA: 0x0016C85E File Offset: 0x0016AA5E
		public override bool IsFault
		{
			get
			{
				if (base.IsDisposed)
				{
					throw TraceUtility.ThrowHelperError(base.CreateMessageDisposedException(), this);
				}
				return this.bodyWriter.IsFault;
			}
		}

		// Token: 0x17001793 RID: 6035
		// (get) Token: 0x060061E6 RID: 25062 RVA: 0x0016C880 File Offset: 0x0016AA80
		public override bool IsEmpty
		{
			get
			{
				if (base.IsDisposed)
				{
					throw TraceUtility.ThrowHelperError(base.CreateMessageDisposedException(), this);
				}
				return this.bodyWriter.IsEmpty;
			}
		}

		// Token: 0x17001794 RID: 6036
		// (get) Token: 0x060061E7 RID: 25063 RVA: 0x0016C8A2 File Offset: 0x0016AAA2
		public override MessageHeaders Headers
		{
			get
			{
				if (base.IsDisposed)
				{
					throw TraceUtility.ThrowHelperError(base.CreateMessageDisposedException(), this);
				}
				return this.headers;
			}
		}

		// Token: 0x17001795 RID: 6037
		// (get) Token: 0x060061E8 RID: 25064 RVA: 0x0016C8BF File Offset: 0x0016AABF
		public override MessageProperties Properties
		{
			get
			{
				if (base.IsDisposed)
				{
					throw TraceUtility.ThrowHelperError(base.CreateMessageDisposedException(), this);
				}
				if (this.properties == null)
				{
					this.properties = new MessageProperties();
				}
				return this.properties;
			}
		}

		// Token: 0x060061E9 RID: 25065 RVA: 0x0016C8F0 File Offset: 0x0016AAF0
		internal override void SetProperty(string name, object value)
		{
			MessageProperties messageProperties = this.properties;
			if (messageProperties != null)
			{
				messageProperties[name] = value;
			}
		}

		// Token: 0x060061EA RID: 25066 RVA: 0x0016C910 File Offset: 0x0016AB10
		internal override bool GetProperty(string name, out object result)
		{
			MessageProperties messageProperties = this.properties;
			if (messageProperties != null)
			{
				return messageProperties.TryGetValue(name, out result);
			}
			result = null;
			return false;
		}

		// Token: 0x17001796 RID: 6038
		// (get) Token: 0x060061EB RID: 25067 RVA: 0x0016C934 File Offset: 0x0016AB34
		public override MessageVersion Version
		{
			get
			{
				if (base.IsDisposed)
				{
					throw TraceUtility.ThrowHelperError(base.CreateMessageDisposedException(), this);
				}
				return this.headers.MessageVersion;
			}
		}

		// Token: 0x060061EC RID: 25068 RVA: 0x0016C958 File Offset: 0x0016AB58
		protected override MessageBuffer OnCreateBufferedCopy(int maxBufferSize)
		{
			BodyWriter bodyWriter;
			if (this.bodyWriter.IsBuffered)
			{
				bodyWriter = this.bodyWriter;
			}
			else
			{
				bodyWriter = this.bodyWriter.CreateBufferedCopy(maxBufferSize);
			}
			KeyValuePair<string, object>[] array = new KeyValuePair<string, object>[this.Properties.Count];
			((ICollection<KeyValuePair<string, object>>)this.Properties).CopyTo(array, 0);
			return new BodyWriterMessageBuffer(this.headers, array, bodyWriter);
		}

		// Token: 0x060061ED RID: 25069 RVA: 0x0016C9B4 File Offset: 0x0016ABB4
		protected override void OnClose()
		{
			Exception ex = null;
			try
			{
				base.OnClose();
			}
			catch (Exception ex2)
			{
				if (Fx.IsFatal(ex2))
				{
					throw;
				}
				ex = ex2;
			}
			try
			{
				if (this.properties != null)
				{
					this.properties.Dispose();
				}
			}
			catch (Exception ex3)
			{
				if (Fx.IsFatal(ex3))
				{
					throw;
				}
				if (ex == null)
				{
					ex = ex3;
				}
			}
			if (ex != null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex);
			}
			this.bodyWriter = null;
		}

		// Token: 0x060061EE RID: 25070 RVA: 0x0016CA34 File Offset: 0x0016AC34
		protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
		{
			this.bodyWriter.WriteBodyContents(writer);
		}

		// Token: 0x060061EF RID: 25071 RVA: 0x0016CA42 File Offset: 0x0016AC42
		protected override IAsyncResult OnBeginWriteMessage(XmlDictionaryWriter writer, AsyncCallback callback, object state)
		{
			base.WriteMessagePreamble(writer);
			return new BodyWriterMessage.OnWriteMessageAsyncResult(writer, this, callback, state);
		}

		// Token: 0x060061F0 RID: 25072 RVA: 0x0016CA54 File Offset: 0x0016AC54
		protected override void OnEndWriteMessage(IAsyncResult result)
		{
			BodyWriterMessage.OnWriteMessageAsyncResult.End(result);
		}

		// Token: 0x060061F1 RID: 25073 RVA: 0x0016CA5C File Offset: 0x0016AC5C
		protected override IAsyncResult OnBeginWriteBodyContents(XmlDictionaryWriter writer, AsyncCallback callback, object state)
		{
			return this.bodyWriter.BeginWriteBodyContents(writer, callback, state);
		}

		// Token: 0x060061F2 RID: 25074 RVA: 0x0016CA6C File Offset: 0x0016AC6C
		protected override void OnEndWriteBodyContents(IAsyncResult result)
		{
			this.bodyWriter.EndWriteBodyContents(result);
		}

		// Token: 0x060061F3 RID: 25075 RVA: 0x0016CA7A File Offset: 0x0016AC7A
		protected override void OnBodyToString(XmlDictionaryWriter writer)
		{
			if (this.bodyWriter.IsBuffered)
			{
				this.bodyWriter.WriteBodyContents(writer);
				return;
			}
			writer.WriteString(SR.GetString("MessageBodyIsStream"));
		}

		// Token: 0x17001797 RID: 6039
		// (get) Token: 0x060061F4 RID: 25076 RVA: 0x0016CAA6 File Offset: 0x0016ACA6
		protected internal BodyWriter BodyWriter
		{
			get
			{
				return this.bodyWriter;
			}
		}

		// Token: 0x040038E1 RID: 14561
		private MessageProperties properties;

		// Token: 0x040038E2 RID: 14562
		private MessageHeaders headers;

		// Token: 0x040038E3 RID: 14563
		private BodyWriter bodyWriter;

		// Token: 0x02000E45 RID: 3653
		private class OnWriteMessageAsyncResult : AsyncResult
		{
			// Token: 0x060082C7 RID: 33479 RVA: 0x001E38F4 File Offset: 0x001E1AF4
			public OnWriteMessageAsyncResult(XmlDictionaryWriter writer, BodyWriterMessage message, AsyncCallback callback, object state) : base(callback, state)
			{
				this.message = message;
				this.writer = writer;
				if (this.HandleWriteBodyContents(null))
				{
					base.Complete(true);
				}
			}

			// Token: 0x060082C8 RID: 33480 RVA: 0x001E3920 File Offset: 0x001E1B20
			private bool HandleWriteBodyContents(IAsyncResult result)
			{
				if (result == null)
				{
					result = this.message.OnBeginWriteBodyContents(this.writer, base.PrepareAsyncCompletion(new AsyncResult.AsyncCompletion(this.HandleWriteBodyContents)), this);
					if (!result.CompletedSynchronously)
					{
						return false;
					}
				}
				this.message.OnEndWriteBodyContents(result);
				this.message.WriteMessagePostamble(this.writer);
				return true;
			}

			// Token: 0x060082C9 RID: 33481 RVA: 0x001E397E File Offset: 0x001E1B7E
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<BodyWriterMessage.OnWriteMessageAsyncResult>(result);
			}

			// Token: 0x04004A40 RID: 19008
			private BodyWriterMessage message;

			// Token: 0x04004A41 RID: 19009
			private XmlDictionaryWriter writer;
		}
	}
}
