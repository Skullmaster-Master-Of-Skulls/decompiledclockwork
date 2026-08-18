using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Data
{
	// Token: 0x020000DF RID: 223
	[Serializable]
	public sealed class DBConcurrencyException : SystemException
	{
		// Token: 0x06000F09 RID: 3849 RVA: 0x00078DDC File Offset: 0x000781DC
		public DBConcurrencyException() : this(Res.GetString("ADP_DBConcurrencyExceptionMessage"), null)
		{
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x00078DFC File Offset: 0x000781FC
		public DBConcurrencyException(string message) : this(message, null)
		{
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x00078E14 File Offset: 0x00078214
		public DBConcurrencyException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146232011;
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x00078E34 File Offset: 0x00078234
		public DBConcurrencyException(string message, Exception inner, DataRow[] dataRows) : base(message, inner)
		{
			base.HResult = -2146232011;
			this._dataRows = dataRows;
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x00078E5C File Offset: 0x0007825C
		private DBConcurrencyException(SerializationInfo si, StreamingContext sc) : base(si, sc)
		{
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x00078E74 File Offset: 0x00078274
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo si, StreamingContext context)
		{
			if (si == null)
			{
				throw new ArgumentNullException("si");
			}
			base.GetObjectData(si, context);
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000F0F RID: 3855 RVA: 0x00078E98 File Offset: 0x00078298
		// (set) Token: 0x06000F10 RID: 3856 RVA: 0x00078EB8 File Offset: 0x000782B8
		public DataRow Row
		{
			get
			{
				DataRow[] dataRows = this._dataRows;
				if (dataRows == null || dataRows.Length == 0)
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

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000F11 RID: 3857 RVA: 0x00078ED8 File Offset: 0x000782D8
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

		// Token: 0x06000F12 RID: 3858 RVA: 0x00078EF4 File Offset: 0x000782F4
		public void CopyToRows(DataRow[] array)
		{
			this.CopyToRows(array, 0);
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x00078F0C File Offset: 0x0007830C
		public void CopyToRows(DataRow[] array, int arrayIndex)
		{
			DataRow[] dataRows = this._dataRows;
			if (dataRows != null)
			{
				dataRows.CopyTo(array, arrayIndex);
			}
		}

		// Token: 0x04000455 RID: 1109
		private DataRow[] _dataRows;
	}
}
