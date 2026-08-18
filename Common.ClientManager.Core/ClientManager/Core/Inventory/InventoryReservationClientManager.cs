using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Inventory;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.Inventory
{
	// Token: 0x02000059 RID: 89
	public class InventoryReservationClientManager : IInventoryReservationClientManager, IWebService
	{
		// Token: 0x06000318 RID: 792 RVA: 0x0000D904 File Offset: 0x0000BB04
		public InventoryReservationDTO GetReservationById(int reservationId)
		{
			GetReservationByIdReq getReservationByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetReservationByIdReq>();
			getReservationByIdReq.ReservationId = reservationId;
			return ClientServiceFactory.GetClientInstance<IInventoryReservation>().GetReservationById(getReservationByIdReq).Reservation;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000D93C File Offset: 0x0000BB3C
		public IList<InventoryReservationDTO> GetReservationsByProduct(Guid productUniqueID)
		{
			GetReservationsByProductReq getReservationsByProductReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetReservationsByProductReq>();
			getReservationsByProductReq.ProductUniqueId = productUniqueID.ToString();
			return ClientServiceFactory.GetClientInstance<IInventoryReservation>().GetReservationsByProduct(getReservationsByProductReq).Reservations;
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000D980 File Offset: 0x0000BB80
		public IList<InventoryReservationDTO> GetReservationsByProduct(Guid productUniqueID, DateTime startDate, DateTime endDate)
		{
			GetReservationsByProductInDateRangeReq getReservationsByProductInDateRangeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetReservationsByProductInDateRangeReq>();
			getReservationsByProductInDateRangeReq.ProductUniqueId = productUniqueID.ToString();
			getReservationsByProductInDateRangeReq.StartDate = startDate;
			getReservationsByProductInDateRangeReq.EndDate = endDate;
			return ClientServiceFactory.GetClientInstance<IInventoryReservation>().GetReservationsByProductInDateRange(getReservationsByProductInDateRangeReq).Reservations;
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000D9D4 File Offset: 0x0000BBD4
		public IList<InventoryReservationDTO> GetReservationsByProduct(int productId, DateTime startDate, DateTime endDate)
		{
			GetReservationsByProductInDateRangeReq getReservationsByProductInDateRangeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetReservationsByProductInDateRangeReq>();
			getReservationsByProductInDateRangeReq.AlternateProductId = productId;
			getReservationsByProductInDateRangeReq.StartDate = startDate;
			getReservationsByProductInDateRangeReq.EndDate = endDate;
			return ClientServiceFactory.GetClientInstance<IInventoryReservation>().GetReservationsByProductInDateRange(getReservationsByProductInDateRangeReq).Reservations;
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000DA1C File Offset: 0x0000BC1C
		public IList<InventoryReservationDTO> GetReservationsByWhoMadeIt(int personId)
		{
			GetReservationsByWhoMadeItReq getReservationsByWhoMadeItReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetReservationsByWhoMadeItReq>();
			getReservationsByWhoMadeItReq.WhoMadeReservationId = personId;
			return ClientServiceFactory.GetClientInstance<IInventoryReservation>().GetReservationsByWhoMadeIt(getReservationsByWhoMadeItReq).Reservations;
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000DA54 File Offset: 0x0000BC54
		public IList<InventoryReservationDTO> GetReservationsByWhoMadeIt(int personId, DateTime startDate, DateTime endDate)
		{
			GetReservationsByWhoMadeItInDateRangeReq getReservationsByWhoMadeItInDateRangeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetReservationsByWhoMadeItInDateRangeReq>();
			getReservationsByWhoMadeItInDateRangeReq.WhoMadeReservationId = personId;
			getReservationsByWhoMadeItInDateRangeReq.StartDate = startDate;
			getReservationsByWhoMadeItInDateRangeReq.EndDate = endDate;
			return ClientServiceFactory.GetClientInstance<IInventoryReservation>().GetReservationsByWhoMadeItInDateRange(getReservationsByWhoMadeItInDateRangeReq).Reservations;
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000DA9C File Offset: 0x0000BC9C
		public IList<InventoryReservationDTO> GetReservations(DateTime startDate, DateTime endDate)
		{
			GetReservationsReq getReservationsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetReservationsReq>();
			getReservationsReq.StartDate = startDate;
			getReservationsReq.EndDate = endDate;
			return ClientServiceFactory.GetClientInstance<IInventoryReservation>().GetReservations(getReservationsReq).Reservations;
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000DADC File Offset: 0x0000BCDC
		public InventoryReservationDTO GetNextReservationAfterDateByProduct(Guid productUniqueID, DateTime date)
		{
			GetNextReservationAfterDateByProductReq getNextReservationAfterDateByProductReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetNextReservationAfterDateByProductReq>();
			getNextReservationAfterDateByProductReq.ProductUniqueId = productUniqueID.ToString();
			getNextReservationAfterDateByProductReq.Date = date;
			return ClientServiceFactory.GetClientInstance<IInventoryReservation>().GetNextReservationAfterDateByProduct(getNextReservationAfterDateByProductReq).Reservation;
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000DB28 File Offset: 0x0000BD28
		public int MakeReservation(InventoryReservationGroupDTO reservationGroup, params Guid[] reservedProductUniqueIds)
		{
			MakeReservationReq makeReservationReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MakeReservationReq>();
			makeReservationReq.ReservationGroup = reservationGroup;
			makeReservationReq.ReservedProductUniqueIds = (from p in reservedProductUniqueIds
			select p.ToString()).ToList<string>();
			return ClientServiceFactory.GetClientInstance<IInventoryReservation>().MakeReservation(makeReservationReq).ReservationId;
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000DB90 File Offset: 0x0000BD90
		public void MarkReservationAsCompleted(int reservationId)
		{
			MarkReservationAsCompletedReq markReservationAsCompletedReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MarkReservationAsCompletedReq>();
			markReservationAsCompletedReq.ReservationId = reservationId;
			ClientServiceFactory.GetClientInstance<IInventoryReservation>().MarkReservationAsCompleted(markReservationAsCompletedReq);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000DBC0 File Offset: 0x0000BDC0
		public void CancelReservation(int reservationId)
		{
			CancelReservationReq cancelReservationReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CancelReservationReq>();
			cancelReservationReq.ReservationId = reservationId;
			ClientServiceFactory.GetClientInstance<IInventoryReservation>().CancelReservation(cancelReservationReq);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000DBF0 File Offset: 0x0000BDF0
		public void CancelReservationGroup(int reservationGroupId)
		{
			CancelReservationGroupReq cancelReservationGroupReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CancelReservationGroupReq>();
			cancelReservationGroupReq.ReservationGroupId = reservationGroupId;
			ClientServiceFactory.GetClientInstance<IInventoryReservation>().CancelReservationGroup(cancelReservationGroupReq);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000DC20 File Offset: 0x0000BE20
		public int UpdateReservation(InventoryReservationDTO reservation)
		{
			UpdateReservationReq updateReservationReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateReservationReq>();
			updateReservationReq.Reservation = reservation;
			return ClientServiceFactory.GetClientInstance<IInventoryReservation>().UpdateReservation(updateReservationReq).ReservationGroupId;
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000DC58 File Offset: 0x0000BE58
		public void UpdateReservationGroup(InventoryReservationGroupDTO reservationGroup)
		{
			UpdateReservationGroupReq updateReservationGroupReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateReservationGroupReq>();
			updateReservationGroupReq.ReservationGroup = reservationGroup;
			ClientServiceFactory.GetClientInstance<IInventoryReservation>().UpdateReservationGroup(updateReservationGroupReq);
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000DC88 File Offset: 0x0000BE88
		public IList<InventoryReservationDTO> GetReservationsByReservationGroupId(int reservationGroupId)
		{
			GetReservationsByReservationGroupIdReq getReservationsByReservationGroupIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetReservationsByReservationGroupIdReq>();
			getReservationsByReservationGroupIdReq.ReservationGroupId = reservationGroupId;
			return ClientServiceFactory.GetClientInstance<IInventoryReservation>().GetReservationsByReservationGroupId(getReservationsByReservationGroupIdReq).Reservations;
		}
	}
}
