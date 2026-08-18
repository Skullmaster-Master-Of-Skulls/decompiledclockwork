using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentFiles;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000145 RID: 325
	public class StudentFilesQueueReusableClientProxy : WCFTokenBasedReusableClientProxy<IStudentFilesQueue>, IStudentFilesQueue, IService
	{
		// Token: 0x06000C74 RID: 3188 RVA: 0x0001F0EA File Offset: 0x0001D2EA
		public StudentFilesQueueReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x0001F0F5 File Offset: 0x0001D2F5
		public StudentFilesQueueReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x0001F104 File Offset: 0x0001D304
		public LoadStudentFilesQueueFileItemsByStudentResp LoadStudentFilesQueueFileItemsByStudent(LoadStudentFilesQueueFileItemsByStudentReq Request)
		{
			return this.WrapServiceMethod<LoadStudentFilesQueueFileItemsByStudentResp>(() => this.Proxy.LoadStudentFilesQueueFileItemsByStudent(Request));
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x0001F13C File Offset: 0x0001D33C
		public LoadStudentFilesQueueItemsResp LoadStudentFilesQueueItems(LoadStudentFilesQueueItemsReq Request)
		{
			return this.WrapServiceMethod<LoadStudentFilesQueueItemsResp>(() => this.Proxy.LoadStudentFilesQueueItems(Request));
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x0001F174 File Offset: 0x0001D374
		public UpdateStudentFilesQueueStudentItemResp UpdateStudentFilesQueueStudentItem(UpdateStudentFilesQueueStudentItemReq Request)
		{
			return this.WrapServiceMethod<UpdateStudentFilesQueueStudentItemResp>(() => this.Proxy.UpdateStudentFilesQueueStudentItem(Request));
		}
	}
}
