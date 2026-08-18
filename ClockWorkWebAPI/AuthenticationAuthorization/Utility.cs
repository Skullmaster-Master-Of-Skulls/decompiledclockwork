using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Security;
using System.Web.SessionState;
using System.Xml;
using ClockWorkLogger;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.ClientManager.Core.Authentication;
using TechnoPro.Common.ClientManager.Core.LookupCourses;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.Authentication;
using TechnoPro.Common.ClientManager.ICore.LookupCourses;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.Web.Entity.Adapters;

namespace ClockWorkWebAPI.AuthenticationAuthorization
{
	// Token: 0x0200007D RID: 125
	public class Utility
	{
		// Token: 0x0600063E RID: 1598 RVA: 0x00028CE0 File Offset: 0x00026EE0
		public static StringDictionary ParseArgs(string argsString)
		{
			StringDictionary args = new StringDictionary();
			argsString.Split(new char[]
			{
				';'
			}).Select(delegate(string h)
			{
				string text = h.Trim();
				int num = text.IndexOf('=');
				args.Add((num > 0) ? text.Substring(0, num).Trim() : text, (num > 0) ? text.Substring(num + 1).Trim() : "");
				return text;
			});
			return args;
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x00028D2C File Offset: 0x00026F2C
		public static GroupMembership LookupGroupMembership(string groupType)
		{
			object obj;
			try
			{
				obj = Enum.Parse(typeof(GroupMembership), groupType, true);
			}
			catch
			{
				obj = null;
			}
			bool flag = obj != null;
			GroupMembership result;
			if (flag)
			{
				result = (GroupMembership)obj;
			}
			else
			{
				result = GroupMembership.unknown;
			}
			return result;
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x00028D80 File Offset: 0x00026F80
		[Obsolete("Use TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Auth.Utility.ParseXmlAuthenticationMethods instead")]
		public static List<AuthenticationMethod> ParseXmlAuthenticationMethods(string xml)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(xml);
			List<AuthenticationMethod> list = new List<AuthenticationMethod>();
			XmlNode firstChild = xmlDocument.FirstChild;
			foreach (object obj in firstChild.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				string name = "";
				string type = "";
				StringDictionary stringDictionary = new StringDictionary();
				foreach (object obj2 in xmlNode.Attributes)
				{
					XmlAttribute xmlAttribute = (XmlAttribute)obj2;
					bool flag = xmlAttribute.Name.CompareTo("enabled") == 0;
					if (flag)
					{
						bool flag2 = Utility.ParseBool(xmlAttribute.Value);
					}
					else
					{
						bool flag3 = xmlAttribute.Name.CompareTo("name") == 0;
						if (flag3)
						{
							name = xmlAttribute.Value;
						}
						else
						{
							bool flag4 = xmlAttribute.Name.CompareTo("type") == 0;
							if (flag4)
							{
								type = xmlAttribute.Value;
							}
							else
							{
								stringDictionary.Add(xmlAttribute.Name, xmlAttribute.Value);
							}
						}
					}
				}
				AuthenticationMethod item = new AuthenticationMethod(type, name, stringDictionary);
				list.Add(item);
			}
			return list;
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x00028F28 File Offset: 0x00027128
		public static List<Group> ParseXmlGroups(string xml, List<AuthenticationMethod> authenticationLookupMethods)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(xml);
			List<Group> list = new List<Group>();
			XmlNode firstChild = xmlDocument.FirstChild;
			foreach (object obj in firstChild)
			{
				XmlNode xmlNode = (XmlNode)obj;
				Group group = new Group(Utility.LookupGroupMembership(xmlNode.Attributes["type"].Value));
				XmlNode firstChild2 = xmlNode.FirstChild;
				foreach (object obj2 in firstChild2.ChildNodes)
				{
					XmlNode xmlNode2 = (XmlNode)obj2;
					string value = xmlNode2.Attributes["enabled"].Value;
					bool flag = Utility.ParseBool(value);
					if (flag)
					{
						string value2 = xmlNode2.Attributes["name"].Value;
						AuthenticationMethod authenticationMethod = Utility.LookupAuthenticationMethod(value2, authenticationLookupMethods);
						bool flag2 = authenticationMethod != null;
						if (flag2)
						{
							AuthenticationLookupMethod authenticationLookupMethod = new AuthenticationLookupMethod(authenticationMethod);
							XmlNode firstChild3 = xmlNode2.FirstChild;
							foreach (object obj3 in firstChild3.ChildNodes)
							{
								XmlNode xmlNode3 = (XmlNode)obj3;
								string lookupMethodType = "";
								StringDictionary stringDictionary = new StringDictionary();
								foreach (object obj4 in xmlNode3.Attributes)
								{
									XmlAttribute xmlAttribute = (XmlAttribute)obj4;
									bool flag3 = xmlAttribute.Name.CompareTo("type") == 0;
									if (flag3)
									{
										lookupMethodType = xmlAttribute.Value;
									}
									else
									{
										stringDictionary.Add(xmlAttribute.Name, xmlAttribute.Value);
									}
								}
								LookupMethod lookupMethod = new LookupMethod(lookupMethodType, stringDictionary);
								authenticationLookupMethod.AddLookupMethod(lookupMethod);
							}
							group.AddAuthenticationLookupMethod(authenticationLookupMethod);
						}
						else
						{
							AuthenticationMethod authMethod = new AuthenticationMethod("unknown", "unknown", "");
							AuthenticationLookupMethod method = new AuthenticationLookupMethod(authMethod);
							group.AddAuthenticationLookupMethod(method);
						}
					}
				}
				list.Add(group);
			}
			return list;
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x00029204 File Offset: 0x00027404
		public static AuthenticationMethod LookupAuthenticationMethod(string name, List<AuthenticationMethod> authenticationLookupMethods)
		{
			foreach (AuthenticationMethod authenticationMethod in authenticationLookupMethods)
			{
				bool flag = authenticationMethod.Is(name);
				if (flag)
				{
					return authenticationMethod;
				}
			}
			return null;
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x00029264 File Offset: 0x00027464
		public static bool ParseBool(string boolstr)
		{
			return "1yestrue".IndexOf(boolstr.ToLower().Trim()) >= 0;
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x00029294 File Offset: 0x00027494
		public static GroupMembership ParseGroupMemberships(string groupMembershipNamesCommaSeparated)
		{
			GroupMembership groupMembership = GroupMembership.unknown;
			string[] array = groupMembershipNamesCommaSeparated.Split(new char[]
			{
				','
			});
			foreach (string value in array)
			{
				try
				{
					GroupMembership groupMembership2 = (GroupMembership)Enum.Parse(typeof(GroupMembership), value, true);
					groupMembership |= groupMembership2;
				}
				catch
				{
				}
			}
			return groupMembership;
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x0002930C File Offset: 0x0002750C
		public static int LookupUser(db conn, ref UserInfo userInfo, GroupMembership groupMembership, List<Group> groupAuthentications, Cache Cache, HttpResponse Response, HttpRequest Request, HttpSessionState Session, iCustomLogin customLogin)
		{
			return Utility.LookupUser(ref userInfo, groupMembership, groupAuthentications, Cache, Response, Request, Session, customLogin);
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x00029330 File Offset: 0x00027530
		public static int LookupUser(ref UserInfo userInfo, GroupMembership groupMembership, List<Group> groupAuthentications, Cache Cache, HttpResponse Response, HttpRequest Request, HttpSessionState Session, iCustomLogin customLogin)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			IEncryption encryption = clockWork.Encryption;
			bool flag = userInfo != null && userInfo.AuthenticationMethod != null && userInfo.AuthenticationGroupMembership > GroupMembership.unknown;
			if (flag)
			{
				if (groupMembership <= GroupMembership.instructors)
				{
					if (groupMembership - GroupMembership.student > 1)
					{
						if (groupMembership == GroupMembership.faculty || groupMembership == GroupMembership.instructors)
						{
							bool flag2 = userInfo.ClockworkIid > 0;
							if (flag2)
							{
								return userInfo.ClockworkIid;
							}
						}
					}
					else
					{
						bool flag3 = userInfo.ClockworkPid > 0;
						if (flag3)
						{
							return userInfo.ClockworkPid;
						}
					}
				}
				else if (groupMembership != GroupMembership.notetakers)
				{
					if (groupMembership != GroupMembership.externalstudent)
					{
						if (groupMembership == GroupMembership.altcontact)
						{
							bool flag4 = userInfo.ClockworkAltContactId > 0;
							if (flag4)
							{
								return userInfo.ClockworkAltContactId;
							}
						}
					}
					else
					{
						bool flag5 = userInfo.ExternalClockWorkPid > 0;
						if (flag5)
						{
							return userInfo.ExternalClockWorkPid;
						}
					}
				}
				else
				{
					bool flag6 = userInfo.ClockworkNid > 0;
					if (flag6)
					{
						return userInfo.ClockworkNid;
					}
				}
				foreach (Group group in groupAuthentications)
				{
					bool flag7 = group.GroupType == groupMembership;
					if (flag7)
					{
						foreach (AuthenticationLookupMethod authenticationLookupMethod in group.AuthenticationLookupMethods)
						{
							bool flag8 = authenticationLookupMethod.AuthenticationMethod.Is(userInfo.AuthenticationMethod.Name);
							if (flag8)
							{
								foreach (LookupMethod lookupMethod in authenticationLookupMethod.LookupMethods)
								{
									string lookupMethodType = lookupMethod.LookupMethodType;
									string text = lookupMethodType;
									uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
									if (num <= 576041450U)
									{
										if (num <= 491182750U)
										{
											if (num != 236505954U)
											{
												if (num == 491182750U)
												{
													if (text == "instructorusername")
													{
														ILookupInstructorClientManager lookupInstructorClientManager = new LookupInstructorClientManager();
														LookupInstructorDTO lookupInstructorDTO = lookupInstructorClientManager.LoadInstructorByUsername(userInfo.Username);
														bool flag9 = lookupInstructorDTO != null && lookupInstructorDTO.InstructorId > 0;
														if (flag9)
														{
															userInfo.ClockworkIid = lookupInstructorDTO.InstructorId;
															return lookupInstructorDTO.InstructorId;
														}
													}
												}
											}
											else if (text == "student_no")
											{
												DbParameter[] array = new DbParameter[]
												{
													clockWork.Parameter
												};
												array[0].ParameterName = "@snume";
												array[0].DbType = DbType.Binary;
												array[0].Value = encryption.Encrypt(userInfo.Username);
												DataTable dataTable = clockWork.ExecuteQuery(QueryStorage.QS_Select_UserByStudent_no, array);
												bool flag10 = dataTable.Rows.Count > 0;
												if (flag10)
												{
													DataRow dataRow = dataTable.Rows[0];
													userInfo.ClockworkPid = (int)dataRow[0];
													return userInfo.ClockworkPid;
												}
											}
										}
										else if (num != 497933244U)
										{
											if (num != 542584942U)
											{
												if (num == 576041450U)
												{
													if (!(text == "userinfo"))
													{
													}
												}
											}
											else if (!(text == "custom"))
											{
											}
										}
										else if (text == "notetakeremail")
										{
											string text2 = userInfo.Username + lookupMethod.GetArgSafe("postfix");
											string text3 = text2.ToLower();
											string plainText = text2.ToUpper();
											bool flag11 = string.IsNullOrEmpty(text3);
											DataTable dataTable2;
											if (flag11)
											{
												dataTable2 = new DataTable();
											}
											else
											{
												DbParameter[] array = new DbParameter[]
												{
													clockWork.GetParameter("@emailbytes", DbType.Binary, encryption.Encrypt(text3)),
													clockWork.GetParameter("@emailbytes2", DbType.Binary, encryption.Encrypt(plainText))
												};
												dataTable2 = clockWork.ExecuteQuery(QueryStorage.QS_Select_ServiceProviderByEmail, array);
											}
											bool flag12 = dataTable2.Rows.Count > 0;
											if (flag12)
											{
												int num2 = (int)dataTable2.Rows[0][0];
												userInfo.ClockworkNid = num2;
												return num2;
											}
										}
									}
									else
									{
										if (num <= 3576918758U)
										{
											if (num != 1382815865U)
											{
												if (num != 3540014724U)
												{
													if (num != 3576918758U)
													{
														continue;
													}
													if (!(text == "instructoremail"))
													{
														continue;
													}
												}
												else
												{
													if (!(text == "notetakerusername"))
													{
														continue;
													}
													string text4 = userInfo.Username.ToLower().Trim();
													DbParameter[] array = new DbParameter[]
													{
														clockWork.GetParameter("@sne", DbType.Binary, encryption.Encrypt(text4.ToUpper())),
														clockWork.GetParameter("@sne2", DbType.Binary, encryption.Encrypt(text4.ToLower()))
													};
													bool flag13 = text4.Length > 0;
													DataTable dataTable2;
													if (flag13)
													{
														dataTable2 = clockWork.ExecuteQuery(QueryStorage.QS_Select_ServiceProviderByUsername, array);
													}
													else
													{
														dataTable2 = new DataTable();
													}
													bool flag14 = dataTable2.Rows.Count > 0;
													if (flag14)
													{
														int num3 = (int)dataTable2.Rows[0][0];
														userInfo.ClockworkNid = num3;
														return num3;
													}
													continue;
												}
											}
											else
											{
												if (!(text == "externalstudent"))
												{
													continue;
												}
												continue;
											}
										}
										else if (num != 3688173630U)
										{
											if (num != 3697442911U)
											{
												if (num != 4294341450U)
												{
													continue;
												}
												if (!(text == "dynamictextbox"))
												{
													continue;
												}
												string argSafe = lookupMethod.GetArgSafe("prefix");
												string argSafe2 = lookupMethod.GetArgSafe("postfix");
												string text5 = (argSafe + userInfo.Username + argSafe2).Trim();
												int num4 = int.Parse(lookupMethod.GetArgSafe("cid"));
												DbParameter[] array = new DbParameter[]
												{
													clockWork.GetParameter("@be", DbType.Binary, encryption.Encrypt(text5)),
													clockWork.GetParameter("@b", DbType.Binary, Core.StringToBytes(text5, false, null)),
													clockWork.GetParameter("@be2", DbType.Binary, encryption.Encrypt(text5.ToLower())),
													clockWork.GetParameter("@b2", DbType.Binary, Core.StringToBytes(text5.ToLower(), false, null)),
													clockWork.GetParameter("@cid", DbType.Int32, num4)
												};
												DataTable dataTable2 = clockWork.ExecuteQuery(QueryStorage.QS_Select_UserByDynamicDataEncryptedString2, array);
												bool flag15 = dataTable2.Rows.Count > 0;
												if (flag15)
												{
													int num5 = (int)dataTable2.Rows[0][0];
													userInfo.ClockworkPid = num5;
													return num5;
												}
												continue;
											}
											else
											{
												if (!(text == "notetakerstudent_no"))
												{
													continue;
												}
												string plainText2 = userInfo.Username.ToUpper().Trim();
												DbParameter[] array = new DbParameter[]
												{
													clockWork.Parameter
												};
												array[0].ParameterName = "@sne";
												array[0].DbType = DbType.Binary;
												array[0].Value = encryption.Encrypt(plainText2);
												DataTable dataTable2 = clockWork.ExecuteQuery(QueryStorage.QS_Select_ServiceProviderByStudent_no, array);
												bool flag16 = dataTable2.Rows.Count > 0;
												if (flag16)
												{
													int num6 = (int)dataTable2.Rows[0][0];
													userInfo.ClockworkNid = num6;
													return num6;
												}
												continue;
											}
										}
										else if (!(text == "ibemail"))
										{
											continue;
										}
										bool flag17 = userInfo.Username.IndexOf('@') >= 0;
										string email;
										if (flag17)
										{
											email = userInfo.Username;
										}
										else
										{
											email = userInfo.Username + lookupMethod.GetArgSafe("postfix");
										}
										ILookupInstructorClientManager lookupInstructorClientManager2 = new LookupInstructorClientManager();
										LookupInstructorDTO lookupInstructorDTO2 = lookupInstructorClientManager2.LoadInstructorByEmail(email);
										bool flag18 = lookupInstructorDTO2 != null && lookupInstructorDTO2.InstructorId > 0;
										if (flag18)
										{
											userInfo.ClockworkIid = lookupInstructorDTO2.InstructorId;
											return lookupInstructorDTO2.InstructorId;
										}
									}
								}
							}
						}
					}
				}
			}
			return -1;
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x00029C2C File Offset: 0x00027E2C
		public static UserInfo TryToLoginUserb(GroupMembership groups, string username, string password, List<Group> groupAuthentications, Cache Cache, HttpResponse Response, HttpRequest Request, HttpSessionState Session, iCustomLogin CustomLogin)
		{
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			IEncryption encryption = clockWork.Encryption;
			UserInfo userInfo = null;
			foreach (Group group in groupAuthentications)
			{
				bool flag = (group.GroupType & groups) == group.GroupType;
				if (flag)
				{
					foreach (AuthenticationLookupMethod authenticationLookupMethod in group.AuthenticationLookupMethods)
					{
						string type = authenticationLookupMethod.AuthenticationMethod.Type;
						string text = type;
						uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
						if (num <= 1382815865U)
						{
							if (num <= 368025498U)
							{
								if (num != 185716809U)
								{
									if (num == 368025498U)
									{
										if (text == "instructor")
										{
											bool flag2 = password.Trim().Length > 0;
											if (flag2)
											{
												bool settingValue = webSettingsClientManager.GetSettingValue<bool>(Setting.INSTRUCTOR_InstructorLoginDebugMode);
												bool flag3 = username.IndexOf("@") < 0;
												string text2;
												if (flag3)
												{
													string settingValue2 = webSettingsClientManager.GetSettingValue<string>(Setting.GENERAL_EmailSuffix);
													text2 = username + settingValue2;
												}
												else
												{
													text2 = username;
												}
												DbParameter[] parameters = new DbParameter[]
												{
													clockWork.GetParameter("@email", DbType.String, text2)
												};
												DataTable dataTable = clockWork.ExecuteQuery(QueryStorage.QS_Select_InstructorByEmail2, parameters);
												DataRow dataRow = null;
												bool flag4 = dataTable.Rows.Count > 0;
												string text4;
												if (flag4)
												{
													dataRow = dataTable.Rows[0];
													string strB = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "sha1");
													string text3 = (dataRow["password"] == DBNull.Value) ? "" : ((string)dataRow["password"]);
													bool flag5 = settingValue;
													if (flag5)
													{
														strB = text3;
													}
													bool flag6 = text3.CompareTo(strB) == 0;
													if (flag6)
													{
														text4 = null;
													}
													else
													{
														text4 = "Login failed.  Invalid email or password.";
													}
												}
												else
												{
													text4 = "Login failed. It does not appear that you have used this system before.";
												}
												bool flag7 = dataRow != null && dataRow["lucoursedataid"] == DBNull.Value;
												if (flag7)
												{
													text4 = "Login failed. It does not appear that you have used this system before.";
												}
												bool flag8 = text4 != null;
												if (flag8)
												{
													CWLogger.Logger.Info("AUTHENTICATION:PROF:{0}:Failed Login:{1}", username, text4);
												}
												else
												{
													userInfo = new UserInfo(username, dataRow["altlookupstring"].ToString(), text2, new GroupMembership[]
													{
														GroupMembership.instructors
													});
													userInfo.ClockworkIid = (int)dataRow["lucoursedataid"];
													Session.Add("username", username);
													Session.Add("userinfo", userInfo);
													Session.Add("authenticated", true);
													CWLogger.Logger.Info("AUTHENTICATION:PROF:{0}:Successful login (iid={1}", username, userInfo.ClockworkIid.ToString());
												}
											}
										}
									}
								}
								else if (text == "customnotetaker")
								{
									CustomLogin.CustomLogin(Session, Cache, Request, Response, ref userInfo, username, password, new StringDictionary
									{
										{
											"mode",
											"notetaker"
										}
									});
									bool flag9 = userInfo != null;
									if (flag9)
									{
										Session.Add("username", username);
										Session.Add("userinfo", userInfo);
										Session.Add("authenticated", true);
									}
								}
							}
							else if (num != 542584942U)
							{
								if (num == 1382815865U)
								{
									if (text == "externalstudent")
									{
										bool flag10 = password.Trim().Length > 0;
										if (flag10)
										{
											userInfo = null;
										}
									}
								}
							}
							else if (text == "custom")
							{
								CustomLogin.CustomLogin(Session, Cache, Request, Response, ref userInfo, username, password, authenticationLookupMethod.AuthenticationMethod.Args);
								bool flag11 = userInfo != null;
								if (flag11)
								{
									Session.Add("username", username);
									Session.Add("userinfo", userInfo);
									Session.Add("authenticated", true);
									CWLogger.Logger.Info("AUTHENTICATION:CUSTOM:{0}:Successful Login", username);
								}
								else
								{
									CWLogger.Logger.Info("AUTHENTICATION:CUSTOM:{0}:Failed Login", username);
								}
							}
						}
						else if (num <= 2200543148U)
						{
							if (num != 1483009432U)
							{
								if (num == 2200543148U)
								{
									if (text == "ldap")
									{
										bool flag12 = password.Length > 0;
										if (flag12)
										{
											int num2 = username.IndexOf("@");
											bool flag13 = num2 > 0;
											if (flag13)
											{
												username = username.Substring(0, num2);
											}
											LdapConnectionInfoDTO ldapConnectionInfoDTO = authenticationLookupMethod.AuthenticationMethod.Args.ParseConnectionInfo();
											ILdapClientManager ldapClientManager = new LdapClientManager();
											LdapAuthenticationResultDTO ldapAuthenticationResultDTO = ldapClientManager.LdapLogin(ldapConnectionInfoDTO, username, password);
											bool isAuthenticated = ldapAuthenticationResultDTO.IsAuthenticated;
											if (isAuthenticated)
											{
												string email = "";
												userInfo = new UserInfo(username, username, email, new GroupMembership[1]);
												Session.Add("username", username);
												Session.Add("userinfo", userInfo);
												Session.Add("authenticated", true);
												CWLogger.Logger.Info("AUTHENTICATION:LDAP:{0}:Successful Login", username);
											}
											else
											{
												StringBuilder stringBuilder = new StringBuilder();
												bool flag14 = ldapConnectionInfoDTO != null;
												if (flag14)
												{
													try
													{
														stringBuilder.AppendFormat("ldapauthtype={0}\r\n", ldapConnectionInfoDTO.AuthType.ToString());
														stringBuilder.AppendFormat("ldapdomain={0}\r\n", ldapConnectionInfoDTO.Domain ?? "NULL");
														stringBuilder.AppendFormat("isdoublebinding={0}\r\n", ldapConnectionInfoDTO.IsDoubleBinding.ToString());
														stringBuilder.AppendFormat("activedirectory={0}\r\n", ldapConnectionInfoDTO.IsActiveDirectory.ToString());
														stringBuilder.AppendFormat("ldaplookupattribute={0}\r\n", ldapConnectionInfoDTO.LookupAttribute ?? "NULL");
														stringBuilder.AppendFormat("ldappredomain={0}\r\n", ldapConnectionInfoDTO.PreDomain ?? "NULL");
														stringBuilder.AppendFormat("ldapprelookupattribute={0}\r\n", ldapConnectionInfoDTO.PreLookupAttribute ?? "NULL");
														stringBuilder.AppendFormat("ldapprepassword={0}\r\n", (ldapConnectionInfoDTO.PrePassword == null) ? "NULL" : ("*LENGTH=" + ldapConnectionInfoDTO.PrePassword.Length.ToString()));
														stringBuilder.AppendFormat("ldappreusername={0}\r\n", ldapConnectionInfoDTO.PreUsername ?? "NULL");
														stringBuilder.AppendFormat("ldapprotocolversion={0}\r\n", ldapConnectionInfoDTO.ProtocolVersion.ToString());
														stringBuilder.AppendFormat("ldapreturnattributes={0}\r\n", (ldapConnectionInfoDTO.ReturnAttributes == null) ? "NULL" : string.Join(",", ldapConnectionInfoDTO.ReturnAttributes));
														stringBuilder.AppendFormat("ldapserver={0}\r\n", ldapConnectionInfoDTO.ServerName ?? "NULL");
														stringBuilder.AppendFormat("ldapusessl={0}\r\n", ldapConnectionInfoDTO.SSL.ToString());
														stringBuilder.AppendFormat("ldapusetls={0}\r\n", ldapConnectionInfoDTO.TLS.ToString());
														stringBuilder.AppendFormat("ldapdontverifyservercertificate={0}\r\n", ldapConnectionInfoDTO.DontVerifyServerCertificate.ToString());
													}
													catch (Exception ex)
													{
														stringBuilder.Append("ERROR building log string: " + ex.ToString());
													}
												}
												foreach (object obj in authenticationLookupMethod.AuthenticationMethod.Args)
												{
													DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
													stringBuilder.AppendFormat("{0}={1}\r\n", dictionaryEntry.Key, dictionaryEntry.Value ?? "NULL");
												}
												CWLogger.Logger.Info("AUTHENTICATION:LDAP:{0}:Failed Login:emsg:{1}", username, string.Format("ErrorMessage={0}:ArgsCount={1}:LdapSettings={2}", ldapAuthenticationResultDTO.ErrorMessage ?? "NULL", (ldapAuthenticationResultDTO.ReturnAttributes == null) ? "NULL" : ldapAuthenticationResultDTO.ReturnAttributes.Count.ToString(), stringBuilder.ToString()));
											}
										}
									}
								}
							}
							else if (!(text == "debug"))
							{
							}
						}
						else if (num != 2816022776U)
						{
							if (num == 2969148036U)
							{
								if (!(text == "notetaker"))
								{
								}
							}
						}
						else if (text == "clockwork")
						{
							bool flag15 = password.Trim().Length > 0;
							if (flag15)
							{
								string value = authenticationLookupMethod.AuthenticationMethod.Args["groupids"];
								DbParameter[] parameters = new DbParameter[]
								{
									clockWork.GetParameter("@uname", DbType.Binary, encryption.Encrypt(username.ToUpper())),
									clockWork.GetParameter("@gids", DbType.String, value)
								};
								DataTable dataTable = clockWork.ExecuteQuery(QueryStorage.QS_Login_User, parameters);
								foreach (object obj2 in dataTable.Rows)
								{
									DataRow dataRow2 = (DataRow)obj2;
									string text5 = encryption.Decrypt((byte[])dataRow2["pass"]);
									bool flag16 = text5.CompareTo(password) == 0 && dataRow2["student_no"] != DBNull.Value;
									if (flag16)
									{
										string username2 = encryption.Decrypt((byte[])dataRow2["student_no"]);
										userInfo = new UserInfo(username2, username, "", new GroupMembership[1]);
										int num3 = (int)dataRow2[0];
										userInfo.ClockworkPid = num3;
										foreach (object obj3 in dataTable.Rows)
										{
											DataRow dataRow3 = (DataRow)obj3;
											int num4 = (int)dataRow3["personid"];
											bool flag17 = num4 == num3;
											if (flag17)
											{
												int num5 = (int)dataRow3["groupid"];
												int num6 = num5;
												int num7 = num6;
												if (num7 != 1)
												{
													if (num7 != 2)
													{
														if (num7 == 10)
														{
															userInfo.GroupMemberships |= GroupMembership.admin;
														}
													}
													else
													{
														userInfo.GroupMemberships |= GroupMembership.staff;
													}
												}
												else
												{
													userInfo.GroupMemberships |= GroupMembership.student;
												}
											}
										}
										Session.Add("username", username);
										Session.Add("userinfo", userInfo);
										Session.Add("authenticated", true);
										break;
									}
								}
							}
						}
						bool flag18 = userInfo != null;
						if (flag18)
						{
							userInfo.AuthenticationMethod = authenticationLookupMethod.AuthenticationMethod;
							userInfo.AuthenticationGroupMembership = group.GroupType;
							bool flag19 = group.GroupType == GroupMembership.student;
							if (flag19)
							{
								bool flag20 = false;
								bool flag21 = flag20;
								if (flag21)
								{
									Guid guid = Guid.NewGuid();
									string query = "INSERT INTO tokens (tokentype,token,expires,info,isactive,personid) \r\nVALUES (@tokentype,@token,dateadd(n,@minutesexpiry,getdate()),@username,1,@personid)";
									clockWork.ExecuteQuery(query, new DbParameter[]
									{
										clockWork.GetParameter("@tokentype", DbType.Int32, 95),
										clockWork.GetParameter("@token", DbType.String, guid.ToString()),
										clockWork.GetParameter("@minutesexpiry", DbType.Int32, 10),
										clockWork.GetParameter("@username", DbType.String, username),
										clockWork.GetParameter("@personid", DbType.Int32, userInfo.ClockworkPid)
									});
									Session.Add("usernametoken", guid.ToString());
								}
							}
							break;
						}
					}
					bool flag22 = userInfo != null;
					if (flag22)
					{
						break;
					}
				}
			}
			bool flag23 = userInfo == null;
			UserInfo result;
			if (flag23)
			{
				result = null;
			}
			else
			{
				result = userInfo;
			}
			return result;
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x0002A90C File Offset: 0x00028B0C
		public static UserInfo AuthorizeUser(string username, int studentEmailCid, bool emailFieldIsEncrypted, string studentEmailPostfix)
		{
			bool flag = string.IsNullOrEmpty(username) || username.Trim().Length < 1;
			UserInfo result;
			if (flag)
			{
				result = null;
			}
			else
			{
				Person person = null;
				Person person2 = null;
				Person person3 = null;
				Person person4 = null;
				Person person5 = null;
				string text = (username + studentEmailPostfix).ToLower();
				string query = "SELECT p.personid,p.firstname,p.middlename,p.lastname,p.student_no\r\nFROM    people p \r\nWHERE   p.isactive=1 \r\n        AND p.personid IN (SELECT personid FROM peoplegroups WHERE groupid=1) \r\n        AND p.personid IN (SELECT personid FROM otherinfops WHERE controlid=@cid AND controlvalue=@val)";
				DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
				DbParameter[] parameters;
				if (emailFieldIsEncrypted)
				{
					parameters = new DbParameter[]
					{
						clockWork.GetParameter("@cid", DbType.Int32, studentEmailCid),
						clockWork.GetParameter("@val", DbType.Binary, clockWork.Encryption.Encrypt(text))
					};
				}
				else
				{
					parameters = new DbParameter[]
					{
						clockWork.GetParameter("@cid", DbType.Int32, studentEmailCid),
						clockWork.GetParameter("@val", DbType.String, text)
					};
				}
				DataTable dataTable = clockWork.ExecuteQuery(query, parameters);
				bool flag2 = dataTable.Rows.Count > 0;
				if (flag2)
				{
					dataTable = clockWork.Encryption.EncryptOrDecryptNameDataTableBatch(false, dataTable, new string[]
					{
						"firstname",
						"middlename",
						"lastname",
						"student_no"
					});
					person = new Person((int)dataTable.Rows[0]["personid"], dataTable.Rows[0]["firstname"].ToString(), "", dataTable.Rows[0]["student_no"].ToString());
					person.FirstName = dataTable.Rows[0]["firstname"].ToString();
				}
				string query2 = "SELECT lucoursedataid,altlookupstring,email,phone,username FROM lucoursedata WHERE lookuplisttype=1 AND username=@username ORDER BY lucoursedataid";
				DbParameter[] parameters2 = new DbParameter[]
				{
					clockWork.GetParameter("@username", DbType.String, username)
				};
				DataTable dataTable2 = clockWork.ExecuteQuery(query2, parameters2);
				bool flag3 = dataTable2.Rows.Count > 0;
				if (flag3)
				{
					person2 = new Person((int)dataTable2.Rows[0]["lucoursedataid"], dataTable2.Rows[0]["altlookupstring"].ToString(), dataTable2.Rows[0]["email"].ToString());
				}
				GroupMembership groupMembership = GroupMembership.unknown;
				bool flag4 = person != null;
				if (flag4)
				{
					groupMembership |= GroupMembership.student;
				}
				bool flag5 = person2 != null;
				if (flag5)
				{
					groupMembership |= GroupMembership.instructors;
				}
				bool flag6 = person3 != null;
				if (flag6)
				{
					groupMembership |= GroupMembership.notetakers;
				}
				bool flag7 = person4 != null;
				if (flag7)
				{
					groupMembership |= GroupMembership.altcontact;
				}
				bool flag8 = person5 != null;
				if (flag8)
				{
					groupMembership |= GroupMembership.staff;
				}
				bool flag9 = person != null;
				UserInfo userInfo;
				if (flag9)
				{
					userInfo = new UserInfo(username, person.FirstName, "", new GroupMembership[]
					{
						groupMembership
					});
				}
				else
				{
					bool flag10 = person2 != null;
					if (flag10)
					{
						userInfo = new UserInfo(username, person2.Name, person2.Email, new GroupMembership[]
						{
							groupMembership
						});
					}
					else
					{
						bool flag11 = person4 != null;
						if (flag11)
						{
							userInfo = new UserInfo(username, person4.FirstName, person4.Email, new GroupMembership[]
							{
								groupMembership
							});
						}
						else
						{
							bool flag12 = person3 != null;
							if (flag12)
							{
								userInfo = new UserInfo(username, person3.FirstName, person3.Email, new GroupMembership[]
								{
									groupMembership
								});
							}
							else
							{
								userInfo = null;
							}
						}
					}
				}
				bool flag13 = userInfo != null;
				if (flag13)
				{
					bool flag14 = person != null;
					if (flag14)
					{
						userInfo.ClockworkPid = person.PersonId;
					}
					else
					{
						bool flag15 = person2 != null;
						if (flag15)
						{
							userInfo.ClockworkIid = person2.PersonId;
						}
						else
						{
							bool flag16 = person4 != null;
							if (flag16)
							{
								userInfo.ClockworkAltContactId = person4.PersonId;
							}
							else
							{
								bool flag17 = person3 != null;
								if (flag17)
								{
									userInfo.ClockworkNid = person3.PersonId;
								}
							}
						}
					}
					userInfo.AuthenticationMethod = new AuthenticationMethod("custom", "custom", "");
					userInfo.AuthenticationGroupMembership = GroupMembership.faculty;
				}
				result = userInfo;
			}
			return result;
		}

		// Token: 0x0400034B RID: 843
		public static Exception LastException;
	}
}
