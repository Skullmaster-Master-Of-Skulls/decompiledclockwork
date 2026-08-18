using System;
using System.IO;

namespace System.Net.Cache
{
	// Token: 0x0200031A RID: 794
	internal abstract class BaseWrapperStream : Stream, IRequestLifetimeTracker
	{
		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06001C54 RID: 7252 RVA: 0x00086B56 File Offset: 0x00084D56
		protected Stream WrappedStream
		{
			get
			{
				return this.m_WrappedStream;
			}
		}

		// Token: 0x06001C55 RID: 7253 RVA: 0x00086B5E File Offset: 0x00084D5E
		public BaseWrapperStream(Stream wrappedStream)
		{
			this.m_WrappedStream = wrappedStream;
		}

		// Token: 0x06001C56 RID: 7254 RVA: 0x00086B70 File Offset: 0x00084D70
		public void TrackRequestLifetime(long requestStartTimestamp)
		{
			IRequestLifetimeTracker requestLifetimeTracker = this.m_WrappedStream as IRequestLifetimeTracker;
			requestLifetimeTracker.TrackRequestLifetime(requestStartTimestamp);
		}

		// Token: 0x04001B97 RID: 7063
		private Stream m_WrappedStream;
	}
}
