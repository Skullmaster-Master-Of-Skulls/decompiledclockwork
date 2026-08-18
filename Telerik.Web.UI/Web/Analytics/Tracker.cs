using System;

namespace Telerik.Web.Analytics
{
	// Token: 0x02000480 RID: 1152
	public static class Tracker
	{
		// Token: 0x17000D58 RID: 3416
		// (get) Token: 0x0600292B RID: 10539 RVA: 0x00084604 File Offset: 0x00082804
		// (set) Token: 0x0600292C RID: 10540 RVA: 0x0008461C File Offset: 0x0008281C
		public static IFeatureResolver Resolver
		{
			get
			{
				if (Tracker._resolver == null)
				{
					Tracker._resolver = new FeatureResolver();
				}
				return Tracker._resolver;
			}
			set
			{
				Tracker._resolver = value;
			}
		}

		// Token: 0x17000D59 RID: 3417
		// (get) Token: 0x0600292D RID: 10541 RVA: 0x00084624 File Offset: 0x00082824
		// (set) Token: 0x0600292E RID: 10542 RVA: 0x0008462B File Offset: 0x0008282B
		public static ITraceMonitor AnalyticsMonitor { get; set; }

		// Token: 0x0600292F RID: 10543 RVA: 0x00084634 File Offset: 0x00082834
		public static void TrackFeature(IFeatureContract feature)
		{
			IFeatureSignature signature = feature.Signature;
			FeatureType featureType = signature.FeatureType;
			if (Tracker.AnalyticsMonitor != null && featureType == FeatureType.Atomic)
			{
				Tracker.AnalyticsMonitor.TrackAtomicFeature(Tracker.Resolver.ResolveToString(signature));
			}
		}

		// Token: 0x04000A73 RID: 2675
		private static IFeatureResolver _resolver;
	}
}
