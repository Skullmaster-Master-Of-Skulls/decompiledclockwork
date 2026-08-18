using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TechnoPro.Common.Unity.Adapters
{
	// Token: 0x02000012 RID: 18
	public static class ObjectHelper
	{
		// Token: 0x0600005C RID: 92 RVA: 0x00003730 File Offset: 0x00001930
		public static int GetHashCode(this object obj, params string[] keyProperties)
		{
			int num = obj.GetType().GetHashCode();
			PropertyInfo[] properties = obj.GetType().GetProperties();
			IEnumerable<PropertyInfo> source = properties;
			Func<PropertyInfo, bool> <>9__0;
			Func<PropertyInfo, bool> predicate;
			if ((predicate = <>9__0) == null)
			{
				predicate = (<>9__0 = ((PropertyInfo p) => keyProperties.Contains(p.Name)));
			}
			IEnumerable<PropertyInfo> source2 = source.Where(predicate);
			Func<PropertyInfo, object> <>9__1;
			Func<PropertyInfo, object> selector;
			if ((selector = <>9__1) == null)
			{
				selector = (<>9__1 = ((PropertyInfo p) => p.GetValue(obj, null)));
			}
			foreach (object obj2 in from value in source2.Select(selector)
			where value != null
			select value)
			{
				bool flag = obj2 is IEnumerable;
				if (flag)
				{
					num = (num * 31 ^ obj2.GetType().GetHashCode());
					num = ((IEnumerable)obj2).Cast<object>().Aggregate(num, (int current, object subvalue) => current * 31 ^ subvalue.GetHashCode());
				}
				else
				{
					num = (num * 31 ^ obj2.GetHashCode());
				}
			}
			return num;
		}
	}
}
