using System;
using AutoMapper;
using Google.Apis.Calendar.v3.Data;
using TechnoPro.Common.Public.Entities.AppointmentSync;

namespace TechnoPro.Common.DAO.GoogleCalendar.Impl.Mappers
{
	// Token: 0x02000006 RID: 6
	internal static class GoogleOrganizerMapper
	{
		// Token: 0x06000030 RID: 48 RVA: 0x00002E58 File Offset: 0x00001058
		static GoogleOrganizerMapper()
		{
			Mapper.CreateMap<ExternalAttendee, Event.OrganizerData>().ForMember((Event.OrganizerData o) => o.DisplayName, delegate(IMemberConfigurationExpression<ExternalAttendee> m)
			{
				m.MapFrom<string>((ExternalAttendee a) => a.Name);
			}).ForMember((Event.OrganizerData o) => o.Email, delegate(IMemberConfigurationExpression<ExternalAttendee> m)
			{
				m.MapFrom<string>((ExternalAttendee a) => a.Username);
			}).ForMember((Event.OrganizerData o) => o.Id, delegate(IMemberConfigurationExpression<ExternalAttendee> m)
			{
				m.Ignore();
			}).ForMember((Event.OrganizerData o) => (object)o.Self, delegate(IMemberConfigurationExpression<ExternalAttendee> m)
			{
				m.MapFrom<bool?>((ExternalAttendee a) => a.Self);
			});
			Mapper.CreateMap<Event.OrganizerData, ExternalAttendee>().ForMember((ExternalAttendee a) => a.Name, delegate(IMemberConfigurationExpression<Event.OrganizerData> m)
			{
				m.MapFrom<string>((Event.OrganizerData o) => o.DisplayName);
			}).ForMember((ExternalAttendee a) => a.Username, delegate(IMemberConfigurationExpression<Event.OrganizerData> m)
			{
				m.MapFrom<string>((Event.OrganizerData o) => o.Email);
			}).ForMember((ExternalAttendee a) => (object)a.Self, delegate(IMemberConfigurationExpression<Event.OrganizerData> m)
			{
				m.MapFrom<bool?>((Event.OrganizerData o) => o.Self);
			}).ForMember((ExternalAttendee a) => (object)a.Organizer, delegate(IMemberConfigurationExpression<Event.OrganizerData> m)
			{
				m.UseValue<bool>(true);
			}).ForMember((ExternalAttendee a) => a.Id, delegate(IMemberConfigurationExpression<Event.OrganizerData> m)
			{
				m.MapFrom<string>((Event.OrganizerData o) => o.Id);
			}).ForMember((ExternalAttendee a) => (object)a.Optional, delegate(IMemberConfigurationExpression<Event.OrganizerData> m)
			{
				m.Ignore();
			}).ForMember((ExternalAttendee a) => a.ResponseStatus, delegate(IMemberConfigurationExpression<Event.OrganizerData> m)
			{
				m.Ignore();
			}).ForMember((ExternalAttendee a) => (object)a.AttendeeType, delegate(IMemberConfigurationExpression<Event.OrganizerData> m)
			{
				m.UseValue<eAttendeeType>(eAttendeeType.EVENT_ORGANIZER);
			});
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002429 File Offset: 0x00000629
		public static void CreateMap()
		{
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000326C File Offset: 0x0000146C
		public static Event.OrganizerData ToDAOWho(this ExternalAttendee externalAppointment)
		{
			return Mapper.Map<ExternalAttendee, Event.OrganizerData>(externalAppointment);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00003284 File Offset: 0x00001484
		public static ExternalAttendee ToDomainObject(this Event.OrganizerData who)
		{
			return Mapper.Map<Event.OrganizerData, ExternalAttendee>(who);
		}
	}
}
