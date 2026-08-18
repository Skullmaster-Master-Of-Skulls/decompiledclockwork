using System;
using System.Collections.Generic;
using TechnoPro.Common.DAO.Impl.Inventory;
using TechnoPro.Common.DAO.Inventory;
using TechnoPro.Common.ICore.Inventory;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.Common.Core.Inventory
{
	// Token: 0x020000EA RID: 234
	public class InventoryReservationManager : IInventoryReservationManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000912 RID: 2322 RVA: 0x0003AB79 File Offset: 0x00038D79
		// (set) Token: 0x06000913 RID: 2323 RVA: 0x0003AB81 File Offset: 0x00038D81
		public IInventoryReservationDAO ReservationDAO { get; set; }

		// Token: 0x06000914 RID: 2324 RVA: 0x0003AB8A File Offset: 0x00038D8A
		public InventoryReservationManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.ReservationDAO = new InventoryReservationDAO(this.OpContext);
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000915 RID: 2325 RVA: 0x0003ABAE File Offset: 0x00038DAE
		// (set) Token: 0x06000916 RID: 2326 RVA: 0x0003ABB6 File Offset: 0x00038DB6
		public OperationContext OpContext { get; set; }

		// Token: 0x06000917 RID: 2327 RVA: 0x0003ABC0 File Offset: 0x00038DC0
		public InventoryReservation GetReservationById(int reservationId)
		{
			return this.ReservationDAO.GetReservationById(reservationId);
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x0003ABE0 File Offset: 0x00038DE0
		public IList<InventoryReservation> GetReservationsByProduct(Guid productUniqueID)
		{
			return this.ReservationDAO.GetReservationsByProduct(productUniqueID);
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x0003AC00 File Offset: 0x00038E00
		public IList<InventoryReservation> GetReservationsByProduct(Guid productUniqueID, DateTime startDate, DateTime endDate)
		{
			return this.ReservationDAO.GetReservationsByProduct(productUniqueID, startDate, endDate);
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x0003AC20 File Offset: 0x00038E20
		public IList<InventoryReservation> GetReservationsByWhoMadeIt(int personId)
		{
			return this.ReservationDAO.GetReservationsByWhoMadeIt(personId);
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x0003AC40 File Offset: 0x00038E40
		public IList<InventoryReservation> GetReservationsByWhoMadeIt(int personId, DateTime startDate, DateTime endDate)
		{
			return this.ReservationDAO.GetReservationsByWhoMadeIt(personId, startDate, endDate);
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x0003AC60 File Offset: 0x00038E60
		public IList<InventoryReservation> GetReservations(DateTime startDate, DateTime endDate)
		{
			return this.ReservationDAO.GetReservations(startDate, endDate);
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x0003AC80 File Offset: 0x00038E80
		public InventoryReservation GetNextReservationAfterDateByProduct(Guid productUniqueID, DateTime date)
		{
			return this.ReservationDAO.GetNextReservationAfterDateByProduct(productUniqueID, date);
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x0003ACA0 File Offset: 0x00038EA0
		public int MakeReservation(InventoryReservationGroup reservation, params Guid[] reservedProductUniqueIds)
		{
			return this.ReservationDAO.MakeReservation(reservation, reservedProductUniqueIds);
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x0003ACBF File Offset: 0x00038EBF
		public void MarkReservationAsCompleted(int reservationId)
		{
			this.ReservationDAO.MarkReservationAsCompleted(reservationId);
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x0003ACCF File Offset: 0x00038ECF
		public void CancelReservation(int reservationId)
		{
			this.ReservationDAO.CancelReservation(reservationId);
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x0003ACDF File Offset: 0x00038EDF
		public void CancelReservationGroup(int reservationGroupId)
		{
			this.ReservationDAO.CancelReservationGroup(reservationGroupId);
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x0003ACF0 File Offset: 0x00038EF0
		public int UpdateReservation(InventoryReservation reservation)
		{
			return this.ReservationDAO.UpdateReservation(reservation);
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x0003AD0E File Offset: 0x00038F0E
		public void UpdateReservationGroup(InventoryReservationGroup reservationGroup)
		{
			this.ReservationDAO.UpdateReservationGroup(reservationGroup);
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x0003AD20 File Offset: 0x00038F20
		public IList<InventoryReservation> GetReservationsByReservationGroupId(int reservationGroupId)
		{
			return this.ReservationDAO.GetReservationsByReservationGroupId(reservationGroupId);
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x0003AD40 File Offset: 0x00038F40
		public IList<InventoryReservation> GetReservationsByProduct(int productId, DateTime startDate, DateTime endDate)
		{
			return this.ReservationDAO.GetReservationsByProduct(productId, startDate, endDate);
		}
	}
}
