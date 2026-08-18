using System;
using System.ComponentModel;
using System.Globalization;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017A5 RID: 6053
	[TypeConverter(typeof(MarginsConverter))]
	[DesignTimeVisible(true)]
	public class ChartMargins : LayoutDecoratorBase
	{
		// Token: 0x0600EBCC RID: 60364 RVA: 0x0035A759 File Offset: 0x00358959
		public ChartMargins(object containerObject) : base(containerObject)
		{
		}

		// Token: 0x0600EBCD RID: 60365 RVA: 0x0035A762 File Offset: 0x00358962
		public ChartMargins()
		{
		}

		// Token: 0x0600EBCE RID: 60366 RVA: 0x0035A76A File Offset: 0x0035896A
		public ChartMargins(object containerObject, Unit top, Unit right, Unit bottom, Unit left) : base(containerObject, top, right, bottom, left)
		{
		}

		// Token: 0x0600EBCF RID: 60367 RVA: 0x0035A779 File Offset: 0x00358979
		public ChartMargins(Unit top, Unit right, Unit bottom, Unit left) : base(top, right, bottom, left)
		{
		}

		// Token: 0x0600EBD0 RID: 60368 RVA: 0x0035A786 File Offset: 0x00358986
		public ChartMargins(int top, int right, int bottom, int left) : base(top, right, bottom, left)
		{
		}

		// Token: 0x0600EBD1 RID: 60369 RVA: 0x0035A793 File Offset: 0x00358993
		public ChartMargins(Unit margin) : base(margin)
		{
		}

		// Token: 0x0600EBD2 RID: 60370 RVA: 0x0035A79C File Offset: 0x0035899C
		public static implicit operator ChartMargins(string value)
		{
			return ChartMargins.Parse(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x0600EBD3 RID: 60371 RVA: 0x0035A7A9 File Offset: 0x003589A9
		public static ChartMargins Parse(string value)
		{
			return ChartMargins.Parse(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x0600EBD4 RID: 60372 RVA: 0x0035A7B6 File Offset: 0x003589B6
		public static ChartMargins Parse(string value, CultureInfo culture)
		{
			return (ChartMargins)new MarginsConverter().ConvertFromInvariantString(value);
		}
	}
}
