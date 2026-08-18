using System;
using System.Runtime.Serialization;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000205 RID: 517
	[DataContract(Name = "ComPlusMethodCallContextTx")]
	internal class ComPlusMethodCallContextTxSchema : ComPlusMethodCallSchema
	{
		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06001001 RID: 4097 RVA: 0x000392D2 File Offset: 0x000374D2
		internal override string EventId
		{
			get
			{
				return "http://schemas.microsoft.com/2006/08/ServiceModel/ComPlusMethodCallContextTxTraceRecord";
			}
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x000392DC File Offset: 0x000374DC
		public ComPlusMethodCallContextTxSchema(Uri from, Guid appid, Guid clsid, Guid iid, string action, int instanceID, int managedThreadID, int unmanagedThreadID, string requestingIdentity, Guid contextTransactionID) : base(from, appid, clsid, iid, action, instanceID, managedThreadID, unmanagedThreadID, requestingIdentity)
		{
			this.contextTransactionID = contextTransactionID;
		}

		// Token: 0x0400181E RID: 6174
		private const string schemaId = "http://schemas.microsoft.com/2006/08/ServiceModel/ComPlusMethodCallContextTxTraceRecord";

		// Token: 0x0400181F RID: 6175
		[DataMember(Name = "ContextTransactionID")]
		private Guid contextTransactionID;
	}
}
