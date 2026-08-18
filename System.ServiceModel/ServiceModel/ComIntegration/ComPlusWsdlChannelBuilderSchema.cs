using System;
using System.Globalization;
using System.IO;
using System.Runtime.Diagnostics;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000207 RID: 519
	[DataContract(Name = "ComPlusWsdlChannelBuilder")]
	internal class ComPlusWsdlChannelBuilderSchema : TraceRecord
	{
		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06001006 RID: 4102 RVA: 0x000393AD File Offset: 0x000375AD
		internal override string EventId
		{
			get
			{
				return "http://schemas.microsoft.com/2006/08/ServiceModel/ComPlusWsdlChannelBuilderTraceRecord";
			}
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x000393B4 File Offset: 0x000375B4
		internal override void WriteTo(XmlWriter xmlWriter)
		{
			ComPlusTraceRecord.SerializeRecord(xmlWriter, this);
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x000393BD File Offset: 0x000375BD
		public ComPlusWsdlChannelBuilderSchema(XmlQualifiedName bindingQname, XmlQualifiedName contractQname, XmlQualifiedName serviceQname, string importedContract, string importedBinding, XmlSchema schema)
		{
			this.bindingQname = bindingQname;
			this.contractQname = contractQname;
			this.serviceQname = serviceQname;
			this.importedContract = importedContract;
			this.importedBinding = importedBinding;
			this.schema = new ComPlusWsdlChannelBuilderSchema.XmlSchemaWrapper(schema);
		}

		// Token: 0x04001831 RID: 6193
		private const string schemaId = "http://schemas.microsoft.com/2006/08/ServiceModel/ComPlusWsdlChannelBuilderTraceRecord";

		// Token: 0x04001832 RID: 6194
		[DataMember(Name = "BindingQName")]
		private XmlQualifiedName bindingQname;

		// Token: 0x04001833 RID: 6195
		[DataMember(Name = "ContractQName")]
		private XmlQualifiedName contractQname;

		// Token: 0x04001834 RID: 6196
		[DataMember(Name = "ServiceQName")]
		private XmlQualifiedName serviceQname;

		// Token: 0x04001835 RID: 6197
		[DataMember(Name = "ImportedContract")]
		private string importedContract;

		// Token: 0x04001836 RID: 6198
		[DataMember(Name = "ImportedBinding")]
		private string importedBinding;

		// Token: 0x04001837 RID: 6199
		[DataMember(Name = "XmlSchemaSet")]
		private ComPlusWsdlChannelBuilderSchema.XmlSchemaWrapper schema;

		// Token: 0x02000B0C RID: 2828
		private class XmlSchemaWrapper : IXmlSerializable
		{
			// Token: 0x06006F67 RID: 28519 RVA: 0x0019DA8C File Offset: 0x0019BC8C
			public XmlSchemaWrapper(XmlSchema schema)
			{
				this.schema = schema;
			}

			// Token: 0x06006F68 RID: 28520 RVA: 0x0019DA9C File Offset: 0x0019BC9C
			public void WriteXml(XmlWriter xmlWriter)
			{
				StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
				XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
				this.schema.Write(xmlTextWriter);
				xmlTextWriter.Flush();
				UTF8Encoding utf8Encoding = new UTF8Encoding();
				byte[] bytes = utf8Encoding.GetBytes(stringWriter.ToString());
				XmlDictionaryReaderQuotas xmlDictionaryReaderQuotas = new XmlDictionaryReaderQuotas();
				xmlDictionaryReaderQuotas.MaxDepth = 32;
				xmlDictionaryReaderQuotas.MaxStringContentLength = 8192;
				xmlDictionaryReaderQuotas.MaxArrayLength = 16384;
				xmlDictionaryReaderQuotas.MaxBytesPerRead = 4096;
				xmlDictionaryReaderQuotas.MaxNameTableCharCount = 16384;
				XmlDictionaryReader xmlDictionaryReader = XmlDictionaryReader.CreateTextReader(bytes, 0, bytes.GetLength(0), null, xmlDictionaryReaderQuotas, null);
				if (xmlDictionaryReader.MoveToContent() == XmlNodeType.Element && xmlDictionaryReader.Name == "xs:schema")
				{
					xmlWriter.WriteNode(xmlDictionaryReader, false);
				}
				xmlDictionaryReader.Close();
			}

			// Token: 0x06006F69 RID: 28521 RVA: 0x0019DB61 File Offset: 0x0019BD61
			public void ReadXml(XmlReader xmlReader)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
			}

			// Token: 0x06006F6A RID: 28522 RVA: 0x0019DB72 File Offset: 0x0019BD72
			public XmlSchema GetSchema()
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
			}

			// Token: 0x04003F99 RID: 16281
			private XmlSchema schema;
		}
	}
}
