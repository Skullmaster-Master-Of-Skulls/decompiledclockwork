using System;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020002B4 RID: 692
	public class ConventionDeleteModificationStoredProcedureConfiguration : ConventionModificationStoredProcedureConfiguration
	{
		// Token: 0x06001841 RID: 6209 RVA: 0x00079D60 File Offset: 0x00077F60
		internal ConventionDeleteModificationStoredProcedureConfiguration(Type type)
		{
			this._type = type;
		}

		// Token: 0x06001842 RID: 6210 RVA: 0x00079D6F File Offset: 0x00077F6F
		public ConventionDeleteModificationStoredProcedureConfiguration HasName(string procedureName)
		{
			Check.NotEmpty(procedureName, "procedureName");
			base.Configuration.HasName(procedureName);
			return this;
		}

		// Token: 0x06001843 RID: 6211 RVA: 0x00079D8A File Offset: 0x00077F8A
		public ConventionDeleteModificationStoredProcedureConfiguration HasName(string procedureName, string schemaName)
		{
			Check.NotEmpty(procedureName, "procedureName");
			Check.NotEmpty(schemaName, "schemaName");
			base.Configuration.HasName(procedureName, schemaName);
			return this;
		}

		// Token: 0x06001844 RID: 6212 RVA: 0x00079DB2 File Offset: 0x00077FB2
		public ConventionDeleteModificationStoredProcedureConfiguration Parameter(string propertyName, string parameterName)
		{
			Check.NotEmpty(propertyName, "propertyName");
			Check.NotEmpty(parameterName, "parameterName");
			return this.Parameter(this._type.GetAnyProperty(propertyName), parameterName);
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x00079DDF File Offset: 0x00077FDF
		public ConventionDeleteModificationStoredProcedureConfiguration Parameter(PropertyInfo propertyInfo, string parameterName)
		{
			Check.NotEmpty(parameterName, "parameterName");
			if (propertyInfo != null)
			{
				base.Configuration.Parameter(new PropertyPath(propertyInfo), parameterName, null, false);
			}
			return this;
		}

		// Token: 0x06001846 RID: 6214 RVA: 0x00079E0B File Offset: 0x0007800B
		public ConventionDeleteModificationStoredProcedureConfiguration RowsAffectedParameter(string parameterName)
		{
			Check.NotEmpty(parameterName, "parameterName");
			base.Configuration.RowsAffectedParameter(parameterName);
			return this;
		}

		// Token: 0x06001847 RID: 6215 RVA: 0x00079E26 File Offset: 0x00078026
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06001848 RID: 6216 RVA: 0x00079E2E File Offset: 0x0007802E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06001849 RID: 6217 RVA: 0x00079E37 File Offset: 0x00078037
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600184A RID: 6218 RVA: 0x00079E3F File Offset: 0x0007803F
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x0400087A RID: 2170
		private readonly Type _type;
	}
}
