using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000507 RID: 1287
	[AttributeUsage(AttributeTargets.Module, Inherited = false)]
	[ComVisible(true)]
	public sealed class DefaultCharSetAttribute : Attribute
	{
		// Token: 0x060031A9 RID: 12713 RVA: 0x000A9989 File Offset: 0x000A8989
		public DefaultCharSetAttribute(CharSet charSet)
		{
			this._CharSet = charSet;
		}

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x060031AA RID: 12714 RVA: 0x000A9998 File Offset: 0x000A8998
		public CharSet CharSet
		{
			get
			{
				return this._CharSet;
			}
		}

		// Token: 0x040019B0 RID: 6576
		internal CharSet _CharSet;
	}
}
