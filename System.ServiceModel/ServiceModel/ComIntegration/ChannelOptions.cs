using System;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x020001DF RID: 479
	internal class ChannelOptions : IChannelOptions, IDisposable
	{
		// Token: 0x06000F78 RID: 3960 RVA: 0x00036B33 File Offset: 0x00034D33
		internal ChannelOptions(IProvideChannelBuilderSettings channelBuilderSettings)
		{
			this.channelBuilderSettings = channelBuilderSettings;
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x00036B44 File Offset: 0x00034D44
		internal static ComProxy Create(IntPtr outer, IProvideChannelBuilderSettings channelBuilderSettings)
		{
			if (channelBuilderSettings == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("CannotCreateChannelOption")));
			}
			ChannelOptions channelOptions = null;
			ComProxy comProxy = null;
			ComProxy result;
			try
			{
				channelOptions = new ChannelOptions(channelBuilderSettings);
				comProxy = ComProxy.Create(outer, channelOptions, channelOptions);
				result = comProxy;
			}
			finally
			{
				if (comProxy == null && channelOptions != null)
				{
					((IDisposable)channelOptions).Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x00036BA4 File Offset: 0x00034DA4
		void IDisposable.Dispose()
		{
		}

		// Token: 0x040017BF RID: 6079
		protected IProvideChannelBuilderSettings channelBuilderSettings;
	}
}
