using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Inventory;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.Inventory
{
	// Token: 0x02000049 RID: 73
	public class InventoryReservationRestClientManager : BearerTokenRestProxy<IInventoryReservationClientManager>, IInventoryReservationClientManager, IWebService
	{
		// Token: 0x060002AC RID: 684 RVA: 0x00007F49 File Offset: 0x00006149
		public InventoryReservationRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00007F53 File Offset: 0x00006153
		public InventoryReservationRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00007F5E File Offset: 0x0000615E
		public InventoryReservationDTO GetReservationById(int reservationId)
		{
			return base.Get<InventoryReservationDTO>(string.Format("inventoryreservation/reservationid/{0}", reservationId), true);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00007F77 File Offset: 0x00006177
		public IList<InventoryReservationDTO> GetReservationsByProduct(Guid productUniqueID)
		{
			return base.GetMany<InventoryReservationDTO>(string.Format("inventoryreservation/productid/{0}", productUniqueID), true);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00007F90 File Offset: 0x00006190
		public IList<InventoryReservationDTO> GetReservationsByProduct(Guid productUniqueID, DateTime startDate, DateTime endDate)
		{
			return base.GetMany<InventoryReservationDTO>(string.Format("inventoryreservation/productuniqueid/{0}/range/{1}/{2}", productUniqueID, startDate, endDate), true);
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x00007FB5 File Offset: 0x000061B5
		public IList<InventoryReservationDTO> GetReservationsByProduct(int productId, DateTime startDate, DateTime endDate)
		{
			return base.GetMany<InventoryReservationDTO>(string.Format("inventoryreservation/productid/{0}/range/{1}/{2}", productId, startDate, endDate), true);
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00007FDA File Offset: 0x000061DA
		public IList<InventoryReservationDTO> GetReservationsByWhoMadeIt(int personId)
		{
			return base.GetMany<InventoryReservationDTO>(string.Format("inventoryreservation/whomade/personid/{0}", personId), true);
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00007FF3 File Offset: 0x000061F3
		public IList<InventoryReservationDTO> GetReservationsByWhoMadeIt(int personId, DateTime startDate, DateTime endDate)
		{
			return base.GetMany<InventoryReservationDTO>(string.Format("inventoryreservation/whomade/personid/{0}/range/{1}/{2}", personId, startDate, endDate), true);
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x00008018 File Offset: 0x00006218
		public IList<InventoryReservationDTO> GetReservations(DateTime startDate, DateTime endDate)
		{
			return base.GetMany<InventoryReservationDTO>(string.Format("inventoryreservation/range/{0}/{1}", startDate, endDate), true);
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00008037 File Offset: 0x00006237
		public InventoryReservationDTO GetNextReservationAfterDateByProduct(Guid productUniqueID, DateTime date)
		{
			return base.Get<InventoryReservationDTO>(string.Format("inventoryreservation/nextreservationafterdate/productid/{0}/date/{1}", productUniqueID, date), true);
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x00008058 File Offset: 0x00006258
		public int MakeReservation(InventoryReservationGroupDTO reservationGroup, params Guid[] reservedProductUniqueIds)
		{
			MakeReservationReq makeReservationReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MakeReservationReq>();
			makeReservationReq.ReservationGroup = reservationGroup;
			makeReservationReq.ReservedProductUniqueIds = (from p in reservedProductUniqueIds
			select p.ToString()).ToList<string>();
			return base.Post<MakeReservationReq, int>(makeReservationReq, "inventoryreservation");
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x000080B4 File Offset: 0x000062B4
		public void MarkReservationAsCompleted(int reservationId)
		{
			MarkReservationAsCompletedReq markReservationAsCompletedReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MarkReservationAsCompletedReq>();
			markReservationAsCompletedReq.ReservationId = reservationId;
			base.Post<MarkReservationAsCompletedReq>(markReservationAsCompletedReq, "inventoryreservation/markascompleted");
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x000080E0 File Offset: 0x000062E0
		public void CancelReservation(int reservationId)
		{
			CancelReservationReq cancelReservationReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CancelReservationReq>();
			cancelReservationReq.ReservationId = reservationId;
			base.Post<CancelReservationReq>(cancelReservationReq, "inventoryreservation/cancel");
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000810C File Offset: 0x0000630C
		public void CancelReservationGroup(int reservationGroupId)
		{
			CancelReservationGroupReq cancelReservationGroupReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CancelReservationGroupReq>();
			cancelReservationGroupReq.ReservationGroupId = reservationGroupId;
			base.Post<CancelReservationGroupReq>(cancelReservationGroupReq, "inventoryreservation/cancelgroup");
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00008137 File Offset: 0x00006337
		public int UpdateReservation(InventoryReservationDTO reservation)
		{
			return base.Post<InventoryReservationDTO, int>(reservation, "inventoryreservation");
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00008145 File Offset: 0x00006345
		public void UpdateReservationGroup(InventoryReservationGroupDTO reservationGroup)
		{
			base.Put<InventoryReservationGroupDTO>(reservationGroup, "inventoryreservation/reservationgroup");
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00008153 File Offset: 0x00006353
		public IList<InventoryReservationDTO> GetReservationsByReservationGroupId(int reservationGroupId)
		{
			return base.GetMany<InventoryReservationDTO>(string.Format("inventoryreservation/reservationgroupid/{0}", reservationGroupId), true);
		}
	}
}
