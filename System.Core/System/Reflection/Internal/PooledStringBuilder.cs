using System;
using System.Text;

namespace System.Reflection.Internal
{
	// Token: 0x0200008B RID: 139
	internal class PooledStringBuilder
	{
		// Token: 0x06000378 RID: 888 RVA: 0x00008BC2 File Offset: 0x00006DC2
		private PooledStringBuilder(ObjectPool<PooledStringBuilder> pool)
		{
			this._pool = pool;
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000379 RID: 889 RVA: 0x00008BDC File Offset: 0x00006DDC
		public int Length
		{
			get
			{
				return this.Builder.Length;
			}
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00008BEC File Offset: 0x00006DEC
		public void Free()
		{
			StringBuilder builder = this.Builder;
			if (builder.Capacity <= 1024)
			{
				builder.Clear();
				this._pool.Free(this);
			}
		}

		// Token: 0x0600037B RID: 891 RVA: 0x00008C20 File Offset: 0x00006E20
		public string ToStringAndFree()
		{
			string result = this.Builder.ToString();
			this.Free();
			return result;
		}

		// Token: 0x0600037C RID: 892 RVA: 0x00008C40 File Offset: 0x00006E40
		public static ObjectPool<PooledStringBuilder> CreatePool()
		{
			ObjectPool<PooledStringBuilder> pool = null;
			pool = new ObjectPool<PooledStringBuilder>(() => new PooledStringBuilder(pool), 32);
			return pool;
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00008C7C File Offset: 0x00006E7C
		public static PooledStringBuilder GetInstance()
		{
			return PooledStringBuilder.s_poolInstance.Allocate();
		}

		// Token: 0x0400049B RID: 1179
		public readonly StringBuilder Builder = new StringBuilder();

		// Token: 0x0400049C RID: 1180
		private readonly ObjectPool<PooledStringBuilder> _pool;

		// Token: 0x0400049D RID: 1181
		private static readonly ObjectPool<PooledStringBuilder> s_poolInstance = PooledStringBuilder.CreatePool();
	}
}
