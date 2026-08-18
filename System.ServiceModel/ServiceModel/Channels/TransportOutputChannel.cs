using System;
using System.Runtime.Diagnostics;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007A1 RID: 1953
	internal abstract class TransportOutputChannel : OutputChannel
	{
		// Token: 0x060049DE RID: 18910 RVA: 0x0010F3A8 File Offset: 0x0010D5A8
		protected TransportOutputChannel(ChannelManagerBase channelManager, EndpointAddress to, Uri via, bool manualAddressing, MessageVersion messageVersion) : base(channelManager)
		{
			this.manualAddressing = manualAddressing;
			this.messageVersion = messageVersion;
			this.to = to;
			this.via = via;
			if (!manualAddressing && to != null)
			{
				Uri uri;
				if (to.IsAnonymous)
				{
					uri = this.messageVersion.Addressing.AnonymousUri;
				}
				else if (to.IsNone)
				{
					uri = this.messageVersion.Addressing.NoneUri;
				}
				else
				{
					uri = to.Uri;
				}
				if (uri != null)
				{
					XmlDictionaryString dictionaryTo = new TransportOutputChannel.ToDictionary(uri.AbsoluteUri).To;
					this.toHeader = ToHeader.Create(uri, dictionaryTo, messageVersion.Addressing);
				}
				this.anyHeadersToAdd = (to.Headers.Count > 0);
			}
			if (FxTrace.Trace.IsEnd2EndActivityTracingEnabled)
			{
				this.channelEventTraceActivity = EventTraceActivity.GetFromThreadOrCreate(false);
			}
		}

		// Token: 0x170012A1 RID: 4769
		// (get) Token: 0x060049DF RID: 18911 RVA: 0x0010F481 File Offset: 0x0010D681
		protected bool ManualAddressing
		{
			get
			{
				return this.manualAddressing;
			}
		}

		// Token: 0x170012A2 RID: 4770
		// (get) Token: 0x060049E0 RID: 18912 RVA: 0x0010F489 File Offset: 0x0010D689
		public MessageVersion MessageVersion
		{
			get
			{
				return this.messageVersion;
			}
		}

		// Token: 0x170012A3 RID: 4771
		// (get) Token: 0x060049E1 RID: 18913 RVA: 0x0010F491 File Offset: 0x0010D691
		public override EndpointAddress RemoteAddress
		{
			get
			{
				return this.to;
			}
		}

		// Token: 0x170012A4 RID: 4772
		// (get) Token: 0x060049E2 RID: 18914 RVA: 0x0010F499 File Offset: 0x0010D699
		public override Uri Via
		{
			get
			{
				return this.via;
			}
		}

		// Token: 0x170012A5 RID: 4773
		// (get) Token: 0x060049E3 RID: 18915 RVA: 0x0010F4A1 File Offset: 0x0010D6A1
		public EventTraceActivity EventTraceActivity
		{
			get
			{
				return this.channelEventTraceActivity;
			}
		}

		// Token: 0x060049E4 RID: 18916 RVA: 0x0010F4A9 File Offset: 0x0010D6A9
		protected override void AddHeadersTo(Message message)
		{
			base.AddHeadersTo(message);
			if (this.toHeader != null)
			{
				message.Headers.SetToHeader(this.toHeader);
				if (this.anyHeadersToAdd)
				{
					this.to.Headers.AddHeadersTo(message);
				}
			}
		}

		// Token: 0x04002ED8 RID: 11992
		private bool anyHeadersToAdd;

		// Token: 0x04002ED9 RID: 11993
		private bool manualAddressing;

		// Token: 0x04002EDA RID: 11994
		private MessageVersion messageVersion;

		// Token: 0x04002EDB RID: 11995
		private EndpointAddress to;

		// Token: 0x04002EDC RID: 11996
		private Uri via;

		// Token: 0x04002EDD RID: 11997
		private ToHeader toHeader;

		// Token: 0x04002EDE RID: 11998
		private EventTraceActivity channelEventTraceActivity;

		// Token: 0x02000CF2 RID: 3314
		private class ToDictionary : IXmlDictionary
		{
			// Token: 0x06007A78 RID: 31352 RVA: 0x001C8285 File Offset: 0x001C6485
			public ToDictionary(string to)
			{
				this.to = new XmlDictionaryString(this, to, 0);
			}

			// Token: 0x17001BB7 RID: 7095
			// (get) Token: 0x06007A79 RID: 31353 RVA: 0x001C829B File Offset: 0x001C649B
			public XmlDictionaryString To
			{
				get
				{
					return this.to;
				}
			}

			// Token: 0x06007A7A RID: 31354 RVA: 0x001C82A3 File Offset: 0x001C64A3
			public bool TryLookup(string value, out XmlDictionaryString result)
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				if (value == this.to.Value)
				{
					result = this.to;
					return true;
				}
				result = null;
				return false;
			}

			// Token: 0x06007A7B RID: 31355 RVA: 0x001C82D9 File Offset: 0x001C64D9
			public bool TryLookup(int key, out XmlDictionaryString result)
			{
				if (key == 0)
				{
					result = this.to;
					return true;
				}
				result = null;
				return false;
			}

			// Token: 0x06007A7C RID: 31356 RVA: 0x001C82EC File Offset: 0x001C64EC
			public bool TryLookup(XmlDictionaryString value, out XmlDictionaryString result)
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				if (value == this.to)
				{
					result = this.to;
					return true;
				}
				result = null;
				return false;
			}

			// Token: 0x04004609 RID: 17929
			private XmlDictionaryString to;
		}
	}
}
