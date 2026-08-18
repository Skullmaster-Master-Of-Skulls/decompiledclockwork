using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentFiles;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000143 RID: 323
	public class StudentFileReusableClientProxy : WCFTokenBasedReusableClientProxy<IStudentFile>, IStudentFile, IService
	{
		// Token: 0x06000C6A RID: 3178 RVA: 0x0001EFB2 File Offset: 0x0001D1B2
		public StudentFileReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x0001EFBD File Offset: 0x0001D1BD
		public StudentFileReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x0001EFCC File Offset: 0x0001D1CC
		public LoadFileFromDynamicFileDescriptionResp LoadFileFromDynamicFileDescription(LoadFileFromDynamicFileDescriptionReq Request)
		{
			return this.WrapServiceMethod<LoadFileFromDynamicFileDescriptionResp>(() => this.Proxy.LoadFileFromDynamicFileDescription(Request));
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x0001F004 File Offset: 0x0001D204
		public LoadStudentFileDescriptionsResp LoadStudentFileDescriptions(LoadStudentFileDescriptionsReq Request)
		{
			return this.WrapServiceMethod<LoadStudentFileDescriptionsResp>(() => this.Proxy.LoadStudentFileDescriptions(Request));
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x0001F03C File Offset: 0x0001D23C
		public UploadStudentFileResp UploadStudentFile(UploadStudentFileReq Request)
		{
			return this.WrapServiceMethod<UploadStudentFileResp>(() => this.Proxy.UploadStudentFile(Request));
		}
	}
}
