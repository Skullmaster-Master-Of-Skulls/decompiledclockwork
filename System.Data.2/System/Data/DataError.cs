using System;

namespace System.Data
{
	// Token: 0x020000AC RID: 172
	internal sealed class DataError
	{
		// Token: 0x06000924 RID: 2340 RVA: 0x0005BF78 File Offset: 0x0005B378
		internal DataError()
		{
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x0005BF98 File Offset: 0x0005B398
		internal DataError(string rowError)
		{
			this.SetText(rowError);
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000926 RID: 2342 RVA: 0x0005BFC0 File Offset: 0x0005B3C0
		// (set) Token: 0x06000927 RID: 2343 RVA: 0x0005BFD4 File Offset: 0x0005B3D4
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

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x0005BFE8 File Offset: 0x0005B3E8
		internal bool HasErrors
		{
			get
			{
				return this.rowError.Length != 0 || this.count != 0;
			}
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x0005C010 File Offset: 0x0005B410
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

		// Token: 0x0600092A RID: 2346 RVA: 0x0005C098 File Offset: 0x0005B498
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

		// Token: 0x0600092B RID: 2347 RVA: 0x0005C0E4 File Offset: 0x0005B4E4
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

		// Token: 0x0600092C RID: 2348 RVA: 0x0005C15C File Offset: 0x0005B55C
		internal void Clear()
		{
			for (int i = 0; i < this.count; i++)
			{
				this.errorList[i].column.errors--;
			}
			this.count = 0;
			this.rowError = string.Empty;
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x0005C1AC File Offset: 0x0005B5AC
		internal DataColumn[] GetColumnsInError()
		{
			DataColumn[] array = new DataColumn[this.count];
			for (int i = 0; i < this.count; i++)
			{
				array[i] = this.errorList[i].column;
			}
			return array;
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x0005C1EC File Offset: 0x0005B5EC
		private void SetText(string errorText)
		{
			if (errorText == null)
			{
				errorText = string.Empty;
			}
			this.rowError = errorText;
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x0005C20C File Offset: 0x0005B60C
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

		// Token: 0x04000327 RID: 807
		private string rowError = string.Empty;

		// Token: 0x04000328 RID: 808
		private int count;

		// Token: 0x04000329 RID: 809
		private DataError.ColumnError[] errorList;

		// Token: 0x0400032A RID: 810
		internal const int initialCapacity = 1;

		// Token: 0x02000346 RID: 838
		internal struct ColumnError
		{
			// Token: 0x04001EAE RID: 7854
			internal DataColumn column;

			// Token: 0x04001EAF RID: 7855
			internal string error;
		}
	}
}
