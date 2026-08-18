using System;

namespace System.Xml.Schema
{
	// Token: 0x02000185 RID: 389
	internal class BaseProcessor
	{
		// Token: 0x06001495 RID: 5269 RVA: 0x00057F48 File Offset: 0x00056F48
		public BaseProcessor(XmlNameTable nameTable, SchemaNames schemaNames, ValidationEventHandler eventHandler) : this(nameTable, schemaNames, eventHandler, new XmlSchemaCompilationSettings())
		{
		}

		// Token: 0x06001496 RID: 5270 RVA: 0x00057F58 File Offset: 0x00056F58
		public BaseProcessor(XmlNameTable nameTable, SchemaNames schemaNames, ValidationEventHandler eventHandler, XmlSchemaCompilationSettings compilationSettings)
		{
			this.nameTable = nameTable;
			this.schemaNames = schemaNames;
			this.eventHandler = eventHandler;
			this.compilationSettings = compilationSettings;
			this.NsXml = nameTable.Add("http://www.w3.org/XML/1998/namespace");
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06001497 RID: 5271 RVA: 0x00057F8E File Offset: 0x00056F8E
		protected XmlNameTable NameTable
		{
			get
			{
				return this.nameTable;
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06001498 RID: 5272 RVA: 0x00057F96 File Offset: 0x00056F96
		protected SchemaNames SchemaNames
		{
			get
			{
				if (this.schemaNames == null)
				{
					this.schemaNames = new SchemaNames(this.nameTable);
				}
				return this.schemaNames;
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06001499 RID: 5273 RVA: 0x00057FB7 File Offset: 0x00056FB7
		protected ValidationEventHandler EventHandler
		{
			get
			{
				return this.eventHandler;
			}
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x0600149A RID: 5274 RVA: 0x00057FBF File Offset: 0x00056FBF
		protected XmlSchemaCompilationSettings CompilationSettings
		{
			get
			{
				return this.compilationSettings;
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x0600149B RID: 5275 RVA: 0x00057FC7 File Offset: 0x00056FC7
		protected bool HasErrors
		{
			get
			{
				return this.errorCount != 0;
			}
		}

		// Token: 0x0600149C RID: 5276 RVA: 0x00057FD8 File Offset: 0x00056FD8
		protected void AddToTable(XmlSchemaObjectTable table, XmlQualifiedName qname, XmlSchemaObject item)
		{
			if (qname.Name.Length == 0)
			{
				return;
			}
			XmlSchemaObject xmlSchemaObject = table[qname];
			if (xmlSchemaObject == null)
			{
				table.Add(qname, item);
				return;
			}
			if (xmlSchemaObject == item)
			{
				return;
			}
			string code = "Sch_DupGlobalElement";
			if (item is XmlSchemaAttributeGroup)
			{
				string strA = this.nameTable.Add(qname.Namespace);
				if (Ref.Equal(strA, this.NsXml))
				{
					XmlSchema buildInSchema = Preprocessor.GetBuildInSchema();
					XmlSchemaObject xmlSchemaObject2 = buildInSchema.AttributeGroups[qname];
					if (xmlSchemaObject == xmlSchemaObject2)
					{
						table.Insert(qname, item);
						return;
					}
					if (item == xmlSchemaObject2)
					{
						return;
					}
				}
				else if (this.IsValidAttributeGroupRedefine(xmlSchemaObject, item, table))
				{
					return;
				}
				code = "Sch_DupAttributeGroup";
			}
			else if (item is XmlSchemaAttribute)
			{
				string strA2 = this.nameTable.Add(qname.Namespace);
				if (Ref.Equal(strA2, this.NsXml))
				{
					XmlSchema buildInSchema2 = Preprocessor.GetBuildInSchema();
					XmlSchemaObject xmlSchemaObject3 = buildInSchema2.Attributes[qname];
					if (xmlSchemaObject == xmlSchemaObject3)
					{
						table.Insert(qname, item);
						return;
					}
					if (item == xmlSchemaObject3)
					{
						return;
					}
				}
				code = "Sch_DupGlobalAttribute";
			}
			else if (item is XmlSchemaSimpleType)
			{
				if (this.IsValidTypeRedefine(xmlSchemaObject, item, table))
				{
					return;
				}
				code = "Sch_DupSimpleType";
			}
			else if (item is XmlSchemaComplexType)
			{
				if (this.IsValidTypeRedefine(xmlSchemaObject, item, table))
				{
					return;
				}
				code = "Sch_DupComplexType";
			}
			else if (item is XmlSchemaGroup)
			{
				if (this.IsValidGroupRedefine(xmlSchemaObject, item, table))
				{
					return;
				}
				code = "Sch_DupGroup";
			}
			else if (item is XmlSchemaNotation)
			{
				code = "Sch_DupNotation";
			}
			else if (item is XmlSchemaIdentityConstraint)
			{
				code = "Sch_DupIdentityConstraint";
			}
			this.SendValidationEvent(code, qname.ToString(), item);
		}

		// Token: 0x0600149D RID: 5277 RVA: 0x00058158 File Offset: 0x00057158
		private bool IsValidAttributeGroupRedefine(XmlSchemaObject existingObject, XmlSchemaObject item, XmlSchemaObjectTable table)
		{
			XmlSchemaAttributeGroup xmlSchemaAttributeGroup = item as XmlSchemaAttributeGroup;
			XmlSchemaAttributeGroup xmlSchemaAttributeGroup2 = existingObject as XmlSchemaAttributeGroup;
			if (xmlSchemaAttributeGroup2 == xmlSchemaAttributeGroup.Redefined)
			{
				if (xmlSchemaAttributeGroup2.AttributeUses.Count == 0)
				{
					table.Insert(xmlSchemaAttributeGroup.QualifiedName, xmlSchemaAttributeGroup);
					return true;
				}
			}
			else if (xmlSchemaAttributeGroup2.Redefined == xmlSchemaAttributeGroup)
			{
				return true;
			}
			return false;
		}

		// Token: 0x0600149E RID: 5278 RVA: 0x000581A4 File Offset: 0x000571A4
		private bool IsValidGroupRedefine(XmlSchemaObject existingObject, XmlSchemaObject item, XmlSchemaObjectTable table)
		{
			XmlSchemaGroup xmlSchemaGroup = item as XmlSchemaGroup;
			XmlSchemaGroup xmlSchemaGroup2 = existingObject as XmlSchemaGroup;
			if (xmlSchemaGroup2 == xmlSchemaGroup.Redefined)
			{
				if (xmlSchemaGroup2.CanonicalParticle == null)
				{
					table.Insert(xmlSchemaGroup.QualifiedName, xmlSchemaGroup);
					return true;
				}
			}
			else if (xmlSchemaGroup2.Redefined == xmlSchemaGroup)
			{
				return true;
			}
			return false;
		}

		// Token: 0x0600149F RID: 5279 RVA: 0x000581EC File Offset: 0x000571EC
		private bool IsValidTypeRedefine(XmlSchemaObject existingObject, XmlSchemaObject item, XmlSchemaObjectTable table)
		{
			XmlSchemaType xmlSchemaType = item as XmlSchemaType;
			XmlSchemaType xmlSchemaType2 = existingObject as XmlSchemaType;
			if (xmlSchemaType2 == xmlSchemaType.Redefined)
			{
				if (xmlSchemaType2.ElementDecl == null)
				{
					table.Insert(xmlSchemaType.QualifiedName, xmlSchemaType);
					return true;
				}
			}
			else if (xmlSchemaType2.Redefined == xmlSchemaType)
			{
				return true;
			}
			return false;
		}

		// Token: 0x060014A0 RID: 5280 RVA: 0x00058233 File Offset: 0x00057233
		protected void SendValidationEvent(string code, XmlSchemaObject source)
		{
			this.SendValidationEvent(new XmlSchemaException(code, source), XmlSeverityType.Error);
		}

		// Token: 0x060014A1 RID: 5281 RVA: 0x00058243 File Offset: 0x00057243
		protected void SendValidationEvent(string code, string msg, XmlSchemaObject source)
		{
			this.SendValidationEvent(new XmlSchemaException(code, msg, source), XmlSeverityType.Error);
		}

		// Token: 0x060014A2 RID: 5282 RVA: 0x00058254 File Offset: 0x00057254
		protected void SendValidationEvent(string code, string msg1, string msg2, XmlSchemaObject source)
		{
			this.SendValidationEvent(new XmlSchemaException(code, new string[]
			{
				msg1,
				msg2
			}, source), XmlSeverityType.Error);
		}

		// Token: 0x060014A3 RID: 5283 RVA: 0x00058280 File Offset: 0x00057280
		protected void SendValidationEvent(string code, string[] args, Exception innerException, XmlSchemaObject source)
		{
			this.SendValidationEvent(new XmlSchemaException(code, args, innerException, source.SourceUri, source.LineNumber, source.LinePosition, source), XmlSeverityType.Error);
		}

		// Token: 0x060014A4 RID: 5284 RVA: 0x000582B4 File Offset: 0x000572B4
		protected void SendValidationEvent(string code, string msg1, string msg2, string sourceUri, int lineNumber, int linePosition)
		{
			this.SendValidationEvent(new XmlSchemaException(code, new string[]
			{
				msg1,
				msg2
			}, sourceUri, lineNumber, linePosition), XmlSeverityType.Error);
		}

		// Token: 0x060014A5 RID: 5285 RVA: 0x000582E4 File Offset: 0x000572E4
		protected void SendValidationEvent(string code, XmlSchemaObject source, XmlSeverityType severity)
		{
			this.SendValidationEvent(new XmlSchemaException(code, source), severity);
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x000582F4 File Offset: 0x000572F4
		protected void SendValidationEvent(XmlSchemaException e)
		{
			this.SendValidationEvent(e, XmlSeverityType.Error);
		}

		// Token: 0x060014A7 RID: 5287 RVA: 0x000582FE File Offset: 0x000572FE
		protected void SendValidationEvent(string code, string msg, XmlSchemaObject source, XmlSeverityType severity)
		{
			this.SendValidationEvent(new XmlSchemaException(code, msg, source), severity);
		}

		// Token: 0x060014A8 RID: 5288 RVA: 0x00058310 File Offset: 0x00057310
		protected void SendValidationEvent(XmlSchemaException e, XmlSeverityType severity)
		{
			if (severity == XmlSeverityType.Error)
			{
				this.errorCount++;
			}
			if (this.eventHandler != null)
			{
				this.eventHandler(null, new ValidationEventArgs(e, severity));
				return;
			}
			if (severity == XmlSeverityType.Error)
			{
				throw e;
			}
		}

		// Token: 0x060014A9 RID: 5289 RVA: 0x00058344 File Offset: 0x00057344
		protected void SendValidationEventNoThrow(XmlSchemaException e, XmlSeverityType severity)
		{
			if (severity == XmlSeverityType.Error)
			{
				this.errorCount++;
			}
			if (this.eventHandler != null)
			{
				this.eventHandler(null, new ValidationEventArgs(e, severity));
			}
		}

		// Token: 0x04000C7A RID: 3194
		private XmlNameTable nameTable;

		// Token: 0x04000C7B RID: 3195
		private SchemaNames schemaNames;

		// Token: 0x04000C7C RID: 3196
		private ValidationEventHandler eventHandler;

		// Token: 0x04000C7D RID: 3197
		private XmlSchemaCompilationSettings compilationSettings;

		// Token: 0x04000C7E RID: 3198
		private int errorCount;

		// Token: 0x04000C7F RID: 3199
		private string NsXml;
	}
}
