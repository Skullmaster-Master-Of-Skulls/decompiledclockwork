using System;

namespace TechnoPro.Common.ClientManager.Notifications.Legacy
{
	// Token: 0x0200000C RID: 12
	public class LegacyPrintingNotificationManager : IDisposable
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00002F73 File Offset: 0x00001173
		public static LegacyPrintingNotificationManager CurrentInstance
		{
			get
			{
				LegacyPrintingNotificationManager result;
				if ((result = LegacyPrintingNotificationManager._currentInstance) == null)
				{
					result = (LegacyPrintingNotificationManager._currentInstance = new LegacyPrintingNotificationManager());
				}
				return result;
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002946 File Offset: 0x00000B46
		public void Dispose()
		{
		}

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000071 RID: 113 RVA: 0x00002F8C File Offset: 0x0000118C
		// (remove) Token: 0x06000072 RID: 114 RVA: 0x00002FC4 File Offset: 0x000011C4
		public event EventHandler<LegacyPrintLabelsEventArgs> OnLegacyPrintLabelsRequested;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06000073 RID: 115 RVA: 0x00002FFC File Offset: 0x000011FC
		// (remove) Token: 0x06000074 RID: 116 RVA: 0x00003034 File Offset: 0x00001234
		public event EventHandler<LegacyPrintControlEventArgs> OnLegacyPrintControlRequested;

		// Token: 0x06000075 RID: 117 RVA: 0x0000306C File Offset: 0x0000126C
		private void FireOnLegacyPrintLabelsRequested(string labelsString, bool showPrintPreview)
		{
			EventHandler<LegacyPrintLabelsEventArgs> onLegacyPrintLabelsRequested = this.OnLegacyPrintLabelsRequested;
			if (onLegacyPrintLabelsRequested != null)
			{
				onLegacyPrintLabelsRequested(this, new LegacyPrintLabelsEventArgs
				{
					LabelsString = labelsString,
					ShowPrintPreview = showPrintPreview
				});
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000030A0 File Offset: 0x000012A0
		private void FireOnLegacyPrintControlRequested(object headerControl, object control, string title, bool showPrintPreview)
		{
			EventHandler<LegacyPrintControlEventArgs> onLegacyPrintControlRequested = this.OnLegacyPrintControlRequested;
			if (onLegacyPrintControlRequested != null)
			{
				onLegacyPrintControlRequested(this, new LegacyPrintControlEventArgs
				{
					HeaderControl = headerControl,
					Control = control,
					ShowPrintPreview = showPrintPreview,
					Title = title
				});
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000030E0 File Offset: 0x000012E0
		public void NotifyLegacyPrintLabelsRequested(string labelsString, bool showPrintPreview)
		{
			this.FireOnLegacyPrintLabelsRequested(labelsString, showPrintPreview);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000030EA File Offset: 0x000012EA
		public void NotifyLegacyPrintControlRequested(object headerControl, object control, string title, bool showPrintPreview)
		{
			this.FireOnLegacyPrintControlRequested(headerControl, control, title, showPrintPreview);
		}

		// Token: 0x0400001E RID: 30
		private static LegacyPrintingNotificationManager _currentInstance;
	}
}
