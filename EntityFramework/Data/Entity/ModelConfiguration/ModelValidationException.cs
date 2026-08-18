using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Runtime.Serialization;

namespace System.Data.Entity.ModelConfiguration
{
	// Token: 0x02000828 RID: 2088
	[Serializable]
	public class ModelValidationException : Exception
	{
		// Token: 0x06005DB7 RID: 23991 RVA: 0x0019554C File Offset: 0x0019374C
		public ModelValidationException()
		{
		}

		// Token: 0x06005DB8 RID: 23992 RVA: 0x00195554 File Offset: 0x00193754
		public ModelValidationException(string message) : base(message)
		{
		}

		// Token: 0x06005DB9 RID: 23993 RVA: 0x0019555D File Offset: 0x0019375D
		public ModelValidationException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06005DBA RID: 23994 RVA: 0x00195567 File Offset: 0x00193767
		internal ModelValidationException(IEnumerable<DataModelErrorEventArgs> validationErrors) : base(validationErrors.ToErrorMessage())
		{
		}

		// Token: 0x06005DBB RID: 23995 RVA: 0x00195575 File Offset: 0x00193775
		protected ModelValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
