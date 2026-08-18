using System;
using System.Collections.Generic;
using System.Linq;
using ClockWorkLogger;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.DAO.Membership;
using TechnoPro.Common.ICore;
using TechnoPro.Common.ICore.Membership;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Membership;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.Core.Membership
{
	// Token: 0x020000B8 RID: 184
	public class UserManager : IUserManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060006EA RID: 1770 RVA: 0x00028E22 File Offset: 0x00027022
		// (set) Token: 0x060006EB RID: 1771 RVA: 0x00028E2A File Offset: 0x0002702A
		private IRepository<string, User> UserRepository { get; set; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060006EC RID: 1772 RVA: 0x00028E33 File Offset: 0x00027033
		// (set) Token: 0x060006ED RID: 1773 RVA: 0x00028E3B File Offset: 0x0002703B
		public OperationContext OpContext { get; set; }

		// Token: 0x060006EE RID: 1774 RVA: 0x00028E44 File Offset: 0x00027044
		public UserManager()
		{
			this.UserRepository = new Repository<string, User>();
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x00028E5C File Offset: 0x0002705C
		public User AddUser(User user)
		{
			return this.UserRepository.Save(user);
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x00028E7C File Offset: 0x0002707C
		public User GetUser(string userName)
		{
			User user = this.UserRepository.FindOne((User u) => u.Name == userName);
			bool flag = user != null;
			User result;
			if (flag)
			{
				result = user;
			}
			else
			{
				IUserDAO userDAO = ObjectFactory.Resolve<IUserDAO>();
				userDAO.OpContext = this.OpContext;
				user = userDAO.GetUser(userName);
				result = ((user != null) ? this.AddUser(user) : null);
			}
			return result;
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x00028EF0 File Offset: 0x000270F0
		public IList<User> GetUsers(string role)
		{
			IList<User> result;
			try
			{
				Func<Role, bool> <>9__1;
				result = this.UserRepository.FindAll(delegate(User u)
				{
					IEnumerable<Role> roles = u.Roles;
					Func<Role, bool> predicate;
					if ((predicate = <>9__1) == null)
					{
						predicate = (<>9__1 = ((Role r) => r.Name.ToLower() == role.ToLower()));
					}
					return roles.Any(predicate);
				}).ToList<User>();
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x00028F44 File Offset: 0x00027144
		public bool IsAdmin(User user)
		{
			bool flag = user.Roles != null;
			if (flag)
			{
				foreach (Role role in user.Roles)
				{
					bool flag2 = role.Name.ToLower().Equals("admin");
					if (flag2)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x00028FC4 File Offset: 0x000271C4
		public void Remove(User user)
		{
			this.UserRepository.Remove(user);
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x00028FD4 File Offset: 0x000271D4
		public void Remove(string username)
		{
			this.UserRepository.Remove(username);
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x00028FE4 File Offset: 0x000271E4
		public int RemoveAll(Predicate<User> userCond)
		{
			return this.UserRepository.RemoveAll(userCond);
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x00029004 File Offset: 0x00027204
		public bool Exists(string username)
		{
			IUserDAO userDAO = ObjectFactory.Resolve<IUserDAO>();
			userDAO.OpContext = this.OpContext;
			return userDAO.Exists(username);
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x00029030 File Offset: 0x00027230
		public bool ValidateUserPassword(string UserName, string password)
		{
			bool flag = string.IsNullOrEmpty(password) || password.Trim().Length < 1;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				IUserDAO userDAO = ObjectFactory.Resolve<IUserDAO>();
				userDAO.OpContext = this.OpContext;
				result = userDAO.ValidateUserPassword(UserName, password);
			}
			return result;
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x00029080 File Offset: 0x00027280
		public bool ChangeUserPassword(string UserName, string CurrentPassword, string NewPassword, out string msg)
		{
			UserAccountManager userAccountManager = new UserAccountManager(new OperationContext
			{
				WhoAmI = 0
			});
			bool flag = userAccountManager.ValidatePasswordAgainstPolicy(NewPassword, out msg);
			bool flag2 = !flag;
			bool result;
			if (flag2)
			{
				result = false;
			}
			else
			{
				IUserDAO userDAO = ObjectFactory.Resolve<IUserDAO>();
				userDAO.OpContext = this.OpContext;
				bool flag3 = userDAO.ChangeUserPassword(UserName, CurrentPassword, NewPassword);
				bool flag4 = flag3;
				if (flag4)
				{
					this.UserRepository.Remove(UserName);
				}
				msg = null;
				result = flag3;
			}
			return result;
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x000290F8 File Offset: 0x000272F8
		public bool UserMustChangePassword(string UserName)
		{
			IUserDAO userDAO = ObjectFactory.Resolve<IUserDAO>();
			userDAO.OpContext = this.OpContext;
			return userDAO.UserMustChangePassword(UserName);
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x00029124 File Offset: 0x00027324
		public bool ChangeUserPasswordByAdmin(string UserName, string NewPassword, out string msg)
		{
			msg = null;
			IUserDAO userDAO = ObjectFactory.Resolve<IUserDAO>();
			userDAO.OpContext = this.OpContext;
			bool flag = string.IsNullOrEmpty(NewPassword);
			bool result;
			if (flag)
			{
				CWLogger.Logger.Trace("UserManager:ChangeUserPasswordByAdmin:ClearingPasswordStart:Username={0}:NewPasswordLen={1}", UserName ?? "NULL", (NewPassword == null) ? "NULL" : NewPassword.Length.ToString());
				result = userDAO.ClearUserPassword(UserName);
			}
			else
			{
				CWLogger.Logger.Trace("UserManager:ChangeUserPasswordByAdmin:SettingPasswordStart:Username={0}:NewPasswordLen={1}", UserName ?? "NULL", (NewPassword == null) ? "NULL" : NewPassword.Length.ToString());
				result = userDAO.SetUserPassword(UserName, NewPassword);
			}
			CWLogger.Logger.Trace("UserManager:ChangeUserPasswordByAdmin:Change/SetPasswordEnd:worked={0}", result.ToString());
			return result;
		}
	}
}
