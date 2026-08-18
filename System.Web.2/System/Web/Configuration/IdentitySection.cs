using System;
using System.Configuration;
using System.Text;
using System.Web.Util;

namespace System.Web.Configuration
{
	// Token: 0x02000707 RID: 1799
	public sealed class IdentitySection : ConfigurationSection
	{
		// Token: 0x060056DA RID: 22234 RVA: 0x0012F9EC File Offset: 0x0012DBEC
		static IdentitySection()
		{
			IdentitySection._properties = new ConfigurationPropertyCollection();
			IdentitySection._properties.Add(IdentitySection._propImpersonate);
			IdentitySection._properties.Add(IdentitySection._propUserName);
			IdentitySection._properties.Add(IdentitySection._propPassword);
		}

		// Token: 0x060056DB RID: 22235 RVA: 0x0012FA90 File Offset: 0x0012DC90
		protected override object GetRuntimeObject()
		{
			if (!this._credentialsValidated)
			{
				object credentialsValidatedLock = this._credentialsValidatedLock;
				lock (credentialsValidatedLock)
				{
					if (!this._credentialsValidated)
					{
						this.ValidateCredentials();
						this._credentialsValidated = true;
					}
				}
			}
			return base.GetRuntimeObject();
		}

		// Token: 0x060056DC RID: 22236 RVA: 0x0012FAF0 File Offset: 0x0012DCF0
		public IdentitySection()
		{
			this.impersonateCached = false;
		}

		// Token: 0x1700191B RID: 6427
		// (get) Token: 0x060056DD RID: 22237 RVA: 0x0012FB25 File Offset: 0x0012DD25
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return IdentitySection._properties;
			}
		}

		// Token: 0x1700191C RID: 6428
		// (get) Token: 0x060056DE RID: 22238 RVA: 0x0012FB2C File Offset: 0x0012DD2C
		// (set) Token: 0x060056DF RID: 22239 RVA: 0x0012FB59 File Offset: 0x0012DD59
		[ConfigurationProperty("impersonate", DefaultValue = false)]
		public bool Impersonate
		{
			get
			{
				if (!this.impersonateCached)
				{
					this.impersonateCache = (bool)base[IdentitySection._propImpersonate];
					this.impersonateCached = true;
				}
				return this.impersonateCache;
			}
			set
			{
				base[IdentitySection._propImpersonate] = value;
				this.impersonateCache = value;
			}
		}

		// Token: 0x1700191D RID: 6429
		// (get) Token: 0x060056E0 RID: 22240 RVA: 0x0012FB73 File Offset: 0x0012DD73
		// (set) Token: 0x060056E1 RID: 22241 RVA: 0x0012FB85 File Offset: 0x0012DD85
		[ConfigurationProperty("userName", DefaultValue = "")]
		public string UserName
		{
			get
			{
				return (string)base[IdentitySection._propUserName];
			}
			set
			{
				base[IdentitySection._propUserName] = value;
			}
		}

		// Token: 0x1700191E RID: 6430
		// (get) Token: 0x060056E2 RID: 22242 RVA: 0x0012FB93 File Offset: 0x0012DD93
		// (set) Token: 0x060056E3 RID: 22243 RVA: 0x0012FBA5 File Offset: 0x0012DDA5
		[ConfigurationProperty("password", DefaultValue = "")]
		public string Password
		{
			get
			{
				return (string)base[IdentitySection._propPassword];
			}
			set
			{
				base[IdentitySection._propPassword] = value;
			}
		}

		// Token: 0x060056E4 RID: 22244 RVA: 0x0012FBB4 File Offset: 0x0012DDB4
		protected override void Reset(ConfigurationElement parentElement)
		{
			base.Reset(parentElement);
			IdentitySection identitySection = parentElement as IdentitySection;
			if (identitySection != null)
			{
				this._impersonateTokenRef = identitySection._impersonateTokenRef;
				if (this.Impersonate)
				{
					this.UserName = null;
					this.Password = null;
					this._impersonateTokenRef = new ImpersonateTokenRef(IntPtr.Zero);
				}
				this.impersonateCached = false;
				this._credentialsValidated = false;
			}
		}

		// Token: 0x060056E5 RID: 22245 RVA: 0x0012FC14 File Offset: 0x0012DE14
		protected override void Unmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
		{
			base.Unmerge(sourceElement, parentElement, saveMode);
			IdentitySection identitySection = sourceElement as IdentitySection;
			if (this.Impersonate != identitySection.Impersonate)
			{
				this.Impersonate = identitySection.Impersonate;
			}
			if (this.Impersonate && (identitySection.ElementInformation.Properties[IdentitySection._propUserName.Name].IsModified || identitySection.ElementInformation.Properties[IdentitySection._propPassword.Name].IsModified))
			{
				this.UserName = identitySection.UserName;
				this.Password = identitySection.Password;
			}
		}

		// Token: 0x060056E6 RID: 22246 RVA: 0x0012FCB0 File Offset: 0x0012DEB0
		private void ValidateCredentials()
		{
			this._username = this.UserName;
			this._password = this.Password;
			if (!HandlerBase.CheckAndReadRegistryValue(ref this._username, false))
			{
				throw new ConfigurationErrorsException(SR.GetString("Invalid_registry_config"), base.ElementInformation.Source, base.ElementInformation.LineNumber);
			}
			if (!HandlerBase.CheckAndReadRegistryValue(ref this._password, false))
			{
				throw new ConfigurationErrorsException(SR.GetString("Invalid_registry_config"), base.ElementInformation.Source, base.ElementInformation.LineNumber);
			}
			if (this._username != null && this._username.Length < 1)
			{
				this._username = null;
			}
			if (this._username != null && this.Impersonate)
			{
				if (this._password == null)
				{
					this._password = string.Empty;
				}
			}
			else if (this._password != null && this._username == null && this._password.Length > 0 && this.Impersonate)
			{
				throw new ConfigurationErrorsException(SR.GetString("Invalid_credentials"), base.ElementInformation.Properties["password"].Source, base.ElementInformation.Properties["password"].LineNumber);
			}
			if (!this.Impersonate || !(this.ImpersonateToken == IntPtr.Zero) || this._username == null)
			{
				return;
			}
			if (this.error.Length > 0)
			{
				throw new ConfigurationErrorsException(SR.GetString("Invalid_credentials_2", new object[]
				{
					this.error
				}), base.ElementInformation.Properties["userName"].Source, base.ElementInformation.Properties["userName"].LineNumber);
			}
			throw new ConfigurationErrorsException(SR.GetString("Invalid_credentials"), base.ElementInformation.Properties["userName"].Source, base.ElementInformation.Properties["userName"].LineNumber);
		}

		// Token: 0x060056E7 RID: 22247 RVA: 0x0012FEBC File Offset: 0x0012E0BC
		private void InitializeToken()
		{
			this.error = string.Empty;
			IntPtr token = IdentitySection.CreateUserToken(this._username, this._password, out this.error);
			this._impersonateTokenRef = new ImpersonateTokenRef(token);
			if (!(this._impersonateTokenRef.Handle == IntPtr.Zero))
			{
				return;
			}
			if (this.error.Length > 0)
			{
				throw new ConfigurationErrorsException(SR.GetString("Invalid_credentials_2", new object[]
				{
					this.error
				}), base.ElementInformation.Properties["userName"].Source, base.ElementInformation.Properties["userName"].LineNumber);
			}
			throw new ConfigurationErrorsException(SR.GetString("Invalid_credentials"), base.ElementInformation.Properties["userName"].Source, base.ElementInformation.Properties["userName"].LineNumber);
		}

		// Token: 0x1700191F RID: 6431
		// (get) Token: 0x060056E8 RID: 22248 RVA: 0x0012FFB7 File Offset: 0x0012E1B7
		internal IntPtr ImpersonateToken
		{
			get
			{
				if (this._impersonateTokenRef.Handle == IntPtr.Zero && this._username != null && this.Impersonate)
				{
					this.InitializeToken();
				}
				return this._impersonateTokenRef.Handle;
			}
		}

		// Token: 0x060056E9 RID: 22249 RVA: 0x0012FFF4 File Offset: 0x0012E1F4
		internal static IntPtr CreateUserToken(string name, string password, out string error)
		{
			IntPtr intPtr = IntPtr.Zero;
			if (VersionInfo.ExeName == "aspnet_wp")
			{
				byte[] array = new byte[IntPtr.Size];
				byte[] bytes = Encoding.Unicode.GetBytes(name + "\t" + password);
				byte[] array2 = new byte[bytes.Length + 2];
				Buffer.BlockCopy(bytes, 0, array2, 0, bytes.Length);
				if (UnsafeNativeMethods.PMCallISAPI(IntPtr.Zero, UnsafeNativeMethods.CallISAPIFunc.GenerateToken, array2, array2.Length, array, array.Length) == 1)
				{
					long num = 0L;
					for (int i = 0; i < IntPtr.Size; i++)
					{
						num = num * 256L + (long)((ulong)array[i]);
					}
					intPtr = (IntPtr)num;
				}
			}
			if (intPtr == IntPtr.Zero)
			{
				StringBuilder stringBuilder = new StringBuilder(256);
				intPtr = UnsafeNativeMethods.CreateUserToken(name, password, 1, stringBuilder, 256);
				error = stringBuilder.ToString();
				if (intPtr != IntPtr.Zero)
				{
				}
			}
			else
			{
				error = string.Empty;
			}
			intPtr == IntPtr.Zero;
			return intPtr;
		}

		// Token: 0x17001920 RID: 6432
		// (get) Token: 0x060056EA RID: 22250 RVA: 0x001300F0 File Offset: 0x0012E2F0
		internal ContextInformation ProtectedEvaluationContext
		{
			get
			{
				return base.EvaluationContext;
			}
		}

		// Token: 0x04002E25 RID: 11813
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04002E26 RID: 11814
		private static readonly ConfigurationProperty _propImpersonate = new ConfigurationProperty("impersonate", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04002E27 RID: 11815
		private static readonly ConfigurationProperty _propUserName = new ConfigurationProperty("userName", typeof(string), string.Empty, ConfigurationPropertyOptions.None);

		// Token: 0x04002E28 RID: 11816
		private static readonly ConfigurationProperty _propPassword = new ConfigurationProperty("password", typeof(string), string.Empty, ConfigurationPropertyOptions.None);

		// Token: 0x04002E29 RID: 11817
		private ImpersonateTokenRef _impersonateTokenRef = new ImpersonateTokenRef(IntPtr.Zero);

		// Token: 0x04002E2A RID: 11818
		private string _username;

		// Token: 0x04002E2B RID: 11819
		private string _password;

		// Token: 0x04002E2C RID: 11820
		private bool impersonateCache;

		// Token: 0x04002E2D RID: 11821
		private bool impersonateCached;

		// Token: 0x04002E2E RID: 11822
		private bool _credentialsValidated;

		// Token: 0x04002E2F RID: 11823
		private object _credentialsValidatedLock = new object();

		// Token: 0x04002E30 RID: 11824
		private string error = string.Empty;
	}
}
