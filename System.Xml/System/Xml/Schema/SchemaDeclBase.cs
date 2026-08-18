using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x02000209 RID: 521
	internal abstract class SchemaDeclBase
	{
		// Token: 0x060018B2 RID: 6322 RVA: 0x000719EC File Offset: 0x000709EC
		protected SchemaDeclBase(XmlQualifiedName name, string prefix)
		{
			this.name = name;
			this.prefix = prefix;
			this.maxLength = -1L;
			this.minLength = -1L;
		}

		// Token: 0x060018B3 RID: 6323 RVA: 0x00071A1D File Offset: 0x00070A1D
		protected SchemaDeclBase()
		{
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x060018B4 RID: 6324 RVA: 0x00071A30 File Offset: 0x00070A30
		// (set) Token: 0x060018B5 RID: 6325 RVA: 0x00071A38 File Offset: 0x00070A38
		public XmlQualifiedName Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x060018B6 RID: 6326 RVA: 0x00071A41 File Offset: 0x00070A41
		// (set) Token: 0x060018B7 RID: 6327 RVA: 0x00071A57 File Offset: 0x00070A57
		public string Prefix
		{
			get
			{
				if (this.prefix != null)
				{
					return this.prefix;
				}
				return string.Empty;
			}
			set
			{
				this.prefix = value;
			}
		}

		// Token: 0x060018B8 RID: 6328 RVA: 0x00071A60 File Offset: 0x00070A60
		public void AddValue(string value)
		{
			if (this.values == null)
			{
				this.values = new ArrayList();
			}
			this.values.Add(value);
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x060018B9 RID: 6329 RVA: 0x00071A82 File Offset: 0x00070A82
		// (set) Token: 0x060018BA RID: 6330 RVA: 0x00071A8A File Offset: 0x00070A8A
		public ArrayList Values
		{
			get
			{
				return this.values;
			}
			set
			{
				this.values = value;
			}
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x060018BB RID: 6331 RVA: 0x00071A93 File Offset: 0x00070A93
		// (set) Token: 0x060018BC RID: 6332 RVA: 0x00071A9B File Offset: 0x00070A9B
		public SchemaDeclBase.Use Presence
		{
			get
			{
				return this.presence;
			}
			set
			{
				this.presence = value;
			}
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x060018BD RID: 6333 RVA: 0x00071AA4 File Offset: 0x00070AA4
		// (set) Token: 0x060018BE RID: 6334 RVA: 0x00071AAC File Offset: 0x00070AAC
		public long MaxLength
		{
			get
			{
				return this.maxLength;
			}
			set
			{
				this.maxLength = value;
			}
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x060018BF RID: 6335 RVA: 0x00071AB5 File Offset: 0x00070AB5
		// (set) Token: 0x060018C0 RID: 6336 RVA: 0x00071ABD File Offset: 0x00070ABD
		public long MinLength
		{
			get
			{
				return this.minLength;
			}
			set
			{
				this.minLength = value;
			}
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x060018C1 RID: 6337 RVA: 0x00071AC6 File Offset: 0x00070AC6
		// (set) Token: 0x060018C2 RID: 6338 RVA: 0x00071ACE File Offset: 0x00070ACE
		public bool IsDeclaredInExternal
		{
			get
			{
				return this.isDeclaredInExternal;
			}
			set
			{
				this.isDeclaredInExternal = value;
			}
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x060018C3 RID: 6339 RVA: 0x00071AD7 File Offset: 0x00070AD7
		// (set) Token: 0x060018C4 RID: 6340 RVA: 0x00071ADF File Offset: 0x00070ADF
		public XmlSchemaType SchemaType
		{
			get
			{
				return this.schemaType;
			}
			set
			{
				this.schemaType = value;
			}
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x060018C5 RID: 6341 RVA: 0x00071AE8 File Offset: 0x00070AE8
		// (set) Token: 0x060018C6 RID: 6342 RVA: 0x00071AF0 File Offset: 0x00070AF0
		public XmlSchemaDatatype Datatype
		{
			get
			{
				return this.datatype;
			}
			set
			{
				this.datatype = value;
			}
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x060018C7 RID: 6343 RVA: 0x00071AF9 File Offset: 0x00070AF9
		// (set) Token: 0x060018C8 RID: 6344 RVA: 0x00071B0F File Offset: 0x00070B0F
		public string DefaultValueRaw
		{
			get
			{
				if (this.defaultValueRaw == null)
				{
					return string.Empty;
				}
				return this.defaultValueRaw;
			}
			set
			{
				this.defaultValueRaw = value;
			}
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x060018C9 RID: 6345 RVA: 0x00071B18 File Offset: 0x00070B18
		// (set) Token: 0x060018CA RID: 6346 RVA: 0x00071B20 File Offset: 0x00070B20
		public object DefaultValueTyped
		{
			get
			{
				return this.defaultValueTyped;
			}
			set
			{
				this.defaultValueTyped = value;
			}
		}

		// Token: 0x060018CB RID: 6347 RVA: 0x00071B29 File Offset: 0x00070B29
		public bool CheckEnumeration(object pVal)
		{
			return (this.datatype.TokenizedType != XmlTokenizedType.NOTATION && this.datatype.TokenizedType != XmlTokenizedType.ENUMERATION) || this.values.Contains(pVal.ToString());
		}

		// Token: 0x060018CC RID: 6348 RVA: 0x00071B5B File Offset: 0x00070B5B
		public bool CheckValue(object pVal)
		{
			return (this.presence != SchemaDeclBase.Use.Fixed && this.presence != SchemaDeclBase.Use.RequiredFixed) || (this.defaultValueTyped != null && this.datatype.IsEqual(pVal, this.defaultValueTyped));
		}

		// Token: 0x04000E8D RID: 3725
		protected XmlQualifiedName name = XmlQualifiedName.Empty;

		// Token: 0x04000E8E RID: 3726
		protected string prefix;

		// Token: 0x04000E8F RID: 3727
		protected ArrayList values;

		// Token: 0x04000E90 RID: 3728
		protected XmlSchemaType schemaType;

		// Token: 0x04000E91 RID: 3729
		protected XmlSchemaDatatype datatype;

		// Token: 0x04000E92 RID: 3730
		protected bool isDeclaredInExternal;

		// Token: 0x04000E93 RID: 3731
		protected SchemaDeclBase.Use presence;

		// Token: 0x04000E94 RID: 3732
		protected string defaultValueRaw;

		// Token: 0x04000E95 RID: 3733
		protected object defaultValueTyped;

		// Token: 0x04000E96 RID: 3734
		protected long maxLength;

		// Token: 0x04000E97 RID: 3735
		protected long minLength;

		// Token: 0x0200020A RID: 522
		public enum Use
		{
			// Token: 0x04000E99 RID: 3737
			Default,
			// Token: 0x04000E9A RID: 3738
			Required,
			// Token: 0x04000E9B RID: 3739
			Implied,
			// Token: 0x04000E9C RID: 3740
			Fixed,
			// Token: 0x04000E9D RID: 3741
			RequiredFixed
		}
	}
}
