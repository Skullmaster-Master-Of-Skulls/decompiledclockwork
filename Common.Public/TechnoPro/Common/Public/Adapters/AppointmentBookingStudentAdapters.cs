using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Newtonsoft.Json;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent.BookingRequest;

namespace TechnoPro.Common.Public.Adapters
{
	// Token: 0x020005F6 RID: 1526
	public static class AppointmentBookingStudentAdapters
	{
		// Token: 0x060030F4 RID: 12532 RVA: 0x00043B44 File Offset: 0x00041D44
		private static XAttribute GetXAttribute(string name, int[] val_ids)
		{
			string text;
			if (val_ids != null)
			{
				text = string.Join(",", val_ids.ToList<int>().ConvertAll<string>((int g) => g.ToString()).ToArray());
			}
			else
			{
				text = "";
			}
			string value = text;
			return new XAttribute(name, value);
		}

		// Token: 0x060030F5 RID: 12533 RVA: 0x00043BA8 File Offset: 0x00041DA8
		private static bool GetBoolFromAttribute(XAttribute attr)
		{
			bool flag = string.IsNullOrEmpty((attr != null) ? attr.Value : null);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = "1yestrue".IndexOf(attr.Value, StringComparison.Ordinal) >= 0;
				bool flag3;
				result = (flag2 || (bool.TryParse(attr.Value, out flag3) && flag3));
			}
			return result;
		}

		// Token: 0x060030F6 RID: 12534 RVA: 0x00043C04 File Offset: 0x00041E04
		private static int[] GetIntArrayFromAttribute(XAttribute attr)
		{
			return string.IsNullOrEmpty((attr != null) ? attr.Value : null) ? null : AppointmentBookingStudentAdapters.GetIntListFromString(attr.Value).ToArray<int>();
		}

		// Token: 0x060030F7 RID: 12535 RVA: 0x00043C3C File Offset: 0x00041E3C
		private static int GetIntFromAttribute(XAttribute attr)
		{
			bool flag = string.IsNullOrEmpty((attr != null) ? attr.Value : null);
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				int num;
				result = ((!int.TryParse(attr.Value, out num)) ? 0 : num);
			}
			return result;
		}

		// Token: 0x060030F8 RID: 12536 RVA: 0x00043C7C File Offset: 0x00041E7C
		private static IList<string> GetStringListFromString(string s)
		{
			List<string> list = new List<string>();
			bool flag = string.IsNullOrEmpty(s);
			IList<string> result;
			if (flag)
			{
				result = list;
			}
			else
			{
				string[] array = s.Split(new char[]
				{
					','
				});
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					string text = array2[i];
					string q = text.Trim();
					bool flag2 = q.Length > 0 && list.FirstOrDefault((string g) => g.Equals(q, StringComparison.OrdinalIgnoreCase)) == null;
					if (flag2)
					{
						list.Add(q);
					}
				}
				result = list;
			}
			return result;
		}

		// Token: 0x060030F9 RID: 12537 RVA: 0x00043D2C File Offset: 0x00041F2C
		private static IList<int> GetIntListFromString(string s)
		{
			List<int> list = new List<int>();
			bool flag = string.IsNullOrEmpty(s);
			IList<int> result;
			if (flag)
			{
				result = list;
			}
			else
			{
				string[] array = s.Split(new char[]
				{
					','
				});
				foreach (string s2 in array)
				{
					int item;
					bool flag2 = int.TryParse(s2, out item) && !list.Contains(item);
					if (flag2)
					{
						list.Add(item);
					}
				}
				result = list;
			}
			return result;
		}

		// Token: 0x060030FA RID: 12538 RVA: 0x00043DB0 File Offset: 0x00041FB0
		public static IList<Channel> GetChannelsFromXmlLegacy(this string xml)
		{
			try
			{
				XDocument xdocument = XDocument.Parse(xml);
				List<AppointmentBookingStudentAdapters.Legacy_ScheduleType> list = (from g in xdocument.Descendants("scheduletype")
				let xDisplayName = g.Element("displayname")
				let xDisplaySummary = g.Element("displaysummary")
				let xAppTypeId = g.Element("apptypeid")
				let xPreBookScreenNum = g.Element("prebookscreennum")
				let xAvailabilityGroupIds = g.Element("availabilitygroupids")
				let xDuration = g.Element("duration")
				let xPreBookNotice = g.Element("prebooknotice")
				let xPostBookNotice = g.Element("postbooknotice")
				let xBookingFormScreenNum = g.Element("bookingformscreennum")
				select new
				{
					<>h__TransparentIdentifier8 = <>h__TransparentIdentifier8,
					xMaxNumInFuture = g.Element("maxnuminfuture")
				}).Select(delegate(<>h__TransparentIdentifier9)
				{
					AppointmentBookingStudentAdapters.Legacy_ScheduleType legacy_ScheduleType3 = new AppointmentBookingStudentAdapters.Legacy_ScheduleType();
					legacy_ScheduleType3.DisplayName = ((<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.xDisplayName == null) ? "" : (<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.xDisplayName.Value ?? ""));
					legacy_ScheduleType3.DisplaySummary = ((<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.xDisplaySummary == null) ? "" : (<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.xDisplaySummary.Value ?? ""));
					legacy_ScheduleType3.AppTypeId = ((<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.xAppTypeId == null) ? 0 : (<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.xAppTypeId.Value ?? "").GetIntFromString(0));
					legacy_ScheduleType3.PreBookScreenNum = ((<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.xPreBookScreenNum == null) ? 0 : (<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.xPreBookScreenNum.Value ?? "").GetIntFromString(0));
					IList<int> availabilityGroupIds;
					if (<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.xAvailabilityGroupIds != null)
					{
						availabilityGroupIds = AppointmentBookingStudentAdapters.GetIntListFromString(<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.xAvailabilityGroupIds.Value ?? "");
					}
					else
					{
						IList<int> list4 = new List<int>();
						availabilityGroupIds = list4;
					}
					legacy_ScheduleType3.AvailabilityGroupIds = availabilityGroupIds;
					legacy_ScheduleType3.BookingFormScreenNum = ((<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.xBookingFormScreenNum == null) ? 0 : (<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.xBookingFormScreenNum.Value ?? "").GetIntFromString(0));
					legacy_ScheduleType3.Duration = ((<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.xDuration == null) ? 0 : (<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.xDuration.Value ?? "").GetIntFromString(0));
					legacy_ScheduleType3.MaxNumInFuture = ((<>h__TransparentIdentifier9.xMaxNumInFuture == null) ? 0 : (<>h__TransparentIdentifier9.xMaxNumInFuture.Value ?? "").GetIntFromString(0));
					legacy_ScheduleType3.PreBookNotice = ((<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.xPreBookNotice == null) ? "" : (<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.xPreBookNotice.Value ?? ""));
					legacy_ScheduleType3.PostBookNotice = ((<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.xPostBookNotice == null) ? "" : (<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.xPostBookNotice.Value ?? ""));
					legacy_ScheduleType3.People = (from h in g.Descendants("person")
					let hDisplayName = h.Element("displayname")
					let hDisplaySummary = h.Element("displaysummary")
					let hPids = h.Element("pids")
					select new
					{
						<>h__TransparentIdentifier2 = <>h__TransparentIdentifier2,
						hChannels = h.Element("channels")
					}).Select(delegate(<>h__TransparentIdentifier3)
					{
						AppointmentBookingStudentAdapters.Legacy_Person legacy_Person2 = new AppointmentBookingStudentAdapters.Legacy_Person();
						legacy_Person2.DisplayName = ((<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.hDisplayName == null) ? "" : (<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.hDisplayName.Value ?? ""));
						legacy_Person2.DisplaySummary = ((<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.hDisplaySummary == null) ? "" : (<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.hDisplaySummary.Value ?? ""));
						IList<int> pids;
						if (<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.hPids != null)
						{
							pids = AppointmentBookingStudentAdapters.GetIntListFromString(<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.hPids.Value ?? "");
						}
						else
						{
							IList<int> list5 = new List<int>();
							pids = list5;
						}
						legacy_Person2.Pids = pids;
						IList<string> channels;
						if (<>h__TransparentIdentifier3.hChannels != null)
						{
							channels = AppointmentBookingStudentAdapters.GetStringListFromString(<>h__TransparentIdentifier3.hChannels.Value ?? "");
						}
						else
						{
							IList<string> list6 = new List<string>();
							channels = list6;
						}
						legacy_Person2.Channels = channels;
						return legacy_Person2;
					}).ToList<AppointmentBookingStudentAdapters.Legacy_Person>();
					return legacy_ScheduleType3;
				}).ToList<AppointmentBookingStudentAdapters.Legacy_ScheduleType>();
				List<Channel> list2 = new List<Channel>();
				List<string> list3 = new List<string>();
				foreach (AppointmentBookingStudentAdapters.Legacy_ScheduleType legacy_ScheduleType in list)
				{
					foreach (AppointmentBookingStudentAdapters.Legacy_Person legacy_Person in legacy_ScheduleType.People)
					{
						using (IEnumerator<string> enumerator3 = legacy_Person.Channels.GetEnumerator())
						{
							while (enumerator3.MoveNext())
							{
								string c = enumerator3.Current;
								bool flag = c.Length > 0 && list3.FirstOrDefault((string q) => q.Equals(c, StringComparison.OrdinalIgnoreCase)) == null;
								if (flag)
								{
									list3.Add(c);
								}
							}
						}
					}
				}
				using (List<string>.Enumerator enumerator4 = list3.GetEnumerator())
				{
					while (enumerator4.MoveNext())
					{
						string legacyChannel = enumerator4.Current;
						Channel channel = new Channel
						{
							Id = legacyChannel,
							Title = legacyChannel,
							IsActive = true,
							Availabilities = new List<ChannelAvailability>()
						};
						Func<string, bool> <>9__19;
						Func<AppointmentBookingStudentAdapters.Legacy_Person, bool> <>9__17;
						foreach (AppointmentBookingStudentAdapters.Legacy_ScheduleType legacy_ScheduleType2 in list)
						{
							foreach (int availabilityGroupId in legacy_ScheduleType2.AvailabilityGroupIds)
							{
								ChannelAvailability channelAvailability = new ChannelAvailability();
								channelAvailability.AvailabilityGroupId = availabilityGroupId;
								channelAvailability.PreBookScreenNum = legacy_ScheduleType2.PreBookScreenNum;
								channelAvailability.AppTypeIdToBookWith = legacy_ScheduleType2.AppTypeId;
								channelAvailability.IsActive = true;
								channelAvailability.SlotSizeInMinutes = legacy_ScheduleType2.Duration;
								IEnumerable<AppointmentBookingStudentAdapters.Legacy_Person> people = legacy_ScheduleType2.People;
								Func<AppointmentBookingStudentAdapters.Legacy_Person, bool> predicate;
								if ((predicate = <>9__17) == null)
								{
									predicate = (<>9__17 = delegate(AppointmentBookingStudentAdapters.Legacy_Person h)
									{
										IEnumerable<string> channels = h.Channels;
										Func<string, bool> predicate2;
										if ((predicate2 = <>9__19) == null)
										{
											predicate2 = (<>9__19 = ((string m) => m.Equals(legacyChannel, StringComparison.OrdinalIgnoreCase)));
										}
										return channels.FirstOrDefault(predicate2) != null;
									});
								}
								channelAvailability.PersonCollection = people.Where(predicate).ToList<AppointmentBookingStudentAdapters.Legacy_Person>().ConvertAll<ChannelPersonCollection>(delegate(AppointmentBookingStudentAdapters.Legacy_Person g)
								{
									ChannelPersonCollection channelPersonCollection = new ChannelPersonCollection();
									channelPersonCollection.Title = g.DisplayName;
									channelPersonCollection.Id = g.DisplayName;
									channelPersonCollection.IsActive = true;
									channelPersonCollection.UnderlyingPeople = g.Pids.ToList<int>().ConvertAll<ChannelUnderlyingPerson>((int q) => new ChannelUnderlyingPerson
									{
										PersonId = q
									});
									return channelPersonCollection;
								});
								channelAvailability.Title = legacy_ScheduleType2.DisplayName;
								ChannelAvailability item = channelAvailability;
								channel.Availabilities.Add(item);
							}
						}
						list2.Add(channel);
					}
				}
				return list2;
			}
			catch (Exception ex)
			{
			}
			return new List<Channel>();
		}

		// Token: 0x060030FB RID: 12539 RVA: 0x000442D4 File Offset: 0x000424D4
		public static XElement AppointmentBookingParametersToXElement(this AppointmentBookingFilterParameters p, string elementName)
		{
			return new XElement(elementName, new object[]
			{
				new XAttribute("isdisabled", (p == null).ToString()),
				new XAttribute("MaxNumberOfAppointmentsPerWeek", ((p != null) ? p.MaxNumberOfAppointmentsPerWeek.ToString() : null) ?? ""),
				AppointmentBookingStudentAdapters.GetXAttribute("MaxNumberOfAppointmentsPerWeekAppTypeIds", (p != null) ? p.MaxNumberOfAppointmentsPerWeekAppTypeIds : null),
				new XAttribute("MaxNumberOfAppointmentsPerDay", (p != null) ? p.MaxNumberOfAppointmentsPerDay : 0),
				AppointmentBookingStudentAdapters.GetXAttribute("MaxNumberOfAppointmentsPerDayAppTypeIds", (p != null) ? p.MaxNumberOfAppointmentsPerDayAppTypeIds : null),
				new XAttribute("MaxNumberOfNoShows", ((p != null) ? p.MaxNumberOfNoShows.ToString() : null) ?? ""),
				AppointmentBookingStudentAdapters.GetXAttribute("MaxNumberOfNoShowsAppTypeIds", (p != null) ? p.MaxNumberOfNoShowsAppTypeIds : null),
				new XAttribute("MaxNumberOfAppointmentsInFuture", ((p != null) ? p.MaxNumberOfAppointmentsInFuture.ToString() : null) ?? ""),
				AppointmentBookingStudentAdapters.GetXAttribute("MaxNumberOfAppointmentsInFutureAppTypeIds", (p != null) ? p.MaxNumberOfAppointmentsInFutureAppTypeIds : null),
				new XAttribute("AllowDoubleBookingStaff", ((p != null) ? p.AllowDoubleBookingStaff.ToString() : null) ?? ""),
				new XAttribute("AllowDoubleBookingStudent", ((p != null) ? p.AllowDoubleBookingStudent.ToString() : null) ?? ""),
				(p != null) ? p.CutoffTime.CutoffTimeToXmlElement() : null
			});
		}

		// Token: 0x060030FC RID: 12540 RVA: 0x000444A0 File Offset: 0x000426A0
		public static AppointmentBookingFilterParameters AppointmentBookingParametersFromElement(this XElement element)
		{
			bool flag = element == null;
			AppointmentBookingFilterParameters result;
			if (flag)
			{
				result = null;
			}
			else
			{
				XAttribute attr = element.Attribute("isdisabled");
				XAttribute attr2 = element.Attribute("MaxNumberOfAppointmentsPerWeek");
				XAttribute attr3 = element.Attribute("MaxNumberOfAppointmentsPerWeekAppTypeIds");
				XAttribute attr4 = element.Attribute("MaxNumberOfAppointmentsPerDay");
				XAttribute attr5 = element.Attribute("MaxNumberOfAppointmentsPerDayAppTypeIds");
				XAttribute attr6 = element.Attribute("MaxNumberOfNoShows");
				XAttribute attr7 = element.Attribute("MaxNumberOfNoShowsAppTypeIds");
				XAttribute attr8 = element.Attribute("MaxNumberOfAppointmentsInFuture");
				XAttribute attr9 = element.Attribute("MaxNumberOfAppointmentsInFutureAppTypeIds");
				XAttribute attr10 = element.Attribute("AllowDoubleBookingStaff");
				XAttribute attr11 = element.Attribute("AllowDoubleBookingStudent");
				XElement xelement = element.Element("CutoffTime");
				bool boolFromAttribute = AppointmentBookingStudentAdapters.GetBoolFromAttribute(attr);
				bool flag2 = boolFromAttribute;
				if (flag2)
				{
					result = null;
				}
				else
				{
					result = new AppointmentBookingFilterParameters
					{
						MaxNumberOfAppointmentsPerWeek = AppointmentBookingStudentAdapters.GetIntFromAttribute(attr2),
						MaxNumberOfAppointmentsPerWeekAppTypeIds = AppointmentBookingStudentAdapters.GetIntArrayFromAttribute(attr3),
						MaxNumberOfAppointmentsPerDay = AppointmentBookingStudentAdapters.GetIntFromAttribute(attr4),
						MaxNumberOfAppointmentsPerDayAppTypeIds = AppointmentBookingStudentAdapters.GetIntArrayFromAttribute(attr5),
						MaxNumberOfNoShows = AppointmentBookingStudentAdapters.GetIntFromAttribute(attr6),
						MaxNumberOfNoShowsAppTypeIds = AppointmentBookingStudentAdapters.GetIntArrayFromAttribute(attr7),
						MaxNumberOfAppointmentsInFuture = AppointmentBookingStudentAdapters.GetIntFromAttribute(attr8),
						MaxNumberOfAppointmentsInFutureAppTypeIds = AppointmentBookingStudentAdapters.GetIntArrayFromAttribute(attr9),
						AllowDoubleBookingStaff = AppointmentBookingStudentAdapters.GetBoolFromAttribute(attr10),
						AllowDoubleBookingStudent = AppointmentBookingStudentAdapters.GetBoolFromAttribute(attr11),
						CutoffTime = ((xelement != null) ? xelement.CutoffTimeFromXElement() : null)
					};
				}
			}
			return result;
		}

		// Token: 0x060030FD RID: 12541 RVA: 0x00044658 File Offset: 0x00042858
		public static string GetXmlFromChannels(this IList<Channel> channels)
		{
			XDeclaration declaration = new XDeclaration("1.0", "utf-8", "yes");
			object[] array = new object[1];
			array[0] = new XElement("channels", channels.Select(delegate(Channel c)
			{
				XName name = "channel";
				object[] array2 = new object[7];
				array2[0] = new XAttribute("isactive", c.IsActive.ToString());
				array2[1] = new XAttribute("id", c.Id ?? "");
				array2[2] = new XAttribute("title", c.Title ?? "");
				array2[3] = new XAttribute("description", c.Description ?? "");
				array2[4] = new XAttribute("ordernum", c.OrderNum.ToString());
				array2[5] = new XElement("availabilities", c.Availabilities.Select(delegate(ChannelAvailability d)
				{
					XName name2 = "availability";
					object[] array3 = new object[9];
					array3[0] = new XAttribute("isactive", d.IsActive.ToString());
					array3[1] = new XAttribute("title", d.Title ?? "");
					array3[2] = new XAttribute("availabilitygroupid", d.AvailabilityGroupId.ToString());
					array3[3] = new XAttribute("apptypeidtobookwith", d.AppTypeIdToBookWith.ToString());
					array3[4] = new XAttribute("prebookscreennum", d.PreBookScreenNum.ToString());
					array3[5] = new XAttribute("slotsizeinminutes", d.SlotSizeInMinutes.ToString());
					array3[6] = new XAttribute("useassignedadvisorinsteadofpersoncollection", d.UseAssignedAdvisorInsteadOfPersonCollection ? "1" : "0");
					int num = 7;
					XName name3 = "overrideassignedadvisorcids";
					object value;
					if (d.UseAssignedAdvisorInsteadOfPersonCollectionOverrideAssignedAdvisorCids != null && d.UseAssignedAdvisorInsteadOfPersonCollectionOverrideAssignedAdvisorCids.Length >= 1)
					{
						value = string.Join(",", from m in d.UseAssignedAdvisorInsteadOfPersonCollectionOverrideAssignedAdvisorCids
						select m.ToString());
					}
					else
					{
						value = "";
					}
					array3[num] = new XAttribute(name3, value);
					array3[8] = new XElement("personcollections", d.PersonCollection.Select(delegate(ChannelPersonCollection f)
					{
						XName name4 = "personcollection";
						object[] array4 = new object[5];
						array4[0] = new XAttribute("isactive", f.IsActive.ToString());
						array4[1] = new XAttribute("title", f.Title ?? "");
						int? num2;
						array4[2] = new XAttribute("colourargb", ((f.ColourArgB != null) ? num2.GetValueOrDefault().ToString() : null) ?? "");
						int num3 = 3;
						XName name5 = "channel";
						SchoolCampus campus = f.Campus;
						array4[num3] = new XAttribute(name5, (((campus != null) ? campus.CampusName : null) == null) ? "" : f.Campus.CampusName);
						array4[4] = new XElement("persons", from g in f.UnderlyingPeople
						select new XElement("person", new XAttribute("pid", g.PersonId.ToString())));
						return new XElement(name4, array4);
					}));
					return new XElement(name2, array3);
				}));
				array2[6] = c.OverrideBookingFilterParameters.AppointmentBookingParametersToXElement("OverrideBookingParameters");
				return new XElement(name, array2);
			}));
			XDocument xdocument = new XDocument(declaration, array);
			return xdocument.Declaration.ToString() + xdocument.ToString();
		}

		// Token: 0x060030FE RID: 12542 RVA: 0x000446D8 File Offset: 0x000428D8
		public static IList<Channel> GetChannelsFromXml(this string xml, out bool usingLegacy)
		{
			bool flag = string.IsNullOrEmpty(xml);
			IList<Channel> result;
			if (flag)
			{
				usingLegacy = false;
				result = new List<Channel>();
			}
			else
			{
				bool flag2 = xml.IndexOf("<scheduletypes>", StringComparison.OrdinalIgnoreCase) >= 0;
				if (flag2)
				{
					usingLegacy = true;
					result = xml.GetChannelsFromXmlLegacy();
				}
				else
				{
					usingLegacy = false;
					try
					{
						XDocument xdocument = XDocument.Parse(xml);
						List<Channel> list = (from g in xdocument.Descendants("channel")
						let xIsActive = g.Attribute("isactive")
						let xId = g.Attribute("id")
						let xTitle = g.Attribute("title")
						let xOrderNum = g.Attribute("ordernum")
						let xDescription = g.Attribute("description")
						select new
						{
							<>h__TransparentIdentifier4 = <>h__TransparentIdentifier4,
							xBookingParameters = g.Element("OverrideBookingParameters")
						}).Select(delegate(<>h__TransparentIdentifier5)
						{
							Channel channel = new Channel();
							channel.IsActive = (<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.xIsActive == null || "1yestrue".IndexOf((<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.xIsActive.Value ?? "").ToLower()) >= 0);
							channel.Title = ((<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.xTitle == null) ? "" : (<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.xTitle.Value ?? ""));
							channel.Id = ((<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.xId == null) ? "" : (<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.xId.Value ?? ""));
							channel.OrderNum = ((<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.xOrderNum == null) ? 0 : (<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.xOrderNum.Value ?? "").GetIntFromString(0));
							channel.Description = ((<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.xDescription == null) ? "" : (<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.xDescription.Value ?? ""));
							channel.Availabilities = (from h in g.Descendants("availability")
							let hIsActive = h.Attribute("isactive")
							let hTitle = h.Attribute("title")
							let hAvailabilityGroupid = h.Attribute("availabilitygroupid")
							let hAppTypeIdToBookWith = h.Attribute("apptypeidtobookwith")
							let hPreBookScreenNum = h.Attribute("prebookscreennum")
							let hSlotSizeInMinutes = h.Attribute("slotsizeinminutes")
							let hUseAssignedAdvisorInsteadOfPersonCollection = h.Attribute("useassignedadvisorinsteadofpersoncollection")
							select new
							{
								<>h__TransparentIdentifier6 = <>h__TransparentIdentifier6,
								hOverrideAssignedAdvisorCids = h.Attribute("overrideassignedadvisorcids")
							}).Select(delegate(<>h__TransparentIdentifier7)
							{
								ChannelAvailability channelAvailability = new ChannelAvailability();
								channelAvailability.IsActive = (<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.hIsActive == null || "1yestrue".IndexOf((<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.hIsActive.Value ?? "").ToLower()) >= 0);
								channelAvailability.Title = ((<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.hTitle == null) ? "" : (<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.hTitle.Value ?? ""));
								ChannelAvailability channelAvailability2 = channelAvailability;
								XAttribute hAppTypeIdToBookWith = <>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.hAppTypeIdToBookWith;
								channelAvailability2.AppTypeIdToBookWith = ((hAppTypeIdToBookWith != null) ? hAppTypeIdToBookWith.Value.GetIntFromString(0) : 0);
								ChannelAvailability channelAvailability3 = channelAvailability;
								XAttribute hAvailabilityGroupid = <>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.hAvailabilityGroupid;
								channelAvailability3.AvailabilityGroupId = ((hAvailabilityGroupid != null) ? hAvailabilityGroupid.Value.GetIntFromString(0) : 0);
								ChannelAvailability channelAvailability4 = channelAvailability;
								XAttribute hPreBookScreenNum = <>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.hPreBookScreenNum;
								channelAvailability4.PreBookScreenNum = ((hPreBookScreenNum != null) ? hPreBookScreenNum.Value.GetIntFromString(0) : 0);
								ChannelAvailability channelAvailability5 = channelAvailability;
								XAttribute hSlotSizeInMinutes = <>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.hSlotSizeInMinutes;
								channelAvailability5.SlotSizeInMinutes = ((hSlotSizeInMinutes != null) ? hSlotSizeInMinutes.Value.GetIntFromString(0) : 0);
								channelAvailability.UseAssignedAdvisorInsteadOfPersonCollection = AppointmentBookingStudentAdapters.GetBoolFromAttribute(<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.hUseAssignedAdvisorInsteadOfPersonCollection);
								ChannelAvailability channelAvailability6 = channelAvailability;
								XAttribute hOverrideAssignedAdvisorCids = <>h__TransparentIdentifier7.hOverrideAssignedAdvisorCids;
								channelAvailability6.UseAssignedAdvisorInsteadOfPersonCollectionOverrideAssignedAdvisorCids = (from n in (((hOverrideAssignedAdvisorCids != null) ? hOverrideAssignedAdvisorCids.Value : null) ?? "").Trim().Split(new char[]
								{
									','
								}).Select(delegate(string gg)
								{
									int result2;
									bool flag3 = !int.TryParse(gg, out result2);
									if (flag3)
									{
										result2 = 0;
									}
									return result2;
								})
								where n > 0
								select n).Distinct<int>().ToArray<int>();
								channelAvailability.PersonCollection = (from f in h.Descendants("personcollection")
								let fIsActive = f.Attribute("isactive")
								let fTitle = f.Attribute("title")
								let fColourArgB = f.Attribute("colourargb")
								select new
								{
									<>h__TransparentIdentifier2 = <>h__TransparentIdentifier2,
									fChannel = f.Attribute("channel")
								}).Select(delegate(<>h__TransparentIdentifier3)
								{
									ChannelPersonCollection channelPersonCollection = new ChannelPersonCollection();
									channelPersonCollection.IsActive = (<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.fIsActive == null || "1yestrue".IndexOf((<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.fIsActive.Value ?? "").ToLower()) >= 0);
									channelPersonCollection.Title = ((<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.fTitle == null) ? "" : (<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.fTitle.Value ?? ""));
									channelPersonCollection.ColourArgB = ((<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.fColourArgB == null) ? null : (string.IsNullOrEmpty(<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.fColourArgB.Value) ? null : new int?(<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.fColourArgB.Value.GetIntFromString(0))));
									SchoolCampus campus;
									if (<>h__TransparentIdentifier3.fChannel != null && !string.IsNullOrEmpty(<>h__TransparentIdentifier3.fChannel.Value))
									{
										(campus = new SchoolCampus()).CampusName = <>h__TransparentIdentifier3.fChannel.Value;
									}
									else
									{
										campus = null;
									}
									channelPersonCollection.Campus = campus;
									channelPersonCollection.UnderlyingPeople = (from d in <>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.f.Descendants("person")
									select new
									{
										d = d,
										dPid = d.Attribute("pid")
									}).Select(delegate(<>h__TransparentIdentifier0)
									{
										ChannelUnderlyingPerson channelUnderlyingPerson = new ChannelUnderlyingPerson();
										XAttribute dPid = <>h__TransparentIdentifier0.dPid;
										channelUnderlyingPerson.PersonId = ((dPid != null) ? dPid.Value.GetIntFromString(0) : 0);
										return channelUnderlyingPerson;
									}).ToList<ChannelUnderlyingPerson>();
									return channelPersonCollection;
								}).ToList<ChannelPersonCollection>();
								return channelAvailability;
							}).ToList<ChannelAvailability>();
							channel.OverrideBookingFilterParameters = <>h__TransparentIdentifier5.xBookingParameters.AppointmentBookingParametersFromElement();
							return channel;
						}).ToList<Channel>();
						list.Sort((Channel g1, Channel g2) => g1.OrderNum.CompareTo(g2.OrderNum));
						return list;
					}
					catch (Exception ex)
					{
					}
					result = new List<Channel>();
				}
			}
			return result;
		}

		// Token: 0x060030FF RID: 12543 RVA: 0x0004489C File Offset: 0x00042A9C
		public static PreCalendarQuestionnaireOptions GetPreCalendarQuestionnaireOptionsFromString(this string s)
		{
			bool flag = string.IsNullOrEmpty(s);
			PreCalendarQuestionnaireOptions result;
			if (flag)
			{
				result = null;
			}
			else
			{
				try
				{
					result = JsonConvert.DeserializeObject<PreCalendarQuestionnaireOptions>(s);
				}
				catch
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x06003100 RID: 12544 RVA: 0x000448DC File Offset: 0x00042ADC
		public static string PreCalendarQuestionnaireOptionsToString(PreCalendarQuestionnaireOptions options)
		{
			bool flag = options == null;
			string result;
			if (flag)
			{
				result = string.Empty;
			}
			else
			{
				result = JsonConvert.SerializeObject(options);
			}
			return result;
		}

		// Token: 0x02000677 RID: 1655
		internal class Legacy_ScheduleType
		{
			// Token: 0x17001407 RID: 5127
			// (get) Token: 0x060033A7 RID: 13223 RVA: 0x0004CAE2 File Offset: 0x0004ACE2
			// (set) Token: 0x060033A8 RID: 13224 RVA: 0x0004CAEA File Offset: 0x0004ACEA
			public string DisplayName { get; set; }

			// Token: 0x17001408 RID: 5128
			// (get) Token: 0x060033A9 RID: 13225 RVA: 0x0004CAF3 File Offset: 0x0004ACF3
			// (set) Token: 0x060033AA RID: 13226 RVA: 0x0004CAFB File Offset: 0x0004ACFB
			public string DisplaySummary { get; set; }

			// Token: 0x17001409 RID: 5129
			// (get) Token: 0x060033AB RID: 13227 RVA: 0x0004CB04 File Offset: 0x0004AD04
			// (set) Token: 0x060033AC RID: 13228 RVA: 0x0004CB0C File Offset: 0x0004AD0C
			public int AppTypeId { get; set; }

			// Token: 0x1700140A RID: 5130
			// (get) Token: 0x060033AD RID: 13229 RVA: 0x0004CB15 File Offset: 0x0004AD15
			// (set) Token: 0x060033AE RID: 13230 RVA: 0x0004CB1D File Offset: 0x0004AD1D
			public int PreBookScreenNum { get; set; }

			// Token: 0x1700140B RID: 5131
			// (get) Token: 0x060033AF RID: 13231 RVA: 0x0004CB26 File Offset: 0x0004AD26
			// (set) Token: 0x060033B0 RID: 13232 RVA: 0x0004CB2E File Offset: 0x0004AD2E
			public IList<int> AvailabilityGroupIds { get; set; }

			// Token: 0x1700140C RID: 5132
			// (get) Token: 0x060033B1 RID: 13233 RVA: 0x0004CB37 File Offset: 0x0004AD37
			// (set) Token: 0x060033B2 RID: 13234 RVA: 0x0004CB3F File Offset: 0x0004AD3F
			public IList<AppointmentBookingStudentAdapters.Legacy_Person> People { get; set; }

			// Token: 0x1700140D RID: 5133
			// (get) Token: 0x060033B3 RID: 13235 RVA: 0x0004CB48 File Offset: 0x0004AD48
			// (set) Token: 0x060033B4 RID: 13236 RVA: 0x0004CB50 File Offset: 0x0004AD50
			public int Duration { get; set; }

			// Token: 0x1700140E RID: 5134
			// (get) Token: 0x060033B5 RID: 13237 RVA: 0x0004CB59 File Offset: 0x0004AD59
			// (set) Token: 0x060033B6 RID: 13238 RVA: 0x0004CB61 File Offset: 0x0004AD61
			public string PreBookNotice { get; set; }

			// Token: 0x1700140F RID: 5135
			// (get) Token: 0x060033B7 RID: 13239 RVA: 0x0004CB6A File Offset: 0x0004AD6A
			// (set) Token: 0x060033B8 RID: 13240 RVA: 0x0004CB72 File Offset: 0x0004AD72
			public string PostBookNotice { get; set; }

			// Token: 0x17001410 RID: 5136
			// (get) Token: 0x060033B9 RID: 13241 RVA: 0x0004CB7B File Offset: 0x0004AD7B
			// (set) Token: 0x060033BA RID: 13242 RVA: 0x0004CB83 File Offset: 0x0004AD83
			public int BookingFormScreenNum { get; set; }

			// Token: 0x17001411 RID: 5137
			// (get) Token: 0x060033BB RID: 13243 RVA: 0x0004CB8C File Offset: 0x0004AD8C
			// (set) Token: 0x060033BC RID: 13244 RVA: 0x0004CB94 File Offset: 0x0004AD94
			public int MaxNumInFuture { get; set; }
		}

		// Token: 0x02000678 RID: 1656
		internal class Legacy_Person
		{
			// Token: 0x17001412 RID: 5138
			// (get) Token: 0x060033BE RID: 13246 RVA: 0x0004CB9D File Offset: 0x0004AD9D
			// (set) Token: 0x060033BF RID: 13247 RVA: 0x0004CBA5 File Offset: 0x0004ADA5
			public string DisplayName { get; set; }

			// Token: 0x17001413 RID: 5139
			// (get) Token: 0x060033C0 RID: 13248 RVA: 0x0004CBAE File Offset: 0x0004ADAE
			// (set) Token: 0x060033C1 RID: 13249 RVA: 0x0004CBB6 File Offset: 0x0004ADB6
			public string DisplaySummary { get; set; }

			// Token: 0x17001414 RID: 5140
			// (get) Token: 0x060033C2 RID: 13250 RVA: 0x0004CBBF File Offset: 0x0004ADBF
			// (set) Token: 0x060033C3 RID: 13251 RVA: 0x0004CBC7 File Offset: 0x0004ADC7
			public IList<int> Pids { get; set; }

			// Token: 0x17001415 RID: 5141
			// (get) Token: 0x060033C4 RID: 13252 RVA: 0x0004CBD0 File Offset: 0x0004ADD0
			// (set) Token: 0x060033C5 RID: 13253 RVA: 0x0004CBD8 File Offset: 0x0004ADD8
			public IList<string> Channels { get; set; }
		}
	}
}
