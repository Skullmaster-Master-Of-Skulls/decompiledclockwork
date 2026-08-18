using System;
using System.Data.Metadata.Edm;

namespace System.Data.Mapping
{
	// Token: 0x02000235 RID: 565
	internal class ObjectComplexPropertyMapping : ObjectPropertyMapping
	{
		// Token: 0x06002405 RID: 9221 RVA: 0x00082733 File Offset: 0x00080933
		internal ObjectComplexPropertyMapping(EdmProperty edmProperty, EdmProperty clrProperty, ObjectTypeMapping complexTypeMapping) : base(edmProperty, clrProperty)
		{
			this.m_objectTypeMapping = complexTypeMapping;
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06002406 RID: 9222 RVA: 0x0003BF8C File Offset: 0x0003A18C
		internal override MemberMappingKind MemberMappingKind
		{
			get
			{
				return MemberMappingKind.ComplexPropertyMapping;
			}
		}

		// Token: 0x04000FF4 RID: 4084
		private readonly ObjectTypeMapping m_objectTypeMapping;
	}
}
