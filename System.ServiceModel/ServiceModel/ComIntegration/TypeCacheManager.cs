using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.ServiceModel.Description;
using System.Threading;
using Microsoft.Win32;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000273 RID: 627
	internal class TypeCacheManager : ITypeCacheManager
	{
		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x060011D7 RID: 4567 RVA: 0x00040A6C File Offset: 0x0003EC6C
		public static ITypeCacheManager Provider
		{
			get
			{
				object obj = TypeCacheManager.instanceLock;
				lock (obj)
				{
					if (TypeCacheManager.instance == null)
					{
						ITypeCacheManager typeCacheManager = new TypeCacheManager();
						Thread.MemoryBarrier();
						TypeCacheManager.instance = typeCacheManager;
					}
				}
				return TypeCacheManager.instance;
			}
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x00040AC4 File Offset: 0x0003ECC4
		internal TypeCacheManager()
		{
			this.assemblyTable = new Dictionary<Guid, Assembly>();
			this.typeTable = new Dictionary<Guid, Type>();
			this.typeTableLock = new object();
			this.assemblyTableLock = new object();
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x00040AF8 File Offset: 0x0003ECF8
		private Guid GettypeLibraryIDFromIID(Guid iid, bool isServer, out string version)
		{
			RegistryKey registryKey = null;
			Guid result;
			try
			{
				if (isServer)
				{
					string name = "software\\classes\\interface\\{" + iid.ToString() + "}\\typelib";
					registryKey = Registry.LocalMachine.OpenSubKey(name, false);
				}
				else
				{
					string name = "interface\\{" + iid.ToString() + "}\\typelib";
					registryKey = Registry.ClassesRoot.OpenSubKey(name, false);
				}
				if (registryKey == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("InterfaceNotRegistered")));
				}
				string text = registryKey.GetValue("").ToString();
				if (string.IsNullOrEmpty(text))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("NoTypeLibraryFoundForInterface")));
				}
				version = registryKey.GetValue("Version").ToString();
				if (string.IsNullOrEmpty(version))
				{
					version = "1.0";
				}
				Guid guid;
				if (!DiagnosticUtility.Utility.TryCreateGuid(text, out guid))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("BadInterfaceRegistration")));
				}
				result = guid;
			}
			finally
			{
				if (registryKey != null)
				{
					registryKey.Close();
				}
			}
			return result;
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x00040C20 File Offset: 0x0003EE20
		private void ParseVersion(string version, bool parseVersionAsHex, out ushort major, out ushort minor)
		{
			NumberStyles style = parseVersionAsHex ? NumberStyles.HexNumber : NumberStyles.None;
			major = 0;
			minor = 0;
			if (string.IsNullOrEmpty(version))
			{
				return;
			}
			int num = version.IndexOf(".", StringComparison.Ordinal);
			try
			{
				if (num == -1)
				{
					major = ushort.Parse(version, style, NumberFormatInfo.InvariantInfo);
					minor = 0;
				}
				else
				{
					major = ushort.Parse(version.Substring(0, num), style, NumberFormatInfo.InvariantInfo);
					string text = version.Substring(num + 1);
					int num2 = text.IndexOf(".", StringComparison.Ordinal);
					if (num2 != -1)
					{
						text = text.Substring(0, num2);
					}
					minor = ushort.Parse(text, style, NumberFormatInfo.InvariantInfo);
				}
			}
			catch (FormatException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("BadInterfaceVersion")));
			}
			catch (OverflowException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("BadInterfaceVersion")));
			}
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x00040D0C File Offset: 0x0003EF0C
		private ITypeLib2 GettypeLibrary(Guid typeLibraryID, string version, bool parseVersionAsHex)
		{
			ushort major = 0;
			ushort minor = 0;
			this.ParseVersion(version, parseVersionAsHex, out major, out minor);
			object obj;
			int num = SafeNativeMethods.LoadRegTypeLib(ref typeLibraryID, major, minor, 0, out obj);
			if (num != 0 || obj == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new COMException(SR.GetString("FailedToLoadTypeLibrary"), num));
			}
			return obj as ITypeLib2;
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x00040D60 File Offset: 0x0003EF60
		private Assembly ResolveAssemblyFromIID(Guid iid, bool noAssemblyGeneration, bool isServer)
		{
			string version;
			Guid typeLibraryID = this.GettypeLibraryIDFromIID(iid, isServer, out version);
			return this.ResolveAssemblyFromTypeLibID(iid, typeLibraryID, version, true, noAssemblyGeneration);
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x00040D84 File Offset: 0x0003EF84
		private Assembly ResolveAssemblyFromTypeLibID(Guid iid, Guid typeLibraryID, string version, bool parseVersionAsHex, bool noAssemblyGeneration)
		{
			ComPlusTLBImportTrace.Trace(TraceEventType.Verbose, 327693, "TraceCodeComIntegrationTLBImportStarting", iid, typeLibraryID);
			bool flag = false;
			ITypeLib2 typeLib = null;
			Assembly assembly;
			try
			{
				object obj = this.assemblyTableLock;
				lock (obj)
				{
					this.assemblyTable.TryGetValue(typeLibraryID, out assembly);
					if (assembly == null)
					{
						typeLib = this.GettypeLibrary(typeLibraryID, version, parseVersionAsHex);
						object obj2 = null;
						typeLib.GetCustData(ref TypeCacheManager.clrAssemblyCustomID, out obj2);
						if (obj2 == null)
						{
							flag = true;
						}
						string text = obj2 as string;
						if (string.IsNullOrEmpty(text))
						{
							flag = true;
						}
						if (noAssemblyGeneration && flag)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("NativeTypeLibraryNotAllowed", new object[]
							{
								typeLibraryID
							})));
						}
						if (!flag)
						{
							ComPlusTLBImportTrace.Trace(TraceEventType.Verbose, 327694, "TraceCodeComIntegrationTLBImportFromAssembly", iid, typeLibraryID, text);
							assembly = Assembly.Load(text);
						}
						else
						{
							ComPlusTLBImportTrace.Trace(TraceEventType.Verbose, 327695, "TraceCodeComIntegrationTLBImportFromTypelib", iid, typeLibraryID);
							assembly = TypeLibraryHelper.GenerateAssemblyFromNativeTypeLibrary(iid, typeLibraryID, typeLib);
						}
						this.assemblyTable[typeLibraryID] = assembly;
					}
				}
			}
			catch (Exception ex)
			{
				DiagnosticUtility.EventLog.LogEvent(TraceEventType.Error, 10, 3221356568U, new string[]
				{
					iid.ToString(),
					typeLibraryID.ToString(),
					ex.ToString()
				});
				throw;
			}
			finally
			{
				if (typeLib != null)
				{
					Marshal.ReleaseComObject(typeLib);
				}
			}
			if (null == assembly)
			{
				throw Fx.AssertAndThrow("Assembly should not be null");
			}
			ComPlusTLBImportTrace.Trace(TraceEventType.Verbose, 327697, "TraceCodeComIntegrationTLBImportFinished", iid, typeLibraryID);
			return assembly;
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x00040F58 File Offset: 0x0003F158
		private bool NoCoClassAttributeOnType(ICustomAttributeProvider attrProvider)
		{
			object[] customAttributes = ServiceReflector.GetCustomAttributes(attrProvider, typeof(CoClassAttribute), false);
			return customAttributes.Length == 0;
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x00040F80 File Offset: 0x0003F180
		Assembly ITypeCacheManager.ResolveAssembly(Guid assembly)
		{
			Assembly result = null;
			object obj = this.assemblyTableLock;
			lock (obj)
			{
				this.assemblyTable.TryGetValue(assembly, out result);
			}
			return result;
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x00040FCC File Offset: 0x0003F1CC
		void ITypeCacheManager.FindOrCreateType(Guid typeLibId, string typeLibVersion, Guid typeDefId, out Type userDefinedType, bool noAssemblyGeneration)
		{
			object obj = this.typeTableLock;
			lock (obj)
			{
				this.typeTable.TryGetValue(typeDefId, out userDefinedType);
				if (userDefinedType == null)
				{
					Assembly assembly = this.ResolveAssemblyFromTypeLibID(Guid.Empty, typeLibId, typeLibVersion, false, noAssemblyGeneration);
					foreach (Type type in assembly.GetTypes())
					{
						if (type.GUID == typeDefId && type.IsValueType)
						{
							userDefinedType = type;
							break;
						}
					}
					if (userDefinedType == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UdtNotFoundInAssembly", new object[]
						{
							typeDefId
						})));
					}
					this.typeTable[typeDefId] = userDefinedType;
				}
			}
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x000410B8 File Offset: 0x0003F2B8
		public void FindOrCreateType(Guid iid, out Type interfaceType, bool noAssemblyGeneration, bool isServer)
		{
			object obj = this.typeTableLock;
			lock (obj)
			{
				this.typeTable.TryGetValue(iid, out interfaceType);
				if (interfaceType == null)
				{
					Type type = null;
					Assembly assembly = this.ResolveAssemblyFromIID(iid, noAssemblyGeneration, isServer);
					foreach (Type type2 in assembly.GetTypes())
					{
						if (type2.GUID == iid)
						{
							if (type2.IsInterface && this.NoCoClassAttributeOnType(type2))
							{
								interfaceType = type2;
								break;
							}
							if (type2.IsInterface && !this.NoCoClassAttributeOnType(type2))
							{
								type = type2;
							}
						}
					}
					if (interfaceType == null && type != null)
					{
						interfaceType = type;
					}
					else if (interfaceType == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("InterfaceNotFoundInAssembly")));
					}
					this.typeTable[iid] = interfaceType;
				}
			}
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x000411C4 File Offset: 0x0003F3C4
		void ITypeCacheManager.FindOrCreateType(Type serverType, Guid iid, out Type interfaceType, bool noAssemblyGeneration, bool isServer)
		{
			interfaceType = null;
			if (serverType == null)
			{
				this.FindOrCreateType(iid, out interfaceType, noAssemblyGeneration, isServer);
				return;
			}
			if (!serverType.IsClass)
			{
				throw Fx.AssertAndThrow("This should be a class");
			}
			foreach (Type type in serverType.GetInterfaces())
			{
				if (type.GUID == iid)
				{
					interfaceType = type;
					break;
				}
			}
			if (interfaceType == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("InterfaceNotFoundInAssembly")));
			}
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x00041250 File Offset: 0x0003F450
		public static Type ResolveClsidToType(Guid clsid)
		{
			string text = "software\\classes\\clsid\\{" + clsid.ToString() + "}\\InprocServer32";
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(text, false))
			{
				if (registryKey != null)
				{
					using (RegistryKey registryKey2 = registryKey.OpenSubKey(typeof(TypeCacheManager).Assembly.ImageRuntimeVersion))
					{
						string text2 = null;
						if (registryKey2 == null)
						{
							foreach (string text3 in registryKey.GetSubKeyNames())
							{
								text = text3;
								if (!string.IsNullOrEmpty(text))
								{
									using (RegistryKey registryKey3 = registryKey.OpenSubKey(text))
									{
										text2 = (string)registryKey3.GetValue("Assembly");
										if (!string.IsNullOrEmpty(text2))
										{
											break;
										}
									}
								}
							}
						}
						else
						{
							text2 = (string)registryKey2.GetValue("Assembly");
						}
						if (string.IsNullOrEmpty(text2))
						{
							return null;
						}
						Assembly assembly = Assembly.Load(text2);
						foreach (Type type in assembly.GetTypes())
						{
							if (type.IsClass && type.GUID == clsid)
							{
								return type;
							}
						}
						return null;
					}
				}
			}
			using (RegistryHandle bitnessHKCR = RegistryHandle.GetBitnessHKCR(IntPtr.Size != 8))
			{
				if (bitnessHKCR != null)
				{
					using (RegistryHandle registryHandle = bitnessHKCR.OpenSubKey("CLSID\\{" + clsid.ToString() + "}\\InprocServer32"))
					{
						using (RegistryHandle registryHandle2 = registryHandle.OpenSubKey(typeof(TypeCacheManager).Assembly.ImageRuntimeVersion))
						{
							string text4 = null;
							if (registryHandle2 == null)
							{
								using (StringEnumerator enumerator = registryHandle.GetSubKeyNames().GetEnumerator())
								{
									while (enumerator.MoveNext())
									{
										string text5 = enumerator.Current;
										text = text5;
										if (!string.IsNullOrEmpty(text))
										{
											using (RegistryHandle registryHandle3 = registryHandle.OpenSubKey(text))
											{
												text4 = registryHandle3.GetStringValue("Assembly");
												if (!string.IsNullOrEmpty(text4))
												{
													break;
												}
											}
										}
									}
									goto IL_227;
								}
							}
							text4 = registryHandle2.GetStringValue("Assembly");
							IL_227:
							if (string.IsNullOrEmpty(text4))
							{
								return null;
							}
							Assembly assembly2 = Assembly.Load(text4);
							foreach (Type type2 in assembly2.GetTypes())
							{
								if (type2.IsClass && type2.GUID == clsid)
								{
									return type2;
								}
							}
							return null;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x000415D0 File Offset: 0x0003F7D0
		internal Type VerifyType(Guid iid)
		{
			Type result;
			((ITypeCacheManager)this).FindOrCreateType(iid, out result, false, true);
			return result;
		}

		// Token: 0x040019B0 RID: 6576
		private static Guid clrAssemblyCustomID = new Guid("90883F05-3D28-11D2-8F17-00A0C9A6186D");

		// Token: 0x040019B1 RID: 6577
		private static object instanceLock = new object();

		// Token: 0x040019B2 RID: 6578
		internal static ITypeCacheManager instance;

		// Token: 0x040019B3 RID: 6579
		private Dictionary<Guid, Assembly> assemblyTable;

		// Token: 0x040019B4 RID: 6580
		private Dictionary<Guid, Type> typeTable;

		// Token: 0x040019B5 RID: 6581
		private object typeTableLock;

		// Token: 0x040019B6 RID: 6582
		private object assemblyTableLock;

		// Token: 0x02000B16 RID: 2838
		private enum RegKind
		{
			// Token: 0x04003FC8 RID: 16328
			Default,
			// Token: 0x04003FC9 RID: 16329
			Register,
			// Token: 0x04003FCA RID: 16330
			None
		}
	}
}
