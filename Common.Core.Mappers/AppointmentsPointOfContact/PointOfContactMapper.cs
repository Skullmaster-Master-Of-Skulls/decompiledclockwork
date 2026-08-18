using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsPointOfContact;
using TechnoPro.Common.Core.Mappers.Appointments;
using TechnoPro.Common.Core.Mappers.DynamicForms;
using TechnoPro.Common.Public.Entities.AppointmentsPointOfContact;

namespace TechnoPro.Common.Core.Mappers.AppointmentsPointOfContact
{
	// Token: 0x02000199 RID: 409
	public static class PointOfContactMapper
	{
		// Token: 0x060006F8 RID: 1784 RVA: 0x0001EF00 File Offset: 0x0001D100
		static PointOfContactMapper()
		{
			AppShowTimeAsTypeMapper.CreateMap();
			AppTypeMapper.CreateMap();
			DynamicDataMapper.CreateMap();
			AttendeeMapper.CreateMap();
			PersonBaseMapper.CreateMap();
			Mapper.CreateMap<PointOfContactDTO, PointOfContact>().ForMember((PointOfContact pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<PointOfContactDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<PointOfContact, PointOfContactDTO>();
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0001EF9C File Offset: 0x0001D19C
		public static PointOfContact ToDomainObject(this PointOfContactDTO dto)
		{
			return Mapper.Map<PointOfContactDTO, PointOfContact>(dto);
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x0001EFB4 File Offset: 0x0001D1B4
		public static PointOfContactDTO ToDTO(this PointOfContact poc)
		{
			return Mapper.Map<PointOfContact, PointOfContactDTO>(poc);
		}
	}
}
