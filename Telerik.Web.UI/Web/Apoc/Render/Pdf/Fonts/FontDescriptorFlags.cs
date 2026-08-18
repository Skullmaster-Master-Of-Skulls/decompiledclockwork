using System;
using System.Collections.Specialized;

namespace Telerik.Web.Apoc.Render.Pdf.Fonts
{
	// Token: 0x0200168A RID: 5770
	internal sealed class FontDescriptorFlags
	{
		// Token: 0x0600DEE4 RID: 57060 RVA: 0x003115C3 File Offset: 0x0030F7C3
		public FontDescriptorFlags()
		{
			this.flags = new BitVector32(0);
		}

		// Token: 0x0600DEE5 RID: 57061 RVA: 0x003115D7 File Offset: 0x0030F7D7
		public FontDescriptorFlags(int flags)
		{
			this.flags = new BitVector32(flags);
		}

		// Token: 0x17004432 RID: 17458
		// (get) Token: 0x0600DEE6 RID: 57062 RVA: 0x003115EB File Offset: 0x0030F7EB
		public int Flags
		{
			get
			{
				return this.flags.Data;
			}
		}

		// Token: 0x17004433 RID: 17459
		// (get) Token: 0x0600DEE7 RID: 57063 RVA: 0x003115F8 File Offset: 0x0030F7F8
		public bool IsFixedPitch
		{
			get
			{
				return this.flags[1];
			}
		}

		// Token: 0x17004434 RID: 17460
		// (get) Token: 0x0600DEE8 RID: 57064 RVA: 0x00311606 File Offset: 0x0030F806
		public bool IsSerif
		{
			get
			{
				return this.flags[2];
			}
		}

		// Token: 0x17004435 RID: 17461
		// (get) Token: 0x0600DEE9 RID: 57065 RVA: 0x00311614 File Offset: 0x0030F814
		public bool IsSymbolic
		{
			get
			{
				return this.flags[3];
			}
		}

		// Token: 0x17004436 RID: 17462
		// (get) Token: 0x0600DEEA RID: 57066 RVA: 0x00311622 File Offset: 0x0030F822
		public bool IsScript
		{
			get
			{
				return this.flags[4];
			}
		}

		// Token: 0x17004437 RID: 17463
		// (get) Token: 0x0600DEEB RID: 57067 RVA: 0x00311630 File Offset: 0x0030F830
		public bool IsNonSymbolic
		{
			get
			{
				return this.flags[6];
			}
		}

		// Token: 0x17004438 RID: 17464
		// (get) Token: 0x0600DEEC RID: 57068 RVA: 0x0031163E File Offset: 0x0030F83E
		public bool IsItalic
		{
			get
			{
				return this.flags[7];
			}
		}

		// Token: 0x17004439 RID: 17465
		// (get) Token: 0x0600DEED RID: 57069 RVA: 0x0031164C File Offset: 0x0030F84C
		public bool IsAllCap
		{
			get
			{
				return this.flags[17];
			}
		}

		// Token: 0x1700443A RID: 17466
		// (get) Token: 0x0600DEEE RID: 57070 RVA: 0x0031165B File Offset: 0x0030F85B
		public bool IsSmallCap
		{
			get
			{
				return this.flags[18];
			}
		}

		// Token: 0x1700443B RID: 17467
		// (get) Token: 0x0600DEEF RID: 57071 RVA: 0x0031166A File Offset: 0x0030F86A
		public bool IsForceBold
		{
			get
			{
				return this.flags[19];
			}
		}

		// Token: 0x04004035 RID: 16437
		private BitVector32 flags;

		// Token: 0x0200168B RID: 5771
		internal enum FontDescriptorFlagsEnum
		{
			// Token: 0x04004037 RID: 16439
			FixedPitch = 1,
			// Token: 0x04004038 RID: 16440
			Serif,
			// Token: 0x04004039 RID: 16441
			Symbolic,
			// Token: 0x0400403A RID: 16442
			Script,
			// Token: 0x0400403B RID: 16443
			Nonsymbolic = 6,
			// Token: 0x0400403C RID: 16444
			Italic,
			// Token: 0x0400403D RID: 16445
			AllCap = 17,
			// Token: 0x0400403E RID: 16446
			SmallCap,
			// Token: 0x0400403F RID: 16447
			ForceBold
		}
	}
}
