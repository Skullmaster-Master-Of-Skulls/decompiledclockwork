using System;
using System.ComponentModel;
using System.Text;

namespace NLog.Targets
{
	// Token: 0x02000150 RID: 336
	[Target("Console")]
	public sealed class ConsoleTarget : TargetWithLayoutHeaderAndFooter
	{
		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000BFC RID: 3068 RVA: 0x0001BED0 File Offset: 0x0001A0D0
		// (set) Token: 0x06000BFD RID: 3069 RVA: 0x0001BED8 File Offset: 0x0001A0D8
		[DefaultValue(false)]
		public bool Error { get; set; }

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000BFE RID: 3070 RVA: 0x0001BEE1 File Offset: 0x0001A0E1
		// (set) Token: 0x06000BFF RID: 3071 RVA: 0x0001BEE8 File Offset: 0x0001A0E8
		public Encoding Encoding
		{
			get
			{
				return Console.OutputEncoding;
			}
			set
			{
				Console.OutputEncoding = value;
			}
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x0001BEF0 File Offset: 0x0001A0F0
		public ConsoleTarget()
		{
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x0001BEF8 File Offset: 0x0001A0F8
		public ConsoleTarget(string name) : this()
		{
			base.Name = name;
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x0001BF07 File Offset: 0x0001A107
		protected override void InitializeTarget()
		{
			base.InitializeTarget();
			if (base.Header != null)
			{
				this.Output(base.Header.Render(LogEventInfo.CreateNullEvent()));
			}
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x0001BF2D File Offset: 0x0001A12D
		protected override void CloseTarget()
		{
			if (base.Footer != null)
			{
				this.Output(base.Footer.Render(LogEventInfo.CreateNullEvent()));
			}
			base.CloseTarget();
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x0001BF53 File Offset: 0x0001A153
		protected override void Write(LogEventInfo logEvent)
		{
			this.Output(this.Layout.Render(logEvent));
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x0001BF67 File Offset: 0x0001A167
		private void Output(string textLine)
		{
			if (this.Error)
			{
				Console.Error.WriteLine(textLine);
				return;
			}
			Console.Out.WriteLine(textLine);
		}
	}
}
