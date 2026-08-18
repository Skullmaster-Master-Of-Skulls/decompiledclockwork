using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000B7F RID: 2943
	internal class GridLinqGroupingHelper
	{
		// Token: 0x17002471 RID: 9329
		// (get) Token: 0x06006F1D RID: 28445 RVA: 0x0019BECF File Offset: 0x0019A0CF
		// (set) Token: 0x06006F1E RID: 28446 RVA: 0x0019BED7 File Offset: 0x0019A0D7
		public GridTableView OwnerTableView { get; set; }

		// Token: 0x17002472 RID: 9330
		// (get) Token: 0x06006F1F RID: 28447 RVA: 0x0019BEE0 File Offset: 0x0019A0E0
		// (set) Token: 0x06006F20 RID: 28448 RVA: 0x0019BEE8 File Offset: 0x0019A0E8
		public GridGroupHeaderItem GroupHeaderItem { get; set; }

		// Token: 0x06006F21 RID: 28449 RVA: 0x0019BEF1 File Offset: 0x0019A0F1
		public GridLinqGroupingHelper(GridTableView ownertableView)
		{
			this.OwnerTableView = ownertableView;
		}

		// Token: 0x06006F22 RID: 28450 RVA: 0x0019BF1C File Offset: 0x0019A11C
		public IEnumerable GetGroupedItemsForCurrentPage(IQueryable queryable)
		{
			List<string> groupExpressionFields = this.GetGroupExpressionFields();
			this.OwnerTableView.groups = new List<GridGroup>();
			this.CreateAllGroups(queryable, groupExpressionFields, null, 0);
			IEnumerable<GridGroup> enumerable = from g in this.OwnerTableView.groups
			where g.Level == this.OwnerTableView.GroupByExpressions.Count - 1
			select g;
			List<object> list = new List<object>();
			foreach (GridGroup gridGroup in enumerable)
			{
				IQueryable queryable2 = gridGroup.Items.AsQueryable();
				foreach (object item in queryable2)
				{
					list.Add(item);
				}
			}
			list = list.ToList<object>();
			this.OwnerTableView.itemsCountWhenGrouping = list.Count;
			bool flag = false;
			if (!this.OwnerTableView.isDataSourceViewFilter)
			{
				flag = this.OwnerTableView.AllowCustomPaging;
			}
			int num = flag ? 0 : (this.OwnerTableView.PageSize * this.OwnerTableView.CurrentPageIndex);
			if (num >= this.OwnerTableView.itemsCountWhenGrouping && this.OwnerTableView.OwnerGrid.CurrentPageIndex > 0)
			{
				this.OwnerTableView.OwnerGrid.CurrentPageIndex = this.OwnerTableView.itemsCountWhenGrouping / this.OwnerTableView.PageSize;
				if (num == this.OwnerTableView.itemsCountWhenGrouping && this.OwnerTableView.OwnerGrid.CurrentPageIndex > 0)
				{
					this.OwnerTableView.OwnerGrid.CurrentPageIndex--;
				}
				num = this.OwnerTableView.OwnerGrid.CurrentPageIndex * this.OwnerTableView.PageSize;
			}
			int count = this.OwnerTableView.AllowPaging ? this.OwnerTableView.PageSize : this.OwnerTableView.itemsCountWhenGrouping;
			return list.Skip(num).Take(count);
		}

		// Token: 0x06006F23 RID: 28451 RVA: 0x0019C134 File Offset: 0x0019A334
		private List<string> GetGroupExpressionFields()
		{
			List<string> list = new List<string>();
			foreach (GridGroupByExpression gridGroupByExpression in this.OwnerTableView.GroupByExpressions)
			{
				foreach (object obj in gridGroupByExpression.GroupByFields)
				{
					GridGroupByField gridGroupByField = (GridGroupByField)obj;
					list.Add(gridGroupByField.FieldName);
				}
			}
			return list;
		}

		// Token: 0x06006F24 RID: 28452 RVA: 0x0019C1E4 File Offset: 0x0019A3E4
		public void CreateAllGroups(IEnumerable enumerable, List<string> groupFields, GridGroup parentGroup, int level)
		{
			if (groupFields.Count > 0)
			{
				string text = groupFields[0];
				int num = (level == this.OwnerTableView.GroupByExpressions.Count) ? (level - 1) : level;
				if (this.OwnerTableView.GroupByExpressions.Count < num)
				{
					num = this.OwnerTableView.GroupByExpressions.Count - 1;
				}
				IQueryable source;
				if (this.OwnerTableView.GroupByExpressions[num].GroupByFields.Count > 1)
				{
					source = this.PerformGroupingByMultiFields(enumerable, level, text);
				}
				else
				{
					source = this.PerformGrouping(enumerable, level, text);
				}
				groupFields.RemoveAt(0);
				foreach (object obj in source.AsQueryable())
				{
					GridGroup gridGroup = new GridGroup();
					gridGroup.Key = this.GetPropValue(obj, text);
					IEnumerable enumerable2 = (IEnumerable)obj;
					gridGroup.Level = level;
					gridGroup.Items = enumerable2;
					gridGroup.FieldName = text;
					gridGroup.ParentGroup = parentGroup;
					List<string> list = new List<string>();
					list.AddRange(groupFields);
					this.OwnerTableView.groups.Add(gridGroup);
					if (list.Count != 0)
					{
						this.CreateAllGroups(enumerable2, list, gridGroup, level + 1);
					}
				}
			}
		}

		// Token: 0x06006F25 RID: 28453 RVA: 0x0019C360 File Offset: 0x0019A560
		private IQueryable PerformGroupingByMultiFields(IEnumerable enumerable, int level, string groupField)
		{
			GridGroupByExpressionCollection groupByExpressions = this.OwnerTableView.GroupByExpressions;
			IQueryable queryable = enumerable.AsQueryable();
			if (groupByExpressions.Count <= level)
			{
				level = groupByExpressions.Count - 1;
			}
			GridGroupByFieldList groupByFields = groupByExpressions[level].GroupByFields;
			GridGroupByFieldList gridGroupByFieldList = new GridGroupByFieldList();
			int num = groupByFields.Count - 1;
			for (int i = num; i >= 0; i--)
			{
				gridGroupByFieldList.Add(groupByFields[i]);
			}
			if (queryable.ElementType == typeof(DataRow))
			{
				queryable = (from DataRow row in queryable
				select row.Table.DefaultView[row.Table.Rows.IndexOf(row)]).Cast<DataRowView>();
			}
			IQueryable result;
			if (queryable.ElementType == typeof(DataRowView))
			{
				IQueryable<DataRowView> source = queryable.Cast<DataRowView>();
				foreach (object obj in gridGroupByFieldList)
				{
					GridGroupByField gridGroupByField = (GridGroupByField)obj;
					string fieldName = gridGroupByField.FieldName;
					if (gridGroupByField.SortOrder == GridSortOrder.Ascending)
					{
						source = from d in source
						orderby (d.Row[fieldName] != DBNull.Value) ? d.Row[fieldName] : null
						select d;
					}
					else if (gridGroupByField.SortOrder == GridSortOrder.Descending)
					{
						source = from d in source
						orderby (d.Row[fieldName] != DBNull.Value) ? d.Row[fieldName] : null descending
						select d;
					}
				}
				result = from d in source
				group d by d.Row[groupField];
			}
			else
			{
				IQueryable source2;
				if (queryable.ElementType.Name == "EntityDataSourceWrapper")
				{
					source2 = this.GetGenericEntityDataSourceElements(queryable);
				}
				else if (queryable.ElementType.Name == "Object")
				{
					source2 = this.GetGenericObjectElements(queryable);
				}
				else if (this.IsSimpleType(queryable.ElementType))
				{
					groupField = "it";
				}
				string text = "";
				foreach (object obj2 in groupByFields)
				{
					GridGroupByField gridGroupByField2 = (GridGroupByField)obj2;
					string fieldName2 = gridGroupByField2.FieldName;
					if (gridGroupByField2.SortOrder == GridSortOrder.Descending)
					{
						text += string.Format("{0} DESC,", gridGroupByField2.FieldName);
					}
					else
					{
						text += string.Format("{0},", gridGroupByField2.FieldName);
					}
				}
				text = text.Substring(0, text.Length - 1);
				source2 = queryable.OrderBy(text, new object[0]);
				result = source2.GroupBy(groupField, "it", new object[0]);
			}
			return result;
		}

		// Token: 0x06006F26 RID: 28454 RVA: 0x0019C958 File Offset: 0x0019AB58
		private IQueryable GroupByAscending(IQueryable<DataRowView> data, string groupField)
		{
			return from d in data
			group d by d.Row[groupField] into g
			orderby (g.Key != DBNull.Value) ? g.Key : null
			select g;
		}

		// Token: 0x06006F27 RID: 28455 RVA: 0x0019CA84 File Offset: 0x0019AC84
		private IQueryable GroupByDescending(IQueryable<DataRowView> data, string groupField)
		{
			return from d in data
			group d by d.Row[groupField] into g
			orderby (g.Key != DBNull.Value) ? g.Key : null descending
			select g;
		}

		// Token: 0x06006F28 RID: 28456 RVA: 0x0019CBA8 File Offset: 0x0019ADA8
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		private IQueryable PerformGrouping(IEnumerable enumerable, int level, string groupField)
		{
			bool flag = false;
			GridGroupByExpressionCollection groupByExpressions = this.OwnerTableView.GroupByExpressions;
			if (groupByExpressions.Count == level)
			{
				level--;
			}
			string ordering;
			if (groupByExpressions[level].GroupByFields[0].SortOrder == GridSortOrder.Descending)
			{
				ordering = "Key DESC";
			}
			else
			{
				flag = true;
				ordering = "Key";
			}
			IQueryable queryable = enumerable.AsQueryable();
			IQueryable result;
			if (queryable.ElementType == typeof(DataRowView))
			{
				if (flag)
				{
					result = this.GroupByAscending(queryable.Cast<DataRowView>(), groupField);
				}
				else
				{
					result = this.GroupByDescending(queryable.Cast<DataRowView>(), groupField);
				}
			}
			else if (queryable.ElementType == typeof(DataRow))
			{
				IQueryable<DataRowView> data = (from DataRow row in queryable
				select row.Table.DefaultView[row.Table.Rows.IndexOf(row)]).Cast<DataRowView>();
				if (flag)
				{
					result = this.GroupByAscending(data, groupField);
				}
				else
				{
					result = this.GroupByDescending(data, groupField);
				}
			}
			else if (queryable.ElementType.Name == "EntityDataSourceWrapper")
			{
				IQueryable genericEntityDataSourceElements = this.GetGenericEntityDataSourceElements(queryable);
				result = genericEntityDataSourceElements.GroupBy(groupField, "it", new object[0]).OrderBy(ordering, new object[0]);
			}
			else if (queryable.ElementType.Name == "Object")
			{
				IQueryable genericObjectElements = this.GetGenericObjectElements(queryable);
				result = genericObjectElements.GroupBy(groupField, "it", new object[0]).OrderBy(ordering, new object[0]);
			}
			else
			{
				if (this.IsSimpleType(queryable.ElementType))
				{
					groupField = "it";
				}
				result = queryable.GroupBy(groupField, "it", new object[0]).OrderBy(ordering, new object[0]);
			}
			return result;
		}

		// Token: 0x06006F29 RID: 28457 RVA: 0x0019CE1F File Offset: 0x0019B01F
		private bool IsSimpleType(Type type)
		{
			return type.IsPrimitive || type == typeof(string) || type.IsValueType || type.IsEnum;
		}

		// Token: 0x06006F2A RID: 28458 RVA: 0x0019CE58 File Offset: 0x0019B058
		public IQueryable GetGenericEntityDataSourceElements(IQueryable queryable)
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

		// Token: 0x06006F2B RID: 28459 RVA: 0x0019CF10 File Offset: 0x0019B110
		public IQueryable GetGenericObjectElements(IQueryable queryable)
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

		// Token: 0x06006F2C RID: 28460 RVA: 0x0019CFA4 File Offset: 0x0019B1A4
		public object GetPropValue(object src, string propName)
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

		// Token: 0x06006F2D RID: 28461 RVA: 0x0019D17C File Offset: 0x0019B37C
		public void EnsureOriginalGroup(GridGroupHeaderItem headerItem)
		{
			if (headerItem.OriginalGroup != null)
			{
				return;
			}
			GridGroupByExpression gridGroupByExpression = this.OwnerTableView.GroupByExpressions[headerItem.GroupLevel];
			headerItem.OriginalGroup = this.GetCorrespondingOriginalGroup(new Dictionary<GridGroupHeaderItem, List<GridItem>>
			{
				{
					headerItem,
					null
				}
			}.First<KeyValuePair<GridGroupHeaderItem, List<GridItem>>>(), gridGroupByExpression, headerItem.DataItem as DataRowView, gridGroupByExpression.SelectFields[gridGroupByExpression.SelectFields.Count - 1]);
		}

		// Token: 0x06006F2E RID: 28462 RVA: 0x0019D2F8 File Offset: 0x0019B4F8
		public void CalculateAggregatesWhenLinqGrouping(ControlCollection rows, GridGroupByExpression expression, DataRowView drv, GridGroupByField field)
		{
			string groupByFieldName = field.FieldName;
			string text = string.Empty;
			int num = expression.GroupByFields.Count - 1;
			if (!drv.DataView.Table.Columns.Contains(groupByFieldName))
			{
				int index = (num >= expression.SelectFields.Count) ? (expression.SelectFields.Count - 1) : num;
				if (groupByFieldName == expression.GroupByFields[num].FieldName || drv.DataView.Table.Columns.Contains(expression.SelectFields[index].FieldAlias))
				{
					groupByFieldName = expression.SelectFields[index].FieldAlias;
				}
				else
				{
					groupByFieldName = expression.GroupByFields[num].FieldName;
				}
			}
			text = groupByFieldName;
			if (this.GroupHeaderItem.OriginalGroup == null)
			{
				string text2 = string.Empty;
				if (expression.GroupByFields.Count > num)
				{
					text2 = expression.GroupByFields[num].FieldName;
				}
				if (text2 != string.Empty && drv.DataView.Table.Columns.Contains(text2))
				{
					groupByFieldName = text2;
				}
				if (drv[groupByFieldName] == DBNull.Value || drv[groupByFieldName] == null)
				{
					return;
				}
				Type columnType = drv[groupByFieldName].GetType();
				List<GridGroup> list = (from g in this.OwnerTableView.groups
				where g.Key != null
				where g.FieldName == groupByFieldName && Convert.ChangeType(g.Key, Type.GetType(columnType.FullName)).ToString().Trim() == drv[groupByFieldName].ToString()
				select g).ToList<GridGroup>();
				if (list.Count == 0)
				{
					groupByFieldName = expression.GroupByFields[num].FieldName;
					if (!drv.DataView.Table.Columns.Contains(groupByFieldName))
					{
						groupByFieldName = expression.SelectFields[num].FieldAlias;
					}
					list = (from g in this.OwnerTableView.groups
					where g.Key != null
					where g.FieldName == groupByFieldName && Convert.ChangeType(g.Key, Type.GetType(columnType.FullName)).ToString() == drv[groupByFieldName].ToString()
					select g).ToList<GridGroup>();
				}
				int num2 = rows.IndexOf(this.GroupHeaderItem);
				bool flag = false;
				int num3 = (list.Count > 0) ? list[0].Level : 0;
				for (int i = 1; i < list.Count; i++)
				{
					if (list[i].Level == num3)
					{
						flag = true;
						break;
					}
				}
				int j = 0;
				while (j < list.Count)
				{
					if (this.FoundSearchedGroup(list[j].ParentGroup, num2 - 1, rows, groupByFieldName))
					{
						this.GroupHeaderItem.OriginalGroup = list[j];
						if (flag)
						{
							Dictionary<GridGroupHeaderItem, List<GridItem>> dictionary = new Dictionary<GridGroupHeaderItem, List<GridItem>>();
							dictionary.Add(this.GroupHeaderItem, null);
							this.GroupHeaderItem.OriginalGroup = this.GetCorrespondingOriginalGroup(dictionary.First<KeyValuePair<GridGroupHeaderItem, List<GridItem>>>(), expression, drv, field);
							break;
						}
						break;
					}
					else
					{
						j++;
					}
				}
			}
			Type dataType = typeof(object);
			if (!drv.DataView.Table.Columns.Contains(groupByFieldName))
			{
				dataType = drv[field.FieldName].GetType();
			}
			else if (drv.DataView.Table.Columns.Contains(field.FieldAlias) && drv[field.FieldAlias].GetType().FullName != "System.DBNull")
			{
				dataType = drv[field.FieldAlias].GetType();
			}
			else if (drv.DataView.Table.Columns.Contains(text))
			{
				dataType = drv[text].GetType();
			}
			else
			{
				dataType = drv[groupByFieldName].GetType();
			}
			if (this.GroupHeaderItem.OriginalGroup.Items.AsQueryable().ElementType == typeof(DataRowView) || this.GroupHeaderItem.OriginalGroup.Items.AsQueryable().ElementType == typeof(DataRow))
			{
				dataType = this.GroupHeaderItem.OriginalGroup.Items.AsQueryable().Take(1).Cast<DataRowView>().First<DataRowView>().Row[field.FieldName].GetType();
			}
			IQueryable queryable = this.GroupHeaderItem.OriginalGroup.Items.AsQueryable();
			if (expression.GroupByFields.Count > 1)
			{
				queryable = this.GetItemsForMultiFieldsGroup(expression, drv, queryable);
			}
			if (field.Aggregate == GridAggregateFunction.Count)
			{
				drv[field.FieldAlias] = queryable.Count();
				return;
			}
			object aggregate = this.GetAggregate(queryable, queryable, field.FieldName, dataType, field.Aggregate);
			drv[field.FieldAlias] = aggregate;
		}

		// Token: 0x06006F2F RID: 28463 RVA: 0x0019D984 File Offset: 0x0019BB84
		private IQueryable GetItemsForMultiFieldsGroup(GridGroupByExpression expression, DataRowView drv, IQueryable queryable)
		{
			GridLinqGroupingHelper.<>c__DisplayClass18 CS$<>8__locals1 = new GridLinqGroupingHelper.<>c__DisplayClass18();
			CS$<>8__locals1.expression = expression;
			CS$<>8__locals1.drv = drv;
			CS$<>8__locals1.<>4__this = this;
			IEnumerable<GridGroup> enumerable = from g in this.OwnerTableView.groups
			where g.Key.ToString() == CS$<>8__locals1.drv[CS$<>8__locals1.expression.GroupByFields[0].FieldName].ToString() && g.Level.ToString() == (CS$<>8__locals1.<>4__this.OwnerTableView.GroupByExpressions.Count - 1).ToString()
			select g;
			GridGroupByFieldList groupByFields = CS$<>8__locals1.expression.GroupByFields;
			GridGroupByFieldList gridGroupByFieldList = new GridGroupByFieldList();
			int num = groupByFields.Count - 1;
			for (int i = num; i >= 0; i--)
			{
				gridGroupByFieldList.Add(groupByFields[i]);
			}
			if (queryable.ElementType == typeof(DataRowView))
			{
				using (IEnumerator<GridGroup> enumerator = enumerable.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						GridGroup gridGroup = enumerator.Current;
						GridGroup gridGroup2 = gridGroup;
						IQueryable<DataRowView> queryable2 = gridGroup2.Items.AsQueryable().Cast<DataRowView>();
						for (int j = 1; j < CS$<>8__locals1.expression.GroupByFields.Count; j++)
						{
							GridGroupByField grField = CS$<>8__locals1.expression.GroupByFields[j];
							string fieldName = GridLinqGroupingHelper.GetFieldName(CS$<>8__locals1.expression, CS$<>8__locals1.drv, grField);
							queryable2 = from d in queryable2
							where d.Row[grField.FieldName].ToString() == CS$<>8__locals1.drv[fieldName].ToString()
							select d;
						}
						if (queryable2.Count() > 0)
						{
							queryable = queryable2;
							break;
						}
					}
					return queryable;
				}
			}
			GridLinqGroupingHelper.<>c__DisplayClass1c CS$<>8__locals3 = new GridLinqGroupingHelper.<>c__DisplayClass1c();
			CS$<>8__locals3.CS$<>8__locals19 = CS$<>8__locals1;
			CS$<>8__locals3.whereClause = "";
			for (int k = 1; k < CS$<>8__locals1.expression.GroupByFields.Count; k++)
			{
				GridGroupByField gridGroupByField = CS$<>8__locals1.expression.GroupByFields[k];
				string fieldName2 = GridLinqGroupingHelper.GetFieldName(CS$<>8__locals1.expression, CS$<>8__locals1.drv, gridGroupByField);
				string text = CS$<>8__locals1.drv[fieldName2].ToString();
				if (text.Contains(" ") || CS$<>8__locals1.drv[fieldName2].GetType().FullName == "System.String" || CS$<>8__locals1.drv[fieldName2].GetType().FullName == "System.DateTime")
				{
					text = string.Format("\"{0}\"", text);
				}
				GridLinqGroupingHelper.<>c__DisplayClass1c CS$<>8__locals4 = CS$<>8__locals3;
				CS$<>8__locals4.whereClause += string.Format("{0}={1} AND ", gridGroupByField.FieldName, text);
			}
			if (CS$<>8__locals3.whereClause.Length > 0)
			{
				CS$<>8__locals3.whereClause = CS$<>8__locals3.whereClause.Substring(0, CS$<>8__locals3.whereClause.Length - 5);
			}
			queryable = (from g in enumerable
			select g.Items.AsQueryable().Where(CS$<>8__locals3.whereClause, new object[0])).First<IQueryable>().AsQueryable();
			return queryable;
		}

		// Token: 0x06006F30 RID: 28464 RVA: 0x0019DD94 File Offset: 0x0019BF94
		private static string GetFieldName(GridGroupByExpression expression, DataRowView drv, GridGroupByField grField)
		{
			string text = grField.FieldName;
			if (!drv.DataView.Table.Columns.Contains(text))
			{
				foreach (object obj in expression.SelectFields)
				{
					GridGroupByField gridGroupByField = (GridGroupByField)obj;
					if (gridGroupByField.FieldName == text)
					{
						text = gridGroupByField.FieldAlias;
					}
				}
			}
			return text;
		}

		// Token: 0x06006F31 RID: 28465 RVA: 0x0019DE1C File Offset: 0x0019C01C
		internal bool FoundSearchedGroup(GridGroup gridGroup, int curretnRowIndex, ControlCollection rows, string fieldName)
		{
			if (curretnRowIndex < 0)
			{
				return true;
			}
			GridGroupHeaderItem gridGroupHeaderItem = rows[curretnRowIndex] as GridGroupHeaderItem;
			return gridGroupHeaderItem == null || !(gridGroupHeaderItem.DataItem as DataRowView).DataView.Table.Columns.Contains(gridGroup.FieldName) || (gridGroup.Key.ToString() == (gridGroupHeaderItem.DataItem as DataRowView)[gridGroup.FieldName].ToString() && this.FoundSearchedGroup(gridGroup.ParentGroup, curretnRowIndex - 1, rows, fieldName));
		}

		// Token: 0x06006F32 RID: 28466 RVA: 0x0019DEAC File Offset: 0x0019C0AC
		internal object GetAggregate(IEnumerable enumerable, IQueryable queryable, string fieldName, Type dataType, GridAggregateFunction func)
		{
			if (enumerable == null)
			{
				return null;
			}
			if (!string.IsNullOrEmpty(fieldName) && !(queryable.ElementType == typeof(DataRowView)) && !(queryable.ElementType == typeof(DataRow)))
			{
				fieldName = this.PrepareFieldName(enumerable, queryable, fieldName, dataType);
			}
			MethodInfo method = typeof(GridLinqGroupingHelper).GetMethod("GetAggregateByType", BindingFlags.Static | BindingFlags.Public);
			if (dataType != typeof(string) && dataType != typeof(object) && !GridLinqGroupingHelper.IsNullableType(dataType))
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

		// Token: 0x06006F33 RID: 28467 RVA: 0x0019DFA0 File Offset: 0x0019C1A0
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)")]
		public string PrepareFieldName(IEnumerable enumerable, IQueryable queryable, string fieldName, Type dataType)
		{
			if (enumerable == null)
			{
				return "";
			}
			dataType = GridLinqGroupingHelper.GetNonNullableType(dataType);
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

		// Token: 0x06006F34 RID: 28468 RVA: 0x0019E050 File Offset: 0x0019C250
		public static Type GetNonNullableType(Type type)
		{
			if (!GridLinqGroupingHelper.IsNullableType(type))
			{
				return type;
			}
			return type.GetGenericArguments()[0];
		}

		// Token: 0x06006F35 RID: 28469 RVA: 0x0019E064 File Offset: 0x0019C264
		public static bool IsNullableType(Type type)
		{
			return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
		}

		// Token: 0x06006F36 RID: 28470 RVA: 0x0019E090 File Offset: 0x0019C290
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
		public static object GetAggregateByType<T>(IQueryable queryable, string fieldName, GridAggregateFunction func)
		{
			if (func == GridAggregateFunction.First)
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
				Type nonNullableType = GridLinqGroupingHelper.GetNonNullableType(typeof(T));
				if (func == GridAggregateFunction.Last)
				{
					return source.Last<T>();
				}
				if (func == GridAggregateFunction.Avg)
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
				else if (func == GridAggregateFunction.Sum)
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
					if (func == GridAggregateFunction.CountDistinct)
					{
						return source.Distinct<T>().Count<T>();
					}
					if (func == GridAggregateFunction.Count)
					{
						return source.Count<T>();
					}
					if (func == GridAggregateFunction.Max)
					{
						return source.Max<T>();
					}
					if (func == GridAggregateFunction.Min)
					{
						return source.Min<T>();
					}
					return null;
				}
			}
		}

		// Token: 0x06006F37 RID: 28471 RVA: 0x0019EB08 File Offset: 0x0019CD08
		internal void SetGroupHeaderItemsContinueText()
		{
			Dictionary<GridGroupHeaderItem, List<GridItem>> dictionary = new Dictionary<GridGroupHeaderItem, List<GridItem>>();
			Dictionary<GridGroupHeaderItem, List<GridItem>> dictionary2 = new Dictionary<GridGroupHeaderItem, List<GridItem>>();
			GridItem[] items = this.OwnerTableView.GetItems(new GridItemType[]
			{
				GridItemType.GroupHeader
			});
			if (items.Count<GridItem>() > 0)
			{
				GridGroupHeaderItem headerItem = items[0] as GridGroupHeaderItem;
				GridGroupHeaderItem headerItem2 = (from GridGroupHeaderItem g in items
				where g.GroupLevel == 0
				select g).Last<GridGroupHeaderItem>();
				this.PopulateGroups(headerItem, dictionary);
				this.PopulateGroups(headerItem2, dictionary2);
				foreach (KeyValuePair<GridGroupHeaderItem, List<GridItem>> pair in dictionary)
				{
					this.SetGroupSplitDisplayFormat(dictionary, pair, GridGroupSplitMode.Continued);
				}
				foreach (KeyValuePair<GridGroupHeaderItem, List<GridItem>> pair2 in dictionary2)
				{
					this.SetGroupSplitDisplayFormat(dictionary2, pair2, GridGroupSplitMode.Continues);
				}
			}
		}

		// Token: 0x06006F38 RID: 28472 RVA: 0x0019EC1C File Offset: 0x0019CE1C
		private void SetGroupSplitDisplayFormat(Dictionary<GridGroupHeaderItem, List<GridItem>> firstItemHeaders, KeyValuePair<GridGroupHeaderItem, List<GridItem>> pair, GridGroupSplitMode groupSplitMode)
		{
			GridGroupByExpression gridGroupByExpression = this.OwnerTableView.GroupByExpressions[pair.Key.GroupLevel];
			DataRowView drv = pair.Key.DataItem as DataRowView;
			if (gridGroupByExpression.GroupByFields.Count > 1)
			{
				int num = 0;
				GridGroupByField field = gridGroupByExpression.GroupByFields[0];
				GridGroup correspondingOriginalGroup = this.GetCorrespondingOriginalGroup(pair, gridGroupByExpression, drv, field);
				if (correspondingOriginalGroup == null)
				{
					return;
				}
				num = this.GetItemsCountForMultyFieldsGroups(gridGroupByExpression, drv, num, correspondingOriginalGroup);
				if (num > pair.Value.Count)
				{
					TableCell dataCell = pair.Key.DataCell;
					if (dataCell.Text.Contains(this.OwnerTableView.OwnerGrid.GroupingSettings.GroupContinuesFormatString))
					{
						return;
					}
					string groupContinuesFormat = this.GetGroupContinuesFormat(groupSplitMode);
					string str = string.Format(this.OwnerTableView.OwnerGrid.GroupingSettings.GroupSplitFormat, string.Format(groupContinuesFormat, pair.Value.Count, num));
					if (this.OwnerTableView.groupHeaderTemplate == null)
					{
						TableCell tableCell = dataCell;
						tableCell.Text += str;
						return;
					}
				}
			}
			else
			{
				GridGroupByField field2 = gridGroupByExpression.GroupByFields[0];
				try
				{
					GridGroup gridGroup = pair.Key.OriginalGroup;
					if (gridGroup == null)
					{
						gridGroup = this.GetCorrespondingOriginalGroup(pair, gridGroupByExpression, drv, field2);
						if (gridGroup == null)
						{
							return;
						}
					}
					int num2 = gridGroup.Items.AsQueryable().Count();
					if (pair.Value.Count != num2)
					{
						if (groupSplitMode == GridGroupSplitMode.Continued)
						{
							this.SortOriginalGroupItems(gridGroup);
							if (gridGroup.Items.AsQueryable().ElementType == typeof(DataRowView) || gridGroup.Items.AsQueryable().ElementType == typeof(DataRow))
							{
								DataRowView dataRowView = gridGroup.Items.AsQueryable().Take(1).Cast<DataRowView>().First<DataRowView>();
								DataRowView dataRowView2 = pair.Value[0].DataItem as DataRowView;
								if (dataRowView == dataRowView2)
								{
									return;
								}
								if (this.OwnerTableView.PageSize < num2)
								{
									DataRowView dataRowView3 = gridGroup.Items.AsQueryable().Skip(num2 - 1).Take(1).Cast<DataRowView>().First<DataRowView>();
									DataRowView dataRowView4 = pair.Value[pair.Value.Count - 1].DataItem as DataRowView;
									if (dataRowView3 != dataRowView4)
									{
										groupSplitMode = GridGroupSplitMode.Both;
									}
								}
							}
							else
							{
								object dataItem = pair.Value[0].DataItem;
								foreach (object obj in gridGroup.Items)
								{
									if (obj == dataItem)
									{
										return;
									}
									if (this.OwnerTableView.PageSize >= num2)
									{
										break;
									}
									IEnumerable enumerable = gridGroup.Items.AsQueryable().Skip(num2 - 1).Take(1);
									object dataItem2 = pair.Value[pair.Value.Count - 1].DataItem;
									using (IEnumerator enumerator2 = enumerable.GetEnumerator())
									{
										if (enumerator2.MoveNext())
										{
											object obj2 = enumerator2.Current;
											if (obj2 != dataItem2)
											{
												groupSplitMode = GridGroupSplitMode.Both;
											}
										}
										break;
									}
								}
							}
						}
						if (groupSplitMode == GridGroupSplitMode.Continues)
						{
							this.SortOriginalGroupItems(gridGroup);
							if (gridGroup.Items.AsQueryable().ElementType == typeof(DataRowView) || gridGroup.Items.AsQueryable().ElementType == typeof(DataRow))
							{
								DataRowView dataRowView5 = gridGroup.Items.AsQueryable().Skip(num2 - 1).Take(1).Cast<DataRowView>().First<DataRowView>();
								DataRowView dataRowView6 = pair.Value[pair.Value.Count - 1].DataItem as DataRowView;
								if (dataRowView5 == dataRowView6)
								{
									return;
								}
							}
							else
							{
								IEnumerable enumerable2 = gridGroup.Items.AsQueryable().Skip(num2 - 1).Take(1);
								object dataItem3 = pair.Value[pair.Value.Count - 1].DataItem;
								using (IEnumerator enumerator3 = enumerable2.GetEnumerator())
								{
									if (enumerator3.MoveNext())
									{
										object obj3 = enumerator3.Current;
										if (obj3 == dataItem3)
										{
											return;
										}
									}
								}
							}
						}
						TableCell dataCell2 = pair.Key.DataCell;
						if (!dataCell2.Text.Contains(this.OwnerTableView.OwnerGrid.GroupingSettings.GroupContinuesFormatString))
						{
							string groupContinuesFormat2 = this.GetGroupContinuesFormat(groupSplitMode);
							string str2 = string.Format(this.OwnerTableView.OwnerGrid.GroupingSettings.GroupSplitFormat, string.Format(groupContinuesFormat2, pair.Value.Count, gridGroup.Items.AsQueryable().Count()));
							if (this.OwnerTableView.groupHeaderTemplate == null)
							{
								TableCell tableCell2 = dataCell2;
								tableCell2.Text += str2;
							}
						}
					}
				}
				catch (Exception)
				{
				}
			}
		}

		// Token: 0x06006F39 RID: 28473 RVA: 0x0019F1E0 File Offset: 0x0019D3E0
		private int GetItemsCountForMultyFieldsGroups(GridGroupByExpression expression, DataRowView drv, int count, GridGroup grGroup)
		{
			string text = "";
			IQueryable queryable = grGroup.Items.AsQueryable();
			IQueryable<DataRowView> queryable2 = null;
			for (int i = 1; i < expression.GroupByFields.Count; i++)
			{
				GridGroupByField grField = expression.GroupByFields[i];
				string fieldName = GridLinqGroupingHelper.GetFieldName(expression, drv, grField);
				if (queryable.ElementType == typeof(DataRowView) || queryable.ElementType == typeof(DataRow))
				{
					if (queryable2 == null)
					{
						if (queryable.ElementType == typeof(DataRow))
						{
							queryable2 = (from DataRow row in queryable
							select row.Table.DefaultView[row.Table.Rows.IndexOf(row)]).Cast<DataRowView>();
						}
						else
						{
							queryable2 = queryable.Cast<DataRowView>();
						}
					}
					queryable2 = from d in queryable2
					where d.Row[grField.FieldName].ToString() == drv[fieldName].ToString()
					select d;
				}
				string text2 = drv[fieldName].ToString();
				if (text2.Contains(" ") || drv[fieldName].GetType().FullName == "System.String" || drv[fieldName].GetType().FullName == "System.DateTime")
				{
					text2 = string.Format("\"{0}\"", text2);
				}
				text += string.Format("{0}={1} AND ", grField.FieldName, text2);
			}
			if (text.Length > 0)
			{
				text = text.Substring(0, text.Length - 5);
			}
			if (queryable.ElementType == typeof(DataRowView))
			{
				count = queryable2.Count();
			}
			else
			{
				if (queryable.ElementType.Name == "EntityDataSourceWrapper")
				{
					queryable = this.GetGenericEntityDataSourceElements(queryable);
				}
				else if (queryable.ElementType.Name == "Object")
				{
					queryable = this.GetGenericObjectElements(queryable);
				}
				count = queryable.Where(text, new object[0]).Count();
			}
			return count;
		}

		// Token: 0x06006F3A RID: 28474 RVA: 0x0019F68C File Offset: 0x0019D88C
		private GridGroup GetCorrespondingOriginalGroup(KeyValuePair<GridGroupHeaderItem, List<GridItem>> pair, GridGroupByExpression expression, DataRowView drv, GridGroupByField field)
		{
			string groupByFieldName = field.FieldName;
			if (!drv.DataView.Table.Columns.Contains(groupByFieldName))
			{
				int num = pair.Key.GroupLevel / 2;
				if (num >= expression.GroupByFields.Count)
				{
					num = expression.GroupByFields.Count - 1;
				}
				if (groupByFieldName == expression.GroupByFields[num].FieldName || drv.DataView.Table.Columns.Contains(expression.SelectFields[0].FieldAlias))
				{
					groupByFieldName = expression.SelectFields[num].FieldAlias;
				}
				else
				{
					groupByFieldName = expression.GroupByFields[num].FieldName;
				}
			}
			string fieldName = string.Empty;
			if (expression.GroupByFields.Count > pair.Key.GroupLevel)
			{
				fieldName = expression.GroupByFields[pair.Key.GroupLevel].FieldName;
			}
			if (fieldName != string.Empty && drv.DataView.Table.Columns.Contains(fieldName))
			{
				groupByFieldName = fieldName;
			}
			else if (string.IsNullOrEmpty(fieldName))
			{
				fieldName = groupByFieldName;
			}
			Type columnType = drv[groupByFieldName].GetType();
			List<GridGroup> list = (from g in this.OwnerTableView.groups
			where g.FieldName == fieldName && Convert.ChangeType(g.Key, Type.GetType(columnType.FullName)).ToString() == drv[groupByFieldName].ToString()
			select g).ToList<GridGroup>();
			if (list.Count == 0)
			{
				int index = pair.Key.GroupLevel / 2;
				groupByFieldName = expression.GroupByFields[index].FieldName;
				list = (from g in this.OwnerTableView.groups
				where g.Key.ToString() == drv[groupByFieldName].ToString()
				select g).ToList<GridGroup>();
			}
			int num2 = this.OwnerTableView.GetItems(new GridItemType[]
			{
				GridItemType.GroupHeader
			}).ToList<GridItem>().IndexOf(pair.Key);
			int i = 0;
			while (i < list.Count)
			{
				int num3 = list.Count - 1 - i;
				if (this.FoundSearchedGroup(list[num3].ParentGroup, num2 - 1, this.OwnerTableView.GetItems(new GridItemType[]
				{
					GridItemType.GroupHeader
				}).ToList<GridItem>(), groupByFieldName))
				{
					if (list[num3].Level != pair.Key.GroupLevel && num3 > 0)
					{
						return list[num3 - 1];
					}
					return list[num3];
				}
				else
				{
					i++;
				}
			}
			return null;
		}

		// Token: 0x06006F3B RID: 28475 RVA: 0x0019F9A4 File Offset: 0x0019DBA4
		private void SortOriginalGroupItems(GridGroup grGroup)
		{
			for (int i = this.OwnerTableView.GroupByExpressions.Count - 1; i >= 0; i--)
			{
				bool flag = false;
				string name = this.OwnerTableView.GroupByExpressions[i].GroupByFields[0].FieldName;
				if (this.OwnerTableView.GroupByExpressions[i].GroupByFields[0].SortOrder != GridSortOrder.Descending)
				{
					flag = true;
				}
				IQueryable queryable = grGroup.Items.AsQueryable();
				IQueryable queryable2 = queryable;
				if (queryable.ElementType == typeof(DataRow))
				{
					queryable2 = (from DataRow row in queryable
					select row.Table.DefaultView[row.Table.Rows.IndexOf(row)]).Cast<DataRowView>();
				}
				if (queryable2.ElementType == typeof(DataRowView))
				{
					if (flag)
					{
						grGroup.Items = from DataRowView d in queryable2
						orderby (d.Row[name] != DBNull.Value) ? d.Row[name] : null
						select d;
					}
					else
					{
						grGroup.Items = from DataRowView d in queryable2
						orderby (d.Row[name] != DBNull.Value) ? d.Row[name] : null descending
						select d;
					}
				}
				else if (queryable.ElementType.Name == "EntityDataSourceWrapper")
				{
					IQueryable genericEntityDataSourceElements = this.GetGenericEntityDataSourceElements(queryable);
					grGroup.Items = genericEntityDataSourceElements.OrderBy(name, new object[0]);
				}
				else if (queryable.ElementType.Name == "Object")
				{
					IQueryable genericObjectElements = this.GetGenericObjectElements(queryable);
					grGroup.Items = genericObjectElements.OrderBy(name, new object[0]);
				}
				else
				{
					grGroup.Items = queryable.OrderBy(name, new object[0]);
				}
			}
		}

		// Token: 0x06006F3C RID: 28476 RVA: 0x0019FDF4 File Offset: 0x0019DFF4
		private string GetGroupContinuesFormat(GridGroupSplitMode groupSplitMode)
		{
			string text = string.Empty;
			if (groupSplitMode == GridGroupSplitMode.Continued)
			{
				text = text + this.OwnerTableView.OwnerGrid.GroupingSettings.GroupContinuedFormatString + this.OwnerTableView.OwnerGrid.GroupingSettings.GroupSplitDisplayFormat;
			}
			else if (groupSplitMode == GridGroupSplitMode.Continues)
			{
				text = text + this.OwnerTableView.OwnerGrid.GroupingSettings.GroupSplitDisplayFormat + this.OwnerTableView.OwnerGrid.GroupingSettings.GroupContinuesFormatString;
			}
			else
			{
				text = text + this.OwnerTableView.OwnerGrid.GroupingSettings.GroupContinuedFormatString + this.OwnerTableView.OwnerGrid.GroupingSettings.GroupSplitDisplayFormat + this.OwnerTableView.OwnerGrid.GroupingSettings.GroupContinuesFormatString;
			}
			return text;
		}

		// Token: 0x06006F3D RID: 28477 RVA: 0x0019FEBC File Offset: 0x0019E0BC
		internal bool FoundSearchedGroup(GridGroup gridGroup, int curretnRowIndex, List<GridItem> groupHeaderItems, string fieldName)
		{
			if (curretnRowIndex < 0)
			{
				return true;
			}
			GridGroupHeaderItem gridGroupHeaderItem = groupHeaderItems[curretnRowIndex] as GridGroupHeaderItem;
			if (gridGroupHeaderItem == null)
			{
				return true;
			}
			if (gridGroup == null)
			{
				return true;
			}
			string fieldName2 = gridGroup.FieldName;
			DataRowView dataRowView = gridGroupHeaderItem.DataItem as DataRowView;
			if (!dataRowView.Row.Table.Columns.Contains(fieldName2))
			{
				dataRowView = ((groupHeaderItems[curretnRowIndex + 1] as GridGroupHeaderItem).DataItem as DataRowView);
			}
			return gridGroup.Key.ToString() == dataRowView[fieldName2].ToString() && this.FoundSearchedGroup(gridGroup.ParentGroup, curretnRowIndex - 1, groupHeaderItems, fieldName);
		}

		// Token: 0x06006F3E RID: 28478 RVA: 0x0019FF5C File Offset: 0x0019E15C
		internal List<GridItem> PopulateGroups(GridGroupHeaderItem headerItem, Dictionary<GridGroupHeaderItem, List<GridItem>> headersChildItems)
		{
			List<GridItem> list = new List<GridItem>();
			foreach (GridItem gridItem in headerItem.GetChildItems())
			{
				if (gridItem is GridGroupHeaderItem)
				{
					list.AddRange(this.PopulateGroups((GridGroupHeaderItem)gridItem, headersChildItems));
				}
				else if (gridItem is GridDataItem)
				{
					list.Add(gridItem);
				}
			}
			if (list.Count > 0)
			{
				if (headersChildItems.ContainsKey(headerItem))
				{
					headersChildItems[headerItem].AddRange(list);
				}
				else
				{
					headersChildItems.Add(headerItem, list);
				}
			}
			return list;
		}

		// Token: 0x04001DFD RID: 7677
		private Type itemsType;

		// Token: 0x04001DFE RID: 7678
		private IQueryable<DataRowView> castedCollection;
	}
}
