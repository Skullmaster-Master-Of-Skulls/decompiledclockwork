using System;
using System.ComponentModel;

namespace DynamicScreens.DynamicControlWrappers
{
	// Token: 0x0200007D RID: 125
	public class DynamicControlWrapper_MultiDatabaseItemChooser : DynamicControlWrapper_Base
	{
		// Token: 0x060005FF RID: 1535 RVA: 0x0004867F File Offset: 0x0004767F
		public DynamicControlWrapper_MultiDatabaseItemChooser(DynamicControl dynamicControl) : base(dynamicControl)
		{
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000600 RID: 1536 RVA: 0x0004868C File Offset: 0x0004768C
		// (set) Token: 0x06000601 RID: 1537 RVA: 0x000486AC File Offset: 0x000476AC
		[Category("Behaviour")]
		[Description("Single selection only")]
		public bool SingleSelectionOnly
		{
			get
			{
				return this.dynamicControl.Setting2 == 1;
			}
			set
			{
				this.dynamicControl.Setting2 = (value ? 1 : 0);
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000602 RID: 1538 RVA: 0x000486C4 File Offset: 0x000476C4
		// (set) Token: 0x06000603 RID: 1539 RVA: 0x000486E1 File Offset: 0x000476E1
		[Category("Display")]
		[Description("Height (use 0 for default height)")]
		public int Height
		{
			get
			{
				return this.dynamicControl.Setting1;
			}
			set
			{
				this.dynamicControl.Setting1 = value;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000604 RID: 1540 RVA: 0x000486F4 File Offset: 0x000476F4
		// (set) Token: 0x06000605 RID: 1541 RVA: 0x00048711 File Offset: 0x00047711
		[Category("Design")]
		[Description("Sql code for display items (first column should be integer id, second column should be binary for decrypting or string, third and subsequent columns are optional.")]
		public string Sql
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

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000606 RID: 1542 RVA: 0x00048724 File Offset: 0x00047724
		// (set) Token: 0x06000607 RID: 1543 RVA: 0x00048750 File Offset: 0x00047750
		[Description("Default value")]
		[Category("Behaviour")]
		public MultiItemDefaultSelection DefaultValue
		{
			get
			{
				int defaultValue = this.dynamicControl.DefaultValue;
				MultiItemDefaultSelection result;
				if (defaultValue != 0)
				{
					result = MultiItemDefaultSelection.All_Selected;
				}
				else
				{
					result = MultiItemDefaultSelection.None_Selected;
				}
				return result;
			}
			set
			{
				switch (value)
				{
				case MultiItemDefaultSelection.None_Selected:
					this.dynamicControl.DefaultValue = 0;
					break;
				case MultiItemDefaultSelection.All_Selected:
					this.dynamicControl.DefaultValue = 1;
					break;
				}
			}
		}
	}
}
