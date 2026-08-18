using System;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000139 RID: 313
	[Obsolete("ConceptualModel and StoreModel are now available as properties directly on DbModel.")]
	public static class DbModelExtensions
	{
		// Token: 0x06000A89 RID: 2697 RVA: 0x00035EFF File Offset: 0x000340FF
		[Obsolete("ConceptualModel is now available as a property directly on DbModel.")]
		public static EdmModel GetConceptualModel(this IEdmModelAdapter model)
		{
			Check.NotNull<IEdmModelAdapter>(model, "model");
			return model.ConceptualModel;
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x00035F13 File Offset: 0x00034113
		[Obsolete("StoreModel is now available as a property directly on DbModel.")]
		public static EdmModel GetStoreModel(this IEdmModelAdapter model)
		{
			Check.NotNull<IEdmModelAdapter>(model, "model");
			return model.StoreModel;
		}
	}
}
