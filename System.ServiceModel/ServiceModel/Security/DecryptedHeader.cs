using System;
using System.ServiceModel.Channels;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x0200029C RID: 668
	internal sealed class DecryptedHeader : ReadableMessageHeader
	{
		// Token: 0x06001436 RID: 5174 RVA: 0x0004C1F0 File Offset: 0x0004A3F0
		public DecryptedHeader(byte[] decryptedBuffer, XmlAttributeHolder[] envelopeAttributes, XmlAttributeHolder[] headerAttributes, MessageVersion version, SignatureTargetIdManager idManager, XmlDictionaryReaderQuotas quotas)
		{
			if (quotas == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("quotas");
			}
			this.decryptedBuffer = decryptedBuffer;
			this.version = version;
			this.envelopeAttributes = envelopeAttributes;
			this.headerAttributes = headerAttributes;
			this.quotas = quotas;
			XmlDictionaryReader xmlDictionaryReader = this.CreateReader();
			xmlDictionaryReader.MoveToStartElement();
			this.name = xmlDictionaryReader.LocalName;
			this.namespaceUri = xmlDictionaryReader.NamespaceURI;
			MessageHeader.GetHeaderAttributes(xmlDictionaryReader, version, out this.actor, out this.mustUnderstand, out this.relay, out this.isRefParam);
			this.id = idManager.ExtractId(xmlDictionaryReader);
			this.cachedReader = xmlDictionaryReader;
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06001437 RID: 5175 RVA: 0x0004C296 File Offset: 0x0004A496
		public override string Actor
		{
			get
			{
				return this.actor;
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06001438 RID: 5176 RVA: 0x0004C29E File Offset: 0x0004A49E
		public string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06001439 RID: 5177 RVA: 0x0004C2A6 File Offset: 0x0004A4A6
		public override bool IsReferenceParameter
		{
			get
			{
				return this.isRefParam;
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x0600143A RID: 5178 RVA: 0x0004C2AE File Offset: 0x0004A4AE
		public override bool MustUnderstand
		{
			get
			{
				return this.mustUnderstand;
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x0600143B RID: 5179 RVA: 0x0004C2B6 File Offset: 0x0004A4B6
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x0600143C RID: 5180 RVA: 0x0004C2BE File Offset: 0x0004A4BE
		public override string Namespace
		{
			get
			{
				return this.namespaceUri;
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x0600143D RID: 5181 RVA: 0x0004C2C6 File Offset: 0x0004A4C6
		public override bool Relay
		{
			get
			{
				return this.relay;
			}
		}

		// Token: 0x0600143E RID: 5182 RVA: 0x0004C2CE File Offset: 0x0004A4CE
		private XmlDictionaryReader CreateReader()
		{
			return ContextImportHelper.CreateSplicedReader(this.decryptedBuffer, this.envelopeAttributes, this.headerAttributes, null, this.quotas);
		}

		// Token: 0x0600143F RID: 5183 RVA: 0x0004C2F0 File Offset: 0x0004A4F0
		public override XmlDictionaryReader GetHeaderReader()
		{
			if (this.cachedReader != null)
			{
				XmlDictionaryReader result = this.cachedReader;
				this.cachedReader = null;
				return result;
			}
			XmlDictionaryReader xmlDictionaryReader = this.CreateReader();
			xmlDictionaryReader.MoveToContent();
			return xmlDictionaryReader;
		}

		// Token: 0x06001440 RID: 5184 RVA: 0x0004C324 File Offset: 0x0004A524
		public override bool IsMessageVersionSupported(MessageVersion messageVersion)
		{
			return this.version.Equals(messageVersion);
		}

		// Token: 0x04001AA3 RID: 6819
		private XmlDictionaryReader cachedReader;

		// Token: 0x04001AA4 RID: 6820
		private readonly byte[] decryptedBuffer;

		// Token: 0x04001AA5 RID: 6821
		private readonly string id;

		// Token: 0x04001AA6 RID: 6822
		private readonly string name;

		// Token: 0x04001AA7 RID: 6823
		private readonly string namespaceUri;

		// Token: 0x04001AA8 RID: 6824
		private readonly string actor;

		// Token: 0x04001AA9 RID: 6825
		private readonly bool mustUnderstand;

		// Token: 0x04001AAA RID: 6826
		private readonly bool relay;

		// Token: 0x04001AAB RID: 6827
		private readonly bool isRefParam;

		// Token: 0x04001AAC RID: 6828
		private readonly MessageVersion version;

		// Token: 0x04001AAD RID: 6829
		private readonly XmlAttributeHolder[] envelopeAttributes;

		// Token: 0x04001AAE RID: 6830
		private readonly XmlAttributeHolder[] headerAttributes;

		// Token: 0x04001AAF RID: 6831
		private readonly XmlDictionaryReaderQuotas quotas;
	}
}
