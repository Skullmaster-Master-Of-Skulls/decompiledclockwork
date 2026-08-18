using System;

namespace System.Data
{
	// Token: 0x020000B4 RID: 180
	public class FillErrorEventArgs : EventArgs
	{
		// Token: 0x06000C15 RID: 3093 RVA: 0x0020F938 File Offset: 0x0020ED38
		public FillErrorEventArgs(DataTable dataTable, object[] values)
		{
			this.dataTable = dataTable;
			this.values = values;
			if (this.values == null)
			{
				this.values = new object[0];
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000C16 RID: 3094 RVA: 0x0020F978 File Offset: 0x0020ED78
		// (set) Token: 0x06000C17 RID: 3095 RVA: 0x0020F998 File Offset: 0x0020ED98
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

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000C18 RID: 3096 RVA: 0x0020F9B8 File Offset: 0x0020EDB8
		public DataTable DataTable
		{
			get
			{
				return this.dataTable;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000C19 RID: 3097 RVA: 0x0020F9D8 File Offset: 0x0020EDD8
		// (set) Token: 0x06000C1A RID: 3098 RVA: 0x0020F9F8 File Offset: 0x0020EDF8
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

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000C1B RID: 3099 RVA: 0x0020FA18 File Offset: 0x0020EE18
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

		// Token: 0x0400089A RID: 2202
		private bool continueFlag;

		// Token: 0x0400089B RID: 2203
		private DataTable dataTable;

		// Token: 0x0400089C RID: 2204
		private Exception errors;

		// Token: 0x0400089D RID: 2205
		private object[] values;
	}
}
