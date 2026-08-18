using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x02000733 RID: 1843
	internal sealed class ProfilePropertyNameValidator : ConfigurationValidatorBase
	{
		// Token: 0x060058D9 RID: 22745 RVA: 0x00136A33 File Offset: 0x00134C33
		public override bool CanValidate(Type type)
		{
			return type == typeof(string);
		}

		// Token: 0x060058DA RID: 22746 RVA: 0x00136A48 File Offset: 0x00134C48
		public override void Validate(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			string text = value as string;
			if (text != null)
			{
				text = text.Trim();
			}
			if (string.IsNullOrEmpty(text))
			{
				throw new ArgumentException(SR.GetString("Profile_name_can_not_be_empty"));
			}
			if (text.Contains("."))
			{
				throw new ArgumentException(SR.GetString("Profile_name_can_not_contain_period"));
			}
		}

		// Token: 0x04002F34 RID: 12084
		internal static ProfilePropertyNameValidator SingletonInstance = new ProfilePropertyNameValidator();
	}
}
