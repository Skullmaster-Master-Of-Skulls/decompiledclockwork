using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Common.Utils.Boolean;
using System.Linq;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x0200044F RID: 1103
	internal abstract class TrueFalseLiteral : BoolLiteral
	{
		// Token: 0x06002890 RID: 10384 RVA: 0x000C5598 File Offset: 0x000C3798
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

		// Token: 0x06002891 RID: 10385 RVA: 0x000C5618 File Offset: 0x000C3818
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
