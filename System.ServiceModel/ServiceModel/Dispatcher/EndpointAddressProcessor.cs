using System;
using System.Collections.Generic;
using System.Globalization;
using System.ServiceModel.Channels;
using System.Text;
using System.Xml;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000467 RID: 1127
	internal class EndpointAddressProcessor
	{
		// Token: 0x06002BD2 RID: 11218 RVA: 0x000ABD77 File Offset: 0x000A9F77
		internal EndpointAddressProcessor(int length)
		{
			this.builder = new StringBuilder();
			this.resultData = new byte[length];
		}

		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x06002BD3 RID: 11219 RVA: 0x000ABD96 File Offset: 0x000A9F96
		// (set) Token: 0x06002BD4 RID: 11220 RVA: 0x000ABD9E File Offset: 0x000A9F9E
		internal EndpointAddressProcessor Next
		{
			get
			{
				return this.next;
			}
			set
			{
				this.next = value;
			}
		}

		// Token: 0x06002BD5 RID: 11221 RVA: 0x000ABDA8 File Offset: 0x000A9FA8
		internal static string GetComparableForm(StringBuilder builder, XmlReader reader)
		{
			List<EndpointAddressProcessor.Attr> list = new List<EndpointAddressProcessor.Attr>();
			int num = -1;
			while (!reader.EOF)
			{
				XmlNodeType xmlNodeType = reader.MoveToContent();
				switch (xmlNodeType)
				{
				case XmlNodeType.Element:
					EndpointAddressProcessor.CompleteValue(builder, num);
					num = -1;
					builder.Append("<");
					EndpointAddressProcessor.AppendString(builder, reader.LocalName);
					builder.Append(":");
					EndpointAddressProcessor.AppendString(builder, reader.NamespaceURI);
					builder.Append(" ");
					list.Clear();
					if (reader.MoveToFirstAttribute())
					{
						do
						{
							if (!(reader.Prefix == "xmlns") && !(reader.Name == "xmlns") && (!(reader.LocalName == "IsReferenceParameter") || !(reader.NamespaceURI == "http://www.w3.org/2005/08/addressing")))
							{
								string text = reader.Value;
								if ((reader.LocalName == "type" && reader.NamespaceURI == EndpointAddressProcessor.XsiNs) || (reader.NamespaceURI == "http://schemas.microsoft.com/2003/10/Serialization/" && (reader.LocalName == "ItemType" || reader.LocalName == "FactoryType")))
								{
									string text2;
									string text3;
									XmlUtil.ParseQName(reader, text, out text2, out text3);
									text = string.Concat(new string[]
									{
										text2,
										"^",
										text2.Length.ToString(CultureInfo.InvariantCulture),
										":",
										text3,
										"^",
										text3.Length.ToString(CultureInfo.InvariantCulture)
									});
								}
								else if (reader.LocalName == XD.UtilityDictionary.IdAttribute.Value && reader.NamespaceURI == XD.UtilityDictionary.Namespace.Value)
								{
									goto IL_207;
								}
								list.Add(new EndpointAddressProcessor.Attr(reader.LocalName, reader.NamespaceURI, text));
							}
							IL_207:;
						}
						while (reader.MoveToNextAttribute());
					}
					reader.MoveToElement();
					if (list.Count > 0)
					{
						list.Sort();
						for (int i = 0; i < list.Count; i++)
						{
							EndpointAddressProcessor.Attr attr = list[i];
							EndpointAddressProcessor.AppendString(builder, attr.local);
							builder.Append(":");
							EndpointAddressProcessor.AppendString(builder, attr.ns);
							builder.Append("=\"");
							EndpointAddressProcessor.AppendString(builder, attr.val);
							builder.Append("\" ");
						}
					}
					if (reader.IsEmptyElement)
					{
						builder.Append("></>");
					}
					else
					{
						builder.Append(">");
					}
					break;
				case XmlNodeType.Attribute:
					break;
				case XmlNodeType.Text:
					goto IL_2FC;
				case XmlNodeType.CDATA:
					EndpointAddressProcessor.CompleteValue(builder, num);
					num = -1;
					builder.Append("<![CDATA[");
					EndpointAddressProcessor.AppendString(builder, reader.Value);
					builder.Append("]]>");
					break;
				default:
					if (xmlNodeType == XmlNodeType.SignificantWhitespace)
					{
						goto IL_2FC;
					}
					if (xmlNodeType == XmlNodeType.EndElement)
					{
						EndpointAddressProcessor.CompleteValue(builder, num);
						num = -1;
						builder.Append("</>");
					}
					break;
				}
				IL_314:
				reader.Read();
				continue;
				IL_2FC:
				if (num < 0)
				{
					num = builder.Length;
				}
				builder.Append(reader.Value);
				goto IL_314;
			}
			return builder.ToString();
		}

		// Token: 0x06002BD6 RID: 11222 RVA: 0x000AC0E4 File Offset: 0x000AA2E4
		private static void AppendString(StringBuilder builder, string s)
		{
			builder.Append(s);
			builder.Append("^");
			builder.Append(s.Length.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06002BD7 RID: 11223 RVA: 0x000AC120 File Offset: 0x000AA320
		private static void CompleteValue(StringBuilder builder, int startLength)
		{
			if (startLength < 0)
			{
				return;
			}
			int num = builder.Length - startLength;
			builder.Append("^");
			builder.Append(num.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06002BD8 RID: 11224 RVA: 0x000AC15A File Offset: 0x000AA35A
		internal void Clear(int length)
		{
			if (this.resultData.Length == length)
			{
				Array.Clear(this.resultData, 0, this.resultData.Length);
				return;
			}
			this.resultData = new byte[length];
		}

		// Token: 0x06002BD9 RID: 11225 RVA: 0x000AC188 File Offset: 0x000AA388
		internal void ProcessHeaders(Message msg, Dictionary<EndpointAddressProcessor.QName, int> qnameLookup, Dictionary<string, EndpointAddressProcessor.HeaderBit[]> headerLookup)
		{
			MessageHeaders headers = msg.Headers;
			for (int i = 0; i < headers.Count; i++)
			{
				EndpointAddressProcessor.QName key;
				key.name = headers[i].Name;
				key.ns = headers[i].Namespace;
				if ((headers.MessageVersion.Addressing != AddressingVersion.WSAddressing10 || headers[i].IsReferenceParameter) && qnameLookup.ContainsKey(key))
				{
					this.builder.Remove(0, this.builder.Length);
					XmlReader xmlReader = headers.GetReaderAtHeader(i).ReadSubtree();
					xmlReader.Read();
					string comparableForm = EndpointAddressProcessor.GetComparableForm(this.builder, xmlReader);
					EndpointAddressProcessor.HeaderBit[] bit;
					if (headerLookup.TryGetValue(comparableForm, out bit))
					{
						this.SetBit(bit);
					}
				}
			}
		}

		// Token: 0x06002BDA RID: 11226 RVA: 0x000AC258 File Offset: 0x000AA458
		internal void SetBit(EndpointAddressProcessor.HeaderBit[] bits)
		{
			if (bits.Length == 1)
			{
				byte[] array = this.resultData;
				int index = bits[0].index;
				array[index] |= bits[0].mask;
				return;
			}
			byte[] array2 = this.resultData;
			for (int i = 0; i < bits.Length; i++)
			{
				if ((array2[bits[i].index] & bits[i].mask) == 0)
				{
					byte[] array3 = array2;
					int index2 = bits[i].index;
					array3[index2] |= bits[i].mask;
					return;
				}
			}
		}

		// Token: 0x06002BDB RID: 11227 RVA: 0x000AC2EC File Offset: 0x000AA4EC
		internal bool TestExact(byte[] exact)
		{
			byte[] array = this.resultData;
			for (int i = 0; i < exact.Length; i++)
			{
				if (array[i] != exact[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002BDC RID: 11228 RVA: 0x000AC31C File Offset: 0x000AA51C
		internal bool TestMask(byte[] mask)
		{
			if (mask == null)
			{
				return true;
			}
			byte[] array = this.resultData;
			for (int i = 0; i < mask.Length; i++)
			{
				if ((array[i] & mask[i]) != mask[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0400242D RID: 9261
		internal static readonly EndpointAddressProcessor.QNameKeyComparer QNameComparer = new EndpointAddressProcessor.QNameKeyComparer();

		// Token: 0x0400242E RID: 9262
		internal static readonly string XsiNs = "http://www.w3.org/2001/XMLSchema-instance";

		// Token: 0x0400242F RID: 9263
		internal const string SerNs = "http://schemas.microsoft.com/2003/10/Serialization/";

		// Token: 0x04002430 RID: 9264
		internal const string TypeLN = "type";

		// Token: 0x04002431 RID: 9265
		internal const string ItemTypeLN = "ItemType";

		// Token: 0x04002432 RID: 9266
		internal const string FactoryTypeLN = "FactoryType";

		// Token: 0x04002433 RID: 9267
		internal EndpointAddressProcessor next;

		// Token: 0x04002434 RID: 9268
		private StringBuilder builder;

		// Token: 0x04002435 RID: 9269
		private byte[] resultData;

		// Token: 0x02000C3C RID: 3132
		internal struct QName
		{
			// Token: 0x04004441 RID: 17473
			internal string name;

			// Token: 0x04004442 RID: 17474
			internal string ns;
		}

		// Token: 0x02000C3D RID: 3133
		internal class QNameKeyComparer : IComparer<EndpointAddressProcessor.QName>, IEqualityComparer<EndpointAddressProcessor.QName>
		{
			// Token: 0x06007752 RID: 30546 RVA: 0x001BDC76 File Offset: 0x001BBE76
			internal QNameKeyComparer()
			{
			}

			// Token: 0x06007753 RID: 30547 RVA: 0x001BDC80 File Offset: 0x001BBE80
			public int Compare(EndpointAddressProcessor.QName x, EndpointAddressProcessor.QName y)
			{
				int num = string.CompareOrdinal(x.name, y.name);
				if (num != 0)
				{
					return num;
				}
				return string.CompareOrdinal(x.ns, y.ns);
			}

			// Token: 0x06007754 RID: 30548 RVA: 0x001BDCB8 File Offset: 0x001BBEB8
			public bool Equals(EndpointAddressProcessor.QName x, EndpointAddressProcessor.QName y)
			{
				int num = string.CompareOrdinal(x.name, y.name);
				return num == 0 && string.CompareOrdinal(x.ns, y.ns) == 0;
			}

			// Token: 0x06007755 RID: 30549 RVA: 0x001BDCF0 File Offset: 0x001BBEF0
			public int GetHashCode(EndpointAddressProcessor.QName obj)
			{
				return obj.name.GetHashCode() ^ obj.ns.GetHashCode();
			}
		}

		// Token: 0x02000C3E RID: 3134
		internal struct HeaderBit
		{
			// Token: 0x06007756 RID: 30550 RVA: 0x001BDD09 File Offset: 0x001BBF09
			internal HeaderBit(int bitNum)
			{
				this.index = bitNum / 8;
				this.mask = (byte)(1 << bitNum % 8);
			}

			// Token: 0x06007757 RID: 30551 RVA: 0x001BDD24 File Offset: 0x001BBF24
			internal void AddToMask(ref byte[] mask)
			{
				if (mask == null)
				{
					mask = new byte[this.index + 1];
				}
				else if (mask.Length <= this.index)
				{
					Array.Resize<byte>(ref mask, this.index + 1);
				}
				byte[] array = mask;
				int num = this.index;
				array[num] |= this.mask;
			}

			// Token: 0x04004443 RID: 17475
			internal int index;

			// Token: 0x04004444 RID: 17476
			internal byte mask;
		}

		// Token: 0x02000C3F RID: 3135
		private class Attr : IComparable<EndpointAddressProcessor.Attr>
		{
			// Token: 0x06007758 RID: 30552 RVA: 0x001BDD78 File Offset: 0x001BBF78
			internal Attr(string l, string ns, string v)
			{
				this.local = l;
				this.ns = ns;
				this.val = v;
				this.key = ns + ":" + l;
			}

			// Token: 0x06007759 RID: 30553 RVA: 0x001BDDA7 File Offset: 0x001BBFA7
			public int CompareTo(EndpointAddressProcessor.Attr a)
			{
				return string.Compare(this.key, a.key, StringComparison.Ordinal);
			}

			// Token: 0x04004445 RID: 17477
			internal string local;

			// Token: 0x04004446 RID: 17478
			internal string ns;

			// Token: 0x04004447 RID: 17479
			internal string val;

			// Token: 0x04004448 RID: 17480
			private string key;
		}
	}
}
