using System;
using System.Collections;
using System.IO;
using System.Text;

namespace Novell.Directory.Ldap.Utilclass
{
	// Token: 0x020000FA RID: 250
	public class SchemaParser
	{
		// Token: 0x06000614 RID: 1556 RVA: 0x0001DC64 File Offset: 0x0001CC64
		private void InitBlock()
		{
			this.usage = 0;
			this.qualifiers = new ArrayList();
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x0001DC84 File Offset: 0x0001CC84
		// (set) Token: 0x06000616 RID: 1558 RVA: 0x0001DC9C File Offset: 0x0001CC9C
		public virtual string RawString
		{
			get
			{
				return this.rawString;
			}
			set
			{
				this.rawString = value;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x0001DCB0 File Offset: 0x0001CCB0
		public virtual string[] Names
		{
			get
			{
				return this.names;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000618 RID: 1560 RVA: 0x0001DCC8 File Offset: 0x0001CCC8
		public virtual IEnumerator Qualifiers
		{
			get
			{
				return new ArrayEnumeration(this.qualifiers.ToArray());
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000619 RID: 1561 RVA: 0x0001DCEC File Offset: 0x0001CCEC
		public virtual string ID
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x0001DD04 File Offset: 0x0001CD04
		public virtual string Description
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x0600061B RID: 1563 RVA: 0x0001DD1C File Offset: 0x0001CD1C
		public virtual string Syntax
		{
			get
			{
				return this.syntax;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x0600061C RID: 1564 RVA: 0x0001DD34 File Offset: 0x0001CD34
		public virtual string Superior
		{
			get
			{
				return this.superior;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x0001DD4C File Offset: 0x0001CD4C
		public virtual bool Single
		{
			get
			{
				return this.single;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600061E RID: 1566 RVA: 0x0001DD64 File Offset: 0x0001CD64
		public virtual bool Obsolete
		{
			get
			{
				return this.obsolete;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x0600061F RID: 1567 RVA: 0x0001DD7C File Offset: 0x0001CD7C
		public virtual string Equality
		{
			get
			{
				return this.equality;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000620 RID: 1568 RVA: 0x0001DD94 File Offset: 0x0001CD94
		public virtual string Ordering
		{
			get
			{
				return this.ordering;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000621 RID: 1569 RVA: 0x0001DDAC File Offset: 0x0001CDAC
		public virtual string Substring
		{
			get
			{
				return this.substring;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000622 RID: 1570 RVA: 0x0001DDC4 File Offset: 0x0001CDC4
		public virtual bool Collective
		{
			get
			{
				return this.collective;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000623 RID: 1571 RVA: 0x0001DDDC File Offset: 0x0001CDDC
		public virtual bool UserMod
		{
			get
			{
				return this.userMod;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000624 RID: 1572 RVA: 0x0001DDF4 File Offset: 0x0001CDF4
		public virtual int Usage
		{
			get
			{
				return this.usage;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000625 RID: 1573 RVA: 0x0001DE0C File Offset: 0x0001CE0C
		public virtual int Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000626 RID: 1574 RVA: 0x0001DE24 File Offset: 0x0001CE24
		public virtual string[] Superiors
		{
			get
			{
				return this.superiors;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000627 RID: 1575 RVA: 0x0001DE3C File Offset: 0x0001CE3C
		public virtual string[] Required
		{
			get
			{
				return this.required;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000628 RID: 1576 RVA: 0x0001DE54 File Offset: 0x0001CE54
		public virtual string[] Optional
		{
			get
			{
				return this.optional;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x0001DE6C File Offset: 0x0001CE6C
		public virtual string[] Auxiliary
		{
			get
			{
				return this.auxiliary;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x0600062A RID: 1578 RVA: 0x0001DE84 File Offset: 0x0001CE84
		public virtual string[] Precluded
		{
			get
			{
				return this.precluded;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x0001DE9C File Offset: 0x0001CE9C
		public virtual string[] Applies
		{
			get
			{
				return this.applies;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x0001DEB4 File Offset: 0x0001CEB4
		public virtual string NameForm
		{
			get
			{
				return this.nameForm;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x0001DECC File Offset: 0x0001CECC
		public virtual string ObjectClass
		{
			get
			{
				return this.nameForm;
			}
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x0001DEE4 File Offset: 0x0001CEE4
		public SchemaParser(string aString)
		{
			this.InitBlock();
			int num;
			if ((num = aString.IndexOf('\\')) != -1)
			{
				StringBuilder stringBuilder = new StringBuilder(aString.Substring(0, num));
				for (int i = num; i < aString.Length; i++)
				{
					stringBuilder.Append(aString[i]);
					if (aString[i] == '\\')
					{
						stringBuilder.Append('\\');
					}
				}
				this.rawString = stringBuilder.ToString();
			}
			else
			{
				this.rawString = aString;
			}
			SchemaTokenCreator schemaTokenCreator = new SchemaTokenCreator(new StringReader(this.rawString));
			schemaTokenCreator.OrdinaryCharacter(46);
			schemaTokenCreator.OrdinaryCharacters(48, 57);
			schemaTokenCreator.OrdinaryCharacter(123);
			schemaTokenCreator.OrdinaryCharacter(125);
			schemaTokenCreator.OrdinaryCharacter(95);
			schemaTokenCreator.OrdinaryCharacter(59);
			schemaTokenCreator.WordCharacters(46, 57);
			schemaTokenCreator.WordCharacters(123, 125);
			schemaTokenCreator.WordCharacters(95, 95);
			schemaTokenCreator.WordCharacters(59, 59);
			try
			{
				if (-1 != schemaTokenCreator.nextToken() && schemaTokenCreator.lastttype == 40)
				{
					if (-3 == schemaTokenCreator.nextToken())
					{
						this.id = schemaTokenCreator.StringValue;
					}
					while (-1 != schemaTokenCreator.nextToken())
					{
						if (schemaTokenCreator.lastttype == -3)
						{
							if (schemaTokenCreator.StringValue.ToUpper().Equals("NAME".ToUpper()))
							{
								if (schemaTokenCreator.nextToken() == 39)
								{
									this.names = new string[1];
									this.names[0] = schemaTokenCreator.StringValue;
								}
								else if (schemaTokenCreator.lastttype == 40)
								{
									ArrayList arrayList = new ArrayList();
									while (schemaTokenCreator.nextToken() == 39)
									{
										if (schemaTokenCreator.StringValue != null)
										{
											arrayList.Add(schemaTokenCreator.StringValue);
										}
									}
									if (arrayList.Count > 0)
									{
										this.names = new string[arrayList.Count];
										SupportClass.ArrayListSupport.ToArray(arrayList, this.names);
									}
								}
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("DESC".ToUpper()))
							{
								if (schemaTokenCreator.nextToken() == 39)
								{
									this.description = schemaTokenCreator.StringValue;
								}
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("SYNTAX".ToUpper()))
							{
								this.result = schemaTokenCreator.nextToken();
								if (this.result == -3 || this.result == 39)
								{
									this.syntax = schemaTokenCreator.StringValue;
								}
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("EQUALITY".ToUpper()))
							{
								if (schemaTokenCreator.nextToken() == -3)
								{
									this.equality = schemaTokenCreator.StringValue;
								}
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("ORDERING".ToUpper()))
							{
								if (schemaTokenCreator.nextToken() == -3)
								{
									this.ordering = schemaTokenCreator.StringValue;
								}
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("SUBSTR".ToUpper()))
							{
								if (schemaTokenCreator.nextToken() == -3)
								{
									this.substring = schemaTokenCreator.StringValue;
								}
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("FORM".ToUpper()))
							{
								if (schemaTokenCreator.nextToken() == -3)
								{
									this.nameForm = schemaTokenCreator.StringValue;
								}
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("OC".ToUpper()))
							{
								if (schemaTokenCreator.nextToken() == -3)
								{
									this.objectClass = schemaTokenCreator.StringValue;
								}
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("SUP".ToUpper()))
							{
								ArrayList arrayList2 = new ArrayList();
								schemaTokenCreator.nextToken();
								if (schemaTokenCreator.lastttype == 40)
								{
									schemaTokenCreator.nextToken();
									while (schemaTokenCreator.lastttype != 41)
									{
										if (schemaTokenCreator.lastttype != 36)
										{
											arrayList2.Add(schemaTokenCreator.StringValue);
										}
										schemaTokenCreator.nextToken();
									}
								}
								else
								{
									arrayList2.Add(schemaTokenCreator.StringValue);
									this.superior = schemaTokenCreator.StringValue;
								}
								if (arrayList2.Count > 0)
								{
									this.superiors = new string[arrayList2.Count];
									SupportClass.ArrayListSupport.ToArray(arrayList2, this.superiors);
								}
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("SINGLE-VALUE".ToUpper()))
							{
								this.single = true;
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("OBSOLETE".ToUpper()))
							{
								this.obsolete = true;
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("COLLECTIVE".ToUpper()))
							{
								this.collective = true;
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("NO-USER-MODIFICATION".ToUpper()))
							{
								this.userMod = false;
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("MUST".ToUpper()))
							{
								ArrayList arrayList3 = new ArrayList();
								schemaTokenCreator.nextToken();
								if (schemaTokenCreator.lastttype == 40)
								{
									schemaTokenCreator.nextToken();
									while (schemaTokenCreator.lastttype != 41)
									{
										if (schemaTokenCreator.lastttype != 36)
										{
											arrayList3.Add(schemaTokenCreator.StringValue);
										}
										schemaTokenCreator.nextToken();
									}
								}
								else
								{
									arrayList3.Add(schemaTokenCreator.StringValue);
								}
								if (arrayList3.Count > 0)
								{
									this.required = new string[arrayList3.Count];
									SupportClass.ArrayListSupport.ToArray(arrayList3, this.required);
								}
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("MAY".ToUpper()))
							{
								ArrayList arrayList4 = new ArrayList();
								schemaTokenCreator.nextToken();
								if (schemaTokenCreator.lastttype == 40)
								{
									schemaTokenCreator.nextToken();
									while (schemaTokenCreator.lastttype != 41)
									{
										if (schemaTokenCreator.lastttype != 36)
										{
											arrayList4.Add(schemaTokenCreator.StringValue);
										}
										schemaTokenCreator.nextToken();
									}
								}
								else
								{
									arrayList4.Add(schemaTokenCreator.StringValue);
								}
								if (arrayList4.Count > 0)
								{
									this.optional = new string[arrayList4.Count];
									SupportClass.ArrayListSupport.ToArray(arrayList4, this.optional);
								}
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("NOT".ToUpper()))
							{
								ArrayList arrayList5 = new ArrayList();
								schemaTokenCreator.nextToken();
								if (schemaTokenCreator.lastttype == 40)
								{
									schemaTokenCreator.nextToken();
									while (schemaTokenCreator.lastttype != 41)
									{
										if (schemaTokenCreator.lastttype != 36)
										{
											arrayList5.Add(schemaTokenCreator.StringValue);
										}
										schemaTokenCreator.nextToken();
									}
								}
								else
								{
									arrayList5.Add(schemaTokenCreator.StringValue);
								}
								if (arrayList5.Count > 0)
								{
									this.precluded = new string[arrayList5.Count];
									SupportClass.ArrayListSupport.ToArray(arrayList5, this.precluded);
								}
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("AUX".ToUpper()))
							{
								ArrayList arrayList6 = new ArrayList();
								schemaTokenCreator.nextToken();
								if (schemaTokenCreator.lastttype == 40)
								{
									schemaTokenCreator.nextToken();
									while (schemaTokenCreator.lastttype != 41)
									{
										if (schemaTokenCreator.lastttype != 36)
										{
											arrayList6.Add(schemaTokenCreator.StringValue);
										}
										schemaTokenCreator.nextToken();
									}
								}
								else
								{
									arrayList6.Add(schemaTokenCreator.StringValue);
								}
								if (arrayList6.Count > 0)
								{
									this.auxiliary = new string[arrayList6.Count];
									SupportClass.ArrayListSupport.ToArray(arrayList6, this.auxiliary);
								}
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("ABSTRACT".ToUpper()))
							{
								this.type = 0;
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("STRUCTURAL".ToUpper()))
							{
								this.type = 1;
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("AUXILIARY".ToUpper()))
							{
								this.type = 2;
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("USAGE".ToUpper()))
							{
								if (schemaTokenCreator.nextToken() == -3)
								{
									string stringValue = schemaTokenCreator.StringValue;
									if (stringValue.ToUpper().Equals("directoryOperation".ToUpper()))
									{
										this.usage = 1;
									}
									else if (stringValue.ToUpper().Equals("distributedOperation".ToUpper()))
									{
										this.usage = 2;
									}
									else if (stringValue.ToUpper().Equals("dSAOperation".ToUpper()))
									{
										this.usage = 3;
									}
									else if (stringValue.ToUpper().Equals("userApplications".ToUpper()))
									{
										this.usage = 0;
									}
								}
							}
							else if (schemaTokenCreator.StringValue.ToUpper().Equals("APPLIES".ToUpper()))
							{
								ArrayList arrayList7 = new ArrayList();
								schemaTokenCreator.nextToken();
								if (schemaTokenCreator.lastttype == 40)
								{
									schemaTokenCreator.nextToken();
									while (schemaTokenCreator.lastttype != 41)
									{
										if (schemaTokenCreator.lastttype != 36)
										{
											arrayList7.Add(schemaTokenCreator.StringValue);
										}
										schemaTokenCreator.nextToken();
									}
								}
								else
								{
									arrayList7.Add(schemaTokenCreator.StringValue);
								}
								if (arrayList7.Count > 0)
								{
									this.applies = new string[arrayList7.Count];
									SupportClass.ArrayListSupport.ToArray(arrayList7, this.applies);
								}
							}
							else
							{
								string stringValue = schemaTokenCreator.StringValue;
								AttributeQualifier attributeQualifier = this.parseQualifier(schemaTokenCreator, stringValue);
								if (attributeQualifier != null)
								{
									this.qualifiers.Add(attributeQualifier);
								}
							}
						}
					}
				}
			}
			catch (IOException ex)
			{
				throw ex;
			}
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x0001E898 File Offset: 0x0001D898
		private AttributeQualifier parseQualifier(SchemaTokenCreator st, string name)
		{
			ArrayList arrayList = new ArrayList(5);
			try
			{
				if (st.nextToken() == 39)
				{
					arrayList.Add(st.StringValue);
				}
				else if (st.lastttype == 40)
				{
					while (st.nextToken() == 39)
					{
						arrayList.Add(st.StringValue);
					}
				}
			}
			catch (IOException ex)
			{
				throw ex;
			}
			string[] array = new string[arrayList.Count];
			array = (string[])SupportClass.ArrayListSupport.ToArray(arrayList, array);
			return new AttributeQualifier(name, array);
		}

		// Token: 0x04000495 RID: 1173
		internal string rawString;

		// Token: 0x04000496 RID: 1174
		internal string[] names = null;

		// Token: 0x04000497 RID: 1175
		internal string id;

		// Token: 0x04000498 RID: 1176
		internal string description;

		// Token: 0x04000499 RID: 1177
		internal string syntax;

		// Token: 0x0400049A RID: 1178
		internal string superior;

		// Token: 0x0400049B RID: 1179
		internal string nameForm;

		// Token: 0x0400049C RID: 1180
		internal string objectClass;

		// Token: 0x0400049D RID: 1181
		internal string[] superiors;

		// Token: 0x0400049E RID: 1182
		internal string[] required;

		// Token: 0x0400049F RID: 1183
		internal string[] optional;

		// Token: 0x040004A0 RID: 1184
		internal string[] auxiliary;

		// Token: 0x040004A1 RID: 1185
		internal string[] precluded;

		// Token: 0x040004A2 RID: 1186
		internal string[] applies;

		// Token: 0x040004A3 RID: 1187
		internal bool single = false;

		// Token: 0x040004A4 RID: 1188
		internal bool obsolete = false;

		// Token: 0x040004A5 RID: 1189
		internal string equality;

		// Token: 0x040004A6 RID: 1190
		internal string ordering;

		// Token: 0x040004A7 RID: 1191
		internal string substring;

		// Token: 0x040004A8 RID: 1192
		internal bool collective = false;

		// Token: 0x040004A9 RID: 1193
		internal bool userMod = true;

		// Token: 0x040004AA RID: 1194
		internal int usage;

		// Token: 0x040004AB RID: 1195
		internal int type = -1;

		// Token: 0x040004AC RID: 1196
		internal int result;

		// Token: 0x040004AD RID: 1197
		internal ArrayList qualifiers;
	}
}
