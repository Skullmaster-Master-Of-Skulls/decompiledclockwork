using System;
using System.Collections;
using System.Runtime;
using System.ServiceModel.Diagnostics.Application;

namespace System.ServiceModel.Security
{
	// Token: 0x02000301 RID: 769
	internal sealed class NegotiationTokenAuthenticatorStateCache<T> : TimeBoundedCache where T : NegotiationTokenAuthenticatorState
	{
		// Token: 0x06001A41 RID: 6721 RVA: 0x00062767 File Offset: 0x00060967
		public NegotiationTokenAuthenticatorStateCache(TimeSpan cachingSpan, int maximumCachedState) : base(NegotiationTokenAuthenticatorStateCache<T>.lowWaterMark, maximumCachedState, null, PurgingMode.TimerBasedPurge, TimeSpan.FromTicks(cachingSpan.Ticks >> 2), true)
		{
			this.cachingSpan = cachingSpan;
		}

		// Token: 0x06001A42 RID: 6722 RVA: 0x00062790 File Offset: 0x00060990
		public void AddState(string context, T state)
		{
			DateTime expirationTime = TimeoutHelper.Add(DateTime.UtcNow, this.cachingSpan);
			if (!base.TryAddItem(context, state, expirationTime, false))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new SecurityNegotiationException(SR.GetString("NegotiationStateAlreadyPresent", new object[]
				{
					context
				})));
			}
			if (TD.NegotiateTokenAuthenticatorStateCacheRatioIsEnabled())
			{
				TD.NegotiateTokenAuthenticatorStateCacheRatio(base.Count, base.Capacity);
			}
		}

		// Token: 0x06001A43 RID: 6723 RVA: 0x000627FD File Offset: 0x000609FD
		public T GetState(string context)
		{
			return base.GetItem(context) as T;
		}

		// Token: 0x06001A44 RID: 6724 RVA: 0x00062810 File Offset: 0x00060A10
		public void RemoveState(string context)
		{
			base.TryRemoveItem(context);
			if (TD.NegotiateTokenAuthenticatorStateCacheRatioIsEnabled())
			{
				TD.NegotiateTokenAuthenticatorStateCacheRatio(base.Count, base.Capacity);
			}
		}

		// Token: 0x06001A45 RID: 6725 RVA: 0x00062834 File Offset: 0x00060A34
		protected override ArrayList OnQuotaReached(Hashtable cacheTable)
		{
			if (TD.NegotiateTokenAuthenticatorStateCacheExceededIsEnabled())
			{
				TD.NegotiateTokenAuthenticatorStateCacheExceeded(SR.GetString("CachedNegotiationStateQuotaReached", new object[]
				{
					base.Capacity
				}));
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new QuotaExceededException(SR.GetString("CachedNegotiationStateQuotaReached", new object[]
			{
				base.Capacity
			})));
		}

		// Token: 0x06001A46 RID: 6726 RVA: 0x00062898 File Offset: 0x00060A98
		protected override void OnRemove(object item)
		{
			((IDisposable)item).Dispose();
			base.OnRemove(item);
		}

		// Token: 0x04001D11 RID: 7441
		private static int lowWaterMark = 50;

		// Token: 0x04001D12 RID: 7442
		private static TimeSpan purgingInterval = TimeSpan.FromMinutes(10.0);

		// Token: 0x04001D13 RID: 7443
		private TimeSpan cachingSpan;
	}
}
