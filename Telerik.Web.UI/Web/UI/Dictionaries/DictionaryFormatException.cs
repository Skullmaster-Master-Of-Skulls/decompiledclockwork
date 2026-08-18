using System;

namespace Telerik.Web.UI.Dictionaries
{
	// Token: 0x020011CA RID: 4554
	[Serializable]
	public class DictionaryFormatException : InvalidOperationException
	{
		// Token: 0x0600BC17 RID: 48151 RVA: 0x0029AE39 File Offset: 0x00299039
		public DictionaryFormatException() : base("Invalid dictionary format.  Please import the word list with the Dictionary tool or download a newer dictionary.")
		{
		}
	}
}
