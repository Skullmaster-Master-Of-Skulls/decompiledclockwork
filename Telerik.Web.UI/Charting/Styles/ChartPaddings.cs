using System;
using System.ComponentModel;
using System.Globalization;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017AB RID: 6059
	[TypeConverter(typeof(PaddingsConverter))]
	public class ChartPaddings : LayoutDecoratorBase
	{
		// Token: 0x0600EBED RID: 60397 RVA: 0x0035A940 File Offset: 0x00358B40
		public ChartPaddings(object containerObject) : base(containerObject)
		{
		}

		// Token: 0x0600EBEE RID: 60398 RVA: 0x0035A949 File Offset: 0x00358B49
		public ChartPaddings()
		{
		}

		// Token: 0x0600EBEF RID: 60399 RVA: 0x0035A951 File Offset: 0x00358B51
		public ChartPaddings(object containerObject, Unit top, Unit right, Unit bottom, Unit left) : base(containerObject, top, right, bottom, left)
		{
		}

		// Token: 0x0600EBF0 RID: 60400 RVA: 0x0035A960 File Offset: 0x00358B60
		public ChartPaddings(Unit top, Unit right, Unit bottom, Unit left) : base(top, right, bottom, left)
		{
		}

		// Token: 0x0600EBF1 RID: 60401 RVA: 0x0035A96D File Offset: 0x00358B6D
		public ChartPaddings(int top, int right, int bottom, int left) : base(top, right, bottom, left)
		{
		}

		// Token: 0x0600EBF2 RID: 60402 RVA: 0x0035A97A File Offset: 0x00358B7A
		public ChartPaddings(Unit margin) : base(margin)
		{
		}

		// Token: 0x0600EBF3 RID: 60403 RVA: 0x0035A983 File Offset: 0x00358B83
		public static implicit operator ChartPaddings(string value)
		{
			return ChartPaddings.Parse(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x0600EBF4 RID: 60404 RVA: 0x0035A990 File Offset: 0x00358B90
		public static ChartPaddings Parse(string value)
		{
			return ChartPaddings.Parse(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x0600EBF5 RID: 60405 RVA: 0x0035A99D File Offset: 0x00358B9D
		public static ChartPaddings Parse(string value, CultureInfo culture)
		{
			return (ChartPaddings)new PaddingsConverter().ConvertFromInvariantString(value);
		}
	}
}
