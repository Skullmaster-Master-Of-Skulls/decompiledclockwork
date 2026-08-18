using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.Common.ICore.Inventory
{
	// Token: 0x02000084 RID: 132
	public interface IInventoryReservationManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000390 RID: 912
		InventoryReservation GetReservationById(int reservationId);

		// Token: 0x06000391 RID: 913
		IList<InventoryReservation> GetReservationsByProduct(Guid productUniqueID);

		// Token: 0x06000392 RID: 914
		IList<InventoryReservation> GetReservationsByProduct(Guid productUniqueID, DateTime startDate, DateTime endDate);

		// Token: 0x06000393 RID: 915
		IList<InventoryReservation> GetReservationsByProduct(int productId, DateTime startDate, DateTime endDate);

		// Token: 0x06000394 RID: 916
		IList<InventoryReservation> GetReservationsByWhoMadeIt(int personId);

		// Token: 0x06000395 RID: 917
		IList<InventoryReservation> GetReservationsByWhoMadeIt(int personId, DateTime startDate, DateTime endDate);

		// Token: 0x06000396 RID: 918
		IList<InventoryReservation> GetReservations(DateTime startDate, DateTime endDate);

		// Token: 0x06000397 RID: 919
		InventoryReservation GetNextReservationAfterDateByProduct(Guid productUniqueID, DateTime date);

		// Token: 0x06000398 RID: 920
		int MakeReservation(InventoryReservationGroup reservation, params Guid[] reservedProductUniqueIds);

		// Token: 0x06000399 RID: 921
		void MarkReservationAsCompleted(int reservationId);

		// Token: 0x0600039A RID: 922
		void CancelReservation(int reservationId);

		// Token: 0x0600039B RID: 923
		void CancelReservationGroup(int reservationGroupId);

		// Token: 0x0600039C RID: 924
		int UpdateReservation(InventoryReservation reservation);

		// Token: 0x0600039D RID: 925
		void UpdateReservationGroup(InventoryReservationGroup reservationGroup);

		// Token: 0x0600039E RID: 926
		IList<InventoryReservation> GetReservationsByReservationGroupId(int reservationGroupId);
	}
}
