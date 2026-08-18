using System;
using System.Text;

namespace System.Reflection.Internal
{
	// Token: 0x02000166 RID: 358
	internal class PooledStringBuilder
	{
		// Token: 0x06000B34 RID: 2868 RVA: 0x000205B7 File Offset: 0x0001E7B7
		private PooledStringBuilder(ObjectPool<PooledStringBuilder> pool)
		{
			this._pool = pool;
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000B35 RID: 2869 RVA: 0x000205D1 File Offset: 0x0001E7D1
		public int Length
		{
			get
			{
				return this.Builder.Length;
			}
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x000205E0 File Offset: 0x0001E7E0
		public void Free()
		{
			StringBuilder builder = this.Builder;
			if (builder.Capacity <= 1024)
			{
				builder.Clear();
				this._pool.Free(this);
			}
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x00020614 File Offset: 0x0001E814
		public string ToStringAndFree()
		{
			string result = this.Builder.ToString();
			this.Free();
			return result;
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x00020627 File Offset: 0x0001E827
		public static ObjectPool<PooledStringBuilder> CreatePool()
		{
			ObjectPool<PooledStringBuilder> pool = null;
			pool = new ObjectPool<PooledStringBuilder>(() => new PooledStringBuilder(pool), 32);
			return pool;
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x00020653 File Offset: 0x0001E853
		public static PooledStringBuilder GetInstance()
		{
			return PooledStringBuilder.s_poolInstance.Allocate();
		}

		// Token: 0x04000929 RID: 2345
		public readonly StringBuilder Builder = new StringBuilder();

		// Token: 0x0400092A RID: 2346
		private readonly ObjectPool<PooledStringBuilder> _pool;

		// Token: 0x0400092B RID: 2347
		private static readonly ObjectPool<PooledStringBuilder> s_poolInstance = PooledStringBuilder.CreatePool();
	}
}
