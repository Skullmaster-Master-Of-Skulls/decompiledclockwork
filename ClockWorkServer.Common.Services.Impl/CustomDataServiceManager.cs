using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Data;
using TechnoPro.Common.Core.CustomForms;
using TechnoPro.Common.Core.Mappers.CustomForms.Data;
using TechnoPro.Common.Core.Mappers.CustomForms.Data.Context;
using TechnoPro.Common.ICore.CustomForms;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.CustomForms.Data;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000031 RID: 49
	public class CustomDataServiceManager : ICustomData, IService
	{
		// Token: 0x060001FE RID: 510 RVA: 0x00009E98 File Offset: 0x00008098
		[DebuggerStepThrough]
		public Task<LoadCustomDataResp> LoadCustomDataAsync(LoadCustomDataReq Request)
		{
			CustomDataServiceManager.<LoadCustomDataAsync>d__0 <LoadCustomDataAsync>d__ = new CustomDataServiceManager.<LoadCustomDataAsync>d__0();
			<LoadCustomDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder<LoadCustomDataResp>.Create();
			<LoadCustomDataAsync>d__.<>4__this = this;
			<LoadCustomDataAsync>d__.Request = Request;
			<LoadCustomDataAsync>d__.<>1__state = -1;
			<LoadCustomDataAsync>d__.<>t__builder.Start<CustomDataServiceManager.<LoadCustomDataAsync>d__0>(ref <LoadCustomDataAsync>d__);
			return <LoadCustomDataAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00009EE4 File Offset: 0x000080E4
		public LoadCustomDataResp LoadCustomData(LoadCustomDataReq Request)
		{
			ICustomDataManager customDataManager = new CustomDataManager(Request.GetOperationContext());
			CustomDataSet customDataSet = customDataManager.LoadData(Request.Context.ToDomainObject(), Request.DataInstanceIds.ToArray<Guid>());
			return new LoadCustomDataResp
			{
				DataSet = ((customDataSet != null) ? customDataSet.ToDTO() : null)
			};
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00009F38 File Offset: 0x00008138
		[DebuggerStepThrough]
		public Task<SaveCustomFormsDataResp> SaveCustomFormsDataAsync(SaveCustomFormsDataReq Request)
		{
			CustomDataServiceManager.<SaveCustomFormsDataAsync>d__2 <SaveCustomFormsDataAsync>d__ = new CustomDataServiceManager.<SaveCustomFormsDataAsync>d__2();
			<SaveCustomFormsDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder<SaveCustomFormsDataResp>.Create();
			<SaveCustomFormsDataAsync>d__.<>4__this = this;
			<SaveCustomFormsDataAsync>d__.Request = Request;
			<SaveCustomFormsDataAsync>d__.<>1__state = -1;
			<SaveCustomFormsDataAsync>d__.<>t__builder.Start<CustomDataServiceManager.<SaveCustomFormsDataAsync>d__2>(ref <SaveCustomFormsDataAsync>d__);
			return <SaveCustomFormsDataAsync>d__.<>t__builder.Task;
		}
	}
}
