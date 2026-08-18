using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Data.Common;
using System.Globalization;
using System.Net;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web.Resources;
using System.Web.Security;
using System.Web.Security.Cryptography;

namespace System.Web.ClientServices.Providers
{
	// Token: 0x0200010F RID: 271
	public class ClientFormsAuthenticationMembershipProvider : MembershipProvider
	{
		// Token: 0x06000E0B RID: 3595 RVA: 0x00031310 File Offset: 0x0002F510
		public static bool ValidateUser(string username, string password, string serviceUri)
		{
			CookieContainer authenticationCookies = null;
			bool useWFCService = serviceUri.EndsWith(".svc", StringComparison.OrdinalIgnoreCase);
			bool flag = ClientFormsAuthenticationMembershipProvider.ValidateUserByCallingLogin(username, password, false, serviceUri, useWFCService, ref authenticationCookies, null, null);
			if (flag)
			{
				Thread.CurrentPrincipal = new ClientRolePrincipal(new ClientFormsIdentity(username, password, new ClientFormsAuthenticationMembershipProvider(), "ClientForms", true, authenticationCookies));
			}
			return flag;
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x0003135C File Offset: 0x0002F55C
		private static bool ValidateUserByCallingLogin(string username, string password, bool rememberMe, string serviceUri, bool useWFCService, ref CookieContainer cookies, string connectionString, string connectionStringProvider)
		{
			if (useWFCService)
			{
				throw new NotImplementedException();
			}
			serviceUri += "/Login";
			string[] paramNames = new string[]
			{
				"userName",
				"password",
				"createPersistentCookie"
			};
			object[] paramValues = new object[]
			{
				username,
				password,
				rememberMe
			};
			object obj = ProxyHelper.CreateWebRequestAndGetResponse(serviceUri, ref cookies, username, connectionString, connectionStringProvider, paramNames, paramValues, typeof(bool));
			return obj != null && obj is bool && (bool)obj;
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x000313E4 File Offset: 0x0002F5E4
		public override void Initialize(string name, NameValueCollection config)
		{
			if (config == null)
			{
				throw new ArgumentNullException("config");
			}
			base.Initialize(name, config);
			this._GetCredentialsTypeName = config["credentialsProvider"];
			this._ConnectionString = config["connectionStringName"];
			this.ServiceUri = config["serviceUri"];
			if (string.IsNullOrEmpty(this._ConnectionString))
			{
				this._ConnectionString = SqlHelper.GetDefaultConnectionString();
			}
			else if (ConfigurationManager.ConnectionStrings[this._ConnectionString] != null)
			{
				this._ConnectionStringProvider = ConfigurationManager.ConnectionStrings[this._ConnectionString].ProviderName;
				this._ConnectionString = ConfigurationManager.ConnectionStrings[this._ConnectionString].ConnectionString;
			}
			int num = SqlHelper.IsSpecialConnectionString(this._ConnectionString);
			if (num != 1)
			{
				if (num == 2)
				{
					this._UsingIsolatedStore = true;
				}
			}
			else
			{
				this._UsingFileSystemStore = true;
			}
			string text = config["savePasswordHashLocally"];
			if (!string.IsNullOrEmpty(text))
			{
				this._SavePasswordHash = (string.Compare(text, "true", StringComparison.OrdinalIgnoreCase) == 0);
			}
			config.Remove("savePasswordHashLocally");
			config.Remove("name");
			config.Remove("description");
			config.Remove("credentialsProvider");
			config.Remove("connectionStringName");
			config.Remove("serviceUri");
			foreach (object obj in config.Keys)
			{
				string text2 = (string)obj;
				if (!string.IsNullOrEmpty(text2))
				{
					throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.AttributeNotRecognized, new object[]
					{
						text2
					}));
				}
			}
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x0003159C File Offset: 0x0002F79C
		public override bool ValidateUser(string username, string password)
		{
			return this.ValidateUserCore(username, password, 2);
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x000315A7 File Offset: 0x0002F7A7
		public bool ValidateUser(string username, string password, bool rememberMe)
		{
			return this.ValidateUserCore(username, password, rememberMe ? 1 : 0);
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x000315B8 File Offset: 0x0002F7B8
		private bool ValidateUserCore(string username, string password, int rememberMeInt)
		{
			bool result;
			lock (this)
			{
				int i = string.IsNullOrEmpty(username) ? 0 : 3;
				if (this.ValidateUserCore(username, password, rememberMeInt, ref i, true))
				{
					if (this.UserValidated != null)
					{
						this.UserValidated(this, new UserValidatedEventArgs(Thread.CurrentPrincipal.Identity.Name));
					}
					result = true;
				}
				else
				{
					if (!string.IsNullOrEmpty(this._GetCredentialsTypeName))
					{
						while (i < 3)
						{
							if (this.ValidateUserCore(null, password, rememberMeInt, ref i, false))
							{
								if (this.UserValidated != null)
								{
									this.UserValidated(this, new UserValidatedEventArgs(Thread.CurrentPrincipal.Identity.Name));
								}
								return true;
							}
						}
					}
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x00031684 File Offset: 0x0002F884
		public void Logout()
		{
			IPrincipal currentPrincipal = Thread.CurrentPrincipal;
			if (currentPrincipal == null || !(currentPrincipal.Identity is ClientFormsIdentity))
			{
				return;
			}
			lock (this)
			{
				if (!ConnectivityStatus.IsOffline)
				{
					CookieContainer authenticationCookies = ((ClientFormsIdentity)currentPrincipal.Identity).AuthenticationCookies;
					if (this._UsingWFCService)
					{
						throw new NotImplementedException();
					}
					ProxyHelper.CreateWebRequestAndGetResponse(this.GetServiceUri() + "/Logout", ref authenticationCookies, currentPrincipal.Identity.Name, this._ConnectionString, this._ConnectionStringProvider, null, null, null);
				}
				SqlHelper.DeleteAllCookies(currentPrincipal.Identity.Name, this._ConnectionString, this._ConnectionStringProvider);
				Thread.CurrentPrincipal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
			}
			this.StoreLastUserNameInOffileStore(null);
			if (this.UserValidated != null)
			{
				this.UserValidated(this, new UserValidatedEventArgs(""));
			}
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x00031778 File Offset: 0x0002F978
		private string GetServiceUri()
		{
			if (string.IsNullOrEmpty(this._ServiceUri))
			{
				throw new ArgumentException(AtlasWeb.ServiceUriNotFound);
			}
			return this._ServiceUri;
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06000E13 RID: 3603 RVA: 0x00031798 File Offset: 0x0002F998
		// (set) Token: 0x06000E14 RID: 3604 RVA: 0x000317A0 File Offset: 0x0002F9A0
		public string ServiceUri
		{
			get
			{
				return this._ServiceUri;
			}
			set
			{
				this._ServiceUri = value;
				if (string.IsNullOrEmpty(this._ServiceUri))
				{
					this._UsingWFCService = false;
					return;
				}
				this._UsingWFCService = this._ServiceUri.EndsWith(".svc", StringComparison.OrdinalIgnoreCase);
			}
		}

		// Token: 0x14000047 RID: 71
		// (add) Token: 0x06000E15 RID: 3605 RVA: 0x000317D8 File Offset: 0x0002F9D8
		// (remove) Token: 0x06000E16 RID: 3606 RVA: 0x00031810 File Offset: 0x0002FA10
		public event EventHandler<UserValidatedEventArgs> UserValidated;

		// Token: 0x06000E17 RID: 3607 RVA: 0x00031848 File Offset: 0x0002FA48
		private bool ValidateUserCore(string username, string password, int rememberMeInt, ref int promptCount, bool tryToUseLastLoggedInUser)
		{
			string text = null;
			bool flag = false;
			string text2 = tryToUseLastLoggedInUser ? this.GetLastUserNameFromOffileStore() : null;
			bool flag2 = string.IsNullOrEmpty(username);
			CookieContainer cookieContainer = null;
			bool flag3 = false;
			bool flag4 = rememberMeInt == 1;
			bool flag5 = rememberMeInt != 2;
			if (Thread.CurrentPrincipal != null && Thread.CurrentPrincipal.Identity is ClientFormsIdentity)
			{
				text = Thread.CurrentPrincipal.Identity.Name;
			}
			if (string.IsNullOrEmpty(text2) && text != null)
			{
				text2 = text;
			}
			if (flag2)
			{
				username = text2;
			}
			if (Thread.CurrentPrincipal is ClientRolePrincipal && Thread.CurrentPrincipal.Identity is ClientFormsIdentity && Thread.CurrentPrincipal.Identity.Name == username)
			{
				cookieContainer = ((ClientFormsIdentity)Thread.CurrentPrincipal.Identity).AuthenticationCookies;
			}
			if (!string.IsNullOrEmpty(text2) && string.Compare(text2, username, StringComparison.OrdinalIgnoreCase) == 0)
			{
				if (!ConnectivityStatus.IsOffline)
				{
					flag = this.ValidateByCallingIsLoggedIn(text2, ref cookieContainer);
				}
				else
				{
					flag = ProxyHelper.DoAnyCookiesExist(this.GetServiceUri(), text2, this._ConnectionString, this._ConnectionStringProvider);
				}
				flag3 = true;
			}
			if (!flag)
			{
				if (flag2)
				{
					promptCount++;
					if (!this.GetCredsFromUI(ref username, ref password, ref flag4))
					{
						promptCount += 100;
						return false;
					}
					flag5 = true;
				}
				if (!ConnectivityStatus.IsOffline)
				{
					if (!ClientFormsAuthenticationMembershipProvider.ValidateUserByCallingLogin(username, password, flag4, this.GetServiceUri(), this._UsingWFCService, ref cookieContainer, this._ConnectionString, this._ConnectionStringProvider))
					{
						return false;
					}
					this.StoreHashedPasswordInDB(username, password);
				}
				else if (!this.ValidateUserWithOfflineStore(username, password))
				{
					return false;
				}
			}
			if (!flag3 || flag5)
			{
				this.StoreLastUserNameInOffileStore(flag4 ? username : null);
			}
			if (!(Thread.CurrentPrincipal is ClientRolePrincipal) || !(Thread.CurrentPrincipal.Identity is ClientFormsIdentity) || Thread.CurrentPrincipal.Identity.Name != username)
			{
				if (cookieContainer == null)
				{
					cookieContainer = ProxyHelper.ConstructCookieContainer(this.GetServiceUri(), username, this._ConnectionString, this._ConnectionStringProvider);
				}
				Thread.CurrentPrincipal = new ClientRolePrincipal(new ClientFormsIdentity(username, password, this, "ClientForms", true, cookieContainer));
			}
			if (text != null && string.Compare(username, text, StringComparison.OrdinalIgnoreCase) != 0)
			{
				SqlHelper.DeleteAllCookies(text, this._ConnectionString, this._ConnectionStringProvider);
			}
			return true;
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x00031A5C File Offset: 0x0002FC5C
		private string GetLastUserNameFromOffileStore()
		{
			if (this._UsingFileSystemStore || this._UsingIsolatedStore)
			{
				return ClientDataManager.GetAppClientData(this._UsingIsolatedStore).LastLoggedInUserName;
			}
			string result;
			using (DbConnection connection = SqlHelper.GetConnection(null, this._ConnectionString, this._ConnectionStringProvider))
			{
				DbTransaction dbTransaction = null;
				try
				{
					dbTransaction = connection.BeginTransaction();
					DbCommand dbCommand = connection.CreateCommand();
					dbCommand.Transaction = dbTransaction;
					dbCommand.CommandText = "SELECT PropertyValue FROM ApplicationProperties WHERE PropertyName = N'LastLoggedInUserName'";
					object obj = dbCommand.ExecuteScalar();
					result = ((obj != null) ? obj.ToString() : null);
				}
				catch
				{
					if (dbTransaction != null)
					{
						dbTransaction.Rollback();
						dbTransaction = null;
					}
					throw;
				}
				finally
				{
					if (dbTransaction != null)
					{
						dbTransaction.Commit();
					}
				}
			}
			return result;
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x00031B24 File Offset: 0x0002FD24
		private void StoreLastUserNameInOffileStore(string username)
		{
			if (this._UsingFileSystemStore || this._UsingIsolatedStore)
			{
				ClientData appClientData = ClientDataManager.GetAppClientData(this._UsingIsolatedStore);
				appClientData.LastLoggedInUserName = username;
				appClientData.LastLoggedInDateUtc = DateTime.UtcNow;
				appClientData.Save();
				return;
			}
			using (DbConnection connection = SqlHelper.GetConnection(null, this._ConnectionString, this._ConnectionStringProvider))
			{
				DbTransaction dbTransaction = null;
				try
				{
					dbTransaction = connection.BeginTransaction();
					DbCommand dbCommand = connection.CreateCommand();
					dbCommand.Transaction = dbTransaction;
					dbCommand.CommandText = "DELETE FROM ApplicationProperties WHERE PropertyName = N'LastLoggedInUserName'";
					dbCommand.ExecuteNonQuery();
					if (!string.IsNullOrEmpty(username))
					{
						dbCommand = connection.CreateCommand();
						dbCommand.Transaction = dbTransaction;
						dbCommand.CommandText = "INSERT INTO ApplicationProperties(PropertyName, PropertyValue) VALUES (N'LastLoggedInUserName', @UserName)";
						SqlHelper.AddParameter(connection, dbCommand, "@UserName", username);
						dbCommand.ExecuteNonQuery();
						dbCommand = connection.CreateCommand();
						dbCommand.Transaction = dbTransaction;
						dbCommand.CommandText = "INSERT INTO ApplicationProperties(PropertyName, PropertyValue) VALUES (N'LastLoggedInDate', @Date)";
						SqlHelper.AddParameter(connection, dbCommand, "@Date", DateTime.Now.ToFileTimeUtc().ToString(CultureInfo.InvariantCulture));
						dbCommand.Transaction = dbTransaction;
						dbCommand.ExecuteNonQuery();
					}
				}
				catch
				{
					if (dbTransaction != null)
					{
						dbTransaction.Rollback();
						dbTransaction = null;
					}
					throw;
				}
				finally
				{
					if (dbTransaction != null)
					{
						dbTransaction.Commit();
					}
				}
			}
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x00031C74 File Offset: 0x0002FE74
		private bool GetCredsFromUI(ref string username, ref string password, ref bool rememberMe)
		{
			if (this._GetCredentialsType == null)
			{
				if (string.IsNullOrEmpty(this._GetCredentialsTypeName))
				{
					return false;
				}
				this._GetCredentialsType = Type.GetType(this._GetCredentialsTypeName, true, true);
			}
			ClientFormsAuthenticationCredentials credentials = ((IClientFormsAuthenticationCredentialsProvider)Activator.CreateInstance(this._GetCredentialsType)).GetCredentials();
			if (credentials == null)
			{
				return false;
			}
			username = credentials.UserName;
			password = credentials.Password;
			rememberMe = credentials.RememberMe;
			return true;
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x00031CE8 File Offset: 0x0002FEE8
		private void StoreHashedPasswordInDB(string username, string password)
		{
			if (!this._SavePasswordHash)
			{
				return;
			}
			byte[] array = new byte[16];
			new RNGCryptoServiceProvider().GetBytes(array);
			string text = Convert.ToBase64String(array);
			string text2 = ClientFormsAuthenticationMembershipProvider.EncodePassword(password, array);
			if (this._UsingFileSystemStore || this._UsingIsolatedStore)
			{
				ClientData userClientData = ClientDataManager.GetUserClientData(username, this._UsingIsolatedStore);
				userClientData.PasswordHash = text2;
				userClientData.PasswordSalt = text;
				userClientData.Save();
				return;
			}
			using (DbConnection connection = SqlHelper.GetConnection(username, this._ConnectionString, this._ConnectionStringProvider))
			{
				DbTransaction dbTransaction = null;
				try
				{
					dbTransaction = connection.BeginTransaction();
					DbCommand dbCommand = connection.CreateCommand();
					dbCommand.CommandText = "DELETE FROM UserProperties WHERE PropertyName = @PasswordHashName";
					SqlHelper.AddParameter(connection, dbCommand, "@PasswordHashName", "PasswordHash_" + username);
					dbCommand.Transaction = dbTransaction;
					dbCommand.ExecuteNonQuery();
					dbCommand = connection.CreateCommand();
					dbCommand.CommandText = "DELETE FROM UserProperties WHERE PropertyName = @PasswordSaltName";
					SqlHelper.AddParameter(connection, dbCommand, "@PasswordSaltName", "PasswordSalt_" + username);
					dbCommand.Transaction = dbTransaction;
					dbCommand.ExecuteNonQuery();
					dbCommand = connection.CreateCommand();
					dbCommand.CommandText = "INSERT INTO UserProperties(PropertyName, PropertyValue) VALUES (@PasswordHashName, @PasswordHashValue)";
					SqlHelper.AddParameter(connection, dbCommand, "@PasswordHashName", "PasswordHash_" + username);
					SqlHelper.AddParameter(connection, dbCommand, "@PasswordHashValue", text2);
					dbCommand.Transaction = dbTransaction;
					dbCommand.ExecuteNonQuery();
					dbCommand = connection.CreateCommand();
					dbCommand.CommandText = "INSERT INTO UserProperties(PropertyName, PropertyValue) VALUES (@PasswordSaltName, @PasswordSaltValue)";
					SqlHelper.AddParameter(connection, dbCommand, "@PasswordSaltName", "PasswordSalt_" + username);
					SqlHelper.AddParameter(connection, dbCommand, "@PasswordSaltValue", text);
					dbCommand.Transaction = dbTransaction;
					dbCommand.ExecuteNonQuery();
				}
				catch
				{
					if (dbTransaction != null)
					{
						dbTransaction.Rollback();
						dbTransaction = null;
					}
					throw;
				}
				finally
				{
					if (dbTransaction != null)
					{
						dbTransaction.Commit();
					}
				}
			}
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x00031F08 File Offset: 0x00030108
		private static string EncodePassword(string password, byte[] salt)
		{
			byte[] bytes = Encoding.Unicode.GetBytes(password);
			byte[] array = new byte[salt.Length + bytes.Length];
			salt.CopyTo(array, 0);
			bytes.CopyTo(array, salt.Length);
			byte[] inArray = null;
			using (SHA1 sha = CryptoAlgorithms.CreateSHA1())
			{
				inArray = sha.ComputeHash(array);
			}
			return Convert.ToBase64String(inArray);
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x00031F74 File Offset: 0x00030174
		private bool ValidateByCallingIsLoggedIn(string username, ref CookieContainer cookies)
		{
			if (this._UsingWFCService)
			{
				throw new NotImplementedException();
			}
			object obj = ProxyHelper.CreateWebRequestAndGetResponse(this.GetServiceUri() + "/IsLoggedIn", ref cookies, username, this._ConnectionString, this._ConnectionStringProvider, null, null, typeof(bool));
			return obj != null && obj is bool && (bool)obj;
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x00031FD4 File Offset: 0x000301D4
		private bool ValidateUserWithOfflineStore(string username, string password)
		{
			if (!this._SavePasswordHash)
			{
				return false;
			}
			string text = null;
			string text2 = null;
			if (this._UsingFileSystemStore || this._UsingIsolatedStore)
			{
				ClientData userClientData = ClientDataManager.GetUserClientData(username, this._UsingIsolatedStore);
				text = userClientData.PasswordHash;
				text2 = userClientData.PasswordSalt;
			}
			else
			{
				DbTransaction dbTransaction = null;
				using (DbConnection connection = SqlHelper.GetConnection(username, this._ConnectionString, this._ConnectionStringProvider))
				{
					try
					{
						DbCommand dbCommand = connection.CreateCommand();
						dbCommand.Transaction = dbTransaction;
						dbCommand.CommandText = "SELECT PropertyValue FROM UserProperties WHERE PropertyName = @PasswordHashName";
						SqlHelper.AddParameter(connection, dbCommand, "@PasswordHashName", "PasswordHash_" + username);
						text = (dbCommand.ExecuteScalar() as string);
						dbCommand = connection.CreateCommand();
						dbCommand.Transaction = dbTransaction;
						dbCommand.CommandText = "SELECT PropertyValue FROM UserProperties WHERE PropertyName = @PasswordSaltName";
						SqlHelper.AddParameter(connection, dbCommand, "@PasswordSaltName", "PasswordSalt_" + username);
						text2 = (dbCommand.ExecuteScalar() as string);
					}
					catch
					{
						if (dbTransaction != null)
						{
							dbTransaction.Rollback();
							dbTransaction = null;
						}
						throw;
					}
					finally
					{
						if (dbTransaction != null)
						{
							dbTransaction.Commit();
						}
					}
				}
			}
			if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(text2))
			{
				return false;
			}
			byte[] salt = Convert.FromBase64String(text2);
			return text == ClientFormsAuthenticationMembershipProvider.EncodePassword(password, salt);
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06000E1F RID: 3615 RVA: 0x0001359B File Offset: 0x0001179B
		public override bool EnablePasswordRetrieval
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06000E20 RID: 3616 RVA: 0x0001359B File Offset: 0x0001179B
		public override bool EnablePasswordReset
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06000E21 RID: 3617 RVA: 0x0001359B File Offset: 0x0001179B
		public override bool RequiresQuestionAndAnswer
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06000E22 RID: 3618 RVA: 0x00032138 File Offset: 0x00030338
		// (set) Token: 0x06000E23 RID: 3619 RVA: 0x000032F4 File Offset: 0x000014F4
		public override string ApplicationName
		{
			get
			{
				return "";
			}
			set
			{
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06000E24 RID: 3620 RVA: 0x0003213F File Offset: 0x0003033F
		public override int MaxInvalidPasswordAttempts
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06000E25 RID: 3621 RVA: 0x0003213F File Offset: 0x0003033F
		public override int PasswordAttemptWindow
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06000E26 RID: 3622 RVA: 0x0001359B File Offset: 0x0001179B
		public override bool RequiresUniqueEmail
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06000E27 RID: 3623 RVA: 0x0001D1CA File Offset: 0x0001B3CA
		public override MembershipPasswordFormat PasswordFormat
		{
			get
			{
				return MembershipPasswordFormat.Hashed;
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06000E28 RID: 3624 RVA: 0x0001D1CA File Offset: 0x0001B3CA
		public override int MinRequiredPasswordLength
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06000E29 RID: 3625 RVA: 0x0001359B File Offset: 0x0001179B
		public override int MinRequiredNonAlphanumericCharacters
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06000E2A RID: 3626 RVA: 0x00032146 File Offset: 0x00030346
		public override string PasswordStrengthRegularExpression
		{
			get
			{
				return "*";
			}
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x0003214D File Offset: 0x0003034D
		public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x0003214D File Offset: 0x0003034D
		public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x0003214D File Offset: 0x0003034D
		public override string GetPassword(string username, string answer)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x0003214D File Offset: 0x0003034D
		public override bool ChangePassword(string username, string oldPassword, string newPassword)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x0003214D File Offset: 0x0003034D
		public override string ResetPassword(string username, string answer)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x0003214D File Offset: 0x0003034D
		public override void UpdateUser(MembershipUser user)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x0003214D File Offset: 0x0003034D
		public override bool UnlockUser(string username)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x0003214D File Offset: 0x0003034D
		public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x0003214D File Offset: 0x0003034D
		public override MembershipUser GetUser(string username, bool userIsOnline)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x0003214D File Offset: 0x0003034D
		public override string GetUserNameByEmail(string email)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x0003214D File Offset: 0x0003034D
		public override bool DeleteUser(string username, bool deleteAllRelatedData)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x0003214D File Offset: 0x0003034D
		public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x0003214D File Offset: 0x0003034D
		public override int GetNumberOfUsersOnline()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x0003214D File Offset: 0x0003034D
		public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x0003214D File Offset: 0x0003034D
		public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			throw new NotSupportedException();
		}

		// Token: 0x040003F9 RID: 1017
		private string _GetCredentialsTypeName;

		// Token: 0x040003FA RID: 1018
		private string _ConnectionString;

		// Token: 0x040003FB RID: 1019
		private string _ConnectionStringProvider;

		// Token: 0x040003FC RID: 1020
		private string _ServiceUri;

		// Token: 0x040003FD RID: 1021
		private Type _GetCredentialsType;

		// Token: 0x040003FE RID: 1022
		private bool _SavePasswordHash = true;

		// Token: 0x040003FF RID: 1023
		private bool _UsingFileSystemStore;

		// Token: 0x04000400 RID: 1024
		private bool _UsingIsolatedStore;

		// Token: 0x04000401 RID: 1025
		private bool _UsingWFCService;
	}
}
