using System;
using System.Linq;
using System.Reflection;
using AutoMapper.Internal;

namespace AutoMapper.Mappers
{
	// Token: 0x0200007E RID: 126
	public class ExplicitConversionOperatorMapper : IObjectMapper
	{
		// Token: 0x06000414 RID: 1044 RVA: 0x00010F7A File Offset: 0x0000F17A
		public object Map(ResolutionContext context)
		{
			return ExplicitConversionOperatorMapper.GetExplicitConversionOperator(context.Types).Invoke(null, new object[]
			{
				context.SourceValue
			});
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00010F9C File Offset: 0x0000F19C
		public bool IsMatch(TypePair context)
		{
			return ExplicitConversionOperatorMapper.GetExplicitConversionOperator(context) != null;
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00010FAC File Offset: 0x0000F1AC
		private static MethodInfo GetExplicitConversionOperator(TypePair context)
		{
			MethodInfo methodInfo = (from mi in context.SourceType.GetDeclaredMethods()
			where mi.IsPublic && mi.IsStatic
			where mi.Name == "op_Explicit"
			select mi).FirstOrDefault((MethodInfo mi) => mi.ReturnType == context.DestinationType);
			MethodInfo method = context.DestinationType.GetMethod("op_Explicit", new Type[]
			{
				context.SourceType
			});
			return methodInfo ?? method;
		}
	}
}
