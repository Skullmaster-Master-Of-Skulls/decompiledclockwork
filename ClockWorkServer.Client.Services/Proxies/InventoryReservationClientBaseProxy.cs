using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000C0 RID: 192
	internal class InventoryReservationClientBaseProxy : ClientBase<IInventoryReservation>, IInventoryReservation, IService
	{
		// Token: 0x060007B3 RID: 1971 RVA: 0x000146B4 File Offset: 0x000128B4
		public InventoryReservationClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x000146BF File Offset: 0x000128BF
		public InventoryReservationClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x000146CC File Offset: 0x000128CC
		public GetReservationByIdResp GetReservationById(GetReservationByIdReq request)
		{
			return base.Channel.GetReservationById(request);
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x000146EC File Offset: 0x000128EC
		public GetReservationsByProductResp GetReservationsByProduct(GetReservationsByProductReq request)
		{
			return base.Channel.GetReservationsByProduct(request);
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x0001470C File Offset: 0x0001290C
		public GetReservationsByProductInDateRangeResp GetReservationsByProductInDateRange(GetReservationsByProductInDateRangeReq request)
		{
			return base.Channel.GetReservationsByProductInDateRange(request);
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x0001472C File Offset: 0x0001292C
		public GetReservationsByWhoMadeItResp GetReservationsByWhoMadeIt(GetReservationsByWhoMadeItReq request)
		{
			return base.Channel.GetReservationsByWhoMadeIt(request);
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x0001474C File Offset: 0x0001294C
		public GetReservationsResp GetReservations(GetReservationsReq request)
		{
			return base.Channel.GetReservations(request);
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x0001476C File Offset: 0x0001296C
		public GetReservationsByWhoMadeItInDateRangeResp GetReservationsByWhoMadeItInDateRange(GetReservationsByWhoMadeItInDateRangeReq request)
		{
			return base.Channel.GetReservationsByWhoMadeItInDateRange(request);
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x0001478C File Offset: 0x0001298C
		public GetNextReservationAfterDateByProductResp GetNextReservationAfterDateByProduct(GetNextReservationAfterDateByProductReq request)
		{
			return base.Channel.GetNextReservationAfterDateByProduct(request);
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x000147AC File Offset: 0x000129AC
		public MakeReservationResp MakeReservation(MakeReservationReq request)
		{
			return base.Channel.MakeReservation(request);
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x000147CC File Offset: 0x000129CC
		public MarkReservationAsCompletedResp MarkReservationAsCompleted(MarkReservationAsCompletedReq request)
		{
			return base.Channel.MarkReservationAsCompleted(request);
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x000147EC File Offset: 0x000129EC
		public CancelReservationResp CancelReservation(CancelReservationReq request)
		{
			return base.Channel.CancelReservation(request);
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x0001480C File Offset: 0x00012A0C
		public CancelReservationGroupResp CancelReservationGroup(CancelReservationGroupReq request)
		{
			return base.Channel.CancelReservationGroup(request);
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x0001482C File Offset: 0x00012A2C
		public UpdateReservationResp UpdateReservation(UpdateReservationReq request)
		{
			return base.Channel.UpdateReservation(request);
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x0001484C File Offset: 0x00012A4C
		public UpdateReservationGroupResp UpdateReservationGroup(UpdateReservationGroupReq request)
		{
			return base.Channel.UpdateReservationGroup(request);
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x0001486C File Offset: 0x00012A6C
		public GetReservationsByReservationGroupIdResp GetReservationsByReservationGroupId(GetReservationsByReservationGroupIdReq request)
		{
			return base.Channel.GetReservationsByReservationGroupId(request);
		}
	}
}
