using System;
using System.IO;
using Org.BouncyCastle.Bcpg.Attr;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x02000302 RID: 770
	public class UserAttributeSubpacketsParser
	{
		// Token: 0x06001C30 RID: 7216 RVA: 0x000A8A47 File Offset: 0x000A7A47
		public UserAttributeSubpacketsParser(Stream input)
		{
			this.input = input;
		}

		// Token: 0x06001C31 RID: 7217 RVA: 0x000A8A58 File Offset: 0x000A7A58
		public UserAttributeSubpacket ReadPacket()
		{
			int num = this.input.ReadByte();
			if (num < 0)
			{
				return null;
			}
			int num2 = 0;
			if (num < 192)
			{
				num2 = num;
			}
			else if (num <= 223)
			{
				num2 = (num - 192 << 8) + this.input.ReadByte() + 192;
			}
			else if (num == 255)
			{
				num2 = (this.input.ReadByte() << 24 | this.input.ReadByte() << 16 | this.input.ReadByte() << 8 | this.input.ReadByte());
			}
			int num3 = this.input.ReadByte();
			if (num3 < 0)
			{
				throw new EndOfStreamException("unexpected EOF reading user attribute sub packet");
			}
			byte[] array = new byte[num2 - 1];
			if (Streams.ReadFully(this.input, array) < array.Length)
			{
				throw new EndOfStreamException();
			}
			UserAttributeSubpacketTag userAttributeSubpacketTag = (UserAttributeSubpacketTag)num3;
			UserAttributeSubpacketTag userAttributeSubpacketTag2 = userAttributeSubpacketTag;
			if (userAttributeSubpacketTag2 == UserAttributeSubpacketTag.ImageAttribute)
			{
				return new ImageAttrib(array);
			}
			return new UserAttributeSubpacket(userAttributeSubpacketTag, array);
		}

		// Token: 0x04001358 RID: 4952
		private readonly Stream input;
	}
}
