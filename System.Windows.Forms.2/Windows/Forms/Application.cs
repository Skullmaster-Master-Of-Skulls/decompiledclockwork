using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Deployment.Application;
using System.Deployment.Internal.Isolation;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Windows.Forms.Layout;
using System.Windows.Forms.VisualStyles;
using Microsoft.Win32;

namespace System.Windows.Forms
{
	// Token: 0x02000122 RID: 290
	public sealed class Application
	{
		// Token: 0x060008F6 RID: 2294 RVA: 0x00002843 File Offset: 0x00000A43
		private Application()
		{
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x060008F7 RID: 2295 RVA: 0x000183A9 File Offset: 0x000165A9
		public static bool AllowQuit
		{
			get
			{
				return Application.ThreadContext.FromCurrent().GetAllowQuit();
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x060008F8 RID: 2296 RVA: 0x000183B5 File Offset: 0x000165B5
		internal static bool CanContinueIdle
		{
			get
			{
				return Application.ThreadContext.FromCurrent().ComponentManager.FContinueIdle();
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x060008F9 RID: 2297 RVA: 0x000183C6 File Offset: 0x000165C6
		internal static bool ComCtlSupportsVisualStyles
		{
			get
			{
				if (!Application.comCtlSupportsVisualStylesInitialized)
				{
					Application.comCtlSupportsVisualStyles = Application.InitializeComCtlSupportsVisualStyles();
					Application.comCtlSupportsVisualStylesInitialized = true;
				}
				return Application.comCtlSupportsVisualStyles;
			}
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x000183E4 File Offset: 0x000165E4
		private static bool InitializeComCtlSupportsVisualStyles()
		{
			if (Application.useVisualStyles && OSFeature.Feature.IsPresent(OSFeature.Themes))
			{
				return true;
			}
			IntPtr intPtr = UnsafeNativeMethods.GetModuleHandle("comctl32.dll");
			if (intPtr != IntPtr.Zero)
			{
				try
				{
					IntPtr procAddress = UnsafeNativeMethods.GetProcAddress(new HandleRef(null, intPtr), "ImageList_WriteEx");
					return procAddress != IntPtr.Zero;
				}
				catch
				{
					return false;
				}
			}
			intPtr = UnsafeNativeMethods.LoadLibraryFromSystemPathIfAvailable("comctl32.dll");
			if (intPtr != IntPtr.Zero)
			{
				IntPtr procAddress2 = UnsafeNativeMethods.GetProcAddress(new HandleRef(null, intPtr), "ImageList_WriteEx");
				return procAddress2 != IntPtr.Zero;
			}
			return false;
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x060008FB RID: 2299 RVA: 0x00018490 File Offset: 0x00016690
		public static RegistryKey CommonAppDataRegistry
		{
			get
			{
				return Registry.LocalMachine.CreateSubKey(Application.CommonAppDataRegistryKeyName);
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x060008FC RID: 2300 RVA: 0x000184A4 File Offset: 0x000166A4
		internal static string CommonAppDataRegistryKeyName
		{
			get
			{
				string format = "Software\\{0}\\{1}\\{2}";
				return string.Format(CultureInfo.CurrentCulture, format, new object[]
				{
					Application.CompanyName,
					Application.ProductName,
					Application.ProductVersion
				});
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x060008FD RID: 2301 RVA: 0x000184E0 File Offset: 0x000166E0
		internal static bool UseEverettThreadAffinity
		{
			get
			{
				if (!Application.checkedThreadAffinity)
				{
					Application.checkedThreadAffinity = true;
					try
					{
						new RegistryPermission(PermissionState.Unrestricted).Assert();
						RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(Application.CommonAppDataRegistryKeyName);
						if (registryKey != null)
						{
							object value = registryKey.GetValue("EnableSystemEventsThreadAffinityCompatibility");
							registryKey.Close();
							if (value != null && (int)value != 0)
							{
								Application.useEverettThreadAffinity = true;
							}
						}
					}
					catch (SecurityException)
					{
					}
					catch (InvalidCastException)
					{
					}
				}
				return Application.useEverettThreadAffinity;
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x060008FE RID: 2302 RVA: 0x00018564 File Offset: 0x00016764
		public static string CommonAppDataPath
		{
			get
			{
				try
				{
					if (ApplicationDeployment.IsNetworkDeployed)
					{
						string text = AppDomain.CurrentDomain.GetData("DataDirectory") as string;
						if (text != null)
						{
							return text;
						}
					}
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsSecurityOrCriticalException(ex))
					{
						throw;
					}
				}
				return Application.GetDataPath(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData));
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x060008FF RID: 2303 RVA: 0x000185C4 File Offset: 0x000167C4
		public static string CompanyName
		{
			get
			{
				object obj = Application.internalSyncObject;
				lock (obj)
				{
					if (Application.companyName == null)
					{
						Assembly entryAssembly = Assembly.GetEntryAssembly();
						if (entryAssembly != null)
						{
							object[] customAttributes = entryAssembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
							if (customAttributes != null && customAttributes.Length != 0)
							{
								Application.companyName = ((AssemblyCompanyAttribute)customAttributes[0]).Company;
							}
						}
						if (Application.companyName == null || Application.companyName.Length == 0)
						{
							Application.companyName = Application.GetAppFileVersionInfo().CompanyName;
							if (Application.companyName != null)
							{
								Application.companyName = Application.companyName.Trim();
							}
						}
						if (Application.companyName == null || Application.companyName.Length == 0)
						{
							Type appMainType = Application.GetAppMainType();
							if (appMainType != null)
							{
								string @namespace = appMainType.Namespace;
								if (!string.IsNullOrEmpty(@namespace))
								{
									int num = @namespace.IndexOf(".");
									if (num != -1)
									{
										Application.companyName = @namespace.Substring(0, num);
									}
									else
									{
										Application.companyName = @namespace;
									}
								}
								else
								{
									Application.companyName = Application.ProductName;
								}
							}
						}
					}
				}
				return Application.companyName;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000900 RID: 2304 RVA: 0x000186EC File Offset: 0x000168EC
		// (set) Token: 0x06000901 RID: 2305 RVA: 0x000186F8 File Offset: 0x000168F8
		public static CultureInfo CurrentCulture
		{
			get
			{
				return Thread.CurrentThread.CurrentCulture;
			}
			set
			{
				Thread.CurrentThread.CurrentCulture = value;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000902 RID: 2306 RVA: 0x00018705 File Offset: 0x00016905
		// (set) Token: 0x06000903 RID: 2307 RVA: 0x0001870C File Offset: 0x0001690C
		public static InputLanguage CurrentInputLanguage
		{
			get
			{
				return InputLanguage.CurrentInputLanguage;
			}
			set
			{
				IntSecurity.AffectThreadBehavior.Demand();
				InputLanguage.CurrentInputLanguage = value;
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000904 RID: 2308 RVA: 0x0001871E File Offset: 0x0001691E
		internal static bool CustomThreadExceptionHandlerAttached
		{
			get
			{
				return Application.ThreadContext.FromCurrent().CustomThreadExceptionHandlerAttached;
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000905 RID: 2309 RVA: 0x0001872C File Offset: 0x0001692C
		public static string ExecutablePath
		{
			get
			{
				if (Application.executablePath == null)
				{
					Assembly entryAssembly = Assembly.GetEntryAssembly();
					if (entryAssembly == null)
					{
						StringBuilder moduleFileNameLongPath = UnsafeNativeMethods.GetModuleFileNameLongPath(NativeMethods.NullHandleRef);
						Application.executablePath = IntSecurity.UnsafeGetFullPath(moduleFileNameLongPath.ToString());
					}
					else
					{
						string codeBase = entryAssembly.CodeBase;
						Uri uri = new Uri(codeBase);
						if (uri.IsFile)
						{
							Application.executablePath = uri.LocalPath + Uri.UnescapeDataString(uri.Fragment);
						}
						else
						{
							Application.executablePath = uri.ToString();
						}
					}
				}
				Uri uri2 = new Uri(Application.executablePath);
				if (uri2.Scheme == "file")
				{
					new FileIOPermission(FileIOPermissionAccess.PathDiscovery, Application.executablePath).Demand();
				}
				return Application.executablePath;
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000906 RID: 2310 RVA: 0x000187E4 File Offset: 0x000169E4
		public static string LocalUserAppDataPath
		{
			get
			{
				try
				{
					if (ApplicationDeployment.IsNetworkDeployed)
					{
						string text = AppDomain.CurrentDomain.GetData("DataDirectory") as string;
						if (text != null)
						{
							return text;
						}
					}
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsSecurityOrCriticalException(ex))
					{
						throw;
					}
				}
				return Application.GetDataPath(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000907 RID: 2311 RVA: 0x00018844 File Offset: 0x00016A44
		public static bool MessageLoop
		{
			get
			{
				return Application.ThreadContext.FromCurrent().GetMessageLoop();
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000908 RID: 2312 RVA: 0x00018850 File Offset: 0x00016A50
		public static FormCollection OpenForms
		{
			[UIPermission(SecurityAction.Demand, Window = UIPermissionWindow.AllWindows)]
			get
			{
				return Application.OpenFormsInternal;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000909 RID: 2313 RVA: 0x00018857 File Offset: 0x00016A57
		internal static FormCollection OpenFormsInternal
		{
			get
			{
				if (Application.forms == null)
				{
					Application.forms = new FormCollection();
				}
				return Application.forms;
			}
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x0001886F File Offset: 0x00016A6F
		internal static void OpenFormsInternalAdd(Form form)
		{
			Application.OpenFormsInternal.Add(form);
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x0001887C File Offset: 0x00016A7C
		internal static void OpenFormsInternalRemove(Form form)
		{
			Application.OpenFormsInternal.Remove(form);
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x0600090C RID: 2316 RVA: 0x0001888C File Offset: 0x00016A8C
		public static string ProductName
		{
			get
			{
				object obj = Application.internalSyncObject;
				lock (obj)
				{
					if (Application.productName == null)
					{
						Assembly entryAssembly = Assembly.GetEntryAssembly();
						if (entryAssembly != null)
						{
							object[] customAttributes = entryAssembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false);
							if (customAttributes != null && customAttributes.Length != 0)
							{
								Application.productName = ((AssemblyProductAttribute)customAttributes[0]).Product;
							}
						}
						if (Application.productName == null || Application.productName.Length == 0)
						{
							Application.productName = Application.GetAppFileVersionInfo().ProductName;
							if (Application.productName != null)
							{
								Application.productName = Application.productName.Trim();
							}
						}
						if (Application.productName == null || Application.productName.Length == 0)
						{
							Type appMainType = Application.GetAppMainType();
							if (appMainType != null)
							{
								string @namespace = appMainType.Namespace;
								if (!string.IsNullOrEmpty(@namespace))
								{
									int num = @namespace.LastIndexOf(".");
									if (num != -1 && num < @namespace.Length - 1)
									{
										Application.productName = @namespace.Substring(num + 1);
									}
									else
									{
										Application.productName = @namespace;
									}
								}
								else
								{
									Application.productName = appMainType.Name;
								}
							}
						}
					}
				}
				return Application.productName;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x0600090D RID: 2317 RVA: 0x000189D0 File Offset: 0x00016BD0
		public static string ProductVersion
		{
			get
			{
				object obj = Application.internalSyncObject;
				lock (obj)
				{
					if (Application.productVersion == null)
					{
						Assembly entryAssembly = Assembly.GetEntryAssembly();
						if (entryAssembly != null)
						{
							object[] customAttributes = entryAssembly.GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute), false);
							if (customAttributes != null && customAttributes.Length != 0)
							{
								Application.productVersion = ((AssemblyInformationalVersionAttribute)customAttributes[0]).InformationalVersion;
							}
						}
						if (Application.productVersion == null || Application.productVersion.Length == 0)
						{
							Application.productVersion = Application.GetAppFileVersionInfo().ProductVersion;
							if (Application.productVersion != null)
							{
								Application.productVersion = Application.productVersion.Trim();
							}
						}
						if (Application.productVersion == null || Application.productVersion.Length == 0)
						{
							Application.productVersion = "1.0.0.0";
						}
					}
				}
				return Application.productVersion;
			}
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x00018AA8 File Offset: 0x00016CA8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public static void RegisterMessageLoop(Application.MessageLoopCallback callback)
		{
			Application.ThreadContext.FromCurrent().RegisterMessageLoop(callback);
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x0600090F RID: 2319 RVA: 0x00018AB5 File Offset: 0x00016CB5
		public static bool RenderWithVisualStyles
		{
			get
			{
				return Application.ComCtlSupportsVisualStyles && VisualStyleRenderer.IsSupported;
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000910 RID: 2320 RVA: 0x00018AC5 File Offset: 0x00016CC5
		// (set) Token: 0x06000911 RID: 2321 RVA: 0x00018AE2 File Offset: 0x00016CE2
		public static string SafeTopLevelCaptionFormat
		{
			get
			{
				if (Application.safeTopLevelCaptionSuffix == null)
				{
					Application.safeTopLevelCaptionSuffix = SR.GetString("SafeTopLevelCaptionFormat");
				}
				return Application.safeTopLevelCaptionSuffix;
			}
			set
			{
				IntSecurity.WindowAdornmentModification.Demand();
				if (value == null)
				{
					value = string.Empty;
				}
				Application.safeTopLevelCaptionSuffix = value;
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000912 RID: 2322 RVA: 0x00018B00 File Offset: 0x00016D00
		public static string StartupPath
		{
			get
			{
				if (Application.startupPath == null)
				{
					StringBuilder moduleFileNameLongPath = UnsafeNativeMethods.GetModuleFileNameLongPath(NativeMethods.NullHandleRef);
					Application.startupPath = Path.GetDirectoryName(moduleFileNameLongPath.ToString());
				}
				new FileIOPermission(FileIOPermissionAccess.PathDiscovery, Application.startupPath).Demand();
				return Application.startupPath;
			}
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x00018B44 File Offset: 0x00016D44
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public static void UnregisterMessageLoop()
		{
			Application.ThreadContext.FromCurrent().RegisterMessageLoop(null);
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000914 RID: 2324 RVA: 0x00018B51 File Offset: 0x00016D51
		// (set) Token: 0x06000915 RID: 2325 RVA: 0x00018B58 File Offset: 0x00016D58
		public static bool UseWaitCursor
		{
			get
			{
				return Application.useWaitCursor;
			}
			set
			{
				object collectionSyncRoot = FormCollection.CollectionSyncRoot;
				lock (collectionSyncRoot)
				{
					Application.useWaitCursor = value;
					foreach (object obj in Application.OpenFormsInternal)
					{
						Form form = (Form)obj;
						form.UseWaitCursor = Application.useWaitCursor;
					}
				}
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000916 RID: 2326 RVA: 0x00018BE4 File Offset: 0x00016DE4
		public static string UserAppDataPath
		{
			get
			{
				try
				{
					if (ApplicationDeployment.IsNetworkDeployed)
					{
						string text = AppDomain.CurrentDomain.GetData("DataDirectory") as string;
						if (text != null)
						{
							return text;
						}
					}
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsSecurityOrCriticalException(ex))
					{
						throw;
					}
				}
				return Application.GetDataPath(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x00018C44 File Offset: 0x00016E44
		public static RegistryKey UserAppDataRegistry
		{
			get
			{
				string format = "Software\\{0}\\{1}\\{2}";
				return Registry.CurrentUser.CreateSubKey(string.Format(CultureInfo.CurrentCulture, format, new object[]
				{
					Application.CompanyName,
					Application.ProductName,
					Application.ProductVersion
				}));
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000918 RID: 2328 RVA: 0x00018C8A File Offset: 0x00016E8A
		internal static bool UseVisualStyles
		{
			get
			{
				return Application.useVisualStyles;
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000919 RID: 2329 RVA: 0x00018C91 File Offset: 0x00016E91
		internal static string WindowsFormsVersion
		{
			get
			{
				return "WindowsForms10";
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x00018C98 File Offset: 0x00016E98
		internal static string WindowMessagesVersion
		{
			get
			{
				return "WindowsForms12";
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x00018CA0 File Offset: 0x00016EA0
		// (set) Token: 0x0600091C RID: 2332 RVA: 0x00018CC0 File Offset: 0x00016EC0
		public static VisualStyleState VisualStyleState
		{
			get
			{
				if (!VisualStyleInformation.IsSupportedByOS)
				{
					return VisualStyleState.NoneEnabled;
				}
				return (VisualStyleState)SafeNativeMethods.GetThemeAppProperties();
			}
			set
			{
				if (VisualStyleInformation.IsSupportedByOS)
				{
					if (!ClientUtils.IsEnumValid(value, (int)value, 0, 3) && LocalAppContextSwitches.EnableVisualStyleValidation)
					{
						throw new InvalidEnumArgumentException("value", (int)value, typeof(VisualStyleState));
					}
					SafeNativeMethods.SetThemeAppProperties((int)value);
					SafeNativeMethods.EnumThreadWindowsCallback enumThreadWindowsCallback = new SafeNativeMethods.EnumThreadWindowsCallback(Application.SendThemeChanged);
					SafeNativeMethods.EnumWindows(enumThreadWindowsCallback, IntPtr.Zero);
					GC.KeepAlive(enumThreadWindowsCallback);
				}
			}
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x00018D28 File Offset: 0x00016F28
		private static bool SendThemeChanged(IntPtr handle, IntPtr extraParameter)
		{
			int currentProcessId = SafeNativeMethods.GetCurrentProcessId();
			int num;
			SafeNativeMethods.GetWindowThreadProcessId(new HandleRef(null, handle), out num);
			if (num == currentProcessId && SafeNativeMethods.IsWindowVisible(new HandleRef(null, handle)))
			{
				Application.SendThemeChangedRecursive(handle, IntPtr.Zero);
				SafeNativeMethods.RedrawWindow(new HandleRef(null, handle), null, NativeMethods.NullHandleRef, 1157);
			}
			return true;
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x00018D81 File Offset: 0x00016F81
		private static bool SendThemeChangedRecursive(IntPtr handle, IntPtr lparam)
		{
			UnsafeNativeMethods.EnumChildWindows(new HandleRef(null, handle), new NativeMethods.EnumChildrenCallback(Application.SendThemeChangedRecursive), NativeMethods.NullHandleRef);
			UnsafeNativeMethods.SendMessage(new HandleRef(null, handle), 794, 0, 0);
			return true;
		}

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x0600091F RID: 2335 RVA: 0x00018DB6 File Offset: 0x00016FB6
		// (remove) Token: 0x06000920 RID: 2336 RVA: 0x00018DC3 File Offset: 0x00016FC3
		public static event EventHandler ApplicationExit
		{
			add
			{
				Application.AddEventHandler(Application.EVENT_APPLICATIONEXIT, value);
			}
			remove
			{
				Application.RemoveEventHandler(Application.EVENT_APPLICATIONEXIT, value);
			}
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x00018DD0 File Offset: 0x00016FD0
		private static void AddEventHandler(object key, Delegate value)
		{
			object obj = Application.internalSyncObject;
			lock (obj)
			{
				if (Application.eventHandlers == null)
				{
					Application.eventHandlers = new EventHandlerList();
				}
				Application.eventHandlers.AddHandler(key, value);
			}
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x00018E28 File Offset: 0x00017028
		private static void RemoveEventHandler(object key, Delegate value)
		{
			object obj = Application.internalSyncObject;
			lock (obj)
			{
				if (Application.eventHandlers != null)
				{
					Application.eventHandlers.RemoveHandler(key, value);
				}
			}
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x00018E78 File Offset: 0x00017078
		public static void AddMessageFilter(IMessageFilter value)
		{
			IntSecurity.UnmanagedCode.Demand();
			Application.ThreadContext.FromCurrent().AddMessageFilter(value);
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x00018E90 File Offset: 0x00017090
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public static bool FilterMessage(ref Message message)
		{
			NativeMethods.MSG msg = default(NativeMethods.MSG);
			msg.hwnd = message.HWnd;
			msg.message = message.Msg;
			msg.wParam = message.WParam;
			msg.lParam = message.LParam;
			bool flag;
			bool result = Application.ThreadContext.FromCurrent().ProcessFilters(ref msg, out flag);
			if (flag)
			{
				message.HWnd = msg.hwnd;
				message.Msg = msg.message;
				message.WParam = msg.wParam;
				message.LParam = msg.lParam;
			}
			return result;
		}

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x06000925 RID: 2341 RVA: 0x00018F1C File Offset: 0x0001711C
		// (remove) Token: 0x06000926 RID: 2342 RVA: 0x00018F78 File Offset: 0x00017178
		public static event EventHandler Idle
		{
			add
			{
				Application.ThreadContext threadContext = Application.ThreadContext.FromCurrent();
				Application.ThreadContext obj = threadContext;
				lock (obj)
				{
					Application.ThreadContext threadContext2 = threadContext;
					threadContext2.idleHandler = (EventHandler)Delegate.Combine(threadContext2.idleHandler, value);
					object componentManager = threadContext.ComponentManager;
				}
			}
			remove
			{
				Application.ThreadContext threadContext = Application.ThreadContext.FromCurrent();
				Application.ThreadContext obj = threadContext;
				lock (obj)
				{
					Application.ThreadContext threadContext2 = threadContext;
					threadContext2.idleHandler = (EventHandler)Delegate.Remove(threadContext2.idleHandler, value);
				}
			}
		}

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x06000927 RID: 2343 RVA: 0x00018FCC File Offset: 0x000171CC
		// (remove) Token: 0x06000928 RID: 2344 RVA: 0x00019020 File Offset: 0x00017220
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static event EventHandler EnterThreadModal
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			add
			{
				Application.ThreadContext threadContext = Application.ThreadContext.FromCurrent();
				Application.ThreadContext obj = threadContext;
				lock (obj)
				{
					Application.ThreadContext threadContext2 = threadContext;
					threadContext2.enterModalHandler = (EventHandler)Delegate.Combine(threadContext2.enterModalHandler, value);
				}
			}
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			remove
			{
				Application.ThreadContext threadContext = Application.ThreadContext.FromCurrent();
				Application.ThreadContext obj = threadContext;
				lock (obj)
				{
					Application.ThreadContext threadContext2 = threadContext;
					threadContext2.enterModalHandler = (EventHandler)Delegate.Remove(threadContext2.enterModalHandler, value);
				}
			}
		}

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x06000929 RID: 2345 RVA: 0x00019074 File Offset: 0x00017274
		// (remove) Token: 0x0600092A RID: 2346 RVA: 0x000190C8 File Offset: 0x000172C8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static event EventHandler LeaveThreadModal
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			add
			{
				Application.ThreadContext threadContext = Application.ThreadContext.FromCurrent();
				Application.ThreadContext obj = threadContext;
				lock (obj)
				{
					Application.ThreadContext threadContext2 = threadContext;
					threadContext2.leaveModalHandler = (EventHandler)Delegate.Combine(threadContext2.leaveModalHandler, value);
				}
			}
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			remove
			{
				Application.ThreadContext threadContext = Application.ThreadContext.FromCurrent();
				Application.ThreadContext obj = threadContext;
				lock (obj)
				{
					Application.ThreadContext threadContext2 = threadContext;
					threadContext2.leaveModalHandler = (EventHandler)Delegate.Remove(threadContext2.leaveModalHandler, value);
				}
			}
		}

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x0600092B RID: 2347 RVA: 0x0001911C File Offset: 0x0001731C
		// (remove) Token: 0x0600092C RID: 2348 RVA: 0x00019168 File Offset: 0x00017368
		public static event ThreadExceptionEventHandler ThreadException
		{
			add
			{
				IntSecurity.AffectThreadBehavior.Demand();
				Application.ThreadContext threadContext = Application.ThreadContext.FromCurrent();
				Application.ThreadContext obj = threadContext;
				lock (obj)
				{
					threadContext.threadExceptionHandler = value;
				}
			}
			remove
			{
				Application.ThreadContext threadContext = Application.ThreadContext.FromCurrent();
				Application.ThreadContext obj = threadContext;
				lock (obj)
				{
					Application.ThreadContext threadContext2 = threadContext;
					threadContext2.threadExceptionHandler = (ThreadExceptionEventHandler)Delegate.Remove(threadContext2.threadExceptionHandler, value);
				}
			}
		}

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x0600092D RID: 2349 RVA: 0x000191BC File Offset: 0x000173BC
		// (remove) Token: 0x0600092E RID: 2350 RVA: 0x000191C9 File Offset: 0x000173C9
		public static event EventHandler ThreadExit
		{
			add
			{
				Application.AddEventHandler(Application.EVENT_THREADEXIT, value);
			}
			remove
			{
				Application.RemoveEventHandler(Application.EVENT_THREADEXIT, value);
			}
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x000191D6 File Offset: 0x000173D6
		internal static void BeginModalMessageLoop()
		{
			Application.ThreadContext.FromCurrent().BeginModalMessageLoop(null);
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x000191E3 File Offset: 0x000173E3
		public static void DoEvents()
		{
			Application.ThreadContext.FromCurrent().RunMessageLoop(2, null);
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x000191F1 File Offset: 0x000173F1
		internal static void DoEventsModal()
		{
			Application.ThreadContext.FromCurrent().RunMessageLoop(-2, null);
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x00019200 File Offset: 0x00017400
		public static void EnableVisualStyles()
		{
			string text = null;
			new FileIOPermission(PermissionState.None)
			{
				AllFiles = FileIOPermissionAccess.PathDiscovery
			}.Assert();
			try
			{
				text = typeof(Application).Assembly.Location;
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			if (text != null)
			{
				Application.EnableVisualStylesInternal(text, 101);
			}
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x0001925C File Offset: 0x0001745C
		private static void EnableVisualStylesInternal(string assemblyFileName, int nativeResourceID)
		{
			Application.useVisualStyles = UnsafeNativeMethods.ThemingScope.CreateActivationContext(assemblyFileName, nativeResourceID);
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x0001926A File Offset: 0x0001746A
		internal static void EndModalMessageLoop()
		{
			Application.ThreadContext.FromCurrent().EndModalMessageLoop(null);
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x00019277 File Offset: 0x00017477
		public static void Exit()
		{
			Application.Exit(null);
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x00019280 File Offset: 0x00017480
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static void Exit(CancelEventArgs e)
		{
			Assembly entryAssembly = Assembly.GetEntryAssembly();
			Assembly callingAssembly = Assembly.GetCallingAssembly();
			if (entryAssembly == null || callingAssembly == null || !entryAssembly.Equals(callingAssembly))
			{
				IntSecurity.AffectThreadBehavior.Demand();
			}
			bool cancel = Application.ExitInternal();
			if (e != null)
			{
				e.Cancel = cancel;
			}
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x000192D0 File Offset: 0x000174D0
		private static bool ExitInternal()
		{
			bool flag = false;
			object obj = Application.internalSyncObject;
			lock (obj)
			{
				if (Application.exiting)
				{
					return false;
				}
				Application.exiting = true;
				try
				{
					if (Application.forms != null)
					{
						foreach (object obj2 in Application.OpenFormsInternal)
						{
							Form form = (Form)obj2;
							if (form.RaiseFormClosingOnAppExit())
							{
								flag = true;
								break;
							}
						}
					}
					if (!flag)
					{
						if (Application.forms != null)
						{
							while (Application.OpenFormsInternal.Count > 0)
							{
								Application.OpenFormsInternal[0].RaiseFormClosedOnAppExit();
							}
						}
						Application.ThreadContext.ExitApplication();
					}
				}
				finally
				{
					Application.exiting = false;
				}
			}
			return flag;
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x000193C0 File Offset: 0x000175C0
		public static void ExitThread()
		{
			IntSecurity.AffectThreadBehavior.Demand();
			Application.ExitThreadInternal();
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x000193D4 File Offset: 0x000175D4
		private static void ExitThreadInternal()
		{
			Application.ThreadContext threadContext = Application.ThreadContext.FromCurrent();
			if (threadContext.ApplicationContext != null)
			{
				threadContext.ApplicationContext.ExitThread();
				return;
			}
			threadContext.Dispose(true);
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x00019402 File Offset: 0x00017602
		internal static void FormActivated(bool modal, bool activated)
		{
			if (modal)
			{
				return;
			}
			Application.ThreadContext.FromCurrent().FormActivated(activated);
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x00019414 File Offset: 0x00017614
		private static FileVersionInfo GetAppFileVersionInfo()
		{
			object obj = Application.internalSyncObject;
			lock (obj)
			{
				if (Application.appFileVersion == null)
				{
					Type appMainType = Application.GetAppMainType();
					if (appMainType != null)
					{
						new FileIOPermission(PermissionState.None)
						{
							AllFiles = (FileIOPermissionAccess.Read | FileIOPermissionAccess.PathDiscovery)
						}.Assert();
						try
						{
							Application.appFileVersion = FileVersionInfo.GetVersionInfo(appMainType.Module.FullyQualifiedName);
							goto IL_73;
						}
						finally
						{
							CodeAccessPermission.RevertAssert();
						}
					}
					Application.appFileVersion = FileVersionInfo.GetVersionInfo(Application.ExecutablePath);
				}
			}
			IL_73:
			return (FileVersionInfo)Application.appFileVersion;
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x000194BC File Offset: 0x000176BC
		private static Type GetAppMainType()
		{
			object obj = Application.internalSyncObject;
			lock (obj)
			{
				if (Application.mainType == null)
				{
					Assembly entryAssembly = Assembly.GetEntryAssembly();
					if (entryAssembly != null)
					{
						Application.mainType = entryAssembly.EntryPoint.ReflectedType;
					}
				}
			}
			return Application.mainType;
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x00019528 File Offset: 0x00017728
		private static Application.ThreadContext GetContextForHandle(HandleRef handle)
		{
			int num;
			int windowThreadProcessId = SafeNativeMethods.GetWindowThreadProcessId(handle, out num);
			return Application.ThreadContext.FromId(windowThreadProcessId);
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x00019548 File Offset: 0x00017748
		private static string GetDataPath(string basePath)
		{
			string format = "{0}\\{1}\\{2}\\{3}";
			string text = Application.CompanyName;
			string text2 = Application.ProductName;
			string text3 = Application.ProductVersion;
			string text4 = string.Format(CultureInfo.CurrentCulture, format, new object[]
			{
				basePath,
				text,
				text2,
				text3
			});
			object obj = Application.internalSyncObject;
			lock (obj)
			{
				if (!Directory.Exists(text4))
				{
					Directory.CreateDirectory(text4);
				}
			}
			return text4;
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x000195D4 File Offset: 0x000177D4
		private static void RaiseExit()
		{
			if (Application.eventHandlers != null)
			{
				Delegate @delegate = Application.eventHandlers[Application.EVENT_APPLICATIONEXIT];
				if (@delegate != null)
				{
					((EventHandler)@delegate)(null, EventArgs.Empty);
				}
			}
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x0001960C File Offset: 0x0001780C
		private static void RaiseThreadExit()
		{
			if (Application.eventHandlers != null)
			{
				Delegate @delegate = Application.eventHandlers[Application.EVENT_THREADEXIT];
				if (@delegate != null)
				{
					((EventHandler)@delegate)(null, EventArgs.Empty);
				}
			}
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x00019644 File Offset: 0x00017844
		internal static void ParkHandle(HandleRef handle, DpiAwarenessContext dpiAwarenessContext = DpiAwarenessContext.DPI_AWARENESS_CONTEXT_UNSPECIFIED)
		{
			Application.ThreadContext contextForHandle = Application.GetContextForHandle(handle);
			if (contextForHandle != null)
			{
				contextForHandle.GetParkingWindow(dpiAwarenessContext).ParkHandle(handle);
			}
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x00019668 File Offset: 0x00017868
		internal static void ParkHandle(CreateParams cp, DpiAwarenessContext dpiAwarenessContext = DpiAwarenessContext.DPI_AWARENESS_CONTEXT_UNSPECIFIED)
		{
			Application.ThreadContext threadContext = Application.ThreadContext.FromCurrent();
			if (threadContext != null)
			{
				cp.Parent = threadContext.GetParkingWindow(dpiAwarenessContext).Handle;
			}
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x00019690 File Offset: 0x00017890
		public static ApartmentState OleRequired()
		{
			return Application.ThreadContext.FromCurrent().OleRequired();
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x0001969C File Offset: 0x0001789C
		public static void OnThreadException(Exception t)
		{
			Application.ThreadContext.FromCurrent().OnThreadException(t);
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x000196AC File Offset: 0x000178AC
		internal static void UnparkHandle(HandleRef handle, DpiAwarenessContext context)
		{
			Application.ThreadContext contextForHandle = Application.GetContextForHandle(handle);
			if (contextForHandle != null)
			{
				contextForHandle.GetParkingWindow(context).UnparkHandle(handle);
			}
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x000196D0 File Offset: 0x000178D0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public static void RaiseIdle(EventArgs e)
		{
			Application.ThreadContext threadContext = Application.ThreadContext.FromCurrent();
			if (threadContext.idleHandler != null)
			{
				threadContext.idleHandler(Thread.CurrentThread, e);
			}
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x000196FC File Offset: 0x000178FC
		public static void RemoveMessageFilter(IMessageFilter value)
		{
			Application.ThreadContext.FromCurrent().RemoveMessageFilter(value);
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x0001970C File Offset: 0x0001790C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public static void Restart()
		{
			if (Assembly.GetEntryAssembly() == null)
			{
				throw new NotSupportedException(SR.GetString("RestartNotSupported"));
			}
			bool flag = false;
			Process currentProcess = Process.GetCurrentProcess();
			if (string.Equals(currentProcess.MainModule.ModuleName, "ieexec.exe", StringComparison.OrdinalIgnoreCase))
			{
				string str = string.Empty;
				new FileIOPermission(PermissionState.Unrestricted).Assert();
				try
				{
					str = Path.GetDirectoryName(typeof(object).Module.FullyQualifiedName);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
				if (string.Equals(str + "\\ieexec.exe", currentProcess.MainModule.FileName, StringComparison.OrdinalIgnoreCase))
				{
					flag = true;
					Application.ExitInternal();
					string text = AppDomain.CurrentDomain.GetData("APP_LAUNCH_URL") as string;
					if (text != null)
					{
						Process.Start(currentProcess.MainModule.FileName, text);
					}
				}
			}
			if (!flag)
			{
				if (ApplicationDeployment.IsNetworkDeployed)
				{
					string updatedApplicationFullName = ApplicationDeployment.CurrentDeployment.UpdatedApplicationFullName;
					uint hostTypeFromMetaData = (uint)Application.ClickOnceUtility.GetHostTypeFromMetaData(updatedApplicationFullName);
					Application.ExitInternal();
					UnsafeNativeMethods.CorLaunchApplication(hostTypeFromMetaData, updatedApplicationFullName, 0, null, 0, null, new UnsafeNativeMethods.PROCESS_INFORMATION());
					return;
				}
				string[] commandLineArgs = Environment.GetCommandLineArgs();
				StringBuilder stringBuilder = new StringBuilder((commandLineArgs.Length - 1) * 16);
				for (int i = 1; i < commandLineArgs.Length - 1; i++)
				{
					stringBuilder.Append('"');
					stringBuilder.Append(commandLineArgs[i]);
					stringBuilder.Append("\" ");
				}
				if (commandLineArgs.Length > 1)
				{
					stringBuilder.Append('"');
					stringBuilder.Append(commandLineArgs[commandLineArgs.Length - 1]);
					stringBuilder.Append('"');
				}
				ProcessStartInfo startInfo = Process.GetCurrentProcess().StartInfo;
				startInfo.FileName = Application.ExecutablePath;
				if (stringBuilder.Length > 0)
				{
					startInfo.Arguments = stringBuilder.ToString();
				}
				Application.ExitInternal();
				Process.Start(startInfo);
			}
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x000198E4 File Offset: 0x00017AE4
		public static void Run()
		{
			Application.ThreadContext.FromCurrent().RunMessageLoop(-1, new ApplicationContext());
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x000198F6 File Offset: 0x00017AF6
		public static void Run(Form mainForm)
		{
			Application.ThreadContext.FromCurrent().RunMessageLoop(-1, new ApplicationContext(mainForm));
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x00019909 File Offset: 0x00017B09
		public static void Run(ApplicationContext context)
		{
			Application.ThreadContext.FromCurrent().RunMessageLoop(-1, context);
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x00019917 File Offset: 0x00017B17
		internal static void RunDialog(Form form)
		{
			Application.ThreadContext.FromCurrent().RunMessageLoop(4, new Application.ModalApplicationContext(form));
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x0001992A File Offset: 0x00017B2A
		public static void SetCompatibleTextRenderingDefault(bool defaultValue)
		{
			if (NativeWindow.AnyHandleCreated)
			{
				throw new InvalidOperationException(SR.GetString("Win32WindowAlreadyCreated"));
			}
			Control.UseCompatibleTextRenderingDefault = defaultValue;
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x00019949 File Offset: 0x00017B49
		public static bool SetSuspendState(PowerState state, bool force, bool disableWakeEvent)
		{
			IntSecurity.AffectMachineState.Demand();
			return UnsafeNativeMethods.SetSuspendState(state == PowerState.Hibernate, force, disableWakeEvent);
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x00019960 File Offset: 0x00017B60
		public static void SetUnhandledExceptionMode(UnhandledExceptionMode mode)
		{
			Application.SetUnhandledExceptionMode(mode, true);
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x00019969 File Offset: 0x00017B69
		public static void SetUnhandledExceptionMode(UnhandledExceptionMode mode, bool threadScope)
		{
			IntSecurity.AffectThreadBehavior.Demand();
			NativeWindow.SetUnhandledExceptionModeInternal(mode, threadScope);
		}

		// Token: 0x040005DA RID: 1498
		private static EventHandlerList eventHandlers;

		// Token: 0x040005DB RID: 1499
		private static string startupPath;

		// Token: 0x040005DC RID: 1500
		private static string executablePath;

		// Token: 0x040005DD RID: 1501
		private static object appFileVersion;

		// Token: 0x040005DE RID: 1502
		private static Type mainType;

		// Token: 0x040005DF RID: 1503
		private static string companyName;

		// Token: 0x040005E0 RID: 1504
		private static string productName;

		// Token: 0x040005E1 RID: 1505
		private static string productVersion;

		// Token: 0x040005E2 RID: 1506
		private static string safeTopLevelCaptionSuffix;

		// Token: 0x040005E3 RID: 1507
		private static bool useVisualStyles = false;

		// Token: 0x040005E4 RID: 1508
		private static bool comCtlSupportsVisualStylesInitialized = false;

		// Token: 0x040005E5 RID: 1509
		private static bool comCtlSupportsVisualStyles = false;

		// Token: 0x040005E6 RID: 1510
		private static FormCollection forms = null;

		// Token: 0x040005E7 RID: 1511
		private static object internalSyncObject = new object();

		// Token: 0x040005E8 RID: 1512
		private static bool useWaitCursor = false;

		// Token: 0x040005E9 RID: 1513
		private static bool useEverettThreadAffinity = false;

		// Token: 0x040005EA RID: 1514
		private static bool checkedThreadAffinity = false;

		// Token: 0x040005EB RID: 1515
		private const string everettThreadAffinityValue = "EnableSystemEventsThreadAffinityCompatibility";

		// Token: 0x040005EC RID: 1516
		private static bool exiting;

		// Token: 0x040005ED RID: 1517
		private static readonly object EVENT_APPLICATIONEXIT = new object();

		// Token: 0x040005EE RID: 1518
		private static readonly object EVENT_THREADEXIT = new object();

		// Token: 0x040005EF RID: 1519
		private const string IEEXEC = "ieexec.exe";

		// Token: 0x040005F0 RID: 1520
		private const string CLICKONCE_APPS_DATADIRECTORY = "DataDirectory";

		// Token: 0x040005F1 RID: 1521
		private static bool parkingWindowSupportsPMAv2 = true;

		// Token: 0x02000600 RID: 1536
		// (Invoke) Token: 0x060061D0 RID: 25040
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public delegate bool MessageLoopCallback();

		// Token: 0x02000601 RID: 1537
		private class ClickOnceUtility
		{
			// Token: 0x060061D3 RID: 25043 RVA: 0x00002843 File Offset: 0x00000A43
			private ClickOnceUtility()
			{
			}

			// Token: 0x060061D4 RID: 25044 RVA: 0x0016969C File Offset: 0x0016789C
			public static Application.ClickOnceUtility.HostType GetHostTypeFromMetaData(string appFullName)
			{
				Application.ClickOnceUtility.HostType result = Application.ClickOnceUtility.HostType.Default;
				try
				{
					IDefinitionAppId appId = IsolationInterop.AppIdAuthority.TextToDefinition(0U, appFullName);
					result = (Application.ClickOnceUtility.GetPropertyBoolean(appId, "IsFullTrust") ? Application.ClickOnceUtility.HostType.CorFlag : Application.ClickOnceUtility.HostType.AppLaunch);
				}
				catch
				{
				}
				return result;
			}

			// Token: 0x060061D5 RID: 25045 RVA: 0x001696E4 File Offset: 0x001678E4
			private static bool GetPropertyBoolean(IDefinitionAppId appId, string propName)
			{
				string propertyString = Application.ClickOnceUtility.GetPropertyString(appId, propName);
				if (string.IsNullOrEmpty(propertyString))
				{
					return false;
				}
				bool result;
				try
				{
					result = Convert.ToBoolean(propertyString, CultureInfo.InvariantCulture);
				}
				catch
				{
					result = false;
				}
				return result;
			}

			// Token: 0x060061D6 RID: 25046 RVA: 0x00169728 File Offset: 0x00167928
			private static string GetPropertyString(IDefinitionAppId appId, string propName)
			{
				byte[] deploymentProperty = IsolationInterop.UserStore.GetDeploymentProperty(Store.GetPackagePropertyFlags.Nothing, appId, Application.ClickOnceUtility.InstallReference, new Guid("2ad613da-6fdb-4671-af9e-18ab2e4df4d8"), propName);
				int num = deploymentProperty.Length;
				if (num == 0 || deploymentProperty.Length % 2 != 0 || deploymentProperty[num - 2] != 0 || deploymentProperty[num - 1] != 0)
				{
					return null;
				}
				return Encoding.Unicode.GetString(deploymentProperty, 0, num - 2);
			}

			// Token: 0x17001501 RID: 5377
			// (get) Token: 0x060061D7 RID: 25047 RVA: 0x0016977F File Offset: 0x0016797F
			private static StoreApplicationReference InstallReference
			{
				get
				{
					return new StoreApplicationReference(IsolationInterop.GUID_SXS_INSTALL_REFERENCE_SCHEME_OPAQUESTRING, "{3f471841-eef2-47d6-89c0-d028f03a4ad5}", null);
				}
			}

			// Token: 0x020008B2 RID: 2226
			public enum HostType
			{
				// Token: 0x04004526 RID: 17702
				Default,
				// Token: 0x04004527 RID: 17703
				AppLaunch,
				// Token: 0x04004528 RID: 17704
				CorFlag
			}
		}

		// Token: 0x02000602 RID: 1538
		private class ComponentManager : UnsafeNativeMethods.IMsoComponentManager
		{
			// Token: 0x17001502 RID: 5378
			// (get) Token: 0x060061D8 RID: 25048 RVA: 0x00169791 File Offset: 0x00167991
			private Hashtable OleComponents
			{
				get
				{
					if (this.oleComponents == null)
					{
						this.oleComponents = new Hashtable();
						this.cookieCounter = 0;
					}
					return this.oleComponents;
				}
			}

			// Token: 0x060061D9 RID: 25049 RVA: 0x001697B3 File Offset: 0x001679B3
			int UnsafeNativeMethods.IMsoComponentManager.QueryService(ref Guid guidService, ref Guid iid, out object ppvObj)
			{
				ppvObj = null;
				return -2147467262;
			}

			// Token: 0x060061DA RID: 25050 RVA: 0x00013062 File Offset: 0x00011262
			bool UnsafeNativeMethods.IMsoComponentManager.FDebugMessage(IntPtr hInst, int msg, IntPtr wparam, IntPtr lparam)
			{
				return true;
			}

			// Token: 0x060061DB RID: 25051 RVA: 0x001697C0 File Offset: 0x001679C0
			bool UnsafeNativeMethods.IMsoComponentManager.FRegisterComponent(UnsafeNativeMethods.IMsoComponent component, NativeMethods.MSOCRINFOSTRUCT pcrinfo, out IntPtr dwComponentID)
			{
				Application.ComponentManager.ComponentHashtableEntry componentHashtableEntry = new Application.ComponentManager.ComponentHashtableEntry();
				componentHashtableEntry.component = component;
				componentHashtableEntry.componentInfo = pcrinfo;
				Hashtable hashtable = this.OleComponents;
				int num = this.cookieCounter + 1;
				this.cookieCounter = num;
				hashtable.Add(num, componentHashtableEntry);
				dwComponentID = (IntPtr)this.cookieCounter;
				return true;
			}

			// Token: 0x060061DC RID: 25052 RVA: 0x00169814 File Offset: 0x00167A14
			bool UnsafeNativeMethods.IMsoComponentManager.FRevokeComponent(IntPtr dwComponentID)
			{
				int num = (int)((long)dwComponentID);
				Application.ComponentManager.ComponentHashtableEntry componentHashtableEntry = (Application.ComponentManager.ComponentHashtableEntry)this.OleComponents[num];
				if (componentHashtableEntry == null)
				{
					return false;
				}
				if (componentHashtableEntry.component == this.activeComponent)
				{
					this.activeComponent = null;
				}
				if (componentHashtableEntry.component == this.trackingComponent)
				{
					this.trackingComponent = null;
				}
				this.OleComponents.Remove(num);
				return true;
			}

			// Token: 0x060061DD RID: 25053 RVA: 0x00169884 File Offset: 0x00167A84
			bool UnsafeNativeMethods.IMsoComponentManager.FUpdateComponentRegistration(IntPtr dwComponentID, NativeMethods.MSOCRINFOSTRUCT info)
			{
				int num = (int)((long)dwComponentID);
				Application.ComponentManager.ComponentHashtableEntry componentHashtableEntry = (Application.ComponentManager.ComponentHashtableEntry)this.OleComponents[num];
				if (componentHashtableEntry == null)
				{
					return false;
				}
				componentHashtableEntry.componentInfo = info;
				return true;
			}

			// Token: 0x060061DE RID: 25054 RVA: 0x001698C0 File Offset: 0x00167AC0
			bool UnsafeNativeMethods.IMsoComponentManager.FOnComponentActivate(IntPtr dwComponentID)
			{
				int num = (int)((long)dwComponentID);
				Application.ComponentManager.ComponentHashtableEntry componentHashtableEntry = (Application.ComponentManager.ComponentHashtableEntry)this.OleComponents[num];
				if (componentHashtableEntry == null)
				{
					return false;
				}
				this.activeComponent = componentHashtableEntry.component;
				return true;
			}

			// Token: 0x060061DF RID: 25055 RVA: 0x00169900 File Offset: 0x00167B00
			bool UnsafeNativeMethods.IMsoComponentManager.FSetTrackingComponent(IntPtr dwComponentID, bool fTrack)
			{
				int num = (int)((long)dwComponentID);
				Application.ComponentManager.ComponentHashtableEntry componentHashtableEntry = (Application.ComponentManager.ComponentHashtableEntry)this.OleComponents[num];
				if (componentHashtableEntry == null)
				{
					return false;
				}
				if (componentHashtableEntry.component == this.trackingComponent ^ fTrack)
				{
					return false;
				}
				if (fTrack)
				{
					this.trackingComponent = componentHashtableEntry.component;
				}
				else
				{
					this.trackingComponent = null;
				}
				return true;
			}

			// Token: 0x060061E0 RID: 25056 RVA: 0x00169960 File Offset: 0x00167B60
			void UnsafeNativeMethods.IMsoComponentManager.OnComponentEnterState(IntPtr dwComponentID, int uStateID, int uContext, int cpicmExclude, int rgpicmExclude, int dwReserved)
			{
				int num = (int)((long)dwComponentID);
				this.currentState |= uStateID;
				if (uContext == 0 || uContext == 1)
				{
					foreach (object obj in this.OleComponents.Values)
					{
						Application.ComponentManager.ComponentHashtableEntry componentHashtableEntry = (Application.ComponentManager.ComponentHashtableEntry)obj;
						componentHashtableEntry.component.OnEnterState(uStateID, true);
					}
				}
			}

			// Token: 0x060061E1 RID: 25057 RVA: 0x001699E4 File Offset: 0x00167BE4
			bool UnsafeNativeMethods.IMsoComponentManager.FOnComponentExitState(IntPtr dwComponentID, int uStateID, int uContext, int cpicmExclude, int rgpicmExclude)
			{
				int num = (int)((long)dwComponentID);
				this.currentState &= ~uStateID;
				if (uContext == 0 || uContext == 1)
				{
					foreach (object obj in this.OleComponents.Values)
					{
						Application.ComponentManager.ComponentHashtableEntry componentHashtableEntry = (Application.ComponentManager.ComponentHashtableEntry)obj;
						componentHashtableEntry.component.OnEnterState(uStateID, false);
					}
				}
				return false;
			}

			// Token: 0x060061E2 RID: 25058 RVA: 0x00169A68 File Offset: 0x00167C68
			bool UnsafeNativeMethods.IMsoComponentManager.FInState(int uStateID, IntPtr pvoid)
			{
				return (this.currentState & uStateID) != 0;
			}

			// Token: 0x060061E3 RID: 25059 RVA: 0x00169A78 File Offset: 0x00167C78
			bool UnsafeNativeMethods.IMsoComponentManager.FContinueIdle()
			{
				NativeMethods.MSG msg = default(NativeMethods.MSG);
				return !UnsafeNativeMethods.PeekMessage(ref msg, NativeMethods.NullHandleRef, 0, 0, 0);
			}

			// Token: 0x060061E4 RID: 25060 RVA: 0x00169AA0 File Offset: 0x00167CA0
			bool UnsafeNativeMethods.IMsoComponentManager.FPushMessageLoop(IntPtr dwComponentID, int reason, int pvLoopData)
			{
				int num = (int)((long)dwComponentID);
				int num2 = this.currentState;
				bool flag = true;
				if (!this.OleComponents.ContainsKey(num))
				{
					return false;
				}
				UnsafeNativeMethods.IMsoComponent msoComponent = this.activeComponent;
				try
				{
					NativeMethods.MSG msg = default(NativeMethods.MSG);
					NativeMethods.MSG[] array = new NativeMethods.MSG[]
					{
						msg
					};
					Application.ComponentManager.ComponentHashtableEntry componentHashtableEntry = (Application.ComponentManager.ComponentHashtableEntry)this.OleComponents[num];
					if (componentHashtableEntry == null)
					{
						return false;
					}
					UnsafeNativeMethods.IMsoComponent component = componentHashtableEntry.component;
					this.activeComponent = component;
					while (flag)
					{
						UnsafeNativeMethods.IMsoComponent msoComponent2;
						if (this.trackingComponent != null)
						{
							msoComponent2 = this.trackingComponent;
						}
						else if (this.activeComponent != null)
						{
							msoComponent2 = this.activeComponent;
						}
						else
						{
							msoComponent2 = component;
						}
						bool flag2 = UnsafeNativeMethods.PeekMessage(ref msg, NativeMethods.NullHandleRef, 0, 0, 0);
						if (flag2)
						{
							array[0] = msg;
							flag = msoComponent2.FContinueMessageLoop(reason, pvLoopData, array);
							if (flag)
							{
								bool flag3;
								if (msg.hwnd != IntPtr.Zero && SafeNativeMethods.IsWindowUnicode(new HandleRef(null, msg.hwnd)))
								{
									flag3 = true;
									UnsafeNativeMethods.GetMessageW(ref msg, NativeMethods.NullHandleRef, 0, 0);
								}
								else
								{
									flag3 = false;
									UnsafeNativeMethods.GetMessageA(ref msg, NativeMethods.NullHandleRef, 0, 0);
								}
								if (msg.message == 18)
								{
									Application.ThreadContext.FromCurrent().DisposeThreadWindows();
									if (reason != -1)
									{
										UnsafeNativeMethods.PostQuitMessage((int)msg.wParam);
									}
									flag = false;
									break;
								}
								if (!msoComponent2.FPreTranslateMessage(ref msg))
								{
									UnsafeNativeMethods.TranslateMessage(ref msg);
									if (flag3)
									{
										UnsafeNativeMethods.DispatchMessageW(ref msg);
									}
									else
									{
										UnsafeNativeMethods.DispatchMessageA(ref msg);
									}
								}
							}
						}
						else
						{
							if (reason == 2)
							{
								break;
							}
							if (reason == -2)
							{
								break;
							}
							bool flag4 = false;
							if (this.OleComponents != null)
							{
								foreach (object obj in this.OleComponents.Values)
								{
									Application.ComponentManager.ComponentHashtableEntry componentHashtableEntry2 = (Application.ComponentManager.ComponentHashtableEntry)obj;
									flag4 |= componentHashtableEntry2.component.FDoIdle(-1);
								}
							}
							flag = msoComponent2.FContinueMessageLoop(reason, pvLoopData, null);
							if (flag)
							{
								if (flag4)
								{
									UnsafeNativeMethods.MsgWaitForMultipleObjectsEx(0, IntPtr.Zero, 100, 255, 4);
								}
								else if (!UnsafeNativeMethods.PeekMessage(ref msg, NativeMethods.NullHandleRef, 0, 0, 0))
								{
									UnsafeNativeMethods.WaitMessage();
								}
							}
						}
					}
				}
				finally
				{
					this.currentState = num2;
					this.activeComponent = msoComponent;
				}
				return !flag;
			}

			// Token: 0x060061E5 RID: 25061 RVA: 0x00169D10 File Offset: 0x00167F10
			bool UnsafeNativeMethods.IMsoComponentManager.FCreateSubComponentManager(object punkOuter, object punkServProv, ref Guid riid, out IntPtr ppvObj)
			{
				ppvObj = IntPtr.Zero;
				return false;
			}

			// Token: 0x060061E6 RID: 25062 RVA: 0x00169D1B File Offset: 0x00167F1B
			bool UnsafeNativeMethods.IMsoComponentManager.FGetParentComponentManager(out UnsafeNativeMethods.IMsoComponentManager ppicm)
			{
				ppicm = null;
				return false;
			}

			// Token: 0x060061E7 RID: 25063 RVA: 0x00169D24 File Offset: 0x00167F24
			bool UnsafeNativeMethods.IMsoComponentManager.FGetActiveComponent(int dwgac, UnsafeNativeMethods.IMsoComponent[] ppic, NativeMethods.MSOCRINFOSTRUCT info, int dwReserved)
			{
				UnsafeNativeMethods.IMsoComponent msoComponent = null;
				if (dwgac == 0)
				{
					msoComponent = this.activeComponent;
				}
				else if (dwgac == 1)
				{
					msoComponent = this.trackingComponent;
				}
				else if (dwgac == 2)
				{
					if (this.trackingComponent != null)
					{
						msoComponent = this.trackingComponent;
					}
					else
					{
						msoComponent = this.activeComponent;
					}
				}
				if (ppic != null)
				{
					ppic[0] = msoComponent;
				}
				if (info != null && msoComponent != null)
				{
					foreach (object obj in this.OleComponents.Values)
					{
						Application.ComponentManager.ComponentHashtableEntry componentHashtableEntry = (Application.ComponentManager.ComponentHashtableEntry)obj;
						if (componentHashtableEntry.component == msoComponent)
						{
							info = componentHashtableEntry.componentInfo;
							break;
						}
					}
				}
				return msoComponent != null;
			}

			// Token: 0x040038A9 RID: 14505
			private Hashtable oleComponents;

			// Token: 0x040038AA RID: 14506
			private int cookieCounter;

			// Token: 0x040038AB RID: 14507
			private UnsafeNativeMethods.IMsoComponent activeComponent;

			// Token: 0x040038AC RID: 14508
			private UnsafeNativeMethods.IMsoComponent trackingComponent;

			// Token: 0x040038AD RID: 14509
			private int currentState;

			// Token: 0x020008B3 RID: 2227
			private class ComponentHashtableEntry
			{
				// Token: 0x04004529 RID: 17705
				public UnsafeNativeMethods.IMsoComponent component;

				// Token: 0x0400452A RID: 17706
				public NativeMethods.MSOCRINFOSTRUCT componentInfo;
			}
		}

		// Token: 0x02000603 RID: 1539
		internal sealed class ThreadContext : MarshalByRefObject, UnsafeNativeMethods.IMsoComponent
		{
			// Token: 0x060061E9 RID: 25065 RVA: 0x00169DD8 File Offset: 0x00167FD8
			public ThreadContext()
			{
				IntPtr zero = IntPtr.Zero;
				UnsafeNativeMethods.DuplicateHandle(new HandleRef(null, SafeNativeMethods.GetCurrentProcess()), new HandleRef(null, SafeNativeMethods.GetCurrentThread()), new HandleRef(null, SafeNativeMethods.GetCurrentProcess()), ref zero, 0, false, 2);
				this.handle = zero;
				this.id = SafeNativeMethods.GetCurrentThreadId();
				this.messageLoopCount = 0;
				Application.ThreadContext.currentThreadContext = this;
				Application.ThreadContext.contextHash[this.id] = this;
			}

			// Token: 0x17001503 RID: 5379
			// (get) Token: 0x060061EA RID: 25066 RVA: 0x00169E64 File Offset: 0x00168064
			public ApplicationContext ApplicationContext
			{
				get
				{
					return this.applicationContext;
				}
			}

			// Token: 0x17001504 RID: 5380
			// (get) Token: 0x060061EB RID: 25067 RVA: 0x00169E6C File Offset: 0x0016806C
			internal UnsafeNativeMethods.IMsoComponentManager ComponentManager
			{
				get
				{
					if (this.componentManager == null)
					{
						if (this.fetchingComponentManager)
						{
							return null;
						}
						this.fetchingComponentManager = true;
						try
						{
							UnsafeNativeMethods.IMsoComponentManager msoComponentManager = null;
							Application.OleRequired();
							IntPtr intPtr = (IntPtr)0;
							if (NativeMethods.Succeeded(UnsafeNativeMethods.CoRegisterMessageFilter(NativeMethods.NullHandleRef, ref intPtr)) && intPtr != (IntPtr)0)
							{
								IntPtr intPtr2 = (IntPtr)0;
								UnsafeNativeMethods.CoRegisterMessageFilter(new HandleRef(null, intPtr), ref intPtr2);
								object obj = Marshal.GetObjectForIUnknown(intPtr);
								Marshal.Release(intPtr);
								UnsafeNativeMethods.IOleServiceProvider oleServiceProvider = obj as UnsafeNativeMethods.IOleServiceProvider;
								if (oleServiceProvider != null)
								{
									try
									{
										IntPtr zero = IntPtr.Zero;
										Guid guid = new Guid("000C060B-0000-0000-C000-000000000046");
										Guid guid2 = new Guid("{000C0601-0000-0000-C000-000000000046}");
										int hr = oleServiceProvider.QueryService(ref guid, ref guid2, out zero);
										if (NativeMethods.Succeeded(hr) && zero != IntPtr.Zero)
										{
											IntPtr intPtr3;
											try
											{
												Guid guid3 = typeof(UnsafeNativeMethods.IMsoComponentManager).GUID;
												hr = Marshal.QueryInterface(zero, ref guid3, out intPtr3);
											}
											finally
											{
												Marshal.Release(zero);
											}
											if (NativeMethods.Succeeded(hr) && intPtr3 != IntPtr.Zero)
											{
												try
												{
													msoComponentManager = ComponentManagerBroker.GetComponentManager(intPtr3);
												}
												finally
												{
													Marshal.Release(intPtr3);
												}
											}
											if (msoComponentManager != null)
											{
												if (intPtr == zero)
												{
													obj = null;
												}
												this.externalComponentManager = true;
												AppDomain.CurrentDomain.DomainUnload += this.OnDomainUnload;
												AppDomain.CurrentDomain.ProcessExit += this.OnDomainUnload;
											}
										}
									}
									catch
									{
									}
								}
								if (obj != null && Marshal.IsComObject(obj))
								{
									Marshal.ReleaseComObject(obj);
								}
							}
							if (msoComponentManager == null)
							{
								msoComponentManager = new Application.ComponentManager();
								this.externalComponentManager = false;
							}
							if (msoComponentManager != null && this.componentID == -1)
							{
								IntPtr value;
								bool flag = msoComponentManager.FRegisterComponent(this, new NativeMethods.MSOCRINFOSTRUCT
								{
									cbSize = Marshal.SizeOf(typeof(NativeMethods.MSOCRINFOSTRUCT)),
									uIdleTimeInterval = 0,
									grfcrf = 9,
									grfcadvf = 1
								}, out value);
								this.componentID = (int)((long)value);
								if (flag && !(msoComponentManager is Application.ComponentManager))
								{
									this.messageLoopCount++;
								}
								this.componentManager = msoComponentManager;
							}
						}
						finally
						{
							this.fetchingComponentManager = false;
						}
					}
					return this.componentManager;
				}
			}

			// Token: 0x17001505 RID: 5381
			// (get) Token: 0x060061EC RID: 25068 RVA: 0x0016A0F8 File Offset: 0x001682F8
			internal bool CustomThreadExceptionHandlerAttached
			{
				get
				{
					return this.threadExceptionHandler != null;
				}
			}

			// Token: 0x060061ED RID: 25069 RVA: 0x0016A104 File Offset: 0x00168304
			internal Application.ParkingWindow GetParkingWindow(DpiAwarenessContext context)
			{
				Application.ParkingWindow result;
				lock (this)
				{
					Application.ParkingWindow parkingWindow = this.GetParkingWindowForContext(context);
					if (parkingWindow == null)
					{
						IntSecurity.ManipulateWndProcAndHandles.Assert();
						try
						{
							using (DpiHelper.EnterDpiAwarenessScope(context))
							{
								parkingWindow = new Application.ParkingWindow();
							}
							this.parkingWindows.Add(parkingWindow);
						}
						finally
						{
							CodeAccessPermission.RevertAssert();
						}
					}
					result = parkingWindow;
				}
				return result;
			}

			// Token: 0x060061EE RID: 25070 RVA: 0x0016A198 File Offset: 0x00168398
			internal Application.ParkingWindow GetParkingWindowForContext(DpiAwarenessContext context)
			{
				if (this.parkingWindows.Count == 0)
				{
					return null;
				}
				if (!DpiHelper.EnableDpiChangedHighDpiImprovements || CommonUnsafeNativeMethods.TryFindDpiAwarenessContextsEqual(context, DpiAwarenessContext.DPI_AWARENESS_CONTEXT_UNSPECIFIED))
				{
					return this.parkingWindows[0];
				}
				foreach (Application.ParkingWindow parkingWindow in this.parkingWindows)
				{
					if (CommonUnsafeNativeMethods.TryFindDpiAwarenessContextsEqual(parkingWindow.DpiAwarenessContext, context))
					{
						return parkingWindow;
					}
				}
				return null;
			}

			// Token: 0x17001506 RID: 5382
			// (get) Token: 0x060061EF RID: 25071 RVA: 0x0016A228 File Offset: 0x00168428
			// (set) Token: 0x060061F0 RID: 25072 RVA: 0x0016A251 File Offset: 0x00168451
			internal Control ActivatingControl
			{
				get
				{
					if (this.activatingControlRef != null && this.activatingControlRef.IsAlive)
					{
						return this.activatingControlRef.Target as Control;
					}
					return null;
				}
				set
				{
					if (value != null)
					{
						this.activatingControlRef = new WeakReference(value);
						return;
					}
					this.activatingControlRef = null;
				}
			}

			// Token: 0x17001507 RID: 5383
			// (get) Token: 0x060061F1 RID: 25073 RVA: 0x0016A26C File Offset: 0x0016846C
			internal Control MarshalingControl
			{
				get
				{
					Control result;
					lock (this)
					{
						if (this.marshalingControl == null)
						{
							this.marshalingControl = new Application.MarshalingControl();
						}
						result = this.marshalingControl;
					}
					return result;
				}
			}

			// Token: 0x060061F2 RID: 25074 RVA: 0x0016A2BC File Offset: 0x001684BC
			internal void AddMessageFilter(IMessageFilter f)
			{
				if (this.messageFilters == null)
				{
					this.messageFilters = new ArrayList();
				}
				if (f != null)
				{
					this.SetState(16, false);
					if (this.messageFilters.Count > 0 && f is IMessageModifyAndFilter)
					{
						this.messageFilters.Insert(0, f);
						return;
					}
					this.messageFilters.Add(f);
				}
			}

			// Token: 0x060061F3 RID: 25075 RVA: 0x0016A31C File Offset: 0x0016851C
			internal void BeginModalMessageLoop(ApplicationContext context)
			{
				bool flag = this.ourModalLoop;
				this.ourModalLoop = true;
				try
				{
					UnsafeNativeMethods.IMsoComponentManager msoComponentManager = this.ComponentManager;
					if (msoComponentManager != null)
					{
						msoComponentManager.OnComponentEnterState((IntPtr)this.componentID, 1, 0, 0, 0, 0);
					}
				}
				finally
				{
					this.ourModalLoop = flag;
				}
				this.DisableWindowsForModalLoop(false, context);
				this.modalCount++;
				if (this.enterModalHandler != null && this.modalCount == 1)
				{
					this.enterModalHandler(Thread.CurrentThread, EventArgs.Empty);
				}
			}

			// Token: 0x060061F4 RID: 25076 RVA: 0x0016A3B0 File Offset: 0x001685B0
			internal void DisableWindowsForModalLoop(bool onlyWinForms, ApplicationContext context)
			{
				Application.ThreadWindows previousThreadWindows = this.threadWindows;
				this.threadWindows = new Application.ThreadWindows(onlyWinForms);
				this.threadWindows.Enable(false);
				this.threadWindows.previousThreadWindows = previousThreadWindows;
				Application.ModalApplicationContext modalApplicationContext = context as Application.ModalApplicationContext;
				if (modalApplicationContext != null)
				{
					modalApplicationContext.DisableThreadWindows(true, onlyWinForms);
				}
			}

			// Token: 0x060061F5 RID: 25077 RVA: 0x0016A3FC File Offset: 0x001685FC
			internal void Dispose(bool postQuit)
			{
				lock (this)
				{
					try
					{
						int num = this.disposeCount;
						this.disposeCount = num + 1;
						if (num == 0)
						{
							if (this.messageLoopCount > 0 && postQuit)
							{
								this.PostQuit();
							}
							else
							{
								bool flag2 = SafeNativeMethods.GetCurrentThreadId() == this.id;
								try
								{
									if (flag2)
									{
										if (this.componentManager != null)
										{
											this.RevokeComponent();
										}
										this.DisposeThreadWindows();
										try
										{
											Application.RaiseThreadExit();
										}
										finally
										{
											if (this.GetState(1) && !this.GetState(2))
											{
												this.SetState(1, false);
												UnsafeNativeMethods.OleUninitialize();
											}
										}
									}
								}
								finally
								{
									if (this.handle != IntPtr.Zero)
									{
										UnsafeNativeMethods.CloseHandle(new HandleRef(this, this.handle));
										this.handle = IntPtr.Zero;
									}
									try
									{
										if (Application.ThreadContext.totalMessageLoopCount == 0)
										{
											Application.RaiseExit();
										}
									}
									finally
									{
										object obj = Application.ThreadContext.tcInternalSyncObject;
										lock (obj)
										{
											Application.ThreadContext.contextHash.Remove(this.id);
										}
										if (Application.ThreadContext.currentThreadContext == this)
										{
											Application.ThreadContext.currentThreadContext = null;
										}
									}
								}
							}
							GC.SuppressFinalize(this);
						}
					}
					finally
					{
						this.disposeCount--;
					}
				}
			}

			// Token: 0x060061F6 RID: 25078 RVA: 0x0016A5D4 File Offset: 0x001687D4
			private void DisposeParkingWindow()
			{
				if (this.parkingWindows.Count != 0)
				{
					int num;
					int windowThreadProcessId = SafeNativeMethods.GetWindowThreadProcessId(new HandleRef(this.parkingWindows[0], this.parkingWindows[0].Handle), out num);
					int currentThreadId = SafeNativeMethods.GetCurrentThreadId();
					for (int i = 0; i < this.parkingWindows.Count; i++)
					{
						if (windowThreadProcessId == currentThreadId)
						{
							this.parkingWindows[i].Destroy();
						}
						else
						{
							this.parkingWindows[i] = null;
						}
					}
					this.parkingWindows.Clear();
				}
			}

			// Token: 0x060061F7 RID: 25079 RVA: 0x0016A664 File Offset: 0x00168864
			internal void DisposeThreadWindows()
			{
				try
				{
					if (this.applicationContext != null)
					{
						this.applicationContext.Dispose();
						this.applicationContext = null;
					}
					Application.ThreadWindows threadWindows = new Application.ThreadWindows(true);
					threadWindows.Dispose();
					this.DisposeParkingWindow();
				}
				catch
				{
				}
			}

			// Token: 0x060061F8 RID: 25080 RVA: 0x0016A6B4 File Offset: 0x001688B4
			internal void EnableWindowsForModalLoop(bool onlyWinForms, ApplicationContext context)
			{
				if (this.threadWindows != null)
				{
					this.threadWindows.Enable(true);
					this.threadWindows = this.threadWindows.previousThreadWindows;
				}
				Application.ModalApplicationContext modalApplicationContext = context as Application.ModalApplicationContext;
				if (modalApplicationContext != null)
				{
					modalApplicationContext.DisableThreadWindows(false, onlyWinForms);
				}
			}

			// Token: 0x060061F9 RID: 25081 RVA: 0x0016A6F8 File Offset: 0x001688F8
			internal void EndModalMessageLoop(ApplicationContext context)
			{
				this.EnableWindowsForModalLoop(false, context);
				bool flag = this.ourModalLoop;
				this.ourModalLoop = true;
				try
				{
					UnsafeNativeMethods.IMsoComponentManager msoComponentManager = this.ComponentManager;
					if (msoComponentManager != null)
					{
						msoComponentManager.FOnComponentExitState((IntPtr)this.componentID, 1, 0, 0, 0);
					}
				}
				finally
				{
					this.ourModalLoop = flag;
				}
				this.modalCount--;
				if (this.leaveModalHandler != null && this.modalCount == 0)
				{
					this.leaveModalHandler(Thread.CurrentThread, EventArgs.Empty);
				}
			}

			// Token: 0x060061FA RID: 25082 RVA: 0x0016A788 File Offset: 0x00168988
			internal static void ExitApplication()
			{
				Application.ThreadContext.ExitCommon(true);
			}

			// Token: 0x060061FB RID: 25083 RVA: 0x0016A790 File Offset: 0x00168990
			private static void ExitCommon(bool disposing)
			{
				object obj = Application.ThreadContext.tcInternalSyncObject;
				lock (obj)
				{
					if (Application.ThreadContext.contextHash != null)
					{
						Application.ThreadContext[] array = new Application.ThreadContext[Application.ThreadContext.contextHash.Values.Count];
						Application.ThreadContext.contextHash.Values.CopyTo(array, 0);
						for (int i = 0; i < array.Length; i++)
						{
							if (array[i].ApplicationContext != null)
							{
								array[i].ApplicationContext.ExitThread();
							}
							else
							{
								array[i].Dispose(disposing);
							}
						}
					}
				}
			}

			// Token: 0x060061FC RID: 25084 RVA: 0x0016A828 File Offset: 0x00168A28
			internal static void ExitDomain()
			{
				Application.ThreadContext.ExitCommon(false);
			}

			// Token: 0x060061FD RID: 25085 RVA: 0x0016A830 File Offset: 0x00168A30
			~ThreadContext()
			{
				if (this.handle != IntPtr.Zero)
				{
					UnsafeNativeMethods.CloseHandle(new HandleRef(this, this.handle));
					this.handle = IntPtr.Zero;
				}
			}

			// Token: 0x060061FE RID: 25086 RVA: 0x0016A888 File Offset: 0x00168A88
			internal void FormActivated(bool activate)
			{
				if (activate)
				{
					UnsafeNativeMethods.IMsoComponentManager msoComponentManager = this.ComponentManager;
					if (msoComponentManager != null && !(msoComponentManager is Application.ComponentManager))
					{
						msoComponentManager.FOnComponentActivate((IntPtr)this.componentID);
					}
				}
			}

			// Token: 0x060061FF RID: 25087 RVA: 0x0016A8BC File Offset: 0x00168ABC
			internal void TrackInput(bool track)
			{
				if (track != this.GetState(32))
				{
					UnsafeNativeMethods.IMsoComponentManager msoComponentManager = this.ComponentManager;
					if (msoComponentManager != null && !(msoComponentManager is Application.ComponentManager))
					{
						msoComponentManager.FSetTrackingComponent((IntPtr)this.componentID, track);
						this.SetState(32, track);
					}
				}
			}

			// Token: 0x06006200 RID: 25088 RVA: 0x0016A904 File Offset: 0x00168B04
			internal static Application.ThreadContext FromCurrent()
			{
				Application.ThreadContext threadContext = Application.ThreadContext.currentThreadContext;
				if (threadContext == null)
				{
					threadContext = new Application.ThreadContext();
				}
				return threadContext;
			}

			// Token: 0x06006201 RID: 25089 RVA: 0x0016A924 File Offset: 0x00168B24
			internal static Application.ThreadContext FromId(int id)
			{
				Application.ThreadContext threadContext = (Application.ThreadContext)Application.ThreadContext.contextHash[id];
				if (threadContext == null && id == SafeNativeMethods.GetCurrentThreadId())
				{
					threadContext = new Application.ThreadContext();
				}
				return threadContext;
			}

			// Token: 0x06006202 RID: 25090 RVA: 0x0016A959 File Offset: 0x00168B59
			internal bool GetAllowQuit()
			{
				return Application.ThreadContext.totalMessageLoopCount > 0 && Application.ThreadContext.baseLoopReason == -1;
			}

			// Token: 0x06006203 RID: 25091 RVA: 0x0016A96D File Offset: 0x00168B6D
			internal IntPtr GetHandle()
			{
				return this.handle;
			}

			// Token: 0x06006204 RID: 25092 RVA: 0x0016A975 File Offset: 0x00168B75
			internal int GetId()
			{
				return this.id;
			}

			// Token: 0x06006205 RID: 25093 RVA: 0x0016A97D File Offset: 0x00168B7D
			internal CultureInfo GetCulture()
			{
				if (this.culture == null || this.culture.LCID != SafeNativeMethods.GetThreadLocale())
				{
					this.culture = new CultureInfo(SafeNativeMethods.GetThreadLocale());
				}
				return this.culture;
			}

			// Token: 0x06006206 RID: 25094 RVA: 0x0016A9AF File Offset: 0x00168BAF
			internal bool GetMessageLoop()
			{
				return this.GetMessageLoop(false);
			}

			// Token: 0x06006207 RID: 25095 RVA: 0x0016A9B8 File Offset: 0x00168BB8
			internal bool GetMessageLoop(bool mustBeActive)
			{
				if (this.messageLoopCount > ((mustBeActive && this.externalComponentManager) ? 1 : 0))
				{
					return true;
				}
				if (this.ComponentManager != null && this.externalComponentManager)
				{
					if (!mustBeActive)
					{
						return true;
					}
					UnsafeNativeMethods.IMsoComponent[] array = new UnsafeNativeMethods.IMsoComponent[1];
					if (this.ComponentManager.FGetActiveComponent(0, array, null, 0) && array[0] == this)
					{
						return true;
					}
				}
				Application.MessageLoopCallback messageLoopCallback = this.messageLoopCallback;
				return messageLoopCallback != null && messageLoopCallback();
			}

			// Token: 0x06006208 RID: 25096 RVA: 0x0016AA25 File Offset: 0x00168C25
			private bool GetState(int bit)
			{
				return (this.threadState & bit) != 0;
			}

			// Token: 0x06006209 RID: 25097 RVA: 0x00015ECC File Offset: 0x000140CC
			public override object InitializeLifetimeService()
			{
				return null;
			}

			// Token: 0x0600620A RID: 25098 RVA: 0x0016AA32 File Offset: 0x00168C32
			internal bool IsValidComponentId()
			{
				return this.componentID != -1;
			}

			// Token: 0x0600620B RID: 25099 RVA: 0x0016AA40 File Offset: 0x00168C40
			internal ApartmentState OleRequired()
			{
				Thread currentThread = Thread.CurrentThread;
				if (!this.GetState(1))
				{
					int num = UnsafeNativeMethods.OleInitialize();
					this.SetState(1, true);
					if (num == -2147417850)
					{
						this.SetState(2, true);
					}
				}
				if (this.GetState(2))
				{
					return ApartmentState.MTA;
				}
				return ApartmentState.STA;
			}

			// Token: 0x0600620C RID: 25100 RVA: 0x0016AA86 File Offset: 0x00168C86
			private void OnAppThreadExit(object sender, EventArgs e)
			{
				this.Dispose(true);
			}

			// Token: 0x0600620D RID: 25101 RVA: 0x0016AA8F File Offset: 0x00168C8F
			[PrePrepareMethod]
			private void OnDomainUnload(object sender, EventArgs e)
			{
				this.RevokeComponent();
				Application.ThreadContext.ExitDomain();
			}

			// Token: 0x0600620E RID: 25102 RVA: 0x0016AA9C File Offset: 0x00168C9C
			internal void OnThreadException(Exception t)
			{
				if (this.GetState(4))
				{
					return;
				}
				this.SetState(4, true);
				try
				{
					if (this.threadExceptionHandler != null)
					{
						this.threadExceptionHandler(Thread.CurrentThread, new ThreadExceptionEventArgs(t));
					}
					else if (SystemInformation.UserInteractive)
					{
						ThreadExceptionDialog threadExceptionDialog = new ThreadExceptionDialog(t);
						DialogResult dialogResult = DialogResult.OK;
						IntSecurity.ModifyFocus.Assert();
						try
						{
							dialogResult = threadExceptionDialog.ShowDialog();
						}
						finally
						{
							CodeAccessPermission.RevertAssert();
							threadExceptionDialog.Dispose();
						}
						if (dialogResult != DialogResult.Abort)
						{
							if (dialogResult == DialogResult.Yes)
							{
								WarningException ex = t as WarningException;
								if (ex != null)
								{
									Help.ShowHelp(null, ex.HelpUrl, ex.HelpTopic);
								}
							}
						}
						else
						{
							Application.ExitInternal();
							new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
							Environment.Exit(0);
						}
					}
				}
				finally
				{
					this.SetState(4, false);
				}
			}

			// Token: 0x0600620F RID: 25103 RVA: 0x0016AB70 File Offset: 0x00168D70
			internal void PostQuit()
			{
				UnsafeNativeMethods.PostThreadMessage(this.id, 18, IntPtr.Zero, IntPtr.Zero);
				this.SetState(8, true);
			}

			// Token: 0x06006210 RID: 25104 RVA: 0x0016AB92 File Offset: 0x00168D92
			internal void RegisterMessageLoop(Application.MessageLoopCallback callback)
			{
				this.messageLoopCallback = callback;
			}

			// Token: 0x06006211 RID: 25105 RVA: 0x0016AB9B File Offset: 0x00168D9B
			internal void RemoveMessageFilter(IMessageFilter f)
			{
				if (this.messageFilters != null)
				{
					this.SetState(16, false);
					this.messageFilters.Remove(f);
				}
			}

			// Token: 0x06006212 RID: 25106 RVA: 0x0016ABBC File Offset: 0x00168DBC
			internal void RunMessageLoop(int reason, ApplicationContext context)
			{
				IntPtr userCookie = IntPtr.Zero;
				if (Application.useVisualStyles)
				{
					userCookie = UnsafeNativeMethods.ThemingScope.Activate();
				}
				try
				{
					this.RunMessageLoopInner(reason, context);
				}
				finally
				{
					UnsafeNativeMethods.ThemingScope.Deactivate(userCookie);
				}
			}

			// Token: 0x06006213 RID: 25107 RVA: 0x0016AC00 File Offset: 0x00168E00
			private void RunMessageLoopInner(int reason, ApplicationContext context)
			{
				if (reason == 4 && !SystemInformation.UserInteractive)
				{
					throw new InvalidOperationException(SR.GetString("CantShowModalOnNonInteractive"));
				}
				if (reason == -1)
				{
					this.SetState(8, false);
				}
				if (Application.ThreadContext.totalMessageLoopCount++ == 0)
				{
					Application.ThreadContext.baseLoopReason = reason;
				}
				this.messageLoopCount++;
				if (reason == -1)
				{
					if (this.messageLoopCount != 1)
					{
						throw new InvalidOperationException(SR.GetString("CantNestMessageLoops"));
					}
					this.applicationContext = context;
					this.applicationContext.ThreadExit += this.OnAppThreadExit;
					if (this.applicationContext.MainForm != null)
					{
						this.applicationContext.MainForm.Visible = true;
					}
					DpiHelper.InitializeDpiHelperForWinforms();
					AccessibilityImprovements.ValidateLevels();
				}
				Form form = this.currentForm;
				if (context != null)
				{
					this.currentForm = context.MainForm;
				}
				bool flag = false;
				bool flag2 = false;
				HandleRef hWnd = new HandleRef(null, IntPtr.Zero);
				if (reason == -2)
				{
					flag2 = true;
				}
				if (reason == 4 || reason == 5)
				{
					flag = true;
					bool flag3 = this.currentForm != null && this.currentForm.Enabled;
					this.BeginModalMessageLoop(context);
					hWnd = new HandleRef(null, UnsafeNativeMethods.GetWindowLong(new HandleRef(this.currentForm, this.currentForm.Handle), -8));
					if (hWnd.Handle != IntPtr.Zero)
					{
						if (SafeNativeMethods.IsWindowEnabled(hWnd))
						{
							SafeNativeMethods.EnableWindow(hWnd, false);
						}
						else
						{
							hWnd = new HandleRef(null, IntPtr.Zero);
						}
					}
					if (this.currentForm != null && this.currentForm.IsHandleCreated && SafeNativeMethods.IsWindowEnabled(new HandleRef(this.currentForm, this.currentForm.Handle)) != flag3)
					{
						SafeNativeMethods.EnableWindow(new HandleRef(this.currentForm, this.currentForm.Handle), flag3);
					}
				}
				try
				{
					if (this.messageLoopCount == 1)
					{
						WindowsFormsSynchronizationContext.InstallIfNeeded();
					}
					if (flag && this.currentForm != null)
					{
						this.currentForm.Visible = true;
					}
					if ((!flag && !flag2) || this.ComponentManager is Application.ComponentManager)
					{
						bool flag4 = this.ComponentManager.FPushMessageLoop((IntPtr)this.componentID, reason, 0);
					}
					else if (reason == 2 || reason == -2)
					{
						bool flag4 = this.LocalModalMessageLoop(null);
					}
					else
					{
						bool flag4 = this.LocalModalMessageLoop(this.currentForm);
					}
				}
				finally
				{
					if (flag)
					{
						this.EndModalMessageLoop(context);
						if (hWnd.Handle != IntPtr.Zero)
						{
							SafeNativeMethods.EnableWindow(hWnd, true);
						}
					}
					this.currentForm = form;
					Application.ThreadContext.totalMessageLoopCount--;
					this.messageLoopCount--;
					if (this.messageLoopCount == 0)
					{
						WindowsFormsSynchronizationContext.Uninstall(false);
					}
					if (reason == -1)
					{
						this.Dispose(true);
					}
					else if (this.messageLoopCount == 0 && this.componentManager != null)
					{
						this.RevokeComponent();
					}
				}
			}

			// Token: 0x06006214 RID: 25108 RVA: 0x0016AEC4 File Offset: 0x001690C4
			private bool LocalModalMessageLoop(Form form)
			{
				bool result;
				try
				{
					NativeMethods.MSG msg = default(NativeMethods.MSG);
					bool flag = true;
					while (flag)
					{
						bool flag2 = UnsafeNativeMethods.PeekMessage(ref msg, NativeMethods.NullHandleRef, 0, 0, 0);
						if (flag2)
						{
							bool flag3;
							if (msg.hwnd != IntPtr.Zero && SafeNativeMethods.IsWindowUnicode(new HandleRef(null, msg.hwnd)))
							{
								flag3 = true;
								if (!UnsafeNativeMethods.GetMessageW(ref msg, NativeMethods.NullHandleRef, 0, 0))
								{
									continue;
								}
							}
							else
							{
								flag3 = false;
								if (!UnsafeNativeMethods.GetMessageA(ref msg, NativeMethods.NullHandleRef, 0, 0))
								{
									continue;
								}
							}
							if (!this.PreTranslateMessage(ref msg))
							{
								UnsafeNativeMethods.TranslateMessage(ref msg);
								if (flag3)
								{
									UnsafeNativeMethods.DispatchMessageW(ref msg);
								}
								else
								{
									UnsafeNativeMethods.DispatchMessageA(ref msg);
								}
							}
							if (form != null)
							{
								flag = !form.CheckCloseDialog(false);
							}
						}
						else
						{
							if (form == null)
							{
								break;
							}
							if (!UnsafeNativeMethods.PeekMessage(ref msg, NativeMethods.NullHandleRef, 0, 0, 0))
							{
								UnsafeNativeMethods.WaitMessage();
							}
						}
					}
					result = flag;
				}
				catch
				{
					result = false;
				}
				return result;
			}

			// Token: 0x06006215 RID: 25109 RVA: 0x0016AFB8 File Offset: 0x001691B8
			internal bool ProcessFilters(ref NativeMethods.MSG msg, out bool modified)
			{
				bool result = false;
				modified = false;
				if (this.messageFilters != null && !this.GetState(16) && (LocalAppContextSwitches.DontSupportReentrantFilterMessage || this.inProcessFilters == 0))
				{
					if (this.messageFilters.Count > 0)
					{
						this.messageFilterSnapshot = new IMessageFilter[this.messageFilters.Count];
						this.messageFilters.CopyTo(this.messageFilterSnapshot);
					}
					else
					{
						this.messageFilterSnapshot = null;
					}
					this.SetState(16, true);
				}
				this.inProcessFilters++;
				try
				{
					if (this.messageFilterSnapshot != null)
					{
						int num = this.messageFilterSnapshot.Length;
						Message message = Message.Create(msg.hwnd, msg.message, msg.wParam, msg.lParam);
						for (int i = 0; i < num; i++)
						{
							IMessageFilter messageFilter = this.messageFilterSnapshot[i];
							bool flag = messageFilter.PreFilterMessage(ref message);
							if (messageFilter is IMessageModifyAndFilter)
							{
								msg.hwnd = message.HWnd;
								msg.message = message.Msg;
								msg.wParam = message.WParam;
								msg.lParam = message.LParam;
								modified = true;
							}
							if (flag)
							{
								result = true;
								break;
							}
						}
					}
				}
				finally
				{
					this.inProcessFilters--;
				}
				return result;
			}

			// Token: 0x06006216 RID: 25110 RVA: 0x0016B100 File Offset: 0x00169300
			internal bool PreTranslateMessage(ref NativeMethods.MSG msg)
			{
				bool flag = false;
				if (this.ProcessFilters(ref msg, out flag))
				{
					return true;
				}
				if (msg.message >= 256 && msg.message <= 264)
				{
					if (msg.message == 258)
					{
						int num = 21364736;
						if ((int)((long)msg.wParam) == 3 && ((int)((long)msg.lParam) & num) == num && Debugger.IsAttached)
						{
							Debugger.Break();
						}
					}
					Control control = Control.FromChildHandleInternal(msg.hwnd);
					bool flag2 = false;
					Message message = Message.Create(msg.hwnd, msg.message, msg.wParam, msg.lParam);
					if (control != null)
					{
						if (NativeWindow.WndProcShouldBeDebuggable)
						{
							if (Control.PreProcessControlMessageInternal(control, ref message) == PreProcessControlState.MessageProcessed)
							{
								flag2 = true;
								goto IL_104;
							}
							goto IL_104;
						}
						else
						{
							try
							{
								if (Control.PreProcessControlMessageInternal(control, ref message) == PreProcessControlState.MessageProcessed)
								{
									flag2 = true;
								}
								goto IL_104;
							}
							catch (Exception t)
							{
								this.OnThreadException(t);
								goto IL_104;
							}
						}
					}
					IntPtr ancestor = UnsafeNativeMethods.GetAncestor(new HandleRef(null, msg.hwnd), 2);
					if (ancestor != IntPtr.Zero && UnsafeNativeMethods.IsDialogMessage(new HandleRef(null, ancestor), ref msg))
					{
						return true;
					}
					IL_104:
					msg.wParam = message.WParam;
					msg.lParam = message.LParam;
					if (flag2)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06006217 RID: 25111 RVA: 0x0016B244 File Offset: 0x00169444
			private void RevokeComponent()
			{
				if (this.componentManager != null && this.componentID != -1)
				{
					int value = this.componentID;
					UnsafeNativeMethods.IMsoComponentManager msoComponentManager = this.componentManager;
					try
					{
						msoComponentManager.FRevokeComponent((IntPtr)value);
						if (Marshal.IsComObject(msoComponentManager))
						{
							Marshal.ReleaseComObject(msoComponentManager);
						}
					}
					finally
					{
						this.componentManager = null;
						this.componentID = -1;
					}
				}
			}

			// Token: 0x06006218 RID: 25112 RVA: 0x0016B2B0 File Offset: 0x001694B0
			internal void SetCulture(CultureInfo culture)
			{
				if (culture != null && culture.LCID != SafeNativeMethods.GetThreadLocale())
				{
					SafeNativeMethods.SetThreadLocale(culture.LCID);
				}
			}

			// Token: 0x06006219 RID: 25113 RVA: 0x0016B2CE File Offset: 0x001694CE
			private void SetState(int bit, bool value)
			{
				if (value)
				{
					this.threadState |= bit;
					return;
				}
				this.threadState &= ~bit;
			}

			// Token: 0x0600621A RID: 25114 RVA: 0x00011A20 File Offset: 0x0000FC20
			bool UnsafeNativeMethods.IMsoComponent.FDebugMessage(IntPtr hInst, int msg, IntPtr wparam, IntPtr lparam)
			{
				return false;
			}

			// Token: 0x0600621B RID: 25115 RVA: 0x0016B2F1 File Offset: 0x001694F1
			bool UnsafeNativeMethods.IMsoComponent.FPreTranslateMessage(ref NativeMethods.MSG msg)
			{
				return this.PreTranslateMessage(ref msg);
			}

			// Token: 0x0600621C RID: 25116 RVA: 0x0016B2FA File Offset: 0x001694FA
			void UnsafeNativeMethods.IMsoComponent.OnEnterState(int uStateID, bool fEnter)
			{
				if (this.ourModalLoop)
				{
					return;
				}
				if (uStateID == 1)
				{
					if (fEnter)
					{
						this.DisableWindowsForModalLoop(true, null);
						return;
					}
					this.EnableWindowsForModalLoop(true, null);
				}
			}

			// Token: 0x0600621D RID: 25117 RVA: 0x000072B6 File Offset: 0x000054B6
			void UnsafeNativeMethods.IMsoComponent.OnAppActivate(bool fActive, int dwOtherThreadID)
			{
			}

			// Token: 0x0600621E RID: 25118 RVA: 0x000072B6 File Offset: 0x000054B6
			void UnsafeNativeMethods.IMsoComponent.OnLoseActivation()
			{
			}

			// Token: 0x0600621F RID: 25119 RVA: 0x000072B6 File Offset: 0x000054B6
			void UnsafeNativeMethods.IMsoComponent.OnActivationChange(UnsafeNativeMethods.IMsoComponent component, bool fSameComponent, int pcrinfo, bool fHostIsActivating, int pchostinfo, int dwReserved)
			{
			}

			// Token: 0x06006220 RID: 25120 RVA: 0x0016B31D File Offset: 0x0016951D
			bool UnsafeNativeMethods.IMsoComponent.FDoIdle(int grfidlef)
			{
				if (this.idleHandler != null)
				{
					this.idleHandler(Thread.CurrentThread, EventArgs.Empty);
				}
				return false;
			}

			// Token: 0x06006221 RID: 25121 RVA: 0x0016B340 File Offset: 0x00169540
			bool UnsafeNativeMethods.IMsoComponent.FContinueMessageLoop(int reason, int pvLoopData, NativeMethods.MSG[] msgPeeked)
			{
				bool result = true;
				if (msgPeeked == null && this.GetState(8))
				{
					result = false;
				}
				else
				{
					switch (reason)
					{
					case -2:
					case 2:
						if (!UnsafeNativeMethods.PeekMessage(ref this.tempMsg, NativeMethods.NullHandleRef, 0, 0, 0))
						{
							result = false;
						}
						break;
					case 1:
					{
						int num;
						SafeNativeMethods.GetWindowThreadProcessId(new HandleRef(null, UnsafeNativeMethods.GetActiveWindow()), out num);
						if (num == SafeNativeMethods.GetCurrentProcessId())
						{
							result = false;
						}
						break;
					}
					case 4:
					case 5:
						if (this.currentForm == null || this.currentForm.CheckCloseDialog(false))
						{
							result = false;
						}
						break;
					}
				}
				return result;
			}

			// Token: 0x06006222 RID: 25122 RVA: 0x00013062 File Offset: 0x00011262
			bool UnsafeNativeMethods.IMsoComponent.FQueryTerminate(bool fPromptUser)
			{
				return true;
			}

			// Token: 0x06006223 RID: 25123 RVA: 0x0016B3DB File Offset: 0x001695DB
			void UnsafeNativeMethods.IMsoComponent.Terminate()
			{
				if (this.messageLoopCount > 0 && !(this.ComponentManager is Application.ComponentManager))
				{
					this.messageLoopCount--;
				}
				this.Dispose(false);
			}

			// Token: 0x06006224 RID: 25124 RVA: 0x000F9F19 File Offset: 0x000F8119
			IntPtr UnsafeNativeMethods.IMsoComponent.HwndGetWindow(int dwWhich, int dwReserved)
			{
				return IntPtr.Zero;
			}

			// Token: 0x040038AE RID: 14510
			private const int STATE_OLEINITIALIZED = 1;

			// Token: 0x040038AF RID: 14511
			private const int STATE_EXTERNALOLEINIT = 2;

			// Token: 0x040038B0 RID: 14512
			private const int STATE_INTHREADEXCEPTION = 4;

			// Token: 0x040038B1 RID: 14513
			private const int STATE_POSTEDQUIT = 8;

			// Token: 0x040038B2 RID: 14514
			private const int STATE_FILTERSNAPSHOTVALID = 16;

			// Token: 0x040038B3 RID: 14515
			private const int STATE_TRACKINGCOMPONENT = 32;

			// Token: 0x040038B4 RID: 14516
			private const int INVALID_ID = -1;

			// Token: 0x040038B5 RID: 14517
			private static Hashtable contextHash = new Hashtable();

			// Token: 0x040038B6 RID: 14518
			private static object tcInternalSyncObject = new object();

			// Token: 0x040038B7 RID: 14519
			private static int totalMessageLoopCount;

			// Token: 0x040038B8 RID: 14520
			private static int baseLoopReason;

			// Token: 0x040038B9 RID: 14521
			[ThreadStatic]
			private static Application.ThreadContext currentThreadContext;

			// Token: 0x040038BA RID: 14522
			internal ThreadExceptionEventHandler threadExceptionHandler;

			// Token: 0x040038BB RID: 14523
			internal EventHandler idleHandler;

			// Token: 0x040038BC RID: 14524
			internal EventHandler enterModalHandler;

			// Token: 0x040038BD RID: 14525
			internal EventHandler leaveModalHandler;

			// Token: 0x040038BE RID: 14526
			private ApplicationContext applicationContext;

			// Token: 0x040038BF RID: 14527
			private List<Application.ParkingWindow> parkingWindows = new List<Application.ParkingWindow>();

			// Token: 0x040038C0 RID: 14528
			private Control marshalingControl;

			// Token: 0x040038C1 RID: 14529
			private CultureInfo culture;

			// Token: 0x040038C2 RID: 14530
			private ArrayList messageFilters;

			// Token: 0x040038C3 RID: 14531
			private IMessageFilter[] messageFilterSnapshot;

			// Token: 0x040038C4 RID: 14532
			private int inProcessFilters;

			// Token: 0x040038C5 RID: 14533
			private IntPtr handle;

			// Token: 0x040038C6 RID: 14534
			private int id;

			// Token: 0x040038C7 RID: 14535
			private int messageLoopCount;

			// Token: 0x040038C8 RID: 14536
			private int threadState;

			// Token: 0x040038C9 RID: 14537
			private int modalCount;

			// Token: 0x040038CA RID: 14538
			private WeakReference activatingControlRef;

			// Token: 0x040038CB RID: 14539
			private UnsafeNativeMethods.IMsoComponentManager componentManager;

			// Token: 0x040038CC RID: 14540
			private bool externalComponentManager;

			// Token: 0x040038CD RID: 14541
			private bool fetchingComponentManager;

			// Token: 0x040038CE RID: 14542
			private int componentID = -1;

			// Token: 0x040038CF RID: 14543
			private Form currentForm;

			// Token: 0x040038D0 RID: 14544
			private Application.ThreadWindows threadWindows;

			// Token: 0x040038D1 RID: 14545
			private NativeMethods.MSG tempMsg;

			// Token: 0x040038D2 RID: 14546
			private int disposeCount;

			// Token: 0x040038D3 RID: 14547
			private bool ourModalLoop;

			// Token: 0x040038D4 RID: 14548
			private Application.MessageLoopCallback messageLoopCallback;
		}

		// Token: 0x02000604 RID: 1540
		internal sealed class MarshalingControl : Control
		{
			// Token: 0x06006226 RID: 25126 RVA: 0x0016B41E File Offset: 0x0016961E
			internal MarshalingControl() : base(false)
			{
				base.Visible = false;
				base.SetState2(8, false);
				base.SetTopLevel(true);
				base.CreateControl();
				this.CreateHandle();
			}

			// Token: 0x17001508 RID: 5384
			// (get) Token: 0x06006227 RID: 25127 RVA: 0x0016B44C File Offset: 0x0016964C
			protected override CreateParams CreateParams
			{
				get
				{
					CreateParams createParams = base.CreateParams;
					if (Environment.OSVersion.Platform == PlatformID.Win32NT)
					{
						createParams.Parent = (IntPtr)NativeMethods.HWND_MESSAGE;
					}
					return createParams;
				}
			}

			// Token: 0x06006228 RID: 25128 RVA: 0x000072B6 File Offset: 0x000054B6
			protected override void OnLayout(LayoutEventArgs levent)
			{
			}

			// Token: 0x06006229 RID: 25129 RVA: 0x000072B6 File Offset: 0x000054B6
			protected override void OnSizeChanged(EventArgs e)
			{
			}
		}

		// Token: 0x02000605 RID: 1541
		internal sealed class ParkingWindow : ContainerControl, IArrangedElement, IComponent, IDisposable
		{
			// Token: 0x0600622A RID: 25130 RVA: 0x0016B47E File Offset: 0x0016967E
			public ParkingWindow()
			{
				base.SetState2(8, false);
				base.SetState(524288, true);
				this.Text = "WindowsFormsParkingWindow";
				base.Visible = false;
			}

			// Token: 0x17001509 RID: 5385
			// (get) Token: 0x0600622B RID: 25131 RVA: 0x0016B4AC File Offset: 0x001696AC
			protected override CreateParams CreateParams
			{
				get
				{
					CreateParams createParams = base.CreateParams;
					if (Environment.OSVersion.Platform == PlatformID.Win32NT)
					{
						createParams.Parent = (IntPtr)NativeMethods.HWND_MESSAGE;
					}
					return createParams;
				}
			}

			// Token: 0x0600622C RID: 25132 RVA: 0x0016B4DE File Offset: 0x001696DE
			internal override void AddReflectChild()
			{
				if (this.childCount < 0)
				{
					this.childCount = 0;
				}
				this.childCount++;
			}

			// Token: 0x0600622D RID: 25133 RVA: 0x0016B500 File Offset: 0x00169700
			internal override void RemoveReflectChild()
			{
				this.childCount--;
				if (this.childCount < 0)
				{
					this.childCount = 0;
				}
				if (this.childCount == 0 && base.IsHandleCreated)
				{
					int num;
					int windowThreadProcessId = SafeNativeMethods.GetWindowThreadProcessId(new HandleRef(this, base.HandleInternal), out num);
					Application.ThreadContext threadContext = Application.ThreadContext.FromId(windowThreadProcessId);
					if (threadContext == null || threadContext != Application.ThreadContext.FromCurrent())
					{
						UnsafeNativeMethods.PostMessage(new HandleRef(this, base.HandleInternal), 1025, IntPtr.Zero, IntPtr.Zero);
						return;
					}
					this.CheckDestroy();
				}
			}

			// Token: 0x0600622E RID: 25134 RVA: 0x0016B58C File Offset: 0x0016978C
			private void CheckDestroy()
			{
				if (this.childCount == 0)
				{
					IntPtr window = UnsafeNativeMethods.GetWindow(new HandleRef(this, base.Handle), 5);
					if (window == IntPtr.Zero)
					{
						this.DestroyHandle();
					}
				}
			}

			// Token: 0x0600622F RID: 25135 RVA: 0x0016B5C7 File Offset: 0x001697C7
			public void Destroy()
			{
				this.DestroyHandle();
			}

			// Token: 0x06006230 RID: 25136 RVA: 0x0016B5CF File Offset: 0x001697CF
			internal void ParkHandle(HandleRef handle)
			{
				if (!base.IsHandleCreated)
				{
					this.CreateHandle();
				}
				UnsafeNativeMethods.SetParent(handle, new HandleRef(this, base.Handle));
			}

			// Token: 0x06006231 RID: 25137 RVA: 0x0016B5F2 File Offset: 0x001697F2
			internal void UnparkHandle(HandleRef handle)
			{
				if (base.IsHandleCreated)
				{
					this.CheckDestroy();
				}
			}

			// Token: 0x06006232 RID: 25138 RVA: 0x000072B6 File Offset: 0x000054B6
			protected override void OnLayout(LayoutEventArgs levent)
			{
			}

			// Token: 0x06006233 RID: 25139 RVA: 0x000072B6 File Offset: 0x000054B6
			void IArrangedElement.PerformLayout(IArrangedElement affectedElement, string affectedProperty)
			{
			}

			// Token: 0x06006234 RID: 25140 RVA: 0x0016B604 File Offset: 0x00169804
			protected override void WndProc(ref Message m)
			{
				if (m.Msg != 24)
				{
					base.WndProc(ref m);
					if (m.Msg == 528)
					{
						if (NativeMethods.Util.LOWORD((int)((long)m.WParam)) == 2)
						{
							UnsafeNativeMethods.PostMessage(new HandleRef(this, base.Handle), 1025, IntPtr.Zero, IntPtr.Zero);
							return;
						}
					}
					else if (m.Msg == 1025)
					{
						this.CheckDestroy();
					}
				}
			}

			// Token: 0x040038D5 RID: 14549
			private const int WM_CHECKDESTROY = 1025;

			// Token: 0x040038D6 RID: 14550
			private int childCount;
		}

		// Token: 0x02000606 RID: 1542
		private sealed class ThreadWindows
		{
			// Token: 0x06006235 RID: 25141 RVA: 0x0016B678 File Offset: 0x00169878
			internal ThreadWindows(bool onlyWinForms)
			{
				this.windows = new IntPtr[16];
				this.onlyWinForms = onlyWinForms;
				UnsafeNativeMethods.EnumThreadWindows(SafeNativeMethods.GetCurrentThreadId(), new NativeMethods.EnumThreadWindowsCallback(this.Callback), NativeMethods.NullHandleRef);
			}

			// Token: 0x06006236 RID: 25142 RVA: 0x0016B6B8 File Offset: 0x001698B8
			private bool Callback(IntPtr hWnd, IntPtr lparam)
			{
				if (SafeNativeMethods.IsWindowVisible(new HandleRef(null, hWnd)) && SafeNativeMethods.IsWindowEnabled(new HandleRef(null, hWnd)))
				{
					bool flag = true;
					if (this.onlyWinForms && Control.FromHandleInternal(hWnd) == null)
					{
						flag = false;
					}
					if (flag)
					{
						if (this.windowCount == this.windows.Length)
						{
							IntPtr[] destinationArray = new IntPtr[this.windowCount * 2];
							Array.Copy(this.windows, 0, destinationArray, 0, this.windowCount);
							this.windows = destinationArray;
						}
						IntPtr[] array = this.windows;
						int num = this.windowCount;
						this.windowCount = num + 1;
						array[num] = hWnd;
					}
				}
				return true;
			}

			// Token: 0x06006237 RID: 25143 RVA: 0x0016B750 File Offset: 0x00169950
			internal void Dispose()
			{
				for (int i = 0; i < this.windowCount; i++)
				{
					IntPtr handle = this.windows[i];
					if (UnsafeNativeMethods.IsWindow(new HandleRef(null, handle)))
					{
						Control control = Control.FromHandleInternal(handle);
						if (control != null)
						{
							control.Dispose();
						}
					}
				}
			}

			// Token: 0x06006238 RID: 25144 RVA: 0x0016B798 File Offset: 0x00169998
			internal void Enable(bool state)
			{
				if (!this.onlyWinForms && !state)
				{
					this.activeHwnd = UnsafeNativeMethods.GetActiveWindow();
					Control activatingControl = Application.ThreadContext.FromCurrent().ActivatingControl;
					if (activatingControl != null)
					{
						this.focusedHwnd = activatingControl.Handle;
					}
					else
					{
						this.focusedHwnd = UnsafeNativeMethods.GetFocus();
					}
				}
				for (int i = 0; i < this.windowCount; i++)
				{
					IntPtr handle = this.windows[i];
					if (UnsafeNativeMethods.IsWindow(new HandleRef(null, handle)))
					{
						SafeNativeMethods.EnableWindow(new HandleRef(null, handle), state);
					}
				}
				if (!this.onlyWinForms && state)
				{
					if (this.activeHwnd != IntPtr.Zero && UnsafeNativeMethods.IsWindow(new HandleRef(null, this.activeHwnd)))
					{
						UnsafeNativeMethods.SetActiveWindow(new HandleRef(null, this.activeHwnd));
					}
					if (this.focusedHwnd != IntPtr.Zero && UnsafeNativeMethods.IsWindow(new HandleRef(null, this.focusedHwnd)))
					{
						UnsafeNativeMethods.SetFocus(new HandleRef(null, this.focusedHwnd));
					}
				}
			}

			// Token: 0x040038D7 RID: 14551
			private IntPtr[] windows;

			// Token: 0x040038D8 RID: 14552
			private int windowCount;

			// Token: 0x040038D9 RID: 14553
			private IntPtr activeHwnd;

			// Token: 0x040038DA RID: 14554
			private IntPtr focusedHwnd;

			// Token: 0x040038DB RID: 14555
			internal Application.ThreadWindows previousThreadWindows;

			// Token: 0x040038DC RID: 14556
			private bool onlyWinForms = true;
		}

		// Token: 0x02000607 RID: 1543
		private class ModalApplicationContext : ApplicationContext
		{
			// Token: 0x06006239 RID: 25145 RVA: 0x0016B893 File Offset: 0x00169A93
			public ModalApplicationContext(Form modalForm) : base(modalForm)
			{
			}

			// Token: 0x0600623A RID: 25146 RVA: 0x0016B89C File Offset: 0x00169A9C
			public void DisableThreadWindows(bool disable, bool onlyWinForms)
			{
				Control control = null;
				if (base.MainForm != null && base.MainForm.IsHandleCreated)
				{
					IntPtr windowLong = UnsafeNativeMethods.GetWindowLong(new HandleRef(this, base.MainForm.Handle), -8);
					control = Control.FromHandleInternal(windowLong);
					if (control != null && control.InvokeRequired)
					{
						this.parentWindowContext = Application.GetContextForHandle(new HandleRef(this, windowLong));
					}
					else
					{
						this.parentWindowContext = null;
					}
				}
				if (this.parentWindowContext != null)
				{
					if (control == null)
					{
						control = this.parentWindowContext.ApplicationContext.MainForm;
					}
					if (disable)
					{
						control.Invoke(new Application.ModalApplicationContext.ThreadWindowCallback(this.DisableThreadWindowsCallback), new object[]
						{
							this.parentWindowContext,
							onlyWinForms
						});
						return;
					}
					control.Invoke(new Application.ModalApplicationContext.ThreadWindowCallback(this.EnableThreadWindowsCallback), new object[]
					{
						this.parentWindowContext,
						onlyWinForms
					});
				}
			}

			// Token: 0x0600623B RID: 25147 RVA: 0x0016B97C File Offset: 0x00169B7C
			private void DisableThreadWindowsCallback(Application.ThreadContext context, bool onlyWinForms)
			{
				context.DisableWindowsForModalLoop(onlyWinForms, this);
			}

			// Token: 0x0600623C RID: 25148 RVA: 0x0016B986 File Offset: 0x00169B86
			private void EnableThreadWindowsCallback(Application.ThreadContext context, bool onlyWinForms)
			{
				context.EnableWindowsForModalLoop(onlyWinForms, this);
			}

			// Token: 0x0600623D RID: 25149 RVA: 0x000072B6 File Offset: 0x000054B6
			protected override void ExitThreadCore()
			{
			}

			// Token: 0x040038DD RID: 14557
			private Application.ThreadContext parentWindowContext;

			// Token: 0x020008B4 RID: 2228
			// (Invoke) Token: 0x0600729D RID: 29341
			private delegate void ThreadWindowCallback(Application.ThreadContext context, bool onlyWinForms);
		}
	}
}
