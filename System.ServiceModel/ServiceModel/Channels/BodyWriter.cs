using System;
using System.Runtime;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009B2 RID: 2482
	[__DynamicallyInvokable]
	public abstract class BodyWriter
	{
		// Token: 0x06006168 RID: 24936 RVA: 0x0016B31F File Offset: 0x0016951F
		[__DynamicallyInvokable]
		protected BodyWriter(bool isBuffered)
		{
			this.isBuffered = isBuffered;
			this.canWrite = true;
			if (!this.isBuffered)
			{
				this.thisLock = new object();
			}
		}

		// Token: 0x1700177C RID: 6012
		// (get) Token: 0x06006169 RID: 24937 RVA: 0x0016B348 File Offset: 0x00169548
		[__DynamicallyInvokable]
		public bool IsBuffered
		{
			[__DynamicallyInvokable]
			get
			{
				return this.isBuffered;
			}
		}

		// Token: 0x1700177D RID: 6013
		// (get) Token: 0x0600616A RID: 24938 RVA: 0x0016B350 File Offset: 0x00169550
		internal virtual bool IsEmpty
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700177E RID: 6014
		// (get) Token: 0x0600616B RID: 24939 RVA: 0x0016B353 File Offset: 0x00169553
		internal virtual bool IsFault
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600616C RID: 24940 RVA: 0x0016B358 File Offset: 0x00169558
		[__DynamicallyInvokable]
		public BodyWriter CreateBufferedCopy(int maxBufferSize)
		{
			if (maxBufferSize < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("maxBufferSize", maxBufferSize, SR.GetString("ValueMustBeNonNegative")));
			}
			if (this.isBuffered)
			{
				return this;
			}
			object obj = this.thisLock;
			lock (obj)
			{
				if (!this.canWrite)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("BodyWriterCanOnlyBeWrittenOnce")));
				}
				this.canWrite = false;
			}
			BodyWriter bodyWriter = this.OnCreateBufferedCopy(maxBufferSize);
			if (!bodyWriter.IsBuffered)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("BodyWriterReturnedIsNotBuffered")));
			}
			return bodyWriter;
		}

		// Token: 0x0600616D RID: 24941 RVA: 0x0016B41C File Offset: 0x0016961C
		[__DynamicallyInvokable]
		protected virtual BodyWriter OnCreateBufferedCopy(int maxBufferSize)
		{
			return this.OnCreateBufferedCopy(maxBufferSize, XmlDictionaryReaderQuotas.Max);
		}

		// Token: 0x0600616E RID: 24942 RVA: 0x0016B42C File Offset: 0x0016962C
		internal BodyWriter OnCreateBufferedCopy(int maxBufferSize, XmlDictionaryReaderQuotas quotas)
		{
			XmlBuffer xmlBuffer = new XmlBuffer(maxBufferSize);
			using (XmlDictionaryWriter xmlDictionaryWriter = xmlBuffer.OpenSection(quotas))
			{
				xmlDictionaryWriter.WriteStartElement("a");
				this.OnWriteBodyContents(xmlDictionaryWriter);
				xmlDictionaryWriter.WriteEndElement();
			}
			xmlBuffer.CloseSection();
			xmlBuffer.Close();
			return new BodyWriter.BufferedBodyWriter(xmlBuffer);
		}

		// Token: 0x0600616F RID: 24943
		[__DynamicallyInvokable]
		protected abstract void OnWriteBodyContents(XmlDictionaryWriter writer);

		// Token: 0x06006170 RID: 24944 RVA: 0x0016B490 File Offset: 0x00169690
		protected virtual IAsyncResult OnBeginWriteBodyContents(XmlDictionaryWriter writer, AsyncCallback callback, object state)
		{
			return new BodyWriter.OnWriteBodyContentsAsyncResult(writer, this, callback, state);
		}

		// Token: 0x06006171 RID: 24945 RVA: 0x0016B49B File Offset: 0x0016969B
		protected virtual void OnEndWriteBodyContents(IAsyncResult result)
		{
			ScheduleActionItemAsyncResult.End(result);
		}

		// Token: 0x06006172 RID: 24946 RVA: 0x0016B4A4 File Offset: 0x001696A4
		private void EnsureWriteBodyContentsState(XmlDictionaryWriter writer)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("writer"));
			}
			if (!this.isBuffered)
			{
				object obj = this.thisLock;
				lock (obj)
				{
					if (!this.canWrite)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("BodyWriterCanOnlyBeWrittenOnce")));
					}
					this.canWrite = false;
				}
			}
		}

		// Token: 0x06006173 RID: 24947 RVA: 0x0016B528 File Offset: 0x00169728
		[__DynamicallyInvokable]
		public void WriteBodyContents(XmlDictionaryWriter writer)
		{
			this.EnsureWriteBodyContentsState(writer);
			this.OnWriteBodyContents(writer);
		}

		// Token: 0x06006174 RID: 24948 RVA: 0x0016B538 File Offset: 0x00169738
		public IAsyncResult BeginWriteBodyContents(XmlDictionaryWriter writer, AsyncCallback callback, object state)
		{
			this.EnsureWriteBodyContentsState(writer);
			return this.OnBeginWriteBodyContents(writer, callback, state);
		}

		// Token: 0x06006175 RID: 24949 RVA: 0x0016B54A File Offset: 0x0016974A
		public void EndWriteBodyContents(IAsyncResult result)
		{
			this.OnEndWriteBodyContents(result);
		}

		// Token: 0x040038D4 RID: 14548
		private bool isBuffered;

		// Token: 0x040038D5 RID: 14549
		private bool canWrite;

		// Token: 0x040038D6 RID: 14550
		private object thisLock;

		// Token: 0x02000E40 RID: 3648
		private class BufferedBodyWriter : BodyWriter
		{
			// Token: 0x060082BC RID: 33468 RVA: 0x001E3429 File Offset: 0x001E1629
			public BufferedBodyWriter(XmlBuffer buffer) : base(true)
			{
				this.buffer = buffer;
			}

			// Token: 0x060082BD RID: 33469 RVA: 0x001E343C File Offset: 0x001E163C
			protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
			{
				XmlDictionaryReader reader = this.buffer.GetReader(0);
				using (reader)
				{
					reader.ReadStartElement();
					while (reader.NodeType != XmlNodeType.EndElement)
					{
						writer.WriteNode(reader, false);
					}
					reader.ReadEndElement();
				}
			}

			// Token: 0x04004A38 RID: 19000
			private XmlBuffer buffer;
		}

		// Token: 0x02000E41 RID: 3649
		private class OnWriteBodyContentsAsyncResult : ScheduleActionItemAsyncResult
		{
			// Token: 0x060082BE RID: 33470 RVA: 0x001E3494 File Offset: 0x001E1694
			public OnWriteBodyContentsAsyncResult(XmlDictionaryWriter writer, BodyWriter bodyWriter, AsyncCallback callback, object state) : base(callback, state)
			{
				this.writer = writer;
				this.bodyWriter = bodyWriter;
				base.Schedule();
			}

			// Token: 0x060082BF RID: 33471 RVA: 0x001E34B3 File Offset: 0x001E16B3
			protected override void OnDoWork()
			{
				this.bodyWriter.OnWriteBodyContents(this.writer);
			}

			// Token: 0x04004A39 RID: 19001
			private BodyWriter bodyWriter;

			// Token: 0x04004A3A RID: 19002
			private XmlDictionaryWriter writer;
		}
	}
}
