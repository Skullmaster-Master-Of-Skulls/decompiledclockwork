using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Core.Mappers.Appointments
{
	// Token: 0x020001AF RID: 431
	public static class AttendeeMapper
	{
		// Token: 0x06000757 RID: 1879 RVA: 0x000201CC File Offset: 0x0001E3CC
		static AttendeeMapper()
		{
			PersonBaseMapper.CreateMap();
			Mapper.CreateMap<AttendeeDTO, Attendee>().ForMember((Attendee pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<AttendeeDTO> m)
			{
				m.Ignore();
			}).ForMember((Attendee pb) => pb.Person, delegate(IMemberConfigurationExpression<AttendeeDTO> m)
			{
				m.MapFrom<PersonBase>((AttendeeDTO pbdto) => pbdto.Person.ToDomainObject());
			});
			Mapper.CreateMap<Attendee, AttendeeDTO>().ForMember((AttendeeDTO pb) => pb.Tag, delegate(IMemberConfigurationExpression<Attendee> m)
			{
				m.Ignore();
			}).ForMember((AttendeeDTO pb) => pb.Person, delegate(IMemberConfigurationExpression<Attendee> m)
			{
				m.MapFrom<PersonBaseDTO>((Attendee pbdto) => pbdto.Person.ToDTO());
			}).ForMember((AttendeeDTO pb) => pb.Attendee, delegate(IMemberConfigurationExpression<Attendee> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x00020388 File Offset: 0x0001E588
		public static Attendee ToDomainObject(this AttendeeDTO attendeeDTO)
		{
			return Mapper.Map<AttendeeDTO, Attendee>(attendeeDTO);
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x000203A0 File Offset: 0x0001E5A0
		public static AttendeeDTO ToDTO(this Attendee attendee)
		{
			return Mapper.Map<Attendee, AttendeeDTO>(attendee);
		}
	}
}
