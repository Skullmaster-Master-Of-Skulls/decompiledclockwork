using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x020005A1 RID: 1441
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class ReferenceConverter : TypeConverter
	{
		// Token: 0x0600359F RID: 13727 RVA: 0x000E8E5E File Offset: 0x000E705E
		public ReferenceConverter(Type type)
		{
			this.type = type;
		}

		// Token: 0x060035A0 RID: 13728 RVA: 0x000E8E6D File Offset: 0x000E706D
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return (sourceType == typeof(string) && context != null) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x060035A1 RID: 13729 RVA: 0x000E8E90 File Offset: 0x000E7090
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				string text = ((string)value).Trim();
				if (!string.Equals(text, ReferenceConverter.none) && context != null)
				{
					IReferenceService referenceService = (IReferenceService)context.GetService(typeof(IReferenceService));
					if (referenceService != null)
					{
						object reference = referenceService.GetReference(text);
						if (reference != null)
						{
							return reference;
						}
					}
					IContainer container = context.Container;
					if (container != null)
					{
						object obj = container.Components[text];
						if (obj != null)
						{
							return obj;
						}
					}
				}
				return null;
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x060035A2 RID: 13730 RVA: 0x000E8F14 File Offset: 0x000E7114
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (!(destinationType == typeof(string)))
			{
				return base.ConvertTo(context, culture, value, destinationType);
			}
			if (value != null)
			{
				if (context != null)
				{
					IReferenceService referenceService = (IReferenceService)context.GetService(typeof(IReferenceService));
					if (referenceService != null)
					{
						string name = referenceService.GetName(value);
						if (name != null)
						{
							return name;
						}
					}
				}
				if (!Marshal.IsComObject(value) && value is IComponent)
				{
					IComponent component = (IComponent)value;
					ISite site = component.Site;
					if (site != null)
					{
						string name2 = site.Name;
						if (name2 != null)
						{
							return name2;
						}
					}
				}
				return string.Empty;
			}
			return ReferenceConverter.none;
		}

		// Token: 0x060035A3 RID: 13731 RVA: 0x000E8FBC File Offset: 0x000E71BC
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			object[] array = null;
			if (context != null)
			{
				ArrayList arrayList = new ArrayList();
				arrayList.Add(null);
				IReferenceService referenceService = (IReferenceService)context.GetService(typeof(IReferenceService));
				if (referenceService != null)
				{
					object[] references = referenceService.GetReferences(this.type);
					int num = references.Length;
					for (int i = 0; i < num; i++)
					{
						if (this.IsValueAllowed(context, references[i]))
						{
							arrayList.Add(references[i]);
						}
					}
				}
				else
				{
					IContainer container = context.Container;
					if (container != null)
					{
						ComponentCollection components = container.Components;
						foreach (object obj in components)
						{
							IComponent component = (IComponent)obj;
							if (component != null && this.type.IsInstanceOfType(component) && this.IsValueAllowed(context, component))
							{
								arrayList.Add(component);
							}
						}
					}
				}
				array = arrayList.ToArray();
				Array.Sort(array, 0, array.Length, new ReferenceConverter.ReferenceComparer(this));
			}
			return new TypeConverter.StandardValuesCollection(array);
		}

		// Token: 0x060035A4 RID: 13732 RVA: 0x000E90D8 File Offset: 0x000E72D8
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x060035A5 RID: 13733 RVA: 0x000E90DB File Offset: 0x000E72DB
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x060035A6 RID: 13734 RVA: 0x000E90DE File Offset: 0x000E72DE
		protected virtual bool IsValueAllowed(ITypeDescriptorContext context, object value)
		{
			return true;
		}

		// Token: 0x04002A5E RID: 10846
		private static readonly string none = SR.GetString("toStringNone");

		// Token: 0x04002A5F RID: 10847
		private Type type;

		// Token: 0x0200089C RID: 2204
		private class ReferenceComparer : IComparer
		{
			// Token: 0x060045B9 RID: 17849 RVA: 0x00123830 File Offset: 0x00121A30
			public ReferenceComparer(ReferenceConverter converter)
			{
				this.converter = converter;
			}

			// Token: 0x060045BA RID: 17850 RVA: 0x00123840 File Offset: 0x00121A40
			public int Compare(object item1, object item2)
			{
				string strA = this.converter.ConvertToString(item1);
				string strB = this.converter.ConvertToString(item2);
				return string.Compare(strA, strB, false, CultureInfo.InvariantCulture);
			}

			// Token: 0x040037EC RID: 14316
			private ReferenceConverter converter;
		}
	}
}
