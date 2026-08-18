using System;
using System.Collections;

namespace System.Web.UI.WebControls.Expressions
{
	// Token: 0x020000CC RID: 204
	public class DataSourceExpressionCollection : StateManagedCollection
	{
		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000A1A RID: 2586 RVA: 0x00026079 File Offset: 0x00024279
		// (set) Token: 0x06000A1B RID: 2587 RVA: 0x00026081 File Offset: 0x00024281
		public HttpContext Context { get; private set; }

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000A1C RID: 2588 RVA: 0x0002608A File Offset: 0x0002428A
		// (set) Token: 0x06000A1D RID: 2589 RVA: 0x00026092 File Offset: 0x00024292
		public Control Owner { get; private set; }

		// Token: 0x170002F1 RID: 753
		public DataSourceExpression this[int index]
		{
			get
			{
				return (DataSourceExpression)((IList)this)[index];
			}
			set
			{
				((IList)this)[index] = value;
			}
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x000260B4 File Offset: 0x000242B4
		internal void SetContext(Control owner, HttpContext context, IQueryableDataSource dataSource)
		{
			this.Owner = owner;
			this.Context = context;
			this._dataSource = dataSource;
			foreach (object obj in this)
			{
				DataSourceExpression dataSourceExpression = (DataSourceExpression)obj;
				dataSourceExpression.SetContext(owner, context, this._dataSource);
			}
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0001C20A File Offset: 0x0001A40A
		public void Add(DataSourceExpression expression)
		{
			((IList)this).Add(expression);
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x00026124 File Offset: 0x00024324
		protected override object CreateKnownType(int index)
		{
			switch (index)
			{
			case 0:
				return new SearchExpression();
			case 1:
				return new MethodExpression();
			case 2:
				return new OrderByExpression();
			case 3:
				return new RangeExpression();
			case 4:
				return new PropertyExpression();
			case 5:
				return new CustomExpression();
			default:
				throw new ArgumentOutOfRangeException("index");
			}
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x0002617F File Offset: 0x0002437F
		public void CopyTo(DataSourceExpression[] expressionArray, int index)
		{
			base.CopyTo(expressionArray, index);
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x00026189 File Offset: 0x00024389
		public void Contains(DataSourceExpression expression)
		{
			((IList)this).Contains(expression);
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x00026193 File Offset: 0x00024393
		protected override Type[] GetKnownTypes()
		{
			return DataSourceExpressionCollection.knownTypes;
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x0001C2C4 File Offset: 0x0001A4C4
		public int IndexOf(DataSourceExpression expression)
		{
			return ((IList)this).IndexOf(expression);
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x0001C2CD File Offset: 0x0001A4CD
		public void Insert(int index, DataSourceExpression expression)
		{
			((IList)this).Insert(index, expression);
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0001C38C File Offset: 0x0001A58C
		public void Remove(DataSourceExpression expression)
		{
			((IList)this).Remove(expression);
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x0001C383 File Offset: 0x0001A583
		public void RemoveAt(int index)
		{
			((IList)this).RemoveAt(index);
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x0002619A File Offset: 0x0002439A
		protected override void SetDirtyObject(object o)
		{
			((DataSourceExpression)o).SetDirty();
		}

		// Token: 0x0400034A RID: 842
		private IQueryableDataSource _dataSource;

		// Token: 0x0400034B RID: 843
		private static readonly Type[] knownTypes = new Type[]
		{
			typeof(SearchExpression),
			typeof(MethodExpression),
			typeof(OrderByExpression),
			typeof(RangeExpression),
			typeof(PropertyExpression),
			typeof(CustomExpression)
		};
	}
}
