using System;
using System.CodeDom;
using System.Collections;
using System.Design;
using System.Globalization;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x020001D6 RID: 470
	[DefaultSerializationProvider(typeof(CodeDomSerializationProvider))]
	public class CodeDomSerializer : CodeDomSerializerBase
	{
		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x0600118C RID: 4492 RVA: 0x000610C3 File Offset: 0x0005F2C3
		internal static CodeDomSerializer Default
		{
			get
			{
				if (CodeDomSerializer._default == null)
				{
					CodeDomSerializer._default = new CodeDomSerializer();
				}
				return CodeDomSerializer._default;
			}
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x000610DC File Offset: 0x0005F2DC
		public virtual string GetTargetComponentName(CodeStatement statement, CodeExpression expression, Type targetType)
		{
			string result = null;
			CodeVariableReferenceExpression codeVariableReferenceExpression;
			CodeFieldReferenceExpression codeFieldReferenceExpression;
			if ((codeVariableReferenceExpression = (expression as CodeVariableReferenceExpression)) != null)
			{
				result = codeVariableReferenceExpression.VariableName;
			}
			else if ((codeFieldReferenceExpression = (expression as CodeFieldReferenceExpression)) != null)
			{
				result = codeFieldReferenceExpression.FieldName;
			}
			return result;
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x00061110 File Offset: 0x0005F310
		public virtual object Deserialize(IDesignerSerializationManager manager, object codeObject)
		{
			object obj = null;
			if (manager == null || codeObject == null)
			{
				throw new ArgumentNullException((manager == null) ? "manager" : "codeObject");
			}
			using (CodeDomSerializerBase.TraceScope("CodeDomSerializer::Deserialize"))
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
								CodeStatement statement = (CodeStatement)obj2;
								if (obj == null)
								{
									obj = this.DeserializeStatementToInstance(manager, statement);
									if (obj == null)
									{
										continue;
									}
									PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(obj, new Attribute[]
									{
										BrowsableAttribute.Yes
									});
									using (IEnumerator enumerator2 = properties.GetEnumerator())
									{
										while (enumerator2.MoveNext())
										{
											object obj3 = enumerator2.Current;
											PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj3;
											if (!propertyDescriptor.Attributes.Contains(DesignerSerializationVisibilityAttribute.Hidden) && propertyDescriptor.Attributes.Contains(DesignerSerializationVisibilityAttribute.Content) && !(manager.GetSerializer(propertyDescriptor.PropertyType, typeof(CodeDomSerializer)) is CollectionCodeDomSerializer))
											{
												this.ResetBrowsableProperties(propertyDescriptor.GetValue(obj));
											}
										}
										continue;
									}
								}
								base.DeserializeStatement(manager, statement);
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

		// Token: 0x0600118F RID: 4495 RVA: 0x00061348 File Offset: 0x0005F548
		protected object DeserializeStatementToInstance(IDesignerSerializationManager manager, CodeStatement statement)
		{
			object result = null;
			CodeAssignStatement codeAssignStatement;
			CodeVariableDeclarationStatement codeVariableDeclarationStatement;
			if ((codeAssignStatement = (statement as CodeAssignStatement)) != null)
			{
				CodeFieldReferenceExpression codeFieldReferenceExpression = codeAssignStatement.Left as CodeFieldReferenceExpression;
				if (codeFieldReferenceExpression != null)
				{
					result = base.DeserializeExpression(manager, codeFieldReferenceExpression.FieldName, codeAssignStatement.Right);
				}
				else
				{
					CodeVariableReferenceExpression codeVariableReferenceExpression = codeAssignStatement.Left as CodeVariableReferenceExpression;
					if (codeVariableReferenceExpression != null)
					{
						result = base.DeserializeExpression(manager, codeVariableReferenceExpression.VariableName, codeAssignStatement.Right);
					}
					else
					{
						base.DeserializeStatement(manager, codeAssignStatement);
					}
				}
			}
			else if ((codeVariableDeclarationStatement = (statement as CodeVariableDeclarationStatement)) != null && codeVariableDeclarationStatement.InitExpression != null)
			{
				result = base.DeserializeExpression(manager, codeVariableDeclarationStatement.Name, codeVariableDeclarationStatement.InitExpression);
			}
			else
			{
				base.DeserializeStatement(manager, statement);
			}
			return result;
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x000613EC File Offset: 0x0005F5EC
		public virtual object Serialize(IDesignerSerializationManager manager, object value)
		{
			object result = null;
			if (manager == null || value == null)
			{
				throw new ArgumentNullException((manager == null) ? "manager" : "value");
			}
			using (CodeDomSerializerBase.TraceScope("CodeDomSerializer::Serialize"))
			{
				if (value is Type)
				{
					result = new CodeTypeOfExpression((Type)value);
				}
				else
				{
					bool flag = false;
					bool flag2;
					CodeExpression codeExpression = base.SerializeCreationExpression(manager, value, out flag2);
					if (!(value is IComponent))
					{
						flag = flag2;
					}
					ExpressionContext expressionContext = manager.Context[typeof(ExpressionContext)] as ExpressionContext;
					bool flag3 = expressionContext != null && expressionContext.PresetValue == value;
					if (codeExpression != null)
					{
						if (flag)
						{
							result = codeExpression;
						}
						else
						{
							CodeStatementCollection codeStatementCollection = new CodeStatementCollection();
							if (flag3)
							{
								base.SetExpression(manager, value, codeExpression, true);
							}
							else
							{
								string uniqueName = base.GetUniqueName(manager, value);
								string className = TypeDescriptor.GetClassName(value);
								codeStatementCollection.Add(new CodeVariableDeclarationStatement(className, uniqueName)
								{
									InitExpression = codeExpression
								});
								CodeExpression expression = new CodeVariableReferenceExpression(uniqueName);
								base.SetExpression(manager, value, expression);
							}
							base.SerializePropertiesToResources(manager, codeStatementCollection, value, CodeDomSerializer._designTimeFilter);
							base.SerializeProperties(manager, codeStatementCollection, value, CodeDomSerializer._runTimeFilter);
							base.SerializeEvents(manager, codeStatementCollection, value, CodeDomSerializer._runTimeFilter);
							result = codeStatementCollection;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x0006154C File Offset: 0x0005F74C
		public virtual object SerializeAbsolute(IDesignerSerializationManager manager, object value)
		{
			SerializeAbsoluteContext context = new SerializeAbsoluteContext();
			manager.Context.Push(context);
			object result;
			try
			{
				result = this.Serialize(manager, value);
			}
			finally
			{
				manager.Context.Pop();
			}
			return result;
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x00061594 File Offset: 0x0005F794
		public virtual CodeStatementCollection SerializeMember(IDesignerSerializationManager manager, object owningObject, MemberDescriptor member)
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			if (owningObject == null)
			{
				throw new ArgumentNullException("owningObject");
			}
			if (member == null)
			{
				throw new ArgumentNullException("member");
			}
			CodeStatementCollection codeStatementCollection = new CodeStatementCollection();
			if (base.GetExpression(manager, owningObject) == null)
			{
				string uniqueName = base.GetUniqueName(manager, owningObject);
				CodeExpression expression = new CodeVariableReferenceExpression(uniqueName);
				base.SetExpression(manager, owningObject, expression);
			}
			PropertyDescriptor propertyDescriptor = member as PropertyDescriptor;
			if (propertyDescriptor != null)
			{
				base.SerializeProperty(manager, codeStatementCollection, owningObject, propertyDescriptor);
			}
			else
			{
				EventDescriptor eventDescriptor = member as EventDescriptor;
				if (eventDescriptor == null)
				{
					throw new NotSupportedException(SR.GetString("SerializerMemberTypeNotSerializable", new object[]
					{
						member.GetType().FullName
					}));
				}
				base.SerializeEvent(manager, codeStatementCollection, owningObject, eventDescriptor);
			}
			return codeStatementCollection;
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x0006164C File Offset: 0x0005F84C
		public virtual CodeStatementCollection SerializeMemberAbsolute(IDesignerSerializationManager manager, object owningObject, MemberDescriptor member)
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			if (owningObject == null)
			{
				throw new ArgumentNullException("owningObject");
			}
			if (member == null)
			{
				throw new ArgumentNullException("member");
			}
			SerializeAbsoluteContext context = new SerializeAbsoluteContext(member);
			manager.Context.Push(context);
			CodeStatementCollection result;
			try
			{
				result = this.SerializeMember(manager, owningObject, member);
			}
			finally
			{
				manager.Context.Pop();
			}
			return result;
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x000616C0 File Offset: 0x0005F8C0
		[Obsolete("This method has been deprecated. Use SerializeToExpression or GetExpression instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		protected CodeExpression SerializeToReferenceExpression(IDesignerSerializationManager manager, object value)
		{
			CodeExpression codeExpression = null;
			using (CodeDomSerializerBase.TraceScope("CodeDomSerializer::SerializeToReferenceExpression"))
			{
				codeExpression = base.GetExpression(manager, value);
				if (codeExpression == null && value is IComponent)
				{
					string name = manager.GetName(value);
					bool flag = false;
					if (name == null)
					{
						IReferenceService referenceService = (IReferenceService)manager.GetService(typeof(IReferenceService));
						if (referenceService != null)
						{
							name = referenceService.GetName(value);
							flag = (name != null);
						}
					}
					if (name != null)
					{
						RootContext rootContext = (RootContext)manager.Context[typeof(RootContext)];
						if (rootContext != null && rootContext.Value == value)
						{
							codeExpression = rootContext.Expression;
						}
						else if (flag && name.IndexOf('.') != -1)
						{
							int num = name.IndexOf('.');
							codeExpression = new CodePropertyReferenceExpression(new CodeFieldReferenceExpression(CodeDomSerializer._thisRef, name.Substring(0, num)), name.Substring(num + 1));
						}
						else
						{
							codeExpression = new CodeFieldReferenceExpression(CodeDomSerializer._thisRef, name);
						}
					}
				}
			}
			return codeExpression;
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x000617C8 File Offset: 0x0005F9C8
		private void ResetBrowsableProperties(object instance)
		{
			if (instance == null)
			{
				return;
			}
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(instance, new Attribute[]
			{
				BrowsableAttribute.Yes
			});
			foreach (object obj in properties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (!propertyDescriptor.Attributes.Contains(DesignerSerializationVisibilityAttribute.Hidden))
				{
					if (propertyDescriptor.CanResetValue(instance))
					{
						try
						{
							propertyDescriptor.ResetValue(instance);
							continue;
						}
						catch (ArgumentException ex)
						{
							continue;
						}
					}
					if (propertyDescriptor.Attributes.Contains(DesignerSerializationVisibilityAttribute.Content))
					{
						this.ResetBrowsableProperties(propertyDescriptor.GetValue(instance));
					}
				}
			}
		}

		// Token: 0x040009D2 RID: 2514
		private static CodeDomSerializer _default;

		// Token: 0x040009D3 RID: 2515
		private static readonly Attribute[] _runTimeFilter = new Attribute[]
		{
			DesignOnlyAttribute.No
		};

		// Token: 0x040009D4 RID: 2516
		private static readonly Attribute[] _designTimeFilter = new Attribute[]
		{
			DesignOnlyAttribute.Yes
		};

		// Token: 0x040009D5 RID: 2517
		private static CodeThisReferenceExpression _thisRef = new CodeThisReferenceExpression();
	}
}
