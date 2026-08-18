using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.DynamicForms.DynamicLists;
using TechnoPro.Common.Public.Entities.Files;

namespace TechnoPro.Common.ICore.DynamicForms
{
	// Token: 0x0200009B RID: 155
	public interface IDynamicFileStorageManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000488 RID: 1160
		DynamicFile LoadDynamicFileById(int FileId, bool LoadFileContents);

		// Token: 0x06000489 RID: 1161
		DynamicListTable LoadFilesByStudent(int PersonId, int ControlId, bool LoadFileContents, eDynamicFormType DataType);

		// Token: 0x0600048A RID: 1162
		IDictionary<int, DynamicListTable> LoadPerStudentFilesByStudents(int ControlId, params int[] PersonIds);

		// Token: 0x0600048B RID: 1163
		int AddFile(int ControlId, DynamicDataContext Context, eDynamicFormType DataType, string Title, string Notes, BinaryFile File, int fileTypeCode = 1000);

		// Token: 0x0600048C RID: 1164
		Task<int> AddFileAsync(int ControlId, DynamicDataContext Context, eDynamicFormType DataType, string Title, string Notes, BinaryFile File, int fileTypeCode = 1000);

		// Token: 0x0600048D RID: 1165
		IList<DynamicListColumn> LoadColumnsByControlId(DynamicField Field);

		// Token: 0x0600048E RID: 1166
		IList<SyncDocumentAction> SyncDocuments(string ExternalFolderPath, int DocumentsControlId, eDynamicFormType DataType);

		// Token: 0x0600048F RID: 1167
		IList<T> LoadPerStudentSingleFileDescriptionsByStudentAndControls<T>(int PersonId, params int[] cids) where T : DynamicFileDescription;

		// Token: 0x06000490 RID: 1168
		Task<IList<T>> LoadPerStudentSingleFileDescriptionsByStudentAndControlsAsync<T>(int PersonId, params int[] cids) where T : DynamicFileDescription;

		// Token: 0x06000491 RID: 1169
		IList<DynamicFileDescription> LoadPerStudentFileListFileDescriptionsByStudentAndControls(int PersonId, params int[] cids);

		// Token: 0x06000492 RID: 1170
		Task<IList<DynamicFileDescription>> LoadPerStudentFileListFileDescriptionsByStudentAndControlsAsync(int PersonId, params int[] cids);

		// Token: 0x06000493 RID: 1171
		IList<DynamicFileDescriptionWithColData> LoadPerStudentFileListFileDescriptionsWithColDataByStudentAndControls(int PersonId, params int[] cids);

		// Token: 0x06000494 RID: 1172
		Task<IList<DynamicFileDescriptionWithColData>> LoadPerStudentFileListFileDescriptionsWithColDataByStudentAndControlsAsync(int PersonId, params int[] cids);

		// Token: 0x06000495 RID: 1173
		BinaryFile LoadFileFromDynamicFileDescription(int studentPersonId, DynamicFileDescription dynamicFileDescription);

		// Token: 0x06000496 RID: 1174
		Task<BinaryFile> LoadFileFromDynamicFileDescriptionAsync(int studentPersonId, DynamicFileDescription dynamicFileDescription);
	}
}
