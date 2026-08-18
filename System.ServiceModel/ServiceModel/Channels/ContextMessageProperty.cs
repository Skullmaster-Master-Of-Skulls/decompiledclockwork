using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007B3 RID: 1971
	[TypeForwardedFrom("System.WorkflowServices, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	[Serializable]
	public class ContextMessageProperty : IMessageProperty
	{
		// Token: 0x06004A87 RID: 19079 RVA: 0x00111F19 File Offset: 0x00110119
		public ContextMessageProperty()
		{
			this.contextStore = new ContextDictionary();
		}

		// Token: 0x06004A88 RID: 19080 RVA: 0x00111F2C File Offset: 0x0011012C
		public ContextMessageProperty(IDictionary<string, string> context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			this.contextStore = new ContextDictionary(context);
		}

		// Token: 0x170012C1 RID: 4801
		// (get) Token: 0x06004A89 RID: 19081 RVA: 0x00111F53 File Offset: 0x00110153
		public static string Name
		{
			get
			{
				return "ContextMessageProperty";
			}
		}

		// Token: 0x170012C2 RID: 4802
		// (get) Token: 0x06004A8A RID: 19082 RVA: 0x00111F5A File Offset: 0x0011015A
		public IDictionary<string, string> Context
		{
			get
			{
				return this.contextStore;
			}
		}

		// Token: 0x170012C3 RID: 4803
		// (get) Token: 0x06004A8B RID: 19083 RVA: 0x00111F64 File Offset: 0x00110164
		internal static ContextMessageProperty Empty
		{
			get
			{
				if (ContextMessageProperty.empty == null)
				{
					ContextMessageProperty.empty = new ContextMessageProperty
					{
						contextStore = ContextDictionary.Empty
					};
				}
				return ContextMessageProperty.empty;
			}
		}

		// Token: 0x06004A8C RID: 19084 RVA: 0x00111F94 File Offset: 0x00110194
		public static bool TryCreateFromHttpCookieHeader(string httpCookieHeader, out ContextMessageProperty context)
		{
			return ContextProtocol.HttpCookieToolbox.TryCreateFromHttpCookieHeader(httpCookieHeader, out context);
		}

		// Token: 0x06004A8D RID: 19085 RVA: 0x00111F9D File Offset: 0x0011019D
		public static bool TryGet(Message message, out ContextMessageProperty contextMessageProperty)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			return ContextMessageProperty.TryGet(message.Properties, out contextMessageProperty);
		}

		// Token: 0x06004A8E RID: 19086 RVA: 0x00111FC0 File Offset: 0x001101C0
		public static bool TryGet(MessageProperties properties, out ContextMessageProperty contextMessageProperty)
		{
			if (properties == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("properties");
			}
			object obj = null;
			if (properties.TryGetValue("ContextMessageProperty", out obj))
			{
				contextMessageProperty = (obj as ContextMessageProperty);
			}
			else
			{
				contextMessageProperty = null;
			}
			return contextMessageProperty != null;
		}

		// Token: 0x06004A8F RID: 19087 RVA: 0x00112003 File Offset: 0x00110203
		public void AddOrReplaceInMessage(Message message)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			this.AddOrReplaceInMessageProperties(message.Properties);
		}

		// Token: 0x06004A90 RID: 19088 RVA: 0x00112024 File Offset: 0x00110224
		public void AddOrReplaceInMessageProperties(MessageProperties properties)
		{
			if (properties == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("properties");
			}
			properties["ContextMessageProperty"] = this;
		}

		// Token: 0x06004A91 RID: 19089 RVA: 0x00112045 File Offset: 0x00110245
		public IMessageProperty CreateCopy()
		{
			return new ContextMessageProperty(this.Context);
		}

		// Token: 0x04002F1C RID: 12060
		internal const string InstanceIdKey = "instanceId";

		// Token: 0x04002F1D RID: 12061
		private const string PropertyName = "ContextMessageProperty";

		// Token: 0x04002F1E RID: 12062
		private static ContextMessageProperty empty;

		// Token: 0x04002F1F RID: 12063
		private IDictionary<string, string> contextStore;
	}
}
