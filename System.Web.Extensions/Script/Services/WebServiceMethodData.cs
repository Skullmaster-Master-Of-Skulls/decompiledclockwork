using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Web.Resources;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace System.Web.Script.Services
{
	// Token: 0x020000FA RID: 250
	internal class WebServiceMethodData
	{
		// Token: 0x06000D3E RID: 3390 RVA: 0x0002CC7C File Offset: 0x0002AE7C
		internal WebServiceMethodData(WebServiceData owner, MethodInfo methodInfo, WebMethodAttribute webMethodAttribute, ScriptMethodAttribute scriptMethodAttribute)
		{
			this._owner = owner;
			this._methodInfo = methodInfo;
			this._webMethodAttribute = webMethodAttribute;
			this._methodName = this._webMethodAttribute.MessageName;
			this._scriptMethodAttribute = scriptMethodAttribute;
			if (string.IsNullOrEmpty(this._methodName))
			{
				this._methodName = methodInfo.Name;
			}
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x0002CCD6 File Offset: 0x0002AED6
		internal WebServiceMethodData(WebServiceData owner, string methodName, Dictionary<string, WebServiceParameterData> parameterData, bool useHttpGet)
		{
			this._owner = owner;
			this._methodName = methodName;
			this._parameterData = parameterData;
			this._useHttpGet = new bool?(useHttpGet);
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06000D40 RID: 3392 RVA: 0x0002CD00 File Offset: 0x0002AF00
		internal WebServiceData Owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x0002CD08 File Offset: 0x0002AF08
		private void EnsureParameters()
		{
			if (this._parameterData != null)
			{
				return;
			}
			lock (this)
			{
				Dictionary<string, WebServiceParameterData> dictionary = new Dictionary<string, WebServiceParameterData>();
				int num = 0;
				foreach (ParameterInfo parameterInfo in this._methodInfo.GetParameters())
				{
					dictionary[parameterInfo.Name] = new WebServiceParameterData(parameterInfo, num);
					num++;
				}
				this._parameterData = dictionary;
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06000D42 RID: 3394 RVA: 0x0002CD98 File Offset: 0x0002AF98
		internal string MethodName
		{
			get
			{
				return this._methodName;
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06000D43 RID: 3395 RVA: 0x0002CDA0 File Offset: 0x0002AFA0
		internal MethodInfo MethodInfo
		{
			get
			{
				return this._methodInfo;
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06000D44 RID: 3396 RVA: 0x0002CDA8 File Offset: 0x0002AFA8
		internal IDictionary<string, WebServiceParameterData> ParameterDataDictionary
		{
			get
			{
				this.EnsureParameters();
				return this._parameterData;
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06000D45 RID: 3397 RVA: 0x0002CDB6 File Offset: 0x0002AFB6
		internal ICollection<WebServiceParameterData> ParameterDatas
		{
			get
			{
				return this.ParameterDataDictionary.Values;
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06000D46 RID: 3398 RVA: 0x0002CDC3 File Offset: 0x0002AFC3
		internal int CacheDuration
		{
			get
			{
				return this._webMethodAttribute.CacheDuration;
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06000D47 RID: 3399 RVA: 0x0002CDD0 File Offset: 0x0002AFD0
		internal bool RequiresSession
		{
			get
			{
				return this._webMethodAttribute.EnableSession;
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06000D48 RID: 3400 RVA: 0x0002CDDD File Offset: 0x0002AFDD
		internal bool IsStatic
		{
			get
			{
				return this._methodInfo.IsStatic;
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06000D49 RID: 3401 RVA: 0x0002CDEA File Offset: 0x0002AFEA
		internal Type ReturnType
		{
			get
			{
				if (!(this._methodInfo == null))
				{
					return this._methodInfo.ReturnType;
				}
				return null;
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06000D4A RID: 3402 RVA: 0x0002CE07 File Offset: 0x0002B007
		internal bool UseXmlResponse
		{
			get
			{
				return this._scriptMethodAttribute != null && this._scriptMethodAttribute.ResponseFormat == ResponseFormat.Xml;
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06000D4B RID: 3403 RVA: 0x0002CE21 File Offset: 0x0002B021
		internal bool XmlSerializeString
		{
			get
			{
				return this._scriptMethodAttribute != null && this._scriptMethodAttribute.XmlSerializeString;
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06000D4C RID: 3404 RVA: 0x0002CE38 File Offset: 0x0002B038
		internal bool UseGet
		{
			get
			{
				if (this._useHttpGet != null)
				{
					return this._useHttpGet.Value;
				}
				return this._scriptMethodAttribute != null && this._scriptMethodAttribute.UseHttpGet;
			}
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x0002CE68 File Offset: 0x0002B068
		internal object CallMethodFromRawParams(object target, IDictionary<string, object> parameters)
		{
			parameters = this.StrongTypeParameters(parameters);
			return this.CallMethod(target, parameters);
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x0002CE7C File Offset: 0x0002B07C
		private object CallMethod(object target, IDictionary<string, object> parameters)
		{
			this.EnsureParameters();
			object[] array = new object[this._parameterData.Count];
			foreach (WebServiceParameterData webServiceParameterData in this._parameterData.Values)
			{
				object obj;
				if (!parameters.TryGetValue(webServiceParameterData.ParameterInfo.Name, out obj))
				{
					throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.WebService_MissingArg, new object[]
					{
						webServiceParameterData.ParameterInfo.Name
					}));
				}
				array[webServiceParameterData.Index] = obj;
			}
			return this._methodInfo.Invoke(target, array);
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x0002CF3C File Offset: 0x0002B13C
		private IDictionary<string, object> StrongTypeParameters(IDictionary<string, object> rawParams)
		{
			IDictionary<string, WebServiceParameterData> parameterDataDictionary = this.ParameterDataDictionary;
			IDictionary<string, object> dictionary = new Dictionary<string, object>(rawParams.Count);
			foreach (KeyValuePair<string, object> keyValuePair in rawParams)
			{
				string key = keyValuePair.Key;
				if (parameterDataDictionary.ContainsKey(key))
				{
					Type parameterType = parameterDataDictionary[key].ParameterInfo.ParameterType;
					dictionary[key] = ObjectConverter.ConvertObjectToType(keyValuePair.Value, parameterType, this.Owner.Serializer);
				}
			}
			return dictionary;
		}

		// Token: 0x040003AC RID: 940
		private MethodInfo _methodInfo;

		// Token: 0x040003AD RID: 941
		private WebMethodAttribute _webMethodAttribute;

		// Token: 0x040003AE RID: 942
		private ScriptMethodAttribute _scriptMethodAttribute;

		// Token: 0x040003AF RID: 943
		private string _methodName;

		// Token: 0x040003B0 RID: 944
		private Dictionary<string, WebServiceParameterData> _parameterData;

		// Token: 0x040003B1 RID: 945
		private WebServiceData _owner;

		// Token: 0x040003B2 RID: 946
		private bool? _useHttpGet;
	}
}
