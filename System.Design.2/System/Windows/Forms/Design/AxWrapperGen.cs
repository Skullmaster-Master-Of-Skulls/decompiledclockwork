using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Design;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.CSharp;
using Microsoft.Win32;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000298 RID: 664
	public class AxWrapperGen
	{
		// Token: 0x06001958 RID: 6488 RVA: 0x0008DDFC File Offset: 0x0008BFFC
		public AxWrapperGen(Type axType)
		{
			this.axctl = axType.Name;
			this.axctl = this.axctl.TrimStart(new char[]
			{
				'_',
				'1'
			});
			this.axctl = "Ax" + this.axctl;
			this.clsidAx = axType.GUID;
			CustomAttributeData[] attributeData = AxWrapperGen.GetAttributeData(axType, typeof(ComSourceInterfacesAttribute));
			if (attributeData.Length == 0 && axType.BaseType.GUID.Equals(axType.GUID))
			{
				attributeData = AxWrapperGen.GetAttributeData(axType.BaseType, typeof(ComSourceInterfacesAttribute));
			}
			if (attributeData.Length != 0)
			{
				CustomAttributeData customAttributeData = attributeData[0];
				string text = customAttributeData.ConstructorArguments[0].Value.ToString();
				int length = text.IndexOfAny(new char[1]);
				string text2 = text.Substring(0, length);
				this.axctlEventsType = axType.Module.Assembly.GetType(text2);
				if (this.axctlEventsType == null)
				{
					this.axctlEventsType = Type.GetType(text2, false);
				}
				if (this.axctlEventsType != null)
				{
					this.axctlEvents = this.axctlEventsType.FullName;
				}
			}
			Type[] interfaces = axType.GetInterfaces();
			this.axctlType = interfaces[0];
			foreach (Type type in interfaces)
			{
				attributeData = AxWrapperGen.GetAttributeData(type, typeof(CoClassAttribute));
				if (attributeData.Length != 0)
				{
					Type[] interfaces2 = type.GetInterfaces();
					if (interfaces2 != null && interfaces2.Length != 0)
					{
						this.axctl = "Ax" + type.Name;
						this.axctlType = interfaces2[0];
						break;
					}
				}
			}
			this.axctlIface = this.axctlType.Name;
			foreach (Type left in interfaces)
			{
				if (left == typeof(IEnumerable))
				{
					this.enumerableInterface = true;
					break;
				}
			}
			try
			{
				attributeData = AxWrapperGen.GetAttributeData(this.axctlType, typeof(InterfaceTypeAttribute));
				if (attributeData.Length != 0)
				{
					CustomAttributeData customAttributeData2 = attributeData[0];
					this.dispInterface = customAttributeData2.ConstructorArguments[0].Value.Equals(ComInterfaceType.InterfaceIsIDispatch);
				}
			}
			catch (MissingMethodException)
			{
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06001959 RID: 6489 RVA: 0x0008E064 File Offset: 0x0008C264
		private Hashtable AxHostMembers
		{
			get
			{
				if (this.axHostMembers == null)
				{
					this.FillAxHostMembers();
				}
				return this.axHostMembers;
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x0600195A RID: 6490 RVA: 0x0008E07A File Offset: 0x0008C27A
		private Hashtable ConflictableThings
		{
			get
			{
				if (this.conflictableThings == null)
				{
					this.FillConflicatableThings();
				}
				return this.conflictableThings;
			}
		}

		// Token: 0x0600195B RID: 6491 RVA: 0x0008E090 File Offset: 0x0008C290
		private void AddClassToNamespace(CodeNamespace ns, CodeTypeDeclaration cls)
		{
			if (AxWrapperGen.classesInNamespace == null)
			{
				AxWrapperGen.classesInNamespace = new Hashtable();
			}
			try
			{
				ns.Types.Add(cls);
				AxWrapperGen.classesInNamespace.Add(cls.Name, cls);
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x0600195C RID: 6492 RVA: 0x0008E0E4 File Offset: 0x0008C2E4
		private AxWrapperGen.EventEntry AddEvent(string name, string eventCls, string eventHandlerCls, Type retType, AxParameterData[] parameters)
		{
			if (this.events == null)
			{
				this.events = new ArrayList();
			}
			if (this.axctlTypeMembers == null)
			{
				this.axctlTypeMembers = new Hashtable();
				Type type = this.axctlType;
				MemberInfo[] members = type.GetMembers();
				foreach (MemberInfo memberInfo in members)
				{
					string name2 = memberInfo.Name;
					if (!this.axctlTypeMembers.Contains(name2))
					{
						this.axctlTypeMembers.Add(name2, memberInfo);
					}
				}
			}
			bool conflict = this.axctlTypeMembers.Contains(name) || this.AxHostMembers.Contains(name) || this.ConflictableThings.Contains(name);
			AxWrapperGen.EventEntry eventEntry = new AxWrapperGen.EventEntry(name, eventCls, eventHandlerCls, retType, parameters, conflict);
			this.events.Add(eventEntry);
			return eventEntry;
		}

		// Token: 0x0600195D RID: 6493 RVA: 0x0008E1B1 File Offset: 0x0008C3B1
		private bool ClassAlreadyExistsInNamespace(CodeNamespace ns, string clsName)
		{
			return AxWrapperGen.classesInNamespace.Contains(clsName);
		}

		// Token: 0x0600195E RID: 6494 RVA: 0x0008E1C0 File Offset: 0x0008C3C0
		private static string Compile(AxImporter importer, CodeNamespace ns, string[] refAssemblies, DateTime tlbTimeStamp, Version version)
		{
			CodeDomProvider codeDomProvider = new CSharpCodeProvider();
			ICodeGenerator codeGenerator = codeDomProvider.CreateGenerator();
			string outputName = importer.options.outputName;
			string text = Path.Combine(importer.options.outputDirectory, outputName);
			string text2 = Path.ChangeExtension(text, ".cs");
			CompilerParameters compilerParameters = new CompilerParameters(refAssemblies, text);
			compilerParameters.IncludeDebugInformation = importer.options.genSources;
			CodeCompileUnit codeCompileUnit = new CodeCompileUnit();
			codeCompileUnit.Namespaces.Add(ns);
			CodeAttributeDeclarationCollection assemblyCustomAttributes = codeCompileUnit.AssemblyCustomAttributes;
			assemblyCustomAttributes.Add(new CodeAttributeDeclaration("System.Reflection.AssemblyVersion", new CodeAttributeArgument[]
			{
				new CodeAttributeArgument(new CodePrimitiveExpression(version.ToString()))
			}));
			assemblyCustomAttributes.Add(new CodeAttributeDeclaration("System.Windows.Forms.AxHost.TypeLibraryTimeStamp", new CodeAttributeArgument[]
			{
				new CodeAttributeArgument(new CodePrimitiveExpression(tlbTimeStamp.ToString(CultureInfo.InvariantCulture)))
			}));
			if (importer.options.delaySign)
			{
				assemblyCustomAttributes.Add(new CodeAttributeDeclaration("System.Reflection.AssemblyDelaySign", new CodeAttributeArgument[]
				{
					new CodeAttributeArgument(new CodePrimitiveExpression(true))
				}));
			}
			if (importer.options.keyFile != null && importer.options.keyFile.Length > 0)
			{
				assemblyCustomAttributes.Add(new CodeAttributeDeclaration("System.Reflection.AssemblyKeyFile", new CodeAttributeArgument[]
				{
					new CodeAttributeArgument(new CodePrimitiveExpression(importer.options.keyFile))
				}));
			}
			if (importer.options.keyContainer != null && importer.options.keyContainer.Length > 0)
			{
				assemblyCustomAttributes.Add(new CodeAttributeDeclaration("System.Reflection.AssemblyKeyName", new CodeAttributeArgument[]
				{
					new CodeAttributeArgument(new CodePrimitiveExpression(importer.options.keyContainer))
				}));
			}
			CompilerResults compilerResults;
			if (importer.options.genSources)
			{
				AxWrapperGen.SaveCompileUnit(codeGenerator, codeCompileUnit, text2);
				compilerResults = ((ICodeCompiler)codeGenerator).CompileAssemblyFromFile(compilerParameters, text2);
			}
			else
			{
				compilerResults = ((ICodeCompiler)codeGenerator).CompileAssemblyFromDom(compilerParameters, codeCompileUnit);
			}
			if (compilerResults.Errors != null && compilerResults.Errors.Count > 0)
			{
				string text3 = null;
				CompilerErrorCollection errors = compilerResults.Errors;
				foreach (object obj in errors)
				{
					CompilerError compilerError = (CompilerError)obj;
					if (!compilerError.IsWarning)
					{
						text3 = text3 + compilerError.ToString() + "\r\n";
					}
				}
				if (text3 != null)
				{
					AxWrapperGen.SaveCompileUnit(codeGenerator, codeCompileUnit, text2);
					text3 = SR.GetString("AXCompilerError", new object[]
					{
						ns.Name,
						text2
					}) + "\r\n" + text3;
					throw new Exception(text3);
				}
			}
			return text;
		}

		// Token: 0x0600195F RID: 6495 RVA: 0x0008E484 File Offset: 0x0008C684
		private string CreateDataSourceFieldName(string propName)
		{
			return "ax" + propName;
		}

		// Token: 0x06001960 RID: 6496 RVA: 0x0008E494 File Offset: 0x0008C694
		private CodeParameterDeclarationExpression CreateParamDecl(string type, string name, bool isOptional)
		{
			CodeParameterDeclarationExpression codeParameterDeclarationExpression = new CodeParameterDeclarationExpression(type, name);
			if (!isOptional)
			{
				return codeParameterDeclarationExpression;
			}
			codeParameterDeclarationExpression.CustomAttributes = new CodeAttributeDeclarationCollection
			{
				new CodeAttributeDeclaration("System.Runtime.InteropServices.Optional", new CodeAttributeArgument[0])
			};
			return codeParameterDeclarationExpression;
		}

		// Token: 0x06001961 RID: 6497 RVA: 0x0008E4D4 File Offset: 0x0008C6D4
		private CodeConditionStatement CreateValidStateCheck()
		{
			CodeConditionStatement codeConditionStatement = new CodeConditionStatement();
			CodeBinaryOperatorExpression left = new CodeBinaryOperatorExpression(this.memIfaceRef, CodeBinaryOperatorType.IdentityInequality, new CodePrimitiveExpression(null));
			CodeBinaryOperatorExpression right = new CodeBinaryOperatorExpression(new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "PropsValid", new CodeExpression[0]), CodeBinaryOperatorType.IdentityEquality, new CodePrimitiveExpression(true));
			CodeBinaryOperatorExpression condition = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.BooleanAnd, right);
			return new CodeConditionStatement
			{
				Condition = condition
			};
		}

		// Token: 0x06001962 RID: 6498 RVA: 0x0008E53C File Offset: 0x0008C73C
		private CodeStatement CreateInvalidStateException(string name, string kind)
		{
			CodeBinaryOperatorExpression condition = new CodeBinaryOperatorExpression(this.memIfaceRef, CodeBinaryOperatorType.IdentityEquality, new CodePrimitiveExpression(null));
			CodeConditionStatement codeConditionStatement = new CodeConditionStatement();
			codeConditionStatement.Condition = condition;
			CodeExpression[] parameters = new CodeExpression[]
			{
				new CodePrimitiveExpression(name),
				new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(null, typeof(AxHost).FullName + ".ActiveXInvokeKind"), kind)
			};
			CodeObjectCreateExpression toThrow = new CodeObjectCreateExpression(typeof(AxHost.InvalidActiveXStateException).FullName, parameters);
			codeConditionStatement.TrueStatements.Add(new CodeThrowExceptionStatement(toThrow));
			return codeConditionStatement;
		}

		// Token: 0x06001963 RID: 6499 RVA: 0x0008E5CC File Offset: 0x0008C7CC
		private void FillAxHostMembers()
		{
			if (this.axHostMembers == null)
			{
				this.axHostMembers = new Hashtable(StringComparer.CurrentCultureIgnoreCase);
				Type typeFromHandle = typeof(AxHost);
				MemberInfo[] members = typeFromHandle.GetMembers();
				foreach (MemberInfo memberInfo in members)
				{
					string name = memberInfo.Name;
					if (!this.axHostMembers.Contains(name))
					{
						FieldInfo fieldInfo = memberInfo as FieldInfo;
						if (fieldInfo != null && !fieldInfo.IsPrivate)
						{
							this.axHostMembers.Add(name, memberInfo);
						}
						else
						{
							PropertyInfo left = memberInfo as PropertyInfo;
							if (left != null)
							{
								this.axHostMembers.Add(name, memberInfo);
							}
							else
							{
								MethodBase methodBase = memberInfo as MethodBase;
								if (methodBase != null && !methodBase.IsPrivate)
								{
									this.axHostMembers.Add(name, memberInfo);
								}
								else
								{
									EventInfo left2 = memberInfo as EventInfo;
									if (left2 != null)
									{
										this.axHostMembers.Add(name, memberInfo);
									}
									else
									{
										Type type = memberInfo as Type;
										if (type != null && (type.IsPublic || type.IsNestedPublic))
										{
											this.axHostMembers.Add(name, memberInfo);
										}
										else
										{
											this.axHostMembers.Add(name, memberInfo);
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x0008E729 File Offset: 0x0008C929
		private void FillConflicatableThings()
		{
			if (this.conflictableThings == null)
			{
				this.conflictableThings = new Hashtable();
				this.conflictableThings.Add("System", "System");
			}
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x0008E754 File Offset: 0x0008C954
		private static void SaveCompileUnit(ICodeGenerator codegen, CodeCompileUnit cu, string fileName)
		{
			try
			{
				try
				{
					if (File.Exists(fileName))
					{
						File.Delete(fileName);
					}
				}
				catch
				{
				}
				FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
				StreamWriter streamWriter = new StreamWriter(fileStream, new UTF8Encoding(false));
				codegen.GenerateCodeFromCompileUnit(cu, streamWriter, null);
				streamWriter.Flush();
				streamWriter.Close();
				fileStream.Close();
				AxWrapperGen.GeneratedSources.Add(fileName);
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06001966 RID: 6502 RVA: 0x0008E7D4 File Offset: 0x0008C9D4
		internal static string MapTypeName(Type type)
		{
			bool isArray = type.IsArray;
			Type elementType = type.GetElementType();
			if (elementType != null)
			{
				type = elementType;
			}
			string fullName = type.FullName;
			if (!isArray)
			{
				return fullName;
			}
			return fullName + "[]";
		}

		// Token: 0x06001967 RID: 6503 RVA: 0x0008E814 File Offset: 0x0008CA14
		private static bool IsTypeComObject(Type type)
		{
			if (type.IsClass && type.IsCOMObject && type.IsPublic && !type.GUID.Equals(Guid.Empty))
			{
				try
				{
					CustomAttributeData[] attributeData = AxWrapperGen.GetAttributeData(type, typeof(ComVisibleAttribute));
					if (attributeData.Length != 0 && attributeData[0].ConstructorArguments[0].Value.Equals(false))
					{
						return false;
					}
				}
				catch
				{
					return false;
				}
				return true;
			}
			return false;
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x0008E8A8 File Offset: 0x0008CAA8
		private static bool IsTypeActiveXControl(Type type)
		{
			if (AxWrapperGen.IsTypeComObject(type))
			{
				string name = "CLSID\\{" + type.GUID.ToString() + "}\\Control";
				RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey(name);
				if (registryKey == null)
				{
					return false;
				}
				registryKey.Close();
				Type[] interfaces = type.GetInterfaces();
				if (interfaces != null && interfaces.Length >= 1)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001969 RID: 6505 RVA: 0x0008E90C File Offset: 0x0008CB0C
		internal static CustomAttributeData[] GetAttributeData(ICustomAttributeProvider attributeProvider, Type attributeType)
		{
			List<CustomAttributeData> list = new List<CustomAttributeData>();
			IList<CustomAttributeData> list2 = null;
			if (attributeProvider is Assembly)
			{
				list2 = CustomAttributeData.GetCustomAttributes(attributeProvider as Assembly);
			}
			else if (attributeProvider is MemberInfo)
			{
				list2 = CustomAttributeData.GetCustomAttributes(attributeProvider as MemberInfo);
			}
			else if (attributeProvider is Module)
			{
				list2 = CustomAttributeData.GetCustomAttributes(attributeProvider as Module);
			}
			else if (attributeProvider is ParameterInfo)
			{
				list2 = CustomAttributeData.GetCustomAttributes(attributeProvider as ParameterInfo);
			}
			if (list2 != null)
			{
				foreach (CustomAttributeData customAttributeData in list2)
				{
					if (customAttributeData.ToString().Contains(attributeType.ToString()))
					{
						list.Add(customAttributeData);
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x0600196A RID: 6506 RVA: 0x0008E9D0 File Offset: 0x0008CBD0
		internal static string GenerateWrappers(AxImporter importer, Guid axClsid, Assembly rcwAssem, string[] refAssemblies, DateTime tlbTimeStamp, bool ignoreRegisteredOcx, out string assem)
		{
			assem = null;
			bool flag = false;
			CodeNamespace codeNamespace = null;
			string text = null;
			try
			{
				Type[] types = rcwAssem.GetTypes();
				for (int i = 0; i < types.Length; i++)
				{
					if ((ignoreRegisteredOcx && AxWrapperGen.IsTypeComObject(types[i])) || (!ignoreRegisteredOcx && AxWrapperGen.IsTypeActiveXControl(types[i])))
					{
						flag = true;
						if (codeNamespace == null)
						{
							AxWrapperGen.axctlNS = "Ax" + types[i].Namespace;
							codeNamespace = new CodeNamespace(AxWrapperGen.axctlNS);
						}
						AxWrapperGen axWrapperGen = new AxWrapperGen(types[i]);
						axWrapperGen.GenerateAxHost(codeNamespace, refAssemblies);
						if (!axClsid.Equals(Guid.Empty) && axClsid.Equals(types[i].GUID))
						{
							text = axWrapperGen.axctl;
						}
						else if (axClsid.Equals(Guid.Empty) && text == null)
						{
							text = axWrapperGen.axctl;
						}
					}
				}
			}
			finally
			{
				if (AxWrapperGen.classesInNamespace != null)
				{
					AxWrapperGen.classesInNamespace.Clear();
					AxWrapperGen.classesInNamespace = null;
				}
			}
			AssemblyName name = rcwAssem.GetName();
			if (flag)
			{
				Version version = name.Version;
				assem = AxWrapperGen.Compile(importer, codeNamespace, refAssemblies, tlbTimeStamp, version);
				if (assem != null)
				{
					if (text == null)
					{
						string name2 = "AXNotValidControl";
						object[] array = new object[1];
						int num = 0;
						string str = "{";
						Guid guid = axClsid;
						array[num] = str + guid.ToString() + "}";
						throw new Exception(SR.GetString(name2, array));
					}
					return string.Concat(new string[]
					{
						AxWrapperGen.axctlNS,
						".",
						text,
						",",
						AxWrapperGen.axctlNS
					});
				}
			}
			return null;
		}

		// Token: 0x0600196B RID: 6507 RVA: 0x0008EB70 File Offset: 0x0008CD70
		private void GenerateAxHost(CodeNamespace ns, string[] refAssemblies)
		{
			CodeTypeDeclaration codeTypeDeclaration = new CodeTypeDeclaration();
			codeTypeDeclaration.Name = this.axctl;
			codeTypeDeclaration.BaseTypes.Add(typeof(AxHost).FullName);
			if (this.enumerableInterface)
			{
				codeTypeDeclaration.BaseTypes.Add(typeof(IEnumerable));
			}
			CodeAttributeDeclarationCollection codeAttributeDeclarationCollection = new CodeAttributeDeclarationCollection();
			CodeAttributeDeclaration value = new CodeAttributeDeclaration(typeof(AxHost.ClsidAttribute).FullName, new CodeAttributeArgument[]
			{
				new CodeAttributeArgument(new CodeSnippetExpression("\"{" + this.clsidAx.ToString() + "}\""))
			});
			codeAttributeDeclarationCollection.Add(value);
			CodeAttributeDeclaration value2 = new CodeAttributeDeclaration(typeof(DesignTimeVisibleAttribute).FullName, new CodeAttributeArgument[]
			{
				new CodeAttributeArgument(new CodePrimitiveExpression(true))
			});
			codeAttributeDeclarationCollection.Add(value2);
			codeTypeDeclaration.CustomAttributes = codeAttributeDeclarationCollection;
			CustomAttributeData[] attributeData = AxWrapperGen.GetAttributeData(this.axctlType, typeof(DefaultMemberAttribute));
			if (attributeData != null && attributeData.Length != 0)
			{
				this.defMember = attributeData[0].ConstructorArguments[0].Value.ToString();
			}
			this.AddClassToNamespace(ns, codeTypeDeclaration);
			this.WriteMembersDecl(codeTypeDeclaration);
			if (this.axctlEventsType != null)
			{
				this.WriteEventMembersDecl(ns, codeTypeDeclaration);
			}
			CodeConstructor codeConstructor = this.WriteConstructor(codeTypeDeclaration);
			this.WriteProperties(codeTypeDeclaration);
			this.WriteMethods(codeTypeDeclaration);
			this.WriteHookupMethods(codeTypeDeclaration);
			if (this.aboutBoxMethod != null)
			{
				CodeObjectCreateExpression codeObjectCreateExpression = new CodeObjectCreateExpression("AboutBoxDelegate", new CodeExpression[0]);
				codeObjectCreateExpression.Parameters.Add(new CodeFieldReferenceExpression(null, this.aboutBoxMethod));
				CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "SetAboutBoxDelegate", new CodeExpression[0]);
				codeMethodInvokeExpression.Parameters.Add(codeObjectCreateExpression);
				codeConstructor.Statements.Add(new CodeExpressionStatement(codeMethodInvokeExpression));
			}
			if (this.axctlEventsType != null)
			{
				this.WriteEvents(ns, codeTypeDeclaration);
			}
			if (this.dataSourceProps.Count > 0)
			{
				this.WriteOnInPlaceActive(codeTypeDeclaration);
			}
		}

		// Token: 0x0600196C RID: 6508 RVA: 0x0008ED78 File Offset: 0x0008CF78
		private CodeExpression GetInitializer(Type type)
		{
			if (type == null)
			{
				return new CodePrimitiveExpression(null);
			}
			if (type == typeof(int) || type == typeof(short) || type == typeof(long) || type == typeof(float) || type == typeof(double) || typeof(Enum).IsAssignableFrom(type))
			{
				return new CodePrimitiveExpression(0);
			}
			if (type == typeof(char))
			{
				return new CodeCastExpression("System.Character", new CodePrimitiveExpression(0));
			}
			if (type == typeof(bool))
			{
				return new CodePrimitiveExpression(false);
			}
			return new CodePrimitiveExpression(null);
		}

		// Token: 0x0600196D RID: 6509 RVA: 0x0008EE5C File Offset: 0x0008D05C
		private bool IsDispidKnown(int dp, string propName)
		{
			return dp == -513 || dp == -501 || dp == -512 || dp == -514 || dp == -516 || dp == -611 || dp == -517 || dp == -515 || (dp == 0 && propName.Equals(this.defMember));
		}

		// Token: 0x0600196E RID: 6510 RVA: 0x0000445B File Offset: 0x0000265B
		private bool IsEventPresent(MethodInfo mievent)
		{
			return false;
		}

		// Token: 0x0600196F RID: 6511 RVA: 0x0008EEBC File Offset: 0x0008D0BC
		private bool IsPropertyBindable(PropertyInfo pinfo, out bool isDefaultBind)
		{
			isDefaultBind = false;
			MethodInfo getMethod = pinfo.GetGetMethod();
			if (getMethod == null)
			{
				return false;
			}
			CustomAttributeData[] attributeData = AxWrapperGen.GetAttributeData(getMethod, typeof(TypeLibFuncAttribute));
			if (attributeData != null && attributeData.Length != 0)
			{
				int num = int.Parse(attributeData[0].ConstructorArguments[0].Value.ToString());
				isDefaultBind = ((num & 32) != 0);
				if (isDefaultBind || (num & 4) != 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001970 RID: 6512 RVA: 0x0008EF2C File Offset: 0x0008D12C
		private bool IsPropertyBrowsable(PropertyInfo pinfo, AxWrapperGen.ComAliasEnum alias)
		{
			MethodInfo getMethod = pinfo.GetGetMethod();
			if (getMethod == null)
			{
				return false;
			}
			CustomAttributeData[] attributeData = AxWrapperGen.GetAttributeData(getMethod, typeof(TypeLibFuncAttribute));
			if (attributeData != null && attributeData.Length != 0)
			{
				int num = int.Parse(attributeData[0].ConstructorArguments[0].Value.ToString());
				if ((num & 1024) != 0 || (num & 64) != 0)
				{
					return false;
				}
			}
			Type propertyType = pinfo.PropertyType;
			return alias != AxWrapperGen.ComAliasEnum.None || !propertyType.IsInterface || propertyType.GUID.Equals(AxWrapperGen.Guid_DataSource);
		}

		// Token: 0x06001971 RID: 6513 RVA: 0x0008EFC4 File Offset: 0x0008D1C4
		private bool IsPropertySignature(PropertyInfo pinfo, out bool useLet)
		{
			int num = 0;
			bool flag = true;
			useLet = false;
			string value = (this.defMember == null) ? "Item" : this.defMember;
			if (pinfo.Name.Equals(value))
			{
				num = pinfo.GetIndexParameters().Length;
			}
			if (pinfo.GetGetMethod() != null)
			{
				flag = this.IsPropertySignature(pinfo.GetGetMethod(), pinfo.PropertyType, true, num);
			}
			if (pinfo.GetSetMethod() != null)
			{
				flag = (flag && this.IsPropertySignature(pinfo.GetSetMethod(), pinfo.PropertyType, false, num + 1));
				if (!flag)
				{
					MethodInfo method = pinfo.DeclaringType.GetMethod("let_" + pinfo.Name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
					if (method != null)
					{
						flag = this.IsPropertySignature(method, pinfo.PropertyType, false, num + 1);
						useLet = true;
					}
				}
			}
			return flag;
		}

		// Token: 0x06001972 RID: 6514 RVA: 0x0008F094 File Offset: 0x0008D294
		private bool IsPropertySignature(MethodInfo method, out bool hasPropInfo, out bool useLet)
		{
			useLet = false;
			hasPropInfo = false;
			if (!method.Name.StartsWith("get_") && !method.Name.StartsWith("set_") && !method.Name.StartsWith("let_"))
			{
				return false;
			}
			string name = method.Name.Substring(4, method.Name.Length - 4);
			PropertyInfo property = this.axctlType.GetProperty(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
			return !(property == null) && this.IsPropertySignature(property, out useLet);
		}

		// Token: 0x06001973 RID: 6515 RVA: 0x0008F120 File Offset: 0x0008D320
		private bool IsPropertySignature(MethodInfo method, Type returnType, bool getter, int nParams)
		{
			if (method.IsConstructor)
			{
				return false;
			}
			if (getter)
			{
				string name = method.Name.Substring(4);
				if (this.axctlType.GetProperty(name) != null && method.GetParameters().Length == nParams)
				{
					return method.ReturnType == returnType;
				}
			}
			else
			{
				string name2 = method.Name.Substring(4);
				ParameterInfo[] parameters = method.GetParameters();
				if (this.axctlType.GetProperty(name2) != null && parameters.Length == nParams)
				{
					return parameters.Length == 0 || parameters[parameters.Length - 1].ParameterType == returnType || (method.Name.StartsWith("let_") && parameters[parameters.Length - 1].ParameterType == typeof(object));
				}
			}
			return false;
		}

		// Token: 0x06001974 RID: 6516 RVA: 0x0008F1F8 File Offset: 0x0008D3F8
		private bool OptionalsPresent(MethodInfo method)
		{
			AxParameterData[] array = AxParameterData.Convert(method.GetParameters());
			if (array != null && array.Length != 0)
			{
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].IsOptional)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001975 RID: 6517 RVA: 0x0008F234 File Offset: 0x0008D434
		private string ResolveConflict(string name, Type returnType, out bool fOverride, out bool fUseNew)
		{
			fOverride = false;
			fUseNew = false;
			string result = "";
			try
			{
				if (AxWrapperGen.axHostPropDescs == null)
				{
					AxWrapperGen.axHostPropDescs = new Hashtable();
					PropertyInfo[] properties = typeof(AxHost).GetProperties();
					foreach (PropertyInfo propertyInfo in properties)
					{
						string key = propertyInfo.Name + propertyInfo.PropertyType.GetHashCode().ToString();
						if (!AxWrapperGen.axHostPropDescs.Contains(key))
						{
							AxWrapperGen.axHostPropDescs.Add(key, propertyInfo);
						}
					}
				}
				PropertyInfo propertyInfo2 = (PropertyInfo)AxWrapperGen.axHostPropDescs[name + returnType.GetHashCode().ToString()];
				if (propertyInfo2 != null)
				{
					if (returnType.Equals(propertyInfo2.PropertyType))
					{
						bool flag = propertyInfo2.CanRead && propertyInfo2.GetGetMethod().IsVirtual;
						if (flag)
						{
							fOverride = true;
						}
						else
						{
							fUseNew = true;
						}
					}
					else
					{
						result = "Ctl";
					}
				}
				else if (this.AxHostMembers.Contains(name) || this.ConflictableThings.Contains(name))
				{
					result = "Ctl";
				}
				else if ((name.StartsWith("get_") || name.StartsWith("set_")) && TypeDescriptor.GetProperties(typeof(AxHost))[name.Substring(4)] != null)
				{
					result = "Ctl";
				}
			}
			catch (AmbiguousMatchException)
			{
				result = "Ctl";
			}
			return result;
		}

		// Token: 0x06001976 RID: 6518 RVA: 0x0008F3C0 File Offset: 0x0008D5C0
		private CodeConstructor WriteConstructor(CodeTypeDeclaration cls)
		{
			CodeConstructor codeConstructor = new CodeConstructor();
			codeConstructor.Attributes = MemberAttributes.Public;
			codeConstructor.BaseConstructorArgs.Add(new CodeSnippetExpression("\"" + this.clsidAx.ToString() + "\""));
			cls.Members.Add(codeConstructor);
			return codeConstructor;
		}

		// Token: 0x06001977 RID: 6519 RVA: 0x0008F420 File Offset: 0x0008D620
		private void WriteOnInPlaceActive(CodeTypeDeclaration cls)
		{
			CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
			codeMemberMethod.Name = "OnInPlaceActive";
			codeMemberMethod.Attributes = (MemberAttributes)12292;
			CodeMethodInvokeExpression expression = new CodeMethodInvokeExpression(new CodeBaseReferenceExpression(), "OnInPlaceActive", new CodeExpression[0]);
			codeMemberMethod.Statements.Add(new CodeExpressionStatement(expression));
			foreach (object obj in this.dataSourceProps)
			{
				PropertyInfo propertyInfo = (PropertyInfo)obj;
				string fieldName = this.CreateDataSourceFieldName(propertyInfo.Name);
				CodeBinaryOperatorExpression condition = new CodeBinaryOperatorExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), fieldName), CodeBinaryOperatorType.IdentityInequality, new CodePrimitiveExpression(null));
				CodeConditionStatement codeConditionStatement = new CodeConditionStatement();
				codeConditionStatement.Condition = condition;
				CodeExpression left = new CodeFieldReferenceExpression(this.memIfaceRef, propertyInfo.Name);
				CodeExpression right = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), fieldName);
				codeConditionStatement.TrueStatements.Add(new CodeAssignStatement(left, right));
				codeMemberMethod.Statements.Add(codeConditionStatement);
			}
			cls.Members.Add(codeMemberMethod);
		}

		// Token: 0x06001978 RID: 6520 RVA: 0x0008F54C File Offset: 0x0008D74C
		private string WriteEventClass(CodeNamespace ns, MethodInfo mi, ParameterInfo[] pinfos)
		{
			string text = this.axctlEventsType.Name + "_" + mi.Name + "Event";
			if (this.ClassAlreadyExistsInNamespace(ns, text))
			{
				return text;
			}
			CodeTypeDeclaration codeTypeDeclaration = new CodeTypeDeclaration();
			codeTypeDeclaration.Name = text;
			AxParameterData[] array = AxParameterData.Convert(pinfos);
			for (int i = 0; i < array.Length; i++)
			{
				CodeMemberField codeMemberField = new CodeMemberField(array[i].TypeName, array[i].Name);
				codeMemberField.Attributes = (MemberAttributes)24578;
				codeTypeDeclaration.Members.Add(codeMemberField);
			}
			CodeConstructor codeConstructor = new CodeConstructor();
			codeConstructor.Attributes = MemberAttributes.Public;
			for (int j = 0; j < array.Length; j++)
			{
				if (array[j].Direction != FieldDirection.Out)
				{
					codeConstructor.Parameters.Add(this.CreateParamDecl(array[j].TypeName, array[j].Name, false));
					CodeFieldReferenceExpression left = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), array[j].Name);
					CodeFieldReferenceExpression right = new CodeFieldReferenceExpression(null, array[j].Name);
					CodeAssignStatement value = new CodeAssignStatement(left, right);
					codeConstructor.Statements.Add(value);
				}
			}
			codeTypeDeclaration.Members.Add(codeConstructor);
			this.AddClassToNamespace(ns, codeTypeDeclaration);
			return text;
		}

		// Token: 0x06001979 RID: 6521 RVA: 0x0008F688 File Offset: 0x0008D888
		private string WriteEventHandlerClass(CodeNamespace ns, MethodInfo mi)
		{
			string text = this.axctlEventsType.Name + "_" + mi.Name + "EventHandler";
			if (this.ClassAlreadyExistsInNamespace(ns, text))
			{
				return text;
			}
			this.AddClassToNamespace(ns, new CodeTypeDelegate
			{
				Name = text,
				Parameters = 
				{
					this.CreateParamDecl(typeof(object).FullName, "sender", false),
					this.CreateParamDecl(this.axctlEventsType.Name + "_" + mi.Name + "Event", "e", false)
				},
				ReturnType = new CodeTypeReference(mi.ReturnType)
			});
			return text;
		}

		// Token: 0x0600197A RID: 6522 RVA: 0x0008F748 File Offset: 0x0008D948
		private void WriteEventMembersDecl(CodeNamespace ns, CodeTypeDeclaration cls)
		{
			bool flag = false;
			MethodInfo[] methods = this.axctlEventsType.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
			for (int i = 0; i < methods.Length; i++)
			{
				AxWrapperGen.EventEntry eventEntry = null;
				if (!this.IsEventPresent(methods[i]))
				{
					ParameterInfo[] parameters = methods[i].GetParameters();
					if (parameters.Length != 0 || methods[i].ReturnType != typeof(void))
					{
						string eventHandlerCls = this.WriteEventHandlerClass(ns, methods[i]);
						string eventCls = this.WriteEventClass(ns, methods[i], parameters);
						eventEntry = this.AddEvent(methods[i].Name, eventCls, eventHandlerCls, methods[i].ReturnType, AxParameterData.Convert(parameters));
					}
					else
					{
						eventEntry = this.AddEvent(methods[i].Name, "System.EventArgs", "System.EventHandler", typeof(void), new AxParameterData[0]);
					}
				}
				if (!flag)
				{
					CustomAttributeData[] attributeData = AxWrapperGen.GetAttributeData(methods[i], typeof(DispIdAttribute));
					if (attributeData != null && attributeData.Length != 0)
					{
						CustomAttributeData customAttributeData = attributeData[0];
						if (int.Parse(customAttributeData.ConstructorArguments[0].Value.ToString()) == 1)
						{
							string value = (eventEntry != null) ? eventEntry.resovledEventName : methods[i].Name;
							CodeAttributeDeclaration value2 = new CodeAttributeDeclaration("System.ComponentModel.DefaultEvent", new CodeAttributeArgument[]
							{
								new CodeAttributeArgument(new CodePrimitiveExpression(value))
							});
							cls.CustomAttributes.Add(value2);
							flag = true;
						}
					}
				}
			}
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x0008F8AC File Offset: 0x0008DAAC
		private string WriteEventMulticaster(CodeNamespace ns)
		{
			string text = this.axctl + "EventMulticaster";
			if (this.ClassAlreadyExistsInNamespace(ns, text))
			{
				return text;
			}
			CodeTypeDeclaration codeTypeDeclaration = new CodeTypeDeclaration();
			codeTypeDeclaration.Name = text;
			codeTypeDeclaration.BaseTypes.Add(this.axctlEvents);
			CodeAttributeDeclarationCollection codeAttributeDeclarationCollection = new CodeAttributeDeclarationCollection();
			CodeAttributeDeclaration value = new CodeAttributeDeclaration("System.Runtime.InteropServices.ClassInterface", new CodeAttributeArgument[]
			{
				new CodeAttributeArgument(new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(null, "System.Runtime.InteropServices.ClassInterfaceType"), "None"))
			});
			codeAttributeDeclarationCollection.Add(value);
			codeTypeDeclaration.CustomAttributes = codeAttributeDeclarationCollection;
			CodeMemberField codeMemberField = new CodeMemberField(this.axctl, "parent");
			codeMemberField.Attributes = (MemberAttributes)20482;
			codeTypeDeclaration.Members.Add(codeMemberField);
			CodeConstructor codeConstructor = new CodeConstructor();
			codeConstructor.Attributes = MemberAttributes.Public;
			codeConstructor.Parameters.Add(this.CreateParamDecl(this.axctl, "parent", false));
			CodeFieldReferenceExpression left = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "parent");
			CodeFieldReferenceExpression right = new CodeFieldReferenceExpression(null, "parent");
			codeConstructor.Statements.Add(new CodeAssignStatement(left, right));
			codeTypeDeclaration.Members.Add(codeConstructor);
			MethodInfo[] methods = this.axctlEventsType.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
			int num = 0;
			for (int i = 0; i < methods.Length; i++)
			{
				AxParameterData[] array = AxParameterData.Convert(methods[i].GetParameters());
				CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
				codeMemberMethod.Name = methods[i].Name;
				codeMemberMethod.Attributes = MemberAttributes.Public;
				codeMemberMethod.ReturnType = new CodeTypeReference(AxWrapperGen.MapTypeName(methods[i].ReturnType));
				for (int j = 0; j < array.Length; j++)
				{
					CodeParameterDeclarationExpression codeParameterDeclarationExpression = this.CreateParamDecl(AxWrapperGen.MapTypeName(array[j].ParameterType), array[j].Name, array[j].IsOptional);
					codeParameterDeclarationExpression.Direction = array[j].Direction;
					codeMemberMethod.Parameters.Add(codeParameterDeclarationExpression);
				}
				CodeFieldReferenceExpression codeFieldReferenceExpression = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "parent");
				if (!this.IsEventPresent(methods[i]))
				{
					AxWrapperGen.EventEntry eventEntry = (AxWrapperGen.EventEntry)this.events[num++];
					CodeExpressionCollection codeExpressionCollection = new CodeExpressionCollection();
					codeExpressionCollection.Add(codeFieldReferenceExpression);
					if (eventEntry.eventCls.Equals("EventArgs"))
					{
						codeExpressionCollection.Add(new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(null, "EventArgs"), "Empty"));
						CodeExpression[] array2 = new CodeExpression[codeExpressionCollection.Count];
						((ICollection)codeExpressionCollection).CopyTo(array2, 0);
						CodeMethodInvokeExpression expression = new CodeMethodInvokeExpression(codeFieldReferenceExpression, eventEntry.invokeMethodName, array2);
						if (methods[i].ReturnType == typeof(void))
						{
							codeMemberMethod.Statements.Add(new CodeExpressionStatement(expression));
						}
						else
						{
							codeMemberMethod.Statements.Add(new CodeMethodReturnStatement(expression));
						}
					}
					else
					{
						CodeObjectCreateExpression codeObjectCreateExpression = new CodeObjectCreateExpression(eventEntry.eventCls, new CodeExpression[0]);
						for (int k = 0; k < eventEntry.parameters.Length; k++)
						{
							if (!eventEntry.parameters[k].IsOut)
							{
								codeObjectCreateExpression.Parameters.Add(new CodeFieldReferenceExpression(null, eventEntry.parameters[k].Name));
							}
						}
						CodeVariableDeclarationStatement codeVariableDeclarationStatement = new CodeVariableDeclarationStatement(eventEntry.eventCls, eventEntry.eventParam);
						codeVariableDeclarationStatement.InitExpression = codeObjectCreateExpression;
						codeMemberMethod.Statements.Add(codeVariableDeclarationStatement);
						codeExpressionCollection.Add(new CodeFieldReferenceExpression(null, eventEntry.eventParam));
						CodeExpression[] array3 = new CodeExpression[codeExpressionCollection.Count];
						((ICollection)codeExpressionCollection).CopyTo(array3, 0);
						CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(codeFieldReferenceExpression, eventEntry.invokeMethodName, array3);
						if (methods[i].ReturnType == typeof(void))
						{
							codeMemberMethod.Statements.Add(new CodeExpressionStatement(codeMethodInvokeExpression));
						}
						else
						{
							CodeVariableDeclarationStatement codeVariableDeclarationStatement2 = new CodeVariableDeclarationStatement(eventEntry.retType, eventEntry.invokeMethodName);
							codeMemberMethod.Statements.Add(codeVariableDeclarationStatement2);
							codeMemberMethod.Statements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(null, codeVariableDeclarationStatement2.Name), codeMethodInvokeExpression));
						}
						for (int l = 0; l < array.Length; l++)
						{
							if (array[l].IsByRef)
							{
								codeMemberMethod.Statements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(null, array[l].Name), new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(null, codeVariableDeclarationStatement.Name), array[l].Name)));
							}
						}
						if (methods[i].ReturnType != typeof(void))
						{
							codeMemberMethod.Statements.Add(new CodeMethodReturnStatement(new CodeFieldReferenceExpression(null, eventEntry.invokeMethodName)));
						}
					}
				}
				else
				{
					CodeExpressionCollection codeExpressionCollection2 = new CodeExpressionCollection();
					for (int m = 0; m < array.Length; m++)
					{
						codeExpressionCollection2.Add(new CodeFieldReferenceExpression(null, array[m].Name));
					}
					CodeExpression[] array4 = new CodeExpression[codeExpressionCollection2.Count];
					((ICollection)codeExpressionCollection2).CopyTo(array4, 0);
					CodeMethodInvokeExpression expression2 = new CodeMethodInvokeExpression(codeFieldReferenceExpression, "RaiseOn" + methods[i].Name, array4);
					if (methods[i].ReturnType == typeof(void))
					{
						codeMemberMethod.Statements.Add(new CodeExpressionStatement(expression2));
					}
					else
					{
						codeMemberMethod.Statements.Add(new CodeMethodReturnStatement(expression2));
					}
				}
				codeTypeDeclaration.Members.Add(codeMemberMethod);
			}
			this.AddClassToNamespace(ns, codeTypeDeclaration);
			return text;
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x0008FE4C File Offset: 0x0008E04C
		private void WriteEvents(CodeNamespace ns, CodeTypeDeclaration cls)
		{
			int num = 0;
			while (this.events != null && num < this.events.Count)
			{
				AxWrapperGen.EventEntry eventEntry = (AxWrapperGen.EventEntry)this.events[num];
				CodeMemberEvent codeMemberEvent = new CodeMemberEvent();
				codeMemberEvent.Name = eventEntry.resovledEventName;
				codeMemberEvent.Attributes = eventEntry.eventFlags;
				codeMemberEvent.Type = new CodeTypeReference(eventEntry.eventHandlerCls);
				cls.Members.Add(codeMemberEvent);
				CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
				codeMemberMethod.Name = eventEntry.invokeMethodName;
				codeMemberMethod.ReturnType = new CodeTypeReference(eventEntry.retType);
				codeMemberMethod.Attributes = (MemberAttributes)4098;
				codeMemberMethod.Parameters.Add(this.CreateParamDecl(AxWrapperGen.MapTypeName(typeof(object)), "sender", false));
				codeMemberMethod.Parameters.Add(this.CreateParamDecl(eventEntry.eventCls, "e", false));
				CodeFieldReferenceExpression left = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), eventEntry.resovledEventName);
				CodeBinaryOperatorExpression condition = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.IdentityInequality, new CodePrimitiveExpression(null));
				CodeConditionStatement codeConditionStatement = new CodeConditionStatement();
				codeConditionStatement.Condition = condition;
				CodeExpressionCollection codeExpressionCollection = new CodeExpressionCollection();
				codeExpressionCollection.Add(new CodeFieldReferenceExpression(null, "sender"));
				codeExpressionCollection.Add(new CodeFieldReferenceExpression(null, "e"));
				CodeExpression[] array = new CodeExpression[codeExpressionCollection.Count];
				((ICollection)codeExpressionCollection).CopyTo(array, 0);
				CodeMethodInvokeExpression expression = new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), eventEntry.resovledEventName, array);
				if (eventEntry.retType == typeof(void))
				{
					codeConditionStatement.TrueStatements.Add(new CodeExpressionStatement(expression));
				}
				else
				{
					codeConditionStatement.TrueStatements.Add(new CodeMethodReturnStatement(expression));
					codeConditionStatement.FalseStatements.Add(new CodeMethodReturnStatement(this.GetInitializer(eventEntry.retType)));
				}
				codeMemberMethod.Statements.Add(codeConditionStatement);
				cls.Members.Add(codeMemberMethod);
				num++;
			}
			this.WriteEventMulticaster(ns);
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x0009004C File Offset: 0x0008E24C
		private void WriteHookupMethods(CodeTypeDeclaration cls)
		{
			if (this.axctlEventsType != null)
			{
				CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
				codeMemberMethod.Name = "CreateSink";
				codeMemberMethod.Attributes = (MemberAttributes)12292;
				CodeObjectCreateExpression codeObjectCreateExpression = new CodeObjectCreateExpression(this.axctl + "EventMulticaster", new CodeExpression[0]);
				codeObjectCreateExpression.Parameters.Add(new CodeThisReferenceExpression());
				CodeAssignStatement value = new CodeAssignStatement(this.multicasterRef, codeObjectCreateExpression);
				CodeObjectCreateExpression codeObjectCreateExpression2 = new CodeObjectCreateExpression(typeof(AxHost.ConnectionPointCookie).FullName, new CodeExpression[0]);
				codeObjectCreateExpression2.Parameters.Add(this.memIfaceRef);
				codeObjectCreateExpression2.Parameters.Add(this.multicasterRef);
				codeObjectCreateExpression2.Parameters.Add(new CodeTypeOfExpression(this.axctlEvents));
				CodeAssignStatement value2 = new CodeAssignStatement(this.cookieRef, codeObjectCreateExpression2);
				CodeTryCatchFinallyStatement codeTryCatchFinallyStatement = new CodeTryCatchFinallyStatement();
				codeTryCatchFinallyStatement.TryStatements.Add(value);
				codeTryCatchFinallyStatement.TryStatements.Add(value2);
				codeTryCatchFinallyStatement.CatchClauses.Add(new CodeCatchClause("", new CodeTypeReference(typeof(Exception))));
				codeMemberMethod.Statements.Add(codeTryCatchFinallyStatement);
				cls.Members.Add(codeMemberMethod);
				CodeMemberMethod codeMemberMethod2 = new CodeMemberMethod();
				codeMemberMethod2.Name = "DetachSink";
				codeMemberMethod2.Attributes = (MemberAttributes)12292;
				CodeFieldReferenceExpression targetObject = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), this.cookie);
				CodeMethodInvokeExpression value3 = new CodeMethodInvokeExpression(targetObject, "Disconnect", new CodeExpression[0]);
				codeTryCatchFinallyStatement = new CodeTryCatchFinallyStatement();
				codeTryCatchFinallyStatement.TryStatements.Add(value3);
				codeTryCatchFinallyStatement.CatchClauses.Add(new CodeCatchClause("", new CodeTypeReference(typeof(Exception))));
				codeMemberMethod2.Statements.Add(codeTryCatchFinallyStatement);
				cls.Members.Add(codeMemberMethod2);
			}
			CodeMemberMethod codeMemberMethod3 = new CodeMemberMethod();
			codeMemberMethod3.Name = "AttachInterfaces";
			codeMemberMethod3.Attributes = (MemberAttributes)12292;
			CodeCastExpression right = new CodeCastExpression(this.axctlType.FullName, new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "GetOcx", new CodeExpression[0]));
			CodeAssignStatement value4 = new CodeAssignStatement(this.memIfaceRef, right);
			CodeTryCatchFinallyStatement codeTryCatchFinallyStatement2 = new CodeTryCatchFinallyStatement();
			codeTryCatchFinallyStatement2.TryStatements.Add(value4);
			codeTryCatchFinallyStatement2.CatchClauses.Add(new CodeCatchClause("", new CodeTypeReference(typeof(Exception))));
			codeMemberMethod3.Statements.Add(codeTryCatchFinallyStatement2);
			cls.Members.Add(codeMemberMethod3);
		}

		// Token: 0x0600197E RID: 6526 RVA: 0x000902D8 File Offset: 0x0008E4D8
		private void WriteMembersDecl(CodeTypeDeclaration cls)
		{
			this.memIface = "ocx";
			this.memIfaceRef = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), this.memIface);
			cls.Members.Add(new CodeMemberField(AxWrapperGen.MapTypeName(this.axctlType), this.memIface));
			if (this.axctlEventsType != null)
			{
				this.multicaster = "eventMulticaster";
				this.multicasterRef = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), this.multicaster);
				cls.Members.Add(new CodeMemberField(this.axctl + "EventMulticaster", this.multicaster));
				this.cookie = "cookie";
				this.cookieRef = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), this.cookie);
				cls.Members.Add(new CodeMemberField(typeof(AxHost.ConnectionPointCookie).FullName, this.cookie));
			}
		}

		// Token: 0x0600197F RID: 6527 RVA: 0x000903C8 File Offset: 0x0008E5C8
		private void WriteMethod(CodeTypeDeclaration cls, MethodInfo method, bool hasPropInfo, bool removeOptionals)
		{
			AxWrapperGen.AxMethodGenerator axMethodGenerator = AxWrapperGen.AxMethodGenerator.Create(method, removeOptionals);
			axMethodGenerator.ControlType = this.axctlType;
			string text = method.Name;
			bool flag = false;
			bool flag2 = false;
			this.ResolveConflict(method.Name, method.ReturnType, out flag, out flag2);
			if (flag)
			{
				text = "Ctl" + text;
			}
			CodeMemberMethod codeMemberMethod = axMethodGenerator.CreateMethod(text);
			codeMemberMethod.Statements.Add(this.CreateInvalidStateException(codeMemberMethod.Name, "MethodInvoke"));
			List<CodeExpression> parameters = axMethodGenerator.GenerateAndMarshalParameters(codeMemberMethod);
			CodeExpression returnExpression = axMethodGenerator.DoMethodInvoke(codeMemberMethod, method.Name, this.memIfaceRef, parameters);
			axMethodGenerator.UnmarshalParameters(codeMemberMethod, parameters);
			axMethodGenerator.GenerateReturn(codeMemberMethod, returnExpression);
			cls.Members.Add(codeMemberMethod);
			CustomAttributeData[] attributeData = AxWrapperGen.GetAttributeData(method, typeof(DispIdAttribute));
			if (attributeData != null && attributeData.Length != 0)
			{
				int num = int.Parse(attributeData[0].ConstructorArguments[0].Value.ToString());
				if (num == -552 && method.GetParameters().Length == 0)
				{
					this.aboutBoxMethod = codeMemberMethod.Name;
				}
			}
		}

		// Token: 0x06001980 RID: 6528 RVA: 0x000904E8 File Offset: 0x0008E6E8
		private void WriteMethods(CodeTypeDeclaration cls)
		{
			MethodInfo[] methods = this.axctlType.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
			for (int i = 0; i < methods.Length; i++)
			{
				bool hasPropInfo;
				bool flag;
				if (!this.IsPropertySignature(methods[i], out hasPropInfo, out flag))
				{
					if (this.OptionalsPresent(methods[i]))
					{
						this.WriteMethod(cls, methods[i], hasPropInfo, true);
					}
					this.WriteMethod(cls, methods[i], hasPropInfo, false);
				}
			}
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x00090548 File Offset: 0x0008E748
		private void WriteProperty(CodeTypeDeclaration cls, PropertyInfo pinfo, bool useLet)
		{
			CodeAttributeDeclaration codeAttributeDeclaration = null;
			CustomAttributeData customAttributeData = null;
			if (AxWrapperGen.nopersist == null)
			{
				AxWrapperGen.nopersist = new CodeAttributeDeclaration("System.ComponentModel.DesignerSerializationVisibility", new CodeAttributeArgument[]
				{
					new CodeAttributeArgument(new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(null, "System.ComponentModel.DesignerSerializationVisibility"), "Hidden"))
				});
				AxWrapperGen.nobrowse = new CodeAttributeDeclaration("System.ComponentModel.Browsable", new CodeAttributeArgument[]
				{
					new CodeAttributeArgument(new CodePrimitiveExpression(false))
				});
				AxWrapperGen.browse = new CodeAttributeDeclaration("System.ComponentModel.Browsable", new CodeAttributeArgument[]
				{
					new CodeAttributeArgument(new CodePrimitiveExpression(true))
				});
				AxWrapperGen.bindable = new CodeAttributeDeclaration("System.ComponentModel.Bindable", new CodeAttributeArgument[]
				{
					new CodeAttributeArgument(new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(null, "System.ComponentModel.BindableSupport"), "Yes"))
				});
				AxWrapperGen.defaultBind = new CodeAttributeDeclaration("System.ComponentModel.Bindable", new CodeAttributeArgument[]
				{
					new CodeAttributeArgument(new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(null, "System.ComponentModel.BindableSupport"), "Default"))
				});
			}
			AxWrapperGen.ComAliasEnum comAliasEnum = AxWrapperGen.ComAliasConverter.GetComAliasEnum(pinfo, pinfo.PropertyType, pinfo);
			Type type = pinfo.PropertyType;
			if (comAliasEnum != AxWrapperGen.ComAliasEnum.None)
			{
				type = AxWrapperGen.ComAliasConverter.GetWFTypeFromComType(type, comAliasEnum);
			}
			bool flag = type.GUID.Equals(AxWrapperGen.Guid_DataSource);
			if (flag)
			{
				CodeMemberField codeMemberField = new CodeMemberField(type.FullName, this.CreateDataSourceFieldName(pinfo.Name));
				codeMemberField.Attributes = (MemberAttributes)20482;
				cls.Members.Add(codeMemberField);
				this.dataSourceProps.Add(pinfo);
			}
			CustomAttributeData[] attributeData = AxWrapperGen.GetAttributeData(pinfo, typeof(DispIdAttribute));
			if (attributeData != null && attributeData.Length != 0)
			{
				customAttributeData = attributeData[0];
				codeAttributeDeclaration = new CodeAttributeDeclaration(typeof(DispIdAttribute).FullName, new CodeAttributeArgument[]
				{
					new CodeAttributeArgument(new CodePrimitiveExpression(int.Parse(customAttributeData.ConstructorArguments[0].Value.ToString())))
				});
			}
			bool flag2 = false;
			bool flag3 = false;
			string str = this.ResolveConflict(pinfo.Name, type, out flag2, out flag3);
			if (flag2 || flag3)
			{
				if (customAttributeData == null)
				{
					return;
				}
				if (!this.IsDispidKnown(int.Parse(customAttributeData.ConstructorArguments[0].Value.ToString()), pinfo.Name))
				{
					str = "Ctl";
					flag2 = false;
					flag3 = false;
				}
			}
			CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
			codeMemberProperty.Type = new CodeTypeReference(AxWrapperGen.MapTypeName(type));
			codeMemberProperty.Name = str + pinfo.Name;
			codeMemberProperty.Attributes = MemberAttributes.Public;
			if (flag2)
			{
				codeMemberProperty.Attributes |= MemberAttributes.Override;
			}
			else if (flag3)
			{
				codeMemberProperty.Attributes |= MemberAttributes.New;
			}
			bool flag4 = false;
			bool flag5 = this.IsPropertyBrowsable(pinfo, comAliasEnum);
			bool flag6 = this.IsPropertyBindable(pinfo, out flag4);
			CodeAttributeDeclarationCollection codeAttributeDeclarationCollection;
			if (!flag5 || comAliasEnum == AxWrapperGen.ComAliasEnum.Handle)
			{
				codeAttributeDeclarationCollection = new CodeAttributeDeclarationCollection(new CodeAttributeDeclaration[]
				{
					AxWrapperGen.nobrowse,
					AxWrapperGen.nopersist,
					codeAttributeDeclaration
				});
			}
			else if (flag)
			{
				codeAttributeDeclarationCollection = new CodeAttributeDeclarationCollection(new CodeAttributeDeclaration[]
				{
					codeAttributeDeclaration
				});
			}
			else if (flag2 || flag3)
			{
				codeAttributeDeclarationCollection = new CodeAttributeDeclarationCollection(new CodeAttributeDeclaration[]
				{
					AxWrapperGen.browse,
					AxWrapperGen.nopersist,
					codeAttributeDeclaration
				});
			}
			else
			{
				codeAttributeDeclarationCollection = new CodeAttributeDeclarationCollection(new CodeAttributeDeclaration[]
				{
					AxWrapperGen.nopersist,
					codeAttributeDeclaration
				});
			}
			if (comAliasEnum != AxWrapperGen.ComAliasEnum.None)
			{
				CodeAttributeDeclaration value = new CodeAttributeDeclaration(typeof(ComAliasNameAttribute).FullName, new CodeAttributeArgument[]
				{
					new CodeAttributeArgument(new CodePrimitiveExpression(pinfo.PropertyType.FullName))
				});
				codeAttributeDeclarationCollection.Add(value);
			}
			if (flag4)
			{
				codeAttributeDeclarationCollection.Add(AxWrapperGen.defaultBind);
			}
			else if (flag6)
			{
				codeAttributeDeclarationCollection.Add(AxWrapperGen.bindable);
			}
			codeMemberProperty.CustomAttributes = codeAttributeDeclarationCollection;
			AxParameterData[] array = AxParameterData.Convert(pinfo.GetIndexParameters());
			if (array != null && array.Length != 0)
			{
				for (int i = 0; i < array.Length; i++)
				{
					CodeParameterDeclarationExpression codeParameterDeclarationExpression = this.CreateParamDecl(array[i].TypeName, array[i].Name, false);
					codeParameterDeclarationExpression.Direction = array[i].Direction;
					codeMemberProperty.Parameters.Add(codeParameterDeclarationExpression);
				}
			}
			bool fMethodSyntax = useLet;
			if (pinfo.CanWrite)
			{
				MethodInfo methodInfo;
				if (useLet)
				{
					methodInfo = pinfo.DeclaringType.GetMethod("let_" + pinfo.Name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
				}
				else
				{
					methodInfo = pinfo.GetSetMethod();
				}
				Type parameterType = methodInfo.GetParameters()[0].ParameterType;
				Type elementType = parameterType.GetElementType();
				if (elementType != null && parameterType != elementType)
				{
					fMethodSyntax = true;
				}
			}
			if (pinfo.CanRead)
			{
				this.WritePropertyGetter(codeMemberProperty, pinfo, comAliasEnum, array, fMethodSyntax, flag2, flag);
			}
			if (pinfo.CanWrite)
			{
				this.WritePropertySetter(codeMemberProperty, pinfo, comAliasEnum, array, fMethodSyntax, flag2, useLet, flag);
			}
			if (array.Length != 0 && codeMemberProperty.Name != "Item")
			{
				CodeAttributeDeclaration value2 = new CodeAttributeDeclaration("System.Runtime.CompilerServices.IndexerName", new CodeAttributeArgument[]
				{
					new CodeAttributeArgument(new CodePrimitiveExpression(codeMemberProperty.Name))
				});
				codeMemberProperty.Name = "Item";
				codeMemberProperty.CustomAttributes.Add(value2);
			}
			if (this.defMember != null && this.defMember.Equals(pinfo.Name))
			{
				CodeAttributeDeclaration value3 = new CodeAttributeDeclaration("System.ComponentModel.DefaultProperty", new CodeAttributeArgument[]
				{
					new CodeAttributeArgument(new CodePrimitiveExpression(codeMemberProperty.Name))
				});
				cls.CustomAttributes.Add(value3);
			}
			cls.Members.Add(codeMemberProperty);
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x00090AB4 File Offset: 0x0008ECB4
		private void WritePropertyGetter(CodeMemberProperty prop, PropertyInfo pinfo, AxWrapperGen.ComAliasEnum alias, AxParameterData[] parameters, bool fMethodSyntax, bool fOverride, bool dataSourceProp)
		{
			if (dataSourceProp)
			{
				string fieldName = this.CreateDataSourceFieldName(pinfo.Name);
				CodeMethodReturnStatement value = new CodeMethodReturnStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), fieldName));
				prop.GetStatements.Add(value);
				return;
			}
			if (fOverride)
			{
				CodeConditionStatement codeConditionStatement = this.CreateValidStateCheck();
				codeConditionStatement.TrueStatements.Add(this.GetPropertyGetRValue(pinfo, this.memIfaceRef, alias, parameters, fMethodSyntax));
				codeConditionStatement.FalseStatements.Add(this.GetPropertyGetRValue(pinfo, new CodeBaseReferenceExpression(), AxWrapperGen.ComAliasEnum.None, parameters, false));
				prop.GetStatements.Add(codeConditionStatement);
				return;
			}
			prop.GetStatements.Add(this.CreateInvalidStateException(prop.Name, "PropertyGet"));
			prop.GetStatements.Add(this.GetPropertyGetRValue(pinfo, this.memIfaceRef, alias, parameters, fMethodSyntax));
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x00090B80 File Offset: 0x0008ED80
		private void WritePropertySetter(CodeMemberProperty prop, PropertyInfo pinfo, AxWrapperGen.ComAliasEnum alias, AxParameterData[] parameters, bool fMethodSyntax, bool fOverride, bool useLet, bool dataSourceProp)
		{
			if (!fOverride && !dataSourceProp)
			{
				prop.SetStatements.Add(this.CreateInvalidStateException(prop.Name, "PropertySet"));
			}
			if (dataSourceProp)
			{
				string dataSourceName = this.CreateDataSourceFieldName(pinfo.Name);
				this.WriteDataSourcePropertySetter(prop, pinfo, dataSourceName);
				return;
			}
			if (!fMethodSyntax)
			{
				this.WritePropertySetterProp(prop, pinfo, alias, parameters, fOverride, useLet);
				return;
			}
			this.WritePropertySetterMethod(prop, pinfo, alias, parameters, fOverride, useLet);
		}

		// Token: 0x06001984 RID: 6532 RVA: 0x00090BF0 File Offset: 0x0008EDF0
		private void WriteDataSourcePropertySetter(CodeMemberProperty prop, PropertyInfo pinfo, string dataSourceName)
		{
			CodeExpression left = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), dataSourceName);
			CodeExpression right = new CodeFieldReferenceExpression(null, "value");
			CodeAssignStatement value = new CodeAssignStatement(left, right);
			prop.SetStatements.Add(value);
			CodeConditionStatement codeConditionStatement = this.CreateValidStateCheck();
			left = new CodeFieldReferenceExpression(this.memIfaceRef, pinfo.Name);
			codeConditionStatement.TrueStatements.Add(new CodeAssignStatement(left, right));
			prop.SetStatements.Add(codeConditionStatement);
		}

		// Token: 0x06001985 RID: 6533 RVA: 0x00090C64 File Offset: 0x0008EE64
		private void WritePropertySetterMethod(CodeMemberProperty prop, PropertyInfo pinfo, AxWrapperGen.ComAliasEnum alias, AxParameterData[] parameters, bool fOverride, bool useLet)
		{
			CodeExpression codeExpression = null;
			CodeConditionStatement codeConditionStatement = null;
			if (fOverride)
			{
				if (parameters.Length != 0)
				{
					codeExpression = new CodeIndexerExpression(this.memIfaceRef, new CodeExpression[0]);
				}
				else
				{
					codeExpression = new CodePropertyReferenceExpression(new CodeBaseReferenceExpression(), pinfo.Name);
				}
				CodeBinaryOperatorExpression condition = new CodeBinaryOperatorExpression(this.memIfaceRef, CodeBinaryOperatorType.IdentityInequality, new CodePrimitiveExpression(null));
				codeConditionStatement = new CodeConditionStatement();
				codeConditionStatement.Condition = condition;
			}
			string methodName = useLet ? ("let_" + pinfo.Name) : pinfo.GetSetMethod().Name;
			CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(this.memIfaceRef, methodName, new CodeExpression[0]);
			for (int i = 0; i < parameters.Length; i++)
			{
				if (fOverride)
				{
					((CodeIndexerExpression)codeExpression).Indices.Add(new CodeFieldReferenceExpression(null, parameters[i].Name));
				}
				codeMethodInvokeExpression.Parameters.Add(new CodeFieldReferenceExpression(null, parameters[i].Name));
			}
			CodeFieldReferenceExpression codeFieldReferenceExpression = new CodeFieldReferenceExpression(null, "value");
			CodeExpression propertySetRValue = this.GetPropertySetRValue(alias, pinfo.PropertyType);
			CodeFieldReferenceExpression expression;
			if (alias != AxWrapperGen.ComAliasEnum.None)
			{
				string wftoComParamConverter = AxWrapperGen.ComAliasConverter.GetWFToComParamConverter(alias, pinfo.PropertyType);
				CodeParameterDeclarationExpression left;
				if (wftoComParamConverter.Length == 0)
				{
					left = this.CreateParamDecl(AxWrapperGen.MapTypeName(pinfo.PropertyType), "paramTemp", false);
				}
				else
				{
					left = this.CreateParamDecl(wftoComParamConverter, "paramTemp", false);
				}
				prop.SetStatements.Add(new CodeAssignStatement(left, propertySetRValue));
				expression = new CodeFieldReferenceExpression(null, "paramTemp");
			}
			else
			{
				expression = codeFieldReferenceExpression;
			}
			codeMethodInvokeExpression.Parameters.Add(new CodeDirectionExpression(useLet ? FieldDirection.In : FieldDirection.Ref, expression));
			if (fOverride)
			{
				prop.SetStatements.Add(new CodeAssignStatement(codeExpression, codeFieldReferenceExpression));
				codeConditionStatement.TrueStatements.Add(new CodeExpressionStatement(codeMethodInvokeExpression));
				prop.SetStatements.Add(codeConditionStatement);
				return;
			}
			prop.SetStatements.Add(new CodeExpressionStatement(codeMethodInvokeExpression));
		}

		// Token: 0x06001986 RID: 6534 RVA: 0x00090E40 File Offset: 0x0008F040
		private void WritePropertySetterProp(CodeMemberProperty prop, PropertyInfo pinfo, AxWrapperGen.ComAliasEnum alias, AxParameterData[] parameters, bool fOverride, bool useLet)
		{
			CodeExpression codeExpression = null;
			CodeConditionStatement codeConditionStatement = null;
			if (fOverride)
			{
				if (parameters.Length != 0)
				{
					codeExpression = new CodeIndexerExpression(this.memIfaceRef, new CodeExpression[0]);
				}
				else
				{
					codeExpression = new CodePropertyReferenceExpression(new CodeBaseReferenceExpression(), pinfo.Name);
				}
				CodeBinaryOperatorExpression condition = new CodeBinaryOperatorExpression(this.memIfaceRef, CodeBinaryOperatorType.IdentityInequality, new CodePrimitiveExpression(null));
				codeConditionStatement = new CodeConditionStatement();
				codeConditionStatement.Condition = condition;
			}
			CodeExpression codeExpression2;
			if (parameters.Length != 0)
			{
				codeExpression2 = new CodeIndexerExpression(this.memIfaceRef, new CodeExpression[0]);
			}
			else
			{
				codeExpression2 = new CodePropertyReferenceExpression(this.memIfaceRef, pinfo.Name);
			}
			for (int i = 0; i < parameters.Length; i++)
			{
				if (fOverride)
				{
					((CodeIndexerExpression)codeExpression).Indices.Add(new CodeFieldReferenceExpression(null, parameters[i].Name));
				}
				((CodeIndexerExpression)codeExpression2).Indices.Add(new CodeFieldReferenceExpression(null, parameters[i].Name));
			}
			CodeFieldReferenceExpression right = new CodeFieldReferenceExpression(null, "value");
			CodeExpression propertySetRValue = this.GetPropertySetRValue(alias, pinfo.PropertyType);
			if (fOverride)
			{
				prop.SetStatements.Add(new CodeAssignStatement(codeExpression, right));
				codeConditionStatement.TrueStatements.Add(new CodeAssignStatement(codeExpression2, propertySetRValue));
				prop.SetStatements.Add(codeConditionStatement);
				return;
			}
			prop.SetStatements.Add(new CodeAssignStatement(codeExpression2, propertySetRValue));
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x00090F90 File Offset: 0x0008F190
		private CodeMethodReturnStatement GetPropertyGetRValue(PropertyInfo pinfo, CodeExpression reference, AxWrapperGen.ComAliasEnum alias, AxParameterData[] parameters, bool fMethodSyntax)
		{
			CodeExpression codeExpression;
			if (fMethodSyntax)
			{
				codeExpression = new CodeMethodInvokeExpression(reference, pinfo.GetGetMethod().Name, new CodeExpression[0]);
				for (int i = 0; i < parameters.Length; i++)
				{
					((CodeMethodInvokeExpression)codeExpression).Parameters.Add(new CodeFieldReferenceExpression(null, parameters[i].Name));
				}
			}
			else if (parameters.Length != 0)
			{
				codeExpression = new CodeIndexerExpression(reference, new CodeExpression[0]);
				for (int j = 0; j < parameters.Length; j++)
				{
					((CodeIndexerExpression)codeExpression).Indices.Add(new CodeFieldReferenceExpression(null, parameters[j].Name));
				}
			}
			else
			{
				codeExpression = new CodePropertyReferenceExpression(reference, (parameters.Length == 0) ? pinfo.Name : "");
			}
			if (alias != AxWrapperGen.ComAliasEnum.None)
			{
				string comToManagedConverter = AxWrapperGen.ComAliasConverter.GetComToManagedConverter(alias);
				string comToWFParamConverter = AxWrapperGen.ComAliasConverter.GetComToWFParamConverter(alias);
				CodeExpression[] parameters2;
				if (comToWFParamConverter.Length == 0)
				{
					parameters2 = new CodeExpression[]
					{
						codeExpression
					};
				}
				else
				{
					CodeCastExpression codeCastExpression = new CodeCastExpression(comToWFParamConverter, codeExpression);
					parameters2 = new CodeExpression[]
					{
						codeCastExpression
					};
				}
				CodeMethodInvokeExpression expression = new CodeMethodInvokeExpression(null, comToManagedConverter, parameters2);
				return new CodeMethodReturnStatement(expression);
			}
			return new CodeMethodReturnStatement(codeExpression);
		}

		// Token: 0x06001988 RID: 6536 RVA: 0x000910A4 File Offset: 0x0008F2A4
		private CodeExpression GetPropertySetRValue(AxWrapperGen.ComAliasEnum alias, Type propertyType)
		{
			CodeExpression codeExpression = new CodePropertySetValueReferenceExpression();
			if (alias == AxWrapperGen.ComAliasEnum.None)
			{
				return codeExpression;
			}
			string wftoComConverter = AxWrapperGen.ComAliasConverter.GetWFToComConverter(alias);
			string wftoComParamConverter = AxWrapperGen.ComAliasConverter.GetWFToComParamConverter(alias, propertyType);
			CodeExpression[] parameters = new CodeExpression[]
			{
				codeExpression
			};
			CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(null, wftoComConverter, parameters);
			if (wftoComParamConverter.Length == 0)
			{
				return codeMethodInvokeExpression;
			}
			return new CodeCastExpression(wftoComParamConverter, codeMethodInvokeExpression);
		}

		// Token: 0x06001989 RID: 6537 RVA: 0x000910F4 File Offset: 0x0008F2F4
		private void WriteProperties(CodeTypeDeclaration cls)
		{
			PropertyInfo[] properties = this.axctlType.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
			for (int i = 0; i < properties.Length; i++)
			{
				bool useLet;
				if (this.IsPropertySignature(properties[i], out useLet))
				{
					this.WriteProperty(cls, properties[i], useLet);
				}
			}
		}

		// Token: 0x0400157B RID: 5499
		private string axctlIface;

		// Token: 0x0400157C RID: 5500
		private Type axctlType;

		// Token: 0x0400157D RID: 5501
		private Guid clsidAx;

		// Token: 0x0400157E RID: 5502
		private string axctlEvents;

		// Token: 0x0400157F RID: 5503
		private Type axctlEventsType;

		// Token: 0x04001580 RID: 5504
		private string axctl;

		// Token: 0x04001581 RID: 5505
		private static string axctlNS;

		// Token: 0x04001582 RID: 5506
		private string memIface;

		// Token: 0x04001583 RID: 5507
		private string multicaster;

		// Token: 0x04001584 RID: 5508
		private string cookie;

		// Token: 0x04001585 RID: 5509
		private bool dispInterface;

		// Token: 0x04001586 RID: 5510
		private bool enumerableInterface;

		// Token: 0x04001587 RID: 5511
		private string defMember;

		// Token: 0x04001588 RID: 5512
		private string aboutBoxMethod;

		// Token: 0x04001589 RID: 5513
		private CodeFieldReferenceExpression memIfaceRef;

		// Token: 0x0400158A RID: 5514
		private CodeFieldReferenceExpression multicasterRef;

		// Token: 0x0400158B RID: 5515
		private CodeFieldReferenceExpression cookieRef;

		// Token: 0x0400158C RID: 5516
		private ArrayList events;

		// Token: 0x0400158D RID: 5517
		public static ArrayList GeneratedSources = new ArrayList();

		// Token: 0x0400158E RID: 5518
		private static Guid Guid_DataSource = new Guid("{7C0FFAB3-CD84-11D0-949A-00A0C91110ED}");

		// Token: 0x0400158F RID: 5519
		internal static BooleanSwitch AxWrapper = new BooleanSwitch("AxWrapper", "ActiveX WFW wrapper generation.");

		// Token: 0x04001590 RID: 5520
		internal static BooleanSwitch AxCodeGen = new BooleanSwitch("AxCodeGen", "ActiveX WFW property generation.");

		// Token: 0x04001591 RID: 5521
		private static CodeAttributeDeclaration nobrowse = null;

		// Token: 0x04001592 RID: 5522
		private static CodeAttributeDeclaration browse = null;

		// Token: 0x04001593 RID: 5523
		private static CodeAttributeDeclaration nopersist = null;

		// Token: 0x04001594 RID: 5524
		private static CodeAttributeDeclaration bindable = null;

		// Token: 0x04001595 RID: 5525
		private static CodeAttributeDeclaration defaultBind = null;

		// Token: 0x04001596 RID: 5526
		private Hashtable axctlTypeMembers;

		// Token: 0x04001597 RID: 5527
		private Hashtable axHostMembers;

		// Token: 0x04001598 RID: 5528
		private Hashtable conflictableThings;

		// Token: 0x04001599 RID: 5529
		private static Hashtable classesInNamespace;

		// Token: 0x0400159A RID: 5530
		private static Hashtable axHostPropDescs;

		// Token: 0x0400159B RID: 5531
		private ArrayList dataSourceProps = new ArrayList();

		// Token: 0x02000526 RID: 1318
		private enum ComAliasEnum
		{
			// Token: 0x040020B4 RID: 8372
			None,
			// Token: 0x040020B5 RID: 8373
			Color,
			// Token: 0x040020B6 RID: 8374
			Font,
			// Token: 0x040020B7 RID: 8375
			FontDisp,
			// Token: 0x040020B8 RID: 8376
			Handle,
			// Token: 0x040020B9 RID: 8377
			Picture,
			// Token: 0x040020BA RID: 8378
			PictureDisp
		}

		// Token: 0x02000527 RID: 1319
		private static class ComAliasConverter
		{
			// Token: 0x06003024 RID: 12324 RVA: 0x001082D8 File Offset: 0x001064D8
			public static string GetComToManagedConverter(AxWrapperGen.ComAliasEnum alias)
			{
				if (alias == AxWrapperGen.ComAliasEnum.Color)
				{
					return "GetColorFromOleColor";
				}
				if (AxWrapperGen.ComAliasConverter.IsFont(alias))
				{
					return "GetFontFromIFont";
				}
				if (AxWrapperGen.ComAliasConverter.IsPicture(alias))
				{
					return "GetPictureFromIPicture";
				}
				return "";
			}

			// Token: 0x06003025 RID: 12325 RVA: 0x00108305 File Offset: 0x00106505
			public static string GetComToWFParamConverter(AxWrapperGen.ComAliasEnum alias)
			{
				if (alias == AxWrapperGen.ComAliasEnum.Color)
				{
					return typeof(uint).FullName;
				}
				return "";
			}

			// Token: 0x06003026 RID: 12326 RVA: 0x00108320 File Offset: 0x00106520
			private static Guid GetGuid(Type t)
			{
				Guid guid = Guid.Empty;
				if (AxWrapperGen.ComAliasConverter.typeGuids == null)
				{
					AxWrapperGen.ComAliasConverter.typeGuids = new Hashtable();
				}
				else if (AxWrapperGen.ComAliasConverter.typeGuids.Contains(t))
				{
					return (Guid)AxWrapperGen.ComAliasConverter.typeGuids[t];
				}
				guid = t.GUID;
				AxWrapperGen.ComAliasConverter.typeGuids.Add(t, guid);
				return guid;
			}

			// Token: 0x06003027 RID: 12327 RVA: 0x00108380 File Offset: 0x00106580
			public static Type GetWFTypeFromComType(Type t, AxWrapperGen.ComAliasEnum alias)
			{
				if (!AxWrapperGen.ComAliasConverter.IsValidType(alias, t))
				{
					return t;
				}
				if (alias == AxWrapperGen.ComAliasEnum.Color)
				{
					return typeof(Color);
				}
				if (AxWrapperGen.ComAliasConverter.IsFont(alias))
				{
					return typeof(Font);
				}
				if (AxWrapperGen.ComAliasConverter.IsPicture(alias))
				{
					return typeof(Image);
				}
				return t;
			}

			// Token: 0x06003028 RID: 12328 RVA: 0x001083CE File Offset: 0x001065CE
			public static string GetWFToComConverter(AxWrapperGen.ComAliasEnum alias)
			{
				if (alias == AxWrapperGen.ComAliasEnum.Color)
				{
					return "GetOleColorFromColor";
				}
				if (AxWrapperGen.ComAliasConverter.IsFont(alias))
				{
					return "GetIFontFromFont";
				}
				if (AxWrapperGen.ComAliasConverter.IsPicture(alias))
				{
					return "GetIPictureFromPicture";
				}
				return "";
			}

			// Token: 0x06003029 RID: 12329 RVA: 0x001083FB File Offset: 0x001065FB
			public static string GetWFToComParamConverter(AxWrapperGen.ComAliasEnum alias, Type t)
			{
				return t.FullName;
			}

			// Token: 0x0600302A RID: 12330 RVA: 0x00108404 File Offset: 0x00106604
			public static AxWrapperGen.ComAliasEnum GetComAliasEnum(MemberInfo memberInfo, Type type, ICustomAttributeProvider attrProvider)
			{
				string text = null;
				int num = -1;
				CustomAttributeData[] array = new CustomAttributeData[0];
				if (attrProvider != null)
				{
					array = AxWrapperGen.GetAttributeData(attrProvider, typeof(ComAliasNameAttribute));
				}
				if (array != null && array.Length != 0)
				{
					CustomAttributeData customAttributeData = array[0];
					text = customAttributeData.ConstructorArguments[0].Value.ToString();
				}
				if (text != null && text.Length != 0)
				{
					if (text.EndsWith(".OLE_COLOR") && AxWrapperGen.ComAliasConverter.IsValidType(AxWrapperGen.ComAliasEnum.Color, type))
					{
						return AxWrapperGen.ComAliasEnum.Color;
					}
					if (text.EndsWith(".OLE_HANDLE") && AxWrapperGen.ComAliasConverter.IsValidType(AxWrapperGen.ComAliasEnum.Handle, type))
					{
						return AxWrapperGen.ComAliasEnum.Handle;
					}
				}
				if (memberInfo is PropertyInfo && string.Equals(memberInfo.Name, "hWnd", StringComparison.OrdinalIgnoreCase) && AxWrapperGen.ComAliasConverter.IsValidType(AxWrapperGen.ComAliasEnum.Handle, type))
				{
					return AxWrapperGen.ComAliasEnum.Handle;
				}
				if (attrProvider != null)
				{
					array = AxWrapperGen.GetAttributeData(attrProvider, typeof(DispIdAttribute));
					if (array != null && array.Length != 0)
					{
						CustomAttributeData customAttributeData2 = array[0];
						num = int.Parse(customAttributeData2.ConstructorArguments[0].Value.ToString());
					}
				}
				if ((num == -501 || num == -513 || num == -510 || num == -503) && AxWrapperGen.ComAliasConverter.IsValidType(AxWrapperGen.ComAliasEnum.Color, type))
				{
					return AxWrapperGen.ComAliasEnum.Color;
				}
				if (num == -512 && AxWrapperGen.ComAliasConverter.IsValidType(AxWrapperGen.ComAliasEnum.Font, type))
				{
					return AxWrapperGen.ComAliasEnum.Font;
				}
				if (num == -523 && AxWrapperGen.ComAliasConverter.IsValidType(AxWrapperGen.ComAliasEnum.Picture, type))
				{
					return AxWrapperGen.ComAliasEnum.Picture;
				}
				if (num == -515 && AxWrapperGen.ComAliasConverter.IsValidType(AxWrapperGen.ComAliasEnum.Handle, type))
				{
					return AxWrapperGen.ComAliasEnum.Handle;
				}
				if (AxWrapperGen.ComAliasConverter.IsValidType(AxWrapperGen.ComAliasEnum.Font, type))
				{
					return AxWrapperGen.ComAliasEnum.Font;
				}
				if (AxWrapperGen.ComAliasConverter.IsValidType(AxWrapperGen.ComAliasEnum.FontDisp, type))
				{
					return AxWrapperGen.ComAliasEnum.FontDisp;
				}
				if (AxWrapperGen.ComAliasConverter.IsValidType(AxWrapperGen.ComAliasEnum.Picture, type))
				{
					return AxWrapperGen.ComAliasEnum.Picture;
				}
				if (AxWrapperGen.ComAliasConverter.IsValidType(AxWrapperGen.ComAliasEnum.PictureDisp, type))
				{
					return AxWrapperGen.ComAliasEnum.PictureDisp;
				}
				return AxWrapperGen.ComAliasEnum.None;
			}

			// Token: 0x0600302B RID: 12331 RVA: 0x00108589 File Offset: 0x00106789
			public static bool IsFont(AxWrapperGen.ComAliasEnum e)
			{
				return e == AxWrapperGen.ComAliasEnum.Font || e == AxWrapperGen.ComAliasEnum.FontDisp;
			}

			// Token: 0x0600302C RID: 12332 RVA: 0x00108595 File Offset: 0x00106795
			public static bool IsPicture(AxWrapperGen.ComAliasEnum e)
			{
				return e == AxWrapperGen.ComAliasEnum.Picture || e == AxWrapperGen.ComAliasEnum.PictureDisp;
			}

			// Token: 0x0600302D RID: 12333 RVA: 0x001085A4 File Offset: 0x001067A4
			private static bool IsValidType(AxWrapperGen.ComAliasEnum e, Type t)
			{
				switch (e)
				{
				case AxWrapperGen.ComAliasEnum.Color:
					return t == typeof(ushort) || t == typeof(uint) || t == typeof(int) || t == typeof(short);
				case AxWrapperGen.ComAliasEnum.Font:
					return AxWrapperGen.ComAliasConverter.GetGuid(t).Equals(AxWrapperGen.ComAliasConverter.Guid_IFont);
				case AxWrapperGen.ComAliasEnum.FontDisp:
					return AxWrapperGen.ComAliasConverter.GetGuid(t).Equals(AxWrapperGen.ComAliasConverter.Guid_IFontDisp);
				case AxWrapperGen.ComAliasEnum.Handle:
					return t == typeof(uint) || t == typeof(int) || t == typeof(IntPtr) || t == typeof(UIntPtr);
				case AxWrapperGen.ComAliasEnum.Picture:
					return AxWrapperGen.ComAliasConverter.GetGuid(t).Equals(AxWrapperGen.ComAliasConverter.Guid_IPicture);
				case AxWrapperGen.ComAliasEnum.PictureDisp:
					return AxWrapperGen.ComAliasConverter.GetGuid(t).Equals(AxWrapperGen.ComAliasConverter.Guid_IPictureDisp);
				default:
					return false;
				}
			}

			// Token: 0x040020BB RID: 8379
			private static Guid Guid_IPicture = new Guid("{7BF80980-BF32-101A-8BBB-00AA00300CAB}");

			// Token: 0x040020BC RID: 8380
			private static Guid Guid_IPictureDisp = new Guid("{7BF80981-BF32-101A-8BBB-00AA00300CAB}");

			// Token: 0x040020BD RID: 8381
			private static Guid Guid_IFont = new Guid("{BEF6E002-A874-101A-8BBA-00AA00300CAB}");

			// Token: 0x040020BE RID: 8382
			private static Guid Guid_IFontDisp = new Guid("{BEF6E003-A874-101A-8BBA-00AA00300CAB}");

			// Token: 0x040020BF RID: 8383
			private static Hashtable typeGuids;
		}

		// Token: 0x02000528 RID: 1320
		private class EventEntry
		{
			// Token: 0x0600302F RID: 12335 RVA: 0x001086F8 File Offset: 0x001068F8
			public EventEntry(string eventName, string eventCls, string eventHandlerCls, Type retType, AxParameterData[] parameters, bool conflict)
			{
				this.eventName = eventName;
				this.eventCls = eventCls;
				this.eventHandlerCls = eventHandlerCls;
				this.retType = retType;
				this.parameters = parameters;
				this.eventParam = eventName.ToLower(CultureInfo.InvariantCulture) + "Event";
				this.resovledEventName = (conflict ? (eventName + "Event") : eventName);
				this.invokeMethodName = "RaiseOn" + this.resovledEventName;
				this.eventFlags = (MemberAttributes)24578;
			}

			// Token: 0x040020C0 RID: 8384
			public string eventName;

			// Token: 0x040020C1 RID: 8385
			public string resovledEventName;

			// Token: 0x040020C2 RID: 8386
			public string eventCls;

			// Token: 0x040020C3 RID: 8387
			public string eventHandlerCls;

			// Token: 0x040020C4 RID: 8388
			public Type retType;

			// Token: 0x040020C5 RID: 8389
			public AxParameterData[] parameters;

			// Token: 0x040020C6 RID: 8390
			public string eventParam;

			// Token: 0x040020C7 RID: 8391
			public string invokeMethodName;

			// Token: 0x040020C8 RID: 8392
			public MemberAttributes eventFlags;
		}

		// Token: 0x02000529 RID: 1321
		private class AxMethodGenerator
		{
			// Token: 0x06003030 RID: 12336 RVA: 0x00108784 File Offset: 0x00106984
			internal AxMethodGenerator(MethodInfo method, bool removeOpts)
			{
				this._method = method;
				this._removeOptionals = removeOpts;
			}

			// Token: 0x1700095C RID: 2396
			// (get) Token: 0x06003031 RID: 12337 RVA: 0x0010879A File Offset: 0x0010699A
			// (set) Token: 0x06003032 RID: 12338 RVA: 0x001087A2 File Offset: 0x001069A2
			public Type ControlType
			{
				get
				{
					return this._controlType;
				}
				set
				{
					this._controlType = value;
				}
			}

			// Token: 0x1700095D RID: 2397
			// (get) Token: 0x06003033 RID: 12339 RVA: 0x001087AC File Offset: 0x001069AC
			private AxParameterData[] Parameters
			{
				get
				{
					if (this._params == null && this._method != null)
					{
						this._params = AxParameterData.Convert(this._method.GetParameters());
						if (this._params == null)
						{
							this._params = new AxParameterData[0];
						}
					}
					return this._params;
				}
			}

			// Token: 0x06003034 RID: 12340 RVA: 0x00108800 File Offset: 0x00106A00
			public static AxWrapperGen.AxMethodGenerator Create(MethodInfo method, bool removeOptionals)
			{
				bool flag = removeOptionals && AxWrapperGen.AxMethodGenerator.NonPrimitiveOptionalsOrMissingPresent(method);
				if (flag)
				{
					return new AxWrapperGen.AxReflectionInvokeMethodGenerator(method, removeOptionals);
				}
				return new AxWrapperGen.AxMethodGenerator(method, removeOptionals);
			}

			// Token: 0x06003035 RID: 12341 RVA: 0x0010882C File Offset: 0x00106A2C
			public CodeMemberMethod CreateMethod(string methodName)
			{
				return new CodeMemberMethod
				{
					Name = methodName,
					Attributes = MemberAttributes.Public,
					ReturnType = new CodeTypeReference(AxWrapperGen.MapTypeName(this._method.ReturnType))
				};
			}

			// Token: 0x06003036 RID: 12342 RVA: 0x00108870 File Offset: 0x00106A70
			public List<CodeExpression> GenerateAndMarshalParameters(CodeMemberMethod method)
			{
				List<CodeExpression> list = new List<CodeExpression>();
				foreach (AxParameterData axParameterData in this.Parameters)
				{
					if (axParameterData.IsOptional && this._removeOptionals)
					{
						CodeExpression defaultExpressionForInvoke = AxWrapperGen.AxMethodGenerator.GetDefaultExpressionForInvoke(this._method, axParameterData);
						list.Add(defaultExpressionForInvoke);
					}
					else
					{
						Type parameterBaseType = axParameterData.ParameterBaseType;
						AxWrapperGen.ComAliasEnum comAliasEnum = AxWrapperGen.ComAliasConverter.GetComAliasEnum(this._method, parameterBaseType, null);
						CodeVariableReferenceExpression codeVariableReferenceExpression = new CodeVariableReferenceExpression(axParameterData.Name);
						codeVariableReferenceExpression.UserData[typeof(AxParameterData)] = axParameterData;
						if (comAliasEnum != AxWrapperGen.ComAliasEnum.None)
						{
							Type wftypeFromComType = AxWrapperGen.ComAliasConverter.GetWFTypeFromComType(parameterBaseType, comAliasEnum);
							CodeParameterDeclarationExpression codeParameterDeclarationExpression = new CodeParameterDeclarationExpression(wftypeFromComType.FullName, axParameterData.Name);
							codeParameterDeclarationExpression.Direction = axParameterData.Direction;
							method.Parameters.Add(codeParameterDeclarationExpression);
							string wftoComConverter = AxWrapperGen.ComAliasConverter.GetWFToComConverter(comAliasEnum);
							CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(null, wftoComConverter, new CodeExpression[0]);
							codeMethodInvokeExpression.Parameters.Add(new CodeVariableReferenceExpression(axParameterData.Name));
							codeVariableReferenceExpression.UserData[AxWrapperGen.AxMethodGenerator.OriginalParamNameKey] = axParameterData.Name;
							codeVariableReferenceExpression.VariableName = "_" + axParameterData.Name;
							CodeVariableDeclarationStatement value = new CodeVariableDeclarationStatement(parameterBaseType.FullName, codeVariableReferenceExpression.VariableName, new CodeCastExpression(parameterBaseType, codeMethodInvokeExpression));
							method.Statements.Add(value);
						}
						else
						{
							CodeParameterDeclarationExpression codeParameterDeclarationExpression2 = new CodeParameterDeclarationExpression(axParameterData.TypeName, axParameterData.Name);
							codeParameterDeclarationExpression2.Direction = axParameterData.Direction;
							method.Parameters.Add(codeParameterDeclarationExpression2);
						}
						list.Add(codeVariableReferenceExpression);
					}
				}
				return list;
			}

			// Token: 0x06003037 RID: 12343 RVA: 0x00108A0E File Offset: 0x00106C0E
			public CodeExpression DoMethodInvoke(CodeMemberMethod method, string methodName, CodeExpression targetObject, List<CodeExpression> parameters)
			{
				return this.DoMethodInvokeCore(method, methodName, this._method.ReturnType, targetObject, parameters);
			}

			// Token: 0x06003038 RID: 12344 RVA: 0x00108A28 File Offset: 0x00106C28
			public virtual CodeExpression DoMethodInvokeCore(CodeMemberMethod method, string methodName, Type returnType, CodeExpression targetObject, List<CodeExpression> parameters)
			{
				CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(targetObject, methodName, new CodeExpression[0]);
				foreach (CodeExpression codeExpression in parameters)
				{
					AxParameterData axParameterData = (AxParameterData)codeExpression.UserData[typeof(AxParameterData)];
					CodeExpression value = codeExpression;
					if (axParameterData != null)
					{
						value = new CodeDirectionExpression(axParameterData.Direction, codeExpression);
					}
					codeMethodInvokeExpression.Parameters.Add(value);
				}
				if (returnType == typeof(void))
				{
					method.Statements.Add(new CodeExpressionStatement(codeMethodInvokeExpression));
					return null;
				}
				CodeVariableDeclarationStatement value2 = new CodeVariableDeclarationStatement(returnType, AxWrapperGen.AxMethodGenerator.ReturnValueVariableName, new CodeCastExpression(returnType, codeMethodInvokeExpression));
				method.Statements.Add(value2);
				return new CodeVariableReferenceExpression(AxWrapperGen.AxMethodGenerator.ReturnValueVariableName);
			}

			// Token: 0x06003039 RID: 12345 RVA: 0x00108B10 File Offset: 0x00106D10
			public void UnmarshalParameters(CodeMemberMethod method, List<CodeExpression> parameters)
			{
				foreach (CodeExpression codeExpression in parameters)
				{
					if (codeExpression is CodeVariableReferenceExpression)
					{
						AxParameterData axParameterData = (AxParameterData)codeExpression.UserData[typeof(AxParameterData)];
						string text = (string)codeExpression.UserData[AxWrapperGen.AxMethodGenerator.OriginalParamNameKey];
						if (axParameterData.Direction != FieldDirection.In && text != null)
						{
							CodeExpression left = new CodeVariableReferenceExpression(text);
							CodeExpression codeExpression2 = new CodeCastExpression(axParameterData.ParameterBaseType, codeExpression);
							AxWrapperGen.ComAliasEnum comAliasEnum = AxWrapperGen.ComAliasConverter.GetComAliasEnum(this._method, axParameterData.ParameterBaseType, null);
							if (comAliasEnum != AxWrapperGen.ComAliasEnum.None)
							{
								string comToManagedConverter = AxWrapperGen.ComAliasConverter.GetComToManagedConverter(comAliasEnum);
								codeExpression2 = new CodeMethodInvokeExpression(null, comToManagedConverter, new CodeExpression[0])
								{
									Parameters = 
									{
										codeExpression2
									}
								};
							}
							CodeAssignStatement value = new CodeAssignStatement(left, codeExpression2);
							method.Statements.Add(value);
						}
					}
				}
			}

			// Token: 0x0600303A RID: 12346 RVA: 0x00108C18 File Offset: 0x00106E18
			public void GenerateReturn(CodeMemberMethod method, CodeExpression returnExpression)
			{
				if (returnExpression == null)
				{
					return;
				}
				AxWrapperGen.ComAliasEnum comAliasEnum = AxWrapperGen.ComAliasConverter.GetComAliasEnum(this._method, this._method.ReturnType, this._method.ReturnTypeCustomAttributes);
				if (comAliasEnum != AxWrapperGen.ComAliasEnum.None)
				{
					string comToManagedConverter = AxWrapperGen.ComAliasConverter.GetComToManagedConverter(comAliasEnum);
					returnExpression = new CodeMethodInvokeExpression(null, comToManagedConverter, new CodeExpression[0])
					{
						Parameters = 
						{
							returnExpression
						}
					};
					method.ReturnType = new CodeTypeReference(AxWrapperGen.ComAliasConverter.GetWFTypeFromComType(this._method.ReturnType, comAliasEnum));
				}
				method.Statements.Add(new CodeMethodReturnStatement(returnExpression));
			}

			// Token: 0x0600303B RID: 12347 RVA: 0x00108CA4 File Offset: 0x00106EA4
			private static bool NonPrimitiveOptionalsOrMissingPresent(MethodInfo method)
			{
				ParameterInfo[] parameters = method.GetParameters();
				if (parameters != null && parameters.Length != 0)
				{
					for (int i = 0; i < parameters.Length; i++)
					{
						if (parameters[i].IsOptional && ((!parameters[i].ParameterType.IsPrimitive && !parameters[i].ParameterType.IsEnum) || parameters[i].RawDefaultValue == Missing.Value))
						{
							return true;
						}
					}
				}
				return false;
			}

			// Token: 0x0600303C RID: 12348 RVA: 0x00108D08 File Offset: 0x00106F08
			private static object GetClsPrimitiveValue(object value)
			{
				if (value is uint)
				{
					return Convert.ChangeType(value, typeof(int), CultureInfo.InvariantCulture);
				}
				if (value is ushort)
				{
					return Convert.ChangeType(value, typeof(short), CultureInfo.InvariantCulture);
				}
				if (value is ulong)
				{
					return Convert.ChangeType(value, typeof(long), CultureInfo.InvariantCulture);
				}
				if (value is sbyte)
				{
					return Convert.ChangeType(value, typeof(byte), CultureInfo.InvariantCulture);
				}
				return value;
			}

			// Token: 0x0600303D RID: 12349 RVA: 0x00108D90 File Offset: 0x00106F90
			private static object GetDefaultValueForUnsignedType(Type parameterType, object value)
			{
				if (parameterType == typeof(uint))
				{
					int value2 = 0;
					if (value is short)
					{
						value2 = (int)((short)value);
					}
					if (value is int)
					{
						value2 = (int)value;
					}
					if (value is long)
					{
						value2 = (int)value;
					}
					return Convert.ToUInt32(Convert.ToString(value2, 16), 16);
				}
				if (parameterType == typeof(ushort))
				{
					short value3 = (short)value;
					return Convert.ToUInt16(Convert.ToString(value3, 16), 16);
				}
				if (parameterType == typeof(ulong))
				{
					long value4 = 0L;
					if (value is short)
					{
						value4 = (long)((short)value);
					}
					if (value is int)
					{
						value4 = (long)((int)value);
					}
					if (value is long)
					{
						value4 = (long)value;
					}
					return Convert.ToUInt64(Convert.ToString(value4, 16), 16);
				}
				return value;
			}

			// Token: 0x0600303E RID: 12350 RVA: 0x00108E7C File Offset: 0x0010707C
			private static object GetPrimitiveDefaultValue(Type destType)
			{
				if (destType == typeof(IntPtr) || destType == typeof(UIntPtr))
				{
					return 0;
				}
				return AxWrapperGen.AxMethodGenerator.GetClsPrimitiveValue(Convert.ChangeType(0, destType, CultureInfo.InvariantCulture));
			}

			// Token: 0x0600303F RID: 12351 RVA: 0x00108ECC File Offset: 0x001070CC
			private static CodeExpression GetDefaultExpressionForInvoke(MethodInfo method, AxParameterData parameterInfo)
			{
				object obj = parameterInfo.ParameterInfo.RawDefaultValue;
				Type type = parameterInfo.ParameterBaseType;
				if (obj == Missing.Value)
				{
					if (type.IsPrimitive)
					{
						obj = AxWrapperGen.AxMethodGenerator.GetPrimitiveDefaultValue(type);
					}
					else if (type.IsEnum)
					{
						obj = 0;
						FieldInfo[] fields = type.GetFields(BindingFlags.Static | BindingFlags.Public);
						if (fields.Length != 0 && !Enum.IsDefined(type, 0))
						{
							obj = fields[0].GetRawConstantValue();
						}
					}
					else
					{
						if (type == typeof(object))
						{
							return new CodeFieldReferenceExpression(new CodeFieldReferenceExpression(null, "System.Reflection.Missing"), "Value");
						}
						if (!type.IsValueType)
						{
							if (type == typeof(string))
							{
								obj = "";
							}
							else
							{
								obj = null;
							}
							type = null;
						}
						else
						{
							if (type.GetConstructor(new Type[0]) != null)
							{
								return new CodeObjectCreateExpression(type, new CodeExpression[0]);
							}
							if (type == typeof(decimal))
							{
								return new CodeObjectCreateExpression(typeof(decimal), new CodeExpression[]
								{
									new CodePrimitiveExpression(0.0)
								});
							}
							if (type == typeof(DateTime))
							{
								return new CodeObjectCreateExpression(typeof(DateTime), new CodeExpression[]
								{
									new CodePrimitiveExpression(0L)
								});
							}
							throw new Exception(SR.GetString("AxImpNoDefaultValue", new object[]
							{
								method.Name,
								parameterInfo.Name,
								type.FullName
							}));
						}
					}
				}
				else if (type.IsPrimitive)
				{
					obj = AxWrapperGen.AxMethodGenerator.GetClsPrimitiveValue(obj);
					obj = AxWrapperGen.AxMethodGenerator.GetDefaultValueForUnsignedType(type, obj);
				}
				else if (obj != null && type.IsInstanceOfType(obj) && (obj is DateTime || obj is decimal || obj is bool))
				{
					if (obj is DateTime)
					{
						return new CodeObjectCreateExpression(typeof(DateTime), new CodeExpression[]
						{
							new CodeCastExpression(typeof(long), new CodePrimitiveExpression(((DateTime)obj).Ticks))
						});
					}
					if (obj is decimal)
					{
						return new CodeObjectCreateExpression(typeof(decimal), new CodeExpression[]
						{
							new CodeCastExpression(typeof(double), new CodePrimitiveExpression(decimal.ToDouble((decimal)obj)))
						});
					}
					if (obj is bool)
					{
						return new CodePrimitiveExpression((bool)obj);
					}
					if (!(obj is string))
					{
						throw new Exception(SR.GetString("AxImpUnrecognizedDefaultValueType", new object[]
						{
							method.Name,
							parameterInfo.Name,
							type.FullName
						}));
					}
					type = null;
				}
				else if (!type.IsValueType)
				{
					if (obj is DispatchWrapper)
					{
						obj = null;
					}
					if (obj == null || obj is string)
					{
						return new CodePrimitiveExpression(obj);
					}
					throw new Exception(SR.GetString("AxImpUnrecognizedDefaultValueType", new object[]
					{
						method.Name,
						parameterInfo.Name,
						type.FullName
					}));
				}
				if (type != null && type.IsEnum)
				{
					obj = (int)obj;
				}
				CodeExpression codeExpression = new CodePrimitiveExpression(obj);
				if (type != null)
				{
					codeExpression = new CodeCastExpression(type, codeExpression);
				}
				return codeExpression;
			}

			// Token: 0x040020C9 RID: 8393
			private MethodInfo _method;

			// Token: 0x040020CA RID: 8394
			private bool _removeOptionals;

			// Token: 0x040020CB RID: 8395
			private AxParameterData[] _params;

			// Token: 0x040020CC RID: 8396
			private Type _controlType;

			// Token: 0x040020CD RID: 8397
			protected static object OriginalParamNameKey = new object();

			// Token: 0x040020CE RID: 8398
			protected static string ReturnValueVariableName = "returnValue";
		}

		// Token: 0x0200052A RID: 1322
		private class AxReflectionInvokeMethodGenerator : AxWrapperGen.AxMethodGenerator
		{
			// Token: 0x06003041 RID: 12353 RVA: 0x00109236 File Offset: 0x00107436
			internal AxReflectionInvokeMethodGenerator(MethodInfo method, bool removeOpts) : base(method, removeOpts)
			{
			}

			// Token: 0x06003042 RID: 12354 RVA: 0x00109240 File Offset: 0x00107440
			public override CodeExpression DoMethodInvokeCore(CodeMemberMethod method, string methodName, Type returnType, CodeExpression targetObject, List<CodeExpression> parameters)
			{
				CodeExpression[] array = parameters.ToArray();
				for (int i = 0; i < array.Length; i++)
				{
					CodeVariableReferenceExpression codeVariableReferenceExpression = array[i] as CodeVariableReferenceExpression;
					if (codeVariableReferenceExpression != null)
					{
						AxParameterData axParameterData = codeVariableReferenceExpression.UserData[typeof(AxParameterData)] as AxParameterData;
						if (axParameterData != null && axParameterData.Direction == FieldDirection.Out)
						{
							array[i] = new CodePrimitiveExpression(null);
						}
					}
				}
				CodeArrayCreateExpression initExpression = new CodeArrayCreateExpression(typeof(object), array);
				CodeVariableDeclarationStatement value = new CodeVariableDeclarationStatement(typeof(object[]), "paramArray", initExpression);
				method.Statements.Add(value);
				CodeTypeOfExpression initExpression2 = new CodeTypeOfExpression(base.ControlType);
				CodeVariableDeclarationStatement value2 = new CodeVariableDeclarationStatement(typeof(Type), "typeVar", initExpression2);
				method.Statements.Add(value2);
				CodeMethodInvokeExpression initExpression3 = new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("typeVar"), "GetMethod", new CodeExpression[]
				{
					new CodePrimitiveExpression(methodName)
				});
				CodeVariableDeclarationStatement value3 = new CodeVariableDeclarationStatement(typeof(MethodInfo), "methodToInvoke", initExpression3);
				method.Statements.Add(value3);
				List<CodeExpression> list = new List<CodeExpression>();
				list.Add(targetObject);
				CodeVariableReferenceExpression codeVariableReferenceExpression2 = new CodeVariableReferenceExpression("paramArray");
				list.Add(codeVariableReferenceExpression2);
				CodeExpression result = base.DoMethodInvokeCore(method, "Invoke", returnType, new CodeVariableReferenceExpression("methodToInvoke"), list);
				for (int j = 0; j < parameters.Count; j++)
				{
					CodeVariableReferenceExpression codeVariableReferenceExpression3 = parameters[j] as CodeVariableReferenceExpression;
					if (codeVariableReferenceExpression3 != null)
					{
						AxParameterData axParameterData2 = codeVariableReferenceExpression3.UserData[typeof(AxParameterData)] as AxParameterData;
						if (axParameterData2 != null && axParameterData2.Direction != FieldDirection.In)
						{
							CodeExpression right = new CodeCastExpression(axParameterData2.TypeName, new CodeArrayIndexerExpression(codeVariableReferenceExpression2, new CodeExpression[]
							{
								new CodePrimitiveExpression(j)
							}));
							CodeAssignStatement value4 = new CodeAssignStatement(codeVariableReferenceExpression3, right);
							method.Statements.Add(value4);
						}
					}
				}
				return result;
			}
		}
	}
}
