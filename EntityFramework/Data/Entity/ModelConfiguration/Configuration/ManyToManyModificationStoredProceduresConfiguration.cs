using System;
using System.ComponentModel;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020002BA RID: 698
	public class ManyToManyModificationStoredProceduresConfiguration<TEntityType, TTargetEntityType> where TEntityType : class where TTargetEntityType : class
	{
		// Token: 0x0600187E RID: 6270 RVA: 0x0007A438 File Offset: 0x00078638
		internal ManyToManyModificationStoredProceduresConfiguration()
		{
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x0600187F RID: 6271 RVA: 0x0007A44B File Offset: 0x0007864B
		internal ModificationStoredProceduresConfiguration Configuration
		{
			get
			{
				return this._configuration;
			}
		}

		// Token: 0x06001880 RID: 6272 RVA: 0x0007A454 File Offset: 0x00078654
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public ManyToManyModificationStoredProceduresConfiguration<TEntityType, TTargetEntityType> Insert(Action<ManyToManyModificationStoredProcedureConfiguration<TEntityType, TTargetEntityType>> modificationStoredProcedureConfigurationAction)
		{
			Check.NotNull<Action<ManyToManyModificationStoredProcedureConfiguration<TEntityType, TTargetEntityType>>>(modificationStoredProcedureConfigurationAction, "modificationStoredProcedureConfigurationAction");
			ManyToManyModificationStoredProcedureConfiguration<TEntityType, TTargetEntityType> manyToManyModificationStoredProcedureConfiguration = new ManyToManyModificationStoredProcedureConfiguration<TEntityType, TTargetEntityType>();
			modificationStoredProcedureConfigurationAction(manyToManyModificationStoredProcedureConfiguration);
			this._configuration.Insert(manyToManyModificationStoredProcedureConfiguration.Configuration);
			return this;
		}

		// Token: 0x06001881 RID: 6273 RVA: 0x0007A48C File Offset: 0x0007868C
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public ManyToManyModificationStoredProceduresConfiguration<TEntityType, TTargetEntityType> Delete(Action<ManyToManyModificationStoredProcedureConfiguration<TEntityType, TTargetEntityType>> modificationStoredProcedureConfigurationAction)
		{
			Check.NotNull<Action<ManyToManyModificationStoredProcedureConfiguration<TEntityType, TTargetEntityType>>>(modificationStoredProcedureConfigurationAction, "modificationStoredProcedureConfigurationAction");
			ManyToManyModificationStoredProcedureConfiguration<TEntityType, TTargetEntityType> manyToManyModificationStoredProcedureConfiguration = new ManyToManyModificationStoredProcedureConfiguration<TEntityType, TTargetEntityType>();
			modificationStoredProcedureConfigurationAction(manyToManyModificationStoredProcedureConfiguration);
			this._configuration.Delete(manyToManyModificationStoredProcedureConfiguration.Configuration);
			return this;
		}

		// Token: 0x06001882 RID: 6274 RVA: 0x0007A4C4 File Offset: 0x000786C4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06001883 RID: 6275 RVA: 0x0007A4CC File Offset: 0x000786CC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06001884 RID: 6276 RVA: 0x0007A4D5 File Offset: 0x000786D5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001885 RID: 6277 RVA: 0x0007A4DD File Offset: 0x000786DD
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000880 RID: 2176
		private readonly ModificationStoredProceduresConfiguration _configuration = new ModificationStoredProceduresConfiguration();
	}
}
