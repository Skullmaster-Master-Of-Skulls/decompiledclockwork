using System;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using WCFExtrasPlus.Utils;

namespace WCFExtrasPlus.Soap
{
	// Token: 0x02000008 RID: 8
	public class SoapHeaderHelper
	{
		// Token: 0x0600001A RID: 26 RVA: 0x000024EC File Offset: 0x000006EC
		public SoapHeaderHelper(Type t)
		{
			this.type = t;
			this.headerNamespace = SoapHeaderHelper.GetNamespace(t);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002507 File Offset: 0x00000707
		public object GetInputHeader(string name)
		{
			return this.GetHeader(name, OperationContext.Current.IncomingMessageHeaders);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000251C File Offset: 0x0000071C
		internal object GetHeader(string name, MessageHeaders headers)
		{
			int num = headers.FindHeader(name, this.headerNamespace);
			if (num >= 0)
			{
				XmlObjectSerializer serializer = new DataContractSerializer(this.type, name, this.headerNamespace, null, int.MaxValue, false, false, null);
				return headers.GetHeader<object>(num, serializer);
			}
			return null;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002561 File Offset: 0x00000761
		public void SetOutputHeader(string name, object value)
		{
			this.AddHeader(name, value, OperationContext.Current.OutgoingMessageHeaders);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002578 File Offset: 0x00000778
		internal void AddHeader(string name, object value, MessageHeaders headers)
		{
			MessageHeader header = MessageHeader.CreateHeader(name, this.headerNamespace, value);
			headers.Add(header);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000259C File Offset: 0x0000079C
		internal static string GetNamespace(Type type)
		{
			string text = null;
			DataContractAttribute dataContractAttribute = ReflectionUtils.GetDataContractAttribute(type);
			if (dataContractAttribute != null)
			{
				text = dataContractAttribute.Namespace;
			}
			if (text == null)
			{
				string @namespace = type.Namespace;
				text = SoapHeaderHelper.GetGlobalDataContractNamespace(@namespace, type.Module);
				if (text == null)
				{
					text = SoapHeaderHelper.GetGlobalDataContractNamespace(@namespace, type.Assembly);
				}
			}
			if (text != null)
			{
				return text;
			}
			return string.Empty;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000025F0 File Offset: 0x000007F0
		private static string GetGlobalDataContractNamespace(string clrNs, ICustomAttributeProvider customAttribuetProvider)
		{
			foreach (ContractNamespaceAttribute contractNamespaceAttribute in customAttribuetProvider.GetCustomAttributes(typeof(ContractNamespaceAttribute), false))
			{
				string text = contractNamespaceAttribute.ClrNamespace;
				if (text == null)
				{
					text = string.Empty;
				}
				if (text == clrNs)
				{
					return contractNamespaceAttribute.ContractNamespace;
				}
			}
			return null;
		}

		// Token: 0x0400000A RID: 10
		private Type type;

		// Token: 0x0400000B RID: 11
		private string headerNamespace;
	}
}
