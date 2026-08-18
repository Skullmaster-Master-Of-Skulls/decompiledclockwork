using System;
using System.Collections;
using System.Diagnostics;
using System.Xml;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000166 RID: 358
	[DebuggerDisplay("Position={CurrentPosition}, Current={debuggerDisplayProxy, nq}")]
	internal class XPathArrayIterator : ResetableIterator
	{
		// Token: 0x0600133F RID: 4927 RVA: 0x0005356C File Offset: 0x0005256C
		public XPathArrayIterator(IList list)
		{
			this.list = list;
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x0005357B File Offset: 0x0005257B
		public XPathArrayIterator(XPathArrayIterator it)
		{
			this.list = it.list;
			this.index = it.index;
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x0005359B File Offset: 0x0005259B
		public XPathArrayIterator(XPathNodeIterator nodeIterator)
		{
			this.list = new ArrayList();
			while (nodeIterator.MoveNext())
			{
				XPathNavigator xpathNavigator = nodeIterator.Current;
				this.list.Add(xpathNavigator.Clone());
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06001342 RID: 4930 RVA: 0x000535CF File Offset: 0x000525CF
		public IList AsList
		{
			get
			{
				return this.list;
			}
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x000535D7 File Offset: 0x000525D7
		public override XPathNodeIterator Clone()
		{
			return new XPathArrayIterator(this);
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06001344 RID: 4932 RVA: 0x000535E0 File Offset: 0x000525E0
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

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06001345 RID: 4933 RVA: 0x0005362E File Offset: 0x0005262E
		public override int CurrentPosition
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06001346 RID: 4934 RVA: 0x00053636 File Offset: 0x00052636
		public override int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x00053643 File Offset: 0x00052643
		public override bool MoveNext()
		{
			if (this.index == this.list.Count)
			{
				return false;
			}
			this.index++;
			return true;
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x00053669 File Offset: 0x00052669
		public override void Reset()
		{
			this.index = 0;
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x00053672 File Offset: 0x00052672
		public override IEnumerator GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x0600134A RID: 4938 RVA: 0x0005367F File Offset: 0x0005267F
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

		// Token: 0x04000BED RID: 3053
		protected IList list;

		// Token: 0x04000BEE RID: 3054
		protected int index;
	}
}
