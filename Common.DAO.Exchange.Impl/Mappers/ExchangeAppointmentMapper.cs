using System;
using System.Collections.Generic;
using System.Linq;
using ClockWorkLogger;
using Microsoft.Exchange.WebServices.Data;
using TechnoPro.Common.DAO.AppointmentSync;
using TechnoPro.Common.DAO.Exchange.Impl.Adapters;
using TechnoPro.Common.Public.Entities.AppointmentSync;

namespace TechnoPro.Common.DAO.Exchange.Impl.Mappers
{
	// Token: 0x02000005 RID: 5
	public static class ExchangeAppointmentMapper
	{
		// Token: 0x06000039 RID: 57 RVA: 0x00004DC8 File Offset: 0x00002FC8
		public static ExternalAppointment ToDTO(this Appointment app, SyncOperationContext opContext)
		{
			bool flag = app == null;
			ExternalAppointment result;
			if (flag)
			{
				CWLogger.Logger.Trace("ExchangeAppointmentMapper::ToDTO:: Appointment is NULL");
				result = null;
			}
			else
			{
				ExchangeAppointmentMapper.LogExchangeApp(app);
				ExternalAppointment outapp = new ExternalAppointment();
				List<ExternalAttendee> list = new List<ExternalAttendee>();
				ISyncContactsDAO syncContactsDAO = new ExchangeContactsDAO(opContext);
				bool flag2 = app.RequiredAttendees != null;
				if (flag2)
				{
					using (IEnumerator<Attendee> enumerator = app.RequiredAttendees.GetEnumerator())
					{
						Func<ExternalAttendee, bool> <>9__1;
						while (enumerator.MoveNext())
						{
							Attendee attendee = enumerator.Current;
							try
							{
								bool flag3 = attendee == null;
								if (flag3)
								{
									CWLogger.Logger.Trace("ExchangeAppointmentMapper::ToDTO:: Attendee is NULL");
								}
								else
								{
									bool flag4 = string.IsNullOrEmpty(attendee.Address);
									if (flag4)
									{
										CWLogger.Logger.Trace("ExchangeAppointmentMapper::ToDTO:: Attendee.Address is NULL");
									}
									else
									{
										bool flag5;
										if (attendee.ResponseType != null)
										{
											MeetingResponseType? responseType = attendee.ResponseType;
											MeetingResponseType meetingResponseType = MeetingResponseType.Decline;
											flag5 = (responseType.GetValueOrDefault() == meetingResponseType & responseType != null);
										}
										else
										{
											flag5 = false;
										}
										bool flag6 = flag5;
										if (flag6)
										{
											CWLogger.Logger.Trace("ExchangeAppointmentMapper::ToDTO:: Attendee.ResponseType is Decline");
										}
										else
										{
											bool flag7 = attendee.MailboxType != null && (attendee.MailboxType.Value == MailboxType.ContactGroup || attendee.MailboxType.Value == MailboxType.PublicGroup);
											if (flag7)
											{
												IList<ExternalAttendee> groupMembers = syncContactsDAO.GetGroupMembers(attendee.Address);
												IEnumerable<ExternalAttendee> source = groupMembers;
												Func<ExternalAttendee, bool> predicate;
												if ((predicate = <>9__1) == null)
												{
													predicate = (<>9__1 = ((ExternalAttendee m) => opContext.SyncSettings.SyncUsers.Any((ClockWorkExternalApplicationSyncUser u) => u.ExternalApplicationUsername.Equals(m.Username, StringComparison.OrdinalIgnoreCase))));
												}
												using (IEnumerator<ExternalAttendee> enumerator2 = source.Where(predicate).GetEnumerator())
												{
													while (enumerator2.MoveNext())
													{
														ExternalAttendee member = enumerator2.Current;
														bool flag8 = list.Find((ExternalAttendee a) => a != null && a.Username != null && a.Username.Equals(member.Username, StringComparison.OrdinalIgnoreCase)) == null;
														if (flag8)
														{
															list.Add(member);
														}
													}
												}
											}
											bool flag9 = list.Find((ExternalAttendee a) => a != null && a.Username != null && a.Username.Equals(attendee.Address, StringComparison.OrdinalIgnoreCase)) == null;
											if (flag9)
											{
												list.Add(new ExternalAttendee
												{
													Username = attendee.Address,
													Name = attendee.Name
												});
											}
										}
									}
								}
							}
							catch (ServiceObjectPropertyException)
							{
							}
						}
					}
				}
				bool flag10 = app.OptionalAttendees != null;
				if (flag10)
				{
					using (IEnumerator<Attendee> enumerator3 = app.OptionalAttendees.GetEnumerator())
					{
						Func<ExternalAttendee, bool> <>9__5;
						while (enumerator3.MoveNext())
						{
							Attendee attendee = enumerator3.Current;
							try
							{
								bool flag11 = attendee == null;
								if (flag11)
								{
									CWLogger.Logger.Trace("ExchangeAppointmentMapper::ToDTO:: Attendee is NULL");
								}
								else
								{
									bool flag12 = string.IsNullOrEmpty(attendee.Address);
									if (flag12)
									{
										CWLogger.Logger.Trace("ExchangeAppointmentMapper::ToDTO:: Attendee.Address is NULL");
									}
									else
									{
										bool flag13;
										if (attendee.ResponseType != null)
										{
											MeetingResponseType? responseType = attendee.ResponseType;
											MeetingResponseType meetingResponseType = MeetingResponseType.Decline;
											flag13 = (responseType.GetValueOrDefault() == meetingResponseType & responseType != null);
										}
										else
										{
											flag13 = false;
										}
										bool flag14 = flag13;
										if (flag14)
										{
											CWLogger.Logger.Trace("ExchangeAppointmentMapper::ToDTO:: Attendee.ResponseType is Decline");
										}
										else
										{
											bool flag15 = attendee.MailboxType != null && (attendee.MailboxType.Value == MailboxType.ContactGroup || attendee.MailboxType.Value == MailboxType.PublicGroup);
											if (flag15)
											{
												IList<ExternalAttendee> groupMembers2 = syncContactsDAO.GetGroupMembers(attendee.Address);
												IEnumerable<ExternalAttendee> source2 = groupMembers2;
												Func<ExternalAttendee, bool> predicate2;
												if ((predicate2 = <>9__5) == null)
												{
													predicate2 = (<>9__5 = ((ExternalAttendee m) => opContext.SyncSettings.SyncUsers.Any((ClockWorkExternalApplicationSyncUser u) => u.ExternalApplicationUsername.Equals(m.Username, StringComparison.OrdinalIgnoreCase))));
												}
												using (IEnumerator<ExternalAttendee> enumerator4 = source2.Where(predicate2).GetEnumerator())
												{
													while (enumerator4.MoveNext())
													{
														ExternalAttendee member = enumerator4.Current;
														bool flag16 = list.Find((ExternalAttendee a) => a != null && a.Username != null && a.Username.Equals(member.Username, StringComparison.OrdinalIgnoreCase)) == null;
														if (flag16)
														{
															list.Add(member);
														}
													}
												}
											}
											bool flag17 = list.Find((ExternalAttendee a) => a != null && a.Username != null && a.Username.Equals(attendee.Address, StringComparison.OrdinalIgnoreCase)) == null;
											if (flag17)
											{
												list.Add(new ExternalAttendee
												{
													Username = attendee.Address,
													Name = attendee.Name
												});
											}
										}
									}
								}
							}
							catch (ServiceObjectPropertyException)
							{
							}
						}
					}
				}
				bool flag18 = app.Resources != null;
				if (flag18)
				{
					using (IEnumerator<Attendee> enumerator5 = app.Resources.GetEnumerator())
					{
						Func<ExternalAttendee, bool> <>9__9;
						while (enumerator5.MoveNext())
						{
							Attendee attendee = enumerator5.Current;
							try
							{
								bool flag19 = attendee == null;
								if (flag19)
								{
									CWLogger.Logger.Trace("ExchangeAppointmentMapper::ToDTO:: Attendee is NULL");
								}
								else
								{
									bool flag20 = string.IsNullOrEmpty(attendee.Address);
									if (flag20)
									{
										CWLogger.Logger.Trace("ExchangeAppointmentMapper::ToDTO:: Attendee.Address is NULL");
									}
									else
									{
										bool flag21;
										if (attendee.ResponseType != null)
										{
											MeetingResponseType? responseType = attendee.ResponseType;
											MeetingResponseType meetingResponseType = MeetingResponseType.Decline;
											flag21 = (responseType.GetValueOrDefault() == meetingResponseType & responseType != null);
										}
										else
										{
											flag21 = false;
										}
										bool flag22 = flag21;
										if (flag22)
										{
											CWLogger.Logger.Trace("ExchangeAppointmentMapper::ToDTO:: Attendee.ResponseType is Decline");
										}
										else
										{
											bool flag23 = attendee.MailboxType != null && (attendee.MailboxType.Value == MailboxType.ContactGroup || attendee.MailboxType.Value == MailboxType.PublicGroup);
											if (flag23)
											{
												IList<ExternalAttendee> groupMembers3 = syncContactsDAO.GetGroupMembers(attendee.Address);
												IEnumerable<ExternalAttendee> source3 = groupMembers3;
												Func<ExternalAttendee, bool> predicate3;
												if ((predicate3 = <>9__9) == null)
												{
													predicate3 = (<>9__9 = ((ExternalAttendee m) => opContext.SyncSettings.SyncUsers.Any((ClockWorkExternalApplicationSyncUser u) => u.ExternalApplicationUsername.Equals(m.Username, StringComparison.OrdinalIgnoreCase))));
												}
												using (IEnumerator<ExternalAttendee> enumerator6 = source3.Where(predicate3).GetEnumerator())
												{
													while (enumerator6.MoveNext())
													{
														ExternalAttendee member = enumerator6.Current;
														bool flag24 = list.Find((ExternalAttendee a) => a != null && a.Username != null && a.Username.Equals(member.Username, StringComparison.OrdinalIgnoreCase)) == null;
														if (flag24)
														{
															list.Add(member);
														}
													}
												}
											}
											bool flag25 = list.Find((ExternalAttendee a) => a != null && a.Username != null && a.Username.Equals(attendee.Address, StringComparison.OrdinalIgnoreCase)) == null;
											if (flag25)
											{
												list.Add(new ExternalAttendee
												{
													Username = attendee.Address,
													Name = attendee.Name
												});
											}
										}
									}
								}
							}
							catch (ServiceObjectPropertyException)
							{
							}
						}
					}
				}
				outapp.Attendees = list;
				ExternalAppointment outapp2 = outapp;
				ExternalAttendee organizer;
				if (app.Organizer == null)
				{
					organizer = null;
				}
				else
				{
					ExternalAttendee externalAttendee = new ExternalAttendee();
					externalAttendee.Username = app.Organizer.Address;
					externalAttendee.Name = app.Organizer.Name;
					organizer = externalAttendee;
					externalAttendee.AttendeeType = eAttendeeType.EVENT_ORGANIZER;
				}
				outapp2.Organizer = organizer;
				ExternalAttendee externalAttendee2 = (outapp.Organizer != null) ? outapp.Attendees.FirstOrDefault((ExternalAttendee a) => a.Username.Equals(outapp.Organizer.Username, StringComparison.OrdinalIgnoreCase)) : null;
				bool flag26 = externalAttendee2 != null;
				if (flag26)
				{
					externalAttendee2.AttendeeType = eAttendeeType.EVENT_ORGANIZER;
				}
				try
				{
					outapp.UniqueId = app.Id.UniqueId;
				}
				catch (ServiceObjectPropertyException)
				{
				}
				try
				{
					outapp.StartDate = app.Start;
				}
				catch (ServiceObjectPropertyException)
				{
				}
				try
				{
					outapp.EndDate = app.End;
				}
				catch (ServiceObjectPropertyException)
				{
				}
				try
				{
					outapp.Memo = ((app.Body != null) ? app.Body.GetMemoPlainText() : string.Empty);
				}
				catch (ServiceObjectPropertyException)
				{
					outapp.Memo = string.Empty;
				}
				try
				{
					outapp.Subject = app.Subject;
				}
				catch (ServiceObjectPropertyException)
				{
					outapp.Subject = string.Empty;
				}
				try
				{
					outapp.Location = app.Location;
				}
				catch (ServiceObjectPropertyException)
				{
					outapp.Location = string.Empty;
				}
				try
				{
					outapp.IsCancelled = app.IsCancelled;
				}
				catch (ServiceObjectPropertyException)
				{
					outapp.IsCancelled = false;
				}
				try
				{
					outapp.IsPrivate = (app.Sensitivity == Sensitivity.Private);
				}
				catch (ServiceObjectPropertyException)
				{
					outapp.IsPrivate = false;
				}
				try
				{
					outapp.LastModifiedTime = app.LastModifiedTime;
				}
				catch (ServiceObjectPropertyException)
				{
				}
				try
				{
					outapp.IsAllDayEvent = app.IsAllDayEvent;
				}
				catch (ServiceObjectPropertyException)
				{
					outapp.IsAllDayEvent = false;
				}
				try
				{
					outapp.AppointmentType = (ExternalAppointmentType)app.AppointmentType;
				}
				catch (Exception)
				{
					outapp.AppointmentType = ExternalAppointmentType.Single;
				}
				try
				{
					outapp.IsRecurring = (app.IsRecurring || app.AppointmentType > AppointmentType.Single);
				}
				catch (ServiceObjectPropertyException)
				{
					outapp.IsRecurring = (app.AppointmentType > AppointmentType.Single);
				}
				outapp.LegacyGlobalAppointmentId = app.GetGlobalAppointmentId(true);
				outapp.UniqueId2 = app.GetUniqueAppointmentId(true);
				bool flag27 = outapp.Subject == null;
				if (flag27)
				{
					outapp.Subject = string.Empty;
				}
				bool flag28 = outapp.Location == null;
				if (flag28)
				{
					outapp.Location = string.Empty;
				}
				bool flag29 = outapp.Memo == null;
				if (flag29)
				{
					outapp.Memo = string.Empty;
				}
				result = outapp;
			}
			return result;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00005A50 File Offset: 0x00003C50
		public static Appointment ToDomainObject(this ExternalAppointment outapp, ExchangeAppointmentDAO exchangeAppointmentDAO)
		{
			Appointment appointment = new Appointment(exchangeAppointmentDAO.ExchangeService)
			{
				Start = outapp.StartDate,
				End = outapp.EndDate,
				Body = new MessageBody(BodyType.Text, outapp.Memo),
				Subject = outapp.Subject,
				Location = outapp.Location,
				IsAllDayEvent = outapp.IsAllDayEvent
			};
			foreach (ExternalAttendee externalAttendee in outapp.Attendees)
			{
				appointment.RequiredAttendees.Add(new Attendee(new EmailAddress(externalAttendee.Username))
				{
					RoutingType = "SMTP",
					Name = externalAttendee.Name
				});
			}
			bool isPrivate = outapp.IsPrivate;
			if (isPrivate)
			{
				appointment.Sensitivity = Sensitivity.Private;
			}
			bool flag = outapp.Mapping != null && outapp.Mapping.ClockWorkAppointmentId > 0;
			if (flag)
			{
				appointment.SetClockWorkAppointmentId(outapp.Mapping.ClockWorkAppointmentId, exchangeAppointmentDAO.ExtendedPropertyDef);
			}
			return appointment;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00005B84 File Offset: 0x00003D84
		public static Appointment ToDomainObject(this ExternalAppointment appFromClockWork, Appointment exchangeApp, ExchangeAppointmentDAO exchangeAppointmentDAO)
		{
			List<ClockWorkExternalApplicationSyncUser> syncUsers = exchangeAppointmentDAO.OpContext.SyncSettings.SyncUsers;
			exchangeApp.StartTimeZone = TimeZoneInfo.Local;
			exchangeApp.Start = appFromClockWork.StartDate;
			exchangeApp.End = appFromClockWork.EndDate;
			exchangeApp.Body = new MessageBody(BodyType.Text, appFromClockWork.Memo);
			exchangeApp.Subject = appFromClockWork.Subject;
			exchangeApp.Location = appFromClockWork.Location;
			List<Attendee> attendeeList = exchangeApp.RequiredAttendees.ToList<Attendee>();
			attendeeList.AddRange(exchangeApp.OptionalAttendees);
			List<Attendee> list = (from att in attendeeList
			where !appFromClockWork.Attendees.Any((ExternalAttendee a) => a.Username.Equals(att.Address, StringComparison.OrdinalIgnoreCase)) && syncUsers.Any((ClockWorkExternalApplicationSyncUser u) => u.ExternalApplicationUsername.Equals(att.Address, StringComparison.OrdinalIgnoreCase))
			select att).ToList<Attendee>();
			IEnumerable<ExternalAttendee> enumerable = from a in appFromClockWork.Attendees
			where !attendeeList.Any((Attendee att) => att.Address.Equals(a.Username, StringComparison.OrdinalIgnoreCase)) && syncUsers.Any((ClockWorkExternalApplicationSyncUser u) => u.ExternalApplicationUsername.Equals(a.Username, StringComparison.OrdinalIgnoreCase))
			select a;
			foreach (Attendee attendee in list)
			{
				bool flag = exchangeApp.RequiredAttendees.Contains(attendee);
				if (flag)
				{
					exchangeApp.RequiredAttendees.Remove(attendee);
				}
				bool flag2 = exchangeApp.OptionalAttendees.Contains(attendee);
				if (flag2)
				{
					exchangeApp.OptionalAttendees.Remove(attendee);
				}
			}
			foreach (ExternalAttendee externalAttendee in enumerable)
			{
				exchangeApp.RequiredAttendees.Add(new Attendee(new EmailAddress(externalAttendee.Username))
				{
					RoutingType = "SMTP",
					Name = externalAttendee.Name
				});
			}
			bool isPrivate = appFromClockWork.IsPrivate;
			if (isPrivate)
			{
				exchangeApp.Sensitivity = Sensitivity.Private;
			}
			else
			{
				bool flag3 = exchangeApp.Sensitivity == Sensitivity.Private;
				if (flag3)
				{
					exchangeApp.Sensitivity = Sensitivity.Normal;
				}
			}
			bool flag4 = appFromClockWork.Mapping != null && appFromClockWork.Mapping.ClockWorkAppointmentId > 0;
			if (flag4)
			{
				exchangeApp.SetClockWorkAppointmentId(appFromClockWork.Mapping.ClockWorkAppointmentId, exchangeAppointmentDAO.ExtendedPropertyDef);
			}
			return exchangeApp;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00005DF4 File Offset: 0x00003FF4
		internal static void LogExchangeApp(Appointment app)
		{
			try
			{
				CWLogger.Logger.Trace("-------------------------- BEGIN -----------------------------");
				CWLogger.Logger.Trace("ExchangeAppointmentMapper::Appointment ({0})", app.Id.UniqueId);
				CWLogger.Logger.Trace("Required Attendees ({0})", (app.RequiredAttendees == null) ? 0 : app.RequiredAttendees.Count);
				bool flag = app.RequiredAttendees != null;
				if (flag)
				{
					for (int i = 0; i < app.RequiredAttendees.Count; i++)
					{
						Attendee attendee = app.RequiredAttendees[i];
						bool flag2 = attendee == null;
						if (flag2)
						{
							CWLogger.Logger.Trace("{0}- NULL attendee", i);
						}
						else
						{
							CWLogger.Logger.Trace(string.Format("{0}- Address = '{1}', ProductName = '{2}', ResponseType = '{3}', MailboxType = '{4}'", new object[]
							{
								i,
								attendee.Address ?? "NULL",
								attendee.Name ?? "NULL",
								attendee.ResponseType.GetValueOrDefault(),
								attendee.MailboxType.GetValueOrDefault()
							}));
						}
					}
				}
				CWLogger.Logger.Trace("Optional Attendees ({0})", (app.OptionalAttendees == null) ? 0 : app.OptionalAttendees.Count);
				bool flag3 = app.OptionalAttendees != null;
				if (flag3)
				{
					for (int j = 0; j < app.OptionalAttendees.Count; j++)
					{
						Attendee attendee2 = app.OptionalAttendees[j];
						bool flag4 = attendee2 == null;
						if (flag4)
						{
							CWLogger.Logger.Trace("{0}- NULL attendee", j);
						}
						else
						{
							CWLogger.Logger.Trace(string.Format("{0}- Address = '{1}', ProductName = '{2}', ResponseType = '{3}', MailboxType = '{4}'", new object[]
							{
								j,
								attendee2.Address ?? "NULL",
								attendee2.Name ?? "NULL",
								attendee2.ResponseType.GetValueOrDefault(),
								attendee2.MailboxType.GetValueOrDefault()
							}));
						}
					}
				}
				CWLogger.Logger.Trace("Resources ({0})", (app.Resources == null) ? 0 : app.Resources.Count);
				bool flag5 = app.Resources != null;
				if (flag5)
				{
					for (int k = 0; k < app.Resources.Count; k++)
					{
						Attendee attendee3 = app.Resources[k];
						bool flag6 = attendee3 == null;
						if (flag6)
						{
							CWLogger.Logger.Trace("{0}- NULL attendee", k);
						}
						else
						{
							CWLogger.Logger.Trace(string.Format("{0}- Address = '{1}', ProductName = '{2}', ResponseType = '{3}', MailboxType = '{4}'", new object[]
							{
								k,
								attendee3.Address ?? "NULL",
								attendee3.Name ?? "NULL",
								attendee3.ResponseType.GetValueOrDefault(),
								attendee3.MailboxType.GetValueOrDefault()
							}));
						}
					}
				}
				CWLogger.Logger.Trace("Subject: {0}", app.Subject ?? "NULL");
				CWLogger.Logger.Trace("Start Time: {0}", app.Start.ToString("MMM dd, yyyy hh:mm:ss tt"));
				CWLogger.Logger.Trace("End Time: {0}", app.End.ToString("MMM dd, yyyy hh:mm:ss tt"));
				CWLogger.Logger.Trace("Organizer: {0}", (app.Organizer != null) ? (app.Organizer.Address ?? "NULL") : "NULL");
				CWLogger.Logger.Trace("-------------------------- END -------------------------------");
			}
			catch (Exception ex)
			{
				CWLogger.Logger.WarnException(string.Format("ExchangeAppointmentMapper::LogApp:: {0}", ex.ToString()), ex);
			}
		}
	}
}
