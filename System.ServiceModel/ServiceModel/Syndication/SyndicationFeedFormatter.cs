using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.ServiceModel.Diagnostics;
using System.Xml;

namespace System.ServiceModel.Syndication
{
	// Token: 0x0200018A RID: 394
	[TypeForwardedFrom("System.ServiceModel.Web, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	[DataContract]
	public abstract class SyndicationFeedFormatter
	{
		// Token: 0x06000BFF RID: 3071 RVA: 0x0002C1D5 File Offset: 0x0002A3D5
		protected SyndicationFeedFormatter()
		{
			this.feed = null;
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x0002C1E4 File Offset: 0x0002A3E4
		protected SyndicationFeedFormatter(SyndicationFeed feedToWrite)
		{
			if (feedToWrite == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("feedToWrite");
			}
			this.feed = feedToWrite;
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000C01 RID: 3073 RVA: 0x0002C206 File Offset: 0x0002A406
		public SyndicationFeed Feed
		{
			get
			{
				return this.feed;
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000C02 RID: 3074
		public abstract string Version { get; }

		// Token: 0x06000C03 RID: 3075
		public abstract bool CanRead(XmlReader reader);

		// Token: 0x06000C04 RID: 3076
		public abstract void ReadFrom(XmlReader reader);

		// Token: 0x06000C05 RID: 3077 RVA: 0x0002C20E File Offset: 0x0002A40E
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "{0}, SyndicationVersion={1}", new object[]
			{
				base.GetType(),
				this.Version
			});
		}

		// Token: 0x06000C06 RID: 3078
		public abstract void WriteTo(XmlWriter writer);

		// Token: 0x06000C07 RID: 3079 RVA: 0x0002C237 File Offset: 0x0002A437
		protected internal static SyndicationCategory CreateCategory(SyndicationFeed feed)
		{
			if (feed == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("feed");
			}
			return SyndicationFeedFormatter.GetNonNullValue<SyndicationCategory>(feed.CreateCategory(), "FeedCreatedNullCategory");
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x0002C25C File Offset: 0x0002A45C
		protected internal static SyndicationCategory CreateCategory(SyndicationItem item)
		{
			if (item == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("item");
			}
			return SyndicationFeedFormatter.GetNonNullValue<SyndicationCategory>(item.CreateCategory(), "ItemCreatedNullCategory");
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x0002C281 File Offset: 0x0002A481
		protected internal static SyndicationItem CreateItem(SyndicationFeed feed)
		{
			if (feed == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("feed");
			}
			return SyndicationFeedFormatter.GetNonNullValue<SyndicationItem>(feed.CreateItem(), "FeedCreatedNullItem");
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x0002C2A6 File Offset: 0x0002A4A6
		protected internal static SyndicationLink CreateLink(SyndicationFeed feed)
		{
			if (feed == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("feed");
			}
			return SyndicationFeedFormatter.GetNonNullValue<SyndicationLink>(feed.CreateLink(), "FeedCreatedNullPerson");
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x0002C2CB File Offset: 0x0002A4CB
		protected internal static SyndicationLink CreateLink(SyndicationItem item)
		{
			if (item == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("item");
			}
			return SyndicationFeedFormatter.GetNonNullValue<SyndicationLink>(item.CreateLink(), "ItemCreatedNullPerson");
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x0002C2F0 File Offset: 0x0002A4F0
		protected internal static SyndicationPerson CreatePerson(SyndicationFeed feed)
		{
			if (feed == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("feed");
			}
			return SyndicationFeedFormatter.GetNonNullValue<SyndicationPerson>(feed.CreatePerson(), "FeedCreatedNullPerson");
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x0002C315 File Offset: 0x0002A515
		protected internal static SyndicationPerson CreatePerson(SyndicationItem item)
		{
			if (item == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("item");
			}
			return SyndicationFeedFormatter.GetNonNullValue<SyndicationPerson>(item.CreatePerson(), "ItemCreatedNullPerson");
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x0002C33A File Offset: 0x0002A53A
		protected internal static void LoadElementExtensions(XmlReader reader, SyndicationFeed feed, int maxExtensionSize)
		{
			if (feed == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("feed");
			}
			feed.LoadElementExtensions(reader, maxExtensionSize);
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x0002C357 File Offset: 0x0002A557
		protected internal static void LoadElementExtensions(XmlReader reader, SyndicationItem item, int maxExtensionSize)
		{
			if (item == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("item");
			}
			item.LoadElementExtensions(reader, maxExtensionSize);
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x0002C374 File Offset: 0x0002A574
		protected internal static void LoadElementExtensions(XmlReader reader, SyndicationCategory category, int maxExtensionSize)
		{
			if (category == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("category");
			}
			category.LoadElementExtensions(reader, maxExtensionSize);
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x0002C391 File Offset: 0x0002A591
		protected internal static void LoadElementExtensions(XmlReader reader, SyndicationLink link, int maxExtensionSize)
		{
			if (link == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("link");
			}
			link.LoadElementExtensions(reader, maxExtensionSize);
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x0002C3AE File Offset: 0x0002A5AE
		protected internal static void LoadElementExtensions(XmlReader reader, SyndicationPerson person, int maxExtensionSize)
		{
			if (person == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("person");
			}
			person.LoadElementExtensions(reader, maxExtensionSize);
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x0002C3CB File Offset: 0x0002A5CB
		protected internal static bool TryParseAttribute(string name, string ns, string value, SyndicationFeed feed, string version)
		{
			if (feed == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("feed");
			}
			return FeedUtils.IsXmlns(name, ns) || feed.TryParseAttribute(name, ns, value, version);
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x0002C3F6 File Offset: 0x0002A5F6
		protected internal static bool TryParseAttribute(string name, string ns, string value, SyndicationItem item, string version)
		{
			if (item == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("item");
			}
			return FeedUtils.IsXmlns(name, ns) || item.TryParseAttribute(name, ns, value, version);
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x0002C421 File Offset: 0x0002A621
		protected internal static bool TryParseAttribute(string name, string ns, string value, SyndicationCategory category, string version)
		{
			if (category == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("category");
			}
			return FeedUtils.IsXmlns(name, ns) || category.TryParseAttribute(name, ns, value, version);
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x0002C44C File Offset: 0x0002A64C
		protected internal static bool TryParseAttribute(string name, string ns, string value, SyndicationLink link, string version)
		{
			if (link == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("link");
			}
			return FeedUtils.IsXmlns(name, ns) || link.TryParseAttribute(name, ns, value, version);
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x0002C477 File Offset: 0x0002A677
		protected internal static bool TryParseAttribute(string name, string ns, string value, SyndicationPerson person, string version)
		{
			if (person == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("person");
			}
			return FeedUtils.IsXmlns(name, ns) || person.TryParseAttribute(name, ns, value, version);
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x0002C4A2 File Offset: 0x0002A6A2
		protected internal static bool TryParseContent(XmlReader reader, SyndicationItem item, string contentType, string version, out SyndicationContent content)
		{
			return item.TryParseContent(reader, contentType, version, out content);
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x0002C4AF File Offset: 0x0002A6AF
		protected internal static bool TryParseElement(XmlReader reader, SyndicationFeed feed, string version)
		{
			if (feed == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("feed");
			}
			return feed.TryParseElement(reader, version);
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x0002C4CC File Offset: 0x0002A6CC
		protected internal static bool TryParseElement(XmlReader reader, SyndicationItem item, string version)
		{
			if (item == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("item");
			}
			return item.TryParseElement(reader, version);
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x0002C4E9 File Offset: 0x0002A6E9
		protected internal static bool TryParseElement(XmlReader reader, SyndicationCategory category, string version)
		{
			if (category == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("category");
			}
			return category.TryParseElement(reader, version);
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x0002C506 File Offset: 0x0002A706
		protected internal static bool TryParseElement(XmlReader reader, SyndicationLink link, string version)
		{
			if (link == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("link");
			}
			return link.TryParseElement(reader, version);
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x0002C523 File Offset: 0x0002A723
		protected internal static bool TryParseElement(XmlReader reader, SyndicationPerson person, string version)
		{
			if (person == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("person");
			}
			return person.TryParseElement(reader, version);
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x0002C540 File Offset: 0x0002A740
		protected internal static void WriteAttributeExtensions(XmlWriter writer, SyndicationFeed feed, string version)
		{
			if (feed == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("feed");
			}
			feed.WriteAttributeExtensions(writer, version);
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x0002C55D File Offset: 0x0002A75D
		protected internal static void WriteAttributeExtensions(XmlWriter writer, SyndicationItem item, string version)
		{
			if (item == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("item");
			}
			item.WriteAttributeExtensions(writer, version);
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x0002C57A File Offset: 0x0002A77A
		protected internal static void WriteAttributeExtensions(XmlWriter writer, SyndicationCategory category, string version)
		{
			if (category == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("category");
			}
			category.WriteAttributeExtensions(writer, version);
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x0002C597 File Offset: 0x0002A797
		protected internal static void WriteAttributeExtensions(XmlWriter writer, SyndicationLink link, string version)
		{
			if (link == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("link");
			}
			link.WriteAttributeExtensions(writer, version);
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x0002C5B4 File Offset: 0x0002A7B4
		protected internal static void WriteAttributeExtensions(XmlWriter writer, SyndicationPerson person, string version)
		{
			if (person == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("person");
			}
			person.WriteAttributeExtensions(writer, version);
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x0002C5D1 File Offset: 0x0002A7D1
		protected internal static void WriteElementExtensions(XmlWriter writer, SyndicationFeed feed, string version)
		{
			if (feed == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("feed");
			}
			feed.WriteElementExtensions(writer, version);
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x0002C5EE File Offset: 0x0002A7EE
		protected internal static void WriteElementExtensions(XmlWriter writer, SyndicationItem item, string version)
		{
			if (item == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("item");
			}
			item.WriteElementExtensions(writer, version);
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x0002C60B File Offset: 0x0002A80B
		protected internal static void WriteElementExtensions(XmlWriter writer, SyndicationCategory category, string version)
		{
			if (category == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("category");
			}
			category.WriteElementExtensions(writer, version);
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x0002C628 File Offset: 0x0002A828
		protected internal static void WriteElementExtensions(XmlWriter writer, SyndicationLink link, string version)
		{
			if (link == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("link");
			}
			link.WriteElementExtensions(writer, version);
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x0002C645 File Offset: 0x0002A845
		protected internal static void WriteElementExtensions(XmlWriter writer, SyndicationPerson person, string version)
		{
			if (person == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("person");
			}
			person.WriteElementExtensions(writer, version);
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x0002C662 File Offset: 0x0002A862
		protected internal virtual void SetFeed(SyndicationFeed feed)
		{
			if (feed == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("feed");
			}
			this.feed = feed;
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x0002C67E File Offset: 0x0002A87E
		internal static void CloseBuffer(XmlBuffer buffer, XmlDictionaryWriter extWriter)
		{
			if (buffer == null)
			{
				return;
			}
			extWriter.WriteEndElement();
			buffer.CloseSection();
			buffer.Close();
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x0002C696 File Offset: 0x0002A896
		internal static void CreateBufferIfRequiredAndWriteNode(ref XmlBuffer buffer, ref XmlDictionaryWriter extWriter, XmlReader reader, int maxExtensionSize)
		{
			if (buffer == null)
			{
				buffer = new XmlBuffer(maxExtensionSize);
				extWriter = buffer.OpenSection(XmlDictionaryReaderQuotas.Max);
				extWriter.WriteStartElement("extensionWrapper");
			}
			extWriter.WriteNode(reader, false);
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x0002C6C7 File Offset: 0x0002A8C7
		internal static SyndicationFeed CreateFeedInstance(Type feedType)
		{
			if (feedType.Equals(typeof(SyndicationFeed)))
			{
				return new SyndicationFeed();
			}
			return (SyndicationFeed)Activator.CreateInstance(feedType);
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x0002C6EC File Offset: 0x0002A8EC
		internal static void LoadElementExtensions(XmlBuffer buffer, XmlDictionaryWriter writer, SyndicationFeed feed)
		{
			if (feed == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("feed");
			}
			SyndicationFeedFormatter.CloseBuffer(buffer, writer);
			feed.LoadElementExtensions(buffer);
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x0002C70F File Offset: 0x0002A90F
		internal static void LoadElementExtensions(XmlBuffer buffer, XmlDictionaryWriter writer, SyndicationItem item)
		{
			if (item == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("item");
			}
			SyndicationFeedFormatter.CloseBuffer(buffer, writer);
			item.LoadElementExtensions(buffer);
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x0002C732 File Offset: 0x0002A932
		internal static void LoadElementExtensions(XmlBuffer buffer, XmlDictionaryWriter writer, SyndicationCategory category)
		{
			if (category == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("category");
			}
			SyndicationFeedFormatter.CloseBuffer(buffer, writer);
			category.LoadElementExtensions(buffer);
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x0002C755 File Offset: 0x0002A955
		internal static void LoadElementExtensions(XmlBuffer buffer, XmlDictionaryWriter writer, SyndicationLink link)
		{
			if (link == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("link");
			}
			SyndicationFeedFormatter.CloseBuffer(buffer, writer);
			link.LoadElementExtensions(buffer);
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x0002C778 File Offset: 0x0002A978
		internal static void LoadElementExtensions(XmlBuffer buffer, XmlDictionaryWriter writer, SyndicationPerson person)
		{
			if (person == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("person");
			}
			SyndicationFeedFormatter.CloseBuffer(buffer, writer);
			person.LoadElementExtensions(buffer);
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x0002C79B File Offset: 0x0002A99B
		internal static void MoveToStartElement(XmlReader reader)
		{
			if (!reader.IsStartElement())
			{
				SyndicationFeedFormatter.XmlExceptionHelper.ThrowStartElementExpected(XmlDictionaryReader.CreateDictionaryReader(reader));
			}
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x0002C7B0 File Offset: 0x0002A9B0
		internal static void TraceFeedReadBegin()
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 983065, SR.GetString("TraceCodeSyndicationFeedReadBegin"));
			}
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x0002C7CE File Offset: 0x0002A9CE
		internal static void TraceFeedReadEnd()
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 983066, SR.GetString("TraceCodeSyndicationFeedReadEnd"));
			}
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x0002C7EC File Offset: 0x0002A9EC
		internal static void TraceFeedWriteBegin()
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 983069, SR.GetString("TraceCodeSyndicationFeedWriteBegin"));
			}
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x0002C80A File Offset: 0x0002AA0A
		internal static void TraceFeedWriteEnd()
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 983070, SR.GetString("TraceCodeSyndicationFeedWriteEnd"));
			}
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x0002C828 File Offset: 0x0002AA28
		internal static void TraceItemReadBegin()
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 983067, SR.GetString("TraceCodeSyndicationItemReadBegin"));
			}
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x0002C846 File Offset: 0x0002AA46
		internal static void TraceItemReadEnd()
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 983068, SR.GetString("TraceCodeSyndicationItemReadEnd"));
			}
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x0002C864 File Offset: 0x0002AA64
		internal static void TraceItemWriteBegin()
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 983071, SR.GetString("TraceCodeSyndicationItemWriteBegin"));
			}
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x0002C882 File Offset: 0x0002AA82
		internal static void TraceItemWriteEnd()
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 983072, SR.GetString("TraceCodeSyndicationItemWriteEnd"));
			}
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x0002C8A0 File Offset: 0x0002AAA0
		internal static void TraceSyndicationElementIgnoredOnRead(XmlReader reader)
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 983073, SR.GetString("TraceCodeSyndicationProtocolElementIgnoredOnRead", new object[]
				{
					reader.NodeType,
					reader.LocalName,
					reader.NamespaceURI
				}));
			}
		}

		// Token: 0x06000C3B RID: 3131
		protected abstract SyndicationFeed CreateFeedInstance();

		// Token: 0x06000C3C RID: 3132 RVA: 0x0002C8EF File Offset: 0x0002AAEF
		private static T GetNonNullValue<T>(T value, string errorMsg)
		{
			if (value == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString(errorMsg)));
			}
			return value;
		}

		// Token: 0x040016A7 RID: 5799
		private SyndicationFeed feed;

		// Token: 0x02000AF5 RID: 2805
		private static class XmlExceptionHelper
		{
			// Token: 0x06006F27 RID: 28455 RVA: 0x0019D330 File Offset: 0x0019B530
			private static void ThrowXmlException(XmlDictionaryReader reader, string res, string arg1)
			{
				string text = SR.GetString(res, new object[]
				{
					arg1
				});
				IXmlLineInfo xmlLineInfo = reader as IXmlLineInfo;
				if (xmlLineInfo != null && xmlLineInfo.HasLineInfo())
				{
					text = text + " " + SR.GetString("XmlLineInfo", new object[]
					{
						xmlLineInfo.LineNumber,
						xmlLineInfo.LinePosition
					});
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(text));
			}

			// Token: 0x06006F28 RID: 28456 RVA: 0x0019D3A8 File Offset: 0x0019B5A8
			private static string GetName(string prefix, string localName)
			{
				if (prefix.Length == 0)
				{
					return localName;
				}
				return prefix + ":" + localName;
			}

			// Token: 0x06006F29 RID: 28457 RVA: 0x0019D3C0 File Offset: 0x0019B5C0
			private static string GetWhatWasFound(XmlDictionaryReader reader)
			{
				if (reader.EOF)
				{
					return SR.GetString("XmlFoundEndOfFile");
				}
				XmlNodeType nodeType = reader.NodeType;
				if (nodeType <= XmlNodeType.Comment)
				{
					switch (nodeType)
					{
					case XmlNodeType.Element:
						return SR.GetString("XmlFoundElement", new object[]
						{
							SyndicationFeedFormatter.XmlExceptionHelper.GetName(reader.Prefix, reader.LocalName),
							reader.NamespaceURI
						});
					case XmlNodeType.Attribute:
						goto IL_FD;
					case XmlNodeType.Text:
						break;
					case XmlNodeType.CDATA:
						return SR.GetString("XmlFoundCData", new object[]
						{
							reader.Value
						});
					default:
						if (nodeType != XmlNodeType.Comment)
						{
							goto IL_FD;
						}
						return SR.GetString("XmlFoundComment", new object[]
						{
							reader.Value
						});
					}
				}
				else if (nodeType - XmlNodeType.Whitespace > 1)
				{
					if (nodeType != XmlNodeType.EndElement)
					{
						goto IL_FD;
					}
					return SR.GetString("XmlFoundEndElement", new object[]
					{
						SyndicationFeedFormatter.XmlExceptionHelper.GetName(reader.Prefix, reader.LocalName),
						reader.NamespaceURI
					});
				}
				return SR.GetString("XmlFoundText", new object[]
				{
					reader.Value
				});
				IL_FD:
				return SR.GetString("XmlFoundNodeType", new object[]
				{
					reader.NodeType
				});
			}

			// Token: 0x06006F2A RID: 28458 RVA: 0x0019D4E8 File Offset: 0x0019B6E8
			public static void ThrowStartElementExpected(XmlDictionaryReader reader)
			{
				SyndicationFeedFormatter.XmlExceptionHelper.ThrowXmlException(reader, "XmlStartElementExpected", SyndicationFeedFormatter.XmlExceptionHelper.GetWhatWasFound(reader));
			}
		}
	}
}
