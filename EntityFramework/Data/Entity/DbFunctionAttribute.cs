using System;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity
{
	// Token: 0x020001FC RID: 508
	[SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes")]
	[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
	public class DbFunctionAttribute : Attribute
	{
		// Token: 0x060011D7 RID: 4567 RVA: 0x0004C89B File Offset: 0x0004AA9B
		public DbFunctionAttribute(string namespaceName, string functionName)
		{
			Check.NotEmpty(namespaceName, "namespaceName");
			Check.NotEmpty(functionName, "functionName");
			this._namespaceName = namespaceName;
			this._functionName = functionName;
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060011D8 RID: 4568 RVA: 0x0004C8C9 File Offset: 0x0004AAC9
		public string NamespaceName
		{
			get
			{
				return this._namespaceName;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060011D9 RID: 4569 RVA: 0x0004C8D1 File Offset: 0x0004AAD1
		public string FunctionName
		{
			get
			{
				return this._functionName;
			}
		}

		// Token: 0x04000557 RID: 1367
		private readonly string _namespaceName;

		// Token: 0x04000558 RID: 1368
		private readonly string _functionName;
	}
}
