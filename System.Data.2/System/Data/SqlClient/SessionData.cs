using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Data.SqlClient
{
	// Token: 0x020001D5 RID: 469
	internal class SessionData
	{
		// Token: 0x06001D97 RID: 7575 RVA: 0x000CFFE0 File Offset: 0x000CF3E0
		public SessionData(SessionData recoveryData)
		{
			this._initialDatabase = recoveryData._initialDatabase;
			this._initialCollation = recoveryData._initialCollation;
			this._initialLanguage = recoveryData._initialLanguage;
			this._resolvedAliases = recoveryData._resolvedAliases;
			for (int i = 0; i < 256; i++)
			{
				if (recoveryData._initialState[i] != null)
				{
					this._initialState[i] = (byte[])recoveryData._initialState[i].Clone();
				}
			}
		}

		// Token: 0x06001D98 RID: 7576 RVA: 0x000D0078 File Offset: 0x000CF478
		public SessionData()
		{
			this._resolvedAliases = new Dictionary<string, Tuple<string, string>>(2);
		}

		// Token: 0x06001D99 RID: 7577 RVA: 0x000D00B8 File Offset: 0x000CF4B8
		public void Reset()
		{
			this._database = null;
			this._collation = null;
			this._language = null;
			if (this._deltaDirty)
			{
				this._delta = new SessionStateRecord[256];
				this._deltaDirty = false;
			}
			this._unrecoverableStatesCount = 0;
		}

		// Token: 0x06001D9A RID: 7578 RVA: 0x000D0100 File Offset: 0x000CF500
		[Conditional("DEBUG")]
		public void AssertUnrecoverableStateCountIsCorrect()
		{
			byte b = 0;
			foreach (SessionStateRecord sessionStateRecord in this._delta)
			{
				if (sessionStateRecord != null && !sessionStateRecord._recoverable)
				{
					b += 1;
				}
			}
		}

		// Token: 0x040010D2 RID: 4306
		internal const int _maxNumberOfSessionStates = 256;

		// Token: 0x040010D3 RID: 4307
		internal uint _tdsVersion;

		// Token: 0x040010D4 RID: 4308
		internal bool _encrypted;

		// Token: 0x040010D5 RID: 4309
		internal string _database;

		// Token: 0x040010D6 RID: 4310
		internal SqlCollation _collation;

		// Token: 0x040010D7 RID: 4311
		internal string _language;

		// Token: 0x040010D8 RID: 4312
		internal string _initialDatabase;

		// Token: 0x040010D9 RID: 4313
		internal SqlCollation _initialCollation;

		// Token: 0x040010DA RID: 4314
		internal string _initialLanguage;

		// Token: 0x040010DB RID: 4315
		internal byte _unrecoverableStatesCount;

		// Token: 0x040010DC RID: 4316
		internal Dictionary<string, Tuple<string, string>> _resolvedAliases;

		// Token: 0x040010DD RID: 4317
		internal SessionStateRecord[] _delta = new SessionStateRecord[256];

		// Token: 0x040010DE RID: 4318
		internal bool _deltaDirty;

		// Token: 0x040010DF RID: 4319
		internal byte[][] _initialState = new byte[256][];
	}
}
