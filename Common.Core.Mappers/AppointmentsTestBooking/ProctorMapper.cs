using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Core.Mappers.PersonBase;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking
{
	// Token: 0x020001C5 RID: 453
	public static class ProctorMapper
	{
		// Token: 0x060007B5 RID: 1973 RVA: 0x0002171C File Offset: 0x0001F91C
		static ProctorMapper()
		{
			PersonBaseMapper.CreateMap();
			StaffCommonInfoMapper.CreateMap();
			Mapper.CreateMap<ProctorDTO, Proctor>().ForMember((Proctor pb) => pb.Staff, delegate(IMemberConfigurationExpression<ProctorDTO> m)
			{
				m.MapFrom<PersonBase>((ProctorDTO pbdto) => (pbdto.Staff == null) ? null : pbdto.Staff.ToDomainObject());
			}).ForMember((Proctor pb) => pb.StaffCommonInfo, delegate(IMemberConfigurationExpression<ProctorDTO> m)
			{
				m.MapFrom<StaffCommonInfo>((ProctorDTO pbdto) => (pbdto.StaffCommonInfo == null) ? null : pbdto.StaffCommonInfo.ToDomainObject());
			});
			Mapper.CreateMap<Proctor, ProctorDTO>().ForMember((ProctorDTO pb) => pb.Staff, delegate(IMemberConfigurationExpression<Proctor> m)
			{
				m.MapFrom<PersonBaseDTO>((Proctor pbdto) => (pbdto.Staff == null) ? null : pbdto.Staff.ToDTO());
			}).ForMember((ProctorDTO pb) => pb.StaffCommonInfo, delegate(IMemberConfigurationExpression<Proctor> m)
			{
				m.MapFrom<StaffCommonInfoDTO>((Proctor pbdto) => (pbdto.StaffCommonInfo == null) ? null : pbdto.StaffCommonInfo.ToDTO());
			});
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x0002187C File Offset: 0x0001FA7C
		public static Proctor ToDomainObject(this ProctorDTO dto)
		{
			return Mapper.Map<ProctorDTO, Proctor>(dto);
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x00021894 File Offset: 0x0001FA94
		public static ProctorDTO ToDTO(this Proctor item)
		{
			return Mapper.Map<Proctor, ProctorDTO>(item);
		}
	}
}
