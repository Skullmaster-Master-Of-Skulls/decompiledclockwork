using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Remoting;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Threading;

namespace System.Net
{
	// Token: 0x02000194 RID: 404
	internal class AutoWebProxyScriptWrapper
	{
		// Token: 0x06000FA8 RID: 4008 RVA: 0x00051ABC File Offset: 0x0004FCBC
		static AutoWebProxyScriptWrapper()
		{
			AppDomain.CurrentDomain.DomainUnload += AutoWebProxyScriptWrapper.OnDomainUnload;
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x00051AE8 File Offset: 0x0004FCE8
		[ReflectionPermission(SecurityAction.Assert, Flags = ReflectionPermissionFlag.MemberAccess)]
		[ReflectionPermission(SecurityAction.Assert, Flags = ReflectionPermissionFlag.TypeInformation)]
		internal AutoWebProxyScriptWrapper()
		{
			Exception ex = null;
			if (AutoWebProxyScriptWrapper.s_ProxyScriptHelperLoadError == null && AutoWebProxyScriptWrapper.s_ProxyScriptHelperType == null)
			{
				object obj = AutoWebProxyScriptWrapper.s_ProxyScriptHelperLock;
				lock (obj)
				{
					if (AutoWebProxyScriptWrapper.s_ProxyScriptHelperLoadError == null && AutoWebProxyScriptWrapper.s_ProxyScriptHelperType == null)
					{
						try
						{
							AutoWebProxyScriptWrapper.s_ProxyScriptHelperType = Type.GetType("System.Net.VsaWebProxyScript, Microsoft.JScript, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", true);
						}
						catch (Exception ex2)
						{
							ex = ex2;
						}
						if (AutoWebProxyScriptWrapper.s_ProxyScriptHelperType == null)
						{
							AutoWebProxyScriptWrapper.s_ProxyScriptHelperLoadError = ((ex == null) ? new InternalException() : ex);
						}
					}
				}
			}
			if (AutoWebProxyScriptWrapper.s_ProxyScriptHelperLoadError != null)
			{
				throw new TypeLoadException(SR.GetString("net_cannot_load_proxy_helper"), (AutoWebProxyScriptWrapper.s_ProxyScriptHelperLoadError is InternalException) ? null : AutoWebProxyScriptWrapper.s_ProxyScriptHelperLoadError);
			}
			this.CreateAppDomain();
			ex = null;
			try
			{
				ObjectHandle objectHandle = Activator.CreateInstance(this.scriptDomain, AutoWebProxyScriptWrapper.s_ProxyScriptHelperType.Assembly.FullName, AutoWebProxyScriptWrapper.s_ProxyScriptHelperType.FullName, false, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.InvokeMethod | BindingFlags.CreateInstance, null, null, null, null, null);
				if (objectHandle != null)
				{
					this.site = (IWebProxyScript)objectHandle.Unwrap();
				}
			}
			catch (Exception ex3)
			{
				ex = ex3;
			}
			if (this.site == null)
			{
				object obj2 = AutoWebProxyScriptWrapper.s_ProxyScriptHelperLock;
				lock (obj2)
				{
					if (AutoWebProxyScriptWrapper.s_ProxyScriptHelperLoadError == null)
					{
						AutoWebProxyScriptWrapper.s_ProxyScriptHelperLoadError = ((ex == null) ? new InternalException() : ex);
					}
				}
				throw new TypeLoadException(SR.GetString("net_cannot_load_proxy_helper"), (AutoWebProxyScriptWrapper.s_ProxyScriptHelperLoadError is InternalException) ? null : AutoWebProxyScriptWrapper.s_ProxyScriptHelperLoadError);
			}
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x00051CB0 File Offset: 0x0004FEB0
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.ControlAppDomain)]
		private void CreateAppDomain()
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				Monitor.Enter(AutoWebProxyScriptWrapper.s_AppDomains.SyncRoot, ref flag);
				if (AutoWebProxyScriptWrapper.s_CleanedUp)
				{
					throw new InvalidOperationException(SR.GetString("net_cant_perform_during_shutdown"));
				}
				if (AutoWebProxyScriptWrapper.s_AppDomainInfo == null)
				{
					AutoWebProxyScriptWrapper.s_AppDomainInfo = new AppDomainSetup();
					AutoWebProxyScriptWrapper.s_AppDomainInfo.DisallowBindingRedirects = true;
					AutoWebProxyScriptWrapper.s_AppDomainInfo.DisallowCodeDownload = true;
					NamedPermissionSet namedPermissionSet = new NamedPermissionSet("__WebProxySandbox", PermissionState.None);
					namedPermissionSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
					ApplicationTrust applicationTrust = new ApplicationTrust();
					applicationTrust.DefaultGrantSet = new PolicyStatement(namedPermissionSet);
					AutoWebProxyScriptWrapper.s_AppDomainInfo.ApplicationTrust = applicationTrust;
					AutoWebProxyScriptWrapper.s_AppDomainInfo.ApplicationBase = Environment.SystemDirectory;
				}
				AppDomain appDomain = AutoWebProxyScriptWrapper.s_ExcessAppDomain;
				if (appDomain != null)
				{
					TimerThread.GetOrCreateQueue(0).CreateTimer(new TimerThread.Callback(AutoWebProxyScriptWrapper.CloseAppDomainCallback), appDomain);
					throw new InvalidOperationException(SR.GetString("net_cant_create_environment"));
				}
				this.appDomainIndex = AutoWebProxyScriptWrapper.s_NextAppDomainIndex++;
				try
				{
				}
				finally
				{
					PermissionSet permissionSet = new PermissionSet(PermissionState.None);
					permissionSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
					AutoWebProxyScriptWrapper.s_ExcessAppDomain = AppDomain.CreateDomain("WebProxyScript", null, AutoWebProxyScriptWrapper.s_AppDomainInfo, permissionSet, null);
					try
					{
						AutoWebProxyScriptWrapper.s_AppDomains.Add(this.appDomainIndex, AutoWebProxyScriptWrapper.s_ExcessAppDomain);
						this.scriptDomain = AutoWebProxyScriptWrapper.s_ExcessAppDomain;
					}
					finally
					{
						if (this.scriptDomain == AutoWebProxyScriptWrapper.s_ExcessAppDomain)
						{
							AutoWebProxyScriptWrapper.s_ExcessAppDomain = null;
						}
						else
						{
							try
							{
								AutoWebProxyScriptWrapper.s_AppDomains.Remove(this.appDomainIndex);
							}
							finally
							{
								TimerThread.GetOrCreateQueue(0).CreateTimer(new TimerThread.Callback(AutoWebProxyScriptWrapper.CloseAppDomainCallback), AutoWebProxyScriptWrapper.s_ExcessAppDomain);
							}
						}
					}
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(AutoWebProxyScriptWrapper.s_AppDomains.SyncRoot);
				}
			}
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x00051ECC File Offset: 0x000500CC
		internal void Close()
		{
			this.site.Close();
			TimerThread.GetOrCreateQueue(0).CreateTimer(new TimerThread.Callback(AutoWebProxyScriptWrapper.CloseAppDomainCallback), this.appDomainIndex);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x00051F04 File Offset: 0x00050104
		~AutoWebProxyScriptWrapper()
		{
			if (!NclUtilities.HasShutdownStarted && this.scriptDomain != null)
			{
				TimerThread.GetOrCreateQueue(0).CreateTimer(new TimerThread.Callback(AutoWebProxyScriptWrapper.CloseAppDomainCallback), this.appDomainIndex);
			}
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x00051F5C File Offset: 0x0005015C
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.ControlAppDomain)]
		private static void CloseAppDomainCallback(TimerThread.Timer timer, int timeNoticed, object context)
		{
			try
			{
				AppDomain appDomain = context as AppDomain;
				if (appDomain == null)
				{
					AutoWebProxyScriptWrapper.CloseAppDomain((int)context);
				}
				else if (appDomain == AutoWebProxyScriptWrapper.s_ExcessAppDomain)
				{
					try
					{
						AppDomain.Unload(appDomain);
					}
					catch (AppDomainUnloadedException)
					{
					}
					AutoWebProxyScriptWrapper.s_ExcessAppDomain = null;
				}
			}
			catch (Exception exception)
			{
				if (NclUtilities.IsFatal(exception))
				{
					throw;
				}
			}
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x00051FC8 File Offset: 0x000501C8
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.ControlAppDomain)]
		private static void CloseAppDomain(int index)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			AppDomain domain;
			try
			{
				Monitor.Enter(AutoWebProxyScriptWrapper.s_AppDomains.SyncRoot, ref flag);
				if (AutoWebProxyScriptWrapper.s_CleanedUp)
				{
					return;
				}
				domain = (AppDomain)AutoWebProxyScriptWrapper.s_AppDomains[index];
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(AutoWebProxyScriptWrapper.s_AppDomains.SyncRoot);
					flag = false;
				}
			}
			try
			{
				AppDomain.Unload(domain);
			}
			catch (AppDomainUnloadedException)
			{
			}
			finally
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					Monitor.Enter(AutoWebProxyScriptWrapper.s_AppDomains.SyncRoot, ref flag);
					AutoWebProxyScriptWrapper.s_AppDomains.Remove(index);
				}
				finally
				{
					if (flag)
					{
						Monitor.Exit(AutoWebProxyScriptWrapper.s_AppDomains.SyncRoot);
					}
				}
			}
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x000520A0 File Offset: 0x000502A0
		[ReliabilityContract(Consistency.MayCorruptProcess, Cer.MayFail)]
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.ControlAppDomain)]
		private static void OnDomainUnload(object sender, EventArgs e)
		{
			object syncRoot = AutoWebProxyScriptWrapper.s_AppDomains.SyncRoot;
			lock (syncRoot)
			{
				if (!AutoWebProxyScriptWrapper.s_CleanedUp)
				{
					AutoWebProxyScriptWrapper.s_CleanedUp = true;
					foreach (object obj in AutoWebProxyScriptWrapper.s_AppDomains.Values)
					{
						AppDomain domain = (AppDomain)obj;
						try
						{
							AppDomain.Unload(domain);
						}
						catch
						{
						}
					}
					AutoWebProxyScriptWrapper.s_AppDomains.Clear();
					AppDomain appDomain = AutoWebProxyScriptWrapper.s_ExcessAppDomain;
					if (appDomain != null)
					{
						try
						{
							AppDomain.Unload(appDomain);
						}
						catch
						{
						}
						AutoWebProxyScriptWrapper.s_ExcessAppDomain = null;
					}
				}
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000FB0 RID: 4016 RVA: 0x00052180 File Offset: 0x00050380
		internal string ScriptBody
		{
			get
			{
				return this.scriptText;
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000FB1 RID: 4017 RVA: 0x00052188 File Offset: 0x00050388
		// (set) Token: 0x06000FB2 RID: 4018 RVA: 0x00052190 File Offset: 0x00050390
		internal byte[] Buffer
		{
			get
			{
				return this.scriptBytes;
			}
			set
			{
				this.scriptBytes = value;
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000FB3 RID: 4019 RVA: 0x00052199 File Offset: 0x00050399
		// (set) Token: 0x06000FB4 RID: 4020 RVA: 0x000521A1 File Offset: 0x000503A1
		internal DateTime LastModified
		{
			get
			{
				return this.lastModified;
			}
			set
			{
				this.lastModified = value;
			}
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x000521AA File Offset: 0x000503AA
		internal string FindProxyForURL(string url, string host)
		{
			return this.site.Run(url, host);
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x000521B9 File Offset: 0x000503B9
		internal bool Compile(Uri engineScriptLocation, string scriptBody, byte[] buffer)
		{
			if (this.site.Load(engineScriptLocation, scriptBody, typeof(WebProxyScriptHelper)))
			{
				this.scriptText = scriptBody;
				this.scriptBytes = buffer;
				return true;
			}
			return false;
		}

		// Token: 0x040012D4 RID: 4820
		private const string c_appDomainName = "WebProxyScript";

		// Token: 0x040012D5 RID: 4821
		private int appDomainIndex;

		// Token: 0x040012D6 RID: 4822
		private AppDomain scriptDomain;

		// Token: 0x040012D7 RID: 4823
		private IWebProxyScript site;

		// Token: 0x040012D8 RID: 4824
		private static volatile AppDomain s_ExcessAppDomain;

		// Token: 0x040012D9 RID: 4825
		private static Hashtable s_AppDomains = new Hashtable();

		// Token: 0x040012DA RID: 4826
		private static bool s_CleanedUp;

		// Token: 0x040012DB RID: 4827
		private static int s_NextAppDomainIndex;

		// Token: 0x040012DC RID: 4828
		private static AppDomainSetup s_AppDomainInfo;

		// Token: 0x040012DD RID: 4829
		private static volatile Type s_ProxyScriptHelperType;

		// Token: 0x040012DE RID: 4830
		private static volatile Exception s_ProxyScriptHelperLoadError;

		// Token: 0x040012DF RID: 4831
		private static object s_ProxyScriptHelperLock = new object();

		// Token: 0x040012E0 RID: 4832
		private string scriptText;

		// Token: 0x040012E1 RID: 4833
		private byte[] scriptBytes;

		// Token: 0x040012E2 RID: 4834
		private DateTime lastModified;
	}
}
