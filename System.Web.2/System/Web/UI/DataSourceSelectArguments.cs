using System;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x0200027E RID: 638
	public sealed class DataSourceSelectArguments
	{
		// Token: 0x06001E24 RID: 7716 RVA: 0x0006136F File Offset: 0x0005F56F
		public DataSourceSelectArguments() : this(string.Empty, 0, 0)
		{
		}

		// Token: 0x06001E25 RID: 7717 RVA: 0x0006137E File Offset: 0x0005F57E
		public DataSourceSelectArguments(string sortExpression) : this(sortExpression, 0, 0)
		{
		}

		// Token: 0x06001E26 RID: 7718 RVA: 0x00061389 File Offset: 0x0005F589
		public DataSourceSelectArguments(int startRowIndex, int maximumRows) : this(string.Empty, startRowIndex, maximumRows)
		{
		}

		// Token: 0x06001E27 RID: 7719 RVA: 0x00061398 File Offset: 0x0005F598
		public DataSourceSelectArguments(string sortExpression, int startRowIndex, int maximumRows)
		{
			this.SortExpression = sortExpression;
			this.StartRowIndex = startRowIndex;
			this.MaximumRows = maximumRows;
		}

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x06001E28 RID: 7720 RVA: 0x000613BC File Offset: 0x0005F5BC
		public static DataSourceSelectArguments Empty
		{
			get
			{
				return new DataSourceSelectArguments();
			}
		}

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x06001E29 RID: 7721 RVA: 0x000613C3 File Offset: 0x0005F5C3
		// (set) Token: 0x06001E2A RID: 7722 RVA: 0x000613CB File Offset: 0x0005F5CB
		public int MaximumRows
		{
			get
			{
				return this._maximumRows;
			}
			set
			{
				if (value == 0)
				{
					if (this._startRowIndex == 0)
					{
						this._requestedCapabilities &= ~DataSourceCapabilities.Page;
					}
				}
				else
				{
					this._requestedCapabilities |= DataSourceCapabilities.Page;
				}
				this._maximumRows = value;
			}
		}

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x06001E2B RID: 7723 RVA: 0x000613FE File Offset: 0x0005F5FE
		// (set) Token: 0x06001E2C RID: 7724 RVA: 0x00061406 File Offset: 0x0005F606
		public bool RetrieveTotalRowCount
		{
			get
			{
				return this._retrieveTotalRowCount;
			}
			set
			{
				if (value)
				{
					this._requestedCapabilities |= DataSourceCapabilities.RetrieveTotalRowCount;
				}
				else
				{
					this._requestedCapabilities &= ~DataSourceCapabilities.RetrieveTotalRowCount;
				}
				this._retrieveTotalRowCount = value;
			}
		}

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x06001E2D RID: 7725 RVA: 0x00061431 File Offset: 0x0005F631
		// (set) Token: 0x06001E2E RID: 7726 RVA: 0x0006144C File Offset: 0x0005F64C
		public string SortExpression
		{
			get
			{
				if (this._sortExpression == null)
				{
					this._sortExpression = string.Empty;
				}
				return this._sortExpression;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					this._requestedCapabilities &= ~DataSourceCapabilities.Sort;
				}
				else
				{
					this._requestedCapabilities |= DataSourceCapabilities.Sort;
				}
				this._sortExpression = value;
			}
		}

		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x06001E2F RID: 7727 RVA: 0x0006147C File Offset: 0x0005F67C
		// (set) Token: 0x06001E30 RID: 7728 RVA: 0x00061484 File Offset: 0x0005F684
		public int StartRowIndex
		{
			get
			{
				return this._startRowIndex;
			}
			set
			{
				if (value == 0)
				{
					if (this._maximumRows == 0)
					{
						this._requestedCapabilities &= ~DataSourceCapabilities.Page;
					}
				}
				else
				{
					this._requestedCapabilities |= DataSourceCapabilities.Page;
				}
				this._startRowIndex = value;
			}
		}

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x06001E31 RID: 7729 RVA: 0x000614B7 File Offset: 0x0005F6B7
		// (set) Token: 0x06001E32 RID: 7730 RVA: 0x000614BF File Offset: 0x0005F6BF
		public int TotalRowCount
		{
			get
			{
				return this._totalRowCount;
			}
			set
			{
				this._totalRowCount = value;
			}
		}

		// Token: 0x06001E33 RID: 7731 RVA: 0x000614C8 File Offset: 0x0005F6C8
		public void AddSupportedCapabilities(DataSourceCapabilities capabilities)
		{
			this._supportedCapabilities |= capabilities;
		}

		// Token: 0x06001E34 RID: 7732 RVA: 0x000614D8 File Offset: 0x0005F6D8
		public override int GetHashCode()
		{
			return HashCodeCombiner.CombineHashCodes(this._maximumRows.GetHashCode(), this._retrieveTotalRowCount.GetHashCode(), this._sortExpression.GetHashCode(), this._startRowIndex.GetHashCode(), this._totalRowCount.GetHashCode());
		}

		// Token: 0x06001E35 RID: 7733 RVA: 0x00061518 File Offset: 0x0005F718
		public override bool Equals(object obj)
		{
			DataSourceSelectArguments dataSourceSelectArguments = obj as DataSourceSelectArguments;
			return dataSourceSelectArguments != null && (dataSourceSelectArguments.MaximumRows == this._maximumRows && dataSourceSelectArguments.RetrieveTotalRowCount == this._retrieveTotalRowCount && dataSourceSelectArguments.SortExpression == this._sortExpression && dataSourceSelectArguments.StartRowIndex == this._startRowIndex) && dataSourceSelectArguments.TotalRowCount == this._totalRowCount;
		}

		// Token: 0x06001E36 RID: 7734 RVA: 0x00061580 File Offset: 0x0005F780
		public void RaiseUnsupportedCapabilitiesError(DataSourceView view)
		{
			DataSourceCapabilities dataSourceCapabilities = this._requestedCapabilities & ~this._supportedCapabilities;
			if ((dataSourceCapabilities & DataSourceCapabilities.Sort) != DataSourceCapabilities.None)
			{
				view.RaiseUnsupportedCapabilityError(DataSourceCapabilities.Sort);
			}
			if ((dataSourceCapabilities & DataSourceCapabilities.Page) != DataSourceCapabilities.None)
			{
				view.RaiseUnsupportedCapabilityError(DataSourceCapabilities.Page);
			}
			if ((dataSourceCapabilities & DataSourceCapabilities.RetrieveTotalRowCount) != DataSourceCapabilities.None)
			{
				view.RaiseUnsupportedCapabilityError(DataSourceCapabilities.RetrieveTotalRowCount);
			}
		}

		// Token: 0x04001984 RID: 6532
		private DataSourceCapabilities _requestedCapabilities;

		// Token: 0x04001985 RID: 6533
		private DataSourceCapabilities _supportedCapabilities;

		// Token: 0x04001986 RID: 6534
		private int _maximumRows;

		// Token: 0x04001987 RID: 6535
		private bool _retrieveTotalRowCount;

		// Token: 0x04001988 RID: 6536
		private string _sortExpression;

		// Token: 0x04001989 RID: 6537
		private int _startRowIndex;

		// Token: 0x0400198A RID: 6538
		private int _totalRowCount = -1;
	}
}
