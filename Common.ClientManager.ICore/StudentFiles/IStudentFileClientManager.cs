using System;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentFiles;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.StudentFiles
{
	// Token: 0x02000012 RID: 18
	public interface IStudentFileClientManager : IWebService
	{
		// Token: 0x06000076 RID: 118
		StudentFileCategoryFileDescriptionsWithColDataDTO[] LoadStudentFileDescriptions(int studentPesonId);

		// Token: 0x06000077 RID: 119
		Task<StudentFileCategoryFileDescriptionsWithColDataDTO[]> LoadStudentFileDescriptionsAsync(int studentPersonId);

		// Token: 0x06000078 RID: 120
		BinaryFileDTO LoadFileFromDynamicFileDescription(int studentPersonId, DynamicFileDescriptionDTO fileDescription);

		// Token: 0x06000079 RID: 121
		Task<BinaryFileDTO> LoadFileFromDynamicFileDescriptionAsync(int studentPersonId, DynamicFileDescriptionDTO fileDescription);

		// Token: 0x0600007A RID: 122
		Task<int> UploadStudentFileAsync(string StudentComment, BinaryFileDTO File);
	}
}
