using System;
using System.Collections.Generic;
using System.Data.Entity.Edm;
using System.Data.Entity.Resources;
using System.Linq;
using System.Text;
using System.Xml;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000026 RID: 38
	internal sealed class EdmSerializationVisitor : EdmModelVisitor
	{
		// Token: 0x06000170 RID: 368 RVA: 0x00008110 File Offset: 0x00006310
		public EdmSerializationVisitor(XmlWriter xmlWriter, double edmVersion, bool serializeDefaultNullability = false) : this(new EdmXmlSchemaWriter(xmlWriter, edmVersion, serializeDefaultNullability, null))
		{
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00008121 File Offset: 0x00006321
		public EdmSerializationVisitor(EdmXmlSchemaWriter schemaWriter)
		{
			this._schemaWriter = schemaWriter;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00008130 File Offset: 0x00006330
		public void Visit(EdmModel edmModel, string modelNamespace)
		{
			string schemaNamespace = modelNamespace ?? edmModel.NamespaceNames.DefaultIfEmpty("Empty").Single<string>();
			this._schemaWriter.WriteSchemaElementHeader(schemaNamespace);
			this.VisitEdmModel(edmModel);
			this._schemaWriter.WriteEndElement();
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00008176 File Offset: 0x00006376
		public void Visit(EdmModel edmModel, string provider, string providerManifestToken)
		{
			this.Visit(edmModel, edmModel.Containers.Single<EntityContainer>().Name + "Schema", provider, providerManifestToken);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x000081D8 File Offset: 0x000063D8
		public void Visit(EdmModel edmModel, string namespaceName, string provider, string providerManifestToken)
		{
			bool writeStoreSchemaGenNamespace = edmModel.Container.BaseEntitySets.Any((EntitySetBase e) => e.MetadataProperties.Any((MetadataProperty p) => p.Name.StartsWith("http://schemas.microsoft.com/ado/2007/12/edm/EntityStoreSchemaGenerator", StringComparison.Ordinal)));
			this._schemaWriter.WriteSchemaElementHeader(namespaceName, provider, providerManifestToken, writeStoreSchemaGenNamespace);
			this.VisitEdmModel(edmModel);
			this._schemaWriter.WriteEndElement();
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00008235 File Offset: 0x00006435
		protected override void VisitEdmEntityContainer(EntityContainer item)
		{
			this._schemaWriter.WriteEntityContainerElementHeader(item);
			base.VisitEdmEntityContainer(item);
			this._schemaWriter.WriteEndElement();
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00008255 File Offset: 0x00006455
		protected internal override void VisitEdmFunction(EdmFunction item)
		{
			this._schemaWriter.WriteFunctionElementHeader(item);
			base.VisitEdmFunction(item);
			this._schemaWriter.WriteEndElement();
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00008275 File Offset: 0x00006475
		protected internal override void VisitFunctionParameter(FunctionParameter functionParameter)
		{
			this._schemaWriter.WriteFunctionParameterHeader(functionParameter);
			base.VisitFunctionParameter(functionParameter);
			this._schemaWriter.WriteEndElement();
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00008295 File Offset: 0x00006495
		protected internal override void VisitFunctionReturnParameter(FunctionParameter returnParameter)
		{
			if (returnParameter.TypeUsage.EdmType.BuiltInTypeKind != BuiltInTypeKind.PrimitiveType)
			{
				this._schemaWriter.WriteFunctionReturnTypeElementHeader();
				base.VisitFunctionReturnParameter(returnParameter);
				this._schemaWriter.WriteEndElement();
				return;
			}
			base.VisitFunctionReturnParameter(returnParameter);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x000082D0 File Offset: 0x000064D0
		protected internal override void VisitCollectionType(CollectionType collectionType)
		{
			this._schemaWriter.WriteCollectionTypeElementHeader();
			base.VisitCollectionType(collectionType);
			this._schemaWriter.WriteEndElement();
		}

		// Token: 0x0600017A RID: 378 RVA: 0x000082F0 File Offset: 0x000064F0
		protected override void VisitEdmAssociationSet(AssociationSet item)
		{
			this._schemaWriter.WriteAssociationSetElementHeader(item);
			base.VisitEdmAssociationSet(item);
			if (item.SourceSet != null)
			{
				this._schemaWriter.WriteAssociationSetEndElement(item.SourceSet, item.SourceEnd.Name);
			}
			if (item.TargetSet != null)
			{
				this._schemaWriter.WriteAssociationSetEndElement(item.TargetSet, item.TargetEnd.Name);
			}
			this._schemaWriter.WriteEndElement();
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00008363 File Offset: 0x00006563
		protected internal override void VisitEdmEntitySet(EntitySet item)
		{
			this._schemaWriter.WriteEntitySetElementHeader(item);
			this._schemaWriter.WriteDefiningQuery(item);
			base.VisitEdmEntitySet(item);
			this._schemaWriter.WriteEndElement();
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00008390 File Offset: 0x00006590
		protected internal override void VisitFunctionImport(EdmFunction functionImport)
		{
			this._schemaWriter.WriteFunctionImportElementHeader(functionImport);
			if (functionImport.ReturnParameters.Count == 1)
			{
				this._schemaWriter.WriteFunctionImportReturnTypeAttributes(functionImport.ReturnParameter, functionImport.EntitySet, true);
				this.VisitFunctionImportReturnParameter(functionImport.ReturnParameter);
			}
			base.VisitFunctionImport(functionImport);
			if (functionImport.ReturnParameters.Count > 1)
			{
				this.VisitFunctionImportReturnParameters(functionImport);
			}
			this._schemaWriter.WriteEndElement();
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00008402 File Offset: 0x00006602
		protected internal override void VisitFunctionImportParameter(FunctionParameter parameter)
		{
			this._schemaWriter.WriteFunctionImportParameterElementHeader(parameter);
			base.VisitFunctionImportParameter(parameter);
			this._schemaWriter.WriteEndElement();
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00008424 File Offset: 0x00006624
		private void VisitFunctionImportReturnParameters(EdmFunction functionImport)
		{
			for (int i = 0; i < functionImport.ReturnParameters.Count; i++)
			{
				this._schemaWriter.WriteFunctionReturnTypeElementHeader();
				this._schemaWriter.WriteFunctionImportReturnTypeAttributes(functionImport.ReturnParameters[i], functionImport.EntitySets[i], false);
				this.VisitFunctionImportReturnParameter(functionImport.ReturnParameter);
				this._schemaWriter.WriteEndElement();
			}
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000848D File Offset: 0x0000668D
		protected internal override void VisitRowType(RowType rowType)
		{
			this._schemaWriter.WriteRowTypeElementHeader();
			base.VisitRowType(rowType);
			this._schemaWriter.WriteEndElement();
		}

		// Token: 0x06000180 RID: 384 RVA: 0x000084B8 File Offset: 0x000066B8
		protected internal override void VisitEdmEntityType(EntityType item)
		{
			StringBuilder stringBuilder = new StringBuilder();
			EdmSerializationVisitor.AppendSchemaErrors(stringBuilder, item);
			if (MetadataItemHelper.IsInvalid(item))
			{
				this.AppendMetadataItem<EntityType>(stringBuilder, item, delegate(EdmSerializationVisitor v, EntityType i)
				{
					v.InternalVisitEdmEntityType(i);
				});
				this.WriteComment(stringBuilder.ToString());
				return;
			}
			this.WriteComment(stringBuilder.ToString());
			this.InternalVisitEdmEntityType(item);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000851F File Offset: 0x0000671F
		protected override void VisitEdmEnumType(EnumType item)
		{
			this._schemaWriter.WriteEnumTypeElementHeader(item);
			base.VisitEdmEnumType(item);
			this._schemaWriter.WriteEndElement();
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000853F File Offset: 0x0000673F
		protected override void VisitEdmEnumTypeMember(EnumMember item)
		{
			this._schemaWriter.WriteEnumTypeMemberElementHeader(item);
			base.VisitEdmEnumTypeMember(item);
			this._schemaWriter.WriteEndElement();
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00008560 File Offset: 0x00006760
		protected override void VisitKeyProperties(EntityType entityType, IList<EdmProperty> properties)
		{
			if (properties.Any<EdmProperty>())
			{
				this._schemaWriter.WriteDelaredKeyPropertiesElementHeader();
				foreach (EdmProperty property in properties)
				{
					this._schemaWriter.WriteDelaredKeyPropertyRefElement(property);
				}
				this._schemaWriter.WriteEndElement();
			}
		}

		// Token: 0x06000184 RID: 388 RVA: 0x000085CC File Offset: 0x000067CC
		protected internal override void VisitEdmProperty(EdmProperty item)
		{
			this._schemaWriter.WritePropertyElementHeader(item);
			base.VisitEdmProperty(item);
			this._schemaWriter.WriteEndElement();
		}

		// Token: 0x06000185 RID: 389 RVA: 0x000085EC File Offset: 0x000067EC
		protected override void VisitEdmNavigationProperty(NavigationProperty item)
		{
			this._schemaWriter.WriteNavigationPropertyElementHeader(item);
			base.VisitEdmNavigationProperty(item);
			this._schemaWriter.WriteEndElement();
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000860C File Offset: 0x0000680C
		protected override void VisitComplexType(ComplexType item)
		{
			this._schemaWriter.WriteComplexTypeElementHeader(item);
			base.VisitComplexType(item);
			this._schemaWriter.WriteEndElement();
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00008638 File Offset: 0x00006838
		protected internal override void VisitEdmAssociationType(AssociationType item)
		{
			StringBuilder stringBuilder = new StringBuilder();
			EdmSerializationVisitor.AppendSchemaErrors(stringBuilder, item);
			if (MetadataItemHelper.IsInvalid(item))
			{
				this.AppendMetadataItem<AssociationType>(stringBuilder, item, delegate(EdmSerializationVisitor v, AssociationType i)
				{
					v.InternalVisitEdmAssociationType(i);
				});
				this.WriteComment(stringBuilder.ToString());
				return;
			}
			this.WriteComment(stringBuilder.ToString());
			this.InternalVisitEdmAssociationType(item);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000869F File Offset: 0x0000689F
		protected override void VisitEdmAssociationEnd(RelationshipEndMember item)
		{
			this._schemaWriter.WriteAssociationEndElementHeader(item);
			if (item.DeleteBehavior != OperationAction.None)
			{
				this._schemaWriter.WriteOperationActionElement("OnDelete", item.DeleteBehavior);
			}
			this.VisitMetadataItem(item);
			this._schemaWriter.WriteEndElement();
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000086E0 File Offset: 0x000068E0
		protected override void VisitEdmAssociationConstraint(ReferentialConstraint item)
		{
			this._schemaWriter.WriteReferentialConstraintElementHeader();
			this._schemaWriter.WriteReferentialConstraintRoleElement("Principal", item.FromRole, item.FromProperties);
			this._schemaWriter.WriteReferentialConstraintRoleElement("Dependent", item.ToRole, item.ToProperties);
			this.VisitMetadataItem(item);
			this._schemaWriter.WriteEndElement();
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00008742 File Offset: 0x00006942
		private void InternalVisitEdmEntityType(EntityType item)
		{
			this._schemaWriter.WriteEntityTypeElementHeader(item);
			base.VisitEdmEntityType(item);
			this._schemaWriter.WriteEndElement();
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00008762 File Offset: 0x00006962
		private void InternalVisitEdmAssociationType(AssociationType item)
		{
			this._schemaWriter.WriteAssociationTypeElementHeader(item);
			base.VisitEdmAssociationType(item);
			this._schemaWriter.WriteEndElement();
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00008784 File Offset: 0x00006984
		private static void AppendSchemaErrors(StringBuilder builder, MetadataItem item)
		{
			if (MetadataItemHelper.HasSchemaErrors(item))
			{
				builder.Append(Strings.MetadataItemErrorsFoundDuringGeneration);
				foreach (EdmSchemaError edmSchemaError in MetadataItemHelper.GetSchemaErrors(item))
				{
					builder.AppendLine();
					builder.Append(edmSchemaError.ToString());
				}
			}
		}

		// Token: 0x0600018D RID: 397 RVA: 0x000087F4 File Offset: 0x000069F4
		private void AppendMetadataItem<T>(StringBuilder builder, T item, Action<EdmSerializationVisitor, T> visitAction) where T : MetadataItem
		{
			XmlWriterSettings xmlWriterSettings = new XmlWriterSettings
			{
				ConformanceLevel = ConformanceLevel.Fragment,
				Indent = true
			};
			XmlWriterSettings xmlWriterSettings2 = xmlWriterSettings;
			xmlWriterSettings2.NewLineChars += "        ";
			builder.Append(xmlWriterSettings.NewLineChars);
			using (XmlWriter xmlWriter = XmlWriter.Create(builder, xmlWriterSettings))
			{
				EdmSerializationVisitor arg = new EdmSerializationVisitor(this._schemaWriter.Replicate(xmlWriter));
				visitAction(arg, item);
			}
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00008878 File Offset: 0x00006A78
		private void WriteComment(string comment)
		{
			this._schemaWriter.WriteComment(comment.Replace("--", "- -"));
		}

		// Token: 0x040000AD RID: 173
		private readonly EdmXmlSchemaWriter _schemaWriter;
	}
}
