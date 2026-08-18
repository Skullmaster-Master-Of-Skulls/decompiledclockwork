using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Text;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000305 RID: 773
	public class X509Name : Asn1Encodable
	{
		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06001C41 RID: 7233 RVA: 0x000A8F9B File Offset: 0x000A7F9B
		// (set) Token: 0x06001C42 RID: 7234 RVA: 0x000A8FA4 File Offset: 0x000A7FA4
		public static bool DefaultReverse
		{
			get
			{
				return X509Name.defaultReverse[0];
			}
			set
			{
				X509Name.defaultReverse[0] = value;
			}
		}

		// Token: 0x06001C43 RID: 7235 RVA: 0x000A8FB0 File Offset: 0x000A7FB0
		static X509Name()
		{
			bool[] array = new bool[1];
			X509Name.defaultReverse = array;
			X509Name.DefaultSymbols = new Hashtable();
			X509Name.RFC2253Symbols = new Hashtable();
			X509Name.RFC1779Symbols = new Hashtable();
			X509Name.DefaultLookup = new Hashtable();
			X509Name.OIDLookup = X509Name.DefaultSymbols;
			X509Name.SymbolLookup = X509Name.DefaultLookup;
			X509Name.DefaultSymbols.Add(X509Name.C, "C");
			X509Name.DefaultSymbols.Add(X509Name.O, "O");
			X509Name.DefaultSymbols.Add(X509Name.T, "T");
			X509Name.DefaultSymbols.Add(X509Name.OU, "OU");
			X509Name.DefaultSymbols.Add(X509Name.CN, "CN");
			X509Name.DefaultSymbols.Add(X509Name.L, "L");
			X509Name.DefaultSymbols.Add(X509Name.ST, "ST");
			X509Name.DefaultSymbols.Add(X509Name.SerialNumber, "SERIALNUMBER");
			X509Name.DefaultSymbols.Add(X509Name.EmailAddress, "E");
			X509Name.DefaultSymbols.Add(X509Name.DC, "DC");
			X509Name.DefaultSymbols.Add(X509Name.UID, "UID");
			X509Name.DefaultSymbols.Add(X509Name.Street, "STREET");
			X509Name.DefaultSymbols.Add(X509Name.Surname, "SURNAME");
			X509Name.DefaultSymbols.Add(X509Name.GivenName, "GIVENNAME");
			X509Name.DefaultSymbols.Add(X509Name.Initials, "INITIALS");
			X509Name.DefaultSymbols.Add(X509Name.Generation, "GENERATION");
			X509Name.DefaultSymbols.Add(X509Name.UnstructuredAddress, "unstructuredAddress");
			X509Name.DefaultSymbols.Add(X509Name.UnstructuredName, "unstructuredName");
			X509Name.DefaultSymbols.Add(X509Name.UniqueIdentifier, "UniqueIdentifier");
			X509Name.DefaultSymbols.Add(X509Name.DnQualifier, "DN");
			X509Name.DefaultSymbols.Add(X509Name.Pseudonym, "Pseudonym");
			X509Name.DefaultSymbols.Add(X509Name.PostalAddress, "PostalAddress");
			X509Name.DefaultSymbols.Add(X509Name.NameAtBirth, "NameAtBirth");
			X509Name.DefaultSymbols.Add(X509Name.CountryOfCitizenship, "CountryOfCitizenship");
			X509Name.DefaultSymbols.Add(X509Name.CountryOfResidence, "CountryOfResidence");
			X509Name.DefaultSymbols.Add(X509Name.Gender, "Gender");
			X509Name.DefaultSymbols.Add(X509Name.PlaceOfBirth, "PlaceOfBirth");
			X509Name.DefaultSymbols.Add(X509Name.DateOfBirth, "DateOfBirth");
			X509Name.DefaultSymbols.Add(X509Name.PostalCode, "PostalCode");
			X509Name.DefaultSymbols.Add(X509Name.BusinessCategory, "BusinessCategory");
			X509Name.DefaultSymbols.Add(X509Name.TelephoneNumber, "TelephoneNumber");
			X509Name.RFC2253Symbols.Add(X509Name.C, "C");
			X509Name.RFC2253Symbols.Add(X509Name.O, "O");
			X509Name.RFC2253Symbols.Add(X509Name.OU, "OU");
			X509Name.RFC2253Symbols.Add(X509Name.CN, "CN");
			X509Name.RFC2253Symbols.Add(X509Name.L, "L");
			X509Name.RFC2253Symbols.Add(X509Name.ST, "ST");
			X509Name.RFC2253Symbols.Add(X509Name.Street, "STREET");
			X509Name.RFC2253Symbols.Add(X509Name.DC, "DC");
			X509Name.RFC2253Symbols.Add(X509Name.UID, "UID");
			X509Name.RFC1779Symbols.Add(X509Name.C, "C");
			X509Name.RFC1779Symbols.Add(X509Name.O, "O");
			X509Name.RFC1779Symbols.Add(X509Name.OU, "OU");
			X509Name.RFC1779Symbols.Add(X509Name.CN, "CN");
			X509Name.RFC1779Symbols.Add(X509Name.L, "L");
			X509Name.RFC1779Symbols.Add(X509Name.ST, "ST");
			X509Name.RFC1779Symbols.Add(X509Name.Street, "STREET");
			X509Name.DefaultLookup.Add("c", X509Name.C);
			X509Name.DefaultLookup.Add("o", X509Name.O);
			X509Name.DefaultLookup.Add("t", X509Name.T);
			X509Name.DefaultLookup.Add("ou", X509Name.OU);
			X509Name.DefaultLookup.Add("cn", X509Name.CN);
			X509Name.DefaultLookup.Add("l", X509Name.L);
			X509Name.DefaultLookup.Add("st", X509Name.ST);
			X509Name.DefaultLookup.Add("serialnumber", X509Name.SerialNumber);
			X509Name.DefaultLookup.Add("street", X509Name.Street);
			X509Name.DefaultLookup.Add("emailaddress", X509Name.E);
			X509Name.DefaultLookup.Add("dc", X509Name.DC);
			X509Name.DefaultLookup.Add("e", X509Name.E);
			X509Name.DefaultLookup.Add("uid", X509Name.UID);
			X509Name.DefaultLookup.Add("surname", X509Name.Surname);
			X509Name.DefaultLookup.Add("givenname", X509Name.GivenName);
			X509Name.DefaultLookup.Add("initials", X509Name.Initials);
			X509Name.DefaultLookup.Add("generation", X509Name.Generation);
			X509Name.DefaultLookup.Add("unstructuredaddress", X509Name.UnstructuredAddress);
			X509Name.DefaultLookup.Add("unstructuredname", X509Name.UnstructuredName);
			X509Name.DefaultLookup.Add("uniqueidentifier", X509Name.UniqueIdentifier);
			X509Name.DefaultLookup.Add("dn", X509Name.DnQualifier);
			X509Name.DefaultLookup.Add("pseudonym", X509Name.Pseudonym);
			X509Name.DefaultLookup.Add("postaladdress", X509Name.PostalAddress);
			X509Name.DefaultLookup.Add("nameofbirth", X509Name.NameAtBirth);
			X509Name.DefaultLookup.Add("countryofcitizenship", X509Name.CountryOfCitizenship);
			X509Name.DefaultLookup.Add("countryofresidence", X509Name.CountryOfResidence);
			X509Name.DefaultLookup.Add("gender", X509Name.Gender);
			X509Name.DefaultLookup.Add("placeofbirth", X509Name.PlaceOfBirth);
			X509Name.DefaultLookup.Add("dateofbirth", X509Name.DateOfBirth);
			X509Name.DefaultLookup.Add("postalcode", X509Name.PostalCode);
			X509Name.DefaultLookup.Add("businesscategory", X509Name.BusinessCategory);
			X509Name.DefaultLookup.Add("telephonenumber", X509Name.TelephoneNumber);
		}

		// Token: 0x06001C44 RID: 7236 RVA: 0x000A9812 File Offset: 0x000A8812
		public static X509Name GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return X509Name.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06001C45 RID: 7237 RVA: 0x000A9820 File Offset: 0x000A8820
		public static X509Name GetInstance(object obj)
		{
			if (obj == null || obj is X509Name)
			{
				return (X509Name)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new X509Name((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name);
		}

		// Token: 0x06001C46 RID: 7238 RVA: 0x000A9870 File Offset: 0x000A8870
		protected X509Name(Asn1Sequence seq)
		{
			this.seq = seq;
			foreach (object obj in seq)
			{
				Asn1Encodable asn1Encodable = (Asn1Encodable)obj;
				Asn1Set instance = Asn1Set.GetInstance(asn1Encodable.ToAsn1Object());
				for (int i = 0; i < instance.Count; i++)
				{
					Asn1Sequence instance2 = Asn1Sequence.GetInstance(instance[i].ToAsn1Object());
					if (instance2.Count != 2)
					{
						throw new ArgumentException("badly sized pair");
					}
					this.ordering.Add(DerObjectIdentifier.GetInstance(instance2[0].ToAsn1Object()));
					Asn1Object asn1Object = instance2[1].ToAsn1Object();
					if (asn1Object is IAsn1String && !(asn1Object is DerUniversalString))
					{
						string text = ((IAsn1String)asn1Object).GetString();
						if (text.StartsWith("#"))
						{
							text = "\\" + text;
						}
						this.values.Add(text);
					}
					else
					{
						this.values.Add("#" + Hex.ToHexString(asn1Object.GetEncoded()));
					}
					this.added.Add(i != 0);
				}
			}
		}

		// Token: 0x06001C47 RID: 7239 RVA: 0x000A9A00 File Offset: 0x000A8A00
		public X509Name(ArrayList ordering, Hashtable attributes) : this(ordering, attributes, new X509DefaultEntryConverter())
		{
		}

		// Token: 0x06001C48 RID: 7240 RVA: 0x000A9A10 File Offset: 0x000A8A10
		public X509Name(ArrayList ordering, Hashtable attributes, X509NameEntryConverter converter)
		{
			this.converter = converter;
			foreach (object obj in ordering)
			{
				DerObjectIdentifier derObjectIdentifier = (DerObjectIdentifier)obj;
				object obj2 = attributes[derObjectIdentifier];
				if (obj2 == null)
				{
					throw new ArgumentException("No attribute for object id - " + derObjectIdentifier + " - passed to distinguished name");
				}
				this.ordering.Add(derObjectIdentifier);
				this.added.Add(false);
				this.values.Add(obj2);
			}
		}

		// Token: 0x06001C49 RID: 7241 RVA: 0x000A9AD8 File Offset: 0x000A8AD8
		public X509Name(ArrayList oids, ArrayList values) : this(oids, values, new X509DefaultEntryConverter())
		{
		}

		// Token: 0x06001C4A RID: 7242 RVA: 0x000A9AE8 File Offset: 0x000A8AE8
		public X509Name(ArrayList oids, ArrayList values, X509NameEntryConverter converter)
		{
			this.converter = converter;
			if (oids.Count != values.Count)
			{
				throw new ArgumentException("'oids' must be same length as 'values'.");
			}
			for (int i = 0; i < oids.Count; i++)
			{
				this.ordering.Add(oids[i]);
				this.values.Add(values[i]);
				this.added.Add(false);
			}
		}

		// Token: 0x06001C4B RID: 7243 RVA: 0x000A9B85 File Offset: 0x000A8B85
		public X509Name(string dirName) : this(X509Name.DefaultReverse, X509Name.DefaultLookup, dirName)
		{
		}

		// Token: 0x06001C4C RID: 7244 RVA: 0x000A9B98 File Offset: 0x000A8B98
		public X509Name(string dirName, X509NameEntryConverter converter) : this(X509Name.DefaultReverse, X509Name.DefaultLookup, dirName, converter)
		{
		}

		// Token: 0x06001C4D RID: 7245 RVA: 0x000A9BAC File Offset: 0x000A8BAC
		public X509Name(bool reverse, string dirName) : this(reverse, X509Name.DefaultLookup, dirName)
		{
		}

		// Token: 0x06001C4E RID: 7246 RVA: 0x000A9BBB File Offset: 0x000A8BBB
		public X509Name(bool reverse, string dirName, X509NameEntryConverter converter) : this(reverse, X509Name.DefaultLookup, dirName, converter)
		{
		}

		// Token: 0x06001C4F RID: 7247 RVA: 0x000A9BCB File Offset: 0x000A8BCB
		public X509Name(bool reverse, Hashtable lookUp, string dirName) : this(reverse, lookUp, dirName, new X509DefaultEntryConverter())
		{
		}

		// Token: 0x06001C50 RID: 7248 RVA: 0x000A9BDC File Offset: 0x000A8BDC
		private DerObjectIdentifier DecodeOid(string name, IDictionary lookUp)
		{
			if (name.ToUpper(CultureInfo.InvariantCulture).StartsWith("OID."))
			{
				return new DerObjectIdentifier(name.Substring(4));
			}
			if (name[0] >= '0' && name[0] <= '9')
			{
				return new DerObjectIdentifier(name);
			}
			DerObjectIdentifier derObjectIdentifier = (DerObjectIdentifier)lookUp[name.ToLower(CultureInfo.InvariantCulture)];
			if (derObjectIdentifier == null)
			{
				throw new ArgumentException("Unknown object id - " + name + " - passed to distinguished name");
			}
			return derObjectIdentifier;
		}

		// Token: 0x06001C51 RID: 7249 RVA: 0x000A9C5C File Offset: 0x000A8C5C
		public X509Name(bool reverse, IDictionary lookUp, string dirName, X509NameEntryConverter converter)
		{
			this.converter = converter;
			X509NameTokenizer x509NameTokenizer = new X509NameTokenizer(dirName);
			while (x509NameTokenizer.HasMoreTokens())
			{
				string text = x509NameTokenizer.NextToken();
				int num = text.IndexOf('=');
				if (num == -1)
				{
					throw new ArgumentException("badly formated directory string");
				}
				string name = text.Substring(0, num);
				string text2 = text.Substring(num + 1);
				DerObjectIdentifier value = this.DecodeOid(name, lookUp);
				if (text2.IndexOf('+') > 0)
				{
					X509NameTokenizer x509NameTokenizer2 = new X509NameTokenizer(text2, '+');
					string value2 = x509NameTokenizer2.NextToken();
					this.ordering.Add(value);
					this.values.Add(value2);
					this.added.Add(false);
					while (x509NameTokenizer2.HasMoreTokens())
					{
						string text3 = x509NameTokenizer2.NextToken();
						int num2 = text3.IndexOf('=');
						string name2 = text3.Substring(0, num2);
						string value3 = text3.Substring(num2 + 1);
						this.ordering.Add(this.DecodeOid(name2, lookUp));
						this.values.Add(value3);
						this.added.Add(true);
					}
				}
				else
				{
					this.ordering.Add(value);
					this.values.Add(text2);
					this.added.Add(false);
				}
			}
			if (reverse)
			{
				ArrayList arrayList = new ArrayList();
				ArrayList arrayList2 = new ArrayList();
				ArrayList arrayList3 = new ArrayList();
				int num3 = 1;
				for (int i = 0; i < this.ordering.Count; i++)
				{
					if (!(bool)this.added[i])
					{
						num3 = 0;
					}
					int index = num3++;
					arrayList.Insert(index, this.ordering[i]);
					arrayList2.Insert(index, this.values[i]);
					arrayList3.Insert(index, this.added[i]);
				}
				this.ordering = arrayList;
				this.values = arrayList2;
				this.added = arrayList3;
			}
		}

		// Token: 0x06001C52 RID: 7250 RVA: 0x000A9E8C File Offset: 0x000A8E8C
		public ArrayList GetOids()
		{
			return (ArrayList)this.ordering.Clone();
		}

		// Token: 0x06001C53 RID: 7251 RVA: 0x000A9E9E File Offset: 0x000A8E9E
		public ArrayList GetValues()
		{
			return (ArrayList)this.values.Clone();
		}

		// Token: 0x06001C54 RID: 7252 RVA: 0x000A9EB0 File Offset: 0x000A8EB0
		public ArrayList GetValues(DerObjectIdentifier oid)
		{
			ArrayList arrayList = new ArrayList();
			for (int num = 0; num != this.values.Count; num++)
			{
				if (this.ordering[num].Equals(oid))
				{
					string text = (string)this.values[num];
					if (text.StartsWith("\\#"))
					{
						text = text.Substring(1);
					}
					arrayList.Add(text);
				}
			}
			return arrayList;
		}

		// Token: 0x06001C55 RID: 7253 RVA: 0x000A9F20 File Offset: 0x000A8F20
		public override Asn1Object ToAsn1Object()
		{
			if (this.seq == null)
			{
				Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
				Asn1EncodableVector asn1EncodableVector2 = new Asn1EncodableVector(new Asn1Encodable[0]);
				DerObjectIdentifier derObjectIdentifier = null;
				for (int num = 0; num != this.ordering.Count; num++)
				{
					DerObjectIdentifier derObjectIdentifier2 = (DerObjectIdentifier)this.ordering[num];
					string value = (string)this.values[num];
					if (derObjectIdentifier != null && !(bool)this.added[num])
					{
						asn1EncodableVector.Add(new Asn1Encodable[]
						{
							new DerSet(asn1EncodableVector2)
						});
						asn1EncodableVector2 = new Asn1EncodableVector(new Asn1Encodable[0]);
					}
					asn1EncodableVector2.Add(new Asn1Encodable[]
					{
						new DerSequence(new Asn1Encodable[]
						{
							derObjectIdentifier2,
							this.converter.GetConvertedValue(derObjectIdentifier2, value)
						})
					});
					derObjectIdentifier = derObjectIdentifier2;
				}
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerSet(asn1EncodableVector2)
				});
				this.seq = new DerSequence(asn1EncodableVector);
			}
			return this.seq;
		}

		// Token: 0x06001C56 RID: 7254 RVA: 0x000AA03B File Offset: 0x000A903B
		[Obsolete("Use 'Equivalent(X509Name, int)' instead")]
		public bool Equals(X509Name other, bool inOrder)
		{
			return this.Equivalent(other, inOrder);
		}

		// Token: 0x06001C57 RID: 7255 RVA: 0x000AA048 File Offset: 0x000A9048
		public bool Equivalent(X509Name other, bool inOrder)
		{
			if (!inOrder)
			{
				return this.Equivalent(other);
			}
			if (other == null)
			{
				return false;
			}
			if (other == this)
			{
				return true;
			}
			int count = this.ordering.Count;
			if (count != other.ordering.Count)
			{
				return false;
			}
			for (int i = 0; i < count; i++)
			{
				DerObjectIdentifier derObjectIdentifier = (DerObjectIdentifier)this.ordering[i];
				DerObjectIdentifier obj = (DerObjectIdentifier)other.ordering[i];
				if (!derObjectIdentifier.Equals(obj))
				{
					return false;
				}
				string s = (string)this.values[i];
				string s2 = (string)other.values[i];
				if (!X509Name.equivalentStrings(s, s2))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001C58 RID: 7256 RVA: 0x000AA0F6 File Offset: 0x000A90F6
		[Obsolete("Use 'Equivalent(X509Name)' instead")]
		public bool Equals(X509Name other)
		{
			return this.Equivalent(other);
		}

		// Token: 0x06001C59 RID: 7257 RVA: 0x000AA100 File Offset: 0x000A9100
		public bool Equivalent(X509Name other)
		{
			if (other == null)
			{
				return false;
			}
			if (other == this)
			{
				return true;
			}
			int count = this.ordering.Count;
			if (count != other.ordering.Count)
			{
				return false;
			}
			bool[] array = new bool[count];
			int num;
			int num2;
			int num3;
			if (this.ordering[0].Equals(other.ordering[0]))
			{
				num = 0;
				num2 = count;
				num3 = 1;
			}
			else
			{
				num = count - 1;
				num2 = -1;
				num3 = -1;
			}
			for (int num4 = num; num4 != num2; num4 += num3)
			{
				bool flag = false;
				DerObjectIdentifier derObjectIdentifier = (DerObjectIdentifier)this.ordering[num4];
				string s = (string)this.values[num4];
				for (int i = 0; i < count; i++)
				{
					if (!array[i])
					{
						DerObjectIdentifier obj = (DerObjectIdentifier)other.ordering[i];
						if (derObjectIdentifier.Equals(obj))
						{
							string s2 = (string)other.values[i];
							if (X509Name.equivalentStrings(s, s2))
							{
								array[i] = true;
								flag = true;
								break;
							}
						}
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001C5A RID: 7258 RVA: 0x000AA214 File Offset: 0x000A9214
		private static bool equivalentStrings(string s1, string s2)
		{
			string text = X509Name.canonicalize(s1);
			string text2 = X509Name.canonicalize(s2);
			if (!text.Equals(text2))
			{
				text = X509Name.stripInternalSpaces(text);
				text2 = X509Name.stripInternalSpaces(text2);
				if (!text.Equals(text2))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001C5B RID: 7259 RVA: 0x000AA254 File Offset: 0x000A9254
		private static string canonicalize(string s)
		{
			string text = s.ToLower(CultureInfo.InvariantCulture).Trim();
			if (text.StartsWith("#"))
			{
				Asn1Object asn1Object = X509Name.decodeObject(text);
				if (asn1Object is IAsn1String)
				{
					text = ((IAsn1String)asn1Object).GetString().ToLower(CultureInfo.InvariantCulture).Trim();
				}
			}
			return text;
		}

		// Token: 0x06001C5C RID: 7260 RVA: 0x000AA2AC File Offset: 0x000A92AC
		private static Asn1Object decodeObject(string v)
		{
			Asn1Object result;
			try
			{
				result = Asn1Object.FromByteArray(Hex.Decode(v.Substring(1)));
			}
			catch (IOException ex)
			{
				throw new InvalidOperationException("unknown encoding in name: " + ex.Message, ex);
			}
			return result;
		}

		// Token: 0x06001C5D RID: 7261 RVA: 0x000AA2F8 File Offset: 0x000A92F8
		private static string stripInternalSpaces(string str)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (str.Length != 0)
			{
				char c = str[0];
				stringBuilder.Append(c);
				for (int i = 1; i < str.Length; i++)
				{
					char c2 = str[i];
					if (c != ' ' || c2 != ' ')
					{
						stringBuilder.Append(c2);
					}
					c = c2;
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001C5E RID: 7262 RVA: 0x000AA358 File Offset: 0x000A9358
		private void AppendValue(StringBuilder buf, Hashtable oidSymbols, DerObjectIdentifier oid, string val)
		{
			string text = (string)oidSymbols[oid];
			if (text != null)
			{
				buf.Append(text);
			}
			else
			{
				buf.Append(oid.Id);
			}
			buf.Append('=');
			int num = buf.Length;
			buf.Append(val);
			int num2 = buf.Length;
			if (val.StartsWith("\\#"))
			{
				num += 2;
			}
			while (num != num2)
			{
				if (buf[num] == ',' || buf[num] == '"' || buf[num] == '\\' || buf[num] == '+' || buf[num] == '=' || buf[num] == '<' || buf[num] == '>' || buf[num] == ';')
				{
					buf.Insert(num++, "\\");
					num2++;
				}
				num++;
			}
		}

		// Token: 0x06001C5F RID: 7263 RVA: 0x000AA438 File Offset: 0x000A9438
		public string ToString(bool reverse, Hashtable oidSymbols)
		{
			ArrayList arrayList = new ArrayList();
			StringBuilder stringBuilder = null;
			for (int i = 0; i < this.ordering.Count; i++)
			{
				if ((bool)this.added[i])
				{
					stringBuilder.Append('+');
					this.AppendValue(stringBuilder, oidSymbols, (DerObjectIdentifier)this.ordering[i], (string)this.values[i]);
				}
				else
				{
					stringBuilder = new StringBuilder();
					this.AppendValue(stringBuilder, oidSymbols, (DerObjectIdentifier)this.ordering[i], (string)this.values[i]);
					arrayList.Add(stringBuilder);
				}
			}
			if (reverse)
			{
				arrayList.Reverse();
			}
			StringBuilder stringBuilder2 = new StringBuilder();
			if (arrayList.Count > 0)
			{
				stringBuilder2.Append(arrayList[0].ToString());
				for (int j = 1; j < arrayList.Count; j++)
				{
					stringBuilder2.Append(',');
					stringBuilder2.Append(arrayList[j].ToString());
				}
			}
			return stringBuilder2.ToString();
		}

		// Token: 0x06001C60 RID: 7264 RVA: 0x000AA54C File Offset: 0x000A954C
		public override string ToString()
		{
			return this.ToString(X509Name.DefaultReverse, X509Name.DefaultSymbols);
		}

		// Token: 0x04001360 RID: 4960
		public static readonly DerObjectIdentifier C = new DerObjectIdentifier("2.5.4.6");

		// Token: 0x04001361 RID: 4961
		public static readonly DerObjectIdentifier O = new DerObjectIdentifier("2.5.4.10");

		// Token: 0x04001362 RID: 4962
		public static readonly DerObjectIdentifier OU = new DerObjectIdentifier("2.5.4.11");

		// Token: 0x04001363 RID: 4963
		public static readonly DerObjectIdentifier T = new DerObjectIdentifier("2.5.4.12");

		// Token: 0x04001364 RID: 4964
		public static readonly DerObjectIdentifier CN = new DerObjectIdentifier("2.5.4.3");

		// Token: 0x04001365 RID: 4965
		public static readonly DerObjectIdentifier Street = new DerObjectIdentifier("2.5.4.9");

		// Token: 0x04001366 RID: 4966
		public static readonly DerObjectIdentifier SerialNumber = new DerObjectIdentifier("2.5.4.5");

		// Token: 0x04001367 RID: 4967
		public static readonly DerObjectIdentifier L = new DerObjectIdentifier("2.5.4.7");

		// Token: 0x04001368 RID: 4968
		public static readonly DerObjectIdentifier ST = new DerObjectIdentifier("2.5.4.8");

		// Token: 0x04001369 RID: 4969
		public static readonly DerObjectIdentifier Surname = new DerObjectIdentifier("2.5.4.4");

		// Token: 0x0400136A RID: 4970
		public static readonly DerObjectIdentifier GivenName = new DerObjectIdentifier("2.5.4.42");

		// Token: 0x0400136B RID: 4971
		public static readonly DerObjectIdentifier Initials = new DerObjectIdentifier("2.5.4.43");

		// Token: 0x0400136C RID: 4972
		public static readonly DerObjectIdentifier Generation = new DerObjectIdentifier("2.5.4.44");

		// Token: 0x0400136D RID: 4973
		public static readonly DerObjectIdentifier UniqueIdentifier = new DerObjectIdentifier("2.5.4.45");

		// Token: 0x0400136E RID: 4974
		public static readonly DerObjectIdentifier BusinessCategory = new DerObjectIdentifier("2.5.4.15");

		// Token: 0x0400136F RID: 4975
		public static readonly DerObjectIdentifier PostalCode = new DerObjectIdentifier("2.5.4.17");

		// Token: 0x04001370 RID: 4976
		public static readonly DerObjectIdentifier DnQualifier = new DerObjectIdentifier("2.5.4.46");

		// Token: 0x04001371 RID: 4977
		public static readonly DerObjectIdentifier Pseudonym = new DerObjectIdentifier("2.5.4.65");

		// Token: 0x04001372 RID: 4978
		public static readonly DerObjectIdentifier DateOfBirth = new DerObjectIdentifier("1.3.6.1.5.5.7.9.1");

		// Token: 0x04001373 RID: 4979
		public static readonly DerObjectIdentifier PlaceOfBirth = new DerObjectIdentifier("1.3.6.1.5.5.7.9.2");

		// Token: 0x04001374 RID: 4980
		public static readonly DerObjectIdentifier Gender = new DerObjectIdentifier("1.3.6.1.5.5.7.9.3");

		// Token: 0x04001375 RID: 4981
		public static readonly DerObjectIdentifier CountryOfCitizenship = new DerObjectIdentifier("1.3.6.1.5.5.7.9.4");

		// Token: 0x04001376 RID: 4982
		public static readonly DerObjectIdentifier CountryOfResidence = new DerObjectIdentifier("1.3.6.1.5.5.7.9.5");

		// Token: 0x04001377 RID: 4983
		public static readonly DerObjectIdentifier NameAtBirth = new DerObjectIdentifier("1.3.36.8.3.14");

		// Token: 0x04001378 RID: 4984
		public static readonly DerObjectIdentifier PostalAddress = new DerObjectIdentifier("2.5.4.16");

		// Token: 0x04001379 RID: 4985
		public static readonly DerObjectIdentifier DmdName = new DerObjectIdentifier("2.5.4.54");

		// Token: 0x0400137A RID: 4986
		public static readonly DerObjectIdentifier TelephoneNumber = X509ObjectIdentifiers.id_at_telephoneNumber;

		// Token: 0x0400137B RID: 4987
		public static readonly DerObjectIdentifier Name = X509ObjectIdentifiers.id_at_name;

		// Token: 0x0400137C RID: 4988
		public static readonly DerObjectIdentifier EmailAddress = PkcsObjectIdentifiers.Pkcs9AtEmailAddress;

		// Token: 0x0400137D RID: 4989
		public static readonly DerObjectIdentifier UnstructuredName = PkcsObjectIdentifiers.Pkcs9AtUnstructuredName;

		// Token: 0x0400137E RID: 4990
		public static readonly DerObjectIdentifier UnstructuredAddress = PkcsObjectIdentifiers.Pkcs9AtUnstructuredAddress;

		// Token: 0x0400137F RID: 4991
		public static readonly DerObjectIdentifier E = X509Name.EmailAddress;

		// Token: 0x04001380 RID: 4992
		public static readonly DerObjectIdentifier DC = new DerObjectIdentifier("0.9.2342.19200300.100.1.25");

		// Token: 0x04001381 RID: 4993
		public static readonly DerObjectIdentifier UID = new DerObjectIdentifier("0.9.2342.19200300.100.1.1");

		// Token: 0x04001382 RID: 4994
		private static readonly bool[] defaultReverse;

		// Token: 0x04001383 RID: 4995
		public static readonly Hashtable DefaultSymbols;

		// Token: 0x04001384 RID: 4996
		public static readonly Hashtable RFC2253Symbols;

		// Token: 0x04001385 RID: 4997
		public static readonly Hashtable RFC1779Symbols;

		// Token: 0x04001386 RID: 4998
		public static readonly Hashtable DefaultLookup;

		// Token: 0x04001387 RID: 4999
		[Obsolete("Use 'DefaultSymbols' instead")]
		public static readonly Hashtable OIDLookup;

		// Token: 0x04001388 RID: 5000
		[Obsolete("Use 'DefaultLookup' instead")]
		public static readonly Hashtable SymbolLookup;

		// Token: 0x04001389 RID: 5001
		private readonly ArrayList ordering = new ArrayList();

		// Token: 0x0400138A RID: 5002
		private readonly X509NameEntryConverter converter;

		// Token: 0x0400138B RID: 5003
		private ArrayList values = new ArrayList();

		// Token: 0x0400138C RID: 5004
		private ArrayList added = new ArrayList();

		// Token: 0x0400138D RID: 5005
		private Asn1Sequence seq;
	}
}
