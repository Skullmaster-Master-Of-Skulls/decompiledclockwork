using System;
using System.Diagnostics;

namespace AutoMapper
{
	// Token: 0x0200003F RID: 63
	[DebuggerDisplay("{SourceType.Name}, {DestinationType.Name}")]
	public class TypePair : IEquatable<TypePair>
	{
		// Token: 0x060002DB RID: 731 RVA: 0x000074D3 File Offset: 0x000056D3
		public TypePair(Type sourceType, Type destinationType)
		{
			this.SourceType = sourceType;
			this.DestinationType = destinationType;
			this._hashcode = (this.SourceType.GetHashCode() * 397 ^ this.DestinationType.GetHashCode());
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060002DC RID: 732 RVA: 0x0000750C File Offset: 0x0000570C
		public Type SourceType { get; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060002DD RID: 733 RVA: 0x00007514 File Offset: 0x00005714
		public Type DestinationType { get; }

		// Token: 0x060002DE RID: 734 RVA: 0x0000751C File Offset: 0x0000571C
		public bool Equals(TypePair other)
		{
			return object.Equals(other.SourceType, this.SourceType) && object.Equals(other.DestinationType, this.DestinationType);
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00007544 File Offset: 0x00005744
		public override bool Equals(object obj)
		{
			return obj != null && !(obj.GetType() != typeof(TypePair)) && this.Equals((TypePair)obj);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00007570 File Offset: 0x00005770
		public override int GetHashCode()
		{
			return this._hashcode;
		}

		// Token: 0x0400008F RID: 143
		private readonly int _hashcode;
	}
}
