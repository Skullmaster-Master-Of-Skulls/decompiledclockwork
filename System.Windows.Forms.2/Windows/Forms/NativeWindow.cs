using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Internal;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;
using System.Security.Permissions;
using System.Text;
using Microsoft.Win32;

namespace System.Windows.Forms
{
	// Token: 0x02000306 RID: 774
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class NativeWindow : MarshalByRefObject, IWin32Window
	{
		// Token: 0x06003141 RID: 12609 RVA: 0x000DE190 File Offset: 0x000DC390
		static NativeWindow()
		{
			EventHandler value = new EventHandler(NativeWindow.OnShutdown);
			AppDomain.CurrentDomain.ProcessExit += value;
			AppDomain.CurrentDomain.DomainUnload += value;
			int num = NativeWindow.primes[4];
			NativeWindow.hashBuckets = new NativeWindow.HandleBucket[num];
			NativeWindow.hashLoadSize = (int)(0.72f * (float)num);
			if (NativeWindow.hashLoadSize >= num)
			{
				NativeWindow.hashLoadSize = num - 1;
			}
			NativeWindow.hashForIdHandle = new Dictionary<short, IntPtr>();
			NativeWindow.hashForHandleId = new Dictionary<IntPtr, short>();
		}

		// Token: 0x06003142 RID: 12610 RVA: 0x000DE242 File Offset: 0x000DC442
		public NativeWindow()
		{
			this.weakThisPtr = new WeakReference(this);
		}

		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x06003143 RID: 12611 RVA: 0x000DE26B File Offset: 0x000DC46B
		internal DpiAwarenessContext DpiAwarenessContext
		{
			get
			{
				return this.windowDpiAwarenessContext;
			}
		}

		// Token: 0x06003144 RID: 12612 RVA: 0x000DE274 File Offset: 0x000DC474
		~NativeWindow()
		{
			this.ForceExitMessageLoop();
		}

		// Token: 0x06003145 RID: 12613 RVA: 0x000DE2A0 File Offset: 0x000DC4A0
		internal void ForceExitMessageLoop()
		{
			IntPtr value;
			bool flag2;
			lock (this)
			{
				value = this.handle;
				flag2 = this.ownHandle;
			}
			if (this.handle != IntPtr.Zero)
			{
				if (UnsafeNativeMethods.IsWindow(new HandleRef(null, this.handle)))
				{
					int num;
					int windowThreadProcessId = SafeNativeMethods.GetWindowThreadProcessId(new HandleRef(null, this.handle), out num);
					Application.ThreadContext threadContext = Application.ThreadContext.FromId(windowThreadProcessId);
					IntPtr value2 = (threadContext == null) ? IntPtr.Zero : threadContext.GetHandle();
					if (value2 != IntPtr.Zero)
					{
						int num2 = 0;
						SafeNativeMethods.GetExitCodeThread(new HandleRef(null, value2), out num2);
						if (!AppDomain.CurrentDomain.IsFinalizingForUnload() && num2 == 259)
						{
							IntPtr intPtr;
							UnsafeNativeMethods.SendMessageTimeout(new HandleRef(null, this.handle), NativeMethods.WM_UIUNSUBCLASS, IntPtr.Zero, IntPtr.Zero, 2, 100, out intPtr) == IntPtr.Zero;
						}
					}
				}
				if (this.handle != IntPtr.Zero)
				{
					this.ReleaseHandle(true);
				}
			}
			if (value != IntPtr.Zero && flag2)
			{
				UnsafeNativeMethods.PostMessage(new HandleRef(this, value), 16, 0, 0);
			}
		}

		// Token: 0x17000B94 RID: 2964
		// (get) Token: 0x06003146 RID: 12614 RVA: 0x000DE3E0 File Offset: 0x000DC5E0
		internal static bool AnyHandleCreated
		{
			get
			{
				return NativeWindow.anyHandleCreated;
			}
		}

		// Token: 0x17000B95 RID: 2965
		// (get) Token: 0x06003147 RID: 12615 RVA: 0x000DE3E7 File Offset: 0x000DC5E7
		public IntPtr Handle
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x17000B96 RID: 2966
		// (get) Token: 0x06003148 RID: 12616 RVA: 0x000DE3EF File Offset: 0x000DC5EF
		internal NativeWindow PreviousWindow
		{
			get
			{
				return this.previousWindow;
			}
		}

		// Token: 0x17000B97 RID: 2967
		// (get) Token: 0x06003149 RID: 12617 RVA: 0x000DE3F7 File Offset: 0x000DC5F7
		internal static IntPtr UserDefindowProc
		{
			get
			{
				return NativeWindow.userDefWindowProc;
			}
		}

		// Token: 0x17000B98 RID: 2968
		// (get) Token: 0x0600314A RID: 12618 RVA: 0x000DE400 File Offset: 0x000DC600
		private static int WndProcFlags
		{
			get
			{
				int num = (int)NativeWindow.wndProcFlags;
				if (num == 0)
				{
					if (NativeWindow.userSetProcFlags != 0)
					{
						num = (int)NativeWindow.userSetProcFlags;
					}
					else if (NativeWindow.userSetProcFlagsForApp != 0)
					{
						num = (int)NativeWindow.userSetProcFlagsForApp;
					}
					else if (!Application.CustomThreadExceptionHandlerAttached)
					{
						if (Debugger.IsAttached)
						{
							num |= 4;
						}
						else
						{
							num = NativeWindow.AdjustWndProcFlagsFromRegistry(num);
							if ((num & 2) != 0)
							{
								num = NativeWindow.AdjustWndProcFlagsFromMetadata(num);
								if ((num & 16) != 0)
								{
									if ((num & 8) != 0)
									{
										num = NativeWindow.AdjustWndProcFlagsFromConfig(num);
									}
									else
									{
										num |= 4;
									}
								}
							}
						}
					}
					num |= 1;
					NativeWindow.wndProcFlags = (byte)num;
				}
				return num;
			}
		}

		// Token: 0x17000B99 RID: 2969
		// (get) Token: 0x0600314B RID: 12619 RVA: 0x000DE47F File Offset: 0x000DC67F
		internal static bool WndProcShouldBeDebuggable
		{
			get
			{
				return (NativeWindow.WndProcFlags & 4) != 0;
			}
		}

		// Token: 0x0600314C RID: 12620 RVA: 0x000DE48C File Offset: 0x000DC68C
		private static void AddWindowToTable(IntPtr handle, NativeWindow window)
		{
			object obj = NativeWindow.internalSyncObject;
			lock (obj)
			{
				if (NativeWindow.handleCount >= NativeWindow.hashLoadSize)
				{
					NativeWindow.ExpandTable();
				}
				NativeWindow.anyHandleCreated = true;
				NativeWindow.anyHandleCreatedInApp = true;
				uint num2;
				uint num3;
				uint num = NativeWindow.InitHash(handle, NativeWindow.hashBuckets.Length, out num2, out num3);
				int num4 = 0;
				int num5 = -1;
				GCHandle window2 = GCHandle.Alloc(window, GCHandleType.Weak);
				int num6;
				for (;;)
				{
					num6 = (int)(num2 % (uint)NativeWindow.hashBuckets.Length);
					if (num5 == -1 && NativeWindow.hashBuckets[num6].handle == new IntPtr(-1) && NativeWindow.hashBuckets[num6].hash_coll < 0)
					{
						num5 = num6;
					}
					if (NativeWindow.hashBuckets[num6].handle == IntPtr.Zero || (NativeWindow.hashBuckets[num6].handle == new IntPtr(-1) && ((long)NativeWindow.hashBuckets[num6].hash_coll & (long)((ulong)-2147483648)) == 0L))
					{
						break;
					}
					if ((long)(NativeWindow.hashBuckets[num6].hash_coll & 2147483647) == (long)((ulong)num) && handle == NativeWindow.hashBuckets[num6].handle)
					{
						goto Block_11;
					}
					if (num5 == -1)
					{
						NativeWindow.HandleBucket[] array = NativeWindow.hashBuckets;
						int num7 = num6;
						array[num7].hash_coll = (array[num7].hash_coll | int.MinValue);
					}
					num2 += num3;
					if (++num4 >= NativeWindow.hashBuckets.Length)
					{
						goto Block_15;
					}
				}
				if (num5 != -1)
				{
					num6 = num5;
				}
				NativeWindow.hashBuckets[num6].window = window2;
				NativeWindow.hashBuckets[num6].handle = handle;
				NativeWindow.HandleBucket[] array2 = NativeWindow.hashBuckets;
				int num8 = num6;
				array2[num8].hash_coll = (array2[num8].hash_coll | (int)num);
				NativeWindow.handleCount++;
				return;
				Block_11:
				GCHandle window3 = NativeWindow.hashBuckets[num6].window;
				if (window3.IsAllocated)
				{
					if (window3.Target != null)
					{
						window.previousWindow = (NativeWindow)window3.Target;
						window.previousWindow.nextWindow = window;
					}
					window3.Free();
				}
				NativeWindow.hashBuckets[num6].window = window2;
				return;
				Block_15:
				if (num5 != -1)
				{
					NativeWindow.hashBuckets[num5].window = window2;
					NativeWindow.hashBuckets[num5].handle = handle;
					NativeWindow.HandleBucket[] array3 = NativeWindow.hashBuckets;
					int num9 = num5;
					array3[num9].hash_coll = (array3[num9].hash_coll | (int)num);
					NativeWindow.handleCount++;
				}
			}
		}

		// Token: 0x0600314D RID: 12621 RVA: 0x000DE720 File Offset: 0x000DC920
		internal static void AddWindowToIDTable(object wrapper, IntPtr handle)
		{
			NativeWindow.hashForIdHandle[NativeWindow.globalID] = handle;
			NativeWindow.hashForHandleId[handle] = NativeWindow.globalID;
			UnsafeNativeMethods.SetWindowLong(new HandleRef(wrapper, handle), -12, new HandleRef(wrapper, (IntPtr)((int)NativeWindow.globalID)));
			NativeWindow.globalID += 1;
		}

		// Token: 0x0600314E RID: 12622 RVA: 0x000DE779 File Offset: 0x000DC979
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static int AdjustWndProcFlagsFromConfig(int wndProcFlags)
		{
			if (WindowsFormsSection.GetSection().JitDebugging)
			{
				wndProcFlags |= 4;
			}
			return wndProcFlags;
		}

		// Token: 0x0600314F RID: 12623 RVA: 0x000DE790 File Offset: 0x000DC990
		private static int AdjustWndProcFlagsFromRegistry(int wndProcFlags)
		{
			new RegistryPermission(PermissionState.Unrestricted).Assert();
			try
			{
				RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\.NETFramework");
				if (registryKey == null)
				{
					return wndProcFlags;
				}
				try
				{
					object value = registryKey.GetValue("DbgJITDebugLaunchSetting");
					if (value != null)
					{
						int num = 0;
						try
						{
							num = (int)value;
						}
						catch (InvalidCastException)
						{
							num = 1;
						}
						if (num != 1)
						{
							wndProcFlags |= 2;
							wndProcFlags |= 8;
						}
					}
					else if (registryKey.GetValue("DbgManagedDebugger") != null)
					{
						wndProcFlags |= 2;
						wndProcFlags |= 8;
					}
				}
				finally
				{
					registryKey.Close();
				}
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			return wndProcFlags;
		}

		// Token: 0x06003150 RID: 12624 RVA: 0x000DE840 File Offset: 0x000DCA40
		private static int AdjustWndProcFlagsFromMetadata(int wndProcFlags)
		{
			if ((wndProcFlags & 2) != 0)
			{
				Assembly entryAssembly = Assembly.GetEntryAssembly();
				if (entryAssembly != null && Attribute.IsDefined(entryAssembly, typeof(DebuggableAttribute)))
				{
					Attribute[] customAttributes = Attribute.GetCustomAttributes(entryAssembly, typeof(DebuggableAttribute));
					if (customAttributes.Length != 0)
					{
						DebuggableAttribute debuggableAttribute = (DebuggableAttribute)customAttributes[0];
						if (debuggableAttribute.IsJITTrackingEnabled)
						{
							wndProcFlags |= 16;
						}
					}
				}
			}
			return wndProcFlags;
		}

		// Token: 0x06003151 RID: 12625 RVA: 0x000DE8A0 File Offset: 0x000DCAA0
		public void AssignHandle(IntPtr handle)
		{
			this.AssignHandle(handle, true);
		}

		// Token: 0x06003152 RID: 12626 RVA: 0x000DE8AC File Offset: 0x000DCAAC
		internal void AssignHandle(IntPtr handle, bool assignUniqueID)
		{
			lock (this)
			{
				this.CheckReleased();
				this.handle = handle;
				if (DpiHelper.EnableDpiChangedHighDpiImprovements && this.windowDpiAwarenessContext != DpiAwarenessContext.DPI_AWARENESS_CONTEXT_UNSPECIFIED)
				{
					DpiAwarenessContext dpiAwarenessContext = CommonUnsafeNativeMethods.TryGetDpiAwarenessContextForWindow(this.handle);
					if (dpiAwarenessContext != DpiAwarenessContext.DPI_AWARENESS_CONTEXT_UNSPECIFIED && !CommonUnsafeNativeMethods.TryFindDpiAwarenessContextsEqual(this.windowDpiAwarenessContext, dpiAwarenessContext))
					{
						this.windowDpiAwarenessContext = dpiAwarenessContext;
					}
				}
				if (NativeWindow.userDefWindowProc == IntPtr.Zero)
				{
					string lpProcName = (Marshal.SystemDefaultCharSize == 1) ? "DefWindowProcA" : "DefWindowProcW";
					NativeWindow.userDefWindowProc = UnsafeNativeMethods.GetProcAddress(new HandleRef(null, UnsafeNativeMethods.GetModuleHandle("user32.dll")), lpProcName);
					if (NativeWindow.userDefWindowProc == IntPtr.Zero)
					{
						throw new Win32Exception();
					}
				}
				this.defWindowProc = UnsafeNativeMethods.GetWindowLong(new HandleRef(this, handle), -4);
				if (NativeWindow.WndProcShouldBeDebuggable)
				{
					this.windowProc = new NativeMethods.WndProc(this.DebuggableCallback);
				}
				else
				{
					this.windowProc = new NativeMethods.WndProc(this.Callback);
				}
				NativeWindow.AddWindowToTable(handle, this);
				UnsafeNativeMethods.SetWindowLong(new HandleRef(this, handle), -4, this.windowProc);
				this.windowProcPtr = UnsafeNativeMethods.GetWindowLong(new HandleRef(this, handle), -4);
				if (assignUniqueID && ((int)((long)UnsafeNativeMethods.GetWindowLong(new HandleRef(this, handle), -16)) & 1073741824) != 0 && (int)((long)UnsafeNativeMethods.GetWindowLong(new HandleRef(this, handle), -12)) == 0)
				{
					UnsafeNativeMethods.SetWindowLong(new HandleRef(this, handle), -12, new HandleRef(this, handle));
				}
				if (this.suppressedGC)
				{
					new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
					try
					{
						GC.ReRegisterForFinalize(this);
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
					this.suppressedGC = false;
				}
				this.OnHandleChange();
			}
		}

		// Token: 0x06003153 RID: 12627 RVA: 0x000DEA88 File Offset: 0x000DCC88
		private IntPtr Callback(IntPtr hWnd, int msg, IntPtr wparam, IntPtr lparam)
		{
			Message message = Message.Create(hWnd, msg, wparam, lparam);
			try
			{
				if (this.weakThisPtr.IsAlive && this.weakThisPtr.Target != null)
				{
					this.WndProc(ref message);
				}
				else
				{
					this.DefWndProc(ref message);
				}
			}
			catch (Exception e)
			{
				this.OnThreadException(e);
			}
			finally
			{
				if (msg == 130)
				{
					this.ReleaseHandle(false);
				}
				if (msg == NativeMethods.WM_UIUNSUBCLASS)
				{
					this.ReleaseHandle(true);
				}
			}
			return message.Result;
		}

		// Token: 0x06003154 RID: 12628 RVA: 0x000DEB1C File Offset: 0x000DCD1C
		private void CheckReleased()
		{
			if (this.handle != IntPtr.Zero)
			{
				throw new InvalidOperationException(SR.GetString("HandleAlreadyExists"));
			}
		}

		// Token: 0x06003155 RID: 12629 RVA: 0x000DEB40 File Offset: 0x000DCD40
		public virtual void CreateHandle(CreateParams cp)
		{
			IntSecurity.CreateAnyWindow.Demand();
			if ((cp.Style & 1073741824) != 1073741824 || cp.Parent == IntPtr.Zero)
			{
				IntSecurity.TopLevelWindow.Demand();
			}
			lock (this)
			{
				this.CheckReleased();
				NativeWindow.WindowClass windowClass = NativeWindow.WindowClass.Create(cp.ClassName, cp.ClassStyle);
				object obj = NativeWindow.createWindowSyncObject;
				lock (obj)
				{
					if (!(this.handle != IntPtr.Zero))
					{
						windowClass.targetWindow = this;
						IntPtr value = IntPtr.Zero;
						int error = 0;
						using (DpiHelper.EnterDpiAwarenessScope(this.windowDpiAwarenessContext))
						{
							IntPtr moduleHandle = UnsafeNativeMethods.GetModuleHandle(null);
							try
							{
								if (cp.Caption != null && cp.Caption.Length > 32767)
								{
									cp.Caption = cp.Caption.Substring(0, 32767);
								}
								value = UnsafeNativeMethods.CreateWindowEx(cp.ExStyle, windowClass.windowClassName, cp.Caption, cp.Style, cp.X, cp.Y, cp.Width, cp.Height, new HandleRef(cp, cp.Parent), NativeMethods.NullHandleRef, new HandleRef(null, moduleHandle), cp.Param);
								error = Marshal.GetLastWin32Error();
							}
							catch (NullReferenceException innerException)
							{
								throw new OutOfMemoryException(SR.GetString("ErrorCreatingHandle"), innerException);
							}
						}
						windowClass.targetWindow = null;
						if (value == IntPtr.Zero)
						{
							throw new Win32Exception(error, SR.GetString("ErrorCreatingHandle"));
						}
						this.ownHandle = true;
						System.Internal.HandleCollector.Add(value, NativeMethods.CommonHandles.Window);
					}
				}
			}
		}

		// Token: 0x06003156 RID: 12630 RVA: 0x000DED60 File Offset: 0x000DCF60
		private IntPtr DebuggableCallback(IntPtr hWnd, int msg, IntPtr wparam, IntPtr lparam)
		{
			Message message = Message.Create(hWnd, msg, wparam, lparam);
			try
			{
				if (this.weakThisPtr.IsAlive && this.weakThisPtr.Target != null)
				{
					this.WndProc(ref message);
				}
				else
				{
					this.DefWndProc(ref message);
				}
			}
			finally
			{
				if (msg == 130)
				{
					this.ReleaseHandle(false);
				}
				if (msg == NativeMethods.WM_UIUNSUBCLASS)
				{
					this.ReleaseHandle(true);
				}
			}
			return message.Result;
		}

		// Token: 0x06003157 RID: 12631 RVA: 0x000DEDDC File Offset: 0x000DCFDC
		public void DefWndProc(ref Message m)
		{
			if (this.previousWindow != null)
			{
				m.Result = this.previousWindow.Callback(m.HWnd, m.Msg, m.WParam, m.LParam);
				return;
			}
			if (this.defWindowProc == IntPtr.Zero)
			{
				m.Result = UnsafeNativeMethods.DefWindowProc(m.HWnd, m.Msg, m.WParam, m.LParam);
				return;
			}
			m.Result = UnsafeNativeMethods.CallWindowProc(this.defWindowProc, m.HWnd, m.Msg, m.WParam, m.LParam);
		}

		// Token: 0x06003158 RID: 12632 RVA: 0x000DEE7C File Offset: 0x000DD07C
		public virtual void DestroyHandle()
		{
			lock (this)
			{
				if (this.handle != IntPtr.Zero)
				{
					if (!UnsafeNativeMethods.DestroyWindow(new HandleRef(this, this.handle)))
					{
						this.UnSubclass();
						UnsafeNativeMethods.PostMessage(new HandleRef(this, this.handle), 16, 0, 0);
					}
					this.handle = IntPtr.Zero;
					this.ownHandle = false;
				}
				new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
				try
				{
					GC.SuppressFinalize(this);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
				this.suppressedGC = true;
			}
		}

		// Token: 0x06003159 RID: 12633 RVA: 0x000DEF30 File Offset: 0x000DD130
		private static void ExpandTable()
		{
			int num = NativeWindow.hashBuckets.Length;
			int prime = NativeWindow.GetPrime(1 + num * 2);
			NativeWindow.HandleBucket[] array = new NativeWindow.HandleBucket[prime];
			for (int i = 0; i < num; i++)
			{
				NativeWindow.HandleBucket handleBucket = NativeWindow.hashBuckets[i];
				if (handleBucket.handle != IntPtr.Zero && handleBucket.handle != new IntPtr(-1))
				{
					uint num2 = (uint)(handleBucket.hash_coll & int.MaxValue);
					uint num3 = 1U + ((num2 >> 5) + 1U) % (uint)(array.Length - 1);
					int num4;
					for (;;)
					{
						num4 = (int)(num2 % (uint)array.Length);
						if (array[num4].handle == IntPtr.Zero || array[num4].handle == new IntPtr(-1))
						{
							break;
						}
						NativeWindow.HandleBucket[] array2 = array;
						int num5 = num4;
						array2[num5].hash_coll = (array2[num5].hash_coll | int.MinValue);
						num2 += num3;
					}
					array[num4].window = handleBucket.window;
					array[num4].handle = handleBucket.handle;
					NativeWindow.HandleBucket[] array3 = array;
					int num6 = num4;
					array3[num6].hash_coll = (array3[num6].hash_coll | (handleBucket.hash_coll & int.MaxValue));
				}
			}
			NativeWindow.hashBuckets = array;
			NativeWindow.hashLoadSize = (int)(0.72f * (float)prime);
			if (NativeWindow.hashLoadSize >= prime)
			{
				NativeWindow.hashLoadSize = prime - 1;
			}
		}

		// Token: 0x0600315A RID: 12634 RVA: 0x000DF08B File Offset: 0x000DD28B
		public static NativeWindow FromHandle(IntPtr handle)
		{
			if (handle != IntPtr.Zero && NativeWindow.handleCount > 0)
			{
				return NativeWindow.GetWindowFromTable(handle);
			}
			return null;
		}

		// Token: 0x0600315B RID: 12635 RVA: 0x000DF0AC File Offset: 0x000DD2AC
		private static int GetPrime(int minSize)
		{
			if (minSize < 0)
			{
				throw new OutOfMemoryException();
			}
			for (int i = 0; i < NativeWindow.primes.Length; i++)
			{
				int num = NativeWindow.primes[i];
				if (num >= minSize)
				{
					return num;
				}
			}
			for (int j = minSize - 2 | 1; j < 2147483647; j += 2)
			{
				bool flag = true;
				if ((j & 1) != 0)
				{
					int num2 = (int)Math.Sqrt((double)j);
					for (int k = 3; k < num2; k += 2)
					{
						if (j % k == 0)
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						return j;
					}
				}
				else if (j == 2)
				{
					return j;
				}
			}
			return minSize;
		}

		// Token: 0x0600315C RID: 12636 RVA: 0x000DF130 File Offset: 0x000DD330
		private static NativeWindow GetWindowFromTable(IntPtr handle)
		{
			NativeWindow.HandleBucket[] array = NativeWindow.hashBuckets;
			int num = 0;
			uint num3;
			uint num4;
			uint num2 = NativeWindow.InitHash(handle, array.Length, out num3, out num4);
			NativeWindow.HandleBucket handleBucket;
			for (;;)
			{
				int num5 = (int)(num3 % (uint)array.Length);
				handleBucket = array[num5];
				if (handleBucket.handle == IntPtr.Zero)
				{
					break;
				}
				if ((long)(handleBucket.hash_coll & 2147483647) == (long)((ulong)num2) && handle == handleBucket.handle && handleBucket.window.IsAllocated)
				{
					goto Block_4;
				}
				num3 += num4;
				if (handleBucket.hash_coll >= 0 || ++num >= array.Length)
				{
					goto IL_97;
				}
			}
			return null;
			Block_4:
			return (NativeWindow)handleBucket.window.Target;
			IL_97:
			return null;
		}

		// Token: 0x0600315D RID: 12637 RVA: 0x000DF1D8 File Offset: 0x000DD3D8
		internal IntPtr GetHandleFromID(short id)
		{
			IntPtr zero;
			if (NativeWindow.hashForIdHandle == null || !NativeWindow.hashForIdHandle.TryGetValue(id, out zero))
			{
				zero = IntPtr.Zero;
			}
			return zero;
		}

		// Token: 0x0600315E RID: 12638 RVA: 0x000DF204 File Offset: 0x000DD404
		private static uint InitHash(IntPtr handle, int hashsize, out uint seed, out uint incr)
		{
			uint num = (uint)(handle.GetHashCode() & int.MaxValue);
			seed = num;
			incr = 1U + ((seed >> 5) + 1U) % (uint)(hashsize - 1);
			return num;
		}

		// Token: 0x0600315F RID: 12639 RVA: 0x000072B6 File Offset: 0x000054B6
		protected virtual void OnHandleChange()
		{
		}

		// Token: 0x06003160 RID: 12640 RVA: 0x000DF234 File Offset: 0x000DD434
		[PrePrepareMethod]
		private static void OnShutdown(object sender, EventArgs e)
		{
			if (NativeWindow.handleCount > 0)
			{
				object obj = NativeWindow.internalSyncObject;
				lock (obj)
				{
					for (int i = 0; i < NativeWindow.hashBuckets.Length; i++)
					{
						NativeWindow.HandleBucket handleBucket = NativeWindow.hashBuckets[i];
						if (handleBucket.handle != IntPtr.Zero && handleBucket.handle != new IntPtr(-1))
						{
							HandleRef handleRef = new HandleRef(handleBucket, handleBucket.handle);
							UnsafeNativeMethods.SetWindowLong(handleRef, -4, new HandleRef(null, NativeWindow.userDefWindowProc));
							UnsafeNativeMethods.SetClassLong(handleRef, -24, NativeWindow.userDefWindowProc);
							UnsafeNativeMethods.PostMessage(handleRef, 16, 0, 0);
							if (handleBucket.window.IsAllocated)
							{
								NativeWindow nativeWindow = (NativeWindow)handleBucket.window.Target;
								if (nativeWindow != null)
								{
									nativeWindow.handle = IntPtr.Zero;
								}
							}
							handleBucket.window.Free();
						}
						NativeWindow.hashBuckets[i].handle = IntPtr.Zero;
						NativeWindow.hashBuckets[i].hash_coll = 0;
					}
					NativeWindow.handleCount = 0;
				}
			}
			NativeWindow.WindowClass.DisposeCache();
		}

		// Token: 0x06003161 RID: 12641 RVA: 0x000072B6 File Offset: 0x000054B6
		protected virtual void OnThreadException(Exception e)
		{
		}

		// Token: 0x06003162 RID: 12642 RVA: 0x000DF384 File Offset: 0x000DD584
		public virtual void ReleaseHandle()
		{
			this.ReleaseHandle(true);
		}

		// Token: 0x06003163 RID: 12643 RVA: 0x000DF390 File Offset: 0x000DD590
		private void ReleaseHandle(bool handleValid)
		{
			if (this.handle != IntPtr.Zero)
			{
				lock (this)
				{
					if (this.handle != IntPtr.Zero)
					{
						if (handleValid)
						{
							this.UnSubclass();
						}
						NativeWindow.RemoveWindowFromTable(this.handle, this);
						if (this.ownHandle)
						{
							System.Internal.HandleCollector.Remove(this.handle, NativeMethods.CommonHandles.Window);
							this.ownHandle = false;
						}
						this.handle = IntPtr.Zero;
						if (this.weakThisPtr.IsAlive && this.weakThisPtr.Target != null)
						{
							this.OnHandleChange();
							new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
							try
							{
								GC.SuppressFinalize(this);
							}
							finally
							{
								CodeAccessPermission.RevertAssert();
							}
							this.suppressedGC = true;
						}
					}
				}
			}
		}

		// Token: 0x06003164 RID: 12644 RVA: 0x000DF47C File Offset: 0x000DD67C
		private static void RemoveWindowFromTable(IntPtr handle, NativeWindow window)
		{
			object obj = NativeWindow.internalSyncObject;
			lock (obj)
			{
				uint num2;
				uint num3;
				uint num = NativeWindow.InitHash(handle, NativeWindow.hashBuckets.Length, out num2, out num3);
				int num4 = 0;
				NativeWindow value = window.PreviousWindow;
				int num5;
				for (;;)
				{
					num5 = (int)(num2 % (uint)NativeWindow.hashBuckets.Length);
					NativeWindow.HandleBucket handleBucket = NativeWindow.hashBuckets[num5];
					if ((long)(handleBucket.hash_coll & 2147483647) == (long)((ulong)num) && handle == handleBucket.handle)
					{
						break;
					}
					num2 += num3;
					if (NativeWindow.hashBuckets[num5].hash_coll >= 0 || ++num4 >= NativeWindow.hashBuckets.Length)
					{
						goto IL_1ED;
					}
				}
				bool flag2 = window.nextWindow == null;
				bool flag3 = NativeWindow.IsRootWindowInListWithChildren(window);
				if (window.previousWindow != null)
				{
					window.previousWindow.nextWindow = window.nextWindow;
				}
				if (window.nextWindow != null)
				{
					window.nextWindow.defWindowProc = window.defWindowProc;
					window.nextWindow.previousWindow = window.previousWindow;
				}
				window.nextWindow = null;
				window.previousWindow = null;
				if (flag3)
				{
					if (NativeWindow.hashBuckets[num5].window.IsAllocated)
					{
						NativeWindow.hashBuckets[num5].window.Free();
					}
					NativeWindow.hashBuckets[num5].window = GCHandle.Alloc(value, GCHandleType.Weak);
				}
				else if (flag2)
				{
					NativeWindow.HandleBucket[] array = NativeWindow.hashBuckets;
					int num6 = num5;
					array[num6].hash_coll = (array[num6].hash_coll & int.MinValue);
					if (NativeWindow.hashBuckets[num5].hash_coll != 0)
					{
						NativeWindow.hashBuckets[num5].handle = new IntPtr(-1);
					}
					else
					{
						NativeWindow.hashBuckets[num5].handle = IntPtr.Zero;
					}
					if (NativeWindow.hashBuckets[num5].window.IsAllocated)
					{
						NativeWindow.hashBuckets[num5].window.Free();
					}
					NativeWindow.handleCount--;
				}
				IL_1ED:;
			}
		}

		// Token: 0x06003165 RID: 12645 RVA: 0x000DF6A0 File Offset: 0x000DD8A0
		private static bool IsRootWindowInListWithChildren(NativeWindow window)
		{
			return window.PreviousWindow != null && window.nextWindow == null;
		}

		// Token: 0x06003166 RID: 12646 RVA: 0x000DF6B8 File Offset: 0x000DD8B8
		internal static void RemoveWindowFromIDTable(IntPtr handle)
		{
			short key = NativeWindow.hashForHandleId[handle];
			NativeWindow.hashForHandleId.Remove(handle);
			NativeWindow.hashForIdHandle.Remove(key);
		}

		// Token: 0x06003167 RID: 12647 RVA: 0x000DF6EC File Offset: 0x000DD8EC
		internal static void SetUnhandledExceptionModeInternal(UnhandledExceptionMode mode, bool threadScope)
		{
			if (!threadScope && NativeWindow.anyHandleCreatedInApp)
			{
				throw new InvalidOperationException(SR.GetString("ApplicationCannotChangeApplicationExceptionMode"));
			}
			if (threadScope && NativeWindow.anyHandleCreated)
			{
				throw new InvalidOperationException(SR.GetString("ApplicationCannotChangeThreadExceptionMode"));
			}
			switch (mode)
			{
			case UnhandledExceptionMode.Automatic:
				if (threadScope)
				{
					NativeWindow.userSetProcFlags = 0;
					return;
				}
				NativeWindow.userSetProcFlagsForApp = 0;
				return;
			case UnhandledExceptionMode.ThrowException:
				if (threadScope)
				{
					NativeWindow.userSetProcFlags = 5;
					return;
				}
				NativeWindow.userSetProcFlagsForApp = 5;
				return;
			case UnhandledExceptionMode.CatchException:
				if (threadScope)
				{
					NativeWindow.userSetProcFlags = 1;
					return;
				}
				NativeWindow.userSetProcFlagsForApp = 1;
				return;
			default:
				throw new InvalidEnumArgumentException("mode", (int)mode, typeof(UnhandledExceptionMode));
			}
		}

		// Token: 0x06003168 RID: 12648 RVA: 0x000DF78C File Offset: 0x000DD98C
		private void UnSubclass()
		{
			bool flag = !this.weakThisPtr.IsAlive || this.weakThisPtr.Target == null;
			HandleRef hWnd = new HandleRef(this, this.handle);
			IntPtr windowLong = UnsafeNativeMethods.GetWindowLong(new HandleRef(this, this.handle), -4);
			if (!(this.windowProcPtr == windowLong))
			{
				if (this.nextWindow == null || this.nextWindow.defWindowProc != this.windowProcPtr)
				{
					UnsafeNativeMethods.SetWindowLong(hWnd, -4, new HandleRef(this, NativeWindow.userDefWindowProc));
				}
				return;
			}
			if (this.previousWindow == null)
			{
				UnsafeNativeMethods.SetWindowLong(hWnd, -4, new HandleRef(this, this.defWindowProc));
				return;
			}
			if (flag)
			{
				UnsafeNativeMethods.SetWindowLong(hWnd, -4, new HandleRef(this, NativeWindow.userDefWindowProc));
				return;
			}
			UnsafeNativeMethods.SetWindowLong(hWnd, -4, this.previousWindow.windowProc);
		}

		// Token: 0x06003169 RID: 12649 RVA: 0x000DF867 File Offset: 0x000DDA67
		protected virtual void WndProc(ref Message m)
		{
			this.DefWndProc(ref m);
		}

		// Token: 0x04001E09 RID: 7689
		private static readonly TraceSwitch WndProcChoice;

		// Token: 0x04001E0A RID: 7690
		private static readonly int[] primes = new int[]
		{
			11,
			17,
			23,
			29,
			37,
			47,
			59,
			71,
			89,
			107,
			131,
			163,
			197,
			239,
			293,
			353,
			431,
			521,
			631,
			761,
			919,
			1103,
			1327,
			1597,
			1931,
			2333,
			2801,
			3371,
			4049,
			4861,
			5839,
			7013,
			8419,
			10103,
			12143,
			14591,
			17519,
			21023,
			25229,
			30293,
			36353,
			43627,
			52361,
			62851,
			75431,
			90523,
			108631,
			130363,
			156437,
			187751,
			225307,
			270371,
			324449,
			389357,
			467237,
			560689,
			672827,
			807403,
			968897,
			1162687,
			1395263,
			1674319,
			2009191,
			2411033,
			2893249,
			3471899,
			4166287,
			4999559,
			5999471,
			7199369
		};

		// Token: 0x04001E0B RID: 7691
		private const int InitializedFlags = 1;

		// Token: 0x04001E0C RID: 7692
		private const int DebuggerPresent = 2;

		// Token: 0x04001E0D RID: 7693
		private const int UseDebuggableWndProc = 4;

		// Token: 0x04001E0E RID: 7694
		private const int LoadConfigSettings = 8;

		// Token: 0x04001E0F RID: 7695
		private const int AssemblyIsDebuggable = 16;

		// Token: 0x04001E10 RID: 7696
		[ThreadStatic]
		private static bool anyHandleCreated;

		// Token: 0x04001E11 RID: 7697
		private static bool anyHandleCreatedInApp;

		// Token: 0x04001E12 RID: 7698
		private const float hashLoadFactor = 0.72f;

		// Token: 0x04001E13 RID: 7699
		private static int handleCount;

		// Token: 0x04001E14 RID: 7700
		private static int hashLoadSize;

		// Token: 0x04001E15 RID: 7701
		private static NativeWindow.HandleBucket[] hashBuckets;

		// Token: 0x04001E16 RID: 7702
		private static IntPtr userDefWindowProc;

		// Token: 0x04001E17 RID: 7703
		[ThreadStatic]
		private static byte wndProcFlags = 0;

		// Token: 0x04001E18 RID: 7704
		[ThreadStatic]
		private static byte userSetProcFlags = 0;

		// Token: 0x04001E19 RID: 7705
		private static byte userSetProcFlagsForApp;

		// Token: 0x04001E1A RID: 7706
		private static short globalID = 1;

		// Token: 0x04001E1B RID: 7707
		private static Dictionary<short, IntPtr> hashForIdHandle;

		// Token: 0x04001E1C RID: 7708
		private static Dictionary<IntPtr, short> hashForHandleId;

		// Token: 0x04001E1D RID: 7709
		private static object internalSyncObject = new object();

		// Token: 0x04001E1E RID: 7710
		private static object createWindowSyncObject = new object();

		// Token: 0x04001E1F RID: 7711
		private IntPtr handle;

		// Token: 0x04001E20 RID: 7712
		private NativeMethods.WndProc windowProc;

		// Token: 0x04001E21 RID: 7713
		private IntPtr windowProcPtr;

		// Token: 0x04001E22 RID: 7714
		private IntPtr defWindowProc;

		// Token: 0x04001E23 RID: 7715
		private bool suppressedGC;

		// Token: 0x04001E24 RID: 7716
		private bool ownHandle;

		// Token: 0x04001E25 RID: 7717
		private NativeWindow previousWindow;

		// Token: 0x04001E26 RID: 7718
		private NativeWindow nextWindow;

		// Token: 0x04001E27 RID: 7719
		private WeakReference weakThisPtr;

		// Token: 0x04001E28 RID: 7720
		private DpiAwarenessContext windowDpiAwarenessContext = DpiHelper.EnableDpiChangedHighDpiImprovements ? CommonUnsafeNativeMethods.TryGetThreadDpiAwarenessContext() : DpiAwarenessContext.DPI_AWARENESS_CONTEXT_UNSPECIFIED;

		// Token: 0x020007C7 RID: 1991
		private struct HandleBucket
		{
			// Token: 0x040041B5 RID: 16821
			public IntPtr handle;

			// Token: 0x040041B6 RID: 16822
			public GCHandle window;

			// Token: 0x040041B7 RID: 16823
			public int hash_coll;
		}

		// Token: 0x020007C8 RID: 1992
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		private class WindowClass
		{
			// Token: 0x06006D70 RID: 28016 RVA: 0x0019217F File Offset: 0x0019037F
			internal WindowClass(string className, int classStyle)
			{
				this.className = className;
				this.classStyle = classStyle;
				this.RegisterClass();
			}

			// Token: 0x06006D71 RID: 28017 RVA: 0x0019219B File Offset: 0x0019039B
			public IntPtr Callback(IntPtr hWnd, int msg, IntPtr wparam, IntPtr lparam)
			{
				UnsafeNativeMethods.SetWindowLong(new HandleRef(null, hWnd), -4, new HandleRef(this, this.defWindowProc));
				this.targetWindow.AssignHandle(hWnd);
				return this.targetWindow.Callback(hWnd, msg, wparam, lparam);
			}

			// Token: 0x06006D72 RID: 28018 RVA: 0x001921D4 File Offset: 0x001903D4
			internal static NativeWindow.WindowClass Create(string className, int classStyle)
			{
				object obj = NativeWindow.WindowClass.wcInternalSyncObject;
				NativeWindow.WindowClass result;
				lock (obj)
				{
					NativeWindow.WindowClass windowClass = NativeWindow.WindowClass.cache;
					if (className == null)
					{
						while (windowClass != null)
						{
							if (windowClass.className == null && windowClass.classStyle == classStyle)
							{
								break;
							}
							windowClass = windowClass.next;
						}
					}
					else
					{
						while (windowClass != null && !className.Equals(windowClass.className))
						{
							windowClass = windowClass.next;
						}
					}
					if (windowClass == null)
					{
						windowClass = new NativeWindow.WindowClass(className, classStyle);
						windowClass.next = NativeWindow.WindowClass.cache;
						NativeWindow.WindowClass.cache = windowClass;
					}
					else if (!windowClass.registered)
					{
						windowClass.RegisterClass();
					}
					result = windowClass;
				}
				return result;
			}

			// Token: 0x06006D73 RID: 28019 RVA: 0x0019227C File Offset: 0x0019047C
			internal static void DisposeCache()
			{
				object obj = NativeWindow.WindowClass.wcInternalSyncObject;
				lock (obj)
				{
					for (NativeWindow.WindowClass windowClass = NativeWindow.WindowClass.cache; windowClass != null; windowClass = windowClass.next)
					{
						windowClass.UnregisterClass();
					}
				}
			}

			// Token: 0x06006D74 RID: 28020 RVA: 0x001922D0 File Offset: 0x001904D0
			private string GetFullClassName(string className)
			{
				StringBuilder stringBuilder = new StringBuilder(50);
				stringBuilder.Append(Application.WindowsFormsVersion);
				stringBuilder.Append('.');
				stringBuilder.Append(className);
				stringBuilder.Append(".app.");
				stringBuilder.Append(NativeWindow.WindowClass.domainQualifier);
				stringBuilder.Append('.');
				string name = Convert.ToString(AppDomain.CurrentDomain.GetHashCode(), 16);
				stringBuilder.Append(VersioningHelper.MakeVersionSafeName(name, ResourceScope.Process, ResourceScope.AppDomain));
				return stringBuilder.ToString();
			}

			// Token: 0x06006D75 RID: 28021 RVA: 0x0019234C File Offset: 0x0019054C
			private void RegisterClass()
			{
				NativeMethods.WNDCLASS_D wndclass_D = new NativeMethods.WNDCLASS_D();
				if (NativeWindow.userDefWindowProc == IntPtr.Zero)
				{
					string lpProcName = (Marshal.SystemDefaultCharSize == 1) ? "DefWindowProcA" : "DefWindowProcW";
					NativeWindow.userDefWindowProc = UnsafeNativeMethods.GetProcAddress(new HandleRef(null, UnsafeNativeMethods.GetModuleHandle("user32.dll")), lpProcName);
					if (NativeWindow.userDefWindowProc == IntPtr.Zero)
					{
						throw new Win32Exception();
					}
				}
				string text;
				if (this.className == null)
				{
					wndclass_D.hbrBackground = UnsafeNativeMethods.GetStockObject(5);
					wndclass_D.style = this.classStyle;
					this.defWindowProc = NativeWindow.userDefWindowProc;
					text = "Window." + Convert.ToString(this.classStyle, 16);
					this.hashCode = 0;
				}
				else
				{
					NativeMethods.WNDCLASS_I wndclass_I = new NativeMethods.WNDCLASS_I();
					bool classInfo = UnsafeNativeMethods.GetClassInfo(NativeMethods.NullHandleRef, this.className, wndclass_I);
					int lastWin32Error = Marshal.GetLastWin32Error();
					if (!classInfo)
					{
						throw new Win32Exception(lastWin32Error, SR.GetString("InvalidWndClsName"));
					}
					wndclass_D.style = wndclass_I.style;
					wndclass_D.cbClsExtra = wndclass_I.cbClsExtra;
					wndclass_D.cbWndExtra = wndclass_I.cbWndExtra;
					wndclass_D.hIcon = wndclass_I.hIcon;
					wndclass_D.hCursor = wndclass_I.hCursor;
					wndclass_D.hbrBackground = wndclass_I.hbrBackground;
					wndclass_D.lpszMenuName = Marshal.PtrToStringAuto(wndclass_I.lpszMenuName);
					text = this.className;
					this.defWindowProc = wndclass_I.lpfnWndProc;
					this.hashCode = this.className.GetHashCode();
				}
				this.windowClassName = this.GetFullClassName(text);
				this.windowProc = new NativeMethods.WndProc(this.Callback);
				wndclass_D.lpfnWndProc = this.windowProc;
				wndclass_D.hInstance = UnsafeNativeMethods.GetModuleHandle(null);
				wndclass_D.lpszClassName = this.windowClassName;
				short num = UnsafeNativeMethods.RegisterClass(wndclass_D);
				if (num == 0)
				{
					int lastWin32Error2 = Marshal.GetLastWin32Error();
					if (lastWin32Error2 == 1410)
					{
						NativeMethods.WNDCLASS_I wndclass_I2 = new NativeMethods.WNDCLASS_I();
						bool classInfo2 = UnsafeNativeMethods.GetClassInfo(new HandleRef(null, UnsafeNativeMethods.GetModuleHandle(null)), this.windowClassName, wndclass_I2);
						if (classInfo2 && wndclass_I2.lpfnWndProc == NativeWindow.UserDefindowProc)
						{
							if (UnsafeNativeMethods.UnregisterClass(this.windowClassName, new HandleRef(null, UnsafeNativeMethods.GetModuleHandle(null))))
							{
								num = UnsafeNativeMethods.RegisterClass(wndclass_D);
							}
							else
							{
								do
								{
									NativeWindow.WindowClass.domainQualifier++;
									this.windowClassName = this.GetFullClassName(text);
									wndclass_D.lpszClassName = this.windowClassName;
									num = UnsafeNativeMethods.RegisterClass(wndclass_D);
								}
								while (num == 0 && Marshal.GetLastWin32Error() == 1410);
							}
						}
					}
					if (num == 0)
					{
						this.windowProc = null;
						throw new Win32Exception(lastWin32Error2);
					}
				}
				this.registered = true;
			}

			// Token: 0x06006D76 RID: 28022 RVA: 0x001925D7 File Offset: 0x001907D7
			private void UnregisterClass()
			{
				if (this.registered && UnsafeNativeMethods.UnregisterClass(this.windowClassName, new HandleRef(null, UnsafeNativeMethods.GetModuleHandle(null))))
				{
					this.windowProc = null;
					this.registered = false;
				}
			}

			// Token: 0x040041B8 RID: 16824
			internal static NativeWindow.WindowClass cache;

			// Token: 0x040041B9 RID: 16825
			internal NativeWindow.WindowClass next;

			// Token: 0x040041BA RID: 16826
			internal string className;

			// Token: 0x040041BB RID: 16827
			internal int classStyle;

			// Token: 0x040041BC RID: 16828
			internal string windowClassName;

			// Token: 0x040041BD RID: 16829
			internal int hashCode;

			// Token: 0x040041BE RID: 16830
			internal IntPtr defWindowProc;

			// Token: 0x040041BF RID: 16831
			internal NativeMethods.WndProc windowProc;

			// Token: 0x040041C0 RID: 16832
			internal bool registered;

			// Token: 0x040041C1 RID: 16833
			internal NativeWindow targetWindow;

			// Token: 0x040041C2 RID: 16834
			private static object wcInternalSyncObject = new object();

			// Token: 0x040041C3 RID: 16835
			private static int domainQualifier = 0;
		}
	}
}
