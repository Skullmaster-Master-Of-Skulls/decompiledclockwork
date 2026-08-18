using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Vets;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Vets;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.Vets
{
	// Token: 0x02000007 RID: 7
	public class VetsChapterClientManager : IVetsChapterClientManager, IWebService
	{
		// Token: 0x06000032 RID: 50 RVA: 0x00002D98 File Offset: 0x00000F98
		[DebuggerStepThrough]
		public Task<IList<VetsChapterDTO>> GetChaptersAsync()
		{
			VetsChapterClientManager.<GetChaptersAsync>d__0 <GetChaptersAsync>d__ = new VetsChapterClientManager.<GetChaptersAsync>d__0();
			<GetChaptersAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<VetsChapterDTO>>.Create();
			<GetChaptersAsync>d__.<>4__this = this;
			<GetChaptersAsync>d__.<>1__state = -1;
			<GetChaptersAsync>d__.<>t__builder.Start<VetsChapterClientManager.<GetChaptersAsync>d__0>(ref <GetChaptersAsync>d__);
			return <GetChaptersAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002DDC File Offset: 0x00000FDC
		public IList<VetsChapterDTO> GetChapters()
		{
			GetChaptersReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetChaptersReq>();
			IVetsChapter clientInstance = ClientServiceFactory.GetClientInstance<IVetsChapter>();
			return clientInstance.GetChapters(request).Chapters;
		}
	}
}
