using System;
using System.Collections;
using System.Configuration.Assemblies;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices.TCEAdapterGen;
using System.Security.Permissions;
using System.Threading;
using Microsoft.Win32;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000534 RID: 1332
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	[Guid("F1C3BF79-C3E4-11d3-88E7-00902754C43A")]
	public sealed class TypeLibConverter : ITypeLibConverter
	{
		// Token: 0x06003322 RID: 13090 RVA: 0x000AD3E0 File Offset: 0x000AC3E0
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public AssemblyBuilder ConvertTypeLibToAssembly([MarshalAs(UnmanagedType.Interface)] object typeLib, string asmFileName, int flags, ITypeLibImporterNotifySink notifySink, byte[] publicKey, StrongNameKeyPair keyPair, bool unsafeInterfaces)
		{
			return this.ConvertTypeLibToAssembly(typeLib, asmFileName, unsafeInterfaces ? TypeLibImporterFlags.UnsafeInterfaces : TypeLibImporterFlags.None, notifySink, publicKey, keyPair, null, null);
		}

		// Token: 0x06003323 RID: 13091 RVA: 0x000AD408 File Offset: 0x000AC408
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public AssemblyBuilder ConvertTypeLibToAssembly([MarshalAs(UnmanagedType.Interface)] object typeLib, string asmFileName, TypeLibImporterFlags flags, ITypeLibImporterNotifySink notifySink, byte[] publicKey, StrongNameKeyPair keyPair, string asmNamespace, Version asmVersion)
		{
			ArrayList arrayList = null;
			if (typeLib == null)
			{
				throw new ArgumentNullException("typeLib");
			}
			if (asmFileName == null)
			{
				throw new ArgumentNullException("asmFileName");
			}
			if (notifySink == null)
			{
				throw new ArgumentNullException("notifySink");
			}
			if (string.Empty.Equals(asmFileName))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_InvalidFileName"), "asmFileName");
			}
			if (asmFileName.Length > 260)
			{
				throw new ArgumentException(Environment.GetResourceString("IO.PathTooLong"), asmFileName);
			}
			if ((flags & TypeLibImporterFlags.PrimaryInteropAssembly) != TypeLibImporterFlags.None && publicKey == null && keyPair == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_PIAMustBeStrongNamed"));
			}
			AssemblyNameFlags asmNameFlags = AssemblyNameFlags.None;
			AssemblyName assemblyNameFromTypelib = TypeLibConverter.GetAssemblyNameFromTypelib(typeLib, asmFileName, publicKey, keyPair, asmVersion, asmNameFlags);
			AssemblyBuilder assemblyBuilder = TypeLibConverter.CreateAssemblyForTypeLib(typeLib, asmFileName, assemblyNameFromTypelib, (flags & TypeLibImporterFlags.PrimaryInteropAssembly) != TypeLibImporterFlags.None, (flags & TypeLibImporterFlags.ReflectionOnlyLoading) != TypeLibImporterFlags.None);
			string fileName = Path.GetFileName(asmFileName);
			ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule(fileName, fileName);
			if (asmNamespace == null)
			{
				asmNamespace = assemblyNameFromTypelib.Name;
			}
			TypeLibConverter.TypeResolveHandler typeResolveHandler = new TypeLibConverter.TypeResolveHandler(moduleBuilder, notifySink);
			AppDomain domain = Thread.GetDomain();
			ResolveEventHandler value = new ResolveEventHandler(typeResolveHandler.ResolveEvent);
			ResolveEventHandler value2 = new ResolveEventHandler(typeResolveHandler.ResolveAsmEvent);
			ResolveEventHandler value3 = new ResolveEventHandler(typeResolveHandler.ResolveROAsmEvent);
			domain.TypeResolve += value;
			domain.AssemblyResolve += value2;
			domain.ReflectionOnlyAssemblyResolve += value3;
			TypeLibConverter.nConvertTypeLibToMetadata(typeLib, assemblyBuilder.InternalAssembly, moduleBuilder.InternalModule, asmNamespace, flags, typeResolveHandler, out arrayList);
			TypeLibConverter.UpdateComTypesInAssembly(assemblyBuilder, moduleBuilder);
			if (arrayList.Count > 0)
			{
				new TCEAdapterGenerator().Process(moduleBuilder, arrayList);
			}
			domain.TypeResolve -= value;
			domain.AssemblyResolve -= value2;
			domain.ReflectionOnlyAssemblyResolve -= value3;
			return assemblyBuilder;
		}

		// Token: 0x06003324 RID: 13092 RVA: 0x000AD59D File Offset: 0x000AC59D
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		[return: MarshalAs(UnmanagedType.Interface)]
		public object ConvertAssemblyToTypeLib(Assembly assembly, string strTypeLibName, TypeLibExporterFlags flags, ITypeLibExporterNotifySink notifySink)
		{
			return TypeLibConverter.nConvertAssemblyToTypeLib((assembly == null) ? null : assembly.InternalAssembly, strTypeLibName, flags, notifySink);
		}

		// Token: 0x06003325 RID: 13093 RVA: 0x000AD5B4 File Offset: 0x000AC5B4
		public bool GetPrimaryInteropAssembly(Guid g, int major, int minor, int lcid, out string asmName, out string asmCodeBase)
		{
			string name = "{" + g.ToString().ToUpper(CultureInfo.InvariantCulture) + "}";
			string name2 = major.ToString("x", CultureInfo.InvariantCulture) + "." + minor.ToString("x", CultureInfo.InvariantCulture);
			asmName = null;
			asmCodeBase = null;
			using (RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey("TypeLib", false))
			{
				if (registryKey != null)
				{
					using (RegistryKey registryKey2 = registryKey.OpenSubKey(name))
					{
						if (registryKey2 != null)
						{
							using (RegistryKey registryKey3 = registryKey2.OpenSubKey(name2, false))
							{
								if (registryKey3 != null)
								{
									asmName = (string)registryKey3.GetValue("PrimaryInteropAssemblyName");
									asmCodeBase = (string)registryKey3.GetValue("PrimaryInteropAssemblyCodeBase");
								}
							}
						}
					}
				}
			}
			return asmName != null;
		}

		// Token: 0x06003326 RID: 13094 RVA: 0x000AD6CC File Offset: 0x000AC6CC
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static AssemblyBuilder CreateAssemblyForTypeLib(object typeLib, string asmFileName, AssemblyName asmName, bool bPrimaryInteropAssembly, bool bReflectionOnly)
		{
			AppDomain domain = Thread.GetDomain();
			string text = null;
			if (asmFileName != null)
			{
				text = Path.GetDirectoryName(asmFileName);
				if (string.Empty.Equals(text))
				{
					text = null;
				}
			}
			AssemblyBuilderAccess access;
			if (bReflectionOnly)
			{
				access = AssemblyBuilderAccess.ReflectionOnly;
			}
			else
			{
				access = AssemblyBuilderAccess.RunAndSave;
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			AssemblyBuilder assemblyBuilder = domain.InternalDefineDynamicAssembly(asmName, access, text, null, null, null, null, ref stackCrawlMark, null);
			TypeLibConverter.SetGuidAttributeOnAssembly(assemblyBuilder, typeLib);
			TypeLibConverter.SetImportedFromTypeLibAttrOnAssembly(assemblyBuilder, typeLib);
			TypeLibConverter.SetVersionInformation(assemblyBuilder, typeLib, asmName);
			if (bPrimaryInteropAssembly)
			{
				TypeLibConverter.SetPIAAttributeOnAssembly(assemblyBuilder, typeLib);
			}
			return assemblyBuilder;
		}

		// Token: 0x06003327 RID: 13095 RVA: 0x000AD740 File Offset: 0x000AC740
		internal static AssemblyName GetAssemblyNameFromTypelib(object typeLib, string asmFileName, byte[] publicKey, StrongNameKeyPair keyPair, Version asmVersion, AssemblyNameFlags asmNameFlags)
		{
			string text = null;
			string text2 = null;
			int num = 0;
			string text3 = null;
			ITypeLib typeLib2 = (ITypeLib)typeLib;
			typeLib2.GetDocumentation(-1, out text, out text2, out num, out text3);
			if (asmFileName == null)
			{
				asmFileName = text;
			}
			else
			{
				string fileName = Path.GetFileName(asmFileName);
				string extension = Path.GetExtension(asmFileName);
				if (!".dll".Equals(extension, StringComparison.OrdinalIgnoreCase))
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_InvalidFileExtension"));
				}
				asmFileName = fileName.Substring(0, fileName.Length - ".dll".Length);
			}
			if (asmVersion == null)
			{
				int major;
				int minor;
				Marshal.GetTypeLibVersion(typeLib2, out major, out minor);
				asmVersion = new Version(major, minor, 0, 0);
			}
			AssemblyName assemblyName = new AssemblyName();
			assemblyName.Init(asmFileName, publicKey, null, asmVersion, null, AssemblyHashAlgorithm.None, AssemblyVersionCompatibility.SameMachine, null, asmNameFlags, keyPair);
			return assemblyName;
		}

		// Token: 0x06003328 RID: 13096 RVA: 0x000AD804 File Offset: 0x000AC804
		private static void UpdateComTypesInAssembly(AssemblyBuilder asmBldr, ModuleBuilder modBldr)
		{
			AssemblyBuilderData assemblyData = asmBldr.m_assemblyData;
			Type[] types = modBldr.GetTypes();
			int num = types.Length;
			for (int i = 0; i < num; i++)
			{
				assemblyData.AddPublicComType(types[i]);
			}
		}

		// Token: 0x06003329 RID: 13097 RVA: 0x000AD838 File Offset: 0x000AC838
		private static void SetGuidAttributeOnAssembly(AssemblyBuilder asmBldr, object typeLib)
		{
			Type[] types = new Type[]
			{
				typeof(string)
			};
			ConstructorInfo constructor = typeof(GuidAttribute).GetConstructor(types);
			object[] constructorArgs = new object[]
			{
				Marshal.GetTypeLibGuid((ITypeLib)typeLib).ToString()
			};
			CustomAttributeBuilder customAttribute = new CustomAttributeBuilder(constructor, constructorArgs);
			asmBldr.SetCustomAttribute(customAttribute);
		}

		// Token: 0x0600332A RID: 13098 RVA: 0x000AD8A8 File Offset: 0x000AC8A8
		private static void SetImportedFromTypeLibAttrOnAssembly(AssemblyBuilder asmBldr, object typeLib)
		{
			Type[] types = new Type[]
			{
				typeof(string)
			};
			ConstructorInfo constructor = typeof(ImportedFromTypeLibAttribute).GetConstructor(types);
			string typeLibName = Marshal.GetTypeLibName((ITypeLib)typeLib);
			object[] constructorArgs = new object[]
			{
				typeLibName
			};
			CustomAttributeBuilder customAttribute = new CustomAttributeBuilder(constructor, constructorArgs);
			asmBldr.SetCustomAttribute(customAttribute);
		}

		// Token: 0x0600332B RID: 13099 RVA: 0x000AD90C File Offset: 0x000AC90C
		private static void SetTypeLibVersionAttribute(AssemblyBuilder asmBldr, object typeLib)
		{
			Type[] types = new Type[]
			{
				typeof(int),
				typeof(int)
			};
			ConstructorInfo constructor = typeof(TypeLibVersionAttribute).GetConstructor(types);
			int num;
			int num2;
			Marshal.GetTypeLibVersion((ITypeLib)typeLib, out num, out num2);
			object[] constructorArgs = new object[]
			{
				num,
				num2
			};
			CustomAttributeBuilder customAttribute = new CustomAttributeBuilder(constructor, constructorArgs);
			asmBldr.SetCustomAttribute(customAttribute);
		}

		// Token: 0x0600332C RID: 13100 RVA: 0x000AD994 File Offset: 0x000AC994
		private static void SetVersionInformation(AssemblyBuilder asmBldr, object typeLib, AssemblyName asmName)
		{
			string text = null;
			string text2 = null;
			int num = 0;
			string text3 = null;
			ITypeLib typeLib2 = (ITypeLib)typeLib;
			typeLib2.GetDocumentation(-1, out text, out text2, out num, out text3);
			string product = string.Format(CultureInfo.InvariantCulture, Environment.GetResourceString("TypeLibConverter_ImportedTypeLibProductName"), new object[]
			{
				text
			});
			asmBldr.DefineVersionInfoResource(product, asmName.Version.ToString(), null, null, null);
			TypeLibConverter.SetTypeLibVersionAttribute(asmBldr, typeLib);
		}

		// Token: 0x0600332D RID: 13101 RVA: 0x000ADA04 File Offset: 0x000ACA04
		private static void SetPIAAttributeOnAssembly(AssemblyBuilder asmBldr, object typeLib)
		{
			IntPtr @null = Win32Native.NULL;
			ITypeLib typeLib2 = (ITypeLib)typeLib;
			int num = 0;
			int num2 = 0;
			Type[] types = new Type[]
			{
				typeof(int),
				typeof(int)
			};
			ConstructorInfo constructor = typeof(PrimaryInteropAssemblyAttribute).GetConstructor(types);
			try
			{
				typeLib2.GetLibAttr(out @null);
				TYPELIBATTR typelibattr = (TYPELIBATTR)Marshal.PtrToStructure(@null, typeof(TYPELIBATTR));
				num = (int)typelibattr.wMajorVerNum;
				num2 = (int)typelibattr.wMinorVerNum;
			}
			finally
			{
				if (@null != Win32Native.NULL)
				{
					typeLib2.ReleaseTLibAttr(@null);
				}
			}
			object[] constructorArgs = new object[]
			{
				num,
				num2
			};
			CustomAttributeBuilder customAttribute = new CustomAttributeBuilder(constructor, constructorArgs);
			asmBldr.SetCustomAttribute(customAttribute);
		}

		// Token: 0x0600332E RID: 13102
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void nConvertTypeLibToMetadata(object typeLib, Assembly asmBldr, Module modBldr, string nameSpace, TypeLibImporterFlags flags, ITypeLibImporterNotifySink notifySink, out ArrayList eventItfInfoList);

		// Token: 0x0600332F RID: 13103
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object nConvertAssemblyToTypeLib(Assembly assembly, string strTypeLibName, TypeLibExporterFlags flags, ITypeLibExporterNotifySink notifySink);

		// Token: 0x04001A40 RID: 6720
		private const string s_strTypeLibAssemblyTitlePrefix = "TypeLib ";

		// Token: 0x04001A41 RID: 6721
		private const string s_strTypeLibAssemblyDescPrefix = "Assembly generated from typelib ";

		// Token: 0x04001A42 RID: 6722
		private const int MAX_NAMESPACE_LENGTH = 1024;

		// Token: 0x02000535 RID: 1333
		private class TypeResolveHandler : ITypeLibImporterNotifySink
		{
			// Token: 0x06003331 RID: 13105 RVA: 0x000ADAF0 File Offset: 0x000ACAF0
			public TypeResolveHandler(Module mod, ITypeLibImporterNotifySink userSink)
			{
				this.m_Module = mod;
				this.m_UserSink = userSink;
			}

			// Token: 0x06003332 RID: 13106 RVA: 0x000ADB11 File Offset: 0x000ACB11
			public void ReportEvent(ImporterEventKind eventKind, int eventCode, string eventMsg)
			{
				this.m_UserSink.ReportEvent(eventKind, eventCode, eventMsg);
			}

			// Token: 0x06003333 RID: 13107 RVA: 0x000ADB24 File Offset: 0x000ACB24
			public Assembly ResolveRef(object typeLib)
			{
				Assembly assembly = this.m_UserSink.ResolveRef(typeLib);
				this.m_AsmList.Add(assembly);
				return assembly;
			}

			// Token: 0x06003334 RID: 13108 RVA: 0x000ADB4C File Offset: 0x000ACB4C
			public Assembly ResolveEvent(object sender, ResolveEventArgs args)
			{
				try
				{
					this.m_Module.InternalLoadInMemoryTypeByName(args.Name);
					return this.m_Module.Assembly;
				}
				catch (TypeLoadException ex)
				{
					if (ex.ResourceId != -2146233054)
					{
						throw;
					}
				}
				foreach (object obj in this.m_AsmList)
				{
					Assembly assembly = (Assembly)obj;
					try
					{
						assembly.GetType(args.Name, true, false);
						return assembly;
					}
					catch (TypeLoadException ex2)
					{
						if (ex2._HResult != -2146233054)
						{
							throw;
						}
					}
				}
				return null;
			}

			// Token: 0x06003335 RID: 13109 RVA: 0x000ADC1C File Offset: 0x000ACC1C
			public Assembly ResolveAsmEvent(object sender, ResolveEventArgs args)
			{
				foreach (object obj in this.m_AsmList)
				{
					Assembly assembly = (Assembly)obj;
					if (string.Compare(assembly.FullName, args.Name, StringComparison.OrdinalIgnoreCase) == 0)
					{
						return assembly;
					}
				}
				return null;
			}

			// Token: 0x06003336 RID: 13110 RVA: 0x000ADC90 File Offset: 0x000ACC90
			public Assembly ResolveROAsmEvent(object sender, ResolveEventArgs args)
			{
				foreach (object obj in this.m_AsmList)
				{
					Assembly assembly = (Assembly)obj;
					if (string.Compare(assembly.FullName, args.Name, StringComparison.OrdinalIgnoreCase) == 0)
					{
						return assembly;
					}
				}
				string assemblyString = AppDomain.CurrentDomain.ApplyPolicy(args.Name);
				return Assembly.ReflectionOnlyLoad(assemblyString);
			}

			// Token: 0x04001A43 RID: 6723
			private Module m_Module;

			// Token: 0x04001A44 RID: 6724
			private ITypeLibImporterNotifySink m_UserSink;

			// Token: 0x04001A45 RID: 6725
			private ArrayList m_AsmList = new ArrayList();
		}
	}
}
