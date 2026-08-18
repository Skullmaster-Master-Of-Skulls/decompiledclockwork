using System;
using System.Collections.Generic;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x02000017 RID: 23
	public class ImportExportSpecifier : AstNode, INameDeclaration
	{
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00004190 File Offset: 0x00002390
		// (set) Token: 0x06000180 RID: 384 RVA: 0x00004198 File Offset: 0x00002398
		public Context NameContext { get; set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000181 RID: 385 RVA: 0x000041A1 File Offset: 0x000023A1
		// (set) Token: 0x06000182 RID: 386 RVA: 0x000041A9 File Offset: 0x000023A9
		public string ExternalName { get; set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000183 RID: 387 RVA: 0x000041B2 File Offset: 0x000023B2
		// (set) Token: 0x06000184 RID: 388 RVA: 0x000041BA File Offset: 0x000023BA
		public Context AsContext { get; set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000185 RID: 389 RVA: 0x000041C3 File Offset: 0x000023C3
		// (set) Token: 0x06000186 RID: 390 RVA: 0x0000420B File Offset: 0x0000240B
		public AstNode LocalIdentifier
		{
			get
			{
				return this.m_localIdentifier;
			}
			set
			{
				this.m_localIdentifier.IfNotNull((AstNode n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
				this.m_localIdentifier = value;
				this.m_localIdentifier.IfNotNull(delegate(AstNode n)
				{
					n.Parent = this;
					return this;
				});
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00004244 File Offset: 0x00002444
		public string Name
		{
			get
			{
				return this.ExternalName;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000188 RID: 392 RVA: 0x0000424C File Offset: 0x0000244C
		public AstNode Initializer
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000189 RID: 393 RVA: 0x0000424F File Offset: 0x0000244F
		public bool IsParameter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00004252 File Offset: 0x00002452
		public bool RenameNotAllowed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00004255 File Offset: 0x00002455
		// (set) Token: 0x0600018C RID: 396 RVA: 0x0000425D File Offset: 0x0000245D
		public JSVariableField VariableField { get; set; }

		// Token: 0x0600018D RID: 397 RVA: 0x00004266 File Offset: 0x00002466
		public ImportExportSpecifier(Context context) : base(context)
		{
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000426F File Offset: 0x0000246F
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600018F RID: 399 RVA: 0x0000427B File Offset: 0x0000247B
		public override IEnumerable<AstNode> Children
		{
			get
			{
				return AstNode.EnumerateNonNullNodes(this.m_localIdentifier, null, null, null);
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000428B File Offset: 0x0000248B
		public override bool ReplaceChild(AstNode oldNode, AstNode newNode)
		{
			if (this.LocalIdentifier == oldNode)
			{
				this.LocalIdentifier = newNode;
				return true;
			}
			return false;
		}

		// Token: 0x04000044 RID: 68
		private AstNode m_localIdentifier;
	}
}
