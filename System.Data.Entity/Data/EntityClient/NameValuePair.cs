using System;

namespace System.Data.EntityClient
{
	// Token: 0x0200011A RID: 282
	internal sealed class NameValuePair
	{
		// Token: 0x06000E78 RID: 3704 RVA: 0x0003E142 File Offset: 0x0003C342
		internal NameValuePair(string name, string value, int length)
		{
			this._name = name;
			this._value = value;
			this._length = length;
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000E79 RID: 3705 RVA: 0x0003E15F File Offset: 0x0003C35F
		// (set) Token: 0x06000E7A RID: 3706 RVA: 0x0003E167 File Offset: 0x0003C367
		internal NameValuePair Next
		{
			get
			{
				return this._next;
			}
			set
			{
				if (this._next != null || value == null)
				{
					throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.NameValuePairNext);
				}
				this._next = value;
			}
		}

		// Token: 0x040009DE RID: 2526
		private readonly string _name;

		// Token: 0x040009DF RID: 2527
		private readonly string _value;

		// Token: 0x040009E0 RID: 2528
		private readonly int _length;

		// Token: 0x040009E1 RID: 2529
		private NameValuePair _next;
	}
}
