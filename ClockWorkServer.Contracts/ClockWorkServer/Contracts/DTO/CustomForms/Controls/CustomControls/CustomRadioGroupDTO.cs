using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.CustomForms.Field;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Controls.CustomControls
{
	// Token: 0x02000781 RID: 1921
	[DataContract(Namespace = "http://tpro.ca")]
	[CustomControlBase(eCustomControlType.RadioGroup)]
	public class CustomRadioGroupDTO : CustomControlDataHolderDTO
	{
		// Token: 0x17000DB0 RID: 3504
		// (get) Token: 0x0600276C RID: 10092 RVA: 0x00012755 File Offset: 0x00010955
		// (set) Token: 0x0600276D RID: 10093 RVA: 0x0001275D File Offset: 0x0001095D
		[DataMember]
		public int NumHorizontal { get; set; }

		// Token: 0x17000DB1 RID: 3505
		// (get) Token: 0x0600276E RID: 10094 RVA: 0x00012766 File Offset: 0x00010966
		// (set) Token: 0x0600276F RID: 10095 RVA: 0x0001276E File Offset: 0x0001096E
		[DataMember]
		public Guid CustomListGroupId { get; set; }
	}
}
