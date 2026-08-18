using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsPointOfContact;
using TechnoPro.Common.Public.Entities.Inventory;
using TechnoPro.Common.TextFormat.Adapters;

namespace TechnoPro.Common.Core.Mappers.Inventory
{
	// Token: 0x020000FB RID: 251
	public static class InventoryProductSnapshotDTOMapper
	{
		// Token: 0x06000445 RID: 1093 RVA: 0x00014E9C File Offset: 0x0001309C
		static InventoryProductSnapshotDTOMapper()
		{
			PersonBaseMapper.CreateMap();
			InventoryProductAccessoryMapper.CreateMap();
			Mapper.CreateMap<InventoryProductSnapshot, InventoryProductSnapshotDTO>().ForMember((InventoryProductSnapshotDTO dto) => dto.Accessories, delegate(IMemberConfigurationExpression<InventoryProductSnapshot> m)
			{
				m.MapFrom<IList<InventoryProductAccessoryDTO>>((InventoryProductSnapshot bo) => bo.Accessories.ToDTO());
			});
			Mapper.CreateMap<InventoryProductSnapshotDTO, InventoryProductSnapshot>().ForMember((InventoryProductSnapshot bo) => (object)bo.Id, delegate(IMemberConfigurationExpression<InventoryProductSnapshotDTO> m)
			{
				m.Ignore();
			}).ForMember((InventoryProductSnapshot bo) => bo.Accessories, delegate(IMemberConfigurationExpression<InventoryProductSnapshotDTO> m)
			{
				m.MapFrom<IList<InventoryProductAccessory>>((InventoryProductSnapshotDTO dto) => dto.Accessories.ToDomainObject());
			});
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00014FC0 File Offset: 0x000131C0
		public static InventoryProductSnapshot ToDomainObject(this InventoryProductSnapshotDTO productDTO)
		{
			return Mapper.Map<InventoryProductSnapshotDTO, InventoryProductSnapshot>(productDTO);
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x00014FD8 File Offset: 0x000131D8
		public static InventoryProductSnapshotDTO ToDTO(this InventoryProductSnapshot product)
		{
			return Mapper.Map<InventoryProductSnapshot, InventoryProductSnapshotDTO>(product);
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x00014FF0 File Offset: 0x000131F0
		public static IList<InventoryProductSnapshot> ToDomainObject(this IList<InventoryProductSnapshotDTO> list)
		{
			IList<InventoryProductSnapshot> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<InventoryProductSnapshot>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00015034 File Offset: 0x00013234
		public static IList<InventoryProductSnapshotDTO> ToDTO(this IList<InventoryProductSnapshot> list)
		{
			IList<InventoryProductSnapshotDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<InventoryProductSnapshotDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00015078 File Offset: 0x00013278
		public static PointOfContact ToPointOfContact(this IList<InventoryProductSnapshot> pSnapshotList)
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
					attendee.Person = pSnapshotList[0].LoanedTo;
					student = attendee;
					list2.Add(attendee);
				}
				bool flag3 = pSnapshotList[0].WhoReturned != null && pSnapshotList[0].WhoReturned.PersonId > 0 && list.Find((Attendee g) => g.Person.PersonId == pSnapshotList[0].WhoReturned.PersonId) == null;
				if (flag3)
				{
					List<Attendee> list3 = list;
					Attendee attendee2 = new Attendee();
					attendee2.Person = pSnapshotList[0].WhoReturned;
					student = attendee2;
					list3.Add(attendee2);
				}
				bool flag4 = pSnapshotList[0].WhoModified != null && pSnapshotList[0].WhoModified.PersonId > 0;
				if (flag4)
				{
					List<Attendee> list4 = list;
					Attendee attendee3 = new Attendee();
					attendee3.Person = pSnapshotList[0].WhoModified;
					staff = attendee3;
					list4.Add(attendee3);
				}
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("{0} loaned products", pSnapshotList.Count);
				stringBuilder.AppendLine();
				foreach (InventoryProductSnapshot inventoryProductSnapshot in pSnapshotList)
				{
					stringBuilder.AppendLine("--------------- Product -----------------");
					string str = inventoryProductSnapshot.Reason.ToString().Replace("_", " ");
					stringBuilder.AppendLine("Reason: " + str);
					stringBuilder.AppendLine(string.Concat(new string[]
					{
						"Product: ",
						inventoryProductSnapshot.ProductName ?? "",
						" (",
						inventoryProductSnapshot.ProductDynamicDataId.ToString(),
						")"
					}));
					bool flag5 = !string.IsNullOrEmpty(inventoryProductSnapshot.BarCode);
					if (flag5)
					{
						stringBuilder.AppendLine("Barcode: " + inventoryProductSnapshot.BarCode);
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
					WhoBooked = pSnapshotList[0].WhoModified,
					StartDateTime = pSnapshotList[0].ModifiedDate.Date,
					EndDateTime = pSnapshotList[0].ModifiedDate.Date.AddHours(1.0)
				};
				result = pointOfContact;
			}
			return result;
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0001541C File Offset: 0x0001361C
		public static PointOfContact ToPointOfContact(this InventoryProductSnapshot inventoryProductSnapshot)
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
					(obj = new Attendee()).Person = inventoryProductSnapshot.LoanedTo;
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
						(obj2 = new Attendee()).Person = inventoryProductSnapshot.WhoModified;
					}
				}
				else
				{
					(obj2 = new Attendee()).Person = inventoryProductSnapshot.WhoReturned;
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
					WhoBooked = inventoryProductSnapshot.WhoModified,
					StartDateTime = inventoryProductSnapshot.ModifiedDate,
					EndDateTime = inventoryProductSnapshot.ModifiedDate.AddHours(1.0)
				};
				result = pointOfContact;
			}
			return result;
		}
	}
}
