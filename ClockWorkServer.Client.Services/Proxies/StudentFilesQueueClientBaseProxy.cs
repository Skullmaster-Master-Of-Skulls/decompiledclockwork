using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentFiles;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000146 RID: 326
	internal class StudentFilesQueueClientBaseProxy : ClientBase<IStudentFilesQueue>, IStudentFilesQueue, IService
	{
		// Token: 0x06000C79 RID: 3193 RVA: 0x0001F1AC File Offset: 0x0001D3AC
		public StudentFilesQueueClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x0001F1B7 File Offset: 0x0001D3B7
		public StudentFilesQueueClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x0001F1C4 File Offset: 0x0001D3C4
		public LoadStudentFilesQueueFileItemsByStudentResp LoadStudentFilesQueueFileItemsByStudent(LoadStudentFilesQueueFileItemsByStudentReq Request)
		{
			return base.Channel.LoadStudentFilesQueueFileItemsByStudent(Request);
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x0001F1E4 File Offset: 0x0001D3E4
		public LoadStudentFilesQueueItemsResp LoadStudentFilesQueueItems(LoadStudentFilesQueueItemsReq Request)
		{
			return base.Channel.LoadStudentFilesQueueItems(Request);
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x0001F204 File Offset: 0x0001D404
		public UpdateStudentFilesQueueStudentItemResp UpdateStudentFilesQueueStudentItem(UpdateStudentFilesQueueStudentItemReq Request)
		{
			return base.Channel.UpdateStudentFilesQueueStudentItem(Request);
		}
	}
}
