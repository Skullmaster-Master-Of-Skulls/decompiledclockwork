using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000039 RID: 57
	internal sealed class SortQuery : Query
	{
		// Token: 0x060001B0 RID: 432 RVA: 0x00006ED7 File Offset: 0x000050D7
		public SortQuery(Query qyInput)
		{
			this.results = new List<SortKey>();
			this.comparer = new XPathSortComparer();
			this.qyInput = qyInput;
			this.count = 0;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00006F04 File Offset: 0x00005104
		private SortQuery(SortQuery other) : base(other)
		{
			this.results = new List<SortKey>(other.results);
			this.comparer = other.comparer.Clone();
			this.qyInput = Query.Clone(other.qyInput);
			this.count = 0;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00006F52 File Offset: 0x00005152
		public override void Reset()
		{
			this.count = 0;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00006F5B File Offset: 0x0000515B
		public override void SetXsltContext(XsltContext xsltContext)
		{
			this.qyInput.SetXsltContext(xsltContext);
			if (this.qyInput.StaticType != XPathResultType.NodeSet && this.qyInput.StaticType != XPathResultType.Any)
			{
				throw XPathException.Create("Xp_NodeSetExpected");
			}
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00006F90 File Offset: 0x00005190
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

		// Token: 0x060001B5 RID: 437 RVA: 0x00007019 File Offset: 0x00005219
		public override object Evaluate(XPathNodeIterator context)
		{
			this.qyInput.Evaluate(context);
			this.results.Clear();
			this.BuildResultsList();
			this.count = 0;
			return this;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00007044 File Offset: 0x00005244
		public override XPathNavigator Advance()
		{
			if (this.count < this.results.Count)
			{
				List<SortKey> list = this.results;
				int count = this.count;
				this.count = count + 1;
				return list[count].Node;
			}
			return null;
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00007087 File Offset: 0x00005287
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

		// Token: 0x060001B8 RID: 440 RVA: 0x000070AB File Offset: 0x000052AB
		internal void AddSort(Query evalQuery, IComparer comparer)
		{
			this.comparer.AddSort(evalQuery, comparer);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x000070BA File Offset: 0x000052BA
		public override XPathNodeIterator Clone()
		{
			return new SortQuery(this);
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001BA RID: 442 RVA: 0x000070C2 File Offset: 0x000052C2
		public override XPathResultType StaticType
		{
			get
			{
				return XPathResultType.NodeSet;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001BB RID: 443 RVA: 0x000070C5 File Offset: 0x000052C5
		public override int CurrentPosition
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001BC RID: 444 RVA: 0x000070CD File Offset: 0x000052CD
		public override int Count
		{
			get
			{
				return this.results.Count;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001BD RID: 445 RVA: 0x000070DA File Offset: 0x000052DA
		public override QueryProps Properties
		{
			get
			{
				return (QueryProps)7;
			}
		}

		// Token: 0x060001BE RID: 446 RVA: 0x000070DD File Offset: 0x000052DD
		public override void PrintQuery(XmlWriter w)
		{
			w.WriteStartElement(base.GetType().Name);
			this.qyInput.PrintQuery(w);
			w.WriteElementString("XPathSortComparer", "... PrintTree() not implemented ...");
			w.WriteEndElement();
		}

		// Token: 0x040000C3 RID: 195
		private List<SortKey> results;

		// Token: 0x040000C4 RID: 196
		private XPathSortComparer comparer;

		// Token: 0x040000C5 RID: 197
		private Query qyInput;
	}
}
