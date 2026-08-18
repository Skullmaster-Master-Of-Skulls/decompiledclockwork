using System;
using System.Runtime.Diagnostics;
using System.Runtime.Serialization;
using System.Xml;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000208 RID: 520
	[DataContract(Name = "ComPlusTypedChannelBuilder")]
	internal class ComPlusTypedChannelBuilderSchema : TraceRecord
	{
		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06001009 RID: 4105 RVA: 0x000393F7 File Offset: 0x000375F7
		internal override string EventId
		{
			get
			{
				return "http://schemas.microsoft.com/2006/08/ServiceModel/ComPlusTypedChannelBuilderTraceRecord";
			}
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x000393FE File Offset: 0x000375FE
		internal override void WriteTo(XmlWriter xmlWriter)
		{
			ComPlusTraceRecord.SerializeRecord(xmlWriter, this);
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x00039407 File Offset: 0x00037607
		public ComPlusTypedChannelBuilderSchema(string contract, string binding)
		{
			this.contract = contract;
			this.binding = binding;
		}

		// Token: 0x04001838 RID: 6200
		private const string schemaId = "http://schemas.microsoft.com/2006/08/ServiceModel/ComPlusTypedChannelBuilderTraceRecord";

		// Token: 0x04001839 RID: 6201
		[DataMember(Name = "Contract")]
		private string contract;

		// Token: 0x0400183A RID: 6202
		[DataMember(Name = "Binding")]
		private string binding;
	}
}
