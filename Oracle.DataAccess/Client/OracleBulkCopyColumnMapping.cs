using System;

namespace Oracle.DataAccess.Client
{
	// Token: 0x020000F3 RID: 243
	public sealed class OracleBulkCopyColumnMapping : IComparable
	{
		// Token: 0x060008DE RID: 2270 RVA: 0x00058555 File Offset: 0x00057555
		static OracleBulkCopyColumnMapping()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x00058563 File Offset: 0x00057563
		public OracleBulkCopyColumnMapping()
		{
			this.m_sourceColumnOrdinal = -1;
			this.m_destinationColumnOrdinal = -1;
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x00058579 File Offset: 0x00057579
		public OracleBulkCopyColumnMapping(int sourceColumnOrdinal, int destinationOrdinal)
		{
			if (sourceColumnOrdinal < 0 || destinationOrdinal < 0)
			{
				throw new IndexOutOfRangeException(OpoErrResManager.GetErrorMesg(ErrRes.DR_INV_COL_INDEX, new string[0]));
			}
			this.m_sourceColumnOrdinal = sourceColumnOrdinal;
			this.m_destinationColumnOrdinal = destinationOrdinal;
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x000585AD File Offset: 0x000575AD
		public OracleBulkCopyColumnMapping(int sourceColumnOrdinal, string destinationColumn)
		{
			if (sourceColumnOrdinal < 0)
			{
				throw new IndexOutOfRangeException(OpoErrResManager.GetErrorMesg(ErrRes.DR_INV_COL_INDEX, new string[0]));
			}
			this.m_sourceColumnOrdinal = sourceColumnOrdinal;
			this.m_destinationColumnName = destinationColumn;
			this.m_destinationColumnOrdinal = -1;
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x000585E4 File Offset: 0x000575E4
		public OracleBulkCopyColumnMapping(string sourceColumn, int destinationOrdinal)
		{
			if (destinationOrdinal < 0)
			{
				throw new IndexOutOfRangeException(OpoErrResManager.GetErrorMesg(ErrRes.DR_INV_COL_INDEX, new string[0]));
			}
			this.m_sourceColumnName = sourceColumn;
			this.m_destinationColumnOrdinal = destinationOrdinal;
			this.m_sourceColumnOrdinal = -1;
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x0005861B File Offset: 0x0005761B
		public OracleBulkCopyColumnMapping(string sourceColumn, string destinationColumn)
		{
			this.m_sourceColumnName = sourceColumn;
			this.m_destinationColumnName = destinationColumn;
			this.m_sourceColumnOrdinal = -1;
			this.m_destinationColumnOrdinal = -1;
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x00058640 File Offset: 0x00057640
		internal OracleBulkCopyColumnMapping Clone()
		{
			return new OracleBulkCopyColumnMapping
			{
				m_sourceColumnName = this.m_sourceColumnName,
				m_destinationColumnName = this.m_destinationColumnName,
				m_sourceColumnOrdinal = this.m_sourceColumnOrdinal,
				m_destinationColumnOrdinal = this.m_destinationColumnOrdinal
			};
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x00058684 File Offset: 0x00057684
		public int CompareTo(object obj)
		{
			if (obj is OracleBulkCopyColumnMapping)
			{
				OracleBulkCopyColumnMapping oracleBulkCopyColumnMapping = (OracleBulkCopyColumnMapping)obj;
				return this.m_sourceColumnOrdinal.CompareTo(oracleBulkCopyColumnMapping.m_sourceColumnOrdinal);
			}
			throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
			{
				"object"
			}));
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060008E6 RID: 2278 RVA: 0x000586D1 File Offset: 0x000576D1
		// (set) Token: 0x060008E7 RID: 2279 RVA: 0x000586E7 File Offset: 0x000576E7
		public string DestinationColumn
		{
			get
			{
				if (this.m_destinationColumnName == null)
				{
					return string.Empty;
				}
				return this.m_destinationColumnName;
			}
			set
			{
				this.m_destinationColumnOrdinal = -1;
				this.m_destinationColumnName = value;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060008E8 RID: 2280 RVA: 0x000586F7 File Offset: 0x000576F7
		// (set) Token: 0x060008E9 RID: 2281 RVA: 0x000586FF File Offset: 0x000576FF
		public int DestinationOrdinal
		{
			get
			{
				return this.m_destinationColumnOrdinal;
			}
			set
			{
				if (value < 0)
				{
					throw new IndexOutOfRangeException(OpoErrResManager.GetErrorMesg(ErrRes.DR_INV_COL_INDEX, new string[0]));
				}
				this.m_destinationColumnName = null;
				this.m_destinationColumnOrdinal = value;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060008EA RID: 2282 RVA: 0x00058729 File Offset: 0x00057729
		// (set) Token: 0x060008EB RID: 2283 RVA: 0x0005873F File Offset: 0x0005773F
		public string SourceColumn
		{
			get
			{
				if (this.m_sourceColumnName == null)
				{
					return string.Empty;
				}
				return this.m_sourceColumnName;
			}
			set
			{
				this.m_sourceColumnOrdinal = -1;
				this.m_sourceColumnName = value;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060008EC RID: 2284 RVA: 0x0005874F File Offset: 0x0005774F
		// (set) Token: 0x060008ED RID: 2285 RVA: 0x00058757 File Offset: 0x00057757
		public int SourceOrdinal
		{
			get
			{
				return this.m_sourceColumnOrdinal;
			}
			set
			{
				if (value < 0)
				{
					throw new IndexOutOfRangeException(OpoErrResManager.GetErrorMesg(ErrRes.DR_INV_COL_INDEX, new string[0]));
				}
				this.m_sourceColumnName = null;
				this.m_sourceColumnOrdinal = value;
			}
		}

		// Token: 0x040007A2 RID: 1954
		internal string m_destinationColumnName;

		// Token: 0x040007A3 RID: 1955
		internal string m_sourceColumnName;

		// Token: 0x040007A4 RID: 1956
		internal int m_destinationColumnOrdinal;

		// Token: 0x040007A5 RID: 1957
		internal int m_sourceColumnOrdinal;
	}
}
