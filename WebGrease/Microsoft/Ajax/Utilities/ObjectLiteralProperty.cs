using System;
using System.Collections.Generic;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x020000BA RID: 186
	public class ObjectLiteralProperty : AstNode
	{
		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000C35 RID: 3125 RVA: 0x00037C77 File Offset: 0x00035E77
		// (set) Token: 0x06000C36 RID: 3126 RVA: 0x00037CBF File Offset: 0x00035EBF
		public ObjectLiteralField Name
		{
			get
			{
				return this.m_propertyName;
			}
			set
			{
				this.m_propertyName.IfNotNull((ObjectLiteralField n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
				this.m_propertyName = value;
				this.m_propertyName.IfNotNull(delegate(ObjectLiteralField n)
				{
					n.Parent = this;
					return this;
				});
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000C37 RID: 3127 RVA: 0x00037CF8 File Offset: 0x00035EF8
		// (set) Token: 0x06000C38 RID: 3128 RVA: 0x00037D3F File Offset: 0x00035F3F
		public AstNode Value
		{
			get
			{
				return this.m_propertyValue;
			}
			set
			{
				this.m_propertyValue.IfNotNull((AstNode n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
				this.m_propertyValue = value;
				this.m_propertyValue.IfNotNull(delegate(AstNode n)
				{
					n.Parent = this;
					return this;
				});
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000C39 RID: 3129 RVA: 0x00037D78 File Offset: 0x00035F78
		public override bool IsConstant
		{
			get
			{
				return this.Value == null || this.Value.IsConstant;
			}
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x00037D8F File Offset: 0x00035F8F
		public ObjectLiteralProperty(Context context) : base(context)
		{
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x00037D98 File Offset: 0x00035F98
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000C3C RID: 3132 RVA: 0x00037DA4 File Offset: 0x00035FA4
		public override IEnumerable<AstNode> Children
		{
			get
			{
				return AstNode.EnumerateNonNullNodes(this.Name, this.Value, null, null);
			}
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x00037DBC File Offset: 0x00035FBC
		public override bool ReplaceChild(AstNode oldNode, AstNode newNode)
		{
			if (this.Name == oldNode)
			{
				ObjectLiteralField objectLiteralField = newNode as ObjectLiteralField;
				if (newNode == null || objectLiteralField != null)
				{
					this.Name = objectLiteralField;
				}
				return true;
			}
			if (this.Value == oldNode)
			{
				this.Value = newNode;
				return true;
			}
			return false;
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x00037E03 File Offset: 0x00036003
		internal override string GetFunctionGuess(AstNode target)
		{
			return this.Name.IfNotNull((ObjectLiteralField n) => n.ToString());
		}

		// Token: 0x040004D9 RID: 1241
		private ObjectLiteralField m_propertyName;

		// Token: 0x040004DA RID: 1242
		private AstNode m_propertyValue;
	}
}
