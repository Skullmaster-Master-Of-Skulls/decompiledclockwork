using System;
using System.Runtime.Diagnostics;
using System.Runtime.Serialization;
using System.ServiceModel.Description;
using System.Xml;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x0200020A RID: 522
	[DataContract(Name = "ComPlusMexBuilderMetadataRetrievedEndpoint")]
	internal class ComPlusMexBuilderMetadataRetrievedEndpoint : TraceRecord
	{
		// Token: 0x170003CD RID: 973
		// (get) Token: 0x0600100F RID: 4111 RVA: 0x0003945A File Offset: 0x0003765A
		internal override string EventId
		{
			get
			{
				return "http://schemas.microsoft.com/2006/08/ServiceModel/ComPlusMexBuilderMetadataRetrievedEndpointTraceRecord";
			}
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x00039461 File Offset: 0x00037661
		internal override void WriteTo(XmlWriter xmlWriter)
		{
			ComPlusTraceRecord.SerializeRecord(xmlWriter, this);
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x0003946C File Offset: 0x0003766C
		public ComPlusMexBuilderMetadataRetrievedEndpoint(ServiceEndpoint endpoint)
		{
			this.binding = endpoint.Binding.Name;
			this.bindingNamespace = endpoint.Binding.Namespace;
			this.address = endpoint.Address.ToString();
			this.contract = endpoint.Contract.Name;
			this.contractNamespace = endpoint.Contract.Namespace;
		}

		// Token: 0x04001841 RID: 6209
		private const string schemaId = "http://schemas.microsoft.com/2006/08/ServiceModel/ComPlusMexBuilderMetadataRetrievedEndpointTraceRecord";

		// Token: 0x04001842 RID: 6210
		[DataMember(Name = "Binding")]
		private string binding;

		// Token: 0x04001843 RID: 6211
		[DataMember(Name = "BindingNamespace")]
		private string bindingNamespace;

		// Token: 0x04001844 RID: 6212
		[DataMember(Name = "Address")]
		private string address;

		// Token: 0x04001845 RID: 6213
		[DataMember(Name = "Contract")]
		private string contract;

		// Token: 0x04001846 RID: 6214
		[DataMember(Name = "ContractNamespace")]
		private string contractNamespace;
	}
}
