using System;

namespace System.Data.Metadata.Edm
{
	// Token: 0x0200020B RID: 523
	internal class TargetPerspective : Perspective
	{
		// Token: 0x060022A4 RID: 8868 RVA: 0x0007B280 File Offset: 0x00079480
		internal TargetPerspective(MetadataWorkspace metadataWorkspace) : base(metadataWorkspace, DataSpace.SSpace)
		{
			this._modelPerspective = new ModelPerspective(metadataWorkspace);
		}

		// Token: 0x060022A5 RID: 8869 RVA: 0x0007B298 File Offset: 0x00079498
		internal override bool TryGetTypeByName(string fullName, bool ignoreCase, out TypeUsage usage)
		{
			EntityUtil.CheckStringArgument(fullName, "fullName");
			EdmType edmType = null;
			if (base.MetadataWorkspace.TryGetItem<EdmType>(fullName, ignoreCase, base.TargetDataspace, out edmType))
			{
				usage = TypeUsage.Create(edmType);
				usage = Helper.GetModelTypeUsage(usage);
				return true;
			}
			return this._modelPerspective.TryGetTypeByName(fullName, ignoreCase, out usage);
		}

		// Token: 0x060022A6 RID: 8870 RVA: 0x0007B2EA File Offset: 0x000794EA
		internal override bool TryGetEntityContainer(string name, bool ignoreCase, out EntityContainer entityContainer)
		{
			return base.TryGetEntityContainer(name, ignoreCase, out entityContainer) || this._modelPerspective.TryGetEntityContainer(name, ignoreCase, out entityContainer);
		}

		// Token: 0x04000EFC RID: 3836
		internal const DataSpace TargetPerspectiveDataSpace = DataSpace.SSpace;

		// Token: 0x04000EFD RID: 3837
		private ModelPerspective _modelPerspective;
	}
}
