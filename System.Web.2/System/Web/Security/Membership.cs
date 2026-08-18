using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using System.Security.Principal;
using System.Threading;
using System.Web.Compilation;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Management;
using System.Web.Util;

namespace System.Web.Security
{
	// Token: 0x020005E8 RID: 1512
	public static class Membership
	{
		// Token: 0x1700166F RID: 5743
		// (get) Token: 0x06004C2E RID: 19502 RVA: 0x0010448D File Offset: 0x0010268D
		public static bool EnablePasswordRetrieval
		{
			get
			{
				Membership.Initialize();
				return Membership.Provider.EnablePasswordRetrieval;
			}
		}

		// Token: 0x17001670 RID: 5744
		// (get) Token: 0x06004C2F RID: 19503 RVA: 0x0010449E File Offset: 0x0010269E
		public static bool EnablePasswordReset
		{
			get
			{
				Membership.Initialize();
				return Membership.Provider.EnablePasswordReset;
			}
		}

		// Token: 0x17001671 RID: 5745
		// (get) Token: 0x06004C30 RID: 19504 RVA: 0x001044AF File Offset: 0x001026AF
		public static bool RequiresQuestionAndAnswer
		{
			get
			{
				Membership.Initialize();
				return Membership.Provider.RequiresQuestionAndAnswer;
			}
		}

		// Token: 0x17001672 RID: 5746
		// (get) Token: 0x06004C31 RID: 19505 RVA: 0x001044C0 File Offset: 0x001026C0
		public static int UserIsOnlineTimeWindow
		{
			get
			{
				Membership.Initialize();
				return Membership.s_UserIsOnlineTimeWindow;
			}
		}

		// Token: 0x17001673 RID: 5747
		// (get) Token: 0x06004C32 RID: 19506 RVA: 0x001044CC File Offset: 0x001026CC
		public static MembershipProviderCollection Providers
		{
			get
			{
				Membership.Initialize();
				return Membership.s_Providers;
			}
		}

		// Token: 0x17001674 RID: 5748
		// (get) Token: 0x06004C33 RID: 19507 RVA: 0x001044D8 File Offset: 0x001026D8
		public static MembershipProvider Provider
		{
			get
			{
				Membership.Initialize();
				if (Membership.s_Provider == null)
				{
					throw new InvalidOperationException(SR.GetString("Def_membership_provider_not_found"));
				}
				return Membership.s_Provider;
			}
		}

		// Token: 0x17001675 RID: 5749
		// (get) Token: 0x06004C34 RID: 19508 RVA: 0x001044FB File Offset: 0x001026FB
		public static string HashAlgorithmType
		{
			get
			{
				Membership.Initialize();
				return Membership.s_HashAlgorithmType;
			}
		}

		// Token: 0x17001676 RID: 5750
		// (get) Token: 0x06004C35 RID: 19509 RVA: 0x00104507 File Offset: 0x00102707
		internal static bool IsHashAlgorithmFromMembershipConfig
		{
			get
			{
				Membership.Initialize();
				return Membership.s_HashAlgorithmFromConfig;
			}
		}

		// Token: 0x17001677 RID: 5751
		// (get) Token: 0x06004C36 RID: 19510 RVA: 0x00104513 File Offset: 0x00102713
		public static int MaxInvalidPasswordAttempts
		{
			get
			{
				Membership.Initialize();
				return Membership.Provider.MaxInvalidPasswordAttempts;
			}
		}

		// Token: 0x17001678 RID: 5752
		// (get) Token: 0x06004C37 RID: 19511 RVA: 0x00104524 File Offset: 0x00102724
		public static int PasswordAttemptWindow
		{
			get
			{
				Membership.Initialize();
				return Membership.Provider.PasswordAttemptWindow;
			}
		}

		// Token: 0x17001679 RID: 5753
		// (get) Token: 0x06004C38 RID: 19512 RVA: 0x00104535 File Offset: 0x00102735
		public static int MinRequiredPasswordLength
		{
			get
			{
				Membership.Initialize();
				return Membership.Provider.MinRequiredPasswordLength;
			}
		}

		// Token: 0x1700167A RID: 5754
		// (get) Token: 0x06004C39 RID: 19513 RVA: 0x00104546 File Offset: 0x00102746
		public static int MinRequiredNonAlphanumericCharacters
		{
			get
			{
				Membership.Initialize();
				return Membership.Provider.MinRequiredNonAlphanumericCharacters;
			}
		}

		// Token: 0x1700167B RID: 5755
		// (get) Token: 0x06004C3A RID: 19514 RVA: 0x00104557 File Offset: 0x00102757
		public static string PasswordStrengthRegularExpression
		{
			get
			{
				Membership.Initialize();
				return Membership.Provider.PasswordStrengthRegularExpression;
			}
		}

		// Token: 0x1700167C RID: 5756
		// (get) Token: 0x06004C3B RID: 19515 RVA: 0x00104568 File Offset: 0x00102768
		// (set) Token: 0x06004C3C RID: 19516 RVA: 0x00104574 File Offset: 0x00102774
		public static string ApplicationName
		{
			get
			{
				return Membership.Provider.ApplicationName;
			}
			set
			{
				Membership.Provider.ApplicationName = value;
			}
		}

		// Token: 0x06004C3D RID: 19517 RVA: 0x00104581 File Offset: 0x00102781
		public static MembershipUser CreateUser(string username, string password)
		{
			return Membership.CreateUser(username, password, null);
		}

		// Token: 0x06004C3E RID: 19518 RVA: 0x0010458C File Offset: 0x0010278C
		public static MembershipUser CreateUser(string username, string password, string email)
		{
			MembershipCreateStatus statusCode;
			MembershipUser membershipUser = Membership.CreateUser(username, password, email, null, null, true, out statusCode);
			if (membershipUser == null)
			{
				throw new MembershipCreateUserException(statusCode);
			}
			return membershipUser;
		}

		// Token: 0x06004C3F RID: 19519 RVA: 0x001045B2 File Offset: 0x001027B2
		public static MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, out MembershipCreateStatus status)
		{
			return Membership.CreateUser(username, password, email, passwordQuestion, passwordAnswer, isApproved, null, out status);
		}

		// Token: 0x06004C40 RID: 19520 RVA: 0x001045C4 File Offset: 0x001027C4
		public static MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
		{
			if (!SecUtility.ValidateParameter(ref username, true, true, true, 0))
			{
				status = MembershipCreateStatus.InvalidUserName;
				return null;
			}
			if (!SecUtility.ValidatePasswordParameter(ref password, 0))
			{
				status = MembershipCreateStatus.InvalidPassword;
				return null;
			}
			if (!SecUtility.ValidateParameter(ref email, false, false, false, 0))
			{
				status = MembershipCreateStatus.InvalidEmail;
				return null;
			}
			if (!SecUtility.ValidateParameter(ref passwordQuestion, false, true, false, 0))
			{
				status = MembershipCreateStatus.InvalidQuestion;
				return null;
			}
			if (!SecUtility.ValidateParameter(ref passwordAnswer, false, true, false, 0))
			{
				status = MembershipCreateStatus.InvalidAnswer;
				return null;
			}
			return Membership.Provider.CreateUser(username, password, email, passwordQuestion, passwordAnswer, isApproved, providerUserKey, out status);
		}

		// Token: 0x06004C41 RID: 19521 RVA: 0x00104643 File Offset: 0x00102843
		public static bool ValidateUser(string username, string password)
		{
			return Membership.Provider.ValidateUser(username, password);
		}

		// Token: 0x06004C42 RID: 19522 RVA: 0x00104651 File Offset: 0x00102851
		public static MembershipUser GetUser()
		{
			return Membership.GetUser(Membership.GetCurrentUserName(), true);
		}

		// Token: 0x06004C43 RID: 19523 RVA: 0x0010465E File Offset: 0x0010285E
		public static MembershipUser GetUser(bool userIsOnline)
		{
			return Membership.GetUser(Membership.GetCurrentUserName(), userIsOnline);
		}

		// Token: 0x06004C44 RID: 19524 RVA: 0x0010466B File Offset: 0x0010286B
		public static MembershipUser GetUser(string username)
		{
			return Membership.GetUser(username, false);
		}

		// Token: 0x06004C45 RID: 19525 RVA: 0x00104674 File Offset: 0x00102874
		public static MembershipUser GetUser(string username, bool userIsOnline)
		{
			SecUtility.CheckParameter(ref username, true, false, true, 0, "username");
			return Membership.Provider.GetUser(username, userIsOnline);
		}

		// Token: 0x06004C46 RID: 19526 RVA: 0x00104692 File Offset: 0x00102892
		public static MembershipUser GetUser(object providerUserKey)
		{
			return Membership.GetUser(providerUserKey, false);
		}

		// Token: 0x06004C47 RID: 19527 RVA: 0x0010469B File Offset: 0x0010289B
		public static MembershipUser GetUser(object providerUserKey, bool userIsOnline)
		{
			if (providerUserKey == null)
			{
				throw new ArgumentNullException("providerUserKey");
			}
			return Membership.Provider.GetUser(providerUserKey, userIsOnline);
		}

		// Token: 0x06004C48 RID: 19528 RVA: 0x001046B7 File Offset: 0x001028B7
		public static string GetUserNameByEmail(string emailToMatch)
		{
			SecUtility.CheckParameter(ref emailToMatch, false, false, false, 0, "emailToMatch");
			return Membership.Provider.GetUserNameByEmail(emailToMatch);
		}

		// Token: 0x06004C49 RID: 19529 RVA: 0x001046D4 File Offset: 0x001028D4
		public static bool DeleteUser(string username)
		{
			SecUtility.CheckParameter(ref username, true, true, true, 0, "username");
			return Membership.Provider.DeleteUser(username, true);
		}

		// Token: 0x06004C4A RID: 19530 RVA: 0x001046F2 File Offset: 0x001028F2
		public static bool DeleteUser(string username, bool deleteAllRelatedData)
		{
			SecUtility.CheckParameter(ref username, true, true, true, 0, "username");
			return Membership.Provider.DeleteUser(username, deleteAllRelatedData);
		}

		// Token: 0x06004C4B RID: 19531 RVA: 0x00104710 File Offset: 0x00102910
		public static void UpdateUser(MembershipUser user)
		{
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			user.Update();
		}

		// Token: 0x06004C4C RID: 19532 RVA: 0x00104728 File Offset: 0x00102928
		public static MembershipUserCollection GetAllUsers()
		{
			int num = 0;
			return Membership.GetAllUsers(0, int.MaxValue, out num);
		}

		// Token: 0x06004C4D RID: 19533 RVA: 0x00104744 File Offset: 0x00102944
		public static MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
		{
			if (pageIndex < 0)
			{
				throw new ArgumentException(SR.GetString("PageIndex_bad"), "pageIndex");
			}
			if (pageSize < 1)
			{
				throw new ArgumentException(SR.GetString("PageSize_bad"), "pageSize");
			}
			return Membership.Provider.GetAllUsers(pageIndex, pageSize, out totalRecords);
		}

		// Token: 0x06004C4E RID: 19534 RVA: 0x00104790 File Offset: 0x00102990
		public static int GetNumberOfUsersOnline()
		{
			return Membership.Provider.GetNumberOfUsersOnline();
		}

		// Token: 0x06004C4F RID: 19535 RVA: 0x0010479C File Offset: 0x0010299C
		public static string GeneratePassword(int length, int numberOfNonAlphanumericCharacters)
		{
			if (length < 1 || length > 128)
			{
				throw new ArgumentException(SR.GetString("Membership_password_length_incorrect"));
			}
			if (numberOfNonAlphanumericCharacters > length || numberOfNonAlphanumericCharacters < 0)
			{
				throw new ArgumentException(SR.GetString("Membership_min_required_non_alphanumeric_characters_incorrect", new object[]
				{
					"numberOfNonAlphanumericCharacters"
				}));
			}
			byte maxVal = (byte)(62 + Membership.punctuations.Length - 1);
			string text;
			int num2;
			do
			{
				char[] array = new char[length];
				int num = 0;
				using (SecUtility.RandomByteBuffer randomByteBuffer = new SecUtility.RandomByteBuffer(2 * length))
				{
					for (int i = 0; i < length; i++)
					{
						byte @byte = randomByteBuffer.GetByte(maxVal);
						if (@byte < 10)
						{
							array[i] = (char)(48 + @byte);
						}
						else if (@byte < 36)
						{
							array[i] = (char)(65 + @byte - 10);
						}
						else if (@byte < 62)
						{
							array[i] = (char)(97 + @byte - 36);
						}
						else
						{
							array[i] = Membership.punctuations[(int)(@byte - 62)];
							num++;
						}
					}
					if (num < numberOfNonAlphanumericCharacters)
					{
						byte maxVal2 = (byte)(Membership.punctuations.Length - 1);
						byte maxVal3 = (byte)(length - 1);
						for (int j = 0; j < numberOfNonAlphanumericCharacters - num; j++)
						{
							int byte2;
							do
							{
								byte2 = (int)randomByteBuffer.GetByte(maxVal3);
							}
							while (!char.IsLetterOrDigit(array[byte2]));
							array[byte2] = Membership.punctuations[(int)randomByteBuffer.GetByte(maxVal2)];
						}
					}
				}
				text = new string(array);
			}
			while (CrossSiteScriptingValidation.IsDangerousString(text, out num2));
			return text;
		}

		// Token: 0x06004C50 RID: 19536 RVA: 0x00104900 File Offset: 0x00102B00
		private static void Initialize()
		{
			if (Membership.s_Initialized && Membership.s_InitializedDefaultProvider)
			{
				return;
			}
			if (Membership.s_InitializeException != null)
			{
				throw Membership.s_InitializeException;
			}
			if (HostingEnvironment.IsHosted)
			{
				HttpRuntime.CheckAspNetHostingPermission(AspNetHostingPermissionLevel.Low, "Feature_not_supported_at_this_level");
			}
			object obj = Membership.s_lock;
			lock (obj)
			{
				if (!Membership.s_Initialized || !Membership.s_InitializedDefaultProvider)
				{
					if (Membership.s_InitializeException != null)
					{
						throw Membership.s_InitializeException;
					}
					bool flag2 = !Membership.s_Initialized;
					bool flag3 = !Membership.s_InitializedDefaultProvider && (!HostingEnvironment.IsHosted || BuildManager.PreStartInitStage == PreStartInitStage.AfterPreStartInit);
					if (flag3 || flag2)
					{
						bool flag4 = false;
						bool flag5;
						try
						{
							RuntimeConfig appConfig = RuntimeConfig.GetAppConfig();
							MembershipSection membership = appConfig.Membership;
							flag5 = Membership.InitializeSettings(flag2, appConfig, membership);
							flag4 = Membership.InitializeDefaultProvider(flag3, membership);
							if (AppSettings.LogMembershipPasswordFormatWarning)
							{
								Membership.CheckedPasswordFormat(membership);
							}
						}
						catch (Exception ex)
						{
							Membership.s_InitializeException = ex;
							throw;
						}
						if (flag5)
						{
							Membership.s_Initialized = true;
						}
						if (flag4)
						{
							Membership.s_InitializedDefaultProvider = true;
						}
					}
				}
			}
		}

		// Token: 0x06004C51 RID: 19537 RVA: 0x00104A20 File Offset: 0x00102C20
		private static void CheckedPasswordFormat(MembershipSection settings)
		{
			try
			{
				if (settings != null && settings.Providers != null)
				{
					foreach (object obj in settings.Providers)
					{
						ProviderSettings providerSettings = (ProviderSettings)obj;
						if (providerSettings != null && providerSettings.Parameters != null)
						{
							string text = providerSettings.Parameters["passwordFormat"];
							if (StringUtil.EqualsIgnoreCase(text, "Clear") || StringUtil.EqualsIgnoreCase(text, "Encrypted"))
							{
								string text2 = providerSettings.Name ?? string.Empty;
								WebBaseEvent.RaiseRuntimeError(new ConfigurationErrorsException(SR.GetString("MembershipPasswordFormat_Obsoleted", new object[]
								{
									text2,
									text
								})), typeof(MembershipProvider));
							}
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x06004C52 RID: 19538 RVA: 0x00104B0C File Offset: 0x00102D0C
		private static bool InitializeSettings(bool initializeGeneralSettings, RuntimeConfig appConfig, MembershipSection settings)
		{
			if (!initializeGeneralSettings)
			{
				return false;
			}
			Membership.s_HashAlgorithmType = settings.HashAlgorithmType;
			Membership.s_HashAlgorithmFromConfig = !string.IsNullOrEmpty(Membership.s_HashAlgorithmType);
			if (!Membership.s_HashAlgorithmFromConfig)
			{
				MachineKeyValidation validation = appConfig.MachineKey.Validation;
				if (validation != MachineKeyValidation.AES && validation != MachineKeyValidation.TripleDES)
				{
					Membership.s_HashAlgorithmType = appConfig.MachineKey.ValidationAlgorithm;
				}
				else
				{
					Membership.s_HashAlgorithmType = "SHA1";
				}
			}
			Membership.s_Providers = new MembershipProviderCollection();
			if (HostingEnvironment.IsHosted)
			{
				ProvidersHelper.InstantiateProviders(settings.Providers, Membership.s_Providers, typeof(MembershipProvider));
			}
			else
			{
				foreach (object obj in settings.Providers)
				{
					ProviderSettings providerSettings = (ProviderSettings)obj;
					Type type = Type.GetType(providerSettings.Type, true, true);
					if (!typeof(MembershipProvider).IsAssignableFrom(type))
					{
						throw new ArgumentException(SR.GetString("Provider_must_implement_type", new object[]
						{
							typeof(MembershipProvider).ToString()
						}));
					}
					MembershipProvider membershipProvider = (MembershipProvider)Activator.CreateInstance(type);
					NameValueCollection parameters = providerSettings.Parameters;
					NameValueCollection nameValueCollection = new NameValueCollection(parameters.Count, StringComparer.Ordinal);
					foreach (object obj2 in parameters)
					{
						string name = (string)obj2;
						nameValueCollection[name] = parameters[name];
					}
					membershipProvider.Initialize(providerSettings.Name, nameValueCollection);
					Membership.s_Providers.Add(membershipProvider);
				}
			}
			Membership.s_UserIsOnlineTimeWindow = (int)settings.UserIsOnlineTimeWindow.TotalMinutes;
			return true;
		}

		// Token: 0x06004C53 RID: 19539 RVA: 0x00104CEC File Offset: 0x00102EEC
		private static bool InitializeDefaultProvider(bool initializeDefaultProvider, MembershipSection settings)
		{
			if (!initializeDefaultProvider)
			{
				return false;
			}
			Membership.s_Providers.SetReadOnly();
			if (settings.DefaultProvider == null || Membership.s_Providers.Count < 1)
			{
				throw new ProviderException(SR.GetString("Def_membership_provider_not_specified"));
			}
			Membership.s_Provider = Membership.s_Providers[settings.DefaultProvider];
			if (Membership.s_Provider == null)
			{
				throw new ConfigurationErrorsException(SR.GetString("Def_membership_provider_not_found"), settings.ElementInformation.Properties["defaultProvider"].Source, settings.ElementInformation.Properties["defaultProvider"].LineNumber);
			}
			return true;
		}

		// Token: 0x06004C54 RID: 19540 RVA: 0x00104D90 File Offset: 0x00102F90
		public static MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			SecUtility.CheckParameter(ref usernameToMatch, true, true, false, 0, "usernameToMatch");
			if (pageIndex < 0)
			{
				throw new ArgumentException(SR.GetString("PageIndex_bad"), "pageIndex");
			}
			if (pageSize < 1)
			{
				throw new ArgumentException(SR.GetString("PageSize_bad"), "pageSize");
			}
			return Membership.Provider.FindUsersByName(usernameToMatch, pageIndex, pageSize, out totalRecords);
		}

		// Token: 0x06004C55 RID: 19541 RVA: 0x00104DF0 File Offset: 0x00102FF0
		public static MembershipUserCollection FindUsersByName(string usernameToMatch)
		{
			SecUtility.CheckParameter(ref usernameToMatch, true, true, false, 0, "usernameToMatch");
			int num = 0;
			return Membership.Provider.FindUsersByName(usernameToMatch, 0, int.MaxValue, out num);
		}

		// Token: 0x06004C56 RID: 19542 RVA: 0x00104E24 File Offset: 0x00103024
		public static MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			SecUtility.CheckParameter(ref emailToMatch, false, false, false, 0, "emailToMatch");
			if (pageIndex < 0)
			{
				throw new ArgumentException(SR.GetString("PageIndex_bad"), "pageIndex");
			}
			if (pageSize < 1)
			{
				throw new ArgumentException(SR.GetString("PageSize_bad"), "pageSize");
			}
			return Membership.Provider.FindUsersByEmail(emailToMatch, pageIndex, pageSize, out totalRecords);
		}

		// Token: 0x06004C57 RID: 19543 RVA: 0x00104E84 File Offset: 0x00103084
		public static MembershipUserCollection FindUsersByEmail(string emailToMatch)
		{
			SecUtility.CheckParameter(ref emailToMatch, false, false, false, 0, "emailToMatch");
			int num = 0;
			return Membership.FindUsersByEmail(emailToMatch, 0, int.MaxValue, out num);
		}

		// Token: 0x06004C58 RID: 19544 RVA: 0x00104EB4 File Offset: 0x001030B4
		private static string GetCurrentUserName()
		{
			if (HostingEnvironment.IsHosted)
			{
				HttpContext httpContext = HttpContext.Current;
				if (httpContext != null)
				{
					return httpContext.User.Identity.Name;
				}
			}
			IPrincipal currentPrincipal = Thread.CurrentPrincipal;
			if (currentPrincipal == null || currentPrincipal.Identity == null)
			{
				return string.Empty;
			}
			return currentPrincipal.Identity.Name;
		}

		// Token: 0x14000129 RID: 297
		// (add) Token: 0x06004C59 RID: 19545 RVA: 0x00104F04 File Offset: 0x00103104
		// (remove) Token: 0x06004C5A RID: 19546 RVA: 0x00104F11 File Offset: 0x00103111
		public static event MembershipValidatePasswordEventHandler ValidatingPassword
		{
			add
			{
				Membership.Provider.ValidatingPassword += value;
			}
			remove
			{
				Membership.Provider.ValidatingPassword -= value;
			}
		}

		// Token: 0x040028FC RID: 10492
		private static readonly char[] punctuations = "!@#$%^&*()_-+=[{]};:>|./?".ToCharArray();

		// Token: 0x040028FD RID: 10493
		private static MembershipProviderCollection s_Providers;

		// Token: 0x040028FE RID: 10494
		private static MembershipProvider s_Provider;

		// Token: 0x040028FF RID: 10495
		private static int s_UserIsOnlineTimeWindow = 15;

		// Token: 0x04002900 RID: 10496
		private static object s_lock = new object();

		// Token: 0x04002901 RID: 10497
		private static bool s_Initialized = false;

		// Token: 0x04002902 RID: 10498
		private static bool s_InitializedDefaultProvider;

		// Token: 0x04002903 RID: 10499
		private static Exception s_InitializeException = null;

		// Token: 0x04002904 RID: 10500
		private static string s_HashAlgorithmType;

		// Token: 0x04002905 RID: 10501
		private static bool s_HashAlgorithmFromConfig;
	}
}
