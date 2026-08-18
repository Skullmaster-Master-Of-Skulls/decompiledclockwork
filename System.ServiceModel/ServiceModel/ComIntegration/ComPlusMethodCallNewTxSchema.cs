using System;
using System.Runtime.Serialization;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000204 RID: 516
	[DataContract(Name = "ComPlusMethodCallNewTx")]
	internal class ComPlusMethodCallNewTxSchema : ComPlusMethodCallSchema
	{
		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000FFF RID: 4095 RVA: 0x0003929E File Offset: 0x0003749E
		internal override string EventId
		{
			get
			{
				return "http://schemas.microsoft.com/2006/08/ServiceModel/ComPlusMethodCallNewTxTraceRecord";
			}
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x000392A8 File Offset: 0x000374A8
		public ComPlusMethodCallNewTxSchema(Uri from, Guid appid, Guid clsid, Guid iid, string action, int instanceID, int managedThreadID, int unmanagedThreadID, string requestingIdentity, Guid newTransactionID) : base(from, appid, clsid, iid, action, instanceID, managedThreadID, unmanagedThreadID, requestingIdentity)
		{
			this.newTransactionID = newTransactionID;
		}

		// Token: 0x0400181C RID: 6172
		private const string schemaId = "http://schemas.microsoft.com/2006/08/ServiceModel/ComPlusMethodCallNewTxTraceRecord";

		// Token: 0x0400181D RID: 6173
		[DataMember(Name = "NewTransactionID")]
		private Guid newTransactionID;
	}
}
