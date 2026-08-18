using System;
using System.ComponentModel;
using DynamicScreens.DynamicControlWrappers.TypeConverters;

namespace DynamicScreens.DynamicControlWrappers
{
	// Token: 0x02000004 RID: 4
	public class DynamicControlWrapper_MaskedTextBox : DynamicControlWrapper_Base
	{
		// Token: 0x06000050 RID: 80 RVA: 0x00002D25 File Offset: 0x00001D25
		public DynamicControlWrapper_MaskedTextBox(DynamicControl dynamicControl) : base(dynamicControl)
		{
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002D34 File Offset: 0x00001D34
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00002D4C File Offset: 0x00001D4C
		[Description("Indicates the list that will be available to the user.")]
		[Category("Design")]
		[TypeConverter(typeof(StringIntTypeConverter))]
		public DynamicControl List
		{
			get
			{
				return this.dynamicControl;
			}
			set
			{
				this.dynamicControl.Setting1 = value.Setting1;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002D64 File Offset: 0x00001D64
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00002D94 File Offset: 0x00001D94
		[Category("Design")]
		[Description("Change the name of the currently selected list here.")]
		public string ListName
		{
			get
			{
				DynamicListGroup selectedDynamicListGroup = this.GetSelectedDynamicListGroup();
				string result;
				if (selectedDynamicListGroup != null)
				{
					result = selectedDynamicListGroup.Description;
				}
				else
				{
					result = "{none}";
				}
				return result;
			}
			set
			{
				DynamicListGroup selectedDynamicListGroup = this.GetSelectedDynamicListGroup();
				if (selectedDynamicListGroup != null)
				{
					selectedDynamicListGroup.Description = value;
				}
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002DBC File Offset: 0x00001DBC
		private DynamicListGroup GetSelectedDynamicListGroup()
		{
			int listGroupId;
			if (this.dynamicControl.IsComboBox)
			{
				listGroupId = this.dynamicControl.Setting1;
			}
			else
			{
				listGroupId = -1;
			}
			DynamicControlWrapper_HelperClass dynamicControlWrapper_HelperClass = (DynamicControlWrapper_HelperClass)this.dynamicControl.Tag;
			return dynamicControlWrapper_HelperClass.ListGroups.FindListGroup(listGroupId);
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002E10 File Offset: 0x00001E10
		// (set) Token: 0x06000057 RID: 87 RVA: 0x00002E49 File Offset: 0x00001E49
		[Category("Design")]
		[Description("Change the list members / ordering here.")]
		public DynamicListGroup ListMembers
		{
			get
			{
				DynamicControlWrapper_HelperClass dynamicControlWrapper_HelperClass = (DynamicControlWrapper_HelperClass)this.dynamicControl.Tag;
				return dynamicControlWrapper_HelperClass.ListGroups.FindListGroup(this.dynamicControl.Setting1);
			}
			set
			{
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002E4C File Offset: 0x00001E4C
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00002E6C File Offset: 0x00001E6C
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

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002E84 File Offset: 0x00001E84
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00002EA1 File Offset: 0x00001EA1
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

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002EB4 File Offset: 0x00001EB4
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00002ED1 File Offset: 0x00001ED1
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

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002EE4 File Offset: 0x00001EE4
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00002F01 File Offset: 0x00001F01
		[Category("Behaviour")]
		[Description("A default value to use for new data.")]
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

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002F14 File Offset: 0x00001F14
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00002F34 File Offset: 0x00001F34
		[Description("Shows the textbox as a multi-select-enabled checklist.")]
		[Category("Behaviour")]
		public bool ShowAsMultiSelect
		{
			get
			{
				return this.dynamicControl.Setting4 == 1;
			}
			set
			{
				this.dynamicControl.Setting4 = (value ? 1 : 0);
			}
		}
	}
}
