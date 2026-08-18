using System;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x0200040E RID: 1038
	public class DerBoolean : Asn1Object
	{
		// Token: 0x0600234E RID: 9038 RVA: 0x000D924C File Offset: 0x000D824C
		public static DerBoolean GetInstance(object obj)
		{
			if (obj == null || obj is DerBoolean)
			{
				return (DerBoolean)obj;
			}
			if (obj is Asn1OctetString)
			{
				return new DerBoolean(((Asn1OctetString)obj).GetOctets());
			}
			if (obj is Asn1TaggedObject)
			{
				return DerBoolean.GetInstance(((Asn1TaggedObject)obj).GetObject());
			}
			throw new ArgumentException("illegal object in GetInstance: " + obj.GetType().Name);
		}

		// Token: 0x0600234F RID: 9039 RVA: 0x000D92B7 File Offset: 0x000D82B7
		public static DerBoolean GetInstance(bool value)
		{
			if (!value)
			{
				return DerBoolean.False;
			}
			return DerBoolean.True;
		}

		// Token: 0x06002350 RID: 9040 RVA: 0x000D92C7 File Offset: 0x000D82C7
		public static DerBoolean GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return DerBoolean.GetInstance(obj.GetObject());
		}

		// Token: 0x06002351 RID: 9041 RVA: 0x000D92D4 File Offset: 0x000D82D4
		public DerBoolean(byte[] value)
		{
			this.value = value[0];
		}

		// Token: 0x06002352 RID: 9042 RVA: 0x000D92E5 File Offset: 0x000D82E5
		private DerBoolean(bool value)
		{
			this.value = (value ? byte.MaxValue : 0);
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06002353 RID: 9043 RVA: 0x000D92FE File Offset: 0x000D82FE
		public bool IsTrue
		{
			get
			{
				return this.value != 0;
			}
		}

		// Token: 0x06002354 RID: 9044 RVA: 0x000D930C File Offset: 0x000D830C
		internal override void Encode(DerOutputStream derOut)
		{
			derOut.WriteEncoded(1, new byte[]
			{
				this.value
			});
		}

		// Token: 0x06002355 RID: 9045 RVA: 0x000D9334 File Offset: 0x000D8334
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			DerBoolean derBoolean = asn1Object as DerBoolean;
			return derBoolean != null && this.IsTrue == derBoolean.IsTrue;
		}

		// Token: 0x06002356 RID: 9046 RVA: 0x000D935C File Offset: 0x000D835C
		protected override int Asn1GetHashCode()
		{
			return this.IsTrue.GetHashCode();
		}

		// Token: 0x06002357 RID: 9047 RVA: 0x000D9377 File Offset: 0x000D8377
		public override string ToString()
		{
			if (!this.IsTrue)
			{
				return "FALSE";
			}
			return "TRUE";
		}

		// Token: 0x04001873 RID: 6259
		private readonly byte value;

		// Token: 0x04001874 RID: 6260
		public static readonly DerBoolean False = new DerBoolean(false);

		// Token: 0x04001875 RID: 6261
		public static readonly DerBoolean True = new DerBoolean(true);
	}
}
