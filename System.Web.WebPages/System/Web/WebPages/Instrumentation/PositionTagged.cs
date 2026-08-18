using System;
using System.Diagnostics;
using Microsoft.Internal.Web.Utils;

namespace System.Web.WebPages.Instrumentation
{
	// Token: 0x02000042 RID: 66
	[DebuggerDisplay("({Position})\"{Value}\"")]
	public class PositionTagged<T>
	{
		// Token: 0x060001C9 RID: 457 RVA: 0x000063AC File Offset: 0x000045AC
		private PositionTagged()
		{
			this.Position = 0;
			this.Value = default(T);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x000063D5 File Offset: 0x000045D5
		public PositionTagged(T value, int offset)
		{
			this.Position = offset;
			this.Value = value;
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001CB RID: 459 RVA: 0x000063EB File Offset: 0x000045EB
		// (set) Token: 0x060001CC RID: 460 RVA: 0x000063F3 File Offset: 0x000045F3
		public int Position { get; private set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001CD RID: 461 RVA: 0x000063FC File Offset: 0x000045FC
		// (set) Token: 0x060001CE RID: 462 RVA: 0x00006404 File Offset: 0x00004604
		public T Value { get; private set; }

		// Token: 0x060001CF RID: 463 RVA: 0x00006410 File Offset: 0x00004610
		public override bool Equals(object obj)
		{
			PositionTagged<T> positionTagged = obj as PositionTagged<T>;
			return positionTagged != null && positionTagged.Position == this.Position && object.Equals(positionTagged.Value, this.Value);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00006458 File Offset: 0x00004658
		public override int GetHashCode()
		{
			return HashCodeCombiner.Start().Add(this.Position).Add(this.Value).CombinedHash;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00006480 File Offset: 0x00004680
		public override string ToString()
		{
			T value = this.Value;
			return value.ToString();
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x000064A1 File Offset: 0x000046A1
		public static implicit operator T(PositionTagged<T> value)
		{
			return value.Value;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x000064A9 File Offset: 0x000046A9
		public static implicit operator PositionTagged<T>(Tuple<T, int> value)
		{
			return new PositionTagged<T>(value.Item1, value.Item2);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x000064BC File Offset: 0x000046BC
		public static bool operator ==(PositionTagged<T> left, PositionTagged<T> right)
		{
			return object.Equals(left, right);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x000064C5 File Offset: 0x000046C5
		public static bool operator !=(PositionTagged<T> left, PositionTagged<T> right)
		{
			return !object.Equals(left, right);
		}
	}
}
