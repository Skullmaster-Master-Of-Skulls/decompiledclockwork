using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NLog.Common;
using NLog.Config;

namespace NLog.Internal
{
	// Token: 0x020000A3 RID: 163
	internal class ObjectGraphScanner
	{
		// Token: 0x0600052D RID: 1325 RVA: 0x0000B50C File Offset: 0x0000970C
		public static List<T> FindReachableObjects<T>(params object[] rootObjects) where T : class
		{
			InternalLogger.Trace("FindReachableObject<{0}>:", new object[]
			{
				typeof(T)
			});
			List<T> list = new List<T>();
			HashSet<object> visitedObjects = new HashSet<object>();
			foreach (object o in rootObjects)
			{
				ObjectGraphScanner.ScanProperties<T>(list, o, 0, visitedObjects);
			}
			return list.ToList<T>();
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0000B570 File Offset: 0x00009770
		private static void ScanProperties<T>(List<T> result, object o, int level, HashSet<object> visitedObjects) where T : class
		{
			if (o == null)
			{
				return;
			}
			Type type = o.GetType();
			if (!type.IsDefined(typeof(NLogConfigurationItemAttribute), true))
			{
				return;
			}
			if (visitedObjects.Contains(o))
			{
				return;
			}
			visitedObjects.Add(o);
			T t = o as T;
			if (t != null)
			{
				result.Add(t);
			}
			if (InternalLogger.IsTraceEnabled)
			{
				InternalLogger.Trace("{0}Scanning {1} '{2}'", new object[]
				{
					new string(' ', level),
					type.Name,
					o
				});
			}
			foreach (PropertyInfo propertyInfo in PropertyHelper.GetAllReadableProperties(type))
			{
				if (!propertyInfo.PropertyType.IsPrimitive && !propertyInfo.PropertyType.IsEnum && !(propertyInfo.PropertyType == typeof(string)) && !propertyInfo.IsDefined(typeof(NLogConfigurationIgnorePropertyAttribute), true))
				{
					object value = propertyInfo.GetValue(o, null);
					if (value != null)
					{
						IList list = value as IList;
						if (list != null)
						{
							List<object> list2;
							lock (list.SyncRoot)
							{
								list2 = new List<object>(list.Count);
								for (int i = 0; i < list.Count; i++)
								{
									object item = list[i];
									list2.Add(item);
								}
							}
							ObjectGraphScanner.ScanPropertiesList<T>(result, list2, level + 1, visitedObjects);
						}
						else
						{
							IEnumerable enumerable = value as IEnumerable;
							if (enumerable != null)
							{
								IList<object> elements = (enumerable as IList<object>) ?? enumerable.Cast<object>().ToList<object>();
								ObjectGraphScanner.ScanPropertiesList<T>(result, elements, level + 1, visitedObjects);
							}
							else
							{
								ObjectGraphScanner.ScanProperties<T>(result, value, level + 1, visitedObjects);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0000B778 File Offset: 0x00009978
		private static void ScanPropertiesList<T>(List<T> result, IEnumerable<object> elements, int level, HashSet<object> visitedObjects) where T : class
		{
			foreach (object o in elements)
			{
				ObjectGraphScanner.ScanProperties<T>(result, o, level, visitedObjects);
			}
		}
	}
}
