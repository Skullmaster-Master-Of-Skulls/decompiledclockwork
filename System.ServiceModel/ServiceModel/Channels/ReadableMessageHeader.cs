using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009CF RID: 2511
	internal abstract class ReadableMessageHeader : MessageHeader
	{
		// Token: 0x060062BE RID: 25278
		public abstract XmlDictionaryReader GetHeaderReader();

		// Token: 0x060062BF RID: 25279 RVA: 0x0016FAA8 File Offset: 0x0016DCA8
		protected override void OnWriteStartHeader(XmlDictionaryWriter writer, MessageVersion messageVersion)
		{
			if (!this.IsMessageVersionSupported(messageVersion))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("MessageHeaderVersionNotSupported", new object[]
				{
					base.GetType().FullName,
					messageVersion.ToString()
				}), "version"));
			}
			XmlDictionaryReader headerReader = this.GetHeaderReader();
			writer.WriteStartElement(headerReader.Prefix, headerReader.LocalName, headerReader.NamespaceURI);
			writer.WriteAttributes(headerReader, false);
			headerReader.Close();
		}

		// Token: 0x060062C0 RID: 25280 RVA: 0x0016FB28 File Offset: 0x0016DD28
		protected override void OnWriteHeaderContents(XmlDictionaryWriter writer, MessageVersion messageVersion)
		{
			XmlDictionaryReader headerReader = this.GetHeaderReader();
			headerReader.ReadStartElement();
			while (headerReader.NodeType != XmlNodeType.EndElement)
			{
				writer.WriteNode(headerReader, false);
			}
			headerReader.ReadEndElement();
			headerReader.Close();
		}
	}
}
