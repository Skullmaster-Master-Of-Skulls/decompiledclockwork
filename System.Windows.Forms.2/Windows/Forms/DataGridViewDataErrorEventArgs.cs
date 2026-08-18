using System;

namespace System.Windows.Forms
{
	// Token: 0x020001CF RID: 463
	public class DataGridViewDataErrorEventArgs : DataGridViewCellCancelEventArgs
	{
		// Token: 0x0600205B RID: 8283 RVA: 0x0009B90E File Offset: 0x00099B0E
		public DataGridViewDataErrorEventArgs(Exception exception, int columnIndex, int rowIndex, DataGridViewDataErrorContexts context) : base(columnIndex, rowIndex)
		{
			this.exception = exception;
			this.context = context;
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x0600205C RID: 8284 RVA: 0x0009B927 File Offset: 0x00099B27
		public DataGridViewDataErrorContexts Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x0600205D RID: 8285 RVA: 0x0009B92F File Offset: 0x00099B2F
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x0600205E RID: 8286 RVA: 0x0009B937 File Offset: 0x00099B37
		// (set) Token: 0x0600205F RID: 8287 RVA: 0x0009B93F File Offset: 0x00099B3F
		public bool ThrowException
		{
			get
			{
				return this.throwException;
			}
			set
			{
				if (value && this.exception == null)
				{
					throw new ArgumentException(SR.GetString("DataGridView_CannotThrowNullException"));
				}
				this.throwException = value;
			}
		}

		// Token: 0x04000DB3 RID: 3507
		private Exception exception;

		// Token: 0x04000DB4 RID: 3508
		private bool throwException;

		// Token: 0x04000DB5 RID: 3509
		private DataGridViewDataErrorContexts context;
	}
}
