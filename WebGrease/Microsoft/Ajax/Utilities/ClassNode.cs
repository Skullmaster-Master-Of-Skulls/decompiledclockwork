using System;
using System.Collections.Generic;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x0200000D RID: 13
	public class ClassNode : AstNode
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000101 RID: 257 RVA: 0x000036EA File Offset: 0x000018EA
		// (set) Token: 0x06000102 RID: 258 RVA: 0x000036F2 File Offset: 0x000018F2
		public Context ClassContext { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000103 RID: 259 RVA: 0x000036FB File Offset: 0x000018FB
		// (set) Token: 0x06000104 RID: 260 RVA: 0x00003743 File Offset: 0x00001943
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

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000105 RID: 261 RVA: 0x0000377C File Offset: 0x0000197C
		// (set) Token: 0x06000106 RID: 262 RVA: 0x00003784 File Offset: 0x00001984
		public Context ExtendsContext { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000107 RID: 263 RVA: 0x0000378D File Offset: 0x0000198D
		// (set) Token: 0x06000108 RID: 264 RVA: 0x000037D7 File Offset: 0x000019D7
		public AstNode Heritage
		{
			get
			{
				return this.m_heritage;
			}
			set
			{
				this.m_heritage.IfNotNull((AstNode n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
				this.m_heritage = value;
				this.m_heritage.IfNotNull(delegate(AstNode n)
				{
					n.Parent = this;
					return this;
				});
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00003810 File Offset: 0x00001A10
		// (set) Token: 0x0600010A RID: 266 RVA: 0x00003818 File Offset: 0x00001A18
		public Context OpenBrace { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00003821 File Offset: 0x00001A21
		// (set) Token: 0x0600010C RID: 268 RVA: 0x0000386B File Offset: 0x00001A6B
		public AstNodeList Elements
		{
			get
			{
				return this.m_elements;
			}
			set
			{
				this.m_elements.IfNotNull((AstNodeList n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
				this.m_elements = value;
				this.m_elements.IfNotNull(delegate(AstNodeList n)
				{
					n.Parent = this;
					return this;
				});
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600010D RID: 269 RVA: 0x000038A4 File Offset: 0x00001AA4
		// (set) Token: 0x0600010E RID: 270 RVA: 0x000038AC File Offset: 0x00001AAC
		public Context CloseBrace { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600010F RID: 271 RVA: 0x000038B5 File Offset: 0x00001AB5
		// (set) Token: 0x06000110 RID: 272 RVA: 0x000038BD File Offset: 0x00001ABD
		public ClassType ClassType { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000111 RID: 273 RVA: 0x000038C6 File Offset: 0x00001AC6
		public override bool IsExpression
		{
			get
			{
				return this.ClassType != ClassType.Declaration;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000112 RID: 274 RVA: 0x000038D4 File Offset: 0x00001AD4
		// (set) Token: 0x06000113 RID: 275 RVA: 0x000038DC File Offset: 0x00001ADC
		public BlockScope Scope { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000114 RID: 276 RVA: 0x000038E5 File Offset: 0x00001AE5
		public override bool IsDeclaration
		{
			get
			{
				return this.ClassType == ClassType.Declaration;
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000038F0 File Offset: 0x00001AF0
		public ClassNode(Context context) : base(context)
		{
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000038F9 File Offset: 0x00001AF9
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000117 RID: 279 RVA: 0x00003905 File Offset: 0x00001B05
		public override IEnumerable<AstNode> Children
		{
			get
			{
				return AstNode.EnumerateNonNullNodes(this.m_binding, this.m_heritage, this.m_elements, null);
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00003920 File Offset: 0x00001B20
		public override bool ReplaceChild(AstNode oldNode, AstNode newNode)
		{
			if (this.Binding == oldNode)
			{
				this.Binding = (newNode as BindingIdentifier);
				return true;
			}
			if (this.Heritage == oldNode)
			{
				this.Heritage = newNode;
				return true;
			}
			if (this.Elements == oldNode)
			{
				this.Elements = (newNode as AstNodeList);
				return true;
			}
			return false;
		}

		// Token: 0x0400001F RID: 31
		private AstNode m_binding;

		// Token: 0x04000020 RID: 32
		private AstNode m_heritage;

		// Token: 0x04000021 RID: 33
		private AstNodeList m_elements;
	}
}
