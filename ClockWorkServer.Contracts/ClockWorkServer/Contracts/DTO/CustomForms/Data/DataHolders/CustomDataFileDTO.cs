using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.CustomForms.Data;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Data.DataHolders
{
	// Token: 0x0200076D RID: 1901
	[DataContract(Namespace = "http://tpro.ca")]
	public class CustomDataFileDTO : CustomDataHolderDTO
	{
		// Token: 0x17000D91 RID: 3473
		// (get) Token: 0x0600270D RID: 9997 RVA: 0x0001222F File Offset: 0x0001042F
		// (set) Token: 0x0600270E RID: 9998 RVA: 0x00012237 File Offset: 0x00010437
		[DataMember]
		public string FileId { get; set; }

		// Token: 0x17000D92 RID: 3474
		// (get) Token: 0x0600270F RID: 9999 RVA: 0x00012240 File Offset: 0x00010440
		// (set) Token: 0x06002710 RID: 10000 RVA: 0x00012248 File Offset: 0x00010448
		[DataMember]
		public string Filename { get; set; }

		// Token: 0x17000D93 RID: 3475
		// (get) Token: 0x06002711 RID: 10001 RVA: 0x00012251 File Offset: 0x00010451
		// (set) Token: 0x06002712 RID: 10002 RVA: 0x00012259 File Offset: 0x00010459
		[DataMember]
		public long FileSize { get; set; }

		// Token: 0x06002713 RID: 10003 RVA: 0x00012262 File Offset: 0x00010462
		public CustomDataFileDTO()
		{
			base.DataType = eCustomDataPrimitiveType.File;
		}

		// Token: 0x06002714 RID: 10004 RVA: 0x00012274 File Offset: 0x00010474
		public CustomDataFileDTO(CustomDataHolderDTO dataObj) : base(dataObj)
		{
			base.DataType = eCustomDataPrimitiveType.File;
		}
	}
}
