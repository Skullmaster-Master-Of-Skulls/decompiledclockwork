using System;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020007CA RID: 1994
	public abstract class AssociationMappingConfiguration
	{
		// Token: 0x06005A90 RID: 23184
		internal abstract void Configure(AssociationSetMapping associationSetMapping, EdmModel database, PropertyInfo navigationProperty);

		// Token: 0x06005A91 RID: 23185
		internal abstract AssociationMappingConfiguration Clone();
	}
}
