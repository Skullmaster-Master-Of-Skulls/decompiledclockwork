using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.DynamicForms.DynamicControls;
using TechnoPro.Common.TextFormat.Adapters;

namespace TechnoPro.Common.Public.Entities.Adapters
{
	// Token: 0x020005D8 RID: 1496
	public static class DynamicFormsAdapter
	{
		// Token: 0x06003012 RID: 12306 RVA: 0x0003C100 File Offset: 0x0003A300
		public static SingleFileMetaData GetSingleFileMetaDataFromXml(this string xml)
		{
			bool flag = string.IsNullOrEmpty(xml);
			SingleFileMetaData result;
			if (flag)
			{
				result = null;
			}
			else
			{
				XDocument xdocument = XDocument.Parse(xml);
				IEnumerable<XElement> enumerable = from el in xdocument.Descendants("file")
				select el;
				using (IEnumerator<XElement> enumerator = enumerable.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						XElement xelement = enumerator.Current;
						bool flag2 = !xelement.HasAttributes;
						if (flag2)
						{
							return null;
						}
						string fileName = ((string)xelement.Attribute("fn")) ?? "";
						string s = ((string)xelement.Attribute("id")) ?? "";
						int dataId;
						int.TryParse(s, out dataId);
						return new SingleFileMetaData
						{
							FileName = fileName,
							DataId = dataId
						};
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x06003013 RID: 12307 RVA: 0x0003C220 File Offset: 0x0003A420
		public static string ConvertSingleFileMetaDataToXml(this SingleFileMetaData item)
		{
			bool flag = item == null;
			string result;
			if (flag)
			{
				result = null;
			}
			else
			{
				XElement xelement = new XElement("file", new object[]
				{
					new XAttribute("fn", item.FileName ?? ""),
					new XAttribute("id", item.DataId.ToString())
				});
				result = xelement.ToString();
			}
			return result;
		}

		// Token: 0x06003014 RID: 12308 RVA: 0x0003C29C File Offset: 0x0003A49C
		public static string GetString(this DynamicData Data)
		{
			bool flag = Data == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				bool flag2 = Data.Field == null;
				if (flag2)
				{
					object value = Data.Value;
					result = (((value != null) ? value.ToString() : null) ?? "");
				}
				else
				{
					object secondaryValue;
					object valueAndSecondaryValue = DynamicFormsAdapter.GetValueAndSecondaryValue(Data, out secondaryValue);
					result = DynamicFormsAdapter.GetString(Data.Field.ControlCode, Data.Field.GetCaptionForDisplay(), secondaryValue, valueAndSecondaryValue, Data.Field.Setting3);
				}
			}
			return result;
		}

		// Token: 0x06003015 RID: 12309 RVA: 0x0003C31C File Offset: 0x0003A51C
		private static object GetValueAndSecondaryValue(DynamicData data, out object secondaryValue)
		{
			object value = data.Value;
			object secondaryValue2 = data.SecondaryValue;
			eControlCode controlCode = data.Field.ControlCode;
			return DynamicFormsAdapter.GetValueAndSecondaryValue(value, secondaryValue2, controlCode, out secondaryValue);
		}

		// Token: 0x06003016 RID: 12310 RVA: 0x0003C354 File Offset: 0x0003A554
		private static object GetValueAndSecondaryValue(object v, object sv, eControlCode controlCode, out object secondaryValue)
		{
			bool flag = controlCode == eControlCode.MultiCheckBoxText || controlCode == eControlCode.MultiCheckBoxDropList;
			if (flag)
			{
				bool flag2 = sv != null && (sv is int || sv is bool);
				if (flag2)
				{
					sv = null;
				}
				else
				{
					bool flag3 = v != null && (v is int || v is bool);
					if (flag3)
					{
						v = sv;
						sv = null;
					}
				}
			}
			secondaryValue = sv;
			return v;
		}

		// Token: 0x06003017 RID: 12311 RVA: 0x0003C3CC File Offset: 0x0003A5CC
		public static DynamicControlAttribute GetControlCodeAttribute(this eControlCode ControlCode)
		{
			return DynamicControlAttribute.GetAttribute<DynamicControlAttribute>(ControlCode);
		}

		// Token: 0x06003018 RID: 12312 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public static string GetDescription(this eControlCode ControlCode)
		{
			DynamicControlAttribute controlCodeAttribute = ControlCode.GetControlCodeAttribute();
			return string.IsNullOrEmpty((controlCodeAttribute != null) ? controlCodeAttribute.Description : null) ? "" : controlCodeAttribute.Description;
		}

		// Token: 0x06003019 RID: 12313 RVA: 0x0003C428 File Offset: 0x0003A628
		public static string GetTitle(this eControlCode ControlCode)
		{
			DynamicControlAttribute controlCodeAttribute = ControlCode.GetControlCodeAttribute();
			bool flag = controlCodeAttribute == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				result = (string.IsNullOrEmpty(controlCodeAttribute.Title) ? ControlCode.ToString() : controlCodeAttribute.Title);
			}
			return result;
		}

		// Token: 0x0600301A RID: 12314 RVA: 0x0003C474 File Offset: 0x0003A674
		public static string GetString(eControlCode controlCode, string caption, object secondaryValue, object value, int setting3)
		{
			DateTime? dateTime = null;
			string text = null;
			int? num = null;
			bool? flag = null;
			int? num2 = null;
			bool flag2 = value != null;
			if (flag2)
			{
				bool flag3 = value is DateTime;
				if (flag3)
				{
					dateTime = new DateTime?((DateTime)value);
				}
				else
				{
					bool flag4 = value is string;
					if (flag4)
					{
						text = (string)value;
					}
					else
					{
						bool flag5 = value is int;
						if (flag5)
						{
							num = new int?((int)value);
						}
						else
						{
							bool flag6 = value is bool;
							if (flag6)
							{
								flag = new bool?((bool)value);
							}
						}
					}
				}
			}
			bool flag7 = secondaryValue is int;
			if (flag7)
			{
				num2 = new int?((int)secondaryValue);
			}
			if (controlCode <= eControlCode.MultiCheckBoxText)
			{
				if (controlCode <= eControlCode.ListSelect)
				{
					if (controlCode != eControlCode.CheckBox && controlCode != eControlCode.ListSelect)
					{
						goto IL_326;
					}
				}
				else
				{
					if (controlCode != eControlCode.MultiCheckBox && controlCode != eControlCode.MultiCheckBoxText)
					{
						goto IL_326;
					}
					goto IL_186;
				}
			}
			else if (controlCode <= eControlCode.RtfTextBox)
			{
				if (controlCode == eControlCode.MultiCheckBoxDropList)
				{
					goto IL_186;
				}
				if (controlCode != eControlCode.RtfTextBox)
				{
					goto IL_326;
				}
				bool flag8 = !string.IsNullOrEmpty(text);
				if (flag8)
				{
					try
					{
						return text.ConvertRtfToPlainText();
					}
					catch (Exception ex)
					{
					}
					return text;
				}
				return "";
			}
			else
			{
				if (controlCode == eControlCode.MultiLineTextBox)
				{
					IList<MultiLineTextBoxItem> source = text.ConvertXmlToMultiLineTextBoxItems();
					return string.Join(Environment.NewLine, (from g in source
					select string.Concat(new string[]
					{
						"• (",
						(g.DateEntered != null) ? (g.DateEntered.Value.ToString("yyyy-MM-dd h:mm tt") + (string.IsNullOrEmpty(g.WhoEntered) ? "" : " ")) : "",
						g.WhoEntered ?? "",
						")\r\n",
						g.Text ?? ""
					})).ToArray<string>());
				}
				switch (controlCode)
				{
				case eControlCode.AccommodationCheckBox:
					break;
				case eControlCode.AccommodationTextBox:
				case eControlCode.AccommodationDropList:
					return string.IsNullOrEmpty(text) ? (caption ?? "") : (caption + "  " + text);
				case eControlCode.AccommodationDatePicker:
					goto IL_326;
				default:
					goto IL_326;
				}
			}
			return caption.EndsWith("?") ? caption.Substring(0, caption.Length - 1) : caption;
			IL_186:
			int num3 = num2 ?? num.GetValueOrDefault();
			string[] array = caption.Split(new char[]
			{
				'.'
			});
			int num4 = array.Length;
			bool flag9 = controlCode == eControlCode.MultiCheckBoxDropList;
			if (flag9)
			{
				bool flag10 = setting3 == 0 || setting3 == 2;
				if (flag10)
				{
					int num5 = num3 >> num4;
					num5--;
					num3 -= num5;
				}
			}
			StringBuilder stringBuilder = new StringBuilder();
			bool flag11 = num3 > 1;
			if (flag11)
			{
				for (int i = 0; i < num4; i++)
				{
					bool flag12 = i > 0;
					if (flag12)
					{
						stringBuilder.Append(" ");
					}
					stringBuilder.Append(((num3 & i + 1) > 0).ToString());
				}
			}
			return (!string.IsNullOrEmpty(text)) ? (text + " " + stringBuilder.ToString()) : stringBuilder.ToString();
			IL_326:
			bool flag13 = !string.IsNullOrEmpty(text);
			string result;
			if (flag13)
			{
				result = text;
			}
			else
			{
				bool flag14 = flag != null;
				if (flag14)
				{
					result = (flag.Value ? "Yes" : "No");
				}
				else
				{
					bool flag15 = dateTime != null;
					if (flag15)
					{
						result = dateTime.Value.ToString("MMMM d, yyyy");
					}
					else
					{
						bool flag16 = num != null;
						if (flag16)
						{
							result = num.Value.ToString();
						}
						else
						{
							bool flag17 = secondaryValue != null;
							if (flag17)
							{
								result = secondaryValue.ToString();
							}
							else
							{
								result = caption;
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600301B RID: 12315 RVA: 0x0003C858 File Offset: 0x0003AA58
		public static IList<MultiLineTextBoxItem> ConvertXmlToMultiLineTextBoxItems(this string xml)
		{
			List<MultiLineTextBoxItem> list = new List<MultiLineTextBoxItem>();
			bool flag = string.IsNullOrEmpty(xml);
			IList<MultiLineTextBoxItem> result;
			if (flag)
			{
				result = list;
			}
			else
			{
				try
				{
					DataSet dataSet = new DataSet();
					dataSet.ReadXml(new StringReader(xml), XmlReadMode.ReadSchema);
					bool flag2 = dataSet.Tables.Count > 0;
					if (flag2)
					{
						DataTable dataTable = dataSet.Tables[0];
						foreach (object obj in dataTable.Rows)
						{
							DataRow dataRow = (DataRow)obj;
							try
							{
								string text = dataRow["text"].ToString();
								string whoEntered = dataRow["whoentered"].ToString();
								string s = dataRow["dateentered"].ToString();
								DateTime dateTime;
								try
								{
									dateTime = DateTime.Parse(s);
								}
								catch
								{
									dateTime = DateTime.MinValue;
								}
								list.Add(new MultiLineTextBoxItem
								{
									Text = text,
									WhoEntered = whoEntered,
									DateEntered = ((dateTime == DateTime.MinValue) ? null : new DateTime?(dateTime))
								});
							}
							catch
							{
							}
						}
					}
				}
				catch (Exception ex)
				{
				}
				result = list;
			}
			return result;
		}

		// Token: 0x0600301C RID: 12316 RVA: 0x0003CA18 File Offset: 0x0003AC18
		public static string GetCaptionForDisplay(this DynamicField Field)
		{
			bool flag = Field == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				result = Field.ControlCaption.GetCaptionForDisplay();
			}
			return result;
		}

		// Token: 0x0600301D RID: 12317 RVA: 0x0003CA48 File Offset: 0x0003AC48
		public static string GetCaptionForDisplay(this string caption)
		{
			string text = (caption ?? "").Trim().Replace(":", "");
			int num = text.IndexOf("__");
			bool flag = num >= 0;
			string result;
			if (flag)
			{
				result = ((num == 0) ? "" : text.Substring(0, num));
			}
			else
			{
				num = text.IndexOf("~~");
				bool flag2 = num >= 0;
				if (flag2)
				{
					result = ((num == 0) ? "" : text.Substring(0, num));
				}
				else
				{
					result = text;
				}
			}
			return result;
		}

		// Token: 0x0600301E RID: 12318 RVA: 0x0003CAD4 File Offset: 0x0003ACD4
		public static string GetStringWithCaption(this DynamicData Data)
		{
			bool flag = ((Data != null) ? Data.Field : null) == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				string captionForDisplay = Data.Field.GetCaptionForDisplay();
				string @string = Data.GetString();
				bool flag2 = @string.Equals(captionForDisplay);
				if (flag2)
				{
					result = @string;
				}
				else
				{
					string str = (captionForDisplay.Length < 1) ? "" : (captionForDisplay + ": ");
					result = str + @string;
				}
			}
			return result;
		}

		// Token: 0x0600301F RID: 12319 RVA: 0x0003CB4C File Offset: 0x0003AD4C
		public static DynamicFormTypeAttribute GetInfo(this eDynamicFormType FormType)
		{
			Type type = FormType.GetType();
			FieldInfo field = type.GetField(FormType.ToString());
			DynamicFormTypeAttribute[] array = field.GetCustomAttributes(typeof(DynamicFormTypeAttribute), false) as DynamicFormTypeAttribute[];
			return (array != null && array.Length != 0) ? array[0] : null;
		}

		// Token: 0x06003020 RID: 12320 RVA: 0x0003CBA4 File Offset: 0x0003ADA4
		public static DynamicControlAttribute GetAttribute(this eControlCode ControlCode)
		{
			Type type = ControlCode.GetType();
			FieldInfo field = type.GetField(ControlCode.ToString());
			DynamicControlAttribute[] array = field.GetCustomAttributes(typeof(DynamicControlAttribute), false) as DynamicControlAttribute[];
			return (array != null && array.Length != 0) ? array[0] : null;
		}

		// Token: 0x06003021 RID: 12321 RVA: 0x0003CBFC File Offset: 0x0003ADFC
		public static object GetValueForPresentation(this DynamicData DataItem)
		{
			Type type;
			if (DataItem == null)
			{
				type = null;
			}
			else
			{
				DynamicField field = DataItem.Field;
				if (field == null)
				{
					type = null;
				}
				else
				{
					DynamicControlAttribute attribute = field.ControlCode.GetAttribute<DynamicControlAttribute>();
					type = ((attribute != null) ? attribute.PresentationDataType : null);
				}
			}
			Type presentationDataType = type ?? typeof(string);
			return DataItem.GetValueForDataTable(presentationDataType);
		}

		// Token: 0x06003022 RID: 12322 RVA: 0x0003CC54 File Offset: 0x0003AE54
		public static object GetValueForDataTable(this DynamicData DataItem, Type PresentationDataType)
		{
			bool flag = DataItem == null;
			object result;
			if (flag)
			{
				result = null;
			}
			else
			{
				object value = DataItem.Value;
				object secondaryValue = DataItem.SecondaryValue;
				DynamicField field = DataItem.Field;
				eControlCode controlCode = (field != null) ? field.ControlCode : eControlCode.Unknown;
				DynamicField field2 = DataItem.Field;
				int setting = (field2 != null) ? field2.Setting3 : -1;
				DynamicField field3 = DataItem.Field;
				result = DynamicFormsAdapter.GetValueForDataTable(value, secondaryValue, controlCode, setting, ((field3 != null) ? field3.GetCaptionForDisplay() : null) ?? "", PresentationDataType);
			}
			return result;
		}

		// Token: 0x06003023 RID: 12323 RVA: 0x0003CCC4 File Offset: 0x0003AEC4
		public static object GetValueForPresentation(object dataItemValue, object secondaryValue, eControlCode controlCode, int setting3, string caption, Type PresentationDataType)
		{
			return DynamicFormsAdapter.GetValueForDataTable(dataItemValue, secondaryValue, controlCode, setting3, caption, PresentationDataType);
		}

		// Token: 0x06003024 RID: 12324 RVA: 0x0003CCE4 File Offset: 0x0003AEE4
		public static object GetValueForDataTable(object dataItemValue, object secondaryValue, eControlCode controlCode, int setting3, string caption, Type PresentationDataType)
		{
			bool flag = dataItemValue == null || dataItemValue == DBNull.Value;
			object result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = PresentationDataType == typeof(bool);
				if (flag2)
				{
					bool flag3 = dataItemValue is bool;
					if (flag3)
					{
						return dataItemValue;
					}
					string value = dataItemValue.ToString();
					bool flag5;
					bool flag4 = bool.TryParse(value, out flag5);
					if (flag4)
					{
						return flag5;
					}
				}
				else
				{
					bool flag6 = PresentationDataType == typeof(DateTime);
					if (flag6)
					{
						bool flag7 = dataItemValue is DateTime;
						if (flag7)
						{
							return dataItemValue;
						}
						string s = dataItemValue.ToString();
						DateTime dateTime;
						bool flag8 = DateTime.TryParse(s, out dateTime);
						if (flag8)
						{
							return dateTime;
						}
					}
					else
					{
						bool flag9 = PresentationDataType == typeof(byte[]);
						if (flag9)
						{
							bool flag10 = dataItemValue is byte[];
							if (flag10)
							{
								return dataItemValue;
							}
						}
					}
				}
				object secondaryValue2;
				DynamicFormsAdapter.GetValueAndSecondaryValue(dataItemValue, secondaryValue, controlCode, out secondaryValue2);
				result = DynamicFormsAdapter.GetString(controlCode, caption, secondaryValue2, dataItemValue, setting3);
			}
			return result;
		}

		// Token: 0x06003025 RID: 12325 RVA: 0x0003CDF8 File Offset: 0x0003AFF8
		public static string GetDescription(this int ControlCode)
		{
			bool flag = !Enum.IsDefined(typeof(eControlCode), ControlCode);
			string result;
			if (flag)
			{
				result = "?";
			}
			else
			{
				DynamicControlAttribute attribute = DynamicControlAttribute.GetAttribute((eControlCode)ControlCode);
				result = (string.IsNullOrEmpty((attribute != null) ? attribute.Description : null) ? "" : attribute.Description);
			}
			return result;
		}

		// Token: 0x06003026 RID: 12326 RVA: 0x0003CE58 File Offset: 0x0003B058
		public static IList<DynamicFormWithFields> ConvertFromXmlNew(string xml)
		{
			List<DynamicFormWithFields> list = new List<DynamicFormWithFields>();
			XDocument xdocument = XDocument.Parse(xml);
			XElement root = xdocument.Root;
			XElement xelement = (root != null) ? root.Descendants("screens").FirstOrDefault<XElement>() : null;
			bool flag = xelement == null;
			IList<DynamicFormWithFields> result;
			if (flag)
			{
				result = new List<DynamicFormWithFields>();
			}
			else
			{
				List<DynamicFormWithExtendedInfo> source = (from g in xelement.Elements("screen")
				select new DynamicFormWithExtendedInfo
				{
					ScreenNum = DynamicFormsAdapter.GetIntFromAttribute(g, "screennum", 0),
					Title = DynamicFormsAdapter.GetStringFromAttribute(g, "title", ""),
					SecondaryTitle = DynamicFormsAdapter.GetStringFromAttribute(g, "secondarytitle", ""),
					FormType = DynamicFormsAdapter.GetFormTypeFromAttribute(g, "formtype"),
					UniqueId = DynamicFormsAdapter.GetStringFromAttribute(g, "uniqueid", ""),
					IsEnabled = DynamicFormsAdapter.GetBoolFromAttribute(g, "isenabled", false),
					ShowAsButton = DynamicFormsAdapter.GetBoolFromAttribute(g, "showasbutton", false),
					ColumnWidthPercent = DynamicFormsAdapter.GetDoubleFromAttribute(g, "columnwidthpercent", 0.0),
					BottomLess = DynamicFormsAdapter.GetBoolFromAttribute(g, "bottomless", false),
					CSharp_FormLoad = DynamicFormsAdapter.GetStringFromAttribute(g, "csharp_formload", ""),
					CSharp_FormSave = DynamicFormsAdapter.GetStringFromAttribute(g, "csharp_formsave", ""),
					CSharp_Misc = DynamicFormsAdapter.GetStringFromAttribute(g, "csharp_misc", ""),
					GroupName = DynamicFormsAdapter.GetStringFromAttribute(g, "groupname", ""),
					LargeImageIndex = DynamicFormsAdapter.GetIntFromAttribute(g, "largeimageindex", 0),
					SmallImageIndex = DynamicFormsAdapter.GetIntFromAttribute(g, "smallimageindex", 0),
					VerticalControlPadding = DynamicFormsAdapter.GetIntFromAttribute(g, "verticalcontrolpadding", 0),
					ColumnPadding = DynamicFormsAdapter.GetIntFromAttribute(g, "columnpadding", 0),
					DateAdded = DynamicFormsAdapter.GetDateFromAttribute(g, "dateadded", DateTime.MinValue),
					DateModified = DynamicFormsAdapter.GetDateFromAttribute(g, "datemodified"),
					StudentNameNumEditable = DynamicFormsAdapter.GetBoolFromAttribute(g, "studentnamenumeditable", false),
					ScreenId = DynamicFormsAdapter.GetIntFromAttribute(g, "screenid", 0),
					FontName = DynamicFormsAdapter.GetStringFromAttribute(g, "fontname", ""),
					FontSize = DynamicFormsAdapter.GetIntFromAttribute(g, "fontsize", 0),
					GroupIds = DynamicFormsAdapter.GetIntListFromAttribute(g, "groupids"),
					IsWebScreen = DynamicFormsAdapter.GetBoolFromAttribute(g, "iswebscreen", false),
					ControlIdToActivate = DynamicFormsAdapter.GetIntFromAttribute(g, "controlidtoactivate", 0),
					StudentNumberCaption = DynamicFormsAdapter.GetStringFromAttribute(g, "studentnumbercaption", ""),
					StudentNumberAutoGenerateRule = DynamicFormsAdapter.GetStringFromAttribute(g, "studentnumberautogeneraterule", ""),
					StudentNameHidden = DynamicFormsAdapter.GetBoolFromAttribute(g, "studentnamehidden", false)
				}).ToList<DynamicFormWithExtendedInfo>();
				XElement xelement2 = xdocument.Root.Descendants("dynamiccontrols").FirstOrDefault<XElement>();
				bool flag2 = xelement2 != null;
				List<DynamicFieldOnForm> list2;
				if (flag2)
				{
					list2 = (from r in xelement2.Elements("dynamiccontrol")
					select new DynamicFieldOnForm
					{
						ControlId = DynamicFormsAdapter.GetIntFromAttribute(r, "controlid", 0),
						ControlCode = DynamicFormsAdapter.GetControlCodeFromAttribute(r, "controlcode"),
						ControlCaption = DynamicFormsAdapter.GetStringFromAttribute(r, "controlcaption", ""),
						ControlName = DynamicFormsAdapter.GetStringFromAttribute(r, "controlname", ""),
						DefaultValue = DynamicFormsAdapter.GetIntFromAttribute(r, "defaultvalue", 0),
						DefaultValueString = DynamicFormsAdapter.GetStringFromAttribute(r, "defaultvaluestring", ""),
						DontWrapToNextLine = DynamicFormsAdapter.GetBoolFromAttribute(r, "dontwraptonextline", false),
						EnforceMethod = DynamicFormsAdapter.GetEnforceFromAttribute(r, "enforcemethod"),
						HideCaption = DynamicFormsAdapter.GetBoolFromAttribute(r, "hidecaption", false),
						IsActive = DynamicFormsAdapter.GetBoolFromAttribute(r, "isactive", false),
						OrderNum = DynamicFormsAdapter.GetIntFromAttribute(r, "ordernum", 0),
						IsReadOnly = DynamicFormsAdapter.GetBoolFromAttribute(r, "isreadonly", false),
						Setting1 = DynamicFormsAdapter.GetIntFromAttribute(r, "setting1", 0),
						Setting2 = DynamicFormsAdapter.GetIntFromAttribute(r, "setting2", 0),
						Setting3 = DynamicFormsAdapter.GetIntFromAttribute(r, "setting3", 0),
						Setting4 = DynamicFormsAdapter.GetIntFromAttribute(r, "setting4", 0),
						Setting4String = DynamicFormsAdapter.GetStringFromAttribute(r, "setting4string", ""),
						Mask = DynamicFormsAdapter.GetStringFromAttribute(r, "mask", ""),
						OriginalCaption = DynamicFormsAdapter.GetStringFromAttribute(r, "originalcaption", ""),
						UniqueId = DynamicFormsAdapter.GetStringFromAttribute(r, "uniqueid", ""),
						ScreenNum = DynamicFormsAdapter.GetIntFromAttribute(r, "screennum", 0),
						Args = DynamicFormsAdapter.GetStringDictionaryFromAttribute(r, "args"),
						SpecialControlType = DynamicFormsAdapter.GetSpecialControlTypeEnumFromAttribute(r, "specialcontroltype")
					}).ToList<DynamicFieldOnForm>();
				}
				else
				{
					list2 = new List<DynamicFieldOnForm>();
				}
				list2.Sort((DynamicFieldOnForm g1, DynamicFieldOnForm g2) => g1.ScreenNum.CompareTo(g2.ScreenNum));
				int j;
				for (int i = 0; i < list2.Count; i = j)
				{
					DynamicFieldOnForm dynamicFieldOnForm = list2[i];
					int screenNum = dynamicFieldOnForm.ScreenNum;
					DynamicFormWithExtendedInfo form = source.FirstOrDefault((DynamicFormWithExtendedInfo g) => g.ScreenNum == screenNum);
					DynamicFormWithFields dynamicFormWithFields = new DynamicFormWithFields();
					dynamicFormWithFields.Form = form;
					dynamicFormWithFields.Fields = new List<DynamicFieldOnForm>();
					list.Add(dynamicFormWithFields);
					for (j = i; j < list2.Count; j++)
					{
						DynamicFieldOnForm dynamicFieldOnForm2 = list2[j];
						bool flag3 = dynamicFieldOnForm2.ScreenNum != screenNum;
						if (flag3)
						{
							break;
						}
						dynamicFormWithFields.Fields.Add(dynamicFieldOnForm2);
					}
					List<DynamicFieldOnForm> list3 = dynamicFormWithFields.Fields.ToList<DynamicFieldOnForm>();
					list3.Sort((DynamicFieldOnForm g1, DynamicFieldOnForm g2) => g1.OrderNum.CompareTo(g2.OrderNum));
					dynamicFormWithFields.Fields = list3;
				}
				result = list;
			}
			return result;
		}

		// Token: 0x06003027 RID: 12327 RVA: 0x0003D094 File Offset: 0x0003B294
		public static string ConvertDictionaryItemToString(KeyValuePair<string, string> item)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(item.Key ?? "");
			byte[] bytes2 = Encoding.UTF8.GetBytes(item.Value ?? "");
			string str = Convert.ToBase64String(bytes);
			string str2 = Convert.ToBase64String(bytes2);
			return str + "=" + str2;
		}

		// Token: 0x06003028 RID: 12328 RVA: 0x0003D0F8 File Offset: 0x0003B2F8
		private static string ConvertArgsToString(Dictionary<string, string> args)
		{
			bool flag = args == null || args.Count < 1;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				result = string.Join(",", args.ToList<KeyValuePair<string, string>>().ConvertAll<string>((KeyValuePair<string, string> g) => DynamicFormsAdapter.ConvertDictionaryItemToString(g)).ToArray());
			}
			return result;
		}

		// Token: 0x06003029 RID: 12329 RVA: 0x0003D160 File Offset: 0x0003B360
		private static Dictionary<string, string> GetStringDictionaryFromAttribute(XElement element, string attributeName)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			bool flag = element == null;
			Dictionary<string, string> result;
			if (flag)
			{
				result = dictionary;
			}
			else
			{
				XAttribute xattribute = element.Attribute(attributeName);
				bool flag2 = xattribute == null || string.IsNullOrEmpty(xattribute.Value);
				if (flag2)
				{
					result = dictionary;
				}
				else
				{
					string[] array = xattribute.Value.Split(new char[]
					{
						','
					});
					foreach (string text in array)
					{
						string text2 = text.Trim();
						bool flag3 = text.Length > 0;
						if (flag3)
						{
							int num = text.IndexOf('=');
							bool flag4 = num > 0;
							if (flag4)
							{
								string text3 = text.Substring(0, num);
								string text4 = text.Substring(num + 1);
								byte[] bytes = Convert.FromBase64String(text3 ?? "");
								byte[] bytes2 = Convert.FromBase64String(text4 ?? "");
								string @string = Encoding.UTF8.GetString(bytes);
								string string2 = Encoding.UTF8.GetString(bytes2);
								bool flag5 = !dictionary.ContainsKey(@string);
								if (flag5)
								{
									dictionary.Add(@string, string2);
								}
							}
						}
					}
					result = dictionary;
				}
			}
			return result;
		}

		// Token: 0x0600302A RID: 12330 RVA: 0x0003D2A0 File Offset: 0x0003B4A0
		private static string GetStringFromAttribute(XElement element, string attributeName, string defaultValue = "")
		{
			bool flag = element == null;
			string result;
			if (flag)
			{
				result = defaultValue;
			}
			else
			{
				XAttribute xattribute = element.Attribute(attributeName);
				bool flag2 = xattribute == null;
				if (flag2)
				{
					result = defaultValue;
				}
				else
				{
					result = (xattribute.Value ?? defaultValue);
				}
			}
			return result;
		}

		// Token: 0x0600302B RID: 12331 RVA: 0x0003D2E4 File Offset: 0x0003B4E4
		private static bool GetBoolFromAttribute(XElement element, string attributeName, bool defaultValue = false)
		{
			bool flag = element == null;
			bool result;
			if (flag)
			{
				result = defaultValue;
			}
			else
			{
				XAttribute xattribute = element.Attribute(attributeName);
				bool flag2 = xattribute == null || string.IsNullOrEmpty(xattribute.Value);
				if (flag2)
				{
					result = defaultValue;
				}
				else
				{
					bool flag4;
					bool flag3 = !bool.TryParse(xattribute.Value, out flag4);
					if (flag3)
					{
						result = defaultValue;
					}
					else
					{
						result = flag4;
					}
				}
			}
			return result;
		}

		// Token: 0x0600302C RID: 12332 RVA: 0x0003D348 File Offset: 0x0003B548
		private static eSpecialControlType GetSpecialControlTypeEnumFromAttribute(XElement element, string attributeName)
		{
			XAttribute xattribute = (element != null) ? element.Attribute(attributeName) : null;
			bool flag = string.IsNullOrEmpty((xattribute != null) ? xattribute.Value : null);
			eSpecialControlType result;
			if (flag)
			{
				result = eSpecialControlType.Unknown;
			}
			else
			{
				int num;
				bool flag2 = !int.TryParse(xattribute.Value, out num);
				if (flag2)
				{
					result = eSpecialControlType.Unknown;
				}
				else
				{
					result = (eSpecialControlType)(Enum.IsDefined(typeof(eSpecialControlType), num) ? num : 0);
				}
			}
			return result;
		}

		// Token: 0x0600302D RID: 12333 RVA: 0x0003D3BC File Offset: 0x0003B5BC
		private static int GetIntFromAttribute(XElement element, string attributeName, int defaultValue = 0)
		{
			bool flag = element == null;
			int result;
			if (flag)
			{
				result = defaultValue;
			}
			else
			{
				XAttribute xattribute = element.Attribute(attributeName);
				bool flag2 = string.IsNullOrEmpty((xattribute != null) ? xattribute.Value : null);
				if (flag2)
				{
					result = defaultValue;
				}
				else
				{
					int num;
					result = ((!int.TryParse(xattribute.Value, out num)) ? defaultValue : num);
				}
			}
			return result;
		}

		// Token: 0x0600302E RID: 12334 RVA: 0x0003D418 File Offset: 0x0003B618
		private static DateTime GetDateFromAttribute(XElement element, string attributeName, DateTime defaultValue)
		{
			return DynamicFormsAdapter.GetDateFromAttribute(element, attributeName) ?? defaultValue;
		}

		// Token: 0x0600302F RID: 12335 RVA: 0x0003D448 File Offset: 0x0003B648
		private static DateTime? GetDateFromAttribute(XElement element, string attributeName)
		{
			DateTime? dateTime = null;
			bool flag = element == null;
			DateTime? result;
			if (flag)
			{
				result = dateTime;
			}
			else
			{
				XAttribute xattribute = element.Attribute(attributeName);
				bool flag2 = string.IsNullOrEmpty((xattribute != null) ? xattribute.Value : null);
				if (flag2)
				{
					result = dateTime;
				}
				else
				{
					DateTime value;
					result = ((!DateTime.TryParse(xattribute.Value, out value)) ? dateTime : new DateTime?(value));
				}
			}
			return result;
		}

		// Token: 0x06003030 RID: 12336 RVA: 0x0003D4B4 File Offset: 0x0003B6B4
		private static double GetDoubleFromAttribute(XElement element, string attributeName, double defaultValue = 0.0)
		{
			bool flag = element == null;
			double result;
			if (flag)
			{
				result = defaultValue;
			}
			else
			{
				XAttribute xattribute = element.Attribute(attributeName);
				bool flag2 = string.IsNullOrEmpty((xattribute != null) ? xattribute.Value : null);
				if (flag2)
				{
					result = defaultValue;
				}
				else
				{
					double num;
					result = ((!double.TryParse(xattribute.Value, out num)) ? defaultValue : num);
				}
			}
			return result;
		}

		// Token: 0x06003031 RID: 12337 RVA: 0x0003D510 File Offset: 0x0003B710
		private static IList<int> GetIntListFromAttribute(XElement element, string attributeName)
		{
			List<int> list = new List<int>();
			bool flag = element == null;
			IList<int> result;
			if (flag)
			{
				result = list;
			}
			else
			{
				XAttribute xattribute = element.Attribute(attributeName);
				bool flag2 = string.IsNullOrEmpty((xattribute != null) ? xattribute.Value : null);
				if (flag2)
				{
					result = list;
				}
				else
				{
					string[] array = xattribute.Value.Split(new char[]
					{
						','
					});
					foreach (string text in array)
					{
						int item;
						bool flag3 = int.TryParse(text.Trim(), out item) && !list.Contains(item);
						if (flag3)
						{
							list.Add(item);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x06003032 RID: 12338 RVA: 0x0003D5C8 File Offset: 0x0003B7C8
		private static eEnforceType GetEnforceFromAttribute(XElement element, string attributeName)
		{
			eEnforceType eEnforceType = eEnforceType.Optional;
			bool flag = element == null;
			eEnforceType result;
			if (flag)
			{
				result = eEnforceType;
			}
			else
			{
				XAttribute xattribute = element.Attribute(attributeName);
				bool flag2 = xattribute == null || string.IsNullOrEmpty(xattribute.Value);
				if (flag2)
				{
					result = eEnforceType;
				}
				else
				{
					int num;
					bool flag3 = !int.TryParse(xattribute.Value, out num);
					if (flag3)
					{
						result = eEnforceType;
					}
					else
					{
						bool flag4 = !Enum.IsDefined(typeof(eEnforceType), num);
						if (flag4)
						{
							result = eEnforceType;
						}
						else
						{
							result = (eEnforceType)num;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06003033 RID: 12339 RVA: 0x0003D654 File Offset: 0x0003B854
		private static eControlCode GetControlCodeFromAttribute(XElement element, string attributeName)
		{
			eControlCode eControlCode = eControlCode.Unknown;
			bool flag = element == null;
			eControlCode result;
			if (flag)
			{
				result = eControlCode;
			}
			else
			{
				XAttribute xattribute = element.Attribute(attributeName);
				bool flag2 = xattribute == null || string.IsNullOrEmpty(xattribute.Value);
				if (flag2)
				{
					result = eControlCode;
				}
				else
				{
					int num;
					bool flag3 = !int.TryParse(xattribute.Value, out num);
					if (flag3)
					{
						result = eControlCode;
					}
					else
					{
						bool flag4 = !Enum.IsDefined(typeof(eControlCode), num);
						if (flag4)
						{
							result = eControlCode;
						}
						else
						{
							result = (eControlCode)num;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06003034 RID: 12340 RVA: 0x0003D6E0 File Offset: 0x0003B8E0
		private static eDynamicFormType GetFormTypeFromAttribute(XElement element, string attributeName)
		{
			eDynamicFormType eDynamicFormType = eDynamicFormType.PerStudent;
			bool flag = element == null;
			eDynamicFormType result;
			if (flag)
			{
				result = eDynamicFormType;
			}
			else
			{
				XAttribute xattribute = element.Attribute(attributeName);
				bool flag2 = xattribute == null || string.IsNullOrEmpty(xattribute.Value);
				if (flag2)
				{
					result = eDynamicFormType;
				}
				else
				{
					int num;
					bool flag3 = !int.TryParse(xattribute.Value, out num);
					if (flag3)
					{
						result = eDynamicFormType;
					}
					else
					{
						bool flag4 = !Enum.IsDefined(typeof(eDynamicFormType), num);
						if (flag4)
						{
							result = eDynamicFormType;
						}
						else
						{
							result = (eDynamicFormType)num;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06003035 RID: 12341 RVA: 0x0003D76C File Offset: 0x0003B96C
		public static string ConvertToXmlNew(IList<DynamicFormWithExtendedInfo> Forms, IList<DynamicFieldOnForm> Fields)
		{
			XElement xelement = DynamicFormsAdapter.ConvertToXmlElementNew(Forms, Fields);
			XDocument xdocument = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), new object[]
			{
				xelement
			});
			return xdocument.Declaration.ToString() + xdocument.ToString();
		}

		// Token: 0x06003036 RID: 12342 RVA: 0x0003D7C0 File Offset: 0x0003B9C0
		public static XElement ConvertToXmlElementNew(IList<DynamicFormWithExtendedInfo> Forms, IList<DynamicFieldOnForm> Fields)
		{
			XName name = "dynamicformwithcontrolspackage";
			object[] array = new object[2];
			array[0] = new XElement("dynamiccontrols", from r in Fields
			select new XElement("dynamiccontrol", new object[]
			{
				new XAttribute("controlid", r.ControlId),
				new XAttribute("controlcode", (int)r.ControlCode),
				new XAttribute("controlcaption", r.ControlCaption ?? ""),
				new XAttribute("controlname", r.ControlName ?? ""),
				new XAttribute("defaultvalue", r.DefaultValue),
				new XAttribute("defaultvaluestring", r.DefaultValueString ?? ""),
				new XAttribute("dontwraptonextline", r.DontWrapToNextLine),
				new XAttribute("enforcemethod", (int)r.EnforceMethod),
				new XAttribute("hidecaption", r.HideCaption),
				new XAttribute("isactive", r.IsActive),
				new XAttribute("ordernum", r.OrderNum),
				new XAttribute("isreadonly", r.IsReadOnly),
				new XAttribute("setting1", r.Setting1),
				new XAttribute("setting2", r.Setting2),
				new XAttribute("setting3", r.Setting3),
				new XAttribute("setting4", r.Setting4),
				new XAttribute("setting4string", r.Setting4String ?? ""),
				new XAttribute("mask", r.Mask ?? ""),
				new XAttribute("originalcaption", r.OriginalCaption ?? ""),
				new XAttribute("uniqueid", r.UniqueId ?? ""),
				new XAttribute("screennum", r.ScreenNum),
				new XAttribute("args", DynamicFormsAdapter.ConvertArgsToString(r.Args)),
				new XAttribute("specialcontroltype", ((int)r.SpecialControlType).ToString())
			}));
			array[1] = new XElement("screens", Forms.Select(delegate(DynamicFormWithExtendedInfo g)
			{
				XName name2 = "screen";
				object[] array2 = new object[29];
				array2[0] = new XAttribute("screennum", g.ScreenNum);
				array2[1] = new XAttribute("title", g.Title ?? "");
				array2[2] = new XAttribute("secondarytitle", g.SecondaryTitle ?? "");
				array2[3] = new XAttribute("formtype", (int)g.FormType);
				array2[4] = new XAttribute("uniqueid", g.UniqueId ?? "");
				array2[5] = new XAttribute("isenabled", g.IsEnabled);
				array2[6] = new XAttribute("showasbutton", g.ShowAsButton);
				array2[7] = new XAttribute("columnwidthpercent", g.ColumnWidthPercent);
				array2[8] = new XAttribute("bottomless", g.BottomLess);
				array2[9] = new XAttribute("csharp_formload", g.CSharp_FormLoad ?? "");
				array2[10] = new XAttribute("csharp_formsave", g.CSharp_FormSave ?? "");
				array2[11] = new XAttribute("csharp_misc", g.CSharp_Misc ?? "");
				array2[12] = new XAttribute("groupname", g.GroupName ?? "");
				array2[13] = new XAttribute("largeimageindex", g.LargeImageIndex);
				array2[14] = new XAttribute("smallimageindex", g.SmallImageIndex);
				array2[15] = new XAttribute("verticalcontrolpadding", g.VerticalControlPadding);
				array2[16] = new XAttribute("columnpadding", g.ColumnPadding);
				array2[17] = new XAttribute("dateadded", g.DateAdded);
				array2[18] = new XAttribute("datemodified", (g.DateModified != null) ? g.DateModified.Value.ToString("yyyy-MM-dd") : "");
				array2[19] = new XAttribute("studentnamenumeditable", g.StudentNameNumEditable);
				array2[20] = new XAttribute("screenid", g.ScreenId);
				array2[21] = new XAttribute("fontname", g.FontName ?? "");
				array2[22] = new XAttribute("fontsize", g.FontSize);
				int num = 23;
				XName name3 = "groupids";
				object value;
				if (g.GroupIds != null)
				{
					value = string.Join(",", g.GroupIds.ToList<int>().ConvertAll<string>((int h) => h.ToString()).ToArray());
				}
				else
				{
					value = "";
				}
				array2[num] = new XAttribute(name3, value);
				array2[24] = new XAttribute("iswebscreen", g.IsWebScreen);
				array2[25] = new XAttribute("controlidtoactivate", g.ControlIdToActivate);
				array2[26] = new XAttribute("studentnumbercaption", g.StudentNumberCaption ?? "");
				array2[27] = new XAttribute("studentnumberautogeneraterule", g.StudentNumberAutoGenerateRule ?? "");
				array2[28] = new XAttribute("studentnamehidden", g.StudentNameHidden);
				return new XElement(name2, array2);
			}));
			return new XElement(name, array);
		}

		// Token: 0x06003037 RID: 12343 RVA: 0x0003D858 File Offset: 0x0003BA58
		public static string ConvertListViewDataTableToEncodedData(this DataTable t)
		{
			List<string[]> source = (from DataRow dr in t.Rows
			select (from g in dr.ItemArray
			select (g is DBNull) ? "" : g.ToString()).ToArray<string>()).ToList<string[]>();
			return string.Join('\t'.ToString(), (from g in source
			select string.Join('\0'.ToString(), g)).ToArray<string>());
		}

		// Token: 0x06003038 RID: 12344 RVA: 0x0003D8D8 File Offset: 0x0003BAD8
		public static DataTable ConvertListViewDataToDataTable(this string dataEncoded, IDictionary<string, Type> columns = null)
		{
			DataTable dataTable = new DataTable("t");
			bool flag = columns != null;
			if (flag)
			{
				foreach (KeyValuePair<string, Type> keyValuePair in columns)
				{
					dataTable.Columns.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
			bool flag2 = string.IsNullOrEmpty(dataEncoded);
			DataTable result;
			if (flag2)
			{
				result = dataTable;
			}
			else
			{
				string[] array = dataEncoded.Split(new char[]
				{
					'\t'
				}, StringSplitOptions.RemoveEmptyEntries);
				foreach (string text in array)
				{
					string[] array3 = text.Split(new char[1]);
					bool flag3 = columns == null && array3.Length > dataTable.Columns.Count;
					if (flag3)
					{
						for (int j = dataTable.Columns.Count; j < array3.Length; j++)
						{
							dataTable.Columns.Add("Column_" + (j + 1).ToString());
						}
					}
					object[] array4 = new object[dataTable.Columns.Count];
					for (int k = 0; k < array4.Length; k++)
					{
						Type dataType = dataTable.Columns[k].DataType;
						string s = (k < array3.Length) ? array3[k] : null;
						array4[k] = DynamicFormsAdapter.GetObjectForDataRow(dataType, s);
					}
					dataTable.Rows.Add(array4);
				}
				result = dataTable;
			}
			return result;
		}

		// Token: 0x06003039 RID: 12345 RVA: 0x0003DA80 File Offset: 0x0003BC80
		private static object GetObjectForDataRow(Type columnType, string s)
		{
			object value = DBNull.Value;
			bool flag = s == null;
			object result;
			if (flag)
			{
				result = value;
			}
			else
			{
				bool flag2 = columnType == typeof(string);
				if (flag2)
				{
					result = s;
				}
				else
				{
					bool flag3 = s.Length < 1;
					if (flag3)
					{
						result = value;
					}
					else
					{
						bool flag4 = columnType == typeof(DateTime);
						if (flag4)
						{
							DateTime dateTime;
							result = ((!DateTime.TryParse(s, out dateTime)) ? value : dateTime);
						}
						else
						{
							bool flag5 = columnType == typeof(int);
							if (flag5)
							{
								int num;
								result = ((!int.TryParse(s, out num)) ? value : num);
							}
							else
							{
								bool flag6 = columnType == typeof(double);
								if (flag6)
								{
									double num2;
									result = ((!double.TryParse(s, out num2)) ? value : num2);
								}
								else
								{
									bool flag7 = columnType == typeof(bool);
									if (flag7)
									{
										bool flag8;
										result = ((!bool.TryParse(s, out flag8)) ? value : flag8);
									}
									else
									{
										result = value;
									}
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x040020C6 RID: 8390
		private const char rowDel = '\t';

		// Token: 0x040020C7 RID: 8391
		private const char colDel = '\0';
	}
}
