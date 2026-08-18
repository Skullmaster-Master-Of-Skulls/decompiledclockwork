using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Web.Caching;
using System.Web.SessionState;
using System.Web.UI;
using System.Xml;
using ClockWorkWebAPI;
using ClockWorkWebAPI.AuthenticationAuthorization;
using ClockWorkWebAPI.TestBooking;
using ClockWorkWebAPIWeb.AppBooking;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Configuration;
using TechnoPro.Common.Core.Settings;
using TechnoPro.Common.Public.Entities.Settings;

namespace ClockWorkWebAPIWeb
{
	// Token: 0x02000004 RID: 4
	public class Caching
	{
		// Token: 0x06000022 RID: 34 RVA: 0x00002838 File Offset: 0x00000A38
		public static DataView GetStaffNames(Page Page, HttpSessionState session, string allowedGroupIds)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			IEncryption encryption = clockWork.Encryption;
			Cache cache = Page.Cache;
			string name = "staffnames";
			object obj = session[name];
			bool dontCache = Caching.DontCache;
			if (dontCache)
			{
				obj = null;
			}
			bool flag = obj == null;
			DataView dataView;
			if (flag)
			{
				string query = "SELECT p.personid,'' AS name,p.firstname,p.lastname FROM people p LEFT JOIN peoplegroups pg ON pg.personid=p.personid WHERE (@ids='' OR pg.groupid IN (SELECT orderid AS groupid FROM splitorderids(@ids,',')))";
				DataTable dataTable = clockWork.ExecuteQuery(query, new DbParameter[]
				{
					clockWork.GetParameter("@ids", DbType.String, allowedGroupIds)
				});
				dataTable = encryption.EncryptOrDecryptNameDataTableBatch(false, dataTable, new string[]
				{
					"firstname",
					"lastname"
				});
				foreach (object obj2 in dataTable.Rows)
				{
					DataRow dataRow = (DataRow)obj2;
					string str = (dataRow[2] == DBNull.Value) ? "" : ((string)dataRow[2]);
					string str2 = (dataRow[3] == DBNull.Value) ? "" : ((string)dataRow[3]);
					dataRow[1] = str + " " + str2;
				}
				dataTable.Columns.RemoveAt(3);
				dataTable.Columns.RemoveAt(2);
				DataRow dataRow2 = dataTable.NewRow();
				dataRow2[0] = 0;
				dataRow2[1] = "";
				dataTable.Rows.InsertAt(dataRow2, 0);
				dataView = new DataView(dataTable);
				dataView.Sort = "name";
				session.Add(name, dataView);
			}
			else
			{
				dataView = (DataView)obj;
			}
			return dataView;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002A10 File Offset: 0x00000C10
		public static DataView GetAppointmentsList(Page Page, HttpSessionState session, string allowedAppTypeIds)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			Cache cache = Page.Cache;
			string name = "apptypes";
			object obj = session[name];
			bool dontCache = Caching.DontCache;
			if (dontCache)
			{
				obj = null;
			}
			bool flag = obj == null;
			DataView dataView;
			if (flag)
			{
				string query = "SELECT apptypeid,description FROM appointmenttypes WHERE @ids='' OR apptypeid IN (SELECT orderid AS apptypeid FROM splitorderids(@ids,',')) ORDER BY description";
				DataTable dataTable = clockWork.ExecuteQuery(query, new DbParameter[]
				{
					clockWork.GetParameter("@ids", DbType.String, allowedAppTypeIds)
				});
				DataRow dataRow = dataTable.NewRow();
				dataRow[0] = 0;
				dataRow[1] = "";
				dataTable.Rows.InsertAt(dataRow, 0);
				dataView = new DataView(dataTable);
				dataView.Sort = "description";
				session.Add(name, dataView);
			}
			else
			{
				dataView = (DataView)obj;
			}
			return dataView;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002AEC File Offset: 0x00000CEC
		[Obsolete("")]
		public static List<AuthenticationMethod> GetLookupAuthenticationMethods(Page Page, object LOGIN_AuthenticationMethods)
		{
			return Caching.GetLookupAuthenticationMethods(Page, Setting.LOGIN_AuthenticationMethods);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002B0C File Offset: 0x00000D0C
		public static List<AuthenticationMethod> GetLookupAuthenticationMethods(Page Page, Setting LOGIN_AuthenticationMethods)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			Cache cache = Page.Cache;
			string key = "LoginAuthenticationMethods";
			object obj = cache[key];
			bool dontCache = Caching.DontCache;
			if (dontCache)
			{
				obj = null;
			}
			bool flag = obj == null;
			List<AuthenticationMethod> list;
			if (flag)
			{
				IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
				string settingValue = webSettingsClientManager.GetSettingValue<string>(LOGIN_AuthenticationMethods);
				list = Utility.ParseXmlAuthenticationMethods(settingValue);
				cache.Insert(key, list);
			}
			else
			{
				list = (List<AuthenticationMethod>)obj;
			}
			return list;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002B88 File Offset: 0x00000D88
		public static List<Channel> GetAppointmentBookingChannels(Page Page, Setting APPOINTMENTBOOKING_Channels)
		{
			Cache cache = Page.Cache;
			string key = "AppointmentBookingChannels";
			object obj = cache[key];
			bool dontCache = Caching.DontCache;
			if (dontCache)
			{
			}
			List<Channel> list = new List<Channel>();
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			string settingValue = webSettingsClientManager.GetSettingValue<string>(APPOINTMENTBOOKING_Channels);
			bool flag = settingValue.Length > 0;
			if (flag)
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(settingValue);
				XmlNode firstChild = xmlDocument.FirstChild;
				foreach (object obj2 in firstChild.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj2;
					string title = "";
					string id = "";
					string text = "";
					string s = "";
					string colour = "";
					string text2 = "";
					int orderNum = 0;
					foreach (object obj3 in xmlNode.ChildNodes)
					{
						XmlNode xmlNode2 = (XmlNode)obj3;
						bool flag2 = xmlNode2.Name.Equals("title");
						if (flag2)
						{
							title = xmlNode2.InnerText;
						}
						else
						{
							bool flag3 = xmlNode2.Name.Equals("id");
							if (flag3)
							{
								id = xmlNode2.InnerText;
							}
							else
							{
								bool flag4 = xmlNode2.Name.Equals("description");
								if (flag4)
								{
									string innerText = xmlNode2.InnerText;
								}
								else
								{
									bool flag5 = xmlNode2.Name.Equals("colour");
									if (flag5)
									{
										colour = xmlNode2.InnerText;
									}
									else
									{
										bool flag6 = xmlNode2.Name.Equals("apptypeid");
										if (flag6)
										{
											text = xmlNode2.InnerText;
										}
										else
										{
											bool flag7 = xmlNode2.Name.Equals("duration");
											if (flag7)
											{
												string innerText2 = xmlNode2.InnerText;
											}
											else
											{
												bool flag8 = xmlNode2.Name.Equals("bookingformscreennum");
												if (flag8)
												{
													s = xmlNode2.InnerText;
												}
												else
												{
													bool flag9 = xmlNode2.Name.Equals("isactive");
													if (flag9)
													{
														text2 = xmlNode2.InnerText;
													}
													else
													{
														bool flag10 = xmlNode2.Name.Equals("ordernum");
														if (flag10)
														{
															bool flag11 = !int.TryParse(xmlNode2.InnerText, out orderNum);
															if (flag11)
															{
																orderNum = 0;
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
					bool flag12 = text2.Length < 0 || "1yestrue".IndexOf(text2) >= 0;
					if (flag12)
					{
						bool flag13 = text.Length > 0;
						int appTypeId;
						if (flag13)
						{
							try
							{
								appTypeId = int.Parse(text);
							}
							catch
							{
								appTypeId = 0;
							}
						}
						else
						{
							appTypeId = 0;
						}
						int screenNum;
						bool flag14 = !int.TryParse(s, out screenNum);
						if (flag14)
						{
							screenNum = 0;
						}
						Channel item = new Channel(id, title, colour, appTypeId, screenNum, orderNum);
						list.Add(item);
					}
				}
			}
			list.Sort((Channel c1, Channel c2) => c1.OrderNum.CompareTo(c2.OrderNum));
			cache.Insert(key, list);
			return list;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002F28 File Offset: 0x00001128
		public static List<ClockWorkWebAPI.AuthenticationAuthorization.Group> GetAuthenticationGroups(Page Page, List<AuthenticationMethod> lookupAuthenticationMethods, object LOGIN_Groups)
		{
			return Caching.GetAuthenticationGroups(Page, lookupAuthenticationMethods, Setting.LOGIN_Groups);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002F48 File Offset: 0x00001148
		public static List<ClockWorkWebAPI.AuthenticationAuthorization.Group> GetAuthenticationGroups(Page Page, List<AuthenticationMethod> lookupAuthenticationMethods, Setting LOGIN_Groups)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			Cache cache = Page.Cache;
			string key = "AuthenticationGroups";
			object obj = cache[key];
			bool dontCache = Caching.DontCache;
			if (dontCache)
			{
				obj = null;
			}
			bool flag = obj == null;
			List<ClockWorkWebAPI.AuthenticationAuthorization.Group> list;
			if (flag)
			{
				IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
				string settingValue = webSettingsClientManager.GetSettingValue<string>(LOGIN_Groups);
				list = Utility.ParseXmlGroups(settingValue, lookupAuthenticationMethods);
				cache.Insert(key, list);
			}
			else
			{
				list = (List<ClockWorkWebAPI.AuthenticationAuthorization.Group>)obj;
			}
			return list;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002FC4 File Offset: 0x000011C4
		public static List<ClockWorkWebAPI.TestBooking.SpecialAccommodation> LoadSpecialAccommodations(Page page, Setting TESTBOOKING_SpecialAccommodations)
		{
			Cache cache = page.Cache;
			string key = "TestBookingSpecialAccommodations";
			List<ClockWorkWebAPI.TestBooking.SpecialAccommodation> list = new List<ClockWorkWebAPI.TestBooking.SpecialAccommodation>();
			object obj = cache[key];
			bool dontCache = Caching.DontCache;
			if (dontCache)
			{
				obj = null;
			}
			bool flag = obj == null;
			if (flag)
			{
				IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
				string settingValue = webSettingsClientManager.GetSettingValue<string>(TESTBOOKING_SpecialAccommodations);
				bool flag2 = TESTBOOKING_SpecialAccommodations > (Setting)230000;
				Setting setting;
				if (flag2)
				{
					setting = Setting.EXAMBOOKING_SpecialAccommodationsToIgnore;
				}
				else
				{
					setting = Setting.TESTBOOKING_SpecialAccommodationsToIgnore;
				}
				string settingValue2 = SettingManager.CurrentInstance.GetSettingValue<string>(setting);
				list = ClockWorkWebAPI.TestBooking.SpecialAccommodation.LoadSpecialAccommodations(settingValue, settingValue2);
				cache.Insert(key, list);
			}
			else
			{
				list = (List<ClockWorkWebAPI.TestBooking.SpecialAccommodation>)obj;
			}
			return list;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00003068 File Offset: 0x00001268
		public static List<Asset> LoadTestBookingAssets(Page page, Setting TESTBOOKING_Assets)
		{
			Cache cache = page.Cache;
			string key = "TestBookingAssets";
			List<Asset> list = new List<Asset>();
			object obj = cache[key];
			bool dontCache = Caching.DontCache;
			if (dontCache)
			{
				obj = null;
			}
			bool flag = obj == null;
			if (flag)
			{
				IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
				string settingValue = webSettingsClientManager.GetSettingValue<string>(TESTBOOKING_Assets);
				list = Asset.LoadAssets(settingValue);
				cache.Insert(key, list);
			}
			else
			{
				list = (List<Asset>)obj;
			}
			return list;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000030E0 File Offset: 0x000012E0
		public static bool DontCache
		{
			get
			{
				string appSettingsByNameUsingProtection = ClockWorkConfigurationManager.GetAppSettingsByNameUsingProtection("dontcache");
				return Core.ParseBooleanAttribute(appSettingsByNameUsingProtection, false);
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00003104 File Offset: 0x00001304
		public static List<ClockWorkWebAPI.TestBooking.Rule> LoadTestBookingRules(Page page, Setting TESTBOOKING_Rules)
		{
			Cache cache = page.Cache;
			string key = "TestBookingRules";
			List<ClockWorkWebAPI.TestBooking.Rule> list = new List<ClockWorkWebAPI.TestBooking.Rule>();
			object obj = cache[key];
			bool dontCache = Caching.DontCache;
			if (dontCache)
			{
				obj = null;
			}
			bool flag = obj == null;
			if (flag)
			{
				IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
				string settingValue = webSettingsClientManager.GetSettingValue<string>(TESTBOOKING_Rules);
				list = ClockWorkWebAPI.TestBooking.Rule.FromXml(settingValue);
				cache.Insert(key, list);
			}
			else
			{
				list = (List<ClockWorkWebAPI.TestBooking.Rule>)obj;
			}
			return list;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000317C File Offset: 0x0000137C
		public static List<Room> LoadTestBookingRooms(List<Asset> availableAssets, Page page, Setting TESTBOOKING_Rooms)
		{
			Cache cache = page.Cache;
			string key = "TestBookingRooms";
			List<Room> list = new List<Room>();
			object obj = cache[key];
			bool dontCache = Caching.DontCache;
			if (dontCache)
			{
				obj = null;
			}
			bool flag = obj == null;
			if (flag)
			{
				IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
				string settingValue = webSettingsClientManager.GetSettingValue<string>(TESTBOOKING_Rooms);
				list = Room.LoadRooms(settingValue, availableAssets);
				cache.Insert(key, list);
			}
			else
			{
				list = (List<Room>)obj;
			}
			return list;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000031F4 File Offset: 0x000013F4
		public static DataTable GetAvailabilityGroups(Page Page, db conn)
		{
			string key = "AvailabilityGroups";
			bool flag = Page.Cache[key] == null;
			if (flag)
			{
				conn.Da.SelectCommand.CommandText = "SELECT availabilitygroupid,availabilitytitle,availabilitydescription,colour FROM availabilitygroup";
				DataTable dataTable = new DataTable();
				conn.Da.Fill(dataTable);
				Page.Cache.Insert(key, dataTable, null, DateTime.Now.AddMinutes(20.0), TimeSpan.Zero);
			}
			return (DataTable)Page.Cache[key];
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00003288 File Offset: 0x00001488
		public static DataTable GetAppTypes(string appTypeIds, Page Page, db conn)
		{
			return Caching.GetAppTypes(appTypeIds, Page);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000032A4 File Offset: 0x000014A4
		public static DataTable GetAppTypes(string appTypeIds, Page Page)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			string key = "AppTypes_" + appTypeIds.Replace(',', '.').Replace(' ', '_');
			bool flag = Page.Cache[key] == null;
			if (flag)
			{
				DbParameter[] array = new DbParameter[]
				{
					clockWork.Parameter
				};
				array[0].ParameterName = "@ids";
				array[0].DbType = DbType.String;
				array[0].Value = appTypeIds;
				DataTable value = clockWork.ExecuteQuery(QueryStorage.QS_Select_AppointmentTypes, array);
				Page.Cache.Insert(key, value, null, DateTime.Now.AddMinutes(20.0), TimeSpan.Zero);
			}
			return (DataTable)Page.Cache[key];
		}
	}
}
