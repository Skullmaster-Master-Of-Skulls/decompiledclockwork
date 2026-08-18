using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020001D0 RID: 464
	public interface IStoreModelConvention<T> : IConvention where T : MetadataItem
	{
		// Token: 0x06000F63 RID: 3939
		void Apply(T item, DbModel model);
	}
}
