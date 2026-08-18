using System;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Navigation;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020007D0 RID: 2000
	public class ManyToManyNavigationPropertyConfiguration<TEntityType, TTargetEntityType> where TEntityType : class where TTargetEntityType : class
	{
		// Token: 0x06005AD4 RID: 23252 RVA: 0x00187519 File Offset: 0x00185719
		internal ManyToManyNavigationPropertyConfiguration(NavigationPropertyConfiguration navigationPropertyConfiguration)
		{
			this._navigationPropertyConfiguration = navigationPropertyConfiguration;
		}

		// Token: 0x06005AD5 RID: 23253 RVA: 0x00187528 File Offset: 0x00185728
		public ManyToManyNavigationPropertyConfiguration<TEntityType, TTargetEntityType> Map(Action<ManyToManyAssociationMappingConfiguration> configurationAction)
		{
			Check.NotNull<Action<ManyToManyAssociationMappingConfiguration>>(configurationAction, "configurationAction");
			ManyToManyAssociationMappingConfiguration manyToManyAssociationMappingConfiguration = new ManyToManyAssociationMappingConfiguration();
			configurationAction(manyToManyAssociationMappingConfiguration);
			this._navigationPropertyConfiguration.AssociationMappingConfiguration = manyToManyAssociationMappingConfiguration;
			return this;
		}

		// Token: 0x06005AD6 RID: 23254 RVA: 0x0018755B File Offset: 0x0018575B
		public ManyToManyNavigationPropertyConfiguration<TEntityType, TTargetEntityType> MapToStoredProcedures()
		{
			if (this._navigationPropertyConfiguration.ModificationStoredProceduresConfiguration == null)
			{
				this._navigationPropertyConfiguration.ModificationStoredProceduresConfiguration = new ModificationStoredProceduresConfiguration();
			}
			return this;
		}

		// Token: 0x06005AD7 RID: 23255 RVA: 0x0018757C File Offset: 0x0018577C
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public ManyToManyNavigationPropertyConfiguration<TEntityType, TTargetEntityType> MapToStoredProcedures(Action<ManyToManyModificationStoredProceduresConfiguration<TEntityType, TTargetEntityType>> modificationStoredProcedureMappingConfigurationAction)
		{
			Check.NotNull<Action<ManyToManyModificationStoredProceduresConfiguration<TEntityType, TTargetEntityType>>>(modificationStoredProcedureMappingConfigurationAction, "modificationStoredProcedureMappingConfigurationAction");
			ManyToManyModificationStoredProceduresConfiguration<TEntityType, TTargetEntityType> manyToManyModificationStoredProceduresConfiguration = new ManyToManyModificationStoredProceduresConfiguration<TEntityType, TTargetEntityType>();
			modificationStoredProcedureMappingConfigurationAction(manyToManyModificationStoredProceduresConfiguration);
			if (this._navigationPropertyConfiguration.ModificationStoredProceduresConfiguration == null)
			{
				this._navigationPropertyConfiguration.ModificationStoredProceduresConfiguration = manyToManyModificationStoredProceduresConfiguration.Configuration;
			}
			else
			{
				this._navigationPropertyConfiguration.ModificationStoredProceduresConfiguration.Merge(manyToManyModificationStoredProceduresConfiguration.Configuration, true);
			}
			return this;
		}

		// Token: 0x06005AD8 RID: 23256 RVA: 0x001875DA File Offset: 0x001857DA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06005AD9 RID: 23257 RVA: 0x001875E2 File Offset: 0x001857E2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06005ADA RID: 23258 RVA: 0x001875EB File Offset: 0x001857EB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06005ADB RID: 23259 RVA: 0x001875F3 File Offset: 0x001857F3
		[EditorBrowsable(EditorBrowsableState.Never)]
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04002435 RID: 9269
		private readonly NavigationPropertyConfiguration _navigationPropertyConfiguration;
	}
}
