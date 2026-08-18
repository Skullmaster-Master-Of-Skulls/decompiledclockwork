using System;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentFiles;
using TechnoPro.Common.Core.DynamicForms;
using TechnoPro.Common.Core.Mappers.DynamicForms;
using TechnoPro.Common.Core.Mappers.Files;
using TechnoPro.Common.Core.Mappers.StudentFiles;
using TechnoPro.Common.Core.StudentFiles;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.ICore.StudentFiles;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Files;
using TechnoPro.Common.Public.Entities.StudentFiles;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x0200008F RID: 143
	public class StudentFileServiceManager : IStudentFile, IService
	{
		// Token: 0x06000520 RID: 1312 RVA: 0x00017F48 File Offset: 0x00016148
		public LoadFileFromDynamicFileDescriptionResp LoadFileFromDynamicFileDescription(LoadFileFromDynamicFileDescriptionReq Request)
		{
			IDynamicFileStorageManager dynamicFileStorageManager = new DynamicFileStorageManager(Request.GetOperationContext());
			LoadFileFromDynamicFileDescriptionResp loadFileFromDynamicFileDescriptionResp = new LoadFileFromDynamicFileDescriptionResp();
			IDynamicFileStorageManager dynamicFileStorageManager2 = dynamicFileStorageManager;
			int studentPersonId = Request.StudentPersonId;
			DynamicFileDescriptionDTO dynamicFileDescription = Request.DynamicFileDescription;
			BinaryFile binaryFile = dynamicFileStorageManager2.LoadFileFromDynamicFileDescription(studentPersonId, (dynamicFileDescription != null) ? dynamicFileDescription.ToDomainObject() : null);
			loadFileFromDynamicFileDescriptionResp.File = ((binaryFile != null) ? binaryFile.ToDTO() : null);
			return loadFileFromDynamicFileDescriptionResp;
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00017F9C File Offset: 0x0001619C
		public LoadStudentFileDescriptionsResp LoadStudentFileDescriptions(LoadStudentFileDescriptionsReq Request)
		{
			IStudentFilesCategoryManager studentFilesCategoryManager = new StudentFilesCategoryManager(Request.GetOperationContext());
			StudentFileCategoryFileDescriptionsWithColData[] array = studentFilesCategoryManager.LoadStudentFileDescriptions(Request.StudentPersonId);
			LoadStudentFileDescriptionsResp loadStudentFileDescriptionsResp = new LoadStudentFileDescriptionsResp();
			StudentFileCategoryFileDescriptionsWithColDataDTO[] studentFileCategoriesWithFileDescriptions;
			if (array == null)
			{
				studentFileCategoriesWithFileDescriptions = null;
			}
			else
			{
				studentFileCategoriesWithFileDescriptions = (from g in array
				select g.ToDTO()).ToArray<StudentFileCategoryFileDescriptionsWithColDataDTO>();
			}
			loadStudentFileDescriptionsResp.StudentFileCategoriesWithFileDescriptions = studentFileCategoriesWithFileDescriptions;
			return loadStudentFileDescriptionsResp;
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00018004 File Offset: 0x00016204
		public UploadStudentFileResp UploadStudentFile(UploadStudentFileReq Request)
		{
			IStudentFilesCategoryManager studentFilesCategoryManager = new StudentFilesCategoryManager(Request.GetOperationContext());
			IStudentFilesCategoryManager studentFilesCategoryManager2 = studentFilesCategoryManager;
			string studentComment = Request.StudentComment;
			BinaryFileDTO file = Request.File;
			int num = studentFilesCategoryManager2.UploadStudentFile(studentComment, (file != null) ? file.ToDomainObject() : null);
			object result;
			if (num <= 0)
			{
				result = null;
			}
			else
			{
				(result = new UploadStudentFileResp()).FileId = num;
			}
			return result;
		}
	}
}
