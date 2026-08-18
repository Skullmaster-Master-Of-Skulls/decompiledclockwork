using System;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x0200013A RID: 314
	[Obsolete("ConceptualModel and StoreModel are now available as properties directly on DbModel.")]
	public interface IEdmModelAdapter
	{
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000A8B RID: 2699
		[Obsolete("ConceptualModel is now available as a property directly on DbModel.")]
		EdmModel ConceptualModel { get; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000A8C RID: 2700
		[Obsolete("StoreModel is now available as a property directly on DbModel.")]
		EdmModel StoreModel { get; }
	}
}
