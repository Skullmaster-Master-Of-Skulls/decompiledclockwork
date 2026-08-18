using System;
using System.Threading;

namespace System.Runtime.InteropServices
{
	// Token: 0x020003DA RID: 986
	[__DynamicallyInvokable]
	public sealed class HandleCollector
	{
		// Token: 0x060025F9 RID: 9721 RVA: 0x000B062D File Offset: 0x000AE82D
		[__DynamicallyInvokable]
		public HandleCollector(string name, int initialThreshold) : this(name, initialThreshold, int.MaxValue)
		{
		}

		// Token: 0x060025FA RID: 9722 RVA: 0x000B063C File Offset: 0x000AE83C
		[__DynamicallyInvokable]
		public HandleCollector(string name, int initialThreshold, int maximumThreshold)
		{
			if (initialThreshold < 0)
			{
				throw new ArgumentOutOfRangeException("initialThreshold", SR.GetString("ArgumentOutOfRange_NeedNonNegNumRequired"));
			}
			if (maximumThreshold < 0)
			{
				throw new ArgumentOutOfRangeException("maximumThreshold", SR.GetString("ArgumentOutOfRange_NeedNonNegNumRequired"));
			}
			if (initialThreshold > maximumThreshold)
			{
				throw new ArgumentException(SR.GetString("Argument_InvalidThreshold"));
			}
			if (name != null)
			{
				this.name = name;
			}
			else
			{
				this.name = string.Empty;
			}
			this.initialThreshold = initialThreshold;
			this.maximumThreshold = maximumThreshold;
			this.threshold = initialThreshold;
			this.handleCount = 0;
		}

		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x060025FB RID: 9723 RVA: 0x000B06D4 File Offset: 0x000AE8D4
		[__DynamicallyInvokable]
		public int Count
		{
			[__DynamicallyInvokable]
			get
			{
				return this.handleCount;
			}
		}

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x060025FC RID: 9724 RVA: 0x000B06DC File Offset: 0x000AE8DC
		[__DynamicallyInvokable]
		public int InitialThreshold
		{
			[__DynamicallyInvokable]
			get
			{
				return this.initialThreshold;
			}
		}

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x060025FD RID: 9725 RVA: 0x000B06E4 File Offset: 0x000AE8E4
		[__DynamicallyInvokable]
		public int MaximumThreshold
		{
			[__DynamicallyInvokable]
			get
			{
				return this.maximumThreshold;
			}
		}

		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x060025FE RID: 9726 RVA: 0x000B06EC File Offset: 0x000AE8EC
		[__DynamicallyInvokable]
		public string Name
		{
			[__DynamicallyInvokable]
			get
			{
				return this.name;
			}
		}

		// Token: 0x060025FF RID: 9727 RVA: 0x000B06F4 File Offset: 0x000AE8F4
		[__DynamicallyInvokable]
		public void Add()
		{
			int num = -1;
			Interlocked.Increment(ref this.handleCount);
			if (this.handleCount < 0)
			{
				throw new InvalidOperationException(SR.GetString("InvalidOperation_HCCountOverflow"));
			}
			if (this.handleCount > this.threshold)
			{
				lock (this)
				{
					this.threshold = this.handleCount + this.handleCount / 10;
					num = this.gc_gen;
					if (this.gc_gen < 2)
					{
						this.gc_gen++;
					}
				}
			}
			if (num >= 0 && (num == 0 || this.gc_counts[num] == GC.CollectionCount(num)))
			{
				GC.Collect(num);
				Thread.Sleep(10 * num);
			}
			for (int i = 1; i < 3; i++)
			{
				this.gc_counts[i] = GC.CollectionCount(i);
			}
		}

		// Token: 0x06002600 RID: 9728 RVA: 0x000B07D4 File Offset: 0x000AE9D4
		[__DynamicallyInvokable]
		public void Remove()
		{
			Interlocked.Decrement(ref this.handleCount);
			if (this.handleCount < 0)
			{
				throw new InvalidOperationException(SR.GetString("InvalidOperation_HCCountOverflow"));
			}
			int num = this.handleCount + this.handleCount / 10;
			if (num < this.threshold - this.threshold / 10)
			{
				lock (this)
				{
					if (num > this.initialThreshold)
					{
						this.threshold = num;
					}
					else
					{
						this.threshold = this.initialThreshold;
					}
					this.gc_gen = 0;
				}
			}
			for (int i = 1; i < 3; i++)
			{
				this.gc_counts[i] = GC.CollectionCount(i);
			}
		}

		// Token: 0x0400207F RID: 8319
		private const int deltaPercent = 10;

		// Token: 0x04002080 RID: 8320
		private string name;

		// Token: 0x04002081 RID: 8321
		private int initialThreshold;

		// Token: 0x04002082 RID: 8322
		private int maximumThreshold;

		// Token: 0x04002083 RID: 8323
		private int threshold;

		// Token: 0x04002084 RID: 8324
		private int handleCount;

		// Token: 0x04002085 RID: 8325
		private int[] gc_counts = new int[3];

		// Token: 0x04002086 RID: 8326
		private int gc_gen;
	}
}
