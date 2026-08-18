using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentFiles;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000144 RID: 324
	internal class StudentFileClientBaseProxy : ClientBase<IStudentFile>, IStudentFile, IService
	{
		// Token: 0x06000C6F RID: 3183 RVA: 0x0001F074 File Offset: 0x0001D274
		public StudentFileClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x0001F07F File Offset: 0x0001D27F
		public StudentFileClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x0001F08C File Offset: 0x0001D28C
		public LoadFileFromDynamicFileDescriptionResp LoadFileFromDynamicFileDescription(LoadFileFromDynamicFileDescriptionReq Request)
		{
			return base.Channel.LoadFileFromDynamicFileDescription(Request);
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x0001F0AC File Offset: 0x0001D2AC
		public LoadStudentFileDescriptionsResp LoadStudentFileDescriptions(LoadStudentFileDescriptionsReq Request)
		{
			return base.Channel.LoadStudentFileDescriptions(Request);
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x0001F0CC File Offset: 0x0001D2CC
		public UploadStudentFileResp UploadStudentFile(UploadStudentFileReq Request)
		{
			return base.Channel.UploadStudentFile(Request);
		}
	}
}
