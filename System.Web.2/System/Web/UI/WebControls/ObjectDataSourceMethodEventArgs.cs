using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200048B RID: 1163
	public class ObjectDataSourceMethodEventArgs : CancelEventArgs
	{
		// Token: 0x060039AC RID: 14764 RVA: 0x000BAE4E File Offset: 0x000B904E
		public ObjectDataSourceMethodEventArgs(IOrderedDictionary inputParameters)
		{
			this._inputParameters = inputParameters;
		}

		// Token: 0x170010CE RID: 4302
		// (get) Token: 0x060039AD RID: 14765 RVA: 0x000BAE5D File Offset: 0x000B905D
		public IOrderedDictionary InputParameters
		{
			get
			{
				return this._inputParameters;
			}
		}

		// Token: 0x040022BD RID: 8893
		private IOrderedDictionary _inputParameters;
	}
}
