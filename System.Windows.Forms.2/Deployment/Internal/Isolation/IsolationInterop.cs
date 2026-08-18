using System;
using System.Deployment.Internal.Isolation.Manifest;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000065 RID: 101
	internal static class IsolationInterop
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001DE RID: 478 RVA: 0x00008174 File Offset: 0x00006374
		public static Store UserStore
		{
			get
			{
				if (IsolationInterop._userStore == null)
				{
					object synchObject = IsolationInterop._synchObject;
					lock (synchObject)
					{
						if (IsolationInterop._userStore == null)
						{
							IsolationInterop._userStore = new Store(IsolationInterop.GetUserStore(0U, IntPtr.Zero, ref IsolationInterop.IID_IStore) as IStore);
						}
					}
				}
				return IsolationInterop._userStore;
			}
		}

		// Token: 0x060001DF RID: 479 RVA: 0x000081E0 File Offset: 0x000063E0
		[SecuritySafeCritical]
		public static Store GetUserStore()
		{
			return new Store(IsolationInterop.GetUserStore(0U, IntPtr.Zero, ref IsolationInterop.IID_IStore) as IStore);
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x000081FC File Offset: 0x000063FC
		public static Store SystemStore
		{
			get
			{
				if (IsolationInterop._systemStore == null)
				{
					object synchObject = IsolationInterop._synchObject;
					lock (synchObject)
					{
						if (IsolationInterop._systemStore == null)
						{
							IsolationInterop._systemStore = new Store(IsolationInterop.GetSystemStore(0U, ref IsolationInterop.IID_IStore) as IStore);
						}
					}
				}
				return IsolationInterop._systemStore;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00008264 File Offset: 0x00006464
		public static IIdentityAuthority IdentityAuthority
		{
			[SecuritySafeCritical]
			get
			{
				if (IsolationInterop._idAuth == null)
				{
					object synchObject = IsolationInterop._synchObject;
					lock (synchObject)
					{
						if (IsolationInterop._idAuth == null)
						{
							IsolationInterop._idAuth = IsolationInterop.GetIdentityAuthority();
						}
					}
				}
				return IsolationInterop._idAuth;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x000082BC File Offset: 0x000064BC
		public static IAppIdAuthority AppIdAuthority
		{
			[SecuritySafeCritical]
			get
			{
				if (IsolationInterop._appIdAuth == null)
				{
					object synchObject = IsolationInterop._synchObject;
					lock (synchObject)
					{
						if (IsolationInterop._appIdAuth == null)
						{
							IsolationInterop._appIdAuth = IsolationInterop.GetAppIdAuthority();
						}
					}
				}
				return IsolationInterop._appIdAuth;
			}
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00008314 File Offset: 0x00006514
		[SecuritySafeCritical]
		internal static IActContext CreateActContext(IDefinitionAppId AppId)
		{
			IsolationInterop.CreateActContextParameters createActContextParameters;
			createActContextParameters.Size = (uint)Marshal.SizeOf(typeof(IsolationInterop.CreateActContextParameters));
			createActContextParameters.Flags = 16U;
			createActContextParameters.CustomStoreList = IntPtr.Zero;
			createActContextParameters.CultureFallbackList = IntPtr.Zero;
			createActContextParameters.ProcessorArchitectureList = IntPtr.Zero;
			createActContextParameters.Source = IntPtr.Zero;
			createActContextParameters.ProcArch = 0;
			IsolationInterop.CreateActContextParametersSource createActContextParametersSource;
			createActContextParametersSource.Size = (uint)Marshal.SizeOf(typeof(IsolationInterop.CreateActContextParametersSource));
			createActContextParametersSource.Flags = 0U;
			createActContextParametersSource.SourceType = 1U;
			createActContextParametersSource.Data = IntPtr.Zero;
			IsolationInterop.CreateActContextParametersSourceDefinitionAppid createActContextParametersSourceDefinitionAppid;
			createActContextParametersSourceDefinitionAppid.Size = (uint)Marshal.SizeOf(typeof(IsolationInterop.CreateActContextParametersSourceDefinitionAppid));
			createActContextParametersSourceDefinitionAppid.Flags = 0U;
			createActContextParametersSourceDefinitionAppid.AppId = AppId;
			IActContext result;
			try
			{
				createActContextParametersSource.Data = createActContextParametersSourceDefinitionAppid.ToIntPtr();
				createActContextParameters.Source = createActContextParametersSource.ToIntPtr();
				result = (IsolationInterop.CreateActContext(ref createActContextParameters) as IActContext);
			}
			finally
			{
				if (createActContextParametersSource.Data != IntPtr.Zero)
				{
					IsolationInterop.CreateActContextParametersSourceDefinitionAppid.Destroy(createActContextParametersSource.Data);
					createActContextParametersSource.Data = IntPtr.Zero;
				}
				if (createActContextParameters.Source != IntPtr.Zero)
				{
					IsolationInterop.CreateActContextParametersSource.Destroy(createActContextParameters.Source);
					createActContextParameters.Source = IntPtr.Zero;
				}
			}
			return result;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00008460 File Offset: 0x00006660
		internal static IActContext CreateActContext(IReferenceAppId AppId)
		{
			IsolationInterop.CreateActContextParameters createActContextParameters;
			createActContextParameters.Size = (uint)Marshal.SizeOf(typeof(IsolationInterop.CreateActContextParameters));
			createActContextParameters.Flags = 16U;
			createActContextParameters.CustomStoreList = IntPtr.Zero;
			createActContextParameters.CultureFallbackList = IntPtr.Zero;
			createActContextParameters.ProcessorArchitectureList = IntPtr.Zero;
			createActContextParameters.Source = IntPtr.Zero;
			createActContextParameters.ProcArch = 0;
			IsolationInterop.CreateActContextParametersSource createActContextParametersSource;
			createActContextParametersSource.Size = (uint)Marshal.SizeOf(typeof(IsolationInterop.CreateActContextParametersSource));
			createActContextParametersSource.Flags = 0U;
			createActContextParametersSource.SourceType = 2U;
			createActContextParametersSource.Data = IntPtr.Zero;
			IsolationInterop.CreateActContextParametersSourceReferenceAppid createActContextParametersSourceReferenceAppid;
			createActContextParametersSourceReferenceAppid.Size = (uint)Marshal.SizeOf(typeof(IsolationInterop.CreateActContextParametersSourceReferenceAppid));
			createActContextParametersSourceReferenceAppid.Flags = 0U;
			createActContextParametersSourceReferenceAppid.AppId = AppId;
			IActContext result;
			try
			{
				createActContextParametersSource.Data = createActContextParametersSourceReferenceAppid.ToIntPtr();
				createActContextParameters.Source = createActContextParametersSource.ToIntPtr();
				result = (IsolationInterop.CreateActContext(ref createActContextParameters) as IActContext);
			}
			finally
			{
				if (createActContextParametersSource.Data != IntPtr.Zero)
				{
					IsolationInterop.CreateActContextParametersSourceDefinitionAppid.Destroy(createActContextParametersSource.Data);
					createActContextParametersSource.Data = IntPtr.Zero;
				}
				if (createActContextParameters.Source != IntPtr.Zero)
				{
					IsolationInterop.CreateActContextParametersSource.Destroy(createActContextParameters.Source);
					createActContextParameters.Source = IntPtr.Zero;
				}
			}
			return result;
		}

		// Token: 0x060001E5 RID: 485
		[DllImport("clr.dll", PreserveSig = false)]
		[return: MarshalAs(UnmanagedType.IUnknown)]
		internal static extern object CreateActContext(ref IsolationInterop.CreateActContextParameters Params);

		// Token: 0x060001E6 RID: 486
		[SecurityCritical]
		[DllImport("clr.dll", PreserveSig = false)]
		[return: MarshalAs(UnmanagedType.IUnknown)]
		internal static extern object CreateCMSFromXml([In] byte[] buffer, [In] uint bufferSize, [In] IManifestParseErrorCallback Callback, [In] ref Guid riid);

		// Token: 0x060001E7 RID: 487
		[SecurityCritical]
		[DllImport("clr.dll", PreserveSig = false)]
		[return: MarshalAs(UnmanagedType.IUnknown)]
		internal static extern object ParseManifest([MarshalAs(UnmanagedType.LPWStr)] [In] string pszManifestPath, [In] IManifestParseErrorCallback pIManifestParseErrorCallback, [In] ref Guid riid);

		// Token: 0x060001E8 RID: 488
		[SecurityCritical]
		[DllImport("clr.dll", PreserveSig = false)]
		[return: MarshalAs(UnmanagedType.IUnknown)]
		private static extern object GetUserStore([In] uint Flags, [In] IntPtr hToken, [In] ref Guid riid);

		// Token: 0x060001E9 RID: 489
		[SecurityCritical]
		[DllImport("clr.dll", PreserveSig = false)]
		[return: MarshalAs(UnmanagedType.IUnknown)]
		private static extern object GetSystemStore([In] uint Flags, [In] ref Guid riid);

		// Token: 0x060001EA RID: 490
		[SecurityCritical]
		[DllImport("clr.dll", PreserveSig = false)]
		[return: MarshalAs(UnmanagedType.Interface)]
		private static extern IIdentityAuthority GetIdentityAuthority();

		// Token: 0x060001EB RID: 491
		[SecurityCritical]
		[DllImport("clr.dll", PreserveSig = false)]
		[return: MarshalAs(UnmanagedType.Interface)]
		private static extern IAppIdAuthority GetAppIdAuthority();

		// Token: 0x060001EC RID: 492
		[DllImport("clr.dll", PreserveSig = false)]
		[return: MarshalAs(UnmanagedType.IUnknown)]
		internal static extern object GetUserStateManager([In] uint Flags, [In] IntPtr hToken, [In] ref Guid riid);

		// Token: 0x060001ED RID: 493 RVA: 0x000085AC File Offset: 0x000067AC
		internal static Guid GetGuidOfType(Type type)
		{
			GuidAttribute guidAttribute = (GuidAttribute)Attribute.GetCustomAttribute(type, typeof(GuidAttribute), false);
			return new Guid(guidAttribute.Value);
		}

		// Token: 0x040001A9 RID: 425
		private static object _synchObject = new object();

		// Token: 0x040001AA RID: 426
		private static Store _userStore = null;

		// Token: 0x040001AB RID: 427
		private static Store _systemStore = null;

		// Token: 0x040001AC RID: 428
		private static IIdentityAuthority _idAuth = null;

		// Token: 0x040001AD RID: 429
		private static IAppIdAuthority _appIdAuth = null;

		// Token: 0x040001AE RID: 430
		public const string IsolationDllName = "clr.dll";

		// Token: 0x040001AF RID: 431
		public static Guid IID_ICMS = IsolationInterop.GetGuidOfType(typeof(ICMS));

		// Token: 0x040001B0 RID: 432
		public static Guid IID_IDefinitionIdentity = IsolationInterop.GetGuidOfType(typeof(IDefinitionIdentity));

		// Token: 0x040001B1 RID: 433
		public static Guid IID_IManifestInformation = IsolationInterop.GetGuidOfType(typeof(IManifestInformation));

		// Token: 0x040001B2 RID: 434
		public static Guid IID_IEnumSTORE_ASSEMBLY = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_ASSEMBLY));

		// Token: 0x040001B3 RID: 435
		public static Guid IID_IEnumSTORE_ASSEMBLY_FILE = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_ASSEMBLY_FILE));

		// Token: 0x040001B4 RID: 436
		public static Guid IID_IEnumSTORE_CATEGORY = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_CATEGORY));

		// Token: 0x040001B5 RID: 437
		public static Guid IID_IEnumSTORE_CATEGORY_INSTANCE = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_CATEGORY_INSTANCE));

		// Token: 0x040001B6 RID: 438
		public static Guid IID_IEnumSTORE_DEPLOYMENT_METADATA = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_DEPLOYMENT_METADATA));

		// Token: 0x040001B7 RID: 439
		public static Guid IID_IEnumSTORE_DEPLOYMENT_METADATA_PROPERTY = IsolationInterop.GetGuidOfType(typeof(IEnumSTORE_DEPLOYMENT_METADATA_PROPERTY));

		// Token: 0x040001B8 RID: 440
		public static Guid IID_IStore = IsolationInterop.GetGuidOfType(typeof(IStore));

		// Token: 0x040001B9 RID: 441
		public static Guid GUID_SXS_INSTALL_REFERENCE_SCHEME_OPAQUESTRING = new Guid("2ec93463-b0c3-45e1-8364-327e96aea856");

		// Token: 0x040001BA RID: 442
		public static Guid SXS_INSTALL_REFERENCE_SCHEME_SXS_STRONGNAME_SIGNED_PRIVATE_ASSEMBLY = new Guid("3ab20ac0-67e8-4512-8385-a487e35df3da");

		// Token: 0x0200053D RID: 1341
		internal struct CreateActContextParameters
		{
			// Token: 0x040037F1 RID: 14321
			[MarshalAs(UnmanagedType.U4)]
			public uint Size;

			// Token: 0x040037F2 RID: 14322
			[MarshalAs(UnmanagedType.U4)]
			public uint Flags;

			// Token: 0x040037F3 RID: 14323
			[MarshalAs(UnmanagedType.SysInt)]
			public IntPtr CustomStoreList;

			// Token: 0x040037F4 RID: 14324
			[MarshalAs(UnmanagedType.SysInt)]
			public IntPtr CultureFallbackList;

			// Token: 0x040037F5 RID: 14325
			[MarshalAs(UnmanagedType.SysInt)]
			public IntPtr ProcessorArchitectureList;

			// Token: 0x040037F6 RID: 14326
			[MarshalAs(UnmanagedType.SysInt)]
			public IntPtr Source;

			// Token: 0x040037F7 RID: 14327
			[MarshalAs(UnmanagedType.U2)]
			public ushort ProcArch;

			// Token: 0x020008A3 RID: 2211
			[Flags]
			public enum CreateFlags
			{
				// Token: 0x040044CE RID: 17614
				Nothing = 0,
				// Token: 0x040044CF RID: 17615
				StoreListValid = 1,
				// Token: 0x040044D0 RID: 17616
				CultureListValid = 2,
				// Token: 0x040044D1 RID: 17617
				ProcessorFallbackListValid = 4,
				// Token: 0x040044D2 RID: 17618
				ProcessorValid = 8,
				// Token: 0x040044D3 RID: 17619
				SourceValid = 16,
				// Token: 0x040044D4 RID: 17620
				IgnoreVisibility = 32
			}
		}

		// Token: 0x0200053E RID: 1342
		internal struct CreateActContextParametersSource
		{
			// Token: 0x0600555A RID: 21850 RVA: 0x00166038 File Offset: 0x00164238
			[SecurityCritical]
			public IntPtr ToIntPtr()
			{
				IntPtr intPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(this));
				Marshal.StructureToPtr(this, intPtr, false);
				return intPtr;
			}

			// Token: 0x0600555B RID: 21851 RVA: 0x0016606E File Offset: 0x0016426E
			[SecurityCritical]
			public static void Destroy(IntPtr p)
			{
				Marshal.DestroyStructure(p, typeof(IsolationInterop.CreateActContextParametersSource));
				Marshal.FreeCoTaskMem(p);
			}

			// Token: 0x040037F8 RID: 14328
			[MarshalAs(UnmanagedType.U4)]
			public uint Size;

			// Token: 0x040037F9 RID: 14329
			[MarshalAs(UnmanagedType.U4)]
			public uint Flags;

			// Token: 0x040037FA RID: 14330
			[MarshalAs(UnmanagedType.U4)]
			public uint SourceType;

			// Token: 0x040037FB RID: 14331
			[MarshalAs(UnmanagedType.SysInt)]
			public IntPtr Data;

			// Token: 0x020008A4 RID: 2212
			[Flags]
			public enum SourceFlags
			{
				// Token: 0x040044D6 RID: 17622
				Definition = 1,
				// Token: 0x040044D7 RID: 17623
				Reference = 2
			}
		}

		// Token: 0x0200053F RID: 1343
		internal struct CreateActContextParametersSourceReferenceAppid
		{
			// Token: 0x0600555C RID: 21852 RVA: 0x00166088 File Offset: 0x00164288
			public IntPtr ToIntPtr()
			{
				IntPtr intPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(this));
				Marshal.StructureToPtr(this, intPtr, false);
				return intPtr;
			}

			// Token: 0x0600555D RID: 21853 RVA: 0x001660BE File Offset: 0x001642BE
			public static void Destroy(IntPtr p)
			{
				Marshal.DestroyStructure(p, typeof(IsolationInterop.CreateActContextParametersSourceReferenceAppid));
				Marshal.FreeCoTaskMem(p);
			}

			// Token: 0x040037FC RID: 14332
			[MarshalAs(UnmanagedType.U4)]
			public uint Size;

			// Token: 0x040037FD RID: 14333
			[MarshalAs(UnmanagedType.U4)]
			public uint Flags;

			// Token: 0x040037FE RID: 14334
			public IReferenceAppId AppId;
		}

		// Token: 0x02000540 RID: 1344
		internal struct CreateActContextParametersSourceDefinitionAppid
		{
			// Token: 0x0600555E RID: 21854 RVA: 0x001660D8 File Offset: 0x001642D8
			[SecurityCritical]
			public IntPtr ToIntPtr()
			{
				IntPtr intPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(this));
				Marshal.StructureToPtr(this, intPtr, false);
				return intPtr;
			}

			// Token: 0x0600555F RID: 21855 RVA: 0x0016610E File Offset: 0x0016430E
			[SecurityCritical]
			public static void Destroy(IntPtr p)
			{
				Marshal.DestroyStructure(p, typeof(IsolationInterop.CreateActContextParametersSourceDefinitionAppid));
				Marshal.FreeCoTaskMem(p);
			}

			// Token: 0x040037FF RID: 14335
			[MarshalAs(UnmanagedType.U4)]
			public uint Size;

			// Token: 0x04003800 RID: 14336
			[MarshalAs(UnmanagedType.U4)]
			public uint Flags;

			// Token: 0x04003801 RID: 14337
			public IDefinitionAppId AppId;
		}
	}
}
