using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace System.Xml.Serialization
{
	// Token: 0x020001B4 RID: 436
	internal class ReflectionAwareCodeGen
	{
		// Token: 0x06001E0E RID: 7694 RVA: 0x000A0C01 File Offset: 0x0009EE01
		internal ReflectionAwareCodeGen(IndentedWriter writer)
		{
			this.writer = writer;
		}

		// Token: 0x06001E0F RID: 7695 RVA: 0x000A0C10 File Offset: 0x0009EE10
		internal void WriteReflectionInit(TypeScope scope)
		{
			foreach (object obj in scope.Types)
			{
				Type type = (Type)obj;
				TypeDesc typeDesc = scope.GetTypeDesc(type);
				if (typeDesc.UseReflection)
				{
					this.WriteTypeInfo(scope, typeDesc, type);
				}
			}
		}

		// Token: 0x06001E10 RID: 7696 RVA: 0x000A0C7C File Offset: 0x0009EE7C
		private string WriteTypeInfo(TypeScope scope, TypeDesc typeDesc, Type type)
		{
			this.InitTheFirstTime();
			string csharpName = typeDesc.CSharpName;
			string text = (string)this.reflectionVariables[csharpName];
			if (text != null)
			{
				return text;
			}
			if (type.IsArray)
			{
				text = this.GenerateVariableName("array", typeDesc.CSharpName);
				TypeDesc arrayElementTypeDesc = typeDesc.ArrayElementTypeDesc;
				if (arrayElementTypeDesc.UseReflection)
				{
					string text2 = this.WriteTypeInfo(scope, arrayElementTypeDesc, scope.GetTypeFromTypeDesc(arrayElementTypeDesc));
					this.writer.WriteLine(string.Concat(new string[]
					{
						"static ",
						typeof(Type).FullName,
						" ",
						text,
						" = ",
						text2,
						".MakeArrayType();"
					}));
				}
				else
				{
					string text3 = this.WriteAssemblyInfo(type);
					this.writer.Write(string.Concat(new string[]
					{
						"static ",
						typeof(Type).FullName,
						" ",
						text,
						" = ",
						text3,
						".GetType("
					}));
					this.WriteQuotedCSharpString(type.FullName);
					this.writer.WriteLine(");");
				}
			}
			else
			{
				text = this.GenerateVariableName("type", typeDesc.CSharpName);
				Type underlyingType = Nullable.GetUnderlyingType(type);
				if (underlyingType != null)
				{
					string text4 = this.WriteTypeInfo(scope, scope.GetTypeDesc(underlyingType), underlyingType);
					this.writer.WriteLine(string.Concat(new string[]
					{
						"static ",
						typeof(Type).FullName,
						" ",
						text,
						" = typeof(System.Nullable<>).MakeGenericType(new ",
						typeof(Type).FullName,
						"[] {",
						text4,
						"});"
					}));
				}
				else
				{
					string text5 = this.WriteAssemblyInfo(type);
					this.writer.Write(string.Concat(new string[]
					{
						"static ",
						typeof(Type).FullName,
						" ",
						text,
						" = ",
						text5,
						".GetType("
					}));
					this.WriteQuotedCSharpString(type.FullName);
					this.writer.WriteLine(");");
				}
			}
			this.reflectionVariables.Add(csharpName, text);
			TypeMapping typeMappingFromTypeDesc = scope.GetTypeMappingFromTypeDesc(typeDesc);
			if (typeMappingFromTypeDesc != null)
			{
				this.WriteMappingInfo(typeMappingFromTypeDesc, text, type);
			}
			if (typeDesc.IsCollection || typeDesc.IsEnumerable)
			{
				TypeDesc arrayElementTypeDesc2 = typeDesc.ArrayElementTypeDesc;
				if (arrayElementTypeDesc2.UseReflection)
				{
					this.WriteTypeInfo(scope, arrayElementTypeDesc2, scope.GetTypeFromTypeDesc(arrayElementTypeDesc2));
				}
				this.WriteCollectionInfo(text, typeDesc, type);
			}
			return text;
		}

		// Token: 0x06001E11 RID: 7697 RVA: 0x000A0F38 File Offset: 0x0009F138
		private void InitTheFirstTime()
		{
			if (this.reflectionVariables == null)
			{
				this.reflectionVariables = new Hashtable();
				this.writer.Write(string.Format(CultureInfo.InvariantCulture, ReflectionAwareCodeGen.helperClassesForUseReflection, new object[]
				{
					"object",
					"string",
					typeof(Type).FullName,
					typeof(FieldInfo).FullName,
					typeof(PropertyInfo).FullName,
					typeof(MemberInfo).FullName,
					typeof(MemberTypes).FullName
				}));
				this.WriteDefaultIndexerInit(typeof(IList), typeof(Array).FullName, false, false);
			}
		}

		// Token: 0x06001E12 RID: 7698 RVA: 0x000A1008 File Offset: 0x0009F208
		private void WriteMappingInfo(TypeMapping mapping, string typeVariable, Type type)
		{
			string csharpName = mapping.TypeDesc.CSharpName;
			if (mapping is StructMapping)
			{
				StructMapping structMapping = mapping as StructMapping;
				for (int i = 0; i < structMapping.Members.Length; i++)
				{
					MemberMapping memberMapping = structMapping.Members[i];
					string text = this.WriteMemberInfo(type, csharpName, typeVariable, memberMapping.Name);
					if (memberMapping.CheckShouldPersist)
					{
						string memberName = "ShouldSerialize" + memberMapping.Name;
						text = this.WriteMethodInfo(csharpName, typeVariable, memberName, false, new string[0]);
					}
					if (memberMapping.CheckSpecified != SpecifiedAccessor.None)
					{
						string memberName2 = memberMapping.Name + "Specified";
						text = this.WriteMemberInfo(type, csharpName, typeVariable, memberName2);
					}
					if (memberMapping.ChoiceIdentifier != null)
					{
						string memberName3 = memberMapping.ChoiceIdentifier.MemberName;
						text = this.WriteMemberInfo(type, csharpName, typeVariable, memberName3);
					}
				}
				return;
			}
			if (mapping is EnumMapping)
			{
				FieldInfo[] fields = type.GetFields();
				for (int j = 0; j < fields.Length; j++)
				{
					this.WriteMemberInfo(type, csharpName, typeVariable, fields[j].Name);
				}
			}
		}

		// Token: 0x06001E13 RID: 7699 RVA: 0x000A1114 File Offset: 0x0009F314
		private void WriteCollectionInfo(string typeVariable, TypeDesc typeDesc, Type type)
		{
			string csharpName = CodeIdentifier.GetCSharpName(type);
			string csharpName2 = typeDesc.ArrayElementTypeDesc.CSharpName;
			bool useReflection = typeDesc.ArrayElementTypeDesc.UseReflection;
			if (typeDesc.IsCollection)
			{
				this.WriteDefaultIndexerInit(type, csharpName, typeDesc.UseReflection, useReflection);
			}
			else if (typeDesc.IsEnumerable)
			{
				if (typeDesc.IsGenericInterface)
				{
					this.WriteMethodInfo(csharpName, typeVariable, "System.Collections.Generic.IEnumerable*", true, new string[0]);
				}
				else if (!typeDesc.IsPrivateImplementation)
				{
					this.WriteMethodInfo(csharpName, typeVariable, "GetEnumerator", true, new string[0]);
				}
			}
			this.WriteMethodInfo(csharpName, typeVariable, "Add", false, new string[]
			{
				this.GetStringForTypeof(csharpName2, useReflection)
			});
		}

		// Token: 0x06001E14 RID: 7700 RVA: 0x000A11C0 File Offset: 0x0009F3C0
		private string WriteAssemblyInfo(Type type)
		{
			string fullName = type.Assembly.FullName;
			string text = (string)this.reflectionVariables[fullName];
			if (text == null)
			{
				int num = fullName.IndexOf(',');
				string fullName2 = (num > -1) ? fullName.Substring(0, num) : fullName;
				text = this.GenerateVariableName("assembly", fullName2);
				this.writer.Write(string.Concat(new string[]
				{
					"static ",
					typeof(Assembly).FullName,
					" ",
					text,
					" = ResolveDynamicAssembly("
				}));
				this.WriteQuotedCSharpString(DynamicAssemblies.GetName(type.Assembly));
				this.writer.WriteLine(");");
				this.reflectionVariables.Add(fullName, text);
			}
			return text;
		}

		// Token: 0x06001E15 RID: 7701 RVA: 0x000A128C File Offset: 0x0009F48C
		private string WriteMemberInfo(Type type, string escapedName, string typeVariable, string memberName)
		{
			MemberInfo[] member = type.GetMember(memberName);
			for (int i = 0; i < member.Length; i++)
			{
				MemberTypes memberType = member[i].MemberType;
				if (memberType == MemberTypes.Property)
				{
					string text = this.GenerateVariableName("prop", memberName);
					this.writer.Write(string.Concat(new string[]
					{
						"static XSPropInfo ",
						text,
						" = new XSPropInfo(",
						typeVariable,
						", "
					}));
					this.WriteQuotedCSharpString(memberName);
					this.writer.WriteLine(");");
					this.reflectionVariables.Add(memberName + ":" + escapedName, text);
					return text;
				}
				if (memberType == MemberTypes.Field)
				{
					string text2 = this.GenerateVariableName("field", memberName);
					this.writer.Write(string.Concat(new string[]
					{
						"static XSFieldInfo ",
						text2,
						" = new XSFieldInfo(",
						typeVariable,
						", "
					}));
					this.WriteQuotedCSharpString(memberName);
					this.writer.WriteLine(");");
					this.reflectionVariables.Add(memberName + ":" + escapedName, text2);
					return text2;
				}
			}
			throw new InvalidOperationException(Res.GetString("XmlSerializerUnsupportedType", new object[]
			{
				member[0].ToString()
			}));
		}

		// Token: 0x06001E16 RID: 7702 RVA: 0x000A13DC File Offset: 0x0009F5DC
		private string WriteMethodInfo(string escapedName, string typeVariable, string memberName, bool isNonPublic, params string[] paramTypes)
		{
			string text = this.GenerateVariableName("method", memberName);
			this.writer.Write(string.Concat(new string[]
			{
				"static ",
				typeof(MethodInfo).FullName,
				" ",
				text,
				" = ",
				typeVariable,
				".GetMethod("
			}));
			this.WriteQuotedCSharpString(memberName);
			this.writer.Write(", ");
			string fullName = typeof(BindingFlags).FullName;
			this.writer.Write(fullName);
			this.writer.Write(".Public | ");
			this.writer.Write(fullName);
			this.writer.Write(".Instance | ");
			this.writer.Write(fullName);
			this.writer.Write(".Static");
			if (isNonPublic)
			{
				this.writer.Write(" | ");
				this.writer.Write(fullName);
				this.writer.Write(".NonPublic");
			}
			this.writer.Write(", null, ");
			this.writer.Write("new " + typeof(Type).FullName + "[] { ");
			for (int i = 0; i < paramTypes.Length; i++)
			{
				this.writer.Write(paramTypes[i]);
				if (i < paramTypes.Length - 1)
				{
					this.writer.Write(", ");
				}
			}
			this.writer.WriteLine("}, null);");
			this.reflectionVariables.Add(memberName + ":" + escapedName, text);
			return text;
		}

		// Token: 0x06001E17 RID: 7703 RVA: 0x000A158C File Offset: 0x0009F78C
		private string WriteDefaultIndexerInit(Type type, string escapedName, bool collectionUseReflection, bool elementUseReflection)
		{
			string text = this.GenerateVariableName("item", escapedName);
			PropertyInfo defaultIndexer = TypeScope.GetDefaultIndexer(type, null);
			this.writer.Write("static XSArrayInfo ");
			this.writer.Write(text);
			this.writer.Write("= new XSArrayInfo(");
			this.writer.Write(this.GetStringForTypeof(CodeIdentifier.GetCSharpName(type), collectionUseReflection));
			this.writer.Write(".GetProperty(");
			this.WriteQuotedCSharpString(defaultIndexer.Name);
			this.writer.Write(",");
			this.writer.Write(this.GetStringForTypeof(CodeIdentifier.GetCSharpName(defaultIndexer.PropertyType), elementUseReflection));
			this.writer.Write(",new ");
			this.writer.Write(typeof(Type[]).FullName);
			this.writer.WriteLine("{typeof(int)}));");
			this.reflectionVariables.Add("0:" + escapedName, text);
			return text;
		}

		// Token: 0x06001E18 RID: 7704 RVA: 0x000A168E File Offset: 0x0009F88E
		private string GenerateVariableName(string prefix, string fullName)
		{
			this.nextReflectionVariableNumber++;
			return prefix + this.nextReflectionVariableNumber.ToString() + "_" + CodeIdentifier.MakeValidInternal(fullName.Replace('.', '_'));
		}

		// Token: 0x06001E19 RID: 7705 RVA: 0x000A16C4 File Offset: 0x0009F8C4
		internal string GetReflectionVariable(string typeFullName, string memberName)
		{
			string key;
			if (memberName == null)
			{
				key = typeFullName;
			}
			else
			{
				key = memberName + ":" + typeFullName;
			}
			return (string)this.reflectionVariables[key];
		}

		// Token: 0x06001E1A RID: 7706 RVA: 0x000A16F8 File Offset: 0x0009F8F8
		internal string GetStringForMethodInvoke(string obj, string escapedTypeName, string methodName, bool useReflection, params string[] args)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (useReflection)
			{
				stringBuilder.Append(this.GetReflectionVariable(escapedTypeName, methodName));
				stringBuilder.Append(".Invoke(");
				stringBuilder.Append(obj);
				stringBuilder.Append(", new object[] {");
			}
			else
			{
				stringBuilder.Append(obj);
				stringBuilder.Append(".@");
				stringBuilder.Append(methodName);
				stringBuilder.Append("(");
			}
			for (int i = 0; i < args.Length; i++)
			{
				if (i != 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(args[i]);
			}
			if (useReflection)
			{
				stringBuilder.Append("})");
			}
			else
			{
				stringBuilder.Append(")");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001E1B RID: 7707 RVA: 0x000A17B8 File Offset: 0x0009F9B8
		internal string GetStringForEnumCompare(EnumMapping mapping, string memberName, bool useReflection)
		{
			if (!useReflection)
			{
				CodeIdentifier.CheckValidIdentifier(memberName);
				return mapping.TypeDesc.CSharpName + ".@" + memberName;
			}
			string stringForEnumMember = this.GetStringForEnumMember(mapping.TypeDesc.CSharpName, memberName, useReflection);
			return this.GetStringForEnumLongValue(stringForEnumMember, useReflection);
		}

		// Token: 0x06001E1C RID: 7708 RVA: 0x000A1804 File Offset: 0x0009FA04
		internal string GetStringForEnumLongValue(string variable, bool useReflection)
		{
			if (useReflection)
			{
				return typeof(Convert).FullName + ".ToInt64(" + variable + ")";
			}
			return string.Concat(new string[]
			{
				"((",
				typeof(long).FullName,
				")",
				variable,
				")"
			});
		}

		// Token: 0x06001E1D RID: 7709 RVA: 0x000A186D File Offset: 0x0009FA6D
		internal string GetStringForTypeof(string typeFullName, bool useReflection)
		{
			if (useReflection)
			{
				return this.GetReflectionVariable(typeFullName, null);
			}
			return "typeof(" + typeFullName + ")";
		}

		// Token: 0x06001E1E RID: 7710 RVA: 0x000A188C File Offset: 0x0009FA8C
		internal string GetStringForMember(string obj, string memberName, TypeDesc typeDesc)
		{
			if (!typeDesc.UseReflection)
			{
				return obj + ".@" + memberName;
			}
			while (typeDesc != null)
			{
				string csharpName = typeDesc.CSharpName;
				string reflectionVariable = this.GetReflectionVariable(csharpName, memberName);
				if (reflectionVariable != null)
				{
					return reflectionVariable + "[" + obj + "]";
				}
				typeDesc = typeDesc.BaseTypeDesc;
				if (typeDesc != null && !typeDesc.UseReflection)
				{
					return string.Concat(new string[]
					{
						"((",
						typeDesc.CSharpName,
						")",
						obj,
						").@",
						memberName
					});
				}
			}
			return "[" + obj + "]";
		}

		// Token: 0x06001E1F RID: 7711 RVA: 0x000A1934 File Offset: 0x0009FB34
		internal string GetStringForEnumMember(string typeFullName, string memberName, bool useReflection)
		{
			if (!useReflection)
			{
				return typeFullName + ".@" + memberName;
			}
			string reflectionVariable = this.GetReflectionVariable(typeFullName, memberName);
			return reflectionVariable + "[null]";
		}

		// Token: 0x06001E20 RID: 7712 RVA: 0x000A1968 File Offset: 0x0009FB68
		internal string GetStringForArrayMember(string arrayName, string subscript, TypeDesc arrayTypeDesc)
		{
			if (!arrayTypeDesc.UseReflection)
			{
				return arrayName + "[" + subscript + "]";
			}
			string typeFullName = arrayTypeDesc.IsCollection ? arrayTypeDesc.CSharpName : typeof(Array).FullName;
			string reflectionVariable = this.GetReflectionVariable(typeFullName, "0");
			return string.Concat(new string[]
			{
				reflectionVariable,
				"[",
				arrayName,
				", ",
				subscript,
				"]"
			});
		}

		// Token: 0x06001E21 RID: 7713 RVA: 0x000A19EC File Offset: 0x0009FBEC
		internal string GetStringForMethod(string obj, string typeFullName, string memberName, bool useReflection)
		{
			if (!useReflection)
			{
				return obj + "." + memberName + "(";
			}
			string reflectionVariable = this.GetReflectionVariable(typeFullName, memberName);
			return reflectionVariable + ".Invoke(" + obj + ", new object[]{";
		}

		// Token: 0x06001E22 RID: 7714 RVA: 0x000A1A29 File Offset: 0x0009FC29
		internal string GetStringForCreateInstance(string escapedTypeName, bool useReflection, bool ctorInaccessible, bool cast)
		{
			return this.GetStringForCreateInstance(escapedTypeName, useReflection, ctorInaccessible, cast, string.Empty);
		}

		// Token: 0x06001E23 RID: 7715 RVA: 0x000A1A3C File Offset: 0x0009FC3C
		internal string GetStringForCreateInstance(string escapedTypeName, bool useReflection, bool ctorInaccessible, bool cast, string arg)
		{
			if (!useReflection && !ctorInaccessible)
			{
				return string.Concat(new string[]
				{
					"new ",
					escapedTypeName,
					"(",
					arg,
					")"
				});
			}
			return this.GetStringForCreateInstance(this.GetStringForTypeof(escapedTypeName, useReflection), (cast && !useReflection) ? escapedTypeName : null, ctorInaccessible, arg);
		}

		// Token: 0x06001E24 RID: 7716 RVA: 0x000A1A98 File Offset: 0x0009FC98
		internal string GetStringForCreateInstance(string type, string cast, bool nonPublic, string arg)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (cast != null && cast.Length > 0)
			{
				stringBuilder.Append("(");
				stringBuilder.Append(cast);
				stringBuilder.Append(")");
			}
			stringBuilder.Append(typeof(Activator).FullName);
			stringBuilder.Append(".CreateInstance(");
			stringBuilder.Append(type);
			stringBuilder.Append(", ");
			string fullName = typeof(BindingFlags).FullName;
			stringBuilder.Append(fullName);
			stringBuilder.Append(".Instance | ");
			stringBuilder.Append(fullName);
			stringBuilder.Append(".Public | ");
			stringBuilder.Append(fullName);
			stringBuilder.Append(".CreateInstance");
			if (nonPublic)
			{
				stringBuilder.Append(" | ");
				stringBuilder.Append(fullName);
				stringBuilder.Append(".NonPublic");
			}
			if (arg == null || arg.Length == 0)
			{
				stringBuilder.Append(", null, new object[0], null)");
			}
			else
			{
				stringBuilder.Append(", null, new object[] { ");
				stringBuilder.Append(arg);
				stringBuilder.Append(" }, null)");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001E25 RID: 7717 RVA: 0x000A1BC0 File Offset: 0x0009FDC0
		internal void WriteLocalDecl(string typeFullName, string variableName, string initValue, bool useReflection)
		{
			if (useReflection)
			{
				typeFullName = "object";
			}
			this.writer.Write(typeFullName);
			this.writer.Write(" ");
			this.writer.Write(variableName);
			if (initValue != null)
			{
				this.writer.Write(" = ");
				if (!useReflection && initValue != "null")
				{
					this.writer.Write("(" + typeFullName + ")");
				}
				this.writer.Write(initValue);
			}
			this.writer.WriteLine(";");
		}

		// Token: 0x06001E26 RID: 7718 RVA: 0x000A1C5C File Offset: 0x0009FE5C
		internal void WriteCreateInstance(string escapedName, string source, bool useReflection, bool ctorInaccessible)
		{
			this.writer.Write(useReflection ? "object" : escapedName);
			this.writer.Write(" ");
			this.writer.Write(source);
			this.writer.Write(" = ");
			this.writer.Write(this.GetStringForCreateInstance(escapedName, useReflection, ctorInaccessible, !useReflection && ctorInaccessible));
			this.writer.WriteLine(";");
		}

		// Token: 0x06001E27 RID: 7719 RVA: 0x000A1CD8 File Offset: 0x0009FED8
		internal void WriteInstanceOf(string source, string escapedTypeName, bool useReflection)
		{
			if (!useReflection)
			{
				this.writer.Write(source);
				this.writer.Write(" is ");
				this.writer.Write(escapedTypeName);
				return;
			}
			this.writer.Write(this.GetReflectionVariable(escapedTypeName, null));
			this.writer.Write(".IsAssignableFrom(");
			this.writer.Write(source);
			this.writer.Write(".GetType())");
		}

		// Token: 0x06001E28 RID: 7720 RVA: 0x000A1D50 File Offset: 0x0009FF50
		internal void WriteArrayLocalDecl(string typeName, string variableName, string initValue, TypeDesc arrayTypeDesc)
		{
			if (arrayTypeDesc.UseReflection)
			{
				if (arrayTypeDesc.IsEnumerable)
				{
					typeName = typeof(IEnumerable).FullName;
				}
				else if (arrayTypeDesc.IsCollection)
				{
					typeName = typeof(ICollection).FullName;
				}
				else
				{
					typeName = typeof(Array).FullName;
				}
			}
			this.writer.Write(typeName);
			this.writer.Write(" ");
			this.writer.Write(variableName);
			if (initValue != null)
			{
				this.writer.Write(" = ");
				if (initValue != "null")
				{
					this.writer.Write("(" + typeName + ")");
				}
				this.writer.Write(initValue);
			}
			this.writer.WriteLine(";");
		}

		// Token: 0x06001E29 RID: 7721 RVA: 0x000A1E30 File Offset: 0x000A0030
		internal void WriteEnumCase(string fullTypeName, ConstantMapping c, bool useReflection)
		{
			this.writer.Write("case ");
			if (useReflection)
			{
				this.writer.Write(c.Value.ToString(CultureInfo.InvariantCulture));
			}
			else
			{
				this.writer.Write(fullTypeName);
				this.writer.Write(".@");
				CodeIdentifier.CheckValidIdentifier(c.Name);
				this.writer.Write(c.Name);
			}
			this.writer.Write(": ");
		}

		// Token: 0x06001E2A RID: 7722 RVA: 0x000A1EB8 File Offset: 0x000A00B8
		internal void WriteTypeCompare(string variable, string escapedTypeName, bool useReflection)
		{
			this.writer.Write(variable);
			this.writer.Write(" == ");
			this.writer.Write(this.GetStringForTypeof(escapedTypeName, useReflection));
		}

		// Token: 0x06001E2B RID: 7723 RVA: 0x000A1EEC File Offset: 0x000A00EC
		internal void WriteArrayTypeCompare(string variable, string escapedTypeName, string elementTypeName, bool useReflection)
		{
			if (!useReflection)
			{
				this.writer.Write(variable);
				this.writer.Write(" == typeof(");
				this.writer.Write(escapedTypeName);
				this.writer.Write(")");
				return;
			}
			this.writer.Write(variable);
			this.writer.Write(".IsArray ");
			this.writer.Write(" && ");
			this.WriteTypeCompare(variable + ".GetElementType()", elementTypeName, useReflection);
		}

		// Token: 0x06001E2C RID: 7724 RVA: 0x000A1F78 File Offset: 0x000A0178
		internal static void WriteQuotedCSharpString(IndentedWriter writer, string value)
		{
			if (value == null)
			{
				writer.Write("null");
				return;
			}
			writer.Write("@\"");
			foreach (char c in value)
			{
				if (c < ' ')
				{
					if (c == '\r')
					{
						writer.Write("\\r");
					}
					else if (c == '\n')
					{
						writer.Write("\\n");
					}
					else if (c == '\t')
					{
						writer.Write("\\t");
					}
					else
					{
						byte b = (byte)c;
						writer.Write("\\x");
						writer.Write("0123456789ABCDEF"[b >> 4]);
						writer.Write("0123456789ABCDEF"[(int)(b & 15)]);
					}
				}
				else if (c == '"')
				{
					writer.Write("\"\"");
				}
				else
				{
					writer.Write(c);
				}
			}
			writer.Write("\"");
		}

		// Token: 0x06001E2D RID: 7725 RVA: 0x000A2056 File Offset: 0x000A0256
		internal void WriteQuotedCSharpString(string value)
		{
			ReflectionAwareCodeGen.WriteQuotedCSharpString(this.writer, value);
		}

		// Token: 0x04000CD1 RID: 3281
		private const string hexDigits = "0123456789ABCDEF";

		// Token: 0x04000CD2 RID: 3282
		private const string arrayMemberKey = "0";

		// Token: 0x04000CD3 RID: 3283
		private Hashtable reflectionVariables;

		// Token: 0x04000CD4 RID: 3284
		private int nextReflectionVariableNumber;

		// Token: 0x04000CD5 RID: 3285
		private IndentedWriter writer;

		// Token: 0x04000CD6 RID: 3286
		private static string helperClassesForUseReflection = "\r\n    sealed class XSFieldInfo {{\r\n       {3} fieldInfo;\r\n        public XSFieldInfo({2} t, {1} memberName){{\r\n            fieldInfo = t.GetField(memberName);\r\n        }}\r\n        public {0} this[{0} o] {{\r\n            get {{\r\n                return fieldInfo.GetValue(o);\r\n            }}\r\n            set {{\r\n                fieldInfo.SetValue(o, value);\r\n            }}\r\n        }}\r\n\r\n    }}\r\n    sealed class XSPropInfo {{\r\n        {4} propInfo;\r\n        public XSPropInfo({2} t, {1} memberName){{\r\n            propInfo = t.GetProperty(memberName);\r\n        }}\r\n        public {0} this[{0} o] {{\r\n            get {{\r\n                return propInfo.GetValue(o, null);\r\n            }}\r\n            set {{\r\n                propInfo.SetValue(o, value, null);\r\n            }}\r\n        }}\r\n    }}\r\n    sealed class XSArrayInfo {{\r\n        {4} propInfo;\r\n        public XSArrayInfo({4} propInfo){{\r\n            this.propInfo = propInfo;\r\n        }}\r\n        public {0} this[{0} a, int i] {{\r\n            get {{\r\n                return propInfo.GetValue(a, new {0}[]{{i}});\r\n            }}\r\n            set {{\r\n                propInfo.SetValue(a, value, new {0}[]{{i}});\r\n            }}\r\n        }}\r\n    }}\r\n";
	}
}
