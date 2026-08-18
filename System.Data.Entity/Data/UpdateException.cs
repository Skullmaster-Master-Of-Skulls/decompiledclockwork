using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Objects;
using System.Runtime.Serialization;

namespace System.Data
{
	// Token: 0x02000014 RID: 20
	[Serializable]
	public class UpdateException : DataException
	{
		// Token: 0x0600005F RID: 95 RVA: 0x0000304C File Offset: 0x0000124C
		public UpdateException()
		{
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002BA3 File Offset: 0x00000DA3
		public UpdateException(string message) : base(message)
		{
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002BAC File Offset: 0x00000DAC
		public UpdateException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003054 File Offset: 0x00001254
		public UpdateException(string message, Exception innerException, IEnumerable<ObjectStateEntry> stateEntries) : base(message, innerException)
		{
			List<ObjectStateEntry> list = new List<ObjectStateEntry>(stateEntries);
			this._stateEntries = list.AsReadOnly();
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000063 RID: 99 RVA: 0x0000307C File Offset: 0x0000127C
		public ReadOnlyCollection<ObjectStateEntry> StateEntries
		{
			get
			{
				return this._stateEntries;
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002BB6 File Offset: 0x00000DB6
		protected UpdateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x04000084 RID: 132
		[NonSerialized]
		private ReadOnlyCollection<ObjectStateEntry> _stateEntries;
	}
}
