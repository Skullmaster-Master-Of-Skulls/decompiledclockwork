using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Linq;
using System.Xml;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x0200002D RID: 45
	internal class MslXmlSchemaWriter : XmlSchemaWriter
	{
		// Token: 0x060001D4 RID: 468 RVA: 0x0000A717 File Offset: 0x00008917
		internal MslXmlSchemaWriter(XmlWriter xmlWriter, double version)
		{
			this._xmlWriter = xmlWriter;
			this._version = version;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000A72D File Offset: 0x0000892D
		internal void WriteSchema(DbDatabaseMapping databaseMapping)
		{
			this.WriteSchemaElementHeader();
			this.WriteDbModelElement(databaseMapping);
			this.WriteEndElement();
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000A744 File Offset: 0x00008944
		private void WriteSchemaElementHeader()
		{
			string mslNamespace = MslConstructs.GetMslNamespace(this._version);
			this._xmlWriter.WriteStartElement("Mapping", mslNamespace);
			this._xmlWriter.WriteAttributeString("Space", "C-S");
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000A784 File Offset: 0x00008984
		private void WriteDbModelElement(DbDatabaseMapping databaseMapping)
		{
			this._entityTypeNamespace = databaseMapping.Model.NamespaceNames.SingleOrDefault<string>();
			this._dbSchemaName = databaseMapping.Database.Containers.Single<EntityContainer>().Name;
			this.WriteEntityContainerMappingElement(databaseMapping.EntityContainerMappings.First<EntityContainerMapping>());
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000A7D4 File Offset: 0x000089D4
		internal void WriteEntityContainerMappingElement(EntityContainerMapping containerMapping)
		{
			this._xmlWriter.WriteStartElement("EntityContainerMapping");
			this._xmlWriter.WriteAttributeString("StorageEntityContainer", this._dbSchemaName);
			this._xmlWriter.WriteAttributeString("CdmEntityContainer", containerMapping.EdmEntityContainer.Name);
			foreach (EntitySetMapping entitySetMapping in containerMapping.EntitySetMappings)
			{
				this.WriteEntitySetMappingElement(entitySetMapping);
			}
			foreach (AssociationSetMapping associationSetMapping in containerMapping.AssociationSetMappings)
			{
				this.WriteAssociationSetMappingElement(associationSetMapping);
			}
			foreach (FunctionImportMappingComposable functionImportMapping in containerMapping.FunctionImportMappings.OfType<FunctionImportMappingComposable>())
			{
				this.WriteFunctionImportMappingElement(functionImportMapping);
			}
			foreach (FunctionImportMappingNonComposable functionImportMapping2 in containerMapping.FunctionImportMappings.OfType<FunctionImportMappingNonComposable>())
			{
				this.WriteFunctionImportMappingElement(functionImportMapping2);
			}
			this._xmlWriter.WriteEndElement();
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000A940 File Offset: 0x00008B40
		public void WriteEntitySetMappingElement(EntitySetMapping entitySetMapping)
		{
			this._xmlWriter.WriteStartElement("EntitySetMapping");
			this._xmlWriter.WriteAttributeString("Name", entitySetMapping.EntitySet.Name);
			foreach (EntityTypeMapping entityTypeMapping in entitySetMapping.EntityTypeMappings)
			{
				this.WriteEntityTypeMappingElement(entityTypeMapping);
			}
			foreach (EntityTypeModificationFunctionMapping entityTypeModificationFunctionMapping in entitySetMapping.ModificationFunctionMappings)
			{
				this._xmlWriter.WriteStartElement("EntityTypeMapping");
				this._xmlWriter.WriteAttributeString("TypeName", MslXmlSchemaWriter.GetEntityTypeName(this._entityTypeNamespace + "." + entityTypeModificationFunctionMapping.EntityType.Name, false));
				this.WriteModificationFunctionMapping(entityTypeModificationFunctionMapping);
				this._xmlWriter.WriteEndElement();
			}
			this._xmlWriter.WriteEndElement();
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000AA4C File Offset: 0x00008C4C
		public void WriteAssociationSetMappingElement(AssociationSetMapping associationSetMapping)
		{
			this._xmlWriter.WriteStartElement("AssociationSetMapping");
			this._xmlWriter.WriteAttributeString("Name", associationSetMapping.AssociationSet.Name);
			this._xmlWriter.WriteAttributeString("TypeName", this._entityTypeNamespace + "." + associationSetMapping.AssociationSet.ElementType.Name);
			this._xmlWriter.WriteAttributeString("StoreEntitySet", associationSetMapping.Table.Name);
			this.WriteAssociationEndMappingElement(associationSetMapping.SourceEndMapping);
			this.WriteAssociationEndMappingElement(associationSetMapping.TargetEndMapping);
			if (associationSetMapping.ModificationFunctionMapping != null)
			{
				this.WriteModificationFunctionMapping(associationSetMapping.ModificationFunctionMapping);
			}
			foreach (ConditionPropertyMapping condition in associationSetMapping.Conditions)
			{
				this.WriteConditionElement(condition);
			}
			this._xmlWriter.WriteEndElement();
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000AB48 File Offset: 0x00008D48
		private void WriteAssociationEndMappingElement(EndPropertyMapping endMapping)
		{
			this._xmlWriter.WriteStartElement("EndProperty");
			this._xmlWriter.WriteAttributeString("Name", endMapping.AssociationEnd.Name);
			foreach (ScalarPropertyMapping scalarPropertyMapping in endMapping.PropertyMappings)
			{
				this.WriteScalarPropertyElement(scalarPropertyMapping.Property.Name, scalarPropertyMapping.Column.Name);
			}
			this._xmlWriter.WriteEndElement();
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000ABE0 File Offset: 0x00008DE0
		private void WriteEntityTypeMappingElement(EntityTypeMapping entityTypeMapping)
		{
			this._xmlWriter.WriteStartElement("EntityTypeMapping");
			this._xmlWriter.WriteAttributeString("TypeName", MslXmlSchemaWriter.GetEntityTypeName(this._entityTypeNamespace + "." + entityTypeMapping.EntityType.Name, entityTypeMapping.IsHierarchyMapping));
			foreach (MappingFragment mappingFragment in entityTypeMapping.MappingFragments)
			{
				this.WriteMappingFragmentElement(mappingFragment);
			}
			this._xmlWriter.WriteEndElement();
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000AC80 File Offset: 0x00008E80
		internal void WriteMappingFragmentElement(MappingFragment mappingFragment)
		{
			this._xmlWriter.WriteStartElement("MappingFragment");
			this._xmlWriter.WriteAttributeString("StoreEntitySet", mappingFragment.TableSet.Name);
			foreach (PropertyMapping propertyMapping in mappingFragment.PropertyMappings)
			{
				this.WritePropertyMapping(propertyMapping);
			}
			foreach (ConditionPropertyMapping condition in mappingFragment.ColumnConditions)
			{
				this.WriteConditionElement(condition);
			}
			this._xmlWriter.WriteEndElement();
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000AD40 File Offset: 0x00008F40
		public void WriteFunctionImportMappingElement(FunctionImportMappingComposable functionImportMapping)
		{
			this.WriteFunctionImportMappingStartElement(functionImportMapping);
			if (functionImportMapping.StructuralTypeMappings != null)
			{
				this._xmlWriter.WriteStartElement("ResultMapping");
				Tuple<StructuralType, List<ConditionPropertyMapping>, List<PropertyMapping>> tuple = functionImportMapping.StructuralTypeMappings.Single<Tuple<StructuralType, List<ConditionPropertyMapping>, List<PropertyMapping>>>();
				if (tuple.Item1.BuiltInTypeKind == BuiltInTypeKind.ComplexType)
				{
					this._xmlWriter.WriteStartElement("ComplexTypeMapping");
					this._xmlWriter.WriteAttributeString("TypeName", tuple.Item1.FullName);
				}
				else
				{
					this._xmlWriter.WriteStartElement("EntityTypeMapping");
					this._xmlWriter.WriteAttributeString("TypeName", tuple.Item1.FullName);
					foreach (ConditionPropertyMapping condition in tuple.Item2)
					{
						this.WriteConditionElement(condition);
					}
				}
				foreach (PropertyMapping propertyMapping in tuple.Item3)
				{
					this.WritePropertyMapping(propertyMapping);
				}
				this._xmlWriter.WriteEndElement();
				this._xmlWriter.WriteEndElement();
			}
			this.WriteFunctionImportEndElement();
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000AE88 File Offset: 0x00009088
		public void WriteFunctionImportMappingElement(FunctionImportMappingNonComposable functionImportMapping)
		{
			this.WriteFunctionImportMappingStartElement(functionImportMapping);
			foreach (FunctionImportResultMapping resultMapping in functionImportMapping.ResultMappings)
			{
				this.WriteFunctionImportResultMappingElement(resultMapping);
			}
			this.WriteFunctionImportEndElement();
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000AEE4 File Offset: 0x000090E4
		private void WriteFunctionImportMappingStartElement(FunctionImportMapping functionImportMapping)
		{
			this._xmlWriter.WriteStartElement("FunctionImportMapping");
			this._xmlWriter.WriteAttributeString("FunctionName", functionImportMapping.TargetFunction.FullName);
			this._xmlWriter.WriteAttributeString("FunctionImportName", functionImportMapping.FunctionImport.Name);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000AF38 File Offset: 0x00009138
		private void WriteFunctionImportResultMappingElement(FunctionImportResultMapping resultMapping)
		{
			this._xmlWriter.WriteStartElement("ResultMapping");
			foreach (FunctionImportStructuralTypeMapping functionImportStructuralTypeMapping in resultMapping.TypeMappings)
			{
				FunctionImportEntityTypeMapping functionImportEntityTypeMapping = functionImportStructuralTypeMapping as FunctionImportEntityTypeMapping;
				if (functionImportEntityTypeMapping != null)
				{
					this.WriteFunctionImportEntityTypeMappingElement(functionImportEntityTypeMapping);
				}
				else
				{
					this.WriteFunctionImportComplexTypeMappingElement((FunctionImportComplexTypeMapping)functionImportStructuralTypeMapping);
				}
			}
			this._xmlWriter.WriteEndElement();
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000AFB8 File Offset: 0x000091B8
		private void WriteFunctionImportEntityTypeMappingElement(FunctionImportEntityTypeMapping entityTypeMapping)
		{
			this._xmlWriter.WriteStartElement("EntityTypeMapping");
			string value = MslXmlSchemaWriter.CreateFunctionImportEntityTypeMappingTypeName(entityTypeMapping);
			this._xmlWriter.WriteAttributeString("TypeName", value);
			this.WriteFunctionImportPropertyMappingElements(entityTypeMapping.PropertyMappings.Cast<FunctionImportReturnTypeScalarPropertyMapping>());
			foreach (FunctionImportEntityTypeMappingCondition condition in entityTypeMapping.Conditions)
			{
				this.WriteFunctionImportConditionElement(condition);
			}
			this._xmlWriter.WriteEndElement();
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000B068 File Offset: 0x00009268
		internal static string CreateFunctionImportEntityTypeMappingTypeName(FunctionImportEntityTypeMapping entityTypeMapping)
		{
			return string.Join(";", (from e in entityTypeMapping.EntityTypes
			select MslXmlSchemaWriter.GetEntityTypeName(e.FullName, false)).Concat(from e in entityTypeMapping.IsOfTypeEntityTypes
			select MslXmlSchemaWriter.GetEntityTypeName(e.FullName, true)));
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000B0D8 File Offset: 0x000092D8
		private void WriteFunctionImportComplexTypeMappingElement(FunctionImportComplexTypeMapping complexTypeMapping)
		{
			this._xmlWriter.WriteStartElement("ComplexTypeMapping");
			this._xmlWriter.WriteAttributeString("TypeName", complexTypeMapping.ReturnType.FullName);
			this.WriteFunctionImportPropertyMappingElements(complexTypeMapping.PropertyMappings.Cast<FunctionImportReturnTypeScalarPropertyMapping>());
			this._xmlWriter.WriteEndElement();
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000B12C File Offset: 0x0000932C
		private void WriteFunctionImportPropertyMappingElements(IEnumerable<FunctionImportReturnTypeScalarPropertyMapping> propertyMappings)
		{
			foreach (FunctionImportReturnTypeScalarPropertyMapping functionImportReturnTypeScalarPropertyMapping in propertyMappings)
			{
				this.WriteScalarPropertyElement(functionImportReturnTypeScalarPropertyMapping.PropertyName, functionImportReturnTypeScalarPropertyMapping.ColumnName);
			}
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000B180 File Offset: 0x00009380
		private void WriteFunctionImportConditionElement(FunctionImportEntityTypeMappingCondition condition)
		{
			this._xmlWriter.WriteStartElement("Condition");
			this._xmlWriter.WriteAttributeString("ColumnName", condition.ColumnName);
			FunctionImportEntityTypeMappingConditionIsNull functionImportEntityTypeMappingConditionIsNull = condition as FunctionImportEntityTypeMappingConditionIsNull;
			if (functionImportEntityTypeMappingConditionIsNull != null)
			{
				this.WriteIsNullConditionAttribute(functionImportEntityTypeMappingConditionIsNull.IsNull);
			}
			else
			{
				this.WriteConditionValue(((FunctionImportEntityTypeMappingConditionValue)condition).Value);
			}
			this._xmlWriter.WriteEndElement();
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000B1E7 File Offset: 0x000093E7
		private void WriteFunctionImportEndElement()
		{
			this._xmlWriter.WriteEndElement();
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000B1F4 File Offset: 0x000093F4
		private void WriteModificationFunctionMapping(EntityTypeModificationFunctionMapping modificationFunctionMapping)
		{
			this._xmlWriter.WriteStartElement("ModificationFunctionMapping");
			this.WriteFunctionMapping("InsertFunction", modificationFunctionMapping.InsertFunctionMapping, false);
			this.WriteFunctionMapping("UpdateFunction", modificationFunctionMapping.UpdateFunctionMapping, false);
			this.WriteFunctionMapping("DeleteFunction", modificationFunctionMapping.DeleteFunctionMapping, false);
			this._xmlWriter.WriteEndElement();
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000B254 File Offset: 0x00009454
		private void WriteModificationFunctionMapping(AssociationSetModificationFunctionMapping modificationFunctionMapping)
		{
			this._xmlWriter.WriteStartElement("ModificationFunctionMapping");
			this.WriteFunctionMapping("InsertFunction", modificationFunctionMapping.InsertFunctionMapping, true);
			this.WriteFunctionMapping("DeleteFunction", modificationFunctionMapping.DeleteFunctionMapping, true);
			this._xmlWriter.WriteEndElement();
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000B2A0 File Offset: 0x000094A0
		public void WriteFunctionMapping(string functionElement, ModificationFunctionMapping functionMapping, bool associationSetMapping = false)
		{
			this._xmlWriter.WriteStartElement(functionElement);
			this._xmlWriter.WriteAttributeString("FunctionName", functionMapping.Function.FullName);
			if (functionMapping.RowsAffectedParameter != null)
			{
				this._xmlWriter.WriteAttributeString("RowsAffectedParameter", functionMapping.RowsAffectedParameter.Name);
			}
			if (!associationSetMapping)
			{
				this.WritePropertyParameterBindings(functionMapping.ParameterBindings, 0);
				this.WriteAssociationParameterBindings(functionMapping.ParameterBindings);
				if (functionMapping.ResultBindings != null)
				{
					this.WriteResultBindings(functionMapping.ResultBindings);
				}
			}
			else
			{
				this.WriteAssociationSetMappingParameterBindings(functionMapping.ParameterBindings);
			}
			this._xmlWriter.WriteEndElement();
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000B360 File Offset: 0x00009560
		private void WriteAssociationSetMappingParameterBindings(IEnumerable<ModificationFunctionParameterBinding> parameterBindings)
		{
			IEnumerable<IGrouping<AssociationSetEnd, ModificationFunctionParameterBinding>> enumerable = from pm in parameterBindings
			where pm.MemberPath.AssociationSetEnd != null
			group pm by pm.MemberPath.AssociationSetEnd;
			foreach (IGrouping<AssociationSetEnd, ModificationFunctionParameterBinding> grouping in enumerable)
			{
				this._xmlWriter.WriteStartElement("EndProperty");
				this._xmlWriter.WriteAttributeString("Name", grouping.Key.Name);
				foreach (ModificationFunctionParameterBinding modificationFunctionParameterBinding in grouping)
				{
					this.WriteScalarParameterElement(modificationFunctionParameterBinding.MemberPath.Members.First<EdmMember>(), modificationFunctionParameterBinding);
				}
				this._xmlWriter.WriteEndElement();
			}
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000B4B8 File Offset: 0x000096B8
		private void WritePropertyParameterBindings(IEnumerable<ModificationFunctionParameterBinding> parameterBindings, int level = 0)
		{
			IEnumerable<IGrouping<EdmMember, ModificationFunctionParameterBinding>> enumerable = from pm in parameterBindings
			where pm.MemberPath.AssociationSetEnd == null && pm.MemberPath.Members.Count<EdmMember>() > level
			group pm by pm.MemberPath.Members.ElementAt(level);
			foreach (IGrouping<EdmMember, ModificationFunctionParameterBinding> grouping in enumerable)
			{
				EdmProperty edmProperty = (EdmProperty)grouping.Key;
				if (edmProperty.IsComplexType)
				{
					this._xmlWriter.WriteStartElement("ComplexProperty");
					this._xmlWriter.WriteAttributeString("Name", edmProperty.Name);
					this._xmlWriter.WriteAttributeString("TypeName", this._entityTypeNamespace + "." + edmProperty.ComplexType.Name);
					this.WritePropertyParameterBindings(grouping, level + 1);
					this._xmlWriter.WriteEndElement();
				}
				else
				{
					foreach (ModificationFunctionParameterBinding parameterBinding in grouping)
					{
						this.WriteScalarParameterElement(edmProperty, parameterBinding);
					}
				}
			}
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000B634 File Offset: 0x00009834
		private void WriteAssociationParameterBindings(IEnumerable<ModificationFunctionParameterBinding> parameterBindings)
		{
			IEnumerable<IGrouping<AssociationSetEnd, ModificationFunctionParameterBinding>> enumerable = from pm in parameterBindings
			where pm.MemberPath.AssociationSetEnd != null
			group pm by pm.MemberPath.AssociationSetEnd;
			using (IEnumerator<IGrouping<AssociationSetEnd, ModificationFunctionParameterBinding>> enumerator = enumerable.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					IGrouping<AssociationSetEnd, ModificationFunctionParameterBinding> group = enumerator.Current;
					this._xmlWriter.WriteStartElement("AssociationEnd");
					AssociationSet parentAssociationSet = group.Key.ParentAssociationSet;
					this._xmlWriter.WriteAttributeString("AssociationSet", parentAssociationSet.Name);
					this._xmlWriter.WriteAttributeString("From", group.Key.Name);
					this._xmlWriter.WriteAttributeString("To", parentAssociationSet.AssociationSetEnds.Single((AssociationSetEnd ae) => ae != group.Key).Name);
					foreach (ModificationFunctionParameterBinding modificationFunctionParameterBinding in group)
					{
						this.WriteScalarParameterElement(modificationFunctionParameterBinding.MemberPath.Members.First<EdmMember>(), modificationFunctionParameterBinding);
					}
					this._xmlWriter.WriteEndElement();
				}
			}
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000B7D8 File Offset: 0x000099D8
		private void WriteResultBindings(IEnumerable<ModificationFunctionResultBinding> resultBindings)
		{
			foreach (ModificationFunctionResultBinding modificationFunctionResultBinding in resultBindings)
			{
				this._xmlWriter.WriteStartElement("ResultBinding");
				this._xmlWriter.WriteAttributeString("Name", modificationFunctionResultBinding.Property.Name);
				this._xmlWriter.WriteAttributeString("ColumnName", modificationFunctionResultBinding.ColumnName);
				this._xmlWriter.WriteEndElement();
			}
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000B868 File Offset: 0x00009A68
		private void WriteScalarParameterElement(EdmMember member, ModificationFunctionParameterBinding parameterBinding)
		{
			this._xmlWriter.WriteStartElement("ScalarProperty");
			this._xmlWriter.WriteAttributeString("Name", member.Name);
			this._xmlWriter.WriteAttributeString("ParameterName", parameterBinding.Parameter.Name);
			this._xmlWriter.WriteAttributeString("Version", parameterBinding.IsCurrent ? "Current" : "Original");
			this._xmlWriter.WriteEndElement();
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000B8E8 File Offset: 0x00009AE8
		private void WritePropertyMapping(PropertyMapping propertyMapping)
		{
			ScalarPropertyMapping scalarPropertyMapping = propertyMapping as ScalarPropertyMapping;
			if (scalarPropertyMapping != null)
			{
				this.WritePropertyMapping(scalarPropertyMapping);
				return;
			}
			ComplexPropertyMapping complexPropertyMapping = propertyMapping as ComplexPropertyMapping;
			if (complexPropertyMapping != null)
			{
				this.WritePropertyMapping(complexPropertyMapping);
			}
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000B918 File Offset: 0x00009B18
		private void WritePropertyMapping(ScalarPropertyMapping scalarPropertyMapping)
		{
			this.WriteScalarPropertyElement(scalarPropertyMapping.Property.Name, scalarPropertyMapping.Column.Name);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000B938 File Offset: 0x00009B38
		private void WritePropertyMapping(ComplexPropertyMapping complexPropertyMapping)
		{
			this._xmlWriter.WriteStartElement("ComplexProperty");
			this._xmlWriter.WriteAttributeString("Name", complexPropertyMapping.Property.Name);
			this._xmlWriter.WriteAttributeString("TypeName", this._entityTypeNamespace + "." + complexPropertyMapping.Property.ComplexType.Name);
			foreach (PropertyMapping propertyMapping in complexPropertyMapping.TypeMappings.Single<ComplexTypeMapping>().PropertyMappings)
			{
				this.WritePropertyMapping(propertyMapping);
			}
			this._xmlWriter.WriteEndElement();
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000B9F8 File Offset: 0x00009BF8
		private static string GetEntityTypeName(string fullyQualifiedEntityTypeName, bool isHierarchyMapping)
		{
			if (isHierarchyMapping)
			{
				return "IsTypeOf(" + fullyQualifiedEntityTypeName + ")";
			}
			return fullyQualifiedEntityTypeName;
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000BA10 File Offset: 0x00009C10
		private void WriteConditionElement(ConditionPropertyMapping condition)
		{
			this._xmlWriter.WriteStartElement("Condition");
			if (condition.IsNull != null)
			{
				this.WriteIsNullConditionAttribute(condition.IsNull.Value);
			}
			else
			{
				this.WriteConditionValue(condition.Value);
			}
			this._xmlWriter.WriteAttributeString("ColumnName", condition.Column.Name);
			this._xmlWriter.WriteEndElement();
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000BA85 File Offset: 0x00009C85
		private void WriteIsNullConditionAttribute(bool isNullValue)
		{
			this._xmlWriter.WriteAttributeString("IsNull", XmlSchemaWriter.GetLowerCaseStringFromBoolValue(isNullValue));
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000BAA0 File Offset: 0x00009CA0
		private void WriteConditionValue(object conditionValue)
		{
			if (conditionValue is bool)
			{
				this._xmlWriter.WriteAttributeString("Value", ((bool)conditionValue) ? "1" : "0");
				return;
			}
			this._xmlWriter.WriteAttributeString("Value", conditionValue.ToString());
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000BAF0 File Offset: 0x00009CF0
		private void WriteScalarPropertyElement(string propertyName, string columnName)
		{
			this._xmlWriter.WriteStartElement("ScalarProperty");
			this._xmlWriter.WriteAttributeString("Name", propertyName);
			this._xmlWriter.WriteAttributeString("ColumnName", columnName);
			this._xmlWriter.WriteEndElement();
		}

		// Token: 0x040000E0 RID: 224
		private string _entityTypeNamespace;

		// Token: 0x040000E1 RID: 225
		private string _dbSchemaName;
	}
}
