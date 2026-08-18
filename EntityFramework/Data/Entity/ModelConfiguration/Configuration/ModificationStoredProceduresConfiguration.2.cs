using System;
using System.ComponentModel;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020002C1 RID: 705
	public class ModificationStoredProceduresConfiguration<TEntityType> where TEntityType : class
	{
		// Token: 0x060018F9 RID: 6393 RVA: 0x0007BC44 File Offset: 0x00079E44
		internal ModificationStoredProceduresConfiguration()
		{
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x060018FA RID: 6394 RVA: 0x0007BC57 File Offset: 0x00079E57
		internal ModificationStoredProceduresConfiguration Configuration
		{
			get
			{
				return this._configuration;
			}
		}

		// Token: 0x060018FB RID: 6395 RVA: 0x0007BC60 File Offset: 0x00079E60
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public ModificationStoredProceduresConfiguration<TEntityType> Insert(Action<InsertModificationStoredProcedureConfiguration<TEntityType>> modificationStoredProcedureConfigurationAction)
		{
			Check.NotNull<Action<InsertModificationStoredProcedureConfiguration<TEntityType>>>(modificationStoredProcedureConfigurationAction, "modificationStoredProcedureConfigurationAction");
			InsertModificationStoredProcedureConfiguration<TEntityType> insertModificationStoredProcedureConfiguration = new InsertModificationStoredProcedureConfiguration<TEntityType>();
			modificationStoredProcedureConfigurationAction(insertModificationStoredProcedureConfiguration);
			this._configuration.Insert(insertModificationStoredProcedureConfiguration.Configuration);
			return this;
		}

		// Token: 0x060018FC RID: 6396 RVA: 0x0007BC98 File Offset: 0x00079E98
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public ModificationStoredProceduresConfiguration<TEntityType> Update(Action<UpdateModificationStoredProcedureConfiguration<TEntityType>> modificationStoredProcedureConfigurationAction)
		{
			Check.NotNull<Action<UpdateModificationStoredProcedureConfiguration<TEntityType>>>(modificationStoredProcedureConfigurationAction, "modificationStoredProcedureConfigurationAction");
			UpdateModificationStoredProcedureConfiguration<TEntityType> updateModificationStoredProcedureConfiguration = new UpdateModificationStoredProcedureConfiguration<TEntityType>();
			modificationStoredProcedureConfigurationAction(updateModificationStoredProcedureConfiguration);
			this._configuration.Update(updateModificationStoredProcedureConfiguration.Configuration);
			return this;
		}

		// Token: 0x060018FD RID: 6397 RVA: 0x0007BCD0 File Offset: 0x00079ED0
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public ModificationStoredProceduresConfiguration<TEntityType> Delete(Action<DeleteModificationStoredProcedureConfiguration<TEntityType>> modificationStoredProcedureConfigurationAction)
		{
			Check.NotNull<Action<DeleteModificationStoredProcedureConfiguration<TEntityType>>>(modificationStoredProcedureConfigurationAction, "modificationStoredProcedureConfigurationAction");
			DeleteModificationStoredProcedureConfiguration<TEntityType> deleteModificationStoredProcedureConfiguration = new DeleteModificationStoredProcedureConfiguration<TEntityType>();
			modificationStoredProcedureConfigurationAction(deleteModificationStoredProcedureConfiguration);
			this._configuration.Delete(deleteModificationStoredProcedureConfiguration.Configuration);
			return this;
		}

		// Token: 0x060018FE RID: 6398 RVA: 0x0007BD08 File Offset: 0x00079F08
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x060018FF RID: 6399 RVA: 0x0007BD10 File Offset: 0x00079F10
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06001900 RID: 6400 RVA: 0x0007BD19 File Offset: 0x00079F19
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001901 RID: 6401 RVA: 0x0007BD21 File Offset: 0x00079F21
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000894 RID: 2196
		private readonly ModificationStoredProceduresConfiguration _configuration = new ModificationStoredProceduresConfiguration();
	}
}
