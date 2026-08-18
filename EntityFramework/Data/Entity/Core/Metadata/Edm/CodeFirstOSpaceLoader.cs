using System;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Linq;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020001F9 RID: 505
	internal class CodeFirstOSpaceLoader
	{
		// Token: 0x060011A8 RID: 4520 RVA: 0x0004B484 File Offset: 0x00049684
		public CodeFirstOSpaceLoader(CodeFirstOSpaceTypeFactory typeFactory = null)
		{
			this._typeFactory = (typeFactory ?? new CodeFirstOSpaceTypeFactory());
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x0004B4C0 File Offset: 0x000496C0
		public void LoadTypes(EdmItemCollection edmItemCollection, ObjectItemCollection objectItemCollection)
		{
			foreach (EdmType edmType in from t in edmItemCollection.OfType<EdmType>()
			where t.BuiltInTypeKind == BuiltInTypeKind.EntityType || t.BuiltInTypeKind == BuiltInTypeKind.EnumType || t.BuiltInTypeKind == BuiltInTypeKind.ComplexType
			select t)
			{
				Type clrType = edmType.GetClrType();
				if (clrType != null)
				{
					EdmType edmType2 = this._typeFactory.TryCreateType(clrType, edmType);
					if (edmType2 != null)
					{
						this._typeFactory.CspaceToOspace.Add(edmType, edmType2);
					}
				}
			}
			this._typeFactory.CreateRelationships(edmItemCollection);
			foreach (Action action in this._typeFactory.ReferenceResolutions)
			{
				action();
			}
			foreach (EdmType edmType3 in this._typeFactory.LoadedTypes.Values)
			{
				edmType3.SetReadOnly();
			}
			objectItemCollection.AddLoadedTypes(this._typeFactory.LoadedTypes);
			objectItemCollection.OSpaceTypesLoaded = true;
		}

		// Token: 0x0400054B RID: 1355
		private readonly CodeFirstOSpaceTypeFactory _typeFactory;
	}
}
