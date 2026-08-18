using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TechnoPro.Common.Public.Adapters;

namespace TechnoPro.Common.Core.DataSync
{
	// Token: 0x0200010E RID: 270
	internal static class DataSyncCourseManagerAdapter
	{
		// Token: 0x06000B0F RID: 2831 RVA: 0x00048874 File Offset: 0x00046A74
		public static IList<T> MakeItemAList<T>(this T item) where T : class
		{
			List<T> result;
			if (item != null)
			{
				(result = new List<T>()).Add(item);
			}
			else
			{
				result = new List<T>();
			}
			return result;
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x000488A4 File Offset: 0x00046AA4
		public static double GetSpecialColumnDouble(this ESpecialColumnType specialColType, DataColumnCollection columns, DataRow dr)
		{
			SpecialColumnTypeAttribute attribute = specialColType.GetAttribute<SpecialColumnTypeAttribute>();
			string[] allowedExternalColumnNames = attribute.AllowedExternalColumnNames;
			string text = (allowedExternalColumnNames != null) ? allowedExternalColumnNames.FirstOrDefault(new Func<string, bool>(columns.Contains)) : null;
			bool flag = text == null;
			double result;
			if (flag)
			{
				result = 0.0;
			}
			else
			{
				object obj = dr[text];
				bool flag2 = obj is double;
				if (flag2)
				{
					result = (double)obj;
				}
				else
				{
					double num;
					result = ((!double.TryParse(obj.ToString(), out num)) ? 0.0 : num);
				}
			}
			return result;
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x00048938 File Offset: 0x00046B38
		public static decimal GetSpecialColumnValueDecimal(this ESpecialColumnType specialColType, DataColumnCollection columns, DataRow dr)
		{
			SpecialColumnTypeAttribute attribute = specialColType.GetAttribute<SpecialColumnTypeAttribute>();
			string[] allowedExternalColumnNames = attribute.AllowedExternalColumnNames;
			string text = (allowedExternalColumnNames != null) ? allowedExternalColumnNames.FirstOrDefault(new Func<string, bool>(columns.Contains)) : null;
			bool flag = text == null;
			decimal result;
			if (flag)
			{
				result = 0m;
			}
			else
			{
				object obj = dr[text];
				bool flag2 = obj is decimal;
				if (flag2)
				{
					result = (decimal)obj;
				}
				else
				{
					decimal num;
					result = ((!decimal.TryParse(obj.ToString(), out num)) ? 0m : num);
				}
			}
			return result;
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x000489C4 File Offset: 0x00046BC4
		public static string GetSpecialColumnValue(this ESpecialColumnType specialColType, DataColumnCollection columns, DataRow dr)
		{
			SpecialColumnTypeAttribute attribute = specialColType.GetAttribute<SpecialColumnTypeAttribute>();
			string[] allowedExternalColumnNames = attribute.AllowedExternalColumnNames;
			string text = (allowedExternalColumnNames != null) ? allowedExternalColumnNames.FirstOrDefault(new Func<string, bool>(columns.Contains)) : null;
			return (text == null) ? string.Empty : dr[text].ToString().Trim();
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x00048A1C File Offset: 0x00046C1C
		public static bool DoAllSpecialColumnsExist(this DataColumnCollection columns, params ESpecialColumnType[] specialColTypes)
		{
			return (from specialColType in specialColTypes
			select specialColType.GetAttribute<SpecialColumnTypeAttribute>()).All((SpecialColumnTypeAttribute attr) => attr.AllowedExternalColumnNames != null && attr.AllowedExternalColumnNames.Any(new Func<string, bool>(columns.Contains)));
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x00048A74 File Offset: 0x00046C74
		public static DateTime? GetSpecialColumnValueDateTime(this ESpecialColumnType specialColType, DataColumnCollection columns, DataRow dr)
		{
			SpecialColumnTypeAttribute attribute = specialColType.GetAttribute<SpecialColumnTypeAttribute>();
			string[] allowedExternalColumnNames = attribute.AllowedExternalColumnNames;
			string text = (allowedExternalColumnNames != null) ? allowedExternalColumnNames.FirstOrDefault(new Func<string, bool>(columns.Contains)) : null;
			bool flag = text == null;
			DateTime? result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string s = dr[text].ToString().Trim();
				DateTime value;
				result = ((!DateTime.TryParse(s, out value)) ? null : new DateTime?(value));
			}
			return result;
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x00048AFC File Offset: 0x00046CFC
		public static TimeSpan? GetSpecialColumnValueTime(this ESpecialColumnType specialColType, DataColumnCollection columns, DataRow dr)
		{
			SpecialColumnTypeAttribute attribute = specialColType.GetAttribute<SpecialColumnTypeAttribute>();
			string[] allowedExternalColumnNames = attribute.AllowedExternalColumnNames;
			string text = (allowedExternalColumnNames != null) ? allowedExternalColumnNames.FirstOrDefault(new Func<string, bool>(columns.Contains)) : null;
			bool flag = text == null;
			TimeSpan? result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string text2 = dr[text].ToString().Trim();
				bool flag2 = text2.Length < 1;
				if (flag2)
				{
					result = null;
				}
				else
				{
					string str = DateTime.Now.ToString("yyyy-MM-dd");
					text2 = str + " " + text2;
					DateTime dateTime;
					result = ((!DateTime.TryParse(text2, out dateTime)) ? null : new TimeSpan?(dateTime.TimeOfDay));
				}
			}
			return result;
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x00048BC8 File Offset: 0x00046DC8
		public static int GetSpecialColumnValueInt(this ESpecialColumnType specialColType, DataColumnCollection columns, DataRow dr, int defaultValue = 0)
		{
			SpecialColumnTypeAttribute attribute = specialColType.GetAttribute<SpecialColumnTypeAttribute>();
			string[] allowedExternalColumnNames = attribute.AllowedExternalColumnNames;
			string text = (allowedExternalColumnNames != null) ? allowedExternalColumnNames.FirstOrDefault(new Func<string, bool>(columns.Contains)) : null;
			bool flag = text == null;
			int result;
			if (flag)
			{
				result = defaultValue;
			}
			else
			{
				string s = dr[text].ToString().Trim();
				int num;
				result = ((!int.TryParse(s, out num)) ? defaultValue : num);
			}
			return result;
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x00048C38 File Offset: 0x00046E38
		public static bool GetSpecialColumnValueBool(this ESpecialColumnType specialColType, DataColumnCollection columns, DataRow dr)
		{
			SpecialColumnTypeAttribute attribute = specialColType.GetAttribute<SpecialColumnTypeAttribute>();
			string[] allowedExternalColumnNames = attribute.AllowedExternalColumnNames;
			string text = (allowedExternalColumnNames != null) ? allowedExternalColumnNames.FirstOrDefault(new Func<string, bool>(columns.Contains)) : null;
			bool flag = text == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = columns[text].DataType == typeof(bool);
				if (flag2)
				{
					result = (!(dr[text] is DBNull) && (bool)dr[text]);
				}
				else
				{
					string value = dr[text].ToString().Trim().ToLower();
					result = ("1yestrue".IndexOf(value) >= 0);
				}
			}
			return result;
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x00048CF0 File Offset: 0x00046EF0
		public static void TrimSpacesAndEnsureNotReadOnlyForEveryCell(this DataTable t)
		{
			bool flag = t == null;
			if (!flag)
			{
				List<DataColumn> list = (from DataColumn dc in t.Columns
				where dc.DataType == typeof(string)
				select dc).ToList<DataColumn>();
				foreach (DataColumn dataColumn in list)
				{
					dataColumn.ReadOnly = false;
				}
				using (IEnumerator enumerator2 = t.Rows.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						DataRow dr = (DataRow)enumerator2.Current;
						IEnumerable<string> source = from dc in list
						select dc.ColumnName;
						Func<string, bool> predicate;
						Func<string, bool> <>9__2;
						if ((predicate = <>9__2) == null)
						{
							predicate = (<>9__2 = ((string cname) => !(dr[cname] is DBNull)));
						}
						foreach (string columnName in source.Where(predicate))
						{
							dr[columnName] = dr[columnName].ToString().Trim();
						}
					}
				}
			}
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x00048E90 File Offset: 0x00047090
		public static int CompareStringsIgnoreCase(this string s1, string s2)
		{
			return (s1 ?? "").Trim().ToLower().CompareTo((s2 ?? "").Trim().ToLower());
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x00048ED0 File Offset: 0x000470D0
		public static bool TryParseDayOfWeek(this string dayOfWeekStr, out DayOfWeek dow)
		{
			string s = (dayOfWeekStr ?? "").Trim().ToLower();
			List<DayOfWeekAttribute> source = (from g in (EDayOfWeek[])Enum.GetValues(typeof(EDayOfWeek))
			select g.GetAttribute<DayOfWeekAttribute>()).ToList<DayOfWeekAttribute>();
			Func<string, bool> <>9__2;
			DayOfWeekAttribute dayOfWeekAttribute = source.FirstOrDefault(delegate(DayOfWeekAttribute g)
			{
				IEnumerable<string> titlesLowerCase = g.TitlesLowerCase;
				Func<string, bool> predicate;
				if ((predicate = <>9__2) == null)
				{
					predicate = (<>9__2 = ((string h) => h == s));
				}
				return titlesLowerCase.Any(predicate);
			});
			dow = ((dayOfWeekAttribute != null) ? dayOfWeekAttribute.DayOfWeek : DayOfWeek.Sunday);
			return dayOfWeekAttribute != null;
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x00048F68 File Offset: 0x00047168
		public static bool AreStringsEqual(this string s1, string s2)
		{
			bool flag = s1 == null && s2 == null;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = s1 == null;
				if (flag2)
				{
					result = (s2.Trim().Length < 1);
				}
				else
				{
					bool flag3 = s2 == null;
					if (flag3)
					{
						result = (s1.Trim().Length < 1);
					}
					else
					{
						result = s1.Trim().Equals(s2.Trim(), StringComparison.OrdinalIgnoreCase);
					}
				}
			}
			return result;
		}
	}
}
