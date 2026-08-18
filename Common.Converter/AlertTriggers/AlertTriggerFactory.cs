using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TechnoPro.Common.Converter.AlertTriggers.Serializers;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.AlertTrigger;
using TechnoPro.Common.Public.Entities.AlertTrigger.AlertTriggerDefinitions;

namespace TechnoPro.Common.Converter.AlertTriggers
{
	// Token: 0x02000027 RID: 39
	public static class AlertTriggerFactory
	{
		// Token: 0x060000C3 RID: 195 RVA: 0x000055D4 File Offset: 0x000037D4
		public static AlertTriggerDefinitionBase[] DeSerializeAlertTriggers(this string xml)
		{
			bool flag = string.IsNullOrEmpty(xml);
			AlertTriggerDefinitionBase[] result;
			if (flag)
			{
				result = new AlertTriggerDefinitionBase[0];
			}
			else
			{
				List<Pair<eAlertTriggerType, AlertTriggerTypeAttribute>> alertTriggerTypes = (from g in (eAlertTriggerType[])Enum.GetValues(typeof(eAlertTriggerType))
				select new Pair<eAlertTriggerType, AlertTriggerTypeAttribute>(g, g.GetAttribute<AlertTriggerTypeAttribute>()) into n
				where !n.Item2.IsDisabled
				select n).ToList<Pair<eAlertTriggerType, AlertTriggerTypeAttribute>>();
				bool flag2 = !xml.ToLower().Contains("<alerttriggers>");
				if (flag2)
				{
					result = AlertTriggerFactory.DeSerializeAlertTriggersLegacy(xml, alertTriggerTypes);
				}
				else
				{
					result = AlertTriggerFactory.DeSerializeAlertTriggers(xml, alertTriggerTypes);
				}
			}
			return result;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00005688 File Offset: 0x00003888
		public static string SerializeAlertTriggers(this AlertTriggerDefinitionBase[] alertTriggers)
		{
			bool flag = alertTriggers == null || alertTriggers.Length < 1;
			string result;
			if (flag)
			{
				result = string.Empty;
			}
			else
			{
				XDeclaration declaration = new XDeclaration("1.0", "utf-8", "yes");
				object[] array = new object[1];
				array[0] = new XElement("alerttriggers", from h in alertTriggers.Select(new Func<AlertTriggerDefinitionBase, XElement>(AlertTriggerFactory.SerializeAlertTrigger))
				where h != null
				select h);
				XDocument xdocument = new XDocument(declaration, array);
				result = xdocument.Declaration.ToString() + xdocument.ToString();
			}
			return result;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00005734 File Offset: 0x00003934
		private static XElement SerializeAlertTrigger(AlertTriggerDefinitionBase alertTrigger)
		{
			bool flag = alertTrigger == null;
			XElement result;
			if (flag)
			{
				result = null;
			}
			else
			{
				XElement xelement;
				bool flag2 = alertTrigger.TryToSerializeAlertTrigger(out xelement);
				if (flag2)
				{
					result = xelement;
				}
				else
				{
					bool flag3 = alertTrigger.TryToSerializeAlertTrigger(out xelement);
					if (flag3)
					{
						result = xelement;
					}
					else
					{
						bool flag4 = alertTrigger.TryToSerializeAlertTrigger(out xelement);
						if (flag4)
						{
							result = xelement;
						}
						else
						{
							bool flag5 = alertTrigger.TryToSerializeAlertTrigger(out xelement);
							if (flag5)
							{
								result = xelement;
							}
							else
							{
								bool flag6 = alertTrigger.TryToSerializeAlertTrigger(out xelement);
								if (flag6)
								{
									result = xelement;
								}
								else
								{
									result = null;
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000057AC File Offset: 0x000039AC
		private static bool TryToSerializeAlertTrigger<T>(this AlertTriggerDefinitionBase alertTrigger, out XElement xElement) where T : AlertTriggerDefinitionBase
		{
			T t = alertTrigger as T;
			bool result = t != null;
			xElement = ((t == null) ? null : AlertTriggerFactory.GetSerializer<T>().Serialize(t));
			return result;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000057F0 File Offset: 0x000039F0
		public static T ExtractBaseAlertTriggerDefinition<T>(this XElement element) where T : AlertTriggerDefinitionBase
		{
			T t = Activator.CreateInstance<T>();
			AlertTriggerDefinitionBase alertTriggerDefinitionBase = t;
			XAttribute xattribute = element.Attribute("disabled");
			bool? flag;
			if (xattribute == null)
			{
				flag = null;
			}
			else
			{
				string value = xattribute.Value;
				flag = ((value != null) ? new bool?(value.ConvertStringToBool(false)) : null);
			}
			bool? flag2 = flag;
			alertTriggerDefinitionBase.IsDisabled = flag2.GetValueOrDefault();
			AlertTriggerDefinitionBase alertTriggerDefinitionBase2 = t;
			XAttribute xattribute2 = element.Attribute("ordernum");
			int? num;
			if (xattribute2 == null)
			{
				num = null;
			}
			else
			{
				string value2 = xattribute2.Value;
				num = ((value2 != null) ? new int?(value2.ConvertStringToInt(0)) : null);
			}
			int? num2 = num;
			alertTriggerDefinitionBase2.OrderNum = num2.GetValueOrDefault();
			AlertTriggerDefinitionBase alertTriggerDefinitionBase3 = t;
			XAttribute xattribute3 = element.Attribute("name");
			alertTriggerDefinitionBase3.Name = (((xattribute3 != null) ? xattribute3.Value : null) ?? "");
			AlertTriggerDefinitionBase alertTriggerDefinitionBase4 = t;
			XAttribute xattribute4 = element.Attribute("note");
			alertTriggerDefinitionBase4.Note = (((xattribute4 != null) ? xattribute4.Value : null) ?? "");
			AlertTriggerDefinitionBase alertTriggerDefinitionBase5 = t;
			XAttribute xattribute5 = element.Attribute("noapps");
			alertTriggerDefinitionBase5.DontAllowAppointmentBooking = (xattribute5 != null && xattribute5.GetBoolFromAttribute(false));
			return t;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00005938 File Offset: 0x00003B38
		public static XElement CreateBaseAlertTriggerElement<T>(this AlertTriggerDefinitionBase dataObj) where T : AlertTriggerDefinitionBase
		{
			bool flag = dataObj == null;
			XElement result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new XElement("alerttrigger", new object[]
				{
					new XAttribute("name", dataObj.Name ?? ""),
					new XAttribute("note", dataObj.Note ?? ""),
					new XAttribute("code", dataObj.GetCode()),
					new XAttribute("isdisabled", dataObj.IsDisabled),
					new XAttribute("ordernum", dataObj.OrderNum),
					new XAttribute("noapps", dataObj.DontAllowAppointmentBooking.ToString())
				});
			}
			return result;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00005A24 File Offset: 0x00003C24
		private static AlertTriggerDefinitionBase[] DeSerializeAlertTriggers(string xml, IList<Pair<eAlertTriggerType, AlertTriggerTypeAttribute>> alertTriggerTypes)
		{
			bool flag = string.IsNullOrEmpty(xml);
			AlertTriggerDefinitionBase[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				try
				{
					XDocument xdocument = XDocument.Parse(xml);
					List<AlertTriggerDefinitionBase> list = (from xe in xdocument.Descendants("alerttrigger")
					select AlertTriggerFactory.DeSerializeAlertTrigger(xe, alertTriggerTypes) into g
					where g != null
					select g).ToList<AlertTriggerDefinitionBase>();
					list.Sort((AlertTriggerDefinitionBase g1, AlertTriggerDefinitionBase g2) => g1.OrderNum.CompareTo(g2.OrderNum));
					return list.ToArray();
				}
				catch (Exception ex)
				{
				}
				result = null;
			}
			return result;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00005AF4 File Offset: 0x00003CF4
		private static AlertTriggerDefinitionBase DeSerializeAlertTrigger(XElement element, IList<Pair<eAlertTriggerType, AlertTriggerTypeAttribute>> alertTriggerTypes)
		{
			AlertTriggerFactory.<>c__DisplayClass7_0 CS$<>8__locals1 = new AlertTriggerFactory.<>c__DisplayClass7_0();
			AlertTriggerFactory.<>c__DisplayClass7_0 CS$<>8__locals2 = CS$<>8__locals1;
			XAttribute xattribute = element.Attribute("code");
			string codeStr;
			if (xattribute == null)
			{
				codeStr = null;
			}
			else
			{
				string value = xattribute.Value;
				codeStr = ((value != null) ? value.Trim().ToLower() : null);
			}
			CS$<>8__locals2.codeStr = codeStr;
			bool flag = string.IsNullOrEmpty(CS$<>8__locals1.codeStr);
			AlertTriggerDefinitionBase result;
			if (flag)
			{
				result = null;
			}
			else
			{
				Pair<eAlertTriggerType, AlertTriggerTypeAttribute> pair = alertTriggerTypes.FirstOrDefault((Pair<eAlertTriggerType, AlertTriggerTypeAttribute> g) => g.Item2.DefinitionBaseType.GetCode() == CS$<>8__locals1.codeStr);
				bool flag2 = pair == null || pair.Item1 == eAlertTriggerType.Unknown;
				if (flag2)
				{
					result = null;
				}
				else
				{
					switch (pair.Item1)
					{
					case eAlertTriggerType.Unknown:
						result = null;
						break;
					case eAlertTriggerType.ExistingInfo:
						result = AlertTriggerFactory.DeSerializeAlertTrigger<AlertTriggerDefinitionExistingInfoBase>(element);
						break;
					case eAlertTriggerType.ExpiredAccommodations:
						result = AlertTriggerFactory.DeSerializeAlertTrigger<AlertTriggerDefinitionExpiredAccommodationsBase>(element);
						break;
					case eAlertTriggerType.MissingInfo:
						result = AlertTriggerFactory.DeSerializeAlertTrigger<AlertTriggerDefinitionMissingInfoBase>(element);
						break;
					case eAlertTriggerType.RequiredSessionForm:
						result = AlertTriggerFactory.DeSerializeAlertTrigger<AlertTriggerDefinitionRequiredSessionFormBase>(element);
						break;
					case eAlertTriggerType.TempStudentNumber:
						result = AlertTriggerFactory.DeSerializeAlertTrigger<AlertTriggerDefinitionTempStudentNumberBase>(element);
						break;
					default:
						result = null;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00005BE0 File Offset: 0x00003DE0
		private static T DeSerializeAlertTrigger<T>(XElement element) where T : AlertTriggerDefinitionBase
		{
			return AlertTriggerFactory.GetSerializer<T>().DeSerialize(element);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00005C00 File Offset: 0x00003E00
		private static IAlertTriggerDefinitionSerializer<T> GetSerializer<T>() where T : AlertTriggerDefinitionBase
		{
			Type typeFromHandle = typeof(T);
			bool flag = typeFromHandle == typeof(AlertTriggerDefinitionExistingInfoBase);
			IAlertTriggerDefinitionSerializer<T> result;
			if (flag)
			{
				result = (IAlertTriggerDefinitionSerializer<T>)new AlertTriggerDefinitionExistingInfoSerializer();
			}
			else
			{
				bool flag2 = typeFromHandle == typeof(AlertTriggerDefinitionExpiredAccommodationsBase);
				if (flag2)
				{
					result = (IAlertTriggerDefinitionSerializer<T>)new AlertTriggerDefinitionExpiredAccommodationsSerializer();
				}
				else
				{
					bool flag3 = typeFromHandle == typeof(AlertTriggerDefinitionMissingInfoBase);
					if (flag3)
					{
						result = (IAlertTriggerDefinitionSerializer<T>)new AlertTriggerDefinitionMissingInfoSerializer();
					}
					else
					{
						bool flag4 = typeFromHandle == typeof(AlertTriggerDefinitionRequiredSessionFormBase);
						if (flag4)
						{
							result = (IAlertTriggerDefinitionSerializer<T>)new AlertTriggerDefinitionRequiredSessionFormSerializer();
						}
						else
						{
							bool flag5 = typeFromHandle == typeof(AlertTriggerDefinitionTempStudentNumberBase);
							if (!flag5)
							{
								throw new NotImplementedException();
							}
							result = (IAlertTriggerDefinitionSerializer<T>)new AlertTriggerDefinitionTempStudentNumberSerializer();
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00005CD0 File Offset: 0x00003ED0
		private static AlertTriggerDefinitionBase[] DeSerializeAlertTriggersLegacy(string s, IList<Pair<eAlertTriggerType, AlertTriggerTypeAttribute>> alertTriggerTypes)
		{
			List<AlertTriggerDefinitionBase> list = new List<AlertTriggerDefinitionBase>();
			List<string> list2 = (from g in (s ?? "").Split(new char[]
			{
				'`'
			})
			select g.Trim() into h
			where h.Length > 0
			select h).ToList<string>();
			foreach (string legacyString in list2)
			{
				AlertTriggerDefinitionBase[] array = AlertTriggerFactory.DeSerializeAlertTriggerLegacy(legacyString, alertTriggerTypes);
				bool flag = array != null && array.Length != 0;
				if (flag)
				{
					list.AddRange(array);
				}
			}
			return list.ToArray();
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00005DBC File Offset: 0x00003FBC
		private static AlertTriggerDefinitionBase[] DeSerializeAlertTriggerLegacy(string legacyString, IList<Pair<eAlertTriggerType, AlertTriggerTypeAttribute>> alertTriggerTypes)
		{
			string[] array = (from g in legacyString.Split(new char[]
			{
				','
			})
			select g.Trim()).ToArray<string>();
			bool flag = array.Length < 1;
			AlertTriggerDefinitionBase[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string codeStr = array[0].ToLower().Trim();
				string[] parts = (array.Length > 1) ? array.ToList<string>().GetRange(1, array.Length - 1).ToArray() : new string[0];
				Pair<eAlertTriggerType, AlertTriggerTypeAttribute> pair = alertTriggerTypes.FirstOrDefault((Pair<eAlertTriggerType, AlertTriggerTypeAttribute> g) => g.Item2.DefinitionBaseType.GetCode() == codeStr);
				bool flag2 = pair == null || pair.Item1 == eAlertTriggerType.Unknown;
				if (flag2)
				{
					result = null;
				}
				else
				{
					switch (pair.Item1)
					{
					case eAlertTriggerType.Unknown:
						result = null;
						break;
					case eAlertTriggerType.ExistingInfo:
					{
						AlertTriggerDefinitionBase[] array2 = AlertTriggerFactory.DeSerializeAlertTriggerLegacy<AlertTriggerDefinitionExistingInfoBase>(codeStr, parts);
						result = array2;
						break;
					}
					case eAlertTriggerType.ExpiredAccommodations:
					{
						AlertTriggerDefinitionBase[] array2 = AlertTriggerFactory.DeSerializeAlertTriggerLegacy<AlertTriggerDefinitionExpiredAccommodationsBase>(codeStr, parts);
						result = array2;
						break;
					}
					case eAlertTriggerType.MissingInfo:
					{
						AlertTriggerDefinitionBase[] array2 = AlertTriggerFactory.DeSerializeAlertTriggerLegacy<AlertTriggerDefinitionMissingInfoBase>(codeStr, parts);
						result = array2;
						break;
					}
					case eAlertTriggerType.RequiredSessionForm:
					{
						AlertTriggerDefinitionBase[] array2 = AlertTriggerFactory.DeSerializeAlertTriggerLegacy<AlertTriggerDefinitionRequiredSessionFormBase>(codeStr, parts);
						result = array2;
						break;
					}
					case eAlertTriggerType.TempStudentNumber:
					{
						AlertTriggerDefinitionBase[] array2 = AlertTriggerFactory.DeSerializeAlertTriggerLegacy<AlertTriggerDefinitionTempStudentNumberBase>(codeStr, parts);
						result = array2;
						break;
					}
					default:
						result = null;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00005F24 File Offset: 0x00004124
		private static T[] DeSerializeAlertTriggerLegacy<T>(string codeStr, string[] parts) where T : AlertTriggerDefinitionBase
		{
			IAlertTriggerDefinitionSerializer<T> serializer = AlertTriggerFactory.GetSerializer<T>();
			return serializer.DeSerializeLegacy(codeStr, parts);
		}
	}
}
