using System;

namespace System.Xml.Schema
{
	// Token: 0x020001DE RID: 478
	internal class BaseProcessor
	{
		// Token: 0x06001FC3 RID: 8131 RVA: 0x000ABBBC File Offset: 0x000A9DBC
		public BaseProcessor(XmlNameTable nameTable, SchemaNames schemaNames, ValidationEventHandler eventHandler) : this(nameTable, schemaNames, eventHandler, new XmlSchemaCompilationSettings())
		{
		}

		// Token: 0x06001FC4 RID: 8132 RVA: 0x000ABBCC File Offset: 0x000A9DCC
		public BaseProcessor(XmlNameTable nameTable, SchemaNames schemaNames, ValidationEventHandler eventHandler, XmlSchemaCompilationSettings compilationSettings)
		{
			this.nameTable = nameTable;
			this.schemaNames = schemaNames;
			this.eventHandler = eventHandler;
			this.compilationSettings = compilationSettings;
			this.NsXml = nameTable.Add("http://www.w3.org/XML/1998/namespace");
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06001FC5 RID: 8133 RVA: 0x000ABC02 File Offset: 0x000A9E02
		protected XmlNameTable NameTable
		{
			get
			{
				return this.nameTable;
			}
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x06001FC6 RID: 8134 RVA: 0x000ABC0A File Offset: 0x000A9E0A
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

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x06001FC7 RID: 8135 RVA: 0x000ABC2B File Offset: 0x000A9E2B
		protected ValidationEventHandler EventHandler
		{
			get
			{
				return this.eventHandler;
			}
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06001FC8 RID: 8136 RVA: 0x000ABC33 File Offset: 0x000A9E33
		protected XmlSchemaCompilationSettings CompilationSettings
		{
			get
			{
				return this.compilationSettings;
			}
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06001FC9 RID: 8137 RVA: 0x000ABC3B File Offset: 0x000A9E3B
		protected bool HasErrors
		{
			get
			{
				return this.errorCount != 0;
			}
		}

		// Token: 0x06001FCA RID: 8138 RVA: 0x000ABC48 File Offset: 0x000A9E48
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

		// Token: 0x06001FCB RID: 8139 RVA: 0x000ABDC8 File Offset: 0x000A9FC8
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

		// Token: 0x06001FCC RID: 8140 RVA: 0x000ABE14 File Offset: 0x000AA014
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

		// Token: 0x06001FCD RID: 8141 RVA: 0x000ABE5C File Offset: 0x000AA05C
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

		// Token: 0x06001FCE RID: 8142 RVA: 0x000ABEA3 File Offset: 0x000AA0A3
		protected void SendValidationEvent(string code, XmlSchemaObject source)
		{
			this.SendValidationEvent(new XmlSchemaException(code, source), XmlSeverityType.Error);
		}

		// Token: 0x06001FCF RID: 8143 RVA: 0x000ABEB3 File Offset: 0x000AA0B3
		protected void SendValidationEvent(string code, string msg, XmlSchemaObject source)
		{
			this.SendValidationEvent(new XmlSchemaException(code, msg, source), XmlSeverityType.Error);
		}

		// Token: 0x06001FD0 RID: 8144 RVA: 0x000ABEC4 File Offset: 0x000AA0C4
		protected void SendValidationEvent(string code, string msg1, string msg2, XmlSchemaObject source)
		{
			this.SendValidationEvent(new XmlSchemaException(code, new string[]
			{
				msg1,
				msg2
			}, source), XmlSeverityType.Error);
		}

		// Token: 0x06001FD1 RID: 8145 RVA: 0x000ABEE3 File Offset: 0x000AA0E3
		protected void SendValidationEvent(string code, string[] args, Exception innerException, XmlSchemaObject source)
		{
			this.SendValidationEvent(new XmlSchemaException(code, args, innerException, source.SourceUri, source.LineNumber, source.LinePosition, source), XmlSeverityType.Error);
		}

		// Token: 0x06001FD2 RID: 8146 RVA: 0x000ABF0B File Offset: 0x000AA10B
		protected void SendValidationEvent(string code, string msg1, string msg2, string sourceUri, int lineNumber, int linePosition)
		{
			this.SendValidationEvent(new XmlSchemaException(code, new string[]
			{
				msg1,
				msg2
			}, sourceUri, lineNumber, linePosition), XmlSeverityType.Error);
		}

		// Token: 0x06001FD3 RID: 8147 RVA: 0x000ABF2E File Offset: 0x000AA12E
		protected void SendValidationEvent(string code, XmlSchemaObject source, XmlSeverityType severity)
		{
			this.SendValidationEvent(new XmlSchemaException(code, source), severity);
		}

		// Token: 0x06001FD4 RID: 8148 RVA: 0x000ABF3E File Offset: 0x000AA13E
		protected void SendValidationEvent(XmlSchemaException e)
		{
			this.SendValidationEvent(e, XmlSeverityType.Error);
		}

		// Token: 0x06001FD5 RID: 8149 RVA: 0x000ABF48 File Offset: 0x000AA148
		protected void SendValidationEvent(string code, string msg, XmlSchemaObject source, XmlSeverityType severity)
		{
			this.SendValidationEvent(new XmlSchemaException(code, msg, source), severity);
		}

		// Token: 0x06001FD6 RID: 8150 RVA: 0x000ABF5A File Offset: 0x000AA15A
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

		// Token: 0x06001FD7 RID: 8151 RVA: 0x000ABF8E File Offset: 0x000AA18E
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

		// Token: 0x04000D65 RID: 3429
		private XmlNameTable nameTable;

		// Token: 0x04000D66 RID: 3430
		private SchemaNames schemaNames;

		// Token: 0x04000D67 RID: 3431
		private ValidationEventHandler eventHandler;

		// Token: 0x04000D68 RID: 3432
		private XmlSchemaCompilationSettings compilationSettings;

		// Token: 0x04000D69 RID: 3433
		private int errorCount;

		// Token: 0x04000D6A RID: 3434
		private string NsXml;
	}
}
