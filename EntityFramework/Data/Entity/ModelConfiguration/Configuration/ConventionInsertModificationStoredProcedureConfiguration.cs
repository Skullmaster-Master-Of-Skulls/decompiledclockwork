using System;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020002B5 RID: 693
	public class ConventionInsertModificationStoredProcedureConfiguration : ConventionModificationStoredProcedureConfiguration
	{
		// Token: 0x0600184B RID: 6219 RVA: 0x00079E47 File Offset: 0x00078047
		internal ConventionInsertModificationStoredProcedureConfiguration(Type type)
		{
			this._type = type;
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x00079E56 File Offset: 0x00078056
		public ConventionInsertModificationStoredProcedureConfiguration HasName(string procedureName)
		{
			Check.NotEmpty(procedureName, "procedureName");
			base.Configuration.HasName(procedureName);
			return this;
		}

		// Token: 0x0600184D RID: 6221 RVA: 0x00079E71 File Offset: 0x00078071
		public ConventionInsertModificationStoredProcedureConfiguration HasName(string procedureName, string schemaName)
		{
			Check.NotEmpty(procedureName, "procedureName");
			Check.NotEmpty(schemaName, "schemaName");
			base.Configuration.HasName(procedureName, schemaName);
			return this;
		}

		// Token: 0x0600184E RID: 6222 RVA: 0x00079E99 File Offset: 0x00078099
		public ConventionInsertModificationStoredProcedureConfiguration Parameter(string propertyName, string parameterName)
		{
			Check.NotEmpty(propertyName, "propertyName");
			Check.NotEmpty(parameterName, "parameterName");
			return this.Parameter(this._type.GetAnyProperty(propertyName), parameterName);
		}

		// Token: 0x0600184F RID: 6223 RVA: 0x00079EC6 File Offset: 0x000780C6
		public ConventionInsertModificationStoredProcedureConfiguration Parameter(PropertyInfo propertyInfo, string parameterName)
		{
			Check.NotEmpty(parameterName, "parameterName");
			if (propertyInfo != null)
			{
				base.Configuration.Parameter(new PropertyPath(propertyInfo), parameterName, null, false);
			}
			return this;
		}

		// Token: 0x06001850 RID: 6224 RVA: 0x00079EF2 File Offset: 0x000780F2
		public ConventionInsertModificationStoredProcedureConfiguration Result(string propertyName, string columnName)
		{
			Check.NotEmpty(propertyName, "propertyName");
			Check.NotEmpty(columnName, "columnName");
			base.Configuration.Result(new PropertyPath(this._type.GetAnyProperty(propertyName)), columnName);
			return this;
		}

		// Token: 0x06001851 RID: 6225 RVA: 0x00079F2A File Offset: 0x0007812A
		public ConventionInsertModificationStoredProcedureConfiguration Result(PropertyInfo propertyInfo, string columnName)
		{
			Check.NotNull<PropertyInfo>(propertyInfo, "propertyInfo");
			Check.NotEmpty(columnName, "columnName");
			base.Configuration.Result(new PropertyPath(propertyInfo), columnName);
			return this;
		}

		// Token: 0x06001852 RID: 6226 RVA: 0x00079F57 File Offset: 0x00078157
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06001853 RID: 6227 RVA: 0x00079F5F File Offset: 0x0007815F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06001854 RID: 6228 RVA: 0x00079F68 File Offset: 0x00078168
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001855 RID: 6229 RVA: 0x00079F70 File Offset: 0x00078170
		[EditorBrowsable(EditorBrowsableState.Never)]
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x0400087B RID: 2171
		private readonly Type _type;
	}
}
