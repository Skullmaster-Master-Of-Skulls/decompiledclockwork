using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200014C RID: 332
	internal sealed class NamespaceQuery : BaseAxisQuery
	{
		// Token: 0x06001286 RID: 4742 RVA: 0x00050B7C File Offset: 0x0004FB7C
		public NamespaceQuery(Query qyParent, string Name, string Prefix, XPathNodeType Type) : base(qyParent, Name, Prefix, Type)
		{
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x00050B89 File Offset: 0x0004FB89
		private NamespaceQuery(NamespaceQuery other) : base(other)
		{
			this.onNamespace = other.onNamespace;
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x00050B9E File Offset: 0x0004FB9E
		public override void Reset()
		{
			this.onNamespace = false;
			base.Reset();
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x00050BB0 File Offset: 0x0004FBB0
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

		// Token: 0x0600128A RID: 4746 RVA: 0x00050C46 File Offset: 0x0004FC46
		public override bool matches(XPathNavigator e)
		{
			return e.Value.Length != 0 && (!base.NameTest || base.Name.Equals(e.LocalName));
		}

		// Token: 0x0600128B RID: 4747 RVA: 0x00050C72 File Offset: 0x0004FC72
		public override XPathNodeIterator Clone()
		{
			return new NamespaceQuery(this);
		}

		// Token: 0x04000B9C RID: 2972
		private bool onNamespace;
	}
}
