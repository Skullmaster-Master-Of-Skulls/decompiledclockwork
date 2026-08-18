using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.Common.Utils
{
	// Token: 0x02000398 RID: 920
	internal class Pair<TFirst, TSecond> : InternalBase
	{
		// Token: 0x060032F1 RID: 13041 RVA: 0x000C6DDD File Offset: 0x000C4FDD
		internal Pair(TFirst first, TSecond second)
		{
			this.first = first;
			this.second = second;
		}

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x060032F2 RID: 13042 RVA: 0x000C6DF3 File Offset: 0x000C4FF3
		internal TFirst First
		{
			get
			{
				return this.first;
			}
		}

		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x060032F3 RID: 13043 RVA: 0x000C6DFB File Offset: 0x000C4FFB
		internal TSecond Second
		{
			get
			{
				return this.second;
			}
		}

		// Token: 0x060032F4 RID: 13044 RVA: 0x000C6E04 File Offset: 0x000C5004
		public override int GetHashCode()
		{
			TFirst tfirst = this.first;
			int num = tfirst.GetHashCode() << 5;
			TSecond tsecond = this.second;
			return num ^ tsecond.GetHashCode();
		}

		// Token: 0x060032F5 RID: 13045 RVA: 0x000C6E3C File Offset: 0x000C503C
		public bool Equals(Pair<TFirst, TSecond> other)
		{
			TFirst tfirst = this.first;
			if (tfirst.Equals(other.first))
			{
				TSecond tsecond = this.second;
				return tsecond.Equals(other.second);
			}
			return false;
		}

		// Token: 0x060032F6 RID: 13046 RVA: 0x000C6E8C File Offset: 0x000C508C
		public override bool Equals(object other)
		{
			Pair<TFirst, TSecond> pair = other as Pair<TFirst, TSecond>;
			return pair != null && this.Equals(pair);
		}

		// Token: 0x060032F7 RID: 13047 RVA: 0x000C6EAC File Offset: 0x000C50AC
		internal override void ToCompactString(StringBuilder builder)
		{
			builder.Append("<");
			TFirst tfirst = this.first;
			builder.Append(tfirst.ToString());
			string str = ", ";
			TSecond tsecond = this.second;
			builder.Append(str + tsecond.ToString());
			builder.Append(">");
		}

		// Token: 0x04001665 RID: 5733
		private readonly TFirst first;

		// Token: 0x04001666 RID: 5734
		private readonly TSecond second;

		// Token: 0x0200067A RID: 1658
		internal class PairComparer : IEqualityComparer<Pair<TFirst, TSecond>>
		{
			// Token: 0x060044C6 RID: 17606 RVA: 0x00002050 File Offset: 0x00000250
			private PairComparer()
			{
			}

			// Token: 0x060044C7 RID: 17607 RVA: 0x000F875F File Offset: 0x000F695F
			public bool Equals(Pair<TFirst, TSecond> x, Pair<TFirst, TSecond> y)
			{
				return Pair<TFirst, TSecond>.PairComparer.firstComparer.Equals(x.First, y.First) && Pair<TFirst, TSecond>.PairComparer.secondComparer.Equals(x.Second, y.Second);
			}

			// Token: 0x060044C8 RID: 17608 RVA: 0x0003C7A1 File Offset: 0x0003A9A1
			public int GetHashCode(Pair<TFirst, TSecond> source)
			{
				return source.GetHashCode();
			}

			// Token: 0x04001FB6 RID: 8118
			internal static readonly Pair<TFirst, TSecond>.PairComparer Instance = new Pair<TFirst, TSecond>.PairComparer();

			// Token: 0x04001FB7 RID: 8119
			private static readonly EqualityComparer<TFirst> firstComparer = EqualityComparer<TFirst>.Default;

			// Token: 0x04001FB8 RID: 8120
			private static readonly EqualityComparer<TSecond> secondComparer = EqualityComparer<TSecond>.Default;
		}
	}
}
