using System;
using System.Xml;
using System.Xml.Serialization;

namespace System.ServiceModel.Description
{
	// Token: 0x020003ED RID: 1005
	internal sealed class MetadataSetSerializer : XmlSerializer1
	{
		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x060025D3 RID: 9683 RVA: 0x000891C2 File Offset: 0x000873C2
		// (set) Token: 0x060025D4 RID: 9684 RVA: 0x000891CA File Offset: 0x000873CA
		public bool ProcessOuterElement
		{
			get
			{
				return this.processOuterElement;
			}
			set
			{
				this.processOuterElement = value;
			}
		}

		// Token: 0x060025D5 RID: 9685 RVA: 0x000891D3 File Offset: 0x000873D3
		public override bool CanDeserialize(XmlReader xmlReader)
		{
			return xmlReader.IsStartElement("Metadata", "http://schemas.xmlsoap.org/ws/2004/09/mex");
		}

		// Token: 0x060025D6 RID: 9686 RVA: 0x000891E5 File Offset: 0x000873E5
		protected override void Serialize(object objectToSerialize, XmlSerializationWriter writer)
		{
			((XmlSerializationWriterMetadataSet)writer).ProcessOuterElement = this.processOuterElement;
			((XmlSerializationWriterMetadataSet)writer).Write68_Metadata(objectToSerialize);
		}

		// Token: 0x060025D7 RID: 9687 RVA: 0x00089204 File Offset: 0x00087404
		protected override object Deserialize(XmlSerializationReader reader)
		{
			((XmlSerializationReaderMetadataSet)reader).ProcessOuterElement = this.processOuterElement;
			return ((XmlSerializationReaderMetadataSet)reader).Read68_Metadata();
		}

		// Token: 0x04002166 RID: 8550
		private bool processOuterElement = true;
	}
}
