using System;
using System.Runtime.ConstrainedExecution;
using System.Threading;

namespace System.Data.ProviderBase
{
	// Token: 0x020002C2 RID: 706
	internal sealed class DbConnectionPoolAuthenticationContext
	{
		// Token: 0x06002AE2 RID: 10978 RVA: 0x0011A134 File Offset: 0x00119534
		internal DbConnectionPoolAuthenticationContext(byte[] accessToken, DateTime expirationTime)
		{
			this._accessToken = accessToken;
			this._expirationTime = expirationTime;
			this._isUpdateInProgress = 0;
		}

		// Token: 0x06002AE3 RID: 10979 RVA: 0x0011A15C File Offset: 0x0011955C
		internal static DbConnectionPoolAuthenticationContext ChooseAuthenticationContextToUpdate(DbConnectionPoolAuthenticationContext context1, DbConnectionPoolAuthenticationContext context2)
		{
			if (!(context1.ExpirationTime > context2.ExpirationTime))
			{
				return context2;
			}
			return context1;
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06002AE4 RID: 10980 RVA: 0x0011A180 File Offset: 0x00119580
		internal byte[] AccessToken
		{
			get
			{
				return this._accessToken;
			}
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06002AE5 RID: 10981 RVA: 0x0011A194 File Offset: 0x00119594
		internal DateTime ExpirationTime
		{
			get
			{
				return this._expirationTime;
			}
		}

		// Token: 0x06002AE6 RID: 10982 RVA: 0x0011A1A8 File Offset: 0x001195A8
		internal bool LockToUpdate()
		{
			int num = Interlocked.CompareExchange(ref this._isUpdateInProgress, 1, 0);
			return num == 0;
		}

		// Token: 0x06002AE7 RID: 10983 RVA: 0x0011A1C8 File Offset: 0x001195C8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal void ReleaseLockToUpdate()
		{
			int num = Interlocked.CompareExchange(ref this._isUpdateInProgress, 0, 1);
		}

		// Token: 0x04001B5F RID: 7007
		private const int STATUS_LOCKED = 1;

		// Token: 0x04001B60 RID: 7008
		private const int STATUS_UNLOCKED = 0;

		// Token: 0x04001B61 RID: 7009
		private readonly byte[] _accessToken;

		// Token: 0x04001B62 RID: 7010
		private readonly DateTime _expirationTime;

		// Token: 0x04001B63 RID: 7011
		private int _isUpdateInProgress;
	}
}
