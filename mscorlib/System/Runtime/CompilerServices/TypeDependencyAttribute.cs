using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020005F4 RID: 1524
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
	internal sealed class TypeDependencyAttribute : Attribute
	{
		// Token: 0x060037FE RID: 14334 RVA: 0x000BBD8C File Offset: 0x000BAD8C
		public TypeDependencyAttribute(string typeName)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			this.typeName = typeName;
		}

		// Token: 0x04001D07 RID: 7431
		private string typeName;
	}
}
