using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000502 RID: 1282
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Interface, Inherited = false)]
	public sealed class CoClassAttribute : Attribute
	{
		// Token: 0x0600319A RID: 12698 RVA: 0x000A98CA File Offset: 0x000A88CA
		public CoClassAttribute(Type coClass)
		{
			this._CoClass = coClass;
		}

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x0600319B RID: 12699 RVA: 0x000A98D9 File Offset: 0x000A88D9
		public Type CoClass
		{
			get
			{
				return this._CoClass;
			}
		}

		// Token: 0x040019A5 RID: 6565
		internal Type _CoClass;
	}
}
