using System;
using System.Collections.Generic;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;

namespace System.Data.Entity.Validation
{
	// Token: 0x02000830 RID: 2096
	[SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors", Justification = "SerializeObjectState used instead")]
	[Serializable]
	public class DbEntityValidationException : DataException
	{
		// Token: 0x06005DDB RID: 24027 RVA: 0x00195AC3 File Offset: 0x00193CC3
		public DbEntityValidationException() : this(Strings.DbEntityValidationException_ValidationFailed)
		{
		}

		// Token: 0x06005DDC RID: 24028 RVA: 0x00195AD0 File Offset: 0x00193CD0
		public DbEntityValidationException(string message) : this(message, Enumerable.Empty<DbEntityValidationResult>())
		{
		}

		// Token: 0x06005DDD RID: 24029 RVA: 0x00195ADE File Offset: 0x00193CDE
		public DbEntityValidationException(string message, IEnumerable<DbEntityValidationResult> entityValidationResults)
		{
			this._state = new DbEntityValidationException.DbEntityValidationExceptionState();
			base..ctor(message);
			Check.NotNull<IEnumerable<DbEntityValidationResult>>(entityValidationResults, "entityValidationResults");
			this._state.InititializeValidationResults(entityValidationResults);
			this.SubscribeToSerializeObjectState();
		}

		// Token: 0x06005DDE RID: 24030 RVA: 0x00195B10 File Offset: 0x00193D10
		public DbEntityValidationException(string message, Exception innerException) : this(message, Enumerable.Empty<DbEntityValidationResult>(), innerException)
		{
		}

		// Token: 0x06005DDF RID: 24031 RVA: 0x00195B1F File Offset: 0x00193D1F
		public DbEntityValidationException(string message, IEnumerable<DbEntityValidationResult> entityValidationResults, Exception innerException)
		{
			this._state = new DbEntityValidationException.DbEntityValidationExceptionState();
			base..ctor(message, innerException);
			Check.NotNull<IEnumerable<DbEntityValidationResult>>(entityValidationResults, "entityValidationResults");
			this._state.InititializeValidationResults(entityValidationResults);
			this.SubscribeToSerializeObjectState();
		}

		// Token: 0x17000FE6 RID: 4070
		// (get) Token: 0x06005DE0 RID: 24032 RVA: 0x00195B52 File Offset: 0x00193D52
		public IEnumerable<DbEntityValidationResult> EntityValidationErrors
		{
			get
			{
				return this._state.EntityValidationErrors;
			}
		}

		// Token: 0x06005DE1 RID: 24033 RVA: 0x00195B6D File Offset: 0x00193D6D
		private void SubscribeToSerializeObjectState()
		{
			base.SerializeObjectState += delegate(object exception, SafeSerializationEventArgs eventArgs)
			{
				eventArgs.AddSerializedState(this._state);
			};
		}

		// Token: 0x0400250F RID: 9487
		[NonSerialized]
		private DbEntityValidationException.DbEntityValidationExceptionState _state;

		// Token: 0x02000831 RID: 2097
		[Serializable]
		private class DbEntityValidationExceptionState : ISafeSerializationData
		{
			// Token: 0x06005DE3 RID: 24035 RVA: 0x00195B81 File Offset: 0x00193D81
			internal void InititializeValidationResults(IEnumerable<DbEntityValidationResult> entityValidationResults)
			{
				this._entityValidationResults = ((entityValidationResults == null) ? new List<DbEntityValidationResult>() : entityValidationResults.ToList<DbEntityValidationResult>());
			}

			// Token: 0x17000FE7 RID: 4071
			// (get) Token: 0x06005DE4 RID: 24036 RVA: 0x00195B99 File Offset: 0x00193D99
			public IEnumerable<DbEntityValidationResult> EntityValidationErrors
			{
				get
				{
					return this._entityValidationResults;
				}
			}

			// Token: 0x06005DE5 RID: 24037 RVA: 0x00195BA4 File Offset: 0x00193DA4
			public void CompleteDeserialization(object deserialized)
			{
				DbEntityValidationException ex = (DbEntityValidationException)deserialized;
				ex._state = this;
				ex.SubscribeToSerializeObjectState();
			}

			// Token: 0x04002510 RID: 9488
			private IList<DbEntityValidationResult> _entityValidationResults;
		}
	}
}
