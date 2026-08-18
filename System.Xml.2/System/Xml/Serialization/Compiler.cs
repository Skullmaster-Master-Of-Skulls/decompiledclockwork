using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Security.Principal;
using System.Threading;
using Microsoft.CSharp;

namespace System.Xml.Serialization
{
	// Token: 0x0200013F RID: 319
	internal class Compiler
	{
		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x060016EF RID: 5871 RVA: 0x00065C08 File Offset: 0x00063E08
		protected string[] Imports
		{
			get
			{
				string[] array = new string[this.imports.Values.Count];
				this.imports.Values.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x060016F0 RID: 5872 RVA: 0x00065C40 File Offset: 0x00063E40
		internal void AddImport(Type type, Hashtable types)
		{
			if (type == null)
			{
				return;
			}
			if (TypeScope.IsKnownType(type))
			{
				return;
			}
			if (types[type] != null)
			{
				return;
			}
			types[type] = type;
			Type baseType = type.BaseType;
			if (baseType != null)
			{
				this.AddImport(baseType, types);
			}
			Type declaringType = type.DeclaringType;
			if (declaringType != null)
			{
				this.AddImport(declaringType, types);
			}
			foreach (Type type2 in type.GetInterfaces())
			{
				this.AddImport(type2, types);
			}
			ConstructorInfo[] constructors = type.GetConstructors();
			for (int j = 0; j < constructors.Length; j++)
			{
				ParameterInfo[] parameters = constructors[j].GetParameters();
				for (int k = 0; k < parameters.Length; k++)
				{
					this.AddImport(parameters[k].ParameterType, types);
				}
			}
			if (type.IsGenericType)
			{
				Type[] genericArguments = type.GetGenericArguments();
				for (int l = 0; l < genericArguments.Length; l++)
				{
					this.AddImport(genericArguments[l], types);
				}
			}
			TempAssembly.FileIOPermission.Assert();
			Module module = type.Module;
			Assembly assembly = module.Assembly;
			if (DynamicAssemblies.IsTypeDynamic(type))
			{
				DynamicAssemblies.Add(assembly);
				return;
			}
			object[] customAttributes = type.GetCustomAttributes(typeof(TypeForwardedFromAttribute), false);
			if (customAttributes.Length != 0)
			{
				TypeForwardedFromAttribute typeForwardedFromAttribute = customAttributes[0] as TypeForwardedFromAttribute;
				Assembly assembly2 = Assembly.Load(typeForwardedFromAttribute.AssemblyFullName);
				this.imports[assembly2] = assembly2.Location;
			}
			this.imports[assembly] = assembly.Location;
		}

		// Token: 0x060016F1 RID: 5873 RVA: 0x00065DC6 File Offset: 0x00063FC6
		internal void AddImport(Assembly assembly)
		{
			TempAssembly.FileIOPermission.Assert();
			this.imports[assembly] = assembly.Location;
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x060016F2 RID: 5874 RVA: 0x00065DE4 File Offset: 0x00063FE4
		internal TextWriter Source
		{
			get
			{
				return this.writer;
			}
		}

		// Token: 0x060016F3 RID: 5875 RVA: 0x00065DEC File Offset: 0x00063FEC
		internal void Close()
		{
		}

		// Token: 0x060016F4 RID: 5876 RVA: 0x00065DF0 File Offset: 0x00063FF0
		internal static string GetTempAssemblyPath(string baseDir, Assembly assembly, string defaultNamespace)
		{
			if (assembly.IsDynamic)
			{
				throw new InvalidOperationException(Res.GetString("XmlPregenAssemblyDynamic"));
			}
			PermissionSet permissionSet = new PermissionSet(PermissionState.None);
			permissionSet.AddPermission(new FileIOPermission(PermissionState.Unrestricted));
			permissionSet.AddPermission(new EnvironmentPermission(PermissionState.Unrestricted));
			permissionSet.Assert();
			try
			{
				if (baseDir != null && baseDir.Length > 0)
				{
					if (!Directory.Exists(baseDir))
					{
						throw new UnauthorizedAccessException(Res.GetString("XmlPregenMissingDirectory", new object[]
						{
							baseDir
						}));
					}
				}
				else
				{
					baseDir = Path.GetTempPath();
					if (!Directory.Exists(baseDir))
					{
						throw new UnauthorizedAccessException(Res.GetString("XmlPregenMissingTempDirectory"));
					}
				}
				if (baseDir.EndsWith("\\", StringComparison.Ordinal))
				{
					baseDir += Compiler.GetTempAssemblyName(assembly.GetName(), defaultNamespace);
				}
				else
				{
					baseDir = baseDir + "\\" + Compiler.GetTempAssemblyName(assembly.GetName(), defaultNamespace);
				}
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			return baseDir + ".dll";
		}

		// Token: 0x060016F5 RID: 5877 RVA: 0x00065EEC File Offset: 0x000640EC
		internal static string GetTempAssemblyName(AssemblyName parent, string ns)
		{
			return parent.Name + ".XmlSerializers" + ((ns == null || ns.Length == 0) ? "" : ("." + ns.GetHashCode().ToString()));
		}

		// Token: 0x060016F6 RID: 5878 RVA: 0x00065F34 File Offset: 0x00064134
		internal Assembly Compile(Assembly parent, string ns, XmlSerializerCompilerParameters xmlParameters, Evidence evidence)
		{
			CodeDomProvider codeDomProvider = new CSharpCodeProvider();
			CompilerParameters codeDomParameters = xmlParameters.CodeDomParameters;
			codeDomParameters.ReferencedAssemblies.AddRange(this.Imports);
			if (this.debugEnabled)
			{
				codeDomParameters.GenerateInMemory = false;
				codeDomParameters.IncludeDebugInformation = true;
				codeDomParameters.TempFiles.KeepFiles = true;
			}
			PermissionSet permissionSet = new PermissionSet(PermissionState.None);
			if (xmlParameters.IsNeedTempDirAccess)
			{
				permissionSet.AddPermission(TempAssembly.FileIOPermission);
			}
			permissionSet.AddPermission(new EnvironmentPermission(PermissionState.Unrestricted));
			permissionSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.UnmanagedCode));
			permissionSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.ControlEvidence));
			permissionSet.Assert();
			if (parent != null && (codeDomParameters.OutputAssembly == null || codeDomParameters.OutputAssembly.Length == 0))
			{
				string text = Compiler.AssemblyNameFromOptions(codeDomParameters.CompilerOptions);
				if (text == null)
				{
					text = Compiler.GetTempAssemblyPath(codeDomParameters.TempFiles.TempDir, parent, ns);
				}
				codeDomParameters.OutputAssembly = text;
			}
			if (codeDomParameters.CompilerOptions == null || codeDomParameters.CompilerOptions.Length == 0)
			{
				codeDomParameters.CompilerOptions = "/nostdlib";
			}
			else
			{
				CompilerParameters compilerParameters = codeDomParameters;
				compilerParameters.CompilerOptions += " /nostdlib";
			}
			CompilerParameters compilerParameters2 = codeDomParameters;
			compilerParameters2.CompilerOptions += " /D:_DYNAMIC_XMLSERIALIZER_COMPILATION";
			codeDomParameters.Evidence = evidence;
			CompilerResults compilerResults = null;
			Assembly assembly = null;
			try
			{
				compilerResults = codeDomProvider.CompileAssemblyFromSource(codeDomParameters, new string[]
				{
					this.writer.ToString()
				});
				if (compilerResults.Errors.Count > 0)
				{
					StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
					stringWriter.WriteLine(Res.GetString("XmlCompilerError", new object[]
					{
						compilerResults.NativeCompilerReturnValue.ToString(CultureInfo.InvariantCulture)
					}));
					bool flag = false;
					foreach (object obj in compilerResults.Errors)
					{
						CompilerError compilerError = (CompilerError)obj;
						compilerError.FileName = "";
						if (!compilerError.IsWarning || compilerError.ErrorNumber == "CS1595")
						{
							flag = true;
							stringWriter.WriteLine(compilerError.ToString());
						}
					}
					if (flag)
					{
						throw new InvalidOperationException(stringWriter.ToString());
					}
				}
				assembly = compilerResults.CompiledAssembly;
			}
			catch (UnauthorizedAccessException)
			{
				string currentUser = Compiler.GetCurrentUser();
				if (currentUser == null || currentUser.Length == 0)
				{
					throw new UnauthorizedAccessException(Res.GetString("XmlSerializerAccessDenied"));
				}
				throw new UnauthorizedAccessException(Res.GetString("XmlIdentityAccessDenied", new object[]
				{
					currentUser
				}));
			}
			catch (FileLoadException innerException)
			{
				throw new InvalidOperationException(Res.GetString("XmlSerializerCompileFailed"), innerException);
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			if (assembly == null)
			{
				throw new InvalidOperationException(Res.GetString("XmlInternalError"));
			}
			return assembly;
		}

		// Token: 0x060016F7 RID: 5879 RVA: 0x00066248 File Offset: 0x00064448
		private static string AssemblyNameFromOptions(string options)
		{
			if (options == null || options.Length == 0)
			{
				return null;
			}
			string result = null;
			string[] array = options.ToLower(CultureInfo.InvariantCulture).Split(null);
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i].Trim();
				if (text.StartsWith("/out:", StringComparison.Ordinal))
				{
					result = text.Substring(5);
				}
			}
			return result;
		}

		// Token: 0x060016F8 RID: 5880 RVA: 0x000662A4 File Offset: 0x000644A4
		internal static string GetCurrentUser()
		{
			try
			{
				WindowsIdentity current = WindowsIdentity.GetCurrent();
				if (current != null && current.Name != null)
				{
					return current.Name;
				}
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
			}
			return "";
		}

		// Token: 0x04000AAD RID: 2733
		private bool debugEnabled = DiagnosticsSwitches.KeepTempFiles.Enabled;

		// Token: 0x04000AAE RID: 2734
		private Hashtable imports = new Hashtable();

		// Token: 0x04000AAF RID: 2735
		private StringWriter writer = new StringWriter(CultureInfo.InvariantCulture);
	}
}
