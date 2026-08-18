using System;
using System.Runtime.Diagnostics;
using System.Runtime.Serialization;
using System.Xml;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x020001F9 RID: 505
	[DataContract(Name = "ComPlusDllHostInitializer")]
	internal class ComPlusDllHostInitializerSchema : TraceRecord
	{
		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000FDB RID: 4059 RVA: 0x00038EAE File Offset: 0x000370AE
		internal override string EventId
		{
			get
			{
				return "http://schemas.microsoft.com/2006/08/ServiceModel/ComPlusDllHostInitializerTraceRecord";
			}
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x00038EB5 File Offset: 0x000370B5
		internal override void WriteTo(XmlWriter xmlWriter)
		{
			ComPlusTraceRecord.SerializeRecord(xmlWriter, this);
		}

		// Token: 0x06000FDD RID: 4061 RVA: 0x00038EBE File Offset: 0x000370BE
		public override string ToString()
		{
			return SR.GetString("ComPlusServiceSchemaDllHost", new object[]
			{
				this.appid.ToString()
			});
		}

		// Token: 0x06000FDE RID: 4062 RVA: 0x00038EE4 File Offset: 0x000370E4
		public ComPlusDllHostInitializerSchema(Guid appid)
		{
			this.appid = appid;
		}

		// Token: 0x040017E9 RID: 6121
		private const string schemaId = "http://schemas.microsoft.com/2006/08/ServiceModel/ComPlusDllHostInitializerTraceRecord";

		// Token: 0x040017EA RID: 6122
		[DataMember(Name = "appid")]
		private Guid appid;
	}
}
