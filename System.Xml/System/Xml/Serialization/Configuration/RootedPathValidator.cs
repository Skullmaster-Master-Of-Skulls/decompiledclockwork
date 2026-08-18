using System;
using System.Configuration;
using System.IO;

namespace System.Xml.Serialization.Configuration
{
	// Token: 0x02000357 RID: 855
	public class RootedPathValidator : ConfigurationValidatorBase
	{
		// Token: 0x0600294C RID: 10572 RVA: 0x000D3A90 File Offset: 0x000D2A90
		public override bool CanValidate(Type type)
		{
			return type == typeof(string);
		}

		// Token: 0x0600294D RID: 10573 RVA: 0x000D3AA0 File Offset: 0x000D2AA0
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
