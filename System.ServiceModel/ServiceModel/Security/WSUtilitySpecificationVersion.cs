using System;
using System.IdentityModel;
using System.IO;
using System.Text;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x02000288 RID: 648
	internal abstract class WSUtilitySpecificationVersion
	{
		// Token: 0x060012E3 RID: 4835 RVA: 0x00044217 File Offset: 0x00042417
		internal WSUtilitySpecificationVersion(XmlDictionaryString namespaceUri)
		{
			this.namespaceUri = namespaceUri;
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x060012E4 RID: 4836 RVA: 0x00044226 File Offset: 0x00042426
		public static WSUtilitySpecificationVersion Default
		{
			get
			{
				return WSUtilitySpecificationVersion.OneDotZero;
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x060012E5 RID: 4837 RVA: 0x0004422D File Offset: 0x0004242D
		internal XmlDictionaryString NamespaceUri
		{
			get
			{
				return this.namespaceUri;
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x060012E6 RID: 4838 RVA: 0x00044235 File Offset: 0x00042435
		public static WSUtilitySpecificationVersion OneDotZero
		{
			get
			{
				return WSUtilitySpecificationVersion.WSUtilitySpecificationVersionOneDotZero.Instance;
			}
		}

		// Token: 0x060012E7 RID: 4839
		internal abstract bool IsReaderAtTimestamp(XmlDictionaryReader reader);

		// Token: 0x060012E8 RID: 4840
		internal abstract SecurityTimestamp ReadTimestamp(XmlDictionaryReader reader, string digestAlgorithm, SignatureResourcePool resourcePool);

		// Token: 0x060012E9 RID: 4841
		internal abstract void WriteTimestamp(XmlDictionaryWriter writer, SecurityTimestamp timestamp);

		// Token: 0x060012EA RID: 4842
		internal abstract void WriteTimestampCanonicalForm(Stream stream, SecurityTimestamp timestamp, byte[] buffer);

		// Token: 0x04001A06 RID: 6662
		internal static readonly string[] AcceptedDateTimeFormats = new string[]
		{
			"yyyy-MM-ddTHH:mm:ss.fffffffZ",
			"yyyy-MM-ddTHH:mm:ss.ffffffZ",
			"yyyy-MM-ddTHH:mm:ss.fffffZ",
			"yyyy-MM-ddTHH:mm:ss.ffffZ",
			"yyyy-MM-ddTHH:mm:ss.fffZ",
			"yyyy-MM-ddTHH:mm:ss.ffZ",
			"yyyy-MM-ddTHH:mm:ss.fZ",
			"yyyy-MM-ddTHH:mm:ssZ"
		};

		// Token: 0x04001A07 RID: 6663
		private readonly XmlDictionaryString namespaceUri;

		// Token: 0x02000B22 RID: 2850
		private sealed class WSUtilitySpecificationVersionOneDotZero : WSUtilitySpecificationVersion
		{
			// Token: 0x06006FBD RID: 28605 RVA: 0x0019E709 File Offset: 0x0019C909
			private WSUtilitySpecificationVersionOneDotZero() : base(XD.UtilityDictionary.Namespace)
			{
			}

			// Token: 0x17001A0E RID: 6670
			// (get) Token: 0x06006FBE RID: 28606 RVA: 0x0019E71B File Offset: 0x0019C91B
			public static WSUtilitySpecificationVersion.WSUtilitySpecificationVersionOneDotZero Instance
			{
				get
				{
					return WSUtilitySpecificationVersion.WSUtilitySpecificationVersionOneDotZero.instance;
				}
			}

			// Token: 0x06006FBF RID: 28607 RVA: 0x0019E722 File Offset: 0x0019C922
			internal override bool IsReaderAtTimestamp(XmlDictionaryReader reader)
			{
				return reader.IsStartElement(XD.UtilityDictionary.Timestamp, XD.UtilityDictionary.Namespace);
			}

			// Token: 0x06006FC0 RID: 28608 RVA: 0x0019E740 File Offset: 0x0019C940
			internal override SecurityTimestamp ReadTimestamp(XmlDictionaryReader reader, string digestAlgorithm, SignatureResourcePool resourcePool)
			{
				bool flag = digestAlgorithm != null && reader.CanCanonicalize;
				HashStream hashStream = null;
				reader.MoveToStartElement(XD.UtilityDictionary.Timestamp, XD.UtilityDictionary.Namespace);
				if (flag)
				{
					hashStream = resourcePool.TakeHashStream(digestAlgorithm);
					reader.StartCanonicalization(hashStream, false, null);
				}
				string attribute = reader.GetAttribute(XD.UtilityDictionary.IdAttribute, XD.UtilityDictionary.Namespace);
				reader.ReadStartElement();
				reader.ReadStartElement(XD.UtilityDictionary.CreatedElement, XD.UtilityDictionary.Namespace);
				DateTime creationTimeUtc = reader.ReadContentAsDateTime().ToUniversalTime();
				reader.ReadEndElement();
				DateTime expiryTimeUtc;
				if (reader.IsStartElement(XD.UtilityDictionary.ExpiresElement, XD.UtilityDictionary.Namespace))
				{
					reader.ReadStartElement();
					expiryTimeUtc = reader.ReadContentAsDateTime().ToUniversalTime();
					reader.ReadEndElement();
				}
				else
				{
					expiryTimeUtc = SecurityUtils.MaxUtcDateTime;
				}
				reader.ReadEndElement();
				byte[] digest;
				if (flag)
				{
					reader.EndCanonicalization();
					digest = hashStream.FlushHashAndGetValue();
				}
				else
				{
					digest = null;
				}
				return new SecurityTimestamp(creationTimeUtc, expiryTimeUtc, attribute, digestAlgorithm, digest);
			}

			// Token: 0x06006FC1 RID: 28609 RVA: 0x0019E848 File Offset: 0x0019CA48
			internal override void WriteTimestamp(XmlDictionaryWriter writer, SecurityTimestamp timestamp)
			{
				writer.WriteStartElement(XD.UtilityDictionary.Prefix.Value, XD.UtilityDictionary.Timestamp, XD.UtilityDictionary.Namespace);
				writer.WriteAttributeString(XD.UtilityDictionary.IdAttribute, XD.UtilityDictionary.Namespace, timestamp.Id);
				writer.WriteStartElement(XD.UtilityDictionary.CreatedElement, XD.UtilityDictionary.Namespace);
				char[] creationTimeChars = timestamp.GetCreationTimeChars();
				writer.WriteChars(creationTimeChars, 0, creationTimeChars.Length);
				writer.WriteEndElement();
				writer.WriteStartElement(XD.UtilityDictionary.ExpiresElement, XD.UtilityDictionary.Namespace);
				char[] expiryTimeChars = timestamp.GetExpiryTimeChars();
				writer.WriteChars(expiryTimeChars, 0, expiryTimeChars.Length);
				writer.WriteEndElement();
				writer.WriteEndElement();
			}

			// Token: 0x06006FC2 RID: 28610 RVA: 0x0019E908 File Offset: 0x0019CB08
			internal override void WriteTimestampCanonicalForm(Stream stream, SecurityTimestamp timestamp, byte[] workBuffer)
			{
				WSUtilitySpecificationVersion.TimestampCanonicalFormWriter.Instance.WriteCanonicalForm(stream, timestamp.Id, timestamp.GetCreationTimeChars(), timestamp.GetExpiryTimeChars(), workBuffer);
			}

			// Token: 0x04003FE0 RID: 16352
			private static readonly WSUtilitySpecificationVersion.WSUtilitySpecificationVersionOneDotZero instance = new WSUtilitySpecificationVersion.WSUtilitySpecificationVersionOneDotZero();
		}

		// Token: 0x02000B23 RID: 2851
		private sealed class TimestampCanonicalFormWriter : CanonicalFormWriter
		{
			// Token: 0x06006FC4 RID: 28612 RVA: 0x0019E934 File Offset: 0x0019CB34
			private TimestampCanonicalFormWriter()
			{
				UTF8Encoding utf8WithoutPreamble = CanonicalFormWriter.Utf8WithoutPreamble;
				this.fragment1 = utf8WithoutPreamble.GetBytes("<u:Timestamp xmlns:u=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd\" u:Id=\"");
				this.fragment2 = utf8WithoutPreamble.GetBytes("\"><u:Created>");
				this.fragment3 = utf8WithoutPreamble.GetBytes("</u:Created><u:Expires>");
				this.fragment4 = utf8WithoutPreamble.GetBytes("</u:Expires></u:Timestamp>");
			}

			// Token: 0x17001A0F RID: 6671
			// (get) Token: 0x06006FC5 RID: 28613 RVA: 0x0019E991 File Offset: 0x0019CB91
			public static WSUtilitySpecificationVersion.TimestampCanonicalFormWriter Instance
			{
				get
				{
					return WSUtilitySpecificationVersion.TimestampCanonicalFormWriter.instance;
				}
			}

			// Token: 0x06006FC6 RID: 28614 RVA: 0x0019E998 File Offset: 0x0019CB98
			public void WriteCanonicalForm(Stream stream, string id, char[] created, char[] expires, byte[] workBuffer)
			{
				stream.Write(this.fragment1, 0, this.fragment1.Length);
				CanonicalFormWriter.EncodeAndWrite(stream, workBuffer, id);
				stream.Write(this.fragment2, 0, this.fragment2.Length);
				CanonicalFormWriter.EncodeAndWrite(stream, workBuffer, created);
				stream.Write(this.fragment3, 0, this.fragment3.Length);
				CanonicalFormWriter.EncodeAndWrite(stream, workBuffer, expires);
				stream.Write(this.fragment4, 0, this.fragment4.Length);
			}

			// Token: 0x04003FE1 RID: 16353
			private const string timestamp = "u:Timestamp";

			// Token: 0x04003FE2 RID: 16354
			private const string created = "u:Created";

			// Token: 0x04003FE3 RID: 16355
			private const string expires = "u:Expires";

			// Token: 0x04003FE4 RID: 16356
			private const string idAttribute = "u:Id";

			// Token: 0x04003FE5 RID: 16357
			private const string ns = "xmlns:u=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd\"";

			// Token: 0x04003FE6 RID: 16358
			private const string xml1 = "<u:Timestamp xmlns:u=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd\" u:Id=\"";

			// Token: 0x04003FE7 RID: 16359
			private const string xml2 = "\"><u:Created>";

			// Token: 0x04003FE8 RID: 16360
			private const string xml3 = "</u:Created><u:Expires>";

			// Token: 0x04003FE9 RID: 16361
			private const string xml4 = "</u:Expires></u:Timestamp>";

			// Token: 0x04003FEA RID: 16362
			private readonly byte[] fragment1;

			// Token: 0x04003FEB RID: 16363
			private readonly byte[] fragment2;

			// Token: 0x04003FEC RID: 16364
			private readonly byte[] fragment3;

			// Token: 0x04003FED RID: 16365
			private readonly byte[] fragment4;

			// Token: 0x04003FEE RID: 16366
			private static readonly WSUtilitySpecificationVersion.TimestampCanonicalFormWriter instance = new WSUtilitySpecificationVersion.TimestampCanonicalFormWriter();
		}
	}
}
