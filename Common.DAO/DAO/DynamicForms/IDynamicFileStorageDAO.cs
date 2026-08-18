using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.DynamicForms.DynamicLists;

namespace TechnoPro.Common.DAO.DynamicForms
{
	// Token: 0x02000083 RID: 131
	public interface IDynamicFileStorageDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000360 RID: 864
		DynamicFile LoadDynamicFileById(int FileId, bool LoadFileContents);

		// Token: 0x06000361 RID: 865
		IList<T> LoadPerStudentSingleFileDescriptionsByStudentAndControls<T>(int PersonId, params int[] cids) where T : DynamicFileDescription;

		// Token: 0x06000362 RID: 866
		Task<IList<T>> LoadPerStudentSingleFileDescriptionsByStudentAndControlsAsync<T>(int PersonId, params int[] cids) where T : DynamicFileDescription;

		// Token: 0x06000363 RID: 867
		IList<DynamicFileDescription> LoadPerStudentFileListFileDescriptionsByStudentAndControls(int PersonId, params int[] cids);

		// Token: 0x06000364 RID: 868
		IList<DynamicFileDescriptionWithColData> LoadPerStudentFileListFileDescriptionsWithColDataByStudentAndControls(int PersonId, params int[] cids);

		// Token: 0x06000365 RID: 869
		Task<IList<DynamicFileDescription>> LoadPerStudentFileListFileDescriptionsByStudentAndControlsAsync(int PersonId, params int[] cids);

		// Token: 0x06000366 RID: 870
		Task<IList<DynamicFileDescriptionWithColData>> LoadPerStudentFileListFileDescriptionsWithColDataByStudentAndControlsAsync(int PersonId, params int[] cids);
	}
}
