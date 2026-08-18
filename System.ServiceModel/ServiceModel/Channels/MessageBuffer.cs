using System;
using System.IO;
using System.ServiceModel.Dispatcher;
using System.Xml;
using System.Xml.XPath;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009C5 RID: 2501
	[__DynamicallyInvokable]
	public abstract class MessageBuffer : IXPathNavigable, IDisposable
	{
		// Token: 0x170017AB RID: 6059
		// (get) Token: 0x0600623E RID: 25150
		[__DynamicallyInvokable]
		public abstract int BufferSize { [__DynamicallyInvokable] get; }

		// Token: 0x0600623F RID: 25151 RVA: 0x0016DB8A File Offset: 0x0016BD8A
		[__DynamicallyInvokable]
		void IDisposable.Dispose()
		{
			this.Close();
		}

		// Token: 0x06006240 RID: 25152
		[__DynamicallyInvokable]
		public abstract void Close();

		// Token: 0x06006241 RID: 25153 RVA: 0x0016DB94 File Offset: 0x0016BD94
		[__DynamicallyInvokable]
		public virtual void WriteMessage(Stream stream)
		{
			if (stream == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("stream"));
			}
			Message message = this.CreateMessage();
			using (message)
			{
				XmlDictionaryWriter xmlDictionaryWriter = XmlDictionaryWriter.CreateBinaryWriter(stream, XD.Dictionary, null, false);
				using (xmlDictionaryWriter)
				{
					message.WriteMessage(xmlDictionaryWriter);
				}
			}
		}

		// Token: 0x170017AC RID: 6060
		// (get) Token: 0x06006242 RID: 25154 RVA: 0x0016DC0C File Offset: 0x0016BE0C
		[__DynamicallyInvokable]
		public virtual string MessageContentType
		{
			[__DynamicallyInvokable]
			get
			{
				return "application/soap+msbin1";
			}
		}

		// Token: 0x06006243 RID: 25155
		[__DynamicallyInvokable]
		public abstract Message CreateMessage();

		// Token: 0x06006244 RID: 25156 RVA: 0x0016DC13 File Offset: 0x0016BE13
		internal Exception CreateBufferDisposedException()
		{
			return new ObjectDisposedException("", SR.GetString("MessageBufferIsClosed"));
		}

		// Token: 0x06006245 RID: 25157 RVA: 0x0016DC29 File Offset: 0x0016BE29
		public XPathNavigator CreateNavigator()
		{
			return this.CreateNavigator(int.MaxValue, XmlSpace.None);
		}

		// Token: 0x06006246 RID: 25158 RVA: 0x0016DC37 File Offset: 0x0016BE37
		public XPathNavigator CreateNavigator(int nodeQuota)
		{
			return this.CreateNavigator(nodeQuota, XmlSpace.None);
		}

		// Token: 0x06006247 RID: 25159 RVA: 0x0016DC41 File Offset: 0x0016BE41
		public XPathNavigator CreateNavigator(XmlSpace space)
		{
			return this.CreateNavigator(int.MaxValue, space);
		}

		// Token: 0x06006248 RID: 25160 RVA: 0x0016DC4F File Offset: 0x0016BE4F
		public XPathNavigator CreateNavigator(int nodeQuota, XmlSpace space)
		{
			if (nodeQuota <= 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("nodeQuota", SR.GetString("FilterQuotaRange")));
			}
			return new SeekableMessageNavigator(this.CreateMessage(), nodeQuota, space, true, true);
		}

		// Token: 0x06006249 RID: 25161 RVA: 0x0016DC83 File Offset: 0x0016BE83
		[__DynamicallyInvokable]
		protected MessageBuffer()
		{
		}
	}
}
