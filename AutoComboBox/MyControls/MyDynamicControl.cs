using System;

namespace AutoComboBox.MyControls
{
	// Token: 0x0200000E RID: 14
	public interface MyDynamicControl
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600003F RID: 63
		bool FilledIn { get; }

		// Token: 0x06000040 RID: 64
		string ToString();

		// Token: 0x06000041 RID: 65
		void FromString(string s);

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000042 RID: 66
		object ReportObject { get; }

		// Token: 0x06000043 RID: 67
		void Refresh();
	}
}
