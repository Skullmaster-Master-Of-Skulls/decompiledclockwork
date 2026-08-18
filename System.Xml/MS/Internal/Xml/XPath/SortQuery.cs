using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200015D RID: 349
	internal sealed class SortQuery : Query
	{
		// Token: 0x060012F0 RID: 4848 RVA: 0x00052488 File Offset: 0x00051488
		public SortQuery(Query qyInput)
		{
			this.results = new List<SortKey>();
			this.comparer = new XPathSortComparer();
			this.qyInput = qyInput;
			this.count = 0;
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x000524B4 File Offset: 0x000514B4
		private SortQuery(SortQuery other) : base(other)
		{
			this.results = new List<SortKey>(other.results);
			this.comparer = other.comparer.Clone();
			this.qyInput = Query.Clone(other.qyInput);
			this.count = 0;
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x00052502 File Offset: 0x00051502
		public override void Reset()
		{
			this.count = 0;
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x0005250B File Offset: 0x0005150B
		public override void SetXsltContext(XsltContext xsltContext)
		{
			this.qyInput.SetXsltContext(xsltContext);
			if (this.qyInput.StaticType != XPathResultType.NodeSet && this.qyInput.StaticType != XPathResultType.Any)
			{
				throw XPathException.Create("Xp_NodeSetExpected");
			}
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x00052540 File Offset: 0x00051540
		private void BuildResultsList()
		{
			int numSorts = this.comparer.NumSorts;
			XPathNavigator xpathNavigator;
			while ((xpathNavigator = this.qyInput.Advance()) != null)
			{
				SortKey sortKey = new SortKey(numSorts, this.results.Count, xpathNavigator.Clone());
				for (int i = 0; i < numSorts; i++)
				{
					sortKey[i] = this.comparer.Expression(i).Evaluate(this.qyInput);
				}
				this.results.Add(sortKey);
			}
			this.results.Sort(this.comparer);
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x000525C9 File Offset: 0x000515C9
		public override object Evaluate(XPathNodeIterator context)
		{
			this.qyInput.Evaluate(context);
			this.results.Clear();
			this.BuildResultsList();
			this.count = 0;
			return this;
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x000525F4 File Offset: 0x000515F4
		public override XPathNavigator Advance()
		{
			if (this.count < this.results.Count)
			{
				return this.results[this.count++].Node;
			}
			return null;
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x060012F7 RID: 4855 RVA: 0x00052637 File Offset: 0x00051637
		public override XPathNavigator Current
		{
			get
			{
				if (this.count == 0)
				{
					return null;
				}
				return this.results[this.count - 1].Node;
			}
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x0005265B File Offset: 0x0005165B
		internal void AddSort(Query evalQuery, IComparer comparer)
		{
			this.comparer.AddSort(evalQuery, comparer);
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x0005266A File Offset: 0x0005166A
		public override XPathNodeIterator Clone()
		{
			return new SortQuery(this);
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x060012FA RID: 4858 RVA: 0x00052672 File Offset: 0x00051672
		public override XPathResultType StaticType
		{
			get
			{
				return XPathResultType.NodeSet;
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x060012FB RID: 4859 RVA: 0x00052675 File Offset: 0x00051675
		public override int CurrentPosition
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x060012FC RID: 4860 RVA: 0x0005267D File Offset: 0x0005167D
		public override int Count
		{
			get
			{
				return this.results.Count;
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x060012FD RID: 4861 RVA: 0x0005268A File Offset: 0x0005168A
		public override QueryProps Properties
		{
			get
			{
				return (QueryProps)7;
			}
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x0005268D File Offset: 0x0005168D
		public override void PrintQuery(XmlWriter w)
		{
			w.WriteStartElement(base.GetType().Name);
			this.qyInput.PrintQuery(w);
			w.WriteElementString("XPathSortComparer", "... PrintTree() not implemented ...");
			w.WriteEndElement();
		}

		// Token: 0x04000BD4 RID: 3028
		private List<SortKey> results;

		// Token: 0x04000BD5 RID: 3029
		private XPathSortComparer comparer;

		// Token: 0x04000BD6 RID: 3030
		private Query qyInput;
	}
}
