using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc
{
	// Token: 0x0200008F RID: 143
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
	public abstract class FilterAttribute : Attribute, IMvcFilter
	{
		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000416 RID: 1046 RVA: 0x0000C1B0 File Offset: 0x0000A3B0
		public bool AllowMultiple
		{
			get
			{
				return FilterAttribute.AllowsMultiple(base.GetType());
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x0000C1BD File Offset: 0x0000A3BD
		// (set) Token: 0x06000418 RID: 1048 RVA: 0x0000C1C5 File Offset: 0x0000A3C5
		public int Order
		{
			get
			{
				return this._order;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("value", MvcResources.FilterAttribute_OrderOutOfRange);
				}
				this._order = value;
			}
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000C204 File Offset: 0x0000A404
		private static bool AllowsMultiple(Type attributeType)
		{
			return FilterAttribute._multiuseAttributeCache.GetOrAdd(attributeType, (Type type) => type.GetCustomAttributes(typeof(AttributeUsageAttribute), true).Cast<AttributeUsageAttribute>().First<AttributeUsageAttribute>().AllowMultiple);
		}

		// Token: 0x04000126 RID: 294
		private static readonly ConcurrentDictionary<Type, bool> _multiuseAttributeCache = new ConcurrentDictionary<Type, bool>();

		// Token: 0x04000127 RID: 295
		private int _order = -1;
	}
}
