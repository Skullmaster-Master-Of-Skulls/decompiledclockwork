using System;
using System.Collections;
using System.IO;
using System.Text;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200002D RID: 45
	public class LdapDITStructureRuleSchema : LdapSchemaElement
	{
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001FC RID: 508 RVA: 0x0000A7AC File Offset: 0x000097AC
		public virtual int RuleID
		{
			get
			{
				return this.ruleID;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001FD RID: 509 RVA: 0x0000A7C4 File Offset: 0x000097C4
		public virtual string NameForm
		{
			get
			{
				return this.nameForm;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001FE RID: 510 RVA: 0x0000A7DC File Offset: 0x000097DC
		public virtual string[] Superiors
		{
			get
			{
				return this.superiorIDs;
			}
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000A7F4 File Offset: 0x000097F4
		public LdapDITStructureRuleSchema(string[] names, int ruleID, string description, bool obsolete, string nameForm, string[] superiorIDs) : base(LdapSchema.schemaTypeNames[5])
		{
			this.names = new string[names.Length];
			names.CopyTo(this.names, 0);
			this.ruleID = ruleID;
			this.description = description;
			this.obsolete = obsolete;
			this.nameForm = nameForm;
			this.superiorIDs = superiorIDs;
			base.Value = this.formatString();
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000A888 File Offset: 0x00009888
		public LdapDITStructureRuleSchema(string raw) : base(LdapSchema.schemaTypeNames[5])
		{
			this.obsolete = false;
			try
			{
				SchemaParser schemaParser = new SchemaParser(raw);
				if (schemaParser.Names != null)
				{
					this.names = new string[schemaParser.Names.Length];
					schemaParser.Names.CopyTo(this.names, 0);
				}
				if (schemaParser.ID != null)
				{
					this.ruleID = int.Parse(schemaParser.ID);
				}
				if (schemaParser.Description != null)
				{
					this.description = schemaParser.Description;
				}
				if (schemaParser.Superiors != null)
				{
					this.superiorIDs = new string[schemaParser.Superiors.Length];
					schemaParser.Superiors.CopyTo(this.superiorIDs, 0);
				}
				if (schemaParser.NameForm != null)
				{
					this.nameForm = schemaParser.NameForm;
				}
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
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000A9E0 File Offset: 0x000099E0
		protected internal override string formatString()
		{
			StringBuilder stringBuilder = new StringBuilder("( ");
			string text = this.RuleID.ToString();
			stringBuilder.Append(text);
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
			if ((text = this.NameForm) != null)
			{
				stringBuilder.Append(" FORM ");
				stringBuilder.Append("'" + text + "'");
			}
			if ((array = this.Superiors) != null)
			{
				stringBuilder.Append(" SUP ");
				if (array.Length > 1)
				{
					stringBuilder.Append("( ");
				}
				for (int j = 0; j < array.Length; j++)
				{
					if (j > 0)
					{
						stringBuilder.Append(" ");
					}
					stringBuilder.Append(array[j]);
				}
				if (array.Length > 1)
				{
					stringBuilder.Append(" )");
				}
			}
			IEnumerator qualifierNames;
			if ((qualifierNames = this.QualifierNames) != null)
			{
				while (qualifierNames.MoveNext())
				{
					object obj = qualifierNames.Current;
					string text2 = (string)obj;
					stringBuilder.Append(" " + text2 + " ");
					string[] qualifier;
					if ((qualifier = this.getQualifier(text2)) != null)
					{
						if (qualifier.Length > 1)
						{
							stringBuilder.Append("( ");
						}
						for (int k = 0; k < qualifier.Length; k++)
						{
							if (k > 0)
							{
								stringBuilder.Append(" ");
							}
							stringBuilder.Append("'" + qualifier[k] + "'");
						}
						if (qualifier.Length > 1)
						{
							stringBuilder.Append(" )");
						}
					}
				}
			}
			stringBuilder.Append(" )");
			return stringBuilder.ToString();
		}

		// Token: 0x040000E9 RID: 233
		private int ruleID = 0;

		// Token: 0x040000EA RID: 234
		private string nameForm = "";

		// Token: 0x040000EB RID: 235
		private string[] superiorIDs = new string[]
		{
			""
		};
	}
}
