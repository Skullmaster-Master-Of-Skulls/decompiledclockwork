using System;
using System.Data.Common;

namespace System.Data.ProviderBase
{
	// Token: 0x020002CF RID: 719
	internal class TimeoutTimer
	{
		// Token: 0x06002B56 RID: 11094 RVA: 0x0011D80C File Offset: 0x0011CC0C
		internal static TimeoutTimer StartSecondsTimeout(int seconds)
		{
			TimeoutTimer timeoutTimer = new TimeoutTimer();
			timeoutTimer.SetTimeoutSeconds(seconds);
			return timeoutTimer;
		}

		// Token: 0x06002B57 RID: 11095 RVA: 0x0011D828 File Offset: 0x0011CC28
		internal static TimeoutTimer StartMillisecondsTimeout(long milliseconds)
		{
			TimeoutTimer timeoutTimer = new TimeoutTimer();
			timeoutTimer._originalTimerTicks = milliseconds * 10000L;
			timeoutTimer._timerExpire = checked(ADP.TimerCurrent() + timeoutTimer._originalTimerTicks);
			timeoutTimer._isInfiniteTimeout = false;
			return timeoutTimer;
		}

		// Token: 0x06002B58 RID: 11096 RVA: 0x0011D864 File Offset: 0x0011CC64
		internal void SetTimeoutSeconds(int seconds)
		{
			if (TimeoutTimer.InfiniteTimeout == (long)seconds)
			{
				this._isInfiniteTimeout = true;
				return;
			}
			this._originalTimerTicks = ADP.TimerFromSeconds(seconds);
			this._timerExpire = checked(ADP.TimerCurrent() + this._originalTimerTicks);
			this._isInfiniteTimeout = false;
		}

		// Token: 0x06002B59 RID: 11097 RVA: 0x0011D8A8 File Offset: 0x0011CCA8
		internal void Reset()
		{
			if (TimeoutTimer.InfiniteTimeout == this._originalTimerTicks)
			{
				this._isInfiniteTimeout = true;
				return;
			}
			this._timerExpire = checked(ADP.TimerCurrent() + this._originalTimerTicks);
			this._isInfiniteTimeout = false;
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06002B5A RID: 11098 RVA: 0x0011D8E4 File Offset: 0x0011CCE4
		internal bool IsExpired
		{
			get
			{
				return !this.IsInfinite && ADP.TimerHasExpired(this._timerExpire);
			}
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x06002B5B RID: 11099 RVA: 0x0011D908 File Offset: 0x0011CD08
		internal bool IsInfinite
		{
			get
			{
				return this._isInfiniteTimeout;
			}
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06002B5C RID: 11100 RVA: 0x0011D91C File Offset: 0x0011CD1C
		internal long LegacyTimerExpire
		{
			get
			{
				if (!this._isInfiniteTimeout)
				{
					return this._timerExpire;
				}
				return long.MaxValue;
			}
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06002B5D RID: 11101 RVA: 0x0011D944 File Offset: 0x0011CD44
		internal long MillisecondsRemaining
		{
			get
			{
				long num;
				if (this._isInfiniteTimeout)
				{
					num = long.MaxValue;
				}
				else
				{
					num = ADP.TimerRemainingMilliseconds(this._timerExpire);
					if (0L > num)
					{
						num = 0L;
					}
				}
				return num;
			}
		}

		// Token: 0x04001BC7 RID: 7111
		private long _timerExpire;

		// Token: 0x04001BC8 RID: 7112
		private bool _isInfiniteTimeout;

		// Token: 0x04001BC9 RID: 7113
		private long _originalTimerTicks;

		// Token: 0x04001BCA RID: 7114
		internal static readonly long InfiniteTimeout;
	}
}
