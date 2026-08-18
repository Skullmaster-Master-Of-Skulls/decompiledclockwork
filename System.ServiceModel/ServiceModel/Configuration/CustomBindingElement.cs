using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200060F RID: 1551
	public class CustomBindingElement : NamedServiceModelExtensionCollectionElement<BindingElementExtensionElement>, ICollection<BindingElementExtensionElement>, IEnumerable<BindingElementExtensionElement>, IEnumerable, IBindingConfigurationElement
	{
		// Token: 0x06003BB6 RID: 15286 RVA: 0x000E46D4 File Offset: 0x000E28D4
		public CustomBindingElement() : this(null)
		{
		}

		// Token: 0x06003BB7 RID: 15287 RVA: 0x000E46DD File Offset: 0x000E28DD
		public CustomBindingElement(string name) : base("bindingElementExtensions", name)
		{
		}

		// Token: 0x17000E4E RID: 3662
		// (get) Token: 0x06003BB8 RID: 15288 RVA: 0x000E46EB File Offset: 0x000E28EB
		// (set) Token: 0x06003BB9 RID: 15289 RVA: 0x000E46FD File Offset: 0x000E28FD
		[ConfigurationProperty("closeTimeout", DefaultValue = "00:01:00")]
		[TypeConverter(typeof(TimeSpanOrInfiniteConverter))]
		[ServiceModelTimeSpanValidator(MinValueString = "00:00:00")]
		public TimeSpan CloseTimeout
		{
			get
			{
				return (TimeSpan)base["closeTimeout"];
			}
			set
			{
				base["closeTimeout"] = value;
				base.SetIsModified();
			}
		}

		// Token: 0x17000E4F RID: 3663
		// (get) Token: 0x06003BBA RID: 15290 RVA: 0x000E4716 File Offset: 0x000E2916
		// (set) Token: 0x06003BBB RID: 15291 RVA: 0x000E4728 File Offset: 0x000E2928
		[ConfigurationProperty("openTimeout", DefaultValue = "00:01:00")]
		[TypeConverter(typeof(TimeSpanOrInfiniteConverter))]
		[ServiceModelTimeSpanValidator(MinValueString = "00:00:00")]
		public TimeSpan OpenTimeout
		{
			get
			{
				return (TimeSpan)base["openTimeout"];
			}
			set
			{
				base["openTimeout"] = value;
				base.SetIsModified();
			}
		}

		// Token: 0x17000E50 RID: 3664
		// (get) Token: 0x06003BBC RID: 15292 RVA: 0x000E4741 File Offset: 0x000E2941
		// (set) Token: 0x06003BBD RID: 15293 RVA: 0x000E4753 File Offset: 0x000E2953
		[ConfigurationProperty("receiveTimeout", DefaultValue = "00:10:00")]
		[TypeConverter(typeof(TimeSpanOrInfiniteConverter))]
		[ServiceModelTimeSpanValidator(MinValueString = "00:00:00")]
		public TimeSpan ReceiveTimeout
		{
			get
			{
				return (TimeSpan)base["receiveTimeout"];
			}
			set
			{
				base["receiveTimeout"] = value;
				base.SetIsModified();
			}
		}

		// Token: 0x17000E51 RID: 3665
		// (get) Token: 0x06003BBE RID: 15294 RVA: 0x000E476C File Offset: 0x000E296C
		// (set) Token: 0x06003BBF RID: 15295 RVA: 0x000E477E File Offset: 0x000E297E
		[ConfigurationProperty("sendTimeout", DefaultValue = "00:01:00")]
		[TypeConverter(typeof(TimeSpanOrInfiniteConverter))]
		[ServiceModelTimeSpanValidator(MinValueString = "00:00:00")]
		public TimeSpan SendTimeout
		{
			get
			{
				return (TimeSpan)base["sendTimeout"];
			}
			set
			{
				base["sendTimeout"] = value;
				base.SetIsModified();
			}
		}

		// Token: 0x06003BC0 RID: 15296 RVA: 0x000E4798 File Offset: 0x000E2998
		public override void Add(BindingElementExtensionElement element)
		{
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			BindingElementExtensionElement bindingElementExtensionElement = null;
			if (!this.CanAddEncodingElement(element, ref bindingElementExtensionElement))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigMessageEncodingAlreadyInBinding", new object[]
				{
					bindingElementExtensionElement.ConfigurationElementName,
					bindingElementExtensionElement.GetType().AssemblyQualifiedName
				})));
			}
			if (!this.CanAddStreamUpgradeElement(element, ref bindingElementExtensionElement))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigStreamUpgradeElementAlreadyInBinding", new object[]
				{
					bindingElementExtensionElement.ConfigurationElementName,
					bindingElementExtensionElement.GetType().AssemblyQualifiedName
				})));
			}
			if (!this.CanAddTransportElement(element, ref bindingElementExtensionElement))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigTransportAlreadyInBinding", new object[]
				{
					bindingElementExtensionElement.ConfigurationElementName,
					bindingElementExtensionElement.GetType().AssemblyQualifiedName
				})));
			}
			base.Add(element);
		}

		// Token: 0x06003BC1 RID: 15297 RVA: 0x000E4888 File Offset: 0x000E2A88
		public void ApplyConfiguration(Binding binding)
		{
			if (binding == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("binding");
			}
			if (binding.GetType() != typeof(CustomBinding))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("ConfigInvalidTypeForBinding", new object[]
				{
					typeof(CustomBinding).AssemblyQualifiedName,
					binding.GetType().AssemblyQualifiedName
				}));
			}
			binding.CloseTimeout = this.CloseTimeout;
			binding.OpenTimeout = this.OpenTimeout;
			binding.ReceiveTimeout = this.ReceiveTimeout;
			binding.SendTimeout = this.SendTimeout;
			this.OnApplyConfiguration(binding);
		}

		// Token: 0x06003BC2 RID: 15298 RVA: 0x000E4934 File Offset: 0x000E2B34
		public override bool CanAdd(BindingElementExtensionElement element)
		{
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			BindingElementExtensionElement bindingElementExtensionElement = null;
			return !base.ContainsKey(element.GetType()) && this.CanAddEncodingElement(element, ref bindingElementExtensionElement) && this.CanAddStreamUpgradeElement(element, ref bindingElementExtensionElement) && this.CanAddTransportElement(element, ref bindingElementExtensionElement);
		}

		// Token: 0x06003BC3 RID: 15299 RVA: 0x000E4985 File Offset: 0x000E2B85
		private bool CanAddEncodingElement(BindingElementExtensionElement element, ref BindingElementExtensionElement existingElement)
		{
			return this.CanAddExclusiveElement(typeof(MessageEncodingBindingElement), element.BindingElementType, ref existingElement);
		}

		// Token: 0x06003BC4 RID: 15300 RVA: 0x000E49A0 File Offset: 0x000E2BA0
		private bool CanAddExclusiveElement(Type exclusiveType, Type bindingElementType, ref BindingElementExtensionElement existingElement)
		{
			bool result = true;
			if (exclusiveType.IsAssignableFrom(bindingElementType))
			{
				foreach (BindingElementExtensionElement bindingElementExtensionElement in this)
				{
					if (exclusiveType.IsAssignableFrom(bindingElementExtensionElement.BindingElementType))
					{
						result = false;
						existingElement = bindingElementExtensionElement;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06003BC5 RID: 15301 RVA: 0x000E4A04 File Offset: 0x000E2C04
		private bool CanAddStreamUpgradeElement(BindingElementExtensionElement element, ref BindingElementExtensionElement existingElement)
		{
			return this.CanAddExclusiveElement(typeof(StreamUpgradeBindingElement), element.BindingElementType, ref existingElement);
		}

		// Token: 0x06003BC6 RID: 15302 RVA: 0x000E4A1D File Offset: 0x000E2C1D
		private bool CanAddTransportElement(BindingElementExtensionElement element, ref BindingElementExtensionElement existingElement)
		{
			return this.CanAddExclusiveElement(typeof(TransportBindingElement), element.BindingElementType, ref existingElement);
		}

		// Token: 0x06003BC7 RID: 15303 RVA: 0x000E4A38 File Offset: 0x000E2C38
		protected void OnApplyConfiguration(Binding binding)
		{
			CustomBinding customBinding = (CustomBinding)binding;
			foreach (BindingElementExtensionElement bindingElementExtensionElement in this)
			{
				customBinding.Elements.Add(bindingElementExtensionElement.CreateBindingElement());
			}
		}

		// Token: 0x17000E52 RID: 3666
		// (get) Token: 0x06003BC8 RID: 15304 RVA: 0x000E4A94 File Offset: 0x000E2C94
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					object lockObj = this.lockObj;
					lock (lockObj)
					{
						if (this.properties == null)
						{
							ConfigurationPropertyCollection configurationPropertyCollection = base.Properties;
							configurationPropertyCollection.Add(new ConfigurationProperty("closeTimeout", typeof(TimeSpan), TimeSpan.Parse("00:01:00", CultureInfo.InvariantCulture), new TimeSpanOrInfiniteConverter(), new TimeSpanOrInfiniteValidator(TimeSpan.Parse("00:00:00", CultureInfo.InvariantCulture), TimeSpan.Parse("24.20:31:23.6470000", CultureInfo.InvariantCulture)), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("openTimeout", typeof(TimeSpan), TimeSpan.Parse("00:01:00", CultureInfo.InvariantCulture), new TimeSpanOrInfiniteConverter(), new TimeSpanOrInfiniteValidator(TimeSpan.Parse("00:00:00", CultureInfo.InvariantCulture), TimeSpan.Parse("24.20:31:23.6470000", CultureInfo.InvariantCulture)), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("receiveTimeout", typeof(TimeSpan), TimeSpan.Parse("00:10:00", CultureInfo.InvariantCulture), new TimeSpanOrInfiniteConverter(), new TimeSpanOrInfiniteValidator(TimeSpan.Parse("00:00:00", CultureInfo.InvariantCulture), TimeSpan.Parse("24.20:31:23.6470000", CultureInfo.InvariantCulture)), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("sendTimeout", typeof(TimeSpan), TimeSpan.Parse("00:01:00", CultureInfo.InvariantCulture), new TimeSpanOrInfiniteConverter(), new TimeSpanOrInfiniteValidator(TimeSpan.Parse("00:00:00", CultureInfo.InvariantCulture), TimeSpan.Parse("24.20:31:23.6470000", CultureInfo.InvariantCulture)), ConfigurationPropertyOptions.None));
							this.properties = configurationPropertyCollection;
						}
					}
				}
				return this.properties;
			}
		}

		// Token: 0x04002C72 RID: 11378
		private ConfigurationPropertyCollection properties;
	}
}
