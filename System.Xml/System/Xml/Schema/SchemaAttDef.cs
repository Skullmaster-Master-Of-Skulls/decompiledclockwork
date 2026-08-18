using System;

namespace System.Xml.Schema
{
	// Token: 0x0200020B RID: 523
	internal sealed class SchemaAttDef : SchemaDeclBase
	{
		// Token: 0x060018CD RID: 6349 RVA: 0x00071B8D File Offset: 0x00070B8D
		public SchemaAttDef(XmlQualifiedName name, string prefix) : base(name, prefix)
		{
			this.reserved = SchemaAttDef.Reserve.None;
		}

		// Token: 0x060018CE RID: 6350 RVA: 0x00071B9E File Offset: 0x00070B9E
		private SchemaAttDef()
		{
		}

		// Token: 0x060018CF RID: 6351 RVA: 0x00071BA6 File Offset: 0x00070BA6
		public SchemaAttDef Clone()
		{
			return (SchemaAttDef)base.MemberwiseClone();
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x060018D0 RID: 6352 RVA: 0x00071BB3 File Offset: 0x00070BB3
		// (set) Token: 0x060018D1 RID: 6353 RVA: 0x00071BBB File Offset: 0x00070BBB
		internal int LinePos
		{
			get
			{
				return this.linePos;
			}
			set
			{
				this.linePos = value;
			}
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x060018D2 RID: 6354 RVA: 0x00071BC4 File Offset: 0x00070BC4
		// (set) Token: 0x060018D3 RID: 6355 RVA: 0x00071BCC File Offset: 0x00070BCC
		internal int LineNum
		{
			get
			{
				return this.lineNum;
			}
			set
			{
				this.lineNum = value;
			}
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x060018D4 RID: 6356 RVA: 0x00071BD5 File Offset: 0x00070BD5
		// (set) Token: 0x060018D5 RID: 6357 RVA: 0x00071BDD File Offset: 0x00070BDD
		internal int ValueLinePos
		{
			get
			{
				return this.valueLinePos;
			}
			set
			{
				this.valueLinePos = value;
			}
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x060018D6 RID: 6358 RVA: 0x00071BE6 File Offset: 0x00070BE6
		// (set) Token: 0x060018D7 RID: 6359 RVA: 0x00071BEE File Offset: 0x00070BEE
		internal int ValueLineNum
		{
			get
			{
				return this.valueLineNum;
			}
			set
			{
				this.valueLineNum = value;
			}
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x060018D8 RID: 6360 RVA: 0x00071BF7 File Offset: 0x00070BF7
		internal bool DefaultValueChecked
		{
			get
			{
				return this.defaultValueChecked;
			}
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x060018D9 RID: 6361 RVA: 0x00071BFF File Offset: 0x00070BFF
		// (set) Token: 0x060018DA RID: 6362 RVA: 0x00071C15 File Offset: 0x00070C15
		public string DefaultValueExpanded
		{
			get
			{
				if (this.defExpanded == null)
				{
					return string.Empty;
				}
				return this.defExpanded;
			}
			set
			{
				this.defExpanded = value;
			}
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x060018DB RID: 6363 RVA: 0x00071C1E File Offset: 0x00070C1E
		// (set) Token: 0x060018DC RID: 6364 RVA: 0x00071C26 File Offset: 0x00070C26
		public SchemaAttDef.Reserve Reserved
		{
			get
			{
				return this.reserved;
			}
			set
			{
				this.reserved = value;
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x060018DD RID: 6365 RVA: 0x00071C2F File Offset: 0x00070C2F
		// (set) Token: 0x060018DE RID: 6366 RVA: 0x00071C37 File Offset: 0x00070C37
		public bool HasEntityRef
		{
			get
			{
				return this.hasEntityRef;
			}
			set
			{
				this.hasEntityRef = value;
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x060018DF RID: 6367 RVA: 0x00071C40 File Offset: 0x00070C40
		// (set) Token: 0x060018E0 RID: 6368 RVA: 0x00071C48 File Offset: 0x00070C48
		public XmlSchemaAttribute SchemaAttribute
		{
			get
			{
				return this.schemaAttribute;
			}
			set
			{
				this.schemaAttribute = value;
			}
		}

		// Token: 0x060018E1 RID: 6369 RVA: 0x00071C54 File Offset: 0x00070C54
		public void CheckXmlSpace(ValidationEventHandler eventhandler)
		{
			if (this.datatype.TokenizedType == XmlTokenizedType.ENUMERATION && this.values != null && this.values.Count <= 2)
			{
				string a = this.values[0].ToString();
				if (this.values.Count == 2)
				{
					string a2 = this.values[1].ToString();
					if ((a == "default" || a2 == "default") && (a == "preserve" || a2 == "preserve"))
					{
						return;
					}
				}
				else if (a == "default" || a == "preserve")
				{
					return;
				}
			}
			eventhandler(this, new ValidationEventArgs(new XmlSchemaException("Sch_XmlSpace", string.Empty)));
		}

		// Token: 0x060018E2 RID: 6370 RVA: 0x00071D2C File Offset: 0x00070D2C
		internal void CheckDefaultValue(SchemaInfo schemaInfo, IDtdParserAdapter readerAdapter)
		{
			DtdValidator.CheckDefaultValue(this, schemaInfo, readerAdapter);
			this.defaultValueChecked = true;
		}

		// Token: 0x04000E9E RID: 3742
		private SchemaAttDef.Reserve reserved;

		// Token: 0x04000E9F RID: 3743
		private string defExpanded;

		// Token: 0x04000EA0 RID: 3744
		private bool hasEntityRef;

		// Token: 0x04000EA1 RID: 3745
		private XmlSchemaAttribute schemaAttribute;

		// Token: 0x04000EA2 RID: 3746
		private bool defaultValueChecked;

		// Token: 0x04000EA3 RID: 3747
		private int lineNum;

		// Token: 0x04000EA4 RID: 3748
		private int linePos;

		// Token: 0x04000EA5 RID: 3749
		private int valueLineNum;

		// Token: 0x04000EA6 RID: 3750
		private int valueLinePos;

		// Token: 0x04000EA7 RID: 3751
		public static readonly SchemaAttDef Empty = new SchemaAttDef();

		// Token: 0x0200020C RID: 524
		public enum Reserve
		{
			// Token: 0x04000EA9 RID: 3753
			None,
			// Token: 0x04000EAA RID: 3754
			XmlSpace,
			// Token: 0x04000EAB RID: 3755
			XmlLang
		}
	}
}
