using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Vets;

namespace TechnoPro.Common.ICore.Vets
{
	// Token: 0x02000012 RID: 18
	public interface IVetsChapterManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600006E RID: 110
		Task<IList<VetsChapter>> GetChaptersAsync();

		// Token: 0x0600006F RID: 111
		IList<VetsChapter> GetChapters();

		// Token: 0x06000070 RID: 112
		Task<bool> DeleteChapterAsync(Guid chapterId);

		// Token: 0x06000071 RID: 113
		Task ChangeChapterDisabled(Guid ChapterId, bool newIsDisabled);

		// Token: 0x06000072 RID: 114
		Task UpdateChapterAsync(VetsChapter chapter);

		// Token: 0x06000073 RID: 115
		Task<Guid> CreateChapterAsync(VetsChapter chapter);
	}
}
