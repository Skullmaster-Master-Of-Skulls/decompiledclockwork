using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.ServiceModel.Diagnostics;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.ServiceModel.Syndication
{
	// Token: 0x02000196 RID: 406
	[TypeForwardedFrom("System.ServiceModel.Web, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	[XmlRoot(ElementName = "rss", Namespace = "")]
	public class Rss20FeedFormatter : SyndicationFeedFormatter, IXmlSerializable
	{
		// Token: 0x06000CEF RID: 3311 RVA: 0x0002DC13 File Offset: 0x0002BE13
		public Rss20FeedFormatter() : this(typeof(SyndicationFeed))
		{
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x0002DC28 File Offset: 0x0002BE28
		public Rss20FeedFormatter(Type feedTypeToCreate)
		{
			if (feedTypeToCreate == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("feedTypeToCreate");
			}
			if (!typeof(SyndicationFeed).IsAssignableFrom(feedTypeToCreate))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("feedTypeToCreate", SR.GetString("InvalidObjectTypePassed", new object[]
				{
					"feedTypeToCreate",
					"SyndicationFeed"
				}));
			}
			this.serializeExtensionsAsAtom = true;
			this.maxExtensionSize = int.MaxValue;
			this.preserveElementExtensions = true;
			this.preserveAttributeExtensions = true;
			this.atomSerializer = new Atom10FeedFormatter(feedTypeToCreate);
			this.feedType = feedTypeToCreate;
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x0002DCC9 File Offset: 0x0002BEC9
		public Rss20FeedFormatter(SyndicationFeed feedToWrite) : this(feedToWrite, true)
		{
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x0002DCD4 File Offset: 0x0002BED4
		public Rss20FeedFormatter(SyndicationFeed feedToWrite, bool serializeExtensionsAsAtom) : base(feedToWrite)
		{
			this.serializeExtensionsAsAtom = serializeExtensionsAsAtom;
			this.maxExtensionSize = int.MaxValue;
			this.preserveElementExtensions = true;
			this.preserveAttributeExtensions = true;
			this.atomSerializer = new Atom10FeedFormatter(base.Feed);
			this.feedType = feedToWrite.GetType();
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000CF3 RID: 3315 RVA: 0x0002DD25 File Offset: 0x0002BF25
		// (set) Token: 0x06000CF4 RID: 3316 RVA: 0x0002DD2D File Offset: 0x0002BF2D
		public bool PreserveAttributeExtensions
		{
			get
			{
				return this.preserveAttributeExtensions;
			}
			set
			{
				this.preserveAttributeExtensions = value;
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000CF5 RID: 3317 RVA: 0x0002DD36 File Offset: 0x0002BF36
		// (set) Token: 0x06000CF6 RID: 3318 RVA: 0x0002DD3E File Offset: 0x0002BF3E
		public bool PreserveElementExtensions
		{
			get
			{
				return this.preserveElementExtensions;
			}
			set
			{
				this.preserveElementExtensions = value;
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000CF7 RID: 3319 RVA: 0x0002DD47 File Offset: 0x0002BF47
		// (set) Token: 0x06000CF8 RID: 3320 RVA: 0x0002DD4F File Offset: 0x0002BF4F
		public bool SerializeExtensionsAsAtom
		{
			get
			{
				return this.serializeExtensionsAsAtom;
			}
			set
			{
				this.serializeExtensionsAsAtom = value;
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000CF9 RID: 3321 RVA: 0x0002DD58 File Offset: 0x0002BF58
		public override string Version
		{
			get
			{
				return "Rss20";
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000CFA RID: 3322 RVA: 0x0002DD5F File Offset: 0x0002BF5F
		protected Type FeedType
		{
			get
			{
				return this.feedType;
			}
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x0002DD67 File Offset: 0x0002BF67
		public override bool CanRead(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			return reader.IsStartElement("rss", "");
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x0002DD8C File Offset: 0x0002BF8C
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x0002DD8F File Offset: 0x0002BF8F
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			SyndicationFeedFormatter.TraceFeedReadBegin();
			this.ReadFeed(reader);
			SyndicationFeedFormatter.TraceFeedReadEnd();
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x0002DDB5 File Offset: 0x0002BFB5
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			SyndicationFeedFormatter.TraceFeedWriteBegin();
			this.WriteFeed(writer);
			SyndicationFeedFormatter.TraceFeedWriteEnd();
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x0002DDDC File Offset: 0x0002BFDC
		public override void ReadFrom(XmlReader reader)
		{
			SyndicationFeedFormatter.TraceFeedReadBegin();
			if (!this.CanRead(reader))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("UnknownFeedXml", new object[]
				{
					reader.LocalName,
					reader.NamespaceURI
				})));
			}
			this.ReadFeed(reader);
			SyndicationFeedFormatter.TraceFeedReadEnd();
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x0002DE35 File Offset: 0x0002C035
		public override void WriteTo(XmlWriter writer)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			SyndicationFeedFormatter.TraceFeedWriteBegin();
			writer.WriteStartElement("rss", "");
			this.WriteFeed(writer);
			writer.WriteEndElement();
			SyndicationFeedFormatter.TraceFeedWriteEnd();
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x0002DE71 File Offset: 0x0002C071
		protected internal override void SetFeed(SyndicationFeed feed)
		{
			base.SetFeed(feed);
			this.atomSerializer.SetFeed(base.Feed);
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x0002DE8B File Offset: 0x0002C08B
		internal static void TraceExtensionsIgnoredOnWrite(string message)
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 983074, SR.GetString(message));
			}
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x0002DEA5 File Offset: 0x0002C0A5
		internal void ReadItemFrom(XmlReader reader, SyndicationItem result)
		{
			this.ReadItemFrom(reader, result, null);
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x0002DEB0 File Offset: 0x0002C0B0
		internal void WriteItemContents(XmlWriter writer, SyndicationItem item)
		{
			this.WriteItemContents(writer, item, null);
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x0002DEBB File Offset: 0x0002C0BB
		protected override SyndicationFeed CreateFeedInstance()
		{
			return SyndicationFeedFormatter.CreateFeedInstance(this.feedType);
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x0002DEC8 File Offset: 0x0002C0C8
		protected virtual SyndicationItem ReadItem(XmlReader reader, SyndicationFeed feed)
		{
			if (feed == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("feed");
			}
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			SyndicationItem result = SyndicationFeedFormatter.CreateItem(feed);
			SyndicationFeedFormatter.TraceItemReadBegin();
			this.ReadItemFrom(reader, result, feed.BaseUri);
			SyndicationFeedFormatter.TraceItemReadEnd();
			return result;
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x0002DF1C File Offset: 0x0002C11C
		protected virtual IEnumerable<SyndicationItem> ReadItems(XmlReader reader, SyndicationFeed feed, out bool areAllItemsRead)
		{
			if (feed == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("feed");
			}
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			NullNotAllowedCollection<SyndicationItem> nullNotAllowedCollection = new NullNotAllowedCollection<SyndicationItem>();
			while (reader.IsStartElement("item", ""))
			{
				nullNotAllowedCollection.Add(this.ReadItem(reader, feed));
			}
			areAllItemsRead = true;
			return nullNotAllowedCollection;
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x0002DF7B File Offset: 0x0002C17B
		protected virtual void WriteItem(XmlWriter writer, SyndicationItem item, Uri feedBaseUri)
		{
			SyndicationFeedFormatter.TraceItemWriteBegin();
			writer.WriteStartElement("item", "");
			this.WriteItemContents(writer, item, feedBaseUri);
			writer.WriteEndElement();
			SyndicationFeedFormatter.TraceItemWriteEnd();
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x0002DFA8 File Offset: 0x0002C1A8
		protected virtual void WriteItems(XmlWriter writer, IEnumerable<SyndicationItem> items, Uri feedBaseUri)
		{
			if (items == null)
			{
				return;
			}
			foreach (SyndicationItem item in items)
			{
				this.WriteItem(writer, item, feedBaseUri);
			}
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x0002DFF8 File Offset: 0x0002C1F8
		private static DateTimeOffset DateFromString(string dateTimeString, XmlReader reader)
		{
			StringBuilder stringBuilder = new StringBuilder(dateTimeString.Trim());
			if (stringBuilder.Length < 18)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(FeedUtils.AddLineInfo(reader, "ErrorParsingDateTime")));
			}
			if (stringBuilder[3] == ',')
			{
				stringBuilder.Remove(0, 4);
				Rss20FeedFormatter.RemoveExtraWhiteSpaceAtStart(stringBuilder);
			}
			Rss20FeedFormatter.ReplaceMultipleWhiteSpaceWithSingleWhiteSpace(stringBuilder);
			if (!char.IsDigit(stringBuilder[1]))
			{
				stringBuilder.Insert(0, '0');
			}
			if (stringBuilder.Length < 19)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(FeedUtils.AddLineInfo(reader, "ErrorParsingDateTime")));
			}
			bool flag = stringBuilder[17] == ':';
			int num;
			if (flag)
			{
				num = 21;
			}
			else
			{
				num = 18;
			}
			string rfc822TimeZone = stringBuilder.ToString().Substring(num);
			stringBuilder.Remove(num, stringBuilder.Length - num);
			bool flag2;
			stringBuilder.Append(Rss20FeedFormatter.NormalizeTimeZone(rfc822TimeZone, out flag2));
			string input = stringBuilder.ToString();
			string format;
			if (flag)
			{
				format = "dd MMM yyyy HH:mm:ss zzz";
			}
			else
			{
				format = "dd MMM yyyy HH:mm zzz";
			}
			DateTimeOffset result;
			if (DateTimeOffset.TryParseExact(input, format, CultureInfo.InvariantCulture.DateTimeFormat, flag2 ? DateTimeStyles.AdjustToUniversal : DateTimeStyles.None, out result))
			{
				return result;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(FeedUtils.AddLineInfo(reader, "ErrorParsingDateTime")));
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x0002E130 File Offset: 0x0002C330
		private static string NormalizeTimeZone(string rfc822TimeZone, out bool isUtc)
		{
			isUtc = false;
			if (rfc822TimeZone[0] == '+' || rfc822TimeZone[0] == '-')
			{
				StringBuilder stringBuilder = new StringBuilder(rfc822TimeZone);
				if (stringBuilder.Length == 4)
				{
					stringBuilder.Insert(1, '0');
				}
				stringBuilder.Insert(3, ':');
				return stringBuilder.ToString();
			}
			uint num = <PrivateImplementationDetails>.ComputeStringHash(rfc822TimeZone);
			if (num <= 3356228888U)
			{
				if (num <= 3140198408U)
				{
					if (num <= 1727238636U)
					{
						if (num <= 339145631U)
						{
							if (num != 308297344U)
							{
								if (num != 339145631U)
								{
									goto IL_618;
								}
								if (!(rfc822TimeZone == "MST"))
								{
									goto IL_618;
								}
								goto IL_5AC;
							}
							else
							{
								if (!(rfc822TimeZone == "MDT"))
								{
									goto IL_618;
								}
								goto IL_5A6;
							}
						}
						else if (num != 995535842U)
						{
							if (num != 1727238636U)
							{
								goto IL_618;
							}
							if (!(rfc822TimeZone == "UT"))
							{
								goto IL_618;
							}
							goto IL_579;
						}
						else
						{
							if (!(rfc822TimeZone == "CDT"))
							{
								goto IL_618;
							}
							goto IL_5A0;
						}
					}
					else if (num <= 2586683136U)
					{
						if (num != 2099357029U)
						{
							if (num != 2586683136U)
							{
								goto IL_618;
							}
							if (!(rfc822TimeZone == "PST"))
							{
								goto IL_618;
							}
							goto IL_5B2;
						}
						else
						{
							if (!(rfc822TimeZone == "CST"))
							{
								goto IL_618;
							}
							goto IL_5A6;
						}
					}
					else if (num != 2617531423U)
					{
						if (num != 3140198408U)
						{
							goto IL_618;
						}
						if (!(rfc822TimeZone == "EDT"))
						{
							goto IL_618;
						}
					}
					else
					{
						if (!(rfc822TimeZone == "PDT"))
						{
							goto IL_618;
						}
						goto IL_5AC;
					}
				}
				else if (num <= 3272340793U)
				{
					if (num <= 3238785555U)
					{
						if (num != 3222007936U)
						{
							if (num != 3238785555U)
							{
								goto IL_618;
							}
							if (!(rfc822TimeZone == "D"))
							{
								goto IL_618;
							}
						}
						else
						{
							if (!(rfc822TimeZone == "E"))
							{
								goto IL_618;
							}
							goto IL_5A0;
						}
					}
					else if (num != 3255563174U)
					{
						if (num != 3272340793U)
						{
							goto IL_618;
						}
						if (!(rfc822TimeZone == "F"))
						{
							goto IL_618;
						}
						goto IL_5A6;
					}
					else
					{
						if (!(rfc822TimeZone == "G"))
						{
							goto IL_618;
						}
						goto IL_5AC;
					}
				}
				else if (num <= 3322673650U)
				{
					if (num != 3289118412U)
					{
						if (num != 3322673650U)
						{
							goto IL_618;
						}
						if (!(rfc822TimeZone == "C"))
						{
							goto IL_618;
						}
						return "-03:00";
					}
					else
					{
						if (!(rfc822TimeZone == "A"))
						{
							goto IL_618;
						}
						return "-01:00";
					}
				}
				else if (num != 3338310079U)
				{
					if (num != 3339451269U)
					{
						if (num != 3356228888U)
						{
							goto IL_618;
						}
						if (!(rfc822TimeZone == "M"))
						{
							goto IL_618;
						}
						return "-12:00";
					}
					else
					{
						if (!(rfc822TimeZone == "B"))
						{
							goto IL_618;
						}
						return "-02:00";
					}
				}
				else
				{
					if (!(rfc822TimeZone == "GMT"))
					{
						goto IL_618;
					}
					return "-00:00";
				}
				return "-04:00";
				IL_5A6:
				return "-06:00";
				IL_5AC:
				return "-07:00";
			}
			if (num <= 3524005078U)
			{
				if (num <= 3423339364U)
				{
					if (num <= 3389784126U)
					{
						if (num != 3373006507U)
						{
							if (num != 3389784126U)
							{
								goto IL_618;
							}
							if (!(rfc822TimeZone == "O"))
							{
								goto IL_618;
							}
							return "+02:00";
						}
						else
						{
							if (!(rfc822TimeZone == "L"))
							{
								goto IL_618;
							}
							return "-11:00";
						}
					}
					else if (num != 3406561745U)
					{
						if (num != 3423339364U)
						{
							goto IL_618;
						}
						if (!(rfc822TimeZone == "I"))
						{
							goto IL_618;
						}
						return "-09:00";
					}
					else
					{
						if (!(rfc822TimeZone == "N"))
						{
							goto IL_618;
						}
						return "+01:00";
					}
				}
				else if (num <= 3456894602U)
				{
					if (num != 3440116983U)
					{
						if (num != 3456894602U)
						{
							goto IL_618;
						}
						if (!(rfc822TimeZone == "K"))
						{
							goto IL_618;
						}
						return "-10:00";
					}
					else
					{
						if (!(rfc822TimeZone == "H"))
						{
							goto IL_618;
						}
						goto IL_5B2;
					}
				}
				else if (num != 3490449840U)
				{
					if (num != 3507227459U)
					{
						if (num != 3524005078U)
						{
							goto IL_618;
						}
						if (!(rfc822TimeZone == "W"))
						{
							goto IL_618;
						}
						return "+10:00";
					}
					else
					{
						if (!(rfc822TimeZone == "T"))
						{
							goto IL_618;
						}
						return "+07:00";
					}
				}
				else
				{
					if (!(rfc822TimeZone == "U"))
					{
						goto IL_618;
					}
					return "+08:00";
				}
			}
			else if (num <= 3591115554U)
			{
				if (num <= 3557560316U)
				{
					if (num != 3540782697U)
					{
						if (num != 3557560316U)
						{
							goto IL_618;
						}
						if (!(rfc822TimeZone == "Q"))
						{
							goto IL_618;
						}
						return "+04:00";
					}
					else
					{
						if (!(rfc822TimeZone == "V"))
						{
							goto IL_618;
						}
						return "+09:00";
					}
				}
				else if (num != 3574337935U)
				{
					if (num != 3591115554U)
					{
						goto IL_618;
					}
					if (!(rfc822TimeZone == "S"))
					{
						goto IL_618;
					}
					return "+06:00";
				}
				else
				{
					if (!(rfc822TimeZone == "P"))
					{
						goto IL_618;
					}
					return "+03:00";
				}
			}
			else if (num <= 3691781268U)
			{
				if (num != 3607893173U)
				{
					if (num != 3691781268U)
					{
						goto IL_618;
					}
					if (!(rfc822TimeZone == "Y"))
					{
						goto IL_618;
					}
					return "+12:00";
				}
				else
				{
					if (!(rfc822TimeZone == "R"))
					{
						goto IL_618;
					}
					return "+05:00";
				}
			}
			else if (num != 3708558887U)
			{
				if (num != 3742114125U)
				{
					if (num != 4249934023U)
					{
						goto IL_618;
					}
					if (!(rfc822TimeZone == "EST"))
					{
						goto IL_618;
					}
					goto IL_5A0;
				}
				else if (!(rfc822TimeZone == "Z"))
				{
					goto IL_618;
				}
			}
			else
			{
				if (!(rfc822TimeZone == "X"))
				{
					goto IL_618;
				}
				return "+11:00";
			}
			IL_579:
			isUtc = true;
			return "-00:00";
			IL_5A0:
			return "-05:00";
			IL_5B2:
			return "-08:00";
			IL_618:
			return "";
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x0002E75C File Offset: 0x0002C95C
		private static void RemoveExtraWhiteSpaceAtStart(StringBuilder stringBuilder)
		{
			int num = 0;
			while (num < stringBuilder.Length && char.IsWhiteSpace(stringBuilder[num]))
			{
				num++;
			}
			if (num > 0)
			{
				stringBuilder.Remove(0, num);
			}
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x0002E798 File Offset: 0x0002C998
		private static void ReplaceMultipleWhiteSpaceWithSingleWhiteSpace(StringBuilder builder)
		{
			int i = 0;
			int num = -1;
			while (i < builder.Length)
			{
				if (char.IsWhiteSpace(builder[i]))
				{
					if (num < 0)
					{
						num = i;
						builder[i] = ' ';
					}
				}
				else if (num >= 0)
				{
					if (i > num + 1)
					{
						builder.Remove(num, i - num - 1);
						i = num + 1;
					}
					num = -1;
				}
				i++;
			}
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x0002E7F4 File Offset: 0x0002C9F4
		private string AsString(DateTimeOffset dateTime)
		{
			if (dateTime.Offset == Atom10FeedFormatter.zeroOffset)
			{
				return dateTime.ToUniversalTime().ToString("ddd, dd MMM yyyy HH:mm:ss Z", CultureInfo.InvariantCulture);
			}
			StringBuilder stringBuilder = new StringBuilder(dateTime.ToString("ddd, dd MMM yyyy HH:mm:ss zzz", CultureInfo.InvariantCulture));
			stringBuilder.Remove(stringBuilder.Length - 3, 1);
			return stringBuilder.ToString();
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x0002E85C File Offset: 0x0002CA5C
		private SyndicationLink ReadAlternateLink(XmlReader reader, Uri baseUri)
		{
			SyndicationLink syndicationLink = new SyndicationLink();
			syndicationLink.BaseUri = baseUri;
			syndicationLink.RelationshipType = "alternate";
			if (reader.HasAttributes)
			{
				while (reader.MoveToNextAttribute())
				{
					if (reader.LocalName == "base" && reader.NamespaceURI == "http://www.w3.org/XML/1998/namespace")
					{
						syndicationLink.BaseUri = FeedUtils.CombineXmlBase(syndicationLink.BaseUri, reader.Value);
					}
					else if (!FeedUtils.IsXmlns(reader.LocalName, reader.NamespaceURI))
					{
						if (this.PreserveAttributeExtensions)
						{
							syndicationLink.AttributeExtensions.Add(new XmlQualifiedName(reader.LocalName, reader.NamespaceURI), reader.Value);
						}
						else
						{
							SyndicationFeedFormatter.TraceSyndicationElementIgnoredOnRead(reader);
						}
					}
				}
			}
			string uriString = reader.ReadElementString();
			syndicationLink.Uri = new Uri(uriString, UriKind.RelativeOrAbsolute);
			return syndicationLink;
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x0002E934 File Offset: 0x0002CB34
		private SyndicationCategory ReadCategory(XmlReader reader, SyndicationFeed feed)
		{
			SyndicationCategory syndicationCategory = SyndicationFeedFormatter.CreateCategory(feed);
			this.ReadCategory(reader, syndicationCategory);
			return syndicationCategory;
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x0002E954 File Offset: 0x0002CB54
		private SyndicationCategory ReadCategory(XmlReader reader, SyndicationItem item)
		{
			SyndicationCategory syndicationCategory = SyndicationFeedFormatter.CreateCategory(item);
			this.ReadCategory(reader, syndicationCategory);
			return syndicationCategory;
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x0002E974 File Offset: 0x0002CB74
		private void ReadCategory(XmlReader reader, SyndicationCategory category)
		{
			bool isEmptyElement = reader.IsEmptyElement;
			if (reader.HasAttributes)
			{
				while (reader.MoveToNextAttribute())
				{
					string namespaceURI = reader.NamespaceURI;
					string localName = reader.LocalName;
					if (!FeedUtils.IsXmlns(localName, namespaceURI))
					{
						string value = reader.Value;
						if (localName == "domain" && namespaceURI == "")
						{
							category.Scheme = value;
						}
						else if (!SyndicationFeedFormatter.TryParseAttribute(localName, namespaceURI, value, category, this.Version))
						{
							if (this.preserveAttributeExtensions)
							{
								category.AttributeExtensions.Add(new XmlQualifiedName(localName, namespaceURI), value);
							}
							else
							{
								SyndicationFeedFormatter.TraceSyndicationElementIgnoredOnRead(reader);
							}
						}
					}
				}
			}
			reader.ReadStartElement("category", "");
			if (!isEmptyElement)
			{
				category.Name = reader.ReadString();
				reader.ReadEndElement();
			}
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x0002EA34 File Offset: 0x0002CC34
		private void ReadFeed(XmlReader reader)
		{
			this.SetFeed(this.CreateFeedInstance());
			this.ReadXml(reader, base.Feed);
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x0002EA50 File Offset: 0x0002CC50
		private void ReadItemFrom(XmlReader reader, SyndicationItem result, Uri feedBaseUri)
		{
			try
			{
				result.BaseUri = feedBaseUri;
				reader.MoveToContent();
				bool isEmptyElement = reader.IsEmptyElement;
				if (reader.HasAttributes)
				{
					while (reader.MoveToNextAttribute())
					{
						string namespaceURI = reader.NamespaceURI;
						string localName = reader.LocalName;
						if (localName == "base" && namespaceURI == "http://www.w3.org/XML/1998/namespace")
						{
							result.BaseUri = FeedUtils.CombineXmlBase(result.BaseUri, reader.Value);
						}
						else if (!FeedUtils.IsXmlns(localName, namespaceURI) && !FeedUtils.IsXmlSchemaType(localName, namespaceURI))
						{
							string value = reader.Value;
							if (!SyndicationFeedFormatter.TryParseAttribute(localName, namespaceURI, value, result, this.Version))
							{
								if (this.preserveAttributeExtensions)
								{
									result.AttributeExtensions.Add(new XmlQualifiedName(localName, namespaceURI), value);
								}
								else
								{
									SyndicationFeedFormatter.TraceSyndicationElementIgnoredOnRead(reader);
								}
							}
						}
					}
				}
				reader.ReadStartElement();
				if (!isEmptyElement)
				{
					string text = null;
					XmlDictionaryWriter xmlDictionaryWriter = null;
					bool flag = false;
					try
					{
						XmlBuffer buffer = null;
						while (reader.IsStartElement())
						{
							if (reader.IsStartElement("title", ""))
							{
								result.Title = new TextSyndicationContent(reader.ReadElementString());
							}
							else if (reader.IsStartElement("link", ""))
							{
								result.Links.Add(this.ReadAlternateLink(reader, result.BaseUri));
								flag = true;
							}
							else if (reader.IsStartElement("description", ""))
							{
								result.Summary = new TextSyndicationContent(reader.ReadElementString());
							}
							else if (reader.IsStartElement("author", ""))
							{
								result.Authors.Add(this.ReadPerson(reader, result));
							}
							else if (reader.IsStartElement("category", ""))
							{
								result.Categories.Add(this.ReadCategory(reader, result));
							}
							else if (reader.IsStartElement("enclosure", ""))
							{
								result.Links.Add(this.ReadMediaEnclosure(reader, result.BaseUri));
							}
							else if (reader.IsStartElement("guid", ""))
							{
								bool flag2 = true;
								string attribute = reader.GetAttribute("isPermaLink", "");
								if (attribute != null && attribute.ToUpperInvariant() == "FALSE")
								{
									flag2 = false;
								}
								result.Id = reader.ReadElementString();
								if (flag2)
								{
									text = result.Id;
								}
							}
							else if (reader.IsStartElement("pubDate", ""))
							{
								bool flag3 = !reader.IsEmptyElement;
								reader.ReadStartElement();
								if (flag3)
								{
									string text2 = reader.ReadString();
									if (!string.IsNullOrEmpty(text2))
									{
										result.PublishDate = Rss20FeedFormatter.DateFromString(text2, reader);
									}
									reader.ReadEndElement();
								}
							}
							else if (reader.IsStartElement("source", ""))
							{
								SyndicationFeed syndicationFeed = new SyndicationFeed();
								if (reader.HasAttributes)
								{
									while (reader.MoveToNextAttribute())
									{
										string namespaceURI2 = reader.NamespaceURI;
										string localName2 = reader.LocalName;
										if (!FeedUtils.IsXmlns(localName2, namespaceURI2))
										{
											string value2 = reader.Value;
											if (localName2 == "url" && namespaceURI2 == "")
											{
												syndicationFeed.Links.Add(SyndicationLink.CreateSelfLink(new Uri(value2, UriKind.RelativeOrAbsolute)));
											}
											else if (!FeedUtils.IsXmlns(localName2, namespaceURI2))
											{
												if (this.preserveAttributeExtensions)
												{
													syndicationFeed.AttributeExtensions.Add(new XmlQualifiedName(localName2, namespaceURI2), value2);
												}
												else
												{
													SyndicationFeedFormatter.TraceSyndicationElementIgnoredOnRead(reader);
												}
											}
										}
									}
								}
								string text3 = reader.ReadElementString();
								syndicationFeed.Title = new TextSyndicationContent(text3);
								result.SourceFeed = syndicationFeed;
							}
							else
							{
								bool flag4 = this.serializeExtensionsAsAtom && this.atomSerializer.TryParseItemElementFrom(reader, result);
								if (!flag4)
								{
									flag4 = SyndicationFeedFormatter.TryParseElement(reader, result, this.Version);
								}
								if (!flag4)
								{
									if (this.preserveElementExtensions)
									{
										SyndicationFeedFormatter.CreateBufferIfRequiredAndWriteNode(ref buffer, ref xmlDictionaryWriter, reader, this.maxExtensionSize);
									}
									else
									{
										SyndicationFeedFormatter.TraceSyndicationElementIgnoredOnRead(reader);
										reader.Skip();
									}
								}
							}
						}
						SyndicationFeedFormatter.LoadElementExtensions(buffer, xmlDictionaryWriter, result);
					}
					finally
					{
						if (xmlDictionaryWriter != null)
						{
							((IDisposable)xmlDictionaryWriter).Dispose();
						}
					}
					reader.ReadEndElement();
					if (!flag && text != null)
					{
						result.Links.Add(SyndicationLink.CreateAlternateLink(new Uri(text, UriKind.RelativeOrAbsolute)));
						flag = true;
					}
					if (result.Content == null && !flag)
					{
						result.Content = result.Summary;
						result.Summary = null;
					}
				}
			}
			catch (FormatException innerException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(FeedUtils.AddLineInfo(reader, "ErrorParsingItem"), innerException));
			}
			catch (ArgumentException innerException2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(FeedUtils.AddLineInfo(reader, "ErrorParsingItem"), innerException2));
			}
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x0002EF24 File Offset: 0x0002D124
		private SyndicationLink ReadMediaEnclosure(XmlReader reader, Uri baseUri)
		{
			SyndicationLink syndicationLink = new SyndicationLink();
			syndicationLink.BaseUri = baseUri;
			syndicationLink.RelationshipType = "enclosure";
			bool isEmptyElement = reader.IsEmptyElement;
			if (reader.HasAttributes)
			{
				while (reader.MoveToNextAttribute())
				{
					string namespaceURI = reader.NamespaceURI;
					string localName = reader.LocalName;
					if (localName == "base" && namespaceURI == "http://www.w3.org/XML/1998/namespace")
					{
						syndicationLink.BaseUri = FeedUtils.CombineXmlBase(syndicationLink.BaseUri, reader.Value);
					}
					else if (!FeedUtils.IsXmlns(localName, namespaceURI))
					{
						string value = reader.Value;
						if (localName == "url" && namespaceURI == "")
						{
							syndicationLink.Uri = new Uri(value, UriKind.RelativeOrAbsolute);
						}
						else if (localName == "type" && namespaceURI == "")
						{
							syndicationLink.MediaType = value;
						}
						else if (localName == "length" && namespaceURI == "")
						{
							syndicationLink.Length = ((!string.IsNullOrEmpty(value)) ? Convert.ToInt64(value, CultureInfo.InvariantCulture.NumberFormat) : 0L);
						}
						else if (!FeedUtils.IsXmlns(localName, namespaceURI))
						{
							if (this.preserveAttributeExtensions)
							{
								syndicationLink.AttributeExtensions.Add(new XmlQualifiedName(localName, namespaceURI), value);
							}
							else
							{
								SyndicationFeedFormatter.TraceSyndicationElementIgnoredOnRead(reader);
							}
						}
					}
				}
			}
			reader.ReadStartElement("enclosure", "");
			if (!isEmptyElement)
			{
				reader.ReadEndElement();
			}
			return syndicationLink;
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x0002F09C File Offset: 0x0002D29C
		private SyndicationPerson ReadPerson(XmlReader reader, SyndicationFeed feed)
		{
			SyndicationPerson syndicationPerson = SyndicationFeedFormatter.CreatePerson(feed);
			this.ReadPerson(reader, syndicationPerson);
			return syndicationPerson;
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x0002F0BC File Offset: 0x0002D2BC
		private SyndicationPerson ReadPerson(XmlReader reader, SyndicationItem item)
		{
			SyndicationPerson syndicationPerson = SyndicationFeedFormatter.CreatePerson(item);
			this.ReadPerson(reader, syndicationPerson);
			return syndicationPerson;
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x0002F0DC File Offset: 0x0002D2DC
		private void ReadPerson(XmlReader reader, SyndicationPerson person)
		{
			bool isEmptyElement = reader.IsEmptyElement;
			if (reader.HasAttributes)
			{
				while (reader.MoveToNextAttribute())
				{
					string namespaceURI = reader.NamespaceURI;
					string localName = reader.LocalName;
					if (!FeedUtils.IsXmlns(localName, namespaceURI))
					{
						string value = reader.Value;
						if (!SyndicationFeedFormatter.TryParseAttribute(localName, namespaceURI, value, person, this.Version))
						{
							if (this.preserveAttributeExtensions)
							{
								person.AttributeExtensions.Add(new XmlQualifiedName(localName, namespaceURI), value);
							}
							else
							{
								SyndicationFeedFormatter.TraceSyndicationElementIgnoredOnRead(reader);
							}
						}
					}
				}
			}
			reader.ReadStartElement();
			if (!isEmptyElement)
			{
				string email = reader.ReadString();
				reader.ReadEndElement();
				person.Email = email;
			}
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x0002F174 File Offset: 0x0002D374
		private void ReadXml(XmlReader reader, SyndicationFeed result)
		{
			try
			{
				string text = null;
				reader.MoveToContent();
				string attribute = reader.GetAttribute("version", "");
				if (attribute != "2.0")
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(FeedUtils.AddLineInfo(reader, SR.GetString("UnsupportedRssVersion", new object[]
					{
						attribute
					}))));
				}
				if (reader.AttributeCount > 1)
				{
					string attribute2 = reader.GetAttribute("base", "http://www.w3.org/XML/1998/namespace");
					if (!string.IsNullOrEmpty(attribute2))
					{
						text = attribute2;
					}
				}
				reader.ReadStartElement();
				reader.MoveToContent();
				if (reader.HasAttributes)
				{
					while (reader.MoveToNextAttribute())
					{
						string namespaceURI = reader.NamespaceURI;
						string localName = reader.LocalName;
						if (localName == "base" && namespaceURI == "http://www.w3.org/XML/1998/namespace")
						{
							text = reader.Value;
						}
						else if (!FeedUtils.IsXmlns(localName, namespaceURI) && !FeedUtils.IsXmlSchemaType(localName, namespaceURI))
						{
							string value = reader.Value;
							if (!SyndicationFeedFormatter.TryParseAttribute(localName, namespaceURI, value, result, this.Version))
							{
								if (this.preserveAttributeExtensions)
								{
									result.AttributeExtensions.Add(new XmlQualifiedName(localName, namespaceURI), value);
								}
								else
								{
									SyndicationFeedFormatter.TraceSyndicationElementIgnoredOnRead(reader);
								}
							}
						}
					}
				}
				if (!string.IsNullOrEmpty(text))
				{
					result.BaseUri = new Uri(text, UriKind.RelativeOrAbsolute);
				}
				bool flag = true;
				bool flag2 = false;
				reader.ReadStartElement("channel", "");
				XmlBuffer buffer = null;
				XmlDictionaryWriter xmlDictionaryWriter = null;
				try
				{
					while (reader.IsStartElement())
					{
						if (reader.IsStartElement("title", ""))
						{
							result.Title = new TextSyndicationContent(reader.ReadElementString());
						}
						else if (reader.IsStartElement("link", ""))
						{
							result.Links.Add(this.ReadAlternateLink(reader, result.BaseUri));
						}
						else if (reader.IsStartElement("description", ""))
						{
							result.Description = new TextSyndicationContent(reader.ReadElementString());
						}
						else if (reader.IsStartElement("language", ""))
						{
							result.Language = reader.ReadElementString();
						}
						else if (reader.IsStartElement("copyright", ""))
						{
							result.Copyright = new TextSyndicationContent(reader.ReadElementString());
						}
						else if (reader.IsStartElement("managingEditor", ""))
						{
							result.Authors.Add(this.ReadPerson(reader, result));
						}
						else if (reader.IsStartElement("lastBuildDate", ""))
						{
							bool flag3 = !reader.IsEmptyElement;
							reader.ReadStartElement();
							if (flag3)
							{
								string text2 = reader.ReadString();
								if (!string.IsNullOrEmpty(text2))
								{
									result.LastUpdatedTime = Rss20FeedFormatter.DateFromString(text2, reader);
								}
								reader.ReadEndElement();
							}
						}
						else if (reader.IsStartElement("category", ""))
						{
							result.Categories.Add(this.ReadCategory(reader, result));
						}
						else if (reader.IsStartElement("generator", ""))
						{
							result.Generator = reader.ReadElementString();
						}
						else if (reader.IsStartElement("image", ""))
						{
							reader.ReadStartElement();
							while (reader.IsStartElement())
							{
								if (reader.IsStartElement("url", ""))
								{
									result.ImageUrl = new Uri(reader.ReadElementString(), UriKind.RelativeOrAbsolute);
								}
								else
								{
									SyndicationFeedFormatter.TraceSyndicationElementIgnoredOnRead(reader);
									reader.Skip();
								}
							}
							reader.ReadEndElement();
						}
						else if (reader.IsStartElement("item", ""))
						{
							if (flag2)
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new InvalidOperationException(SR.GetString("FeedHasNonContiguousItems", new object[]
								{
									base.GetType().ToString()
								})));
							}
							result.Items = this.ReadItems(reader, result, out flag);
							flag2 = true;
							if (!flag)
							{
								break;
							}
						}
						else
						{
							bool flag4 = this.serializeExtensionsAsAtom && this.atomSerializer.TryParseFeedElementFrom(reader, result);
							if (!flag4)
							{
								flag4 = SyndicationFeedFormatter.TryParseElement(reader, result, this.Version);
							}
							if (!flag4)
							{
								if (this.preserveElementExtensions)
								{
									SyndicationFeedFormatter.CreateBufferIfRequiredAndWriteNode(ref buffer, ref xmlDictionaryWriter, reader, this.maxExtensionSize);
								}
								else
								{
									SyndicationFeedFormatter.TraceSyndicationElementIgnoredOnRead(reader);
									reader.Skip();
								}
							}
						}
					}
					SyndicationFeedFormatter.LoadElementExtensions(buffer, xmlDictionaryWriter, result);
				}
				finally
				{
					if (xmlDictionaryWriter != null)
					{
						((IDisposable)xmlDictionaryWriter).Dispose();
					}
				}
				if (flag)
				{
					reader.ReadEndElement();
					reader.ReadEndElement();
				}
			}
			catch (FormatException innerException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(FeedUtils.AddLineInfo(reader, "ErrorParsingFeed"), innerException));
			}
			catch (ArgumentException innerException2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(FeedUtils.AddLineInfo(reader, "ErrorParsingFeed"), innerException2));
			}
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x0002F648 File Offset: 0x0002D848
		private void WriteAlternateLink(XmlWriter writer, SyndicationLink link, Uri baseUri)
		{
			writer.WriteStartElement("link", "");
			Uri baseUriToWrite = FeedUtils.GetBaseUriToWrite(baseUri, link.BaseUri);
			if (baseUriToWrite != null)
			{
				writer.WriteAttributeString("xml", "base", "http://www.w3.org/XML/1998/namespace", FeedUtils.GetUriString(baseUriToWrite));
			}
			link.WriteAttributeExtensions(writer, "Rss20");
			writer.WriteString(FeedUtils.GetUriString(link.Uri));
			writer.WriteEndElement();
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x0002F6BC File Offset: 0x0002D8BC
		private void WriteCategory(XmlWriter writer, SyndicationCategory category)
		{
			if (category == null)
			{
				return;
			}
			writer.WriteStartElement("category", "");
			SyndicationFeedFormatter.WriteAttributeExtensions(writer, category, this.Version);
			if (!string.IsNullOrEmpty(category.Scheme) && !category.AttributeExtensions.ContainsKey(Rss20FeedFormatter.Rss20Domain))
			{
				writer.WriteAttributeString("domain", "", category.Scheme);
			}
			writer.WriteString(category.Name);
			writer.WriteEndElement();
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x0002F734 File Offset: 0x0002D934
		private void WriteFeed(XmlWriter writer)
		{
			if (base.Feed == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("FeedFormatterDoesNotHaveFeed")));
			}
			if (this.serializeExtensionsAsAtom)
			{
				writer.WriteAttributeString("xmlns", "a10", null, "http://www.w3.org/2005/Atom");
			}
			writer.WriteAttributeString("version", "2.0");
			writer.WriteStartElement("channel", "");
			if (base.Feed.BaseUri != null)
			{
				writer.WriteAttributeString("xml", "base", "http://www.w3.org/XML/1998/namespace", FeedUtils.GetUriString(base.Feed.BaseUri));
			}
			SyndicationFeedFormatter.WriteAttributeExtensions(writer, base.Feed, this.Version);
			string value = (base.Feed.Title != null) ? base.Feed.Title.Text : string.Empty;
			writer.WriteElementString("title", "", value);
			SyndicationLink syndicationLink = null;
			for (int i = 0; i < base.Feed.Links.Count; i++)
			{
				if (base.Feed.Links[i].RelationshipType == "alternate")
				{
					syndicationLink = base.Feed.Links[i];
					this.WriteAlternateLink(writer, syndicationLink, base.Feed.BaseUri);
					break;
				}
			}
			string value2 = (base.Feed.Description != null) ? base.Feed.Description.Text : string.Empty;
			writer.WriteElementString("description", "", value2);
			if (base.Feed.Language != null)
			{
				writer.WriteElementString("language", base.Feed.Language);
			}
			if (base.Feed.Copyright != null)
			{
				writer.WriteElementString("copyright", "", base.Feed.Copyright.Text);
			}
			if (base.Feed.Authors.Count == 1 && base.Feed.Authors[0].Email != null)
			{
				this.WritePerson(writer, "managingEditor", base.Feed.Authors[0]);
			}
			else if (this.serializeExtensionsAsAtom)
			{
				this.atomSerializer.WriteFeedAuthorsTo(writer, base.Feed.Authors);
			}
			else
			{
				Rss20FeedFormatter.TraceExtensionsIgnoredOnWrite("FeedAuthorsIgnoredOnWrite");
			}
			if (base.Feed.LastUpdatedTime > DateTimeOffset.MinValue)
			{
				writer.WriteStartElement("lastBuildDate");
				writer.WriteString(this.AsString(base.Feed.LastUpdatedTime));
				writer.WriteEndElement();
			}
			for (int j = 0; j < base.Feed.Categories.Count; j++)
			{
				this.WriteCategory(writer, base.Feed.Categories[j]);
			}
			if (!string.IsNullOrEmpty(base.Feed.Generator))
			{
				writer.WriteElementString("generator", base.Feed.Generator);
			}
			if (base.Feed.Contributors.Count > 0)
			{
				if (this.serializeExtensionsAsAtom)
				{
					this.atomSerializer.WriteFeedContributorsTo(writer, base.Feed.Contributors);
				}
				else
				{
					Rss20FeedFormatter.TraceExtensionsIgnoredOnWrite("FeedContributorsIgnoredOnWrite");
				}
			}
			if (base.Feed.ImageUrl != null)
			{
				writer.WriteStartElement("image");
				writer.WriteElementString("url", FeedUtils.GetUriString(base.Feed.ImageUrl));
				writer.WriteElementString("title", "", value);
				string value3 = (syndicationLink != null) ? FeedUtils.GetUriString(syndicationLink.Uri) : string.Empty;
				writer.WriteElementString("link", "", value3);
				writer.WriteEndElement();
			}
			if (this.serializeExtensionsAsAtom)
			{
				this.atomSerializer.WriteElement(writer, "id", base.Feed.Id);
				bool flag = true;
				for (int k = 0; k < base.Feed.Links.Count; k++)
				{
					if (base.Feed.Links[k].RelationshipType == "alternate" && flag)
					{
						flag = false;
					}
					else
					{
						this.atomSerializer.WriteLink(writer, base.Feed.Links[k], base.Feed.BaseUri);
					}
				}
			}
			else
			{
				if (base.Feed.Id != null)
				{
					Rss20FeedFormatter.TraceExtensionsIgnoredOnWrite("FeedIdIgnoredOnWrite");
				}
				if (base.Feed.Links.Count > 1)
				{
					Rss20FeedFormatter.TraceExtensionsIgnoredOnWrite("FeedLinksIgnoredOnWrite");
				}
			}
			SyndicationFeedFormatter.WriteElementExtensions(writer, base.Feed, this.Version);
			this.WriteItems(writer, base.Feed.Items, base.Feed.BaseUri);
			writer.WriteEndElement();
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x0002FBE0 File Offset: 0x0002DDE0
		private void WriteItemContents(XmlWriter writer, SyndicationItem item, Uri feedBaseUri)
		{
			Uri baseUriToWrite = FeedUtils.GetBaseUriToWrite(feedBaseUri, item.BaseUri);
			if (baseUriToWrite != null)
			{
				writer.WriteAttributeString("xml", "base", "http://www.w3.org/XML/1998/namespace", FeedUtils.GetUriString(baseUriToWrite));
			}
			SyndicationFeedFormatter.WriteAttributeExtensions(writer, item, this.Version);
			string text = item.Id ?? string.Empty;
			bool flag = false;
			SyndicationLink syndicationLink = null;
			for (int i = 0; i < item.Links.Count; i++)
			{
				if (item.Links[i].RelationshipType == "alternate")
				{
					if (syndicationLink == null)
					{
						syndicationLink = item.Links[i];
					}
					if (text == FeedUtils.GetUriString(item.Links[i].Uri))
					{
						flag = true;
						break;
					}
				}
			}
			if (!string.IsNullOrEmpty(text))
			{
				writer.WriteStartElement("guid");
				if (flag)
				{
					writer.WriteAttributeString("isPermaLink", "true");
				}
				else
				{
					writer.WriteAttributeString("isPermaLink", "false");
				}
				writer.WriteString(text);
				writer.WriteEndElement();
			}
			if (syndicationLink != null)
			{
				this.WriteAlternateLink(writer, syndicationLink, (item.BaseUri != null) ? item.BaseUri : feedBaseUri);
			}
			if (item.Authors.Count == 1 && !string.IsNullOrEmpty(item.Authors[0].Email))
			{
				this.WritePerson(writer, "author", item.Authors[0]);
			}
			else if (this.serializeExtensionsAsAtom)
			{
				this.atomSerializer.WriteItemAuthorsTo(writer, item.Authors);
			}
			else
			{
				Rss20FeedFormatter.TraceExtensionsIgnoredOnWrite("ItemAuthorsIgnoredOnWrite");
			}
			for (int j = 0; j < item.Categories.Count; j++)
			{
				this.WriteCategory(writer, item.Categories[j]);
			}
			bool flag2 = false;
			if (item.Title != null)
			{
				writer.WriteElementString("title", item.Title.Text);
				flag2 = true;
			}
			bool flag3 = false;
			TextSyndicationContent textSyndicationContent = item.Summary;
			if (textSyndicationContent == null)
			{
				textSyndicationContent = (item.Content as TextSyndicationContent);
				flag3 = (textSyndicationContent != null);
			}
			if (!flag2 && textSyndicationContent == null)
			{
				textSyndicationContent = new TextSyndicationContent(string.Empty);
			}
			if (textSyndicationContent != null)
			{
				writer.WriteElementString("description", "", textSyndicationContent.Text);
			}
			if (item.SourceFeed != null)
			{
				writer.WriteStartElement("source", "");
				SyndicationFeedFormatter.WriteAttributeExtensions(writer, item.SourceFeed, this.Version);
				SyndicationLink syndicationLink2 = null;
				for (int k = 0; k < item.SourceFeed.Links.Count; k++)
				{
					if (item.SourceFeed.Links[k].RelationshipType == "self")
					{
						syndicationLink2 = item.SourceFeed.Links[k];
						break;
					}
				}
				if (syndicationLink2 != null && !item.SourceFeed.AttributeExtensions.ContainsKey(Rss20FeedFormatter.Rss20Url))
				{
					writer.WriteAttributeString("url", "", FeedUtils.GetUriString(syndicationLink2.Uri));
				}
				string text2 = (item.SourceFeed.Title != null) ? item.SourceFeed.Title.Text : string.Empty;
				writer.WriteString(text2);
				writer.WriteEndElement();
			}
			if (item.PublishDate > DateTimeOffset.MinValue)
			{
				writer.WriteElementString("pubDate", "", this.AsString(item.PublishDate));
			}
			SyndicationLink syndicationLink3 = null;
			bool flag4 = false;
			bool flag5 = false;
			int l = 0;
			while (l < item.Links.Count)
			{
				if (item.Links[l].RelationshipType == "enclosure")
				{
					if (syndicationLink3 != null)
					{
						goto IL_3D5;
					}
					syndicationLink3 = item.Links[l];
					this.WriteMediaEnclosure(writer, item.Links[l], item.BaseUri);
				}
				else
				{
					if (!(item.Links[l].RelationshipType == "alternate") || flag4)
					{
						goto IL_3D5;
					}
					flag4 = true;
				}
				IL_401:
				l++;
				continue;
				IL_3D5:
				if (this.serializeExtensionsAsAtom)
				{
					this.atomSerializer.WriteLink(writer, item.Links[l], item.BaseUri);
					goto IL_401;
				}
				flag5 = true;
				goto IL_401;
			}
			if (flag5)
			{
				Rss20FeedFormatter.TraceExtensionsIgnoredOnWrite("ItemLinksIgnoredOnWrite");
			}
			if (item.LastUpdatedTime > DateTimeOffset.MinValue)
			{
				if (this.serializeExtensionsAsAtom)
				{
					this.atomSerializer.WriteItemLastUpdatedTimeTo(writer, item.LastUpdatedTime);
				}
				else
				{
					Rss20FeedFormatter.TraceExtensionsIgnoredOnWrite("ItemLastUpdatedTimeIgnoredOnWrite");
				}
			}
			if (this.serializeExtensionsAsAtom)
			{
				this.atomSerializer.WriteContentTo(writer, "rights", item.Copyright);
			}
			else
			{
				Rss20FeedFormatter.TraceExtensionsIgnoredOnWrite("ItemCopyrightIgnoredOnWrite");
			}
			if (!flag3)
			{
				if (this.serializeExtensionsAsAtom)
				{
					this.atomSerializer.WriteContentTo(writer, "content", item.Content);
				}
				else
				{
					Rss20FeedFormatter.TraceExtensionsIgnoredOnWrite("ItemContentIgnoredOnWrite");
				}
			}
			if (item.Contributors.Count > 0)
			{
				if (this.serializeExtensionsAsAtom)
				{
					this.atomSerializer.WriteItemContributorsTo(writer, item.Contributors);
				}
				else
				{
					Rss20FeedFormatter.TraceExtensionsIgnoredOnWrite("ItemContributorsIgnoredOnWrite");
				}
			}
			SyndicationFeedFormatter.WriteElementExtensions(writer, item, this.Version);
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x000300E8 File Offset: 0x0002E2E8
		private void WriteMediaEnclosure(XmlWriter writer, SyndicationLink link, Uri baseUri)
		{
			writer.WriteStartElement("enclosure", "");
			Uri baseUriToWrite = FeedUtils.GetBaseUriToWrite(baseUri, link.BaseUri);
			if (baseUriToWrite != null)
			{
				writer.WriteAttributeString("xml", "base", "http://www.w3.org/XML/1998/namespace", FeedUtils.GetUriString(baseUriToWrite));
			}
			link.WriteAttributeExtensions(writer, "Rss20");
			if (!link.AttributeExtensions.ContainsKey(Rss20FeedFormatter.Rss20Url))
			{
				writer.WriteAttributeString("url", "", FeedUtils.GetUriString(link.Uri));
			}
			if (link.MediaType != null && !link.AttributeExtensions.ContainsKey(Rss20FeedFormatter.Rss20Type))
			{
				writer.WriteAttributeString("type", "", link.MediaType);
			}
			if (link.Length != 0L && !link.AttributeExtensions.ContainsKey(Rss20FeedFormatter.Rss20Length))
			{
				writer.WriteAttributeString("length", "", Convert.ToString(link.Length, CultureInfo.InvariantCulture));
			}
			writer.WriteEndElement();
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x000301DF File Offset: 0x0002E3DF
		private void WritePerson(XmlWriter writer, string elementTag, SyndicationPerson person)
		{
			writer.WriteStartElement(elementTag, "");
			SyndicationFeedFormatter.WriteAttributeExtensions(writer, person, this.Version);
			writer.WriteString(person.Email);
			writer.WriteEndElement();
		}

		// Token: 0x040016E8 RID: 5864
		private static readonly XmlQualifiedName Rss20Domain = new XmlQualifiedName("domain", string.Empty);

		// Token: 0x040016E9 RID: 5865
		private static readonly XmlQualifiedName Rss20Length = new XmlQualifiedName("length", string.Empty);

		// Token: 0x040016EA RID: 5866
		private static readonly XmlQualifiedName Rss20Type = new XmlQualifiedName("type", string.Empty);

		// Token: 0x040016EB RID: 5867
		private static readonly XmlQualifiedName Rss20Url = new XmlQualifiedName("url", string.Empty);

		// Token: 0x040016EC RID: 5868
		private const string Rfc822OutputLocalDateTimeFormat = "ddd, dd MMM yyyy HH:mm:ss zzz";

		// Token: 0x040016ED RID: 5869
		private const string Rfc822OutputUtcDateTimeFormat = "ddd, dd MMM yyyy HH:mm:ss Z";

		// Token: 0x040016EE RID: 5870
		private Atom10FeedFormatter atomSerializer;

		// Token: 0x040016EF RID: 5871
		private Type feedType;

		// Token: 0x040016F0 RID: 5872
		private int maxExtensionSize;

		// Token: 0x040016F1 RID: 5873
		private bool preserveAttributeExtensions;

		// Token: 0x040016F2 RID: 5874
		private bool preserveElementExtensions;

		// Token: 0x040016F3 RID: 5875
		private bool serializeExtensionsAsAtom;
	}
}
