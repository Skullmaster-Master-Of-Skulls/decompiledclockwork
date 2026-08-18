using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.CustomForms.Field;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Controls.CustomControls
{
	// Token: 0x0200077D RID: 1917
	[DataContract(Namespace = "http://tpro.ca")]
	[CustomControlBase(eCustomControlType.DropList)]
	public class CustomDropListDTO : CustomControlDataHolderDTO
	{
		// Token: 0x17000DA9 RID: 3497
		// (get) Token: 0x0600275A RID: 10074 RVA: 0x000126CC File Offset: 0x000108CC
		// (set) Token: 0x0600275B RID: 10075 RVA: 0x000126D4 File Offset: 0x000108D4
		[DataMember]
		public Guid CustomListGroupId { get; set; }
	}
}
