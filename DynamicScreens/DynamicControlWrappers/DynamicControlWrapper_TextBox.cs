using System;
using System.ComponentModel;

namespace DynamicScreens.DynamicControlWrappers
{
	// Token: 0x02000064 RID: 100
	public class DynamicControlWrapper_TextBox : DynamicControlWrapper_Base
	{
		// Token: 0x06000507 RID: 1287 RVA: 0x000416A8 File Offset: 0x000406A8
		public DynamicControlWrapper_TextBox(DynamicControl dynamicControl) : base(dynamicControl)
		{
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000508 RID: 1288 RVA: 0x000416B4 File Offset: 0x000406B4
		// (set) Token: 0x06000509 RID: 1289 RVA: 0x000416E2 File Offset: 0x000406E2
		[Category("Display")]
		[Description("Indicates the number of rows this textbox should contain.  Use -1 to indicate it should fill it's container vertically.")]
		public int MultilineCount
		{
			get
			{
				return (this.dynamicControl.Setting1 <= 1) ? 1 : this.dynamicControl.Setting1;
			}
			set
			{
				this.dynamicControl.Setting1 = ((value > 1) ? value : 0);
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x0600050A RID: 1290 RVA: 0x000416FC File Offset: 0x000406FC
		// (set) Token: 0x0600050B RID: 1291 RVA: 0x0004173B File Offset: 0x0004073B
		[Category("Display")]
		[Description("Indicates the placement of the associated label.")]
		public eLabelOrientation LabelOrientation
		{
			get
			{
				int setting = this.dynamicControl.Setting4;
				eLabelOrientation result;
				if (Enum.IsDefined(typeof(eLabelOrientation), setting))
				{
					result = (eLabelOrientation)setting;
				}
				else
				{
					result = eLabelOrientation.LabelLeft;
				}
				return result;
			}
			set
			{
				this.dynamicControl.Setting4 = (int)value;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x0004174C File Offset: 0x0004074C
		// (set) Token: 0x0600050D RID: 1293 RVA: 0x0004176C File Offset: 0x0004076C
		[Category("Design")]
		[Description("Indicates whether the data for this textbox is encrypted.")]
		public bool Encrypted
		{
			get
			{
				return this.dynamicControl.Setting3 == 1;
			}
			set
			{
				this.dynamicControl.Setting3 = (value ? 1 : 0);
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x00041784 File Offset: 0x00040784
		// (set) Token: 0x0600050F RID: 1295 RVA: 0x000417B0 File Offset: 0x000407B0
		[Description("Indicates whether the textbox is formatted for currency.")]
		[Category("Design")]
		public bool IsCurrency
		{
			get
			{
				return this.dynamicControl.Mask.Trim().Equals("$");
			}
			set
			{
				this.dynamicControl.Mask = (value ? "$" : (this.dynamicControl.Mask.Equals("$") ? "" : this.dynamicControl.Mask));
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000510 RID: 1296 RVA: 0x00041800 File Offset: 0x00040800
		// (set) Token: 0x06000511 RID: 1297 RVA: 0x0004181D File Offset: 0x0004081D
		[Category("Display")]
		[Description("Indicates the number of characters wide this textbox should be.")]
		public int CharacterWidth
		{
			get
			{
				return this.dynamicControl.Setting2;
			}
			set
			{
				this.dynamicControl.Setting2 = value;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x00041830 File Offset: 0x00040830
		// (set) Token: 0x06000513 RID: 1299 RVA: 0x00041854 File Offset: 0x00040854
		[Category("Per Appointment Behaviour")]
		[Description("If set, this control will show information about the underlying appointment and does not load or save data like a normal control.")]
		public SpecialPerAppointmentControlType SpecialPerAppointmentControlType
		{
			get
			{
				return this.GetSpecialPerAppointmentControlType(this.dynamicControl.ControlCaption);
			}
			set
			{
				SpecialPerAppointmentControlType specialPerAppointmentControlType = this.GetSpecialPerAppointmentControlType(this.dynamicControl.ControlCaption);
				if (specialPerAppointmentControlType != value)
				{
					int length = this.dynamicControl.ControlCaption.IndexOf("~~");
					if (value == SpecialPerAppointmentControlType.None)
					{
						this.dynamicControl.ControlCaption = this.dynamicControl.ControlCaption.Substring(0, length);
					}
					else
					{
						this.dynamicControl.ControlCaption = value.ToString() + "~~" + value.ToString();
						this.dynamicControl.ReadOnly = true;
					}
				}
			}
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00041900 File Offset: 0x00040900
		private SpecialPerAppointmentControlType GetSpecialPerAppointmentControlType(string controlCaption)
		{
			string[] names = Enum.GetNames(typeof(SpecialPerAppointmentControlType));
			SpecialPerAppointmentControlType[] array = (SpecialPerAppointmentControlType[])Enum.GetValues(typeof(SpecialPerAppointmentControlType));
			SpecialPerAppointmentControlType result = SpecialPerAppointmentControlType.None;
			string value = controlCaption.ToLower();
			for (int i = 0; i < names.Length; i++)
			{
				string text = names[i].ToLower();
				text = text + "~~" + text;
				if (text.Equals(value))
				{
					result = array[i];
					break;
				}
			}
			return result;
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x00041994 File Offset: 0x00040994
		// (set) Token: 0x06000516 RID: 1302 RVA: 0x000419EC File Offset: 0x000409EC
		[Category("Display")]
		[Description("Horizontal alignment of label text.")]
		public DynamicControlWrapper_Label.LabelAlign HorizontalAlign
		{
			get
			{
				string specialInstructionStringValue = DynamicControl.GetSpecialInstructionStringValue(this.dynamicControl.ControlGroup, "align");
				DynamicControlWrapper_Label.LabelAlign result;
				if (specialInstructionStringValue.Equals("right"))
				{
					result = DynamicControlWrapper_Label.LabelAlign.right;
				}
				else if (specialInstructionStringValue.Equals("center"))
				{
					result = DynamicControlWrapper_Label.LabelAlign.center;
				}
				else
				{
					result = DynamicControlWrapper_Label.LabelAlign.left;
				}
				return result;
			}
			set
			{
				string value2;
				if (value == DynamicControlWrapper_Label.LabelAlign.right)
				{
					value2 = "right";
				}
				else if (value == DynamicControlWrapper_Label.LabelAlign.center)
				{
					value2 = "center";
				}
				else
				{
					value2 = "";
				}
				this.dynamicControl.ControlGroup = DynamicControl.SetSpecialInstructionStringValue(this.dynamicControl.ControlGroup, "align", value2);
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x00041A48 File Offset: 0x00040A48
		// (set) Token: 0x06000518 RID: 1304 RVA: 0x00041A94 File Offset: 0x00040A94
		[Description("Use if another combobox control will be controlling the text mask for this textbox.  Example: ON:LLLL 0000 0000;PQ:000 000 LL0;Other:0L0 L0L 0L0")]
		[Category("MaskRules")]
		public string MaskRules
		{
			get
			{
				string result;
				if (this.dynamicControl.HasSpecialInstructions)
				{
					string text = this.dynamicControl.SpecialInstructions("maskrules");
					result = ((text == null) ? "" : text);
				}
				else
				{
					result = "";
				}
				return result;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					this.dynamicControl.RemoveSpecialInstruction("maskrules");
				}
				else
				{
					this.dynamicControl.SetSpecialInstruction("maskrules", value);
				}
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000519 RID: 1305 RVA: 0x00041AD8 File Offset: 0x00040AD8
		// (set) Token: 0x0600051A RID: 1306 RVA: 0x00041B30 File Offset: 0x00040B30
		[Category("MaskRules")]
		[Description("The combobox on this form that will be controlling the mask for this textbox.")]
		public int MaskRulesComboBox
		{
			get
			{
				if (this.dynamicControl.HasSpecialInstructions)
				{
					string text = this.dynamicControl.SpecialInstructions("masktype");
					if (!string.IsNullOrEmpty(text))
					{
						int result;
						if (int.TryParse(text, out result))
						{
							return result;
						}
					}
				}
				return 0;
			}
			set
			{
				if (value > 0)
				{
					this.dynamicControl.RemoveSpecialInstruction("masktype");
				}
				else
				{
					this.dynamicControl.SetSpecialInstruction("masktype", value.ToString());
				}
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600051B RID: 1307 RVA: 0x00041B78 File Offset: 0x00040B78
		// (set) Token: 0x0600051C RID: 1308 RVA: 0x00041BEC File Offset: 0x00040BEC
		[Description("TextBox text character casing (upper case only, lower case only, or normal (both upper and lower case allowed)")]
		[Category("Display")]
		public eCharacterCasing CharacterCasing
		{
			get
			{
				if (this.dynamicControl.HasSpecialInstructions)
				{
					string value = this.dynamicControl.SpecialInstructions("casing") ?? "";
					if (Enum.IsDefined(typeof(eCharacterCasing), value))
					{
						return (eCharacterCasing)Enum.Parse(typeof(eCharacterCasing), value);
					}
				}
				return eCharacterCasing.Normal;
			}
			set
			{
				if (value == eCharacterCasing.Normal)
				{
					this.dynamicControl.RemoveSpecialInstruction("casing");
				}
				else
				{
					this.dynamicControl.SetSpecialInstruction("casing", value.ToString());
				}
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x00041C34 File Offset: 0x00040C34
		// (set) Token: 0x0600051E RID: 1310 RVA: 0x00041C51 File Offset: 0x00040C51
		[Description("A default value to use for new data.")]
		[Category("Behaviour")]
		public string DefaultValue
		{
			get
			{
				return this.dynamicControl.DefaultValueString;
			}
			set
			{
				this.dynamicControl.DefaultValueString = value;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x00041C64 File Offset: 0x00040C64
		// (set) Token: 0x06000520 RID: 1312 RVA: 0x00041C81 File Offset: 0x00040C81
		[Description("The text masking to use.")]
		[Category("Behaviour")]
		public string TextMask
		{
			get
			{
				return this.dynamicControl.Mask;
			}
			set
			{
				this.dynamicControl.Mask = value;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x00041C94 File Offset: 0x00040C94
		// (set) Token: 0x06000522 RID: 1314 RVA: 0x00041CB1 File Offset: 0x00040CB1
		[Category("Behaviour")]
		[Description("How will users will be able to enter text and modify text.")]
		public TextBoxEnterModifyBehaviour TextBoxEnterModifyBehaviour
		{
			get
			{
				return (TextBoxEnterModifyBehaviour)this.dynamicControl.DefaultValue;
			}
			set
			{
				this.dynamicControl.DefaultValue = (int)value;
			}
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00041CC1 File Offset: 0x00040CC1
		public override void SetDefaultValues(DynamicControl dc)
		{
			dc.Setting3 = 1;
		}
	}
}
