using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentBookingStudent;
using TechnoPro.Common.Core.Mappers.Campus;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent;

namespace TechnoPro.Common.Core.Mappers.AppointmentBookingStudent
{
	// Token: 0x02000209 RID: 521
	public static class ChannelPersonCollectionMapper
	{
		// Token: 0x060008CA RID: 2250 RVA: 0x00025FF4 File Offset: 0x000241F4
		static ChannelPersonCollectionMapper()
		{
			SchoolCampusMapper.CreateMap();
			ChannelUnderlyingPersonMapper.CreateMap();
			Mapper.CreateMap<ChannelPersonCollectionDTO, ChannelPersonCollection>().ForMember((ChannelPersonCollection pb) => pb.Campus, delegate(IMemberConfigurationExpression<ChannelPersonCollectionDTO> m)
			{
				m.MapFrom<SchoolCampus>((ChannelPersonCollectionDTO pbdto) => (pbdto.Campus == null) ? null : pbdto.Campus.ToDomainObject());
			}).ForMember((ChannelPersonCollection pb) => pb.UnderlyingPeople, delegate(IMemberConfigurationExpression<ChannelPersonCollectionDTO> m)
			{
				m.MapFrom<IEnumerable<ChannelUnderlyingPerson>>((ChannelPersonCollectionDTO pbdto) => (pbdto.UnderlyingPeople == null) ? null : (from g in pbdto.UnderlyingPeople
				select g.ToDomainObject()));
			});
			Mapper.CreateMap<ChannelPersonCollection, ChannelPersonCollectionDTO>().ForMember((ChannelPersonCollectionDTO pb) => pb.Campus, delegate(IMemberConfigurationExpression<ChannelPersonCollection> m)
			{
				m.MapFrom<SchoolCampusDTO>((ChannelPersonCollection pbdto) => (pbdto.Campus == null) ? null : pbdto.Campus.ToDTO());
			}).ForMember((ChannelPersonCollectionDTO pb) => pb.UnderlyingPeople, delegate(IMemberConfigurationExpression<ChannelPersonCollection> m)
			{
				m.MapFrom<IEnumerable<ChannelUnderlyingPersonDTO>>((ChannelPersonCollection pbdto) => (pbdto.UnderlyingPeople == null) ? null : (from g in pbdto.UnderlyingPeople
				select g.ToDTO()));
			});
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x00026154 File Offset: 0x00024354
		public static ChannelPersonCollection ToDomainObject(this ChannelPersonCollectionDTO dto)
		{
			return Mapper.Map<ChannelPersonCollectionDTO, ChannelPersonCollection>(dto);
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x0002616C File Offset: 0x0002436C
		public static ChannelPersonCollectionDTO ToDTO(this ChannelPersonCollection item)
		{
			return Mapper.Map<ChannelPersonCollection, ChannelPersonCollectionDTO>(item);
		}
	}
}
