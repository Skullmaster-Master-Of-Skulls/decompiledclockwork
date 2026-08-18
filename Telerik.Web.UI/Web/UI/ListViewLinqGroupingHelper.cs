using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace Telerik.Web.UI
{
	// Token: 0x02000586 RID: 1414
	internal class ListViewLinqGroupingHelper
	{
		// Token: 0x1700108B RID: 4235
		// (get) Token: 0x060032FD RID: 13053 RVA: 0x000A8A5D File Offset: 0x000A6C5D
		// (set) Token: 0x060032FE RID: 13054 RVA: 0x000A8A65 File Offset: 0x000A6C65
		public RadListView OwnerListView { get; set; }

		// Token: 0x060032FF RID: 13055 RVA: 0x000A8A70 File Offset: 0x000A6C70
		public ListViewLinqGroupingHelper(RadListView ownerListView, RadListViewPagingManager pagingManager)
		{
			this.OwnerListView = ownerListView;
			this.isPaging = pagingManager.IsPagingEnabled;
			this.pageSize = pagingManager.PageSize;
			this.startIndex = pagingManager.CurrentPageIndex * pagingManager.PageSize;
			this.endIndex = this.startIndex + pagingManager.PageSize;
		}

		// Token: 0x06003300 RID: 13056 RVA: 0x000A8AC8 File Offset: 0x000A6CC8
		public IEnumerable GroupDataItems(IEnumerable originalEnumerable)
		{
			this.OwnerListView.DataSourceGroups = new List<ListViewDataSourceGroup>();
			List<string> groupExpressionFields = this.GetGroupExpressionFields();
			if (this.hasGroupAggregate.Count > 0)
			{
				this.hasAggregates = true;
			}
			this.CreateAllGroups(originalEnumerable, groupExpressionFields, null, 0);
			return this.groupedQueryable;
		}

		// Token: 0x06003301 RID: 13057 RVA: 0x000A8B14 File Offset: 0x000A6D14
		private List<string> GetGroupExpressionFields()
		{
			List<string> list = new List<string>();
			this.hasGroupAggregate = new Dictionary<string, bool>(this.OwnerListView.DataGroups.Count);
			foreach (ListViewDataGroup listViewDataGroup in this.OwnerListView.DataGroups)
			{
				string groupField = listViewDataGroup.GroupField;
				list.Add(groupField);
				this.hasGroupAggregate.Add(groupField, listViewDataGroup.GroupAggregates.Count > 0);
			}
			return list;
		}

		// Token: 0x06003302 RID: 13058 RVA: 0x000A8BDC File Offset: 0x000A6DDC
		private void CreateAllGroups(IEnumerable enumerable, List<string> groupFields, ListViewDataSourceGroup parentGroup, int level)
		{
			if (groupFields.Count > 0)
			{
				string text = groupFields[0];
				bool flag = true;
				int count = this.OwnerListView.DataGroups.Count;
				IQueryable queryable = this.PerformGrouping(enumerable, level, text);
				this.groupedQueryable = queryable;
				groupFields.RemoveAt(0);
				foreach (object obj in queryable)
				{
					ListViewDataSourceGroup group = new ListViewDataSourceGroup();
					group.Key = this.GetPropValue(obj, text);
					IEnumerable enumerable2 = (IEnumerable)obj;
					group.Level = level;
					group.FieldName = text;
					group.ParentGroup = parentGroup;
					if (level == count - 1)
					{
						IQueryable queryable2 = enumerable2.AsQueryable();
						if (!this.isPaging)
						{
							group.DataItems = queryable2;
						}
						int num = queryable2.Count();
						if (this.hasAggregates)
						{
							group.AggregateItems = queryable2;
						}
						group.DataItemsCount = num;
						this.currentItemsCount += num;
						if (this.isPaging && this.currentItemsCount > this.startIndex && !this.itemsCountReached)
						{
							group.IsOnCurrentPage = true;
							this.SetParentGroupPage(group);
							this.itemsCountReached = (this.currentItemsCount >= this.endIndex);
							if (flag)
							{
								flag = false;
								int num2 = this.currentItemsCount - this.startIndex;
								queryable2 = queryable2.Skip(num - num2);
							}
							if (this.itemsCountReached)
							{
								int num3 = this.currentItemsCount - this.endIndex;
								queryable2 = queryable2.Take(Math.Min(num - num3, this.pageSize));
							}
							group.DataItems = queryable2;
							group.DataItemsCount = queryable2.Count();
						}
						if (this.isPaging && this.hasAggregates && this.OwnerListView.GroupAggregatesScope == ListViewGroupAggregatesScope.CurrentPage)
						{
							group.AggregateItems = queryable2;
						}
					}
					else if (this.hasAggregates && (!this.isPaging || this.OwnerListView.GroupAggregatesScope == ListViewGroupAggregatesScope.AllItems))
					{
						group.AggregateItems = enumerable2.AsQueryable();
					}
					this.OwnerListView.DataSourceGroups.Add(group);
					List<string> list = new List<string>();
					list.AddRange(groupFields);
					if (list.Count != 0)
					{
						this.CreateAllGroups(enumerable2, list, group, level + 1);
						if (this.isPaging && this.OwnerListView.GroupAggregatesScope == ListViewGroupAggregatesScope.CurrentPage && level < count - 1)
						{
							IEnumerable<ListViewDataSourceGroup> enumerable3 = from g in this.OwnerListView.DataSourceGroups
							where g.IsOnCurrentPage && g.ParentGroup != null && g.ParentGroup == @group
							select g;
							IEnumerable<object> enumerable4 = null;
							foreach (ListViewDataSourceGroup listViewDataSourceGroup in enumerable3)
							{
								IEnumerable<object> enumerable5 = listViewDataSourceGroup.AggregateItems.OfType<object>();
								if (enumerable4 == null)
								{
									enumerable4 = enumerable5;
								}
								else
								{
									enumerable4 = enumerable4.Concat(enumerable5);
								}
							}
							if (enumerable4 != null)
							{
								group.AggregateItems = this.GetGenericObjectElements(enumerable4.AsQueryable<object>());
							}
						}
					}
				}
			}
		}

		// Token: 0x06003303 RID: 13059 RVA: 0x000A8F84 File Offset: 0x000A7184
		private void SetParentGroupPage(ListViewDataSourceGroup group)
		{
			ListViewDataSourceGroup listViewDataSourceGroup = group;
			while (listViewDataSourceGroup.ParentGroup != null)
			{
				listViewDataSourceGroup = listViewDataSourceGroup.ParentGroup;
				listViewDataSourceGroup.IsOnCurrentPage = true;
			}
		}

		// Token: 0x06003304 RID: 13060 RVA: 0x000A8FB4 File Offset: 0x000A71B4
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		private IQueryable PerformGrouping(IEnumerable enumerable, int level, string groupField)
		{
			string text = "";
			ListViewDataGroupCollection dataGroups = this.OwnerListView.DataGroups;
			enumerable = ListViewLinqEnumerableHelper.ToGenericEnumerable(enumerable);
			IQueryable queryable = enumerable.AsQueryable();
			Type elementType = queryable.ElementType;
			if (dataGroups.Count == level)
			{
				level--;
			}
			RadListViewSortOrder sortOrder = dataGroups[level].SortOrder;
			if (sortOrder == RadListViewSortOrder.Descending)
			{
				text = "Key DESC";
			}
			if (dataGroups[level].SortOrder == RadListViewSortOrder.Ascending)
			{
				text = "Key";
			}
			IQueryable queryable3;
			if (elementType == typeof(DataRowView) || elementType == typeof(DataRow))
			{
				IQueryable<DataRowView> source;
				if (queryable.ElementType == typeof(DataRowView))
				{
					source = queryable.Cast<DataRowView>();
				}
				else
				{
					source = (from DataRow row in queryable
					select row.Table.DefaultView[row.Table.Rows.IndexOf(row)]).Cast<DataRowView>();
				}
				IQueryable<IGrouping<object, DataRowView>> queryable2 = from d in source
				group d by d.Row[groupField];
				queryable3 = queryable2;
				if (text == "Key")
				{
					queryable3 = from g in queryable2
					orderby (g.Key != DBNull.Value) ? g.Key : null
					select g;
				}
				else if (!string.IsNullOrEmpty(text))
				{
					queryable3 = from g in queryable2
					orderby (g.Key != DBNull.Value) ? g.Key : null descending
					select g;
				}
			}
			else
			{
				if (elementType.Name == "EntityDataSourceWrapper")
				{
					queryable = this.GetGenericEntityDataSourceElements(queryable);
				}
				else if (queryable.ElementType.Name == "Object")
				{
					queryable = this.GetGenericObjectElements(queryable);
				}
				if (this.IsSimpleType(elementType))
				{
					groupField = "it";
				}
				queryable3 = queryable.GroupBy(groupField, "it", new object[0]);
				if (!string.IsNullOrEmpty(text))
				{
					queryable3 = queryable3.OrderBy(text, new object[0]);
				}
			}
			return queryable3;
		}

		// Token: 0x06003305 RID: 13061 RVA: 0x000A93B8 File Offset: 0x000A75B8
		private bool IsSimpleType(Type type)
		{
			return type.IsPrimitive || type == typeof(string) || type.IsValueType || type.IsEnum;
		}

		// Token: 0x06003306 RID: 13062 RVA: 0x000A93F0 File Offset: 0x000A75F0
		private IQueryable GetGenericEntityDataSourceElements(IQueryable queryable)
		{
			MethodInfo method = typeof(Queryable).GetMethod("Cast");
			IQueryable queryable2 = queryable;
			if (this.itemsType == null)
			{
				List<ICustomTypeDescriptor> source = queryable.Cast<ICustomTypeDescriptor>().ToList<ICustomTypeDescriptor>();
				queryable2 = (from c in source
				select c.GetPropertyOwner(null)).AsQueryable<object>();
				this.itemsType = source.First<ICustomTypeDescriptor>().GetPropertyOwner(null).GetType();
			}
			MethodInfo methodInfo = method.MakeGenericMethod(new Type[]
			{
				this.itemsType
			});
			return (IQueryable)methodInfo.Invoke(null, new object[]
			{
				queryable2
			});
		}

		// Token: 0x06003307 RID: 13063 RVA: 0x000A94A8 File Offset: 0x000A76A8
		private IQueryable GetGenericObjectElements(IQueryable queryable)
		{
			MethodInfo method = typeof(Queryable).GetMethod("Cast");
			IQueryable queryable2 = queryable;
			if (this.itemsType == null)
			{
				List<object> list = queryable.Cast<object>().ToList<object>();
				this.itemsType = list[0].GetType();
				queryable2 = list.AsQueryable<object>();
			}
			MethodInfo methodInfo = method.MakeGenericMethod(new Type[]
			{
				this.itemsType
			});
			return (IQueryable)methodInfo.Invoke(null, new object[]
			{
				queryable2
			});
		}

		// Token: 0x06003308 RID: 13064 RVA: 0x000A953C File Offset: 0x000A773C
		private object GetPropValue(object src, string propName)
		{
			object obj = null;
			try
			{
				obj = src.GetType().GetProperty("Key").GetValue(src, null);
			}
			catch
			{
				IEnumerable source = src as IEnumerable;
				IQueryable queryable = source.AsQueryable();
				if (queryable.ElementType == typeof(DataRowView))
				{
					this.castedCollection = source.AsQueryable().Cast<DataRowView>();
					using (IEnumerator enumerator = this.castedCollection.Take(1).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj2 = enumerator.Current;
							DataRowView dataRowView = (DataRowView)obj2;
							obj = dataRowView[propName];
						}
						goto IL_13F;
					}
				}
				if (queryable.ElementType.Name == "EntityDataSourceWrapper")
				{
					IQueryable genericEntityDataSourceElements = this.GetGenericEntityDataSourceElements(queryable);
					queryable = genericEntityDataSourceElements.Select(propName, new object[0]).Take(1);
				}
				else if (queryable.ElementType.Name == "Object")
				{
					IQueryable genericObjectElements = this.GetGenericObjectElements(queryable);
					queryable = genericObjectElements.Select(propName, new object[0]).Take(1);
				}
				else
				{
					if (this.IsSimpleType(queryable.ElementType))
					{
						propName = "it";
					}
					queryable = source.AsQueryable().Select(propName, new object[0]).Take(1);
				}
				IL_13F:
				if (obj == null)
				{
					using (IEnumerator enumerator2 = queryable.GetEnumerator())
					{
						if (enumerator2.MoveNext())
						{
							object obj3 = enumerator2.Current;
							obj = obj3;
						}
					}
				}
			}
			return obj;
		}

		// Token: 0x06003309 RID: 13065 RVA: 0x000A9714 File Offset: 0x000A7914
		internal static object GetAggregate(IEnumerable enumerable, string fieldName, Type dataType, ListViewAggregateFunction func)
		{
			if (enumerable == null)
			{
				return null;
			}
			IQueryable queryable = enumerable.AsQueryable();
			if (!string.IsNullOrEmpty(fieldName) && !(queryable.ElementType == typeof(DataRowView)) && !(queryable.ElementType == typeof(DataRow)))
			{
				fieldName = ListViewLinqGroupingHelper.PrepareFieldName(enumerable, queryable, fieldName, dataType);
			}
			MethodInfo method = typeof(ListViewLinqGroupingHelper).GetMethod("GetAggregateByType", BindingFlags.Static | BindingFlags.Public);
			if (dataType != typeof(string) && dataType != typeof(object) && !ListViewLinqGroupingHelper.IsNullableType(dataType))
			{
				dataType = typeof(Nullable<>).MakeGenericType(new Type[]
				{
					dataType
				});
			}
			MethodInfo methodInfo = method.MakeGenericMethod(new Type[]
			{
				dataType
			});
			return methodInfo.Invoke(null, new object[]
			{
				queryable,
				fieldName,
				func
			});
		}

		// Token: 0x0600330A RID: 13066 RVA: 0x000A980C File Offset: 0x000A7A0C
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)")]
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
		private static string PrepareFieldName(IEnumerable enumerable, IQueryable queryable, string fieldName, Type dataType)
		{
			if (enumerable == null)
			{
				return "";
			}
			dataType = ListViewLinqGroupingHelper.GetNonNullableType(dataType);
			string arg = dataType.ToString().Split(new char[]
			{
				'.'
			})[1];
			if (dataType != typeof(string) && dataType != typeof(object))
			{
				arg = string.Format("{0}?", arg);
				return string.Format("{0}({1})", arg, fieldName);
			}
			if (dataType == typeof(string))
			{
				return string.Format("{0}({1})", "String", fieldName);
			}
			return string.Format("{0}({1})", "object", fieldName);
		}

		// Token: 0x0600330B RID: 13067 RVA: 0x000A98B7 File Offset: 0x000A7AB7
		private static Type GetNonNullableType(Type type)
		{
			if (!ListViewLinqGroupingHelper.IsNullableType(type))
			{
				return type;
			}
			return type.GetGenericArguments()[0];
		}

		// Token: 0x0600330C RID: 13068 RVA: 0x000A98CB File Offset: 0x000A7ACB
		private static bool IsNullableType(Type type)
		{
			return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
		}

		// Token: 0x0600330D RID: 13069 RVA: 0x000A98F4 File Offset: 0x000A7AF4
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
		public static object GetAggregateByType<T>(IQueryable queryable, string fieldName, ListViewAggregateFunction func)
		{
			if (func == ListViewAggregateFunction.First)
			{
				if (queryable.ElementType == typeof(DataRowView))
				{
					return (from DataRowView d in queryable
					select d.Row[fieldName]).First<object>();
				}
				return ((IQueryable<T>)queryable.Take(1).Select(fieldName, new object[0])).First<T>();
			}
			else
			{
				IQueryable<T> source;
				if (!string.IsNullOrEmpty(fieldName))
				{
					if (queryable.ElementType == typeof(DataRowView))
					{
						source = (from DataRowView g in queryable
						select g.Row[fieldName]).Cast<T>();
					}
					else if (queryable.ElementType == typeof(DataRow))
					{
						IQueryable<DataRowView> source2 = (from DataRow row in queryable
						select row.Table.DefaultView[row.Table.Rows.IndexOf(row)]).Cast<DataRowView>();
						source = (from g in source2
						select g.Row[fieldName]).Cast<T>();
					}
					else
					{
						source = (IQueryable<T>)queryable.Select(fieldName, new object[0]);
					}
				}
				else
				{
					source = queryable.OfType<T>().AsQueryable<T>();
				}
				Type nonNullableType = ListViewLinqGroupingHelper.GetNonNullableType(typeof(T));
				if (func == ListViewAggregateFunction.Last)
				{
					return source.Last<T>();
				}
				if (func == ListViewAggregateFunction.Avg)
				{
					if (nonNullableType == typeof(short))
					{
						return source.Cast<short>().Average((short n) => (int)((short)n));
					}
					if (nonNullableType == typeof(int))
					{
						return source.Cast<int>().Average();
					}
					if (nonNullableType == typeof(long))
					{
						return source.Cast<long>().Average((long n) => (long)n);
					}
					if (nonNullableType == typeof(long))
					{
						return source.Cast<long>().Average((long n) => (long)n);
					}
					if (nonNullableType == typeof(decimal))
					{
						return source.Cast<decimal>().Average();
					}
					if (nonNullableType == typeof(float))
					{
						return source.Cast<float>().Average((float n) => (float)n);
					}
					if (nonNullableType == typeof(double))
					{
						return source.Cast<double>().Average();
					}
					if (nonNullableType == typeof(uint))
					{
						return source.Cast<uint>().Average((uint n) => (long)((uint)n));
					}
					if (nonNullableType == typeof(short))
					{
						return source.Cast<short>().Average((short n) => (int)((short)n));
					}
					if (nonNullableType == typeof(ushort))
					{
						return source.Cast<ushort>().Average((ushort n) => (int)((ushort)n));
					}
					throw new NotSupportedException(string.Format("Average is not supported for type \"{0}\"", nonNullableType));
				}
				else if (func == ListViewAggregateFunction.Sum)
				{
					if (nonNullableType == typeof(short))
					{
						return source.Cast<short>().Sum((short n) => (int)((short)n));
					}
					if (nonNullableType == typeof(int))
					{
						return source.Cast<int>().Sum();
					}
					if (nonNullableType == typeof(long))
					{
						return source.Cast<long>().Sum((long n) => (long)n);
					}
					if (nonNullableType == typeof(long))
					{
						return source.Cast<long>().Sum((long n) => (long)n);
					}
					if (nonNullableType == typeof(decimal))
					{
						return source.Cast<decimal>().Sum();
					}
					if (nonNullableType == typeof(float))
					{
						return source.Cast<float>().Sum((float n) => (float)n);
					}
					if (nonNullableType == typeof(double))
					{
						return source.Cast<double>().Sum();
					}
					if (nonNullableType == typeof(uint))
					{
						return source.Cast<uint>().Sum((uint n) => (long)((uint)n));
					}
					if (nonNullableType == typeof(short))
					{
						return source.Cast<short>().Sum((short n) => (int)((short)n));
					}
					if (nonNullableType == typeof(ushort))
					{
						return source.Cast<ushort>().Sum((ushort n) => (int)((ushort)n));
					}
					throw new NotSupportedException(string.Format("Sum is not supported for type \"{0}\"", typeof(T)));
				}
				else
				{
					if (func == ListViewAggregateFunction.CountDistinct)
					{
						return source.Distinct<T>().Count<T>();
					}
					if (func == ListViewAggregateFunction.Count)
					{
						return source.Count<T>();
					}
					if (func == ListViewAggregateFunction.Max)
					{
						return source.Max<T>();
					}
					if (func == ListViewAggregateFunction.Min)
					{
						return source.Min<T>();
					}
					return null;
				}
			}
		}

		// Token: 0x04000DF8 RID: 3576
		private Type itemsType;

		// Token: 0x04000DF9 RID: 3577
		private bool isPaging;

		// Token: 0x04000DFA RID: 3578
		private int startIndex;

		// Token: 0x04000DFB RID: 3579
		private int endIndex;

		// Token: 0x04000DFC RID: 3580
		private int currentItemsCount;

		// Token: 0x04000DFD RID: 3581
		private int pageSize;

		// Token: 0x04000DFE RID: 3582
		private bool itemsCountReached;

		// Token: 0x04000DFF RID: 3583
		private bool hasAggregates;

		// Token: 0x04000E00 RID: 3584
		private Dictionary<string, bool> hasGroupAggregate;

		// Token: 0x04000E01 RID: 3585
		private IQueryable groupedQueryable;

		// Token: 0x04000E02 RID: 3586
		private IQueryable<DataRowView> castedCollection;
	}
}
