using System;
using System.Collections;
using System.Deployment.Internal.Isolation;
using System.Deployment.Internal.Isolation.Manifest;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;

namespace System.Security.Policy
{
	// Token: 0x0200049B RID: 1179
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.ControlPolicy)]
	public sealed class ApplicationTrustCollection : ICollection, IEnumerable
	{
		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x06002EB4 RID: 11956 RVA: 0x0009DAC3 File Offset: 0x0009CAC3
		private static StoreApplicationReference InstallReference
		{
			get
			{
				if (ApplicationTrustCollection.s_installReference == null)
				{
					Interlocked.CompareExchange(ref ApplicationTrustCollection.s_installReference, new StoreApplicationReference(IsolationInterop.GUID_SXS_INSTALL_REFERENCE_SCHEME_OPAQUESTRING, "{60051b8f-4f12-400a-8e50-dd05ebd438d1}", null), null);
				}
				return (StoreApplicationReference)ApplicationTrustCollection.s_installReference;
			}
		}

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x06002EB5 RID: 11957 RVA: 0x0009DAF8 File Offset: 0x0009CAF8
		private ArrayList AppTrusts
		{
			get
			{
				if (this.m_appTrusts == null)
				{
					ArrayList arrayList = new ArrayList();
					if (this.m_storeBounded)
					{
						this.RefreshStorePointer();
						StoreDeploymentMetadataEnumeration storeDeploymentMetadataEnumeration = this.m_pStore.EnumInstallerDeployments(IsolationInterop.GUID_SXS_INSTALL_REFERENCE_SCHEME_OPAQUESTRING, "{60051b8f-4f12-400a-8e50-dd05ebd438d1}", "ApplicationTrust", null);
						foreach (object obj in storeDeploymentMetadataEnumeration)
						{
							IDefinitionAppId deployment = (IDefinitionAppId)obj;
							StoreDeploymentMetadataPropertyEnumeration storeDeploymentMetadataPropertyEnumeration = this.m_pStore.EnumInstallerDeploymentProperties(IsolationInterop.GUID_SXS_INSTALL_REFERENCE_SCHEME_OPAQUESTRING, "{60051b8f-4f12-400a-8e50-dd05ebd438d1}", "ApplicationTrust", deployment);
							foreach (object obj2 in storeDeploymentMetadataPropertyEnumeration)
							{
								string value = ((StoreOperationMetadataProperty)obj2).Value;
								if (value != null && value.Length > 0)
								{
									SecurityElement element = SecurityElement.FromString(value);
									ApplicationTrust applicationTrust = new ApplicationTrust();
									applicationTrust.FromXml(element);
									arrayList.Add(applicationTrust);
								}
							}
						}
					}
					Interlocked.CompareExchange(ref this.m_appTrusts, arrayList, null);
				}
				return this.m_appTrusts as ArrayList;
			}
		}

		// Token: 0x06002EB6 RID: 11958 RVA: 0x0009DC44 File Offset: 0x0009CC44
		internal ApplicationTrustCollection() : this(false)
		{
		}

		// Token: 0x06002EB7 RID: 11959 RVA: 0x0009DC4D File Offset: 0x0009CC4D
		internal ApplicationTrustCollection(bool storeBounded)
		{
			this.m_storeBounded = storeBounded;
		}

		// Token: 0x06002EB8 RID: 11960 RVA: 0x0009DC67 File Offset: 0x0009CC67
		private void RefreshStorePointer()
		{
			if (this.m_pStore != null)
			{
				Marshal.ReleaseComObject(this.m_pStore.InternalStore);
			}
			this.m_pStore = IsolationInterop.GetUserStore();
		}

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x06002EB9 RID: 11961 RVA: 0x0009DC8D File Offset: 0x0009CC8D
		public int Count
		{
			get
			{
				return this.AppTrusts.Count;
			}
		}

		// Token: 0x17000849 RID: 2121
		public ApplicationTrust this[int index]
		{
			get
			{
				return this.AppTrusts[index] as ApplicationTrust;
			}
		}

		// Token: 0x1700084A RID: 2122
		public ApplicationTrust this[string appFullName]
		{
			get
			{
				ApplicationIdentity applicationIdentity = new ApplicationIdentity(appFullName);
				ApplicationTrustCollection applicationTrustCollection = this.Find(applicationIdentity, ApplicationVersionMatch.MatchExactVersion);
				if (applicationTrustCollection.Count > 0)
				{
					return applicationTrustCollection[0];
				}
				return null;
			}
		}

		// Token: 0x06002EBC RID: 11964 RVA: 0x0009DCE0 File Offset: 0x0009CCE0
		private void CommitApplicationTrust(ApplicationIdentity applicationIdentity, string trustXml)
		{
			StoreOperationMetadataProperty[] setProperties = new StoreOperationMetadataProperty[]
			{
				new StoreOperationMetadataProperty(ApplicationTrustCollection.ClrPropertySet, "ApplicationTrust", trustXml)
			};
			IEnumDefinitionIdentity enumDefinitionIdentity = applicationIdentity.Identity.EnumAppPath();
			IDefinitionIdentity[] array = new IDefinitionIdentity[1];
			IDefinitionIdentity definitionIdentity = null;
			if (enumDefinitionIdentity.Next(1U, array) == 1U)
			{
				definitionIdentity = array[0];
			}
			IDefinitionAppId definitionAppId = IsolationInterop.AppIdAuthority.CreateDefinition();
			definitionAppId.SetAppPath(1U, new IDefinitionIdentity[]
			{
				definitionIdentity
			});
			definitionAppId.put_Codebase(applicationIdentity.CodeBase);
			using (StoreTransaction storeTransaction = new StoreTransaction())
			{
				storeTransaction.Add(new StoreOperationSetDeploymentMetadata(definitionAppId, ApplicationTrustCollection.InstallReference, setProperties));
				this.RefreshStorePointer();
				this.m_pStore.Transact(storeTransaction.Operations);
			}
			this.m_appTrusts = null;
		}

		// Token: 0x06002EBD RID: 11965 RVA: 0x0009DDC4 File Offset: 0x0009CDC4
		public int Add(ApplicationTrust trust)
		{
			if (trust == null)
			{
				throw new ArgumentNullException("trust");
			}
			if (trust.ApplicationIdentity == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_ApplicationTrustShouldHaveIdentity"));
			}
			if (this.m_storeBounded)
			{
				this.CommitApplicationTrust(trust.ApplicationIdentity, trust.ToXml().ToString());
				return -1;
			}
			return this.AppTrusts.Add(trust);
		}

		// Token: 0x06002EBE RID: 11966 RVA: 0x0009DE24 File Offset: 0x0009CE24
		public void AddRange(ApplicationTrust[] trusts)
		{
			if (trusts == null)
			{
				throw new ArgumentNullException("trusts");
			}
			int i = 0;
			try
			{
				while (i < trusts.Length)
				{
					this.Add(trusts[i]);
					i++;
				}
			}
			catch
			{
				for (int j = 0; j < i; j++)
				{
					this.Remove(trusts[j]);
				}
				throw;
			}
		}

		// Token: 0x06002EBF RID: 11967 RVA: 0x0009DE84 File Offset: 0x0009CE84
		public void AddRange(ApplicationTrustCollection trusts)
		{
			if (trusts == null)
			{
				throw new ArgumentNullException("trusts");
			}
			int num = 0;
			try
			{
				foreach (ApplicationTrust trust in trusts)
				{
					this.Add(trust);
					num++;
				}
			}
			catch
			{
				for (int i = 0; i < num; i++)
				{
					this.Remove(trusts[i]);
				}
				throw;
			}
		}

		// Token: 0x06002EC0 RID: 11968 RVA: 0x0009DEF4 File Offset: 0x0009CEF4
		public ApplicationTrustCollection Find(ApplicationIdentity applicationIdentity, ApplicationVersionMatch versionMatch)
		{
			ApplicationTrustCollection applicationTrustCollection = new ApplicationTrustCollection(false);
			foreach (ApplicationTrust applicationTrust in this)
			{
				if (CmsUtils.CompareIdentities(applicationTrust.ApplicationIdentity, applicationIdentity, versionMatch))
				{
					applicationTrustCollection.Add(applicationTrust);
				}
			}
			return applicationTrustCollection;
		}

		// Token: 0x06002EC1 RID: 11969 RVA: 0x0009DF38 File Offset: 0x0009CF38
		public void Remove(ApplicationIdentity applicationIdentity, ApplicationVersionMatch versionMatch)
		{
			ApplicationTrustCollection trusts = this.Find(applicationIdentity, versionMatch);
			this.RemoveRange(trusts);
		}

		// Token: 0x06002EC2 RID: 11970 RVA: 0x0009DF58 File Offset: 0x0009CF58
		public void Remove(ApplicationTrust trust)
		{
			if (trust == null)
			{
				throw new ArgumentNullException("trust");
			}
			if (trust.ApplicationIdentity == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_ApplicationTrustShouldHaveIdentity"));
			}
			if (this.m_storeBounded)
			{
				this.CommitApplicationTrust(trust.ApplicationIdentity, null);
				return;
			}
			this.AppTrusts.Remove(trust);
		}

		// Token: 0x06002EC3 RID: 11971 RVA: 0x0009DFB0 File Offset: 0x0009CFB0
		public void RemoveRange(ApplicationTrust[] trusts)
		{
			if (trusts == null)
			{
				throw new ArgumentNullException("trusts");
			}
			int i = 0;
			try
			{
				while (i < trusts.Length)
				{
					this.Remove(trusts[i]);
					i++;
				}
			}
			catch
			{
				for (int j = 0; j < i; j++)
				{
					this.Add(trusts[j]);
				}
				throw;
			}
		}

		// Token: 0x06002EC4 RID: 11972 RVA: 0x0009E010 File Offset: 0x0009D010
		public void RemoveRange(ApplicationTrustCollection trusts)
		{
			if (trusts == null)
			{
				throw new ArgumentNullException("trusts");
			}
			int num = 0;
			try
			{
				foreach (ApplicationTrust trust in trusts)
				{
					this.Remove(trust);
					num++;
				}
			}
			catch
			{
				for (int i = 0; i < num; i++)
				{
					this.Add(trusts[i]);
				}
				throw;
			}
		}

		// Token: 0x06002EC5 RID: 11973 RVA: 0x0009E080 File Offset: 0x0009D080
		public void Clear()
		{
			ArrayList appTrusts = this.AppTrusts;
			if (this.m_storeBounded)
			{
				foreach (object obj in appTrusts)
				{
					ApplicationTrust applicationTrust = (ApplicationTrust)obj;
					if (applicationTrust.ApplicationIdentity == null)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_ApplicationTrustShouldHaveIdentity"));
					}
					this.CommitApplicationTrust(applicationTrust.ApplicationIdentity, null);
				}
			}
			appTrusts.Clear();
		}

		// Token: 0x06002EC6 RID: 11974 RVA: 0x0009E108 File Offset: 0x0009D108
		public ApplicationTrustEnumerator GetEnumerator()
		{
			return new ApplicationTrustEnumerator(this);
		}

		// Token: 0x06002EC7 RID: 11975 RVA: 0x0009E110 File Offset: 0x0009D110
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new ApplicationTrustEnumerator(this);
		}

		// Token: 0x06002EC8 RID: 11976 RVA: 0x0009E118 File Offset: 0x0009D118
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
			}
			if (index < 0 || index >= array.Length)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (array.Length - index < this.Count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index++);
			}
		}

		// Token: 0x06002EC9 RID: 11977 RVA: 0x0009E1B2 File Offset: 0x0009D1B2
		public void CopyTo(ApplicationTrust[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x06002ECA RID: 11978 RVA: 0x0009E1BC File Offset: 0x0009D1BC
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x06002ECB RID: 11979 RVA: 0x0009E1BF File Offset: 0x0009D1BF
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x040017E1 RID: 6113
		private const string ApplicationTrustProperty = "ApplicationTrust";

		// Token: 0x040017E2 RID: 6114
		private const string InstallerIdentifier = "{60051b8f-4f12-400a-8e50-dd05ebd438d1}";

		// Token: 0x040017E3 RID: 6115
		private static Guid ClrPropertySet = new Guid("c989bb7a-8385-4715-98cf-a741a8edb823");

		// Token: 0x040017E4 RID: 6116
		private static object s_installReference = null;

		// Token: 0x040017E5 RID: 6117
		private readonly object m_syncRoot = new object();

		// Token: 0x040017E6 RID: 6118
		private object m_appTrusts;

		// Token: 0x040017E7 RID: 6119
		private bool m_storeBounded;

		// Token: 0x040017E8 RID: 6120
		private Store m_pStore;
	}
}
