using System;
using System.Collections.Generic;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x02000019 RID: 25
	public class InitializerNode : AstNode
	{
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000195 RID: 405 RVA: 0x000042B5 File Offset: 0x000024B5
		// (set) Token: 0x06000196 RID: 406 RVA: 0x000042FF File Offset: 0x000024FF
		public AstNode Binding
		{
			get
			{
				return this.m_binding;
			}
			set
			{
				this.m_binding.IfNotNull((AstNode n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
				this.m_binding = value;
				this.m_binding.IfNotNull(delegate(AstNode n)
				{
					n.Parent = this;
					return this;
				});
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00004338 File Offset: 0x00002538
		// (set) Token: 0x06000198 RID: 408 RVA: 0x00004340 File Offset: 0x00002540
		public Context AssignContext { get; set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00004349 File Offset: 0x00002549
		// (set) Token: 0x0600019A RID: 410 RVA: 0x00004393 File Offset: 0x00002593
		public AstNode Initializer
		{
			get
			{
				return this.m_initializer;
			}
			set
			{
				this.m_initializer.IfNotNull((AstNode n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
				this.m_initializer = value;
				this.m_initializer.IfNotNull(delegate(AstNode n)
				{
					n.Parent = this;
					return this;
				});
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600019B RID: 411 RVA: 0x000043D4 File Offset: 0x000025D4
		public override bool IsConstant
		{
			get
			{
				return this.Binding.IfNotNull((AstNode v) => v.IsConstant, true);
			}
		}

		// Token: 0x0600019C RID: 412 RVA: 0x000043FF File Offset: 0x000025FF
		public InitializerNode(Context context) : base(context)
		{
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00004408 File Offset: 0x00002608
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00004414 File Offset: 0x00002614
		public override IEnumerable<AstNode> Children
		{
			get
			{
				return AstNode.EnumerateNonNullNodes(this.Binding, this.Initializer, null, null);
			}
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00004429 File Offset: 0x00002629
		public override bool ReplaceChild(AstNode oldNode, AstNode newNode)
		{
			if (this.Binding == oldNode)
			{
				this.Binding = newNode;
				return true;
			}
			if (this.Initializer == oldNode)
			{
				this.Initializer = newNode;
				return true;
			}
			return false;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00004468 File Offset: 0x00002668
		internal override string GetFunctionGuess(AstNode target)
		{
			return this.Binding.IfNotNull((AstNode b) => b.GetFunctionGuess(target));
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000449C File Offset: 0x0000269C
		public override bool IsEquivalentTo(AstNode otherNode)
		{
			bool result = true;
			IList<BindingIdentifier> list = BindingsVisitor.Bindings(this.Binding);
			foreach (Lookup otherNode2 in BindingsVisitor.References(otherNode))
			{
				bool flag = false;
				foreach (BindingIdentifier bindingIdentifier in list)
				{
					if (bindingIdentifier.IsEquivalentTo(otherNode2))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					result = false;
					break;
				}
			}
			return result;
		}

		// Token: 0x04000049 RID: 73
		private AstNode m_binding;

		// Token: 0x0400004A RID: 74
		private AstNode m_initializer;
	}
}
