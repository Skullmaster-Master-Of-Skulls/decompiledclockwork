using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.OnlineForms;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.OnlineForms;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.OnlineForms
{
	// Token: 0x02000034 RID: 52
	public class OnlineFormClientManager : IOnlineFormClientManager, IWebService
	{
		// Token: 0x060001D0 RID: 464 RVA: 0x00008EC0 File Offset: 0x000070C0
		public IList<OnlineFormDTO> GetAllOnlineForms()
		{
			GetAllOnlineFormsReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetAllOnlineFormsReq>();
			return ClientServiceFactory.GetClientInstance<IOnlineForm>().GetAllOnlineForms(request).OnlineForms;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00008EF0 File Offset: 0x000070F0
		public IList<OnlineFormDTO> GetActiveOnlineForms()
		{
			GetActiveOnlineFormsReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetActiveOnlineFormsReq>();
			return ClientServiceFactory.GetClientInstance<IOnlineForm>().GetActiveOnlineForms(request).OnlineForms;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00008F20 File Offset: 0x00007120
		[DebuggerStepThrough]
		public Task<IList<OnlineFormDTO>> GetActiveOnlineFormsAsync()
		{
			OnlineFormClientManager.<GetActiveOnlineFormsAsync>d__2 <GetActiveOnlineFormsAsync>d__ = new OnlineFormClientManager.<GetActiveOnlineFormsAsync>d__2();
			<GetActiveOnlineFormsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<OnlineFormDTO>>.Create();
			<GetActiveOnlineFormsAsync>d__.<>4__this = this;
			<GetActiveOnlineFormsAsync>d__.<>1__state = -1;
			<GetActiveOnlineFormsAsync>d__.<>t__builder.Start<OnlineFormClientManager.<GetActiveOnlineFormsAsync>d__2>(ref <GetActiveOnlineFormsAsync>d__);
			return <GetActiveOnlineFormsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00008F64 File Offset: 0x00007164
		public OnlineFormDTO GetOnlineForm(int OnlineFormId)
		{
			GetOnlineFormReq getOnlineFormReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetOnlineFormReq>();
			getOnlineFormReq.OnlineFormId = OnlineFormId;
			return ClientServiceFactory.GetClientInstance<IOnlineForm>().GetOnlineForm(getOnlineFormReq).OnlineForm;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00008F9C File Offset: 0x0000719C
		public void DeleteOnlineForm(int OnlineFormId)
		{
			DeleteOnlineFormReq deleteOnlineFormReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteOnlineFormReq>();
			deleteOnlineFormReq.OnlineFormId = OnlineFormId;
			ClientServiceFactory.GetClientInstance<IOnlineForm>().DeleteOnlineForm(deleteOnlineFormReq);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00008FCC File Offset: 0x000071CC
		public void UpdateOnlineForm(OnlineFormDTO OnlineForm)
		{
			UpdateOnlineFormReq updateOnlineFormReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateOnlineFormReq>();
			updateOnlineFormReq.OnlineForm = OnlineForm;
			ClientServiceFactory.GetClientInstance<IOnlineForm>().UpdateOnlineForm(updateOnlineFormReq);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00008FFC File Offset: 0x000071FC
		public int CreateNewOnlineForm(OnlineFormDTO OnlineForm)
		{
			CreateNewOnlineFormReq createNewOnlineFormReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateNewOnlineFormReq>();
			createNewOnlineFormReq.OnlineForm = OnlineForm;
			return ClientServiceFactory.GetClientInstance<IOnlineForm>().CreateNewOnlineForm(createNewOnlineFormReq).OnlineFormId;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00009034 File Offset: 0x00007234
		public void DisableOnlineForm(int OnlineFormId)
		{
			DisableOnlineFormReq disableOnlineFormReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DisableOnlineFormReq>();
			disableOnlineFormReq.OnlineFormId = OnlineFormId;
			ClientServiceFactory.GetClientInstance<IOnlineForm>().DisableOnlineForm(disableOnlineFormReq);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00009064 File Offset: 0x00007264
		public void EnableOnlineForm(int OnlineFormId)
		{
			EnableOnlineFormReq enableOnlineFormReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<EnableOnlineFormReq>();
			enableOnlineFormReq.OnlineFormId = OnlineFormId;
			ClientServiceFactory.GetClientInstance<IOnlineForm>().EnableOnlineForm(enableOnlineFormReq);
		}
	}
}
