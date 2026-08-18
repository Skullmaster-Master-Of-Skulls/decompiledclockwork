using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Core.Inventory;
using TechnoPro.Common.Core.Mappers.Inventory;
using TechnoPro.Common.ICore.Inventory;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000056 RID: 86
	public class InventoryReservationServiceManager : IInventoryReservation, IService
	{
		// Token: 0x06000343 RID: 835 RVA: 0x0000F800 File Offset: 0x0000DA00
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000F814 File Offset: 0x0000DA14
		public GetReservationByIdResp GetReservationById(GetReservationByIdReq request)
		{
			IInventoryReservationManager inventoryReservationManager = new InventoryReservationManager(request.GetOperationContext());
			return new GetReservationByIdResp
			{
				Reservation = inventoryReservationManager.GetReservationById(request.ReservationId).ToDTO()
			};
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000F850 File Offset: 0x0000DA50
		public GetReservationsByProductResp GetReservationsByProduct(GetReservationsByProductReq request)
		{
			IInventoryReservationManager inventoryReservationManager = new InventoryReservationManager(request.GetOperationContext());
			return new GetReservationsByProductResp
			{
				Reservations = inventoryReservationManager.GetReservationsByProduct(new Guid(request.ProductUniqueId)).ToDTO()
			};
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000F890 File Offset: 0x0000DA90
		public GetReservationsByProductInDateRangeResp GetReservationsByProductInDateRange(GetReservationsByProductInDateRangeReq request)
		{
			IInventoryReservationManager inventoryReservationManager = new InventoryReservationManager(request.GetOperationContext());
			IList<InventoryReservation> list = string.IsNullOrEmpty(request.ProductUniqueId) ? inventoryReservationManager.GetReservationsByProduct(request.AlternateProductId, request.StartDate, request.EndDate) : inventoryReservationManager.GetReservationsByProduct(new Guid(request.ProductUniqueId), request.StartDate, request.EndDate);
			return new GetReservationsByProductInDateRangeResp
			{
				Reservations = list.ToDTO()
			};
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000F908 File Offset: 0x0000DB08
		public GetReservationsByWhoMadeItResp GetReservationsByWhoMadeIt(GetReservationsByWhoMadeItReq request)
		{
			IInventoryReservationManager inventoryReservationManager = new InventoryReservationManager(request.GetOperationContext());
			return new GetReservationsByWhoMadeItResp
			{
				Reservations = inventoryReservationManager.GetReservationsByWhoMadeIt(request.WhoMadeReservationId).ToDTO()
			};
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000F944 File Offset: 0x0000DB44
		public GetReservationsResp GetReservations(GetReservationsReq request)
		{
			IInventoryReservationManager inventoryReservationManager = new InventoryReservationManager(request.GetOperationContext());
			return new GetReservationsResp
			{
				Reservations = inventoryReservationManager.GetReservations(request.StartDate, request.EndDate).ToDTO()
			};
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000F988 File Offset: 0x0000DB88
		public GetReservationsByWhoMadeItInDateRangeResp GetReservationsByWhoMadeItInDateRange(GetReservationsByWhoMadeItInDateRangeReq request)
		{
			IInventoryReservationManager inventoryReservationManager = new InventoryReservationManager(request.GetOperationContext());
			return new GetReservationsByWhoMadeItInDateRangeResp
			{
				Reservations = inventoryReservationManager.GetReservationsByWhoMadeIt(request.WhoMadeReservationId, request.StartDate, request.EndDate).ToDTO()
			};
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000F9D0 File Offset: 0x0000DBD0
		public GetNextReservationAfterDateByProductResp GetNextReservationAfterDateByProduct(GetNextReservationAfterDateByProductReq request)
		{
			IInventoryReservationManager inventoryReservationManager = new InventoryReservationManager(request.GetOperationContext());
			return new GetNextReservationAfterDateByProductResp
			{
				Reservation = inventoryReservationManager.GetNextReservationAfterDateByProduct(new Guid(request.ProductUniqueId), request.Date).ToDTO()
			};
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000FA18 File Offset: 0x0000DC18
		public MakeReservationResp MakeReservation(MakeReservationReq request)
		{
			IInventoryReservationManager inventoryReservationManager = new InventoryReservationManager(request.GetOperationContext());
			MakeReservationResp makeReservationResp = new MakeReservationResp();
			makeReservationResp.ReservationId = inventoryReservationManager.MakeReservation(request.ReservationGroup.ToDomainObject(), (from pId in request.ReservedProductUniqueIds
			select new Guid(pId)).ToArray<Guid>());
			return makeReservationResp;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000FA84 File Offset: 0x0000DC84
		public MarkReservationAsCompletedResp MarkReservationAsCompleted(MarkReservationAsCompletedReq request)
		{
			IInventoryReservationManager inventoryReservationManager = new InventoryReservationManager(request.GetOperationContext());
			inventoryReservationManager.MarkReservationAsCompleted(request.ReservationId);
			return new MarkReservationAsCompletedResp();
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000FAB4 File Offset: 0x0000DCB4
		public CancelReservationResp CancelReservation(CancelReservationReq request)
		{
			IInventoryReservationManager inventoryReservationManager = new InventoryReservationManager(request.GetOperationContext());
			inventoryReservationManager.CancelReservation(request.ReservationId);
			return new CancelReservationResp();
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000FAE4 File Offset: 0x0000DCE4
		public CancelReservationGroupResp CancelReservationGroup(CancelReservationGroupReq request)
		{
			IInventoryReservationManager inventoryReservationManager = new InventoryReservationManager(request.GetOperationContext());
			inventoryReservationManager.CancelReservationGroup(request.ReservationGroupId);
			return new CancelReservationGroupResp();
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000FB14 File Offset: 0x0000DD14
		public UpdateReservationResp UpdateReservation(UpdateReservationReq request)
		{
			IInventoryReservationManager inventoryReservationManager = new InventoryReservationManager(request.GetOperationContext());
			return new UpdateReservationResp
			{
				ReservationGroupId = inventoryReservationManager.UpdateReservation(request.Reservation.ToDomainObject())
			};
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000FB50 File Offset: 0x0000DD50
		public UpdateReservationGroupResp UpdateReservationGroup(UpdateReservationGroupReq request)
		{
			IInventoryReservationManager inventoryReservationManager = new InventoryReservationManager(request.GetOperationContext());
			inventoryReservationManager.UpdateReservationGroup(request.ReservationGroup.ToDomainObject());
			return new UpdateReservationGroupResp();
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000FB88 File Offset: 0x0000DD88
		public GetReservationsByReservationGroupIdResp GetReservationsByReservationGroupId(GetReservationsByReservationGroupIdReq request)
		{
			IInventoryReservationManager inventoryReservationManager = new InventoryReservationManager(request.GetOperationContext());
			return new GetReservationsByReservationGroupIdResp
			{
				Reservations = inventoryReservationManager.GetReservationsByReservationGroupId(request.ReservationGroupId).ToDTO()
			};
		}
	}
}
