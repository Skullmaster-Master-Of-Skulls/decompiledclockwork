using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.Common.DAO.Impl.OnlineForms;
using TechnoPro.Common.DAO.OnlineForms;
using TechnoPro.Common.ICore.OnlineForms;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.OnlineForms;

namespace TechnoPro.Common.Core.OnlineForms
{
	// Token: 0x020000AB RID: 171
	public class OnlineFormManager : IOnlineFormManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x00024C3E File Offset: 0x00022E3E
		// (set) Token: 0x06000641 RID: 1601 RVA: 0x00024C46 File Offset: 0x00022E46
		public IOnlineFormDAO dao { get; set; }

		// Token: 0x06000642 RID: 1602 RVA: 0x00024C4F File Offset: 0x00022E4F
		public OnlineFormManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new OnlineFormDAO(opContext);
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000643 RID: 1603 RVA: 0x00024C6E File Offset: 0x00022E6E
		// (set) Token: 0x06000644 RID: 1604 RVA: 0x00024C76 File Offset: 0x00022E76
		public OperationContext OpContext { get; set; }

		// Token: 0x06000645 RID: 1605 RVA: 0x00024C80 File Offset: 0x00022E80
		public List<OnlineForm> GetAllOnlineForms()
		{
			return this.dao.GetAllOnlineForms();
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x00024CA0 File Offset: 0x00022EA0
		public List<OnlineForm> GetActiveOnlineForms()
		{
			return this.dao.GetActiveOnlineForms();
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x00024CC0 File Offset: 0x00022EC0
		[DebuggerStepThrough]
		public Task<List<OnlineForm>> GetActiveOnlineFormsAsync()
		{
			OnlineFormManager.<GetActiveOnlineFormsAsync>d__11 <GetActiveOnlineFormsAsync>d__ = new OnlineFormManager.<GetActiveOnlineFormsAsync>d__11();
			<GetActiveOnlineFormsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<List<OnlineForm>>.Create();
			<GetActiveOnlineFormsAsync>d__.<>4__this = this;
			<GetActiveOnlineFormsAsync>d__.<>1__state = -1;
			<GetActiveOnlineFormsAsync>d__.<>t__builder.Start<OnlineFormManager.<GetActiveOnlineFormsAsync>d__11>(ref <GetActiveOnlineFormsAsync>d__);
			return <GetActiveOnlineFormsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x00024D04 File Offset: 0x00022F04
		public OnlineForm GetOnlineForm(int OnlineFormId)
		{
			return this.dao.GetOnlineForm(OnlineFormId);
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x00024D22 File Offset: 0x00022F22
		public void DeleteOnlineForm(int OnlineFormId)
		{
			this.dao.DeleteOnlineForm(OnlineFormId);
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x00024D32 File Offset: 0x00022F32
		public void UpdateOnlineForm(OnlineForm OnlineForm)
		{
			this.dao.UpdateOnlineForm(OnlineForm);
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x00024D44 File Offset: 0x00022F44
		public int CreateOnlineForm(OnlineForm OnlineForm)
		{
			return this.dao.CreateNewOnlineForm(OnlineForm);
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x00024D62 File Offset: 0x00022F62
		public void DisableOnlineForm(int OnlineFormId)
		{
			this.dao.DisableOnlineForm(OnlineFormId);
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x00024D72 File Offset: 0x00022F72
		public void EnableOnlineForm(int OnlineFormId)
		{
			this.dao.EnableOnlineForm(OnlineFormId);
		}
	}
}
