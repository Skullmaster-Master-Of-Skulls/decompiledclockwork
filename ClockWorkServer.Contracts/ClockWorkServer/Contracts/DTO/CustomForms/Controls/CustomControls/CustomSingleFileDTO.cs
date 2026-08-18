using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.CustomForms.Field;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Controls.CustomControls
{
	// Token: 0x02000784 RID: 1924
	[DataContract(Namespace = "http://tpro.ca")]
	[CustomControlBase(eCustomControlType.File)]
	public class CustomSingleFileDTO : CustomControlDataHolderDTO
	{
		// Token: 0x17000DB4 RID: 3508
		// (get) Token: 0x06002777 RID: 10103 RVA: 0x00012799 File Offset: 0x00010999
		// (set) Token: 0x06002778 RID: 10104 RVA: 0x000127A1 File Offset: 0x000109A1
		[DataMember]
		public string[] AllowedFileTypes { get; set; }
	}
}
