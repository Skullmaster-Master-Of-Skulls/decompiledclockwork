using System;
using System.Collections;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x020005AF RID: 1455
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class WebPartTransformerAttribute : Attribute
	{
		// Token: 0x0600499F RID: 18847 RVA: 0x000F4BEC File Offset: 0x000F2DEC
		public WebPartTransformerAttribute(Type consumerType, Type providerType)
		{
			if (consumerType == null)
			{
				throw new ArgumentNullException("consumerType");
			}
			if (providerType == null)
			{
				throw new ArgumentNullException("providerType");
			}
			this._consumerType = consumerType;
			this._providerType = providerType;
		}

		// Token: 0x170015A0 RID: 5536
		// (get) Token: 0x060049A0 RID: 18848 RVA: 0x000F4C2A File Offset: 0x000F2E2A
		public Type ConsumerType
		{
			get
			{
				return this._consumerType;
			}
		}

		// Token: 0x170015A1 RID: 5537
		// (get) Token: 0x060049A1 RID: 18849 RVA: 0x000F4C32 File Offset: 0x000F2E32
		public Type ProviderType
		{
			get
			{
				return this._providerType;
			}
		}

		// Token: 0x060049A2 RID: 18850 RVA: 0x000F4C3A File Offset: 0x000F2E3A
		public static Type GetConsumerType(Type transformerType)
		{
			return WebPartTransformerAttribute.GetTransformerTypes(transformerType)[0];
		}

		// Token: 0x060049A3 RID: 18851 RVA: 0x000F4C44 File Offset: 0x000F2E44
		public static Type GetProviderType(Type transformerType)
		{
			return WebPartTransformerAttribute.GetTransformerTypes(transformerType)[1];
		}

		// Token: 0x060049A4 RID: 18852 RVA: 0x000F4C50 File Offset: 0x000F2E50
		private static Type[] GetTransformerTypes(Type transformerType)
		{
			if (transformerType == null)
			{
				throw new ArgumentNullException("transformerType");
			}
			if (!transformerType.IsSubclassOf(typeof(WebPartTransformer)))
			{
				throw new InvalidOperationException(SR.GetString("WebPartTransformerAttribute_NotTransformer", new object[]
				{
					transformerType.FullName
				}));
			}
			Type[] array = (Type[])WebPartTransformerAttribute.transformerCache[transformerType];
			if (array == null)
			{
				array = WebPartTransformerAttribute.GetTransformerTypesFromAttribute(transformerType);
				WebPartTransformerAttribute.transformerCache[transformerType] = array;
			}
			return array;
		}

		// Token: 0x060049A5 RID: 18853 RVA: 0x000F4CCC File Offset: 0x000F2ECC
		private static Type[] GetTransformerTypesFromAttribute(Type transformerType)
		{
			Type[] array = new Type[2];
			object[] customAttributes = transformerType.GetCustomAttributes(typeof(WebPartTransformerAttribute), true);
			if (customAttributes.Length != 1)
			{
				throw new InvalidOperationException(SR.GetString("WebPartTransformerAttribute_Missing", new object[]
				{
					transformerType.FullName
				}));
			}
			WebPartTransformerAttribute webPartTransformerAttribute = (WebPartTransformerAttribute)customAttributes[0];
			if (webPartTransformerAttribute.ConsumerType == webPartTransformerAttribute.ProviderType)
			{
				throw new InvalidOperationException(SR.GetString("WebPartTransformerAttribute_SameTypes"));
			}
			array[0] = webPartTransformerAttribute.ConsumerType;
			array[1] = webPartTransformerAttribute.ProviderType;
			return array;
		}

		// Token: 0x040027B2 RID: 10162
		private static readonly Hashtable transformerCache = Hashtable.Synchronized(new Hashtable());

		// Token: 0x040027B3 RID: 10163
		private Type _consumerType;

		// Token: 0x040027B4 RID: 10164
		private Type _providerType;
	}
}
