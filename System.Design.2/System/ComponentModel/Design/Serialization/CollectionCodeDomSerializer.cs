using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Design;
using System.Globalization;
using System.Reflection;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x020001DA RID: 474
	public class CollectionCodeDomSerializer : CodeDomSerializer
	{
		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x060011EA RID: 4586 RVA: 0x00065958 File Offset: 0x00063B58
		internal new static CollectionCodeDomSerializer Default
		{
			get
			{
				if (CollectionCodeDomSerializer.defaultSerializer == null)
				{
					CollectionCodeDomSerializer.defaultSerializer = new CollectionCodeDomSerializer();
				}
				return CollectionCodeDomSerializer.defaultSerializer;
			}
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x00065970 File Offset: 0x00063B70
		private ICollection GetCollectionDelta(ICollection original, ICollection modified)
		{
			if (original == null || modified == null || original.Count == 0)
			{
				return modified;
			}
			IEnumerator enumerator = modified.GetEnumerator();
			if (enumerator == null)
			{
				return modified;
			}
			IDictionary dictionary = new HybridDictionary();
			foreach (object key in original)
			{
				if (dictionary.Contains(key))
				{
					int num = (int)dictionary[key];
					dictionary[key] = num + 1;
				}
				else
				{
					dictionary.Add(key, 1);
				}
			}
			ArrayList arrayList = null;
			int num2 = 0;
			while (num2 < modified.Count && enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				if (dictionary.Contains(obj))
				{
					if (arrayList == null)
					{
						arrayList = new ArrayList();
						enumerator.Reset();
						int num3 = 0;
						while (num3 < num2 && enumerator.MoveNext())
						{
							arrayList.Add(enumerator.Current);
							num3++;
						}
						enumerator.MoveNext();
					}
					int num4 = (int)dictionary[obj];
					if (--num4 == 0)
					{
						dictionary.Remove(obj);
					}
					else
					{
						dictionary[obj] = num4;
					}
				}
				else if (arrayList != null)
				{
					arrayList.Add(obj);
				}
				num2++;
			}
			if (arrayList != null)
			{
				return arrayList;
			}
			return modified;
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x00065AD4 File Offset: 0x00063CD4
		protected bool MethodSupportsSerialization(MethodInfo method)
		{
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			object[] customAttributes = method.GetCustomAttributes(typeof(DesignerSerializationVisibilityAttribute), true);
			if (customAttributes.Length != 0)
			{
				DesignerSerializationVisibilityAttribute designerSerializationVisibilityAttribute = (DesignerSerializationVisibilityAttribute)customAttributes[0];
				if (designerSerializationVisibilityAttribute != null && designerSerializationVisibilityAttribute.Visibility == DesignerSerializationVisibility.Hidden)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060011ED RID: 4589 RVA: 0x00065B24 File Offset: 0x00063D24
		public override object Serialize(IDesignerSerializationManager manager, object value)
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			object obj = null;
			using (CodeDomSerializerBase.TraceScope("CollectionCodeDomSerializer::Serialize"))
			{
				ExpressionContext expressionContext = manager.Context[typeof(ExpressionContext)] as ExpressionContext;
				PropertyDescriptor propertyDescriptor = manager.Context[typeof(PropertyDescriptor)] as PropertyDescriptor;
				CodeExpression codeExpression;
				if (expressionContext != null && expressionContext.PresetValue == value && propertyDescriptor != null && propertyDescriptor.PropertyType == expressionContext.ExpressionType)
				{
					codeExpression = expressionContext.Expression;
				}
				else
				{
					codeExpression = null;
					expressionContext = null;
					propertyDescriptor = null;
				}
				ICollection collection = value as ICollection;
				if (collection != null)
				{
					ICollection valuesToSerialize = collection;
					InheritedPropertyDescriptor inheritedPropertyDescriptor = propertyDescriptor as InheritedPropertyDescriptor;
					Type type = (expressionContext == null) ? collection.GetType() : expressionContext.ExpressionType;
					bool flag = typeof(Array).IsAssignableFrom(type);
					if (codeExpression == null && !flag)
					{
						bool flag2;
						codeExpression = base.SerializeCreationExpression(manager, collection, out flag2);
						if (flag2)
						{
							return codeExpression;
						}
					}
					if (codeExpression != null || flag)
					{
						if (inheritedPropertyDescriptor != null && !flag)
						{
							valuesToSerialize = this.GetCollectionDelta(inheritedPropertyDescriptor.OriginalValue as ICollection, collection);
						}
						obj = this.SerializeCollection(manager, codeExpression, type, collection, valuesToSerialize);
						if (codeExpression != null && this.ShouldClearCollection(manager, collection))
						{
							CodeStatementCollection codeStatementCollection = obj as CodeStatementCollection;
							if (collection.Count > 0 && (obj == null || (codeStatementCollection != null && codeStatementCollection.Count == 0)))
							{
								return null;
							}
							if (codeStatementCollection == null)
							{
								codeStatementCollection = new CodeStatementCollection();
								CodeStatement codeStatement = obj as CodeStatement;
								if (codeStatement != null)
								{
									codeStatementCollection.Add(codeStatement);
								}
								obj = codeStatementCollection;
							}
							if (codeStatementCollection != null)
							{
								CodeMethodInvokeExpression expression = new CodeMethodInvokeExpression(codeExpression, "Clear", new CodeExpression[0]);
								CodeExpressionStatement value2 = new CodeExpressionStatement(expression);
								codeStatementCollection.Insert(0, value2);
							}
						}
					}
				}
			}
			return obj;
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x00065D18 File Offset: 0x00063F18
		private static MethodInfo ChooseMethodByType(TypeDescriptionProvider provider, List<MethodInfo> methods, ICollection values)
		{
			MethodInfo methodInfo = null;
			Type type = null;
			foreach (object instance in values)
			{
				Type reflectionType = provider.GetReflectionType(instance);
				MethodInfo methodInfo2 = null;
				Type type2 = null;
				if (methodInfo == null || (type != null && !type.IsAssignableFrom(reflectionType)))
				{
					foreach (MethodInfo methodInfo3 in methods)
					{
						ParameterInfo parameterInfo = methodInfo3.GetParameters()[0];
						if (parameterInfo != null)
						{
							Type type3 = parameterInfo.ParameterType.IsArray ? parameterInfo.ParameterType.GetElementType() : parameterInfo.ParameterType;
							if (type3 != null && type3.IsAssignableFrom(reflectionType))
							{
								if (methodInfo != null)
								{
									if (type3.IsAssignableFrom(type))
									{
										methodInfo = methodInfo3;
										type = type3;
										break;
									}
								}
								else if (methodInfo2 == null)
								{
									methodInfo2 = methodInfo3;
									type2 = type3;
								}
								else
								{
									bool flag = type2.IsAssignableFrom(type3);
									methodInfo2 = (flag ? methodInfo3 : methodInfo2);
									type2 = (flag ? type3 : type2);
								}
							}
						}
					}
				}
				if (methodInfo == null)
				{
					methodInfo = methodInfo2;
					type = type2;
				}
			}
			return methodInfo;
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x00065EA4 File Offset: 0x000640A4
		protected virtual object SerializeCollection(IDesignerSerializationManager manager, CodeExpression targetExpression, Type targetType, ICollection originalCollection, ICollection valuesToSerialize)
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			if (targetType == null)
			{
				throw new ArgumentNullException("targetType");
			}
			if (originalCollection == null)
			{
				throw new ArgumentNullException("originalCollection");
			}
			if (valuesToSerialize == null)
			{
				throw new ArgumentNullException("valuesToSerialize");
			}
			object result = null;
			bool flag = false;
			if (typeof(Array).IsAssignableFrom(targetType))
			{
				CodeArrayCreateExpression codeArrayCreateExpression = this.SerializeArray(manager, targetType, originalCollection, valuesToSerialize);
				if (codeArrayCreateExpression != null)
				{
					if (targetExpression != null)
					{
						result = new CodeAssignStatement(targetExpression, codeArrayCreateExpression);
					}
					else
					{
						result = codeArrayCreateExpression;
					}
				}
			}
			else if (valuesToSerialize.Count > 0)
			{
				TypeDescriptionProvider typeDescriptionProvider = CodeDomSerializerBase.GetTargetFrameworkProvider(manager, originalCollection);
				if (typeDescriptionProvider == null)
				{
					typeDescriptionProvider = TypeDescriptor.GetProvider(originalCollection);
				}
				MethodInfo[] methods = typeDescriptionProvider.GetReflectionType(originalCollection).GetMethods(BindingFlags.Instance | BindingFlags.Public);
				List<MethodInfo> list = new List<MethodInfo>();
				List<MethodInfo> list2 = new List<MethodInfo>();
				foreach (MethodInfo methodInfo in methods)
				{
					if (methodInfo.Name.Equals("AddRange"))
					{
						ParameterInfo[] parameters = methodInfo.GetParameters();
						if (parameters.Length == 1 && parameters[0].ParameterType.IsArray && this.MethodSupportsSerialization(methodInfo))
						{
							list.Add(methodInfo);
						}
					}
					if (methodInfo.Name.Equals("Add"))
					{
						ParameterInfo[] parameters = methodInfo.GetParameters();
						if (parameters.Length == 1 && this.MethodSupportsSerialization(methodInfo))
						{
							list2.Add(methodInfo);
						}
					}
				}
				MethodInfo methodInfo2 = CollectionCodeDomSerializer.ChooseMethodByType(typeDescriptionProvider, list, valuesToSerialize);
				if (methodInfo2 != null)
				{
					Type runtimeType = typeDescriptionProvider.GetRuntimeType(methodInfo2.GetParameters()[0].ParameterType.GetElementType());
					result = this.SerializeViaAddRange(manager, targetExpression, targetType, runtimeType, valuesToSerialize);
					flag = true;
				}
				else
				{
					MethodInfo methodInfo3 = CollectionCodeDomSerializer.ChooseMethodByType(typeDescriptionProvider, list2, valuesToSerialize);
					if (methodInfo3 != null)
					{
						Type runtimeType2 = typeDescriptionProvider.GetRuntimeType(methodInfo3.GetParameters()[0].ParameterType);
						result = this.SerializeViaAdd(manager, targetExpression, targetType, runtimeType2, valuesToSerialize);
						flag = true;
					}
				}
				if (!flag && originalCollection.GetType().IsSerializable)
				{
					result = base.SerializeToResourceExpression(manager, originalCollection, false);
				}
			}
			return result;
		}

		// Token: 0x060011F0 RID: 4592 RVA: 0x000660AC File Offset: 0x000642AC
		private CodeArrayCreateExpression SerializeArray(IDesignerSerializationManager manager, Type targetType, ICollection array, ICollection valuesToSerialize)
		{
			CodeArrayCreateExpression result = null;
			using (CodeDomSerializerBase.TraceScope("CollectionCodeDomSerializer::SerializeArray"))
			{
				if (((Array)array).Rank != 1)
				{
					manager.ReportError(SR.GetString("SerializerInvalidArrayRank", new object[]
					{
						((Array)array).Rank.ToString(CultureInfo.InvariantCulture)
					}));
				}
				else
				{
					Type elementType = targetType.GetElementType();
					CodeTypeReference createType = new CodeTypeReference(elementType);
					CodeArrayCreateExpression codeArrayCreateExpression = new CodeArrayCreateExpression();
					codeArrayCreateExpression.CreateType = createType;
					bool flag = true;
					foreach (object obj in valuesToSerialize)
					{
						if (obj is IComponent && TypeDescriptor.GetAttributes(obj).Contains(InheritanceAttribute.InheritedReadOnly))
						{
							flag = false;
							break;
						}
						CodeExpression codeExpression = null;
						ExpressionContext expressionContext = null;
						ExpressionContext expressionContext2 = manager.Context[typeof(ExpressionContext)] as ExpressionContext;
						if (expressionContext2 != null)
						{
							expressionContext = new ExpressionContext(expressionContext2.Expression, elementType, expressionContext2.Owner);
							manager.Context.Push(expressionContext);
						}
						try
						{
							codeExpression = base.SerializeToExpression(manager, obj);
						}
						finally
						{
							if (expressionContext != null)
							{
								manager.Context.Pop();
							}
						}
						if (codeExpression == null)
						{
							flag = false;
							break;
						}
						if (obj != null && obj.GetType() != elementType)
						{
							codeExpression = new CodeCastExpression(elementType, codeExpression);
						}
						codeArrayCreateExpression.Initializers.Add(codeExpression);
					}
					if (flag)
					{
						result = codeArrayCreateExpression;
					}
				}
			}
			return result;
		}

		// Token: 0x060011F1 RID: 4593 RVA: 0x0006628C File Offset: 0x0006448C
		private object SerializeViaAdd(IDesignerSerializationManager manager, CodeExpression targetExpression, Type targetType, Type elementType, ICollection valuesToSerialize)
		{
			CodeStatementCollection codeStatementCollection = new CodeStatementCollection();
			using (CodeDomSerializerBase.TraceScope("CollectionCodeDomSerializer::SerializeViaAdd"))
			{
				CodeMethodReferenceExpression method = new CodeMethodReferenceExpression(targetExpression, "Add");
				if (valuesToSerialize.Count > 0)
				{
					ExpressionContext expressionContext = manager.Context[typeof(ExpressionContext)] as ExpressionContext;
					foreach (object obj in valuesToSerialize)
					{
						bool flag = !(obj is IComponent);
						if (!flag)
						{
							InheritanceAttribute inheritanceAttribute = (InheritanceAttribute)TypeDescriptor.GetAttributes(obj)[typeof(InheritanceAttribute)];
							flag = (inheritanceAttribute == null || inheritanceAttribute.InheritanceLevel != InheritanceLevel.InheritedReadOnly);
						}
						if (flag)
						{
							CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression();
							codeMethodInvokeExpression.Method = method;
							CodeExpression codeExpression = null;
							ExpressionContext expressionContext2 = null;
							if (expressionContext != null)
							{
								expressionContext2 = new ExpressionContext(expressionContext.Expression, elementType, expressionContext.Owner);
								manager.Context.Push(expressionContext2);
							}
							try
							{
								codeExpression = base.SerializeToExpression(manager, obj);
							}
							finally
							{
								if (expressionContext2 != null)
								{
									manager.Context.Pop();
								}
							}
							if (obj != null && !elementType.IsAssignableFrom(obj.GetType()) && obj.GetType().IsPrimitive)
							{
								codeExpression = new CodeCastExpression(elementType, codeExpression);
							}
							if (codeExpression != null)
							{
								codeMethodInvokeExpression.Parameters.Add(codeExpression);
								codeStatementCollection.Add(codeMethodInvokeExpression);
							}
						}
					}
				}
			}
			return codeStatementCollection;
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x00066468 File Offset: 0x00064668
		private object SerializeViaAddRange(IDesignerSerializationManager manager, CodeExpression targetExpression, Type targetType, Type elementType, ICollection valuesToSerialize)
		{
			CodeStatementCollection codeStatementCollection = new CodeStatementCollection();
			using (CodeDomSerializerBase.TraceScope("CollectionCodeDomSerializer::SerializeViaAddRange"))
			{
				if (valuesToSerialize.Count > 0)
				{
					ArrayList arrayList = new ArrayList(valuesToSerialize.Count);
					ExpressionContext expressionContext = manager.Context[typeof(ExpressionContext)] as ExpressionContext;
					foreach (object obj in valuesToSerialize)
					{
						bool flag = !(obj is IComponent);
						if (!flag)
						{
							InheritanceAttribute inheritanceAttribute = (InheritanceAttribute)TypeDescriptor.GetAttributes(obj)[typeof(InheritanceAttribute)];
							flag = (inheritanceAttribute == null || inheritanceAttribute.InheritanceLevel != InheritanceLevel.InheritedReadOnly);
						}
						if (flag)
						{
							CodeExpression codeExpression = null;
							ExpressionContext expressionContext2 = null;
							if (expressionContext != null)
							{
								expressionContext2 = new ExpressionContext(expressionContext.Expression, elementType, expressionContext.Owner);
								manager.Context.Push(expressionContext2);
							}
							try
							{
								codeExpression = base.SerializeToExpression(manager, obj);
							}
							finally
							{
								if (expressionContext2 != null)
								{
									manager.Context.Pop();
								}
							}
							if (codeExpression != null)
							{
								if (obj != null && !elementType.IsAssignableFrom(obj.GetType()))
								{
									codeExpression = new CodeCastExpression(elementType, codeExpression);
								}
								arrayList.Add(codeExpression);
							}
						}
					}
					if (arrayList.Count > 0)
					{
						CodeTypeReference createType = new CodeTypeReference(elementType);
						CodeArrayCreateExpression codeArrayCreateExpression = new CodeArrayCreateExpression();
						codeArrayCreateExpression.CreateType = createType;
						foreach (object obj2 in arrayList)
						{
							CodeExpression value = (CodeExpression)obj2;
							codeArrayCreateExpression.Initializers.Add(value);
						}
						CodeMethodReferenceExpression method = new CodeMethodReferenceExpression(targetExpression, "AddRange");
						codeStatementCollection.Add(new CodeExpressionStatement(new CodeMethodInvokeExpression
						{
							Method = method,
							Parameters = 
							{
								codeArrayCreateExpression
							}
						}));
					}
				}
			}
			return codeStatementCollection;
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x000666D4 File Offset: 0x000648D4
		private bool ShouldClearCollection(IDesignerSerializationManager manager, ICollection collection)
		{
			bool flag = false;
			PropertyDescriptor propertyDescriptor = manager.Properties["ClearCollections"];
			if (propertyDescriptor != null && propertyDescriptor.PropertyType == typeof(bool) && (bool)propertyDescriptor.GetValue(manager))
			{
				flag = true;
			}
			if (!flag)
			{
				SerializeAbsoluteContext serializeAbsoluteContext = (SerializeAbsoluteContext)manager.Context[typeof(SerializeAbsoluteContext)];
				PropertyDescriptor member = manager.Context[typeof(PropertyDescriptor)] as PropertyDescriptor;
				if (serializeAbsoluteContext != null && serializeAbsoluteContext.ShouldSerialize(member))
				{
					flag = true;
				}
			}
			if (flag)
			{
				MethodInfo method = TypeDescriptor.GetReflectionType(collection).GetMethod("Clear", BindingFlags.Instance | BindingFlags.Public, null, new Type[0], null);
				if (method == null || !this.MethodSupportsSerialization(method))
				{
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x040009E4 RID: 2532
		private static CollectionCodeDomSerializer defaultSerializer;
	}
}
