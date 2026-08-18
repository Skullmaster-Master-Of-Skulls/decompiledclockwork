using System;

namespace System.Web.UI
{
	// Token: 0x020002C8 RID: 712
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class NonVisualControlAttribute : Attribute
	{
		// Token: 0x06002014 RID: 8212 RVA: 0x000660B9 File Offset: 0x000642B9
		public NonVisualControlAttribute() : this(true)
		{
		}

		// Token: 0x06002015 RID: 8213 RVA: 0x000660C2 File Offset: 0x000642C2
		public NonVisualControlAttribute(bool nonVisual)
		{
			this._nonVisual = nonVisual;
		}

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x06002016 RID: 8214 RVA: 0x000660D1 File Offset: 0x000642D1
		public bool IsNonVisual
		{
			get
			{
				return this._nonVisual;
			}
		}

		// Token: 0x06002017 RID: 8215 RVA: 0x000660DC File Offset: 0x000642DC
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			NonVisualControlAttribute nonVisualControlAttribute = obj as NonVisualControlAttribute;
			return nonVisualControlAttribute != null && nonVisualControlAttribute.IsNonVisual == this.IsNonVisual;
		}

		// Token: 0x06002018 RID: 8216 RVA: 0x00066109 File Offset: 0x00064309
		public override int GetHashCode()
		{
			return this._nonVisual.GetHashCode();
		}

		// Token: 0x06002019 RID: 8217 RVA: 0x00066116 File Offset: 0x00064316
		public override bool IsDefaultAttribute()
		{
			return this.Equals(NonVisualControlAttribute.Default);
		}

		// Token: 0x04001ACD RID: 6861
		public static readonly NonVisualControlAttribute NonVisual = new NonVisualControlAttribute(true);

		// Token: 0x04001ACE RID: 6862
		public static readonly NonVisualControlAttribute Visual = new NonVisualControlAttribute(false);

		// Token: 0x04001ACF RID: 6863
		public static readonly NonVisualControlAttribute Default = NonVisualControlAttribute.Visual;

		// Token: 0x04001AD0 RID: 6864
		private bool _nonVisual;
	}
}
