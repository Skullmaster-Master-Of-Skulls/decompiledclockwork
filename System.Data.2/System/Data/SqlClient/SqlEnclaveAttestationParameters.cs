using System;
using System.Security.Cryptography;

namespace System.Data.SqlClient
{
	// Token: 0x02000238 RID: 568
	public class SqlEnclaveAttestationParameters
	{
		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x0600231F RID: 8991 RVA: 0x000F30F8 File Offset: 0x000F24F8
		public int Protocol { get; }

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06002320 RID: 8992 RVA: 0x000F310C File Offset: 0x000F250C
		public ECDiffieHellmanCng ClientDiffieHellmanKey { get; }

		// Token: 0x06002321 RID: 8993 RVA: 0x000F3120 File Offset: 0x000F2520
		public byte[] GetInput()
		{
			return this.Clone(this._input);
		}

		// Token: 0x06002322 RID: 8994 RVA: 0x000F313C File Offset: 0x000F253C
		private byte[] Clone(byte[] arrayToClone)
		{
			if (arrayToClone == null)
			{
				return null;
			}
			byte[] array = new byte[arrayToClone.Length];
			for (int i = 0; i < arrayToClone.Length; i++)
			{
				array[i] = arrayToClone[i];
			}
			return array;
		}

		// Token: 0x06002323 RID: 8995 RVA: 0x000F316C File Offset: 0x000F256C
		public SqlEnclaveAttestationParameters(int protocol, byte[] input, ECDiffieHellmanCng clientDiffieHellmanKey)
		{
			if (clientDiffieHellmanKey == null)
			{
				throw SQL.NullArgumentInConstructorInternal(SqlEnclaveAttestationParameters._clientDiffieHellmanKeyName, SqlEnclaveAttestationParameters._className);
			}
			if (input == null)
			{
				throw SQL.NullArgumentInConstructorInternal(SqlEnclaveAttestationParameters._inputName, SqlEnclaveAttestationParameters._className);
			}
			this._input = input;
			this.Protocol = protocol;
			this.ClientDiffieHellmanKey = clientDiffieHellmanKey;
		}

		// Token: 0x0400154D RID: 5453
		private static readonly string _clientDiffieHellmanKeyName = "ClientDiffieHellmanKey";

		// Token: 0x0400154E RID: 5454
		private static readonly string _inputName = "input";

		// Token: 0x0400154F RID: 5455
		private static readonly string _className = "EnclaveAttestationParameters";

		// Token: 0x04001550 RID: 5456
		private readonly byte[] _input;
	}
}
