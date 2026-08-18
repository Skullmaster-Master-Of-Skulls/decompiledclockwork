using System;
using System.Runtime.Serialization;
using System.Xml;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x020001FC RID: 508
	[DataContract(Name = "ComPlusTLBImportFromAssembly")]
	internal class ComPlusTLBImportFromAssemblySchema : ComPlusTLBImportSchema
	{
		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000FE6 RID: 4070 RVA: 0x00038FBE File Offset: 0x000371BE
		internal override string EventId
		{
			get
			{
				return "http://schemas.microsoft.com/2006/08/ServiceModel/ComPlusTLBImportFromAssemblyTraceRecord";
			}
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x00038FC5 File Offset: 0x000371C5
		internal override void WriteTo(XmlWriter xmlWriter)
		{
			ComPlusTraceRecord.SerializeRecord(xmlWriter, this);
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x00038FCE File Offset: 0x000371CE
		public ComPlusTLBImportFromAssemblySchema(Guid iid, Guid typeLibraryID, string assembly) : base(iid, typeLibraryID)
		{
			this.assembly = assembly;
		}

		// Token: 0x040017F8 RID: 6136
		private const string schemaId = "http://schemas.microsoft.com/2006/08/ServiceModel/ComPlusTLBImportFromAssemblyTraceRecord";

		// Token: 0x040017F9 RID: 6137
		[DataMember(Name = "Assembly")]
		private string assembly;
	}
}
