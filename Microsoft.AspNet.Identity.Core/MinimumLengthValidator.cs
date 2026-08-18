using System;
using System.Globalization;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x0200003C RID: 60
	public class MinimumLengthValidator : IIdentityValidator<string>
	{
		// Token: 0x060000F1 RID: 241 RVA: 0x000066E2 File Offset: 0x000048E2
		public MinimumLengthValidator(int requiredLength)
		{
			this.RequiredLength = requiredLength;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x000066F1 File Offset: 0x000048F1
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x000066F9 File Offset: 0x000048F9
		public int RequiredLength { get; set; }

		// Token: 0x060000F4 RID: 244 RVA: 0x00006704 File Offset: 0x00004904
		public virtual Task<IdentityResult> ValidateAsync(string item)
		{
			if (string.IsNullOrWhiteSpace(item) || item.Length < this.RequiredLength)
			{
				return Task.FromResult<IdentityResult>(IdentityResult.Failed(new string[]
				{
					string.Format(CultureInfo.CurrentCulture, Resources.PasswordTooShort, new object[]
					{
						this.RequiredLength
					})
				}));
			}
			return Task.FromResult<IdentityResult>(IdentityResult.Success);
		}
	}
}
