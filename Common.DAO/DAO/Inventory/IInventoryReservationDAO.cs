using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.Common.DAO.Inventory
{
	// Token: 0x0200006D RID: 109
	public interface IInventoryReservationDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000299 RID: 665
		InventoryReservation GetReservationById(int reservationId);

		// Token: 0x0600029A RID: 666
		IList<InventoryReservation> GetReservationsByProduct(Guid productUniqueID);

		// Token: 0x0600029B RID: 667
		IList<InventoryReservation> GetReservationsByProduct(Guid productUniqueID, DateTime startDate, DateTime endDate);

		// Token: 0x0600029C RID: 668
		IList<InventoryReservation> GetReservationsByProduct(int productId, DateTime startDate, DateTime endDate);

		// Token: 0x0600029D RID: 669
		IList<InventoryReservation> GetReservationsByWhoMadeIt(int personId);

		// Token: 0x0600029E RID: 670
		IList<InventoryReservation> GetReservationsByWhoMadeIt(int personId, DateTime startDate, DateTime endDate);

		// Token: 0x0600029F RID: 671
		IList<InventoryReservation> GetReservations(DateTime startDate, DateTime endDate);

		// Token: 0x060002A0 RID: 672
		InventoryReservation GetNextReservationAfterDateByProduct(Guid productUniqueID, DateTime date);

		// Token: 0x060002A1 RID: 673
		int MakeReservation(InventoryReservationGroup reservation, params Guid[] reservedProductUniqueIds);

		// Token: 0x060002A2 RID: 674
		void MarkReservationAsCompleted(int reservationId);

		// Token: 0x060002A3 RID: 675
		void CancelReservation(int reservationId);

		// Token: 0x060002A4 RID: 676
		void CancelReservationGroup(int reservationGroupId);

		// Token: 0x060002A5 RID: 677
		int UpdateReservation(InventoryReservation reservation);

		// Token: 0x060002A6 RID: 678
		void UpdateReservationGroup(InventoryReservationGroup reservationGroup);

		// Token: 0x060002A7 RID: 679
		IList<InventoryReservation> GetReservationsByReservationGroupId(int reservationGroupId);
	}
}
