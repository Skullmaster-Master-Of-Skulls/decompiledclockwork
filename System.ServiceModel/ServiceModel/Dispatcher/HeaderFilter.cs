using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000469 RID: 1129
	internal abstract class HeaderFilter : MessageFilter
	{
		// Token: 0x06002BE6 RID: 11238 RVA: 0x000AC3D0 File Offset: 0x000AA5D0
		public override bool Match(MessageBuffer buffer)
		{
			if (buffer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("buffer");
			}
			Message message = buffer.CreateMessage();
			bool result;
			try
			{
				result = this.Match(message);
			}
			finally
			{
				message.Close();
			}
			return result;
		}
	}
}
