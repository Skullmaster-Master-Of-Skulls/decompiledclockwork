using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Text;
using System.Web;
using ClockWorkLogger;
using Microsoft.Exchange.WebServices.Data;
using TechnoPro.Common.Public.Entities.AppointmentSync;

namespace TechnoPro.Common.DAO.Exchange.Impl.Adapters
{
	// Token: 0x02000007 RID: 7
	public static class AppointmentAdapter
	{
		// Token: 0x06000041 RID: 65 RVA: 0x0000641C File Offset: 0x0000461C
		public static ExternalAppointmentId GetExternalAppointmentId(this Appointment app, bool loadProperties = false)
		{
			bool flag = !loadProperties;
			if (flag)
			{
				app.LoadPropertiesForAppointment(new PropertyDefinitionBase[]
				{
					ItemSchema.Id,
					AppointmentSchema.ICalUid,
					AppointmentAdapter.PROP_DEF_PidLidGlobalObjectId
				});
			}
			string uniqueId = string.Empty;
			object obj;
			bool flag2 = app.TryGetProperty(AppointmentAdapter.PROP_DEF_PidLidGlobalObjectId, out obj);
			if (flag2)
			{
				uniqueId = Convert.ToBase64String((byte[])obj);
			}
			return new ExternalAppointmentId
			{
				UniqueId = app.Id.UniqueId,
				UniqueId2 = uniqueId,
				GlobalAppId = AppointmentAdapter.GetObjectIdStringFromUid(app.ICalUid)
			};
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000064B1 File Offset: 0x000046B1
		public static void LoadPropertiesForAppointment(this Appointment app, PropertySet propertySet)
		{
			app.Load(propertySet);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000064BC File Offset: 0x000046BC
		public static void LoadPropertiesForAppointment(this Appointment app, params PropertyDefinitionBase[] properties)
		{
			app.Load(new PropertySet(properties));
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000064CC File Offset: 0x000046CC
		public static string GetGlobalAppointmentId(this Appointment appointment, bool loadProperties = false)
		{
			bool flag = !loadProperties;
			if (flag)
			{
				appointment.Load(new PropertySet(new PropertyDefinitionBase[]
				{
					AppointmentSchema.ICalUid
				}));
			}
			return AppointmentAdapter.GetObjectIdStringFromUid(appointment.ICalUid);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000650C File Offset: 0x0000470C
		public static string GetUniqueAppointmentId(this Appointment appointment, bool loadProperties = false)
		{
			string result;
			try
			{
				bool flag = !loadProperties;
				if (flag)
				{
					appointment.Load(new PropertySet(new PropertyDefinitionBase[]
					{
						AppointmentAdapter.PROP_DEF_PidLidGlobalObjectId
					}));
				}
				object obj;
				result = (appointment.TryGetProperty(AppointmentAdapter.PROP_DEF_PidLidGlobalObjectId, out obj) ? Convert.ToBase64String((byte[])obj) : string.Empty);
			}
			catch (Exception ex)
			{
				CWLogger.Logger.ErrorException(string.Format("AppointmentAdapter::GetUniqueAppointmentId: {0}", ex.ToString()), ex);
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00006598 File Offset: 0x00004798
		public static string GetObjectIdStringFromUid(string id)
		{
			bool flag = string.IsNullOrEmpty(id);
			string result;
			if (flag)
			{
				result = string.Empty;
			}
			else
			{
				try
				{
					byte[] array = new byte[id.Length / 2];
					for (int i = 0; i < id.Length / 2; i++)
					{
						byte b;
						bool flag2 = byte.TryParse(id.Substring(i * 2, 2), NumberStyles.AllowHexSpecifier, null, out b);
						if (!flag2)
						{
							return string.Empty;
						}
						array[i] = b;
					}
					result = Convert.ToBase64String(array);
				}
				catch (Exception ex)
				{
					CWLogger.Logger.ErrorException(string.Format("AppointmentAdapter::GetObjectIdStringFromUid: {0}", ex.ToString()), ex);
					result = string.Empty;
				}
			}
			return result;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00006658 File Offset: 0x00004858
		public static int GetClockWorkAppointmentId(this Appointment app)
		{
			return 0;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000063E6 File Offset: 0x000045E6
		public static void SetClockWorkAppointmentId(this Appointment app, int cwAppId, ExtendedPropertyDefinition exProDef)
		{
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000666C File Offset: 0x0000486C
		public static string ToDisplayString(this Appointment app)
		{
			bool flag = app == null;
			string result;
			if (flag)
			{
				result = string.Empty;
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine("Subject: " + app.Subject);
				stringBuilder.AppendLine("Start: " + app.Start.ToString("F"));
				stringBuilder.AppendLine("End: " + app.End.ToString("F"));
				stringBuilder.AppendLine("Body: " + app.Body.GetMemoPlainText());
				stringBuilder.AppendLine("Organizer.Name: " + app.Organizer.Name);
				stringBuilder.AppendLine("Organizer.Address: " + app.Organizer.Address);
				bool flag2 = app.RequiredAttendees != null;
				if (flag2)
				{
					stringBuilder.AppendLine(string.Format("nRequiredAttendees: {0}", app.RequiredAttendees.Count));
					int num = 1;
					foreach (Attendee attendee in app.RequiredAttendees)
					{
						stringBuilder.AppendLine(string.Format(" ----- Begin Attendee{0} --------", num++));
						stringBuilder.AppendLine(attendee.ToDisplayString());
						stringBuilder.AppendLine(string.Format(" ----- End Attendee{0} --------", num));
					}
				}
				bool flag3 = app.OptionalAttendees != null;
				if (flag3)
				{
					stringBuilder.AppendLine(string.Format("nOptionalAttendees: {0}", app.OptionalAttendees.Count));
					int num2 = 1;
					foreach (Attendee attendee2 in app.OptionalAttendees)
					{
						stringBuilder.AppendLine(string.Format(" ----- Begin Attendee{0} --------", num2++));
						stringBuilder.AppendLine(attendee2.ToDisplayString());
						stringBuilder.AppendLine(string.Format(" ----- End Attendee{0} --------", num2));
					}
				}
				stringBuilder.AppendLine(string.Format("IsCancelled: {0}", app.IsCancelled));
				stringBuilder.AppendLine(string.Format("IsMeeting: {0}", app.IsMeeting));
				stringBuilder.AppendLine(string.Format("AppointmentType: {0}", app.AppointmentType));
				stringBuilder.AppendLine(string.Format("IsRecurring: {0}", app.IsRecurring));
				stringBuilder.AppendLine("LastModifiedName: " + app.LastModifiedName);
				stringBuilder.AppendLine("LastModifiedTime: " + app.LastModifiedTime.ToString("F"));
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00006974 File Offset: 0x00004B74
		public static string ToDisplayString(this Attendee attendee)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("Name: " + attendee.Name);
			stringBuilder.AppendLine("Address: " + attendee.Address);
			bool flag = attendee.ResponseType != null;
			if (flag)
			{
				stringBuilder.AppendLine(string.Format("ResponseType: {0}", attendee.ResponseType.Value));
			}
			bool flag2 = attendee.LastResponseTime != null;
			if (flag2)
			{
				stringBuilder.AppendLine("LastResponseTime: " + attendee.LastResponseTime.Value.ToString("F"));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00006A3C File Offset: 0x00004C3C
		private static int GetClockWorkAppIdFromLocation(string location)
		{
			bool flag = !string.IsNullOrEmpty(location) && location.Contains("cw=");
			int result;
			if (flag)
			{
				int num = location.IndexOf("cw=");
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = num + 3; i < location.Length; i++)
				{
					char c = location[i];
					bool flag2 = char.IsDigit(c);
					if (!flag2)
					{
						break;
					}
					stringBuilder.Append(c);
				}
				int num2;
				result = ((stringBuilder.Length > 0 && int.TryParse(stringBuilder.ToString(), out num2)) ? num2 : 0);
			}
			else
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00006AE4 File Offset: 0x00004CE4
		private static string GetClockWorkAppIdUrl(string url, int cwAppId)
		{
			string text = string.IsNullOrEmpty(url) ? "http://clockworks.ca" : url;
			Uri uri = new Uri(text);
			NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(uri.Query);
			nameValueCollection.Remove("cwappid");
			nameValueCollection.Add("cwappid", cwAppId.ToString());
			string value = text.Split(new char[]
			{
				'?'
			})[0];
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(value);
			string[] allKeys = nameValueCollection.AllKeys;
			bool flag = allKeys.Length != 0;
			if (flag)
			{
				stringBuilder.AppendFormat("?{0}={1}", allKeys[0], nameValueCollection[allKeys[0]]);
			}
			for (int i = 1; i < allKeys.Length; i++)
			{
				stringBuilder.AppendFormat("&{0}={1}", allKeys[i], nameValueCollection[allKeys[i]]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00006BD0 File Offset: 0x00004DD0
		private static int GetClockWorkAppIdFromUrl(string url)
		{
			int result;
			try
			{
				bool flag = string.IsNullOrEmpty(url);
				if (flag)
				{
					result = 0;
				}
				else
				{
					Uri uri = new Uri(url);
					NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(uri.Query);
					string text = nameValueCollection["cwappid"];
					int num;
					result = ((!string.IsNullOrEmpty(text) && int.TryParse(text, out num) && num > 0) ? num : 0);
				}
			}
			catch (Exception)
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x04000014 RID: 20
		private static PropertyDefinitionBase PROP_DEF_PidLidGlobalObjectId = new ExtendedPropertyDefinition(DefaultExtendedPropertySet.Meeting, 3, MapiPropertyType.Binary);
	}
}
