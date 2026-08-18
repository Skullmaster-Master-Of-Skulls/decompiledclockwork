using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020001D2 RID: 466
	public interface IConceptualModelConvention<T> : IConvention where T : MetadataItem
	{
		// Token: 0x06000F68 RID: 3944
		void Apply(T item, DbModel model);
	}
}
