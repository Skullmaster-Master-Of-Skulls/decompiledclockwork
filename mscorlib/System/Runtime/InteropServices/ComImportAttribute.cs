using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x020004F6 RID: 1270
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
	[ComVisible(true)]
	public sealed class ComImportAttribute : Attribute
	{
		// Token: 0x06003173 RID: 12659 RVA: 0x000A9452 File Offset: 0x000A8452
		internal static Attribute GetCustomAttribute(RuntimeType type)
		{
			if ((type.Attributes & TypeAttributes.Import) == TypeAttributes.NotPublic)
			{
				return null;
			}
			return new ComImportAttribute();
		}

		// Token: 0x06003174 RID: 12660 RVA: 0x000A9469 File Offset: 0x000A8469
		internal static bool IsDefined(RuntimeType type)
		{
			return (type.Attributes & TypeAttributes.Import) != TypeAttributes.NotPublic;
		}
	}
}
