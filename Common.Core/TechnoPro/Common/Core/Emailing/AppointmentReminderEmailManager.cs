using System;
using System.Collections.Generic;
using System.Linq;
using ClockWorkLogger;
using TechnoPro.Common.Core.Appointments;
using TechnoPro.Common.Core.AppointmentsCalendar;
using TechnoPro.Common.Core.AppointmentsPointOfContact;
using TechnoPro.Common.Core.MailMerging;
using TechnoPro.Common.DAO.Email;
using TechnoPro.Common.DAO.Impl.Email;
using TechnoPro.Common.ICore;
using TechnoPro.Common.ICore.Appointments;
using TechnoPro.Common.ICore.AppointmentsCalendar;
using TechnoPro.Common.ICore.AppointmentsPointOfContact;
using TechnoPro.Common.ICore.Emailing;
using TechnoPro.Common.ICore.MailMerging;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsCalendar;
using TechnoPro.Common.Public.Entities.AppointmentsPointOfContact;
using TechnoPro.Common.Public.Entities.MailMergeEntities;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.TPMailMan;

namespace TechnoPro.Common.Core.Emailing
{
	// Token: 0x020000F4 RID: 244
	public class AppointmentReminderEmailManager : IAppointmentReminderEmailManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000986 RID: 2438 RVA: 0x0003C3A1 File Offset: 0x0003A5A1
		public AppointmentReminderEmailManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000987 RID: 2439 RVA: 0x0003C3B3 File Offset: 0x0003A5B3
		// (set) Token: 0x06000988 RID: 2440 RVA: 0x0003C3BB File Offset: 0x0003A5BB
		public OperationContext OpContext { get; set; }

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000989 RID: 2441 RVA: 0x0003C3C4 File Offset: 0x0003A5C4
		private IMailMergingEmailManager mailMergingEmailManager
		{
			get
			{
				bool flag = this._em == null;
				if (flag)
				{
					this._em = new MailMergingEmailManager(this.OpContext);
				}
				return this._em;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600098A RID: 2442 RVA: 0x0003C3FC File Offset: 0x0003A5FC
		private IEmailManager emailManager
		{
			get
			{
				bool flag = this._emailMan == null;
				if (flag)
				{
					this._emailMan = new EmailManager(this.OpContext);
				}
				return this._emailMan;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600098B RID: 2443 RVA: 0x0003C434 File Offset: 0x0003A634
		private IPointOfContactManager pointOfContactManager
		{
			get
			{
				bool flag = this._pocMan == null;
				if (flag)
				{
					this._pocMan = new PointOfContactManager(this.OpContext);
				}
				return this._pocMan;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600098C RID: 2444 RVA: 0x0003C46C File Offset: 0x0003A66C
		private IAppointmentIconManager appointmentIconManager
		{
			get
			{
				bool flag = this._aim == null;
				if (flag)
				{
					this._aim = new AppointmentIconManager(this.OpContext);
				}
				return this._aim;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x0600098D RID: 2445 RVA: 0x0003C4A4 File Offset: 0x0003A6A4
		private IAppointmentReminderEmailDAO appointmentReminderEmailDao
		{
			get
			{
				bool flag = this._redao == null;
				if (flag)
				{
					this._redao = new AppointmentReminderEmailDAO(this.OpContext);
				}
				return this._redao;
			}
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x0003C4DC File Offset: 0x0003A6DC
		private TPMailResult SendEmail(int pid, int appId, int EmailTemplateId, string TestModeEmail, bool CopyEmailToPointOfContact, int IconIdToIndicateEmailWasSent, string BatchEmailTitle)
		{
			MailMergeContextWithCustomDictionary mailMergeContextWithCustomDictionary = new MailMergeContextWithCustomDictionary
			{
				CustomDictionary = new MailMergeCustomDictionary
				{
					Args = new Dictionary<string, string>()
				},
				Context = new MailMergeContext
				{
					AppointmentId = appId,
					PersonId = pid
				}
			};
			TPMailMessage tpmailMessage = this.mailMergingEmailManager.MailMerge(mailMergeContextWithCustomDictionary, EmailTemplateId);
			bool flag = tpmailMessage == null;
			TPMailResult result;
			if (flag)
			{
				result = new TPMailResult
				{
					ErrorMessage = "Failed mail merge",
					Id = mailMergeContextWithCustomDictionary.Context.AppointmentId.ToString() + "." + mailMergeContextWithCustomDictionary.Context.PersonId.ToString(),
					Status = eTPMailResultStatus.Failed
				};
			}
			else
			{
				bool flag2 = !string.IsNullOrEmpty(TestModeEmail);
				if (flag2)
				{
					tpmailMessage.Subject = string.Concat(new string[]
					{
						"TEST MODE: ",
						tpmailMessage.Subject ?? "",
						"to=",
						tpmailMessage.To.GetEmailList(),
						"; cc=",
						tpmailMessage.Cc.GetEmailList(),
						"; bcc=",
						tpmailMessage.Bcc.GetEmailList()
					});
					tpmailMessage.To = new List<TPMailAddress>
					{
						new TPMailAddress
						{
							EmailAddress = TestModeEmail
						}
					};
					tpmailMessage.Cc = new List<TPMailAddress>();
					tpmailMessage.Bcc = new List<TPMailAddress>();
				}
				bool flag3 = tpmailMessage.From == null || string.IsNullOrEmpty(tpmailMessage.From.EmailAddress);
				if (flag3)
				{
				}
				bool flag4 = tpmailMessage.From == null || string.IsNullOrEmpty(tpmailMessage.From.EmailAddress);
				if (flag4)
				{
					bool flag5 = tpmailMessage.To != null && tpmailMessage.To.Count > 0;
					if (flag5)
					{
						tpmailMessage.From = tpmailMessage.To[0];
					}
				}
				CWLogger.Logger.Trace("AppointmentReminderEmailManager:SendEmail:EmailToSend={0}", tpmailMessage.ConvertToDisplayString());
				TPMailResult tpmailResult = this.emailManager.SendEmail(tpmailMessage);
				bool flag6 = tpmailResult != null && tpmailResult.Status == eTPMailResultStatus.CompletedSuccess;
				if (flag6)
				{
					if (CopyEmailToPointOfContact)
					{
						try
						{
							this.pointOfContactManager.SaveEmailAsPointOfContact(false, mailMergeContextWithCustomDictionary.Context.PersonId, 1, tpmailMessage, ePointOfContactContext.AutomaticSystemCreated);
						}
						catch (Exception ex)
						{
							CWLogger.Logger.Error("SendAppointmentReminderEmailsToStudents:CreatePointOfContactFromEmail:{0}", ex.ToString());
						}
					}
					bool flag7 = IconIdToIndicateEmailWasSent >= 0;
					if (flag7)
					{
						try
						{
							IconInfo icon = new IconInfo
							{
								IconNum = IconIdToIndicateEmailWasSent
							};
							this.appointmentIconManager.InsertOrUpdateAppointmentIcon(false, mailMergeContextWithCustomDictionary.Context.AppointmentId, new AppointmentIcon
							{
								Icon = icon
							});
						}
						catch (Exception ex2)
						{
							CWLogger.Logger.Error("SendAppointmentReminderEmailsToStudents.InsertOrUpdateAppointmentIcon:{0}", ex2.ToString());
						}
					}
					bool flag8 = !string.IsNullOrEmpty(BatchEmailTitle);
					if (flag8)
					{
						try
						{
							this.appointmentReminderEmailDao.LogEmailSent(mailMergeContextWithCustomDictionary.Context.PersonId, BatchEmailTitle, tpmailMessage, "", EmailTemplateId);
						}
						catch (Exception ex3)
						{
							CWLogger.Logger.Error("SendAppointmentReminderEmailsToStudents.LogEmailSent:{0}", ex3.ToString());
						}
					}
				}
				result = tpmailResult;
			}
			return result;
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0003C838 File Offset: 0x0003AA38
		public IList<TPMailResult> SendAppointmentReminderEmailsToStudents(DateTime DayToSendRemindersFor, int EmailTemplateId, string BatchEmailTitle, bool CopyEmailToPointOfContact = true, int IconIdToIndicateEmailWasSent = 121, int[] AppTypeIds = null, string TestModeEmail = null)
		{
			List<TPMailResult> list = new List<TPMailResult>();
			IAppointmentManager appointmentManager = new AppointmentManager(this.OpContext);
			IList<Appointment> list2 = appointmentManager.LoadAllAppointmentsInADay(DayToSendRemindersFor.Date, false, 1, AppTypeIds);
			Func<AppointmentIcon, bool> <>9__2;
			list2 = list2.Where(delegate(Appointment g)
			{
				bool result2;
				if (g.Icons != null)
				{
					IEnumerable<AppointmentIcon> icons = g.Icons;
					Func<AppointmentIcon, bool> predicate;
					if ((predicate = <>9__2) == null)
					{
						predicate = (<>9__2 = ((AppointmentIcon h) => h.IconNum == IconIdToIndicateEmailWasSent));
					}
					result2 = (icons.FirstOrDefault(predicate) == null);
				}
				else
				{
					result2 = true;
				}
				return result2;
			}).ToList<Appointment>();
			bool flag = list2.Count < 1;
			IList<TPMailResult> result;
			if (flag)
			{
				CWLogger logger = CWLogger.Logger;
				string message = "SendAppointmentReminderEmailsToStudents:Date={0}:AppTypeIds={1}:No Appointments were found; no emails were sent.";
				object arg = DayToSendRemindersFor.ToString("yyyy-MM-dd");
				object arg2;
				if (AppTypeIds != null)
				{
					arg2 = string.Join(", ", AppTypeIds.ToList<int>().ConvertAll<string>((int g) => g.ToString()).ToArray());
				}
				else
				{
					arg2 = "NULL";
				}
				logger.Trace(message, arg, arg2);
				result = list;
			}
			else
			{
				foreach (Appointment appointment in list2)
				{
					List<PersonBase> students = appointment.GetStudents();
					foreach (PersonBase personBase in students)
					{
						TPMailResult tpmailResult = this.SendEmail(personBase.PersonId, appointment.AppointmentId, EmailTemplateId, TestModeEmail, CopyEmailToPointOfContact, IconIdToIndicateEmailWasSent, BatchEmailTitle);
						bool flag2 = tpmailResult != null;
						if (flag2)
						{
							list.Add(tpmailResult);
						}
					}
				}
				result = list;
			}
			return result;
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x0003C9D0 File Offset: 0x0003ABD0
		public IList<TPMailResult> SendNoshowReminderEmailsToStudents(int EmailTemplateId, string BatchEmailTitle, bool CopyEmailToPointOfContact = true, int IconIdToIndicateEmailWasSent = 121, int[] AppTypeIds = null, string TestModeEmail = null, DateTime? minimumDateToCheckFrom = null)
		{
			List<TPMailResult> list = new List<TPMailResult>();
			IAppointmentAttendeeManager appointmentAttendeeManager = new AppointmentAttendeeManager(this.OpContext);
			IList<AttendeeWithAppointmentId> list2 = appointmentAttendeeManager.LoadAttendeesWhoHaveNoShowedInThePast(new DateTime?((minimumDateToCheckFrom != null) ? minimumDateToCheckFrom.Value : DateTime.Now.Date.AddDays(-14.0)), IconIdToIndicateEmailWasSent, AppTypeIds);
			bool flag = list2.Count < 1;
			IList<TPMailResult> result;
			if (flag)
			{
				CWLogger.Logger.Trace("SendNoshowReminderEmailsToStudents:No no-show attendees were found; no emails were sent.");
				result = list;
			}
			else
			{
				foreach (AttendeeWithAppointmentId attendeeWithAppointmentId in list2)
				{
					int appointmentId = attendeeWithAppointmentId.AppointmentId;
					int personId = attendeeWithAppointmentId.Person.PersonId;
					TPMailResult tpmailResult = this.SendEmail(attendeeWithAppointmentId.Person.PersonId, attendeeWithAppointmentId.AppointmentId, EmailTemplateId, TestModeEmail, CopyEmailToPointOfContact, IconIdToIndicateEmailWasSent, BatchEmailTitle);
					bool flag2 = tpmailResult != null;
					if (flag2)
					{
						list.Add(tpmailResult);
					}
				}
				result = list;
			}
			return result;
		}

		// Token: 0x040001A8 RID: 424
		private IMailMergingEmailManager _em;

		// Token: 0x040001A9 RID: 425
		private IEmailManager _emailMan;

		// Token: 0x040001AA RID: 426
		private IPointOfContactManager _pocMan;

		// Token: 0x040001AB RID: 427
		private IAppointmentIconManager _aim;

		// Token: 0x040001AC RID: 428
		private IAppointmentReminderEmailDAO _redao;
	}
}
