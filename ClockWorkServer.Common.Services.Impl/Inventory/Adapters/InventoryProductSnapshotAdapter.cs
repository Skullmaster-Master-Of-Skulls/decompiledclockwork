using System;
using System.Collections.Generic;
using System.Text;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Core.Mappers;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsPointOfContact;
using TechnoPro.Common.TextFormat.Adapters;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl.Inventory.Adapters
{
	// Token: 0x020000A5 RID: 165
	public static class InventoryProductSnapshotAdapter
	{
		// Token: 0x060005FD RID: 1533 RVA: 0x0001BA40 File Offset: 0x00019C40
		public static PointOfContact ConvertToPointOfContact(this InventoryProductSnapshotDTO inventoryProductSnapshot)
		{
			bool flag = inventoryProductSnapshot == null;
			PointOfContact result;
			if (flag)
			{
				result = null;
			}
			else
			{
				object obj;
				if (inventoryProductSnapshot.LoanedTo == null || inventoryProductSnapshot.LoanedTo.PersonId <= 0)
				{
					obj = null;
				}
				else
				{
					(obj = new Attendee()).Person = inventoryProductSnapshot.LoanedTo.ToDomainObject();
				}
				Attendee student = obj;
				object obj2;
				if (inventoryProductSnapshot.WhoReturned == null || inventoryProductSnapshot.WhoReturned.PersonId <= 0)
				{
					if (inventoryProductSnapshot.WhoModified == null || inventoryProductSnapshot.WhoModified.PersonId <= 0)
					{
						obj2 = null;
					}
					else
					{
						(obj2 = new Attendee()).Person = inventoryProductSnapshot.WhoModified.ToDomainObject();
					}
				}
				else
				{
					(obj2 = new Attendee()).Person = inventoryProductSnapshot.WhoReturned.ToDomainObject();
				}
				Attendee staff = obj2;
				StringBuilder stringBuilder = new StringBuilder();
				string value = inventoryProductSnapshot.Reason.ToString().Replace("_", " ");
				stringBuilder.AppendLine(value);
				stringBuilder.AppendLine(string.Concat(new string[]
				{
					"Product: ",
					inventoryProductSnapshot.ProductName ?? "",
					" (",
					inventoryProductSnapshot.ProductDynamicDataId.ToString(),
					")"
				}));
				bool flag2 = !string.IsNullOrEmpty(inventoryProductSnapshot.BarCode);
				if (flag2)
				{
					stringBuilder.AppendLine("Barcode: " + inventoryProductSnapshot.BarCode);
				}
				PointOfContact pointOfContact = new PointOfContact
				{
					Student = student,
					Staff = staff,
					DateBooked = DateTime.Now,
					Attendees = new List<Attendee>(),
					Memo = stringBuilder.ToString().ConvertPlainTextToRtf(),
					SubTitle = inventoryProductSnapshot.Reason.ToString().Replace("_", " "),
					WhoBooked = inventoryProductSnapshot.WhoModified.ToDomainObject(),
					StartDateTime = inventoryProductSnapshot.ModifiedDate,
					EndDateTime = inventoryProductSnapshot.ModifiedDate.AddHours(1.0)
				};
				result = pointOfContact;
			}
			return result;
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x0001BC50 File Offset: 0x00019E50
		public static PointOfContact ConvertToPointOfContact(this IList<InventoryProductSnapshotDTO> pSnapshotList)
		{
			bool flag = pSnapshotList == null || pSnapshotList.Count == 0;
			PointOfContact result;
			if (flag)
			{
				result = null;
			}
			else
			{
				List<Attendee> list = new List<Attendee>();
				Attendee student = null;
				Attendee staff = null;
				bool flag2 = pSnapshotList[0].LoanedTo != null && pSnapshotList[0].LoanedTo.PersonId > 0;
				if (flag2)
				{
					List<Attendee> list2 = list;
					Attendee attendee = new Attendee();
					attendee.Person = pSnapshotList[0].LoanedTo.ToDomainObject();
					student = attendee;
					list2.Add(attendee);
				}
				bool flag3 = pSnapshotList[0].WhoReturned != null && pSnapshotList[0].WhoReturned.PersonId > 0 && list.Find((Attendee g) => g.Person.PersonId == pSnapshotList[0].WhoReturned.PersonId) == null;
				if (flag3)
				{
					List<Attendee> list3 = list;
					Attendee attendee2 = new Attendee();
					attendee2.Person = pSnapshotList[0].WhoReturned.ToDomainObject();
					student = attendee2;
					list3.Add(attendee2);
				}
				bool flag4 = pSnapshotList[0].WhoModified != null && pSnapshotList[0].WhoModified.PersonId > 0;
				if (flag4)
				{
					List<Attendee> list4 = list;
					Attendee attendee3 = new Attendee();
					attendee3.Person = pSnapshotList[0].WhoModified.ToDomainObject();
					staff = attendee3;
					list4.Add(attendee3);
				}
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("{0} loaned products", pSnapshotList.Count);
				stringBuilder.AppendLine();
				foreach (InventoryProductSnapshotDTO inventoryProductSnapshotDTO in pSnapshotList)
				{
					stringBuilder.AppendLine("--------------- Product -----------------");
					string str = inventoryProductSnapshotDTO.Reason.ToString().Replace("_", " ");
					stringBuilder.AppendLine("Reason: " + str);
					stringBuilder.AppendLine(string.Concat(new string[]
					{
						"Product: ",
						inventoryProductSnapshotDTO.ProductName ?? "",
						" (",
						inventoryProductSnapshotDTO.ProductDynamicDataId.ToString(),
						")"
					}));
					bool flag5 = !string.IsNullOrEmpty(inventoryProductSnapshotDTO.BarCode);
					if (flag5)
					{
						stringBuilder.AppendLine("Barcode: " + inventoryProductSnapshotDTO.BarCode);
					}
					stringBuilder.AppendLine("--------------- End of product -----------------");
				}
				PointOfContact pointOfContact = new PointOfContact
				{
					Student = student,
					Staff = staff,
					DateBooked = DateTime.Now,
					Attendees = list,
					Memo = stringBuilder.ToString().ConvertPlainTextToRtf(),
					SubTitle = pSnapshotList[0].Reason.ToString().Replace("_", " "),
					WhoBooked = pSnapshotList[0].WhoModified.ToDomainObject(),
					StartDateTime = pSnapshotList[0].ModifiedDate.Date,
					EndDateTime = pSnapshotList[0].ModifiedDate.Date.AddHours(1.0)
				};
				result = pointOfContact;
			}
			return result;
		}
	}
}
