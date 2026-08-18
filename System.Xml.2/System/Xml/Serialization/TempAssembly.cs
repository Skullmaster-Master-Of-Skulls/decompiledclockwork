using System;
using System.Collections;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Xml.Serialization.Configuration;
using Microsoft.Win32;

namespace System.Xml.Serialization
{
	// Token: 0x0200013B RID: 315
	internal class TempAssembly
	{
		// Token: 0x060016CC RID: 5836 RVA: 0x000646F2 File Offset: 0x000628F2
		private TempAssembly()
		{
		}

		// Token: 0x060016CD RID: 5837 RVA: 0x00064708 File Offset: 0x00062908
		internal TempAssembly(XmlMapping[] xmlMappings, Type[] types, string defaultNamespace, string location, Evidence evidence)
		{
			bool flag = false;
			for (int i = 0; i < xmlMappings.Length; i++)
			{
				xmlMappings[i].CheckShallow();
				if (xmlMappings[i].IsSoap)
				{
					flag = true;
				}
			}
			bool flag2 = false;
			if (!flag && !TempAssembly.UseLegacySerializerGeneration)
			{
				try
				{
					this.assembly = TempAssembly.GenerateRefEmitAssembly(xmlMappings, types, defaultNamespace, evidence);
					goto IL_5A;
				}
				catch (CodeGeneratorConversionException)
				{
					flag2 = true;
					goto IL_5A;
				}
			}
			flag2 = true;
			IL_5A:
			if (flag2)
			{
				this.assembly = TempAssembly.GenerateAssembly(xmlMappings, types, defaultNamespace, evidence, XmlSerializerCompilerParameters.Create(location), null, this.assemblies);
			}
			this.InitAssemblyMethods(xmlMappings);
		}

		// Token: 0x060016CE RID: 5838 RVA: 0x000647A8 File Offset: 0x000629A8
		internal TempAssembly(XmlMapping[] xmlMappings, Assembly assembly, XmlSerializerImplementation contract)
		{
			this.assembly = assembly;
			this.InitAssemblyMethods(xmlMappings);
			this.contract = contract;
			this.pregeneratedAssmbly = true;
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x060016CF RID: 5839 RVA: 0x000647D8 File Offset: 0x000629D8
		internal static bool UseLegacySerializerGeneration
		{
			get
			{
				if (AppSettings.UseLegacySerializerGeneration != null)
				{
					return AppSettings.UseLegacySerializerGeneration.Value;
				}
				XmlSerializerSection xmlSerializerSection = ConfigurationManager.GetSection(ConfigurationStrings.XmlSerializerSectionPath) as XmlSerializerSection;
				return xmlSerializerSection != null && xmlSerializerSection.UseLegacySerializerGeneration;
			}
		}

		// Token: 0x060016D0 RID: 5840 RVA: 0x0006481D File Offset: 0x00062A1D
		internal TempAssembly(XmlSerializerImplementation contract)
		{
			this.contract = contract;
			this.pregeneratedAssmbly = true;
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x060016D1 RID: 5841 RVA: 0x0006483E File Offset: 0x00062A3E
		internal XmlSerializerImplementation Contract
		{
			get
			{
				if (this.contract == null)
				{
					this.contract = (XmlSerializerImplementation)Activator.CreateInstance(TempAssembly.GetTypeFromAssembly(this.assembly, "XmlSerializerContract"));
				}
				return this.contract;
			}
		}

		// Token: 0x060016D2 RID: 5842 RVA: 0x00064870 File Offset: 0x00062A70
		internal void InitAssemblyMethods(XmlMapping[] xmlMappings)
		{
			this.methods = new TempAssembly.TempMethodDictionary();
			for (int i = 0; i < xmlMappings.Length; i++)
			{
				TempAssembly.TempMethod tempMethod = new TempAssembly.TempMethod();
				tempMethod.isSoap = xmlMappings[i].IsSoap;
				tempMethod.methodKey = xmlMappings[i].Key;
				XmlTypeMapping xmlTypeMapping = xmlMappings[i] as XmlTypeMapping;
				if (xmlTypeMapping != null)
				{
					tempMethod.name = xmlTypeMapping.ElementName;
					tempMethod.ns = xmlTypeMapping.Namespace;
				}
				this.methods.Add(xmlMappings[i].Key, tempMethod);
			}
		}

		// Token: 0x060016D3 RID: 5843 RVA: 0x000648F0 File Offset: 0x00062AF0
		internal static Assembly LoadGeneratedAssembly(Type type, string defaultNamespace, out XmlSerializerImplementation contract)
		{
			Assembly assembly = null;
			contract = null;
			string text = null;
			if (UnsafeNativeMethods.IsPackagedProcess.Value)
			{
				return null;
			}
			bool enabled = DiagnosticsSwitches.PregenEventLog.Enabled;
			object[] customAttributes = type.GetCustomAttributes(typeof(XmlSerializerAssemblyAttribute), false);
			if (customAttributes.Length == 0)
			{
				AssemblyName name = TempAssembly.GetName(type.Assembly, true);
				text = Compiler.GetTempAssemblyName(name, defaultNamespace);
				name.Name = text;
				name.CodeBase = null;
				name.CultureInfo = CultureInfo.InvariantCulture;
				try
				{
					assembly = Assembly.Load(name);
				}
				catch (Exception ex)
				{
					if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
					{
						throw;
					}
					if (enabled)
					{
						TempAssembly.Log(ex.Message, EventLogEntryType.Information);
					}
					byte[] publicKeyToken = name.GetPublicKeyToken();
					if (publicKeyToken != null && publicKeyToken.Length != 0)
					{
						return null;
					}
					assembly = Assembly.LoadWithPartialName(text, null);
				}
				if (assembly == null)
				{
					if (enabled)
					{
						TempAssembly.Log(Res.GetString("XmlPregenCannotLoad", new object[]
						{
							text
						}), EventLogEntryType.Information);
					}
					return null;
				}
				if (!TempAssembly.IsSerializerVersionMatch(assembly, type, defaultNamespace, null))
				{
					if (enabled)
					{
						TempAssembly.Log(Res.GetString("XmlSerializerExpiredDetails", new object[]
						{
							text,
							type.FullName
						}), EventLogEntryType.Error);
					}
					return null;
				}
				goto IL_1D1;
			}
			XmlSerializerAssemblyAttribute xmlSerializerAssemblyAttribute = (XmlSerializerAssemblyAttribute)customAttributes[0];
			if (xmlSerializerAssemblyAttribute.AssemblyName != null && xmlSerializerAssemblyAttribute.CodeBase != null)
			{
				throw new InvalidOperationException(Res.GetString("XmlPregenInvalidXmlSerializerAssemblyAttribute", new object[]
				{
					"AssemblyName",
					"CodeBase"
				}));
			}
			if (xmlSerializerAssemblyAttribute.AssemblyName != null)
			{
				text = xmlSerializerAssemblyAttribute.AssemblyName;
				assembly = Assembly.LoadWithPartialName(text, null);
			}
			else if (xmlSerializerAssemblyAttribute.CodeBase != null && xmlSerializerAssemblyAttribute.CodeBase.Length > 0)
			{
				text = xmlSerializerAssemblyAttribute.CodeBase;
				assembly = Assembly.LoadFrom(text);
			}
			else
			{
				text = type.Assembly.FullName;
				assembly = type.Assembly;
			}
			if (assembly == null)
			{
				throw new FileNotFoundException(null, text);
			}
			IL_1D1:
			Type typeFromAssembly = TempAssembly.GetTypeFromAssembly(assembly, "XmlSerializerContract");
			contract = (XmlSerializerImplementation)Activator.CreateInstance(typeFromAssembly);
			if (contract.CanSerialize(type))
			{
				return assembly;
			}
			if (enabled)
			{
				TempAssembly.Log(Res.GetString("XmlSerializerExpiredDetails", new object[]
				{
					text,
					type.FullName
				}), EventLogEntryType.Error);
			}
			return null;
		}

		// Token: 0x060016D4 RID: 5844 RVA: 0x00064B30 File Offset: 0x00062D30
		private static void Log(string message, EventLogEntryType type)
		{
			new EventLogPermission(PermissionState.Unrestricted).Assert();
			EventLog.WriteEntry("XmlSerializer", message, type);
		}

		// Token: 0x060016D5 RID: 5845 RVA: 0x00064B4C File Offset: 0x00062D4C
		private static AssemblyName GetName(Assembly assembly, bool copyName)
		{
			PermissionSet permissionSet = new PermissionSet(PermissionState.None);
			permissionSet.AddPermission(new FileIOPermission(PermissionState.Unrestricted));
			permissionSet.Assert();
			return assembly.GetName(copyName);
		}

		// Token: 0x060016D6 RID: 5846 RVA: 0x00064B7C File Offset: 0x00062D7C
		private static bool IsSerializerVersionMatch(Assembly serializer, Type type, string defaultNamespace, string location)
		{
			if (serializer == null)
			{
				return false;
			}
			object[] customAttributes = serializer.GetCustomAttributes(typeof(XmlSerializerVersionAttribute), false);
			if (customAttributes.Length != 1)
			{
				return false;
			}
			XmlSerializerVersionAttribute xmlSerializerVersionAttribute = (XmlSerializerVersionAttribute)customAttributes[0];
			return xmlSerializerVersionAttribute.ParentAssemblyId == TempAssembly.GenerateAssemblyId(type) && xmlSerializerVersionAttribute.Namespace == defaultNamespace;
		}

		// Token: 0x060016D7 RID: 5847 RVA: 0x00064BDC File Offset: 0x00062DDC
		private static string GenerateAssemblyId(Type type)
		{
			Module[] modules = type.Assembly.GetModules();
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < modules.Length; i++)
			{
				arrayList.Add(modules[i].ModuleVersionId.ToString());
			}
			arrayList.Sort();
			StringBuilder stringBuilder = new StringBuilder();
			for (int j = 0; j < arrayList.Count; j++)
			{
				stringBuilder.Append(arrayList[j].ToString());
				stringBuilder.Append(",");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060016D8 RID: 5848 RVA: 0x00064C70 File Offset: 0x00062E70
		internal static Assembly GenerateAssembly(XmlMapping[] xmlMappings, Type[] types, string defaultNamespace, Evidence evidence, XmlSerializerCompilerParameters parameters, Assembly assembly, Hashtable assemblies)
		{
			TempAssembly.FileIOPermission.Assert();
			Compiler compiler = new Compiler();
			Assembly result;
			try
			{
				Hashtable hashtable = new Hashtable();
				foreach (XmlMapping xmlMapping in xmlMappings)
				{
					hashtable[xmlMapping.Scope] = xmlMapping;
				}
				TypeScope[] array = new TypeScope[hashtable.Keys.Count];
				hashtable.Keys.CopyTo(array, 0);
				assemblies.Clear();
				Hashtable types2 = new Hashtable();
				foreach (TypeScope typeScope in array)
				{
					foreach (object obj in typeScope.Types)
					{
						Type type = (Type)obj;
						compiler.AddImport(type, types2);
						Assembly assembly2 = type.Assembly;
						string fullName = assembly2.FullName;
						if (assemblies[fullName] == null && !assembly2.GlobalAssemblyCache)
						{
							assemblies[fullName] = assembly2;
						}
					}
				}
				for (int k = 0; k < types.Length; k++)
				{
					compiler.AddImport(types[k], types2);
				}
				compiler.AddImport(typeof(object).Assembly);
				compiler.AddImport(typeof(XmlSerializer).Assembly);
				IndentedWriter indentedWriter = new IndentedWriter(compiler.Source, false);
				indentedWriter.WriteLine("#if _DYNAMIC_XMLSERIALIZER_COMPILATION");
				indentedWriter.WriteLine("[assembly:System.Security.AllowPartiallyTrustedCallers()]");
				indentedWriter.WriteLine("[assembly:System.Security.SecurityTransparent()]");
				indentedWriter.WriteLine("[assembly:System.Security.SecurityRules(System.Security.SecurityRuleSet.Level1)]");
				indentedWriter.WriteLine("#endif");
				if (types != null && types.Length != 0 && types[0] != null)
				{
					indentedWriter.WriteLine("[assembly:System.Reflection.AssemblyVersionAttribute(\"" + types[0].Assembly.GetName().Version.ToString() + "\")]");
				}
				if (assembly != null && types.Length != 0)
				{
					for (int l = 0; l < types.Length; l++)
					{
						Type type2 = types[l];
						if (!(type2 == null) && DynamicAssemblies.IsTypeDynamic(type2))
						{
							throw new InvalidOperationException(Res.GetString("XmlPregenTypeDynamic", new object[]
							{
								types[l].FullName
							}));
						}
					}
					indentedWriter.Write("[assembly:");
					indentedWriter.Write(typeof(XmlSerializerVersionAttribute).FullName);
					indentedWriter.Write("(");
					indentedWriter.Write("ParentAssemblyId=");
					ReflectionAwareCodeGen.WriteQuotedCSharpString(indentedWriter, TempAssembly.GenerateAssemblyId(types[0]));
					indentedWriter.Write(", Version=");
					ReflectionAwareCodeGen.WriteQuotedCSharpString(indentedWriter, "4.0.0.0");
					if (defaultNamespace != null)
					{
						indentedWriter.Write(", Namespace=");
						ReflectionAwareCodeGen.WriteQuotedCSharpString(indentedWriter, defaultNamespace);
					}
					indentedWriter.WriteLine(")]");
				}
				CodeIdentifiers codeIdentifiers = new CodeIdentifiers();
				codeIdentifiers.AddUnique("XmlSerializationWriter", "XmlSerializationWriter");
				codeIdentifiers.AddUnique("XmlSerializationReader", "XmlSerializationReader");
				string text = null;
				if (types != null && types.Length == 1 && types[0] != null)
				{
					text = CodeIdentifier.MakeValid(types[0].Name);
					if (types[0].IsArray)
					{
						text += "Array";
					}
				}
				indentedWriter.WriteLine("namespace Microsoft.Xml.Serialization.GeneratedAssembly {");
				IndentedWriter indentedWriter2 = indentedWriter;
				int indent = indentedWriter2.Indent;
				indentedWriter2.Indent = indent + 1;
				indentedWriter.WriteLine();
				string text2 = "XmlSerializationWriter" + text;
				text2 = codeIdentifiers.AddUnique(text2, text2);
				XmlSerializationWriterCodeGen xmlSerializationWriterCodeGen = new XmlSerializationWriterCodeGen(indentedWriter, array, "public", text2);
				xmlSerializationWriterCodeGen.GenerateBegin();
				string[] array3 = new string[xmlMappings.Length];
				for (int m = 0; m < xmlMappings.Length; m++)
				{
					array3[m] = xmlSerializationWriterCodeGen.GenerateElement(xmlMappings[m]);
				}
				xmlSerializationWriterCodeGen.GenerateEnd();
				indentedWriter.WriteLine();
				string text3 = "XmlSerializationReader" + text;
				text3 = codeIdentifiers.AddUnique(text3, text3);
				XmlSerializationReaderCodeGen xmlSerializationReaderCodeGen = new XmlSerializationReaderCodeGen(indentedWriter, array, "public", text3);
				xmlSerializationReaderCodeGen.GenerateBegin();
				string[] array4 = new string[xmlMappings.Length];
				for (int n = 0; n < xmlMappings.Length; n++)
				{
					array4[n] = xmlSerializationReaderCodeGen.GenerateElement(xmlMappings[n]);
				}
				xmlSerializationReaderCodeGen.GenerateEnd(array4, xmlMappings, types);
				string baseSerializer = xmlSerializationReaderCodeGen.GenerateBaseSerializer("XmlSerializer1", text3, text2, codeIdentifiers);
				Hashtable hashtable2 = new Hashtable();
				for (int num = 0; num < xmlMappings.Length; num++)
				{
					if (hashtable2[xmlMappings[num].Key] == null)
					{
						hashtable2[xmlMappings[num].Key] = xmlSerializationReaderCodeGen.GenerateTypedSerializer(array4[num], array3[num], xmlMappings[num], codeIdentifiers, baseSerializer, text3, text2);
					}
				}
				xmlSerializationReaderCodeGen.GenerateSerializerContract("XmlSerializerContract", xmlMappings, types, text3, array4, text2, array3, hashtable2);
				IndentedWriter indentedWriter3 = indentedWriter;
				indent = indentedWriter3.Indent;
				indentedWriter3.Indent = indent - 1;
				indentedWriter.WriteLine("}");
				result = compiler.Compile(assembly, defaultNamespace, parameters, evidence);
			}
			finally
			{
				compiler.Close();
			}
			return result;
		}

		// Token: 0x060016D9 RID: 5849 RVA: 0x00065190 File Offset: 0x00063390
		internal static Assembly GenerateRefEmitAssembly(XmlMapping[] xmlMappings, Type[] types, string defaultNamespace, Evidence evidence)
		{
			Hashtable hashtable = new Hashtable();
			foreach (XmlMapping xmlMapping in xmlMappings)
			{
				hashtable[xmlMapping.Scope] = xmlMapping;
			}
			TypeScope[] array = new TypeScope[hashtable.Keys.Count];
			hashtable.Keys.CopyTo(array, 0);
			string text = "Microsoft.GeneratedCode";
			AssemblyBuilder assemblyBuilder = CodeGenerator.CreateAssemblyBuilder(AppDomain.CurrentDomain, text);
			ConstructorInfo constructor = typeof(SecurityTransparentAttribute).GetConstructor(CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			assemblyBuilder.SetCustomAttribute(new CustomAttributeBuilder(constructor, new object[0]));
			ConstructorInfo constructor2 = typeof(AllowPartiallyTrustedCallersAttribute).GetConstructor(CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			assemblyBuilder.SetCustomAttribute(new CustomAttributeBuilder(constructor2, new object[0]));
			ConstructorInfo constructor3 = typeof(SecurityRulesAttribute).GetConstructor(CodeGenerator.InstanceBindingFlags, null, new Type[]
			{
				typeof(SecurityRuleSet)
			}, null);
			assemblyBuilder.SetCustomAttribute(new CustomAttributeBuilder(constructor3, new object[]
			{
				SecurityRuleSet.Level1
			}));
			if (types != null && types.Length != 0 && types[0] != null)
			{
				ConstructorInfo constructor4 = typeof(AssemblyVersionAttribute).GetConstructor(CodeGenerator.InstanceBindingFlags, null, new Type[]
				{
					typeof(string)
				}, null);
				TempAssembly.FileIOPermission.Assert();
				string text2 = types[0].Assembly.GetName().Version.ToString();
				CodeAccessPermission.RevertAssert();
				assemblyBuilder.SetCustomAttribute(new CustomAttributeBuilder(constructor4, new object[]
				{
					text2
				}));
			}
			CodeIdentifiers codeIdentifiers = new CodeIdentifiers();
			codeIdentifiers.AddUnique("XmlSerializationWriter", "XmlSerializationWriter");
			codeIdentifiers.AddUnique("XmlSerializationReader", "XmlSerializationReader");
			string text3 = null;
			if (types != null && types.Length == 1 && types[0] != null)
			{
				text3 = CodeIdentifier.MakeValid(types[0].Name);
				if (types[0].IsArray)
				{
					text3 += "Array";
				}
			}
			ModuleBuilder moduleBuilder = CodeGenerator.CreateModuleBuilder(assemblyBuilder, text);
			string text4 = "XmlSerializationWriter" + text3;
			text4 = codeIdentifiers.AddUnique(text4, text4);
			XmlSerializationWriterILGen xmlSerializationWriterILGen = new XmlSerializationWriterILGen(array, "public", text4);
			xmlSerializationWriterILGen.ModuleBuilder = moduleBuilder;
			xmlSerializationWriterILGen.GenerateBegin();
			string[] array2 = new string[xmlMappings.Length];
			for (int j = 0; j < xmlMappings.Length; j++)
			{
				array2[j] = xmlSerializationWriterILGen.GenerateElement(xmlMappings[j]);
			}
			Type type = xmlSerializationWriterILGen.GenerateEnd();
			string text5 = "XmlSerializationReader" + text3;
			text5 = codeIdentifiers.AddUnique(text5, text5);
			XmlSerializationReaderILGen xmlSerializationReaderILGen = new XmlSerializationReaderILGen(array, "public", text5);
			xmlSerializationReaderILGen.ModuleBuilder = moduleBuilder;
			xmlSerializationReaderILGen.CreatedTypes.Add(type.Name, type);
			xmlSerializationReaderILGen.GenerateBegin();
			string[] array3 = new string[xmlMappings.Length];
			for (int k = 0; k < xmlMappings.Length; k++)
			{
				array3[k] = xmlSerializationReaderILGen.GenerateElement(xmlMappings[k]);
			}
			xmlSerializationReaderILGen.GenerateEnd(array3, xmlMappings, types);
			string baseSerializer = xmlSerializationReaderILGen.GenerateBaseSerializer("XmlSerializer1", text5, text4, codeIdentifiers);
			Hashtable hashtable2 = new Hashtable();
			for (int l = 0; l < xmlMappings.Length; l++)
			{
				if (hashtable2[xmlMappings[l].Key] == null)
				{
					hashtable2[xmlMappings[l].Key] = xmlSerializationReaderILGen.GenerateTypedSerializer(array3[l], array2[l], xmlMappings[l], codeIdentifiers, baseSerializer, text5, text4);
				}
			}
			xmlSerializationReaderILGen.GenerateSerializerContract("XmlSerializerContract", xmlMappings, types, text5, array3, text4, array2, hashtable2);
			if (DiagnosticsSwitches.KeepTempFiles.Enabled)
			{
				TempAssembly.FileIOPermission.Assert();
				assemblyBuilder.Save(text + ".dll");
			}
			return type.Assembly;
		}

		// Token: 0x060016DA RID: 5850 RVA: 0x0006553C File Offset: 0x0006373C
		private static MethodInfo GetMethodFromType(Type type, string methodName, Assembly assembly)
		{
			MethodInfo method = type.GetMethod(methodName);
			if (method != null)
			{
				return method;
			}
			MissingMethodException ex = new MissingMethodException(type.FullName, methodName);
			if (assembly != null)
			{
				throw new InvalidOperationException(Res.GetString("XmlSerializerExpired", new object[]
				{
					assembly.FullName,
					assembly.CodeBase
				}), ex);
			}
			throw ex;
		}

		// Token: 0x060016DB RID: 5851 RVA: 0x0006559C File Offset: 0x0006379C
		internal static Type GetTypeFromAssembly(Assembly assembly, string typeName)
		{
			typeName = "Microsoft.Xml.Serialization.GeneratedAssembly." + typeName;
			Type type = assembly.GetType(typeName);
			if (type == null)
			{
				throw new InvalidOperationException(Res.GetString("XmlMissingType", new object[]
				{
					typeName,
					assembly.FullName
				}));
			}
			return type;
		}

		// Token: 0x060016DC RID: 5852 RVA: 0x000655EC File Offset: 0x000637EC
		internal bool CanRead(XmlMapping mapping, XmlReader xmlReader)
		{
			if (mapping == null)
			{
				return false;
			}
			if (mapping.Accessor.Any)
			{
				return true;
			}
			TempAssembly.TempMethod tempMethod = this.methods[mapping.Key];
			return xmlReader.IsStartElement(tempMethod.name, tempMethod.ns);
		}

		// Token: 0x060016DD RID: 5853 RVA: 0x00065634 File Offset: 0x00063834
		private string ValidateEncodingStyle(string encodingStyle, string methodKey)
		{
			if (encodingStyle != null && encodingStyle.Length > 0)
			{
				if (!this.methods[methodKey].isSoap)
				{
					throw new InvalidOperationException(Res.GetString("XmlInvalidEncodingNotEncoded1", new object[]
					{
						encodingStyle
					}));
				}
				if (encodingStyle != "http://schemas.xmlsoap.org/soap/encoding/" && encodingStyle != "http://www.w3.org/2003/05/soap-encoding")
				{
					throw new InvalidOperationException(Res.GetString("XmlInvalidEncoding3", new object[]
					{
						encodingStyle,
						"http://schemas.xmlsoap.org/soap/encoding/",
						"http://www.w3.org/2003/05/soap-encoding"
					}));
				}
			}
			else if (this.methods[methodKey].isSoap)
			{
				encodingStyle = "http://schemas.xmlsoap.org/soap/encoding/";
			}
			return encodingStyle;
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x060016DE RID: 5854 RVA: 0x000656D9 File Offset: 0x000638D9
		internal static FileIOPermission FileIOPermission
		{
			get
			{
				if (TempAssembly.fileIOPermission == null)
				{
					TempAssembly.fileIOPermission = new FileIOPermission(PermissionState.Unrestricted);
				}
				return TempAssembly.fileIOPermission;
			}
		}

		// Token: 0x060016DF RID: 5855 RVA: 0x000656F8 File Offset: 0x000638F8
		internal object InvokeReader(XmlMapping mapping, XmlReader xmlReader, XmlDeserializationEvents events, string encodingStyle)
		{
			XmlSerializationReader xmlSerializationReader = null;
			object result;
			try
			{
				encodingStyle = this.ValidateEncodingStyle(encodingStyle, mapping.Key);
				xmlSerializationReader = this.Contract.Reader;
				xmlSerializationReader.Init(xmlReader, events, encodingStyle, this);
				if (this.methods[mapping.Key].readMethod == null)
				{
					if (this.readerMethods == null)
					{
						this.readerMethods = this.Contract.ReadMethods;
					}
					string text = (string)this.readerMethods[mapping.Key];
					if (text == null)
					{
						throw new InvalidOperationException(Res.GetString("XmlNotSerializable", new object[]
						{
							mapping.Accessor.Name
						}));
					}
					this.methods[mapping.Key].readMethod = TempAssembly.GetMethodFromType(xmlSerializationReader.GetType(), text, this.pregeneratedAssmbly ? this.assembly : null);
				}
				result = this.methods[mapping.Key].readMethod.Invoke(xmlSerializationReader, TempAssembly.emptyObjectArray);
			}
			catch (SecurityException innerException)
			{
				throw new InvalidOperationException(Res.GetString("XmlNoPartialTrust"), innerException);
			}
			finally
			{
				if (xmlSerializationReader != null)
				{
					xmlSerializationReader.Dispose();
				}
			}
			return result;
		}

		// Token: 0x060016E0 RID: 5856 RVA: 0x00065850 File Offset: 0x00063A50
		internal void InvokeWriter(XmlMapping mapping, XmlWriter xmlWriter, object o, XmlSerializerNamespaces namespaces, string encodingStyle, string id)
		{
			XmlSerializationWriter xmlSerializationWriter = null;
			try
			{
				encodingStyle = this.ValidateEncodingStyle(encodingStyle, mapping.Key);
				xmlSerializationWriter = this.Contract.Writer;
				xmlSerializationWriter.Init(xmlWriter, namespaces, encodingStyle, id, this);
				if (this.methods[mapping.Key].writeMethod == null)
				{
					if (this.writerMethods == null)
					{
						this.writerMethods = this.Contract.WriteMethods;
					}
					string text = (string)this.writerMethods[mapping.Key];
					if (text == null)
					{
						throw new InvalidOperationException(Res.GetString("XmlNotSerializable", new object[]
						{
							mapping.Accessor.Name
						}));
					}
					this.methods[mapping.Key].writeMethod = TempAssembly.GetMethodFromType(xmlSerializationWriter.GetType(), text, this.pregeneratedAssmbly ? this.assembly : null);
				}
				this.methods[mapping.Key].writeMethod.Invoke(xmlSerializationWriter, new object[]
				{
					o
				});
			}
			catch (SecurityException innerException)
			{
				throw new InvalidOperationException(Res.GetString("XmlNoPartialTrust"), innerException);
			}
			finally
			{
				if (xmlSerializationWriter != null)
				{
					xmlSerializationWriter.Dispose();
				}
			}
		}

		// Token: 0x060016E1 RID: 5857 RVA: 0x000659B0 File Offset: 0x00063BB0
		internal Assembly GetReferencedAssembly(string name)
		{
			if (this.assemblies == null || name == null)
			{
				return null;
			}
			return (Assembly)this.assemblies[name];
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x060016E2 RID: 5858 RVA: 0x000659D0 File Offset: 0x00063BD0
		internal bool NeedAssembyResolve
		{
			get
			{
				return this.assemblies != null && this.assemblies.Count > 0;
			}
		}

		// Token: 0x04000A9E RID: 2718
		internal const string GeneratedAssemblyNamespace = "Microsoft.Xml.Serialization.GeneratedAssembly";

		// Token: 0x04000A9F RID: 2719
		private Assembly assembly;

		// Token: 0x04000AA0 RID: 2720
		private bool pregeneratedAssmbly;

		// Token: 0x04000AA1 RID: 2721
		private XmlSerializerImplementation contract;

		// Token: 0x04000AA2 RID: 2722
		private Hashtable writerMethods;

		// Token: 0x04000AA3 RID: 2723
		private Hashtable readerMethods;

		// Token: 0x04000AA4 RID: 2724
		private TempAssembly.TempMethodDictionary methods;

		// Token: 0x04000AA5 RID: 2725
		private static object[] emptyObjectArray = new object[0];

		// Token: 0x04000AA6 RID: 2726
		private Hashtable assemblies = new Hashtable();

		// Token: 0x04000AA7 RID: 2727
		private static volatile FileIOPermission fileIOPermission;

		// Token: 0x02000478 RID: 1144
		internal class TempMethod
		{
			// Token: 0x04001DC8 RID: 7624
			internal MethodInfo writeMethod;

			// Token: 0x04001DC9 RID: 7625
			internal MethodInfo readMethod;

			// Token: 0x04001DCA RID: 7626
			internal string name;

			// Token: 0x04001DCB RID: 7627
			internal string ns;

			// Token: 0x04001DCC RID: 7628
			internal bool isSoap;

			// Token: 0x04001DCD RID: 7629
			internal string methodKey;
		}

		// Token: 0x02000479 RID: 1145
		internal sealed class TempMethodDictionary : DictionaryBase
		{
			// Token: 0x17000A40 RID: 2624
			internal TempAssembly.TempMethod this[string key]
			{
				get
				{
					return (TempAssembly.TempMethod)base.Dictionary[key];
				}
			}

			// Token: 0x060030C4 RID: 12484 RVA: 0x0011D609 File Offset: 0x0011B809
			internal void Add(string key, TempAssembly.TempMethod value)
			{
				base.Dictionary.Add(key, value);
			}
		}
	}
}
