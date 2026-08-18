using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Security.Permissions;
using System.Xml.Schema;
using System.Xml.Serialization.Advanced;

namespace System.Xml.Serialization
{
	// Token: 0x0200018F RID: 399
	public class XmlCodeExporter : CodeExporter
	{
		// Token: 0x06001A13 RID: 6675 RVA: 0x00073AD8 File Offset: 0x00071CD8
		public XmlCodeExporter(CodeNamespace codeNamespace) : base(codeNamespace, null, null, CodeGenerationOptions.GenerateProperties, null)
		{
		}

		// Token: 0x06001A14 RID: 6676 RVA: 0x00073AE5 File Offset: 0x00071CE5
		public XmlCodeExporter(CodeNamespace codeNamespace, CodeCompileUnit codeCompileUnit) : base(codeNamespace, codeCompileUnit, null, CodeGenerationOptions.GenerateProperties, null)
		{
		}

		// Token: 0x06001A15 RID: 6677 RVA: 0x00073AF2 File Offset: 0x00071CF2
		public XmlCodeExporter(CodeNamespace codeNamespace, CodeCompileUnit codeCompileUnit, CodeGenerationOptions options) : base(codeNamespace, codeCompileUnit, null, options, null)
		{
		}

		// Token: 0x06001A16 RID: 6678 RVA: 0x00073AFF File Offset: 0x00071CFF
		public XmlCodeExporter(CodeNamespace codeNamespace, CodeCompileUnit codeCompileUnit, CodeGenerationOptions options, Hashtable mappings) : base(codeNamespace, codeCompileUnit, null, options, mappings)
		{
		}

		// Token: 0x06001A17 RID: 6679 RVA: 0x00073B0D File Offset: 0x00071D0D
		public XmlCodeExporter(CodeNamespace codeNamespace, CodeCompileUnit codeCompileUnit, CodeDomProvider codeProvider, CodeGenerationOptions options, Hashtable mappings) : base(codeNamespace, codeCompileUnit, codeProvider, options, mappings)
		{
		}

		// Token: 0x06001A18 RID: 6680 RVA: 0x00073B1C File Offset: 0x00071D1C
		public void ExportTypeMapping(XmlTypeMapping xmlTypeMapping)
		{
			xmlTypeMapping.CheckShallow();
			base.CheckScope(xmlTypeMapping.Scope);
			if (xmlTypeMapping.Accessor.Any)
			{
				throw new InvalidOperationException(Res.GetString("XmlIllegalWildcard"));
			}
			this.ExportElement(xmlTypeMapping.Accessor);
		}

		// Token: 0x06001A19 RID: 6681 RVA: 0x00073B5C File Offset: 0x00071D5C
		public void ExportMembersMapping(XmlMembersMapping xmlMembersMapping)
		{
			xmlMembersMapping.CheckShallow();
			base.CheckScope(xmlMembersMapping.Scope);
			for (int i = 0; i < xmlMembersMapping.Count; i++)
			{
				AccessorMapping mapping = xmlMembersMapping[i].Mapping;
				if (mapping.Xmlns == null)
				{
					if (mapping.Attribute != null)
					{
						this.ExportType(mapping.Attribute.Mapping, Accessor.UnescapeName(mapping.Attribute.Name), mapping.Attribute.Namespace, null, false);
					}
					if (mapping.Elements != null)
					{
						for (int j = 0; j < mapping.Elements.Length; j++)
						{
							ElementAccessor elementAccessor = mapping.Elements[j];
							this.ExportType(elementAccessor.Mapping, Accessor.UnescapeName(elementAccessor.Name), elementAccessor.Namespace, null, false);
						}
					}
					if (mapping.Text != null)
					{
						this.ExportType(mapping.Text.Mapping, Accessor.UnescapeName(mapping.Text.Name), mapping.Text.Namespace, null, false);
					}
				}
			}
		}

		// Token: 0x06001A1A RID: 6682 RVA: 0x00073C59 File Offset: 0x00071E59
		private void ExportElement(ElementAccessor element)
		{
			this.ExportType(element.Mapping, Accessor.UnescapeName(element.Name), element.Namespace, element, true);
		}

		// Token: 0x06001A1B RID: 6683 RVA: 0x00073C7A File Offset: 0x00071E7A
		private void ExportType(TypeMapping mapping, string ns)
		{
			this.ExportType(mapping, null, ns, null, true);
		}

		// Token: 0x06001A1C RID: 6684 RVA: 0x00073C88 File Offset: 0x00071E88
		private void ExportType(TypeMapping mapping, string name, string ns, ElementAccessor rootElement, bool checkReference)
		{
			if (mapping.IsReference && mapping.Namespace != "http://schemas.xmlsoap.org/soap/encoding/")
			{
				return;
			}
			if (mapping is StructMapping && checkReference && ((StructMapping)mapping).ReferencedByTopLevelElement && rootElement == null)
			{
				return;
			}
			if (mapping is ArrayMapping && rootElement != null && rootElement.IsTopLevelInSchema && ((ArrayMapping)mapping).TopLevelMapping != null)
			{
				mapping = ((ArrayMapping)mapping).TopLevelMapping;
			}
			CodeTypeDeclaration codeTypeDeclaration = null;
			if (base.ExportedMappings[mapping] == null)
			{
				base.ExportedMappings.Add(mapping, mapping);
				if (mapping.TypeDesc.IsMappedType)
				{
					codeTypeDeclaration = mapping.TypeDesc.ExtendedType.ExportTypeDefinition(base.CodeNamespace, base.CodeCompileUnit);
				}
				else if (mapping is EnumMapping)
				{
					codeTypeDeclaration = base.ExportEnum((EnumMapping)mapping, typeof(XmlEnumAttribute));
				}
				else if (mapping is StructMapping)
				{
					codeTypeDeclaration = this.ExportStruct((StructMapping)mapping);
				}
				else if (mapping is ArrayMapping)
				{
					Accessor[] elements = ((ArrayMapping)mapping).Elements;
					this.EnsureTypesExported(elements, ns);
				}
				if (codeTypeDeclaration != null)
				{
					if (!mapping.TypeDesc.IsMappedType)
					{
						codeTypeDeclaration.CustomAttributes.Add(base.GeneratedCodeAttribute);
						codeTypeDeclaration.CustomAttributes.Add(new CodeAttributeDeclaration(typeof(SerializableAttribute).FullName));
						if (!codeTypeDeclaration.IsEnum)
						{
							codeTypeDeclaration.CustomAttributes.Add(new CodeAttributeDeclaration(typeof(DebuggerStepThroughAttribute).FullName));
							codeTypeDeclaration.CustomAttributes.Add(new CodeAttributeDeclaration(typeof(DesignerCategoryAttribute).FullName, new CodeAttributeArgument[]
							{
								new CodeAttributeArgument(new CodePrimitiveExpression("code"))
							}));
						}
						base.AddTypeMetadata(codeTypeDeclaration.CustomAttributes, typeof(XmlTypeAttribute), mapping.TypeDesc.Name, Accessor.UnescapeName(mapping.TypeName), mapping.Namespace, mapping.IncludeInSchema);
					}
					else if (CodeExporter.FindAttributeDeclaration(typeof(GeneratedCodeAttribute), codeTypeDeclaration.CustomAttributes) == null)
					{
						codeTypeDeclaration.CustomAttributes.Add(base.GeneratedCodeAttribute);
					}
					base.ExportedClasses.Add(mapping, codeTypeDeclaration);
				}
			}
			else
			{
				codeTypeDeclaration = (CodeTypeDeclaration)base.ExportedClasses[mapping];
			}
			if (codeTypeDeclaration != null && rootElement != null)
			{
				this.AddRootMetadata(codeTypeDeclaration.CustomAttributes, mapping, name, ns, rootElement);
			}
		}

		// Token: 0x06001A1D RID: 6685 RVA: 0x00073EE4 File Offset: 0x000720E4
		private void AddRootMetadata(CodeAttributeDeclarationCollection metadata, TypeMapping typeMapping, string name, string ns, ElementAccessor rootElement)
		{
			string fullName = typeof(XmlRootAttribute).FullName;
			foreach (object obj in metadata)
			{
				CodeAttributeDeclaration codeAttributeDeclaration = (CodeAttributeDeclaration)obj;
				if (codeAttributeDeclaration.Name == fullName)
				{
					return;
				}
			}
			CodeAttributeDeclaration codeAttributeDeclaration2 = new CodeAttributeDeclaration(fullName);
			if (typeMapping.TypeDesc.Name != name)
			{
				codeAttributeDeclaration2.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(name)));
			}
			if (ns != null)
			{
				codeAttributeDeclaration2.Arguments.Add(new CodeAttributeArgument("Namespace", new CodePrimitiveExpression(ns)));
			}
			if (typeMapping.TypeDesc != null && typeMapping.TypeDesc.IsAmbiguousDataType)
			{
				codeAttributeDeclaration2.Arguments.Add(new CodeAttributeArgument("DataType", new CodePrimitiveExpression(typeMapping.TypeDesc.DataType.Name)));
			}
			if (rootElement.IsNullable != null)
			{
				codeAttributeDeclaration2.Arguments.Add(new CodeAttributeArgument("IsNullable", new CodePrimitiveExpression(rootElement.IsNullable)));
			}
			metadata.Add(codeAttributeDeclaration2);
		}

		// Token: 0x06001A1E RID: 6686 RVA: 0x00074028 File Offset: 0x00072228
		private CodeAttributeArgument[] GetDefaultValueArguments(PrimitiveMapping mapping, object value, out CodeExpression initExpression)
		{
			initExpression = null;
			if (value == null)
			{
				return null;
			}
			CodeExpression codeExpression = null;
			Type type = value.GetType();
			CodeAttributeArgument[] result = null;
			if (mapping is EnumMapping)
			{
				if (((EnumMapping)mapping).IsFlags)
				{
					string[] array = ((string)value).Split(null);
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i].Length != 0)
						{
							CodeExpression codeExpression2 = new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(mapping.TypeDesc.FullName), array[i]);
							if (codeExpression != null)
							{
								codeExpression = new CodeBinaryOperatorExpression(codeExpression, CodeBinaryOperatorType.BitwiseOr, codeExpression2);
							}
							else
							{
								codeExpression = codeExpression2;
							}
						}
					}
				}
				else
				{
					codeExpression = new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(mapping.TypeDesc.FullName), (string)value);
				}
				initExpression = codeExpression;
				result = new CodeAttributeArgument[]
				{
					new CodeAttributeArgument(codeExpression)
				};
			}
			else if (type == typeof(bool) || type == typeof(int) || type == typeof(string) || type == typeof(double))
			{
				initExpression = (codeExpression = new CodePrimitiveExpression(value));
				result = new CodeAttributeArgument[]
				{
					new CodeAttributeArgument(codeExpression)
				};
			}
			else if (type == typeof(short) || type == typeof(long) || type == typeof(float) || type == typeof(byte) || type == typeof(decimal))
			{
				codeExpression = new CodePrimitiveExpression(Convert.ToString(value, NumberFormatInfo.InvariantInfo));
				CodeExpression value2 = new CodeTypeOfExpression(type.FullName);
				result = new CodeAttributeArgument[]
				{
					new CodeAttributeArgument(value2),
					new CodeAttributeArgument(codeExpression)
				};
				initExpression = new CodeCastExpression(type.FullName, new CodePrimitiveExpression(value));
			}
			else if (type == typeof(sbyte) || type == typeof(ushort) || type == typeof(uint) || type == typeof(ulong))
			{
				value = CodeExporter.PromoteType(type, value);
				codeExpression = new CodePrimitiveExpression(Convert.ToString(value, NumberFormatInfo.InvariantInfo));
				CodeExpression value2 = new CodeTypeOfExpression(type.FullName);
				result = new CodeAttributeArgument[]
				{
					new CodeAttributeArgument(value2),
					new CodeAttributeArgument(codeExpression)
				};
				initExpression = new CodeCastExpression(type.FullName, new CodePrimitiveExpression(value));
			}
			else if (type == typeof(DateTime))
			{
				DateTime value3 = (DateTime)value;
				string value4;
				long ticks;
				if (mapping.TypeDesc.FormatterName == "Date")
				{
					value4 = XmlCustomFormatter.FromDate(value3);
					ticks = new DateTime(value3.Year, value3.Month, value3.Day).Ticks;
				}
				else if (mapping.TypeDesc.FormatterName == "Time")
				{
					value4 = XmlCustomFormatter.FromDateTime(value3);
					ticks = value3.Ticks;
				}
				else
				{
					value4 = XmlCustomFormatter.FromDateTime(value3);
					ticks = value3.Ticks;
				}
				codeExpression = new CodePrimitiveExpression(value4);
				CodeExpression value2 = new CodeTypeOfExpression(type.FullName);
				result = new CodeAttributeArgument[]
				{
					new CodeAttributeArgument(value2),
					new CodeAttributeArgument(codeExpression)
				};
				initExpression = new CodeObjectCreateExpression(new CodeTypeReference(typeof(DateTime)), new CodeExpression[]
				{
					new CodePrimitiveExpression(ticks)
				});
			}
			else if (type == typeof(Guid))
			{
				codeExpression = new CodePrimitiveExpression(Convert.ToString(value, NumberFormatInfo.InvariantInfo));
				CodeExpression value2 = new CodeTypeOfExpression(type.FullName);
				result = new CodeAttributeArgument[]
				{
					new CodeAttributeArgument(value2),
					new CodeAttributeArgument(codeExpression)
				};
				initExpression = new CodeObjectCreateExpression(new CodeTypeReference(typeof(Guid)), new CodeExpression[]
				{
					codeExpression
				});
			}
			if (mapping.TypeDesc.FullName != type.ToString() && !(mapping is EnumMapping))
			{
				initExpression = new CodeCastExpression(mapping.TypeDesc.FullName, initExpression);
			}
			return result;
		}

		// Token: 0x06001A1F RID: 6687 RVA: 0x0007443C File Offset: 0x0007263C
		private object ImportDefault(TypeMapping mapping, string defaultValue)
		{
			if (defaultValue == null)
			{
				return null;
			}
			if (mapping.IsList)
			{
				string[] array = defaultValue.Trim().Split(null);
				int num = 0;
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] != null && array[i].Length > 0)
					{
						num++;
					}
				}
				object[] array2 = new object[num];
				num = 0;
				for (int j = 0; j < array.Length; j++)
				{
					if (array[j] != null && array[j].Length > 0)
					{
						array2[num++] = this.ImportDefaultValue(mapping, array[j]);
					}
				}
				return array2;
			}
			return this.ImportDefaultValue(mapping, defaultValue);
		}

		// Token: 0x06001A20 RID: 6688 RVA: 0x000744D0 File Offset: 0x000726D0
		private object ImportDefaultValue(TypeMapping mapping, string defaultValue)
		{
			if (defaultValue == null)
			{
				return null;
			}
			if (!(mapping is PrimitiveMapping))
			{
				return DBNull.Value;
			}
			if (!(mapping is EnumMapping))
			{
				PrimitiveMapping primitiveMapping = (PrimitiveMapping)mapping;
				if (!primitiveMapping.TypeDesc.HasCustomFormatter)
				{
					if (primitiveMapping.TypeDesc.FormatterName == "String")
					{
						return defaultValue;
					}
					if (primitiveMapping.TypeDesc.FormatterName == "DateTime")
					{
						return XmlCustomFormatter.ToDateTime(defaultValue);
					}
					Type typeFromHandle = typeof(XmlConvert);
					MethodInfo method = typeFromHandle.GetMethod("To" + primitiveMapping.TypeDesc.FormatterName, new Type[]
					{
						typeof(string)
					});
					if (method != null)
					{
						return method.Invoke(typeFromHandle, new object[]
						{
							defaultValue
						});
					}
				}
				else if (primitiveMapping.TypeDesc.HasDefaultSupport)
				{
					return XmlCustomFormatter.ToDefaultValue(defaultValue, primitiveMapping.TypeDesc.FormatterName);
				}
				return DBNull.Value;
			}
			EnumMapping enumMapping = (EnumMapping)mapping;
			ConstantMapping[] constants = enumMapping.Constants;
			if (enumMapping.IsFlags)
			{
				Hashtable hashtable = new Hashtable();
				string[] array = new string[constants.Length];
				long[] array2 = new long[constants.Length];
				for (int i = 0; i < constants.Length; i++)
				{
					array2[i] = (enumMapping.IsFlags ? (1L << i) : ((long)i));
					array[i] = constants[i].Name;
					hashtable.Add(constants[i].Name, array2[i]);
				}
				long val = XmlCustomFormatter.ToEnum(defaultValue, hashtable, enumMapping.TypeName, true);
				return XmlCustomFormatter.FromEnum(val, array, array2, enumMapping.TypeDesc.FullName);
			}
			for (int j = 0; j < constants.Length; j++)
			{
				if (constants[j].XmlName == defaultValue)
				{
					return constants[j].Name;
				}
			}
			throw new InvalidOperationException(Res.GetString("XmlInvalidDefaultValue", new object[]
			{
				defaultValue,
				enumMapping.TypeDesc.FullName
			}));
		}

		// Token: 0x06001A21 RID: 6689 RVA: 0x000746D0 File Offset: 0x000728D0
		private void AddDefaultValueAttribute(CodeMemberField field, CodeAttributeDeclarationCollection metadata, object defaultValue, TypeMapping mapping, CodeCommentStatementCollection comments, TypeDesc memberTypeDesc, Accessor accessor, CodeConstructor ctor)
		{
			string text = accessor.IsFixed ? "fixed" : "default";
			if (!memberTypeDesc.HasDefaultSupport)
			{
				if (comments != null && defaultValue is string)
				{
					XmlCodeExporter.DropDefaultAttribute(accessor, comments, memberTypeDesc.FullName);
					CodeExporter.AddWarningComment(comments, Res.GetString("XmlDropAttributeValue", new object[]
					{
						text,
						mapping.TypeName,
						defaultValue.ToString()
					}));
				}
				return;
			}
			if (memberTypeDesc.IsArrayLike && accessor is ElementAccessor)
			{
				if (comments != null && defaultValue is string)
				{
					XmlCodeExporter.DropDefaultAttribute(accessor, comments, memberTypeDesc.FullName);
					CodeExporter.AddWarningComment(comments, Res.GetString("XmlDropArrayAttributeValue", new object[]
					{
						text,
						defaultValue.ToString(),
						((ElementAccessor)accessor).Name
					}));
				}
				return;
			}
			if (mapping.TypeDesc.IsMappedType && field != null && defaultValue is string)
			{
				SchemaImporterExtension extension = mapping.TypeDesc.ExtendedType.Extension;
				CodeExpression codeExpression = extension.ImportDefaultValue((string)defaultValue, mapping.TypeDesc.FullName);
				if (codeExpression != null)
				{
					if (ctor != null)
					{
						XmlCodeExporter.AddInitializationStatement(ctor, field, codeExpression);
					}
					else
					{
						field.InitExpression = extension.ImportDefaultValue((string)defaultValue, mapping.TypeDesc.FullName);
					}
				}
				if (comments != null)
				{
					XmlCodeExporter.DropDefaultAttribute(accessor, comments, mapping.TypeDesc.FullName);
					if (codeExpression == null)
					{
						CodeExporter.AddWarningComment(comments, Res.GetString("XmlNotKnownDefaultValue", new object[]
						{
							extension.GetType().FullName,
							text,
							(string)defaultValue,
							mapping.TypeName,
							mapping.Namespace
						}));
					}
				}
				return;
			}
			object obj = null;
			if (defaultValue is string || defaultValue == null)
			{
				obj = this.ImportDefault(mapping, (string)defaultValue);
			}
			if (obj == null)
			{
				return;
			}
			if (!(mapping is PrimitiveMapping))
			{
				if (comments != null)
				{
					XmlCodeExporter.DropDefaultAttribute(accessor, comments, memberTypeDesc.FullName);
					CodeExporter.AddWarningComment(comments, Res.GetString("XmlDropNonPrimitiveAttributeValue", new object[]
					{
						text,
						defaultValue.ToString()
					}));
				}
				return;
			}
			PrimitiveMapping primitiveMapping = (PrimitiveMapping)mapping;
			if (comments != null && !primitiveMapping.TypeDesc.HasDefaultSupport && primitiveMapping.TypeDesc.IsMappedType)
			{
				XmlCodeExporter.DropDefaultAttribute(accessor, comments, primitiveMapping.TypeDesc.FullName);
				return;
			}
			if (obj == DBNull.Value)
			{
				if (comments != null)
				{
					CodeExporter.AddWarningComment(comments, Res.GetString("XmlDropAttributeValue", new object[]
					{
						text,
						primitiveMapping.TypeName,
						defaultValue.ToString()
					}));
				}
				return;
			}
			CodeAttributeArgument[] array = null;
			CodeExpression codeExpression2 = null;
			if (primitiveMapping.IsList)
			{
				object[] array2 = (object[])obj;
				CodeExpression[] array3 = new CodeExpression[array2.Length];
				for (int i = 0; i < array2.Length; i++)
				{
					this.GetDefaultValueArguments(primitiveMapping, array2[i], out array3[i]);
				}
				codeExpression2 = new CodeArrayCreateExpression(field.Type, array3);
			}
			else
			{
				array = this.GetDefaultValueArguments(primitiveMapping, obj, out codeExpression2);
			}
			if (field != null)
			{
				if (ctor != null)
				{
					XmlCodeExporter.AddInitializationStatement(ctor, field, codeExpression2);
				}
				else
				{
					field.InitExpression = codeExpression2;
				}
			}
			if (array != null && primitiveMapping.TypeDesc.HasDefaultSupport && accessor.IsOptional && !accessor.IsFixed)
			{
				CodeAttributeDeclaration value = new CodeAttributeDeclaration(typeof(DefaultValueAttribute).FullName, array);
				metadata.Add(value);
				return;
			}
			if (comments != null)
			{
				XmlCodeExporter.DropDefaultAttribute(accessor, comments, memberTypeDesc.FullName);
			}
		}

		// Token: 0x06001A22 RID: 6690 RVA: 0x00074A40 File Offset: 0x00072C40
		private static void AddInitializationStatement(CodeConstructor ctor, CodeMemberField field, CodeExpression init)
		{
			CodeAssignStatement codeAssignStatement = new CodeAssignStatement();
			codeAssignStatement.Left = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), field.Name);
			codeAssignStatement.Right = init;
			ctor.Statements.Add(codeAssignStatement);
		}

		// Token: 0x06001A23 RID: 6691 RVA: 0x00074A7D File Offset: 0x00072C7D
		private static void DropDefaultAttribute(Accessor accessor, CodeCommentStatementCollection comments, string type)
		{
			if (!accessor.IsFixed && accessor.IsOptional)
			{
				CodeExporter.AddWarningComment(comments, Res.GetString("XmlDropDefaultAttribute", new object[]
				{
					type
				}));
			}
		}

		// Token: 0x06001A24 RID: 6692 RVA: 0x00074AAC File Offset: 0x00072CAC
		private CodeTypeDeclaration ExportStruct(StructMapping mapping)
		{
			if (mapping.TypeDesc.IsRoot)
			{
				base.ExportRoot(mapping, typeof(XmlIncludeAttribute));
				return null;
			}
			string name = mapping.TypeDesc.Name;
			string text = (mapping.TypeDesc.BaseTypeDesc == null || mapping.TypeDesc.BaseTypeDesc.IsRoot) ? string.Empty : mapping.TypeDesc.BaseTypeDesc.FullName;
			CodeTypeDeclaration codeTypeDeclaration = new CodeTypeDeclaration(name);
			codeTypeDeclaration.IsPartial = base.CodeProvider.Supports(GeneratorSupport.PartialTypes);
			codeTypeDeclaration.Comments.Add(new CodeCommentStatement(Res.GetString("XmlRemarks"), true));
			base.CodeNamespace.Types.Add(codeTypeDeclaration);
			CodeConstructor codeConstructor = new CodeConstructor();
			codeConstructor.Attributes = ((codeConstructor.Attributes & (MemberAttributes)(-61441)) | MemberAttributes.Public);
			codeTypeDeclaration.Members.Add(codeConstructor);
			if (mapping.TypeDesc.IsAbstract)
			{
				codeConstructor.Attributes |= MemberAttributes.Abstract;
			}
			if (text != null && text.Length > 0)
			{
				codeTypeDeclaration.BaseTypes.Add(text);
			}
			else
			{
				base.AddPropertyChangedNotifier(codeTypeDeclaration);
			}
			codeTypeDeclaration.TypeAttributes |= TypeAttributes.Public;
			if (mapping.TypeDesc.IsAbstract)
			{
				codeTypeDeclaration.TypeAttributes |= TypeAttributes.Abstract;
			}
			CodeExporter.AddIncludeMetadata(codeTypeDeclaration.CustomAttributes, mapping, typeof(XmlIncludeAttribute));
			if (mapping.IsSequence)
			{
				int num = 0;
				for (int i = 0; i < mapping.Members.Length; i++)
				{
					MemberMapping memberMapping = mapping.Members[i];
					if (memberMapping.IsParticle && memberMapping.SequenceId < 0)
					{
						memberMapping.SequenceId = num++;
					}
				}
			}
			if (base.GenerateProperties)
			{
				for (int j = 0; j < mapping.Members.Length; j++)
				{
					this.ExportProperty(codeTypeDeclaration, mapping.Members[j], mapping.Namespace, mapping.Scope, codeConstructor);
				}
			}
			else
			{
				for (int k = 0; k < mapping.Members.Length; k++)
				{
					this.ExportMember(codeTypeDeclaration, mapping.Members[k], mapping.Namespace, codeConstructor);
				}
			}
			for (int l = 0; l < mapping.Members.Length; l++)
			{
				if (mapping.Members[l].Xmlns == null)
				{
					Accessor[] elements = mapping.Members[l].Elements;
					this.EnsureTypesExported(elements, mapping.Namespace);
					this.EnsureTypesExported(mapping.Members[l].Attribute, mapping.Namespace);
					this.EnsureTypesExported(mapping.Members[l].Text, mapping.Namespace);
				}
			}
			if (mapping.BaseMapping != null)
			{
				this.ExportType(mapping.BaseMapping, null, mapping.Namespace, null, false);
			}
			this.ExportDerivedStructs(mapping);
			CodeGenerator.ValidateIdentifiers(codeTypeDeclaration);
			if (codeConstructor.Statements.Count == 0)
			{
				codeTypeDeclaration.Members.Remove(codeConstructor);
			}
			return codeTypeDeclaration;
		}

		// Token: 0x06001A25 RID: 6693 RVA: 0x00074D88 File Offset: 0x00072F88
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		internal override void ExportDerivedStructs(StructMapping mapping)
		{
			for (StructMapping structMapping = mapping.DerivedMappings; structMapping != null; structMapping = structMapping.NextDerivedMapping)
			{
				this.ExportType(structMapping, mapping.Namespace);
			}
		}

		// Token: 0x06001A26 RID: 6694 RVA: 0x00074DB8 File Offset: 0x00072FB8
		public void AddMappingMetadata(CodeAttributeDeclarationCollection metadata, XmlTypeMapping mapping, string ns)
		{
			mapping.CheckShallow();
			base.CheckScope(mapping.Scope);
			if (mapping.Mapping is StructMapping || mapping.Mapping is EnumMapping)
			{
				return;
			}
			this.AddRootMetadata(metadata, mapping.Mapping, Accessor.UnescapeName(mapping.Accessor.Name), mapping.Accessor.Namespace, mapping.Accessor);
		}

		// Token: 0x06001A27 RID: 6695 RVA: 0x00074E20 File Offset: 0x00073020
		public void AddMappingMetadata(CodeAttributeDeclarationCollection metadata, XmlMemberMapping member, string ns, bool forceUseMemberName)
		{
			this.AddMemberMetadata(null, metadata, member.Mapping, ns, forceUseMemberName, null, null);
		}

		// Token: 0x06001A28 RID: 6696 RVA: 0x00074E35 File Offset: 0x00073035
		public void AddMappingMetadata(CodeAttributeDeclarationCollection metadata, XmlMemberMapping member, string ns)
		{
			this.AddMemberMetadata(null, metadata, member.Mapping, ns, false, null, null);
		}

		// Token: 0x06001A29 RID: 6697 RVA: 0x00074E4C File Offset: 0x0007304C
		private void ExportArrayElements(CodeAttributeDeclarationCollection metadata, ArrayMapping array, string ns, TypeDesc elementTypeDesc, int nestingLevel)
		{
			for (int i = 0; i < array.Elements.Length; i++)
			{
				ElementAccessor elementAccessor = array.Elements[i];
				TypeMapping mapping = elementAccessor.Mapping;
				string text = Accessor.UnescapeName(elementAccessor.Name);
				bool flag = !elementAccessor.Mapping.TypeDesc.IsArray && text == elementAccessor.Mapping.TypeName;
				bool flag2 = mapping.TypeDesc == elementTypeDesc;
				bool flag3 = elementAccessor.Form == XmlSchemaForm.Unqualified || elementAccessor.Namespace == ns;
				bool flag4 = elementAccessor.IsNullable == mapping.TypeDesc.IsNullable;
				bool flag5 = elementAccessor.Form != XmlSchemaForm.Unqualified;
				if (!flag || !flag2 || !flag3 || !flag4 || !flag5 || nestingLevel > 0)
				{
					this.ExportArrayItem(metadata, flag ? null : text, flag3 ? null : elementAccessor.Namespace, flag2 ? null : mapping.TypeDesc, mapping.TypeDesc, elementAccessor.IsNullable, flag5 ? XmlSchemaForm.None : elementAccessor.Form, nestingLevel);
				}
				if (mapping is ArrayMapping)
				{
					this.ExportArrayElements(metadata, (ArrayMapping)mapping, ns, elementTypeDesc.ArrayElementTypeDesc, nestingLevel + 1);
				}
			}
		}

		// Token: 0x06001A2A RID: 6698 RVA: 0x00074F80 File Offset: 0x00073180
		private void AddMemberMetadata(CodeMemberField field, CodeAttributeDeclarationCollection metadata, MemberMapping member, string ns, bool forceUseMemberName, CodeCommentStatementCollection comments, CodeConstructor ctor)
		{
			if (member.Xmlns != null)
			{
				CodeAttributeDeclaration value = new CodeAttributeDeclaration(typeof(XmlNamespaceDeclarationsAttribute).FullName);
				metadata.Add(value);
				return;
			}
			if (member.Attribute == null)
			{
				if (member.Text != null)
				{
					TypeMapping mapping = member.Text.Mapping;
					this.ExportText(metadata, (mapping.TypeDesc == member.TypeDesc || (member.TypeDesc.IsArrayLike && mapping.TypeDesc == member.TypeDesc.ArrayElementTypeDesc)) ? null : mapping.TypeDesc, mapping.TypeDesc.IsAmbiguousDataType ? mapping.TypeDesc.DataType.Name : null);
				}
				if (member.Elements.Length == 1)
				{
					ElementAccessor elementAccessor = member.Elements[0];
					TypeMapping mapping2 = elementAccessor.Mapping;
					string text = Accessor.UnescapeName(elementAccessor.Name);
					bool flag = text == member.Name && !forceUseMemberName;
					bool flag2 = mapping2 is ArrayMapping;
					bool flag3 = elementAccessor.Namespace == ns;
					bool flag4 = elementAccessor.Form != XmlSchemaForm.Unqualified;
					if (elementAccessor.Any)
					{
						this.ExportAnyElement(metadata, text, elementAccessor.Namespace, member.SequenceId);
					}
					else if (flag2)
					{
						bool flag5 = mapping2.TypeDesc == member.TypeDesc;
						ArrayMapping array = (ArrayMapping)mapping2;
						if (!flag || !flag3 || elementAccessor.IsNullable || !flag4 || member.SequenceId != -1)
						{
							this.ExportArray(metadata, flag ? null : text, flag3 ? null : elementAccessor.Namespace, elementAccessor.IsNullable, flag4 ? XmlSchemaForm.None : elementAccessor.Form, member.SequenceId);
						}
						else if (mapping2.TypeDesc.ArrayElementTypeDesc == new TypeScope().GetTypeDesc(typeof(byte)))
						{
							this.ExportArray(metadata, null, null, false, XmlSchemaForm.None, member.SequenceId);
						}
						this.ExportArrayElements(metadata, array, elementAccessor.Namespace, member.TypeDesc.ArrayElementTypeDesc, 0);
					}
					else
					{
						bool flag6 = mapping2.TypeDesc == member.TypeDesc || (member.TypeDesc.IsArrayLike && mapping2.TypeDesc == member.TypeDesc.ArrayElementTypeDesc);
						if (member.TypeDesc.IsArrayLike)
						{
							flag = false;
						}
						this.ExportElement(metadata, flag ? null : text, flag3 ? null : elementAccessor.Namespace, flag6 ? null : mapping2.TypeDesc, mapping2.TypeDesc, elementAccessor.IsNullable, flag4 ? XmlSchemaForm.None : elementAccessor.Form, member.SequenceId);
					}
					this.AddDefaultValueAttribute(field, metadata, elementAccessor.Default, mapping2, comments, member.TypeDesc, elementAccessor, ctor);
				}
				else
				{
					for (int i = 0; i < member.Elements.Length; i++)
					{
						ElementAccessor elementAccessor2 = member.Elements[i];
						string name = Accessor.UnescapeName(elementAccessor2.Name);
						bool flag7 = elementAccessor2.Namespace == ns;
						if (elementAccessor2.Any)
						{
							this.ExportAnyElement(metadata, name, elementAccessor2.Namespace, member.SequenceId);
						}
						else
						{
							bool flag8 = elementAccessor2.Form != XmlSchemaForm.Unqualified;
							this.ExportElement(metadata, name, flag7 ? null : elementAccessor2.Namespace, elementAccessor2.Mapping.TypeDesc, elementAccessor2.Mapping.TypeDesc, elementAccessor2.IsNullable, flag8 ? XmlSchemaForm.None : elementAccessor2.Form, member.SequenceId);
						}
					}
				}
				if (member.ChoiceIdentifier != null)
				{
					metadata.Add(new CodeAttributeDeclaration(typeof(XmlChoiceIdentifierAttribute).FullName)
					{
						Arguments = 
						{
							new CodeAttributeArgument(new CodePrimitiveExpression(member.ChoiceIdentifier.MemberName))
						}
					});
				}
				if (member.Ignore)
				{
					CodeAttributeDeclaration value2 = new CodeAttributeDeclaration(typeof(XmlIgnoreAttribute).FullName);
					metadata.Add(value2);
				}
				return;
			}
			AttributeAccessor attribute = member.Attribute;
			if (attribute.Any)
			{
				this.ExportAnyAttribute(metadata);
				return;
			}
			TypeMapping mapping3 = attribute.Mapping;
			string text2 = Accessor.UnescapeName(attribute.Name);
			bool flag9 = mapping3.TypeDesc == member.TypeDesc || (member.TypeDesc.IsArrayLike && mapping3.TypeDesc == member.TypeDesc.ArrayElementTypeDesc);
			bool flag10 = text2 == member.Name && !forceUseMemberName;
			bool flag11 = attribute.Namespace == ns;
			bool flag12 = attribute.Form != XmlSchemaForm.Qualified;
			this.ExportAttribute(metadata, flag10 ? null : text2, (flag11 || flag12) ? null : attribute.Namespace, flag9 ? null : mapping3.TypeDesc, mapping3.TypeDesc, flag12 ? XmlSchemaForm.None : attribute.Form);
			this.AddDefaultValueAttribute(field, metadata, attribute.Default, mapping3, comments, member.TypeDesc, attribute, ctor);
		}

		// Token: 0x06001A2B RID: 6699 RVA: 0x00075480 File Offset: 0x00073680
		private void ExportMember(CodeTypeDeclaration codeClass, MemberMapping member, string ns, CodeConstructor ctor)
		{
			string typeName = member.GetTypeName(base.CodeProvider);
			CodeMemberField codeMemberField = new CodeMemberField(typeName, member.Name);
			codeMemberField.Attributes = ((codeMemberField.Attributes & (MemberAttributes)(-61441)) | MemberAttributes.Public);
			codeMemberField.Comments.Add(new CodeCommentStatement(Res.GetString("XmlRemarks"), true));
			codeClass.Members.Add(codeMemberField);
			this.AddMemberMetadata(codeMemberField, codeMemberField.CustomAttributes, member, ns, false, codeMemberField.Comments, ctor);
			if (member.CheckSpecified != SpecifiedAccessor.None)
			{
				codeMemberField = new CodeMemberField(typeof(bool).FullName, member.Name + "Specified");
				codeMemberField.Attributes = ((codeMemberField.Attributes & (MemberAttributes)(-61441)) | MemberAttributes.Public);
				codeMemberField.Comments.Add(new CodeCommentStatement(Res.GetString("XmlRemarks"), true));
				CodeAttributeDeclaration value = new CodeAttributeDeclaration(typeof(XmlIgnoreAttribute).FullName);
				codeMemberField.CustomAttributes.Add(value);
				codeClass.Members.Add(codeMemberField);
			}
		}

		// Token: 0x06001A2C RID: 6700 RVA: 0x00075594 File Offset: 0x00073794
		private void ExportProperty(CodeTypeDeclaration codeClass, MemberMapping member, string ns, CodeIdentifiers memberScope, CodeConstructor ctor)
		{
			string text = memberScope.AddUnique(CodeExporter.MakeFieldName(member.Name), member);
			string typeName = member.GetTypeName(base.CodeProvider);
			CodeMemberField codeMemberField = new CodeMemberField(typeName, text);
			codeMemberField.Attributes = MemberAttributes.Private;
			codeClass.Members.Add(codeMemberField);
			CodeMemberProperty codeMemberProperty = base.CreatePropertyDeclaration(codeMemberField, member.Name, typeName);
			codeMemberProperty.Comments.Add(new CodeCommentStatement(Res.GetString("XmlRemarks"), true));
			this.AddMemberMetadata(codeMemberField, codeMemberProperty.CustomAttributes, member, ns, false, codeMemberProperty.Comments, ctor);
			codeClass.Members.Add(codeMemberProperty);
			if (member.CheckSpecified != SpecifiedAccessor.None)
			{
				codeMemberField = new CodeMemberField(typeof(bool).FullName, text + "Specified");
				codeMemberField.Attributes = MemberAttributes.Private;
				codeClass.Members.Add(codeMemberField);
				codeMemberProperty = base.CreatePropertyDeclaration(codeMemberField, member.Name + "Specified", typeof(bool).FullName);
				codeMemberProperty.Comments.Add(new CodeCommentStatement(Res.GetString("XmlRemarks"), true));
				CodeAttributeDeclaration value = new CodeAttributeDeclaration(typeof(XmlIgnoreAttribute).FullName);
				codeMemberProperty.CustomAttributes.Add(value);
				codeClass.Members.Add(codeMemberProperty);
			}
		}

		// Token: 0x06001A2D RID: 6701 RVA: 0x000756EC File Offset: 0x000738EC
		private void ExportText(CodeAttributeDeclarationCollection metadata, TypeDesc typeDesc, string dataType)
		{
			CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(typeof(XmlTextAttribute).FullName);
			if (typeDesc != null)
			{
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodeTypeOfExpression(typeDesc.FullName)));
			}
			if (dataType != null)
			{
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("DataType", new CodePrimitiveExpression(dataType)));
			}
			metadata.Add(codeAttributeDeclaration);
		}

		// Token: 0x06001A2E RID: 6702 RVA: 0x00075754 File Offset: 0x00073954
		private void ExportAttribute(CodeAttributeDeclarationCollection metadata, string name, string ns, TypeDesc typeDesc, TypeDesc dataTypeDesc, XmlSchemaForm form)
		{
			this.ExportMetadata(metadata, typeof(XmlAttributeAttribute), name, ns, typeDesc, dataTypeDesc, null, form, 0, -1);
		}

		// Token: 0x06001A2F RID: 6703 RVA: 0x00075780 File Offset: 0x00073980
		private void ExportArrayItem(CodeAttributeDeclarationCollection metadata, string name, string ns, TypeDesc typeDesc, TypeDesc dataTypeDesc, bool isNullable, XmlSchemaForm form, int nestingLevel)
		{
			this.ExportMetadata(metadata, typeof(XmlArrayItemAttribute), name, ns, typeDesc, dataTypeDesc, isNullable ? null : false, form, nestingLevel, -1);
		}

		// Token: 0x06001A30 RID: 6704 RVA: 0x000757B8 File Offset: 0x000739B8
		private void ExportElement(CodeAttributeDeclarationCollection metadata, string name, string ns, TypeDesc typeDesc, TypeDesc dataTypeDesc, bool isNullable, XmlSchemaForm form, int sequenceId)
		{
			this.ExportMetadata(metadata, typeof(XmlElementAttribute), name, ns, typeDesc, dataTypeDesc, isNullable ? true : null, form, 0, sequenceId);
		}

		// Token: 0x06001A31 RID: 6705 RVA: 0x000757F0 File Offset: 0x000739F0
		private void ExportArray(CodeAttributeDeclarationCollection metadata, string name, string ns, bool isNullable, XmlSchemaForm form, int sequenceId)
		{
			this.ExportMetadata(metadata, typeof(XmlArrayAttribute), name, ns, null, null, isNullable ? true : null, form, 0, sequenceId);
		}

		// Token: 0x06001A32 RID: 6706 RVA: 0x00075824 File Offset: 0x00073A24
		private void ExportMetadata(CodeAttributeDeclarationCollection metadata, Type attributeType, string name, string ns, TypeDesc typeDesc, TypeDesc dataTypeDesc, object isNullable, XmlSchemaForm form, int nestingLevel, int sequenceId)
		{
			CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(attributeType.FullName);
			if (name != null)
			{
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(name)));
			}
			if (typeDesc != null)
			{
				if (isNullable != null && (bool)isNullable && typeDesc.IsValueType && !typeDesc.IsMappedType && base.CodeProvider.Supports(GeneratorSupport.GenericTypeReference))
				{
					codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodeTypeOfExpression("System.Nullable`1[" + typeDesc.FullName + "]")));
					isNullable = null;
				}
				else
				{
					codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodeTypeOfExpression(typeDesc.FullName)));
				}
			}
			if (form != XmlSchemaForm.None)
			{
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("Form", new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(XmlSchemaForm).FullName), Enum.Format(typeof(XmlSchemaForm), form, "G"))));
				if (form == XmlSchemaForm.Unqualified && ns != null && ns.Length == 0)
				{
					ns = null;
				}
			}
			if (ns != null)
			{
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("Namespace", new CodePrimitiveExpression(ns)));
			}
			if (dataTypeDesc != null && dataTypeDesc.IsAmbiguousDataType && !dataTypeDesc.IsMappedType)
			{
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("DataType", new CodePrimitiveExpression(dataTypeDesc.DataType.Name)));
			}
			if (isNullable != null)
			{
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("IsNullable", new CodePrimitiveExpression((bool)isNullable)));
			}
			if (nestingLevel > 0)
			{
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("NestingLevel", new CodePrimitiveExpression(nestingLevel)));
			}
			if (sequenceId >= 0)
			{
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("Order", new CodePrimitiveExpression(sequenceId)));
			}
			if (codeAttributeDeclaration.Arguments.Count == 0 && attributeType == typeof(XmlElementAttribute))
			{
				return;
			}
			metadata.Add(codeAttributeDeclaration);
		}

		// Token: 0x06001A33 RID: 6707 RVA: 0x00075A3C File Offset: 0x00073C3C
		private void ExportAnyElement(CodeAttributeDeclarationCollection metadata, string name, string ns, int sequenceId)
		{
			CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(typeof(XmlAnyElementAttribute).FullName);
			if (name != null && name.Length > 0)
			{
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("Name", new CodePrimitiveExpression(name)));
			}
			if (ns != null)
			{
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("Namespace", new CodePrimitiveExpression(ns)));
			}
			if (sequenceId >= 0)
			{
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("Order", new CodePrimitiveExpression(sequenceId)));
			}
			metadata.Add(codeAttributeDeclaration);
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x00075AD4 File Offset: 0x00073CD4
		private void ExportAnyAttribute(CodeAttributeDeclarationCollection metadata)
		{
			metadata.Add(new CodeAttributeDeclaration(typeof(XmlAnyAttributeAttribute).FullName));
		}

		// Token: 0x06001A35 RID: 6709 RVA: 0x00075AF4 File Offset: 0x00073CF4
		internal override void EnsureTypesExported(Accessor[] accessors, string ns)
		{
			if (accessors == null)
			{
				return;
			}
			for (int i = 0; i < accessors.Length; i++)
			{
				this.EnsureTypesExported(accessors[i], ns);
			}
		}

		// Token: 0x06001A36 RID: 6710 RVA: 0x00075B1D File Offset: 0x00073D1D
		private void EnsureTypesExported(Accessor accessor, string ns)
		{
			if (accessor == null)
			{
				return;
			}
			this.ExportType(accessor.Mapping, null, ns, null, false);
		}
	}
}
