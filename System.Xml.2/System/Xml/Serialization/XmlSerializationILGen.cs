using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Text.RegularExpressions;

namespace System.Xml.Serialization
{
	// Token: 0x020001A9 RID: 425
	internal class XmlSerializationILGen
	{
		// Token: 0x06001C44 RID: 7236 RVA: 0x00084C10 File Offset: 0x00082E10
		internal XmlSerializationILGen(TypeScope[] scopes, string access, string className)
		{
			this.scopes = scopes;
			if (scopes.Length != 0)
			{
				this.stringTypeDesc = scopes[0].GetTypeDesc(typeof(string));
				this.qnameTypeDesc = scopes[0].GetTypeDesc(typeof(XmlQualifiedName));
			}
			this.raCodeGen = new ReflectionAwareILGen();
			this.className = className;
			this.typeAttributes = TypeAttributes.Public;
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06001C45 RID: 7237 RVA: 0x00084CAE File Offset: 0x00082EAE
		// (set) Token: 0x06001C46 RID: 7238 RVA: 0x00084CB6 File Offset: 0x00082EB6
		internal int NextMethodNumber
		{
			get
			{
				return this.nextMethodNumber;
			}
			set
			{
				this.nextMethodNumber = value;
			}
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06001C47 RID: 7239 RVA: 0x00084CBF File Offset: 0x00082EBF
		internal ReflectionAwareILGen RaCodeGen
		{
			get
			{
				return this.raCodeGen;
			}
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06001C48 RID: 7240 RVA: 0x00084CC7 File Offset: 0x00082EC7
		internal TypeDesc StringTypeDesc
		{
			get
			{
				return this.stringTypeDesc;
			}
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06001C49 RID: 7241 RVA: 0x00084CCF File Offset: 0x00082ECF
		internal TypeDesc QnameTypeDesc
		{
			get
			{
				return this.qnameTypeDesc;
			}
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06001C4A RID: 7242 RVA: 0x00084CD7 File Offset: 0x00082ED7
		internal string ClassName
		{
			get
			{
				return this.className;
			}
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06001C4B RID: 7243 RVA: 0x00084CDF File Offset: 0x00082EDF
		internal TypeScope[] Scopes
		{
			get
			{
				return this.scopes;
			}
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06001C4C RID: 7244 RVA: 0x00084CE7 File Offset: 0x00082EE7
		internal Hashtable MethodNames
		{
			get
			{
				return this.methodNames;
			}
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06001C4D RID: 7245 RVA: 0x00084CEF File Offset: 0x00082EEF
		internal Hashtable GeneratedMethods
		{
			get
			{
				return this.generatedMethods;
			}
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06001C4E RID: 7246 RVA: 0x00084CF7 File Offset: 0x00082EF7
		// (set) Token: 0x06001C4F RID: 7247 RVA: 0x00084CFF File Offset: 0x00082EFF
		internal ModuleBuilder ModuleBuilder
		{
			get
			{
				return this.moduleBuilder;
			}
			set
			{
				this.moduleBuilder = value;
			}
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06001C50 RID: 7248 RVA: 0x00084D08 File Offset: 0x00082F08
		internal TypeAttributes TypeAttributes
		{
			get
			{
				return this.typeAttributes;
			}
		}

		// Token: 0x06001C51 RID: 7249 RVA: 0x00084D10 File Offset: 0x00082F10
		internal static Regex NewRegex(string pattern)
		{
			Dictionary<string, Regex> obj = XmlSerializationILGen.regexs;
			Regex regex;
			lock (obj)
			{
				if (!XmlSerializationILGen.regexs.TryGetValue(pattern, out regex))
				{
					regex = new Regex(pattern);
					XmlSerializationILGen.regexs.Add(pattern, regex);
				}
			}
			return regex;
		}

		// Token: 0x06001C52 RID: 7250 RVA: 0x00084D6C File Offset: 0x00082F6C
		internal MethodBuilder EnsureMethodBuilder(TypeBuilder typeBuilder, string methodName, MethodAttributes attributes, Type returnType, Type[] parameterTypes)
		{
			MethodBuilderInfo methodBuilderInfo;
			if (!this.methodBuilders.TryGetValue(methodName, out methodBuilderInfo))
			{
				MethodBuilder methodBuilder = typeBuilder.DefineMethod(methodName, attributes, returnType, parameterTypes);
				methodBuilderInfo = new MethodBuilderInfo(methodBuilder, parameterTypes);
				this.methodBuilders.Add(methodName, methodBuilderInfo);
			}
			return methodBuilderInfo.MethodBuilder;
		}

		// Token: 0x06001C53 RID: 7251 RVA: 0x00084DB2 File Offset: 0x00082FB2
		internal MethodBuilderInfo GetMethodBuilder(string methodName)
		{
			return this.methodBuilders[methodName];
		}

		// Token: 0x06001C54 RID: 7252 RVA: 0x00084DC0 File Offset: 0x00082FC0
		internal virtual void GenerateMethod(TypeMapping mapping)
		{
		}

		// Token: 0x06001C55 RID: 7253 RVA: 0x00084DC4 File Offset: 0x00082FC4
		internal void GenerateReferencedMethods()
		{
			while (this.references > 0)
			{
				TypeMapping[] array = this.referencedMethods;
				int num = this.references - 1;
				this.references = num;
				TypeMapping mapping = array[num];
				this.GenerateMethod(mapping);
			}
		}

		// Token: 0x06001C56 RID: 7254 RVA: 0x00084DFC File Offset: 0x00082FFC
		internal string ReferenceMapping(TypeMapping mapping)
		{
			if (this.generatedMethods[mapping] == null)
			{
				this.referencedMethods = this.EnsureArrayIndex(this.referencedMethods, this.references);
				TypeMapping[] array = this.referencedMethods;
				int num = this.references;
				this.references = num + 1;
				array[num] = mapping;
			}
			return (string)this.methodNames[mapping];
		}

		// Token: 0x06001C57 RID: 7255 RVA: 0x00084E5C File Offset: 0x0008305C
		private TypeMapping[] EnsureArrayIndex(TypeMapping[] a, int index)
		{
			if (a == null)
			{
				return new TypeMapping[32];
			}
			if (index < a.Length)
			{
				return a;
			}
			TypeMapping[] array = new TypeMapping[a.Length + 32];
			Array.Copy(a, array, index);
			return array;
		}

		// Token: 0x06001C58 RID: 7256 RVA: 0x00084E94 File Offset: 0x00083094
		internal FieldBuilder GenerateHashtableGetBegin(string privateName, string publicName, TypeBuilder serializerContractTypeBuilder)
		{
			FieldBuilder fieldBuilder = serializerContractTypeBuilder.DefineField(privateName, typeof(Hashtable), FieldAttributes.Private);
			this.ilg = new CodeGenerator(serializerContractTypeBuilder);
			PropertyBuilder propertyBuilder = serializerContractTypeBuilder.DefineProperty(publicName, PropertyAttributes.None, CallingConventions.HasThis, typeof(Hashtable), null, null, null, null, null);
			this.ilg.BeginMethod(typeof(Hashtable), "get_" + publicName, CodeGenerator.EmptyTypeArray, CodeGenerator.EmptyStringArray, CodeGenerator.PublicOverrideMethodAttributes | MethodAttributes.SpecialName);
			propertyBuilder.SetGetMethod(this.ilg.MethodBuilder);
			this.ilg.Ldarg(0);
			this.ilg.LoadMember(fieldBuilder);
			this.ilg.Load(null);
			this.ilg.If(Cmp.EqualTo);
			ConstructorInfo constructor = typeof(Hashtable).GetConstructor(CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			LocalBuilder local = this.ilg.DeclareLocal(typeof(Hashtable), "_tmp");
			this.ilg.New(constructor);
			this.ilg.Stloc(local);
			return fieldBuilder;
		}

		// Token: 0x06001C59 RID: 7257 RVA: 0x00084FA4 File Offset: 0x000831A4
		internal void GenerateHashtableGetEnd(FieldBuilder fieldBuilder)
		{
			this.ilg.Ldarg(0);
			this.ilg.LoadMember(fieldBuilder);
			this.ilg.Load(null);
			this.ilg.If(Cmp.EqualTo);
			this.ilg.Ldarg(0);
			this.ilg.Ldloc(typeof(Hashtable), "_tmp");
			this.ilg.StoreMember(fieldBuilder);
			this.ilg.EndIf();
			this.ilg.EndIf();
			this.ilg.Ldarg(0);
			this.ilg.LoadMember(fieldBuilder);
			this.ilg.GotoMethodEnd();
			this.ilg.EndMethod();
		}

		// Token: 0x06001C5A RID: 7258 RVA: 0x0008505C File Offset: 0x0008325C
		internal FieldBuilder GeneratePublicMethods(string privateName, string publicName, string[] methods, XmlMapping[] xmlMappings, TypeBuilder serializerContractTypeBuilder)
		{
			FieldBuilder fieldBuilder = this.GenerateHashtableGetBegin(privateName, publicName, serializerContractTypeBuilder);
			if (methods != null && methods.Length != 0 && xmlMappings != null && xmlMappings.Length == methods.Length)
			{
				MethodInfo method = typeof(Hashtable).GetMethod("set_Item", CodeGenerator.InstanceBindingFlags, null, new Type[]
				{
					typeof(object),
					typeof(object)
				}, null);
				for (int i = 0; i < methods.Length; i++)
				{
					if (methods[i] != null)
					{
						this.ilg.Ldloc(typeof(Hashtable), "_tmp");
						this.ilg.Ldstr(xmlMappings[i].Key);
						this.ilg.Ldstr(methods[i]);
						this.ilg.Call(method);
					}
				}
			}
			this.GenerateHashtableGetEnd(fieldBuilder);
			return fieldBuilder;
		}

		// Token: 0x06001C5B RID: 7259 RVA: 0x00085134 File Offset: 0x00083334
		internal void GenerateSupportedTypes(Type[] types, TypeBuilder serializerContractTypeBuilder)
		{
			this.ilg = new CodeGenerator(serializerContractTypeBuilder);
			this.ilg.BeginMethod(typeof(bool), "CanSerialize", new Type[]
			{
				typeof(Type)
			}, new string[]
			{
				"type"
			}, CodeGenerator.PublicOverrideMethodAttributes);
			Hashtable hashtable = new Hashtable();
			foreach (Type type in types)
			{
				if (!(type == null) && (type.IsPublic || type.IsNestedPublic) && hashtable[type] == null && !type.IsGenericType && !type.ContainsGenericParameters)
				{
					hashtable[type] = type;
					this.ilg.Ldarg("type");
					this.ilg.Ldc(type);
					this.ilg.If(Cmp.EqualTo);
					this.ilg.Ldc(true);
					this.ilg.GotoMethodEnd();
					this.ilg.EndIf();
				}
			}
			this.ilg.Ldc(false);
			this.ilg.GotoMethodEnd();
			this.ilg.EndMethod();
		}

		// Token: 0x06001C5C RID: 7260 RVA: 0x00085254 File Offset: 0x00083454
		internal string GenerateBaseSerializer(string baseSerializer, string readerClass, string writerClass, CodeIdentifiers classes)
		{
			baseSerializer = CodeIdentifier.MakeValid(baseSerializer);
			baseSerializer = classes.AddUnique(baseSerializer, baseSerializer);
			TypeBuilder typeBuilder = CodeGenerator.CreateTypeBuilder(this.moduleBuilder, CodeIdentifier.GetCSharpName(baseSerializer), TypeAttributes.Public | TypeAttributes.Abstract | TypeAttributes.BeforeFieldInit, typeof(XmlSerializer), CodeGenerator.EmptyTypeArray);
			ConstructorInfo constructor = this.CreatedTypes[readerClass].GetConstructor(CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			this.ilg = new CodeGenerator(typeBuilder);
			this.ilg.BeginMethod(typeof(XmlSerializationReader), "CreateReader", CodeGenerator.EmptyTypeArray, CodeGenerator.EmptyStringArray, CodeGenerator.ProtectedOverrideMethodAttributes);
			this.ilg.New(constructor);
			this.ilg.EndMethod();
			ConstructorInfo constructor2 = this.CreatedTypes[writerClass].GetConstructor(CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			this.ilg.BeginMethod(typeof(XmlSerializationWriter), "CreateWriter", CodeGenerator.EmptyTypeArray, CodeGenerator.EmptyStringArray, CodeGenerator.ProtectedOverrideMethodAttributes);
			this.ilg.New(constructor2);
			this.ilg.EndMethod();
			typeBuilder.DefineDefaultConstructor(MethodAttributes.Family | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);
			Type type = typeBuilder.CreateType();
			this.CreatedTypes.Add(type.Name, type);
			return baseSerializer;
		}

		// Token: 0x06001C5D RID: 7261 RVA: 0x0008538C File Offset: 0x0008358C
		internal string GenerateTypedSerializer(string readMethod, string writeMethod, XmlMapping mapping, CodeIdentifiers classes, string baseSerializer, string readerClass, string writerClass)
		{
			string text = CodeIdentifier.MakeValid(Accessor.UnescapeName(mapping.Accessor.Mapping.TypeDesc.Name));
			text = classes.AddUnique(text + "Serializer", mapping);
			TypeBuilder typeBuilder = CodeGenerator.CreateTypeBuilder(this.moduleBuilder, CodeIdentifier.GetCSharpName(text), TypeAttributes.Public | TypeAttributes.Sealed | TypeAttributes.BeforeFieldInit, this.CreatedTypes[baseSerializer], CodeGenerator.EmptyTypeArray);
			this.ilg = new CodeGenerator(typeBuilder);
			this.ilg.BeginMethod(typeof(bool), "CanDeserialize", new Type[]
			{
				typeof(XmlReader)
			}, new string[]
			{
				"xmlReader"
			}, CodeGenerator.PublicOverrideMethodAttributes);
			if (mapping.Accessor.Any)
			{
				this.ilg.Ldc(true);
				this.ilg.Stloc(this.ilg.ReturnLocal);
				this.ilg.Br(this.ilg.ReturnLabel);
			}
			else
			{
				MethodInfo method = typeof(XmlReader).GetMethod("IsStartElement", CodeGenerator.InstanceBindingFlags, null, new Type[]
				{
					typeof(string),
					typeof(string)
				}, null);
				this.ilg.Ldarg(this.ilg.GetArg("xmlReader"));
				this.ilg.Ldstr(mapping.Accessor.Name);
				this.ilg.Ldstr(mapping.Accessor.Namespace);
				this.ilg.Call(method);
				this.ilg.Stloc(this.ilg.ReturnLocal);
				this.ilg.Br(this.ilg.ReturnLabel);
			}
			this.ilg.MarkLabel(this.ilg.ReturnLabel);
			this.ilg.Ldloc(this.ilg.ReturnLocal);
			this.ilg.EndMethod();
			if (writeMethod != null)
			{
				this.ilg = new CodeGenerator(typeBuilder);
				this.ilg.BeginMethod(typeof(void), "Serialize", new Type[]
				{
					typeof(object),
					typeof(XmlSerializationWriter)
				}, new string[]
				{
					"objectToSerialize",
					"writer"
				}, CodeGenerator.ProtectedOverrideMethodAttributes);
				MethodInfo method2 = this.CreatedTypes[writerClass].GetMethod(writeMethod, CodeGenerator.InstanceBindingFlags, null, new Type[]
				{
					(mapping is XmlMembersMapping) ? typeof(object[]) : typeof(object)
				}, null);
				this.ilg.Ldarg("writer");
				this.ilg.Castclass(this.CreatedTypes[writerClass]);
				this.ilg.Ldarg("objectToSerialize");
				if (mapping is XmlMembersMapping)
				{
					this.ilg.ConvertValue(typeof(object), typeof(object[]));
				}
				this.ilg.Call(method2);
				this.ilg.EndMethod();
			}
			if (readMethod != null)
			{
				this.ilg = new CodeGenerator(typeBuilder);
				this.ilg.BeginMethod(typeof(object), "Deserialize", new Type[]
				{
					typeof(XmlSerializationReader)
				}, new string[]
				{
					"reader"
				}, CodeGenerator.ProtectedOverrideMethodAttributes);
				MethodInfo method3 = this.CreatedTypes[readerClass].GetMethod(readMethod, CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
				this.ilg.Ldarg("reader");
				this.ilg.Castclass(this.CreatedTypes[readerClass]);
				this.ilg.Call(method3);
				this.ilg.EndMethod();
			}
			typeBuilder.DefineDefaultConstructor(CodeGenerator.PublicMethodAttributes);
			Type type = typeBuilder.CreateType();
			this.CreatedTypes.Add(type.Name, type);
			return type.Name;
		}

		// Token: 0x06001C5E RID: 7262 RVA: 0x00085780 File Offset: 0x00083980
		private FieldBuilder GenerateTypedSerializers(Hashtable serializers, TypeBuilder serializerContractTypeBuilder)
		{
			string privateName = "typedSerializers";
			FieldBuilder fieldBuilder = this.GenerateHashtableGetBegin(privateName, "TypedSerializers", serializerContractTypeBuilder);
			MethodInfo method = typeof(Hashtable).GetMethod("Add", CodeGenerator.InstanceBindingFlags, null, new Type[]
			{
				typeof(object),
				typeof(object)
			}, null);
			foreach (object obj in serializers.Keys)
			{
				string text = (string)obj;
				ConstructorInfo constructor = this.CreatedTypes[(string)serializers[text]].GetConstructor(CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
				this.ilg.Ldloc(typeof(Hashtable), "_tmp");
				this.ilg.Ldstr(text);
				this.ilg.New(constructor);
				this.ilg.Call(method);
			}
			this.GenerateHashtableGetEnd(fieldBuilder);
			return fieldBuilder;
		}

		// Token: 0x06001C5F RID: 7263 RVA: 0x0008589C File Offset: 0x00083A9C
		private void GenerateGetSerializer(Hashtable serializers, XmlMapping[] xmlMappings, TypeBuilder serializerContractTypeBuilder)
		{
			this.ilg = new CodeGenerator(serializerContractTypeBuilder);
			this.ilg.BeginMethod(typeof(XmlSerializer), "GetSerializer", new Type[]
			{
				typeof(Type)
			}, new string[]
			{
				"type"
			}, CodeGenerator.PublicOverrideMethodAttributes);
			for (int i = 0; i < xmlMappings.Length; i++)
			{
				if (xmlMappings[i] is XmlTypeMapping)
				{
					Type type = xmlMappings[i].Accessor.Mapping.TypeDesc.Type;
					if (!(type == null) && (type.IsPublic || type.IsNestedPublic) && !type.IsGenericType && !type.ContainsGenericParameters)
					{
						this.ilg.Ldarg("type");
						this.ilg.Ldc(type);
						this.ilg.If(Cmp.EqualTo);
						ConstructorInfo constructor = this.CreatedTypes[(string)serializers[xmlMappings[i].Key]].GetConstructor(CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
						this.ilg.New(constructor);
						this.ilg.Stloc(this.ilg.ReturnLocal);
						this.ilg.Br(this.ilg.ReturnLabel);
						this.ilg.EndIf();
					}
				}
			}
			this.ilg.Load(null);
			this.ilg.Stloc(this.ilg.ReturnLocal);
			this.ilg.Br(this.ilg.ReturnLabel);
			this.ilg.MarkLabel(this.ilg.ReturnLabel);
			this.ilg.Ldloc(this.ilg.ReturnLocal);
			this.ilg.EndMethod();
		}

		// Token: 0x06001C60 RID: 7264 RVA: 0x00085A70 File Offset: 0x00083C70
		internal void GenerateSerializerContract(string className, XmlMapping[] xmlMappings, Type[] types, string readerType, string[] readMethods, string writerType, string[] writerMethods, Hashtable serializers)
		{
			TypeBuilder typeBuilder = CodeGenerator.CreateTypeBuilder(this.moduleBuilder, "XmlSerializerContract", TypeAttributes.Public | TypeAttributes.BeforeFieldInit, typeof(XmlSerializerImplementation), CodeGenerator.EmptyTypeArray);
			this.ilg = new CodeGenerator(typeBuilder);
			PropertyBuilder propertyBuilder = typeBuilder.DefineProperty("Reader", PropertyAttributes.None, CallingConventions.HasThis, typeof(XmlSerializationReader), null, null, null, null, null);
			this.ilg.BeginMethod(typeof(XmlSerializationReader), "get_Reader", CodeGenerator.EmptyTypeArray, CodeGenerator.EmptyStringArray, CodeGenerator.PublicOverrideMethodAttributes | MethodAttributes.SpecialName);
			propertyBuilder.SetGetMethod(this.ilg.MethodBuilder);
			ConstructorInfo constructor = this.CreatedTypes[readerType].GetConstructor(CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			this.ilg.New(constructor);
			this.ilg.EndMethod();
			this.ilg = new CodeGenerator(typeBuilder);
			propertyBuilder = typeBuilder.DefineProperty("Writer", PropertyAttributes.None, CallingConventions.HasThis, typeof(XmlSerializationWriter), null, null, null, null, null);
			this.ilg.BeginMethod(typeof(XmlSerializationWriter), "get_Writer", CodeGenerator.EmptyTypeArray, CodeGenerator.EmptyStringArray, CodeGenerator.PublicOverrideMethodAttributes | MethodAttributes.SpecialName);
			propertyBuilder.SetGetMethod(this.ilg.MethodBuilder);
			constructor = this.CreatedTypes[writerType].GetConstructor(CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			this.ilg.New(constructor);
			this.ilg.EndMethod();
			FieldBuilder memberInfo = this.GeneratePublicMethods("readMethods", "ReadMethods", readMethods, xmlMappings, typeBuilder);
			FieldBuilder memberInfo2 = this.GeneratePublicMethods("writeMethods", "WriteMethods", writerMethods, xmlMappings, typeBuilder);
			FieldBuilder memberInfo3 = this.GenerateTypedSerializers(serializers, typeBuilder);
			this.GenerateSupportedTypes(types, typeBuilder);
			this.GenerateGetSerializer(serializers, xmlMappings, typeBuilder);
			ConstructorInfo constructor2 = typeof(XmlSerializerImplementation).GetConstructor(CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			this.ilg = new CodeGenerator(typeBuilder);
			this.ilg.BeginMethod(typeof(void), ".ctor", CodeGenerator.EmptyTypeArray, CodeGenerator.EmptyStringArray, CodeGenerator.PublicMethodAttributes | MethodAttributes.RTSpecialName | MethodAttributes.SpecialName);
			this.ilg.Ldarg(0);
			this.ilg.Load(null);
			this.ilg.StoreMember(memberInfo);
			this.ilg.Ldarg(0);
			this.ilg.Load(null);
			this.ilg.StoreMember(memberInfo2);
			this.ilg.Ldarg(0);
			this.ilg.Load(null);
			this.ilg.StoreMember(memberInfo3);
			this.ilg.Ldarg(0);
			this.ilg.Call(constructor2);
			this.ilg.EndMethod();
			Type type = typeBuilder.CreateType();
			this.CreatedTypes.Add(type.Name, type);
		}

		// Token: 0x06001C61 RID: 7265 RVA: 0x00085D39 File Offset: 0x00083F39
		internal static bool IsWildcard(SpecialMapping mapping)
		{
			if (mapping is SerializableMapping)
			{
				return ((SerializableMapping)mapping).IsAny;
			}
			return mapping.TypeDesc.CanBeElementValue;
		}

		// Token: 0x06001C62 RID: 7266 RVA: 0x00085D5A File Offset: 0x00083F5A
		internal void ILGenLoad(string source)
		{
			this.ILGenLoad(source, null);
		}

		// Token: 0x06001C63 RID: 7267 RVA: 0x00085D64 File Offset: 0x00083F64
		internal void ILGenLoad(string source, Type type)
		{
			if (source.StartsWith("o.@", StringComparison.Ordinal))
			{
				MemberInfo memberInfo = this.memberInfos[source.Substring(3)];
				this.ilg.LoadMember(this.ilg.GetVariable("o"), memberInfo);
				if (type != null)
				{
					Type source2 = (memberInfo.MemberType == MemberTypes.Field) ? ((FieldInfo)memberInfo).FieldType : ((PropertyInfo)memberInfo).PropertyType;
					this.ilg.ConvertValue(source2, type);
					return;
				}
			}
			else
			{
				SourceInfo sourceInfo = new SourceInfo(source, null, null, null, this.ilg);
				sourceInfo.Load(type);
			}
		}

		// Token: 0x04000C4C RID: 3148
		private int nextMethodNumber;

		// Token: 0x04000C4D RID: 3149
		private Hashtable methodNames = new Hashtable();

		// Token: 0x04000C4E RID: 3150
		private Dictionary<string, MethodBuilderInfo> methodBuilders = new Dictionary<string, MethodBuilderInfo>();

		// Token: 0x04000C4F RID: 3151
		internal Dictionary<string, Type> CreatedTypes = new Dictionary<string, Type>();

		// Token: 0x04000C50 RID: 3152
		internal Dictionary<string, MemberInfo> memberInfos = new Dictionary<string, MemberInfo>();

		// Token: 0x04000C51 RID: 3153
		private ReflectionAwareILGen raCodeGen;

		// Token: 0x04000C52 RID: 3154
		private TypeScope[] scopes;

		// Token: 0x04000C53 RID: 3155
		private TypeDesc stringTypeDesc;

		// Token: 0x04000C54 RID: 3156
		private TypeDesc qnameTypeDesc;

		// Token: 0x04000C55 RID: 3157
		private string className;

		// Token: 0x04000C56 RID: 3158
		private TypeMapping[] referencedMethods;

		// Token: 0x04000C57 RID: 3159
		private int references;

		// Token: 0x04000C58 RID: 3160
		private Hashtable generatedMethods = new Hashtable();

		// Token: 0x04000C59 RID: 3161
		private ModuleBuilder moduleBuilder;

		// Token: 0x04000C5A RID: 3162
		private TypeAttributes typeAttributes;

		// Token: 0x04000C5B RID: 3163
		protected TypeBuilder typeBuilder;

		// Token: 0x04000C5C RID: 3164
		protected CodeGenerator ilg;

		// Token: 0x04000C5D RID: 3165
		private static Dictionary<string, Regex> regexs = new Dictionary<string, Regex>();
	}
}
