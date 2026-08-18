using System;
using System.Data.ProviderBase;

namespace System.Data.Odbc
{
	// Token: 0x02000294 RID: 660
	internal sealed class OdbcConnectionPoolGroupProviderInfo : DbConnectionPoolGroupProviderInfo
	{
		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x0600281C RID: 10268 RVA: 0x0010CBD8 File Offset: 0x0010BFD8
		// (set) Token: 0x0600281D RID: 10269 RVA: 0x0010CBEC File Offset: 0x0010BFEC
		internal string DriverName
		{
			get
			{
				return this._driverName;
			}
			set
			{
				this._driverName = value;
			}
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x0600281E RID: 10270 RVA: 0x0010CC00 File Offset: 0x0010C000
		// (set) Token: 0x0600281F RID: 10271 RVA: 0x0010CC14 File Offset: 0x0010C014
		internal string DriverVersion
		{
			get
			{
				return this._driverVersion;
			}
			set
			{
				this._driverVersion = value;
			}
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06002820 RID: 10272 RVA: 0x0010CC28 File Offset: 0x0010C028
		internal bool HasQuoteChar
		{
			get
			{
				return this._hasQuoteChar;
			}
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06002821 RID: 10273 RVA: 0x0010CC3C File Offset: 0x0010C03C
		internal bool HasEscapeChar
		{
			get
			{
				return this._hasEscapeChar;
			}
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06002822 RID: 10274 RVA: 0x0010CC50 File Offset: 0x0010C050
		// (set) Token: 0x06002823 RID: 10275 RVA: 0x0010CC64 File Offset: 0x0010C064
		internal string QuoteChar
		{
			get
			{
				return this._quoteChar;
			}
			set
			{
				this._quoteChar = value;
				this._hasQuoteChar = true;
			}
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06002824 RID: 10276 RVA: 0x0010CC80 File Offset: 0x0010C080
		// (set) Token: 0x06002825 RID: 10277 RVA: 0x0010CC94 File Offset: 0x0010C094
		internal char EscapeChar
		{
			get
			{
				return this._escapeChar;
			}
			set
			{
				this._escapeChar = value;
				this._hasEscapeChar = true;
			}
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06002826 RID: 10278 RVA: 0x0010CCB0 File Offset: 0x0010C0B0
		// (set) Token: 0x06002827 RID: 10279 RVA: 0x0010CCC4 File Offset: 0x0010C0C4
		internal bool IsV3Driver
		{
			get
			{
				return this._isV3Driver;
			}
			set
			{
				this._isV3Driver = value;
			}
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06002828 RID: 10280 RVA: 0x0010CCD8 File Offset: 0x0010C0D8
		// (set) Token: 0x06002829 RID: 10281 RVA: 0x0010CCEC File Offset: 0x0010C0EC
		internal int SupportedSQLTypes
		{
			get
			{
				return this._supportedSQLTypes;
			}
			set
			{
				this._supportedSQLTypes = value;
			}
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x0600282A RID: 10282 RVA: 0x0010CD00 File Offset: 0x0010C100
		// (set) Token: 0x0600282B RID: 10283 RVA: 0x0010CD14 File Offset: 0x0010C114
		internal int TestedSQLTypes
		{
			get
			{
				return this._testedSQLTypes;
			}
			set
			{
				this._testedSQLTypes = value;
			}
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x0600282C RID: 10284 RVA: 0x0010CD28 File Offset: 0x0010C128
		// (set) Token: 0x0600282D RID: 10285 RVA: 0x0010CD3C File Offset: 0x0010C13C
		internal int RestrictedSQLBindTypes
		{
			get
			{
				return this._restrictedSQLBindTypes;
			}
			set
			{
				this._restrictedSQLBindTypes = value;
			}
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x0600282E RID: 10286 RVA: 0x0010CD50 File Offset: 0x0010C150
		// (set) Token: 0x0600282F RID: 10287 RVA: 0x0010CD64 File Offset: 0x0010C164
		internal bool NoCurrentCatalog
		{
			get
			{
				return this._noCurrentCatalog;
			}
			set
			{
				this._noCurrentCatalog = value;
			}
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x06002830 RID: 10288 RVA: 0x0010CD78 File Offset: 0x0010C178
		// (set) Token: 0x06002831 RID: 10289 RVA: 0x0010CD8C File Offset: 0x0010C18C
		internal bool NoConnectionDead
		{
			get
			{
				return this._noConnectionDead;
			}
			set
			{
				this._noConnectionDead = value;
			}
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06002832 RID: 10290 RVA: 0x0010CDA0 File Offset: 0x0010C1A0
		// (set) Token: 0x06002833 RID: 10291 RVA: 0x0010CDB4 File Offset: 0x0010C1B4
		internal bool NoQueryTimeout
		{
			get
			{
				return this._noQueryTimeout;
			}
			set
			{
				this._noQueryTimeout = value;
			}
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06002834 RID: 10292 RVA: 0x0010CDC8 File Offset: 0x0010C1C8
		// (set) Token: 0x06002835 RID: 10293 RVA: 0x0010CDDC File Offset: 0x0010C1DC
		internal bool NoSqlSoptSSNoBrowseTable
		{
			get
			{
				return this._noSqlSoptSSNoBrowseTable;
			}
			set
			{
				this._noSqlSoptSSNoBrowseTable = value;
			}
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06002836 RID: 10294 RVA: 0x0010CDF0 File Offset: 0x0010C1F0
		// (set) Token: 0x06002837 RID: 10295 RVA: 0x0010CE04 File Offset: 0x0010C204
		internal bool NoSqlSoptSSHiddenColumns
		{
			get
			{
				return this._noSqlSoptSSHiddenColumns;
			}
			set
			{
				this._noSqlSoptSSHiddenColumns = value;
			}
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06002838 RID: 10296 RVA: 0x0010CE18 File Offset: 0x0010C218
		// (set) Token: 0x06002839 RID: 10297 RVA: 0x0010CE2C File Offset: 0x0010C22C
		internal bool NoSqlCASSColumnKey
		{
			get
			{
				return this._noSqlCASSColumnKey;
			}
			set
			{
				this._noSqlCASSColumnKey = value;
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x0600283A RID: 10298 RVA: 0x0010CE40 File Offset: 0x0010C240
		// (set) Token: 0x0600283B RID: 10299 RVA: 0x0010CE54 File Offset: 0x0010C254
		internal bool NoSqlPrimaryKeys
		{
			get
			{
				return this._noSqlPrimaryKeys;
			}
			set
			{
				this._noSqlPrimaryKeys = value;
			}
		}

		// Token: 0x04001A6E RID: 6766
		private string _driverName;

		// Token: 0x04001A6F RID: 6767
		private string _driverVersion;

		// Token: 0x04001A70 RID: 6768
		private string _quoteChar;

		// Token: 0x04001A71 RID: 6769
		private char _escapeChar;

		// Token: 0x04001A72 RID: 6770
		private bool _hasQuoteChar;

		// Token: 0x04001A73 RID: 6771
		private bool _hasEscapeChar;

		// Token: 0x04001A74 RID: 6772
		private bool _isV3Driver;

		// Token: 0x04001A75 RID: 6773
		private int _supportedSQLTypes;

		// Token: 0x04001A76 RID: 6774
		private int _testedSQLTypes;

		// Token: 0x04001A77 RID: 6775
		private int _restrictedSQLBindTypes;

		// Token: 0x04001A78 RID: 6776
		private bool _noCurrentCatalog;

		// Token: 0x04001A79 RID: 6777
		private bool _noConnectionDead;

		// Token: 0x04001A7A RID: 6778
		private bool _noQueryTimeout;

		// Token: 0x04001A7B RID: 6779
		private bool _noSqlSoptSSNoBrowseTable;

		// Token: 0x04001A7C RID: 6780
		private bool _noSqlSoptSSHiddenColumns;

		// Token: 0x04001A7D RID: 6781
		private bool _noSqlCASSColumnKey;

		// Token: 0x04001A7E RID: 6782
		private bool _noSqlPrimaryKeys;
	}
}
