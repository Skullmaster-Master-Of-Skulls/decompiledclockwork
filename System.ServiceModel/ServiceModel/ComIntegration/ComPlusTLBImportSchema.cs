using System;
using System.Runtime.Diagnostics;
using System.Runtime.Serialization;
using System.Xml;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x020001FB RID: 507
	[DataContract(Name = "ComPlusTLBImport")]
	internal class ComPlusTLBImportSchema : TraceRecord
	{
		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000FE2 RID: 4066 RVA: 0x00038F5E File Offset: 0x0003715E
		internal override string EventId
		{
			get
			{
				return "http://schemas.microsoft.com/2006/08/ServiceModel/ComPlusTLBImportTraceRecord";
			}
		}

		// Token: 0x06000FE3 RID: 4067 RVA: 0x00038F65 File Offset: 0x00037165
		internal override void WriteTo(XmlWriter xmlWriter)
		{
			ComPlusTraceRecord.SerializeRecord(xmlWriter, this);
		}

		// Token: 0x06000FE4 RID: 4068 RVA: 0x00038F6E File Offset: 0x0003716E
		public override string ToString()
		{
			return SR.GetString("ComPlusTLBImportSchema", new object[]
			{
				this.iid.ToString(),
				this.typeLibraryID.ToString()
			});
		}

		// Token: 0x06000FE5 RID: 4069 RVA: 0x00038FA8 File Offset: 0x000371A8
		public ComPlusTLBImportSchema(Guid iid, Guid typeLibraryID)
		{
			this.iid = iid;
			this.typeLibraryID = typeLibraryID;
		}

		// Token: 0x040017F5 RID: 6133
		private const string schemaId = "http://schemas.microsoft.com/2006/08/ServiceModel/ComPlusTLBImportTraceRecord";

		// Token: 0x040017F6 RID: 6134
		[DataMember(Name = "InterfaceID")]
		private Guid iid;

		// Token: 0x040017F7 RID: 6135
		[DataMember(Name = "TypeLibraryID")]
		private Guid typeLibraryID;
	}
}
