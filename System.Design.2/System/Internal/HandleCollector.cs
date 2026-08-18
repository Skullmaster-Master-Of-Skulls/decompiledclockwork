using System;
using System.Threading;

namespace System.Internal
{
	// Token: 0x02000397 RID: 919
	internal sealed class HandleCollector
	{
		// Token: 0x14000057 RID: 87
		// (add) Token: 0x06002570 RID: 9584 RVA: 0x000EB0A4 File Offset: 0x000E92A4
		// (remove) Token: 0x06002571 RID: 9585 RVA: 0x000EB0D8 File Offset: 0x000E92D8
		internal static event HandleChangeEventHandler HandleAdded;

		// Token: 0x14000058 RID: 88
		// (add) Token: 0x06002572 RID: 9586 RVA: 0x000EB10C File Offset: 0x000E930C
		// (remove) Token: 0x06002573 RID: 9587 RVA: 0x000EB140 File Offset: 0x000E9340
		internal static event HandleChangeEventHandler HandleRemoved;

		// Token: 0x06002574 RID: 9588 RVA: 0x000EB173 File Offset: 0x000E9373
		internal static IntPtr Add(IntPtr handle, int type)
		{
			HandleCollector.handleTypes[type - 1].Add(handle);
			return handle;
		}

		// Token: 0x06002575 RID: 9589 RVA: 0x000EB188 File Offset: 0x000E9388
		internal static void SuspendCollect()
		{
			object obj = HandleCollector.internalSyncObject;
			lock (obj)
			{
				HandleCollector.suspendCount++;
			}
		}

		// Token: 0x06002576 RID: 9590 RVA: 0x000EB1D0 File Offset: 0x000E93D0
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

		// Token: 0x06002577 RID: 9591 RVA: 0x000EB280 File Offset: 0x000E9480
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

		// Token: 0x06002578 RID: 9592 RVA: 0x000EB320 File Offset: 0x000E9520
		internal static IntPtr Remove(IntPtr handle, int type)
		{
			return HandleCollector.handleTypes[type - 1].Remove(handle);
		}

		// Token: 0x04001B55 RID: 6997
		private static HandleCollector.HandleType[] handleTypes;

		// Token: 0x04001B56 RID: 6998
		private static int handleTypeCount;

		// Token: 0x04001B57 RID: 6999
		private static int suspendCount;

		// Token: 0x04001B5A RID: 7002
		private static object internalSyncObject = new object();

		// Token: 0x020005AD RID: 1453
		private class HandleType
		{
			// Token: 0x060033C3 RID: 13251 RVA: 0x0011B27D File Offset: 0x0011947D
			internal HandleType(string name, int expense, int initialThreshHold)
			{
				this.name = name;
				this.initialThreshHold = initialThreshHold;
				this.threshHold = initialThreshHold;
				this.deltaPercent = 100 - expense;
			}

			// Token: 0x060033C4 RID: 13252 RVA: 0x0011B2A4 File Offset: 0x001194A4
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

			// Token: 0x060033C5 RID: 13253 RVA: 0x0011B370 File Offset: 0x00119570
			internal int GetHandleCount()
			{
				int result;
				lock (this)
				{
					result = this.handleCount;
				}
				return result;
			}

			// Token: 0x060033C6 RID: 13254 RVA: 0x0011B3B0 File Offset: 0x001195B0
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

			// Token: 0x060033C7 RID: 13255 RVA: 0x0011B42C File Offset: 0x0011962C
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

			// Token: 0x040022B6 RID: 8886
			internal readonly string name;

			// Token: 0x040022B7 RID: 8887
			private int initialThreshHold;

			// Token: 0x040022B8 RID: 8888
			private int threshHold;

			// Token: 0x040022B9 RID: 8889
			private int handleCount;

			// Token: 0x040022BA RID: 8890
			private readonly int deltaPercent;
		}
	}
}
