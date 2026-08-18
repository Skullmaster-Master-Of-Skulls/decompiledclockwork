using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x02000046 RID: 70
	public class UserValidator<TUser, TKey> : IIdentityValidator<TUser> where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
	{
		// Token: 0x060001A3 RID: 419 RVA: 0x00010645 File Offset: 0x0000E845
		public UserValidator(UserManager<TUser, TKey> manager)
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			this.AllowOnlyAlphanumericUserNames = true;
			this.Manager = manager;
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00010669 File Offset: 0x0000E869
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x00010671 File Offset: 0x0000E871
		public bool AllowOnlyAlphanumericUserNames { get; set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x0001067A File Offset: 0x0000E87A
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x00010682 File Offset: 0x0000E882
		public bool RequireUniqueEmail { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x0001068B File Offset: 0x0000E88B
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x00010693 File Offset: 0x0000E893
		private UserManager<TUser, TKey> Manager { get; set; }

		// Token: 0x060001AA RID: 426 RVA: 0x00010878 File Offset: 0x0000EA78
		public virtual async Task<IdentityResult> ValidateAsync(TUser item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			List<string> errors = new List<string>();
			await this.ValidateUserName(item, errors).WithCurrentCulture();
			if (this.RequireUniqueEmail)
			{
				await this.ValidateEmailAsync(item, errors).WithCurrentCulture();
			}
			IdentityResult result;
			if (errors.Count > 0)
			{
				result = IdentityResult.Failed(errors.ToArray());
			}
			else
			{
				result = IdentityResult.Success;
			}
			return result;
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00010AF8 File Offset: 0x0000ECF8
		private async Task ValidateUserName(TUser user, List<string> errors)
		{
			if (string.IsNullOrWhiteSpace(user.UserName))
			{
				errors.Add(string.Format(CultureInfo.CurrentCulture, Resources.PropertyTooShort, new object[]
				{
					"Name"
				}));
			}
			else if (this.AllowOnlyAlphanumericUserNames && !Regex.IsMatch(user.UserName, "^[A-Za-z0-9@_\\.]+$"))
			{
				errors.Add(string.Format(CultureInfo.CurrentCulture, Resources.InvalidUserName, new object[]
				{
					user.UserName
				}));
			}
			else
			{
				TUser owner = await this.Manager.FindByNameAsync(user.UserName).WithCurrentCulture<TUser>();
				if (owner != null && !EqualityComparer<TKey>.Default.Equals(owner.Id, user.Id))
				{
					errors.Add(string.Format(CultureInfo.CurrentCulture, Resources.DuplicateName, new object[]
					{
						user.UserName
					}));
				}
			}
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00010DE8 File Offset: 0x0000EFE8
		private async Task ValidateEmailAsync(TUser user, List<string> errors)
		{
			string email = await this.Manager.GetEmailStore().GetEmailAsync(user).WithCurrentCulture<string>();
			if (string.IsNullOrWhiteSpace(email))
			{
				errors.Add(string.Format(CultureInfo.CurrentCulture, Resources.PropertyTooShort, new object[]
				{
					"Email"
				}));
			}
			else
			{
				try
				{
					new MailAddress(email);
				}
				catch (FormatException)
				{
					errors.Add(string.Format(CultureInfo.CurrentCulture, Resources.InvalidEmail, new object[]
					{
						email
					}));
					return;
				}
				TUser owner = await this.Manager.FindByEmailAsync(email).WithCurrentCulture<TUser>();
				if (owner != null && !EqualityComparer<TKey>.Default.Equals(owner.Id, user.Id))
				{
					errors.Add(string.Format(CultureInfo.CurrentCulture, Resources.DuplicateEmail, new object[]
					{
						email
					}));
				}
			}
		}
	}
}
