using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Web.UI;
using Telerik.Web.UI.Functions;

namespace Telerik.Web.UI
{
	// Token: 0x020001EE RID: 494
	internal class DataFormInMemoryEnumerableHelper : DataFormEnumerableHelper
	{
		// Token: 0x06001175 RID: 4469 RVA: 0x0003F659 File Offset: 0x0003D859
		public DataFormInMemoryEnumerableHelper() : this(false)
		{
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x0003F662 File Offset: 0x0003D862
		internal DataFormInMemoryEnumerableHelper(bool allowStableSort) : base(allowStableSort)
		{
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x0003F66C File Offset: 0x0003D86C
		public override int GetCount<TSource>(IEnumerable<TSource> source)
		{
			ICollection<TSource> collection = source as ICollection<TSource>;
			if (collection != null)
			{
				return collection.Count;
			}
			return this.GetCount(source);
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x0003F694 File Offset: 0x0003D894
		public override int GetCount(IEnumerable source)
		{
			ICollection collection = source as ICollection;
			if (collection != null)
			{
				return collection.Count;
			}
			Array array = source as Array;
			if (array != null)
			{
				return array.Length;
			}
			int num = 0;
			foreach (object obj in source)
			{
				num++;
			}
			return num;
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x0003F9E0 File Offset: 0x0003DBE0
		public override IEnumerable GetPage(IEnumerable enumerable, int startIndex, int pageSize)
		{
			startIndex = Math.Max(startIndex, 0);
			if (enumerable is IList)
			{
				IList list = (IList)enumerable;
				int itemCounter = 0;
				for (int i = startIndex; i < list.Count; i++)
				{
					yield return list[i];
					itemCounter++;
					if (pageSize == itemCounter)
					{
						break;
					}
				}
			}
			else
			{
				int index = 0;
				foreach (object item in enumerable)
				{
					if (index < startIndex)
					{
						index++;
					}
					else
					{
						yield return item;
						index++;
						if (pageSize + startIndex == index)
						{
							yield break;
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x0003FA50 File Offset: 0x0003DC50
		private TFunc<object, TResult> GetEvalFunc<TResult>(string propertyName)
		{
			return delegate(object element)
			{
				object obj = DataBinder.Eval(element, propertyName);
				if (obj == Convert.DBNull)
				{
					return default(TResult);
				}
				return (TResult)((object)obj);
			};
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x0003FA78 File Offset: 0x0003DC78
		private static Type GetPropertyType(Type itemType, string propertyName, bool isCustomTypeDescriptor)
		{
			Type result = typeof(object);
			if (isCustomTypeDescriptor && DataFormEnumerableHelper._customTypeDescriptorProperties != null)
			{
				PropertyDescriptor propertyDescriptor = DataFormEnumerableHelper._customTypeDescriptorProperties.Find(propertyName, true);
				if (propertyDescriptor != null)
				{
					result = propertyDescriptor.PropertyType;
				}
			}
			else
			{
				PropertyInfo property = itemType.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				if (property != null)
				{
					result = property.PropertyType;
				}
			}
			return result;
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x0003FACE File Offset: 0x0003DCCE
		public static TSource[] ToArray<TSource>(IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return DataFormInMemoryEnumerableHelper.ToList<TSource>(source).ToArray();
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x0003FAE9 File Offset: 0x0003DCE9
		public static List<TSource> ToList<TSource>(IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new List<TSource>(source);
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x0003FCE8 File Offset: 0x0003DEE8
		protected IEnumerable Where(IEnumerable source, Predicate<object> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			foreach (object item in source)
			{
				if (predicate(item))
				{
					yield return item;
				}
			}
			yield break;
		}
	}
}
