using System;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000524 RID: 1316
	internal class TargetPerspective : Perspective
	{
		// Token: 0x0600319D RID: 12701 RVA: 0x000ED7A8 File Offset: 0x000EB9A8
		internal TargetPerspective(MetadataWorkspace metadataWorkspace) : base(metadataWorkspace, DataSpace.SSpace)
		{
			this._modelPerspective = new ModelPerspective(metadataWorkspace);
		}

		// Token: 0x0600319E RID: 12702 RVA: 0x000ED7C0 File Offset: 0x000EB9C0
		internal override bool TryGetTypeByName(string fullName, bool ignoreCase, out TypeUsage usage)
		{
			Check.NotEmpty(fullName, "fullName");
			EdmType edmType = null;
			if (base.MetadataWorkspace.TryGetItem<EdmType>(fullName, ignoreCase, base.TargetDataspace, out edmType))
			{
				usage = TypeUsage.Create(edmType);
				usage = Helper.GetModelTypeUsage(usage);
				return true;
			}
			return this._modelPerspective.TryGetTypeByName(fullName, ignoreCase, out usage);
		}

		// Token: 0x0600319F RID: 12703 RVA: 0x000ED813 File Offset: 0x000EBA13
		internal override bool TryGetEntityContainer(string name, bool ignoreCase, out EntityContainer entityContainer)
		{
			return base.TryGetEntityContainer(name, ignoreCase, out entityContainer) || this._modelPerspective.TryGetEntityContainer(name, ignoreCase, out entityContainer);
		}

		// Token: 0x040012C0 RID: 4800
		internal const DataSpace TargetPerspectiveDataSpace = DataSpace.SSpace;

		// Token: 0x040012C1 RID: 4801
		private readonly ModelPerspective _modelPerspective;
	}
}
