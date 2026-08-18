using System;
using System.Web;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.TextFormat.Adapters;

namespace TechnoPro.ClockWorkWeb.user.TutoringStudents
{
	// Token: 0x0200004F RID: 79
	public class DynamicDataItemWrapper
	{
		// Token: 0x060001E6 RID: 486 RVA: 0x0000AF9E File Offset: 0x0000919E
		public DynamicDataItemWrapper()
		{
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000C720 File Offset: 0x0000A920
		public DynamicDataItemWrapper(DynamicDataDTO dataItem)
		{
			DynamicFieldDTO field = dataItem.Field;
			this.ControlId = field.ControlId;
			this.ControlCaption = field.GetDescription();
			this.ControlCode = DynamicDataItemWrapper.GetWrapperFromControlCode(field.ControlCode);
			this.Value = DynamicDataItemWrapper.GetDataStringValueEscaped(this.ControlCode, dataItem);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000C77C File Offset: 0x0000A97C
		private static string GetDataStringValueEscaped(eDynamicDataControlCodeWrapper controlCode, DynamicDataDTO dataItem)
		{
			string s = DynamicDataItemWrapper.GetDataStringValueUnescaped(controlCode, dataItem) ?? "";
			return HttpUtility.HtmlEncode(s);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000C7A8 File Offset: 0x0000A9A8
		private static string GetDataStringValueUnescaped(eDynamicDataControlCodeWrapper controlCode, DynamicDataDTO dataItem)
		{
			bool flag = dataItem == null;
			string result;
			if (flag)
			{
				result = string.Empty;
			}
			else
			{
				switch (controlCode)
				{
				case eDynamicDataControlCodeWrapper.Checkbox:
					result = ((dataItem.Value != null && ((dataItem.Value is bool && (bool)dataItem.Value) || (dataItem.Value is int && (int)dataItem.Value != 0) || "1yestrue".IndexOf(dataItem.Value.ToString()) >= 0)) ? "1" : "0");
					break;
				case eDynamicDataControlCodeWrapper.Textbox:
				{
					string text = dataItem.Value.ToString();
					bool flag2 = dataItem.Field.ControlCode == eControlCode.RtfTextBox;
					if (flag2)
					{
						text = text.ConvertRtfToPlainText();
					}
					result = text;
					break;
				}
				case eDynamicDataControlCodeWrapper.Droplist:
				{
					object secondaryValue = dataItem.SecondaryValue;
					string text2 = ((secondaryValue != null) ? secondaryValue.ToString() : null) ?? "";
					string text3;
					if (text2.Length >= 1)
					{
						text3 = text2;
					}
					else
					{
						object value = dataItem.Value;
						text3 = (((value != null) ? value.ToString() : null) ?? "");
					}
					result = text3;
					break;
				}
				case eDynamicDataControlCodeWrapper.Radiogroup:
				{
					object secondaryValue2 = dataItem.SecondaryValue;
					result = (((secondaryValue2 != null) ? secondaryValue2.ToString() : null) ?? "");
					break;
				}
				case eDynamicDataControlCodeWrapper.Datepicker:
				{
					string text4;
					if (!(dataItem.Value is DateTime))
					{
						object value2 = dataItem.Value;
						text4 = ((value2 != null) ? value2.ToString() : null);
					}
					else
					{
						text4 = ((DateTime)dataItem.Value).ToString("yyyy-MM-dd");
					}
					result = text4;
					break;
				}
				default:
				{
					object value3 = dataItem.Value;
					result = (((value3 != null) ? value3.ToString() : null) ?? "");
					break;
				}
				}
			}
			return result;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000C95C File Offset: 0x0000AB5C
		private static eDynamicDataControlCodeWrapper GetWrapperFromControlCode(eControlCode code)
		{
			if (code <= eControlCode.StaffComboBox)
			{
				switch (code)
				{
				case eControlCode.TextBox:
					goto IL_8A;
				case eControlCode.CheckBox:
					break;
				case eControlCode.DropList:
					goto IL_86;
				case eControlCode.RadioButton:
				case eControlCode.Label:
					goto IL_92;
				case eControlCode.Date:
					goto IL_82;
				default:
					switch (code)
					{
					case eControlCode.MyTextBox:
						goto IL_8A;
					case eControlCode.MyCheckBox:
						break;
					case eControlCode.Indent:
						goto IL_92;
					case eControlCode.RadioGroup:
						return eDynamicDataControlCodeWrapper.Radiogroup;
					default:
						if (code != eControlCode.StaffComboBox)
						{
							goto IL_92;
						}
						goto IL_86;
					}
					break;
				}
			}
			else
			{
				if (code == eControlCode.MaskedTextBox || code == eControlCode.RtfTextBox)
				{
					goto IL_8A;
				}
				switch (code)
				{
				case eControlCode.AccommodationCheckBox:
					break;
				case eControlCode.AccommodationTextBox:
					goto IL_8A;
				case eControlCode.AccommodationDatePicker:
					goto IL_82;
				case eControlCode.AccommodationDropList:
					goto IL_86;
				default:
					goto IL_92;
				}
			}
			return eDynamicDataControlCodeWrapper.Checkbox;
			IL_82:
			return eDynamicDataControlCodeWrapper.Datepicker;
			IL_86:
			return eDynamicDataControlCodeWrapper.Droplist;
			IL_8A:
			return eDynamicDataControlCodeWrapper.Textbox;
			IL_92:
			return eDynamicDataControlCodeWrapper.Unknown;
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001EB RID: 491 RVA: 0x0000CA00 File Offset: 0x0000AC00
		// (set) Token: 0x060001EC RID: 492 RVA: 0x0000CA08 File Offset: 0x0000AC08
		public int ControlId { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001ED RID: 493 RVA: 0x0000CA11 File Offset: 0x0000AC11
		// (set) Token: 0x060001EE RID: 494 RVA: 0x0000CA19 File Offset: 0x0000AC19
		public string ControlCaption { get; set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001EF RID: 495 RVA: 0x0000CA22 File Offset: 0x0000AC22
		// (set) Token: 0x060001F0 RID: 496 RVA: 0x0000CA2A File Offset: 0x0000AC2A
		public eDynamicDataControlCodeWrapper ControlCode { get; set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x0000CA33 File Offset: 0x0000AC33
		// (set) Token: 0x060001F2 RID: 498 RVA: 0x0000CA3B File Offset: 0x0000AC3B
		public string Value { get; set; }
	}
}
