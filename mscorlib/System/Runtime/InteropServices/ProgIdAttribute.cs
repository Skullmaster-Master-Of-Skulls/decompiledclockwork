using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020004E7 RID: 1255
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	[ComVisible(true)]
	public sealed class ProgIdAttribute : Attribute
	{
		// Token: 0x06003153 RID: 12627 RVA: 0x000A90F9 File Offset: 0x000A80F9
		public ProgIdAttribute(string progId)
		{
			this._val = progId;
		}

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x06003154 RID: 12628 RVA: 0x000A9108 File Offset: 0x000A8108
		public string Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04001900 RID: 6400
		internal string _val;
	}
}
