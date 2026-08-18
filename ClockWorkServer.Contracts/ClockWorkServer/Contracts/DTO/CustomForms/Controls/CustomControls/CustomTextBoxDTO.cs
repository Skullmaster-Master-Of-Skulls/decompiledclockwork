using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.CustomForms.Field;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Controls.CustomControls
{
	// Token: 0x02000785 RID: 1925
	[DataContract(Namespace = "http://tpro.ca")]
	[CustomControlBase(eCustomControlType.TextBox)]
	public class CustomTextBoxDTO : CustomControlDataHolderDTO
	{
		// Token: 0x17000DB5 RID: 3509
		// (get) Token: 0x0600277A RID: 10106 RVA: 0x000127AA File Offset: 0x000109AA
		// (set) Token: 0x0600277B RID: 10107 RVA: 0x000127B2 File Offset: 0x000109B2
		[DataMember]
		public int RowCount { get; set; }

		// Token: 0x17000DB6 RID: 3510
		// (get) Token: 0x0600277C RID: 10108 RVA: 0x000127BB File Offset: 0x000109BB
		// (set) Token: 0x0600277D RID: 10109 RVA: 0x000127C3 File Offset: 0x000109C3
		[DataMember]
		public int MaxChars { get; set; }
	}
}
