using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Reflection;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x02000115 RID: 277
	public sealed class DbLambda
	{
		// Token: 0x06000739 RID: 1849 RVA: 0x00027139 File Offset: 0x00025339
		internal DbLambda(ReadOnlyCollection<DbVariableReferenceExpression> variables, DbExpression bodyExp)
		{
			this._variables = variables;
			this._body = bodyExp;
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600073A RID: 1850 RVA: 0x0002714F File Offset: 0x0002534F
		public DbExpression Body
		{
			get
			{
				return this._body;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600073B RID: 1851 RVA: 0x00027157 File Offset: 0x00025357
		public IList<DbVariableReferenceExpression> Variables
		{
			get
			{
				return this._variables;
			}
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x0002715F File Offset: 0x0002535F
		public static DbLambda Create(DbExpression body, IEnumerable<DbVariableReferenceExpression> variables)
		{
			return DbExpressionBuilder.Lambda(body, variables);
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x00027168 File Offset: 0x00025368
		public static DbLambda Create(DbExpression body, params DbVariableReferenceExpression[] variables)
		{
			return DbExpressionBuilder.Lambda(body, variables);
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x00027174 File Offset: 0x00025374
		public static DbLambda Create(TypeUsage argument1Type, Func<DbExpression, DbExpression> lambdaFunction)
		{
			Check.NotNull<TypeUsage>(argument1Type, "argument1Type");
			Check.NotNull<Func<DbExpression, DbExpression>>(lambdaFunction, "lambdaFunction");
			DbVariableReferenceExpression[] array = DbLambda.CreateVariables(lambdaFunction.Method, new TypeUsage[]
			{
				argument1Type
			});
			DbExpression body = lambdaFunction(array[0]);
			return DbExpressionBuilder.Lambda(body, array);
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x000271C4 File Offset: 0x000253C4
		public static DbLambda Create(TypeUsage argument1Type, TypeUsage argument2Type, Func<DbExpression, DbExpression, DbExpression> lambdaFunction)
		{
			Check.NotNull<TypeUsage>(argument1Type, "argument1Type");
			Check.NotNull<TypeUsage>(argument2Type, "argument2Type");
			Check.NotNull<Func<DbExpression, DbExpression, DbExpression>>(lambdaFunction, "lambdaFunction");
			DbVariableReferenceExpression[] array = DbLambda.CreateVariables(lambdaFunction.Method, new TypeUsage[]
			{
				argument1Type,
				argument2Type
			});
			DbExpression body = lambdaFunction(array[0], array[1]);
			return DbExpressionBuilder.Lambda(body, array);
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x00027228 File Offset: 0x00025428
		public static DbLambda Create(TypeUsage argument1Type, TypeUsage argument2Type, TypeUsage argument3Type, Func<DbExpression, DbExpression, DbExpression, DbExpression> lambdaFunction)
		{
			Check.NotNull<TypeUsage>(argument1Type, "argument1Type");
			Check.NotNull<TypeUsage>(argument2Type, "argument2Type");
			Check.NotNull<TypeUsage>(argument3Type, "argument3Type");
			Check.NotNull<Func<DbExpression, DbExpression, DbExpression, DbExpression>>(lambdaFunction, "lambdaFunction");
			DbVariableReferenceExpression[] array = DbLambda.CreateVariables(lambdaFunction.Method, new TypeUsage[]
			{
				argument1Type,
				argument2Type,
				argument3Type
			});
			DbExpression body = lambdaFunction(array[0], array[1], array[2]);
			return DbExpressionBuilder.Lambda(body, array);
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x0002729C File Offset: 0x0002549C
		public static DbLambda Create(TypeUsage argument1Type, TypeUsage argument2Type, TypeUsage argument3Type, TypeUsage argument4Type, Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression> lambdaFunction)
		{
			Check.NotNull<TypeUsage>(argument1Type, "argument1Type");
			Check.NotNull<TypeUsage>(argument2Type, "argument2Type");
			Check.NotNull<TypeUsage>(argument3Type, "argument3Type");
			Check.NotNull<TypeUsage>(argument4Type, "argument4Type");
			Check.NotNull<Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression>>(lambdaFunction, "lambdaFunction");
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

		// Token: 0x06000742 RID: 1858 RVA: 0x00027328 File Offset: 0x00025528
		public static DbLambda Create(TypeUsage argument1Type, TypeUsage argument2Type, TypeUsage argument3Type, TypeUsage argument4Type, TypeUsage argument5Type, Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression> lambdaFunction)
		{
			Check.NotNull<TypeUsage>(argument1Type, "argument1Type");
			Check.NotNull<TypeUsage>(argument2Type, "argument2Type");
			Check.NotNull<TypeUsage>(argument3Type, "argument3Type");
			Check.NotNull<TypeUsage>(argument4Type, "argument4Type");
			Check.NotNull<TypeUsage>(argument5Type, "argument5Type");
			Check.NotNull<Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression>>(lambdaFunction, "lambdaFunction");
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

		// Token: 0x06000743 RID: 1859 RVA: 0x000273C8 File Offset: 0x000255C8
		public static DbLambda Create(TypeUsage argument1Type, TypeUsage argument2Type, TypeUsage argument3Type, TypeUsage argument4Type, TypeUsage argument5Type, TypeUsage argument6Type, Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression> lambdaFunction)
		{
			Check.NotNull<TypeUsage>(argument1Type, "argument1Type");
			Check.NotNull<TypeUsage>(argument2Type, "argument2Type");
			Check.NotNull<TypeUsage>(argument3Type, "argument3Type");
			Check.NotNull<TypeUsage>(argument4Type, "argument4Type");
			Check.NotNull<TypeUsage>(argument5Type, "argument5Type");
			Check.NotNull<TypeUsage>(argument6Type, "argument6Type");
			Check.NotNull<Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression>>(lambdaFunction, "lambdaFunction");
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

		// Token: 0x06000744 RID: 1860 RVA: 0x0002747C File Offset: 0x0002567C
		public static DbLambda Create(TypeUsage argument1Type, TypeUsage argument2Type, TypeUsage argument3Type, TypeUsage argument4Type, TypeUsage argument5Type, TypeUsage argument6Type, TypeUsage argument7Type, Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression> lambdaFunction)
		{
			Check.NotNull<TypeUsage>(argument1Type, "argument1Type");
			Check.NotNull<TypeUsage>(argument2Type, "argument2Type");
			Check.NotNull<TypeUsage>(argument3Type, "argument3Type");
			Check.NotNull<TypeUsage>(argument4Type, "argument4Type");
			Check.NotNull<TypeUsage>(argument5Type, "argument5Type");
			Check.NotNull<TypeUsage>(argument6Type, "argument6Type");
			Check.NotNull<TypeUsage>(argument7Type, "argument7Type");
			Check.NotNull<Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression>>(lambdaFunction, "lambdaFunction");
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

		// Token: 0x06000745 RID: 1861 RVA: 0x00027548 File Offset: 0x00025748
		public static DbLambda Create(TypeUsage argument1Type, TypeUsage argument2Type, TypeUsage argument3Type, TypeUsage argument4Type, TypeUsage argument5Type, TypeUsage argument6Type, TypeUsage argument7Type, TypeUsage argument8Type, Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression> lambdaFunction)
		{
			Check.NotNull<TypeUsage>(argument1Type, "argument1Type");
			Check.NotNull<TypeUsage>(argument2Type, "argument2Type");
			Check.NotNull<TypeUsage>(argument3Type, "argument3Type");
			Check.NotNull<TypeUsage>(argument4Type, "argument4Type");
			Check.NotNull<TypeUsage>(argument5Type, "argument5Type");
			Check.NotNull<TypeUsage>(argument6Type, "argument6Type");
			Check.NotNull<TypeUsage>(argument7Type, "argument7Type");
			Check.NotNull<TypeUsage>(argument8Type, "argument8Type");
			Check.NotNull<Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression>>(lambdaFunction, "lambdaFunction");
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

		// Token: 0x06000746 RID: 1862 RVA: 0x00027628 File Offset: 0x00025828
		public static DbLambda Create(TypeUsage argument1Type, TypeUsage argument2Type, TypeUsage argument3Type, TypeUsage argument4Type, TypeUsage argument5Type, TypeUsage argument6Type, TypeUsage argument7Type, TypeUsage argument8Type, TypeUsage argument9Type, Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression> lambdaFunction)
		{
			Check.NotNull<TypeUsage>(argument1Type, "argument1Type");
			Check.NotNull<TypeUsage>(argument2Type, "argument2Type");
			Check.NotNull<TypeUsage>(argument3Type, "argument3Type");
			Check.NotNull<TypeUsage>(argument4Type, "argument4Type");
			Check.NotNull<TypeUsage>(argument5Type, "argument5Type");
			Check.NotNull<TypeUsage>(argument6Type, "argument6Type");
			Check.NotNull<TypeUsage>(argument7Type, "argument7Type");
			Check.NotNull<TypeUsage>(argument8Type, "argument8Type");
			Check.NotNull<TypeUsage>(argument9Type, "argument9Type");
			Check.NotNull<Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression>>(lambdaFunction, "lambdaFunction");
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

		// Token: 0x06000747 RID: 1863 RVA: 0x0002771C File Offset: 0x0002591C
		public static DbLambda Create(TypeUsage argument1Type, TypeUsage argument2Type, TypeUsage argument3Type, TypeUsage argument4Type, TypeUsage argument5Type, TypeUsage argument6Type, TypeUsage argument7Type, TypeUsage argument8Type, TypeUsage argument9Type, TypeUsage argument10Type, Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression> lambdaFunction)
		{
			Check.NotNull<TypeUsage>(argument1Type, "argument1Type");
			Check.NotNull<TypeUsage>(argument2Type, "argument2Type");
			Check.NotNull<TypeUsage>(argument3Type, "argument3Type");
			Check.NotNull<TypeUsage>(argument4Type, "argument4Type");
			Check.NotNull<TypeUsage>(argument5Type, "argument5Type");
			Check.NotNull<TypeUsage>(argument6Type, "argument6Type");
			Check.NotNull<TypeUsage>(argument7Type, "argument7Type");
			Check.NotNull<TypeUsage>(argument8Type, "argument8Type");
			Check.NotNull<TypeUsage>(argument9Type, "argument9Type");
			Check.NotNull<TypeUsage>(argument10Type, "argument10Type");
			Check.NotNull<Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression>>(lambdaFunction, "lambdaFunction");
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

		// Token: 0x06000748 RID: 1864 RVA: 0x00027828 File Offset: 0x00025A28
		public static DbLambda Create(TypeUsage argument1Type, TypeUsage argument2Type, TypeUsage argument3Type, TypeUsage argument4Type, TypeUsage argument5Type, TypeUsage argument6Type, TypeUsage argument7Type, TypeUsage argument8Type, TypeUsage argument9Type, TypeUsage argument10Type, TypeUsage argument11Type, Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression> lambdaFunction)
		{
			Check.NotNull<TypeUsage>(argument1Type, "argument1Type");
			Check.NotNull<TypeUsage>(argument2Type, "argument2Type");
			Check.NotNull<TypeUsage>(argument3Type, "argument3Type");
			Check.NotNull<TypeUsage>(argument4Type, "argument4Type");
			Check.NotNull<TypeUsage>(argument5Type, "argument5Type");
			Check.NotNull<TypeUsage>(argument6Type, "argument6Type");
			Check.NotNull<TypeUsage>(argument7Type, "argument7Type");
			Check.NotNull<TypeUsage>(argument8Type, "argument8Type");
			Check.NotNull<TypeUsage>(argument9Type, "argument9Type");
			Check.NotNull<TypeUsage>(argument10Type, "argument10Type");
			Check.NotNull<TypeUsage>(argument11Type, "argument11Type");
			Check.NotNull<Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression>>(lambdaFunction, "lambdaFunction");
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

		// Token: 0x06000749 RID: 1865 RVA: 0x0002794C File Offset: 0x00025B4C
		public static DbLambda Create(TypeUsage argument1Type, TypeUsage argument2Type, TypeUsage argument3Type, TypeUsage argument4Type, TypeUsage argument5Type, TypeUsage argument6Type, TypeUsage argument7Type, TypeUsage argument8Type, TypeUsage argument9Type, TypeUsage argument10Type, TypeUsage argument11Type, TypeUsage argument12Type, Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression> lambdaFunction)
		{
			Check.NotNull<TypeUsage>(argument1Type, "argument1Type");
			Check.NotNull<TypeUsage>(argument2Type, "argument2Type");
			Check.NotNull<TypeUsage>(argument3Type, "argument3Type");
			Check.NotNull<TypeUsage>(argument4Type, "argument4Type");
			Check.NotNull<TypeUsage>(argument5Type, "argument5Type");
			Check.NotNull<TypeUsage>(argument6Type, "argument6Type");
			Check.NotNull<TypeUsage>(argument7Type, "argument7Type");
			Check.NotNull<TypeUsage>(argument8Type, "argument8Type");
			Check.NotNull<TypeUsage>(argument9Type, "argument9Type");
			Check.NotNull<TypeUsage>(argument10Type, "argument10Type");
			Check.NotNull<TypeUsage>(argument11Type, "argument11Type");
			Check.NotNull<TypeUsage>(argument12Type, "argument12Type");
			Check.NotNull<Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression>>(lambdaFunction, "lambdaFunction");
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

		// Token: 0x0600074A RID: 1866 RVA: 0x00027A88 File Offset: 0x00025C88
		public static DbLambda Create(TypeUsage argument1Type, TypeUsage argument2Type, TypeUsage argument3Type, TypeUsage argument4Type, TypeUsage argument5Type, TypeUsage argument6Type, TypeUsage argument7Type, TypeUsage argument8Type, TypeUsage argument9Type, TypeUsage argument10Type, TypeUsage argument11Type, TypeUsage argument12Type, TypeUsage argument13Type, Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression> lambdaFunction)
		{
			Check.NotNull<TypeUsage>(argument1Type, "argument1Type");
			Check.NotNull<TypeUsage>(argument2Type, "argument2Type");
			Check.NotNull<TypeUsage>(argument3Type, "argument3Type");
			Check.NotNull<TypeUsage>(argument4Type, "argument4Type");
			Check.NotNull<TypeUsage>(argument5Type, "argument5Type");
			Check.NotNull<TypeUsage>(argument6Type, "argument6Type");
			Check.NotNull<TypeUsage>(argument7Type, "argument7Type");
			Check.NotNull<TypeUsage>(argument8Type, "argument8Type");
			Check.NotNull<TypeUsage>(argument9Type, "argument9Type");
			Check.NotNull<TypeUsage>(argument10Type, "argument10Type");
			Check.NotNull<TypeUsage>(argument11Type, "argument11Type");
			Check.NotNull<TypeUsage>(argument12Type, "argument12Type");
			Check.NotNull<TypeUsage>(argument13Type, "argument13Type");
			Check.NotNull<Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression>>(lambdaFunction, "lambdaFunction");
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

		// Token: 0x0600074B RID: 1867 RVA: 0x00027BD8 File Offset: 0x00025DD8
		public static DbLambda Create(TypeUsage argument1Type, TypeUsage argument2Type, TypeUsage argument3Type, TypeUsage argument4Type, TypeUsage argument5Type, TypeUsage argument6Type, TypeUsage argument7Type, TypeUsage argument8Type, TypeUsage argument9Type, TypeUsage argument10Type, TypeUsage argument11Type, TypeUsage argument12Type, TypeUsage argument13Type, TypeUsage argument14Type, Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression> lambdaFunction)
		{
			Check.NotNull<TypeUsage>(argument1Type, "argument1Type");
			Check.NotNull<TypeUsage>(argument2Type, "argument2Type");
			Check.NotNull<TypeUsage>(argument3Type, "argument3Type");
			Check.NotNull<TypeUsage>(argument4Type, "argument4Type");
			Check.NotNull<TypeUsage>(argument5Type, "argument5Type");
			Check.NotNull<TypeUsage>(argument6Type, "argument6Type");
			Check.NotNull<TypeUsage>(argument7Type, "argument7Type");
			Check.NotNull<TypeUsage>(argument8Type, "argument8Type");
			Check.NotNull<TypeUsage>(argument9Type, "argument9Type");
			Check.NotNull<TypeUsage>(argument10Type, "argument10Type");
			Check.NotNull<TypeUsage>(argument11Type, "argument11Type");
			Check.NotNull<TypeUsage>(argument12Type, "argument12Type");
			Check.NotNull<TypeUsage>(argument13Type, "argument13Type");
			Check.NotNull<TypeUsage>(argument14Type, "argument14Type");
			Check.NotNull<Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression>>(lambdaFunction, "lambdaFunction");
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

		// Token: 0x0600074C RID: 1868 RVA: 0x00027D40 File Offset: 0x00025F40
		public static DbLambda Create(TypeUsage argument1Type, TypeUsage argument2Type, TypeUsage argument3Type, TypeUsage argument4Type, TypeUsage argument5Type, TypeUsage argument6Type, TypeUsage argument7Type, TypeUsage argument8Type, TypeUsage argument9Type, TypeUsage argument10Type, TypeUsage argument11Type, TypeUsage argument12Type, TypeUsage argument13Type, TypeUsage argument14Type, TypeUsage argument15Type, Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression> lambdaFunction)
		{
			Check.NotNull<TypeUsage>(argument1Type, "argument1Type");
			Check.NotNull<TypeUsage>(argument2Type, "argument2Type");
			Check.NotNull<TypeUsage>(argument3Type, "argument3Type");
			Check.NotNull<TypeUsage>(argument4Type, "argument4Type");
			Check.NotNull<TypeUsage>(argument5Type, "argument5Type");
			Check.NotNull<TypeUsage>(argument6Type, "argument6Type");
			Check.NotNull<TypeUsage>(argument7Type, "argument7Type");
			Check.NotNull<TypeUsage>(argument8Type, "argument8Type");
			Check.NotNull<TypeUsage>(argument9Type, "argument9Type");
			Check.NotNull<TypeUsage>(argument10Type, "argument10Type");
			Check.NotNull<TypeUsage>(argument11Type, "argument11Type");
			Check.NotNull<TypeUsage>(argument12Type, "argument12Type");
			Check.NotNull<TypeUsage>(argument13Type, "argument13Type");
			Check.NotNull<TypeUsage>(argument14Type, "argument14Type");
			Check.NotNull<TypeUsage>(argument15Type, "argument15Type");
			Check.NotNull<Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression>>(lambdaFunction, "lambdaFunction");
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

		// Token: 0x0600074D RID: 1869 RVA: 0x00027EC0 File Offset: 0x000260C0
		public static DbLambda Create(TypeUsage argument1Type, TypeUsage argument2Type, TypeUsage argument3Type, TypeUsage argument4Type, TypeUsage argument5Type, TypeUsage argument6Type, TypeUsage argument7Type, TypeUsage argument8Type, TypeUsage argument9Type, TypeUsage argument10Type, TypeUsage argument11Type, TypeUsage argument12Type, TypeUsage argument13Type, TypeUsage argument14Type, TypeUsage argument15Type, TypeUsage argument16Type, Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression> lambdaFunction)
		{
			Check.NotNull<TypeUsage>(argument1Type, "argument1Type");
			Check.NotNull<TypeUsage>(argument2Type, "argument2Type");
			Check.NotNull<TypeUsage>(argument3Type, "argument3Type");
			Check.NotNull<TypeUsage>(argument4Type, "argument4Type");
			Check.NotNull<TypeUsage>(argument5Type, "argument5Type");
			Check.NotNull<TypeUsage>(argument6Type, "argument6Type");
			Check.NotNull<TypeUsage>(argument7Type, "argument7Type");
			Check.NotNull<TypeUsage>(argument8Type, "argument8Type");
			Check.NotNull<TypeUsage>(argument9Type, "argument9Type");
			Check.NotNull<TypeUsage>(argument10Type, "argument10Type");
			Check.NotNull<TypeUsage>(argument11Type, "argument11Type");
			Check.NotNull<TypeUsage>(argument12Type, "argument12Type");
			Check.NotNull<TypeUsage>(argument13Type, "argument13Type");
			Check.NotNull<TypeUsage>(argument14Type, "argument14Type");
			Check.NotNull<TypeUsage>(argument15Type, "argument15Type");
			Check.NotNull<TypeUsage>(argument16Type, "argument16Type");
			Check.NotNull<Func<DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression, DbExpression>>(lambdaFunction, "lambdaFunction");
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

		// Token: 0x0600074E RID: 1870 RVA: 0x00028058 File Offset: 0x00026258
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

		// Token: 0x0400024C RID: 588
		private readonly ReadOnlyCollection<DbVariableReferenceExpression> _variables;

		// Token: 0x0400024D RID: 589
		private readonly DbExpression _body;
	}
}
