using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities.AppointmentsCalendar;
using TechnoPro.Common.TextFormat.Adapters;
using Telerik.Web.UI;

namespace TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.AppointmentsCalendar
{
	// Token: 0x02000026 RID: 38
	public static class InterfaceAppointmentHelper
	{
		// Token: 0x060000E9 RID: 233 RVA: 0x000098B0 File Offset: 0x00007AB0
		public static AppTypeDTO GetAppType(this Telerik.Web.UI.Appointment app)
		{
			bool flag = app == null;
			AppTypeDTO result;
			if (flag)
			{
				result = null;
			}
			else
			{
				int intFromAttribute = InterfaceAppointmentHelper.GetIntFromAttribute(app.Attributes, "AppTypeIdStr");
				AppTypeDTO appTypeDTO2;
				if (intFromAttribute >= 1)
				{
					AppTypeDTO appTypeDTO = new AppTypeDTO();
					appTypeDTO.AppTypeId = intFromAttribute;
					appTypeDTO2 = appTypeDTO;
					appTypeDTO.Description = (app.Attributes["AppTypeTitle"] ?? "");
				}
				else
				{
					appTypeDTO2 = null;
				}
				result = appTypeDTO2;
			}
			return result;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00009914 File Offset: 0x00007B14
		public static void SetAppType(this Telerik.Web.UI.Appointment app, AppTypeDTO appType)
		{
			app.Attributes.AddAttribute("AppTypeIdStr", ((appType != null) ? appType.AppTypeId.ToString() : null) ?? "");
			app.Attributes.AddAttribute("AppTypeTitle", ((appType != null) ? appType.Description : null) ?? "");
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00009978 File Offset: 0x00007B78
		public static string GetSubTitle(this Telerik.Web.UI.Appointment app)
		{
			return (app != null) ? app.Subject : null;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00009996 File Offset: 0x00007B96
		public static void SetSubTitle(this Telerik.Web.UI.Appointment app, string subTitle)
		{
			app.Subject = (subTitle ?? "");
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000099AC File Offset: 0x00007BAC
		public static bool GetIsCancelled(this Telerik.Web.UI.Appointment app)
		{
			return app != null && InterfaceAppointmentHelper.GetBoolFromAttribute(app.Attributes, "IsCancelled");
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000099D4 File Offset: 0x00007BD4
		public static void SetIsCancelled(this Telerik.Web.UI.Appointment app, bool isCancelled)
		{
			app.Attributes.AddAttribute("IsCancelled", isCancelled.ToString());
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000099F0 File Offset: 0x00007BF0
		public static bool GetIsPrivate(this Telerik.Web.UI.Appointment app)
		{
			return app != null && InterfaceAppointmentHelper.GetBoolFromAttribute(app.Attributes, "IsPrivate");
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00009A18 File Offset: 0x00007C18
		public static void SetIsPrivate(this Telerik.Web.UI.Appointment app, bool isPrivate)
		{
			app.Attributes.AddAttribute("IsPrivate", isPrivate.ToString());
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00009A34 File Offset: 0x00007C34
		public static string GetMemoPlainText(this Telerik.Web.UI.Appointment app)
		{
			return (app != null) ? app.Attributes["MemoPlainText"] : null;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00009A5C File Offset: 0x00007C5C
		public static void SetMemoPlainTextByRtf(this Telerik.Web.UI.Appointment app, string memoRtf)
		{
			app.SetMemoPlainTextByPlainText(string.IsNullOrEmpty(memoRtf) ? "" : (memoRtf.ConvertRtfToPlainText() ?? ""));
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00009A84 File Offset: 0x00007C84
		public static void SetMemoPlainTextByPlainText(this Telerik.Web.UI.Appointment app, string memoPlainText)
		{
			app.Attributes.AddAttribute("MemoPlainText", memoPlainText ?? "");
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00009AA4 File Offset: 0x00007CA4
		public static IList<AttendeeDTO> GetAttendees(this Telerik.Web.UI.Appointment app)
		{
			return (app == null) ? null : app.Attributes["AttendeesSerialized"].GetAttendees();
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00009AD4 File Offset: 0x00007CD4
		public static IList<AttendeeDTO> GetAttendees(this string attendeesSerialized)
		{
			return InterfaceAppointmentHelper.DeSerializeAttendees(attendeesSerialized ?? "").ToList<AttendeeDTO>();
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00009AFA File Offset: 0x00007CFA
		public static void SetAttendees(this Telerik.Web.UI.Appointment app, IList<AttendeeDTO> attendees)
		{
			app.Attributes.AddAttribute("AttendeesSerialized", InterfaceAppointmentHelper.SerializeAttendees(attendees ?? new List<AttendeeDTO>()));
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00009B20 File Offset: 0x00007D20
		public static RoomAndLocation GetRoomAndLocation(this Telerik.Web.UI.Appointment app)
		{
			return (app == null) ? null : app.Attributes["RoomStr"].GetRoomAndLocation();
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00009B50 File Offset: 0x00007D50
		public static RoomAndLocation GetRoomAndLocation(this string roomAndLocationSerialized)
		{
			return (roomAndLocationSerialized ?? "").DeSerializeRoomAndLocation();
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00009B71 File Offset: 0x00007D71
		public static void SetRoomAndLocation(this Telerik.Web.UI.Appointment app, AppointmentRoomDTO room, string location)
		{
			app.Attributes.AddAttribute("RoomStr", InterfaceAppointmentHelper.SerializeRoomAndLocation(room, location));
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00009B8C File Offset: 0x00007D8C
		public static string GetAppCssClass(this Telerik.Web.UI.Appointment app)
		{
			bool flag = app == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				result = app.Attributes["AppClass"];
			}
			return result;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00009BBE File Offset: 0x00007DBE
		public static void SetAppCssClass(this Telerik.Web.UI.Appointment app, AppointmentDTO cwApp)
		{
			app.Attributes.AddAttribute("AppClass", (cwApp != null && cwApp.IsCancelled) ? "CancelledAppointment" : "");
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00009BEC File Offset: 0x00007DEC
		public static string GetAttendeeSerialized(this AttendeeDTO attendee)
		{
			bool flag = attendee == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				result = InterfaceAppointmentHelper.SerializeAttendee(attendee);
			}
			return result;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00009C14 File Offset: 0x00007E14
		public static IList<eAppointmentPermissionRestriction> GetRestrictions(this Telerik.Web.UI.Appointment app)
		{
			bool flag = app == null;
			IList<eAppointmentPermissionRestriction> result;
			if (flag)
			{
				result = new List<eAppointmentPermissionRestriction>();
			}
			else
			{
				result = (from h in (app.Attributes["Restrictions"] ?? "").Split(new char[]
				{
					','
				}).Select(delegate(string g)
				{
					int num;
					bool flag2 = g.Length < 1 || !int.TryParse(g, out num);
					int result2;
					if (flag2)
					{
						result2 = 0;
					}
					else
					{
						result2 = num;
					}
					return result2;
				})
				where h > 0 && Enum.IsDefined(typeof(eAppointmentPermissionRestriction), h)
				select h into m
				select (eAppointmentPermissionRestriction)m).ToList<eAppointmentPermissionRestriction>();
			}
			return result;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00009CD4 File Offset: 0x00007ED4
		public static void SetRestrictions(this Telerik.Web.UI.Appointment app, IList<eAppointmentPermissionRestriction> restrictions)
		{
			AttributeCollection attributes = app.Attributes;
			string key = "Restrictions";
			string val;
			if (restrictions != null)
			{
				val = string.Join(",", restrictions.Select(delegate(eAppointmentPermissionRestriction g)
				{
					int num = (int)g;
					return num.ToString();
				}).ToArray<string>());
			}
			else
			{
				val = "";
			}
			attributes.AddAttribute(key, val);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00009D34 File Offset: 0x00007F34
		public static bool HasRestriction(this IList<eAppointmentPermissionRestriction> restrictions, params eAppointmentPermissionRestrictionResult[] hasOneOrMoreOfThese)
		{
			return restrictions != null && restrictions.Any((eAppointmentPermissionRestriction g) => hasOneOrMoreOfThese.Contains(g.GetAttribute<AppointmentPermissionRestrictionAttribute>().Result));
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00009D6C File Offset: 0x00007F6C
		public static int GetWhoBookedPid(this Telerik.Web.UI.Appointment app)
		{
			bool flag = app == null;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				result = InterfaceAppointmentHelper.GetIntFromAttribute(app.Attributes, "WhoBookedPid");
			}
			return result;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00009D9A File Offset: 0x00007F9A
		public static void SetWhobookedPid(this Telerik.Web.UI.Appointment app, int whoBookedPid)
		{
			app.Attributes.AddAttribute("WhoBookedPid", whoBookedPid.ToString());
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00009DB8 File Offset: 0x00007FB8
		private static void AddAttribute(this AttributeCollection attributes, string key, string val)
		{
			string text = attributes[key];
			bool flag = text != null;
			if (flag)
			{
				attributes.Remove(key);
			}
			attributes.Add(key, val);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00009DE8 File Offset: 0x00007FE8
		private static string SerializeAttendee(AttendeeDTO att)
		{
			string result;
			if (att != null)
			{
				string format = "{0}.{1}.{2}.{3}.{4}";
				object[] array = new object[5];
				array[0] = att.AttendeeId;
				int num = 1;
				PersonBaseDTO person = att.Person;
				array[num] = ((person != null) ? person.PersonId : 0).ToString();
				array[2] = att.IsNoShow.ToString();
				array[3] = att.MiscCode.ToString();
				int num2 = 4;
				PersonBaseDTO person2 = att.Person;
				array[num2] = (((person2 != null) ? person2.GetName() : null) ?? "");
				result = string.Format(format, array);
			}
			else
			{
				result = "";
			}
			return result;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00009E80 File Offset: 0x00008080
		private static IList<AttendeeDTO> DeSerializeAttendees(string attendeesSerialized)
		{
			return (attendeesSerialized.Length > 0) ? attendeesSerialized.Split(new char[]
			{
				'`'
			}).Select(new Func<string, AttendeeDTO>(InterfaceAppointmentHelper.DeSerializeAttendee)).ToList<AttendeeDTO>() : new List<AttendeeDTO>();
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00009ECC File Offset: 0x000080CC
		private static string SerializeAttendees(IList<AttendeeDTO> attendees)
		{
			return (attendees == null) ? "" : string.Join("`", attendees.Select(new Func<AttendeeDTO, string>(InterfaceAppointmentHelper.SerializeAttendee)).ToArray<string>());
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00009F0C File Offset: 0x0000810C
		private static AttendeeDTO DeSerializeAttendee(string s)
		{
			bool flag = string.IsNullOrEmpty(s);
			AttendeeDTO result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string[] array = s.Split(new char[]
				{
					'.'
				});
				int attendeeId;
				int.TryParse(array[0], out attendeeId);
				int num;
				int.TryParse(array[1], out num);
				int miscCode;
				int.TryParse(array[3], out miscCode);
				bool isNoShow;
				bool.TryParse(array[2], out isNoShow);
				AttendeeDTO attendeeDTO = new AttendeeDTO();
				attendeeDTO.AttendeeId = attendeeId;
				PersonBaseDTO person;
				if (num >= 1)
				{
					PersonBaseDTO personBaseDTO = new PersonBaseDTO();
					personBaseDTO.PersonId = num;
					person = personBaseDTO;
					personBaseDTO.FirstName = array[4];
				}
				else
				{
					person = null;
				}
				attendeeDTO.Person = person;
				attendeeDTO.IsNoShow = isNoShow;
				attendeeDTO.MiscCode = miscCode;
				result = attendeeDTO;
			}
			return result;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00009FB8 File Offset: 0x000081B8
		private static bool GetBoolFromAttribute(AttributeCollection attributes, string name)
		{
			string text = attributes[name] ?? "";
			bool flag2;
			bool flag = text.Length < 1 || !bool.TryParse(text, out flag2);
			return !flag && flag2;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00009FFC File Offset: 0x000081FC
		private static int GetIntFromAttribute(AttributeCollection attributes, string name)
		{
			string text = attributes[name] ?? "";
			int num;
			bool flag = text.Length < 1 || !int.TryParse(text, out num);
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				result = num;
			}
			return result;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0000A040 File Offset: 0x00008240
		private static string SerializeRoomAndLocation(AppointmentRoomDTO room, string location)
		{
			return string.Concat(new string[]
			{
				((room != null) ? room.RoomId : 0).ToString(),
				"`",
				(((room != null) ? room.RoomTitle : null) ?? "").Replace("`", "~~~"),
				"`",
				(location ?? "").Replace("`", "~~~")
			});
		}

		// Token: 0x0600010A RID: 266 RVA: 0x0000A0C8 File Offset: 0x000082C8
		private static RoomAndLocation DeSerializeRoomAndLocation(this string s)
		{
			bool flag = string.IsNullOrEmpty(s);
			RoomAndLocation result;
			if (flag)
			{
				result = new RoomAndLocation();
			}
			else
			{
				string[] array = s.Split(new char[]
				{
					'`'
				});
				int num;
				int.TryParse(array[0], out num);
				RoomAndLocation roomAndLocation = new RoomAndLocation();
				AppointmentRoomDTO room;
				if (num >= 1)
				{
					AppointmentRoomDTO appointmentRoomDTO = new AppointmentRoomDTO();
					appointmentRoomDTO.RoomId = num;
					room = appointmentRoomDTO;
					appointmentRoomDTO.RoomTitle = array[1].Replace("~~~", "`");
				}
				else
				{
					room = null;
				}
				roomAndLocation.Room = room;
				roomAndLocation.Location = ((array.Length > 2) ? array[2].Replace("~~~", "`") : "");
				result = roomAndLocation;
			}
			return result;
		}
	}
}
