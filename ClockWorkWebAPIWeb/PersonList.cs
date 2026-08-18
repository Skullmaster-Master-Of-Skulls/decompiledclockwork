using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web;
using System.Web.Caching;
using System.Xml;
using ClockWorkWebAPI;
using ClockWorkWebAPIWeb.AppBooking;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public.Entities.Settings;

namespace ClockWorkWebAPIWeb
{
	// Token: 0x02000017 RID: 23
	public class PersonList
	{
		// Token: 0x06000134 RID: 308 RVA: 0x0000FA6C File Offset: 0x0000DC6C
		public static void Add(PersonInfo pi)
		{
			List<PersonInfo> list = PersonList.AllData();
			list.Add(pi);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000FA88 File Offset: 0x0000DC88
		public static List<PersonInfo> AllData()
		{
			List<PersonInfo> list = HttpContext.Current.Session["Scheduler.GettingStarted_Persons"] as List<PersonInfo>;
			bool flag = list == null;
			if (flag)
			{
				list = new List<PersonInfo>();
				HttpContext.Current.Session["Scheduler.GettingStarted_Persons"] = list;
			}
			return list;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00002616 File Offset: 0x00000816
		public static void InsertPerson(string ID, string name, string email)
		{
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00002616 File Offset: 0x00000816
		public static void DeleteAppointment(string ID)
		{
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00002616 File Offset: 0x00000816
		public static void UpdateAppointment(string ID, string name, string email)
		{
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000FADC File Offset: 0x0000DCDC
		public static PersonInfo FindById(string ID, List<PersonInfo> sessPersons)
		{
			foreach (PersonInfo personInfo in sessPersons)
			{
				bool flag = personInfo.ID == ID;
				if (flag)
				{
					return personInfo;
				}
			}
			return null;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000FB44 File Offset: 0x0000DD44
		public static void GetUniquePidsAndAvailabilityGroupIds(db conn, Cache cache, out List<int> pids, out List<int> agids, out List<ClockWorkWebAPI.AppType> appTypes, List<string> ignoreScheduleTypeTexts)
		{
			PersonList.GetUniquePidsAndAvailabilityGroupIds(cache, out pids, out agids, out appTypes, ignoreScheduleTypeTexts);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000FB54 File Offset: 0x0000DD54
		public static void GetUniquePidsAndAvailabilityGroupIds(Cache cache, out List<int> pids, out List<int> agids, out List<ClockWorkWebAPI.AppType> appTypes, List<string> ignoreScheduleTypeTexts)
		{
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			string settingValue = webSettingsClientManager.GetSettingValue<string>(Setting.APPOINTMENTBOOKING_availabilitygroupidsdurations);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(settingValue);
			XmlNode xmlNode = xmlDocument.ChildNodes[0];
			Color[] array = new Color[]
			{
				Color.LightBlue,
				Color.LightCoral,
				Color.LightCyan,
				Color.LightGoldenrodYellow,
				Color.LightGray,
				Color.LightGreen,
				Color.LightPink,
				Color.LightSalmon,
				Color.LightSeaGreen,
				Color.LightSkyBlue,
				Color.LightSlateGray,
				Color.LightSteelBlue,
				Color.LightYellow
			};
			int num = -1;
			appTypes = new List<ClockWorkWebAPI.AppType>();
			pids = new List<int>();
			agids = new List<int>();
			foreach (object obj in xmlNode.ChildNodes)
			{
				XmlNode xmlNode2 = (XmlNode)obj;
				num++;
				bool flag = num >= array.Length;
				if (flag)
				{
					num = 0;
				}
				string innerText = xmlNode2["displayname"].InnerText;
				bool flag2 = !ignoreScheduleTypeTexts.Contains(innerText);
				if (flag2)
				{
					XmlElement xmlElement = xmlNode2["colourint"];
					bool flag3 = xmlElement != null && xmlElement.InnerText != null && xmlElement.InnerText.Length > 0;
					int colourInt;
					if (flag3)
					{
						colourInt = int.Parse(xmlElement.InnerText);
					}
					else
					{
						colourInt = array[num].ToArgb();
					}
					ClockWorkWebAPI.AppType item = new ClockWorkWebAPI.AppType(0, 0, innerText, colourInt);
					appTypes.Add(item);
					List<int> list = PersonList.ParseIntStringList(xmlNode2["availabilitygroupids"].InnerText.ToString());
					foreach (int item2 in list)
					{
						bool flag4 = !agids.Contains(item2);
						if (flag4)
						{
							agids.Add(item2);
						}
					}
					foreach (object obj2 in xmlNode2["people"].ChildNodes)
					{
						XmlNode xmlNode3 = (XmlNode)obj2;
						bool flag5 = xmlNode3["hidden"] == null || xmlNode3["hidden"].InnerText.Trim().ToLower().CompareTo("true") != 0;
						if (flag5)
						{
							List<int> list2 = PersonList.ParseIntStringList(xmlNode3["pids"].InnerText.ToString());
							foreach (int item3 in list2)
							{
								bool flag6 = !pids.Contains(item3);
								if (flag6)
								{
									pids.Add(item3);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000FF0C File Offset: 0x0000E10C
		public static List<int> ParseIntStringList(string s)
		{
			string[] array = s.Split(new char[]
			{
				','
			});
			List<int> list = new List<int>();
			foreach (string s2 in array)
			{
				int item = int.Parse(s2);
				list.Add(item);
			}
			return list;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000FF64 File Offset: 0x0000E164
		public static void GetDisplayPeopleAndAppointments(db conn, Cache cache, AvailabilitySchedule asched, ref List<PersonInfo> displayPeople, ref List<AppointmentInfo> apps, DateTime sdate, DateTime edate, List<ClockWorkWebAPI.AppType> appTypes, List<string> ignoreScheduleTypeTexts, string tutorName, Channel currentChannel, string defaultChannelId)
		{
			PersonList.GetDisplayPeopleAndAppointments(cache, asched, ref displayPeople, ref apps, sdate, edate, appTypes, ignoreScheduleTypeTexts, tutorName, currentChannel, defaultChannelId);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000FF8C File Offset: 0x0000E18C
		public static void GetDisplayPeopleAndAppointments(Cache cache, AvailabilitySchedule asched, ref List<PersonInfo> displayPeople, ref List<AppointmentInfo> apps, DateTime sdate, DateTime edate, List<ClockWorkWebAPI.AppType> appTypes, List<string> ignoreScheduleTypeTexts, string tutorName, Channel currentChannel, string defaultChannelId)
		{
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			string settingValue = webSettingsClientManager.GetSettingValue<string>(Setting.APPOINTMENTBOOKING_availabilitygroupidsdurations);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(settingValue);
			XmlNode xmlNode = xmlDocument.ChildNodes[0];
			int num = 1;
			int num2 = 0;
			foreach (object obj in xmlNode.ChildNodes)
			{
				XmlNode xmlNode2 = (XmlNode)obj;
				string text = (currentChannel == null) ? xmlNode2["displayname"].InnerText.ToString() : currentChannel.Title;
				string name = (xmlNode2["name"] == null) ? ("col" + num2++.ToString()) : xmlNode2["name"].InnerText.ToString();
				bool flag = !ignoreScheduleTypeTexts.Contains(text);
				if (flag)
				{
					List<int> list = PersonList.ParseIntStringList(xmlNode2["availabilitygroupids"].InnerText.ToString());
					ClockWorkWebAPI.AppType appType = ClockWorkWebAPI.AppType.FindAppType(appTypes, text);
					foreach (object obj2 in xmlNode2["people"].ChildNodes)
					{
						XmlNode xmlNode3 = (XmlNode)obj2;
						string text2 = xmlNode3["displayname"].InnerText.ToString();
						string text3 = xmlNode3["pids"].InnerText.ToString();
						bool flag2 = currentChannel != null;
						bool flag7;
						if (flag2)
						{
							string text4 = (xmlNode3["channels"] == null) ? defaultChannelId : xmlNode3["channels"].InnerText.ToString();
							bool flag3 = text4.Length > 0;
							if (flag3)
							{
								string[] array = text4.Split(new char[]
								{
									','
								});
								string id = currentChannel.Id;
								bool flag4 = false;
								foreach (string text5 in array)
								{
									bool flag5 = text5.Trim().Equals(id);
									if (flag5)
									{
										flag4 = true;
										break;
									}
								}
								bool flag6 = !flag4;
								flag7 = !flag6;
							}
							else
							{
								flag7 = true;
							}
							bool flag8 = appType == null;
							if (flag8)
							{
								appType = new ClockWorkWebAPI.AppType(currentChannel.AppTypeId, 0, currentChannel.Title, currentChannel.Colour);
							}
							else
							{
								appType.AppTypeId = currentChannel.AppTypeId;
								appType.ColourInt = currentChannel.Colour;
							}
						}
						else
						{
							flag7 = true;
						}
						bool flag9 = flag7 && (tutorName.Length < 1 || tutorName.CompareTo(text3) == 0);
						if (flag9)
						{
							List<int> pids = PersonList.ParseIntStringList(text3);
							string summary = xmlNode3["displaysummary"].InnerText.ToString();
							PersonInfo personInfo = null;
							foreach (PersonInfo personInfo2 in displayPeople)
							{
								bool flag10 = personInfo2.Name.CompareTo(text2) == 0;
								if (flag10)
								{
									personInfo = personInfo2;
									break;
								}
							}
							bool flag11 = personInfo == null;
							if (flag11)
							{
								personInfo = new PersonInfo(text3, text2, summary);
								displayPeople.Add(personInfo);
							}
							int[] array3 = new int[list.Count];
							for (int j = 0; j < list.Count; j++)
							{
								array3[j] = list[j];
							}
							int minuteBlocks = int.Parse(xmlNode2["duration"].InnerText.ToString());
							List<AvailabilityScheduleRange> list2 = asched.FindAvailableSpots(pids, array3, sdate, edate, minuteBlocks);
							List<int> list3 = new List<int>();
							foreach (AvailabilityScheduleRange availabilityScheduleRange in list2)
							{
								AppointmentInfo appointmentInfo = new AppointmentInfo(num++.ToString(), personInfo.ID, text, availabilityScheduleRange.Start, availabilityScheduleRange.End, availabilityScheduleRange.Rid);
								int num3 = availabilityScheduleRange.Booked ? availabilityScheduleRange.AppId : 0;
								bool flag12 = num3 < 1 || !list3.Contains(num3);
								if (flag12)
								{
									list3.Add(num3);
									bool flag13 = appType != null;
									if (flag13)
									{
										appointmentInfo.AppTypeId = appType.AppTypeId;
									}
									appointmentInfo.Booked = availabilityScheduleRange.Booked;
									bool booked = appointmentInfo.Booked;
									if (booked)
									{
										appointmentInfo.ActualAppointmentId = availabilityScheduleRange.AppId;
										bool isBookedByLoggedInUser = availabilityScheduleRange.IsBookedByLoggedInUser;
										if (isBookedByLoggedInUser)
										{
											appointmentInfo.IsBookedByLoggedInUser = true;
											appointmentInfo.Colour = Color.Yellow.ToArgb();
											appointmentInfo.Subject = "You are booked";
										}
										else
										{
											appointmentInfo.Colour = Color.Gray.ToArgb();
											appointmentInfo.Subject = "Not available";
										}
									}
									else
									{
										bool flag14 = currentChannel != null;
										if (flag14)
										{
											appointmentInfo.Colour = currentChannel.Colour;
										}
										else
										{
											bool flag15 = appType != null;
											if (flag15)
											{
												appointmentInfo.Colour = appType.ColourInt;
											}
											else
											{
												appointmentInfo.Colour = Color.LightBlue.ToArgb();
											}
										}
									}
									appointmentInfo.Name = name;
									apps.Add(appointmentInfo);
								}
							}
						}
						else
						{
							PersonInfo.RemovePersonFromList(ref displayPeople, text2);
						}
					}
				}
			}
		}

		// Token: 0x0400007F RID: 127
		private const string PersonsKey = "Scheduler.GettingStarted_Persons";
	}
}
