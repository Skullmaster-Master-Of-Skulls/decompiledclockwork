using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.Common.DAO.Impl.Vets;
using TechnoPro.Common.DAO.Vets;
using TechnoPro.Common.ICore.Vets;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Vets;

namespace TechnoPro.Common.Core.Vets
{
	// Token: 0x02000028 RID: 40
	public class VetsChapterManager : IVetsChapterManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000158 RID: 344 RVA: 0x000072F2 File Offset: 0x000054F2
		public VetsChapterManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00007304 File Offset: 0x00005504
		// (set) Token: 0x0600015A RID: 346 RVA: 0x0000730C File Offset: 0x0000550C
		public OperationContext OpContext { get; set; }

		// Token: 0x0600015B RID: 347 RVA: 0x00007318 File Offset: 0x00005518
		[DebuggerStepThrough]
		public Task<IList<VetsChapter>> GetChaptersAsync()
		{
			VetsChapterManager.<GetChaptersAsync>d__5 <GetChaptersAsync>d__ = new VetsChapterManager.<GetChaptersAsync>d__5();
			<GetChaptersAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<VetsChapter>>.Create();
			<GetChaptersAsync>d__.<>4__this = this;
			<GetChaptersAsync>d__.<>1__state = -1;
			<GetChaptersAsync>d__.<>t__builder.Start<VetsChapterManager.<GetChaptersAsync>d__5>(ref <GetChaptersAsync>d__);
			return <GetChaptersAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000735C File Offset: 0x0000555C
		public IList<VetsChapter> GetChapters()
		{
			IVetsChapterDAO vetsChapterDAO = new VetsChapterDAO(this.OpContext);
			return vetsChapterDAO.GetChapters();
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00007380 File Offset: 0x00005580
		[DebuggerStepThrough]
		public Task<bool> DeleteChapterAsync(Guid chapterId)
		{
			VetsChapterManager.<DeleteChapterAsync>d__7 <DeleteChapterAsync>d__ = new VetsChapterManager.<DeleteChapterAsync>d__7();
			<DeleteChapterAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<DeleteChapterAsync>d__.<>4__this = this;
			<DeleteChapterAsync>d__.chapterId = chapterId;
			<DeleteChapterAsync>d__.<>1__state = -1;
			<DeleteChapterAsync>d__.<>t__builder.Start<VetsChapterManager.<DeleteChapterAsync>d__7>(ref <DeleteChapterAsync>d__);
			return <DeleteChapterAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x000073CC File Offset: 0x000055CC
		[DebuggerStepThrough]
		public Task ChangeChapterDisabled(Guid ChapterId, bool newIsDisabled)
		{
			VetsChapterManager.<ChangeChapterDisabled>d__8 <ChangeChapterDisabled>d__ = new VetsChapterManager.<ChangeChapterDisabled>d__8();
			<ChangeChapterDisabled>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ChangeChapterDisabled>d__.<>4__this = this;
			<ChangeChapterDisabled>d__.ChapterId = ChapterId;
			<ChangeChapterDisabled>d__.newIsDisabled = newIsDisabled;
			<ChangeChapterDisabled>d__.<>1__state = -1;
			<ChangeChapterDisabled>d__.<>t__builder.Start<VetsChapterManager.<ChangeChapterDisabled>d__8>(ref <ChangeChapterDisabled>d__);
			return <ChangeChapterDisabled>d__.<>t__builder.Task;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00007420 File Offset: 0x00005620
		[DebuggerStepThrough]
		public Task UpdateChapterAsync(VetsChapter chapter)
		{
			VetsChapterManager.<UpdateChapterAsync>d__9 <UpdateChapterAsync>d__ = new VetsChapterManager.<UpdateChapterAsync>d__9();
			<UpdateChapterAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<UpdateChapterAsync>d__.<>4__this = this;
			<UpdateChapterAsync>d__.chapter = chapter;
			<UpdateChapterAsync>d__.<>1__state = -1;
			<UpdateChapterAsync>d__.<>t__builder.Start<VetsChapterManager.<UpdateChapterAsync>d__9>(ref <UpdateChapterAsync>d__);
			return <UpdateChapterAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000746C File Offset: 0x0000566C
		[DebuggerStepThrough]
		public Task<Guid> CreateChapterAsync(VetsChapter chapter)
		{
			VetsChapterManager.<CreateChapterAsync>d__10 <CreateChapterAsync>d__ = new VetsChapterManager.<CreateChapterAsync>d__10();
			<CreateChapterAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Guid>.Create();
			<CreateChapterAsync>d__.<>4__this = this;
			<CreateChapterAsync>d__.chapter = chapter;
			<CreateChapterAsync>d__.<>1__state = -1;
			<CreateChapterAsync>d__.<>t__builder.Start<VetsChapterManager.<CreateChapterAsync>d__10>(ref <CreateChapterAsync>d__);
			return <CreateChapterAsync>d__.<>t__builder.Task;
		}
	}
}
