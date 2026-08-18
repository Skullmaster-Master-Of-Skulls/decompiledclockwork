using System;
using System.Collections.Generic;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x020000BD RID: 189
	public sealed class ParameterDeclaration : AstNode
	{
		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000CC2 RID: 3266 RVA: 0x0003D356 File Offset: 0x0003B556
		// (set) Token: 0x06000CC3 RID: 3267 RVA: 0x0003D35E File Offset: 0x0003B55E
		public int Position { get; set; }

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000CC4 RID: 3268 RVA: 0x0003D367 File Offset: 0x0003B567
		// (set) Token: 0x06000CC5 RID: 3269 RVA: 0x0003D36F File Offset: 0x0003B56F
		public bool HasRest { get; set; }

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000CC6 RID: 3270 RVA: 0x0003D378 File Offset: 0x0003B578
		// (set) Token: 0x06000CC7 RID: 3271 RVA: 0x0003D380 File Offset: 0x0003B580
		public Context RestContext { get; set; }

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000CC8 RID: 3272 RVA: 0x0003D389 File Offset: 0x0003B589
		// (set) Token: 0x06000CC9 RID: 3273 RVA: 0x0003D3D3 File Offset: 0x0003B5D3
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

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000CCA RID: 3274 RVA: 0x0003D40C File Offset: 0x0003B60C
		// (set) Token: 0x06000CCB RID: 3275 RVA: 0x0003D414 File Offset: 0x0003B614
		public Context AssignContext { get; set; }

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000CCC RID: 3276 RVA: 0x0003D41D File Offset: 0x0003B61D
		// (set) Token: 0x06000CCD RID: 3277 RVA: 0x0003D467 File Offset: 0x0003B667
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

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000CCE RID: 3278 RVA: 0x0003D4A0 File Offset: 0x0003B6A0
		public bool IsReferenced
		{
			get
			{
				foreach (BindingIdentifier bindingIdentifier in BindingsVisitor.Bindings(this))
				{
					if (bindingIdentifier.VariableField == null || bindingIdentifier.VariableField.IsReferenced)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x0003D504 File Offset: 0x0003B704
		public ParameterDeclaration(Context context) : base(context)
		{
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x0003D50D File Offset: 0x0003B70D
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x0003D530 File Offset: 0x0003B730
		internal override string GetFunctionGuess(AstNode target)
		{
			return this.Binding.IfNotNull((AstNode b) => b.GetFunctionGuess(target));
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000CD2 RID: 3282 RVA: 0x0003D561 File Offset: 0x0003B761
		public override IEnumerable<AstNode> Children
		{
			get
			{
				return AstNode.EnumerateNonNullNodes(this.Binding, this.Initializer, null, null);
			}
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x0003D576 File Offset: 0x0003B776
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

		// Token: 0x04000511 RID: 1297
		private AstNode m_binding;

		// Token: 0x04000512 RID: 1298
		private AstNode m_initializer;
	}
}
