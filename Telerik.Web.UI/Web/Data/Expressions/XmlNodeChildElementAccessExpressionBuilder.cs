using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml;

namespace Telerik.Web.Data.Expressions
{
	// Token: 0x02001BC1 RID: 7105
	internal class XmlNodeChildElementAccessExpressionBuilder : MemberAccessExpressionBuilderBase
	{
		// Token: 0x0601129C RID: 70300 RVA: 0x003C9065 File Offset: 0x003C7265
		public XmlNodeChildElementAccessExpressionBuilder(string memberName) : base(typeof(XmlNode), memberName)
		{
		}

		// Token: 0x0601129D RID: 70301 RVA: 0x003C9078 File Offset: 0x003C7278
		protected override Expression CreateMemberAccessExpressionOverride()
		{
			ConstantExpression arg = Expression.Constant(base.MemberName);
			return Expression.Call(XmlNodeChildElementAccessExpressionBuilder.ChildElementInnerTextMethod, base.ParameterExpression, arg);
		}

		// Token: 0x04004CD2 RID: 19666
		private static readonly MethodInfo ChildElementInnerTextMethod = typeof(XmlNodeExtensions).GetMethod("ChildElementInnerText", new Type[]
		{
			typeof(XmlNode),
			typeof(string)
		});
	}
}
