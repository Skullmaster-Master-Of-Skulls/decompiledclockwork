using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	// Token: 0x020004B7 RID: 1207
	internal class Int32CAMarshaler : BaseCAMarshaler
	{
		// Token: 0x06004F82 RID: 20354 RVA: 0x0014818D File Offset: 0x0014638D
		public Int32CAMarshaler(NativeMethods.CA_STRUCT caStruct) : base(caStruct)
		{
		}

		// Token: 0x17001378 RID: 4984
		// (get) Token: 0x06004F83 RID: 20355 RVA: 0x00148196 File Offset: 0x00146396
		public override Type ItemType
		{
			get
			{
				return typeof(int);
			}
		}

		// Token: 0x06004F84 RID: 20356 RVA: 0x001481A2 File Offset: 0x001463A2
		protected override Array CreateArray()
		{
			return new int[base.Count];
		}

		// Token: 0x06004F85 RID: 20357 RVA: 0x001481AF File Offset: 0x001463AF
		protected override object UnmarshalAndFreeOneItem(IntPtr arrayAddr, int itemIndex)
		{
			return Marshal.ReadInt32(arrayAddr, itemIndex * 4);
		}
	}
}
