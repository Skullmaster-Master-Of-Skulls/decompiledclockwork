using System;
using System.Threading;

namespace System.Transactions
{
	// Token: 0x02000045 RID: 69
	internal abstract class DurableEnlistmentState : EnlistmentState
	{
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000213 RID: 531 RVA: 0x00031464 File Offset: 0x00030864
		internal static DurableEnlistmentActive _DurableEnlistmentActive
		{
			get
			{
				if (DurableEnlistmentState._durableEnlistmentActive == null)
				{
					lock (DurableEnlistmentState.ClassSyncObject)
					{
						if (DurableEnlistmentState._durableEnlistmentActive == null)
						{
							DurableEnlistmentActive durableEnlistmentActive = new DurableEnlistmentActive();
							Thread.MemoryBarrier();
							DurableEnlistmentState._durableEnlistmentActive = durableEnlistmentActive;
						}
					}
				}
				return DurableEnlistmentState._durableEnlistmentActive;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000214 RID: 532 RVA: 0x000314D4 File Offset: 0x000308D4
		protected static DurableEnlistmentAborting _DurableEnlistmentAborting
		{
			get
			{
				if (DurableEnlistmentState._durableEnlistmentAborting == null)
				{
					lock (DurableEnlistmentState.ClassSyncObject)
					{
						if (DurableEnlistmentState._durableEnlistmentAborting == null)
						{
							DurableEnlistmentAborting durableEnlistmentAborting = new DurableEnlistmentAborting();
							Thread.MemoryBarrier();
							DurableEnlistmentState._durableEnlistmentAborting = durableEnlistmentAborting;
						}
					}
				}
				return DurableEnlistmentState._durableEnlistmentAborting;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000215 RID: 533 RVA: 0x00031544 File Offset: 0x00030944
		protected static DurableEnlistmentCommitting _DurableEnlistmentCommitting
		{
			get
			{
				if (DurableEnlistmentState._durableEnlistmentCommitting == null)
				{
					lock (DurableEnlistmentState.ClassSyncObject)
					{
						if (DurableEnlistmentState._durableEnlistmentCommitting == null)
						{
							DurableEnlistmentCommitting durableEnlistmentCommitting = new DurableEnlistmentCommitting();
							Thread.MemoryBarrier();
							DurableEnlistmentState._durableEnlistmentCommitting = durableEnlistmentCommitting;
						}
					}
				}
				return DurableEnlistmentState._durableEnlistmentCommitting;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000216 RID: 534 RVA: 0x000315B4 File Offset: 0x000309B4
		protected static DurableEnlistmentDelegated _DurableEnlistmentDelegated
		{
			get
			{
				if (DurableEnlistmentState._durableEnlistmentDelegated == null)
				{
					lock (DurableEnlistmentState.ClassSyncObject)
					{
						if (DurableEnlistmentState._durableEnlistmentDelegated == null)
						{
							DurableEnlistmentDelegated durableEnlistmentDelegated = new DurableEnlistmentDelegated();
							Thread.MemoryBarrier();
							DurableEnlistmentState._durableEnlistmentDelegated = durableEnlistmentDelegated;
						}
					}
				}
				return DurableEnlistmentState._durableEnlistmentDelegated;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000217 RID: 535 RVA: 0x00031624 File Offset: 0x00030A24
		protected static DurableEnlistmentEnded _DurableEnlistmentEnded
		{
			get
			{
				if (DurableEnlistmentState._durableEnlistmentEnded == null)
				{
					lock (DurableEnlistmentState.ClassSyncObject)
					{
						if (DurableEnlistmentState._durableEnlistmentEnded == null)
						{
							DurableEnlistmentEnded durableEnlistmentEnded = new DurableEnlistmentEnded();
							Thread.MemoryBarrier();
							DurableEnlistmentState._durableEnlistmentEnded = durableEnlistmentEnded;
						}
					}
				}
				return DurableEnlistmentState._durableEnlistmentEnded;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00031694 File Offset: 0x00030A94
		private static object ClassSyncObject
		{
			get
			{
				if (DurableEnlistmentState.classSyncObject == null)
				{
					object value = new object();
					Interlocked.CompareExchange(ref DurableEnlistmentState.classSyncObject, value, null);
				}
				return DurableEnlistmentState.classSyncObject;
			}
		}

		// Token: 0x040000F8 RID: 248
		private static DurableEnlistmentActive _durableEnlistmentActive;

		// Token: 0x040000F9 RID: 249
		private static DurableEnlistmentAborting _durableEnlistmentAborting;

		// Token: 0x040000FA RID: 250
		private static DurableEnlistmentCommitting _durableEnlistmentCommitting;

		// Token: 0x040000FB RID: 251
		private static DurableEnlistmentDelegated _durableEnlistmentDelegated;

		// Token: 0x040000FC RID: 252
		private static DurableEnlistmentEnded _durableEnlistmentEnded;

		// Token: 0x040000FD RID: 253
		private static object classSyncObject;
	}
}
