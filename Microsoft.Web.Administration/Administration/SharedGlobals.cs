using System;

namespace Microsoft.Web.Administration
{
	// Token: 0x0200007C RID: 124
	internal static class SharedGlobals
	{
		// Token: 0x06000384 RID: 900 RVA: 0x000092E6 File Offset: 0x000082E6
		internal static char[] GetInvalidApplicationPathCharacters()
		{
			return new char[]
			{
				'\\',
				'?',
				';',
				':',
				'@',
				'&',
				'=',
				'+',
				'$',
				',',
				'|',
				'"',
				'<',
				'>',
				'*'
			};
		}

		// Token: 0x06000385 RID: 901 RVA: 0x00009322 File Offset: 0x00008322
		internal static char[] GetInvalidApplicationPoolNameCharacters()
		{
			return new char[]
			{
				'\\',
				'/',
				'"',
				'|',
				'<',
				'>',
				':',
				'*',
				'?',
				']',
				'[',
				'+',
				'=',
				';',
				',',
				'@',
				'&'
			};
		}

		// Token: 0x06000386 RID: 902 RVA: 0x00009356 File Offset: 0x00008356
		internal static char[] GetInvalidSiteNameCharacters()
		{
			return new char[]
			{
				'\\',
				'/',
				'?',
				';',
				':',
				'@',
				'&',
				'=',
				'+',
				'$',
				',',
				'|',
				'"',
				'<',
				'>'
			};
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000938E File Offset: 0x0000838E
		internal static char[] GetInvalidVirtualDirectoryPathCharacters()
		{
			return new char[]
			{
				'\\',
				'?',
				';',
				':',
				'@',
				'&',
				'=',
				'+',
				'$',
				',',
				'|',
				'"',
				'<',
				'>',
				'*'
			};
		}
	}
}
