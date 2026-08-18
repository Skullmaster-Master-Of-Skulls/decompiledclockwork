using System;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x0200044E RID: 1102
	[AttributeUsage(AttributeTargets.Class)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class ParseChildrenAttribute : Attribute
	{
		// Token: 0x06003465 RID: 13413 RVA: 0x000E3548 File Offset: 0x000E2548
		public ParseChildrenAttribute() : this(false, null)
		{
		}

		// Token: 0x06003466 RID: 13414 RVA: 0x000E3552 File Offset: 0x000E2552
		public ParseChildrenAttribute(bool childrenAsProperties) : this(childrenAsProperties, null)
		{
		}

		// Token: 0x06003467 RID: 13415 RVA: 0x000E355C File Offset: 0x000E255C
		public ParseChildrenAttribute(Type childControlType) : this(false, null)
		{
			if (childControlType == null)
			{
				throw new ArgumentNullException("childControlType");
			}
			this._childControlType = childControlType;
		}

		// Token: 0x06003468 RID: 13416 RVA: 0x000E357B File Offset: 0x000E257B
		private ParseChildrenAttribute(bool childrenAsProperties, bool allowChanges) : this(childrenAsProperties, null)
		{
			this._allowChanges = allowChanges;
		}

		// Token: 0x06003469 RID: 13417 RVA: 0x000E358C File Offset: 0x000E258C
		public ParseChildrenAttribute(bool childrenAsProperties, string defaultProperty)
		{
			this._childrenAsProps = childrenAsProperties;
			if (this._childrenAsProps)
			{
				this._defaultProperty = defaultProperty;
			}
		}

		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x0600346A RID: 13418 RVA: 0x000E35B1 File Offset: 0x000E25B1
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

		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x0600346B RID: 13419 RVA: 0x000E35CC File Offset: 0x000E25CC
		// (set) Token: 0x0600346C RID: 13420 RVA: 0x000E35D4 File Offset: 0x000E25D4
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

		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x0600346D RID: 13421 RVA: 0x000E35EB File Offset: 0x000E25EB
		// (set) Token: 0x0600346E RID: 13422 RVA: 0x000E3601 File Offset: 0x000E2601
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

		// Token: 0x0600346F RID: 13423 RVA: 0x000E3618 File Offset: 0x000E2618
		public override int GetHashCode()
		{
			if (!this._childrenAsProps)
			{
				return HashCodeCombiner.CombineHashCodes(this._childrenAsProps.GetHashCode(), this._childControlType.GetHashCode());
			}
			return HashCodeCombiner.CombineHashCodes(this._childrenAsProps.GetHashCode(), this.DefaultProperty.GetHashCode());
		}

		// Token: 0x06003470 RID: 13424 RVA: 0x000E3664 File Offset: 0x000E2664
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

		// Token: 0x06003471 RID: 13425 RVA: 0x000E36BF File Offset: 0x000E26BF
		public override bool IsDefaultAttribute()
		{
			return this.Equals(ParseChildrenAttribute.Default);
		}

		// Token: 0x040024B9 RID: 9401
		public static readonly ParseChildrenAttribute ParseAsChildren = new ParseChildrenAttribute(false, false);

		// Token: 0x040024BA RID: 9402
		public static readonly ParseChildrenAttribute ParseAsProperties = new ParseChildrenAttribute(true, false);

		// Token: 0x040024BB RID: 9403
		public static readonly ParseChildrenAttribute Default = ParseChildrenAttribute.ParseAsChildren;

		// Token: 0x040024BC RID: 9404
		private bool _childrenAsProps;

		// Token: 0x040024BD RID: 9405
		private string _defaultProperty;

		// Token: 0x040024BE RID: 9406
		private Type _childControlType;

		// Token: 0x040024BF RID: 9407
		private bool _allowChanges = true;
	}
}
