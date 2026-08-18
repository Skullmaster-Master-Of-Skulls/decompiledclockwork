using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.MergeDuplicates.Students;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MergeDuplicates
{
	// Token: 0x02000460 RID: 1120
	[DataContract(Namespace = "http://tpro.ca")]
	public class DuplicateStudentSetDTO
	{
		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x060017EC RID: 6124 RVA: 0x0000B0AE File Offset: 0x000092AE
		// (set) Token: 0x060017ED RID: 6125 RVA: 0x0000B0B6 File Offset: 0x000092B6
		[DataMember]
		public DuplicateStudentDTO Student1 { get; set; }

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x060017EE RID: 6126 RVA: 0x0000B0BF File Offset: 0x000092BF
		// (set) Token: 0x060017EF RID: 6127 RVA: 0x0000B0C7 File Offset: 0x000092C7
		[DataMember]
		public DuplicateStudentDTO Student2 { get; set; }

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x060017F0 RID: 6128 RVA: 0x0000B0D0 File Offset: 0x000092D0
		// (set) Token: 0x060017F1 RID: 6129 RVA: 0x0000B0D8 File Offset: 0x000092D8
		[DataMember]
		public string CorrectStudentNumber { get; set; }

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x060017F2 RID: 6130 RVA: 0x0000B0E1 File Offset: 0x000092E1
		// (set) Token: 0x060017F3 RID: 6131 RVA: 0x0000B0E9 File Offset: 0x000092E9
		[DataMember]
		public eDuplicateItemToUse StudentToKeep { get; set; }

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x060017F4 RID: 6132 RVA: 0x0000B0F2 File Offset: 0x000092F2
		// (set) Token: 0x060017F5 RID: 6133 RVA: 0x0000B0FA File Offset: 0x000092FA
		[DataMember]
		public IList<DuplicateDynamicDataItemDTO> DuplicateDataItems { get; set; }
	}
}
