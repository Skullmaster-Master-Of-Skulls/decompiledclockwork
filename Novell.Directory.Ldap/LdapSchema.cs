using System;
using System.Collections;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000041 RID: 65
	public class LdapSchema : LdapEntry
	{
		// Token: 0x0600027E RID: 638 RVA: 0x0000D36C File Offset: 0x0000C36C
		private void InitBlock()
		{
			this.nameTable = new Hashtable[8];
			this.idTable = new Hashtable[8];
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0000D394 File Offset: 0x0000C394
		public virtual IEnumerator AttributeSchemas
		{
			get
			{
				return new EnumeratedIterator(this.idTable[0].Values.GetEnumerator());
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000280 RID: 640 RVA: 0x0000D3BC File Offset: 0x0000C3BC
		public virtual IEnumerator DITContentRuleSchemas
		{
			get
			{
				return new EnumeratedIterator(this.idTable[4].Values.GetEnumerator());
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000281 RID: 641 RVA: 0x0000D3E4 File Offset: 0x0000C3E4
		public virtual IEnumerator DITStructureRuleSchemas
		{
			get
			{
				return new EnumeratedIterator(this.idTable[5].Values.GetEnumerator());
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000282 RID: 642 RVA: 0x0000D40C File Offset: 0x0000C40C
		public virtual IEnumerator MatchingRuleSchemas
		{
			get
			{
				return new EnumeratedIterator(this.idTable[6].Values.GetEnumerator());
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000283 RID: 643 RVA: 0x0000D434 File Offset: 0x0000C434
		public virtual IEnumerator MatchingRuleUseSchemas
		{
			get
			{
				return new EnumeratedIterator(this.idTable[7].Values.GetEnumerator());
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000284 RID: 644 RVA: 0x0000D45C File Offset: 0x0000C45C
		public virtual IEnumerator NameFormSchemas
		{
			get
			{
				return new EnumeratedIterator(this.idTable[3].Values.GetEnumerator());
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000285 RID: 645 RVA: 0x0000D484 File Offset: 0x0000C484
		public virtual IEnumerator ObjectClassSchemas
		{
			get
			{
				return new EnumeratedIterator(this.idTable[1].Values.GetEnumerator());
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000286 RID: 646 RVA: 0x0000D4AC File Offset: 0x0000C4AC
		public virtual IEnumerator SyntaxSchemas
		{
			get
			{
				return new EnumeratedIterator(this.idTable[2].Values.GetEnumerator());
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000287 RID: 647 RVA: 0x0000D4D4 File Offset: 0x0000C4D4
		public virtual IEnumerator AttributeNames
		{
			get
			{
				return new EnumeratedIterator(new SupportClass.SetSupport(this.nameTable[0].Keys).GetEnumerator());
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000288 RID: 648 RVA: 0x0000D504 File Offset: 0x0000C504
		public virtual IEnumerator DITContentRuleNames
		{
			get
			{
				return new EnumeratedIterator(new SupportClass.SetSupport(this.nameTable[4].Keys).GetEnumerator());
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000289 RID: 649 RVA: 0x0000D534 File Offset: 0x0000C534
		public virtual IEnumerator DITStructureRuleNames
		{
			get
			{
				return new EnumeratedIterator(new SupportClass.SetSupport(this.nameTable[5].Keys).GetEnumerator());
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600028A RID: 650 RVA: 0x0000D564 File Offset: 0x0000C564
		public virtual IEnumerator MatchingRuleNames
		{
			get
			{
				return new EnumeratedIterator(new SupportClass.SetSupport(this.nameTable[6].Keys).GetEnumerator());
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600028B RID: 651 RVA: 0x0000D594 File Offset: 0x0000C594
		public virtual IEnumerator MatchingRuleUseNames
		{
			get
			{
				return new EnumeratedIterator(new SupportClass.SetSupport(this.nameTable[7].Keys).GetEnumerator());
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600028C RID: 652 RVA: 0x0000D5C4 File Offset: 0x0000C5C4
		public virtual IEnumerator NameFormNames
		{
			get
			{
				return new EnumeratedIterator(new SupportClass.SetSupport(this.nameTable[3].Keys).GetEnumerator());
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0000D5F4 File Offset: 0x0000C5F4
		public virtual IEnumerator ObjectClassNames
		{
			get
			{
				return new EnumeratedIterator(new SupportClass.SetSupport(this.nameTable[1].Keys).GetEnumerator());
			}
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000D624 File Offset: 0x0000C624
		public LdapSchema(LdapEntry ent) : base(ent.DN, ent.getAttributeSet())
		{
			this.InitBlock();
			for (int i = 0; i < LdapSchema.schemaTypeNames.Length; i++)
			{
				this.idTable[i] = new Hashtable();
				this.nameTable[i] = new Hashtable();
			}
			foreach (object obj in base.getAttributeSet())
			{
				LdapAttribute ldapAttribute = (LdapAttribute)obj;
				string name = ldapAttribute.Name;
				IEnumerator stringValues = ldapAttribute.StringValues;
				if (name.ToUpper().Equals(LdapSchema.schemaTypeNames[1].ToUpper()))
				{
					while (stringValues.MoveNext())
					{
						object obj2 = stringValues.Current;
						string text = (string)obj2;
						LdapObjectClassSchema element;
						try
						{
							element = new LdapObjectClassSchema(text);
						}
						catch (Exception ex)
						{
							continue;
						}
						this.addElement(1, element);
					}
				}
				else if (name.ToUpper().Equals(LdapSchema.schemaTypeNames[0].ToUpper()))
				{
					while (stringValues.MoveNext())
					{
						object obj3 = stringValues.Current;
						string text = (string)obj3;
						LdapAttributeSchema element2;
						try
						{
							element2 = new LdapAttributeSchema(text);
						}
						catch (Exception ex2)
						{
							continue;
						}
						this.addElement(0, element2);
					}
				}
				else if (name.ToUpper().Equals(LdapSchema.schemaTypeNames[2].ToUpper()))
				{
					while (stringValues.MoveNext())
					{
						object obj4 = stringValues.Current;
						string text = (string)obj4;
						LdapSyntaxSchema element3 = new LdapSyntaxSchema(text);
						this.addElement(2, element3);
					}
				}
				else if (name.ToUpper().Equals(LdapSchema.schemaTypeNames[6].ToUpper()))
				{
					while (stringValues.MoveNext())
					{
						object obj5 = stringValues.Current;
						string text = (string)obj5;
						LdapMatchingRuleSchema element4 = new LdapMatchingRuleSchema(text, null);
						this.addElement(6, element4);
					}
				}
				else if (name.ToUpper().Equals(LdapSchema.schemaTypeNames[7].ToUpper()))
				{
					while (stringValues.MoveNext())
					{
						object obj6 = stringValues.Current;
						string text = (string)obj6;
						LdapMatchingRuleUseSchema element5 = new LdapMatchingRuleUseSchema(text);
						this.addElement(7, element5);
					}
				}
				else if (name.ToUpper().Equals(LdapSchema.schemaTypeNames[4].ToUpper()))
				{
					while (stringValues.MoveNext())
					{
						object obj7 = stringValues.Current;
						string text = (string)obj7;
						LdapDITContentRuleSchema element6 = new LdapDITContentRuleSchema(text);
						this.addElement(4, element6);
					}
				}
				else if (name.ToUpper().Equals(LdapSchema.schemaTypeNames[5].ToUpper()))
				{
					while (stringValues.MoveNext())
					{
						object obj8 = stringValues.Current;
						string text = (string)obj8;
						LdapDITStructureRuleSchema element7 = new LdapDITStructureRuleSchema(text);
						this.addElement(5, element7);
					}
				}
				else if (name.ToUpper().Equals(LdapSchema.schemaTypeNames[3].ToUpper()))
				{
					while (stringValues.MoveNext())
					{
						object obj9 = stringValues.Current;
						string text = (string)obj9;
						LdapNameFormSchema element8 = new LdapNameFormSchema(text);
						this.addElement(3, element8);
					}
				}
			}
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000D928 File Offset: 0x0000C928
		private void addElement(int schemaType, LdapSchemaElement element)
		{
			SupportClass.PutElement(this.idTable[schemaType], element.ID, element);
			string[] names = element.Names;
			for (int i = 0; i < names.Length; i++)
			{
				SupportClass.PutElement(this.nameTable[schemaType], names[i].ToUpper(), element);
			}
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000D978 File Offset: 0x0000C978
		private LdapSchemaElement getSchemaElement(int schemaType, string key)
		{
			LdapSchemaElement result;
			if (key == null || key.ToUpper().Equals("".ToUpper()))
			{
				result = null;
			}
			else
			{
				char c = key[0];
				if (c >= '0' && c <= '9')
				{
					result = (LdapSchemaElement)this.idTable[schemaType][key];
				}
				else
				{
					result = (LdapSchemaElement)this.nameTable[schemaType][key.ToUpper()];
				}
			}
			return result;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000D9E8 File Offset: 0x0000C9E8
		public virtual LdapAttributeSchema getAttributeSchema(string name)
		{
			return (LdapAttributeSchema)this.getSchemaElement(0, name);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000DA08 File Offset: 0x0000CA08
		public virtual LdapDITContentRuleSchema getDITContentRuleSchema(string name)
		{
			return (LdapDITContentRuleSchema)this.getSchemaElement(4, name);
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000DA28 File Offset: 0x0000CA28
		public virtual LdapDITStructureRuleSchema getDITStructureRuleSchema(string name)
		{
			return (LdapDITStructureRuleSchema)this.getSchemaElement(5, name);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000DA48 File Offset: 0x0000CA48
		public virtual LdapDITStructureRuleSchema getDITStructureRuleSchema(int ID)
		{
			return (LdapDITStructureRuleSchema)this.idTable[5][ID];
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000DA74 File Offset: 0x0000CA74
		public virtual LdapMatchingRuleSchema getMatchingRuleSchema(string name)
		{
			return (LdapMatchingRuleSchema)this.getSchemaElement(6, name);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000DA94 File Offset: 0x0000CA94
		public virtual LdapMatchingRuleUseSchema getMatchingRuleUseSchema(string name)
		{
			return (LdapMatchingRuleUseSchema)this.getSchemaElement(7, name);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000DAB4 File Offset: 0x0000CAB4
		public virtual LdapNameFormSchema getNameFormSchema(string name)
		{
			return (LdapNameFormSchema)this.getSchemaElement(3, name);
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000DAD4 File Offset: 0x0000CAD4
		public virtual LdapObjectClassSchema getObjectClassSchema(string name)
		{
			return (LdapObjectClassSchema)this.getSchemaElement(1, name);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000DAF4 File Offset: 0x0000CAF4
		public virtual LdapSyntaxSchema getSyntaxSchema(string oid)
		{
			return (LdapSyntaxSchema)this.getSchemaElement(2, oid);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000DB14 File Offset: 0x0000CB14
		private int getType(LdapSchemaElement element)
		{
			int result;
			if (element is LdapAttributeSchema)
			{
				result = 0;
			}
			else if (element is LdapObjectClassSchema)
			{
				result = 1;
			}
			else if (element is LdapSyntaxSchema)
			{
				result = 2;
			}
			else if (element is LdapNameFormSchema)
			{
				result = 3;
			}
			else if (element is LdapMatchingRuleSchema)
			{
				result = 6;
			}
			else if (element is LdapMatchingRuleUseSchema)
			{
				result = 7;
			}
			else if (element is LdapDITContentRuleSchema)
			{
				result = 4;
			}
			else
			{
				if (!(element is LdapDITStructureRuleSchema))
				{
					throw new ArgumentException("The specified schema element type is not recognized");
				}
				result = 5;
			}
			return result;
		}

		// Token: 0x04000127 RID: 295
		internal const int ATTRIBUTE = 0;

		// Token: 0x04000128 RID: 296
		internal const int OBJECT_CLASS = 1;

		// Token: 0x04000129 RID: 297
		internal const int SYNTAX = 2;

		// Token: 0x0400012A RID: 298
		internal const int NAME_FORM = 3;

		// Token: 0x0400012B RID: 299
		internal const int DITCONTENT = 4;

		// Token: 0x0400012C RID: 300
		internal const int DITSTRUCTURE = 5;

		// Token: 0x0400012D RID: 301
		internal const int MATCHING = 6;

		// Token: 0x0400012E RID: 302
		internal const int MATCHING_USE = 7;

		// Token: 0x0400012F RID: 303
		private Hashtable[] idTable;

		// Token: 0x04000130 RID: 304
		private Hashtable[] nameTable;

		// Token: 0x04000131 RID: 305
		internal static readonly string[] schemaTypeNames = new string[]
		{
			"attributeTypes",
			"objectClasses",
			"ldapSyntaxes",
			"nameForms",
			"dITContentRules",
			"dITStructureRules",
			"matchingRules",
			"matchingRuleUse"
		};
	}
}
