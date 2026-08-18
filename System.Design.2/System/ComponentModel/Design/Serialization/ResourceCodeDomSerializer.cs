using System;
using System.CodeDom;
using System.Collections;
using System.Design;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x020001E7 RID: 487
	internal class ResourceCodeDomSerializer : CodeDomSerializer
	{
		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06001236 RID: 4662 RVA: 0x0006869B File Offset: 0x0006689B
		internal new static ResourceCodeDomSerializer Default
		{
			get
			{
				if (ResourceCodeDomSerializer.defaultSerializer == null)
				{
					ResourceCodeDomSerializer.defaultSerializer = new ResourceCodeDomSerializer();
				}
				return ResourceCodeDomSerializer.defaultSerializer;
			}
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x000686B4 File Offset: 0x000668B4
		public override string GetTargetComponentName(CodeStatement statement, CodeExpression expression, Type type)
		{
			string text = null;
			CodeExpressionStatement codeExpressionStatement = statement as CodeExpressionStatement;
			if (codeExpressionStatement != null)
			{
				CodeMethodInvokeExpression codeMethodInvokeExpression = codeExpressionStatement.Expression as CodeMethodInvokeExpression;
				if (codeMethodInvokeExpression != null)
				{
					CodeMethodReferenceExpression method = codeMethodInvokeExpression.Method;
					if (method != null && string.Equals(method.MethodName, "ApplyResources", StringComparison.OrdinalIgnoreCase) && codeMethodInvokeExpression.Parameters.Count > 0)
					{
						CodeFieldReferenceExpression codeFieldReferenceExpression = codeMethodInvokeExpression.Parameters[0] as CodeFieldReferenceExpression;
						CodeVariableReferenceExpression codeVariableReferenceExpression = codeMethodInvokeExpression.Parameters[0] as CodeVariableReferenceExpression;
						if (codeFieldReferenceExpression != null && codeFieldReferenceExpression.TargetObject is CodeThisReferenceExpression)
						{
							text = codeFieldReferenceExpression.FieldName;
						}
						else if (codeVariableReferenceExpression != null)
						{
							text = codeVariableReferenceExpression.VariableName;
						}
					}
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				text = base.GetTargetComponentName(statement, expression, type);
			}
			return text;
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06001238 RID: 4664 RVA: 0x0006876B File Offset: 0x0006696B
		private string ResourceManagerName
		{
			get
			{
				return "resources";
			}
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x00068774 File Offset: 0x00066974
		public override object Deserialize(IDesignerSerializationManager manager, object codeObject)
		{
			object obj = null;
			if (manager == null || codeObject == null)
			{
				throw new ArgumentNullException((manager == null) ? "manager" : "codeObject");
			}
			using (CodeDomSerializerBase.TraceScope("ResourceCodeDomSerializer::Deserialize"))
			{
				CodeExpression codeExpression = codeObject as CodeExpression;
				if (codeExpression != null)
				{
					obj = base.DeserializeExpression(manager, null, codeExpression);
				}
				else
				{
					CodeStatementCollection codeStatementCollection = codeObject as CodeStatementCollection;
					if (codeStatementCollection != null)
					{
						using (IEnumerator enumerator = codeStatementCollection.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								object obj2 = enumerator.Current;
								CodeStatement codeStatement = (CodeStatement)obj2;
								if (codeStatement is CodeVariableDeclarationStatement)
								{
									CodeVariableDeclarationStatement codeVariableDeclarationStatement = (CodeVariableDeclarationStatement)codeStatement;
									if (codeVariableDeclarationStatement.Name.Equals(this.ResourceManagerName))
									{
										obj = this.CreateResourceManager(manager);
									}
								}
								else if (obj == null)
								{
									obj = base.DeserializeStatementToInstance(manager, codeStatement);
								}
								else
								{
									base.DeserializeStatement(manager, codeStatement);
								}
							}
							return obj;
						}
					}
					if (!(codeObject is CodeStatement))
					{
						string text = string.Format(CultureInfo.CurrentCulture, "{0}, {1}, {2}", new object[]
						{
							typeof(CodeExpression).Name,
							typeof(CodeStatement).Name,
							typeof(CodeStatementCollection).Name
						});
						throw new ArgumentException(SR.GetString("SerializerBadElementTypes", new object[]
						{
							codeObject.GetType().Name,
							text
						}));
					}
				}
			}
			return obj;
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x00068918 File Offset: 0x00066B18
		private ResourceCodeDomSerializer.SerializationResourceManager CreateResourceManager(IDesignerSerializationManager manager)
		{
			ResourceCodeDomSerializer.SerializationResourceManager resourceManager = this.GetResourceManager(manager);
			if (!resourceManager.DeclarationAdded)
			{
				resourceManager.DeclarationAdded = true;
				manager.SetName(resourceManager, this.ResourceManagerName);
			}
			return resourceManager;
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x0006894C File Offset: 0x00066B4C
		protected override object DeserializeInstance(IDesignerSerializationManager manager, Type type, object[] parameters, string name, bool addToContainer)
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (name != null && name.Equals(this.ResourceManagerName) && typeof(ResourceManager).IsAssignableFrom(type))
			{
				return this.CreateResourceManager(manager);
			}
			return manager.CreateInstance(type, parameters, name, addToContainer);
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x000689B4 File Offset: 0x00066BB4
		public object DeserializeInvariant(IDesignerSerializationManager manager, string resourceName)
		{
			ResourceCodeDomSerializer.SerializationResourceManager resourceManager = this.GetResourceManager(manager);
			return resourceManager.GetObject(resourceName, true);
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x000689D4 File Offset: 0x00066BD4
		private Type GetCastType(IDesignerSerializationManager manager, object value)
		{
			ExpressionContext expressionContext = (ExpressionContext)manager.Context[typeof(ExpressionContext)];
			if (expressionContext != null)
			{
				return expressionContext.ExpressionType;
			}
			if (value != null)
			{
				Type type = value.GetType();
				while (!type.IsPublic && !type.IsNestedPublic)
				{
					type = type.BaseType;
				}
				return type;
			}
			return null;
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x00068A2C File Offset: 0x00066C2C
		public IDictionaryEnumerator GetEnumerator(IDesignerSerializationManager manager, CultureInfo culture)
		{
			ResourceCodeDomSerializer.SerializationResourceManager resourceManager = this.GetResourceManager(manager);
			return resourceManager.GetEnumerator(culture);
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x00068A48 File Offset: 0x00066C48
		public IDictionaryEnumerator GetMetadataEnumerator(IDesignerSerializationManager manager)
		{
			ResourceCodeDomSerializer.SerializationResourceManager resourceManager = this.GetResourceManager(manager);
			return resourceManager.GetMetadataEnumerator();
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x00068A64 File Offset: 0x00066C64
		private ResourceCodeDomSerializer.SerializationResourceManager GetResourceManager(IDesignerSerializationManager manager)
		{
			ResourceCodeDomSerializer.SerializationResourceManager serializationResourceManager = manager.Context[typeof(ResourceCodeDomSerializer.SerializationResourceManager)] as ResourceCodeDomSerializer.SerializationResourceManager;
			if (serializationResourceManager == null)
			{
				serializationResourceManager = new ResourceCodeDomSerializer.SerializationResourceManager(manager);
				manager.Context.Append(serializationResourceManager);
			}
			return serializationResourceManager;
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x00068AA3 File Offset: 0x00066CA3
		public override object Serialize(IDesignerSerializationManager manager, object value)
		{
			return this.Serialize(manager, value, false, false, true);
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x00068AB0 File Offset: 0x00066CB0
		public object Serialize(IDesignerSerializationManager manager, object value, bool shouldSerializeInvariant)
		{
			return this.Serialize(manager, value, false, shouldSerializeInvariant, true);
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x00068ABD File Offset: 0x00066CBD
		public object Serialize(IDesignerSerializationManager manager, object value, bool shouldSerializeInvariant, bool ensureInvariant)
		{
			return this.Serialize(manager, value, false, shouldSerializeInvariant, ensureInvariant);
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x00068ACC File Offset: 0x00066CCC
		private object Serialize(IDesignerSerializationManager manager, object value, bool forceInvariant, bool shouldSerializeInvariant, bool ensureInvariant)
		{
			CodeExpression result = null;
			using (CodeDomSerializerBase.TraceScope("ResourceCodeDomSerializer::Serialize"))
			{
				ResourceCodeDomSerializer.SerializationResourceManager resourceManager = this.GetResourceManager(manager);
				CodeStatementCollection codeStatementCollection = (CodeStatementCollection)manager.Context[typeof(CodeStatementCollection)];
				if (!forceInvariant)
				{
					if (!resourceManager.DeclarationAdded)
					{
						resourceManager.DeclarationAdded = true;
						RootContext rootContext = manager.Context[typeof(RootContext)] as RootContext;
						if (codeStatementCollection != null)
						{
							CodeExpression[] parameters;
							if (rootContext != null)
							{
								string name = manager.GetName(rootContext.Value);
								parameters = new CodeExpression[]
								{
									new CodeTypeOfExpression(name)
								};
							}
							else
							{
								parameters = new CodeExpression[]
								{
									new CodePrimitiveExpression(this.ResourceManagerName)
								};
							}
							CodeExpression initExpression = new CodeObjectCreateExpression(typeof(ComponentResourceManager), parameters);
							codeStatementCollection.Add(new CodeVariableDeclarationStatement(typeof(ComponentResourceManager), this.ResourceManagerName, initExpression));
							base.SetExpression(manager, resourceManager, new CodeVariableReferenceExpression(this.ResourceManagerName));
							resourceManager.ExpressionAdded = true;
						}
					}
					else if (!resourceManager.ExpressionAdded)
					{
						if (base.GetExpression(manager, resourceManager) == null)
						{
							base.SetExpression(manager, resourceManager, new CodeVariableReferenceExpression(this.ResourceManagerName));
						}
						resourceManager.ExpressionAdded = true;
					}
				}
				ExpressionContext expressionContext = (ExpressionContext)manager.Context[typeof(ExpressionContext)];
				string value2 = resourceManager.SetValue(manager, expressionContext, value, forceInvariant, shouldSerializeInvariant, ensureInvariant, false);
				bool flag;
				string methodName;
				if (value is string || (expressionContext != null && expressionContext.ExpressionType == typeof(string)))
				{
					flag = false;
					methodName = "GetString";
				}
				else
				{
					flag = true;
					methodName = "GetObject";
				}
				CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression();
				codeMethodInvokeExpression.Method = new CodeMethodReferenceExpression(new CodeVariableReferenceExpression(this.ResourceManagerName), methodName);
				codeMethodInvokeExpression.Parameters.Add(new CodePrimitiveExpression(value2));
				if (flag)
				{
					Type castType = this.GetCastType(manager, value);
					if (castType != null)
					{
						result = new CodeCastExpression(castType, codeMethodInvokeExpression);
					}
					else
					{
						result = codeMethodInvokeExpression;
					}
				}
				else
				{
					result = codeMethodInvokeExpression;
				}
			}
			return result;
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x00068CEC File Offset: 0x00066EEC
		public object SerializeInvariant(IDesignerSerializationManager manager, object value, bool shouldSerializeValue)
		{
			return this.Serialize(manager, value, true, shouldSerializeValue, true);
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x00068CFC File Offset: 0x00066EFC
		public void SerializeMetadata(IDesignerSerializationManager manager, string name, object value, bool shouldSerializeValue)
		{
			using (CodeDomSerializerBase.TraceScope("ResourceCodeDomSerializer::SerializeMetadata"))
			{
				ResourceCodeDomSerializer.SerializationResourceManager resourceManager = this.GetResourceManager(manager);
				resourceManager.SetMetadata(manager, name, value, shouldSerializeValue, false);
			}
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x00068D44 File Offset: 0x00066F44
		public void WriteResource(IDesignerSerializationManager manager, string name, object value)
		{
			using (CodeDomSerializerBase.TraceScope("ResourceCodeDomSerializer::WriteResource"))
			{
				ResourceCodeDomSerializer.SerializationResourceManager resourceManager = this.GetResourceManager(manager);
				resourceManager.SetValue(manager, name, value, false, false, true, false);
			}
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x00068D90 File Offset: 0x00066F90
		public void WriteResourceInvariant(IDesignerSerializationManager manager, string name, object value)
		{
			using (CodeDomSerializerBase.TraceScope("ResourceCodeDomSerializer::WriteResourceInvariant"))
			{
				ResourceCodeDomSerializer.SerializationResourceManager resourceManager = this.GetResourceManager(manager);
				resourceManager.SetValue(manager, name, value, true, true, true, false);
			}
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x00068DDC File Offset: 0x00066FDC
		internal void ApplyCacheEntry(IDesignerSerializationManager manager, ComponentCache.Entry entry)
		{
			ResourceCodeDomSerializer.SerializationResourceManager resourceManager = this.GetResourceManager(manager);
			if (entry.Metadata != null)
			{
				foreach (ComponentCache.ResourceEntry resourceEntry in entry.Metadata)
				{
					resourceManager.SetMetadata(manager, resourceEntry.Name, resourceEntry.Value, resourceEntry.ShouldSerializeValue, true);
				}
			}
			if (entry.Resources != null)
			{
				foreach (ComponentCache.ResourceEntry resourceEntry2 in entry.Resources)
				{
					manager.Context.Push(resourceEntry2.PropertyDescriptor);
					manager.Context.Push(resourceEntry2.ExpressionContext);
					try
					{
						resourceManager.SetValue(manager, resourceEntry2.Name, resourceEntry2.Value, resourceEntry2.ForceInvariant, resourceEntry2.ShouldSerializeValue, resourceEntry2.EnsureInvariant, true);
					}
					finally
					{
						manager.Context.Pop();
						manager.Context.Pop();
					}
				}
			}
		}

		// Token: 0x040009FC RID: 2556
		private static ResourceCodeDomSerializer defaultSerializer;

		// Token: 0x020004A9 RID: 1193
		internal class SerializationResourceManager : ComponentResourceManager
		{
			// Token: 0x06002BB2 RID: 11186 RVA: 0x00104B7E File Offset: 0x00102D7E
			public SerializationResourceManager(IDesignerSerializationManager manager)
			{
				this.manager = manager;
				this.nameTable = new Hashtable();
				manager.SerializationComplete += this.OnSerializationComplete;
			}

			// Token: 0x1700093E RID: 2366
			// (get) Token: 0x06002BB3 RID: 11187 RVA: 0x00104BAA File Offset: 0x00102DAA
			// (set) Token: 0x06002BB4 RID: 11188 RVA: 0x00104BB2 File Offset: 0x00102DB2
			public bool DeclarationAdded
			{
				get
				{
					return this.declarationAdded;
				}
				set
				{
					this.declarationAdded = value;
				}
			}

			// Token: 0x1700093F RID: 2367
			// (get) Token: 0x06002BB5 RID: 11189 RVA: 0x00104BBB File Offset: 0x00102DBB
			// (set) Token: 0x06002BB6 RID: 11190 RVA: 0x00104BC3 File Offset: 0x00102DC3
			public bool ExpressionAdded
			{
				get
				{
					return this.expressionAdded;
				}
				set
				{
					this.expressionAdded = value;
				}
			}

			// Token: 0x17000940 RID: 2368
			// (get) Token: 0x06002BB7 RID: 11191 RVA: 0x00104BCC File Offset: 0x00102DCC
			private CultureInfo LocalizationLanguage
			{
				get
				{
					if (!this.checkedLocalizationLanguage)
					{
						RootContext rootContext = this.manager.Context[typeof(RootContext)] as RootContext;
						if (rootContext != null)
						{
							object value = rootContext.Value;
							PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(value)["LoadLanguage"];
							if (propertyDescriptor != null && propertyDescriptor.PropertyType == typeof(CultureInfo))
							{
								this.localizationLanguage = (CultureInfo)propertyDescriptor.GetValue(value);
							}
						}
						this.checkedLocalizationLanguage = true;
					}
					return this.localizationLanguage;
				}
			}

			// Token: 0x17000941 RID: 2369
			// (get) Token: 0x06002BB8 RID: 11192 RVA: 0x00104C58 File Offset: 0x00102E58
			private CultureInfo ReadCulture
			{
				get
				{
					if (this.readCulture == null)
					{
						CultureInfo cultureInfo = this.LocalizationLanguage;
						if (cultureInfo != null)
						{
							this.readCulture = cultureInfo;
						}
						else
						{
							this.readCulture = CultureInfo.InvariantCulture;
						}
					}
					return this.readCulture;
				}
			}

			// Token: 0x17000942 RID: 2370
			// (get) Token: 0x06002BB9 RID: 11193 RVA: 0x00104C91 File Offset: 0x00102E91
			private Hashtable ResourceTable
			{
				get
				{
					if (this.resourceSets == null)
					{
						this.resourceSets = new Hashtable();
					}
					return this.resourceSets;
				}
			}

			// Token: 0x17000943 RID: 2371
			// (get) Token: 0x06002BBA RID: 11194 RVA: 0x00104CAC File Offset: 0x00102EAC
			private object RootComponent
			{
				get
				{
					if (this.rootComponent == null)
					{
						RootContext rootContext = this.manager.Context[typeof(RootContext)] as RootContext;
						if (rootContext != null)
						{
							this.rootComponent = rootContext.Value;
						}
					}
					return this.rootComponent;
				}
			}

			// Token: 0x17000944 RID: 2372
			// (get) Token: 0x06002BBB RID: 11195 RVA: 0x00104CF8 File Offset: 0x00102EF8
			private IResourceWriter Writer
			{
				get
				{
					if (this.writer == null)
					{
						IResourceService resourceService = (IResourceService)this.manager.GetService(typeof(IResourceService));
						if (resourceService != null)
						{
							this.writer = resourceService.GetResourceWriter(this.ReadCulture);
						}
						else
						{
							this.writer = new ResourceWriter(new MemoryStream());
						}
					}
					return this.writer;
				}
			}

			// Token: 0x06002BBC RID: 11196 RVA: 0x00104D58 File Offset: 0x00102F58
			private void AddCacheEntry(IDesignerSerializationManager manager, string name, object value, bool isMetadata, bool forceInvariant, bool shouldSerializeValue, bool ensureInvariant)
			{
				ComponentCache.Entry entry = manager.Context[typeof(ComponentCache.Entry)] as ComponentCache.Entry;
				if (entry != null)
				{
					ComponentCache.ResourceEntry re = default(ComponentCache.ResourceEntry);
					re.Name = name;
					re.Value = value;
					re.ForceInvariant = forceInvariant;
					re.ShouldSerializeValue = shouldSerializeValue;
					re.EnsureInvariant = ensureInvariant;
					re.PropertyDescriptor = (PropertyDescriptor)manager.Context[typeof(PropertyDescriptor)];
					re.ExpressionContext = (ExpressionContext)manager.Context[typeof(ExpressionContext)];
					if (isMetadata)
					{
						entry.AddMetadata(re);
						return;
					}
					entry.AddResource(re);
				}
			}

			// Token: 0x06002BBD RID: 11197 RVA: 0x00104E10 File Offset: 0x00103010
			public bool AddPropertyFill(object value)
			{
				bool flag = false;
				if (this.propertyFillAdded == null)
				{
					this.propertyFillAdded = new Hashtable();
				}
				else
				{
					flag = this.propertyFillAdded.ContainsKey(value);
				}
				if (!flag)
				{
					this.propertyFillAdded[value] = value;
				}
				return !flag;
			}

			// Token: 0x06002BBE RID: 11198 RVA: 0x00104E58 File Offset: 0x00103058
			public override void ApplyResources(object value, string objectName, CultureInfo culture)
			{
				if (culture == null)
				{
					culture = this.ReadCulture;
				}
				Control control = value as Control;
				if (control != null)
				{
					control.SuspendLayout();
				}
				base.ApplyResources(value, objectName, culture);
				if (control != null)
				{
					control.ResumeLayout(false);
				}
			}

			// Token: 0x06002BBF RID: 11199 RVA: 0x00104E94 File Offset: 0x00103094
			private ResourceCodeDomSerializer.SerializationResourceManager.CompareValue CompareWithParentValue(string name, object value)
			{
				if (this.ReadCulture.Equals(CultureInfo.InvariantCulture))
				{
					return ResourceCodeDomSerializer.SerializationResourceManager.CompareValue.Different;
				}
				CultureInfo parent = this.ReadCulture;
				Hashtable resourceSet;
				for (;;)
				{
					parent = parent.Parent;
					resourceSet = this.GetResourceSet(parent);
					bool flag = resourceSet != null && resourceSet.ContainsKey(name);
					if (flag)
					{
						break;
					}
					if (parent.Equals(CultureInfo.InvariantCulture))
					{
						return ResourceCodeDomSerializer.SerializationResourceManager.CompareValue.New;
					}
				}
				object obj = (resourceSet != null) ? resourceSet[name] : null;
				if (obj == value)
				{
					return ResourceCodeDomSerializer.SerializationResourceManager.CompareValue.Same;
				}
				if (obj == null)
				{
					return ResourceCodeDomSerializer.SerializationResourceManager.CompareValue.Different;
				}
				if (obj.Equals(value))
				{
					return ResourceCodeDomSerializer.SerializationResourceManager.CompareValue.Same;
				}
				return ResourceCodeDomSerializer.SerializationResourceManager.CompareValue.Different;
			}

			// Token: 0x06002BC0 RID: 11200 RVA: 0x00104F10 File Offset: 0x00103110
			private Hashtable CreateResourceSet(IResourceReader reader, CultureInfo culture)
			{
				Hashtable hashtable = new Hashtable();
				try
				{
					IDictionaryEnumerator enumerator = reader.GetEnumerator();
					while (enumerator.MoveNext())
					{
						string key = (string)enumerator.Key;
						object value = enumerator.Value;
						hashtable[key] = value;
					}
				}
				catch (Exception ex)
				{
					string text = ex.Message;
					if (text == null || text.Length == 0)
					{
						text = ex.GetType().Name;
					}
					Exception errorInformation;
					if (culture == CultureInfo.InvariantCulture)
					{
						errorInformation = new SerializationException(SR.GetString("SerializerResourceExceptionInvariant", new object[]
						{
							text
						}), ex);
					}
					else
					{
						errorInformation = new SerializationException(SR.GetString("SerializerResourceException", new object[]
						{
							culture.ToString(),
							text
						}), ex);
					}
					this.manager.ReportError(errorInformation);
				}
				return hashtable;
			}

			// Token: 0x06002BC1 RID: 11201 RVA: 0x00104FEC File Offset: 0x001031EC
			public IDictionaryEnumerator GetMetadataEnumerator()
			{
				if (this.mergedMetadata == null)
				{
					Hashtable hashtable = this.GetMetadata();
					if (hashtable != null)
					{
						Hashtable resourceSet = this.GetResourceSet(CultureInfo.InvariantCulture);
						if (resourceSet != null)
						{
							foreach (object obj in resourceSet)
							{
								DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
								if (!hashtable.ContainsKey(dictionaryEntry.Key))
								{
									hashtable.Add(dictionaryEntry.Key, dictionaryEntry.Value);
								}
							}
						}
						this.mergedMetadata = hashtable;
					}
				}
				if (this.mergedMetadata != null)
				{
					return this.mergedMetadata.GetEnumerator();
				}
				return null;
			}

			// Token: 0x06002BC2 RID: 11202 RVA: 0x0010509C File Offset: 0x0010329C
			public IDictionaryEnumerator GetEnumerator(CultureInfo culture)
			{
				Hashtable resourceSet = this.GetResourceSet(culture);
				if (resourceSet != null)
				{
					return resourceSet.GetEnumerator();
				}
				return null;
			}

			// Token: 0x06002BC3 RID: 11203 RVA: 0x001050BC File Offset: 0x001032BC
			private Hashtable GetMetadata()
			{
				if (this.metadata == null)
				{
					IResourceService resourceService = (IResourceService)this.manager.GetService(typeof(IResourceService));
					if (resourceService != null)
					{
						IResourceReader resourceReader = resourceService.GetResourceReader(CultureInfo.InvariantCulture);
						if (resourceReader != null)
						{
							try
							{
								ResXResourceReader resXResourceReader = resourceReader as ResXResourceReader;
								if (resXResourceReader != null)
								{
									this.metadata = new Hashtable();
									IDictionaryEnumerator metadataEnumerator = resXResourceReader.GetMetadataEnumerator();
									while (metadataEnumerator.MoveNext())
									{
										this.metadata[metadataEnumerator.Key] = metadataEnumerator.Value;
									}
								}
							}
							finally
							{
								resourceReader.Close();
							}
						}
					}
				}
				return this.metadata;
			}

			// Token: 0x06002BC4 RID: 11204 RVA: 0x0010515C File Offset: 0x0010335C
			public override object GetObject(string resourceName)
			{
				return this.GetObject(resourceName, false);
			}

			// Token: 0x06002BC5 RID: 11205 RVA: 0x00105168 File Offset: 0x00103368
			public object GetObject(string resourceName, bool forceInvariant)
			{
				CultureInfo cultureInfo;
				if (forceInvariant)
				{
					cultureInfo = CultureInfo.InvariantCulture;
				}
				else
				{
					cultureInfo = this.ReadCulture;
				}
				object obj = null;
				while (obj == null)
				{
					Hashtable resourceSet = this.GetResourceSet(cultureInfo);
					if (resourceSet != null)
					{
						obj = resourceSet[resourceName];
					}
					CultureInfo cultureInfo2 = cultureInfo;
					cultureInfo = cultureInfo.Parent;
					if (cultureInfo2.Equals(cultureInfo))
					{
						break;
					}
				}
				return obj;
			}

			// Token: 0x06002BC6 RID: 11206 RVA: 0x001051B4 File Offset: 0x001033B4
			private Hashtable GetResourceSet(CultureInfo culture)
			{
				Hashtable hashtable = null;
				object obj = this.ResourceTable[culture];
				if (obj == null)
				{
					IResourceService resourceService = (IResourceService)this.manager.GetService(typeof(IResourceService));
					if (resourceService != null)
					{
						IResourceReader resourceReader = resourceService.GetResourceReader(culture);
						if (resourceReader != null)
						{
							try
							{
								hashtable = this.CreateResourceSet(resourceReader, culture);
							}
							finally
							{
								resourceReader.Close();
							}
							this.ResourceTable[culture] = hashtable;
						}
						else if (culture.Equals(CultureInfo.InvariantCulture))
						{
							hashtable = new Hashtable();
							this.ResourceTable[culture] = hashtable;
						}
						else
						{
							this.ResourceTable[culture] = ResourceCodeDomSerializer.SerializationResourceManager.resourceSetSentinel;
						}
					}
				}
				else
				{
					hashtable = (obj as Hashtable);
				}
				return hashtable;
			}

			// Token: 0x06002BC7 RID: 11207 RVA: 0x0010526C File Offset: 0x0010346C
			public override ResourceSet GetResourceSet(CultureInfo culture, bool createIfNotExists, bool tryParents)
			{
				if (culture == null)
				{
					throw new ArgumentNullException("culture");
				}
				Hashtable resourceSet;
				for (;;)
				{
					resourceSet = this.GetResourceSet(culture);
					if (resourceSet != null)
					{
						break;
					}
					CultureInfo cultureInfo = culture;
					culture = culture.Parent;
					if (!tryParents || cultureInfo.Equals(culture))
					{
						goto IL_38;
					}
				}
				return new ResourceCodeDomSerializer.SerializationResourceManager.CodeDomResourceSet(resourceSet);
				IL_38:
				if (createIfNotExists)
				{
					return new ResourceCodeDomSerializer.SerializationResourceManager.CodeDomResourceSet();
				}
				return null;
			}

			// Token: 0x06002BC8 RID: 11208 RVA: 0x001052BB File Offset: 0x001034BB
			public override string GetString(string resourceName)
			{
				return this.GetObject(resourceName, false) as string;
			}

			// Token: 0x06002BC9 RID: 11209 RVA: 0x001052CC File Offset: 0x001034CC
			private void OnSerializationComplete(object sender, EventArgs e)
			{
				if (this.writer != null)
				{
					this.writer.Close();
					this.writer = null;
				}
				if (this.invariantCultureResourcesDirty || this.metadataResourcesDirty)
				{
					IResourceService resourceService = (IResourceService)this.manager.GetService(typeof(IResourceService));
					if (resourceService != null)
					{
						IResourceWriter resourceWriter = resourceService.GetResourceWriter(CultureInfo.InvariantCulture);
						try
						{
							object obj = this.ResourceTable[CultureInfo.InvariantCulture];
							Hashtable hashtable = (Hashtable)obj;
							IDictionaryEnumerator enumerator = hashtable.GetEnumerator();
							while (enumerator.MoveNext())
							{
								string name = (string)enumerator.Key;
								object value = enumerator.Value;
								resourceWriter.AddResource(name, value);
							}
							this.invariantCultureResourcesDirty = false;
							ResXResourceWriter resXResourceWriter = resourceWriter as ResXResourceWriter;
							if (resXResourceWriter != null)
							{
								foreach (object obj2 in this.metadata)
								{
									DictionaryEntry dictionaryEntry = (DictionaryEntry)obj2;
									resXResourceWriter.AddMetadata((string)dictionaryEntry.Key, dictionaryEntry.Value);
								}
							}
							this.metadataResourcesDirty = false;
							return;
						}
						finally
						{
							resourceWriter.Close();
						}
					}
					this.invariantCultureResourcesDirty = false;
					this.metadataResourcesDirty = false;
				}
			}

			// Token: 0x06002BCA RID: 11210 RVA: 0x00105424 File Offset: 0x00103624
			public void SetMetadata(IDesignerSerializationManager manager, string resourceName, object value, bool shouldSerializeValue, bool applyingCachedResources)
			{
				if (value != null && !value.GetType().IsSerializable)
				{
					return;
				}
				if (this.ReadCulture.Equals(CultureInfo.InvariantCulture))
				{
					ResXResourceWriter resXResourceWriter = this.Writer as ResXResourceWriter;
					if (shouldSerializeValue)
					{
						if (resXResourceWriter != null)
						{
							resXResourceWriter.AddMetadata(resourceName, value);
						}
						else
						{
							this.Writer.AddResource(resourceName, value);
						}
					}
				}
				else
				{
					IResourceWriter resourceWriter = null;
					IResourceService resourceService = (IResourceService)manager.GetService(typeof(IResourceService));
					if (resourceService != null)
					{
						resourceWriter = resourceService.GetResourceWriter(CultureInfo.InvariantCulture);
					}
					Hashtable resourceSet = this.GetResourceSet(CultureInfo.InvariantCulture);
					Hashtable hashtable;
					if (resourceWriter == null || resourceWriter is ResXResourceWriter)
					{
						hashtable = this.GetMetadata();
						if (hashtable == null)
						{
							this.metadata = new Hashtable();
							hashtable = this.metadata;
						}
						if (resourceSet.ContainsKey(resourceName))
						{
							resourceSet.Remove(resourceName);
						}
						this.metadataResourcesDirty = true;
					}
					else
					{
						hashtable = resourceSet;
						this.invariantCultureResourcesDirty = true;
					}
					if (hashtable != null)
					{
						if (shouldSerializeValue)
						{
							hashtable[resourceName] = value;
						}
						else
						{
							hashtable.Remove(resourceName);
						}
					}
					this.mergedMetadata = null;
				}
				if (!applyingCachedResources)
				{
					this.AddCacheEntry(manager, resourceName, value, true, false, shouldSerializeValue, false);
				}
			}

			// Token: 0x06002BCB RID: 11211 RVA: 0x0010553C File Offset: 0x0010373C
			public void SetValue(IDesignerSerializationManager manager, string resourceName, object value, bool forceInvariant, bool shouldSerializeInvariant, bool ensureInvariant, bool applyingCachedResources)
			{
				if (value != null && !value.GetType().IsSerializable)
				{
					return;
				}
				if (forceInvariant)
				{
					if (this.ReadCulture.Equals(CultureInfo.InvariantCulture))
					{
						if (shouldSerializeInvariant)
						{
							this.Writer.AddResource(resourceName, value);
						}
					}
					else
					{
						Hashtable resourceSet = this.GetResourceSet(CultureInfo.InvariantCulture);
						if (shouldSerializeInvariant)
						{
							resourceSet[resourceName] = value;
						}
						else
						{
							resourceSet.Remove(resourceName);
						}
						this.invariantCultureResourcesDirty = true;
					}
				}
				else
				{
					switch (this.CompareWithParentValue(resourceName, value))
					{
					case ResourceCodeDomSerializer.SerializationResourceManager.CompareValue.Different:
						this.Writer.AddResource(resourceName, value);
						break;
					case ResourceCodeDomSerializer.SerializationResourceManager.CompareValue.New:
						if (ensureInvariant)
						{
							Hashtable resourceSet2 = this.GetResourceSet(CultureInfo.InvariantCulture);
							resourceSet2[resourceName] = value;
							this.invariantCultureResourcesDirty = true;
							this.Writer.AddResource(resourceName, value);
						}
						else
						{
							bool flag = true;
							bool flag2 = false;
							PropertyDescriptor propertyDescriptor = (PropertyDescriptor)manager.Context[typeof(PropertyDescriptor)];
							if (propertyDescriptor != null)
							{
								ExpressionContext expressionContext = (ExpressionContext)manager.Context[typeof(ExpressionContext)];
								if (expressionContext != null && expressionContext.Expression is CodePropertyReferenceExpression)
								{
									flag = propertyDescriptor.ShouldSerializeValue(expressionContext.Owner);
									flag2 = !propertyDescriptor.CanResetValue(expressionContext.Owner);
								}
							}
							if (flag)
							{
								this.Writer.AddResource(resourceName, value);
								if (flag2)
								{
									Hashtable resourceSet3 = this.GetResourceSet(CultureInfo.InvariantCulture);
									resourceSet3[resourceName] = value;
									this.invariantCultureResourcesDirty = true;
								}
							}
						}
						break;
					}
				}
				if (!applyingCachedResources)
				{
					this.AddCacheEntry(manager, resourceName, value, false, forceInvariant, shouldSerializeInvariant, ensureInvariant);
				}
			}

			// Token: 0x06002BCC RID: 11212 RVA: 0x001056D4 File Offset: 0x001038D4
			public string SetValue(IDesignerSerializationManager manager, ExpressionContext tree, object value, bool forceInvariant, bool shouldSerializeInvariant, bool ensureInvariant, bool applyingCachedResources)
			{
				bool flag = false;
				string text;
				if (tree != null)
				{
					if (tree.Owner == this.RootComponent)
					{
						text = "$this";
					}
					else
					{
						text = manager.GetName(tree.Owner);
						if (text == null)
						{
							IReferenceService referenceService = (IReferenceService)manager.GetService(typeof(IReferenceService));
							if (referenceService != null)
							{
								text = referenceService.GetName(tree.Owner);
							}
						}
					}
					CodeExpression expression = tree.Expression;
					string text2;
					if (expression is CodePropertyReferenceExpression)
					{
						text2 = ((CodePropertyReferenceExpression)expression).PropertyName;
					}
					else if (expression is CodeFieldReferenceExpression)
					{
						text2 = ((CodeFieldReferenceExpression)expression).FieldName;
					}
					else if (expression is CodeMethodReferenceExpression)
					{
						text2 = ((CodeMethodReferenceExpression)expression).MethodName;
						if (text2.StartsWith("Set"))
						{
							text2 = text2.Substring(3);
						}
					}
					else
					{
						text2 = null;
					}
					if (text == null)
					{
						text = "resource";
					}
					if (text2 != null)
					{
						text = text + "." + text2;
					}
				}
				else
				{
					text = "resource";
					flag = true;
				}
				string text3 = text;
				int num = 1;
				do
				{
					if (flag)
					{
						text3 = text + num.ToString(CultureInfo.InvariantCulture);
						num++;
					}
					else
					{
						flag = true;
					}
				}
				while (this.nameTable.ContainsKey(text3));
				this.SetValue(manager, text3, value, forceInvariant, shouldSerializeInvariant, ensureInvariant, applyingCachedResources);
				this.nameTable[text3] = text3;
				return text3;
			}

			// Token: 0x04001E55 RID: 7765
			private static object resourceSetSentinel = new object();

			// Token: 0x04001E56 RID: 7766
			private IDesignerSerializationManager manager;

			// Token: 0x04001E57 RID: 7767
			private bool checkedLocalizationLanguage;

			// Token: 0x04001E58 RID: 7768
			private CultureInfo localizationLanguage;

			// Token: 0x04001E59 RID: 7769
			private IResourceWriter writer;

			// Token: 0x04001E5A RID: 7770
			private CultureInfo readCulture;

			// Token: 0x04001E5B RID: 7771
			private Hashtable nameTable;

			// Token: 0x04001E5C RID: 7772
			private Hashtable resourceSets;

			// Token: 0x04001E5D RID: 7773
			private Hashtable metadata;

			// Token: 0x04001E5E RID: 7774
			private Hashtable mergedMetadata;

			// Token: 0x04001E5F RID: 7775
			private object rootComponent;

			// Token: 0x04001E60 RID: 7776
			private bool declarationAdded;

			// Token: 0x04001E61 RID: 7777
			private bool expressionAdded;

			// Token: 0x04001E62 RID: 7778
			private Hashtable propertyFillAdded;

			// Token: 0x04001E63 RID: 7779
			private bool invariantCultureResourcesDirty;

			// Token: 0x04001E64 RID: 7780
			private bool metadataResourcesDirty;

			// Token: 0x020005DD RID: 1501
			private class CodeDomResourceSet : ResourceSet
			{
				// Token: 0x0600347F RID: 13439 RVA: 0x0011D478 File Offset: 0x0011B678
				public CodeDomResourceSet()
				{
				}

				// Token: 0x06003480 RID: 13440 RVA: 0x0011D480 File Offset: 0x0011B680
				public CodeDomResourceSet(Hashtable resources)
				{
					this.Table = resources;
				}
			}

			// Token: 0x020005DE RID: 1502
			private enum CompareValue
			{
				// Token: 0x04002319 RID: 8985
				Same,
				// Token: 0x0400231A RID: 8986
				Different,
				// Token: 0x0400231B RID: 8987
				New
			}
		}
	}
}
