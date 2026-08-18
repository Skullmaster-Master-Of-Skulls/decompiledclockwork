using System;

namespace System.Net.Http.Headers
{
	// Token: 0x02000039 RID: 57
	[__DynamicallyInvokable]
	public sealed class MediaTypeWithQualityHeaderValue : MediaTypeHeaderValue, ICloneable
	{
		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600033A RID: 826 RVA: 0x0000C943 File Offset: 0x0000AB43
		// (set) Token: 0x0600033B RID: 827 RVA: 0x0000C950 File Offset: 0x0000AB50
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

		// Token: 0x0600033C RID: 828 RVA: 0x0000C95E File Offset: 0x0000AB5E
		internal MediaTypeWithQualityHeaderValue()
		{
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000C966 File Offset: 0x0000AB66
		[__DynamicallyInvokable]
		public MediaTypeWithQualityHeaderValue(string mediaType) : base(mediaType)
		{
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000C96F File Offset: 0x0000AB6F
		[__DynamicallyInvokable]
		public MediaTypeWithQualityHeaderValue(string mediaType, double quality) : base(mediaType)
		{
			this.Quality = new double?(quality);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000C984 File Offset: 0x0000AB84
		private MediaTypeWithQualityHeaderValue(MediaTypeWithQualityHeaderValue source) : base(source)
		{
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000C98D File Offset: 0x0000AB8D
		object ICloneable.Clone()
		{
			return new MediaTypeWithQualityHeaderValue(this);
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000C998 File Offset: 0x0000AB98
		[__DynamicallyInvokable]
		public new static MediaTypeWithQualityHeaderValue Parse(string input)
		{
			int num = 0;
			return (MediaTypeWithQualityHeaderValue)MediaTypeHeaderParser.SingleValueWithQualityParser.ParseValue(input, null, ref num);
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000C9BC File Offset: 0x0000ABBC
		[__DynamicallyInvokable]
		public static bool TryParse(string input, out MediaTypeWithQualityHeaderValue parsedValue)
		{
			int num = 0;
			parsedValue = null;
			object obj;
			if (MediaTypeHeaderParser.SingleValueWithQualityParser.TryParseValue(input, null, ref num, out obj))
			{
				parsedValue = (MediaTypeWithQualityHeaderValue)obj;
				return true;
			}
			return false;
		}
	}
}
