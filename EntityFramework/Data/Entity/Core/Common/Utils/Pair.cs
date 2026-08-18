using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.Entity.Core.Common.Utils
{
	// Token: 0x0200032E RID: 814
	internal class Pair<TFirst, TSecond> : InternalBase
	{
		// Token: 0x06001C28 RID: 7208 RVA: 0x0008ACA1 File Offset: 0x00088EA1
		internal Pair(TFirst first, TSecond second)
		{
			this.first = first;
			this.second = second;
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06001C29 RID: 7209 RVA: 0x0008ACB7 File Offset: 0x00088EB7
		internal TFirst First
		{
			get
			{
				return this.first;
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06001C2A RID: 7210 RVA: 0x0008ACBF File Offset: 0x00088EBF
		internal TSecond Second
		{
			get
			{
				return this.second;
			}
		}

		// Token: 0x06001C2B RID: 7211 RVA: 0x0008ACC8 File Offset: 0x00088EC8
		public override int GetHashCode()
		{
			TFirst tfirst = this.first;
			int num = tfirst.GetHashCode() << 5;
			TSecond tsecond = this.second;
			return num ^ tsecond.GetHashCode();
		}

		// Token: 0x06001C2C RID: 7212 RVA: 0x0008AD00 File Offset: 0x00088F00
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

		// Token: 0x06001C2D RID: 7213 RVA: 0x0008AD50 File Offset: 0x00088F50
		public override bool Equals(object other)
		{
			Pair<TFirst, TSecond> pair = other as Pair<TFirst, TSecond>;
			return pair != null && this.Equals(pair);
		}

		// Token: 0x06001C2E RID: 7214 RVA: 0x0008AD70 File Offset: 0x00088F70
		internal override void ToCompactString(StringBuilder builder)
		{
			builder.Append("<");
			builder.Append(this.first);
			builder.Append(", " + this.second);
			builder.Append(">");
		}

		// Token: 0x040009C3 RID: 2499
		private readonly TFirst first;

		// Token: 0x040009C4 RID: 2500
		private readonly TSecond second;

		// Token: 0x0200032F RID: 815
		internal class PairComparer : IEqualityComparer<Pair<TFirst, TSecond>>
		{
			// Token: 0x06001C2F RID: 7215 RVA: 0x0008ADC3 File Offset: 0x00088FC3
			private PairComparer()
			{
			}

			// Token: 0x06001C30 RID: 7216 RVA: 0x0008ADCB File Offset: 0x00088FCB
			public bool Equals(Pair<TFirst, TSecond> x, Pair<TFirst, TSecond> y)
			{
				return Pair<TFirst, TSecond>.PairComparer._firstComparer.Equals(x.First, y.First) && Pair<TFirst, TSecond>.PairComparer._secondComparer.Equals(x.Second, y.Second);
			}

			// Token: 0x06001C31 RID: 7217 RVA: 0x0008ADFD File Offset: 0x00088FFD
			public int GetHashCode(Pair<TFirst, TSecond> source)
			{
				return source.GetHashCode();
			}

			// Token: 0x040009C5 RID: 2501
			internal static readonly Pair<TFirst, TSecond>.PairComparer Instance = new Pair<TFirst, TSecond>.PairComparer();

			// Token: 0x040009C6 RID: 2502
			private static readonly EqualityComparer<TFirst> _firstComparer = EqualityComparer<TFirst>.Default;

			// Token: 0x040009C7 RID: 2503
			private static readonly EqualityComparer<TSecond> _secondComparer = EqualityComparer<TSecond>.Default;
		}
	}
}
