using System;
using System.Configuration;
using System.IO;

namespace System.Xml.Serialization.Configuration
{
	// Token: 0x020001D3 RID: 467
	public class RootedPathValidator : ConfigurationValidatorBase
	{
		// Token: 0x06001F7B RID: 8059 RVA: 0x000AA9A9 File Offset: 0x000A8BA9
		public override bool CanValidate(Type type)
		{
			return type == typeof(string);
		}

		// Token: 0x06001F7C RID: 8060 RVA: 0x000AA9BC File Offset: 0x000A8BBC
		public override void Validate(object value)
		{
			string text = value as string;
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			text = text.Trim();
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			if (!Path.IsPathRooted(text))
			{
				throw new ConfigurationErrorsException();
			}
			char c = text[0];
			if (c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar)
			{
				throw new ConfigurationErrorsException();
			}
		}
	}
}
