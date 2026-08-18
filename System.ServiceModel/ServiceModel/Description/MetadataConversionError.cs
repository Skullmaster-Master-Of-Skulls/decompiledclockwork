using System;

namespace System.ServiceModel.Description
{
	// Token: 0x02000411 RID: 1041
	public class MetadataConversionError
	{
		// Token: 0x060027E5 RID: 10213 RVA: 0x0009690D File Offset: 0x00094B0D
		public MetadataConversionError(string message) : this(message, false)
		{
		}

		// Token: 0x060027E6 RID: 10214 RVA: 0x00096917 File Offset: 0x00094B17
		public MetadataConversionError(string message, bool isWarning)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			this.message = message;
			this.isWarning = isWarning;
		}

		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x060027E7 RID: 10215 RVA: 0x00096940 File Offset: 0x00094B40
		public string Message
		{
			get
			{
				return this.message;
			}
		}

		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x060027E8 RID: 10216 RVA: 0x00096948 File Offset: 0x00094B48
		public bool IsWarning
		{
			get
			{
				return this.isWarning;
			}
		}

		// Token: 0x060027E9 RID: 10217 RVA: 0x00096950 File Offset: 0x00094B50
		public override bool Equals(object obj)
		{
			MetadataConversionError metadataConversionError = obj as MetadataConversionError;
			return metadataConversionError != null && metadataConversionError.IsWarning == this.IsWarning && metadataConversionError.Message == this.Message;
		}

		// Token: 0x060027EA RID: 10218 RVA: 0x0009698A File Offset: 0x00094B8A
		public override int GetHashCode()
		{
			return this.message.GetHashCode();
		}

		// Token: 0x040021FF RID: 8703
		private string message;

		// Token: 0x04002200 RID: 8704
		private bool isWarning;
	}
}
