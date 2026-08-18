using System;
using System.Collections.Generic;
using System.Text;
using NLog.Config;

namespace NLog.Layouts
{
	// Token: 0x0200011A RID: 282
	[Layout("CompoundLayout")]
	public class CompoundLayout : Layout
	{
		// Token: 0x060007CD RID: 1997 RVA: 0x000117F1 File Offset: 0x0000F9F1
		public CompoundLayout()
		{
			this.Layouts = new List<Layout>();
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060007CE RID: 1998 RVA: 0x00011804 File Offset: 0x0000FA04
		// (set) Token: 0x060007CF RID: 1999 RVA: 0x0001180C File Offset: 0x0000FA0C
		[ArrayParameter(typeof(Layout), "layout")]
		public IList<Layout> Layouts { get; private set; }

		// Token: 0x060007D0 RID: 2000 RVA: 0x00011818 File Offset: 0x0000FA18
		protected override void InitializeLayout()
		{
			base.InitializeLayout();
			foreach (Layout layout in this.Layouts)
			{
				layout.Initialize(base.LoggingConfiguration);
			}
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x00011870 File Offset: 0x0000FA70
		protected override string GetFormattedMessage(LogEventInfo logEvent)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (Layout layout in this.Layouts)
			{
				stringBuilder.Append(layout.Render(logEvent));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x000118D0 File Offset: 0x0000FAD0
		protected override void CloseLayout()
		{
			foreach (Layout layout in this.Layouts)
			{
				layout.Close();
			}
			base.CloseLayout();
		}
	}
}
