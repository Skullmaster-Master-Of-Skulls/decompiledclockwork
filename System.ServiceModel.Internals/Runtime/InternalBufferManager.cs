using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Runtime
{
	// Token: 0x0200001E RID: 30
	internal abstract class InternalBufferManager
	{
		// Token: 0x060000F0 RID: 240
		public abstract byte[] TakeBuffer(int bufferSize);

		// Token: 0x060000F1 RID: 241
		public abstract void ReturnBuffer(byte[] buffer);

		// Token: 0x060000F2 RID: 242
		public abstract void Clear();

		// Token: 0x060000F3 RID: 243 RVA: 0x00005080 File Offset: 0x00003280
		public static InternalBufferManager Create(long maxBufferPoolSize, int maxBufferSize)
		{
			if (maxBufferPoolSize == 0L)
			{
				return InternalBufferManager.GCBufferManager.Value;
			}
			return new InternalBufferManager.PooledBufferManager(maxBufferPoolSize, maxBufferSize);
		}

		// Token: 0x02000075 RID: 117
		private class PooledBufferManager : InternalBufferManager
		{
			// Token: 0x060003C9 RID: 969 RVA: 0x0001228C File Offset: 0x0001048C
			public PooledBufferManager(long maxMemoryToPool, int maxBufferSize)
			{
				this.tuningLock = new object();
				this.memoryLimit = maxMemoryToPool;
				this.remainingMemory = maxMemoryToPool;
				List<InternalBufferManager.PooledBufferManager.BufferPool> list = new List<InternalBufferManager.PooledBufferManager.BufferPool>();
				int num = 128;
				for (;;)
				{
					long num2 = this.remainingMemory / (long)num;
					int num3 = (num2 > 2147483647L) ? int.MaxValue : ((int)num2);
					if (num3 > 1)
					{
						num3 = 1;
					}
					list.Add(InternalBufferManager.PooledBufferManager.BufferPool.CreatePool(num, num3));
					this.remainingMemory -= (long)num3 * (long)num;
					if (num >= maxBufferSize)
					{
						break;
					}
					long num4 = (long)num * 2L;
					if (num4 > (long)maxBufferSize)
					{
						num = maxBufferSize;
					}
					else
					{
						num = (int)num4;
					}
				}
				this.bufferPools = list.ToArray();
				this.bufferSizes = new int[this.bufferPools.Length];
				for (int i = 0; i < this.bufferPools.Length; i++)
				{
					this.bufferSizes[i] = this.bufferPools[i].BufferSize;
				}
			}

			// Token: 0x060003CA RID: 970 RVA: 0x00012370 File Offset: 0x00010570
			public override void Clear()
			{
				for (int i = 0; i < this.bufferPools.Length; i++)
				{
					InternalBufferManager.PooledBufferManager.BufferPool bufferPool = this.bufferPools[i];
					bufferPool.Clear();
				}
			}

			// Token: 0x060003CB RID: 971 RVA: 0x000123A0 File Offset: 0x000105A0
			private void ChangeQuota(ref InternalBufferManager.PooledBufferManager.BufferPool bufferPool, int delta)
			{
				if (TraceCore.BufferPoolChangeQuotaIsEnabled(Fx.Trace))
				{
					TraceCore.BufferPoolChangeQuota(Fx.Trace, bufferPool.BufferSize, delta);
				}
				InternalBufferManager.PooledBufferManager.BufferPool bufferPool2 = bufferPool;
				int num = bufferPool2.Limit + delta;
				InternalBufferManager.PooledBufferManager.BufferPool bufferPool3 = InternalBufferManager.PooledBufferManager.BufferPool.CreatePool(bufferPool2.BufferSize, num);
				for (int i = 0; i < num; i++)
				{
					byte[] array = bufferPool2.Take();
					if (array == null)
					{
						break;
					}
					bufferPool3.Return(array);
					bufferPool3.IncrementCount();
				}
				this.remainingMemory -= (long)(bufferPool2.BufferSize * delta);
				bufferPool = bufferPool3;
			}

			// Token: 0x060003CC RID: 972 RVA: 0x00012424 File Offset: 0x00010624
			private void DecreaseQuota(ref InternalBufferManager.PooledBufferManager.BufferPool bufferPool)
			{
				this.ChangeQuota(ref bufferPool, -1);
			}

			// Token: 0x060003CD RID: 973 RVA: 0x00012430 File Offset: 0x00010630
			private int FindMostExcessivePool()
			{
				long num = 0L;
				int result = -1;
				for (int i = 0; i < this.bufferPools.Length; i++)
				{
					InternalBufferManager.PooledBufferManager.BufferPool bufferPool = this.bufferPools[i];
					if (bufferPool.Peak < bufferPool.Limit)
					{
						long num2 = (long)(bufferPool.Limit - bufferPool.Peak) * (long)bufferPool.BufferSize;
						if (num2 > num)
						{
							result = i;
							num = num2;
						}
					}
				}
				return result;
			}

			// Token: 0x060003CE RID: 974 RVA: 0x00012490 File Offset: 0x00010690
			private int FindMostStarvedPool()
			{
				long num = 0L;
				int result = -1;
				for (int i = 0; i < this.bufferPools.Length; i++)
				{
					InternalBufferManager.PooledBufferManager.BufferPool bufferPool = this.bufferPools[i];
					if (bufferPool.Peak == bufferPool.Limit)
					{
						long num2 = (long)bufferPool.Misses * (long)bufferPool.BufferSize;
						if (num2 > num)
						{
							result = i;
							num = num2;
						}
					}
				}
				return result;
			}

			// Token: 0x060003CF RID: 975 RVA: 0x000124E8 File Offset: 0x000106E8
			private InternalBufferManager.PooledBufferManager.BufferPool FindPool(int desiredBufferSize)
			{
				for (int i = 0; i < this.bufferSizes.Length; i++)
				{
					if (desiredBufferSize <= this.bufferSizes[i])
					{
						return this.bufferPools[i];
					}
				}
				return null;
			}

			// Token: 0x060003D0 RID: 976 RVA: 0x0001251D File Offset: 0x0001071D
			private void IncreaseQuota(ref InternalBufferManager.PooledBufferManager.BufferPool bufferPool)
			{
				this.ChangeQuota(ref bufferPool, 1);
			}

			// Token: 0x060003D1 RID: 977 RVA: 0x00012528 File Offset: 0x00010728
			public override void ReturnBuffer(byte[] buffer)
			{
				InternalBufferManager.PooledBufferManager.BufferPool bufferPool = this.FindPool(buffer.Length);
				if (bufferPool != null)
				{
					if (buffer.Length != bufferPool.BufferSize)
					{
						throw Fx.Exception.Argument("buffer", InternalSR.BufferIsNotRightSizeForBufferManager);
					}
					if (bufferPool.Return(buffer))
					{
						bufferPool.IncrementCount();
					}
				}
			}

			// Token: 0x060003D2 RID: 978 RVA: 0x00012574 File Offset: 0x00010774
			public override byte[] TakeBuffer(int bufferSize)
			{
				InternalBufferManager.PooledBufferManager.BufferPool bufferPool = this.FindPool(bufferSize);
				byte[] result;
				if (bufferPool != null)
				{
					byte[] array = bufferPool.Take();
					if (array != null)
					{
						bufferPool.DecrementCount();
						result = array;
					}
					else
					{
						if (bufferPool.Peak == bufferPool.Limit)
						{
							InternalBufferManager.PooledBufferManager.BufferPool bufferPool2 = bufferPool;
							int num = bufferPool2.Misses;
							bufferPool2.Misses = num + 1;
							num = this.totalMisses + 1;
							this.totalMisses = num;
							if (num >= 8)
							{
								this.TuneQuotas();
							}
						}
						if (TraceCore.BufferPoolAllocationIsEnabled(Fx.Trace))
						{
							TraceCore.BufferPoolAllocation(Fx.Trace, bufferPool.BufferSize);
						}
						result = Fx.AllocateByteArray(bufferPool.BufferSize);
					}
				}
				else
				{
					if (TraceCore.BufferPoolAllocationIsEnabled(Fx.Trace))
					{
						TraceCore.BufferPoolAllocation(Fx.Trace, bufferSize);
					}
					result = Fx.AllocateByteArray(bufferSize);
				}
				return result;
			}

			// Token: 0x060003D3 RID: 979 RVA: 0x00012624 File Offset: 0x00010824
			private void TuneQuotas()
			{
				if (this.areQuotasBeingTuned)
				{
					return;
				}
				bool flag = false;
				try
				{
					Monitor.TryEnter(this.tuningLock, ref flag);
					if (!flag || this.areQuotasBeingTuned)
					{
						return;
					}
					this.areQuotasBeingTuned = true;
				}
				finally
				{
					if (flag)
					{
						Monitor.Exit(this.tuningLock);
					}
				}
				int num = this.FindMostStarvedPool();
				if (num >= 0)
				{
					InternalBufferManager.PooledBufferManager.BufferPool bufferPool = this.bufferPools[num];
					if (this.remainingMemory < (long)bufferPool.BufferSize)
					{
						int num2 = this.FindMostExcessivePool();
						if (num2 >= 0)
						{
							this.DecreaseQuota(ref this.bufferPools[num2]);
						}
					}
					if (this.remainingMemory >= (long)bufferPool.BufferSize)
					{
						this.IncreaseQuota(ref this.bufferPools[num]);
					}
				}
				for (int i = 0; i < this.bufferPools.Length; i++)
				{
					InternalBufferManager.PooledBufferManager.BufferPool bufferPool2 = this.bufferPools[i];
					bufferPool2.Misses = 0;
				}
				this.totalMisses = 0;
				this.areQuotasBeingTuned = false;
			}

			// Token: 0x0400024E RID: 590
			private const int minBufferSize = 128;

			// Token: 0x0400024F RID: 591
			private const int maxMissesBeforeTuning = 8;

			// Token: 0x04000250 RID: 592
			private const int initialBufferCount = 1;

			// Token: 0x04000251 RID: 593
			private readonly object tuningLock;

			// Token: 0x04000252 RID: 594
			private int[] bufferSizes;

			// Token: 0x04000253 RID: 595
			private InternalBufferManager.PooledBufferManager.BufferPool[] bufferPools;

			// Token: 0x04000254 RID: 596
			private long memoryLimit;

			// Token: 0x04000255 RID: 597
			private long remainingMemory;

			// Token: 0x04000256 RID: 598
			private bool areQuotasBeingTuned;

			// Token: 0x04000257 RID: 599
			private int totalMisses;

			// Token: 0x020000B2 RID: 178
			private abstract class BufferPool
			{
				// Token: 0x060004B6 RID: 1206 RVA: 0x00014494 File Offset: 0x00012694
				public BufferPool(int bufferSize, int limit)
				{
					this.bufferSize = bufferSize;
					this.limit = limit;
				}

				// Token: 0x170000EA RID: 234
				// (get) Token: 0x060004B7 RID: 1207 RVA: 0x000144AA File Offset: 0x000126AA
				public int BufferSize
				{
					get
					{
						return this.bufferSize;
					}
				}

				// Token: 0x170000EB RID: 235
				// (get) Token: 0x060004B8 RID: 1208 RVA: 0x000144B2 File Offset: 0x000126B2
				public int Limit
				{
					get
					{
						return this.limit;
					}
				}

				// Token: 0x170000EC RID: 236
				// (get) Token: 0x060004B9 RID: 1209 RVA: 0x000144BA File Offset: 0x000126BA
				// (set) Token: 0x060004BA RID: 1210 RVA: 0x000144C2 File Offset: 0x000126C2
				public int Misses
				{
					get
					{
						return this.misses;
					}
					set
					{
						this.misses = value;
					}
				}

				// Token: 0x170000ED RID: 237
				// (get) Token: 0x060004BB RID: 1211 RVA: 0x000144CB File Offset: 0x000126CB
				public int Peak
				{
					get
					{
						return this.peak;
					}
				}

				// Token: 0x060004BC RID: 1212 RVA: 0x000144D3 File Offset: 0x000126D3
				public void Clear()
				{
					this.OnClear();
					this.count = 0;
				}

				// Token: 0x060004BD RID: 1213 RVA: 0x000144E4 File Offset: 0x000126E4
				public void DecrementCount()
				{
					int num = this.count - 1;
					if (num >= 0)
					{
						this.count = num;
					}
				}

				// Token: 0x060004BE RID: 1214 RVA: 0x00014508 File Offset: 0x00012708
				public void IncrementCount()
				{
					int num = this.count + 1;
					if (num <= this.limit)
					{
						this.count = num;
						if (num > this.peak)
						{
							this.peak = num;
						}
					}
				}

				// Token: 0x060004BF RID: 1215
				internal abstract byte[] Take();

				// Token: 0x060004C0 RID: 1216
				internal abstract bool Return(byte[] buffer);

				// Token: 0x060004C1 RID: 1217
				internal abstract void OnClear();

				// Token: 0x060004C2 RID: 1218 RVA: 0x0001453E File Offset: 0x0001273E
				internal static InternalBufferManager.PooledBufferManager.BufferPool CreatePool(int bufferSize, int limit)
				{
					if (bufferSize < 85000)
					{
						return new InternalBufferManager.PooledBufferManager.BufferPool.SynchronizedBufferPool(bufferSize, limit);
					}
					return new InternalBufferManager.PooledBufferManager.BufferPool.LargeBufferPool(bufferSize, limit);
				}

				// Token: 0x0400031D RID: 797
				private int bufferSize;

				// Token: 0x0400031E RID: 798
				private int count;

				// Token: 0x0400031F RID: 799
				private int limit;

				// Token: 0x04000320 RID: 800
				private int misses;

				// Token: 0x04000321 RID: 801
				private int peak;

				// Token: 0x020000B7 RID: 183
				private class SynchronizedBufferPool : InternalBufferManager.PooledBufferManager.BufferPool
				{
					// Token: 0x060004D5 RID: 1237 RVA: 0x000148A1 File Offset: 0x00012AA1
					internal SynchronizedBufferPool(int bufferSize, int limit) : base(bufferSize, limit)
					{
						this.innerPool = new SynchronizedPool<byte[]>(limit);
					}

					// Token: 0x060004D6 RID: 1238 RVA: 0x000148B7 File Offset: 0x00012AB7
					internal override void OnClear()
					{
						this.innerPool.Clear();
					}

					// Token: 0x060004D7 RID: 1239 RVA: 0x000148C4 File Offset: 0x00012AC4
					internal override byte[] Take()
					{
						return this.innerPool.Take();
					}

					// Token: 0x060004D8 RID: 1240 RVA: 0x000148D1 File Offset: 0x00012AD1
					internal override bool Return(byte[] buffer)
					{
						return this.innerPool.Return(buffer);
					}

					// Token: 0x0400032A RID: 810
					private SynchronizedPool<byte[]> innerPool;
				}

				// Token: 0x020000B8 RID: 184
				private class LargeBufferPool : InternalBufferManager.PooledBufferManager.BufferPool
				{
					// Token: 0x060004D9 RID: 1241 RVA: 0x000148DF File Offset: 0x00012ADF
					internal LargeBufferPool(int bufferSize, int limit) : base(bufferSize, limit)
					{
						this.items = new Stack<byte[]>(limit);
					}

					// Token: 0x170000F2 RID: 242
					// (get) Token: 0x060004DA RID: 1242 RVA: 0x000148F5 File Offset: 0x00012AF5
					private object ThisLock
					{
						get
						{
							return this.items;
						}
					}

					// Token: 0x060004DB RID: 1243 RVA: 0x00014900 File Offset: 0x00012B00
					internal override void OnClear()
					{
						object thisLock = this.ThisLock;
						lock (thisLock)
						{
							this.items.Clear();
						}
					}

					// Token: 0x060004DC RID: 1244 RVA: 0x00014948 File Offset: 0x00012B48
					internal override byte[] Take()
					{
						object thisLock = this.ThisLock;
						lock (thisLock)
						{
							if (this.items.Count > 0)
							{
								return this.items.Pop();
							}
						}
						return null;
					}

					// Token: 0x060004DD RID: 1245 RVA: 0x000149A4 File Offset: 0x00012BA4
					internal override bool Return(byte[] buffer)
					{
						object thisLock = this.ThisLock;
						lock (thisLock)
						{
							if (this.items.Count < base.Limit)
							{
								this.items.Push(buffer);
								return true;
							}
						}
						return false;
					}

					// Token: 0x0400032B RID: 811
					private Stack<byte[]> items;
				}
			}
		}

		// Token: 0x02000076 RID: 118
		private class GCBufferManager : InternalBufferManager
		{
			// Token: 0x060003D4 RID: 980 RVA: 0x0001271C File Offset: 0x0001091C
			private GCBufferManager()
			{
			}

			// Token: 0x170000A7 RID: 167
			// (get) Token: 0x060003D5 RID: 981 RVA: 0x00012724 File Offset: 0x00010924
			public static InternalBufferManager.GCBufferManager Value
			{
				get
				{
					return InternalBufferManager.GCBufferManager.value;
				}
			}

			// Token: 0x060003D6 RID: 982 RVA: 0x000033BD File Offset: 0x000015BD
			public override void Clear()
			{
			}

			// Token: 0x060003D7 RID: 983 RVA: 0x0001272B File Offset: 0x0001092B
			public override byte[] TakeBuffer(int bufferSize)
			{
				return Fx.AllocateByteArray(bufferSize);
			}

			// Token: 0x060003D8 RID: 984 RVA: 0x000033BD File Offset: 0x000015BD
			public override void ReturnBuffer(byte[] buffer)
			{
			}

			// Token: 0x04000258 RID: 600
			private static InternalBufferManager.GCBufferManager value = new InternalBufferManager.GCBufferManager();
		}
	}
}
