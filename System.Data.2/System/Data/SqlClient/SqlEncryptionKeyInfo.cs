using System;

namespace System.Data.SqlClient
{
	// Token: 0x02000215 RID: 533
	internal struct SqlEncryptionKeyInfo
	{
		// Token: 0x04001413 RID: 5139
		internal byte[] encryptedKey;

		// Token: 0x04001414 RID: 5140
		internal int databaseId;

		// Token: 0x04001415 RID: 5141
		internal int cekId;

		// Token: 0x04001416 RID: 5142
		internal int cekVersion;

		// Token: 0x04001417 RID: 5143
		internal byte[] cekMdVersion;

		// Token: 0x04001418 RID: 5144
		internal string keyPath;

		// Token: 0x04001419 RID: 5145
		internal string keyStoreName;

		// Token: 0x0400141A RID: 5146
		internal string algorithmName;

		// Token: 0x0400141B RID: 5147
		internal byte normalizationRuleVersion;
	}
}
