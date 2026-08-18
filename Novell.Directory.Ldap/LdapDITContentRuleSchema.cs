using System;
using System.Collections;
using System.IO;
using System.Text;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200002C RID: 44
	public class LdapDITContentRuleSchema : LdapSchemaElement
	{
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x0000A128 File Offset: 0x00009128
		public virtual string[] AuxiliaryClasses
		{
			get
			{
				return this.auxiliary;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x0000A140 File Offset: 0x00009140
		public virtual string[] RequiredAttributes
		{
			get
			{
				return this.required;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x0000A158 File Offset: 0x00009158
		public virtual string[] OptionalAttributes
		{
			get
			{
				return this.optional;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x0000A170 File Offset: 0x00009170
		public virtual string[] PrecludedAttributes
		{
			get
			{
				return this.precluded;
			}
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000A188 File Offset: 0x00009188
		public LdapDITContentRuleSchema(string[] names, string oid, string description, bool obsolete, string[] auxiliary, string[] required, string[] optional, string[] precluded) : base(LdapSchema.schemaTypeNames[4])
		{
			this.names = new string[names.Length];
			names.CopyTo(this.names, 0);
			this.oid = oid;
			this.description = description;
			this.obsolete = obsolete;
			this.auxiliary = auxiliary;
			this.required = required;
			this.optional = optional;
			this.precluded = precluded;
			base.Value = this.formatString();
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000A25C File Offset: 0x0000925C
		public LdapDITContentRuleSchema(string raw) : base(LdapSchema.schemaTypeNames[4])
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
					this.oid = schemaParser.ID;
				}
				if (schemaParser.Description != null)
				{
					this.description = schemaParser.Description;
				}
				if (schemaParser.Auxiliary != null)
				{
					this.auxiliary = new string[schemaParser.Auxiliary.Length];
					schemaParser.Auxiliary.CopyTo(this.auxiliary, 0);
				}
				if (schemaParser.Required != null)
				{
					this.required = new string[schemaParser.Required.Length];
					schemaParser.Required.CopyTo(this.required, 0);
				}
				if (schemaParser.Optional != null)
				{
					this.optional = new string[schemaParser.Optional.Length];
					schemaParser.Optional.CopyTo(this.optional, 0);
				}
				if (schemaParser.Precluded != null)
				{
					this.precluded = new string[schemaParser.Precluded.Length];
					schemaParser.Precluded.CopyTo(this.precluded, 0);
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

		// Token: 0x060001FB RID: 507 RVA: 0x0000A45C File Offset: 0x0000945C
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
			if ((array = this.AuxiliaryClasses) != null)
			{
				stringBuilder.Append(" AUX ");
				if (array.Length > 1)
				{
					stringBuilder.Append("( ");
				}
				for (int j = 0; j < array.Length; j++)
				{
					if (j > 0)
					{
						stringBuilder.Append(" $ ");
					}
					stringBuilder.Append(array[j]);
				}
				if (array.Length > 1)
				{
					stringBuilder.Append(" )");
				}
			}
			if ((array = this.RequiredAttributes) != null)
			{
				stringBuilder.Append(" MUST ");
				if (array.Length > 1)
				{
					stringBuilder.Append("( ");
				}
				for (int k = 0; k < array.Length; k++)
				{
					if (k > 0)
					{
						stringBuilder.Append(" $ ");
					}
					stringBuilder.Append(array[k]);
				}
				if (array.Length > 1)
				{
					stringBuilder.Append(" )");
				}
			}
			if ((array = this.OptionalAttributes) != null)
			{
				stringBuilder.Append(" MAY ");
				if (array.Length > 1)
				{
					stringBuilder.Append("( ");
				}
				for (int l = 0; l < array.Length; l++)
				{
					if (l > 0)
					{
						stringBuilder.Append(" $ ");
					}
					stringBuilder.Append(array[l]);
				}
				if (array.Length > 1)
				{
					stringBuilder.Append(" )");
				}
			}
			if ((array = this.PrecludedAttributes) != null)
			{
				stringBuilder.Append(" NOT ");
				if (array.Length > 1)
				{
					stringBuilder.Append("( ");
				}
				for (int m = 0; m < array.Length; m++)
				{
					if (m > 0)
					{
						stringBuilder.Append(" $ ");
					}
					stringBuilder.Append(array[m]);
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
						for (int n = 0; n < qualifier.Length; n++)
						{
							if (n > 0)
							{
								stringBuilder.Append(" ");
							}
							stringBuilder.Append("'" + qualifier[n] + "'");
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

		// Token: 0x040000E5 RID: 229
		private string[] auxiliary = new string[]
		{
			""
		};

		// Token: 0x040000E6 RID: 230
		private string[] required = new string[]
		{
			""
		};

		// Token: 0x040000E7 RID: 231
		private string[] optional = new string[]
		{
			""
		};

		// Token: 0x040000E8 RID: 232
		private string[] precluded = new string[]
		{
			""
		};
	}
}
