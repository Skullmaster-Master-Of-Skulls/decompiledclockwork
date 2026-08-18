using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Vets;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000166 RID: 358
	internal class VetsChapterClientBaseProxy : ClientBase<IVetsChapter>, IVetsChapter, IService
	{
		// Token: 0x06000DC3 RID: 3523 RVA: 0x000222C8 File Offset: 0x000204C8
		public VetsChapterClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x000222D3 File Offset: 0x000204D3
		public VetsChapterClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x000222E0 File Offset: 0x000204E0
		[DebuggerStepThrough]
		public Task<GetChaptersResp> GetChaptersAsync(GetChaptersReq Request)
		{
			VetsChapterClientBaseProxy.<GetChaptersAsync>d__2 <GetChaptersAsync>d__ = new VetsChapterClientBaseProxy.<GetChaptersAsync>d__2();
			<GetChaptersAsync>d__.<>t__builder = AsyncTaskMethodBuilder<GetChaptersResp>.Create();
			<GetChaptersAsync>d__.<>4__this = this;
			<GetChaptersAsync>d__.Request = Request;
			<GetChaptersAsync>d__.<>1__state = -1;
			<GetChaptersAsync>d__.<>t__builder.Start<VetsChapterClientBaseProxy.<GetChaptersAsync>d__2>(ref <GetChaptersAsync>d__);
			return <GetChaptersAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x0002232C File Offset: 0x0002052C
		public GetChaptersResp GetChapters(GetChaptersReq Request)
		{
			return base.Channel.GetChapters(Request);
		}
	}
}
