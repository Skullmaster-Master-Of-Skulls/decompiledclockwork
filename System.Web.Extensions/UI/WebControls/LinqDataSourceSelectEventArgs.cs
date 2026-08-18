using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x020000A2 RID: 162
	public class LinqDataSourceSelectEventArgs : CancelEventArgs
	{
		// Token: 0x06000720 RID: 1824 RVA: 0x0001CF9F File Offset: 0x0001B19F
		public LinqDataSourceSelectEventArgs(DataSourceSelectArguments arguments, IDictionary<string, object> whereParameters, IOrderedDictionary orderByParameters, IDictionary<string, object> groupByParameters, IDictionary<string, object> orderGroupsByParameters, IDictionary<string, object> selectParameters)
		{
			this._arguments = arguments;
			this._groupByParameters = groupByParameters;
			this._orderByParameters = orderByParameters;
			this._orderGroupsByParameters = orderGroupsByParameters;
			this._selectParameters = selectParameters;
			this._whereParameters = whereParameters;
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000721 RID: 1825 RVA: 0x0001CFD4 File Offset: 0x0001B1D4
		public DataSourceSelectArguments Arguments
		{
			get
			{
				return this._arguments;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000722 RID: 1826 RVA: 0x0001CFDC File Offset: 0x0001B1DC
		public IDictionary<string, object> GroupByParameters
		{
			get
			{
				return this._groupByParameters;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000723 RID: 1827 RVA: 0x0001CFE4 File Offset: 0x0001B1E4
		public IOrderedDictionary OrderByParameters
		{
			get
			{
				return this._orderByParameters;
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000724 RID: 1828 RVA: 0x0001CFEC File Offset: 0x0001B1EC
		public IDictionary<string, object> OrderGroupsByParameters
		{
			get
			{
				return this._orderGroupsByParameters;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000725 RID: 1829 RVA: 0x0001CFF4 File Offset: 0x0001B1F4
		// (set) Token: 0x06000726 RID: 1830 RVA: 0x0001CFFC File Offset: 0x0001B1FC
		public object Result
		{
			get
			{
				return this._result;
			}
			set
			{
				this._result = value;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000727 RID: 1831 RVA: 0x0001D005 File Offset: 0x0001B205
		public IDictionary<string, object> SelectParameters
		{
			get
			{
				return this._selectParameters;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000728 RID: 1832 RVA: 0x0001D00D File Offset: 0x0001B20D
		public IDictionary<string, object> WhereParameters
		{
			get
			{
				return this._whereParameters;
			}
		}

		// Token: 0x04000263 RID: 611
		private DataSourceSelectArguments _arguments;

		// Token: 0x04000264 RID: 612
		private IDictionary<string, object> _groupByParameters;

		// Token: 0x04000265 RID: 613
		private IOrderedDictionary _orderByParameters;

		// Token: 0x04000266 RID: 614
		private IDictionary<string, object> _orderGroupsByParameters;

		// Token: 0x04000267 RID: 615
		private object _result;

		// Token: 0x04000268 RID: 616
		private IDictionary<string, object> _selectParameters;

		// Token: 0x04000269 RID: 617
		private IDictionary<string, object> _whereParameters;
	}
}
