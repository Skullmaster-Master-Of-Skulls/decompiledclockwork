using System;
using System.ComponentModel;

namespace DynamicScreens.DynamicControlWrappers
{
	// Token: 0x02000066 RID: 102
	public class DynamicControlWrapper_ListSelect : DynamicControlWrapper_Base
	{
		// Token: 0x06000529 RID: 1321 RVA: 0x00041D35 File Offset: 0x00040D35
		public DynamicControlWrapper_ListSelect(DynamicControl dynamicControl) : base(dynamicControl)
		{
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x00041D44 File Offset: 0x00040D44
		// (set) Token: 0x0600052B RID: 1323 RVA: 0x00041D68 File Offset: 0x00040D68
		[Category("Behaviour")]
		[Description("Is this checkbox checked by default?")]
		public bool DefaultChecked
		{
			get
			{
				return (this.dynamicControl.DefaultValue & 1) == 1;
			}
			set
			{
				int num = this.dynamicControl.DefaultValue >> 1;
				this.dynamicControl.DefaultValue = (num << 1) + (value ? 1 : 0);
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x00041D9C File Offset: 0x00040D9C
		// (set) Token: 0x0600052D RID: 1325 RVA: 0x00041DB9 File Offset: 0x00040DB9
		[Category("Behaviour")]
		[Description("Enter the number of rows high the list control should be.  Any of the list select items can set the height for the list control; the last list select item with a height setting will persist.")]
		[DisplayName("List row count")]
		public int ListRowCount
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

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x0600052E RID: 1326 RVA: 0x00041DCC File Offset: 0x00040DCC
		// (set) Token: 0x0600052F RID: 1327 RVA: 0x00041DEF File Offset: 0x00040DEF
		[DisplayName("Show as drop-list")]
		[Description("Show as a multi-checkbox drop-list.")]
		[Category("Display")]
		public bool ShowAsComboBox
		{
			get
			{
				return this.dynamicControl.Setting3 != 0;
			}
			set
			{
				this.dynamicControl.Setting3 = (value ? 1 : 0);
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000530 RID: 1328 RVA: 0x00041E08 File Offset: 0x00040E08
		// (set) Token: 0x06000531 RID: 1329 RVA: 0x00041E48 File Offset: 0x00040E48
		[Description("Enter a title for the checked list.")]
		[DisplayName("Checked List Title")]
		[Category("Description")]
		public string ListTitleUnchecked
		{
			get
			{
				string[] array = this.dynamicControl.Setting4String.Split(new char[]
				{
					'`'
				});
				return (array.Length > 0) ? array[0] : "";
			}
			set
			{
				this.dynamicControl.Setting4String = value + "`" + this.ListTitleChecked;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x00041E68 File Offset: 0x00040E68
		// (set) Token: 0x06000533 RID: 1331 RVA: 0x00041EA8 File Offset: 0x00040EA8
		[DisplayName("Un-hecked List Title")]
		[Description("Enter a title for the un-checked list.")]
		[Category("Description")]
		public string ListTitleChecked
		{
			get
			{
				string[] array = this.dynamicControl.Setting4String.Split(new char[]
				{
					'`'
				});
				return (array.Length > 1) ? array[1] : "";
			}
			set
			{
				this.dynamicControl.Setting4String = this.ListTitleUnchecked + "`" + value;
			}
		}
	}
}
