using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.UnivDataAccess;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000157 RID: 343
	public class UnivDataAccessReusableClientProxy : WCFTokenBasedReusableClientProxy<IUnivDataAccess>, IUnivDataAccess, IService
	{
		// Token: 0x06000D34 RID: 3380 RVA: 0x00020C6A File Offset: 0x0001EE6A
		public UnivDataAccessReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x00020C75 File Offset: 0x0001EE75
		public UnivDataAccessReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x00020C84 File Offset: 0x0001EE84
		public DoesColumnExistResp DoesColumnExist(DoesColumnExistReq Request)
		{
			return this.WrapServiceMethod<DoesColumnExistResp>(() => this.Proxy.DoesColumnExist(Request));
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x00020CBC File Offset: 0x0001EEBC
		public DoesTableExistResp DoesTableExist(DoesTableExistReq Request)
		{
			return this.WrapServiceMethod<DoesTableExistResp>(() => this.Proxy.DoesTableExist(Request));
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x00020CF4 File Offset: 0x0001EEF4
		public ExecuteNonQueryResp ExecuteNonQuery(ExecuteNonQueryReq Request)
		{
			return this.WrapServiceMethod<ExecuteNonQueryResp>(() => this.Proxy.ExecuteNonQuery(Request));
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x00020D2C File Offset: 0x0001EF2C
		public ExecuteScalarResp ExecuteScalar(ExecuteScalarReq Request)
		{
			return this.WrapServiceMethod<ExecuteScalarResp>(() => this.Proxy.ExecuteScalar(Request));
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x00020D64 File Offset: 0x0001EF64
		public FillResp Fill(FillReq Request)
		{
			return this.WrapServiceMethod<FillResp>(() => this.Proxy.Fill(Request));
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x00020D9C File Offset: 0x0001EF9C
		public FillReturnIdentityResp FillReturnIdentity(FillReturnIdentityReq Request)
		{
			return this.WrapServiceMethod<FillReturnIdentityResp>(() => this.Proxy.FillReturnIdentity(Request));
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x00020DD4 File Offset: 0x0001EFD4
		public GetSQLCommandParametersFilledInResp GetSQLCommandParametersFilledIn(GetSQLCommandParametersFilledInReq Request)
		{
			return this.WrapServiceMethod<GetSQLCommandParametersFilledInResp>(() => this.Proxy.GetSQLCommandParametersFilledIn(Request));
		}
	}
}
