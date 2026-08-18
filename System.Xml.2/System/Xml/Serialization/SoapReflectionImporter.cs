using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x02000176 RID: 374
	public class SoapReflectionImporter
	{
		// Token: 0x060018BD RID: 6333 RVA: 0x0006CAA1 File Offset: 0x0006ACA1
		public SoapReflectionImporter() : this(null, null)
		{
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x0006CAAB File Offset: 0x0006ACAB
		public SoapReflectionImporter(string defaultNamespace) : this(null, defaultNamespace)
		{
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x0006CAB5 File Offset: 0x0006ACB5
		public SoapReflectionImporter(SoapAttributeOverrides attributeOverrides) : this(attributeOverrides, null)
		{
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x0006CAC0 File Offset: 0x0006ACC0
		public SoapReflectionImporter(SoapAttributeOverrides attributeOverrides, string defaultNamespace)
		{
			if (defaultNamespace == null)
			{
				defaultNamespace = string.Empty;
			}
			if (attributeOverrides == null)
			{
				attributeOverrides = new SoapAttributeOverrides();
			}
			this.attributeOverrides = attributeOverrides;
			this.defaultNs = defaultNamespace;
			this.typeScope = new TypeScope();
			this.modelScope = new ModelScope(this.typeScope);
		}

		// Token: 0x060018C1 RID: 6337 RVA: 0x0006CB27 File Offset: 0x0006AD27
		public void IncludeTypes(ICustomAttributeProvider provider)
		{
			this.IncludeTypes(provider, new RecursionLimiter());
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x0006CB38 File Offset: 0x0006AD38
		private void IncludeTypes(ICustomAttributeProvider provider, RecursionLimiter limiter)
		{
			object[] customAttributes = provider.GetCustomAttributes(typeof(SoapIncludeAttribute), false);
			for (int i = 0; i < customAttributes.Length; i++)
			{
				this.IncludeType(((SoapIncludeAttribute)customAttributes[i]).Type, limiter);
			}
		}

		// Token: 0x060018C3 RID: 6339 RVA: 0x0006CB79 File Offset: 0x0006AD79
		public void IncludeType(Type type)
		{
			this.IncludeType(type, new RecursionLimiter());
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x0006CB87 File Offset: 0x0006AD87
		private void IncludeType(Type type, RecursionLimiter limiter)
		{
			this.ImportTypeMapping(this.modelScope.GetTypeModel(type), limiter);
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x0006CB9D File Offset: 0x0006AD9D
		public XmlTypeMapping ImportTypeMapping(Type type)
		{
			return this.ImportTypeMapping(type, null);
		}

		// Token: 0x060018C6 RID: 6342 RVA: 0x0006CBA8 File Offset: 0x0006ADA8
		public XmlTypeMapping ImportTypeMapping(Type type, string defaultNamespace)
		{
			ElementAccessor elementAccessor = new ElementAccessor();
			elementAccessor.IsSoap = true;
			elementAccessor.Mapping = this.ImportTypeMapping(this.modelScope.GetTypeModel(type), new RecursionLimiter());
			elementAccessor.Name = elementAccessor.Mapping.DefaultElementName;
			elementAccessor.Namespace = ((elementAccessor.Mapping.Namespace == null) ? defaultNamespace : elementAccessor.Mapping.Namespace);
			elementAccessor.Form = XmlSchemaForm.Qualified;
			XmlTypeMapping xmlTypeMapping = new XmlTypeMapping(this.typeScope, elementAccessor);
			xmlTypeMapping.SetKeyInternal(XmlMapping.GenerateKey(type, null, defaultNamespace));
			xmlTypeMapping.IsSoap = true;
			xmlTypeMapping.GenerateSerializer = true;
			return xmlTypeMapping;
		}

		// Token: 0x060018C7 RID: 6343 RVA: 0x0006CC42 File Offset: 0x0006AE42
		public XmlMembersMapping ImportMembersMapping(string elementName, string ns, XmlReflectionMember[] members)
		{
			return this.ImportMembersMapping(elementName, ns, members, true, true, false);
		}

		// Token: 0x060018C8 RID: 6344 RVA: 0x0006CC50 File Offset: 0x0006AE50
		public XmlMembersMapping ImportMembersMapping(string elementName, string ns, XmlReflectionMember[] members, bool hasWrapperElement, bool writeAccessors)
		{
			return this.ImportMembersMapping(elementName, ns, members, hasWrapperElement, writeAccessors, false);
		}

		// Token: 0x060018C9 RID: 6345 RVA: 0x0006CC60 File Offset: 0x0006AE60
		public XmlMembersMapping ImportMembersMapping(string elementName, string ns, XmlReflectionMember[] members, bool hasWrapperElement, bool writeAccessors, bool validate)
		{
			return this.ImportMembersMapping(elementName, ns, members, hasWrapperElement, writeAccessors, validate, XmlMappingAccess.Read | XmlMappingAccess.Write);
		}

		// Token: 0x060018CA RID: 6346 RVA: 0x0006CC74 File Offset: 0x0006AE74
		public XmlMembersMapping ImportMembersMapping(string elementName, string ns, XmlReflectionMember[] members, bool hasWrapperElement, bool writeAccessors, bool validate, XmlMappingAccess access)
		{
			ElementAccessor elementAccessor = new ElementAccessor();
			elementAccessor.IsSoap = true;
			elementAccessor.Name = ((elementName == null || elementName.Length == 0) ? elementName : XmlConvert.EncodeLocalName(elementName));
			elementAccessor.Mapping = this.ImportMembersMapping(members, ns, hasWrapperElement, writeAccessors, validate, new RecursionLimiter());
			elementAccessor.Mapping.TypeName = elementName;
			elementAccessor.Namespace = ((elementAccessor.Mapping.Namespace == null) ? ns : elementAccessor.Mapping.Namespace);
			elementAccessor.Form = XmlSchemaForm.Qualified;
			return new XmlMembersMapping(this.typeScope, elementAccessor, access)
			{
				IsSoap = true,
				GenerateSerializer = true
			};
		}

		// Token: 0x060018CB RID: 6347 RVA: 0x0006CD13 File Offset: 0x0006AF13
		private Exception ReflectionException(string context, Exception e)
		{
			return new InvalidOperationException(Res.GetString("XmlReflectionError", new object[]
			{
				context
			}), e);
		}

		// Token: 0x060018CC RID: 6348 RVA: 0x0006CD30 File Offset: 0x0006AF30
		private SoapAttributes GetAttributes(Type type)
		{
			SoapAttributes soapAttributes = this.attributeOverrides[type];
			if (soapAttributes != null)
			{
				return soapAttributes;
			}
			return new SoapAttributes(type);
		}

		// Token: 0x060018CD RID: 6349 RVA: 0x0006CD58 File Offset: 0x0006AF58
		private SoapAttributes GetAttributes(MemberInfo memberInfo)
		{
			SoapAttributes soapAttributes = this.attributeOverrides[memberInfo.DeclaringType, memberInfo.Name];
			if (soapAttributes != null)
			{
				return soapAttributes;
			}
			return new SoapAttributes(memberInfo);
		}

		// Token: 0x060018CE RID: 6350 RVA: 0x0006CD88 File Offset: 0x0006AF88
		private TypeMapping ImportTypeMapping(TypeModel model, RecursionLimiter limiter)
		{
			return this.ImportTypeMapping(model, string.Empty, limiter);
		}

		// Token: 0x060018CF RID: 6351 RVA: 0x0006CD98 File Offset: 0x0006AF98
		private TypeMapping ImportTypeMapping(TypeModel model, string dataType, RecursionLimiter limiter)
		{
			if (dataType.Length > 0)
			{
				if (!model.TypeDesc.IsPrimitive)
				{
					throw new InvalidOperationException(Res.GetString("XmlInvalidDataTypeUsage", new object[]
					{
						dataType,
						"SoapElementAttribute.DataType"
					}));
				}
				TypeDesc typeDesc = this.typeScope.GetTypeDesc(dataType, "http://www.w3.org/2001/XMLSchema");
				if (typeDesc == null)
				{
					throw new InvalidOperationException(Res.GetString("XmlInvalidXsdDataType", new object[]
					{
						dataType,
						"SoapElementAttribute.DataType",
						new XmlQualifiedName(dataType, "http://www.w3.org/2001/XMLSchema").ToString()
					}));
				}
				if (model.TypeDesc.FullName != typeDesc.FullName)
				{
					throw new InvalidOperationException(Res.GetString("XmlDataTypeMismatch", new object[]
					{
						dataType,
						"SoapElementAttribute.DataType",
						model.TypeDesc.FullName
					}));
				}
			}
			SoapAttributes attributes = this.GetAttributes(model.Type);
			if ((attributes.SoapFlags & (SoapAttributeFlags)(-3)) != (SoapAttributeFlags)0)
			{
				throw new InvalidOperationException(Res.GetString("XmlInvalidTypeAttributes", new object[]
				{
					model.Type.FullName
				}));
			}
			switch (model.TypeDesc.Kind)
			{
			case TypeKind.Root:
			case TypeKind.Struct:
			case TypeKind.Class:
				if (model.TypeDesc.IsOptionalValue)
				{
					TypeDesc baseTypeDesc = model.TypeDesc.BaseTypeDesc;
					SoapAttributes attributes2 = this.GetAttributes(baseTypeDesc.Type);
					string @namespace = this.defaultNs;
					if (attributes2.SoapType != null && attributes2.SoapType.Namespace != null)
					{
						@namespace = attributes2.SoapType.Namespace;
					}
					TypeDesc typeDesc2 = string.IsNullOrEmpty(dataType) ? model.TypeDesc.BaseTypeDesc : this.typeScope.GetTypeDesc(dataType, "http://www.w3.org/2001/XMLSchema");
					string typeName = string.IsNullOrEmpty(dataType) ? model.TypeDesc.BaseTypeDesc.Name : dataType;
					TypeMapping typeMapping = this.GetTypeMapping(typeName, @namespace, typeDesc2);
					if (typeMapping == null)
					{
						typeMapping = this.ImportTypeMapping(this.modelScope.GetTypeModel(baseTypeDesc.Type), dataType, limiter);
					}
					return this.CreateNullableMapping(typeMapping, model.TypeDesc.Type);
				}
				return this.ImportStructLikeMapping((StructModel)model, limiter);
			case TypeKind.Primitive:
				return this.ImportPrimitiveMapping((PrimitiveModel)model, dataType);
			case TypeKind.Enum:
				return this.ImportEnumMapping((EnumModel)model);
			case TypeKind.Array:
			case TypeKind.Collection:
			case TypeKind.Enumerable:
				return this.ImportArrayLikeMapping((ArrayModel)model, limiter);
			default:
				throw new NotSupportedException(Res.GetString("XmlUnsupportedSoapTypeKind", new object[]
				{
					model.TypeDesc.FullName
				}));
			}
		}

		// Token: 0x060018D0 RID: 6352 RVA: 0x0006D01C File Offset: 0x0006B21C
		private StructMapping CreateRootMapping()
		{
			TypeDesc typeDesc = this.typeScope.GetTypeDesc(typeof(object));
			return new StructMapping
			{
				IsSoap = true,
				TypeDesc = typeDesc,
				Members = new MemberMapping[0],
				IncludeInSchema = false,
				TypeName = "anyType",
				Namespace = "http://www.w3.org/2001/XMLSchema"
			};
		}

		// Token: 0x060018D1 RID: 6353 RVA: 0x0006D07D File Offset: 0x0006B27D
		private StructMapping GetRootMapping()
		{
			if (this.root == null)
			{
				this.root = this.CreateRootMapping();
				this.typeScope.AddTypeMapping(this.root);
			}
			return this.root;
		}

		// Token: 0x060018D2 RID: 6354 RVA: 0x0006D0AC File Offset: 0x0006B2AC
		private TypeMapping GetTypeMapping(string typeName, string ns, TypeDesc typeDesc)
		{
			TypeMapping typeMapping = (TypeMapping)this.types[typeName, ns];
			if (typeMapping == null)
			{
				return null;
			}
			if (typeMapping.TypeDesc != typeDesc)
			{
				throw new InvalidOperationException(Res.GetString("XmlTypesDuplicate", new object[]
				{
					typeDesc.FullName,
					typeMapping.TypeDesc.FullName,
					typeName,
					ns
				}));
			}
			return typeMapping;
		}

		// Token: 0x060018D3 RID: 6355 RVA: 0x0006D110 File Offset: 0x0006B310
		private NullableMapping CreateNullableMapping(TypeMapping baseMapping, Type type)
		{
			TypeDesc nullableTypeDesc = baseMapping.TypeDesc.GetNullableTypeDesc(type);
			TypeMapping typeMapping = (TypeMapping)this.nullables[baseMapping.TypeName, baseMapping.Namespace];
			NullableMapping nullableMapping;
			if (typeMapping != null)
			{
				if (typeMapping is NullableMapping)
				{
					nullableMapping = (NullableMapping)typeMapping;
					if (nullableMapping.BaseMapping is PrimitiveMapping && baseMapping is PrimitiveMapping)
					{
						return nullableMapping;
					}
					if (nullableMapping.BaseMapping == baseMapping)
					{
						return nullableMapping;
					}
					throw new InvalidOperationException(Res.GetString("XmlTypesDuplicate", new object[]
					{
						nullableTypeDesc.FullName,
						typeMapping.TypeDesc.FullName,
						nullableTypeDesc.Name,
						typeMapping.Namespace
					}));
				}
				else if (!(baseMapping is PrimitiveMapping))
				{
					throw new InvalidOperationException(Res.GetString("XmlTypesDuplicate", new object[]
					{
						nullableTypeDesc.FullName,
						typeMapping.TypeDesc.FullName,
						nullableTypeDesc.Name,
						typeMapping.Namespace
					}));
				}
			}
			nullableMapping = new NullableMapping();
			nullableMapping.BaseMapping = baseMapping;
			nullableMapping.TypeDesc = nullableTypeDesc;
			nullableMapping.TypeName = baseMapping.TypeName;
			nullableMapping.Namespace = baseMapping.Namespace;
			nullableMapping.IncludeInSchema = false;
			this.nullables.Add(baseMapping.TypeName, nullableMapping.Namespace, nullableMapping);
			this.typeScope.AddTypeMapping(nullableMapping);
			return nullableMapping;
		}

		// Token: 0x060018D4 RID: 6356 RVA: 0x0006D25C File Offset: 0x0006B45C
		private StructMapping ImportStructLikeMapping(StructModel model, RecursionLimiter limiter)
		{
			if (model.TypeDesc.Kind == TypeKind.Root)
			{
				return this.GetRootMapping();
			}
			SoapAttributes attributes = this.GetAttributes(model.Type);
			string @namespace = this.defaultNs;
			if (attributes.SoapType != null && attributes.SoapType.Namespace != null)
			{
				@namespace = attributes.SoapType.Namespace;
			}
			string text = this.XsdTypeName(model.Type, attributes, model.TypeDesc.Name);
			text = XmlConvert.EncodeLocalName(text);
			StructMapping structMapping = (StructMapping)this.GetTypeMapping(text, @namespace, model.TypeDesc);
			if (structMapping == null)
			{
				structMapping = new StructMapping();
				structMapping.IsSoap = true;
				structMapping.TypeDesc = model.TypeDesc;
				structMapping.Namespace = @namespace;
				structMapping.TypeName = text;
				if (attributes.SoapType != null)
				{
					structMapping.IncludeInSchema = attributes.SoapType.IncludeInSchema;
				}
				this.typeScope.AddTypeMapping(structMapping);
				this.types.Add(text, @namespace, structMapping);
				if (limiter.IsExceededLimit)
				{
					limiter.DeferredWorkItems.Add(new ImportStructWorkItem(model, structMapping));
					return structMapping;
				}
				int depth = limiter.Depth;
				limiter.Depth = depth + 1;
				this.InitializeStructMembers(structMapping, model, limiter);
				while (limiter.DeferredWorkItems.Count > 0)
				{
					int index = limiter.DeferredWorkItems.Count - 1;
					ImportStructWorkItem importStructWorkItem = limiter.DeferredWorkItems[index];
					if (this.InitializeStructMembers(importStructWorkItem.Mapping, importStructWorkItem.Model, limiter))
					{
						limiter.DeferredWorkItems.RemoveAt(index);
					}
				}
				depth = limiter.Depth;
				limiter.Depth = depth - 1;
			}
			return structMapping;
		}

		// Token: 0x060018D5 RID: 6357 RVA: 0x0006D3E4 File Offset: 0x0006B5E4
		private bool InitializeStructMembers(StructMapping mapping, StructModel model, RecursionLimiter limiter)
		{
			if (mapping.IsFullyInitialized)
			{
				return true;
			}
			if (model.TypeDesc.BaseTypeDesc != null)
			{
				StructMapping baseMapping = this.ImportStructLikeMapping((StructModel)this.modelScope.GetTypeModel(model.Type.BaseType, false), limiter);
				int num = limiter.DeferredWorkItems.IndexOf(mapping.BaseMapping);
				if (num >= 0)
				{
					if (!limiter.DeferredWorkItems.Contains(mapping))
					{
						limiter.DeferredWorkItems.Add(new ImportStructWorkItem(model, mapping));
					}
					int num2 = limiter.DeferredWorkItems.Count - 1;
					if (num < num2)
					{
						ImportStructWorkItem value = limiter.DeferredWorkItems[num];
						limiter.DeferredWorkItems[num] = limiter.DeferredWorkItems[num2];
						limiter.DeferredWorkItems[num2] = value;
					}
					return false;
				}
				mapping.BaseMapping = baseMapping;
			}
			ArrayList arrayList = new ArrayList();
			foreach (MemberInfo memberInfo in model.GetMemberInfos())
			{
				if ((memberInfo.MemberType & (MemberTypes.Field | MemberTypes.Property)) != (MemberTypes)0)
				{
					SoapAttributes attributes = this.GetAttributes(memberInfo);
					if (!attributes.SoapIgnore)
					{
						FieldModel fieldModel = model.GetFieldModel(memberInfo);
						if (fieldModel != null)
						{
							MemberMapping memberMapping = this.ImportFieldMapping(fieldModel, attributes, mapping.Namespace, limiter);
							if (memberMapping != null)
							{
								if (!memberMapping.TypeDesc.IsPrimitive && !memberMapping.TypeDesc.IsEnum && !memberMapping.TypeDesc.IsOptionalValue)
								{
									if (model.TypeDesc.IsValueType)
									{
										throw new NotSupportedException(Res.GetString("XmlRpcRefsInValueType", new object[]
										{
											model.TypeDesc.FullName
										}));
									}
									if (memberMapping.TypeDesc.IsValueType)
									{
										throw new NotSupportedException(Res.GetString("XmlRpcNestedValueType", new object[]
										{
											memberMapping.TypeDesc.FullName
										}));
									}
								}
								if (mapping.BaseMapping == null || !mapping.BaseMapping.Declares(memberMapping, mapping.TypeName))
								{
									arrayList.Add(memberMapping);
								}
							}
						}
					}
				}
			}
			mapping.Members = (MemberMapping[])arrayList.ToArray(typeof(MemberMapping));
			if (mapping.BaseMapping == null)
			{
				mapping.BaseMapping = this.GetRootMapping();
			}
			this.IncludeTypes(model.Type, limiter);
			return true;
		}

		// Token: 0x060018D6 RID: 6358 RVA: 0x0006D628 File Offset: 0x0006B828
		private ArrayMapping ImportArrayLikeMapping(ArrayModel model, RecursionLimiter limiter)
		{
			ArrayMapping arrayMapping = new ArrayMapping();
			arrayMapping.IsSoap = true;
			TypeMapping typeMapping = this.ImportTypeMapping(model.Element, limiter);
			if (typeMapping.TypeDesc.IsValueType && !typeMapping.TypeDesc.IsPrimitive && !typeMapping.TypeDesc.IsEnum)
			{
				throw new NotSupportedException(Res.GetString("XmlRpcArrayOfValueTypes", new object[]
				{
					model.TypeDesc.FullName
				}));
			}
			arrayMapping.TypeDesc = model.TypeDesc;
			arrayMapping.Elements = new ElementAccessor[]
			{
				SoapReflectionImporter.CreateElementAccessor(typeMapping, arrayMapping.Namespace)
			};
			this.SetArrayMappingType(arrayMapping);
			ArrayMapping arrayMapping2 = (ArrayMapping)this.types[arrayMapping.TypeName, arrayMapping.Namespace];
			if (arrayMapping2 != null)
			{
				ArrayMapping next = arrayMapping2;
				while (arrayMapping2 != null)
				{
					if (arrayMapping2.TypeDesc == model.TypeDesc)
					{
						return arrayMapping2;
					}
					arrayMapping2 = arrayMapping2.Next;
				}
				arrayMapping.Next = next;
				this.types[arrayMapping.TypeName, arrayMapping.Namespace] = arrayMapping;
				return arrayMapping;
			}
			this.typeScope.AddTypeMapping(arrayMapping);
			this.types.Add(arrayMapping.TypeName, arrayMapping.Namespace, arrayMapping);
			this.IncludeTypes(model.Type);
			return arrayMapping;
		}

		// Token: 0x060018D7 RID: 6359 RVA: 0x0006D75C File Offset: 0x0006B95C
		private void SetArrayMappingType(ArrayMapping mapping)
		{
			bool flag = false;
			TypeMapping typeMapping;
			if (mapping.Elements.Length == 1)
			{
				typeMapping = mapping.Elements[0].Mapping;
			}
			else
			{
				typeMapping = null;
			}
			string text;
			string text2;
			if (typeMapping is EnumMapping)
			{
				text = typeMapping.Namespace;
				text2 = typeMapping.TypeName;
			}
			else if (typeMapping is PrimitiveMapping)
			{
				text = (typeMapping.TypeDesc.IsXsdType ? "http://www.w3.org/2001/XMLSchema" : "http://microsoft.com/wsdl/types/");
				text2 = typeMapping.TypeDesc.DataType.Name;
				flag = true;
			}
			else if (typeMapping is StructMapping)
			{
				if (typeMapping.TypeDesc.IsRoot)
				{
					text = "http://www.w3.org/2001/XMLSchema";
					text2 = "anyType";
					flag = true;
				}
				else
				{
					text = typeMapping.Namespace;
					text2 = typeMapping.TypeName;
				}
			}
			else
			{
				if (!(typeMapping is ArrayMapping))
				{
					throw new InvalidOperationException(Res.GetString("XmlInvalidSoapArray", new object[]
					{
						mapping.TypeDesc.FullName
					}));
				}
				text = typeMapping.Namespace;
				text2 = typeMapping.TypeName;
			}
			text2 = CodeIdentifier.MakePascal(text2);
			string text3 = "ArrayOf" + text2;
			string text4 = flag ? this.defaultNs : text;
			int num = 1;
			TypeMapping typeMapping2 = (TypeMapping)this.types[text3, text4];
			while (typeMapping2 != null)
			{
				if (typeMapping2 is ArrayMapping)
				{
					ArrayMapping arrayMapping = (ArrayMapping)typeMapping2;
					if (AccessorMapping.ElementsMatch(arrayMapping.Elements, mapping.Elements))
					{
						break;
					}
				}
				text3 = text2 + num.ToString(CultureInfo.InvariantCulture);
				typeMapping2 = (TypeMapping)this.types[text3, text4];
				num++;
			}
			mapping.Namespace = text4;
			mapping.TypeName = text3;
		}

		// Token: 0x060018D8 RID: 6360 RVA: 0x0006D8F4 File Offset: 0x0006BAF4
		private PrimitiveMapping ImportPrimitiveMapping(PrimitiveModel model, string dataType)
		{
			PrimitiveMapping primitiveMapping = new PrimitiveMapping();
			primitiveMapping.IsSoap = true;
			if (dataType.Length > 0)
			{
				primitiveMapping.TypeDesc = this.typeScope.GetTypeDesc(dataType, "http://www.w3.org/2001/XMLSchema");
				if (primitiveMapping.TypeDesc == null)
				{
					primitiveMapping.TypeDesc = this.typeScope.GetTypeDesc(dataType, "http://microsoft.com/wsdl/types/");
					if (primitiveMapping.TypeDesc == null)
					{
						throw new InvalidOperationException(Res.GetString("XmlUdeclaredXsdType", new object[]
						{
							dataType
						}));
					}
				}
			}
			else
			{
				primitiveMapping.TypeDesc = model.TypeDesc;
			}
			primitiveMapping.TypeName = primitiveMapping.TypeDesc.DataType.Name;
			primitiveMapping.Namespace = (primitiveMapping.TypeDesc.IsXsdType ? "http://www.w3.org/2001/XMLSchema" : "http://microsoft.com/wsdl/types/");
			return primitiveMapping;
		}

		// Token: 0x060018D9 RID: 6361 RVA: 0x0006D9B4 File Offset: 0x0006BBB4
		private EnumMapping ImportEnumMapping(EnumModel model)
		{
			SoapAttributes attributes = this.GetAttributes(model.Type);
			string @namespace = this.defaultNs;
			if (attributes.SoapType != null && attributes.SoapType.Namespace != null)
			{
				@namespace = attributes.SoapType.Namespace;
			}
			string text = this.XsdTypeName(model.Type, attributes, model.TypeDesc.Name);
			text = XmlConvert.EncodeLocalName(text);
			EnumMapping enumMapping = (EnumMapping)this.GetTypeMapping(text, @namespace, model.TypeDesc);
			if (enumMapping == null)
			{
				enumMapping = new EnumMapping();
				enumMapping.IsSoap = true;
				enumMapping.TypeDesc = model.TypeDesc;
				enumMapping.TypeName = text;
				enumMapping.Namespace = @namespace;
				enumMapping.IsFlags = model.Type.IsDefined(typeof(FlagsAttribute), false);
				this.typeScope.AddTypeMapping(enumMapping);
				this.types.Add(text, @namespace, enumMapping);
				ArrayList arrayList = new ArrayList();
				for (int i = 0; i < model.Constants.Length; i++)
				{
					ConstantMapping constantMapping = this.ImportConstantMapping(model.Constants[i]);
					if (constantMapping != null)
					{
						arrayList.Add(constantMapping);
					}
				}
				if (arrayList.Count == 0)
				{
					throw new InvalidOperationException(Res.GetString("XmlNoSerializableMembers", new object[]
					{
						model.TypeDesc.FullName
					}));
				}
				enumMapping.Constants = (ConstantMapping[])arrayList.ToArray(typeof(ConstantMapping));
			}
			return enumMapping;
		}

		// Token: 0x060018DA RID: 6362 RVA: 0x0006DB14 File Offset: 0x0006BD14
		private ConstantMapping ImportConstantMapping(ConstantModel model)
		{
			SoapAttributes attributes = this.GetAttributes(model.FieldInfo);
			if (attributes.SoapIgnore)
			{
				return null;
			}
			if ((attributes.SoapFlags & (SoapAttributeFlags)(-2)) != (SoapAttributeFlags)0)
			{
				throw new InvalidOperationException(Res.GetString("XmlInvalidEnumAttribute"));
			}
			if (attributes.SoapEnum == null)
			{
				attributes.SoapEnum = new SoapEnumAttribute();
			}
			return new ConstantMapping
			{
				XmlName = ((attributes.SoapEnum.Name.Length == 0) ? model.Name : attributes.SoapEnum.Name),
				Name = model.Name,
				Value = model.Value
			};
		}

		// Token: 0x060018DB RID: 6363 RVA: 0x0006DBB0 File Offset: 0x0006BDB0
		private MembersMapping ImportMembersMapping(XmlReflectionMember[] xmlReflectionMembers, string ns, bool hasWrapperElement, bool writeAccessors, bool validateWrapperElement, RecursionLimiter limiter)
		{
			MembersMapping membersMapping = new MembersMapping();
			membersMapping.TypeDesc = this.typeScope.GetTypeDesc(typeof(object[]));
			MemberMapping[] array = new MemberMapping[xmlReflectionMembers.Length];
			for (int i = 0; i < array.Length; i++)
			{
				try
				{
					XmlReflectionMember xmlReflectionMember = xmlReflectionMembers[i];
					MemberMapping memberMapping = this.ImportMemberMapping(xmlReflectionMember, ns, xmlReflectionMembers, hasWrapperElement ? XmlSchemaForm.Unqualified : XmlSchemaForm.Qualified, limiter);
					if (xmlReflectionMember.IsReturnValue && writeAccessors)
					{
						if (i > 0)
						{
							throw new InvalidOperationException(Res.GetString("XmlInvalidReturnPosition"));
						}
						memberMapping.IsReturnValue = true;
					}
					array[i] = memberMapping;
				}
				catch (Exception ex)
				{
					if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
					{
						throw;
					}
					throw this.ReflectionException(xmlReflectionMembers[i].MemberName, ex);
				}
			}
			membersMapping.Members = array;
			membersMapping.HasWrapperElement = hasWrapperElement;
			if (hasWrapperElement)
			{
				membersMapping.ValidateRpcWrapperElement = validateWrapperElement;
			}
			membersMapping.WriteAccessors = writeAccessors;
			membersMapping.IsSoap = true;
			if (hasWrapperElement && !writeAccessors)
			{
				membersMapping.Namespace = ns;
			}
			return membersMapping;
		}

		// Token: 0x060018DC RID: 6364 RVA: 0x0006DCB8 File Offset: 0x0006BEB8
		private MemberMapping ImportMemberMapping(XmlReflectionMember xmlReflectionMember, string ns, XmlReflectionMember[] xmlReflectionMembers, XmlSchemaForm form, RecursionLimiter limiter)
		{
			SoapAttributes soapAttributes = xmlReflectionMember.SoapAttributes;
			if (soapAttributes.SoapIgnore)
			{
				return null;
			}
			MemberMapping memberMapping = new MemberMapping();
			memberMapping.IsSoap = true;
			memberMapping.Name = xmlReflectionMember.MemberName;
			bool checkSpecified = XmlReflectionImporter.FindSpecifiedMember(xmlReflectionMember.MemberName, xmlReflectionMembers) != null;
			FieldModel fieldModel = new FieldModel(xmlReflectionMember.MemberName, xmlReflectionMember.MemberType, this.typeScope.GetTypeDesc(xmlReflectionMember.MemberType), checkSpecified, false);
			memberMapping.CheckShouldPersist = fieldModel.CheckShouldPersist;
			memberMapping.CheckSpecified = fieldModel.CheckSpecified;
			memberMapping.ReadOnly = fieldModel.ReadOnly;
			this.ImportAccessorMapping(memberMapping, fieldModel, soapAttributes, ns, form, limiter);
			if (xmlReflectionMember.OverrideIsNullable)
			{
				memberMapping.Elements[0].IsNullable = false;
			}
			return memberMapping;
		}

		// Token: 0x060018DD RID: 6365 RVA: 0x0006DD70 File Offset: 0x0006BF70
		private MemberMapping ImportFieldMapping(FieldModel model, SoapAttributes a, string ns, RecursionLimiter limiter)
		{
			if (a.SoapIgnore)
			{
				return null;
			}
			MemberMapping memberMapping = new MemberMapping();
			memberMapping.IsSoap = true;
			memberMapping.Name = model.Name;
			memberMapping.CheckShouldPersist = model.CheckShouldPersist;
			memberMapping.CheckSpecified = model.CheckSpecified;
			memberMapping.MemberInfo = model.MemberInfo;
			memberMapping.CheckSpecifiedMemberInfo = model.CheckSpecifiedMemberInfo;
			memberMapping.CheckShouldPersistMethodInfo = model.CheckShouldPersistMethodInfo;
			memberMapping.ReadOnly = model.ReadOnly;
			this.ImportAccessorMapping(memberMapping, model, a, ns, XmlSchemaForm.Unqualified, limiter);
			return memberMapping;
		}

		// Token: 0x060018DE RID: 6366 RVA: 0x0006DDF8 File Offset: 0x0006BFF8
		private void ImportAccessorMapping(MemberMapping accessor, FieldModel model, SoapAttributes a, string ns, XmlSchemaForm form, RecursionLimiter limiter)
		{
			Type fieldType = model.FieldType;
			string name = model.Name;
			accessor.TypeDesc = this.typeScope.GetTypeDesc(fieldType);
			if (accessor.TypeDesc.IsVoid)
			{
				throw new InvalidOperationException(Res.GetString("XmlInvalidVoid"));
			}
			SoapAttributeFlags soapFlags = a.SoapFlags;
			if ((soapFlags & SoapAttributeFlags.Attribute) == SoapAttributeFlags.Attribute)
			{
				if (!accessor.TypeDesc.IsPrimitive && !accessor.TypeDesc.IsEnum)
				{
					throw new InvalidOperationException(Res.GetString("XmlIllegalSoapAttribute", new object[]
					{
						name,
						accessor.TypeDesc.FullName
					}));
				}
				if ((soapFlags & SoapAttributeFlags.Attribute) != soapFlags)
				{
					throw new InvalidOperationException(Res.GetString("XmlInvalidElementAttribute"));
				}
				accessor.Attribute = new AttributeAccessor
				{
					Name = Accessor.EscapeQName((a.SoapAttribute == null || a.SoapAttribute.AttributeName.Length == 0) ? name : a.SoapAttribute.AttributeName),
					Namespace = ((a.SoapAttribute == null || a.SoapAttribute.Namespace == null) ? ns : a.SoapAttribute.Namespace),
					Form = XmlSchemaForm.Qualified,
					Mapping = this.ImportTypeMapping(this.modelScope.GetTypeModel(fieldType), (a.SoapAttribute == null) ? string.Empty : a.SoapAttribute.DataType, limiter),
					Default = this.GetDefaultValue(model.FieldTypeDesc, a)
				};
				accessor.Elements = new ElementAccessor[0];
				return;
			}
			else
			{
				if ((soapFlags & SoapAttributeFlags.Element) != soapFlags)
				{
					throw new InvalidOperationException(Res.GetString("XmlInvalidElementAttribute"));
				}
				ElementAccessor elementAccessor = new ElementAccessor();
				elementAccessor.IsSoap = true;
				elementAccessor.Name = XmlConvert.EncodeLocalName((a.SoapElement == null || a.SoapElement.ElementName.Length == 0) ? name : a.SoapElement.ElementName);
				elementAccessor.Namespace = ns;
				elementAccessor.Form = form;
				elementAccessor.Mapping = this.ImportTypeMapping(this.modelScope.GetTypeModel(fieldType), (a.SoapElement == null) ? string.Empty : a.SoapElement.DataType, limiter);
				if (a.SoapElement != null)
				{
					elementAccessor.IsNullable = a.SoapElement.IsNullable;
				}
				accessor.Elements = new ElementAccessor[]
				{
					elementAccessor
				};
				return;
			}
		}

		// Token: 0x060018DF RID: 6367 RVA: 0x0006E03C File Offset: 0x0006C23C
		private static ElementAccessor CreateElementAccessor(TypeMapping mapping, string ns)
		{
			return new ElementAccessor
			{
				IsSoap = true,
				Name = mapping.TypeName,
				Namespace = ns,
				Mapping = mapping
			};
		}

		// Token: 0x060018E0 RID: 6368 RVA: 0x0006E074 File Offset: 0x0006C274
		private object GetDefaultValue(TypeDesc fieldTypeDesc, SoapAttributes a)
		{
			if (a.SoapDefaultValue == null || a.SoapDefaultValue == DBNull.Value)
			{
				return null;
			}
			if (fieldTypeDesc.Kind != TypeKind.Primitive && fieldTypeDesc.Kind != TypeKind.Enum)
			{
				a.SoapDefaultValue = null;
				return a.SoapDefaultValue;
			}
			if (fieldTypeDesc.Kind != TypeKind.Enum)
			{
				return a.SoapDefaultValue;
			}
			if (fieldTypeDesc != this.typeScope.GetTypeDesc(a.SoapDefaultValue.GetType()))
			{
				throw new InvalidOperationException(Res.GetString("XmlInvalidDefaultEnumValue", new object[]
				{
					a.SoapDefaultValue.GetType().FullName,
					fieldTypeDesc.FullName
				}));
			}
			string text = Enum.Format(a.SoapDefaultValue.GetType(), a.SoapDefaultValue, "G").Replace(",", " ");
			string b = Enum.Format(a.SoapDefaultValue.GetType(), a.SoapDefaultValue, "D");
			if (text == b)
			{
				throw new InvalidOperationException(Res.GetString("XmlInvalidDefaultValue", new object[]
				{
					text,
					a.SoapDefaultValue.GetType().FullName
				}));
			}
			return text;
		}

		// Token: 0x060018E1 RID: 6369 RVA: 0x0006E194 File Offset: 0x0006C394
		internal string XsdTypeName(Type type)
		{
			if (type == typeof(object))
			{
				return "anyType";
			}
			TypeDesc typeDesc = this.typeScope.GetTypeDesc(type);
			if (typeDesc.IsPrimitive && typeDesc.DataType != null && typeDesc.DataType.Name != null && typeDesc.DataType.Name.Length > 0)
			{
				return typeDesc.DataType.Name;
			}
			return this.XsdTypeName(type, this.GetAttributes(type), typeDesc.Name);
		}

		// Token: 0x060018E2 RID: 6370 RVA: 0x0006E218 File Offset: 0x0006C418
		internal string XsdTypeName(Type type, SoapAttributes a, string name)
		{
			string text = name;
			if (a.SoapType != null && a.SoapType.TypeName.Length > 0)
			{
				text = a.SoapType.TypeName;
			}
			if (type.IsGenericType && text.IndexOf('{') >= 0)
			{
				Type genericTypeDefinition = type.GetGenericTypeDefinition();
				Type[] genericArguments = genericTypeDefinition.GetGenericArguments();
				Type[] genericArguments2 = type.GetGenericArguments();
				for (int i = 0; i < genericArguments.Length; i++)
				{
					string str = "{";
					Type type2 = genericArguments[i];
					string text2 = str + ((type2 != null) ? type2.ToString() : null) + "}";
					if (text.Contains(text2))
					{
						text = text.Replace(text2, this.XsdTypeName(genericArguments2[i]));
						if (text.IndexOf('{') < 0)
						{
							break;
						}
					}
				}
			}
			return text;
		}

		// Token: 0x04000B51 RID: 2897
		private TypeScope typeScope;

		// Token: 0x04000B52 RID: 2898
		private SoapAttributeOverrides attributeOverrides;

		// Token: 0x04000B53 RID: 2899
		private NameTable types = new NameTable();

		// Token: 0x04000B54 RID: 2900
		private NameTable nullables = new NameTable();

		// Token: 0x04000B55 RID: 2901
		private StructMapping root;

		// Token: 0x04000B56 RID: 2902
		private string defaultNs;

		// Token: 0x04000B57 RID: 2903
		private ModelScope modelScope;
	}
}
