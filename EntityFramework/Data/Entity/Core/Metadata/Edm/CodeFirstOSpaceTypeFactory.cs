using System;
using System.Collections.Generic;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020001FB RID: 507
	internal class CodeFirstOSpaceTypeFactory : OSpaceTypeFactory
	{
		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060011CF RID: 4559 RVA: 0x0004C847 File Offset: 0x0004AA47
		public override List<Action> ReferenceResolutions
		{
			get
			{
				return this._referenceResolutions;
			}
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x0004C84F File Offset: 0x0004AA4F
		public override void LogLoadMessage(string message, EdmType relatedType)
		{
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x0004C851 File Offset: 0x0004AA51
		public override void LogError(string errorMessage, EdmType relatedType)
		{
			throw new MetadataException(Strings.InvalidSchemaEncountered(errorMessage));
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x0004C85E File Offset: 0x0004AA5E
		public override void TrackClosure(Type type)
		{
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060011D3 RID: 4563 RVA: 0x0004C860 File Offset: 0x0004AA60
		public override Dictionary<EdmType, EdmType> CspaceToOspace
		{
			get
			{
				return this._cspaceToOspace;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060011D4 RID: 4564 RVA: 0x0004C868 File Offset: 0x0004AA68
		public override Dictionary<string, EdmType> LoadedTypes
		{
			get
			{
				return this._loadedTypes;
			}
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x0004C870 File Offset: 0x0004AA70
		public override void AddToTypesInAssembly(EdmType type)
		{
		}

		// Token: 0x04000554 RID: 1364
		private readonly List<Action> _referenceResolutions = new List<Action>();

		// Token: 0x04000555 RID: 1365
		private readonly Dictionary<EdmType, EdmType> _cspaceToOspace = new Dictionary<EdmType, EdmType>();

		// Token: 0x04000556 RID: 1366
		private readonly Dictionary<string, EdmType> _loadedTypes = new Dictionary<string, EdmType>();
	}
}
