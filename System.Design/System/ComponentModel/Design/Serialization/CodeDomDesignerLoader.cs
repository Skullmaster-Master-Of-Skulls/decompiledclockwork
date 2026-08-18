using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Specialized;
using System.Design;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using Microsoft.Internal.Performance;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x02000584 RID: 1412
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public abstract class CodeDomDesignerLoader : BasicDesignerLoader, INameCreationService, IDesignerSerializationService
	{
		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x06003214 RID: 12820
		protected abstract CodeDomProvider CodeDomProvider { get; }

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x06003215 RID: 12821
		protected abstract ITypeResolutionService TypeResolutionService { get; }

		// Token: 0x06003216 RID: 12822 RVA: 0x0011AE08 File Offset: 0x00119E08
		private void ClearDocument()
		{
			if (this._documentType != null)
			{
				base.LoaderHost.RemoveService(typeof(CodeTypeDeclaration));
				this._documentType = null;
				this._documentNamespace = null;
				this._documentCompileUnit = null;
				this._rootSerializer = null;
				this._typeSerializer = null;
			}
		}

		// Token: 0x06003217 RID: 12823 RVA: 0x0011AE58 File Offset: 0x00119E58
		public override void Dispose()
		{
			IDesignerHost designerHost = base.GetService(typeof(IDesignerHost)) as IDesignerHost;
			IComponentChangeService componentChangeService = base.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
			if (componentChangeService != null)
			{
				componentChangeService.ComponentRemoved -= this.OnComponentRemoved;
				componentChangeService.ComponentRename -= this.OnComponentRename;
			}
			if (designerHost != null)
			{
				designerHost.RemoveService(typeof(INameCreationService));
				designerHost.RemoveService(typeof(IDesignerSerializationService));
				designerHost.RemoveService(typeof(ComponentSerializationService));
				if (this._state[CodeDomDesignerLoader.StateOwnTypeResolution])
				{
					designerHost.RemoveService(typeof(ITypeResolutionService));
					this._state[CodeDomDesignerLoader.StateOwnTypeResolution] = false;
				}
			}
			if (this._extenderProviderService != null)
			{
				foreach (IExtenderProvider provider in this._extenderProviders)
				{
					this._extenderProviderService.RemoveExtenderProvider(provider);
				}
			}
			base.Dispose();
		}

		// Token: 0x06003218 RID: 12824 RVA: 0x0011AF58 File Offset: 0x00119F58
		private bool HasRootDesignerAttribute(Type t)
		{
			AttributeCollection attributes = TypeDescriptor.GetAttributes(t);
			for (int i = 0; i < attributes.Count; i++)
			{
				DesignerAttribute designerAttribute = attributes[i] as DesignerAttribute;
				if (designerAttribute != null)
				{
					Type type = Type.GetType(designerAttribute.DesignerBaseTypeName);
					if (type != null && type == typeof(IRootDesigner))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06003219 RID: 12825 RVA: 0x0011AFAC File Offset: 0x00119FAC
		private void EnsureDocument(IDesignerSerializationManager manager)
		{
			if (this._documentCompileUnit == null)
			{
				this._documentCompileUnit = this.Parse();
				if (this._documentCompileUnit == null)
				{
					throw new NotSupportedException(SR.GetString("CodeDomDesignerLoaderNoLanguageSupport"))
					{
						HelpLink = "CodeDomDesignerLoaderNoLanguageSupport"
					};
				}
			}
			if (this._documentType == null)
			{
				ArrayList arrayList = null;
				bool flag = true;
				if (this._documentCompileUnit.UserData[typeof(InvalidOperationException)] != null)
				{
					InvalidOperationException ex = this._documentCompileUnit.UserData[typeof(InvalidOperationException)] as InvalidOperationException;
					if (ex != null)
					{
						this._documentCompileUnit = null;
						throw ex;
					}
				}
				foreach (object obj in this._documentCompileUnit.Namespaces)
				{
					CodeNamespace codeNamespace = (CodeNamespace)obj;
					foreach (object obj2 in codeNamespace.Types)
					{
						CodeTypeDeclaration codeTypeDeclaration = (CodeTypeDeclaration)obj2;
						Type type = null;
						foreach (object obj3 in codeTypeDeclaration.BaseTypes)
						{
							CodeTypeReference codeTypeReference = (CodeTypeReference)obj3;
							Type type2 = base.LoaderHost.GetType(CodeDomSerializerBase.GetTypeNameFromCodeTypeReference(manager, codeTypeReference));
							if (type2 != null && !type2.IsInterface)
							{
								type = type2;
								break;
							}
							if (type2 == null)
							{
								if (arrayList == null)
								{
									arrayList = new ArrayList();
								}
								arrayList.Add(SR.GetString("CodeDomDesignerLoaderDocumentFailureTypeNotFound", new object[]
								{
									codeTypeDeclaration.Name,
									codeTypeReference.BaseType
								}));
							}
						}
						if (type != null)
						{
							bool flag2 = false;
							AttributeCollection attributes = TypeDescriptor.GetAttributes(type);
							foreach (object obj4 in attributes)
							{
								Attribute attribute = (Attribute)obj4;
								if (attribute is RootDesignerSerializerAttribute)
								{
									RootDesignerSerializerAttribute rootDesignerSerializerAttribute = (RootDesignerSerializerAttribute)attribute;
									string serializerBaseTypeName = rootDesignerSerializerAttribute.SerializerBaseTypeName;
									if (serializerBaseTypeName != null && base.LoaderHost.GetType(serializerBaseTypeName) == typeof(CodeDomSerializer))
									{
										Type type3 = base.LoaderHost.GetType(rootDesignerSerializerAttribute.SerializerTypeName);
										if (type3 != null && type3 != typeof(RootCodeDomSerializer))
										{
											flag2 = true;
											if (flag)
											{
												this._rootSerializer = (CodeDomSerializer)Activator.CreateInstance(type3, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, null, null);
												break;
											}
											throw new InvalidOperationException(SR.GetString("CodeDomDesignerLoaderSerializerTypeNotFirstType", new object[]
											{
												codeTypeDeclaration.Name
											}));
										}
									}
								}
							}
							if (this._rootSerializer == null && this.HasRootDesignerAttribute(type))
							{
								this._typeSerializer = (manager.GetSerializer(type, typeof(TypeCodeDomSerializer)) as TypeCodeDomSerializer);
								if (!flag && this._typeSerializer != null)
								{
									this._typeSerializer = null;
									this._documentCompileUnit = null;
									throw new InvalidOperationException(SR.GetString("CodeDomDesignerLoaderSerializerTypeNotFirstType", new object[]
									{
										codeTypeDeclaration.Name
									}));
								}
							}
							if (this._rootSerializer == null && this._typeSerializer == null)
							{
								if (arrayList == null)
								{
									arrayList = new ArrayList();
								}
								if (flag2)
								{
									arrayList.Add(SR.GetString("CodeDomDesignerLoaderDocumentFailureTypeDesignerNotInstalled", new object[]
									{
										codeTypeDeclaration.Name,
										type.FullName
									}));
								}
								else
								{
									arrayList.Add(SR.GetString("CodeDomDesignerLoaderDocumentFailureTypeNotDesignable", new object[]
									{
										codeTypeDeclaration.Name,
										type.FullName
									}));
								}
							}
						}
						if (this._rootSerializer != null || this._typeSerializer != null)
						{
							this._documentNamespace = codeNamespace;
							this._documentType = codeTypeDeclaration;
							break;
						}
						flag = false;
					}
					if (this._documentType != null)
					{
						break;
					}
				}
				if (this._documentType == null)
				{
					this._documentCompileUnit = null;
					Exception ex2;
					if (arrayList != null)
					{
						StringBuilder stringBuilder = new StringBuilder();
						foreach (object obj5 in arrayList)
						{
							string value = (string)obj5;
							stringBuilder.Append("\r\n");
							stringBuilder.Append(value);
						}
						ex2 = new InvalidOperationException(SR.GetString("CodeDomDesignerLoaderNoRootSerializerWithFailures", new object[]
						{
							stringBuilder.ToString()
						}));
						ex2.HelpLink = "CodeDomDesignerLoaderNoRootSerializer";
					}
					else
					{
						ex2 = new InvalidOperationException(SR.GetString("CodeDomDesignerLoaderNoRootSerializer"));
						ex2.HelpLink = "CodeDomDesignerLoaderNoRootSerializer";
					}
					throw ex2;
				}
				base.LoaderHost.AddService(typeof(CodeTypeDeclaration), this._documentType);
			}
			CodeDomDesignerLoader.codemarkers.CodeMarker(CodeMarkerEvent.perfFXGetDocumentType);
		}

		// Token: 0x0600321A RID: 12826 RVA: 0x0011B4FC File Offset: 0x0011A4FC
		private bool IntegrateSerializedTree(IDesignerSerializationManager manager, CodeTypeDeclaration newDecl)
		{
			this.EnsureDocument(manager);
			CodeTypeDeclaration documentType = this._documentType;
			bool flag = false;
			bool result = false;
			CodeDomProvider codeDomProvider = this.CodeDomProvider;
			if (codeDomProvider != null)
			{
				flag = ((codeDomProvider.LanguageOptions & LanguageOptions.CaseInsensitive) != LanguageOptions.None);
			}
			if (!string.Equals(documentType.Name, newDecl.Name, flag ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal))
			{
				documentType.Name = newDecl.Name;
				result = true;
			}
			if (!documentType.Attributes.Equals(newDecl.Attributes))
			{
				documentType.Attributes = newDecl.Attributes;
				result = true;
			}
			int num = 0;
			bool flag2 = false;
			int num2 = 0;
			bool flag3 = false;
			IDictionary dictionary = new HybridDictionary(documentType.Members.Count, flag);
			int count = documentType.Members.Count;
			for (int i = 0; i < count; i++)
			{
				CodeTypeMember codeTypeMember = documentType.Members[i];
				string key;
				if (codeTypeMember is CodeConstructor)
				{
					key = ".ctor";
				}
				else if (codeTypeMember is CodeTypeConstructor)
				{
					key = ".cctor";
				}
				else
				{
					key = codeTypeMember.Name;
				}
				dictionary[key] = i;
				if (codeTypeMember is CodeMemberField)
				{
					if (!flag2)
					{
						num = i;
					}
				}
				else if (num > 0)
				{
					flag2 = true;
				}
				if (codeTypeMember is CodeMemberMethod)
				{
					if (!flag3)
					{
						num2 = i;
					}
				}
				else if (num2 > 0)
				{
					flag3 = true;
				}
			}
			ArrayList arrayList = new ArrayList();
			foreach (object obj in newDecl.Members)
			{
				CodeTypeMember codeTypeMember2 = (CodeTypeMember)obj;
				string key2;
				if (codeTypeMember2 is CodeConstructor)
				{
					key2 = ".ctor";
				}
				else
				{
					key2 = codeTypeMember2.Name;
				}
				object obj2 = dictionary[key2];
				if (obj2 != null)
				{
					int index = (int)obj2;
					CodeTypeMember codeTypeMember3 = documentType.Members[index];
					if (codeTypeMember3 != codeTypeMember2)
					{
						if (codeTypeMember2 is CodeMemberField)
						{
							if (codeTypeMember3 is CodeMemberField)
							{
								CodeMemberField codeMemberField = (CodeMemberField)codeTypeMember3;
								CodeMemberField codeMemberField2 = (CodeMemberField)codeTypeMember2;
								if (string.Equals(codeMemberField2.Name, codeMemberField.Name) && codeMemberField2.Attributes == codeMemberField.Attributes && CodeDomDesignerLoader.TypesEqual(codeMemberField2.Type, codeMemberField.Type))
								{
									continue;
								}
								documentType.Members[index] = codeTypeMember2;
							}
							else
							{
								arrayList.Add(codeTypeMember2);
							}
						}
						else if (codeTypeMember2 is CodeMemberMethod)
						{
							if (codeTypeMember3 is CodeMemberMethod && !(codeTypeMember3 is CodeConstructor))
							{
								CodeMemberMethod codeMemberMethod = (CodeMemberMethod)codeTypeMember3;
								CodeMemberMethod codeMemberMethod2 = (CodeMemberMethod)codeTypeMember2;
								codeMemberMethod.Statements.Clear();
								codeMemberMethod.Statements.AddRange(codeMemberMethod2.Statements);
							}
						}
						else
						{
							documentType.Members[index] = codeTypeMember2;
						}
						result = true;
					}
				}
				else
				{
					arrayList.Add(codeTypeMember2);
				}
			}
			foreach (object obj3 in arrayList)
			{
				CodeTypeMember codeTypeMember4 = (CodeTypeMember)obj3;
				if (codeTypeMember4 is CodeMemberField)
				{
					if (num >= documentType.Members.Count)
					{
						documentType.Members.Add(codeTypeMember4);
					}
					else
					{
						documentType.Members.Insert(num, codeTypeMember4);
					}
					num++;
					num2++;
					result = true;
				}
				else if (codeTypeMember4 is CodeMemberMethod)
				{
					if (num2 >= documentType.Members.Count)
					{
						documentType.Members.Add(codeTypeMember4);
					}
					else
					{
						documentType.Members.Insert(num2, codeTypeMember4);
					}
					num2++;
					result = true;
				}
				else
				{
					documentType.Members.Add(codeTypeMember4);
					result = true;
				}
			}
			return result;
		}

		// Token: 0x0600321B RID: 12827 RVA: 0x0011B8DC File Offset: 0x0011A8DC
		protected override void Initialize()
		{
			base.Initialize();
			ServiceCreatorCallback callback = new ServiceCreatorCallback(this.OnCreateService);
			base.LoaderHost.AddService(typeof(ComponentSerializationService), callback);
			base.LoaderHost.AddService(typeof(INameCreationService), this);
			base.LoaderHost.AddService(typeof(IDesignerSerializationService), this);
			if (base.GetService(typeof(ITypeResolutionService)) == null)
			{
				ITypeResolutionService typeResolutionService = this.TypeResolutionService;
				if (typeResolutionService == null)
				{
					throw new InvalidOperationException(SR.GetString("CodeDomDesignerLoaderNoTypeResolution"));
				}
				base.LoaderHost.AddService(typeof(ITypeResolutionService), typeResolutionService);
				this._state[CodeDomDesignerLoader.StateOwnTypeResolution] = true;
			}
			this._extenderProviderService = (base.GetService(typeof(IExtenderProviderService)) as IExtenderProviderService);
			if (this._extenderProviderService != null)
			{
				this._extenderProviders = new IExtenderProvider[]
				{
					new CodeDomDesignerLoader.ModifiersExtenderProvider(),
					new CodeDomDesignerLoader.ModifiersInheritedExtenderProvider()
				};
				foreach (IExtenderProvider provider in this._extenderProviders)
				{
					this._extenderProviderService.AddExtenderProvider(provider);
				}
			}
		}

		// Token: 0x0600321C RID: 12828 RVA: 0x0011BA00 File Offset: 0x0011AA00
		protected override bool IsReloadNeeded()
		{
			if (!base.IsReloadNeeded())
			{
				return false;
			}
			if (this._documentType == null)
			{
				return true;
			}
			ICodeDomDesignerReload codeDomDesignerReload = this.CodeDomProvider as ICodeDomDesignerReload;
			if (codeDomDesignerReload == null)
			{
				return true;
			}
			bool flag = true;
			string name = this._documentType.Name;
			try
			{
				this.ClearDocument();
				this.EnsureDocument(base.GetService(typeof(IDesignerSerializationManager)) as IDesignerSerializationManager);
			}
			catch
			{
			}
			if (this._documentCompileUnit != null)
			{
				flag = codeDomDesignerReload.ShouldReloadDesigner(this._documentCompileUnit);
				flag |= (this._documentType == null || !this._documentType.Name.Equals(name));
			}
			return flag;
		}

		// Token: 0x0600321D RID: 12829 RVA: 0x0011BAB0 File Offset: 0x0011AAB0
		protected override void OnBeginLoad()
		{
			IComponentChangeService componentChangeService = (IComponentChangeService)base.GetService(typeof(IComponentChangeService));
			if (componentChangeService != null)
			{
				componentChangeService.ComponentRemoved -= this.OnComponentRemoved;
				componentChangeService.ComponentRename -= this.OnComponentRename;
			}
			base.OnBeginLoad();
		}

		// Token: 0x0600321E RID: 12830 RVA: 0x0011BB00 File Offset: 0x0011AB00
		protected override void OnBeginUnload()
		{
			base.OnBeginUnload();
			this.ClearDocument();
		}

		// Token: 0x0600321F RID: 12831 RVA: 0x0011BB10 File Offset: 0x0011AB10
		private void OnComponentRemoved(object sender, ComponentEventArgs e)
		{
			string name = e.Component.Site.Name;
			this.RemoveDeclaration(name);
		}

		// Token: 0x06003220 RID: 12832 RVA: 0x0011BB35 File Offset: 0x0011AB35
		private void OnComponentRename(object sender, ComponentRenameEventArgs e)
		{
			this.OnComponentRename(e.Component, e.OldName, e.NewName);
		}

		// Token: 0x06003221 RID: 12833 RVA: 0x0011BB4F File Offset: 0x0011AB4F
		private object OnCreateService(IServiceContainer container, Type serviceType)
		{
			if (serviceType == typeof(ComponentSerializationService))
			{
				return new CodeDomComponentSerializationService(base.LoaderHost);
			}
			return null;
		}

		// Token: 0x06003222 RID: 12834 RVA: 0x0011BB6C File Offset: 0x0011AB6C
		protected override void OnEndLoad(bool successful, ICollection errors)
		{
			base.OnEndLoad(successful, errors);
			if (successful)
			{
				IComponentChangeService componentChangeService = (IComponentChangeService)base.GetService(typeof(IComponentChangeService));
				if (componentChangeService != null)
				{
					componentChangeService.ComponentRemoved += this.OnComponentRemoved;
					componentChangeService.ComponentRename += this.OnComponentRename;
				}
			}
		}

		// Token: 0x06003223 RID: 12835
		protected abstract CodeCompileUnit Parse();

		// Token: 0x06003224 RID: 12836 RVA: 0x0011BBC4 File Offset: 0x0011ABC4
		protected override void PerformFlush(IDesignerSerializationManager manager)
		{
			CodeTypeDeclaration codeTypeDeclaration = null;
			if (this._rootSerializer != null)
			{
				codeTypeDeclaration = (this._rootSerializer.Serialize(manager, base.LoaderHost.RootComponent) as CodeTypeDeclaration);
			}
			else if (this._typeSerializer != null)
			{
				codeTypeDeclaration = this._typeSerializer.Serialize(manager, base.LoaderHost.RootComponent, base.LoaderHost.Container.Components);
			}
			CodeDomDesignerLoader.codemarkers.CodeMarker(CodeMarkerEvent.perfFXGenerateCodeTreeEnd);
			if (codeTypeDeclaration != null && this.IntegrateSerializedTree(manager, codeTypeDeclaration))
			{
				CodeDomDesignerLoader.codemarkers.CodeMarker(CodeMarkerEvent.perfFXIntegrateSerializedTreeEnd);
				this.Write(this._documentCompileUnit);
			}
		}

		// Token: 0x06003225 RID: 12837 RVA: 0x0011BC64 File Offset: 0x0011AC64
		protected override void PerformLoad(IDesignerSerializationManager manager)
		{
			this.EnsureDocument(manager);
			CodeDomDesignerLoader.codemarkers.CodeMarker(CodeMarkerEvent.perfFXDeserializeStart);
			if (this._rootSerializer != null)
			{
				this._rootSerializer.Deserialize(manager, this._documentType);
			}
			else
			{
				this._typeSerializer.Deserialize(manager, this._documentType);
			}
			CodeDomDesignerLoader.codemarkers.CodeMarker(CodeMarkerEvent.perfFXDeserializeEnd);
			string baseComponentClassName = string.Format(CultureInfo.CurrentCulture, "{0}.{1}", new object[]
			{
				this._documentNamespace.Name,
				this._documentType.Name
			});
			base.SetBaseComponentClassName(baseComponentClassName);
		}

		// Token: 0x06003226 RID: 12838 RVA: 0x0011BD04 File Offset: 0x0011AD04
		protected virtual void OnComponentRename(object component, string oldName, string newName)
		{
			if (base.LoaderHost.RootComponent == component)
			{
				if (this._documentType != null)
				{
					this._documentType.Name = newName;
					return;
				}
			}
			else if (this._documentType != null)
			{
				CodeTypeMemberCollection members = this._documentType.Members;
				for (int i = 0; i < members.Count; i++)
				{
					if (members[i] is CodeMemberField && members[i].Name.Equals(oldName) && ((CodeMemberField)members[i]).Type.BaseType.Equals(TypeDescriptor.GetClassName(component)))
					{
						members[i].Name = newName;
						return;
					}
				}
			}
		}

		// Token: 0x06003227 RID: 12839 RVA: 0x0011BDB0 File Offset: 0x0011ADB0
		private void RemoveDeclaration(string name)
		{
			if (this._documentType != null)
			{
				CodeTypeMemberCollection members = this._documentType.Members;
				for (int i = 0; i < members.Count; i++)
				{
					if (members[i] is CodeMemberField && members[i].Name.Equals(name))
					{
						((IList)members).RemoveAt(i);
						return;
					}
				}
			}
		}

		// Token: 0x06003228 RID: 12840 RVA: 0x0011BE0C File Offset: 0x0011AE0C
		private void ThrowMissingService(Type serviceType)
		{
			throw new InvalidOperationException(SR.GetString("BasicDesignerLoaderMissingService", new object[]
			{
				serviceType.Name
			}))
			{
				HelpLink = "BasicDesignerLoaderMissingService"
			};
		}

		// Token: 0x06003229 RID: 12841 RVA: 0x0011BE48 File Offset: 0x0011AE48
		private static bool TypesEqual(CodeTypeReference typeLeft, CodeTypeReference typeRight)
		{
			if (typeLeft.ArrayRank != typeRight.ArrayRank)
			{
				return false;
			}
			if (!typeLeft.BaseType.Equals(typeRight.BaseType))
			{
				return false;
			}
			if (typeLeft.TypeArguments != null && typeRight.TypeArguments == null)
			{
				return false;
			}
			if (typeLeft.TypeArguments == null && typeRight.TypeArguments != null)
			{
				return false;
			}
			if (typeLeft.TypeArguments != null && typeRight.TypeArguments != null)
			{
				if (typeLeft.TypeArguments.Count != typeRight.TypeArguments.Count)
				{
					return false;
				}
				for (int i = 0; i < typeLeft.TypeArguments.Count; i++)
				{
					if (!CodeDomDesignerLoader.TypesEqual(typeLeft.TypeArguments[i], typeRight.TypeArguments[i]))
					{
						return false;
					}
				}
			}
			return typeLeft.ArrayRank <= 0 || CodeDomDesignerLoader.TypesEqual(typeLeft.ArrayElementType, typeRight.ArrayElementType);
		}

		// Token: 0x0600322A RID: 12842
		protected abstract void Write(CodeCompileUnit unit);

		// Token: 0x0600322B RID: 12843 RVA: 0x0011BF1C File Offset: 0x0011AF1C
		ICollection IDesignerSerializationService.Deserialize(object serializationData)
		{
			if (!(serializationData is SerializationStore))
			{
				throw new ArgumentException(SR.GetString("CodeDomDesignerLoaderBadSerializationObject"))
				{
					HelpLink = "CodeDomDesignerLoaderBadSerializationObject"
				};
			}
			ComponentSerializationService componentSerializationService = base.GetService(typeof(ComponentSerializationService)) as ComponentSerializationService;
			if (componentSerializationService == null)
			{
				this.ThrowMissingService(typeof(ComponentSerializationService));
			}
			return componentSerializationService.Deserialize((SerializationStore)serializationData, base.LoaderHost.Container);
		}

		// Token: 0x0600322C RID: 12844 RVA: 0x0011BF90 File Offset: 0x0011AF90
		object IDesignerSerializationService.Serialize(ICollection objects)
		{
			if (objects == null)
			{
				objects = new object[0];
			}
			ComponentSerializationService componentSerializationService = base.GetService(typeof(ComponentSerializationService)) as ComponentSerializationService;
			if (componentSerializationService == null)
			{
				this.ThrowMissingService(typeof(ComponentSerializationService));
			}
			SerializationStore serializationStore = componentSerializationService.CreateStore();
			using (serializationStore)
			{
				foreach (object value in objects)
				{
					componentSerializationService.Serialize(serializationStore, value);
				}
			}
			return serializationStore;
		}

		// Token: 0x0600322D RID: 12845 RVA: 0x0011C040 File Offset: 0x0011B040
		string INameCreationService.CreateName(IContainer container, Type dataType)
		{
			if (dataType == null)
			{
				throw new ArgumentNullException("dataType");
			}
			string text = dataType.Name;
			StringBuilder stringBuilder = new StringBuilder(text.Length);
			for (int i = 0; i < text.Length; i++)
			{
				if (!char.IsUpper(text[i]) || (i != 0 && i != text.Length - 1 && !char.IsUpper(text[i + 1])))
				{
					stringBuilder.Append(text.Substring(i));
					break;
				}
				stringBuilder.Append(char.ToLower(text[i], CultureInfo.CurrentCulture));
			}
			stringBuilder.Replace('`', '_');
			text = stringBuilder.ToString();
			CodeTypeDeclaration documentType = this._documentType;
			Hashtable hashtable = new Hashtable(StringComparer.CurrentCultureIgnoreCase);
			if (documentType != null)
			{
				foreach (object obj in documentType.Members)
				{
					CodeTypeMember codeTypeMember = (CodeTypeMember)obj;
					hashtable[codeTypeMember.Name] = codeTypeMember;
				}
			}
			string text2;
			if (container != null)
			{
				int num = 0;
				bool flag;
				do
				{
					num++;
					flag = false;
					text2 = string.Format(CultureInfo.CurrentCulture, "{0}{1}", new object[]
					{
						text,
						num.ToString(CultureInfo.InvariantCulture)
					});
					if (container != null && container.Components[text2] != null)
					{
						flag = true;
					}
					if (!flag && hashtable[text2] != null)
					{
						flag = true;
					}
				}
				while (flag);
			}
			else
			{
				text2 = text;
			}
			if (this._codeGenerator == null)
			{
				CodeDomProvider codeDomProvider = this.CodeDomProvider;
				if (codeDomProvider != null)
				{
					this._codeGenerator = codeDomProvider.CreateGenerator();
				}
			}
			if (this._codeGenerator != null)
			{
				text2 = this._codeGenerator.CreateValidIdentifier(text2);
			}
			return text2;
		}

		// Token: 0x0600322E RID: 12846 RVA: 0x0011C200 File Offset: 0x0011B200
		bool INameCreationService.IsValidName(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				return false;
			}
			if (this._codeGenerator == null)
			{
				CodeDomProvider codeDomProvider = this.CodeDomProvider;
				if (codeDomProvider != null)
				{
					this._codeGenerator = codeDomProvider.CreateGenerator();
				}
			}
			if (this._codeGenerator != null)
			{
				if (!this._codeGenerator.IsValidIdentifier(name))
				{
					return false;
				}
				if (!this._codeGenerator.IsValidIdentifier(name + "Handler"))
				{
					return false;
				}
			}
			if (!this.Loading)
			{
				CodeTypeDeclaration documentType = this._documentType;
				if (documentType != null)
				{
					foreach (object obj in documentType.Members)
					{
						CodeTypeMember codeTypeMember = (CodeTypeMember)obj;
						if (string.Equals(codeTypeMember.Name, name, StringComparison.OrdinalIgnoreCase))
						{
							return false;
						}
					}
				}
				if (this.Modified && base.LoaderHost.Container.Components[name] != null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600322F RID: 12847 RVA: 0x0011C30C File Offset: 0x0011B30C
		void INameCreationService.ValidateName(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(SR.GetString("CodeDomDesignerLoaderInvalidBlankIdentifier"))
				{
					HelpLink = "CodeDomDesignerLoaderInvalidIdentifier"
				};
			}
			if (this._codeGenerator == null)
			{
				CodeDomProvider codeDomProvider = this.CodeDomProvider;
				if (codeDomProvider != null)
				{
					this._codeGenerator = codeDomProvider.CreateGenerator();
				}
			}
			if (this._codeGenerator != null)
			{
				this._codeGenerator.ValidateIdentifier(name);
				try
				{
					this._codeGenerator.ValidateIdentifier(name + "_");
				}
				catch
				{
					throw new ArgumentException(SR.GetString("CodeDomDesignerLoaderInvalidIdentifier", new object[]
					{
						name
					}))
					{
						HelpLink = "CodeDomDesignerLoaderInvalidIdentifier"
					};
				}
			}
			if (!this.Loading)
			{
				bool flag = false;
				CodeTypeDeclaration documentType = this._documentType;
				if (documentType != null)
				{
					foreach (object obj in documentType.Members)
					{
						CodeTypeMember codeTypeMember = (CodeTypeMember)obj;
						if (string.Equals(codeTypeMember.Name, name, StringComparison.OrdinalIgnoreCase))
						{
							flag = true;
							break;
						}
					}
				}
				if (!flag && this.Modified && base.LoaderHost.Container.Components[name] != null)
				{
					flag = true;
				}
				if (flag)
				{
					throw new ArgumentException(SR.GetString("CodeDomDesignerLoaderDupComponentName", new object[]
					{
						name
					}))
					{
						HelpLink = "CodeDomDesignerLoaderDupComponentName"
					};
				}
			}
		}

		// Token: 0x04002155 RID: 8533
		private static TraceSwitch traceCDLoader = new TraceSwitch("CodeDomDesignerLoader", "Trace CodeDomDesignerLoader");

		// Token: 0x04002156 RID: 8534
		private static CodeMarkers codemarkers = CodeMarkers.Instance;

		// Token: 0x04002157 RID: 8535
		private static readonly int StateCodeDomDirty = BitVector32.CreateMask();

		// Token: 0x04002158 RID: 8536
		private static readonly int StateCodeParserChecked = BitVector32.CreateMask(CodeDomDesignerLoader.StateCodeDomDirty);

		// Token: 0x04002159 RID: 8537
		private static readonly int StateOwnTypeResolution = BitVector32.CreateMask(CodeDomDesignerLoader.StateCodeParserChecked);

		// Token: 0x0400215A RID: 8538
		private BitVector32 _state = default(BitVector32);

		// Token: 0x0400215B RID: 8539
		private IExtenderProvider[] _extenderProviders;

		// Token: 0x0400215C RID: 8540
		private IExtenderProviderService _extenderProviderService;

		// Token: 0x0400215D RID: 8541
		private ICodeGenerator _codeGenerator;

		// Token: 0x0400215E RID: 8542
		private CodeDomSerializer _rootSerializer;

		// Token: 0x0400215F RID: 8543
		private TypeCodeDomSerializer _typeSerializer;

		// Token: 0x04002160 RID: 8544
		private CodeCompileUnit _documentCompileUnit;

		// Token: 0x04002161 RID: 8545
		private CodeNamespace _documentNamespace;

		// Token: 0x04002162 RID: 8546
		private CodeTypeDeclaration _documentType;

		// Token: 0x02000585 RID: 1413
		[ProvideProperty("GenerateMember", typeof(IComponent))]
		[ProvideProperty("Modifiers", typeof(IComponent))]
		private class ModifiersExtenderProvider : IExtenderProvider
		{
			// Token: 0x06003232 RID: 12850 RVA: 0x0011C50C File Offset: 0x0011B50C
			public bool CanExtend(object o)
			{
				IComponent component = o as IComponent;
				if (component == null)
				{
					return false;
				}
				IComponent baseComponent = this.GetBaseComponent(component);
				return o != baseComponent && TypeDescriptor.GetAttributes(o)[typeof(InheritanceAttribute)].Equals(InheritanceAttribute.NotInherited);
			}

			// Token: 0x06003233 RID: 12851 RVA: 0x0011C558 File Offset: 0x0011B558
			private IComponent GetBaseComponent(IComponent c)
			{
				IComponent result = null;
				if (c == null)
				{
					return null;
				}
				if (this._host == null)
				{
					ISite site = c.Site;
					if (site != null)
					{
						this._host = (IDesignerHost)site.GetService(typeof(IDesignerHost));
					}
				}
				if (this._host != null)
				{
					result = this._host.RootComponent;
				}
				return result;
			}

			// Token: 0x06003234 RID: 12852 RVA: 0x0011C5B0 File Offset: 0x0011B5B0
			[SRDescription("CodeDomDesignerLoaderPropGenerateMember")]
			[Category("Design")]
			[HelpKeyword("Designer_GenerateMember")]
			[DefaultValue(true)]
			[DesignOnly(true)]
			public bool GetGenerateMember(IComponent comp)
			{
				ISite site = comp.Site;
				if (site != null)
				{
					IDictionaryService dictionaryService = (IDictionaryService)site.GetService(typeof(IDictionaryService));
					if (dictionaryService != null)
					{
						object value = dictionaryService.GetValue("GenerateMember");
						if (value is bool)
						{
							return (bool)value;
						}
					}
				}
				return true;
			}

			// Token: 0x06003235 RID: 12853 RVA: 0x0011C5FC File Offset: 0x0011B5FC
			[DesignOnly(true)]
			[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
			[HelpKeyword("Designer_Modifiers")]
			[TypeConverter(typeof(CodeDomDesignerLoader.ModifierConverter))]
			[Category("Design")]
			[DefaultValue(MemberAttributes.Private)]
			[SRDescription("CodeDomDesignerLoaderPropModifiers")]
			public MemberAttributes GetModifiers(IComponent comp)
			{
				ISite site = comp.Site;
				if (site != null)
				{
					IDictionaryService dictionaryService = (IDictionaryService)site.GetService(typeof(IDictionaryService));
					if (dictionaryService != null)
					{
						object value = dictionaryService.GetValue("Modifiers");
						if (value is MemberAttributes)
						{
							return (MemberAttributes)value;
						}
					}
				}
				PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(comp);
				PropertyDescriptor propertyDescriptor = properties["DefaultModifiers"];
				if (propertyDescriptor != null && propertyDescriptor.PropertyType == typeof(MemberAttributes))
				{
					return (MemberAttributes)propertyDescriptor.GetValue(comp);
				}
				return MemberAttributes.Private;
			}

			// Token: 0x06003236 RID: 12854 RVA: 0x0011C688 File Offset: 0x0011B688
			public void SetGenerateMember(IComponent comp, bool generate)
			{
				ISite site = comp.Site;
				if (site != null)
				{
					IDictionaryService dictionaryService = (IDictionaryService)site.GetService(typeof(IDictionaryService));
					bool generateMember = this.GetGenerateMember(comp);
					if (dictionaryService != null)
					{
						dictionaryService.SetValue("GenerateMember", generate);
					}
					if (generateMember && !generate)
					{
						CodeTypeDeclaration codeTypeDeclaration = site.GetService(typeof(CodeTypeDeclaration)) as CodeTypeDeclaration;
						string name = site.Name;
						if (codeTypeDeclaration != null && name != null)
						{
							foreach (object obj in codeTypeDeclaration.Members)
							{
								CodeTypeMember codeTypeMember = (CodeTypeMember)obj;
								CodeMemberField codeMemberField = codeTypeMember as CodeMemberField;
								if (codeMemberField != null && codeMemberField.Name.Equals(name))
								{
									codeTypeDeclaration.Members.Remove(codeMemberField);
									break;
								}
							}
						}
					}
				}
			}

			// Token: 0x06003237 RID: 12855 RVA: 0x0011C780 File Offset: 0x0011B780
			public void SetModifiers(IComponent comp, MemberAttributes modifiers)
			{
				ISite site = comp.Site;
				if (site != null)
				{
					IDictionaryService dictionaryService = (IDictionaryService)site.GetService(typeof(IDictionaryService));
					if (dictionaryService != null)
					{
						dictionaryService.SetValue("Modifiers", modifiers);
					}
				}
			}

			// Token: 0x04002163 RID: 8547
			private IDesignerHost _host;
		}

		// Token: 0x02000586 RID: 1414
		[ProvideProperty("Modifiers", typeof(IComponent))]
		private class ModifiersInheritedExtenderProvider : IExtenderProvider
		{
			// Token: 0x06003239 RID: 12857 RVA: 0x0011C7CC File Offset: 0x0011B7CC
			public bool CanExtend(object o)
			{
				IComponent component = o as IComponent;
				if (component == null)
				{
					return false;
				}
				IComponent baseComponent = this.GetBaseComponent(component);
				if (o == baseComponent)
				{
					return false;
				}
				AttributeCollection attributes = TypeDescriptor.GetAttributes(o);
				return !attributes[typeof(InheritanceAttribute)].Equals(InheritanceAttribute.NotInherited);
			}

			// Token: 0x0600323A RID: 12858 RVA: 0x0011C81C File Offset: 0x0011B81C
			private IComponent GetBaseComponent(IComponent c)
			{
				IComponent result = null;
				if (c == null)
				{
					return null;
				}
				if (this._host == null)
				{
					ISite site = c.Site;
					if (site != null)
					{
						this._host = (IDesignerHost)site.GetService(typeof(IDesignerHost));
					}
				}
				if (this._host != null)
				{
					result = this._host.RootComponent;
				}
				return result;
			}

			// Token: 0x0600323B RID: 12859 RVA: 0x0011C874 File Offset: 0x0011B874
			[DesignOnly(true)]
			[Category("Design")]
			[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
			[TypeConverter(typeof(CodeDomDesignerLoader.ModifierConverter))]
			[DefaultValue(MemberAttributes.Private)]
			[SRDescription("CodeDomDesignerLoaderPropModifiers")]
			public MemberAttributes GetModifiers(IComponent comp)
			{
				IComponent baseComponent = this.GetBaseComponent(comp);
				Type type = baseComponent.GetType();
				ISite site = comp.Site;
				if (site != null)
				{
					string name = site.Name;
					if (name != null)
					{
						FieldInfo field = TypeDescriptor.GetReflectionType(type).GetField(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
						if (field != null)
						{
							if (field.IsPrivate)
							{
								return MemberAttributes.Private;
							}
							if (field.IsPublic)
							{
								return MemberAttributes.Public;
							}
							if (field.IsFamily)
							{
								return MemberAttributes.Family;
							}
							if (field.IsAssembly)
							{
								return MemberAttributes.Assembly;
							}
							if (field.IsFamilyOrAssembly)
							{
								return MemberAttributes.FamilyOrAssembly;
							}
							if (field.IsFamilyAndAssembly)
							{
								return MemberAttributes.FamilyAndAssembly;
							}
						}
						else
						{
							PropertyInfo property = TypeDescriptor.GetReflectionType(type).GetProperty(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
							if (property != null)
							{
								MethodInfo[] accessors = property.GetAccessors(true);
								if (accessors != null && accessors.Length > 0)
								{
									MethodInfo methodInfo = accessors[0];
									if (methodInfo != null)
									{
										if (methodInfo.IsPrivate)
										{
											return MemberAttributes.Private;
										}
										if (methodInfo.IsPublic)
										{
											return MemberAttributes.Public;
										}
										if (methodInfo.IsFamily)
										{
											return MemberAttributes.Family;
										}
										if (methodInfo.IsAssembly)
										{
											return MemberAttributes.Assembly;
										}
										if (methodInfo.IsFamilyOrAssembly)
										{
											return MemberAttributes.FamilyOrAssembly;
										}
										if (methodInfo.IsFamilyAndAssembly)
										{
											return MemberAttributes.FamilyAndAssembly;
										}
									}
								}
							}
						}
					}
				}
				return MemberAttributes.Private;
			}

			// Token: 0x04002164 RID: 8548
			private IDesignerHost _host;
		}

		// Token: 0x02000587 RID: 1415
		private class ModifierConverter : TypeConverter
		{
			// Token: 0x0600323D RID: 12861 RVA: 0x0011C9B5 File Offset: 0x0011B9B5
			public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
			{
				return this.GetConverter(context).CanConvertFrom(context, sourceType);
			}

			// Token: 0x0600323E RID: 12862 RVA: 0x0011C9C5 File Offset: 0x0011B9C5
			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			{
				return this.GetConverter(context).CanConvertTo(context, destinationType);
			}

			// Token: 0x0600323F RID: 12863 RVA: 0x0011C9D5 File Offset: 0x0011B9D5
			public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
			{
				return this.GetConverter(context).ConvertFrom(context, culture, value);
			}

			// Token: 0x06003240 RID: 12864 RVA: 0x0011C9E6 File Offset: 0x0011B9E6
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				return this.GetConverter(context).ConvertTo(context, culture, value, destinationType);
			}

			// Token: 0x06003241 RID: 12865 RVA: 0x0011C9F9 File Offset: 0x0011B9F9
			public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
			{
				return this.GetConverter(context).CreateInstance(context, propertyValues);
			}

			// Token: 0x06003242 RID: 12866 RVA: 0x0011CA0C File Offset: 0x0011BA0C
			private TypeConverter GetConverter(ITypeDescriptorContext context)
			{
				TypeConverter typeConverter = null;
				if (context != null)
				{
					CodeDomProvider codeDomProvider = (CodeDomProvider)context.GetService(typeof(CodeDomProvider));
					if (codeDomProvider != null)
					{
						typeConverter = codeDomProvider.GetConverter(typeof(MemberAttributes));
					}
				}
				if (typeConverter == null)
				{
					typeConverter = TypeDescriptor.GetConverter(typeof(MemberAttributes));
				}
				return typeConverter;
			}

			// Token: 0x06003243 RID: 12867 RVA: 0x0011CA5C File Offset: 0x0011BA5C
			public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
			{
				return this.GetConverter(context).GetCreateInstanceSupported(context);
			}

			// Token: 0x06003244 RID: 12868 RVA: 0x0011CA6B File Offset: 0x0011BA6B
			public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
			{
				return this.GetConverter(context).GetProperties(context, value, attributes);
			}

			// Token: 0x06003245 RID: 12869 RVA: 0x0011CA7C File Offset: 0x0011BA7C
			public override bool GetPropertiesSupported(ITypeDescriptorContext context)
			{
				return this.GetConverter(context).GetPropertiesSupported(context);
			}

			// Token: 0x06003246 RID: 12870 RVA: 0x0011CA8C File Offset: 0x0011BA8C
			public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
			{
				TypeConverter.StandardValuesCollection standardValuesCollection = this.GetConverter(context).GetStandardValues(context);
				if (standardValuesCollection != null && standardValuesCollection.Count > 0)
				{
					bool flag = false;
					foreach (object obj in standardValuesCollection)
					{
						MemberAttributes memberAttributes = (MemberAttributes)obj;
						if ((memberAttributes & MemberAttributes.AccessMask) == (MemberAttributes)0)
						{
							flag = true;
							break;
						}
					}
					if (flag)
					{
						ArrayList arrayList = new ArrayList(standardValuesCollection.Count);
						foreach (object obj2 in standardValuesCollection)
						{
							MemberAttributes memberAttributes2 = (MemberAttributes)obj2;
							if ((memberAttributes2 & MemberAttributes.AccessMask) != (MemberAttributes)0 && memberAttributes2 != MemberAttributes.AccessMask)
							{
								arrayList.Add(memberAttributes2);
							}
						}
						standardValuesCollection = new TypeConverter.StandardValuesCollection(arrayList);
					}
				}
				return standardValuesCollection;
			}

			// Token: 0x06003247 RID: 12871 RVA: 0x0011CB8C File Offset: 0x0011BB8C
			public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
			{
				return this.GetConverter(context).GetStandardValuesExclusive(context);
			}

			// Token: 0x06003248 RID: 12872 RVA: 0x0011CB9B File Offset: 0x0011BB9B
			public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
			{
				return this.GetConverter(context).GetStandardValuesSupported(context);
			}

			// Token: 0x06003249 RID: 12873 RVA: 0x0011CBAA File Offset: 0x0011BBAA
			public override bool IsValid(ITypeDescriptorContext context, object value)
			{
				return this.GetConverter(context).IsValid(context, value);
			}
		}
	}
}
