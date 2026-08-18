using System;
using System.Runtime.Serialization;
using System.Xml;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x020001FF RID: 511
	[DataContract(Name = "ComPlusInstanceCreationSuccess")]
	internal class ComPlusInstanceCreationSuccessSchema : ComPlusInstanceCreationRequestSchema
	{
		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000FF0 RID: 4080 RVA: 0x000390BD File Offset: 0x000372BD
		internal override string EventId
		{
			get
			{
				return "http://schemas.microsoft.com/2006/08/ServiceModel/ComPlusInstanceCreationSuccessTraceRecord";
			}
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x000390C4 File Offset: 0x000372C4
		internal override void WriteTo(XmlWriter xmlWriter)
		{
			ComPlusTraceRecord.SerializeRecord(xmlWriter, this);
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x000390CD File Offset: 0x000372CD
		public ComPlusInstanceCreationSuccessSchema(Guid appid, Guid clsid, Uri from, Guid incomingTransactionID, string requestingIdentity, int instanceID) : base(appid, clsid, from, incomingTransactionID, requestingIdentity)
		{
			this.instanceID = instanceID;
		}

		// Token: 0x04001804 RID: 6148
		private const string schemaId = "http://schemas.microsoft.com/2006/08/ServiceModel/ComPlusInstanceCreationSuccessTraceRecord";

		// Token: 0x04001805 RID: 6149
		[DataMember(Name = "InstanceID")]
		private int instanceID;
	}
}
