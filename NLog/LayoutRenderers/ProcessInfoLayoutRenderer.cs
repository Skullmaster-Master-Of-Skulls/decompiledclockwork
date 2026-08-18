using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using NLog.Config;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000E6 RID: 230
	[LayoutRenderer("processinfo")]
	public class ProcessInfoLayoutRenderer : LayoutRenderer
	{
		// Token: 0x060006A2 RID: 1698 RVA: 0x0000ED8F File Offset: 0x0000CF8F
		public ProcessInfoLayoutRenderer()
		{
			this.Property = ProcessInfoProperty.Id;
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060006A3 RID: 1699 RVA: 0x0000ED9E File Offset: 0x0000CF9E
		// (set) Token: 0x060006A4 RID: 1700 RVA: 0x0000EDA6 File Offset: 0x0000CFA6
		[DefaultValue("Id")]
		[DefaultParameter]
		public ProcessInfoProperty Property { get; set; }

		// Token: 0x060006A5 RID: 1701 RVA: 0x0000EDB0 File Offset: 0x0000CFB0
		protected override void InitializeLayoutRenderer()
		{
			base.InitializeLayoutRenderer();
			this.propertyInfo = typeof(Process).GetProperty(this.Property.ToString());
			if (this.propertyInfo == null)
			{
				throw new ArgumentException("Property '" + this.propertyInfo + "' not found in System.Diagnostics.Process");
			}
			this.process = Process.GetCurrentProcess();
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0000EE1C File Offset: 0x0000D01C
		protected override void CloseLayoutRenderer()
		{
			if (this.process != null)
			{
				this.process.Close();
				this.process = null;
			}
			base.CloseLayoutRenderer();
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x0000EE40 File Offset: 0x0000D040
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			if (this.propertyInfo != null)
			{
				IFormatProvider formatProvider = base.GetFormatProvider(logEvent, null);
				builder.Append(Convert.ToString(this.propertyInfo.GetValue(this.process, null), formatProvider));
			}
		}

		// Token: 0x040001B1 RID: 433
		private Process process;

		// Token: 0x040001B2 RID: 434
		private PropertyInfo propertyInfo;
	}
}
