using System;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Web.Util
{
	// Token: 0x020001CC RID: 460
	internal sealed class ActivityIdHelper
	{
		// Token: 0x06001764 RID: 5988 RVA: 0x0004970D File Offset: 0x0004790D
		private ActivityIdHelper(ActivityIdHelper.GetCurrentDelegate getCurrentDel, ActivityIdHelper.SetAndDestroyDelegate setAndDestroyDel, ActivityIdHelper.SetAndPreserveDelegate setAndPreserveDel)
		{
			this._getCurrentDel = getCurrentDel;
			this._setAndDestroyDel = setAndDestroyDel;
			this._setAndPreserveDel = setAndPreserveDel;
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06001765 RID: 5989 RVA: 0x0004972A File Offset: 0x0004792A
		public Guid CurrentThreadActivityId
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this._getCurrentDel();
			}
		}

		// Token: 0x06001766 RID: 5990 RVA: 0x00049738 File Offset: 0x00047938
		private static ActivityIdHelper GetSingleton()
		{
			try
			{
				ActivityIdHelper.GetCurrentDelegate getCurrentDelegate = (ActivityIdHelper.GetCurrentDelegate)Delegate.CreateDelegate(typeof(ActivityIdHelper.GetCurrentDelegate), typeof(EventSource), "get_CurrentThreadActivityId", false, false);
				ActivityIdHelper.SetAndDestroyDelegate setAndDestroyDelegate = (ActivityIdHelper.SetAndDestroyDelegate)Delegate.CreateDelegate(typeof(ActivityIdHelper.SetAndDestroyDelegate), typeof(EventSource), "SetCurrentThreadActivityId", false, false);
				ActivityIdHelper.SetAndPreserveDelegate setAndPreserveDelegate = (ActivityIdHelper.SetAndPreserveDelegate)Delegate.CreateDelegate(typeof(ActivityIdHelper.SetAndPreserveDelegate), typeof(EventSource), "SetCurrentThreadActivityId", false, false);
				if (getCurrentDelegate != null && setAndDestroyDelegate != null && setAndPreserveDelegate != null)
				{
					return new ActivityIdHelper(getCurrentDelegate, setAndDestroyDelegate, setAndPreserveDelegate);
				}
			}
			catch
			{
			}
			return null;
		}

		// Token: 0x06001767 RID: 5991 RVA: 0x000497E4 File Offset: 0x000479E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetCurrentThreadActivityId(Guid activityId)
		{
			this._setAndDestroyDel(activityId);
		}

		// Token: 0x06001768 RID: 5992 RVA: 0x000497F2 File Offset: 0x000479F2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetCurrentThreadActivityId(Guid activityId, out Guid oldActivityThatWillContinue)
		{
			this._setAndPreserveDel(activityId, out oldActivityThatWillContinue);
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x00049804 File Offset: 0x00047A04
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static Guid UnsafeCreateNewActivityId()
		{
			Guid baseGuid = ActivityIdHelper._baseGuid;
			*(long*)(&baseGuid) ^= Interlocked.Increment(ref ActivityIdHelper._counter);
			return baseGuid;
		}

		// Token: 0x04001708 RID: 5896
		internal static readonly ActivityIdHelper Instance = ActivityIdHelper.GetSingleton();

		// Token: 0x04001709 RID: 5897
		private static readonly Guid _baseGuid = Guid.NewGuid();

		// Token: 0x0400170A RID: 5898
		private static long _counter;

		// Token: 0x0400170B RID: 5899
		private readonly ActivityIdHelper.GetCurrentDelegate _getCurrentDel;

		// Token: 0x0400170C RID: 5900
		private readonly ActivityIdHelper.SetAndDestroyDelegate _setAndDestroyDel;

		// Token: 0x0400170D RID: 5901
		private readonly ActivityIdHelper.SetAndPreserveDelegate _setAndPreserveDel;

		// Token: 0x02000934 RID: 2356
		// (Invoke) Token: 0x06006950 RID: 26960
		private delegate Guid GetCurrentDelegate();

		// Token: 0x02000935 RID: 2357
		// (Invoke) Token: 0x06006954 RID: 26964
		private delegate void SetAndDestroyDelegate(Guid activityId);

		// Token: 0x02000936 RID: 2358
		// (Invoke) Token: 0x06006958 RID: 26968
		private delegate void SetAndPreserveDelegate(Guid activityId, out Guid oldActivityThatWillContinue);
	}
}
