using System;
using AutoMapper;
using Microsoft.Exchange.WebServices.Data;
using TechnoPro.Common.Public.Entities.AppointmentSync;

namespace TechnoPro.Common.DAO.Exchange.Impl.Mappers
{
	// Token: 0x02000006 RID: 6
	public static class ExchangeAttendeeMapper
	{
		// Token: 0x0600003D RID: 61 RVA: 0x0000622C File Offset: 0x0000442C
		static ExchangeAttendeeMapper()
		{
			Mapper.CreateMap<ExternalAttendee, Attendee>().ForMember((Attendee att) => att.Id, delegate(IMemberConfigurationExpression<ExternalAttendee> m)
			{
				m.Ignore();
			}).ForMember((Attendee att) => att.Name, delegate(IMemberConfigurationExpression<ExternalAttendee> m)
			{
				m.MapFrom<string>((ExternalAttendee att) => att.Name);
			}).ConstructUsing((ExternalAttendee oatt) => new Attendee(new EmailAddress(oatt.Username)));
			Mapper.CreateMap<Attendee, ExternalAttendee>().ForMember((ExternalAttendee oatt) => oatt.Id, delegate(IMemberConfigurationExpression<Attendee> m)
			{
				m.Ignore();
			}).ForMember((ExternalAttendee oatt) => oatt.Name, delegate(IMemberConfigurationExpression<Attendee> m)
			{
				m.MapFrom<string>((Attendee att) => att.Name);
			}).ForMember((ExternalAttendee oatt) => oatt.Username, delegate(IMemberConfigurationExpression<Attendee> m)
			{
				m.MapFrom<string>((Attendee att) => att.Address);
			});
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000063E6 File Offset: 0x000045E6
		public static void CreateMap()
		{
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000063EC File Offset: 0x000045EC
		public static Attendee ToDomainObject(this ExternalAttendee externalAttendee)
		{
			return Mapper.Map<ExternalAttendee, Attendee>(externalAttendee);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00006404 File Offset: 0x00004604
		public static ExternalAttendee ToDTO(this Attendee attendee)
		{
			return Mapper.Map<Attendee, ExternalAttendee>(attendee);
		}
	}
}
