using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Web.UI;

namespace System.Web.Compilation
{
	// Token: 0x02000831 RID: 2097
	internal static class CodeDomUtility
	{
		// Token: 0x06006403 RID: 25603 RVA: 0x0015E68C File Offset: 0x0015C88C
		internal static CodeExpression GenerateExpressionForValue(PropertyInfo propertyInfo, object value, Type valueType)
		{
			CodeExpression result = null;
			if (valueType == null)
			{
				throw new ArgumentNullException("valueType");
			}
			PropertyDescriptor propertyDescriptor = null;
			if (propertyInfo != null)
			{
				propertyDescriptor = TypeDescriptor.GetProperties(propertyInfo.ReflectedType)[propertyInfo.Name];
			}
			if (valueType == typeof(string) && value is string)
			{
				bool enabled = CodeDomUtility.WebFormsCompilation.Enabled;
				result = new CodePrimitiveExpression((string)value);
			}
			else if (valueType.IsPrimitive)
			{
				bool enabled2 = CodeDomUtility.WebFormsCompilation.Enabled;
				result = new CodePrimitiveExpression(value);
			}
			else if (propertyInfo == null && valueType == typeof(object) && (value == null || value.GetType().IsPrimitive))
			{
				bool enabled3 = CodeDomUtility.WebFormsCompilation.Enabled;
				result = new CodePrimitiveExpression(value);
			}
			else if (valueType.IsArray)
			{
				bool enabled4 = CodeDomUtility.WebFormsCompilation.Enabled;
				Array array = (Array)value;
				CodeArrayCreateExpression codeArrayCreateExpression = new CodeArrayCreateExpression();
				codeArrayCreateExpression.CreateType = new CodeTypeReference(valueType.GetElementType());
				if (array != null)
				{
					foreach (object value2 in array)
					{
						codeArrayCreateExpression.Initializers.Add(CodeDomUtility.GenerateExpressionForValue(null, value2, valueType.GetElementType()));
					}
				}
				result = codeArrayCreateExpression;
			}
			else if (valueType == typeof(Type))
			{
				result = new CodeTypeOfExpression((Type)value);
			}
			else
			{
				bool enabled5 = CodeDomUtility.WebFormsCompilation.Enabled;
				TypeConverter converter;
				if (propertyDescriptor != null)
				{
					converter = propertyDescriptor.Converter;
				}
				else
				{
					converter = TypeDescriptor.GetConverter(valueType);
				}
				bool flag = false;
				if (converter != null)
				{
					InstanceDescriptor instanceDescriptor = null;
					if (converter.CanConvertTo(typeof(InstanceDescriptor)))
					{
						instanceDescriptor = (InstanceDescriptor)converter.ConvertTo(value, typeof(InstanceDescriptor));
					}
					if (instanceDescriptor != null)
					{
						bool enabled6 = CodeDomUtility.WebFormsCompilation.Enabled;
						if (instanceDescriptor.MemberInfo is FieldInfo)
						{
							bool enabled7 = CodeDomUtility.WebFormsCompilation.Enabled;
							CodeFieldReferenceExpression codeFieldReferenceExpression = new CodeFieldReferenceExpression(CodeDomUtility.BuildGlobalCodeTypeReferenceExpression(instanceDescriptor.MemberInfo.DeclaringType.FullName), instanceDescriptor.MemberInfo.Name);
							result = codeFieldReferenceExpression;
							flag = true;
						}
						else if (instanceDescriptor.MemberInfo is PropertyInfo)
						{
							bool enabled8 = CodeDomUtility.WebFormsCompilation.Enabled;
							CodePropertyReferenceExpression codePropertyReferenceExpression = new CodePropertyReferenceExpression(CodeDomUtility.BuildGlobalCodeTypeReferenceExpression(instanceDescriptor.MemberInfo.DeclaringType.FullName), instanceDescriptor.MemberInfo.Name);
							result = codePropertyReferenceExpression;
							flag = true;
						}
						else
						{
							object[] array2 = new object[instanceDescriptor.Arguments.Count];
							instanceDescriptor.Arguments.CopyTo(array2, 0);
							CodeExpression[] array3 = new CodeExpression[array2.Length];
							if (instanceDescriptor.MemberInfo is MethodInfo)
							{
								MethodInfo methodInfo = (MethodInfo)instanceDescriptor.MemberInfo;
								ParameterInfo[] parameters = methodInfo.GetParameters();
								for (int i = 0; i < array2.Length; i++)
								{
									array3[i] = CodeDomUtility.GenerateExpressionForValue(null, array2[i], parameters[i].ParameterType);
								}
								bool enabled9 = CodeDomUtility.WebFormsCompilation.Enabled;
								CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(CodeDomUtility.BuildGlobalCodeTypeReferenceExpression(instanceDescriptor.MemberInfo.DeclaringType.FullName), instanceDescriptor.MemberInfo.Name, new CodeExpression[0]);
								foreach (CodeExpression value3 in array3)
								{
									codeMethodInvokeExpression.Parameters.Add(value3);
								}
								result = new CodeCastExpression(valueType, codeMethodInvokeExpression);
								flag = true;
							}
							else if (instanceDescriptor.MemberInfo is ConstructorInfo)
							{
								ConstructorInfo constructorInfo = (ConstructorInfo)instanceDescriptor.MemberInfo;
								ParameterInfo[] parameters2 = constructorInfo.GetParameters();
								for (int k = 0; k < array2.Length; k++)
								{
									array3[k] = CodeDomUtility.GenerateExpressionForValue(null, array2[k], parameters2[k].ParameterType);
								}
								bool enabled10 = CodeDomUtility.WebFormsCompilation.Enabled;
								CodeObjectCreateExpression codeObjectCreateExpression = new CodeObjectCreateExpression(instanceDescriptor.MemberInfo.DeclaringType.FullName, new CodeExpression[0]);
								foreach (CodeExpression value4 in array3)
								{
									codeObjectCreateExpression.Parameters.Add(value4);
								}
								result = codeObjectCreateExpression;
								flag = true;
							}
						}
					}
				}
				if (!flag)
				{
					if (valueType.GetMethod("Parse", new Type[]
					{
						typeof(string),
						typeof(CultureInfo)
					}) != null)
					{
						CodeMethodInvokeExpression codeMethodInvokeExpression2 = new CodeMethodInvokeExpression(CodeDomUtility.BuildGlobalCodeTypeReferenceExpression(valueType.FullName), "Parse", new CodeExpression[0]);
						string value5;
						if (converter != null)
						{
							value5 = converter.ConvertToInvariantString(value);
						}
						else
						{
							value5 = value.ToString();
						}
						codeMethodInvokeExpression2.Parameters.Add(new CodePrimitiveExpression(value5));
						codeMethodInvokeExpression2.Parameters.Add(new CodePropertyReferenceExpression(CodeDomUtility.BuildGlobalCodeTypeReferenceExpression(typeof(CultureInfo)), "InvariantCulture"));
						result = codeMethodInvokeExpression2;
					}
					else
					{
						if (!(valueType.GetMethod("Parse", new Type[]
						{
							typeof(string)
						}) != null))
						{
							throw new HttpException(SR.GetString("CantGenPropertySet", new object[]
							{
								propertyInfo.Name,
								valueType.FullName
							}));
						}
						result = new CodeMethodInvokeExpression(CodeDomUtility.BuildGlobalCodeTypeReferenceExpression(valueType.FullName), "Parse", new CodeExpression[0])
						{
							Parameters = 
							{
								new CodePrimitiveExpression(value.ToString())
							}
						};
					}
				}
			}
			return result;
		}

		// Token: 0x06006404 RID: 25604 RVA: 0x0015EC14 File Offset: 0x0015CE14
		internal static void CreatePropertySetStatements(CodeStatementCollection methodStatements, CodeStatementCollection statements, CodeExpression target, string targetPropertyName, Type destinationType, CodeExpression value, CodeLinePragma linePragma)
		{
			bool flag = false;
			if (destinationType == null)
			{
				flag = true;
			}
			if (flag)
			{
				CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression();
				CodeExpressionStatement codeExpressionStatement = new CodeExpressionStatement(codeMethodInvokeExpression);
				codeExpressionStatement.LinePragma = linePragma;
				if (targetPropertyName.Equals("Style", StringComparison.Ordinal))
				{
					targetPropertyName = "style";
				}
				codeMethodInvokeExpression.Method.TargetObject = new CodeCastExpression(typeof(IAttributeAccessor), target);
				codeMethodInvokeExpression.Method.MethodName = "SetAttribute";
				codeMethodInvokeExpression.Parameters.Add(new CodePrimitiveExpression(targetPropertyName));
				codeMethodInvokeExpression.Parameters.Add(CodeDomUtility.GenerateConvertToString(value));
				statements.Add(codeExpressionStatement);
				return;
			}
			if (destinationType.IsValueType)
			{
				statements.Add(new CodeAssignStatement(CodeDomUtility.BuildPropertyReferenceExpression(target, targetPropertyName), new CodeCastExpression(destinationType, value))
				{
					LinePragma = linePragma
				});
				return;
			}
			CodeExpression right;
			if (destinationType == typeof(string))
			{
				right = CodeDomUtility.GenerateConvertToString(value);
			}
			else
			{
				right = new CodeCastExpression(destinationType, value);
			}
			statements.Add(new CodeAssignStatement(CodeDomUtility.BuildPropertyReferenceExpression(target, targetPropertyName), right)
			{
				LinePragma = linePragma
			});
		}

		// Token: 0x06006405 RID: 25605 RVA: 0x0015ED34 File Offset: 0x0015CF34
		internal static CodeExpression GenerateConvertToString(CodeExpression value)
		{
			return new CodeMethodInvokeExpression
			{
				Method = 
				{
					TargetObject = CodeDomUtility.BuildGlobalCodeTypeReferenceExpression(typeof(Convert)),
					MethodName = "ToString"
				},
				Parameters = 
				{
					value,
					new CodePropertyReferenceExpression(CodeDomUtility.BuildGlobalCodeTypeReferenceExpression(typeof(CultureInfo)), "CurrentCulture")
				}
			};
		}

		// Token: 0x06006406 RID: 25606 RVA: 0x0015EDA4 File Offset: 0x0015CFA4
		internal static void PrependCompilerOption(CompilerParameters compilParams, string compilerOptions)
		{
			if (compilParams.CompilerOptions == null)
			{
				compilParams.CompilerOptions = compilerOptions;
				return;
			}
			compilParams.CompilerOptions = compilerOptions + " " + compilParams.CompilerOptions;
		}

		// Token: 0x06006407 RID: 25607 RVA: 0x0015EDCD File Offset: 0x0015CFCD
		internal static void AppendCompilerOption(CompilerParameters compilParams, string compilerOptions)
		{
			if (compilParams.CompilerOptions == null)
			{
				compilParams.CompilerOptions = compilerOptions;
				return;
			}
			compilParams.CompilerOptions = compilParams.CompilerOptions + " " + compilerOptions;
		}

		// Token: 0x06006408 RID: 25608 RVA: 0x0015EDF8 File Offset: 0x0015CFF8
		internal static CodeExpression BuildPropertyReferenceExpression(CodeExpression objRefExpr, string propName)
		{
			string[] array = propName.Split(new char[]
			{
				'.'
			});
			CodeExpression codeExpression = objRefExpr;
			foreach (string propertyName in array)
			{
				codeExpression = new CodePropertyReferenceExpression(codeExpression, propertyName);
			}
			return codeExpression;
		}

		// Token: 0x06006409 RID: 25609 RVA: 0x0015EE38 File Offset: 0x0015D038
		internal static CodeCastExpression BuildJSharpCastExpression(Type castType, CodeExpression expression)
		{
			return new CodeCastExpression(castType, expression)
			{
				UserData = 
				{
					{
						"CastIsBoxing",
						true
					}
				}
			};
		}

		// Token: 0x0600640A RID: 25610 RVA: 0x0015EE64 File Offset: 0x0015D064
		internal static CodeTypeReference BuildGlobalCodeTypeReference(string typeName)
		{
			return new CodeTypeReference(typeName, CodeTypeReferenceOptions.GlobalReference);
		}

		// Token: 0x0600640B RID: 25611 RVA: 0x0015EE6D File Offset: 0x0015D06D
		internal static CodeTypeReference BuildGlobalCodeTypeReference(Type type)
		{
			return new CodeTypeReference(type, CodeTypeReferenceOptions.GlobalReference);
		}

		// Token: 0x0600640C RID: 25612 RVA: 0x0015EE78 File Offset: 0x0015D078
		private static CodeTypeReferenceExpression BuildGlobalCodeTypeReferenceExpression(string typeName)
		{
			CodeTypeReference type = CodeDomUtility.BuildGlobalCodeTypeReference(typeName);
			return new CodeTypeReferenceExpression(type);
		}

		// Token: 0x0600640D RID: 25613 RVA: 0x0015EE94 File Offset: 0x0015D094
		private static CodeTypeReferenceExpression BuildGlobalCodeTypeReferenceExpression(Type type)
		{
			CodeTypeReference type2 = CodeDomUtility.BuildGlobalCodeTypeReference(type);
			return new CodeTypeReferenceExpression(type2);
		}

		// Token: 0x040033D0 RID: 13264
		internal static BooleanSwitch WebFormsCompilation = new BooleanSwitch("WebFormsCompilation", "Outputs information about the WebForms compilation of ASPX templates");
	}
}
