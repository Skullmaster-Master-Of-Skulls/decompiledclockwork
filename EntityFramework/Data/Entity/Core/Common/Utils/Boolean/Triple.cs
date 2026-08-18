using System;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x0200031E RID: 798
	internal struct Triple<T1, T2, T3> : IEquatable<Triple<T1, T2, T3>> where T1 : IEquatable<T1> where T2 : IEquatable<T2> where T3 : IEquatable<T3>
	{
		// Token: 0x06001B8D RID: 7053 RVA: 0x0008803C File Offset: 0x0008623C
		internal Triple(T1 value1, T2 value2, T3 value3)
		{
			this._value1 = value1;
			this._value2 = value2;
			this._value3 = value3;
		}

		// Token: 0x06001B8E RID: 7054 RVA: 0x00088054 File Offset: 0x00086254
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

		// Token: 0x06001B8F RID: 7055 RVA: 0x000880B8 File Offset: 0x000862B8
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06001B90 RID: 7056 RVA: 0x000880CC File Offset: 0x000862CC
		public override int GetHashCode()
		{
			T1 value = this._value1;
			int hashCode = value.GetHashCode();
			T2 value2 = this._value2;
			int num = hashCode ^ value2.GetHashCode();
			T3 value3 = this._value3;
			return num ^ value3.GetHashCode();
		}

		// Token: 0x040009AB RID: 2475
		private readonly T1 _value1;

		// Token: 0x040009AC RID: 2476
		private readonly T2 _value2;

		// Token: 0x040009AD RID: 2477
		private readonly T3 _value3;
	}
}
