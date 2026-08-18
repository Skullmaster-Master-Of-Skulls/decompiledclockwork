using System;

namespace System.ServiceModel.Description
{
	// Token: 0x020003E8 RID: 1000
	[MessageContract(IsWrapped = false)]
	internal class GetResponse
	{
		// Token: 0x060025B1 RID: 9649 RVA: 0x0008756A File Offset: 0x0008576A
		internal GetResponse()
		{
		}

		// Token: 0x060025B2 RID: 9650 RVA: 0x00087572 File Offset: 0x00085772
		internal GetResponse(MetadataSet metadataSet) : this()
		{
			this.metadataSet = metadataSet;
		}

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x060025B3 RID: 9651 RVA: 0x00087581 File Offset: 0x00085781
		// (set) Token: 0x060025B4 RID: 9652 RVA: 0x00087589 File Offset: 0x00085789
		[MessageBodyMember(Name = "Metadata", Namespace = "http://schemas.xmlsoap.org/ws/2004/09/mex")]
		internal MetadataSet Metadata
		{
			get
			{
				return this.metadataSet;
			}
			set
			{
				this.metadataSet = value;
			}
		}

		// Token: 0x040020D4 RID: 8404
		private MetadataSet metadataSet;
	}
}
