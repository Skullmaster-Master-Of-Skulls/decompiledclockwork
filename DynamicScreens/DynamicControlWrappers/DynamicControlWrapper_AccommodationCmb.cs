using System;
using System.ComponentModel;
using System.Windows.Forms;
using DynamicScreens.DynamicControlWrappers.TypeConverters;

namespace DynamicScreens.DynamicControlWrappers
{
	// Token: 0x02000005 RID: 5
	public class DynamicControlWrapper_AccommodationCmb : DynamicControlWrapper_Base
	{
		// Token: 0x06000062 RID: 98 RVA: 0x00002F4A File Offset: 0x00001F4A
		public DynamicControlWrapper_AccommodationCmb(DynamicControl dynamicControl) : base(dynamicControl)
		{
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002F58 File Offset: 0x00001F58
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00002F78 File Offset: 0x00001F78
		[Description("Indent (number of pixels to pad on the left of the control)")]
		[Category("Display")]
		public int Indent
		{
			get
			{
				return this.dynamicControl.DefaultValue >> 1;
			}
			set
			{
				int num = this.dynamicControl.DefaultValue & 1;
				this.dynamicControl.DefaultValue = (value << 1) + num;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002FA8 File Offset: 0x00001FA8
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00002FC8 File Offset: 0x00001FC8
		[TypeConverter(typeof(DropListStyleEnumConverter))]
		[Description("Indicates whether the user can only choose from what is within the list, or if they are able to enter items not already in the list.")]
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

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00003028 File Offset: 0x00002028
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00003045 File Offset: 0x00002045
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

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00003058 File Offset: 0x00002058
		// (set) Token: 0x0600006A RID: 106 RVA: 0x0000307C File Offset: 0x0000207C
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
	}
}
