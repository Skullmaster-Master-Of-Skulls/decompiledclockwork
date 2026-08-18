using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Vets;
using TechnoPro.Common.Core.Mappers.Templates.Vets;
using TechnoPro.Common.Core.Vets;
using TechnoPro.Common.ICore.Vets;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Vets;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x020000A2 RID: 162
	public class VetsChapterServiceManager : IVetsChapter, IService
	{
		// Token: 0x060005E6 RID: 1510 RVA: 0x0001B500 File Offset: 0x00019700
		[DebuggerStepThrough]
		public Task<GetChaptersResp> GetChaptersAsync(GetChaptersReq Request)
		{
			VetsChapterServiceManager.<GetChaptersAsync>d__0 <GetChaptersAsync>d__ = new VetsChapterServiceManager.<GetChaptersAsync>d__0();
			<GetChaptersAsync>d__.<>t__builder = AsyncTaskMethodBuilder<GetChaptersResp>.Create();
			<GetChaptersAsync>d__.<>4__this = this;
			<GetChaptersAsync>d__.Request = Request;
			<GetChaptersAsync>d__.<>1__state = -1;
			<GetChaptersAsync>d__.<>t__builder.Start<VetsChapterServiceManager.<GetChaptersAsync>d__0>(ref <GetChaptersAsync>d__);
			return <GetChaptersAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0001B54C File Offset: 0x0001974C
		public GetChaptersResp GetChapters(GetChaptersReq Request)
		{
			IVetsChapterManager vetsChapterManager = new VetsChapterManager(new OperationContext
			{
				WhoAmI = Request.WhoAmI
			});
			IList<VetsChapter> chapters = vetsChapterManager.GetChapters();
			GetChaptersResp getChaptersResp = new GetChaptersResp();
			IList<VetsChapterDTO> chapters2;
			if (chapters == null)
			{
				chapters2 = null;
			}
			else
			{
				chapters2 = (from g in chapters
				select g.ToDTO()).ToList<VetsChapterDTO>();
			}
			getChaptersResp.Chapters = chapters2;
			return getChaptersResp;
		}
	}
}
