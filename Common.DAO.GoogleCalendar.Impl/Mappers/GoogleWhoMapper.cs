using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Google.Apis.Calendar.v3.Data;
using TechnoPro.Common.Public.Entities.AppointmentSync;

namespace TechnoPro.Common.DAO.GoogleCalendar.Impl.Mappers
{
	// Token: 0x02000007 RID: 7
	internal static class GoogleWhoMapper
	{
		// Token: 0x06000034 RID: 52 RVA: 0x0000329C File Offset: 0x0000149C
		static GoogleWhoMapper()
		{
			Mapper.CreateMap<EventAttendee, ExternalAttendee>().ForMember((ExternalAttendee a) => a.Username, delegate(IMemberConfigurationExpression<EventAttendee> m)
			{
				m.MapFrom<string>((EventAttendee w) => w.Email);
			}).ForMember((ExternalAttendee a) => a.Name, delegate(IMemberConfigurationExpression<EventAttendee> m)
			{
				m.MapFrom<string>((EventAttendee w) => w.DisplayName);
			}).ForMember((ExternalAttendee a) => (object)a.Self, delegate(IMemberConfigurationExpression<EventAttendee> m)
			{
				m.MapFrom<bool?>((EventAttendee w) => w.Self);
			}).ForMember((ExternalAttendee a) => (object)a.Organizer, delegate(IMemberConfigurationExpression<EventAttendee> m)
			{
				m.MapFrom<bool?>((EventAttendee w) => w.Organizer);
			}).ForMember((ExternalAttendee a) => (object)a.Optional, delegate(IMemberConfigurationExpression<EventAttendee> m)
			{
				m.MapFrom<bool?>((EventAttendee w) => w.Optional);
			}).ForMember((ExternalAttendee a) => (object)a.AttendeeType, delegate(IMemberConfigurationExpression<EventAttendee> m)
			{
				m.ResolveUsing(new AttendeeTypeResolver());
			});
			Mapper.CreateMap<ExternalAttendee, EventAttendee>().ForMember((EventAttendee w) => w.DisplayName, delegate(IMemberConfigurationExpression<ExternalAttendee> m)
			{
				m.MapFrom<string>((ExternalAttendee a) => a.Name);
			}).ForMember((EventAttendee w) => w.Comment, delegate(IMemberConfigurationExpression<ExternalAttendee> m)
			{
				m.Ignore();
			}).ForMember((EventAttendee w) => (object)w.AdditionalGuests, delegate(IMemberConfigurationExpression<ExternalAttendee> m)
			{
				m.Ignore();
			}).ForMember((EventAttendee w) => w.ETag, delegate(IMemberConfigurationExpression<ExternalAttendee> m)
			{
				m.Ignore();
			}).ForMember((EventAttendee w) => w.Email, delegate(IMemberConfigurationExpression<ExternalAttendee> m)
			{
				m.MapFrom<string>((ExternalAttendee a) => a.Username);
			}).ForMember((EventAttendee w) => (object)w.Optional, delegate(IMemberConfigurationExpression<ExternalAttendee> m)
			{
				m.MapFrom<bool?>((ExternalAttendee a) => a.Optional);
			}).ForMember((EventAttendee w) => (object)w.Organizer, delegate(IMemberConfigurationExpression<ExternalAttendee> m)
			{
				m.Condition((ExternalAttendee a) => a.AttendeeType == eAttendeeType.EVENT_ORGANIZER);
			}).ForMember((EventAttendee w) => w.Id, delegate(IMemberConfigurationExpression<ExternalAttendee> m)
			{
				m.Ignore();
			}).ForMember((EventAttendee w) => (object)w.Self, delegate(IMemberConfigurationExpression<ExternalAttendee> m)
			{
				m.MapFrom<bool?>((ExternalAttendee a) => a.Self);
			}).ForMember((EventAttendee w) => w.ResponseStatus, delegate(IMemberConfigurationExpression<ExternalAttendee> m)
			{
				m.MapFrom<string>((ExternalAttendee a) => a.ResponseStatus);
			}).ForMember((EventAttendee w) => (object)w.Resource, delegate(IMemberConfigurationExpression<ExternalAttendee> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002429 File Offset: 0x00000629
		public static void CreateMap()
		{
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000386C File Offset: 0x00001A6C
		public static EventAttendee ToDAOWho(this ExternalAttendee externalAppointment)
		{
			return Mapper.Map<ExternalAttendee, EventAttendee>(externalAppointment);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003884 File Offset: 0x00001A84
		public static ExternalAttendee ToDomainObject(this EventAttendee who)
		{
			return Mapper.Map<EventAttendee, ExternalAttendee>(who);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000389C File Offset: 0x00001A9C
		public static IList<EventAttendee> ToDAOWho(this IList<ExternalAttendee> outlookAppointment)
		{
			return Mapper.Map<IList<ExternalAttendee>, IList<EventAttendee>>(outlookAppointment);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000038B4 File Offset: 0x00001AB4
		public static IList<ExternalAttendee> ToDomainObject(this IList<EventAttendee> who)
		{
			IList<ExternalAttendee> result;
			if (who == null)
			{
				IList<ExternalAttendee> list = new List<ExternalAttendee>();
				result = list;
			}
			else
			{
				result = Mapper.Map<IList<EventAttendee>, IList<ExternalAttendee>>((from w in who
				where string.IsNullOrEmpty(w.ResponseStatus) || !w.ResponseStatus.Equals("declined")
				select w).ToList<EventAttendee>());
			}
			return result;
		}
	}
}
