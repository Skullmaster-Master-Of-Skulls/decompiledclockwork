using System;
using System.Runtime.Diagnostics;
using System.Runtime.Serialization;
using System.Xml;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000200 RID: 512
	[DataContract(Name = "ComPlusInstanceReleased")]
	internal class ComPlusInstanceReleasedSchema : TraceRecord
	{
		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000FF3 RID: 4083 RVA: 0x000390E4 File Offset: 0x000372E4
		internal override string EventId
		{
			get
			{
				return "http://schemas.microsoft.com/2006/08/ServiceModel/ComPlusInstanceReleasedTraceRecord";
			}
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x000390EB File Offset: 0x000372EB
		internal override void WriteTo(XmlWriter xmlWriter)
		{
			ComPlusTraceRecord.SerializeRecord(xmlWriter, this);
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x000390F4 File Offset: 0x000372F4
		public ComPlusInstanceReleasedSchema(Guid appid, Guid clsid, int instanceID)
		{
			this.appid = appid;
			this.clsid = clsid;
			this.instanceID = instanceID;
		}

		// Token: 0x04001806 RID: 6150
		private const string schemaId = "http://schemas.microsoft.com/2006/08/ServiceModel/ComPlusInstanceReleasedTraceRecord";

		// Token: 0x04001807 RID: 6151
		[DataMember(Name = "appid")]
		private Guid appid;

		// Token: 0x04001808 RID: 6152
		[DataMember(Name = "clsid")]
		private Guid clsid;

		// Token: 0x04001809 RID: 6153
		[DataMember(Name = "InstanceID")]
		private int instanceID;
	}
}
