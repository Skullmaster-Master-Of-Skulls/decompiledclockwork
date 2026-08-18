using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x0200057F RID: 1407
	[HostProtection(SecurityAction.LinkDemand, ExternalProcessMgmt = true)]
	public sealed class LicenseManager
	{
		// Token: 0x0600340A RID: 13322 RVA: 0x000E452E File Offset: 0x000E272E
		private LicenseManager()
		{
		}

		// Token: 0x17000CBC RID: 3260
		// (get) Token: 0x0600340B RID: 13323 RVA: 0x000E4538 File Offset: 0x000E2738
		// (set) Token: 0x0600340C RID: 13324 RVA: 0x000E4598 File Offset: 0x000E2798
		public static LicenseContext CurrentContext
		{
			get
			{
				if (LicenseManager.context == null)
				{
					object obj = LicenseManager.internalSyncObject;
					lock (obj)
					{
						if (LicenseManager.context == null)
						{
							LicenseManager.context = new RuntimeLicenseContext();
						}
					}
				}
				return LicenseManager.context;
			}
			set
			{
				object obj = LicenseManager.internalSyncObject;
				lock (obj)
				{
					if (LicenseManager.contextLockHolder != null)
					{
						throw new InvalidOperationException(SR.GetString("LicMgrContextCannotBeChanged"));
					}
					LicenseManager.context = value;
				}
			}
		}

		// Token: 0x17000CBD RID: 3261
		// (get) Token: 0x0600340D RID: 13325 RVA: 0x000E45F0 File Offset: 0x000E27F0
		public static LicenseUsageMode UsageMode
		{
			get
			{
				if (LicenseManager.context != null)
				{
					return LicenseManager.context.UsageMode;
				}
				return LicenseUsageMode.Runtime;
			}
		}

		// Token: 0x0600340E RID: 13326 RVA: 0x000E460C File Offset: 0x000E280C
		private static void CacheProvider(Type type, LicenseProvider provider)
		{
			if (LicenseManager.providers == null)
			{
				LicenseManager.providers = new Hashtable();
			}
			LicenseManager.providers[type] = provider;
			if (provider != null)
			{
				if (LicenseManager.providerInstances == null)
				{
					LicenseManager.providerInstances = new Hashtable();
				}
				LicenseManager.providerInstances[provider.GetType()] = provider;
			}
		}

		// Token: 0x0600340F RID: 13327 RVA: 0x000E4667 File Offset: 0x000E2867
		public static object CreateWithContext(Type type, LicenseContext creationContext)
		{
			return LicenseManager.CreateWithContext(type, creationContext, new object[0]);
		}

		// Token: 0x06003410 RID: 13328 RVA: 0x000E4678 File Offset: 0x000E2878
		public static object CreateWithContext(Type type, LicenseContext creationContext, object[] args)
		{
			object result = null;
			object obj = LicenseManager.internalSyncObject;
			lock (obj)
			{
				LicenseContext currentContext = LicenseManager.CurrentContext;
				try
				{
					LicenseManager.CurrentContext = creationContext;
					LicenseManager.LockContext(LicenseManager.selfLock);
					try
					{
						result = SecurityUtils.SecureCreateInstance(type, args);
					}
					catch (TargetInvocationException ex)
					{
						throw ex.InnerException;
					}
				}
				finally
				{
					LicenseManager.UnlockContext(LicenseManager.selfLock);
					LicenseManager.CurrentContext = currentContext;
				}
			}
			return result;
		}

		// Token: 0x06003411 RID: 13329 RVA: 0x000E4708 File Offset: 0x000E2908
		private static bool GetCachedNoLicenseProvider(Type type)
		{
			return LicenseManager.providers != null && LicenseManager.providers.ContainsKey(type);
		}

		// Token: 0x06003412 RID: 13330 RVA: 0x000E4722 File Offset: 0x000E2922
		private static LicenseProvider GetCachedProvider(Type type)
		{
			if (LicenseManager.providers != null)
			{
				return (LicenseProvider)LicenseManager.providers[type];
			}
			return null;
		}

		// Token: 0x06003413 RID: 13331 RVA: 0x000E4741 File Offset: 0x000E2941
		private static LicenseProvider GetCachedProviderInstance(Type providerType)
		{
			if (LicenseManager.providerInstances != null)
			{
				return (LicenseProvider)LicenseManager.providerInstances[providerType];
			}
			return null;
		}

		// Token: 0x06003414 RID: 13332 RVA: 0x000E4760 File Offset: 0x000E2960
		private static IntPtr GetLicenseInteropHelperType()
		{
			return typeof(LicenseManager.LicenseInteropHelper).TypeHandle.Value;
		}

		// Token: 0x06003415 RID: 13333 RVA: 0x000E4784 File Offset: 0x000E2984
		public static bool IsLicensed(Type type)
		{
			License license;
			bool result = LicenseManager.ValidateInternal(type, null, false, out license);
			if (license != null)
			{
				license.Dispose();
				license = null;
			}
			return result;
		}

		// Token: 0x06003416 RID: 13334 RVA: 0x000E47A8 File Offset: 0x000E29A8
		public static bool IsValid(Type type)
		{
			License license;
			bool result = LicenseManager.ValidateInternal(type, null, false, out license);
			if (license != null)
			{
				license.Dispose();
				license = null;
			}
			return result;
		}

		// Token: 0x06003417 RID: 13335 RVA: 0x000E47CC File Offset: 0x000E29CC
		public static bool IsValid(Type type, object instance, out License license)
		{
			return LicenseManager.ValidateInternal(type, instance, false, out license);
		}

		// Token: 0x06003418 RID: 13336 RVA: 0x000E47D8 File Offset: 0x000E29D8
		public static void LockContext(object contextUser)
		{
			object obj = LicenseManager.internalSyncObject;
			lock (obj)
			{
				if (LicenseManager.contextLockHolder != null)
				{
					throw new InvalidOperationException(SR.GetString("LicMgrAlreadyLocked"));
				}
				LicenseManager.contextLockHolder = contextUser;
			}
		}

		// Token: 0x06003419 RID: 13337 RVA: 0x000E4830 File Offset: 0x000E2A30
		public static void UnlockContext(object contextUser)
		{
			object obj = LicenseManager.internalSyncObject;
			lock (obj)
			{
				if (LicenseManager.contextLockHolder != contextUser)
				{
					throw new ArgumentException(SR.GetString("LicMgrDifferentUser"));
				}
				LicenseManager.contextLockHolder = null;
			}
		}

		// Token: 0x0600341A RID: 13338 RVA: 0x000E4888 File Offset: 0x000E2A88
		private static bool ValidateInternal(Type type, object instance, bool allowExceptions, out License license)
		{
			string text;
			return LicenseManager.ValidateInternalRecursive(LicenseManager.CurrentContext, type, instance, allowExceptions, out license, out text);
		}

		// Token: 0x0600341B RID: 13339 RVA: 0x000E48A8 File Offset: 0x000E2AA8
		private static bool ValidateInternalRecursive(LicenseContext context, Type type, object instance, bool allowExceptions, out License license, out string licenseKey)
		{
			LicenseProvider licenseProvider = LicenseManager.GetCachedProvider(type);
			if (licenseProvider == null && !LicenseManager.GetCachedNoLicenseProvider(type))
			{
				LicenseProviderAttribute licenseProviderAttribute = (LicenseProviderAttribute)Attribute.GetCustomAttribute(type, typeof(LicenseProviderAttribute), false);
				if (licenseProviderAttribute != null)
				{
					Type licenseProvider2 = licenseProviderAttribute.LicenseProvider;
					licenseProvider = LicenseManager.GetCachedProviderInstance(licenseProvider2);
					if (licenseProvider == null)
					{
						licenseProvider = (LicenseProvider)SecurityUtils.SecureCreateInstance(licenseProvider2);
					}
				}
				LicenseManager.CacheProvider(type, licenseProvider);
			}
			license = null;
			bool flag = true;
			licenseKey = null;
			if (licenseProvider != null)
			{
				license = licenseProvider.GetLicense(context, type, instance, allowExceptions);
				if (license == null)
				{
					flag = false;
				}
				else
				{
					licenseKey = license.LicenseKey;
				}
			}
			if (flag && instance == null)
			{
				Type baseType = type.BaseType;
				if (baseType != typeof(object) && baseType != null)
				{
					if (license != null)
					{
						license.Dispose();
						license = null;
					}
					string text;
					flag = LicenseManager.ValidateInternalRecursive(context, baseType, null, allowExceptions, out license, out text);
					if (license != null)
					{
						license.Dispose();
						license = null;
					}
				}
			}
			return flag;
		}

		// Token: 0x0600341C RID: 13340 RVA: 0x000E4990 File Offset: 0x000E2B90
		public static void Validate(Type type)
		{
			License license;
			if (!LicenseManager.ValidateInternal(type, null, true, out license))
			{
				throw new LicenseException(type);
			}
			if (license != null)
			{
				license.Dispose();
				license = null;
			}
		}

		// Token: 0x0600341D RID: 13341 RVA: 0x000E49BC File Offset: 0x000E2BBC
		public static License Validate(Type type, object instance)
		{
			License result;
			if (!LicenseManager.ValidateInternal(type, instance, true, out result))
			{
				throw new LicenseException(type, instance);
			}
			return result;
		}

		// Token: 0x040029CB RID: 10699
		private static readonly object selfLock = new object();

		// Token: 0x040029CC RID: 10700
		private static volatile LicenseContext context = null;

		// Token: 0x040029CD RID: 10701
		private static object contextLockHolder = null;

		// Token: 0x040029CE RID: 10702
		private static volatile Hashtable providers;

		// Token: 0x040029CF RID: 10703
		private static volatile Hashtable providerInstances;

		// Token: 0x040029D0 RID: 10704
		private static object internalSyncObject = new object();

		// Token: 0x02000895 RID: 2197
		private class LicenseInteropHelper
		{
			// Token: 0x0600459F RID: 17823 RVA: 0x00123414 File Offset: 0x00121614
			private static object AllocateAndValidateLicense(RuntimeTypeHandle rth, IntPtr bstrKey, int fDesignTime)
			{
				Type typeFromHandle = Type.GetTypeFromHandle(rth);
				LicenseManager.LicenseInteropHelper.CLRLicenseContext clrlicenseContext = new LicenseManager.LicenseInteropHelper.CLRLicenseContext((fDesignTime != 0) ? LicenseUsageMode.Designtime : LicenseUsageMode.Runtime, typeFromHandle);
				if (fDesignTime == 0 && bstrKey != (IntPtr)0)
				{
					clrlicenseContext.SetSavedLicenseKey(typeFromHandle, Marshal.PtrToStringBSTR(bstrKey));
				}
				object result;
				try
				{
					result = LicenseManager.CreateWithContext(typeFromHandle, clrlicenseContext);
				}
				catch (LicenseException ex)
				{
					throw new COMException(ex.Message, -2147221230);
				}
				return result;
			}

			// Token: 0x060045A0 RID: 17824 RVA: 0x00123484 File Offset: 0x00121684
			private static int RequestLicKey(RuntimeTypeHandle rth, ref IntPtr pbstrKey)
			{
				Type typeFromHandle = Type.GetTypeFromHandle(rth);
				License license;
				string text;
				if (!LicenseManager.ValidateInternalRecursive(LicenseManager.CurrentContext, typeFromHandle, null, false, out license, out text))
				{
					return -2147483640;
				}
				if (text == null)
				{
					return -2147483640;
				}
				pbstrKey = Marshal.StringToBSTR(text);
				if (license != null)
				{
					license.Dispose();
					license = null;
				}
				return 0;
			}

			// Token: 0x060045A1 RID: 17825 RVA: 0x001234D0 File Offset: 0x001216D0
			private void GetLicInfo(RuntimeTypeHandle rth, ref int pRuntimeKeyAvail, ref int pLicVerified)
			{
				pRuntimeKeyAvail = 0;
				pLicVerified = 0;
				Type typeFromHandle = Type.GetTypeFromHandle(rth);
				if (this.helperContext == null)
				{
					this.helperContext = new DesigntimeLicenseContext();
				}
				else
				{
					this.helperContext.savedLicenseKeys.Clear();
				}
				License license;
				string text;
				if (LicenseManager.ValidateInternalRecursive(this.helperContext, typeFromHandle, null, false, out license, out text))
				{
					if (this.helperContext.savedLicenseKeys.Contains(typeFromHandle.AssemblyQualifiedName))
					{
						pRuntimeKeyAvail = 1;
					}
					if (license != null)
					{
						license.Dispose();
						license = null;
						pLicVerified = 1;
					}
				}
			}

			// Token: 0x060045A2 RID: 17826 RVA: 0x0012354C File Offset: 0x0012174C
			private void GetCurrentContextInfo(ref int fDesignTime, ref IntPtr bstrKey, RuntimeTypeHandle rth)
			{
				this.savedLicenseContext = LicenseManager.CurrentContext;
				this.savedType = Type.GetTypeFromHandle(rth);
				if (this.savedLicenseContext.UsageMode == LicenseUsageMode.Designtime)
				{
					fDesignTime = 1;
					bstrKey = (IntPtr)0;
					return;
				}
				fDesignTime = 0;
				string savedLicenseKey = this.savedLicenseContext.GetSavedLicenseKey(this.savedType, null);
				bstrKey = Marshal.StringToBSTR(savedLicenseKey);
			}

			// Token: 0x060045A3 RID: 17827 RVA: 0x001235A8 File Offset: 0x001217A8
			private void SaveKeyInCurrentContext(IntPtr bstrKey)
			{
				if (bstrKey != (IntPtr)0)
				{
					this.savedLicenseContext.SetSavedLicenseKey(this.savedType, Marshal.PtrToStringBSTR(bstrKey));
				}
			}

			// Token: 0x040037D0 RID: 14288
			private const int S_OK = 0;

			// Token: 0x040037D1 RID: 14289
			private const int E_NOTIMPL = -2147467263;

			// Token: 0x040037D2 RID: 14290
			private const int CLASS_E_NOTLICENSED = -2147221230;

			// Token: 0x040037D3 RID: 14291
			private const int E_FAIL = -2147483640;

			// Token: 0x040037D4 RID: 14292
			private DesigntimeLicenseContext helperContext;

			// Token: 0x040037D5 RID: 14293
			private LicenseContext savedLicenseContext;

			// Token: 0x040037D6 RID: 14294
			private Type savedType;

			// Token: 0x02000933 RID: 2355
			internal class CLRLicenseContext : LicenseContext
			{
				// Token: 0x060046B0 RID: 18096 RVA: 0x00127090 File Offset: 0x00125290
				public CLRLicenseContext(LicenseUsageMode usageMode, Type type)
				{
					this.usageMode = usageMode;
					this.type = type;
				}

				// Token: 0x17000FF0 RID: 4080
				// (get) Token: 0x060046B1 RID: 18097 RVA: 0x001270A6 File Offset: 0x001252A6
				public override LicenseUsageMode UsageMode
				{
					get
					{
						return this.usageMode;
					}
				}

				// Token: 0x060046B2 RID: 18098 RVA: 0x001270AE File Offset: 0x001252AE
				public override string GetSavedLicenseKey(Type type, Assembly resourceAssembly)
				{
					if (!(type == this.type))
					{
						return null;
					}
					return this.key;
				}

				// Token: 0x060046B3 RID: 18099 RVA: 0x001270C6 File Offset: 0x001252C6
				public override void SetSavedLicenseKey(Type type, string key)
				{
					if (type == this.type)
					{
						this.key = key;
					}
				}

				// Token: 0x04003DE6 RID: 15846
				private LicenseUsageMode usageMode;

				// Token: 0x04003DE7 RID: 15847
				private Type type;

				// Token: 0x04003DE8 RID: 15848
				private string key;
			}
		}
	}
}
