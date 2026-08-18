using System;

namespace System.Data.SqlClient
{
	// Token: 0x02000222 RID: 546
	internal class SqlCipherMetadata
	{
		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06002213 RID: 8723 RVA: 0x000ECB2C File Offset: 0x000EBF2C
		// (set) Token: 0x06002214 RID: 8724 RVA: 0x000ECB40 File Offset: 0x000EBF40
		internal SqlTceCipherInfoEntry? EncryptionInfo
		{
			get
			{
				return this._sqlTceCipherInfoEntry;
			}
			set
			{
				this._sqlTceCipherInfoEntry = value;
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06002215 RID: 8725 RVA: 0x000ECB54 File Offset: 0x000EBF54
		internal byte CipherAlgorithmId
		{
			get
			{
				return this._cipherAlgorithmId;
			}
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06002216 RID: 8726 RVA: 0x000ECB68 File Offset: 0x000EBF68
		internal string CipherAlgorithmName
		{
			get
			{
				return this._cipherAlgorithmName;
			}
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06002217 RID: 8727 RVA: 0x000ECB7C File Offset: 0x000EBF7C
		internal byte EncryptionType
		{
			get
			{
				return this._encryptionType;
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06002218 RID: 8728 RVA: 0x000ECB90 File Offset: 0x000EBF90
		internal byte NormalizationRuleVersion
		{
			get
			{
				return this._normalizationRuleVersion;
			}
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06002219 RID: 8729 RVA: 0x000ECBA4 File Offset: 0x000EBFA4
		// (set) Token: 0x0600221A RID: 8730 RVA: 0x000ECBB8 File Offset: 0x000EBFB8
		internal SqlClientEncryptionAlgorithm CipherAlgorithm
		{
			get
			{
				return this._sqlClientEncryptionAlgorithm;
			}
			set
			{
				this._sqlClientEncryptionAlgorithm = value;
			}
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x0600221B RID: 8731 RVA: 0x000ECBCC File Offset: 0x000EBFCC
		// (set) Token: 0x0600221C RID: 8732 RVA: 0x000ECBE0 File Offset: 0x000EBFE0
		internal SqlEncryptionKeyInfo? EncryptionKeyInfo
		{
			get
			{
				return this._sqlEncryptionKeyInfo;
			}
			set
			{
				this._sqlEncryptionKeyInfo = value;
			}
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x0600221D RID: 8733 RVA: 0x000ECBF4 File Offset: 0x000EBFF4
		internal ushort CekTableOrdinal
		{
			get
			{
				return this._ordinal;
			}
		}

		// Token: 0x0600221E RID: 8734 RVA: 0x000ECC08 File Offset: 0x000EC008
		internal SqlCipherMetadata(SqlTceCipherInfoEntry? sqlTceCipherInfoEntry, ushort ordinal, byte cipherAlgorithmId, string cipherAlgorithmName, byte encryptionType, byte normalizationRuleVersion)
		{
			this._sqlTceCipherInfoEntry = sqlTceCipherInfoEntry;
			this._ordinal = ordinal;
			this._cipherAlgorithmId = cipherAlgorithmId;
			this._cipherAlgorithmName = cipherAlgorithmName;
			this._encryptionType = encryptionType;
			this._normalizationRuleVersion = normalizationRuleVersion;
			this._sqlEncryptionKeyInfo = null;
		}

		// Token: 0x0600221F RID: 8735 RVA: 0x000ECC54 File Offset: 0x000EC054
		internal bool IsAlgorithmInitialized()
		{
			return this._sqlClientEncryptionAlgorithm != null;
		}

		// Token: 0x04001471 RID: 5233
		private SqlTceCipherInfoEntry? _sqlTceCipherInfoEntry;

		// Token: 0x04001472 RID: 5234
		private readonly byte _cipherAlgorithmId;

		// Token: 0x04001473 RID: 5235
		private readonly string _cipherAlgorithmName;

		// Token: 0x04001474 RID: 5236
		private readonly byte _encryptionType;

		// Token: 0x04001475 RID: 5237
		private readonly byte _normalizationRuleVersion;

		// Token: 0x04001476 RID: 5238
		private SqlClientEncryptionAlgorithm _sqlClientEncryptionAlgorithm;

		// Token: 0x04001477 RID: 5239
		private SqlEncryptionKeyInfo? _sqlEncryptionKeyInfo;

		// Token: 0x04001478 RID: 5240
		private readonly ushort _ordinal;
	}
}
