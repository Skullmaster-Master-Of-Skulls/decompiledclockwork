using System;
using System.Globalization;

namespace System.Web.Razor.Text
{
	// Token: 0x02000095 RID: 149
	[Serializable]
	public struct SourceLocation : IEquatable<SourceLocation>, IComparable<SourceLocation>
	{
		// Token: 0x06000696 RID: 1686 RVA: 0x00018196 File Offset: 0x00016396
		public SourceLocation(int absoluteIndex, int lineIndex, int characterIndex)
		{
			this._absoluteIndex = absoluteIndex;
			this._lineIndex = lineIndex;
			this._characterIndex = characterIndex;
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x000181AD File Offset: 0x000163AD
		public int AbsoluteIndex
		{
			get
			{
				return this._absoluteIndex;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000698 RID: 1688 RVA: 0x000181B5 File Offset: 0x000163B5
		public int LineIndex
		{
			get
			{
				return this._lineIndex;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x000181BD File Offset: 0x000163BD
		public int CharacterIndex
		{
			get
			{
				return this._characterIndex;
			}
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x000181C8 File Offset: 0x000163C8
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "({0}:{1},{2})", new object[]
			{
				this.AbsoluteIndex,
				this.LineIndex,
				this.CharacterIndex
			});
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x00018216 File Offset: 0x00016416
		public override bool Equals(object obj)
		{
			return obj is SourceLocation && this.Equals((SourceLocation)obj);
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x0001822E File Offset: 0x0001642E
		public override int GetHashCode()
		{
			return this.AbsoluteIndex;
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x00018236 File Offset: 0x00016436
		public bool Equals(SourceLocation other)
		{
			return this.AbsoluteIndex == other.AbsoluteIndex && this.LineIndex == other.LineIndex && this.CharacterIndex == other.CharacterIndex;
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x00018268 File Offset: 0x00016468
		public int CompareTo(SourceLocation other)
		{
			return this.AbsoluteIndex.CompareTo(other.AbsoluteIndex);
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0001828C File Offset: 0x0001648C
		public static SourceLocation Advance(SourceLocation left, string text)
		{
			SourceLocationTracker sourceLocationTracker = new SourceLocationTracker(left);
			sourceLocationTracker.UpdateLocation(text);
			return sourceLocationTracker.CurrentLocation;
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x000182B0 File Offset: 0x000164B0
		public static SourceLocation Add(SourceLocation left, SourceLocation right)
		{
			if (right.LineIndex > 0)
			{
				return new SourceLocation(left.AbsoluteIndex + right.AbsoluteIndex, left.LineIndex + right.LineIndex, right.CharacterIndex);
			}
			return new SourceLocation(left.AbsoluteIndex + right.AbsoluteIndex, left.LineIndex + right.LineIndex, left.CharacterIndex + right.CharacterIndex);
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x00018324 File Offset: 0x00016524
		public static SourceLocation Subtract(SourceLocation left, SourceLocation right)
		{
			return new SourceLocation(left.AbsoluteIndex - right.AbsoluteIndex, left.LineIndex - right.LineIndex, (left.LineIndex != right.LineIndex) ? left.CharacterIndex : (left.CharacterIndex - right.CharacterIndex));
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x0001837C File Offset: 0x0001657C
		private static SourceLocation CreateUndefined()
		{
			return new SourceLocation
			{
				_absoluteIndex = -1,
				_lineIndex = -1,
				_characterIndex = -1
			};
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x000183AA File Offset: 0x000165AA
		public static bool operator <(SourceLocation left, SourceLocation right)
		{
			return left.CompareTo(right) < 0;
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x000183B7 File Offset: 0x000165B7
		public static bool operator >(SourceLocation left, SourceLocation right)
		{
			return left.CompareTo(right) > 0;
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x000183C4 File Offset: 0x000165C4
		public static bool operator ==(SourceLocation left, SourceLocation right)
		{
			return left.Equals(right);
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x000183CE File Offset: 0x000165CE
		public static bool operator !=(SourceLocation left, SourceLocation right)
		{
			return !left.Equals(right);
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x000183DB File Offset: 0x000165DB
		public static SourceLocation operator +(SourceLocation left, SourceLocation right)
		{
			return SourceLocation.Add(left, right);
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x000183E4 File Offset: 0x000165E4
		public static SourceLocation operator -(SourceLocation left, SourceLocation right)
		{
			return SourceLocation.Subtract(left, right);
		}

		// Token: 0x0400032F RID: 815
		public static readonly SourceLocation Undefined = SourceLocation.CreateUndefined();

		// Token: 0x04000330 RID: 816
		public static readonly SourceLocation Zero = new SourceLocation(0, 0, 0);

		// Token: 0x04000331 RID: 817
		private int _absoluteIndex;

		// Token: 0x04000332 RID: 818
		private int _lineIndex;

		// Token: 0x04000333 RID: 819
		private int _characterIndex;
	}
}
