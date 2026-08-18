using System;
using System.Globalization;
using System.IO;
using System.Text;
using Renci.SshNet.Common;
using Renci.SshNet.Sftp.Responses;

namespace Renci.SshNet.Sftp
{
	// Token: 0x02000039 RID: 57
	internal abstract class SftpMessage : SshData
	{
		// Token: 0x0600048D RID: 1165 RVA: 0x000106EC File Offset: 0x0000E8EC
		public static SftpMessage Load(uint protocolVersion, byte[] data, Encoding encoding)
		{
			SftpMessageTypes messageType = (SftpMessageTypes)data[4];
			return SftpMessage.Load(protocolVersion, data, messageType, encoding);
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x00010706 File Offset: 0x0000E906
		protected override int ZeroReaderIndex
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x00010709 File Offset: 0x0000E909
		protected override int BufferCapacity
		{
			get
			{
				return this.ZeroReaderIndex;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000490 RID: 1168
		public abstract SftpMessageTypes SftpMessageType { get; }

		// Token: 0x06000491 RID: 1169 RVA: 0x0000262A File Offset: 0x0000082A
		protected override void LoadData()
		{
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00010711 File Offset: 0x0000E911
		protected override void SaveData()
		{
			base.Write((byte)this.SftpMessageType);
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00010720 File Offset: 0x0000E920
		protected override void WriteBytes(SshDataStream stream)
		{
			long position = stream.Position;
			stream.Seek(4L, SeekOrigin.Current);
			base.WriteBytes(stream);
			long position2 = stream.Position;
			long num = position2 - position - 4L;
			stream.Position = position;
			stream.Write((uint)num);
			stream.Position = position2;
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00010769 File Offset: 0x0000E969
		protected SftpFileAttributes ReadAttributes()
		{
			return SftpFileAttributes.FromBytes(base.DataStream);
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00010778 File Offset: 0x0000E978
		private static SftpMessage Load(uint protocolVersion, byte[] data, SftpMessageTypes messageType, Encoding encoding)
		{
			SftpMessage sftpMessage;
			if (messageType != SftpMessageTypes.Version)
			{
				switch (messageType)
				{
				case SftpMessageTypes.Status:
					sftpMessage = new SftpStatusResponse(protocolVersion);
					break;
				case SftpMessageTypes.Handle:
					sftpMessage = new SftpHandleResponse(protocolVersion);
					break;
				case SftpMessageTypes.Data:
					sftpMessage = new SftpDataResponse(protocolVersion);
					break;
				case SftpMessageTypes.Name:
					sftpMessage = new SftpNameResponse(protocolVersion, encoding);
					break;
				case SftpMessageTypes.Attrs:
					sftpMessage = new SftpAttrsResponse(protocolVersion);
					break;
				default:
					if (messageType != SftpMessageTypes.ExtendedReply)
					{
						throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture, "Message type '{0}' is not supported.", new object[]
						{
							messageType
						}));
					}
					sftpMessage = new SftpExtendedReplyResponse(protocolVersion);
					break;
				}
			}
			else
			{
				sftpMessage = new SftpVersionResponse();
			}
			sftpMessage.Load(data);
			return sftpMessage;
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0001081B File Offset: 0x0000EA1B
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "SFTP Message : {0}", new object[]
			{
				this.SftpMessageType
			});
		}
	}
}
