using System;

namespace System.Xml.Schema
{
	// Token: 0x02000256 RID: 598
	internal sealed class SchemaAttDef : SchemaDeclBase, IDtdDefaultAttributeInfo, IDtdAttributeInfo
	{
		// Token: 0x06002363 RID: 9059 RVA: 0x000BE8A0 File Offset: 0x000BCAA0
		public SchemaAttDef(XmlQualifiedName name, string prefix) : base(name, prefix)
		{
		}

		// Token: 0x06002364 RID: 9060 RVA: 0x000BE8AA File Offset: 0x000BCAAA
		public SchemaAttDef(XmlQualifiedName name) : base(name, null)
		{
		}

		// Token: 0x06002365 RID: 9061 RVA: 0x000BE8B4 File Offset: 0x000BCAB4
		private SchemaAttDef()
		{
		}

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06002366 RID: 9062 RVA: 0x000BE8BC File Offset: 0x000BCABC
		string IDtdAttributeInfo.Prefix
		{
			get
			{
				return this.Prefix;
			}
		}

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x06002367 RID: 9063 RVA: 0x000BE8C4 File Offset: 0x000BCAC4
		string IDtdAttributeInfo.LocalName
		{
			get
			{
				return this.Name.Name;
			}
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x06002368 RID: 9064 RVA: 0x000BE8D1 File Offset: 0x000BCAD1
		int IDtdAttributeInfo.LineNumber
		{
			get
			{
				return this.LineNumber;
			}
		}

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x06002369 RID: 9065 RVA: 0x000BE8D9 File Offset: 0x000BCAD9
		int IDtdAttributeInfo.LinePosition
		{
			get
			{
				return this.LinePosition;
			}
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x0600236A RID: 9066 RVA: 0x000BE8E1 File Offset: 0x000BCAE1
		bool IDtdAttributeInfo.IsNonCDataType
		{
			get
			{
				return this.TokenizedType > XmlTokenizedType.CDATA;
			}
		}

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x0600236B RID: 9067 RVA: 0x000BE8EC File Offset: 0x000BCAEC
		bool IDtdAttributeInfo.IsDeclaredInExternal
		{
			get
			{
				return this.IsDeclaredInExternal;
			}
		}

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x0600236C RID: 9068 RVA: 0x000BE8F4 File Offset: 0x000BCAF4
		bool IDtdAttributeInfo.IsXmlAttribute
		{
			get
			{
				return this.Reserved > SchemaAttDef.Reserve.None;
			}
		}

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x0600236D RID: 9069 RVA: 0x000BE8FF File Offset: 0x000BCAFF
		string IDtdDefaultAttributeInfo.DefaultValueExpanded
		{
			get
			{
				return this.DefaultValueExpanded;
			}
		}

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x0600236E RID: 9070 RVA: 0x000BE907 File Offset: 0x000BCB07
		object IDtdDefaultAttributeInfo.DefaultValueTyped
		{
			get
			{
				return this.DefaultValueTyped;
			}
		}

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x0600236F RID: 9071 RVA: 0x000BE90F File Offset: 0x000BCB0F
		int IDtdDefaultAttributeInfo.ValueLineNumber
		{
			get
			{
				return this.ValueLineNumber;
			}
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x06002370 RID: 9072 RVA: 0x000BE917 File Offset: 0x000BCB17
		int IDtdDefaultAttributeInfo.ValueLinePosition
		{
			get
			{
				return this.ValueLinePosition;
			}
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x06002371 RID: 9073 RVA: 0x000BE91F File Offset: 0x000BCB1F
		// (set) Token: 0x06002372 RID: 9074 RVA: 0x000BE927 File Offset: 0x000BCB27
		internal int LinePosition
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

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x06002373 RID: 9075 RVA: 0x000BE930 File Offset: 0x000BCB30
		// (set) Token: 0x06002374 RID: 9076 RVA: 0x000BE938 File Offset: 0x000BCB38
		internal int LineNumber
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

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06002375 RID: 9077 RVA: 0x000BE941 File Offset: 0x000BCB41
		// (set) Token: 0x06002376 RID: 9078 RVA: 0x000BE949 File Offset: 0x000BCB49
		internal int ValueLinePosition
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

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x06002377 RID: 9079 RVA: 0x000BE952 File Offset: 0x000BCB52
		// (set) Token: 0x06002378 RID: 9080 RVA: 0x000BE95A File Offset: 0x000BCB5A
		internal int ValueLineNumber
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

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06002379 RID: 9081 RVA: 0x000BE963 File Offset: 0x000BCB63
		// (set) Token: 0x0600237A RID: 9082 RVA: 0x000BE979 File Offset: 0x000BCB79
		internal string DefaultValueExpanded
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

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x0600237B RID: 9083 RVA: 0x000BE982 File Offset: 0x000BCB82
		// (set) Token: 0x0600237C RID: 9084 RVA: 0x000BE98F File Offset: 0x000BCB8F
		internal XmlTokenizedType TokenizedType
		{
			get
			{
				return base.Datatype.TokenizedType;
			}
			set
			{
				base.Datatype = XmlSchemaDatatype.FromXmlTokenizedType(value);
			}
		}

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x0600237D RID: 9085 RVA: 0x000BE99D File Offset: 0x000BCB9D
		// (set) Token: 0x0600237E RID: 9086 RVA: 0x000BE9A5 File Offset: 0x000BCBA5
		internal SchemaAttDef.Reserve Reserved
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

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x0600237F RID: 9087 RVA: 0x000BE9AE File Offset: 0x000BCBAE
		internal bool DefaultValueChecked
		{
			get
			{
				return this.defaultValueChecked;
			}
		}

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x06002380 RID: 9088 RVA: 0x000BE9B6 File Offset: 0x000BCBB6
		// (set) Token: 0x06002381 RID: 9089 RVA: 0x000BE9BE File Offset: 0x000BCBBE
		internal bool HasEntityRef
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

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x06002382 RID: 9090 RVA: 0x000BE9C7 File Offset: 0x000BCBC7
		// (set) Token: 0x06002383 RID: 9091 RVA: 0x000BE9CF File Offset: 0x000BCBCF
		internal XmlSchemaAttribute SchemaAttribute
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

		// Token: 0x06002384 RID: 9092 RVA: 0x000BE9D8 File Offset: 0x000BCBD8
		internal void CheckXmlSpace(IValidationEventHandling validationEventHandling)
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
			validationEventHandling.SendEvent(new XmlSchemaException("Sch_XmlSpace", string.Empty), XmlSeverityType.Error);
		}

		// Token: 0x06002385 RID: 9093 RVA: 0x000BEAAB File Offset: 0x000BCCAB
		internal SchemaAttDef Clone()
		{
			return (SchemaAttDef)base.MemberwiseClone();
		}

		// Token: 0x04000EEA RID: 3818
		private string defExpanded;

		// Token: 0x04000EEB RID: 3819
		private int lineNum;

		// Token: 0x04000EEC RID: 3820
		private int linePos;

		// Token: 0x04000EED RID: 3821
		private int valueLineNum;

		// Token: 0x04000EEE RID: 3822
		private int valueLinePos;

		// Token: 0x04000EEF RID: 3823
		private SchemaAttDef.Reserve reserved;

		// Token: 0x04000EF0 RID: 3824
		private bool defaultValueChecked;

		// Token: 0x04000EF1 RID: 3825
		private bool hasEntityRef;

		// Token: 0x04000EF2 RID: 3826
		private XmlSchemaAttribute schemaAttribute;

		// Token: 0x04000EF3 RID: 3827
		public static readonly SchemaAttDef Empty = new SchemaAttDef();

		// Token: 0x02000494 RID: 1172
		internal enum Reserve
		{
			// Token: 0x04001E39 RID: 7737
			None,
			// Token: 0x04001E3A RID: 7738
			XmlSpace,
			// Token: 0x04001E3B RID: 7739
			XmlLang
		}
	}
}
