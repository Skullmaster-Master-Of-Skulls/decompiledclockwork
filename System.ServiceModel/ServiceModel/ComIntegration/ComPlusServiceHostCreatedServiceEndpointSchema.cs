using System;
using System.Runtime.Serialization;
using System.Xml;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x020001F8 RID: 504
	[DataContract(Name = "ComPlusServiceHostCreatedServiceEndpoint")]
	internal class ComPlusServiceHostCreatedServiceEndpointSchema : ComPlusServiceHostSchema
	{
		// Token: 0x06000FD8 RID: 4056 RVA: 0x00038E77 File Offset: 0x00037077
		public ComPlusServiceHostCreatedServiceEndpointSchema(Guid appid, Guid clsid, string contract, Uri address, string binding) : base(appid, clsid)
		{
			this.contract = contract;
			this.address = address;
			this.binding = binding;
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000FD9 RID: 4057 RVA: 0x00038E98 File Offset: 0x00037098
		internal override string EventId
		{
			get
			{
				return base.BuildEventId("ComPlusServiceHostCreatedServiceEndpoint");
			}
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x00038EA5 File Offset: 0x000370A5
		internal override void WriteTo(XmlWriter xmlWriter)
		{
			ComPlusTraceRecord.SerializeRecord(xmlWriter, this);
		}

		// Token: 0x040017E6 RID: 6118
		[DataMember(Name = "Contract")]
		private string contract;

		// Token: 0x040017E7 RID: 6119
		[DataMember(Name = "Address")]
		private Uri address;

		// Token: 0x040017E8 RID: 6120
		[DataMember(Name = "Binding")]
		private string binding;
	}
}
