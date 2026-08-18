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
	// Token: 0x0200006D RID: 109
	public class CustomDataReusableClientProxy : WCFTokenBasedReusableClientProxy<ICustomData>, ICustomData, IService
	{
		// Token: 0x060004B8 RID: 1208 RVA: 0x0000D3A6 File Offset: 0x0000B5A6
		public CustomDataReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0000D3B1 File Offset: 0x0000B5B1
		public CustomDataReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0000D3C0 File Offset: 0x0000B5C0
		[DebuggerStepThrough]
		public Task<LoadCustomDataResp> LoadCustomDataAsync(LoadCustomDataReq Request)
		{
			CustomDataReusableClientProxy.<LoadCustomDataAsync>d__2 <LoadCustomDataAsync>d__ = new CustomDataReusableClientProxy.<LoadCustomDataAsync>d__2();
			<LoadCustomDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder<LoadCustomDataResp>.Create();
			<LoadCustomDataAsync>d__.<>4__this = this;
			<LoadCustomDataAsync>d__.Request = Request;
			<LoadCustomDataAsync>d__.<>1__state = -1;
			<LoadCustomDataAsync>d__.<>t__builder.Start<CustomDataReusableClientProxy.<LoadCustomDataAsync>d__2>(ref <LoadCustomDataAsync>d__);
			return <LoadCustomDataAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0000D40C File Offset: 0x0000B60C
		public LoadCustomDataResp LoadCustomData(LoadCustomDataReq Request)
		{
			return this.WrapServiceMethod<LoadCustomDataResp>(() => this.Proxy.LoadCustomData(Request));
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0000D444 File Offset: 0x0000B644
		[DebuggerStepThrough]
		public Task<SaveCustomFormsDataResp> SaveCustomFormsDataAsync(SaveCustomFormsDataReq Request)
		{
			CustomDataReusableClientProxy.<SaveCustomFormsDataAsync>d__4 <SaveCustomFormsDataAsync>d__ = new CustomDataReusableClientProxy.<SaveCustomFormsDataAsync>d__4();
			<SaveCustomFormsDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder<SaveCustomFormsDataResp>.Create();
			<SaveCustomFormsDataAsync>d__.<>4__this = this;
			<SaveCustomFormsDataAsync>d__.Request = Request;
			<SaveCustomFormsDataAsync>d__.<>1__state = -1;
			<SaveCustomFormsDataAsync>d__.<>t__builder.Start<CustomDataReusableClientProxy.<SaveCustomFormsDataAsync>d__4>(ref <SaveCustomFormsDataAsync>d__);
			return <SaveCustomFormsDataAsync>d__.<>t__builder.Task;
		}
	}
}
