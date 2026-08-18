using System;

namespace System.Data
{
	// Token: 0x0200006D RID: 109
	internal sealed class DataError
	{
		// Token: 0x0600057D RID: 1405 RVA: 0x001ED168 File Offset: 0x001EC568
		internal DataError()
		{
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x001ED188 File Offset: 0x001EC588
		internal DataError(string rowError)
		{
			this.SetText(rowError);
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x001ED1B8 File Offset: 0x001EC5B8
		// (set) Token: 0x06000580 RID: 1408 RVA: 0x001ED1D8 File Offset: 0x001EC5D8
		internal string Text
		{
			get
			{
				return this.rowError;
			}
			set
			{
				this.SetText(value);
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x001ED1F8 File Offset: 0x001EC5F8
		internal bool HasErrors
		{
			get
			{
				return this.rowError.Length != 0 || this.count != 0;
			}
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x001ED228 File Offset: 0x001EC628
		internal void SetColumnError(DataColumn column, string error)
		{
			if (error == null || error.Length == 0)
			{
				this.Clear(column);
				return;
			}
			if (this.errorList == null)
			{
				this.errorList = new DataError.ColumnError[1];
			}
			int num = this.IndexOf(column);
			this.errorList[num].column = column;
			this.errorList[num].error = error;
			column.errors++;
			if (num == this.count)
			{
				this.count++;
			}
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x001ED2B8 File Offset: 0x001EC6B8
		internal string GetColumnError(DataColumn column)
		{
			for (int i = 0; i < this.count; i++)
			{
				if (this.errorList[i].column == column)
				{
					return this.errorList[i].error;
				}
			}
			return string.Empty;
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x001ED308 File Offset: 0x001EC708
		internal void Clear(DataColumn column)
		{
			if (this.count == 0)
			{
				return;
			}
			for (int i = 0; i < this.count; i++)
			{
				if (this.errorList[i].column == column)
				{
					Array.Copy(this.errorList, i + 1, this.errorList, i, this.count - i - 1);
					this.count--;
					column.errors--;
				}
			}
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x001ED388 File Offset: 0x001EC788
		internal void Clear()
		{
			for (int i = 0; i < this.count; i++)
			{
				this.errorList[i].column.errors--;
			}
			this.count = 0;
			this.rowError = string.Empty;
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x001ED3D8 File Offset: 0x001EC7D8
		internal DataColumn[] GetColumnsInError()
		{
			DataColumn[] array = new DataColumn[this.count];
			for (int i = 0; i < this.count; i++)
			{
				array[i] = this.errorList[i].column;
			}
			return array;
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x001ED418 File Offset: 0x001EC818
		private void SetText(string errorText)
		{
			if (errorText == null)
			{
				errorText = string.Empty;
			}
			this.rowError = errorText;
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x001ED438 File Offset: 0x001EC838
		internal int IndexOf(DataColumn column)
		{
			for (int i = 0; i < this.count; i++)
			{
				if (this.errorList[i].column == column)
				{
					return i;
				}
			}
			if (this.count >= this.errorList.Length)
			{
				int num = Math.Min(this.count * 2, column.Table.Columns.Count);
				DataError.ColumnError[] destinationArray = new DataError.ColumnError[num];
				Array.Copy(this.errorList, 0, destinationArray, 0, this.count);
				this.errorList = destinationArray;
			}
			return this.count;
		}

		// Token: 0x04000719 RID: 1817
		internal const int initialCapacity = 1;

		// Token: 0x0400071A RID: 1818
		private string rowError = string.Empty;

		// Token: 0x0400071B RID: 1819
		private int count;

		// Token: 0x0400071C RID: 1820
		private DataError.ColumnError[] errorList;

		// Token: 0x0200006E RID: 110
		internal struct ColumnError
		{
			// Token: 0x0400071D RID: 1821
			internal DataColumn column;

			// Token: 0x0400071E RID: 1822
			internal string error;
		}
	}
}
