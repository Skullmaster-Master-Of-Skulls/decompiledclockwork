using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000732 RID: 1842
	internal class ChannelDemuxerBindingElement : BindingElement
	{
		// Token: 0x06004619 RID: 17945 RVA: 0x00105FD8 File Offset: 0x001041D8
		public ChannelDemuxerBindingElement(bool cacheContextState)
		{
			this.cacheContextState = cacheContextState;
			if (cacheContextState)
			{
				this.cachedContextState = new ChannelDemuxerBindingElement.CachedBindingContextState();
			}
			this.demuxer = new ChannelDemuxer();
		}

		// Token: 0x0600461A RID: 17946 RVA: 0x00106000 File Offset: 0x00104200
		public ChannelDemuxerBindingElement(ChannelDemuxerBindingElement element)
		{
			this.demuxer = element.demuxer;
			this.cacheContextState = element.cacheContextState;
			this.cachedContextState = element.cachedContextState;
		}

		// Token: 0x170011E7 RID: 4583
		// (get) Token: 0x0600461B RID: 17947 RVA: 0x0010602C File Offset: 0x0010422C
		// (set) Token: 0x0600461C RID: 17948 RVA: 0x00106039 File Offset: 0x00104239
		public TimeSpan PeekTimeout
		{
			get
			{
				return this.demuxer.PeekTimeout;
			}
			set
			{
				if (value < TimeSpan.Zero && value != ChannelDemuxer.UseDefaultReceiveTimeout)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.demuxer.PeekTimeout = value;
			}
		}

		// Token: 0x170011E8 RID: 4584
		// (get) Token: 0x0600461D RID: 17949 RVA: 0x00106076 File Offset: 0x00104276
		// (set) Token: 0x0600461E RID: 17950 RVA: 0x00106083 File Offset: 0x00104283
		public int MaxPendingSessions
		{
			get
			{
				return this.demuxer.MaxPendingSessions;
			}
			set
			{
				if (value < 1)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException(SR.GetString("ValueMustBeGreaterThanZero")));
				}
				this.demuxer.MaxPendingSessions = value;
			}
		}

		// Token: 0x0600461F RID: 17951 RVA: 0x001060B0 File Offset: 0x001042B0
		private void SubstituteCachedBindingContextParametersIfNeeded(BindingContext context)
		{
			if (!this.cacheContextState)
			{
				return;
			}
			if (!this.cachedContextState.IsStateCached)
			{
				foreach (object item in context.BindingParameters)
				{
					this.cachedContextState.CachedBindingParameters.Add(item);
				}
				this.cachedContextState.IsStateCached = true;
				return;
			}
			context.BindingParameters.Clear();
			foreach (object item2 in this.cachedContextState.CachedBindingParameters)
			{
				context.BindingParameters.Add(item2);
			}
		}

		// Token: 0x06004620 RID: 17952 RVA: 0x0010617C File Offset: 0x0010437C
		public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			this.SubstituteCachedBindingContextParametersIfNeeded(context);
			return context.BuildInnerChannelFactory<TChannel>();
		}

		// Token: 0x06004621 RID: 17953 RVA: 0x001061A0 File Offset: 0x001043A0
		public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			ChannelDemuxerFilter channelDemuxerFilter = context.BindingParameters.Remove<ChannelDemuxerFilter>();
			this.SubstituteCachedBindingContextParametersIfNeeded(context);
			if (channelDemuxerFilter == null)
			{
				return this.demuxer.BuildChannelListener<TChannel>(context);
			}
			return this.demuxer.BuildChannelListener<TChannel>(context, channelDemuxerFilter);
		}

		// Token: 0x06004622 RID: 17954 RVA: 0x001061F0 File Offset: 0x001043F0
		public override bool CanBuildChannelFactory<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			return context.CanBuildInnerChannelFactory<TChannel>();
		}

		// Token: 0x06004623 RID: 17955 RVA: 0x0010620B File Offset: 0x0010440B
		public override bool CanBuildChannelListener<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			return context.CanBuildInnerChannelListener<TChannel>();
		}

		// Token: 0x06004624 RID: 17956 RVA: 0x00106226 File Offset: 0x00104426
		public override BindingElement Clone()
		{
			return new ChannelDemuxerBindingElement(this);
		}

		// Token: 0x06004625 RID: 17957 RVA: 0x00106230 File Offset: 0x00104430
		public override T GetProperty<T>(BindingContext context)
		{
			if (this.cacheContextState && this.cachedContextState.IsStateCached)
			{
				for (int i = 0; i < this.cachedContextState.CachedBindingParameters.Count; i++)
				{
					if (!context.BindingParameters.Contains(this.cachedContextState.CachedBindingParameters[i].GetType()))
					{
						context.BindingParameters.Add(this.cachedContextState.CachedBindingParameters[i]);
					}
				}
			}
			return context.GetInnerProperty<T>();
		}

		// Token: 0x04002D72 RID: 11634
		private ChannelDemuxer demuxer;

		// Token: 0x04002D73 RID: 11635
		private ChannelDemuxerBindingElement.CachedBindingContextState cachedContextState;

		// Token: 0x04002D74 RID: 11636
		private bool cacheContextState;

		// Token: 0x02000CD2 RID: 3282
		private class CachedBindingContextState
		{
			// Token: 0x060079D1 RID: 31185 RVA: 0x001C665D File Offset: 0x001C485D
			public CachedBindingContextState()
			{
				this.CachedBindingParameters = new BindingParameterCollection();
			}

			// Token: 0x040045B2 RID: 17842
			public bool IsStateCached;

			// Token: 0x040045B3 RID: 17843
			public BindingParameterCollection CachedBindingParameters;
		}
	}
}
