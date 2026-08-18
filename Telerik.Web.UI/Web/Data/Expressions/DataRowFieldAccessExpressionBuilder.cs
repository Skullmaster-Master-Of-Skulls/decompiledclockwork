using System;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;

namespace Telerik.Web.Data.Expressions
{
	// Token: 0x02001BB9 RID: 7097
	internal class DataRowFieldAccessExpressionBuilder : MemberAccessExpressionBuilderBase
	{
		// Token: 0x0601127C RID: 70268 RVA: 0x003C870C File Offset: 0x003C690C
		public DataRowFieldAccessExpressionBuilder(Type memberType, string memberName) : base(typeof(DataRow), memberName)
		{
			if (memberType.IsValueType && !memberType.IsNullableType())
			{
				this.columnDataType = typeof(Nullable<>).MakeGenericType(new Type[]
				{
					memberType
				});
				return;
			}
			this.columnDataType = memberType;
		}

		// Token: 0x170053AE RID: 21422
		// (get) Token: 0x0601127D RID: 70269 RVA: 0x003C8763 File Offset: 0x003C6963
		public Type ColumnDataType
		{
			get
			{
				return this.columnDataType;
			}
		}

		// Token: 0x0601127E RID: 70270 RVA: 0x003C876C File Offset: 0x003C696C
		protected override Expression CreateMemberAccessExpressionOverride()
		{
			ConstantExpression arg = Expression.Constant(base.MemberName);
			return Expression.Call(DataRowFieldAccessExpressionBuilder.DataRowFieldMethod.MakeGenericMethod(new Type[]
			{
				this.columnDataType
			}), base.ParameterExpression, arg);
		}

		// Token: 0x04004CC8 RID: 19656
		private readonly Type columnDataType;

		// Token: 0x04004CC9 RID: 19657
		private static readonly MethodInfo DataRowFieldMethod = typeof(DataRowExtensions).GetMethod("Field", new Type[]
		{
			typeof(DataRow),
			typeof(string)
		});
	}
}
