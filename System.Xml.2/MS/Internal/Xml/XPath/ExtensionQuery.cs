using System;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200001A RID: 26
	internal abstract class ExtensionQuery : Query
	{
		// Token: 0x060000A6 RID: 166 RVA: 0x000036BE File Offset: 0x000018BE
		public ExtensionQuery(string prefix, string name)
		{
			this.prefix = prefix;
			this.name = name;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000036D4 File Offset: 0x000018D4
		protected ExtensionQuery(ExtensionQuery other) : base(other)
		{
			this.prefix = other.prefix;
			this.name = other.name;
			this.xsltContext = other.xsltContext;
			this.queryIterator = (ResetableIterator)Query.Clone(other.queryIterator);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003722 File Offset: 0x00001922
		public override void Reset()
		{
			if (this.queryIterator != null)
			{
				this.queryIterator.Reset();
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00003737 File Offset: 0x00001937
		public override XPathNavigator Current
		{
			get
			{
				if (this.queryIterator == null)
				{
					throw XPathException.Create("Xp_NodeSetExpected");
				}
				if (this.queryIterator.CurrentPosition == 0)
				{
					this.Advance();
				}
				return this.queryIterator.Current;
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x0000376B File Offset: 0x0000196B
		public override XPathNavigator Advance()
		{
			if (this.queryIterator == null)
			{
				throw XPathException.Create("Xp_NodeSetExpected");
			}
			if (this.queryIterator.MoveNext())
			{
				return this.queryIterator.Current;
			}
			return null;
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000AB RID: 171 RVA: 0x0000379A File Offset: 0x0000199A
		public override int CurrentPosition
		{
			get
			{
				if (this.queryIterator != null)
				{
					return this.queryIterator.CurrentPosition;
				}
				return 0;
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000037B4 File Offset: 0x000019B4
		protected object ProcessResult(object value)
		{
			if (value is string)
			{
				return value;
			}
			if (value is double)
			{
				return value;
			}
			if (value is bool)
			{
				return value;
			}
			if (value is XPathNavigator)
			{
				return value;
			}
			if (value is int)
			{
				return (double)((int)value);
			}
			if (value == null)
			{
				this.queryIterator = XPathEmptyIterator.Instance;
				return this;
			}
			ResetableIterator resetableIterator = value as ResetableIterator;
			if (resetableIterator != null)
			{
				this.queryIterator = (ResetableIterator)resetableIterator.Clone();
				return this;
			}
			XPathNodeIterator xpathNodeIterator = value as XPathNodeIterator;
			if (xpathNodeIterator != null)
			{
				this.queryIterator = new XPathArrayIterator(xpathNodeIterator);
				return this;
			}
			IXPathNavigable ixpathNavigable = value as IXPathNavigable;
			if (ixpathNavigable != null)
			{
				return ixpathNavigable.CreateNavigator();
			}
			if (value is short)
			{
				return (double)((short)value);
			}
			if (value is long)
			{
				return (double)((long)value);
			}
			if (value is uint)
			{
				return (uint)value;
			}
			if (value is ushort)
			{
				return (double)((ushort)value);
			}
			if (value is ulong)
			{
				return (ulong)value;
			}
			if (value is float)
			{
				return (double)((float)value);
			}
			if (value is decimal)
			{
				return (double)((decimal)value);
			}
			return value.ToString();
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000AD RID: 173 RVA: 0x000038F4 File Offset: 0x00001AF4
		protected string QName
		{
			get
			{
				if (this.prefix.Length == 0)
				{
					return this.name;
				}
				return this.prefix + ":" + this.name;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00003920 File Offset: 0x00001B20
		public override int Count
		{
			get
			{
				if (this.queryIterator != null)
				{
					return this.queryIterator.Count;
				}
				return 1;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00003937 File Offset: 0x00001B37
		public override XPathResultType StaticType
		{
			get
			{
				return XPathResultType.Any;
			}
		}

		// Token: 0x0400007D RID: 125
		protected string prefix;

		// Token: 0x0400007E RID: 126
		protected string name;

		// Token: 0x0400007F RID: 127
		protected XsltContext xsltContext;

		// Token: 0x04000080 RID: 128
		private ResetableIterator queryIterator;
	}
}
