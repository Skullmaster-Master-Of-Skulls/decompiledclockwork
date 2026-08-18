using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System.Windows.Forms
{
	// Token: 0x020002C1 RID: 705
	[TypeConverter(typeof(LinkArea.LinkAreaConverter))]
	[Serializable]
	public struct LinkArea
	{
		// Token: 0x06002B32 RID: 11058 RVA: 0x000C238D File Offset: 0x000C058D
		public LinkArea(int start, int length)
		{
			this.start = start;
			this.length = length;
		}

		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x06002B33 RID: 11059 RVA: 0x000C239D File Offset: 0x000C059D
		// (set) Token: 0x06002B34 RID: 11060 RVA: 0x000C23A5 File Offset: 0x000C05A5
		public int Start
		{
			get
			{
				return this.start;
			}
			set
			{
				this.start = value;
			}
		}

		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x06002B35 RID: 11061 RVA: 0x000C23AE File Offset: 0x000C05AE
		// (set) Token: 0x06002B36 RID: 11062 RVA: 0x000C23B6 File Offset: 0x000C05B6
		public int Length
		{
			get
			{
				return this.length;
			}
			set
			{
				this.length = value;
			}
		}

		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x06002B37 RID: 11063 RVA: 0x000C23BF File Offset: 0x000C05BF
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsEmpty
		{
			get
			{
				return this.length == this.start && this.start == 0;
			}
		}

		// Token: 0x06002B38 RID: 11064 RVA: 0x000C23DC File Offset: 0x000C05DC
		public override bool Equals(object o)
		{
			if (!(o is LinkArea))
			{
				return false;
			}
			LinkArea linkArea = (LinkArea)o;
			return this == linkArea;
		}

		// Token: 0x06002B39 RID: 11065 RVA: 0x000C2408 File Offset: 0x000C0608
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"{Start=",
				this.Start.ToString(CultureInfo.CurrentCulture),
				", Length=",
				this.Length.ToString(CultureInfo.CurrentCulture),
				"}"
			});
		}

		// Token: 0x06002B3A RID: 11066 RVA: 0x000C2464 File Offset: 0x000C0664
		public static bool operator ==(LinkArea linkArea1, LinkArea linkArea2)
		{
			return linkArea1.start == linkArea2.start && linkArea1.length == linkArea2.length;
		}

		// Token: 0x06002B3B RID: 11067 RVA: 0x000C2484 File Offset: 0x000C0684
		public static bool operator !=(LinkArea linkArea1, LinkArea linkArea2)
		{
			return !(linkArea1 == linkArea2);
		}

		// Token: 0x06002B3C RID: 11068 RVA: 0x000C2490 File Offset: 0x000C0690
		public override int GetHashCode()
		{
			return this.start << 4 | this.length;
		}

		// Token: 0x04001230 RID: 4656
		private int start;

		// Token: 0x04001231 RID: 4657
		private int length;

		// Token: 0x020006BA RID: 1722
		public class LinkAreaConverter : TypeConverter
		{
			// Token: 0x060068E2 RID: 26850 RVA: 0x000C24B8 File Offset: 0x000C06B8
			public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
			{
				return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
			}

			// Token: 0x060068E3 RID: 26851 RVA: 0x00027AC8 File Offset: 0x00025CC8
			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			{
				return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
			}

			// Token: 0x060068E4 RID: 26852 RVA: 0x001862AC File Offset: 0x001844AC
			public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
			{
				if (!(value is string))
				{
					return base.ConvertFrom(context, culture, value);
				}
				string text = ((string)value).Trim();
				if (text.Length == 0)
				{
					return null;
				}
				if (culture == null)
				{
					culture = CultureInfo.CurrentCulture;
				}
				char c = culture.TextInfo.ListSeparator[0];
				string[] array = text.Split(new char[]
				{
					c
				});
				int[] array2 = new int[array.Length];
				TypeConverter converter = TypeDescriptor.GetConverter(typeof(int));
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i] = (int)converter.ConvertFromString(context, culture, array[i]);
				}
				if (array2.Length == 2)
				{
					return new LinkArea(array2[0], array2[1]);
				}
				throw new ArgumentException(SR.GetString("TextParseFailedFormat", new object[]
				{
					text,
					"start, length"
				}));
			}

			// Token: 0x060068E5 RID: 26853 RVA: 0x0018638C File Offset: 0x0018458C
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType == null)
				{
					throw new ArgumentNullException("destinationType");
				}
				if (destinationType == typeof(string) && value is LinkArea)
				{
					LinkArea linkArea = (LinkArea)value;
					if (culture == null)
					{
						culture = CultureInfo.CurrentCulture;
					}
					string separator = culture.TextInfo.ListSeparator + " ";
					TypeConverter converter = TypeDescriptor.GetConverter(typeof(int));
					string[] array = new string[2];
					int num = 0;
					array[num++] = converter.ConvertToString(context, culture, linkArea.Start);
					array[num++] = converter.ConvertToString(context, culture, linkArea.Length);
					return string.Join(separator, array);
				}
				if (destinationType == typeof(InstanceDescriptor) && value is LinkArea)
				{
					LinkArea linkArea2 = (LinkArea)value;
					ConstructorInfo constructor = typeof(LinkArea).GetConstructor(new Type[]
					{
						typeof(int),
						typeof(int)
					});
					if (constructor != null)
					{
						return new InstanceDescriptor(constructor, new object[]
						{
							linkArea2.Start,
							linkArea2.Length
						});
					}
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}

			// Token: 0x060068E6 RID: 26854 RVA: 0x001864E7 File Offset: 0x001846E7
			public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
			{
				return new LinkArea((int)propertyValues["Start"], (int)propertyValues["Length"]);
			}

			// Token: 0x060068E7 RID: 26855 RVA: 0x00013062 File Offset: 0x00011262
			public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
			{
				return true;
			}

			// Token: 0x060068E8 RID: 26856 RVA: 0x00186514 File Offset: 0x00184714
			public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
			{
				PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(LinkArea), attributes);
				return properties.Sort(new string[]
				{
					"Start",
					"Length"
				});
			}

			// Token: 0x060068E9 RID: 26857 RVA: 0x00013062 File Offset: 0x00011262
			public override bool GetPropertiesSupported(ITypeDescriptorContext context)
			{
				return true;
			}
		}
	}
}
