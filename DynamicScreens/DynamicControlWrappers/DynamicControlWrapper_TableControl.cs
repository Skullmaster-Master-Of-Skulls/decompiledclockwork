using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;

namespace DynamicScreens.DynamicControlWrappers
{
	// Token: 0x02000071 RID: 113
	public class DynamicControlWrapper_TableControl : DynamicControlWrapper_Base
	{
		// Token: 0x06000592 RID: 1426 RVA: 0x00042DDA File Offset: 0x00041DDA
		public DynamicControlWrapper_TableControl(DynamicControl dynamicControl) : base(dynamicControl)
		{
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x00042DE8 File Offset: 0x00041DE8
		// (set) Token: 0x06000594 RID: 1428 RVA: 0x00042E05 File Offset: 0x00041E05
		[Description("Indicates column definitions.")]
		[Category("Display")]
		[Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
		public string ColumnDefinitions
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
	}
}
