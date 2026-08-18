using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ClockWorkLogger;
using TechnoPro.Common.Core.Authentication;
using TechnoPro.Common.Core.Ldap;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.DAO.Impl.Membership;
using TechnoPro.Common.ICore;
using TechnoPro.Common.ICore.Membership;
using TechnoPro.Common.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.Authentication;
using TechnoPro.Common.Public.Entities.Caching;
using TechnoPro.Common.Public.Entities.Membership;
using TechnoPro.Common.Public.Entities.Membership.LoginMethods;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.Core.Membership
{
	// Token: 0x020000B7 RID: 183
	public class MembershipManager : IMembershipManager
	{
		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060006D3 RID: 1747 RVA: 0x00027D90 File Offset: 0x00025F90
		[Obsolete("Use ObjectFactory.Resolve<IMembership>() instead.")]
		public static IMembershipManager Current
		{
			get
			{
				return ObjectFactory.Resolve<IMembershipManager>();
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060006D4 RID: 1748 RVA: 0x00027D97 File Offset: 0x00025F97
		// (set) Token: 0x060006D5 RID: 1749 RVA: 0x00027D9F File Offset: 0x00025F9F
		private IRepository<Guid, AuthenticationSession> AuthenticationSessionRepository { get; set; }

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060006D6 RID: 1750 RVA: 0x00027DA8 File Offset: 0x00025FA8
		// (remove) Token: 0x060006D7 RID: 1751 RVA: 0x00027DE0 File Offset: 0x00025FE0
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<OnLogonEventArgs> OnUserLogon;

		// Token: 0x060006D8 RID: 1752 RVA: 0x00027E15 File Offset: 0x00026015
		public MembershipManager()
		{
			this.AuthenticationSessionRepository = new Repository<Guid, AuthenticationSession>();
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x00027E2C File Offset: 0x0002602C
		public bool ValidateClockWorkHashingAuthentication(ClockWorkHashAuthentication hashAuth)
		{
			HashingAuthenticationManager hashingAuthenticationManager = new HashingAuthenticationManager(new HashingOperationContext
			{
				WhoAmI = 0,
				HashingKey = "$Ys5+TBS!)yV~XW|B%>+\\S2zBY'^sKx,j~7zJj95#<G%l4)A'wnV^6d/=M=;UK#%x+%$SQ#F';v|3Ty_~?/|kY!.NK|eyXT6I}.L~|0_FgfK]\\!6o/9,/HpE~De93}uB",
				TokenLifetimeInMinutes = 30
			});
			return hashingAuthenticationManager.ValidateClockWorkHash(hashAuth);
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x00027E70 File Offset: 0x00026070
		public bool TryLogon(ClockWorkHashAuthentication hashAuth, ClientParameters clientParameters, out AuthenticationSession ticket)
		{
			bool flag = this._TryLogon(hashAuth, clientParameters, out ticket);
			bool flag2 = flag;
			if (flag2)
			{
				EventHandler<OnLogonEventArgs> onUserLogon = this.OnUserLogon;
				if (onUserLogon != null)
				{
					onUserLogon(this, new OnLogonEventArgs(hashAuth.Username, ticket.User.UserId, clientParameters));
				}
			}
			return flag;
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x00027EC0 File Offset: 0x000260C0
		public bool TryLogon(string username, string password, string logonAsUsername, ClientParameters clientParameters, out AuthenticationSession ticket)
		{
			bool flag = this._TryLogon(username, password, clientParameters, out ticket);
			bool flag2 = flag && !string.IsNullOrEmpty(logonAsUsername);
			if (flag2)
			{
				bool flag3 = ticket.User.Roles.Any((Role role) => role.Id == 10);
				bool flag4 = flag3;
				if (!flag4)
				{
					ticket = null;
					bool isInfoEnabled = CWLogger.Logger.IsInfoEnabled;
					if (isInfoEnabled)
					{
						CWLogger.Logger.Info("User trying to login as another user but they are not admin: {0} trying as {1}", username, logonAsUsername);
					}
					return false;
				}
				IUserManager userManager = ObjectFactory.Resolve<IUserManager>();
				userManager.OpContext = new OperationContext
				{
					AppContext = ObjectFactory.Resolve<ApplicationContext>()
				};
				User user = userManager.GetUser(logonAsUsername);
				bool flag5 = user == null;
				if (flag5)
				{
					bool isInfoEnabled2 = CWLogger.Logger.IsInfoEnabled;
					if (isInfoEnabled2)
					{
						CWLogger.Logger.Info("Trying to login as another user but cannot resolve user for {0} logging in as {1}", username, logonAsUsername);
					}
					ticket = null;
					return false;
				}
				ticket = this.GetAuthTicket(user, clientParameters);
			}
			EventHandler<OnLogonEventArgs> onUserLogon = this.OnUserLogon;
			if (onUserLogon != null)
			{
				onUserLogon(this, new OnLogonEventArgs(username, ticket.User.UserId, clientParameters));
			}
			return true;
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x00027FFC File Offset: 0x000261FC
		public bool TryLogon(string username, string password, ClientParameters clientParameters, out AuthenticationSession ticket)
		{
			bool flag = this._TryLogon(username, password, clientParameters, out ticket);
			bool flag2 = flag;
			if (flag2)
			{
				EventHandler<OnLogonEventArgs> onUserLogon = this.OnUserLogon;
				if (onUserLogon != null)
				{
					onUserLogon(this, new OnLogonEventArgs(username, ticket.User.UserId, clientParameters));
				}
			}
			return flag;
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x0002804C File Offset: 0x0002624C
		protected bool _TryLogon(string username, string password, ClientParameters clientParameters, out AuthenticationSession ticket)
		{
			ticket = null;
			CWLogger.Logger.Trace("TryLogon:PasswordLength={0}", (password == null) ? "NULL" : password.Length.ToString());
			IUserManager userManager = ObjectFactory.Resolve<IUserManager>();
			userManager.OpContext = new OperationContext
			{
				AppContext = ObjectFactory.Resolve<ApplicationContext>()
			};
			User user = userManager.GetUser(username);
			bool flag = user == null;
			bool result;
			if (flag)
			{
				bool isInfoEnabled = CWLogger.Logger.IsInfoEnabled;
				if (isInfoEnabled)
				{
					CWLogger.Logger.Info("Cannot resolve user for {0}", username);
				}
				result = false;
			}
			else
			{
				bool flag2 = false;
				bool flag3 = false;
				IMiscCodeManager miscCodeManager = new MiscCodeManager(new OperationContext());
				string text = miscCodeManager.LoadMiscCodeValue(eMiscCode.LoginMethod) ?? "";
				bool flag4 = !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(text) && text.Equals("activedirectory", StringComparison.OrdinalIgnoreCase);
				if (flag4)
				{
					LoginMethodActiveDirectorySettings loginMethodActiveDirectorySettings = (miscCodeManager.LoadMiscCodeValue(eMiscCode.LdapSettings) ?? "").ActiveDirectorySettingsFromXml();
					string text2 = (loginMethodActiveDirectorySettings == null || loginMethodActiveDirectorySettings.Domain == null) ? "" : loginMethodActiveDirectorySettings.Domain;
					Exception ex;
					LDAP.IsAuthenticatedActiveDirectory(text2, username, password, out ex);
					flag2 = (ex == null);
					CWLogger.Logger.Trace("Common.Core.Membership.MembershipManager._TryLogin:ActiveDirectory attempt:success={0}:username={1}:server={2}:ex={3}", new object[]
					{
						flag2.ToString(),
						username ?? "NULL",
						text2 ?? "NULL",
						(ex == null) ? "NULL" : ex.ToString()
					});
					bool flag5 = !flag2 && (loginMethodActiveDirectorySettings == null || loginMethodActiveDirectorySettings.DontAllowFallbackToClockWorkUsernamePasswordCheck);
					if (flag5)
					{
						flag3 = true;
					}
				}
				bool flag6 = !flag3 && (flag2 || (!string.IsNullOrEmpty(password) && userManager.ValidateUserPassword(username, password)));
				bool flag7 = flag3;
				if (flag7)
				{
					CWLogger.Logger.Trace("Common.Core.Membership.MembershipManager._TryLogin:ActiveDirectory attempt failed:Skipping ClockWork check because of active directory setting 'noclockworkfallback'");
				}
				bool flag8 = !flag6;
				if (flag8)
				{
					bool isInfoEnabled2 = CWLogger.Logger.IsInfoEnabled;
					if (isInfoEnabled2)
					{
						CWLogger.Logger.Info("User password is different from supplied password for user {0}", username);
					}
					result = false;
				}
				else
				{
					ticket = this.GetAuthTicket(user, clientParameters);
					CWLogger.Logger.Trace("Log in user {0} at {1} on {2}", ticket.User.FullName, clientParameters, ticket.LastCheckedTime.ToString(MembershipManager.DatetimeLongFormat));
					result = true;
				}
			}
			return result;
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x0002829C File Offset: 0x0002649C
		protected bool _TryLogon(ClockWorkHashAuthentication hashAuth, ClientParameters clientParameters, out AuthenticationSession ticket)
		{
			ticket = null;
			IUserManager userManager = ObjectFactory.Resolve<IUserManager>();
			userManager.OpContext = new OperationContext
			{
				AppContext = ObjectFactory.Resolve<ApplicationContext>()
			};
			User user = userManager.GetUser(hashAuth.Username);
			bool flag = user == null;
			bool result;
			if (flag)
			{
				bool isInfoEnabled = CWLogger.Logger.IsInfoEnabled;
				if (isInfoEnabled)
				{
					CWLogger.Logger.Info("Cannot resolve user for {0}", hashAuth.Username);
				}
				result = false;
			}
			else
			{
				bool flag2 = this.ValidateClockWorkHashingAuthentication(hashAuth);
				bool flag3 = !flag2;
				if (flag3)
				{
					bool isInfoEnabled2 = CWLogger.Logger.IsInfoEnabled;
					if (isInfoEnabled2)
					{
						CWLogger.Logger.Info("SSO authentication failed for user {0}", hashAuth.Username);
					}
					result = false;
				}
				else
				{
					ticket = this.GetAuthTicket(user, clientParameters);
					CWLogger.Logger.Trace("Log in user {0} at {1} on {2}", ticket.User.FullName, clientParameters, ticket.LastCheckedTime.ToString(MembershipManager.DatetimeLongFormat));
					result = true;
				}
			}
			return result;
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x00028394 File Offset: 0x00026594
		public AuthTicket Authenticate(Guid ticket)
		{
			AuthenticationSession authenticationSession = this.AuthenticationSessionRepository.Get(ticket);
			bool flag = authenticationSession == null;
			AuthTicket result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = authenticationSession.IsTimeout();
				if (flag2)
				{
					result = new AuthTicket
					{
						IsValid = false
					};
				}
				else
				{
					this.Refresh(authenticationSession);
					AuthTicket authTicket = new AuthTicket
					{
						IsSessionBased = false,
						IsValid = true,
						Username = authenticationSession.User.Name,
						SessionTicket = ticket,
						User = authenticationSession.User
					};
					result = authTicket;
				}
			}
			return result;
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x00028424 File Offset: 0x00026624
		public void Logout(Guid ticket, ClientParameters clientParameters)
		{
			AuthenticationSession authenticationSession = this.AuthenticationSessionRepository[ticket];
			bool flag = authenticationSession == null;
			if (!flag)
			{
				bool flag2 = authenticationSession.User != null;
				if (flag2)
				{
					IUserDatabaseCacheStorageManager userDatabaseCacheStorageManager = new UserDatabaseCacheStorageManager();
					userDatabaseCacheStorageManager.Clear(authenticationSession.User.UserId);
					ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
					string key = "u" + eServerCacheItemType.uAllowedAppTypeIds.ToString() + "_" + authenticationSession.User.UserId.ToString();
					cacheStorageManager.Remove(key);
				}
				AuthenticationSessionDAO authenticationSessionDAO = new AuthenticationSessionDAO(new OperationContext
				{
					WhoAmI = 0
				});
				this.AuthenticationSessionRepository.Remove(authenticationSession);
				IUserManager userManager = ObjectFactory.Resolve<IUserManager>();
				userManager.OpContext = new OperationContext
				{
					AppContext = ObjectFactory.Resolve<ApplicationContext>()
				};
				userManager.Remove(authenticationSession.User);
				authenticationSessionDAO.DeleteSession(authenticationSession.Id.ToString());
				CWLogger.Logger.Trace("MembershipManager::Logout: Authentication session '{0}' from '{1}' was deleted from DB cache", authenticationSession.Id.ToString(), authenticationSession.User.Name);
				CWLogger.Logger.Trace("MembershipManager::Logout: Logout user '{0}' at '{1}' on {2}", authenticationSession.User.FullName, clientParameters.ContainsKey("IP") ? clientParameters["IP"] : string.Empty, DateTime.Now.ToString(MembershipManager.DatetimeLongFormat));
			}
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x000285A4 File Offset: 0x000267A4
		public void RemoveExpiredSessions()
		{
			bool isTraceEnabled = CWLogger.Logger.IsTraceEnabled;
			if (isTraceEnabled)
			{
				CWLogger.Logger.Trace("MembershipManager:: Removing expired sessions");
			}
			IUserManager userManager = ObjectFactory.Resolve<IUserManager>();
			userManager.OpContext = new OperationContext
			{
				AppContext = ObjectFactory.Resolve<ApplicationContext>()
			};
			int argument = userManager.RemoveAll((User u) => u.AuthenticationSession != null && u.AuthenticationSession.IsTimeout());
			AuthenticationSessionDAO authenticationSessionDAO = new AuthenticationSessionDAO(new OperationContext
			{
				WhoAmI = 0
			});
			ICollection<AuthenticationSession> collection = this.AuthenticationSessionRepository.FindAll(new Predicate<AuthenticationSession>(AuthenticationSessionAdapter.IsTimeout));
			foreach (AuthenticationSession authenticationSession in collection)
			{
				authenticationSessionDAO.DeleteSession(authenticationSession.Id.ToString());
			}
			int argument2 = this.AuthenticationSessionRepository.RemoveAll(new Predicate<AuthenticationSession>(AuthenticationSessionAdapter.IsTimeout));
			bool isTraceEnabled2 = CWLogger.Logger.IsTraceEnabled;
			if (isTraceEnabled2)
			{
				CWLogger.Logger.Trace("- {0} expired users were removed", argument);
			}
			bool isTraceEnabled3 = CWLogger.Logger.IsTraceEnabled;
			if (isTraceEnabled3)
			{
				CWLogger.Logger.Trace("- {0} expired sessions were removed", argument2);
			}
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x000286F8 File Offset: 0x000268F8
		public bool ChangeUserPassword(string UserName, string CurrentPassword, string NewPassword, out string msg)
		{
			IUserManager userManager = ObjectFactory.Resolve<IUserManager>();
			userManager.OpContext = new OperationContext
			{
				AppContext = ObjectFactory.Resolve<ApplicationContext>()
			};
			return userManager.ChangeUserPassword(UserName, CurrentPassword, NewPassword, out msg);
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x00028734 File Offset: 0x00026934
		public bool UserMustChangePassword(string UserName)
		{
			IUserManager userManager = ObjectFactory.Resolve<IUserManager>();
			userManager.OpContext = new OperationContext
			{
				AppContext = ObjectFactory.Resolve<ApplicationContext>()
			};
			return userManager.UserMustChangePassword(UserName);
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x0002876C File Offset: 0x0002696C
		public bool ChangeUserPasswordByAdmin(Guid ticket, string UserName, string NewPassword, out string msg)
		{
			AuthTicket authTicket = this.Authenticate(ticket);
			bool flag = authTicket == null;
			bool result;
			if (flag)
			{
				CWLogger.Logger.Warn("MembershipManager:ChangeUserPasswordByAdmin:FailedToRetrieveTicket:AbortingOperation:ticket={0}", ticket.ToString());
				msg = null;
				result = false;
			}
			else
			{
				bool flag2 = false;
				foreach (Role role in authTicket.User.Roles)
				{
					bool flag3 = role.Id == 10;
					if (flag3)
					{
						flag2 = true;
						break;
					}
				}
				bool flag4 = !flag2;
				if (flag4)
				{
					CWLogger logger = CWLogger.Logger;
					string message = "MembershipManager:ChangeUserPasswordByAdmin:FailedToRetrieveAdminRoleForCaller:AbortingOperation:authTicket.User={0}:authTicket.User.Roles={1}";
					object arg = (authTicket.User == null) ? "NULL user" : authTicket.User.UserId.ToString();
					object arg2;
					if (authTicket.User != null && authTicket.User.Roles != null)
					{
						arg2 = string.Join(",", authTicket.User.Roles.ToList<Role>().ConvertAll<string>((Role g) => g.Id.ToString()).ToArray());
					}
					else
					{
						arg2 = "NULL";
					}
					logger.Warn(message, arg, arg2);
					msg = null;
					result = false;
				}
				else
				{
					IUserManager userManager = ObjectFactory.Resolve<IUserManager>();
					userManager.OpContext = new OperationContext
					{
						AppContext = ObjectFactory.Resolve<ApplicationContext>()
					};
					result = userManager.ChangeUserPasswordByAdmin(UserName, NewPassword, out msg);
				}
			}
			return result;
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x000288EC File Offset: 0x00026AEC
		public void LoadAuthenticationSessions()
		{
			try
			{
				AuthenticationSessionDAO authenticationSessionDAO = new AuthenticationSessionDAO(new OperationContext
				{
					WhoAmI = 0,
					AppContext = ObjectFactory.Resolve<ApplicationContext>()
				});
				IList<AuthenticationSession> allSessions = authenticationSessionDAO.GetAllSessions();
				foreach (AuthenticationSession authenticationSession in allSessions)
				{
					try
					{
						bool flag = authenticationSession.IsTimeout();
						if (flag)
						{
							authenticationSessionDAO.DeleteSession(authenticationSession.Id.ToString());
							bool isTraceEnabled = CWLogger.Logger.IsTraceEnabled;
							if (isTraceEnabled)
							{
								CWLogger.Logger.Trace("MembershipManager::LoadSessionsFromCache::Session '{0}' for user '{1}' was deleted because of timeout", authenticationSession.Id.ToString(), authenticationSession.User.FullName);
							}
						}
						else
						{
							this.AuthenticationSessionRepository.Save(authenticationSession);
							IUserManager userManager = ObjectFactory.Resolve<IUserManager>();
							userManager.OpContext = new OperationContext
							{
								AppContext = ObjectFactory.Resolve<ApplicationContext>()
							};
							userManager.AddUser(authenticationSession.User);
							bool isTraceEnabled2 = CWLogger.Logger.IsTraceEnabled;
							if (isTraceEnabled2)
							{
								CWLogger.Logger.Trace("MembershipManager::LoadSessionsFromCache::Getting session '{0}' for user '{1}' from DB cache", authenticationSession.Id.ToString(), authenticationSession.User.FullName);
							}
						}
					}
					catch (Exception ex)
					{
						CWLogger.Logger.Error("MembershipManager:LoadSessionsFromDBCache:{0}", ex.ToString());
					}
				}
			}
			catch (Exception ex2)
			{
				CWLogger.Logger.Error("MembershipManager:LoadSessionsFromDBCache:All:{0}", ex2.ToString());
			}
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x00028AC8 File Offset: 0x00026CC8
		private void Refresh(AuthenticationSession session)
		{
			session.LastCheckedTime = DateTime.Now;
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x00028AD8 File Offset: 0x00026CD8
		private AuthenticationSession SetTokenGrantedStatus(AuthenticationSession session)
		{
			try
			{
				IList<AuthenticationSession> allSessionsWithDistinctIpAddress = this.AuthenticationSessionRepository.Items.GetAllSessionsWithDistinctIpAddress();
				ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
				object obj = cacheStorageManager["Licenses.CAL"];
				int num = (obj == null) ? 0 : ((LicenseKeyInfo)obj).NLicenses;
				eSessionTokenStatus eSessionTokenStatus = (allSessionsWithDistinctIpAddress.Count<AuthenticationSession>() <= num) ? eSessionTokenStatus.BelowConcurrentUserLimit : eSessionTokenStatus.AboveConcurrentUserLimit;
				AuthenticationSessionInfo authenticationSessionInfo = new AuthenticationSessionInfo();
				authenticationSessionInfo.MaxAllowConcurrentUsers = num;
				authenticationSessionInfo.Status = eSessionTokenStatus;
				IList<LogonUserInfo> logonUsers;
				if (eSessionTokenStatus != eSessionTokenStatus.BelowConcurrentUserLimit)
				{
					logonUsers = (from ss in allSessionsWithDistinctIpAddress
					select new LogonUserInfo
					{
						Firstname = ss.User.FirstName,
						Lastname = ss.User.LastName,
						Username = ss.User.Name
					}).ToList<LogonUserInfo>();
				}
				else
				{
					logonUsers = null;
				}
				authenticationSessionInfo.LogonUsers = logonUsers;
				session.TokenStatus = authenticationSessionInfo;
			}
			catch (Exception ex)
			{
				CWLogger.Logger.ErrorException(string.Format("MembershipManager::ValidateSession: {0}", ex.ToString()), ex);
			}
			return session;
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00028BC0 File Offset: 0x00026DC0
		private AuthenticationSession GetAuthTicket(User user, ClientParameters clientParameters)
		{
			AuthenticationSessionDAO authenticationSessionDAO = new AuthenticationSessionDAO(new OperationContext
			{
				WhoAmI = 0
			});
			bool flag = user.AuthenticationSession != null;
			if (flag)
			{
				bool flag2 = user.AuthenticationSession.IsTimeout();
				if (!flag2)
				{
					this.Refresh(user.AuthenticationSession);
					bool flag3 = !user.AuthenticationSession.ClientParameters.Equals(clientParameters);
					if (flag3)
					{
						user.AuthenticationSession.ClientParameters = clientParameters;
						authenticationSessionDAO.UpdateClientParameters(user.AuthenticationSession.Id.ToString(), user.AuthenticationSession.ClientParameters);
						bool isTraceEnabled = CWLogger.Logger.IsTraceEnabled;
						if (isTraceEnabled)
						{
							CWLogger.Logger.Trace("MembershipManager::GetAuthTicket::Session {0} from {1}, client parameters was updated", user.AuthenticationSession.Id.ToString(), user.Name);
						}
					}
					return this.SetTokenGrantedStatus(user.AuthenticationSession);
				}
				this.AuthenticationSessionRepository.Remove(user.AuthenticationSession);
				authenticationSessionDAO.DeleteSession(user.AuthenticationSession.Id.ToString());
				CWLogger.Logger.Trace("Authentication session '{0}' of user '{1}' expired on {2}", user.AuthenticationSession.Id.ToString(), user.FullName, user.AuthenticationSession.IssuedOn.Add(MembershipManager.TokenMaxIdleTimeInterval).ToString(MembershipManager.DatetimeLongFormat));
				user.AuthenticationSession = null;
			}
			AuthenticationSession authenticationSession = new AuthenticationSession
			{
				Id = Guid.NewGuid(),
				IssuedOn = DateTime.Now,
				User = user,
				LastCheckedTime = DateTime.Now,
				ClientParameters = clientParameters
			};
			user.AuthenticationSession = authenticationSession;
			authenticationSessionDAO.SaveSession(authenticationSession);
			bool isTraceEnabled2 = CWLogger.Logger.IsTraceEnabled;
			if (isTraceEnabled2)
			{
				CWLogger.Logger.Trace("MembershipManager::GetAuthTicket::Session {0} from {1} was save to DB cache", authenticationSession.Id.ToString(), authenticationSession.User.Name);
			}
			AuthenticationSession tokenGrantedStatus = this.AuthenticationSessionRepository.Save(authenticationSession);
			return this.SetTokenGrantedStatus(tokenGrantedStatus);
		}

		// Token: 0x0400014D RID: 333
		public static readonly TimeSpan TokenMaxIdleTimeInterval = new TimeSpan(0, 8, 0, 0);

		// Token: 0x0400014E RID: 334
		public static readonly TimeSpan TokenMaxLifeTimeInterval = new TimeSpan(2, 0, 0, 0);

		// Token: 0x0400014F RID: 335
		public static string DatetimeLongFormat = "MMM dd, yyyy hh:mm:ss tt";
	}
}
