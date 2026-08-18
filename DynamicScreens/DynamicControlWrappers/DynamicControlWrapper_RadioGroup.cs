using System;
using System.ComponentModel;
using AutoComboBox;
using DynamicScreens.DynamicControlWrappers.TypeConverters;

namespace DynamicScreens.DynamicControlWrappers
{
	// Token: 0x02000049 RID: 73
	public class DynamicControlWrapper_RadioGroup : DynamicControlWrapper_Base
	{
		// Token: 0x0600041E RID: 1054 RVA: 0x00037B0C File Offset: 0x00036B0C
		public DynamicControlWrapper_RadioGroup(DynamicControl dynamicControl) : base(dynamicControl)
		{
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x00037B18 File Offset: 0x00036B18
		// (set) Token: 0x06000420 RID: 1056 RVA: 0x00037B35 File Offset: 0x00036B35
		[Description("The number of columns to use when displaying the radio buttons.")]
		[Category("Display")]
		public int ColumnCount
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

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x00037B48 File Offset: 0x00036B48
		// (set) Token: 0x06000422 RID: 1058 RVA: 0x00037B65 File Offset: 0x00036B65
		[Category("Display")]
		[Description("How should the label be presented.")]
		public MyRadioGroup.DisplayFormat DisplayType
		{
			get
			{
				return (MyRadioGroup.DisplayFormat)this.dynamicControl.Setting3;
			}
			set
			{
				this.dynamicControl.Setting3 = (int)value;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x00037B78 File Offset: 0x00036B78
		// (set) Token: 0x06000424 RID: 1060 RVA: 0x00037B9C File Offset: 0x00036B9C
		[Description("Indicates the list of descriptions for each radio button item in this group.")]
		[TypeConverter(typeof(RuleConverter))]
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

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x00037BC4 File Offset: 0x00036BC4
		// (set) Token: 0x06000426 RID: 1062 RVA: 0x00037BE4 File Offset: 0x00036BE4
		[Description("Act as the primary item for a set of checkboxes to follow (no list required)")]
		[Category("Behaviour")]
		public bool ActAsPrimaryForMultipleCheckboxes
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

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x00037BFC File Offset: 0x00036BFC
		// (set) Token: 0x06000428 RID: 1064 RVA: 0x00037C19 File Offset: 0x00036C19
		[Category("Behaviour")]
		[Description("Default Selection LookupListId")]
		public int DefaultSelection
		{
			get
			{
				return this.dynamicControl.DefaultValue;
			}
			set
			{
				this.dynamicControl.DefaultValue = value;
			}
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00037C29 File Offset: 0x00036C29
		public override void SetDefaultValues(DynamicControl dc)
		{
			dc.Setting3 = 2;
			dc.Setting2 = 2;
		}
	}
}
