using System;
using System.Collections;
using System.Diagnostics;
using System.Xml;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000042 RID: 66
	[DebuggerDisplay("Position={CurrentPosition}, Current={debuggerDisplayProxy, nq}")]
	internal class XPathArrayIterator : ResetableIterator
	{
		// Token: 0x060001FF RID: 511 RVA: 0x00007E6E File Offset: 0x0000606E
		public XPathArrayIterator(IList list)
		{
			this.list = list;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00007E7D File Offset: 0x0000607D
		public XPathArrayIterator(XPathArrayIterator it)
		{
			this.list = it.list;
			this.index = it.index;
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00007E9D File Offset: 0x0000609D
		public XPathArrayIterator(XPathNodeIterator nodeIterator)
		{
			this.list = new ArrayList();
			while (nodeIterator.MoveNext())
			{
				XPathNavigator xpathNavigator = nodeIterator.Current;
				this.list.Add(xpathNavigator.Clone());
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000202 RID: 514 RVA: 0x00007ED1 File Offset: 0x000060D1
		public IList AsList
		{
			get
			{
				return this.list;
			}
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00007ED9 File Offset: 0x000060D9
		public override XPathNodeIterator Clone()
		{
			return new XPathArrayIterator(this);
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000204 RID: 516 RVA: 0x00007EE4 File Offset: 0x000060E4
		public override XPathNavigator Current
		{
			get
			{
				if (this.index < 1)
				{
					throw new InvalidOperationException(Res.GetString("Sch_EnumNotStarted", new object[]
					{
						string.Empty
					}));
				}
				return (XPathNavigator)this.list[this.index - 1];
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000205 RID: 517 RVA: 0x00007F30 File Offset: 0x00006130
		public override int CurrentPosition
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000206 RID: 518 RVA: 0x00007F38 File Offset: 0x00006138
		public override int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00007F45 File Offset: 0x00006145
		public override bool MoveNext()
		{
			if (this.index == this.list.Count)
			{
				return false;
			}
			this.index++;
			return true;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00007F6B File Offset: 0x0000616B
		public override void Reset()
		{
			this.index = 0;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00007F74 File Offset: 0x00006174
		public override IEnumerator GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600020A RID: 522 RVA: 0x00007F81 File Offset: 0x00006181
		private object debuggerDisplayProxy
		{
			get
			{
				if (this.index >= 1)
				{
					return new XPathNavigator.DebuggerDisplayProxy(this.Current);
				}
				return null;
			}
		}

		// Token: 0x040000D5 RID: 213
		protected IList list;

		// Token: 0x040000D6 RID: 214
		protected int index;
	}
}
