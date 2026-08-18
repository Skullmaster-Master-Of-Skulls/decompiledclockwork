using System;
using System.Threading;

namespace System.Net.Sockets
{
	// Token: 0x0200038D RID: 909
	internal class OverlappedCache
	{
		// Token: 0x06002232 RID: 8754 RVA: 0x000A3B3E File Offset: 0x000A1D3E
		internal OverlappedCache(Overlapped overlapped, object[] pinnedObjectsArray, IOCompletionCallback callback)
		{
			this.m_Overlapped = overlapped;
			this.m_PinnedObjects = pinnedObjectsArray;
			this.m_PinnedObjectsArray = pinnedObjectsArray;
			this.m_NativeOverlapped = new SafeNativeOverlapped(overlapped.UnsafePack(callback, pinnedObjectsArray));
		}

		// Token: 0x06002233 RID: 8755 RVA: 0x000A3B6E File Offset: 0x000A1D6E
		internal OverlappedCache(Overlapped overlapped, object pinnedObjects, IOCompletionCallback callback, bool alreadyTriedCast)
		{
			this.m_Overlapped = overlapped;
			this.m_PinnedObjects = pinnedObjects;
			this.m_PinnedObjectsArray = (alreadyTriedCast ? null : NclConstants.EmptyObjectArray);
			this.m_NativeOverlapped = new SafeNativeOverlapped(overlapped.UnsafePack(callback, pinnedObjects));
		}

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x06002234 RID: 8756 RVA: 0x000A3BA9 File Offset: 0x000A1DA9
		internal Overlapped Overlapped
		{
			get
			{
				return this.m_Overlapped;
			}
		}

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x06002235 RID: 8757 RVA: 0x000A3BB1 File Offset: 0x000A1DB1
		internal SafeNativeOverlapped NativeOverlapped
		{
			get
			{
				return this.m_NativeOverlapped;
			}
		}

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x06002236 RID: 8758 RVA: 0x000A3BB9 File Offset: 0x000A1DB9
		internal object PinnedObjects
		{
			get
			{
				return this.m_PinnedObjects;
			}
		}

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x06002237 RID: 8759 RVA: 0x000A3BC4 File Offset: 0x000A1DC4
		internal object[] PinnedObjectsArray
		{
			get
			{
				object[] array = this.m_PinnedObjectsArray;
				if (array != null && array.Length == 0)
				{
					array = (this.m_PinnedObjects as object[]);
					if (array != null && array.Length == 0)
					{
						this.m_PinnedObjectsArray = null;
					}
					else
					{
						this.m_PinnedObjectsArray = array;
					}
				}
				return this.m_PinnedObjectsArray;
			}
		}

		// Token: 0x06002238 RID: 8760 RVA: 0x000A3C08 File Offset: 0x000A1E08
		internal void Free()
		{
			this.InternalFree();
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002239 RID: 8761 RVA: 0x000A3C16 File Offset: 0x000A1E16
		private void InternalFree()
		{
			this.m_Overlapped = null;
			this.m_PinnedObjects = null;
			if (this.m_NativeOverlapped != null)
			{
				if (!this.m_NativeOverlapped.IsInvalid)
				{
					this.m_NativeOverlapped.Dispose();
				}
				this.m_NativeOverlapped = null;
			}
		}

		// Token: 0x0600223A RID: 8762 RVA: 0x000A3C50 File Offset: 0x000A1E50
		internal static void InterlockedFree(ref OverlappedCache overlappedCache)
		{
			OverlappedCache overlappedCache2 = (overlappedCache == null) ? null : Interlocked.Exchange<OverlappedCache>(ref overlappedCache, null);
			if (overlappedCache2 != null)
			{
				overlappedCache2.Free();
			}
		}

		// Token: 0x0600223B RID: 8763 RVA: 0x000A3C78 File Offset: 0x000A1E78
		~OverlappedCache()
		{
			if (!NclUtilities.HasShutdownStarted)
			{
				this.InternalFree();
			}
		}

		// Token: 0x04001F6B RID: 8043
		internal Overlapped m_Overlapped;

		// Token: 0x04001F6C RID: 8044
		internal SafeNativeOverlapped m_NativeOverlapped;

		// Token: 0x04001F6D RID: 8045
		internal object m_PinnedObjects;

		// Token: 0x04001F6E RID: 8046
		internal object[] m_PinnedObjectsArray;
	}
}
