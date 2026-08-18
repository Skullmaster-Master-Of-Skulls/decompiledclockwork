using System;
using System.Collections.Generic;
using System.Data;

namespace DynamicScreens.DynamicData
{
	// Token: 0x02000008 RID: 8
	public class DynamicDataField
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000085 RID: 133 RVA: 0x0000463C File Offset: 0x0000363C
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00004654 File Offset: 0x00003654
		public int ControlCode
		{
			get
			{
				return this.controlCode;
			}
			set
			{
				this.controlCode = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00004660 File Offset: 0x00003660
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00004678 File Offset: 0x00003678
		public int ControlId
		{
			get
			{
				return this.controlId;
			}
			set
			{
				this.controlId = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00004684 File Offset: 0x00003684
		// (set) Token: 0x0600008A RID: 138 RVA: 0x0000469C File Offset: 0x0000369C
		public List<DynamicDataValue> DataValues
		{
			get
			{
				return this.dataValues;
			}
			set
			{
				this.dataValues = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600008B RID: 139 RVA: 0x000046A8 File Offset: 0x000036A8
		// (set) Token: 0x0600008C RID: 140 RVA: 0x000046D8 File Offset: 0x000036D8
		public DynamicDataValue DataValueFirst
		{
			get
			{
				return (this.dataValues.Count > 0) ? this.dataValues[0] : null;
			}
			set
			{
				if (this.dataValues.Count > 0)
				{
					this.dataValues[0] = value;
				}
				else
				{
					this.dataValues.Add(value);
				}
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000471C File Offset: 0x0000371C
		public DynamicDataField(DataRow dr)
		{
		}

		// Token: 0x04000021 RID: 33
		private int controlId;

		// Token: 0x04000022 RID: 34
		private int controlCode;

		// Token: 0x04000023 RID: 35
		private int setting1;

		// Token: 0x04000024 RID: 36
		private int setting2;

		// Token: 0x04000025 RID: 37
		private int setting3;

		// Token: 0x04000026 RID: 38
		private int setting4;

		// Token: 0x04000027 RID: 39
		private string controlCaption;

		// Token: 0x04000028 RID: 40
		private string controlCaptionFrench;

		// Token: 0x04000029 RID: 41
		private Dictionary<string, string> args;

		// Token: 0x0400002A RID: 42
		private List<DynamicDataValue> dataValues;
	}
}
