using System;
using System.Security.Cryptography;

namespace System.Data.SqlClient
{
	// Token: 0x0200023B RID: 571
	public abstract class SqlColumnEncryptionEnclaveProvider
	{
		// Token: 0x0600233B RID: 9019
		public abstract void GetEnclaveSession(string serverName, string attestationUrl, out SqlEnclaveSession sqlEnclaveSession, out long counter);

		// Token: 0x0600233C RID: 9020
		public abstract SqlEnclaveAttestationParameters GetAttestationParameters();

		// Token: 0x0600233D RID: 9021
		public abstract void CreateEnclaveSession(byte[] enclaveAttestationInfo, ECDiffieHellmanCng clientDiffieHellmanKey, string attestationUrl, string servername, out SqlEnclaveSession sqlEnclaveSession, out long counter);

		// Token: 0x0600233E RID: 9022
		public abstract void InvalidateEnclaveSession(string serverName, string enclaveAttestationUrl, SqlEnclaveSession enclaveSession);
	}
}
