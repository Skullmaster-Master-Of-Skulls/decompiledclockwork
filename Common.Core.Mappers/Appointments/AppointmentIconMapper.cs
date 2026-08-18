using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.Common.Core.Mappers.DynamicForms;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.Common.Core.Mappers.Appointments
{
	// Token: 0x020001A6 RID: 422
	public static class AppointmentIconMapper
	{
		// Token: 0x0600072B RID: 1835 RVA: 0x0001F734 File Offset: 0x0001D934
		static AppointmentIconMapper()
		{
			IconInfoMapper.CreateMap();
			DynamicFormMapper.CreateMap();
			Mapper.CreateMap<AppointmentIconDTO, AppointmentIcon>().ForMember((AppointmentIcon pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<AppointmentIconDTO> m)
			{
				m.Ignore();
			}).ForMember((AppointmentIcon pb) => (object)pb.IconNum, delegate(IMemberConfigurationExpression<AppointmentIconDTO> m)
			{
				m.Ignore();
			}).ForMember((AppointmentIcon pb) => pb.Icon, delegate(IMemberConfigurationExpression<AppointmentIconDTO> m)
			{
				m.MapFrom<IconInfo>((AppointmentIconDTO pbdto) => pbdto.Icon.ToDomainObject());
			});
			Mapper.CreateMap<AppointmentIcon, AppointmentIconDTO>().ForMember((AppointmentIconDTO pb) => pb.Icon, delegate(IMemberConfigurationExpression<AppointmentIcon> m)
			{
				m.MapFrom<IconInfoDTO>((AppointmentIcon pbdto) => pbdto.Icon.ToDTO());
			});
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0001F8B8 File Offset: 0x0001DAB8
		public static AppointmentIcon ToDomainObject(this AppointmentIconDTO appointmentIconDTO)
		{
			return Mapper.Map<AppointmentIconDTO, AppointmentIcon>(appointmentIconDTO);
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x0001F8D0 File Offset: 0x0001DAD0
		public static AppointmentIconDTO ToDTO(this AppointmentIcon appointmentIcon)
		{
			return Mapper.Map<AppointmentIcon, AppointmentIconDTO>(appointmentIcon);
		}
	}
}
