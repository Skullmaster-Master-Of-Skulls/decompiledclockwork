using System;

namespace System.Data.Entity.Core.Objects.DataClasses
{
	// Token: 0x020001FD RID: 509
	[Obsolete("This attribute has been replaced by System.Data.Entity.DbFunctionAttribute.")]
	[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
	public sealed class EdmFunctionAttribute : DbFunctionAttribute
	{
		// Token: 0x060011DA RID: 4570 RVA: 0x0004C8D9 File Offset: 0x0004AAD9
		public EdmFunctionAttribute(string namespaceName, string functionName) : base(namespaceName, functionName)
		{
		}
	}
}
