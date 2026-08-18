using System;

namespace Telerik.Web.Analytics
{
	// Token: 0x0200047F RID: 1151
	public interface ITraceMonitor
	{
		// Token: 0x06002924 RID: 10532
		void TrackFeatureStart(IFeatureSignature feature);

		// Token: 0x06002925 RID: 10533
		void TrackError(Exception exception, IFeatureSignature feature);

		// Token: 0x06002926 RID: 10534
		void TrackError(Exception exception);

		// Token: 0x06002927 RID: 10535
		void TrackValue(IFeatureSignature feature, long value);

		// Token: 0x06002928 RID: 10536
		void TrackFeatureEnd(IFeatureSignature feature);

		// Token: 0x06002929 RID: 10537
		void TrackFeatureCancel(IFeatureSignature feature);

		// Token: 0x0600292A RID: 10538
		void TrackAtomicFeature(string feature);
	}
}
