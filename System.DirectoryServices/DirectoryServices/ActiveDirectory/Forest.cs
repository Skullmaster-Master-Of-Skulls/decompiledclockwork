using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;

namespace System.DirectoryServices.ActiveDirectory
{
	// Token: 0x020000A7 RID: 167
	[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
	public class Forest : IDisposable
	{
		// Token: 0x06000572 RID: 1394 RVA: 0x0001EEA2 File Offset: 0x0001DEA2
		internal Forest(DirectoryContext context, string forestDnsName, DirectoryEntryManager directoryEntryMgr)
		{
			this.context = context;
			this.directoryEntryMgr = directoryEntryMgr;
			this.forestDnsName = forestDnsName;
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x0001EEDC File Offset: 0x0001DEDC
		internal Forest(DirectoryContext context, string name) : this(context, name, new DirectoryEntryManager(context))
		{
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x0001EEEC File Offset: 0x0001DEEC
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x0001EEF8 File Offset: 0x0001DEF8
		protected void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					foreach (object obj in this.directoryEntryMgr.GetCachedDirectoryEntries())
					{
						DirectoryEntry directoryEntry = (DirectoryEntry)obj;
						directoryEntry.Dispose();
					}
				}
				this.disposed = true;
			}
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x0001EF68 File Offset: 0x0001DF68
		public static Forest GetForest(DirectoryContext context)
		{
			DirectoryEntryManager directoryEntryManager = null;
			string distinguishedName = null;
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (context.ContextType != DirectoryContextType.Forest && context.ContextType != DirectoryContextType.DirectoryServer)
			{
				throw new ArgumentException(Res.GetString("TargetShouldBeServerORForest"), "context");
			}
			if (context.Name == null && !context.isRootDomain())
			{
				throw new ActiveDirectoryObjectNotFoundException(Res.GetString("ContextNotAssociatedWithDomain"), typeof(Forest), null);
			}
			if (context.Name == null || context.isRootDomain() || context.isServer())
			{
				context = new DirectoryContext(context);
				directoryEntryManager = new DirectoryEntryManager(context);
				try
				{
					DirectoryEntry cachedDirectoryEntry = directoryEntryManager.GetCachedDirectoryEntry(WellKnownDN.RootDSE);
					if (context.isServer() && !Utils.CheckCapability(cachedDirectoryEntry, Capability.ActiveDirectory))
					{
						throw new ActiveDirectoryObjectNotFoundException(Res.GetString("DCNotFound", new object[]
						{
							context.Name
						}), typeof(Forest), null);
					}
					distinguishedName = (string)PropertyManager.GetPropertyValue(context, cachedDirectoryEntry, PropertyManager.RootDomainNamingContext);
				}
				catch (COMException ex)
				{
					int errorCode = ex.ErrorCode;
					if (errorCode != -2147016646)
					{
						throw ExceptionHelper.GetExceptionFromCOMException(context, ex);
					}
					if (context.ContextType == DirectoryContextType.Forest)
					{
						throw new ActiveDirectoryObjectNotFoundException(Res.GetString("ForestNotFound"), typeof(Forest), context.Name);
					}
					throw new ActiveDirectoryObjectNotFoundException(Res.GetString("DCNotFound", new object[]
					{
						context.Name
					}), typeof(Forest), null);
				}
				return new Forest(context, Utils.GetDnsNameFromDN(distinguishedName), directoryEntryManager);
			}
			if (context.ContextType == DirectoryContextType.Forest)
			{
				throw new ActiveDirectoryObjectNotFoundException(Res.GetString("ForestNotFound"), typeof(Forest), context.Name);
			}
			throw new ActiveDirectoryObjectNotFoundException(Res.GetString("DCNotFound", new object[]
			{
				context.Name
			}), typeof(Forest), null);
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0001F148 File Offset: 0x0001E148
		public void RaiseForestFunctionality(ForestMode forestMode)
		{
			this.CheckIfDisposed();
			if (forestMode < ForestMode.Windows2000Forest || forestMode > ForestMode.Windows2008R2Forest)
			{
				throw new InvalidEnumArgumentException("forestMode", (int)forestMode, typeof(ForestMode));
			}
			if (forestMode <= this.GetForestMode())
			{
				throw new ArgumentException(Res.GetString("InvalidMode"), "forestMode");
			}
			DirectoryEntry directoryEntry = DirectoryEntryManager.GetDirectoryEntry(this.context, this.directoryEntryMgr.ExpandWellKnownDN(WellKnownDN.PartitionsContainer));
			try
			{
				directoryEntry.Properties[PropertyManager.MsDSBehaviorVersion].Value = (int)forestMode;
				directoryEntry.CommitChanges();
			}
			catch (COMException ex)
			{
				if (ex.ErrorCode == -2147016694)
				{
					throw new ArgumentException(Res.GetString("NoW2K3DCsInForest"), "forestMode");
				}
				throw ExceptionHelper.GetExceptionFromCOMException(this.context, ex);
			}
			finally
			{
				directoryEntry.Dispose();
			}
			this.currentForestMode = (ForestMode)(-1);
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x0001F230 File Offset: 0x0001E230
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0001F238 File Offset: 0x0001E238
		public GlobalCatalog FindGlobalCatalog()
		{
			this.CheckIfDisposed();
			return GlobalCatalog.FindOneInternal(this.context, this.Name, null, (LocatorOptions)0L);
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x0001F254 File Offset: 0x0001E254
		public GlobalCatalog FindGlobalCatalog(string siteName)
		{
			this.CheckIfDisposed();
			if (siteName == null)
			{
				throw new ArgumentNullException("siteName");
			}
			return GlobalCatalog.FindOneInternal(this.context, this.Name, siteName, (LocatorOptions)0L);
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x0001F27E File Offset: 0x0001E27E
		public GlobalCatalog FindGlobalCatalog(LocatorOptions flag)
		{
			this.CheckIfDisposed();
			return GlobalCatalog.FindOneInternal(this.context, this.Name, null, flag);
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0001F299 File Offset: 0x0001E299
		public GlobalCatalog FindGlobalCatalog(string siteName, LocatorOptions flag)
		{
			this.CheckIfDisposed();
			if (siteName == null)
			{
				throw new ArgumentNullException("siteName");
			}
			return GlobalCatalog.FindOneInternal(this.context, this.Name, siteName, flag);
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x0001F2C2 File Offset: 0x0001E2C2
		public GlobalCatalogCollection FindAllGlobalCatalogs()
		{
			this.CheckIfDisposed();
			return GlobalCatalog.FindAllInternal(this.context, null);
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x0001F2D6 File Offset: 0x0001E2D6
		public GlobalCatalogCollection FindAllGlobalCatalogs(string siteName)
		{
			this.CheckIfDisposed();
			if (siteName == null)
			{
				throw new ArgumentNullException("siteName");
			}
			return GlobalCatalog.FindAllInternal(this.context, siteName);
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x0001F2F8 File Offset: 0x0001E2F8
		public GlobalCatalogCollection FindAllDiscoverableGlobalCatalogs()
		{
			long dcFlags = 64L;
			this.CheckIfDisposed();
			return new GlobalCatalogCollection(Locator.EnumerateDomainControllers(this.context, this.Name, null, dcFlags));
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0001F328 File Offset: 0x0001E328
		public GlobalCatalogCollection FindAllDiscoverableGlobalCatalogs(string siteName)
		{
			long dcFlags = 64L;
			this.CheckIfDisposed();
			if (siteName == null)
			{
				throw new ArgumentNullException("siteName");
			}
			if (siteName.Length == 0)
			{
				throw new ArgumentException(Res.GetString("EmptyStringParameter"), "siteName");
			}
			return new GlobalCatalogCollection(Locator.EnumerateDomainControllers(this.context, this.Name, siteName, dcFlags));
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0001F382 File Offset: 0x0001E382
		public TrustRelationshipInformationCollection GetAllTrustRelationships()
		{
			this.CheckIfDisposed();
			return this.GetTrustsHelper(null);
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0001F394 File Offset: 0x0001E394
		public ForestTrustRelationshipInformation GetTrustRelationship(string targetForestName)
		{
			this.CheckIfDisposed();
			if (targetForestName == null)
			{
				throw new ArgumentNullException("targetForestName");
			}
			if (targetForestName.Length == 0)
			{
				throw new ArgumentException(Res.GetString("EmptyStringParameter"), "targetForestName");
			}
			TrustRelationshipInformationCollection trustsHelper = this.GetTrustsHelper(targetForestName);
			if (trustsHelper.Count != 0)
			{
				return (ForestTrustRelationshipInformation)trustsHelper[0];
			}
			throw new ActiveDirectoryObjectNotFoundException(Res.GetString("ForestTrustDoesNotExist", new object[]
			{
				this.Name,
				targetForestName
			}), typeof(TrustRelationshipInformation), null);
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0001F420 File Offset: 0x0001E420
		public bool GetSelectiveAuthenticationStatus(string targetForestName)
		{
			this.CheckIfDisposed();
			if (targetForestName == null)
			{
				throw new ArgumentNullException("targetForestName");
			}
			if (targetForestName.Length == 0)
			{
				throw new ArgumentException(Res.GetString("EmptyStringParameter"), "targetForestName");
			}
			return TrustHelper.GetTrustedDomainInfoStatus(this.context, this.Name, targetForestName, TRUST_ATTRIBUTE.TRUST_ATTRIBUTE_CROSS_ORGANIZATION, true);
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0001F474 File Offset: 0x0001E474
		public void SetSelectiveAuthenticationStatus(string targetForestName, bool enable)
		{
			this.CheckIfDisposed();
			if (targetForestName == null)
			{
				throw new ArgumentNullException("targetForestName");
			}
			if (targetForestName.Length == 0)
			{
				throw new ArgumentException(Res.GetString("EmptyStringParameter"), "targetForestName");
			}
			TrustHelper.SetTrustedDomainInfoStatus(this.context, this.Name, targetForestName, TRUST_ATTRIBUTE.TRUST_ATTRIBUTE_CROSS_ORGANIZATION, enable, true);
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0001F4C8 File Offset: 0x0001E4C8
		public bool GetSidFilteringStatus(string targetForestName)
		{
			this.CheckIfDisposed();
			if (targetForestName == null)
			{
				throw new ArgumentNullException("targetForestName");
			}
			if (targetForestName.Length == 0)
			{
				throw new ArgumentException(Res.GetString("EmptyStringParameter"), "targetForestName");
			}
			return TrustHelper.GetTrustedDomainInfoStatus(this.context, this.Name, targetForestName, TRUST_ATTRIBUTE.TRUST_ATTRIBUTE_TREAT_AS_EXTERNAL, true);
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x0001F51C File Offset: 0x0001E51C
		public void SetSidFilteringStatus(string targetForestName, bool enable)
		{
			this.CheckIfDisposed();
			if (targetForestName == null)
			{
				throw new ArgumentNullException("targetForestName");
			}
			if (targetForestName.Length == 0)
			{
				throw new ArgumentException(Res.GetString("EmptyStringParameter"), "targetForestName");
			}
			TrustHelper.SetTrustedDomainInfoStatus(this.context, this.Name, targetForestName, TRUST_ATTRIBUTE.TRUST_ATTRIBUTE_TREAT_AS_EXTERNAL, enable, true);
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x0001F570 File Offset: 0x0001E570
		public void DeleteLocalSideOfTrustRelationship(string targetForestName)
		{
			this.CheckIfDisposed();
			if (targetForestName == null)
			{
				throw new ArgumentNullException("targetForestName");
			}
			if (targetForestName.Length == 0)
			{
				throw new ArgumentException(Res.GetString("EmptyStringParameter"), "targetForestName");
			}
			TrustHelper.DeleteTrust(this.context, this.Name, targetForestName, true);
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x0001F5C4 File Offset: 0x0001E5C4
		public void DeleteTrustRelationship(Forest targetForest)
		{
			this.CheckIfDisposed();
			if (targetForest == null)
			{
				throw new ArgumentNullException("targetForest");
			}
			TrustHelper.DeleteTrust(targetForest.GetDirectoryContext(), targetForest.Name, this.Name, true);
			TrustHelper.DeleteTrust(this.context, this.Name, targetForest.Name, true);
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x0001F618 File Offset: 0x0001E618
		public void VerifyOutboundTrustRelationship(string targetForestName)
		{
			this.CheckIfDisposed();
			if (targetForestName == null)
			{
				throw new ArgumentNullException("targetForestName");
			}
			if (targetForestName.Length == 0)
			{
				throw new ArgumentException(Res.GetString("EmptyStringParameter"), "targetForestName");
			}
			TrustHelper.VerifyTrust(this.context, this.Name, targetForestName, true, TrustDirection.Outbound, false, null);
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x0001F66C File Offset: 0x0001E66C
		public void VerifyTrustRelationship(Forest targetForest, TrustDirection direction)
		{
			this.CheckIfDisposed();
			if (targetForest == null)
			{
				throw new ArgumentNullException("targetForest");
			}
			if (direction < TrustDirection.Inbound || direction > TrustDirection.Bidirectional)
			{
				throw new InvalidEnumArgumentException("direction", (int)direction, typeof(TrustDirection));
			}
			if ((direction & TrustDirection.Outbound) != (TrustDirection)0)
			{
				try
				{
					TrustHelper.VerifyTrust(this.context, this.Name, targetForest.Name, true, TrustDirection.Outbound, false, null);
				}
				catch (ActiveDirectoryObjectNotFoundException)
				{
					throw new ActiveDirectoryObjectNotFoundException(Res.GetString("WrongTrustDirection", new object[]
					{
						this.Name,
						targetForest.Name,
						direction
					}), typeof(ForestTrustRelationshipInformation), null);
				}
			}
			if ((direction & TrustDirection.Inbound) != (TrustDirection)0)
			{
				try
				{
					TrustHelper.VerifyTrust(targetForest.GetDirectoryContext(), targetForest.Name, this.Name, true, TrustDirection.Outbound, false, null);
				}
				catch (ActiveDirectoryObjectNotFoundException)
				{
					throw new ActiveDirectoryObjectNotFoundException(Res.GetString("WrongTrustDirection", new object[]
					{
						this.Name,
						targetForest.Name,
						direction
					}), typeof(ForestTrustRelationshipInformation), null);
				}
			}
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x0001F78C File Offset: 0x0001E78C
		public void CreateLocalSideOfTrustRelationship(string targetForestName, TrustDirection direction, string trustPassword)
		{
			this.CheckIfDisposed();
			if (targetForestName == null)
			{
				throw new ArgumentNullException("targetForestName");
			}
			if (targetForestName.Length == 0)
			{
				throw new ArgumentException(Res.GetString("EmptyStringParameter"), "targetForestName");
			}
			if (direction < TrustDirection.Inbound || direction > TrustDirection.Bidirectional)
			{
				throw new InvalidEnumArgumentException("direction", (int)direction, typeof(TrustDirection));
			}
			if (trustPassword == null)
			{
				throw new ArgumentNullException("trustPassword");
			}
			if (trustPassword.Length == 0)
			{
				throw new ArgumentException(Res.GetString("EmptyStringParameter"), "trustPassword");
			}
			Locator.GetDomainControllerInfo(null, targetForestName, null, 80L);
			DirectoryContext newDirectoryContext = Utils.GetNewDirectoryContext(targetForestName, DirectoryContextType.Forest, this.context);
			TrustHelper.CreateTrust(this.context, this.Name, newDirectoryContext, targetForestName, true, direction, trustPassword);
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0001F844 File Offset: 0x0001E844
		public void CreateTrustRelationship(Forest targetForest, TrustDirection direction)
		{
			this.CheckIfDisposed();
			if (targetForest == null)
			{
				throw new ArgumentNullException("targetForest");
			}
			if (direction < TrustDirection.Inbound || direction > TrustDirection.Bidirectional)
			{
				throw new InvalidEnumArgumentException("direction", (int)direction, typeof(TrustDirection));
			}
			string password = TrustHelper.CreateTrustPassword();
			TrustHelper.CreateTrust(this.context, this.Name, targetForest.GetDirectoryContext(), targetForest.Name, true, direction, password);
			int num = 0;
			if ((direction & TrustDirection.Inbound) != (TrustDirection)0)
			{
				num |= 2;
			}
			if ((direction & TrustDirection.Outbound) != (TrustDirection)0)
			{
				num |= 1;
			}
			TrustHelper.CreateTrust(targetForest.GetDirectoryContext(), targetForest.Name, this.context, this.Name, true, (TrustDirection)num, password);
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0001F8E0 File Offset: 0x0001E8E0
		public void UpdateLocalSideOfTrustRelationship(string targetForestName, string newTrustPassword)
		{
			this.CheckIfDisposed();
			if (targetForestName == null)
			{
				throw new ArgumentNullException("targetForestName");
			}
			if (targetForestName.Length == 0)
			{
				throw new ArgumentException(Res.GetString("EmptyStringParameter"), "targetForestName");
			}
			if (newTrustPassword == null)
			{
				throw new ArgumentNullException("newTrustPassword");
			}
			if (newTrustPassword.Length == 0)
			{
				throw new ArgumentException(Res.GetString("EmptyStringParameter"), "newTrustPassword");
			}
			TrustHelper.UpdateTrust(this.context, this.Name, targetForestName, newTrustPassword, true);
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x0001F960 File Offset: 0x0001E960
		public void UpdateLocalSideOfTrustRelationship(string targetForestName, TrustDirection newTrustDirection, string newTrustPassword)
		{
			this.CheckIfDisposed();
			if (targetForestName == null)
			{
				throw new ArgumentNullException("targetForestName");
			}
			if (targetForestName.Length == 0)
			{
				throw new ArgumentException(Res.GetString("EmptyStringParameter"), "targetForestName");
			}
			if (newTrustDirection < TrustDirection.Inbound || newTrustDirection > TrustDirection.Bidirectional)
			{
				throw new InvalidEnumArgumentException("newTrustDirection", (int)newTrustDirection, typeof(TrustDirection));
			}
			if (newTrustPassword == null)
			{
				throw new ArgumentNullException("newTrustPassword");
			}
			if (newTrustPassword.Length == 0)
			{
				throw new ArgumentException(Res.GetString("EmptyStringParameter"), "newTrustPassword");
			}
			TrustHelper.UpdateTrustDirection(this.context, this.Name, targetForestName, newTrustPassword, true, newTrustDirection);
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x0001F9FC File Offset: 0x0001E9FC
		public void UpdateTrustRelationship(Forest targetForest, TrustDirection newTrustDirection)
		{
			this.CheckIfDisposed();
			if (targetForest == null)
			{
				throw new ArgumentNullException("targetForest");
			}
			if (newTrustDirection < TrustDirection.Inbound || newTrustDirection > TrustDirection.Bidirectional)
			{
				throw new InvalidEnumArgumentException("newTrustDirection", (int)newTrustDirection, typeof(TrustDirection));
			}
			string password = TrustHelper.CreateTrustPassword();
			TrustHelper.UpdateTrustDirection(this.context, this.Name, targetForest.Name, password, true, newTrustDirection);
			TrustDirection trustDirection = (TrustDirection)0;
			if ((newTrustDirection & TrustDirection.Inbound) != (TrustDirection)0)
			{
				trustDirection |= TrustDirection.Outbound;
			}
			if ((newTrustDirection & TrustDirection.Outbound) != (TrustDirection)0)
			{
				trustDirection |= TrustDirection.Inbound;
			}
			TrustHelper.UpdateTrustDirection(targetForest.GetDirectoryContext(), targetForest.Name, this.Name, password, true, trustDirection);
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x0001FA8C File Offset: 0x0001EA8C
		public void RepairTrustRelationship(Forest targetForest)
		{
			TrustDirection trustDirection = TrustDirection.Bidirectional;
			this.CheckIfDisposed();
			if (targetForest == null)
			{
				throw new ArgumentNullException("targetForest");
			}
			try
			{
				trustDirection = this.GetTrustRelationship(targetForest.Name).TrustDirection;
				if ((trustDirection & TrustDirection.Outbound) != (TrustDirection)0)
				{
					TrustHelper.VerifyTrust(this.context, this.Name, targetForest.Name, true, TrustDirection.Outbound, true, null);
				}
				if ((trustDirection & TrustDirection.Inbound) != (TrustDirection)0)
				{
					TrustHelper.VerifyTrust(targetForest.GetDirectoryContext(), targetForest.Name, this.Name, true, TrustDirection.Outbound, true, null);
				}
			}
			catch (ActiveDirectoryOperationException)
			{
				this.RepairTrustHelper(targetForest, trustDirection);
			}
			catch (UnauthorizedAccessException)
			{
				this.RepairTrustHelper(targetForest, trustDirection);
			}
			catch (ActiveDirectoryObjectNotFoundException)
			{
				throw new ActiveDirectoryObjectNotFoundException(Res.GetString("WrongTrustDirection", new object[]
				{
					this.Name,
					targetForest.Name,
					trustDirection
				}), typeof(ForestTrustRelationshipInformation), null);
			}
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x0001FB80 File Offset: 0x0001EB80
		public static Forest GetCurrentForest()
		{
			return Forest.GetForest(new DirectoryContext(DirectoryContextType.Forest));
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x0001FB8D File Offset: 0x0001EB8D
		public string Name
		{
			get
			{
				this.CheckIfDisposed();
				return this.forestDnsName;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x0001FB9B File Offset: 0x0001EB9B
		public ReadOnlySiteCollection Sites
		{
			get
			{
				this.CheckIfDisposed();
				if (this.cachedSites == null)
				{
					this.cachedSites = new ReadOnlySiteCollection(this.GetSites());
				}
				return this.cachedSites;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x0001FBC2 File Offset: 0x0001EBC2
		public DomainCollection Domains
		{
			get
			{
				this.CheckIfDisposed();
				if (this.cachedDomains == null)
				{
					this.cachedDomains = new DomainCollection(this.GetDomains());
				}
				return this.cachedDomains;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000595 RID: 1429 RVA: 0x0001FBE9 File Offset: 0x0001EBE9
		public GlobalCatalogCollection GlobalCatalogs
		{
			get
			{
				this.CheckIfDisposed();
				if (this.cachedGlobalCatalogs == null)
				{
					this.cachedGlobalCatalogs = this.FindAllGlobalCatalogs();
				}
				return this.cachedGlobalCatalogs;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000596 RID: 1430 RVA: 0x0001FC0B File Offset: 0x0001EC0B
		public ApplicationPartitionCollection ApplicationPartitions
		{
			get
			{
				this.CheckIfDisposed();
				if (this.cachedApplicationPartitions == null)
				{
					this.cachedApplicationPartitions = new ApplicationPartitionCollection(this.GetApplicationPartitions());
				}
				return this.cachedApplicationPartitions;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000597 RID: 1431 RVA: 0x0001FC32 File Offset: 0x0001EC32
		public ForestMode ForestMode
		{
			get
			{
				this.CheckIfDisposed();
				if (this.currentForestMode == (ForestMode)(-1))
				{
					this.currentForestMode = this.GetForestMode();
				}
				return this.currentForestMode;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x0001FC58 File Offset: 0x0001EC58
		public Domain RootDomain
		{
			get
			{
				this.CheckIfDisposed();
				if (this.cachedRootDomain == null)
				{
					DirectoryContext newDirectoryContext = Utils.GetNewDirectoryContext(this.Name, DirectoryContextType.Domain, this.context);
					this.cachedRootDomain = new Domain(newDirectoryContext, this.Name);
				}
				return this.cachedRootDomain;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x0001FCA0 File Offset: 0x0001ECA0
		public ActiveDirectorySchema Schema
		{
			get
			{
				this.CheckIfDisposed();
				if (this.cachedSchema == null)
				{
					try
					{
						this.cachedSchema = new ActiveDirectorySchema(this.context, this.directoryEntryMgr.ExpandWellKnownDN(WellKnownDN.SchemaNamingContext));
					}
					catch (COMException e)
					{
						throw ExceptionHelper.GetExceptionFromCOMException(this.context, e);
					}
				}
				return this.cachedSchema;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x0001FD00 File Offset: 0x0001ED00
		public DomainController SchemaRoleOwner
		{
			get
			{
				this.CheckIfDisposed();
				if (this.cachedSchemaRoleOwner == null)
				{
					this.cachedSchemaRoleOwner = this.GetRoleOwner(ActiveDirectoryRole.SchemaRole);
				}
				return this.cachedSchemaRoleOwner;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x0001FD23 File Offset: 0x0001ED23
		public DomainController NamingRoleOwner
		{
			get
			{
				this.CheckIfDisposed();
				if (this.cachedNamingRoleOwner == null)
				{
					this.cachedNamingRoleOwner = this.GetRoleOwner(ActiveDirectoryRole.NamingRole);
				}
				return this.cachedNamingRoleOwner;
			}
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x0001FD46 File Offset: 0x0001ED46
		internal DirectoryContext GetDirectoryContext()
		{
			return this.context;
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x0001FD50 File Offset: 0x0001ED50
		private ForestMode GetForestMode()
		{
			DirectoryEntry directoryEntry = DirectoryEntryManager.GetDirectoryEntry(this.context, WellKnownDN.RootDSE);
			ForestMode result;
			try
			{
				if (!directoryEntry.Properties.Contains(PropertyManager.ForestFunctionality))
				{
					result = ForestMode.Windows2000Forest;
				}
				else
				{
					result = (ForestMode)int.Parse((string)directoryEntry.Properties[PropertyManager.ForestFunctionality].Value, NumberFormatInfo.InvariantInfo);
				}
			}
			catch (COMException e)
			{
				throw ExceptionHelper.GetExceptionFromCOMException(this.context, e);
			}
			finally
			{
				directoryEntry.Dispose();
			}
			return result;
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x0001FDDC File Offset: 0x0001EDDC
		private DomainController GetRoleOwner(ActiveDirectoryRole role)
		{
			DirectoryEntry directoryEntry = null;
			string text = null;
			try
			{
				switch (role)
				{
				case ActiveDirectoryRole.SchemaRole:
					directoryEntry = DirectoryEntryManager.GetDirectoryEntry(this.context, this.directoryEntryMgr.ExpandWellKnownDN(WellKnownDN.SchemaNamingContext));
					break;
				case ActiveDirectoryRole.NamingRole:
					directoryEntry = DirectoryEntryManager.GetDirectoryEntry(this.context, this.directoryEntryMgr.ExpandWellKnownDN(WellKnownDN.PartitionsContainer));
					break;
				}
				text = Utils.GetDnsHostNameFromNTDSA(this.context, (string)PropertyManager.GetPropertyValue(this.context, directoryEntry, PropertyManager.FsmoRoleOwner));
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
			DirectoryContext newDirectoryContext = Utils.GetNewDirectoryContext(text, DirectoryContextType.DirectoryServer, this.context);
			return new DomainController(newDirectoryContext, text);
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x0001FEA4 File Offset: 0x0001EEA4
		private ArrayList GetSites()
		{
			ArrayList arrayList = new ArrayList();
			IntPtr zero = IntPtr.Zero;
			IntPtr zero2 = IntPtr.Zero;
			IntPtr zero3 = IntPtr.Zero;
			try
			{
				this.GetDSHandle(out zero, out zero2);
				IntPtr procAddress = UnsafeNativeMethods.GetProcAddress(DirectoryContext.ADHandle, "DsListSitesW");
				if (procAddress == (IntPtr)0)
				{
					throw ExceptionHelper.GetExceptionFromErrorCode(Marshal.GetLastWin32Error());
				}
				NativeMethods.DsListSites dsListSites = (NativeMethods.DsListSites)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(NativeMethods.DsListSites));
				int num = dsListSites(zero, out zero3);
				if (num == 0)
				{
					try
					{
						DsNameResult dsNameResult = new DsNameResult();
						Marshal.PtrToStructure(zero3, dsNameResult);
						IntPtr intPtr = dsNameResult.items;
						for (int i = 0; i < dsNameResult.itemCount; i++)
						{
							DsNameResultItem dsNameResultItem = new DsNameResultItem();
							Marshal.PtrToStructure(intPtr, dsNameResultItem);
							if (dsNameResultItem.status == 0)
							{
								string value = Utils.GetDNComponents(dsNameResultItem.name)[0].Value;
								arrayList.Add(new ActiveDirectorySite(this.context, value, true));
							}
							intPtr = Utils.AddToIntPtr(intPtr, Marshal.SizeOf(dsNameResultItem));
						}
						goto IL_16E;
					}
					finally
					{
						if (zero3 != IntPtr.Zero)
						{
							procAddress = UnsafeNativeMethods.GetProcAddress(DirectoryContext.ADHandle, "DsFreeNameResultW");
							if (procAddress == (IntPtr)0)
							{
								throw ExceptionHelper.GetExceptionFromErrorCode(Marshal.GetLastWin32Error());
							}
							UnsafeNativeMethods.DsFreeNameResultW dsFreeNameResultW = (UnsafeNativeMethods.DsFreeNameResultW)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(UnsafeNativeMethods.DsFreeNameResultW));
							dsFreeNameResultW(zero3);
						}
					}
					goto IL_15C;
					IL_16E:
					return arrayList;
				}
				IL_15C:
				throw ExceptionHelper.GetExceptionFromErrorCode(num, this.context.GetServerName());
			}
			finally
			{
				if (zero != (IntPtr)0)
				{
					Utils.FreeDSHandle(zero, DirectoryContext.ADHandle);
				}
				if (zero2 != (IntPtr)0)
				{
					Utils.FreeAuthIdentity(zero2, DirectoryContext.ADHandle);
				}
			}
			return arrayList;
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x0002008C File Offset: 0x0001F08C
		private ArrayList GetApplicationPartitions()
		{
			ArrayList arrayList = new ArrayList();
			DirectoryEntry directoryEntry = DirectoryEntryManager.GetDirectoryEntry(this.context, this.directoryEntryMgr.ExpandWellKnownDN(WellKnownDN.PartitionsContainer));
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
			stringBuilder.Append(")))");
			string filter = stringBuilder.ToString();
			ADSearcher adsearcher = new ADSearcher(directoryEntry, filter, new string[]
			{
				PropertyManager.DnsRoot,
				PropertyManager.NCName
			}, SearchScope.OneLevel);
			SearchResultCollection searchResultCollection = null;
			try
			{
				searchResultCollection = adsearcher.FindAll();
				string value = this.directoryEntryMgr.ExpandWellKnownDN(WellKnownDN.SchemaNamingContext);
				string value2 = this.directoryEntryMgr.ExpandWellKnownDN(WellKnownDN.ConfigurationNamingContext);
				foreach (object obj in searchResultCollection)
				{
					SearchResult res = (SearchResult)obj;
					string text = (string)PropertyManager.GetSearchResultPropertyValue(res, PropertyManager.NCName);
					if (!text.Equals(value) && !text.Equals(value2))
					{
						string name = (string)PropertyManager.GetSearchResultPropertyValue(res, PropertyManager.DnsRoot);
						DirectoryContext newDirectoryContext = Utils.GetNewDirectoryContext(name, DirectoryContextType.ApplicationPartition, this.context);
						arrayList.Add(new ApplicationPartition(newDirectoryContext, text, (string)PropertyManager.GetSearchResultPropertyValue(res, PropertyManager.DnsRoot), ApplicationPartitionType.ADApplicationPartition, new DirectoryEntryManager(newDirectoryContext)));
					}
				}
			}
			catch (COMException e)
			{
				throw ExceptionHelper.GetExceptionFromCOMException(this.context, e);
			}
			finally
			{
				if (searchResultCollection != null)
				{
					searchResultCollection.Dispose();
				}
				directoryEntry.Dispose();
			}
			return arrayList;
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x00020294 File Offset: 0x0001F294
		private ArrayList GetDomains()
		{
			ArrayList arrayList = new ArrayList();
			DirectoryEntry directoryEntry = DirectoryEntryManager.GetDirectoryEntry(this.context, this.directoryEntryMgr.ExpandWellKnownDN(WellKnownDN.PartitionsContainer));
			StringBuilder stringBuilder = new StringBuilder(15);
			stringBuilder.Append("(&(");
			stringBuilder.Append(PropertyManager.ObjectCategory);
			stringBuilder.Append("=crossRef)(");
			stringBuilder.Append(PropertyManager.SystemFlags);
			stringBuilder.Append(":1.2.840.113556.1.4.804:=");
			stringBuilder.Append(1);
			stringBuilder.Append(")(");
			stringBuilder.Append(PropertyManager.SystemFlags);
			stringBuilder.Append(":1.2.840.113556.1.4.804:=");
			stringBuilder.Append(2);
			stringBuilder.Append("))");
			string filter = stringBuilder.ToString();
			ADSearcher adsearcher = new ADSearcher(directoryEntry, filter, new string[]
			{
				PropertyManager.DnsRoot
			}, SearchScope.OneLevel);
			SearchResultCollection searchResultCollection = null;
			try
			{
				searchResultCollection = adsearcher.FindAll();
				foreach (object obj in searchResultCollection)
				{
					SearchResult res = (SearchResult)obj;
					string text = (string)PropertyManager.GetSearchResultPropertyValue(res, PropertyManager.DnsRoot);
					DirectoryContext newDirectoryContext = Utils.GetNewDirectoryContext(text, DirectoryContextType.Domain, this.context);
					arrayList.Add(new Domain(newDirectoryContext, text));
				}
			}
			catch (COMException e)
			{
				throw ExceptionHelper.GetExceptionFromCOMException(this.context, e);
			}
			finally
			{
				if (searchResultCollection != null)
				{
					searchResultCollection.Dispose();
				}
				directoryEntry.Dispose();
			}
			return arrayList;
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x00020430 File Offset: 0x0001F430
		private void GetDSHandle(out IntPtr dsHandle, out IntPtr authIdentity)
		{
			authIdentity = Utils.GetAuthIdentity(this.context, DirectoryContext.ADHandle);
			if (this.context.ContextType == DirectoryContextType.DirectoryServer)
			{
				dsHandle = Utils.GetDSHandle(this.context.GetServerName(), null, authIdentity, DirectoryContext.ADHandle);
				return;
			}
			dsHandle = Utils.GetDSHandle(null, this.context.GetServerName(), authIdentity, DirectoryContext.ADHandle);
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x000204A6 File Offset: 0x0001F4A6
		private void CheckIfDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x000204C4 File Offset: 0x0001F4C4
		private TrustRelationshipInformationCollection GetTrustsHelper(string targetForestName)
		{
			string text = null;
			IntPtr intPtr = (IntPtr)0;
			int num = 0;
			TrustRelationshipInformationCollection trustRelationshipInformationCollection = new TrustRelationshipInformationCollection();
			bool flag = false;
			int num2 = 0;
			text = Utils.GetPolicyServerName(this.context, true, false, this.Name);
			flag = Utils.Impersonate(this.context);
			try
			{
				try
				{
					num2 = UnsafeNativeMethods.DsEnumerateDomainTrustsW(text, 42, out intPtr, out num);
				}
				finally
				{
					if (flag)
					{
						Utils.Revert();
					}
				}
			}
			catch
			{
				throw;
			}
			if (num2 != 0)
			{
				throw ExceptionHelper.GetExceptionFromErrorCode(num2, text);
			}
			TrustRelationshipInformationCollection result;
			try
			{
				if (intPtr != (IntPtr)0 && num != 0)
				{
					IntPtr ptr = (IntPtr)0;
					int i = 0;
					while (i < num)
					{
						ptr = Utils.AddToIntPtr(intPtr, i * Marshal.SizeOf(typeof(DS_DOMAIN_TRUSTS)));
						DS_DOMAIN_TRUSTS ds_DOMAIN_TRUSTS = new DS_DOMAIN_TRUSTS();
						Marshal.PtrToStructure(ptr, ds_DOMAIN_TRUSTS);
						if (targetForestName == null)
						{
							goto IL_12D;
						}
						bool flag2 = false;
						string text2 = null;
						string text3 = null;
						if (ds_DOMAIN_TRUSTS.DnsDomainName != (IntPtr)0)
						{
							text2 = Marshal.PtrToStringUni(ds_DOMAIN_TRUSTS.DnsDomainName);
						}
						if (ds_DOMAIN_TRUSTS.NetbiosDomainName != (IntPtr)0)
						{
							text3 = Marshal.PtrToStringUni(ds_DOMAIN_TRUSTS.NetbiosDomainName);
						}
						if (text2 != null && Utils.Compare(targetForestName, text2) == 0)
						{
							flag2 = true;
						}
						else if (text3 != null && Utils.Compare(targetForestName, text3) == 0)
						{
							flag2 = true;
						}
						if (flag2)
						{
							goto IL_12D;
						}
						IL_170:
						i++;
						continue;
						IL_12D:
						if (ds_DOMAIN_TRUSTS.TrustType == TrustHelper.TRUST_TYPE_UPLEVEL && (ds_DOMAIN_TRUSTS.TrustAttributes & 8) != 0 && (ds_DOMAIN_TRUSTS.Flags & 8) == 0)
						{
							TrustRelationshipInformation info = new ForestTrustRelationshipInformation(this.context, this.Name, ds_DOMAIN_TRUSTS, TrustType.Forest);
							trustRelationshipInformationCollection.Add(info);
							goto IL_170;
						}
						goto IL_170;
					}
				}
				result = trustRelationshipInformationCollection;
			}
			finally
			{
				if (intPtr != (IntPtr)0)
				{
					UnsafeNativeMethods.NetApiBufferFree(intPtr);
				}
			}
			return result;
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x000206B8 File Offset: 0x0001F6B8
		private void RepairTrustHelper(Forest targetForest, TrustDirection direction)
		{
			string password = TrustHelper.CreateTrustPassword();
			string preferredTargetServer = TrustHelper.UpdateTrust(targetForest.GetDirectoryContext(), targetForest.Name, this.Name, password, true);
			string preferredTargetServer2 = TrustHelper.UpdateTrust(this.context, this.Name, targetForest.Name, password, true);
			if ((direction & TrustDirection.Outbound) != (TrustDirection)0)
			{
				try
				{
					TrustHelper.VerifyTrust(this.context, this.Name, targetForest.Name, true, TrustDirection.Outbound, true, preferredTargetServer);
				}
				catch (ActiveDirectoryObjectNotFoundException)
				{
					throw new ActiveDirectoryObjectNotFoundException(Res.GetString("WrongTrustDirection", new object[]
					{
						this.Name,
						targetForest.Name,
						direction
					}), typeof(ForestTrustRelationshipInformation), null);
				}
			}
			if ((direction & TrustDirection.Inbound) != (TrustDirection)0)
			{
				try
				{
					TrustHelper.VerifyTrust(targetForest.GetDirectoryContext(), targetForest.Name, this.Name, true, TrustDirection.Outbound, true, preferredTargetServer2);
				}
				catch (ActiveDirectoryObjectNotFoundException)
				{
					throw new ActiveDirectoryObjectNotFoundException(Res.GetString("WrongTrustDirection", new object[]
					{
						this.Name,
						targetForest.Name,
						direction
					}), typeof(ForestTrustRelationshipInformation), null);
				}
			}
		}

		// Token: 0x04000451 RID: 1105
		private DirectoryContext context;

		// Token: 0x04000452 RID: 1106
		private DirectoryEntryManager directoryEntryMgr;

		// Token: 0x04000453 RID: 1107
		private IntPtr dsHandle = IntPtr.Zero;

		// Token: 0x04000454 RID: 1108
		private IntPtr authIdentity = IntPtr.Zero;

		// Token: 0x04000455 RID: 1109
		private bool disposed;

		// Token: 0x04000456 RID: 1110
		private string forestDnsName;

		// Token: 0x04000457 RID: 1111
		private ReadOnlySiteCollection cachedSites;

		// Token: 0x04000458 RID: 1112
		private DomainCollection cachedDomains;

		// Token: 0x04000459 RID: 1113
		private GlobalCatalogCollection cachedGlobalCatalogs;

		// Token: 0x0400045A RID: 1114
		private ApplicationPartitionCollection cachedApplicationPartitions;

		// Token: 0x0400045B RID: 1115
		private ForestMode currentForestMode = (ForestMode)(-1);

		// Token: 0x0400045C RID: 1116
		private Domain cachedRootDomain;

		// Token: 0x0400045D RID: 1117
		private ActiveDirectorySchema cachedSchema;

		// Token: 0x0400045E RID: 1118
		private DomainController cachedSchemaRoleOwner;

		// Token: 0x0400045F RID: 1119
		private DomainController cachedNamingRoleOwner;
	}
}
