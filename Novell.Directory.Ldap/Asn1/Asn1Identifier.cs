using System;
using System.IO;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x02000058 RID: 88
	[CLSCompliant(true)]
	public class Asn1Identifier : ICloneable
	{
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000341 RID: 833 RVA: 0x00010A44 File Offset: 0x0000FA44
		public virtual int Asn1Class
		{
			get
			{
				return this.tagClass;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000342 RID: 834 RVA: 0x00010A5C File Offset: 0x0000FA5C
		public virtual bool Constructed
		{
			get
			{
				return this.constructed;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000343 RID: 835 RVA: 0x00010A74 File Offset: 0x0000FA74
		public virtual int Tag
		{
			get
			{
				return this.tag;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000344 RID: 836 RVA: 0x00010A8C File Offset: 0x0000FA8C
		public virtual int EncodedLength
		{
			get
			{
				return this.encodedLength;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000345 RID: 837 RVA: 0x00010AA4 File Offset: 0x0000FAA4
		[CLSCompliant(false)]
		public virtual bool Universal
		{
			get
			{
				return this.tagClass == 0;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000346 RID: 838 RVA: 0x00010AC0 File Offset: 0x0000FAC0
		[CLSCompliant(false)]
		public virtual bool Application
		{
			get
			{
				return this.tagClass == 1;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000347 RID: 839 RVA: 0x00010ADC File Offset: 0x0000FADC
		[CLSCompliant(false)]
		public virtual bool Context
		{
			get
			{
				return this.tagClass == 2;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000348 RID: 840 RVA: 0x00010AF8 File Offset: 0x0000FAF8
		[CLSCompliant(false)]
		public virtual bool Private
		{
			get
			{
				return this.tagClass == 3;
			}
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00010B14 File Offset: 0x0000FB14
		public Asn1Identifier(int tagClass, bool constructed, int tag)
		{
			this.tagClass = tagClass;
			this.constructed = constructed;
			this.tag = tag;
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00010B3C File Offset: 0x0000FB3C
		public Asn1Identifier(Stream in_Renamed)
		{
			int num = in_Renamed.ReadByte();
			this.encodedLength++;
			if (num < 0)
			{
				throw new EndOfStreamException("BERDecoder: decode: EOF in Identifier");
			}
			this.tagClass = num >> 6;
			this.constructed = ((num & 32) != 0);
			this.tag = (num & 31);
			if (this.tag == 31)
			{
				this.tag = this.decodeTagNumber(in_Renamed);
			}
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00010BB0 File Offset: 0x0000FBB0
		public Asn1Identifier()
		{
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00010BC8 File Offset: 0x0000FBC8
		public void reset(Stream in_Renamed)
		{
			this.encodedLength = 0;
			int num = in_Renamed.ReadByte();
			this.encodedLength++;
			if (num < 0)
			{
				throw new EndOfStreamException("BERDecoder: decode: EOF in Identifier");
			}
			this.tagClass = num >> 6;
			this.constructed = ((num & 32) != 0);
			this.tag = (num & 31);
			if (this.tag == 31)
			{
				this.tag = this.decodeTagNumber(in_Renamed);
			}
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00010C3C File Offset: 0x0000FC3C
		private int decodeTagNumber(Stream in_Renamed)
		{
			int num = 0;
			for (;;)
			{
				int num2 = in_Renamed.ReadByte();
				this.encodedLength++;
				if (num2 < 0)
				{
					break;
				}
				num = (num << 7) + (num2 & 127);
				if ((num2 & 128) == 0)
				{
					return num;
				}
			}
			throw new EndOfStreamException("BERDecoder: decode: EOF in tag number");
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00010C8C File Offset: 0x0000FC8C
		public object Clone()
		{
			object result;
			try
			{
				result = base.MemberwiseClone();
			}
			catch (Exception ex)
			{
				throw new SystemException("Internal error, cannot create clone");
			}
			return result;
		}

		// Token: 0x04000187 RID: 391
		public const int UNIVERSAL = 0;

		// Token: 0x04000188 RID: 392
		public const int APPLICATION = 1;

		// Token: 0x04000189 RID: 393
		public const int CONTEXT = 2;

		// Token: 0x0400018A RID: 394
		public const int PRIVATE = 3;

		// Token: 0x0400018B RID: 395
		private int tagClass;

		// Token: 0x0400018C RID: 396
		private bool constructed;

		// Token: 0x0400018D RID: 397
		private int tag;

		// Token: 0x0400018E RID: 398
		private int encodedLength;
	}
}
