using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.FileStorage;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B5D RID: 2909
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteMediaContentFileReq : BaseMessageReq
	{
		// Token: 0x170016BD RID: 5821
		// (get) Token: 0x06003DBD RID: 15805 RVA: 0x0001E521 File Offset: 0x0001C721
		// (set) Token: 0x06003DBE RID: 15806 RVA: 0x0001E529 File Offset: 0x0001C729
		[DataMember]
		public FileIdentifierDTO FileId { get; set; }
	}
}
