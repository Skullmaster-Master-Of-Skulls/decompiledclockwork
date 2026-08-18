using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Design;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.CSharp;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x020001D7 RID: 471
	[EditorBrowsable(EditorBrowsableState.Never)]
	public abstract class CodeDomSerializerBase
	{
		// Token: 0x06001198 RID: 4504 RVA: 0x0000362F File Offset: 0x0000182F
		internal CodeDomSerializerBase()
		{
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x000618BE File Offset: 0x0005FABE
		protected virtual object DeserializeInstance(IDesignerSerializationManager manager, Type type, object[] parameters, string name, bool addToContainer)
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return manager.CreateInstance(type, parameters, name, addToContainer);
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x000618EE File Offset: 0x0005FAEE
		internal static string GetTypeNameFromCodeTypeReference(IDesignerSerializationManager manager, CodeTypeReference typeref)
		{
			if (typeref.TypeArguments == null || typeref.TypeArguments.Count == 0)
			{
				return typeref.BaseType;
			}
			return CodeDomSerializerBase.GetTypeNameFromCodeTypeReferenceHelper(manager, typeref);
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x00061914 File Offset: 0x0005FB14
		protected static TypeDescriptionProvider GetTargetFrameworkProvider(IServiceProvider provider, object instance)
		{
			TypeDescriptionProviderService typeDescriptionProviderService = provider.GetService(typeof(TypeDescriptionProviderService)) as TypeDescriptionProviderService;
			if (typeDescriptionProviderService != null)
			{
				return typeDescriptionProviderService.GetProvider(instance);
			}
			return null;
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x00061944 File Offset: 0x0005FB44
		private static TypeDescriptionProvider GetTargetFrameworkProviderForType(IServiceProvider provider, Type type)
		{
			TypeDescriptionProviderService typeDescriptionProviderService = provider.GetService(typeof(TypeDescriptionProviderService)) as TypeDescriptionProviderService;
			if (typeDescriptionProviderService != null)
			{
				return typeDescriptionProviderService.GetProvider(type);
			}
			return null;
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x00061974 File Offset: 0x0005FB74
		protected static Type GetReflectionTypeFromTypeHelper(IDesignerSerializationManager manager, Type type)
		{
			if (type == null || manager == null)
			{
				return null;
			}
			TypeDescriptionProvider targetFrameworkProviderForType = CodeDomSerializerBase.GetTargetFrameworkProviderForType(manager, type);
			if (targetFrameworkProviderForType != null)
			{
				if (targetFrameworkProviderForType.IsSupportedType(type))
				{
					return targetFrameworkProviderForType.GetReflectionType(type);
				}
				CodeDomSerializerBase.Error(manager, SR.GetString("TypeNotFoundInTargetFramework", new object[]
				{
					type.FullName
				}), "SerializerUndeclaredName");
			}
			return TypeDescriptor.GetReflectionType(type);
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x000619D8 File Offset: 0x0005FBD8
		protected static Type GetReflectionTypeHelper(IDesignerSerializationManager manager, object instance)
		{
			if (instance == null || manager == null)
			{
				return null;
			}
			Type type = instance.GetType();
			if (type.IsValueType)
			{
				TypeDescriptionProvider targetFrameworkProvider = CodeDomSerializerBase.GetTargetFrameworkProvider(manager, instance);
				if (targetFrameworkProvider != null)
				{
					if (targetFrameworkProvider.IsSupportedType(type))
					{
						return targetFrameworkProvider.GetReflectionType(instance);
					}
					CodeDomSerializerBase.Error(manager, SR.GetString("TypeNotFoundInTargetFramework", new object[]
					{
						instance.GetType().FullName
					}), "SerializerUndeclaredName");
				}
			}
			return TypeDescriptor.GetReflectionType(instance);
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x00061A48 File Offset: 0x0005FC48
		protected static PropertyDescriptorCollection GetPropertiesHelper(IDesignerSerializationManager manager, object instance, Attribute[] attributes)
		{
			if (instance == null || manager == null)
			{
				return null;
			}
			if (instance.GetType().IsValueType)
			{
				TypeDescriptionProvider targetFrameworkProvider = CodeDomSerializerBase.GetTargetFrameworkProvider(manager, instance);
				if (targetFrameworkProvider != null)
				{
					if (targetFrameworkProvider.IsSupportedType(instance.GetType()))
					{
						ICustomTypeDescriptor typeDescriptor = targetFrameworkProvider.GetTypeDescriptor(instance);
						if (typeDescriptor != null)
						{
							if (attributes == null)
							{
								return typeDescriptor.GetProperties();
							}
							return typeDescriptor.GetProperties(attributes);
						}
					}
					else
					{
						CodeDomSerializerBase.Error(manager, SR.GetString("TypeNotFoundInTargetFramework", new object[]
						{
							instance.GetType().FullName
						}), "SerializerUndeclaredName");
					}
				}
			}
			if (attributes == null)
			{
				return TypeDescriptor.GetProperties(instance);
			}
			return TypeDescriptor.GetProperties(instance, attributes);
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x00061ADC File Offset: 0x0005FCDC
		protected static EventDescriptorCollection GetEventsHelper(IDesignerSerializationManager manager, object instance, Attribute[] attributes)
		{
			if (instance == null || manager == null)
			{
				return null;
			}
			if (instance.GetType().IsValueType)
			{
				TypeDescriptionProvider targetFrameworkProvider = CodeDomSerializerBase.GetTargetFrameworkProvider(manager, instance);
				if (targetFrameworkProvider != null)
				{
					if (targetFrameworkProvider.IsSupportedType(instance.GetType()))
					{
						ICustomTypeDescriptor typeDescriptor = targetFrameworkProvider.GetTypeDescriptor(instance);
						if (typeDescriptor != null)
						{
							if (attributes == null)
							{
								return typeDescriptor.GetEvents();
							}
							return typeDescriptor.GetEvents(attributes);
						}
					}
					else
					{
						CodeDomSerializerBase.Error(manager, SR.GetString("TypeNotFoundInTargetFramework", new object[]
						{
							instance.GetType().FullName
						}), "SerializerUndeclaredName");
					}
				}
			}
			if (attributes == null)
			{
				return TypeDescriptor.GetEvents(instance);
			}
			return TypeDescriptor.GetEvents(instance, attributes);
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x00061B70 File Offset: 0x0005FD70
		protected static AttributeCollection GetAttributesHelper(IDesignerSerializationManager manager, object instance)
		{
			if (instance == null || manager == null)
			{
				return null;
			}
			if (instance.GetType().IsValueType)
			{
				TypeDescriptionProvider targetFrameworkProvider = CodeDomSerializerBase.GetTargetFrameworkProvider(manager, instance);
				if (targetFrameworkProvider != null)
				{
					if (targetFrameworkProvider.IsSupportedType(instance.GetType()))
					{
						ICustomTypeDescriptor typeDescriptor = targetFrameworkProvider.GetTypeDescriptor(instance);
						if (typeDescriptor != null)
						{
							return typeDescriptor.GetAttributes();
						}
					}
					else
					{
						CodeDomSerializerBase.Error(manager, SR.GetString("TypeNotFoundInTargetFramework", new object[]
						{
							instance.GetType().FullName
						}), "SerializerUndeclaredName");
					}
				}
			}
			return TypeDescriptor.GetAttributes(instance);
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x00061BEC File Offset: 0x0005FDEC
		protected static AttributeCollection GetAttributesFromTypeHelper(IDesignerSerializationManager manager, Type type)
		{
			if (type == null || manager == null)
			{
				return null;
			}
			if (type.IsValueType)
			{
				TypeDescriptionProvider targetFrameworkProviderForType = CodeDomSerializerBase.GetTargetFrameworkProviderForType(manager, type);
				if (targetFrameworkProviderForType != null)
				{
					if (targetFrameworkProviderForType.IsSupportedType(type))
					{
						ICustomTypeDescriptor typeDescriptor = targetFrameworkProviderForType.GetTypeDescriptor(type);
						if (typeDescriptor != null)
						{
							return typeDescriptor.GetAttributes();
						}
					}
					else
					{
						CodeDomSerializerBase.Error(manager, SR.GetString("TypeNotFoundInTargetFramework", new object[]
						{
							type.FullName
						}), "SerializerUndeclaredName");
					}
				}
			}
			return TypeDescriptor.GetAttributes(type);
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x00061C60 File Offset: 0x0005FE60
		private static string GetTypeNameFromCodeTypeReferenceHelper(IDesignerSerializationManager manager, CodeTypeReference typeref)
		{
			if (typeref.TypeArguments != null && typeref.TypeArguments.Count != 0)
			{
				StringBuilder stringBuilder = new StringBuilder(typeref.BaseType);
				if (!typeref.BaseType.Contains("`"))
				{
					stringBuilder.Append("`");
					stringBuilder.Append(typeref.TypeArguments.Count);
				}
				stringBuilder.Append("[");
				bool flag = true;
				foreach (object obj in typeref.TypeArguments)
				{
					CodeTypeReference typeref2 = (CodeTypeReference)obj;
					if (!flag)
					{
						stringBuilder.Append(",");
					}
					stringBuilder.Append("[");
					stringBuilder.Append(CodeDomSerializerBase.GetTypeNameFromCodeTypeReferenceHelper(manager, typeref2));
					stringBuilder.Append("]");
					flag = false;
				}
				stringBuilder.Append("]");
				return stringBuilder.ToString();
			}
			Type type = manager.GetType(typeref.BaseType);
			if (type != null)
			{
				return CodeDomSerializerBase.GetReflectionTypeFromTypeHelper(manager, type).AssemblyQualifiedName;
			}
			return typeref.BaseType;
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x00061D8C File Offset: 0x0005FF8C
		private object DeserializePropertyReferenceExpression(IDesignerSerializationManager manager, CodePropertyReferenceExpression propertyReferenceEx, bool reportError)
		{
			object obj = propertyReferenceEx;
			object obj2 = this.DeserializeExpression(manager, null, propertyReferenceEx.TargetObject);
			if (obj2 != null && !(obj2 is CodeExpression))
			{
				if (!(obj2 is Type))
				{
					PropertyDescriptor propertyDescriptor = CodeDomSerializerBase.GetPropertiesHelper(manager, obj2, null)[propertyReferenceEx.PropertyName];
					if (propertyDescriptor != null)
					{
						obj = propertyDescriptor.GetValue(obj2);
					}
					else if (this.GetExpression(manager, obj2) is CodeThisReferenceExpression)
					{
						PropertyInfo property = CodeDomSerializerBase.GetReflectionTypeHelper(manager, obj2).GetProperty(propertyReferenceEx.PropertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty);
						if (property != null)
						{
							obj = property.GetValue(obj2, null);
						}
					}
				}
				else
				{
					PropertyInfo property2 = CodeDomSerializerBase.GetReflectionTypeFromTypeHelper(manager, (Type)obj2).GetProperty(propertyReferenceEx.PropertyName, BindingFlags.Static | BindingFlags.Public | BindingFlags.GetProperty);
					if (property2 != null)
					{
						obj = property2.GetValue(null, null);
					}
				}
				if (obj == propertyReferenceEx && reportError)
				{
					string text = (obj2 is Type) ? ((Type)obj2).FullName : CodeDomSerializerBase.GetReflectionTypeHelper(manager, obj2).FullName;
					CodeDomSerializerBase.Error(manager, SR.GetString("SerializerNoSuchProperty", new object[]
					{
						text,
						propertyReferenceEx.PropertyName
					}), "SerializerNoSuchProperty");
				}
			}
			return obj;
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x00061EA8 File Offset: 0x000600A8
		protected object DeserializeExpression(IDesignerSerializationManager manager, string name, CodeExpression expression)
		{
			object obj = expression;
			using (CodeDomSerializerBase.TraceScope("CodeDomSerializerBase::DeserializeExpression"))
			{
				if (obj != null)
				{
					CodePrimitiveExpression codePrimitiveExpression;
					CodePropertyReferenceExpression propertyReferenceEx;
					CodeTypeReferenceExpression codeTypeReferenceExpression;
					CodeObjectCreateExpression codeObjectCreateExpression;
					CodeArgumentReferenceExpression codeArgumentReferenceExpression;
					CodeFieldReferenceExpression codeFieldReferenceExpression;
					CodeMethodInvokeExpression codeMethodInvokeExpression;
					CodeVariableReferenceExpression codeVariableReferenceExpression;
					CodeCastExpression codeCastExpression2;
					CodeArrayCreateExpression codeArrayCreateExpression;
					CodeArrayIndexerExpression codeArrayIndexerExpression;
					CodeBinaryOperatorExpression codeBinaryOperatorExpression;
					CodeDelegateInvokeExpression codeDelegateInvokeExpression;
					CodeDirectionExpression codeDirectionExpression;
					CodeIndexerExpression codeIndexerExpression;
					CodeParameterDeclarationExpression codeParameterDeclarationExpression;
					CodeTypeOfExpression codeTypeOfExpression;
					if ((codePrimitiveExpression = (obj as CodePrimitiveExpression)) != null)
					{
						obj = codePrimitiveExpression.Value;
					}
					else if ((propertyReferenceEx = (obj as CodePropertyReferenceExpression)) != null)
					{
						obj = this.DeserializePropertyReferenceExpression(manager, propertyReferenceEx, true);
					}
					else if (obj is CodeThisReferenceExpression)
					{
						RootContext rootContext = (RootContext)manager.Context[typeof(RootContext)];
						if (rootContext != null)
						{
							obj = rootContext.Value;
						}
						else
						{
							IDesignerHost designerHost = manager.GetService(typeof(IDesignerHost)) as IDesignerHost;
							if (designerHost != null)
							{
								obj = designerHost.RootComponent;
							}
						}
						if (obj == null)
						{
							CodeDomSerializerBase.Error(manager, SR.GetString("SerializerNoRootExpression"), "SerializerNoRootExpression");
						}
					}
					else if ((codeTypeReferenceExpression = (obj as CodeTypeReferenceExpression)) != null)
					{
						obj = manager.GetType(CodeDomSerializerBase.GetTypeNameFromCodeTypeReference(manager, codeTypeReferenceExpression.Type));
					}
					else if ((codeObjectCreateExpression = (obj as CodeObjectCreateExpression)) != null)
					{
						obj = null;
						Type type = manager.GetType(CodeDomSerializerBase.GetTypeNameFromCodeTypeReference(manager, codeObjectCreateExpression.CreateType));
						if (type != null)
						{
							object[] array = new object[codeObjectCreateExpression.Parameters.Count];
							bool flag = true;
							for (int i = 0; i < array.Length; i++)
							{
								array[i] = this.DeserializeExpression(manager, null, codeObjectCreateExpression.Parameters[i]);
								if (array[i] is CodeExpression)
								{
									if (typeof(Delegate).IsAssignableFrom(type) && array.Length == 1 && array[i] is CodeMethodReferenceExpression)
									{
										CodeMethodReferenceExpression codeMethodReferenceExpression = (CodeMethodReferenceExpression)array[i];
										if (!(codeMethodReferenceExpression.TargetObject is CodeThisReferenceExpression))
										{
											object obj2 = this.DeserializeExpression(manager, null, codeMethodReferenceExpression.TargetObject);
											if (!(obj2 is CodeExpression))
											{
												MethodInfo method = type.GetMethod("Invoke");
												if (method != null)
												{
													ParameterInfo[] parameters = method.GetParameters();
													Type[] array2 = new Type[parameters.Length];
													for (int j = 0; j < array2.Length; j++)
													{
														array2[j] = parameters[i].ParameterType;
													}
													MethodInfo method2 = CodeDomSerializerBase.GetReflectionTypeHelper(manager, obj2).GetMethod(codeMethodReferenceExpression.MethodName, array2);
													if (method2 != null)
													{
														method2 = obj2.GetType().GetMethod(codeMethodReferenceExpression.MethodName, array2);
														obj = Activator.CreateInstance(type, new object[]
														{
															obj2,
															method2.MethodHandle.GetFunctionPointer()
														});
													}
												}
											}
										}
									}
									flag = false;
									break;
								}
							}
							if (flag)
							{
								obj = this.DeserializeInstance(manager, type, array, name, name != null);
							}
						}
						else
						{
							CodeDomSerializerBase.Error(manager, SR.GetString("SerializerTypeNotFound", new object[]
							{
								codeObjectCreateExpression.CreateType.BaseType
							}), "SerializerTypeNotFound");
						}
					}
					else if ((codeArgumentReferenceExpression = (obj as CodeArgumentReferenceExpression)) != null)
					{
						obj = manager.GetInstance(codeArgumentReferenceExpression.ParameterName);
						if (obj == null)
						{
							CodeDomSerializerBase.Error(manager, SR.GetString("SerializerUndeclaredName", new object[]
							{
								codeArgumentReferenceExpression.ParameterName
							}), "SerializerUndeclaredName");
						}
					}
					else if ((codeFieldReferenceExpression = (obj as CodeFieldReferenceExpression)) != null)
					{
						object obj3 = this.DeserializeExpression(manager, null, codeFieldReferenceExpression.TargetObject);
						if (obj3 != null && !(obj3 is CodeExpression))
						{
							RootContext rootContext2 = (RootContext)manager.Context[typeof(RootContext)];
							if (rootContext2 != null && rootContext2.Value == obj3)
							{
								object instance = manager.GetInstance(codeFieldReferenceExpression.FieldName);
								if (instance != null)
								{
									obj = instance;
								}
								else
								{
									CodeDomSerializerBase.Error(manager, SR.GetString("SerializerUndeclaredName", new object[]
									{
										codeFieldReferenceExpression.FieldName
									}), "SerializerUndeclaredName");
								}
							}
							else
							{
								Type type2 = obj3 as Type;
								object obj4;
								FieldInfo field;
								if (type2 != null)
								{
									obj4 = null;
									field = CodeDomSerializerBase.GetReflectionTypeFromTypeHelper(manager, type2).GetField(codeFieldReferenceExpression.FieldName, BindingFlags.Static | BindingFlags.Public | BindingFlags.GetField);
								}
								else
								{
									obj4 = obj3;
									field = CodeDomSerializerBase.GetReflectionTypeHelper(manager, obj3).GetField(codeFieldReferenceExpression.FieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetField);
								}
								if (field != null)
								{
									obj = field.GetValue(obj4);
								}
								else
								{
									obj = this.DeserializePropertyReferenceExpression(manager, new CodePropertyReferenceExpression
									{
										TargetObject = codeFieldReferenceExpression.TargetObject,
										PropertyName = codeFieldReferenceExpression.FieldName
									}, false);
									if (obj == codeFieldReferenceExpression)
									{
										CodeDomSerializerBase.Error(manager, SR.GetString("SerializerUndeclaredName", new object[]
										{
											codeFieldReferenceExpression.FieldName
										}), "SerializerUndeclaredName");
									}
								}
							}
						}
						else
						{
							CodeDomSerializerBase.Error(manager, SR.GetString("SerializerFieldTargetEvalFailed", new object[]
							{
								codeFieldReferenceExpression.FieldName
							}), "SerializerFieldTargetEvalFailed");
						}
					}
					else if ((codeMethodInvokeExpression = (obj as CodeMethodInvokeExpression)) != null)
					{
						object obj5 = this.DeserializeExpression(manager, null, codeMethodInvokeExpression.Method.TargetObject);
						if (obj5 != null)
						{
							object[] array3 = new object[codeMethodInvokeExpression.Parameters.Count];
							bool flag2 = true;
							for (int k = 0; k < array3.Length; k++)
							{
								array3[k] = this.DeserializeExpression(manager, null, codeMethodInvokeExpression.Parameters[k]);
								if (array3[k] is CodeExpression)
								{
									flag2 = false;
									break;
								}
							}
							if (flag2)
							{
								IComponentChangeService componentChangeService = (IComponentChangeService)manager.GetService(typeof(IComponentChangeService));
								Type type3 = obj5 as Type;
								if (type3 != null)
								{
									obj = CodeDomSerializerBase.GetReflectionTypeFromTypeHelper(manager, type3).InvokeMember(codeMethodInvokeExpression.Method.MethodName, BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod, null, null, array3, null, null, null);
								}
								else
								{
									if (componentChangeService != null)
									{
										componentChangeService.OnComponentChanging(obj5, null);
									}
									try
									{
										obj = CodeDomSerializerBase.GetReflectionTypeHelper(manager, obj5).InvokeMember(codeMethodInvokeExpression.Method.MethodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod, null, obj5, array3, null, null, null);
									}
									catch (MissingMethodException)
									{
										CodeCastExpression codeCastExpression = codeMethodInvokeExpression.Method.TargetObject as CodeCastExpression;
										if (codeCastExpression == null)
										{
											throw;
										}
										Type type4 = manager.GetType(CodeDomSerializerBase.GetTypeNameFromCodeTypeReference(manager, codeCastExpression.TargetType));
										if (!(type4 != null) || !type4.IsInterface)
										{
											throw;
										}
										obj = CodeDomSerializerBase.GetReflectionTypeFromTypeHelper(manager, type4).InvokeMember(codeMethodInvokeExpression.Method.MethodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, obj5, array3, null, null, null);
									}
									if (componentChangeService != null)
									{
										componentChangeService.OnComponentChanged(obj5, null, null, null);
									}
								}
							}
							else if (array3.Length == 1 && array3[0] is CodeDelegateCreateExpression)
							{
								string text = codeMethodInvokeExpression.Method.MethodName;
								if (text.StartsWith("add_"))
								{
									text = text.Substring(4);
									this.DeserializeAttachEventStatement(manager, new CodeAttachEventStatement(codeMethodInvokeExpression.Method.TargetObject, text, (CodeExpression)array3[0]));
									obj = null;
								}
							}
						}
					}
					else if ((codeVariableReferenceExpression = (obj as CodeVariableReferenceExpression)) != null)
					{
						obj = manager.GetInstance(codeVariableReferenceExpression.VariableName);
						if (obj == null)
						{
							CodeDomSerializerBase.Error(manager, SR.GetString("SerializerUndeclaredName", new object[]
							{
								codeVariableReferenceExpression.VariableName
							}), "SerializerUndeclaredName");
						}
					}
					else if ((codeCastExpression2 = (obj as CodeCastExpression)) != null)
					{
						obj = this.DeserializeExpression(manager, name, codeCastExpression2.Expression);
						IConvertible convertible = obj as IConvertible;
						if (convertible != null)
						{
							Type type5 = manager.GetType(CodeDomSerializerBase.GetTypeNameFromCodeTypeReference(manager, codeCastExpression2.TargetType));
							if (type5 != null)
							{
								obj = convertible.ToType(type5, null);
							}
						}
					}
					else if (obj is CodeBaseReferenceExpression)
					{
						RootContext rootContext3 = (RootContext)manager.Context[typeof(RootContext)];
						if (rootContext3 != null)
						{
							obj = rootContext3.Value;
						}
						else
						{
							obj = null;
						}
					}
					else if ((codeArrayCreateExpression = (obj as CodeArrayCreateExpression)) != null)
					{
						Type type6 = manager.GetType(CodeDomSerializerBase.GetTypeNameFromCodeTypeReference(manager, codeArrayCreateExpression.CreateType));
						Array array4 = null;
						if (type6 != null)
						{
							if (codeArrayCreateExpression.Initializers.Count > 0)
							{
								ArrayList arrayList = new ArrayList(codeArrayCreateExpression.Initializers.Count);
								foreach (object obj6 in codeArrayCreateExpression.Initializers)
								{
									CodeExpression expression2 = (CodeExpression)obj6;
									try
									{
										object obj7 = this.DeserializeExpression(manager, null, expression2);
										if (!(obj7 is CodeExpression))
										{
											if (!type6.IsInstanceOfType(obj7))
											{
												obj7 = Convert.ChangeType(obj7, type6, CultureInfo.InvariantCulture);
											}
											arrayList.Add(obj7);
										}
									}
									catch (Exception errorInformation)
									{
										manager.ReportError(errorInformation);
									}
								}
								array4 = Array.CreateInstance(type6, arrayList.Count);
								arrayList.CopyTo(array4, 0);
							}
							else if (codeArrayCreateExpression.SizeExpression != null)
							{
								object obj8 = this.DeserializeExpression(manager, name, codeArrayCreateExpression.SizeExpression);
								IConvertible convertible2 = obj8 as IConvertible;
								if (convertible2 != null)
								{
									int length = convertible2.ToInt32(null);
									array4 = Array.CreateInstance(type6, length);
								}
							}
							else
							{
								array4 = Array.CreateInstance(type6, codeArrayCreateExpression.Size);
							}
						}
						else
						{
							CodeDomSerializerBase.Error(manager, SR.GetString("SerializerTypeNotFound", new object[]
							{
								codeArrayCreateExpression.CreateType.BaseType
							}), "SerializerTypeNotFound");
						}
						obj = array4;
						if (obj != null && name != null)
						{
							manager.SetName(obj, name);
						}
					}
					else if ((codeArrayIndexerExpression = (obj as CodeArrayIndexerExpression)) != null)
					{
						obj = null;
						Array array5 = this.DeserializeExpression(manager, name, codeArrayIndexerExpression.TargetObject) as Array;
						if (array5 != null)
						{
							int[] array6 = new int[codeArrayIndexerExpression.Indices.Count];
							bool flag3 = true;
							for (int l = 0; l < array6.Length; l++)
							{
								IConvertible convertible3 = this.DeserializeExpression(manager, name, codeArrayIndexerExpression.Indices[l]) as IConvertible;
								if (convertible3 == null)
								{
									flag3 = false;
									break;
								}
								array6[l] = convertible3.ToInt32(null);
							}
							if (flag3)
							{
								obj = array5.GetValue(array6);
							}
						}
					}
					else if ((codeBinaryOperatorExpression = (obj as CodeBinaryOperatorExpression)) != null)
					{
						object obj9 = this.DeserializeExpression(manager, null, codeBinaryOperatorExpression.Left);
						object obj10 = this.DeserializeExpression(manager, null, codeBinaryOperatorExpression.Right);
						obj = obj9;
						IConvertible convertible4 = obj9 as IConvertible;
						IConvertible convertible5 = obj10 as IConvertible;
						if (convertible4 != null && convertible5 != null)
						{
							obj = this.ExecuteBinaryExpression(convertible4, convertible5, codeBinaryOperatorExpression.Operator);
						}
					}
					else if ((codeDelegateInvokeExpression = (obj as CodeDelegateInvokeExpression)) != null)
					{
						object obj11 = this.DeserializeExpression(manager, null, codeDelegateInvokeExpression.TargetObject);
						Delegate @delegate = obj11 as Delegate;
						if (@delegate != null)
						{
							object[] array7 = new object[codeDelegateInvokeExpression.Parameters.Count];
							bool flag4 = true;
							for (int m = 0; m < array7.Length; m++)
							{
								array7[m] = this.DeserializeExpression(manager, null, codeDelegateInvokeExpression.Parameters[m]);
								if (array7[m] is CodeExpression)
								{
									flag4 = false;
									break;
								}
							}
							if (flag4)
							{
								@delegate.DynamicInvoke(array7);
							}
						}
					}
					else if ((codeDirectionExpression = (obj as CodeDirectionExpression)) != null)
					{
						obj = this.DeserializeExpression(manager, name, codeDirectionExpression.Expression);
					}
					else if ((codeIndexerExpression = (obj as CodeIndexerExpression)) != null)
					{
						obj = null;
						object obj12 = this.DeserializeExpression(manager, null, codeIndexerExpression.TargetObject);
						if (obj12 != null)
						{
							object[] array8 = new object[codeIndexerExpression.Indices.Count];
							bool flag5 = true;
							for (int n = 0; n < array8.Length; n++)
							{
								array8[n] = this.DeserializeExpression(manager, null, codeIndexerExpression.Indices[n]);
								if (array8[n] is CodeExpression)
								{
									flag5 = false;
									break;
								}
							}
							if (flag5)
							{
								obj = CodeDomSerializerBase.GetReflectionTypeHelper(manager, obj12).InvokeMember("Item", BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty, null, obj12, array8, null, null, null);
							}
						}
					}
					else if (obj is CodeSnippetExpression)
					{
						obj = null;
					}
					else if ((codeParameterDeclarationExpression = (obj as CodeParameterDeclarationExpression)) != null)
					{
						obj = manager.GetType(CodeDomSerializerBase.GetTypeNameFromCodeTypeReference(manager, codeParameterDeclarationExpression.Type));
					}
					else if ((codeTypeOfExpression = (obj as CodeTypeOfExpression)) != null)
					{
						string text2 = CodeDomSerializerBase.GetTypeNameFromCodeTypeReference(manager, codeTypeOfExpression.Type);
						for (int num = 0; num < codeTypeOfExpression.Type.ArrayRank; num++)
						{
							text2 += "[]";
						}
						obj = manager.GetType(text2);
						if (obj == null)
						{
							CodeDomSerializerBase.Error(manager, SR.GetString("SerializerTypeNotFound", new object[]
							{
								text2
							}), "SerializerTypeNotFound");
						}
					}
					else if (!(obj is CodeEventReferenceExpression) && !(obj is CodeMethodReferenceExpression) && obj is CodeDelegateCreateExpression)
					{
					}
				}
			}
			return obj;
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x00062B80 File Offset: 0x00060D80
		protected void DeserializePropertiesFromResources(IDesignerSerializationManager manager, object value, Attribute[] filter)
		{
			using (CodeDomSerializerBase.TraceScope("ComponentCodeDomSerializerBase::DeserializePropertiesFromResources"))
			{
				IDictionaryEnumerator dictionaryEnumerator = ResourceCodeDomSerializer.Default.GetMetadataEnumerator(manager);
				if (dictionaryEnumerator == null)
				{
					dictionaryEnumerator = ResourceCodeDomSerializer.Default.GetEnumerator(manager, CultureInfo.InvariantCulture);
				}
				if (dictionaryEnumerator != null)
				{
					RootContext rootContext = manager.Context[typeof(RootContext)] as RootContext;
					string value2;
					if (rootContext != null && rootContext.Value == value)
					{
						value2 = "$this";
					}
					else
					{
						value2 = manager.GetName(value);
					}
					PropertyDescriptorCollection propertiesHelper = CodeDomSerializerBase.GetPropertiesHelper(manager, value, null);
					while (dictionaryEnumerator.MoveNext())
					{
						string text = dictionaryEnumerator.Key as string;
						int num = text.IndexOf('.');
						if (num != -1)
						{
							string text2 = text.Substring(0, num);
							if (text2.Equals(value2))
							{
								string name = text.Substring(num + 1);
								PropertyDescriptor propertyDescriptor = propertiesHelper[name];
								if (propertyDescriptor != null)
								{
									bool flag = true;
									if (filter != null)
									{
										AttributeCollection attributes = propertyDescriptor.Attributes;
										foreach (Attribute attribute in filter)
										{
											if (!attributes.Contains(attribute))
											{
												flag = false;
												break;
											}
										}
									}
									if (flag)
									{
										object value3 = dictionaryEnumerator.Value;
										try
										{
											propertyDescriptor.SetValue(value, value3);
										}
										catch (Exception errorInformation)
										{
											manager.ReportError(errorInformation);
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x00062D04 File Offset: 0x00060F04
		protected void DeserializeStatement(IDesignerSerializationManager manager, CodeStatement statement)
		{
			using (CodeDomSerializerBase.TraceScope("CodeDomSerializerBase::DeserializeStatement"))
			{
				manager.Context.Push(statement);
				try
				{
					CodeAssignStatement codeAssignStatement = statement as CodeAssignStatement;
					if (codeAssignStatement != null)
					{
						this.DeserializeAssignStatement(manager, codeAssignStatement);
					}
					else
					{
						CodeVariableDeclarationStatement codeVariableDeclarationStatement = statement as CodeVariableDeclarationStatement;
						if (codeVariableDeclarationStatement != null)
						{
							this.DeserializeVariableDeclarationStatement(manager, codeVariableDeclarationStatement);
						}
						else if (!(statement is CodeCommentStatement))
						{
							CodeExpressionStatement codeExpressionStatement = statement as CodeExpressionStatement;
							if (codeExpressionStatement != null)
							{
								this.DeserializeExpression(manager, null, codeExpressionStatement.Expression);
							}
							else
							{
								CodeMethodReturnStatement codeMethodReturnStatement = statement as CodeMethodReturnStatement;
								if (codeMethodReturnStatement != null)
								{
									this.DeserializeExpression(manager, null, codeExpressionStatement.Expression);
								}
								else
								{
									CodeAttachEventStatement codeAttachEventStatement = statement as CodeAttachEventStatement;
									if (codeAttachEventStatement != null)
									{
										this.DeserializeAttachEventStatement(manager, codeAttachEventStatement);
									}
									else
									{
										CodeRemoveEventStatement codeRemoveEventStatement = statement as CodeRemoveEventStatement;
										if (codeRemoveEventStatement != null)
										{
											this.DeserializeDetachEventStatement(manager, codeRemoveEventStatement);
										}
										else
										{
											CodeLabeledStatement codeLabeledStatement = statement as CodeLabeledStatement;
											if (codeLabeledStatement != null)
											{
												this.DeserializeStatement(manager, codeLabeledStatement.Statement);
											}
										}
									}
								}
							}
						}
					}
				}
				catch (CheckoutException)
				{
					throw;
				}
				catch (Exception ex)
				{
					if (ex is TargetInvocationException)
					{
						ex = ex.InnerException;
					}
					if (!(ex is CodeDomSerializerException) && statement.LinePragma != null)
					{
						ex = new CodeDomSerializerException(ex, statement.LinePragma);
					}
					manager.ReportError(ex);
				}
				finally
				{
					manager.Context.Pop();
				}
			}
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x00062EA0 File Offset: 0x000610A0
		private bool DeserializePropertyAssignStatement(IDesignerSerializationManager manager, CodeAssignStatement statement, CodePropertyReferenceExpression propertyReferenceEx, bool reportError)
		{
			object obj = this.DeserializeExpression(manager, null, propertyReferenceEx.TargetObject);
			if (obj != null && !(obj is CodeExpression))
			{
				PropertyDescriptorCollection propertiesHelper = CodeDomSerializerBase.GetPropertiesHelper(manager, obj, CodeDomSerializerBase.runTimeProperties);
				PropertyDescriptor propertyDescriptor = propertiesHelper[propertyReferenceEx.PropertyName];
				if (propertyDescriptor != null)
				{
					object obj2 = this.DeserializeExpression(manager, null, statement.Right);
					if (obj2 is CodeExpression)
					{
						return false;
					}
					IConvertible convertible = obj2 as IConvertible;
					if (convertible != null && propertyDescriptor.PropertyType != obj2.GetType())
					{
						try
						{
							obj2 = convertible.ToType(propertyDescriptor.PropertyType, null);
						}
						catch
						{
						}
					}
					Type type = obj2 as Type;
					if (type != null && type.UnderlyingSystemType != null)
					{
						obj2 = type.UnderlyingSystemType;
					}
					MemberRelationship value = MemberRelationship.Empty;
					MemberRelationshipService memberRelationshipService = null;
					if (statement.Right is CodePropertyReferenceExpression)
					{
						memberRelationshipService = (manager.GetService(typeof(MemberRelationshipService)) as MemberRelationshipService);
						if (memberRelationshipService != null)
						{
							CodePropertyReferenceExpression codePropertyReferenceExpression = (CodePropertyReferenceExpression)statement.Right;
							object obj3 = this.DeserializeExpression(manager, null, codePropertyReferenceExpression.TargetObject);
							PropertyDescriptor propertyDescriptor2 = CodeDomSerializerBase.GetPropertiesHelper(manager, obj3, null)[codePropertyReferenceExpression.PropertyName];
							if (propertyDescriptor2 != null)
							{
								MemberRelationship source = new MemberRelationship(obj, propertyDescriptor);
								MemberRelationship memberRelationship = new MemberRelationship(obj3, propertyDescriptor2);
								value = memberRelationshipService[source];
								if (memberRelationshipService.SupportsRelationship(source, memberRelationship))
								{
									memberRelationshipService[source] = memberRelationship;
								}
							}
						}
					}
					else
					{
						memberRelationshipService = (manager.GetService(typeof(MemberRelationshipService)) as MemberRelationshipService);
						if (memberRelationshipService != null)
						{
							value = memberRelationshipService[obj, propertyDescriptor];
							memberRelationshipService[obj, propertyDescriptor] = MemberRelationship.Empty;
						}
					}
					try
					{
						propertyDescriptor.SetValue(obj, obj2);
					}
					catch
					{
						if (memberRelationshipService != null)
						{
							memberRelationshipService[obj, propertyDescriptor] = value;
						}
						throw;
					}
					return true;
				}
				else if (reportError)
				{
					CodeDomSerializerBase.Error(manager, SR.GetString("SerializerNoSuchProperty", new object[]
					{
						obj.GetType().FullName,
						propertyReferenceEx.PropertyName
					}), "SerializerNoSuchProperty");
				}
			}
			return false;
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x000630B4 File Offset: 0x000612B4
		private void DeserializeAssignStatement(IDesignerSerializationManager manager, CodeAssignStatement statement)
		{
			using (CodeDomSerializerBase.TraceScope("CodeDomSerializerBase::DeserializeAssignStatement"))
			{
				CodeExpression left = statement.Left;
				CodePropertyReferenceExpression propertyReferenceEx;
				CodeFieldReferenceExpression codeFieldReferenceExpression;
				CodeVariableReferenceExpression codeVariableReferenceExpression;
				CodeArrayIndexerExpression codeArrayIndexerExpression;
				if ((propertyReferenceEx = (left as CodePropertyReferenceExpression)) != null)
				{
					this.DeserializePropertyAssignStatement(manager, statement, propertyReferenceEx, true);
				}
				else if ((codeFieldReferenceExpression = (left as CodeFieldReferenceExpression)) != null)
				{
					object obj = this.DeserializeExpression(manager, codeFieldReferenceExpression.FieldName, codeFieldReferenceExpression.TargetObject);
					if (obj != null)
					{
						RootContext rootContext = (RootContext)manager.Context[typeof(RootContext)];
						if (rootContext != null && rootContext.Value == obj)
						{
							object obj2 = this.DeserializeExpression(manager, codeFieldReferenceExpression.FieldName, statement.Right);
							if (obj2 is CodeExpression)
							{
							}
						}
						else
						{
							Type type = obj as Type;
							object obj3;
							FieldInfo field;
							if (type != null)
							{
								obj3 = null;
								field = CodeDomSerializerBase.GetReflectionTypeFromTypeHelper(manager, type).GetField(codeFieldReferenceExpression.FieldName, BindingFlags.Static | BindingFlags.Public | BindingFlags.GetField);
							}
							else
							{
								obj3 = obj;
								field = CodeDomSerializerBase.GetReflectionTypeHelper(manager, obj).GetField(codeFieldReferenceExpression.FieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetField);
							}
							if (field != null)
							{
								object obj4 = this.DeserializeExpression(manager, codeFieldReferenceExpression.FieldName, statement.Right);
								if (!(obj4 is CodeExpression))
								{
									IConvertible convertible = obj4 as IConvertible;
									if (convertible != null)
									{
										Type type2 = field.FieldType;
										TypeDescriptionProvider targetFrameworkProviderForType = CodeDomSerializerBase.GetTargetFrameworkProviderForType(manager, type2);
										if (targetFrameworkProviderForType != null)
										{
											type2 = targetFrameworkProviderForType.GetRuntimeType(type2);
										}
										if (type2 != obj4.GetType())
										{
											try
											{
												obj4 = convertible.ToType(type2, null);
											}
											catch
											{
											}
										}
									}
									field.SetValue(obj3, obj4);
								}
							}
							else if (!this.DeserializePropertyAssignStatement(manager, statement, new CodePropertyReferenceExpression
							{
								TargetObject = codeFieldReferenceExpression.TargetObject,
								PropertyName = codeFieldReferenceExpression.FieldName
							}, false))
							{
								CodeDomSerializerBase.Error(manager, SR.GetString("SerializerNoSuchField", new object[]
								{
									obj.GetType().FullName,
									codeFieldReferenceExpression.FieldName
								}), "SerializerNoSuchField");
							}
						}
					}
				}
				else if ((codeVariableReferenceExpression = (left as CodeVariableReferenceExpression)) != null)
				{
					object obj5 = this.DeserializeExpression(manager, codeVariableReferenceExpression.VariableName, statement.Right);
					if (!(obj5 is CodeExpression))
					{
						manager.SetName(obj5, codeVariableReferenceExpression.VariableName);
					}
				}
				else if ((codeArrayIndexerExpression = (left as CodeArrayIndexerExpression)) != null)
				{
					int[] array = new int[codeArrayIndexerExpression.Indices.Count];
					object obj6 = this.DeserializeExpression(manager, null, codeArrayIndexerExpression.TargetObject);
					bool flag = true;
					for (int i = 0; i < array.Length; i++)
					{
						object obj7 = this.DeserializeExpression(manager, null, codeArrayIndexerExpression.Indices[i]);
						IConvertible convertible2 = obj7 as IConvertible;
						if (convertible2 == null)
						{
							flag = false;
							break;
						}
						array[i] = convertible2.ToInt32(null);
					}
					Array array2 = obj6 as Array;
					if (array2 != null && flag)
					{
						object obj8 = this.DeserializeExpression(manager, null, statement.Right);
						if (!(obj8 is CodeExpression))
						{
							array2.SetValue(obj8, array);
						}
					}
				}
			}
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x000633F0 File Offset: 0x000615F0
		private void DeserializeAttachEventStatement(IDesignerSerializationManager manager, CodeAttachEventStatement statement)
		{
			using (CodeDomSerializerBase.TraceScope("CodeDomSerializerBase::DeserializeAttachEventStatement"))
			{
				string text = null;
				object obj = null;
				object obj2 = this.DeserializeExpression(manager, null, statement.Event.TargetObject);
				string eventName = statement.Event.EventName;
				if (eventName != null && obj2 != null)
				{
					CodeObjectCreateExpression codeObjectCreateExpression = statement.Listener as CodeObjectCreateExpression;
					if (codeObjectCreateExpression != null)
					{
						if (codeObjectCreateExpression.Parameters.Count == 1)
						{
							CodeMethodReferenceExpression codeMethodReferenceExpression = codeObjectCreateExpression.Parameters[0] as CodeMethodReferenceExpression;
							if (codeMethodReferenceExpression != null)
							{
								text = codeMethodReferenceExpression.MethodName;
								obj = this.DeserializeExpression(manager, null, codeMethodReferenceExpression.TargetObject);
							}
						}
					}
					else
					{
						object obj3 = this.DeserializeExpression(manager, null, statement.Listener);
						CodeDelegateCreateExpression codeDelegateCreateExpression = obj3 as CodeDelegateCreateExpression;
						if (codeDelegateCreateExpression != null)
						{
							obj = this.DeserializeExpression(manager, null, codeDelegateCreateExpression.TargetObject);
							text = codeDelegateCreateExpression.MethodName;
						}
					}
					RootContext rootContext = (RootContext)manager.Context[typeof(RootContext)];
					bool flag = rootContext == null || (rootContext != null && rootContext.Value == obj);
					if (text != null)
					{
						if (flag && !(obj2 is CodeExpression))
						{
							EventDescriptor eventDescriptor = CodeDomSerializerBase.GetEventsHelper(manager, obj2, null)[eventName];
							if (eventDescriptor != null)
							{
								IEventBindingService eventBindingService = (IEventBindingService)manager.GetService(typeof(IEventBindingService));
								if (eventBindingService != null)
								{
									PropertyDescriptor eventProperty = eventBindingService.GetEventProperty(eventDescriptor);
									eventProperty.SetValue(obj2, text);
								}
							}
							else
							{
								CodeDomSerializerBase.Error(manager, SR.GetString("SerializerNoSuchEvent", new object[]
								{
									obj2.GetType().FullName,
									eventName
								}), "SerializerNoSuchEvent");
							}
						}
					}
				}
			}
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x000635A8 File Offset: 0x000617A8
		private void DeserializeDetachEventStatement(IDesignerSerializationManager manager, CodeRemoveEventStatement statement)
		{
			using (CodeDomSerializerBase.TraceScope("CodeDomSerializerBase::DeserializeDetachEventStatement"))
			{
				object obj = this.DeserializeExpression(manager, null, statement.Listener);
				CodeDelegateCreateExpression codeDelegateCreateExpression = obj as CodeDelegateCreateExpression;
				if (codeDelegateCreateExpression != null)
				{
					object obj2 = this.DeserializeExpression(manager, null, codeDelegateCreateExpression.TargetObject);
					RootContext rootContext = (RootContext)manager.Context[typeof(RootContext)];
					bool flag = rootContext == null || (rootContext != null && rootContext.Value == obj2);
					if (flag)
					{
						object obj3 = this.DeserializeExpression(manager, null, statement.Event.TargetObject);
						if (!(obj3 is CodeExpression))
						{
							EventDescriptor eventDescriptor = CodeDomSerializerBase.GetEventsHelper(manager, obj3, null)[statement.Event.EventName];
							if (eventDescriptor != null)
							{
								IEventBindingService eventBindingService = (IEventBindingService)manager.GetService(typeof(IEventBindingService));
								if (eventBindingService != null)
								{
									PropertyDescriptor eventProperty = eventBindingService.GetEventProperty(eventDescriptor);
									eventProperty.SetValue(obj3, null);
								}
							}
							else
							{
								CodeDomSerializerBase.Error(manager, SR.GetString("SerializerNoSuchEvent", new object[]
								{
									obj3.GetType().FullName,
									statement.Event.EventName
								}), "SerializerNoSuchEvent");
							}
						}
					}
				}
			}
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x000636FC File Offset: 0x000618FC
		private void DeserializeVariableDeclarationStatement(IDesignerSerializationManager manager, CodeVariableDeclarationStatement statement)
		{
			using (CodeDomSerializerBase.TraceScope("CodeDomSerializerBase::DeserializeVariableDeclarationStatement"))
			{
				if (statement.InitExpression != null)
				{
					this.DeserializeExpression(manager, statement.Name, statement.InitExpression);
				}
			}
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x0006374C File Offset: 0x0006194C
		internal static void Error(IDesignerSerializationManager manager, string exceptionText, string helpLink)
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			if (exceptionText == null)
			{
				throw new ArgumentNullException("exceptionText");
			}
			CodeStatement codeStatement = (CodeStatement)manager.Context[typeof(CodeStatement)];
			CodeLinePragma linePragma = null;
			if (codeStatement != null)
			{
				linePragma = codeStatement.LinePragma;
			}
			throw new CodeDomSerializerException(exceptionText, linePragma)
			{
				HelpLink = helpLink
			};
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x000637AC File Offset: 0x000619AC
		private object ExecuteBinaryExpression(IConvertible left, IConvertible right, CodeBinaryOperatorType op)
		{
			CodeBinaryOperatorType[] array = new CodeBinaryOperatorType[9];
			RuntimeHelpers.InitializeArray(array, fieldof(<PrivateImplementationDetails>.A3B30E326B5D2A84049D53B4CB1E137B0BF0808762D1DAA3DAB25ACF09F2BD36).FieldHandle);
			CodeBinaryOperatorType[] array2 = array;
			CodeBinaryOperatorType[] array3 = new CodeBinaryOperatorType[5];
			RuntimeHelpers.InitializeArray(array3, fieldof(<PrivateImplementationDetails>.E528F4309E1413E6BC35AEA5D8DB8519384D2FCC33F9DD5D1126D73F104CF92A).FieldHandle);
			CodeBinaryOperatorType[] array4 = array3;
			CodeBinaryOperatorType[] array5 = new CodeBinaryOperatorType[]
			{
				CodeBinaryOperatorType.BitwiseOr,
				CodeBinaryOperatorType.BitwiseAnd
			};
			for (int i = 0; i < array5.Length; i++)
			{
				if (op == array5[i])
				{
					return this.ExecuteBinaryOperator(left, right, op);
				}
			}
			for (int j = 0; j < array4.Length; j++)
			{
				if (op == array4[j])
				{
					return this.ExecuteMathOperator(left, right, op);
				}
			}
			for (int k = 0; k < array2.Length; k++)
			{
				if (op == array2[k])
				{
					return this.ExecuteBooleanOperator(left, right, op);
				}
			}
			return left;
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x00063854 File Offset: 0x00061A54
		private object ExecuteBinaryOperator(IConvertible left, IConvertible right, CodeBinaryOperatorType op)
		{
			TypeCode typeCode = left.GetTypeCode();
			TypeCode typeCode2 = right.GetTypeCode();
			TypeCode[] array = new TypeCode[8];
			RuntimeHelpers.InitializeArray(array, fieldof(<PrivateImplementationDetails>.2DD8119AB96B20E03EE64D16D5114485FB82193F33A56157E391ECA2BB4EFD46).FieldHandle);
			TypeCode[] array2 = array;
			int num = -1;
			int num2 = -1;
			for (int i = 0; i < array2.Length; i++)
			{
				if (typeCode == array2[i])
				{
					num = i;
				}
				if (typeCode2 == array2[i])
				{
					num2 = i;
				}
				if (num != -1 && num2 != -1)
				{
					break;
				}
			}
			if (num == -1 || num2 == -1)
			{
				return left;
			}
			int num3 = Math.Max(num, num2);
			object obj = left;
			switch (array2[num3])
			{
			case TypeCode.Char:
			{
				char c = left.ToChar(null);
				char c2 = right.ToChar(null);
				if (op == CodeBinaryOperatorType.BitwiseOr)
				{
					obj = (int)(c | c2);
				}
				else
				{
					obj = (int)(c & c2);
				}
				break;
			}
			case TypeCode.Byte:
			{
				byte b = left.ToByte(null);
				byte b2 = right.ToByte(null);
				if (op == CodeBinaryOperatorType.BitwiseOr)
				{
					obj = (int)(b | b2);
				}
				else
				{
					obj = (int)(b & b2);
				}
				break;
			}
			case TypeCode.Int16:
			{
				short num4 = left.ToInt16(null);
				short num5 = right.ToInt16(null);
				if (op == CodeBinaryOperatorType.BitwiseOr)
				{
					obj = (short)((ushort)num4 | (ushort)num5);
				}
				else
				{
					obj = (int)(num4 & num5);
				}
				break;
			}
			case TypeCode.UInt16:
			{
				ushort num6 = left.ToUInt16(null);
				ushort num7 = right.ToUInt16(null);
				if (op == CodeBinaryOperatorType.BitwiseOr)
				{
					obj = (int)(num6 | num7);
				}
				else
				{
					obj = (int)(num6 & num7);
				}
				break;
			}
			case TypeCode.Int32:
			{
				int num8 = left.ToInt32(null);
				int num9 = right.ToInt32(null);
				if (op == CodeBinaryOperatorType.BitwiseOr)
				{
					obj = (num8 | num9);
				}
				else
				{
					obj = (num8 & num9);
				}
				break;
			}
			case TypeCode.UInt32:
			{
				uint num10 = left.ToUInt32(null);
				uint num11 = right.ToUInt32(null);
				if (op == CodeBinaryOperatorType.BitwiseOr)
				{
					obj = (num10 | num11);
				}
				else
				{
					obj = (num10 & num11);
				}
				break;
			}
			case TypeCode.Int64:
			{
				long num12 = left.ToInt64(null);
				long num13 = right.ToInt64(null);
				if (op == CodeBinaryOperatorType.BitwiseOr)
				{
					obj = (num12 | num13);
				}
				else
				{
					obj = (num12 & num13);
				}
				break;
			}
			case TypeCode.UInt64:
			{
				ulong num14 = left.ToUInt64(null);
				ulong num15 = right.ToUInt64(null);
				if (op == CodeBinaryOperatorType.BitwiseOr)
				{
					obj = (num14 | num15);
				}
				else
				{
					obj = (num14 & num15);
				}
				break;
			}
			}
			if (obj != left && left is Enum)
			{
				obj = Enum.ToObject(left.GetType(), obj);
			}
			return obj;
		}

		// Token: 0x060011B0 RID: 4528 RVA: 0x00063ADC File Offset: 0x00061CDC
		private object ExecuteBooleanOperator(IConvertible left, IConvertible right, CodeBinaryOperatorType op)
		{
			bool flag = false;
			switch (op)
			{
			case CodeBinaryOperatorType.IdentityInequality:
				flag = (left != right);
				break;
			case CodeBinaryOperatorType.IdentityEquality:
				flag = (left == right);
				break;
			case CodeBinaryOperatorType.ValueEquality:
				flag = left.Equals(right);
				break;
			case CodeBinaryOperatorType.BooleanOr:
				flag = (left.ToBoolean(null) || right.ToBoolean(null));
				break;
			case CodeBinaryOperatorType.BooleanAnd:
				flag = (left.ToBoolean(null) && right.ToBoolean(null));
				break;
			}
			return flag;
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x00063B6C File Offset: 0x00061D6C
		private object ExecuteMathOperator(IConvertible left, IConvertible right, CodeBinaryOperatorType op)
		{
			if (op != CodeBinaryOperatorType.Add)
			{
				return left;
			}
			string text = left as string;
			string text2 = right as string;
			if (text == null && left is char)
			{
				text = left.ToString();
			}
			if (text2 == null && right is char)
			{
				text2 = right.ToString();
			}
			if (text != null && text2 != null)
			{
				return text + text2;
			}
			return left;
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x00063BC0 File Offset: 0x00061DC0
		protected CodeExpression GetExpression(IDesignerSerializationManager manager, object value)
		{
			CodeExpression codeExpression = null;
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			ExpressionTable expressionTable = manager.Context[typeof(ExpressionTable)] as ExpressionTable;
			if (expressionTable != null)
			{
				codeExpression = expressionTable.GetExpression(value);
			}
			if (codeExpression == null)
			{
				RootContext rootContext = manager.Context[typeof(RootContext)] as RootContext;
				if (rootContext != null && rootContext.Value == value)
				{
					codeExpression = rootContext.Expression;
				}
			}
			if (codeExpression == null)
			{
				string name = manager.GetName(value);
				if (name == null || name.IndexOf('.') != -1)
				{
					IReferenceService referenceService = manager.GetService(typeof(IReferenceService)) as IReferenceService;
					if (referenceService != null)
					{
						name = referenceService.GetName(value);
						if (name != null && name.IndexOf('.') != -1)
						{
							string[] array = name.Split(new char[]
							{
								'.'
							});
							object instance = manager.GetInstance(array[0]);
							if (instance != null)
							{
								CodeExpression codeExpression2 = this.SerializeToExpression(manager, instance);
								if (codeExpression2 != null)
								{
									for (int i = 1; i < array.Length; i++)
									{
										codeExpression2 = new CodePropertyReferenceExpression(codeExpression2, array[i]);
									}
									codeExpression = codeExpression2;
								}
							}
						}
					}
				}
			}
			if (codeExpression == null)
			{
				ExpressionContext expressionContext = manager.Context[typeof(ExpressionContext)] as ExpressionContext;
				if (expressionContext != null && expressionContext.PresetValue == value)
				{
					codeExpression = expressionContext.Expression;
				}
			}
			if (codeExpression != null)
			{
				ComponentCache.Entry entry = (ComponentCache.Entry)manager.Context[typeof(ComponentCache.Entry)];
				ComponentCache componentCache = (ComponentCache)manager.Context[typeof(ComponentCache)];
				if (entry != null && entry.Component != value && componentCache != null)
				{
					ComponentCache.Entry entryAll = componentCache.GetEntryAll(value);
					if (entryAll != null && entry.Component != null)
					{
						entryAll.AddDependency(entry.Component);
					}
				}
			}
			return codeExpression;
		}

		// Token: 0x060011B3 RID: 4531 RVA: 0x00063D94 File Offset: 0x00061F94
		private PropertyDescriptorCollection GetFilteredProperties(IDesignerSerializationManager manager, object value, Attribute[] filter)
		{
			IComponent component = value as IComponent;
			PropertyDescriptorCollection propertyDescriptorCollection = CodeDomSerializerBase.GetPropertiesHelper(manager, value, filter);
			if (component != null)
			{
				if (((IDictionary)propertyDescriptorCollection).IsReadOnly)
				{
					PropertyDescriptor[] array = new PropertyDescriptor[propertyDescriptorCollection.Count];
					propertyDescriptorCollection.CopyTo(array, 0);
					propertyDescriptorCollection = new PropertyDescriptorCollection(array);
				}
				PropertyDescriptor propertyDescriptor = manager.Properties["FilteredProperties"];
				if (propertyDescriptor != null)
				{
					ITypeDescriptorFilterService typeDescriptorFilterService = propertyDescriptor.GetValue(manager) as ITypeDescriptorFilterService;
					if (typeDescriptorFilterService != null)
					{
						typeDescriptorFilterService.FilterProperties(component, propertyDescriptorCollection);
					}
				}
			}
			return propertyDescriptorCollection;
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x00063E08 File Offset: 0x00062008
		private CodeExpression GetLegacyExpression(IDesignerSerializationManager manager, object value)
		{
			CodeDomSerializerBase.LegacyExpressionTable legacyExpressionTable = manager.Context[typeof(CodeDomSerializerBase.LegacyExpressionTable)] as CodeDomSerializerBase.LegacyExpressionTable;
			CodeExpression codeExpression = null;
			if (legacyExpressionTable != null)
			{
				object obj = legacyExpressionTable[value];
				if (obj == value)
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
						if (rootContext != null)
						{
							if (rootContext.Value == value)
							{
								codeExpression = rootContext.Expression;
							}
							else if (flag && name.IndexOf('.') != -1)
							{
								int num = name.IndexOf('.');
								codeExpression = new CodePropertyReferenceExpression(new CodeFieldReferenceExpression(rootContext.Expression, name.Substring(0, num)), name.Substring(num + 1));
							}
							else
							{
								codeExpression = new CodeFieldReferenceExpression(rootContext.Expression, name);
							}
						}
						else if (flag && name.IndexOf('.') != -1)
						{
							int num2 = name.IndexOf('.');
							codeExpression = new CodePropertyReferenceExpression(new CodeVariableReferenceExpression(name.Substring(0, num2)), name.Substring(num2 + 1));
						}
						else
						{
							codeExpression = new CodeVariableReferenceExpression(name);
						}
					}
					legacyExpressionTable[value] = codeExpression;
				}
				else
				{
					codeExpression = (obj as CodeExpression);
				}
			}
			return codeExpression;
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x00063F60 File Offset: 0x00062160
		protected CodeDomSerializer GetSerializer(IDesignerSerializationManager manager, object value)
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			if (value != null)
			{
				AttributeCollection attributesHelper = CodeDomSerializerBase.GetAttributesHelper(manager, value);
				AttributeCollection attributesFromTypeHelper = CodeDomSerializerBase.GetAttributesFromTypeHelper(manager, value.GetType());
				if (attributesHelper.Count != attributesFromTypeHelper.Count)
				{
					string text = null;
					Type typeFromHandle = typeof(CodeDomSerializer);
					DesignerSerializationManager designerSerializationManager = manager as DesignerSerializationManager;
					foreach (object obj in attributesHelper)
					{
						Attribute attribute = (Attribute)obj;
						DesignerSerializerAttribute designerSerializerAttribute = attribute as DesignerSerializerAttribute;
						if (designerSerializerAttribute != null)
						{
							Type left;
							if (designerSerializationManager != null)
							{
								left = designerSerializationManager.GetRuntimeType(designerSerializerAttribute.SerializerBaseTypeName);
							}
							else
							{
								left = manager.GetType(designerSerializerAttribute.SerializerBaseTypeName);
							}
							if (left == typeFromHandle)
							{
								text = designerSerializerAttribute.SerializerTypeName;
								break;
							}
						}
					}
					if (text != null)
					{
						foreach (object obj2 in attributesFromTypeHelper)
						{
							Attribute attribute2 = (Attribute)obj2;
							DesignerSerializerAttribute designerSerializerAttribute2 = attribute2 as DesignerSerializerAttribute;
							if (designerSerializerAttribute2 != null)
							{
								Type left2;
								if (designerSerializationManager != null)
								{
									left2 = designerSerializationManager.GetRuntimeType(designerSerializerAttribute2.SerializerBaseTypeName);
								}
								else
								{
									left2 = manager.GetType(designerSerializerAttribute2.SerializerBaseTypeName);
								}
								if (left2 == typeFromHandle)
								{
									if (text.Equals(designerSerializerAttribute2.SerializerTypeName))
									{
										text = null;
										break;
									}
									break;
								}
							}
						}
					}
					if (text != null)
					{
						Type type = (designerSerializationManager != null) ? designerSerializationManager.GetRuntimeType(text) : manager.GetType(text);
						if (type != null && typeFromHandle.IsAssignableFrom(type))
						{
							return (CodeDomSerializer)Activator.CreateInstance(type);
						}
					}
				}
			}
			Type objectType = null;
			if (value != null)
			{
				objectType = value.GetType();
			}
			return (CodeDomSerializer)manager.GetSerializer(objectType, typeof(CodeDomSerializer));
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x00064144 File Offset: 0x00062344
		protected CodeDomSerializer GetSerializer(IDesignerSerializationManager manager, Type valueType)
		{
			return manager.GetSerializer(valueType, typeof(CodeDomSerializer)) as CodeDomSerializer;
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x0006415C File Offset: 0x0006235C
		protected bool IsSerialized(IDesignerSerializationManager manager, object value)
		{
			return this.IsSerialized(manager, value, false);
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x00064168 File Offset: 0x00062368
		protected bool IsSerialized(IDesignerSerializationManager manager, object value, bool honorPreset)
		{
			bool result = false;
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			ExpressionTable expressionTable = manager.Context[typeof(ExpressionTable)] as ExpressionTable;
			if (expressionTable != null && expressionTable.GetExpression(value) != null && (!honorPreset || !expressionTable.ContainsPresetExpression(value)))
			{
				result = true;
			}
			return result;
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x000641CC File Offset: 0x000623CC
		protected CodeExpression SerializeCreationExpression(IDesignerSerializationManager manager, object value, out bool isComplete)
		{
			isComplete = false;
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			TypeConverter converter = TypeDescriptor.GetConverter(value);
			ExpressionContext expressionContext = manager.Context[typeof(ExpressionContext)] as ExpressionContext;
			if (expressionContext != null && expressionContext.PresetValue == value)
			{
				CodeExpression expression = expressionContext.Expression;
				if (converter.CanConvertTo(typeof(InstanceDescriptor)))
				{
					InstanceDescriptor instanceDescriptor = converter.ConvertTo(value, typeof(InstanceDescriptor)) as InstanceDescriptor;
					if (instanceDescriptor != null && instanceDescriptor.MemberInfo != null)
					{
						isComplete = instanceDescriptor.IsComplete;
					}
				}
				return expression;
			}
			if (converter.CanConvertTo(typeof(InstanceDescriptor)))
			{
				InstanceDescriptor instanceDescriptor2 = converter.ConvertTo(value, typeof(InstanceDescriptor)) as InstanceDescriptor;
				if (instanceDescriptor2 != null && instanceDescriptor2.MemberInfo != null)
				{
					isComplete = instanceDescriptor2.IsComplete;
					return this.SerializeInstanceDescriptor(manager, value, instanceDescriptor2);
				}
			}
			if (CodeDomSerializerBase.GetReflectionTypeHelper(manager, value).IsSerializable && (!(value is IComponent) || ((IComponent)value).Site == null))
			{
				CodeExpression codeExpression = this.SerializeToResourceExpression(manager, value);
				if (codeExpression != null)
				{
					isComplete = true;
					return codeExpression;
				}
			}
			ConstructorInfo constructor = CodeDomSerializerBase.GetReflectionTypeHelper(manager, value).GetConstructor(new Type[0]);
			if (constructor != null)
			{
				isComplete = false;
				return new CodeObjectCreateExpression(TypeDescriptor.GetClassName(value), new CodeExpression[0]);
			}
			return null;
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x00064330 File Offset: 0x00062530
		protected string GetUniqueName(IDesignerSerializationManager manager, object value)
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			string text = manager.GetName(value);
			if (text == null)
			{
				Type reflectionTypeHelper = CodeDomSerializerBase.GetReflectionTypeHelper(manager, value);
				INameCreationService nameCreationService = manager.GetService(typeof(INameCreationService)) as INameCreationService;
				string text2;
				if (nameCreationService != null)
				{
					text2 = nameCreationService.CreateName(null, reflectionTypeHelper);
				}
				else
				{
					text2 = reflectionTypeHelper.Name.ToLower(CultureInfo.InvariantCulture);
				}
				int num = 1;
				ComponentCache componentCache = manager.Context[typeof(ComponentCache)] as ComponentCache;
				for (;;)
				{
					text = string.Format(CultureInfo.CurrentCulture, "{0}{1}", new object[]
					{
						text2,
						num
					});
					if (manager.GetInstance(text) == null && (componentCache == null || !componentCache.ContainsLocalName(text)))
					{
						break;
					}
					num++;
				}
				manager.SetName(value, text);
				ComponentCache.Entry entry = manager.Context[typeof(ComponentCache.Entry)] as ComponentCache.Entry;
				if (entry != null)
				{
					entry.AddLocalName(text);
				}
			}
			return text;
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x0006443C File Offset: 0x0006263C
		protected void SerializeEvent(IDesignerSerializationManager manager, CodeStatementCollection statements, object value, EventDescriptor descriptor)
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			if (statements == null)
			{
				throw new ArgumentNullException("statements");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (descriptor == null)
			{
				throw new ArgumentNullException("descriptor");
			}
			using (CodeDomSerializerBase.TraceScope("CodeDomSerializerBase::SerializeEvent"))
			{
				manager.Context.Push(statements);
				manager.Context.Push(descriptor);
				try
				{
					MemberCodeDomSerializer memberCodeDomSerializer = (MemberCodeDomSerializer)manager.GetSerializer(descriptor.GetType(), typeof(MemberCodeDomSerializer));
					if (memberCodeDomSerializer != null && memberCodeDomSerializer.ShouldSerialize(manager, value, descriptor))
					{
						memberCodeDomSerializer.Serialize(manager, value, descriptor, statements);
					}
				}
				finally
				{
					manager.Context.Pop();
					manager.Context.Pop();
				}
			}
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x00064520 File Offset: 0x00062720
		protected void SerializeEvents(IDesignerSerializationManager manager, CodeStatementCollection statements, object value, params Attribute[] filter)
		{
			EventDescriptorCollection eventDescriptorCollection = CodeDomSerializerBase.GetEventsHelper(manager, value, filter).Sort();
			foreach (object obj in eventDescriptorCollection)
			{
				EventDescriptor descriptor = (EventDescriptor)obj;
				this.SerializeEvent(manager, statements, value, descriptor);
			}
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x00064588 File Offset: 0x00062788
		private CodeExpression SerializeInstanceDescriptor(IDesignerSerializationManager manager, object value, InstanceDescriptor descriptor)
		{
			CodeExpression codeExpression = null;
			using (CodeDomSerializerBase.TraceScope("CodeDomSerializerBase::SerializeInstanceDescriptor"))
			{
				CodeExpression[] array = new CodeExpression[descriptor.Arguments.Count];
				object[] array2 = new object[array.Length];
				ParameterInfo[] array3 = null;
				if (array.Length != 0)
				{
					descriptor.Arguments.CopyTo(array2, 0);
					MethodBase methodBase = descriptor.MemberInfo as MethodBase;
					if (methodBase != null)
					{
						array3 = methodBase.GetParameters();
					}
				}
				bool flag = true;
				for (int i = 0; i < array.Length; i++)
				{
					object obj = array2[i];
					CodeExpression codeExpression2 = null;
					ExpressionContext expressionContext = null;
					ExpressionContext expressionContext2 = manager.Context[typeof(ExpressionContext)] as ExpressionContext;
					if (expressionContext2 != null)
					{
						expressionContext = new ExpressionContext(expressionContext2.Expression, array3[i].ParameterType, expressionContext2.Owner);
						manager.Context.Push(expressionContext);
					}
					try
					{
						codeExpression2 = this.SerializeToExpression(manager, obj);
					}
					finally
					{
						if (expressionContext != null)
						{
							manager.Context.Pop();
						}
					}
					if (codeExpression2 == null)
					{
						flag = false;
						break;
					}
					if (obj != null && !array3[i].ParameterType.IsAssignableFrom(obj.GetType()))
					{
						codeExpression2 = new CodeCastExpression(array3[i].ParameterType, codeExpression2);
					}
					array[i] = codeExpression2;
				}
				if (flag)
				{
					Type type = descriptor.MemberInfo.DeclaringType;
					CodeTypeReference codeTypeReference = new CodeTypeReference(type);
					if (descriptor.MemberInfo is ConstructorInfo)
					{
						codeExpression = new CodeObjectCreateExpression(codeTypeReference, array);
					}
					else if (descriptor.MemberInfo is MethodInfo)
					{
						CodeTypeReferenceExpression targetObject = new CodeTypeReferenceExpression(codeTypeReference);
						CodeMethodReferenceExpression method = new CodeMethodReferenceExpression(targetObject, descriptor.MemberInfo.Name);
						codeExpression = new CodeMethodInvokeExpression(method, array);
						type = ((MethodInfo)descriptor.MemberInfo).ReturnType;
					}
					else if (descriptor.MemberInfo is PropertyInfo)
					{
						CodeTypeReferenceExpression targetObject2 = new CodeTypeReferenceExpression(codeTypeReference);
						CodePropertyReferenceExpression codePropertyReferenceExpression = new CodePropertyReferenceExpression(targetObject2, descriptor.MemberInfo.Name);
						codeExpression = codePropertyReferenceExpression;
						type = ((PropertyInfo)descriptor.MemberInfo).PropertyType;
					}
					else if (descriptor.MemberInfo is FieldInfo)
					{
						CodeTypeReferenceExpression targetObject3 = new CodeTypeReferenceExpression(codeTypeReference);
						codeExpression = new CodeFieldReferenceExpression(targetObject3, descriptor.MemberInfo.Name);
						type = ((FieldInfo)descriptor.MemberInfo).FieldType;
					}
					Type type2 = value.GetType();
					while (!type2.IsPublic)
					{
						type2 = type2.BaseType;
					}
					if (!type2.IsAssignableFrom(type))
					{
						codeExpression = new CodeCastExpression(type2, codeExpression);
					}
				}
			}
			return codeExpression;
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x00064830 File Offset: 0x00062A30
		protected void SerializeProperties(IDesignerSerializationManager manager, CodeStatementCollection statements, object value, Attribute[] filter)
		{
			using (CodeDomSerializerBase.TraceScope("CodeDomSerializerBase::SerializeProperties"))
			{
				PropertyDescriptorCollection propertyDescriptorCollection = this.GetFilteredProperties(manager, value, filter).Sort();
				InheritanceAttribute inheritanceAttribute = (InheritanceAttribute)CodeDomSerializerBase.GetAttributesHelper(manager, value)[typeof(InheritanceAttribute)];
				if (inheritanceAttribute == null)
				{
					inheritanceAttribute = InheritanceAttribute.NotInherited;
				}
				manager.Context.Push(inheritanceAttribute);
				try
				{
					foreach (object obj in propertyDescriptorCollection)
					{
						PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
						if (!propertyDescriptor.Attributes.Contains(DesignerSerializationVisibilityAttribute.Hidden))
						{
							this.SerializeProperty(manager, statements, value, propertyDescriptor);
						}
					}
				}
				finally
				{
					manager.Context.Pop();
				}
			}
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x0006491C File Offset: 0x00062B1C
		protected void SerializePropertiesToResources(IDesignerSerializationManager manager, CodeStatementCollection statements, object value, Attribute[] filter)
		{
			using (CodeDomSerializerBase.TraceScope("ComponentCodeDomSerializerBase::SerializePropertiesToResources"))
			{
				PropertyDescriptorCollection propertiesHelper = CodeDomSerializerBase.GetPropertiesHelper(manager, value, filter);
				manager.Context.Push(statements);
				try
				{
					CodeExpression codeExpression = this.SerializeToExpression(manager, value);
					if (codeExpression != null)
					{
						CodePropertyReferenceExpression codePropertyReferenceExpression = new CodePropertyReferenceExpression(codeExpression, string.Empty);
						foreach (object obj in propertiesHelper)
						{
							PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
							ExpressionContext context = new ExpressionContext(codePropertyReferenceExpression, propertyDescriptor.PropertyType, value);
							manager.Context.Push(context);
							try
							{
								if (propertyDescriptor.Attributes.Contains(DesignerSerializationVisibilityAttribute.Visible))
								{
									codePropertyReferenceExpression.PropertyName = propertyDescriptor.Name;
									string text;
									if (codeExpression is CodeThisReferenceExpression)
									{
										text = "$this";
									}
									else
									{
										text = manager.GetName(value);
									}
									text = string.Format(CultureInfo.CurrentCulture, "{0}.{1}", new object[]
									{
										text,
										propertyDescriptor.Name
									});
									ResourceCodeDomSerializer.Default.SerializeMetadata(manager, text, propertyDescriptor.GetValue(value), propertyDescriptor.ShouldSerializeValue(value));
								}
							}
							finally
							{
								manager.Context.Pop();
							}
						}
					}
				}
				finally
				{
					manager.Context.Pop();
				}
			}
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x00064ACC File Offset: 0x00062CCC
		protected void SerializeProperty(IDesignerSerializationManager manager, CodeStatementCollection statements, object value, PropertyDescriptor propertyToSerialize)
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (propertyToSerialize == null)
			{
				throw new ArgumentNullException("propertyToSerialize");
			}
			if (statements == null)
			{
				throw new ArgumentNullException("statements");
			}
			manager.Context.Push(statements);
			manager.Context.Push(propertyToSerialize);
			try
			{
				MemberCodeDomSerializer memberCodeDomSerializer = (MemberCodeDomSerializer)manager.GetSerializer(propertyToSerialize.GetType(), typeof(MemberCodeDomSerializer));
				if (memberCodeDomSerializer != null && memberCodeDomSerializer.ShouldSerialize(manager, value, propertyToSerialize))
				{
					memberCodeDomSerializer.Serialize(manager, value, propertyToSerialize, statements);
				}
			}
			finally
			{
				manager.Context.Pop();
				manager.Context.Pop();
			}
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x00064B90 File Offset: 0x00062D90
		protected void SerializeResource(IDesignerSerializationManager manager, string resourceName, object value)
		{
			ResourceCodeDomSerializer.Default.WriteResource(manager, resourceName, value);
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x00064B9F File Offset: 0x00062D9F
		protected void SerializeResourceInvariant(IDesignerSerializationManager manager, string resourceName, object value)
		{
			ResourceCodeDomSerializer.Default.WriteResourceInvariant(manager, resourceName, value);
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x00064BB0 File Offset: 0x00062DB0
		protected CodeExpression SerializeToExpression(IDesignerSerializationManager manager, object value)
		{
			CodeExpression codeExpression = null;
			using (CodeDomSerializerBase.TraceScope("SerializeToExpression"))
			{
				if (value != null)
				{
					if (this.IsSerialized(manager, value))
					{
						codeExpression = this.GetExpression(manager, value);
					}
					else
					{
						codeExpression = this.GetLegacyExpression(manager, value);
						if (codeExpression != null)
						{
							this.SetExpression(manager, value, codeExpression);
						}
					}
				}
				if (codeExpression == null)
				{
					CodeDomSerializer serializer = this.GetSerializer(manager, value);
					if (serializer != null)
					{
						CodeStatementCollection codeStatementCollection = null;
						if (value != null)
						{
							this.SetLegacyExpression(manager, value);
							StatementContext statementContext = manager.Context[typeof(StatementContext)] as StatementContext;
							if (statementContext != null)
							{
								codeStatementCollection = statementContext.StatementCollection[value];
							}
							if (codeStatementCollection != null)
							{
								manager.Context.Push(codeStatementCollection);
							}
						}
						object obj = null;
						try
						{
							obj = serializer.Serialize(manager, value);
						}
						finally
						{
							if (codeStatementCollection != null)
							{
								manager.Context.Pop();
							}
						}
						codeExpression = (obj as CodeExpression);
						if (codeExpression == null && value != null)
						{
							codeExpression = this.GetExpression(manager, value);
						}
						CodeStatementCollection codeStatementCollection2 = obj as CodeStatementCollection;
						if (codeStatementCollection2 == null)
						{
							CodeStatement codeStatement = obj as CodeStatement;
							if (codeStatement != null)
							{
								codeStatementCollection2 = new CodeStatementCollection();
								codeStatementCollection2.Add(codeStatement);
							}
						}
						if (codeStatementCollection2 != null)
						{
							if (codeStatementCollection == null)
							{
								codeStatementCollection = (manager.Context[typeof(CodeStatementCollection)] as CodeStatementCollection);
							}
							if (codeStatementCollection != null)
							{
								codeStatementCollection.AddRange(codeStatementCollection2);
							}
							else
							{
								string text = "(null)";
								if (value != null)
								{
									text = manager.GetName(value);
									if (text == null)
									{
										text = value.GetType().Name;
									}
								}
								manager.ReportError(SR.GetString("SerializerLostStatements", new object[]
								{
									text
								}));
							}
						}
					}
					else
					{
						manager.ReportError(SR.GetString("SerializerNoSerializerForComponent", new object[]
						{
							value.GetType().FullName
						}));
					}
				}
			}
			return codeExpression;
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x00064D90 File Offset: 0x00062F90
		protected CodeExpression SerializeToResourceExpression(IDesignerSerializationManager manager, object value)
		{
			return this.SerializeToResourceExpression(manager, value, true);
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x00064D9C File Offset: 0x00062F9C
		protected CodeExpression SerializeToResourceExpression(IDesignerSerializationManager manager, object value, bool ensureInvariant)
		{
			CodeExpression result = null;
			if (value == null || value.GetType().IsSerializable)
			{
				CodeStatementCollection codeStatementCollection = null;
				if (value != null)
				{
					StatementContext statementContext = manager.Context[typeof(StatementContext)] as StatementContext;
					if (statementContext != null)
					{
						codeStatementCollection = statementContext.StatementCollection[value];
					}
					if (codeStatementCollection != null)
					{
						manager.Context.Push(codeStatementCollection);
					}
				}
				try
				{
					result = (ResourceCodeDomSerializer.Default.Serialize(manager, value, false, ensureInvariant) as CodeExpression);
				}
				finally
				{
					if (codeStatementCollection != null)
					{
						manager.Context.Pop();
					}
				}
			}
			return result;
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x00064E34 File Offset: 0x00063034
		protected void SetExpression(IDesignerSerializationManager manager, object value, CodeExpression expression)
		{
			this.SetExpression(manager, value, expression, false);
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x00064E40 File Offset: 0x00063040
		protected void SetExpression(IDesignerSerializationManager manager, object value, CodeExpression expression, bool isPreset)
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			ExpressionTable expressionTable = (ExpressionTable)manager.Context[typeof(ExpressionTable)];
			if (expressionTable == null)
			{
				expressionTable = new ExpressionTable();
				manager.Context.Append(expressionTable);
			}
			expressionTable.SetExpression(value, expression, isPreset);
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x00064EB4 File Offset: 0x000630B4
		private void SetLegacyExpression(IDesignerSerializationManager manager, object value)
		{
			if (value is IComponent)
			{
				CodeDomSerializerBase.LegacyExpressionTable legacyExpressionTable = (CodeDomSerializerBase.LegacyExpressionTable)manager.Context[typeof(CodeDomSerializerBase.LegacyExpressionTable)];
				if (legacyExpressionTable == null)
				{
					legacyExpressionTable = new CodeDomSerializerBase.LegacyExpressionTable();
					manager.Context.Append(legacyExpressionTable);
				}
				legacyExpressionTable[value] = value;
			}
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x00064F04 File Offset: 0x00063104
		[Conditional("DEBUG")]
		internal static void Trace(string message, params object[] values)
		{
			if (CodeDomSerializerBase.traceSerialization.TraceVerbose)
			{
				int indentLevel = 0;
				int indentLevel2 = Debug.IndentLevel;
				if (CodeDomSerializerBase.traceScope != null)
				{
					indentLevel = CodeDomSerializerBase.traceScope.Count;
				}
				try
				{
					Debug.IndentLevel = indentLevel;
				}
				finally
				{
					Debug.IndentLevel = indentLevel2;
				}
			}
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x00064F58 File Offset: 0x00063158
		[Conditional("DEBUG")]
		internal static void Trace(CodeTypeDeclaration typeDecl)
		{
			if (CodeDomSerializerBase.traceSerialization.TraceInfo)
			{
				StringWriter writer = new StringWriter(CultureInfo.InvariantCulture);
				new CSharpCodeProvider().GenerateCodeFromType(typeDecl, writer, new CodeGeneratorOptions());
			}
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x00064F90 File Offset: 0x00063190
		[Conditional("DEBUG")]
		internal static void TraceError(string message, params object[] values)
		{
			if (CodeDomSerializerBase.traceSerialization.TraceError)
			{
				string text = string.Empty;
				if (CodeDomSerializerBase.traceScope != null)
				{
					foreach (object obj in CodeDomSerializerBase.traceScope)
					{
						string str = (string)obj;
						if (text.Length > 0)
						{
							text = "/" + text;
						}
						text = str + text;
					}
				}
			}
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x00065018 File Offset: 0x00063218
		[Conditional("DEBUG")]
		internal static void TraceErrorIf(bool condition, string message, params object[] values)
		{
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x00065018 File Offset: 0x00063218
		[Conditional("DEBUG")]
		internal static void TraceIf(bool condition, string message, params object[] values)
		{
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x0006501C File Offset: 0x0006321C
		internal static IDisposable TraceScope(string name)
		{
			return default(CodeDomSerializerBase.TracingScope);
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x00065038 File Offset: 0x00063238
		[Conditional("DEBUG")]
		internal static void TraceWarning(string message, params object[] values)
		{
			if (CodeDomSerializerBase.traceSerialization.TraceWarning)
			{
				string text = string.Empty;
				if (CodeDomSerializerBase.traceScope != null)
				{
					foreach (object obj in CodeDomSerializerBase.traceScope)
					{
						string str = (string)obj;
						if (text.Length > 0)
						{
							text = "/" + text;
						}
						text = str + text;
					}
				}
			}
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x00065018 File Offset: 0x00063218
		[Conditional("DEBUG")]
		internal static void TraceWarningIf(bool condition, string message, params object[] values)
		{
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x000650C0 File Offset: 0x000632C0
		private static void AddStatement(IDictionary table, string name, CodeStatement statement)
		{
			CodeDomSerializerBase.OrderedCodeStatementCollection orderedCodeStatementCollection;
			if (table.Contains(name))
			{
				orderedCodeStatementCollection = (CodeDomSerializerBase.OrderedCodeStatementCollection)table[name];
			}
			else
			{
				orderedCodeStatementCollection = new CodeDomSerializerBase.OrderedCodeStatementCollection();
				orderedCodeStatementCollection.Order = table.Count;
				orderedCodeStatementCollection.Name = name;
				table[name] = orderedCodeStatementCollection;
			}
			orderedCodeStatementCollection.Add(statement);
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x00065110 File Offset: 0x00063310
		internal static Type GetType(IDesignerSerializationManager manager, string name, Dictionary<string, string> names)
		{
			Type result = null;
			if (names != null && names.ContainsKey(name))
			{
				string text = names[name];
				if (manager != null && !string.IsNullOrEmpty(text))
				{
					result = manager.GetType(text);
				}
			}
			return result;
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x00065147 File Offset: 0x00063347
		internal static void FillStatementTable(IDesignerSerializationManager manager, IDictionary table, CodeStatementCollection statements)
		{
			CodeDomSerializerBase.FillStatementTable(manager, table, null, statements, null);
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x00065154 File Offset: 0x00063354
		internal static void FillStatementTable(IDesignerSerializationManager manager, IDictionary table, Dictionary<string, string> names, CodeStatementCollection statements, string className)
		{
			using (CodeDomSerializerBase.TraceScope("CodeDomSerializerBase::FillStatementTable"))
			{
				foreach (object obj in statements)
				{
					CodeStatement codeStatement = (CodeStatement)obj;
					CodeExpression codeExpression = null;
					CodeAssignStatement codeAssignStatement;
					CodeAttachEventStatement codeAttachEventStatement;
					CodeRemoveEventStatement codeRemoveEventStatement;
					CodeExpressionStatement codeExpressionStatement;
					CodeVariableDeclarationStatement codeVariableDeclarationStatement;
					if ((codeAssignStatement = (codeStatement as CodeAssignStatement)) != null)
					{
						codeExpression = codeAssignStatement.Left;
					}
					else if ((codeAttachEventStatement = (codeStatement as CodeAttachEventStatement)) != null)
					{
						codeExpression = codeAttachEventStatement.Event;
					}
					else if ((codeRemoveEventStatement = (codeStatement as CodeRemoveEventStatement)) != null)
					{
						codeExpression = codeRemoveEventStatement.Event;
					}
					else if ((codeExpressionStatement = (codeStatement as CodeExpressionStatement)) != null)
					{
						codeExpression = codeExpressionStatement.Expression;
					}
					else if ((codeVariableDeclarationStatement = (codeStatement as CodeVariableDeclarationStatement)) != null)
					{
						CodeDomSerializerBase.AddStatement(table, codeVariableDeclarationStatement.Name, codeVariableDeclarationStatement);
						if (names != null && codeVariableDeclarationStatement.Type != null && !string.IsNullOrEmpty(codeVariableDeclarationStatement.Type.BaseType))
						{
							names[codeVariableDeclarationStatement.Name] = CodeDomSerializerBase.GetTypeNameFromCodeTypeReference(manager, codeVariableDeclarationStatement.Type);
						}
						codeExpression = null;
					}
					if (codeExpression != null)
					{
						CodeFieldReferenceExpression codeFieldReferenceExpression;
						bool flag;
						CodePropertyReferenceExpression codePropertyReferenceExpression;
						for (;;)
						{
							CodeCastExpression codeCastExpression;
							CodeDelegateCreateExpression codeDelegateCreateExpression;
							CodeDelegateInvokeExpression codeDelegateInvokeExpression;
							CodeDirectionExpression codeDirectionExpression;
							CodeEventReferenceExpression codeEventReferenceExpression;
							CodeMethodInvokeExpression codeMethodInvokeExpression;
							CodeMethodReferenceExpression codeMethodReferenceExpression;
							CodeArrayIndexerExpression codeArrayIndexerExpression;
							if ((codeCastExpression = (codeExpression as CodeCastExpression)) != null)
							{
								codeExpression = codeCastExpression.Expression;
							}
							else if ((codeDelegateCreateExpression = (codeExpression as CodeDelegateCreateExpression)) != null)
							{
								codeExpression = codeDelegateCreateExpression.TargetObject;
							}
							else if ((codeDelegateInvokeExpression = (codeExpression as CodeDelegateInvokeExpression)) != null)
							{
								codeExpression = codeDelegateInvokeExpression.TargetObject;
							}
							else if ((codeDirectionExpression = (codeExpression as CodeDirectionExpression)) != null)
							{
								codeExpression = codeDirectionExpression.Expression;
							}
							else if ((codeEventReferenceExpression = (codeExpression as CodeEventReferenceExpression)) != null)
							{
								codeExpression = codeEventReferenceExpression.TargetObject;
							}
							else if ((codeMethodInvokeExpression = (codeExpression as CodeMethodInvokeExpression)) != null)
							{
								codeExpression = codeMethodInvokeExpression.Method;
							}
							else if ((codeMethodReferenceExpression = (codeExpression as CodeMethodReferenceExpression)) != null)
							{
								codeExpression = codeMethodReferenceExpression.TargetObject;
							}
							else if ((codeArrayIndexerExpression = (codeExpression as CodeArrayIndexerExpression)) != null)
							{
								codeExpression = codeArrayIndexerExpression.TargetObject;
							}
							else if ((codeFieldReferenceExpression = (codeExpression as CodeFieldReferenceExpression)) != null)
							{
								flag = false;
								if (codeFieldReferenceExpression.TargetObject is CodeThisReferenceExpression)
								{
									break;
								}
								codeExpression = codeFieldReferenceExpression.TargetObject;
							}
							else
							{
								if ((codePropertyReferenceExpression = (codeExpression as CodePropertyReferenceExpression)) == null)
								{
									goto IL_29A;
								}
								if (codePropertyReferenceExpression.TargetObject is CodeThisReferenceExpression && (names == null || names.ContainsKey(codePropertyReferenceExpression.PropertyName)))
								{
									goto IL_278;
								}
								codeExpression = codePropertyReferenceExpression.TargetObject;
							}
						}
						Type type = CodeDomSerializerBase.GetType(manager, codeFieldReferenceExpression.FieldName, names);
						if (type != null)
						{
							CodeDomSerializer codeDomSerializer = manager.GetSerializer(type, typeof(CodeDomSerializer)) as CodeDomSerializer;
							if (codeDomSerializer != null)
							{
								string targetComponentName = codeDomSerializer.GetTargetComponentName(codeStatement, codeExpression, type);
								if (!string.IsNullOrEmpty(targetComponentName))
								{
									CodeDomSerializerBase.AddStatement(table, targetComponentName, codeStatement);
									flag = true;
								}
							}
						}
						if (!flag)
						{
							CodeDomSerializerBase.AddStatement(table, codeFieldReferenceExpression.FieldName, codeStatement);
							continue;
						}
						continue;
						IL_278:
						CodeDomSerializerBase.AddStatement(table, codePropertyReferenceExpression.PropertyName, codeStatement);
						continue;
						IL_29A:
						CodeVariableReferenceExpression codeVariableReferenceExpression;
						if ((codeVariableReferenceExpression = (codeExpression as CodeVariableReferenceExpression)) != null)
						{
							bool flag2 = false;
							if (names != null)
							{
								Type type2 = CodeDomSerializerBase.GetType(manager, codeVariableReferenceExpression.VariableName, names);
								if (type2 != null)
								{
									CodeDomSerializer codeDomSerializer2 = manager.GetSerializer(type2, typeof(CodeDomSerializer)) as CodeDomSerializer;
									if (codeDomSerializer2 != null)
									{
										string targetComponentName2 = codeDomSerializer2.GetTargetComponentName(codeStatement, codeExpression, type2);
										if (!string.IsNullOrEmpty(targetComponentName2))
										{
											CodeDomSerializerBase.AddStatement(table, targetComponentName2, codeStatement);
											flag2 = true;
										}
									}
								}
							}
							else
							{
								CodeDomSerializerBase.AddStatement(table, codeVariableReferenceExpression.VariableName, codeStatement);
								flag2 = true;
							}
							if (!flag2)
							{
								manager.ReportError(new CodeDomSerializerException(SR.GetString("SerializerUndeclaredName", new object[]
								{
									codeVariableReferenceExpression.VariableName
								}), manager));
							}
						}
						else if ((codeExpression is CodeThisReferenceExpression || codeExpression is CodeBaseReferenceExpression) && className != null)
						{
							CodeDomSerializerBase.AddStatement(table, className, codeStatement);
						}
					}
				}
			}
		}

		// Token: 0x040009D6 RID: 2518
		private static readonly Attribute[] runTimeProperties = new Attribute[]
		{
			DesignOnlyAttribute.No
		};

		// Token: 0x040009D7 RID: 2519
		private static readonly CodeThisReferenceExpression thisRef = new CodeThisReferenceExpression();

		// Token: 0x040009D8 RID: 2520
		private static TraceSwitch traceSerialization = new TraceSwitch("DesignerSerialization", "Trace design time serialization");

		// Token: 0x040009D9 RID: 2521
		private static Stack traceScope;

		// Token: 0x020004A2 RID: 1186
		private class LegacyExpressionTable : Hashtable
		{
		}

		// Token: 0x020004A3 RID: 1187
		private struct TracingScope : IDisposable
		{
			// Token: 0x06002B9D RID: 11165 RVA: 0x00104A52 File Offset: 0x00102C52
			public void Dispose()
			{
				if (CodeDomSerializerBase.traceScope != null)
				{
					CodeDomSerializerBase.traceScope.Pop();
				}
			}
		}

		// Token: 0x020004A4 RID: 1188
		internal class OrderedCodeStatementCollection : CodeStatementCollection
		{
			// Token: 0x04001E41 RID: 7745
			public int Order;

			// Token: 0x04001E42 RID: 7746
			public string Name;
		}
	}
}
