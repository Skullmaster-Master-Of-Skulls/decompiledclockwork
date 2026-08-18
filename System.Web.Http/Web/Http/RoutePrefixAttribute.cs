using System;
using System.Web.Http.Routing;

namespace System.Web.Http
{
	// Token: 0x0200006F RID: 111
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public class RoutePrefixAttribute : Attribute, IRoutePrefix
	{
		// Token: 0x06000309 RID: 777 RVA: 0x00009F84 File Offset: 0x00008184
		protected RoutePrefixAttribute()
		{
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00009F8C File Offset: 0x0000818C
		public RoutePrefixAttribute(string prefix)
		{
			if (prefix == null)
			{
				throw Error.ArgumentNull("prefix");
			}
			this.Prefix = prefix;
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x0600030B RID: 779 RVA: 0x00009FA9 File Offset: 0x000081A9
		// (set) Token: 0x0600030C RID: 780 RVA: 0x00009FB1 File Offset: 0x000081B1
		public virtual string Prefix { get; private set; }
	}
}
