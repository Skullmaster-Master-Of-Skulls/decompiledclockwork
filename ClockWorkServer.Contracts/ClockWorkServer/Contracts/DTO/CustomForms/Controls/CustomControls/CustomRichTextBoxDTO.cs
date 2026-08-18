using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.CustomForms.Field;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Controls.CustomControls
{
	// Token: 0x02000783 RID: 1923
	[DataContract(Namespace = "http://tpro.ca")]
	[CustomControlBase(eCustomControlType.RichTextBox)]
	public class CustomRichTextBoxDTO : CustomControlDataHolderDTO
	{
		// Token: 0x17000DB2 RID: 3506
		// (get) Token: 0x06002772 RID: 10098 RVA: 0x00012777 File Offset: 0x00010977
		// (set) Token: 0x06002773 RID: 10099 RVA: 0x0001277F File Offset: 0x0001097F
		[DataMember]
		public int RowCount { get; set; }

		// Token: 0x17000DB3 RID: 3507
		// (get) Token: 0x06002774 RID: 10100 RVA: 0x00012788 File Offset: 0x00010988
		// (set) Token: 0x06002775 RID: 10101 RVA: 0x00012790 File Offset: 0x00010990
		[DataMember]
		public int MaxChars { get; set; }
	}
}
