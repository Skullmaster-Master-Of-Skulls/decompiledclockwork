using System;
using System.ComponentModel;
using DynamicScreens.DynamicControlWrappers.TypeConverters;

namespace DynamicScreens.DynamicControlWrappers
{
	// Token: 0x02000067 RID: 103
	public class DynamicControlWrapper_FileTable : DynamicControlWrapper_Base
	{
		// Token: 0x06000534 RID: 1332 RVA: 0x00041EC8 File Offset: 0x00040EC8
		public DynamicControlWrapper_FileTable(DynamicControl dynamicControl) : base(dynamicControl)
		{
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x00041ED4 File Offset: 0x00040ED4
		// (set) Token: 0x06000536 RID: 1334 RVA: 0x00041EF8 File Offset: 0x00040EF8
		[Description("Indicates the columns that will be used for the table.  Note that a date column is always provided by default as the column in the table (you don't need to specify it in this list).")]
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

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x00041F20 File Offset: 0x00040F20
		// (set) Token: 0x06000538 RID: 1336 RVA: 0x00041F3D File Offset: 0x00040F3D
		[Description("Indicates the number of rows high.")]
		[Category("Display")]
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

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x00041F50 File Offset: 0x00040F50
		// (set) Token: 0x0600053A RID: 1338 RVA: 0x00041F70 File Offset: 0x00040F70
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

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x00041F88 File Offset: 0x00040F88
		// (set) Token: 0x0600053C RID: 1340 RVA: 0x00041FD0 File Offset: 0x00040FD0
		[Category("Behaviour")]
		[Description("Can users change an existing file? (Note that if this is set to false the user can still remove the row and add a new row with a new file)")]
		public bool AllowedToEditExistingFiles
		{
			get
			{
				string specialInstructionStringValue = DynamicControl.GetSpecialInstructionStringValue(this.dynamicControl.ControlGroup, "noediting");
				return string.IsNullOrEmpty(specialInstructionStringValue) || !specialInstructionStringValue.Equals("1");
			}
			set
			{
				this.dynamicControl.ControlGroup = DynamicControl.SetSpecialInstructionStringValue(this.dynamicControl.ControlGroup, "noediting", value ? "" : "1");
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600053D RID: 1341 RVA: 0x00042004 File Offset: 0x00041004
		// (set) Token: 0x0600053E RID: 1342 RVA: 0x0004204C File Offset: 0x0004104C
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
