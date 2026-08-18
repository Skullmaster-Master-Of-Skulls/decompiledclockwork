using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.CustomForms.Field;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Controls.CustomControls
{
	// Token: 0x02000786 RID: 1926
	[DataContract(Namespace = "http://tpro.ca")]
	[CustomControlBase(eCustomControlType.TextBoxList)]
	public class CustomTextBoxListDTO : CustomControlDataHolderDTO
	{
		// Token: 0x17000DB7 RID: 3511
		// (get) Token: 0x0600277F RID: 10111 RVA: 0x000127CC File Offset: 0x000109CC
		// (set) Token: 0x06002780 RID: 10112 RVA: 0x000127D4 File Offset: 0x000109D4
		[DataMember]
		public int MaxChars { get; set; }

		// Token: 0x17000DB8 RID: 3512
		// (get) Token: 0x06002781 RID: 10113 RVA: 0x000127DD File Offset: 0x000109DD
		// (set) Token: 0x06002782 RID: 10114 RVA: 0x000127E5 File Offset: 0x000109E5
		[DataMember]
		public int TextBoxCountStart { get; set; }

		// Token: 0x17000DB9 RID: 3513
		// (get) Token: 0x06002783 RID: 10115 RVA: 0x000127EE File Offset: 0x000109EE
		// (set) Token: 0x06002784 RID: 10116 RVA: 0x000127F6 File Offset: 0x000109F6
		[DataMember]
		public int MaxTextBoxCount { get; set; }
	}
}
