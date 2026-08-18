using System;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x02000271 RID: 625
	internal static class ConstantCheck
	{
		// Token: 0x0600165C RID: 5724 RVA: 0x00049F4A File Offset: 0x0004814A
		internal static bool IsNull(Expression e)
		{
			return e.NodeType == ExpressionType.Constant && ((ConstantExpression)e).Value == null;
		}

		// Token: 0x0600165D RID: 5725 RVA: 0x00049F66 File Offset: 0x00048166
		internal static AnalyzeTypeIsResult AnalyzeTypeIs(TypeBinaryExpression typeIs)
		{
			return ConstantCheck.AnalyzeTypeIs(typeIs.Expression, typeIs.TypeOperand);
		}

		// Token: 0x0600165E RID: 5726 RVA: 0x00049F7C File Offset: 0x0004817C
		private static AnalyzeTypeIsResult AnalyzeTypeIs(Expression operand, Type testType)
		{
			Type type = operand.Type;
			if (type == typeof(void))
			{
				return AnalyzeTypeIsResult.KnownFalse;
			}
			Type nonNullableType = type.GetNonNullableType();
			Type nonNullableType2 = testType.GetNonNullableType();
			if (!nonNullableType2.IsAssignableFrom(nonNullableType))
			{
				return AnalyzeTypeIsResult.Unknown;
			}
			if (type.IsValueType && !type.IsNullableType())
			{
				return AnalyzeTypeIsResult.KnownTrue;
			}
			return AnalyzeTypeIsResult.KnownAssignable;
		}
	}
}
