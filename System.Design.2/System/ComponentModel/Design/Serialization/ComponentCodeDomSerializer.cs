using System;
using System.CodeDom;
using System.Configuration;
using System.Reflection;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x020001DC RID: 476
	internal class ComponentCodeDomSerializer : CodeDomSerializer
	{
		// Token: 0x06001201 RID: 4609 RVA: 0x00066C68 File Offset: 0x00064E68
		private Type[] GetContainerConstructor(IDesignerSerializationManager manager)
		{
			if (this._containerConstructor == null)
			{
				this._containerConstructor = new Type[]
				{
					CodeDomSerializerBase.GetReflectionTypeFromTypeHelper(manager, typeof(IContainer))
				};
			}
			return this._containerConstructor;
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06001202 RID: 4610 RVA: 0x00066C98 File Offset: 0x00064E98
		internal new static ComponentCodeDomSerializer Default
		{
			get
			{
				ComponentCodeDomSerializer componentCodeDomSerializer;
				if (ComponentCodeDomSerializer._defaultSerializerRef != null)
				{
					componentCodeDomSerializer = (ComponentCodeDomSerializer._defaultSerializerRef.Target as ComponentCodeDomSerializer);
					if (componentCodeDomSerializer != null)
					{
						return componentCodeDomSerializer;
					}
				}
				componentCodeDomSerializer = new ComponentCodeDomSerializer();
				ComponentCodeDomSerializer._defaultSerializerRef = new WeakReference(componentCodeDomSerializer);
				return componentCodeDomSerializer;
			}
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x00066CD4 File Offset: 0x00064ED4
		private bool CanCacheComponent(IDesignerSerializationManager manager, object value, PropertyDescriptorCollection props)
		{
			IComponent component = value as IComponent;
			if (component != null)
			{
				if (component.Site != null)
				{
					INestedSite nestedSite = component.Site as INestedSite;
					if (nestedSite != null && !string.IsNullOrEmpty(nestedSite.FullName))
					{
						return false;
					}
				}
				if (props == null)
				{
					props = TypeDescriptor.GetProperties(component);
				}
				foreach (object obj in props)
				{
					PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
					if (typeof(IComponent).IsAssignableFrom(propertyDescriptor.PropertyType) && !propertyDescriptor.Attributes.Contains(DesignerSerializationVisibilityAttribute.Hidden))
					{
						MemberCodeDomSerializer memberCodeDomSerializer = (MemberCodeDomSerializer)manager.GetSerializer(propertyDescriptor.GetType(), typeof(MemberCodeDomSerializer));
						if (memberCodeDomSerializer != null && memberCodeDomSerializer.ShouldSerialize(manager, value, propertyDescriptor))
						{
							return false;
						}
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x00066DC4 File Offset: 0x00064FC4
		protected override object DeserializeInstance(IDesignerSerializationManager manager, Type type, object[] parameters, string name, bool addToContainer)
		{
			object obj = base.DeserializeInstance(manager, type, parameters, name, addToContainer);
			if (obj != null)
			{
				base.DeserializePropertiesFromResources(manager, obj, ComponentCodeDomSerializer._designTimeFilter);
			}
			return obj;
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x00066DF0 File Offset: 0x00064FF0
		public override object Serialize(IDesignerSerializationManager manager, object value)
		{
			CodeStatementCollection codeStatementCollection = null;
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(value);
			using (CodeDomSerializerBase.TraceScope("ComponentCodeDomSerializer::Serialize"))
			{
				if (manager == null || value == null)
				{
					throw new ArgumentNullException((manager == null) ? "manager" : "value");
				}
				if (base.IsSerialized(manager, value))
				{
					return base.GetExpression(manager, value);
				}
				InheritanceLevel inheritanceLevel = InheritanceLevel.NotInherited;
				InheritanceAttribute inheritanceAttribute = (InheritanceAttribute)TypeDescriptor.GetAttributes(value)[typeof(InheritanceAttribute)];
				if (inheritanceAttribute != null)
				{
					inheritanceLevel = inheritanceAttribute.InheritanceLevel;
				}
				if (inheritanceLevel != InheritanceLevel.InheritedReadOnly)
				{
					codeStatementCollection = new CodeStatementCollection();
					CodeTypeDeclaration codeTypeDeclaration = manager.Context[typeof(CodeTypeDeclaration)] as CodeTypeDeclaration;
					RootContext rootContext = manager.Context[typeof(RootContext)] as RootContext;
					CodeExpression codeExpression = null;
					bool flag = false;
					bool flag2 = true;
					bool flag3 = true;
					bool flag4 = false;
					codeExpression = base.GetExpression(manager, value);
					if (codeExpression != null)
					{
						flag = false;
						flag2 = false;
						flag3 = false;
						IComponent component = value as IComponent;
						if (component != null && component.Site == null)
						{
							ExpressionContext expressionContext = manager.Context[typeof(ExpressionContext)] as ExpressionContext;
							if (expressionContext == null || expressionContext.PresetValue != value)
							{
								flag4 = true;
							}
						}
					}
					else
					{
						if (inheritanceLevel == InheritanceLevel.NotInherited)
						{
							PropertyDescriptor propertyDescriptor = properties["GenerateMember"];
							if (propertyDescriptor != null && propertyDescriptor.PropertyType == typeof(bool) && !(bool)propertyDescriptor.GetValue(value))
							{
								flag = true;
								flag2 = false;
							}
						}
						else
						{
							flag3 = false;
						}
						if (rootContext == null)
						{
							flag = true;
							flag2 = false;
						}
					}
					manager.Context.Push(value);
					manager.Context.Push(codeStatementCollection);
					try
					{
						string name = manager.GetName(value);
						string className = TypeDescriptor.GetClassName(value);
						if ((flag2 || flag) && name != null)
						{
							if (flag2)
							{
								if (inheritanceLevel == InheritanceLevel.NotInherited)
								{
									CodeMemberField codeMemberField = new CodeMemberField(className, name);
									PropertyDescriptor propertyDescriptor2 = properties["Modifiers"];
									if (propertyDescriptor2 == null)
									{
										propertyDescriptor2 = properties["DefaultModifiers"];
									}
									MemberAttributes attributes;
									if (propertyDescriptor2 != null && propertyDescriptor2.PropertyType == typeof(MemberAttributes))
									{
										attributes = (MemberAttributes)propertyDescriptor2.GetValue(value);
									}
									else
									{
										attributes = MemberAttributes.Private;
									}
									codeMemberField.Attributes = attributes;
									codeTypeDeclaration.Members.Add(codeMemberField);
								}
								codeExpression = new CodeFieldReferenceExpression(rootContext.Expression, name);
							}
							else
							{
								if (inheritanceLevel == InheritanceLevel.NotInherited)
								{
									CodeVariableDeclarationStatement value2 = new CodeVariableDeclarationStatement(className, name);
									codeStatementCollection.Add(value2);
								}
								codeExpression = new CodeVariableReferenceExpression(name);
							}
						}
						if (flag3)
						{
							IContainer container = manager.GetService(typeof(IContainer)) as IContainer;
							ConstructorInfo left = null;
							if (container != null)
							{
								left = CodeDomSerializerBase.GetReflectionTypeHelper(manager, value).GetConstructor(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.ExactBinding, null, this.GetContainerConstructor(manager), null);
							}
							CodeExpression codeExpression2;
							if (left != null)
							{
								codeExpression2 = new CodeObjectCreateExpression(className, new CodeExpression[]
								{
									base.SerializeToExpression(manager, container)
								});
							}
							else
							{
								bool flag5;
								codeExpression2 = base.SerializeCreationExpression(manager, value, out flag5);
							}
							if (codeExpression2 != null)
							{
								if (codeExpression == null)
								{
									if (flag4)
									{
										codeExpression = codeExpression2;
									}
								}
								else
								{
									CodeAssignStatement value3 = new CodeAssignStatement(codeExpression, codeExpression2);
									codeStatementCollection.Add(value3);
								}
							}
						}
						if (codeExpression != null)
						{
							base.SetExpression(manager, value, codeExpression);
						}
						if (codeExpression != null && !flag4)
						{
							bool flag6 = value is ISupportInitialize;
							if (flag6)
							{
								string fullName = typeof(ISupportInitialize).FullName;
								flag6 = (manager.GetType(fullName) != null);
							}
							Type type = null;
							if (flag6)
							{
								type = CodeDomSerializerBase.GetReflectionTypeHelper(manager, value);
								flag6 = CodeDomSerializerBase.GetReflectionTypeFromTypeHelper(manager, typeof(ISupportInitialize)).IsAssignableFrom(type);
							}
							bool flag7 = value is IPersistComponentSettings && ((IPersistComponentSettings)value).SaveSettings;
							if (flag7)
							{
								string fullName2 = typeof(IPersistComponentSettings).FullName;
								flag7 = (manager.GetType(fullName2) != null);
							}
							if (flag7)
							{
								type = (type ?? CodeDomSerializerBase.GetReflectionTypeHelper(manager, value));
								flag7 = CodeDomSerializerBase.GetReflectionTypeFromTypeHelper(manager, typeof(IPersistComponentSettings)).IsAssignableFrom(type);
							}
							IDesignerSerializationManager designerSerializationManager = (IDesignerSerializationManager)manager.GetService(typeof(IDesignerSerializationManager));
							if (flag6)
							{
								this.SerializeSupportInitialize(manager, codeStatementCollection, codeExpression, value, "BeginInit");
							}
							base.SerializePropertiesToResources(manager, codeStatementCollection, value, ComponentCodeDomSerializer._designTimeFilter);
							ComponentCache componentCache = (ComponentCache)manager.GetService(typeof(ComponentCache));
							ComponentCache.Entry entry = null;
							if (componentCache == null)
							{
								IServiceContainer serviceContainer = (IServiceContainer)manager.GetService(typeof(IServiceContainer));
								if (serviceContainer != null)
								{
									componentCache = new ComponentCache(manager);
									serviceContainer.AddService(typeof(ComponentCache), componentCache);
								}
							}
							else if (manager == designerSerializationManager && componentCache != null && componentCache.Enabled)
							{
								entry = componentCache[value];
							}
							if (entry == null || entry.Tracking)
							{
								if (entry == null)
								{
									entry = new ComponentCache.Entry(componentCache);
									ComponentCache.Entry entryAll = componentCache.GetEntryAll(value);
									if (entryAll != null && entryAll.Dependencies != null && entryAll.Dependencies.Count > 0)
									{
										foreach (object dep in entryAll.Dependencies)
										{
											entry.AddDependency(dep);
										}
									}
								}
								entry.Component = value;
								bool flag8 = manager == designerSerializationManager;
								entry.Valid = (flag8 && this.CanCacheComponent(manager, value, properties));
								if (flag8 && componentCache != null && componentCache.Enabled)
								{
									manager.Context.Push(componentCache);
									manager.Context.Push(entry);
								}
								try
								{
									entry.Statements = new CodeStatementCollection();
									base.SerializeProperties(manager, entry.Statements, value, ComponentCodeDomSerializer._runTimeFilter);
									base.SerializeEvents(manager, entry.Statements, value, null);
									foreach (object obj in entry.Statements)
									{
										CodeStatement codeStatement = (CodeStatement)obj;
										CodeVariableDeclarationStatement codeVariableDeclarationStatement = codeStatement as CodeVariableDeclarationStatement;
										if (codeVariableDeclarationStatement != null)
										{
											entry.Tracking = true;
											break;
										}
									}
									if (entry.Statements.Count > 0)
									{
										entry.Statements.Insert(0, new CodeCommentStatement(string.Empty));
										entry.Statements.Insert(0, new CodeCommentStatement(name));
										entry.Statements.Insert(0, new CodeCommentStatement(string.Empty));
										if (flag8 && componentCache != null && componentCache.Enabled)
										{
											componentCache[value] = entry;
										}
									}
									goto IL_6A8;
								}
								finally
								{
									if (flag8 && componentCache != null && componentCache.Enabled)
									{
										manager.Context.Pop();
										manager.Context.Pop();
									}
								}
							}
							if ((entry.Resources != null || entry.Metadata != null) && componentCache != null && componentCache.Enabled)
							{
								ResourceCodeDomSerializer @default = ResourceCodeDomSerializer.Default;
								@default.ApplyCacheEntry(manager, entry);
							}
							IL_6A8:
							codeStatementCollection.AddRange(entry.Statements);
							if (flag7)
							{
								this.SerializeLoadComponentSettings(manager, codeStatementCollection, codeExpression, value);
							}
							if (flag6)
							{
								this.SerializeSupportInitialize(manager, codeStatementCollection, codeExpression, value, "EndInit");
							}
						}
					}
					catch (CheckoutException)
					{
						throw;
					}
					catch (Exception errorInformation)
					{
						manager.ReportError(errorInformation);
					}
					finally
					{
						manager.Context.Pop();
						manager.Context.Pop();
					}
				}
			}
			return codeStatementCollection;
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x000675BC File Offset: 0x000657BC
		private void SerializeLoadComponentSettings(IDesignerSerializationManager manager, CodeStatementCollection statements, CodeExpression valueExpression, object value)
		{
			CodeTypeReference targetType = new CodeTypeReference(typeof(IPersistComponentSettings));
			CodeCastExpression targetObject = new CodeCastExpression(targetType, valueExpression);
			CodeMethodReferenceExpression method = new CodeMethodReferenceExpression(targetObject, "LoadComponentSettings");
			CodeExpressionStatement codeExpressionStatement = new CodeExpressionStatement(new CodeMethodInvokeExpression
			{
				Method = method
			});
			codeExpressionStatement.UserData["statement-ordering"] = "end";
			statements.Add(codeExpressionStatement);
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x00067624 File Offset: 0x00065824
		private void SerializeSupportInitialize(IDesignerSerializationManager manager, CodeStatementCollection statements, CodeExpression valueExpression, object value, string methodName)
		{
			CodeTypeReference targetType = new CodeTypeReference(typeof(ISupportInitialize));
			CodeCastExpression targetObject = new CodeCastExpression(targetType, valueExpression);
			CodeMethodReferenceExpression method = new CodeMethodReferenceExpression(targetObject, methodName);
			CodeExpressionStatement codeExpressionStatement = new CodeExpressionStatement(new CodeMethodInvokeExpression
			{
				Method = method
			});
			if (methodName == "BeginInit")
			{
				codeExpressionStatement.UserData["statement-ordering"] = "begin";
			}
			else
			{
				codeExpressionStatement.UserData["statement-ordering"] = "end";
			}
			statements.Add(codeExpressionStatement);
		}

		// Token: 0x040009E8 RID: 2536
		private Type[] _containerConstructor;

		// Token: 0x040009E9 RID: 2537
		private static readonly Attribute[] _runTimeFilter = new Attribute[]
		{
			DesignOnlyAttribute.No
		};

		// Token: 0x040009EA RID: 2538
		private static readonly Attribute[] _designTimeFilter = new Attribute[]
		{
			DesignOnlyAttribute.Yes
		};

		// Token: 0x040009EB RID: 2539
		private static WeakReference _defaultSerializerRef;
	}
}
