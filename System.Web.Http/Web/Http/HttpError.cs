using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.ModelBinding;
using System.Web.Http.Properties;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Web.Http
{
	// Token: 0x020000D2 RID: 210
	[XmlRoot("Error")]
	public sealed class HttpError : Dictionary<string, object>, IXmlSerializable
	{
		// Token: 0x06000519 RID: 1305 RVA: 0x00010A09 File Offset: 0x0000EC09
		public HttpError() : base(StringComparer.OrdinalIgnoreCase)
		{
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00010A16 File Offset: 0x0000EC16
		public HttpError(string message) : this()
		{
			if (message == null)
			{
				throw Error.ArgumentNull("message");
			}
			this.Message = message;
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00010A34 File Offset: 0x0000EC34
		public HttpError(Exception exception, bool includeErrorDetail) : this()
		{
			if (exception == null)
			{
				throw Error.ArgumentNull("exception");
			}
			this.Message = SRResources.ErrorOccurred;
			if (includeErrorDetail)
			{
				base.Add(HttpErrorKeys.ExceptionMessageKey, exception.Message);
				base.Add(HttpErrorKeys.ExceptionTypeKey, exception.GetType().FullName);
				base.Add(HttpErrorKeys.StackTraceKey, exception.StackTrace);
				if (exception.InnerException != null)
				{
					base.Add(HttpErrorKeys.InnerExceptionKey, new HttpError(exception.InnerException, includeErrorDetail));
				}
			}
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x00010AFC File Offset: 0x0000ECFC
		public HttpError(ModelStateDictionary modelState, bool includeErrorDetail) : this()
		{
			if (modelState == null)
			{
				throw Error.ArgumentNull("modelState");
			}
			if (modelState.IsValid)
			{
				throw Error.Argument("modelState", SRResources.ValidModelState, new object[0]);
			}
			this.Message = SRResources.BadRequest;
			HttpError httpError = new HttpError();
			foreach (KeyValuePair<string, ModelState> keyValuePair in modelState)
			{
				string key = keyValuePair.Key;
				ModelErrorCollection errors = keyValuePair.Value.Errors;
				if (errors != null && errors.Count > 0)
				{
					IEnumerable<string> value = errors.Select(delegate(ModelError error)
					{
						if (includeErrorDetail && error.Exception != null)
						{
							return error.Exception.Message;
						}
						if (!string.IsNullOrEmpty(error.ErrorMessage))
						{
							return error.ErrorMessage;
						}
						return SRResources.ErrorOccurred;
					}).ToArray<string>();
					httpError.Add(key, value);
				}
			}
			base.Add(HttpErrorKeys.ModelStateKey, httpError);
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00010BF0 File Offset: 0x0000EDF0
		internal HttpError(string message, string messageDetail) : this(message)
		{
			if (messageDetail == null)
			{
				throw Error.ArgumentNull("message");
			}
			base.Add(HttpErrorKeys.MessageDetailKey, messageDetail);
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x00010C13 File Offset: 0x0000EE13
		// (set) Token: 0x0600051F RID: 1311 RVA: 0x00010C20 File Offset: 0x0000EE20
		public string Message
		{
			get
			{
				return this.GetPropertyValue<string>(HttpErrorKeys.MessageKey);
			}
			set
			{
				base[HttpErrorKeys.MessageKey] = value;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x00010C2E File Offset: 0x0000EE2E
		public HttpError ModelState
		{
			get
			{
				return this.GetPropertyValue<HttpError>(HttpErrorKeys.ModelStateKey);
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x00010C3B File Offset: 0x0000EE3B
		// (set) Token: 0x06000522 RID: 1314 RVA: 0x00010C48 File Offset: 0x0000EE48
		public string MessageDetail
		{
			get
			{
				return this.GetPropertyValue<string>(HttpErrorKeys.MessageDetailKey);
			}
			set
			{
				base[HttpErrorKeys.MessageDetailKey] = value;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x00010C56 File Offset: 0x0000EE56
		// (set) Token: 0x06000524 RID: 1316 RVA: 0x00010C63 File Offset: 0x0000EE63
		public string ExceptionMessage
		{
			get
			{
				return this.GetPropertyValue<string>(HttpErrorKeys.ExceptionMessageKey);
			}
			set
			{
				base[HttpErrorKeys.ExceptionMessageKey] = value;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x00010C71 File Offset: 0x0000EE71
		// (set) Token: 0x06000526 RID: 1318 RVA: 0x00010C7E File Offset: 0x0000EE7E
		public string ExceptionType
		{
			get
			{
				return this.GetPropertyValue<string>(HttpErrorKeys.ExceptionTypeKey);
			}
			set
			{
				base[HttpErrorKeys.ExceptionTypeKey] = value;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x00010C8C File Offset: 0x0000EE8C
		// (set) Token: 0x06000528 RID: 1320 RVA: 0x00010C99 File Offset: 0x0000EE99
		public string StackTrace
		{
			get
			{
				return this.GetPropertyValue<string>(HttpErrorKeys.StackTraceKey);
			}
			set
			{
				base[HttpErrorKeys.StackTraceKey] = value;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x00010CA7 File Offset: 0x0000EEA7
		public HttpError InnerException
		{
			get
			{
				return this.GetPropertyValue<HttpError>(HttpErrorKeys.InnerExceptionKey);
			}
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00010CB4 File Offset: 0x0000EEB4
		public TValue GetPropertyValue<TValue>(string key)
		{
			TValue result;
			if (this.TryGetValue(key, out result))
			{
				return result;
			}
			return default(TValue);
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00010CD7 File Offset: 0x0000EED7
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x00010CDC File Offset: 0x0000EEDC
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			if (reader.IsEmptyElement)
			{
				reader.Read();
				return;
			}
			reader.ReadStartElement();
			while (reader.NodeType != XmlNodeType.EndElement)
			{
				string key = XmlConvert.DecodeName(reader.LocalName);
				string value = reader.ReadInnerXml();
				base.Add(key, value);
				reader.MoveToContent();
			}
			reader.ReadEndElement();
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00010D34 File Offset: 0x0000EF34
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			foreach (KeyValuePair<string, object> keyValuePair in this)
			{
				string key = keyValuePair.Key;
				object value = keyValuePair.Value;
				writer.WriteStartElement(XmlConvert.EncodeLocalName(key));
				if (value != null)
				{
					HttpError httpError = value as HttpError;
					if (httpError == null)
					{
						writer.WriteValue(value);
					}
					else
					{
						((IXmlSerializable)httpError).WriteXml(writer);
					}
				}
				writer.WriteEndElement();
			}
		}
	}
}
