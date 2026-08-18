using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading;

namespace System.Data.SqlClient
{
	// Token: 0x0200022C RID: 556
	internal class TdsParserSessionPool
	{
		// Token: 0x0600224D RID: 8781 RVA: 0x000ED694 File Offset: 0x000ECA94
		internal TdsParserSessionPool(TdsParser parser)
		{
			this._parser = parser;
			this._cache = new List<TdsParserStateObject>();
			this._freeStateObjects = new TdsParserStateObject[10];
			this._freeStateObjectCount = 0;
			if (Bid.AdvancedOn)
			{
				Bid.Trace("<sc.TdsParserSessionPool.ctor|ADV> %d# created session pool for parser %d\n", this.ObjectID, parser.ObjectID);
			}
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x0600224E RID: 8782 RVA: 0x000ED6FC File Offset: 0x000ECAFC
		private bool IsDisposed
		{
			get
			{
				return this._freeStateObjects == null;
			}
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x0600224F RID: 8783 RVA: 0x000ED714 File Offset: 0x000ECB14
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x06002250 RID: 8784 RVA: 0x000ED728 File Offset: 0x000ECB28
		internal void Deactivate()
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<sc.TdsParserSessionPool.Deactivate|ADV> %d# deactivating cachedCount=%d\n", this.ObjectID, this._cachedCount);
			try
			{
				List<TdsParserStateObject> cache = this._cache;
				lock (cache)
				{
					for (int i = this._cache.Count - 1; i >= 0; i--)
					{
						TdsParserStateObject tdsParserStateObject = this._cache[i];
						if (tdsParserStateObject != null && tdsParserStateObject.IsOrphaned)
						{
							if (Bid.AdvancedOn)
							{
								Bid.Trace("<sc.TdsParserSessionPool.Deactivate|ADV> %d# reclaiming session %d\n", this.ObjectID, tdsParserStateObject.ObjectID);
							}
							this.PutSession(tdsParserStateObject);
						}
					}
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06002251 RID: 8785 RVA: 0x000ED7FC File Offset: 0x000ECBFC
		internal void BestEffortCleanup()
		{
			for (int i = 0; i < this._cache.Count; i++)
			{
				TdsParserStateObject tdsParserStateObject = this._cache[i];
				if (tdsParserStateObject != null)
				{
					SNIHandle handle = tdsParserStateObject.Handle;
					if (handle != null)
					{
						handle.Dispose();
					}
				}
			}
		}

		// Token: 0x06002252 RID: 8786 RVA: 0x000ED840 File Offset: 0x000ECC40
		internal void Dispose()
		{
			if (Bid.AdvancedOn)
			{
				Bid.Trace("<sc.TdsParserSessionPool.Dispose|ADV> %d# disposing cachedCount=%d\n", this.ObjectID, this._cachedCount);
			}
			List<TdsParserStateObject> cache = this._cache;
			lock (cache)
			{
				for (int i = 0; i < this._freeStateObjectCount; i++)
				{
					if (this._freeStateObjects[i] != null)
					{
						this._freeStateObjects[i].Dispose();
					}
				}
				this._freeStateObjects = null;
				this._freeStateObjectCount = 0;
				for (int j = 0; j < this._cache.Count; j++)
				{
					if (this._cache[j] != null)
					{
						if (this._cache[j].IsOrphaned)
						{
							this._cache[j].Dispose();
						}
						else
						{
							this._cache[j].DecrementPendingCallbacks(false);
						}
					}
				}
				this._cache.Clear();
				this._cachedCount = 0;
			}
		}

		// Token: 0x06002253 RID: 8787 RVA: 0x000ED948 File Offset: 0x000ECD48
		internal TdsParserStateObject GetSession(object owner)
		{
			List<TdsParserStateObject> cache = this._cache;
			TdsParserStateObject tdsParserStateObject;
			lock (cache)
			{
				if (this.IsDisposed)
				{
					throw ADP.ClosedConnectionError();
				}
				if (this._freeStateObjectCount > 0)
				{
					this._freeStateObjectCount--;
					tdsParserStateObject = this._freeStateObjects[this._freeStateObjectCount];
					this._freeStateObjects[this._freeStateObjectCount] = null;
				}
				else
				{
					tdsParserStateObject = this._parser.CreateSession();
					if (Bid.AdvancedOn)
					{
						Bid.Trace("<sc.TdsParserSessionPool.CreateSession|ADV> %d# adding session %d to pool\n", this.ObjectID, tdsParserStateObject.ObjectID);
					}
					this._cache.Add(tdsParserStateObject);
					this._cachedCount = this._cache.Count;
				}
				tdsParserStateObject.Activate(owner);
			}
			if (Bid.AdvancedOn)
			{
				Bid.Trace("<sc.TdsParserSessionPool.GetSession|ADV> %d# using session %d\n", this.ObjectID, tdsParserStateObject.ObjectID);
			}
			return tdsParserStateObject;
		}

		// Token: 0x06002254 RID: 8788 RVA: 0x000EDA3C File Offset: 0x000ECE3C
		internal void PutSession(TdsParserStateObject session)
		{
			bool flag = session.Deactivate();
			List<TdsParserStateObject> cache = this._cache;
			lock (cache)
			{
				if (this.IsDisposed)
				{
					session.Dispose();
				}
				else if (flag && this._freeStateObjectCount < 10)
				{
					if (Bid.AdvancedOn)
					{
						Bid.Trace("<sc.TdsParserSessionPool.PutSession|ADV> %d# keeping session %d cachedCount=%d\n", this.ObjectID, session.ObjectID, this._cachedCount);
					}
					this._freeStateObjects[this._freeStateObjectCount] = session;
					this._freeStateObjectCount++;
				}
				else
				{
					if (Bid.AdvancedOn)
					{
						Bid.Trace("<sc.TdsParserSessionPool.PutSession|ADV> %d# disposing session %d cachedCount=%d\n", this.ObjectID, session.ObjectID, this._cachedCount);
					}
					bool flag3 = this._cache.Remove(session);
					this._cachedCount = this._cache.Count;
					session.Dispose();
				}
				session.RemoveOwner();
			}
		}

		// Token: 0x06002255 RID: 8789 RVA: 0x000EDB38 File Offset: 0x000ECF38
		internal string TraceString()
		{
			return string.Format(null, "(ObjID={0}, free={1}, cached={2}, total={3})", new object[]
			{
				this._objectID,
				(this._freeStateObjects == null) ? "(null)" : this._freeStateObjectCount.ToString(null),
				this._cachedCount,
				this._cache.Count
			});
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06002256 RID: 8790 RVA: 0x000EDBA4 File Offset: 0x000ECFA4
		internal int ActiveSessionsCount
		{
			get
			{
				return this._cachedCount - this._freeStateObjectCount;
			}
		}

		// Token: 0x040014B7 RID: 5303
		private const int MaxInactiveCount = 10;

		// Token: 0x040014B8 RID: 5304
		private static int _objectTypeCount;

		// Token: 0x040014B9 RID: 5305
		private readonly int _objectID = Interlocked.Increment(ref TdsParserSessionPool._objectTypeCount);

		// Token: 0x040014BA RID: 5306
		private readonly TdsParser _parser;

		// Token: 0x040014BB RID: 5307
		private readonly List<TdsParserStateObject> _cache;

		// Token: 0x040014BC RID: 5308
		private int _cachedCount;

		// Token: 0x040014BD RID: 5309
		private TdsParserStateObject[] _freeStateObjects;

		// Token: 0x040014BE RID: 5310
		private int _freeStateObjectCount;
	}
}
