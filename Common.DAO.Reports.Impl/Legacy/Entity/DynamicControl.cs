using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity
{
	// Token: 0x02000016 RID: 22
	public class DynamicControl
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00025A55 File Offset: 0x00023C55
		// (set) Token: 0x06000180 RID: 384 RVA: 0x00025A5D File Offset: 0x00023C5D
		public string UniqueId { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00025A68 File Offset: 0x00023C68
		// (set) Token: 0x06000182 RID: 386 RVA: 0x00025A80 File Offset: 0x00023C80
		public int SpecialControlType
		{
			get
			{
				return this._specialControlType;
			}
			set
			{
				this._specialControlType = value;
				this.SetModified();
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00025A94 File Offset: 0x00023C94
		// (set) Token: 0x06000184 RID: 388 RVA: 0x00025AAC File Offset: 0x00023CAC
		public int ShowOnLetter
		{
			get
			{
				return this.showOnLetter;
			}
			set
			{
				this.showOnLetter = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00025AB8 File Offset: 0x00023CB8
		public bool IsAccommodationControl
		{
			get
			{
				return this.accommodationId > 0;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00025AD4 File Offset: 0x00023CD4
		// (set) Token: 0x06000187 RID: 391 RVA: 0x00025AEC File Offset: 0x00023CEC
		public object Tag
		{
			get
			{
				return this.tag;
			}
			set
			{
				this.tag = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00025AF8 File Offset: 0x00023CF8
		// (set) Token: 0x06000189 RID: 393 RVA: 0x00025B10 File Offset: 0x00023D10
		public DynamicControl AssociatedDynamicControl
		{
			get
			{
				return this.associatedDynamicControl;
			}
			set
			{
				this.associatedDynamicControl = value;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00025B1C File Offset: 0x00023D1C
		// (set) Token: 0x0600018B RID: 395 RVA: 0x00025B34 File Offset: 0x00023D34
		public ModificationType HowModified
		{
			get
			{
				return this.howModified;
			}
			set
			{
				this.howModified = value;
			}
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00025B3E File Offset: 0x00023D3E
		public void SetControlCode(int newControlCode)
		{
			this.control_code = newControlCode;
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00025B48 File Offset: 0x00023D48
		public bool HasSpecialInstructions
		{
			get
			{
				return this.controlGroup.Length > 0;
			}
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00025B68 File Offset: 0x00023D68
		public string SpecialInstructionsNoNull(string key)
		{
			string text = this.SpecialInstructions(key);
			return (text == null) ? "" : text;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00025B90 File Offset: 0x00023D90
		public string SpecialInstructions(string key)
		{
			bool flag = this.specialInstructionArgs != null;
			string result;
			if (flag)
			{
				result = this.specialInstructionArgs[key];
			}
			else
			{
				bool flag2 = this.controlGroup != null && this.controlGroup.Length > 0;
				if (flag2)
				{
					this.specialInstructionArgs = DynamicControl.ParseArgs(this.controlGroup);
					result = this.specialInstructionArgs[key];
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00025C00 File Offset: 0x00023E00
		public void RemoveSpecialInstruction(string key)
		{
			bool flag = this.specialInstructionArgs != null && this.specialInstructionArgs.ContainsKey(key);
			if (flag)
			{
				this.specialInstructionArgs.Remove(key);
			}
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00025C38 File Offset: 0x00023E38
		public void SetSpecialInstruction(string key, string val)
		{
			bool flag = this.specialInstructionArgs == null;
			if (flag)
			{
				this.specialInstructionArgs = new StringDictionary();
			}
			bool flag2 = this.specialInstructionArgs.ContainsKey(key);
			if (flag2)
			{
				this.specialInstructionArgs.Remove(key);
			}
			this.specialInstructionArgs.Add(key, val);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00025C90 File Offset: 0x00023E90
		public static StringDictionary ParseArgs(string args)
		{
			StringDictionary stringDictionary = new StringDictionary();
			string[] array = DynamicControl.SplitStringIntoNEWLINE_delimitered_parts(args, true);
			foreach (string text in array)
			{
				int num = text.IndexOf('=');
				bool flag = num > 0;
				if (flag)
				{
					int num2 = num + 1;
					stringDictionary.Add(text.Substring(0, num), (text.Length > num2) ? text.Substring(num + 1) : "");
				}
				else
				{
					stringDictionary.Add(text, "");
				}
			}
			return stringDictionary;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00025D2C File Offset: 0x00023F2C
		public static string[] SplitStringIntoNEWLINE_delimitered_parts(string s, bool excludeEmptyStrings)
		{
			string[] array = s.Split(Environment.NewLine.ToCharArray());
			if (excludeEmptyStrings)
			{
				ArrayList arrayList = new ArrayList();
				foreach (string text in array)
				{
					bool flag = text.Trim().Length > 0;
					if (flag)
					{
						arrayList.Add(text);
					}
				}
				array = new string[arrayList.Count];
				for (int j = 0; j < arrayList.Count; j++)
				{
					array[j] = (string)arrayList[j];
				}
			}
			return array;
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00025DD4 File Offset: 0x00023FD4
		// (set) Token: 0x06000195 RID: 405 RVA: 0x00025DEC File Offset: 0x00023FEC
		public int ControlId
		{
			get
			{
				return this.control_id;
			}
			set
			{
				this.control_id = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00025DF8 File Offset: 0x00023FF8
		// (set) Token: 0x06000197 RID: 407 RVA: 0x00025E10 File Offset: 0x00024010
		public string ControlCaption
		{
			get
			{
				return this.control_caption;
			}
			set
			{
				this.control_caption = value;
				this.SetModified();
			}
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00025E24 File Offset: 0x00024024
		private void SetModified()
		{
			bool flag = this.howModified == ModificationType.Unchanged;
			if (flag)
			{
				this.howModified = ModificationType.Modified;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00025E48 File Offset: 0x00024048
		public string FrenchControlCaptionForDisplay
		{
			get
			{
				int num = this.setting4String.IndexOf("__");
				bool flag = num >= 0;
				string result;
				if (flag)
				{
					result = ((num == 0) ? "" : this.setting4String.Substring(0, num));
				}
				else
				{
					num = this.setting4String.IndexOf("~~");
					bool flag2 = num >= 0;
					if (flag2)
					{
						result = ((num == 0) ? "" : this.setting4String.Substring(0, num));
					}
					else
					{
						result = this.setting4String;
					}
				}
				return result;
			}
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00025ED0 File Offset: 0x000240D0
		public static string GetControlCaptionForDisplay(string control_caption)
		{
			int num = control_caption.IndexOf("__");
			bool flag = num >= 0;
			string result;
			if (flag)
			{
				result = ((num == 0) ? "" : control_caption.Substring(0, num));
			}
			else
			{
				num = control_caption.IndexOf("~~");
				bool flag2 = num >= 0;
				if (flag2)
				{
					result = ((num == 0) ? "" : control_caption.Substring(0, num));
				}
				else
				{
					result = control_caption;
				}
			}
			return result;
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00025F3C File Offset: 0x0002413C
		public bool IsValueEncrypted
		{
			get
			{
				bool flag = this.control_code == 1 || this.control_code == 701;
				bool result;
				if (flag)
				{
					result = (this.setting3 == 1);
				}
				else
				{
					bool flag2 = this.control_code == 3 || this.control_code == 703;
					result = (flag2 && this.setting3 == -1);
				}
				return result;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00025FA4 File Offset: 0x000241A4
		public string ControlCaptionAsColumnName
		{
			get
			{
				return Regex.Replace(this.control_caption, "[^0-9a-zA-Z\\._]", string.Empty);
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00025FCC File Offset: 0x000241CC
		public string ControlCaptionForDisplay
		{
			get
			{
				return DynamicControl.GetControlCaptionForDisplay(this.control_caption);
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00025FEC File Offset: 0x000241EC
		public int ControlCode
		{
			get
			{
				return this.control_code;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00026004 File Offset: 0x00024204
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x0002601C File Offset: 0x0002421C
		public int Setting1
		{
			get
			{
				return this.setting1;
			}
			set
			{
				this.setting1 = value;
				this.SetModified();
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00026030 File Offset: 0x00024230
		// (set) Token: 0x060001A2 RID: 418 RVA: 0x00026048 File Offset: 0x00024248
		public int Setting2
		{
			get
			{
				return this.setting2;
			}
			set
			{
				this.setting2 = value;
				this.SetModified();
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x0002605C File Offset: 0x0002425C
		// (set) Token: 0x060001A4 RID: 420 RVA: 0x00026074 File Offset: 0x00024274
		public int Setting3
		{
			get
			{
				return this.setting3;
			}
			set
			{
				this.setting3 = value;
				this.SetModified();
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00026088 File Offset: 0x00024288
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x000260A0 File Offset: 0x000242A0
		public int DefaultValue
		{
			get
			{
				return this.default_value;
			}
			set
			{
				this.default_value = value;
				this.SetModified();
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x000260B4 File Offset: 0x000242B4
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x000260CC File Offset: 0x000242CC
		public string ControlName
		{
			get
			{
				return this.controlName;
			}
			set
			{
				this.controlName = value;
				this.SetModified();
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x000260E0 File Offset: 0x000242E0
		// (set) Token: 0x060001AA RID: 426 RVA: 0x000260F8 File Offset: 0x000242F8
		public string ControlGroup
		{
			get
			{
				return this.controlGroup;
			}
			set
			{
				this.controlGroup = value;
				this.SetModified();
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001AB RID: 427 RVA: 0x0002610C File Offset: 0x0002430C
		// (set) Token: 0x060001AC RID: 428 RVA: 0x00026124 File Offset: 0x00024324
		public string ControlGroupOverride
		{
			get
			{
				return this.controlGroupOverride;
			}
			set
			{
				this.controlGroupOverride = value;
				this.SetModified();
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00026138 File Offset: 0x00024338
		// (set) Token: 0x060001AE RID: 430 RVA: 0x00026150 File Offset: 0x00024350
		public string HelpText
		{
			get
			{
				return this.helpText;
			}
			set
			{
				this.helpText = value;
				this.SetModified();
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00026164 File Offset: 0x00024364
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x0002617C File Offset: 0x0002437C
		public string Mask
		{
			get
			{
				return this.mask;
			}
			set
			{
				this.mask = value;
				this.SetModified();
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00026190 File Offset: 0x00024390
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x000261A8 File Offset: 0x000243A8
		public string ActionHandlers
		{
			get
			{
				return this.actionHandlers;
			}
			set
			{
				this.actionHandlers = value;
				this.SetModified();
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x000261BC File Offset: 0x000243BC
		// (set) Token: 0x060001B4 RID: 436 RVA: 0x000261D4 File Offset: 0x000243D4
		public string DefaultValueString
		{
			get
			{
				return this.defaultValueString;
			}
			set
			{
				this.defaultValueString = value;
				this.SetModified();
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x000261E8 File Offset: 0x000243E8
		// (set) Token: 0x060001B6 RID: 438 RVA: 0x00026200 File Offset: 0x00024400
		public string Setting4String
		{
			get
			{
				return this.setting4String;
			}
			set
			{
				this.setting4String = value;
				this.SetModified();
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00026214 File Offset: 0x00024414
		public int HelpTextDisplayMethod
		{
			get
			{
				return this.helpTextDisplayMethod;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x0002622C File Offset: 0x0002442C
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x00026244 File Offset: 0x00024444
		public int Setting4
		{
			get
			{
				return this.setting4;
			}
			set
			{
				this.setting4 = value;
				this.SetModified();
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00026258 File Offset: 0x00024458
		public int FontSize
		{
			get
			{
				return this.fontSize;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00026270 File Offset: 0x00024470
		// (set) Token: 0x060001BC RID: 444 RVA: 0x00026288 File Offset: 0x00024488
		public int Enforce
		{
			get
			{
				return this.enforce;
			}
			set
			{
				this.enforce = value;
				this.SetModified();
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001BD RID: 445 RVA: 0x0002629C File Offset: 0x0002449C
		// (set) Token: 0x060001BE RID: 446 RVA: 0x000262B4 File Offset: 0x000244B4
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				this.enabled = value;
				this.SetModified();
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001BF RID: 447 RVA: 0x000262C8 File Offset: 0x000244C8
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x000262E0 File Offset: 0x000244E0
		public bool ReadOnly
		{
			get
			{
				return this.readOnly;
			}
			set
			{
				this.readOnly = value;
				this.SetModified();
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x000262F4 File Offset: 0x000244F4
		// (set) Token: 0x060001C2 RID: 450 RVA: 0x0002630C File Offset: 0x0002450C
		public bool HideCaption
		{
			get
			{
				return this.hideCaption;
			}
			set
			{
				this.hideCaption = value;
				this.SetModified();
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00026320 File Offset: 0x00024520
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x00026338 File Offset: 0x00024538
		public bool DontWrapToNextLine
		{
			get
			{
				return this.dontWrapToNextLine;
			}
			set
			{
				this.dontWrapToNextLine = value;
				this.SetModified();
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x0002634C File Offset: 0x0002454C
		public int ExtendedAccommodation_ShowOnLetter
		{
			get
			{
				int num = 0;
				bool flag = this.extendedAccommodation_group_prof;
				if (flag)
				{
					num++;
				}
				bool flag2 = this.extendedAccommodation_group_exam;
				if (flag2)
				{
					num += 2;
				}
				bool flag3 = this.extendedAccommodation_group_other;
				if (flag3)
				{
					num += 4;
				}
				return num;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00026390 File Offset: 0x00024590
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x000263A8 File Offset: 0x000245A8
		public bool ExtendedAccommodation_IsExtraTimeAccommodation
		{
			get
			{
				return this.extendedAccommodation_IsExtraTimeAccommodation;
			}
			set
			{
				this.extendedAccommodation_IsExtraTimeAccommodation = value;
				this.ExtendedAccommodation_SomethingChangedByUser = true;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x000263BC File Offset: 0x000245BC
		// (set) Token: 0x060001C9 RID: 457 RVA: 0x000263D4 File Offset: 0x000245D4
		public bool ExtendedAccommodation_IsAloneAccommodation
		{
			get
			{
				return this.extendedAccommodation_IsAloneAccommodation;
			}
			set
			{
				this.extendedAccommodation_IsAloneAccommodation = value;
				this.ExtendedAccommodation_SomethingChangedByUser = true;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001CA RID: 458 RVA: 0x000263E8 File Offset: 0x000245E8
		// (set) Token: 0x060001CB RID: 459 RVA: 0x00026400 File Offset: 0x00024600
		public bool ExtendedAccommodation_IsGroupAccommodation
		{
			get
			{
				return this.extendedAccommodation_IsGroupAccommodation;
			}
			set
			{
				this.extendedAccommodation_IsGroupAccommodation = value;
				this.ExtendedAccommodation_SomethingChangedByUser = true;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00026414 File Offset: 0x00024614
		// (set) Token: 0x060001CD RID: 461 RVA: 0x0002642C File Offset: 0x0002462C
		public bool ExtendedAccommodation_IsComputerAccommodation
		{
			get
			{
				return this.extendedAccommodation_IsComputerAccommodation;
			}
			set
			{
				this.extendedAccommodation_IsComputerAccommodation = value;
				this.ExtendedAccommodation_SomethingChangedByUser = true;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00026440 File Offset: 0x00024640
		// (set) Token: 0x060001CF RID: 463 RVA: 0x00026458 File Offset: 0x00024658
		public bool ExtendedAccommodation_IsReaderScribeAccommodation
		{
			get
			{
				return this.extendedAccommodation_IsReaderScribeAccommodation;
			}
			set
			{
				this.extendedAccommodation_IsReaderScribeAccommodation = value;
				this.ExtendedAccommodation_SomethingChangedByUser = true;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x0002646C File Offset: 0x0002466C
		// (set) Token: 0x060001D1 RID: 465 RVA: 0x00026484 File Offset: 0x00024684
		public bool ExtendedAccommodation_IsEnlargedTextAccommodation
		{
			get
			{
				return this.extendedAccommodation_IsEnlargedTextAccommodation;
			}
			set
			{
				this.extendedAccommodation_IsEnlargedTextAccommodation = value;
				this.ExtendedAccommodation_SomethingChangedByUser = true;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x00026498 File Offset: 0x00024698
		// (set) Token: 0x060001D3 RID: 467 RVA: 0x000264B0 File Offset: 0x000246B0
		public bool ExtendedAccommodation_IsOtherAccommodation
		{
			get
			{
				return this.extendedAccommodation_IsOtherAccommodation;
			}
			set
			{
				this.extendedAccommodation_IsOtherAccommodation = value;
				this.ExtendedAccommodation_SomethingChangedByUser = true;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x000264C4 File Offset: 0x000246C4
		// (set) Token: 0x060001D5 RID: 469 RVA: 0x000264DC File Offset: 0x000246DC
		public string ExtendedAccommodation_LongDescription
		{
			get
			{
				return this.extendedAccommodation_LongDescription;
			}
			set
			{
				this.extendedAccommodation_LongDescription = value;
				this.ExtendedAccommodation_SomethingChangedByUser = true;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x000264F0 File Offset: 0x000246F0
		// (set) Token: 0x060001D7 RID: 471 RVA: 0x00026508 File Offset: 0x00024708
		public bool ExtendedAccommodation_group_prof
		{
			get
			{
				return this.extendedAccommodation_group_prof;
			}
			set
			{
				this.extendedAccommodation_group_prof = value;
				this.ExtendedAccommodation_SomethingChangedByUser = true;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x0002651C File Offset: 0x0002471C
		// (set) Token: 0x060001D9 RID: 473 RVA: 0x00026534 File Offset: 0x00024734
		public bool ExtendedAccommodation_group_exam
		{
			get
			{
				return this.extendedAccommodation_group_exam;
			}
			set
			{
				this.extendedAccommodation_group_exam = value;
				this.ExtendedAccommodation_SomethingChangedByUser = true;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00026548 File Offset: 0x00024748
		// (set) Token: 0x060001DB RID: 475 RVA: 0x00026560 File Offset: 0x00024760
		public bool ExtendedAccommodation_group_other
		{
			get
			{
				return this.extendedAccommodation_group_other;
			}
			set
			{
				this.extendedAccommodation_group_other = value;
				this.ExtendedAccommodation_SomethingChangedByUser = true;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00026574 File Offset: 0x00024774
		// (set) Token: 0x060001DD RID: 477 RVA: 0x0002658C File Offset: 0x0002478C
		public bool ExtendedAccommodation_group_report
		{
			get
			{
				return this.extendedAccommodation_group_report;
			}
			set
			{
				this.extendedAccommodation_group_report = value;
				this.ExtendedAccommodation_SomethingChangedByUser = true;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001DE RID: 478 RVA: 0x000265A0 File Offset: 0x000247A0
		// (set) Token: 0x060001DF RID: 479 RVA: 0x000265B8 File Offset: 0x000247B8
		public string ExtendedAccommodation_shortCode
		{
			get
			{
				return this.extendedAccommodation_shortCode;
			}
			set
			{
				this.extendedAccommodation_shortCode = value;
				this.ExtendedAccommodation_SomethingChangedByUser = true;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x000265CC File Offset: 0x000247CC
		public string Name
		{
			get
			{
				bool flag = this.controlName == null || this.controlName.Length < 1;
				string result;
				if (flag)
				{
					result = this.control_caption;
				}
				else
				{
					result = this.controlName;
				}
				return result;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x0002660C File Offset: 0x0002480C
		public bool ComboIsTextBased
		{
			get
			{
				return this.control_code == 3 && (this.setting3 == -1 || this.setting3 == 1);
			}
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00026640 File Offset: 0x00024840
		public DynamicControl(int control_id, string control_caption, int control_code, int setting1, int setting2, int setting3, int default_value)
		{
			this.ScreensIBelongTo = new ScreenCollection();
			this.control_id = control_id;
			this.control_caption = control_caption;
			this.control_code = control_code;
			this.setting1 = setting1;
			this.setting2 = setting2;
			this.setting3 = setting3;
			this.default_value = default_value;
			this.SetDefaultExtendedValues();
			bool flag = control_id < 0;
			if (flag)
			{
				this.howModified = ModificationType.Added;
			}
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x000266EC File Offset: 0x000248EC
		public DynamicControl(string dynamicControlCreationStrings)
		{
			this.SetDefaultExtendedValues();
			this.control_id = 0;
			string[] array = dynamicControlCreationStrings.Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				string s = array[i];
				switch (i)
				{
				case 0:
					this.control_code = int.Parse(s);
					break;
				case 1:
					this.control_caption = s;
					break;
				case 2:
					this.setting1 = int.Parse(s);
					break;
				case 3:
					this.setting2 = int.Parse(s);
					break;
				case 4:
					this.setting3 = int.Parse(s);
					break;
				case 5:
					this.default_value = int.Parse(s);
					break;
				case 6:
					this.defaultValueString = s;
					break;
				}
			}
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00026804 File Offset: 0x00024A04
		private void SetDefaultExtendedValues()
		{
			this.controlName = "";
			this.controlGroup = "";
			this.helpText = "";
			this.mask = "";
			this.actionHandlers = "";
			this.defaultValueString = "";
			this.setting4String = "";
			this.helpTextDisplayMethod = 1;
			this.setting4 = 0;
			this.fontSize = 0;
			this.enforce = 0;
			this.enabled = true;
			this.readOnly = false;
			this.hideCaption = false;
			this.dontWrapToNextLine = false;
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00026898 File Offset: 0x00024A98
		public DynamicControl(DataRow dr)
		{
			this.ScreensIBelongTo = new ScreenCollection();
			bool flag = dr != null && dr.RowState != DataRowState.Deleted;
			if (flag)
			{
				this.control_id = ((dr["controlid"] == DBNull.Value) ? -1 : ((int)dr["controlid"]));
				this.control_caption = dr["controlcaption"].ToString();
				this.control_code = ((dr["controlcode"] == DBNull.Value) ? 0 : ((int)dr["controlcode"]));
				this.setting1 = ((dr["setting1"] == DBNull.Value) ? 0 : ((int)dr["setting1"]));
				this.setting2 = ((dr["setting2"] == DBNull.Value) ? 0 : ((int)dr["setting2"]));
				this.setting3 = ((dr["setting3"] == DBNull.Value) ? 0 : ((int)dr["setting3"]));
				this.default_value = ((dr["defaultvalue"] == DBNull.Value) ? 0 : ((int)dr["defaultvalue"]));
				DataTable table = dr.Table;
				string[] array = new string[]
				{
					"controlname",
					"controlgroup",
					"helptext",
					"mask",
					"actionhandlers",
					"defaultvaluestring",
					"setting4string"
				};
				foreach (string text in array)
				{
					bool flag2 = table.Columns.Contains(text) && dr[text] == DBNull.Value;
					if (flag2)
					{
						dr[text] = "";
					}
				}
				string[] array3 = new string[]
				{
					"setting4",
					"fontsize",
					"enforce"
				};
				foreach (string text2 in array3)
				{
					bool flag3 = table.Columns.Contains(text2) && dr[text2] == DBNull.Value;
					if (flag3)
					{
						dr[text2] = 0;
					}
				}
				bool flag4 = table.Columns.Contains("helptextdisplaymethod") && dr["helptextdisplaymethod"] == DBNull.Value;
				if (flag4)
				{
					dr["helptextdisplaymethod"] = 1;
				}
				string[] array5 = new string[]
				{
					"readonly",
					"hidecaption",
					"dontwraptonextline"
				};
				foreach (string text3 in array5)
				{
					bool flag5 = table.Columns.Contains(text3) && dr[text3] == DBNull.Value;
					if (flag5)
					{
						dr[text3] = false;
					}
				}
				bool flag6 = table.Columns.Contains("enabled") && dr["enabled"] == DBNull.Value;
				if (flag6)
				{
					dr["enabled"] = true;
				}
				bool flag7 = table.Columns.Contains("controlname");
				if (flag7)
				{
					this.controlName = (string)dr["controlname"];
				}
				else
				{
					this.controlName = "";
				}
				bool flag8 = table.Columns.Contains("controlgroup");
				if (flag8)
				{
					this.controlGroup = (string)dr["controlgroup"];
				}
				else
				{
					this.controlGroup = "";
				}
				bool flag9 = table.Columns.Contains("controlgroupoverride");
				if (flag9)
				{
					this.controlGroupOverride = ((dr["controlgroupoverride"] == DBNull.Value) ? "" : ((string)dr["controlgroupoverride"]));
				}
				bool flag10 = table.Columns.Contains("helptext");
				if (flag10)
				{
					this.helpText = (string)dr["helptext"];
				}
				else
				{
					this.helpText = "";
				}
				bool flag11 = table.Columns.Contains("mask");
				if (flag11)
				{
					this.mask = (string)dr["mask"];
				}
				else
				{
					this.mask = "";
				}
				bool flag12 = table.Columns.Contains("actionhandlers");
				if (flag12)
				{
					this.actionHandlers = (string)dr["actionhandlers"];
				}
				else
				{
					this.actionHandlers = "";
				}
				bool flag13 = table.Columns.Contains("defaultvaluestring");
				if (flag13)
				{
					this.defaultValueString = (string)dr["defaultvaluestring"];
				}
				else
				{
					this.defaultValueString = "";
				}
				bool flag14 = table.Columns.Contains("setting4string");
				if (flag14)
				{
					this.setting4String = (string)dr["setting4string"];
				}
				else
				{
					this.setting4String = "";
				}
				bool flag15 = table.Columns.Contains("helptextdisplaymethod");
				if (flag15)
				{
					this.helpTextDisplayMethod = (int)dr["helptextdisplaymethod"];
				}
				else
				{
					this.helpTextDisplayMethod = 1;
				}
				bool flag16 = table.Columns.Contains("setting4");
				if (flag16)
				{
					this.setting4 = (int)dr["setting4"];
				}
				else
				{
					this.setting4 = 0;
				}
				bool flag17 = table.Columns.Contains("fontsize");
				if (flag17)
				{
					this.fontSize = (int)dr["fontsize"];
				}
				else
				{
					this.fontSize = 0;
				}
				bool flag18 = table.Columns.Contains("enforce");
				if (flag18)
				{
					this.enforce = (int)dr["enforce"];
				}
				else
				{
					this.enforce = 0;
				}
				bool flag19 = table.Columns.Contains("enabled");
				if (flag19)
				{
					this.enabled = Convert.ToBoolean(dr["enabled"]);
				}
				else
				{
					this.enabled = true;
				}
				bool flag20 = table.Columns.Contains("readonly");
				if (flag20)
				{
					this.readOnly = Convert.ToBoolean(dr["readonly"]);
				}
				else
				{
					this.readOnly = false;
				}
				bool flag21 = table.Columns.Contains("hidecaption");
				if (flag21)
				{
					this.hideCaption = Convert.ToBoolean(dr["hidecaption"]);
				}
				else
				{
					this.hideCaption = false;
				}
				bool flag22 = table.Columns.Contains("dontwraptonextline");
				if (flag22)
				{
					this.dontWrapToNextLine = Convert.ToBoolean(dr["dontwraptonextline"]);
				}
				else
				{
					this.dontWrapToNextLine = false;
				}
				bool flag23 = table.Columns.Contains("accommodationid");
				if (flag23)
				{
					this.accommodationId = ((dr["accommodationid"] == DBNull.Value) ? 0 : ((int)dr["accommodationid"]));
				}
				bool flag24 = table.Columns.Contains("showonletter");
				if (flag24)
				{
					this.showOnLetter = ((dr["showonletter"] == DBNull.Value) ? 0 : ((int)dr["showonletter"]));
				}
				bool flag25 = table.Columns.Contains("longdescription");
				if (flag25)
				{
					this.extendedAccommodation_LongDescription = ((dr["longdescription"] == DBNull.Value) ? "" : ((string)dr["longdescription"]));
				}
				bool flag26 = table.Columns.Contains("shortcode");
				if (flag26)
				{
					this.extendedAccommodation_shortCode = ((dr["shortcode"] == DBNull.Value) ? "" : ((string)dr["shortcode"]));
				}
				bool flag27 = table.Columns.Contains("extratime");
				if (flag27)
				{
					this.extendedAccommodation_IsExtraTimeAccommodation = (dr["extratime"] != DBNull.Value && Convert.ToBoolean(dr["extratime"]));
				}
				bool flag28 = table.Columns.Contains("isalone");
				if (flag28)
				{
					this.extendedAccommodation_IsAloneAccommodation = (dr["isalone"] != DBNull.Value && Convert.ToBoolean(dr["isalone"]));
				}
				bool flag29 = table.Columns.Contains("needscomputer");
				if (flag29)
				{
					this.extendedAccommodation_IsComputerAccommodation = (dr["needscomputer"] != DBNull.Value && Convert.ToBoolean(dr["needscomputer"]));
				}
				bool flag30 = table.Columns.Contains("needsreaderscribe");
				if (flag30)
				{
					this.extendedAccommodation_IsReaderScribeAccommodation = (dr["needsreaderscribe"] != DBNull.Value && Convert.ToBoolean(dr["needsreaderscribe"]));
				}
				bool flag31 = table.Columns.Contains("isgroup");
				if (flag31)
				{
					this.extendedAccommodation_IsGroupAccommodation = (dr["isgroup"] != DBNull.Value && Convert.ToBoolean(dr["isgroup"]));
				}
				bool flag32 = table.Columns.Contains("other");
				if (flag32)
				{
					this.extendedAccommodation_IsOtherAccommodation = (dr["other"] != DBNull.Value && Convert.ToBoolean(dr["other"]));
				}
				bool flag33 = table.Columns.Contains("enlarged");
				if (flag33)
				{
					this.extendedAccommodation_IsEnlargedTextAccommodation = (dr["enlarged"] != DBNull.Value && Convert.ToBoolean(dr["enlarged"]));
				}
				bool flag34 = table.Columns.Contains("showonletter");
				if (flag34)
				{
					int num = (dr["showonletter"] == DBNull.Value) ? 0 : ((int)dr["showonletter"]);
					bool flag35 = (num & 1) == 1;
					if (flag35)
					{
						this.extendedAccommodation_group_prof = true;
					}
					bool flag36 = (num & 2) == 2;
					if (flag36)
					{
						this.extendedAccommodation_group_exam = true;
					}
					bool flag37 = (num & 4) == 4;
					if (flag37)
					{
						this.extendedAccommodation_group_other = true;
					}
				}
				bool flag38 = table.Columns.Contains("showonreport");
				if (flag38)
				{
					this.extendedAccommodation_group_report = (dr["showonreport"] != DBNull.Value && (int)dr["showonreport"] != 0);
				}
				bool flag39 = table.Columns.Contains("uniqueid");
				if (flag39)
				{
					this.UniqueId = dr["uniqueid"].ToString();
				}
				bool flag40 = table.Columns.Contains("specialcontroltype");
				if (flag40)
				{
					this.SpecialControlType = ((dr["specialcontroltype"] is DBNull) ? 0 : ((int)dr["specialcontroltype"]));
				}
			}
			else
			{
				this.control_id = -1;
				this.control_caption = "";
				this.control_code = -1;
				this.setting1 = -1;
				this.setting2 = -1;
				this.setting3 = -1;
				this.default_value = -1;
				this.SetDefaultExtendedValues();
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x000273E8 File Offset: 0x000255E8
		public bool IsComboBox
		{
			get
			{
				return this.control_code == 3;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x00027404 File Offset: 0x00025604
		public bool IsTextBox
		{
			get
			{
				return this.control_code == 1 || this.control_code == 11 || this.control_code == 300;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x0002743C File Offset: 0x0002563C
		public bool IsCheckBox
		{
			get
			{
				return this.control_code == 2;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x00027458 File Offset: 0x00025658
		public bool IsRadioButton
		{
			get
			{
				return this.control_code == 4;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001EA RID: 490 RVA: 0x00027474 File Offset: 0x00025674
		public bool IsDate
		{
			get
			{
				return this.control_code == 6;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00027490 File Offset: 0x00025690
		public bool IsLabel
		{
			get
			{
				return this.control_code == 5;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001EC RID: 492 RVA: 0x000274AC File Offset: 0x000256AC
		public bool IsListView
		{
			get
			{
				return this.control_code == 10;
			}
		}

		// Token: 0x060001ED RID: 493 RVA: 0x000274C8 File Offset: 0x000256C8
		public static string GetSpecialInstructionStringValue(string allSpecials, string name)
		{
			bool flag = allSpecials == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				string[] array = DynamicControl.SplitStringIntoNEWLINE_delimitered_parts(allSpecials, true);
				string value = name.ToLower().Trim();
				foreach (string text in array)
				{
					int num = text.IndexOf('=');
					bool flag2 = num > 0;
					if (flag2)
					{
						bool flag3 = text.Substring(0, num).ToLower().Trim().Equals(value);
						if (flag3)
						{
							num++;
							bool flag4 = num < text.Length;
							if (flag4)
							{
								return text.Substring(num);
							}
							return "";
						}
					}
				}
				result = "";
			}
			return result;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0002758C File Offset: 0x0002578C
		public static string SetSpecialInstructionStringValue(string allSpecials, string name, string value)
		{
			bool flag = string.IsNullOrEmpty(allSpecials);
			string result;
			if (flag)
			{
				result = (string.IsNullOrEmpty(value) ? "" : (name + "=" + value));
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder();
				string[] array = DynamicControl.SplitStringIntoNEWLINE_delimitered_parts(allSpecials, true);
				string value2 = name.ToLower().Trim();
				bool flag2 = false;
				foreach (string text in array)
				{
					int num = text.IndexOf('=');
					bool flag3 = num > 0;
					if (flag3)
					{
						bool flag4 = text.Substring(0, num).ToLower().Trim().Equals(value2);
						if (flag4)
						{
							flag2 = true;
							bool flag5 = !string.IsNullOrEmpty(value);
							if (flag5)
							{
								bool flag6 = stringBuilder.Length > 0;
								if (flag6)
								{
									stringBuilder.Append(Environment.NewLine);
								}
								stringBuilder.Append(name);
								stringBuilder.Append("=");
								stringBuilder.Append(value);
							}
						}
						else
						{
							bool flag7 = stringBuilder.Length > 0;
							if (flag7)
							{
								stringBuilder.Append(Environment.NewLine);
							}
							stringBuilder.Append(text);
						}
					}
				}
				bool flag8 = !flag2;
				if (flag8)
				{
					bool flag9 = stringBuilder.Length > 0;
					if (flag9)
					{
						stringBuilder.Append(Environment.NewLine);
					}
					stringBuilder.Append(name);
					stringBuilder.Append("=");
					stringBuilder.Append(value);
				}
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0002770C File Offset: 0x0002590C
		public static DataTable CreateControlsTable()
		{
			DataTable dataTable = new DataTable("t");
			Type typeFromHandle = typeof(int);
			dataTable.Columns.Add("controlid", typeFromHandle);
			dataTable.Columns.Add("screennum", typeFromHandle);
			dataTable.Columns.Add("controlcode", typeFromHandle);
			dataTable.Columns.Add("controlcaption");
			dataTable.Columns.Add("setting1", typeFromHandle);
			dataTable.Columns.Add("setting2", typeFromHandle);
			dataTable.Columns.Add("setting3", typeFromHandle);
			dataTable.Columns.Add("defaultvalue", typeFromHandle);
			dataTable.Columns.Add("controlname");
			dataTable.Columns.Add("controlgroup");
			dataTable.Columns.Add("helptext");
			dataTable.Columns.Add("helptextdisplaymethod", typeFromHandle);
			dataTable.Columns.Add("mask");
			dataTable.Columns.Add("enforce", typeFromHandle);
			dataTable.Columns.Add("actionhandlers");
			dataTable.Columns.Add("defaultvaluestring");
			dataTable.Columns.Add("setting4string");
			dataTable.Columns.Add("enabled", typeof(bool));
			dataTable.Columns.Add("readonly", typeof(bool));
			dataTable.Columns.Add("hidecaption", typeof(bool));
			dataTable.Columns.Add("setting4", typeFromHandle);
			dataTable.Columns.Add("fontsize", typeFromHandle);
			dataTable.Columns.Add("dontwraptonextline", typeof(bool));
			dataTable.Columns.Add("controlgroupoverride");
			dataTable.Columns.Add("uniqueid");
			dataTable.Columns.Add("specialcontroltype", typeof(int));
			return dataTable;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0002792C File Offset: 0x00025B2C
		public void CreateRowAndAddToTable(ref DataTable t, int screenNum)
		{
			DataRow dataRow = t.NewRow();
			dataRow[0] = this.ControlId;
			dataRow[1] = screenNum;
			dataRow[8] = this.ControlName;
			dataRow[3] = this.ControlCaption;
			dataRow[14] = this.ActionHandlers;
			dataRow[2] = this.ControlCode;
			dataRow[9] = this.ControlGroup;
			dataRow[7] = this.DefaultValue;
			dataRow[15] = this.DefaultValueString;
			dataRow[22] = this.DontWrapToNextLine;
			dataRow[17] = this.Enabled;
			dataRow[13] = this.Enforce;
			dataRow[21] = this.FontSize;
			dataRow[10] = this.HelpText;
			dataRow[11] = this.HelpTextDisplayMethod;
			dataRow[19] = this.HideCaption;
			dataRow[12] = this.Mask;
			dataRow[18] = this.ReadOnly;
			dataRow[4] = this.Setting1;
			dataRow[5] = this.Setting2;
			dataRow[6] = this.Setting3;
			dataRow[20] = this.Setting4;
			dataRow[16] = this.Setting4String;
			dataRow["uniqueid"] = (this.UniqueId ?? "");
			dataRow["specialcontroltype"] = this.SpecialControlType;
			bool flag = t.Columns.Contains("controlgroupoverride");
			if (flag)
			{
				bool flag2 = string.IsNullOrEmpty(this.controlGroupOverride);
				if (flag2)
				{
					dataRow["controlgroupoverride"] = DBNull.Value;
				}
				else
				{
					dataRow["controlgroupoverride"] = this.controlGroupOverride;
				}
			}
			t.Rows.Add(dataRow);
		}

		// Token: 0x04000068 RID: 104
		public const int Enforce_Optional = 0;

		// Token: 0x04000069 RID: 105
		public const int Enforce_Warning = 1;

		// Token: 0x0400006A RID: 106
		public const int Enforce_Error = 2;

		// Token: 0x0400006B RID: 107
		private ModificationType howModified = ModificationType.Unchanged;

		// Token: 0x0400006C RID: 108
		private int control_id;

		// Token: 0x0400006D RID: 109
		private string control_caption;

		// Token: 0x0400006E RID: 110
		private int control_code;

		// Token: 0x0400006F RID: 111
		private int setting1;

		// Token: 0x04000070 RID: 112
		private int setting2;

		// Token: 0x04000071 RID: 113
		private int setting3;

		// Token: 0x04000072 RID: 114
		private int default_value;

		// Token: 0x04000073 RID: 115
		private string controlName;

		// Token: 0x04000074 RID: 116
		private string controlGroup;

		// Token: 0x04000075 RID: 117
		private string helpText;

		// Token: 0x04000076 RID: 118
		private string mask;

		// Token: 0x04000077 RID: 119
		private string actionHandlers;

		// Token: 0x04000078 RID: 120
		private string defaultValueString;

		// Token: 0x04000079 RID: 121
		private string setting4String;

		// Token: 0x0400007A RID: 122
		private int helpTextDisplayMethod;

		// Token: 0x0400007B RID: 123
		private int setting4;

		// Token: 0x0400007C RID: 124
		private int fontSize;

		// Token: 0x0400007D RID: 125
		private int enforce;

		// Token: 0x0400007E RID: 126
		private bool enabled;

		// Token: 0x0400007F RID: 127
		private bool readOnly;

		// Token: 0x04000080 RID: 128
		private bool hideCaption;

		// Token: 0x04000081 RID: 129
		private bool dontWrapToNextLine;

		// Token: 0x04000082 RID: 130
		private ScreenCollection screensIBelongTo;

		// Token: 0x04000083 RID: 131
		public ArrayList tagList;

		// Token: 0x04000084 RID: 132
		private string controlGroupOverride;

		// Token: 0x04000085 RID: 133
		private int accommodationId = 0;

		// Token: 0x04000086 RID: 134
		private int showOnLetter = 0;

		// Token: 0x04000088 RID: 136
		private int _specialControlType;

		// Token: 0x04000089 RID: 137
		private object tag;

		// Token: 0x0400008A RID: 138
		private DynamicControl associatedDynamicControl = null;

		// Token: 0x0400008B RID: 139
		private StringDictionary specialInstructionArgs = null;

		// Token: 0x0400008C RID: 140
		public ScreenCollection ScreensIBelongTo;

		// Token: 0x0400008D RID: 141
		public bool ExtendedAccommodation_SomethingChangedByUser = false;

		// Token: 0x0400008E RID: 142
		private string extendedAccommodation_LongDescription = "";

		// Token: 0x0400008F RID: 143
		private bool extendedAccommodation_group_prof;

		// Token: 0x04000090 RID: 144
		private bool extendedAccommodation_group_exam;

		// Token: 0x04000091 RID: 145
		private bool extendedAccommodation_group_other;

		// Token: 0x04000092 RID: 146
		private bool extendedAccommodation_group_report;

		// Token: 0x04000093 RID: 147
		private string extendedAccommodation_shortCode = "";

		// Token: 0x04000094 RID: 148
		private bool extendedAccommodation_IsExtraTimeAccommodation;

		// Token: 0x04000095 RID: 149
		private bool extendedAccommodation_IsAloneAccommodation;

		// Token: 0x04000096 RID: 150
		private bool extendedAccommodation_IsGroupAccommodation;

		// Token: 0x04000097 RID: 151
		private bool extendedAccommodation_IsComputerAccommodation;

		// Token: 0x04000098 RID: 152
		private bool extendedAccommodation_IsReaderScribeAccommodation;

		// Token: 0x04000099 RID: 153
		private bool extendedAccommodation_IsEnlargedTextAccommodation;

		// Token: 0x0400009A RID: 154
		private bool extendedAccommodation_IsOtherAccommodation;
	}
}
