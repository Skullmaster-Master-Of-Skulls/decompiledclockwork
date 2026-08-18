using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x0200032F RID: 815
	public abstract class XmlSerializationWriter : XmlSerializationGeneratedCode
	{
		// Token: 0x06002756 RID: 10070 RVA: 0x000C76E1 File Offset: 0x000C66E1
		internal void Init(XmlWriter w, XmlSerializerNamespaces namespaces, string encodingStyle, string idBase, TempAssembly tempAssembly)
		{
			this.w = w;
			this.namespaces = namespaces;
			this.soap12 = (encodingStyle == "http://www.w3.org/2003/05/soap-encoding");
			this.idBase = idBase;
			base.Init(tempAssembly);
		}

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x06002757 RID: 10071 RVA: 0x000C7712 File Offset: 0x000C6712
		// (set) Token: 0x06002758 RID: 10072 RVA: 0x000C771A File Offset: 0x000C671A
		protected bool EscapeName
		{
			get
			{
				return this.escapeName;
			}
			set
			{
				this.escapeName = value;
			}
		}

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x06002759 RID: 10073 RVA: 0x000C7723 File Offset: 0x000C6723
		// (set) Token: 0x0600275A RID: 10074 RVA: 0x000C772B File Offset: 0x000C672B
		protected XmlWriter Writer
		{
			get
			{
				return this.w;
			}
			set
			{
				this.w = value;
			}
		}

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x0600275B RID: 10075 RVA: 0x000C7734 File Offset: 0x000C6734
		// (set) Token: 0x0600275C RID: 10076 RVA: 0x000C774C File Offset: 0x000C674C
		protected ArrayList Namespaces
		{
			get
			{
				if (this.namespaces != null)
				{
					return this.namespaces.NamespaceList;
				}
				return null;
			}
			set
			{
				if (value == null)
				{
					this.namespaces = null;
					return;
				}
				XmlQualifiedName[] array = (XmlQualifiedName[])value.ToArray(typeof(XmlQualifiedName));
				this.namespaces = new XmlSerializerNamespaces(array);
			}
		}

		// Token: 0x0600275D RID: 10077 RVA: 0x000C7786 File Offset: 0x000C6786
		protected static byte[] FromByteArrayBase64(byte[] value)
		{
			return value;
		}

		// Token: 0x0600275E RID: 10078 RVA: 0x000C7789 File Offset: 0x000C6789
		protected static Assembly ResolveDynamicAssembly(string assemblyFullName)
		{
			return DynamicAssemblies.Get(assemblyFullName);
		}

		// Token: 0x0600275F RID: 10079 RVA: 0x000C7791 File Offset: 0x000C6791
		protected static string FromByteArrayHex(byte[] value)
		{
			return XmlCustomFormatter.FromByteArrayHex(value);
		}

		// Token: 0x06002760 RID: 10080 RVA: 0x000C7799 File Offset: 0x000C6799
		protected static string FromDateTime(DateTime value)
		{
			return XmlCustomFormatter.FromDateTime(value);
		}

		// Token: 0x06002761 RID: 10081 RVA: 0x000C77A1 File Offset: 0x000C67A1
		protected static string FromDate(DateTime value)
		{
			return XmlCustomFormatter.FromDate(value);
		}

		// Token: 0x06002762 RID: 10082 RVA: 0x000C77A9 File Offset: 0x000C67A9
		protected static string FromTime(DateTime value)
		{
			return XmlCustomFormatter.FromTime(value);
		}

		// Token: 0x06002763 RID: 10083 RVA: 0x000C77B1 File Offset: 0x000C67B1
		protected static string FromChar(char value)
		{
			return XmlCustomFormatter.FromChar(value);
		}

		// Token: 0x06002764 RID: 10084 RVA: 0x000C77B9 File Offset: 0x000C67B9
		protected static string FromEnum(long value, string[] values, long[] ids)
		{
			return XmlCustomFormatter.FromEnum(value, values, ids, null);
		}

		// Token: 0x06002765 RID: 10085 RVA: 0x000C77C4 File Offset: 0x000C67C4
		protected static string FromEnum(long value, string[] values, long[] ids, string typeName)
		{
			return XmlCustomFormatter.FromEnum(value, values, ids, typeName);
		}

		// Token: 0x06002766 RID: 10086 RVA: 0x000C77CF File Offset: 0x000C67CF
		protected static string FromXmlName(string name)
		{
			return XmlCustomFormatter.FromXmlName(name);
		}

		// Token: 0x06002767 RID: 10087 RVA: 0x000C77D7 File Offset: 0x000C67D7
		protected static string FromXmlNCName(string ncName)
		{
			return XmlCustomFormatter.FromXmlNCName(ncName);
		}

		// Token: 0x06002768 RID: 10088 RVA: 0x000C77DF File Offset: 0x000C67DF
		protected static string FromXmlNmToken(string nmToken)
		{
			return XmlCustomFormatter.FromXmlNmToken(nmToken);
		}

		// Token: 0x06002769 RID: 10089 RVA: 0x000C77E7 File Offset: 0x000C67E7
		protected static string FromXmlNmTokens(string nmTokens)
		{
			return XmlCustomFormatter.FromXmlNmTokens(nmTokens);
		}

		// Token: 0x0600276A RID: 10090 RVA: 0x000C77EF File Offset: 0x000C67EF
		protected void WriteXsiType(string name, string ns)
		{
			this.WriteAttribute("type", "http://www.w3.org/2001/XMLSchema-instance", this.GetQualifiedName(name, ns));
		}

		// Token: 0x0600276B RID: 10091 RVA: 0x000C7809 File Offset: 0x000C6809
		private XmlQualifiedName GetPrimitiveTypeName(Type type)
		{
			return this.GetPrimitiveTypeName(type, true);
		}

		// Token: 0x0600276C RID: 10092 RVA: 0x000C7814 File Offset: 0x000C6814
		private XmlQualifiedName GetPrimitiveTypeName(Type type, bool throwIfUnknown)
		{
			XmlQualifiedName primitiveTypeNameInternal = XmlSerializationWriter.GetPrimitiveTypeNameInternal(type);
			if (throwIfUnknown && primitiveTypeNameInternal == null)
			{
				throw this.CreateUnknownTypeException(type);
			}
			return primitiveTypeNameInternal;
		}

		// Token: 0x0600276D RID: 10093 RVA: 0x000C7840 File Offset: 0x000C6840
		internal static XmlQualifiedName GetPrimitiveTypeNameInternal(Type type)
		{
			string ns = "http://www.w3.org/2001/XMLSchema";
			string name;
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Boolean:
				name = "boolean";
				goto IL_155;
			case TypeCode.Char:
				name = "char";
				ns = "http://microsoft.com/wsdl/types/";
				goto IL_155;
			case TypeCode.SByte:
				name = "byte";
				goto IL_155;
			case TypeCode.Byte:
				name = "unsignedByte";
				goto IL_155;
			case TypeCode.Int16:
				name = "short";
				goto IL_155;
			case TypeCode.UInt16:
				name = "unsignedShort";
				goto IL_155;
			case TypeCode.Int32:
				name = "int";
				goto IL_155;
			case TypeCode.UInt32:
				name = "unsignedInt";
				goto IL_155;
			case TypeCode.Int64:
				name = "long";
				goto IL_155;
			case TypeCode.UInt64:
				name = "unsignedLong";
				goto IL_155;
			case TypeCode.Single:
				name = "float";
				goto IL_155;
			case TypeCode.Double:
				name = "double";
				goto IL_155;
			case TypeCode.Decimal:
				name = "decimal";
				goto IL_155;
			case TypeCode.DateTime:
				name = "dateTime";
				goto IL_155;
			case TypeCode.String:
				name = "string";
				goto IL_155;
			}
			if (type == typeof(XmlQualifiedName))
			{
				name = "QName";
			}
			else if (type == typeof(byte[]))
			{
				name = "base64Binary";
			}
			else if (type == typeof(Guid))
			{
				name = "guid";
				ns = "http://microsoft.com/wsdl/types/";
			}
			else
			{
				if (type != typeof(XmlNode[]))
				{
					return null;
				}
				name = "anyType";
			}
			IL_155:
			return new XmlQualifiedName(name, ns);
		}

		// Token: 0x0600276E RID: 10094 RVA: 0x000C79AC File Offset: 0x000C69AC
		protected void WriteTypedPrimitive(string name, string ns, object o, bool xsiType)
		{
			string ns2 = "http://www.w3.org/2001/XMLSchema";
			bool flag = true;
			bool flag2 = false;
			Type type = o.GetType();
			bool flag3 = false;
			string text;
			string text2;
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Boolean:
				text = XmlConvert.ToString((bool)o);
				text2 = "boolean";
				goto IL_2E2;
			case TypeCode.Char:
				text = XmlSerializationWriter.FromChar((char)o);
				text2 = "char";
				ns2 = "http://microsoft.com/wsdl/types/";
				goto IL_2E2;
			case TypeCode.SByte:
				text = XmlConvert.ToString((sbyte)o);
				text2 = "byte";
				goto IL_2E2;
			case TypeCode.Byte:
				text = XmlConvert.ToString((byte)o);
				text2 = "unsignedByte";
				goto IL_2E2;
			case TypeCode.Int16:
				text = XmlConvert.ToString((short)o);
				text2 = "short";
				goto IL_2E2;
			case TypeCode.UInt16:
				text = XmlConvert.ToString((ushort)o);
				text2 = "unsignedShort";
				goto IL_2E2;
			case TypeCode.Int32:
				text = XmlConvert.ToString((int)o);
				text2 = "int";
				goto IL_2E2;
			case TypeCode.UInt32:
				text = XmlConvert.ToString((uint)o);
				text2 = "unsignedInt";
				goto IL_2E2;
			case TypeCode.Int64:
				text = XmlConvert.ToString((long)o);
				text2 = "long";
				goto IL_2E2;
			case TypeCode.UInt64:
				text = XmlConvert.ToString((ulong)o);
				text2 = "unsignedLong";
				goto IL_2E2;
			case TypeCode.Single:
				text = XmlConvert.ToString((float)o);
				text2 = "float";
				goto IL_2E2;
			case TypeCode.Double:
				text = XmlConvert.ToString((double)o);
				text2 = "double";
				goto IL_2E2;
			case TypeCode.Decimal:
				text = XmlConvert.ToString((decimal)o);
				text2 = "decimal";
				goto IL_2E2;
			case TypeCode.DateTime:
				text = XmlSerializationWriter.FromDateTime((DateTime)o);
				text2 = "dateTime";
				goto IL_2E2;
			case TypeCode.String:
				text = (string)o;
				text2 = "string";
				flag = false;
				goto IL_2E2;
			}
			if (type == typeof(XmlQualifiedName))
			{
				text2 = "QName";
				flag3 = true;
				if (name == null)
				{
					this.w.WriteStartElement(text2, ns2);
				}
				else
				{
					this.w.WriteStartElement(name, ns);
				}
				text = this.FromXmlQualifiedName((XmlQualifiedName)o, false);
			}
			else if (type == typeof(byte[]))
			{
				text = string.Empty;
				flag2 = true;
				text2 = "base64Binary";
			}
			else if (type == typeof(Guid))
			{
				text = XmlConvert.ToString((Guid)o);
				text2 = "guid";
				ns2 = "http://microsoft.com/wsdl/types/";
			}
			else
			{
				if (typeof(XmlNode[]).IsAssignableFrom(type))
				{
					if (name == null)
					{
						this.w.WriteStartElement("anyType", "http://www.w3.org/2001/XMLSchema");
					}
					else
					{
						this.w.WriteStartElement(name, ns);
					}
					XmlNode[] array = (XmlNode[])o;
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i] != null)
						{
							array[i].WriteTo(this.w);
						}
					}
					this.w.WriteEndElement();
					return;
				}
				throw this.CreateUnknownTypeException(type);
			}
			IL_2E2:
			if (!flag3)
			{
				if (name == null)
				{
					this.w.WriteStartElement(text2, ns2);
				}
				else
				{
					this.w.WriteStartElement(name, ns);
				}
			}
			if (xsiType)
			{
				this.WriteXsiType(text2, ns2);
			}
			if (text == null)
			{
				this.w.WriteAttributeString("nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
			}
			else if (flag2)
			{
				XmlCustomFormatter.WriteArrayBase64(this.w, (byte[])o, 0, ((byte[])o).Length);
			}
			else if (flag)
			{
				this.w.WriteRaw(text);
			}
			else
			{
				this.w.WriteString(text);
			}
			this.w.WriteEndElement();
		}

		// Token: 0x0600276F RID: 10095 RVA: 0x000C7D34 File Offset: 0x000C6D34
		private string GetQualifiedName(string name, string ns)
		{
			if (ns == null || ns.Length == 0)
			{
				return name;
			}
			string text = this.w.LookupPrefix(ns);
			if (text == null)
			{
				if (ns == "http://www.w3.org/XML/1998/namespace")
				{
					text = "xml";
				}
				else
				{
					text = this.NextPrefix();
					this.WriteAttribute("xmlns", text, null, ns);
				}
			}
			else if (text.Length == 0)
			{
				return name;
			}
			return text + ":" + name;
		}

		// Token: 0x06002770 RID: 10096 RVA: 0x000C7DA0 File Offset: 0x000C6DA0
		protected string FromXmlQualifiedName(XmlQualifiedName xmlQualifiedName)
		{
			return this.FromXmlQualifiedName(xmlQualifiedName, true);
		}

		// Token: 0x06002771 RID: 10097 RVA: 0x000C7DAC File Offset: 0x000C6DAC
		protected string FromXmlQualifiedName(XmlQualifiedName xmlQualifiedName, bool ignoreEmpty)
		{
			if (xmlQualifiedName == null)
			{
				return null;
			}
			if (xmlQualifiedName.IsEmpty && ignoreEmpty)
			{
				return null;
			}
			return this.GetQualifiedName(this.EscapeName ? XmlConvert.EncodeLocalName(xmlQualifiedName.Name) : xmlQualifiedName.Name, xmlQualifiedName.Namespace);
		}

		// Token: 0x06002772 RID: 10098 RVA: 0x000C7DF8 File Offset: 0x000C6DF8
		protected void WriteStartElement(string name)
		{
			this.WriteStartElement(name, null, null, false, null);
		}

		// Token: 0x06002773 RID: 10099 RVA: 0x000C7E05 File Offset: 0x000C6E05
		protected void WriteStartElement(string name, string ns)
		{
			this.WriteStartElement(name, ns, null, false, null);
		}

		// Token: 0x06002774 RID: 10100 RVA: 0x000C7E12 File Offset: 0x000C6E12
		protected void WriteStartElement(string name, string ns, bool writePrefixed)
		{
			this.WriteStartElement(name, ns, null, writePrefixed, null);
		}

		// Token: 0x06002775 RID: 10101 RVA: 0x000C7E1F File Offset: 0x000C6E1F
		protected void WriteStartElement(string name, string ns, object o)
		{
			this.WriteStartElement(name, ns, o, false, null);
		}

		// Token: 0x06002776 RID: 10102 RVA: 0x000C7E2C File Offset: 0x000C6E2C
		protected void WriteStartElement(string name, string ns, object o, bool writePrefixed)
		{
			this.WriteStartElement(name, ns, o, writePrefixed, null);
		}

		// Token: 0x06002777 RID: 10103 RVA: 0x000C7E3C File Offset: 0x000C6E3C
		protected void WriteStartElement(string name, string ns, object o, bool writePrefixed, XmlSerializerNamespaces xmlns)
		{
			if (o != null && this.objectsInUse != null)
			{
				if (this.objectsInUse.ContainsKey(o))
				{
					throw new InvalidOperationException(Res.GetString("XmlCircularReference", new object[]
					{
						o.GetType().FullName
					}));
				}
				this.objectsInUse.Add(o, o);
			}
			string text = null;
			bool flag = false;
			if (this.namespaces != null)
			{
				foreach (object obj in this.namespaces.Namespaces.Keys)
				{
					string text2 = (string)obj;
					string text3 = (string)this.namespaces.Namespaces[text2];
					if (text2.Length > 0 && text3 == ns)
					{
						text = text2;
					}
					if (text2.Length == 0)
					{
						if (text3 == null || text3.Length == 0)
						{
							flag = true;
						}
						if (ns != text3)
						{
							writePrefixed = true;
						}
					}
				}
				this.usedPrefixes = this.ListUsedPrefixes(this.namespaces.Namespaces, this.aliasBase);
			}
			if (writePrefixed && text == null && ns != null && ns.Length > 0)
			{
				text = this.w.LookupPrefix(ns);
				if (text == null || text.Length == 0)
				{
					text = this.NextPrefix();
				}
			}
			if (text == null && xmlns != null)
			{
				text = xmlns.LookupPrefix(ns);
			}
			if (flag && text == null && ns != null && ns.Length != 0)
			{
				text = this.NextPrefix();
			}
			this.w.WriteStartElement(text, name, ns);
			if (this.namespaces != null)
			{
				foreach (object obj2 in this.namespaces.Namespaces.Keys)
				{
					string text4 = (string)obj2;
					string text5 = (string)this.namespaces.Namespaces[text4];
					if (text4.Length != 0 || (text5 != null && text5.Length != 0))
					{
						if (text5 == null || text5.Length == 0)
						{
							if (text4.Length > 0)
							{
								throw new InvalidOperationException(Res.GetString("XmlInvalidXmlns", new object[]
								{
									text4
								}));
							}
							this.WriteAttribute("xmlns", text4, null, text5);
						}
						else if (this.w.LookupPrefix(text5) == null)
						{
							if (text == null && text4.Length == 0)
							{
								break;
							}
							this.WriteAttribute("xmlns", text4, null, text5);
						}
					}
				}
			}
			this.WriteNamespaceDeclarations(xmlns);
		}

		// Token: 0x06002778 RID: 10104 RVA: 0x000C80E0 File Offset: 0x000C70E0
		private Hashtable ListUsedPrefixes(Hashtable nsList, string prefix)
		{
			Hashtable hashtable = new Hashtable();
			int length = prefix.Length;
			foreach (object obj in this.namespaces.Namespaces.Keys)
			{
				string text = (string)obj;
				if (text.Length > length)
				{
					string text2 = text;
					int length2 = text2.Length;
					if (text2.Length > length && text2.Length <= length + "2147483647".Length && text2.StartsWith(prefix, StringComparison.Ordinal))
					{
						bool flag = true;
						for (int i = length; i < text2.Length; i++)
						{
							if (!char.IsDigit(text2, i))
							{
								flag = false;
								break;
							}
						}
						if (flag)
						{
							long num = long.Parse(text2.Substring(length), CultureInfo.InvariantCulture);
							if (num <= 2147483647L)
							{
								int num2 = (int)num;
								if (!hashtable.ContainsKey(num2))
								{
									hashtable.Add(num2, num2);
								}
							}
						}
					}
				}
			}
			if (hashtable.Count > 0)
			{
				return hashtable;
			}
			return null;
		}

		// Token: 0x06002779 RID: 10105 RVA: 0x000C8214 File Offset: 0x000C7214
		protected void WriteNullTagEncoded(string name)
		{
			this.WriteNullTagEncoded(name, null);
		}

		// Token: 0x0600277A RID: 10106 RVA: 0x000C821E File Offset: 0x000C721E
		protected void WriteNullTagEncoded(string name, string ns)
		{
			if (name == null || name.Length == 0)
			{
				return;
			}
			this.WriteStartElement(name, ns, null, true);
			this.w.WriteAttributeString("nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
			this.w.WriteEndElement();
		}

		// Token: 0x0600277B RID: 10107 RVA: 0x000C825B File Offset: 0x000C725B
		protected void WriteNullTagLiteral(string name)
		{
			this.WriteNullTagLiteral(name, null);
		}

		// Token: 0x0600277C RID: 10108 RVA: 0x000C8265 File Offset: 0x000C7265
		protected void WriteNullTagLiteral(string name, string ns)
		{
			if (name == null || name.Length == 0)
			{
				return;
			}
			this.WriteStartElement(name, ns, null, false);
			this.w.WriteAttributeString("nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
			this.w.WriteEndElement();
		}

		// Token: 0x0600277D RID: 10109 RVA: 0x000C82A2 File Offset: 0x000C72A2
		protected void WriteEmptyTag(string name)
		{
			this.WriteEmptyTag(name, null);
		}

		// Token: 0x0600277E RID: 10110 RVA: 0x000C82AC File Offset: 0x000C72AC
		protected void WriteEmptyTag(string name, string ns)
		{
			if (name == null || name.Length == 0)
			{
				return;
			}
			this.WriteStartElement(name, ns, null, false);
			this.w.WriteEndElement();
		}

		// Token: 0x0600277F RID: 10111 RVA: 0x000C82CF File Offset: 0x000C72CF
		protected void WriteEndElement()
		{
			this.w.WriteEndElement();
		}

		// Token: 0x06002780 RID: 10112 RVA: 0x000C82DC File Offset: 0x000C72DC
		protected void WriteEndElement(object o)
		{
			this.w.WriteEndElement();
			if (o != null && this.objectsInUse != null)
			{
				this.objectsInUse.Remove(o);
			}
		}

		// Token: 0x06002781 RID: 10113 RVA: 0x000C8300 File Offset: 0x000C7300
		protected void WriteSerializable(IXmlSerializable serializable, string name, string ns, bool isNullable)
		{
			this.WriteSerializable(serializable, name, ns, isNullable, true);
		}

		// Token: 0x06002782 RID: 10114 RVA: 0x000C830E File Offset: 0x000C730E
		protected void WriteSerializable(IXmlSerializable serializable, string name, string ns, bool isNullable, bool wrapped)
		{
			if (serializable == null)
			{
				if (isNullable)
				{
					this.WriteNullTagLiteral(name, ns);
				}
				return;
			}
			if (wrapped)
			{
				this.w.WriteStartElement(name, ns);
			}
			serializable.WriteXml(this.w);
			if (wrapped)
			{
				this.w.WriteEndElement();
			}
		}

		// Token: 0x06002783 RID: 10115 RVA: 0x000C834C File Offset: 0x000C734C
		protected void WriteNullableStringEncoded(string name, string ns, string value, XmlQualifiedName xsiType)
		{
			if (value == null)
			{
				this.WriteNullTagEncoded(name, ns);
				return;
			}
			this.WriteElementString(name, ns, value, xsiType);
		}

		// Token: 0x06002784 RID: 10116 RVA: 0x000C8365 File Offset: 0x000C7365
		protected void WriteNullableStringLiteral(string name, string ns, string value)
		{
			if (value == null)
			{
				this.WriteNullTagLiteral(name, ns);
				return;
			}
			this.WriteElementString(name, ns, value, null);
		}

		// Token: 0x06002785 RID: 10117 RVA: 0x000C837D File Offset: 0x000C737D
		protected void WriteNullableStringEncodedRaw(string name, string ns, string value, XmlQualifiedName xsiType)
		{
			if (value == null)
			{
				this.WriteNullTagEncoded(name, ns);
				return;
			}
			this.WriteElementStringRaw(name, ns, value, xsiType);
		}

		// Token: 0x06002786 RID: 10118 RVA: 0x000C8396 File Offset: 0x000C7396
		protected void WriteNullableStringEncodedRaw(string name, string ns, byte[] value, XmlQualifiedName xsiType)
		{
			if (value == null)
			{
				this.WriteNullTagEncoded(name, ns);
				return;
			}
			this.WriteElementStringRaw(name, ns, value, xsiType);
		}

		// Token: 0x06002787 RID: 10119 RVA: 0x000C83AF File Offset: 0x000C73AF
		protected void WriteNullableStringLiteralRaw(string name, string ns, string value)
		{
			if (value == null)
			{
				this.WriteNullTagLiteral(name, ns);
				return;
			}
			this.WriteElementStringRaw(name, ns, value, null);
		}

		// Token: 0x06002788 RID: 10120 RVA: 0x000C83C7 File Offset: 0x000C73C7
		protected void WriteNullableStringLiteralRaw(string name, string ns, byte[] value)
		{
			if (value == null)
			{
				this.WriteNullTagLiteral(name, ns);
				return;
			}
			this.WriteElementStringRaw(name, ns, value, null);
		}

		// Token: 0x06002789 RID: 10121 RVA: 0x000C83DF File Offset: 0x000C73DF
		protected void WriteNullableQualifiedNameEncoded(string name, string ns, XmlQualifiedName value, XmlQualifiedName xsiType)
		{
			if (value == null)
			{
				this.WriteNullTagEncoded(name, ns);
				return;
			}
			this.WriteElementQualifiedName(name, ns, value, xsiType);
		}

		// Token: 0x0600278A RID: 10122 RVA: 0x000C83FE File Offset: 0x000C73FE
		protected void WriteNullableQualifiedNameLiteral(string name, string ns, XmlQualifiedName value)
		{
			if (value == null)
			{
				this.WriteNullTagLiteral(name, ns);
				return;
			}
			this.WriteElementQualifiedName(name, ns, value, null);
		}

		// Token: 0x0600278B RID: 10123 RVA: 0x000C841C File Offset: 0x000C741C
		protected void WriteElementEncoded(XmlNode node, string name, string ns, bool isNullable, bool any)
		{
			if (node == null)
			{
				if (isNullable)
				{
					this.WriteNullTagEncoded(name, ns);
				}
				return;
			}
			this.WriteElement(node, name, ns, isNullable, any);
		}

		// Token: 0x0600278C RID: 10124 RVA: 0x000C843B File Offset: 0x000C743B
		protected void WriteElementLiteral(XmlNode node, string name, string ns, bool isNullable, bool any)
		{
			if (node == null)
			{
				if (isNullable)
				{
					this.WriteNullTagLiteral(name, ns);
				}
				return;
			}
			this.WriteElement(node, name, ns, isNullable, any);
		}

		// Token: 0x0600278D RID: 10125 RVA: 0x000C845C File Offset: 0x000C745C
		private void WriteElement(XmlNode node, string name, string ns, bool isNullable, bool any)
		{
			if (typeof(XmlAttribute).IsAssignableFrom(node.GetType()))
			{
				throw new InvalidOperationException(Res.GetString("XmlNoAttributeHere"));
			}
			if (node is XmlDocument)
			{
				node = ((XmlDocument)node).DocumentElement;
				if (node == null)
				{
					if (isNullable)
					{
						this.WriteNullTagEncoded(name, ns);
					}
					return;
				}
			}
			if (any)
			{
				if (node is XmlElement && name != null && name.Length > 0 && (node.LocalName != name || node.NamespaceURI != ns))
				{
					throw new InvalidOperationException(Res.GetString("XmlElementNameMismatch", new object[]
					{
						node.LocalName,
						node.NamespaceURI,
						name,
						ns
					}));
				}
			}
			else
			{
				this.w.WriteStartElement(name, ns);
			}
			node.WriteTo(this.w);
			if (!any)
			{
				this.w.WriteEndElement();
			}
		}

		// Token: 0x0600278E RID: 10126 RVA: 0x000C8543 File Offset: 0x000C7543
		protected Exception CreateUnknownTypeException(object o)
		{
			return this.CreateUnknownTypeException(o.GetType());
		}

		// Token: 0x0600278F RID: 10127 RVA: 0x000C8554 File Offset: 0x000C7554
		protected Exception CreateUnknownTypeException(Type type)
		{
			if (typeof(IXmlSerializable).IsAssignableFrom(type))
			{
				return new InvalidOperationException(Res.GetString("XmlInvalidSerializable", new object[]
				{
					type.FullName
				}));
			}
			TypeDesc typeDesc = new TypeScope().GetTypeDesc(type);
			if (!typeDesc.IsStructLike)
			{
				return new InvalidOperationException(Res.GetString("XmlInvalidUseOfType", new object[]
				{
					type.FullName
				}));
			}
			return new InvalidOperationException(Res.GetString("XmlUnxpectedType", new object[]
			{
				type.FullName
			}));
		}

		// Token: 0x06002790 RID: 10128 RVA: 0x000C85EC File Offset: 0x000C75EC
		protected Exception CreateMismatchChoiceException(string value, string elementName, string enumValue)
		{
			return new InvalidOperationException(Res.GetString("XmlChoiceMismatchChoiceException", new object[]
			{
				elementName,
				value,
				enumValue
			}));
		}

		// Token: 0x06002791 RID: 10129 RVA: 0x000C861C File Offset: 0x000C761C
		protected Exception CreateUnknownAnyElementException(string name, string ns)
		{
			return new InvalidOperationException(Res.GetString("XmlUnknownAnyElement", new object[]
			{
				name,
				ns
			}));
		}

		// Token: 0x06002792 RID: 10130 RVA: 0x000C8648 File Offset: 0x000C7648
		protected Exception CreateInvalidChoiceIdentifierValueException(string type, string identifier)
		{
			return new InvalidOperationException(Res.GetString("XmlInvalidChoiceIdentifierValue", new object[]
			{
				type,
				identifier
			}));
		}

		// Token: 0x06002793 RID: 10131 RVA: 0x000C8674 File Offset: 0x000C7674
		protected Exception CreateChoiceIdentifierValueException(string value, string identifier, string name, string ns)
		{
			return new InvalidOperationException(Res.GetString("XmlChoiceIdentifierMismatch", new object[]
			{
				value,
				identifier,
				name,
				ns
			}));
		}

		// Token: 0x06002794 RID: 10132 RVA: 0x000C86AC File Offset: 0x000C76AC
		protected Exception CreateInvalidEnumValueException(object value, string typeName)
		{
			return new InvalidOperationException(Res.GetString("XmlUnknownConstant", new object[]
			{
				value,
				typeName
			}));
		}

		// Token: 0x06002795 RID: 10133 RVA: 0x000C86D8 File Offset: 0x000C76D8
		protected Exception CreateInvalidAnyTypeException(object o)
		{
			return this.CreateInvalidAnyTypeException(o.GetType());
		}

		// Token: 0x06002796 RID: 10134 RVA: 0x000C86E8 File Offset: 0x000C76E8
		protected Exception CreateInvalidAnyTypeException(Type type)
		{
			return new InvalidOperationException(Res.GetString("XmlIllegalAnyElement", new object[]
			{
				type.FullName
			}));
		}

		// Token: 0x06002797 RID: 10135 RVA: 0x000C8715 File Offset: 0x000C7715
		protected void WriteReferencingElement(string n, string ns, object o)
		{
			this.WriteReferencingElement(n, ns, o, false);
		}

		// Token: 0x06002798 RID: 10136 RVA: 0x000C8724 File Offset: 0x000C7724
		protected void WriteReferencingElement(string n, string ns, object o, bool isNullable)
		{
			if (o == null)
			{
				if (isNullable)
				{
					this.WriteNullTagEncoded(n, ns);
				}
				return;
			}
			this.WriteStartElement(n, ns, null, true);
			if (this.soap12)
			{
				this.w.WriteAttributeString("ref", "http://www.w3.org/2003/05/soap-encoding", this.GetId(o, true));
			}
			else
			{
				this.w.WriteAttributeString("href", "#" + this.GetId(o, true));
			}
			this.w.WriteEndElement();
		}

		// Token: 0x06002799 RID: 10137 RVA: 0x000C879F File Offset: 0x000C779F
		private bool IsIdDefined(object o)
		{
			return this.references != null && this.references.Contains(o);
		}

		// Token: 0x0600279A RID: 10138 RVA: 0x000C87B8 File Offset: 0x000C77B8
		private string GetId(object o, bool addToReferencesList)
		{
			if (this.references == null)
			{
				this.references = new Hashtable();
				this.referencesToWrite = new ArrayList();
			}
			string text = (string)this.references[o];
			if (text == null)
			{
				string str = this.idBase;
				string str2 = "id";
				int num = ++this.nextId;
				text = str + str2 + num.ToString(CultureInfo.InvariantCulture);
				this.references.Add(o, text);
				if (addToReferencesList)
				{
					this.referencesToWrite.Add(o);
				}
			}
			return text;
		}

		// Token: 0x0600279B RID: 10139 RVA: 0x000C8845 File Offset: 0x000C7845
		protected void WriteId(object o)
		{
			this.WriteId(o, true);
		}

		// Token: 0x0600279C RID: 10140 RVA: 0x000C884F File Offset: 0x000C784F
		private void WriteId(object o, bool addToReferencesList)
		{
			if (this.soap12)
			{
				this.w.WriteAttributeString("id", "http://www.w3.org/2003/05/soap-encoding", this.GetId(o, addToReferencesList));
				return;
			}
			this.w.WriteAttributeString("id", this.GetId(o, addToReferencesList));
		}

		// Token: 0x0600279D RID: 10141 RVA: 0x000C888F File Offset: 0x000C788F
		protected void WriteXmlAttribute(XmlNode node)
		{
			this.WriteXmlAttribute(node, null);
		}

		// Token: 0x0600279E RID: 10142 RVA: 0x000C889C File Offset: 0x000C789C
		protected void WriteXmlAttribute(XmlNode node, object container)
		{
			XmlAttribute xmlAttribute = node as XmlAttribute;
			if (xmlAttribute == null)
			{
				throw new InvalidOperationException(Res.GetString("XmlNeedAttributeHere"));
			}
			if (xmlAttribute.Value != null)
			{
				if (xmlAttribute.NamespaceURI == "http://schemas.xmlsoap.org/wsdl/" && xmlAttribute.LocalName == "arrayType")
				{
					string str;
					XmlQualifiedName xmlQualifiedName = TypeScope.ParseWsdlArrayType(xmlAttribute.Value, out str, (container is XmlSchemaObject) ? ((XmlSchemaObject)container) : null);
					string value = this.FromXmlQualifiedName(xmlQualifiedName, true) + str;
					this.WriteAttribute("arrayType", "http://schemas.xmlsoap.org/wsdl/", value);
					return;
				}
				this.WriteAttribute(xmlAttribute.Name, xmlAttribute.NamespaceURI, xmlAttribute.Value);
			}
		}

		// Token: 0x0600279F RID: 10143 RVA: 0x000C8948 File Offset: 0x000C7948
		protected void WriteAttribute(string localName, string ns, string value)
		{
			if (value == null)
			{
				return;
			}
			if (!(localName == "xmlns"))
			{
				if (localName.StartsWith("xmlns:", StringComparison.Ordinal))
				{
					return;
				}
				int num = localName.IndexOf(':');
				if (num < 0)
				{
					if (ns == "http://www.w3.org/XML/1998/namespace")
					{
						string text = this.w.LookupPrefix(ns);
						if (text == null || text.Length == 0)
						{
							text = "xml";
						}
						this.w.WriteAttributeString(text, localName, ns, value);
						return;
					}
					this.w.WriteAttributeString(localName, ns, value);
					return;
				}
				else
				{
					string prefix = localName.Substring(0, num);
					this.w.WriteAttributeString(prefix, localName.Substring(num + 1), ns, value);
				}
			}
		}

		// Token: 0x060027A0 RID: 10144 RVA: 0x000C89F0 File Offset: 0x000C79F0
		protected void WriteAttribute(string localName, string ns, byte[] value)
		{
			if (value == null)
			{
				return;
			}
			if (!(localName == "xmlns"))
			{
				if (localName.StartsWith("xmlns:", StringComparison.Ordinal))
				{
					return;
				}
				int num = localName.IndexOf(':');
				if (num < 0)
				{
					if (ns == "http://www.w3.org/XML/1998/namespace")
					{
						string text = this.w.LookupPrefix(ns);
						if (text == null || text.Length == 0)
						{
						}
						this.w.WriteStartAttribute("xml", localName, ns);
					}
					else
					{
						this.w.WriteStartAttribute(null, localName, ns);
					}
				}
				else
				{
					string prefix = localName.Substring(0, num);
					prefix = this.w.LookupPrefix(ns);
					this.w.WriteStartAttribute(prefix, localName.Substring(num + 1), ns);
				}
				XmlCustomFormatter.WriteArrayBase64(this.w, value, 0, value.Length);
				this.w.WriteEndAttribute();
			}
		}

		// Token: 0x060027A1 RID: 10145 RVA: 0x000C8AC3 File Offset: 0x000C7AC3
		protected void WriteAttribute(string localName, string value)
		{
			if (value == null)
			{
				return;
			}
			this.w.WriteAttributeString(localName, null, value);
		}

		// Token: 0x060027A2 RID: 10146 RVA: 0x000C8AD7 File Offset: 0x000C7AD7
		protected void WriteAttribute(string localName, byte[] value)
		{
			if (value == null)
			{
				return;
			}
			this.w.WriteStartAttribute(null, localName, null);
			XmlCustomFormatter.WriteArrayBase64(this.w, value, 0, value.Length);
			this.w.WriteEndAttribute();
		}

		// Token: 0x060027A3 RID: 10147 RVA: 0x000C8B06 File Offset: 0x000C7B06
		protected void WriteAttribute(string prefix, string localName, string ns, string value)
		{
			if (value == null)
			{
				return;
			}
			this.w.WriteAttributeString(prefix, localName, null, value);
		}

		// Token: 0x060027A4 RID: 10148 RVA: 0x000C8B1D File Offset: 0x000C7B1D
		protected void WriteValue(string value)
		{
			if (value == null)
			{
				return;
			}
			this.w.WriteString(value);
		}

		// Token: 0x060027A5 RID: 10149 RVA: 0x000C8B2F File Offset: 0x000C7B2F
		protected void WriteValue(byte[] value)
		{
			if (value == null)
			{
				return;
			}
			XmlCustomFormatter.WriteArrayBase64(this.w, value, 0, value.Length);
		}

		// Token: 0x060027A6 RID: 10150 RVA: 0x000C8B45 File Offset: 0x000C7B45
		protected void WriteStartDocument()
		{
			if (this.w.WriteState == WriteState.Start)
			{
				this.w.WriteStartDocument();
			}
		}

		// Token: 0x060027A7 RID: 10151 RVA: 0x000C8B5F File Offset: 0x000C7B5F
		protected void WriteElementString(string localName, string value)
		{
			this.WriteElementString(localName, null, value, null);
		}

		// Token: 0x060027A8 RID: 10152 RVA: 0x000C8B6B File Offset: 0x000C7B6B
		protected void WriteElementString(string localName, string ns, string value)
		{
			this.WriteElementString(localName, ns, value, null);
		}

		// Token: 0x060027A9 RID: 10153 RVA: 0x000C8B77 File Offset: 0x000C7B77
		protected void WriteElementString(string localName, string value, XmlQualifiedName xsiType)
		{
			this.WriteElementString(localName, null, value, xsiType);
		}

		// Token: 0x060027AA RID: 10154 RVA: 0x000C8B84 File Offset: 0x000C7B84
		protected void WriteElementString(string localName, string ns, string value, XmlQualifiedName xsiType)
		{
			if (value == null)
			{
				return;
			}
			if (xsiType == null)
			{
				this.w.WriteElementString(localName, ns, value);
				return;
			}
			this.w.WriteStartElement(localName, ns);
			this.WriteXsiType(xsiType.Name, xsiType.Namespace);
			this.w.WriteString(value);
			this.w.WriteEndElement();
		}

		// Token: 0x060027AB RID: 10155 RVA: 0x000C8BE6 File Offset: 0x000C7BE6
		protected void WriteElementStringRaw(string localName, string value)
		{
			this.WriteElementStringRaw(localName, null, value, null);
		}

		// Token: 0x060027AC RID: 10156 RVA: 0x000C8BF2 File Offset: 0x000C7BF2
		protected void WriteElementStringRaw(string localName, byte[] value)
		{
			this.WriteElementStringRaw(localName, null, value, null);
		}

		// Token: 0x060027AD RID: 10157 RVA: 0x000C8BFE File Offset: 0x000C7BFE
		protected void WriteElementStringRaw(string localName, string ns, string value)
		{
			this.WriteElementStringRaw(localName, ns, value, null);
		}

		// Token: 0x060027AE RID: 10158 RVA: 0x000C8C0A File Offset: 0x000C7C0A
		protected void WriteElementStringRaw(string localName, string ns, byte[] value)
		{
			this.WriteElementStringRaw(localName, ns, value, null);
		}

		// Token: 0x060027AF RID: 10159 RVA: 0x000C8C16 File Offset: 0x000C7C16
		protected void WriteElementStringRaw(string localName, string value, XmlQualifiedName xsiType)
		{
			this.WriteElementStringRaw(localName, null, value, xsiType);
		}

		// Token: 0x060027B0 RID: 10160 RVA: 0x000C8C22 File Offset: 0x000C7C22
		protected void WriteElementStringRaw(string localName, byte[] value, XmlQualifiedName xsiType)
		{
			this.WriteElementStringRaw(localName, null, value, xsiType);
		}

		// Token: 0x060027B1 RID: 10161 RVA: 0x000C8C30 File Offset: 0x000C7C30
		protected void WriteElementStringRaw(string localName, string ns, string value, XmlQualifiedName xsiType)
		{
			if (value == null)
			{
				return;
			}
			this.w.WriteStartElement(localName, ns);
			if (xsiType != null)
			{
				this.WriteXsiType(xsiType.Name, xsiType.Namespace);
			}
			this.w.WriteRaw(value);
			this.w.WriteEndElement();
		}

		// Token: 0x060027B2 RID: 10162 RVA: 0x000C8C84 File Offset: 0x000C7C84
		protected void WriteElementStringRaw(string localName, string ns, byte[] value, XmlQualifiedName xsiType)
		{
			if (value == null)
			{
				return;
			}
			this.w.WriteStartElement(localName, ns);
			if (xsiType != null)
			{
				this.WriteXsiType(xsiType.Name, xsiType.Namespace);
			}
			XmlCustomFormatter.WriteArrayBase64(this.w, value, 0, value.Length);
			this.w.WriteEndElement();
		}

		// Token: 0x060027B3 RID: 10163 RVA: 0x000C8CDB File Offset: 0x000C7CDB
		protected void WriteRpcResult(string name, string ns)
		{
			if (!this.soap12)
			{
				return;
			}
			this.WriteElementQualifiedName("result", "http://www.w3.org/2003/05/soap-rpc", new XmlQualifiedName(name, ns), null);
		}

		// Token: 0x060027B4 RID: 10164 RVA: 0x000C8CFE File Offset: 0x000C7CFE
		protected void WriteElementQualifiedName(string localName, XmlQualifiedName value)
		{
			this.WriteElementQualifiedName(localName, null, value, null);
		}

		// Token: 0x060027B5 RID: 10165 RVA: 0x000C8D0A File Offset: 0x000C7D0A
		protected void WriteElementQualifiedName(string localName, XmlQualifiedName value, XmlQualifiedName xsiType)
		{
			this.WriteElementQualifiedName(localName, null, value, xsiType);
		}

		// Token: 0x060027B6 RID: 10166 RVA: 0x000C8D16 File Offset: 0x000C7D16
		protected void WriteElementQualifiedName(string localName, string ns, XmlQualifiedName value)
		{
			this.WriteElementQualifiedName(localName, ns, value, null);
		}

		// Token: 0x060027B7 RID: 10167 RVA: 0x000C8D24 File Offset: 0x000C7D24
		protected void WriteElementQualifiedName(string localName, string ns, XmlQualifiedName value, XmlQualifiedName xsiType)
		{
			if (value == null)
			{
				return;
			}
			if (value.Namespace == null || value.Namespace.Length == 0)
			{
				this.WriteStartElement(localName, ns, null, true);
				this.WriteAttribute("xmlns", "");
			}
			else
			{
				this.w.WriteStartElement(localName, ns);
			}
			if (xsiType != null)
			{
				this.WriteXsiType(xsiType.Name, xsiType.Namespace);
			}
			this.w.WriteString(this.FromXmlQualifiedName(value, false));
			this.w.WriteEndElement();
		}

		// Token: 0x060027B8 RID: 10168 RVA: 0x000C8DB8 File Offset: 0x000C7DB8
		protected void AddWriteCallback(Type type, string typeName, string typeNs, XmlSerializationWriteCallback callback)
		{
			XmlSerializationWriter.TypeEntry typeEntry = new XmlSerializationWriter.TypeEntry();
			typeEntry.typeName = typeName;
			typeEntry.typeNs = typeNs;
			typeEntry.type = type;
			typeEntry.callback = callback;
			this.typeEntries[type] = typeEntry;
		}

		// Token: 0x060027B9 RID: 10169 RVA: 0x000C8DF8 File Offset: 0x000C7DF8
		private void WriteArray(string name, string ns, object o, Type type)
		{
			Type arrayElementType = TypeScope.GetArrayElementType(type, null);
			StringBuilder stringBuilder = new StringBuilder();
			if (!this.soap12)
			{
				while ((arrayElementType.IsArray || typeof(IEnumerable).IsAssignableFrom(arrayElementType)) && this.GetPrimitiveTypeName(arrayElementType, false) == null)
				{
					arrayElementType = TypeScope.GetArrayElementType(arrayElementType, null);
					stringBuilder.Append("[]");
				}
			}
			string text;
			string ns2;
			if (arrayElementType == typeof(object))
			{
				text = "anyType";
				ns2 = "http://www.w3.org/2001/XMLSchema";
			}
			else
			{
				XmlSerializationWriter.TypeEntry typeEntry = this.GetTypeEntry(arrayElementType);
				if (typeEntry != null)
				{
					text = typeEntry.typeName;
					ns2 = typeEntry.typeNs;
				}
				else if (this.soap12)
				{
					XmlQualifiedName primitiveTypeName = this.GetPrimitiveTypeName(arrayElementType, false);
					if (primitiveTypeName != null)
					{
						text = primitiveTypeName.Name;
						ns2 = primitiveTypeName.Namespace;
					}
					else
					{
						for (Type baseType = arrayElementType.BaseType; baseType != null; baseType = baseType.BaseType)
						{
							typeEntry = this.GetTypeEntry(baseType);
							if (typeEntry != null)
							{
								break;
							}
						}
						if (typeEntry != null)
						{
							text = typeEntry.typeName;
							ns2 = typeEntry.typeNs;
						}
						else
						{
							text = "anyType";
							ns2 = "http://www.w3.org/2001/XMLSchema";
						}
					}
				}
				else
				{
					XmlQualifiedName primitiveTypeName2 = this.GetPrimitiveTypeName(arrayElementType);
					text = primitiveTypeName2.Name;
					ns2 = primitiveTypeName2.Namespace;
				}
			}
			if (stringBuilder.Length > 0)
			{
				text += stringBuilder.ToString();
			}
			if (this.soap12 && name != null && name.Length > 0)
			{
				this.WriteStartElement(name, ns, null, false);
			}
			else
			{
				this.WriteStartElement("Array", "http://schemas.xmlsoap.org/soap/encoding/", null, true);
			}
			this.WriteId(o, false);
			if (type.IsArray)
			{
				Array array = (Array)o;
				int length = array.Length;
				if (this.soap12)
				{
					this.w.WriteAttributeString("itemType", "http://www.w3.org/2003/05/soap-encoding", this.GetQualifiedName(text, ns2));
					this.w.WriteAttributeString("arraySize", "http://www.w3.org/2003/05/soap-encoding", length.ToString(CultureInfo.InvariantCulture));
				}
				else
				{
					this.w.WriteAttributeString("arrayType", "http://schemas.xmlsoap.org/soap/encoding/", this.GetQualifiedName(text, ns2) + "[" + length.ToString(CultureInfo.InvariantCulture) + "]");
				}
				for (int i = 0; i < length; i++)
				{
					this.WritePotentiallyReferencingElement("Item", "", array.GetValue(i), arrayElementType, false, true);
				}
			}
			else
			{
				int num = typeof(ICollection).IsAssignableFrom(type) ? ((ICollection)o).Count : -1;
				if (this.soap12)
				{
					this.w.WriteAttributeString("itemType", "http://www.w3.org/2003/05/soap-encoding", this.GetQualifiedName(text, ns2));
					if (num >= 0)
					{
						this.w.WriteAttributeString("arraySize", "http://www.w3.org/2003/05/soap-encoding", num.ToString(CultureInfo.InvariantCulture));
					}
				}
				else
				{
					string str = (num >= 0) ? ("[" + num + "]") : "[]";
					this.w.WriteAttributeString("arrayType", "http://schemas.xmlsoap.org/soap/encoding/", this.GetQualifiedName(text, ns2) + str);
				}
				IEnumerator enumerator = ((IEnumerable)o).GetEnumerator();
				if (enumerator != null)
				{
					while (enumerator.MoveNext())
					{
						object o2 = enumerator.Current;
						this.WritePotentiallyReferencingElement("Item", "", o2, arrayElementType, false, true);
					}
				}
			}
			this.w.WriteEndElement();
		}

		// Token: 0x060027BA RID: 10170 RVA: 0x000C9144 File Offset: 0x000C8144
		protected void WritePotentiallyReferencingElement(string n, string ns, object o)
		{
			this.WritePotentiallyReferencingElement(n, ns, o, null, false, false);
		}

		// Token: 0x060027BB RID: 10171 RVA: 0x000C9152 File Offset: 0x000C8152
		protected void WritePotentiallyReferencingElement(string n, string ns, object o, Type ambientType)
		{
			this.WritePotentiallyReferencingElement(n, ns, o, ambientType, false, false);
		}

		// Token: 0x060027BC RID: 10172 RVA: 0x000C9161 File Offset: 0x000C8161
		protected void WritePotentiallyReferencingElement(string n, string ns, object o, Type ambientType, bool suppressReference)
		{
			this.WritePotentiallyReferencingElement(n, ns, o, ambientType, suppressReference, false);
		}

		// Token: 0x060027BD RID: 10173 RVA: 0x000C9174 File Offset: 0x000C8174
		protected void WritePotentiallyReferencingElement(string n, string ns, object o, Type ambientType, bool suppressReference, bool isNullable)
		{
			if (o == null)
			{
				if (isNullable)
				{
					this.WriteNullTagEncoded(n, ns);
				}
				return;
			}
			Type type = o.GetType();
			if (Convert.GetTypeCode(o) == TypeCode.Object && !(o is Guid) && type != typeof(XmlQualifiedName) && !(o is XmlNode[]) && type != typeof(byte[]))
			{
				if ((suppressReference || this.soap12) && !this.IsIdDefined(o))
				{
					this.WriteReferencedElement(n, ns, o, ambientType);
					return;
				}
				if (n == null)
				{
					XmlSerializationWriter.TypeEntry typeEntry = this.GetTypeEntry(type);
					this.WriteReferencingElement(typeEntry.typeName, typeEntry.typeNs, o, isNullable);
					return;
				}
				this.WriteReferencingElement(n, ns, o, isNullable);
				return;
			}
			else
			{
				bool flag = type != ambientType && !type.IsEnum;
				XmlSerializationWriter.TypeEntry typeEntry2 = this.GetTypeEntry(type);
				if (typeEntry2 != null)
				{
					if (n == null)
					{
						this.WriteStartElement(typeEntry2.typeName, typeEntry2.typeNs, null, true);
					}
					else
					{
						this.WriteStartElement(n, ns, null, true);
					}
					if (flag)
					{
						this.WriteXsiType(typeEntry2.typeName, typeEntry2.typeNs);
					}
					typeEntry2.callback(o);
					this.w.WriteEndElement();
					return;
				}
				this.WriteTypedPrimitive(n, ns, o, flag);
				return;
			}
		}

		// Token: 0x060027BE RID: 10174 RVA: 0x000C9290 File Offset: 0x000C8290
		private void WriteReferencedElement(object o, Type ambientType)
		{
			this.WriteReferencedElement(null, null, o, ambientType);
		}

		// Token: 0x060027BF RID: 10175 RVA: 0x000C929C File Offset: 0x000C829C
		private void WriteReferencedElement(string name, string ns, object o, Type ambientType)
		{
			if (name == null)
			{
				name = string.Empty;
			}
			Type type = o.GetType();
			if (type.IsArray || typeof(IEnumerable).IsAssignableFrom(type))
			{
				this.WriteArray(name, ns, o, type);
				return;
			}
			XmlSerializationWriter.TypeEntry typeEntry = this.GetTypeEntry(type);
			if (typeEntry == null)
			{
				throw this.CreateUnknownTypeException(type);
			}
			this.WriteStartElement((name.Length == 0) ? typeEntry.typeName : name, (ns == null) ? typeEntry.typeNs : ns, null, true);
			this.WriteId(o, false);
			if (ambientType != type)
			{
				this.WriteXsiType(typeEntry.typeName, typeEntry.typeNs);
			}
			typeEntry.callback(o);
			this.w.WriteEndElement();
		}

		// Token: 0x060027C0 RID: 10176 RVA: 0x000C934D File Offset: 0x000C834D
		private XmlSerializationWriter.TypeEntry GetTypeEntry(Type t)
		{
			if (this.typeEntries == null)
			{
				this.typeEntries = new Hashtable();
				this.InitCallbacks();
			}
			return (XmlSerializationWriter.TypeEntry)this.typeEntries[t];
		}

		// Token: 0x060027C1 RID: 10177
		protected abstract void InitCallbacks();

		// Token: 0x060027C2 RID: 10178 RVA: 0x000C937C File Offset: 0x000C837C
		protected void WriteReferencedElements()
		{
			if (this.referencesToWrite == null)
			{
				return;
			}
			for (int i = 0; i < this.referencesToWrite.Count; i++)
			{
				this.WriteReferencedElement(this.referencesToWrite[i], null);
			}
		}

		// Token: 0x060027C3 RID: 10179 RVA: 0x000C93BB File Offset: 0x000C83BB
		protected void TopLevelElement()
		{
			this.objectsInUse = new Hashtable();
		}

		// Token: 0x060027C4 RID: 10180 RVA: 0x000C93C8 File Offset: 0x000C83C8
		protected void WriteNamespaceDeclarations(XmlSerializerNamespaces xmlns)
		{
			if (xmlns != null)
			{
				foreach (object obj in xmlns.Namespaces)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					string text = (string)dictionaryEntry.Key;
					string text2 = (string)dictionaryEntry.Value;
					if (this.namespaces != null)
					{
						string text3 = this.namespaces.Namespaces[text] as string;
						if (text3 != null && text3 != text2)
						{
							throw new InvalidOperationException(Res.GetString("XmlDuplicateNs", new object[]
							{
								text,
								text2
							}));
						}
					}
					string text4 = (text2 == null || text2.Length == 0) ? null : this.Writer.LookupPrefix(text2);
					if (text4 == null || text4 != text)
					{
						this.WriteAttribute("xmlns", text, null, text2);
					}
				}
			}
			this.namespaces = null;
		}

		// Token: 0x060027C5 RID: 10181 RVA: 0x000C94D8 File Offset: 0x000C84D8
		private string NextPrefix()
		{
			if (this.usedPrefixes == null)
			{
				return this.aliasBase + ++this.tempNamespacePrefix;
			}
			while (this.usedPrefixes.ContainsKey(++this.tempNamespacePrefix))
			{
			}
			return this.aliasBase + this.tempNamespacePrefix;
		}

		// Token: 0x04001655 RID: 5717
		private XmlWriter w;

		// Token: 0x04001656 RID: 5718
		private XmlSerializerNamespaces namespaces;

		// Token: 0x04001657 RID: 5719
		private int tempNamespacePrefix;

		// Token: 0x04001658 RID: 5720
		private Hashtable usedPrefixes;

		// Token: 0x04001659 RID: 5721
		private Hashtable references;

		// Token: 0x0400165A RID: 5722
		private string idBase;

		// Token: 0x0400165B RID: 5723
		private int nextId;

		// Token: 0x0400165C RID: 5724
		private Hashtable typeEntries;

		// Token: 0x0400165D RID: 5725
		private ArrayList referencesToWrite;

		// Token: 0x0400165E RID: 5726
		private Hashtable objectsInUse;

		// Token: 0x0400165F RID: 5727
		private string aliasBase = "q";

		// Token: 0x04001660 RID: 5728
		private bool soap12;

		// Token: 0x04001661 RID: 5729
		private bool escapeName = true;

		// Token: 0x02000330 RID: 816
		internal class TypeEntry
		{
			// Token: 0x04001662 RID: 5730
			internal XmlSerializationWriteCallback callback;

			// Token: 0x04001663 RID: 5731
			internal string typeNs;

			// Token: 0x04001664 RID: 5732
			internal string typeName;

			// Token: 0x04001665 RID: 5733
			internal Type type;
		}
	}
}
