using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Xml;

namespace TechnoPro.Common.UI.ClientManager.Web.Auth
{
	// Token: 0x02000008 RID: 8
	public static class Utility
	{
		// Token: 0x06000049 RID: 73 RVA: 0x00004208 File Offset: 0x00002408
		public static AuthenticationMethod LookupAuthenticationMethod(string name, List<AuthenticationMethod> authenticationLookupMethods)
		{
			return authenticationLookupMethods.FirstOrDefault((AuthenticationMethod m) => m.Is(name));
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000423C File Offset: 0x0000243C
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
					bool flag = !Utility.ParseBool(value);
					if (!flag)
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

		// Token: 0x0600004B RID: 75 RVA: 0x0000451C File Offset: 0x0000271C
		public static bool ParseBool(string boolstr)
		{
			return "1yestrue".IndexOf(boolstr.ToLower().Trim()) >= 0;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000454C File Offset: 0x0000274C
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
			return ((GroupMembership?)obj).GetValueOrDefault();
		}

		// Token: 0x0600004D RID: 77 RVA: 0x0000459C File Offset: 0x0000279C
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

		// Token: 0x0600004E RID: 78 RVA: 0x000045E8 File Offset: 0x000027E8
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
	}
}
