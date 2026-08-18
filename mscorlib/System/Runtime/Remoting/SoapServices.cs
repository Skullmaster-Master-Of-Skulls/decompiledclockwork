using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata;
using System.Security.Permissions;
using System.Text;

namespace System.Runtime.Remoting
{
	// Token: 0x02000773 RID: 1907
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	public class SoapServices
	{
		// Token: 0x0600440A RID: 17418 RVA: 0x000E8C73 File Offset: 0x000E7C73
		private SoapServices()
		{
		}

		// Token: 0x0600440B RID: 17419 RVA: 0x000E8C7B File Offset: 0x000E7C7B
		private static string CreateKey(string elementName, string elementNamespace)
		{
			if (elementNamespace == null)
			{
				return elementName;
			}
			return elementName + " " + elementNamespace;
		}

		// Token: 0x0600440C RID: 17420 RVA: 0x000E8C8E File Offset: 0x000E7C8E
		public static void RegisterInteropXmlElement(string xmlElement, string xmlNamespace, Type type)
		{
			SoapServices._interopXmlElementToType[SoapServices.CreateKey(xmlElement, xmlNamespace)] = type;
			SoapServices._interopTypeToXmlElement[type] = new SoapServices.XmlEntry(xmlElement, xmlNamespace);
		}

		// Token: 0x0600440D RID: 17421 RVA: 0x000E8CB4 File Offset: 0x000E7CB4
		public static void RegisterInteropXmlType(string xmlType, string xmlTypeNamespace, Type type)
		{
			SoapServices._interopXmlTypeToType[SoapServices.CreateKey(xmlType, xmlTypeNamespace)] = type;
			SoapServices._interopTypeToXmlType[type] = new SoapServices.XmlEntry(xmlType, xmlTypeNamespace);
		}

		// Token: 0x0600440E RID: 17422 RVA: 0x000E8CDC File Offset: 0x000E7CDC
		public static void PreLoad(Type type)
		{
			MethodInfo[] methods = type.GetMethods();
			foreach (MethodInfo mb in methods)
			{
				SoapServices.RegisterSoapActionForMethodBase(mb);
			}
			SoapTypeAttribute soapTypeAttribute = (SoapTypeAttribute)InternalRemotingServices.GetCachedSoapAttribute(type);
			if (soapTypeAttribute.IsInteropXmlElement())
			{
				SoapServices.RegisterInteropXmlElement(soapTypeAttribute.XmlElementName, soapTypeAttribute.XmlNamespace, type);
			}
			if (soapTypeAttribute.IsInteropXmlType())
			{
				SoapServices.RegisterInteropXmlType(soapTypeAttribute.XmlTypeName, soapTypeAttribute.XmlTypeNamespace, type);
			}
			int num = 0;
			SoapServices.XmlToFieldTypeMap xmlToFieldTypeMap = new SoapServices.XmlToFieldTypeMap();
			foreach (FieldInfo fieldInfo in type.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				SoapFieldAttribute soapFieldAttribute = (SoapFieldAttribute)InternalRemotingServices.GetCachedSoapAttribute(fieldInfo);
				if (soapFieldAttribute.IsInteropXmlElement())
				{
					string xmlElementName = soapFieldAttribute.XmlElementName;
					string xmlNamespace = soapFieldAttribute.XmlNamespace;
					if (soapFieldAttribute.UseAttribute)
					{
						xmlToFieldTypeMap.AddXmlAttribute(fieldInfo.FieldType, fieldInfo.Name, xmlElementName, xmlNamespace);
					}
					else
					{
						xmlToFieldTypeMap.AddXmlElement(fieldInfo.FieldType, fieldInfo.Name, xmlElementName, xmlNamespace);
					}
					num++;
				}
			}
			if (num > 0)
			{
				SoapServices._xmlToFieldTypeMap[type] = xmlToFieldTypeMap;
			}
		}

		// Token: 0x0600440F RID: 17423 RVA: 0x000E8DFC File Offset: 0x000E7DFC
		public static void PreLoad(Assembly assembly)
		{
			Type[] types = assembly.GetTypes();
			foreach (Type type in types)
			{
				SoapServices.PreLoad(type);
			}
		}

		// Token: 0x06004410 RID: 17424 RVA: 0x000E8E2A File Offset: 0x000E7E2A
		public static Type GetInteropTypeFromXmlElement(string xmlElement, string xmlNamespace)
		{
			return (Type)SoapServices._interopXmlElementToType[SoapServices.CreateKey(xmlElement, xmlNamespace)];
		}

		// Token: 0x06004411 RID: 17425 RVA: 0x000E8E42 File Offset: 0x000E7E42
		public static Type GetInteropTypeFromXmlType(string xmlType, string xmlTypeNamespace)
		{
			return (Type)SoapServices._interopXmlTypeToType[SoapServices.CreateKey(xmlType, xmlTypeNamespace)];
		}

		// Token: 0x06004412 RID: 17426 RVA: 0x000E8E5C File Offset: 0x000E7E5C
		public static void GetInteropFieldTypeAndNameFromXmlElement(Type containingType, string xmlElement, string xmlNamespace, out Type type, out string name)
		{
			if (containingType == null)
			{
				type = null;
				name = null;
				return;
			}
			SoapServices.XmlToFieldTypeMap xmlToFieldTypeMap = (SoapServices.XmlToFieldTypeMap)SoapServices._xmlToFieldTypeMap[containingType];
			if (xmlToFieldTypeMap != null)
			{
				xmlToFieldTypeMap.GetFieldTypeAndNameFromXmlElement(xmlElement, xmlNamespace, out type, out name);
				return;
			}
			type = null;
			name = null;
		}

		// Token: 0x06004413 RID: 17427 RVA: 0x000E8E9C File Offset: 0x000E7E9C
		public static void GetInteropFieldTypeAndNameFromXmlAttribute(Type containingType, string xmlAttribute, string xmlNamespace, out Type type, out string name)
		{
			if (containingType == null)
			{
				type = null;
				name = null;
				return;
			}
			SoapServices.XmlToFieldTypeMap xmlToFieldTypeMap = (SoapServices.XmlToFieldTypeMap)SoapServices._xmlToFieldTypeMap[containingType];
			if (xmlToFieldTypeMap != null)
			{
				xmlToFieldTypeMap.GetFieldTypeAndNameFromXmlAttribute(xmlAttribute, xmlNamespace, out type, out name);
				return;
			}
			type = null;
			name = null;
		}

		// Token: 0x06004414 RID: 17428 RVA: 0x000E8EDC File Offset: 0x000E7EDC
		public static bool GetXmlElementForInteropType(Type type, out string xmlElement, out string xmlNamespace)
		{
			SoapServices.XmlEntry xmlEntry = (SoapServices.XmlEntry)SoapServices._interopTypeToXmlElement[type];
			if (xmlEntry != null)
			{
				xmlElement = xmlEntry.Name;
				xmlNamespace = xmlEntry.Namespace;
				return true;
			}
			SoapTypeAttribute soapTypeAttribute = (SoapTypeAttribute)InternalRemotingServices.GetCachedSoapAttribute(type);
			if (soapTypeAttribute.IsInteropXmlElement())
			{
				xmlElement = soapTypeAttribute.XmlElementName;
				xmlNamespace = soapTypeAttribute.XmlNamespace;
				return true;
			}
			xmlElement = null;
			xmlNamespace = null;
			return false;
		}

		// Token: 0x06004415 RID: 17429 RVA: 0x000E8F3C File Offset: 0x000E7F3C
		public static bool GetXmlTypeForInteropType(Type type, out string xmlType, out string xmlTypeNamespace)
		{
			SoapServices.XmlEntry xmlEntry = (SoapServices.XmlEntry)SoapServices._interopTypeToXmlType[type];
			if (xmlEntry != null)
			{
				xmlType = xmlEntry.Name;
				xmlTypeNamespace = xmlEntry.Namespace;
				return true;
			}
			SoapTypeAttribute soapTypeAttribute = (SoapTypeAttribute)InternalRemotingServices.GetCachedSoapAttribute(type);
			if (soapTypeAttribute.IsInteropXmlType())
			{
				xmlType = soapTypeAttribute.XmlTypeName;
				xmlTypeNamespace = soapTypeAttribute.XmlTypeNamespace;
				return true;
			}
			xmlType = null;
			xmlTypeNamespace = null;
			return false;
		}

		// Token: 0x06004416 RID: 17430 RVA: 0x000E8F9C File Offset: 0x000E7F9C
		public static string GetXmlNamespaceForMethodCall(MethodBase mb)
		{
			SoapMethodAttribute soapMethodAttribute = (SoapMethodAttribute)InternalRemotingServices.GetCachedSoapAttribute(mb);
			return soapMethodAttribute.XmlNamespace;
		}

		// Token: 0x06004417 RID: 17431 RVA: 0x000E8FBC File Offset: 0x000E7FBC
		public static string GetXmlNamespaceForMethodResponse(MethodBase mb)
		{
			SoapMethodAttribute soapMethodAttribute = (SoapMethodAttribute)InternalRemotingServices.GetCachedSoapAttribute(mb);
			return soapMethodAttribute.ResponseXmlNamespace;
		}

		// Token: 0x06004418 RID: 17432 RVA: 0x000E8FDC File Offset: 0x000E7FDC
		public static void RegisterSoapActionForMethodBase(MethodBase mb)
		{
			SoapMethodAttribute soapMethodAttribute = (SoapMethodAttribute)InternalRemotingServices.GetCachedSoapAttribute(mb);
			if (soapMethodAttribute.SoapActionExplicitySet)
			{
				SoapServices.RegisterSoapActionForMethodBase(mb, soapMethodAttribute.SoapAction);
			}
		}

		// Token: 0x06004419 RID: 17433 RVA: 0x000E900C File Offset: 0x000E800C
		public static void RegisterSoapActionForMethodBase(MethodBase mb, string soapAction)
		{
			if (soapAction != null)
			{
				SoapServices._methodBaseToSoapAction[mb] = soapAction;
				ArrayList arrayList = (ArrayList)SoapServices._soapActionToMethodBase[soapAction];
				if (arrayList == null)
				{
					lock (SoapServices._soapActionToMethodBase)
					{
						arrayList = ArrayList.Synchronized(new ArrayList());
						SoapServices._soapActionToMethodBase[soapAction] = arrayList;
					}
				}
				arrayList.Add(mb);
			}
		}

		// Token: 0x0600441A RID: 17434 RVA: 0x000E9080 File Offset: 0x000E8080
		public static string GetSoapActionFromMethodBase(MethodBase mb)
		{
			string text = (string)SoapServices._methodBaseToSoapAction[mb];
			if (text == null)
			{
				SoapMethodAttribute soapMethodAttribute = (SoapMethodAttribute)InternalRemotingServices.GetCachedSoapAttribute(mb);
				text = soapMethodAttribute.SoapAction;
			}
			return text;
		}

		// Token: 0x0600441B RID: 17435 RVA: 0x000E90B8 File Offset: 0x000E80B8
		public static bool IsSoapActionValidForMethodBase(string soapAction, MethodBase mb)
		{
			if (soapAction[0] == '"' && soapAction[soapAction.Length - 1] == '"')
			{
				soapAction = soapAction.Substring(1, soapAction.Length - 2);
			}
			SoapMethodAttribute soapMethodAttribute = (SoapMethodAttribute)InternalRemotingServices.GetCachedSoapAttribute(mb);
			if (string.CompareOrdinal(soapMethodAttribute.SoapAction, soapAction) == 0)
			{
				return true;
			}
			string text = (string)SoapServices._methodBaseToSoapAction[mb];
			if (text != null && string.CompareOrdinal(text, soapAction) == 0)
			{
				return true;
			}
			string[] array = soapAction.Split(new char[]
			{
				'#'
			});
			if (array.Length != 2)
			{
				return false;
			}
			bool flag;
			string typeNameForSoapActionNamespace = XmlNamespaceEncoder.GetTypeNameForSoapActionNamespace(array[0], out flag);
			if (typeNameForSoapActionNamespace == null)
			{
				return false;
			}
			string value = array[1];
			Type declaringType = mb.DeclaringType;
			string text2 = declaringType.FullName;
			if (flag)
			{
				text2 = text2 + ", " + declaringType.Module.Assembly.nGetSimpleName();
			}
			return text2.Equals(typeNameForSoapActionNamespace) && mb.Name.Equals(value);
		}

		// Token: 0x0600441C RID: 17436 RVA: 0x000E91B4 File Offset: 0x000E81B4
		public static bool GetTypeAndMethodNameFromSoapAction(string soapAction, out string typeName, out string methodName)
		{
			if (soapAction[0] == '"' && soapAction[soapAction.Length - 1] == '"')
			{
				soapAction = soapAction.Substring(1, soapAction.Length - 2);
			}
			ArrayList arrayList = (ArrayList)SoapServices._soapActionToMethodBase[soapAction];
			if (arrayList != null)
			{
				if (arrayList.Count > 1)
				{
					typeName = null;
					methodName = null;
					return false;
				}
				MethodBase methodBase = (MethodBase)arrayList[0];
				if (methodBase != null)
				{
					Type declaringType = methodBase.DeclaringType;
					typeName = declaringType.FullName + ", " + declaringType.Module.Assembly.nGetSimpleName();
					methodName = methodBase.Name;
					return true;
				}
			}
			string[] array = soapAction.Split(new char[]
			{
				'#'
			});
			if (array.Length != 2)
			{
				typeName = null;
				methodName = null;
				return false;
			}
			bool flag;
			typeName = XmlNamespaceEncoder.GetTypeNameForSoapActionNamespace(array[0], out flag);
			if (typeName == null)
			{
				methodName = null;
				return false;
			}
			methodName = array[1];
			return true;
		}

		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x0600441D RID: 17437 RVA: 0x000E9297 File Offset: 0x000E8297
		public static string XmlNsForClrType
		{
			get
			{
				return SoapServices.startNS;
			}
		}

		// Token: 0x17000BEA RID: 3050
		// (get) Token: 0x0600441E RID: 17438 RVA: 0x000E929E File Offset: 0x000E829E
		public static string XmlNsForClrTypeWithAssembly
		{
			get
			{
				return SoapServices.assemblyNS;
			}
		}

		// Token: 0x17000BEB RID: 3051
		// (get) Token: 0x0600441F RID: 17439 RVA: 0x000E92A5 File Offset: 0x000E82A5
		public static string XmlNsForClrTypeWithNs
		{
			get
			{
				return SoapServices.namespaceNS;
			}
		}

		// Token: 0x17000BEC RID: 3052
		// (get) Token: 0x06004420 RID: 17440 RVA: 0x000E92AC File Offset: 0x000E82AC
		public static string XmlNsForClrTypeWithNsAndAssembly
		{
			get
			{
				return SoapServices.fullNS;
			}
		}

		// Token: 0x06004421 RID: 17441 RVA: 0x000E92B3 File Offset: 0x000E82B3
		public static bool IsClrTypeNamespace(string namespaceString)
		{
			return namespaceString.StartsWith(SoapServices.startNS, StringComparison.Ordinal);
		}

		// Token: 0x06004422 RID: 17442 RVA: 0x000E92C8 File Offset: 0x000E82C8
		public static string CodeXmlNamespaceForClrTypeNamespace(string typeNamespace, string assemblyName)
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			if (SoapServices.IsNameNull(typeNamespace))
			{
				if (SoapServices.IsNameNull(assemblyName))
				{
					throw new ArgumentNullException("typeNamespace,assemblyName");
				}
				stringBuilder.Append(SoapServices.assemblyNS);
				SoapServices.UriEncode(assemblyName, stringBuilder);
			}
			else if (SoapServices.IsNameNull(assemblyName))
			{
				stringBuilder.Append(SoapServices.namespaceNS);
				stringBuilder.Append(typeNamespace);
			}
			else
			{
				stringBuilder.Append(SoapServices.fullNS);
				if (typeNamespace[0] == '.')
				{
					stringBuilder.Append(typeNamespace.Substring(1));
				}
				else
				{
					stringBuilder.Append(typeNamespace);
				}
				stringBuilder.Append('/');
				SoapServices.UriEncode(assemblyName, stringBuilder);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06004423 RID: 17443 RVA: 0x000E9374 File Offset: 0x000E8374
		public static bool DecodeXmlNamespaceForClrTypeNamespace(string inNamespace, out string typeNamespace, out string assemblyName)
		{
			if (SoapServices.IsNameNull(inNamespace))
			{
				throw new ArgumentNullException("inNamespace");
			}
			assemblyName = null;
			typeNamespace = "";
			if (inNamespace.StartsWith(SoapServices.assemblyNS, StringComparison.Ordinal))
			{
				assemblyName = SoapServices.UriDecode(inNamespace.Substring(SoapServices.assemblyNS.Length));
			}
			else if (inNamespace.StartsWith(SoapServices.namespaceNS, StringComparison.Ordinal))
			{
				typeNamespace = inNamespace.Substring(SoapServices.namespaceNS.Length);
			}
			else
			{
				if (!inNamespace.StartsWith(SoapServices.fullNS, StringComparison.Ordinal))
				{
					return false;
				}
				int num = inNamespace.IndexOf("/", SoapServices.fullNS.Length);
				typeNamespace = inNamespace.Substring(SoapServices.fullNS.Length, num - SoapServices.fullNS.Length);
				assemblyName = SoapServices.UriDecode(inNamespace.Substring(num + 1));
			}
			return true;
		}

		// Token: 0x06004424 RID: 17444 RVA: 0x000E9440 File Offset: 0x000E8440
		internal static void UriEncode(string value, StringBuilder sb)
		{
			if (value == null || value.Length == 0)
			{
				return;
			}
			for (int i = 0; i < value.Length; i++)
			{
				if (value[i] == ' ')
				{
					sb.Append("%20");
				}
				else if (value[i] == '=')
				{
					sb.Append("%3D");
				}
				else if (value[i] == ',')
				{
					sb.Append("%2C");
				}
				else
				{
					sb.Append(value[i]);
				}
			}
		}

		// Token: 0x06004425 RID: 17445 RVA: 0x000E94C4 File Offset: 0x000E84C4
		internal static string UriDecode(string value)
		{
			if (value == null || value.Length == 0)
			{
				return value;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < value.Length; i++)
			{
				if (value[i] == '%' && value.Length - i >= 3)
				{
					if (value[i + 1] == '2' && value[i + 2] == '0')
					{
						stringBuilder.Append(' ');
						i += 2;
					}
					else if (value[i + 1] == '3' && value[i + 2] == 'D')
					{
						stringBuilder.Append('=');
						i += 2;
					}
					else if (value[i + 1] == '2' && value[i + 2] == 'C')
					{
						stringBuilder.Append(',');
						i += 2;
					}
					else
					{
						stringBuilder.Append(value[i]);
					}
				}
				else
				{
					stringBuilder.Append(value[i]);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06004426 RID: 17446 RVA: 0x000E95B6 File Offset: 0x000E85B6
		private static bool IsNameNull(string name)
		{
			return name == null || name.Length == 0;
		}

		// Token: 0x04002212 RID: 8722
		private static Hashtable _interopXmlElementToType = Hashtable.Synchronized(new Hashtable());

		// Token: 0x04002213 RID: 8723
		private static Hashtable _interopTypeToXmlElement = Hashtable.Synchronized(new Hashtable());

		// Token: 0x04002214 RID: 8724
		private static Hashtable _interopXmlTypeToType = Hashtable.Synchronized(new Hashtable());

		// Token: 0x04002215 RID: 8725
		private static Hashtable _interopTypeToXmlType = Hashtable.Synchronized(new Hashtable());

		// Token: 0x04002216 RID: 8726
		private static Hashtable _xmlToFieldTypeMap = Hashtable.Synchronized(new Hashtable());

		// Token: 0x04002217 RID: 8727
		private static Hashtable _methodBaseToSoapAction = Hashtable.Synchronized(new Hashtable());

		// Token: 0x04002218 RID: 8728
		private static Hashtable _soapActionToMethodBase = Hashtable.Synchronized(new Hashtable());

		// Token: 0x04002219 RID: 8729
		internal static string startNS = "http://schemas.microsoft.com/clr/";

		// Token: 0x0400221A RID: 8730
		internal static string assemblyNS = "http://schemas.microsoft.com/clr/assem/";

		// Token: 0x0400221B RID: 8731
		internal static string namespaceNS = "http://schemas.microsoft.com/clr/ns/";

		// Token: 0x0400221C RID: 8732
		internal static string fullNS = "http://schemas.microsoft.com/clr/nsassem/";

		// Token: 0x02000774 RID: 1908
		private class XmlEntry
		{
			// Token: 0x06004428 RID: 17448 RVA: 0x000E9666 File Offset: 0x000E8666
			public XmlEntry(string name, string xmlNamespace)
			{
				this.Name = name;
				this.Namespace = xmlNamespace;
			}

			// Token: 0x0400221D RID: 8733
			public string Name;

			// Token: 0x0400221E RID: 8734
			public string Namespace;
		}

		// Token: 0x02000775 RID: 1909
		private class XmlToFieldTypeMap
		{
			// Token: 0x0600442A RID: 17450 RVA: 0x000E969A File Offset: 0x000E869A
			public void AddXmlElement(Type fieldType, string fieldName, string xmlElement, string xmlNamespace)
			{
				this._elements[SoapServices.CreateKey(xmlElement, xmlNamespace)] = new SoapServices.XmlToFieldTypeMap.FieldEntry(fieldType, fieldName);
			}

			// Token: 0x0600442B RID: 17451 RVA: 0x000E96B6 File Offset: 0x000E86B6
			public void AddXmlAttribute(Type fieldType, string fieldName, string xmlAttribute, string xmlNamespace)
			{
				this._attributes[SoapServices.CreateKey(xmlAttribute, xmlNamespace)] = new SoapServices.XmlToFieldTypeMap.FieldEntry(fieldType, fieldName);
			}

			// Token: 0x0600442C RID: 17452 RVA: 0x000E96D4 File Offset: 0x000E86D4
			public void GetFieldTypeAndNameFromXmlElement(string xmlElement, string xmlNamespace, out Type type, out string name)
			{
				SoapServices.XmlToFieldTypeMap.FieldEntry fieldEntry = (SoapServices.XmlToFieldTypeMap.FieldEntry)this._elements[SoapServices.CreateKey(xmlElement, xmlNamespace)];
				if (fieldEntry != null)
				{
					type = fieldEntry.Type;
					name = fieldEntry.Name;
					return;
				}
				type = null;
				name = null;
			}

			// Token: 0x0600442D RID: 17453 RVA: 0x000E9718 File Offset: 0x000E8718
			public void GetFieldTypeAndNameFromXmlAttribute(string xmlAttribute, string xmlNamespace, out Type type, out string name)
			{
				SoapServices.XmlToFieldTypeMap.FieldEntry fieldEntry = (SoapServices.XmlToFieldTypeMap.FieldEntry)this._attributes[SoapServices.CreateKey(xmlAttribute, xmlNamespace)];
				if (fieldEntry != null)
				{
					type = fieldEntry.Type;
					name = fieldEntry.Name;
					return;
				}
				type = null;
				name = null;
			}

			// Token: 0x0400221F RID: 8735
			private Hashtable _attributes = new Hashtable();

			// Token: 0x04002220 RID: 8736
			private Hashtable _elements = new Hashtable();

			// Token: 0x02000776 RID: 1910
			private class FieldEntry
			{
				// Token: 0x0600442E RID: 17454 RVA: 0x000E9759 File Offset: 0x000E8759
				public FieldEntry(Type type, string name)
				{
					this.Type = type;
					this.Name = name;
				}

				// Token: 0x04002221 RID: 8737
				public Type Type;

				// Token: 0x04002222 RID: 8738
				public string Name;
			}
		}
	}
}
