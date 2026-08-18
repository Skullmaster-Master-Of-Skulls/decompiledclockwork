using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security;
using System.Text;
using System.Web.Resources;
using System.Web.Script.Serialization;

namespace System.Web.Script.Services
{
	// Token: 0x020000F0 RID: 240
	internal class RestHandler : IHttpHandler
	{
		// Token: 0x06000CE6 RID: 3302 RVA: 0x0002B4BC File Offset: 0x000296BC
		internal static IHttpHandler CreateHandler(HttpContext context)
		{
			if (context.Request.PathInfo.Length < 2 || context.Request.PathInfo[0] != '/')
			{
				throw new InvalidOperationException(AtlasWeb.WebService_InvalidWebServiceCall);
			}
			WebServiceData webServiceData = WebServiceData.GetWebServiceData(context, context.Request.FilePath);
			string methodName = context.Request.PathInfo.Substring(1);
			return RestHandler.CreateHandler(webServiceData, methodName);
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x0002B528 File Offset: 0x00029728
		private static IHttpHandler CreateHandler(WebServiceData webServiceData, string methodName)
		{
			WebServiceMethodData methodData = webServiceData.GetMethodData(methodName);
			RestHandler restHandler;
			if (methodData.RequiresSession)
			{
				restHandler = new RestHandlerWithSession();
			}
			else
			{
				restHandler = new RestHandler();
			}
			restHandler._webServiceMethodData = methodData;
			return restHandler;
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x0002B55C File Offset: 0x0002975C
		private static void InitializeCachePolicy(WebServiceMethodData methodData, HttpContext context)
		{
			int cacheDuration = methodData.CacheDuration;
			if (cacheDuration <= 0)
			{
				context.Response.Cache.SetNoServerCaching();
				context.Response.Cache.SetMaxAge(TimeSpan.Zero);
				return;
			}
			context.Response.Cache.SetCacheability(HttpCacheability.Server);
			context.Response.Cache.SetExpires(DateTime.Now.AddSeconds((double)cacheDuration));
			context.Response.Cache.SetSlidingExpiration(false);
			context.Response.Cache.SetValidUntilExpires(true);
			if (methodData.ParameterDatas.Count > 0)
			{
				context.Response.Cache.VaryByParams["*"] = true;
				return;
			}
			context.Response.Cache.VaryByParams.IgnoreParams = true;
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x0002B630 File Offset: 0x00029830
		private static IDictionary<string, object> GetRawParamsFromGetRequest(HttpContext context, JavaScriptSerializer serializer, WebServiceMethodData methodData)
		{
			NameValueCollection queryString = context.Request.QueryString;
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			foreach (WebServiceParameterData webServiceParameterData in methodData.ParameterDatas)
			{
				string name = webServiceParameterData.ParameterInfo.Name;
				string text = queryString[name];
				if (text != null)
				{
					dictionary.Add(name, serializer.DeserializeObject(text));
				}
			}
			return dictionary;
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x0002B6B4 File Offset: 0x000298B4
		private static IDictionary<string, object> GetRawParamsFromPostRequest(HttpContext context, JavaScriptSerializer serializer)
		{
			TextReader textReader = new StreamReader(context.Request.InputStream);
			string text = textReader.ReadToEnd();
			if (string.IsNullOrEmpty(text))
			{
				return new Dictionary<string, object>();
			}
			return serializer.Deserialize<IDictionary<string, object>>(text);
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x0002B6F0 File Offset: 0x000298F0
		private static IDictionary<string, object> GetRawParams(WebServiceMethodData methodData, HttpContext context)
		{
			if (methodData.UseGet)
			{
				if (context.Request.HttpMethod == "GET")
				{
					return RestHandler.GetRawParamsFromGetRequest(context, methodData.Owner.Serializer, methodData);
				}
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, AtlasWeb.WebService_InvalidVerbRequest, new object[]
				{
					methodData.MethodName,
					"POST"
				}));
			}
			else
			{
				if (context.Request.HttpMethod == "POST")
				{
					return RestHandler.GetRawParamsFromPostRequest(context, methodData.Owner.Serializer);
				}
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, AtlasWeb.WebService_InvalidVerbRequest, new object[]
				{
					methodData.MethodName,
					"GET"
				}));
			}
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x0002B7B0 File Offset: 0x000299B0
		private static void InvokeMethod(HttpContext context, WebServiceMethodData methodData, IDictionary<string, object> rawParams)
		{
			RestHandler.InitializeCachePolicy(methodData, context);
			object target = null;
			if (!methodData.IsStatic)
			{
				target = Activator.CreateInstance(methodData.Owner.TypeData.Type);
			}
			object obj = methodData.CallMethodFromRawParams(target, rawParams);
			string text = null;
			string contentType;
			if (methodData.UseXmlResponse)
			{
				text = (obj as string);
				if (text == null || methodData.XmlSerializeString)
				{
					try
					{
						text = ServicesUtilities.XmlSerializeObjectToString(obj);
					}
					catch (Exception ex)
					{
						throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, AtlasWeb.WebService_InvalidXmlReturnType, new object[]
						{
							methodData.MethodName,
							obj.GetType().FullName,
							ex.Message
						}));
					}
				}
				contentType = "text/xml";
			}
			else
			{
				text = "{\"d\":" + methodData.Owner.Serializer.Serialize(obj) + "}";
				contentType = "application/json";
			}
			context.Response.ContentType = contentType;
			if (text != null)
			{
				context.Response.Write(text);
			}
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x0002B8AC File Offset: 0x00029AAC
		internal static void ExecuteWebServiceCall(HttpContext context, WebServiceMethodData methodData)
		{
			try
			{
				NamedPermissionSet namedPermissionSet = HttpRuntime.NamedPermissionSet;
				if (namedPermissionSet != null)
				{
					namedPermissionSet.PermitOnly();
				}
				IDictionary<string, object> rawParams = RestHandler.GetRawParams(methodData, context);
				RestHandler.InvokeMethod(context, methodData, rawParams);
			}
			catch (Exception ex)
			{
				RestHandler.WriteExceptionJsonString(context, ex);
			}
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x0002B8F4 File Offset: 0x00029AF4
		private static object BuildWebServiceError(string msg, string stack, string type)
		{
			OrderedDictionary orderedDictionary = new OrderedDictionary();
			orderedDictionary["Message"] = msg;
			orderedDictionary["StackTrace"] = stack;
			orderedDictionary["ExceptionType"] = type;
			return orderedDictionary;
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x0002B92C File Offset: 0x00029B2C
		internal static void WriteExceptionJsonString(HttpContext context, Exception ex)
		{
			RestHandler.WriteExceptionJsonString(context, ex, 500);
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x0002B93C File Offset: 0x00029B3C
		internal static void WriteExceptionJsonString(HttpContext context, Exception ex, int statusCode)
		{
			string charset = context.Response.Charset;
			context.Response.ClearHeaders();
			context.Response.ClearContent();
			context.Response.Clear();
			context.Response.StatusCode = statusCode;
			context.Response.StatusDescription = HttpWorkerRequest.GetStatusDescription(statusCode);
			context.Response.ContentType = "application/json";
			context.Response.AddHeader("jsonerror", "true");
			context.Response.Charset = charset;
			context.Response.TrySkipIisCustomErrors = true;
			using (StreamWriter streamWriter = new StreamWriter(context.Response.OutputStream, new UTF8Encoding(false)))
			{
				if (ex is TargetInvocationException)
				{
					ex = ex.InnerException;
				}
				if (context.IsCustomErrorEnabled)
				{
					streamWriter.Write(JavaScriptSerializer.SerializeInternal(RestHandler.BuildWebServiceError(AtlasWeb.WebService_Error, string.Empty, string.Empty)));
				}
				else
				{
					streamWriter.Write(JavaScriptSerializer.SerializeInternal(RestHandler.BuildWebServiceError(ex.Message, ex.StackTrace, ex.GetType().FullName)));
				}
				streamWriter.Flush();
			}
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x0002BA6C File Offset: 0x00029C6C
		public void ProcessRequest(HttpContext context)
		{
			RestHandler.ExecuteWebServiceCall(context, this._webServiceMethodData);
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06000CF2 RID: 3314 RVA: 0x0001359B File Offset: 0x0001179B
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000393 RID: 915
		private WebServiceMethodData _webServiceMethodData;
	}
}
