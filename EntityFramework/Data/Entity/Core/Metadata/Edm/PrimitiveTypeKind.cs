using System;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004F6 RID: 1270
	public enum PrimitiveTypeKind
	{
		// Token: 0x040011F3 RID: 4595
		Binary,
		// Token: 0x040011F4 RID: 4596
		Boolean,
		// Token: 0x040011F5 RID: 4597
		Byte,
		// Token: 0x040011F6 RID: 4598
		DateTime,
		// Token: 0x040011F7 RID: 4599
		Decimal,
		// Token: 0x040011F8 RID: 4600
		Double,
		// Token: 0x040011F9 RID: 4601
		Guid,
		// Token: 0x040011FA RID: 4602
		Single,
		// Token: 0x040011FB RID: 4603
		SByte,
		// Token: 0x040011FC RID: 4604
		Int16,
		// Token: 0x040011FD RID: 4605
		Int32,
		// Token: 0x040011FE RID: 4606
		Int64,
		// Token: 0x040011FF RID: 4607
		String,
		// Token: 0x04001200 RID: 4608
		Time,
		// Token: 0x04001201 RID: 4609
		DateTimeOffset,
		// Token: 0x04001202 RID: 4610
		Geometry,
		// Token: 0x04001203 RID: 4611
		Geography,
		// Token: 0x04001204 RID: 4612
		GeometryPoint,
		// Token: 0x04001205 RID: 4613
		GeometryLineString,
		// Token: 0x04001206 RID: 4614
		GeometryPolygon,
		// Token: 0x04001207 RID: 4615
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi")]
		[SuppressMessage("Microsoft.Naming", "CA1702", MessageId = "MultiPoint")]
		GeometryMultiPoint,
		// Token: 0x04001208 RID: 4616
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi")]
		[SuppressMessage("Microsoft.Naming", "CA1702", MessageId = "MultiLine")]
		GeometryMultiLineString,
		// Token: 0x04001209 RID: 4617
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi")]
		GeometryMultiPolygon,
		// Token: 0x0400120A RID: 4618
		GeometryCollection,
		// Token: 0x0400120B RID: 4619
		GeographyPoint,
		// Token: 0x0400120C RID: 4620
		GeographyLineString,
		// Token: 0x0400120D RID: 4621
		GeographyPolygon,
		// Token: 0x0400120E RID: 4622
		[SuppressMessage("Microsoft.Naming", "CA1702", MessageId = "MultiPoint")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi")]
		GeographyMultiPoint,
		// Token: 0x0400120F RID: 4623
		[SuppressMessage("Microsoft.Naming", "CA1702", MessageId = "MultiLine")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi")]
		GeographyMultiLineString,
		// Token: 0x04001210 RID: 4624
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi")]
		GeographyMultiPolygon,
		// Token: 0x04001211 RID: 4625
		GeographyCollection
	}
}
