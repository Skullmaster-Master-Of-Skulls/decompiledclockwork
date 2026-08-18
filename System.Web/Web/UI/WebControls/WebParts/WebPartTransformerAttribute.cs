using System;
using System.Collections;
using System.Security.Permissions;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000742 RID: 1858
	[AttributeUsage(AttributeTargets.Class)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class WebPartTransformerAttribute : Attribute
	{
		// Token: 0x06005A29 RID: 23081 RVA: 0x0016C19B File Offset: 0x0016B19B
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

		// Token: 0x1700174D RID: 5965
		// (get) Token: 0x06005A2A RID: 23082 RVA: 0x0016C1CD File Offset: 0x0016B1CD
		public Type ConsumerType
		{
			get
			{
				return this._consumerType;
			}
		}

		// Token: 0x1700174E RID: 5966
		// (get) Token: 0x06005A2B RID: 23083 RVA: 0x0016C1D5 File Offset: 0x0016B1D5
		public Type ProviderType
		{
			get
			{
				return this._providerType;
			}
		}

		// Token: 0x06005A2C RID: 23084 RVA: 0x0016C1DD File Offset: 0x0016B1DD
		public static Type GetConsumerType(Type transformerType)
		{
			return WebPartTransformerAttribute.GetTransformerTypes(transformerType)[0];
		}

		// Token: 0x06005A2D RID: 23085 RVA: 0x0016C1E7 File Offset: 0x0016B1E7
		public static Type GetProviderType(Type transformerType)
		{
			return WebPartTransformerAttribute.GetTransformerTypes(transformerType)[1];
		}

		// Token: 0x06005A2E RID: 23086 RVA: 0x0016C1F4 File Offset: 0x0016B1F4
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

		// Token: 0x06005A2F RID: 23087 RVA: 0x0016C26C File Offset: 0x0016B26C
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

		// Token: 0x04003080 RID: 12416
		private static readonly Hashtable transformerCache = Hashtable.Synchronized(new Hashtable());

		// Token: 0x04003081 RID: 12417
		private Type _consumerType;

		// Token: 0x04003082 RID: 12418
		private Type _providerType;
	}
}
