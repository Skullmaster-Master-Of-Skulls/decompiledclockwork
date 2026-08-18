using System;
using System.Globalization;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.QueryRewriting
{
	// Token: 0x02000452 RID: 1106
	internal class TileBinaryOperator<T_Query> : Tile<T_Query> where T_Query : ITileQuery
	{
		// Token: 0x060028A8 RID: 10408 RVA: 0x000C5B27 File Offset: 0x000C3D27
		public TileBinaryOperator(Tile<T_Query> arg1, Tile<T_Query> arg2, TileOpKind opKind, T_Query query) : base(opKind, query)
		{
			this.m_arg1 = arg1;
			this.m_arg2 = arg2;
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x060028A9 RID: 10409 RVA: 0x000C5B40 File Offset: 0x000C3D40
		public override Tile<T_Query> Arg1
		{
			get
			{
				return this.m_arg1;
			}
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x060028AA RID: 10410 RVA: 0x000C5B48 File Offset: 0x000C3D48
		public override Tile<T_Query> Arg2
		{
			get
			{
				return this.m_arg2;
			}
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x060028AB RID: 10411 RVA: 0x000C5B50 File Offset: 0x000C3D50
		public override string Description
		{
			get
			{
				string format = null;
				switch (base.OpKind)
				{
				case TileOpKind.Union:
					format = "({0} | {1})";
					break;
				case TileOpKind.Join:
					format = "({0} & {1})";
					break;
				case TileOpKind.AntiSemiJoin:
					format = "({0} - {1})";
					break;
				}
				return string.Format(CultureInfo.InvariantCulture, format, new object[]
				{
					this.Arg1.Description,
					this.Arg2.Description
				});
			}
		}

		// Token: 0x060028AC RID: 10412 RVA: 0x000C5BC0 File Offset: 0x000C3DC0
		internal override Tile<T_Query> Replace(Tile<T_Query> oldTile, Tile<T_Query> newTile)
		{
			Tile<T_Query> tile = this.Arg1.Replace(oldTile, newTile);
			Tile<T_Query> tile2 = this.Arg2.Replace(oldTile, newTile);
			if (tile != this.Arg1 || tile2 != this.Arg2)
			{
				return new TileBinaryOperator<T_Query>(tile, tile2, base.OpKind, base.Query);
			}
			return this;
		}

		// Token: 0x04000F39 RID: 3897
		private readonly Tile<T_Query> m_arg1;

		// Token: 0x04000F3A RID: 3898
		private readonly Tile<T_Query> m_arg2;
	}
}
