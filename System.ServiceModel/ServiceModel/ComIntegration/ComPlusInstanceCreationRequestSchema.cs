using System;
using System.Runtime.Diagnostics;
using System.Runtime.Serialization;
using System.Xml;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x020001FE RID: 510
	[DataContract(Name = "ComPlusInstanceCreationRequest")]
	internal class ComPlusInstanceCreationRequestSchema : TraceRecord
	{
		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000FEC RID: 4076 RVA: 0x00039010 File Offset: 0x00037210
		internal override string EventId
		{
			get
			{
				return "http://schemas.microsoft.com/2006/08/ServiceModel/ComPlusInstanceCreationRequestTraceRecord";
			}
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x00039017 File Offset: 0x00037217
		internal override void WriteTo(XmlWriter xmlWriter)
		{
			ComPlusTraceRecord.SerializeRecord(xmlWriter, this);
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x00039020 File Offset: 0x00037220
		public override string ToString()
		{
			return SR.GetString("ComPlusInstanceCreationRequestSchema", new object[]
			{
				this.from.ToString(),
				this.appid.ToString(),
				this.clsid.ToString(),
				this.incomingTransactionID.ToString(),
				this.requestingIdentity
			});
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x00039090 File Offset: 0x00037290
		public ComPlusInstanceCreationRequestSchema(Guid appid, Guid clsid, Uri from, Guid incomingTransactionID, string requestingIdentity)
		{
			this.from = from;
			this.appid = appid;
			this.clsid = clsid;
			this.incomingTransactionID = incomingTransactionID;
			this.requestingIdentity = requestingIdentity;
		}

		// Token: 0x040017FE RID: 6142
		private const string schemaId = "http://schemas.microsoft.com/2006/08/ServiceModel/ComPlusInstanceCreationRequestTraceRecord";

		// Token: 0x040017FF RID: 6143
		[DataMember(Name = "From")]
		private Uri from;

		// Token: 0x04001800 RID: 6144
		[DataMember(Name = "appid")]
		private Guid appid;

		// Token: 0x04001801 RID: 6145
		[DataMember(Name = "clsid")]
		private Guid clsid;

		// Token: 0x04001802 RID: 6146
		[DataMember(Name = "IncomingTransactionID")]
		private Guid incomingTransactionID;

		// Token: 0x04001803 RID: 6147
		[DataMember(Name = "RequestingIdentity")]
		private string requestingIdentity;
	}
}
