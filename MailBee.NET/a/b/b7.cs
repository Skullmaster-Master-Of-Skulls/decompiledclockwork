using System;
using System.Collections;

namespace a.b
{
	// Token: 0x02000373 RID: 883
	internal abstract class b7 : ReadOnlyCollectionBase
	{
		// Token: 0x06001FE8 RID: 8168 RVA: 0x00085FE5 File Offset: 0x00084FE5
		public sealed override bool Equals(object obj)
		{
			return obj == this || (obj != null && !(base.GetType() != obj.GetType()) && this.a(obj));
		}

		// Token: 0x06001FE9 RID: 8169 RVA: 0x0008600C File Offset: 0x0008500C
		public override string ToString()
		{
			return i2.a(this);
		}

		// Token: 0x06001FEA RID: 8170 RVA: 0x00086014 File Offset: 0x00085014
		protected virtual bool a(object A_0)
		{
			return i2.a(this, A_0);
		}

		// Token: 0x06001FEB RID: 8171 RVA: 0x0008601D File Offset: 0x0008501D
		public sealed override int GetHashCode()
		{
			return f3.a(base.GetType().GetHashCode(), this.b());
		}

		// Token: 0x06001FEC RID: 8172 RVA: 0x00086035 File Offset: 0x00085035
		protected virtual int b()
		{
			return f3.a(this);
		}
	}
}
