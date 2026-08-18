using System;
using System.Globalization;
using System.ServiceModel.Channels;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x0200029E RID: 670
	internal sealed class EncryptedHeader : DelegatingHeader
	{
		// Token: 0x0600144A RID: 5194 RVA: 0x0004C3BC File Offset: 0x0004A5BC
		public EncryptedHeader(MessageHeader plainTextHeader, EncryptedHeaderXml headerXml, string name, string namespaceUri, MessageVersion version) : base(plainTextHeader)
		{
			if (!headerXml.HasId || headerXml.Id == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("EncryptedHeaderXmlMustHaveId")));
			}
			this.headerXml = headerXml;
			this.name = name;
			this.namespaceUri = namespaceUri;
			this.version = version;
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x0600144B RID: 5195 RVA: 0x0004C418 File Offset: 0x0004A618
		public string Id
		{
			get
			{
				return this.headerXml.Id;
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x0600144C RID: 5196 RVA: 0x0004C425 File Offset: 0x0004A625
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x0600144D RID: 5197 RVA: 0x0004C42D File Offset: 0x0004A62D
		public override string Namespace
		{
			get
			{
				return this.namespaceUri;
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x0600144E RID: 5198 RVA: 0x0004C435 File Offset: 0x0004A635
		public override string Actor
		{
			get
			{
				return this.headerXml.Actor;
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x0600144F RID: 5199 RVA: 0x0004C442 File Offset: 0x0004A642
		public override bool MustUnderstand
		{
			get
			{
				return this.headerXml.MustUnderstand;
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06001450 RID: 5200 RVA: 0x0004C44F File Offset: 0x0004A64F
		public override bool Relay
		{
			get
			{
				return this.headerXml.Relay;
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06001451 RID: 5201 RVA: 0x0004C45C File Offset: 0x0004A65C
		internal MessageHeader OriginalHeader
		{
			get
			{
				return base.InnerHeader;
			}
		}

		// Token: 0x06001452 RID: 5202 RVA: 0x0004C464 File Offset: 0x0004A664
		public override bool IsMessageVersionSupported(MessageVersion messageVersion)
		{
			return this.version.Equals(messageVersion);
		}

		// Token: 0x06001453 RID: 5203 RVA: 0x0004C474 File Offset: 0x0004A674
		protected override void OnWriteStartHeader(XmlDictionaryWriter writer, MessageVersion messageVersion)
		{
			if (!this.IsMessageVersionSupported(messageVersion))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("MessageHeaderVersionNotSupported", new object[]
				{
					string.Format(CultureInfo.InvariantCulture, "{0}:{1}", new object[]
					{
						this.Namespace,
						this.Name
					}),
					this.version.ToString()
				}), "version"));
			}
			this.headerXml.WriteHeaderElement(writer);
			base.WriteHeaderAttributes(writer, messageVersion);
			this.headerXml.WriteHeaderId(writer);
		}

		// Token: 0x06001454 RID: 5204 RVA: 0x0004C507 File Offset: 0x0004A707
		protected override void OnWriteHeaderContents(XmlDictionaryWriter writer, MessageVersion messageVersion)
		{
			this.headerXml.WriteHeaderContents(writer);
		}

		// Token: 0x04001AB1 RID: 6833
		private EncryptedHeaderXml headerXml;

		// Token: 0x04001AB2 RID: 6834
		private string name;

		// Token: 0x04001AB3 RID: 6835
		private string namespaceUri;

		// Token: 0x04001AB4 RID: 6836
		private MessageVersion version;
	}
}
