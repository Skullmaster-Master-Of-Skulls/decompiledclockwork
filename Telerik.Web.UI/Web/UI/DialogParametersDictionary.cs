using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02001038 RID: 4152
	[Serializable]
	public class DialogParametersDictionary : Dictionary<string, DialogParameters>
	{
		// Token: 0x0600A385 RID: 41861 RVA: 0x002462E7 File Offset: 0x002444E7
		public DialogParametersDictionary()
		{
		}

		// Token: 0x0600A386 RID: 41862 RVA: 0x002462EF File Offset: 0x002444EF
		protected DialogParametersDictionary(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
