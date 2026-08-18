using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml;

namespace System.ServiceModel.Syndication
{
	// Token: 0x0200018B RID: 395
	[TypeForwardedFrom("System.ServiceModel.Web, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	[DataContract]
	public abstract class SyndicationItemFormatter
	{
		// Token: 0x06000C3D RID: 3133 RVA: 0x0002C910 File Offset: 0x0002AB10
		protected SyndicationItemFormatter()
		{
			this.item = null;
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x0002C91F File Offset: 0x0002AB1F
		protected SyndicationItemFormatter(SyndicationItem itemToWrite)
		{
			if (itemToWrite == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("itemToWrite");
			}
			this.item = itemToWrite;
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000C3F RID: 3135 RVA: 0x0002C941 File Offset: 0x0002AB41
		public SyndicationItem Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000C40 RID: 3136
		public abstract string Version { get; }

		// Token: 0x06000C41 RID: 3137
		public abstract bool CanRead(XmlReader reader);

		// Token: 0x06000C42 RID: 3138
		public abstract void ReadFrom(XmlReader reader);

		// Token: 0x06000C43 RID: 3139 RVA: 0x0002C949 File Offset: 0x0002AB49
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "{0}, SyndicationVersion={1}", new object[]
			{
				base.GetType(),
				this.Version
			});
		}

		// Token: 0x06000C44 RID: 3140
		public abstract void WriteTo(XmlWriter writer);

		// Token: 0x06000C45 RID: 3141 RVA: 0x0002C972 File Offset: 0x0002AB72
		protected internal virtual void SetItem(SyndicationItem item)
		{
			if (item == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("item");
			}
			this.item = item;
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x0002C98E File Offset: 0x0002AB8E
		internal static void CreateBufferIfRequiredAndWriteNode(ref XmlBuffer buffer, ref XmlDictionaryWriter extWriter, XmlDictionaryReader reader, int maxExtensionSize)
		{
			SyndicationFeedFormatter.CreateBufferIfRequiredAndWriteNode(ref buffer, ref extWriter, reader, maxExtensionSize);
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x0002C999 File Offset: 0x0002AB99
		internal static SyndicationItem CreateItemInstance(Type itemType)
		{
			if (itemType.Equals(typeof(SyndicationItem)))
			{
				return new SyndicationItem();
			}
			return (SyndicationItem)Activator.CreateInstance(itemType);
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x0002C9BE File Offset: 0x0002ABBE
		internal static void LoadElementExtensions(XmlBuffer buffer, XmlDictionaryWriter writer, SyndicationItem item)
		{
			SyndicationFeedFormatter.LoadElementExtensions(buffer, writer, item);
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x0002C9C8 File Offset: 0x0002ABC8
		internal static void LoadElementExtensions(XmlBuffer buffer, XmlDictionaryWriter writer, SyndicationCategory category)
		{
			SyndicationFeedFormatter.LoadElementExtensions(buffer, writer, category);
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x0002C9D2 File Offset: 0x0002ABD2
		internal static void LoadElementExtensions(XmlBuffer buffer, XmlDictionaryWriter writer, SyndicationLink link)
		{
			SyndicationFeedFormatter.LoadElementExtensions(buffer, writer, link);
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x0002C9DC File Offset: 0x0002ABDC
		internal static void LoadElementExtensions(XmlBuffer buffer, XmlDictionaryWriter writer, SyndicationPerson person)
		{
			SyndicationFeedFormatter.LoadElementExtensions(buffer, writer, person);
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x0002C9E6 File Offset: 0x0002ABE6
		protected static SyndicationCategory CreateCategory(SyndicationItem item)
		{
			return SyndicationFeedFormatter.CreateCategory(item);
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x0002C9EE File Offset: 0x0002ABEE
		protected static SyndicationLink CreateLink(SyndicationItem item)
		{
			return SyndicationFeedFormatter.CreateLink(item);
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x0002C9F6 File Offset: 0x0002ABF6
		protected static SyndicationPerson CreatePerson(SyndicationItem item)
		{
			return SyndicationFeedFormatter.CreatePerson(item);
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x0002C9FE File Offset: 0x0002ABFE
		protected static void LoadElementExtensions(XmlReader reader, SyndicationItem item, int maxExtensionSize)
		{
			SyndicationFeedFormatter.LoadElementExtensions(reader, item, maxExtensionSize);
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x0002CA08 File Offset: 0x0002AC08
		protected static void LoadElementExtensions(XmlReader reader, SyndicationCategory category, int maxExtensionSize)
		{
			SyndicationFeedFormatter.LoadElementExtensions(reader, category, maxExtensionSize);
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x0002CA12 File Offset: 0x0002AC12
		protected static void LoadElementExtensions(XmlReader reader, SyndicationLink link, int maxExtensionSize)
		{
			SyndicationFeedFormatter.LoadElementExtensions(reader, link, maxExtensionSize);
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x0002CA1C File Offset: 0x0002AC1C
		protected static void LoadElementExtensions(XmlReader reader, SyndicationPerson person, int maxExtensionSize)
		{
			SyndicationFeedFormatter.LoadElementExtensions(reader, person, maxExtensionSize);
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x0002CA26 File Offset: 0x0002AC26
		protected static bool TryParseAttribute(string name, string ns, string value, SyndicationItem item, string version)
		{
			return SyndicationFeedFormatter.TryParseAttribute(name, ns, value, item, version);
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x0002CA33 File Offset: 0x0002AC33
		protected static bool TryParseAttribute(string name, string ns, string value, SyndicationCategory category, string version)
		{
			return SyndicationFeedFormatter.TryParseAttribute(name, ns, value, category, version);
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x0002CA40 File Offset: 0x0002AC40
		protected static bool TryParseAttribute(string name, string ns, string value, SyndicationLink link, string version)
		{
			return SyndicationFeedFormatter.TryParseAttribute(name, ns, value, link, version);
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x0002CA4D File Offset: 0x0002AC4D
		protected static bool TryParseAttribute(string name, string ns, string value, SyndicationPerson person, string version)
		{
			return SyndicationFeedFormatter.TryParseAttribute(name, ns, value, person, version);
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x0002CA5A File Offset: 0x0002AC5A
		protected static bool TryParseContent(XmlReader reader, SyndicationItem item, string contentType, string version, out SyndicationContent content)
		{
			return SyndicationFeedFormatter.TryParseContent(reader, item, contentType, version, out content);
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x0002CA67 File Offset: 0x0002AC67
		protected static bool TryParseElement(XmlReader reader, SyndicationItem item, string version)
		{
			return SyndicationFeedFormatter.TryParseElement(reader, item, version);
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x0002CA71 File Offset: 0x0002AC71
		protected static bool TryParseElement(XmlReader reader, SyndicationCategory category, string version)
		{
			return SyndicationFeedFormatter.TryParseElement(reader, category, version);
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x0002CA7B File Offset: 0x0002AC7B
		protected static bool TryParseElement(XmlReader reader, SyndicationLink link, string version)
		{
			return SyndicationFeedFormatter.TryParseElement(reader, link, version);
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x0002CA85 File Offset: 0x0002AC85
		protected static bool TryParseElement(XmlReader reader, SyndicationPerson person, string version)
		{
			return SyndicationFeedFormatter.TryParseElement(reader, person, version);
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x0002CA8F File Offset: 0x0002AC8F
		protected static void WriteAttributeExtensions(XmlWriter writer, SyndicationItem item, string version)
		{
			SyndicationFeedFormatter.WriteAttributeExtensions(writer, item, version);
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x0002CA99 File Offset: 0x0002AC99
		protected static void WriteAttributeExtensions(XmlWriter writer, SyndicationCategory category, string version)
		{
			SyndicationFeedFormatter.WriteAttributeExtensions(writer, category, version);
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x0002CAA3 File Offset: 0x0002ACA3
		protected static void WriteAttributeExtensions(XmlWriter writer, SyndicationLink link, string version)
		{
			SyndicationFeedFormatter.WriteAttributeExtensions(writer, link, version);
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x0002CAAD File Offset: 0x0002ACAD
		protected static void WriteAttributeExtensions(XmlWriter writer, SyndicationPerson person, string version)
		{
			SyndicationFeedFormatter.WriteAttributeExtensions(writer, person, version);
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x0002CAB7 File Offset: 0x0002ACB7
		protected static void WriteElementExtensions(XmlWriter writer, SyndicationItem item, string version)
		{
			SyndicationFeedFormatter.WriteElementExtensions(writer, item, version);
		}

		// Token: 0x06000C61 RID: 3169
		protected abstract SyndicationItem CreateItemInstance();

		// Token: 0x06000C62 RID: 3170 RVA: 0x0002CAC1 File Offset: 0x0002ACC1
		protected void WriteElementExtensions(XmlWriter writer, SyndicationCategory category, string version)
		{
			SyndicationFeedFormatter.WriteElementExtensions(writer, category, version);
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x0002CACB File Offset: 0x0002ACCB
		protected void WriteElementExtensions(XmlWriter writer, SyndicationLink link, string version)
		{
			SyndicationFeedFormatter.WriteElementExtensions(writer, link, version);
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x0002CAD5 File Offset: 0x0002ACD5
		protected void WriteElementExtensions(XmlWriter writer, SyndicationPerson person, string version)
		{
			SyndicationFeedFormatter.WriteElementExtensions(writer, person, version);
		}

		// Token: 0x040016A8 RID: 5800
		private SyndicationItem item;
	}
}
