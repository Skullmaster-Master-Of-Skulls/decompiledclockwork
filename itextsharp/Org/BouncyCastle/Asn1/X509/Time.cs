using System;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000200 RID: 512
	public class Time : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x060013C4 RID: 5060 RVA: 0x000721B2 File Offset: 0x000711B2
		public static Time GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return Time.GetInstance(obj.GetObject());
		}

		// Token: 0x060013C5 RID: 5061 RVA: 0x000721BF File Offset: 0x000711BF
		public Time(Asn1Object time)
		{
			if (time == null)
			{
				throw new ArgumentNullException("time");
			}
			if (!(time is DerUtcTime) && !(time is DerGeneralizedTime))
			{
				throw new ArgumentException("unknown object passed to Time");
			}
			this.time = time;
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x000721F8 File Offset: 0x000711F8
		public Time(DateTime date)
		{
			string text = date.ToString("yyyyMMddHHmmss") + "Z";
			int num = int.Parse(text.Substring(0, 4));
			if (num < 1950 || num > 2049)
			{
				this.time = new DerGeneralizedTime(text);
				return;
			}
			this.time = new DerUtcTime(text.Substring(2));
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x00072260 File Offset: 0x00071260
		public static Time GetInstance(object obj)
		{
			if (obj is Time)
			{
				return (Time)obj;
			}
			if (obj is DerUtcTime)
			{
				return new Time((DerUtcTime)obj);
			}
			if (obj is DerGeneralizedTime)
			{
				return new Time((DerGeneralizedTime)obj);
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x060013C8 RID: 5064 RVA: 0x000722C3 File Offset: 0x000712C3
		public string GetTime()
		{
			if (this.time is DerUtcTime)
			{
				return ((DerUtcTime)this.time).AdjustedTimeString;
			}
			return ((DerGeneralizedTime)this.time).GetTime();
		}

		// Token: 0x060013C9 RID: 5065 RVA: 0x000722F4 File Offset: 0x000712F4
		public DateTime ToDateTime()
		{
			DateTime result;
			try
			{
				if (this.time is DerUtcTime)
				{
					result = ((DerUtcTime)this.time).ToAdjustedDateTime();
				}
				else
				{
					result = ((DerGeneralizedTime)this.time).ToDateTime();
				}
			}
			catch (FormatException ex)
			{
				throw new InvalidOperationException("invalid date string: " + ex.Message);
			}
			return result;
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x0007235C File Offset: 0x0007135C
		public override Asn1Object ToAsn1Object()
		{
			return this.time;
		}

		// Token: 0x060013CB RID: 5067 RVA: 0x00072364 File Offset: 0x00071364
		public override string ToString()
		{
			return this.GetTime();
		}

		// Token: 0x04000DB8 RID: 3512
		internal Asn1Object time;
	}
}
