using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechnoPro.Common.Core.Appointments;
using TechnoPro.Common.Core.AppointmentsTestBooking;
using TechnoPro.Common.Core.People;
using TechnoPro.Common.DAO.DynamicForms;
using TechnoPro.Common.DAO.Impl.DynamicForms;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.ICore.Appointments;
using TechnoPro.Common.ICore.AppointmentsTestBooking;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.ICore.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.DynamicForms.AppointmentNotes;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.TextFormat.Adapters;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.Core.DynamicForms
{
	// Token: 0x020000F8 RID: 248
	public class AppointmentNotesManager : IAppointmentNotesManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060009AF RID: 2479 RVA: 0x0003CF83 File Offset: 0x0003B183
		public AppointmentNotesManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new AppointmentNotesDAO(this.OpContext);
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060009B0 RID: 2480 RVA: 0x0003CFA6 File Offset: 0x0003B1A6
		// (set) Token: 0x060009B1 RID: 2481 RVA: 0x0003CFAE File Offset: 0x0003B1AE
		public OperationContext OpContext { get; set; }

		// Token: 0x060009B2 RID: 2482 RVA: 0x0003CFB8 File Offset: 0x0003B1B8
		private DynamicField GetFirstRtfFieldForAppTypeId(int AppTypeId)
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			string key = "FirstRtfOnPerAppFormOnAppTypeId";
			DynamicField dynamicField = (DynamicField)cacheStorageManager[key];
			bool flag = dynamicField == null;
			if (flag)
			{
				IDynamicFieldManager dynamicFieldManager = new DynamicFieldManager(this.OpContext);
				dynamicField = dynamicFieldManager.GetFirstFieldOnFirstPerAppointmentForm(AppTypeId, eControlCode.RtfTextBox);
				bool flag2 = dynamicField != null;
				if (flag2)
				{
					cacheStorageManager.Insert(key, dynamicField);
				}
			}
			return dynamicField;
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x0003D020 File Offset: 0x0003B220
		public IList<NotesAppointment> LoadNotesAppointmentsForStudentNoAttendees(int primaryStudentPersonId, Range<DateTime> dateRange, IList<int> appTypeIds, IList<int> screenNums)
		{
			IAppointmentNotesDAO appointmentNotesDAO = new AppointmentNotesDAO(this.OpContext);
			IList<NotesAppointment> list = appointmentNotesDAO.LoadNotesAppointmentsForStudentNoAttendeesNoHasNotes(primaryStudentPersonId, dateRange, appTypeIds);
			IList<int> list2 = this.LoadAllAppointmentIdsWithNotes(primaryStudentPersonId, dateRange, (screenNums != null) ? screenNums.ToArray<int>() : null);
			foreach (NotesAppointment notesAppointment in list)
			{
				notesAppointment.HasNotes = list2.Contains(notesAppointment.AppointmentId);
			}
			return list;
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x0003D0B0 File Offset: 0x0003B2B0
		public NotesAppointment LoadNotesAppointmentByAppointmentId(int appointmentId, int primaryStudentPersonId, IList<int> screenNums)
		{
			IAppointmentNotesDAO appointmentNotesDAO = new AppointmentNotesDAO(this.OpContext);
			NotesAppointment notesAppointment = appointmentNotesDAO.LoadNotesAppointmentByAppointmentId(appointmentId);
			bool flag = notesAppointment == null;
			NotesAppointment result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = notesAppointment.PrimaryStudent == null || notesAppointment.PrimaryStudent.PersonId != primaryStudentPersonId;
				if (flag2)
				{
					Attendee attendee = notesAppointment.Attendees.FirstOrDefault((Attendee g) => g.Person.PersonId == primaryStudentPersonId);
					bool flag3 = attendee != null;
					if (flag3)
					{
						notesAppointment.PrimaryStudent = attendee.Person;
						notesAppointment.IsPrimaryStudentNoShow = attendee.IsNoShow;
					}
				}
				IList<int> list = this.LoadAllAppointmentIdsWithNotes(primaryStudentPersonId, new Range<DateTime>(notesAppointment.StartDateTime.Date, notesAppointment.StartDateTime.Date), (screenNums != null) ? screenNums.ToArray<int>() : null);
				notesAppointment.HasNotes = (list != null && list.Any((int g) => g == notesAppointment.AppointmentId));
				result = notesAppointment;
			}
			return result;
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x0003D1F4 File Offset: 0x0003B3F4
		public IList<NotesAppointmentExtendedInfo> LoadNotesAppointmentExtendedInfos(params int[] appointmentIds)
		{
			List<int> list;
			if (appointmentIds == null)
			{
				list = null;
			}
			else
			{
				list = (from g in appointmentIds.Distinct<int>()
				where g > 0
				select g).ToList<int>();
			}
			List<int> list2 = list ?? new List<int>();
			bool flag = list2.Count < 1;
			IList<NotesAppointmentExtendedInfo> result;
			if (flag)
			{
				result = new List<NotesAppointmentExtendedInfo>();
			}
			else
			{
				IAppointmentAttendeeManager appointmentAttendeeManager = new AppointmentAttendeeManager(this.OpContext);
				IDictionary<int, IList<Attendee>> source = appointmentAttendeeManager.LoadAttendeesByAppointmentIds(list2);
				IStudentClassTestInfoManager studentClassTestInfoManager = new StudentClassTestInfoManager(this.OpContext);
				IDictionary<int, StudentClassTest> studentTestInfos = studentClassTestInfoManager.LoadClassTestsByAppointmentIds(list2.ToArray());
				result = (from g in source
				select new NotesAppointmentExtendedInfo
				{
					AppointmentId = g.Key,
					Attendees = g.Value,
					StudentClassTestInfo = (studentTestInfos.ContainsKey(g.Key) ? studentTestInfos[g.Key] : null)
				}).ToList<NotesAppointmentExtendedInfo>();
			}
			return result;
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x0003D2B0 File Offset: 0x0003B4B0
		public IList<int> LoadAllAppointmentIdsWithNotes(int PersonId, Range<DateTime> DateRange, params int[] ScreenNums)
		{
			IAppointmentTypeManager appointmentTypeManager = new AppointmentTypeManager(this.OpContext);
			IList<int> allowedAppTypeIds = appointmentTypeManager.GetAllowedAppTypeIds(this.OpContext.WhoAmI);
			return this.dao.LoadAllAppointmentIdsWithNotes(PersonId, DateRange, allowedAppTypeIds, ScreenNums);
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x0003D2F0 File Offset: 0x0003B4F0
		public string GetAppointmentNotesSummaryHtml(int PersonId, int[] AppointmentIds, int[] ScreenNums)
		{
			IAppointmentTypeManager appointmentTypeManager = new AppointmentTypeManager(this.OpContext);
			IList<int> allowedAppTypeIds = appointmentTypeManager.GetAllowedAppTypeIds(this.OpContext.WhoAmI);
			IBaseAppointmentManager baseAppointmentManager = new BaseAppointmentManager(this.OpContext);
			List<BaseExtendedAppointment> list = (from g in baseAppointmentManager.LoadBaseExtendedAppointmentsByAppointmentIds<BaseExtendedAppointment>(AppointmentIds)
			where allowedAppTypeIds.Contains((g.AppType == null) ? -1 : g.AppType.AppTypeId)
			select g).ToList<BaseExtendedAppointment>();
			list.Sort((BaseExtendedAppointment g1, BaseExtendedAppointment g2) => g2.StartDateTime.CompareTo(g1.StartDateTime));
			IPeopleManager peopleManager = new PeopleManager(this.OpContext);
			PersonBase personBase = peopleManager.LoadPerson(PersonId);
			IDynamicDataManager dynamicDataManager = new DynamicDataManager(this.OpContext);
			IList<DynamicDataSet> source = dynamicDataManager.LoadData(PersonId, AppointmentIds, ScreenNums.ToList<int>(), eDynamicFormType.PerAppointment);
			StringBuilder stringBuilder = new StringBuilder();
			string format = "<h1>{0} {1}{2} ({3})</h1>\r\n";
			string format2 = "<h2>{0}</h2>\r\n";
			string format3 = "<h2>{0} . {1} to {2} ({3})</h2>\r\n";
			string format4 = "dddd MMMM d, yyyy";
			string format5 = "h:mm tt";
			string text = "<b>Appointment Type:</b> {0}<br />\r\n<b>Service Provider:</b> {1}<br />{2}{3}{4}";
			string format6 = "<div style='padding-top: 8px; padding-bottom: 8px;'><b>Notes:</b><br />\r\n{0}</div>";
			string format7 = "<b>{0}:</b> {1}<br />";
			string format8 = "{0}<br />";
			stringBuilder.AppendFormat(format, new object[]
			{
				personBase.FirstName ?? "",
				string.IsNullOrEmpty(personBase.MiddleName) ? "" : (personBase.MiddleName + " "),
				personBase.LastName ?? "",
				personBase.Student_no ?? ""
			});
			using (List<BaseExtendedAppointment>.Enumerator enumerator = list.GetEnumerator())
			{
				Predicate<Attendee> <>9__4;
				while (enumerator.MoveNext())
				{
					BaseExtendedAppointment app = enumerator.Current;
					List<DynamicDataSet> list2 = (from g in source
					where ((g.Context == null) ? 0 : g.Context.SecondaryId) == app.AppointmentId
					select g).ToList<DynamicDataSet>();
					bool isPointOfContact = app.IsPointOfContact;
					if (isPointOfContact)
					{
						stringBuilder.AppendFormat(format2, app.StartDateTime.ToString("dddd MMMM d, yyyy"));
					}
					else
					{
						stringBuilder.AppendFormat(format3, new object[]
						{
							app.StartDateTime.ToString(format4),
							app.StartDateTime.ToString(format5),
							app.EndDateTime.ToString(format5),
							app.GetDurationInMinutes().GetDurationDescription()
						});
					}
					string text2 = (app.AppType == null) ? "" : (app.AppType.Description ?? "");
					string text3 = app.SubTitle ?? "";
					bool flag = text2.Length < 1;
					if (flag)
					{
						text2 = text3;
					}
					else
					{
						bool flag2 = text3.Length > 0;
						if (flag2)
						{
							text2 = text2 + ": " + text3;
						}
					}
					List<PersonBase> list3 = new List<PersonBase>();
					bool flag3 = app.Attendees != null;
					if (flag3)
					{
						using (List<Attendee>.Enumerator enumerator2 = app.Attendees.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								Attendee att = enumerator2.Current;
								bool flag4 = att.Person.CoreGroup == eCoreGroup.Staff && list3.Find((PersonBase m) => m.PersonId == att.Person.PersonId) == null;
								if (flag4)
								{
									list3.Add(att.Person);
								}
							}
						}
					}
					list3.Sort((PersonBase p1, PersonBase p2) => p1.GetName().CompareTo(p2.GetName()));
					string text4 = (app.ShowTimeAs == null) ? "" : (app.ShowTimeAs.Title ?? "");
					string text5 = (app.CancelInfo == null || app.CancelInfo.CancelReason == null) ? "" : (app.CancelInfo.CancelReason.CancelReasonTitle ?? "");
					string text6 = (app.CancelInfo == null) ? "" : (app.CancelInfo.CancelReasonText ?? "");
					bool flag5 = text6.Length > 0;
					if (flag5)
					{
						text5 = ((text5.Length > 0) ? (text5 + ": " + text6) : text6);
					}
					Attendee attendee;
					if (app.Attendees != null)
					{
						List<Attendee> attendees = app.Attendees;
						Predicate<Attendee> match;
						if ((match = <>9__4) == null)
						{
							match = (<>9__4 = ((Attendee g) => g.Person.PersonId == PersonId));
						}
						attendee = attendees.Find(match);
					}
					else
					{
						attendee = null;
					}
					Attendee attendee2 = attendee;
					bool flag6 = attendee2 != null && attendee2.IsNoShow;
					bool flag7 = flag6;
					string text7;
					if (flag7)
					{
						text7 = "No-Show";
					}
					else
					{
						bool isCancelled = app.IsCancelled;
						if (isCancelled)
						{
							text7 = "Cancelled";
						}
						else
						{
							text7 = "";
						}
					}
					StringBuilder stringBuilder2 = stringBuilder;
					string format9 = text;
					object[] array = new object[5];
					array[0] = text2;
					array[1] = string.Join("; ", list3.ConvertAll<string>((PersonBase g) => g.GetName()).ToArray());
					array[2] = ((text7.Length > 0) ? ("<b>Status:</b> " + text7 + "<br />") : "");
					array[3] = ((text4.Length > 0) ? ("<b>Type:</b> " + text4 + "<br />") : "");
					array[4] = ((text5.Length > 0) ? ("<b>Cancel reason:</b> " + text5 + "<br /") : "");
					stringBuilder2.AppendFormat(format9, array);
					bool flag8 = list2 != null && list2.Any<DynamicDataSet>();
					if (flag8)
					{
						StringBuilder stringBuilder3 = new StringBuilder();
						foreach (DynamicDataSet dynamicDataSet in list2)
						{
							foreach (DynamicData dynamicData in dynamicDataSet.Data)
							{
								bool flag9 = dynamicData.Field.ControlCode == eControlCode.RtfTextBox && dynamicData.Value != null;
								if (flag9)
								{
									string text8 = dynamicData.Value.ToString();
									string arg;
									try
									{
										arg = text8.ConvertRtfToHtmlBodyInnerHtml();
									}
									catch
									{
										arg = text8;
									}
									stringBuilder3.AppendFormat(format8, arg);
								}
								else
								{
									bool flag10 = dynamicData.Field.ControlCode == eControlCode.CheckBox;
									if (flag10)
									{
										bool flag11 = dynamicData.Value == null || dynamicData.Value == DBNull.Value;
										bool flag12;
										if (flag11)
										{
											flag12 = false;
										}
										else
										{
											bool flag13 = dynamicData.Value is bool;
											if (flag13)
											{
												flag12 = (bool)dynamicData.Value;
											}
											else
											{
												bool flag14 = dynamicData.Value is int;
												flag12 = (flag14 && Convert.ToBoolean((int)dynamicData.Value));
											}
										}
										bool flag15 = flag12;
										if (flag15)
										{
											stringBuilder3.AppendFormat(format8, string.Format("<input type='checkbox' name='{0}' checked='checked' /> {1}", "c_" + dynamicData.DataId.ToString(), dynamicData.Field.GetCaptionForDisplay()));
										}
									}
									else
									{
										string captionForDisplay = dynamicData.Field.GetCaptionForDisplay();
										string @string = dynamicData.GetString();
										stringBuilder3.AppendFormat(format7, captionForDisplay ?? "", @string ?? "");
									}
								}
							}
						}
						stringBuilder.AppendFormat(format6, stringBuilder3);
					}
					stringBuilder.Append("<br />");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x0003DB98 File Offset: 0x0003BD98
		public void SaveAppointmentNotesToFirstRtfInFirstFormAttachedToAppointmentType(int StudentPersonId, int AppointmentId, int AppTypeId, string NotesRtf)
		{
			bool flag = NotesRtf == null;
			if (flag)
			{
				NotesRtf = "";
			}
			DynamicField firstRtfFieldForAppTypeId = this.GetFirstRtfFieldForAppTypeId(AppTypeId);
			bool flag2 = firstRtfFieldForAppTypeId == null;
			if (flag2)
			{
				IBaseAppointmentManager baseAppointmentManager = new BaseAppointmentManager(this.OpContext);
				baseAppointmentManager.InsertOrUpdateAppointmentMemo(false, AppointmentId, NotesRtf);
			}
			else
			{
				IDynamicDataManager dynamicDataManager = new DynamicDataManager(this.OpContext);
				DynamicDataContext context = new DynamicDataContext
				{
					PrimaryId = StudentPersonId,
					SecondaryId = AppointmentId
				};
				DynamicData item = new DynamicData
				{
					Field = firstRtfFieldForAppTypeId,
					Value = NotesRtf
				};
				dynamicDataManager.SaveData(context, new List<DynamicData>
				{
					item
				}, eDynamicFormType.PerAppointment);
			}
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x0003DC38 File Offset: 0x0003BE38
		public string LoadAppointmentNotesRtfFromFirstRtfInFirstFormAttachedToAppointmentType(int StudentPersonId, int AppointmentId, int AppTypeId)
		{
			DynamicField firstRtfFieldForAppTypeId = this.GetFirstRtfFieldForAppTypeId(AppTypeId);
			bool flag = firstRtfFieldForAppTypeId == null;
			string result;
			if (flag)
			{
				IBaseAppointmentManager baseAppointmentManager = new BaseAppointmentManager(this.OpContext);
				BaseExtendedAppointment baseExtendedAppointment = baseAppointmentManager.LoadBaseExtendedAppointmentById<BaseExtendedAppointment>(AppointmentId);
				result = ((baseExtendedAppointment == null) ? "" : (baseExtendedAppointment.Memo ?? ""));
			}
			else
			{
				DynamicDataContext context = new DynamicDataContext
				{
					PrimaryId = StudentPersonId,
					SecondaryId = AppointmentId
				};
				IDynamicDataManager dynamicDataManager = new DynamicDataManager(this.OpContext);
				List<DynamicData> list = dynamicDataManager.LoadDataByFields(context, new List<int>
				{
					firstRtfFieldForAppTypeId.ControlId
				}, eDynamicFormType.PerAppointment);
				bool flag2 = list == null || list.Count < 1 || list[0].Value == null;
				if (flag2)
				{
					result = "";
				}
				else
				{
					result = list[0].Value.ToString();
				}
			}
			return result;
		}

		// Token: 0x040001B3 RID: 435
		private IAppointmentNotesDAO dao;
	}
}
