using System;
using System.Collections.Generic;
using System.Data.Common.Utils;
using System.Data.Common.Utils.Boolean;
using System.Linq;

namespace System.Data.Mapping.ViewGeneration.Structures
{
	// Token: 0x0200029D RID: 669
	internal abstract class TrueFalseLiteral : BoolLiteral
	{
		// Token: 0x060027D2 RID: 10194 RVA: 0x0009A5D4 File Offset: 0x000987D4
		internal override BoolExpr<DomainConstraint<BoolLiteral, Constant>> GetDomainBoolExpression(MemberDomainMap domainMap)
		{
			IEnumerable<Constant> elements = new Constant[]
			{
				new ScalarConstant(true)
			};
			IEnumerable<Constant> elements2 = new Constant[]
			{
				new ScalarConstant(true),
				new ScalarConstant(false)
			};
			Set<Constant> domain = new Set<Constant>(elements2, Constant.EqualityComparer).MakeReadOnly();
			Set<Constant> range = new Set<Constant>(elements, Constant.EqualityComparer).MakeReadOnly();
			return BoolLiteral.MakeTermExpression(this, domain, range);
		}

		// Token: 0x060027D3 RID: 10195 RVA: 0x0009A648 File Offset: 0x00098848
		internal override BoolExpr<DomainConstraint<BoolLiteral, Constant>> FixRange(Set<Constant> range, MemberDomainMap memberDomainMap)
		{
			ScalarConstant scalarConstant = (ScalarConstant)range.First<Constant>();
			BoolExpr<DomainConstraint<BoolLiteral, Constant>> boolExpr = this.GetDomainBoolExpression(memberDomainMap);
			if (!(bool)scalarConstant.Value)
			{
				boolExpr = new NotExpr<DomainConstraint<BoolLiteral, Constant>>(boolExpr);
			}
			return boolExpr;
		}
	}
}
