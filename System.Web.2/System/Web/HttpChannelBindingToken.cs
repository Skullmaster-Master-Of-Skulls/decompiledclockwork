using System;
using System.Security.Authentication.ExtendedProtection;

namespace System.Web
{
	// Token: 0x0200008D RID: 141
	internal sealed class HttpChannelBindingToken : ChannelBinding
	{
		// Token: 0x060008A7 RID: 2215 RVA: 0x000133A5 File Offset: 0x000115A5
		internal HttpChannelBindingToken(IntPtr token, int tokenSize)
		{
			base.SetHandle(token);
			this._size = tokenSize;
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x000133BB File Offset: 0x000115BB
		protected override bool ReleaseHandle()
		{
			base.SetHandle(IntPtr.Zero);
			this._size = 0;
			return true;
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x060008A9 RID: 2217 RVA: 0x000133D0 File Offset: 0x000115D0
		public override int Size
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x04000321 RID: 801
		private int _size;
	}
}
