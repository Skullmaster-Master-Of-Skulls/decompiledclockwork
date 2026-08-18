using System;
using System.Diagnostics;
using System.Globalization;
using Microsoft.Internal.Web.Utils;

namespace System.Web.Razor.Text
{
	// Token: 0x02000061 RID: 97
	[DebuggerDisplay("({Location})\"{Value}\"")]
	public class LocationTagged<T> : IFormattable
	{
		// Token: 0x0600047F RID: 1151 RVA: 0x00011FD8 File Offset: 0x000101D8
		private LocationTagged()
		{
			this.Location = SourceLocation.Undefined;
			this.Value = default(T);
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00012005 File Offset: 0x00010205
		public LocationTagged(T value, int offset, int line, int col) : this(value, new SourceLocation(offset, line, col))
		{
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00012017 File Offset: 0x00010217
		public LocationTagged(T value, SourceLocation location)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.Location = location;
			this.Value = value;
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x00012040 File Offset: 0x00010240
		// (set) Token: 0x06000483 RID: 1155 RVA: 0x00012048 File Offset: 0x00010248
		public SourceLocation Location { get; private set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x00012051 File Offset: 0x00010251
		// (set) Token: 0x06000485 RID: 1157 RVA: 0x00012059 File Offset: 0x00010259
		public T Value { get; private set; }

		// Token: 0x06000486 RID: 1158 RVA: 0x00012064 File Offset: 0x00010264
		public override bool Equals(object obj)
		{
			LocationTagged<T> locationTagged = obj as LocationTagged<T>;
			return locationTagged != null && object.Equals(locationTagged.Location, this.Location) && object.Equals(locationTagged.Value, this.Value);
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x000120BB File Offset: 0x000102BB
		public override int GetHashCode()
		{
			return HashCodeCombiner.Start().Add(this.Location).Add(this.Value).CombinedHash;
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x000120E8 File Offset: 0x000102E8
		public override string ToString()
		{
			T value = this.Value;
			return value.ToString();
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0001210C File Offset: 0x0001030C
		public string ToString(string format, IFormatProvider formatProvider)
		{
			if (string.IsNullOrEmpty(format))
			{
				format = "P";
			}
			if (formatProvider == null)
			{
				formatProvider = CultureInfo.CurrentCulture;
			}
			string a;
			if ((a = format.ToUpperInvariant()) != null && a == "F")
			{
				return string.Format(formatProvider, "{0}@{1}", new object[]
				{
					this.Value,
					this.Location
				});
			}
			T value = this.Value;
			return value.ToString();
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0001218D File Offset: 0x0001038D
		public static implicit operator T(LocationTagged<T> value)
		{
			return value.Value;
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00012195 File Offset: 0x00010395
		public static bool operator ==(LocationTagged<T> left, LocationTagged<T> right)
		{
			return object.Equals(left, right);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0001219E File Offset: 0x0001039E
		public static bool operator !=(LocationTagged<T> left, LocationTagged<T> right)
		{
			return !object.Equals(left, right);
		}
	}
}
