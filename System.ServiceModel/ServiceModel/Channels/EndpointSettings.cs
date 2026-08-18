using System;
using System.Collections.Generic;

namespace System.ServiceModel.Channels
{
	// Token: 0x020006F9 RID: 1785
	internal static class EndpointSettings
	{
		// Token: 0x0600446F RID: 17519 RVA: 0x001021BC File Offset: 0x001003BC
		internal static T GetValue<T>(BindingContext context, string name, T defaultValue)
		{
			if (context == null || context.BindingParameters == null || context.BindingParameters.Count == 0)
			{
				return defaultValue;
			}
			return EndpointSettings.GetValue<T>(context.BindingParameters, name, defaultValue);
		}

		// Token: 0x06004470 RID: 17520 RVA: 0x001021E8 File Offset: 0x001003E8
		internal static T GetValue<T>(BindingParameterCollection bindingParameters, string name, T defaultValue)
		{
			if (bindingParameters == null || bindingParameters.Count == 0)
			{
				return defaultValue;
			}
			IDictionary<string, object> dictionary = bindingParameters.Find<IDictionary<string, object>>();
			object obj;
			if (dictionary == null || !dictionary.TryGetValue(name, out obj) || !(obj is T))
			{
				return defaultValue;
			}
			return (T)((object)obj);
		}

		// Token: 0x04002D34 RID: 11572
		internal const string ValidateOptionalClientCertificates = "wcf:HttpTransport:ValidateOptionalClientCertificates";
	}
}
