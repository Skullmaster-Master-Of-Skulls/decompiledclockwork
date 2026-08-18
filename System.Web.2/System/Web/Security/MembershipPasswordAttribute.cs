using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Util;

namespace System.Web.Security
{
	// Token: 0x020005C3 RID: 1475
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
	public class MembershipPasswordAttribute : ValidationAttribute
	{
		// Token: 0x17001606 RID: 5638
		// (get) Token: 0x06004AC0 RID: 19136 RVA: 0x000F9156 File Offset: 0x000F7356
		// (set) Token: 0x06004AC1 RID: 19137 RVA: 0x000F917B File Offset: 0x000F737B
		public int MinRequiredPasswordLength
		{
			get
			{
				if (this._minRequiredPasswordLength == null)
				{
					return Membership.Provider.MinRequiredPasswordLength;
				}
				return this._minRequiredPasswordLength.Value;
			}
			set
			{
				this._minRequiredPasswordLength = new int?(value);
			}
		}

		// Token: 0x17001607 RID: 5639
		// (get) Token: 0x06004AC2 RID: 19138 RVA: 0x000F9189 File Offset: 0x000F7389
		// (set) Token: 0x06004AC3 RID: 19139 RVA: 0x000F91AE File Offset: 0x000F73AE
		public int MinRequiredNonAlphanumericCharacters
		{
			get
			{
				if (this._minRequiredNonAlphanumericCharacters == null)
				{
					return Membership.Provider.MinRequiredNonAlphanumericCharacters;
				}
				return this._minRequiredNonAlphanumericCharacters.Value;
			}
			set
			{
				this._minRequiredNonAlphanumericCharacters = new int?(value);
			}
		}

		// Token: 0x17001608 RID: 5640
		// (get) Token: 0x06004AC4 RID: 19140 RVA: 0x000F91BC File Offset: 0x000F73BC
		// (set) Token: 0x06004AC5 RID: 19141 RVA: 0x000F91D2 File Offset: 0x000F73D2
		public string PasswordStrengthRegularExpression
		{
			get
			{
				return this._passwordStrengthRegularExpression ?? Membership.Provider.PasswordStrengthRegularExpression;
			}
			set
			{
				this._passwordStrengthRegularExpression = value;
			}
		}

		// Token: 0x17001609 RID: 5641
		// (get) Token: 0x06004AC6 RID: 19142 RVA: 0x000F91DB File Offset: 0x000F73DB
		// (set) Token: 0x06004AC7 RID: 19143 RVA: 0x000F91E3 File Offset: 0x000F73E3
		public Type ResourceType
		{
			get
			{
				return this._resourceType;
			}
			set
			{
				if (this._resourceType != value)
				{
					this._resourceType = value;
					this._minPasswordLengthError.ResourceType = value;
					this._minNonAlphanumericCharactersError.ResourceType = value;
					this._passwordStrengthError.ResourceType = value;
				}
			}
		}

		// Token: 0x1700160A RID: 5642
		// (get) Token: 0x06004AC8 RID: 19144 RVA: 0x000F921E File Offset: 0x000F741E
		// (set) Token: 0x06004AC9 RID: 19145 RVA: 0x000F922B File Offset: 0x000F742B
		public string MinPasswordLengthError
		{
			get
			{
				return this._minPasswordLengthError.Value;
			}
			set
			{
				if (this._minPasswordLengthError.Value != value)
				{
					this._minPasswordLengthError.Value = value;
				}
			}
		}

		// Token: 0x1700160B RID: 5643
		// (get) Token: 0x06004ACA RID: 19146 RVA: 0x000F924C File Offset: 0x000F744C
		// (set) Token: 0x06004ACB RID: 19147 RVA: 0x000F9259 File Offset: 0x000F7459
		public string MinNonAlphanumericCharactersError
		{
			get
			{
				return this._minNonAlphanumericCharactersError.Value;
			}
			set
			{
				if (this._minNonAlphanumericCharactersError.Value != value)
				{
					this._minNonAlphanumericCharactersError.Value = value;
				}
			}
		}

		// Token: 0x1700160C RID: 5644
		// (get) Token: 0x06004ACC RID: 19148 RVA: 0x000F927A File Offset: 0x000F747A
		// (set) Token: 0x06004ACD RID: 19149 RVA: 0x000F9287 File Offset: 0x000F7487
		public string PasswordStrengthError
		{
			get
			{
				return this._passwordStrengthError.Value;
			}
			set
			{
				if (this._passwordStrengthError.Value != value)
				{
					this._passwordStrengthError.Value = value;
				}
			}
		}

		// Token: 0x1700160D RID: 5645
		// (get) Token: 0x06004ACE RID: 19150 RVA: 0x000F92A8 File Offset: 0x000F74A8
		// (set) Token: 0x06004ACF RID: 19151 RVA: 0x000F92B0 File Offset: 0x000F74B0
		public int? PasswordStrengthRegexTimeout { get; set; }

		// Token: 0x06004AD0 RID: 19152 RVA: 0x000F92BC File Offset: 0x000F74BC
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			string text = value as string;
			string name = (validationContext != null) ? validationContext.DisplayName : string.Empty;
			object obj;
			if (validationContext == null)
			{
				obj = null;
			}
			else
			{
				(obj = new string[1])[0] = validationContext.MemberName;
			}
			string[] memberNames = obj;
			if (string.IsNullOrEmpty(text))
			{
				return ValidationResult.Success;
			}
			if (text.Length < this.MinRequiredPasswordLength)
			{
				string errorMessageString = this.GetMinPasswordLengthError();
				return new ValidationResult(this.FormatErrorMessage(errorMessageString, name, this.MinRequiredPasswordLength), memberNames);
			}
			int num = text.Count((char c) => !char.IsLetterOrDigit(c));
			if (num < this.MinRequiredNonAlphanumericCharacters)
			{
				string errorMessageString = this.GetMinNonAlphanumericCharactersError();
				return new ValidationResult(this.FormatErrorMessage(errorMessageString, name, this.MinRequiredNonAlphanumericCharacters), memberNames);
			}
			string passwordStrengthRegularExpression = this.PasswordStrengthRegularExpression;
			if (passwordStrengthRegularExpression != null)
			{
				Regex regex;
				try
				{
					regex = RegexUtil.CreateRegex(passwordStrengthRegularExpression, RegexOptions.None, this.PasswordStrengthRegexTimeout);
				}
				catch (ArgumentException innerException)
				{
					throw new InvalidOperationException(SR.GetString("MembershipPasswordAttribute_InvalidRegularExpression"), innerException);
				}
				if (!regex.IsMatch(text))
				{
					string errorMessageString = this.GetPasswordStrengthError();
					return new ValidationResult(this.FormatErrorMessage(errorMessageString, name, string.Empty), memberNames);
				}
			}
			return ValidationResult.Success;
		}

		// Token: 0x06004AD1 RID: 19153 RVA: 0x000F93F4 File Offset: 0x000F75F4
		public override string FormatErrorMessage(string name)
		{
			return this.FormatErrorMessage(base.ErrorMessageString, name, string.Empty);
		}

		// Token: 0x06004AD2 RID: 19154 RVA: 0x000F9408 File Offset: 0x000F7608
		private string GetMinPasswordLengthError()
		{
			return this._minPasswordLengthError.GetLocalizableValue() ?? SR.GetString("MembershipPasswordAttribute_InvalidPasswordLength");
		}

		// Token: 0x06004AD3 RID: 19155 RVA: 0x000F9423 File Offset: 0x000F7623
		private string GetMinNonAlphanumericCharactersError()
		{
			return this._minNonAlphanumericCharactersError.GetLocalizableValue() ?? SR.GetString("MembershipPasswordAttribute_InvalidPasswordNonAlphanumericCharacters");
		}

		// Token: 0x06004AD4 RID: 19156 RVA: 0x000F943E File Offset: 0x000F763E
		private string GetPasswordStrengthError()
		{
			return this._passwordStrengthError.GetLocalizableValue() ?? SR.GetString("MembershipPasswordAttribute_InvalidPasswordStrength");
		}

		// Token: 0x06004AD5 RID: 19157 RVA: 0x000F9459 File Offset: 0x000F7659
		private string FormatErrorMessage(string errorMessageString, string name, object additionalArgument)
		{
			return string.Format(CultureInfo.CurrentCulture, errorMessageString, new object[]
			{
				name,
				additionalArgument
			});
		}

		// Token: 0x04002820 RID: 10272
		private int? _minRequiredPasswordLength;

		// Token: 0x04002821 RID: 10273
		private int? _minRequiredNonAlphanumericCharacters;

		// Token: 0x04002822 RID: 10274
		private string _passwordStrengthRegularExpression;

		// Token: 0x04002823 RID: 10275
		private Type _resourceType;

		// Token: 0x04002824 RID: 10276
		private LocalizableString _minPasswordLengthError = new LocalizableString("MinPasswordLengthError");

		// Token: 0x04002825 RID: 10277
		private LocalizableString _minNonAlphanumericCharactersError = new LocalizableString("MinNonAlphanumericCharactersError");

		// Token: 0x04002826 RID: 10278
		private LocalizableString _passwordStrengthError = new LocalizableString("PasswordStrengthError");
	}
}
