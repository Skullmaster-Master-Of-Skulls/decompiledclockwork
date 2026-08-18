using System;
using System.Data.Common;

namespace System.Data.SqlClient
{
	// Token: 0x020001AA RID: 426
	public sealed class SqlBulkCopyColumnMapping
	{
		// Token: 0x17000382 RID: 898
		// (get) Token: 0x060018FC RID: 6396 RVA: 0x000B1614 File Offset: 0x000B0A14
		// (set) Token: 0x060018FD RID: 6397 RVA: 0x000B1638 File Offset: 0x000B0A38
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

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x060018FE RID: 6398 RVA: 0x000B165C File Offset: 0x000B0A5C
		// (set) Token: 0x060018FF RID: 6399 RVA: 0x000B1670 File Offset: 0x000B0A70
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

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06001900 RID: 6400 RVA: 0x000B16A0 File Offset: 0x000B0AA0
		// (set) Token: 0x06001901 RID: 6401 RVA: 0x000B16C4 File Offset: 0x000B0AC4
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

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06001902 RID: 6402 RVA: 0x000B16E8 File Offset: 0x000B0AE8
		// (set) Token: 0x06001903 RID: 6403 RVA: 0x000B16FC File Offset: 0x000B0AFC
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

		// Token: 0x06001904 RID: 6404 RVA: 0x000B172C File Offset: 0x000B0B2C
		public SqlBulkCopyColumnMapping()
		{
			this._internalSourceColumnOrdinal = -1;
		}

		// Token: 0x06001905 RID: 6405 RVA: 0x000B1748 File Offset: 0x000B0B48
		public SqlBulkCopyColumnMapping(string sourceColumn, string destinationColumn)
		{
			this.SourceColumn = sourceColumn;
			this.DestinationColumn = destinationColumn;
		}

		// Token: 0x06001906 RID: 6406 RVA: 0x000B176C File Offset: 0x000B0B6C
		public SqlBulkCopyColumnMapping(int sourceColumnOrdinal, string destinationColumn)
		{
			this.SourceOrdinal = sourceColumnOrdinal;
			this.DestinationColumn = destinationColumn;
		}

		// Token: 0x06001907 RID: 6407 RVA: 0x000B1790 File Offset: 0x000B0B90
		public SqlBulkCopyColumnMapping(string sourceColumn, int destinationOrdinal)
		{
			this.SourceColumn = sourceColumn;
			this.DestinationOrdinal = destinationOrdinal;
		}

		// Token: 0x06001908 RID: 6408 RVA: 0x000B17B4 File Offset: 0x000B0BB4
		public SqlBulkCopyColumnMapping(int sourceColumnOrdinal, int destinationOrdinal)
		{
			this.SourceOrdinal = sourceColumnOrdinal;
			this.DestinationOrdinal = destinationOrdinal;
		}

		// Token: 0x04000EE8 RID: 3816
		internal string _destinationColumnName;

		// Token: 0x04000EE9 RID: 3817
		internal int _destinationColumnOrdinal;

		// Token: 0x04000EEA RID: 3818
		internal string _sourceColumnName;

		// Token: 0x04000EEB RID: 3819
		internal int _sourceColumnOrdinal;

		// Token: 0x04000EEC RID: 3820
		internal int _internalDestinationColumnOrdinal;

		// Token: 0x04000EED RID: 3821
		internal int _internalSourceColumnOrdinal;
	}
}
