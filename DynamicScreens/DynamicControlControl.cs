using System;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace DynamicScreens
{
	// Token: 0x0200001E RID: 30
	public class DynamicControlControl
	{
		// Token: 0x060001FF RID: 511 RVA: 0x0001914D File Offset: 0x0001814D
		public DynamicControlControl(int controlCode)
		{
			this.controlCode = controlCode;
			this.args = new DynamicControlParameterCollection();
			this.SetupArgs();
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000200 RID: 512 RVA: 0x00019174 File Offset: 0x00018174
		public int ControlCode
		{
			get
			{
				return this.controlCode;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000201 RID: 513 RVA: 0x0001918C File Offset: 0x0001818C
		public DynamicControlParameterCollection Args
		{
			get
			{
				return this.args;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000202 RID: 514 RVA: 0x000191A4 File Offset: 0x000181A4
		public string Title
		{
			get
			{
				return this.GetControlTitle();
			}
		}

		// Token: 0x06000203 RID: 515 RVA: 0x000191BC File Offset: 0x000181BC
		private string GetControlTitle()
		{
			return this.controlCode.GetDescription();
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000204 RID: 516 RVA: 0x000191DC File Offset: 0x000181DC
		public string Description
		{
			get
			{
				string result;
				if (Enum.IsDefined(typeof(eControlCode), this.controlCode))
				{
					switch (this.controlCode)
					{
					case 1:
						return "This can be a single-line or multi-line textbox with or without spell-check.";
					case 2:
						return "A checkbox - can be true (checked) or false (un-checked).";
					case 3:
						return "A drop list. This can be a flat list where the user can only choose from the provided list items, or it can allow users to either choose from the list or type their own value in.";
					case 5:
						return "A label - this is for display purposes only.";
					}
					result = "Unknown";
				}
				else
				{
					result = "Unknown";
				}
				return result;
			}
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00019264 File Offset: 0x00018264
		private void SetupArgs()
		{
			if (Enum.IsDefined(typeof(eControlCode), this.controlCode))
			{
				switch (this.controlCode)
				{
				case 1:
					this.args.Add("Number of lines for multiline textbox", null, DynamicControlSetting.Setting1, DynamicControlParameterDataType.Number);
					this.args.Add("Edit behaviour", null, DynamicControlSetting.DefaultValue, DynamicControlParameterDataType.Number);
					this.args.Add("Encrypted", null, DynamicControlSetting.Setting3, DynamicControlParameterDataType.Boolean_01);
					this.args.Add("Width in characters", null, DynamicControlSetting.Setting2, DynamicControlParameterDataType.Number);
					break;
				case 2:
					this.args.Add("Control to enable when checked", null, DynamicControlSetting.Setting1, DynamicControlParameterDataType.Number);
					this.args.Add("Control to force checked/unchecked", null, DynamicControlSetting.Setting2, DynamicControlParameterDataType.Number);
					break;
				case 3:
					this.args.Add("List", null, DynamicControlSetting.Setting1, DynamicControlParameterDataType.List);
					this.args.Add("Default list item", null, DynamicControlSetting.DefaultValue, DynamicControlParameterDataType.ListItem);
					this.args.Add("List Type", null, DynamicControlSetting.Setting2, DynamicControlParameterDataType.ListType);
					this.args.Add("Width (characters)", null, DynamicControlSetting.Setting4, DynamicControlParameterDataType.Number);
					break;
				case 5:
					this.args.Add("Text style", null, DynamicControlSetting.Setting1, DynamicControlParameterDataType.FontStyle);
					this.args.Add("Font size", null, DynamicControlSetting.DefaultValue, DynamicControlParameterDataType.Number);
					this.args.Add("AutoSize", null, DynamicControlSetting.Setting2, DynamicControlParameterDataType.Boolean_01);
					break;
				}
			}
		}

		// Token: 0x04000156 RID: 342
		private int controlCode;

		// Token: 0x04000157 RID: 343
		private DynamicControlParameterCollection args;
	}
}
