using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Xml;

namespace Telerik.Web.Data.Expressions
{
	// Token: 0x02001BAB RID: 7083
	internal static class ExpressionBuilderFactory
	{
		// Token: 0x06011217 RID: 70167 RVA: 0x003C73EC File Offset: 0x003C55EC
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "memberType")]
		public static MemberAccessExpressionBuilderBase MemberAccess(Type elementType, Type memberType, string memberName)
		{
			memberType = (memberType ?? typeof(object));
			if (elementType.IsCompatibleWith(typeof(DataRow)))
			{
				return new DataRowFieldAccessExpressionBuilder(memberType, memberName);
			}
			if (elementType.IsCompatibleWith(typeof(ICustomTypeDescriptor)))
			{
				return new CustomTypeDescriptorPropertyAccessExpressionBuilder(elementType, memberType, memberName);
			}
			if (elementType.IsCompatibleWith(typeof(XmlNode)))
			{
				return new XmlNodeChildElementAccessExpressionBuilder(memberName);
			}
			return new PropertyAccessExpressionBuilder(elementType, memberName);
		}

		// Token: 0x06011218 RID: 70168 RVA: 0x003C7460 File Offset: 0x003C5660
		public static MemberAccessExpressionBuilderBase MemberAccess(IQueryable source, Type memberType, string memberName)
		{
			MemberAccessExpressionBuilderBase memberAccessExpressionBuilderBase = ExpressionBuilderFactory.MemberAccess(source.ElementType, memberType, memberName);
			memberAccessExpressionBuilderBase.Options.LiftMemberAccessToNull = source.Provider.IsLinqToObjectsProvider();
			return memberAccessExpressionBuilderBase;
		}
	}
}
