using System;
using System.Runtime;
using System.ServiceModel.Diagnostics;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009BE RID: 2494
	internal sealed class StreamedMessage : ReceivedMessage
	{
		// Token: 0x060061FD RID: 25085 RVA: 0x0016CC74 File Offset: 0x0016AE74
		public StreamedMessage(XmlDictionaryReader reader, int maxSizeOfHeaders, MessageVersion desiredVersion)
		{
			this.properties = new MessageProperties();
			if (reader.NodeType != XmlNodeType.Element)
			{
				reader.MoveToContent();
			}
			if (desiredVersion.Envelope == EnvelopeVersion.None)
			{
				this.reader = reader;
				this.headerAttributes = XmlAttributeHolder.emptyArray;
				this.headers = new MessageHeaders(desiredVersion);
				return;
			}
			this.envelopeAttributes = XmlAttributeHolder.ReadAttributes(reader, ref maxSizeOfHeaders);
			this.envelopePrefix = reader.Prefix;
			EnvelopeVersion envelopeVersion = ReceivedMessage.ReadStartEnvelope(reader);
			if (desiredVersion.Envelope != envelopeVersion)
			{
				Exception ex = new ArgumentException(SR.GetString("EncoderEnvelopeVersionMismatch", new object[]
				{
					envelopeVersion,
					desiredVersion.Envelope
				}), "reader");
				throw TraceUtility.ThrowHelperError(new CommunicationException(ex.Message, ex), this);
			}
			if (ReceivedMessage.HasHeaderElement(reader, envelopeVersion))
			{
				this.headerPrefix = reader.Prefix;
				this.headerAttributes = XmlAttributeHolder.ReadAttributes(reader, ref maxSizeOfHeaders);
				this.headers = new MessageHeaders(desiredVersion, reader, this.envelopeAttributes, this.headerAttributes, ref maxSizeOfHeaders);
			}
			else
			{
				this.headerAttributes = XmlAttributeHolder.emptyArray;
				this.headers = new MessageHeaders(desiredVersion);
			}
			if (reader.NodeType != XmlNodeType.Element)
			{
				reader.MoveToContent();
			}
			this.bodyPrefix = reader.Prefix;
			ReceivedMessage.VerifyStartBody(reader, envelopeVersion);
			this.bodyAttributes = XmlAttributeHolder.ReadAttributes(reader, ref maxSizeOfHeaders);
			if (base.ReadStartBody(reader))
			{
				this.reader = reader;
				return;
			}
			this.quotas = new XmlDictionaryReaderQuotas();
			reader.Quotas.CopyTo(this.quotas);
			reader.Close();
		}

		// Token: 0x1700179A RID: 6042
		// (get) Token: 0x060061FE RID: 25086 RVA: 0x0016CDEE File Offset: 0x0016AFEE
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

		// Token: 0x1700179B RID: 6043
		// (get) Token: 0x060061FF RID: 25087 RVA: 0x0016CE0B File Offset: 0x0016B00B
		public override MessageVersion Version
		{
			get
			{
				return this.headers.MessageVersion;
			}
		}

		// Token: 0x1700179C RID: 6044
		// (get) Token: 0x06006200 RID: 25088 RVA: 0x0016CE18 File Offset: 0x0016B018
		public override MessageProperties Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x06006201 RID: 25089 RVA: 0x0016CE20 File Offset: 0x0016B020
		protected override void OnBodyToString(XmlDictionaryWriter writer)
		{
			writer.WriteString(SR.GetString("MessageBodyIsStream"));
		}

		// Token: 0x06006202 RID: 25090 RVA: 0x0016CE34 File Offset: 0x0016B034
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
				this.properties.Dispose();
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
			try
			{
				if (this.reader != null)
				{
					this.reader.Close();
				}
			}
			catch (Exception ex4)
			{
				if (Fx.IsFatal(ex4))
				{
					throw;
				}
				if (ex == null)
				{
					ex = ex4;
				}
			}
			if (ex != null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex);
			}
		}

		// Token: 0x06006203 RID: 25091 RVA: 0x0016CEDC File Offset: 0x0016B0DC
		protected override XmlDictionaryReader OnGetReaderAtBodyContents()
		{
			XmlDictionaryReader result = this.reader;
			this.reader = null;
			return result;
		}

		// Token: 0x06006204 RID: 25092 RVA: 0x0016CEF8 File Offset: 0x0016B0F8
		protected override MessageBuffer OnCreateBufferedCopy(int maxBufferSize)
		{
			if (this.reader != null)
			{
				return base.OnCreateBufferedCopy(maxBufferSize, this.reader.Quotas);
			}
			return base.OnCreateBufferedCopy(maxBufferSize, this.quotas);
		}

		// Token: 0x06006205 RID: 25093 RVA: 0x0016CF22 File Offset: 0x0016B122
		protected override void OnWriteStartBody(XmlDictionaryWriter writer)
		{
			writer.WriteStartElement(this.bodyPrefix, "Body", this.Version.Envelope.Namespace);
			XmlAttributeHolder.WriteAttributes(this.bodyAttributes, writer);
		}

		// Token: 0x06006206 RID: 25094 RVA: 0x0016CF54 File Offset: 0x0016B154
		protected override void OnWriteStartEnvelope(XmlDictionaryWriter writer)
		{
			EnvelopeVersion envelope = this.Version.Envelope;
			writer.WriteStartElement(this.envelopePrefix, "Envelope", envelope.Namespace);
			XmlAttributeHolder.WriteAttributes(this.envelopeAttributes, writer);
		}

		// Token: 0x06006207 RID: 25095 RVA: 0x0016CF90 File Offset: 0x0016B190
		protected override void OnWriteStartHeaders(XmlDictionaryWriter writer)
		{
			EnvelopeVersion envelope = this.Version.Envelope;
			writer.WriteStartElement(this.headerPrefix, "Header", envelope.Namespace);
			XmlAttributeHolder.WriteAttributes(this.headerAttributes, writer);
		}

		// Token: 0x06006208 RID: 25096 RVA: 0x0016CFCC File Offset: 0x0016B1CC
		protected override string OnGetBodyAttribute(string localName, string ns)
		{
			return XmlAttributeHolder.GetAttribute(this.bodyAttributes, localName, ns);
		}

		// Token: 0x040038E6 RID: 14566
		private MessageHeaders headers;

		// Token: 0x040038E7 RID: 14567
		private XmlAttributeHolder[] envelopeAttributes;

		// Token: 0x040038E8 RID: 14568
		private XmlAttributeHolder[] headerAttributes;

		// Token: 0x040038E9 RID: 14569
		private XmlAttributeHolder[] bodyAttributes;

		// Token: 0x040038EA RID: 14570
		private string envelopePrefix;

		// Token: 0x040038EB RID: 14571
		private string headerPrefix;

		// Token: 0x040038EC RID: 14572
		private string bodyPrefix;

		// Token: 0x040038ED RID: 14573
		private MessageProperties properties;

		// Token: 0x040038EE RID: 14574
		private XmlDictionaryReader reader;

		// Token: 0x040038EF RID: 14575
		private XmlDictionaryReaderQuotas quotas;
	}
}
