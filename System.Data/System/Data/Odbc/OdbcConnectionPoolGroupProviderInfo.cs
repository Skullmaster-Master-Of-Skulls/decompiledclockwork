using System;
using System.Data.ProviderBase;

namespace System.Data.Odbc
{
	// Token: 0x020001DF RID: 479
	internal sealed class OdbcConnectionPoolGroupProviderInfo : DbConnectionPoolGroupProviderInfo
	{
		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06001AAE RID: 6830 RVA: 0x0025F0B8 File Offset: 0x0025E4B8
		// (set) Token: 0x06001AAF RID: 6831 RVA: 0x0025F0D8 File Offset: 0x0025E4D8
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

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06001AB0 RID: 6832 RVA: 0x0025F0F8 File Offset: 0x0025E4F8
		// (set) Token: 0x06001AB1 RID: 6833 RVA: 0x0025F118 File Offset: 0x0025E518
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

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06001AB2 RID: 6834 RVA: 0x0025F138 File Offset: 0x0025E538
		internal bool HasQuoteChar
		{
			get
			{
				return this._hasQuoteChar;
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06001AB3 RID: 6835 RVA: 0x0025F158 File Offset: 0x0025E558
		internal bool HasEscapeChar
		{
			get
			{
				return this._hasEscapeChar;
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06001AB4 RID: 6836 RVA: 0x0025F178 File Offset: 0x0025E578
		// (set) Token: 0x06001AB5 RID: 6837 RVA: 0x0025F198 File Offset: 0x0025E598
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

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06001AB6 RID: 6838 RVA: 0x0025F1B8 File Offset: 0x0025E5B8
		// (set) Token: 0x06001AB7 RID: 6839 RVA: 0x0025F1D8 File Offset: 0x0025E5D8
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

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06001AB8 RID: 6840 RVA: 0x0025F1F8 File Offset: 0x0025E5F8
		// (set) Token: 0x06001AB9 RID: 6841 RVA: 0x0025F218 File Offset: 0x0025E618
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

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06001ABA RID: 6842 RVA: 0x0025F238 File Offset: 0x0025E638
		// (set) Token: 0x06001ABB RID: 6843 RVA: 0x0025F258 File Offset: 0x0025E658
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

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06001ABC RID: 6844 RVA: 0x0025F278 File Offset: 0x0025E678
		// (set) Token: 0x06001ABD RID: 6845 RVA: 0x0025F298 File Offset: 0x0025E698
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

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06001ABE RID: 6846 RVA: 0x0025F2B8 File Offset: 0x0025E6B8
		// (set) Token: 0x06001ABF RID: 6847 RVA: 0x0025F2D8 File Offset: 0x0025E6D8
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

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06001AC0 RID: 6848 RVA: 0x0025F2F8 File Offset: 0x0025E6F8
		// (set) Token: 0x06001AC1 RID: 6849 RVA: 0x0025F318 File Offset: 0x0025E718
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

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06001AC2 RID: 6850 RVA: 0x0025F338 File Offset: 0x0025E738
		// (set) Token: 0x06001AC3 RID: 6851 RVA: 0x0025F358 File Offset: 0x0025E758
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

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06001AC4 RID: 6852 RVA: 0x0025F378 File Offset: 0x0025E778
		// (set) Token: 0x06001AC5 RID: 6853 RVA: 0x0025F398 File Offset: 0x0025E798
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

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06001AC6 RID: 6854 RVA: 0x0025F3B8 File Offset: 0x0025E7B8
		// (set) Token: 0x06001AC7 RID: 6855 RVA: 0x0025F3D8 File Offset: 0x0025E7D8
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

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06001AC8 RID: 6856 RVA: 0x0025F3F8 File Offset: 0x0025E7F8
		// (set) Token: 0x06001AC9 RID: 6857 RVA: 0x0025F418 File Offset: 0x0025E818
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

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06001ACA RID: 6858 RVA: 0x0025F438 File Offset: 0x0025E838
		// (set) Token: 0x06001ACB RID: 6859 RVA: 0x0025F458 File Offset: 0x0025E858
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

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06001ACC RID: 6860 RVA: 0x0025F478 File Offset: 0x0025E878
		// (set) Token: 0x06001ACD RID: 6861 RVA: 0x0025F498 File Offset: 0x0025E898
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

		// Token: 0x04000FC6 RID: 4038
		private string _driverName;

		// Token: 0x04000FC7 RID: 4039
		private string _driverVersion;

		// Token: 0x04000FC8 RID: 4040
		private string _quoteChar;

		// Token: 0x04000FC9 RID: 4041
		private char _escapeChar;

		// Token: 0x04000FCA RID: 4042
		private bool _hasQuoteChar;

		// Token: 0x04000FCB RID: 4043
		private bool _hasEscapeChar;

		// Token: 0x04000FCC RID: 4044
		private bool _isV3Driver;

		// Token: 0x04000FCD RID: 4045
		private int _supportedSQLTypes;

		// Token: 0x04000FCE RID: 4046
		private int _testedSQLTypes;

		// Token: 0x04000FCF RID: 4047
		private int _restrictedSQLBindTypes;

		// Token: 0x04000FD0 RID: 4048
		private bool _noCurrentCatalog;

		// Token: 0x04000FD1 RID: 4049
		private bool _noConnectionDead;

		// Token: 0x04000FD2 RID: 4050
		private bool _noQueryTimeout;

		// Token: 0x04000FD3 RID: 4051
		private bool _noSqlSoptSSNoBrowseTable;

		// Token: 0x04000FD4 RID: 4052
		private bool _noSqlSoptSSHiddenColumns;

		// Token: 0x04000FD5 RID: 4053
		private bool _noSqlCASSColumnKey;

		// Token: 0x04000FD6 RID: 4054
		private bool _noSqlPrimaryKeys;
	}
}
