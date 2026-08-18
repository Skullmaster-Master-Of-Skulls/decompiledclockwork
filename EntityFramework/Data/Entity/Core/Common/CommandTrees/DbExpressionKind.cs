using System;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x02000111 RID: 273
	public enum DbExpressionKind
	{
		// Token: 0x0400020A RID: 522
		All,
		// Token: 0x0400020B RID: 523
		And,
		// Token: 0x0400020C RID: 524
		Any,
		// Token: 0x0400020D RID: 525
		Case,
		// Token: 0x0400020E RID: 526
		Cast,
		// Token: 0x0400020F RID: 527
		Constant,
		// Token: 0x04000210 RID: 528
		CrossApply,
		// Token: 0x04000211 RID: 529
		CrossJoin,
		// Token: 0x04000212 RID: 530
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Deref")]
		Deref,
		// Token: 0x04000213 RID: 531
		Distinct,
		// Token: 0x04000214 RID: 532
		Divide,
		// Token: 0x04000215 RID: 533
		Element,
		// Token: 0x04000216 RID: 534
		EntityRef,
		// Token: 0x04000217 RID: 535
		Equals,
		// Token: 0x04000218 RID: 536
		Except,
		// Token: 0x04000219 RID: 537
		Filter,
		// Token: 0x0400021A RID: 538
		FullOuterJoin,
		// Token: 0x0400021B RID: 539
		Function,
		// Token: 0x0400021C RID: 540
		GreaterThan,
		// Token: 0x0400021D RID: 541
		GreaterThanOrEquals,
		// Token: 0x0400021E RID: 542
		GroupBy,
		// Token: 0x0400021F RID: 543
		InnerJoin,
		// Token: 0x04000220 RID: 544
		Intersect,
		// Token: 0x04000221 RID: 545
		IsEmpty,
		// Token: 0x04000222 RID: 546
		IsNull,
		// Token: 0x04000223 RID: 547
		IsOf,
		// Token: 0x04000224 RID: 548
		IsOfOnly,
		// Token: 0x04000225 RID: 549
		LeftOuterJoin,
		// Token: 0x04000226 RID: 550
		LessThan,
		// Token: 0x04000227 RID: 551
		LessThanOrEquals,
		// Token: 0x04000228 RID: 552
		Like,
		// Token: 0x04000229 RID: 553
		Limit,
		// Token: 0x0400022A RID: 554
		Minus,
		// Token: 0x0400022B RID: 555
		Modulo,
		// Token: 0x0400022C RID: 556
		Multiply,
		// Token: 0x0400022D RID: 557
		NewInstance,
		// Token: 0x0400022E RID: 558
		Not,
		// Token: 0x0400022F RID: 559
		NotEquals,
		// Token: 0x04000230 RID: 560
		Null,
		// Token: 0x04000231 RID: 561
		OfType,
		// Token: 0x04000232 RID: 562
		OfTypeOnly,
		// Token: 0x04000233 RID: 563
		Or,
		// Token: 0x04000234 RID: 564
		OuterApply,
		// Token: 0x04000235 RID: 565
		ParameterReference,
		// Token: 0x04000236 RID: 566
		Plus,
		// Token: 0x04000237 RID: 567
		Project,
		// Token: 0x04000238 RID: 568
		Property,
		// Token: 0x04000239 RID: 569
		Ref,
		// Token: 0x0400023A RID: 570
		RefKey,
		// Token: 0x0400023B RID: 571
		RelationshipNavigation,
		// Token: 0x0400023C RID: 572
		Scan,
		// Token: 0x0400023D RID: 573
		Skip,
		// Token: 0x0400023E RID: 574
		Sort,
		// Token: 0x0400023F RID: 575
		Treat,
		// Token: 0x04000240 RID: 576
		UnaryMinus,
		// Token: 0x04000241 RID: 577
		UnionAll,
		// Token: 0x04000242 RID: 578
		VariableReference,
		// Token: 0x04000243 RID: 579
		Lambda,
		// Token: 0x04000244 RID: 580
		In
	}
}
