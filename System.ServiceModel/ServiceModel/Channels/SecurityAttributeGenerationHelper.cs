using System;
using System.CodeDom;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200098E RID: 2446
	internal static class SecurityAttributeGenerationHelper
	{
		// Token: 0x06005EE2 RID: 24290 RVA: 0x0015F0F0 File Offset: 0x0015D2F0
		public static CodeAttributeDeclaration FindOrCreateAttributeDeclaration<T>(CodeAttributeDeclarationCollection attributes) where T : Attribute
		{
			if (attributes == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("attributes");
			}
			CodeTypeReference codeTypeReference = new CodeTypeReference(typeof(T));
			foreach (object obj in attributes)
			{
				CodeAttributeDeclaration codeAttributeDeclaration = (CodeAttributeDeclaration)obj;
				if (codeAttributeDeclaration.AttributeType.BaseType == codeTypeReference.BaseType)
				{
					return codeAttributeDeclaration;
				}
			}
			CodeAttributeDeclaration codeAttributeDeclaration2 = new CodeAttributeDeclaration(codeTypeReference);
			attributes.Add(codeAttributeDeclaration2);
			return codeAttributeDeclaration2;
		}

		// Token: 0x06005EE3 RID: 24291 RVA: 0x0015F194 File Offset: 0x0015D394
		public static void CreateOrOverridePropertyDeclaration<V>(CodeAttributeDeclaration attribute, string propertyName, V value)
		{
			if (attribute == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("attribute");
			}
			if (propertyName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("propertyName");
			}
			CodeExpression value2;
			if (value is TimeSpan)
			{
				value2 = new CodeObjectCreateExpression(typeof(TimeSpan), new CodeExpression[]
				{
					new CodePrimitiveExpression(((TimeSpan)((object)value)).Ticks)
				});
			}
			else if (value is Enum)
			{
				value2 = new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(V)), value.ToString());
			}
			else
			{
				value2 = new CodePrimitiveExpression(value);
			}
			CodeAttributeArgument codeAttributeArgument = SecurityAttributeGenerationHelper.TryGetAttributeProperty(attribute, propertyName);
			if (codeAttributeArgument == null)
			{
				codeAttributeArgument = new CodeAttributeArgument(propertyName, value2);
				attribute.Arguments.Add(codeAttributeArgument);
				return;
			}
			codeAttributeArgument.Value = value2;
		}

		// Token: 0x06005EE4 RID: 24292 RVA: 0x0015F270 File Offset: 0x0015D470
		public static CodeAttributeArgument TryGetAttributeProperty(CodeAttributeDeclaration attribute, string propertyName)
		{
			if (attribute == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("attribute");
			}
			if (propertyName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("propertyName");
			}
			foreach (object obj in attribute.Arguments)
			{
				CodeAttributeArgument codeAttributeArgument = (CodeAttributeArgument)obj;
				if (codeAttributeArgument.Name == propertyName)
				{
					return codeAttributeArgument;
				}
			}
			return null;
		}
	}
}
