using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Field
{
	// Token: 0x02000762 RID: 1890
	[DataContract(Namespace = "http://tpro.ca")]
	public class CustomListItemDTO
	{
		// Token: 0x17000D7F RID: 3455
		// (get) Token: 0x060026DB RID: 9947 RVA: 0x00012061 File Offset: 0x00010261
		// (set) Token: 0x060026DC RID: 9948 RVA: 0x00012069 File Offset: 0x00010269
		[DataMember]
		public Guid ListItemId { get; set; }

		// Token: 0x17000D80 RID: 3456
		// (get) Token: 0x060026DD RID: 9949 RVA: 0x00012072 File Offset: 0x00010272
		// (set) Token: 0x060026DE RID: 9950 RVA: 0x0001207A File Offset: 0x0001027A
		[DataMember]
		public string ItemCaption { get; set; }

		// Token: 0x17000D81 RID: 3457
		// (get) Token: 0x060026DF RID: 9951 RVA: 0x00012083 File Offset: 0x00010283
		// (set) Token: 0x060026E0 RID: 9952 RVA: 0x0001208B File Offset: 0x0001028B
		[DataMember]
		public int OrderNum { get; set; }
	}
}
