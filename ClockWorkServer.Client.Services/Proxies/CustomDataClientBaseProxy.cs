using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Data;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200006E RID: 110
	internal class CustomDataClientBaseProxy : ClientBase<ICustomData>, ICustomData, IService
	{
		// Token: 0x060004BD RID: 1213 RVA: 0x0000D48F File Offset: 0x0000B68F
		public CustomDataClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0000D49A File Offset: 0x0000B69A
		public CustomDataClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0000D4A8 File Offset: 0x0000B6A8
		[DebuggerStepThrough]
		public Task<LoadCustomDataResp> LoadCustomDataAsync(LoadCustomDataReq Request)
		{
			CustomDataClientBaseProxy.<LoadCustomDataAsync>d__2 <LoadCustomDataAsync>d__ = new CustomDataClientBaseProxy.<LoadCustomDataAsync>d__2();
			<LoadCustomDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder<LoadCustomDataResp>.Create();
			<LoadCustomDataAsync>d__.<>4__this = this;
			<LoadCustomDataAsync>d__.Request = Request;
			<LoadCustomDataAsync>d__.<>1__state = -1;
			<LoadCustomDataAsync>d__.<>t__builder.Start<CustomDataClientBaseProxy.<LoadCustomDataAsync>d__2>(ref <LoadCustomDataAsync>d__);
			return <LoadCustomDataAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0000D4F4 File Offset: 0x0000B6F4
		public LoadCustomDataResp LoadCustomData(LoadCustomDataReq Request)
		{
			return base.Channel.LoadCustomData(Request);
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0000D514 File Offset: 0x0000B714
		[DebuggerStepThrough]
		public Task<SaveCustomFormsDataResp> SaveCustomFormsDataAsync(SaveCustomFormsDataReq Request)
		{
			CustomDataClientBaseProxy.<SaveCustomFormsDataAsync>d__4 <SaveCustomFormsDataAsync>d__ = new CustomDataClientBaseProxy.<SaveCustomFormsDataAsync>d__4();
			<SaveCustomFormsDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder<SaveCustomFormsDataResp>.Create();
			<SaveCustomFormsDataAsync>d__.<>4__this = this;
			<SaveCustomFormsDataAsync>d__.Request = Request;
			<SaveCustomFormsDataAsync>d__.<>1__state = -1;
			<SaveCustomFormsDataAsync>d__.<>t__builder.Start<CustomDataClientBaseProxy.<SaveCustomFormsDataAsync>d__4>(ref <SaveCustomFormsDataAsync>d__);
			return <SaveCustomFormsDataAsync>d__.<>t__builder.Task;
		}
	}
}
