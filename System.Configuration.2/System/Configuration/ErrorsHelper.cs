using System;
using System.Collections.Generic;

namespace System.Configuration
{
	// Token: 0x02000059 RID: 89
	internal static class ErrorsHelper
	{
		// Token: 0x06000383 RID: 899 RVA: 0x00013589 File Offset: 0x00011789
		internal static int GetErrorCount(List<ConfigurationException> errors)
		{
			if (errors == null)
			{
				return 0;
			}
			return errors.Count;
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00013596 File Offset: 0x00011796
		internal static bool GetHasErrors(List<ConfigurationException> errors)
		{
			return ErrorsHelper.GetErrorCount(errors) > 0;
		}

		// Token: 0x06000385 RID: 901 RVA: 0x000135A4 File Offset: 0x000117A4
		internal static void AddError(ref List<ConfigurationException> errors, ConfigurationException e)
		{
			if (errors == null)
			{
				errors = new List<ConfigurationException>();
			}
			ConfigurationErrorsException ex = e as ConfigurationErrorsException;
			if (ex == null)
			{
				errors.Add(e);
				return;
			}
			ICollection<ConfigurationException> errorsGeneric = ex.ErrorsGeneric;
			if (errorsGeneric.Count == 1)
			{
				errors.Add(e);
				return;
			}
			errors.AddRange(errorsGeneric);
		}

		// Token: 0x06000386 RID: 902 RVA: 0x000135F0 File Offset: 0x000117F0
		internal static void AddErrors(ref List<ConfigurationException> errors, ICollection<ConfigurationException> coll)
		{
			if (coll == null || coll.Count == 0)
			{
				return;
			}
			foreach (ConfigurationException e in coll)
			{
				ErrorsHelper.AddError(ref errors, e);
			}
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00013644 File Offset: 0x00011844
		internal static ConfigurationErrorsException GetErrorsException(List<ConfigurationException> errors)
		{
			if (errors == null)
			{
				return null;
			}
			return new ConfigurationErrorsException(errors);
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00013654 File Offset: 0x00011854
		internal static void ThrowOnErrors(List<ConfigurationException> errors)
		{
			ConfigurationErrorsException errorsException = ErrorsHelper.GetErrorsException(errors);
			if (errorsException != null)
			{
				throw errorsException;
			}
		}
	}
}
