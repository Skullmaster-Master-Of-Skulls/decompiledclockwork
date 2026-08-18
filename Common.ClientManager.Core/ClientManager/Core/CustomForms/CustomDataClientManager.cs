using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Data;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Data.Context;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.CustomForms;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.CustomForms
{
	// Token: 0x0200006F RID: 111
	public class CustomDataClientManager : ICustomDataClientManager, IWebService
	{
		// Token: 0x06000410 RID: 1040 RVA: 0x00012450 File Offset: 0x00010650
		[DebuggerStepThrough]
		public Task<CustomDataSetDTO> LoadDataAsync(CustomDataContextDTO context, IList<Guid> dataInstanceIds)
		{
			CustomDataClientManager.<LoadDataAsync>d__0 <LoadDataAsync>d__ = new CustomDataClientManager.<LoadDataAsync>d__0();
			<LoadDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder<CustomDataSetDTO>.Create();
			<LoadDataAsync>d__.<>4__this = this;
			<LoadDataAsync>d__.context = context;
			<LoadDataAsync>d__.dataInstanceIds = dataInstanceIds;
			<LoadDataAsync>d__.<>1__state = -1;
			<LoadDataAsync>d__.<>t__builder.Start<CustomDataClientManager.<LoadDataAsync>d__0>(ref <LoadDataAsync>d__);
			return <LoadDataAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x000124A4 File Offset: 0x000106A4
		public CustomDataSetDTO LoadData(CustomDataContextDTO context, IList<Guid> dataInstanceIds)
		{
			LoadCustomDataReq loadCustomDataReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadCustomDataReq>();
			loadCustomDataReq.Context = context;
			loadCustomDataReq.DataInstanceIds = ((dataInstanceIds != null) ? dataInstanceIds.ToArray<Guid>() : null);
			return ClientServiceFactory.GetClientInstance<ICustomData>().LoadCustomData(loadCustomDataReq).DataSet;
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x000124EC File Offset: 0x000106EC
		[DebuggerStepThrough]
		public Task SaveCustomFormsDataAsync(CustomDataSetDTO dataSet, IList<Guid> dataInstanceIds)
		{
			CustomDataClientManager.<SaveCustomFormsDataAsync>d__2 <SaveCustomFormsDataAsync>d__ = new CustomDataClientManager.<SaveCustomFormsDataAsync>d__2();
			<SaveCustomFormsDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SaveCustomFormsDataAsync>d__.<>4__this = this;
			<SaveCustomFormsDataAsync>d__.dataSet = dataSet;
			<SaveCustomFormsDataAsync>d__.dataInstanceIds = dataInstanceIds;
			<SaveCustomFormsDataAsync>d__.<>1__state = -1;
			<SaveCustomFormsDataAsync>d__.<>t__builder.Start<CustomDataClientManager.<SaveCustomFormsDataAsync>d__2>(ref <SaveCustomFormsDataAsync>d__);
			return <SaveCustomFormsDataAsync>d__.<>t__builder.Task;
		}
	}
}
