using System;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace System.Data.Entity.Core
{
	// Token: 0x020005C3 RID: 1475
	[SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors", Justification = "SerializeObjectState used instead")]
	[Serializable]
	public sealed class PropertyConstraintException : ConstraintException
	{
		// Token: 0x06003B0A RID: 15114 RVA: 0x00117DA6 File Offset: 0x00115FA6
		public PropertyConstraintException()
		{
			this.SubscribeToSerializeObjectState();
		}

		// Token: 0x06003B0B RID: 15115 RVA: 0x00117DB4 File Offset: 0x00115FB4
		public PropertyConstraintException(string message) : base(message)
		{
			this.SubscribeToSerializeObjectState();
		}

		// Token: 0x06003B0C RID: 15116 RVA: 0x00117DC3 File Offset: 0x00115FC3
		public PropertyConstraintException(string message, Exception innerException) : base(message, innerException)
		{
			this.SubscribeToSerializeObjectState();
		}

		// Token: 0x06003B0D RID: 15117 RVA: 0x00117DD3 File Offset: 0x00115FD3
		public PropertyConstraintException(string message, string propertyName) : base(message)
		{
			Check.NotEmpty(propertyName, "propertyName");
			this._state.PropertyName = propertyName;
			this.SubscribeToSerializeObjectState();
		}

		// Token: 0x06003B0E RID: 15118 RVA: 0x00117DFA File Offset: 0x00115FFA
		public PropertyConstraintException(string message, string propertyName, Exception innerException) : base(message, innerException)
		{
			Check.NotEmpty(propertyName, "propertyName");
			this._state.PropertyName = propertyName;
			this.SubscribeToSerializeObjectState();
		}

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x06003B0F RID: 15119 RVA: 0x00117E22 File Offset: 0x00116022
		public string PropertyName
		{
			get
			{
				return this._state.PropertyName;
			}
		}

		// Token: 0x06003B10 RID: 15120 RVA: 0x00117E42 File Offset: 0x00116042
		private void SubscribeToSerializeObjectState()
		{
			base.SerializeObjectState += delegate(object _, SafeSerializationEventArgs a)
			{
				a.AddSerializedState(this._state);
			};
		}

		// Token: 0x0400164D RID: 5709
		[NonSerialized]
		private PropertyConstraintException.PropertyConstraintExceptionState _state;

		// Token: 0x020005C4 RID: 1476
		[Serializable]
		private struct PropertyConstraintExceptionState : ISafeSerializationData
		{
			// Token: 0x170008FA RID: 2298
			// (get) Token: 0x06003B12 RID: 15122 RVA: 0x00117E56 File Offset: 0x00116056
			// (set) Token: 0x06003B13 RID: 15123 RVA: 0x00117E5E File Offset: 0x0011605E
			public string PropertyName { get; set; }

			// Token: 0x06003B14 RID: 15124 RVA: 0x00117E68 File Offset: 0x00116068
			public void CompleteDeserialization(object deserialized)
			{
				PropertyConstraintException ex = (PropertyConstraintException)deserialized;
				ex._state = this;
				ex.SubscribeToSerializeObjectState();
			}
		}
	}
}
