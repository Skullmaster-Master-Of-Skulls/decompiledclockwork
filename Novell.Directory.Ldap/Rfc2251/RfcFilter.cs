using System;
using System.Collections;
using System.IO;
using System.Text;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x020000D3 RID: 211
	public class RfcFilter : Asn1Choice
	{
		// Token: 0x06000545 RID: 1349 RVA: 0x00018624 File Offset: 0x00017624
		public RfcFilter(string filter) : base(null)
		{
			this.ChoiceValue = this.parse(filter);
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00018648 File Offset: 0x00017648
		public RfcFilter() : base(null)
		{
			this.filterStack = new Stack();
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x0001866C File Offset: 0x0001766C
		private Asn1Tagged parse(string filterExpr)
		{
			if (filterExpr == null || filterExpr.Equals(""))
			{
				filterExpr = new StringBuilder("(objectclass=*)").ToString();
			}
			int num;
			if ((num = filterExpr.IndexOf('\\')) != -1)
			{
				StringBuilder stringBuilder = new StringBuilder(filterExpr);
				int i = num;
				while (i < stringBuilder.Length - 1)
				{
					char c = stringBuilder[i++];
					if (c == '\\')
					{
						c = stringBuilder[i];
						if (c == '*' || c == '(' || c == ')' || c == '\\')
						{
							stringBuilder.Remove(i, i + 1 - i);
							stringBuilder.Insert(i, Convert.ToString((int)c, 16));
							i += 2;
						}
					}
				}
				filterExpr = stringBuilder.ToString();
			}
			if (filterExpr[0] != '(' && filterExpr[filterExpr.Length - 1] != ')')
			{
				filterExpr = "(" + filterExpr + ")";
			}
			char c2 = filterExpr[0];
			int length = filterExpr.Length;
			if (c2 != '(')
			{
				throw new LdapLocalException("MISSING_LEFT_PAREN", 87);
			}
			if (filterExpr[length - 1] != ')')
			{
				throw new LdapLocalException("MISSING_RIGHT_PAREN", 87);
			}
			int num2 = 0;
			for (int j = 0; j < length; j++)
			{
				if (filterExpr[j] == '(')
				{
					num2++;
				}
				if (filterExpr[j] == ')')
				{
					num2--;
				}
			}
			if (num2 > 0)
			{
				throw new LdapLocalException("MISSING_RIGHT_PAREN", 87);
			}
			if (num2 < 0)
			{
				throw new LdapLocalException("MISSING_LEFT_PAREN", 87);
			}
			this.ft = new RfcFilter.FilterTokenizer(this, filterExpr);
			return this.parseFilter();
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x000187F8 File Offset: 0x000177F8
		private Asn1Tagged parseFilter()
		{
			this.ft.getLeftParen();
			Asn1Tagged result = this.parseFilterComp();
			this.ft.getRightParen();
			return result;
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x00018828 File Offset: 0x00017828
		private Asn1Tagged parseFilterComp()
		{
			Asn1Tagged result = null;
			int opOrAttr = this.ft.OpOrAttr;
			switch (opOrAttr)
			{
			case 0:
			case 1:
				result = new Asn1Tagged(new Asn1Identifier(2, true, opOrAttr), this.parseFilterList(), false);
				break;
			case 2:
				result = new Asn1Tagged(new Asn1Identifier(2, true, opOrAttr), this.parseFilter(), true);
				break;
			default:
			{
				int filterType = this.ft.FilterType;
				string value = this.ft.Value;
				switch (filterType)
				{
				case 3:
					if (value.Equals("*"))
					{
						result = new Asn1Tagged(new Asn1Identifier(2, false, 7), new RfcAttributeDescription(this.ft.Attr), false);
					}
					else if (value.IndexOf('*') != -1)
					{
						SupportClass.Tokenizer tokenizer = new SupportClass.Tokenizer(value, "*", true);
						Asn1SequenceOf asn1SequenceOf = new Asn1SequenceOf(5);
						int count = tokenizer.Count;
						int num = 0;
						string text = new StringBuilder("").ToString();
						while (tokenizer.HasMoreTokens())
						{
							string text2 = tokenizer.NextToken();
							num++;
							if (text2.Equals("*"))
							{
								if (text.Equals(text2))
								{
									asn1SequenceOf.add(new Asn1Tagged(new Asn1Identifier(2, false, 1), new RfcLdapString(this.unescapeString("")), false));
								}
							}
							else if (num == 1)
							{
								asn1SequenceOf.add(new Asn1Tagged(new Asn1Identifier(2, false, 0), new RfcLdapString(this.unescapeString(text2)), false));
							}
							else if (num < count)
							{
								asn1SequenceOf.add(new Asn1Tagged(new Asn1Identifier(2, false, 1), new RfcLdapString(this.unescapeString(text2)), false));
							}
							else
							{
								asn1SequenceOf.add(new Asn1Tagged(new Asn1Identifier(2, false, 2), new RfcLdapString(this.unescapeString(text2)), false));
							}
							text = text2;
						}
						result = new Asn1Tagged(new Asn1Identifier(2, true, 4), new RfcSubstringFilter(new RfcAttributeDescription(this.ft.Attr), asn1SequenceOf), false);
					}
					else
					{
						result = new Asn1Tagged(new Asn1Identifier(2, true, 3), new RfcAttributeValueAssertion(new RfcAttributeDescription(this.ft.Attr), new RfcAssertionValue(this.unescapeString(value))), false);
					}
					break;
				case 5:
				case 6:
				case 8:
					result = new Asn1Tagged(new Asn1Identifier(2, true, filterType), new RfcAttributeValueAssertion(new RfcAttributeDescription(this.ft.Attr), new RfcAssertionValue(this.unescapeString(value))), false);
					break;
				case 9:
				{
					string text3 = null;
					string text4 = null;
					bool flag = false;
					SupportClass.Tokenizer tokenizer2 = new SupportClass.Tokenizer(this.ft.Attr, ":");
					bool flag2 = true;
					while (tokenizer2.HasMoreTokens())
					{
						string text5 = tokenizer2.NextToken().Trim();
						if (flag2 && !text5.Equals(":"))
						{
							text3 = text5;
						}
						else if (text5.Equals("dn"))
						{
							flag = true;
						}
						else if (!text5.Equals(":"))
						{
							text4 = text5;
						}
						flag2 = false;
					}
					result = new Asn1Tagged(new Asn1Identifier(2, true, 9), new RfcMatchingRuleAssertion((text4 == null) ? null : new RfcMatchingRuleId(text4), (text3 == null) ? null : new RfcAttributeDescription(text3), new RfcAssertionValue(this.unescapeString(value)), (!flag) ? null : new Asn1Boolean(true)), false);
					break;
				}
				}
				break;
			}
			}
			return result;
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x00018B84 File Offset: 0x00017B84
		private Asn1SetOf parseFilterList()
		{
			Asn1SetOf asn1SetOf = new Asn1SetOf();
			asn1SetOf.add(this.parseFilter());
			while (this.ft.peekChar() == '(')
			{
				asn1SetOf.add(this.parseFilter());
			}
			return asn1SetOf;
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00018BC8 File Offset: 0x00017BC8
		internal static int hex2int(char c)
		{
			return (c >= '0' && c <= '9') ? ((int)(c - '0')) : ((c >= 'A' && c <= 'F') ? ((int)(c - 'A' + '\n')) : ((c >= 'a' && c <= 'f') ? ((int)(c - 'a' + '\n')) : -1));
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00018C10 File Offset: 0x00017C10
		private sbyte[] unescapeString(string string_Renamed)
		{
			sbyte[] array = new sbyte[string_Renamed.Length * 3];
			bool flag = false;
			bool flag2 = false;
			int length = string_Renamed.Length;
			char[] array2 = new char[1];
			char c = '\0';
			int i = 0;
			int num = 0;
			while (i < length)
			{
				char c2 = string_Renamed[i];
				if (flag)
				{
					int num2;
					if ((num2 = RfcFilter.hex2int(c2)) < 0)
					{
						throw new LdapLocalException("INVALID_ESCAPE", new object[]
						{
							c2
						}, 87);
					}
					if (flag2)
					{
						c = (char)(num2 << 4);
						flag2 = false;
					}
					else
					{
						c |= (char)num2;
						array[num++] = (sbyte)c;
						flag = (flag2 = false);
					}
				}
				else if (c2 == '\\')
				{
					flag = (flag2 = true);
				}
				else
				{
					try
					{
						if ((c2 < '\u0001' || c2 > '\'') && (c2 < '+' || c2 > '[') && c2 < ']')
						{
							string text = "";
							array2[0] = c2;
							Encoding encoding = Encoding.GetEncoding("utf-8");
							byte[] bytes = encoding.GetBytes(new string(array2));
							foreach (sbyte b in SupportClass.ToSByteArray(bytes))
							{
								if (b >= 0 && b < 16)
								{
									text = text + "\\0" + Convert.ToString((int)b & 255, 16);
								}
								else
								{
									text = text + "\\" + Convert.ToString((int)b & 255, 16);
								}
							}
							throw new LdapLocalException("INVALID_CHAR_IN_FILTER", new object[]
							{
								c2,
								text
							}, 87);
						}
						if (c2 <= '\u007f')
						{
							array[num++] = (sbyte)c2;
						}
						else
						{
							array2[0] = c2;
							Encoding encoding2 = Encoding.GetEncoding("utf-8");
							byte[] bytes2 = encoding2.GetBytes(new string(array2));
							sbyte[] array3 = SupportClass.ToSByteArray(bytes2);
							Array.Copy(array3, 0, array, num, array3.Length);
							num += array3.Length;
						}
						flag = false;
					}
					catch (IOException ex)
					{
						throw new SystemException("UTF-8 String encoding not supported by JVM");
					}
				}
				i++;
			}
			if (flag2 || flag)
			{
				throw new LdapLocalException("SHORT_ESCAPE", 87);
			}
			sbyte[] array4 = new sbyte[num];
			Array.Copy(array, 0, array4, 0, num);
			array = null;
			return array4;
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00018E60 File Offset: 0x00017E60
		private void addObject(Asn1Object current)
		{
			if (this.filterStack == null)
			{
				this.filterStack = new Stack();
			}
			if (base.choiceValue() == null)
			{
				this.ChoiceValue = current;
			}
			else
			{
				Asn1Tagged asn1Tagged = (Asn1Tagged)this.filterStack.Peek();
				Asn1Object asn1Object = asn1Tagged.taggedValue();
				if (asn1Object == null)
				{
					asn1Tagged.TaggedValue = current;
					this.filterStack.Push(current);
				}
				else if (asn1Object is Asn1SetOf)
				{
					((Asn1SetOf)asn1Object).add(current);
				}
				else if (asn1Object is Asn1Set)
				{
					((Asn1Set)asn1Object).add(current);
				}
				else if (asn1Object.getIdentifier().Tag == 2)
				{
					throw new LdapLocalException("Attemp to create more than one 'not' sub-filter", 87);
				}
			}
			int tag = current.getIdentifier().Tag;
			if (tag == 0 || tag == 1 || tag == 2)
			{
				this.filterStack.Push(current);
			}
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00018F30 File Offset: 0x00017F30
		public virtual void startSubstrings(string attrName)
		{
			this.finalFound = false;
			Asn1SequenceOf asn1SequenceOf = new Asn1SequenceOf(5);
			Asn1Object current = new Asn1Tagged(new Asn1Identifier(2, true, 4), new RfcSubstringFilter(new RfcAttributeDescription(attrName), asn1SequenceOf), false);
			this.addObject(current);
			SupportClass.StackPush(this.filterStack, asn1SequenceOf);
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x00018F7C File Offset: 0x00017F7C
		[CLSCompliant(false)]
		public virtual void addSubstring(int type, sbyte[] value_Renamed)
		{
			try
			{
				Asn1SequenceOf asn1SequenceOf = (Asn1SequenceOf)this.filterStack.Peek();
				if (type != 0 && type != 1 && type != 2)
				{
					throw new LdapLocalException("Attempt to add an invalid substring type", 87);
				}
				if (type == 0 && asn1SequenceOf.size() != 0)
				{
					throw new LdapLocalException("Attempt to add an initial substring match after the first substring", 87);
				}
				if (this.finalFound)
				{
					throw new LdapLocalException("Attempt to add a substring match after a final substring match", 87);
				}
				if (type == 2)
				{
					this.finalFound = true;
				}
				asn1SequenceOf.add(new Asn1Tagged(new Asn1Identifier(2, false, type), new RfcLdapString(value_Renamed), false));
			}
			catch (InvalidCastException ex)
			{
				throw new LdapLocalException("A call to addSubstring occured without calling startSubstring", 87);
			}
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00019034 File Offset: 0x00018034
		public virtual void endSubstrings()
		{
			try
			{
				this.finalFound = false;
				Asn1SequenceOf asn1SequenceOf = (Asn1SequenceOf)this.filterStack.Peek();
				if (asn1SequenceOf.size() == 0)
				{
					throw new LdapLocalException("Empty substring filter", 87);
				}
			}
			catch (InvalidCastException ex)
			{
				throw new LdapLocalException("Missmatched ending of substrings", 87);
			}
			this.filterStack.Pop();
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x000190A8 File Offset: 0x000180A8
		[CLSCompliant(false)]
		public virtual void addAttributeValueAssertion(int rfcType, string attrName, sbyte[] value_Renamed)
		{
			if (this.filterStack != null && this.filterStack.Count != 0 && this.filterStack.Peek() is Asn1SequenceOf)
			{
				throw new LdapLocalException("Cannot insert an attribute assertion in a substring", 87);
			}
			if (rfcType != 3 && rfcType != 5 && rfcType != 6 && rfcType != 8)
			{
				throw new LdapLocalException("Invalid filter type for AttributeValueAssertion", 87);
			}
			Asn1Object current = new Asn1Tagged(new Asn1Identifier(2, true, rfcType), new RfcAttributeValueAssertion(new RfcAttributeDescription(attrName), new RfcAssertionValue(value_Renamed)), false);
			this.addObject(current);
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x00019130 File Offset: 0x00018130
		public virtual void addPresent(string attrName)
		{
			Asn1Object current = new Asn1Tagged(new Asn1Identifier(2, false, 7), new RfcAttributeDescription(attrName), false);
			this.addObject(current);
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x0001915C File Offset: 0x0001815C
		[CLSCompliant(false)]
		public virtual void addExtensibleMatch(string matchingRule, string attrName, sbyte[] value_Renamed, bool useDNMatching)
		{
			Asn1Object current = new Asn1Tagged(new Asn1Identifier(2, true, 9), new RfcMatchingRuleAssertion((matchingRule == null) ? null : new RfcMatchingRuleId(matchingRule), (attrName == null) ? null : new RfcAttributeDescription(attrName), new RfcAssertionValue(value_Renamed), (!useDNMatching) ? null : new Asn1Boolean(true)), false);
			this.addObject(current);
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x000191B4 File Offset: 0x000181B4
		public virtual void startNestedFilter(int rfcType)
		{
			Asn1Object current;
			if (rfcType == 0 || rfcType == 1)
			{
				current = new Asn1Tagged(new Asn1Identifier(2, true, rfcType), new Asn1SetOf(), false);
			}
			else
			{
				if (rfcType != 2)
				{
					throw new LdapLocalException("Attempt to create a nested filter other than AND, OR or NOT", 87);
				}
				current = new Asn1Tagged(new Asn1Identifier(2, true, rfcType), null, true);
			}
			this.addObject(current);
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x0001920C File Offset: 0x0001820C
		public virtual void endNestedFilter(int rfcType)
		{
			if (rfcType == 2)
			{
				this.filterStack.Pop();
			}
			int tag = ((Asn1Object)this.filterStack.Peek()).getIdentifier().Tag;
			if (tag != rfcType)
			{
				throw new LdapLocalException("Missmatched ending of nested filter", 87);
			}
			this.filterStack.Pop();
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x00019264 File Offset: 0x00018264
		public virtual IEnumerator getFilterIterator()
		{
			return new RfcFilter.FilterIterator(this, (Asn1Tagged)base.choiceValue());
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00019288 File Offset: 0x00018288
		public virtual string filterToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			RfcFilter.stringFilter(this.getFilterIterator(), stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x000192B4 File Offset: 0x000182B4
		private static void stringFilter(IEnumerator itr, StringBuilder filter)
		{
			filter.Append('(');
			while (itr.MoveNext())
			{
				object obj = itr.Current;
				if (obj is int)
				{
					switch ((int)obj)
					{
					case 0:
						filter.Append('&');
						break;
					case 1:
						filter.Append('|');
						break;
					case 2:
						filter.Append('!');
						break;
					case 3:
					{
						filter.Append((string)itr.Current);
						filter.Append('=');
						sbyte[] value_Renamed = (sbyte[])itr.Current;
						filter.Append(RfcFilter.byteString(value_Renamed));
						break;
					}
					case 4:
					{
						filter.Append((string)itr.Current);
						filter.Append('=');
						bool flag = false;
						while (itr.MoveNext())
						{
							switch ((int)itr.Current)
							{
							case 0:
								filter.Append((string)itr.Current);
								filter.Append('*');
								flag = false;
								break;
							case 1:
								if (flag)
								{
									filter.Append('*');
								}
								filter.Append((string)itr.Current);
								filter.Append('*');
								flag = false;
								break;
							case 2:
								if (flag)
								{
									filter.Append('*');
								}
								filter.Append((string)itr.Current);
								break;
							}
						}
						break;
					}
					case 5:
					{
						filter.Append((string)itr.Current);
						filter.Append(">=");
						sbyte[] value_Renamed2 = (sbyte[])itr.Current;
						filter.Append(RfcFilter.byteString(value_Renamed2));
						break;
					}
					case 6:
					{
						filter.Append((string)itr.Current);
						filter.Append("<=");
						sbyte[] value_Renamed3 = (sbyte[])itr.Current;
						filter.Append(RfcFilter.byteString(value_Renamed3));
						break;
					}
					case 7:
						filter.Append((string)itr.Current);
						filter.Append("=*");
						break;
					case 8:
					{
						filter.Append((string)itr.Current);
						filter.Append("~=");
						sbyte[] value_Renamed4 = (sbyte[])itr.Current;
						filter.Append(RfcFilter.byteString(value_Renamed4));
						break;
					}
					case 9:
					{
						string value = (string)itr.Current;
						filter.Append((string)itr.Current);
						filter.Append(':');
						filter.Append(value);
						filter.Append(":=");
						filter.Append((string)itr.Current);
						break;
					}
					}
				}
				else if (obj is IEnumerator)
				{
					RfcFilter.stringFilter((IEnumerator)obj, filter);
				}
			}
			filter.Append(')');
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x000195A4 File Offset: 0x000185A4
		private static string byteString(sbyte[] value_Renamed)
		{
			string result = null;
			if (Base64.isValidUTF8(value_Renamed, true))
			{
				try
				{
					Encoding encoding = Encoding.GetEncoding("utf-8");
					char[] chars = encoding.GetChars(SupportClass.ToByteArray(value_Renamed));
					result = new string(chars);
				}
				catch (IOException arg)
				{
					throw new SystemException("Default JVM does not support UTF-8 encoding" + arg);
				}
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < value_Renamed.Length; i++)
				{
					if (value_Renamed[i] >= 0)
					{
						stringBuilder.Append("\\0");
						stringBuilder.Append(Convert.ToString((short)value_Renamed[i], 16));
					}
					else
					{
						stringBuilder.Append("\\" + Convert.ToString((short)value_Renamed[i], 16).Substring(6));
					}
				}
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x04000404 RID: 1028
		public const int AND = 0;

		// Token: 0x04000405 RID: 1029
		public const int OR = 1;

		// Token: 0x04000406 RID: 1030
		public const int NOT = 2;

		// Token: 0x04000407 RID: 1031
		public const int EQUALITY_MATCH = 3;

		// Token: 0x04000408 RID: 1032
		public const int SUBSTRINGS = 4;

		// Token: 0x04000409 RID: 1033
		public const int GREATER_OR_EQUAL = 5;

		// Token: 0x0400040A RID: 1034
		public const int LESS_OR_EQUAL = 6;

		// Token: 0x0400040B RID: 1035
		public const int PRESENT = 7;

		// Token: 0x0400040C RID: 1036
		public const int APPROX_MATCH = 8;

		// Token: 0x0400040D RID: 1037
		public const int EXTENSIBLE_MATCH = 9;

		// Token: 0x0400040E RID: 1038
		public const int INITIAL = 0;

		// Token: 0x0400040F RID: 1039
		public const int ANY = 1;

		// Token: 0x04000410 RID: 1040
		public const int FINAL = 2;

		// Token: 0x04000411 RID: 1041
		private RfcFilter.FilterTokenizer ft;

		// Token: 0x04000412 RID: 1042
		private Stack filterStack;

		// Token: 0x04000413 RID: 1043
		private bool finalFound;

		// Token: 0x020000D4 RID: 212
		private class FilterIterator : IEnumerator
		{
			// Token: 0x0600055A RID: 1370 RVA: 0x00019684 File Offset: 0x00018684
			public void Reset()
			{
			}

			// Token: 0x0600055B RID: 1371 RVA: 0x00019694 File Offset: 0x00018694
			private void InitBlock(RfcFilter enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}

			// Token: 0x1700014B RID: 331
			// (get) Token: 0x0600055C RID: 1372 RVA: 0x000196A8 File Offset: 0x000186A8
			public virtual object Current
			{
				get
				{
					object result = null;
					if (!this.tagReturned)
					{
						this.tagReturned = true;
						result = this.root.getIdentifier().Tag;
					}
					else
					{
						Asn1Object asn1Object = this.root.taggedValue();
						if (asn1Object is RfcLdapString)
						{
							this.hasMore = false;
							result = ((RfcLdapString)asn1Object).stringValue();
						}
						else if (asn1Object is RfcSubstringFilter)
						{
							RfcSubstringFilter rfcSubstringFilter = (RfcSubstringFilter)asn1Object;
							if (this.index == -1)
							{
								this.index = 0;
								RfcAttributeDescription rfcAttributeDescription = (RfcAttributeDescription)rfcSubstringFilter.get_Renamed(0);
								result = rfcAttributeDescription.stringValue();
							}
							else if (this.index % 2 == 0)
							{
								Asn1SequenceOf asn1SequenceOf = (Asn1SequenceOf)rfcSubstringFilter.get_Renamed(1);
								result = ((Asn1Tagged)asn1SequenceOf.get_Renamed(this.index / 2)).getIdentifier().Tag;
								this.index++;
							}
							else
							{
								Asn1SequenceOf asn1SequenceOf2 = (Asn1SequenceOf)rfcSubstringFilter.get_Renamed(1);
								Asn1Tagged asn1Tagged = (Asn1Tagged)asn1SequenceOf2.get_Renamed(this.index / 2);
								RfcLdapString rfcLdapString = (RfcLdapString)asn1Tagged.taggedValue();
								result = rfcLdapString.stringValue();
								this.index++;
							}
							if (this.index / 2 >= ((Asn1SequenceOf)rfcSubstringFilter.get_Renamed(1)).size())
							{
								this.hasMore = false;
							}
						}
						else if (asn1Object is RfcAttributeValueAssertion)
						{
							RfcAttributeValueAssertion rfcAttributeValueAssertion = (RfcAttributeValueAssertion)asn1Object;
							if (this.index == -1)
							{
								result = rfcAttributeValueAssertion.AttributeDescription;
								this.index = 1;
							}
							else if (this.index == 1)
							{
								result = rfcAttributeValueAssertion.AssertionValue;
								this.index = 2;
								this.hasMore = false;
							}
						}
						else if (asn1Object is RfcMatchingRuleAssertion)
						{
							RfcMatchingRuleAssertion rfcMatchingRuleAssertion = (RfcMatchingRuleAssertion)asn1Object;
							if (this.index == -1)
							{
								this.index = 0;
							}
							result = ((Asn1OctetString)((Asn1Tagged)rfcMatchingRuleAssertion.get_Renamed(this.index++)).taggedValue()).stringValue();
							if (this.index > 2)
							{
								this.hasMore = false;
							}
						}
						else if (asn1Object is Asn1SetOf)
						{
							Asn1SetOf asn1SetOf = (Asn1SetOf)asn1Object;
							if (this.index == -1)
							{
								this.index = 0;
							}
							result = new RfcFilter.FilterIterator(this.enclosingInstance, (Asn1Tagged)asn1SetOf.get_Renamed(this.index++));
							if (this.index >= asn1SetOf.size())
							{
								this.hasMore = false;
							}
						}
						else if (asn1Object is Asn1Tagged)
						{
							result = new RfcFilter.FilterIterator(this.enclosingInstance, (Asn1Tagged)asn1Object);
							this.hasMore = false;
						}
					}
					return result;
				}
			}

			// Token: 0x1700014C RID: 332
			// (get) Token: 0x0600055D RID: 1373 RVA: 0x00019940 File Offset: 0x00018940
			public RfcFilter Enclosing_Instance
			{
				get
				{
					return this.enclosingInstance;
				}
			}

			// Token: 0x0600055E RID: 1374 RVA: 0x00019958 File Offset: 0x00018958
			public FilterIterator(RfcFilter enclosingInstance, Asn1Tagged root)
			{
				this.InitBlock(enclosingInstance);
				this.root = root;
			}

			// Token: 0x0600055F RID: 1375 RVA: 0x00019990 File Offset: 0x00018990
			public virtual bool MoveNext()
			{
				return this.hasMore;
			}

			// Token: 0x06000560 RID: 1376 RVA: 0x000199A8 File Offset: 0x000189A8
			public void remove()
			{
				throw new NotSupportedException("Remove is not supported on a filter iterator");
			}

			// Token: 0x04000414 RID: 1044
			private RfcFilter enclosingInstance;

			// Token: 0x04000415 RID: 1045
			internal Asn1Tagged root;

			// Token: 0x04000416 RID: 1046
			internal bool tagReturned = false;

			// Token: 0x04000417 RID: 1047
			internal int index = -1;

			// Token: 0x04000418 RID: 1048
			private bool hasMore = true;
		}

		// Token: 0x020000D5 RID: 213
		internal class FilterTokenizer
		{
			// Token: 0x06000561 RID: 1377 RVA: 0x000199C0 File Offset: 0x000189C0
			private void InitBlock(RfcFilter enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}

			// Token: 0x1700014D RID: 333
			// (get) Token: 0x06000562 RID: 1378 RVA: 0x000199D4 File Offset: 0x000189D4
			public virtual int OpOrAttr
			{
				get
				{
					if (this.offset >= this.filterLength)
					{
						throw new LdapLocalException("UNEXPECTED_END", 87);
					}
					int num = (int)this.filter[this.offset];
					int result;
					if (num == 38)
					{
						this.offset++;
						result = 0;
					}
					else if (num == 124)
					{
						this.offset++;
						result = 1;
					}
					else if (num == 33)
					{
						this.offset++;
						result = 2;
					}
					else
					{
						if (this.filter.Substring(this.offset).StartsWith(":="))
						{
							throw new LdapLocalException("NO_MATCHING_RULE", 87);
						}
						if (this.filter.Substring(this.offset).StartsWith("::=") || this.filter.Substring(this.offset).StartsWith(":::="))
						{
							throw new LdapLocalException("NO_DN_NOR_MATCHING_RULE", 87);
						}
						string text = "=~<>()";
						StringBuilder stringBuilder = new StringBuilder();
						while (text.IndexOf(this.filter[this.offset]) == -1 && !this.filter.Substring(this.offset).StartsWith(":="))
						{
							stringBuilder.Append(this.filter[this.offset++]);
						}
						this.attr = stringBuilder.ToString().Trim();
						if (this.attr.Length == 0 || this.attr[0] == ';')
						{
							throw new LdapLocalException("NO_ATTRIBUTE_NAME", 87);
						}
						int i = 0;
						while (i < this.attr.Length)
						{
							char c = this.attr[i];
							if (!char.IsLetterOrDigit(c) && c != '-' && c != '.' && c != ';' && c != ':')
							{
								if (c == '\\')
								{
									throw new LdapLocalException("INVALID_ESC_IN_DESCR", 87);
								}
								throw new LdapLocalException("INVALID_CHAR_IN_DESCR", new object[]
								{
									c
								}, 87);
							}
							else
							{
								i++;
							}
						}
						i = this.attr.IndexOf(';');
						if (i != -1 && i == this.attr.Length - 1)
						{
							throw new LdapLocalException("NO_OPTION", 87);
						}
						result = -1;
					}
					return result;
				}
			}

			// Token: 0x1700014E RID: 334
			// (get) Token: 0x06000563 RID: 1379 RVA: 0x00019C28 File Offset: 0x00018C28
			public virtual int FilterType
			{
				get
				{
					if (this.offset >= this.filterLength)
					{
						throw new LdapLocalException("UNEXPECTED_END", 87);
					}
					int result;
					if (this.filter.Substring(this.offset).StartsWith(">="))
					{
						this.offset += 2;
						result = 5;
					}
					else if (this.filter.Substring(this.offset).StartsWith("<="))
					{
						this.offset += 2;
						result = 6;
					}
					else if (this.filter.Substring(this.offset).StartsWith("~="))
					{
						this.offset += 2;
						result = 8;
					}
					else if (this.filter.Substring(this.offset).StartsWith(":="))
					{
						this.offset += 2;
						result = 9;
					}
					else
					{
						if (this.filter[this.offset] != '=')
						{
							throw new LdapLocalException("INVALID_FILTER_COMPARISON", 87);
						}
						this.offset++;
						result = 3;
					}
					return result;
				}
			}

			// Token: 0x1700014F RID: 335
			// (get) Token: 0x06000564 RID: 1380 RVA: 0x00019D4C File Offset: 0x00018D4C
			public virtual string Value
			{
				get
				{
					if (this.offset >= this.filterLength)
					{
						throw new LdapLocalException("UNEXPECTED_END", 87);
					}
					int num = this.filter.IndexOf(')', this.offset);
					if (num == -1)
					{
						num = this.filterLength;
					}
					string result = this.filter.Substring(this.offset, num - this.offset);
					this.offset = num;
					return result;
				}
			}

			// Token: 0x17000150 RID: 336
			// (get) Token: 0x06000565 RID: 1381 RVA: 0x00019DBC File Offset: 0x00018DBC
			public virtual string Attr
			{
				get
				{
					return this.attr;
				}
			}

			// Token: 0x17000151 RID: 337
			// (get) Token: 0x06000566 RID: 1382 RVA: 0x00019DD4 File Offset: 0x00018DD4
			public RfcFilter Enclosing_Instance
			{
				get
				{
					return this.enclosingInstance;
				}
			}

			// Token: 0x06000567 RID: 1383 RVA: 0x00019DEC File Offset: 0x00018DEC
			public FilterTokenizer(RfcFilter enclosingInstance, string filter)
			{
				this.InitBlock(enclosingInstance);
				this.filter = filter;
				this.offset = 0;
				this.filterLength = filter.Length;
			}

			// Token: 0x06000568 RID: 1384 RVA: 0x00019E24 File Offset: 0x00018E24
			public void getLeftParen()
			{
				if (this.offset >= this.filterLength)
				{
					throw new LdapLocalException("UNEXPECTED_END", 87);
				}
				if (this.filter[this.offset++] != '(')
				{
					throw new LdapLocalException("EXPECTING_LEFT_PAREN", new object[]
					{
						this.filter[--this.offset]
					}, 87);
				}
			}

			// Token: 0x06000569 RID: 1385 RVA: 0x00019EA8 File Offset: 0x00018EA8
			public void getRightParen()
			{
				if (this.offset >= this.filterLength)
				{
					throw new LdapLocalException("UNEXPECTED_END", 87);
				}
				if (this.filter[this.offset++] != ')')
				{
					throw new LdapLocalException("EXPECTING_RIGHT_PAREN", new object[]
					{
						this.filter[this.offset - 1]
					}, 87);
				}
			}

			// Token: 0x0600056A RID: 1386 RVA: 0x00019F24 File Offset: 0x00018F24
			public char peekChar()
			{
				if (this.offset >= this.filterLength)
				{
					throw new LdapLocalException("UNEXPECTED_END", 87);
				}
				return this.filter[this.offset];
			}

			// Token: 0x04000419 RID: 1049
			private RfcFilter enclosingInstance;

			// Token: 0x0400041A RID: 1050
			private string filter;

			// Token: 0x0400041B RID: 1051
			private string attr;

			// Token: 0x0400041C RID: 1052
			private int offset;

			// Token: 0x0400041D RID: 1053
			private int filterLength;
		}
	}
}
