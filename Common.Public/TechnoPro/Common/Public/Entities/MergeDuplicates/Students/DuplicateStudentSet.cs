using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.MergeDuplicates.Students
{
	// Token: 0x02000291 RID: 657
	public class DuplicateStudentSet
	{
		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x060013EA RID: 5098 RVA: 0x00019ACE File Offset: 0x00017CCE
		// (set) Token: 0x060013EB RID: 5099 RVA: 0x00019AD6 File Offset: 0x00017CD6
		public DuplicateStudent Student1 { get; set; }

		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x060013EC RID: 5100 RVA: 0x00019ADF File Offset: 0x00017CDF
		// (set) Token: 0x060013ED RID: 5101 RVA: 0x00019AE7 File Offset: 0x00017CE7
		public DuplicateStudent Student2 { get; set; }

		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x060013EE RID: 5102 RVA: 0x00019AF0 File Offset: 0x00017CF0
		// (set) Token: 0x060013EF RID: 5103 RVA: 0x00019AF8 File Offset: 0x00017CF8
		public string CorrectStudentNumber { get; set; }

		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x060013F0 RID: 5104 RVA: 0x00019B01 File Offset: 0x00017D01
		// (set) Token: 0x060013F1 RID: 5105 RVA: 0x00019B09 File Offset: 0x00017D09
		public eDuplicateItemToUse StudentToKeep { get; set; }

		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x060013F2 RID: 5106 RVA: 0x00019B12 File Offset: 0x00017D12
		// (set) Token: 0x060013F3 RID: 5107 RVA: 0x00019B1A File Offset: 0x00017D1A
		public IList<DuplicateDynamicDataItem> DuplicateDataItems { get; set; }
	}
}
