using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x020003CD RID: 973
	internal class ObjectComplexPropertyMapping : ObjectPropertyMapping
	{
		// Token: 0x06002375 RID: 9077 RVA: 0x000A5253 File Offset: 0x000A3453
		internal ObjectComplexPropertyMapping(EdmProperty edmProperty, EdmProperty clrProperty) : base(edmProperty, clrProperty)
		{
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06002376 RID: 9078 RVA: 0x000A525D File Offset: 0x000A345D
		internal override MemberMappingKind MemberMappingKind
		{
			get
			{
				return MemberMappingKind.ComplexPropertyMapping;
			}
		}
	}
}
