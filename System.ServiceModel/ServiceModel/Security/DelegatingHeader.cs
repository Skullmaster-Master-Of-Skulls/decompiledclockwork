using System;
using System.ServiceModel.Channels;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x0200029D RID: 669
	internal abstract class DelegatingHeader : MessageHeader
	{
		// Token: 0x06001441 RID: 5185 RVA: 0x0004C332 File Offset: 0x0004A532
		protected DelegatingHeader(MessageHeader innerHeader)
		{
			if (innerHeader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("innerHeader");
			}
			this.innerHeader = innerHeader;
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06001442 RID: 5186 RVA: 0x0004C354 File Offset: 0x0004A554
		public override bool MustUnderstand
		{
			get
			{
				return this.innerHeader.MustUnderstand;
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06001443 RID: 5187 RVA: 0x0004C361 File Offset: 0x0004A561
		public override string Name
		{
			get
			{
				return this.innerHeader.Name;
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06001444 RID: 5188 RVA: 0x0004C36E File Offset: 0x0004A56E
		public override string Namespace
		{
			get
			{
				return this.innerHeader.Namespace;
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06001445 RID: 5189 RVA: 0x0004C37B File Offset: 0x0004A57B
		public override bool Relay
		{
			get
			{
				return this.innerHeader.Relay;
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06001446 RID: 5190 RVA: 0x0004C388 File Offset: 0x0004A588
		public override string Actor
		{
			get
			{
				return this.innerHeader.Actor;
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06001447 RID: 5191 RVA: 0x0004C395 File Offset: 0x0004A595
		protected MessageHeader InnerHeader
		{
			get
			{
				return this.innerHeader;
			}
		}

		// Token: 0x06001448 RID: 5192 RVA: 0x0004C39D File Offset: 0x0004A59D
		protected override void OnWriteStartHeader(XmlDictionaryWriter writer, MessageVersion messageVersion)
		{
			this.innerHeader.WriteStartHeader(writer, messageVersion);
		}

		// Token: 0x06001449 RID: 5193 RVA: 0x0004C3AC File Offset: 0x0004A5AC
		protected override void OnWriteHeaderContents(XmlDictionaryWriter writer, MessageVersion messageVersion)
		{
			this.innerHeader.WriteHeaderContents(writer, messageVersion);
		}

		// Token: 0x04001AB0 RID: 6832
		private MessageHeader innerHeader;
	}
}
