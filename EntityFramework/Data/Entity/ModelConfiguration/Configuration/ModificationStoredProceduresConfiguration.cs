using System;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Mapping;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020002C0 RID: 704
	internal class ModificationStoredProceduresConfiguration
	{
		// Token: 0x060018EC RID: 6380 RVA: 0x0007BA15 File Offset: 0x00079C15
		public ModificationStoredProceduresConfiguration()
		{
		}

		// Token: 0x060018ED RID: 6381 RVA: 0x0007BA20 File Offset: 0x00079C20
		private ModificationStoredProceduresConfiguration(ModificationStoredProceduresConfiguration source)
		{
			if (source._insertModificationStoredProcedureConfiguration != null)
			{
				this._insertModificationStoredProcedureConfiguration = source._insertModificationStoredProcedureConfiguration.Clone();
			}
			if (source._updateModificationStoredProcedureConfiguration != null)
			{
				this._updateModificationStoredProcedureConfiguration = source._updateModificationStoredProcedureConfiguration.Clone();
			}
			if (source._deleteModificationStoredProcedureConfiguration != null)
			{
				this._deleteModificationStoredProcedureConfiguration = source._deleteModificationStoredProcedureConfiguration.Clone();
			}
		}

		// Token: 0x060018EE RID: 6382 RVA: 0x0007BA7E File Offset: 0x00079C7E
		public virtual ModificationStoredProceduresConfiguration Clone()
		{
			return new ModificationStoredProceduresConfiguration(this);
		}

		// Token: 0x060018EF RID: 6383 RVA: 0x0007BA86 File Offset: 0x00079C86
		public virtual void Insert(ModificationStoredProcedureConfiguration modificationStoredProcedureConfiguration)
		{
			this._insertModificationStoredProcedureConfiguration = modificationStoredProcedureConfiguration;
		}

		// Token: 0x060018F0 RID: 6384 RVA: 0x0007BA8F File Offset: 0x00079C8F
		public virtual void Update(ModificationStoredProcedureConfiguration modificationStoredProcedureConfiguration)
		{
			this._updateModificationStoredProcedureConfiguration = modificationStoredProcedureConfiguration;
		}

		// Token: 0x060018F1 RID: 6385 RVA: 0x0007BA98 File Offset: 0x00079C98
		public virtual void Delete(ModificationStoredProcedureConfiguration modificationStoredProcedureConfiguration)
		{
			this._deleteModificationStoredProcedureConfiguration = modificationStoredProcedureConfiguration;
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x060018F2 RID: 6386 RVA: 0x0007BAA1 File Offset: 0x00079CA1
		public ModificationStoredProcedureConfiguration InsertModificationStoredProcedureConfiguration
		{
			get
			{
				return this._insertModificationStoredProcedureConfiguration;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x060018F3 RID: 6387 RVA: 0x0007BAA9 File Offset: 0x00079CA9
		public ModificationStoredProcedureConfiguration UpdateModificationStoredProcedureConfiguration
		{
			get
			{
				return this._updateModificationStoredProcedureConfiguration;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x060018F4 RID: 6388 RVA: 0x0007BAB1 File Offset: 0x00079CB1
		public ModificationStoredProcedureConfiguration DeleteModificationStoredProcedureConfiguration
		{
			get
			{
				return this._deleteModificationStoredProcedureConfiguration;
			}
		}

		// Token: 0x060018F5 RID: 6389 RVA: 0x0007BABC File Offset: 0x00079CBC
		public virtual void Configure(EntityTypeModificationFunctionMapping modificationStoredProcedureMapping, DbProviderManifest providerManifest)
		{
			if (this._insertModificationStoredProcedureConfiguration != null)
			{
				this._insertModificationStoredProcedureConfiguration.Configure(modificationStoredProcedureMapping.InsertFunctionMapping, providerManifest);
			}
			if (this._updateModificationStoredProcedureConfiguration != null)
			{
				this._updateModificationStoredProcedureConfiguration.Configure(modificationStoredProcedureMapping.UpdateFunctionMapping, providerManifest);
			}
			if (this._deleteModificationStoredProcedureConfiguration != null)
			{
				this._deleteModificationStoredProcedureConfiguration.Configure(modificationStoredProcedureMapping.DeleteFunctionMapping, providerManifest);
			}
		}

		// Token: 0x060018F6 RID: 6390 RVA: 0x0007BB17 File Offset: 0x00079D17
		public void Configure(AssociationSetModificationFunctionMapping modificationStoredProcedureMapping, DbProviderManifest providerManifest)
		{
			if (this._insertModificationStoredProcedureConfiguration != null)
			{
				this._insertModificationStoredProcedureConfiguration.Configure(modificationStoredProcedureMapping.InsertFunctionMapping, providerManifest);
			}
			if (this._deleteModificationStoredProcedureConfiguration != null)
			{
				this._deleteModificationStoredProcedureConfiguration.Configure(modificationStoredProcedureMapping.DeleteFunctionMapping, providerManifest);
			}
		}

		// Token: 0x060018F7 RID: 6391 RVA: 0x0007BB50 File Offset: 0x00079D50
		public bool IsCompatibleWith(ModificationStoredProceduresConfiguration other)
		{
			return (this._insertModificationStoredProcedureConfiguration == null || other._insertModificationStoredProcedureConfiguration == null || this._insertModificationStoredProcedureConfiguration.IsCompatibleWith(other._insertModificationStoredProcedureConfiguration)) && (this._deleteModificationStoredProcedureConfiguration == null || other._deleteModificationStoredProcedureConfiguration == null || this._deleteModificationStoredProcedureConfiguration.IsCompatibleWith(other._deleteModificationStoredProcedureConfiguration));
		}

		// Token: 0x060018F8 RID: 6392 RVA: 0x0007BBA8 File Offset: 0x00079DA8
		public void Merge(ModificationStoredProceduresConfiguration modificationStoredProceduresConfiguration, bool allowOverride)
		{
			if (this._insertModificationStoredProcedureConfiguration == null)
			{
				this._insertModificationStoredProcedureConfiguration = modificationStoredProceduresConfiguration.InsertModificationStoredProcedureConfiguration;
			}
			else if (modificationStoredProceduresConfiguration.InsertModificationStoredProcedureConfiguration != null)
			{
				this._insertModificationStoredProcedureConfiguration.Merge(modificationStoredProceduresConfiguration.InsertModificationStoredProcedureConfiguration, allowOverride);
			}
			if (this._updateModificationStoredProcedureConfiguration == null)
			{
				this._updateModificationStoredProcedureConfiguration = modificationStoredProceduresConfiguration.UpdateModificationStoredProcedureConfiguration;
			}
			else if (modificationStoredProceduresConfiguration.UpdateModificationStoredProcedureConfiguration != null)
			{
				this._updateModificationStoredProcedureConfiguration.Merge(modificationStoredProceduresConfiguration.UpdateModificationStoredProcedureConfiguration, allowOverride);
			}
			if (this._deleteModificationStoredProcedureConfiguration == null)
			{
				this._deleteModificationStoredProcedureConfiguration = modificationStoredProceduresConfiguration.DeleteModificationStoredProcedureConfiguration;
				return;
			}
			if (modificationStoredProceduresConfiguration.DeleteModificationStoredProcedureConfiguration != null)
			{
				this._deleteModificationStoredProcedureConfiguration.Merge(modificationStoredProceduresConfiguration.DeleteModificationStoredProcedureConfiguration, allowOverride);
			}
		}

		// Token: 0x04000891 RID: 2193
		private ModificationStoredProcedureConfiguration _insertModificationStoredProcedureConfiguration;

		// Token: 0x04000892 RID: 2194
		private ModificationStoredProcedureConfiguration _updateModificationStoredProcedureConfiguration;

		// Token: 0x04000893 RID: 2195
		private ModificationStoredProcedureConfiguration _deleteModificationStoredProcedureConfiguration;
	}
}
