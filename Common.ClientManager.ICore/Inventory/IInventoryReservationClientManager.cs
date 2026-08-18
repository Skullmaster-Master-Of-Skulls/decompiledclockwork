using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.Inventory
{
	// Token: 0x02000052 RID: 82
	public interface IInventoryReservationClientManager : IWebService
	{
		// Token: 0x06000252 RID: 594
		InventoryReservationDTO GetReservationById(int reservationId);

		// Token: 0x06000253 RID: 595
		IList<InventoryReservationDTO> GetReservationsByProduct(Guid productUniqueID);

		// Token: 0x06000254 RID: 596
		IList<InventoryReservationDTO> GetReservationsByProduct(Guid productUniqueID, DateTime startDate, DateTime endDate);

		// Token: 0x06000255 RID: 597
		IList<InventoryReservationDTO> GetReservationsByProduct(int productId, DateTime startDate, DateTime endDate);

		// Token: 0x06000256 RID: 598
		IList<InventoryReservationDTO> GetReservationsByWhoMadeIt(int personId);

		// Token: 0x06000257 RID: 599
		IList<InventoryReservationDTO> GetReservationsByWhoMadeIt(int personId, DateTime startDate, DateTime endDate);

		// Token: 0x06000258 RID: 600
		IList<InventoryReservationDTO> GetReservations(DateTime startDate, DateTime endDate);

		// Token: 0x06000259 RID: 601
		InventoryReservationDTO GetNextReservationAfterDateByProduct(Guid productUniqueID, DateTime date);

		// Token: 0x0600025A RID: 602
		int MakeReservation(InventoryReservationGroupDTO reservationGroup, params Guid[] reservedProductUniqueIds);

		// Token: 0x0600025B RID: 603
		void MarkReservationAsCompleted(int reservationId);

		// Token: 0x0600025C RID: 604
		void CancelReservation(int reservationId);

		// Token: 0x0600025D RID: 605
		void CancelReservationGroup(int reservationGroupId);

		// Token: 0x0600025E RID: 606
		int UpdateReservation(InventoryReservationDTO reservation);

		// Token: 0x0600025F RID: 607
		void UpdateReservationGroup(InventoryReservationGroupDTO reservationGroup);

		// Token: 0x06000260 RID: 608
		IList<InventoryReservationDTO> GetReservationsByReservationGroupId(int reservationGroupId);
	}
}
