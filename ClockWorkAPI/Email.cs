using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Text;
using System.Xml;
using EmailClassLibrary;
using EncryptionClassLibrary;
using UnivOleDb;

namespace ClockWorkAPI
{
	// Token: 0x020000A8 RID: 168
	public class Email
	{
		// Token: 0x06000831 RID: 2097 RVA: 0x00031F8C File Offset: 0x00030F8C
		public static SmtpSettings ExtractSmtpSettingsFromRegistry()
		{
			string server = "";
			int num = 25;
			string username = null;
			string password = null;
			bool useSsl = false;
			bool useDefaultEmailSoftware = false;
			bool bodyHtml = true;
			string text = "";
			object registryValueStringCurrentUser = ClockWorkCore.GetRegistryValueStringCurrentUser("smtp", false);
			if (registryValueStringCurrentUser != null && registryValueStringCurrentUser is string)
			{
				server = ((string)registryValueStringCurrentUser).Trim();
			}
			object registryValueStringCurrentUser2 = ClockWorkCore.GetRegistryValueStringCurrentUser("eun", false);
			if (registryValueStringCurrentUser2 != null)
			{
				string text2 = (string)registryValueStringCurrentUser2;
				if (text2.Length > 0)
				{
					username = DPAPIencryption.UnProtectData(text2, DPAPIencryption.GetEntropy());
					object registryValueStringCurrentUser3 = ClockWorkCore.GetRegistryValueStringCurrentUser("eup", false);
					if (registryValueStringCurrentUser3 != null)
					{
						string text3 = (string)registryValueStringCurrentUser3;
						if (text3.Length > 0)
						{
							password = DPAPIencryption.UnProtectData(text3, DPAPIencryption.GetEntropy());
						}
					}
				}
			}
			object registryValueStringCurrentUser4 = ClockWorkCore.GetRegistryValueStringCurrentUser("eUseDefaultEmailSoftware", false);
			if (registryValueStringCurrentUser4 != null)
			{
				try
				{
					useDefaultEmailSoftware = Convert.ToBoolean(registryValueStringCurrentUser4);
				}
				catch
				{
				}
			}
			object registryValueStringCurrentUser5 = ClockWorkCore.GetRegistryValueStringCurrentUser("eUseSSL", false);
			if (registryValueStringCurrentUser5 != null)
			{
				try
				{
					useSsl = Convert.ToBoolean(registryValueStringCurrentUser5);
				}
				catch
				{
				}
			}
			object registryValueStringCurrentUser6 = ClockWorkCore.GetRegistryValueStringCurrentUser("eSMTPPortOut", false);
			if (registryValueStringCurrentUser6 != null)
			{
				string text4 = registryValueStringCurrentUser6.ToString().Trim();
				if (text4.Length > 0)
				{
					try
					{
						num = int.Parse(text4);
					}
					catch
					{
					}
				}
			}
			object registryValueStringCurrentUser7 = ClockWorkCore.GetRegistryValueStringCurrentUser("ebodyHtml", false);
			if (registryValueStringCurrentUser7 != null)
			{
				try
				{
					bodyHtml = Convert.ToBoolean(registryValueStringCurrentUser7);
				}
				catch
				{
				}
			}
			object registryValueStringCurrentUser8 = ClockWorkCore.GetRegistryValueStringCurrentUser("eDefaultFrom", false);
			if (registryValueStringCurrentUser8 != null)
			{
				string text5 = ((string)registryValueStringCurrentUser8).Trim();
				if (text5.Length > 0)
				{
					text = text5;
				}
			}
			text = text.Trim();
			if (num < 0)
			{
				num = 25;
			}
			return new SmtpSettings(num, server, username, password)
			{
				UseSsl = useSsl,
				DefaultFrom = text,
				BodyHtml = bodyHtml,
				UseDefaultEmailSoftware = useDefaultEmailSoftware
			};
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x00032214 File Offset: 0x00031214
		public static void ExtractBlankReplacementsAndWarningErrorCodesFromEmailTemplateRow(DataRow emailTemplateRow, out StringDictionary blankReplacements, out string[] warningIfMissingCodes, out string[] errorIfMissingCodes)
		{
			blankReplacements = Email.ParseStringDictionary((emailTemplateRow["blankreplacements"] == DBNull.Value) ? "" : ((string)emailTemplateRow["blankreplacements"]));
			warningIfMissingCodes = Email.ParseStringArrayCommaSeparated((emailTemplateRow["warningifmissingcodes"] == DBNull.Value) ? "" : ((string)emailTemplateRow["warningifmissingcodes"]));
			errorIfMissingCodes = Email.ParseStringArrayCommaSeparated((emailTemplateRow["errorifmissingcodes"] == DBNull.Value) ? "" : ((string)emailTemplateRow["errorifmissingcodes"]));
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x000322B4 File Offset: 0x000312B4
		private static string[] ParseStringArrayCommaSeparated(string s)
		{
			string[] result;
			if (s.Trim().Length < 1)
			{
				result = null;
			}
			else
			{
				string[] array = s.Split(new char[]
				{
					','
				});
				ArrayList arrayList = new ArrayList();
				foreach (string text in array)
				{
					string text2 = text.Trim();
					if (text2.Length > 0)
					{
						arrayList.Add(text2);
					}
				}
				if (arrayList.Count > 0)
				{
					string[] array3 = new string[arrayList.Count];
					for (int j = 0; j < array3.Length; j++)
					{
						array3[j] = (string)arrayList[j];
					}
					result = array3;
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x000323A0 File Offset: 0x000313A0
		private static StringDictionary ParseStringDictionary(string s)
		{
			StringDictionary result;
			if (s.Trim().Length < 1)
			{
				result = null;
			}
			else
			{
				StringDictionary stringDictionary = new StringDictionary();
				string[] array = s.Split(Environment.NewLine.ToCharArray());
				foreach (string text in array)
				{
					int num = text.IndexOf("=");
					if (num > 0)
					{
						string key = text.Substring(0, num);
						string value = text.Substring(num + 1);
						stringDictionary.Add(key, value);
					}
				}
				if (stringDictionary.Count > 0)
				{
					result = stringDictionary;
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x00032464 File Offset: 0x00031464
		public static DataRow LoadEmailTemplateRow(UnivDataAdapter da, int templateId)
		{
			da.SelectCommand.CommandText = "SELECT templateid,efrom,eto,ecc,ebcc,eattachments,ebody,emisc,blankreplacements,warningifmissingcodes,errorifmissingcodes FROM emailtemplates WHERE templateid=@templateid";
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@templateid", templateId);
			DataTable dataTable = new DataTable();
			string text;
			da.Fill(dataTable, out text);
			DataRow result;
			if (dataTable.Rows.Count > 0)
			{
				result = dataTable.Rows[0];
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x000324EC File Offset: 0x000314EC
		public static Hashtable ParseEmailTemplate(string template)
		{
			Hashtable hashtable = new Hashtable();
			if (!string.IsNullOrEmpty(template))
			{
				if (template.StartsWith("<email>"))
				{
					try
					{
						XmlDocument xmlDocument = new XmlDocument();
						string xml = "<?xml version=\"1.0\"?>" + template;
						xmlDocument.LoadXml(xml);
						foreach (object obj in xmlDocument.LastChild.ChildNodes)
						{
							XmlNode xmlNode = (XmlNode)obj;
							string text = xmlNode.Name.ToLower();
							string value = xmlNode.InnerText.Replace("#~", "#<").Replace("~#", ">#");
							if (xmlNode.Attributes.Count > 0)
							{
								if (!string.IsNullOrEmpty(value) && !hashtable.ContainsKey(text))
								{
									hashtable.Add(text, value);
								}
								foreach (object obj2 in xmlNode.Attributes)
								{
									XmlAttribute xmlAttribute = (XmlAttribute)obj2;
									string key = string.Format("{0}.{1}", text, xmlAttribute.Name);
									if (!hashtable.ContainsKey(key))
									{
										hashtable.Add(key, (xmlAttribute.Value == null) ? "" : xmlAttribute.Value);
									}
								}
							}
							else if (!hashtable.ContainsKey(text))
							{
								hashtable.Add(text, value);
							}
						}
						if (hashtable.ContainsKey("testmode.isactive") && hashtable["testmode.isactive"].ToString().Equals("1"))
						{
							string arg = hashtable.ContainsKey("to") ? hashtable["to"].ToString() : "";
							string text2 = hashtable.ContainsKey("cc") ? hashtable["cc"].ToString() : "";
							string text3 = hashtable.ContainsKey("bcc") ? hashtable["bcc"].ToString() : "";
							string text4 = hashtable.ContainsKey("body") ? hashtable["body"].ToString() : "";
							string value2 = hashtable.ContainsKey("testmode.adminemail") ? hashtable["testmode.adminemail"].ToString() : "";
							StringBuilder stringBuilder = new StringBuilder();
							stringBuilder.Append(string.Format("** TEST MODE **\nOriginal to: {0}\n", arg));
							if (!string.IsNullOrEmpty(text2))
							{
								stringBuilder.Append(string.Format("Original cc: {0}", text2));
							}
							if (!string.IsNullOrEmpty(text3))
							{
								stringBuilder.Append(string.Format("Original bcc: {0}", text3));
							}
							if (hashtable.ContainsKey("to"))
							{
								hashtable["to"] = value2;
							}
							else
							{
								hashtable.Add("to", value2);
							}
							if (hashtable.ContainsKey("cc"))
							{
								hashtable["cc"] = "";
							}
							if (hashtable.ContainsKey("bcc"))
							{
								hashtable["bcc"] = "";
							}
							text4 = stringBuilder.ToString() + text4;
							if (hashtable.ContainsKey("body"))
							{
								hashtable["body"] = text4;
							}
							else
							{
								hashtable.Add("body", text4);
							}
						}
					}
					catch (Exception ex)
					{
					}
				}
				else
				{
					string newLine = Environment.NewLine;
					int length = newLine.Length;
					int num;
					for (int i = 0; i < template.Length; i = num + length)
					{
						num = template.IndexOf(newLine, i);
						string text5;
						if (num > i)
						{
							text5 = template.Substring(i, num - i);
						}
						else
						{
							text5 = template.Substring(i);
						}
						if (text5.Trim().Length < 1 || text5.IndexOf(newLine) == 0)
						{
							hashtable.Add("body", template.Substring(i));
							break;
						}
						int num2 = text5.IndexOf(':');
						if (num2 > 0)
						{
							string text = text5.Substring(0, num2);
							int num3 = num2 + 2;
							string value = (num3 < text5.Length) ? text5.Substring(num3) : "";
							hashtable.Add(text.ToLower().Trim(), value);
						}
					}
				}
			}
			return hashtable;
		}
	}
}
