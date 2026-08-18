using System;
using System.Runtime.Serialization;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000203 RID: 515
	[DataContract(Name = "ComPlusMethodCallTxMismatch")]
	internal class ComPlusMethodCallTxMismatchSchema : ComPlusMethodCallSchema
	{
		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000FFD RID: 4093 RVA: 0x00039264 File Offset: 0x00037464
		internal override string EventId
		{
			get
			{
				return "http://schemas.microsoft.com/2006/08/ServiceModel/ComPlusMethodCallTxMismatchTraceRecord";
			}
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x0003926C File Offset: 0x0003746C
		public ComPlusMethodCallTxMismatchSchema(Uri from, Guid appid, Guid clsid, Guid iid, string action, int instanceID, int managedThreadID, int unmanagedThreadID, string requestingIdentity, Guid incomingTransactionID, Guid currentTransactionID) : base(from, appid, clsid, iid, action, instanceID, managedThreadID, unmanagedThreadID, requestingIdentity)
		{
			this.incomingTransactionID = incomingTransactionID;
			this.currentTransactionID = currentTransactionID;
		}

		// Token: 0x04001819 RID: 6169
		private const string schemaId = "http://schemas.microsoft.com/2006/08/ServiceModel/ComPlusMethodCallTxMismatchTraceRecord";

		// Token: 0x0400181A RID: 6170
		[DataMember(Name = "IncomingTransactionID")]
		private Guid incomingTransactionID;

		// Token: 0x0400181B RID: 6171
		[DataMember(Name = "CurrentTransactionID")]
		private Guid currentTransactionID;
	}
}
