using System;
using System.Linq;
using System.Reflection;
using AutoMapper.Internal;

namespace AutoMapper.Mappers
{
	// Token: 0x02000082 RID: 130
	public class ImplicitConversionOperatorMapper : IObjectMapper
	{
		// Token: 0x06000422 RID: 1058 RVA: 0x000113D1 File Offset: 0x0000F5D1
		public object Map(ResolutionContext context)
		{
			return ImplicitConversionOperatorMapper.GetImplicitConversionOperator(context.Types).Invoke(null, new object[]
			{
				context.SourceValue
			});
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x000113F3 File Offset: 0x0000F5F3
		public bool IsMatch(TypePair context)
		{
			return ImplicitConversionOperatorMapper.GetImplicitConversionOperator(context) != null;
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x00011404 File Offset: 0x0000F604
		private static MethodInfo GetImplicitConversionOperator(TypePair context)
		{
			Type destinationType = context.DestinationType;
			if (destinationType.IsNullableType())
			{
				destinationType = destinationType.GetTypeOfNullable();
			}
			MethodInfo result;
			if ((result = context.SourceType.GetDeclaredMethods().FirstOrDefault((MethodInfo mi) => mi.IsPublic && mi.IsStatic && mi.Name == "op_Implicit" && mi.ReturnType == destinationType)) == null)
			{
				result = destinationType.GetMethod("op_Implicit", new Type[]
				{
					context.SourceType
				});
			}
			return result;
		}
	}
}
