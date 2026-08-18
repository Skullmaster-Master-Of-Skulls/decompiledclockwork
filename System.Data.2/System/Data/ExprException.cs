using System;
using System.Globalization;

namespace System.Data
{
	// Token: 0x020000F3 RID: 243
	internal sealed class ExprException
	{
		// Token: 0x06000FA9 RID: 4009 RVA: 0x0007E424 File Offset: 0x0007D824
		private ExprException()
		{
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x0007E438 File Offset: 0x0007D838
		private static OverflowException _Overflow(string error)
		{
			OverflowException ex = new OverflowException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x0007E454 File Offset: 0x0007D854
		private static InvalidExpressionException _Expr(string error)
		{
			InvalidExpressionException ex = new InvalidExpressionException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x0007E470 File Offset: 0x0007D870
		private static SyntaxErrorException _Syntax(string error)
		{
			SyntaxErrorException ex = new SyntaxErrorException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x0007E48C File Offset: 0x0007D88C
		private static EvaluateException _Eval(string error)
		{
			EvaluateException ex = new EvaluateException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x0007E4A8 File Offset: 0x0007D8A8
		private static EvaluateException _Eval(string error, Exception innerException)
		{
			EvaluateException ex = new EvaluateException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x0007E4C4 File Offset: 0x0007D8C4
		public static Exception InvokeArgument()
		{
			return ExceptionBuilder._Argument(Res.GetString("Expr_InvokeArgument"));
		}

		// Token: 0x06000FB0 RID: 4016 RVA: 0x0007E4E0 File Offset: 0x0007D8E0
		public static Exception NYI(string moreinfo)
		{
			string @string = Res.GetString("Expr_NYI", new object[]
			{
				moreinfo
			});
			return ExprException._Expr(@string);
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x0007E508 File Offset: 0x0007D908
		public static Exception MissingOperand(OperatorInfo before)
		{
			return ExprException._Syntax(Res.GetString("Expr_MissingOperand", new object[]
			{
				Operators.ToString(before.op)
			}));
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x0007E538 File Offset: 0x0007D938
		public static Exception MissingOperator(string token)
		{
			return ExprException._Syntax(Res.GetString("Expr_MissingOperand", new object[]
			{
				token
			}));
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x0007E560 File Offset: 0x0007D960
		public static Exception TypeMismatch(string expr)
		{
			return ExprException._Eval(Res.GetString("Expr_TypeMismatch", new object[]
			{
				expr
			}));
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x0007E588 File Offset: 0x0007D988
		public static Exception FunctionArgumentOutOfRange(string arg, string func)
		{
			return ExceptionBuilder._ArgumentOutOfRange(arg, Res.GetString("Expr_ArgumentOutofRange", new object[]
			{
				func
			}));
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x0007E5B0 File Offset: 0x0007D9B0
		public static Exception ExpressionTooComplex()
		{
			return ExprException._Eval(Res.GetString("Expr_ExpressionTooComplex"));
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x0007E5CC File Offset: 0x0007D9CC
		public static Exception UnboundName(string name)
		{
			return ExprException._Eval(Res.GetString("Expr_UnboundName", new object[]
			{
				name
			}));
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x0007E5F4 File Offset: 0x0007D9F4
		public static Exception InvalidString(string str)
		{
			return ExprException._Syntax(Res.GetString("Expr_InvalidString", new object[]
			{
				str
			}));
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x0007E61C File Offset: 0x0007DA1C
		public static Exception UndefinedFunction(string name)
		{
			return ExprException._Eval(Res.GetString("Expr_UndefinedFunction", new object[]
			{
				name
			}));
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x0007E644 File Offset: 0x0007DA44
		public static Exception SyntaxError()
		{
			return ExprException._Syntax(Res.GetString("Expr_Syntax"));
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x0007E660 File Offset: 0x0007DA60
		public static Exception FunctionArgumentCount(string name)
		{
			return ExprException._Eval(Res.GetString("Expr_FunctionArgumentCount", new object[]
			{
				name
			}));
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x0007E688 File Offset: 0x0007DA88
		public static Exception MissingRightParen()
		{
			return ExprException._Syntax(Res.GetString("Expr_MissingRightParen"));
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x0007E6A4 File Offset: 0x0007DAA4
		public static Exception UnknownToken(string token, int position)
		{
			return ExprException._Syntax(Res.GetString("Expr_UnknownToken", new object[]
			{
				token,
				position.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x0007E6DC File Offset: 0x0007DADC
		public static Exception UnknownToken(Tokens tokExpected, Tokens tokCurr, int position)
		{
			return ExprException._Syntax(Res.GetString("Expr_UnknownToken1", new object[]
			{
				tokExpected.ToString(),
				tokCurr.ToString(),
				position.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x0007E730 File Offset: 0x0007DB30
		public static Exception DatatypeConvertion(Type type1, Type type2)
		{
			return ExprException._Eval(Res.GetString("Expr_DatatypeConvertion", new object[]
			{
				type1.ToString(),
				type2.ToString()
			}));
		}

		// Token: 0x06000FBF RID: 4031 RVA: 0x0007E764 File Offset: 0x0007DB64
		public static Exception DatavalueConvertion(object value, Type type, Exception innerException)
		{
			return ExprException._Eval(Res.GetString("Expr_DatavalueConvertion", new object[]
			{
				value.ToString(),
				type.ToString()
			}), innerException);
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x0007E79C File Offset: 0x0007DB9C
		public static Exception InvalidName(string name)
		{
			return ExprException._Syntax(Res.GetString("Expr_InvalidName", new object[]
			{
				name
			}));
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x0007E7C4 File Offset: 0x0007DBC4
		public static Exception InvalidDate(string date)
		{
			return ExprException._Syntax(Res.GetString("Expr_InvalidDate", new object[]
			{
				date
			}));
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x0007E7EC File Offset: 0x0007DBEC
		public static Exception NonConstantArgument()
		{
			return ExprException._Eval(Res.GetString("Expr_NonConstantArgument"));
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x0007E808 File Offset: 0x0007DC08
		public static Exception InvalidPattern(string pat)
		{
			return ExprException._Eval(Res.GetString("Expr_InvalidPattern", new object[]
			{
				pat
			}));
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x0007E830 File Offset: 0x0007DC30
		public static Exception InWithoutParentheses()
		{
			return ExprException._Syntax(Res.GetString("Expr_InWithoutParentheses"));
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x0007E84C File Offset: 0x0007DC4C
		public static Exception InWithoutList()
		{
			return ExprException._Syntax(Res.GetString("Expr_InWithoutList"));
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x0007E868 File Offset: 0x0007DC68
		public static Exception InvalidIsSyntax()
		{
			return ExprException._Syntax(Res.GetString("Expr_IsSyntax"));
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x0007E884 File Offset: 0x0007DC84
		public static Exception Overflow(Type type)
		{
			return ExprException._Overflow(Res.GetString("Expr_Overflow", new object[]
			{
				type.Name
			}));
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x0007E8B0 File Offset: 0x0007DCB0
		public static Exception ArgumentType(string function, int arg, Type type)
		{
			return ExprException._Eval(Res.GetString("Expr_ArgumentType", new object[]
			{
				function,
				arg.ToString(CultureInfo.InvariantCulture),
				type.ToString()
			}));
		}

		// Token: 0x06000FC9 RID: 4041 RVA: 0x0007E8F0 File Offset: 0x0007DCF0
		public static Exception ArgumentTypeInteger(string function, int arg)
		{
			return ExprException._Eval(Res.GetString("Expr_ArgumentTypeInteger", new object[]
			{
				function,
				arg.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x0007E928 File Offset: 0x0007DD28
		public static Exception TypeMismatchInBinop(int op, Type type1, Type type2)
		{
			return ExprException._Eval(Res.GetString("Expr_TypeMismatchInBinop", new object[]
			{
				Operators.ToString(op),
				type1.ToString(),
				type2.ToString()
			}));
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x0007E968 File Offset: 0x0007DD68
		public static Exception AmbiguousBinop(int op, Type type1, Type type2)
		{
			return ExprException._Eval(Res.GetString("Expr_AmbiguousBinop", new object[]
			{
				Operators.ToString(op),
				type1.ToString(),
				type2.ToString()
			}));
		}

		// Token: 0x06000FCC RID: 4044 RVA: 0x0007E9A8 File Offset: 0x0007DDA8
		public static Exception UnsupportedOperator(int op)
		{
			return ExprException._Eval(Res.GetString("Expr_UnsupportedOperator", new object[]
			{
				Operators.ToString(op)
			}));
		}

		// Token: 0x06000FCD RID: 4045 RVA: 0x0007E9D4 File Offset: 0x0007DDD4
		public static Exception InvalidNameBracketing(string name)
		{
			return ExprException._Syntax(Res.GetString("Expr_InvalidNameBracketing", new object[]
			{
				name
			}));
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x0007E9FC File Offset: 0x0007DDFC
		public static Exception MissingOperandBefore(string op)
		{
			return ExprException._Syntax(Res.GetString("Expr_MissingOperandBefore", new object[]
			{
				op
			}));
		}

		// Token: 0x06000FCF RID: 4047 RVA: 0x0007EA24 File Offset: 0x0007DE24
		public static Exception TooManyRightParentheses()
		{
			return ExprException._Syntax(Res.GetString("Expr_TooManyRightParentheses"));
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x0007EA40 File Offset: 0x0007DE40
		public static Exception UnresolvedRelation(string name, string expr)
		{
			return ExprException._Eval(Res.GetString("Expr_UnresolvedRelation", new object[]
			{
				name,
				expr
			}));
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x0007EA6C File Offset: 0x0007DE6C
		internal static EvaluateException BindFailure(string relationName)
		{
			return ExprException._Eval(Res.GetString("Expr_BindFailure", new object[]
			{
				relationName
			}));
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x0007EA94 File Offset: 0x0007DE94
		public static Exception AggregateArgument()
		{
			return ExprException._Syntax(Res.GetString("Expr_AggregateArgument"));
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x0007EAB0 File Offset: 0x0007DEB0
		public static Exception AggregateUnbound(string expr)
		{
			return ExprException._Eval(Res.GetString("Expr_AggregateUnbound", new object[]
			{
				expr
			}));
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x0007EAD8 File Offset: 0x0007DED8
		public static Exception EvalNoContext()
		{
			return ExprException._Eval(Res.GetString("Expr_EvalNoContext"));
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x0007EAF4 File Offset: 0x0007DEF4
		public static Exception ExpressionUnbound(string expr)
		{
			return ExprException._Eval(Res.GetString("Expr_ExpressionUnbound", new object[]
			{
				expr
			}));
		}

		// Token: 0x06000FD6 RID: 4054 RVA: 0x0007EB1C File Offset: 0x0007DF1C
		public static Exception ComputeNotAggregate(string expr)
		{
			return ExprException._Eval(Res.GetString("Expr_ComputeNotAggregate", new object[]
			{
				expr
			}));
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x0007EB44 File Offset: 0x0007DF44
		public static Exception FilterConvertion(string expr)
		{
			return ExprException._Eval(Res.GetString("Expr_FilterConvertion", new object[]
			{
				expr
			}));
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x0007EB6C File Offset: 0x0007DF6C
		public static Exception LookupArgument()
		{
			return ExprException._Syntax(Res.GetString("Expr_LookupArgument"));
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x0007EB88 File Offset: 0x0007DF88
		public static Exception InvalidType(string typeName)
		{
			return ExprException._Eval(Res.GetString("Expr_InvalidType", new object[]
			{
				typeName
			}));
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x0007EBB0 File Offset: 0x0007DFB0
		public static Exception InvalidHoursArgument()
		{
			return ExprException._Eval(Res.GetString("Expr_InvalidHoursArgument"));
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x0007EBCC File Offset: 0x0007DFCC
		public static Exception InvalidMinutesArgument()
		{
			return ExprException._Eval(Res.GetString("Expr_InvalidMinutesArgument"));
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x0007EBE8 File Offset: 0x0007DFE8
		public static Exception InvalidTimeZoneRange()
		{
			return ExprException._Eval(Res.GetString("Expr_InvalidTimeZoneRange"));
		}

		// Token: 0x06000FDD RID: 4061 RVA: 0x0007EC04 File Offset: 0x0007E004
		public static Exception MismatchKindandTimeSpan()
		{
			return ExprException._Eval(Res.GetString("Expr_MismatchKindandTimeSpan"));
		}

		// Token: 0x06000FDE RID: 4062 RVA: 0x0007EC20 File Offset: 0x0007E020
		public static Exception UnsupportedDataType(Type type)
		{
			return ExceptionBuilder._Argument(Res.GetString("Expr_UnsupportedType", new object[]
			{
				type.FullName
			}));
		}
	}
}
