using System;

namespace System.Web.Http
{
	// Token: 0x02000004 RID: 4
	internal class EfficientTypePropertyKey<T1, T2> : Tuple<T1, T2>
	{
		// Token: 0x06000013 RID: 19 RVA: 0x000027A8 File Offset: 0x000009A8
		public EfficientTypePropertyKey(T1 item1, T2 item2) : base(item1, item2)
		{
			this._hashCode = base.GetHashCode();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000027BE File Offset: 0x000009BE
		public override int GetHashCode()
		{
			return this._hashCode;
		}

		// Token: 0x04000001 RID: 1
		private int _hashCode;
	}
}
