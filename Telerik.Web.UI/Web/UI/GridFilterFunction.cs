using System;
using System.Collections;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Telerik.Web.UI
{
	// Token: 0x02001101 RID: 4353
	public class GridFilterFunction
	{
		// Token: 0x170039B2 RID: 14770
		// (get) Token: 0x0600B218 RID: 45592 RVA: 0x0026AF64 File Offset: 0x00269164
		public GridKnownFunction CurrentKnownFunction
		{
			get
			{
				return this._currentKnownFunction;
			}
		}

		// Token: 0x0600B219 RID: 45593 RVA: 0x0026AF6C File Offset: 0x0026916C
		public GridFilterFunction(GridKnownFunction function)
		{
			this._currentKnownFunction = function;
		}

		// Token: 0x0600B21A RID: 45594 RVA: 0x0026AF8E File Offset: 0x0026918E
		public GridFilterFunction(string customFunction)
		{
			if (customFunction != null)
			{
				this._customFunction = customFunction;
			}
		}

		// Token: 0x170039B3 RID: 14771
		// (get) Token: 0x0600B21B RID: 45595 RVA: 0x0026AFB4 File Offset: 0x002691B4
		private Hashtable KnownFunctionHash
		{
			get
			{
				if (this._knownFunctionHash == null)
				{
					this._knownFunctionHash = new Hashtable();
					if (this.tableView.IsOpenAccessDataSourceView())
					{
						this.ApplyOqlFilters();
						return this._knownFunctionHash;
					}
					if ((this.tableView.IsBoundToForwardOnly || (!this.tableView.OwnerGrid.EnableLinqExpressions && !this.tableView.IsDataSourceViewWithFiltering())) && !this.tableView.IsPageDataSourceView())
					{
						this.ApplySqlFilters();
						return this._knownFunctionHash;
					}
					Type queryableElementType = this.tableView.QueryableElementType;
					bool flag = false;
					bool flag2 = false;
					bool flag3 = this.tableView.HasCalculatedColumns();
					if ((queryableElementType == typeof(DataRowView) || queryableElementType == typeof(DataRow) || queryableElementType.GetInterface("IDataRecord") != null) && !flag3)
					{
						flag = true;
						if (this.tableView.IsEntityDataSourceView() && this.tableView.OwnerGrid.EnableLinqExpressions)
						{
							flag = false;
						}
					}
					if (GridBaseDataList.IsBindableType(queryableElementType) && !flag3)
					{
						flag2 = true;
					}
					if (flag)
					{
						this._knownFunctionHash[GridKnownFunction.Contains] = new GridFilterFunction.StringFunctionEntry("it[\"{0}\"].ToString().Contains(\"{1}\")", false, 2);
						this._knownFunctionHash[GridKnownFunction.DoesNotContain] = new GridFilterFunction.StringFunctionEntry("!it[\"{0}\"].ToString().Contains(\"{1}\")", false, 2);
						this._knownFunctionHash[GridKnownFunction.StartsWith] = new GridFilterFunction.StringFunctionEntry("it[\"{0}\"].ToString().StartsWith(\"{1}\")", false, 2);
						this._knownFunctionHash[GridKnownFunction.EndsWith] = new GridFilterFunction.StringFunctionEntry("it[\"{0}\"].ToString().EndsWith(\"{1}\")", false, 2);
						this._knownFunctionHash[GridKnownFunction.EqualTo] = new GridFilterFunction.FunctionEntry("{1}(it[\"{0}\"]){3} = {2}", true, 4);
						this._knownFunctionHash[GridKnownFunction.NotEqualTo] = new GridFilterFunction.FunctionEntry("{1}(it[\"{0}\"]){3} <> {2}", true, 4);
						this._knownFunctionHash[GridKnownFunction.GreaterThan] = new GridFilterFunction.FunctionEntry("{1}(it[\"{0}\"]){3} > {2}", true, 4);
						this._knownFunctionHash[GridKnownFunction.LessThan] = new GridFilterFunction.FunctionEntry("{1}(it[\"{0}\"]){3} < {2}", true, 4);
						this._knownFunctionHash[GridKnownFunction.GreaterThanOrEqualTo] = new GridFilterFunction.FunctionEntry("{1}(it[\"{0}\"]){3} >= {2}", true, 4);
						this._knownFunctionHash[GridKnownFunction.LessThanOrEqualTo] = new GridFilterFunction.FunctionEntry("{1}(it[\"{0}\"]){3} <= {2}", true, 4);
						this._knownFunctionHash[GridKnownFunction.Between] = new GridFilterFunction.FunctionEntry("({1}(it[\"{0}\"]){4} >= {2}) AND ( {1}(it[\"{0}\"]){4} <= {3})", true, 5);
						this._knownFunctionHash[GridKnownFunction.NotBetween] = new GridFilterFunction.FunctionEntry("({1}(it[\"{0}\"]){4}  < {2}) OR ( {1}(it[\"{0}\"]){4} > {3})", true, 5);
						this._knownFunctionHash[GridKnownFunction.IsEmpty] = new GridFilterFunction.FunctionEntry("it[\"{0}\"] = \"\"", false, 1);
						this._knownFunctionHash[GridKnownFunction.NotIsEmpty] = new GridFilterFunction.FunctionEntry("it[\"{0}\"] <> \"\"", false, 1);
						this._knownFunctionHash[GridKnownFunction.IsNull] = new GridFilterFunction.FunctionEntry("it[\"{0}\"] == Convert.DBNull", false, 1);
						this._knownFunctionHash[GridKnownFunction.NotIsNull] = new GridFilterFunction.FunctionEntry("(it[\"{0}\"] != Convert.DBNull)", false, 1);
					}
					else if (flag2)
					{
						this._knownFunctionHash[GridKnownFunction.Contains] = new GridFilterFunction.StringFunctionEntry("it.ToString().Contains(\"{1}\")", false, 2);
						this._knownFunctionHash[GridKnownFunction.DoesNotContain] = new GridFilterFunction.StringFunctionEntry("!it.ToString().Contains(\"{1}\")", false, 2);
						this._knownFunctionHash[GridKnownFunction.StartsWith] = new GridFilterFunction.StringFunctionEntry("it.ToString().StartsWith(\"{1}\")", false, 2);
						this._knownFunctionHash[GridKnownFunction.EndsWith] = new GridFilterFunction.StringFunctionEntry("it.ToString().EndsWith(\"{1}\")", false, 2);
						this._knownFunctionHash[GridKnownFunction.EqualTo] = new GridFilterFunction.FunctionEntry("it = {1}", true, 2);
						this._knownFunctionHash[GridKnownFunction.NotEqualTo] = new GridFilterFunction.FunctionEntry("it <> {1}", true, 2);
						this._knownFunctionHash[GridKnownFunction.GreaterThan] = new GridFilterFunction.FunctionEntry("it > {1}", true, 2);
						this._knownFunctionHash[GridKnownFunction.LessThan] = new GridFilterFunction.FunctionEntry("it < {1}", true, 2);
						this._knownFunctionHash[GridKnownFunction.GreaterThanOrEqualTo] = new GridFilterFunction.FunctionEntry("it >= {1}", true, 2);
						this._knownFunctionHash[GridKnownFunction.LessThanOrEqualTo] = new GridFilterFunction.FunctionEntry("it <= {1}", true, 2);
						this._knownFunctionHash[GridKnownFunction.Between] = new GridFilterFunction.FunctionEntry("(it >= {1}) AND (it <= {2})", true, 3);
						this._knownFunctionHash[GridKnownFunction.NotBetween] = new GridFilterFunction.FunctionEntry("(it < {1}) OR (it > {2})", true, 3);
						this._knownFunctionHash[GridKnownFunction.IsEmpty] = new GridFilterFunction.FunctionEntry("it = \"\"", false, 1);
						this._knownFunctionHash[GridKnownFunction.NotIsEmpty] = new GridFilterFunction.FunctionEntry("it <> \"\"", false, 1);
						this._knownFunctionHash[GridKnownFunction.IsNull] = new GridFilterFunction.FunctionEntry("it == null", false, 1);
						this._knownFunctionHash[GridKnownFunction.NotIsNull] = new GridFilterFunction.FunctionEntry("(it != null)", false, 1);
					}
					else if (this.tableView.IsEntityDataSourceView())
					{
						this._knownFunctionHash[GridKnownFunction.Contains] = new GridFilterFunction.StringFunctionEntry("it.{0} LIKE \"%{1}%\"", false, 2);
						this._knownFunctionHash[GridKnownFunction.DoesNotContain] = new GridFilterFunction.StringFunctionEntry("it.{0} NOT LIKE \"%{1}%\"", false, 2);
						this._knownFunctionHash[GridKnownFunction.StartsWith] = new GridFilterFunction.StringFunctionEntry("it.{0} LIKE \"{1}%\"", false, 2);
						this._knownFunctionHash[GridKnownFunction.EndsWith] = new GridFilterFunction.StringFunctionEntry("it.{0} LIKE \"%{1}\"", false, 2);
						this._knownFunctionHash[GridKnownFunction.EqualTo] = new GridFilterFunction.FunctionEntry("it.{0} = {1}", true, 2);
						this._knownFunctionHash[GridKnownFunction.NotEqualTo] = new GridFilterFunction.FunctionEntry("it.{0} <> {1}", true, 2);
						this._knownFunctionHash[GridKnownFunction.GreaterThan] = new GridFilterFunction.FunctionEntry("it.{0} > {1}", true, 2);
						this._knownFunctionHash[GridKnownFunction.LessThan] = new GridFilterFunction.FunctionEntry("it.{0} < {1}", true, 2);
						this._knownFunctionHash[GridKnownFunction.GreaterThanOrEqualTo] = new GridFilterFunction.FunctionEntry("it.{0} >= {1}", true, 2);
						this._knownFunctionHash[GridKnownFunction.LessThanOrEqualTo] = new GridFilterFunction.FunctionEntry("it.{0} <= {1}", true, 2);
						this._knownFunctionHash[GridKnownFunction.Between] = new GridFilterFunction.FunctionEntry("(it.{0} >= {1}) AND (it.{0} <= {2})", true, 3);
						this._knownFunctionHash[GridKnownFunction.NotBetween] = new GridFilterFunction.FunctionEntry("(it.{0} < {1}) OR (it.{0} > {2})", true, 3);
						this._knownFunctionHash[GridKnownFunction.IsEmpty] = new GridFilterFunction.FunctionEntry("it.{0} = \"\"", true, 1);
						this._knownFunctionHash[GridKnownFunction.NotIsEmpty] = new GridFilterFunction.FunctionEntry("it.{0} <> \"\"", true, 1);
						this._knownFunctionHash[GridKnownFunction.IsNull] = new GridFilterFunction.FunctionEntry("it.{0} IS null", true, 1);
						this._knownFunctionHash[GridKnownFunction.NotIsNull] = new GridFilterFunction.FunctionEntry("NOT (it.{0} IS null)", true, 1);
					}
					else if (flag3)
					{
						this._knownFunctionHash[GridKnownFunction.Contains] = new GridFilterFunction.StringFunctionEntry("{0}.ToString().Contains(\"{1}\")", false, 2);
						this._knownFunctionHash[GridKnownFunction.DoesNotContain] = new GridFilterFunction.StringFunctionEntry("!{0}.ToString().Contains(\"{1}\")", false, 2);
						this._knownFunctionHash[GridKnownFunction.StartsWith] = new GridFilterFunction.StringFunctionEntry("{0}.ToString().StartsWith(\"{1}\")", false, 2);
						this._knownFunctionHash[GridKnownFunction.EndsWith] = new GridFilterFunction.StringFunctionEntry("{0}.ToString().EndsWith(\"{1}\")", false, 2);
						this._knownFunctionHash[GridKnownFunction.EqualTo] = new GridFilterFunction.FunctionEntry("{1}({0}){3} = {2}", true, 4);
						this._knownFunctionHash[GridKnownFunction.NotEqualTo] = new GridFilterFunction.FunctionEntry("{1}({0}){3} <> {2}", true, 4);
						this._knownFunctionHash[GridKnownFunction.GreaterThan] = new GridFilterFunction.FunctionEntry("{1}({0}){3} > {2}", true, 4);
						this._knownFunctionHash[GridKnownFunction.LessThan] = new GridFilterFunction.FunctionEntry("{1}({0}){3} < {2}", true, 4);
						this._knownFunctionHash[GridKnownFunction.GreaterThanOrEqualTo] = new GridFilterFunction.FunctionEntry("{1}({0}){3} >= {2}", true, 4);
						this._knownFunctionHash[GridKnownFunction.LessThanOrEqualTo] = new GridFilterFunction.FunctionEntry("{1}({0}){3} <= {2}", true, 4);
						this._knownFunctionHash[GridKnownFunction.Between] = new GridFilterFunction.FunctionEntry("({3}({0}) >= {1}) AND ({3}({0}) <= {2})", true, 4);
						this._knownFunctionHash[GridKnownFunction.NotBetween] = new GridFilterFunction.FunctionEntry("({3}({0}) < {1}) OR ({3}({0}) > {2})", true, 4);
						this._knownFunctionHash[GridKnownFunction.IsEmpty] = new GridFilterFunction.FunctionEntry("{0} = \"\"", false, 1);
						this._knownFunctionHash[GridKnownFunction.NotIsEmpty] = new GridFilterFunction.FunctionEntry("{0} <> \"\"", false, 1);
						this._knownFunctionHash[GridKnownFunction.IsNull] = new GridFilterFunction.FunctionEntry("{0} == null", false, 1);
						this._knownFunctionHash[GridKnownFunction.NotIsNull] = new GridFilterFunction.FunctionEntry("({0} != null)", false, 1);
					}
					else
					{
						this._knownFunctionHash[GridKnownFunction.Contains] = new GridFilterFunction.StringFunctionEntry("{0}.ToString().Contains(\"{1}\")", false, 2);
						this._knownFunctionHash[GridKnownFunction.DoesNotContain] = new GridFilterFunction.StringFunctionEntry("!{0}.ToString().Contains(\"{1}\")", false, 2);
						this._knownFunctionHash[GridKnownFunction.StartsWith] = new GridFilterFunction.StringFunctionEntry("{0}.ToString().StartsWith(\"{1}\")", false, 2);
						this._knownFunctionHash[GridKnownFunction.EndsWith] = new GridFilterFunction.StringFunctionEntry("{0}.ToString().EndsWith(\"{1}\")", false, 2);
						this._knownFunctionHash[GridKnownFunction.EqualTo] = new GridFilterFunction.FunctionEntry("{0} = {1}", true, 2);
						this._knownFunctionHash[GridKnownFunction.NotEqualTo] = new GridFilterFunction.FunctionEntry("{0} <> {1}", true, 2);
						this._knownFunctionHash[GridKnownFunction.GreaterThan] = new GridFilterFunction.FunctionEntry("{0} > {1}", true, 2);
						this._knownFunctionHash[GridKnownFunction.LessThan] = new GridFilterFunction.FunctionEntry("{0} < {1}", true, 2);
						this._knownFunctionHash[GridKnownFunction.GreaterThanOrEqualTo] = new GridFilterFunction.FunctionEntry("{0} >= {1}", true, 2);
						this._knownFunctionHash[GridKnownFunction.LessThanOrEqualTo] = new GridFilterFunction.FunctionEntry("{0} <= {1}", true, 2);
						this._knownFunctionHash[GridKnownFunction.Between] = new GridFilterFunction.FunctionEntry("({0} >= {1}) AND ({0} <= {2})", true, 3);
						this._knownFunctionHash[GridKnownFunction.NotBetween] = new GridFilterFunction.FunctionEntry("({0} < {1}) OR ({0} > {2})", true, 3);
						this._knownFunctionHash[GridKnownFunction.IsEmpty] = new GridFilterFunction.FunctionEntry("{0} = \"\"", false, 1);
						this._knownFunctionHash[GridKnownFunction.NotIsEmpty] = new GridFilterFunction.FunctionEntry("{0} <> \"\"", false, 1);
						this._knownFunctionHash[GridKnownFunction.IsNull] = new GridFilterFunction.FunctionEntry("{0} == null", false, 1);
						this._knownFunctionHash[GridKnownFunction.NotIsNull] = new GridFilterFunction.FunctionEntry("({0} != null)", false, 1);
					}
				}
				return this._knownFunctionHash;
			}
		}

		// Token: 0x0600B21C RID: 45596 RVA: 0x0026BA34 File Offset: 0x00269C34
		private void ApplySqlFilters()
		{
			this._knownFunctionHash[GridKnownFunction.Contains] = new GridFilterFunction.StringFunctionEntry("[{0}] LIKE '%{1}%'", false, 2);
			this._knownFunctionHash[GridKnownFunction.DoesNotContain] = new GridFilterFunction.StringFunctionEntry("[{0}] NOT LIKE '%{1}%'", false, 2);
			this._knownFunctionHash[GridKnownFunction.StartsWith] = new GridFilterFunction.StringFunctionEntry("[{0}] LIKE '{1}%'", false, 2);
			this._knownFunctionHash[GridKnownFunction.EndsWith] = new GridFilterFunction.StringFunctionEntry("[{0}] LIKE '%{1}'", false, 2);
			this._knownFunctionHash[GridKnownFunction.EqualTo] = new GridFilterFunction.FunctionEntry("[{0}] = {1}", true, 2);
			this._knownFunctionHash[GridKnownFunction.NotEqualTo] = new GridFilterFunction.FunctionEntry("[{0}] <> {1}", true, 2);
			this._knownFunctionHash[GridKnownFunction.GreaterThan] = new GridFilterFunction.FunctionEntry("[{0}] > {1}", true, 2);
			this._knownFunctionHash[GridKnownFunction.LessThan] = new GridFilterFunction.FunctionEntry("[{0}] < {1}", true, 2);
			this._knownFunctionHash[GridKnownFunction.GreaterThanOrEqualTo] = new GridFilterFunction.FunctionEntry("[{0}] >= {1}", true, 2);
			this._knownFunctionHash[GridKnownFunction.LessThanOrEqualTo] = new GridFilterFunction.FunctionEntry("[{0}] <= {1}", true, 2);
			this._knownFunctionHash[GridKnownFunction.Between] = new GridFilterFunction.FunctionEntry("([{0}] >= {1}) AND ([{0}] <= {2})", true, 3);
			this._knownFunctionHash[GridKnownFunction.NotBetween] = new GridFilterFunction.FunctionEntry("([{0}] < {1}) OR ([{0}] > {2})", true, 3);
			this._knownFunctionHash[GridKnownFunction.IsEmpty] = new GridFilterFunction.FunctionEntry("[{0}] = ''", false, 1);
			this._knownFunctionHash[GridKnownFunction.NotIsEmpty] = new GridFilterFunction.FunctionEntry("[{0}] <> ''", false, 1);
			this._knownFunctionHash[GridKnownFunction.IsNull] = new GridFilterFunction.FunctionEntry("[{0}] IS NULL", false, 1);
			this._knownFunctionHash[GridKnownFunction.NotIsNull] = new GridFilterFunction.FunctionEntry("NOT ([{0}] IS NULL)", false, 1);
		}

		// Token: 0x0600B21D RID: 45597 RVA: 0x0026BC1C File Offset: 0x00269E1C
		private void ApplyOqlFilters()
		{
			this._knownFunctionHash[GridKnownFunction.Contains] = new GridFilterFunction.StringFunctionEntry("{0} LIKE '*{1}*'", false, 2);
			this._knownFunctionHash[GridKnownFunction.DoesNotContain] = new GridFilterFunction.StringFunctionEntry("NOT ({0} LIKE '*{1}*')", false, 2);
			this._knownFunctionHash[GridKnownFunction.StartsWith] = new GridFilterFunction.StringFunctionEntry("{0} LIKE '{1}*'", false, 2);
			this._knownFunctionHash[GridKnownFunction.EndsWith] = new GridFilterFunction.StringFunctionEntry("{0} LIKE '*{1}'", false, 2);
			this._knownFunctionHash[GridKnownFunction.EqualTo] = new GridFilterFunction.FunctionEntry("{0} = {1}", true, 2);
			this._knownFunctionHash[GridKnownFunction.NotEqualTo] = new GridFilterFunction.FunctionEntry("{0} <> {1}", true, 2);
			this._knownFunctionHash[GridKnownFunction.GreaterThan] = new GridFilterFunction.FunctionEntry("{0} > {1}", true, 2);
			this._knownFunctionHash[GridKnownFunction.LessThan] = new GridFilterFunction.FunctionEntry("{0} < {1}", true, 2);
			this._knownFunctionHash[GridKnownFunction.GreaterThanOrEqualTo] = new GridFilterFunction.FunctionEntry("{0} >= {1}", true, 2);
			this._knownFunctionHash[GridKnownFunction.LessThanOrEqualTo] = new GridFilterFunction.FunctionEntry("{0} <= {1}", true, 2);
			this._knownFunctionHash[GridKnownFunction.Between] = new GridFilterFunction.FunctionEntry("({0} >= {1}) AND ({0} <= {2})", true, 3);
			this._knownFunctionHash[GridKnownFunction.NotBetween] = new GridFilterFunction.FunctionEntry("({0} < {1}) OR ({0} > {2})", true, 3);
			this._knownFunctionHash[GridKnownFunction.IsEmpty] = new GridFilterFunction.FunctionEntry("{0} = ''", false, 1);
			this._knownFunctionHash[GridKnownFunction.NotIsEmpty] = new GridFilterFunction.FunctionEntry("{0} <> ''", false, 1);
			this._knownFunctionHash[GridKnownFunction.IsNull] = new GridFilterFunction.FunctionEntry("{0} == nil", false, 1);
			this._knownFunctionHash[GridKnownFunction.NotIsNull] = new GridFilterFunction.FunctionEntry("({0} != nil)", false, 1);
		}

		// Token: 0x0600B21E RID: 45598 RVA: 0x0026BE01 File Offset: 0x0026A001
		public string GetFunctionString(string fieldName, object value, string valueFormatString)
		{
			return this.GetFunctionString(fieldName, string.Format(valueFormatString, value), value.GetType(), this.tableView);
		}

		// Token: 0x0600B21F RID: 45599 RVA: 0x0026BE1D File Offset: 0x0026A01D
		public string GetFunctionString(string fieldName, object value)
		{
			return this.GetFunctionString(fieldName, value.ToString(), value.GetType(), this.tableView);
		}

		// Token: 0x170039B4 RID: 14772
		// (get) Token: 0x0600B220 RID: 45600 RVA: 0x0026BE38 File Offset: 0x0026A038
		// (set) Token: 0x0600B221 RID: 45601 RVA: 0x0026BEA7 File Offset: 0x0026A0A7
		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		public static string[] IllegalStrings
		{
			get
			{
				if (GridFilterFunction._illegalStrings == null)
				{
					GridFilterFunction._illegalStrings = new string[]
					{
						" LIKE ",
						" AND ",
						" OR ",
						"\"",
						">",
						"<",
						"<>",
						" NULL ",
						" IS "
					};
				}
				return GridFilterFunction._illegalStrings;
			}
			set
			{
				GridFilterFunction._illegalStrings = value;
			}
		}

		// Token: 0x0600B222 RID: 45602 RVA: 0x0026BEB0 File Offset: 0x0026A0B0
		[SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.EndsWith(System.String)")]
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.DateTime.ToString(System.String)")]
		public string GetFunctionString(string fieldName, string value, Type valueDataType, GridTableView tableView)
		{
			GridKnownFunction gridKnownFunction = this._currentKnownFunction;
			if (value == null)
			{
				throw new GridFilteringException("Value should not be null");
			}
			this.tableView = tableView;
			string delimiter = " ";
			if (gridKnownFunction != GridKnownFunction.Custom)
			{
				if (value.IndexOf("'") != -1)
				{
					if (tableView.OwnerGrid.EnableLinqExpressions && !tableView.IsBoundToForwardOnly)
					{
						value = value.Replace("'", "'");
					}
					else
					{
						value = value.Replace("'", "''");
					}
				}
				foreach (string value2 in GridFilterFunction.IllegalStrings)
				{
					if (value.IndexOf(value2) >= 0)
					{
						return "";
					}
				}
			}
			string text = "{0}";
			if (gridKnownFunction == GridKnownFunction.NoFilter)
			{
				return "";
			}
			if (valueDataType == typeof(string) || valueDataType == typeof(char))
			{
				text = "'{0}'";
				if (!tableView.IsBoundToForwardOnly && (tableView.OwnerGrid.EnableLinqExpressions || tableView.IsDataSourceViewWithFiltering()) && valueDataType == typeof(string))
				{
					text = "\"{0}\"";
				}
			}
			else if (valueDataType == typeof(DateTime) || valueDataType == typeof(TimeSpan))
			{
				if (!string.IsNullOrEmpty(tableView.TimeZoneID) && valueDataType == typeof(DateTime))
				{
					DateTime local;
					if (DateTime.TryParse(value, out local))
					{
						local = tableView.TimeZoneProvider.LocalToUtc(local);
						value = local.ToString();
					}
					else
					{
						string[] array = value.Split(new char[]
						{
							' '
						});
						local = DateTime.Parse(array[0]);
						DateTime local2 = DateTime.Parse(array[1]);
						local = tableView.TimeZoneProvider.LocalToUtc(local);
						local2 = tableView.TimeZoneProvider.LocalToUtc(local2);
						if (value.IndexOf(',') > 0)
						{
							value = string.Format("{0} {1}", local.ToString().Replace(' ', ','), local2.ToString().Replace(' ', ','));
						}
						else
						{
							value = string.Format("{0} {1}", local.ToString(), local2.ToString());
						}
					}
				}
				if (tableView.IsOpenAccessDataSourceView())
				{
					text = "timestamp '{0}' ";
					string format = "yyyy-MM-dd H:mm:ss";
					value = GridFilterFunction.GetValueForDateTime(value, gridKnownFunction, format, ref delimiter, true);
				}
				else
				{
					text = "'{0}'";
				}
				if (!tableView.IsOpenAccessDataSourceView() && !tableView.IsBoundToForwardOnly && (tableView.OwnerGrid.EnableLinqExpressions || tableView.IsDataSourceViewWithFiltering()))
				{
					if (valueDataType == typeof(DateTime))
					{
						text = (tableView.IsEntityDataSourceView() ? "DATETIME'{0}'" : "DateTime.Parse(\"{0}\")");
						if (tableView.IsEntityDataSourceView())
						{
							string format2 = "yyyy-MM-dd HH:mm";
							value = GridFilterFunction.GetValueForDateTime(value, gridKnownFunction, format2, ref delimiter, false);
						}
					}
					if (valueDataType == typeof(TimeSpan))
					{
						text = (tableView.IsEntityDataSourceView() ? "TIME'{0}'" : "TimeSpan.Parse(\"{0}\")");
					}
				}
			}
			else if (valueDataType == typeof(Guid))
			{
				text = "'{0}'";
				if (!tableView.IsOpenAccessDataSourceView() && !tableView.IsBoundToForwardOnly && (tableView.OwnerGrid.EnableLinqExpressions || tableView.IsDataSourceViewWithFiltering()))
				{
					if (this.ShouldConvertGuidToString())
					{
						text = (tableView.IsEntityDataSourceView() ? "GUID'{0}'" : "Guid(\"{0}\").ToString()");
					}
					else
					{
						text = (tableView.IsEntityDataSourceView() ? "GUID'{0}'" : "Guid(\"{0}\")");
					}
				}
			}
			if (gridKnownFunction == GridKnownFunction.Custom)
			{
				return value;
			}
			if (gridKnownFunction == GridKnownFunction.EqualTo && string.IsNullOrEmpty(value))
			{
				gridKnownFunction = GridKnownFunction.IsNull;
			}
			GridFilterFunction.FunctionEntry functionEntry = (GridFilterFunction.FunctionEntry)this.KnownFunctionHash[gridKnownFunction];
			functionEntry.PreserveWhiteSpacesInFilter = tableView.PersistWhiteSpacesInFilter;
			functionEntry.RestoreFunctionFormat();
			bool flag = tableView.BoundUsingDataSourceID && tableView.IsLinqDataSourceView();
			Type queryableElementType = tableView.QueryableElementType;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = tableView.HasCalculatedColumns();
			if ((queryableElementType == typeof(DataRowView) || queryableElementType == typeof(DataRow) || queryableElementType.GetInterface("IDataRecord") != null) && !flag4)
			{
				flag2 = true;
			}
			if (GridBaseDataList.IsBindableType(queryableElementType) && !flag4)
			{
				flag3 = true;
			}
			if (!tableView.IsBoundToForwardOnly && gridKnownFunction != GridKnownFunction.IsNull && gridKnownFunction != GridKnownFunction.NotIsNull && !tableView.IsOpenAccessDataSourceView() && !flag2 && !tableView.IsEntityDataSourceView() && !flag4 && (tableView.OwnerGrid.EnableLinqExpressions || tableView.IsDataSourceViewWithFiltering()) && (valueDataType == typeof(string) || valueDataType == typeof(char)))
			{
				if (functionEntry.FunctionFormat.IndexOf("ToString()") == -1)
				{
					if (flag3)
					{
						functionEntry.FunctionFormat = functionEntry.FunctionFormat.Replace("it", "it.ToString()");
					}
					else if (!flag || valueDataType != typeof(char))
					{
						functionEntry.FunctionFormat = functionEntry.FunctionFormat.Replace("{0}", "{0}.ToString()");
					}
				}
				if (flag3)
				{
					functionEntry.FunctionFormat = functionEntry.FunctionFormat.Replace("it.", "iif(it == null, \"\", it).");
				}
				else if (!flag || valueDataType != typeof(char))
				{
					functionEntry.FunctionFormat = functionEntry.FunctionFormat.Replace("{0}", "iif({0} == null, \"\", {0})");
				}
			}
			if (!functionEntry.DataTypeValid(valueDataType))
			{
				return string.Empty;
			}
			bool flag5 = false;
			if (flag4 && tableView.OwnerGrid.EnableLinqExpressions)
			{
				flag5 = true;
			}
			ArrayList arrayList = new ArrayList();
			if (flag4 && valueDataType == typeof(Guid) && (gridKnownFunction == GridKnownFunction.Between || gridKnownFunction == GridKnownFunction.NotBetween))
			{
				arrayList.Add(string.Format("Guid({0}).ToString()", fieldName));
			}
			else
			{
				arrayList.Add(fieldName);
			}
			if (functionEntry.ParamCount == 2)
			{
				arrayList.Add(functionEntry.FormatValue(value, text));
				if (flag && valueDataType == typeof(char) && value == " ")
				{
					arrayList[1] = "' '";
				}
			}
			else if (functionEntry.ParamCount > 2)
			{
				bool flag6 = gridKnownFunction == GridKnownFunction.Between || gridKnownFunction == GridKnownFunction.NotBetween || !tableView.OwnerGrid.EnableLinqExpressions;
				if (flag6)
				{
					if (flag2 && (this.CurrentKnownFunction == GridKnownFunction.Between || gridKnownFunction == GridKnownFunction.NotBetween))
					{
						GridFilterFunction.AddTypeParameter(valueDataType, tableView, functionEntry, flag4, arrayList);
					}
					GridStringTokenizer gridStringTokenizer = new GridStringTokenizer(value.Trim(), delimiter);
					foreach (object obj in gridStringTokenizer)
					{
						string value3 = (string)obj;
						arrayList.Add(functionEntry.FormatValue(value3, text));
					}
					if (flag2 && (this.CurrentKnownFunction == GridKnownFunction.Between || gridKnownFunction == GridKnownFunction.NotBetween) && !tableView.IsDataSourceViewWithFiltering() && tableView.OwnerGrid.EnableLinqExpressions)
					{
						if (gridStringTokenizer.NumTokens != functionEntry.ParamCount - 3)
						{
							return string.Empty;
						}
					}
					else if (flag5 && (this.CurrentKnownFunction == GridKnownFunction.Between || gridKnownFunction == GridKnownFunction.NotBetween))
					{
						if (gridStringTokenizer.NumTokens != functionEntry.ParamCount - 2)
						{
							return string.Empty;
						}
					}
					else if (gridStringTokenizer.NumTokens != functionEntry.ParamCount - 1)
					{
						return string.Empty;
					}
				}
				else
				{
					string text2 = valueDataType.ToString().Split(new char[]
					{
						'.'
					})[1];
					if (!tableView.IsEntityDataSourceView() && (tableView.OwnerGrid.EnableLinqExpressions || tableView.IsDataSourceViewWithFiltering()))
					{
						if (valueDataType != typeof(string) && valueDataType != typeof(object))
						{
							if (flag4 && valueDataType != typeof(Guid))
							{
								text2 = string.Format("{0}?", text2);
							}
							else
							{
								functionEntry.FunctionFormat = functionEntry.FunctionFormat.Replace("it[\"{0}\"]", "iif(it[\"{0}\"]==Convert.DBNull,null,it[\"{0}\"])");
								functionEntry.FunctionFormat = string.Format("{0} {1}", functionEntry.FunctionFormat, " AND it[\"{0}\"] != Convert.DBNull");
								if (valueDataType != typeof(Guid))
								{
									text2 = string.Format("Convert.To{0}", text2);
								}
							}
						}
						Type queryableElementType2 = tableView.QueryableElementType;
						if ((queryableElementType2 == typeof(DataRowView) || queryableElementType2 == typeof(DataRow) || queryableElementType2.GetInterface("IDataRecord") != null) && valueDataType == typeof(string))
						{
							text2 = "Convert.ToString";
						}
					}
					arrayList.Add(text2);
					arrayList.Add(string.Format(text, value));
				}
			}
			if (!tableView.OwnerGrid.GroupingSettings.CaseSensitive && !tableView.IsBoundToForwardOnly)
			{
				if (functionEntry.FunctionFormat.IndexOf("ToString()") != -1)
				{
					functionEntry.FunctionFormat = functionEntry.FunctionFormat.Replace("ToString()", "ToString().ToUpper()");
					functionEntry.FunctionFormat = functionEntry.FunctionFormat.Replace("\"{1}\"", "\"{1}\".ToUpper()");
				}
				if (arrayList.Count < functionEntry.ParamCount && arrayList[1].ToString() == "Convert.ToString")
				{
					arrayList.Add(".ToUpper()");
					functionEntry.FunctionFormat = functionEntry.FunctionFormat.Replace("{2}", "{2}.ToUpper()");
					if (this.CurrentKnownFunction == GridKnownFunction.Between || this.CurrentKnownFunction == GridKnownFunction.NotBetween)
					{
						functionEntry.FunctionFormat = functionEntry.FunctionFormat.Replace("{3}", "{3}.ToUpper()");
					}
				}
				if (!tableView.IsOpenAccessDataSourceView() && !flag2 && !flag4 && (valueDataType == typeof(string) || valueDataType == typeof(char)) && !tableView.IsEntityDataSourceView() && (tableView.OwnerGrid.EnableLinqExpressions || tableView.IsDataSourceViewWithFiltering()) && (functionEntry.FunctionFormat.EndsWith("{1}") || this.CurrentKnownFunction == GridKnownFunction.Between || this.CurrentKnownFunction == GridKnownFunction.NotBetween))
				{
					functionEntry.FunctionFormat = functionEntry.FunctionFormat.Replace("{1}", "{1}.ToUpper()");
					functionEntry.FunctionFormat = functionEntry.FunctionFormat.Replace("{2}", "{2}.ToUpper()");
				}
			}
			if (!tableView.IsEntityDataSourceView())
			{
				if (tableView.OwnerGrid.EnableLinqExpressions || tableView.IsDataSourceViewWithFiltering())
				{
					Type queryableElementType3 = tableView.QueryableElementType;
					if ((queryableElementType3 == typeof(DataRowView) || queryableElementType3 == typeof(DataRow) || queryableElementType3.GetInterface("IDataRecord") != null || flag4) && (valueDataType != typeof(string) || tableView.OwnerGrid.GroupingSettings.CaseSensitive))
					{
						if (valueDataType == typeof(Guid) && this.ShouldConvertGuidToString())
						{
							arrayList.Add(".ToString()");
						}
						else
						{
							arrayList.Add(string.Empty);
						}
					}
				}
			}
			else if (valueDataType == typeof(decimal))
			{
				functionEntry.FunctionFormat = functionEntry.FunctionFormat.Replace("{1}", "{1}m");
				functionEntry.FunctionFormat = functionEntry.FunctionFormat.Replace("{2}", "{2}m");
			}
			if (flag5 && (this.CurrentKnownFunction == GridKnownFunction.Between || gridKnownFunction == GridKnownFunction.NotBetween))
			{
				GridFilterFunction.AddTypeParameter(valueDataType, tableView, functionEntry, flag4, arrayList);
				if (arrayList.Count > functionEntry.ParamCount)
				{
					arrayList[functionEntry.ParamCount - 1] = arrayList[arrayList.Count - 1];
				}
			}
			return string.Format(functionEntry.FunctionFormat, arrayList.ToArray());
		}

		// Token: 0x0600B223 RID: 45603 RVA: 0x0026CB1C File Offset: 0x0026AD1C
		private static void AddTypeParameter(Type valueDataType, GridTableView tableView, GridFilterFunction.FunctionEntry entry, bool hasCalculatedColumns, ArrayList paramList)
		{
			if (!tableView.IsDataSourceViewWithFiltering() && tableView.OwnerGrid.EnableLinqExpressions)
			{
				string text = valueDataType.ToString().Split(new char[]
				{
					'.'
				})[1];
				if (valueDataType != typeof(string) && valueDataType != typeof(object))
				{
					if (hasCalculatedColumns && valueDataType != typeof(Guid))
					{
						text = string.Format("{0}?", text);
					}
					else
					{
						entry.FunctionFormat = entry.FunctionFormat.Replace("it[\"{0}\"]", "iif(it[\"{0}\"]==Convert.DBNull,null,it[\"{0}\"])");
						if (valueDataType != typeof(Guid))
						{
							text = string.Format("Convert.To{0}", text);
						}
					}
				}
				Type queryableElementType = tableView.QueryableElementType;
				if ((queryableElementType == typeof(DataRowView) || queryableElementType == typeof(DataRow) || queryableElementType.GetInterface("IDataRecord") != null) && valueDataType == typeof(string))
				{
					text = "Convert.ToString";
				}
				paramList.Add(text);
			}
		}

		// Token: 0x0600B224 RID: 45604 RVA: 0x0026CC3E File Offset: 0x0026AE3E
		private bool ShouldConvertGuidToString()
		{
			return this._currentKnownFunction == GridKnownFunction.GreaterThan || this._currentKnownFunction == GridKnownFunction.LessThan || this._currentKnownFunction == GridKnownFunction.GreaterThanOrEqualTo || this._currentKnownFunction == GridKnownFunction.LessThanOrEqualTo || this._currentKnownFunction == GridKnownFunction.Between || this._currentKnownFunction == GridKnownFunction.NotBetween;
		}

		// Token: 0x0600B225 RID: 45605 RVA: 0x0026CC80 File Offset: 0x0026AE80
		public static string GetValueForDateTime(string value, GridKnownFunction function, string format, ref string tokanizerDelimiter, bool useCultureInvariant = false)
		{
			DateTime dateTime;
			if (DateTime.TryParse(value, out dateTime))
			{
				if (useCultureInvariant)
				{
					value = dateTime.ToString(format, DateTimeFormatInfo.InvariantInfo);
				}
				else
				{
					value = dateTime.ToString(format);
				}
			}
			else if (function == GridKnownFunction.Between || function == GridKnownFunction.NotBetween)
			{
				string[] array = value.Split(new char[]
				{
					' '
				});
				DateTime dateTime2;
				DateTime dateTime3;
				if (DateTime.TryParse(array[0], out dateTime2) && DateTime.TryParse(array[1], out dateTime3))
				{
					if (useCultureInvariant)
					{
						value = string.Format("{0};b;{1}", dateTime2.ToString(format, DateTimeFormatInfo.InvariantInfo), dateTime3.ToString(format, DateTimeFormatInfo.InvariantInfo));
					}
					else
					{
						value = string.Format("{0};b;{1}", dateTime2.ToString(format), dateTime3.ToString(format));
					}
					tokanizerDelimiter = ";b;";
				}
			}
			return value;
		}

		// Token: 0x0600B226 RID: 45606 RVA: 0x0026CD47 File Offset: 0x0026AF47
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		internal bool IsTypeNullable(Type type)
		{
			return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
		}

		// Token: 0x04002EDD RID: 11997
		private GridKnownFunction _currentKnownFunction = GridKnownFunction.Custom;

		// Token: 0x04002EDE RID: 11998
		private string _customFunction = string.Empty;

		// Token: 0x04002EDF RID: 11999
		private Hashtable _knownFunctionHash;

		// Token: 0x04002EE0 RID: 12000
		private static string[] _illegalStrings;

		// Token: 0x04002EE1 RID: 12001
		internal GridTableView tableView;

		// Token: 0x02001102 RID: 4354
		[SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
		public class FunctionEntry
		{
			// Token: 0x0600B228 RID: 45608 RVA: 0x0026CD6D File Offset: 0x0026AF6D
			public FunctionEntry(string functionFormat, bool mustQuoteValues, int paramCount)
			{
				this.FunctionFormat = functionFormat;
				this.MustQuoteValues = mustQuoteValues;
				this.ParamCount = paramCount;
				this.OriginalFunctionFormat = functionFormat;
			}

			// Token: 0x0600B229 RID: 45609 RVA: 0x0026CD91 File Offset: 0x0026AF91
			internal void RestoreFunctionFormat()
			{
				this.FunctionFormat = this.OriginalFunctionFormat;
			}

			// Token: 0x0600B22A RID: 45610 RVA: 0x0026CD9F File Offset: 0x0026AF9F
			public string FormatValue(string value, string quotedValueFormatString)
			{
				if (!this.PreserveWhiteSpacesInFilter)
				{
					value = value.Trim();
				}
				if (!this.MustQuoteValues)
				{
					return value;
				}
				return string.Format(quotedValueFormatString, value);
			}

			// Token: 0x0600B22B RID: 45611 RVA: 0x0026CDC2 File Offset: 0x0026AFC2
			public virtual bool DataTypeValid(Type dataType)
			{
				return true;
			}

			// Token: 0x04002EE2 RID: 12002
			[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
			public string FunctionFormat;

			// Token: 0x04002EE3 RID: 12003
			[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
			public bool MustQuoteValues;

			// Token: 0x04002EE4 RID: 12004
			[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
			public int ParamCount;

			// Token: 0x04002EE5 RID: 12005
			private string OriginalFunctionFormat;

			// Token: 0x04002EE6 RID: 12006
			internal bool PreserveWhiteSpacesInFilter;
		}

		// Token: 0x02001103 RID: 4355
		[SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
		public class StringFunctionEntry : GridFilterFunction.FunctionEntry
		{
			// Token: 0x0600B22C RID: 45612 RVA: 0x0026CDC5 File Offset: 0x0026AFC5
			public StringFunctionEntry(string functionFormat, bool mustQuoteValues, int paramCount) : base(functionFormat, mustQuoteValues, paramCount)
			{
			}

			// Token: 0x0600B22D RID: 45613 RVA: 0x0026CDD0 File Offset: 0x0026AFD0
			public override bool DataTypeValid(Type dataType)
			{
				return dataType == typeof(string);
			}
		}
	}
}
