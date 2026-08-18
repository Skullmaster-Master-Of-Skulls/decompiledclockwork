using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Windows.Forms;
using DynamicScreens.DynamicControlWrappers.TypeConverters;

namespace DynamicScreens.DynamicControlWrappers
{
	// Token: 0x02000068 RID: 104
	public class DynamicControlWrapper_DropList : DynamicControlWrapper_Base
	{
		// Token: 0x0600053F RID: 1343 RVA: 0x0004207F File Offset: 0x0004107F
		public DynamicControlWrapper_DropList(DynamicControl dynamicControl) : base(dynamicControl)
		{
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000540 RID: 1344 RVA: 0x0004208C File Offset: 0x0004108C
		// (set) Token: 0x06000541 RID: 1345 RVA: 0x000420F4 File Offset: 0x000410F4
		[Category("Display")]
		[Description("Indicates the placement of the associated label.")]
		public eLabelOrientation LabelOrientation
		{
			get
			{
				string specialInstructionStringValue = DynamicControl.GetSpecialInstructionStringValue(this.dynamicControl.ControlGroup, "labelorientation");
				eLabelOrientation result;
				int num;
				if (string.IsNullOrEmpty(specialInstructionStringValue))
				{
					result = eLabelOrientation.LabelLeft;
				}
				else if (!int.TryParse(specialInstructionStringValue, out num))
				{
					result = eLabelOrientation.LabelLeft;
				}
				else if (!Enum.IsDefined(typeof(eLabelOrientation), num))
				{
					result = eLabelOrientation.LabelLeft;
				}
				else
				{
					result = (eLabelOrientation)num;
				}
				return result;
			}
			set
			{
				DynamicControl dynamicControl = this.dynamicControl;
				string controlGroup = this.dynamicControl.ControlGroup;
				string name = "labelorientation";
				string value2;
				if (value <= eLabelOrientation.LabelLeft)
				{
					value2 = "";
				}
				else
				{
					int num = (int)value;
					value2 = num.ToString();
				}
				dynamicControl.ControlGroup = DynamicControl.SetSpecialInstructionStringValue(controlGroup, name, value2);
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x00042138 File Offset: 0x00041138
		// (set) Token: 0x06000543 RID: 1347 RVA: 0x00042158 File Offset: 0x00041158
		[Description("Indicates whether the user can only choose from what is within the list, or if they are able to enter items not already in the list.")]
		[TypeConverter(typeof(DropListStyleEnumConverter))]
		[Category("Design")]
		public DropListBehaviour DropListStyle
		{
			get
			{
				return (DropListBehaviour)this.dynamicControl.Setting3;
			}
			set
			{
				if (this.dynamicControl.ControlId < 0)
				{
					this.dynamicControl.Setting3 = (int)value;
				}
				else
				{
					DynamicControlWrapper_HelperClass dynamicControlWrapper_HelperClass = (DynamicControlWrapper_HelperClass)this.dynamicControl.Tag;
					MessageBox.Show("Any existing data will not be affected by changing this value.  You should use the functions in the 'Functions' menu at the top to convert existing data to the new type.");
					this.dynamicControl.Setting3 = (int)value;
				}
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x000421B8 File Offset: 0x000411B8
		// (set) Token: 0x06000545 RID: 1349 RVA: 0x000421D5 File Offset: 0x000411D5
		[Description("Indicates the number of characters wide this control will be when displayed.")]
		[Category("Display")]
		public int CharacterWidth
		{
			get
			{
				return this.dynamicControl.Setting4;
			}
			set
			{
				this.dynamicControl.Setting4 = value;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x000421E8 File Offset: 0x000411E8
		// (set) Token: 0x06000547 RID: 1351 RVA: 0x00042236 File Offset: 0x00041236
		[Category("Behaviour")]
		[Description("If greater than zero, this is the controlid of the field that will automatically be set to the 'alternate value' of a selected drop list item.  You can set the 'alternate value' by double-clicking the list to edit, then double-clicking a list item to edit.")]
		public int ControlToPlaceSelectedAlternateValueIn
		{
			get
			{
				string specialInstructionStringValue = DynamicControl.GetSpecialInstructionStringValue(this.dynamicControl.ControlGroup, "valuecid");
				int result;
				int num;
				if (string.IsNullOrEmpty(specialInstructionStringValue))
				{
					result = 0;
				}
				else if (int.TryParse(specialInstructionStringValue, out num))
				{
					result = num;
				}
				else
				{
					result = 0;
				}
				return result;
			}
			set
			{
				this.dynamicControl.ControlGroup = DynamicControl.SetSpecialInstructionStringValue(this.dynamicControl.ControlGroup, "valuecid", (value > 0) ? value.ToString() : "");
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x0004226C File Offset: 0x0004126C
		// (set) Token: 0x06000549 RID: 1353 RVA: 0x00042290 File Offset: 0x00041290
		[TypeConverter(typeof(RuleConverter))]
		[Description("Indicates the list that will be available to the user.")]
		[Category("Design")]
		public string List
		{
			get
			{
				int setting = this.dynamicControl.Setting1;
				return HE_GlobalVars.FindDisplayString(setting);
			}
			set
			{
				int setting;
				string text;
				HE_GlobalVars.GetLookupGroupIdAndDescriptionFromDisplayString(value, out setting, out text);
				this.dynamicControl.Setting1 = setting;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x000422B8 File Offset: 0x000412B8
		// (set) Token: 0x0600054B RID: 1355 RVA: 0x000422DF File Offset: 0x000412DF
		[Category("Design")]
		[Description("Indicates the sql code to use to load the contents of this drop list.")]
		[Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
		public string Sql
		{
			get
			{
				return DynamicControl.GetSpecialInstructionStringValue(this.dynamicControl.ControlGroup, "sql");
			}
			set
			{
				this.dynamicControl.ControlGroup = DynamicControl.SetSpecialInstructionStringValue(this.dynamicControl.ControlGroup, "sql", value);
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x00042304 File Offset: 0x00041304
		// (set) Token: 0x0600054D RID: 1357 RVA: 0x00042350 File Offset: 0x00041350
		[Description("If true, this will allow the user to move through the items in the drop list using the scroll wheel on their mouse.  This is turned off by default so that users do not accidently change a drop list value by hitting the scroll wheel by mistake.")]
		[Category("Behaviour")]
		public bool UseMouseScrollWheel
		{
			get
			{
				string specialInstructionStringValue = DynamicControl.GetSpecialInstructionStringValue(this.dynamicControl.ControlGroup, "usemousewheel");
				bool flag;
				return !string.IsNullOrEmpty(specialInstructionStringValue) && bool.TryParse(specialInstructionStringValue, out flag) && flag;
			}
			set
			{
				string value2 = value ? value.ToString() : "";
				this.dynamicControl.ControlGroup = DynamicControl.SetSpecialInstructionStringValue(this.dynamicControl.ControlGroup, "usemousewheel", value2);
			}
		}
	}
}
