using System;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x0200038E RID: 910
	public class Time : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x06001FAC RID: 8108 RVA: 0x000BCA4C File Offset: 0x000BBA4C
		public static Time GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return Time.GetInstance(obj.GetObject());
		}

		// Token: 0x06001FAD RID: 8109 RVA: 0x000BCA59 File Offset: 0x000BBA59
		public Time(Asn1Object time)
		{
			if (!(time is DerUtcTime) && !(time is DerGeneralizedTime))
			{
				throw new ArgumentException("unknown object passed to Time");
			}
			this.time = time;
		}

		// Token: 0x06001FAE RID: 8110 RVA: 0x000BCA84 File Offset: 0x000BBA84
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

		// Token: 0x06001FAF RID: 8111 RVA: 0x000BCAEC File Offset: 0x000BBAEC
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

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06001FB0 RID: 8112 RVA: 0x000BCB4F File Offset: 0x000BBB4F
		public string TimeString
		{
			get
			{
				if (this.time is DerUtcTime)
				{
					return ((DerUtcTime)this.time).AdjustedTimeString;
				}
				return ((DerGeneralizedTime)this.time).GetTime();
			}
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06001FB1 RID: 8113 RVA: 0x000BCB80 File Offset: 0x000BBB80
		public DateTime Date
		{
			get
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
		}

		// Token: 0x06001FB2 RID: 8114 RVA: 0x000BCBE8 File Offset: 0x000BBBE8
		public override Asn1Object ToAsn1Object()
		{
			return this.time;
		}

		// Token: 0x040015DE RID: 5598
		private readonly Asn1Object time;
	}
}
