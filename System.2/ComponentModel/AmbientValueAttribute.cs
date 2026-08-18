using System;

namespace System.ComponentModel
{
	// Token: 0x0200050E RID: 1294
	[AttributeUsage(AttributeTargets.All)]
	public sealed class AmbientValueAttribute : Attribute
	{
		// Token: 0x06003114 RID: 12564 RVA: 0x000DECC8 File Offset: 0x000DCEC8
		public AmbientValueAttribute(Type type, string value)
		{
			try
			{
				this.value = TypeDescriptor.GetConverter(type).ConvertFromInvariantString(value);
			}
			catch
			{
			}
		}

		// Token: 0x06003115 RID: 12565 RVA: 0x000DED04 File Offset: 0x000DCF04
		public AmbientValueAttribute(char value)
		{
			this.value = value;
		}

		// Token: 0x06003116 RID: 12566 RVA: 0x000DED18 File Offset: 0x000DCF18
		public AmbientValueAttribute(byte value)
		{
			this.value = value;
		}

		// Token: 0x06003117 RID: 12567 RVA: 0x000DED2C File Offset: 0x000DCF2C
		public AmbientValueAttribute(short value)
		{
			this.value = value;
		}

		// Token: 0x06003118 RID: 12568 RVA: 0x000DED40 File Offset: 0x000DCF40
		public AmbientValueAttribute(int value)
		{
			this.value = value;
		}

		// Token: 0x06003119 RID: 12569 RVA: 0x000DED54 File Offset: 0x000DCF54
		public AmbientValueAttribute(long value)
		{
			this.value = value;
		}

		// Token: 0x0600311A RID: 12570 RVA: 0x000DED68 File Offset: 0x000DCF68
		public AmbientValueAttribute(float value)
		{
			this.value = value;
		}

		// Token: 0x0600311B RID: 12571 RVA: 0x000DED7C File Offset: 0x000DCF7C
		public AmbientValueAttribute(double value)
		{
			this.value = value;
		}

		// Token: 0x0600311C RID: 12572 RVA: 0x000DED90 File Offset: 0x000DCF90
		public AmbientValueAttribute(bool value)
		{
			this.value = value;
		}

		// Token: 0x0600311D RID: 12573 RVA: 0x000DEDA4 File Offset: 0x000DCFA4
		public AmbientValueAttribute(string value)
		{
			this.value = value;
		}

		// Token: 0x0600311E RID: 12574 RVA: 0x000DEDB3 File Offset: 0x000DCFB3
		public AmbientValueAttribute(object value)
		{
			this.value = value;
		}

		// Token: 0x17000C04 RID: 3076
		// (get) Token: 0x0600311F RID: 12575 RVA: 0x000DEDC2 File Offset: 0x000DCFC2
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06003120 RID: 12576 RVA: 0x000DEDCC File Offset: 0x000DCFCC
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			AmbientValueAttribute ambientValueAttribute = obj as AmbientValueAttribute;
			if (ambientValueAttribute == null)
			{
				return false;
			}
			if (this.value != null)
			{
				return this.value.Equals(ambientValueAttribute.Value);
			}
			return ambientValueAttribute.Value == null;
		}

		// Token: 0x06003121 RID: 12577 RVA: 0x000DEE0E File Offset: 0x000DD00E
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04002909 RID: 10505
		private readonly object value;
	}
}
