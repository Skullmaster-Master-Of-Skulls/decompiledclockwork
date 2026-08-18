using System;
using System.Collections;
using System.Design;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.Win32;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000296 RID: 662
	public class AxImporter
	{
		// Token: 0x06001929 RID: 6441 RVA: 0x0008C854 File Offset: 0x0008AA54
		public AxImporter(AxImporter.Options options)
		{
			this.options = options;
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x0600192A RID: 6442 RVA: 0x0008C864 File Offset: 0x0008AA64
		public string[] GeneratedAssemblies
		{
			get
			{
				if (this.genAssems == null || this.genAssems.Count <= 0)
				{
					return new string[0];
				}
				string[] array = new string[this.genAssems.Count];
				for (int i = 0; i < this.genAssems.Count; i++)
				{
					array[i] = (string)this.genAssems[i];
				}
				return array;
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x0600192B RID: 6443 RVA: 0x0008C8CC File Offset: 0x0008AACC
		public System.Runtime.InteropServices.TYPELIBATTR[] GeneratedTypeLibAttributes
		{
			get
			{
				if (this.tlbAttrs == null)
				{
					return new System.Runtime.InteropServices.TYPELIBATTR[0];
				}
				System.Runtime.InteropServices.TYPELIBATTR[] array = new System.Runtime.InteropServices.TYPELIBATTR[this.tlbAttrs.Count];
				for (int i = 0; i < this.tlbAttrs.Count; i++)
				{
					array[i] = (System.Runtime.InteropServices.TYPELIBATTR)this.tlbAttrs[i];
				}
				return array;
			}
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x0600192C RID: 6444 RVA: 0x0008C928 File Offset: 0x0008AB28
		public string[] GeneratedSources
		{
			get
			{
				if (this.options.genSources)
				{
					string[] array = new string[this.generatedSources.Count];
					for (int i = 0; i < this.generatedSources.Count; i++)
					{
						array[i] = (string)this.generatedSources[i];
					}
					return array;
				}
				return null;
			}
		}

		// Token: 0x0600192D RID: 6445 RVA: 0x0008C980 File Offset: 0x0008AB80
		private void AddDependentAssemblies(Assembly assem, string assemPath)
		{
			AssemblyName[] referencedAssemblies = assem.GetReferencedAssemblies();
			foreach (AssemblyName assemblyName in referencedAssemblies)
			{
				if (!string.Equals(assemblyName.Name, "mscorlib", StringComparison.OrdinalIgnoreCase))
				{
					string text = this.GetComReference(assemblyName);
					if (text == null)
					{
						Assembly assembly = null;
						try
						{
							assembly = Assembly.ReflectionOnlyLoad(assemblyName.FullName);
						}
						catch (FileNotFoundException)
						{
							if (assemblyName.CodeBase != null)
							{
								throw;
							}
							string assemblyFile = Path.Combine(Path.GetDirectoryName(assemPath), assemblyName.Name + ".dll");
							assembly = Assembly.ReflectionOnlyLoadFrom(assemblyFile);
						}
						text = assembly.EscapedCodeBase;
						if (text != null)
						{
							text = this.GetLocalPath(text);
						}
					}
					this.AddReferencedAssembly(text);
				}
			}
		}

		// Token: 0x0600192E RID: 6446 RVA: 0x0008CA44 File Offset: 0x0008AC44
		private void AddReferencedAssembly(string assem)
		{
			if (this.refAssems == null)
			{
				this.refAssems = new ArrayList();
			}
			this.refAssems.Add(assem);
		}

		// Token: 0x0600192F RID: 6447 RVA: 0x0008CA66 File Offset: 0x0008AC66
		private void AddGeneratedAssembly(string assem)
		{
			if (this.genAssems == null)
			{
				this.genAssems = new ArrayList();
			}
			this.genAssems.Add(assem);
		}

		// Token: 0x06001930 RID: 6448 RVA: 0x0008CA88 File Offset: 0x0008AC88
		internal void AddRCW(ITypeLib typeLib, Assembly assem)
		{
			if (this.rcwCache == null)
			{
				this.rcwCache = new Hashtable();
			}
			IntPtr invalidIntPtr = NativeMethods.InvalidIntPtr;
			typeLib.GetLibAttr(out invalidIntPtr);
			try
			{
				if (invalidIntPtr != NativeMethods.InvalidIntPtr)
				{
					System.Runtime.InteropServices.TYPELIBATTR typelibattr = (System.Runtime.InteropServices.TYPELIBATTR)Marshal.PtrToStructure(invalidIntPtr, typeof(System.Runtime.InteropServices.TYPELIBATTR));
					this.rcwCache.Add(typelibattr.guid, assem);
				}
			}
			finally
			{
				typeLib.ReleaseTLibAttr(invalidIntPtr);
			}
		}

		// Token: 0x06001931 RID: 6449 RVA: 0x0008CB0C File Offset: 0x0008AD0C
		internal Assembly FindRCW(ITypeLib typeLib)
		{
			if (this.rcwCache == null)
			{
				return null;
			}
			IntPtr invalidIntPtr = NativeMethods.InvalidIntPtr;
			typeLib.GetLibAttr(out invalidIntPtr);
			try
			{
				if (invalidIntPtr != NativeMethods.InvalidIntPtr)
				{
					System.Runtime.InteropServices.TYPELIBATTR typelibattr = (System.Runtime.InteropServices.TYPELIBATTR)Marshal.PtrToStructure(invalidIntPtr, typeof(System.Runtime.InteropServices.TYPELIBATTR));
					return (Assembly)this.rcwCache[typelibattr.guid];
				}
			}
			finally
			{
				typeLib.ReleaseTLibAttr(invalidIntPtr);
			}
			return null;
		}

		// Token: 0x06001932 RID: 6450 RVA: 0x0008CB90 File Offset: 0x0008AD90
		private void AddTypeLibAttr(ITypeLib typeLib)
		{
			if (this.tlbAttrs == null)
			{
				this.tlbAttrs = new ArrayList();
			}
			IntPtr invalidIntPtr = NativeMethods.InvalidIntPtr;
			typeLib.GetLibAttr(out invalidIntPtr);
			if (invalidIntPtr != NativeMethods.InvalidIntPtr)
			{
				System.Runtime.InteropServices.TYPELIBATTR typelibattr = (System.Runtime.InteropServices.TYPELIBATTR)Marshal.PtrToStructure(invalidIntPtr, typeof(System.Runtime.InteropServices.TYPELIBATTR));
				this.tlbAttrs.Add(typelibattr);
				typeLib.ReleaseTLibAttr(invalidIntPtr);
			}
		}

		// Token: 0x06001933 RID: 6451 RVA: 0x0008CBFA File Offset: 0x0008ADFA
		private string GetAxReference(ITypeLib typeLib)
		{
			if (this.options.references == null)
			{
				return null;
			}
			return this.options.references.ResolveActiveXReference((UCOMITypeLib)typeLib);
		}

		// Token: 0x06001934 RID: 6452 RVA: 0x0008CC24 File Offset: 0x0008AE24
		private string GetReferencedAssembly(string assemName)
		{
			if (this.refAssems == null || this.refAssems.Count <= 0)
			{
				return null;
			}
			foreach (object obj in this.refAssems)
			{
				string text = (string)obj;
				if (string.Equals(text, assemName, StringComparison.OrdinalIgnoreCase))
				{
					return text;
				}
			}
			return null;
		}

		// Token: 0x06001935 RID: 6453 RVA: 0x0008CCA0 File Offset: 0x0008AEA0
		private string GetComReference(ITypeLib typeLib)
		{
			if (this.options.references == null)
			{
				return null;
			}
			return this.options.references.ResolveComReference((UCOMITypeLib)typeLib);
		}

		// Token: 0x06001936 RID: 6454 RVA: 0x0008CCC7 File Offset: 0x0008AEC7
		private string GetComReference(AssemblyName name)
		{
			if (this.options.references == null)
			{
				return name.EscapedCodeBase;
			}
			return this.options.references.ResolveComReference(name);
		}

		// Token: 0x06001937 RID: 6455 RVA: 0x0008CCEE File Offset: 0x0008AEEE
		private string GetManagedReference(string assemName)
		{
			if (this.options.references == null)
			{
				return assemName + ".dll";
			}
			return this.options.references.ResolveManagedReference(assemName);
		}

		// Token: 0x06001938 RID: 6456 RVA: 0x0008CD1C File Offset: 0x0008AF1C
		private string GetAxTypeFromAssembly(string fileName, Guid clsid)
		{
			Assembly copiedAssembly = this.GetCopiedAssembly(fileName, true, false);
			Type[] types = copiedAssembly.GetTypes();
			foreach (Type type in types)
			{
				if (typeof(AxHost).IsAssignableFrom(type))
				{
					CustomAttributeData[] attributeData = AxWrapperGen.GetAttributeData(type, typeof(AxHost.ClsidAttribute));
					if (attributeData[0].ConstructorArguments[0].Value.ToString() == "{" + clsid.ToString() + "}")
					{
						return type.FullName;
					}
				}
			}
			return null;
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x0008CDC0 File Offset: 0x0008AFC0
		private Assembly GetCopiedAssembly(string fileName, bool loadPdb, bool isPIA)
		{
			if (!File.Exists(fileName))
			{
				return null;
			}
			string key = fileName.ToUpper(CultureInfo.InvariantCulture);
			if (this.copiedAssems == null)
			{
				this.copiedAssems = new Hashtable();
			}
			else if (this.copiedAssems.Contains(key))
			{
				return (Assembly)this.copiedAssems[key];
			}
			Assembly assembly;
			if (!isPIA)
			{
				Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
				int num = (int)stream.Length;
				byte[] array = new byte[num];
				stream.Read(array, 0, num);
				stream.Close();
				if (loadPdb)
				{
					string path = Path.ChangeExtension(fileName, "pdb");
					if (File.Exists(path))
					{
						stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
						num = (int)stream.Length;
						byte[] buffer = new byte[num];
						stream.Read(buffer, 0, num);
						stream.Close();
					}
				}
				assembly = this.GetLoadedAssembly(fileName, true);
				if (assembly == null)
				{
					assembly = Assembly.ReflectionOnlyLoad(array);
				}
			}
			else
			{
				assembly = Assembly.ReflectionOnlyLoadFrom(fileName);
			}
			this.copiedAssems.Add(key, assembly);
			return assembly;
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x0008CEC4 File Offset: 0x0008B0C4
		private Assembly GetLoadedAssembly(string filePath, bool reflectionOnly)
		{
			Assembly[] array = reflectionOnly ? AppDomain.CurrentDomain.ReflectionOnlyGetAssemblies() : AppDomain.CurrentDomain.GetAssemblies();
			foreach (Assembly assembly in array)
			{
				if (assembly.Location.Equals(filePath, StringComparison.InvariantCultureIgnoreCase))
				{
					return assembly;
				}
			}
			return null;
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x0008CF14 File Offset: 0x0008B114
		private static string GetFileOfTypeLib(ITypeLib typeLib)
		{
			IntPtr invalidIntPtr = NativeMethods.InvalidIntPtr;
			typeLib.GetLibAttr(out invalidIntPtr);
			if (invalidIntPtr != NativeMethods.InvalidIntPtr)
			{
				System.Runtime.InteropServices.TYPELIBATTR typelibattr = (System.Runtime.InteropServices.TYPELIBATTR)Marshal.PtrToStructure(invalidIntPtr, typeof(System.Runtime.InteropServices.TYPELIBATTR));
				try
				{
					return AxImporter.GetFileOfTypeLib(ref typelibattr);
				}
				finally
				{
					typeLib.ReleaseTLibAttr(invalidIntPtr);
				}
			}
			return null;
		}

		// Token: 0x0600193C RID: 6460 RVA: 0x0008CF78 File Offset: 0x0008B178
		public static string GetFileOfTypeLib(ref System.Runtime.InteropServices.TYPELIBATTR tlibattr)
		{
			string text = NativeMethods.QueryPathOfRegTypeLib(ref tlibattr.guid, tlibattr.wMajorVerNum, tlibattr.wMinorVerNum, tlibattr.lcid);
			if (text.Length > 0)
			{
				int num = text.IndexOf('\0');
				if (num > -1)
				{
					text = text.Substring(0, num);
				}
				if (!File.Exists(text))
				{
					int num2 = text.LastIndexOf(Path.DirectorySeparatorChar);
					if (num2 != -1)
					{
						bool flag = true;
						for (int i = num2 + 1; i < text.Length; i++)
						{
							if (text[i] != '\0' && !char.IsDigit(text[i]))
							{
								flag = false;
								break;
							}
						}
						if (flag)
						{
							text = text.Substring(0, num2);
							if (!File.Exists(text))
							{
								text = null;
							}
						}
						else
						{
							text = null;
						}
					}
					else
					{
						text = null;
					}
				}
			}
			return text;
		}

		// Token: 0x0600193D RID: 6461 RVA: 0x0008D034 File Offset: 0x0008B234
		private string GetLocalPath(string fileName)
		{
			Uri uri = new Uri(fileName);
			return uri.LocalPath + uri.Fragment;
		}

		// Token: 0x0600193E RID: 6462 RVA: 0x0008D05C File Offset: 0x0008B25C
		internal string GenerateFromActiveXClsid(Guid clsid)
		{
			string text = "CLSID\\{" + clsid.ToString() + "}";
			RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey(text);
			if (registryKey == null)
			{
				throw new ArgumentException(SR.GetString("AXNotRegistered", new object[]
				{
					text.ToString()
				}));
			}
			ITypeLib typeLib = null;
			Guid empty = Guid.Empty;
			RegistryKey registryKey2 = registryKey.OpenSubKey("TypeLib");
			if (registryKey2 != null)
			{
				RegistryKey registryKey3 = registryKey.OpenSubKey("Version");
				string text2 = (string)registryKey3.GetValue("");
				int num = text2.IndexOf('.');
				short majorVersion;
				short minorVersion;
				if (num == -1)
				{
					majorVersion = short.Parse(text2, CultureInfo.InvariantCulture);
					minorVersion = 0;
				}
				else
				{
					majorVersion = short.Parse(text2.Substring(0, num), CultureInfo.InvariantCulture);
					minorVersion = short.Parse(text2.Substring(num + 1, text2.Length - num - 1), CultureInfo.InvariantCulture);
				}
				registryKey3.Close();
				object value = registryKey2.GetValue("");
				empty = new Guid((string)value);
				registryKey2.Close();
				try
				{
					typeLib = NativeMethods.LoadRegTypeLib(ref empty, majorVersion, minorVersion, Application.CurrentCulture.LCID);
				}
				catch (Exception ex)
				{
				}
			}
			if (typeLib == null)
			{
				RegistryKey registryKey4 = registryKey.OpenSubKey("InprocServer32");
				if (registryKey4 != null)
				{
					string typelib = (string)registryKey4.GetValue("");
					registryKey4.Close();
					typeLib = NativeMethods.LoadTypeLib(typelib);
				}
			}
			registryKey.Close();
			if (typeLib != null)
			{
				try
				{
					return this.GenerateFromTypeLibrary((UCOMITypeLib)typeLib, clsid);
				}
				finally
				{
					Marshal.ReleaseComObject(typeLib);
				}
			}
			throw new ArgumentException(SR.GetString("AXNotRegistered", new object[]
			{
				text.ToString()
			}));
		}

		// Token: 0x0600193F RID: 6463 RVA: 0x0008D230 File Offset: 0x0008B430
		public string GenerateFromFile(FileInfo file)
		{
			this.typeLibName = file.FullName;
			ITypeLib typeLib = null;
			typeLib = NativeMethods.LoadTypeLib(this.typeLibName);
			if (typeLib == null)
			{
				throw new Exception(SR.GetString("AXCannotLoadTypeLib", new object[]
				{
					this.typeLibName
				}));
			}
			string result;
			try
			{
				result = this.GenerateFromTypeLibrary((UCOMITypeLib)typeLib);
			}
			finally
			{
				if (typeLib != null)
				{
					Marshal.ReleaseComObject(typeLib);
				}
			}
			return result;
		}

		// Token: 0x06001940 RID: 6464 RVA: 0x0008D2A8 File Offset: 0x0008B4A8
		public string GenerateFromTypeLibrary(UCOMITypeLib typeLib)
		{
			bool flag = this.options.ignoreRegisteredOcx;
			if (!flag)
			{
				int typeInfoCount = ((ITypeLib)typeLib).GetTypeInfoCount();
				for (int i = 0; i < typeInfoCount; i++)
				{
					ITypeInfo typeInfo;
					((ITypeLib)typeLib).GetTypeInfo(i, out typeInfo);
					IntPtr zero;
					typeInfo.GetTypeAttr(out zero);
					System.Runtime.InteropServices.ComTypes.TYPEATTR typeattr = (System.Runtime.InteropServices.ComTypes.TYPEATTR)Marshal.PtrToStructure(zero, typeof(System.Runtime.InteropServices.ComTypes.TYPEATTR));
					if (typeattr.typekind == System.Runtime.InteropServices.ComTypes.TYPEKIND.TKIND_COCLASS)
					{
						Guid guid = typeattr.guid;
						string name = "CLSID\\{" + guid.ToString() + "}\\Control";
						RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey(name);
						if (registryKey != null)
						{
							flag = true;
						}
					}
					typeInfo.ReleaseTypeAttr(zero);
					zero = IntPtr.Zero;
					Marshal.ReleaseComObject(typeInfo);
					typeInfo = null;
				}
			}
			if (flag)
			{
				return this.GenerateFromTypeLibrary(typeLib, Guid.Empty);
			}
			string text = SR.GetString("AXNoActiveXControls", new object[]
			{
				(this.typeLibName != null) ? this.typeLibName : Marshal.GetTypeLibName((ITypeLib)typeLib)
			});
			if (this.options.msBuildErrors)
			{
				text = "AxImp: error aximp000: " + text;
			}
			throw new Exception(text);
		}

		// Token: 0x06001941 RID: 6465 RVA: 0x0008D3D4 File Offset: 0x0008B5D4
		public string GenerateFromTypeLibrary(UCOMITypeLib typeLib, Guid clsid)
		{
			string text = null;
			string text2 = null;
			Assembly assembly = null;
			text = this.GetAxReference((ITypeLib)typeLib);
			if (text != null && clsid != Guid.Empty)
			{
				text2 = this.GetAxTypeFromAssembly(text, clsid);
			}
			if (text == null)
			{
				string text3 = Marshal.GetTypeLibName((ITypeLib)typeLib);
				string text4 = Path.Combine(this.options.outputDirectory, text3 + ".dll");
				this.AddReferencedAssembly(this.GetManagedReference("System.Windows.Forms"));
				this.AddReferencedAssembly(this.GetManagedReference("System.Drawing"));
				this.AddReferencedAssembly(this.GetManagedReference("System"));
				string text5 = this.GetComReference((ITypeLib)typeLib);
				if (text5 != null)
				{
					this.AddReferencedAssembly(text5);
					assembly = this.GetCopiedAssembly(text5, false, false);
					this.AddDependentAssemblies(assembly, text5);
				}
				else
				{
					TypeLibConverter typeLibConverter = new TypeLibConverter();
					assembly = this.GetPrimaryInteropAssembly((ITypeLib)typeLib, typeLibConverter);
					if (assembly != null)
					{
						text5 = this.GetLocalPath(assembly.EscapedCodeBase);
						this.AddDependentAssemblies(assembly, text5);
					}
					else
					{
						AssemblyBuilder assemblyBuilder = typeLibConverter.ConvertTypeLibToAssembly((ITypeLib)typeLib, text4, TypeLibImporterFlags.None, new AxImporter.ImporterCallback(this), this.options.publicKey, this.options.keyPair, null, null);
						if (text5 == null)
						{
							text5 = this.SaveAssemblyBuilder((ITypeLib)typeLib, assemblyBuilder, text4);
							assembly = assemblyBuilder;
						}
					}
				}
				int num = 0;
				string[] array = new string[this.refAssems.Count];
				foreach (object obj in this.refAssems)
				{
					string text6 = (string)obj;
					string text7 = text6;
					text7 = text7.Replace("%20", " ");
					array[num++] = text7;
				}
				if (text2 == null)
				{
					string fileOfTypeLib;
					if (this.options.ignoreRegisteredOcx)
					{
						fileOfTypeLib = this.typeLibName;
					}
					else
					{
						fileOfTypeLib = AxImporter.GetFileOfTypeLib((ITypeLib)typeLib);
					}
					DateTime tlbTimeStamp = (fileOfTypeLib == null) ? DateTime.Now : File.GetLastWriteTime(fileOfTypeLib);
					ResolveEventHandler value = new ResolveEventHandler(this.OnAssemblyResolve);
					AppDomain.CurrentDomain.AssemblyResolve += value;
					AppDomain.CurrentDomain.TypeResolve += this.OnTypeResolve;
					try
					{
						if (this.options.genSources)
						{
							AxWrapperGen.GeneratedSources = new ArrayList();
						}
						if (this.options.outputName == null)
						{
							this.options.outputName = "Ax" + text3 + ".dll";
						}
						text2 = AxWrapperGen.GenerateWrappers(this, clsid, assembly, array, tlbTimeStamp, this.options.ignoreRegisteredOcx, out text);
						if (this.options.genSources)
						{
							this.generatedSources = AxWrapperGen.GeneratedSources;
						}
					}
					finally
					{
						AppDomain.CurrentDomain.AssemblyResolve -= value;
						AppDomain.CurrentDomain.TypeResolve -= this.OnTypeResolve;
					}
					if (text2 == null)
					{
						string text8 = SR.GetString("AXNoActiveXControls", new object[]
						{
							(this.typeLibName != null) ? this.typeLibName : text3
						});
						if (this.options.msBuildErrors)
						{
							text8 = "AxImp: error aximp000: " + text8;
						}
						throw new Exception(text8);
					}
				}
				if (text2 != null)
				{
					this.AddReferencedAssembly(text);
					this.AddTypeLibAttr((ITypeLib)typeLib);
					this.AddGeneratedAssembly(text);
				}
			}
			return text2;
		}

		// Token: 0x06001942 RID: 6466 RVA: 0x0008D728 File Offset: 0x0008B928
		internal Assembly GetPrimaryInteropAssembly(ITypeLib typeLib, TypeLibConverter tlbConverter)
		{
			Assembly assembly = this.FindRCW(typeLib);
			if (assembly != null)
			{
				return assembly;
			}
			IntPtr invalidIntPtr = NativeMethods.InvalidIntPtr;
			typeLib.GetLibAttr(out invalidIntPtr);
			if (invalidIntPtr != NativeMethods.InvalidIntPtr)
			{
				System.Runtime.InteropServices.TYPELIBATTR typelibattr = (System.Runtime.InteropServices.TYPELIBATTR)Marshal.PtrToStructure(invalidIntPtr, typeof(System.Runtime.InteropServices.TYPELIBATTR));
				string text = null;
				string text2 = null;
				try
				{
					tlbConverter.GetPrimaryInteropAssembly(typelibattr.guid, (int)typelibattr.wMajorVerNum, (int)typelibattr.wMinorVerNum, typelibattr.lcid, out text, out text2);
					if (text != null && text2 == null)
					{
						try
						{
							assembly = Assembly.ReflectionOnlyLoad(text);
							text2 = this.GetLocalPath(assembly.EscapedCodeBase);
							goto IL_A7;
						}
						catch (Exception ex)
						{
							goto IL_A7;
						}
					}
					if (text2 != null)
					{
						text2 = this.GetLocalPath(text2);
						assembly = Assembly.ReflectionOnlyLoadFrom(text2);
					}
					IL_A7:
					if (assembly != null)
					{
						this.AddRCW(typeLib, assembly);
						this.AddReferencedAssembly(text2);
					}
				}
				finally
				{
					typeLib.ReleaseTLibAttr(invalidIntPtr);
				}
			}
			return assembly;
		}

		// Token: 0x06001943 RID: 6467 RVA: 0x0008D81C File Offset: 0x0008BA1C
		private Assembly OnAssemblyResolve(object sender, ResolveEventArgs e)
		{
			string name = e.Name;
			if (this.rcwCache != null)
			{
				foreach (object obj in this.rcwCache.Values)
				{
					Assembly assembly = (Assembly)obj;
					if (assembly.FullName == name)
					{
						return assembly;
					}
				}
			}
			if (this.copiedAssems == null)
			{
				this.copiedAssems = new Hashtable();
			}
			else
			{
				Assembly assembly2 = (Assembly)this.copiedAssems[name];
				if (assembly2 != null)
				{
					return assembly2;
				}
			}
			if (this.refAssems == null || this.refAssems.Count == 0)
			{
				return null;
			}
			foreach (object obj2 in this.refAssems)
			{
				string fileName = (string)obj2;
				Assembly copiedAssembly = this.GetCopiedAssembly(fileName, false, false);
				if (!(copiedAssembly == null))
				{
					string fullName = copiedAssembly.FullName;
					if (fullName == name)
					{
						return copiedAssembly;
					}
				}
			}
			return null;
		}

		// Token: 0x06001944 RID: 6468 RVA: 0x0008D964 File Offset: 0x0008BB64
		private Assembly OnTypeResolve(object sender, ResolveEventArgs e)
		{
			try
			{
				string name = e.Name;
				if (this.refAssems == null || this.refAssems.Count == 0)
				{
					return null;
				}
				foreach (object obj in this.refAssems)
				{
					string fileName = (string)obj;
					Assembly copiedAssembly = this.GetCopiedAssembly(fileName, false, false);
					if (!(copiedAssembly == null) && copiedAssembly.GetType(name, false) != null)
					{
						return copiedAssembly;
					}
				}
			}
			catch
			{
			}
			return null;
		}

		// Token: 0x06001945 RID: 6469 RVA: 0x0008DA18 File Offset: 0x0008BC18
		private string SaveAssemblyBuilder(ITypeLib typeLib, AssemblyBuilder asmBldr, string rcwName)
		{
			FileInfo fileInfo = new FileInfo(rcwName);
			string name = fileInfo.Name;
			if (fileInfo.Exists)
			{
				if (!this.options.overwriteRCW)
				{
					goto IL_C7;
				}
				if (this.typeLibName != null && string.Equals(this.typeLibName, fileInfo.FullName, StringComparison.OrdinalIgnoreCase))
				{
					throw new Exception(SR.GetString("AXCannotOverwriteFile", new object[]
					{
						fileInfo.FullName
					}));
				}
				if ((fileInfo.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
				{
					throw new Exception(SR.GetString("AXReadOnlyFile", new object[]
					{
						fileInfo.FullName
					}));
				}
				try
				{
					fileInfo.Delete();
					asmBldr.Save(name);
					goto IL_C7;
				}
				catch (Exception ex)
				{
					throw new Exception(SR.GetString("AXCannotOverwriteFile", new object[]
					{
						fileInfo.FullName
					}));
				}
			}
			asmBldr.Save(name);
			IL_C7:
			string fullName = fileInfo.FullName;
			this.AddReferencedAssembly(fullName);
			this.AddTypeLibAttr(typeLib);
			this.AddGeneratedAssembly(fullName);
			return fullName;
		}

		// Token: 0x0400156B RID: 5483
		internal AxImporter.Options options;

		// Token: 0x0400156C RID: 5484
		internal string typeLibName;

		// Token: 0x0400156D RID: 5485
		private ArrayList refAssems;

		// Token: 0x0400156E RID: 5486
		private ArrayList genAssems;

		// Token: 0x0400156F RID: 5487
		private ArrayList tlbAttrs;

		// Token: 0x04001570 RID: 5488
		private ArrayList generatedSources;

		// Token: 0x04001571 RID: 5489
		private Hashtable copiedAssems;

		// Token: 0x04001572 RID: 5490
		private Hashtable rcwCache;

		// Token: 0x02000523 RID: 1315
		private class ImporterCallback : ITypeLibImporterNotifySink
		{
			// Token: 0x0600301C RID: 12316 RVA: 0x0010813F File Offset: 0x0010633F
			public ImporterCallback(AxImporter importer)
			{
				this.importer = importer;
				this.options = importer.options;
			}

			// Token: 0x0600301D RID: 12317 RVA: 0x00003937 File Offset: 0x00001B37
			void ITypeLibImporterNotifySink.ReportEvent(ImporterEventKind EventKind, int EventCode, string EventMsg)
			{
			}

			// Token: 0x0600301E RID: 12318 RVA: 0x0010815C File Offset: 0x0010635C
			Assembly ITypeLibImporterNotifySink.ResolveRef(object typeLib)
			{
				Assembly result;
				try
				{
					string comReference = this.importer.GetComReference((ITypeLib)typeLib);
					if (comReference != null)
					{
						this.importer.AddReferencedAssembly(comReference);
					}
					Assembly assembly = this.importer.FindRCW((ITypeLib)typeLib);
					if (assembly != null)
					{
						result = assembly;
					}
					else
					{
						try
						{
							string typeLibName = Marshal.GetTypeLibName((ITypeLib)typeLib);
							string text = Path.Combine(this.options.outputDirectory, typeLibName + ".dll");
							if (this.importer.GetReferencedAssembly(text) != null)
							{
								result = this.importer.GetCopiedAssembly(text, false, false);
							}
							else
							{
								TypeLibConverter typeLibConverter = new TypeLibConverter();
								assembly = this.importer.GetPrimaryInteropAssembly((ITypeLib)typeLib, typeLibConverter);
								if (assembly != null)
								{
									result = assembly;
								}
								else
								{
									AssemblyBuilder assemblyBuilder = typeLibConverter.ConvertTypeLibToAssembly(typeLib, text, TypeLibImporterFlags.None, new AxImporter.ImporterCallback(this.importer), this.options.publicKey, this.options.keyPair, null, null);
									if (comReference == null)
									{
										this.importer.SaveAssemblyBuilder((ITypeLib)typeLib, assemblyBuilder, text);
										this.importer.AddRCW((ITypeLib)typeLib, assemblyBuilder);
										result = assemblyBuilder;
									}
									else
									{
										result = this.importer.GetCopiedAssembly(comReference, false, false);
									}
								}
							}
						}
						catch
						{
							result = null;
						}
					}
				}
				finally
				{
					Marshal.ReleaseComObject(typeLib);
				}
				return result;
			}

			// Token: 0x040020A2 RID: 8354
			private AxImporter importer;

			// Token: 0x040020A3 RID: 8355
			private AxImporter.Options options;
		}

		// Token: 0x02000524 RID: 1316
		public sealed class Options
		{
			// Token: 0x040020A4 RID: 8356
			public string outputName;

			// Token: 0x040020A5 RID: 8357
			public string outputDirectory;

			// Token: 0x040020A6 RID: 8358
			public byte[] publicKey;

			// Token: 0x040020A7 RID: 8359
			public StrongNameKeyPair keyPair;

			// Token: 0x040020A8 RID: 8360
			public string keyFile;

			// Token: 0x040020A9 RID: 8361
			public string keyContainer;

			// Token: 0x040020AA RID: 8362
			public bool genSources;

			// Token: 0x040020AB RID: 8363
			public bool msBuildErrors;

			// Token: 0x040020AC RID: 8364
			public bool noLogo;

			// Token: 0x040020AD RID: 8365
			public bool silentMode;

			// Token: 0x040020AE RID: 8366
			public bool verboseMode;

			// Token: 0x040020AF RID: 8367
			public bool delaySign;

			// Token: 0x040020B0 RID: 8368
			public bool overwriteRCW;

			// Token: 0x040020B1 RID: 8369
			public AxImporter.IReferenceResolver references;

			// Token: 0x040020B2 RID: 8370
			public bool ignoreRegisteredOcx;
		}

		// Token: 0x02000525 RID: 1317
		public interface IReferenceResolver
		{
			// Token: 0x06003020 RID: 12320
			string ResolveManagedReference(string assemName);

			// Token: 0x06003021 RID: 12321
			string ResolveComReference(UCOMITypeLib typeLib);

			// Token: 0x06003022 RID: 12322
			string ResolveComReference(AssemblyName name);

			// Token: 0x06003023 RID: 12323
			string ResolveActiveXReference(UCOMITypeLib typeLib);
		}
	}
}
