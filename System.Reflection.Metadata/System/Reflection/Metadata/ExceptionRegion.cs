using System;

namespace System.Reflection.Metadata
{
	// Token: 0x02000039 RID: 57
	public struct ExceptionRegion
	{
		// Token: 0x060002D2 RID: 722 RVA: 0x00008226 File Offset: 0x00006426
		internal ExceptionRegion(ExceptionRegionKind kind, int tryOffset, int tryLength, int handlerOffset, int handlerLength, int classTokenOrFilterOffset)
		{
			this._kind = kind;
			this._tryOffset = tryOffset;
			this._tryLength = tryLength;
			this._handlerOffset = handlerOffset;
			this._handlerLength = handlerLength;
			this._classTokenOrFilterOffset = classTokenOrFilterOffset;
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x00008255 File Offset: 0x00006455
		public ExceptionRegionKind Kind
		{
			get
			{
				return this._kind;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x0000825D File Offset: 0x0000645D
		public int TryOffset
		{
			get
			{
				return this._tryOffset;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x00008265 File Offset: 0x00006465
		public int TryLength
		{
			get
			{
				return this._tryLength;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x0000826D File Offset: 0x0000646D
		public int HandlerOffset
		{
			get
			{
				return this._handlerOffset;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x00008275 File Offset: 0x00006475
		public int HandlerLength
		{
			get
			{
				return this._handlerLength;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x0000827D File Offset: 0x0000647D
		public int FilterOffset
		{
			get
			{
				if (this.Kind != ExceptionRegionKind.Filter)
				{
					return -1;
				}
				return this._classTokenOrFilterOffset;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x00008290 File Offset: 0x00006490
		public EntityHandle CatchType
		{
			get
			{
				if (this.Kind != ExceptionRegionKind.Catch)
				{
					return default(EntityHandle);
				}
				return new EntityHandle((uint)this._classTokenOrFilterOffset);
			}
		}

		// Token: 0x0400028A RID: 650
		private readonly ExceptionRegionKind _kind;

		// Token: 0x0400028B RID: 651
		private readonly int _tryOffset;

		// Token: 0x0400028C RID: 652
		private readonly int _tryLength;

		// Token: 0x0400028D RID: 653
		private readonly int _handlerOffset;

		// Token: 0x0400028E RID: 654
		private readonly int _handlerLength;

		// Token: 0x0400028F RID: 655
		private readonly int _classTokenOrFilterOffset;
	}
}
