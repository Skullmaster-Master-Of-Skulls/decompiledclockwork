using System;

namespace System.Net.Http.Headers
{
	// Token: 0x02000048 RID: 72
	[__DynamicallyInvokable]
	public sealed class TransferCodingWithQualityHeaderValue : TransferCodingHeaderValue, ICloneable
	{
		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x0000E9CE File Offset: 0x0000CBCE
		// (set) Token: 0x060003E4 RID: 996 RVA: 0x0000E9DB File Offset: 0x0000CBDB
		[__DynamicallyInvokable]
		public double? Quality
		{
			[__DynamicallyInvokable]
			get
			{
				return HeaderUtilities.GetQuality(base.Parameters);
			}
			[__DynamicallyInvokable]
			set
			{
				HeaderUtilities.SetQuality(base.Parameters, value);
			}
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0000E9E9 File Offset: 0x0000CBE9
		internal TransferCodingWithQualityHeaderValue()
		{
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000E9F1 File Offset: 0x0000CBF1
		[__DynamicallyInvokable]
		public TransferCodingWithQualityHeaderValue(string value) : base(value)
		{
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0000E9FA File Offset: 0x0000CBFA
		[__DynamicallyInvokable]
		public TransferCodingWithQualityHeaderValue(string value, double quality) : base(value)
		{
			this.Quality = new double?(quality);
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0000EA0F File Offset: 0x0000CC0F
		private TransferCodingWithQualityHeaderValue(TransferCodingWithQualityHeaderValue source) : base(source)
		{
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000EA18 File Offset: 0x0000CC18
		object ICloneable.Clone()
		{
			return new TransferCodingWithQualityHeaderValue(this);
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0000EA20 File Offset: 0x0000CC20
		[__DynamicallyInvokable]
		public new static TransferCodingWithQualityHeaderValue Parse(string input)
		{
			int num = 0;
			return (TransferCodingWithQualityHeaderValue)TransferCodingHeaderParser.SingleValueWithQualityParser.ParseValue(input, null, ref num);
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0000EA44 File Offset: 0x0000CC44
		[__DynamicallyInvokable]
		public static bool TryParse(string input, out TransferCodingWithQualityHeaderValue parsedValue)
		{
			int num = 0;
			parsedValue = null;
			object obj;
			if (TransferCodingHeaderParser.SingleValueWithQualityParser.TryParseValue(input, null, ref num, out obj))
			{
				parsedValue = (TransferCodingWithQualityHeaderValue)obj;
				return true;
			}
			return false;
		}
	}
}
