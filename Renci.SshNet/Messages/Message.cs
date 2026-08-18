using System;
using System.Globalization;
using System.IO;
using System.Linq;
using Renci.SshNet.Abstractions;
using Renci.SshNet.Common;
using Renci.SshNet.Compression;

namespace Renci.SshNet.Messages
{
	// Token: 0x02000097 RID: 151
	public abstract class Message : SshData
	{
		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000783 RID: 1923 RVA: 0x0000CACF File Offset: 0x0000ACCF
		protected override int ZeroReaderIndex
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000784 RID: 1924 RVA: 0x0000CACF File Offset: 0x0000ACCF
		protected override int BufferCapacity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x0001D9E0 File Offset: 0x0001BBE0
		protected override void WriteBytes(SshDataStream stream)
		{
			MessageAttribute messageAttribute = base.GetType().GetCustomAttributes(true).FirstOrDefault<MessageAttribute>();
			if (messageAttribute == null)
			{
				throw new SshException(string.Format(CultureInfo.CurrentCulture, "Type '{0}' is not a valid message type.", new object[]
				{
					base.GetType().AssemblyQualifiedName
				}));
			}
			stream.WriteByte(messageAttribute.Number);
			base.WriteBytes(stream);
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0001DA40 File Offset: 0x0001BC40
		internal byte[] GetPacket(byte paddingMultiplier, Compressor compressor)
		{
			int num = this.BufferCapacity;
			SshDataStream sshDataStream;
			if (num == -1 || compressor != null)
			{
				sshDataStream = new SshDataStream(64);
				sshDataStream.Seek(9L, SeekOrigin.Begin);
				if (compressor != null)
				{
					SshDataStream sshDataStream2 = new SshDataStream((num != -1) ? num : 64);
					this.WriteBytes(sshDataStream2);
					byte[] array = compressor.Compress(sshDataStream2.ToArray());
					sshDataStream.Write(array, 0, array.Length);
				}
				else
				{
					this.WriteBytes(sshDataStream);
				}
				num = (int)sshDataStream.Length - 9;
				int num2 = num + 4 + 1;
				byte paddingLength = Message.GetPaddingLength(paddingMultiplier, (long)num2);
				byte[] array2 = new byte[(int)paddingLength];
				CryptoAbstraction.GenerateRandom(array2);
				sshDataStream.Write(array2, 0, (int)paddingLength);
				uint packetDataLength = Message.GetPacketDataLength(num, paddingLength);
				sshDataStream.Seek(4L, SeekOrigin.Begin);
				sshDataStream.Write(packetDataLength.GetBytes(), 0, 4);
				sshDataStream.WriteByte(paddingLength);
			}
			else
			{
				int num3 = num + 4 + 1;
				byte paddingLength2 = Message.GetPaddingLength(paddingMultiplier, (long)num3);
				uint packetDataLength2 = Message.GetPacketDataLength(num, paddingLength2);
				sshDataStream = new SshDataStream(num3 + (int)paddingLength2 + 4);
				sshDataStream.Seek(4L, SeekOrigin.Begin);
				sshDataStream.Write(packetDataLength2.GetBytes(), 0, 4);
				sshDataStream.WriteByte(paddingLength2);
				this.WriteBytes(sshDataStream);
				byte[] array3 = new byte[(int)paddingLength2];
				CryptoAbstraction.GenerateRandom(array3);
				sshDataStream.Write(array3, 0, (int)paddingLength2);
			}
			return sshDataStream.ToArray();
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0001DB81 File Offset: 0x0001BD81
		private static uint GetPacketDataLength(int messageLength, byte paddingLength)
		{
			return (uint)(messageLength + (int)paddingLength + 1);
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0001DB88 File Offset: 0x0001BD88
		private static byte GetPaddingLength(byte paddingMultiplier, long packetLength)
		{
			byte b = (byte)(-packetLength & (long)(paddingMultiplier - 1));
			if (b < paddingMultiplier)
			{
				b += paddingMultiplier;
			}
			return b;
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x0001DBA8 File Offset: 0x0001BDA8
		public override string ToString()
		{
			MessageAttribute messageAttribute = base.GetType().GetCustomAttributes(true).SingleOrDefault<MessageAttribute>();
			if (messageAttribute == null)
			{
				return string.Format(CultureInfo.CurrentCulture, "'{0}' without Message attribute.", new object[]
				{
					base.GetType().FullName
				});
			}
			return messageAttribute.Name;
		}
	}
}
