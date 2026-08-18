using System;

namespace System.Data.SqlClient
{
	// Token: 0x0200023A RID: 570
	public class SqlEnclaveSession
	{
		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06002336 RID: 9014 RVA: 0x000F3938 File Offset: 0x000F2D38
		public long SessionId { get; }

		// Token: 0x06002337 RID: 9015 RVA: 0x000F394C File Offset: 0x000F2D4C
		public byte[] GetSessionKey()
		{
			return this.Clone(this._sessionKey);
		}

		// Token: 0x06002338 RID: 9016 RVA: 0x000F3968 File Offset: 0x000F2D68
		private byte[] Clone(byte[] arrayToClone)
		{
			byte[] array = new byte[arrayToClone.Length];
			for (int i = 0; i < arrayToClone.Length; i++)
			{
				array[i] = arrayToClone[i];
			}
			return array;
		}

		// Token: 0x06002339 RID: 9017 RVA: 0x000F3994 File Offset: 0x000F2D94
		public SqlEnclaveSession(byte[] sessionKey, long sessionId)
		{
			if (sessionKey == null)
			{
				throw SQL.NullArgumentInConstructorInternal(SqlEnclaveSession._sessionKeyName, SqlEnclaveSession._className);
			}
			if (sessionKey.Length == 0)
			{
				throw SQL.EmptyArgumentInConstructorInternal(SqlEnclaveSession._sessionKeyName, SqlEnclaveSession._className);
			}
			this._sessionKey = sessionKey;
			this.SessionId = sessionId;
		}

		// Token: 0x0400155B RID: 5467
		private static readonly string _sessionKeyName = "SessionKey";

		// Token: 0x0400155C RID: 5468
		private static readonly string _className = "EnclaveSession";

		// Token: 0x0400155D RID: 5469
		private readonly byte[] _sessionKey;
	}
}
