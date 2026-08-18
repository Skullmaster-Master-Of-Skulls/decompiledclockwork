using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using ClockWorkLogger;
using TechnoPro.Common.DAO.AppointmentsCalendar;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent;

namespace TechnoPro.Common.DAO.Impl.AppointmentsCalendar
{
	// Token: 0x0200015E RID: 350
	public class AppointmentBookingStudentDAO : IAppointmentBookingStudentDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000A40 RID: 2624 RVA: 0x0000ED1A File Offset: 0x0000CF1A
		public AppointmentBookingStudentDAO()
		{
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0006BF82 File Offset: 0x0006A182
		public AppointmentBookingStudentDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000A42 RID: 2626 RVA: 0x0006BF94 File Offset: 0x0006A194
		// (set) Token: 0x06000A43 RID: 2627 RVA: 0x0006BF9C File Offset: 0x0006A19C
		public OperationContext OpContext { get; set; }

		// Token: 0x06000A44 RID: 2628 RVA: 0x0006BFA8 File Offset: 0x0006A1A8
		public IList<Channel> GetAllChannels(string channelsXml, string legacyChannelsXml)
		{
			bool flag;
			IList<Channel> channelsFromXml = channelsXml.GetChannelsFromXml(out flag);
			bool flag2 = flag;
			if (flag2)
			{
				IList<AppointmentBookingStudentDAO.AppointmentBookingAvailabilityGroupIdsDurations_Channel> activeChannels = AppointmentBookingStudentDAO.AppointmentBookingAvailabilityGroupIdsDurations_Channel.GetActiveChannels(legacyChannelsXml);
				using (IEnumerator<Channel> enumerator = channelsFromXml.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Channel channel = enumerator.Current;
						AppointmentBookingStudentDAO.AppointmentBookingAvailabilityGroupIdsDurations_Channel appointmentBookingAvailabilityGroupIdsDurations_Channel = activeChannels.FirstOrDefault((AppointmentBookingStudentDAO.AppointmentBookingAvailabilityGroupIdsDurations_Channel g) => g.Id.Equals(channel.Id, StringComparison.OrdinalIgnoreCase));
						bool flag3 = appointmentBookingAvailabilityGroupIdsDurations_Channel != null;
						if (flag3)
						{
							foreach (ChannelAvailability channelAvailability in channel.Availabilities)
							{
								channelAvailability.SlotSizeInMinutes = appointmentBookingAvailabilityGroupIdsDurations_Channel.DurationMinutes;
								channelAvailability.AppTypeIdToBookWith = appointmentBookingAvailabilityGroupIdsDurations_Channel.AppTypeId;
							}
						}
					}
				}
			}
			return channelsFromXml;
		}

		// Token: 0x020002B9 RID: 697
		internal class AppointmentBookingAvailabilityGroupIdsDurations_Channel
		{
			// Token: 0x06000FA3 RID: 4003 RVA: 0x0008E5A8 File Offset: 0x0008C7A8
			public static IList<AppointmentBookingStudentDAO.AppointmentBookingAvailabilityGroupIdsDurations_Channel> GetActiveChannels(string xml)
			{
				bool flag = string.IsNullOrEmpty(xml);
				IList<AppointmentBookingStudentDAO.AppointmentBookingAvailabilityGroupIdsDurations_Channel> result;
				if (flag)
				{
					result = new List<AppointmentBookingStudentDAO.AppointmentBookingAvailabilityGroupIdsDurations_Channel>();
				}
				else
				{
					try
					{
						XDocument xdocument = XDocument.Parse(xml);
						return (from g in xdocument.Descendants("channel")
						let xTitle = g.Element("title")
						let xId = g.Element("id")
						let xDescription = g.Element("description")
						let xAppTypeId = g.Element("apptypeid")
						let xDurationMinutes = g.Element("duration")
						let xBookingFormScreenNum = g.Element("bookingformscreennum")
						let xIsActive = g.Element("isactive")
						select new AppointmentBookingStudentDAO.AppointmentBookingAvailabilityGroupIdsDurations_Channel
						{
							Title = ((xTitle == null) ? "" : (xTitle.Value ?? "")),
							Id = ((xId == null) ? "" : (xId.Value ?? "")),
							Description = ((xDescription == null) ? "" : (xDescription.Value ?? "")),
							AppTypeId = ((xAppTypeId == null) ? 0 : xAppTypeId.GetIntFromElement().GetValueOrDefault()),
							DurationMinutes = ((xDurationMinutes == null) ? 0 : xDurationMinutes.GetIntFromElement().GetValueOrDefault()),
							BookingFormScreenNum = ((xBookingFormScreenNum == null) ? 0 : xBookingFormScreenNum.GetIntFromElement().GetValueOrDefault()),
							IsActive = ("1trueyes".IndexOf(((xId == null) ? "" : (xId.Value ?? "")).ToLower().Trim()) >= 0)
						}).ToList<AppointmentBookingStudentDAO.AppointmentBookingAvailabilityGroupIdsDurations_Channel>();
					}
					catch (Exception ex)
					{
						CWLogger.Logger.Error("Common.UI.Web.Entity.AppointmentBooking.AppointmentBookingAvailabilityGroupIdsDurations_Channel:ErrorParsingXml:xml={0}", xml ?? "NULL");
					}
					result = new List<AppointmentBookingStudentDAO.AppointmentBookingAvailabilityGroupIdsDurations_Channel>();
				}
				return result;
			}

			// Token: 0x1700015A RID: 346
			// (get) Token: 0x06000FA4 RID: 4004 RVA: 0x0008E754 File Offset: 0x0008C954
			// (set) Token: 0x06000FA5 RID: 4005 RVA: 0x0008E75C File Offset: 0x0008C95C
			public string Title { get; set; }

			// Token: 0x1700015B RID: 347
			// (get) Token: 0x06000FA6 RID: 4006 RVA: 0x0008E765 File Offset: 0x0008C965
			// (set) Token: 0x06000FA7 RID: 4007 RVA: 0x0008E76D File Offset: 0x0008C96D
			public string Id { get; set; }

			// Token: 0x1700015C RID: 348
			// (get) Token: 0x06000FA8 RID: 4008 RVA: 0x0008E776 File Offset: 0x0008C976
			// (set) Token: 0x06000FA9 RID: 4009 RVA: 0x0008E77E File Offset: 0x0008C97E
			public string Description { get; set; }

			// Token: 0x1700015D RID: 349
			// (get) Token: 0x06000FAA RID: 4010 RVA: 0x0008E787 File Offset: 0x0008C987
			// (set) Token: 0x06000FAB RID: 4011 RVA: 0x0008E78F File Offset: 0x0008C98F
			public int AppTypeId { get; set; }

			// Token: 0x1700015E RID: 350
			// (get) Token: 0x06000FAC RID: 4012 RVA: 0x0008E798 File Offset: 0x0008C998
			// (set) Token: 0x06000FAD RID: 4013 RVA: 0x0008E7A0 File Offset: 0x0008C9A0
			public int DurationMinutes { get; set; }

			// Token: 0x1700015F RID: 351
			// (get) Token: 0x06000FAE RID: 4014 RVA: 0x0008E7A9 File Offset: 0x0008C9A9
			// (set) Token: 0x06000FAF RID: 4015 RVA: 0x0008E7B1 File Offset: 0x0008C9B1
			public int BookingFormScreenNum { get; set; }

			// Token: 0x17000160 RID: 352
			// (get) Token: 0x06000FB0 RID: 4016 RVA: 0x0008E7BA File Offset: 0x0008C9BA
			// (set) Token: 0x06000FB1 RID: 4017 RVA: 0x0008E7C2 File Offset: 0x0008C9C2
			public bool IsActive { get; set; }
		}
	}
}
