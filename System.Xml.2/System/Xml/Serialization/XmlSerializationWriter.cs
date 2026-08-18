using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x020001B0 RID: 432
	public abstract class XmlSerializationWriter : XmlSerializationGeneratedCode
	{
		// Token: 0x06001D60 RID: 7520 RVA: 0x000990DE File Offset: 0x000972DE
		internal void Init(XmlWriter w, XmlSerializerNamespaces namespaces, string encodingStyle, string idBase, TempAssembly tempAssembly)
		{
			this.w = w;
			this.namespaces = namespaces;
			this.soap12 = (encodingStyle == "http://www.w3.org/2003/05/soap-encoding");
			this.idBase = idBase;
			base.Init(tempAssembly);
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06001D61 RID: 7521 RVA: 0x0009910F File Offset: 0x0009730F
		// (set) Token: 0x06001D62 RID: 7522 RVA: 0x00099117 File Offset: 0x00097317
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

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06001D63 RID: 7523 RVA: 0x00099120 File Offset: 0x00097320
		// (set) Token: 0x06001D64 RID: 7524 RVA: 0x00099128 File Offset: 0x00097328
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

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06001D65 RID: 7525 RVA: 0x00099131 File Offset: 0x00097331
		// (set) Token: 0x06001D66 RID: 7526 RVA: 0x00099148 File Offset: 0x00097348
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

		// Token: 0x06001D67 RID: 7527 RVA: 0x00099182 File Offset: 0x00097382
		protected static byte[] FromByteArrayBase64(byte[] value)
		{
			return value;
		}

		// Token: 0x06001D68 RID: 7528 RVA: 0x00099185 File Offset: 0x00097385
		protected static Assembly ResolveDynamicAssembly(string assemblyFullName)
		{
			return DynamicAssemblies.Get(assemblyFullName);
		}

		// Token: 0x06001D69 RID: 7529 RVA: 0x0009918D File Offset: 0x0009738D
		protected static string FromByteArrayHex(byte[] value)
		{
			return XmlCustomFormatter.FromByteArrayHex(value);
		}

		// Token: 0x06001D6A RID: 7530 RVA: 0x00099195 File Offset: 0x00097395
		protected static string FromDateTime(DateTime value)
		{
			return XmlCustomFormatter.FromDateTime(value);
		}

		// Token: 0x06001D6B RID: 7531 RVA: 0x0009919D File Offset: 0x0009739D
		protected static string FromDate(DateTime value)
		{
			return XmlCustomFormatter.FromDate(value);
		}

		// Token: 0x06001D6C RID: 7532 RVA: 0x000991A5 File Offset: 0x000973A5
		protected static string FromTime(DateTime value)
		{
			return XmlCustomFormatter.FromTime(value);
		}

		// Token: 0x06001D6D RID: 7533 RVA: 0x000991AD File Offset: 0x000973AD
		protected static string FromChar(char value)
		{
			return XmlCustomFormatter.FromChar(value);
		}

		// Token: 0x06001D6E RID: 7534 RVA: 0x000991B5 File Offset: 0x000973B5
		protected static string FromEnum(long value, string[] values, long[] ids)
		{
			return XmlCustomFormatter.FromEnum(value, values, ids, null);
		}

		// Token: 0x06001D6F RID: 7535 RVA: 0x000991C0 File Offset: 0x000973C0
		protected static string FromEnum(long value, string[] values, long[] ids, string typeName)
		{
			return XmlCustomFormatter.FromEnum(value, values, ids, typeName);
		}

		// Token: 0x06001D70 RID: 7536 RVA: 0x000991CB File Offset: 0x000973CB
		protected static string FromXmlName(string name)
		{
			return XmlCustomFormatter.FromXmlName(name);
		}

		// Token: 0x06001D71 RID: 7537 RVA: 0x000991D3 File Offset: 0x000973D3
		protected static string FromXmlNCName(string ncName)
		{
			return XmlCustomFormatter.FromXmlNCName(ncName);
		}

		// Token: 0x06001D72 RID: 7538 RVA: 0x000991DB File Offset: 0x000973DB
		protected static string FromXmlNmToken(string nmToken)
		{
			return XmlCustomFormatter.FromXmlNmToken(nmToken);
		}

		// Token: 0x06001D73 RID: 7539 RVA: 0x000991E3 File Offset: 0x000973E3
		protected static string FromXmlNmTokens(string nmTokens)
		{
			return XmlCustomFormatter.FromXmlNmTokens(nmTokens);
		}

		// Token: 0x06001D74 RID: 7540 RVA: 0x000991EB File Offset: 0x000973EB
		protected void WriteXsiType(string name, string ns)
		{
			this.WriteAttribute("type", "http://www.w3.org/2001/XMLSchema-instance", this.GetQualifiedName(name, ns));
		}

		// Token: 0x06001D75 RID: 7541 RVA: 0x00099205 File Offset: 0x00097405
		private XmlQualifiedName GetPrimitiveTypeName(Type type)
		{
			return this.GetPrimitiveTypeName(type, true);
		}

		// Token: 0x06001D76 RID: 7542 RVA: 0x00099210 File Offset: 0x00097410
		private XmlQualifiedName GetPrimitiveTypeName(Type type, bool throwIfUnknown)
		{
			XmlQualifiedName primitiveTypeNameInternal = XmlSerializationWriter.GetPrimitiveTypeNameInternal(type);
			if (throwIfUnknown && primitiveTypeNameInternal == null)
			{
				throw this.CreateUnknownTypeException(type);
			}
			return primitiveTypeNameInternal;
		}

		// Token: 0x06001D77 RID: 7543 RVA: 0x0009923C File Offset: 0x0009743C
		internal static XmlQualifiedName GetPrimitiveTypeNameInternal(Type type)
		{
			string ns = "http://www.w3.org/2001/XMLSchema";
			string name;
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Boolean:
				name = "boolean";
				goto IL_196;
			case TypeCode.Char:
				name = "char";
				ns = "http://microsoft.com/wsdl/types/";
				goto IL_196;
			case TypeCode.SByte:
				name = "byte";
				goto IL_196;
			case TypeCode.Byte:
				name = "unsignedByte";
				goto IL_196;
			case TypeCode.Int16:
				name = "short";
				goto IL_196;
			case TypeCode.UInt16:
				name = "unsignedShort";
				goto IL_196;
			case TypeCode.Int32:
				name = "int";
				goto IL_196;
			case TypeCode.UInt32:
				name = "unsignedInt";
				goto IL_196;
			case TypeCode.Int64:
				name = "long";
				goto IL_196;
			case TypeCode.UInt64:
				name = "unsignedLong";
				goto IL_196;
			case TypeCode.Single:
				name = "float";
				goto IL_196;
			case TypeCode.Double:
				name = "double";
				goto IL_196;
			case TypeCode.Decimal:
				name = "decimal";
				goto IL_196;
			case TypeCode.DateTime:
				name = "dateTime";
				goto IL_196;
			case TypeCode.String:
				name = "string";
				goto IL_196;
			}
			if (type == typeof(XmlQualifiedName))
			{
				name = "QName";
			}
			else if (type == typeof(byte[]))
			{
				name = "base64Binary";
			}
			else if (type == typeof(TimeSpan) && LocalAppContextSwitches.EnableTimeSpanSerialization)
			{
				name = "TimeSpan";
			}
			else if (type == typeof(Guid))
			{
				name = "guid";
				ns = "http://microsoft.com/wsdl/types/";
			}
			else
			{
				if (!(type == typeof(XmlNode[])))
				{
					return null;
				}
				name = "anyType";
			}
			IL_196:
			return new XmlQualifiedName(name, ns);
		}

		// Token: 0x06001D78 RID: 7544 RVA: 0x000993E8 File Offset: 0x000975E8
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
				goto IL_322;
			case TypeCode.Char:
				text = XmlSerializationWriter.FromChar((char)o);
				text2 = "char";
				ns2 = "http://microsoft.com/wsdl/types/";
				goto IL_322;
			case TypeCode.SByte:
				text = XmlConvert.ToString((sbyte)o);
				text2 = "byte";
				goto IL_322;
			case TypeCode.Byte:
				text = XmlConvert.ToString((byte)o);
				text2 = "unsignedByte";
				goto IL_322;
			case TypeCode.Int16:
				text = XmlConvert.ToString((short)o);
				text2 = "short";
				goto IL_322;
			case TypeCode.UInt16:
				text = XmlConvert.ToString((ushort)o);
				text2 = "unsignedShort";
				goto IL_322;
			case TypeCode.Int32:
				text = XmlConvert.ToString((int)o);
				text2 = "int";
				goto IL_322;
			case TypeCode.UInt32:
				text = XmlConvert.ToString((uint)o);
				text2 = "unsignedInt";
				goto IL_322;
			case TypeCode.Int64:
				text = XmlConvert.ToString((long)o);
				text2 = "long";
				goto IL_322;
			case TypeCode.UInt64:
				text = XmlConvert.ToString((ulong)o);
				text2 = "unsignedLong";
				goto IL_322;
			case TypeCode.Single:
				text = XmlConvert.ToString((float)o);
				text2 = "float";
				goto IL_322;
			case TypeCode.Double:
				text = XmlConvert.ToString((double)o);
				text2 = "double";
				goto IL_322;
			case TypeCode.Decimal:
				text = XmlConvert.ToString((decimal)o);
				text2 = "decimal";
				goto IL_322;
			case TypeCode.DateTime:
				text = XmlSerializationWriter.FromDateTime((DateTime)o);
				text2 = "dateTime";
				goto IL_322;
			case TypeCode.String:
				text = (string)o;
				text2 = "string";
				flag = false;
				goto IL_322;
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
			else if (type == typeof(TimeSpan) && LocalAppContextSwitches.EnableTimeSpanSerialization)
			{
				text = XmlConvert.ToString((TimeSpan)o);
				text2 = "TimeSpan";
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
			IL_322:
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

		// Token: 0x06001D79 RID: 7545 RVA: 0x000997B0 File Offset: 0x000979B0
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

		// Token: 0x06001D7A RID: 7546 RVA: 0x0009981C File Offset: 0x00097A1C
		protected string FromXmlQualifiedName(XmlQualifiedName xmlQualifiedName)
		{
			return this.FromXmlQualifiedName(xmlQualifiedName, true);
		}

		// Token: 0x06001D7B RID: 7547 RVA: 0x00099826 File Offset: 0x00097A26
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

		// Token: 0x06001D7C RID: 7548 RVA: 0x00099866 File Offset: 0x00097A66
		protected void WriteStartElement(string name)
		{
			this.WriteStartElement(name, null, null, false, null);
		}

		// Token: 0x06001D7D RID: 7549 RVA: 0x00099873 File Offset: 0x00097A73
		protected void WriteStartElement(string name, string ns)
		{
			this.WriteStartElement(name, ns, null, false, null);
		}

		// Token: 0x06001D7E RID: 7550 RVA: 0x00099880 File Offset: 0x00097A80
		protected void WriteStartElement(string name, string ns, bool writePrefixed)
		{
			this.WriteStartElement(name, ns, null, writePrefixed, null);
		}

		// Token: 0x06001D7F RID: 7551 RVA: 0x0009988D File Offset: 0x00097A8D
		protected void WriteStartElement(string name, string ns, object o)
		{
			this.WriteStartElement(name, ns, o, false, null);
		}

		// Token: 0x06001D80 RID: 7552 RVA: 0x0009989A File Offset: 0x00097A9A
		protected void WriteStartElement(string name, string ns, object o, bool writePrefixed)
		{
			this.WriteStartElement(name, ns, o, writePrefixed, null);
		}

		// Token: 0x06001D81 RID: 7553 RVA: 0x000998A8 File Offset: 0x00097AA8
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

		// Token: 0x06001D82 RID: 7554 RVA: 0x00099B44 File Offset: 0x00097D44
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

		// Token: 0x06001D83 RID: 7555 RVA: 0x00099C7C File Offset: 0x00097E7C
		protected void WriteNullTagEncoded(string name)
		{
			this.WriteNullTagEncoded(name, null);
		}

		// Token: 0x06001D84 RID: 7556 RVA: 0x00099C86 File Offset: 0x00097E86
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

		// Token: 0x06001D85 RID: 7557 RVA: 0x00099CC3 File Offset: 0x00097EC3
		protected void WriteNullTagLiteral(string name)
		{
			this.WriteNullTagLiteral(name, null);
		}

		// Token: 0x06001D86 RID: 7558 RVA: 0x00099CCD File Offset: 0x00097ECD
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

		// Token: 0x06001D87 RID: 7559 RVA: 0x00099D0A File Offset: 0x00097F0A
		protected void WriteEmptyTag(string name)
		{
			this.WriteEmptyTag(name, null);
		}

		// Token: 0x06001D88 RID: 7560 RVA: 0x00099D14 File Offset: 0x00097F14
		protected void WriteEmptyTag(string name, string ns)
		{
			if (name == null || name.Length == 0)
			{
				return;
			}
			this.WriteStartElement(name, ns, null, false);
			this.w.WriteEndElement();
		}

		// Token: 0x06001D89 RID: 7561 RVA: 0x00099D37 File Offset: 0x00097F37
		protected void WriteEndElement()
		{
			this.w.WriteEndElement();
		}

		// Token: 0x06001D8A RID: 7562 RVA: 0x00099D44 File Offset: 0x00097F44
		protected void WriteEndElement(object o)
		{
			this.w.WriteEndElement();
			if (o != null && this.objectsInUse != null)
			{
				this.objectsInUse.Remove(o);
			}
		}

		// Token: 0x06001D8B RID: 7563 RVA: 0x00099D68 File Offset: 0x00097F68
		protected void WriteSerializable(IXmlSerializable serializable, string name, string ns, bool isNullable)
		{
			this.WriteSerializable(serializable, name, ns, isNullable, true);
		}

		// Token: 0x06001D8C RID: 7564 RVA: 0x00099D76 File Offset: 0x00097F76
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

		// Token: 0x06001D8D RID: 7565 RVA: 0x00099DB4 File Offset: 0x00097FB4
		protected void WriteNullableStringEncoded(string name, string ns, string value, XmlQualifiedName xsiType)
		{
			if (value == null)
			{
				this.WriteNullTagEncoded(name, ns);
				return;
			}
			this.WriteElementString(name, ns, value, xsiType);
		}

		// Token: 0x06001D8E RID: 7566 RVA: 0x00099DCD File Offset: 0x00097FCD
		protected void WriteNullableStringLiteral(string name, string ns, string value)
		{
			if (value == null)
			{
				this.WriteNullTagLiteral(name, ns);
				return;
			}
			this.WriteElementString(name, ns, value, null);
		}

		// Token: 0x06001D8F RID: 7567 RVA: 0x00099DE5 File Offset: 0x00097FE5
		protected void WriteNullableStringEncodedRaw(string name, string ns, string value, XmlQualifiedName xsiType)
		{
			if (value == null)
			{
				this.WriteNullTagEncoded(name, ns);
				return;
			}
			this.WriteElementStringRaw(name, ns, value, xsiType);
		}

		// Token: 0x06001D90 RID: 7568 RVA: 0x00099DFE File Offset: 0x00097FFE
		protected void WriteNullableStringEncodedRaw(string name, string ns, byte[] value, XmlQualifiedName xsiType)
		{
			if (value == null)
			{
				this.WriteNullTagEncoded(name, ns);
				return;
			}
			this.WriteElementStringRaw(name, ns, value, xsiType);
		}

		// Token: 0x06001D91 RID: 7569 RVA: 0x00099E17 File Offset: 0x00098017
		protected void WriteNullableStringLiteralRaw(string name, string ns, string value)
		{
			if (value == null)
			{
				this.WriteNullTagLiteral(name, ns);
				return;
			}
			this.WriteElementStringRaw(name, ns, value, null);
		}

		// Token: 0x06001D92 RID: 7570 RVA: 0x00099E2F File Offset: 0x0009802F
		protected void WriteNullableStringLiteralRaw(string name, string ns, byte[] value)
		{
			if (value == null)
			{
				this.WriteNullTagLiteral(name, ns);
				return;
			}
			this.WriteElementStringRaw(name, ns, value, null);
		}

		// Token: 0x06001D93 RID: 7571 RVA: 0x00099E47 File Offset: 0x00098047
		protected void WriteNullableQualifiedNameEncoded(string name, string ns, XmlQualifiedName value, XmlQualifiedName xsiType)
		{
			if (value == null)
			{
				this.WriteNullTagEncoded(name, ns);
				return;
			}
			this.WriteElementQualifiedName(name, ns, value, xsiType);
		}

		// Token: 0x06001D94 RID: 7572 RVA: 0x00099E66 File Offset: 0x00098066
		protected void WriteNullableQualifiedNameLiteral(string name, string ns, XmlQualifiedName value)
		{
			if (value == null)
			{
				this.WriteNullTagLiteral(name, ns);
				return;
			}
			this.WriteElementQualifiedName(name, ns, value, null);
		}

		// Token: 0x06001D95 RID: 7573 RVA: 0x00099E84 File Offset: 0x00098084
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

		// Token: 0x06001D96 RID: 7574 RVA: 0x00099EA3 File Offset: 0x000980A3
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

		// Token: 0x06001D97 RID: 7575 RVA: 0x00099EC4 File Offset: 0x000980C4
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

		// Token: 0x06001D98 RID: 7576 RVA: 0x00099FA9 File Offset: 0x000981A9
		protected Exception CreateUnknownTypeException(object o)
		{
			return this.CreateUnknownTypeException(o.GetType());
		}

		// Token: 0x06001D99 RID: 7577 RVA: 0x00099FB8 File Offset: 0x000981B8
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

		// Token: 0x06001D9A RID: 7578 RVA: 0x0009A047 File Offset: 0x00098247
		protected Exception CreateMismatchChoiceException(string value, string elementName, string enumValue)
		{
			return new InvalidOperationException(Res.GetString("XmlChoiceMismatchChoiceException", new object[]
			{
				elementName,
				value,
				enumValue
			}));
		}

		// Token: 0x06001D9B RID: 7579 RVA: 0x0009A06A File Offset: 0x0009826A
		protected Exception CreateUnknownAnyElementException(string name, string ns)
		{
			return new InvalidOperationException(Res.GetString("XmlUnknownAnyElement", new object[]
			{
				name,
				ns
			}));
		}

		// Token: 0x06001D9C RID: 7580 RVA: 0x0009A089 File Offset: 0x00098289
		protected Exception CreateInvalidChoiceIdentifierValueException(string type, string identifier)
		{
			return new InvalidOperationException(Res.GetString("XmlInvalidChoiceIdentifierValue", new object[]
			{
				type,
				identifier
			}));
		}

		// Token: 0x06001D9D RID: 7581 RVA: 0x0009A0A8 File Offset: 0x000982A8
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

		// Token: 0x06001D9E RID: 7582 RVA: 0x0009A0D0 File Offset: 0x000982D0
		protected Exception CreateInvalidEnumValueException(object value, string typeName)
		{
			return new InvalidOperationException(Res.GetString("XmlUnknownConstant", new object[]
			{
				value,
				typeName
			}));
		}

		// Token: 0x06001D9F RID: 7583 RVA: 0x0009A0EF File Offset: 0x000982EF
		protected Exception CreateInvalidAnyTypeException(object o)
		{
			return this.CreateInvalidAnyTypeException(o.GetType());
		}

		// Token: 0x06001DA0 RID: 7584 RVA: 0x0009A0FD File Offset: 0x000982FD
		protected Exception CreateInvalidAnyTypeException(Type type)
		{
			return new InvalidOperationException(Res.GetString("XmlIllegalAnyElement", new object[]
			{
				type.FullName
			}));
		}

		// Token: 0x06001DA1 RID: 7585 RVA: 0x0009A11D File Offset: 0x0009831D
		protected void WriteReferencingElement(string n, string ns, object o)
		{
			this.WriteReferencingElement(n, ns, o, false);
		}

		// Token: 0x06001DA2 RID: 7586 RVA: 0x0009A12C File Offset: 0x0009832C
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

		// Token: 0x06001DA3 RID: 7587 RVA: 0x0009A1A7 File Offset: 0x000983A7
		private bool IsIdDefined(object o)
		{
			return this.references != null && this.references.Contains(o);
		}

		// Token: 0x06001DA4 RID: 7588 RVA: 0x0009A1C0 File Offset: 0x000983C0
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
				int num = this.nextId + 1;
				this.nextId = num;
				text = str + str2 + num.ToString(CultureInfo.InvariantCulture);
				this.references.Add(o, text);
				if (addToReferencesList)
				{
					this.referencesToWrite.Add(o);
				}
			}
			return text;
		}

		// Token: 0x06001DA5 RID: 7589 RVA: 0x0009A24B File Offset: 0x0009844B
		protected void WriteId(object o)
		{
			this.WriteId(o, true);
		}

		// Token: 0x06001DA6 RID: 7590 RVA: 0x0009A255 File Offset: 0x00098455
		private void WriteId(object o, bool addToReferencesList)
		{
			if (this.soap12)
			{
				this.w.WriteAttributeString("id", "http://www.w3.org/2003/05/soap-encoding", this.GetId(o, addToReferencesList));
				return;
			}
			this.w.WriteAttributeString("id", this.GetId(o, addToReferencesList));
		}

		// Token: 0x06001DA7 RID: 7591 RVA: 0x0009A295 File Offset: 0x00098495
		protected void WriteXmlAttribute(XmlNode node)
		{
			this.WriteXmlAttribute(node, null);
		}

		// Token: 0x06001DA8 RID: 7592 RVA: 0x0009A2A0 File Offset: 0x000984A0
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

		// Token: 0x06001DA9 RID: 7593 RVA: 0x0009A34C File Offset: 0x0009854C
		protected void WriteAttribute(string localName, string ns, string value)
		{
			if (value == null)
			{
				return;
			}
			if (!(localName == "xmlns") && !localName.StartsWith("xmlns:", StringComparison.Ordinal))
			{
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

		// Token: 0x06001DAA RID: 7594 RVA: 0x0009A3F4 File Offset: 0x000985F4
		protected void WriteAttribute(string localName, string ns, byte[] value)
		{
			if (value == null)
			{
				return;
			}
			if (!(localName == "xmlns") && !localName.StartsWith("xmlns:", StringComparison.Ordinal))
			{
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

		// Token: 0x06001DAB RID: 7595 RVA: 0x0009A4C9 File Offset: 0x000986C9
		protected void WriteAttribute(string localName, string value)
		{
			if (value == null)
			{
				return;
			}
			this.w.WriteAttributeString(localName, null, value);
		}

		// Token: 0x06001DAC RID: 7596 RVA: 0x0009A4DD File Offset: 0x000986DD
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

		// Token: 0x06001DAD RID: 7597 RVA: 0x0009A50C File Offset: 0x0009870C
		protected void WriteAttribute(string prefix, string localName, string ns, string value)
		{
			if (value == null)
			{
				return;
			}
			this.w.WriteAttributeString(prefix, localName, null, value);
		}

		// Token: 0x06001DAE RID: 7598 RVA: 0x0009A523 File Offset: 0x00098723
		protected void WriteValue(string value)
		{
			if (value == null)
			{
				return;
			}
			this.w.WriteString(value);
		}

		// Token: 0x06001DAF RID: 7599 RVA: 0x0009A535 File Offset: 0x00098735
		protected void WriteValue(byte[] value)
		{
			if (value == null)
			{
				return;
			}
			XmlCustomFormatter.WriteArrayBase64(this.w, value, 0, value.Length);
		}

		// Token: 0x06001DB0 RID: 7600 RVA: 0x0009A54B File Offset: 0x0009874B
		protected void WriteStartDocument()
		{
			if (this.w.WriteState == WriteState.Start)
			{
				this.w.WriteStartDocument();
			}
		}

		// Token: 0x06001DB1 RID: 7601 RVA: 0x0009A565 File Offset: 0x00098765
		protected void WriteElementString(string localName, string value)
		{
			this.WriteElementString(localName, null, value, null);
		}

		// Token: 0x06001DB2 RID: 7602 RVA: 0x0009A571 File Offset: 0x00098771
		protected void WriteElementString(string localName, string ns, string value)
		{
			this.WriteElementString(localName, ns, value, null);
		}

		// Token: 0x06001DB3 RID: 7603 RVA: 0x0009A57D File Offset: 0x0009877D
		protected void WriteElementString(string localName, string value, XmlQualifiedName xsiType)
		{
			this.WriteElementString(localName, null, value, xsiType);
		}

		// Token: 0x06001DB4 RID: 7604 RVA: 0x0009A58C File Offset: 0x0009878C
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

		// Token: 0x06001DB5 RID: 7605 RVA: 0x0009A5EE File Offset: 0x000987EE
		protected void WriteElementStringRaw(string localName, string value)
		{
			this.WriteElementStringRaw(localName, null, value, null);
		}

		// Token: 0x06001DB6 RID: 7606 RVA: 0x0009A5FA File Offset: 0x000987FA
		protected void WriteElementStringRaw(string localName, byte[] value)
		{
			this.WriteElementStringRaw(localName, null, value, null);
		}

		// Token: 0x06001DB7 RID: 7607 RVA: 0x0009A606 File Offset: 0x00098806
		protected void WriteElementStringRaw(string localName, string ns, string value)
		{
			this.WriteElementStringRaw(localName, ns, value, null);
		}

		// Token: 0x06001DB8 RID: 7608 RVA: 0x0009A612 File Offset: 0x00098812
		protected void WriteElementStringRaw(string localName, string ns, byte[] value)
		{
			this.WriteElementStringRaw(localName, ns, value, null);
		}

		// Token: 0x06001DB9 RID: 7609 RVA: 0x0009A61E File Offset: 0x0009881E
		protected void WriteElementStringRaw(string localName, string value, XmlQualifiedName xsiType)
		{
			this.WriteElementStringRaw(localName, null, value, xsiType);
		}

		// Token: 0x06001DBA RID: 7610 RVA: 0x0009A62A File Offset: 0x0009882A
		protected void WriteElementStringRaw(string localName, byte[] value, XmlQualifiedName xsiType)
		{
			this.WriteElementStringRaw(localName, null, value, xsiType);
		}

		// Token: 0x06001DBB RID: 7611 RVA: 0x0009A638 File Offset: 0x00098838
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

		// Token: 0x06001DBC RID: 7612 RVA: 0x0009A68C File Offset: 0x0009888C
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

		// Token: 0x06001DBD RID: 7613 RVA: 0x0009A6E3 File Offset: 0x000988E3
		protected void WriteRpcResult(string name, string ns)
		{
			if (!this.soap12)
			{
				return;
			}
			this.WriteElementQualifiedName("result", "http://www.w3.org/2003/05/soap-rpc", new XmlQualifiedName(name, ns), null);
		}

		// Token: 0x06001DBE RID: 7614 RVA: 0x0009A706 File Offset: 0x00098906
		protected void WriteElementQualifiedName(string localName, XmlQualifiedName value)
		{
			this.WriteElementQualifiedName(localName, null, value, null);
		}

		// Token: 0x06001DBF RID: 7615 RVA: 0x0009A712 File Offset: 0x00098912
		protected void WriteElementQualifiedName(string localName, XmlQualifiedName value, XmlQualifiedName xsiType)
		{
			this.WriteElementQualifiedName(localName, null, value, xsiType);
		}

		// Token: 0x06001DC0 RID: 7616 RVA: 0x0009A71E File Offset: 0x0009891E
		protected void WriteElementQualifiedName(string localName, string ns, XmlQualifiedName value)
		{
			this.WriteElementQualifiedName(localName, ns, value, null);
		}

		// Token: 0x06001DC1 RID: 7617 RVA: 0x0009A72C File Offset: 0x0009892C
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

		// Token: 0x06001DC2 RID: 7618 RVA: 0x0009A7C0 File Offset: 0x000989C0
		protected void AddWriteCallback(Type type, string typeName, string typeNs, XmlSerializationWriteCallback callback)
		{
			XmlSerializationWriter.TypeEntry typeEntry = new XmlSerializationWriter.TypeEntry();
			typeEntry.typeName = typeName;
			typeEntry.typeNs = typeNs;
			typeEntry.type = type;
			typeEntry.callback = callback;
			this.typeEntries[type] = typeEntry;
		}

		// Token: 0x06001DC3 RID: 7619 RVA: 0x0009A800 File Offset: 0x00098A00
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
						Type baseType = arrayElementType.BaseType;
						while (baseType != null)
						{
							typeEntry = this.GetTypeEntry(baseType);
							if (typeEntry != null)
							{
								break;
							}
							baseType = baseType.BaseType;
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
					string str = (num >= 0) ? ("[" + num.ToString() + "]") : "[]";
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

		// Token: 0x06001DC4 RID: 7620 RVA: 0x0009AB57 File Offset: 0x00098D57
		protected void WritePotentiallyReferencingElement(string n, string ns, object o)
		{
			this.WritePotentiallyReferencingElement(n, ns, o, null, false, false);
		}

		// Token: 0x06001DC5 RID: 7621 RVA: 0x0009AB65 File Offset: 0x00098D65
		protected void WritePotentiallyReferencingElement(string n, string ns, object o, Type ambientType)
		{
			this.WritePotentiallyReferencingElement(n, ns, o, ambientType, false, false);
		}

		// Token: 0x06001DC6 RID: 7622 RVA: 0x0009AB74 File Offset: 0x00098D74
		protected void WritePotentiallyReferencingElement(string n, string ns, object o, Type ambientType, bool suppressReference)
		{
			this.WritePotentiallyReferencingElement(n, ns, o, ambientType, suppressReference, false);
		}

		// Token: 0x06001DC7 RID: 7623 RVA: 0x0009AB84 File Offset: 0x00098D84
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

		// Token: 0x06001DC8 RID: 7624 RVA: 0x0009ACB2 File Offset: 0x00098EB2
		private void WriteReferencedElement(object o, Type ambientType)
		{
			this.WriteReferencedElement(null, null, o, ambientType);
		}

		// Token: 0x06001DC9 RID: 7625 RVA: 0x0009ACC0 File Offset: 0x00098EC0
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

		// Token: 0x06001DCA RID: 7626 RVA: 0x0009AD76 File Offset: 0x00098F76
		private XmlSerializationWriter.TypeEntry GetTypeEntry(Type t)
		{
			if (this.typeEntries == null)
			{
				this.typeEntries = new Hashtable();
				this.InitCallbacks();
			}
			return (XmlSerializationWriter.TypeEntry)this.typeEntries[t];
		}

		// Token: 0x06001DCB RID: 7627
		protected abstract void InitCallbacks();

		// Token: 0x06001DCC RID: 7628 RVA: 0x0009ADA4 File Offset: 0x00098FA4
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

		// Token: 0x06001DCD RID: 7629 RVA: 0x0009ADE3 File Offset: 0x00098FE3
		protected void TopLevelElement()
		{
			this.objectsInUse = new Hashtable();
		}

		// Token: 0x06001DCE RID: 7630 RVA: 0x0009ADF0 File Offset: 0x00098FF0
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

		// Token: 0x06001DCF RID: 7631 RVA: 0x0009AEF8 File Offset: 0x000990F8
		private string NextPrefix()
		{
			int num;
			if (this.usedPrefixes == null)
			{
				string str = this.aliasBase;
				num = this.tempNamespacePrefix + 1;
				this.tempNamespacePrefix = num;
				return str + num.ToString();
			}
			Hashtable hashtable;
			do
			{
				hashtable = this.usedPrefixes;
				num = this.tempNamespacePrefix + 1;
				this.tempNamespacePrefix = num;
			}
			while (hashtable.ContainsKey(num));
			return this.aliasBase + this.tempNamespacePrefix.ToString();
		}

		// Token: 0x04000CBF RID: 3263
		private XmlWriter w;

		// Token: 0x04000CC0 RID: 3264
		private XmlSerializerNamespaces namespaces;

		// Token: 0x04000CC1 RID: 3265
		private int tempNamespacePrefix;

		// Token: 0x04000CC2 RID: 3266
		private Hashtable usedPrefixes;

		// Token: 0x04000CC3 RID: 3267
		private Hashtable references;

		// Token: 0x04000CC4 RID: 3268
		private string idBase;

		// Token: 0x04000CC5 RID: 3269
		private int nextId;

		// Token: 0x04000CC6 RID: 3270
		private Hashtable typeEntries;

		// Token: 0x04000CC7 RID: 3271
		private ArrayList referencesToWrite;

		// Token: 0x04000CC8 RID: 3272
		private Hashtable objectsInUse;

		// Token: 0x04000CC9 RID: 3273
		private string aliasBase = "q";

		// Token: 0x04000CCA RID: 3274
		private bool soap12;

		// Token: 0x04000CCB RID: 3275
		private bool escapeName = true;

		// Token: 0x02000486 RID: 1158
		internal class TypeEntry
		{
			// Token: 0x04001E02 RID: 7682
			internal XmlSerializationWriteCallback callback;

			// Token: 0x04001E03 RID: 7683
			internal string typeNs;

			// Token: 0x04001E04 RID: 7684
			internal string typeName;

			// Token: 0x04001E05 RID: 7685
			internal Type type;
		}
	}
}
