using System;
using System.Collections;

namespace System.Web.UI
{
	// Token: 0x02000319 RID: 793
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
	public sealed class ThemeableAttribute : Attribute
	{
		// Token: 0x06002505 RID: 9477 RVA: 0x0007A5AC File Offset: 0x000787AC
		public ThemeableAttribute(bool themeable)
		{
			this._themeable = themeable;
		}

		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x06002506 RID: 9478 RVA: 0x0007A5BB File Offset: 0x000787BB
		public bool Themeable
		{
			get
			{
				return this._themeable;
			}
		}

		// Token: 0x06002507 RID: 9479 RVA: 0x0007A5C4 File Offset: 0x000787C4
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ThemeableAttribute themeableAttribute = obj as ThemeableAttribute;
			return themeableAttribute != null && themeableAttribute.Themeable == this._themeable;
		}

		// Token: 0x06002508 RID: 9480 RVA: 0x0007A5F1 File Offset: 0x000787F1
		public override int GetHashCode()
		{
			return this._themeable.GetHashCode();
		}

		// Token: 0x06002509 RID: 9481 RVA: 0x0007A5FE File Offset: 0x000787FE
		public override bool IsDefaultAttribute()
		{
			return this.Equals(ThemeableAttribute.Default);
		}

		// Token: 0x0600250A RID: 9482 RVA: 0x0007A60B File Offset: 0x0007880B
		public static bool IsObjectThemeable(object instance)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			return ThemeableAttribute.IsTypeThemeable(instance.GetType());
		}

		// Token: 0x0600250B RID: 9483 RVA: 0x0007A628 File Offset: 0x00078828
		public static bool IsTypeThemeable(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			object obj = ThemeableAttribute._themeableTypes[type];
			if (obj != null)
			{
				return (bool)obj;
			}
			ThemeableAttribute themeableAttribute = Attribute.GetCustomAttribute(type, typeof(ThemeableAttribute)) as ThemeableAttribute;
			obj = (themeableAttribute != null && themeableAttribute.Themeable);
			ThemeableAttribute._themeableTypes[type] = obj;
			return (bool)obj;
		}

		// Token: 0x04001D61 RID: 7521
		public static readonly ThemeableAttribute Yes = new ThemeableAttribute(true);

		// Token: 0x04001D62 RID: 7522
		public static readonly ThemeableAttribute No = new ThemeableAttribute(false);

		// Token: 0x04001D63 RID: 7523
		public static readonly ThemeableAttribute Default = ThemeableAttribute.Yes;

		// Token: 0x04001D64 RID: 7524
		private bool _themeable;

		// Token: 0x04001D65 RID: 7525
		private static Hashtable _themeableTypes = Hashtable.Synchronized(new Hashtable());
	}
}
