using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;

namespace DynamicScreens
{
	// Token: 0x02000029 RID: 41
	public class DynamicControl
	{
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000249 RID: 585 RVA: 0x00019D78 File Offset: 0x00018D78
		// (set) Token: 0x0600024A RID: 586 RVA: 0x00019D8F File Offset: 0x00018D8F
		public string UniqueId { get; set; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600024B RID: 587 RVA: 0x00019D98 File Offset: 0x00018D98
		// (set) Token: 0x0600024C RID: 588 RVA: 0x00019DB0 File Offset: 0x00018DB0
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

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600024D RID: 589 RVA: 0x00019DC4 File Offset: 0x00018DC4
		// (set) Token: 0x0600024E RID: 590 RVA: 0x00019DDC File Offset: 0x00018DDC
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

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600024F RID: 591 RVA: 0x00019DE8 File Offset: 0x00018DE8
		public bool IsAccommodationControl
		{
			get
			{
				return this.accommodationId > 0;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000250 RID: 592 RVA: 0x00019E04 File Offset: 0x00018E04
		public Point Location
		{
			get
			{
				int num = this.control_caption.IndexOf('%');
				Point result;
				if (num > 0)
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

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000251 RID: 593 RVA: 0x00019E7C File Offset: 0x00018E7C
		public MeasurementUnit LocationUnit
		{
			get
			{
				return MeasurementUnit.Pixel;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000252 RID: 594 RVA: 0x00019E90 File Offset: 0x00018E90
		// (set) Token: 0x06000253 RID: 595 RVA: 0x00019EA8 File Offset: 0x00018EA8
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

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000254 RID: 596 RVA: 0x00019EB4 File Offset: 0x00018EB4
		// (set) Token: 0x06000255 RID: 597 RVA: 0x00019ECC File Offset: 0x00018ECC
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

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000256 RID: 598 RVA: 0x00019ED8 File Offset: 0x00018ED8
		// (set) Token: 0x06000257 RID: 599 RVA: 0x00019EF0 File Offset: 0x00018EF0
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

		// Token: 0x06000258 RID: 600 RVA: 0x00019EFA File Offset: 0x00018EFA
		public void SetControlCode(int newControlCode)
		{
			this.control_code = newControlCode;
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000259 RID: 601 RVA: 0x00019F04 File Offset: 0x00018F04
		public bool HasSpecialInstructions
		{
			get
			{
				return this.controlGroup.Length > 0;
			}
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00019F24 File Offset: 0x00018F24
		public string SpecialInstructionsNoNull(string key)
		{
			string text = this.SpecialInstructions(key);
			return (text == null) ? "" : text;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00019F4C File Offset: 0x00018F4C
		public string SpecialInstructions(string key)
		{
			string result;
			if (this.specialInstructionArgs != null)
			{
				result = this.specialInstructionArgs[key];
			}
			else if (this.controlGroup != null && this.controlGroup.Length > 0)
			{
				this.specialInstructionArgs = DynamicControl.ParseArgs(this.controlGroup);
				result = this.specialInstructionArgs[key];
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00019FBC File Offset: 0x00018FBC
		public void RemoveSpecialInstruction(string key)
		{
			if (this.specialInstructionArgs != null && this.specialInstructionArgs.ContainsKey(key))
			{
				this.specialInstructionArgs.Remove(key);
			}
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00019FF8 File Offset: 0x00018FF8
		public void SetSpecialInstruction(string key, string val)
		{
			if (this.specialInstructionArgs == null)
			{
				this.specialInstructionArgs = new StringDictionary();
			}
			if (this.specialInstructionArgs.ContainsKey(key))
			{
				this.specialInstructionArgs.Remove(key);
			}
			this.specialInstructionArgs.Add(key, val);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0001A054 File Offset: 0x00019054
		public static StringDictionary ParseArgs(string args)
		{
			StringDictionary stringDictionary = new StringDictionary();
			string[] array = DynamicControl.SplitStringIntoNEWLINE_delimitered_parts(args, true);
			foreach (string text in array)
			{
				int num = text.IndexOf('=');
				if (num > 0)
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

		// Token: 0x0600025F RID: 607 RVA: 0x0001A0F4 File Offset: 0x000190F4
		public static string[] SplitStringIntoNEWLINE_delimitered_parts(string s, bool excludeEmptyStrings)
		{
			string[] array = s.Split(Environment.NewLine.ToCharArray());
			if (excludeEmptyStrings)
			{
				ArrayList arrayList = new ArrayList();
				foreach (string text in array)
				{
					if (text.Trim().Length > 0)
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

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000260 RID: 608 RVA: 0x0001A1A4 File Offset: 0x000191A4
		// (set) Token: 0x06000261 RID: 609 RVA: 0x0001A1BC File Offset: 0x000191BC
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

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000262 RID: 610 RVA: 0x0001A1C8 File Offset: 0x000191C8
		// (set) Token: 0x06000263 RID: 611 RVA: 0x0001A1E0 File Offset: 0x000191E0
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

		// Token: 0x06000264 RID: 612 RVA: 0x0001A1F4 File Offset: 0x000191F4
		private void SetModified()
		{
			if (this.howModified == ModificationType.Unchanged)
			{
				this.howModified = ModificationType.Modified;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000265 RID: 613 RVA: 0x0001A21C File Offset: 0x0001921C
		public string FrenchControlCaptionForDisplay
		{
			get
			{
				int num = this.setting4String.IndexOf("__");
				string result;
				if (num >= 0)
				{
					result = ((num == 0) ? "" : this.setting4String.Substring(0, num));
				}
				else
				{
					num = this.setting4String.IndexOf("~~");
					if (num >= 0)
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

		// Token: 0x06000266 RID: 614 RVA: 0x0001A29C File Offset: 0x0001929C
		public static string GetControlCaptionForDisplay(string control_caption)
		{
			int num = control_caption.IndexOf("__");
			string result;
			if (num >= 0)
			{
				result = ((num == 0) ? "" : control_caption.Substring(0, num));
			}
			else
			{
				num = control_caption.IndexOf("~~");
				if (num >= 0)
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

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000267 RID: 615 RVA: 0x0001A304 File Offset: 0x00019304
		public bool IsValueEncrypted
		{
			get
			{
				bool result;
				if (this.control_code == 1 || this.control_code == 701)
				{
					result = (this.setting3 == 1);
				}
				else
				{
					result = ((this.control_code == 3 || this.control_code == 703) && this.setting3 == -1);
				}
				return result;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000268 RID: 616 RVA: 0x0001A370 File Offset: 0x00019370
		public string ControlCaptionAsColumnName
		{
			get
			{
				return Regex.Replace(this.control_caption, "[^0-9a-zA-Z\\._]", string.Empty);
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0001A398 File Offset: 0x00019398
		public string ControlCaptionForDisplay
		{
			get
			{
				return DynamicControl.GetControlCaptionForDisplay(this.control_caption);
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600026A RID: 618 RVA: 0x0001A3B8 File Offset: 0x000193B8
		public int ControlCode
		{
			get
			{
				return this.control_code;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600026B RID: 619 RVA: 0x0001A3D0 File Offset: 0x000193D0
		// (set) Token: 0x0600026C RID: 620 RVA: 0x0001A3E8 File Offset: 0x000193E8
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

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0001A3FC File Offset: 0x000193FC
		// (set) Token: 0x0600026E RID: 622 RVA: 0x0001A414 File Offset: 0x00019414
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

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600026F RID: 623 RVA: 0x0001A428 File Offset: 0x00019428
		// (set) Token: 0x06000270 RID: 624 RVA: 0x0001A440 File Offset: 0x00019440
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

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000271 RID: 625 RVA: 0x0001A454 File Offset: 0x00019454
		// (set) Token: 0x06000272 RID: 626 RVA: 0x0001A46C File Offset: 0x0001946C
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

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000273 RID: 627 RVA: 0x0001A480 File Offset: 0x00019480
		// (set) Token: 0x06000274 RID: 628 RVA: 0x0001A498 File Offset: 0x00019498
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

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0001A4AC File Offset: 0x000194AC
		// (set) Token: 0x06000276 RID: 630 RVA: 0x0001A4C4 File Offset: 0x000194C4
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

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000277 RID: 631 RVA: 0x0001A4D8 File Offset: 0x000194D8
		// (set) Token: 0x06000278 RID: 632 RVA: 0x0001A4F0 File Offset: 0x000194F0
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

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000279 RID: 633 RVA: 0x0001A504 File Offset: 0x00019504
		// (set) Token: 0x0600027A RID: 634 RVA: 0x0001A51C File Offset: 0x0001951C
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

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600027B RID: 635 RVA: 0x0001A530 File Offset: 0x00019530
		// (set) Token: 0x0600027C RID: 636 RVA: 0x0001A548 File Offset: 0x00019548
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

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600027D RID: 637 RVA: 0x0001A55C File Offset: 0x0001955C
		// (set) Token: 0x0600027E RID: 638 RVA: 0x0001A574 File Offset: 0x00019574
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

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0001A588 File Offset: 0x00019588
		// (set) Token: 0x06000280 RID: 640 RVA: 0x0001A5A0 File Offset: 0x000195A0
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

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000281 RID: 641 RVA: 0x0001A5B4 File Offset: 0x000195B4
		// (set) Token: 0x06000282 RID: 642 RVA: 0x0001A5CC File Offset: 0x000195CC
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

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000283 RID: 643 RVA: 0x0001A5E0 File Offset: 0x000195E0
		public int HelpTextDisplayMethod
		{
			get
			{
				return this.helpTextDisplayMethod;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000284 RID: 644 RVA: 0x0001A5F8 File Offset: 0x000195F8
		// (set) Token: 0x06000285 RID: 645 RVA: 0x0001A610 File Offset: 0x00019610
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

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000286 RID: 646 RVA: 0x0001A624 File Offset: 0x00019624
		public int FontSize
		{
			get
			{
				return this.fontSize;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000287 RID: 647 RVA: 0x0001A63C File Offset: 0x0001963C
		// (set) Token: 0x06000288 RID: 648 RVA: 0x0001A654 File Offset: 0x00019654
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

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000289 RID: 649 RVA: 0x0001A668 File Offset: 0x00019668
		// (set) Token: 0x0600028A RID: 650 RVA: 0x0001A680 File Offset: 0x00019680
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

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600028B RID: 651 RVA: 0x0001A694 File Offset: 0x00019694
		// (set) Token: 0x0600028C RID: 652 RVA: 0x0001A6AC File Offset: 0x000196AC
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

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0001A6C0 File Offset: 0x000196C0
		// (set) Token: 0x0600028E RID: 654 RVA: 0x0001A6D8 File Offset: 0x000196D8
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

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600028F RID: 655 RVA: 0x0001A6EC File Offset: 0x000196EC
		// (set) Token: 0x06000290 RID: 656 RVA: 0x0001A704 File Offset: 0x00019704
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

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000291 RID: 657 RVA: 0x0001A718 File Offset: 0x00019718
		public int ExtendedAccommodation_ShowOnLetter
		{
			get
			{
				int num = 0;
				if (this.extendedAccommodation_group_prof)
				{
					num++;
				}
				if (this.extendedAccommodation_group_exam)
				{
					num += 2;
				}
				if (this.extendedAccommodation_group_other)
				{
					num += 4;
				}
				return num;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000292 RID: 658 RVA: 0x0001A760 File Offset: 0x00019760
		// (set) Token: 0x06000293 RID: 659 RVA: 0x0001A778 File Offset: 0x00019778
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

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000294 RID: 660 RVA: 0x0001A78C File Offset: 0x0001978C
		// (set) Token: 0x06000295 RID: 661 RVA: 0x0001A7A4 File Offset: 0x000197A4
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

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000296 RID: 662 RVA: 0x0001A7B8 File Offset: 0x000197B8
		// (set) Token: 0x06000297 RID: 663 RVA: 0x0001A7D0 File Offset: 0x000197D0
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

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000298 RID: 664 RVA: 0x0001A7E4 File Offset: 0x000197E4
		// (set) Token: 0x06000299 RID: 665 RVA: 0x0001A7FC File Offset: 0x000197FC
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

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600029A RID: 666 RVA: 0x0001A810 File Offset: 0x00019810
		// (set) Token: 0x0600029B RID: 667 RVA: 0x0001A828 File Offset: 0x00019828
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

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600029C RID: 668 RVA: 0x0001A83C File Offset: 0x0001983C
		// (set) Token: 0x0600029D RID: 669 RVA: 0x0001A854 File Offset: 0x00019854
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

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600029E RID: 670 RVA: 0x0001A868 File Offset: 0x00019868
		// (set) Token: 0x0600029F RID: 671 RVA: 0x0001A880 File Offset: 0x00019880
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

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x0001A894 File Offset: 0x00019894
		// (set) Token: 0x060002A1 RID: 673 RVA: 0x0001A8AC File Offset: 0x000198AC
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

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0001A8C0 File Offset: 0x000198C0
		// (set) Token: 0x060002A3 RID: 675 RVA: 0x0001A8D8 File Offset: 0x000198D8
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

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x0001A8EC File Offset: 0x000198EC
		// (set) Token: 0x060002A5 RID: 677 RVA: 0x0001A904 File Offset: 0x00019904
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

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x0001A918 File Offset: 0x00019918
		// (set) Token: 0x060002A7 RID: 679 RVA: 0x0001A930 File Offset: 0x00019930
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

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x0001A944 File Offset: 0x00019944
		// (set) Token: 0x060002A9 RID: 681 RVA: 0x0001A95C File Offset: 0x0001995C
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

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060002AA RID: 682 RVA: 0x0001A970 File Offset: 0x00019970
		// (set) Token: 0x060002AB RID: 683 RVA: 0x0001A988 File Offset: 0x00019988
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

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060002AC RID: 684 RVA: 0x0001A99C File Offset: 0x0001999C
		public string Name
		{
			get
			{
				string result;
				if (this.controlName == null || this.controlName.Length < 1)
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

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0001A9E0 File Offset: 0x000199E0
		public bool ComboIsTextBased
		{
			get
			{
				return this.control_code == 3 && (this.setting3 == -1 || this.setting3 == 1);
			}
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0001AA14 File Offset: 0x00019A14
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
			if (control_id < 0)
			{
				this.howModified = ModificationType.Added;
			}
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0001AAC4 File Offset: 0x00019AC4
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

		// Token: 0x060002B0 RID: 688 RVA: 0x0001ABDC File Offset: 0x00019BDC
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

		// Token: 0x060002B1 RID: 689 RVA: 0x0001AC70 File Offset: 0x00019C70
		public DynamicControl(DataRow dr)
		{
			this.ScreensIBelongTo = new ScreenCollection();
			if (dr != null && dr.RowState != DataRowState.Deleted)
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
					if (table.Columns.Contains(text) && dr[text] == DBNull.Value)
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
				foreach (string text in array3)
				{
					if (table.Columns.Contains(text) && dr[text] == DBNull.Value)
					{
						dr[text] = 0;
					}
				}
				if (table.Columns.Contains("helptextdisplaymethod") && dr["helptextdisplaymethod"] == DBNull.Value)
				{
					dr["helptextdisplaymethod"] = 1;
				}
				string[] array4 = new string[]
				{
					"readonly",
					"hidecaption",
					"dontwraptonextline"
				};
				foreach (string text in array4)
				{
					if (table.Columns.Contains(text) && dr[text] == DBNull.Value)
					{
						dr[text] = false;
					}
				}
				if (table.Columns.Contains("enabled") && dr["enabled"] == DBNull.Value)
				{
					dr["enabled"] = true;
				}
				if (table.Columns.Contains("controlname"))
				{
					this.controlName = (string)dr["controlname"];
				}
				else
				{
					this.controlName = "";
				}
				if (table.Columns.Contains("controlgroup"))
				{
					this.controlGroup = (string)dr["controlgroup"];
				}
				else
				{
					this.controlGroup = "";
				}
				if (table.Columns.Contains("controlgroupoverride"))
				{
					this.controlGroupOverride = ((dr["controlgroupoverride"] == DBNull.Value) ? "" : ((string)dr["controlgroupoverride"]));
				}
				if (table.Columns.Contains("helptext"))
				{
					this.helpText = (string)dr["helptext"];
				}
				else
				{
					this.helpText = "";
				}
				if (table.Columns.Contains("mask"))
				{
					this.mask = (string)dr["mask"];
				}
				else
				{
					this.mask = "";
				}
				if (table.Columns.Contains("actionhandlers"))
				{
					this.actionHandlers = (string)dr["actionhandlers"];
				}
				else
				{
					this.actionHandlers = "";
				}
				if (table.Columns.Contains("defaultvaluestring"))
				{
					this.defaultValueString = (string)dr["defaultvaluestring"];
				}
				else
				{
					this.defaultValueString = "";
				}
				if (table.Columns.Contains("setting4string"))
				{
					this.setting4String = (string)dr["setting4string"];
				}
				else
				{
					this.setting4String = "";
				}
				if (table.Columns.Contains("helptextdisplaymethod"))
				{
					this.helpTextDisplayMethod = (int)dr["helptextdisplaymethod"];
				}
				else
				{
					this.helpTextDisplayMethod = 1;
				}
				if (table.Columns.Contains("setting4"))
				{
					this.setting4 = (int)dr["setting4"];
				}
				else
				{
					this.setting4 = 0;
				}
				if (table.Columns.Contains("fontsize"))
				{
					this.fontSize = (int)dr["fontsize"];
				}
				else
				{
					this.fontSize = 0;
				}
				if (table.Columns.Contains("enforce"))
				{
					this.enforce = (int)dr["enforce"];
				}
				else
				{
					this.enforce = 0;
				}
				if (table.Columns.Contains("enabled"))
				{
					this.enabled = Convert.ToBoolean(dr["enabled"]);
				}
				else
				{
					this.enabled = true;
				}
				if (table.Columns.Contains("readonly"))
				{
					this.readOnly = Convert.ToBoolean(dr["readonly"]);
				}
				else
				{
					this.readOnly = false;
				}
				if (table.Columns.Contains("hidecaption"))
				{
					this.hideCaption = Convert.ToBoolean(dr["hidecaption"]);
				}
				else
				{
					this.hideCaption = false;
				}
				if (table.Columns.Contains("dontwraptonextline"))
				{
					this.dontWrapToNextLine = Convert.ToBoolean(dr["dontwraptonextline"]);
				}
				else
				{
					this.dontWrapToNextLine = false;
				}
				if (table.Columns.Contains("accommodationid"))
				{
					this.accommodationId = ((dr["accommodationid"] == DBNull.Value) ? 0 : ((int)dr["accommodationid"]));
				}
				if (table.Columns.Contains("showonletter"))
				{
					this.showOnLetter = ((dr["showonletter"] == DBNull.Value) ? 0 : ((int)dr["showonletter"]));
				}
				if (table.Columns.Contains("longdescription"))
				{
					this.extendedAccommodation_LongDescription = ((dr["longdescription"] == DBNull.Value) ? "" : ((string)dr["longdescription"]));
				}
				if (table.Columns.Contains("shortcode"))
				{
					this.extendedAccommodation_shortCode = ((dr["shortcode"] == DBNull.Value) ? "" : ((string)dr["shortcode"]));
				}
				if (table.Columns.Contains("extratime"))
				{
					this.extendedAccommodation_IsExtraTimeAccommodation = (dr["extratime"] != DBNull.Value && Convert.ToBoolean(dr["extratime"]));
				}
				if (table.Columns.Contains("isalone"))
				{
					this.extendedAccommodation_IsAloneAccommodation = (dr["isalone"] != DBNull.Value && Convert.ToBoolean(dr["isalone"]));
				}
				if (table.Columns.Contains("needscomputer"))
				{
					this.extendedAccommodation_IsComputerAccommodation = (dr["needscomputer"] != DBNull.Value && Convert.ToBoolean(dr["needscomputer"]));
				}
				if (table.Columns.Contains("needsreaderscribe"))
				{
					this.extendedAccommodation_IsReaderScribeAccommodation = (dr["needsreaderscribe"] != DBNull.Value && Convert.ToBoolean(dr["needsreaderscribe"]));
				}
				if (table.Columns.Contains("isgroup"))
				{
					this.extendedAccommodation_IsGroupAccommodation = (dr["isgroup"] != DBNull.Value && Convert.ToBoolean(dr["isgroup"]));
				}
				if (table.Columns.Contains("other"))
				{
					this.extendedAccommodation_IsOtherAccommodation = (dr["other"] != DBNull.Value && Convert.ToBoolean(dr["other"]));
				}
				if (table.Columns.Contains("enlarged"))
				{
					this.extendedAccommodation_IsEnlargedTextAccommodation = (dr["enlarged"] != DBNull.Value && Convert.ToBoolean(dr["enlarged"]));
				}
				if (table.Columns.Contains("showonletter"))
				{
					int num = (dr["showonletter"] == DBNull.Value) ? 0 : ((int)dr["showonletter"]);
					if ((num & 1) == 1)
					{
						this.extendedAccommodation_group_prof = true;
					}
					if ((num & 2) == 2)
					{
						this.extendedAccommodation_group_exam = true;
					}
					if ((num & 4) == 4)
					{
						this.extendedAccommodation_group_other = true;
					}
				}
				if (table.Columns.Contains("showonreport"))
				{
					this.extendedAccommodation_group_report = (dr["showonreport"] != DBNull.Value && (int)dr["showonreport"] != 0);
				}
				if (table.Columns.Contains("uniqueid"))
				{
					this.UniqueId = dr["uniqueid"].ToString();
				}
				if (table.Columns.Contains("specialcontroltype"))
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

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x0001B858 File Offset: 0x0001A858
		public bool IsComboBox
		{
			get
			{
				return this.control_code == 3;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x0001B874 File Offset: 0x0001A874
		public bool IsTextBox
		{
			get
			{
				return this.control_code == 1 || this.control_code == 11 || this.control_code == 300;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0001B8AC File Offset: 0x0001A8AC
		public bool IsCheckBox
		{
			get
			{
				return this.control_code == 2;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x0001B8C8 File Offset: 0x0001A8C8
		public bool IsRadioButton
		{
			get
			{
				return this.control_code == 4;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0001B8E4 File Offset: 0x0001A8E4
		public bool IsDate
		{
			get
			{
				return this.control_code == 6;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x0001B900 File Offset: 0x0001A900
		public bool IsLabel
		{
			get
			{
				return this.control_code == 5;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x0001B91C File Offset: 0x0001A91C
		public bool IsListView
		{
			get
			{
				return this.control_code == 10;
			}
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0001B938 File Offset: 0x0001A938
		public static string GetSpecialInstructionStringValue(string allSpecials, string name)
		{
			string result;
			if (allSpecials == null)
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
					if (num > 0)
					{
						if (text.Substring(0, num).ToLower().Trim().Equals(value))
						{
							num++;
							if (num < text.Length)
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

		// Token: 0x060002BA RID: 698 RVA: 0x0001BA08 File Offset: 0x0001AA08
		public static string SetSpecialInstructionStringValue(string allSpecials, string name, string value)
		{
			string result;
			if (string.IsNullOrEmpty(allSpecials))
			{
				result = (string.IsNullOrEmpty(value) ? "" : (name + "=" + value));
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder();
				string[] array = DynamicControl.SplitStringIntoNEWLINE_delimitered_parts(allSpecials, true);
				string value2 = name.ToLower().Trim();
				bool flag = false;
				foreach (string text in array)
				{
					int num = text.IndexOf('=');
					if (num > 0)
					{
						if (text.Substring(0, num).ToLower().Trim().Equals(value2))
						{
							flag = true;
							if (!string.IsNullOrEmpty(value))
							{
								if (stringBuilder.Length > 0)
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
							if (stringBuilder.Length > 0)
							{
								stringBuilder.Append(Environment.NewLine);
							}
							stringBuilder.Append(text);
						}
					}
				}
				if (!flag)
				{
					if (stringBuilder.Length > 0)
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

		// Token: 0x060002BB RID: 699 RVA: 0x0001BB98 File Offset: 0x0001AB98
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
			dataTable.Columns.Add("controlgroupoverride");
			dataTable.Columns.Add("uniqueid");
			dataTable.Columns.Add("specialcontroltype", typeof(int));
			return dataTable;
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0001BDB4 File Offset: 0x0001ADB4
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
			if (t.Columns.Contains("controlgroupoverride"))
			{
				if (string.IsNullOrEmpty(this.controlGroupOverride))
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

		// Token: 0x04000167 RID: 359
		public const int Enforce_Optional = 0;

		// Token: 0x04000168 RID: 360
		public const int Enforce_Warning = 1;

		// Token: 0x04000169 RID: 361
		public const int Enforce_Error = 2;

		// Token: 0x0400016A RID: 362
		private ModificationType howModified = ModificationType.Unchanged;

		// Token: 0x0400016B RID: 363
		private int control_id;

		// Token: 0x0400016C RID: 364
		private string control_caption;

		// Token: 0x0400016D RID: 365
		private int control_code;

		// Token: 0x0400016E RID: 366
		private int setting1;

		// Token: 0x0400016F RID: 367
		private int setting2;

		// Token: 0x04000170 RID: 368
		private int setting3;

		// Token: 0x04000171 RID: 369
		private int default_value;

		// Token: 0x04000172 RID: 370
		private string controlName;

		// Token: 0x04000173 RID: 371
		private string controlGroup;

		// Token: 0x04000174 RID: 372
		private string helpText;

		// Token: 0x04000175 RID: 373
		private string mask;

		// Token: 0x04000176 RID: 374
		private string actionHandlers;

		// Token: 0x04000177 RID: 375
		private string defaultValueString;

		// Token: 0x04000178 RID: 376
		private string setting4String;

		// Token: 0x04000179 RID: 377
		private int helpTextDisplayMethod;

		// Token: 0x0400017A RID: 378
		private int setting4;

		// Token: 0x0400017B RID: 379
		private int fontSize;

		// Token: 0x0400017C RID: 380
		private int enforce;

		// Token: 0x0400017D RID: 381
		private bool enabled;

		// Token: 0x0400017E RID: 382
		private bool readOnly;

		// Token: 0x0400017F RID: 383
		private bool hideCaption;

		// Token: 0x04000180 RID: 384
		private bool dontWrapToNextLine;

		// Token: 0x04000181 RID: 385
		private ScreenCollection screensIBelongTo;

		// Token: 0x04000182 RID: 386
		public ArrayList tagList;

		// Token: 0x04000183 RID: 387
		private string controlGroupOverride;

		// Token: 0x04000184 RID: 388
		private int accommodationId = 0;

		// Token: 0x04000185 RID: 389
		private int showOnLetter = 0;

		// Token: 0x04000186 RID: 390
		private int _specialControlType;

		// Token: 0x04000187 RID: 391
		private object tag;

		// Token: 0x04000188 RID: 392
		private DynamicControl associatedDynamicControl = null;

		// Token: 0x04000189 RID: 393
		private StringDictionary specialInstructionArgs = null;

		// Token: 0x0400018A RID: 394
		public ScreenCollection ScreensIBelongTo;

		// Token: 0x0400018B RID: 395
		public bool ExtendedAccommodation_SomethingChangedByUser = false;

		// Token: 0x0400018C RID: 396
		private string extendedAccommodation_LongDescription = "";

		// Token: 0x0400018D RID: 397
		private bool extendedAccommodation_group_prof;

		// Token: 0x0400018E RID: 398
		private bool extendedAccommodation_group_exam;

		// Token: 0x0400018F RID: 399
		private bool extendedAccommodation_group_other;

		// Token: 0x04000190 RID: 400
		private bool extendedAccommodation_group_report;

		// Token: 0x04000191 RID: 401
		private string extendedAccommodation_shortCode = "";

		// Token: 0x04000192 RID: 402
		private bool extendedAccommodation_IsExtraTimeAccommodation;

		// Token: 0x04000193 RID: 403
		private bool extendedAccommodation_IsAloneAccommodation;

		// Token: 0x04000194 RID: 404
		private bool extendedAccommodation_IsGroupAccommodation;

		// Token: 0x04000195 RID: 405
		private bool extendedAccommodation_IsComputerAccommodation;

		// Token: 0x04000196 RID: 406
		private bool extendedAccommodation_IsReaderScribeAccommodation;

		// Token: 0x04000197 RID: 407
		private bool extendedAccommodation_IsEnlargedTextAccommodation;

		// Token: 0x04000198 RID: 408
		private bool extendedAccommodation_IsOtherAccommodation;
	}
}
