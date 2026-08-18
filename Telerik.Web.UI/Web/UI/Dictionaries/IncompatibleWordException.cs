using System;

namespace Telerik.Web.UI.Dictionaries
{
	// Token: 0x020011D5 RID: 4565
	[Serializable]
	internal class IncompatibleWordException : InvalidOperationException
	{
		// Token: 0x0600BC9B RID: 48283 RVA: 0x0029D655 File Offset: 0x0029B855
		internal IncompatibleWordException() : base("The word you are trying to add is not compatible with the current spell check provider. Please use the edit distance spell provider by setting the SpellCheckProvider property to SpellCheckProvider.EditDistanceProvider.")
		{
		}
	}
}
