using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.Common.Public.Entities.StudentFiles
{
	// Token: 0x0200018B RID: 395
	public class StudentFileCategoryFileDescriptionsWithColData : BusinessBase<string>
	{
		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x060009E1 RID: 2529 RVA: 0x00013104 File Offset: 0x00011304
		// (set) Token: 0x060009E2 RID: 2530 RVA: 0x0000E9FC File Offset: 0x0000CBFC
		public virtual string StudentFileCategoryTitle
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x060009E3 RID: 2531 RVA: 0x0001311C File Offset: 0x0001131C
		// (set) Token: 0x060009E4 RID: 2532 RVA: 0x00013124 File Offset: 0x00011324
		public IList<DynamicFileDescriptionWithColData> FileDescriptions { get; set; }
	}
}
