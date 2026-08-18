using System;
using System.Runtime.InteropServices;

namespace System.IdentityModel
{
	// Token: 0x020000A3 RID: 163
	internal class LifeSpan
	{
		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x000131F8 File Offset: 0x000113F8
		internal DateTime EffectiveTimeUtc
		{
			get
			{
				return this.effectiveTimeUtc;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x00013200 File Offset: 0x00011400
		internal DateTime ExpiryTimeUtc
		{
			get
			{
				return this.expiryTimeUtc;
			}
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00013208 File Offset: 0x00011408
		internal unsafe LifeSpan(byte[] buffer)
		{
			fixed (byte* ptr = &buffer[0])
			{
				byte* value = ptr;
				IntPtr ptr2 = new IntPtr((void*)value);
				LifeSpan_Struct lifeSpan_Struct = (LifeSpan_Struct)Marshal.PtrToStructure(ptr2, typeof(LifeSpan_Struct));
				this.effectiveTimeUtc = DateTime.FromFileTimeUtc(lifeSpan_Struct.start) + (DateTime.UtcNow - DateTime.Now);
				this.expiryTimeUtc = DateTime.FromFileTimeUtc(lifeSpan_Struct.end) + (DateTime.UtcNow - DateTime.Now);
			}
		}

		// Token: 0x040004A1 RID: 1185
		private DateTime effectiveTimeUtc;

		// Token: 0x040004A2 RID: 1186
		private DateTime expiryTimeUtc;
	}
}
