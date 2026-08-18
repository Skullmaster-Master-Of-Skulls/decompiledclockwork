using System;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Inventory.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Core.AppointmentsPointOfContact;
using TechnoPro.Common.ICore.AppointmentsPointOfContact;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentsPointOfContact;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x0200004B RID: 75
	public class InventoryProductSnapshotAsyncActionsServiceManager : IInventoryProductSnapshotAsyncActions, IService
	{
		// Token: 0x060002D6 RID: 726 RVA: 0x0000E0FC File Offset: 0x0000C2FC
		public void SaveAsPointOfContact(SaveAsPointOfContactReq request)
		{
			PointOfContact pointOfContact = request.ProductSnapshot.ConvertToPointOfContact();
			bool flag = pointOfContact == null;
			if (!flag)
			{
				IPointOfContactManager pointOfContactManager = new PointOfContactManager(request.GetOperationContext());
				pointOfContactManager.CreatePointOfContact(true, pointOfContact);
			}
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000E138 File Offset: 0x0000C338
		public void SaveListAsPointOfContact(SaveListAsPointOfContactReq request)
		{
			PointOfContact pointOfContact = request.ProductSnapshotList.ConvertToPointOfContact();
			bool flag = pointOfContact == null;
			if (!flag)
			{
				IPointOfContactManager pointOfContactManager = new PointOfContactManager(request.GetOperationContext());
				pointOfContactManager.CreatePointOfContact(true, pointOfContact);
			}
		}
	}
}
