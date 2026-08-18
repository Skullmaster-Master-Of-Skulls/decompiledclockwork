using System;
using System.Globalization;
using System.Runtime.Diagnostics;
using System.Runtime.Serialization;
using System.Xml;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000202 RID: 514
	[DataContract(Name = "ComPlusMethodCall")]
	internal class ComPlusMethodCallSchema : TraceRecord
	{
		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000FF9 RID: 4089 RVA: 0x00039146 File Offset: 0x00037346
		internal override string EventId
		{
			get
			{
				return "http://schemas.microsoft.com/2006/08/ServiceModel/ComPlusMethodCallTraceRecord";
			}
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x0003914D File Offset: 0x0003734D
		internal override void WriteTo(XmlWriter xmlWriter)
		{
			ComPlusTraceRecord.SerializeRecord(xmlWriter, this);
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x00039158 File Offset: 0x00037358
		public override string ToString()
		{
			return SR.GetString("ComPlusMethodCallSchema", new object[]
			{
				this.from.ToString(),
				this.appid.ToString(),
				this.clsid.ToString(),
				this.iid.ToString(),
				this.action,
				this.instanceID.ToString(CultureInfo.CurrentCulture),
				this.managedThreadID.ToString(CultureInfo.CurrentCulture),
				this.unmanagedThreadID.ToString(CultureInfo.CurrentCulture),
				this.requestingIdentity
			});
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x0003920C File Offset: 0x0003740C
		public ComPlusMethodCallSchema(Uri from, Guid appid, Guid clsid, Guid iid, string action, int instanceID, int managedThreadID, int unmanagedThreadID, string requestingIdentity)
		{
			this.from = from;
			this.appid = appid;
			this.clsid = clsid;
			this.iid = iid;
			this.action = action;
			this.instanceID = instanceID;
			this.managedThreadID = managedThreadID;
			this.unmanagedThreadID = unmanagedThreadID;
			this.requestingIdentity = requestingIdentity;
		}

		// Token: 0x0400180F RID: 6159
		private const string schemaId = "http://schemas.microsoft.com/2006/08/ServiceModel/ComPlusMethodCallTraceRecord";

		// Token: 0x04001810 RID: 6160
		[DataMember(Name = "From")]
		private Uri from;

		// Token: 0x04001811 RID: 6161
		[DataMember(Name = "appid")]
		private Guid appid;

		// Token: 0x04001812 RID: 6162
		[DataMember(Name = "clsid")]
		private Guid clsid;

		// Token: 0x04001813 RID: 6163
		[DataMember(Name = "iid")]
		private Guid iid;

		// Token: 0x04001814 RID: 6164
		[DataMember(Name = "Action")]
		private string action;

		// Token: 0x04001815 RID: 6165
		[DataMember(Name = "InstanceID")]
		private int instanceID;

		// Token: 0x04001816 RID: 6166
		[DataMember(Name = "ManagedThreadID")]
		private int managedThreadID;

		// Token: 0x04001817 RID: 6167
		[DataMember(Name = "UnmanagedThreadID")]
		private int unmanagedThreadID;

		// Token: 0x04001818 RID: 6168
		[DataMember(Name = "RequestingIdentity")]
		private string requestingIdentity;
	}
}
