using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000989 RID: 2441
	internal abstract class DelegatingMessage : Message
	{
		// Token: 0x06005E7A RID: 24186 RVA: 0x0015D802 File Offset: 0x0015BA02
		protected DelegatingMessage(Message innerMessage)
		{
			if (innerMessage == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("innerMessage");
			}
			this.innerMessage = innerMessage;
		}

		// Token: 0x170016AD RID: 5805
		// (get) Token: 0x06005E7B RID: 24187 RVA: 0x0015D824 File Offset: 0x0015BA24
		public override bool IsEmpty
		{
			get
			{
				return this.innerMessage.IsEmpty;
			}
		}

		// Token: 0x170016AE RID: 5806
		// (get) Token: 0x06005E7C RID: 24188 RVA: 0x0015D831 File Offset: 0x0015BA31
		public override bool IsFault
		{
			get
			{
				return this.innerMessage.IsFault;
			}
		}

		// Token: 0x170016AF RID: 5807
		// (get) Token: 0x06005E7D RID: 24189 RVA: 0x0015D83E File Offset: 0x0015BA3E
		public override MessageHeaders Headers
		{
			get
			{
				return this.innerMessage.Headers;
			}
		}

		// Token: 0x170016B0 RID: 5808
		// (get) Token: 0x06005E7E RID: 24190 RVA: 0x0015D84B File Offset: 0x0015BA4B
		public override MessageProperties Properties
		{
			get
			{
				return this.innerMessage.Properties;
			}
		}

		// Token: 0x170016B1 RID: 5809
		// (get) Token: 0x06005E7F RID: 24191 RVA: 0x0015D858 File Offset: 0x0015BA58
		public override MessageVersion Version
		{
			get
			{
				return this.innerMessage.Version;
			}
		}

		// Token: 0x170016B2 RID: 5810
		// (get) Token: 0x06005E80 RID: 24192 RVA: 0x0015D865 File Offset: 0x0015BA65
		protected Message InnerMessage
		{
			get
			{
				return this.innerMessage;
			}
		}

		// Token: 0x06005E81 RID: 24193 RVA: 0x0015D86D File Offset: 0x0015BA6D
		protected override void OnClose()
		{
			base.OnClose();
			this.innerMessage.Close();
		}

		// Token: 0x06005E82 RID: 24194 RVA: 0x0015D880 File Offset: 0x0015BA80
		protected override void OnWriteStartEnvelope(XmlDictionaryWriter writer)
		{
			this.innerMessage.WriteStartEnvelope(writer);
		}

		// Token: 0x06005E83 RID: 24195 RVA: 0x0015D88E File Offset: 0x0015BA8E
		protected override void OnWriteStartHeaders(XmlDictionaryWriter writer)
		{
			this.innerMessage.WriteStartHeaders(writer);
		}

		// Token: 0x06005E84 RID: 24196 RVA: 0x0015D89C File Offset: 0x0015BA9C
		protected override void OnWriteStartBody(XmlDictionaryWriter writer)
		{
			this.innerMessage.WriteStartBody(writer);
		}

		// Token: 0x06005E85 RID: 24197 RVA: 0x0015D8AA File Offset: 0x0015BAAA
		protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
		{
			this.innerMessage.WriteBodyContents(writer);
		}

		// Token: 0x06005E86 RID: 24198 RVA: 0x0015D8B8 File Offset: 0x0015BAB8
		protected override string OnGetBodyAttribute(string localName, string ns)
		{
			return this.innerMessage.GetBodyAttribute(localName, ns);
		}

		// Token: 0x06005E87 RID: 24199 RVA: 0x0015D8C7 File Offset: 0x0015BAC7
		protected override void OnBodyToString(XmlDictionaryWriter writer)
		{
			this.innerMessage.BodyToString(writer);
		}

		// Token: 0x040037FE RID: 14334
		private Message innerMessage;
	}
}
