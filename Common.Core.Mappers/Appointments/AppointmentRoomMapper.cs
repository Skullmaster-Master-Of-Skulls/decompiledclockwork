using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Core.Mappers.Appointments
{
	// Token: 0x020001A8 RID: 424
	public static class AppointmentRoomMapper
	{
		// Token: 0x06000733 RID: 1843 RVA: 0x0001F9D0 File Offset: 0x0001DBD0
		static AppointmentRoomMapper()
		{
			Mapper.CreateMap<PersonBase, AppointmentRoom>().ForMember((AppointmentRoom ar) => (object)ar.RoomId, delegate(IMemberConfigurationExpression<PersonBase> m)
			{
				m.MapFrom<int>((PersonBase pb) => pb.PersonId);
			}).ForMember((AppointmentRoom ar) => ar.RoomTitle, delegate(IMemberConfigurationExpression<PersonBase> m)
			{
				m.MapFrom<string>((PersonBase pb) => pb.FirstName);
			}).ForMember((AppointmentRoom ar) => ar.RoomDescription, delegate(IMemberConfigurationExpression<PersonBase> m)
			{
				m.MapFrom<string>((PersonBase pb) => pb.LastName);
			}).ForMember((AppointmentRoom ar) => ar.RoomInfo, delegate(IMemberConfigurationExpression<PersonBase> m)
			{
				m.Ignore();
			}).ForMember((AppointmentRoom ar) => ar.RoomUniqueId, delegate(IMemberConfigurationExpression<PersonBase> m)
			{
				m.MapFrom<string>((PersonBase pb) => pb.Student_no);
			});
			Mapper.CreateMap<AppointmentRoom, AppointmentRoomDTO>();
			Mapper.CreateMap<AppointmentRoomDTO, AppointmentRoom>().ForMember((AppointmentRoom pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<AppointmentRoomDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x0001FBE8 File Offset: 0x0001DDE8
		public static AppointmentRoom ToDomainObject(this PersonBase room)
		{
			return Mapper.Map<PersonBase, AppointmentRoom>(room);
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0001FC00 File Offset: 0x0001DE00
		public static IList<AppointmentRoom> ToDomainObject(this IList<PersonBase> list)
		{
			IList<AppointmentRoom> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<AppointmentRoom>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x0001FC44 File Offset: 0x0001DE44
		public static AppointmentRoom ToDomainObject(this AppointmentRoomDTO dto)
		{
			return Mapper.Map<AppointmentRoomDTO, AppointmentRoom>(dto);
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x0001FC5C File Offset: 0x0001DE5C
		public static AppointmentRoomDTO ToDTO(this AppointmentRoom item)
		{
			return Mapper.Map<AppointmentRoom, AppointmentRoomDTO>(item);
		}
	}
}
