using System;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.DataHandler.Serializer;
using Microsoft.Owin.Security.DataProtection;

namespace Microsoft.Owin.Security.DataHandler
{
	// Token: 0x02000008 RID: 8
	public class PropertiesDataFormat : SecureDataFormat<AuthenticationProperties>
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00002288 File Offset: 0x00000488
		public PropertiesDataFormat(IDataProtector protector) : base(DataSerializers.Properties, protector, TextEncodings.Base64Url)
		{
		}
	}
}
