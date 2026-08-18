using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.Common.Public.Entities.StudentFiles
{
	// Token: 0x0200018A RID: 394
	public class StudentFileCategoryFileDescriptions : BusinessBase<string>
	{
		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x060009DC RID: 2524 RVA: 0x000130D8 File Offset: 0x000112D8
		// (set) Token: 0x060009DD RID: 2525 RVA: 0x0000E9FC File Offset: 0x0000CBFC
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

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x060009DE RID: 2526 RVA: 0x000130F0 File Offset: 0x000112F0
		// (set) Token: 0x060009DF RID: 2527 RVA: 0x000130F8 File Offset: 0x000112F8
		public IList<DynamicFileDescription> FileDescriptions { get; set; }
	}
}
