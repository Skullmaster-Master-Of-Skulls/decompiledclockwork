using System;

namespace System.Data
{
	// Token: 0x020000E2 RID: 226
	public class FillErrorEventArgs : EventArgs
	{
		// Token: 0x06000F18 RID: 3864 RVA: 0x00079018 File Offset: 0x00078418
		public FillErrorEventArgs(DataTable dataTable, object[] values)
		{
			this.dataTable = dataTable;
			this.values = values;
			if (this.values == null)
			{
				this.values = new object[0];
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000F19 RID: 3865 RVA: 0x00079050 File Offset: 0x00078450
		// (set) Token: 0x06000F1A RID: 3866 RVA: 0x00079064 File Offset: 0x00078464
		public bool Continue
		{
			get
			{
				return this.continueFlag;
			}
			set
			{
				this.continueFlag = value;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000F1B RID: 3867 RVA: 0x00079078 File Offset: 0x00078478
		public DataTable DataTable
		{
			get
			{
				return this.dataTable;
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000F1C RID: 3868 RVA: 0x0007908C File Offset: 0x0007848C
		// (set) Token: 0x06000F1D RID: 3869 RVA: 0x000790A0 File Offset: 0x000784A0
		public Exception Errors
		{
			get
			{
				return this.errors;
			}
			set
			{
				this.errors = value;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000F1E RID: 3870 RVA: 0x000790B4 File Offset: 0x000784B4
		public object[] Values
		{
			get
			{
				object[] array = new object[this.values.Length];
				for (int i = 0; i < this.values.Length; i++)
				{
					array[i] = this.values[i];
				}
				return array;
			}
		}

		// Token: 0x04000474 RID: 1140
		private bool continueFlag;

		// Token: 0x04000475 RID: 1141
		private DataTable dataTable;

		// Token: 0x04000476 RID: 1142
		private Exception errors;

		// Token: 0x04000477 RID: 1143
		private object[] values;
	}
}
