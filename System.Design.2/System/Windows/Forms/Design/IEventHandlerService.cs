using System;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002EF RID: 751
	internal interface IEventHandlerService
	{
		// Token: 0x1400004E RID: 78
		// (add) Token: 0x06001E08 RID: 7688
		// (remove) Token: 0x06001E09 RID: 7689
		event EventHandler EventHandlerChanged;

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06001E0A RID: 7690
		Control FocusWindow { get; }

		// Token: 0x06001E0B RID: 7691
		object GetHandler(Type handlerType);

		// Token: 0x06001E0C RID: 7692
		void PopHandler(object handler);

		// Token: 0x06001E0D RID: 7693
		void PushHandler(object handler);
	}
}
