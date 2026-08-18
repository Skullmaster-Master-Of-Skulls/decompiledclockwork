using System;
using System.Web.Mvc.Routing;

namespace System.Web.Mvc
{
	// Token: 0x0200009E RID: 158
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public class RoutePrefixAttribute : Attribute, IRoutePrefix
	{
		// Token: 0x0600046C RID: 1132 RVA: 0x0000D03C File Offset: 0x0000B23C
		protected RoutePrefixAttribute()
		{
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0000D044 File Offset: 0x0000B244
		public RoutePrefixAttribute(string prefix)
		{
			if (prefix == null)
			{
				throw new ArgumentNullException("prefix");
			}
			this.Prefix = prefix;
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x0000D061 File Offset: 0x0000B261
		// (set) Token: 0x0600046F RID: 1135 RVA: 0x0000D069 File Offset: 0x0000B269
		public virtual string Prefix { get; private set; }
	}
}
