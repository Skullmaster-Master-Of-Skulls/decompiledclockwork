using System;
using System.Data.Common;

namespace System.Data.SqlClient
{
	// Token: 0x020002B6 RID: 694
	public sealed class SqlBulkCopyColumnMapping
	{
		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x0600232D RID: 9005 RVA: 0x002901B8 File Offset: 0x0028F5B8
		// (set) Token: 0x0600232E RID: 9006 RVA: 0x002901E8 File Offset: 0x0028F5E8
		public string DestinationColumn
		{
			get
			{
				if (this._destinationColumnName != null)
				{
					return this._destinationColumnName;
				}
				return string.Empty;
			}
			set
			{
				this._destinationColumnOrdinal = (this._internalDestinationColumnOrdinal = -1);
				this._destinationColumnName = value;
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x0600232F RID: 9007 RVA: 0x00290218 File Offset: 0x0028F618
		// (set) Token: 0x06002330 RID: 9008 RVA: 0x00290238 File Offset: 0x0028F638
		public int DestinationOrdinal
		{
			get
			{
				return this._destinationColumnOrdinal;
			}
			set
			{
				if (value >= 0)
				{
					this._destinationColumnName = null;
					this._internalDestinationColumnOrdinal = value;
					this._destinationColumnOrdinal = value;
					return;
				}
				throw ADP.IndexOutOfRange(value);
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06002331 RID: 9009 RVA: 0x00290268 File Offset: 0x0028F668
		// (set) Token: 0x06002332 RID: 9010 RVA: 0x00290298 File Offset: 0x0028F698
		public string SourceColumn
		{
			get
			{
				if (this._sourceColumnName != null)
				{
					return this._sourceColumnName;
				}
				return string.Empty;
			}
			set
			{
				this._sourceColumnOrdinal = (this._internalSourceColumnOrdinal = -1);
				this._sourceColumnName = value;
			}
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06002333 RID: 9011 RVA: 0x002902C8 File Offset: 0x0028F6C8
		// (set) Token: 0x06002334 RID: 9012 RVA: 0x002902E8 File Offset: 0x0028F6E8
		public int SourceOrdinal
		{
			get
			{
				return this._sourceColumnOrdinal;
			}
			set
			{
				if (value >= 0)
				{
					this._sourceColumnName = null;
					this._internalSourceColumnOrdinal = value;
					this._sourceColumnOrdinal = value;
					return;
				}
				throw ADP.IndexOutOfRange(value);
			}
		}

		// Token: 0x06002335 RID: 9013 RVA: 0x00290318 File Offset: 0x0028F718
		public SqlBulkCopyColumnMapping()
		{
			this._internalSourceColumnOrdinal = -1;
		}

		// Token: 0x06002336 RID: 9014 RVA: 0x00290338 File Offset: 0x0028F738
		public SqlBulkCopyColumnMapping(string sourceColumn, string destinationColumn)
		{
			this.SourceColumn = sourceColumn;
			this.DestinationColumn = destinationColumn;
		}

		// Token: 0x06002337 RID: 9015 RVA: 0x00290368 File Offset: 0x0028F768
		public SqlBulkCopyColumnMapping(int sourceColumnOrdinal, string destinationColumn)
		{
			this.SourceOrdinal = sourceColumnOrdinal;
			this.DestinationColumn = destinationColumn;
		}

		// Token: 0x06002338 RID: 9016 RVA: 0x00290398 File Offset: 0x0028F798
		public SqlBulkCopyColumnMapping(string sourceColumn, int destinationOrdinal)
		{
			this.SourceColumn = sourceColumn;
			this.DestinationOrdinal = destinationOrdinal;
		}

		// Token: 0x06002339 RID: 9017 RVA: 0x002903C8 File Offset: 0x0028F7C8
		public SqlBulkCopyColumnMapping(int sourceColumnOrdinal, int destinationOrdinal)
		{
			this.SourceOrdinal = sourceColumnOrdinal;
			this.DestinationOrdinal = destinationOrdinal;
		}

		// Token: 0x040016F4 RID: 5876
		internal string _destinationColumnName;

		// Token: 0x040016F5 RID: 5877
		internal int _destinationColumnOrdinal;

		// Token: 0x040016F6 RID: 5878
		internal string _sourceColumnName;

		// Token: 0x040016F7 RID: 5879
		internal int _sourceColumnOrdinal;

		// Token: 0x040016F8 RID: 5880
		internal int _internalDestinationColumnOrdinal;

		// Token: 0x040016F9 RID: 5881
		internal int _internalSourceColumnOrdinal;
	}
}
