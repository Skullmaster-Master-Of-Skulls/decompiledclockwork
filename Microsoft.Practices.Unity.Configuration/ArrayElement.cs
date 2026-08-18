using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Xml;
using Microsoft.Practices.Unity.Configuration.ConfigurationHelpers;
using Microsoft.Practices.Unity.Configuration.Properties;

namespace Microsoft.Practices.Unity.Configuration
{
	// Token: 0x02000008 RID: 8
	public class ArrayElement : ParameterValueElement
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000024B0 File Offset: 0x000006B0
		// (set) Token: 0x06000024 RID: 36 RVA: 0x000024C2 File Offset: 0x000006C2
		[ConfigurationProperty("type", IsRequired = false)]
		public string TypeName
		{
			get
			{
				return (string)base["type"];
			}
			set
			{
				base["type"] = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000024D0 File Offset: 0x000006D0
		[ConfigurationProperty("", IsDefaultCollection = true)]
		public ParameterValueElementCollection Values
		{
			get
			{
				return (ParameterValueElementCollection)base[""];
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000024E4 File Offset: 0x000006E4
		public override void SerializeContent(XmlWriter writer)
		{
			writer.WriteAttributeIfNotEmpty("type", this.TypeName);
			foreach (ParameterValueElement element in this.Values)
			{
				ValueElementHelper.SerializeParameterValueElement(writer, element, true);
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002560 File Offset: 0x00000760
		public override InjectionParameterValue GetInjectionParameterValue(IUnityContainer container, Type parameterType)
		{
			this.GuardTypeIsAnArray(parameterType);
			Type elementType = this.GetElementType(parameterType);
			IEnumerable<InjectionParameterValue> source = from v in this.Values
			select v.GetInjectionParameterValue(container, elementType);
			if (elementType.IsGenericParameter)
			{
				return new GenericResolvedArrayParameter(elementType.Name, source.ToArray<InjectionParameterValue>());
			}
			return new ResolvedArrayParameter(elementType, source.ToArray<InjectionParameterValue>());
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000025DC File Offset: 0x000007DC
		private void GuardTypeIsAnArray(Type externalParameterType)
		{
			if (string.IsNullOrEmpty(this.TypeName) && !externalParameterType.IsArray)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.NotAnArray, new object[]
				{
					externalParameterType.Name
				}));
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002624 File Offset: 0x00000824
		private Type GetElementType(Type parameterType)
		{
			return TypeResolver.ResolveTypeWithDefault(this.TypeName, null) ?? parameterType.GetElementType();
		}

		// Token: 0x04000007 RID: 7
		private const string TypeNamePropertyName = "type";

		// Token: 0x04000008 RID: 8
		private const string ValuesPropertyName = "";
	}
}
