using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200002A RID: 42
	internal sealed class NamespaceQuery : BaseAxisQuery
	{
		// Token: 0x0600013E RID: 318 RVA: 0x00005524 File Offset: 0x00003724
		public NamespaceQuery(Query qyParent, string Name, string Prefix, XPathNodeType Type) : base(qyParent, Name, Prefix, Type)
		{
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00005531 File Offset: 0x00003731
		private NamespaceQuery(NamespaceQuery other) : base(other)
		{
			this.onNamespace = other.onNamespace;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00005546 File Offset: 0x00003746
		public override void Reset()
		{
			this.onNamespace = false;
			base.Reset();
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00005558 File Offset: 0x00003758
		public override XPathNavigator Advance()
		{
			for (;;)
			{
				if (!this.onNamespace)
				{
					this.currentNode = this.qyInput.Advance();
					if (this.currentNode == null)
					{
						break;
					}
					this.position = 0;
					this.currentNode = this.currentNode.Clone();
					this.onNamespace = this.currentNode.MoveToFirstNamespace();
				}
				else
				{
					this.onNamespace = this.currentNode.MoveToNextNamespace();
				}
				if (this.onNamespace && this.matches(this.currentNode))
				{
					goto Block_3;
				}
			}
			return null;
			Block_3:
			this.position++;
			return this.currentNode;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x000055EE File Offset: 0x000037EE
		public override bool matches(XPathNavigator e)
		{
			return e.Value.Length != 0 && (!base.NameTest || base.Name.Equals(e.LocalName));
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000561A File Offset: 0x0000381A
		public override XPathNodeIterator Clone()
		{
			return new NamespaceQuery(this);
		}

		// Token: 0x040000A3 RID: 163
		private bool onNamespace;
	}
}
