using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;

namespace Telerik.Web.Data.Expressions
{
	// Token: 0x02001BB8 RID: 7096
	internal class CustomTypeDescriptorPropertyAccessExpressionBuilder : MemberAccessExpressionBuilderBase
	{
		// Token: 0x06011276 RID: 70262 RVA: 0x003C85BC File Offset: 0x003C67BC
		public CustomTypeDescriptorPropertyAccessExpressionBuilder(Type elementType, Type memberType, string memberName) : base(elementType, memberName)
		{
			if (!elementType.IsCompatibleWith(typeof(ICustomTypeDescriptor)))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "ElementType: {0} did not implement {1}", new object[]
				{
					elementType,
					typeof(ICustomTypeDescriptor)
				}), "elementType");
			}
			this.propertyType = this.GetPropertyType(memberType);
		}

		// Token: 0x06011277 RID: 70263 RVA: 0x003C8624 File Offset: 0x003C6824
		private Type GetPropertyType(Type memberType)
		{
			Type propertyTypeFromTypeDescriptorProvider = this.GetPropertyTypeFromTypeDescriptorProvider();
			if (propertyTypeFromTypeDescriptorProvider != null)
			{
				memberType = propertyTypeFromTypeDescriptorProvider;
			}
			if (memberType.IsValueType && !memberType.IsNullableType())
			{
				return typeof(Nullable<>).MakeGenericType(new Type[]
				{
					memberType
				});
			}
			return memberType;
		}

		// Token: 0x06011278 RID: 70264 RVA: 0x003C8674 File Offset: 0x003C6874
		private Type GetPropertyTypeFromTypeDescriptorProvider()
		{
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(base.ItemType)[base.MemberName];
			if (propertyDescriptor != null)
			{
				return propertyDescriptor.PropertyType;
			}
			return null;
		}

		// Token: 0x170053AD RID: 21421
		// (get) Token: 0x06011279 RID: 70265 RVA: 0x003C86A3 File Offset: 0x003C68A3
		public Type PropertyType
		{
			get
			{
				return this.propertyType;
			}
		}

		// Token: 0x0601127A RID: 70266 RVA: 0x003C86AC File Offset: 0x003C68AC
		protected override Expression CreateMemberAccessExpressionOverride()
		{
			ConstantExpression arg = Expression.Constant(base.MemberName);
			return Expression.Call(CustomTypeDescriptorPropertyAccessExpressionBuilder.PropertyMethod.MakeGenericMethod(new Type[]
			{
				this.propertyType
			}), base.ParameterExpression, arg);
		}

		// Token: 0x04004CC6 RID: 19654
		private static readonly MethodInfo PropertyMethod = typeof(CustomTypeDescriptorExtensions).GetMethod("Property");

		// Token: 0x04004CC7 RID: 19655
		private readonly Type propertyType;
	}
}
