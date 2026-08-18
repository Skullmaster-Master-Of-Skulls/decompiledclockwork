using System;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Configuration;
using System.Web.DataAccess;
using System.Web.Management;
using System.Web.Util;

namespace System.Web.Security
{
	// Token: 0x020005F7 RID: 1527
	public class SqlMembershipProvider : MembershipProvider
	{
		// Token: 0x170016B2 RID: 5810
		// (get) Token: 0x06004D15 RID: 19733 RVA: 0x00108557 File Offset: 0x00106757
		public override bool EnablePasswordRetrieval
		{
			get
			{
				return this._EnablePasswordRetrieval;
			}
		}

		// Token: 0x170016B3 RID: 5811
		// (get) Token: 0x06004D16 RID: 19734 RVA: 0x0010855F File Offset: 0x0010675F
		public override bool EnablePasswordReset
		{
			get
			{
				return this._EnablePasswordReset;
			}
		}

		// Token: 0x170016B4 RID: 5812
		// (get) Token: 0x06004D17 RID: 19735 RVA: 0x00108567 File Offset: 0x00106767
		public override bool RequiresQuestionAndAnswer
		{
			get
			{
				return this._RequiresQuestionAndAnswer;
			}
		}

		// Token: 0x170016B5 RID: 5813
		// (get) Token: 0x06004D18 RID: 19736 RVA: 0x0010856F File Offset: 0x0010676F
		public override bool RequiresUniqueEmail
		{
			get
			{
				return this._RequiresUniqueEmail;
			}
		}

		// Token: 0x170016B6 RID: 5814
		// (get) Token: 0x06004D19 RID: 19737 RVA: 0x00108577 File Offset: 0x00106777
		public override MembershipPasswordFormat PasswordFormat
		{
			get
			{
				return this._PasswordFormat;
			}
		}

		// Token: 0x170016B7 RID: 5815
		// (get) Token: 0x06004D1A RID: 19738 RVA: 0x0010857F File Offset: 0x0010677F
		public override int MaxInvalidPasswordAttempts
		{
			get
			{
				return this._MaxInvalidPasswordAttempts;
			}
		}

		// Token: 0x170016B8 RID: 5816
		// (get) Token: 0x06004D1B RID: 19739 RVA: 0x00108587 File Offset: 0x00106787
		public override int PasswordAttemptWindow
		{
			get
			{
				return this._PasswordAttemptWindow;
			}
		}

		// Token: 0x170016B9 RID: 5817
		// (get) Token: 0x06004D1C RID: 19740 RVA: 0x0010858F File Offset: 0x0010678F
		public override int MinRequiredPasswordLength
		{
			get
			{
				return this._MinRequiredPasswordLength;
			}
		}

		// Token: 0x170016BA RID: 5818
		// (get) Token: 0x06004D1D RID: 19741 RVA: 0x00108597 File Offset: 0x00106797
		public override int MinRequiredNonAlphanumericCharacters
		{
			get
			{
				return this._MinRequiredNonalphanumericCharacters;
			}
		}

		// Token: 0x170016BB RID: 5819
		// (get) Token: 0x06004D1E RID: 19742 RVA: 0x0010859F File Offset: 0x0010679F
		public override string PasswordStrengthRegularExpression
		{
			get
			{
				return this._PasswordStrengthRegularExpression;
			}
		}

		// Token: 0x170016BC RID: 5820
		// (get) Token: 0x06004D1F RID: 19743 RVA: 0x001085A7 File Offset: 0x001067A7
		// (set) Token: 0x06004D20 RID: 19744 RVA: 0x001085AF File Offset: 0x001067AF
		public override string ApplicationName
		{
			get
			{
				return this._AppName;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw ExceptionUtil.PropertyNullOrEmpty("ApplicationName");
				}
				if (value.Length > 256)
				{
					throw new ProviderException(SR.GetString("Provider_application_name_too_long"));
				}
				this._AppName = value;
			}
		}

		// Token: 0x06004D21 RID: 19745 RVA: 0x001085E8 File Offset: 0x001067E8
		public override void Initialize(string name, NameValueCollection config)
		{
			HttpRuntime.CheckAspNetHostingPermission(AspNetHostingPermissionLevel.Low, "Feature_not_supported_at_this_level");
			if (config == null)
			{
				throw new ArgumentNullException("config");
			}
			if (string.IsNullOrEmpty(name))
			{
				name = "SqlMembershipProvider";
			}
			if (string.IsNullOrEmpty(config["description"]))
			{
				config.Remove("description");
				config.Add("description", SR.GetString("MembershipSqlProvider_description"));
			}
			base.Initialize(name, config);
			this._SchemaVersionCheck = 0;
			this._EnablePasswordRetrieval = SecUtility.GetBooleanValue(config, "enablePasswordRetrieval", false);
			this._EnablePasswordReset = SecUtility.GetBooleanValue(config, "enablePasswordReset", true);
			this._RequiresQuestionAndAnswer = SecUtility.GetBooleanValue(config, "requiresQuestionAndAnswer", true);
			this._RequiresUniqueEmail = SecUtility.GetBooleanValue(config, "requiresUniqueEmail", true);
			this._MaxInvalidPasswordAttempts = SecUtility.GetIntValue(config, "maxInvalidPasswordAttempts", 5, false, 0);
			this._PasswordAttemptWindow = SecUtility.GetIntValue(config, "passwordAttemptWindow", 10, false, 0);
			this._MinRequiredPasswordLength = SecUtility.GetIntValue(config, "minRequiredPasswordLength", 7, false, 128);
			this._MinRequiredNonalphanumericCharacters = SecUtility.GetIntValue(config, "minRequiredNonalphanumericCharacters", 1, true, 128);
			this._passwordStrengthRegexTimeout = SecUtility.GetNullableIntValue(config, "passwordStrengthRegexTimeout");
			this._PasswordStrengthRegularExpression = config["passwordStrengthRegularExpression"];
			if (this._PasswordStrengthRegularExpression != null)
			{
				this._PasswordStrengthRegularExpression = this._PasswordStrengthRegularExpression.Trim();
				if (this._PasswordStrengthRegularExpression.Length == 0)
				{
					goto IL_17D;
				}
				try
				{
					Regex regex = new Regex(this._PasswordStrengthRegularExpression);
					goto IL_17D;
				}
				catch (ArgumentException ex)
				{
					throw new ProviderException(ex.Message, ex);
				}
			}
			this._PasswordStrengthRegularExpression = string.Empty;
			IL_17D:
			if (this._MinRequiredNonalphanumericCharacters > this._MinRequiredPasswordLength)
			{
				throw new HttpException(SR.GetString("MinRequiredNonalphanumericCharacters_can_not_be_more_than_MinRequiredPasswordLength"));
			}
			this._CommandTimeout = SecUtility.GetIntValue(config, "commandTimeout", 30, true, 0);
			this._AppName = config["applicationName"];
			if (string.IsNullOrEmpty(this._AppName))
			{
				this._AppName = SecUtility.GetDefaultAppName();
			}
			if (this._AppName.Length > 256)
			{
				throw new ProviderException(SR.GetString("Provider_application_name_too_long"));
			}
			string text = config["passwordFormat"];
			if (text == null)
			{
				text = "Hashed";
			}
			if (!(text == "Clear"))
			{
				if (!(text == "Encrypted"))
				{
					if (!(text == "Hashed"))
					{
						throw new ProviderException(SR.GetString("Provider_bad_password_format"));
					}
					this._PasswordFormat = MembershipPasswordFormat.Hashed;
				}
				else
				{
					this._PasswordFormat = MembershipPasswordFormat.Encrypted;
				}
			}
			else
			{
				this._PasswordFormat = MembershipPasswordFormat.Clear;
			}
			if (this.PasswordFormat == MembershipPasswordFormat.Hashed && this.EnablePasswordRetrieval)
			{
				throw new ProviderException(SR.GetString("Provider_can_not_retrieve_hashed_password"));
			}
			this._sqlConnectionString = SecUtility.GetConnectionString(config);
			string value = config["passwordCompatMode"];
			if (!string.IsNullOrEmpty(value))
			{
				this._LegacyPasswordCompatibilityMode = (MembershipPasswordCompatibilityMode)Enum.Parse(typeof(MembershipPasswordCompatibilityMode), value);
			}
			config.Remove("connectionStringName");
			config.Remove("connectionString");
			config.Remove("enablePasswordRetrieval");
			config.Remove("enablePasswordReset");
			config.Remove("requiresQuestionAndAnswer");
			config.Remove("applicationName");
			config.Remove("requiresUniqueEmail");
			config.Remove("maxInvalidPasswordAttempts");
			config.Remove("passwordAttemptWindow");
			config.Remove("commandTimeout");
			config.Remove("passwordFormat");
			config.Remove("name");
			config.Remove("minRequiredPasswordLength");
			config.Remove("minRequiredNonalphanumericCharacters");
			config.Remove("passwordStrengthRegularExpression");
			config.Remove("passwordCompatMode");
			config.Remove("passwordStrengthRegexTimeout");
			if (config.Count > 0)
			{
				string key = config.GetKey(0);
				if (!string.IsNullOrEmpty(key))
				{
					throw new ProviderException(SR.GetString("Provider_unrecognized_attribute", new object[]
					{
						key
					}));
				}
			}
		}

		// Token: 0x06004D22 RID: 19746 RVA: 0x001089B8 File Offset: 0x00106BB8
		private void CheckSchemaVersion(SqlConnection connection)
		{
			string[] features = new string[]
			{
				"Common",
				"Membership"
			};
			string version = "1";
			SecUtility.CheckSchemaVersion(this, connection, features, version, ref this._SchemaVersionCheck);
		}

		// Token: 0x170016BD RID: 5821
		// (get) Token: 0x06004D23 RID: 19747 RVA: 0x001089F1 File Offset: 0x00106BF1
		private int CommandTimeout
		{
			get
			{
				return this._CommandTimeout;
			}
		}

		// Token: 0x06004D24 RID: 19748 RVA: 0x001089FC File Offset: 0x00106BFC
		public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
		{
			if (!SecUtility.ValidateParameter(ref password, true, true, false, 128))
			{
				status = MembershipCreateStatus.InvalidPassword;
				return null;
			}
			string text = this.GenerateSalt();
			string text2 = this.EncodePassword(password, (int)this._PasswordFormat, text);
			if (text2.Length > 128)
			{
				status = MembershipCreateStatus.InvalidPassword;
				return null;
			}
			if (passwordAnswer != null)
			{
				passwordAnswer = passwordAnswer.Trim();
			}
			string objValue;
			if (!string.IsNullOrEmpty(passwordAnswer))
			{
				if (passwordAnswer.Length > 128)
				{
					status = MembershipCreateStatus.InvalidAnswer;
					return null;
				}
				objValue = this.EncodePassword(passwordAnswer.ToLower(CultureInfo.InvariantCulture), (int)this._PasswordFormat, text);
			}
			else
			{
				objValue = passwordAnswer;
			}
			if (!SecUtility.ValidateParameter(ref objValue, this.RequiresQuestionAndAnswer, true, false, 128))
			{
				status = MembershipCreateStatus.InvalidAnswer;
				return null;
			}
			if (!SecUtility.ValidateParameter(ref username, true, true, true, 256))
			{
				status = MembershipCreateStatus.InvalidUserName;
				return null;
			}
			if (!SecUtility.ValidateParameter(ref email, this.RequiresUniqueEmail, this.RequiresUniqueEmail, false, 256))
			{
				status = MembershipCreateStatus.InvalidEmail;
				return null;
			}
			if (!SecUtility.ValidateParameter(ref passwordQuestion, this.RequiresQuestionAndAnswer, true, false, 256))
			{
				status = MembershipCreateStatus.InvalidQuestion;
				return null;
			}
			if (providerUserKey != null && !(providerUserKey is Guid))
			{
				status = MembershipCreateStatus.InvalidProviderUserKey;
				return null;
			}
			if (password.Length < this.MinRequiredPasswordLength)
			{
				status = MembershipCreateStatus.InvalidPassword;
				return null;
			}
			int num = 0;
			for (int i = 0; i < password.Length; i++)
			{
				if (!char.IsLetterOrDigit(password, i))
				{
					num++;
				}
			}
			if (num < this.MinRequiredNonAlphanumericCharacters)
			{
				status = MembershipCreateStatus.InvalidPassword;
				return null;
			}
			if (this.PasswordStrengthRegularExpression.Length > 0 && !RegexUtil.IsMatch(password, this.PasswordStrengthRegularExpression, RegexOptions.None, this._passwordStrengthRegexTimeout))
			{
				status = MembershipCreateStatus.InvalidPassword;
				return null;
			}
			ValidatePasswordEventArgs validatePasswordEventArgs = new ValidatePasswordEventArgs(username, password, true);
			this.OnValidatingPassword(validatePasswordEventArgs);
			if (validatePasswordEventArgs.Cancel)
			{
				status = MembershipCreateStatus.InvalidPassword;
				return null;
			}
			MembershipUser result;
			try
			{
				SqlConnectionHolder sqlConnectionHolder = null;
				try
				{
					sqlConnectionHolder = SqlConnectionHelper.GetConnection(this._sqlConnectionString, true);
					this.CheckSchemaVersion(sqlConnectionHolder.Connection);
					DateTime dateTime = this.RoundToSeconds(DateTime.UtcNow);
					SqlCommand sqlCommand = new SqlCommand("dbo.aspnet_Membership_CreateUser", sqlConnectionHolder.Connection);
					sqlCommand.CommandTimeout = this.CommandTimeout;
					sqlCommand.CommandType = CommandType.StoredProcedure;
					sqlCommand.Parameters.Add(this.CreateInputParam("@ApplicationName", SqlDbType.NVarChar, this.ApplicationName));
					sqlCommand.Parameters.Add(this.CreateInputParam("@UserName", SqlDbType.NVarChar, username));
					sqlCommand.Parameters.Add(this.CreateInputParam("@Password", SqlDbType.NVarChar, text2));
					sqlCommand.Parameters.Add(this.CreateInputParam("@PasswordSalt", SqlDbType.NVarChar, text));
					sqlCommand.Parameters.Add(this.CreateInputParam("@Email", SqlDbType.NVarChar, email));
					sqlCommand.Parameters.Add(this.CreateInputParam("@PasswordQuestion", SqlDbType.NVarChar, passwordQuestion));
					sqlCommand.Parameters.Add(this.CreateInputParam("@PasswordAnswer", SqlDbType.NVarChar, objValue));
					sqlCommand.Parameters.Add(this.CreateInputParam("@IsApproved", SqlDbType.Bit, isApproved));
					sqlCommand.Parameters.Add(this.CreateInputParam("@UniqueEmail", SqlDbType.Int, this.RequiresUniqueEmail ? 1 : 0));
					sqlCommand.Parameters.Add(this.CreateInputParam("@PasswordFormat", SqlDbType.Int, (int)this.PasswordFormat));
					sqlCommand.Parameters.Add(this.CreateInputParam("@CurrentTimeUtc", SqlDbType.DateTime, dateTime));
					SqlParameter sqlParameter = this.CreateInputParam("@UserId", SqlDbType.UniqueIdentifier, providerUserKey);
					sqlParameter.Direction = ParameterDirection.InputOutput;
					sqlCommand.Parameters.Add(sqlParameter);
					sqlParameter = new SqlParameter("@ReturnValue", SqlDbType.Int);
					sqlParameter.Direction = ParameterDirection.ReturnValue;
					sqlCommand.Parameters.Add(sqlParameter);
					try
					{
						sqlCommand.ExecuteNonQuery();
					}
					catch (SqlException ex)
					{
						if (ex.Number == 2627 || ex.Number == 2601 || ex.Number == 2512)
						{
							status = MembershipCreateStatus.DuplicateUserName;
							return null;
						}
						throw;
					}
					int num2 = (sqlParameter.Value != null) ? ((int)sqlParameter.Value) : -1;
					if (num2 < 0 || num2 > 11)
					{
						num2 = 11;
					}
					status = (MembershipCreateStatus)num2;
					if (num2 != 0)
					{
						result = null;
					}
					else
					{
						providerUserKey = new Guid(sqlCommand.Parameters["@UserId"].Value.ToString());
						dateTime = dateTime.ToLocalTime();
						result = new MembershipUser(this.Name, username, providerUserKey, email, passwordQuestion, null, isApproved, false, dateTime, dateTime, dateTime, dateTime, new DateTime(1754, 1, 1));
					}
				}
				finally
				{
					if (sqlConnectionHolder != null)
					{
						sqlConnectionHolder.Close();
						sqlConnectionHolder = null;
					}
				}
			}
			catch
			{
				throw;
			}
			return result;
		}

		// Token: 0x06004D25 RID: 19749 RVA: 0x00108ED4 File Offset: 0x001070D4
		public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
		{
			SecUtility.CheckParameter(ref username, true, true, true, 256, "username");
			SecUtility.CheckParameter(ref password, true, true, false, 128, "password");
			string salt;
			int passwordFormat;
			if (!this.CheckPassword(username, password, false, false, out salt, out passwordFormat))
			{
				return false;
			}
			SecUtility.CheckParameter(ref newPasswordQuestion, this.RequiresQuestionAndAnswer, this.RequiresQuestionAndAnswer, false, 256, "newPasswordQuestion");
			if (newPasswordAnswer != null)
			{
				newPasswordAnswer = newPasswordAnswer.Trim();
			}
			SecUtility.CheckParameter(ref newPasswordAnswer, this.RequiresQuestionAndAnswer, this.RequiresQuestionAndAnswer, false, 128, "newPasswordAnswer");
			string objValue;
			if (!string.IsNullOrEmpty(newPasswordAnswer))
			{
				objValue = this.EncodePassword(newPasswordAnswer.ToLower(CultureInfo.InvariantCulture), passwordFormat, salt);
			}
			else
			{
				objValue = newPasswordAnswer;
			}
			SecUtility.CheckParameter(ref objValue, this.RequiresQuestionAndAnswer, this.RequiresQuestionAndAnswer, false, 128, "newPasswordAnswer");
			bool result;
			try
			{
				SqlConnectionHolder sqlConnectionHolder = null;
				try
				{
					sqlConnectionHolder = SqlConnectionHelper.GetConnection(this._sqlConnectionString, true);
					this.CheckSchemaVersion(sqlConnectionHolder.Connection);
					SqlCommand sqlCommand = new SqlCommand("dbo.aspnet_Membership_ChangePasswordQuestionAndAnswer", sqlConnectionHolder.Connection);
					sqlCommand.CommandTimeout = this.CommandTimeout;
					sqlCommand.CommandType = CommandType.StoredProcedure;
					sqlCommand.Parameters.Add(this.CreateInputParam("@ApplicationName", SqlDbType.NVarChar, this.ApplicationName));
					sqlCommand.Parameters.Add(this.CreateInputParam("@UserName", SqlDbType.NVarChar, username));
					sqlCommand.Parameters.Add(this.CreateInputParam("@NewPasswordQuestion", SqlDbType.NVarChar, newPasswordQuestion));
					sqlCommand.Parameters.Add(this.CreateInputParam("@NewPasswordAnswer", SqlDbType.NVarChar, objValue));
					SqlParameter sqlParameter = new SqlParameter("@ReturnValue", SqlDbType.Int);
					sqlParameter.Direction = ParameterDirection.ReturnValue;
					sqlCommand.Parameters.Add(sqlParameter);
					sqlCommand.ExecuteNonQuery();
					int num = (sqlParameter.Value != null) ? ((int)sqlParameter.Value) : -1;
					if (num != 0)
					{
						throw new ProviderException(SqlMembershipProvider.GetExceptionText(num));
					}
					result = (num == 0);
				}
				finally
				{
					if (sqlConnectionHolder != null)
					{
						sqlConnectionHolder.Close();
						sqlConnectionHolder = null;
					}
				}
			}
			catch
			{
				throw;
			}
			return result;
		}

		// Token: 0x06004D26 RID: 19750 RVA: 0x00109100 File Offset: 0x00107300
		public override string GetPassword(string username, string passwordAnswer)
		{
			if (!this.EnablePasswordRetrieval)
			{
				throw new NotSupportedException(SR.GetString("Membership_PasswordRetrieval_not_supported"));
			}
			SecUtility.CheckParameter(ref username, true, true, true, 256, "username");
			string encodedPasswordAnswer = this.GetEncodedPasswordAnswer(username, passwordAnswer);
			SecUtility.CheckParameter(ref encodedPasswordAnswer, this.RequiresQuestionAndAnswer, this.RequiresQuestionAndAnswer, false, 128, "passwordAnswer");
			int passwordFormat = 0;
			int status = 0;
			string passwordFromDB = this.GetPasswordFromDB(username, encodedPasswordAnswer, this.RequiresQuestionAndAnswer, out passwordFormat, out status);
			if (passwordFromDB != null)
			{
				return this.UnEncodePassword(passwordFromDB, passwordFormat);
			}
			string exceptionText = SqlMembershipProvider.GetExceptionText(status);
			if (SqlMembershipProvider.IsStatusDueToBadPassword(status))
			{
				throw new MembershipPasswordException(exceptionText);
			}
			throw new ProviderException(exceptionText);
		}

		// Token: 0x06004D27 RID: 19751 RVA: 0x001091A4 File Offset: 0x001073A4
		public override bool ChangePassword(string username, string oldPassword, string newPassword)
		{
			SecUtility.CheckParameter(ref username, true, true, true, 256, "username");
			SecUtility.CheckParameter(ref oldPassword, true, true, false, 128, "oldPassword");
			SecUtility.CheckParameter(ref newPassword, true, true, false, 128, "newPassword");
			string text = null;
			int num;
			if (!this.CheckPassword(username, oldPassword, false, false, out text, out num))
			{
				return false;
			}
			if (newPassword.Length < this.MinRequiredPasswordLength)
			{
				throw new ArgumentException(SR.GetString("Password_too_short", new object[]
				{
					"newPassword",
					this.MinRequiredPasswordLength.ToString(CultureInfo.InvariantCulture)
				}));
			}
			int num2 = 0;
			for (int i = 0; i < newPassword.Length; i++)
			{
				if (!char.IsLetterOrDigit(newPassword, i))
				{
					num2++;
				}
			}
			if (num2 < this.MinRequiredNonAlphanumericCharacters)
			{
				throw new ArgumentException(SR.GetString("Password_need_more_non_alpha_numeric_chars", new object[]
				{
					"newPassword",
					this.MinRequiredNonAlphanumericCharacters.ToString(CultureInfo.InvariantCulture)
				}));
			}
			if (this.PasswordStrengthRegularExpression.Length > 0 && !RegexUtil.IsMatch(newPassword, this.PasswordStrengthRegularExpression, RegexOptions.None, this._passwordStrengthRegexTimeout))
			{
				throw new ArgumentException(SR.GetString("Password_does_not_match_regular_expression", new object[]
				{
					"newPassword"
				}));
			}
			string text2 = this.EncodePassword(newPassword, num, text);
			if (text2.Length > 128)
			{
				throw new ArgumentException(SR.GetString("Membership_password_too_long"), "newPassword");
			}
			ValidatePasswordEventArgs validatePasswordEventArgs = new ValidatePasswordEventArgs(username, newPassword, false);
			this.OnValidatingPassword(validatePasswordEventArgs);
			if (!validatePasswordEventArgs.Cancel)
			{
				bool result;
				try
				{
					SqlConnectionHolder sqlConnectionHolder = null;
					try
					{
						sqlConnectionHolder = SqlConnectionHelper.GetConnection(this._sqlConnectionString, true);
						this.CheckSchemaVersion(sqlConnectionHolder.Connection);
						SqlCommand sqlCommand = new SqlCommand("dbo.aspnet_Membership_SetPassword", sqlConnectionHolder.Connection);
						sqlCommand.CommandTimeout = this.CommandTimeout;
						sqlCommand.CommandType = CommandType.StoredProcedure;
						sqlCommand.Parameters.Add(this.CreateInputParam("@ApplicationName", SqlDbType.NVarChar, this.ApplicationName));
						sqlCommand.Parameters.Add(this.CreateInputParam("@UserName", SqlDbType.NVarChar, username));
						sqlCommand.Parameters.Add(this.CreateInputParam("@NewPassword", SqlDbType.NVarChar, text2));
						sqlCommand.Parameters.Add(this.CreateInputParam("@PasswordSalt", SqlDbType.NVarChar, text));
						sqlCommand.Parameters.Add(this.CreateInputParam("@PasswordFormat", SqlDbType.Int, num));
						sqlCommand.Parameters.Add(this.CreateInputParam("@CurrentTimeUtc", SqlDbType.DateTime, DateTime.UtcNow));
						SqlParameter sqlParameter = new SqlParameter("@ReturnValue", SqlDbType.Int);
						sqlParameter.Direction = ParameterDirection.ReturnValue;
						sqlCommand.Parameters.Add(sqlParameter);
						sqlCommand.ExecuteNonQuery();
						int num3 = (sqlParameter.Value != null) ? ((int)sqlParameter.Value) : -1;
						if (num3 != 0)
						{
							string exceptionText = SqlMembershipProvider.GetExceptionText(num3);
							if (SqlMembershipProvider.IsStatusDueToBadPassword(num3))
							{
								throw new MembershipPasswordException(exceptionText);
							}
							throw new ProviderException(exceptionText);
						}
						else
						{
							result = true;
						}
					}
					finally
					{
						if (sqlConnectionHolder != null)
						{
							sqlConnectionHolder.Close();
							sqlConnectionHolder = null;
						}
					}
				}
				catch
				{
					throw;
				}
				return result;
			}
			if (validatePasswordEventArgs.FailureInformation != null)
			{
				throw validatePasswordEventArgs.FailureInformation;
			}
			throw new ArgumentException(SR.GetString("Membership_Custom_Password_Validation_Failure"), "newPassword");
		}

		// Token: 0x06004D28 RID: 19752 RVA: 0x00109508 File Offset: 0x00107708
		public override string ResetPassword(string username, string passwordAnswer)
		{
			if (!this.EnablePasswordReset)
			{
				throw new NotSupportedException(SR.GetString("Not_configured_to_support_password_resets"));
			}
			SecUtility.CheckParameter(ref username, true, true, true, 256, "username");
			int num;
			string text;
			int num2;
			string text2;
			int num3;
			int num4;
			bool flag;
			DateTime dateTime;
			DateTime dateTime2;
			this.GetPasswordWithFormat(username, false, out num, out text, out num2, out text2, out num3, out num4, out flag, out dateTime, out dateTime2);
			if (num != 0)
			{
				if (SqlMembershipProvider.IsStatusDueToBadPassword(num))
				{
					throw new MembershipPasswordException(SqlMembershipProvider.GetExceptionText(num));
				}
				throw new ProviderException(SqlMembershipProvider.GetExceptionText(num));
			}
			else
			{
				if (passwordAnswer != null)
				{
					passwordAnswer = passwordAnswer.Trim();
				}
				string objValue;
				if (!string.IsNullOrEmpty(passwordAnswer))
				{
					objValue = this.EncodePassword(passwordAnswer.ToLower(CultureInfo.InvariantCulture), num2, text2);
				}
				else
				{
					objValue = passwordAnswer;
				}
				SecUtility.CheckParameter(ref objValue, this.RequiresQuestionAndAnswer, this.RequiresQuestionAndAnswer, false, 128, "passwordAnswer");
				string text3 = this.GeneratePassword();
				ValidatePasswordEventArgs validatePasswordEventArgs = new ValidatePasswordEventArgs(username, text3, false);
				this.OnValidatingPassword(validatePasswordEventArgs);
				if (!validatePasswordEventArgs.Cancel)
				{
					string result;
					try
					{
						SqlConnectionHolder sqlConnectionHolder = null;
						try
						{
							sqlConnectionHolder = SqlConnectionHelper.GetConnection(this._sqlConnectionString, true);
							this.CheckSchemaVersion(sqlConnectionHolder.Connection);
							SqlCommand sqlCommand = new SqlCommand("dbo.aspnet_Membership_ResetPassword", sqlConnectionHolder.Connection);
							sqlCommand.CommandTimeout = this.CommandTimeout;
							sqlCommand.CommandType = CommandType.StoredProcedure;
							sqlCommand.Parameters.Add(this.CreateInputParam("@ApplicationName", SqlDbType.NVarChar, this.ApplicationName));
							sqlCommand.Parameters.Add(this.CreateInputParam("@UserName", SqlDbType.NVarChar, username));
							sqlCommand.Parameters.Add(this.CreateInputParam("@NewPassword", SqlDbType.NVarChar, this.EncodePassword(text3, num2, text2)));
							sqlCommand.Parameters.Add(this.CreateInputParam("@MaxInvalidPasswordAttempts", SqlDbType.Int, this.MaxInvalidPasswordAttempts));
							sqlCommand.Parameters.Add(this.CreateInputParam("@PasswordAttemptWindow", SqlDbType.Int, this.PasswordAttemptWindow));
							sqlCommand.Parameters.Add(this.CreateInputParam("@PasswordSalt", SqlDbType.NVarChar, text2));
							sqlCommand.Parameters.Add(this.CreateInputParam("@PasswordFormat", SqlDbType.Int, num2));
							sqlCommand.Parameters.Add(this.CreateInputParam("@CurrentTimeUtc", SqlDbType.DateTime, DateTime.UtcNow));
							if (this.RequiresQuestionAndAnswer)
							{
								sqlCommand.Parameters.Add(this.CreateInputParam("@PasswordAnswer", SqlDbType.NVarChar, objValue));
							}
							SqlParameter sqlParameter = new SqlParameter("@ReturnValue", SqlDbType.Int);
							sqlParameter.Direction = ParameterDirection.ReturnValue;
							sqlCommand.Parameters.Add(sqlParameter);
							sqlCommand.ExecuteNonQuery();
							num = ((sqlParameter.Value != null) ? ((int)sqlParameter.Value) : -1);
							if (num != 0)
							{
								string exceptionText = SqlMembershipProvider.GetExceptionText(num);
								if (SqlMembershipProvider.IsStatusDueToBadPassword(num))
								{
									throw new MembershipPasswordException(exceptionText);
								}
								throw new ProviderException(exceptionText);
							}
							else
							{
								result = text3;
							}
						}
						finally
						{
							if (sqlConnectionHolder != null)
							{
								sqlConnectionHolder.Close();
								sqlConnectionHolder = null;
							}
						}
					}
					catch
					{
						throw;
					}
					return result;
				}
				if (validatePasswordEventArgs.FailureInformation != null)
				{
					throw validatePasswordEventArgs.FailureInformation;
				}
				throw new ProviderException(SR.GetString("Membership_Custom_Password_Validation_Failure"));
			}
		}

		// Token: 0x06004D29 RID: 19753 RVA: 0x00109834 File Offset: 0x00107A34
		public override void UpdateUser(MembershipUser user)
		{
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			string email = user.UserName;
			SecUtility.CheckParameter(ref email, true, true, true, 256, "UserName");
			email = user.Email;
			SecUtility.CheckParameter(ref email, this.RequiresUniqueEmail, this.RequiresUniqueEmail, false, 256, "Email");
			user.Email = email;
			try
			{
				SqlConnectionHolder sqlConnectionHolder = null;
				try
				{
					sqlConnectionHolder = SqlConnectionHelper.GetConnection(this._sqlConnectionString, true);
					this.CheckSchemaVersion(sqlConnectionHolder.Connection);
					SqlCommand sqlCommand = new SqlCommand("dbo.aspnet_Membership_UpdateUser", sqlConnectionHolder.Connection);
					sqlCommand.CommandTimeout = this.CommandTimeout;
					sqlCommand.CommandType = CommandType.StoredProcedure;
					sqlCommand.Parameters.Add(this.CreateInputParam("@ApplicationName", SqlDbType.NVarChar, this.ApplicationName));
					sqlCommand.Parameters.Add(this.CreateInputParam("@UserName", SqlDbType.NVarChar, user.UserName));
					sqlCommand.Parameters.Add(this.CreateInputParam("@Email", SqlDbType.NVarChar, user.Email));
					sqlCommand.Parameters.Add(this.CreateInputParam("@Comment", SqlDbType.NText, user.Comment));
					sqlCommand.Parameters.Add(this.CreateInputParam("@IsApproved", SqlDbType.Bit, user.IsApproved ? 1 : 0));
					sqlCommand.Parameters.Add(this.CreateInputParam("@LastLoginDate", SqlDbType.DateTime, user.LastLoginDate.ToUniversalTime()));
					sqlCommand.Parameters.Add(this.CreateInputParam("@LastActivityDate", SqlDbType.DateTime, user.LastActivityDate.ToUniversalTime()));
					sqlCommand.Parameters.Add(this.CreateInputParam("@UniqueEmail", SqlDbType.Int, this.RequiresUniqueEmail ? 1 : 0));
					sqlCommand.Parameters.Add(this.CreateInputParam("@CurrentTimeUtc", SqlDbType.DateTime, DateTime.UtcNow));
					SqlParameter sqlParameter = new SqlParameter("@ReturnValue", SqlDbType.Int);
					sqlParameter.Direction = ParameterDirection.ReturnValue;
					sqlCommand.Parameters.Add(sqlParameter);
					sqlCommand.ExecuteNonQuery();
					int num = (sqlParameter.Value != null) ? ((int)sqlParameter.Value) : -1;
					if (num != 0)
					{
						throw new ProviderException(SqlMembershipProvider.GetExceptionText(num));
					}
				}
				finally
				{
					if (sqlConnectionHolder != null)
					{
						sqlConnectionHolder.Close();
						sqlConnectionHolder = null;
					}
				}
			}
			catch
			{
				throw;
			}
		}

		// Token: 0x06004D2A RID: 19754 RVA: 0x00109AB4 File Offset: 0x00107CB4
		public override bool ValidateUser(string username, string password)
		{
			if (SecUtility.ValidateParameter(ref username, true, true, true, 256) && SecUtility.ValidateParameter(ref password, true, true, false, 128) && this.CheckPassword(username, password, true, true))
			{
				PerfCounters.IncrementCounter(AppPerfCounter.MEMBER_SUCCESS);
				WebBaseEvent.RaiseSystemEvent(null, 4002, username);
				return true;
			}
			PerfCounters.IncrementCounter(AppPerfCounter.MEMBER_FAIL);
			WebBaseEvent.RaiseSystemEvent(null, 4006, username);
			return false;
		}

		// Token: 0x06004D2B RID: 19755 RVA: 0x00109B18 File Offset: 0x00107D18
		public override bool UnlockUser(string username)
		{
			SecUtility.CheckParameter(ref username, true, true, true, 256, "username");
			bool result;
			try
			{
				SqlConnectionHolder sqlConnectionHolder = null;
				try
				{
					sqlConnectionHolder = SqlConnectionHelper.GetConnection(this._sqlConnectionString, true);
					this.CheckSchemaVersion(sqlConnectionHolder.Connection);
					SqlCommand sqlCommand = new SqlCommand("dbo.aspnet_Membership_UnlockUser", sqlConnectionHolder.Connection);
					sqlCommand.CommandTimeout = this.CommandTimeout;
					sqlCommand.CommandType = CommandType.StoredProcedure;
					sqlCommand.Parameters.Add(this.CreateInputParam("@ApplicationName", SqlDbType.NVarChar, this.ApplicationName));
					sqlCommand.Parameters.Add(this.CreateInputParam("@UserName", SqlDbType.NVarChar, username));
					SqlParameter sqlParameter = new SqlParameter("@ReturnValue", SqlDbType.Int);
					sqlParameter.Direction = ParameterDirection.ReturnValue;
					sqlCommand.Parameters.Add(sqlParameter);
					sqlCommand.ExecuteNonQuery();
					if (((sqlParameter.Value != null) ? ((int)sqlParameter.Value) : -1) == 0)
					{
						result = true;
					}
					else
					{
						result = false;
					}
				}
				finally
				{
					if (sqlConnectionHolder != null)
					{
						sqlConnectionHolder.Close();
						sqlConnectionHolder = null;
					}
				}
			}
			catch
			{
				throw;
			}
			return result;
		}

		// Token: 0x06004D2C RID: 19756 RVA: 0x00109C2C File Offset: 0x00107E2C
		public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
		{
			if (providerUserKey == null)
			{
				throw new ArgumentNullException("providerUserKey");
			}
			if (!(providerUserKey is Guid))
			{
				throw new ArgumentException(SR.GetString("Membership_InvalidProviderUserKey"), "providerUserKey");
			}
			SqlDataReader sqlDataReader = null;
			MembershipUser result;
			try
			{
				SqlConnectionHolder sqlConnectionHolder = null;
				try
				{
					sqlConnectionHolder = SqlConnectionHelper.GetConnection(this._sqlConnectionString, true);
					this.CheckSchemaVersion(sqlConnectionHolder.Connection);
					SqlCommand sqlCommand = new SqlCommand("dbo.aspnet_Membership_GetUserByUserId", sqlConnectionHolder.Connection);
					sqlCommand.CommandTimeout = this.CommandTimeout;
					sqlCommand.CommandType = CommandType.StoredProcedure;
					sqlCommand.Parameters.Add(this.CreateInputParam("@UserId", SqlDbType.UniqueIdentifier, providerUserKey));
					sqlCommand.Parameters.Add(this.CreateInputParam("@UpdateLastActivity", SqlDbType.Bit, userIsOnline));
					sqlCommand.Parameters.Add(this.CreateInputParam("@CurrentTimeUtc", SqlDbType.DateTime, DateTime.UtcNow));
					SqlParameter sqlParameter = new SqlParameter("@ReturnValue", SqlDbType.Int);
					sqlParameter.Direction = ParameterDirection.ReturnValue;
					sqlCommand.Parameters.Add(sqlParameter);
					sqlDataReader = sqlCommand.ExecuteReader();
					if (sqlDataReader.Read())
					{
						string nullableString = this.GetNullableString(sqlDataReader, 0);
						string nullableString2 = this.GetNullableString(sqlDataReader, 1);
						string nullableString3 = this.GetNullableString(sqlDataReader, 2);
						bool boolean = sqlDataReader.GetBoolean(3);
						DateTime creationDate = sqlDataReader.GetDateTime(4).ToLocalTime();
						DateTime lastLoginDate = sqlDataReader.GetDateTime(5).ToLocalTime();
						DateTime lastActivityDate = sqlDataReader.GetDateTime(6).ToLocalTime();
						DateTime lastPasswordChangedDate = sqlDataReader.GetDateTime(7).ToLocalTime();
						string nullableString4 = this.GetNullableString(sqlDataReader, 8);
						bool boolean2 = sqlDataReader.GetBoolean(9);
						DateTime lastLockoutDate = sqlDataReader.GetDateTime(10).ToLocalTime();
						result = new MembershipUser(this.Name, nullableString4, providerUserKey, nullableString, nullableString2, nullableString3, boolean, boolean2, creationDate, lastLoginDate, lastActivityDate, lastPasswordChangedDate, lastLockoutDate);
					}
					else
					{
						result = null;
					}
				}
				finally
				{
					if (sqlDataReader != null)
					{
						sqlDataReader.Close();
						sqlDataReader = null;
					}
					if (sqlConnectionHolder != null)
					{
						sqlConnectionHolder.Close();
						sqlConnectionHolder = null;
					}
				}
			}
			catch
			{
				throw;
			}
			return result;
		}

		// Token: 0x06004D2D RID: 19757 RVA: 0x00109E44 File Offset: 0x00108044
		public override MembershipUser GetUser(string username, bool userIsOnline)
		{
			SecUtility.CheckParameter(ref username, true, false, true, 256, "username");
			SqlDataReader sqlDataReader = null;
			MembershipUser result;
			try
			{
				SqlConnectionHolder sqlConnectionHolder = null;
				try
				{
					sqlConnectionHolder = SqlConnectionHelper.GetConnection(this._sqlConnectionString, true);
					this.CheckSchemaVersion(sqlConnectionHolder.Connection);
					SqlCommand sqlCommand = new SqlCommand("dbo.aspnet_Membership_GetUserByName", sqlConnectionHolder.Connection);
					sqlCommand.CommandTimeout = this.CommandTimeout;
					sqlCommand.CommandType = CommandType.StoredProcedure;
					sqlCommand.Parameters.Add(this.CreateInputParam("@ApplicationName", SqlDbType.NVarChar, this.ApplicationName));
					sqlCommand.Parameters.Add(this.CreateInputParam("@UserName", SqlDbType.NVarChar, username));
					sqlCommand.Parameters.Add(this.CreateInputParam("@UpdateLastActivity", SqlDbType.Bit, userIsOnline));
					sqlCommand.Parameters.Add(this.CreateInputParam("@CurrentTimeUtc", SqlDbType.DateTime, DateTime.UtcNow));
					SqlParameter sqlParameter = new SqlParameter("@ReturnValue", SqlDbType.Int);
					sqlParameter.Direction = ParameterDirection.ReturnValue;
					sqlCommand.Parameters.Add(sqlParameter);
					sqlDataReader = sqlCommand.ExecuteReader();
					if (sqlDataReader.Read())
					{
						string nullableString = this.GetNullableString(sqlDataReader, 0);
						string nullableString2 = this.GetNullableString(sqlDataReader, 1);
						string nullableString3 = this.GetNullableString(sqlDataReader, 2);
						bool boolean = sqlDataReader.GetBoolean(3);
						DateTime creationDate = sqlDataReader.GetDateTime(4).ToLocalTime();
						DateTime lastLoginDate = sqlDataReader.GetDateTime(5).ToLocalTime();
						DateTime lastActivityDate = sqlDataReader.GetDateTime(6).ToLocalTime();
						DateTime lastPasswordChangedDate = sqlDataReader.GetDateTime(7).ToLocalTime();
						Guid guid = sqlDataReader.GetGuid(8);
						bool boolean2 = sqlDataReader.GetBoolean(9);
						DateTime lastLockoutDate = sqlDataReader.GetDateTime(10).ToLocalTime();
						result = new MembershipUser(this.Name, username, guid, nullableString, nullableString2, nullableString3, boolean, boolean2, creationDate, lastLoginDate, lastActivityDate, lastPasswordChangedDate, lastLockoutDate);
					}
					else
					{
						result = null;
					}
				}
				finally
				{
					if (sqlDataReader != null)
					{
						sqlDataReader.Close();
						sqlDataReader = null;
					}
					if (sqlConnectionHolder != null)
					{
						sqlConnectionHolder.Close();
						sqlConnectionHolder = null;
					}
				}
			}
			catch
			{
				throw;
			}
			return result;
		}

		// Token: 0x06004D2E RID: 19758 RVA: 0x0010A068 File Offset: 0x00108268
		public override string GetUserNameByEmail(string email)
		{
			SecUtility.CheckParameter(ref email, false, false, false, 256, "email");
			string result;
			try
			{
				SqlConnectionHolder sqlConnectionHolder = null;
				try
				{
					sqlConnectionHolder = SqlConnectionHelper.GetConnection(this._sqlConnectionString, true);
					this.CheckSchemaVersion(sqlConnectionHolder.Connection);
					SqlCommand sqlCommand = new SqlCommand("dbo.aspnet_Membership_GetUserByEmail", sqlConnectionHolder.Connection);
					string text = null;
					SqlDataReader sqlDataReader = null;
					sqlCommand.CommandTimeout = this.CommandTimeout;
					sqlCommand.CommandType = CommandType.StoredProcedure;
					sqlCommand.Parameters.Add(this.CreateInputParam("@ApplicationName", SqlDbType.NVarChar, this.ApplicationName));
					sqlCommand.Parameters.Add(this.CreateInputParam("@Email", SqlDbType.NVarChar, email));
					SqlParameter sqlParameter = new SqlParameter("@ReturnValue", SqlDbType.Int);
					sqlParameter.Direction = ParameterDirection.ReturnValue;
					sqlCommand.Parameters.Add(sqlParameter);
					try
					{
						sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.SequentialAccess);
						if (sqlDataReader.Read())
						{
							text = this.GetNullableString(sqlDataReader, 0);
							if (this.RequiresUniqueEmail && sqlDataReader.Read())
							{
								throw new ProviderException(SR.GetString("Membership_more_than_one_user_with_email"));
							}
						}
					}
					finally
					{
						if (sqlDataReader != null)
						{
							sqlDataReader.Close();
						}
					}
					result = text;
				}
				finally
				{
					if (sqlConnectionHolder != null)
					{
						sqlConnectionHolder.Close();
						sqlConnectionHolder = null;
					}
				}
			}
			catch
			{
				throw;
			}
			return result;
		}

		// Token: 0x06004D2F RID: 19759 RVA: 0x0010A1AC File Offset: 0x001083AC
		public override bool DeleteUser(string username, bool deleteAllRelatedData)
		{
			SecUtility.CheckParameter(ref username, true, true, true, 256, "username");
			bool result;
			try
			{
				SqlConnectionHolder sqlConnectionHolder = null;
				try
				{
					sqlConnectionHolder = SqlConnectionHelper.GetConnection(this._sqlConnectionString, true);
					this.CheckSchemaVersion(sqlConnectionHolder.Connection);
					SqlCommand sqlCommand = new SqlCommand("dbo.aspnet_Users_DeleteUser", sqlConnectionHolder.Connection);
					sqlCommand.CommandTimeout = this.CommandTimeout;
					sqlCommand.CommandType = CommandType.StoredProcedure;
					sqlCommand.Parameters.Add(this.CreateInputParam("@ApplicationName", SqlDbType.NVarChar, this.ApplicationName));
					sqlCommand.Parameters.Add(this.CreateInputParam("@UserName", SqlDbType.NVarChar, username));
					if (deleteAllRelatedData)
					{
						sqlCommand.Parameters.Add(this.CreateInputParam("@TablesToDeleteFrom", SqlDbType.Int, 15));
					}
					else
					{
						sqlCommand.Parameters.Add(this.CreateInputParam("@TablesToDeleteFrom", SqlDbType.Int, 1));
					}
					SqlParameter sqlParameter = new SqlParameter("@NumTablesDeletedFrom", SqlDbType.Int);
					sqlParameter.Direction = ParameterDirection.Output;
					sqlCommand.Parameters.Add(sqlParameter);
					sqlCommand.ExecuteNonQuery();
					int num = (sqlParameter.Value != null) ? ((int)sqlParameter.Value) : -1;
					result = (num > 0);
				}
				finally
				{
					if (sqlConnectionHolder != null)
					{
						sqlConnectionHolder.Close();
						sqlConnectionHolder = null;
					}
				}
			}
			catch
			{
				throw;
			}
			return result;
		}

		// Token: 0x06004D30 RID: 19760 RVA: 0x0010A314 File Offset: 0x00108514
		public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
		{
			if (pageIndex < 0)
			{
				throw new ArgumentException(SR.GetString("PageIndex_bad"), "pageIndex");
			}
			if (pageSize < 1)
			{
				throw new ArgumentException(SR.GetString("PageSize_bad"), "pageSize");
			}
			long num = (long)pageIndex * (long)pageSize + (long)pageSize - 1L;
			if (num > 2147483647L)
			{
				throw new ArgumentException(SR.GetString("PageIndex_PageSize_bad"), "pageIndex and pageSize");
			}
			MembershipUserCollection membershipUserCollection = new MembershipUserCollection();
			totalRecords = 0;
			try
			{
				SqlConnectionHolder sqlConnectionHolder = null;
				try
				{
					sqlConnectionHolder = SqlConnectionHelper.GetConnection(this._sqlConnectionString, true);
					this.CheckSchemaVersion(sqlConnectionHolder.Connection);
					SqlCommand sqlCommand = new SqlCommand("dbo.aspnet_Membership_GetAllUsers", sqlConnectionHolder.Connection);
					SqlDataReader sqlDataReader = null;
					SqlParameter sqlParameter = new SqlParameter("@ReturnValue", SqlDbType.Int);
					sqlCommand.CommandTimeout = this.CommandTimeout;
					sqlCommand.CommandType = CommandType.StoredProcedure;
					sqlCommand.Parameters.Add(this.CreateInputParam("@ApplicationName", SqlDbType.NVarChar, this.ApplicationName));
					sqlCommand.Parameters.Add(this.CreateInputParam("@PageIndex", SqlDbType.Int, pageIndex));
					sqlCommand.Parameters.Add(this.CreateInputParam("@PageSize", SqlDbType.Int, pageSize));
					sqlParameter.Direction = ParameterDirection.ReturnValue;
					sqlCommand.Parameters.Add(sqlParameter);
					try
					{
						sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.SequentialAccess);
						while (sqlDataReader.Read())
						{
							string nullableString = this.GetNullableString(sqlDataReader, 0);
							string nullableString2 = this.GetNullableString(sqlDataReader, 1);
							string nullableString3 = this.GetNullableString(sqlDataReader, 2);
							string nullableString4 = this.GetNullableString(sqlDataReader, 3);
							bool boolean = sqlDataReader.GetBoolean(4);
							DateTime creationDate = sqlDataReader.GetDateTime(5).ToLocalTime();
							DateTime lastLoginDate = sqlDataReader.GetDateTime(6).ToLocalTime();
							DateTime lastActivityDate = sqlDataReader.GetDateTime(7).ToLocalTime();
							DateTime lastPasswordChangedDate = sqlDataReader.GetDateTime(8).ToLocalTime();
							Guid guid = sqlDataReader.GetGuid(9);
							bool boolean2 = sqlDataReader.GetBoolean(10);
							DateTime lastLockoutDate = sqlDataReader.GetDateTime(11).ToLocalTime();
							membershipUserCollection.Add(new MembershipUser(this.Name, nullableString, guid, nullableString2, nullableString3, nullableString4, boolean, boolean2, creationDate, lastLoginDate, lastActivityDate, lastPasswordChangedDate, lastLockoutDate));
						}
					}
					finally
					{
						if (sqlDataReader != null)
						{
							sqlDataReader.Close();
						}
						if (sqlParameter.Value != null && sqlParameter.Value is int)
						{
							totalRecords = (int)sqlParameter.Value;
						}
					}
				}
				finally
				{
					if (sqlConnectionHolder != null)
					{
						sqlConnectionHolder.Close();
						sqlConnectionHolder = null;
					}
				}
			}
			catch
			{
				throw;
			}
			return membershipUserCollection;
		}

		// Token: 0x06004D31 RID: 19761 RVA: 0x0010A5D0 File Offset: 0x001087D0
		public override int GetNumberOfUsersOnline()
		{
			int result;
			try
			{
				SqlConnectionHolder sqlConnectionHolder = null;
				try
				{
					sqlConnectionHolder = SqlConnectionHelper.GetConnection(this._sqlConnectionString, true);
					this.CheckSchemaVersion(sqlConnectionHolder.Connection);
					SqlCommand sqlCommand = new SqlCommand("dbo.aspnet_Membership_GetNumberOfUsersOnline", sqlConnectionHolder.Connection);
					SqlParameter sqlParameter = new SqlParameter("@ReturnValue", SqlDbType.Int);
					sqlCommand.CommandTimeout = this.CommandTimeout;
					sqlCommand.CommandType = CommandType.StoredProcedure;
					sqlCommand.Parameters.Add(this.CreateInputParam("@ApplicationName", SqlDbType.NVarChar, this.ApplicationName));
					sqlCommand.Parameters.Add(this.CreateInputParam("@MinutesSinceLastInActive", SqlDbType.Int, Membership.UserIsOnlineTimeWindow));
					sqlCommand.Parameters.Add(this.CreateInputParam("@CurrentTimeUtc", SqlDbType.DateTime, DateTime.UtcNow));
					sqlParameter.Direction = ParameterDirection.ReturnValue;
					sqlCommand.Parameters.Add(sqlParameter);
					sqlCommand.ExecuteNonQuery();
					int num = (sqlParameter.Value != null) ? ((int)sqlParameter.Value) : -1;
					result = num;
				}
				finally
				{
					if (sqlConnectionHolder != null)
					{
						sqlConnectionHolder.Close();
						sqlConnectionHolder = null;
					}
				}
			}
			catch
			{
				throw;
			}
			return result;
		}

		// Token: 0x06004D32 RID: 19762 RVA: 0x0010A6F0 File Offset: 0x001088F0
		public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			SecUtility.CheckParameter(ref usernameToMatch, true, true, false, 256, "usernameToMatch");
			if (pageIndex < 0)
			{
				throw new ArgumentException(SR.GetString("PageIndex_bad"), "pageIndex");
			}
			if (pageSize < 1)
			{
				throw new ArgumentException(SR.GetString("PageSize_bad"), "pageSize");
			}
			long num = (long)pageIndex * (long)pageSize + (long)pageSize - 1L;
			if (num > 2147483647L)
			{
				throw new ArgumentException(SR.GetString("PageIndex_PageSize_bad"), "pageIndex and pageSize");
			}
			MembershipUserCollection result;
			try
			{
				SqlConnectionHolder sqlConnectionHolder = null;
				totalRecords = 0;
				SqlParameter sqlParameter = new SqlParameter("@ReturnValue", SqlDbType.Int);
				sqlParameter.Direction = ParameterDirection.ReturnValue;
				try
				{
					sqlConnectionHolder = SqlConnectionHelper.GetConnection(this._sqlConnectionString, true);
					this.CheckSchemaVersion(sqlConnectionHolder.Connection);
					SqlCommand sqlCommand = new SqlCommand("dbo.aspnet_Membership_FindUsersByName", sqlConnectionHolder.Connection);
					MembershipUserCollection membershipUserCollection = new MembershipUserCollection();
					SqlDataReader sqlDataReader = null;
					sqlCommand.CommandTimeout = this.CommandTimeout;
					sqlCommand.CommandType = CommandType.StoredProcedure;
					sqlCommand.Parameters.Add(this.CreateInputParam("@ApplicationName", SqlDbType.NVarChar, this.ApplicationName));
					sqlCommand.Parameters.Add(this.CreateInputParam("@UserNameToMatch", SqlDbType.NVarChar, usernameToMatch));
					sqlCommand.Parameters.Add(this.CreateInputParam("@PageIndex", SqlDbType.Int, pageIndex));
					sqlCommand.Parameters.Add(this.CreateInputParam("@PageSize", SqlDbType.Int, pageSize));
					sqlCommand.Parameters.Add(sqlParameter);
					try
					{
						sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.SequentialAccess);
						while (sqlDataReader.Read())
						{
							string nullableString = this.GetNullableString(sqlDataReader, 0);
							string nullableString2 = this.GetNullableString(sqlDataReader, 1);
							string nullableString3 = this.GetNullableString(sqlDataReader, 2);
							string nullableString4 = this.GetNullableString(sqlDataReader, 3);
							bool boolean = sqlDataReader.GetBoolean(4);
							DateTime creationDate = sqlDataReader.GetDateTime(5).ToLocalTime();
							DateTime lastLoginDate = sqlDataReader.GetDateTime(6).ToLocalTime();
							DateTime lastActivityDate = sqlDataReader.GetDateTime(7).ToLocalTime();
							DateTime lastPasswordChangedDate = sqlDataReader.GetDateTime(8).ToLocalTime();
							Guid guid = sqlDataReader.GetGuid(9);
							bool boolean2 = sqlDataReader.GetBoolean(10);
							DateTime lastLockoutDate = sqlDataReader.GetDateTime(11).ToLocalTime();
							membershipUserCollection.Add(new MembershipUser(this.Name, nullableString, guid, nullableString2, nullableString3, nullableString4, boolean, boolean2, creationDate, lastLoginDate, lastActivityDate, lastPasswordChangedDate, lastLockoutDate));
						}
						result = membershipUserCollection;
					}
					finally
					{
						if (sqlDataReader != null)
						{
							sqlDataReader.Close();
						}
						if (sqlParameter.Value != null && sqlParameter.Value is int)
						{
							totalRecords = (int)sqlParameter.Value;
						}
					}
				}
				finally
				{
					if (sqlConnectionHolder != null)
					{
						sqlConnectionHolder.Close();
						sqlConnectionHolder = null;
					}
				}
			}
			catch
			{
				throw;
			}
			return result;
		}

		// Token: 0x06004D33 RID: 19763 RVA: 0x0010A9D8 File Offset: 0x00108BD8
		public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			SecUtility.CheckParameter(ref emailToMatch, false, false, false, 256, "emailToMatch");
			if (pageIndex < 0)
			{
				throw new ArgumentException(SR.GetString("PageIndex_bad"), "pageIndex");
			}
			if (pageSize < 1)
			{
				throw new ArgumentException(SR.GetString("PageSize_bad"), "pageSize");
			}
			long num = (long)pageIndex * (long)pageSize + (long)pageSize - 1L;
			if (num > 2147483647L)
			{
				throw new ArgumentException(SR.GetString("PageIndex_PageSize_bad"), "pageIndex and pageSize");
			}
			MembershipUserCollection result;
			try
			{
				SqlConnectionHolder sqlConnectionHolder = null;
				totalRecords = 0;
				SqlParameter sqlParameter = new SqlParameter("@ReturnValue", SqlDbType.Int);
				sqlParameter.Direction = ParameterDirection.ReturnValue;
				try
				{
					sqlConnectionHolder = SqlConnectionHelper.GetConnection(this._sqlConnectionString, true);
					this.CheckSchemaVersion(sqlConnectionHolder.Connection);
					SqlCommand sqlCommand = new SqlCommand("dbo.aspnet_Membership_FindUsersByEmail", sqlConnectionHolder.Connection);
					MembershipUserCollection membershipUserCollection = new MembershipUserCollection();
					SqlDataReader sqlDataReader = null;
					sqlCommand.CommandTimeout = this.CommandTimeout;
					sqlCommand.CommandType = CommandType.StoredProcedure;
					sqlCommand.Parameters.Add(this.CreateInputParam("@ApplicationName", SqlDbType.NVarChar, this.ApplicationName));
					sqlCommand.Parameters.Add(this.CreateInputParam("@EmailToMatch", SqlDbType.NVarChar, emailToMatch));
					sqlCommand.Parameters.Add(this.CreateInputParam("@PageIndex", SqlDbType.Int, pageIndex));
					sqlCommand.Parameters.Add(this.CreateInputParam("@PageSize", SqlDbType.Int, pageSize));
					sqlCommand.Parameters.Add(sqlParameter);
					try
					{
						sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.SequentialAccess);
						while (sqlDataReader.Read())
						{
							string nullableString = this.GetNullableString(sqlDataReader, 0);
							string nullableString2 = this.GetNullableString(sqlDataReader, 1);
							string nullableString3 = this.GetNullableString(sqlDataReader, 2);
							string nullableString4 = this.GetNullableString(sqlDataReader, 3);
							bool boolean = sqlDataReader.GetBoolean(4);
							DateTime creationDate = sqlDataReader.GetDateTime(5).ToLocalTime();
							DateTime lastLoginDate = sqlDataReader.GetDateTime(6).ToLocalTime();
							DateTime lastActivityDate = sqlDataReader.GetDateTime(7).ToLocalTime();
							DateTime lastPasswordChangedDate = sqlDataReader.GetDateTime(8).ToLocalTime();
							Guid guid = sqlDataReader.GetGuid(9);
							bool boolean2 = sqlDataReader.GetBoolean(10);
							DateTime lastLockoutDate = sqlDataReader.GetDateTime(11).ToLocalTime();
							membershipUserCollection.Add(new MembershipUser(this.Name, nullableString, guid, nullableString2, nullableString3, nullableString4, boolean, boolean2, creationDate, lastLoginDate, lastActivityDate, lastPasswordChangedDate, lastLockoutDate));
						}
						result = membershipUserCollection;
					}
					finally
					{
						if (sqlDataReader != null)
						{
							sqlDataReader.Close();
						}
						if (sqlParameter.Value != null && sqlParameter.Value is int)
						{
							totalRecords = (int)sqlParameter.Value;
						}
					}
				}
				finally
				{
					if (sqlConnectionHolder != null)
					{
						sqlConnectionHolder.Close();
						sqlConnectionHolder = null;
					}
				}
			}
			catch
			{
				throw;
			}
			return result;
		}

		// Token: 0x06004D34 RID: 19764 RVA: 0x0010ACC0 File Offset: 0x00108EC0
		private bool CheckPassword(string username, string password, bool updateLastLoginActivityDate, bool failIfNotApproved)
		{
			string text;
			int num;
			return this.CheckPassword(username, password, updateLastLoginActivityDate, failIfNotApproved, out text, out num);
		}

		// Token: 0x06004D35 RID: 19765 RVA: 0x0010ACDC File Offset: 0x00108EDC
		private bool CheckPassword(string username, string password, bool updateLastLoginActivityDate, bool failIfNotApproved, out string salt, out int passwordFormat)
		{
			SqlConnectionHolder sqlConnectionHolder = null;
			int num;
			string text;
			int num2;
			int num3;
			bool flag;
			DateTime dateTime;
			DateTime dateTime2;
			this.GetPasswordWithFormat(username, updateLastLoginActivityDate, out num, out text, out passwordFormat, out salt, out num2, out num3, out flag, out dateTime, out dateTime2);
			if (num != 0)
			{
				return false;
			}
			if (!flag && failIfNotApproved)
			{
				return false;
			}
			string value = this.EncodePassword(password, passwordFormat, salt);
			bool flag2 = text.Equals(value);
			if (flag2 && num2 == 0 && num3 == 0)
			{
				return true;
			}
			try
			{
				try
				{
					sqlConnectionHolder = SqlConnectionHelper.GetConnection(this._sqlConnectionString, true);
					this.CheckSchemaVersion(sqlConnectionHolder.Connection);
					SqlCommand sqlCommand = new SqlCommand("dbo.aspnet_Membership_UpdateUserInfo", sqlConnectionHolder.Connection);
					DateTime utcNow = DateTime.UtcNow;
					sqlCommand.CommandTimeout = this.CommandTimeout;
					sqlCommand.CommandType = CommandType.StoredProcedure;
					sqlCommand.Parameters.Add(this.CreateInputParam("@ApplicationName", SqlDbType.NVarChar, this.ApplicationName));
					sqlCommand.Parameters.Add(this.CreateInputParam("@UserName", SqlDbType.NVarChar, username));
					sqlCommand.Parameters.Add(this.CreateInputParam("@IsPasswordCorrect", SqlDbType.Bit, flag2));
					sqlCommand.Parameters.Add(this.CreateInputParam("@UpdateLastLoginActivityDate", SqlDbType.Bit, updateLastLoginActivityDate));
					sqlCommand.Parameters.Add(this.CreateInputParam("@MaxInvalidPasswordAttempts", SqlDbType.Int, this.MaxInvalidPasswordAttempts));
					sqlCommand.Parameters.Add(this.CreateInputParam("@PasswordAttemptWindow", SqlDbType.Int, this.PasswordAttemptWindow));
					sqlCommand.Parameters.Add(this.CreateInputParam("@CurrentTimeUtc", SqlDbType.DateTime, utcNow));
					sqlCommand.Parameters.Add(this.CreateInputParam("@LastLoginDate", SqlDbType.DateTime, flag2 ? utcNow : dateTime));
					sqlCommand.Parameters.Add(this.CreateInputParam("@LastActivityDate", SqlDbType.DateTime, flag2 ? utcNow : dateTime2));
					SqlParameter sqlParameter = new SqlParameter("@ReturnValue", SqlDbType.Int);
					sqlParameter.Direction = ParameterDirection.ReturnValue;
					sqlCommand.Parameters.Add(sqlParameter);
					sqlCommand.ExecuteNonQuery();
					num = ((sqlParameter.Value != null) ? ((int)sqlParameter.Value) : -1);
				}
				finally
				{
					if (sqlConnectionHolder != null)
					{
						sqlConnectionHolder.Close();
						sqlConnectionHolder = null;
					}
				}
			}
			catch
			{
				throw;
			}
			return flag2;
		}

		// Token: 0x06004D36 RID: 19766 RVA: 0x0010AF44 File Offset: 0x00109144
		private void GetPasswordWithFormat(string username, bool updateLastLoginActivityDate, out int status, out string password, out int passwordFormat, out string passwordSalt, out int failedPasswordAttemptCount, out int failedPasswordAnswerAttemptCount, out bool isApproved, out DateTime lastLoginDate, out DateTime lastActivityDate)
		{
			try
			{
				SqlConnectionHolder sqlConnectionHolder = null;
				SqlDataReader sqlDataReader = null;
				SqlParameter sqlParameter = null;
				try
				{
					sqlConnectionHolder = SqlConnectionHelper.GetConnection(this._sqlConnectionString, true);
					this.CheckSchemaVersion(sqlConnectionHolder.Connection);
					SqlCommand sqlCommand = new SqlCommand("dbo.aspnet_Membership_GetPasswordWithFormat", sqlConnectionHolder.Connection);
					sqlCommand.CommandTimeout = this.CommandTimeout;
					sqlCommand.CommandType = CommandType.StoredProcedure;
					sqlCommand.Parameters.Add(this.CreateInputParam("@ApplicationName", SqlDbType.NVarChar, this.ApplicationName));
					sqlCommand.Parameters.Add(this.CreateInputParam("@UserName", SqlDbType.NVarChar, username));
					sqlCommand.Parameters.Add(this.CreateInputParam("@UpdateLastLoginActivityDate", SqlDbType.Bit, updateLastLoginActivityDate));
					sqlCommand.Parameters.Add(this.CreateInputParam("@CurrentTimeUtc", SqlDbType.DateTime, DateTime.UtcNow));
					sqlParameter = new SqlParameter("@ReturnValue", SqlDbType.Int);
					sqlParameter.Direction = ParameterDirection.ReturnValue;
					sqlCommand.Parameters.Add(sqlParameter);
					sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.SingleRow);
					status = -1;
					if (sqlDataReader.Read())
					{
						password = sqlDataReader.GetString(0);
						passwordFormat = sqlDataReader.GetInt32(1);
						passwordSalt = sqlDataReader.GetString(2);
						failedPasswordAttemptCount = sqlDataReader.GetInt32(3);
						failedPasswordAnswerAttemptCount = sqlDataReader.GetInt32(4);
						isApproved = sqlDataReader.GetBoolean(5);
						lastLoginDate = sqlDataReader.GetDateTime(6);
						lastActivityDate = sqlDataReader.GetDateTime(7);
					}
					else
					{
						password = null;
						passwordFormat = 0;
						passwordSalt = null;
						failedPasswordAttemptCount = 0;
						failedPasswordAnswerAttemptCount = 0;
						isApproved = false;
						lastLoginDate = DateTime.UtcNow;
						lastActivityDate = DateTime.UtcNow;
					}
				}
				finally
				{
					if (sqlDataReader != null)
					{
						sqlDataReader.Close();
						sqlDataReader = null;
						status = ((sqlParameter.Value != null) ? ((int)sqlParameter.Value) : -1);
					}
					if (sqlConnectionHolder != null)
					{
						sqlConnectionHolder.Close();
						sqlConnectionHolder = null;
					}
				}
			}
			catch
			{
				throw;
			}
		}

		// Token: 0x06004D37 RID: 19767 RVA: 0x0010B134 File Offset: 0x00109334
		private string GetPasswordFromDB(string username, string passwordAnswer, bool requiresQuestionAndAnswer, out int passwordFormat, out int status)
		{
			string result;
			try
			{
				SqlConnectionHolder sqlConnectionHolder = null;
				SqlDataReader sqlDataReader = null;
				SqlParameter sqlParameter = null;
				try
				{
					sqlConnectionHolder = SqlConnectionHelper.GetConnection(this._sqlConnectionString, true);
					this.CheckSchemaVersion(sqlConnectionHolder.Connection);
					SqlCommand sqlCommand = new SqlCommand("dbo.aspnet_Membership_GetPassword", sqlConnectionHolder.Connection);
					sqlCommand.CommandTimeout = this.CommandTimeout;
					sqlCommand.CommandType = CommandType.StoredProcedure;
					sqlCommand.Parameters.Add(this.CreateInputParam("@ApplicationName", SqlDbType.NVarChar, this.ApplicationName));
					sqlCommand.Parameters.Add(this.CreateInputParam("@UserName", SqlDbType.NVarChar, username));
					sqlCommand.Parameters.Add(this.CreateInputParam("@MaxInvalidPasswordAttempts", SqlDbType.Int, this.MaxInvalidPasswordAttempts));
					sqlCommand.Parameters.Add(this.CreateInputParam("@PasswordAttemptWindow", SqlDbType.Int, this.PasswordAttemptWindow));
					sqlCommand.Parameters.Add(this.CreateInputParam("@CurrentTimeUtc", SqlDbType.DateTime, DateTime.UtcNow));
					if (requiresQuestionAndAnswer)
					{
						sqlCommand.Parameters.Add(this.CreateInputParam("@PasswordAnswer", SqlDbType.NVarChar, passwordAnswer));
					}
					sqlParameter = new SqlParameter("@ReturnValue", SqlDbType.Int);
					sqlParameter.Direction = ParameterDirection.ReturnValue;
					sqlCommand.Parameters.Add(sqlParameter);
					sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.SingleRow);
					status = -1;
					string text;
					if (sqlDataReader.Read())
					{
						text = sqlDataReader.GetString(0);
						passwordFormat = sqlDataReader.GetInt32(1);
					}
					else
					{
						text = null;
						passwordFormat = 0;
					}
					result = text;
				}
				finally
				{
					if (sqlDataReader != null)
					{
						sqlDataReader.Close();
						sqlDataReader = null;
						status = ((sqlParameter.Value != null) ? ((int)sqlParameter.Value) : -1);
					}
					if (sqlConnectionHolder != null)
					{
						sqlConnectionHolder.Close();
						sqlConnectionHolder = null;
					}
				}
			}
			catch
			{
				throw;
			}
			return result;
		}

		// Token: 0x06004D38 RID: 19768 RVA: 0x0010B304 File Offset: 0x00109504
		private string GetEncodedPasswordAnswer(string username, string passwordAnswer)
		{
			if (passwordAnswer != null)
			{
				passwordAnswer = passwordAnswer.Trim();
			}
			if (string.IsNullOrEmpty(passwordAnswer))
			{
				return passwordAnswer;
			}
			int num;
			string text;
			int passwordFormat;
			string salt;
			int num2;
			int num3;
			bool flag;
			DateTime dateTime;
			DateTime dateTime2;
			this.GetPasswordWithFormat(username, false, out num, out text, out passwordFormat, out salt, out num2, out num3, out flag, out dateTime, out dateTime2);
			if (num == 0)
			{
				return this.EncodePassword(passwordAnswer.ToLower(CultureInfo.InvariantCulture), passwordFormat, salt);
			}
			throw new ProviderException(SqlMembershipProvider.GetExceptionText(num));
		}

		// Token: 0x06004D39 RID: 19769 RVA: 0x000FC004 File Offset: 0x000FA204
		public virtual string GeneratePassword()
		{
			return Membership.GeneratePassword((this.MinRequiredPasswordLength < 14) ? 14 : this.MinRequiredPasswordLength, this.MinRequiredNonAlphanumericCharacters);
		}

		// Token: 0x06004D3A RID: 19770 RVA: 0x0010B364 File Offset: 0x00109564
		private SqlParameter CreateInputParam(string paramName, SqlDbType dbType, object objValue)
		{
			SqlParameter sqlParameter = new SqlParameter(paramName, dbType);
			if (objValue == null)
			{
				sqlParameter.IsNullable = true;
				sqlParameter.Value = DBNull.Value;
			}
			else
			{
				sqlParameter.Value = objValue;
			}
			return sqlParameter;
		}

		// Token: 0x06004D3B RID: 19771 RVA: 0x0010B398 File Offset: 0x00109598
		private string GetNullableString(SqlDataReader reader, int col)
		{
			if (!reader.IsDBNull(col))
			{
				return reader.GetString(col);
			}
			return null;
		}

		// Token: 0x06004D3C RID: 19772 RVA: 0x0010B3AC File Offset: 0x001095AC
		internal static string GetExceptionText(int status)
		{
			string name;
			switch (status)
			{
			case 0:
				return string.Empty;
			case 1:
				name = "Membership_UserNotFound";
				break;
			case 2:
				name = "Membership_WrongPassword";
				break;
			case 3:
				name = "Membership_WrongAnswer";
				break;
			case 4:
				name = "Membership_InvalidPassword";
				break;
			case 5:
				name = "Membership_InvalidQuestion";
				break;
			case 6:
				name = "Membership_InvalidAnswer";
				break;
			case 7:
				name = "Membership_InvalidEmail";
				break;
			default:
				if (status != 99)
				{
					name = "Provider_Error";
				}
				else
				{
					name = "Membership_AccountLockOut";
				}
				break;
			}
			return SR.GetString(name);
		}

		// Token: 0x06004D3D RID: 19773 RVA: 0x0010B438 File Offset: 0x00109638
		internal static bool IsStatusDueToBadPassword(int status)
		{
			return (status >= 2 && status <= 6) || status == 99;
		}

		// Token: 0x06004D3E RID: 19774 RVA: 0x0010B449 File Offset: 0x00109649
		private DateTime RoundToSeconds(DateTime utcDateTime)
		{
			return new DateTime(utcDateTime.Year, utcDateTime.Month, utcDateTime.Day, utcDateTime.Hour, utcDateTime.Minute, utcDateTime.Second, DateTimeKind.Utc);
		}

		// Token: 0x06004D3F RID: 19775 RVA: 0x0010B47C File Offset: 0x0010967C
		private string EncodePassword(string pass, int passwordFormat, string salt)
		{
			if (passwordFormat == 0)
			{
				return pass;
			}
			byte[] bytes = Encoding.Unicode.GetBytes(pass);
			byte[] array = Convert.FromBase64String(salt);
			byte[] inArray;
			if (passwordFormat == 1)
			{
				HashAlgorithm hashAlgorithm = this.GetHashAlgorithm();
				if (hashAlgorithm is KeyedHashAlgorithm)
				{
					KeyedHashAlgorithm keyedHashAlgorithm = (KeyedHashAlgorithm)hashAlgorithm;
					if (keyedHashAlgorithm.Key.Length == array.Length)
					{
						keyedHashAlgorithm.Key = array;
					}
					else if (keyedHashAlgorithm.Key.Length < array.Length)
					{
						byte[] array2 = new byte[keyedHashAlgorithm.Key.Length];
						Buffer.BlockCopy(array, 0, array2, 0, array2.Length);
						keyedHashAlgorithm.Key = array2;
					}
					else
					{
						byte[] array3 = new byte[keyedHashAlgorithm.Key.Length];
						int num;
						for (int i = 0; i < array3.Length; i += num)
						{
							num = Math.Min(array.Length, array3.Length - i);
							Buffer.BlockCopy(array, 0, array3, i, num);
						}
						keyedHashAlgorithm.Key = array3;
					}
					inArray = keyedHashAlgorithm.ComputeHash(bytes);
				}
				else
				{
					byte[] array4 = new byte[array.Length + bytes.Length];
					Buffer.BlockCopy(array, 0, array4, 0, array.Length);
					Buffer.BlockCopy(bytes, 0, array4, array.Length, bytes.Length);
					inArray = hashAlgorithm.ComputeHash(array4);
				}
			}
			else
			{
				byte[] array5 = new byte[array.Length + bytes.Length];
				Buffer.BlockCopy(array, 0, array5, 0, array.Length);
				Buffer.BlockCopy(bytes, 0, array5, array.Length, bytes.Length);
				inArray = this.EncryptPassword(array5, this._LegacyPasswordCompatibilityMode);
			}
			return Convert.ToBase64String(inArray);
		}

		// Token: 0x06004D40 RID: 19776 RVA: 0x0010B5E0 File Offset: 0x001097E0
		private string UnEncodePassword(string pass, int passwordFormat)
		{
			if (passwordFormat == 0)
			{
				return pass;
			}
			if (passwordFormat == 1)
			{
				throw new ProviderException(SR.GetString("Provider_can_not_decode_hashed_password"));
			}
			byte[] encodedPassword = Convert.FromBase64String(pass);
			byte[] array = this.DecryptPassword(encodedPassword);
			if (array == null)
			{
				return null;
			}
			return Encoding.Unicode.GetString(array, 16, array.Length - 16);
		}

		// Token: 0x06004D41 RID: 19777 RVA: 0x0010B630 File Offset: 0x00109830
		private string GenerateSalt()
		{
			byte[] array = new byte[16];
			new RNGCryptoServiceProvider().GetBytes(array);
			return Convert.ToBase64String(array);
		}

		// Token: 0x06004D42 RID: 19778 RVA: 0x0010B658 File Offset: 0x00109858
		private HashAlgorithm GetHashAlgorithm()
		{
			if (this.s_HashAlgorithm != null)
			{
				return HashAlgorithm.Create(this.s_HashAlgorithm);
			}
			string text = Membership.HashAlgorithmType;
			if (this._LegacyPasswordCompatibilityMode == MembershipPasswordCompatibilityMode.Framework20 && !Membership.IsHashAlgorithmFromMembershipConfig && text != "MD5")
			{
				text = "SHA1";
			}
			HashAlgorithm hashAlgorithm = HashAlgorithm.Create(text);
			if (hashAlgorithm == null)
			{
				RuntimeConfig.GetAppConfig().Membership.ThrowHashAlgorithmException();
			}
			this.s_HashAlgorithm = text;
			return hashAlgorithm;
		}

		// Token: 0x04002936 RID: 10550
		internal const int SALT_SIZE = 16;

		// Token: 0x04002937 RID: 10551
		private string _sqlConnectionString;

		// Token: 0x04002938 RID: 10552
		private bool _EnablePasswordRetrieval;

		// Token: 0x04002939 RID: 10553
		private bool _EnablePasswordReset;

		// Token: 0x0400293A RID: 10554
		private bool _RequiresQuestionAndAnswer;

		// Token: 0x0400293B RID: 10555
		private string _AppName;

		// Token: 0x0400293C RID: 10556
		private bool _RequiresUniqueEmail;

		// Token: 0x0400293D RID: 10557
		private int _MaxInvalidPasswordAttempts;

		// Token: 0x0400293E RID: 10558
		private int _CommandTimeout;

		// Token: 0x0400293F RID: 10559
		private int _PasswordAttemptWindow;

		// Token: 0x04002940 RID: 10560
		private int _MinRequiredPasswordLength;

		// Token: 0x04002941 RID: 10561
		private int _MinRequiredNonalphanumericCharacters;

		// Token: 0x04002942 RID: 10562
		private string _PasswordStrengthRegularExpression;

		// Token: 0x04002943 RID: 10563
		private int _SchemaVersionCheck;

		// Token: 0x04002944 RID: 10564
		private MembershipPasswordFormat _PasswordFormat;

		// Token: 0x04002945 RID: 10565
		private MembershipPasswordCompatibilityMode _LegacyPasswordCompatibilityMode;

		// Token: 0x04002946 RID: 10566
		private string s_HashAlgorithm;

		// Token: 0x04002947 RID: 10567
		private int? _passwordStrengthRegexTimeout;

		// Token: 0x04002948 RID: 10568
		private const int PASSWORD_SIZE = 14;
	}
}
