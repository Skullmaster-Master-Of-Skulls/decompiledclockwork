using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using TechnoPro.Common.Public.Entities.Adapters;

namespace DynamicScreens.DynamicControlWrappers
{
	// Token: 0x02000003 RID: 3
	public class DynamicControlWrapper_Base : ICustomTypeDescriptor
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002220 File Offset: 0x00001220
		// (set) Token: 0x0600000B RID: 11 RVA: 0x0000223D File Offset: 0x0000123D
		[Category("Behaviour")]
		[Description("Additional Instructions")]
		[Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
		public string AdditionalInstructions
		{
			get
			{
				return this.dynamicControl.ControlGroup;
			}
			set
			{
				this.dynamicControl.ControlGroup = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002250 File Offset: 0x00001250
		// (set) Token: 0x0600000D RID: 13 RVA: 0x0000226D File Offset: 0x0000126D
		[Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
		[Description("Additional Instructions Override")]
		[Category("Behaviour")]
		public string AdditionalInstructionsOverride
		{
			get
			{
				return this.dynamicControl.ControlGroupOverride;
			}
			set
			{
				this.dynamicControl.ControlGroupOverride = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002280 File Offset: 0x00001280
		// (set) Token: 0x0600000F RID: 15 RVA: 0x0000229D File Offset: 0x0000129D
		[Category("Display")]
		[Description("The text that will appear on the control for display purposes.")]
		public string ControlCaption
		{
			get
			{
				return this.dynamicControl.ControlCaption;
			}
			set
			{
				this.dynamicControl.ControlCaption = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000022B0 File Offset: 0x000012B0
		// (set) Token: 0x06000011 RID: 17 RVA: 0x000022CD File Offset: 0x000012CD
		[Category("Display")]
		[Description("The french version of the control caption.")]
		public string ControlCaptionFrench
		{
			get
			{
				return this.dynamicControl.Setting4String;
			}
			set
			{
				this.dynamicControl.Setting4String = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000022E0 File Offset: 0x000012E0
		// (set) Token: 0x06000013 RID: 19 RVA: 0x000022FD File Offset: 0x000012FD
		[Category("Behaviour")]
		[Description("Indicates whether the control is visible.")]
		public bool Visible
		{
			get
			{
				return this.dynamicControl.Enabled;
			}
			set
			{
				this.dynamicControl.Enabled = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002310 File Offset: 0x00001310
		// (set) Token: 0x06000015 RID: 21 RVA: 0x0000232D File Offset: 0x0000132D
		[Description("Indicates whether the control data can be modified.")]
		[Category("Behaviour")]
		public bool ReadOnly
		{
			get
			{
				return this.dynamicControl.ReadOnly;
			}
			set
			{
				this.dynamicControl.ReadOnly = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002340 File Offset: 0x00001340
		// (set) Token: 0x06000017 RID: 23 RVA: 0x0000235D File Offset: 0x0000135D
		[Category("Display")]
		[Description("Indicates whether to hide the ControlCaption or display it.")]
		public bool HideCaption
		{
			get
			{
				return this.dynamicControl.HideCaption;
			}
			set
			{
				this.dynamicControl.HideCaption = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002370 File Offset: 0x00001370
		// (set) Token: 0x06000019 RID: 25 RVA: 0x0000238D File Offset: 0x0000138D
		[Category("Display")]
		[Description("Normally the flow of control layout goes to the next vertical line; setting this to true will force the next control to appear to the right of this control instead of below it.")]
		public virtual bool DontWrapToNextLine
		{
			get
			{
				return this.dynamicControl.DontWrapToNextLine;
			}
			set
			{
				this.dynamicControl.DontWrapToNextLine = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000023A0 File Offset: 0x000013A0
		// (set) Token: 0x0600001B RID: 27 RVA: 0x000023BD File Offset: 0x000013BD
		[Description("Some additional help text to describe what this field is for or how it should be used.  The message will appear as a pop-up if the user holds their mouse over this control.")]
		[Category("Display")]
		[Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
		public virtual string HelpText
		{
			get
			{
				return this.dynamicControl.HelpText;
			}
			set
			{
				this.dynamicControl.HelpText = value;
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000023D0 File Offset: 0x000013D0
		public static string[] StringToStringArray(string s)
		{
			return s.Split(Environment.NewLine.ToCharArray());
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000023F4 File Offset: 0x000013F4
		public static string StringArrayToString(string[] ss)
		{
			string text = "";
			for (int i = 0; i < ss.Length; i++)
			{
				if (i > 0)
				{
					text += Environment.NewLine;
				}
				text += ss[i];
			}
			return text;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002444 File Offset: 0x00001444
		// (set) Token: 0x0600001F RID: 31 RVA: 0x00002461 File Offset: 0x00001461
		[Category("Behaviour")]
		[Description("Is this field optional, requested or required.")]
		public EnforceType EnforceType
		{
			get
			{
				return (EnforceType)this.dynamicControl.Enforce;
			}
			set
			{
				this.dynamicControl.Enforce = (int)value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002474 File Offset: 0x00001474
		[Description("Indicates the type of data this control stores.")]
		[Category("Design")]
		public string FieldType
		{
			get
			{
				return this.dynamicControl.ControlCode.GetDescription();
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002498 File Offset: 0x00001498
		[Category("Design")]
		[Description("Indicates the unique id number associated with this control.")]
		public int ControlId
		{
			get
			{
				return this.dynamicControl.ControlId;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000024B8 File Offset: 0x000014B8
		[Description("Indicates the type of control.")]
		[Category("Design")]
		public string ControlType
		{
			get
			{
				return DynamicScreen.GetControlNameByControlCode(this.dynamicControl.ControlCode) + " (" + this.dynamicControl.ControlCode.ToString() + ")";
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000024FC File Offset: 0x000014FC
		// (set) Token: 0x06000024 RID: 36 RVA: 0x00002519 File Offset: 0x00001519
		[Category("Design")]
		[Description("Indicates the unique name associated with this control.")]
		public string Name
		{
			get
			{
				return this.dynamicControl.ControlName;
			}
			set
			{
				this.dynamicControl.ControlName = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000025 RID: 37 RVA: 0x0000252C File Offset: 0x0000152C
		// (set) Token: 0x06000026 RID: 38 RVA: 0x00002549 File Offset: 0x00001549
		[Description("This text will show for the instructor and student on the LOA or online, instead of the ControlCaption for this field.")]
		[Category("Extended Accommodation Info")]
		public string AccommodationLongDescription
		{
			get
			{
				return this.dynamicControl.ExtendedAccommodation_LongDescription;
			}
			set
			{
				this.dynamicControl.ExtendedAccommodation_LongDescription = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000027 RID: 39 RVA: 0x0000255C File Offset: 0x0000155C
		// (set) Token: 0x06000028 RID: 40 RVA: 0x00002579 File Offset: 0x00001579
		[Category("Extended Accommodation Info")]
		[Description("The group this accommodation belongs to.")]
		public string AccommodationGroup
		{
			get
			{
				return this.dynamicControl.ExtendedAccommodation_shortCode;
			}
			set
			{
				this.dynamicControl.ExtendedAccommodation_shortCode = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000029 RID: 41 RVA: 0x0000258C File Offset: 0x0000158C
		// (set) Token: 0x0600002A RID: 42 RVA: 0x000025A9 File Offset: 0x000015A9
		[Category("Extended Accommodation Info")]
		[Description("If yes, this accommodation will appear by default in the instructor section on the LOA.")]
		public bool GroupInstructor
		{
			get
			{
				return this.dynamicControl.ExtendedAccommodation_group_prof;
			}
			set
			{
				this.dynamicControl.ExtendedAccommodation_group_prof = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000025BC File Offset: 0x000015BC
		// (set) Token: 0x0600002C RID: 44 RVA: 0x000025D9 File Offset: 0x000015D9
		[Description("If yes, this accommodation will appear by default in the test/exam section on the LOA.")]
		[Category("Extended Accommodation Info")]
		public bool GroupTestExam
		{
			get
			{
				return this.dynamicControl.ExtendedAccommodation_group_exam;
			}
			set
			{
				this.dynamicControl.ExtendedAccommodation_group_exam = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000025EC File Offset: 0x000015EC
		// (set) Token: 0x0600002E RID: 46 RVA: 0x00002609 File Offset: 0x00001609
		[Category("Extended Accommodation Info")]
		[Description("If yes, this accommodation will appear by default in the 'other' section on the LOA, if it exists.  This can also be used to group accommodations for custom reports.")]
		public bool GroupOther
		{
			get
			{
				return this.dynamicControl.ExtendedAccommodation_group_other;
			}
			set
			{
				this.dynamicControl.ExtendedAccommodation_group_other = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600002F RID: 47 RVA: 0x0000261C File Offset: 0x0000161C
		// (set) Token: 0x06000030 RID: 48 RVA: 0x00002639 File Offset: 0x00001639
		[Description("If yes, this accommodation will appear by default in the 'report' section on the LOA, if it exists.  This can also be used to group accommodations for custom reports.")]
		[Category("Extended Accommodation Info")]
		public bool GroupReport
		{
			get
			{
				return this.dynamicControl.ExtendedAccommodation_group_report;
			}
			set
			{
				this.dynamicControl.ExtendedAccommodation_group_report = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000031 RID: 49 RVA: 0x0000264C File Offset: 0x0000164C
		// (set) Token: 0x06000032 RID: 50 RVA: 0x00002669 File Offset: 0x00001669
		[Category("Extended Accommodation Info")]
		[Description("Does this accommodation indicate additional time for tests and exams?")]
		public bool IsExtraTimeAccommodation
		{
			get
			{
				return this.dynamicControl.ExtendedAccommodation_IsExtraTimeAccommodation;
			}
			set
			{
				this.dynamicControl.ExtendedAccommodation_IsExtraTimeAccommodation = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000033 RID: 51 RVA: 0x0000267C File Offset: 0x0000167C
		// (set) Token: 0x06000034 RID: 52 RVA: 0x00002699 File Offset: 0x00001699
		[Description("Does this accommodation indicate that the student requires a private room to write tests and exams?")]
		[Category("Extended Accommodation Info")]
		public bool IsAloneAccommodation
		{
			get
			{
				return this.dynamicControl.ExtendedAccommodation_IsAloneAccommodation;
			}
			set
			{
				this.dynamicControl.ExtendedAccommodation_IsAloneAccommodation = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000035 RID: 53 RVA: 0x000026AC File Offset: 0x000016AC
		// (set) Token: 0x06000036 RID: 54 RVA: 0x000026C9 File Offset: 0x000016C9
		[Category("Extended Accommodation Info")]
		[Description("Does this accommodation indicate that the student requires a small group room to write tests and exams?")]
		public bool IsGroupAccommodation
		{
			get
			{
				return this.dynamicControl.ExtendedAccommodation_IsGroupAccommodation;
			}
			set
			{
				this.dynamicControl.ExtendedAccommodation_IsGroupAccommodation = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000037 RID: 55 RVA: 0x000026DC File Offset: 0x000016DC
		// (set) Token: 0x06000038 RID: 56 RVA: 0x000026F9 File Offset: 0x000016F9
		[Category("Extended Accommodation Info")]
		[Description("Does this accommodation indicate that the student requires a computer to write tests and exams?")]
		public bool IsComputerAccommodation
		{
			get
			{
				return this.dynamicControl.ExtendedAccommodation_IsComputerAccommodation;
			}
			set
			{
				this.dynamicControl.ExtendedAccommodation_IsComputerAccommodation = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000039 RID: 57 RVA: 0x0000270C File Offset: 0x0000170C
		// (set) Token: 0x0600003A RID: 58 RVA: 0x00002729 File Offset: 0x00001729
		[Description("Does this accommodation indicate that the student requires a reader and/or scribe to write tests and exams?")]
		[Category("Extended Accommodation Info")]
		public bool IsReaderScribeAccommodation
		{
			get
			{
				return this.dynamicControl.ExtendedAccommodation_IsReaderScribeAccommodation;
			}
			set
			{
				this.dynamicControl.ExtendedAccommodation_IsReaderScribeAccommodation = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600003B RID: 59 RVA: 0x0000273C File Offset: 0x0000173C
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00002759 File Offset: 0x00001759
		[Category("Extended Accommodation Info")]
		[Description("Does this accommodation indicate that the student requires the test / exam font to be enlarged?")]
		public bool IsEnlargedTextAccommodation
		{
			get
			{
				return this.dynamicControl.ExtendedAccommodation_IsEnlargedTextAccommodation;
			}
			set
			{
				this.dynamicControl.ExtendedAccommodation_IsEnlargedTextAccommodation = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600003D RID: 61 RVA: 0x0000276C File Offset: 0x0000176C
		// (set) Token: 0x0600003E RID: 62 RVA: 0x00002789 File Offset: 0x00001789
		[Description("Does this accommodation indicate an 'other' field (ie. the disability advisor can type in something not covered by the accommodation checkboxes)?")]
		[Category("Extended Accommodation Info")]
		public bool IsOtherAccommodation
		{
			get
			{
				return this.dynamicControl.ExtendedAccommodation_IsOtherAccommodation;
			}
			set
			{
				this.dynamicControl.ExtendedAccommodation_IsOtherAccommodation = value;
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002799 File Offset: 0x00001799
		public DynamicControlWrapper_Base(DynamicControl dynamicControl)
		{
			this.dynamicControl = dynamicControl;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000027AB File Offset: 0x000017AB
		public virtual void SetDefaultValues(DynamicControl dc)
		{
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000027B0 File Offset: 0x000017B0
		public static DynamicControlWrapper_Base CreateWrapper(DynamicControl dynamicControl)
		{
			int controlCode = dynamicControl.ControlCode;
			if (controlCode <= 500)
			{
				if (controlCode <= 50)
				{
					switch (controlCode)
					{
					case 1:
						return new DynamicControlWrapper_TextBox(dynamicControl);
					case 2:
						return new DynamicControlWrapper_Checkbox(dynamicControl);
					case 3:
						return new DynamicControlWrapper_DropList(dynamicControl);
					case 4:
					case 7:
					case 11:
					case 12:
					case 15:
					case 16:
					case 17:
					case 18:
					case 19:
					case 22:
					case 23:
					case 24:
						break;
					case 5:
						return new DynamicControlWrapper_Label(dynamicControl);
					case 6:
						return new DynamicControlWrapper_Date(dynamicControl);
					case 8:
						return new DynamicControlWrapper_HRule(dynamicControl);
					case 9:
						return new DynamicControlWrapper_BlankSpace(dynamicControl);
					case 10:
						return new DynamicControlWrapper_Table(dynamicControl);
					case 13:
						return new DynamicControlWrapper_Base(dynamicControl);
					case 14:
						return new DynamicControlWrapper_RadioGroup(dynamicControl);
					case 20:
						return new DynamicControlWrapper_FileTable(dynamicControl);
					case 21:
						return new DynamicControlWrapper_Picture(dynamicControl);
					case 25:
						return new DynamicControlWrapper_DynamicTable(dynamicControl);
					default:
						switch (controlCode)
						{
						case 30:
							return new DynamicControlWrapper_PanelStart(dynamicControl);
						case 31:
							break;
						case 32:
							return new DynamicControlWrapper_TabControl(dynamicControl);
						case 33:
							return new DynamicControlWrapper_TabPage(dynamicControl);
						default:
							if (controlCode == 50)
							{
								return new DynamicControlWrapper_ColumnBreak(dynamicControl);
							}
							break;
						}
						break;
					}
				}
				else
				{
					if (controlCode == 100)
					{
						return new DynamicControlWrapper_StaffDropList(dynamicControl);
					}
					switch (controlCode)
					{
					case 300:
						return new DynamicControlWrapper_MaskedTextBox(dynamicControl);
					case 301:
						return new DynamicControlWrapper_ListSelect(dynamicControl);
					default:
						if (controlCode == 500)
						{
							return new DynamicControlWrapper_MultiCheckbox(dynamicControl);
						}
						break;
					}
				}
			}
			else if (controlCode <= 600)
			{
				if (controlCode == 510)
				{
					return new DynamicControlWrapper_MultiCheckboxTextbox(dynamicControl);
				}
				if (controlCode == 520)
				{
					return new DynamicControlWrapper_MultiCheckboxDroplist(dynamicControl);
				}
				if (controlCode == 600)
				{
					return new DynamicControlWrapper_RichTextbox(dynamicControl);
				}
			}
			else
			{
				if (controlCode == 620)
				{
					return new DynamicControlWrapper_MultiLineTextBox(dynamicControl);
				}
				switch (controlCode)
				{
				case 700:
					return new DynamicControlWrapper_AccommodationChk(dynamicControl);
				case 701:
					return new DynamicControlWrapper_AccommodationTxt(dynamicControl);
				case 702:
					return new DynamicControlWrapper_AccommodationDtp(dynamicControl);
				case 703:
					return new DynamicControlWrapper_AccommodationCmb(dynamicControl);
				default:
					switch (controlCode)
					{
					case 800:
						return new DynamicControlWrapper_FormSettings(dynamicControl);
					case 801:
						return new DynamicControlWrapper_DynamicControlsChooser(dynamicControl);
					case 802:
						return new DynamicControlWrapper_MultiDatabaseItemChooser(dynamicControl);
					case 803:
						return new DynamicControlWrapper_InfoBox(dynamicControl);
					case 804:
						return new DynamicControlWrapper_CalcButton(dynamicControl);
					case 805:
						return new DynamicControlWrapper_PMTable(dynamicControl);
					case 806:
						return new DynamicControlWrapper_CaseComboBox(dynamicControl);
					case 807:
						return new DynamicControlWrapper_EmailHistory(dynamicControl);
					case 808:
						return new DynamicControlWrapper_AppointmentHistory(dynamicControl);
					}
					break;
				}
			}
			return new DynamicControlWrapper_Base(dynamicControl);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002AC8 File Offset: 0x00001AC8
		public AttributeCollection GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this, true);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002AE4 File Offset: 0x00001AE4
		public string GetClassName()
		{
			return TypeDescriptor.GetClassName(this, true);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002B00 File Offset: 0x00001B00
		public string GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002B1C File Offset: 0x00001B1C
		public TypeConverter GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002B38 File Offset: 0x00001B38
		public EventDescriptor GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002B54 File Offset: 0x00001B54
		public PropertyDescriptor GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002B70 File Offset: 0x00001B70
		public object GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002B8C File Offset: 0x00001B8C
		public EventDescriptorCollection GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002BA8 File Offset: 0x00001BA8
		public EventDescriptorCollection GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002BC4 File Offset: 0x00001BC4
		public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this, attributes, true);
			return this.FilterProperties(properties);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002BE8 File Offset: 0x00001BE8
		public PropertyDescriptorCollection GetProperties()
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this, true);
			return this.FilterProperties(properties);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002C0C File Offset: 0x00001C0C
		private PropertyDescriptorCollection FilterProperties(PropertyDescriptorCollection pdc)
		{
			PropertyDescriptorCollection propertyDescriptorCollection = new PropertyDescriptorCollection(new PropertyDescriptor[0]);
			foreach (object obj in pdc)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (DynamicControlWrapper_Base.ShowExtendedAccommodationInfo || !propertyDescriptor.Category.Equals("Extended Accommodation Info"))
				{
					if (DynamicControlWrapper_Base.ShowExtendedAccommodationInfo || !propertyDescriptor.Category.Equals("Accommodation specific"))
					{
						if (DynamicControlWrapper_Base.ShowPerAppointmentInfo || !(propertyDescriptor.Category == "Per Appointment Behaviour"))
						{
							propertyDescriptorCollection.Add(propertyDescriptor);
						}
					}
				}
			}
			return propertyDescriptorCollection;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002D04 File Offset: 0x00001D04
		public object GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		// Token: 0x04000001 RID: 1
		public DynamicControl dynamicControl;

		// Token: 0x04000002 RID: 2
		public static bool ShowExtendedAccommodationInfo = true;

		// Token: 0x04000003 RID: 3
		public static bool ShowPerAppointmentInfo = true;
	}
}
