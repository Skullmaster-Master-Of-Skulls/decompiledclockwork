using System;
using System.ComponentModel;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020002B6 RID: 694
	public class ConventionModificationStoredProceduresConfiguration
	{
		// Token: 0x06001856 RID: 6230 RVA: 0x00079F78 File Offset: 0x00078178
		internal ConventionModificationStoredProceduresConfiguration(Type type)
		{
			this._type = type;
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06001857 RID: 6231 RVA: 0x00079F92 File Offset: 0x00078192
		internal ModificationStoredProceduresConfiguration Configuration
		{
			get
			{
				return this._configuration;
			}
		}

		// Token: 0x06001858 RID: 6232 RVA: 0x00079F9C File Offset: 0x0007819C
		public ConventionModificationStoredProceduresConfiguration Insert(Action<ConventionInsertModificationStoredProcedureConfiguration> modificationStoredProcedureConfigurationAction)
		{
			Check.NotNull<Action<ConventionInsertModificationStoredProcedureConfiguration>>(modificationStoredProcedureConfigurationAction, "modificationStoredProcedureConfigurationAction");
			ConventionInsertModificationStoredProcedureConfiguration conventionInsertModificationStoredProcedureConfiguration = new ConventionInsertModificationStoredProcedureConfiguration(this._type);
			modificationStoredProcedureConfigurationAction(conventionInsertModificationStoredProcedureConfiguration);
			this._configuration.Insert(conventionInsertModificationStoredProcedureConfiguration.Configuration);
			return this;
		}

		// Token: 0x06001859 RID: 6233 RVA: 0x00079FDC File Offset: 0x000781DC
		public ConventionModificationStoredProceduresConfiguration Update(Action<ConventionUpdateModificationStoredProcedureConfiguration> modificationStoredProcedureConfigurationAction)
		{
			Check.NotNull<Action<ConventionUpdateModificationStoredProcedureConfiguration>>(modificationStoredProcedureConfigurationAction, "modificationStoredProcedureConfigurationAction");
			ConventionUpdateModificationStoredProcedureConfiguration conventionUpdateModificationStoredProcedureConfiguration = new ConventionUpdateModificationStoredProcedureConfiguration(this._type);
			modificationStoredProcedureConfigurationAction(conventionUpdateModificationStoredProcedureConfiguration);
			this._configuration.Update(conventionUpdateModificationStoredProcedureConfiguration.Configuration);
			return this;
		}

		// Token: 0x0600185A RID: 6234 RVA: 0x0007A01C File Offset: 0x0007821C
		public ConventionModificationStoredProceduresConfiguration Delete(Action<ConventionDeleteModificationStoredProcedureConfiguration> modificationStoredProcedureConfigurationAction)
		{
			Check.NotNull<Action<ConventionDeleteModificationStoredProcedureConfiguration>>(modificationStoredProcedureConfigurationAction, "modificationStoredProcedureConfigurationAction");
			ConventionDeleteModificationStoredProcedureConfiguration conventionDeleteModificationStoredProcedureConfiguration = new ConventionDeleteModificationStoredProcedureConfiguration(this._type);
			modificationStoredProcedureConfigurationAction(conventionDeleteModificationStoredProcedureConfiguration);
			this._configuration.Delete(conventionDeleteModificationStoredProcedureConfiguration.Configuration);
			return this;
		}

		// Token: 0x0600185B RID: 6235 RVA: 0x0007A05A File Offset: 0x0007825A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x0600185C RID: 6236 RVA: 0x0007A062 File Offset: 0x00078262
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x0600185D RID: 6237 RVA: 0x0007A06B File Offset: 0x0007826B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600185E RID: 6238 RVA: 0x0007A073 File Offset: 0x00078273
		[EditorBrowsable(EditorBrowsableState.Never)]
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x0400087C RID: 2172
		private readonly Type _type;

		// Token: 0x0400087D RID: 2173
		private readonly ModificationStoredProceduresConfiguration _configuration = new ModificationStoredProceduresConfiguration();
	}
}
