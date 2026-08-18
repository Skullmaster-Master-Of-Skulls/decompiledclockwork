using System;
using System.Runtime.Diagnostics;
using System.Runtime.Serialization;
using System.Xml;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x0200020E RID: 526
	[DataContract(Name = "ComPlusTxProxySchema")]
	internal class ComPlusTxProxySchema : TraceRecord
	{
		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x0600101B RID: 4123 RVA: 0x00039546 File Offset: 0x00037746
		internal override string EventId
		{
			get
			{
				return "http://schemas.microsoft.com/2006/08/ServiceModel/ComPlusTxProxyTxTraceRecord";
			}
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x0003954D File Offset: 0x0003774D
		internal override void WriteTo(XmlWriter xmlWriter)
		{
			ComPlusTraceRecord.SerializeRecord(xmlWriter, this);
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x00039556 File Offset: 0x00037756
		public ComPlusTxProxySchema(Guid appid, Guid clsid, Guid transactionID, int instanceID)
		{
			this.appid = appid;
			this.clsid = clsid;
			this.transactionID = transactionID;
			this.instanceID = instanceID;
		}

		// Token: 0x04001850 RID: 6224
		private const string schemaId = "http://schemas.microsoft.com/2006/08/ServiceModel/ComPlusTxProxyTxTraceRecord";

		// Token: 0x04001851 RID: 6225
		[DataMember(Name = "appid")]
		private Guid appid;

		// Token: 0x04001852 RID: 6226
		[DataMember(Name = "clsid")]
		private Guid clsid;

		// Token: 0x04001853 RID: 6227
		[DataMember(Name = "TransactionID")]
		private Guid transactionID;

		// Token: 0x04001854 RID: 6228
		[DataMember(Name = "InstanceID")]
		private int instanceID;
	}
}
