using System;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Objects.DataClasses
{
	// Token: 0x02000539 RID: 1337
	[SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments")]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, AllowMultiple = true)]
	public sealed class EdmSchemaAttribute : Attribute
	{
		// Token: 0x060032F4 RID: 13044 RVA: 0x000F0829 File Offset: 0x000EEA29
		public EdmSchemaAttribute()
		{
		}

		// Token: 0x060032F5 RID: 13045 RVA: 0x000F0831 File Offset: 0x000EEA31
		public EdmSchemaAttribute(string assemblyGuid)
		{
			Check.NotNull<string>(assemblyGuid, "assemblyGuid");
		}
	}
}
