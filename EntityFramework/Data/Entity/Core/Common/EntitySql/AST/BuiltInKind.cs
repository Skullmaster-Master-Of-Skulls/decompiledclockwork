using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x02000216 RID: 534
	internal enum BuiltInKind
	{
		// Token: 0x040005AF RID: 1455
		And,
		// Token: 0x040005B0 RID: 1456
		Or,
		// Token: 0x040005B1 RID: 1457
		Not,
		// Token: 0x040005B2 RID: 1458
		Cast,
		// Token: 0x040005B3 RID: 1459
		OfType,
		// Token: 0x040005B4 RID: 1460
		Treat,
		// Token: 0x040005B5 RID: 1461
		IsOf,
		// Token: 0x040005B6 RID: 1462
		Union,
		// Token: 0x040005B7 RID: 1463
		UnionAll,
		// Token: 0x040005B8 RID: 1464
		Intersect,
		// Token: 0x040005B9 RID: 1465
		Overlaps,
		// Token: 0x040005BA RID: 1466
		AnyElement,
		// Token: 0x040005BB RID: 1467
		Element,
		// Token: 0x040005BC RID: 1468
		Except,
		// Token: 0x040005BD RID: 1469
		Exists,
		// Token: 0x040005BE RID: 1470
		Flatten,
		// Token: 0x040005BF RID: 1471
		In,
		// Token: 0x040005C0 RID: 1472
		NotIn,
		// Token: 0x040005C1 RID: 1473
		Distinct,
		// Token: 0x040005C2 RID: 1474
		IsNull,
		// Token: 0x040005C3 RID: 1475
		IsNotNull,
		// Token: 0x040005C4 RID: 1476
		Like,
		// Token: 0x040005C5 RID: 1477
		Equal,
		// Token: 0x040005C6 RID: 1478
		NotEqual,
		// Token: 0x040005C7 RID: 1479
		LessEqual,
		// Token: 0x040005C8 RID: 1480
		LessThan,
		// Token: 0x040005C9 RID: 1481
		GreaterThan,
		// Token: 0x040005CA RID: 1482
		GreaterEqual,
		// Token: 0x040005CB RID: 1483
		Plus,
		// Token: 0x040005CC RID: 1484
		Minus,
		// Token: 0x040005CD RID: 1485
		Multiply,
		// Token: 0x040005CE RID: 1486
		Divide,
		// Token: 0x040005CF RID: 1487
		Modulus,
		// Token: 0x040005D0 RID: 1488
		UnaryMinus,
		// Token: 0x040005D1 RID: 1489
		UnaryPlus,
		// Token: 0x040005D2 RID: 1490
		Between,
		// Token: 0x040005D3 RID: 1491
		NotBetween
	}
}
