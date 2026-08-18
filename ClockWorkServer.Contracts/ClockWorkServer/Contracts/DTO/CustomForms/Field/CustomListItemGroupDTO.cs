using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Field
{
	// Token: 0x02000763 RID: 1891
	[DataContract(Namespace = "http://tpro.ca")]
	public class CustomListItemGroupDTO
	{
		// Token: 0x17000D82 RID: 3458
		// (get) Token: 0x060026E2 RID: 9954 RVA: 0x00012094 File Offset: 0x00010294
		// (set) Token: 0x060026E3 RID: 9955 RVA: 0x0001209C File Offset: 0x0001029C
		[DataMember]
		public Guid Id { get; set; }

		// Token: 0x17000D83 RID: 3459
		// (get) Token: 0x060026E4 RID: 9956 RVA: 0x000120A5 File Offset: 0x000102A5
		// (set) Token: 0x060026E5 RID: 9957 RVA: 0x000120AD File Offset: 0x000102AD
		[DataMember]
		public string GroupCaption { get; set; }

		// Token: 0x17000D84 RID: 3460
		// (get) Token: 0x060026E6 RID: 9958 RVA: 0x000120B6 File Offset: 0x000102B6
		// (set) Token: 0x060026E7 RID: 9959 RVA: 0x000120BE File Offset: 0x000102BE
		[DataMember]
		public IList<CustomListItemDTO> ListItems { get; set; }
	}
}
