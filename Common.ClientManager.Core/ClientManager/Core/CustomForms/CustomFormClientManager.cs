using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Controls;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Form;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.CustomForms;
using TechnoPro.Common.Converter.CustomFormControls;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.CustomForms
{
	// Token: 0x02000071 RID: 113
	public class CustomFormClientManager : ICustomFormClientManager, IWebService
	{
		// Token: 0x06000418 RID: 1048 RVA: 0x00012624 File Offset: 0x00010824
		[DebuggerStepThrough]
		public Task<CustomFormDTO> LoadFormByIdAsync(Guid formId)
		{
			CustomFormClientManager.<LoadFormByIdAsync>d__0 <LoadFormByIdAsync>d__ = new CustomFormClientManager.<LoadFormByIdAsync>d__0();
			<LoadFormByIdAsync>d__.<>t__builder = AsyncTaskMethodBuilder<CustomFormDTO>.Create();
			<LoadFormByIdAsync>d__.<>4__this = this;
			<LoadFormByIdAsync>d__.formId = formId;
			<LoadFormByIdAsync>d__.<>1__state = -1;
			<LoadFormByIdAsync>d__.<>t__builder.Start<CustomFormClientManager.<LoadFormByIdAsync>d__0>(ref <LoadFormByIdAsync>d__);
			return <LoadFormByIdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00012670 File Offset: 0x00010870
		public CustomFormDTO LoadFormById(Guid formId)
		{
			LoadFormByIdReq loadFormByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadFormByIdReq>();
			loadFormByIdReq.FormId = formId;
			return ClientServiceFactory.GetClientInstance<ICustomForm>().LoadFormById(loadFormByIdReq).Form;
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x000126A8 File Offset: 0x000108A8
		public Forest<CustomControlBaseDTO> LoadFormForestById(Guid formId)
		{
			CustomFormDTO customFormDTO = this.LoadFormById(formId);
			string formXml = ((customFormDTO != null) ? customFormDTO.Xml : null) ?? "";
			Guid guid;
			return formXml.ExtractControlForest(out guid);
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x000126E0 File Offset: 0x000108E0
		[DebuggerStepThrough]
		public Task<Forest<CustomControlBaseDTO>> LoadFormForestByIdAsync(Guid formId)
		{
			CustomFormClientManager.<LoadFormForestByIdAsync>d__3 <LoadFormForestByIdAsync>d__ = new CustomFormClientManager.<LoadFormForestByIdAsync>d__3();
			<LoadFormForestByIdAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Forest<CustomControlBaseDTO>>.Create();
			<LoadFormForestByIdAsync>d__.<>4__this = this;
			<LoadFormForestByIdAsync>d__.formId = formId;
			<LoadFormForestByIdAsync>d__.<>1__state = -1;
			<LoadFormForestByIdAsync>d__.<>t__builder.Start<CustomFormClientManager.<LoadFormForestByIdAsync>d__3>(ref <LoadFormForestByIdAsync>d__);
			return <LoadFormForestByIdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0001272C File Offset: 0x0001092C
		[DebuggerStepThrough]
		public Task<Guid> CreateFormAsync(CustomFormDTO form)
		{
			CustomFormClientManager.<CreateFormAsync>d__4 <CreateFormAsync>d__ = new CustomFormClientManager.<CreateFormAsync>d__4();
			<CreateFormAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Guid>.Create();
			<CreateFormAsync>d__.<>4__this = this;
			<CreateFormAsync>d__.form = form;
			<CreateFormAsync>d__.<>1__state = -1;
			<CreateFormAsync>d__.<>t__builder.Start<CustomFormClientManager.<CreateFormAsync>d__4>(ref <CreateFormAsync>d__);
			return <CreateFormAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00012778 File Offset: 0x00010978
		[DebuggerStepThrough]
		public Task DeleteFormAsync(Guid formId)
		{
			CustomFormClientManager.<DeleteFormAsync>d__5 <DeleteFormAsync>d__ = new CustomFormClientManager.<DeleteFormAsync>d__5();
			<DeleteFormAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DeleteFormAsync>d__.<>4__this = this;
			<DeleteFormAsync>d__.formId = formId;
			<DeleteFormAsync>d__.<>1__state = -1;
			<DeleteFormAsync>d__.<>t__builder.Start<CustomFormClientManager.<DeleteFormAsync>d__5>(ref <DeleteFormAsync>d__);
			return <DeleteFormAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x000127C4 File Offset: 0x000109C4
		[DebuggerStepThrough]
		public Task UpdateFormAsync(CustomFormDTO form)
		{
			CustomFormClientManager.<UpdateFormAsync>d__6 <UpdateFormAsync>d__ = new CustomFormClientManager.<UpdateFormAsync>d__6();
			<UpdateFormAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<UpdateFormAsync>d__.<>4__this = this;
			<UpdateFormAsync>d__.form = form;
			<UpdateFormAsync>d__.<>1__state = -1;
			<UpdateFormAsync>d__.<>t__builder.Start<CustomFormClientManager.<UpdateFormAsync>d__6>(ref <UpdateFormAsync>d__);
			return <UpdateFormAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00012810 File Offset: 0x00010A10
		[DebuggerStepThrough]
		public Task<IList<CustomFormDTO>> LoadAllCustomFormsAsync()
		{
			CustomFormClientManager.<LoadAllCustomFormsAsync>d__7 <LoadAllCustomFormsAsync>d__ = new CustomFormClientManager.<LoadAllCustomFormsAsync>d__7();
			<LoadAllCustomFormsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<CustomFormDTO>>.Create();
			<LoadAllCustomFormsAsync>d__.<>4__this = this;
			<LoadAllCustomFormsAsync>d__.<>1__state = -1;
			<LoadAllCustomFormsAsync>d__.<>t__builder.Start<CustomFormClientManager.<LoadAllCustomFormsAsync>d__7>(ref <LoadAllCustomFormsAsync>d__);
			return <LoadAllCustomFormsAsync>d__.<>t__builder.Task;
		}
	}
}
