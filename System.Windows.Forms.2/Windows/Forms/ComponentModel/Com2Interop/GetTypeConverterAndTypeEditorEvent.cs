using System;
using System.ComponentModel;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	// Token: 0x020004AE RID: 1198
	internal class GetTypeConverterAndTypeEditorEvent : EventArgs
	{
		// Token: 0x06004F4A RID: 20298 RVA: 0x0014645E File Offset: 0x0014465E
		public GetTypeConverterAndTypeEditorEvent(TypeConverter typeConverter, object typeEditor)
		{
			this.typeEditor = typeEditor;
			this.typeConverter = typeConverter;
		}

		// Token: 0x17001373 RID: 4979
		// (get) Token: 0x06004F4B RID: 20299 RVA: 0x00146474 File Offset: 0x00144674
		// (set) Token: 0x06004F4C RID: 20300 RVA: 0x0014647C File Offset: 0x0014467C
		public TypeConverter TypeConverter
		{
			get
			{
				return this.typeConverter;
			}
			set
			{
				this.typeConverter = value;
			}
		}

		// Token: 0x17001374 RID: 4980
		// (get) Token: 0x06004F4D RID: 20301 RVA: 0x00146485 File Offset: 0x00144685
		// (set) Token: 0x06004F4E RID: 20302 RVA: 0x0014648D File Offset: 0x0014468D
		public object TypeEditor
		{
			get
			{
				return this.typeEditor;
			}
			set
			{
				this.typeEditor = value;
			}
		}

		// Token: 0x04003450 RID: 13392
		private TypeConverter typeConverter;

		// Token: 0x04003451 RID: 13393
		private object typeEditor;
	}
}
