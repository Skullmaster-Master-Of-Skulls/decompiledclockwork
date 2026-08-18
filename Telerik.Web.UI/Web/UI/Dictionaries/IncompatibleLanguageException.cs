using System;

namespace Telerik.Web.UI.Dictionaries
{
	// Token: 0x020011D4 RID: 4564
	[Serializable]
	internal class IncompatibleLanguageException : InvalidOperationException
	{
		// Token: 0x0600BC9A RID: 48282 RVA: 0x0029D648 File Offset: 0x0029B848
		internal IncompatibleLanguageException() : base("This language is incompatible with the phonetic spell check provider.  Please use the edit distance spell provider by setting the SpellCheckProvider property to SpellCheckProvider.EditDistanceProvider .")
		{
		}
	}
}
