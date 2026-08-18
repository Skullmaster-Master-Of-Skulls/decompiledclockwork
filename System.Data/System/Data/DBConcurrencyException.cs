using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Data
{
	// Token: 0x020000B1 RID: 177
	[Serializable]
	public sealed class DBConcurrencyException : SystemException
	{
		// Token: 0x06000C06 RID: 3078 RVA: 0x0020F6A8 File Offset: 0x0020EAA8
		public DBConcurrencyException() : this(Res.GetString("ADP_DBConcurrencyExceptionMessage"), null)
		{
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x0020F6C8 File Offset: 0x0020EAC8
		public DBConcurrencyException(string message) : this(message, null)
		{
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x0020F6E8 File Offset: 0x0020EAE8
		public DBConcurrencyException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146232011;
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x0020F708 File Offset: 0x0020EB08
		public DBConcurrencyException(string message, Exception inner, DataRow[] dataRows) : base(message, inner)
		{
			base.HResult = -2146232011;
			this._dataRows = dataRows;
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x0020F738 File Offset: 0x0020EB38
		private DBConcurrencyException(SerializationInfo si, StreamingContext sc) : base(si, sc)
		{
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x0020F758 File Offset: 0x0020EB58
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo si, StreamingContext context)
		{
			if (si == null)
			{
				throw new ArgumentNullException("si");
			}
			base.GetObjectData(si, context);
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000C0C RID: 3084 RVA: 0x0020F788 File Offset: 0x0020EB88
		// (set) Token: 0x06000C0D RID: 3085 RVA: 0x0020F7B8 File Offset: 0x0020EBB8
		public DataRow Row
		{
			get
			{
				DataRow[] dataRows = this._dataRows;
				if (dataRows == null || 0 >= dataRows.Length)
				{
					return null;
				}
				return dataRows[0];
			}
			set
			{
				this._dataRows = new DataRow[]
				{
					value
				};
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000C0E RID: 3086 RVA: 0x0020F7D8 File Offset: 0x0020EBD8
		public int RowCount
		{
			get
			{
				DataRow[] dataRows = this._dataRows;
				if (dataRows == null)
				{
					return 0;
				}
				return dataRows.Length;
			}
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x0020F7F8 File Offset: 0x0020EBF8
		public void CopyToRows(DataRow[] array)
		{
			this.CopyToRows(array, 0);
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x0020F818 File Offset: 0x0020EC18
		public void CopyToRows(DataRow[] array, int arrayIndex)
		{
			DataRow[] dataRows = this._dataRows;
			if (dataRows != null)
			{
				dataRows.CopyTo(array, arrayIndex);
			}
		}

		// Token: 0x0400087B RID: 2171
		private DataRow[] _dataRows;
	}
}
