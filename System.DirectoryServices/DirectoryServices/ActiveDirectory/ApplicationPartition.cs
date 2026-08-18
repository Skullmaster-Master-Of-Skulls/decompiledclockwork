using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;

namespace System.DirectoryServices.ActiveDirectory
{
	// Token: 0x0200008A RID: 138
	[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
	public class ApplicationPartition : ActiveDirectoryPartition
	{
		// Token: 0x06000427 RID: 1063 RVA: 0x000166B3 File Offset: 0x000156B3
		public ApplicationPartition(DirectoryContext context, string distinguishedName)
		{
			this.ValidateApplicationPartitionParameters(context, distinguishedName, null, false);
			this.CreateApplicationPartition(distinguishedName, "domainDns");
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x000166DF File Offset: 0x000156DF
		public ApplicationPartition(DirectoryContext context, string distinguishedName, string objectClass)
		{
			this.ValidateApplicationPartitionParameters(context, distinguishedName, objectClass, true);
			this.CreateApplicationPartition(distinguishedName, objectClass);
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00016707 File Offset: 0x00015707
		internal ApplicationPartition(DirectoryContext context, string distinguishedName, string dnsName, ApplicationPartitionType appType, DirectoryEntryManager directoryEntryMgr) : base(context, distinguishedName)
		{
			this.directoryEntryMgr = directoryEntryMgr;
			this.appType = appType;
			this.dnsName = dnsName;
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00016736 File Offset: 0x00015736
		internal ApplicationPartition(DirectoryContext context, string distinguishedName, string dnsName, DirectoryEntryManager directoryEntryMgr) : this(context, distinguishedName, dnsName, ApplicationPartition.GetApplicationPartitionType(context), directoryEntryMgr)
		{
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0001674C File Offset: 0x0001574C
		protected override void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				try
				{
					if (this.crossRefEntry != null)
					{
						this.crossRefEntry.Dispose();
						this.crossRefEntry = null;
					}
					if (this.domainDNSEntry != null)
					{
						this.domainDNSEntry.Dispose();
						this.domainDNSEntry = null;
					}
					this.disposed = true;
				}
				finally
				{
					base.Dispose();
				}
			}
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x000167B8 File Offset: 0x000157B8
		public static ApplicationPartition GetApplicationPartition(DirectoryContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (context.ContextType != DirectoryContextType.ApplicationPartition)
			{
				throw new ArgumentException(Res.GetString("TargetShouldBeAppNCDnsName"), "context");
			}
			if (!context.isNdnc())
			{
				throw new ActiveDirectoryObjectNotFoundException(Res.GetString("NDNCNotFound"), typeof(ApplicationPartition), context.Name);
			}
			context = new DirectoryContext(context);
			string dnfromDnsName = Utils.GetDNFromDnsName(context.Name);
			DirectoryEntryManager directoryEntryManager = new DirectoryEntryManager(context);
			try
			{
				DirectoryEntry cachedDirectoryEntry = directoryEntryManager.GetCachedDirectoryEntry(dnfromDnsName);
				cachedDirectoryEntry.Bind(true);
			}
			catch (COMException ex)
			{
				int errorCode = ex.ErrorCode;
				if (errorCode == -2147016646)
				{
					throw new ActiveDirectoryObjectNotFoundException(Res.GetString("NDNCNotFound"), typeof(ApplicationPartition), context.Name);
				}
				throw ExceptionHelper.GetExceptionFromCOMException(context, ex);
			}
			return new ApplicationPartition(context, dnfromDnsName, context.Name, ApplicationPartitionType.ADApplicationPartition, directoryEntryManager);
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x000168A0 File Offset: 0x000158A0
		public static ApplicationPartition FindByName(DirectoryContext context, string distinguishedName)
		{
			DirectoryEntryManager directoryEntryManager = null;
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (context.Name == null && !context.isRootDomain())
			{
				throw new ArgumentException(Res.GetString("ContextNotAssociatedWithDomain"), "context");
			}
			if (context.Name != null && !context.isRootDomain() && !context.isADAMConfigSet() && !context.isServer())
			{
				throw new ArgumentException(Res.GetString("NotADOrADAM"), "context");
			}
			if (distinguishedName == null)
			{
				throw new ArgumentNullException("distinguishedName");
			}
			if (distinguishedName.Length == 0)
			{
				throw new ArgumentException(Res.GetString("EmptyStringParameter"), "distinguishedName");
			}
			if (!Utils.IsValidDNFormat(distinguishedName))
			{
				throw new ArgumentException(Res.GetString("InvalidDNFormat"), "distinguishedName");
			}
			context = new DirectoryContext(context);
			directoryEntryManager = new DirectoryEntryManager(context);
			DirectoryEntry directoryEntry = null;
			try
			{
				directoryEntry = DirectoryEntryManager.GetDirectoryEntry(context, directoryEntryManager.ExpandWellKnownDN(WellKnownDN.PartitionsContainer));
			}
			catch (COMException e)
			{
				throw ExceptionHelper.GetExceptionFromCOMException(context, e);
			}
			catch (ActiveDirectoryObjectNotFoundException)
			{
				throw new ActiveDirectoryOperationException(Res.GetString("ADAMInstanceNotFoundInConfigSet", new object[]
				{
					context.Name
				}));
			}
			StringBuilder stringBuilder = new StringBuilder(15);
			stringBuilder.Append("(&(");
			stringBuilder.Append(PropertyManager.ObjectCategory);
			stringBuilder.Append("=crossRef)(");
			stringBuilder.Append(PropertyManager.SystemFlags);
			stringBuilder.Append(":1.2.840.113556.1.4.804:=");
			stringBuilder.Append(1);
			stringBuilder.Append(")(!(");
			stringBuilder.Append(PropertyManager.SystemFlags);
			stringBuilder.Append(":1.2.840.113556.1.4.803:=");
			stringBuilder.Append(2);
			stringBuilder.Append("))(");
			stringBuilder.Append(PropertyManager.NCName);
			stringBuilder.Append("=");
			stringBuilder.Append(Utils.GetEscapedFilterValue(distinguishedName));
			stringBuilder.Append("))");
			string filter = stringBuilder.ToString();
			ADSearcher adsearcher = new ADSearcher(directoryEntry, filter, new string[]
			{
				PropertyManager.DnsRoot,
				PropertyManager.NCName
			}, SearchScope.OneLevel, false, false);
			SearchResult searchResult = null;
			try
			{
				searchResult = adsearcher.FindOne();
			}
			catch (COMException ex)
			{
				if (ex.ErrorCode == -2147016656)
				{
					throw new ActiveDirectoryObjectNotFoundException(Res.GetString("AppNCNotFound"), typeof(ApplicationPartition), distinguishedName);
				}
				throw ExceptionHelper.GetExceptionFromCOMException(context, ex);
			}
			finally
			{
				directoryEntry.Dispose();
			}
			if (searchResult == null)
			{
				throw new ActiveDirectoryObjectNotFoundException(Res.GetString("AppNCNotFound"), typeof(ApplicationPartition), distinguishedName);
			}
			string domainName = null;
			try
			{
				domainName = ((searchResult.Properties[PropertyManager.DnsRoot].Count > 0) ? ((string)searchResult.Properties[PropertyManager.DnsRoot][0]) : null);
			}
			catch (COMException e2)
			{
				throw ExceptionHelper.GetExceptionFromCOMException(context, e2);
			}
			ApplicationPartitionType applicationPartitionType = ApplicationPartition.GetApplicationPartitionType(context);
			DirectoryContext context2;
			if (context.ContextType == DirectoryContextType.DirectoryServer)
			{
				bool flag = false;
				DistinguishedName dn = new DistinguishedName(distinguishedName);
				DirectoryEntry directoryEntry2 = DirectoryEntryManager.GetDirectoryEntry(context, WellKnownDN.RootDSE);
				try
				{
					foreach (object obj in directoryEntry2.Properties[PropertyManager.NamingContexts])
					{
						string dn2 = (string)obj;
						DistinguishedName distinguishedName2 = new DistinguishedName(dn2);
						if (distinguishedName2.Equals(dn))
						{
							flag = true;
							break;
						}
					}
				}
				catch (COMException e3)
				{
					throw ExceptionHelper.GetExceptionFromCOMException(context, e3);
				}
				finally
				{
					directoryEntry2.Dispose();
				}
				if (!flag)
				{
					throw new ActiveDirectoryObjectNotFoundException(Res.GetString("AppNCNotFound"), typeof(ApplicationPartition), distinguishedName);
				}
				context2 = context;
			}
			else if (applicationPartitionType == ApplicationPartitionType.ADApplicationPartition)
			{
				DomainControllerInfo domainControllerInfo;
				int num = Locator.DsGetDcNameWrapper(null, domainName, null, 32768L, out domainControllerInfo);
				if (num == 1355)
				{
					throw new ActiveDirectoryObjectNotFoundException(Res.GetString("AppNCNotFound"), typeof(ApplicationPartition), distinguishedName);
				}
				if (num != 0)
				{
					throw ExceptionHelper.GetExceptionFromErrorCode(num);
				}
				string name = domainControllerInfo.DomainControllerName.Substring(2);
				context2 = Utils.GetNewDirectoryContext(name, DirectoryContextType.DirectoryServer, context);
			}
			else
			{
				string name2 = ConfigurationSet.FindOneAdamInstance(context.Name, context, distinguishedName, null).Name;
				context2 = Utils.GetNewDirectoryContext(name2, DirectoryContextType.DirectoryServer, context);
			}
			return new ApplicationPartition(context2, (string)PropertyManager.GetSearchResultPropertyValue(searchResult, PropertyManager.NCName), domainName, applicationPartitionType, directoryEntryManager);
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00016D2C File Offset: 0x00015D2C
		public DirectoryServer FindDirectoryServer()
		{
			base.CheckIfDisposed();
			DirectoryServer result;
			if (this.appType == ApplicationPartitionType.ADApplicationPartition)
			{
				result = this.FindDirectoryServerInternal(null, false);
			}
			else
			{
				if (!this.committed)
				{
					throw new InvalidOperationException(Res.GetString("CannotPerformOperationOnUncommittedObject"));
				}
				result = ConfigurationSet.FindOneAdamInstance(this.context, base.Name, null);
			}
			return result;
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00016D80 File Offset: 0x00015D80
		public DirectoryServer FindDirectoryServer(string siteName)
		{
			base.CheckIfDisposed();
			if (siteName == null)
			{
				throw new ArgumentNullException("siteName");
			}
			DirectoryServer result;
			if (this.appType == ApplicationPartitionType.ADApplicationPartition)
			{
				result = this.FindDirectoryServerInternal(siteName, false);
			}
			else
			{
				if (!this.committed)
				{
					throw new InvalidOperationException(Res.GetString("CannotPerformOperationOnUncommittedObject"));
				}
				result = ConfigurationSet.FindOneAdamInstance(this.context, base.Name, siteName);
			}
			return result;
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00016DE4 File Offset: 0x00015DE4
		public DirectoryServer FindDirectoryServer(bool forceRediscovery)
		{
			base.CheckIfDisposed();
			DirectoryServer result;
			if (this.appType == ApplicationPartitionType.ADApplicationPartition)
			{
				result = this.FindDirectoryServerInternal(null, forceRediscovery);
			}
			else
			{
				if (!this.committed)
				{
					throw new InvalidOperationException(Res.GetString("CannotPerformOperationOnUncommittedObject"));
				}
				result = ConfigurationSet.FindOneAdamInstance(this.context, base.Name, null);
			}
			return result;
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x00016E38 File Offset: 0x00015E38
		public DirectoryServer FindDirectoryServer(string siteName, bool forceRediscovery)
		{
			base.CheckIfDisposed();
			if (siteName == null)
			{
				throw new ArgumentNullException("siteName");
			}
			DirectoryServer result;
			if (this.appType == ApplicationPartitionType.ADApplicationPartition)
			{
				result = this.FindDirectoryServerInternal(siteName, forceRediscovery);
			}
			else
			{
				if (!this.committed)
				{
					throw new InvalidOperationException(Res.GetString("CannotPerformOperationOnUncommittedObject"));
				}
				result = ConfigurationSet.FindOneAdamInstance(this.context, base.Name, siteName);
			}
			return result;
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00016E9C File Offset: 0x00015E9C
		public ReadOnlyDirectoryServerCollection FindAllDirectoryServers()
		{
			base.CheckIfDisposed();
			if (this.appType == ApplicationPartitionType.ADApplicationPartition)
			{
				return this.FindAllDirectoryServersInternal(null);
			}
			if (!this.committed)
			{
				throw new InvalidOperationException(Res.GetString("CannotPerformOperationOnUncommittedObject"));
			}
			ReadOnlyDirectoryServerCollection readOnlyDirectoryServerCollection = new ReadOnlyDirectoryServerCollection();
			readOnlyDirectoryServerCollection.AddRange(ConfigurationSet.FindAdamInstances(this.context, base.Name, null));
			return readOnlyDirectoryServerCollection;
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00016EF8 File Offset: 0x00015EF8
		public ReadOnlyDirectoryServerCollection FindAllDirectoryServers(string siteName)
		{
			base.CheckIfDisposed();
			if (siteName == null)
			{
				throw new ArgumentNullException("siteName");
			}
			if (this.appType == ApplicationPartitionType.ADApplicationPartition)
			{
				return this.FindAllDirectoryServersInternal(siteName);
			}
			if (!this.committed)
			{
				throw new InvalidOperationException(Res.GetString("CannotPerformOperationOnUncommittedObject"));
			}
			ReadOnlyDirectoryServerCollection readOnlyDirectoryServerCollection = new ReadOnlyDirectoryServerCollection();
			readOnlyDirectoryServerCollection.AddRange(ConfigurationSet.FindAdamInstances(this.context, base.Name, siteName));
			return readOnlyDirectoryServerCollection;
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00016F60 File Offset: 0x00015F60
		public ReadOnlyDirectoryServerCollection FindAllDiscoverableDirectoryServers()
		{
			base.CheckIfDisposed();
			if (this.appType == ApplicationPartitionType.ADApplicationPartition)
			{
				return this.FindAllDiscoverableDirectoryServersInternal(null);
			}
			throw new NotSupportedException(Res.GetString("OperationInvalidForADAM"));
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00016F87 File Offset: 0x00015F87
		public ReadOnlyDirectoryServerCollection FindAllDiscoverableDirectoryServers(string siteName)
		{
			base.CheckIfDisposed();
			if (siteName == null)
			{
				throw new ArgumentNullException("siteName");
			}
			if (this.appType == ApplicationPartitionType.ADApplicationPartition)
			{
				return this.FindAllDiscoverableDirectoryServersInternal(siteName);
			}
			throw new NotSupportedException(Res.GetString("OperationInvalidForADAM"));
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00016FBC File Offset: 0x00015FBC
		public void Delete()
		{
			base.CheckIfDisposed();
			if (!this.committed)
			{
				throw new InvalidOperationException(Res.GetString("CannotPerformOperationOnUncommittedObject"));
			}
			DirectoryEntry directoryEntry = DirectoryEntryManager.GetDirectoryEntry(this.context, this.directoryEntryMgr.ExpandWellKnownDN(WellKnownDN.PartitionsContainer));
			try
			{
				this.GetCrossRefEntry();
				directoryEntry.Children.Remove(this.crossRefEntry);
			}
			catch (COMException e)
			{
				throw ExceptionHelper.GetExceptionFromCOMException(this.context, e);
			}
			finally
			{
				directoryEntry.Dispose();
			}
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0001704C File Offset: 0x0001604C
		public void Save()
		{
			base.CheckIfDisposed();
			if (!this.committed)
			{
				bool flag = false;
				if (this.appType == ApplicationPartitionType.ADApplicationPartition)
				{
					try
					{
						this.domainDNSEntry.CommitChanges();
						goto IL_4B;
					}
					catch (COMException ex)
					{
						if (ex.ErrorCode == -2147016663)
						{
							flag = true;
							goto IL_4B;
						}
						throw ExceptionHelper.GetExceptionFromCOMException(this.context, ex);
					}
				}
				flag = true;
				IL_4B:
				if (flag)
				{
					try
					{
						this.InitializeCrossRef(this.partitionName);
						this.crossRefEntry.CommitChanges();
					}
					catch (COMException e)
					{
						throw ExceptionHelper.GetExceptionFromCOMException(this.context, e);
					}
					try
					{
						this.domainDNSEntry.CommitChanges();
					}
					catch (COMException e2)
					{
						DirectoryEntry parent = this.crossRefEntry.Parent;
						try
						{
							parent.Children.Remove(this.crossRefEntry);
						}
						catch (COMException e3)
						{
							throw ExceptionHelper.GetExceptionFromCOMException(e3);
						}
						throw ExceptionHelper.GetExceptionFromCOMException(this.context, e2);
					}
					try
					{
						this.crossRefEntry.RefreshCache();
					}
					catch (COMException e4)
					{
						throw ExceptionHelper.GetExceptionFromCOMException(this.context, e4);
					}
				}
				DirectoryEntry cachedDirectoryEntry = this.directoryEntryMgr.GetCachedDirectoryEntry(WellKnownDN.RootDSE);
				string str = (string)PropertyManager.GetPropertyValue(this.context, cachedDirectoryEntry, PropertyManager.DsServiceName);
				if (this.appType == ApplicationPartitionType.ADApplicationPartition)
				{
					this.GetCrossRefEntry();
				}
				string str2 = (string)PropertyManager.GetPropertyValue(this.context, this.crossRefEntry, PropertyManager.DistinguishedName);
				DirectoryContext newDirectoryContext = Utils.GetNewDirectoryContext(this.GetNamingRoleOwner(), DirectoryContextType.DirectoryServer, this.context);
				DirectoryEntry directoryEntry = DirectoryEntryManager.GetDirectoryEntry(newDirectoryContext, WellKnownDN.RootDSE);
				try
				{
					directoryEntry.Properties[PropertyManager.ReplicateSingleObject].Value = str + ":" + str2;
					directoryEntry.CommitChanges();
				}
				catch (COMException e5)
				{
					throw ExceptionHelper.GetExceptionFromCOMException(this.context, e5);
				}
				finally
				{
					directoryEntry.Dispose();
				}
				this.committed = true;
				if (this.cachedDirectoryServers == null && !this.securityRefDomainModified)
				{
					goto IL_24C;
				}
				if (this.cachedDirectoryServers != null)
				{
					this.crossRefEntry.Properties[PropertyManager.MsDSNCReplicaLocations].AddRange(this.cachedDirectoryServers.GetMultiValuedProperty());
				}
				if (this.securityRefDomainModified)
				{
					this.crossRefEntry.Properties[PropertyManager.MsDSSDReferenceDomain].Value = this.securityRefDomain;
				}
				try
				{
					this.crossRefEntry.CommitChanges();
					goto IL_24C;
				}
				catch (COMException e6)
				{
					throw ExceptionHelper.GetExceptionFromCOMException(this.context, e6);
				}
			}
			if (this.cachedDirectoryServers == null)
			{
				if (!this.securityRefDomainModified)
				{
					goto IL_24C;
				}
			}
			try
			{
				this.crossRefEntry.CommitChanges();
			}
			catch (COMException e7)
			{
				throw ExceptionHelper.GetExceptionFromCOMException(this.context, e7);
			}
			IL_24C:
			this.cachedDirectoryServers = null;
			this.securityRefDomainModified = false;
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00017324 File Offset: 0x00016324
		[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		public override DirectoryEntry GetDirectoryEntry()
		{
			base.CheckIfDisposed();
			if (!this.committed)
			{
				throw new InvalidOperationException(Res.GetString("CannotGetObject"));
			}
			return DirectoryEntryManager.GetDirectoryEntry(this.context, base.Name);
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x00017358 File Offset: 0x00016358
		public DirectoryServerCollection DirectoryServers
		{
			get
			{
				base.CheckIfDisposed();
				if (this.cachedDirectoryServers == null)
				{
					ReadOnlyDirectoryServerCollection servers = this.committed ? this.FindAllDirectoryServers() : new ReadOnlyDirectoryServerCollection();
					bool isADAM = this.appType == ApplicationPartitionType.ADAMApplicationPartition;
					if (this.committed)
					{
						this.GetCrossRefEntry();
					}
					this.cachedDirectoryServers = new DirectoryServerCollection(this.context, this.committed ? this.crossRefEntry : null, isADAM, servers);
				}
				return this.cachedDirectoryServers;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x000173D0 File Offset: 0x000163D0
		// (set) Token: 0x0600043B RID: 1083 RVA: 0x00017474 File Offset: 0x00016474
		public string SecurityReferenceDomain
		{
			get
			{
				base.CheckIfDisposed();
				if (this.appType == ApplicationPartitionType.ADAMApplicationPartition)
				{
					throw new NotSupportedException(Res.GetString("PropertyInvalidForADAM"));
				}
				if (this.committed)
				{
					this.GetCrossRefEntry();
					try
					{
						if (this.crossRefEntry.Properties[PropertyManager.MsDSSDReferenceDomain].Count > 0)
						{
							return (string)this.crossRefEntry.Properties[PropertyManager.MsDSSDReferenceDomain].Value;
						}
						return null;
					}
					catch (COMException e)
					{
						throw ExceptionHelper.GetExceptionFromCOMException(this.context, e);
					}
				}
				return this.securityRefDomain;
			}
			set
			{
				base.CheckIfDisposed();
				if (this.appType == ApplicationPartitionType.ADAMApplicationPartition)
				{
					throw new NotSupportedException(Res.GetString("PropertyInvalidForADAM"));
				}
				if (this.committed)
				{
					this.GetCrossRefEntry();
					if (value != null)
					{
						this.crossRefEntry.Properties[PropertyManager.MsDSSDReferenceDomain].Value = value;
						this.securityRefDomainModified = true;
						return;
					}
					if (this.crossRefEntry.Properties.Contains(PropertyManager.MsDSSDReferenceDomain))
					{
						this.crossRefEntry.Properties[PropertyManager.MsDSSDReferenceDomain].Clear();
						this.securityRefDomainModified = true;
						return;
					}
				}
				else if (this.securityRefDomain != null || value != null)
				{
					this.securityRefDomain = value;
					this.securityRefDomainModified = true;
				}
			}
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00017528 File Offset: 0x00016528
		private void ValidateApplicationPartitionParameters(DirectoryContext context, string distinguishedName, string objectClass, bool objectClassSpecified)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (context.Name == null || !context.isServer())
			{
				throw new ArgumentException(Res.GetString("TargetShouldBeServer"), "context");
			}
			if (distinguishedName == null)
			{
				throw new ArgumentNullException("distinguishedName");
			}
			if (distinguishedName.Length == 0)
			{
				throw new ArgumentException(Res.GetString("EmptyStringParameter"), "distinguishedName");
			}
			this.context = new DirectoryContext(context);
			this.directoryEntryMgr = new DirectoryEntryManager(this.context);
			this.dnsName = Utils.GetDnsNameFromDN(distinguishedName);
			this.partitionName = distinguishedName;
			Component[] dncomponents = Utils.GetDNComponents(distinguishedName);
			if (dncomponents.Length == 1)
			{
				throw new NotSupportedException(Res.GetString("OneLevelPartitionNotSupported"));
			}
			this.appType = ApplicationPartition.GetApplicationPartitionType(this.context);
			if (this.appType == ApplicationPartitionType.ADApplicationPartition && objectClassSpecified)
			{
				throw new InvalidOperationException(Res.GetString("NoObjectClassForADPartition"));
			}
			if (objectClassSpecified)
			{
				if (objectClass == null)
				{
					throw new ArgumentNullException("objectClass");
				}
				if (objectClass.Length == 0)
				{
					throw new ArgumentException(Res.GetString("EmptyStringParameter"), "objectClass");
				}
			}
			if (this.appType == ApplicationPartitionType.ADApplicationPartition)
			{
				string name = null;
				try
				{
					DirectoryEntry cachedDirectoryEntry = this.directoryEntryMgr.GetCachedDirectoryEntry(WellKnownDN.RootDSE);
					name = (string)PropertyManager.GetPropertyValue(this.context, cachedDirectoryEntry, PropertyManager.DnsHostName);
				}
				catch (COMException e)
				{
					ExceptionHelper.GetExceptionFromCOMException(this.context, e);
				}
				this.context = Utils.GetNewDirectoryContext(name, DirectoryContextType.DirectoryServer, context);
			}
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0001769C File Offset: 0x0001669C
		[DirectoryServicesPermission(SecurityAction.Assert, Unrestricted = true)]
		private void CreateApplicationPartition(string distinguishedName, string objectClass)
		{
			if (this.appType == ApplicationPartitionType.ADApplicationPartition)
			{
				DirectoryEntry directoryEntry = null;
				DirectoryEntry directoryEntry2 = null;
				try
				{
					try
					{
						AuthenticationTypes authenticationTypes = Utils.DefaultAuthType | AuthenticationTypes.FastBind | AuthenticationTypes.Delegation;
						if (DirectoryContext.ServerBindSupported)
						{
							authenticationTypes |= AuthenticationTypes.ServerBind;
						}
						directoryEntry = new DirectoryEntry("LDAP://" + this.context.GetServerName() + "/" + distinguishedName, this.context.UserName, this.context.Password, authenticationTypes);
						directoryEntry2 = directoryEntry.Parent;
						this.domainDNSEntry = directoryEntry2.Children.Add(Utils.GetRdnFromDN(distinguishedName), PropertyManager.DomainDNS);
						this.domainDNSEntry.Properties[PropertyManager.InstanceType].Value = (NCFlags)5;
						this.committed = false;
					}
					catch (COMException e)
					{
						throw ExceptionHelper.GetExceptionFromCOMException(this.context, e);
					}
					return;
				}
				finally
				{
					if (directoryEntry2 != null)
					{
						directoryEntry2.Dispose();
					}
					if (directoryEntry != null)
					{
						directoryEntry.Dispose();
					}
				}
			}
			try
			{
				this.InitializeCrossRef(distinguishedName);
				DirectoryEntry directoryEntry3 = null;
				DirectoryEntry directoryEntry4 = null;
				try
				{
					AuthenticationTypes authenticationTypes2 = Utils.DefaultAuthType | AuthenticationTypes.FastBind;
					if (DirectoryContext.ServerBindSupported)
					{
						authenticationTypes2 |= AuthenticationTypes.ServerBind;
					}
					directoryEntry3 = new DirectoryEntry("LDAP://" + this.context.Name + "/" + distinguishedName, this.context.UserName, this.context.Password, authenticationTypes2);
					directoryEntry4 = directoryEntry3.Parent;
					this.domainDNSEntry = directoryEntry4.Children.Add(Utils.GetRdnFromDN(distinguishedName), objectClass);
					this.domainDNSEntry.Properties[PropertyManager.InstanceType].Value = (NCFlags)5;
					this.committed = false;
				}
				finally
				{
					if (directoryEntry4 != null)
					{
						directoryEntry4.Dispose();
					}
					if (directoryEntry3 != null)
					{
						directoryEntry3.Dispose();
					}
				}
			}
			catch (COMException e2)
			{
				throw ExceptionHelper.GetExceptionFromCOMException(this.context, e2);
			}
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0001788C File Offset: 0x0001688C
		private void InitializeCrossRef(string distinguishedName)
		{
			if (this.crossRefEntry != null)
			{
				return;
			}
			DirectoryEntry directoryEntry = null;
			try
			{
				string namingRoleOwner = this.GetNamingRoleOwner();
				DirectoryContext newDirectoryContext = Utils.GetNewDirectoryContext(namingRoleOwner, DirectoryContextType.DirectoryServer, this.context);
				directoryEntry = DirectoryEntryManager.GetDirectoryEntry(newDirectoryContext, WellKnownDN.PartitionsContainer);
				string name = "CN={" + Guid.NewGuid() + "}";
				this.crossRefEntry = directoryEntry.Children.Add(name, "crossRef");
				string value;
				if (this.appType == ApplicationPartitionType.ADAMApplicationPartition)
				{
					DirectoryEntry cachedDirectoryEntry = this.directoryEntryMgr.GetCachedDirectoryEntry(WellKnownDN.RootDSE);
					string dn = (string)PropertyManager.GetPropertyValue(this.context, cachedDirectoryEntry, PropertyManager.DsServiceName);
					value = Utils.GetAdamHostNameAndPortsFromNTDSA(this.context, dn);
				}
				else
				{
					value = this.context.Name;
				}
				this.crossRefEntry.Properties[PropertyManager.DnsRoot].Value = value;
				this.crossRefEntry.Properties[PropertyManager.Enabled].Value = false;
				this.crossRefEntry.Properties[PropertyManager.NCName].Value = distinguishedName;
			}
			catch (COMException e)
			{
				throw ExceptionHelper.GetExceptionFromCOMException(this.context, e);
			}
			finally
			{
				if (directoryEntry != null)
				{
					directoryEntry.Dispose();
				}
			}
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x000179EC File Offset: 0x000169EC
		private static ApplicationPartitionType GetApplicationPartitionType(DirectoryContext context)
		{
			ApplicationPartitionType applicationPartitionType = ApplicationPartitionType.Unknown;
			DirectoryEntry directoryEntry = DirectoryEntryManager.GetDirectoryEntry(context, WellKnownDN.RootDSE);
			try
			{
				foreach (object obj in directoryEntry.Properties[PropertyManager.SupportedCapabilities])
				{
					string strA = (string)obj;
					if (string.Compare(strA, SupportedCapability.ADOid, StringComparison.OrdinalIgnoreCase) == 0)
					{
						applicationPartitionType = ApplicationPartitionType.ADApplicationPartition;
					}
					if (string.Compare(strA, SupportedCapability.ADAMOid, StringComparison.OrdinalIgnoreCase) == 0)
					{
						applicationPartitionType = ApplicationPartitionType.ADAMApplicationPartition;
					}
				}
			}
			catch (COMException e)
			{
				throw ExceptionHelper.GetExceptionFromCOMException(context, e);
			}
			finally
			{
				directoryEntry.Dispose();
			}
			if (applicationPartitionType == ApplicationPartitionType.Unknown)
			{
				throw new ActiveDirectoryOperationException(Res.GetString("ApplicationPartitionTypeUnknown"));
			}
			return applicationPartitionType;
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00017ABC File Offset: 0x00016ABC
		internal DirectoryEntry GetCrossRefEntry()
		{
			if (this.crossRefEntry != null)
			{
				return this.crossRefEntry;
			}
			DirectoryEntry directoryEntry = DirectoryEntryManager.GetDirectoryEntry(this.context, this.directoryEntryMgr.ExpandWellKnownDN(WellKnownDN.PartitionsContainer));
			try
			{
				this.crossRefEntry = Utils.GetCrossRefEntry(this.context, directoryEntry, base.Name);
			}
			finally
			{
				directoryEntry.Dispose();
			}
			return this.crossRefEntry;
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00017B28 File Offset: 0x00016B28
		internal string GetNamingRoleOwner()
		{
			string result = null;
			DirectoryEntry directoryEntry = DirectoryEntryManager.GetDirectoryEntry(this.context, this.directoryEntryMgr.ExpandWellKnownDN(WellKnownDN.PartitionsContainer));
			try
			{
				if (this.appType == ApplicationPartitionType.ADApplicationPartition)
				{
					result = Utils.GetDnsHostNameFromNTDSA(this.context, (string)PropertyManager.GetPropertyValue(this.context, directoryEntry, PropertyManager.FsmoRoleOwner));
				}
				else
				{
					result = Utils.GetAdamDnsHostNameFromNTDSA(this.context, (string)PropertyManager.GetPropertyValue(this.context, directoryEntry, PropertyManager.FsmoRoleOwner));
				}
			}
			finally
			{
				directoryEntry.Dispose();
			}
			return result;
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00017BB8 File Offset: 0x00016BB8
		private DirectoryServer FindDirectoryServerInternal(string siteName, bool forceRediscovery)
		{
			LocatorOptions locatorOptions = (LocatorOptions)0L;
			if (siteName != null && siteName.Length == 0)
			{
				throw new ArgumentException(Res.GetString("EmptyStringParameter"), "siteName");
			}
			if (!this.committed)
			{
				throw new InvalidOperationException(Res.GetString("CannotPerformOperationOnUncommittedObject"));
			}
			if (forceRediscovery)
			{
				locatorOptions = LocatorOptions.ForceRediscovery;
			}
			DomainControllerInfo domainControllerInfo;
			int num = Locator.DsGetDcNameWrapper(null, this.dnsName, siteName, (long)(locatorOptions | (LocatorOptions)32768L), out domainControllerInfo);
			if (num == 1355)
			{
				throw new ActiveDirectoryObjectNotFoundException(Res.GetString("ReplicaNotFound"), typeof(DirectoryServer), null);
			}
			if (num != 0)
			{
				throw ExceptionHelper.GetExceptionFromErrorCode(num);
			}
			string text = domainControllerInfo.DomainControllerName.Substring(2);
			DirectoryContext newDirectoryContext = Utils.GetNewDirectoryContext(text, DirectoryContextType.DirectoryServer, this.context);
			return new DomainController(newDirectoryContext, text);
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00017C78 File Offset: 0x00016C78
		private ReadOnlyDirectoryServerCollection FindAllDirectoryServersInternal(string siteName)
		{
			if (siteName != null && siteName.Length == 0)
			{
				throw new ArgumentException(Res.GetString("EmptyStringParameter"), "siteName");
			}
			if (!this.committed)
			{
				throw new InvalidOperationException(Res.GetString("CannotPerformOperationOnUncommittedObject"));
			}
			ArrayList arrayList = new ArrayList();
			foreach (object obj in Utils.GetReplicaList(this.context, base.Name, siteName, false, false, false))
			{
				string text = (string)obj;
				DirectoryContext newDirectoryContext = Utils.GetNewDirectoryContext(text, DirectoryContextType.DirectoryServer, this.context);
				arrayList.Add(new DomainController(newDirectoryContext, text));
			}
			return new ReadOnlyDirectoryServerCollection(arrayList);
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00017D3C File Offset: 0x00016D3C
		private ReadOnlyDirectoryServerCollection FindAllDiscoverableDirectoryServersInternal(string siteName)
		{
			if (siteName != null && siteName.Length == 0)
			{
				throw new ArgumentException(Res.GetString("EmptyStringParameter"), "siteName");
			}
			if (!this.committed)
			{
				throw new InvalidOperationException(Res.GetString("CannotPerformOperationOnUncommittedObject"));
			}
			long dcFlags = 32768L;
			return new ReadOnlyDirectoryServerCollection(Locator.EnumerateDomainControllers(this.context, this.dnsName, siteName, dcFlags));
		}

		// Token: 0x040003BF RID: 959
		private bool disposed;

		// Token: 0x040003C0 RID: 960
		private ApplicationPartitionType appType = ApplicationPartitionType.Unknown;

		// Token: 0x040003C1 RID: 961
		private bool committed = true;

		// Token: 0x040003C2 RID: 962
		private DirectoryEntry domainDNSEntry;

		// Token: 0x040003C3 RID: 963
		private DirectoryEntry crossRefEntry;

		// Token: 0x040003C4 RID: 964
		private string dnsName;

		// Token: 0x040003C5 RID: 965
		private DirectoryServerCollection cachedDirectoryServers;

		// Token: 0x040003C6 RID: 966
		private bool securityRefDomainModified;

		// Token: 0x040003C7 RID: 967
		private string securityRefDomain;
	}
}
