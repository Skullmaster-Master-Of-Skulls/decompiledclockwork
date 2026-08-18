using System;
using System.Globalization;
using NLog.Config;
using NLog.Layouts;

namespace NLog.Targets
{
	// Token: 0x02000164 RID: 356
	[NLogConfigurationItem]
	public class MethodCallParameter
	{
		// Token: 0x06000D93 RID: 3475 RVA: 0x00020CE2 File Offset: 0x0001EEE2
		public MethodCallParameter()
		{
			this.Type = typeof(string);
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x00020CFA File Offset: 0x0001EEFA
		public MethodCallParameter(Layout layout)
		{
			this.Type = typeof(string);
			this.Layout = layout;
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x00020D19 File Offset: 0x0001EF19
		public MethodCallParameter(string parameterName, Layout layout)
		{
			this.Type = typeof(string);
			this.Name = parameterName;
			this.Layout = layout;
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x00020D3F File Offset: 0x0001EF3F
		public MethodCallParameter(string name, Layout layout, Type type)
		{
			this.Type = type;
			this.Name = name;
			this.Layout = layout;
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000D97 RID: 3479 RVA: 0x00020D5C File Offset: 0x0001EF5C
		// (set) Token: 0x06000D98 RID: 3480 RVA: 0x00020D64 File Offset: 0x0001EF64
		public string Name { get; set; }

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000D99 RID: 3481 RVA: 0x00020D6D File Offset: 0x0001EF6D
		// (set) Token: 0x06000D9A RID: 3482 RVA: 0x00020D75 File Offset: 0x0001EF75
		public Type Type { get; set; }

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000D9B RID: 3483 RVA: 0x00020D7E File Offset: 0x0001EF7E
		// (set) Token: 0x06000D9C RID: 3484 RVA: 0x00020D86 File Offset: 0x0001EF86
		[RequiredParameter]
		public Layout Layout { get; set; }

		// Token: 0x06000D9D RID: 3485 RVA: 0x00020D8F File Offset: 0x0001EF8F
		internal object GetValue(LogEventInfo logEvent)
		{
			return Convert.ChangeType(this.Layout.Render(logEvent), this.Type, CultureInfo.InvariantCulture);
		}
	}
}
