using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Internal;

namespace System.Data.Entity.Utilities
{
	// Token: 0x0200082F RID: 2095
	internal static class ValidationContextExtensions
	{
		// Token: 0x06005DDA RID: 24026 RVA: 0x00195A64 File Offset: 0x00193C64
		public static void SetDisplayName(this ValidationContext validationContext, InternalMemberEntry property, DisplayAttribute displayAttribute)
		{
			string text = (displayAttribute == null) ? null : displayAttribute.GetName();
			if (property == null)
			{
				Type objectType = ObjectContextTypeCache.GetObjectType(validationContext.ObjectType);
				validationContext.DisplayName = (text ?? objectType.Name);
				validationContext.MemberName = null;
				return;
			}
			validationContext.DisplayName = (text ?? DbHelpers.GetPropertyPath(property));
			validationContext.MemberName = DbHelpers.GetPropertyPath(property);
		}
	}
}
