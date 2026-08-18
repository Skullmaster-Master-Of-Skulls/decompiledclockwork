using System;
using Org.BouncyCastle.Bcpg.Attr;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x0200029A RID: 666
	public class PgpUserAttributeSubpacketVector
	{
		// Token: 0x06001911 RID: 6417 RVA: 0x000932C1 File Offset: 0x000922C1
		internal PgpUserAttributeSubpacketVector(UserAttributeSubpacket[] packets)
		{
			this.packets = packets;
		}

		// Token: 0x06001912 RID: 6418 RVA: 0x000932D0 File Offset: 0x000922D0
		public UserAttributeSubpacket GetSubpacket(UserAttributeSubpacketTag type)
		{
			for (int num = 0; num != this.packets.Length; num++)
			{
				if (this.packets[num].SubpacketType == type)
				{
					return this.packets[num];
				}
			}
			return null;
		}

		// Token: 0x06001913 RID: 6419 RVA: 0x0009330C File Offset: 0x0009230C
		public ImageAttrib GetImageAttribute()
		{
			UserAttributeSubpacket subpacket = this.GetSubpacket(UserAttributeSubpacketTag.ImageAttribute);
			if (subpacket != null)
			{
				return (ImageAttrib)subpacket;
			}
			return null;
		}

		// Token: 0x06001914 RID: 6420 RVA: 0x0009332C File Offset: 0x0009232C
		internal UserAttributeSubpacket[] ToSubpacketArray()
		{
			return this.packets;
		}

		// Token: 0x06001915 RID: 6421 RVA: 0x00093334 File Offset: 0x00092334
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			PgpUserAttributeSubpacketVector pgpUserAttributeSubpacketVector = obj as PgpUserAttributeSubpacketVector;
			if (pgpUserAttributeSubpacketVector == null)
			{
				return false;
			}
			if (pgpUserAttributeSubpacketVector.packets.Length != this.packets.Length)
			{
				return false;
			}
			for (int num = 0; num != this.packets.Length; num++)
			{
				if (!pgpUserAttributeSubpacketVector.packets[num].Equals(this.packets[num]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001916 RID: 6422 RVA: 0x00093394 File Offset: 0x00092394
		public override int GetHashCode()
		{
			int num = 0;
			foreach (UserAttributeSubpacket obj in this.packets)
			{
				num ^= obj.GetHashCode();
			}
			return num;
		}

		// Token: 0x040010ED RID: 4333
		private readonly UserAttributeSubpacket[] packets;
	}
}
