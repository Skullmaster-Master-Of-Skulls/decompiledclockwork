using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Data.SqlTypes;
using System.Design;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace System.Data.Design
{
	// Token: 0x02000218 RID: 536
	internal sealed class CodeGenHelper
	{
		// Token: 0x0600139A RID: 5018 RVA: 0x0000362F File Offset: 0x0000182F
		private CodeGenHelper()
		{
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x0006FDA4 File Offset: 0x0006DFA4
		internal static CodeExpression This()
		{
			return new CodeThisReferenceExpression();
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x0006FDAB File Offset: 0x0006DFAB
		internal static CodeExpression Base()
		{
			return new CodeBaseReferenceExpression();
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x0006FDB2 File Offset: 0x0006DFB2
		internal static CodeExpression Value()
		{
			return new CodePropertySetValueReferenceExpression();
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x0006FDB9 File Offset: 0x0006DFB9
		internal static CodeTypeReference Type(string type)
		{
			return new CodeTypeReference(type);
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x0006FDC1 File Offset: 0x0006DFC1
		internal static CodeTypeReference Type(Type type)
		{
			return new CodeTypeReference(type);
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x0006FDCC File Offset: 0x0006DFCC
		internal static CodeTypeReference NullableType(Type type)
		{
			return new CodeTypeReference(typeof(Nullable))
			{
				Options = CodeTypeReferenceOptions.GlobalReference,
				TypeArguments = 
				{
					CodeGenHelper.GlobalType(type)
				}
			};
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x0006FE03 File Offset: 0x0006E003
		internal static CodeTypeReference Type(string type, int rank)
		{
			return new CodeTypeReference(type, rank);
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x0006FE0C File Offset: 0x0006E00C
		internal static CodeTypeReference GlobalType(Type type)
		{
			return new CodeTypeReference(type.ToString(), CodeTypeReferenceOptions.GlobalReference);
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x0006FE1A File Offset: 0x0006E01A
		internal static CodeTypeReference GlobalType(Type type, int rank)
		{
			return new CodeTypeReference(CodeGenHelper.GlobalType(type), rank);
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x0006FE28 File Offset: 0x0006E028
		internal static CodeTypeReference GlobalType(string type)
		{
			return new CodeTypeReference(type, CodeTypeReferenceOptions.GlobalReference);
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x0006FE31 File Offset: 0x0006E031
		internal static CodeTypeReferenceExpression TypeExpr(CodeTypeReference type)
		{
			return new CodeTypeReferenceExpression(type);
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x0006FE39 File Offset: 0x0006E039
		internal static CodeTypeReferenceExpression GlobalTypeExpr(Type type)
		{
			return new CodeTypeReferenceExpression(CodeGenHelper.GlobalType(type));
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x0006FE46 File Offset: 0x0006E046
		internal static CodeTypeReferenceExpression GlobalTypeExpr(string type)
		{
			return new CodeTypeReferenceExpression(CodeGenHelper.GlobalType(type));
		}

		// Token: 0x060013A8 RID: 5032 RVA: 0x0006FE53 File Offset: 0x0006E053
		internal static CodeTypeReference GlobalGenericType(string fullTypeName, Type itemType)
		{
			return CodeGenHelper.GlobalGenericType(fullTypeName, CodeGenHelper.GlobalType(itemType));
		}

		// Token: 0x060013A9 RID: 5033 RVA: 0x0006FE64 File Offset: 0x0006E064
		internal static CodeTypeReference GlobalGenericType(string fullTypeName, CodeTypeReference itemType)
		{
			return new CodeTypeReference(fullTypeName, new CodeTypeReference[]
			{
				itemType
			})
			{
				Options = CodeTypeReferenceOptions.GlobalReference
			};
		}

		// Token: 0x060013AA RID: 5034 RVA: 0x0006FE8A File Offset: 0x0006E08A
		internal static CodeExpression Cast(CodeTypeReference type, CodeExpression expr)
		{
			return new CodeCastExpression(type, expr);
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x0006FE93 File Offset: 0x0006E093
		internal static CodeExpression TypeOf(CodeTypeReference type)
		{
			return new CodeTypeOfExpression(type);
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x0006FE9B File Offset: 0x0006E09B
		internal static CodeExpression Field(CodeExpression exp, string field)
		{
			return new CodeFieldReferenceExpression(exp, field);
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x0006FEA4 File Offset: 0x0006E0A4
		internal static CodeExpression ThisField(string field)
		{
			return new CodeFieldReferenceExpression(CodeGenHelper.This(), field);
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x0006FEB1 File Offset: 0x0006E0B1
		internal static CodeExpression Property(CodeExpression exp, string property)
		{
			return new CodePropertyReferenceExpression(exp, property);
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x0006FEBA File Offset: 0x0006E0BA
		internal static CodeExpression ThisProperty(string property)
		{
			return new CodePropertyReferenceExpression(CodeGenHelper.This(), property);
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x0006FEC7 File Offset: 0x0006E0C7
		internal static CodeExpression Argument(string argument)
		{
			return new CodeArgumentReferenceExpression(argument);
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x0006FECF File Offset: 0x0006E0CF
		internal static CodeExpression Variable(string variable)
		{
			return new CodeVariableReferenceExpression(variable);
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x0006FED7 File Offset: 0x0006E0D7
		internal static CodeExpression Event(string eventName)
		{
			return new CodeEventReferenceExpression(CodeGenHelper.This(), eventName);
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x0006FEE4 File Offset: 0x0006E0E4
		internal static CodeExpression New(CodeTypeReference type, CodeExpression[] parameters)
		{
			return new CodeObjectCreateExpression(type, parameters);
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x0006FEED File Offset: 0x0006E0ED
		internal static CodeExpression NewArray(CodeTypeReference type, int size)
		{
			return new CodeArrayCreateExpression(type, size);
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x0006FEF6 File Offset: 0x0006E0F6
		internal static CodeExpression NewArray(CodeTypeReference type, params CodeExpression[] initializers)
		{
			return new CodeArrayCreateExpression(type, initializers);
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x0006FEFF File Offset: 0x0006E0FF
		internal static CodeExpression Primitive(object primitive)
		{
			return new CodePrimitiveExpression(primitive);
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x0006FF07 File Offset: 0x0006E107
		internal static CodeExpression Str(string str)
		{
			return CodeGenHelper.Primitive(str);
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x0006FF0F File Offset: 0x0006E10F
		internal static CodeExpression MethodCall(CodeExpression targetObject, string methodName, CodeExpression[] parameters)
		{
			return new CodeMethodInvokeExpression(targetObject, methodName, parameters);
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x0006FF19 File Offset: 0x0006E119
		internal static CodeStatement MethodCallStm(CodeExpression targetObject, string methodName, CodeExpression[] parameters)
		{
			return CodeGenHelper.Stm(CodeGenHelper.MethodCall(targetObject, methodName, parameters));
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x0006FF28 File Offset: 0x0006E128
		internal static CodeExpression MethodCall(CodeExpression targetObject, string methodName)
		{
			return new CodeMethodInvokeExpression(targetObject, methodName, new CodeExpression[0]);
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x0006FF37 File Offset: 0x0006E137
		internal static CodeStatement MethodCallStm(CodeExpression targetObject, string methodName)
		{
			return CodeGenHelper.Stm(CodeGenHelper.MethodCall(targetObject, methodName));
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x0006FF45 File Offset: 0x0006E145
		internal static CodeExpression MethodCall(CodeExpression targetObject, string methodName, CodeExpression par)
		{
			return new CodeMethodInvokeExpression(targetObject, methodName, new CodeExpression[]
			{
				par
			});
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x0006FF58 File Offset: 0x0006E158
		internal static CodeStatement MethodCallStm(CodeExpression targetObject, string methodName, CodeExpression par)
		{
			return CodeGenHelper.Stm(CodeGenHelper.MethodCall(targetObject, methodName, par));
		}

		// Token: 0x060013BE RID: 5054 RVA: 0x0006FF67 File Offset: 0x0006E167
		internal static CodeExpression DelegateCall(CodeExpression targetObject, CodeExpression par)
		{
			return new CodeDelegateInvokeExpression(targetObject, new CodeExpression[]
			{
				CodeGenHelper.This(),
				par
			});
		}

		// Token: 0x060013BF RID: 5055 RVA: 0x0006FF81 File Offset: 0x0006E181
		internal static CodeExpression Indexer(CodeExpression targetObject, CodeExpression indices)
		{
			return new CodeIndexerExpression(targetObject, new CodeExpression[]
			{
				indices
			});
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x0006FF93 File Offset: 0x0006E193
		internal static CodeExpression ArrayIndexer(CodeExpression targetObject, CodeExpression indices)
		{
			return new CodeArrayIndexerExpression(targetObject, new CodeExpression[]
			{
				indices
			});
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x0006FFA5 File Offset: 0x0006E1A5
		internal static CodeExpression ReferenceEquals(CodeExpression left, CodeExpression right)
		{
			return CodeGenHelper.MethodCall(CodeGenHelper.GlobalTypeExpr(typeof(object)), "ReferenceEquals", new CodeExpression[]
			{
				left,
				right
			});
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x0006FFCE File Offset: 0x0006E1CE
		internal static CodeExpression ReferenceNotEquals(CodeExpression left, CodeExpression right)
		{
			return CodeGenHelper.EQ(CodeGenHelper.ReferenceEquals(left, right), CodeGenHelper.Primitive(false));
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x0006FFE7 File Offset: 0x0006E1E7
		internal static CodeBinaryOperatorExpression BinOperator(CodeExpression left, CodeBinaryOperatorType op, CodeExpression right)
		{
			return new CodeBinaryOperatorExpression(left, op, right);
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x0006FFF1 File Offset: 0x0006E1F1
		internal static CodeBinaryOperatorExpression IdNotEQ(CodeExpression left, CodeExpression right)
		{
			return CodeGenHelper.BinOperator(left, CodeBinaryOperatorType.IdentityInequality, right);
		}

		// Token: 0x060013C5 RID: 5061 RVA: 0x0006FFFB File Offset: 0x0006E1FB
		internal static CodeBinaryOperatorExpression IdEQ(CodeExpression left, CodeExpression right)
		{
			return CodeGenHelper.BinOperator(left, CodeBinaryOperatorType.IdentityEquality, right);
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x00070005 File Offset: 0x0006E205
		internal static CodeBinaryOperatorExpression IdIsNull(CodeExpression id)
		{
			return CodeGenHelper.IdEQ(id, CodeGenHelper.Primitive(null));
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x00070013 File Offset: 0x0006E213
		internal static CodeBinaryOperatorExpression IdIsNotNull(CodeExpression id)
		{
			return CodeGenHelper.IdNotEQ(id, CodeGenHelper.Primitive(null));
		}

		// Token: 0x060013C8 RID: 5064 RVA: 0x00070021 File Offset: 0x0006E221
		internal static CodeBinaryOperatorExpression EQ(CodeExpression left, CodeExpression right)
		{
			return CodeGenHelper.BinOperator(left, CodeBinaryOperatorType.ValueEquality, right);
		}

		// Token: 0x060013C9 RID: 5065 RVA: 0x0007002B File Offset: 0x0006E22B
		internal static CodeBinaryOperatorExpression NotEQ(CodeExpression left, CodeExpression right)
		{
			return CodeGenHelper.EQ(CodeGenHelper.EQ(left, right), CodeGenHelper.Primitive(false));
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x00070044 File Offset: 0x0006E244
		internal static CodeBinaryOperatorExpression BitwiseAnd(CodeExpression left, CodeExpression right)
		{
			return CodeGenHelper.BinOperator(left, CodeBinaryOperatorType.BitwiseAnd, right);
		}

		// Token: 0x060013CB RID: 5067 RVA: 0x0007004F File Offset: 0x0006E24F
		internal static CodeBinaryOperatorExpression And(CodeExpression left, CodeExpression right)
		{
			return CodeGenHelper.BinOperator(left, CodeBinaryOperatorType.BooleanAnd, right);
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x0007005A File Offset: 0x0006E25A
		internal static CodeBinaryOperatorExpression Or(CodeExpression left, CodeExpression right)
		{
			return CodeGenHelper.BinOperator(left, CodeBinaryOperatorType.BooleanOr, right);
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x00070065 File Offset: 0x0006E265
		internal static CodeBinaryOperatorExpression Less(CodeExpression left, CodeExpression right)
		{
			return CodeGenHelper.BinOperator(left, CodeBinaryOperatorType.LessThan, right);
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x00070070 File Offset: 0x0006E270
		internal static CodeStatement Stm(CodeExpression expr)
		{
			return new CodeExpressionStatement(expr);
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x00070078 File Offset: 0x0006E278
		internal static CodeStatement Return(CodeExpression expr)
		{
			return new CodeMethodReturnStatement(expr);
		}

		// Token: 0x060013D0 RID: 5072 RVA: 0x00070080 File Offset: 0x0006E280
		internal static CodeStatement Return()
		{
			return new CodeMethodReturnStatement();
		}

		// Token: 0x060013D1 RID: 5073 RVA: 0x00070087 File Offset: 0x0006E287
		internal static CodeStatement Assign(CodeExpression left, CodeExpression right)
		{
			return new CodeAssignStatement(left, right);
		}

		// Token: 0x060013D2 RID: 5074 RVA: 0x00070090 File Offset: 0x0006E290
		internal static CodeStatement Throw(CodeTypeReference exception, string arg)
		{
			return new CodeThrowExceptionStatement(CodeGenHelper.New(exception, new CodeExpression[]
			{
				CodeGenHelper.Str(arg)
			}));
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x000700AC File Offset: 0x0006E2AC
		internal static CodeStatement Throw(CodeTypeReference exception, string arg, string inner)
		{
			return new CodeThrowExceptionStatement(CodeGenHelper.New(exception, new CodeExpression[]
			{
				CodeGenHelper.Str(arg),
				CodeGenHelper.Variable(inner)
			}));
		}

		// Token: 0x060013D4 RID: 5076 RVA: 0x000700D1 File Offset: 0x0006E2D1
		internal static CodeStatement Throw(CodeTypeReference exception, string arg, CodeExpression inner)
		{
			return new CodeThrowExceptionStatement(CodeGenHelper.New(exception, new CodeExpression[]
			{
				CodeGenHelper.Str(arg),
				inner
			}));
		}

		// Token: 0x060013D5 RID: 5077 RVA: 0x000700F1 File Offset: 0x0006E2F1
		internal static CodeCommentStatement Comment(string comment, bool docSummary)
		{
			if (docSummary)
			{
				return new CodeCommentStatement("<summary>\r\n" + comment + "\r\n</summary>", docSummary);
			}
			return new CodeCommentStatement(comment);
		}

		// Token: 0x060013D6 RID: 5078 RVA: 0x00070113 File Offset: 0x0006E313
		internal static CodeStatement If(CodeExpression cond, CodeStatement[] trueStms, CodeStatement[] falseStms)
		{
			return new CodeConditionStatement(cond, trueStms, falseStms);
		}

		// Token: 0x060013D7 RID: 5079 RVA: 0x0007011D File Offset: 0x0006E31D
		internal static CodeStatement If(CodeExpression cond, CodeStatement trueStm, CodeStatement falseStm)
		{
			return new CodeConditionStatement(cond, new CodeStatement[]
			{
				trueStm
			}, new CodeStatement[]
			{
				falseStm
			});
		}

		// Token: 0x060013D8 RID: 5080 RVA: 0x00070139 File Offset: 0x0006E339
		internal static CodeStatement If(CodeExpression cond, CodeStatement[] trueStms)
		{
			return new CodeConditionStatement(cond, trueStms);
		}

		// Token: 0x060013D9 RID: 5081 RVA: 0x00070142 File Offset: 0x0006E342
		internal static CodeStatement If(CodeExpression cond, CodeStatement trueStm)
		{
			return CodeGenHelper.If(cond, new CodeStatement[]
			{
				trueStm
			});
		}

		// Token: 0x060013DA RID: 5082 RVA: 0x00070154 File Offset: 0x0006E354
		internal static CodeMemberField FieldDecl(CodeTypeReference type, string name)
		{
			return new CodeMemberField(type, name);
		}

		// Token: 0x060013DB RID: 5083 RVA: 0x00070160 File Offset: 0x0006E360
		internal static CodeMemberField FieldDecl(CodeTypeReference type, string name, CodeExpression initExpr)
		{
			return new CodeMemberField(type, name)
			{
				InitExpression = initExpr
			};
		}

		// Token: 0x060013DC RID: 5084 RVA: 0x00070180 File Offset: 0x0006E380
		internal static CodeTypeDeclaration Class(string name, bool isPartial, TypeAttributes typeAttributes)
		{
			CodeTypeDeclaration codeTypeDeclaration = new CodeTypeDeclaration(name);
			codeTypeDeclaration.IsPartial = isPartial;
			codeTypeDeclaration.TypeAttributes = typeAttributes;
			if (!codeTypeDeclaration.IsPartial)
			{
				codeTypeDeclaration.CustomAttributes.Add(CodeGenHelper.GeneratedCodeAttributeDecl());
			}
			return codeTypeDeclaration;
		}

		// Token: 0x060013DD RID: 5085 RVA: 0x000701BC File Offset: 0x0006E3BC
		internal static CodeConstructor Constructor(MemberAttributes attributes)
		{
			return new CodeConstructor
			{
				Attributes = attributes,
				CustomAttributes = 
				{
					CodeGenHelper.AttributeDecl(typeof(DebuggerNonUserCodeAttribute).FullName),
					CodeGenHelper.GeneratedCodeAttributeDecl()
				}
			};
		}

		// Token: 0x060013DE RID: 5086 RVA: 0x00070208 File Offset: 0x0006E408
		internal static CodeMemberMethod MethodDecl(CodeTypeReference type, string name, MemberAttributes attributes)
		{
			return new CodeMemberMethod
			{
				ReturnType = type,
				Name = name,
				Attributes = attributes,
				CustomAttributes = 
				{
					CodeGenHelper.AttributeDecl(typeof(DebuggerNonUserCodeAttribute).FullName),
					CodeGenHelper.GeneratedCodeAttributeDecl()
				}
			};
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x00070264 File Offset: 0x0006E464
		internal static CodeMemberProperty PropertyDecl(CodeTypeReference type, string name, MemberAttributes attributes)
		{
			return new CodeMemberProperty
			{
				Type = type,
				Name = name,
				Attributes = attributes,
				CustomAttributes = 
				{
					CodeGenHelper.AttributeDecl(typeof(DebuggerNonUserCodeAttribute).FullName),
					CodeGenHelper.GeneratedCodeAttributeDecl()
				}
			};
		}

		// Token: 0x060013E0 RID: 5088 RVA: 0x000702BE File Offset: 0x0006E4BE
		internal static CodeStatement VariableDecl(CodeTypeReference type, string name)
		{
			return new CodeVariableDeclarationStatement(type, name);
		}

		// Token: 0x060013E1 RID: 5089 RVA: 0x000702C7 File Offset: 0x0006E4C7
		internal static CodeStatement VariableDecl(CodeTypeReference type, string name, CodeExpression initExpr)
		{
			return new CodeVariableDeclarationStatement(type, name, initExpr);
		}

		// Token: 0x060013E2 RID: 5090 RVA: 0x000702D1 File Offset: 0x0006E4D1
		internal static CodeStatement ForLoop(CodeStatement initStmt, CodeExpression testExpression, CodeStatement incrementStmt, CodeStatement[] statements)
		{
			return new CodeIterationStatement(initStmt, testExpression, incrementStmt, statements);
		}

		// Token: 0x060013E3 RID: 5091 RVA: 0x000702DC File Offset: 0x0006E4DC
		internal static CodeMemberEvent EventDecl(string type, string name)
		{
			return new CodeMemberEvent
			{
				Name = name,
				Type = CodeGenHelper.Type(type),
				Attributes = (MemberAttributes)24578,
				CustomAttributes = 
				{
					CodeGenHelper.GeneratedCodeAttributeDecl()
				}
			};
		}

		// Token: 0x060013E4 RID: 5092 RVA: 0x0007031F File Offset: 0x0006E51F
		internal static CodeParameterDeclarationExpression ParameterDecl(CodeTypeReference type, string name)
		{
			return new CodeParameterDeclarationExpression(type, name);
		}

		// Token: 0x060013E5 RID: 5093 RVA: 0x00070328 File Offset: 0x0006E528
		internal static CodeAttributeDeclaration AttributeDecl(string name)
		{
			return new CodeAttributeDeclaration(CodeGenHelper.GlobalType(name));
		}

		// Token: 0x060013E6 RID: 5094 RVA: 0x00070335 File Offset: 0x0006E535
		internal static CodeAttributeDeclaration AttributeDecl(string name, CodeExpression value)
		{
			return new CodeAttributeDeclaration(CodeGenHelper.GlobalType(name), new CodeAttributeArgument[]
			{
				new CodeAttributeArgument(value)
			});
		}

		// Token: 0x060013E7 RID: 5095 RVA: 0x00070351 File Offset: 0x0006E551
		internal static CodeAttributeDeclaration AttributeDecl(string name, CodeExpression value1, CodeExpression value2)
		{
			return new CodeAttributeDeclaration(CodeGenHelper.GlobalType(name), new CodeAttributeArgument[]
			{
				new CodeAttributeArgument(value1),
				new CodeAttributeArgument(value2)
			});
		}

		// Token: 0x060013E8 RID: 5096 RVA: 0x00070376 File Offset: 0x0006E576
		internal static CodeAttributeDeclaration GeneratedCodeAttributeDecl()
		{
			return CodeGenHelper.AttributeDecl(typeof(GeneratedCodeAttribute).FullName, CodeGenHelper.Str(typeof(TypedDataSetGenerator).FullName), CodeGenHelper.Str("4.0.0.0"));
		}

		// Token: 0x060013E9 RID: 5097 RVA: 0x000703AA File Offset: 0x0006E5AA
		internal static CodeStatement Try(CodeStatement tryStmnt, CodeCatchClause catchClause)
		{
			return new CodeTryCatchFinallyStatement(new CodeStatement[]
			{
				tryStmnt
			}, new CodeCatchClause[]
			{
				catchClause
			});
		}

		// Token: 0x060013EA RID: 5098 RVA: 0x000703C5 File Offset: 0x0006E5C5
		internal static CodeStatement Try(CodeStatement[] tryStmnts, CodeCatchClause[] catchClauses, CodeStatement[] finallyStmnts)
		{
			return new CodeTryCatchFinallyStatement(tryStmnts, catchClauses, finallyStmnts);
		}

		// Token: 0x060013EB RID: 5099 RVA: 0x000703D0 File Offset: 0x0006E5D0
		internal static CodeCatchClause Catch(CodeTypeReference type, string name, CodeStatement catchStmnt)
		{
			CodeCatchClause codeCatchClause = new CodeCatchClause();
			codeCatchClause.CatchExceptionType = type;
			codeCatchClause.LocalName = name;
			if (catchStmnt != null)
			{
				codeCatchClause.Statements.Add(catchStmnt);
			}
			return codeCatchClause;
		}

		// Token: 0x060013EC RID: 5100 RVA: 0x00070404 File Offset: 0x0006E604
		internal static FieldDirection ParameterDirectionToFieldDirection(ParameterDirection paramDirection)
		{
			switch (paramDirection)
			{
			case ParameterDirection.Input:
				return FieldDirection.In;
			case ParameterDirection.Output:
				return FieldDirection.Out;
			case ParameterDirection.InputOutput:
				return FieldDirection.Ref;
			case ParameterDirection.ReturnValue:
				throw new InternalException("Can't map from ParameterDirection.ReturnValue to FieldDirection.");
			}
			throw new InternalException("Unknown ParameterDirection.");
		}

		// Token: 0x060013ED RID: 5101 RVA: 0x00070458 File Offset: 0x0006E658
		internal static CodeExpression GenerateDbNullCheck(CodeExpression returnParam)
		{
			return CodeGenHelper.Or(CodeGenHelper.IdEQ(returnParam, CodeGenHelper.Primitive(null)), CodeGenHelper.IdEQ(CodeGenHelper.MethodCall(returnParam, "GetType"), CodeGenHelper.TypeOf(CodeGenHelper.GlobalType(typeof(DBNull)))));
		}

		// Token: 0x060013EE RID: 5102 RVA: 0x00070490 File Offset: 0x0006E690
		internal static CodeExpression GenerateNullExpression(Type returnType)
		{
			if (CodeGenHelper.IsSqlType(returnType))
			{
				return CodeGenHelper.Field(CodeGenHelper.GlobalTypeExpr(returnType), "Null");
			}
			if (returnType == typeof(object))
			{
				return CodeGenHelper.Field(CodeGenHelper.GlobalTypeExpr(typeof(DBNull)), "Value");
			}
			if (!returnType.IsValueType)
			{
				return CodeGenHelper.Primitive(null);
			}
			return null;
		}

		// Token: 0x060013EF RID: 5103 RVA: 0x000704F4 File Offset: 0x0006E6F4
		internal static CodeExpression GenerateConvertExpression(CodeExpression sourceExpression, Type sourceType, Type targetType)
		{
			if (sourceType == targetType)
			{
				return sourceExpression;
			}
			if (CodeGenHelper.IsSqlType(sourceType))
			{
				if (CodeGenHelper.IsSqlType(targetType))
				{
					throw new InternalException("Cannot perform the conversion between 2 SqlTypes.");
				}
				PropertyInfo property = sourceType.GetProperty("Value");
				if (property == null)
				{
					throw new InternalException("Type does not expose a 'Value' property.");
				}
				Type propertyType = property.PropertyType;
				CodeExpression sourceExpression2 = new CodePropertyReferenceExpression(sourceExpression, "Value");
				return CodeGenHelper.GenerateUrtConvertExpression(sourceExpression2, propertyType, targetType);
			}
			else
			{
				if (CodeGenHelper.IsSqlType(targetType))
				{
					PropertyInfo property2 = targetType.GetProperty("Value");
					Type propertyType2 = property2.PropertyType;
					CodeExpression codeExpression = CodeGenHelper.GenerateUrtConvertExpression(sourceExpression, sourceType, propertyType2);
					return new CodeObjectCreateExpression(targetType, new CodeExpression[]
					{
						codeExpression
					});
				}
				return CodeGenHelper.GenerateUrtConvertExpression(sourceExpression, sourceType, targetType);
			}
		}

		// Token: 0x060013F0 RID: 5104 RVA: 0x000705A4 File Offset: 0x0006E7A4
		internal static string GetTypeName(CodeDomProvider codeProvider, string string1, string string2)
		{
			string typeOutput = codeProvider.GetTypeOutput(CodeGenHelper.Type(typeof(Activator)));
			string str = typeOutput.Replace("System", "").Replace("Activator", "");
			return string1 + str + string2;
		}

		// Token: 0x060013F1 RID: 5105 RVA: 0x000705F0 File Offset: 0x0006E7F0
		internal static bool SupportsMultipleNamespaces(CodeDomProvider codeProvider)
		{
			string text = MemberNameValidator.GenerateIdName("TestNs1", codeProvider, false);
			string text2 = MemberNameValidator.GenerateIdName("TestNs2", codeProvider, false);
			CodeNamespace value = new CodeNamespace(text);
			CodeNamespace value2 = new CodeNamespace(text2);
			CodeCompileUnit codeCompileUnit = new CodeCompileUnit();
			codeCompileUnit.Namespaces.Add(value);
			codeCompileUnit.Namespaces.Add(value2);
			StringWriter stringWriter = new StringWriter(CultureInfo.CurrentCulture);
			codeProvider.GenerateCodeFromCompileUnit(codeCompileUnit, stringWriter, new CodeGeneratorOptions());
			string text3 = stringWriter.GetStringBuilder().ToString();
			return text3.Contains(text) && text3.Contains(text2);
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x00070688 File Offset: 0x0006E888
		internal static DSGeneratorProblem GenerateValueExprAndFieldInit(DesignColumn designColumn, object valueObj, object value, string className, string fieldName, out CodeExpression valueExpr, out CodeExpression fieldInit)
		{
			DataColumn dataColumn = designColumn.DataColumn;
			valueExpr = null;
			fieldInit = null;
			if (dataColumn.DataType == typeof(char) || dataColumn.DataType == typeof(string) || dataColumn.DataType == typeof(decimal) || dataColumn.DataType == typeof(bool) || dataColumn.DataType == typeof(float) || dataColumn.DataType == typeof(double) || dataColumn.DataType == typeof(sbyte) || dataColumn.DataType == typeof(byte) || dataColumn.DataType == typeof(short) || dataColumn.DataType == typeof(ushort) || dataColumn.DataType == typeof(int) || dataColumn.DataType == typeof(uint) || dataColumn.DataType == typeof(long) || dataColumn.DataType == typeof(ulong))
			{
				valueExpr = CodeGenHelper.Primitive(valueObj);
			}
			else
			{
				valueExpr = CodeGenHelper.Field(CodeGenHelper.TypeExpr(CodeGenHelper.Type(className)), fieldName);
				if (dataColumn.DataType == typeof(byte[]))
				{
					fieldInit = CodeGenHelper.MethodCall(CodeGenHelper.GlobalTypeExpr(typeof(Convert)), "FromBase64String", CodeGenHelper.Primitive(value));
				}
				else if (dataColumn.DataType == typeof(DateTime))
				{
					fieldInit = CodeGenHelper.MethodCall(CodeGenHelper.GlobalTypeExpr(dataColumn.DataType), "Parse", CodeGenHelper.Primitive(((DateTime)valueObj).ToString("s", DateTimeFormatInfo.InvariantInfo)));
				}
				else if (dataColumn.DataType == typeof(TimeSpan))
				{
					fieldInit = CodeGenHelper.MethodCall(CodeGenHelper.GlobalTypeExpr(dataColumn.DataType), "Parse", CodeGenHelper.Primitive(valueObj.ToString()));
				}
				else
				{
					ConstructorInfo constructor = dataColumn.DataType.GetConstructor(new Type[]
					{
						typeof(string)
					});
					if (constructor == null)
					{
						return new DSGeneratorProblem(SR.GetString("CG_NoCtor1", new object[]
						{
							dataColumn.ColumnName,
							dataColumn.DataType.Name
						}), ProblemSeverity.NonFatalError, designColumn);
					}
					constructor.Invoke(new object[]
					{
						value
					});
					fieldInit = CodeGenHelper.New(CodeGenHelper.GlobalType(dataColumn.DataType), new CodeExpression[]
					{
						CodeGenHelper.Primitive(value)
					});
				}
			}
			return null;
		}

		// Token: 0x060013F3 RID: 5107 RVA: 0x0007097C File Offset: 0x0006EB7C
		internal static string GetLanguageExtension(CodeDomProvider codeProvider)
		{
			if (codeProvider == null)
			{
				return string.Empty;
			}
			string text = "." + codeProvider.FileExtension;
			if (text.StartsWith("..", StringComparison.Ordinal))
			{
				text = text.Substring(1);
			}
			return text;
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x000709BA File Offset: 0x0006EBBA
		internal static bool IsGeneratingJSharpCode(CodeDomProvider codeProvider)
		{
			return StringUtil.EqualValue(CodeGenHelper.GetLanguageExtension(codeProvider), ".jsl");
		}

		// Token: 0x060013F5 RID: 5109 RVA: 0x000709CC File Offset: 0x0006EBCC
		private static bool IsSqlType(Type type)
		{
			return type == typeof(SqlBinary) || type == typeof(SqlBoolean) || type == typeof(SqlByte) || type == typeof(SqlDateTime) || type == typeof(SqlDecimal) || type == typeof(SqlDouble) || type == typeof(SqlGuid) || type == typeof(SqlInt16) || type == typeof(SqlInt32) || type == typeof(SqlInt64) || type == typeof(SqlMoney) || type == typeof(SqlSingle) || type == typeof(SqlString);
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x00070AD8 File Offset: 0x0006ECD8
		private static CodeExpression GenerateUrtConvertExpression(CodeExpression sourceExpression, Type sourceUrtType, Type targetUrtType)
		{
			if (sourceUrtType == targetUrtType)
			{
				return sourceExpression;
			}
			if (sourceUrtType == typeof(object))
			{
				return CodeGenHelper.Cast(CodeGenHelper.GlobalType(targetUrtType), sourceExpression);
			}
			if (ConversionHelper.CanConvert(sourceUrtType, targetUrtType))
			{
				return new CodeMethodInvokeExpression(CodeGenHelper.GlobalTypeExpr("System.Convert"), ConversionHelper.GetConversionMethodName(sourceUrtType, targetUrtType), new CodeExpression[]
				{
					sourceExpression
				});
			}
			return new CodeCastExpression(CodeGenHelper.GlobalType(targetUrtType), new CodeMethodInvokeExpression(CodeGenHelper.GlobalTypeExpr("System.Convert"), "ChangeType", new CodeExpression[]
			{
				sourceExpression,
				CodeGenHelper.TypeOf(CodeGenHelper.GlobalType(targetUrtType))
			}));
		}
	}
}
