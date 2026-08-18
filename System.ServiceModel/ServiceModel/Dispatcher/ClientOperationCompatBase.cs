using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000596 RID: 1430
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class ClientOperationCompatBase
	{
		// Token: 0x06003744 RID: 14148 RVA: 0x000D5490 File Offset: 0x000D3690
		internal ClientOperationCompatBase()
		{
		}

		// Token: 0x17000D16 RID: 3350
		// (get) Token: 0x06003745 RID: 14149 RVA: 0x000D5498 File Offset: 0x000D3698
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
		public IList<IParameterInspector> ParameterInspectors
		{
			get
			{
				return this.parameterInspectors;
			}
		}

		// Token: 0x0400291B RID: 10523
		internal SynchronizedCollection<IParameterInspector> parameterInspectors;
	}
}
