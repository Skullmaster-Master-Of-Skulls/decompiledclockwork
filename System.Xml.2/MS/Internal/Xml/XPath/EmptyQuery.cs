using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000019 RID: 25
	internal sealed class EmptyQuery : Query
	{
		// Token: 0x0600009C RID: 156 RVA: 0x0000369B File Offset: 0x0000189B
		public override XPathNavigator Advance()
		{
			return null;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x0000369E File Offset: 0x0000189E
		public override XPathNodeIterator Clone()
		{
			return this;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000036A1 File Offset: 0x000018A1
		public override object Evaluate(XPathNodeIterator context)
		{
			return this;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600009F RID: 159 RVA: 0x000036A4 File Offset: 0x000018A4
		public override int CurrentPosition
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x000036A7 File Offset: 0x000018A7
		public override int Count
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x000036AA File Offset: 0x000018AA
		public override QueryProps Properties
		{
			get
			{
				return (QueryProps)23;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x000036AE File Offset: 0x000018AE
		public override XPathResultType StaticType
		{
			get
			{
				return XPathResultType.NodeSet;
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000036B1 File Offset: 0x000018B1
		public override void Reset()
		{
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x000036B3 File Offset: 0x000018B3
		public override XPathNavigator Current
		{
			get
			{
				return null;
			}
		}
	}
}
