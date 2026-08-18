using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Text;

namespace System.Data.Entity.ModelConfiguration.Edm
{
	// Token: 0x02000809 RID: 2057
	internal static class DataModelErrorEventArgsExtensions
	{
		// Token: 0x06005CA3 RID: 23715 RVA: 0x00190064 File Offset: 0x0018E264
		public static string ToErrorMessage(this IEnumerable<DataModelErrorEventArgs> validationErrors)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(Strings.ValidationHeader);
			stringBuilder.AppendLine();
			foreach (DataModelErrorEventArgs dataModelErrorEventArgs in validationErrors)
			{
				stringBuilder.AppendLine(Strings.ValidationItemFormat(dataModelErrorEventArgs.Item, dataModelErrorEventArgs.PropertyName, dataModelErrorEventArgs.ErrorMessage));
			}
			return stringBuilder.ToString();
		}
	}
}
