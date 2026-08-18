using System;
using System.Runtime.Diagnostics;
using System.Runtime.Serialization;
using System.Xml;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x020001F5 RID: 501
	[DataContract(Name = "ComPlusServiceHost")]
	internal class ComPlusServiceHostSchema : TraceRecord
	{
		// Token: 0x06000FCE RID: 4046 RVA: 0x00038DB6 File Offset: 0x00036FB6
		public ComPlusServiceHostSchema(Guid appid, Guid clsid)
		{
			this.appid = appid;
			this.clsid = clsid;
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000FCF RID: 4047 RVA: 0x00038DCC File Offset: 0x00036FCC
		internal override string EventId
		{
			get
			{
				return base.BuildEventId("ComPlusServiceHost");
			}
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x00038DD9 File Offset: 0x00036FD9
		internal override void WriteTo(XmlWriter xmlWriter)
		{
			ComPlusTraceRecord.SerializeRecord(xmlWriter, this);
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x00038DE2 File Offset: 0x00036FE2
		public override string ToString()
		{
			return SR.GetString("ComPlusServiceSchema", new object[]
			{
				this.appid.ToString(),
				this.clsid.ToString()
			});
		}

		// Token: 0x040017E1 RID: 6113
		[DataMember(Name = "appid")]
		private Guid appid;

		// Token: 0x040017E2 RID: 6114
		[DataMember(Name = "clsid")]
		private Guid clsid;
	}
}
