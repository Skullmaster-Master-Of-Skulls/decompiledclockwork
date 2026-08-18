using System;

namespace Telerik.Web.UI.PivotGrid.Core.Olap.Expressions
{
	// Token: 0x02000703 RID: 1795
	internal class OlapMemberFuntionExpression : OlapExpression
	{
		// Token: 0x06003FC2 RID: 16322 RVA: 0x000C9CDA File Offset: 0x000C7EDA
		internal OlapMemberFuntionExpression(string name, OlapExpression member)
		{
			if (member == null)
			{
				throw new ArgumentNullException("member");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			this.Name = name;
			this.Member = member;
		}

		// Token: 0x170014C3 RID: 5315
		// (get) Token: 0x06003FC3 RID: 16323 RVA: 0x000C9D11 File Offset: 0x000C7F11
		// (set) Token: 0x06003FC4 RID: 16324 RVA: 0x000C9D19 File Offset: 0x000C7F19
		public OlapExpression Member { get; private set; }

		// Token: 0x170014C4 RID: 5316
		// (get) Token: 0x06003FC5 RID: 16325 RVA: 0x000C9D22 File Offset: 0x000C7F22
		public override OlapExpressionType NodeType
		{
			get
			{
				return OlapExpressionType.MemberFunction;
			}
		}

		// Token: 0x170014C5 RID: 5317
		// (get) Token: 0x06003FC6 RID: 16326 RVA: 0x000C9D25 File Offset: 0x000C7F25
		// (set) Token: 0x06003FC7 RID: 16327 RVA: 0x000C9D2D File Offset: 0x000C7F2D
		public string Name { get; private set; }

		// Token: 0x06003FC8 RID: 16328 RVA: 0x000C9D36 File Offset: 0x000C7F36
		protected internal override OlapExpression Accept(OlapExpressionVisitor visitor)
		{
			return visitor.VisitMemberFunction(this);
		}
	}
}
