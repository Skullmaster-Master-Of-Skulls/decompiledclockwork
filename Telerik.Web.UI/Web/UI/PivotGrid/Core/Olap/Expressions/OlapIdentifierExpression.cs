using System;

namespace Telerik.Web.UI.PivotGrid.Core.Olap.Expressions
{
	// Token: 0x02000702 RID: 1794
	internal class OlapIdentifierExpression : OlapExpression
	{
		// Token: 0x06003FBB RID: 16315 RVA: 0x000C9C7C File Offset: 0x000C7E7C
		internal OlapIdentifierExpression(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			this.DelmitIdentifier = true;
			this.name = name;
		}

		// Token: 0x06003FBC RID: 16316 RVA: 0x000C9CA5 File Offset: 0x000C7EA5
		internal OlapIdentifierExpression(string name, bool delimitIdentifier) : this(name)
		{
			this.DelmitIdentifier = delimitIdentifier;
		}

		// Token: 0x170014C0 RID: 5312
		// (get) Token: 0x06003FBD RID: 16317 RVA: 0x000C9CB5 File Offset: 0x000C7EB5
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170014C1 RID: 5313
		// (get) Token: 0x06003FBE RID: 16318 RVA: 0x000C9CBD File Offset: 0x000C7EBD
		// (set) Token: 0x06003FBF RID: 16319 RVA: 0x000C9CC5 File Offset: 0x000C7EC5
		public bool DelmitIdentifier { get; private set; }

		// Token: 0x170014C2 RID: 5314
		// (get) Token: 0x06003FC0 RID: 16320 RVA: 0x000C9CCE File Offset: 0x000C7ECE
		public override OlapExpressionType NodeType
		{
			get
			{
				return OlapExpressionType.Identifier;
			}
		}

		// Token: 0x06003FC1 RID: 16321 RVA: 0x000C9CD1 File Offset: 0x000C7ED1
		protected internal override OlapExpression Accept(OlapExpressionVisitor visitor)
		{
			return visitor.VisitIdentifier(this);
		}

		// Token: 0x040010EC RID: 4332
		private readonly string name;
	}
}
