using System;
using System.IO;
using System.Text;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000037 RID: 55
	public class LdapMatchingRuleSchema : LdapSchemaElement
	{
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600023F RID: 575 RVA: 0x0000BAB0 File Offset: 0x0000AAB0
		public virtual string[] Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000240 RID: 576 RVA: 0x0000BAC8 File Offset: 0x0000AAC8
		public virtual string SyntaxString
		{
			get
			{
				return this.syntaxString;
			}
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000BAE0 File Offset: 0x0000AAE0
		public LdapMatchingRuleSchema(string[] names, string oid, string description, string[] attributes, bool obsolete, string syntaxString) : base(LdapSchema.schemaTypeNames[6])
		{
			this.names = new string[names.Length];
			names.CopyTo(this.names, 0);
			this.oid = oid;
			this.description = description;
			this.obsolete = obsolete;
			this.attributes = new string[attributes.Length];
			attributes.CopyTo(this.attributes, 0);
			this.syntaxString = syntaxString;
			base.Value = this.formatString();
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000BB60 File Offset: 0x0000AB60
		public LdapMatchingRuleSchema(string rawMatchingRule, string rawMatchingRuleUse) : base(LdapSchema.schemaTypeNames[6])
		{
			try
			{
				SchemaParser schemaParser = new SchemaParser(rawMatchingRule);
				this.names = new string[schemaParser.Names.Length];
				schemaParser.Names.CopyTo(this.names, 0);
				this.oid = schemaParser.ID;
				this.description = schemaParser.Description;
				this.obsolete = schemaParser.Obsolete;
				this.syntaxString = schemaParser.Syntax;
				if (rawMatchingRuleUse != null)
				{
					SchemaParser schemaParser2 = new SchemaParser(rawMatchingRuleUse);
					this.attributes = schemaParser2.Applies;
				}
				base.Value = this.formatString();
			}
			catch (IOException ex)
			{
			}
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000BC1C File Offset: 0x0000AC1C
		protected internal override string formatString()
		{
			StringBuilder stringBuilder = new StringBuilder("( ");
			string text;
			if ((text = this.ID) != null)
			{
				stringBuilder.Append(text);
			}
			string[] names = this.Names;
			if (names != null)
			{
				stringBuilder.Append(" NAME ");
				if (names.Length == 1)
				{
					stringBuilder.Append("'" + names[0] + "'");
				}
				else
				{
					stringBuilder.Append("( ");
					for (int i = 0; i < names.Length; i++)
					{
						stringBuilder.Append(" '" + names[i] + "'");
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
			if ((text = this.SyntaxString) != null)
			{
				stringBuilder.Append(" SYNTAX ");
				stringBuilder.Append(text);
			}
			stringBuilder.Append(" )");
			return stringBuilder.ToString();
		}

		// Token: 0x0400010F RID: 271
		private string syntaxString;

		// Token: 0x04000110 RID: 272
		private string[] attributes;
	}
}
