using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000BF RID: 191
	public class InventoryReservationReusableClientProxy : WCFTokenBasedReusableClientProxy<IInventoryReservation>, IInventoryReservation, IService
	{
		// Token: 0x060007A3 RID: 1955 RVA: 0x0001438A File Offset: 0x0001258A
		public InventoryReservationReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x00014395 File Offset: 0x00012595
		public InventoryReservationReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x000143A4 File Offset: 0x000125A4
		public GetReservationByIdResp GetReservationById(GetReservationByIdReq request)
		{
			return this.WrapServiceMethod<GetReservationByIdResp>(() => this.Proxy.GetReservationById(request));
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x000143DC File Offset: 0x000125DC
		public GetReservationsByProductResp GetReservationsByProduct(GetReservationsByProductReq request)
		{
			return this.WrapServiceMethod<GetReservationsByProductResp>(() => this.Proxy.GetReservationsByProduct(request));
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x00014414 File Offset: 0x00012614
		public GetReservationsByProductInDateRangeResp GetReservationsByProductInDateRange(GetReservationsByProductInDateRangeReq request)
		{
			return this.WrapServiceMethod<GetReservationsByProductInDateRangeResp>(() => this.Proxy.GetReservationsByProductInDateRange(request));
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x0001444C File Offset: 0x0001264C
		public GetReservationsByWhoMadeItResp GetReservationsByWhoMadeIt(GetReservationsByWhoMadeItReq request)
		{
			return this.WrapServiceMethod<GetReservationsByWhoMadeItResp>(() => this.Proxy.GetReservationsByWhoMadeIt(request));
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x00014484 File Offset: 0x00012684
		public GetReservationsResp GetReservations(GetReservationsReq request)
		{
			return this.WrapServiceMethod<GetReservationsResp>(() => this.Proxy.GetReservations(request));
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x000144BC File Offset: 0x000126BC
		public GetReservationsByWhoMadeItInDateRangeResp GetReservationsByWhoMadeItInDateRange(GetReservationsByWhoMadeItInDateRangeReq request)
		{
			return this.WrapServiceMethod<GetReservationsByWhoMadeItInDateRangeResp>(() => this.Proxy.GetReservationsByWhoMadeItInDateRange(request));
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x000144F4 File Offset: 0x000126F4
		public GetNextReservationAfterDateByProductResp GetNextReservationAfterDateByProduct(GetNextReservationAfterDateByProductReq request)
		{
			return this.WrapServiceMethod<GetNextReservationAfterDateByProductResp>(() => this.Proxy.GetNextReservationAfterDateByProduct(request));
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0001452C File Offset: 0x0001272C
		public MakeReservationResp MakeReservation(MakeReservationReq request)
		{
			return this.WrapServiceMethod<MakeReservationResp>(() => this.Proxy.MakeReservation(request));
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x00014564 File Offset: 0x00012764
		public MarkReservationAsCompletedResp MarkReservationAsCompleted(MarkReservationAsCompletedReq request)
		{
			return this.WrapServiceMethod<MarkReservationAsCompletedResp>(() => this.Proxy.MarkReservationAsCompleted(request));
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x0001459C File Offset: 0x0001279C
		public CancelReservationResp CancelReservation(CancelReservationReq request)
		{
			return this.WrapServiceMethod<CancelReservationResp>(() => this.Proxy.CancelReservation(request));
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x000145D4 File Offset: 0x000127D4
		public CancelReservationGroupResp CancelReservationGroup(CancelReservationGroupReq request)
		{
			return this.WrapServiceMethod<CancelReservationGroupResp>(() => this.Proxy.CancelReservationGroup(request));
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x0001460C File Offset: 0x0001280C
		public UpdateReservationResp UpdateReservation(UpdateReservationReq request)
		{
			return this.WrapServiceMethod<UpdateReservationResp>(() => this.Proxy.UpdateReservation(request));
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x00014644 File Offset: 0x00012844
		public UpdateReservationGroupResp UpdateReservationGroup(UpdateReservationGroupReq request)
		{
			return this.WrapServiceMethod<UpdateReservationGroupResp>(() => this.Proxy.UpdateReservationGroup(request));
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x0001467C File Offset: 0x0001287C
		public GetReservationsByReservationGroupIdResp GetReservationsByReservationGroupId(GetReservationsByReservationGroupIdReq request)
		{
			return this.WrapServiceMethod<GetReservationsByReservationGroupIdResp>(() => this.Proxy.GetReservationsByReservationGroupId(request));
		}
	}
}
