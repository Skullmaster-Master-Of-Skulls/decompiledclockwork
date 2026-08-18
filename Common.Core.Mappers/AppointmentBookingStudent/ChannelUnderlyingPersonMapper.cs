using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentBookingStudent;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent;

namespace TechnoPro.Common.Core.Mappers.AppointmentBookingStudent
{
	// Token: 0x0200020A RID: 522
	public static class ChannelUnderlyingPersonMapper
	{
		// Token: 0x060008CE RID: 2254 RVA: 0x00026184 File Offset: 0x00024384
		static ChannelUnderlyingPersonMapper()
		{
			Mapper.CreateMap<ChannelUnderlyingPersonDTO, ChannelUnderlyingPerson>();
			Mapper.CreateMap<ChannelUnderlyingPerson, ChannelUnderlyingPersonDTO>();
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x00026194 File Offset: 0x00024394
		public static ChannelUnderlyingPerson ToDomainObject(this ChannelUnderlyingPersonDTO dto)
		{
			return Mapper.Map<ChannelUnderlyingPersonDTO, ChannelUnderlyingPerson>(dto);
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x000261AC File Offset: 0x000243AC
		public static ChannelUnderlyingPersonDTO ToDTO(this ChannelUnderlyingPerson item)
		{
			return Mapper.Map<ChannelUnderlyingPerson, ChannelUnderlyingPersonDTO>(item);
		}
	}
}
