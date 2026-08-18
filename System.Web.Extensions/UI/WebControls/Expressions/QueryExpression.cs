using System;
using System.Linq;

namespace System.Web.UI.WebControls.Expressions
{
	// Token: 0x020000D3 RID: 211
	[ParseChildren(true, "Expressions")]
	[PersistChildren(false)]
	public class QueryExpression
	{
		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000A63 RID: 2659 RVA: 0x00026D6C File Offset: 0x00024F6C
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public DataSourceExpressionCollection Expressions
		{
			get
			{
				if (this._expressions == null)
				{
					this._expressions = new DataSourceExpressionCollection();
				}
				return this._expressions;
			}
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x00026D87 File Offset: 0x00024F87
		public void Initialize(Control owner, HttpContext context, IQueryableDataSource dataSource)
		{
			this._owner = owner;
			this._context = context;
			this._dataSource = dataSource;
			this.Expressions.SetContext(owner, context, dataSource);
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x00026DAC File Offset: 0x00024FAC
		public virtual IQueryable GetQueryable(IQueryable source)
		{
			if (source == null)
			{
				return null;
			}
			foreach (object obj in this.Expressions)
			{
				DataSourceExpression dataSourceExpression = (DataSourceExpression)obj;
				source = (dataSourceExpression.GetQueryable(source) ?? source);
			}
			return source;
		}

		// Token: 0x04000358 RID: 856
		private HttpContext _context;

		// Token: 0x04000359 RID: 857
		private Control _owner;

		// Token: 0x0400035A RID: 858
		private IQueryableDataSource _dataSource;

		// Token: 0x0400035B RID: 859
		private DataSourceExpressionCollection _expressions;
	}
}
