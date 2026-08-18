using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.Common.DAO.CustomForms;
using TechnoPro.Common.DAO.Impl.CustomForms;
using TechnoPro.Common.ICore.CustomForms;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.CustomForms.Form;

namespace TechnoPro.Common.Core.CustomForms
{
	// Token: 0x02000117 RID: 279
	public class CustomFormManager : ICustomFormManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000BBC RID: 3004 RVA: 0x000532D6 File Offset: 0x000514D6
		public CustomFormManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000BBD RID: 3005 RVA: 0x000532E8 File Offset: 0x000514E8
		// (set) Token: 0x06000BBE RID: 3006 RVA: 0x000532F0 File Offset: 0x000514F0
		public OperationContext OpContext { get; set; }

		// Token: 0x06000BBF RID: 3007 RVA: 0x000532FC File Offset: 0x000514FC
		[DebuggerStepThrough]
		public Task<IList<CustomForm>> LoadAllCustomForms()
		{
			CustomFormManager.<LoadAllCustomForms>d__5 <LoadAllCustomForms>d__ = new CustomFormManager.<LoadAllCustomForms>d__5();
			<LoadAllCustomForms>d__.<>t__builder = AsyncTaskMethodBuilder<IList<CustomForm>>.Create();
			<LoadAllCustomForms>d__.<>4__this = this;
			<LoadAllCustomForms>d__.<>1__state = -1;
			<LoadAllCustomForms>d__.<>t__builder.Start<CustomFormManager.<LoadAllCustomForms>d__5>(ref <LoadAllCustomForms>d__);
			return <LoadAllCustomForms>d__.<>t__builder.Task;
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x00053340 File Offset: 0x00051540
		[DebuggerStepThrough]
		public Task<CustomForm> LoadFormByIdAsync(Guid formId)
		{
			CustomFormManager.<LoadFormByIdAsync>d__6 <LoadFormByIdAsync>d__ = new CustomFormManager.<LoadFormByIdAsync>d__6();
			<LoadFormByIdAsync>d__.<>t__builder = AsyncTaskMethodBuilder<CustomForm>.Create();
			<LoadFormByIdAsync>d__.<>4__this = this;
			<LoadFormByIdAsync>d__.formId = formId;
			<LoadFormByIdAsync>d__.<>1__state = -1;
			<LoadFormByIdAsync>d__.<>t__builder.Start<CustomFormManager.<LoadFormByIdAsync>d__6>(ref <LoadFormByIdAsync>d__);
			return <LoadFormByIdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x0005338C File Offset: 0x0005158C
		public CustomForm LoadFormById(Guid formId)
		{
			ICustomFormDAO customFormDAO = new CustomFormDAO(this.OpContext);
			return customFormDAO.LoadFormById(formId);
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x000533B4 File Offset: 0x000515B4
		[DebuggerStepThrough]
		public Task<Guid> CreateFormAsync(CustomForm form)
		{
			CustomFormManager.<CreateFormAsync>d__8 <CreateFormAsync>d__ = new CustomFormManager.<CreateFormAsync>d__8();
			<CreateFormAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Guid>.Create();
			<CreateFormAsync>d__.<>4__this = this;
			<CreateFormAsync>d__.form = form;
			<CreateFormAsync>d__.<>1__state = -1;
			<CreateFormAsync>d__.<>t__builder.Start<CustomFormManager.<CreateFormAsync>d__8>(ref <CreateFormAsync>d__);
			return <CreateFormAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x00053400 File Offset: 0x00051600
		[DebuggerStepThrough]
		public Task DeleteFormAsync(Guid formId)
		{
			CustomFormManager.<DeleteFormAsync>d__9 <DeleteFormAsync>d__ = new CustomFormManager.<DeleteFormAsync>d__9();
			<DeleteFormAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DeleteFormAsync>d__.<>4__this = this;
			<DeleteFormAsync>d__.formId = formId;
			<DeleteFormAsync>d__.<>1__state = -1;
			<DeleteFormAsync>d__.<>t__builder.Start<CustomFormManager.<DeleteFormAsync>d__9>(ref <DeleteFormAsync>d__);
			return <DeleteFormAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x0005344C File Offset: 0x0005164C
		[DebuggerStepThrough]
		public Task UpdateFormAsync(CustomForm form)
		{
			CustomFormManager.<UpdateFormAsync>d__10 <UpdateFormAsync>d__ = new CustomFormManager.<UpdateFormAsync>d__10();
			<UpdateFormAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<UpdateFormAsync>d__.<>4__this = this;
			<UpdateFormAsync>d__.form = form;
			<UpdateFormAsync>d__.<>1__state = -1;
			<UpdateFormAsync>d__.<>t__builder.Start<CustomFormManager.<UpdateFormAsync>d__10>(ref <UpdateFormAsync>d__);
			return <UpdateFormAsync>d__.<>t__builder.Task;
		}
	}
}
