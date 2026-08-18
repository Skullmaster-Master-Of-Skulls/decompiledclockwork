using System;
using System.IO;
using System.Text;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x0200005C RID: 92
	[CLSCompliant(true)]
	public class Asn1OctetString : Asn1Object
	{
		// Token: 0x0600035F RID: 863 RVA: 0x00010F8C File Offset: 0x0000FF8C
		[CLSCompliant(false)]
		public Asn1OctetString(sbyte[] content) : base(Asn1OctetString.ID)
		{
			this.content = content;
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00010FB0 File Offset: 0x0000FFB0
		public Asn1OctetString(string content) : base(Asn1OctetString.ID)
		{
			try
			{
				Encoding encoding = Encoding.GetEncoding("utf-8");
				byte[] bytes = encoding.GetBytes(content);
				sbyte[] array = SupportClass.ToSByteArray(bytes);
				this.content = array;
			}
			catch (IOException ex)
			{
				throw new SystemException(ex.ToString());
			}
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00011018 File Offset: 0x00010018
		[CLSCompliant(false)]
		public Asn1OctetString(Asn1Decoder dec, Stream in_Renamed, int len) : base(Asn1OctetString.ID)
		{
			this.content = ((len > 0) ? ((sbyte[])dec.decodeOctetString(in_Renamed, len)) : new sbyte[0]);
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00011054 File Offset: 0x00010054
		public override void encode(Asn1Encoder enc, Stream out_Renamed)
		{
			enc.encode(this, out_Renamed);
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0001106C File Offset: 0x0001006C
		[CLSCompliant(false)]
		public sbyte[] byteValue()
		{
			return this.content;
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00011084 File Offset: 0x00010084
		public string stringValue()
		{
			string result = null;
			try
			{
				Encoding encoding = Encoding.GetEncoding("utf-8");
				char[] chars = encoding.GetChars(SupportClass.ToByteArray(this.content));
				result = new string(chars);
			}
			catch (IOException ex)
			{
				throw new SystemException(ex.ToString());
			}
			return result;
		}

		// Token: 0x06000365 RID: 869 RVA: 0x000110EC File Offset: 0x000100EC
		public override string ToString()
		{
			return base.ToString() + "OCTET STRING: " + this.stringValue();
		}

		// Token: 0x04000195 RID: 405
		public const int TAG = 4;

		// Token: 0x04000196 RID: 406
		private sbyte[] content;

		// Token: 0x04000197 RID: 407
		protected internal static readonly Asn1Identifier ID = new Asn1Identifier(0, false, 4);
	}
}
