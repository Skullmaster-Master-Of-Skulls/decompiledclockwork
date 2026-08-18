using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Internal;
using System.Data.Entity.Resources;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000754 RID: 1876
	[SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors", Justification = "SerializeObjectState used instead")]
	[Serializable]
	public class DbUpdateException : DataException
	{
		// Token: 0x06005514 RID: 21780 RVA: 0x00172811 File Offset: 0x00170A11
		internal DbUpdateException(InternalContext internalContext, UpdateException innerException, bool involvesIndependentAssociations) : base(involvesIndependentAssociations ? Strings.DbContext_IndependentAssociationUpdateException : innerException.Message, innerException)
		{
			this._internalContext = internalContext;
			this._state.InvolvesIndependentAssociations = involvesIndependentAssociations;
			this.SubscribeToSerializeObjectState();
		}

		// Token: 0x17000E8C RID: 3724
		// (get) Token: 0x06005515 RID: 21781 RVA: 0x0017285C File Offset: 0x00170A5C
		public IEnumerable<DbEntityEntry> Entries
		{
			get
			{
				UpdateException ex = base.InnerException as UpdateException;
				if (this._state.InvolvesIndependentAssociations || this._internalContext == null || ex == null || ex.StateEntries == null)
				{
					return Enumerable.Empty<DbEntityEntry>();
				}
				return from e in ex.StateEntries
				select new DbEntityEntry(new InternalEntityEntry(this._internalContext, new StateEntryAdapter(e)));
			}
		}

		// Token: 0x06005516 RID: 21782 RVA: 0x001728B2 File Offset: 0x00170AB2
		public DbUpdateException()
		{
			this.SubscribeToSerializeObjectState();
		}

		// Token: 0x06005517 RID: 21783 RVA: 0x001728C0 File Offset: 0x00170AC0
		public DbUpdateException(string message) : base(message)
		{
			this.SubscribeToSerializeObjectState();
		}

		// Token: 0x06005518 RID: 21784 RVA: 0x001728CF File Offset: 0x00170ACF
		public DbUpdateException(string message, Exception innerException) : base(message, innerException)
		{
			this.SubscribeToSerializeObjectState();
		}

		// Token: 0x06005519 RID: 21785 RVA: 0x001728F2 File Offset: 0x00170AF2
		private void SubscribeToSerializeObjectState()
		{
			base.SerializeObjectState += delegate(object exception, SafeSerializationEventArgs eventArgs)
			{
				eventArgs.AddSerializedState(this._state);
			};
		}

		// Token: 0x0400229E RID: 8862
		[NonSerialized]
		private readonly InternalContext _internalContext;

		// Token: 0x0400229F RID: 8863
		[NonSerialized]
		private DbUpdateException.DbUpdateExceptionState _state;

		// Token: 0x02000755 RID: 1877
		[Serializable]
		private struct DbUpdateExceptionState : ISafeSerializationData
		{
			// Token: 0x17000E8D RID: 3725
			// (get) Token: 0x0600551C RID: 21788 RVA: 0x00172906 File Offset: 0x00170B06
			// (set) Token: 0x0600551D RID: 21789 RVA: 0x0017290E File Offset: 0x00170B0E
			public bool InvolvesIndependentAssociations { get; set; }

			// Token: 0x0600551E RID: 21790 RVA: 0x00172918 File Offset: 0x00170B18
			public void CompleteDeserialization(object deserialized)
			{
				DbUpdateException ex = (DbUpdateException)deserialized;
				ex._state = this;
				ex.SubscribeToSerializeObjectState();
			}
		}
	}
}
