using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200054A RID: 1354
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class ClientRuntimeCompatBase
	{
		// Token: 0x06003373 RID: 13171 RVA: 0x000C6BCE File Offset: 0x000C4DCE
		internal ClientRuntimeCompatBase()
		{
		}

		// Token: 0x17000C13 RID: 3091
		// (get) Token: 0x06003374 RID: 13172 RVA: 0x000C6BD6 File Offset: 0x000C4DD6
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
		public IList<IClientMessageInspector> MessageInspectors
		{
			get
			{
				return this.messageInspectors;
			}
		}

		// Token: 0x17000C14 RID: 3092
		// (get) Token: 0x06003375 RID: 13173 RVA: 0x000C6BDE File Offset: 0x000C4DDE
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
		public KeyedCollection<string, ClientOperation> Operations
		{
			get
			{
				return this.compatOperations;
			}
		}

		// Token: 0x04002788 RID: 10120
		internal SynchronizedCollection<IClientMessageInspector> messageInspectors;

		// Token: 0x04002789 RID: 10121
		internal SynchronizedKeyedCollection<string, ClientOperation> operations;

		// Token: 0x0400278A RID: 10122
		internal KeyedCollection<string, ClientOperation> compatOperations;
	}
}
