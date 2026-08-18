using System;

namespace System.Data.Common.Utils.Boolean
{
	// Token: 0x020003A8 RID: 936
	internal struct Triple<T1, T2, T3> : IEquatable<Triple<T1, T2, T3>> where T1 : IEquatable<T1> where T2 : IEquatable<T2> where T3 : IEquatable<T3>
	{
		// Token: 0x06003384 RID: 13188 RVA: 0x000C85CB File Offset: 0x000C67CB
		internal Triple(T1 value1, T2 value2, T3 value3)
		{
			this._value1 = value1;
			this._value2 = value2;
			this._value3 = value3;
		}

		// Token: 0x06003385 RID: 13189 RVA: 0x000C85E4 File Offset: 0x000C67E4
		public bool Equals(Triple<T1, T2, T3> other)
		{
			T1 value = this._value1;
			if (value.Equals(other._value1))
			{
				T2 value2 = this._value2;
				if (value2.Equals(other._value2))
				{
					T3 value3 = this._value3;
					return value3.Equals(other._value3);
				}
			}
			return false;
		}

		// Token: 0x06003386 RID: 13190 RVA: 0x000C8645 File Offset: 0x000C6845
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06003387 RID: 13191 RVA: 0x000C8658 File Offset: 0x000C6858
		public override int GetHashCode()
		{
			T1 value = this._value1;
			int hashCode = value.GetHashCode();
			T2 value2 = this._value2;
			int num = hashCode ^ value2.GetHashCode();
			T3 value3 = this._value3;
			return num ^ value3.GetHashCode();
		}

		// Token: 0x0400168D RID: 5773
		private readonly T1 _value1;

		// Token: 0x0400168E RID: 5774
		private readonly T2 _value2;

		// Token: 0x0400168F RID: 5775
		private readonly T3 _value3;
	}
}
