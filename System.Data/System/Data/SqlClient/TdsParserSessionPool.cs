using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Data.SqlClient
{
	// Token: 0x02000332 RID: 818
	internal class TdsParserSessionPool
	{
		// Token: 0x06002A8A RID: 10890 RVA: 0x002BF038 File Offset: 0x002BE438
		internal TdsParserSessionPool(TdsParser parser)
		{
			this._parser = parser;
			this._cache = new List<TdsParserStateObject>();
			this._freeStack = new TdsParserSessionPool.TdsParserStateObjectListStack();
			if (Bid.AdvancedOn)
			{
				Bid.Trace("<sc.TdsParserSessionPool.ctor|ADV> %d# created session pool for parser %d\n", this.ObjectID, parser.ObjectID);
			}
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06002A8B RID: 10891 RVA: 0x002BF098 File Offset: 0x002BE498
		private bool IsDisposed
		{
			get
			{
				return null == this._freeStack;
			}
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06002A8C RID: 10892 RVA: 0x002BF0B8 File Offset: 0x002BE4B8
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x06002A8D RID: 10893 RVA: 0x002BF0D8 File Offset: 0x002BE4D8
		internal TdsParserStateObject CreateSession()
		{
			TdsParserStateObject tdsParserStateObject = this._parser.CreateSession();
			lock (this._cache)
			{
				if (Bid.AdvancedOn)
				{
					Bid.Trace("<sc.TdsParserSessionPool.CreateSession|ADV> %d# adding session %d to pool\n", this.ObjectID, tdsParserStateObject.ObjectID);
				}
				this._cache.Add(tdsParserStateObject);
				this._cachedCount = this._cache.Count;
			}
			return tdsParserStateObject;
		}

		// Token: 0x06002A8E RID: 10894 RVA: 0x002BF168 File Offset: 0x002BE568
		internal void Deactivate()
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<sc.TdsParserSessionPool.Deactivate|ADV> %d# deactivating cachedCount=%d\n", this.ObjectID, this._cachedCount);
			try
			{
				lock (this._cache)
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

		// Token: 0x06002A8F RID: 10895 RVA: 0x002BF238 File Offset: 0x002BE638
		internal void Dispose()
		{
			if (Bid.AdvancedOn)
			{
				Bid.Trace("<sc.TdsParserSessionPool.Dispose|ADV> %d# disposing cachedCount=%d\n", this.ObjectID, this._cachedCount);
			}
			this._freeStack = null;
			lock (this._cache)
			{
				for (int i = 0; i < this._cache.Count; i++)
				{
					TdsParserStateObject tdsParserStateObject = this._cache[i];
					if (tdsParserStateObject != null)
					{
						tdsParserStateObject.Dispose();
					}
				}
				this._cache.Clear();
			}
		}

		// Token: 0x06002A90 RID: 10896 RVA: 0x002BF2D8 File Offset: 0x002BE6D8
		internal TdsParserStateObject GetSession(object owner)
		{
			TdsParserStateObject tdsParserStateObject = this._freeStack.SynchronizedPop();
			if (tdsParserStateObject == null)
			{
				tdsParserStateObject = this.CreateSession();
			}
			tdsParserStateObject.Activate(owner);
			if (Bid.AdvancedOn)
			{
				Bid.Trace("<sc.TdsParserSessionPool.GetSession|ADV> %d# using session %d\n", this.ObjectID, tdsParserStateObject.ObjectID);
			}
			return tdsParserStateObject;
		}

		// Token: 0x06002A91 RID: 10897 RVA: 0x002BF328 File Offset: 0x002BE728
		internal void PutSession(TdsParserStateObject session)
		{
			bool flag = session.Deactivate();
			if (!this.IsDisposed)
			{
				if (flag && this._cachedCount < 10)
				{
					if (Bid.AdvancedOn)
					{
						Bid.Trace("<sc.TdsParserSessionPool.PutSession|ADV> %d# keeping session %d cachedCount=%d\n", this.ObjectID, session.ObjectID, this._cachedCount);
					}
					this._freeStack.SynchronizedPush(session);
					return;
				}
				if (Bid.AdvancedOn)
				{
					Bid.Trace("<sc.TdsParserSessionPool.PutSession|ADV> %d# disposing session %d cachedCount=%d\n", this.ObjectID, session.ObjectID, this._cachedCount);
				}
				lock (this._cache)
				{
					this._cache.Remove(session);
					this._cachedCount = this._cache.Count;
				}
				session.Dispose();
			}
		}

		// Token: 0x06002A92 RID: 10898 RVA: 0x002BF408 File Offset: 0x002BE808
		internal string TraceString()
		{
			return string.Format(null, "(ObjID={0}, free={1}, cached={2}, total={3})", new object[]
			{
				this._objectID,
				(this._freeStack == null) ? "(null)" : this._freeStack.CountDebugOnly.ToString(null),
				this._cachedCount,
				this._cache.Count
			});
		}

		// Token: 0x04001C08 RID: 7176
		private const int MaxInactiveCount = 10;

		// Token: 0x04001C09 RID: 7177
		private static int _objectTypeCount;

		// Token: 0x04001C0A RID: 7178
		private readonly int _objectID = Interlocked.Increment(ref TdsParserSessionPool._objectTypeCount);

		// Token: 0x04001C0B RID: 7179
		private readonly TdsParser _parser;

		// Token: 0x04001C0C RID: 7180
		private readonly List<TdsParserStateObject> _cache;

		// Token: 0x04001C0D RID: 7181
		private int _cachedCount;

		// Token: 0x04001C0E RID: 7182
		private TdsParserSessionPool.TdsParserStateObjectListStack _freeStack;

		// Token: 0x02000333 RID: 819
		private class TdsParserStateObjectListStack
		{
			// Token: 0x170006FB RID: 1787
			// (get) Token: 0x06002A93 RID: 10899 RVA: 0x002BF488 File Offset: 0x002BE888
			internal int CountDebugOnly
			{
				get
				{
					return -1;
				}
			}

			// Token: 0x06002A94 RID: 10900 RVA: 0x002BF498 File Offset: 0x002BE898
			internal TdsParserStateObjectListStack()
			{
			}

			// Token: 0x06002A95 RID: 10901 RVA: 0x002BF4B8 File Offset: 0x002BE8B8
			internal TdsParserStateObject SynchronizedPop()
			{
				TdsParserStateObject stack;
				lock (this)
				{
					stack = this._stack;
					if (stack != null)
					{
						this._stack = stack.NextPooledObject;
						stack.NextPooledObject = null;
					}
				}
				return stack;
			}

			// Token: 0x06002A96 RID: 10902 RVA: 0x002BF518 File Offset: 0x002BE918
			internal void SynchronizedPush(TdsParserStateObject value)
			{
				lock (this)
				{
					value.NextPooledObject = this._stack;
					this._stack = value;
				}
			}

			// Token: 0x04001C0F RID: 7183
			private TdsParserStateObject _stack;
		}
	}
}
