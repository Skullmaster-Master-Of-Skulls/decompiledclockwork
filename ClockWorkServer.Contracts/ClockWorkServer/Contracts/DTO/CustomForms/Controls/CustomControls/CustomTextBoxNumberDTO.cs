using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.CustomForms.Field;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Controls.CustomControls
{
	// Token: 0x02000787 RID: 1927
	[DataContract(Namespace = "http://tpro.ca")]
	[CustomControlBase(eCustomControlType.TextBoxNumber)]
	public class CustomTextBoxNumberDTO : CustomControlDataHolderDTO
	{
		// Token: 0x17000DBA RID: 3514
		// (get) Token: 0x06002786 RID: 10118 RVA: 0x000127FF File Offset: 0x000109FF
		// (set) Token: 0x06002787 RID: 10119 RVA: 0x00012807 File Offset: 0x00010A07
		[DataMember]
		public int MinValue { get; set; }

		// Token: 0x17000DBB RID: 3515
		// (get) Token: 0x06002788 RID: 10120 RVA: 0x00012810 File Offset: 0x00010A10
		// (set) Token: 0x06002789 RID: 10121 RVA: 0x00012818 File Offset: 0x00010A18
		[DataMember]
		public int MaxValue { get; set; }
	}
}
