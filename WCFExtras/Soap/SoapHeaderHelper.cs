using System;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using WCFExtras.Utils;

namespace WCFExtras.Soap
{
	// Token: 0x02000007 RID: 7
	public class SoapHeaderHelper
	{
		// Token: 0x06000023 RID: 35 RVA: 0x00002A22 File Offset: 0x00000C22
		public SoapHeaderHelper(Type t)
		{
			this.type = t;
			this.headerNamespace = SoapHeaderHelper.GetNamespace(t);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002A40 File Offset: 0x00000C40
		public object GetInputHeader(string name)
		{
			return this.GetHeader(name, OperationContext.Current.IncomingMessageHeaders);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002A64 File Offset: 0x00000C64
		internal object GetHeader(string name, MessageHeaders headers)
		{
			int num = headers.FindHeader(name, this.headerNamespace);
			object result;
			if (num >= 0)
			{
				XmlObjectSerializer serializer = new DataContractSerializer(this.type, name, this.headerNamespace, null, int.MaxValue, false, false, null);
				result = headers.GetHeader<object>(num, serializer);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002AB5 File Offset: 0x00000CB5
		public void SetOutputHeader(string name, object value)
		{
			this.AddHeader(name, value, OperationContext.Current.OutgoingMessageHeaders);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002ACC File Offset: 0x00000CCC
		internal void AddHeader(string name, object value, MessageHeaders headers)
		{
			MessageHeader header = MessageHeader.CreateHeader(name, this.headerNamespace, value);
			headers.Add(header);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002AF0 File Offset: 0x00000CF0
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
			string result;
			if (text != null)
			{
				result = text;
			}
			else
			{
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002B70 File Offset: 0x00000D70
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

		// Token: 0x04000006 RID: 6
		private Type type;

		// Token: 0x04000007 RID: 7
		private string headerNamespace;
	}
}
