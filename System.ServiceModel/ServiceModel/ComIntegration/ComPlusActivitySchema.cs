using System;
using System.Runtime.Diagnostics;
using System.Runtime.Serialization;
using System.Xml;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000201 RID: 513
	[DataContract(Name = "ComPlusActivity")]
	internal class ComPlusActivitySchema : TraceRecord
	{
		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000FF6 RID: 4086 RVA: 0x00039111 File Offset: 0x00037311
		internal override string EventId
		{
			get
			{
				return "http://schemas.microsoft.com/2006/08/ServiceModel/ComPlusActivityTraceRecord";
			}
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x00039118 File Offset: 0x00037318
		internal override void WriteTo(XmlWriter xmlWriter)
		{
			ComPlusTraceRecord.SerializeRecord(xmlWriter, this);
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x00039121 File Offset: 0x00037321
		public ComPlusActivitySchema(Guid activityID, Guid logicalThreadID, int managedThreadID, int unmanagedThreadID)
		{
			this.activityID = activityID;
			this.logicalThreadID = logicalThreadID;
			this.managedThreadID = managedThreadID;
			this.unmanagedThreadID = unmanagedThreadID;
		}

		// Token: 0x0400180A RID: 6154
		private const string schemaId = "http://schemas.microsoft.com/2006/08/ServiceModel/ComPlusActivityTraceRecord";

		// Token: 0x0400180B RID: 6155
		[DataMember(Name = "ActivityID")]
		private Guid activityID;

		// Token: 0x0400180C RID: 6156
		[DataMember(Name = "LogicalThreadID")]
		private Guid logicalThreadID;

		// Token: 0x0400180D RID: 6157
		[DataMember(Name = "ManagedThreadID")]
		private int managedThreadID;

		// Token: 0x0400180E RID: 6158
		[DataMember(Name = "UnmanagedThreadID")]
		private int unmanagedThreadID;
	}
}
