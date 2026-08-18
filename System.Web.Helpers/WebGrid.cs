using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web.Helpers.Resources;
using System.Web.WebPages;
using Microsoft.Internal.Web.Utils;

namespace System.Web.Helpers
{
	// Token: 0x0200001E RID: 30
	public class WebGrid
	{
		// Token: 0x06000146 RID: 326 RVA: 0x00006620 File Offset: 0x00004820
		public WebGrid(IEnumerable<dynamic> source = null, IEnumerable<string> columnNames = null, string defaultSort = null, int rowsPerPage = 10, bool canPage = true, bool canSort = true, string ajaxUpdateContainerId = null, string ajaxUpdateCallback = null, string fieldNamePrefix = null, string pageFieldName = null, string selectionFieldName = null, string sortFieldName = null, string sortDirectionFieldName = null) : this(new HttpContextWrapper(System.Web.HttpContext.Current), defaultSort, rowsPerPage, canPage, canSort, ajaxUpdateContainerId, ajaxUpdateCallback, fieldNamePrefix, pageFieldName, selectionFieldName, sortFieldName, sortDirectionFieldName)
		{
			if (source != null)
			{
				this.Bind(source, columnNames, true, -1);
			}
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00006660 File Offset: 0x00004860
		internal WebGrid(HttpContextBase context, string defaultSort = null, int rowsPerPage = 10, bool canPage = true, bool canSort = true, string ajaxUpdateContainerId = null, string ajaxUpdateCallback = null, string fieldNamePrefix = null, string pageFieldName = null, string selectionFieldName = null, string sortFieldName = null, string sortDirectionFieldName = null)
		{
			if (rowsPerPage < 1)
			{
				throw new ArgumentOutOfRangeException("rowsPerPage", string.Format(CultureInfo.CurrentCulture, CommonResources.Argument_Must_Be_GreaterThanOrEqualTo, new object[]
				{
					1
				}));
			}
			this._context = context;
			this._defaultSort = defaultSort;
			this._rowsPerPage = rowsPerPage;
			this._canPage = canPage;
			this._canSort = canSort;
			this._ajaxUpdateContainerId = ajaxUpdateContainerId;
			this._ajaxUpdateCallback = ajaxUpdateCallback;
			this._fieldNamePrefix = fieldNamePrefix;
			if (!string.IsNullOrEmpty(pageFieldName))
			{
				this._pageFieldName = pageFieldName;
			}
			if (!string.IsNullOrEmpty(selectionFieldName))
			{
				this._selectionFieldName = selectionFieldName;
			}
			if (!string.IsNullOrEmpty(sortFieldName))
			{
				this._sortFieldName = sortFieldName;
			}
			if (!string.IsNullOrEmpty(sortDirectionFieldName))
			{
				this._sortDirectionFieldName = sortDirectionFieldName;
			}
			this.CustomSorters = new Dictionary<string, Expression>(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000148 RID: 328 RVA: 0x0000676D File Offset: 0x0000496D
		public IEnumerable<string> ColumnNames
		{
			get
			{
				this.EnsureDataBound();
				return this._columnNames;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000149 RID: 329 RVA: 0x0000677B File Offset: 0x0000497B
		public bool CanSort
		{
			get
			{
				return this._canSort;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00006783 File Offset: 0x00004983
		public string AjaxUpdateContainerId
		{
			get
			{
				return this._ajaxUpdateContainerId;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600014B RID: 331 RVA: 0x0000678B File Offset: 0x0000498B
		public bool IsAjaxEnabled
		{
			get
			{
				return !string.IsNullOrEmpty(this._ajaxUpdateContainerId);
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600014C RID: 332 RVA: 0x0000679B File Offset: 0x0000499B
		public string AjaxUpdateCallback
		{
			get
			{
				return this._ajaxUpdateCallback;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600014D RID: 333 RVA: 0x000067A3 File Offset: 0x000049A3
		public string FieldNamePrefix
		{
			get
			{
				return this._fieldNamePrefix ?? string.Empty;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600014E RID: 334 RVA: 0x000067B4 File Offset: 0x000049B4
		public bool HasSelection
		{
			get
			{
				return this.SelectedIndex >= 0;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600014F RID: 335 RVA: 0x000067C2 File Offset: 0x000049C2
		public int PageCount
		{
			get
			{
				if (!this._canPage)
				{
					return 1;
				}
				return (int)Math.Ceiling((double)this.TotalRowCount / (double)this.RowsPerPage);
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000150 RID: 336 RVA: 0x000067E3 File Offset: 0x000049E3
		public string PageFieldName
		{
			get
			{
				return this.FieldNamePrefix + this._pageFieldName;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000151 RID: 337 RVA: 0x000067F8 File Offset: 0x000049F8
		// (set) Token: 0x06000152 RID: 338 RVA: 0x00006870 File Offset: 0x00004A70
		public int PageIndex
		{
			get
			{
				if (!this._canPage)
				{
					return 0;
				}
				if (!this._pageIndexSet)
				{
					int num;
					if (!this._canPage || !int.TryParse(this.QueryString[this.PageFieldName], out num) || num < 1)
					{
						num = 1;
					}
					if (this._dataSourceBound && num > this.PageCount)
					{
						num = this.PageCount;
					}
					this._pageIndex = num - 1;
					this._pageIndexSet = true;
				}
				return this._pageIndex;
			}
			set
			{
				if (!this._canPage)
				{
					throw new NotSupportedException(HelpersResources.WebGrid_NotSupportedIfPagingIsDisabled);
				}
				if (!this._dataSourceBound)
				{
					if (value < 0)
					{
						throw new ArgumentOutOfRangeException("value", string.Format(CultureInfo.CurrentCulture, CommonResources.Argument_Must_Be_GreaterThanOrEqualTo, new object[]
						{
							0
						}));
					}
					this._pageIndex = value;
					this._pageIndexSet = true;
					return;
				}
				else
				{
					if (value < 0 || value >= this.PageCount)
					{
						throw new ArgumentOutOfRangeException("value", string.Format(CultureInfo.CurrentCulture, CommonResources.Argument_Must_Be_Between, new object[]
						{
							0,
							this.PageCount - 1
						}));
					}
					if (value != this._pageIndex)
					{
						this.EnsureDataSourceNotMaterialized();
						this._pageIndex = value;
						this._pageIndexSet = true;
					}
					return;
				}
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000153 RID: 339 RVA: 0x0000693B File Offset: 0x00004B3B
		public IList<WebGridRow> Rows
		{
			get
			{
				this.EnsureDataBound();
				if (!this._dataSourceMaterialized)
				{
					this._rows = this._dataSource.GetRows(this.SortInfo, this.PageIndex);
					this._dataSourceMaterialized = true;
				}
				return this._rows;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00006975 File Offset: 0x00004B75
		public int RowsPerPage
		{
			get
			{
				return this._rowsPerPage;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000155 RID: 341 RVA: 0x0000697D File Offset: 0x00004B7D
		public WebGridRow SelectedRow
		{
			get
			{
				if (this.SelectedIndex >= 0 && this.SelectedIndex < this.Rows.Count)
				{
					return this.Rows[this.SelectedIndex];
				}
				return null;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000156 RID: 342 RVA: 0x000069B0 File Offset: 0x00004BB0
		// (set) Token: 0x06000157 RID: 343 RVA: 0x00006A0C File Offset: 0x00004C0C
		public int SelectedIndex
		{
			get
			{
				if (!this._selectedIndexSet)
				{
					int num;
					if (!int.TryParse(this.QueryString[this.SelectionFieldName], out num) || num < 1 || (this._canPage && num > this.RowsPerPage))
					{
						num = 0;
					}
					this._selectedIndex = num - 1;
					this._selectedIndexSet = true;
				}
				return this._selectedIndex;
			}
			set
			{
				if (this._selectedIndex != value)
				{
					this.EnsureDataSourceNotMaterialized();
					this._selectedIndex = value;
				}
				this._selectedIndexSet = true;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00006A2B File Offset: 0x00004C2B
		public string SelectionFieldName
		{
			get
			{
				return this.FieldNamePrefix + this._selectionFieldName;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00006A40 File Offset: 0x00004C40
		// (set) Token: 0x0600015A RID: 346 RVA: 0x00006AA9 File Offset: 0x00004CA9
		public string SortColumn
		{
			get
			{
				if (!this._sortColumnSet)
				{
					string text = this.QueryString[this.SortFieldName];
					if (!this._dataSourceBound || this.ValidateSortColumn(text))
					{
						this._sortColumn = text;
						this._sortColumnSet = true;
					}
				}
				if (string.IsNullOrEmpty(this._sortColumn))
				{
					return this._defaultSort ?? string.Empty;
				}
				return this._sortColumn;
			}
			set
			{
				this.EnsureDataBound();
				if (!this.SortColumn.Equals(value, StringComparison.OrdinalIgnoreCase))
				{
					this.EnsureDataSourceNotMaterialized();
					this._sortColumn = value;
				}
				this._sortColumnSet = true;
				this._sortColumnExplicitlySet = true;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00006ADC File Offset: 0x00004CDC
		// (set) Token: 0x0600015C RID: 348 RVA: 0x00006B36 File Offset: 0x00004D36
		public SortDirection SortDirection
		{
			get
			{
				if (!this._sortDirectionSet)
				{
					string text = this.QueryString[this.SortDirectionFieldName];
					if (text != null && (text.Equals("DESC", StringComparison.OrdinalIgnoreCase) || text.Equals("DESCENDING", StringComparison.OrdinalIgnoreCase)))
					{
						this._sortDirection = SortDirection.Descending;
					}
					this._sortDirectionSet = true;
				}
				return this._sortDirection;
			}
			set
			{
				if (!this._dataSourceBound)
				{
					this._sortDirection = value;
				}
				else if (this._sortDirection != value)
				{
					this.EnsureDataSourceNotMaterialized();
					this._sortDirection = value;
				}
				this._sortDirectionSet = true;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00006B68 File Offset: 0x00004D68
		private SortInfo SortInfo
		{
			get
			{
				return new SortInfo
				{
					SortColumn = this.SortColumn,
					SortDirection = this.SortDirection
				};
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00006B94 File Offset: 0x00004D94
		public string SortDirectionFieldName
		{
			get
			{
				return this.FieldNamePrefix + this._sortDirectionFieldName;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00006BA7 File Offset: 0x00004DA7
		public string SortFieldName
		{
			get
			{
				return this.FieldNamePrefix + this._sortFieldName;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00006BBA File Offset: 0x00004DBA
		// (set) Token: 0x06000161 RID: 353 RVA: 0x00006BC2 File Offset: 0x00004DC2
		internal IDictionary<string, Expression> CustomSorters { get; private set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00006BCB File Offset: 0x00004DCB
		public int TotalRowCount
		{
			get
			{
				this.EnsureDataBound();
				return this._dataSource.TotalRowCount;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00006BDE File Offset: 0x00004DDE
		private HttpContextBase HttpContext
		{
			get
			{
				return this._context;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00006BE6 File Offset: 0x00004DE6
		private NameValueCollection QueryString
		{
			get
			{
				return this.HttpContext.Request.QueryString;
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00006C04 File Offset: 0x00004E04
		internal static Type GetElementType(IEnumerable<dynamic> source)
		{
			Type type = source.GetType();
			if (source.FirstOrDefault<object>() is IDynamicMetaObjectProvider)
			{
				return typeof(IDynamicMetaObjectProvider);
			}
			if (type.IsArray)
			{
				return type.GetElementType();
			}
			return type.GetInterfaces().Select(new Func<Type, Type>(WebGrid.GetGenericEnumerableType)).FirstOrDefault((Type t) => t != null);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00006C7C File Offset: 0x00004E7C
		private static Type GetGenericEnumerableType(Type type)
		{
			Type typeFromHandle = typeof(IEnumerable<>);
			if (type.IsGenericType && typeFromHandle.IsAssignableFrom(type.GetGenericTypeDefinition()))
			{
				return type.GetGenericArguments()[0];
			}
			return null;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00006CB4 File Offset: 0x00004EB4
		public WebGrid Bind(IEnumerable<dynamic> source, IEnumerable<string> columnNames = null, bool autoSortAndPage = true, int rowCount = -1)
		{
			if (this._dataSourceBound)
			{
				throw new InvalidOperationException(HelpersResources.WebGrid_DataSourceBound);
			}
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (!autoSortAndPage && this._canPage && rowCount == -1)
			{
				throw new ArgumentException(HelpersResources.WebGrid_RowCountNotSpecified, "rowCount");
			}
			this._elementType = WebGrid.GetElementType(source);
			if (this._columnNames == null)
			{
				this._columnNames = (columnNames ?? WebGrid.GetDefaultColumnNames(source, this._elementType));
			}
			if (!autoSortAndPage)
			{
				this._dataSource = new PreComputedGridDataSource(this, source, rowCount);
			}
			else
			{
				this._dataSource = new WebGridDataSource(this, source, this._elementType, this._canPage, this._canSort)
				{
					DefaultSort = new SortInfo
					{
						SortColumn = this._defaultSort,
						SortDirection = SortDirection.Ascending
					},
					RowsPerPage = this._rowsPerPage
				};
			}
			this._dataSourceBound = true;
			this.ValidatePreDataBoundValues();
			return this;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00006D9C File Offset: 0x00004F9C
		public WebGridColumn Column(string columnName = null, string header = null, Func<dynamic, object> format = null, string style = null, bool canSort = true)
		{
			if (string.IsNullOrEmpty(columnName) && format == null)
			{
				throw new ArgumentException(HelpersResources.WebGrid_ColumnNameOrFormatRequired, "columnName");
			}
			return new WebGridColumn
			{
				ColumnName = columnName,
				Header = header,
				Format = format,
				Style = style,
				CanSort = canSort
			};
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00006DF0 File Offset: 0x00004FF0
		public WebGridColumn[] Columns(params WebGridColumn[] columnSet)
		{
			return columnSet;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00006DF4 File Offset: 0x00004FF4
		public IHtmlString GetContainerUpdateScript(string path)
		{
			string s = string.Format(CultureInfo.InvariantCulture, "$({1}).swhgLoad({0},{1}{2});", new object[]
			{
				HttpUtility.JavaScriptStringEncode(path, true),
				HttpUtility.JavaScriptStringEncode('#' + this.AjaxUpdateContainerId, true),
				(!string.IsNullOrEmpty(this.AjaxUpdateCallback)) ? (',' + HttpUtility.JavaScriptStringEncode(this.AjaxUpdateCallback)) : string.Empty
			});
			return new HtmlString(HttpUtility.HtmlAttributeEncode(s));
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00006EB4 File Offset: 0x000050B4
		public IHtmlString GetHtml(string tableStyle = null, string headerStyle = null, string footerStyle = null, string rowStyle = null, string alternatingRowStyle = null, string selectedRowStyle = null, string caption = null, bool displayHeader = true, bool fillEmptyRows = false, string emptyRowCellValue = null, IEnumerable<WebGridColumn> columns = null, IEnumerable<string> exclusions = null, WebGridPagerModes mode = WebGridPagerModes.Numeric | WebGridPagerModes.NextPrevious, string firstText = null, string previousText = null, string nextText = null, string lastText = null, int numericLinksCount = 5, object htmlAttributes = null)
		{
			Func<object, object> footer = null;
			if (this._canPage && this.PageCount > 1)
			{
				footer = ((dynamic item) => this.Pager(mode, firstText, previousText, nextText, lastText, numericLinksCount, false));
			}
			return this.Table(tableStyle, headerStyle, footerStyle, rowStyle, alternatingRowStyle, selectedRowStyle, caption, displayHeader, fillEmptyRows, emptyRowCellValue, columns, exclusions, footer, htmlAttributes);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00006F44 File Offset: 0x00005144
		public string GetPageUrl(int pageIndex)
		{
			if (!this._canPage)
			{
				throw new NotSupportedException(HelpersResources.WebGrid_NotSupportedIfPagingIsDisabled);
			}
			if (pageIndex < 0 || pageIndex >= this.PageCount)
			{
				throw new ArgumentOutOfRangeException("pageIndex", string.Format(CultureInfo.CurrentCulture, CommonResources.Argument_Must_Be_Between, new object[]
				{
					0,
					this.PageCount - 1
				}));
			}
			NameValueCollection nameValueCollection = new NameValueCollection(1);
			nameValueCollection[this.PageFieldName] = ((long)pageIndex + 1L).ToString(CultureInfo.CurrentCulture);
			return this.GetPath(nameValueCollection, new string[]
			{
				this.SelectionFieldName
			});
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00006FEC File Offset: 0x000051EC
		public string GetSortUrl(string column)
		{
			if (!this._canSort)
			{
				throw new NotSupportedException(HelpersResources.WebGrid_NotSupportedIfSortingIsDisabled);
			}
			if (string.IsNullOrEmpty(column))
			{
				throw new ArgumentException(CommonResources.Argument_Cannot_Be_Null_Or_Empty, "column");
			}
			string sortColumn = this.SortColumn;
			SortDirection sortDir = SortDirection.Ascending;
			if (column.Equals(sortColumn, StringComparison.OrdinalIgnoreCase) && this.SortDirection == SortDirection.Ascending)
			{
				sortDir = SortDirection.Descending;
			}
			NameValueCollection nameValueCollection = new NameValueCollection(2);
			nameValueCollection[this.SortFieldName] = column;
			nameValueCollection[this.SortDirectionFieldName] = WebGrid.GetSortDirectionString(sortDir);
			return this.GetPath(nameValueCollection, new string[]
			{
				this.PageFieldName,
				this.SelectionFieldName
			});
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00007088 File Offset: 0x00005288
		public HelperResult Pager(WebGridPagerModes mode = WebGridPagerModes.Numeric | WebGridPagerModes.NextPrevious, string firstText = null, string previousText = null, string nextText = null, string lastText = null, int numericLinksCount = 5)
		{
			return this.Pager(mode, firstText, previousText, nextText, lastText, numericLinksCount, true);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000709C File Offset: 0x0000529C
		private HelperResult Pager(WebGridPagerModes mode, string firstText, string previousText, string nextText, string lastText, int numericLinksCount, bool explicitlyCalled)
		{
			if (!this._canPage)
			{
				throw new NotSupportedException(HelpersResources.WebGrid_NotSupportedIfPagingIsDisabled);
			}
			if (!WebGrid.ModeEnabled(mode, WebGridPagerModes.FirstLast) && firstText != null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, HelpersResources.WebGrid_PagerModeMustBeEnabled, new object[]
				{
					"FirstLast"
				}), "firstText");
			}
			if (!WebGrid.ModeEnabled(mode, WebGridPagerModes.NextPrevious) && previousText != null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, HelpersResources.WebGrid_PagerModeMustBeEnabled, new object[]
				{
					"NextPrevious"
				}), "previousText");
			}
			if (!WebGrid.ModeEnabled(mode, WebGridPagerModes.NextPrevious) && nextText != null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, HelpersResources.WebGrid_PagerModeMustBeEnabled, new object[]
				{
					"NextPrevious"
				}), "nextText");
			}
			if (!WebGrid.ModeEnabled(mode, WebGridPagerModes.FirstLast) && lastText != null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, HelpersResources.WebGrid_PagerModeMustBeEnabled, new object[]
				{
					"FirstLast"
				}), "lastText");
			}
			if (numericLinksCount < 0)
			{
				throw new ArgumentOutOfRangeException("numericLinksCount", string.Format(CultureInfo.CurrentCulture, CommonResources.Argument_Must_Be_GreaterThanOrEqualTo, new object[]
				{
					0
				}));
			}
			return WebGridRenderer.Pager(this, this.HttpContext, mode, firstText, previousText, nextText, lastText, numericLinksCount, explicitlyCalled);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000071E0 File Offset: 0x000053E0
		public IHtmlString Table(string tableStyle = null, string headerStyle = null, string footerStyle = null, string rowStyle = null, string alternatingRowStyle = null, string selectedRowStyle = null, string caption = null, bool displayHeader = true, bool fillEmptyRows = false, string emptyRowCellValue = null, IEnumerable<WebGridColumn> columns = null, IEnumerable<string> exclusions = null, Func<dynamic, object> footer = null, object htmlAttributes = null)
		{
			if (columns == null)
			{
				columns = this.GetDefaultColumns(exclusions);
			}
			this.EnsureColumnIsSortable(columns);
			if (emptyRowCellValue == null)
			{
				emptyRowCellValue = "&nbsp;";
			}
			return WebGridRenderer.Table(this, this.HttpContext, tableStyle, headerStyle, footerStyle, rowStyle, alternatingRowStyle, selectedRowStyle, caption, displayHeader, fillEmptyRows, emptyRowCellValue, columns, exclusions, footer, htmlAttributes);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00007233 File Offset: 0x00005433
		public WebGrid AddSorter<TElement, TProperty>(string columnName, Expression<Func<TElement, TProperty>> keySelector)
		{
			this.CustomSorters[columnName] = keySelector;
			return this;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000724C File Offset: 0x0000544C
		private void EnsureColumnIsSortable(IEnumerable<WebGridColumn> columns)
		{
			if (this._canSort && !this._sortColumnExplicitlySet && !string.IsNullOrEmpty(this.SortColumn) && !StringComparer.OrdinalIgnoreCase.Equals(this._defaultSort, this.SortColumn))
			{
				if (!(from c in columns
				select c.ColumnName).Contains(this.SortColumn, StringComparer.OrdinalIgnoreCase))
				{
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, HelpersResources.WebGrid_ColumnNotFound, new object[]
					{
						this.SortColumn
					}));
				}
			}
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000072F0 File Offset: 0x000054F0
		[return: Dynamic]
		internal static dynamic GetMember(WebGridRow row, string name)
		{
			object result;
			if (row.TryGetMember(name, out result))
			{
				return result;
			}
			throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, HelpersResources.WebGrid_ColumnNotFound, new object[]
			{
				name
			}));
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000732C File Offset: 0x0000552C
		internal string GetPath(NameValueCollection queryString, params string[] exclusions)
		{
			NameValueCollection nameValueCollection = new NameValueCollection(this.QueryString);
			if (nameValueCollection.AllKeys.Contains(this.PageFieldName))
			{
				nameValueCollection.Set(this.PageFieldName, ((long)this.PageIndex + 1L).ToString(CultureInfo.CurrentCulture));
			}
			if (nameValueCollection.AllKeys.Contains(this.SelectionFieldName))
			{
				if (this.SelectedIndex < 0)
				{
					nameValueCollection.Remove(this.SelectionFieldName);
				}
				else
				{
					nameValueCollection.Set(this.SelectionFieldName, ((long)this.SelectedIndex + 1L).ToString(CultureInfo.CurrentCulture));
				}
			}
			if (nameValueCollection.AllKeys.Contains(this.SortFieldName))
			{
				if (string.IsNullOrEmpty(this.SortColumn))
				{
					nameValueCollection.Remove(this.SortFieldName);
				}
				else
				{
					nameValueCollection.Set(this.SortFieldName, this.SortColumn);
				}
			}
			if (nameValueCollection.AllKeys.Contains(this.SortDirectionFieldName))
			{
				nameValueCollection.Set(this.SortDirectionFieldName, WebGrid.GetSortDirectionString(this.SortDirection));
			}
			foreach (string name in exclusions)
			{
				nameValueCollection.Remove(name);
			}
			foreach (object obj in queryString.Keys)
			{
				string name2 = (string)obj;
				nameValueCollection.Set(name2, queryString[name2]);
			}
			queryString = nameValueCollection;
			StringBuilder stringBuilder = new StringBuilder(this.HttpContext.Request.Path);
			stringBuilder.Append("?");
			for (int j = 0; j < queryString.Count; j++)
			{
				if (j > 0)
				{
					stringBuilder.Append("&");
				}
				stringBuilder.Append(HttpUtility.UrlEncode(queryString.Keys[j]));
				stringBuilder.Append("=");
				stringBuilder.Append(HttpUtility.UrlEncode(queryString[j]));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000753C File Offset: 0x0000573C
		internal static string GetSortDirectionString(SortDirection sortDir)
		{
			if (sortDir != SortDirection.Ascending)
			{
				return "DESC";
			}
			return "ASC";
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000754C File Offset: 0x0000574C
		private void EnsureDataBound()
		{
			if (!this._dataSourceBound)
			{
				throw new InvalidOperationException(HelpersResources.WebGrid_NoDataSourceBound);
			}
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00007561 File Offset: 0x00005761
		private void EnsureDataSourceNotMaterialized()
		{
			if (this._dataSourceMaterialized)
			{
				throw new InvalidOperationException(HelpersResources.WebGrid_PropertySetterNotSupportedAfterDataBound);
			}
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00007578 File Offset: 0x00005778
		private void ValidatePreDataBoundValues()
		{
			if (this._canPage && this._pageIndexSet && this.PageIndex > this.PageCount)
			{
				this.PageIndex = this.PageCount;
				return;
			}
			if (this._canSort && this._sortColumnSet && !this.ValidateSortColumn(this.SortColumn))
			{
				this.SortColumn = this._defaultSort;
			}
		}

		// Token: 0x06000179 RID: 377 RVA: 0x000075DC File Offset: 0x000057DC
		private bool ValidateSortColumn(string value)
		{
			return this._sortColumnExplicitlySet || string.IsNullOrEmpty(value) || StringComparer.OrdinalIgnoreCase.Equals(this._defaultSort, value) || this.ColumnNames.Contains(value, StringComparer.OrdinalIgnoreCase) || value.Contains('.');
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00007650 File Offset: 0x00005850
		private static IEnumerable<string> GetDefaultColumnNames(IEnumerable<dynamic> source, Type elementType)
		{
			IDynamicMetaObjectProvider dynamicMetaObjectProvider = source.FirstOrDefault<object>() as IDynamicMetaObjectProvider;
			if (dynamicMetaObjectProvider != null)
			{
				return DynamicHelper.GetMemberNames(dynamicMetaObjectProvider);
			}
			return (from p in elementType.GetProperties()
			where WebGrid.IsBindableType(p.PropertyType) && p.GetIndexParameters().Length == 0
			select p.Name).OrderBy((string n) => n, StringComparer.OrdinalIgnoreCase).ToArray<string>();
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00007710 File Offset: 0x00005910
		private IEnumerable<WebGridColumn> GetDefaultColumns(IEnumerable<string> exclusions)
		{
			IEnumerable<string> enumerable = this.ColumnNames;
			if (exclusions != null)
			{
				enumerable = enumerable.Except(exclusions);
			}
			return (from n in enumerable
			select new WebGridColumn
			{
				ColumnName = n,
				CanSort = true
			}).ToArray<WebGridColumn>();
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00007758 File Offset: 0x00005958
		private static bool IsBindableType(Type type)
		{
			Type underlyingType = Nullable.GetUnderlyingType(type);
			if (underlyingType != null)
			{
				type = underlyingType;
			}
			return type.IsPrimitive || type.Equals(typeof(string)) || type.Equals(typeof(DateTime)) || type.Equals(typeof(decimal)) || type.Equals(typeof(Guid)) || type.Equals(typeof(DateTimeOffset)) || type.Equals(typeof(TimeSpan));
		}

		// Token: 0x0600017D RID: 381 RVA: 0x000077EC File Offset: 0x000059EC
		private static bool ModeEnabled(WebGridPagerModes mode, WebGridPagerModes modeCheck)
		{
			return (mode & modeCheck) == modeCheck;
		}

		// Token: 0x04000059 RID: 89
		private const string AjaxUpdateScript = "$({1}).swhgLoad({0},{1}{2});";

		// Token: 0x0400005A RID: 90
		private readonly HttpContextBase _context;

		// Token: 0x0400005B RID: 91
		private readonly bool _canPage;

		// Token: 0x0400005C RID: 92
		private readonly bool _canSort;

		// Token: 0x0400005D RID: 93
		private readonly string _ajaxUpdateContainerId;

		// Token: 0x0400005E RID: 94
		private readonly string _ajaxUpdateCallback;

		// Token: 0x0400005F RID: 95
		private readonly string _defaultSort;

		// Token: 0x04000060 RID: 96
		private readonly string _pageFieldName = "page";

		// Token: 0x04000061 RID: 97
		private readonly string _sortDirectionFieldName = "sortdir";

		// Token: 0x04000062 RID: 98
		private readonly string _selectionFieldName = "row";

		// Token: 0x04000063 RID: 99
		private readonly string _sortFieldName = "sort";

		// Token: 0x04000064 RID: 100
		private readonly string _fieldNamePrefix;

		// Token: 0x04000065 RID: 101
		private int _pageIndex = -1;

		// Token: 0x04000066 RID: 102
		private bool _pageIndexSet;

		// Token: 0x04000067 RID: 103
		private int _rowsPerPage;

		// Token: 0x04000068 RID: 104
		private int _selectedIndex = -1;

		// Token: 0x04000069 RID: 105
		private bool _selectedIndexSet;

		// Token: 0x0400006A RID: 106
		private string _sortColumn;

		// Token: 0x0400006B RID: 107
		private bool _sortColumnSet;

		// Token: 0x0400006C RID: 108
		private bool _sortColumnExplicitlySet;

		// Token: 0x0400006D RID: 109
		private SortDirection _sortDirection;

		// Token: 0x0400006E RID: 110
		private bool _sortDirectionSet;

		// Token: 0x0400006F RID: 111
		private IWebGridDataSource _dataSource;

		// Token: 0x04000070 RID: 112
		private bool _dataSourceBound;

		// Token: 0x04000071 RID: 113
		private bool _dataSourceMaterialized;

		// Token: 0x04000072 RID: 114
		private IEnumerable<string> _columnNames;

		// Token: 0x04000073 RID: 115
		private Type _elementType;

		// Token: 0x04000074 RID: 116
		private IList<WebGridRow> _rows;
	}
}
