using System;
using System.Collections;
using System.IO;
using System.Text;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200001F RID: 31
	public class LdapAttributeSchema : LdapSchemaElement
	{
		// Token: 0x0600013C RID: 316 RVA: 0x0000706C File Offset: 0x0000606C
		private void InitBlock()
		{
			this.usage = 0;
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00007080 File Offset: 0x00006080
		public virtual string SyntaxString
		{
			get
			{
				return this.syntaxString;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00007098 File Offset: 0x00006098
		public virtual string Superior
		{
			get
			{
				return this.superior;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600013F RID: 319 RVA: 0x000070B0 File Offset: 0x000060B0
		public virtual bool SingleValued
		{
			get
			{
				return this.single;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000140 RID: 320 RVA: 0x000070C8 File Offset: 0x000060C8
		public virtual string EqualityMatchingRule
		{
			get
			{
				return this.equality;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000141 RID: 321 RVA: 0x000070E0 File Offset: 0x000060E0
		public virtual string OrderingMatchingRule
		{
			get
			{
				return this.ordering;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000142 RID: 322 RVA: 0x000070F8 File Offset: 0x000060F8
		public virtual string SubstringMatchingRule
		{
			get
			{
				return this.substring;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00007110 File Offset: 0x00006110
		public virtual bool Collective
		{
			get
			{
				return this.collective;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00007128 File Offset: 0x00006128
		public virtual bool UserModifiable
		{
			get
			{
				return this.userMod;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00007140 File Offset: 0x00006140
		public virtual int Usage
		{
			get
			{
				return this.usage;
			}
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00007158 File Offset: 0x00006158
		public LdapAttributeSchema(string[] names, string oid, string description, string syntaxString, bool single, string superior, bool obsolete, string equality, string ordering, string substring, bool collective, bool isUserModifiable, int usage) : base(LdapSchema.schemaTypeNames[0])
		{
			this.InitBlock();
			this.names = names;
			this.oid = oid;
			this.description = description;
			this.obsolete = obsolete;
			this.syntaxString = syntaxString;
			this.single = single;
			this.equality = equality;
			this.ordering = ordering;
			this.substring = substring;
			this.collective = collective;
			this.userMod = isUserModifiable;
			this.usage = usage;
			this.superior = superior;
			base.Value = this.formatString();
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00007200 File Offset: 0x00006200
		public LdapAttributeSchema(string raw) : base(LdapSchema.schemaTypeNames[0])
		{
			this.InitBlock();
			try
			{
				SchemaParser schemaParser = new SchemaParser(raw);
				if (schemaParser.Names != null)
				{
					this.names = schemaParser.Names;
				}
				if (schemaParser.ID != null)
				{
					this.oid = schemaParser.ID;
				}
				if (schemaParser.Description != null)
				{
					this.description = schemaParser.Description;
				}
				if (schemaParser.Syntax != null)
				{
					this.syntaxString = schemaParser.Syntax;
				}
				if (schemaParser.Superior != null)
				{
					this.superior = schemaParser.Superior;
				}
				this.single = schemaParser.Single;
				this.obsolete = schemaParser.Obsolete;
				IEnumerator qualifiers = schemaParser.Qualifiers;
				while (qualifiers.MoveNext())
				{
					object obj = qualifiers.Current;
					AttributeQualifier attributeQualifier = (AttributeQualifier)obj;
					this.setQualifier(attributeQualifier.Name, attributeQualifier.Values);
				}
				base.Value = this.formatString();
			}
			catch (IOException ex)
			{
				throw new SystemException(ex.ToString());
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00007320 File Offset: 0x00006320
		protected internal override string formatString()
		{
			StringBuilder stringBuilder = new StringBuilder("( ");
			string text;
			if ((text = this.ID) != null)
			{
				stringBuilder.Append(text);
			}
			string[] array = this.Names;
			if (array != null)
			{
				stringBuilder.Append(" NAME ");
				if (array.Length == 1)
				{
					stringBuilder.Append("'" + array[0] + "'");
				}
				else
				{
					stringBuilder.Append("( ");
					for (int i = 0; i < array.Length; i++)
					{
						stringBuilder.Append(" '" + array[i] + "'");
					}
					stringBuilder.Append(" )");
				}
			}
			if ((text = this.Description) != null)
			{
				stringBuilder.Append(" DESC ");
				stringBuilder.Append("'" + text + "'");
			}
			if (this.Obsolete)
			{
				stringBuilder.Append(" OBSOLETE");
			}
			if ((text = this.Superior) != null)
			{
				stringBuilder.Append(" SUP ");
				stringBuilder.Append("'" + text + "'");
			}
			if ((text = this.EqualityMatchingRule) != null)
			{
				stringBuilder.Append(" EQUALITY ");
				stringBuilder.Append("'" + text + "'");
			}
			if ((text = this.OrderingMatchingRule) != null)
			{
				stringBuilder.Append(" ORDERING ");
				stringBuilder.Append("'" + text + "'");
			}
			if ((text = this.SubstringMatchingRule) != null)
			{
				stringBuilder.Append(" SUBSTR ");
				stringBuilder.Append("'" + text + "'");
			}
			if ((text = this.SyntaxString) != null)
			{
				stringBuilder.Append(" SYNTAX ");
				stringBuilder.Append(text);
			}
			if (this.SingleValued)
			{
				stringBuilder.Append(" SINGLE-VALUE");
			}
			if (this.Collective)
			{
				stringBuilder.Append(" COLLECTIVE");
			}
			if (!this.UserModifiable)
			{
				stringBuilder.Append(" NO-USER-MODIFICATION");
			}
			int num;
			if ((num = this.Usage) != 0)
			{
				switch (num)
				{
				case 1:
					stringBuilder.Append(" USAGE directoryOperation");
					break;
				case 2:
					stringBuilder.Append(" USAGE distributedOperation");
					break;
				case 3:
					stringBuilder.Append(" USAGE dSAOperation");
					break;
				}
			}
			IEnumerator qualifierNames = this.QualifierNames;
			while (qualifierNames.MoveNext())
			{
				object obj = qualifierNames.Current;
				text = (string)obj;
				if (text != null)
				{
					stringBuilder.Append(" " + text);
					array = this.getQualifier(text);
					if (array != null)
					{
						if (array.Length > 1)
						{
							stringBuilder.Append("(");
						}
						for (int j = 0; j < array.Length; j++)
						{
							stringBuilder.Append(" '" + array[j] + "'");
						}
						if (array.Length > 1)
						{
							stringBuilder.Append(" )");
						}
					}
				}
			}
			stringBuilder.Append(" )");
			return stringBuilder.ToString();
		}

		// Token: 0x040000B2 RID: 178
		public const int USER_APPLICATIONS = 0;

		// Token: 0x040000B3 RID: 179
		public const int DIRECTORY_OPERATION = 1;

		// Token: 0x040000B4 RID: 180
		public const int DISTRIBUTED_OPERATION = 2;

		// Token: 0x040000B5 RID: 181
		public const int DSA_OPERATION = 3;

		// Token: 0x040000B6 RID: 182
		private string syntaxString;

		// Token: 0x040000B7 RID: 183
		private bool single = false;

		// Token: 0x040000B8 RID: 184
		private string superior;

		// Token: 0x040000B9 RID: 185
		private string equality;

		// Token: 0x040000BA RID: 186
		private string ordering;

		// Token: 0x040000BB RID: 187
		private string substring;

		// Token: 0x040000BC RID: 188
		private bool collective = false;

		// Token: 0x040000BD RID: 189
		private bool userMod = true;

		// Token: 0x040000BE RID: 190
		private int usage;
	}
}
