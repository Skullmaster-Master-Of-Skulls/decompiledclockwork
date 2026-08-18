using System;

namespace System.Data.Entity.Validation
{
	// Token: 0x02000834 RID: 2100
	[Serializable]
	public class DbValidationError
	{
		// Token: 0x06005DF0 RID: 24048 RVA: 0x00195C74 File Offset: 0x00193E74
		public DbValidationError(string propertyName, string errorMessage)
		{
			this._propertyName = propertyName;
			this._errorMessage = errorMessage;
		}

		// Token: 0x17000FEB RID: 4075
		// (get) Token: 0x06005DF1 RID: 24049 RVA: 0x00195C8A File Offset: 0x00193E8A
		public string PropertyName
		{
			get
			{
				return this._propertyName;
			}
		}

		// Token: 0x17000FEC RID: 4076
		// (get) Token: 0x06005DF2 RID: 24050 RVA: 0x00195C92 File Offset: 0x00193E92
		public string ErrorMessage
		{
			get
			{
				return this._errorMessage;
			}
		}

		// Token: 0x04002513 RID: 9491
		private readonly string _propertyName;

		// Token: 0x04002514 RID: 9492
		private readonly string _errorMessage;
	}
}
