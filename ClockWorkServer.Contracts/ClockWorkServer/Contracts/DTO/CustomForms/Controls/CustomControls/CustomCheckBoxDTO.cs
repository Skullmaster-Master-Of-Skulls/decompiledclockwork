using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.CustomForms.Controls;
using TechnoPro.Common.Public.Entities.CustomForms.Field;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Controls.CustomControls
{
	// Token: 0x0200077C RID: 1916
	[DataContract(Namespace = "http://tpro.ca")]
	[CustomControlBase(eCustomControlType.CheckBox)]
	public class CustomCheckBoxDTO : CustomControlDataHolderDTO
	{
		// Token: 0x17000DA8 RID: 3496
		// (get) Token: 0x06002757 RID: 10071 RVA: 0x000126B2 File Offset: 0x000108B2
		// (set) Token: 0x06002758 RID: 10072 RVA: 0x000126BA File Offset: 0x000108BA
		[DataMember]
		public eCustomControlSize Size { get; set; }
	}
}
