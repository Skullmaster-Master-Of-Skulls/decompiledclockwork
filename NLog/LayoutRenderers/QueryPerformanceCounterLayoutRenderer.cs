using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000EA RID: 234
	[LayoutRenderer("qpc")]
	public class QueryPerformanceCounterLayoutRenderer : LayoutRenderer
	{
		// Token: 0x060006AF RID: 1711 RVA: 0x0000F00A File Offset: 0x0000D20A
		public QueryPerformanceCounterLayoutRenderer()
		{
			this.Normalize = true;
			this.Difference = false;
			this.Precision = 4;
			this.AlignDecimalPoint = true;
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060006B0 RID: 1712 RVA: 0x0000F03D File Offset: 0x0000D23D
		// (set) Token: 0x060006B1 RID: 1713 RVA: 0x0000F045 File Offset: 0x0000D245
		[DefaultValue(true)]
		public bool Normalize { get; set; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x0000F04E File Offset: 0x0000D24E
		// (set) Token: 0x060006B3 RID: 1715 RVA: 0x0000F056 File Offset: 0x0000D256
		[DefaultValue(false)]
		public bool Difference { get; set; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x0000F05F File Offset: 0x0000D25F
		// (set) Token: 0x060006B5 RID: 1717 RVA: 0x0000F06A File Offset: 0x0000D26A
		[DefaultValue(true)]
		public bool Seconds
		{
			get
			{
				return !this.raw;
			}
			set
			{
				this.raw = !value;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060006B6 RID: 1718 RVA: 0x0000F076 File Offset: 0x0000D276
		// (set) Token: 0x060006B7 RID: 1719 RVA: 0x0000F07E File Offset: 0x0000D27E
		[DefaultValue(4)]
		public int Precision { get; set; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060006B8 RID: 1720 RVA: 0x0000F087 File Offset: 0x0000D287
		// (set) Token: 0x060006B9 RID: 1721 RVA: 0x0000F08F File Offset: 0x0000D28F
		[DefaultValue(true)]
		public bool AlignDecimalPoint { get; set; }

		// Token: 0x060006BA RID: 1722 RVA: 0x0000F098 File Offset: 0x0000D298
		protected override void InitializeLayoutRenderer()
		{
			base.InitializeLayoutRenderer();
			ulong num;
			if (!NativeMethods.QueryPerformanceFrequency(out num))
			{
				throw new InvalidOperationException("Cannot determine high-performance counter frequency.");
			}
			ulong num2;
			if (!NativeMethods.QueryPerformanceCounter(out num2))
			{
				throw new InvalidOperationException("Cannot determine high-performance counter value.");
			}
			this.frequency = num;
			this.firstQpcValue = num2;
			this.lastQpcValue = num2;
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x0000F0EC File Offset: 0x0000D2EC
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			ulong num;
			if (!NativeMethods.QueryPerformanceCounter(out num))
			{
				return;
			}
			ulong num2 = num;
			if (this.Difference)
			{
				num -= this.lastQpcValue;
			}
			else if (this.Normalize)
			{
				num -= this.firstQpcValue;
			}
			this.lastQpcValue = num2;
			string text;
			if (this.Seconds)
			{
				double value = Math.Round(num / this.frequency, this.Precision);
				text = Convert.ToString(value, CultureInfo.InvariantCulture);
				if (this.AlignDecimalPoint)
				{
					int num3 = text.IndexOf('.');
					if (num3 == -1)
					{
						text = text + "." + new string('0', this.Precision);
					}
					else
					{
						text += new string('0', this.Precision - (text.Length - 1 - num3));
					}
				}
			}
			else
			{
				text = Convert.ToString(num, CultureInfo.InvariantCulture);
			}
			builder.Append(text);
		}

		// Token: 0x040001DD RID: 477
		private bool raw;

		// Token: 0x040001DE RID: 478
		private ulong firstQpcValue;

		// Token: 0x040001DF RID: 479
		private ulong lastQpcValue;

		// Token: 0x040001E0 RID: 480
		private double frequency = 1.0;
	}
}
