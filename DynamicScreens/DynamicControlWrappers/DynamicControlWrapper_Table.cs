using System;
using System.ComponentModel;
using DynamicScreens.DynamicControlWrappers.TypeConverters;

namespace DynamicScreens.DynamicControlWrappers
{
	// Token: 0x0200007B RID: 123
	public class DynamicControlWrapper_Table : DynamicControlWrapper_Base
	{
		// Token: 0x060005F4 RID: 1524 RVA: 0x00048513 File Offset: 0x00047513
		public DynamicControlWrapper_Table(DynamicControl dynamicControl) : base(dynamicControl)
		{
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x00048520 File Offset: 0x00047520
		// (set) Token: 0x060005F6 RID: 1526 RVA: 0x00048544 File Offset: 0x00047544
		[Description("Indicates the columns that will be used for the table.  Note that a date column is always provided by default as the column in the table (you don't need to specify it in this list).")]
		[Category("Design")]
		[TypeConverter(typeof(RuleConverter))]
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

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x0004856C File Offset: 0x0004756C
		// (set) Token: 0x060005F8 RID: 1528 RVA: 0x00048589 File Offset: 0x00047589
		[Category("Display")]
		[Description("Indicates the number of rows high.")]
		public int RowCount
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

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060005F9 RID: 1529 RVA: 0x0004859C File Offset: 0x0004759C
		// (set) Token: 0x060005FA RID: 1530 RVA: 0x000485BC File Offset: 0x000475BC
		[Description("Will the table show grid lines between rows and columns?")]
		[Category("Display")]
		public bool GridLines
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

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x000485D4 File Offset: 0x000475D4
		// (set) Token: 0x060005FC RID: 1532 RVA: 0x000485F1 File Offset: 0x000475F1
		[Description("Font size percent (eg. 50 means 4, 100 means 8, 200 means 16)")]
		[Category("Display")]
		public int FontSize
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

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060005FD RID: 1533 RVA: 0x00048604 File Offset: 0x00047604
		// (set) Token: 0x060005FE RID: 1534 RVA: 0x0004864C File Offset: 0x0004764C
		[Description("Can users delete an existing row?")]
		[Category("Behaviour")]
		public bool AllowedToDeleteRows
		{
			get
			{
				string specialInstructionStringValue = DynamicControl.GetSpecialInstructionStringValue(this.dynamicControl.ControlGroup, "nodeleting");
				return string.IsNullOrEmpty(specialInstructionStringValue) || !specialInstructionStringValue.Equals("1");
			}
			set
			{
				this.dynamicControl.ControlGroup = DynamicControl.SetSpecialInstructionStringValue(this.dynamicControl.ControlGroup, "nodeleting", value ? "" : "1");
			}
		}
	}
}
