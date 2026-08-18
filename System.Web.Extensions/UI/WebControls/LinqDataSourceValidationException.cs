using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security;
using System.Web.DynamicData;
using System.Web.Resources;

namespace System.Web.UI.WebControls
{
	// Token: 0x020000A5 RID: 165
	[Serializable]
	public class LinqDataSourceValidationException : Exception, IDynamicValidatorException, ISerializable
	{
		// Token: 0x06000738 RID: 1848 RVA: 0x0001D0D5 File Offset: 0x0001B2D5
		public LinqDataSourceValidationException() : base(AtlasWeb.LinqDataSourceValidationException_ValidationFailed)
		{
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x0001D0E2 File Offset: 0x0001B2E2
		public LinqDataSourceValidationException(string message) : base(message)
		{
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x0001D0EB File Offset: 0x0001B2EB
		public LinqDataSourceValidationException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x0001D0F5 File Offset: 0x0001B2F5
		public LinqDataSourceValidationException(string message, IDictionary<string, Exception> innerExceptions) : this(message)
		{
			this._innerExceptions = innerExceptions;
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x0001D105 File Offset: 0x0001B305
		protected LinqDataSourceValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this._innerExceptions = (IDictionary<string, Exception>)info.GetValue("InnerExceptions", typeof(IDictionary<string, Exception>));
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x0600073D RID: 1853 RVA: 0x0001D12F File Offset: 0x0001B32F
		public IDictionary<string, Exception> InnerExceptions
		{
			get
			{
				if (this._innerExceptions == null)
				{
					this._innerExceptions = new Dictionary<string, Exception>(StringComparer.OrdinalIgnoreCase);
				}
				return this._innerExceptions;
			}
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x0001D14F File Offset: 0x0001B34F
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("InnerExceptions", this.InnerExceptions, typeof(IDictionary<string, Exception>));
		}

		// Token: 0x04000272 RID: 626
		private IDictionary<string, Exception> _innerExceptions;
	}
}
