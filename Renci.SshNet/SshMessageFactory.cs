using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Renci.SshNet.Common;
using Renci.SshNet.Messages;
using Renci.SshNet.Messages.Authentication;
using Renci.SshNet.Messages.Connection;
using Renci.SshNet.Messages.Transport;

namespace Renci.SshNet
{
	// Token: 0x0200002F RID: 47
	internal class SshMessageFactory
	{
		// Token: 0x060003A0 RID: 928 RVA: 0x0000DEDC File Offset: 0x0000C0DC
		static SshMessageFactory()
		{
			foreach (SshMessageFactory.MessageMetadata messageMetadata in SshMessageFactory.AllMessages)
			{
				SshMessageFactory.MessagesByName.Add(messageMetadata.Name, messageMetadata);
			}
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000E146 File Offset: 0x0000C346
		public SshMessageFactory()
		{
			this._activatedMessagesById = new bool[31];
			this._enabledMessagesByNumber = new SshMessageFactory.MessageMetadata[101];
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000E168 File Offset: 0x0000C368
		public void Reset()
		{
			Array.Clear(this._activatedMessagesById, 0, this._activatedMessagesById.Length);
			Array.Clear(this._enabledMessagesByNumber, 0, this._enabledMessagesByNumber.Length);
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000E194 File Offset: 0x0000C394
		public Message Create(byte messageNumber)
		{
			if (messageNumber > 100)
			{
				throw SshMessageFactory.CreateMessageTypeNotSupportedException(messageNumber);
			}
			SshMessageFactory.MessageMetadata messageMetadata = this._enabledMessagesByNumber[(int)messageNumber];
			if (messageMetadata != null)
			{
				return messageMetadata.Create();
			}
			if (SshMessageFactory.AllMessages.FirstOrDefault((SshMessageFactory.MessageMetadata p) => p.Number == messageNumber) == null)
			{
				throw SshMessageFactory.CreateMessageTypeNotSupportedException(messageNumber);
			}
			throw new SshException(string.Format(CultureInfo.InvariantCulture, "Message type {0} is not valid in the current context.", new object[]
			{
				messageNumber
			}));
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000E228 File Offset: 0x0000C428
		public void DisableNonKeyExchangeMessages()
		{
			SshMessageFactory.MessageMetadata[] allMessages = SshMessageFactory.AllMessages;
			for (int i = 0; i < allMessages.Length; i++)
			{
				byte number = allMessages[i].Number;
				if ((number > 2 && number < 20) || number > 30)
				{
					this._enabledMessagesByNumber[(int)number] = null;
				}
			}
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000E26C File Offset: 0x0000C46C
		public void EnableActivatedMessages()
		{
			foreach (SshMessageFactory.MessageMetadata messageMetadata in SshMessageFactory.AllMessages)
			{
				if (this._activatedMessagesById[(int)messageMetadata.Id])
				{
					SshMessageFactory.MessageMetadata messageMetadata2 = this._enabledMessagesByNumber[(int)messageMetadata.Number];
					if (messageMetadata2 != null && messageMetadata2 != messageMetadata)
					{
						throw SshMessageFactory.CreateMessageTypeAlreadyEnabledForOtherMessageException(messageMetadata.Number, messageMetadata.Name, messageMetadata2.Name);
					}
					this._enabledMessagesByNumber[(int)messageMetadata.Number] = messageMetadata;
				}
			}
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000E2DC File Offset: 0x0000C4DC
		public void EnableAndActivateMessage(string messageName)
		{
			if (messageName == null)
			{
				throw new ArgumentNullException("messageName");
			}
			lock (this)
			{
				SshMessageFactory.MessageMetadata messageMetadata;
				if (!SshMessageFactory.MessagesByName.TryGetValue(messageName, out messageMetadata))
				{
					throw SshMessageFactory.CreateMessageNotSupportedException(messageName);
				}
				SshMessageFactory.MessageMetadata messageMetadata2 = this._enabledMessagesByNumber[(int)messageMetadata.Number];
				if (messageMetadata2 != null && messageMetadata2 != messageMetadata)
				{
					throw SshMessageFactory.CreateMessageTypeAlreadyEnabledForOtherMessageException(messageMetadata.Number, messageMetadata.Name, messageMetadata2.Name);
				}
				this._enabledMessagesByNumber[(int)messageMetadata.Number] = messageMetadata;
				this._activatedMessagesById[(int)messageMetadata.Id] = true;
			}
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000E380 File Offset: 0x0000C580
		public void DisableAndDeactivateMessage(string messageName)
		{
			if (messageName == null)
			{
				throw new ArgumentNullException("messageName");
			}
			lock (this)
			{
				SshMessageFactory.MessageMetadata messageMetadata;
				if (!SshMessageFactory.MessagesByName.TryGetValue(messageName, out messageMetadata))
				{
					throw SshMessageFactory.CreateMessageNotSupportedException(messageName);
				}
				SshMessageFactory.MessageMetadata messageMetadata2 = this._enabledMessagesByNumber[(int)messageMetadata.Number];
				if (messageMetadata2 != null && messageMetadata2 != messageMetadata)
				{
					throw SshMessageFactory.CreateMessageTypeAlreadyEnabledForOtherMessageException(messageMetadata.Number, messageMetadata.Name, messageMetadata2.Name);
				}
				this._activatedMessagesById[(int)messageMetadata.Id] = false;
				this._enabledMessagesByNumber[(int)messageMetadata.Number] = null;
			}
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000E424 File Offset: 0x0000C624
		private static SshException CreateMessageTypeNotSupportedException(byte messageNumber)
		{
			throw new SshException(string.Format(CultureInfo.InvariantCulture, "Message type {0} is not supported.", new object[]
			{
				messageNumber
			}));
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000E449 File Offset: 0x0000C649
		private static SshException CreateMessageNotSupportedException(string messageName)
		{
			throw new SshException(string.Format(CultureInfo.InvariantCulture, "Message '{0}' is not supported.", new object[]
			{
				messageName
			}));
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0000E469 File Offset: 0x0000C669
		private static SshException CreateMessageTypeAlreadyEnabledForOtherMessageException(byte messageNumber, string messageName, string currentEnabledForMessageName)
		{
			throw new SshException(string.Format(CultureInfo.InvariantCulture, "Cannot enable message '{0}'. Message type {1} is already enabled for '{2}'.", new object[]
			{
				messageName,
				messageNumber,
				currentEnabledForMessageName
			}));
		}

		// Token: 0x04000114 RID: 276
		private readonly SshMessageFactory.MessageMetadata[] _enabledMessagesByNumber;

		// Token: 0x04000115 RID: 277
		private readonly bool[] _activatedMessagesById;

		// Token: 0x04000116 RID: 278
		internal static readonly SshMessageFactory.MessageMetadata[] AllMessages = new SshMessageFactory.MessageMetadata[]
		{
			new SshMessageFactory.MessageMetadata<KeyExchangeInitMessage>(0, "SSH_MSG_KEXINIT", 20),
			new SshMessageFactory.MessageMetadata<NewKeysMessage>(1, "SSH_MSG_NEWKEYS", 21),
			new SshMessageFactory.MessageMetadata<RequestFailureMessage>(2, "SSH_MSG_REQUEST_FAILURE", 82),
			new SshMessageFactory.MessageMetadata<ChannelOpenFailureMessage>(3, "SSH_MSG_CHANNEL_OPEN_FAILURE", 92),
			new SshMessageFactory.MessageMetadata<ChannelFailureMessage>(4, "SSH_MSG_CHANNEL_FAILURE", 100),
			new SshMessageFactory.MessageMetadata<ChannelExtendedDataMessage>(5, "SSH_MSG_CHANNEL_EXTENDED_DATA", 95),
			new SshMessageFactory.MessageMetadata<ChannelDataMessage>(6, "SSH_MSG_CHANNEL_DATA", 94),
			new SshMessageFactory.MessageMetadata<ChannelRequestMessage>(7, "SSH_MSG_CHANNEL_REQUEST", 98),
			new SshMessageFactory.MessageMetadata<BannerMessage>(8, "SSH_MSG_USERAUTH_BANNER", 53),
			new SshMessageFactory.MessageMetadata<InformationResponseMessage>(9, "SSH_MSG_USERAUTH_INFO_RESPONSE", 61),
			new SshMessageFactory.MessageMetadata<FailureMessage>(10, "SSH_MSG_USERAUTH_FAILURE", 51),
			new SshMessageFactory.MessageMetadata<DebugMessage>(11, "SSH_MSG_DEBUG", 4),
			new SshMessageFactory.MessageMetadata<GlobalRequestMessage>(12, "SSH_MSG_GLOBAL_REQUEST", 80),
			new SshMessageFactory.MessageMetadata<ChannelOpenMessage>(13, "SSH_MSG_CHANNEL_OPEN", 90),
			new SshMessageFactory.MessageMetadata<ChannelOpenConfirmationMessage>(14, "SSH_MSG_CHANNEL_OPEN_CONFIRMATION", 91),
			new SshMessageFactory.MessageMetadata<InformationRequestMessage>(15, "SSH_MSG_USERAUTH_INFO_REQUEST", 60),
			new SshMessageFactory.MessageMetadata<UnimplementedMessage>(16, "SSH_MSG_UNIMPLEMENTED", 3),
			new SshMessageFactory.MessageMetadata<RequestSuccessMessage>(17, "SSH_MSG_REQUEST_SUCCESS", 81),
			new SshMessageFactory.MessageMetadata<ChannelSuccessMessage>(18, "SSH_MSG_CHANNEL_SUCCESS", 99),
			new SshMessageFactory.MessageMetadata<PasswordChangeRequiredMessage>(19, "SSH_MSG_USERAUTH_PASSWD_CHANGEREQ", 60),
			new SshMessageFactory.MessageMetadata<DisconnectMessage>(20, "SSH_MSG_DISCONNECT", 1),
			new SshMessageFactory.MessageMetadata<SuccessMessage>(21, "SSH_MSG_USERAUTH_SUCCESS", 52),
			new SshMessageFactory.MessageMetadata<PublicKeyMessage>(22, "SSH_MSG_USERAUTH_PK_OK", 60),
			new SshMessageFactory.MessageMetadata<IgnoreMessage>(23, "SSH_MSG_IGNORE", 2),
			new SshMessageFactory.MessageMetadata<ChannelWindowAdjustMessage>(24, "SSH_MSG_CHANNEL_WINDOW_ADJUST", 93),
			new SshMessageFactory.MessageMetadata<ChannelEofMessage>(25, "SSH_MSG_CHANNEL_EOF", 96),
			new SshMessageFactory.MessageMetadata<ChannelCloseMessage>(26, "SSH_MSG_CHANNEL_CLOSE", 97),
			new SshMessageFactory.MessageMetadata<ServiceAcceptMessage>(27, "SSH_MSG_SERVICE_ACCEPT", 6),
			new SshMessageFactory.MessageMetadata<KeyExchangeDhGroupExchangeGroup>(28, "SSH_MSG_KEX_DH_GEX_GROUP", 31),
			new SshMessageFactory.MessageMetadata<KeyExchangeDhReplyMessage>(29, "SSH_MSG_KEXDH_REPLY", 31),
			new SshMessageFactory.MessageMetadata<KeyExchangeDhGroupExchangeReply>(30, "SSH_MSG_KEX_DH_GEX_REPLY", 33)
		};

		// Token: 0x04000117 RID: 279
		private static readonly IDictionary<string, SshMessageFactory.MessageMetadata> MessagesByName = new Dictionary<string, SshMessageFactory.MessageMetadata>(SshMessageFactory.AllMessages.Length);

		// Token: 0x04000118 RID: 280
		internal const byte HighestMessageNumber = 100;

		// Token: 0x04000119 RID: 281
		internal const int TotalMessageCount = 31;

		// Token: 0x0200013D RID: 317
		internal abstract class MessageMetadata
		{
			// Token: 0x06000C99 RID: 3225 RVA: 0x0002848C File Offset: 0x0002668C
			protected MessageMetadata(byte id, string name, byte number)
			{
				this.Id = id;
				this.Name = name;
				this.Number = number;
			}

			// Token: 0x06000C9A RID: 3226
			public abstract Message Create();

			// Token: 0x0400051D RID: 1309
			public readonly byte Id;

			// Token: 0x0400051E RID: 1310
			public readonly string Name;

			// Token: 0x0400051F RID: 1311
			public readonly byte Number;
		}

		// Token: 0x0200013E RID: 318
		internal class MessageMetadata<T> : SshMessageFactory.MessageMetadata where T : Message, new()
		{
			// Token: 0x06000C9B RID: 3227 RVA: 0x000284A9 File Offset: 0x000266A9
			public MessageMetadata(byte id, string name, byte number) : base(id, name, number)
			{
			}

			// Token: 0x06000C9C RID: 3228 RVA: 0x000284B4 File Offset: 0x000266B4
			public override Message Create()
			{
				return Activator.CreateInstance<T>();
			}
		}
	}
}
