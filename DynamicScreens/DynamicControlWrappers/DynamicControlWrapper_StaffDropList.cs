using System;
using System.ComponentModel;
using DynamicScreens.DynamicControlWrappers.TypeConverters;

namespace DynamicScreens.DynamicControlWrappers
{
	// Token: 0x02000072 RID: 114
	public class DynamicControlWrapper_StaffDropList : DynamicControlWrapper_Base
	{
		// Token: 0x06000595 RID: 1429 RVA: 0x00042E15 File Offset: 0x00041E15
		public DynamicControlWrapper_StaffDropList(DynamicControl dynamicControl) : base(dynamicControl)
		{
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000596 RID: 1430 RVA: 0x00042E24 File Offset: 0x00041E24
		// (set) Token: 0x06000597 RID: 1431 RVA: 0x00042E63 File Offset: 0x00041E63
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

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x00042E74 File Offset: 0x00041E74
		// (set) Token: 0x06000599 RID: 1433 RVA: 0x00042E9F File Offset: 0x00041E9F
		[Category("Behaviour")]
		[Description("Default Value")]
		public StaffDropListDefaultValue DefaultSelection
		{
			get
			{
				int defaultValue = this.dynamicControl.DefaultValue;
				StaffDropListDefaultValue result;
				if (defaultValue != -2)
				{
					result = StaffDropListDefaultValue.none;
				}
				else
				{
					result = StaffDropListDefaultValue.Logged_in_user;
				}
				return result;
			}
			set
			{
				this.dynamicControl.DefaultValue = (int)value;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x00042EB0 File Offset: 0x00041EB0
		// (set) Token: 0x0600059B RID: 1435 RVA: 0x00042ED4 File Offset: 0x00041ED4
		[Description("What group of staff/clients/other will be provided by this drop-list?")]
		[Category("Design")]
		[TypeConverter(typeof(ClockWorkUserGroupIdConverter))]
		public string Group
		{
			get
			{
				int setting = this.dynamicControl.Setting1;
				return HE_GlobalVars_ClockWorkGroupList.FindDisplayString(setting);
			}
			set
			{
				int setting;
				string text;
				HE_GlobalVars_ClockWorkGroupList.GetLookupGroupIdAndDescriptionFromDisplayString(value, out setting, out text);
				this.dynamicControl.Setting1 = setting;
			}
		}
	}
}
