using System;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Files;
using TechnoPro.Common.Public.Entities.StudentFiles;

namespace TechnoPro.Common.ICore.StudentFiles
{
	// Token: 0x0200002D RID: 45
	public interface IStudentFilesCategoryManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000145 RID: 325
		StudentFileCategoryFileDescriptionsWithColData[] LoadStudentFileDescriptions(int studentPersonId);

		// Token: 0x06000146 RID: 326
		Task<StudentFileCategoryFileDescriptionsWithColData[]> LoadStudentFileDescriptionsAsync(int studentPersonId);

		// Token: 0x06000147 RID: 327
		Task<int> UploadStudentFileAsync(string StudentComment, BinaryFile File);

		// Token: 0x06000148 RID: 328
		int UploadStudentFile(string StudentComment, BinaryFile File);
	}
}
