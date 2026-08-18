using System;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x020002E4 RID: 740
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class ParseChildrenAttribute : Attribute
	{
		// Token: 0x06002261 RID: 8801 RVA: 0x000704C1 File Offset: 0x0006E6C1
		public ParseChildrenAttribute() : this(false, null)
		{
		}

		// Token: 0x06002262 RID: 8802 RVA: 0x000704CB File Offset: 0x0006E6CB
		public ParseChildrenAttribute(bool childrenAsProperties) : this(childrenAsProperties, null)
		{
		}

		// Token: 0x06002263 RID: 8803 RVA: 0x000704D5 File Offset: 0x0006E6D5
		public ParseChildrenAttribute(Type childControlType) : this(false, null)
		{
			if (childControlType == null)
			{
				throw new ArgumentNullException("childControlType");
			}
			this._childControlType = childControlType;
		}

		// Token: 0x06002264 RID: 8804 RVA: 0x000704FA File Offset: 0x0006E6FA
		private ParseChildrenAttribute(bool childrenAsProperties, bool allowChanges) : this(childrenAsProperties, null)
		{
			this._allowChanges = allowChanges;
		}

		// Token: 0x06002265 RID: 8805 RVA: 0x0007050B File Offset: 0x0006E70B
		public ParseChildrenAttribute(bool childrenAsProperties, string defaultProperty)
		{
			this._childrenAsProps = childrenAsProperties;
			if (this._childrenAsProps)
			{
				this._defaultProperty = defaultProperty;
			}
		}

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x06002266 RID: 8806 RVA: 0x00070530 File Offset: 0x0006E730
		public Type ChildControlType
		{
			get
			{
				if (this._childControlType == null)
				{
					return typeof(Control);
				}
				return this._childControlType;
			}
		}

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x06002267 RID: 8807 RVA: 0x00070551 File Offset: 0x0006E751
		// (set) Token: 0x06002268 RID: 8808 RVA: 0x00070559 File Offset: 0x0006E759
		public bool ChildrenAsProperties
		{
			get
			{
				return this._childrenAsProps;
			}
			set
			{
				if (!this._allowChanges)
				{
					throw new NotSupportedException();
				}
				this._childrenAsProps = value;
			}
		}

		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x06002269 RID: 8809 RVA: 0x00070570 File Offset: 0x0006E770
		// (set) Token: 0x0600226A RID: 8810 RVA: 0x00070586 File Offset: 0x0006E786
		public string DefaultProperty
		{
			get
			{
				if (this._defaultProperty == null)
				{
					return string.Empty;
				}
				return this._defaultProperty;
			}
			set
			{
				if (!this._allowChanges)
				{
					throw new NotSupportedException();
				}
				this._defaultProperty = value;
			}
		}

		// Token: 0x0600226B RID: 8811 RVA: 0x000705A0 File Offset: 0x0006E7A0
		public override int GetHashCode()
		{
			if (!this._childrenAsProps)
			{
				return HashCodeCombiner.CombineHashCodes(this._childrenAsProps.GetHashCode(), this._childControlType.GetHashCode());
			}
			return HashCodeCombiner.CombineHashCodes(this._childrenAsProps.GetHashCode(), this.DefaultProperty.GetHashCode());
		}

		// Token: 0x0600226C RID: 8812 RVA: 0x000705EC File Offset: 0x0006E7EC
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ParseChildrenAttribute parseChildrenAttribute = obj as ParseChildrenAttribute;
			if (parseChildrenAttribute == null)
			{
				return false;
			}
			if (!this._childrenAsProps)
			{
				return !parseChildrenAttribute.ChildrenAsProperties && parseChildrenAttribute._childControlType == this._childControlType;
			}
			return parseChildrenAttribute.ChildrenAsProperties && this.DefaultProperty.Equals(parseChildrenAttribute.DefaultProperty);
		}

		// Token: 0x0600226D RID: 8813 RVA: 0x0007064A File Offset: 0x0006E84A
		public override bool IsDefaultAttribute()
		{
			return this.Equals(ParseChildrenAttribute.Default);
		}

		// Token: 0x04001C3C RID: 7228
		public static readonly ParseChildrenAttribute ParseAsChildren = new ParseChildrenAttribute(false, false);

		// Token: 0x04001C3D RID: 7229
		public static readonly ParseChildrenAttribute ParseAsProperties = new ParseChildrenAttribute(true, false);

		// Token: 0x04001C3E RID: 7230
		public static readonly ParseChildrenAttribute Default = ParseChildrenAttribute.ParseAsChildren;

		// Token: 0x04001C3F RID: 7231
		private bool _childrenAsProps;

		// Token: 0x04001C40 RID: 7232
		private string _defaultProperty;

		// Token: 0x04001C41 RID: 7233
		private Type _childControlType;

		// Token: 0x04001C42 RID: 7234
		private bool _allowChanges = true;
	}
}
