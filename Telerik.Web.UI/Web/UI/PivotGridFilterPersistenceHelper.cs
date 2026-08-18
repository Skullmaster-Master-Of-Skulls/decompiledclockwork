using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Telerik.Web.UI.PivotGrid.Core;

namespace Telerik.Web.UI
{
	// Token: 0x0200076F RID: 1903
	internal static class PivotGridFilterPersistenceHelper
	{
		// Token: 0x0600432A RID: 17194 RVA: 0x000D1E4C File Offset: 0x000D004C
		public static string SerializeObject(object objectToSerialize)
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			MemoryStream memoryStream = new MemoryStream();
			using (memoryStream)
			{
				binaryFormatter.Serialize(memoryStream, objectToSerialize);
			}
			return Convert.ToBase64String(memoryStream.ToArray());
		}

		// Token: 0x0600432B RID: 17195 RVA: 0x000D1E98 File Offset: 0x000D0098
		public static object DeserializeObject(string objectToDeserialize)
		{
			byte[] buffer = Convert.FromBase64String(objectToDeserialize);
			new BinaryFormatter();
			object result;
			using (MemoryStream memoryStream = new MemoryStream(buffer))
			{
				result = new BinaryFormatter().Deserialize(memoryStream);
			}
			return result;
		}

		// Token: 0x0600432C RID: 17196 RVA: 0x000D1EE4 File Offset: 0x000D00E4
		public static string SerializePivotFilter(PivotGridFilter filter)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("{0};{1};", filter.GetType().Name, filter.FieldName);
			IPivotConditionFilter pivotConditionFilter = filter as IPivotConditionFilter;
			PivotGridSortedGroupsFilter pivotGridSortedGroupsFilter;
			if (pivotConditionFilter != null)
			{
				stringBuilder.AppendFormat("{0};", PivotGridFilterPersistenceHelper.SerializeObject(pivotConditionFilter.Condition));
				PivotGridValueGroupFilter pivotGridValueGroupFilter = filter as PivotGridValueGroupFilter;
				PivotGridOlapValueGroupFilter pivotGridOlapValueGroupFilter;
				if (pivotGridValueGroupFilter != null)
				{
					stringBuilder.AppendFormat("{0};", pivotGridValueGroupFilter.AggregateIndex);
				}
				else if ((pivotGridOlapValueGroupFilter = (filter as PivotGridOlapValueGroupFilter)) != null)
				{
					stringBuilder.AppendFormat("{0};", pivotGridOlapValueGroupFilter.AggregateIndex);
				}
			}
			else if ((pivotGridSortedGroupsFilter = (filter as PivotGridSortedGroupsFilter)) != null)
			{
				stringBuilder.AppendFormat("{0};", pivotGridSortedGroupsFilter.AggregateIndex);
				stringBuilder.AppendFormat("{0};", pivotGridSortedGroupsFilter.Selection.ToString());
				PivotGridGroupsCountFilter pivotGridGroupsCountFilter = filter as PivotGridGroupsCountFilter;
				PivotGridGroupsPercentFilter pivotGridGroupsPercentFilter;
				PivotGridGroupsSumFilter pivotGridGroupsSumFilter;
				if (pivotGridGroupsCountFilter != null)
				{
					stringBuilder.AppendFormat("{0};", pivotGridGroupsCountFilter.Count);
				}
				else if ((pivotGridGroupsPercentFilter = (filter as PivotGridGroupsPercentFilter)) != null)
				{
					stringBuilder.AppendFormat("{0};", pivotGridGroupsPercentFilter.Percent);
				}
				else if ((pivotGridGroupsSumFilter = (filter as PivotGridGroupsSumFilter)) != null)
				{
					stringBuilder.AppendFormat("{0};", pivotGridGroupsSumFilter.Sum);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600432D RID: 17197 RVA: 0x000D203C File Offset: 0x000D023C
		public static PivotGridFilter DeserializePivotFilter(string serializedFilter)
		{
			string[] array = serializedFilter.Split(new char[]
			{
				';'
			}, StringSplitOptions.RemoveEmptyEntries);
			string a = array[0];
			PivotGridFilter pivotGridFilter = null;
			if (a == "PivotGridReportFilter")
			{
				pivotGridFilter = new PivotGridReportFilter();
				PivotGridReportFilter pivotGridReportFilter = pivotGridFilter as PivotGridReportFilter;
				pivotGridReportFilter.FieldName = array[1];
				pivotGridReportFilter.Condition = (IFilterCondition)PivotGridFilterPersistenceHelper.DeserializeObject(array[2]);
			}
			else if (a == "PivotGridGroupsCountFilter")
			{
				pivotGridFilter = new PivotGridGroupsCountFilter();
				PivotGridGroupsCountFilter pivotGridGroupsCountFilter = pivotGridFilter as PivotGridGroupsCountFilter;
				pivotGridGroupsCountFilter.FieldName = array[1];
				pivotGridGroupsCountFilter.AggregateIndex = int.Parse(array[2]);
				pivotGridGroupsCountFilter.Selection = (SortedListSelection)Enum.Parse(typeof(SortedListSelection), array[3]);
				pivotGridGroupsCountFilter.Count = int.Parse(array[4]);
			}
			else if (a == "PivotGridGroupsPercentFilter")
			{
				pivotGridFilter = new PivotGridGroupsPercentFilter();
				PivotGridGroupsPercentFilter pivotGridGroupsPercentFilter = pivotGridFilter as PivotGridGroupsPercentFilter;
				pivotGridGroupsPercentFilter.FieldName = array[1];
				pivotGridGroupsPercentFilter.AggregateIndex = int.Parse(array[2]);
				pivotGridGroupsPercentFilter.Selection = (SortedListSelection)Enum.Parse(typeof(SortedListSelection), array[3]);
				pivotGridGroupsPercentFilter.Percent = double.Parse(array[4]);
			}
			else if (a == "PivotGridGroupsSumFilter")
			{
				pivotGridFilter = new PivotGridGroupsSumFilter();
				PivotGridGroupsSumFilter pivotGridGroupsSumFilter = pivotGridFilter as PivotGridGroupsSumFilter;
				pivotGridGroupsSumFilter.FieldName = array[1];
				pivotGridGroupsSumFilter.AggregateIndex = int.Parse(array[2]);
				pivotGridGroupsSumFilter.Selection = (SortedListSelection)Enum.Parse(typeof(SortedListSelection), array[3]);
				pivotGridGroupsSumFilter.Sum = double.Parse(array[4]);
			}
			else if (a == "PivotGridLabelGroupFilter")
			{
				pivotGridFilter = new PivotGridLabelGroupFilter();
				PivotGridLabelGroupFilter pivotGridLabelGroupFilter = pivotGridFilter as PivotGridLabelGroupFilter;
				pivotGridLabelGroupFilter.FieldName = array[1];
				pivotGridLabelGroupFilter.Condition = (IFilterCondition)PivotGridFilterPersistenceHelper.DeserializeObject(array[2]);
			}
			else if (a == "PivotGridValueGroupFilter")
			{
				pivotGridFilter = new PivotGridValueGroupFilter();
				PivotGridValueGroupFilter pivotGridValueGroupFilter = pivotGridFilter as PivotGridValueGroupFilter;
				pivotGridValueGroupFilter.FieldName = array[1];
				pivotGridValueGroupFilter.Condition = (IFilterCondition)PivotGridFilterPersistenceHelper.DeserializeObject(array[2]);
				pivotGridValueGroupFilter.AggregateIndex = int.Parse(array[3]);
			}
			else if (a == "PivotGridOlapLabelGroupFilter")
			{
				pivotGridFilter = new PivotGridOlapLabelGroupFilter();
				PivotGridOlapLabelGroupFilter pivotGridOlapLabelGroupFilter = pivotGridFilter as PivotGridOlapLabelGroupFilter;
				pivotGridOlapLabelGroupFilter.FieldName = array[1];
				pivotGridOlapLabelGroupFilter.Condition = (IFilterCondition)PivotGridFilterPersistenceHelper.DeserializeObject(array[2]);
			}
			else if (a == "PivotGridOlapValueGroupFilter")
			{
				pivotGridFilter = new PivotGridOlapValueGroupFilter();
				PivotGridOlapValueGroupFilter pivotGridOlapValueGroupFilter = pivotGridFilter as PivotGridOlapValueGroupFilter;
				pivotGridOlapValueGroupFilter.FieldName = array[1];
				pivotGridOlapValueGroupFilter.Condition = (IFilterCondition)PivotGridFilterPersistenceHelper.DeserializeObject(array[2]);
				pivotGridOlapValueGroupFilter.AggregateIndex = int.Parse(array[3]);
			}
			return pivotGridFilter;
		}
	}
}
