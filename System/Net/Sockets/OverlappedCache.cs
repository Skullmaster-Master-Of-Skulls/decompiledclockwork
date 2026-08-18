using System;
using System.Threading;

namespace System.Net.Sockets
{
	// Token: 0x020005CF RID: 1487
	internal class OverlappedCache
	{
		// Token: 0x06002EBF RID: 11967 RVA: 0x000CE3FE File Offset: 0x000CD3FE
		internal OverlappedCache(Overlapped overlapped, object[] pinnedObjectsArray, IOCompletionCallback callback)
		{
			this.m_Overlapped = overlapped;
			this.m_PinnedObjects = pinnedObjectsArray;
			this.m_PinnedObjectsArray = pinnedObjectsArray;
			this.m_NativeOverlapped = new SafeNativeOverlapped(overlapped.UnsafePack(callback, pinnedObjectsArray));
		}

		// Token: 0x06002EC0 RID: 11968 RVA: 0x000CE42E File Offset: 0x000CD42E
		internal OverlappedCache(Overlapped overlapped, object pinnedObjects, IOCompletionCallback callback, bool alreadyTriedCast)
		{
			this.m_Overlapped = overlapped;
			this.m_PinnedObjects = pinnedObjects;
			this.m_PinnedObjectsArray = (alreadyTriedCast ? null : NclConstants.EmptyObjectArray);
			this.m_NativeOverlapped = new SafeNativeOverlapped(overlapped.UnsafePack(callback, pinnedObjects));
		}

		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x06002EC1 RID: 11969 RVA: 0x000CE469 File Offset: 0x000CD469
		internal Overlapped Overlapped
		{
			get
			{
				return this.m_Overlapped;
			}
		}

		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x06002EC2 RID: 11970 RVA: 0x000CE471 File Offset: 0x000CD471
		internal SafeNativeOverlapped NativeOverlapped
		{
			get
			{
				return this.m_NativeOverlapped;
			}
		}

		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x06002EC3 RID: 11971 RVA: 0x000CE479 File Offset: 0x000CD479
		internal object PinnedObjects
		{
			get
			{
				return this.m_PinnedObjects;
			}
		}

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x06002EC4 RID: 11972 RVA: 0x000CE484 File Offset: 0x000CD484
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

		// Token: 0x06002EC5 RID: 11973 RVA: 0x000CE4CA File Offset: 0x000CD4CA
		internal void Free()
		{
			this.InternalFree();
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002EC6 RID: 11974 RVA: 0x000CE4D8 File Offset: 0x000CD4D8
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

		// Token: 0x06002EC7 RID: 11975 RVA: 0x000CE510 File Offset: 0x000CD510
		internal static void InterlockedFree(ref OverlappedCache overlappedCache)
		{
			OverlappedCache overlappedCache2 = (overlappedCache == null) ? null : Interlocked.Exchange<OverlappedCache>(ref overlappedCache, null);
			if (overlappedCache2 != null)
			{
				overlappedCache2.Free();
			}
		}

		// Token: 0x06002EC8 RID: 11976 RVA: 0x000CE538 File Offset: 0x000CD538
		~OverlappedCache()
		{
			if (!NclUtilities.HasShutdownStarted)
			{
				this.InternalFree();
			}
		}

		// Token: 0x04002C51 RID: 11345
		internal Overlapped m_Overlapped;

		// Token: 0x04002C52 RID: 11346
		internal SafeNativeOverlapped m_NativeOverlapped;

		// Token: 0x04002C53 RID: 11347
		internal object m_PinnedObjects;

		// Token: 0x04002C54 RID: 11348
		internal object[] m_PinnedObjectsArray;
	}
}
