using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02001032 RID: 4146
	[Serializable]
	public class DialogDefinitionDictionary : Dictionary<string, DialogDefinition>
	{
		// Token: 0x0600A359 RID: 41817 RVA: 0x002456AE File Offset: 0x002438AE
		public DialogDefinitionDictionary()
		{
		}

		// Token: 0x0600A35A RID: 41818 RVA: 0x002456B6 File Offset: 0x002438B6
		protected DialogDefinitionDictionary(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x17003394 RID: 13204
		// (get) Token: 0x0600A35B RID: 41819 RVA: 0x002456C0 File Offset: 0x002438C0
		internal DialogParametersDictionary AllDialogParameters
		{
			get
			{
				DialogParametersDictionary dialogParametersDictionary = new DialogParametersDictionary();
				foreach (string key in base.Keys)
				{
					dialogParametersDictionary[key] = base[key].Parameters;
				}
				return dialogParametersDictionary;
			}
		}
	}
}
