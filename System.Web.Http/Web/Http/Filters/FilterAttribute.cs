using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Web.Http.Internal;

namespace System.Web.Http.Filters
{
	// Token: 0x020000E3 RID: 227
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
	public abstract class FilterAttribute : Attribute, IFilter
	{
		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000587 RID: 1415 RVA: 0x00011FDA File Offset: 0x000101DA
		public virtual bool AllowMultiple
		{
			get
			{
				return FilterAttribute.AllowsMultiple(base.GetType());
			}
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00011FFA File Offset: 0x000101FA
		private static bool AllowsMultiple(Type attributeType)
		{
			return FilterAttribute._attributeUsageCache.GetOrAdd(attributeType, (Type type) => type.GetCustomAttributes(true).First<AttributeUsageAttribute>().AllowMultiple);
		}

		// Token: 0x04000195 RID: 405
		private static readonly ConcurrentDictionary<Type, bool> _attributeUsageCache = new ConcurrentDictionary<Type, bool>();
	}
}
