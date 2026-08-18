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
	// Token: 0x02000165 RID: 357
	public class VetsChapterReusableClientProxy : WCFTokenBasedReusableClientProxy<IVetsChapter>, IVetsChapter, IService
	{
		// Token: 0x06000DBF RID: 3519 RVA: 0x0002222B File Offset: 0x0002042B
		public VetsChapterReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x00022236 File Offset: 0x00020436
		public VetsChapterReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x00022244 File Offset: 0x00020444
		[DebuggerStepThrough]
		public Task<GetChaptersResp> GetChaptersAsync(GetChaptersReq Request)
		{
			VetsChapterReusableClientProxy.<GetChaptersAsync>d__2 <GetChaptersAsync>d__ = new VetsChapterReusableClientProxy.<GetChaptersAsync>d__2();
			<GetChaptersAsync>d__.<>t__builder = AsyncTaskMethodBuilder<GetChaptersResp>.Create();
			<GetChaptersAsync>d__.<>4__this = this;
			<GetChaptersAsync>d__.Request = Request;
			<GetChaptersAsync>d__.<>1__state = -1;
			<GetChaptersAsync>d__.<>t__builder.Start<VetsChapterReusableClientProxy.<GetChaptersAsync>d__2>(ref <GetChaptersAsync>d__);
			return <GetChaptersAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x00022290 File Offset: 0x00020490
		public GetChaptersResp GetChapters(GetChaptersReq Request)
		{
			return this.WrapServiceMethod<GetChaptersResp>(() => this.Proxy.GetChapters(Request));
		}
	}
}
