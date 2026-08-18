using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using NLog.Layouts;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000CA RID: 202
	[LayoutRenderer("counter")]
	public class CounterLayoutRenderer : LayoutRenderer
	{
		// Token: 0x060005E6 RID: 1510 RVA: 0x0000D38F File Offset: 0x0000B58F
		public CounterLayoutRenderer()
		{
			this.Increment = 1;
			this.Value = 1;
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060005E7 RID: 1511 RVA: 0x0000D3A5 File Offset: 0x0000B5A5
		// (set) Token: 0x060005E8 RID: 1512 RVA: 0x0000D3AD File Offset: 0x0000B5AD
		[DefaultValue(1)]
		public int Value { get; set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060005E9 RID: 1513 RVA: 0x0000D3B6 File Offset: 0x0000B5B6
		// (set) Token: 0x060005EA RID: 1514 RVA: 0x0000D3BE File Offset: 0x0000B5BE
		[DefaultValue(1)]
		public int Increment { get; set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060005EB RID: 1515 RVA: 0x0000D3C7 File Offset: 0x0000B5C7
		// (set) Token: 0x060005EC RID: 1516 RVA: 0x0000D3CF File Offset: 0x0000B5CF
		public Layout Sequence { get; set; }

		// Token: 0x060005ED RID: 1517 RVA: 0x0000D3D8 File Offset: 0x0000B5D8
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			int num;
			if (this.Sequence != null)
			{
				num = CounterLayoutRenderer.GetNextSequenceValue(this.Sequence.Render(logEvent), this.Value, this.Increment);
			}
			else
			{
				num = this.Value;
				this.Value += this.Increment;
			}
			builder.Append(num.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0000D43C File Offset: 0x0000B63C
		private static int GetNextSequenceValue(string sequenceName, int defaultValue, int increment)
		{
			int result;
			lock (CounterLayoutRenderer.sequences)
			{
				int num;
				if (!CounterLayoutRenderer.sequences.TryGetValue(sequenceName, out num))
				{
					num = defaultValue;
				}
				int num2 = num;
				num += increment;
				CounterLayoutRenderer.sequences[sequenceName] = num;
				result = num2;
			}
			return result;
		}

		// Token: 0x04000169 RID: 361
		private static Dictionary<string, int> sequences = new Dictionary<string, int>();
	}
}
