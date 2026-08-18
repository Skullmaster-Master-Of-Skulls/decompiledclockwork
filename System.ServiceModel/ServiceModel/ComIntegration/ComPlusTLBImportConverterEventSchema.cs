using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Xml;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x020001FD RID: 509
	[DataContract(Name = "ComPlusTLBImportConverterEvent")]
	internal class ComPlusTLBImportConverterEventSchema : ComPlusTLBImportSchema
	{
		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000FE9 RID: 4073 RVA: 0x00038FDF File Offset: 0x000371DF
		internal override string EventId
		{
			get
			{
				return "http://schemas.microsoft.com/2006/08/ServiceModel/ComPlusTLBImportConverterEventTraceRecord";
			}
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x00038FE6 File Offset: 0x000371E6
		internal override void WriteTo(XmlWriter xmlWriter)
		{
			ComPlusTraceRecord.SerializeRecord(xmlWriter, this);
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x00038FEF File Offset: 0x000371EF
		public ComPlusTLBImportConverterEventSchema(Guid iid, Guid typeLibraryID, ImporterEventKind eventKind, int eventCode, string eventMessage) : base(iid, typeLibraryID)
		{
			this.eventKind = eventKind;
			this.eventCode = eventCode;
			this.eventMessage = eventMessage;
		}

		// Token: 0x040017FA RID: 6138
		private const string schemaId = "http://schemas.microsoft.com/2006/08/ServiceModel/ComPlusTLBImportConverterEventTraceRecord";

		// Token: 0x040017FB RID: 6139
		[DataMember(Name = "EventKind")]
		private ImporterEventKind eventKind;

		// Token: 0x040017FC RID: 6140
		[DataMember(Name = "EventCode")]
		private int eventCode;

		// Token: 0x040017FD RID: 6141
		[DataMember(Name = "EventMessage")]
		private string eventMessage;
	}
}
