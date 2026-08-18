using System;
using System.Collections.Generic;

namespace System.Data.SqlClient
{
	// Token: 0x02000216 RID: 534
	internal struct SqlTceCipherInfoEntry
	{
		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x060021E0 RID: 8672 RVA: 0x000EBF6C File Offset: 0x000EB36C
		internal int Ordinal
		{
			get
			{
				return this._ordinal;
			}
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x060021E1 RID: 8673 RVA: 0x000EBF80 File Offset: 0x000EB380
		internal int DatabaseId
		{
			get
			{
				return this._databaseId;
			}
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x060021E2 RID: 8674 RVA: 0x000EBF94 File Offset: 0x000EB394
		internal int CekId
		{
			get
			{
				return this._cekId;
			}
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x060021E3 RID: 8675 RVA: 0x000EBFA8 File Offset: 0x000EB3A8
		internal int CekVersion
		{
			get
			{
				return this._cekVersion;
			}
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x060021E4 RID: 8676 RVA: 0x000EBFBC File Offset: 0x000EB3BC
		internal byte[] CekMdVersion
		{
			get
			{
				return this._cekMdVersion;
			}
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x060021E5 RID: 8677 RVA: 0x000EBFD0 File Offset: 0x000EB3D0
		internal List<SqlEncryptionKeyInfo> ColumnEncryptionKeyValues
		{
			get
			{
				return this._columnEncryptionKeyValues;
			}
		}

		// Token: 0x060021E6 RID: 8678 RVA: 0x000EBFE4 File Offset: 0x000EB3E4
		internal void Add(byte[] encryptedKey, int databaseId, int cekId, int cekVersion, byte[] cekMdVersion, string keyPath, string keyStoreName, string algorithmName)
		{
			SqlEncryptionKeyInfo item = default(SqlEncryptionKeyInfo);
			item.encryptedKey = encryptedKey;
			item.databaseId = databaseId;
			item.cekId = cekId;
			item.cekVersion = cekVersion;
			item.cekMdVersion = cekMdVersion;
			item.keyPath = keyPath;
			item.keyStoreName = keyStoreName;
			item.algorithmName = algorithmName;
			this._columnEncryptionKeyValues.Add(item);
			if (this._databaseId == 0)
			{
				this._databaseId = databaseId;
				this._cekId = cekId;
				this._cekVersion = cekVersion;
				this._cekMdVersion = cekMdVersion;
			}
		}

		// Token: 0x060021E7 RID: 8679 RVA: 0x000EC070 File Offset: 0x000EB470
		internal SqlTceCipherInfoEntry(int ordinal = 0)
		{
			this = default(SqlTceCipherInfoEntry);
			this._ordinal = ordinal;
			this._databaseId = 0;
			this._cekId = 0;
			this._cekVersion = 0;
			this._cekMdVersion = null;
			this._columnEncryptionKeyValues = new List<SqlEncryptionKeyInfo>();
		}

		// Token: 0x0400141C RID: 5148
		private readonly List<SqlEncryptionKeyInfo> _columnEncryptionKeyValues;

		// Token: 0x0400141D RID: 5149
		private readonly int _ordinal;

		// Token: 0x0400141E RID: 5150
		private int _databaseId;

		// Token: 0x0400141F RID: 5151
		private int _cekId;

		// Token: 0x04001420 RID: 5152
		private int _cekVersion;

		// Token: 0x04001421 RID: 5153
		private byte[] _cekMdVersion;
	}
}
