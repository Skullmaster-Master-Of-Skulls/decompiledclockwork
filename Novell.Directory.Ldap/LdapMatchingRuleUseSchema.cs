using System;
using System.IO;
using System.Text;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000038 RID: 56
	public class LdapMatchingRuleUseSchema : LdapSchemaElement
	{
		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000244 RID: 580 RVA: 0x0000BD34 File Offset: 0x0000AD34
		public virtual string[] Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000BD4C File Offset: 0x0000AD4C
		public LdapMatchingRuleUseSchema(string[] names, string oid, string description, bool obsolete, string[] attributes) : base(LdapSchema.schemaTypeNames[7])
		{
			this.names = new string[names.Length];
			names.CopyTo(this.names, 0);
			this.oid = oid;
			this.description = description;
			this.obsolete = obsolete;
			this.attributes = new string[attributes.Length];
			attributes.CopyTo(this.attributes, 0);
			base.Value = this.formatString();
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000BDC4 File Offset: 0x0000ADC4
		public LdapMatchingRuleUseSchema(string raw) : base(LdapSchema.schemaTypeNames[7])
		{
			try
			{
				SchemaParser schemaParser = new SchemaParser(raw);
				this.names = new string[schemaParser.Names.Length];
				schemaParser.Names.CopyTo(this.names, 0);
				this.oid = schemaParser.ID;
				this.description = schemaParser.Description;
				this.obsolete = schemaParser.Obsolete;
				this.attributes = schemaParser.Applies;
				base.Value = this.formatString();
			}
			catch (IOException ex)
			{
			}
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000BE6C File Offset: 0x0000AE6C
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
			if ((names = this.Attributes) != null)
			{
				stringBuilder.Append(" APPLIES ");
				if (names.Length > 1)
				{
					stringBuilder.Append("( ");
				}
				for (int j = 0; j < names.Length; j++)
				{
					if (j > 0)
					{
						stringBuilder.Append(" $ ");
					}
					stringBuilder.Append(names[j]);
				}
				if (names.Length > 1)
				{
					stringBuilder.Append(" )");
				}
			}
			stringBuilder.Append(" )");
			return stringBuilder.ToString();
		}

		// Token: 0x04000111 RID: 273
		private string[] attributes;
	}
}
