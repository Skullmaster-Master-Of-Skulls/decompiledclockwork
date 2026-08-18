using System;
using System.Collections.Generic;
using System.Web.Security;

namespace System.Web.Mvc
{
	// Token: 0x02000080 RID: 128
	internal class MembershipPasswordAttributeAdapter : DataAnnotationsModelValidator<MembershipPasswordAttribute>
	{
		// Token: 0x060003D8 RID: 984 RVA: 0x0000B5AC File Offset: 0x000097AC
		public MembershipPasswordAttributeAdapter(ModelMetadata metadata, ControllerContext context, MembershipPasswordAttribute attribute) : base(metadata, context, attribute)
		{
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000B6BC File Offset: 0x000098BC
		public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
		{
			yield return new ModelClientValidationMembershipPasswordRule(base.ErrorMessage, base.Attribute.MinRequiredPasswordLength, base.Attribute.MinRequiredNonAlphanumericCharacters, base.Attribute.PasswordStrengthRegularExpression);
			yield break;
		}
	}
}
