using System;
using System.Linq;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentFiles;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.StudentFiles;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.StudentFiles
{
	// Token: 0x02000010 RID: 16
	public class StudentFileRestClientManager : BearerTokenRestProxy<IStudentFileClientManager>, IStudentFileClientManager, IWebService
	{
		// Token: 0x06000085 RID: 133 RVA: 0x00003511 File Offset: 0x00001711
		public StudentFileRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000351B File Offset: 0x0000171B
		public StudentFileRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003526 File Offset: 0x00001726
		public StudentFileCategoryFileDescriptionsDTO[] LoadStudentFileDescriptions(int studentPesonId)
		{
			return base.GetMany<StudentFileCategoryFileDescriptionsDTO>(string.Format("studentfile/descriptions/studentpersonid/{0}", studentPesonId), true).ToArray<StudentFileCategoryFileDescriptionsDTO>();
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003544 File Offset: 0x00001744
		public async Task<StudentFileCategoryFileDescriptionsDTO[]> LoadStudentFileDescriptionsAsync(int studentPersonId)
		{
			return (await this.GetManyAsync<StudentFileCategoryFileDescriptionsDTO>(string.Format("studentfile/descriptions/studentpersonid/{0}", studentPersonId), true).ConfigureAwait(false)).ToArray<StudentFileCategoryFileDescriptionsDTO>();
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003594 File Offset: 0x00001794
		public BinaryFileDTO LoadFileFromDynamicFileDescription(int studentPersonId, DynamicFileDescriptionDTO fileDescription)
		{
			LoadFileFromDynamicFileDescriptionReq loadFileFromDynamicFileDescriptionReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadFileFromDynamicFileDescriptionReq>();
			loadFileFromDynamicFileDescriptionReq.StudentPersonId = studentPersonId;
			loadFileFromDynamicFileDescriptionReq.DynamicFileDescription = fileDescription;
			return base.Post<LoadFileFromDynamicFileDescriptionReq, BinaryFileDTO>(loadFileFromDynamicFileDescriptionReq, "studentfile/loadfilefromdynamicfiledescription");
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000035C8 File Offset: 0x000017C8
		public async Task<BinaryFileDTO> LoadFileFromDynamicFileDescriptionAsync(int studentPersonId, DynamicFileDescriptionDTO fileDescription)
		{
			LoadFileFromDynamicFileDescriptionReq loadFileFromDynamicFileDescriptionReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadFileFromDynamicFileDescriptionReq>();
			loadFileFromDynamicFileDescriptionReq.StudentPersonId = studentPersonId;
			loadFileFromDynamicFileDescriptionReq.DynamicFileDescription = fileDescription;
			return await this.PostAsync<LoadFileFromDynamicFileDescriptionReq, BinaryFileDTO>(loadFileFromDynamicFileDescriptionReq, "studentfile/loadfilefromdynamicfiledescription").ConfigureAwait(false);
		}
	}
}
