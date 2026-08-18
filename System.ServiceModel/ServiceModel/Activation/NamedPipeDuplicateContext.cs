using System;
using System.Runtime.Serialization;

namespace System.ServiceModel.Activation
{
	// Token: 0x020005C0 RID: 1472
	[DataContract]
	internal class NamedPipeDuplicateContext : DuplicateContext
	{
		// Token: 0x0600397E RID: 14718 RVA: 0x000DE6B8 File Offset: 0x000DC8B8
		public NamedPipeDuplicateContext(IntPtr handle, Uri via, byte[] readData) : base(via, readData)
		{
			this.handle = handle;
		}

		// Token: 0x17000D84 RID: 3460
		// (get) Token: 0x0600397F RID: 14719 RVA: 0x000DE6C9 File Offset: 0x000DC8C9
		public IntPtr Handle
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x040029EB RID: 10731
		[DataMember]
		private IntPtr handle;
	}
}
