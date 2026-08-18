using System;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Xml.Serialization
{
	// Token: 0x02000136 RID: 310
	internal class MethodBuilderInfo
	{
		// Token: 0x060016A9 RID: 5801 RVA: 0x00063F1C File Offset: 0x0006211C
		public MethodBuilderInfo(MethodBuilder methodBuilder, Type[] parameterTypes)
		{
			this.MethodBuilder = methodBuilder;
			this.ParameterTypes = parameterTypes;
		}

		// Token: 0x060016AA RID: 5802 RVA: 0x00063F32 File Offset: 0x00062132
		public void Validate(Type returnType, Type[] parameterTypes, MethodAttributes attributes)
		{
		}

		// Token: 0x04000A92 RID: 2706
		public readonly MethodBuilder MethodBuilder;

		// Token: 0x04000A93 RID: 2707
		public readonly Type[] ParameterTypes;
	}
}
