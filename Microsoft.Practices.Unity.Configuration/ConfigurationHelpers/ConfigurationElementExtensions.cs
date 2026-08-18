using System;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Xml;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity.Configuration.ConfigurationHelpers
{
	// Token: 0x0200000C RID: 12
	public static class ConfigurationElementExtensions
	{
		// Token: 0x06000032 RID: 50 RVA: 0x000026A8 File Offset: 0x000008A8
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "baseElement", Justification = "Made this an extension method to get nice usage syntax.")]
		public static TElementType ReadUnwrappedElement<TElementType>(this ConfigurationElement baseElement, XmlReader reader, DeserializableConfigurationElementCollectionBase<TElementType> elementCollection) where TElementType : DeserializableConfigurationElement, new()
		{
			Guard.ArgumentNotNull(reader, "reader");
			Guard.ArgumentNotNull(elementCollection, "elementCollection");
			TElementType telementType = Activator.CreateInstance<TElementType>();
			telementType.Deserialize(reader);
			elementCollection.Add(telementType);
			return telementType;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000026E8 File Offset: 0x000008E8
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "baseElement", Justification = "Made this an extension method to get nice usage syntax.")]
		public static TElementType ReadElementByType<TElementType>(this ConfigurationElement baseElement, XmlReader reader, Type elementType, DeserializableConfigurationElementCollectionBase<TElementType> elementCollection) where TElementType : DeserializableConfigurationElement
		{
			Guard.ArgumentNotNull(reader, "reader");
			Guard.ArgumentNotNull(elementType, "elementType");
			Guard.ArgumentNotNull(elementCollection, "elementCollection");
			Guard.TypeIsAssignable(typeof(TElementType), elementType, "elementType");
			TElementType telementType = (TElementType)((object)Activator.CreateInstance(elementType));
			telementType.Deserialize(reader);
			elementCollection.Add(telementType);
			return telementType;
		}
	}
}
