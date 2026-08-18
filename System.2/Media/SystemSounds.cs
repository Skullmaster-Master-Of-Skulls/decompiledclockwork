using System;
using System.Security.Permissions;

namespace System.Media
{
	// Token: 0x020003A5 RID: 933
	[HostProtection(SecurityAction.LinkDemand, UI = true)]
	public sealed class SystemSounds
	{
		// Token: 0x060022E2 RID: 8930 RVA: 0x000A6201 File Offset: 0x000A4401
		private SystemSounds()
		{
		}

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x060022E3 RID: 8931 RVA: 0x000A6209 File Offset: 0x000A4409
		public static SystemSound Asterisk
		{
			get
			{
				if (SystemSounds.asterisk == null)
				{
					SystemSounds.asterisk = new SystemSound(64);
				}
				return SystemSounds.asterisk;
			}
		}

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x060022E4 RID: 8932 RVA: 0x000A6229 File Offset: 0x000A4429
		public static SystemSound Beep
		{
			get
			{
				if (SystemSounds.beep == null)
				{
					SystemSounds.beep = new SystemSound(0);
				}
				return SystemSounds.beep;
			}
		}

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x060022E5 RID: 8933 RVA: 0x000A6248 File Offset: 0x000A4448
		public static SystemSound Exclamation
		{
			get
			{
				if (SystemSounds.exclamation == null)
				{
					SystemSounds.exclamation = new SystemSound(48);
				}
				return SystemSounds.exclamation;
			}
		}

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x060022E6 RID: 8934 RVA: 0x000A6268 File Offset: 0x000A4468
		public static SystemSound Hand
		{
			get
			{
				if (SystemSounds.hand == null)
				{
					SystemSounds.hand = new SystemSound(16);
				}
				return SystemSounds.hand;
			}
		}

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x060022E7 RID: 8935 RVA: 0x000A6288 File Offset: 0x000A4488
		public static SystemSound Question
		{
			get
			{
				if (SystemSounds.question == null)
				{
					SystemSounds.question = new SystemSound(32);
				}
				return SystemSounds.question;
			}
		}

		// Token: 0x04001FB5 RID: 8117
		private static volatile SystemSound asterisk;

		// Token: 0x04001FB6 RID: 8118
		private static volatile SystemSound beep;

		// Token: 0x04001FB7 RID: 8119
		private static volatile SystemSound exclamation;

		// Token: 0x04001FB8 RID: 8120
		private static volatile SystemSound hand;

		// Token: 0x04001FB9 RID: 8121
		private static volatile SystemSound question;

		// Token: 0x020007E5 RID: 2021
		private class NativeMethods
		{
			// Token: 0x060043E1 RID: 17377 RVA: 0x0011DCFD File Offset: 0x0011BEFD
			private NativeMethods()
			{
			}

			// Token: 0x040034F6 RID: 13558
			internal const int MB_ICONHAND = 16;

			// Token: 0x040034F7 RID: 13559
			internal const int MB_ICONQUESTION = 32;

			// Token: 0x040034F8 RID: 13560
			internal const int MB_ICONEXCLAMATION = 48;

			// Token: 0x040034F9 RID: 13561
			internal const int MB_ICONASTERISK = 64;
		}
	}
}
