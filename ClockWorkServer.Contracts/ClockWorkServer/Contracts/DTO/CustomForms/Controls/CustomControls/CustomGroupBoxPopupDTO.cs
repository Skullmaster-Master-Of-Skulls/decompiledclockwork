using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.CustomForms.Field;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Controls.CustomControls
{
	// Token: 0x0200077F RID: 1919
	[DataContract(Namespace = "http://tpro.ca")]
	[CustomControlBase(eCustomControlType.GroupBoxPopup)]
	public class CustomGroupBoxPopupDTO : CustomControlContainerDTO
	{
		// Token: 0x17000DAC RID: 3500
		// (get) Token: 0x06002762 RID: 10082 RVA: 0x00012708 File Offset: 0x00010908
		// (set) Token: 0x06002763 RID: 10083 RVA: 0x00012710 File Offset: 0x00010910
		[DataMember]
		public bool ShowCaption { get; set; }

		// Token: 0x17000DAD RID: 3501
		// (get) Token: 0x06002764 RID: 10084 RVA: 0x00012719 File Offset: 0x00010919
		// (set) Token: 0x06002765 RID: 10085 RVA: 0x00012721 File Offset: 0x00010921
		[DataMember]
		public int? BackgroundColorArgb { get; set; }
	}
}
