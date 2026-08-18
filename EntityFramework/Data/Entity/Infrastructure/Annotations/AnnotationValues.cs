using System;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Infrastructure.Annotations
{
	// Token: 0x0200013D RID: 317
	public sealed class AnnotationValues
	{
		// Token: 0x06000A92 RID: 2706 RVA: 0x00035FCD File Offset: 0x000341CD
		public AnnotationValues(object oldValue, object newValue)
		{
			this._oldValue = oldValue;
			this._newValue = newValue;
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000A93 RID: 2707 RVA: 0x00035FE3 File Offset: 0x000341E3
		public object OldValue
		{
			get
			{
				return this._oldValue;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000A94 RID: 2708 RVA: 0x00035FEB File Offset: 0x000341EB
		public object NewValue
		{
			get
			{
				return this._newValue;
			}
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x00035FF3 File Offset: 0x000341F3
		private bool Equals(AnnotationValues other)
		{
			return object.Equals(this._oldValue, other._oldValue) && object.Equals(this._newValue, other._newValue);
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x0003601B File Offset: 0x0003421B
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		public override bool Equals(object obj)
		{
			return !object.ReferenceEquals(null, obj) && (object.ReferenceEquals(this, obj) || (obj is AnnotationValues && this.Equals((AnnotationValues)obj)));
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x00036049 File Offset: 0x00034249
		public override int GetHashCode()
		{
			return ((this._oldValue != null) ? this._oldValue.GetHashCode() : 0) * 397 ^ ((this._newValue != null) ? this._newValue.GetHashCode() : 0);
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x0003607E File Offset: 0x0003427E
		[SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates")]
		public static bool operator ==(AnnotationValues left, AnnotationValues right)
		{
			return object.Equals(left, right);
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x00036087 File Offset: 0x00034287
		[SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates")]
		public static bool operator !=(AnnotationValues left, AnnotationValues right)
		{
			return !object.Equals(left, right);
		}

		// Token: 0x040002D3 RID: 723
		private readonly object _oldValue;

		// Token: 0x040002D4 RID: 724
		private readonly object _newValue;
	}
}
