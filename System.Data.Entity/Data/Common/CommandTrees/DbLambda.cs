using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Data.Metadata.Edm;
using System.Reflection;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x02000425 RID: 1061
	public sealed class DbLambda
	{
		// Token: 0x06003729 RID: 14121 RVA: 0x000D1C15 File Offset: 0x000CFE15
		internal DbLambda(ReadOnlyCollection<DbVariableReferenceExpression> variables, DbExpression bodyExp)
		{
			this._variables = variables;
			this._body = bodyExp;
		}

		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x0600372A RID: 14122 RVA: 0x000D1C2B File Offset: 0x000CFE2B
		public DbExpression Body
		{
			get
			{
				return this._body;
			}
		}

		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x0600372B RID: 14123 RVA: 0x000D1C33 File Offset: 0x000CFE33
		public IList<DbVariableReferenceExpression> Variables
		{
			get
			{
				return this._variables;
			}
		}

		// Token: 0x0600372C RID: 14124 RVA: 0x000D1C3B File Offset: 0x000CFE3B
		public static DbLambda Create(DbExpression body, IEnumerable<DbVariableReferenceExpression> variables)
		{
			return DbExpressionBuilder.Lambda(body, variables);
		}

		// Token: 0x0600372D RID: 14125 RVA: 0x000D1C44 File Offset: 0x000CFE44
		public static DbLambda Create(DbExpression body, params DbVariableReferenceExpression[] variables)
		{
			return DbExpressionBuilder.Lambda(body, variables);
		}

		// Token: 0x0600372E RID: 14126 RVA: 0x000D1C50 File Offset: 0x000CFE50
		public static DbLambda Create(TypeUsage argument1Type, Func<DbExpression, DbExpression> lambdaFunction)
		{
			EntityUtil.CheckArgumentNull<TypeUsage>(argument1Type, "argument1Type");
			EntityUtil.CheckArgumentNull<Func<DbExpression, DbExpression>>(lambdaFunction, "lambdaFunction");
			DbVariableReferenceExpression[] array = DbLambda.CreateVariables(lambdaFunction.Method, new TypeUsage[]
			{
				argument1Type
			});
			DbExpression body = lambdaFunction(array[0]);
			return DbExpressionBuilder.Lambda(body, array);
		}

		// Token: 0x0600372F RID: 14127 RVA: 0x000D1C9C File Offset: 0x000CFE9C
		public static DbLambda Create(TypeUsage argument1Type, TypeUsage argument2Type, Func<DbExpression, DbExpression, DbExpression> lambdaFunction)
		{
			EntityUtil.CheckArgumentNull<TypeUsage>(argument1Type, "argument1Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument2Type, "argument2Type");
			EntityUtil.CheckArgumentNull<Func<DbExpression, DbExpression, DbExpression>>(lambdaFunction, "lambdaFunction");
			DbVariableReferenceExpression[] array = DbLambda.CreateVariables(lambdaFunction.Method, new TypeUsage[]
			{
				argument1Type,
				argument2Type
			});
			DbExpression body = lambdaFunction(array[0], array[1]);
			return DbExpressionBuilder.Lambda(body, array);
		}

		// Token: 0x06003730 RID: 14128 RVA: 0x000D1CFC File Offset: 0x000CFEFC
		public static DbLambda Create(TypeUsage argument1Type, TypeUsage argument2Type, TypeUsage argument3Type, Func<DbExpression, DbExpression, DbExpression, DbExpression> lambdaFunction)
		{
			EntityUtil.CheckArgumentNull<TypeUsage>(argument1Type, "argument1Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument2Type, "argument2Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument3Type, "argument3Type");
			EntityUtil.CheckArgumentNull<Func<DbExpression, DbExpression, DbExpression, DbExpression>>(lambdaFunction, "lambdaFunction");
			DbVariableReferenceExpression[] array = DbLambda.CreateVariables(lambdaFunction.Method, new TypeUsage[]
			{
				argument1Type,
				argument2Type,
				argument3Type
			});
			DbExpression body = lambdaFunction(array[0], array[1], array[2]);
			return DbExpressionBuilder.Lambda(body, array);
		}

		// Token: 0x06003731 RID: 14129 RVA: 0x000D1D70 File Offset: 0x000CFF70
		public static DbLambda Create(TypeUsage argument1Type, TypeUsage argument2Type, TypeUsage argument3Type, TypeUsage argument4Type, Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression> lambdaFunction)
		{
			EntityUtil.CheckArgumentNull<TypeUsage>(argument1Type, "argument1Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument2Type, "argument2Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument3Type, "argument3Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument4Type, "argument4Type");
			EntityUtil.CheckArgumentNull<Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression>>(lambdaFunction, "lambdaFunction");
			DbVariableReferenceExpression[] array = DbLambda.CreateVariables(lambdaFunction.Method, new TypeUsage[]
			{
				argument1Type,
				argument2Type,
				argument3Type,
				argument4Type
			});
			DbExpression body = lambdaFunction(array[0], array[1], array[2], array[3]);
			return DbExpressionBuilder.Lambda(body, array);
		}

		// Token: 0x06003732 RID: 14130 RVA: 0x000D1DF8 File Offset: 0x000CFFF8
		public static DbLambda Create(TypeUsage argument1Type, TypeUsage argument2Type, TypeUsage argument3Type, TypeUsage argument4Type, TypeUsage argument5Type, Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression> lambdaFunction)
		{
			EntityUtil.CheckArgumentNull<TypeUsage>(argument1Type, "argument1Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument2Type, "argument2Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument3Type, "argument3Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument4Type, "argument4Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument5Type, "argument5Type");
			EntityUtil.CheckArgumentNull<Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression>>(lambdaFunction, "lambdaFunction");
			DbVariableReferenceExpression[] array = DbLambda.CreateVariables(lambdaFunction.Method, new TypeUsage[]
			{
				argument1Type,
				argument2Type,
				argument3Type,
				argument4Type,
				argument5Type
			});
			DbExpression body = lambdaFunction(array[0], array[1], array[2], array[3], array[4]);
			return DbExpressionBuilder.Lambda(body, array);
		}

		// Token: 0x06003733 RID: 14131 RVA: 0x000D1E98 File Offset: 0x000D0098
		public static DbLambda Create(TypeUsage argument1Type, TypeUsage argument2Type, TypeUsage argument3Type, TypeUsage argument4Type, TypeUsage argument5Type, TypeUsage argument6Type, Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression> lambdaFunction)
		{
			EntityUtil.CheckArgumentNull<TypeUsage>(argument1Type, "argument1Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument2Type, "argument2Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument3Type, "argument3Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument4Type, "argument4Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument5Type, "argument5Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument6Type, "argument6Type");
			EntityUtil.CheckArgumentNull<Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression>>(lambdaFunction, "lambdaFunction");
			DbVariableReferenceExpression[] array = DbLambda.CreateVariables(lambdaFunction.Method, new TypeUsage[]
			{
				argument1Type,
				argument2Type,
				argument3Type,
				argument4Type,
				argument5Type,
				argument6Type
			});
			DbExpression body = lambdaFunction(array[0], array[1], array[2], array[3], array[4], array[5]);
			return DbExpressionBuilder.Lambda(body, array);
		}

		// Token: 0x06003734 RID: 14132 RVA: 0x000D1F4C File Offset: 0x000D014C
		public static DbLambda Create(TypeUsage argument1Type, TypeUsage argument2Type, TypeUsage argument3Type, TypeUsage argument4Type, TypeUsage argument5Type, TypeUsage argument6Type, TypeUsage argument7Type, Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression> lambdaFunction)
		{
			EntityUtil.CheckArgumentNull<TypeUsage>(argument1Type, "argument1Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument2Type, "argument2Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument3Type, "argument3Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument4Type, "argument4Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument5Type, "argument5Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument6Type, "argument6Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument7Type, "argument7Type");
			EntityUtil.CheckArgumentNull<Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression>>(lambdaFunction, "lambdaFunction");
			DbVariableReferenceExpression[] array = DbLambda.CreateVariables(lambdaFunction.Method, new TypeUsage[]
			{
				argument1Type,
				argument2Type,
				argument3Type,
				argument4Type,
				argument5Type,
				argument6Type,
				argument7Type
			});
			DbExpression body = lambdaFunction(array[0], array[1], array[2], array[3], array[4], array[5], array[6]);
			return DbExpressionBuilder.Lambda(body, array);
		}

		// Token: 0x06003735 RID: 14133 RVA: 0x000D2014 File Offset: 0x000D0214
		public static DbLambda Create(TypeUsage argument1Type, TypeUsage argument2Type, TypeUsage argument3Type, TypeUsage argument4Type, TypeUsage argument5Type, TypeUsage argument6Type, TypeUsage argument7Type, TypeUsage argument8Type, Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression> lambdaFunction)
		{
			EntityUtil.CheckArgumentNull<TypeUsage>(argument1Type, "argument1Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument2Type, "argument2Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument3Type, "argument3Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument4Type, "argument4Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument5Type, "argument5Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument6Type, "argument6Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument7Type, "argument7Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument8Type, "argument8Type");
			EntityUtil.CheckArgumentNull<Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression>>(lambdaFunction, "lambdaFunction");
			DbVariableReferenceExpression[] array = DbLambda.CreateVariables(lambdaFunction.Method, new TypeUsage[]
			{
				argument1Type,
				argument2Type,
				argument3Type,
				argument4Type,
				argument5Type,
				argument6Type,
				argument7Type,
				argument8Type
			});
			DbExpression body = lambdaFunction(array[0], array[1], array[2], array[3], array[4], array[5], array[6], array[7]);
			return DbExpressionBuilder.Lambda(body, array);
		}

		// Token: 0x06003736 RID: 14134 RVA: 0x000D20F0 File Offset: 0x000D02F0
		public static DbLambda Create(TypeUsage argument1Type, TypeUsage argument2Type, TypeUsage argument3Type, TypeUsage argument4Type, TypeUsage argument5Type, TypeUsage argument6Type, TypeUsage argument7Type, TypeUsage argument8Type, TypeUsage argument9Type, Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression> lambdaFunction)
		{
			EntityUtil.CheckArgumentNull<TypeUsage>(argument1Type, "argument1Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument2Type, "argument2Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument3Type, "argument3Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument4Type, "argument4Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument5Type, "argument5Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument6Type, "argument6Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument7Type, "argument7Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument8Type, "argument8Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument9Type, "argument9Type");
			EntityUtil.CheckArgumentNull<Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression>>(lambdaFunction, "lambdaFunction");
			DbVariableReferenceExpression[] array = DbLambda.CreateVariables(lambdaFunction.Method, new TypeUsage[]
			{
				argument1Type,
				argument2Type,
				argument3Type,
				argument4Type,
				argument5Type,
				argument6Type,
				argument7Type,
				argument8Type,
				argument9Type
			});
			DbExpression body = lambdaFunction(array[0], array[1], array[2], array[3], array[4], array[5], array[6], array[7], array[8]);
			return DbExpressionBuilder.Lambda(body, array);
		}

		// Token: 0x06003737 RID: 14135 RVA: 0x000D21E4 File Offset: 0x000D03E4
		public static DbLambda Create(TypeUsage argument1Type, TypeUsage argument2Type, TypeUsage argument3Type, TypeUsage argument4Type, TypeUsage argument5Type, TypeUsage argument6Type, TypeUsage argument7Type, TypeUsage argument8Type, TypeUsage argument9Type, TypeUsage argument10Type, Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression> lambdaFunction)
		{
			EntityUtil.CheckArgumentNull<TypeUsage>(argument1Type, "argument1Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument2Type, "argument2Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument3Type, "argument3Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument4Type, "argument4Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument5Type, "argument5Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument6Type, "argument6Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument7Type, "argument7Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument8Type, "argument8Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument9Type, "argument9Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument10Type, "argument10Type");
			EntityUtil.CheckArgumentNull<Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression>>(lambdaFunction, "lambdaFunction");
			DbVariableReferenceExpression[] array = DbLambda.CreateVariables(lambdaFunction.Method, new TypeUsage[]
			{
				argument1Type,
				argument2Type,
				argument3Type,
				argument4Type,
				argument5Type,
				argument6Type,
				argument7Type,
				argument8Type,
				argument9Type,
				argument10Type
			});
			DbExpression body = lambdaFunction(array[0], array[1], array[2], array[3], array[4], array[5], array[6], array[7], array[8], array[9]);
			return DbExpressionBuilder.Lambda(body, array);
		}

		// Token: 0x06003738 RID: 14136 RVA: 0x000D22F0 File Offset: 0x000D04F0
		public static DbLambda Create(TypeUsage argument1Type, TypeUsage argument2Type, TypeUsage argument3Type, TypeUsage argument4Type, TypeUsage argument5Type, TypeUsage argument6Type, TypeUsage argument7Type, TypeUsage argument8Type, TypeUsage argument9Type, TypeUsage argument10Type, TypeUsage argument11Type, Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression> lambdaFunction)
		{
			EntityUtil.CheckArgumentNull<TypeUsage>(argument1Type, "argument1Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument2Type, "argument2Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument3Type, "argument3Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument4Type, "argument4Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument5Type, "argument5Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument6Type, "argument6Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument7Type, "argument7Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument8Type, "argument8Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument9Type, "argument9Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument10Type, "argument10Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument11Type, "argument11Type");
			EntityUtil.CheckArgumentNull<Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression>>(lambdaFunction, "lambdaFunction");
			DbVariableReferenceExpression[] array = DbLambda.CreateVariables(lambdaFunction.Method, new TypeUsage[]
			{
				argument1Type,
				argument2Type,
				argument3Type,
				argument4Type,
				argument5Type,
				argument6Type,
				argument7Type,
				argument8Type,
				argument9Type,
				argument10Type,
				argument11Type
			});
			DbExpression body = lambdaFunction(array[0], array[1], array[2], array[3], array[4], array[5], array[6], array[7], array[8], array[9], array[10]);
			return DbExpressionBuilder.Lambda(body, array);
		}

		// Token: 0x06003739 RID: 14137 RVA: 0x000D2410 File Offset: 0x000D0610
		public static DbLambda Create(TypeUsage argument1Type, TypeUsage argument2Type, TypeUsage argument3Type, TypeUsage argument4Type, TypeUsage argument5Type, TypeUsage argument6Type, TypeUsage argument7Type, TypeUsage argument8Type, TypeUsage argument9Type, TypeUsage argument10Type, TypeUsage argument11Type, TypeUsage argument12Type, Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression> lambdaFunction)
		{
			EntityUtil.CheckArgumentNull<TypeUsage>(argument1Type, "argument1Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument2Type, "argument2Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument3Type, "argument3Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument4Type, "argument4Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument5Type, "argument5Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument6Type, "argument6Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument7Type, "argument7Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument8Type, "argument8Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument9Type, "argument9Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument10Type, "argument10Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument11Type, "argument11Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument12Type, "argument12Type");
			EntityUtil.CheckArgumentNull<Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression>>(lambdaFunction, "lambdaFunction");
			DbVariableReferenceExpression[] array = DbLambda.CreateVariables(lambdaFunction.Method, new TypeUsage[]
			{
				argument1Type,
				argument2Type,
				argument3Type,
				argument4Type,
				argument5Type,
				argument6Type,
				argument7Type,
				argument8Type,
				argument9Type,
				argument10Type,
				argument11Type,
				argument12Type
			});
			DbExpression body = lambdaFunction(array[0], array[1], array[2], array[3], array[4], array[5], array[6], array[7], array[8], array[9], array[10], array[11]);
			return DbExpressionBuilder.Lambda(body, array);
		}

		// Token: 0x0600373A RID: 14138 RVA: 0x000D2548 File Offset: 0x000D0748
		public static DbLambda Create(TypeUsage argument1Type, TypeUsage argument2Type, TypeUsage argument3Type, TypeUsage argument4Type, TypeUsage argument5Type, TypeUsage argument6Type, TypeUsage argument7Type, TypeUsage argument8Type, TypeUsage argument9Type, TypeUsage argument10Type, TypeUsage argument11Type, TypeUsage argument12Type, TypeUsage argument13Type, Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression> lambdaFunction)
		{
			EntityUtil.CheckArgumentNull<TypeUsage>(argument1Type, "argument1Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument2Type, "argument2Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument3Type, "argument3Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument4Type, "argument4Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument5Type, "argument5Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument6Type, "argument6Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument7Type, "argument7Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument8Type, "argument8Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument9Type, "argument9Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument10Type, "argument10Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument11Type, "argument11Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument12Type, "argument12Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument13Type, "argument13Type");
			EntityUtil.CheckArgumentNull<Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression>>(lambdaFunction, "lambdaFunction");
			DbVariableReferenceExpression[] array = DbLambda.CreateVariables(lambdaFunction.Method, new TypeUsage[]
			{
				argument1Type,
				argument2Type,
				argument3Type,
				argument4Type,
				argument5Type,
				argument6Type,
				argument7Type,
				argument8Type,
				argument9Type,
				argument10Type,
				argument11Type,
				argument12Type,
				argument13Type
			});
			DbExpression body = lambdaFunction(array[0], array[1], array[2], array[3], array[4], array[5], array[6], array[7], array[8], array[9], array[10], array[11], array[12]);
			return DbExpressionBuilder.Lambda(body, array);
		}

		// Token: 0x0600373B RID: 14139 RVA: 0x000D2698 File Offset: 0x000D0898
		public static DbLambda Create(TypeUsage argument1Type, TypeUsage argument2Type, TypeUsage argument3Type, TypeUsage argument4Type, TypeUsage argument5Type, TypeUsage argument6Type, TypeUsage argument7Type, TypeUsage argument8Type, TypeUsage argument9Type, TypeUsage argument10Type, TypeUsage argument11Type, TypeUsage argument12Type, TypeUsage argument13Type, TypeUsage argument14Type, Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression> lambdaFunction)
		{
			EntityUtil.CheckArgumentNull<TypeUsage>(argument1Type, "argument1Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument2Type, "argument2Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument3Type, "argument3Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument4Type, "argument4Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument5Type, "argument5Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument6Type, "argument6Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument7Type, "argument7Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument8Type, "argument8Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument9Type, "argument9Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument10Type, "argument10Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument11Type, "argument11Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument12Type, "argument12Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument13Type, "argument13Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument14Type, "argument14Type");
			EntityUtil.CheckArgumentNull<Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression>>(lambdaFunction, "lambdaFunction");
			DbVariableReferenceExpression[] array = DbLambda.CreateVariables(lambdaFunction.Method, new TypeUsage[]
			{
				argument1Type,
				argument2Type,
				argument3Type,
				argument4Type,
				argument5Type,
				argument6Type,
				argument7Type,
				argument8Type,
				argument9Type,
				argument10Type,
				argument11Type,
				argument12Type,
				argument13Type,
				argument14Type
			});
			DbExpression body = lambdaFunction(array[0], array[1], array[2], array[3], array[4], array[5], array[6], array[7], array[8], array[9], array[10], array[11], array[12], array[13]);
			return DbExpressionBuilder.Lambda(body, array);
		}

		// Token: 0x0600373C RID: 14140 RVA: 0x000D2800 File Offset: 0x000D0A00
		public static DbLambda Create(TypeUsage argument1Type, TypeUsage argument2Type, TypeUsage argument3Type, TypeUsage argument4Type, TypeUsage argument5Type, TypeUsage argument6Type, TypeUsage argument7Type, TypeUsage argument8Type, TypeUsage argument9Type, TypeUsage argument10Type, TypeUsage argument11Type, TypeUsage argument12Type, TypeUsage argument13Type, TypeUsage argument14Type, TypeUsage argument15Type, Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression> lambdaFunction)
		{
			EntityUtil.CheckArgumentNull<TypeUsage>(argument1Type, "argument1Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument2Type, "argument2Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument3Type, "argument3Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument4Type, "argument4Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument5Type, "argument5Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument6Type, "argument6Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument7Type, "argument7Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument8Type, "argument8Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument9Type, "argument9Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument10Type, "argument10Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument11Type, "argument11Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument12Type, "argument12Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument13Type, "argument13Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument14Type, "argument14Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument15Type, "argument15Type");
			EntityUtil.CheckArgumentNull<Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression>>(lambdaFunction, "lambdaFunction");
			DbVariableReferenceExpression[] array = DbLambda.CreateVariables(lambdaFunction.Method, new TypeUsage[]
			{
				argument1Type,
				argument2Type,
				argument3Type,
				argument4Type,
				argument5Type,
				argument6Type,
				argument7Type,
				argument8Type,
				argument9Type,
				argument10Type,
				argument11Type,
				argument12Type,
				argument13Type,
				argument14Type,
				argument15Type
			});
			DbExpression body = lambdaFunction(array[0], array[1], array[2], array[3], array[4], array[5], array[6], array[7], array[8], array[9], array[10], array[11], array[12], array[13], array[14]);
			return DbExpressionBuilder.Lambda(body, array);
		}

		// Token: 0x0600373D RID: 14141 RVA: 0x000D297C File Offset: 0x000D0B7C
		public static DbLambda Create(TypeUsage argument1Type, TypeUsage argument2Type, TypeUsage argument3Type, TypeUsage argument4Type, TypeUsage argument5Type, TypeUsage argument6Type, TypeUsage argument7Type, TypeUsage argument8Type, TypeUsage argument9Type, TypeUsage argument10Type, TypeUsage argument11Type, TypeUsage argument12Type, TypeUsage argument13Type, TypeUsage argument14Type, TypeUsage argument15Type, TypeUsage argument16Type, Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression> lambdaFunction)
		{
			EntityUtil.CheckArgumentNull<TypeUsage>(argument1Type, "argument1Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument2Type, "argument2Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument3Type, "argument3Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument4Type, "argument4Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument5Type, "argument5Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument6Type, "argument6Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument7Type, "argument7Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument8Type, "argument8Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument9Type, "argument9Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument10Type, "argument10Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument11Type, "argument11Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument12Type, "argument12Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument13Type, "argument13Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument14Type, "argument14Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument15Type, "argument15Type");
			EntityUtil.CheckArgumentNull<TypeUsage>(argument16Type, "argument16Type");
			EntityUtil.CheckArgumentNull<Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression>>(lambdaFunction, "lambdaFunction");
			DbVariableReferenceExpression[] array = DbLambda.CreateVariables(lambdaFunction.Method, new TypeUsage[]
			{
				argument1Type,
				argument2Type,
				argument3Type,
				argument4Type,
				argument5Type,
				argument6Type,
				argument7Type,
				argument8Type,
				argument9Type,
				argument10Type,
				argument11Type,
				argument12Type,
				argument13Type,
				argument14Type,
				argument15Type,
				argument16Type
			});
			DbExpression body = lambdaFunction(array[0], array[1], array[2], array[3], array[4], array[5], array[6], array[7], array[8], array[9], array[10], array[11], array[12], array[13], array[14], array[15]);
			return DbExpressionBuilder.Lambda(body, array);
		}

		// Token: 0x0600373E RID: 14142 RVA: 0x000D2B10 File Offset: 0x000D0D10
		private static DbVariableReferenceExpression[] CreateVariables(MethodInfo lambdaMethod, params TypeUsage[] argumentTypes)
		{
			string[] array = DbExpressionBuilder.ExtractAliases(lambdaMethod);
			DbVariableReferenceExpression[] array2 = new DbVariableReferenceExpression[argumentTypes.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = argumentTypes[i].Variable(array[i]);
			}
			return array2;
		}

		// Token: 0x04001839 RID: 6201
		private readonly ReadOnlyCollection<DbVariableReferenceExpression> _variables;

		// Token: 0x0400183A RID: 6202
		private readonly DbExpression _body;
	}
}
