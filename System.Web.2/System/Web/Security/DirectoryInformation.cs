using System;
using System.Collections;
using System.Configuration.Provider;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.DirectoryServices.Protocols;
using System.Globalization;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Web.Util;

namespace System.Web.Security
{
	// Token: 0x020005C8 RID: 1480
	internal sealed class DirectoryInformation
	{
		// Token: 0x06004B1A RID: 19226 RVA: 0x000FDD44 File Offset: 0x000FBF44
		internal DirectoryInformation(string adspath, NetworkCredential credentials, string connProtection, int clientSearchTimeout, int serverSearchTimeout, bool enablePasswordReset, TimeUnit timeUnit)
		{
			AuthenticationTypes[,] array = new AuthenticationTypes[3, 2];
			RuntimeHelpers.InitializeArray(array, fieldof(<PrivateImplementationDetails>.5744C3703BDDD0AADCC6C51BEFA4D69E15D26AC0190D41D0745C315F43F44100).FieldHandle);
			this.authTypes = array;
			AuthType[,] array2 = new AuthType[3, 2];
			RuntimeHelpers.InitializeArray(array2, fieldof(<PrivateImplementationDetails>.180DB6FC148F0002B00E8AFC8BB28AE0BD194C9443A45D1F99B2E9C6790E88AF).FieldHandle);
			this.ldapAuthTypes = array2;
			base..ctor();
			this.adspath = adspath;
			this.credentials = credentials;
			this.clientSearchTimeout = clientSearchTimeout;
			this.serverSearchTimeout = serverSearchTimeout;
			this.timeUnit = timeUnit;
			if (!adspath.StartsWith("LDAP", StringComparison.Ordinal))
			{
				throw new ProviderException(SR.GetString("ADMembership_OnlyLdap_supported"));
			}
			NativeComInterfaces.IAdsPathname adsPathname = (NativeComInterfaces.IAdsPathname)new NativeComInterfaces.Pathname();
			try
			{
				adsPathname.Set(adspath, 1);
			}
			catch (COMException ex)
			{
				if (ex.ErrorCode == -2147463168)
				{
					throw new ProviderException(SR.GetString("ADMembership_invalid_path"));
				}
				throw;
			}
			try
			{
				this.serverName = adsPathname.Retrieve(9);
			}
			catch (COMException ex2)
			{
				if (ex2.ErrorCode == -2147463168)
				{
					throw new ProviderException(SR.GetString("ADMembership_ServerlessADsPath_not_supported"));
				}
				throw;
			}
			this.creationContainerDN = (this.containerDN = adsPathname.Retrieve(7));
			int num = this.serverName.IndexOf(':');
			if (num != -1)
			{
				string text = this.serverName;
				this.serverName = text.Substring(0, num);
				this.port = int.Parse(text.Substring(num + 1), NumberFormatInfo.InvariantInfo);
				this.portSpecified = true;
			}
			if (string.Compare(connProtection, "Secure", StringComparison.Ordinal) == 0)
			{
				bool flag = false;
				bool flag2 = false;
				if (!this.IsDefaultCredential())
				{
					this.authenticationType = this.GetAuthenticationTypes(ActiveDirectoryConnectionProtection.Ssl, CredentialsType.NonWindows);
					this.ldapAuthType = this.GetLdapAuthenticationTypes(ActiveDirectoryConnectionProtection.Ssl, CredentialsType.NonWindows);
					try
					{
						this.rootdse = new DirectoryEntry(this.GetADsPath("rootdse"), this.GetUsername(), this.GetPassword(), this.authenticationType);
						this.rootdse.RefreshCache();
						this.connectionProtection = ActiveDirectoryConnectionProtection.Ssl;
						if (!this.portSpecified)
						{
							this.port = 636;
							this.portSpecified = true;
						}
						goto IL_22F;
					}
					catch (COMException ex3)
					{
						if (ex3.ErrorCode == -2147023570)
						{
							flag2 = true;
						}
						else
						{
							if (ex3.ErrorCode != -2147016646)
							{
								throw;
							}
							flag = true;
						}
						goto IL_22F;
					}
				}
				flag2 = true;
				IL_22F:
				if (flag2)
				{
					this.authenticationType = this.GetAuthenticationTypes(ActiveDirectoryConnectionProtection.Ssl, CredentialsType.Windows);
					this.ldapAuthType = this.GetLdapAuthenticationTypes(ActiveDirectoryConnectionProtection.Ssl, CredentialsType.Windows);
					try
					{
						this.rootdse = new DirectoryEntry(this.GetADsPath("rootdse"), this.GetUsername(), this.GetPassword(), this.authenticationType);
						this.rootdse.RefreshCache();
						this.connectionProtection = ActiveDirectoryConnectionProtection.Ssl;
						if (!this.portSpecified)
						{
							this.port = 636;
							this.portSpecified = true;
						}
					}
					catch (COMException ex4)
					{
						if (ex4.ErrorCode != -2147016646)
						{
							throw;
						}
						flag = true;
					}
				}
				if (!flag)
				{
					goto IL_3AD;
				}
				this.authenticationType = this.GetAuthenticationTypes(ActiveDirectoryConnectionProtection.SignAndSeal, CredentialsType.Windows);
				this.ldapAuthType = this.GetLdapAuthenticationTypes(ActiveDirectoryConnectionProtection.SignAndSeal, CredentialsType.Windows);
				try
				{
					this.rootdse = new DirectoryEntry(this.GetADsPath("rootdse"), this.GetUsername(), this.GetPassword(), this.authenticationType);
					this.rootdse.RefreshCache();
					this.connectionProtection = ActiveDirectoryConnectionProtection.SignAndSeal;
					goto IL_3AD;
				}
				catch (COMException ex5)
				{
					throw new ProviderException(SR.GetString("ADMembership_Secure_connection_not_established", new object[]
					{
						ex5.Message
					}), ex5);
				}
			}
			if (this.IsDefaultCredential())
			{
				throw new NotSupportedException(SR.GetString("ADMembership_Default_Creds_not_supported"));
			}
			this.authenticationType = this.GetAuthenticationTypes(this.connectionProtection, CredentialsType.NonWindows);
			this.ldapAuthType = this.GetLdapAuthenticationTypes(this.connectionProtection, CredentialsType.NonWindows);
			this.rootdse = new DirectoryEntry(this.GetADsPath("rootdse"), this.GetUsername(), this.GetPassword(), this.authenticationType);
			IL_3AD:
			if (this.rootdse == null)
			{
				this.rootdse = new DirectoryEntry(this.GetADsPath("RootDSE"), this.GetUsername(), this.GetPassword(), this.authenticationType);
			}
			this.directoryType = this.GetDirectoryType();
			if (this.directoryType == DirectoryType.ADAM && this.connectionProtection == ActiveDirectoryConnectionProtection.SignAndSeal)
			{
				throw new ProviderException(SR.GetString("ADMembership_Ssl_connection_not_established"));
			}
			if (this.directoryType == DirectoryType.AD && (this.port == 3268 || this.port == 3269))
			{
				throw new ProviderException(SR.GetString("ADMembership_GCPortsNotSupported"));
			}
			if (string.IsNullOrEmpty(this.containerDN))
			{
				if (this.directoryType == DirectoryType.AD)
				{
					this.containerDN = (string)this.rootdse.Properties["defaultNamingContext"].Value;
					if (this.containerDN == null)
					{
						throw new ProviderException(SR.GetString("ADMembership_DefContainer_not_specified"));
					}
					string adsPath = this.GetADsPath("<WKGUID=a9d1ca15768811d1aded00c04fd8d5cd," + this.containerDN + ">");
					DirectoryEntry directoryEntry = new DirectoryEntry(adsPath, this.GetUsername(), this.GetPassword(), this.authenticationType);
					try
					{
						this.creationContainerDN = (string)PropertyManager.GetPropertyValue(directoryEntry, "distinguishedName");
						goto IL_586;
					}
					catch (COMException ex6)
					{
						if (ex6.ErrorCode == -2147016656)
						{
							throw new ProviderException(SR.GetString("ADMembership_DefContainer_does_not_exist"));
						}
						throw;
					}
				}
				throw new ProviderException(SR.GetString("ADMembership_Container_must_be_specified"));
			}
			DirectoryEntry directoryEntry2 = new DirectoryEntry(this.GetADsPath(this.containerDN), this.GetUsername(), this.GetPassword(), this.authenticationType);
			try
			{
				this.creationContainerDN = (this.containerDN = (string)PropertyManager.GetPropertyValue(directoryEntry2, "distinguishedName"));
			}
			catch (COMException ex7)
			{
				if (ex7.ErrorCode == -2147016656)
				{
					throw new ProviderException(SR.GetString("ADMembership_Container_does_not_exist"));
				}
				throw;
			}
			IL_586:
			LdapConnection ldapConnection = new LdapConnection(new LdapDirectoryIdentifier(this.serverName + ":" + this.port.ToString()), DirectoryInformation.GetCredentialsWithDomain(credentials), this.ldapAuthType);
			ldapConnection.SessionOptions.ProtocolVersion = 3;
			try
			{
				ldapConnection.SessionOptions.ReferralChasing = ReferralChasingOptions.None;
				this.SetSessionOptionsForSecureConnection(ldapConnection, false);
				ldapConnection.Bind();
				SearchRequest searchRequest = new SearchRequest();
				searchRequest.DistinguishedName = this.containerDN;
				searchRequest.Filter = "(objectClass=*)";
				searchRequest.Scope = System.DirectoryServices.Protocols.SearchScope.Base;
				searchRequest.Attributes.Add("distinguishedName");
				searchRequest.Attributes.Add("objectClass");
				if (this.ServerSearchTimeout != -1)
				{
					searchRequest.TimeLimit = new TimeSpan(0, this.ServerSearchTimeout, 0);
				}
				SearchResponse searchResponse;
				try
				{
					searchResponse = (SearchResponse)ldapConnection.SendRequest(searchRequest);
					if (searchResponse.ResultCode == ResultCode.Referral || searchResponse.ResultCode == ResultCode.NoSuchObject)
					{
						throw new ProviderException(SR.GetString("ADMembership_Container_does_not_exist"));
					}
					if (searchResponse.ResultCode != ResultCode.Success)
					{
						throw new ProviderException(searchResponse.ErrorMessage);
					}
				}
				catch (DirectoryOperationException ex8)
				{
					SearchResponse searchResponse2 = (SearchResponse)ex8.Response;
					if (searchResponse2.ResultCode == ResultCode.NoSuchObject)
					{
						throw new ProviderException(SR.GetString("ADMembership_Container_does_not_exist"));
					}
					throw;
				}
				DirectoryAttribute objectClass = searchResponse.Entries[0].Attributes["objectClass"];
				if (!this.ContainerIsSuperiorOfUser(objectClass))
				{
					throw new ProviderException(SR.GetString("ADMembership_Container_not_superior"));
				}
				if (this.connectionProtection == ActiveDirectoryConnectionProtection.None || this.connectionProtection == ActiveDirectoryConnectionProtection.Ssl)
				{
					this.concurrentBindSupported = this.IsConcurrentBindSupported(ldapConnection);
				}
			}
			finally
			{
				ldapConnection.Dispose();
			}
			if (this.directoryType == DirectoryType.ADAM)
			{
				this.adamPartitionDN = this.GetADAMPartitionFromContainer();
				return;
			}
			if (enablePasswordReset)
			{
				DirectoryEntry directoryEntry3 = new DirectoryEntry(this.GetADsPath((string)PropertyManager.GetPropertyValue(this.rootdse, "defaultNamingContext")), this.GetUsername(), this.GetPassword(), this.AuthenticationTypes);
				NativeComInterfaces.IAdsLargeInteger adsLargeInteger = (NativeComInterfaces.IAdsLargeInteger)PropertyManager.GetPropertyValue(directoryEntry3, "lockoutDuration");
				long num2 = adsLargeInteger.HighPart * 4294967296L + (long)((ulong)((uint)adsLargeInteger.LowPart));
				this.adLockoutDuration = new TimeSpan(-num2);
			}
		}

		// Token: 0x1700161C RID: 5660
		// (get) Token: 0x06004B1B RID: 19227 RVA: 0x000FE5DC File Offset: 0x000FC7DC
		internal bool ConcurrentBindSupported
		{
			get
			{
				return this.concurrentBindSupported;
			}
		}

		// Token: 0x1700161D RID: 5661
		// (get) Token: 0x06004B1C RID: 19228 RVA: 0x000FE5E4 File Offset: 0x000FC7E4
		internal string ContainerDN
		{
			get
			{
				return this.containerDN;
			}
		}

		// Token: 0x1700161E RID: 5662
		// (get) Token: 0x06004B1D RID: 19229 RVA: 0x000FE5EC File Offset: 0x000FC7EC
		internal string CreationContainerDN
		{
			get
			{
				return this.creationContainerDN;
			}
		}

		// Token: 0x1700161F RID: 5663
		// (get) Token: 0x06004B1E RID: 19230 RVA: 0x000FE5F4 File Offset: 0x000FC7F4
		internal int Port
		{
			get
			{
				return this.port;
			}
		}

		// Token: 0x17001620 RID: 5664
		// (get) Token: 0x06004B1F RID: 19231 RVA: 0x000FE5FC File Offset: 0x000FC7FC
		internal bool PortSpecified
		{
			get
			{
				return this.portSpecified;
			}
		}

		// Token: 0x17001621 RID: 5665
		// (get) Token: 0x06004B20 RID: 19232 RVA: 0x000FE604 File Offset: 0x000FC804
		internal DirectoryType DirectoryType
		{
			get
			{
				return this.directoryType;
			}
		}

		// Token: 0x17001622 RID: 5666
		// (get) Token: 0x06004B21 RID: 19233 RVA: 0x000FE60C File Offset: 0x000FC80C
		internal ActiveDirectoryConnectionProtection ConnectionProtection
		{
			get
			{
				return this.connectionProtection;
			}
		}

		// Token: 0x17001623 RID: 5667
		// (get) Token: 0x06004B22 RID: 19234 RVA: 0x000FE614 File Offset: 0x000FC814
		internal AuthenticationTypes AuthenticationTypes
		{
			get
			{
				return this.authenticationType;
			}
		}

		// Token: 0x17001624 RID: 5668
		// (get) Token: 0x06004B23 RID: 19235 RVA: 0x000FE61C File Offset: 0x000FC81C
		internal int ClientSearchTimeout
		{
			get
			{
				return this.clientSearchTimeout;
			}
		}

		// Token: 0x17001625 RID: 5669
		// (get) Token: 0x06004B24 RID: 19236 RVA: 0x000FE624 File Offset: 0x000FC824
		internal int ServerSearchTimeout
		{
			get
			{
				return this.serverSearchTimeout;
			}
		}

		// Token: 0x17001626 RID: 5670
		// (get) Token: 0x06004B25 RID: 19237 RVA: 0x000FE62C File Offset: 0x000FC82C
		internal TimeUnit TimeoutUnit
		{
			get
			{
				return this.timeUnit;
			}
		}

		// Token: 0x17001627 RID: 5671
		// (get) Token: 0x06004B26 RID: 19238 RVA: 0x000FE634 File Offset: 0x000FC834
		internal string ADAMPartitionDN
		{
			get
			{
				return this.adamPartitionDN;
			}
		}

		// Token: 0x17001628 RID: 5672
		// (get) Token: 0x06004B27 RID: 19239 RVA: 0x000FE63C File Offset: 0x000FC83C
		internal TimeSpan ADLockoutDuration
		{
			get
			{
				return this.adLockoutDuration;
			}
		}

		// Token: 0x17001629 RID: 5673
		// (get) Token: 0x06004B28 RID: 19240 RVA: 0x000FE644 File Offset: 0x000FC844
		internal string ForestName
		{
			get
			{
				return this.forestName;
			}
		}

		// Token: 0x1700162A RID: 5674
		// (get) Token: 0x06004B29 RID: 19241 RVA: 0x000FE64C File Offset: 0x000FC84C
		internal string DomainName
		{
			get
			{
				return this.domainName;
			}
		}

		// Token: 0x06004B2A RID: 19242 RVA: 0x000FE654 File Offset: 0x000FC854
		internal void InitializeDomainAndForestName()
		{
			if (!this.isServer)
			{
				DirectoryContext context = new DirectoryContext(DirectoryContextType.Domain, this.serverName, this.GetUsername(), this.GetPassword());
				try
				{
					Domain domain = Domain.GetDomain(context);
					this.domainName = this.GetNetbiosDomainNameIfAvailable(domain.Name);
					this.forestName = domain.Forest.Name;
				}
				catch (ActiveDirectoryObjectNotFoundException)
				{
					this.isServer = true;
				}
			}
			if (this.isServer)
			{
				DirectoryContext context2 = new DirectoryContext(DirectoryContextType.DirectoryServer, this.serverName, this.GetUsername(), this.GetPassword());
				try
				{
					Domain domain2 = Domain.GetDomain(context2);
					this.domainName = this.GetNetbiosDomainNameIfAvailable(domain2.Name);
					this.forestName = domain2.Forest.Name;
				}
				catch (ActiveDirectoryObjectNotFoundException)
				{
					throw new ProviderException(SR.GetString("ADMembership_unable_to_contact_domain"));
				}
			}
		}

		// Token: 0x06004B2B RID: 19243 RVA: 0x000FE734 File Offset: 0x000FC934
		internal void SelectServer()
		{
			this.serverName = this.GetPdcIfDomain(this.serverName);
			this.isServer = true;
		}

		// Token: 0x06004B2C RID: 19244 RVA: 0x000FE750 File Offset: 0x000FC950
		internal LdapConnection CreateNewLdapConnection(AuthType authType)
		{
			LdapConnection ldapConnection = new LdapConnection(new LdapDirectoryIdentifier(this.serverName + ":" + this.port.ToString()));
			ldapConnection.AuthType = authType;
			ldapConnection.SessionOptions.ProtocolVersion = 3;
			this.SetSessionOptionsForSecureConnection(ldapConnection, true);
			return ldapConnection;
		}

		// Token: 0x06004B2D RID: 19245 RVA: 0x000FE7A4 File Offset: 0x000FC9A4
		internal string GetADsPath(string dn)
		{
			string str = "LDAP://" + this.serverName;
			if (this.portSpecified)
			{
				str = str + ":" + this.port.ToString();
			}
			NativeComInterfaces.IAdsPathname adsPathname = (NativeComInterfaces.IAdsPathname)new NativeComInterfaces.Pathname();
			adsPathname.Set(dn, 4);
			adsPathname.EscapedMode = 2;
			return str + "/" + adsPathname.Retrieve(7);
		}

		// Token: 0x06004B2E RID: 19246 RVA: 0x000FE814 File Offset: 0x000FCA14
		internal void SetSessionOptionsForSecureConnection(LdapConnection connection, bool useConcurrentBind)
		{
			if (this.connectionProtection == ActiveDirectoryConnectionProtection.Ssl)
			{
				connection.SessionOptions.SecureSocketLayer = true;
			}
			else if (this.connectionProtection == ActiveDirectoryConnectionProtection.SignAndSeal)
			{
				connection.SessionOptions.Signing = true;
				connection.SessionOptions.Sealing = true;
			}
			if (useConcurrentBind && this.concurrentBindSupported)
			{
				try
				{
					connection.SessionOptions.FastConcurrentBind();
				}
				catch (PlatformNotSupportedException)
				{
					this.concurrentBindSupported = false;
				}
				catch (DirectoryOperationException)
				{
					this.concurrentBindSupported = false;
				}
			}
		}

		// Token: 0x06004B2F RID: 19247 RVA: 0x000FE8A4 File Offset: 0x000FCAA4
		[EnvironmentPermission(SecurityAction.Assert, Read = "USERNAME")]
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
		internal string GetUsername()
		{
			if (this.credentials == null)
			{
				return null;
			}
			if (this.credentials.UserName == null)
			{
				return null;
			}
			if (this.credentials.UserName.Length == 0 && (this.credentials.Password == null || this.credentials.Password.Length == 0))
			{
				return null;
			}
			return this.credentials.UserName;
		}

		// Token: 0x06004B30 RID: 19248 RVA: 0x000FE908 File Offset: 0x000FCB08
		[EnvironmentPermission(SecurityAction.Assert, Read = "USERNAME")]
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
		internal string GetPassword()
		{
			if (this.credentials == null)
			{
				return null;
			}
			if (this.credentials.Password == null)
			{
				return null;
			}
			if (this.credentials.Password.Length == 0 && (this.credentials.UserName == null || this.credentials.UserName.Length == 0))
			{
				return null;
			}
			return this.credentials.Password;
		}

		// Token: 0x06004B31 RID: 19249 RVA: 0x000FE96C File Offset: 0x000FCB6C
		internal AuthenticationTypes GetAuthenticationTypes(ActiveDirectoryConnectionProtection connectionProtection, CredentialsType type)
		{
			return this.authTypes[(int)connectionProtection, (int)type];
		}

		// Token: 0x06004B32 RID: 19250 RVA: 0x000FE97B File Offset: 0x000FCB7B
		internal AuthType GetLdapAuthenticationTypes(ActiveDirectoryConnectionProtection connectionProtection, CredentialsType type)
		{
			return this.ldapAuthTypes[(int)connectionProtection, (int)type];
		}

		// Token: 0x06004B33 RID: 19251 RVA: 0x000FE98C File Offset: 0x000FCB8C
		[EnvironmentPermission(SecurityAction.Assert, Read = "USERNAME")]
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
		internal bool IsDefaultCredential()
		{
			return (this.credentials.UserName == null || this.credentials.UserName.Length == 0) && (this.credentials.Password == null || this.credentials.Password.Length == 0);
		}

		// Token: 0x06004B34 RID: 19252 RVA: 0x000FE9DC File Offset: 0x000FCBDC
		[EnvironmentPermission(SecurityAction.Assert, Read = "USERNAME")]
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
		internal static NetworkCredential GetCredentialsWithDomain(NetworkCredential credentials)
		{
			NetworkCredential result;
			if (credentials == null)
			{
				result = new NetworkCredential(null, "");
			}
			else
			{
				string userName = credentials.UserName;
				string userName2 = null;
				string password = null;
				string domain = null;
				if (!string.IsNullOrEmpty(userName))
				{
					int num = userName.IndexOf('\\');
					if (num != -1)
					{
						domain = userName.Substring(0, num);
						userName2 = userName.Substring(num + 1);
					}
					else
					{
						userName2 = userName;
					}
					password = credentials.Password;
				}
				result = new NetworkCredential(userName2, password, domain);
			}
			return result;
		}

		// Token: 0x06004B35 RID: 19253 RVA: 0x000FEA4C File Offset: 0x000FCC4C
		private bool IsConcurrentBindSupported(LdapConnection ldapConnection)
		{
			bool result = false;
			SearchRequest searchRequest = new SearchRequest();
			searchRequest.Scope = System.DirectoryServices.Protocols.SearchScope.Base;
			searchRequest.Attributes.Add("supportedExtension");
			if (this.ServerSearchTimeout != -1)
			{
				searchRequest.TimeLimit = new TimeSpan(0, this.ServerSearchTimeout, 0);
			}
			SearchResponse searchResponse = (SearchResponse)ldapConnection.SendRequest(searchRequest);
			if (searchResponse.ResultCode != ResultCode.Success)
			{
				throw new ProviderException(searchResponse.ErrorMessage);
			}
			foreach (string s in searchResponse.Entries[0].Attributes["supportedExtension"].GetValues(typeof(string)))
			{
				if (StringUtil.EqualsIgnoreCase(s, "1.2.840.113556.1.4.1781"))
				{
					result = true;
					break;
				}
			}
			return result;
		}

		// Token: 0x06004B36 RID: 19254 RVA: 0x000FEB14 File Offset: 0x000FCD14
		private string GetADAMPartitionFromContainer()
		{
			string text = null;
			int num = int.MaxValue;
			foreach (object obj in this.rootdse.Properties["namingContexts"])
			{
				string text2 = (string)obj;
				bool flag = this.containerDN.EndsWith(text2, StringComparison.Ordinal);
				int num2 = this.containerDN.LastIndexOf(text2, StringComparison.Ordinal);
				if (flag && num2 != -1 && num2 < num)
				{
					text = text2;
					num = num2;
				}
			}
			if (text == null)
			{
				throw new ProviderException(SR.GetString("ADMembership_No_ADAM_Partition"));
			}
			return text;
		}

		// Token: 0x06004B37 RID: 19255 RVA: 0x000FEBC8 File Offset: 0x000FCDC8
		private bool ContainerIsSuperiorOfUser(DirectoryAttribute objectClass)
		{
			ArrayList arrayList = new ArrayList();
			DirectoryEntry directoryEntry = new DirectoryEntry(this.GetADsPath("schema") + "/user", this.GetUsername(), this.GetPassword(), this.AuthenticationTypes);
			ArrayList arrayList2 = new ArrayList();
			bool flag = false;
			object obj = null;
			try
			{
				obj = directoryEntry.InvokeGet("DerivedFrom");
			}
			catch (COMException ex)
			{
				if (ex.ErrorCode != -2147463155)
				{
					throw;
				}
				flag = true;
			}
			if (!flag)
			{
				if (obj is ICollection)
				{
					arrayList2.AddRange((ICollection)obj);
				}
				else
				{
					arrayList2.Add((string)obj);
				}
			}
			arrayList2.Add("user");
			DirectoryEntry searchRoot = new DirectoryEntry(this.GetADsPath((string)this.rootdse.Properties["schemaNamingContext"].Value), this.GetUsername(), this.GetPassword(), this.AuthenticationTypes);
			DirectorySearcher directorySearcher = new DirectorySearcher(searchRoot);
			directorySearcher.Filter = "(&(objectClass=classSchema)(|";
			foreach (object obj2 in arrayList2)
			{
				string str = (string)obj2;
				DirectorySearcher directorySearcher2 = directorySearcher;
				directorySearcher2.Filter = directorySearcher2.Filter + "(ldapDisplayName=" + str + ")";
			}
			DirectorySearcher directorySearcher3 = directorySearcher;
			directorySearcher3.Filter += "))";
			directorySearcher.SearchScope = System.DirectoryServices.SearchScope.OneLevel;
			directorySearcher.PropertiesToLoad.Add("possSuperiors");
			directorySearcher.PropertiesToLoad.Add("systemPossSuperiors");
			SearchResultCollection searchResultCollection = directorySearcher.FindAll();
			try
			{
				foreach (object obj3 in searchResultCollection)
				{
					SearchResult searchResult = (SearchResult)obj3;
					arrayList.AddRange(searchResult.Properties["possSuperiors"]);
					arrayList.AddRange(searchResult.Properties["systemPossSuperiors"]);
				}
			}
			finally
			{
				searchResultCollection.Dispose();
			}
			foreach (string item in objectClass.GetValues(typeof(string)))
			{
				if (arrayList.Contains(item))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004B38 RID: 19256 RVA: 0x000FEE48 File Offset: 0x000FD048
		private DirectoryType GetDirectoryType()
		{
			DirectoryType directoryType = DirectoryType.Unknown;
			foreach (object obj in this.rootdse.Properties["supportedCapabilities"])
			{
				string s = (string)obj;
				if (StringUtil.EqualsIgnoreCase(s, "1.2.840.113556.1.4.1851"))
				{
					directoryType = DirectoryType.ADAM;
					break;
				}
				if (StringUtil.EqualsIgnoreCase(s, "1.2.840.113556.1.4.800"))
				{
					directoryType = DirectoryType.AD;
					break;
				}
			}
			if (directoryType == DirectoryType.Unknown)
			{
				throw new ProviderException(SR.GetString("ADMembership_Valid_Targets"));
			}
			return directoryType;
		}

		// Token: 0x06004B39 RID: 19257 RVA: 0x000FEEE4 File Offset: 0x000FD0E4
		internal string GetPdcIfDomain(string name)
		{
			IntPtr zero = IntPtr.Zero;
			uint flags = 1073741968U;
			string result = null;
			int num = 1355;
			int num2 = NativeMethods.DsGetDcName(null, name, IntPtr.Zero, null, flags, out zero);
			try
			{
				if (num2 == 0)
				{
					DomainControllerInfo domainControllerInfo = new DomainControllerInfo();
					Marshal.PtrToStructure(zero, domainControllerInfo);
					result = domainControllerInfo.DomainControllerName.Substring(2);
				}
				else
				{
					if (num2 != num)
					{
						throw new ProviderException(DirectoryInformation.GetErrorMessage(num2));
					}
					result = name;
				}
			}
			finally
			{
				if (zero != IntPtr.Zero)
				{
					NativeMethods.NetApiBufferFree(zero);
				}
			}
			return result;
		}

		// Token: 0x06004B3A RID: 19258 RVA: 0x000FEF78 File Offset: 0x000FD178
		internal string GetNetbiosDomainNameIfAvailable(string dnsDomainName)
		{
			DirectoryEntry searchRoot = new DirectoryEntry(this.GetADsPath("CN=Partitions," + (string)PropertyManager.GetPropertyValue(this.rootdse, "configurationNamingContext")), this.GetUsername(), this.GetPassword());
			DirectorySearcher directorySearcher = new DirectorySearcher(searchRoot);
			directorySearcher.SearchScope = System.DirectoryServices.SearchScope.OneLevel;
			StringBuilder stringBuilder = new StringBuilder(15);
			stringBuilder.Append("(&(objectCategory=crossRef)(dnsRoot=");
			stringBuilder.Append(dnsDomainName);
			stringBuilder.Append(")(systemFlags:1.2.840.113556.1.4.804:=1)(systemFlags:1.2.840.113556.1.4.804:=2))");
			directorySearcher.Filter = stringBuilder.ToString();
			directorySearcher.PropertiesToLoad.Add("nETBIOSName");
			SearchResult searchResult = directorySearcher.FindOne();
			string result;
			if (searchResult == null || !searchResult.Properties.Contains("nETBIOSName"))
			{
				result = dnsDomainName;
			}
			else
			{
				result = (string)PropertyManager.GetSearchResultPropertyValue(searchResult, "nETBIOSName");
			}
			return result;
		}

		// Token: 0x06004B3B RID: 19259 RVA: 0x000FF048 File Offset: 0x000FD248
		private static string GetErrorMessage(int errorCode)
		{
			uint dwMessageId = (uint)((errorCode & 65535) | 458752 | int.MinValue);
			string result = string.Empty;
			StringBuilder stringBuilder = new StringBuilder(256);
			int num = NativeMethods.FormatMessageW(12800, 0, (int)dwMessageId, 0, stringBuilder, stringBuilder.Capacity + 1, 0);
			if (num != 0)
			{
				result = stringBuilder.ToString(0, num);
			}
			else
			{
				result = SR.GetString("ADMembership_Unknown_Error", new object[]
				{
					string.Format(CultureInfo.InvariantCulture, "{0}", new object[]
					{
						errorCode
					})
				});
			}
			return result;
		}

		// Token: 0x0400285E RID: 10334
		private string serverName;

		// Token: 0x0400285F RID: 10335
		private string containerDN;

		// Token: 0x04002860 RID: 10336
		private string creationContainerDN;

		// Token: 0x04002861 RID: 10337
		private string adspath;

		// Token: 0x04002862 RID: 10338
		private int port = 389;

		// Token: 0x04002863 RID: 10339
		private bool portSpecified;

		// Token: 0x04002864 RID: 10340
		private DirectoryType directoryType = DirectoryType.Unknown;

		// Token: 0x04002865 RID: 10341
		private ActiveDirectoryConnectionProtection connectionProtection;

		// Token: 0x04002866 RID: 10342
		private bool concurrentBindSupported;

		// Token: 0x04002867 RID: 10343
		private int clientSearchTimeout = -1;

		// Token: 0x04002868 RID: 10344
		private int serverSearchTimeout = -1;

		// Token: 0x04002869 RID: 10345
		private TimeUnit timeUnit;

		// Token: 0x0400286A RID: 10346
		private DirectoryEntry rootdse;

		// Token: 0x0400286B RID: 10347
		private NetworkCredential credentials;

		// Token: 0x0400286C RID: 10348
		private AuthenticationTypes authenticationType;

		// Token: 0x0400286D RID: 10349
		private AuthType ldapAuthType = AuthType.Basic;

		// Token: 0x0400286E RID: 10350
		private string adamPartitionDN;

		// Token: 0x0400286F RID: 10351
		private TimeSpan adLockoutDuration;

		// Token: 0x04002870 RID: 10352
		private string forestName;

		// Token: 0x04002871 RID: 10353
		private string domainName;

		// Token: 0x04002872 RID: 10354
		private bool isServer;

		// Token: 0x04002873 RID: 10355
		private const string LDAP_CAP_ACTIVE_DIRECTORY_ADAM_OID = "1.2.840.113556.1.4.1851";

		// Token: 0x04002874 RID: 10356
		private const string LDAP_CAP_ACTIVE_DIRECTORY_OID = "1.2.840.113556.1.4.800";

		// Token: 0x04002875 RID: 10357
		private const string LDAP_SERVER_FAST_BIND_OID = "1.2.840.113556.1.4.1781";

		// Token: 0x04002876 RID: 10358
		internal const int SSL_PORT = 636;

		// Token: 0x04002877 RID: 10359
		private const int GC_PORT = 3268;

		// Token: 0x04002878 RID: 10360
		private const int GC_SSL_PORT = 3269;

		// Token: 0x04002879 RID: 10361
		private const string GUID_USERS_CONTAINER_W = "a9d1ca15768811d1aded00c04fd8d5cd";

		// Token: 0x0400287A RID: 10362
		private AuthenticationTypes[,] authTypes;

		// Token: 0x0400287B RID: 10363
		private AuthType[,] ldapAuthTypes;
	}
}
