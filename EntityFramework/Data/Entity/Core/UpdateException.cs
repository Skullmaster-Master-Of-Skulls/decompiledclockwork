using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Objects;
using System.Runtime.Serialization;

namespace System.Data.Entity.Core
{
	// Token: 0x020005C1 RID: 1473
	[Serializable]
	public class UpdateException : DataException
	{
		// Token: 0x06003AFF RID: 15103 RVA: 0x00117D21 File Offset: 0x00115F21
		public UpdateException()
		{
		}

		// Token: 0x06003B00 RID: 15104 RVA: 0x00117D29 File Offset: 0x00115F29
		public UpdateException(string message) : base(message)
		{
		}

		// Token: 0x06003B01 RID: 15105 RVA: 0x00117D32 File Offset: 0x00115F32
		public UpdateException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06003B02 RID: 15106 RVA: 0x00117D3C File Offset: 0x00115F3C
		public UpdateException(string message, Exception innerException, IEnumerable<ObjectStateEntry> stateEntries) : base(message, innerException)
		{
			List<ObjectStateEntry> list = new List<ObjectStateEntry>(stateEntries);
			this._stateEntries = new ReadOnlyCollection<ObjectStateEntry>(list);
		}

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x06003B03 RID: 15107 RVA: 0x00117D64 File Offset: 0x00115F64
		public ReadOnlyCollection<ObjectStateEntry> StateEntries
		{
			get
			{
				return this._stateEntries;
			}
		}

		// Token: 0x06003B04 RID: 15108 RVA: 0x00117D6C File Offset: 0x00115F6C
		protected UpdateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0400164C RID: 5708
		[NonSerialized]
		private readonly ReadOnlyCollection<ObjectStateEntry> _stateEntries;
	}
}
