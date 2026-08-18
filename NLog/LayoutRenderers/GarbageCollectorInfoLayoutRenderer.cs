using System;
using System.ComponentModel;
using System.Text;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000D1 RID: 209
	[LayoutRenderer("gc")]
	public class GarbageCollectorInfoLayoutRenderer : LayoutRenderer
	{
		// Token: 0x0600062A RID: 1578 RVA: 0x0000DD00 File Offset: 0x0000BF00
		public GarbageCollectorInfoLayoutRenderer()
		{
			this.Property = GarbageCollectorProperty.TotalMemory;
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x0000DD0F File Offset: 0x0000BF0F
		// (set) Token: 0x0600062C RID: 1580 RVA: 0x0000DD17 File Offset: 0x0000BF17
		[DefaultValue("TotalMemory")]
		public GarbageCollectorProperty Property { get; set; }

		// Token: 0x0600062D RID: 1581 RVA: 0x0000DD20 File Offset: 0x0000BF20
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			object value = null;
			switch (this.Property)
			{
			case GarbageCollectorProperty.TotalMemory:
				value = GC.GetTotalMemory(false);
				break;
			case GarbageCollectorProperty.TotalMemoryForceCollection:
				value = GC.GetTotalMemory(true);
				break;
			case GarbageCollectorProperty.CollectionCount0:
				value = GC.CollectionCount(0);
				break;
			case GarbageCollectorProperty.CollectionCount1:
				value = GC.CollectionCount(1);
				break;
			case GarbageCollectorProperty.CollectionCount2:
				value = GC.CollectionCount(2);
				break;
			case GarbageCollectorProperty.MaxGeneration:
				value = GC.MaxGeneration;
				break;
			}
			IFormatProvider formatProvider = base.GetFormatProvider(logEvent, null);
			builder.Append(Convert.ToString(value, formatProvider));
		}
	}
}
