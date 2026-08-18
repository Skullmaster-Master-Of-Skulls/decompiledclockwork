using System;
using System.Runtime.Serialization;
using System.Xml;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x020001F6 RID: 502
	[DataContract(Name = "ComPlusServiceHostCreatedServiceContract")]
	internal class ComPlusServiceHostCreatedServiceContractSchema : ComPlusServiceHostSchema
	{
		// Token: 0x06000FD2 RID: 4050 RVA: 0x00038E1C File Offset: 0x0003701C
		public ComPlusServiceHostCreatedServiceContractSchema(Guid appid, Guid clsid, XmlQualifiedName contractQname, string contract) : base(appid, clsid)
		{
			this.contractQname = contractQname;
			this.contract = contract;
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000FD3 RID: 4051 RVA: 0x00038E35 File Offset: 0x00037035
		internal override string EventId
		{
			get
			{
				return base.BuildEventId("ComPlusServiceHostCreatedServiceContract");
			}
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x00038E42 File Offset: 0x00037042
		internal override void WriteTo(XmlWriter xmlWriter)
		{
			ComPlusTraceRecord.SerializeRecord(xmlWriter, this);
		}

		// Token: 0x040017E3 RID: 6115
		[DataMember(Name = "ContractQName")]
		private XmlQualifiedName contractQname;

		// Token: 0x040017E4 RID: 6116
		[DataMember(Name = "Contract")]
		private string contract;
	}
}
