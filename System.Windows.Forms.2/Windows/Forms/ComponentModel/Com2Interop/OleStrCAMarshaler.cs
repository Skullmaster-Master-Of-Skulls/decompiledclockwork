using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	// Token: 0x020004B8 RID: 1208
	internal class OleStrCAMarshaler : BaseCAMarshaler
	{
		// Token: 0x06004F86 RID: 20358 RVA: 0x0014818D File Offset: 0x0014638D
		public OleStrCAMarshaler(NativeMethods.CA_STRUCT caAddr) : base(caAddr)
		{
		}

		// Token: 0x17001379 RID: 4985
		// (get) Token: 0x06004F87 RID: 20359 RVA: 0x00142D03 File Offset: 0x00140F03
		public override Type ItemType
		{
			get
			{
				return typeof(string);
			}
		}

		// Token: 0x06004F88 RID: 20360 RVA: 0x001481BF File Offset: 0x001463BF
		protected override Array CreateArray()
		{
			return new string[base.Count];
		}

		// Token: 0x06004F89 RID: 20361 RVA: 0x001481CC File Offset: 0x001463CC
		protected override object UnmarshalAndFreeOneItem(IntPtr arrayAddr, int itemIndex)
		{
			IntPtr ptr = Marshal.ReadIntPtr(arrayAddr, itemIndex * IntPtr.Size);
			string result = Marshal.PtrToStringUni(ptr);
			Marshal.FreeCoTaskMem(ptr);
			return result;
		}
	}
}
