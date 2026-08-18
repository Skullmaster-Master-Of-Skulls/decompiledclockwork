using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace System.Web.WebPages
{
	// Token: 0x02000054 RID: 84
	internal class HtmlAttributePropertyHelper : PropertyHelper
	{
		// Token: 0x06000204 RID: 516 RVA: 0x00008418 File Offset: 0x00006618
		public new static PropertyHelper[] GetProperties(object instance)
		{
			return PropertyHelper.GetProperties(instance, new Func<PropertyInfo, PropertyHelper>(HtmlAttributePropertyHelper.CreateInstance), HtmlAttributePropertyHelper._reflectionCache);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00008431 File Offset: 0x00006631
		private static PropertyHelper CreateInstance(PropertyInfo property)
		{
			return new HtmlAttributePropertyHelper(property);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00008439 File Offset: 0x00006639
		public HtmlAttributePropertyHelper(PropertyInfo property) : base(property)
		{
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000207 RID: 519 RVA: 0x00008442 File Offset: 0x00006642
		// (set) Token: 0x06000208 RID: 520 RVA: 0x0000844A File Offset: 0x0000664A
		public override string Name
		{
			get
			{
				return base.Name;
			}
			protected set
			{
				base.Name = ((value == null) ? null : value.Replace('_', '-'));
			}
		}

		// Token: 0x040000A9 RID: 169
		private static ConcurrentDictionary<Type, PropertyHelper[]> _reflectionCache = new ConcurrentDictionary<Type, PropertyHelper[]>();
	}
}
