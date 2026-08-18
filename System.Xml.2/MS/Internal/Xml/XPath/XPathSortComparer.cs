using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200003B RID: 59
	internal sealed class XPathSortComparer : IComparer<SortKey>
	{
		// Token: 0x060001C5 RID: 453 RVA: 0x00007168 File Offset: 0x00005368
		public XPathSortComparer(int size)
		{
			if (size <= 0)
			{
				size = 3;
			}
			this.expressions = new Query[size];
			this.comparers = new IComparer[size];
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000718F File Offset: 0x0000538F
		public XPathSortComparer() : this(3)
		{
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00007198 File Offset: 0x00005398
		public void AddSort(Query evalQuery, IComparer comparer)
		{
			if (this.numSorts == this.expressions.Length)
			{
				Query[] array = new Query[this.numSorts * 2];
				IComparer[] array2 = new IComparer[this.numSorts * 2];
				for (int i = 0; i < this.numSorts; i++)
				{
					array[i] = this.expressions[i];
					array2[i] = this.comparers[i];
				}
				this.expressions = array;
				this.comparers = array2;
			}
			if (evalQuery.StaticType == XPathResultType.NodeSet || evalQuery.StaticType == XPathResultType.Any)
			{
				evalQuery = new StringFunctions(Function.FunctionType.FuncString, new Query[]
				{
					evalQuery
				});
			}
			this.expressions[this.numSorts] = evalQuery;
			this.comparers[this.numSorts] = comparer;
			this.numSorts++;
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00007254 File Offset: 0x00005454
		public int NumSorts
		{
			get
			{
				return this.numSorts;
			}
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000725C File Offset: 0x0000545C
		public Query Expression(int i)
		{
			return this.expressions[i];
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00007268 File Offset: 0x00005468
		int IComparer<SortKey>.Compare(SortKey x, SortKey y)
		{
			for (int i = 0; i < x.NumKeys; i++)
			{
				int num = this.comparers[i].Compare(x[i], y[i]);
				if (num != 0)
				{
					return num;
				}
			}
			return x.OriginalPosition - y.OriginalPosition;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x000072B8 File Offset: 0x000054B8
		internal XPathSortComparer Clone()
		{
			XPathSortComparer xpathSortComparer = new XPathSortComparer(this.numSorts);
			for (int i = 0; i < this.numSorts; i++)
			{
				xpathSortComparer.comparers[i] = this.comparers[i];
				xpathSortComparer.expressions[i] = (Query)this.expressions[i].Clone();
			}
			xpathSortComparer.numSorts = this.numSorts;
			return xpathSortComparer;
		}

		// Token: 0x040000CA RID: 202
		private const int minSize = 3;

		// Token: 0x040000CB RID: 203
		private Query[] expressions;

		// Token: 0x040000CC RID: 204
		private IComparer[] comparers;

		// Token: 0x040000CD RID: 205
		private int numSorts;
	}
}
