using System;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Infrastructure.Annotations
{
	// Token: 0x0200013E RID: 318
	public sealed class CompatibilityResult
	{
		// Token: 0x06000A9A RID: 2714 RVA: 0x00036093 File Offset: 0x00034293
		public CompatibilityResult(bool isCompatible, string errorMessage)
		{
			this._isCompatible = isCompatible;
			this._errorMessage = errorMessage;
			if (!isCompatible)
			{
				Check.NotEmpty(errorMessage, "errorMessage");
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000A9B RID: 2715 RVA: 0x000360B8 File Offset: 0x000342B8
		public bool IsCompatible
		{
			get
			{
				return this._isCompatible;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000A9C RID: 2716 RVA: 0x000360C0 File Offset: 0x000342C0
		public string ErrorMessage
		{
			get
			{
				return this._errorMessage;
			}
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x000360C8 File Offset: 0x000342C8
		[SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates")]
		public static implicit operator bool(CompatibilityResult result)
		{
			Check.NotNull<CompatibilityResult>(result, "result");
			return result._isCompatible;
		}

		// Token: 0x040002D5 RID: 725
		private readonly bool _isCompatible;

		// Token: 0x040002D6 RID: 726
		private readonly string _errorMessage;
	}
}
