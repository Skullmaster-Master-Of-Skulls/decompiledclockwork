using System;

namespace System.Reflection
{
	// Token: 0x02000010 RID: 16
	internal static class TypeAttributesExtensions
	{
		// Token: 0x06000108 RID: 264 RVA: 0x000044F6 File Offset: 0x000026F6
		public static bool IsForwarder(this TypeAttributes flags)
		{
			return (flags & (TypeAttributes)2097152) > TypeAttributes.NotPublic;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00004502 File Offset: 0x00002702
		public static bool IsNested(this TypeAttributes flags)
		{
			return (flags & TypeAttributes.NestedFamANDAssem) > TypeAttributes.NotPublic;
		}

		// Token: 0x0400004D RID: 77
		private const TypeAttributes Forwarder = (TypeAttributes)2097152;

		// Token: 0x0400004E RID: 78
		private const TypeAttributes NestedMask = TypeAttributes.NestedFamANDAssem;
	}
}
