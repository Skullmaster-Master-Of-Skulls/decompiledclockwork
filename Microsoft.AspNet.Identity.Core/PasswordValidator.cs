using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x0200001E RID: 30
	public class PasswordValidator : IIdentityValidator<string>
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002745 File Offset: 0x00000945
		// (set) Token: 0x0600004C RID: 76 RVA: 0x0000274D File Offset: 0x0000094D
		public int RequiredLength { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002756 File Offset: 0x00000956
		// (set) Token: 0x0600004E RID: 78 RVA: 0x0000275E File Offset: 0x0000095E
		public bool RequireNonLetterOrDigit { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002767 File Offset: 0x00000967
		// (set) Token: 0x06000050 RID: 80 RVA: 0x0000276F File Offset: 0x0000096F
		public bool RequireLowercase { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002778 File Offset: 0x00000978
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00002780 File Offset: 0x00000980
		public bool RequireUppercase { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002789 File Offset: 0x00000989
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00002791 File Offset: 0x00000991
		public bool RequireDigit { get; set; }

		// Token: 0x06000055 RID: 85 RVA: 0x000027C0 File Offset: 0x000009C0
		public virtual Task<IdentityResult> ValidateAsync(string item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			List<string> list = new List<string>();
			if (string.IsNullOrWhiteSpace(item) || item.Length < this.RequiredLength)
			{
				list.Add(string.Format(CultureInfo.CurrentCulture, Resources.PasswordTooShort, new object[]
				{
					this.RequiredLength
				}));
			}
			if (this.RequireNonLetterOrDigit && item.All(new Func<char, bool>(this.IsLetterOrDigit)))
			{
				list.Add(Resources.PasswordRequireNonLetterOrDigit);
			}
			if (this.RequireDigit && item.All((char c) => !this.IsDigit(c)))
			{
				list.Add(Resources.PasswordRequireDigit);
			}
			if (this.RequireLowercase && item.All((char c) => !this.IsLower(c)))
			{
				list.Add(Resources.PasswordRequireLower);
			}
			if (this.RequireUppercase && item.All((char c) => !this.IsUpper(c)))
			{
				list.Add(Resources.PasswordRequireUpper);
			}
			if (list.Count == 0)
			{
				return Task.FromResult<IdentityResult>(IdentityResult.Success);
			}
			return Task.FromResult<IdentityResult>(IdentityResult.Failed(new string[]
			{
				string.Join(" ", list)
			}));
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000028F2 File Offset: 0x00000AF2
		public virtual bool IsDigit(char c)
		{
			return c >= '0' && c <= '9';
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002903 File Offset: 0x00000B03
		public virtual bool IsLower(char c)
		{
			return c >= 'a' && c <= 'z';
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002914 File Offset: 0x00000B14
		public virtual bool IsUpper(char c)
		{
			return c >= 'A' && c <= 'Z';
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002925 File Offset: 0x00000B25
		public virtual bool IsLetterOrDigit(char c)
		{
			return this.IsUpper(c) || this.IsLower(c) || this.IsDigit(c);
		}
	}
}
