using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x0200028E RID: 654
	[ComVisible(true)]
	public interface IDataObject
	{
		// Token: 0x0600299D RID: 10653
		object GetData(string format, bool autoConvert);

		// Token: 0x0600299E RID: 10654
		object GetData(string format);

		// Token: 0x0600299F RID: 10655
		object GetData(Type format);

		// Token: 0x060029A0 RID: 10656
		void SetData(string format, bool autoConvert, object data);

		// Token: 0x060029A1 RID: 10657
		void SetData(string format, object data);

		// Token: 0x060029A2 RID: 10658
		void SetData(Type format, object data);

		// Token: 0x060029A3 RID: 10659
		void SetData(object data);

		// Token: 0x060029A4 RID: 10660
		bool GetDataPresent(string format, bool autoConvert);

		// Token: 0x060029A5 RID: 10661
		bool GetDataPresent(string format);

		// Token: 0x060029A6 RID: 10662
		bool GetDataPresent(Type format);

		// Token: 0x060029A7 RID: 10663
		string[] GetFormats(bool autoConvert);

		// Token: 0x060029A8 RID: 10664
		string[] GetFormats();
	}
}
