using System;
using System.Runtime.Interop;
using System.Security;

namespace System.Runtime
{
	// Token: 0x0200002D RID: 45
	internal static class Ticks
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000174 RID: 372 RVA: 0x0000698C File Offset: 0x00004B8C
		public static long Now
		{
			[SecuritySafeCritical]
			get
			{
				long result;
				UnsafeNativeMethods.GetSystemTimeAsFileTime(out result);
				return result;
			}
		}

		// Token: 0x06000175 RID: 373 RVA: 0x000069A1 File Offset: 0x00004BA1
		public static long FromMilliseconds(int milliseconds)
		{
			return checked(unchecked((long)milliseconds) * 10000L);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x000069AC File Offset: 0x00004BAC
		public static int ToMilliseconds(long ticks)
		{
			return checked((int)(ticks / 10000L));
		}

		// Token: 0x06000177 RID: 375 RVA: 0x000069B7 File Offset: 0x00004BB7
		public static long FromTimeSpan(TimeSpan duration)
		{
			return duration.Ticks;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x000069C0 File Offset: 0x00004BC0
		public static TimeSpan ToTimeSpan(long ticks)
		{
			return new TimeSpan(ticks);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x000069C8 File Offset: 0x00004BC8
		public static long Add(long firstTicks, long secondTicks)
		{
			if (firstTicks == 9223372036854775807L || firstTicks == -9223372036854775808L)
			{
				return firstTicks;
			}
			if (secondTicks == 9223372036854775807L || secondTicks == -9223372036854775808L)
			{
				return secondTicks;
			}
			if (firstTicks >= 0L && 9223372036854775807L - firstTicks <= secondTicks)
			{
				return 9223372036854775806L;
			}
			if (firstTicks <= 0L && -9223372036854775808L - firstTicks >= secondTicks)
			{
				return -9223372036854775807L;
			}
			return checked(firstTicks + secondTicks);
		}
	}
}
