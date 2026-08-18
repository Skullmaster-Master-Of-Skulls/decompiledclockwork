using System;
using System.Threading;

namespace System.Internal
{
	// Token: 0x020000FB RID: 251
	internal sealed class HandleCollector
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060003EA RID: 1002 RVA: 0x0000C6CC File Offset: 0x0000A8CC
		// (remove) Token: 0x060003EB RID: 1003 RVA: 0x0000C700 File Offset: 0x0000A900
		internal static event HandleChangeEventHandler HandleAdded;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060003EC RID: 1004 RVA: 0x0000C734 File Offset: 0x0000A934
		// (remove) Token: 0x060003ED RID: 1005 RVA: 0x0000C768 File Offset: 0x0000A968
		internal static event HandleChangeEventHandler HandleRemoved;

		// Token: 0x060003EE RID: 1006 RVA: 0x0000C79B File Offset: 0x0000A99B
		internal static IntPtr Add(IntPtr handle, int type)
		{
			HandleCollector.handleTypes[type - 1].Add(handle);
			return handle;
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0000C7B0 File Offset: 0x0000A9B0
		internal static void SuspendCollect()
		{
			object obj = HandleCollector.internalSyncObject;
			lock (obj)
			{
				HandleCollector.suspendCount++;
			}
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0000C7F8 File Offset: 0x0000A9F8
		internal static void ResumeCollect()
		{
			bool flag = false;
			object obj = HandleCollector.internalSyncObject;
			lock (obj)
			{
				if (HandleCollector.suspendCount > 0)
				{
					HandleCollector.suspendCount--;
				}
				if (HandleCollector.suspendCount == 0)
				{
					for (int i = 0; i < HandleCollector.handleTypeCount; i++)
					{
						HandleCollector.HandleType obj2 = HandleCollector.handleTypes[i];
						lock (obj2)
						{
							if (HandleCollector.handleTypes[i].NeedCollection())
							{
								flag = true;
							}
						}
					}
				}
			}
			if (flag)
			{
				GC.Collect();
			}
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0000C8A8 File Offset: 0x0000AAA8
		internal static int RegisterType(string typeName, int expense, int initialThreshold)
		{
			object obj = HandleCollector.internalSyncObject;
			int result;
			lock (obj)
			{
				if (HandleCollector.handleTypeCount == 0 || HandleCollector.handleTypeCount == HandleCollector.handleTypes.Length)
				{
					HandleCollector.HandleType[] destinationArray = new HandleCollector.HandleType[HandleCollector.handleTypeCount + 10];
					if (HandleCollector.handleTypes != null)
					{
						Array.Copy(HandleCollector.handleTypes, 0, destinationArray, 0, HandleCollector.handleTypeCount);
					}
					HandleCollector.handleTypes = destinationArray;
				}
				HandleCollector.handleTypes[HandleCollector.handleTypeCount++] = new HandleCollector.HandleType(typeName, expense, initialThreshold);
				result = HandleCollector.handleTypeCount;
			}
			return result;
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0000C948 File Offset: 0x0000AB48
		internal static IntPtr Remove(IntPtr handle, int type)
		{
			return HandleCollector.handleTypes[type - 1].Remove(handle);
		}

		// Token: 0x04000437 RID: 1079
		private static HandleCollector.HandleType[] handleTypes;

		// Token: 0x04000438 RID: 1080
		private static int handleTypeCount;

		// Token: 0x04000439 RID: 1081
		private static int suspendCount;

		// Token: 0x0400043C RID: 1084
		private static object internalSyncObject = new object();

		// Token: 0x02000547 RID: 1351
		private class HandleType
		{
			// Token: 0x0600556F RID: 21871 RVA: 0x00166500 File Offset: 0x00164700
			internal HandleType(string name, int expense, int initialThreshHold)
			{
				this.name = name;
				this.initialThreshHold = initialThreshHold;
				this.threshHold = initialThreshHold;
				this.deltaPercent = 100 - expense;
			}

			// Token: 0x06005570 RID: 21872 RVA: 0x00166528 File Offset: 0x00164728
			internal void Add(IntPtr handle)
			{
				if (handle == IntPtr.Zero)
				{
					return;
				}
				bool flag = false;
				int currentHandleCount = 0;
				lock (this)
				{
					this.handleCount++;
					flag = this.NeedCollection();
					currentHandleCount = this.handleCount;
				}
				object internalSyncObject = HandleCollector.internalSyncObject;
				lock (internalSyncObject)
				{
					if (HandleCollector.HandleAdded != null)
					{
						HandleCollector.HandleAdded(this.name, handle, currentHandleCount);
					}
				}
				if (!flag)
				{
					return;
				}
				if (flag)
				{
					GC.Collect();
					int millisecondsTimeout = (100 - this.deltaPercent) / 4;
					Thread.Sleep(millisecondsTimeout);
				}
			}

			// Token: 0x06005571 RID: 21873 RVA: 0x001665F4 File Offset: 0x001647F4
			internal int GetHandleCount()
			{
				int result;
				lock (this)
				{
					result = this.handleCount;
				}
				return result;
			}

			// Token: 0x06005572 RID: 21874 RVA: 0x00166634 File Offset: 0x00164834
			internal bool NeedCollection()
			{
				if (HandleCollector.suspendCount > 0)
				{
					return false;
				}
				if (this.handleCount > this.threshHold)
				{
					this.threshHold = this.handleCount + this.handleCount * this.deltaPercent / 100;
					return true;
				}
				int num = 100 * this.threshHold / (100 + this.deltaPercent);
				if (num >= this.initialThreshHold && this.handleCount < (int)((float)num * 0.9f))
				{
					this.threshHold = num;
				}
				return false;
			}

			// Token: 0x06005573 RID: 21875 RVA: 0x001666B0 File Offset: 0x001648B0
			internal IntPtr Remove(IntPtr handle)
			{
				if (handle == IntPtr.Zero)
				{
					return handle;
				}
				int currentHandleCount = 0;
				lock (this)
				{
					this.handleCount--;
					if (this.handleCount < 0)
					{
						this.handleCount = 0;
					}
					currentHandleCount = this.handleCount;
				}
				object internalSyncObject = HandleCollector.internalSyncObject;
				lock (internalSyncObject)
				{
					if (HandleCollector.HandleRemoved != null)
					{
						HandleCollector.HandleRemoved(this.name, handle, currentHandleCount);
					}
				}
				return handle;
			}

			// Token: 0x0400380E RID: 14350
			internal readonly string name;

			// Token: 0x0400380F RID: 14351
			private int initialThreshHold;

			// Token: 0x04003810 RID: 14352
			private int threshHold;

			// Token: 0x04003811 RID: 14353
			private int handleCount;

			// Token: 0x04003812 RID: 14354
			private readonly int deltaPercent;
		}
	}
}
