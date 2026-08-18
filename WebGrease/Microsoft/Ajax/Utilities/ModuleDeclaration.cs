using System;
using System.Collections.Generic;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x0200001B RID: 27
	public class ModuleDeclaration : AstNode, IModuleReference
	{
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00004AB6 File Offset: 0x00002CB6
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x00004AFF File Offset: 0x00002CFF
		public BindingIdentifier Binding
		{
			get
			{
				return this.m_binding;
			}
			set
			{
				this.m_binding.IfNotNull((BindingIdentifier n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
				this.m_binding = value;
				this.m_binding.IfNotNull(delegate(BindingIdentifier n)
				{
					n.Parent = this;
					return this;
				});
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00004B38 File Offset: 0x00002D38
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x00004B40 File Offset: 0x00002D40
		public Context FromContext { get; set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00004B49 File Offset: 0x00002D49
		// (set) Token: 0x060001BB RID: 443 RVA: 0x00004B51 File Offset: 0x00002D51
		public string ModuleName { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00004B5A File Offset: 0x00002D5A
		// (set) Token: 0x060001BD RID: 445 RVA: 0x00004B62 File Offset: 0x00002D62
		public Context ModuleContext { get; set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00004B6B File Offset: 0x00002D6B
		// (set) Token: 0x060001BF RID: 447 RVA: 0x00004B73 File Offset: 0x00002D73
		public ModuleScope ReferencedModule { get; set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00004B7C File Offset: 0x00002D7C
		// (set) Token: 0x060001C1 RID: 449 RVA: 0x00004B84 File Offset: 0x00002D84
		public bool IsImplicit { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00004B8D File Offset: 0x00002D8D
		// (set) Token: 0x060001C3 RID: 451 RVA: 0x00004BD7 File Offset: 0x00002DD7
		public Block Body
		{
			get
			{
				return this.m_body;
			}
			set
			{
				this.m_body.IfNotNull((Block n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
				this.m_body = value;
				this.m_body.IfNotNull(delegate(Block n)
				{
					n.Parent = this;
					return this;
				});
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00004C10 File Offset: 0x00002E10
		public override bool IsDeclaration
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00004C13 File Offset: 0x00002E13
		public ModuleDeclaration(Context context) : base(context)
		{
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00004C1C File Offset: 0x00002E1C
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x00004C28 File Offset: 0x00002E28
		public override IEnumerable<AstNode> Children
		{
			get
			{
				return AstNode.EnumerateNonNullNodes(this.m_binding, this.m_body, null, null);
			}
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00004C48 File Offset: 0x00002E48
		public override bool ReplaceChild(AstNode oldNode, AstNode newNode)
		{
			if (this.Binding == oldNode)
			{
				return (newNode as BindingIdentifier).IfNotNull(delegate(BindingIdentifier b)
				{
					this.Binding = b;
					return true;
				});
			}
			if (this.Body == oldNode)
			{
				this.Body = AstNode.ForceToBlock(newNode);
				return true;
			}
			return false;
		}

		// Token: 0x04000050 RID: 80
		private BindingIdentifier m_binding;

		// Token: 0x04000051 RID: 81
		private Block m_body;
	}
}
