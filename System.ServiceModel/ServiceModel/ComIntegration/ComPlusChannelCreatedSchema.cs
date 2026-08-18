using System;
using System.Runtime.Diagnostics;
using System.Runtime.Serialization;
using System.Xml;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x0200020C RID: 524
	[DataContract(Name = "ComPlusChannelCreated")]
	internal class ComPlusChannelCreatedSchema : TraceRecord
	{
		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06001015 RID: 4117 RVA: 0x000394F3 File Offset: 0x000376F3
		internal override string EventId
		{
			get
			{
				return "http://schemas.microsoft.com/2006/08/ServiceModel/ComPlusChannelCreatedTraceRecord";
			}
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x000394FA File Offset: 0x000376FA
		internal override void WriteTo(XmlWriter xmlWriter)
		{
			ComPlusTraceRecord.SerializeRecord(xmlWriter, this);
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x00039503 File Offset: 0x00037703
		public ComPlusChannelCreatedSchema(Uri address, string contract)
		{
			this.address = address;
			this.contract = contract;
		}

		// Token: 0x04001849 RID: 6217
		private const string schemaId = "http://schemas.microsoft.com/2006/08/ServiceModel/ComPlusChannelCreatedTraceRecord";

		// Token: 0x0400184A RID: 6218
		[DataMember(Name = "Address")]
		private Uri address;

		// Token: 0x0400184B RID: 6219
		[DataMember(Name = "Contract")]
		private string contract;
	}
}
