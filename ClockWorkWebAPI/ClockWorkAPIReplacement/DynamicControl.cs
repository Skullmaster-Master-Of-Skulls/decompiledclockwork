using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;

namespace ClockWorkWebAPI.ClockWorkAPIReplacement
{
	// Token: 0x02000056 RID: 86
	public class DynamicControl
	{
		// Token: 0x17000148 RID: 328
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x0001E35C File Offset: 0x0001C55C
		// (set) Token: 0x0600042D RID: 1069 RVA: 0x0001E374 File Offset: 0x0001C574
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

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x0001E380 File Offset: 0x0001C580
		public bool IsAccommodationControl
		{
			get
			{
				return this.accommodationId > 0;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x0001E39C File Offset: 0x0001C59C
		public Point Location
		{
			get
			{
				int num = this.control_caption.IndexOf('%');
				bool flag = num > 0;
				Point result;
				if (flag)
				{
					string text = this.control_caption.Substring(num + 1);
					num = text.IndexOf(',');
					int x = int.Parse(text.Substring(0, num));
					int y = int.Parse(text.Substring(num + 1));
					result = new Point(x, y);
				}
				else
				{
					result = Point.Empty;
				}
				return result;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x0001E410 File Offset: 0x0001C610
		public MeasurementUnit LocationUnit
		{
			get
			{
				return MeasurementUnit.Pixel;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x0001E424 File Offset: 0x0001C624
		// (set) Token: 0x06000432 RID: 1074 RVA: 0x0001E43C File Offset: 0x0001C63C
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

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x0001E448 File Offset: 0x0001C648
		// (set) Token: 0x06000434 RID: 1076 RVA: 0x0001E460 File Offset: 0x0001C660
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

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x0001E46C File Offset: 0x0001C66C
		// (set) Token: 0x06000436 RID: 1078 RVA: 0x0001E484 File Offset: 0x0001C684
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

		// Token: 0x06000437 RID: 1079 RVA: 0x0001E48E File Offset: 0x0001C68E
		public void SetControlCode(int newControlCode)
		{
			this.control_code = newControlCode;
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x0001E498 File Offset: 0x0001C698
		public bool HasSpecialInstructions
		{
			get
			{
				return this.controlGroup.Length > 0;
			}
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0001E4B8 File Offset: 0x0001C6B8
		public string SpecialInstructionsNoNull(string key)
		{
			string text = this.SpecialInstructions(key);
			return (text == null) ? "" : text;
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0001E4E0 File Offset: 0x0001C6E0
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

		// Token: 0x0600043B RID: 1083 RVA: 0x0001E550 File Offset: 0x0001C750
		public void RemoveSpecialInstruction(string key)
		{
			bool flag = this.specialInstructionArgs != null && this.specialInstructionArgs.ContainsKey(key);
			if (flag)
			{
				this.specialInstructionArgs.Remove(key);
			}
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0001E588 File Offset: 0x0001C788
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

		// Token: 0x0600043D RID: 1085 RVA: 0x0001E5E0 File Offset: 0x0001C7E0
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

		// Token: 0x0600043E RID: 1086 RVA: 0x0001E67C File Offset: 0x0001C87C
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

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x0001E724 File Offset: 0x0001C924
		// (set) Token: 0x06000440 RID: 1088 RVA: 0x0001E73C File Offset: 0x0001C93C
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

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x0001E748 File Offset: 0x0001C948
		// (set) Token: 0x06000442 RID: 1090 RVA: 0x0001E760 File Offset: 0x0001C960
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

		// Token: 0x06000443 RID: 1091 RVA: 0x0001E774 File Offset: 0x0001C974
		private void SetModified()
		{
			bool flag = this.howModified == ModificationType.Unchanged;
			if (flag)
			{
				this.howModified = ModificationType.Modified;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x0001E798 File Offset: 0x0001C998
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

		// Token: 0x06000445 RID: 1093 RVA: 0x0001E820 File Offset: 0x0001CA20
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

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x0001E88C File Offset: 0x0001CA8C
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

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x0001E8F4 File Offset: 0x0001CAF4
		public string ControlCaptionAsColumnName
		{
			get
			{
				return Regex.Replace(this.control_caption, "[^0-9a-zA-Z\\._]", string.Empty);
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x0001E91C File Offset: 0x0001CB1C
		public string ControlCaptionForDisplay
		{
			get
			{
				return DynamicControl.GetControlCaptionForDisplay(this.control_caption);
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x0001E93C File Offset: 0x0001CB3C
		public int ControlCode
		{
			get
			{
				return this.control_code;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x0001E954 File Offset: 0x0001CB54
		// (set) Token: 0x0600044B RID: 1099 RVA: 0x0001E96C File Offset: 0x0001CB6C
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

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x0001E980 File Offset: 0x0001CB80
		// (set) Token: 0x0600044D RID: 1101 RVA: 0x0001E998 File Offset: 0x0001CB98
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

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x0001E9AC File Offset: 0x0001CBAC
		// (set) Token: 0x0600044F RID: 1103 RVA: 0x0001E9C4 File Offset: 0x0001CBC4
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

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x0001E9D8 File Offset: 0x0001CBD8
		// (set) Token: 0x06000451 RID: 1105 RVA: 0x0001E9F0 File Offset: 0x0001CBF0
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

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x0001EA04 File Offset: 0x0001CC04
		// (set) Token: 0x06000453 RID: 1107 RVA: 0x0001EA1C File Offset: 0x0001CC1C
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

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x0001EA30 File Offset: 0x0001CC30
		// (set) Token: 0x06000455 RID: 1109 RVA: 0x0001EA48 File Offset: 0x0001CC48
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

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x0001EA5C File Offset: 0x0001CC5C
		// (set) Token: 0x06000457 RID: 1111 RVA: 0x0001EA74 File Offset: 0x0001CC74
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

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x0001EA88 File Offset: 0x0001CC88
		// (set) Token: 0x06000459 RID: 1113 RVA: 0x0001EAA0 File Offset: 0x0001CCA0
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

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x0001EAB4 File Offset: 0x0001CCB4
		// (set) Token: 0x0600045B RID: 1115 RVA: 0x0001EACC File Offset: 0x0001CCCC
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

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x0001EAE0 File Offset: 0x0001CCE0
		// (set) Token: 0x0600045D RID: 1117 RVA: 0x0001EAF8 File Offset: 0x0001CCF8
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

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x0001EB0C File Offset: 0x0001CD0C
		// (set) Token: 0x0600045F RID: 1119 RVA: 0x0001EB24 File Offset: 0x0001CD24
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

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x0001EB38 File Offset: 0x0001CD38
		public int HelpTextDisplayMethod
		{
			get
			{
				return this.helpTextDisplayMethod;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x0001EB50 File Offset: 0x0001CD50
		// (set) Token: 0x06000462 RID: 1122 RVA: 0x0001EB68 File Offset: 0x0001CD68
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

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x0001EB7C File Offset: 0x0001CD7C
		public int FontSize
		{
			get
			{
				return this.fontSize;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x0001EB94 File Offset: 0x0001CD94
		// (set) Token: 0x06000465 RID: 1125 RVA: 0x0001EBAC File Offset: 0x0001CDAC
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

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x0001EBC0 File Offset: 0x0001CDC0
		// (set) Token: 0x06000467 RID: 1127 RVA: 0x0001EBD8 File Offset: 0x0001CDD8
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

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x0001EBEC File Offset: 0x0001CDEC
		// (set) Token: 0x06000469 RID: 1129 RVA: 0x0001EC04 File Offset: 0x0001CE04
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

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600046A RID: 1130 RVA: 0x0001EC18 File Offset: 0x0001CE18
		// (set) Token: 0x0600046B RID: 1131 RVA: 0x0001EC30 File Offset: 0x0001CE30
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

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x0001EC44 File Offset: 0x0001CE44
		// (set) Token: 0x0600046D RID: 1133 RVA: 0x0001EC5C File Offset: 0x0001CE5C
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

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x0001EC70 File Offset: 0x0001CE70
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

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x0001ECB4 File Offset: 0x0001CEB4
		// (set) Token: 0x06000470 RID: 1136 RVA: 0x0001ECCC File Offset: 0x0001CECC
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

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x0001ECE0 File Offset: 0x0001CEE0
		// (set) Token: 0x06000472 RID: 1138 RVA: 0x0001ECF8 File Offset: 0x0001CEF8
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

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x0001ED0C File Offset: 0x0001CF0C
		// (set) Token: 0x06000474 RID: 1140 RVA: 0x0001ED24 File Offset: 0x0001CF24
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

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x0001ED38 File Offset: 0x0001CF38
		// (set) Token: 0x06000476 RID: 1142 RVA: 0x0001ED50 File Offset: 0x0001CF50
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

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x0001ED64 File Offset: 0x0001CF64
		// (set) Token: 0x06000478 RID: 1144 RVA: 0x0001ED7C File Offset: 0x0001CF7C
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

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x0001ED90 File Offset: 0x0001CF90
		// (set) Token: 0x0600047A RID: 1146 RVA: 0x0001EDA8 File Offset: 0x0001CFA8
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

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x0001EDBC File Offset: 0x0001CFBC
		// (set) Token: 0x0600047C RID: 1148 RVA: 0x0001EDD4 File Offset: 0x0001CFD4
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

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x0001EDE8 File Offset: 0x0001CFE8
		// (set) Token: 0x0600047E RID: 1150 RVA: 0x0001EE00 File Offset: 0x0001D000
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

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x0001EE14 File Offset: 0x0001D014
		// (set) Token: 0x06000480 RID: 1152 RVA: 0x0001EE2C File Offset: 0x0001D02C
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

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x0001EE40 File Offset: 0x0001D040
		// (set) Token: 0x06000482 RID: 1154 RVA: 0x0001EE58 File Offset: 0x0001D058
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

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x0001EE6C File Offset: 0x0001D06C
		// (set) Token: 0x06000484 RID: 1156 RVA: 0x0001EE84 File Offset: 0x0001D084
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

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x0001EE98 File Offset: 0x0001D098
		// (set) Token: 0x06000486 RID: 1158 RVA: 0x0001EEB0 File Offset: 0x0001D0B0
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

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x0001EEC4 File Offset: 0x0001D0C4
		// (set) Token: 0x06000488 RID: 1160 RVA: 0x0001EEDC File Offset: 0x0001D0DC
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

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x0001EEF0 File Offset: 0x0001D0F0
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

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x0001EF30 File Offset: 0x0001D130
		public bool ComboIsTextBased
		{
			get
			{
				return this.control_code == 3 && (this.setting3 == -1 || this.setting3 == 1);
			}
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0001EF64 File Offset: 0x0001D164
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

		// Token: 0x0600048C RID: 1164 RVA: 0x0001F010 File Offset: 0x0001D210
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

		// Token: 0x0600048D RID: 1165 RVA: 0x0001F128 File Offset: 0x0001D328
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

		// Token: 0x0600048E RID: 1166 RVA: 0x0001F1BC File Offset: 0x0001D3BC
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
				bool flag9 = table.Columns.Contains("helptext");
				if (flag9)
				{
					this.helpText = (string)dr["helptext"];
				}
				else
				{
					this.helpText = "";
				}
				bool flag10 = table.Columns.Contains("mask");
				if (flag10)
				{
					this.mask = (string)dr["mask"];
				}
				else
				{
					this.mask = "";
				}
				bool flag11 = table.Columns.Contains("actionhandlers");
				if (flag11)
				{
					this.actionHandlers = (string)dr["actionhandlers"];
				}
				else
				{
					this.actionHandlers = "";
				}
				bool flag12 = table.Columns.Contains("defaultvaluestring");
				if (flag12)
				{
					this.defaultValueString = (string)dr["defaultvaluestring"];
				}
				else
				{
					this.defaultValueString = "";
				}
				bool flag13 = table.Columns.Contains("setting4string");
				if (flag13)
				{
					this.setting4String = (string)dr["setting4string"];
				}
				else
				{
					this.setting4String = "";
				}
				bool flag14 = table.Columns.Contains("helptextdisplaymethod");
				if (flag14)
				{
					this.helpTextDisplayMethod = (int)dr["helptextdisplaymethod"];
				}
				else
				{
					this.helpTextDisplayMethod = 1;
				}
				bool flag15 = table.Columns.Contains("setting4");
				if (flag15)
				{
					this.setting4 = (int)dr["setting4"];
				}
				else
				{
					this.setting4 = 0;
				}
				bool flag16 = table.Columns.Contains("fontsize");
				if (flag16)
				{
					this.fontSize = (int)dr["fontsize"];
				}
				else
				{
					this.fontSize = 0;
				}
				bool flag17 = table.Columns.Contains("enforce");
				if (flag17)
				{
					this.enforce = (int)dr["enforce"];
				}
				else
				{
					this.enforce = 0;
				}
				bool flag18 = table.Columns.Contains("enabled");
				if (flag18)
				{
					this.enabled = Convert.ToBoolean(dr["enabled"]);
				}
				else
				{
					this.enabled = true;
				}
				bool flag19 = table.Columns.Contains("readonly");
				if (flag19)
				{
					this.readOnly = Convert.ToBoolean(dr["readonly"]);
				}
				else
				{
					this.readOnly = false;
				}
				bool flag20 = table.Columns.Contains("hidecaption");
				if (flag20)
				{
					this.hideCaption = Convert.ToBoolean(dr["hidecaption"]);
				}
				else
				{
					this.hideCaption = false;
				}
				bool flag21 = table.Columns.Contains("dontwraptonextline");
				if (flag21)
				{
					this.dontWrapToNextLine = Convert.ToBoolean(dr["dontwraptonextline"]);
				}
				else
				{
					this.dontWrapToNextLine = false;
				}
				bool flag22 = table.Columns.Contains("accommodationid");
				if (flag22)
				{
					this.accommodationId = ((dr["accommodationid"] == DBNull.Value) ? 0 : ((int)dr["accommodationid"]));
				}
				bool flag23 = table.Columns.Contains("showonletter");
				if (flag23)
				{
					this.showOnLetter = ((dr["showonletter"] == DBNull.Value) ? 0 : ((int)dr["showonletter"]));
				}
				bool flag24 = table.Columns.Contains("longdescription");
				if (flag24)
				{
					this.extendedAccommodation_LongDescription = ((dr["longdescription"] == DBNull.Value) ? "" : ((string)dr["longdescription"]));
				}
				bool flag25 = table.Columns.Contains("shortcode");
				if (flag25)
				{
					this.extendedAccommodation_shortCode = ((dr["shortcode"] == DBNull.Value) ? "" : ((string)dr["shortcode"]));
				}
				bool flag26 = table.Columns.Contains("extratime");
				if (flag26)
				{
					this.extendedAccommodation_IsExtraTimeAccommodation = (dr["extratime"] != DBNull.Value && Convert.ToBoolean(dr["extratime"]));
				}
				bool flag27 = table.Columns.Contains("isalone");
				if (flag27)
				{
					this.extendedAccommodation_IsAloneAccommodation = (dr["isalone"] != DBNull.Value && Convert.ToBoolean(dr["isalone"]));
				}
				bool flag28 = table.Columns.Contains("needscomputer");
				if (flag28)
				{
					this.extendedAccommodation_IsComputerAccommodation = (dr["needscomputer"] != DBNull.Value && Convert.ToBoolean(dr["needscomputer"]));
				}
				bool flag29 = table.Columns.Contains("needsreaderscribe");
				if (flag29)
				{
					this.extendedAccommodation_IsReaderScribeAccommodation = (dr["needsreaderscribe"] != DBNull.Value && Convert.ToBoolean(dr["needsreaderscribe"]));
				}
				bool flag30 = table.Columns.Contains("isgroup");
				if (flag30)
				{
					this.extendedAccommodation_IsGroupAccommodation = (dr["isgroup"] != DBNull.Value && Convert.ToBoolean(dr["isgroup"]));
				}
				bool flag31 = table.Columns.Contains("other");
				if (flag31)
				{
					this.extendedAccommodation_IsOtherAccommodation = (dr["other"] != DBNull.Value && Convert.ToBoolean(dr["other"]));
				}
				bool flag32 = table.Columns.Contains("enlarged");
				if (flag32)
				{
					this.extendedAccommodation_IsEnlargedTextAccommodation = (dr["enlarged"] != DBNull.Value && Convert.ToBoolean(dr["enlarged"]));
				}
				bool flag33 = table.Columns.Contains("showonletter");
				if (flag33)
				{
					int num = (dr["showonletter"] == DBNull.Value) ? 0 : ((int)dr["showonletter"]);
					bool flag34 = (num & 1) == 1;
					if (flag34)
					{
						this.extendedAccommodation_group_prof = true;
					}
					bool flag35 = (num & 2) == 2;
					if (flag35)
					{
						this.extendedAccommodation_group_exam = true;
					}
					bool flag36 = (num & 4) == 4;
					if (flag36)
					{
						this.extendedAccommodation_group_other = true;
					}
				}
				bool flag37 = table.Columns.Contains("showonreport");
				if (flag37)
				{
					this.extendedAccommodation_group_report = (dr["showonreport"] != DBNull.Value && (int)dr["showonreport"] != 0);
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

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x0001FC58 File Offset: 0x0001DE58
		public bool IsComboBox
		{
			get
			{
				return this.control_code == 3;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x0001FC74 File Offset: 0x0001DE74
		public bool IsTextBox
		{
			get
			{
				return this.control_code == 1 || this.control_code == 11 || this.control_code == 300;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x0001FCAC File Offset: 0x0001DEAC
		public bool IsCheckBox
		{
			get
			{
				return this.control_code == 2;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x0001FCC8 File Offset: 0x0001DEC8
		public bool IsRadioButton
		{
			get
			{
				return this.control_code == 4;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x0001FCE4 File Offset: 0x0001DEE4
		public bool IsDate
		{
			get
			{
				return this.control_code == 6;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x0001FD00 File Offset: 0x0001DF00
		public bool IsLabel
		{
			get
			{
				return this.control_code == 5;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x0001FD1C File Offset: 0x0001DF1C
		public bool IsListView
		{
			get
			{
				return this.control_code == 10;
			}
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0001FD38 File Offset: 0x0001DF38
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

		// Token: 0x06000497 RID: 1175 RVA: 0x0001FDFC File Offset: 0x0001DFFC
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

		// Token: 0x06000498 RID: 1176 RVA: 0x0001FF7C File Offset: 0x0001E17C
		public static DataTable CreateControlsTable()
		{
			DataTable dataTable = new DataTable();
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
			return dataTable;
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0002015C File Offset: 0x0001E35C
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
			t.Rows.Add(dataRow);
		}

		// Token: 0x0400022A RID: 554
		public const int Enforce_Optional = 0;

		// Token: 0x0400022B RID: 555
		public const int Enforce_Warning = 1;

		// Token: 0x0400022C RID: 556
		public const int Enforce_Error = 2;

		// Token: 0x0400022D RID: 557
		private ModificationType howModified = ModificationType.Unchanged;

		// Token: 0x0400022E RID: 558
		private int control_id;

		// Token: 0x0400022F RID: 559
		private string control_caption;

		// Token: 0x04000230 RID: 560
		private int control_code;

		// Token: 0x04000231 RID: 561
		private int setting1;

		// Token: 0x04000232 RID: 562
		private int setting2;

		// Token: 0x04000233 RID: 563
		private int setting3;

		// Token: 0x04000234 RID: 564
		private int default_value;

		// Token: 0x04000235 RID: 565
		private string controlName;

		// Token: 0x04000236 RID: 566
		private string controlGroup;

		// Token: 0x04000237 RID: 567
		private string helpText;

		// Token: 0x04000238 RID: 568
		private string mask;

		// Token: 0x04000239 RID: 569
		private string actionHandlers;

		// Token: 0x0400023A RID: 570
		private string defaultValueString;

		// Token: 0x0400023B RID: 571
		private string setting4String;

		// Token: 0x0400023C RID: 572
		private int helpTextDisplayMethod;

		// Token: 0x0400023D RID: 573
		private int setting4;

		// Token: 0x0400023E RID: 574
		private int fontSize;

		// Token: 0x0400023F RID: 575
		private int enforce;

		// Token: 0x04000240 RID: 576
		private bool enabled;

		// Token: 0x04000241 RID: 577
		private bool readOnly;

		// Token: 0x04000242 RID: 578
		private bool hideCaption;

		// Token: 0x04000243 RID: 579
		private bool dontWrapToNextLine;

		// Token: 0x04000244 RID: 580
		private ScreenCollection screensIBelongTo;

		// Token: 0x04000245 RID: 581
		public ArrayList tagList;

		// Token: 0x04000246 RID: 582
		private int accommodationId = 0;

		// Token: 0x04000247 RID: 583
		private int showOnLetter = 0;

		// Token: 0x04000248 RID: 584
		private object tag;

		// Token: 0x04000249 RID: 585
		private DynamicControl associatedDynamicControl = null;

		// Token: 0x0400024A RID: 586
		private StringDictionary specialInstructionArgs = null;

		// Token: 0x0400024B RID: 587
		public ScreenCollection ScreensIBelongTo;

		// Token: 0x0400024C RID: 588
		public bool ExtendedAccommodation_SomethingChangedByUser = false;

		// Token: 0x0400024D RID: 589
		private string extendedAccommodation_LongDescription = "";

		// Token: 0x0400024E RID: 590
		private bool extendedAccommodation_group_prof;

		// Token: 0x0400024F RID: 591
		private bool extendedAccommodation_group_exam;

		// Token: 0x04000250 RID: 592
		private bool extendedAccommodation_group_other;

		// Token: 0x04000251 RID: 593
		private bool extendedAccommodation_group_report;

		// Token: 0x04000252 RID: 594
		private string extendedAccommodation_shortCode = "";

		// Token: 0x04000253 RID: 595
		private bool extendedAccommodation_IsExtraTimeAccommodation;

		// Token: 0x04000254 RID: 596
		private bool extendedAccommodation_IsAloneAccommodation;

		// Token: 0x04000255 RID: 597
		private bool extendedAccommodation_IsGroupAccommodation;

		// Token: 0x04000256 RID: 598
		private bool extendedAccommodation_IsComputerAccommodation;

		// Token: 0x04000257 RID: 599
		private bool extendedAccommodation_IsReaderScribeAccommodation;

		// Token: 0x04000258 RID: 600
		private bool extendedAccommodation_IsEnlargedTextAccommodation;

		// Token: 0x04000259 RID: 601
		private bool extendedAccommodation_IsOtherAccommodation;
	}
}
