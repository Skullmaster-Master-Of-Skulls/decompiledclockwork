using System;
using System.Runtime.Diagnostics;
using System.Runtime.Serialization;
using System.Xml;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x0200020B RID: 523
	[DataContract(Name = "ComPlusMexBuilderMetadataRetrieved")]
	internal class ComPlusMexBuilderMetadataRetrievedSchema : TraceRecord
	{
		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06001012 RID: 4114 RVA: 0x000394D4 File Offset: 0x000376D4
		internal override string EventId
		{
			get
			{
				return "http://schemas.microsoft.com/2006/08/ServiceModel/ComPlusMexBuilderMetadataRetrievedTraceRecord";
			}
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x000394DB File Offset: 0x000376DB
		internal override void WriteTo(XmlWriter xmlWriter)
		{
			ComPlusTraceRecord.SerializeRecord(xmlWriter, this);
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x000394E4 File Offset: 0x000376E4
		public ComPlusMexBuilderMetadataRetrievedSchema(ComPlusMexBuilderMetadataRetrievedEndpoint[] endpoints)
		{
			this.endpoints = endpoints;
		}

		// Token: 0x04001847 RID: 6215
		private const string schemaId = "http://schemas.microsoft.com/2006/08/ServiceModel/ComPlusMexBuilderMetadataRetrievedTraceRecord";

		// Token: 0x04001848 RID: 6216
		[DataMember(Name = "bindingNamespaces")]
		private ComPlusMexBuilderMetadataRetrievedEndpoint[] endpoints;
	}
}
