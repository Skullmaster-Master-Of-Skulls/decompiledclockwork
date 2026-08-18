using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200015F RID: 351
	internal sealed class XPathSortComparer : IComparer<SortKey>
	{
		// Token: 0x06001305 RID: 4869 RVA: 0x00052718 File Offset: 0x00051718
		public XPathSortComparer(int size)
		{
			if (size <= 0)
			{
				size = 3;
			}
			this.expressions = new Query[size];
			this.comparers = new IComparer[size];
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x0005273F File Offset: 0x0005173F
		public XPathSortComparer() : this(3)
		{
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x00052748 File Offset: 0x00051748
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

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06001308 RID: 4872 RVA: 0x00052806 File Offset: 0x00051806
		public int NumSorts
		{
			get
			{
				return this.numSorts;
			}
		}

		// Token: 0x06001309 RID: 4873 RVA: 0x0005280E File Offset: 0x0005180E
		public Query Expression(int i)
		{
			return this.expressions[i];
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x00052818 File Offset: 0x00051818
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

		// Token: 0x0600130B RID: 4875 RVA: 0x00052868 File Offset: 0x00051868
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

		// Token: 0x04000BDB RID: 3035
		private const int minSize = 3;

		// Token: 0x04000BDC RID: 3036
		private Query[] expressions;

		// Token: 0x04000BDD RID: 3037
		private IComparer[] comparers;

		// Token: 0x04000BDE RID: 3038
		private int numSorts;
	}
}
