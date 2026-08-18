using System;
using System.Collections.Generic;
using ClockWorkLogger;
using TechnoPro.Common.Core.People;
using TechnoPro.Common.DAO.Impl.UserAccount;
using TechnoPro.Common.DAO.UserAccount;
using TechnoPro.Common.ICore.Membership;
using TechnoPro.Common.ICore.People;
using TechnoPro.Common.ICore.UserAccount;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.UserAccount;
using TechnoPro.Common.Public.Exceptions.PermissionDenied;
using TechnoPro.Common.Public.Exceptions.RequestDenied;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.Core.UserSettingsPermissions
{
	// Token: 0x0200002C RID: 44
	public class UserAccountManager : IUserAccountManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00007F65 File Offset: 0x00006165
		// (set) Token: 0x06000193 RID: 403 RVA: 0x00007F6D File Offset: 0x0000616D
		public OperationContext OpContext { get; set; }

		// Token: 0x06000194 RID: 404 RVA: 0x00007F76 File Offset: 0x00006176
		public UserAccountManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new UserAccountDAO(opContext);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00007F94 File Offset: 0x00006194
		private bool HasManageUserRoomPermissions(int PersonIdToModify)
		{
			bool flag = this.IsAdmin(PersonIdToModify);
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				IPeopleGroupManager peopleGroupManager = new PeopleGroupManager(this.OpContext);
				result = peopleGroupManager.HasManageUserRoomPermissions(this.OpContext.WhoAmI);
			}
			return result;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00007FD4 File Offset: 0x000061D4
		private bool IsAdmin(int PersonIdToModify)
		{
			bool flag = PersonIdToModify == this.OpContext.WhoAmI;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				IPeopleGroupManager peopleGroupManager = new PeopleGroupManager(this.OpContext);
				result = peopleGroupManager.IsAdmin(this.OpContext.WhoAmI);
			}
			return result;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000801C File Offset: 0x0000621C
		public void RemovePassword(int PersonId, string UserName)
		{
			bool flag = !this.HasManageUserRoomPermissions(PersonId);
			if (flag)
			{
				throw new PermissionDeniedException("Not admin");
			}
			this.dao.RemovePassword(PersonId, UserName);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00008054 File Offset: 0x00006254
		public bool CreatePassword(UserInfoPassword PasswordInfo, out string message)
		{
			bool flag = !this.HasManageUserRoomPermissions(PasswordInfo.PersonId);
			if (flag)
			{
				throw new PermissionDeniedException("Not admin");
			}
			UserInfoPassword userInfoPassword = this.dao.LoadPassword(PasswordInfo.UserName, PasswordInfo.PersonId);
			bool flag2 = userInfoPassword != null;
			if (flag2)
			{
				CWLogger.Logger.Warn("UserAccountManager:CreatePassword:Could not create password due to already existing username: " + PasswordInfo.UserName);
				throw new AbortedDueToDuplicateKeyCheck("The username '" + PasswordInfo.UserName + "' already exists.  Nothing was done.");
			}
			bool flag3 = !this.ValidatePasswordAgainstPolicy(PasswordInfo.Password, out message);
			bool result;
			if (flag3)
			{
				result = false;
			}
			else
			{
				this.dao.CreatePassword(PasswordInfo);
				result = true;
			}
			return result;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00008108 File Offset: 0x00006308
		public void ClearAllPasswords(int PersonId, bool ClearPrimaryPassword = true)
		{
			bool flag = !this.HasManageUserRoomPermissions(PersonId);
			if (flag)
			{
				throw new PermissionDeniedException("Not admin");
			}
			this.dao.ClearAllPasswords(PersonId, ClearPrimaryPassword);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00008140 File Offset: 0x00006340
		public UserInfoPassword LoadPassword(int PersonId, string UserName)
		{
			bool flag = !this.HasManageUserRoomPermissions(PersonId);
			if (flag)
			{
				throw new PermissionDeniedException("Not admin");
			}
			return this.dao.LoadPassword(UserName, PersonId);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00008178 File Offset: 0x00006378
		public UserInfoPassword LoadPrimaryPassword(int PersonId)
		{
			bool flag = !this.HasManageUserRoomPermissions(PersonId);
			if (flag)
			{
				throw new PermissionDeniedException("Not admin");
			}
			IPeopleManager peopleManager = new PeopleManager(this.OpContext);
			PersonBase personBase = peopleManager.LoadPerson(PersonId);
			bool flag2 = personBase == null || string.IsNullOrEmpty(personBase.Student_no);
			UserInfoPassword result;
			if (flag2)
			{
				result = null;
			}
			else
			{
				result = this.LoadPassword(PersonId, personBase.Student_no);
			}
			return result;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x000081E0 File Offset: 0x000063E0
		public void ClearPrimaryPassword(int PersonId)
		{
			bool flag = !this.HasManageUserRoomPermissions(PersonId);
			if (flag)
			{
				throw new PermissionDeniedException("Not admin");
			}
			IPeopleManager peopleManager = new PeopleManager(this.OpContext);
			PersonBase personBase = peopleManager.LoadPerson(PersonId);
			bool flag2 = personBase == null || string.IsNullOrEmpty(personBase.Student_no);
			if (!flag2)
			{
				this.RemovePassword(PersonId, personBase.Student_no);
			}
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00008240 File Offset: 0x00006440
		public void UpdatePasswordRequireChange(int PersonId, string UserName, bool NewDoesRequirePasswordChange)
		{
			this.dao.UpdatePasswordRequireChange(PersonId, UserName, NewDoesRequirePasswordChange);
			IUserManager userManager = ObjectFactory.Resolve<IUserManager>();
			userManager.OpContext = this.OpContext;
			userManager.Remove(UserName);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00008278 File Offset: 0x00006478
		public bool UpdatePassword(int PersonId, string UserName, string NewPassword, out string msg)
		{
			bool flag = !this.ValidatePasswordAgainstPolicy(NewPassword, out msg);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				this.dao.UpdatePassword(PersonId, UserName, NewPassword);
				result = true;
			}
			return result;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x000082B0 File Offset: 0x000064B0
		public void UpdatePrimaryPasswordRequireChange(int PersonId, bool NewDoesRequirePasswordChange)
		{
			bool flag = !this.HasManageUserRoomPermissions(PersonId);
			if (flag)
			{
				throw new PermissionDeniedException("Not admin");
			}
			IPeopleManager peopleManager = new PeopleManager(this.OpContext);
			PersonBase personBase = peopleManager.LoadPerson(PersonId);
			bool flag2 = personBase == null || string.IsNullOrEmpty(personBase.Student_no);
			if (!flag2)
			{
				this.UpdatePasswordRequireChange(PersonId, personBase.Student_no, NewDoesRequirePasswordChange);
			}
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00008314 File Offset: 0x00006514
		public bool UpdatePrimaryPassword(int PersonId, string NewPassword, out string message)
		{
			bool flag = !this.HasManageUserRoomPermissions(PersonId);
			if (flag)
			{
				throw new PermissionDeniedException("Not admin");
			}
			IPeopleManager peopleManager = new PeopleManager(this.OpContext);
			PersonBase personBase = peopleManager.LoadPerson(PersonId);
			bool flag2 = personBase == null || string.IsNullOrEmpty(personBase.Student_no);
			bool result;
			if (flag2)
			{
				message = "Failed load user check";
				result = false;
			}
			else
			{
				result = this.UpdatePassword(PersonId, personBase.Student_no, NewPassword, out message);
			}
			return result;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00008388 File Offset: 0x00006588
		public bool UpdatePrimaryPassword2(UserInfoPassword PasswordInfo, out string message)
		{
			bool flag = !this.HasManageUserRoomPermissions(PasswordInfo.PersonId);
			if (flag)
			{
				throw new PermissionDeniedException("Not admin");
			}
			IPeopleManager peopleManager = new PeopleManager(this.OpContext);
			PersonBase personBase = peopleManager.LoadPerson(PasswordInfo.PersonId);
			bool flag2 = personBase == null || string.IsNullOrEmpty(personBase.Student_no);
			bool result;
			if (flag2)
			{
				message = "Failed load user check";
				result = false;
			}
			else
			{
				bool flag3 = !this.ValidatePasswordAgainstPolicy(PasswordInfo.Password, out message);
				if (flag3)
				{
					result = false;
				}
				else
				{
					this.dao.UpdatePassword2(personBase.Student_no, PasswordInfo);
					IUserManager userManager = ObjectFactory.Resolve<IUserManager>();
					userManager.OpContext = this.OpContext;
					userManager.Remove(personBase.Student_no);
					result = true;
				}
			}
			return result;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00008448 File Offset: 0x00006648
		public void UpdatePrimaryPasswordExpiry(int PersonId, DateTime? NewExpiryDate)
		{
			bool flag = !this.HasManageUserRoomPermissions(PersonId);
			if (flag)
			{
				throw new PermissionDeniedException("Not admin");
			}
			IPeopleManager peopleManager = new PeopleManager(this.OpContext);
			PersonBase personBase = peopleManager.LoadPerson(PersonId);
			bool flag2 = personBase == null || string.IsNullOrEmpty(personBase.Student_no);
			if (!flag2)
			{
				this.dao.UpdatePrimaryPasswordExpiry(PersonId, personBase.Student_no, NewExpiryDate);
				IUserManager userManager = ObjectFactory.Resolve<IUserManager>();
				userManager.OpContext = this.OpContext;
				userManager.Remove(personBase.Student_no);
			}
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x000084D0 File Offset: 0x000066D0
		public IList<int> LoadPersonIdsWithUsername(string Username, bool includeDeletedAccounts = false)
		{
			bool flag = !this.HasManageUserRoomPermissions(this.OpContext.WhoAmI);
			if (flag)
			{
				throw new PermissionDeniedException("Not admin");
			}
			return this.dao.LoadPersonIdsWithUsername(Username, includeDeletedAccounts);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00008514 File Offset: 0x00006714
		public bool ValidatePasswordAgainstPolicy(string Password, out string message)
		{
			bool flag = Password == null || Password.Trim().Length < 1;
			bool result;
			if (flag)
			{
				message = "Password cannot be empty";
				result = false;
			}
			else
			{
				PasswordPolicy passwordPolicy = this.LoadPasswordPolicy();
				bool flag2 = passwordPolicy == null || !passwordPolicy.EnforcePasswordPolicy;
				if (flag2)
				{
					message = null;
					result = true;
				}
				else
				{
					bool flag3 = !this.CheckPasswordRequirement(Password, passwordPolicy.MinimumLengthTotal, UserAccountManager.ePasswordMinimumLengthCheckType.TotalLength);
					if (flag3)
					{
						message = "Password must be at least " + passwordPolicy.MinimumLengthTotal.ToString() + " character(s)";
						result = false;
					}
					else
					{
						bool flag4 = !this.CheckPasswordRequirement(Password, passwordPolicy.MinimumLengthLowercase, UserAccountManager.ePasswordMinimumLengthCheckType.Lowercase);
						if (flag4)
						{
							message = "Password must have at least " + passwordPolicy.MinimumLengthLowercase.ToString() + " lowercase character(s)";
							result = false;
						}
						else
						{
							bool flag5 = !this.CheckPasswordRequirement(Password, passwordPolicy.MinimumLengthUppercase, UserAccountManager.ePasswordMinimumLengthCheckType.Uppercase);
							if (flag5)
							{
								message = "Password must have at least " + passwordPolicy.MinimumLengthUppercase.ToString() + " uppercase character(s)";
								result = false;
							}
							else
							{
								bool flag6 = !this.CheckPasswordRequirement(Password, passwordPolicy.MinimumLengthSpecialCharacter, UserAccountManager.ePasswordMinimumLengthCheckType.SpecialCharacters);
								if (flag6)
								{
									message = string.Concat(new string[]
									{
										"Password must have at least ",
										passwordPolicy.MinimumLengthUppercase.ToString(),
										" special character(s) [",
										"!@#$%^&*()+-=_",
										"]"
									});
									result = false;
								}
								else
								{
									bool flag7 = !this.CheckPasswordRequirement(Password, passwordPolicy.MinimumLengthNumeric, UserAccountManager.ePasswordMinimumLengthCheckType.Numeric);
									if (flag7)
									{
										message = "Password must have at least " + passwordPolicy.MinimumLengthUppercase.ToString() + " numeric character(s)";
										result = false;
									}
									else
									{
										message = null;
										result = true;
									}
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000086CC File Offset: 0x000068CC
		private bool CheckPasswordRequirement(string password, int minLength, UserAccountManager.ePasswordMinimumLengthCheckType checkType)
		{
			bool flag = minLength < 1 || string.IsNullOrEmpty(password);
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = checkType == UserAccountManager.ePasswordMinimumLengthCheckType.TotalLength;
				if (flag2)
				{
					result = (password.Length >= minLength);
				}
				else
				{
					int num = 0;
					foreach (char c in password)
					{
						switch (checkType)
						{
						case UserAccountManager.ePasswordMinimumLengthCheckType.Lowercase:
						{
							bool flag3 = char.IsLetter(c) && char.ToLower(c) == c;
							if (flag3)
							{
								num++;
							}
							break;
						}
						case UserAccountManager.ePasswordMinimumLengthCheckType.Uppercase:
						{
							bool flag4 = char.IsLetter(c) && char.ToUpper(c) == c;
							if (flag4)
							{
								num++;
							}
							break;
						}
						case UserAccountManager.ePasswordMinimumLengthCheckType.SpecialCharacters:
						{
							bool flag5 = "!@#$%^&*()+-=_".IndexOf(c) >= 0;
							if (flag5)
							{
								num++;
							}
							break;
						}
						case UserAccountManager.ePasswordMinimumLengthCheckType.Numeric:
						{
							bool flag6 = char.IsDigit(c);
							if (flag6)
							{
								num++;
							}
							break;
						}
						}
					}
					result = (num >= minLength);
				}
			}
			return result;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x000087E0 File Offset: 0x000069E0
		public PasswordPolicy LoadPasswordPolicy()
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			PasswordPolicy passwordPolicy = (PasswordPolicy)cacheStorageManager["uPasswordPolicy"];
			bool flag = passwordPolicy == null;
			if (flag)
			{
				MiscSafeManager miscSafeManager = new MiscSafeManager();
				string value = miscSafeManager.GetValue("passwordpolicy");
				bool flag2 = string.IsNullOrEmpty(value);
				if (flag2)
				{
					passwordPolicy = this.GetDefaultPasswordPolicy();
				}
				else
				{
					passwordPolicy = value.ParsePasswordPolicy();
				}
				cacheStorageManager.Insert("uPasswordPolicy", passwordPolicy, TimeSpan.FromHours(1.0));
			}
			return passwordPolicy;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00008864 File Offset: 0x00006A64
		private PasswordPolicy GetDefaultPasswordPolicy()
		{
			return new PasswordPolicy
			{
				EnforcePasswordPolicy = true,
				MinimumLengthTotal = 6,
				MinimumLengthUppercase = 1,
				MinimumLengthLowercase = 2,
				LockoutDurationMinutes = 3,
				MaxFailedAttempts = 5
			};
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x000088AC File Offset: 0x00006AAC
		public void UpdatePasswordPolicy(PasswordPolicy Policy)
		{
			bool flag = !this.IsAdmin(this.OpContext.WhoAmI);
			if (flag)
			{
				throw new PermissionDeniedException("Not admin");
			}
			bool flag2 = Policy == null;
			if (!flag2)
			{
				string value = Policy.ConvertToXml();
				MiscSafeManager miscSafeManager = new MiscSafeManager();
				miscSafeManager.Save("passwordpolicy", value);
				ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
				cacheStorageManager.Insert("uPasswordPolicy", Policy, TimeSpan.FromHours(1.0));
			}
		}

		// Token: 0x04000056 RID: 86
		private IUserAccountDAO dao;

		// Token: 0x04000057 RID: 87
		private const string SPECIAL_CHARACTERS = "!@#$%^&*()+-=_";

		// Token: 0x0200019C RID: 412
		internal enum ePasswordMinimumLengthCheckType
		{
			// Token: 0x040003F4 RID: 1012
			TotalLength,
			// Token: 0x040003F5 RID: 1013
			Lowercase,
			// Token: 0x040003F6 RID: 1014
			Uppercase,
			// Token: 0x040003F7 RID: 1015
			SpecialCharacters,
			// Token: 0x040003F8 RID: 1016
			Numeric
		}
	}
}
