using System;
using System.Collections.Specialized;
using System.Web.Util;

namespace System.Web.Management
{
	// Token: 0x0200016D RID: 365
	public abstract class BufferedWebEventProvider : WebEventProvider
	{
		// Token: 0x0600145E RID: 5214 RVA: 0x0003C5E0 File Offset: 0x0003A7E0
		public override void Initialize(string name, NameValueCollection config)
		{
			ProviderUtil.GetAndRemoveBooleanAttribute(config, "buffer", name, ref this._buffer);
			if (this._buffer)
			{
				ProviderUtil.GetAndRemoveRequiredNonEmptyStringAttribute(config, "bufferMode", name, ref this._bufferMode);
				this._webEventBuffer = new WebEventBuffer(this, this._bufferMode, new WebEventBufferFlushCallback(this.ProcessEventFlush));
			}
			else
			{
				ProviderUtil.GetAndRemoveStringAttribute(config, "bufferMode", name, ref this._bufferMode);
			}
			base.Initialize(name, config);
			ProviderUtil.CheckUnrecognizedAttributes(config, name);
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x0600145F RID: 5215 RVA: 0x0003C65B File Offset: 0x0003A85B
		public bool UseBuffering
		{
			get
			{
				return this._buffer;
			}
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06001460 RID: 5216 RVA: 0x0003C663 File Offset: 0x0003A863
		public string BufferMode
		{
			get
			{
				return this._bufferMode;
			}
		}

		// Token: 0x06001461 RID: 5217 RVA: 0x0003C66C File Offset: 0x0003A86C
		public override void ProcessEvent(WebBaseEvent eventRaised)
		{
			if (this._buffer)
			{
				this._webEventBuffer.AddEvent(eventRaised);
				return;
			}
			WebEventBufferFlushInfo flushInfo = new WebEventBufferFlushInfo(new WebBaseEventCollection(eventRaised), EventNotificationType.Unbuffered, 0, DateTime.MinValue, 0, 0);
			this.ProcessEventFlush(flushInfo);
		}

		// Token: 0x06001462 RID: 5218
		public abstract void ProcessEventFlush(WebEventBufferFlushInfo flushInfo);

		// Token: 0x06001463 RID: 5219 RVA: 0x0003C6AA File Offset: 0x0003A8AA
		public override void Flush()
		{
			if (this._buffer)
			{
				this._webEventBuffer.Flush(int.MaxValue, FlushCallReason.StaticFlush);
			}
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x0003C6C5 File Offset: 0x0003A8C5
		public override void Shutdown()
		{
			if (this._webEventBuffer != null)
			{
				this._webEventBuffer.Shutdown();
			}
		}

		// Token: 0x04001534 RID: 5428
		private bool _buffer = true;

		// Token: 0x04001535 RID: 5429
		private string _bufferMode;

		// Token: 0x04001536 RID: 5430
		private WebEventBuffer _webEventBuffer;
	}
}
