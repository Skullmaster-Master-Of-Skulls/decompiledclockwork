using System;
using System.Globalization;

namespace System.Data
{
	// Token: 0x020001B1 RID: 433
	internal sealed class ExprException
	{
		// Token: 0x060018C0 RID: 6336 RVA: 0x00256248 File Offset: 0x00255648
		private ExprException()
		{
		}

		// Token: 0x060018C1 RID: 6337 RVA: 0x00256268 File Offset: 0x00255668
		private static OverflowException _Overflow(string error)
		{
			OverflowException ex = new OverflowException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x00256288 File Offset: 0x00255688
		private static InvalidExpressionException _Expr(string error)
		{
			InvalidExpressionException ex = new InvalidExpressionException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060018C3 RID: 6339 RVA: 0x002562A8 File Offset: 0x002556A8
		private static SyntaxErrorException _Syntax(string error)
		{
			SyntaxErrorException ex = new SyntaxErrorException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x002562C8 File Offset: 0x002556C8
		private static EvaluateException _Eval(string error)
		{
			EvaluateException ex = new EvaluateException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x002562E8 File Offset: 0x002556E8
		private static EvaluateException _Eval(string error, Exception innerException)
		{
			EvaluateException ex = new EvaluateException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060018C6 RID: 6342 RVA: 0x00256308 File Offset: 0x00255708
		public static Exception InvokeArgument()
		{
			return ExceptionBuilder._Argument(Res.GetString("Expr_InvokeArgument"));
		}

		// Token: 0x060018C7 RID: 6343 RVA: 0x00256328 File Offset: 0x00255728
		public static Exception NYI(string moreinfo)
		{
			string @string = Res.GetString("Expr_NYI", new object[]
			{
				moreinfo
			});
			return ExprException._Expr(@string);
		}

		// Token: 0x060018C8 RID: 6344 RVA: 0x00256358 File Offset: 0x00255758
		public static Exception MissingOperand(OperatorInfo before)
		{
			return ExprException._Syntax(Res.GetString("Expr_MissingOperand", new object[]
			{
				Operators.ToString(before.op)
			}));
		}

		// Token: 0x060018C9 RID: 6345 RVA: 0x00256398 File Offset: 0x00255798
		public static Exception MissingOperator(string token)
		{
			return ExprException._Syntax(Res.GetString("Expr_MissingOperand", new object[]
			{
				token
			}));
		}

		// Token: 0x060018CA RID: 6346 RVA: 0x002563C8 File Offset: 0x002557C8
		public static Exception TypeMismatch(string expr)
		{
			return ExprException._Eval(Res.GetString("Expr_TypeMismatch", new object[]
			{
				expr
			}));
		}

		// Token: 0x060018CB RID: 6347 RVA: 0x002563F8 File Offset: 0x002557F8
		public static Exception FunctionArgumentOutOfRange(string arg, string func)
		{
			return ExceptionBuilder._ArgumentOutOfRange(arg, Res.GetString("Expr_ArgumentOutofRange", new object[]
			{
				func
			}));
		}

		// Token: 0x060018CC RID: 6348 RVA: 0x00256428 File Offset: 0x00255828
		public static Exception ExpressionTooComplex()
		{
			return ExprException._Eval(Res.GetString("Expr_ExpressionTooComplex"));
		}

		// Token: 0x060018CD RID: 6349 RVA: 0x00256448 File Offset: 0x00255848
		public static Exception UnboundName(string name)
		{
			return ExprException._Eval(Res.GetString("Expr_UnboundName", new object[]
			{
				name
			}));
		}

		// Token: 0x060018CE RID: 6350 RVA: 0x00256478 File Offset: 0x00255878
		public static Exception InvalidString(string str)
		{
			return ExprException._Syntax(Res.GetString("Expr_InvalidString", new object[]
			{
				str
			}));
		}

		// Token: 0x060018CF RID: 6351 RVA: 0x002564A8 File Offset: 0x002558A8
		public static Exception UndefinedFunction(string name)
		{
			return ExprException._Eval(Res.GetString("Expr_UndefinedFunction", new object[]
			{
				name
			}));
		}

		// Token: 0x060018D0 RID: 6352 RVA: 0x002564D8 File Offset: 0x002558D8
		public static Exception SyntaxError()
		{
			return ExprException._Syntax(Res.GetString("Expr_Syntax"));
		}

		// Token: 0x060018D1 RID: 6353 RVA: 0x002564F8 File Offset: 0x002558F8
		public static Exception FunctionArgumentCount(string name)
		{
			return ExprException._Eval(Res.GetString("Expr_FunctionArgumentCount", new object[]
			{
				name
			}));
		}

		// Token: 0x060018D2 RID: 6354 RVA: 0x00256528 File Offset: 0x00255928
		public static Exception MissingRightParen()
		{
			return ExprException._Syntax(Res.GetString("Expr_MissingRightParen"));
		}

		// Token: 0x060018D3 RID: 6355 RVA: 0x00256548 File Offset: 0x00255948
		public static Exception UnknownToken(string token, int position)
		{
			return ExprException._Syntax(Res.GetString("Expr_UnknownToken", new object[]
			{
				token,
				position.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x060018D4 RID: 6356 RVA: 0x00256588 File Offset: 0x00255988
		public static Exception UnknownToken(Tokens tokExpected, Tokens tokCurr, int position)
		{
			return ExprException._Syntax(Res.GetString("Expr_UnknownToken1", new object[]
			{
				tokExpected.ToString(),
				tokCurr.ToString(),
				position.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x060018D5 RID: 6357 RVA: 0x002565D8 File Offset: 0x002559D8
		public static Exception DatatypeConvertion(Type type1, Type type2)
		{
			return ExprException._Eval(Res.GetString("Expr_DatatypeConvertion", new object[]
			{
				type1.ToString(),
				type2.ToString()
			}));
		}

		// Token: 0x060018D6 RID: 6358 RVA: 0x00256618 File Offset: 0x00255A18
		public static Exception DatavalueConvertion(object value, Type type, Exception innerException)
		{
			return ExprException._Eval(Res.GetString("Expr_DatavalueConvertion", new object[]
			{
				value.ToString(),
				type.ToString()
			}), innerException);
		}

		// Token: 0x060018D7 RID: 6359 RVA: 0x00256658 File Offset: 0x00255A58
		public static Exception InvalidName(string name)
		{
			return ExprException._Syntax(Res.GetString("Expr_InvalidName", new object[]
			{
				name
			}));
		}

		// Token: 0x060018D8 RID: 6360 RVA: 0x00256688 File Offset: 0x00255A88
		public static Exception InvalidDate(string date)
		{
			return ExprException._Syntax(Res.GetString("Expr_InvalidDate", new object[]
			{
				date
			}));
		}

		// Token: 0x060018D9 RID: 6361 RVA: 0x002566B8 File Offset: 0x00255AB8
		public static Exception NonConstantArgument()
		{
			return ExprException._Eval(Res.GetString("Expr_NonConstantArgument"));
		}

		// Token: 0x060018DA RID: 6362 RVA: 0x002566D8 File Offset: 0x00255AD8
		public static Exception InvalidPattern(string pat)
		{
			return ExprException._Eval(Res.GetString("Expr_InvalidPattern", new object[]
			{
				pat
			}));
		}

		// Token: 0x060018DB RID: 6363 RVA: 0x00256708 File Offset: 0x00255B08
		public static Exception InWithoutParentheses()
		{
			return ExprException._Syntax(Res.GetString("Expr_InWithoutParentheses"));
		}

		// Token: 0x060018DC RID: 6364 RVA: 0x00256728 File Offset: 0x00255B28
		public static Exception InWithoutList()
		{
			return ExprException._Syntax(Res.GetString("Expr_InWithoutList"));
		}

		// Token: 0x060018DD RID: 6365 RVA: 0x00256748 File Offset: 0x00255B48
		public static Exception InvalidIsSyntax()
		{
			return ExprException._Syntax(Res.GetString("Expr_IsSyntax"));
		}

		// Token: 0x060018DE RID: 6366 RVA: 0x00256768 File Offset: 0x00255B68
		public static Exception Overflow(Type type)
		{
			return ExprException._Overflow(Res.GetString("Expr_Overflow", new object[]
			{
				type.Name
			}));
		}

		// Token: 0x060018DF RID: 6367 RVA: 0x00256798 File Offset: 0x00255B98
		public static Exception ArgumentType(string function, int arg, Type type)
		{
			return ExprException._Eval(Res.GetString("Expr_ArgumentType", new object[]
			{
				function,
				arg.ToString(CultureInfo.InvariantCulture),
				type.ToString()
			}));
		}

		// Token: 0x060018E0 RID: 6368 RVA: 0x002567D8 File Offset: 0x00255BD8
		public static Exception ArgumentTypeInteger(string function, int arg)
		{
			return ExprException._Eval(Res.GetString("Expr_ArgumentTypeInteger", new object[]
			{
				function,
				arg.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x060018E1 RID: 6369 RVA: 0x00256818 File Offset: 0x00255C18
		public static Exception TypeMismatchInBinop(int op, Type type1, Type type2)
		{
			return ExprException._Eval(Res.GetString("Expr_TypeMismatchInBinop", new object[]
			{
				Operators.ToString(op),
				type1.ToString(),
				type2.ToString()
			}));
		}

		// Token: 0x060018E2 RID: 6370 RVA: 0x00256858 File Offset: 0x00255C58
		public static Exception AmbiguousBinop(int op, Type type1, Type type2)
		{
			return ExprException._Eval(Res.GetString("Expr_AmbiguousBinop", new object[]
			{
				Operators.ToString(op),
				type1.ToString(),
				type2.ToString()
			}));
		}

		// Token: 0x060018E3 RID: 6371 RVA: 0x00256898 File Offset: 0x00255C98
		public static Exception UnsupportedOperator(int op)
		{
			return ExprException._Eval(Res.GetString("Expr_UnsupportedOperator", new object[]
			{
				Operators.ToString(op)
			}));
		}

		// Token: 0x060018E4 RID: 6372 RVA: 0x002568C8 File Offset: 0x00255CC8
		public static Exception InvalidNameBracketing(string name)
		{
			return ExprException._Syntax(Res.GetString("Expr_InvalidNameBracketing", new object[]
			{
				name
			}));
		}

		// Token: 0x060018E5 RID: 6373 RVA: 0x002568F8 File Offset: 0x00255CF8
		public static Exception MissingOperandBefore(string op)
		{
			return ExprException._Syntax(Res.GetString("Expr_MissingOperandBefore", new object[]
			{
				op
			}));
		}

		// Token: 0x060018E6 RID: 6374 RVA: 0x00256928 File Offset: 0x00255D28
		public static Exception TooManyRightParentheses()
		{
			return ExprException._Syntax(Res.GetString("Expr_TooManyRightParentheses"));
		}

		// Token: 0x060018E7 RID: 6375 RVA: 0x00256948 File Offset: 0x00255D48
		public static Exception UnresolvedRelation(string name, string expr)
		{
			return ExprException._Eval(Res.GetString("Expr_UnresolvedRelation", new object[]
			{
				name,
				expr
			}));
		}

		// Token: 0x060018E8 RID: 6376 RVA: 0x00256978 File Offset: 0x00255D78
		internal static EvaluateException BindFailure(string relationName)
		{
			return ExprException._Eval(Res.GetString("Expr_BindFailure", new object[]
			{
				relationName
			}));
		}

		// Token: 0x060018E9 RID: 6377 RVA: 0x002569A8 File Offset: 0x00255DA8
		public static Exception AggregateArgument()
		{
			return ExprException._Syntax(Res.GetString("Expr_AggregateArgument"));
		}

		// Token: 0x060018EA RID: 6378 RVA: 0x002569C8 File Offset: 0x00255DC8
		public static Exception AggregateUnbound(string expr)
		{
			return ExprException._Eval(Res.GetString("Expr_AggregateUnbound", new object[]
			{
				expr
			}));
		}

		// Token: 0x060018EB RID: 6379 RVA: 0x002569F8 File Offset: 0x00255DF8
		public static Exception EvalNoContext()
		{
			return ExprException._Eval(Res.GetString("Expr_EvalNoContext"));
		}

		// Token: 0x060018EC RID: 6380 RVA: 0x00256A18 File Offset: 0x00255E18
		public static Exception ExpressionUnbound(string expr)
		{
			return ExprException._Eval(Res.GetString("Expr_ExpressionUnbound", new object[]
			{
				expr
			}));
		}

		// Token: 0x060018ED RID: 6381 RVA: 0x00256A48 File Offset: 0x00255E48
		public static Exception ComputeNotAggregate(string expr)
		{
			return ExprException._Eval(Res.GetString("Expr_ComputeNotAggregate", new object[]
			{
				expr
			}));
		}

		// Token: 0x060018EE RID: 6382 RVA: 0x00256A78 File Offset: 0x00255E78
		public static Exception FilterConvertion(string expr)
		{
			return ExprException._Eval(Res.GetString("Expr_FilterConvertion", new object[]
			{
				expr
			}));
		}

		// Token: 0x060018EF RID: 6383 RVA: 0x00256AA8 File Offset: 0x00255EA8
		public static Exception LookupArgument()
		{
			return ExprException._Syntax(Res.GetString("Expr_LookupArgument"));
		}

		// Token: 0x060018F0 RID: 6384 RVA: 0x00256AC8 File Offset: 0x00255EC8
		public static Exception InvalidType(string typeName)
		{
			return ExprException._Eval(Res.GetString("Expr_InvalidType", new object[]
			{
				typeName
			}));
		}

		// Token: 0x060018F1 RID: 6385 RVA: 0x00256AF8 File Offset: 0x00255EF8
		public static Exception InvalidHoursArgument()
		{
			return ExprException._Eval(Res.GetString("Expr_InvalidHoursArgument"));
		}

		// Token: 0x060018F2 RID: 6386 RVA: 0x00256B18 File Offset: 0x00255F18
		public static Exception InvalidMinutesArgument()
		{
			return ExprException._Eval(Res.GetString("Expr_InvalidMinutesArgument"));
		}

		// Token: 0x060018F3 RID: 6387 RVA: 0x00256B38 File Offset: 0x00255F38
		public static Exception InvalidTimeZoneRange()
		{
			return ExprException._Eval(Res.GetString("Expr_InvalidTimeZoneRange"));
		}

		// Token: 0x060018F4 RID: 6388 RVA: 0x00256B58 File Offset: 0x00255F58
		public static Exception MismatchKindandTimeSpan()
		{
			return ExprException._Eval(Res.GetString("Expr_MismatchKindandTimeSpan"));
		}
	}
}
