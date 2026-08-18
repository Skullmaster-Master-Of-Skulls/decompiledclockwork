using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Vets;

namespace TechnoPro.Common.DAO.Vets
{
	// Token: 0x02000015 RID: 21
	public interface IVetsChapterDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600002E RID: 46
		Task<IList<VetsChapter>> GetChaptersAsync();

		// Token: 0x0600002F RID: 47
		IList<VetsChapter> GetChapters();

		// Token: 0x06000030 RID: 48
		Task<bool> DeleteChapterAsync(Guid chapterId);

		// Token: 0x06000031 RID: 49
		Task ChangeChapterDisabled(Guid chapterId, bool newIsDisabled);

		// Token: 0x06000032 RID: 50
		Task UpdateChapterAsync(VetsChapter chapter);

		// Token: 0x06000033 RID: 51
		Task<Guid> CreateChapterAsync(VetsChapter chapter);
	}
}
