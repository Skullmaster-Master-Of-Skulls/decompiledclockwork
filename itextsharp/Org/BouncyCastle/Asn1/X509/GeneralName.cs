using System;
using System.Collections;
using System.Globalization;
using System.Text;
using Org.BouncyCastle.Utilities.Net;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000362 RID: 866
	public class GeneralName : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x06001EF8 RID: 7928 RVA: 0x000BA38D File Offset: 0x000B938D
		public GeneralName(X509Name directoryName)
		{
			this.obj = directoryName;
			this.tag = 4;
		}

		// Token: 0x06001EF9 RID: 7929 RVA: 0x000BA3A3 File Offset: 0x000B93A3
		public GeneralName(Asn1Object name, int tag)
		{
			this.obj = name;
			this.tag = tag;
		}

		// Token: 0x06001EFA RID: 7930 RVA: 0x000BA3B9 File Offset: 0x000B93B9
		public GeneralName(int tag, Asn1Encodable name)
		{
			this.obj = name;
			this.tag = tag;
		}

		// Token: 0x06001EFB RID: 7931 RVA: 0x000BA3D0 File Offset: 0x000B93D0
		public GeneralName(int tag, string name)
		{
			this.tag = tag;
			if (tag == 1 || tag == 2 || tag == 6)
			{
				this.obj = new DerIA5String(name);
				return;
			}
			if (tag == 8)
			{
				this.obj = new DerObjectIdentifier(name);
				return;
			}
			if (tag == 4)
			{
				this.obj = new X509Name(name);
				return;
			}
			if (tag != 7)
			{
				throw new ArgumentException("can't process string for tag: " + tag, "tag");
			}
			byte[] array = this.toGeneralNameEncoding(name);
			if (array == null)
			{
				throw new ArgumentException("IP Address is invalid", "name");
			}
			this.obj = new DerOctetString(array);
		}

		// Token: 0x06001EFC RID: 7932 RVA: 0x000BA46C File Offset: 0x000B946C
		public static GeneralName GetInstance(object obj)
		{
			if (obj == null || obj is GeneralName)
			{
				return (GeneralName)obj;
			}
			if (obj is Asn1TaggedObject)
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)obj;
				int tagNo = asn1TaggedObject.TagNo;
				switch (tagNo)
				{
				case 0:
					return new GeneralName(tagNo, Asn1Sequence.GetInstance(asn1TaggedObject, false));
				case 1:
					return new GeneralName(tagNo, DerIA5String.GetInstance(asn1TaggedObject, false));
				case 2:
					return new GeneralName(tagNo, DerIA5String.GetInstance(asn1TaggedObject, false));
				case 3:
					throw new ArgumentException("unknown tag: " + tagNo);
				case 4:
					return new GeneralName(tagNo, X509Name.GetInstance(asn1TaggedObject, true));
				case 5:
					return new GeneralName(tagNo, Asn1Sequence.GetInstance(asn1TaggedObject, false));
				case 6:
					return new GeneralName(tagNo, DerIA5String.GetInstance(asn1TaggedObject, false));
				case 7:
					return new GeneralName(tagNo, Asn1OctetString.GetInstance(asn1TaggedObject, false));
				case 8:
					return new GeneralName(tagNo, DerObjectIdentifier.GetInstance(asn1TaggedObject, false));
				}
			}
			throw new ArgumentException("unknown object in GetInstance: " + obj.GetType().FullName, "obj");
		}

		// Token: 0x06001EFD RID: 7933 RVA: 0x000BA57A File Offset: 0x000B957A
		public static GeneralName GetInstance(Asn1TaggedObject tagObj, bool explicitly)
		{
			return GeneralName.GetInstance(Asn1TaggedObject.GetInstance(tagObj, true));
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06001EFE RID: 7934 RVA: 0x000BA588 File Offset: 0x000B9588
		public int TagNo
		{
			get
			{
				return this.tag;
			}
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06001EFF RID: 7935 RVA: 0x000BA590 File Offset: 0x000B9590
		public Asn1Encodable Name
		{
			get
			{
				return this.obj;
			}
		}

		// Token: 0x06001F00 RID: 7936 RVA: 0x000BA598 File Offset: 0x000B9598
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.tag);
			stringBuilder.Append(": ");
			switch (this.tag)
			{
			case 1:
			case 2:
			case 6:
				stringBuilder.Append(DerIA5String.GetInstance(this.obj).GetString());
				goto IL_8C;
			case 4:
				stringBuilder.Append(X509Name.GetInstance(this.obj).ToString());
				goto IL_8C;
			}
			stringBuilder.Append(this.obj.ToString());
			IL_8C:
			return stringBuilder.ToString();
		}

		// Token: 0x06001F01 RID: 7937 RVA: 0x000BA638 File Offset: 0x000B9638
		private byte[] toGeneralNameEncoding(string ip)
		{
			if (Org.BouncyCastle.Utilities.Net.IPAddress.IsValidIPv6WithNetmask(ip) || Org.BouncyCastle.Utilities.Net.IPAddress.IsValidIPv6(ip))
			{
				int num = ip.IndexOf('/');
				if (num < 0)
				{
					byte[] array = new byte[16];
					int[] parsedIp = this.parseIPv6(ip);
					this.copyInts(parsedIp, array, 0);
					return array;
				}
				byte[] array2 = new byte[32];
				int[] parsedIp2 = this.parseIPv6(ip.Substring(0, num));
				this.copyInts(parsedIp2, array2, 0);
				string text = ip.Substring(num + 1);
				if (text.IndexOf(':') > 0)
				{
					parsedIp2 = this.parseIPv6(text);
				}
				else
				{
					parsedIp2 = this.parseMask(text);
				}
				this.copyInts(parsedIp2, array2, 16);
				return array2;
			}
			else
			{
				if (!Org.BouncyCastle.Utilities.Net.IPAddress.IsValidIPv4WithNetmask(ip) && !Org.BouncyCastle.Utilities.Net.IPAddress.IsValidIPv4(ip))
				{
					return null;
				}
				int num2 = ip.IndexOf('/');
				if (num2 < 0)
				{
					byte[] array3 = new byte[4];
					this.parseIPv4(ip, array3, 0);
					return array3;
				}
				byte[] array4 = new byte[8];
				this.parseIPv4(ip.Substring(0, num2), array4, 0);
				string text2 = ip.Substring(num2 + 1);
				if (text2.IndexOf('.') > 0)
				{
					this.parseIPv4(text2, array4, 4);
				}
				else
				{
					this.parseIPv4Mask(text2, array4, 4);
				}
				return array4;
			}
		}

		// Token: 0x06001F02 RID: 7938 RVA: 0x000BA760 File Offset: 0x000B9760
		private void parseIPv4Mask(string mask, byte[] addr, int offset)
		{
			int num = int.Parse(mask);
			for (int num2 = 0; num2 != num; num2++)
			{
				int num3 = num2 / 8 + offset;
				addr[num3] |= (byte)(1 << num2 % 8);
			}
		}

		// Token: 0x06001F03 RID: 7939 RVA: 0x000BA7A4 File Offset: 0x000B97A4
		private void parseIPv4(string ip, byte[] addr, int offset)
		{
			foreach (string s in ip.Split(new char[]
			{
				'.',
				'/'
			}))
			{
				addr[offset++] = (byte)int.Parse(s);
			}
		}

		// Token: 0x06001F04 RID: 7940 RVA: 0x000BA7EC File Offset: 0x000B97EC
		private int[] parseMask(string mask)
		{
			int[] array = new int[8];
			int num = int.Parse(mask);
			for (int num2 = 0; num2 != num; num2++)
			{
				array[num2 / 16] |= 1 << num2 % 16;
			}
			return array;
		}

		// Token: 0x06001F05 RID: 7941 RVA: 0x000BA834 File Offset: 0x000B9834
		private void copyInts(int[] parsedIp, byte[] addr, int offSet)
		{
			for (int num = 0; num != parsedIp.Length; num++)
			{
				addr[num * 2 + offSet] = (byte)(parsedIp[num] >> 8);
				addr[num * 2 + 1 + offSet] = (byte)parsedIp[num];
			}
		}

		// Token: 0x06001F06 RID: 7942 RVA: 0x000BA86C File Offset: 0x000B986C
		private int[] parseIPv6(string ip)
		{
			if (ip.StartsWith("::"))
			{
				ip = ip.Substring(1);
			}
			else if (ip.EndsWith("::"))
			{
				ip = ip.Substring(0, ip.Length - 1);
			}
			IEnumerator enumerator = ip.Split(new char[]
			{
				':'
			}).GetEnumerator();
			int num = 0;
			int[] array = new int[8];
			int num2 = -1;
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				string text = (string)obj;
				if (text.Length == 0)
				{
					num2 = num;
					array[num++] = 0;
				}
				else if (text.IndexOf('.') < 0)
				{
					array[num++] = int.Parse(text, NumberStyles.AllowHexSpecifier);
				}
				else
				{
					string[] array2 = text.Split(new char[]
					{
						'.'
					});
					array[num++] = (int.Parse(array2[0]) << 8 | int.Parse(array2[1]));
					array[num++] = (int.Parse(array2[2]) << 8 | int.Parse(array2[3]));
				}
			}
			if (num != array.Length)
			{
				Array.Copy(array, num2, array, array.Length - (num - num2), num - num2);
				for (int num3 = num2; num3 != array.Length - (num - num2); num3++)
				{
					array[num3] = 0;
				}
			}
			return array;
		}

		// Token: 0x06001F07 RID: 7943 RVA: 0x000BA9AE File Offset: 0x000B99AE
		public override Asn1Object ToAsn1Object()
		{
			return new DerTaggedObject(this.tag == 4, this.tag, this.obj);
		}

		// Token: 0x04001564 RID: 5476
		public const int OtherName = 0;

		// Token: 0x04001565 RID: 5477
		public const int Rfc822Name = 1;

		// Token: 0x04001566 RID: 5478
		public const int DnsName = 2;

		// Token: 0x04001567 RID: 5479
		public const int X400Address = 3;

		// Token: 0x04001568 RID: 5480
		public const int DirectoryName = 4;

		// Token: 0x04001569 RID: 5481
		public const int EdiPartyName = 5;

		// Token: 0x0400156A RID: 5482
		public const int UniformResourceIdentifier = 6;

		// Token: 0x0400156B RID: 5483
		public const int IPAddress = 7;

		// Token: 0x0400156C RID: 5484
		public const int RegisteredID = 8;

		// Token: 0x0400156D RID: 5485
		internal readonly Asn1Encodable obj;

		// Token: 0x0400156E RID: 5486
		internal readonly int tag;
	}
}
