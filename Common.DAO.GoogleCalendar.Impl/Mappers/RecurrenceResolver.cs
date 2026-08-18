using System;
using AutoMapper;
using Google.Apis.Calendar.v3.Data;

namespace TechnoPro.Common.DAO.GoogleCalendar.Impl.Mappers
{
	// Token: 0x02000005 RID: 5
	internal class RecurrenceResolver : ValueResolver<Event, bool>
	{
		// Token: 0x0600002E RID: 46 RVA: 0x00002E24 File Offset: 0x00001024
		protected override bool ResolveCore(Event e)
		{
			return e.Recurrence != null || !string.IsNullOrEmpty(e.RecurringEventId);
		}
	}
}
