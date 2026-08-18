using System;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020002B7 RID: 695
	public class ConventionUpdateModificationStoredProcedureConfiguration : ConventionModificationStoredProcedureConfiguration
	{
		// Token: 0x0600185F RID: 6239 RVA: 0x0007A07B File Offset: 0x0007827B
		internal ConventionUpdateModificationStoredProcedureConfiguration(Type type)
		{
			this._type = type;
		}

		// Token: 0x06001860 RID: 6240 RVA: 0x0007A08A File Offset: 0x0007828A
		public ConventionUpdateModificationStoredProcedureConfiguration HasName(string procedureName)
		{
			Check.NotEmpty(procedureName, "procedureName");
			base.Configuration.HasName(procedureName);
			return this;
		}

		// Token: 0x06001861 RID: 6241 RVA: 0x0007A0A5 File Offset: 0x000782A5
		public ConventionUpdateModificationStoredProcedureConfiguration HasName(string procedureName, string schemaName)
		{
			Check.NotEmpty(procedureName, "procedureName");
			Check.NotEmpty(schemaName, "schemaName");
			base.Configuration.HasName(procedureName, schemaName);
			return this;
		}

		// Token: 0x06001862 RID: 6242 RVA: 0x0007A0CD File Offset: 0x000782CD
		public ConventionUpdateModificationStoredProcedureConfiguration Parameter(string propertyName, string parameterName)
		{
			Check.NotEmpty(propertyName, "propertyName");
			Check.NotEmpty(parameterName, "parameterName");
			return this.Parameter(this._type.GetAnyProperty(propertyName), parameterName);
		}

		// Token: 0x06001863 RID: 6243 RVA: 0x0007A0FA File Offset: 0x000782FA
		public ConventionUpdateModificationStoredProcedureConfiguration Parameter(PropertyInfo propertyInfo, string parameterName)
		{
			Check.NotEmpty(parameterName, "parameterName");
			if (propertyInfo != null)
			{
				base.Configuration.Parameter(new PropertyPath(propertyInfo), parameterName, null, false);
			}
			return this;
		}

		// Token: 0x06001864 RID: 6244 RVA: 0x0007A126 File Offset: 0x00078326
		public ConventionUpdateModificationStoredProcedureConfiguration Parameter(string propertyName, string currentValueParameterName, string originalValueParameterName)
		{
			Check.NotEmpty(propertyName, "propertyName");
			Check.NotEmpty(currentValueParameterName, "currentValueParameterName");
			Check.NotEmpty(originalValueParameterName, "originalValueParameterName");
			return this.Parameter(this._type.GetAnyProperty(propertyName), currentValueParameterName, originalValueParameterName);
		}

		// Token: 0x06001865 RID: 6245 RVA: 0x0007A160 File Offset: 0x00078360
		public ConventionUpdateModificationStoredProcedureConfiguration Parameter(PropertyInfo propertyInfo, string currentValueParameterName, string originalValueParameterName)
		{
			Check.NotEmpty(currentValueParameterName, "currentValueParameterName");
			Check.NotEmpty(originalValueParameterName, "originalValueParameterName");
			if (propertyInfo != null)
			{
				base.Configuration.Parameter(new PropertyPath(propertyInfo), currentValueParameterName, originalValueParameterName, false);
			}
			return this;
		}

		// Token: 0x06001866 RID: 6246 RVA: 0x0007A198 File Offset: 0x00078398
		public ConventionUpdateModificationStoredProcedureConfiguration Result(string propertyName, string columnName)
		{
			Check.NotEmpty(propertyName, "propertyName");
			Check.NotEmpty(columnName, "columnName");
			base.Configuration.Result(new PropertyPath(this._type.GetAnyProperty(propertyName)), columnName);
			return this;
		}

		// Token: 0x06001867 RID: 6247 RVA: 0x0007A1D0 File Offset: 0x000783D0
		public ConventionUpdateModificationStoredProcedureConfiguration Result(PropertyInfo propertyInfo, string columnName)
		{
			Check.NotNull<PropertyInfo>(propertyInfo, "propertyInfo");
			Check.NotEmpty(columnName, "columnName");
			base.Configuration.Result(new PropertyPath(propertyInfo), columnName);
			return this;
		}

		// Token: 0x06001868 RID: 6248 RVA: 0x0007A1FD File Offset: 0x000783FD
		public ConventionUpdateModificationStoredProcedureConfiguration RowsAffectedParameter(string parameterName)
		{
			Check.NotEmpty(parameterName, "parameterName");
			base.Configuration.RowsAffectedParameter(parameterName);
			return this;
		}

		// Token: 0x06001869 RID: 6249 RVA: 0x0007A218 File Offset: 0x00078418
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x0600186A RID: 6250 RVA: 0x0007A220 File Offset: 0x00078420
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x0600186B RID: 6251 RVA: 0x0007A229 File Offset: 0x00078429
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600186C RID: 6252 RVA: 0x0007A231 File Offset: 0x00078431
		[EditorBrowsable(EditorBrowsableState.Never)]
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x0400087E RID: 2174
		private readonly Type _type;
	}
}
