using System;
using System.Collections;
using System.IO;
using System.Text;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000048 RID: 72
	public class LdapSyntaxSchema : LdapSchemaElement
	{
		// Token: 0x060002C7 RID: 711 RVA: 0x0000E83C File Offset: 0x0000D83C
		public LdapSyntaxSchema(string oid, string description) : base(LdapSchema.schemaTypeNames[2])
		{
			this.oid = oid;
			this.description = description;
			base.Value = this.formatString();
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000E874 File Offset: 0x0000D874
		public LdapSyntaxSchema(string raw) : base(LdapSchema.schemaTypeNames[2])
		{
			try
			{
				SchemaParser schemaParser = new SchemaParser(raw);
				if (schemaParser.ID != null)
				{
					this.oid = schemaParser.ID;
				}
				if (schemaParser.Description != null)
				{
					this.description = schemaParser.Description;
				}
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

		// Token: 0x060002C9 RID: 713 RVA: 0x0000E928 File Offset: 0x0000D928
		protected internal override string formatString()
		{
			StringBuilder stringBuilder = new StringBuilder("( ");
			string text;
			if ((text = this.ID) != null)
			{
				stringBuilder.Append(text);
			}
			if ((text = this.Description) != null)
			{
				stringBuilder.Append(" DESC ");
				stringBuilder.Append("'" + text + "'");
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
					if ((qualifier = this.getQualifier(text2)) != null && qualifier.Length > 1)
					{
						stringBuilder.Append("( ");
						for (int i = 0; i < qualifier.Length; i++)
						{
							if (i > 0)
							{
								stringBuilder.Append(" ");
							}
							stringBuilder.Append("'" + qualifier[i] + "'");
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
	}
}
