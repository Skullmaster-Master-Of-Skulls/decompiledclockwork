using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.UnivDataAccess;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000158 RID: 344
	internal class UnivDataAccessClientBaseProxy : ClientBase<IUnivDataAccess>, IUnivDataAccess, IService
	{
		// Token: 0x06000D3D RID: 3389 RVA: 0x00020E0C File Offset: 0x0001F00C
		public UnivDataAccessClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x00020E17 File Offset: 0x0001F017
		public UnivDataAccessClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x00020E24 File Offset: 0x0001F024
		public DoesColumnExistResp DoesColumnExist(DoesColumnExistReq Request)
		{
			return base.Channel.DoesColumnExist(Request);
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x00020E44 File Offset: 0x0001F044
		public DoesTableExistResp DoesTableExist(DoesTableExistReq Request)
		{
			return base.Channel.DoesTableExist(Request);
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x00020E64 File Offset: 0x0001F064
		public ExecuteNonQueryResp ExecuteNonQuery(ExecuteNonQueryReq Request)
		{
			return base.Channel.ExecuteNonQuery(Request);
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x00020E84 File Offset: 0x0001F084
		public ExecuteScalarResp ExecuteScalar(ExecuteScalarReq Request)
		{
			return base.Channel.ExecuteScalar(Request);
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x00020EA4 File Offset: 0x0001F0A4
		public FillResp Fill(FillReq Request)
		{
			return base.Channel.Fill(Request);
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x00020EC4 File Offset: 0x0001F0C4
		public FillReturnIdentityResp FillReturnIdentity(FillReturnIdentityReq Request)
		{
			return base.Channel.FillReturnIdentity(Request);
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x00020EE4 File Offset: 0x0001F0E4
		public GetSQLCommandParametersFilledInResp GetSQLCommandParametersFilledIn(GetSQLCommandParametersFilledInReq Request)
		{
			return base.Channel.GetSQLCommandParametersFilledIn(Request);
		}
	}
}
